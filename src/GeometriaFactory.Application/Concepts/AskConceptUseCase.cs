using GeometriaFactory.Application.Ports;
using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Application.Concepts;

/// <summary>Cómo está configurada la redacción con modelo (la decide la raíz de composición).</summary>
/// <param name="ModelWriterRequested"><c>AI:ConceptsModelWriter</c>: se pidió redactar con el modelo.</param>
/// <param name="MissingKey">Si se pidió y no está habilitada, la clave que falta (nunca su valor).</param>
public sealed record ConceptWriterSettings(bool ModelWriterRequested, string? MissingKey);

/// <summary>
/// «Preguntá un concepto» (ADR-14013, ADR-14014): enruta, resuelve con la ficha o con un caso de uso,
/// valida el destino por rol e instancia y deja la huella.
/// </summary>
/// <remarks>
/// <para>
/// UN ASISTENTE EN GEOMETRÍA AYUDA A ENTENDER Y A ENCONTRAR, NO HACE EL EJERCICIO (ADR-10010). Todo
/// pedido de resolver es negativa útil con texto fijo, sin modelo y antes de buscar fichas.
/// </para>
/// <para>
/// EL MODELO NO SE LLAMA POR OMISIÓN. Sólo con la redacción pedida y habilitada, y sólo en el anillo 2,
/// con la ficha y la pregunta (REQ-IA-15). Si no contesta, o el filtro lo rechaza, va la ficha.
/// </para>
/// </remarks>
public sealed class AskConceptUseCase
{
    private static readonly Dictionary<WorkStatus, string> StatusLabels = new()
    {
        [WorkStatus.Draft] = "Borrador",
        [WorkStatus.Submitted] = "Pendiente",
        [WorkStatus.Approved] = "Finalizado",
        [WorkStatus.Rejected] = "Rechazado",
    };

    private readonly IConceptCorpus _corpus;
    private readonly IWorkRepository _works;
    private readonly IConceptTraceStore _traces;
    private readonly IConceptAnswerWriter? _writer;
    private readonly ConceptWriterSettings _settings;
    private readonly ISystemClock _clock;

    public AskConceptUseCase(
        IConceptCorpus corpus,
        IWorkRepository works,
        IConceptTraceStore traces,
        IConceptAnswerWriter? writer,
        ConceptWriterSettings settings,
        ISystemClock clock)
    {
        ArgumentNullException.ThrowIfNull(corpus);
        ArgumentNullException.ThrowIfNull(works);
        ArgumentNullException.ThrowIfNull(traces);
        ArgumentNullException.ThrowIfNull(settings);
        ArgumentNullException.ThrowIfNull(clock);
        _corpus = corpus;
        _works = works;
        _traces = traces;
        _writer = writer;
        _settings = settings;
        _clock = clock;
    }

    /// <summary>Las fichas que ve un rol. El filtro va antes de elegir (R4, REQ-IA-26).</summary>
    public IReadOnlyList<ConceptCard> CardsFor(Role role) =>
        [.. _corpus.Cards.Where(card => card.Audience.Contains(role))];

