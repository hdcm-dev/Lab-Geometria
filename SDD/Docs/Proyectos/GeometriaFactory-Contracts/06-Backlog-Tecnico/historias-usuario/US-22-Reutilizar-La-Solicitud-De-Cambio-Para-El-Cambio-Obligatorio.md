# US-22 — Reutilizar la solicitud de cambio de contraseña para el cambio obligatorio

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** US-22-Reutilizar-La-Solicitud-De-Cambio-Para-El-Cambio-Obligatorio.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-03 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **extremo que compila contra el contrato (`GeometriaFactory-Api` y `GeometriaFactory-Web`)**, quiero **que el cambio obligatorio de contraseña use **el mismo tipo** que el cambio voluntario, con la provisoria como credencial vigente**, para **que no exista una segunda superficie de escritura de contraseña, que es por donde se abriría el agujero**.

## 2. Contexto

`02` §3.1 declara que el cambio obligatorio reutiliza el mismo tipo que el cambio voluntario y que lo que difiere es la precondición, que es una regla de `GeometriaFactory-Domain`. `RN-16` declara que **no existe ninguna escritura anónima de credencial**: toda operación que fija o cambia una contraseña ocurre con la cuenta ya autenticada.

## 3. Criterios de aceptación

- Given una cuenta con la marca de cambio pendiente, When arma su cambio obligatorio, Then usa **el mismo tipo** de solicitud que el cambio voluntario, con la provisoria como contraseña vigente.
- Given la superficie completa del contrato, When se busca un punto que acepte un correo y una contraseña nueva **sin credencial**, Then no existe ninguno, por `RN-16`.
- Given una operación cualquiera intentada con la marca puesta, When el servicio responde, Then el rechazo usa **un solo** código para todas las operaciones bloqueadas, y no uno por operación.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-02 |
| CU cubiertos | CU-08, CU-02 |
| Familia de tipos de `05` §3.1 | Familia de reseteo, Familia de cuentas |
| Restricciones transversales de `02` §6 | RT-01 |
| RN que refiere por identificador | RN-13, RN-16 |
| BT derivadas | BT-08, BT-11 |
| Etapa del producto | `d`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba de integración del cambio obligatorio y prueba de inspección de que no existe ningún punto anónimo de escritura de contraseña. |

## 5. Prioridad y estimación

`Must` por derivar de `F-04`, `Must Have` en `PRODUCT-INTAKE` §4, y porque el roadmap §5.2 incorporó como criterio bloqueante de la transición `d` → `e` que ningún punto de acceso acepte un correo y una contraseña nueva sin credencial.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un contrato de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Declara la familia de tipos de `05` §3.1 que la sostiene
- [x] Ninguna regla de negocio se redacta acá: las refiere por identificador a `GeometriaFactory-Domain`
- [x] Se refinó contra la regla de exposición de `05` §3.2 y ningún campo que introduce puede transportar una dirección de servicio, una ruta de datos ni un secreto

## 7. Notas y supuestos

Ésta es la **única arista adicional** del grafo de familias de `05` §3.1: reseteo depende de cuentas, y el motivo está declarado —el cambio obligatorio reutiliza el tipo del cambio voluntario en lugar de redeclararlo—. El grafo sigue siendo acíclico.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 previó con este mismo identificador y esta misma pertenencia a necesidades de negocio. |
