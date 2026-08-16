using System.Text.Json;
using GeometriaFactory.Application.Ports;
using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Infrastructure.Figures;

/// <summary>
/// Adaptador del puerto de validación de figuras. Motor propio, en proceso y **sin red**.
/// </summary>
/// <remarks>
/// ES LA MITAD DE RIESGO DEL PRODUCTO. El intake declara que el defecto que más veces se repite es
/// **escribir el validador sin leer el análisis** (RN-B3), y que su mitigación es la batería
/// obligatoria de diez casos con los escenarios `E-1` a `E-8` como fixtures. Este adaptador nace
/// con las cuatro trampas del formato ya declaradas y no las descubre después
/// (`Definicion-Contrato-Del-Validador-De-Figuras.md` §2):
///
/// <list type="bullet">
/// <item><b>T1</b> — el ortoedro emite `Tapas` donde el visualizador previo exige `Bases`: las dos
/// claves son **sinónimas**. Es la línea que desbloquea el dibujo de todos los ortoedros.</item>
/// <item><b>T2</b> — el texto trae **comas finales** y no es JSON estrictamente válido: se lee con
/// tolerancia, porque es un hecho del producto y no un error a corregir.</item>
/// <item><b>T3</b> — las caras del cubo llegan como `Cuadrado` o como `Rectangulo` según qué
/// ejemplo de la cátedra las emitió: **las dos se aceptan**.</item>
/// <item><b>T4</b> — los valores calculados que trae el texto son incorrectos en dos casos
/// reproducibles: **se señalan y no se rechazan ni se corrigen**. Es el mayor valor didáctico del
/// producto.</item>
/// </list>
///
/// Y LA QUINTA, QUE NO ES DE FORMATO Y SE EQUIVOCA IGUAL DE SEGUIDO: **existencia contra
/// veracidad**. Una dimensión presente con valor `0.00` está presente, y la figura no se descarta.
///
/// TODO CAMINO TERMINA EN UN RESULTADO Y NINGUNO EN UNA NEGATIVA (G-7): un texto que el alumno
/// escribió mal produce observaciones, nunca una condición degradada. Es la garantía que más veces
/// se rompe al implementar.
/// </remarks>
public sealed class LocalFigureValidator : IFigureValidator
{
    /// <summary>
    /// Tolerancia absoluta de la comparación de valores, con **operador estricto**.
    /// </summary>
    /// <remarks>
    /// NO ES UNA ASUNCIÓN (`CU-06002` §10): sale de que el emisor redondea a dos decimales.
    ///
    /// EL OPERADOR ESTRICTO SÍ ES UNA DECISIÓN, Y ESTÁ TOMADA aguas arriba. Sobre el escenario
    /// semilla decide si el producto devuelve **dos** advertencias o tres: el área del cilindro de
    /// `E-1` difiere en exactamente 0.01, y con «mayor o igual» el caso de prueba canónico del
    /// producto fallaría.
    /// </remarks>
    public const double ComparisonTolerance = 0.01;

    private const string TypeField = "Tipo";
    private const string LengthField = "Largo";
    private const string WidthField = "Ancho";
    private const string RadiusField = "Radio";
    private const string HeightField = "Altura";
    private const string AreaField = "Area";
    private const string VolumeField = "Volumen";

    /// <summary>
    /// El campo que llevan las dos observaciones que **no son de ninguna figura**.
    /// </summary>
    /// <remarks>
    /// **[derivación de la etapa `f`, declarada.]** `CU-06001` FA-03 y FA-04 declaran que esas dos
    /// observaciones van **sin posición de pieza**, y no dicen con qué campo. RN-02009 exige campo
    /// en toda observación de especie error de validación, de modo que dejarlo vacío incumpliría la
    /// regla y ponerle el nombre de una clave afirmaría que el defecto está en un campo que nadie
    /// leyó. Se nombra el texto entero, que es lo que efectivamente falló.
    /// </remarks>
    private const string WholeTextField = "Texto";

    private static readonly JsonDocumentOptions ReaderOptions = new()
    {
        // T2: el texto del alumno no es JSON estrictamente válido, y eso es un hecho del producto.
        AllowTrailingCommas = true,
        CommentHandling = JsonCommentHandling.Skip,
    };

