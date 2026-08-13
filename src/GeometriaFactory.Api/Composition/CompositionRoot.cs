using GeometriaFactory.Application.Ports;
using GeometriaFactory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GeometriaFactory.Api.Composition;

/// <summary>
/// El único lugar donde se conectan los puertos con sus adaptadores (`Api ADR-06`).
/// </summary>
/// <remarks>
/// La puerta `QG-10` (`Api/09 Pipeline-CI-CD.md` §2.1) exige **4 de 4** puertos conectados,
/// **0** sin adaptador y **0** con más de uno, y falla en construcción cuando falta un puerto.
///
/// ESTADO EN LA ETAPA `a`, y hay que decirlo entero: los cuatro puertos están DECLARADOS acá
/// y CERO están conectados, porque ninguno de sus cuatro adaptadores existe todavía —
/// `EfCoreAccountRepository` es `Infrastructure BT-09` (etapa `c`), `UtcSystemClock` es `BT-12`
/// (etapa `c`), `EfCoreWorkRepository` es `BT-10` (etapa `e`) y `LocalFigureValidator` es `BT-16`
/// (etapa `f`). `Plan-Etapa-A.md` §1.6 lo declara: «los adaptadores se nombran en la etapa `a`
/// pero sólo dos se construyen después». Escribir un adaptador acá para que `QG-10` cuadre sería
/// implementar en la etapa `a` lo que la etapa `c` tiene asignado.
///
/// Lo que la etapa `a` sí deja: <see cref="DeclaredPorts"/>, que es la lista contra la que
/// `QG-10` se cuadra, y el lugar único donde cada conexión se va a escribir.
/// </remarks>
public static class CompositionRoot
{
    /// <summary>
    /// Los cuatro puertos del producto, en un solo lugar. Es la mitad izquierda del cuadre de
    /// `QG-10`: la derecha —el adaptador de cada uno— se completa entre las etapas `c` y `f`.
    /// </summary>
    public static IReadOnlyList<Type> DeclaredPorts { get; } =
    [
        typeof(IAccountRepository),
        typeof(IWorkRepository),
        typeof(IFigureValidator),
        typeof(ISystemClock)
    ];

    /// <summary>Nombre de la cadena de conexión del almacén. Su valor llega por configuración.</summary>
    public const string StoreConnectionName = "Store";

    public static IServiceCollection AddCompositionRoot(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        var connectionString = configuration.GetConnectionString(StoreConnectionName)
            ?? throw new InvalidOperationException(
                $"Falta la cadena de conexión '{StoreConnectionName}'. La ruta del almacén llega " +
                "por configuración y nunca embebida en el código.");

        // Contexto por operación (intake §17.3.P.4): alcance de petición, nunca compartido.
        services.AddDbContext<GeometriaFactoryDbContext>(options => options.UseSqlite(connectionString));
        services.AddScoped<StorePreparation>();
        services.AddSingleton<TwoPhaseStartup>();

        // CONEXIÓN DE LOS CUATRO PUERTOS — pendiente, y con su etapa declarada:
        //   IAccountRepository ⟶ EfCoreAccountRepository   (Infrastructure BT-09, etapa `c`)
        //   ISystemClock       ⟶ UtcSystemClock            (Infrastructure BT-12, etapa `c`)
        //   IWorkRepository    ⟶ EfCoreWorkRepository      (Infrastructure BT-10, etapa `e`)
        //   IFigureValidator   ⟶ LocalFigureValidator      (Infrastructure BT-16, etapa `f`)

        return services;
    }
}
