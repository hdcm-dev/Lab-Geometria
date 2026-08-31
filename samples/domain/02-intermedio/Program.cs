using GeometriaFactory.Domain.Values;
using GeometriaFactory.Samples.Domain.Intermedio;
using GeometriaFactory.Samples.Domain.Intermedio.Escenarios;
using GeometriaFactory.Samples.Domain.Intermedio.Recorrido;

// ============================================================================
// Sample `domain/02-intermedio` — los seis escenarios del `PRODUCT-INTAKE` §20 que
// este proyecto de código ejercita, en el orden que declara §6 del documento que
// gobierna esta carpeta.
//
// EL ORDEN NO ES EL NUMÉRICO Y ES A PROPÓSITO: `E-6` va antes que `E-5` porque el
// snapshot los enfrenta así — el caso del cero, que se adopta, contra el caso del
// tipo desconocido, que se rechaza. Cambiar el orden rompería el contrato.
// ============================================================================

var bitacora = new Bitacora();
var alumna = new Guid("11111111-1111-1111-1111-111111111111");
// El dominio NO lee el reloj (`ADR-02006`): el momento lo aporta el consumidor.
var momento = new DateTimeOffset(2026, 8, 29, 0, 0, 0, TimeSpan.Zero);

string Texto(string escenario) =>
    File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Escenarios", $"{escenario}.txt"));

// ---- E-1 · tres figuras, dos advertencias, ningún error ----
var e1 = ActoConstituirTrabajo.Ejecutar(bitacora, alumna, "E-1", Texto("E1"), momento, anunciar: true);
ActoAdoptarPiezas.Ejecutar(bitacora, e1, Interpretacion.E1(), momento);
bitacora.Escribir(
    $"[E-1] Piezas adoptadas: {e1.Pieces.Count} | Observaciones adoptadas: {e1.Observations.Count} "
    + $"| Errores de validacion: {ActoAdoptarObservaciones.ErroresDeValidacion(e1)}");
ActoEnviar.Ejecutar(bitacora, e1, momento);
bitacora.Escribir($"[E-1] Envio: estado={Vocabulario.De(e1.Status)} (las advertencias no impiden el envio)");

// ---- E-3 y E-4 · el mismo cubo de lado 3, con dos áreas declaradas distintas ----
var e3 = ActoConstituirTrabajo.Ejecutar(bitacora, alumna, "E-3", Texto("E3"), momento, anunciar: false);
ActoAdoptarPiezas.Ejecutar(bitacora, e3, Interpretacion.E3(), momento);
ActoAdoptarObservaciones.DeclararAdvertencia(bitacora, e3, "E-3");
ActoEnviar.Ejecutar(bitacora, e3, momento);

var e4 = ActoConstituirTrabajo.Ejecutar(bitacora, alumna, "E-4", Texto("E4"), momento, anunciar: false);
ActoAdoptarPiezas.Ejecutar(bitacora, e4, Interpretacion.E4(), momento);
bitacora.Escribir(
    $"[E-4] Observaciones adoptadas: {e4.Observations.Count} "
    + "(mismo cubo de lado 3, area declarada coincidente)");
ActoEnviar.Ejecutar(bitacora, e4, momento);

// ---- E-6 · el cero es un valor y no una ausencia ----
var e6 = ActoConstituirTrabajo.Ejecutar(bitacora, alumna, "E-6", Texto("E6"), momento, anunciar: false);
ActoAdoptarPiezas.Ejecutar(bitacora, e6, Interpretacion.E6(), momento);
ActoEnviar.Ejecutar(bitacora, e6, momento);
bitacora.Escribir(
    $"[E-6] Piezas adoptadas: {e6.Pieces.Count} | Envio: estado={Vocabulario.De(e6.Status)} "
    + "(el cero es un valor, no una ausencia)");

// ---- E-5 · tipo desconocido: la posición queda reservada y el envío se retiene ----
var e5 = ActoConstituirTrabajo.Ejecutar(bitacora, alumna, "E-5", Texto("E5"), momento, anunciar: false);
ActoAdoptarPiezas.Ejecutar(bitacora, e5, Interpretacion.E5(), momento);
bitacora.Escribir(
    $"[E-5] Pieza del indice {e5.Pieces[0].Position} adoptada "
    + "| Pieza del indice 1 rechazada: TIPO_DE_PIEZA_DESCONOCIDO");
var reservada = e5.Observations.Any(o => o.Kind == ObservationKind.ValidationError && o.PiecePosition == 1);
bitacora.Escribir(
    $"[E-5] Posicion 1 reservada: observacion de error {(reservada ? "aceptada" : "rechazada")} sobre esa posicion");
ActoAdoptarObservaciones.DeclararError(bitacora, e5, "E-5", "Observacion de error");
ActoEnviar.Ejecutar(bitacora, e5, momento);
bitacora.Escribir(
    $"[E-5] Envio: estado={Vocabulario.De(e5.Status)} (RN-02005: un error de validacion retiene el trabajo)");

// ---- E-8 · el número entre comillas y con coma: el error se localiza ----
var textoE8 = Texto("E8");
var e8 = ActoConstituirTrabajo.Ejecutar(bitacora, alumna, "E-8", textoE8, momento, anunciar: false);
ActoAdoptarPiezas.Ejecutar(bitacora, e8, Interpretacion.E8(), momento);
ActoAdoptarObservaciones.DeclararError(bitacora, e8, "E-8", "Observacion de error localizada");
ActoEnviar.Ejecutar(bitacora, e8, momento);
var intacto = string.Equals(e8.OriginalJson, textoE8, StringComparison.Ordinal);
bitacora.Escribir(
    $"[E-8] Envio: estado={Vocabulario.De(e8.Status)} | texto-original-intacto={(intacto ? "si" : "no")}");

bitacora.Cerrar();

// LA COMPARACIÓN CORRE SIEMPRE, Y ANTES ERA OPCIONAL.
//
// Hasta el 2026-08-30 estaba detrás de `--verificar`, y el comando que el §4 del documento declara
// —y que el contrato de verificación de su §9 cita— **no pasa esa bandera**. Corrido como está
// documentado, el sample imprimía sus renglones y devolvía **cero sin comparar nada**.
//
// ERA UN INSTRUMENTO QUE SE LEÍA COMO VERDE SIN HABER VERIFICADO, que es el defecto que estos
// samples existen para encontrar en otros lados. Se midió sobre los nueve de .NET: **siete estaban
// así**. Quien sólo quiera ver la salida la sigue viendo; lo que se agrega es el veredicto.
return SalidaEsperada.Comparar(bitacora.Lineas);
