using Microsoft.EntityFrameworkCore;

namespace GeometriaFactory.Infrastructure.Persistence;

/// <summary>
/// Prepara el almacén al arrancar: aplica las transformaciones de esquema pendientes y
/// DETIENE EL ARRANQUE ante un esquema que no corresponde.
/// </summary>
/// <remarks>
/// `Infrastructure ADR-07`, `BT-06`, `US-24` y `US-25`. La regla que hace cumplir es la de
/// `QG-11`: CERO peticiones atendidas con la preparación incompleta. Quien la invoca antes de
/// abrir la escucha es `TwoPhaseStartup` de `GeometriaFactory.Api.Composition`.
/// </remarks>
public sealed class StorePreparation
{
    private readonly GeometriaFactoryDbContext _dbContext;

    public StorePreparation(GeometriaFactoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Si la preparación terminó. Es el único dato de estado que el punto de salud publica
    /// sobre el almacén: la respuesta NO lleva su ruta (`US-29` §3, tercer criterio).
    /// </summary>
    public bool IsPrepared { get; private set; }

    /// <summary>
    /// Aplica las transformaciones pendientes. Si al terminar queda alguna sin aplicar, lanza:
    /// el arranque se detiene en lugar de atender sobre un almacén dudoso (`US-28`).
    /// </summary>
    public async Task PrepareAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await _dbContext.Database.MigrateAsync(cancellationToken).ConfigureAwait(false);

            // EL MODO DE DIARIO SE FIJA ACÁ, Y SE LEE LO QUE EL MOTOR CONTESTA.
            //
            // `PRODUCT-INTAKE` §17.1.P.4 declara diario **WAL** y `Entornos-Deploy.md` §11.1 lo
            // transcribe como condición obligatoria del respaldo. Hasta el 2026-08-31 nadie lo
            // fijaba: `CompositionRoot` llama a `UseSqlite(cadena)` a secas y SQLite se queda en
            // `delete`, de modo que la condición estaba escrita y el motor hacía otra cosa.
            //
            // VA UNA VEZ EN LA PREPARACIÓN Y NO POR CONEXIÓN, porque el PRAGMA es **persistente**:
            // queda grabado en el archivo y sobrevive al reinicio. Ponerlo por conexión sería
            // repetir en cada petición algo que el almacén ya recuerda.
            //
            // SE LEE EL RESULTADO Y NO SE ASUME, que es la mitad que importa. Sobre un almacén en
            // memoria, o sobre un sistema de archivos sin mapeo compartido, SQLite **se queda en
            // `delete` y no falla**: devuelve el modo que pudo aplicar y sigue. Un `ExecuteSql` a
            // ciegas dejaría exactamente el defecto que este bloque viene a reparar —una condición
            // declarada que nadie contrasta—, y el respaldo tomaría copias inconsistentes sin
            // avisar.
            //
            // EL `catch` DE ABAJO LO ENVUELVE A PROPÓSITO: su criterio —nombrar la causa y no el
            // síntoma— vale igual para esto que para el linaje.
            var conexion = _dbContext.Database.GetDbConnection();
            await _dbContext.Database.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
            await using (var orden = conexion.CreateCommand())
            {
                orden.CommandText = "PRAGMA journal_mode=WAL;";
                var modo = (await orden.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false))
                    ?.ToString();

                if (!string.Equals(modo, "wal", StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException(
                        $"El almacén no quedó en modo de diario WAL: el motor informó '{modo ?? "nada"}'. " +
                        "La fuente lo declara WAL y el procedimiento de respaldo depende de él: sin " +
                        "WAL, una copia tomada con el servicio escribiendo puede quedar inconsistente. " +
                        "Suele deberse a un almacén en memoria o a un sistema de archivos que no " +
                        "admite el mapeo compartido que WAL necesita.");
                }
            }
        }
        catch (Exception falla) when (falla is not OperationCanceledException)
        {
            // EL MENSAJE NOMBRA LA CAUSA Y NO EL SÍNTOMA, y antes decía el síntoma.
            //
            // Sin este envoltorio, un almacén que alguien tocó por fuera detenía el arranque con la
            // excepción cruda del proveedor —«table "Account" already exists»— y una traza de pila
            // entera. Quien despliega leía eso y salía a buscar una tabla duplicada: el síntoma. La
            // causa es otra, y es la que importa: **el almacén tiene un linaje que el servicio no
            // entiende**, y por eso no se puede operar sobre él.
            //
            // LA TRAZA NO SE PROPAGA HACIA AFUERA Y TAMPOCO SE PIERDE. `RA-03` gobierna lo que el
            // servicio DICE, y una traza de pila en el mensaje del arranque es lo que más tienta a
            // incluir justamente cuando quien lo lee está diagnosticando. Queda como
            // `InnerException`, disponible para quien la busque y ausente de lo que se muestra.
            //
            // NO SE INTENTA REPARAR NADA. Atender peticiones sobre un almacén que no se entiende es
            // peor que no atender ninguna (`US-00028`), y adivinar el linaje sería inventarlo.
            throw new InvalidOperationException(
                "El almacén no se pudo preparar: su linaje no corresponde al de este servicio. " +
                "Puede ser un almacén de otra versión, uno modificado por fuera de las " +
                "transformaciones del producto, o uno creado a mano. El arranque se detiene en " +
                "lugar de atender sobre un esquema que no corresponde.",
                falla);
        }

        var pending = await _dbContext.Database
            .GetPendingMigrationsAsync(cancellationToken)
            .ConfigureAwait(false);

        if (pending.Any())
        {
            throw new InvalidOperationException(
                "El almacén quedó con transformaciones de esquema sin aplicar. " +
                "El arranque se detiene en lugar de atender sobre un esquema que no corresponde.");
        }

        IsPrepared = true;
    }
}
