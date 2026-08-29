using GeometriaFactory.Application.Accounts;
using GeometriaFactory.Domain.Values;
using GeometriaFactory.Samples.Application.Avanzado.Semilla;

namespace GeometriaFactory.Samples.Application.Avanzado.Actos;

/// <summary>
/// `CU-04002` — El gobierno de las cuentas de la comisión: las transiciones de situación, con su
/// provisoria, y la baja con confirmación escrita.
/// </summary>
/// <remarks>
/// **LA PROVISORIA LA PRODUCE EL SISTEMA Y NO EL ADMINISTRADOR** (`RN-02014`), y el caso de uso lo
/// hace visible en su firma: recibe **una función que la produce**, no un valor. El sample le pasa
/// una función declarada, que es la forma de verificarlo con dobles.
///
/// **Y LA BAJA EXIGE ESCRIBIR EL CORREO**, no confirmar un diálogo: es la única acción destructiva
/// del producto y `RN-02007` pide que arrastre los trabajos, de modo que confirmarla por descuido
/// cuesta caro.
/// </remarks>
internal static class ActoGobiernoDeCuentas
{
    private static string? Provisoria() => "provisoria-producida-por-el-sistema";

    private static string? Derivar(string enClaro) => $"hash-de-{enClaro}";

    internal static async Task EjecutarAsync(
        Bitacora b, GovernCommissionAccountsUseCase caso, ComisionDeEjemplo comision,
        Func<Guid, Task<int>> contarTrabajos)
    {
        // Habilitar la cuenta pendiente.
        var habilitar = await caso.ChangeStatusAsync(Role.Administrator, comision.AlumnaPendiente.Id,
            AccountStatus.Enabled, Provisoria, Derivar);
        var r1 = habilitar.Value!;
        b.Escribir(
            $"[1] Habilitar cuenta pendiente: {Vocabulario.Situacion(r1.Status).ToLowerInvariant()} "
            + $"| provisoria producida por el sistema: {(r1.ProvisionalPassword is not null ? "si" : "no")}");

        // Bloquear y rehabilitar: la rehabilitación produce una provisoria NUEVA.
        var bloquear = await caso.ChangeStatusAsync(Role.Administrator, comision.AlumnaPendiente.Id,
            AccountStatus.Blocked, Provisoria, Derivar);
        var rehabilitar = await caso.ChangeStatusAsync(Role.Administrator, comision.AlumnaPendiente.Id,
            AccountStatus.Enabled, Provisoria, Derivar);
        b.Escribir(
            $"[1] Bloquear cuenta habilitada: {Vocabulario.Situacion(bloquear.Value!.Status).ToLowerInvariant()} "
            + $"| Rehabilitar: {Vocabulario.Situacion(rehabilitar.Value!.Status).ToLowerInvariant()} "
            + $"+ provisoria {(rehabilitar.Value!.ProvisionalPassword is not null ? "nueva" : "ausente")}");

        // Una transición que la tabla no declara.
        var noAdmitida = await caso.ChangeStatusAsync(Role.Administrator, comision.Alumna.Id,
            AccountStatus.Pending, Provisoria, Derivar);
        b.Escribir($"[1] Transicion no admitida sobre una cuenta habilitada: rechazada {noAdmitida.ConditionCode}");

        // La baja, con el correo escrito distinto y con el correcto.
        var distinto = await caso.DeleteAsync(Role.Administrator, comision.Alumna.Id, "otro@frre.utn.edu.ar");
        b.Escribir($"[1] Baja con el correo escrito distinto: rechazada {distinto.ConditionCode}");

        var antes = await contarTrabajos(comision.AlumnaBloqueada.Id);
        var coincidente = await caso.DeleteAsync(Role.Administrator, comision.AlumnaBloqueada.Id,
            comision.AlumnaBloqueada.Email);
        b.Escribir(
            $"[1] Baja con el correo escrito coincidente: "
            + $"{(coincidente.Succeeded ? "dada de baja" : "rechazada " + coincidente.ConditionCode)} "
            + $"| trabajos arrastrados: {antes}");

        var administrador = await caso.DeleteAsync(Role.Administrator, comision.Administrador.Id,
            comision.Administrador.Email);
        b.Escribir($"[1] Baja de la cuenta de administrador: rechazada {administrador.ConditionCode}");
    }
}
