using GeometriaFactory.Api.Composition;
using GeometriaFactory.Api.Endpoints;
using Microsoft.EntityFrameworkCore;

// Host delgado: no decide nada. La composición vive en `Composition/CompositionRoot.cs`
// y el orden de arranque en `Composition/TwoPhaseStartup.cs` (`Api ADR-06` y `ADR-07`).

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCompositionRoot(builder.Configuration);

// La descripción navegable de la superficie. Qué se publica y dónde lo decide `ApiDocumentation`.
builder.Services.AddApiDocumentation();

var app = builder.Build();

// Fase 1 — preparar el almacén. Nada atiende hasta que esto termina (`QG-11`, `US-27`, `US-28`).
// La guarda de tiempo de diseño existe para que `scripts/migrate.sh` pueda GENERAR una
// transformación sin aplicar ninguna: al generar, la herramienta ejecuta este archivo.
if (!EF.IsDesignTime)
{
    var startup = app.Services.GetRequiredService<TwoPhaseStartup>();

    try
    {
        await startup.PrepareStoreAsync(app.Lifetime.ApplicationStopping);
    }
    catch (InvalidOperationException noSePudoPreparar)
    {
        // DETENERSE ES UNA DECISIÓN Y NO UN CUELGUE, y hasta el 2026-08-30 era un cuelgue.
        //
        // La fase 1 ya se detenía ante un almacén que no se entiende —eso lo exige `US-00028` y
        // funcionaba—, pero lo hacía **dejando escapar la excepción**: el runtime imprimía la
        // cadena entera, con la traza de pila del proveedor y su mensaje sobre una tabla que ya
        // existe. Quien despliega leía el síntoma y una traza, y no la causa.
        //
        // ENVOLVER LA EXCEPCIÓN NO ALCANZABA, y esa fue la primera corrección intentada: el runtime
        // imprime también las internas, así que la traza salía igual. Lo que hace falta es
        // **atraparla acá y terminar por decisión propia**, que es lo que este bloque hace.
        //
        // LA CAUSA VIAJA AL OPERADOR Y LA TRAZA AL REGISTRO. El mensaje dice qué pasó y qué hacer;
        // el detalle técnico se registra por el canal de registro, que es donde lo busca quien
        // programa y no quien despliega. `RA-03` gobierna lo que el servicio DICE.
        var registro = app.Services.GetRequiredService<ILoggerFactory>().CreateLogger("Arranque");
        registro.LogError(noSePudoPreparar, "La preparación del almacén falló.");

        await Console.Error.WriteLineAsync(noSePudoPreparar.Message);
        await Console.Error.WriteLineAsync(
            "El servicio no atendió ninguna petición. Revisá la ruta del almacén en la " +
            "configuración, o partí de un almacén en su estado de primer arranque.");

        // 78 es `EX_CONFIG` de la convención `sysexits`: la configuración del entorno no sirve.
        // Un código propio distingue esta parada de un cuelgue, que es lo que un orquestador de
        // contenedores necesita para no reintentar indefinidamente algo que no se va a arreglar solo.
        return 78;
    }
}

// Fase 2 — recién ahora se abre la superficie HTTP.
// La guardia de `Api CU-02` va ANTES que cualquier punto: verificar el acceso y su expiración
// ocurre antes de que el punto haga nada, y un rechazo no lee ni escribe nada del almacén.
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// El paso 5 de la guardia: la comprobación del cambio de contraseña pendiente, aplicada a TODO
// punto que exija acceso salvo el del cambio de la propia contraseña. Va como intermediario y
// no como filtro por punto, porque el defecto que se quiere impedir es **olvidarse de un punto**
// y un filtro se olvida en silencio (`Api CU-02` §1 y CA-05).
app.UseMiddleware<PendingPasswordChangeGuard>();

app.MapApiDocumentation();

app.MapHealthEndpoint();
app.MapAuthenticationEndpoints();
app.MapAccountEndpoints();
app.MapCommissionAccountEndpoints();
app.MapWorkEndpoints();

await app.RunAsync();

return 0;

/// <summary>
/// Hace visible el punto de entrada para `GeometriaFactory.Integration.Tests`, que levanta esta
/// misma aplicación en memoria y la golpea por HTTP (intake §17.5.P.6). Sin esto, la batería de
/// integración tendría que reconstruir la composición por su cuenta, que es exactamente lo que
/// dejaría de verificar el cableado real.
/// </summary>
public partial class Program;
