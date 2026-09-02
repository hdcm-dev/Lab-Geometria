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

    /// <summary>
    /// El mismo día, con la forma que el control nativo de fecha sabe recibir.
    /// </summary>
    /// <remarks>
    /// ES LA INVERSA DE <see cref="ToDisplay"/>, Y SU AUSENCIA LE COSTABA LA FECHA AL ALUMNO.
    /// `&lt;input type="date"&gt;` acepta **un solo formato**, `aaaa-mm-dd`, y descarta en silencio
    /// cualquier otro: no avisa, no marca error, simplemente **queda vacío**. Hasta el 2026-09-02
    /// la reedición le pasaba al control el valor guardado tal cual, de modo que un borrador con
    /// la forma vieja —`30/08/2026`, que es lo que este producto guardaba antes— abría el
    /// formulario **sin fecha**.
    ///
    /// Y LO PEOR NO ERA PERDERLA, ERA LA ACUSACION. El alumno apretaba «Enviar» y la pantalla le
    /// decía que le faltaba la fecha, cuando él la había declarado y seguía viéndose en
    /// `/mis-trabajos` y en la vista del trabajo. Desde el formulario ya no podía leer cuál era.
    /// Medido por el peritaje del 2026-09-02:
    ///
    ///     FALLA 4. La reedicion trae la fecha declarada TAL COMO QUEDO
    ///              fecha en el control="" · el dato guardado es "30/08/2026"
    ///
    /// SE CONVIERTE PARA MOSTRAR, NO PARA GUARDAR. Lo guardado no se toca —sigue siendo lo que la
    /// persona declaró—; esto es sólo la forma que el control necesita para poder mostrarlo.
    ///
    /// DEVUELVE VACIO CUANDO NO PUEDE, y no inventa un día. Un día inventado sería peor que uno
    /// ausente: el alumno enviaría una fecha que nunca declaró sin enterarse.
    /// </remarks>
    public static string ToControlValue(string? declaredDate)
    {
        if (string.IsNullOrWhiteSpace(declaredDate))
        {
            return string.Empty;
        }

        var texto = declaredDate.Trim();

        // Ya viene con la forma que el control quiere.
        if (DateOnly.TryParseExact(texto, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
        {
            return texto;
        }

        // LAS DOS FORMAS VIEJAS QUE ESTE PRODUCTO LLEGO A GUARDAR, y ninguna más: convertir de
        // cualquier formato con la cultura del servidor haría que `03/04/2026` signifique cosas
        // distintas según dónde corra, que es exactamente el defecto que el control nativo vino
        // a cerrar.
        foreach (var forma in new[] { "dd/MM/yyyy", "d/M/yyyy" })
        {
            if (DateOnly.TryParseExact(texto, forma, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dia))
            {
                return dia.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
        }

        return string.Empty;
    }
}