    /// <inheritdoc />
    public FigureInterpretation Interpret(string originalJson)
    {
        ArgumentNullException.ThrowIfNull(originalJson);

        // FA-04: un texto que no se puede leer NI CON LA TOLERANCIA es un resultado, no una avería.
        JsonDocument document;
        try
        {
            document = JsonDocument.Parse(originalJson, ReaderOptions);
        }
        catch (JsonException)
        {
            return FigureInterpretation.From(
                0,
                [],
                [Observation.ValidationErrorAt(null, WholeTextField)]);
        }

        using (document)
        {
            var root = document.RootElement;

            // Los escenarios E-3 y E-4 traen UNA FIGURA SUELTA y no un array: el conjunto raíz de
            // un texto así tiene una figura. **[derivación de la etapa `f`, declarada: §20.E-3 y
            // §20.E-4 transcriben el objeto sin envolverlo, y rechazarlo dejaría dos casos de la
            // batería obligatoria sin poder ejecutarse.]**
            var figures = root.ValueKind switch
            {
                JsonValueKind.Array => root.EnumerateArray().ToList(),
                JsonValueKind.Object => [root],
                _ => (List<JsonElement>)[],
            };

            if (root.ValueKind is not (JsonValueKind.Array or JsonValueKind.Object))
            {
                return FigureInterpretation.From(
                    0,
                    [],
                    [Observation.ValidationErrorAt(null, WholeTextField)]);
            }

            // FA-03: el conjunto raíz vacío devuelve 0 figuras y una observación sobre el conjunto.
            if (figures.Count == 0)
            {
                return FigureInterpretation.From(
                    0,
                    [],
                    [Observation.ValidationErrorAt(null, WholeTextField)]);
            }

            var pieces = new List<Piece>();
            var observations = new List<Observation>();

            for (var position = 0; position < figures.Count; position++)
            {
                // G-2: un defecto en una figura NO DESCARTA EL RESTO DEL ANÁLISIS, y G-3: su
                // posición queda reservada porque las siguientes conservan la suya.
                var reconstruction = Reconstruct(figures[position], position);

                if (reconstruction.Failure is not null)
                {
                    observations.Add(reconstruction.Failure);
                    continue;
                }

                pieces.Add(reconstruction.Piece!);
                observations.AddRange(Verify(reconstruction.Piece!));
            }

            return FigureInterpretation.From(figures.Count, pieces, observations);
        }
    }

    /// <summary>
    /// `CU-06001` — Reconstruye una figura, o dice con qué campo falló y en qué posición.
    /// </summary>
    private static (Piece? Piece, Observation? Failure) Reconstruct(JsonElement figure, int position)
    {
        if (figure.ValueKind is not JsonValueKind.Object)
        {
            return (null, Observation.ValidationErrorAt(position, TypeField));
        }

        // FA-01 y FA-02: el tipo ausente y el tipo desconocido tienen el mismo tratamiento.
        if (!TryReadPieceType(figure, out var type))
        {
            return (null, Observation.ValidationErrorAt(position, TypeField));
        }

        var components = new List<Component>();

        foreach (var (key, role) in ComponentKeysOf(type))
        {
            if (!figure.TryGetProperty(key, out var holder))
            {
                continue;
            }

            var elements = holder.ValueKind == JsonValueKind.Array
                ? holder.EnumerateArray().ToList()
                : [holder];

            foreach (var element in elements)
            {
                var component = ReadComponent(element, components.Count, role, out var badField);

                // FA-05, y el caso de `E-8`: una dimensión que está pero no se puede leer.
                if (component is null)
                {
                    return (null, Observation.ValidationErrorAt(position, badField!));
                }

                components.Add(component);
            }
        }

        // Las dimensiones propias de la pieza se leen igual que las del componente: por existencia
        // del campo y no por veracidad de su valor.
        if (!TryReadNumber(figure, LengthField, out var length, out var unreadable)
            || !TryReadNumber(figure, WidthField, out var width, out unreadable)
            || !TryReadNumber(figure, RadiusField, out var radius, out unreadable)
            || !TryReadNumber(figure, HeightField, out var height, out unreadable)
            || !TryReadNumber(figure, AreaField, out var declaredArea, out unreadable)
            || !TryReadNumber(figure, VolumeField, out var declaredVolume, out unreadable))
        {
            return (null, Observation.ValidationErrorAt(position, unreadable!));
        }

        var derivedArea = DeriveArea(type, components, length, width, radius);
        var derivedVolume = DeriveVolume(type, components, length, width, radius, height);

        return (
            Piece.Reconstruct(position, type, declaredArea, derivedArea, declaredVolume, derivedVolume, components),
            null);
    }

