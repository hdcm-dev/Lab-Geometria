# US-18 — Reenviar un trabajo en `Borrador` con el texto que la persona volvió a pegar

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** US-18-Reenviar-Un-Trabajo-En-Borrador-Con-El-Texto-Que-La-Persona-Volvio-A-Pegar.md
**Versión:** 1.0
**Estado:** Propuesta
**Fecha:** 2026-08-10
**Autor:** Scrum Master + API Product Owner (AG-06)
**Épica:** EP-05 Interpretación y verificación del dato del alumno
**Etapa del producto:** `f`
**Punto de acceso:** `A-11`, bajo la guardia
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código de `GeometriaFactory-Web`**, quiero **reenviar un trabajo que quedó en `Borrador` con el texto corregido**, para **que el alumno vuelva a intentar la entrega sin cargar un trabajo nuevo**.

## 2. Contexto

`F-07` del intake §4 declara `Must Have` reeditar el trabajo **sólo mientras está en `Borrador`**, y `F-22` unifica guardar y enviar. El contrato de uso es [`CU-06`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-06-Exponer-El-Envio-Y-La-Eliminacion-De-Un-Trabajo.md).

## 3. Criterios de aceptación

- Given un trabajo propio en `Borrador`, When se lo reenvía con un texto nuevo, Then la interpretación se rehace y el estado resultante viaja en una respuesta exitosa.
- Given un trabajo propio en cualquier otro estado, When se lo reenvía, Then se rechaza, y **el código del contrato que lo transporta es el genérico con su hueco declarado**.
- Given un trabajo de otro alumno, When se lo reenvía, Then la respuesta es **la misma que la de un identificador inexistente**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-03, NB-04 |
| CU cubiertos | CU-06 |
| RN que ejerce | RN-03, RN-04, RN-08 |
| Componente de `05` §3.1 | Superficie de trabajos, Traductor de motivos y códigos |
| ¿Decide qué se dice? | **No** |
| Familia empobrecida | **Sí**: la del recurso que no se ve, que es la primera de las tres |
| BT derivadas | BT-14, BT-15, BT-18 |
| Tests previstos en 08 | Batería de integración con el reenvío rechazado fuera de `Borrador` |

## 5. Prioridad y estimación

`Must` por derivar de `F-07` y `F-22`, `Must Have`, y porque el criterio de transición `e` → `f` exige que un trabajo quede en `Borrador` **con el texto inválido** y se reedite.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el punto de acceso que la realiza y el componente de `05` §3.1 que lo aloja
- [x] Declara si su punto está bajo la guardia, y si no lo está, cuál de las cuatro ausencias declaradas es
- [x] Toda condición que transporta es uno de los quince códigos vivos del contrato, con su destino declarado
- [x] Declara que no decide qué se dice
- [x] Declara si su respuesta pertenece a una de las tres familias deliberadamente empobrecidas

## 7. Notas y supuestos

**El código del contrato para un reenvío forzado fuera de `Borrador` es un hueco declarado.** El código análogo del conjunto cerrado está **acotado a la eliminación y al camino del alumno**, y esta categoría **no inventa uno**: usa el genérico y eleva el hueco como `PA-04` de [`../Product-Backlog.md`](../Product-Backlog.md) §6, con BT-15.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
