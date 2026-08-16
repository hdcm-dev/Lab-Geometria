# US-08019 — Transportar el conjunto cerrado de cuatro estados del trabajo

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** US-08019-Transportar-El-Conjunto-Cerrado-De-Cuatro-Estados.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-08004 Gestión del trabajo
**Etapa del producto:** `e`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **extremo que compila contra el contrato (`GeometriaFactory-Api` y `GeometriaFactory-Web`)**, quiero **que el estado del trabajo viaje como conjunto cerrado de **cuatro** valores, dos de ellos terminales**, para **que los dos extremos hablen del mismo conjunto de estados y que ninguno pueda inventar uno quinto**.

## 2. Contexto

`PRODUCT-INTAKE` §4.2 declara el modelo de estados con sus cuatro valores y sus dos terminales. Es la restricción transversal `RT-08` de `02` §6, que además exige que **ningún tipo permita salir de los terminales**.

## 3. Criterios de aceptación

- Given cualquier trabajo, When su estado viaja en el listado o en el detalle, Then el valor pertenece al conjunto cerrado de **cuatro**.
- Given un trabajo en uno de los **dos** estados terminales, When se busca en la superficie algún tipo que permita transicionarlo, Then no existe ninguno.
- Given un envío, When se arma su respuesta, Then el estado resultante viaja como salida del mismo envío y no como una operación aparte, por el criterio de recorte de `02` §3.1.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00003, NB-00004, NB-00009 |
| CU cubiertos | CU-08003, CU-08004 |
| Familia de tipos de `05` §3.1 | Familia de trabajo, Familia de listado |
| Restricciones transversales de `02` §6 | RT-08 |
| RN que refiere por identificador | RN-08005, RN-08010 |
| BT derivadas | BT-08012, BT-08013 |
| Etapa del producto | `e`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba de inspección del conjunto cerrado y prueba de integración del envío en sus dos resultados. |

## 5. Prioridad y estimación

`Must` por derivar de `F-08` y de `F-22`, las dos `Must Have` en `PRODUCT-INTAKE` §4.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un contrato de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Declara la familia de tipos de `05` §3.1 que la sostiene
- [x] Ninguna regla de negocio se redacta acá: las refiere por identificador a `GeometriaFactory-Domain`
- [x] Se refinó contra la regla de exposición de `05` §3.2 y ningún campo que introduce puede transportar una dirección de servicio, una ruta de datos ni un secreto

## 7. Notas y supuestos

**`Pendiente` se escribe siempre calificado** —«trabajo en estado `Pendiente`»— porque el mismo término nombra también una situación de cuenta, y los dos sentidos cruzan este mismo contrato (`PRODUCT-INTAKE` §4.2, `05` §7).

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 previó con este mismo identificador y esta misma pertenencia a necesidades de negocio. |
