using System.Security.Claims;
using GeometriaFactory.Application.Concepts;
using GeometriaFactory.Contracts.Concepts;
using GeometriaFactory.Domain.Values;
using GeometriaFactory.Infrastructure.AI;
using GeometriaFactory.Infrastructure.Security;

namespace GeometriaFactory.Api.Endpoints;

/// <summary>
/// El asistente de conceptos (ADR-14013): <c>POST /v1/conceptos/consultas</c>.
/// </summary>
/// <remarks>
/// <para>
/// APAGADO, EL PUNTO NO EXISTE PARA NADIE: <c>404</c> sin cuerpo, como cualquier ruta que el contrato no
/// declara. Así «fusionar no enciende» se comprueba desde afuera (REQ-IA-17).
/// </para>
/// <para>
/// EL PAPEL Y LA CUENTA SALEN DEL ACCESO FIRMADO; el trabajo de la pantalla lo revalida el caso de uso.
/// La pregunta no va a ningún registro (C-13).
/// </para>
/// </remarks>
public static class ConceptEndpoints
{
    public const string QuestionsRoute = "/conceptos/consultas";

    public static IEndpointRouteBuilder MapConceptEndpoints(this IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        endpoints.MapPost(QuestionsRoute, async (
            ConceptQuestionRequest request,
            HttpContext context,
            AiOptions ai,
            AskConceptUseCase ask,
            CancellationToken cancellationToken) =>
        {
            if (!ai.ConceptsEnabled)
            {
                return Results.NotFound();
            }

            var role = RoleOf(context.User);
            var accountId = AuthenticationEndpoints.AccountIdOf(context.User) ?? Guid.Empty;

            var answer = await ask
                .ExecuteAsync(accountId, role, request?.Question, request?.WorkId, cancellationToken)
                .ConfigureAwait(false);

            return Results.Ok(new ConceptAnswerResponse(
                answer.Form.ToString(),
                (int)answer.Ring,
                answer.Intent,
                answer.Text,
                answer.Card is { } card ? new ConceptCardReference(card.Id, card.Title, card.Text, card.Source) : null,
                answer.Destination is { } d ? new ConceptDestinationChip(d.Id, d.Label, d.Path) : null,
                answer.Origin,
                answer.Note,
                [.. answer.Breakdown.Select(b => new ConceptBreakdownEntry(b.Label, b.Count))]));
        })
        .WithName("AskConcept")
        .WithSummary("Pregunta un concepto al asistente (apagado por omisión)")
        .Produces<ConceptAnswerResponse>(StatusCodes.Status200OK)
        .ProducesContractErrors(StatusCodes.Status401Unauthorized, StatusCodes.Status403Forbidden, StatusCodes.Status500InternalServerError)
        .Produces(StatusCodes.Status404NotFound)
        .ProducesRateLimitRejection()
        .RequireAuthorization();

        return endpoints;
    }

    private static Role RoleOf(ClaimsPrincipal principal)
    {
        var claim = principal.FindFirstValue(AccessTokenIssuer.RoleClaim);

        return Enum.TryParse<Role>(claim, ignoreCase: false, out var role) && Enum.IsDefined(role)
            ? role
            : Role.Student;
    }
}
