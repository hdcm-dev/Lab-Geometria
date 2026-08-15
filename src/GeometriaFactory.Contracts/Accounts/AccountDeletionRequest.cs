namespace GeometriaFactory.Contracts.Accounts;

/// <summary>
/// Solicitud de baja de una cuenta del punto `A-08` (`Contracts CU-02` FA-01).
/// </summary>
/// <remarks>
/// ES LA ÚNICA SOLICITUD DEL ENSAMBLADO CON UN CAMPO DE CONFIRMACIÓN, y es porque describe la
/// única operación destructiva del producto: la baja elimina la cuenta **y todos sus trabajos**,
/// en cualquier estado, y no se deshace (RN-07).
///
/// EL CONTRATO NO DECLARA NINGÚN CAMPO QUE PERMITA CONSERVAR LOS TRABAJOS: el arrastre es
/// invariante de dominio y no una opción del solicitante.
///
/// LA BAJA Y EL RESETEO SON OPERACIONES OPUESTAS Y NO SE CONFUNDEN POR SU FORMA: ésta exige la
/// confirmación escrita y elimina todo; la de reseteo no exige confirmación y **conserva la
/// cuenta y todos sus trabajos** (RN-12).
/// </remarks>
/// <param name="AccountId">Identidad de la cuenta destino.</param>
/// <param name="ConfirmationEmail">
/// El correo de la cuenta, **escrito por el administrador** como confirmación. Sin él la baja no
/// procede, y si no coincide tampoco.
/// </param>
public sealed record AccountDeletionRequest(Guid AccountId, string? ConfirmationEmail);
