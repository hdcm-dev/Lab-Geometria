using GeometriaFactory.Infrastructure.Persistence;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// La salud del almacén, que hasta el 2026-08-31 era un sello del arranque.
/// </summary>
/// <remarks>
/// EL DEFECTO QUE ESTAS PRUEBAS FIJAN. <c>TwoPhaseStartup</c> escribe <c>StoreIsPrepared</c> una
/// sola vez y nada la reevalúa; el <c>healthcheck</c> de la composición sondea <c>/salud</c> cada
/// 30 segundos. El servicio informaba <b>200 «Ready» indefinidamente con el almacén borrado, de
/// sólo lectura o corrupto</b>. Es <c>MI-09</c> de la mesa del 2026-08-31.
///
/// Y son de integración y sobre archivo real por el mismo motivo que las del diario: los modos de
/// falla que interesan —el archivo que desaparece, el permiso que cambia— <b>no existen en
/// memoria</b>, y una prueba que corriera ahí pasaría sin ejercer ninguno.
/// </remarks>
public sealed class StoreHealthTests
{
    private static async Task ConUnAlmacen(Func<string, GeometriaFactoryDbContext, Task> cuerpo)
    {
        var carpeta = Directory.CreateTempSubdirectory("gf-salud-");
        var ruta = Path.Combine(carpeta.FullName, "almacen.db");
        try
        {
            await using var contexto = Contexto(ruta);
            await new StorePreparation(contexto).PrepareAsync();
            await cuerpo(ruta, contexto).ConfigureAwait(false);
        }
        finally
        {
            SqliteConnection.ClearAllPools();
            carpeta.Delete(recursive: true);
        }
    }

    private static GeometriaFactoryDbContext Contexto(string ruta) =>
        new(new DbContextOptionsBuilder<GeometriaFactoryDbContext>()
            .UseSqlite($"Data Source={ruta}")
            .Options);

    [Fact]
    public async Task UnAlmacenPreparadoEstaEnCondiciones()
    {
        await ConUnAlmacen(async (_, contexto) =>
            Assert.True(await new StoreHealth(contexto).IsUsableAsync()));
    }

    [Fact]
    public async Task UnAlmacenBorradoNoEstaEnCondiciones()
    {
        // ES LA PRUEBA QUE DA NOMBRE AL HALLAZGO, y la que muestra por qué abrir la conexión no
        // alcanzaba: SQLite CREA EL ARCHIVO al abrirlo. Con la comprobación anterior —un booleano
        // escrito en el arranque— esto respondía «Ready» para siempre; con una que sólo abriera
        // la conexión, respondería «Ready» sobre una base recién creada y vacía.
        await ConUnAlmacen(async (ruta, _) =>
        {
            SqliteConnection.ClearAllPools();
            File.Delete(ruta);

            await using var sobreElHueco = Contexto(ruta);
            Assert.False(await new StoreHealth(sobreElHueco).IsUsableAsync());
            Assert.True(File.Exists(ruta), "SQLite recrea el archivo al abrirlo: por eso contar el esquema es la comprobación y abrir no lo es.");
        });
    }

    [Fact]
    public async Task UnAlmacenSinEsquemaNoEstaEnCondiciones()
    {
        // El caso intermedio: el archivo existe, se abre sin error, y no tiene nada adentro.
        var carpeta = Directory.CreateTempSubdirectory("gf-salud-vacio-");
        var ruta = Path.Combine(carpeta.FullName, "vacio.db");
        try
        {
            await using (var crear = new SqliteConnection($"Data Source={ruta}"))
            {
                await crear.OpenAsync();
            }

            await using var contexto = Contexto(ruta);
            Assert.False(await new StoreHealth(contexto).IsUsableAsync());
        }
        finally
        {
            SqliteConnection.ClearAllPools();
            carpeta.Delete(recursive: true);
        }
    }

    [Fact]
    public async Task UnAlmacenDeSoloLecturaNoEstaEnCondiciones()
    {
        // LEER NO PRUEBA QUE SE PUEDA ESCRIBIR. Un volumen remontado de sólo lectura deja un
        // almacén perfectamente legible sobre el que NINGÚN ALUMNO PUEDE ENTREGAR, y la salud
        // tiene que verlo. Es lo que la transacción `BEGIN IMMEDIATE` comprueba: sin ella, la
        // cuenta de `__EFMigrationsHistory` pasa —el esquema está intacto— y la salud daría verde.
        //
        // SE USA `Mode=ReadOnly` Y NO UN CAMBIO DE PERMISOS: es la misma condición para el motor
        // y no depende del sistema operativo. El producto es Linux exclusivamente, pero una
        // prueba que sólo compile ahí obliga a suprimir un analizador, y suprimirlo por esto
        // sería pagar con una excepción permanente una portabilidad que no cuesta nada.
        await ConUnAlmacen(async (ruta, _) =>
        {
            SqliteConnection.ClearAllPools();

            await using var contexto = new GeometriaFactoryDbContext(
                new DbContextOptionsBuilder<GeometriaFactoryDbContext>()
                    .UseSqlite($"Data Source={ruta};Mode=ReadOnly")
                    .Options);

            Assert.False(await new StoreHealth(contexto).IsUsableAsync());
        });
    }
}
