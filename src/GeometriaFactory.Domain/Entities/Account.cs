namespace GeometriaFactory.Domain.Entities;

/// <summary>
/// Cuenta de la comisión. Una fila por persona, sea alumno o administrador.
/// </summary>
/// <remarks>
/// ETAPA `a`: el tipo existe y NO tiene atributos ni invariantes. El Product Owner
/// decidió que modelar las entidades es de la etapa `c` (`Domain BT-06`), lo que
/// resuelve el riesgo `R-02` de `Plan-Etapa-A.md` §7 a favor de la etapa `c`.
/// Concepto declarado en `Definicion-Modelo-De-Dominio.md` §2.1.
/// Nombre tomado del glosario de `Norma-De-Nomenclatura.md` §6.4 (`Cuenta` ⟶ `Account`).
/// </remarks>
public sealed class Account
{
}
