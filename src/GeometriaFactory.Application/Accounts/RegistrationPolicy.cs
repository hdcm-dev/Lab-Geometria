namespace GeometriaFactory.Application.Accounts;

/// <summary>
/// CU-01 — Qué correos admite el autorregistro: los de los dominios que quien despliega declara, o
/// cualquiera si no declara ninguno.
/// </summary>
/// <remarks>
/// POR QUÉ EXISTE (mesa `SDD/Docs/Audit/Mesa-2026-09-14.md`, R-07, escalada E-1). El registro es
/// anónimo por diseño (`PRODUCT-INTAKE` 1.15 §4.1) y admitía cualquier correo. Retirar el
/// autorregistro rompía `F-02` y el punto público `A-02` de `/v1/`; acotarlo por dominio no rompe
/// ninguno de los dos (`Api ADR-00012`).
///
/// SIN DOMINIOS DECLARADOS NO HAY RESTRICCIÓN, y es deliberado: el producto no conoce el dominio
/// institucional de cada despliegue y no lo inventa. La llave es
/// `Registration__AdmittedEmailDomains__N` en la composición.
///
/// LA COMPARACIÓN ES EXACTA SOBRE EL DOMINIO ENTERO, sin distinguir mayúsculas: un subdominio no
/// entra por el dominio que lo contiene, y `frre.utn.edu.ar.example.com` no es `frre.utn.edu.ar`.
/// </remarks>
public sealed class RegistrationPolicy
{
    private readonly HashSet<string> _admittedDomains;

    /// <param name="admittedEmailDomains">Dominios admitidos, con o sin `@` adelante. Los vacíos se ignoran.</param>
    public RegistrationPolicy(IEnumerable<string?> admittedEmailDomains)
    {
        ArgumentNullException.ThrowIfNull(admittedEmailDomains);

        _admittedDomains = admittedEmailDomains
            .Select(domain => domain?.Trim().TrimStart('@').Trim())
            .Where(domain => !string.IsNullOrEmpty(domain))
            .Select(domain => domain!)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>La política de un despliegue que no declaró dominios: admite cualquier correo.</summary>
    public static RegistrationPolicy Unrestricted { get; } = new([]);

    /// <summary>Si el correo, ya normalizado, puede registrarse.</summary>
    public bool Admits(string? normalizedEmail)
    {
        if (_admittedDomains.Count == 0)
        {
            return true;
        }

        if (string.IsNullOrWhiteSpace(normalizedEmail))
        {
            return false;
        }

        var at = normalizedEmail.LastIndexOf('@');

        return at > 0 && _admittedDomains.Contains(normalizedEmail[(at + 1)..].Trim());
    }
}
