using System.Reflection;
using GeometriaFactory.Api.Endpoints;
using GeometriaFactory.Contracts.Errors;
using GeometriaFactory.Domain.Values;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// La cobertura de la traducción: qué condiciones del dominio llegan al borde con un código propio
/// y cuáles caen al genérico, **con el criterio que decide cuál corresponde**.
/// </summary>
/// <remarks>
/// POR QUÉ ESTA BATERÍA EXISTE. El barrido del 2026-08-31 encontró **quince** condiciones que el
/// dominio emite y que **ningún lugar de la capa API nombraba**: las quince caían al genérico y
/// respondían `500`. No era un hallazgo de dos códigos —que es como se había reportado— sino de
/// quince, y ninguno se había buscado: los seis conocidos hasta entonces habían aparecido de a uno,
/// tropezando con ellos al arreglar otra cosa.
///
/// EL CRITERIO NO ES LA ALCANZABILIDAD, Y ES LA DECISIÓN QUE ESTA BATERÍA SOSTIENE. Preguntar «¿se
/// puede llegar?» se contesta hoy leyendo los invocadores y **deja de valer mañana**, cuando alguien
/// agregue uno. La pregunta que se eligió es **de quién sería el defecto si se llegara**:
///
///   · **Del producto** —el llamador pasó algo que no correspondía—: `500` es la respuesta
///     correcta, y son las doce que esta batería declara.
///   · **Del pedido** —la persona pidió algo que su papel o el estado del trabajo no admite—:
///     `500` le diría que el producto falló cuando lo que pasa es que su pedido no procede.
///
/// LAS DOCE SE ENUMERAN Y NO SE CUENTAN. Una prueba que dijera «doce caen al genérico» pasaría igual
/// con doce distintas; enumerarlas hace que agregar una decimotercera **rompa la prueba** y obligue
/// a clasificarla con el mismo criterio.
/// </remarks>
public sealed class ContractCoverageTests
{
    /// <summary>
    /// Las doce cuyo `500` es correcto: si se alcanzaran, el defecto sería del producto.
    /// </summary>
    private static readonly string[] DefectoDelProducto =
    [
        ConditionCode.AdministratorRoleOutsideThisPath,
        ConditionCode.CredentialNotAllowedOnRegistration,
        ConditionCode.DeletionWithoutWorkCascade,
        ConditionCode.EmailUniquenessNotVerified,
        ConditionCode.ErrorWithoutLocation,
        ConditionCode.InitialStatusNotNegotiable,
        ConditionCode.ObservationOnMissingPiece,
        ConditionCode.ResetWithWorkCascade,
        ConditionCode.SubmissionWithoutParseResult,
        ConditionCode.UnknownObservationKind,
        ConditionCode.UnknownOperation,
        ConditionCode.WarningMissingBothValues,
    ];

    [Theory]
    [InlineData(ConditionCode.ScopeRequiresAdministratorRole, ErrorCode.OperationAdminOnly, StatusCodes.Status403Forbidden)]
    [InlineData(ConditionCode.EditOutsideDraft, ErrorCode.StateForbidsUpdate, StatusCodes.Status409Conflict)]
    [InlineData(ConditionCode.SubmissionOutsideDraft, ErrorCode.StateForbidsUpdate, StatusCodes.Status409Conflict)]
    public void Las_tres_que_hablan_del_pedido_no_caen_al_generico(
        string condicion, string codigoEsperado, int estadoEsperado)
    {
        var t = ContractTranslation.Translate(condicion);

        Assert.Equal(codigoEsperado, t.Code);
        Assert.Equal(estadoEsperado, t.StatusCode);
    }

    [Fact]
    public void Las_doce_cuyo_defecto_seria_del_producto_caen_al_generico_con_500()
    {
        foreach (var condicion in DefectoDelProducto)
        {
            var t = ContractTranslation.Translate(condicion);

            Assert.Equal(ErrorCode.UnclassifiedError, t.Code);
            Assert.Equal(StatusCodes.Status500InternalServerError, t.StatusCode);
        }
    }

    /// <summary>
    /// Las que la traducción manda al genérico **a propósito**, y no por omisión: su fila existe,
    /// con el destino que su fundamento pide.
    /// </summary>
    private static readonly string[] GenericoDeliberado =
    [
        ConditionCode.AccountTransitionNotAllowed,
        ConditionCode.EnableWithoutTemporaryCredential,
        ConditionCode.OperationNotApplicableToAdministratorAccount,
        ConditionCode.WorkWithoutOwner,
        ConditionCode.OriginalJsonAltered,
    ];

    /// <summary>Las que **el punto de acceso** traduce por su cuenta, sin pasar por el conmutador.</summary>
    private static readonly string[] TratadasEnElPunto =
    [
        ConditionCode.OperationOutsideDraft,
        ConditionCode.TransitionFromTerminalStatus,
        ConditionCode.OutcomeOutsideSubmitted,
    ];

    /// <summary>Declarada y que **ninguna operación emite**. Ver el barrido del 2026-08-31.</summary>
    private static readonly string[] SinUso = [ConditionCode.CredentialAlreadySet];

    [Fact]
    public void El_catalogo_del_dominio_queda_particionado_y_ninguna_condicion_cae_por_omision()
    {
        // ESTE CONTROL ES EL QUE HACE QUE LA BATERÍA SIRVA, y su primera versión estaba mal: filtraba
        // por «traduce al genérico», y `UNCLASSIFIED_ERROR` es un **código legítimo del contrato**
        // que cinco condiciones usan a propósito, con destino `409` o `503`. Confundía «cae por
        // omisión» con «traduce al genérico», y levantó nueve falsos positivos la primera vez que
        // corrió. Lo destapó correrlo, que es la única forma.
        //
        // LA PARTICIÓN ES DE CUATRO Y NO DE DOS, porque el árbol tiene cuatro casos reales: con
        // código propio, genérico deliberado, tratada en el punto, y declarada sin uso.
        var catalogo = typeof(ConditionCode)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(f => f.IsLiteral && f.FieldType == typeof(string))
            .Select(f => (string)f.GetRawConstantValue()!)
            .ToArray();

        var declaradas = DefectoDelProducto
            .Concat(GenericoDeliberado)
            .Concat(TratadasEnElPunto)
            .Concat(SinUso)
            .ToHashSet(StringComparer.Ordinal);

        var sinClasificar = catalogo
            .Where(c => ContractTranslation.Translate(c).Code == ErrorCode.UnclassifiedError)
            .Where(c => !declaradas.Contains(c))
            .ToArray();

        Assert.True(
            sinClasificar.Length == 0,
            "Estas condiciones no tienen código propio y no están declaradas en ninguna de las "
            + "cuatro listas. Decidí de quién sería el defecto si se alcanzaran y agregalas donde "
            + "corresponda: " + string.Join(", ", sinClasificar));
    }

    [Fact]
    public void Las_cuatro_listas_no_se_solapan()
    {
        // Sin esto, una condición podría estar en dos listas y la partición dejaría de serlo: el
        // recuento cerraría y la clasificación no diría nada.
        var todas = DefectoDelProducto
            .Concat(GenericoDeliberado)
            .Concat(TratadasEnElPunto)
            .Concat(SinUso)
            .ToArray();

        Assert.Equal(todas.Length, todas.Distinct(StringComparer.Ordinal).Count());
    }
}
