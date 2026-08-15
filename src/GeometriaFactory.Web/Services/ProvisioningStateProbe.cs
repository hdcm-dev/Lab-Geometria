using GeometriaFactory.Web.Integration;
using Microsoft.Extensions.DependencyInjection;

namespace GeometriaFactory.Web.Services;

/// <summary>
/// Responde si el laboratorio ya tiene administrador, y **recuerda la respuesta afirmativa para
/// siempre**. Es lo que hace que el guardián 1 de <c>Web ADR-03</c> §2 no cueste un viaje de red
/// por navegación.
/// </summary>
/// <remarks>
/// EL PROBLEMA QUE ESTA CLASE RESUELVE, Y POR QUÉ NO SE PODÍA IGNORAR. El guardián 1 corre en
/// **toda** petición: si cada una preguntara al servicio de datos, cada navegación del producto
/// pagaría una ida y vuelta de red antes de dibujar nada, y el producto se despliega sobre un plan
/// gratuito donde ése es el recurso escaso. Un caché resuelve el costo; el problema es que **un
/// caché mal hecho rompe exactamente el caso que este guardián existe para atender**.
///
/// EL CACHÉ ES ASIMÉTRICO A PROPÓSITO, Y ÉSA ES TODA LA DECISIÓN:
///
///   · **El «sí» se recuerda para siempre.** Una vez que el laboratorio tiene administrador, no
///     hay ningún camino por el que deje de tenerlo: `RN-01` admite **una sola** cuenta con papel
///     `Administrator`, `A-03` responde `409` a la segunda —probado en las tres capas—, y `A-08`
///     **no admite dar de baja la cuenta de administrador**. El estado es de ida y **no vuelve**,
///     de modo que recordar el «sí» no puede quedar viejo: no existe la transición que lo
///     invalidaría. Desde la primera respuesta afirmativa, esta clase **no vuelve a salir a la
///     red nunca**, que es el caso que dura el resto de la vida de la instancia.
///
///   · **El «no» no se recuerda, ni un segundo.** Es la mitad que un caché con vencimiento rompe.
///     La transición «no configurado → configurado» ocurre **una sola vez en la vida de la
///     instancia**, y es exactamente el momento en que hay una persona adelante mirando: acaba de
///     crear la cuenta de administrador y lo siguiente que hace es entrar. Con un «no» recordado
///     —aunque sea por dos segundos— esa persona vería el guardián devolverla al formulario que
///     acaba de completar, y el defecto sería intermitente, irreproducible y de los caros de
///     diagnosticar. El costo de no recordarlo está acotado por el propio guardián: mientras la
///     respuesta sea «no», **todas las rutas desvían al aprovisionamiento**, de modo que la
///     ventana entera son unas pocas peticiones de una sola persona, una vez.
///
///   · **El «no se sabe» tampoco se recuerda, y no es lo mismo que «no».** Cuando el servicio de
///     datos no responde, el cliente devuelve nulo y esta clase lo propaga tal cual: el guardián
///     decide qué hacer con la incertidumbre, y no la convierte en un hecho.
///
/// NO HACE FALTA NINGUNA INVALIDACIÓN, Y ÉSE ES EL PUNTO. No hay un aviso que la pantalla del
/// aprovisionamiento tenga que mandar cuando la configuración sale bien, y no hay un vencimiento
/// que alguien tenga que elegir bien. La corrección no depende de que nadie se acuerde de nada:
/// depende de que el estado sea monótono, que lo es, y de que sólo se recuerde el extremo del que
/// no se vuelve.
///
/// ALCANCE DE APLICACIÓN, y por el mismo motivo que el almacén de testigos: la respuesta es de la
/// instancia, no de la persona ni de la petición. Se pierde con el reciclado del proceso, y eso no
/// cuesta nada —la primera petición después del reciclado vuelve a preguntar una vez—.
///
/// LA LECTURA ES SIN CERROJO. El campo es un <c>bool</c> escrito con un solo valor posible, y dos
/// peticiones que lleguen a la vez con el laboratorio recién configurado escriben las dos
/// <c>true</c>: no hay carrera que pueda producir un valor equivocado, porque no hay dos valores
/// que competir. <see cref="Volatile"/> está por la visibilidad entre hilos, no por la exclusión.
/// </remarks>
public sealed class ProvisioningStateProbe
{
    /// <summary>
    /// El mensajero se pide POR CONSULTA y no se guarda, y no es un detalle de estilo.
    /// </summary>
    /// <remarks>
    /// Esta clase vive con alcance de aplicación y <see cref="DataServiceClient"/> no: lo produce
    /// la fábrica de mensajeros del marco, que recicla sus conexiones sola. Guardarse una
    /// instancia acá la dejaría viva para siempre y le impediría reciclar, que es el defecto
    /// clásico de mezclar los dos alcances. Como el «sí» se recuerda, **este ámbito se crea sólo
    /// mientras la respuesta siga siendo «no»**, es decir, una vez en la vida de la instancia.
    /// </remarks>
    private readonly IServiceScopeFactory _scopes;

    /// <summary>El único extremo que se recuerda: el laboratorio ya está configurado.</summary>
    private bool _configured;

    public ProvisioningStateProbe(IServiceScopeFactory scopes)
    {
        ArgumentNullException.ThrowIfNull(scopes);

        _scopes = scopes;
    }

    /// <summary>
    /// <c>true</c> si el laboratorio ya tiene administrador, <c>false</c> si todavía no, y
    /// **nulo si no se pudo saber**.
    /// </summary>
    public async Task<bool?> IsConfiguredAsync(CancellationToken cancellationToken = default)
    {
        if (Volatile.Read(ref _configured))
        {
            return true;
        }

        using var scope = _scopes.CreateScope();
        var dataService = scope.ServiceProvider.GetRequiredService<DataServiceClient>();

        var configured = await dataService
            .GetLaboratoryProvisioningAsync(cancellationToken)
            .ConfigureAwait(false);

        if (configured is true)
        {
            Volatile.Write(ref _configured, true);
        }

        return configured;
    }
}
