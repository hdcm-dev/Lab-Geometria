using GeometriaFactory.Application.Accounts;
using GeometriaFactory.Samples.Application.Basico.Dobles;

namespace GeometriaFactory.Samples.Application.Basico.Actos;

/// <summary>
/// `CU-04001` — El alta de la cuenta de alumno, que nace **`Pendiente` y sin credencial**, y el
/// rechazo tipado del correo ya registrado.
/// </summary>
/// <remarks>
/// EL RECHAZO NO INFORMA NADA DE LA CUENTA QUE OCUPA EL CORREO —ni su estado ni su papel—, y es
/// deliberado: informarlo permitiría averiguar por tanteo qué correos están registrados.
/// </remarks>
internal static class ActoAltaDeAlumno
{
    internal static async Task<AccountSnapshot> EjecutarAsync(
        Bitacora bitacora, RepositorioDeCuentasEnMemoria cuentas, RelojFijo reloj)
    {
        bitacora.Acto();
        var alta = new RegisterAccountUseCase(cuentas, reloj);

        var primera = await bitacora.InvocarAsync(() =>
            alta.ExecuteAsync("alumna@frre.utn.edu.ar", "Alumna", "Ejemplo"));

        var cuenta = primera.Value!;
        bitacora.Escribir(
            $"[1] Alta de alumno: constituida situacion={Vocabulario.Situacion(cuenta.Status)} "
            + $"credencial={(cuenta.MustChangePassword ? "pendiente" : "ausente")}");

        var repetida = await bitacora.InvocarAsync(() =>
            alta.ExecuteAsync("ALUMNA@frre.utn.edu.ar", "Otra", "Persona"));

        bitacora.Rechazo($"[1] Alta repetida con el mismo correo: rechazada {repetida.ConditionCode}");
        return cuenta;
    }
}
