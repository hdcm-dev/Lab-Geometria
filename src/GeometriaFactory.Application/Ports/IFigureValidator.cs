namespace GeometriaFactory.Application.Ports;

/// <summary>
/// Validación de figuras. Puerto DECLARADO por el intake §17.2.P.1.
/// </summary>
/// <remarks>
/// ETAPA `a`: el puerto se DECLARA y sus miembros NO se escriben. La etapa `a` no valida JSON
/// ni verifica valores declarados contra derivados (`Plan-Etapa-A.md` §6).
/// Su adaptador es `LocalFigureValidator`, motor propio y SIN RED (`Infrastructure BT-16`, etapa `f`).
/// </remarks>
public interface IFigureValidator
{
}
