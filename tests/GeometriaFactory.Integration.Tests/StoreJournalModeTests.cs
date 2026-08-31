using GeometriaFactory.Infrastructure.Persistence;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// El modo de diario del almacén, que la fuente declara y hasta el 2026-08-31 nadie fijaba.
/// </summary>
/// <remarks>
/// POR QUÉ ESTA PRUEBA EXISTE, y por qué es de integración y no unitaria. `PRODUCT-INTAKE`
/// §17.1.P.4 declara diario <b>WAL</b> y <c>Entornos-Deploy.md</c> §11.1 lo transcribe como
/// condición obligatoria del respaldo. `CompositionRoot` llamaba a <c>UseSqlite(cadena)</c> a
/// secas, con lo cual SQLite se quedaba en <c>delete</c>: <b>la condición estaba escrita y el
/// motor hacía otra cosa</b>. Es el hallazgo <c>MI-01</c> de la mesa del 2026-08-31, y el
/// diagnóstico de esa mesa en una frase —«este producto verifica sus condiciones y no verifica
/// sus efectos»— es exactamente lo que esta prueba viene a romper: <b>afirma el efecto</b>.
///
/// Y va sobre un archivo real y no sobre un almacén en memoria a propósito: WAL <b>no se puede
/// activar en memoria</b>, y una prueba que corriera ahí pasaría sin ejercer nada.
/// </remarks>
public sealed class StoreJournalModeTests
{
    private static async Task<T> SobreUnAlmacenDeArchivo<T>(Func<string, Task<T>> cuerpo)
    {
        var carpeta = Directory.CreateTempSubdirectory("gf-diario-");
        var ruta = Path.Combine(carpeta.FullName, "almacen.db");
        try
        {
            return await cuerpo(ruta).ConfigureAwait(false);
        }
        finally
        {
            // Las tres piezas: el almacén y los dos acompañantes que WAL deja.
            SqliteConnection.ClearAllPools();
            carpeta.Delete(recursive: true);
        }
    }

    private static GeometriaFactoryDbContext ContextoSobre(string ruta) =>
        new(new DbContextOptionsBuilder<GeometriaFactoryDbContext>()
            .UseSqlite($"Data Source={ruta}")
            .Options);

    [Fact]
    public async Task PrepararDejaElAlmacenEnWal()
    {
        var modo = await SobreUnAlmacenDeArchivo(async ruta =>
        {
            await using var contexto = ContextoSobre(ruta);
            await new StorePreparation(contexto).PrepareAsync();

            // Se pregunta por una conexión NUEVA y no por la que preparó: el PRAGMA es
            // persistente y lo que hay que comprobar es que quedó GRABADO en el archivo,
            // no que la sesión que lo puso lo recuerde.
            await using var otra = new SqliteConnection($"Data Source={ruta}");
            await otra.OpenAsync();
            await using var orden = otra.CreateCommand();
            orden.CommandText = "PRAGMA journal_mode;";
            return (await orden.ExecuteScalarAsync())?.ToString();
        });

        Assert.Equal("wal", modo, ignoreCase: true);
    }

    [Fact]
    public async Task ElDiarioSobreviveAlCierreYAlaReapertura()
    {
        // La propiedad de la que depende el respaldo: si el modo se perdiera al reabrir, una
        // copia tomada con el servicio escribiendo podría quedar inconsistente, y el guion
        // `respaldo-almacen.sh` estaría apoyado en algo que no se cumple.
        var modo = await SobreUnAlmacenDeArchivo(async ruta =>
        {
            await using (var contexto = ContextoSobre(ruta))
            {
                await new StorePreparation(contexto).PrepareAsync();
            }

            SqliteConnection.ClearAllPools();

            await using var reabierto = ContextoSobre(ruta);
            await reabierto.Database.OpenConnectionAsync();
            await using var orden = reabierto.Database.GetDbConnection().CreateCommand();
            orden.CommandText = "PRAGMA journal_mode;";
            return (await orden.ExecuteScalarAsync())?.ToString();
        });

        Assert.Equal("wal", modo, ignoreCase: true);
    }
}
