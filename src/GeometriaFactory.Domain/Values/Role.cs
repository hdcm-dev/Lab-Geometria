namespace GeometriaFactory.Domain.Values;

/// <summary>
/// Papel de una cuenta. Conjunto cerrado de dos valores.
/// </summary>
/// <remarks>
/// Los valores se guardan y se serializan POR SU NOMBRE, nunca por su posición
/// (`Contratos-REST.md` §2.2). La etiqueta que ve la persona la compone
/// `GeometriaFactory.Web.Services` y va en castellano (`Norma-De-Nomenclatura.md` §5.2, control `V-3`).
/// Identificadores y etiquetas: `Norma-De-Nomenclatura.md` §6.7.
/// </remarks>
public enum Role
{
    /// <summary>Etiqueta: «Alumno».</summary>
    Student = 1,

    /// <summary>Etiqueta: «Administrador».</summary>
    Administrator = 2
}
