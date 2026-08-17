/**
 * Fachada externa del visor. Es la ÚNICA superficie que el front consume.
 *
 * Las seis funciones son las que `Norma-De-Nomenclatura.md` §5.1 y §6.6 fijaron el 2026-08-12:
 * `initialize`, `loadPieces`, `selectPiece`, `resize`, `destroy` y `setMotion`.
 *
 * `loadPieces` SE LLAMABA `loadJson` Y RECIBÍA EL TEXTO DEL ALUMNO. Cambió el 2026-08-16 por
 * `ADR-08006`: el visor recibe **las piezas ya reconstruidas**. El nombre cambió junto con la
 * firma, porque una función que se llama «cargar JSON» y recibe otra cosa promete lo que no cumple.
 *
 * ETAPA `g`: **la capa 3 existe**. `src/viewer/` construye las mallas y gobierna la escena; este
 * archivo es la frontera y no dibuja: valida sus argumentos, resuelve la instancia y delega.
 *
 * ESTE BUNDLE NO HACE RED, NO TIENE IDENTIDAD Y NO LEE CONFIGURACIÓN (`RA-02`). Se puede comprobar
 * sin leerlo entero: **no importa ningún cliente, no conoce ninguna dirección y todo lo que dibuja
 * se lo dan por parámetro**. La única dependencia del paquete es el motor gráfico, que entra
 * empaquetado y no por red de distribución (`PT-03`).
 *
 * LOS ERRORES SE DEVUELVEN Y NO SE LANZAN. Un anfitrión que pasa un identificador viejo tiene que
 * poder seguir: lanzar dejaría la página del alumno rota por un error de coordinación entre dos
 * piezas del producto.
 */

import type { DrawOutcome, MotionOptions, Piece, ViewerOptions } from './contract';
import { ViewerInstance } from './viewer/instance';

export type { DrawOutcome, MotionOptions, Piece, PieceComponent, UndrawnPiece, ViewerOptions } from './contract';

const UNKNOWN_INSTANCE = 'UNKNOWN_INSTANCE';
const INVALID_CANVAS_ELEMENT = 'INVALID_CANVAS_ELEMENT';
const GRAPHICS_CAPABILITY_MISSING = 'GRAPHICS_CAPABILITY_MISSING';
const INDEX_OUT_OF_RANGE = 'INDEX_OUT_OF_RANGE';

const instances = new Map<string, ViewerInstance>();
let nextInstance = 0;

function report(functionName: string, code: string): void {
  // Castellano para la persona, código para quien programa (`Norma-De-Nomenclatura.md` §4).
  console.warn(`[visor] ${functionName}: ${code}`);
}

/**
 * Crea una instancia viva sobre el elemento, y **no dibuja ninguna pieza hasta que se las den**.
 */
export function initialize(element: HTMLElement, options?: ViewerOptions): string {
  if (element === null || element === undefined || typeof element.appendChild !== 'function') {
    report('initialize', INVALID_CANVAS_ELEMENT);
    return '';
  }

  try {
    const id = `visor-${++nextInstance}`;
    instances.set(id, new ViewerInstance(element, options));
    return id;
  } catch {
    // Sin capacidad gráfica tridimensional no hay escena, y **se dice**: la alternativa es un
    // recuadro vacío que la persona interpreta como que su trabajo no tiene figuras.
    report('initialize', GRAPHICS_CAPABILITY_MISSING);
    return '';
  }
}

/**
 * Reemplaza por completo lo dibujado con las piezas recibidas.
 *
 * NO PIDE LAS PIEZAS POR SU CUENTA, no valida el trabajo, no emite observaciones, no recalcula
 * valores y **no interpreta el texto del alumno, que ya no recibe**.
 *
 * DEVUELVE LAS DIBUJADAS **Y LAS NO DIBUJADAS CON SU MOTIVO**: ninguna pieza desaparece sin quedar
 * enumerada, que es el fallo silencioso que este visor existe para eliminar.
 */
export function loadPieces(id: string, pieces: readonly Piece[]): DrawOutcome {
  const instance = instances.get(id);

  if (instance === undefined) {
    report('loadPieces', UNKNOWN_INSTANCE);
    return { drawn: [], undrawn: [] };
  }

  return instance.load(pieces ?? []);
}

export function selectPiece(id: string, index: number): void {
  const instance = instances.get(id);

  if (instance === undefined) {
    report('selectPiece', UNKNOWN_INSTANCE);
    return;
  }

  if (!instance.select(index)) {
    report('selectPiece', INDEX_OUT_OF_RANGE);
  }
}

export function resize(id: string): void {
  const instance = instances.get(id);

  if (instance === undefined) {
    report('resize', UNKNOWN_INSTANCE);
    return;
  }

  instance.resize();
}

/** Libera la instancia entera. Llamarla dos veces sobre la misma no es un error. */
export function destroy(id: string): void {
  const instance = instances.get(id);

  if (instance === undefined) {
    report('destroy', UNKNOWN_INSTANCE);
    return;
  }

  instance.dispose();
  instances.delete(id);
}

export function setMotion(id: string, options: MotionOptions): void {
  const instance = instances.get(id);

  if (instance === undefined) {
    report('setMotion', UNKNOWN_INSTANCE);
    return;
  }

  instance.setMotion(options);
}

/**
 * Cuántas instancias hay vivas. **Es instrumento de medición de `PT-02`** y no superficie del
 * producto: el front no la usa, y por eso no es una séptima función de la fachada.
 */
export function liveInstanceCount(): number {
  return instances.size;
}