    /// <summary>Contesta una pregunta.</summary>
    /// <param name="requesterId">Cuenta de quien pregunta, del acceso firmado.</param>
    /// <param name="role">Rol de quien pregunta, del acceso firmado.</param>
    /// <param name="question">La pregunta, tal como la escribió.</param>
    /// <param name="screenWorkId">El trabajo de la pantalla, si lo hay. Nunca se toma del texto (ARQ-2).</param>
    public async Task<ConceptAnswer> ExecuteAsync(
        Guid requesterId,
        Role role,
        string? question,
        Guid? screenWorkId,
        CancellationToken cancellationToken = default)
    {
        var cards = CardsFor(role);
        var route = ConceptRouter.Route(question, cards);

        // EL TRABAJO DE LA PANTALLA SE VALIDA UNA SOLA VEZ, con la misma regla que la vista (`View`).
        Work? work = null;
        var workIsForeign = false;

        if (screenWorkId is { } id && id != Guid.Empty)
        {
            work = await _works.FindByIdAsync(id, cancellationToken).ConfigureAwait(false);

            if (work is null || !CanView(work, requesterId, role))
            {
                work = null;
                workIsForeign = true;
            }
        }

        var answer = route.Intent switch
        {
            ConceptRouter.Intents.Injection or ConceptRouter.Intents.Unrelated or ConceptRouter.Intents.TooLong =>
                Fixed(route, ConceptForm.Outside, ConceptTexts.Outside, null),
            ConceptRouter.Intents.Adjacent =>
                Fixed(route, ConceptForm.Outside, ConceptTexts.Adjacent, DestinationCatalog.Resolve(DestinationCatalog.NewWork, role, null)),
            ConceptRouter.Intents.AsksToSolve => Refusal(route, question!, cards, role, work),
            ConceptRouter.Intents.NoCard => Fixed(route, ConceptForm.Abstention, ConceptTexts.Abstention, null),
            ConceptRouter.Intents.CountWarnings => CountWarnings(route, role, work, workIsForeign),
            ConceptRouter.Intents.CountWorks => await CountWorksAsync(route, requesterId, role, cancellationToken).ConfigureAwait(false),
            ConceptRouter.Intents.NewVersion => NewVersion(route, requesterId, role, work, workIsForeign),
            ConceptRouter.Intents.MyWorks => role == Role.Administrator
                ? Fixed(route, ConceptForm.FromHelp, ConceptTexts.CommissionWorks, DestinationCatalog.Resolve(DestinationCatalog.CommissionList, role, null))
                : Fixed(route, ConceptForm.FromHelp, ConceptTexts.MyWorks, DestinationCatalog.Resolve(DestinationCatalog.MyWorks, role, null)),
            ConceptRouter.Intents.NewWork => role == Role.Administrator
                ? Fixed(route, ConceptForm.UsefulRefusal, ConceptTexts.TeacherCannotEdit, null)
                : Fixed(route, ConceptForm.FromHelp, ConceptTexts.NewWork, DestinationCatalog.Resolve(DestinationCatalog.NewWork, role, null)),
            _ => await FromCardAsync(route, question!, role, work, cancellationToken).ConfigureAwait(false),
        };

        await TraceAsync(answer, cancellationToken).ConfigureAwait(false);
        return answer;
    }

    private static bool CanView(Work work, Guid requesterId, Role role) => role == Role.Administrator
        ? work.ResolveAdministratorScope(role, WorkOperation.View).Succeeded
        : work.ResolveStudentAccess(requesterId, WorkOperation.View).Succeeded;

    private static ConceptAnswer Fixed(ConceptRoute route, ConceptForm form, string text, ResolvedDestination? destination) =>
        new(form, route.Ring, route.Intent, text, null, destination, "fijo", null, []);

    private static ConceptAnswer Refusal(ConceptRoute route, string question, IReadOnlyList<ConceptCard> cards, Role role, Work? work)
    {
        var card = ConceptRouter.MatchCard(question, cards);
        var normalized = ConceptRouter.Normalize(question);

        // EL CHIP DE LA NEGATIVA LLEVA A LO QUE SÍ AYUDA, NUNCA A LA RESPUESTA.
        // Y DEPENDE DEL ESTADO (H2, act. 003): «Cargar un trabajo» sólo sin trabajo o con uno en borrador; sobre un
        // trabajo ya enviado no se ofrece (no hay nada que cargar ahí): quedan las pistas si hay advertencias.
        var asksForCode = normalized.Contains("describir", StringComparison.Ordinal) || normalized.Contains(" metodo", StringComparison.Ordinal)
            || normalized.Contains(" codigo", StringComparison.Ordinal);
        string? destinationId =
            normalized.Contains(" json ", StringComparison.Ordinal) ? DestinationCatalog.FormatExample
            : asksForCode && (work is null || work.Status == WorkStatus.Draft) ? DestinationCatalog.NewWork
            : work is not null && work.Observations.Any(o => o.Kind == ObservationKind.Warning) ? DestinationCatalog.SeeHints
            : null;

        return new(
            ConceptForm.UsefulRefusal,
            route.Ring,
            route.Intent,
            card is null ? ConceptTexts.RefusalWithoutConcept : ConceptTexts.RefusalFor(card),
            card,
            DestinationCatalog.Resolve(destinationId, role, work?.Id),
            "fijo",
            null,
            []);
    }

    private static ConceptAnswer CountWarnings(ConceptRoute route, Role role, Work? work, bool workIsForeign)
    {
        if (workIsForeign)
        {
            return new(ConceptForm.NotFound, route.Ring, route.Intent, ConceptTexts.NotFound, null,
                DestinationCatalog.Resolve(role == Role.Administrator ? DestinationCatalog.CommissionList : DestinationCatalog.MyWorks, role, null),
                "fijo", null, []);
        }

        if (work is null)
        {
            return Fixed(route, ConceptForm.FromHelp, ConceptTexts.OpenAWorkFirst,
                DestinationCatalog.Resolve(role == Role.Administrator ? DestinationCatalog.CommissionList : DestinationCatalog.MyWorks, role, null));
        }

        var count = work.Observations.Count(o => o.Kind == ObservationKind.Warning);

        return new(ConceptForm.Figure, route.Ring, route.Intent, ConceptTexts.Warnings(count), null,
            DestinationCatalog.Resolve(DestinationCatalog.CurrentWork, role, work.Id), "caso-de-uso", ConceptTexts.WarningNote, []);
    }

