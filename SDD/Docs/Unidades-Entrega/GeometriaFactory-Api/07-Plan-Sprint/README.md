# 07 · Plan de sprint — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** README.md
**Versión:** 1.2
**Estado:** Aprobado
**Fecha:** 2026-08-12
**Autor:** Scrum Master + API PM (AG-07)

---

## 1. Documento de esta sección

| Documento | Propósito |
| --- | --- |
| [`Mini-Plan.md`](Mini-Plan.md) | Plan único condensado: los seis tramos, ítems comprometidos, alcance técnico, Definition of Done aplicada, riesgos, criterios de hecho, trazabilidad y bitácora |

## 2. Artefactos que esta sección no emite, y por qué

| Artefacto | Estado | Motivo |
| --- | --- | --- |
| `Plan-Iteracion-Sprint-XX.md` | **Omitido** | `equipo_n = 1` (`PRODUCT-INTAKE` §2). La regla de la categoría sustituye los cuatro artefactos por el mini-plan |
| `Template-Sprint-Review.md` | **Omitido** | Ídem. El evento de cierre de este producto es el **punto de control bloqueante** de cada etapa (`PRODUCT-INTAKE` §15) |
| `Template-Sprint-Retrospectiva.md` | **Omitido** | Ídem. Con una sola persona no hay retrospectiva de equipo que facilitar |
| `Velocidad-Equipo.md` | **Omitido** | Ídem, y no hay iteraciones cerradas ni plazo calendario del que derivar una velocidad (`Roadmap-Producto.md` §1.1) |

## 3. Estado del plan

| Aspecto | Valor al 2026-08-10 |
| --- | --- |
| Etapas comprometidas del producto | 8 (`a` a `h`) |
| Etapas que toca este proyecto de código | 6: `a`, `c`, `d`, `e`, `f` y `h` |
| Etapas cerradas | 0 |
| Etapa abierta | Ninguna: el producto está en fase de especificación |
| Historias comprometidas | 30 de 30 |
| Tareas técnicas comprometidas | 26 |
| Puntos de acceso que el plan pone en pie | 15 de 15; `A-04` está **retirado y no se recicla** |
| Puertas técnicas propias | **`PT-04`**, medida en la etapa `a` |

**Las etapas `b` y `g` no producen trabajo acá**, y el motivo está en [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §2: la `b` no agrega ningún punto de acceso y **todo lo que la `g` necesita de esta superficie ya está expuesto en la `e`**.

## 4. Dónde vive lo que este plan no decide

| Contenido | Dónde vive |
| --- | --- |
| Qué se construye y en qué prioridad | [`../06-Backlog-Tecnico/`](../06-Backlog-Tecnico/) |
| La superficie: los quince puntos, sus verbos y sus códigos de respuesta | [`../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md`](../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md) y [`../05-Arquitectura-Tecnica/Contratos-REST.md`](../05-Arquitectura-Tecnica/Contratos-REST.md) |
| El orden de las etapas, sus criterios de transición y dónde se miden las puertas | [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) |
| Las dieciséis reglas y los nueve invariantes | `GeometriaFactory-Domain`, categorías 02 y 05 |
| El conjunto cerrado de diecisiete códigos vivos | `GeometriaFactory-Contracts`. **Esta capa no agrega, no renombra y no traduce a texto ninguno** |
| El nombre del cuarto puerto | El punto de control de la etapa `a`, **sobre la superficie de `GeometriaFactory-Application`** |
| Los **dos huecos** del conjunto cerrado de códigos | Abiertos: `PA-03` y `PA-04` del backlog, elevados con BT-00015. **Esta categoría no inventa códigos** |
| El despliegue | **Manual, por el docente** (`PRODUCT-INTAKE` §17.1.P.8 · GeometriaFactory-Api). `09-Devops`, **todavía no emitida**, mide el mecanismo de construcción en destino con BT-00026 |
| La Definition of Done canónica | `08-Calidad-Y-Pruebas`, **todavía no emitida** |
| La colección de peticiones como entregable de ejemplos | [`../10-Examples/`](../10-Examples/), **emitida el 2026-08-11** en su pasada de diseño. La colección es el sample 02, [`../10-Examples/ejemplo-02-intermedio.md`](../10-Examples/ejemplo-02-intermedio.md), y su contrato de verificación `VER-00002` entra a [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md) como fila `SD-00002`. La fila se conserva con su desenlace en lugar de retirarse |

**Dos filas más de esta tabla quedaron desactualizadas, y se declara acá en lugar de corregirlas desde esta categoría.** Las de `09-Devops` y `08-Calidad-Y-Pruebas` dicen «todavía no emitida», y **las dos están emitidas** desde el 2026-08-11. Actualizarlas pertenece a la categoría 07 en su próxima revisión: esta emisión sólo cierra la fila que la Fase G resuelve.

## 5. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **Cierre de la fila de `10-Examples`** de §4, que decía «todavía no emitida». La categoría se emitió en su pasada de diseño y la colección de peticiones es su sample 02, con el contrato `VER-00002` que entra a la matriz de sensado como `SD-00002`. La fila se **conserva** con su desenlace y su fecha. Se declara además que las filas de `08-Calidad-Y-Pruebas` y de `09-Devops` de esa misma tabla **también quedaron desactualizadas** y que corregirlas pertenece a esta categoría en su próxima revisión. **Ningún tramo del plan, ninguna puerta y ningún recuento cambian.** Sube minor. |
| 1.0 | 2026-08-10 | Emisión inicial del índice de la sección. Declara el único artefacto emitido, los **cuatro** que se omiten con el motivo de cada uno, el estado del plan con sus **seis** tramos, sus quince puntos de acceso y la puerta `PT-04`, y dónde vive lo que este plan no decide, incluidos los dos huecos del conjunto cerrado de códigos y las tres categorías todavía no emitidas. |
| 1.2 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
