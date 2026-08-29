using GeometriaFactory.Application.Accounts;
using GeometriaFactory.Samples.Application.Basico.Dobles;

namespace GeometriaFactory.Samples.Application.Basico.Actos;

/// <summary>
/// `CU-04010` — La configuración de la única cuenta de administrador, y el cierre de su ventana.
/// </summary>
/// <remarks>
/// LA VENTANA SE CIERRA CON LA PRIMERA CONFIGURACIÓN Y NO VUELVE A ABRIRSE, y el caso de uso lo
/// resuelve **sin consultar el correo y sin escribir nada**: pregunta si ya hay administrador y
/// termina ahí.
/// </remarks>
internal static class ActoAltaDeAdministrador
{
    internal static async Task EjecutarAsync(
        Bitacora bitacora, RepositorioDeCuentasEnMemoria cuentas, RelojFijo reloj)
    {
        bitacora.Acto();
        var configurar = new ConfigureAdministratorUseCase(cuentas, reloj);

        var primera = await bitacora.InvocarAsync(() =>
            configurar.ExecuteAsync("docente@frre.utn.edu.ar", "Docente", "Titular", "hash-inicial"));

        var identidad = primera.Value!;
        var cuenta = (await cuentas.FindByIdAsync(identidad.Id))!;
        bitacora.Escribir(
            $"[2] Alta de administrador: constituida situacion={Vocabulario.Situacion(cuenta.Status)} "
            + $"papel={Vocabulario.De(cuenta.Role)}");

        var segunda = await bitacora.InvocarAsync(() =>
            configurar.ExecuteAsync("otro@frre.utn.edu.ar", "Otro", "Docente", "hash"));

        bitacora.Rechazo($"[2] Segundo administrador: rechazado {segunda.ConditionCode}");
    }
}
