namespace GeometriaFactory.Contracts.Accounts;

/// <summary>
/// Solicitud de cambio de la contraseña propia del punto `A-05` (`Contracts CU-02` FA-02).
/// </summary>
/// <remarks>
/// DOS CAMPOS, Y LA VIGENTE ES OBLIGATORIA POR CONTRATO. Es **un solo tipo para las tres
/// situaciones** —el cambio voluntario, el primer ingreso y el cambio posterior a un reseteo—,
/// que es la decisión de `PRODUCT-INTAKE` 1.13: no existe en el ensamblado ningún tipo que
/// acepte una contraseña nueva sin la vigente, y ésa es la forma que tiene RN-16 de ser
/// imposible de violar por olvido.
/// </remarks>
public sealed record OwnPasswordChangeRequest(string? CurrentPassword, string? NewPassword);
