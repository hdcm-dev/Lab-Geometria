# US-00023 — Aprobar o rechazar un trabajo en estado `Pendiente`, con comentario opcional

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** US-00023-Aprobar-O-Rechazar-Un-Trabajo-En-Estado-Pendiente.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master + API Product Owner (AG-06)
**Épica:** EP-00006 Desenlace de la entrega
**Etapa del producto:** `h`
**Punto de acceso:** `A-15`, bajo la guardia
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código de `GeometriaFactory-Web`**, quiero **exponer el desenlace de un trabajo en estado `Pendiente`, con comentario opcional**, para **que el administrador cierre la entrega y el alumno reciba una respuesta explícita**.

## 2. Contexto

`RN-00010` declara el desenlace **exclusivo del administrador y terminal**, y `F-23` y `F-21` del intake §4 son `Must Have`. El contrato de uso es [`CU-00008`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00008-Exponer-El-Desenlace-De-La-Revision.md), y `02` §8 declara que aprobar y rechazar quedaron juntos **por la misma fusión que el ensamblado de contratos ya justificó**: se distinguen por el valor de un campo de conjunto cerrado.

## 3. Criterios de aceptación

- Given un acceso con papel `Administrador` y un trabajo en estado `Pendiente`, When se aprueba o se rechaza, con o sin comentario, Then el desenlace se aplica y el estado resultante es terminal.
- Given un trabajo en un estado que **no admite desenlace**, incluido el terminal, When se lo intenta, Then se traduce a **conflicto de estado** y la respuesta **no sugiere ninguna forma de revertirlo**.
- Given un acceso con papel `Alumno`, When fuerza la petición, Then se rechaza en la guardia.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00009 |
| CU cubiertos | CU-00008 |
| RN que ejerce | RN-00003 en su tramo de traducción, RN-00010; `INV-07` |
| Componente de `05` §3.1 | Superficie de desenlace, Guardia de admisión |
| ¿Decide qué se dice? | **No.** La transición y su exclusividad las deciden el dominio y la capa de aplicación |
| Familia empobrecida | **Sí**, en su camino de recurso que no se ve |
| BT derivadas | BT-00011, BT-00013, BT-00019 |
| Tests previstos en 08 | Batería de integración con los dos desenlaces, con y sin comentario, y con el forzado desde papel `Alumno` |

## 5. Prioridad y estimación

`Must` por derivar de `F-21` y `F-23`, `Must Have`, y porque el criterio de transición `h` → `i…` exige que **un alumno que fuerce la transición contra el servicio de datos sea rechazado**.

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

**El código del contrato para una operación de administrador pedida por quien no lo es está acotado al desenlace**, y para los otros tres caminos —gobierno de cuentas, reseteo y revisión de la comisión— **el conjunto cerrado no declara ninguno**. Esta categoría usa el genérico y **eleva el hueco** como `PA-03` de [`../Product-Backlog.md`](../Product-Backlog.md) §6, con BT-00015.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
