namespace GeometriaFactory.Contracts.Accounts;

/// <summary>
/// Respuesta de sesión del punto `A-01` (`Contracts CU-01` §4 paso 4).
/// </summary>
/// <remarks>
/// CUATRO CAMPOS Y NINGUNO MÁS (`Contracts CU-01` CA-01 y CA-06): credencial de sesión,
/// identificador, correo y papel. En particular **no declara ningún campo de cambio pendiente**:
/// una cuenta marcada no obtiene respuesta de sesión, obtiene el desvío, de modo que un quinto
/// campo describiría una respuesta que no existe.
///
/// Y no declara ninguna forma almacenada de la contraseña, ninguna clave de firma y ninguna
/// dirección de servicio interno: 0 campos de esas tres clases.
///
/// LA CREDENCIAL DE SESIÓN QUEDA EN EL SERVIDOR DE LA PIEZA PÚBLICA. Este tipo lo consume el
/// servidor del front, no el navegador.
/// </remarks>
public sealed record SessionResponse(string AccessToken, Guid AccountId, string Email, string Role);
