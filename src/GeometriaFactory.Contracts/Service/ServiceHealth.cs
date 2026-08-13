namespace GeometriaFactory.Contracts.Service;

/// <summary>
/// Cuerpo de la respuesta del punto de salud `A-16`.
/// </summary>
/// <remarks>
/// APARTAMIENTO DECLARADO de `Plan-Etapa-A.md` §2.1, que no da ningún archivo a
/// `GeometriaFactory.Contracts` en la etapa `a`. El motivo es el riesgo `R-03` de su §7:
/// `US-29` exige que la salud devuelva «datos reales del servidor propio» y ninguna fuente
/// declara el tipo del cuerpo. Sin un tipo compartido, `GeometriaFactory.Web` tendría que
/// declarar el mismo concepto por segunda vez, contra el corolario 1 de
/// `Norma-De-Nomenclatura.md` §6.1 —un concepto, un nombre—.
/// El espacio de nombres `GeometriaFactory.Contracts.Service` ya está declarado para
/// «estado del servicio» por `Norma-De-Nomenclatura.md` §6.10.
///
/// Los tres miembros son PROPUESTA SIN BASE DECLARADA en cuanto a su elección; lo que sí
/// está declarado es lo que la respuesta NO puede llevar (`US-29` §3, tercer criterio):
/// ninguna dirección de servicio interno, ninguna ruta del almacén y ninguna traza.
/// El punto de control de la etapa `a` decide si estos tres son «los datos reales».
/// Nombres agregados al glosario de `Norma-De-Nomenclatura.md` §6.4 y §6.5 en su 1.5.
/// </remarks>
/// <param name="Ready">
/// Si la preparación del almacén terminó. Es el dato que hace visible el arranque en dos fases.
/// </param>
/// <param name="Version">Versión del ensamblado que está atendiendo.</param>
/// <param name="ServerTimeUtc">Momento del servidor en tiempo universal coordinado.</param>
public sealed record ServiceHealth(bool Ready, string Version, DateTimeOffset ServerTimeUtc);
