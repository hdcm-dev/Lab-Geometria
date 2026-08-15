using GeometriaFactory.Application.Accounts;
using GeometriaFactory.Application.Ports;
using GeometriaFactory.Infrastructure.Persistence;
using GeometriaFactory.Infrastructure.Security;
using GeometriaFactory.Infrastructure.Time;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

namespace GeometriaFactory.Api.Composition;

/// <summary>
/// El único lugar donde se conectan los puertos con sus adaptadores (`Api ADR-06`).
/// </summary>
/// <remarks>
/// La puerta `QG-10` (`Api/09 Pipeline-CI-CD.md` §2.1) exige **4 de 4** puertos conectados,
/// **0** sin adaptador y **0** con más de uno, y falla en construcción cuando falta un puerto.
///
/// ESTADO EN LA ETAPA `c`: **2 de 4 puertos conectados**, y los dos que faltan tienen su etapa
/// declarada. `IAccountRepository` ⟶ `EfCoreAccountRepository` e `ISystemClock` ⟶
/// `UtcSystemClock` son `Infrastructure BT-09` y `BT-12`, los dos de esta etapa, y quedan
/// conectados acá. `IWorkRepository` ⟶ `EfCoreWorkRepository` es `BT-10` (etapa `e`) y
/// `IFigureValidator` ⟶ `LocalFigureValidator` es `BT-16` (etapa `f`): conectarlos ahora exigiría
/// escribirlos, y eso es adelantar dos etapas.
///
/// LA CLAVE DE FIRMA SE RECIBE Y NO SE BUSCA. Llega por configuración —variable de entorno o
/// archivo montado— y **no está en el repositorio de código ni en la imagen** (intake §17.3.P.5).
/// Si no llega, el servicio arranca y **no emite ningún acceso**: la elección deliberada es que
/// la falla sea visible en el primer canje y no que se genere una clave al vuelo, que dejaría el
/// sistema funcionando hasta que alguien falsifique un acceso (`Infrastructure ADR-04` §2 punto 3).
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

    /// <summary>Los puertos que la etapa `c` deja conectados, con su adaptador.</summary>
    public static IReadOnlyDictionary<Type, Type> ConnectedPorts { get; } = new Dictionary<Type, Type>
    {
        [typeof(IAccountRepository)] = typeof(EfCoreAccountRepository),
        [typeof(ISystemClock)] = typeof(UtcSystemClock),
    };

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

        // CONEXIÓN DE LOS PUERTOS. Dos de cuatro, con la etapa de los otros dos declarada:
        //   IAccountRepository ⟶ EfCoreAccountRepository   (Infrastructure BT-09, etapa `c`)
        //   ISystemClock       ⟶ UtcSystemClock            (Infrastructure BT-12, etapa `c`)
        //   IWorkRepository    ⟶ EfCoreWorkRepository      (Infrastructure BT-10, etapa `e`)
        //   IFigureValidator   ⟶ LocalFigureValidator      (Infrastructure BT-16, etapa `f`)
        services.AddScoped<IAccountRepository, EfCoreAccountRepository>();
        services.AddSingleton<ISystemClock, UtcSystemClock>();

        // Los dos mecanismos sensibles. Son los ÚNICOS lugares del producto donde existen una
        // contraseña en claro y una clave de firma (`Infrastructure ADR-04` §7).
        var signing = new SigningOptions();
        configuration.GetSection(SigningOptions.SectionName).Bind(signing);
        services.AddSingleton(signing);
        services.AddSingleton(new PasswordDerivation(
            configuration.GetValue("PasswordDerivation:Iterations", PasswordDerivation.AnchoredIterations)));
        services.AddSingleton<AccessTokenIssuer>();

        // Los casos de uso de la etapa `c`.
        services.AddScoped<ConfigureAdministratorUseCase>();
        services.AddScoped<ResolveSignInUseCase>();
        services.AddScoped<ChangeOwnPasswordUseCase>();

        // La guardia de `CU-02`: verificar la firma y la expiración del acceso presentado.
        // El `401` de la guardia NO lleva código del contrato, y es deliberado: el conjunto
        // cerrado no declara ninguno para un acceso ausente, vencido o mal firmado, y esta capa
        // no inventa códigos.
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var issuer = new AccessTokenIssuer(signing);
                options.TokenValidationParameters = issuer.ValidationParameters;
                options.MapInboundClaims = false;
            });
        services.AddAuthorization();

        return services;
    }
}
