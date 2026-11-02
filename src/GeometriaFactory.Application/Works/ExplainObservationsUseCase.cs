using GeometriaFactory.Application.Ports;
using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Application.Works;

/// <summary>
/// Explica las advertencias de valor con pistas graduadas (`ai-layer/01` §3.2, V1).
/// </summary>
/// <remarks>
/// <para>
/// EL VALIDADOR DECIDE. El caso de uso corre <see cref="IFigureValidator.Interpret"/> —sincrónico y
/// sin red, igual que en A-18— y devuelve sus observaciones **tal cual** (REQ-IA-16). La capa de IA
/// sólo agrega texto al lado.
/// </para>
/// <para>
/// SÓLO LAS ADVERTENCIAS DE LA LISTA PERMITIDA: `Area` y `Volumen`, los dos nombres que fija el
/// validador. Un error de validación no lleva pistas: no hay valor del que hablar.
/// </para>
/// <para>
/// TOPE POR PEDIDO (REQ-IA-10): las primeras <c>maxObservations</c> van al redactor; las demás salen
/// de plantilla sin tocar el modelo. Las llamadas son de a una: el cupo es uno por instancia.
/// </para>
/// </remarks>
public sealed class ExplainObservationsUseCase
{
    /// <summary>Los dos campos que el validador compara (`LocalFigureValidator.AreaField`, `VolumeField`).</summary>
    private static readonly string[] AllowedFields = ["Area", "Volumen"];

    private readonly IFigureValidator _validator;
    private readonly IObservationHintWriter _writer;
    private readonly int _maxObservations;

    public ExplainObservationsUseCase(IFigureValidator validator, IObservationHintWriter writer, int maxObservations)
    {
        ArgumentNullException.ThrowIfNull(validator);
        ArgumentNullException.ThrowIfNull(writer);
        ArgumentOutOfRangeException.ThrowIfNegative(maxObservations);

        _validator = validator;
        _writer = writer;
        _maxObservations = maxObservations;
    }

    public async Task<ExplainedObservations> ExplainAsync(string originalJson, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(originalJson);

        var interpretation = _validator.Interpret(originalJson);
        var hints = new List<ObservationHints>();
        var sentToWriter = 0;

        for (var index = 0; index < interpretation.Observations.Count; index++)
        {
            if (RequestFor(interpretation, interpretation.Observations[index]) is not { } request)
            {
                continue;
            }

            Hints written;

            if (sentToWriter < _maxObservations)
            {
                sentToWriter++;
                written = (await _writer.WriteAsync(request, cancellationToken).ConfigureAwait(false)).Hints;
            }
            else
            {
                written = _writer.TemplateFor(request);
            }

            hints.Add(new ObservationHints(index, written));
        }

        return new ExplainedObservations(interpretation, hints);
    }

    /// <summary>
    /// Arma el pedido sólo con datos validados, o nada si la observación no lleva pistas.
    /// </summary>
    private static ObservationHintRequest? RequestFor(FigureInterpretation interpretation, Observation observation)
    {
        if (observation is not
            {
                Kind: ObservationKind.Warning,
                PiecePosition: { } position,
                DeclaredValue: { } declared,
                DerivedValue: { } derived,
            }
            || !AllowedFields.Contains(observation.Field, StringComparer.Ordinal)
            || !double.IsFinite(declared)
            || !double.IsFinite(derived)
            || declared == derived)
        {
            return null;
        }

        var piece = interpretation.Pieces.FirstOrDefault(p => p.Position == position);

        if (piece is null || !Enum.IsDefined(piece.Type))
        {
            return null;
        }

        // EL CAMPO SE TOMA DE LA LISTA PERMITIDA Y NO DE LA OBSERVACIÓN: así la cadena que viaja es
        // una constante de este archivo, nunca algo que pasó por el texto del alumno.
        var field = AllowedFields.First(f => string.Equals(f, observation.Field, StringComparison.Ordinal));
        var direction = declared > derived ? DiscrepancyDirection.Above : DiscrepancyDirection.Below;

        return new ObservationHintRequest(piece.Type, field, declared, direction, derived);
    }
}
