# US-21 — Listar trabajos con el alcance ya decidido y sin parámetro para pedir borradores ajenos

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** US-21-Listar-Trabajos-Sin-Parametro-Para-Pedir-Borradores-Ajenos.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master + API Product Owner (AG-06)
**Épica:** EP-04 Gestión del trabajo
**Etapa del producto:** `e`
**Punto de acceso:** `A-13`, bajo la guardia
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **producto**, quiero **que el punto de listado no ofrezca ningún parámetro con el que se puedan pedir trabajos en `Borrador` ajenos**, para **que la puerta por la que `RN-11` se rompería sencillamente no exista**.

## 2. Contexto

`RN-11` declara que el administrador **no ve los trabajos en `Borrador`**. `02` §6 declara que esta capa la ejerce **de forma negativa**: **la superficie no declara ningún parámetro** con el que el administrador pueda pedir borradores; **el alcance llega decidido y acá no se ofrece la puerta**. El contrato de uso es [`CU-07`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-07-Exponer-El-Listado-Y-El-Detalle-De-Los-Trabajos.md).

## 3. Criterios de aceptación

- Given un acceso con papel `Alumno`, When se pide el listado, Then vienen sus trabajos con los cuatro estados.
- Given un acceso con papel `Administrador`, When se pide el listado, Then vienen los de la comisión **sin los que están en `Borrador`**.
- Given el punto de acceso, When se recorren sus parámetros, Then **no hay ninguno** con el que se puedan pedir borradores ajenos.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-03, NB-07 (parcial), NB-09 (parcial) |
| CU cubiertos | CU-07 |
| RN que ejerce | **RN-11**, de forma negativa; RN-03 |
| Componente de `05` §3.1 | Superficie de trabajos |
| ¿Decide qué se dice? | **No.** El alcance lo decide `GeometriaFactory-Application` |
| Familia empobrecida | **Sí**, en su camino de rechazo: la del recurso que no se ve |
| BT derivadas | BT-14, BT-18 |
| Tests previstos en 08 | Batería de integración sobre un alumno con un borrador y un pendiente |

## 5. Prioridad y estimación

`Must` por derivar de `F-08` y `F-12`, `Must Have`, y porque el criterio de transición `e` → `f` exige que el listado del administrador **no incluya los que están en estado `Borrador`**.

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

**El listado no arrastra el texto original ni los componentes**, porque la proyección llega **ya separada del detalle** desde el ensamblado de contratos y desde el adaptador, y **esta capa no la recompone** (`05` §6).

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
