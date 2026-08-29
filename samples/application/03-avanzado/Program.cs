using GeometriaFactory.Application.Accounts;
using GeometriaFactory.Application.Works;
using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Values;
using GeometriaFactory.Samples.Application.Avanzado;
using GeometriaFactory.Samples.Application.Avanzado.Actos;
using GeometriaFactory.Samples.Application.Avanzado.Dobles;
using GeometriaFactory.Samples.Application.Avanzado.Semilla;

// ============================================================================
// Sample `application/03-avanzado` — los cuatro actos del administrador, contra
// dobles de los puertos y sobre una comisión sembrada con los cuatro estados.
// ============================================================================

var bitacora = new Bitacora();
var cuentas = new RepositorioDeCuentasEnMemoria();
var trabajos = new RepositorioDeTrabajosEnMemoria();
var reloj = new RelojFijo();

var comision = await ComisionDeEjemplo.SembrarAsync(cuentas, trabajos);

async Task<int> ContarTrabajos(Guid dueño) =>
    (await trabajos.ListOwnedByAsync(dueño)).Count;

// LOS TRABAJOS EXTRA DEL ACTO [3] SE AGREGAN DESPUÉS DEL [2], y no antes: el acto [2] cuenta
// cuántos ve el administrador, y sumarlos a la semilla movería ese número. La primera versión de
// este sample los agregaba al principio y el listado daba cuatro donde el contrato pide tres.
Work Pendiente(string nombre)
{
    var w = Work.Create(comision.Alumna.Id, nombre, "2026-08-29", null, "[]", true, RelojFijo.Momento).Value!;
    w.Submit(true, false, RelojFijo.Momento);
    return w;
}

var situacionAntes = comision.AlumnaBloqueadaParaReseteo.Status;
var trabajosAntes = await ContarTrabajos(comision.AlumnaBloqueadaParaReseteo.Id);
var bloqueada = comision.AlumnaBloqueadaParaReseteo.Id;

// [1] El gobierno de las cuentas.
await ActoGobiernoDeCuentas.EjecutarAsync(bitacora,
    new GovernCommissionAccountsUseCase(cuentas), comision, ContarTrabajos);

// [2] La revisión de la comisión.
await ActoRevisarLaComision.EjecutarAsync(bitacora,
    new ReviewCommissionWorksUseCase(trabajos, cuentas), comision);

// [3] Los desenlaces. Dos pendientes más: uno para rechazar y otro para el pedido del alumno.
var paraRechazar = Pendiente("Para rechazar");
var paraElAlumno = Pendiente("Para el pedido del alumno");
await trabajos.AddAsync(paraRechazar);
await trabajos.AddAsync(paraElAlumno);

await ActoDesenlace.EjecutarAsync(bitacora,
    new ResolveWorkUseCase(trabajos, reloj), comision, paraRechazar.Id, paraElAlumno.Id);

// [4] El reseteo, sobre la cuenta bloqueada.
await ActoReseteo.EjecutarAsync(bitacora, new ResetStudentPasswordUseCase(cuentas), comision,
    bloqueada, situacionAntes, trabajosAntes, ContarTrabajos);

return args.Contains("--verificar", StringComparer.Ordinal)
    ? SalidaEsperada.Comparar(bitacora.Lineas)
    : 0;
