using GeometriaFactory.Api.Composition;
using GeometriaFactory.Api.Endpoints;
using Microsoft.EntityFrameworkCore;

// Host delgado: no decide nada. La composición vive en `Composition/CompositionRoot.cs`
// y el orden de arranque en `Composition/TwoPhaseStartup.cs` (`Api ADR-06` y `ADR-07`).

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCompositionRoot(builder.Configuration);

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
app.UseAuthentication();
app.UseAuthorization();

app.MapHealthEndpoint();
app.MapAuthenticationEndpoints();
app.MapAccountEndpoints();

await app.RunAsync();

/// <summary>
/// Hace visible el punto de entrada para `GeometriaFactory.Integration.Tests`, que levanta esta
/// misma aplicación en memoria y la golpea por HTTP (intake §17.5.P.6). Sin esto, la batería de
/// integración tendría que reconstruir la composición por su cuenta, que es exactamente lo que
/// dejaría de verificar el cableado real.
/// </summary>
public partial class Program;
