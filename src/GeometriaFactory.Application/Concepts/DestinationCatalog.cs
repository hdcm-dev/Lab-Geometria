using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Application.Concepts;

/// <summary>Un destino del catálogo: chip (verbo + objeto), ruta y quién lo ve.</summary>
/// <param name="Id">Id estable; el modelo nunca arma una ruta (R5).</param>
/// <param name="Label">Texto del chip.</param>
/// <param name="RouteTemplate">Ruta de la página, con <c>{WorkId}</c> si la necesita.</param>
/// <param name="Viewers">Roles que pueden recibir el destino.</param>
/// <param name="QueryParameters">Parámetros de consulta que el destino puede llevar.</param>
/// <param name="OpensAction">Si el chip abre la acción o sólo la pantalla.</param>
/// <param name="OpensWith">
/// Parámetro de consulta que el chip emite SIEMPRE para abrir la acción (<c>pistas</c>, <c>ejemplo</c>), con valor
/// <c>1</c>. Tiene que figurar en <paramref name="QueryParameters"/>, que es lo que la conformidad contrasta con la
/// página (EXP-0005: cierra D-2 y D-3, deuda R-06).
/// </param>
public sealed record Destination(
    string Id,
    string Label,
    string RouteTemplate,
    IReadOnlyList<Role> Viewers,
    IReadOnlyList<string> QueryParameters,
    bool OpensAction,
    string? OpensWith = null);

/// <summary>Un destino ya resuelto para una persona y una pantalla.</summary>
public sealed record ResolvedDestination(string Id, string Label, string Path);

/// <summary>
/// El catálogo de destinos del asistente (`catalogo-destinos.md` del banco; K2 §5).
/// </summary>
/// <remarks>
/// VER NO ES HACER (ARQ-6): «Cargar una versión nueva» sólo la ve el alumno; al docente el caso de uso le
/// contesta con la negativa útil, nunca con un destino que termina en el rechazo de la página.
/// </remarks>
public static class DestinationCatalog
{
    public const string MyWorks = "mis-trabajos";
    public const string NewWork = "trabajo-nuevo";
    public const string FormatExample = "ejemplo-formato";
    public const string CurrentWork = "trabajo-actual";
    public const string SeeHints = "ver-pistas";
    public const string EditWork = "editar-trabajo";
    public const string CommissionList = "entrega-comision";

    /// <summary>`?pistas=1` en `/trabajos/{WorkId}`: abre las pistas de la primera advertencia (EXP-0005, D-3);
    /// por eso el rótulo dice «de la primera advertencia»: la negativa no sabe por cuál se preguntó.</summary>
    public const string HintsParameter = "pistas";

    /// <summary>`?ejemplo=1` en `/trabajo-nuevo`: despliega «Ver un ejemplo» y lo enfoca (EXP-0005, D-2).</summary>
    public const string ExampleParameter = "ejemplo";

    /// <summary>
    /// El chip de <see cref="CurrentWork"/> para el docente: el trabajo que mira es de un alumno, no «tuyo»
    /// (verificación de la act. 003 del EXP-0004, defecto 4).
    /// </summary>
    public const string CurrentWorkLabelForAdministrator = "Volver al trabajo del alumno";

    private static readonly Role[] Student = [Role.Student];
    private static readonly Role[] Both = [Role.Student, Role.Administrator];

    /// <summary>Las siete entradas.</summary>
    public static IReadOnlyList<Destination> All { get; } =
    [
        new(MyWorks, "Abrir mis trabajos", "/mis-trabajos", Student, ["estado"], true),
        new(NewWork, "Cargar un trabajo", "/trabajo-nuevo", Student, [], true),
        new(FormatExample, "Ver el ejemplo de formato", "/trabajo-nuevo", Student, [ExampleParameter], true, ExampleParameter),
        new(CurrentWork, "Volver a tu trabajo", "/trabajos/{WorkId}", Both, [], true),
        new(SeeHints, "Ver pistas de la primera advertencia", "/trabajos/{WorkId}", Student, [HintsParameter], true, HintsParameter),
        new(EditWork, "Cargar una versión nueva", "/trabajos/{WorkId}/editar", Student, [], true),
        new(CommissionList, "Abrir la entrega de la comisión", "/entrega-comision", [Role.Administrator], ["alumno", "estado", "resuelto", "trabajo"], true),
    ];

    /// <summary>
    /// Resuelve un destino para un rol. Devuelve <c>null</c> si el id no existe, si el rol no lo ve o si
    /// la ruta pide un trabajo y no hay uno validado.
    /// </summary>
    public static ResolvedDestination? Resolve(string? id, Role role, Guid? validatedWorkId, string? state = null)
    {
        var entry = All.FirstOrDefault(d => string.Equals(d.Id, id, StringComparison.Ordinal));

        if (entry is null || !entry.Viewers.Contains(role))
        {
            return null;
        }

        var path = entry.RouteTemplate;

        if (path.Contains("{WorkId}", StringComparison.Ordinal))
        {
            if (validatedWorkId is not { } workId || workId == Guid.Empty)
            {
                return null;
            }

            path = path.Replace("{WorkId}", workId.ToString("D"), StringComparison.Ordinal);
        }

        if (state is not null && entry.QueryParameters.Contains("estado"))
        {
            path += "?estado=" + Uri.EscapeDataString(state);
        }

        // EL CHIP ABRE LA ACCIÓN (K2 §5.4): la página lee el parámetro y, si no hay qué abrir (sin advertencias,
        // pistas apagadas), queda igual que sin él. El valor es fijo: nada del texto llega a la dirección.
        if (entry.OpensWith is { } opens)
        {
            path += (path.Contains('?', StringComparison.Ordinal) ? "&" : "?") + opens + "=1";
        }

        var label = entry.Id == CurrentWork && role == Role.Administrator ? CurrentWorkLabelForAdministrator : entry.Label;

        return new(entry.Id, label, path);
    }

    /// <summary>
    /// Valida el catálogo contra las rutas de las páginas. Devuelve los problemas; vacío si está bien.
    /// </summary>
    public static IReadOnlyList<string> Validate(
        IReadOnlyList<Destination> catalog,
        IReadOnlyCollection<string> pageRoutes,
        Func<string, IReadOnlyCollection<string>> queryParametersOf)
    {
        ArgumentNullException.ThrowIfNull(catalog);
        ArgumentNullException.ThrowIfNull(pageRoutes);
        ArgumentNullException.ThrowIfNull(queryParametersOf);

        var problems = new List<string>();

        foreach (var entry in catalog)
        {
            if (!pageRoutes.Contains(entry.RouteTemplate, StringComparer.OrdinalIgnoreCase))
            {
                problems.Add($"{entry.Id}: la ruta {entry.RouteTemplate} no es un @page");
                continue;
            }

            if (entry.OpensWith is { } opens && !entry.QueryParameters.Contains(opens, StringComparer.Ordinal))
            {
                problems.Add($"{entry.Id}: abre con {opens}, que no está entre sus parámetros");
            }

            var supplied = queryParametersOf(entry.RouteTemplate);

            foreach (var parameter in entry.QueryParameters.Where(p => !supplied.Contains(p, StringComparer.Ordinal)))
            {
                problems.Add($"{entry.Id}: la página no recibe el parámetro {parameter}");
            }
        }

        return problems;
    }
}
