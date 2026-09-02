using GeometriaFactory.Web.Integration;

namespace GeometriaFactory.Web.Services;

/// <summary>
/// Si el servicio de datos está respondiendo, y desde cuándo se sabe.
/// </summary>
/// <remarks>
/// POR QUÉ EXISTE, Y QUÉ DECISIÓN DEL PRODUCT OWNER LO ORIGINA. El laboratorio corre en una
/// infraestructura hogareña: el servicio de datos vive detrás de una dirección pública que **el
/// proveedor puede cambiar sin aviso**. El día que cambia, el sitio publicado sigue en pie y deja
/// de alcanzar los datos, y hasta hoy eso se descubría **cuando alguien intentaba hacer algo y le
/// fallaba**.
///
/// La respuesta «correcta» de manual sería un nombre público estable sobre 443. **El Product
/// Owner la descartó, y con razón declarada**: es un laboratorio académico, no hay certificados en
/// esa red, y montar infraestructura para eso está fuera del alcance del proyecto. Lo que pidió en
/// su lugar es que **la desconexión se vea venir**, y eso es lo que esto sostiene.
///
/// DOS PROPIEDADES QUE NO SON DE ADORNO:
///
///   1. **SE CONSULTA SIEMPRE, NO SÓLO CUANDO ALGO FALLA.** Un aviso que aparece recién con el
///      primer error llega tarde por definición: para entonces alguien ya se topó con la falla.
///      El indicador se dibuja en las dos situaciones, y por eso avisa ANTES.
///
///   2. **LA RESPUESTA SE GUARDA POR UNOS SEGUNDOS.** Sin eso, cada dibujo de cada página sumaría
///      una llamada de red por persona: el indicador se pagaría con lentitud en todas las
///      pantallas. La ventana es corta a propósito —lo que se quiere es enterarse pronto, no
///      instantáneamente—.
///
/// ES SINGLETON Y ESO ES DELIBERADO: el estado del servicio no es de una persona ni de una
/// sesión, es del laboratorio. Que dos pestañas compartan la misma lectura es lo correcto.
/// </remarks>
public sealed class DataServiceReachability(IServiceScopeFactory scopes, TimeProvider clock)
{
    /// <summary>
    /// Cuánto dura una lectura antes de volver a preguntar.
    /// </summary>
    /// <remarks>
    /// QUINCE SEGUNDOS NO ES UN NÚMERO ELEGIDO POR SIMETRÍA. Es el mayor retraso que puede tener
    /// el aviso, y a la vez el piso del costo: con una ventana más corta, un aula entera
    /// navegando produciría una llamada de red por página dibujada.
    /// </remarks>
    private static readonly TimeSpan Ventana = TimeSpan.FromSeconds(15);

    private readonly SemaphoreSlim _turno = new(1, 1);
    private Lectura? _ultima;

    /// <summary>Lo que se sabe ahora del servicio de datos.</summary>
    public async Task<Lectura> LeerAsync(CancellationToken cancellationToken = default)
    {
        var ahora = clock.GetUtcNow();

        if (_ultima is { } vigente && ahora - vigente.Momento < Ventana)
        {
            return vigente;
        }

        await _turno.WaitAsync(cancellationToken).ConfigureAwait(false);

        try
        {
            // SE VUELVE A MIRAR DESPUÉS DE ESPERAR EL TURNO: mientras esta llamada hacía cola, otra
            // pudo haber refrescado la lectura, y repetir la consulta de red no agregaría nada.
            if (_ultima is { } yaRefrescada && clock.GetUtcNow() - yaRefrescada.Momento < Ventana)
            {
                return yaRefrescada;
            }

            // EL CLIENTE SE PIDE EN UN ALCANCE PROPIO. Este servicio vive lo que vive la
            // aplicación y el cliente del servicio de datos no: tomarlo del alcance de una
            // petición lo dejaría colgando de una petición que ya terminó.
            using var alcance = scopes.CreateScope();
            var cliente = alcance.ServiceProvider.GetRequiredService<DataServiceClient>();

            var salud = await cliente.GetServiceHealthAsync(cancellationToken).ConfigureAwait(false);
            _ultima = new Lectura(salud is not null, clock.GetUtcNow());
            return _ultima;
        }
        catch
        {
            // CUALQUIER FALLA ES «NO RESPONDE», y no se propaga: este indicador no puede ser la
            // causa de que una pantalla no se dibuje. Sería el colmo que el aviso de caída
            // tumbara la página que iba a mostrarlo.
            _ultima = new Lectura(false, clock.GetUtcNow());
            return _ultima;
        }
        finally
        {
            _turno.Release();
        }
    }

    /// <param name="Responde">Si el servicio de datos contestó.</param>
    /// <param name="Momento">Cuándo se preguntó. Es lo que permite decir «hace tanto».</param>
    public sealed record Lectura(bool Responde, DateTimeOffset Momento);
}
