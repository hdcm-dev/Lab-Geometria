# US-00022 — Devolver el detalle con piezas, componentes, observaciones y comentario

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** US-00022-Devolver-El-Detalle-Con-Piezas-Componentes-Observaciones-Y-Comentario.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master + API Product Owner (AG-06)
**Épica:** EP-00004 Gestión del trabajo
**Etapa del producto:** `e`
**Punto de acceso:** `A-14`, bajo la guardia
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código de `GeometriaFactory-Web`**, quiero **obtener el detalle de un trabajo con sus piezas, sus componentes, sus observaciones y el comentario del administrador**, para **tener todo lo que la vista de trabajo necesita en una sola petición**.

## 2. Contexto

`NB-00006` recibe de esta capa que **las piezas, sus componentes y el texto original lleguen al otro lado del proceso** (`02` §7.2), y `NB-00009` que el comentario cruce. El contrato de uso es [`CU-00007`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00007-Exponer-El-Listado-Y-El-Detalle-De-Los-Trabajos.md).

## 3. Criterios de aceptación

- Given un trabajo interpretado, When se pide su detalle, Then vienen sus piezas **con su posición**, sus componentes, sus observaciones y el texto original.
- Given un trabajo con desenlace, When se pide su detalle, Then viene además el **comentario**, si lo hay.
- Given una observación, When cruza la frontera, Then **su índice de figura y su campo no se recortan**: producirla es de las capas de adentro, **no perderla al traducir es de acá**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00003, NB-00005 (parcial), NB-00006 (parcial), NB-00007 (parcial), NB-00009 (parcial) |
| CU cubiertos | CU-00007 |
| RN que ejerce | RN-00009, RN-00003 |
| Componente de `05` §3.1 | Superficie de trabajos, Traductor de motivos y códigos |
| ¿Decide qué se dice? | **No.** Los tipos son del ensamblado de contratos y esta capa **no agrega ni recorta campos** |
| Familia empobrecida | **Sí**, en su camino de rechazo |
| BT derivadas | BT-00008, BT-00018 |
| Tests previstos en 08 | Batería de integración con un trabajo interpretado y con uno con desenlace |

## 5. Prioridad y estimación

`Must` porque el criterio de transición `g` → `h` exige que **el administrador abra cualquier trabajo que ve y encuentre exactamente lo mismo que vio el alumno**, y ese «lo mismo» viaja por acá.

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

**Este punto existe desde la etapa `e` y no cambia en la `g`**, y es el motivo por el que la etapa `g` **no produce épica** en este proyecto de código: todo lo que la visualización necesita de esta superficie ya está expuesto, y el dibujo ocurre del otro lado de la frontera, en el navegador.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
