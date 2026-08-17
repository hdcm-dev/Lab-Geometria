# US-00017 — Enviar un trabajo nuevo y recibir el estado que la interpretación decidió

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-00017-Enviar-Un-Trabajo-Nuevo-Y-Recibir-El-Estado-Que-La-Interpretacion-Decidio.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master + API Product Owner (AG-06)
**Épica:** EP-00005 Interpretación y verificación del dato del alumno
**Etapa del producto:** `f`
**Punto de acceso:** `A-10`, bajo la guardia
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código de `GeometriaFactory-Web`**, quiero **enviar un trabajo y recibir en una respuesta exitosa el estado que la interpretación decidió**, para **poder mostrarle a la persona si su trabajo quedó pendiente de revisión o en borrador con sus errores**.

## 2. Contexto

`F-22` del intake §4 declara `Must Have` el envío como **acción única de guardado**. `02` §6 declara que **`RN-00005` no tiene tramo acá** y explica la trampa: un envío cuyo texto no verifica **no es un fallo de protocolo**, y llamarla **la confusión más cara de esta capa**. El contrato de uso es [`CU-00026`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00026-Enviar-Un-Trabajo-Y-Ver-Sus-Observaciones.md).

## 3. Criterios de aceptación

- Given un envío cuyo texto verifica, When se lo procesa, Then la respuesta es **exitosa** y transporta el estado `Pendiente`.
- Given un envío cuyo texto **no** verifica, When se lo procesa, Then la respuesta **también es exitosa** y transporta el estado `Borrador` con sus observaciones: **el trabajo se guardó y su estado se decidió; lo que no verifica es el texto, no la petición**.
- Given cualquiera de los dos, When se busca quién decidió el estado, Then lo decidió **el dominio**: esta capa lo transporta.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00003, NB-00004, NB-00009 |
| CU cubiertos | CU-00006 |
| RN que ejerce | RN-00005 **sin tramo acá**, declarado; RN-00008, RN-00009 |
| Componente de `05` §3.1 | Superficie de trabajos |
| ¿Decide qué se dice? | **No.** El estado llega decidido y viaja en una respuesta exitosa |
| Familia empobrecida | **No** |
| BT derivadas | BT-00018, BT-00022 |
| Tests previstos en 08 | Batería de integración con los escenarios `E-1`, `E-3`, `E-5` y `E-8` del intake §20 |

## 5. Prioridad y estimación

`Must` por derivar de `F-22`, `Must Have`, y porque el criterio de transición `f` → `g` exige que **enviar sea la única acción de guardado**, con sus dos desenlaces.

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

**El escenario `E-8` es el caso testigo de esta distinción**: el intake lo fija como **error de validación** con el trabajo en `Borrador`, y para esta capa eso significa que **ese envío responde con éxito**. `05` §9 declara el riesgo contrario con impacto medio: le diría a la persona que su petición estaba mal cuando el trabajo ya quedó guardado.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