    /// <summary>
    /// `CU-06002` — Compara declarado contra derivado y emite las advertencias, **con los dos
    /// valores**. Nunca emite errores de validación: ésos son de `CU-06001`.
    /// </summary>
    private static IEnumerable<Observation> Verify(Piece piece)
    {
        if (Discrepant(piece.DeclaredArea, piece.DerivedArea))
        {
            yield return Observation.ValueDiscrepancyAt(
                piece.Position, AreaField, piece.DeclaredArea!.Value, piece.DerivedArea!.Value);
        }

        if (Discrepant(piece.DeclaredVolume, piece.DerivedVolume))
        {
            yield return Observation.ValueDiscrepancyAt(
                piece.Position, VolumeField, piece.DeclaredVolume!.Value, piece.DerivedVolume!.Value);
        }
    }

    /// <summary>
    /// El operador estricto: advierte cuando la diferencia **supera** la tolerancia.
    /// </summary>
    /// <remarks>
    /// SIN LOS DOS VALORES NO HAY DISCREPANCIA QUE DECLARAR. Si el texto no trae el valor, o si el
    /// tipo no permite derivarlo con lo que el texto trae, **no se compara**: una advertencia
    /// contra un valor que nadie calculó afirmaría una discrepancia inventada.
    /// </remarks>
    private static bool Discrepant(double? declared, double? derived) =>
        declared is not null
        && derived is not null
        && Math.Abs(declared.Value - derived.Value) > ComparisonTolerance;

    /// <summary>
    /// El área derivada de una pieza es **la suma de sus componentes** (`CU-06002` §10).
    /// </summary>
    /// <remarks>
    /// LA SUMA Y LA FÓRMULA COINCIDEN DONDE LAS DOS ESTÁN ESCRITAS: el intake contrasta el área del
    /// cilindro de `E-1` y la del ortoedro de `E-2` contra la suma de sus componentes, y la del
    /// cubo de `E-3` contra `6·l²`, que es la misma suma de sus seis caras de 9.00.
    ///
    /// UN CONJUNTO DE COMPONENTES INCOMPLETO NO SE SUMA. El ortoedro de `E-8` trae `Bases` y no
    /// trae `Laterales`: sumar lo que hay daría 48.00 contra 208.00 declarados y emitiría una
    /// advertencia **que ninguna fuente pide**, sobre una diferencia que no es del alumno sino de
    /// lo que su texto no incluyó. Sin el conjunto completo, el área **no se deriva**.
    /// **[derivación de la etapa `f`, declarada.]**
    /// </remarks>
    private static double? DeriveArea(
        FigureType type,
        IReadOnlyList<Component> components,
        double? length,
        double? width,
        double? radius) => type switch
        {
            FigureType.Rectangle or FigureType.Square =>
                length is not null && width is not null ? length * width : null,
            FigureType.Circle =>
                radius is not null ? Math.PI * radius * radius : null,
            FigureType.DevelopedRectangle =>
                length is not null && width is not null ? length * width : null,
            _ => HasCompleteComponentSet(type, components) && components.All(c => c.DeclaredArea is not null)
                ? components.Sum(c => c.DeclaredArea!.Value)
                : null,
        };

