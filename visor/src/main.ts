/**
 * Fachada externa del visor. Es la ÚNICA superficie que el front consume.
 *
 * Las seis funciones son las que `Norma-De-Nomenclatura.md` §5.1 y §6.6 fijaron el 2026-08-12:
 * `initialize`, `loadPieces`, `selectPiece`, `resize`, `destroy` y `setMotion`. El intake
 * §17.7.P.3 las dejaba «a fijar en la etapa que la implementa», de modo que nunca estuvieron
 * fijadas antes de esa decisión.
 *
 * `loadPieces` SE LLAMABA `loadJson` Y RECIBÍA EL TEXTO DEL ALUMNO. Cambió el 2026-08-16 por
 * `ADR-08006`: el visor recibe **las piezas ya reconstruidas** y no el texto. El nombre cambió
 * junto con la firma, porque una función que se llama «cargar JSON» y recibe otra cosa es un
 * nombre que promete lo que no cumple.
 *
 * LO QUE ESO SACA DE ACÁ, Y ES LA MITAD QUE MÁS PESABA: la tolerancia del formato. Las cuatro
 * trampas del texto del alumno —la clave sinónima del ortoedro, las comas finales, la cara del
 * cubo con dos nombres y los valores calculados erróneos— las resuelve el validador del
 * laboratorio, con su batería obligatoria de diez casos. Este bundle **no las ve**: recibe piezas
 * con su tipo, sus dimensiones y sus componentes, y su trabajo es dibujarlas y rotarlas.
 *
 * LO QUE NO CAMBIA: este bundle NO HACE RED, no tiene identidad y no lee configuración propia
 * (`RA-02`). Las piezas se las da su componente anfitrión, que es lo que siempre hizo.
 *
 * ETAPA `a`: la fachada existe y NO CONTIENE LÓGICA DE DIBUJO (intake §17.7.P.2 y §17.7.P.3).
 * La capa 3 —`src/viewer/`— está vacía y llega en la etapa `g`, con `PT-02` y `PT-03` medidas
 * antes de comprometerla. Cada función registra su llamada y no hace nada más: es lo que
 * significa «vacío pero real».
 */

export interface ViewerOptions {
  readonly background?: string;
}

export interface MotionOptions {
  readonly enabled: boolean;
}

/**
 * Un componente de una pieza, con las dimensiones desde las que se construye la malla.
 *
 * Las tres dimensiones son opcionales porque LA AUSENCIA ES UN DATO: un componente que no trae
 * radio no es uno con radio cero. Y un `0` presente es una dimensión legible: la figura no se
 * descarta por tenerlo.
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

/**
 * Una pieza reconstruida, tal como el anfitrión se la entrega a `loadPieces`.
 *
 * LA POSICIÓN ES LA IDENTIDAD y no se recalcula: el conjunto **admite huecos**, porque una figura
 * que el laboratorio no pudo reconstruir no llega acá y su posición **no la ocupa la siguiente**.
 * Es el número con el que `selectPiece` selecciona y con el que el anfitrión resalta.
 *
 * LOS VALORES DECLARADO Y DERIVADO VIAJAN Y ESTE BUNDLE NO LOS USA para dibujar ni los juzga: son
 * del alumno y de quien arma la vista.
 */
export interface Piece {
  readonly position: number;
  readonly type: string;
  readonly declaredArea?: number | null;
  readonly derivedArea?: number | null;
  readonly declaredVolume?: number | null;
  readonly derivedVolume?: number | null;
  readonly components: readonly PieceComponent[];
}

const NOT_IMPLEMENTED_YET = 'Etapa `a`: la fachada del visor existe y la capa 3 llega en la etapa `g`.';

function announce(functionName: string): void {
  // Castellano: el mensaje lo lee una persona (Norma-De-Nomenclatura.md §4).
  console.info(`[visor] ${functionName}: ${NOT_IMPLEMENTED_YET}`);
}

export function initialize(element: HTMLElement, options?: ViewerOptions): string {
  void element;
  void options;
  announce('initialize');
  return '';
}

/**
 * Reemplaza por completo lo dibujado en la instancia con las piezas recibidas.
 *
 * NO PIDE LAS PIEZAS POR SU CUENTA (G-1), no valida el trabajo, no emite observaciones, no
 * recalcula valores y **no interpreta el texto del alumno, que ya no recibe**.
 */
export function loadPieces(id: string, pieces: readonly Piece[]): void {
  void id;
  void pieces;
  announce('loadPieces');
}

export function selectPiece(id: string, index: number): void {
  void id;
  void index;
  announce('selectPiece');
}

export function resize(id: string): void {
  void id;
  announce('resize');
}

export function destroy(id: string): void {
  void id;
  announce('destroy');
}

export function setMotion(id: string, options: MotionOptions): void {
  void id;
  void options;
  announce('setMotion');
}
