namespace GeometriaFactory.Infrastructure.AI;

/// <summary>Motivo de habilitación de la IA del asistente (REQ-IA-29). Conjunto cerrado.</summary>
public enum AiEnablementReason
{
    /// <summary>Asistente encendido y servidor de modelos con dirección y modelo.</summary>
    Habilitada = 1,

    /// <summary>Falta <c>AI__Endpoint</c>, o no tiene forma de dirección http o https.</summary>
    FaltaEndpoint = 2,

    /// <summary>Falta <c>AI__Model</c>.</summary>
    FaltaModelo = 3,

    /// <summary><c>AI__ConceptsEnabled</c> en <c>false</c> (el valor por omisión).</summary>
    ApagadaPorParametro = 4,

    /// <summary>Llegó una clave parecida que el producto no lee (<c>AI__Modelo</c>, <c>IA__Model</c>).</summary>
    ClaveDesconocida = 5,
}

/// <summary>El motivo y, si corresponde, la clave que lo explica. Nunca un valor (K2 R6).</summary>
public sealed record AiEnablement(AiEnablementReason Reason, string? Key)
{
    /// <summary>Las claves que el producto lee en la sección <c>AI</c>: una sola clave por dato (REQ-IA-31).</summary>
    public static IReadOnlySet<string> KnownKeys { get; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        nameof(AiOptions.Endpoint), nameof(AiOptions.Model), nameof(AiOptions.TimeoutSeconds),
        nameof(AiOptions.MaxObservations), nameof(AiOptions.KeepAlive), nameof(AiOptions.HintsEnabled),
        nameof(AiOptions.ConceptsEnabled), nameof(AiOptions.ConceptsModelWriter),
    };

    /// <summary>Texto para <c>/estado</c>: el motivo y la clave, como <c>FaltaModelo (AI__Model)</c>.</summary>
    public string Display => Key is null ? Reason.ToString() : $"{Reason} ({Key})";

    /// <summary>
    /// Evalúa el motivo. <paramref name="aiSectionKeys"/> son los nombres hijos de la sección <c>AI</c> y
    /// <paramref name="rootSectionKeys"/> los de la raíz de la configuración.
    /// </summary>
    /// <remarks>
    /// LA CLAVE PARECIDA SE DICE PRIMERO: en PushDispatch una variable con un nombre parecido dejó la IA
    /// apagada en silencio en el sitio real (K2-AP-3). Decir «FaltaModelo» ahí escondería la causa.
    /// </remarks>
    public static AiEnablement Evaluate(AiOptions options, IEnumerable<string> aiSectionKeys, IEnumerable<string> rootSectionKeys)
    {
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(aiSectionKeys);
        ArgumentNullException.ThrowIfNull(rootSectionKeys);

        var unknown = aiSectionKeys.FirstOrDefault(key => !KnownKeys.Contains(key));

        if (unknown is not null)
        {
            return new(AiEnablementReason.ClaveDesconocida, $"{AiOptions.SectionName}__{unknown}");
        }

        // SÓLO SE MARCAN LAS VARIANTES DE ESTA SECCIÓN: una raíz «IA» (en cualquier caja) o «AI_<clave>» /
        // «IA_<clave>» con una clave que el producto sí lee (un guion bajo en lugar de dos). Una variable ajena
        // que empieza igual —en el entorno de los agentes existe `AI_AGENT`— no es de este producto: marcarla
        // dejaba la IA en «ClaveDesconocida» sin motivo (lo encontró la primera corrida de esta prueba).
        var lookalike = rootSectionKeys.FirstOrDefault(key =>
            string.Equals(key, "IA", StringComparison.OrdinalIgnoreCase)
            || ((key.StartsWith("AI_", StringComparison.OrdinalIgnoreCase) || key.StartsWith("IA_", StringComparison.OrdinalIgnoreCase))
                && KnownKeys.Contains(key[3..].TrimStart('_'))));

        if (lookalike is not null)
        {
            return new(AiEnablementReason.ClaveDesconocida, lookalike + "__…");
        }

        if (!options.ConceptsEnabled)
        {
            return new(AiEnablementReason.ApagadaPorParametro, "AI__ConceptsEnabled");
        }

        if (!Uri.TryCreate(options.Endpoint, UriKind.Absolute, out var uri)
            || (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
        {
            return new(AiEnablementReason.FaltaEndpoint, "AI__Endpoint");
        }

        return string.IsNullOrWhiteSpace(options.Model)
            ? new(AiEnablementReason.FaltaModelo, "AI__Model")
            : new(AiEnablementReason.Habilitada, null);
    }
}
