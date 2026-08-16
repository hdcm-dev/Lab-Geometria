import * as THREE from 'three';

import type { Piece, PieceComponent } from '../contract';

/**
 * Construcción de la malla de cada pieza, a partir de sus dimensiones declaradas.
 *
 * ACÁ NO SE INTERPRETA NADA. Las piezas llegan reconstruidas por el laboratorio (`ADR-08006`): lo
 * que este archivo hace es elegir la geometría del tipo y sacar sus medidas de los campos que la
 * pieza ya trae. No hay claves sinónimas que resolver, no hay comas finales que tolerar y no hay
 * valores que recalcular.
 *
 * DE DÓNDE SALE CADA MEDIDA, que es lo único que este archivo decide. El emisor no manda una
 * «altura»: manda componentes, y la medida está en ellos. Cada regla de abajo cita el escenario del
 * intake que la fija, para que ninguna sea una conjetura:
 *
 * - **Cubo**: la arista es el `Largo` de una cara (`E-3`, cara de 3.00 → cubo de 3).
 * - **Ortoedro**: ancho y profundidad son el `Largo` y el `Ancho` de una base; la altura es **la
 *   dimensión del lateral que no es un lado de la base** (`E-1` la tiene en `Largo` y `E-7` en
 *   `Ancho`, y es la única regla que satisface a las dos).
 * - **Cilindro**: el radio es el de una tapa y la altura es el `Largo` del rectángulo desarrollado
 *   —su `Ancho` es `2πr`, la circunferencia— (`E-1`, `Lado` de `Largo` 3.00 sobre radio 3).
 * - **Planas**: se dibujan con sus propias dimensiones y sin espesor.
 *
 * UN CERO ES UNA MEDIDA LEGÍBLE Y NO UN FALTANTE (`E-6`). La malla degenerada que produce **es el
 * dibujo correcto** de lo que el alumno escribió: no verla en la escena no es una falla del visor.
 */

/** Lo mínimo que una medida puede valer sin que la malla deje de existir. */
const MINIMUM_EXTENT = 0.0001;

export interface MeshOutcome {
  readonly mesh: THREE.Mesh | null;
  readonly reason: string | null;
}

const UNREADABLE_DIMENSION = 'UNREADABLE_DIMENSION';
const NON_DRAWABLE_TYPE = 'NON_DRAWABLE_TYPE';

function componentWithRole(piece: Piece, role: string): PieceComponent | undefined {
  return piece.components.find((component) => component.role === role);
}

function isUsable(value: number | null | undefined): value is number {
  return typeof value === 'number' && Number.isFinite(value) && value >= 0;
}

/** La dimensión del lateral que no es un lado de la base. Ver el encabezado. */
function heightFromLateral(
  lateral: PieceComponent | undefined,
  baseLength: number,
  baseWidth: number,
): number | null {
  if (lateral === undefined) {
    return null;
  }

  const matchesBaseSide = (value: number | null | undefined): boolean =>
    isUsable(value) && (Math.abs(value - baseLength) <= 0.01 || Math.abs(value - baseWidth) <= 0.01);

  if (matchesBaseSide(lateral.declaredLength) && isUsable(lateral.declaredWidth)) {
    return lateral.declaredWidth;
  }

  if (isUsable(lateral.declaredLength)) {
    return lateral.declaredLength;
  }

  return isUsable(lateral.declaredWidth) ? lateral.declaredWidth : null;
}

function geometryOf(piece: Piece): THREE.BufferGeometry | null {
  const extent = (value: number): number => Math.max(value, MINIMUM_EXTENT);

  switch (piece.type) {
    case 'Cube': {
      const face = componentWithRole(piece, 'Face');
      const edge = face?.declaredLength;
      return isUsable(edge) ? new THREE.BoxGeometry(extent(edge), extent(edge), extent(edge)) : null;
    }

    case 'Orthohedron': {
      const base = componentWithRole(piece, 'Base');
      const lateral = componentWithRole(piece, 'Lateral');

      if (!isUsable(base?.declaredLength) || !isUsable(base?.declaredWidth)) {
        return null;
      }

      const height = heightFromLateral(lateral, base.declaredLength, base.declaredWidth);

      return height === null
        ? null
        : new THREE.BoxGeometry(
            extent(base.declaredLength),
            extent(height),
            extent(base.declaredWidth),
          );
    }

    case 'Cylinder': {
      const cap = componentWithRole(piece, 'Cap');
      const side = componentWithRole(piece, 'Side');
      const radius = cap?.declaredRadius;
      const height = side?.declaredLength;

      return isUsable(radius) && isUsable(height)
        ? new THREE.CylinderGeometry(extent(radius), extent(radius), extent(height), 48)
        : null;
    }

    case 'Rectangle':
    case 'Square': {
      const width = piece.components[0]?.declaredLength ?? null;
      const depth = piece.components[0]?.declaredWidth ?? null;
      return null !== width && isUsable(width) && isUsable(depth)
        ? new THREE.PlaneGeometry(extent(width), extent(depth))
        : null;
    }

    case 'Circle': {
      const radius = piece.components[0]?.declaredRadius ?? null;
      return isUsable(radius) ? new THREE.CircleGeometry(extent(radius), 48) : null;
    }

    default:
      return null;
  }
}

/**
 * Las figuras planas del conjunto raíz no traen componentes: sus medidas son suyas.
 *
 * **[decisión de la etapa `g`, declarada.]** `E-7` las transcribe con `Largo`, `Ancho` y `Radio`
 * **en la propia figura** —`{ "Tipo": "Circulo", "Radio": 2.50 }`—, y la pieza reconstruida no
 * lleva esas tres columnas porque el modelo del dominio no se las asigna: las lleva su componente.
 * Para que una figura plana suelta se pueda dibujar, el anfitrión le entrega **un componente con
 * sus propias medidas**, y es lo que este archivo lee arriba.
 */
export function meshFor(piece: Piece): MeshOutcome {
  const drawable = ['Cube', 'Orthohedron', 'Cylinder', 'Rectangle', 'Square', 'Circle'];

  if (!drawable.includes(piece.type)) {
    // `TIPO_NO_DIBUJABLE`: el laboratorio pudo reconstruirla y este visor no sabe dibujarla.
    return { mesh: null, reason: NON_DRAWABLE_TYPE };
  }

  const geometry = geometryOf(piece);

  if (geometry === null) {
    // `DIMENSION_NO_LEGIBLE`: la pieza llegó sin la medida desde la que se construye su malla.
    return { mesh: null, reason: UNREADABLE_DIMENSION };
  }

  const material = new THREE.MeshStandardMaterial({
    color: colorFor(piece.position),
    metalness: 0.05,
    roughness: 0.65,
    side: THREE.DoubleSide,
  });

  const mesh = new THREE.Mesh(geometry, material);

  // LA POSICIÓN DE LA PIEZA VIAJA CON SU MALLA: es su identidad, y es con lo que `selectPiece`
  // la encuentra y con lo que el anfitrión la resalta.
  mesh.userData.piecePosition = piece.position;

  return { mesh, reason: null };
}

/**
 * El color de una pieza **se deriva de su posición** y de nada más.
 *
 * ES LO QUE HACE LA DISPOSICIÓN DETERMINISTA (`F-13`): procesar el mismo trabajo dos veces produce
 * la misma escena, porque nada acá depende del orden de llegada, del reloj ni del azar.
 */
function colorFor(position: number): THREE.Color {
  const hue = (position * 0.137) % 1;
  return new THREE.Color().setHSL(hue, 0.55, 0.55);
}
