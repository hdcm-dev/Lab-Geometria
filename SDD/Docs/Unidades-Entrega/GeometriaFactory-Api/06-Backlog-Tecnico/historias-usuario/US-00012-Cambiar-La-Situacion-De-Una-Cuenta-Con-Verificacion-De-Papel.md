# US-00012 — Cambiar la situación de una cuenta con verificación de papel

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** US-00012-Cambiar-La-Situacion-De-Una-Cuenta-Con-Verificacion-De-Papel.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master + API Product Owner (AG-06)
**Épica:** EP-00003 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Punto de acceso:** `A-07`, bajo la guardia
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código de `GeometriaFactory-Web`**, quiero **exponer el cambio de situación de una cuenta y recibir la provisoria cuando la operación la produce**, para **que el panel pueda comunicársela al alumno en el momento**.

## 2. Contexto

`F-03` del intake §4 es `Must Have`, y `RN-00016` agrega que **habilitar produce la provisoria**. `02` §6 declara el segundo efecto estructural de esa regla sobre esta superficie: **el resultado de este punto devuelve la provisoria**. El contrato de uso es [`CU-00004`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00004-Exponer-El-Gobierno-De-Las-Cuentas-De-La-Comision.md).

## 3. Criterios de aceptación

- Given un acceso con papel `Administrador` y una cuenta en estado `Pendiente`, When se la habilita, Then la transición se aplica y **el resultado devuelve la provisoria**.
- Given una rehabilitación, When se la aplica, Then ocurre lo mismo.
- Given ese resultado, When se inspecciona el registro del servidor, Then **la provisoria no aparece en ninguna traza**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00001, NB-00002 |
| CU cubiertos | CU-00004 |
| RN que ejerce | RN-00001, RN-00006, RN-00016 en su efecto estructural |
| Componente de `05` §3.1 | Superficie de gobierno de la comisión |
| ¿Decide qué se dice? | **No.** La transición la resuelve el dominio y la provisoria la produce `GeometriaFactory-Infrastructure` |
| Familia empobrecida | **No** |
| BT derivadas | BT-00011, BT-00017 |
| Tests previstos en 08 | Batería de integración, y la inspección de que la provisoria no queda en el registro |

## 5. Prioridad y estimación

`Must` por derivar de `F-03` y `F-04`, `Must Have`, y porque el criterio de transición `d` → `e` exige que al habilitar el producto muestre **una contraseña provisoria que el administrador no escribió**.

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

**`RN-00016` tiene dos efectos estructurales sobre esta superficie y ninguno es un tramo propio** (`02` §6): el **retiro** de `A-04` y el resultado de este punto. Lo que esta capa aporta es **no exponer ningún punto que la contradiga**.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
