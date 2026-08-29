using GeometriaFactory.Application.Accounts;
using GeometriaFactory.Application.Works;
using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Samples.Application.Basico.Dobles;

namespace GeometriaFactory.Samples.Application.Basico.Actos;

/// <summary>
/// `CU-04003` — La puerta de `ADR-04004` vista de punta a punta: **la comprobación que corta
/// primero, su única excepción declarada, y lo único que levanta la marca**.
/// </summary>
/// <remarks>
/// ES EL CORAZÓN DEL SAMPLE, y son tres hechos en el mismo recorrido:
///
/// 1. Una cuenta con la marca de cambio pendiente **no puede hacer nada más** — ni siquiera pedir
///    su propio listado.
/// 2. **La única excepción es reemplazar su propia credencial**, que es lo que `ADR-04004` §2
///    punto 1 declara y lo que hace que la marca no sea una trampa sin salida.
/// 3. Y **es ese reemplazo, hecho por la propia cuenta, lo único que la levanta**: la misma
///    petición que se rechazaba ahora procede, sin que nadie más intervenga.
/// </remarks>
internal static class ActoCambioDeCredencial
{
    internal static async Task EjecutarAsync(
        Bitacora bitacora,
        RepositorioDeCuentasEnMemoria cuentas,
        RepositorioDeTrabajosEnMemoria trabajos,
        Account marcada)
    {
        bitacora.Acto();
        var listado = new ConsultOwnWorksUseCase(trabajos, cuentas);
        var cambio = new ChangeOwnPasswordUseCase(cuentas);

        // SE INFORMA LO QUE PASÓ Y NO LO QUE EL CONTRATO ESPERA. La primera versión de este acto
        // contaba esta línea como rechazo antes de mirar el resultado, y con eso el recuento final
        // cuadraba con el snapshot **tapando** que la petición había procedido. Un sample que
        // fuerza su propio número deja de servir para lo único que sirve.
        var antes = await bitacora.InvocarAsync(() => listado.ListAsync(marcada.Id));
        if (antes.Succeeded)
        {
            bitacora.Escribir(
                "[4] Cuenta marcada pide listar sus trabajos: PROCEDIÓ — la capa de aplicación no "
                + "comprueba la marca");
        }
        else
        {
            bitacora.Rechazo($"[4] Cuenta marcada pide listar sus trabajos: rechazado {antes.ConditionCode}");
        }

        var reemplazo = await bitacora.InvocarAsync(() => cambio.ExecuteAsync(
            marcada.Id,
            verifyCurrentCredential: _ => CredentialCheck.Matches,
            deriveNewCredential: () => "hash-de-la-credencial-elegida"));
        bitacora.Escribir(
            $"[4] Cuenta marcada reemplaza su credencial: "
            + $"{(reemplazo.Succeeded ? "aceptado" : "rechazado")} (unica excepcion de ADR-04004)");

        var despues = await bitacora.InvocarAsync(() => listado.ListAsync(marcada.Id));
        bitacora.Escribir(
            $"[4] Marca levantada por la propia cuenta: la misma peticion de listado "
            + $"{(despues.Succeeded ? "ahora procede" : "sigue rechazada")}");

        var sinPresentar = await bitacora.InvocarAsync(() => cambio.ExecuteAsync(
            marcada.Id,
            verifyCurrentCredential: _ => CredentialCheck.DoesNotMatch,
            deriveNewCredential: () => "otro-hash"));
        bitacora.Rechazo($"[4] Reemplazo sin presentar la vigente: rechazado {sinPresentar.ConditionCode}");
    }
}
