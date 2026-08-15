namespace GeometriaFactory.Contracts.Accounts;

/// <summary>
/// Resultado de la configuración de la cuenta de administrador del punto `A-03`.
/// </summary>
/// <remarks>
/// TRES CAMPOS: identificador, correo y papel. **No devuelve credencial de sesión**: configurar
/// no es entrar. El guion de la etapa `c` entra por `A-01` inmediatamente después, y la maqueta
/// dice lo mismo con sus palabras — «La cuenta de administrador quedó creada. Ahora entrá con
/// ella» —, con el lazo cerrado por la superficie siguiente y no por ésta.
/// </remarks>
public sealed record AccountSetupResponse(Guid AccountId, string Email, string Role);
