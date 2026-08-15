using GeometriaFactory.Contracts.Accounts;

namespace GeometriaFactory.Web.Services;

/// <summary>
/// La credencial de sesión y la identidad de quien entró, EN EL ESTADO DEL CIRCUITO
/// (`Web ADR-03`).
/// </summary>
/// <remarks>
/// ES EL ÚNICO COMPONENTE QUE CUSTODIA LA CREDENCIAL. Se registra con alcance de circuito, de
/// modo que vive en la memoria del servidor de la pieza pública y **el navegador no la recibe
/// nunca**: no se escribe en el marcado, no se pasa por interoperabilidad de guion, no se guarda
/// en el almacenamiento del navegador y no viaja en ninguna cookie. Que no aparezca en el
/// navegador es criterio de aceptación verificable, no una aspiración.
///
/// QUÉ ES LA «MARCA DE SESIÓN» DEL NAVEGADOR EN ESTA ETAPA. `Web CU-02` §4 paso 6 declara que el
/// navegador conserva una marca de sesión propia del circuito que **no transporta la credencial**.
/// Bajo interactividad de servidor esa marca **es el circuito mismo**: la conexión de tiempo real
/// que el navegador ya tiene abierta identifica a la persona sin que ningún dato de cuenta salga
/// del servidor. **No se agrega ninguna cookie propia**, y es una decisión declarada de esta
/// etapa: agregar una sería agregar exactamente el objeto que `ADR-03` descartó por acercar la
/// credencial al navegador, para resolver un problema —sobrevivir a la recarga— que la misma ADR
/// aceptó no resolver («se acepta que la sesión se pierda cuando el proceso recicla»).
///
/// CONSECUENCIA ACEPTADA, Y ES LA QUE `ADR-03` §6 punto 1 declara: recargar la página o perder el
/// proceso termina la sesión, y la persona vuelve a entrar. La maqueta aprobada ya tiene el texto
/// para ese caso — «Tu sesión no se pudo restablecer y volviste acá» —.
/// </remarks>
public sealed class SessionState
{
    private string? _accessToken;

    /// <summary>Si hay una sesión de trabajo abierta en este circuito.</summary>
    public bool IsOpen => _accessToken is not null;

    /// <summary>Identidad de la cuenta que entró.</summary>
    public Guid AccountId { get; private set; }

    /// <summary>Correo de la cuenta que entró. Es lo que la barra lateral muestra.</summary>
    public string Email { get; private set; } = string.Empty;

    /// <summary>Papel de la cuenta que entró. Es lo que decide qué destinos se dibujan.</summary>
    public string Role { get; private set; } = string.Empty;

    /// <summary>Si el papel de la sesión es el de administrador.</summary>
    public bool IsAdministrator => string.Equals(Role, "Administrator", StringComparison.Ordinal);

    /// <summary>
    /// Abre la sesión con lo que el canje devolvió. La credencial entra acá y no sale hacia
    /// ninguna superficie: la única forma de usarla es <see cref="UseAccessToken"/>, que sólo
    /// invoca el cliente del servicio de datos, del lado del servidor.
    /// </summary>
    public void Open(SessionResponse session)
    {
        ArgumentNullException.ThrowIfNull(session);

        _accessToken = session.AccessToken;
        AccountId = session.AccountId;
        Email = session.Email;
        Role = session.Role;
    }

    /// <summary>
    /// Cierra la sesión: descarta la credencial del estado del circuito y olvida la identidad.
    /// </summary>
    public void Close()
    {
        _accessToken = null;
        AccountId = Guid.Empty;
        Email = string.Empty;
        Role = string.Empty;
    }

    /// <summary>
    /// Entrega la credencial a quien va a adjuntarla en una petición hacia el servicio de datos.
    /// </summary>
    /// <remarks>
    /// NO ES UNA PROPIEDAD, Y ES DELIBERADO: una propiedad de lectura es interpolable en el
    /// marcado sin que nadie lo note, y el marcado es lo que el circuito le manda al navegador.
    /// Un método con este nombre no se escribe por accidente dentro de una vista.
    /// </remarks>
    public string? UseAccessToken() => _accessToken;
}
