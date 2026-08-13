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
app.MapHealthEndpoint();

await app.RunAsync();