    private async Task<ConceptAnswer> CountWorksAsync(ConceptRoute route, Guid requesterId, Role role, CancellationToken cancellationToken)
    {
        var administrator = role == Role.Administrator;
        var entries = administrator
            ? await _works.ListInAdministratorScopeAsync(null, cancellationToken).ConfigureAwait(false)
            : await _works.ListOwnedByAsync(requesterId, cancellationToken).ConfigureAwait(false);

        // EL TOTAL SALE DEL MISMO CASO DE USO QUE EL LISTADO, y el desglose suma exactamente el total.
        var breakdown = entries
            .GroupBy(entry => entry.Status)
            .OrderBy(group => group.Key)
            .Select(group => new ConceptBreakdownItem(StatusLabels[group.Key], StatusLabels[group.Key], group.Count()))
            .ToList();

        return new(ConceptForm.Total, route.Ring, route.Intent, ConceptTexts.Total(entries.Count, administrator), null,
            DestinationCatalog.Resolve(administrator ? DestinationCatalog.CommissionList : DestinationCatalog.MyWorks, role, null),
            "caso-de-uso", administrator ? ConceptTexts.CommissionTotalNote : ConceptTexts.TotalNote, breakdown);
    }

    private static ConceptAnswer NewVersion(ConceptRoute route, Guid requesterId, Role role, Work? work, bool workIsForeign)
    {
        if (role == Role.Administrator)
        {
            // VER NO ES HACER (ARQ-6): el docente no recibe el destino de la acción.
            return Fixed(route, ConceptForm.UsefulRefusal, ConceptTexts.TeacherCannotEdit,
                DestinationCatalog.Resolve(DestinationCatalog.CurrentWork, role, work?.Id));
        }

        if (workIsForeign)
        {
            return new(ConceptForm.NotFound, route.Ring, route.Intent, ConceptTexts.NotFound, null,
                DestinationCatalog.Resolve(DestinationCatalog.MyWorks, role, null), "fijo", null, []);
        }

        if (work is null)
        {
            return Fixed(route, ConceptForm.FromHelp, ConceptTexts.NewVersionNoWork, DestinationCatalog.Resolve(DestinationCatalog.MyWorks, role, null));
        }

        return work.ResolveStudentAccess(requesterId, WorkOperation.Edit).Succeeded
            ? Fixed(route, ConceptForm.FromHelp, ConceptTexts.NewVersion, DestinationCatalog.Resolve(DestinationCatalog.EditWork, role, work.Id))
            : Fixed(route, ConceptForm.UsefulRefusal, ConceptTexts.NewVersionNotDraft, DestinationCatalog.Resolve(DestinationCatalog.CurrentWork, role, work.Id));
    }

    private async Task<ConceptAnswer> FromCardAsync(ConceptRoute route, string question, Role role, Work? work, CancellationToken cancellationToken)
    {
        var card = route.Card!;
        var destination = DestinationCatalog.Resolve(card.Destination, role, work?.Id);

        if (!_settings.ModelWriterRequested)
        {
            return new(ConceptForm.FromHelp, route.Ring, route.Intent, card.Text, card, destination, "ficha", null, []);
        }

        if (_writer is null)
        {
            return new(ConceptForm.AiOff, route.Ring, route.Intent, card.Text, card, destination, "ficha",
                ConceptTexts.AiOff(_settings.MissingKey ?? "AI__Model"), []);
        }

        var written = await _writer.WriteAsync(card, question, cancellationToken).ConfigureAwait(false);

        return written.Kind == HintResultKind.Ok && !string.IsNullOrWhiteSpace(written.Text)
            ? new(ConceptForm.FromHelp, route.Ring, route.Intent, written.Text!, card, destination, "modelo", null, [])
            : new(ConceptForm.FromHelp, route.Ring, route.Intent, card.Text, card, destination, "ficha", null, []);
    }

    private async Task TraceAsync(ConceptAnswer answer, CancellationToken cancellationToken)
    {
        try
        {
            await _traces.RecordAsync(
                new ConceptTrace(answer.Ring, answer.Intent, answer.Form, answer.Origin, _corpus.Version, DateOnly.FromDateTime(_clock.UtcNow.UtcDateTime)),
                cancellationToken).ConfigureAwait(false);
        }
        catch (Exception exception) when (exception is not OperationCanceledException)
        {
            // LA HUELLA NO ROMPE LA RESPUESTA: perderla cuesta una fila; no contestar cuesta la ayuda.
        }
    }
}
