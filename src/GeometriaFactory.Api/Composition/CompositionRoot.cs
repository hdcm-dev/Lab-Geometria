using System.Text;
using GeometriaFactory.Application.Accounts;
using GeometriaFactory.Application.Works;
using GeometriaFactory.Application.Ports;
using GeometriaFactory.Infrastructure.Figures;
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
/// ESTADO EN LA ETAPA `e`: **3 de 4 puertos conectados**, y el que falta tiene su etapa
/// declarada. `IAccountRepository` ⟶ `EfCoreAccountRepository` e `ISystemClock` ⟶
/// `UtcSystemClock` son `Infrastructure BT-09` y `BT-12`, los dos de la etapa `c`;
/// `IWorkRepository` ⟶ `EfCoreWorkRepository` es `BT-10` y lo conecta **esta** etapa.
/// `IFigureValidator` ⟶ `LocalFigureValidator` es `BT-16` (etapa `f`): conectarlo ahora exigiría
/// escribir el intérprete del texto, y eso es adelantar una etapa entera. **No entra ningún
/// puerto nuevo**: siguen siendo cuatro y `QG-10` sigue cuadrando.
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

    /// <summary>
    /// Los puertos conectados, con su adaptador. **La etapa `f` cierra el cuadre de `QG-10`**: con
    /// el validador de figuras los cuatro puertos declarados tienen adaptador, y la mitad derecha
    /// deja de estar incompleta por primera vez desde la etapa `a`.
    /// </summary>
    public static IReadOnlyDictionary<Type, Type> ConnectedPorts { get; } = new Dictionary<Type, Type>
    {
        [typeof(IAccountRepository)] = typeof(EfCoreAccountRepository),
        [typeof(ISystemClock)] = typeof(UtcSystemClock),
        [typeof(IWorkRepository)] = typeof(EfCoreWorkRepository),
        [typeof(IFigureValidator)] = typeof(LocalFigureValidator),
    };

    /// <summary>Nombre de la cadena de conexión del almacén. Su valor llega por configuración.</summary>
    public const string StoreConnectionName = "Store";

    public static IServiceCollection AddCompositionRoot(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        // EL ARRANQUE SE DETIENE SI LA RUTA DEL ALMACÉN NO LLEGÓ, igual que con la clave de firma,
        // y por un motivo que ya costó datos. Hasta la etapa `d` `appsettings.json` traía
        // `Data Source=geometriafactory.db`: una ruta RELATIVA, que resolvía contra el directorio
        // de trabajo y dejaba el almacén de desarrollo ADENTRO del árbol del repositorio. Un
        // valor por omisión así no falla nunca —siempre encuentra dónde escribir— y por eso nadie
        // lo mira: el 2026-08-15 una corrida de guiones borró esa base y se llevó la cuenta de
        // administrador del Product Owner. En la etapa `e` el mismo archivo tiene los trabajos de
        // una comisión.
        //
        // Sin valor por omisión, la falla aparece en el arranque y nombra la llave que falta, en
        // lugar de aparecer como datos que se evaporan. LA RUTA SIGUE SIN ESTAR ESCRITA ACÁ: este
        // código no propone ninguna, sólo se niega a inventarla.
        var connectionString = configuration.GetConnectionString(StoreConnectionName);

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                $"Falta la cadena de conexión '{StoreConnectionName}'. La ruta del almacén llega " +
                "por configuración y nunca embebida en el código: en desarrollo la calcula " +
                "`scripts/store-path.sh` y la exporta como `ConnectionStrings__Store`, y en el " +
                "despliegue la fija el `ENV` de `deploy/Dockerfile`. Se detiene el arranque en " +
                "lugar de elegir un archivo por su cuenta.");
        }

        // Contexto por operación (intake §17.3.P.4): alcance de petición, nunca compartido.
        services.AddDbContext<GeometriaFactoryDbContext>(options => options.UseSqlite(connectionString));
        services.AddScoped<StorePreparation>();
        services.AddSingleton<TwoPhaseStartup>();

        // CONEXIÓN DE LOS PUERTOS. Tres de cuatro, con la etapa del que falta declarada:
        //   IAccountRepository ⟶ EfCoreAccountRepository   (Infrastructure BT-09, etapa `c`)
        //   ISystemClock       ⟶ UtcSystemClock            (Infrastructure BT-12, etapa `c`)
        //   IWorkRepository    ⟶ EfCoreWorkRepository      (Infrastructure BT-10, etapa `e`)
        //   IFigureValidator   ⟶ LocalFigureValidator      (Infrastructure BT-16, etapa `f`)
        services.AddScoped<IAccountRepository, EfCoreAccountRepository>();
        services.AddSingleton<ISystemClock, UtcSystemClock>();
        services.AddScoped<IWorkRepository, EfCoreWorkRepository>();

        //   IFigureValidator   ⟶ LocalFigureValidator     (Infrastructure BT-16, etapa `f`)
        // SINGLETON PORQUE NO TIENE ESTADO NI RECURSO QUE ADMINISTRAR: no abre conexiones, no lee
        // configuración y no toca la base. Es una función del texto que recibe.
        services.AddSingleton<IFigureValidator, LocalFigureValidator>();

        // Los dos mecanismos sensibles. Son los ÚNICOS lugares del producto donde existen una
        // contraseña en claro y una clave de firma (`Infrastructure ADR-04` §7).
        var signing = new SigningOptions();
        configuration.GetSection(SigningOptions.SectionName).Bind(signing);

        // EL ARRANQUE SE DETIENE SI LA CLAVE NO LLEGÓ, y ésta es la razón. Sin esta guardia la
        // pieza arranca entera, responde el punto de salud y se comporta como si estuviera sana:
        // el primer fallo aparece cuando alguien intenta entrar, con un error genérico en
        // pantalla y el motivo real sólo en el registro del servidor. Eso ya ocurrió en el
        // despliegue del 2026-08-15, donde el contenedor se levantó sin la variable y el defecto
        // se descubrió con una persona adelante intentando usar el producto.
        //
        // Una pieza a la que le falta algo sin lo cual NO PUEDE CUMPLIR SU FUNCIÓN tiene que
        // negarse a arrancar, para que el defecto aparezca donde lo ve quien despliega. Es el
        // mismo criterio que `GeometriaFactory.Web` ya aplica a la dirección del servicio de
        // datos, y esta guardia lo trae a la pieza que faltaba.
        //
        // EL MENSAJE NOMBRA LA LLAVE Y NUNCA EL VALOR: dice qué falta y dónde se pone, y no
        // filtra ni un fragmento de la clave hacia el registro (`Infrastructure ADR-04` §7).
        if (string.IsNullOrWhiteSpace(signing.SigningKey))
        {
            throw new InvalidOperationException(
                $"Falta '{SigningOptions.SectionName}:{nameof(SigningOptions.SigningKey)}'. " +
                "La clave de firma del acceso llega por configuración —variable de entorno o " +
                "archivo montado— y sin ella la pieza no puede emitir ninguna sesión de trabajo.");
        }

        if (Encoding.UTF8.GetByteCount(signing.SigningKey) < AccessTokenIssuer.MinimumSigningKeySizeInBytes)
        {
            throw new InvalidOperationException(
                $"La clave de firma provista es más corta que el mínimo de " +
                $"{AccessTokenIssuer.MinimumSigningKeySizeInBytes} bytes que exige el algoritmo. " +
                "Se detiene el arranque en lugar de emitir accesos que no se pueden verificar.");
        }

        services.AddSingleton(signing);
        services.AddSingleton(new PasswordDerivation(
            configuration.GetValue("PasswordDerivation:Iterations", PasswordDerivation.AnchoredIterations)));
        services.AddSingleton<AccessTokenIssuer>();

        // La producción de la contraseña provisoria. NO ES UN PUERTO Y NO LO NECESITA: cruza la
        // frontera como función, igual que la derivación y que la comprobación de credencial, y
        // por eso los puertos del producto siguen siendo cuatro (`Infrastructure CU-07` §10).
        services.AddSingleton<ProvisionalPasswordFactory>();

        // Los casos de uso de la etapa `c`.
        services.AddScoped<ConfigureAdministratorUseCase>();
        services.AddScoped<ResolveSignInUseCase>();
        services.AddScoped<ChangeOwnPasswordUseCase>();

        // Los casos de uso de la etapa `d`: el ciclo de vida de la cuenta de alumno.
        services.AddScoped<RegisterAccountUseCase>();
        services.AddScoped<GovernCommissionAccountsUseCase>();
        services.AddScoped<ResetStudentPasswordUseCase>();

        // Los casos de uso de la etapa `e`: el trabajo con dueño, estado y persistencia.
        // NO ENTRA `Application CU-05` —enviar e interpretar el texto—: es de la etapa `f` y
        // consume el puerto de validación, que sigue sin conectar.
        services.AddScoped<LoadAndEditOwnWorkUseCase>();
        services.AddScoped<ConsultOwnWorksUseCase>();
        services.AddScoped<ReviewCommissionWorksUseCase>();
        services.AddScoped<DeleteWorkUseCase>();

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
