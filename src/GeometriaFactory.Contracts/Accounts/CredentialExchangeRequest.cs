namespace GeometriaFactory.Contracts.Accounts;

/// <summary>
/// Solicitud de canje de credenciales del punto `A-01` (`Contracts CU-01` §4 paso 1).
/// </summary>
/// <remarks>
/// DOS CAMPOS. Viaja servidor a servidor, de la pieza pública a la pieza de datos: el navegador
/// nunca la construye ni la ve (RA-01). Que la contraseña viaje en claro por ese tramo es la
/// decisión de autenticación del producto, registrada como consciente y con su riesgo `R-02`
/// aceptado por escrito (intake §17.5.P.5).
/// </remarks>
public sealed record CredentialExchangeRequest(string? Email, string? Password);