    /// <summary>
    /// El volumen se deriva **de las dimensiones** —`7·7·21`, `3³`— y no de los componentes.
    /// </summary>
    /// <remarks>
    /// DE DÓNDE SALE LA ALTURA DEL ORTOEDRO, que es lo único que las fuentes no enuncian como
    /// regla. Los dos escenarios que la fijan **la ponen en claves distintas**: en `E-1` y `E-2` el
    /// lateral es `Largo 21 · Ancho 7` sobre bases de `7 · 7`, y el volumen derivado que el intake
    /// declara es `7·7·21`, con la altura en `Largo`; en `E-7` el lateral es `Largo 6 · Ancho 8`
    /// sobre bases de `6 · 4`, y el intake declara «altura = `Laterales[0].Ancho` = 8».
    ///
    /// La regla que satisface a los dos, y la única que se sostiene geométricamente: **la altura es
    /// la dimensión del lateral que no es un lado de la base**. Tomar siempre `Largo` rompería
    /// `E-7` y tomar siempre `Ancho` rompería `E-1`, que es el caso de prueba canónico del
    /// producto. **[derivación de la etapa `f`, declarada y elevada al punto de control.]**
    ///
    /// `E-8` trae además la altura **en su propia clave** `Altura`, y ahí no hay nada que deducir.
    /// </remarks>
    private static double? DeriveVolume(
        FigureType type,
        IReadOnlyList<Component> components,
        double? length,
        double? width,
        double? radius,
        double? height) => type switch
        {
            FigureType.Cube => CubeEdge(components, length) is { } edge ? edge * edge * edge : null,
            FigureType.Cylinder => CylinderVolume(components, radius, length),
            FigureType.Orthohedron => OrthohedronVolume(components, length, width, height),
            // Las figuras planas no tienen volumen, y su ausencia no es un dato faltante.
            _ => null,
        };

    private static double? CubeEdge(IReadOnlyList<Component> components, double? length) =>
        length ?? components.FirstOrDefault(c => c.Role == ComponentRole.Face)?.DeclaredLength;

    private static double? CylinderVolume(
        IReadOnlyList<Component> components,
        double? radius,
        double? length)
    {
        var cap = components.FirstOrDefault(c => c.Role == ComponentRole.Cap);
        var side = components.FirstOrDefault(c => c.Role == ComponentRole.Side);

        var r = radius ?? cap?.DeclaredRadius;
        // La altura del cilindro es el `Largo` del rectángulo desarrollado: su `Ancho` es `2πr`.
        var h = length ?? side?.DeclaredLength;

        return r is not null && h is not null ? Math.PI * r * r * h : null;
    }

    private static double? OrthohedronVolume(
        IReadOnlyList<Component> components,
        double? length,
        double? width,
        double? height)
    {
        var b = components.FirstOrDefault(c => c.Role == ComponentRole.Base);

        var baseLength = length ?? b?.DeclaredLength;
        var baseWidth = width ?? b?.DeclaredWidth;

        if (baseLength is null || baseWidth is null)
        {
            return null;
        }

        var h = height ?? HeightFromLaterals(components, baseLength.Value, baseWidth.Value);

        return h is not null ? baseLength * baseWidth * h : null;
    }

    /// <summary>La dimensión del lateral que no es un lado de la base.</summary>
    private static double? HeightFromLaterals(
        IReadOnlyList<Component> components,
        double baseLength,
        double baseWidth)
    {
        var lateral = components.FirstOrDefault(c => c.Role == ComponentRole.Lateral);

        if (lateral is null)
        {
            return null;
        }

        var isBaseSide = (double? d) =>
            d is not null
            && (Math.Abs(d.Value - baseLength) <= ComparisonTolerance
                || Math.Abs(d.Value - baseWidth) <= ComparisonTolerance);

        if (isBaseSide(lateral.DeclaredLength) && lateral.DeclaredWidth is not null)
        {
            return lateral.DeclaredWidth;
        }

        return lateral.DeclaredLength ?? lateral.DeclaredWidth;
    }

    private static bool HasCompleteComponentSet(FigureType type, IReadOnlyList<Component> components) =>
        type switch
        {
            FigureType.Cube => components.Any(c => c.Role == ComponentRole.Face),
            FigureType.Cylinder => components.Any(c => c.Role == ComponentRole.Cap)
                && components.Any(c => c.Role == ComponentRole.Side),
            FigureType.Orthohedron => components.Any(c => c.Role == ComponentRole.Base)
                && components.Any(c => c.Role == ComponentRole.Lateral),
            _ => components.Count > 0,
        };

    /// <summary>
    /// Las claves de componente de cada tipo, con el papel que el emisor les da.
    /// </summary>
    /// <remarks>
    /// ACÁ VIVE T1: el ortoedro se lee **indistintamente** de `Bases` o de `Tapas`, que son
    /// sinónimas, y las dos producen componentes con papel de base. Es la línea que desbloquea el
    /// dibujo de todos los ortoedros que el visualizador previo pierde.
    /// </remarks>
    private static IEnumerable<(string Key, ComponentRole Role)> ComponentKeysOf(FigureType type) =>
        type switch
        {
            FigureType.Cylinder => [("Tapas", ComponentRole.Cap), ("Lado", ComponentRole.Side)],
            FigureType.Cube => [("Caras", ComponentRole.Face)],
            FigureType.Orthohedron =>
                [("Bases", ComponentRole.Base), ("Tapas", ComponentRole.Base), ("Laterales", ComponentRole.Lateral)],
            _ => [],
        };

