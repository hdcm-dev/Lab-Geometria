# US-00029 — Responder por el estado del servicio en un punto que no exige acceso

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-00029-Responder-Por-El-Estado-Del-Servicio-Sin-Exigir-Acceso.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master + API Product Owner (AG-06)
**Épica:** EP-00001 Esqueleto ambulante y verificación de viabilidad
**Etapa del producto:** `a`
**Punto de acceso:** `A-16`, **fuera de la guardia**: tiene que poder responder cuando nadie puede autenticarse
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código de `GeometriaFactory-Web` y como el mecanismo de comprobación del despliegue**, quiero **un punto que responda por el estado del servicio sin exigir acceso**, para **que la página de salud del front muestre datos reales y para que la comprobación del despliegue sirva de algo**.

## 2. Contexto

`PRODUCT-INTAKE` §17.1.P.3 · GeometriaFactory-Api declara el punto de salud **consumido por la página de salud del front y por la comprobación del despliegue**, y `02` §11 registra que **la fuente declara su existencia pero no su ruta**. `PT-01.d`, que se mide en la etapa `a`, exige que **una llamada de salud devuelva datos reales del servidor propio**. El contrato de uso es [`CU-00011`](../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-00011-Arrancar-El-Servicio-Y-Dejar-El-Almacen-En-Condiciones.md).

## 3. Criterios de aceptación

- Given un servicio en condiciones, When se consulta el punto de salud, Then responde con **datos reales del servidor propio**.
- Given el punto, When se lo consulta **sin ningún acceso**, Then responde igual: es una de las **cuatro** ausencias declaradas de la guardia.
- Given la respuesta, When se la inspecciona, Then **no lleva dirección de servicio interno, ruta del almacén ni traza**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00008 |
| CU cubiertos | CU-00011 |
| RN que ejerce | — directamente; ejerce `RA-03` |
| Componente de `05` §3.1 | Arranque y salud |
| ¿Decide qué se dice? | **No** |
| Familia empobrecida | **No** |
| BT derivadas | BT-00003, BT-00004, BT-00012 |
| Tests previstos en 08 | `PT-01.d` en la etapa `a`, y la puerta de imagen del pipeline |

## 5. Prioridad y estimación

`Must` porque es lo que hace medible **`PT-01.d`** y lo que la comprobación de despliegue consulta; y porque `NB-00008` **recibe acá su primer tramo propio y no parcial**: es donde el producto **se vuelve alcanzable**.

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

**La ruta de este punto es propuesta derivada y no está declarada por ninguna fuente.** `02` §11 lo registra: las **dos** únicas cosas que una fuente declara de la superficie son el punto de canje, con su ruta, y **la existencia** de este punto. La forma definitiva se valida en el punto de control de la etapa `a`, con BT-00007.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
