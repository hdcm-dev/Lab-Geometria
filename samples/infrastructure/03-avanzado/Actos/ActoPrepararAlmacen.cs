using GeometriaFactory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GeometriaFactory.Samples.Infrastructure.Avanzado;

/// <summary>Acto 5 — `CU-06010`: el arranque en su forma completa, incluida la que se detiene.</summary>
/// <remarks>
/// LAS TRES LÍNEAS SON UNA SOLA REGLA: `QG-11`, cero peticiones atendidas con la preparación
/// incompleta. La primera aplica y registra el linaje; la segunda no vuelve a aplicar nada,
/// porque el linaje es inmutable; y la tercera **detiene el arranque** en lugar de operar sobre
/// un almacén que no se entiende. Atender sobre un almacén dudoso es peor que no atender.
/// </remarks>
internal static class ActoPrepararAlmacen
{
    internal static async Task EjecutarAsync(string directorio, Action<string> escribir)
    {
        var archivo = Path.Combine(directorio, "geometriafactory-sample-03-preparacion.db");
        Borrar(archivo);

        // ---- Primera preparación ----
        await using (var contexto = Abrir(archivo))
        {
            await new StorePreparation(contexto).PrepareAsync().ConfigureAwait(false);
        }

        int linaje;
        await using (var contexto = Abrir(archivo))
        {
            var aplicadas = await contexto.Database.GetAppliedMigrationsAsync().ConfigureAwait(false);
            linaje = aplicadas.Count();
        }

        escribir($"[5] Preparacion del almacen: {(linaje > 0 ? "transformaciones aplicadas" : "NINGUNA")} "
            + $"| {(linaje > 0 ? "linaje registrado" : "SIN LINAJE")}");

        // ---- Segunda preparación sobre el mismo almacén ----
        await using (var contexto = Abrir(archivo))
        {
            await new StorePreparation(contexto).PrepareAsync().ConfigureAwait(false);
            var pendientes = await contexto.Database.GetPendingMigrationsAsync().ConfigureAwait(false);
            escribir($"[5] Segunda preparacion sobre el mismo almacen: "
                + $"{(pendientes.Any() ? "QUEDARON PENDIENTES" : "sin transformaciones nuevas")}");
        }

        Borrar(archivo);

        // ---- Almacén con linaje desconocido ----
        // SE FABRICA CREANDO LA TABLA `Account` A MANO Y SIN REGISTRO DE LINAJE. Es exactamente
        // la forma del defecto real: un almacén que alguien tocó por fuera, con estructura que se
        // parece a la del producto y sin nada que diga de qué versión viene.
        //
        // DIVERGENCIA D-5 CONTRA §6. §6 espera `arranque detenido MIGRATION_NOT_APPLICABLE`; el
        // código no existe. El arranque SÍ SE DETIENE —que es la mitad que importa— pero lo hace
        // con una excepción del proveedor de base de datos, cuyo texto habla de una tabla que ya
        // existe y no de un linaje que no se entiende. Quien despliega lee el síntoma y no la causa.
        var dudoso = Path.Combine(directorio, "geometriafactory-sample-03-dudoso.db");
        Borrar(dudoso);
        await using (var contexto = Abrir(dudoso))
        {
            await contexto.Database.ExecuteSqlRawAsync(
                "CREATE TABLE Account (Id TEXT NOT NULL PRIMARY KEY);").ConfigureAwait(false);
        }

        var detenido = false;
        var tipoDeLaFalla = "NINGUNA";
        try
        {
            await using var contexto = Abrir(dudoso);
            await new StorePreparation(contexto).PrepareAsync().ConfigureAwait(false);
        }
        catch (Exception falla)
        {
            detenido = true;
            tipoDeLaFalla = falla.GetType().Name;
        }

        escribir($"[5] Preparacion sobre un almacen con linaje desconocido: "
            + $"{(detenido ? "arranque detenido" : "SIGUIO")} {tipoDeLaFalla}, sin codigo tipado");

        Borrar(dudoso);
    }

    /// <summary>Borra el almacén Y SUS DOS LATERALES.</summary>
    /// <remarks>
    /// SQLite EN MODO WAL DEJA TRES ARCHIVOS, NO UNO: al `.db` lo acompañan un `-wal` con las
    /// escrituras todavía no plegadas y un `-shm` con el índice compartido. Borrar sólo el `.db`
    /// deja los otros dos en el directorio del almacén de trabajo, que es justamente el lugar que
    /// este sample se comprometió a no ensuciar. Se vio corriéndolo: la primera versión los dejó.
    ///
    /// Y ANTES HAY QUE SOLTAR LAS CONEXIONES DEL POZO: `Microsoft.Data.Sqlite` las reutiliza, y
    /// una conexión viva mantiene el `-wal` abierto y el borrado no lo alcanza.
    /// </remarks>
    private static void Borrar(string archivo)
    {
        Microsoft.Data.Sqlite.SqliteConnection.ClearAllPools();
        foreach (var lateral in new[] { archivo, archivo + "-wal", archivo + "-shm" })
        {
            if (File.Exists(lateral)) File.Delete(lateral);
        }
    }

    private static GeometriaFactoryDbContext Abrir(string archivo) =>
        new(new DbContextOptionsBuilder<GeometriaFactoryDbContext>()
            .UseSqlite($"Data Source={archivo}").Options);
}
