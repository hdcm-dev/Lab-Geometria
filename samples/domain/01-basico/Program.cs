using GeometriaFactory.Samples.Domain.Basico;
using GeometriaFactory.Samples.Domain.Basico.Recorrido;

// ============================================================================
// Sample `domain/01-basico` — los cuatro actos del recorrido.
//
// Gobernado por `ejemplo-01-basico-dominio.md`: su §5 declara este árbol y su §6 la
// salida exacta, LAS DOS ESCRITAS ANTES QUE ESTE CÓDIGO. Cuando la salida y el
// documento no coinciden, el que manda es el documento y lo que se corrige es el
// código; si lo que estaba mal era el documento, se corrige AHÍ y con su motivo.
//
// `--verificar` compara la salida contra el snapshot y sale con 1 si difiere. Es
// lo que materializa el `criterio_aceptacion` de `VER-02001` sin pedirle a nadie
// que compare doce líneas a ojo.
// ============================================================================

var bitacora = new Bitacora();

// El momento lo aporta el consumidor: el dominio NO lee el reloj (`ADR-02006`).
// Es fijo para que la salida sea comparable contra el snapshot.
var momento = new DateTimeOffset(2026, 8, 27, 0, 0, 0, TimeSpan.Zero);

ActoConfigurarAdministrador.Ejecutar(bitacora, momento);

var alumna = ActoAltaDeAlumno.Ejecutar(bitacora, momento);

ActoEvaluarAdmisibilidad.Ejecutar(bitacora, alumna, "[3] Admisibilidad de la cuenta Pendiente");

ActoHabilitarConProvisoria.Habilitar(bitacora, alumna);
ActoEvaluarAdmisibilidad.Ejecutar(bitacora, alumna, "[5] Admisibilidad tras habilitar");

ActoHabilitarConProvisoria.ReemplazarCredencial(bitacora, alumna);
ActoEvaluarAdmisibilidad.Ejecutar(bitacora, alumna, "[7] Admisibilidad final");

bitacora.Cerrar();

if (!args.Contains("--verificar", StringComparer.Ordinal))
{
    return 0;
}

return SalidaEsperada.Comparar(bitacora.Lineas);
