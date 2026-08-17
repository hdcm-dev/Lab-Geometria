import * as THREE from 'three';

import type { DrawOutcome, MotionOptions, Piece, UndrawnPiece, ViewerOptions } from '../contract';
import { meshFor } from './meshes';
import { palette } from './palette';

/**
 * Una escena viva: su render, su cámara, sus mallas y su bucle de dibujo.
 *
 * LO QUE ESTA CLASE TIENE QUE HACER BIEN, Y ES LO QUE `PT-02` MIDE: **liberar todo**. Cada
 * geometría, cada material y el contexto WebGL se sueltan en `dispose`, porque el alumno navega
 * entre trabajos y cada navegación crea una escena nueva. Diez idas y vueltas sin liberar dejan
 * diez contextos vivos, y el navegador **corta el más viejo sin avisar**: la escena se apaga y
 * nadie sabe por qué.
 *
 * NO HAY NINGUNA PETICIÓN DE RED ACÁ, y no la hay por construcción: esta clase no conoce ninguna
 * dirección, no importa ningún cliente y todo lo que dibuja se lo dan por parámetro (`RA-02`).
 */
export class ViewerInstance {
  private readonly renderer: THREE.WebGLRenderer;
  private readonly scene: THREE.Scene;
  private readonly camera: THREE.PerspectiveCamera;
  private readonly pieces = new Map<number, THREE.Mesh>();
  private readonly grid: THREE.GridHelper;
  private readonly selectionColour = palette().selection;
  private readonly element: HTMLElement;

  private readonly onPieceSelected?: (position: number) => void;
  private readonly raycaster = new THREE.Raycaster();
  private pointerMoved = false;

  private frame: number | null = null;
  private motion: MotionOptions = { cameraOrbit: false, pieceSpin: false };
  private orbitAngle = 0;
  private radius = 10;
  private readonly focus = new THREE.Vector3(0, 1, 0);
  private dragging = false;

  public constructor(element: HTMLElement, options?: ViewerOptions) {
    this.element = element;
    this.onPieceSelected = options?.onPieceSelected;

    this.renderer = new THREE.WebGLRenderer({ antialias: true });
    this.renderer.setSize(element.clientWidth || 1, element.clientHeight || 1);
    element.appendChild(this.renderer.domElement);

    const colours = palette();

    this.scene = new THREE.Scene();
    this.scene.background = options?.background !== undefined
      ? new THREE.Color(options.background)
      : colours.background;

    // GRILLA DE REFERENCIA, del visor original y conservada por el port de la maqueta: sin un
    // plano de apoyo, tres figuras flotando en un fondo liso no dicen a qué escala están.
    this.grid = new THREE.GridHelper(60, 30, colours.grid, colours.grid);
    this.grid.material.opacity = 0.45;
    this.grid.material.transparent = true;
    this.scene.add(this.grid);

    this.camera = new THREE.PerspectiveCamera(50, this.aspect(), 0.1, 1000);

    this.scene.add(new THREE.AmbientLight(0xffffff, 0.75));
    const key = new THREE.DirectionalLight(0xffffff, 1.1);
    key.position.set(5, 8, 6);
    this.scene.add(key);

    // LOS DOS MOVIMIENTOS SE DETIENEN MIENTRAS LA PERSONA ARRASTRA (`F-25`, criterio de la
    // transición `g` → `h`): que la escena siga girando bajo el dedo es lo que hace imposible
    // encuadrar una figura.
    this.renderer.domElement.addEventListener('pointerdown', this.onPointerDown);
    this.renderer.domElement.addEventListener('pointerup', this.onPointerUp);
    this.renderer.domElement.addEventListener('pointerleave', this.onPointerUp);
    this.renderer.domElement.addEventListener('pointermove', this.onPointerMove);

    this.start();
  }

