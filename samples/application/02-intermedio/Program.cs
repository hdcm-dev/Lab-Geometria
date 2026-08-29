using GeometriaFactory.Application.Works;
using GeometriaFactory.Domain.Values;
using GeometriaFactory.Samples.Application.Intermedio;
using GeometriaFactory.Samples.Application.Intermedio.Actos;
using GeometriaFactory.Samples.Application.Intermedio.Dobles;
using GeometriaFactory.Samples.Application.Intermedio.Escenarios;

// ============================================================================
// Sample `application/02-intermedio` — los ocho escenarios del intake §20, contra
// DOBLES DE LOS PUERTOS.
//
// LA CARGA INTERPRETA Y ENVÍA EN EL MISMO ACTO, y por eso cada línea del snapshot
// junta las dos mitades: `LoadAsync` pide la interpretación al puerto y con lo que
// vuelve resuelve el envío.
// ============================================================================

var bitacora = new Bitacora();

var otra = new Guid("22222222-2222-2222-2222-222222222222");

var trabajos = new RepositorioDeTrabajosEnMemoria();
var reloj = new RelojFijo();
var validador = new ValidadorDeFigurasDeclarado();

string Texto(string e) => File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Escenarios", $"{e}.txt"));

// El doble se carga con el resultado declarado de cada texto, antes de empezar.
foreach (var (clave, resultado) in new[]
{
    ("E1", ResultadosDeclarados.E1()), ("E2", ResultadosDeclarados.E2()),
    ("E3", ResultadosDeclarados.E3()), ("E4", ResultadosDeclarados.E4()),
    ("E5", ResultadosDeclarados.E5()), ("E6", ResultadosDeclarados.E6()),
    ("E7", ResultadosDeclarados.E7()), ("E8", ResultadosDeclarados.E8()),
})
{
    validador.Declarar(Texto(clave), resultado);
}

var cuentas = new RepositorioDeCuentasEnMemoria();
// El detalle pone el correo y el nombre de la persona dueña, y para eso el puerto de cuentas
// tiene que poder encontrarla. La cuenta se constituye por el dominio y no se fabrica a mano.
var cuentaDeLaAlumna = GeometriaFactory.Domain.Entities.Account.Register(
    "alumna@frre.utn.edu.ar", "Alumna", "Ejemplo", null, true,
    Role.Student, AccountStatus.Pending, RelojFijo.Momento).Value!;
cuentas.Agregar(cuentaDeLaAlumna);
var alumna = cuentaDeLaAlumna.Id;

var carga = new LoadAndEditOwnWorkUseCase(trabajos, reloj, validador);
var consulta = new ConsultOwnWorksUseCase(trabajos, cuentas);
var retiro = new DeleteWorkUseCase(trabajos);

// ---- E-1 ----
var e1 = await ActoCargarYReeditar.CargarAsync(bitacora, carga, alumna, "E-1", Texto("E1"));
var t1 = (await trabajos.FindByIdAsync(e1.WorkId))!;
bitacora.Escribir(
    $"[E-1] Cargado: texto-identico={(t1.OriginalJson == Texto("E1") ? "si" : "no")} estado=Borrador "
    + $"| Envio: {t1.Pieces.Count} piezas, {ActoEnviar.Advertencias(e1)} advertencias, "
    + $"{ActoEnviar.Errores(e1)} errores -> {Vocabulario.De(e1.Status)}");

// ---- E-2 ----
var e2 = await ActoCargarYReeditar.CargarAsync(bitacora, carga, alumna, "E-2", Texto("E2"));
bitacora.Escribir(
    $"[E-2] Envio: 1 pieza, {ActoEnviar.Advertencias(e2)} advertencia de volumen, "
    + $"{ActoEnviar.Errores(e2)} errores -> {Vocabulario.De(e2.Status)}");

// ---- E-3 y E-4 · el mismo cubo con dos áreas declaradas ----
var e3 = await ActoCargarYReeditar.CargarAsync(bitacora, carga, alumna, "E-3", Texto("E3"));
bitacora.Escribir($"[E-3] Envio: {ActoEnviar.Advertencia(e3)} -> {Vocabulario.De(e3.Status)}");

var e4 = await ActoCargarYReeditar.CargarAsync(bitacora, carga, alumna, "E-4", Texto("E4"));
bitacora.Escribir(
    $"[E-4] Envio: {e4.Observations?.Count ?? 0} observaciones -> {Vocabulario.De(e4.Status)} "
    + "(mismo cubo de lado 3, area declarada coincidente)");

// ---- E-5 · el tipo desconocido retiene el trabajo ----
var e5 = await ActoCargarYReeditar.CargarAsync(bitacora, carga, alumna, "E-5", Texto("E5"));
bitacora.Escribir($"[E-5] Envio: {ActoEnviar.Error(e5)} -> {Vocabulario.De(e5.Status)} (RN-04005)");

// ---- E-6 · el cero es un valor ----
var e6 = await ActoCargarYReeditar.CargarAsync(bitacora, carga, alumna, "E-6", Texto("E6"));
bitacora.Escribir(
    $"[E-6] Envio: la figura se {(e6.Status == WorkStatus.Submitted ? "interpreta y no se descarta" : "descarta")} "
    + $"-> {Vocabulario.De(e6.Status)}");

// ---- E-7 · el detalle contra el listado ----
var e7 = await ActoCargarYReeditar.CargarAsync(bitacora, carga, alumna, "E-7", Texto("E7"));
await ActoConsultarLoPropio.DetalleContraListadoAsync(bitacora, consulta, alumna, e7.WorkId);

// ---- E-8 · el error localizado ----
var e8 = await ActoCargarYReeditar.CargarAsync(bitacora, carga, alumna, "E-8", Texto("E8"));
bitacora.Escribir($"[E-8] Envio: {ActoEnviar.Error(e8)} -> {Vocabulario.De(e8.Status)} (RN-04005)");

// ---- El listado propio, con los ocho ----
await ActoConsultarLoPropio.ListadoPropioAsync(bitacora, consulta, alumna);

// ---- Los tres retiros ----
await ActoEliminar.EnBorradorAsync(bitacora, retiro, alumna, e5.WorkId);
await ActoEliminar.EnPendienteAsync(bitacora, retiro, alumna, e1.WorkId);
await ActoEliminar.AjenoAsync(bitacora, retiro, otra, e2.WorkId);

// ---- La reedición fuera de Borrador, con el texto intacto ----
await ActoCargarYReeditar.ReeditarFueraDeBorradorAsync(bitacora, carga, alumna, e1.WorkId, Texto("E1"),
    async id => (await trabajos.FindByIdAsync(id))!.OriginalJson);

bitacora.Cerrar();

return args.Contains("--verificar", StringComparer.Ordinal)
    ? SalidaEsperada.Comparar(bitacora.Lineas)
    : 0;
