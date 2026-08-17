using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Domain.Entities;

/// <summary>
/// Componente de una pieza. Es término del glosario del cliente y no se renombra en el texto.
/// </summary>
/// <remarks>
/// ETAPA `f` (`Domain BT-06`): la entidad se modela con los atributos de
/// `Definicion-Modelo-De-Dominio.md` §2.4.
///
/// LAS DIMENSIONES SE COMPRUEBAN POR EXISTENCIA Y NO POR VERACIDAD (§2.4, y `PRODUCT-INTAKE`
/// §20.E-6): un `0.00` presente es una dimensión legible y **no descarta la figura**. Por eso las
/// tres dimensiones son anulables —`null` significa **ausente en el texto**, que es distinto de
/// presente en cero— y ninguna se rechaza por su valor.
///
/// EL TIPO NO SE UNIFICA NI SE CORRIGE (§2.4): «dos discriminantes distintos pueden nombrar la
/// misma forma». La cara del cubo llega como `Cuadrado` o como `Rectangulo` según qué ejemplo de
/// la cátedra la emitió (T3), y las dos se conservan **como llegaron**. Unificarlas acá borraría
/// del dato guardado la diferencia que el alumno ve en su propio programa.
/// </remarks>
public sealed class Component
{
    private Component()
    {
    }

    /// <summary>Lugar que ocupa dentro del conjunto de componentes de su pieza. Contigua desde 0.</summary>
    public int Position { get; private set; }

    /// <summary>Qué es respecto de su pieza: tapa, cara, base, lateral o lado.</summary>
    public ComponentRole Role { get; private set; }

    /// <summary>Discriminante que el texto declara. **No se unifica ni se corrige.**</summary>
    public FigureType Type { get; private set; }

    /// <summary>Valor de la clave `Largo`. Nulo cuando el texto no la trae.</summary>
    public double? DeclaredLength { get; private set; }

    /// <summary>Valor de la clave `Ancho`. Nulo cuando el texto no la trae.</summary>
    public double? DeclaredWidth { get; private set; }

    /// <summary>Valor de la clave `Radio`. Nulo cuando el texto no la trae.</summary>
    public double? DeclaredRadius { get; private set; }

    /// <summary>Valor de la clave `Area`, tal como el texto lo trae. **No se corrige.**</summary>
    public double? DeclaredArea { get; private set; }

    /// <summary>
    /// Declara un componente leído del texto del alumno.
    /// </summary>
    /// <remarks>
    /// NO VALIDA NADA Y NO PUEDE FALLAR, y es deliberado: quien decide si una figura se pudo
    /// reconstruir es el adaptador que la lee, con las observaciones que emite. Un componente que
    /// se rechazara a sí mismo dejaría al validador sin nada que reportar y sin posición que
    /// reservar.
    /// </remarks>
    public static Component Declare(
        int position,
        ComponentRole role,
        FigureType type,
        double? declaredLength,
        double? declaredWidth,
        double? declaredRadius,
        double? declaredArea) => new()
        {
            Position = position,
            Role = role,
            Type = type,
            DeclaredLength = declaredLength,
            DeclaredWidth = declaredWidth,
            DeclaredRadius = declaredRadius,
            DeclaredArea = declaredArea,
        };
}
