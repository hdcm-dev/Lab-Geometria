using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Application.Accounts;

/// <summary>
/// Lo que un caso de uso de cuentas devuelve hacia afuera: identidad y papel, y nada más.
/// </summary>
/// <remarks>
/// NO lleva la credencial derivada, ni la marca, ni el estado. Es la contracara de la regla de
/// `GeometriaFactory-Contracts`: ningún dato de cuenta que cruce hacia afuera transporta la
/// forma almacenada de la contraseña. Que el tipo no la declare es lo que lo vuelve imposible,
/// en lugar de dejarlo librado a que nadie la copie.
/// </remarks>
/// <param name="Id">Identidad propia de la cuenta.</param>
/// <param name="Email">Correo escrito, tal como la persona lo escribió.</param>
/// <param name="Role">Papel de la cuenta.</param>
public sealed record AccountIdentity(Guid Id, string Email, Role Role);
