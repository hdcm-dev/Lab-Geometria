# US-06016 — Conservar y transportar la marca de cambio de contraseña pendiente sin alterar el estado

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** US-06016-Conservar-Y-Transportar-La-Marca-Sin-Alterar-El-Estado.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-11
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-06003 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que la marca de cambio de contraseña pendiente se guarde como atributo propio de la cuenta y viaje en cada recuperación, sin tocar su estado de habilitación**, para **que la comprobación transversal de la capa de aplicación tenga sobre qué decidir**.

## 2. Contexto

`RN-06013` y `RN-06012` gobiernan la marca; `RC-06007` declara que **la marca no es un estado de cuenta**. `02` §6 declara el tramo de esta capa: **conserva la marca y la hace viajar**; **la comprobación no es de acá**. El contrato de uso es [`CU-06005`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-06005-Guardar-Y-Recuperar-Las-Cuentas-De-La-Comision.md).

## 3. Criterios de aceptación

- Given una cuenta con la marca puesta, When se la recupera, Then la marca **viaja con ella**.
- Given una cuenta en estado `Pendiente`, otra `Habilitado` y otra `Bloqueado`, When se les escribe la marca, Then **ninguna cambia de estado**: la marca se escribe sobre los tres sin alterarlos.
- Given un reseteo, When se inspeccionan los trabajos de la cuenta, Then **están todos**, con sus estados y sus comentarios: el reseteo **no pasa por ninguna ruta de retiro**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00001, NB-00002 |
| CU cubiertos | CU-06005 |
| RN que ejerce | RN-06012, RN-06013, RN-06015, RN-06016 |
| Componente de `05` §3.1 | Adaptador de repositorio de cuentas |
| Reglas conceptuales de modelo | `RC-06007`, la marca no es un estado de cuenta |
| ¿Toma alguna decisión de negocio? | **No.** La comprobación de la marca es de `GeometriaFactory-Application` |
| ¿Toca el almacén? | **Sí** |
| BT derivadas | BT-06005, BT-06009 |
| Tests previstos en 08 | Prueba con un alumno con trabajos en tres estados, antes y después del reseteo |

## 5. Prioridad y estimación

`Must` porque **sin este dato la cuarta comprobación de la capa de aplicación no tendría sobre qué decidir**, e `INV-09` dejaría de poder sostenerse; y porque el criterio de transición `d` → `e` exige que la cuenta con la contraseña reseteada conserve su identidad, su situación y **todos sus trabajos**.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el componente de `05` §3.1 y, si toca el almacén, las reglas conceptuales de modelo que materializa
- [x] Declara que no toma ninguna decisión de negocio
- [x] Toda condición que produce existe en el catálogo de las 17 de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md), declarada como resultado o como fallo
- [x] Declara el camino en que el mecanismo se detiene en lugar de cumplir a medias, cuando puede fallar
- [x] Declara si toca el almacén y, en consecuencia, dónde vive su prueba

## 7. Notas y supuestos

**La marca tiene dos orígenes y esta capa no los distingue**: la ponen la habilitación —`RN-06016`— y el reseteo —`RN-06014`—, y el adaptador escribe la misma marca en los dos casos. Cuál de los dos actos la motivó lo sabe la capa de aplicación, no ésta.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **Unificación de nomenclatura del reseteo: se resetea la contraseña de la cuenta, no la cuenta.** Corrección pedida por el Product Owner —«ese resetear cuenta hay que corregirlo por resetear clave de cuenta de usuario alumno»— y corregida primero en la fuente, `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.28**: leído literal, «resetear la cuenta» sugiere darla de baja y volver a darla de alta, que es exactamente el remedio que **F-26** vino a reemplazar. Acá se reescriben **1** ocurrencia a «resetear / reseteo **de la contraseña** de la cuenta» y «cuenta **con la contraseña reseteada**». No cambia ninguna regla ni su verificación, y **no se toca ningún identificador** de código de error ni de regla —`RESETEO_ACOTADO_A_CUENTAS_DE_ALUMNO` y `CONTRATO_RESETEO_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` se conservan tal cual—. |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
