namespace GeometriaFactory.Web.Services;

/// <summary>
/// Dónde vive el almacén de claves de protección de datos, y cuántas claves hay ahí.
/// </summary>
/// <remarks>
/// POR QUÉ ESTO EXISTE, Y NO ES UN ADORNO DE DIAGNÓSTICO. Hasta el 2026-09-01 el producto corría
/// en el anfitrión real con las claves EN MEMORIA, y la única señal de eso era un aviso al
/// arrancar, en un registro que estaba apagado. El resultado fue semanas de fallos intermitentes
/// sin síntoma legible: formularios rechazados, sesiones que se caían solas y **componentes
/// interactivos que quedaban dibujados y muertos**, porque los descriptores de componente de
/// Blazor también viajan protegidos.
///
/// LA REGLA QUE SALIÓ DE AHÍ ES LA MISMA QUE LA MESA DEL 2026-09-01 YA HABÍA ESCRITO PARA TODO
/// EL PRODUCTO: **verificar los efectos, no las condiciones**. Que el arranque haya llamado a
/// `PersistKeysToFileSystem` es una condición; que haya claves escritas en disco y que sigan ahí
/// después de un reciclado es el efecto, y es lo único que importa. Por eso el estado se publica
/// en la página de estado: se comprueba MIRANDO EL SITIO, sin entrar al anfitrión y sin depender
/// de que alguien se acuerde de encender un registro.
/// </remarks>
public sealed class DataProtectionState(string keysDirectory)
{
    /// <summary>Ruta del almacén de claves, tal como el arranque la resolvió.</summary>
    public string Directory { get; } = keysDirectory;

    /// <summary>
    /// Cuántas claves hay escritas ahora mismo.
    /// </summary>
    /// <remarks>
    /// SE CUENTA AL LEER Y NO SE GUARDA. Un número tomado al arrancar diría lo que había entonces,
    /// que es justamente el error que se quiere dejar de cometer: interesa lo que hay AHORA.
    /// Cero claves con el sitio en pie es un estado válido y transitorio —el marco crea la primera
    /// cuando algo necesita proteger— y por eso la página lo distingue de «no se puede leer».
    /// </remarks>
    public int? CountKeys()
    {
        try
        {
            return System.IO.Directory.EnumerateFiles(Directory, "key-*.xml").Count();
        }
        catch
        {
            // No se puede leer el almacén. NO se devuelve cero: cero es un estado real y distinto.
            return null;
        }
    }
}
