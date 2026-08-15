namespace GeometriaFactory.Contracts.Accounts;

/// <summary>
/// Solicitud de configuración de la cuenta de administrador del punto `A-03`
/// (`Contracts CU-02` FA-03).
/// </summary>
/// <remarks>
/// NO DECLARA NINGÚN CAMPO QUE PERMITA CONFIGURAR UNA SEGUNDA: no lleva identificador de cuenta
/// ni papel. El papel lo fija el dominio y la unicidad la hace cumplir la capa de aplicación
/// sobre el conjunto de cuentas (RN-01, INV-05).
///
/// CUÁNTOS CAMPOS LLEVA ES UNA CONTRADICCIÓN DE LAS FUENTES, RESUELTA ACÁ Y DECLARADA COMO TAL.
/// `Contracts CU-02` FA-03 la describe en una línea «con correo y contraseña», y la maqueta
/// aprobada de `Aprovisionamiento-Inicial` dibuja tres campos —correo, contraseña y su
/// repetición—. `Domain CU-12` §3 y §4 y `Application CU-10` §3 y §4 exigen **correo, nombre,
/// apellido y credencial derivada**, y los dos lo ejercen en su criterio de aceptación `CA-01`,
/// con nombre y apellido concretos.
///
/// Se resuelve a favor de los dos contratos que describen el acto **con criterios de aceptación
/// propios**, contra la mención de paso de un contrato que trata de otra cosa, y en consecuencia
/// la superficie de la pieza pública suma dos campos a los tres de la maqueta. **Es una
/// propuesta de la etapa `c` y va al punto de control**: la resolución opuesta —quitarle nombre
/// y apellido a `CU-12`— también es posible, y es del Product Owner, no de quien construye.
/// </remarks>
public sealed record AdministratorSetupRequest(
    string? Email,
    string? FirstName,
    string? LastName,
    string? Password);
