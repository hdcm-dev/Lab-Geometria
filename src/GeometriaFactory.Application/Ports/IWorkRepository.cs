namespace GeometriaFactory.Application.Ports;

/// <summary>
/// Repositorio de trabajos. Puerto DECLARADO por el intake §13, §14 y §17.2.P.1.
/// </summary>
/// <remarks>
/// ETAPA `a`: el puerto se DECLARA y sus miembros NO se escriben, por el mismo motivo que
/// `IAccountRepository`: transportan atributos de `Work`, que es de la etapa `c`.
/// Su adaptador es `EfCoreWorkRepository` (`Infrastructure BT-10`, etapa `e`).
/// </remarks>
public interface IWorkRepository
{
}
