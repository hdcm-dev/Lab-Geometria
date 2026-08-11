# US-30 — Ejercitar la superficie con una colección reproducible en cinco pasos o menos

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** US-30-Ejercitar-La-Superficie-Con-Una-Coleccion-Reproducible.md
**Versión:** 1.0
**Estado:** Propuesta
**Fecha:** 2026-08-10
**Autor:** Scrum Master + API Product Owner (AG-06)
**Épica:** EP-06 Desenlace de la entrega
**Etapa del producto:** `h`
**Punto de acceso:** Ninguno propio: **ejercita** los quince
**Prioridad MoSCoW:** **Should**
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **integrador de la superficie —el propio equipo o quien la revise—**, quiero **una colección de peticiones que se reproduzca en cinco pasos o menos y ejercite la superficie de punta a punta**, para **poder demostrar el servicio sin escribir peticiones a mano y sin inventar datos**.

## 2. Contexto

`PRODUCT-INTAKE` §16.1 declara, para el tipo de proyecto de código de esta pieza, una **colección de peticiones reproducible con los escenarios como cuerpo**: alta de trabajo, envío con texto que verifica y que no verifica, y **aprobación y rechazo por el administrador**, con los códigos de respuesta esperados. El contrato de uso es [`CU-12`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-12-Ejercitar-La-Superficie-Con-La-Coleccion-De-Peticiones-Reproducible.md).

## 3. Criterios de aceptación

- Given la colección, When se la reproduce desde cero, Then se completa en **5 pasos o menos**.
- Given sus cuerpos, When se los inspecciona, Then son los escenarios del intake §20 y hay **0** datos de prueba inventados.
- Given la colección, When se la recorre, Then incluye el alta, el envío que verifica, el que no verifica y **la aprobación y el rechazo por el administrador**, que es lo que la ubica en la etapa `h`.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | **Ninguna.** `02` §7.2 declara que `CU-12` **no traza a ninguna necesidad**: **no implementa nada, demuestra**, y asignarle las necesidades de las capacidades que ejercita las contaría dos veces |
| CU cubiertos | CU-12 |
| RN que ejerce | — |
| Componente de `05` §3.1 | **Ninguno**, y es correcto: `05` §3.3 declara que es el único de los doce casos de uso **sin componente**, porque es un artefacto del árbol de muestras y no código de producción |
| ¿Decide qué se dice? | **No** |
| Familia empobrecida | **No** |
| BT derivadas | BT-20, BT-21 |
| Tests previstos en 08 | Ejecución de la colección en la demostración de etapa |

## 5. Prioridad y estimación

**`Should`, y es la única de las treinta.** Su origen **no es una capacidad** de `PRODUCT-INTAKE` §4 sino la **estrategia de demostración** de §16.1 y §18, y **es la única historia de este backlog que no implementa nada**. El producto funciona sin ella; lo que se pierde es la forma de demostración que el tipo de proyecto de código tiene declarada. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.2.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap, o declara que su caso de uso no traza a ninguna y por qué
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el punto de acceso que la realiza, o declara que no realiza ninguno, y el componente de `05` §3.1
- [x] Declara si su punto está bajo la guardia, y si no lo está, cuál de las cuatro ausencias declaradas es
- [x] Toda condición que transporta es uno de los quince códigos vivos del contrato, con su destino declarado
- [x] Declara que no decide qué se dice
- [x] Declara si su respuesta pertenece a una de las tres familias deliberadamente empobrecidas

## 7. Notas y supuestos

**El alcance de la colección está abierto y es una divergencia entre dos textos vivos, no un recuento envejecido.** La fuente lo declara en dos lugares con alcances distintos —los **ocho** escenarios en uno y **dos** en el otro—, los dos textos están al día y **la fuente no declara cuál manda**. La categoría 02 adopta **los ocho** con el fundamento de que `E-8` es el modo de falla que el propio intake llama **el más probable**, y este backlog **hereda esa lectura y no la reabre**: es `PA-07` de [`../Product-Backlog.md`](../Product-Backlog.md) §6, elevado con BT-21.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
