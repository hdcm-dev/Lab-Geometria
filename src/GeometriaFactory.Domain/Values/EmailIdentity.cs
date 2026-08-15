namespace GeometriaFactory.Domain.Values;

/// <summary>
/// La forma normalizada del correo, que es la que decide la identidad de una cuenta.
/// </summary>
/// <remarks>
/// `Modelo-Datos-Logico.md` §2.1 declara las dos columnas —correo escrito y correo normalizado—
/// y `Infrastructure ADR-03` declara que la comparación se hace sobre la segunda, sostenida por
/// un índice único. Lo que ninguna fuente declara es LA FUNCIÓN, y acá va marcada como
/// PROPUESTA: recorte de espacios y mayúsculas invariantes de cultura.
///
/// Vive en el dominio y no en la infraestructura porque es la regla de identidad de INV-01, y
/// el dominio es quien la tiene que poder hacer cumplir sin depender del motor de persistencia.
/// El índice único de la infraestructura es la segunda defensa, no la primera.
/// </remarks>
public static class EmailIdentity
{
    /// <summary>Forma normalizada del correo escrito. Nunca se edita por separado.</summary>
    public static string Normalize(string? writtenEmail) =>
        (writtenEmail ?? string.Empty).Trim().ToUpperInvariant();
}
