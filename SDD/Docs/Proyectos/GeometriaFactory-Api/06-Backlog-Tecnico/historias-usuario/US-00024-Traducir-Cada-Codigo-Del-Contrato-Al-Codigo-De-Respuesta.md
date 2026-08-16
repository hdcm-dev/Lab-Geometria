# US-00024 — Traducir cada código del contrato al código de respuesta que le corresponde

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** US-00024-Traducir-Cada-Codigo-Del-Contrato-Al-Codigo-De-Respuesta.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master + API Product Owner (AG-06)
**Épica:** EP-00002 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Punto de acceso:** Ninguno propio: es transversal a los **quince**
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **producto**, quiero **que cada uno de los diecisiete códigos vivos del contrato tenga su código de respuesta declarado en una sola tabla**, para **que ninguna traducción se improvise en un punto de acceso**.

## 2. Contexto

`02` §3 declara la **traducción a protocolo** como una de las cinco responsabilidades, y §2 lo enuncia: **acá se traduce, y traducir es decidir**; un motivo de la capa de aplicación **no es** un código de respuesta, y un código del contrato **no es** un número de protocolo. El contrato de uso es [`CU-00009`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00009-Traducir-El-Motivo-Del-Contrato-A-Respuesta-De-Protocolo.md).

## 3. Criterios de aceptación

- Given el conjunto cerrado de **diecisiete** códigos vivos, When se recorre la tabla de traducción, Then **16 de 17** tienen destino declarado y **1** está declarado **sin destino, con su motivo**.
- Given ese recorrido, When se lo hace **en las dos direcciones**, Then hay **0** códigos inventados y **0** renombrados: esta capa **no agrega, no renombra y no traduce a texto** ninguno.
- Given una condición del adaptador que **no tiene código propio** en el conjunto cerrado, When se la traduce, Then se usa el **genérico** y **el hueco se declara** en lugar de inventarse uno.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00004, NB-00008 |
| CU cubiertos | CU-00009 |
| RN que ejerce | RN-00002, RN-00003, RN-00009, RN-00010 en sus tramos de traducción |
| Componente de `05` §3.1 | Traductor de motivos y códigos, **transversal a los quince puntos** |
| ¿Decide qué se dice? | **Decide cómo se dice**, que es lo propio de esta capa, y **es donde una decisión ya tomada puede deshacerse sin que nadie lo note** |
| Familia empobrecida | **Sí**, gobierna las tres |
| BT derivadas | BT-00013, BT-00015 |
| Tests previstos en 08 | **Prueba de inspección que recorre el conjunto cerrado contra la tabla, en las dos direcciones** |

## 5. Prioridad y estimación

`Must` porque **ningún camino de fallo sale sin pasar por acá** (`05` §4), y porque las dos reglas que esta capa puede romper sola se rompen en esta traducción o en la guardia.

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

**Los diecisiete códigos vivos son diecisiete sobre veinte identificadores emitidos**: tres están retirados y **ninguno se recicla**. El recuento se verifica en la tabla del catálogo de la categoría 03 de `GeometriaFactory-Contracts`, que es **la única tabla donde los veinte están enumerados juntos**.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **4**. Sube minor. |
