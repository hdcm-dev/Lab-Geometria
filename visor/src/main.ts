/**
 * Fachada externa del visor. Es la ÚNICA superficie que el front consume.
 *
 * Las seis funciones son las que `Norma-De-Nomenclatura.md` §5.1 y §6.6 fijaron el 2026-08-12:
 * `initialize`, `loadJson`, `selectPiece`, `resize`, `destroy` y `setMotion`. El intake
 * §17.7.P.3 las dejaba «a fijar en la etapa que la implementa», de modo que nunca estuvieron
 * fijadas antes de esa decisión.
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

export function loadJson(id: string, text: string): void {
  void id;
  void text;
  announce('loadJson');
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
