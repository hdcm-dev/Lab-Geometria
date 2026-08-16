# US-00020 — Eliminar un trabajo con los dos alcances, verificado **forzando la petición**

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** US-00020-Eliminar-Un-Trabajo-Con-Los-Dos-Alcances-Forzando-La-Peticion.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master + API Product Owner (AG-06)
**Épica:** EP-00004 Gestión del trabajo
**Etapa del producto:** `e`
**Punto de acceso:** `A-12`, bajo la guardia
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **producto**, quiero **que la eliminación de un trabajo respete sus dos alcances aunque la petición se fuerce sin pasar por ninguna pantalla**, para **que la acotación no dependa de que la interfaz oculte un control**.

## 2. Contexto

`RN-00004` acota la eliminación del alumno al estado `Borrador` y habilita al administrador sobre todo lo que ve. `02` §6 declara que **es la única regla del producto con un criterio de verificación que exige forzar la petición contra esta superficie**, y el intake §17.5.P.6 lo fija como criterio bloqueante. El contrato de uso es [`CU-00006`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00006-Exponer-El-Envio-Y-La-Eliminacion-De-Un-Trabajo.md).

## 3. Criterios de aceptación

- Given un alumno y un trabajo propio en `Borrador`, When lo elimina, Then el trabajo desaparece.
- Given un alumno y un trabajo propio que **no** está en `Borrador`, When **fuerza la petición** contra esta superficie, Then se rechaza: **0** eliminaciones fuera de alcance aceptadas.
- Given un alumno y un trabajo **ajeno**, When fuerza la petición, Then la respuesta es **indistinguible** de la de un identificador inexistente, en cuerpo y en código.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00003, NB-00009 |
| CU cubiertos | CU-00006 |
| RN que ejerce | **RN-00003** en su tramo de traducción y **RN-00004** en sus dos alcances; `INV-02`, `INV-03` |
| Componente de `05` §3.1 | Superficie de trabajos, Traductor de motivos y códigos |
| ¿Decide qué se dice? | **No.** La acotación sobre el dato es de `GeometriaFactory-Application`; **la propiedad observable se decide acá** |
| Familia empobrecida | **Sí**: la del recurso que no se ve |
| BT derivadas | BT-00014, BT-00018, BT-00023 |
| Tests previstos en 08 | **Prueba de integración que fuerza la eliminación** de un trabajo que no está en `Borrador` y de uno que no pertenece al solicitante |

## 5. Prioridad y estimación

`Must` porque el criterio de transición `e` → `f` exige verificar la acotación **forzando la petición al servicio de datos, no sólo por la interfaz**, y porque `RN-00003` se rompe desde acá **sin que ninguna capa de adentro se entere**.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el punto de acceso que la realiza y el componente de `05` §3.1 que lo aloja
- [x] Declara si su punto está bajo la guardia, y si no lo está, cuál de las cuatro ausencias declaradas es
- [x] Toda condición que transporta es uno de los diecisiete códigos vivos del contrato, con su destino declarado
- [x] Declara que no decide qué se dice
- [x] Declara si su respuesta pertenece a una de las tres familias deliberadamente empobrecidas

## 7. Notas y supuestos

**Elegir «no autorizado» donde corresponde «no encontrado» es el error más caro de esta superficie**: confirma la existencia de un recurso ajeno y permite averiguar por tanteo qué identificadores existen. La capa de aplicación emite un motivo que declara «que el consumidor traduce a no encontrado y **nunca** a no autorizado»; **el consumidor es esta capa**.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
