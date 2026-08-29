using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Values;
using GeometriaFactory.Samples.Domain.Avanzado;
using GeometriaFactory.Samples.Domain.Avanzado.Inspeccion;
using GeometriaFactory.Samples.Domain.Avanzado.Recorrido;

// ============================================================================
// Sample `domain/03-avanzado` — los diez actos, en el orden de §6.
//
// ES EL ARNÉS DEL PROYECTO DE CÓDIGO y no un recorrido más: los actos [9], [10] y
// [11] no ejercitan una capacidad del dominio, la INSPECCIONAN. Cuentan sus
// dependencias, comparan dos corridas con relojes distintos y verifican que las
// doce condiciones provocadas vuelvan por valor.
// ============================================================================

var bitacora = new Bitacora();
var alumna = new Guid("11111111-1111-1111-1111-111111111111");
var otra = new Guid("22222222-2222-2222-2222-222222222222");

// ---- [1] [2] [3] · el trabajo ajeno y el inexistente son indistinguibles ----
var ajeno = Fabrica.Trabajo(otra, "Trabajo de otra alumna", WorkStatus.Draft);
var (deAjeno, deInexistente) = ActoResolverAcceso.Ejecutar(bitacora, ajeno, alumna);
ActoResolverAcceso.CompararCampoPorCampo(bitacora, deAjeno, deInexistente);

// ---- [4] [5] · el alcance del administrador, y el borrador que queda afuera ----
var comision = new List<Work>
{
    Fabrica.Trabajo(alumna, "En borrador", WorkStatus.Draft),
    Fabrica.Trabajo(alumna, "Enviado", WorkStatus.Submitted),
    Fabrica.Trabajo(alumna, "Aprobado", WorkStatus.Approved),
    Fabrica.Trabajo(alumna, "Rechazado", WorkStatus.Rejected),
};
ActoAlcanceDelAdministrador.Ejecutar(bitacora, comision);
ActoAlcanceDelAdministrador.EliminacionAdmitida(bitacora, comision);

// ---- [6] [7] [7b] [7c] · los dos desenlaces y sus dos rechazos ----
ActoDesenlace.Aprobar(bitacora, Fabrica.Trabajo(alumna, "Para aprobar", WorkStatus.Submitted));
ActoDesenlace.Rechazar(bitacora, Fabrica.Trabajo(alumna, "Para rechazar", WorkStatus.Submitted));
ActoDesenlace.SobreTerminal(bitacora, Fabrica.Trabajo(alumna, "Ya resuelto", WorkStatus.Approved));
ActoDesenlace.SinPapel(bitacora, Fabrica.Trabajo(alumna, "Sin papel", WorkStatus.Submitted));

// ---- [8] · el reseteo conserva la cuenta y sus trabajos ----
var cuenta = Account.Register("alumna@frre.utn.edu.ar", "Alumna", "Ejemplo", null, true,
    Role.Student, AccountStatus.Pending, Fabrica.Momento).Value!;
cuenta.Enable("hash-de-la-provisoria");
ActoResetear.Ejecutar(bitacora, cuenta, comision);

// ---- [9] · las dependencias salientes del dominio, leídas de su archivo de proyecto ----
var proyectoDelDominio = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory,
    "../../../../../../src/GeometriaFactory.Domain/GeometriaFactory.Domain.csproj"));
var (salientes, infraestructura) = DependenciasSalientes.Medir(proyectoDelDominio);
bitacora.Escribir(
    $"[9] Dependencias salientes declaradas: {salientes} "
    + $"| Bibliotecas de persistencia o transporte: {infraestructura}");

// ---- [10] · dos corridas con relojes distintos dan lo mismo ----
bitacora.Escribir(
    $"[10] Dos corridas consecutivas sin fijar el reloj: "
    + $"resultado-identico={(SinRelojNiConjunto.ResultadoIdentico() ? "si" : "no")}");

// ---- [11] · el recuento que convierte al sample en arnés ----
CondicionesTipadas.Provocar(bitacora, alumna);
bitacora.Cerrar();

return args.Contains("--verificar", StringComparer.Ordinal)
    ? SalidaEsperada.Comparar(bitacora.Lineas)
    : 0;