  /**
   * Reemplaza por completo lo dibujado y libera lo anterior.
   *
   * LA DISPOSICIÓN ES DETERMINISTA Y SE DERIVA DEL ÍNDICE (`F-13`): la pieza de la posición N va
   * siempre al mismo lugar de la fila, sin importar en qué orden llegaron ni cuántas se pudieron
   * dibujar. **Se predica de la posición y no de la orientación**, que es lo que permite que el
   * giro automático exista sin romper el criterio.
   */
  public load(pieces: readonly Piece[]): DrawOutcome {
    this.clearPieces();

    const drawn: number[] = [];
    const undrawn: UndrawnPiece[] = [];

    // LA FILA SE ARMA CON EL TAMAÑO DE CADA FIGURA Y NO CON UN PASO FIJO. Las figuras de un mismo
    // trabajo tienen escalas muy distintas —el ortoedro de `E-1` mide 21 y el cubo 3—, y un paso
    // fijo las superpone: la grande se come a las chicas y la escena muestra dos donde hay tres.
    //
    // EL ORDEN ES EL DE LA POSICIÓN Y NO EL DE LLEGADA, que es lo que sostiene el determinismo de
    // `F-13`: el mismo trabajo procesado dos veces arma la misma fila.
    const ordered = [...pieces].sort((a, b) => a.position - b.position);
    const built: Array<{ position: number; mesh: THREE.Mesh; size: number }> = [];

    for (const piece of ordered) {
      const outcome = meshFor(piece);

      if (outcome.mesh === null) {
        undrawn.push({ position: piece.position, reason: outcome.reason ?? 'UNKNOWN' });
        continue;
      }

      built.push({ position: piece.position, mesh: outcome.mesh, size: this.sizeOf(outcome.mesh) });
    }

    let cursor = 0;

    for (const item of built) {
      const half = item.size / 2;
      cursor += half;

      // Apoyadas SOBRE la grilla y no atravesándola: el plano es la referencia de escala. Las
      // figuras acostadas —las planas— quedan **sobre** el plano y no elevadas a media altura.
      const box = new THREE.Box3().setFromObject(item.mesh);
      const height = box.max.y - box.min.y;

      item.mesh.position.set(cursor, height < 0.01 ? 0.02 : height / 2, 0);
      this.scene.add(item.mesh);
      this.pieces.set(item.position, item.mesh);
      drawn.push(item.position);

      // Un cuarto del tamaño de aire entre una figura y la siguiente: separa sin dispersar.
      cursor += half + Math.max(item.size * 0.25, 0.5);
    }

    this.frameCamera();

    return { drawn, undrawn };
  }

  /** Resalta la pieza de esa posición. Devuelve falso si esa posición no está dibujada. */
  public select(position: number): boolean {
    const target = this.pieces.get(position);

    for (const [key, mesh] of this.pieces) {
      const material = mesh.material as THREE.MeshStandardMaterial;
      // EL RESALTE USA EL COLOR DE MARCA, que es el que la maqueta declara para la selección.
      if (key === position) {
        material.emissive.copy(this.selectionColour).multiplyScalar(0.45);
      } else {
        material.emissive.setHex(0x000000);
      }
    }

    return target !== undefined;
  }

  public resize(): void {
    this.renderer.setSize(this.element.clientWidth || 1, this.element.clientHeight || 1);
    this.camera.aspect = this.aspect();
    this.camera.updateProjectionMatrix();
  }

  /** Los dos movimientos se gobiernan por separado y se reciben, no se deciden (`F-25`). */
  public setMotion(options: MotionOptions): void {
    this.motion = options;
  }

  /**
   * Suelta TODO: el bucle, los escuchas, las geometrías, los materiales y el contexto WebGL.
   *
   * Es la mitad de `PT-02` que se puede romper sin que nada falle hoy y falle a la décima
   * navegación.
   */
  public dispose(): void {
    if (this.frame !== null) {
      cancelAnimationFrame(this.frame);
      this.frame = null;
    }

    this.renderer.domElement.removeEventListener('pointerdown', this.onPointerDown);
    this.renderer.domElement.removeEventListener('pointerup', this.onPointerUp);
    this.renderer.domElement.removeEventListener('pointerleave', this.onPointerUp);
    this.renderer.domElement.removeEventListener('pointermove', this.onPointerMove);

    this.clearPieces();

    this.grid.geometry.dispose();
    (this.grid.material as THREE.Material).dispose();

    this.scene.clear();
    this.renderer.dispose();
    this.renderer.forceContextLoss();

    if (this.renderer.domElement.parentNode !== null) {
      this.renderer.domElement.parentNode.removeChild(this.renderer.domElement);
    }
  }

  /** Cuántas mallas hay vivas. Lo usa la prueba de `PT-02` y nadie más. */
  public get liveMeshCount(): number {
    return this.pieces.size;
  }

  private clearPieces(): void {
    for (const mesh of this.pieces.values()) {
      this.scene.remove(mesh);
      mesh.geometry.dispose();

      const material = mesh.material as THREE.Material | THREE.Material[];
      if (Array.isArray(material)) {
        material.forEach((one) => one.dispose());
      } else {
        material.dispose();
      }
    }

    this.pieces.clear();
  }

  private sizeOf(mesh: THREE.Mesh): number {
    const box = new THREE.Box3().setFromObject(mesh);
    const size = new THREE.Vector3();
    box.getSize(size);
    return Math.max(size.x, size.y, size.z);
  }

