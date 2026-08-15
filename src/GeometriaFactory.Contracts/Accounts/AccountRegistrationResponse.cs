namespace GeometriaFactory.Contracts.Accounts;

/// <summary>
/// Resultado del registro de una cuenta de alumno del punto `A-02` (`Contracts CU-02` §4 paso 2).
/// </summary>
/// <remarks>
/// LO QUE DECLARA ES **LA SITUACIÓN INICIAL DE LA CUENTA**, que es el dato con el que la pieza
/// pública le dice a la persona que su cuenta quedó pendiente de habilitación. Sin ese dato, el
/// aviso explícito que `RN-06` exige quedaría librado a que la pantalla lo supiera de memoria.
///
/// NO DEVUELVE CREDENCIAL DE SESIÓN NI NINGUNA FORMA DE CONTRASEÑA: registrarse no es entrar, y
/// la cuenta recién registrada **todavía no obtiene acceso** (RN-06, `Api CU-03` §7).
/// </remarks>
/// <param name="AccountId">Identidad propia de la cuenta recién constituida.</param>
/// <param name="Email">Correo escrito, tal como la persona lo escribió.</param>
/// <param name="Status">Situación inicial de la cuenta, por su nombre. Siempre es la pendiente.</param>
public sealed record AccountRegistrationResponse(Guid AccountId, string Email, string Status);
