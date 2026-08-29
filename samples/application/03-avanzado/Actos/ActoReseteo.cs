using GeometriaFactory.Application.Accounts;
using GeometriaFactory.Domain.Values;
using GeometriaFactory.Samples.Application.Avanzado.Semilla;

namespace GeometriaFactory.Samples.Application.Avanzado.Actos;

/// <summary>
/// `CU-04011` — El reseteo de la credencial de un alumno. **`RN-02012`: conserva la cuenta y sus
/// trabajos**, y no toca su situación.
/// </summary>
/// <remarks>
/// SE RESETEA SOBRE UNA CUENTA BLOQUEADA A PROPÓSITO: es el caso donde la propiedad se ve, porque
/// una cuenta habilitada no permitiría distinguir «no cambió la situación» de «la puso en
/// habilitada».
/// </remarks>
internal static class ActoReseteo
{
    internal static async Task EjecutarAsync(
        Bitacora b, ResetStudentPasswordUseCase caso, ComisionDeEjemplo comision,
        Guid alumnoBloqueado, AccountStatus situacionAntes, int trabajosAntes,
        Func<Guid, Task<int>> contarTrabajos)
    {
        var r = await caso.ExecuteAsync(Role.Administrator, alumnoBloqueado,
            () => "provisoria-nueva", enClaro => $"hash-de-{enClaro}");

        var despues = await contarTrabajos(alumnoBloqueado);
        b.Escribir(
            $"[4] Reseteo de un alumno bloqueado: {(r.Succeeded ? "aplicado" : "rechazado " + r.ConditionCode)} "
            + $"| situacion conservada={Vocabulario.Situacion(r.Value?.Status ?? situacionAntes)} "
            + $"trabajos conservados={despues}");

        var sobreAdministrador = await caso.ExecuteAsync(Role.Administrator, comision.Administrador.Id,
            () => "provisoria", enClaro => $"hash-de-{enClaro}");
        b.Escribir($"[4] Reseteo sobre la cuenta de administrador: rechazado {sobreAdministrador.ConditionCode}");
    }
}
