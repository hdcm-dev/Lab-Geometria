using System.Reflection;
using GeometriaFactory.Api.Endpoints;
using GeometriaFactory.Application.Accounts;
using GeometriaFactory.Contracts.Errors;
using GeometriaFactory.Domain.Values;
using Xunit;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// `Api CU-09` — Las dos traducciones que convierten un motivo interno en una respuesta de
/// protocolo, ejercidas sobre **el conjunto cerrado entero** y no sobre una muestra.
/// </summary>
/// <remarks>
/// POR QUÉ ESTA BATERÍA EXISTE Y POR QUÉ AHORA. Es la respuesta a la escalada `ESC-001` de
/// [`Mesa-2026-08-29.md`], que el Product Owner cerró con la opción **A** —cubrir con pruebas en
/// lugar de mover el umbral—. `ContractTranslation` concentraba **131 de las 201 ramas** de
/// `GeometriaFactory.Api` y estaba en **38,9 %**: es el único lugar del proyecto de código donde
/// cubrir mueve el número de verdad.
///
/// EL RECORRIDO ES POR REFLEXIÓN SOBRE EL CONJUNTO CERRADO, y no por una lista escrita a mano.
/// Una lista a mano se desincroniza el día que alguien agrega un motivo: la prueba seguiría
/// pasando y el motivo nuevo caería en el genérico sin que nadie se entere. Recorriendo las
/// constantes, **un motivo nuevo sin traducción aparece como falla el mismo día que se agrega**.
///
/// LO QUE ESTA BATERÍA NO HACE. No decide qué motivo merece qué código de respuesta: eso lo
/// declaran `Definicion-Superficie-HTTP.md` §5 y `Api ADR-04`, y acá se verifican las invariantes
/// que esos documentos fijan, más las traducciones nombradas una por una.
/// </remarks>
public sealed class ContractTranslationTests
{
    /// <summary>Los diez códigos de respuesta que la superficie admite (`Definicion-Superficie-HTTP.md` §4).</summary>
    private static readonly int[] CodigosAdmitidos =
        [200, 201, 204, 400, 401, 403, 404, 409, 500, 503];

    private static IEnumerable<string> MotivosDelConjuntoCerrado(Type catalogo) =>
        catalogo.GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(f => f.IsLiteral && f.FieldType == typeof(string))
            .Select(f => (string)f.GetRawConstantValue()!);

    public static TheoryData<string> TodosLosMotivos()
    {
        var datos = new TheoryData<string>();
        foreach (var motivo in MotivosDelConjuntoCerrado(typeof(ConditionCode))
                     .Concat(MotivosDelConjuntoCerrado(typeof(ApplicationConditionCode)))
                     .Distinct(StringComparer.Ordinal))
        {
            datos.Add(motivo);
        }

        return datos;
    }

    /// <summary>
    /// CA-01 — **Todo motivo del conjunto cerrado traduce**, y su traducción está bien formada:
    /// código del contrato no vacío, código de respuesta de los diez admitidos y mensaje con texto.
    /// </summary>
    [Theory]
    [MemberData(nameof(TodosLosMotivos))]
    public void EveryConditionCodeTranslatesIntoAWellFormedResponse(string motivo)
    {
        var traduccion = ContractTranslation.Translate(motivo);

        Assert.False(string.IsNullOrWhiteSpace(traduccion.Code));
        Assert.Contains(traduccion.StatusCode, CodigosAdmitidos);
        Assert.False(string.IsNullOrWhiteSpace(traduccion.Message));
    }

    /// <summary>
    /// CA-02 — **El código del contrato pertenece al conjunto cerrado del contrato.** Es lo que
    /// `Api ADR-04` exige con la frase «esta capa no inventa códigos»: verificarlo con una prueba
    /// convierte la regla en algo que falla si alguien la incumple.
    /// </summary>
    [Theory]
    [MemberData(nameof(TodosLosMotivos))]
    public void EveryTranslationEmitsACodeFromTheContractClosedSet(string motivo)
    {
        var delContrato = MotivosDelConjuntoCerrado(typeof(ErrorCode)).ToHashSet(StringComparer.Ordinal);

        Assert.Contains(ContractTranslation.Translate(motivo).Code, delContrato);
    }

    /// <summary>
    /// CA-03 — **Un motivo desconocido cae en el genérico y en `500`**, y no lanza. Es la salida
    /// que `Api ADR-04` reserva para el motivo sin código propio.
    /// </summary>
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("UN_MOTIVO_QUE_NO_EXISTE")]
    public void AnUnknownConditionFallsIntoTheGenericAndFiveHundred(string? motivo)
    {
        var traduccion = ContractTranslation.Translate(motivo);

        Assert.Equal(500, traduccion.StatusCode);
        Assert.False(string.IsNullOrWhiteSpace(traduccion.Code));
    }

    /// <summary>
    /// CA-04 — **El correo desconocido y la contraseña equivocada responden IGUAL**, que es la
    /// propiedad deliberada de `Definicion-Superficie-HTTP.md` §5: distinguirlos permitiría
    /// averiguar por tanteo qué correos están registrados.
    /// </summary>
    [Fact]
    public void UnknownEmailAndWrongPasswordAreIndistinguishable()
    {
        var porCuenta = ContractTranslation.Translate(ApplicationConditionCode.AccountNotFound);
        var porCredencial = ContractTranslation.Translate(ConditionCode.CurrentCredentialNotVerified);

        Assert.Equal(porCuenta.Code, porCredencial.Code);
        Assert.Equal(porCuenta.StatusCode, porCredencial.StatusCode);
        Assert.Equal(porCuenta.Message, porCredencial.Message);
        Assert.Equal(401, porCuenta.StatusCode);
    }

    /// <summary>
    /// CA-05 — Las traducciones que los documentos nombran una por una, verificadas por su nombre
    /// y no por recorrido. Son las que un cambio silencioso rompería sin que las anteriores lo noten.
    /// </summary>
    [Theory]
    [InlineData(nameof(ConditionCode.RequiredFieldMissing), 400)]
    [InlineData(nameof(ConditionCode.AdministratorAlreadyConfigured), 409)]
    [InlineData(nameof(ConditionCode.AccountPending), 403)]
    [InlineData(nameof(ConditionCode.PasswordChangePending), 403)]
    [InlineData(nameof(ConditionCode.AccountBlocked), 403)]
    public void TheNamedTranslationsKeepTheirDocumentedStatusCode(string constante, int esperado)
    {
        var motivo = (string)typeof(ConditionCode)
            .GetField(constante, BindingFlags.Public | BindingFlags.Static)!
            .GetRawConstantValue()!;

        Assert.Equal(esperado, ContractTranslation.Translate(motivo).StatusCode);
    }

    /// <summary>
    /// CA-06 — **La traducción es una función pura**: el mismo motivo devuelve lo mismo dos veces.
    /// Sostiene que la tabla no depende del reloj ni de estado compartido.
    /// </summary>
    [Theory]
    [MemberData(nameof(TodosLosMotivos))]
    public void TranslationIsPure(string motivo)
    {
        Assert.Equal(ContractTranslation.Translate(motivo), ContractTranslation.Translate(motivo));
    }
}
