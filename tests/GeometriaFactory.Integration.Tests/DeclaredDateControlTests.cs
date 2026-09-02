using Xunit;
using GeometriaFactory.Web.Services;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// La fecha declarada, ida y vuelta entre lo guardado y el control nativo.
/// </summary>
/// <remarks>
/// POR QUÉ EXISTE ESTA BATERÍA, Y QUÉ COSTÓ NO TENERLA. El peritaje del 2026-09-02 reprodujo esto
/// sobre la publicación real:
///
///     FALLA 4. La reedicion trae la fecha declarada TAL COMO QUEDO
///              fecha en el control="" · el dato guardado es "30/08/2026"
///
/// `&lt;input type="date"&gt;` acepta **un solo formato** y descarta cualquier otro EN SILENCIO. Un
/// borrador con la forma vieja abría el formulario sin fecha, el alumno apretaba «Enviar», y la
/// pantalla le decía que le faltaba **la fecha que él había declarado** —y que seguía viéndose en
/// su panel—. Desde el formulario ya no podía leer cuál era.
///
/// LO QUE ESTAS PRUEBAS FIJAN NO ES LA CONVERSIÓN: ES QUE NO SE INVENTE UN DÍA. Convertir era la
/// parte fácil; lo que no se puede negociar es que ante un valor ilegible el producto devuelva
/// **vacío** y la pantalla muestre lo declarado, en vez de completar con un día que nadie eligió.
/// </remarks>
public sealed class DeclaredDateControlTests
{
    [Theory]
    [InlineData("2026-08-30", "2026-08-30")] // ya viene con la forma del control
    [InlineData("30/08/2026", "2026-08-30")] // la forma vieja de este producto
    [InlineData("3/4/2026", "2026-04-03")]   // la forma vieja, sin ceros a la izquierda
    [InlineData(" 30/08/2026 ", "2026-08-30")]
    public void ConvierteLoQuePuedeALaFormaDelControl(string guardado, string esperado) =>
        Assert.Equal(esperado, DeclaredDateText.ToControlValue(guardado));

    /// <summary>
    /// EL CASO QUE IMPORTA: ante lo ilegible se devuelve vacío, y NO se inventa un día.
    /// </summary>
    /// <remarks>
    /// Un día inventado sería peor que uno ausente: el alumno enviaría una fecha que nunca declaró
    /// sin enterarse, y el trabajo quedaría con un dato falso que nadie escribió.
    /// </remarks>
    [Theory]
    [InlineData("el martes pasado")]
    [InlineData("2026-13-45")]
    [InlineData("30-08-2026")]
    [InlineData("")]
    [InlineData(null)]
    public void AnteLoIlegibleDevuelveVacioYNoInventaUnDia(string? guardado) =>
        Assert.Equal(string.Empty, DeclaredDateText.ToControlValue(guardado));

    /// <summary>
    /// La ida y la vuelta no se contradicen: lo que el control recibe, mostrado, es lo mismo.
    /// </summary>
    /// <remarks>
    /// ES LA PROPIEDAD QUE FALTABA, y la que hace que el alumno vea siempre el mismo día en las
    /// tres superficies —su panel, la vista del trabajo y el formulario de reedición—. Que las dos
    /// funciones vivan juntas en el mismo archivo es lo que impide que una diga una cosa y la otra
    /// otra.
    /// </remarks>
    [Theory]
    [InlineData("2026-08-30")]
    [InlineData("30/08/2026")]
    public void LaIdaYLaVueltaMuestranElMismoDia(string guardado)
    {
        var paraElControl = DeclaredDateText.ToControlValue(guardado);

        Assert.Equal(DeclaredDateText.ToDisplay(guardado), DeclaredDateText.ToDisplay(paraElControl));
    }
}