  /**
   * Encuadra la cámara sobre TODO lo dibujado, calculado desde la caja real de la escena.
   *
   * SE CALCULA Y NO SE ESTIMA, y es la diferencia que se ve: la versión anterior derivaba la
   * distancia del ancho de la fila con un factor, y con seis figuras **tres quedaban fuera de
   * cuadro mientras el texto decía que se habían dibujado las seis**. Un encuadre que miente sobre
   * lo que dibujó es la misma clase de defecto que este visor existe para eliminar.
   *
   * LA DISTANCIA SALE DEL ÁNGULO DE LA CÁMARA: para que una esfera de radio `r` entre, hace falta
   * `r / sen(mitad del ángulo)`. Con el margen, entra con aire y sin depender de cuántas figuras
   * haya ni de cuán distintas sean entre sí.
   */
  private frameCamera(): void {
    const box = new THREE.Box3();

    for (const mesh of this.pieces.values()) {
      box.expandByObject(mesh);
    }

    if (box.isEmpty()) {
      this.focus.set(0, 1, 0);
      this.radius = 10;
    } else {
      box.getCenter(this.focus);

      const size = new THREE.Vector3();
      box.getSize(size);

      const enclosing = Math.max(size.x, size.y, size.z, 1) * 0.5 * Math.SQRT2;
      const halfAngle = THREE.MathUtils.degToRad(this.camera.fov) / 2;

      this.radius = (enclosing / Math.sin(halfAngle)) * 1.25;
    }

    // LA CÁMARA MIRA LA FILA DE FRENTE Y DESDE ARRIBA, no en diagonal: las figuras se ordenan a lo
    // largo de un eje, y mirarlas en diagonal las apila unas sobre otras en pantalla. Con la vista
    // de frente **se ven las seis separadas**, que es lo que el escenario `E-7` viene a mostrar.
    this.camera.position.set(
      this.focus.x,
      this.focus.y + this.radius * 0.45,
      this.focus.z + this.radius * 0.9,
    );
    this.camera.lookAt(this.focus);
  }

  private centre(): THREE.Vector3 {
    return this.focus;
  }

  private aspect(): number {
    const width = this.element.clientWidth || 1;
    const height = this.element.clientHeight || 1;
    return width / height;
  }

  private start(): void {
    const tick = (): void => {
      this.frame = requestAnimationFrame(tick);

      // LOS DOS MOVIMIENTOS SE DETIENEN MIENTRAS ARRASTRA, y los dos por separado.
      if (this.motion.cameraOrbit && !this.dragging) {
        this.orbitAngle += 0.004;
        const centre = this.centre();
        this.camera.position.set(
          centre.x + Math.cos(this.orbitAngle) * this.radius,
          this.radius * 0.6,
          centre.z + Math.sin(this.orbitAngle) * this.radius,
        );
        this.camera.lookAt(centre);
      }

      if (this.motion.pieceSpin && !this.dragging) {
        for (const mesh of this.pieces.values()) {
          mesh.rotation.y += 0.01;
        }
      }

      this.renderer.render(this.scene, this.camera);
    };

    tick();
  }

  private readonly onPointerDown = (): void => {
    this.dragging = true;
    this.pointerMoved = false;
  };

  /**
   * Soltar sin haber arrastrado ES UNA SELECCIÓN; soltar después de arrastrar es encuadrar.
   *
   * DISTINGUIRLOS IMPORTA: sin esta distinción, cada vez que la persona gira la escena para mirar
   * una figura de atrás, al soltar seleccionaría la que quedó bajo el dedo. La selección dejaría de
   * ser una decisión suya.
   */
  private readonly onPointerUp = (event: PointerEvent): void => {
    const wasDragging = this.dragging;
    this.dragging = false;

    if (!wasDragging || this.pointerMoved || this.onPieceSelected === undefined) {
      return;
    }

    const position = this.pieceAt(event);

    if (position !== null) {
      this.select(position);
      this.onPieceSelected(position);
    }
  };

  /** Qué pieza hay bajo el puntero, o nada. */
  private pieceAt(event: PointerEvent): number | null {
    const bounds = this.renderer.domElement.getBoundingClientRect();

    if (bounds.width === 0 || bounds.height === 0) {
      return null;
    }

    const pointer = new THREE.Vector2(
      ((event.clientX - bounds.left) / bounds.width) * 2 - 1,
      -((event.clientY - bounds.top) / bounds.height) * 2 + 1,
    );

    this.raycaster.setFromCamera(pointer, this.camera);

    const hits = this.raycaster.intersectObjects([...this.pieces.values()], false);

    if (hits.length === 0) {
      return null;
    }

    // LA POSICIÓN VIAJA CON LA MALLA desde que se construyó: acá no se deduce de nada.
    const position = hits[0].object.userData.piecePosition;

    return typeof position === 'number' ? position : null;
  }

  private readonly onPointerMove = (event: PointerEvent): void => {
    if (!this.dragging) {
      return;
    }

    // Un movimiento mínimo ya es arrastre: el umbral existe para que un temblor de la mano al
    // hacer clic no cuente como encuadre.
    if (Math.abs(event.movementX) + Math.abs(event.movementY) > 2) {
      this.pointerMoved = true;
    }

    // Arrastrar orbita la cámara a mano. **Cero peticiones**: es geometría y nada más.
    this.orbitAngle += event.movementX * 0.005;
    const centre = this.centre();
    this.camera.position.set(
      centre.x + Math.cos(this.orbitAngle) * this.radius,
      this.radius * 0.6,
      centre.z + Math.sin(this.orbitAngle) * this.radius,
    );
    this.camera.lookAt(centre);
  };
}
