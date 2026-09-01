using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace GeometriaFactory.Infrastructure.Persistence;

/// <summary>
/// Responde si el almacén SIGUE estando en condiciones, ahora y no cuando arrancó el servicio.
/// </summary>
/// <remarks>
/// POR QUÉ EXISTE, y qué había antes. `TwoPhaseStartup` escribe `StoreIsPrepared` **una sola vez**
/// al terminar la preparación y nada la vuelve a evaluar; `HealthEndpoint` la publicaba tal cual y
/// el `healthcheck` de la composición sondea esa ruta **cada 30 segundos**. O sea que el sondeo
/// estaba consultando un booleano inmutable: **el servicio informaba 200 «Ready» indefinidamente
/// con el almacén borrado, de sólo lectura o corrupto.** Es el hallazgo <c>MI-09</c> de la mesa del
/// 2026-08-31, votado 5-0.
///
/// LA DECISIÓN YA ESTABA BIEN TOMADA Y NO SE HABÍA REALIZADO. <c>ADR-00007</c> §2 puntos 4 y 5 ya
/// declaran que el punto de salud responde por el estado del servicio y no por el hecho de haber
/// arrancado. Esta clase no cambia esa decisión: la cumple.
///
/// LAS DOS COMPROBACIONES, Y POR QUÉ HACEN FALTA LAS DOS:
///
/// <list type="number">
/// <item><b>Contar las transformaciones aplicadas.</b> Abrir la conexión NO ALCANZA, y es el error
/// que parece natural: <b>SQLite crea el archivo al abrirlo</b>. Un almacén borrado se abriría sin
/// error, vacío y sin esquema, y la salud daría verde sobre una base que perdió todo. Contar filas
/// de <c>__EFMigrationsHistory</c> exige que el esquema exista.</item>
/// <item><b>Intentar una escritura real y deshacerla.</b> Leer no prueba que se pueda escribir: un
/// volumen remontado de sólo lectura, o un disco cuyo permiso cambió, dejan un almacén perfectamente
/// legible sobre el que <b>ningún alumno puede entregar</b>.
///
/// <para><b>Y acá el diseño del parche que la mesa entregó estaba equivocado, medido.</b> Especificaba
/// <c>BEGIN IMMEDIATE; ROLLBACK;</c>, con el fundamento de que <c>IMMEDIATE</c> toma el bloqueo de
/// escritura de inmediato. <b>No lo toma</b>: SQLite difiere la adquisición hasta la primera escritura
/// real, y esa secuencia <b>termina sin error sobre una base de sólo lectura</b> —comprobado sobre
/// `sqlite3` y sobre el proveedor—. La prueba
/// <c>UnAlmacenDeSoloLecturaNoEstaEnCondiciones</c> lo destapó en la primera corrida.</para>
///
/// <para>Lo que sí funciona es <b>forzar la escritura dentro de la transacción y deshacerla</b>. Se
/// verificó que el <c>ROLLBACK</c> no deja rastro: la tabla temporal no queda en <c>sqlite_master</c>.
/// Se descartó la alternativa de reescribir <c>PRAGMA user_version</c> con su propio valor, que
/// también detecta el caso, porque <b>escribe en el encabezado del archivo en cada sondeo</b> —2880
/// veces por día— y esto no deja nada.</para></item>
/// </list>
///
/// LO QUE NO DETECTA, Y SE DICE EN VEZ DE SUPONERSE: <b>disco lleno</b>. La transacción vacía no
/// escribe páginas, de modo que un volumen sin espacio pasa esta comprobación y falla en la primera
/// entrega real. Detectarlo exigiría escribir de verdad y borrar, que sobre el almacén de
/// producción es peor que el problema.
///
/// NO DICE POR QUÉ FALLA, y es deliberado: `RA-03` gobierna lo que el servicio DICE, y el punto de
/// salud es anónimo. El motivo va al registro del servidor, que es donde el operador mira.
/// </remarks>
public sealed class StoreHealth
{
    private readonly GeometriaFactoryDbContext _dbContext;

    public StoreHealth(GeometriaFactoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Si el almacén está en condiciones de atender: tiene esquema y admite escritura.
    /// </summary>
    /// <returns><c>true</c> si las dos comprobaciones pasan; <c>false</c> ante cualquier falla.</returns>
    public async Task<bool> IsUsableAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var conexion = _dbContext.Database.GetDbConnection();
            await _dbContext.Database.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);

            await using (var esquema = conexion.CreateCommand())
            {
                esquema.CommandText = "SELECT COUNT(*) FROM \"__EFMigrationsHistory\";";
                var aplicadas = Convert.ToInt64(
                    await esquema.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false),
                    System.Globalization.CultureInfo.InvariantCulture);

                if (aplicadas <= 0)
                {
                    return false;
                }
            }

            try
            {
                await using var escritura = conexion.CreateCommand();
                escritura.CommandText =
                    "BEGIN IMMEDIATE; CREATE TABLE \"__gf_salud_temporal\" (x); ROLLBACK;";
                await escritura.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (SqliteException ocupado) when (
                ocupado.SqliteErrorCode == 5 /* BUSY */ || ocupado.SqliteErrorCode == 6 /* LOCKED */)
            {
                // OTRO ESCRITOR TIENE EL BLOQUEO, Y ESO NO ES UNA FALLA: ES LA PRUEBA.
                //
                // El producto tiene UN SOLO ESCRITOR por diseño (`ADR-06002`), de modo que este
                // sondeo compite con las entregas de los alumnos. Si tratáramos `BUSY` como
                // «no está en condiciones», la salud se pondría en rojo **exactamente cuando
                // alguien está escribiendo**, que es el momento en que el almacén demuestra que
                // se puede escribir. El sondeo corre cada 30 segundos: un falso rojo ahí llevaría
                // al operador a mirar un problema que no existe, y peor, a desconfiar del aviso
                // cuando sea de verdad.
                //
                // Que el motor conteste `BUSY` prueba dos cosas de una: el archivo está, y hay
                // alguien escribiéndolo.
            }

            return true;
        }
        catch (Exception falla) when (falla is not OperationCanceledException)
        {
            // CUALQUIER falla es «no está en condiciones», y no se distingue entre ellas acá: el
            // punto de salud no las va a poder decir de todos modos. La excepción no se propaga
            // —un sondeo no puede tumbar el proceso que sondea— y el motivo queda para el registro.
            return false;
        }
        finally
        {
            await _dbContext.Database.CloseConnectionAsync().ConfigureAwait(false);
        }
    }
}
