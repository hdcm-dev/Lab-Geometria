# US-00008 — Configurar la cuenta de administrador sólo mientras no exista ninguna

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** US-00008-Configurar-La-Cuenta-De-Administrador-Solo-Mientras-No-Exista-Ninguna.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master + API Product Owner (AG-06)
**Épica:** EP-00002 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Punto de acceso:** `A-03`, **fuera de la guardia**: no hay todavía identidad que pueda autenticarse
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código de `GeometriaFactory-Web`**, quiero **exponer la configuración de la cuenta de administrador y que deje de proceder en cuanto exista una**, para **que el laboratorio tenga quien lo gobierne desde el primer arranque y esa puerta se cierre para siempre**.

## 2. Contexto

`RN-00001` declara que existe **exactamente un** administrador y que su alta sólo es posible mientras no exista ninguno; `INV-05` lo sostiene. El contrato de uso es [`CU-00025`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00025-Configurar-La-Cuenta-De-Administrador-En-El-Primer-Arranque.md).

## 3. Criterios de aceptación

- Given una instancia sin administrador, When se envía la configuración, Then la cuenta queda constituida y habilitada.
- Given una instancia que **ya tiene** administrador, When se envía de nuevo, Then se traduce a **conflicto de estado** y no procede.
- Given ese rechazo, When se lo inspecciona, Then **no revela nada** de la cuenta de administrador existente.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00001, NB-00002 |
| CU cubiertos | CU-00003 |
| RN que ejerce | RN-00001; sostiene `INV-05` |
| Componente de `05` §3.1 | Superficie de acceso y credencial propia |
| ¿Decide qué se dice? | **No.** La ventana de alta la ejerce la capa de aplicación |
| Familia empobrecida | **No** |
| BT derivadas | BT-00013, BT-00016 |
| Tests previstos en 08 | Batería de integración con la segunda configuración rechazada |

## 5. Prioridad y estimación

`Must` por `RN-00001`, y porque el criterio de transición `c` → `d` exige que el administrador se configure en el primer arranque y **sólo** mientras no exista ninguno.

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

**Es el segundo de los cuatro puntos fuera de la guardia, y su ausencia de acceso tiene un motivo propio**: en el momento en que se ejerce **no hay todavía identidad que pueda autenticarse**. `05` §3.4 lo declara así, punto por punto.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