    private static Component? ReadComponent(
        JsonElement element,
        int position,
        ComponentRole role,
        out string? unreadableField)
    {
        unreadableField = null;

        if (element.ValueKind is not JsonValueKind.Object)
        {
            unreadableField = TypeField;
            return null;
        }

        if (!TryReadComponentType(element, out var type))
        {
            unreadableField = TypeField;
            return null;
        }

        if (!TryReadNumber(element, LengthField, out var length, out unreadableField)
            || !TryReadNumber(element, WidthField, out var width, out unreadableField)
            || !TryReadNumber(element, RadiusField, out var radius, out unreadableField)
            || !TryReadNumber(element, AreaField, out var area, out unreadableField))
        {
            return null;
        }

        return Component.Declare(position, role, type, length, width, radius, area);
    }

    /// <summary>
    /// Lee un número **por existencia del campo y no por veracidad de su valor**.
    /// </summary>
    /// <returns>
    /// Falso **sólo** cuando el campo está y no se puede leer como número, que es el caso de `E-8`:
    /// el emisor escribió `"3,50"` con la coma decimal de su cultura. Un campo ausente devuelve
    /// verdadero con valor nulo, porque ausente no es ilegible.
    /// </returns>
    private static bool TryReadNumber(
        JsonElement element,
        string field,
        out double? value,
        out string? unreadableField)
    {
        value = null;
        unreadableField = null;

        if (!element.TryGetProperty(field, out var property))
        {
            return true;
        }

        if (property.ValueKind == JsonValueKind.Number && property.TryGetDouble(out var number))
        {
            // Un `0.00` presente ES una dimensión legible: existencia contra veracidad.
            value = number;
            return true;
        }

        if (property.ValueKind is JsonValueKind.Null)
        {
            return true;
        }

        unreadableField = field;
        return false;
    }

    private static bool TryReadPieceType(JsonElement figure, out FigureType type)
    {
        type = default;

        if (!TryReadTypeName(figure, out var name))
        {
            return false;
        }

        // El rectángulo desarrollado NO ES UN TIPO DE PIEZA: aparece sólo como componente, y
        // ninguna fuente lo documenta como salida real en el conjunto raíz.
        return name switch
        {
            "Cilindro" => Set(FigureType.Cylinder, out type),
            "Cubo" => Set(FigureType.Cube, out type),
            "Ortoedro" => Set(FigureType.Orthohedron, out type),
            "Rectangulo" => Set(FigureType.Rectangle, out type),
            "Cuadrado" => Set(FigureType.Square, out type),
            "Circulo" => Set(FigureType.Circle, out type),
            _ => false,
        };
    }

    /// <summary>
    /// T3 vive acá: la cara del cubo llega como `Cuadrado` o como `Rectangulo` según qué ejemplo la
    /// emitió, y **el tipo se conserva como llegó**. El dominio no los unifica ni los corrige.
    /// </summary>
    private static bool TryReadComponentType(JsonElement element, out FigureType type)
    {
        type = default;

        if (!TryReadTypeName(element, out var name))
        {
            return false;
        }

        return name switch
        {
            "Rectangulo" => Set(FigureType.Rectangle, out type),
            "Cuadrado" => Set(FigureType.Square, out type),
            "Circulo" => Set(FigureType.Circle, out type),
            "RectanguloDesarrollado" => Set(FigureType.DevelopedRectangle, out type),
            _ => false,
        };
    }

    private static bool TryReadTypeName(JsonElement element, out string name)
    {
        name = string.Empty;

        if (!element.TryGetProperty(TypeField, out var property)
            || property.ValueKind != JsonValueKind.String)
        {
            return false;
        }

        name = property.GetString() ?? string.Empty;
        return name.Length > 0;
    }

    private static bool Set(FigureType value, out FigureType type)
    {
        type = value;
        return true;
    }
}
