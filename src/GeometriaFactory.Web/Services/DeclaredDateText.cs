using System.Globalization;

namespace GeometriaFactory.Web.Services;

/// <summary>
/// Cómo se LEE la fecha que el alumno declaró. **No la convierte: sólo la presenta.**
/// </summary>
/// <remarks>
/// EL PRODUCTO GUARDA LO QUE LA PERSONA DECLARÓ Y NO LO REESCRIBE (`RC-06`), de modo que en el
/// almacén conviven dos formas: la que entregó el control de calendario —`aaaa-mm-dd`, desde la
/// etapa `g`— y la que se escribió a mano antes. Esta clase existe para que la pantalla las lea
/// las dos **sin tocar el dato**.
///
/// LO QUE NO RECONOCE, LO MUESTRA TAL CUAL. Es la regla que importa: ante una fecha que no entiende,
/// la alternativa sería esconderla o inventarle una interpretación, y las dos le mentirían a quien
/// la escribió.
///
/// Y NO HAY ZONA HORARIA ACÁ. Es un día declarado, no un instante: convertirlo sería el defecto que
/// `RC-06` existe para impedir.
/// </remarks>
public static class DeclaredDateText
{
    /// <summary>La fecha declarada, lista para leer.</summary>
    public static string ToDisplay(string? declaredDate)
    {
        if (string.IsNullOrWhiteSpace(declaredDate))
        {
            return string.Empty;
        }

        return DateOnly.TryParseExact(
            declaredDate.Trim(),
            "yyyy-MM-dd",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out var day)
            ? day.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
            : declaredDate;
    }
}
