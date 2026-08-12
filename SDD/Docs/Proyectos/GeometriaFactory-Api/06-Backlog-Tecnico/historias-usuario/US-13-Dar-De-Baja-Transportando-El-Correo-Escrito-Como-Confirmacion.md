# US-13 — Dar de baja una cuenta transportando el correo escrito como confirmación

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** US-13-Dar-De-Baja-Transportando-El-Correo-Escrito-Como-Confirmacion.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master + API Product Owner (AG-06)
**Épica:** EP-03 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Punto de acceso:** `A-08`, bajo la guardia
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código de `GeometriaFactory-Web`**, quiero **que el punto de baja transporte el correo escrito como confirmación y no proceda sin él**, para **que la única operación irreversible del producto no ocurra por una petición suelta**.

## 2. Contexto

`RN-07` exige confirmación escrita y arrastre. `02` §6 declara el tramo de esta capa: **el punto transporta el correo escrito y no procede sin él**; **la comparación y el arrastre son de las capas de adentro**. El contrato de uso es [`CU-04`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-04-Exponer-El-Gobierno-De-Las-Cuentas-De-La-Comision.md).

## 3. Criterios de aceptación

- Given un acceso con papel `Administrador` y el correo escrito, When se pide la baja, Then el punto lo transporta y la operación procede si la comparación de la capa de aplicación da positiva.
- Given una petición **sin** el correo escrito, When se la envía, Then **no procede**.
- Given una baja y un reseteo, When se comparan los dos puntos, Then son **dos puntos distintos, con verbos distintos**, y el del reseteo **no toca ninguna ruta de retiro**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-01 |
| CU cubiertos | CU-04 |
| RN que ejerce | RN-07, RN-12 por contraste |
| Componente de `05` §3.1 | Superficie de gobierno de la comisión |
| ¿Decide qué se dice? | **No.** La comparación del correo y el arrastre son de las capas de adentro |
| Familia empobrecida | **No** |
| BT derivadas | BT-11, BT-17 |
| Tests previstos en 08 | Batería de integración con y sin el correo escrito |

## 5. Prioridad y estimación

`Must` por `RN-07`, y porque el criterio de transición `d` → `e` exige que la baja **exija confirmación escribiendo el correo de la cuenta**.

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

**Que el reseteo y la baja sean dos puntos distintos es lo que hace verificable `RN-12` desde esta superficie**: no hace falta leer el código para saber que el reseteo no retira nada, alcanza con mirar que no toca ninguna ruta de retiro.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
