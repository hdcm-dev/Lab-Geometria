namespace GeometriaFactory.Contracts.Accounts;

/// <summary>
/// Solicitud de cambio de la contraseña propia del punto `A-05` (`Contracts CU-02` FA-02).
/// </summary>
/// <remarks>
/// LA VIGENTE ES OBLIGATORIA POR CONTRATO. Es **un solo tipo para las tres situaciones** —el
/// cambio voluntario, el primer ingreso y el cambio posterior a un reseteo—, que es la decisión
/// de `PRODUCT-INTAKE` 1.13: no existe en el ensamblado ningún tipo que acepte una contraseña
/// nueva sin la vigente, y ésa es la forma que tiene RN-16 de ser imposible de violar por olvido.
///
/// EL CORREO ES EL TERCER CAMPO Y SIRVE A UNA SOLA COSA: identificar la cuenta **cuando no hay
/// sesión de trabajo**. Lo agrega `PRODUCT-INTAKE` **1.34**, que declara que la operación admite
/// **dos formas de autenticarse** —con sesión de trabajo, el cambio corriente; con la contraseña
/// actual, el cambio forzado, donde la provisoria que el administrador comunicó es la que
/// autentica—. Sin este campo, la cuenta con la marca de cambio pendiente **no tenía forma de
/// nombrarse**: RN-13 le niega la sesión de trabajo, con lo cual la pantalla del cambio forzado
/// quedaba inalcanzable.
///
/// **No afloja RN-13 y no abre una escritura de contraseña sin credencial**: la contraseña
/// vigente sigue siendo obligatoria en las dos formas, y el correo por sí solo no cambia nada.
/// Con sesión, el correo se ignora: la cuenta que cambia es la del acceso firmado y nunca la que
/// el cuerpo nombre.
/// </remarks>
/// <param name="CurrentPassword">Contraseña vigente. Obligatoria en las dos formas.</param>
/// <param name="NewPassword">Contraseña nueva, elegida por la propia persona.</param>
/// <param name="Email">
/// Correo de la cuenta que cambia. **Sólo se usa en la forma sin sesión**, y por eso es opcional:
/// la petición que llega con acceso firmado no lo necesita y no lo mira.
/// </param>
public sealed record OwnPasswordChangeRequest(
    string? CurrentPassword,
    string? NewPassword,
    string? Email = null);
