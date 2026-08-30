using GeometriaFactory.Application.Ports;
using GeometriaFactory.Infrastructure.Persistence;
using GeometriaFactory.Infrastructure.Time;
using Microsoft.EntityFrameworkCore;

namespace GeometriaFactory.Samples.Infrastructure.Intermedio;

/// <summary>Los dos adaptadores del almacén, el reloj, y la relectura con contexto nuevo.</summary>
/// <remarks>
/// LA RELECTURA CON CONTEXTO NUEVO ES LA MITAD DEL SAMPLE. EF Core mantiene un seguimiento de
/// identidad: una entidad recién escrita vuelve de una consulta **desde la memoria del contexto**
/// y no del archivo. Contar sobre eso mediría el grafo que armó el sample. Cada verificación de
/// «qué quedó guardado» abre un contexto nuevo sobre el mismo archivo, y por eso lo que se cuenta
/// es lo que SQLite tiene.
/// </remarks>
internal sealed class Contexto : IAsyncDisposable
{
    private readonly string _archivo;
    private readonly GeometriaFactoryDbContext _preparacion;

    private Contexto(string archivo, GeometriaFactoryDbContext preparacion)
    {
        _archivo = archivo;
        _preparacion = preparacion;
    }

    internal ISystemClock Reloj { get; } = new UtcSystemClock();

    internal static async Task<Contexto> AbrirAsync(string archivo) =>
        new(archivo, await Almacen.AbrirEnPrimerArranqueAsync(archivo).ConfigureAwait(false));

    // ------------------------------------------------------------------------
    // CADA OPERACIÓN ABRE SU PROPIO CONTEXTO, Y NO ES UN DETALLE DE PRUEBA.
    //
    // Al escribir este sample un contexto de larga vida hizo estallar el acto 3 con
    // «another instance with the same key value is already being tracked»: el trabajo
    // se releía de un contexto nuevo —para medir lo que quedó en el archivo— y se
    // borraba con el contexto viejo, que todavía seguía la instancia original.
    //
    // La lección no es cómo callar el error: es que `ADR-06002` dice UNA UNIDAD DE
    // TRABAJO POR OPERACIÓN, y un contexto que sobrevive a varias operaciones no es eso.
    // El servicio abre uno por petición; el sample abre uno por acto, que es lo mismo.
    // ------------------------------------------------------------------------

    internal async Task<T> EnTrabajos<T>(Func<IWorkRepository, Task<T>> operacion)
    {
        await using var contexto = Nuevo();
        return await operacion(new EfCoreWorkRepository(contexto)).ConfigureAwait(false);
    }

    internal async Task EnTrabajos(Func<IWorkRepository, Task> operacion)
    {
        await using var contexto = Nuevo();
        await operacion(new EfCoreWorkRepository(contexto)).ConfigureAwait(false);
    }

    internal async Task EnCuentas(Func<IAccountRepository, Task> operacion)
    {
        await using var contexto = Nuevo();
        await operacion(new EfCoreAccountRepository(contexto)).ConfigureAwait(false);
    }

    private GeometriaFactoryDbContext Nuevo() =>
        new(new DbContextOptionsBuilder<GeometriaFactoryDbContext>()
            .UseSqlite($"Data Source={_archivo}").Options);

    internal async Task<T> EnCuentas<T>(Func<IAccountRepository, Task<T>> consulta)
    {
        await using var contexto = Nuevo();
        return await consulta(new EfCoreAccountRepository(contexto)).ConfigureAwait(false);
    }

    /// <summary>Cuántos trabajos de esa cuenta hay en el archivo, contados sin pasar por el puerto.</summary>
    /// <remarks>
    /// EL CONTEO DEL ARRASTRE NO PUEDE PASAR POR EL PUERTO: `ListOwnedByAsync` devuelve la
    /// proyección de listado, que es una consulta del producto; acá hace falta la pregunta cruda
    /// «cuántas filas quedaron», que es lo que la unidad de trabajo deja o no deja.
    /// </remarks>
    internal async Task<int> ContarTrabajosDeAsync(Guid dueño)
    {
        await using var contexto = Nuevo();
        return await contexto.Works.CountAsync(t => t.OwnerId == dueño).ConfigureAwait(false);
    }

    /// <summary>Cuántas filas de piezas, componentes y observaciones cuelgan todavía de ese trabajo.</summary>
    /// <remarks>
    /// SE PREGUNTA EN SQL Y NO POR EL PUERTO, a propósito. El puerto sólo sabe devolver trabajos, y
    /// un trabajo retirado vuelve nulo: preguntándole a él, una fila huérfana en `Pieza` sería
    /// invisible. La pregunta que el retiro físico exige es sobre las TABLAS, y por eso baja hasta
    /// ahí. Es el único lugar del sample que se saltea la frontera, y se saltea para verificarla.
    /// </remarks>
    internal async Task<int> ContarFilasColgandoDeAsync(Guid trabajo)
    {
        await using var contexto = Nuevo();
        var conexion = contexto.Database.GetDbConnection();
        await conexion.OpenAsync().ConfigureAwait(false);
        await using var orden = conexion.CreateCommand();
        orden.CommandText =
            "SELECT (SELECT COUNT(*) FROM Pieza WHERE WorkId = $t) " +
            "     + (SELECT COUNT(*) FROM Observacion WHERE WorkId = $t) " +
            "     + (SELECT COUNT(*) FROM Componente WHERE PieceId IN (SELECT Id FROM Pieza WHERE WorkId = $t))";
        var parametro = orden.CreateParameter();
        parametro.ParameterName = "$t";
        parametro.Value = trabajo.ToString();
        orden.Parameters.Add(parametro);
        return Convert.ToInt32(await orden.ExecuteScalarAsync().ConfigureAwait(false),
            System.Globalization.CultureInfo.InvariantCulture);
    }

    public async ValueTask DisposeAsync() => await _preparacion.DisposeAsync().ConfigureAwait(false);
}

/// <summary>Convierte un rechazo del dominio en una falla ruidosa del sample.</summary>
/// <remarks>
/// SIN ESTO, UN RECHAZO NO ESPERADO SE VE COMO UNA REFERENCIA NULA VEINTE LÍNEAS DESPUÉS. Pasó al
/// escribir este sample: `Account.Register` exige credencial nula y estado `Pending` —la
/// credencial la fija el acto de habilitación, RN-16— y el `Value!` de un resultado rechazado
/// explotó dentro del adaptador, señalando al adaptador. El código de condición dice en una línea
/// lo que la referencia nula no dice en ninguna.
/// </remarks>
internal static class Exigencia
{
    internal static void Exigir(this GeometriaFactory.Domain.Guards.DomainResult resultado, string que)
    {
        if (!resultado.Succeeded)
        {
            throw new InvalidOperationException($"{que} fue rechazado por el dominio: {resultado.ConditionCode}");
        }
    }

    internal static T Exigir<T>(this GeometriaFactory.Domain.Guards.DomainResult<T> resultado, string que) =>
        resultado.Succeeded
            ? resultado.Value!
            : throw new InvalidOperationException($"{que} fue rechazado por el dominio: {resultado.ConditionCode}");
}
