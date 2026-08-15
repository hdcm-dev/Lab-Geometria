using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Application.Accounts;

/// <summary>
/// Lo que un caso de uso devuelve cuando el consumidor necesita ver una cuenta entera: su
/// identidad, su situación y su marca.
/// </summary>
/// <remarks>
/// ES EL COMPLEMENTO DE <see cref="AccountIdentity"/>, NO SU REEMPLAZO. Aquél existe para el
/// canje y para el alta, donde lo único que el consumidor necesita es identidad y papel; éste
/// existe para el **listado del administrador** (`Api CU-04` `A-06`) y para el **resultado del
/// registro** (`Contracts CU-02` §4 paso 2), donde la situación es precisamente el dato.
///
/// LO QUE NO DECLARA ES LO QUE IMPORTA: **0 campos con la credencial derivada**, en cualquiera
/// de sus formas (`Contracts CU-02` CA-05; `Api CU-04` CA-01). Que el tipo no la declare es lo
/// que lo vuelve imposible, en lugar de dejarlo librado a que nadie la copie.
///
/// LA MARCA SÍ VIAJA, Y NO ES UNA CONTRASEÑA. Es lo que le permite al administrador ver que una
/// cuenta que él habilitó o reseteó todavía no cambió su provisoria, **sin conocer ningún valor**
/// (`Api CU-04` §10).
/// </remarks>
/// <param name="Id">Identidad propia de la cuenta.</param>
/// <param name="Email">Correo escrito, tal como la persona lo escribió.</param>
/// <param name="FirstName">Nombre declarado en el alta.</param>
/// <param name="LastName">Apellido declarado en el alta.</param>
/// <param name="Role">Papel de la cuenta.</param>
/// <param name="Status">Situación de la cuenta.</param>
/// <param name="MustChangePassword">Marca de cambio de contraseña pendiente (INV-09).</param>
/// <param name="CreatedAt">Momento de alta, en tiempo universal coordinado.</param>
public sealed record AccountSnapshot(
    Guid Id,
    string Email,
    string FirstName,
    string LastName,
    Role Role,
    AccountStatus Status,
    bool MustChangePassword,
    DateTimeOffset CreatedAt)
{
    /// <summary>Toma la foto de una cuenta, dejando afuera su credencial derivada.</summary>
    public static AccountSnapshot Of(Domain.Entities.Account account)
    {
        ArgumentNullException.ThrowIfNull(account);

        return new AccountSnapshot(
            account.Id,
            account.Email,
            account.FirstName,
            account.LastName,
            account.Role,
            account.Status,
            account.MustChangePassword,
            account.CreatedAt);
    }
}
