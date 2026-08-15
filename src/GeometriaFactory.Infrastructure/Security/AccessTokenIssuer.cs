using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace GeometriaFactory.Infrastructure.Security;

/// <summary>
/// CU-08 — Emite el acceso firmado con el que la pieza pública opera contra la pieza de datos.
/// </summary>
/// <remarks>
/// CUATRO RECLAMOS Y NINGUNO POR DEFECTO (`ADR-04` §2 punto 4): identificador, correo, papel y
/// expiración. Uno sin papel dejaría a las capas de adentro decidiendo sobre un dato que nadie
/// declaró; uno sin expiración no vencería nunca.
///
/// ESTE CONTRATO NO DECIDE SI LA CUENTA ADMITE EL ACCESO: llega resuelto por el dominio. Una
/// cuenta `Pending`, `Blocked` o con la marca puesta no llega hasta acá.
///
/// NO SOSTIENE SESIÓN: el acceso no tiene estado de este lado. Quien lo conserva es el circuito
/// de la pieza pública, del lado de su servidor.
/// </remarks>
public sealed class AccessTokenIssuer
{
    /// <summary>Reclamo del papel. El mismo nombre que la pieza de datos exige al verificar.</summary>
    public const string RoleClaim = ClaimTypes.Role;

    private readonly SigningOptions _options;

    public AccessTokenIssuer(SigningOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        _options = options;
    }

    /// <summary>
    /// Si hay clave de firma provista. Sin clave no se emite y no se verifica.
    /// </summary>
    /// <remarks>
    /// UNA CLAVE MÁS CORTA QUE EL RESUMEN CUENTA COMO AUSENTE. HS256 firma con un resumen de 32
    /// bytes: rellenar una clave corta para que entre sería exactamente el atajo que `ADR-04` §2
    /// punto 3 prohíbe —el sistema arranca, emite accesos y nadie lo nota—. Se prefiere no
    /// arrancar. El mínimo es PROPUESTA de la etapa `c`: ninguna fuente da longitud.
    /// </remarks>
    public bool SigningKeyIsProvided =>
        !string.IsNullOrWhiteSpace(_options.SigningKey)
        && Encoding.UTF8.GetByteCount(_options.SigningKey) >= MinimumSigningKeySizeInBytes;

    /// <summary>Longitud mínima de la clave de firma, en bytes. Es la del resumen de HS256.</summary>
    public const int MinimumSigningKeySizeInBytes = 32;

    /// <summary>Parámetros con los que la pieza de datos verifica un acceso recibido.</summary>
    public TokenValidationParameters ValidationParameters => new()
    {
        ValidateIssuer = true,
        ValidIssuer = _options.Issuer,
        ValidateAudience = true,
        ValidAudience = _options.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = SecurityKey(),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromSeconds(30),
        RoleClaimType = RoleClaim,
        NameClaimType = JwtRegisteredClaimNames.Email,
    };

    /// <summary>
    /// Emite el acceso para una cuenta ya admitida. Devuelve nulo cuando falta la clave de firma
    /// o cuando falta alguno de los cuatro reclamos: **no se emite un acceso sin firma ni con una
    /// clave generada al vuelo**, y ningún reclamo se completa con un valor por defecto.
    /// </summary>
    public string? Issue(Guid accountId, string? email, string? role, DateTimeOffset issuedAt)
    {
        if (!SigningKeyIsProvided
            || accountId == Guid.Empty
            || string.IsNullOrWhiteSpace(email)
            || string.IsNullOrWhiteSpace(role)
            || _options.LifetimeInMinutes < 1)
        {
            return null;
        }

        var descriptor = new SecurityTokenDescriptor
        {
            Issuer = _options.Issuer,
            Audience = _options.Audience,
            IssuedAt = issuedAt.UtcDateTime,
            NotBefore = issuedAt.UtcDateTime,
            Expires = issuedAt.UtcDateTime.AddMinutes(_options.LifetimeInMinutes),
            Subject = new ClaimsIdentity(
            [
                new Claim(JwtRegisteredClaimNames.Sub, accountId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(RoleClaim, role),
            ]),
            SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256),
        };

        return new JsonWebTokenHandler().CreateToken(descriptor);
    }

    private SymmetricSecurityKey SecurityKey() =>
        new(Encoding.UTF8.GetBytes(
            SigningKeyIsProvided ? _options.SigningKey : new string('\0', MinimumSigningKeySizeInBytes)));
}
