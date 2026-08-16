/**
 * Los tipos que cruzan la frontera hacia el visor. **Es lo único que este bundle sabe del dato.**
 *
 * Las piezas llegan **ya reconstruidas** por el laboratorio (`ADR-08006`): este archivo no describe
 * el texto del alumno, describe lo que su anfitrión le entrega. No hay claves sinónimas acá, y no
 * las hay porque no llegan.
 */

export interface PieceComponent {
  readonly position: number;
  readonly role: string;
  readonly type: string;
  readonly declaredLength?: number | null;
  readonly declaredWidth?: number | null;
  readonly declaredRadius?: number | null;
  readonly declaredArea?: number | null;
}

export interface Piece {
  readonly position: number;
  readonly type: string;
  readonly declaredArea?: number | null;
  readonly derivedArea?: number | null;
  readonly declaredVolume?: number | null;
  readonly derivedVolume?: number | null;
  readonly components: readonly PieceComponent[];
}

/** Una pieza que no se pudo dibujar, con su posición y su motivo. */
export interface UndrawnPiece {
  readonly position: number;
  readonly reason: string;
}

/**
 * Lo que `loadPieces` devuelve.
 *
 * **NINGUNA PIEZA DESAPARECE SIN QUEDAR ENUMERADA**, que es la garantía que este visor existe para
 * cumplir: el visualizador previo fallaba en silencio y la persona veía dos figuras donde había
 * pegado tres, sin saber por qué.
 */
export interface DrawOutcome {
  readonly drawn: readonly number[];
  readonly undrawn: readonly UndrawnPiece[];
}

export interface ViewerOptions {
  readonly background?: string;

  /**
   * Aviso de que la persona eligió una pieza **en la escena**, con su posición.
   *
   * ES LA ÚNICA VÍA DEL VISOR HACIA SU ANFITRIÓN, y entra por `ADR-08007`. Sin ella `F-13` sólo
   * podía cumplirse en una dirección: las seis funciones de la fachada van todas del anfitrión
   * hacia el visor, de modo que una selección hecha en la escena **no tenía por dónde enterarse**.
   *
   * VA EN LAS OPCIONES Y NO COMO SÉPTIMA FUNCIÓN, y es deliberado: las seis las fijó el Product
   * Owner y su recuento está citado en cinco documentos. Las opciones ya son el lugar donde el
   * anfitrión configura su instancia.
   *
   * EL VISOR NO GUARDA LA SELECCIÓN NI DECIDE QUÉ HACER CON ELLA: avisa y resalta. Qué se marca en
   * el árbol es del anfitrión.
   */
  readonly onPieceSelected?: (position: number) => void;
}

/**
 * Los dos movimientos automáticos, **gobernados por separado** (`F-25`).
 *
 * El estado inicial lo fija la pieza pública, que es la que consulta la preferencia de movimiento
 * reducido del sistema: este bundle **no la consulta y no la conserva**, la recibe y la ejerce.
 */
export interface MotionOptions {
  readonly cameraOrbit: boolean;
  readonly pieceSpin: boolean;
}
