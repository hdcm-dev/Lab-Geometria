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
    await startup.PrepareStoreAsync(app.Lifetime.ApplicationStopping);
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

/// <summary>
/// Hace visible el punto de entrada para `GeometriaFactory.Integration.Tests`, que levanta esta
/// misma aplicación en memoria y la golpea por HTTP (intake §17.5.P.6). Sin esto, la batería de
/// integración tendría que reconstruir la composición por su cuenta, que es exactamente lo que
/// dejaría de verificar el cableado real.
/// </summary>
public partial class Program;
