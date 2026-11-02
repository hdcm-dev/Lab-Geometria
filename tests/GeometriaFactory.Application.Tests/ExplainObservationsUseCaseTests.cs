using GeometriaFactory.Application.Ports;
using GeometriaFactory.Application.Works;
using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Values;
using Xunit;

namespace GeometriaFactory.Application.Tests;

/// <summary>
/// `ExplainObservationsUseCase` con dobles del validador y del redactor: sin modelo y sin red
/// (`ai-layer/01` §7, pruebas de garantía).
/// </summary>
public sealed class ExplainObservationsUseCaseTests
{
    [Fact]
    public async Task Hints_DoNotChangeObservations()
    {
        var interpretation = Interpretation(
            Observation.ValidationErrorAt(null, "Texto"),
            Observation.ValueDiscrepancyAt(0, "Area", 36, 54),
            Observation.ValueDiscrepancyAt(0, "Volumen", 9, 27));
        var writer = new RecordingWriter();
        var useCase = new ExplainObservationsUseCase(new FixedValidator(interpretation), writer, 5);

        var explained = await useCase.ExplainAsync("{}");

        // LA MISMA INTERPRETACIÓN, NO UNA COPIA PARECIDA: el validador decide (REQ-IA-16).
        Assert.Same(interpretation, explained.Interpretation);
        Assert.Same(interpretation.Observations, explained.Interpretation.Observations);
        Assert.Equal([1, 2], explained.Hints.Select(h => h.ObservationIndex));
    }

    [Fact]
    public async Task OnlyAllowedWarningsReachTheWriter_WithTypeFieldDeclaredAndDirection()
    {
        var interpretation = Interpretation(
            Observation.ValidationErrorAt(0, "Largo"),
            Observation.ValueDiscrepancyAt(0, "Area", 36, 54),
            Observation.ValueDiscrepancyAt(0, "Volumen", 30, 27),
            Observation.ValueDiscrepancyAt(0, "Otro", 1, 2));
        var writer = new RecordingWriter();

        await new ExplainObservationsUseCase(new FixedValidator(interpretation), writer, 5).ExplainAsync("{}");

        Assert.Equal(2, writer.Written.Count);
        Assert.Equal(new ObservationHintRequest(FigureType.Cube, "Area", 36, DiscrepancyDirection.Below, 54), writer.Written[0]);
        Assert.Equal(new ObservationHintRequest(FigureType.Cube, "Volumen", 30, DiscrepancyDirection.Above, 27), writer.Written[1]);
    }

    /// <summary>REQ-IA-10, la parte del tope: con seis advertencias, cinco al redactor y una de plantilla.</summary>
    [Fact]
    public async Task SixWarnings_FiveGoToTheWriterAndOneIsTemplate()
    {
        var observations = Enumerable.Range(0, 6)
            .Select(i => Observation.ValueDiscrepancyAt(0, i % 2 == 0 ? "Area" : "Volumen", i + 1, 100))
            .ToArray();
        var writer = new RecordingWriter();

        var explained = await new ExplainObservationsUseCase(new FixedValidator(Interpretation(observations)), writer, 5)
            .ExplainAsync("{}");

        Assert.Equal(5, writer.Written.Count);
        Assert.Equal(1, writer.Templates);
        Assert.Equal(6, explained.Hints.Count);
        Assert.Equal(HintOrigin.Template, explained.Hints[5].Hints.Origin);
        Assert.All(explained.Hints.Take(5), h => Assert.Equal(HintOrigin.Model, h.Hints.Origin));
    }

    [Fact]
    public async Task ATextWithoutWarningsAsksForNothing()
    {
        var writer = new RecordingWriter();

        var explained = await new ExplainObservationsUseCase(
            new FixedValidator(Interpretation(Observation.ValidationErrorAt(null, "Texto"))), writer, 5)
            .ExplainAsync("no es json");

        Assert.Empty(explained.Hints);
        Assert.Empty(writer.Written);
        Assert.Equal(0, writer.Templates);
    }

    [Fact]
    public async Task AWarningOnAPieceThatDoesNotExistIsSkipped()
    {
        var interpretation = FigureInterpretation.From(1, [], [Observation.ValueDiscrepancyAt(3, "Area", 1, 2)]);
        var writer = new RecordingWriter();

        var explained = await new ExplainObservationsUseCase(new FixedValidator(interpretation), writer, 5).ExplainAsync("{}");

        Assert.Empty(explained.Hints);
    }

    [Fact]
    public void TheConstructorRejectsANegativeCap() =>
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new ExplainObservationsUseCase(new FixedValidator(Interpretation()), new RecordingWriter(), -1));

    [Fact]
    public void TheResultAlwaysCarriesHints()
    {
        var template = new Hints("a", "b", "c", HintOrigin.Template);

        Assert.Equal(HintResultKind.Degraded, HintResult.Degraded(template, "sin-servidor").Kind);
        Assert.Equal("filtro", HintResult.Rejected(template, "filtro").Reason);
        Assert.Null(HintResult.Ok(template with { Origin = HintOrigin.Model }).Reason);
        Assert.Throws<ArgumentNullException>(() => HintResult.Ok(null!));
    }

    private static FigureInterpretation Interpretation(params Observation[] observations) =>
        FigureInterpretation.From(
            1,
            [Piece.Reconstruct(0, FigureType.Cube, 36, 54, 9, 27)],
            observations);

    private sealed class FixedValidator(FigureInterpretation interpretation) : IFigureValidator
    {
        public FigureInterpretation Interpret(string originalJson) => interpretation;
    }

    private sealed class RecordingWriter : IObservationHintWriter
    {
        public List<ObservationHintRequest> Written { get; } = [];

        public int Templates { get; private set; }

        public Task<HintResult> WriteAsync(ObservationHintRequest request, CancellationToken cancellationToken = default)
        {
            Written.Add(request);
            return Task.FromResult(HintResult.Ok(new Hints("1", "2", "3", HintOrigin.Model)));
        }

        public Hints TemplateFor(ObservationHintRequest request)
        {
            Templates++;
            return new Hints("t1", "t2", "t3", HintOrigin.Template);
        }
    }
}
