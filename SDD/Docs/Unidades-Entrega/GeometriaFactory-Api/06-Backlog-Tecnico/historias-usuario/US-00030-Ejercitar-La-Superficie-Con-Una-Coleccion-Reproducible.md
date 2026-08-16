# US-00030 — Ejercitar la superficie con una colección reproducible en cinco pasos o menos

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** US-00030-Ejercitar-La-Superficie-Con-Una-Coleccion-Reproducible.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master + API Product Owner (AG-06)
**Épica:** EP-00006 Desenlace de la entrega
**Etapa del producto:** `h`
**Punto de acceso:** Ninguno propio: **ejercita** los quince
**Prioridad MoSCoW:** **Should**
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **integrador de la superficie —el propio equipo o quien la revise—**, quiero **una colección de peticiones que se reproduzca en cinco pasos o menos y ejercite la superficie de punta a punta**, para **poder demostrar el servicio sin escribir peticiones a mano y sin inventar datos**.

## 2. Contexto

`PRODUCT-INTAKE` §16.1 declara, para el tipo de proyecto de código de esta pieza, una **colección de peticiones reproducible con los escenarios como cuerpo**: alta de trabajo, envío con texto que verifica y que no verifica, y **aprobación y rechazo por el administrador**, con los códigos de respuesta esperados. El contrato de uso es [`CU-00012`](../../10-Examples/CU-00012-Ejercitar-La-Superficie-Con-La-Coleccion-De-Peticiones-Reproducible.md).

## 3. Criterios de aceptación

- Given la colección, When se la reproduce desde cero, Then se completa en **5 pasos o menos**.
- Given sus cuerpos, When se los inspecciona, Then son los escenarios del intake §20 y hay **0** datos de prueba inventados.
- Given la colección, When se la recorre, Then incluye el alta, el envío que verifica, el que no verifica y **la aprobación y el rechazo por el administrador**, que es lo que la ubica en la etapa `h`.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | **Ninguna.** `02` §7.2 declara que `CU-00012` **no traza a ninguna necesidad**: **no implementa nada, demuestra**, y asignarle las necesidades de las capacidades que ejercita las contaría dos veces |
| CU cubiertos | CU-00012 |
| RN que ejerce | — |
| Componente de `05` §3.1 | **Ninguno**, y es correcto: `05` §3.3 declara que es el único de los doce casos de uso **sin componente**, porque es un artefacto del árbol de muestras y no código de producción |
| ¿Decide qué se dice? | **No** |
| Familia empobrecida | **No** |
| BT derivadas | BT-00020, BT-00021 |
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
- [x] Toda condición que transporta es uno de los diecisiete códigos vivos del contrato, con su destino declarado
- [x] Declara que no decide qué se dice
- [x] Declara si su respuesta pertenece a una de las tres familias deliberadamente empobrecidas

## 7. Notas y supuestos

**El alcance de la colección está abierto y es una divergencia entre dos textos vivos, no un recuento envejecido.** La fuente lo declara en dos lugares con alcances distintos —los **ocho** escenarios en uno y **dos** en el otro—, los dos textos están al día y **la fuente no declara cuál manda**. La categoría 02 adopta **los ocho** con el fundamento de que `E-8` es el modo de falla que el propio intake llama **el más probable**, y este backlog **hereda esa lectura y no la reabre**: es `PA-07` de [`../Product-Backlog.md`](../Product-Backlog.md) §6, elevado con BT-00021.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
