using System.Collections.Concurrent;
using System.Security.Cryptography;

namespace GeometriaFactory.Web.Services;

/// <summary>
/// De quién es la cuenta que está en medio de un cambio forzado de contraseña, del lado del
/// servidor y contra una marca opaca del navegador.
/// </summary>
/// <remarks>
/// POR QUÉ EXISTE, Y QUÉ DEFECTO RETIRA. La pantalla del cambio forzado le pedía el correo a la
/// persona **que acababa de escribirlo en el ingreso**. No era un capricho: el ingreso es una
/// superficie estática, la redirección abre otra petición y el estado de sesión —de ámbito de
/// petición— llega vacío del otro lado. La salida de entonces fue un cuarto campo, declarado como
/// apartamiento porque el wireframe dibuja **tres**.
///
/// ACÁ SE RESUELVE COMO EL PRODUCTO YA RESOLVÍA LO MISMO: el testigo de sesión tampoco viaja al
/// navegador, vive del lado del servidor contra una marca opaca (`Web ADR-10003`). Esto es la misma
/// forma para un dato mucho menor, y con eso la pantalla vuelve a los tres campos del wireframe.
///
/// EL CORREO NO VIAJA POR LA DIRECCIÓN, que es lo que el wireframe §5 no quiere: lo que viaja es
/// una marca que **no dice nada de nadie**.
///
/// SIGUE SIN HABER CALLEJÓN. Quien llega de frente, con un enlace guardado o después de que la
/// marca caducara, **no queda sin puerta**: la pantalla le pide el correo, como hasta hoy. Lo que
/// cambia es que **quien viene del ingreso ya no tiene que repetirlo**.
///
/// VIVE EN MEMORIA DEL PROCESO, con el mismo alcance y la misma consecuencia que el almacén de
/// testigos: un reciclado del proceso lo vacía, y ahí la pantalla vuelve a preguntar. Es el costo
/// que `ADR-10003` §6.1 ya aceptó por escrito para la sesión, sobre un dato que cuesta menos.
/// </remarks>
public sealed class PendingCredentialChangeStore
{
    private readonly ConcurrentDictionary<string, string> _emails = new(StringComparer.Ordinal);

    /// <summary>Nombre de la marca. No lleva nada de la persona: es un identificador opaco.</summary>
    public const string MarkName = "gf-cambio-pendiente";

    /// <summary>Anota de quién es el cambio y devuelve la marca con la que se recupera.</summary>
    public string Remember(string email)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(email);

        var mark = Convert.ToHexString(RandomNumberGenerator.GetBytes(16));
        _emails[mark] = email;

        return mark;
    }

    /// <summary>De quién es, o nada si esa marca no dice nada.</summary>
    public string? Find(string? mark) =>
        string.IsNullOrWhiteSpace(mark) ? null : _emails.GetValueOrDefault(mark);

    /// <summary>
    /// Lo borra. **Se llama al terminar el cambio**: la marca sirve una vez y no queda dando
    /// vueltas para el siguiente que use ese navegador.
    /// </summary>
    public void Discard(string? mark)
    {
        if (!string.IsNullOrWhiteSpace(mark))
        {
            _emails.TryRemove(mark, out _);
        }
    }
}
