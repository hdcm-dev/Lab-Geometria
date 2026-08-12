# US-27 — Aplicar las transformaciones de esquema al arrancar, sobre almacén inexistente

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** US-27-Aplicar-Las-Transformaciones-De-Esquema-Al-Arrancar.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master + API Product Owner (AG-06)
**Épica:** EP-01 Esqueleto ambulante y verificación de viabilidad
**Etapa del producto:** `a`
**Punto de acceso:** Ninguno: es el arranque, previo a atender
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **producto**, quiero **que el arranque del servicio dispare la preparación del almacén antes de atender la primera petición**, para **que el laboratorio se pueda levantar desde cero sin ningún paso manual de despliegue**.

## 2. Contexto

`02` §4 declara que **aplicar las transformaciones al arrancar es de acá como disparo**, y que **la transformación la ejecuta el adaptador** de `GeometriaFactory-Infrastructure`. `PT-04`, que se mide en la etapa `a`, exige que la imagen **arranque, aplique sus actualizaciones de esquema sobre base vacía y responda salud**. El contrato de uso es [`CU-11`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-11-Arrancar-El-Servicio-Y-Dejar-El-Almacen-En-Condiciones.md).

## 3. Criterios de aceptación

- Given un almacén inexistente, When arranca el servicio, Then las transformaciones se aplican **solas**, sin paso manual, y recién entonces el servicio escucha.
- Given ese arranque, When se lo cronometra, Then aplica las transformaciones y responde salud dentro del tiempo declarado, que viene **rotulado como asunción** y se usa como vigente.
- Given el orden del arranque, When se lo inspecciona, Then es **en dos fases**: primero se construye el grafo, después se prepara el almacén, y **recién entonces se escucha**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-03, NB-08 |
| CU cubiertos | CU-11 |
| RN que ejerce | — |
| Componente de `05` §3.1 | Arranque y salud |
| ¿Decide qué se dice? | **No.** La transformación la ejecuta el adaptador; acá es el **disparo** |
| Familia empobrecida | **No** |
| BT derivadas | BT-03, BT-04 |
| Tests previstos en 08 | Puerta de imagen del pipeline: se construye, arranca, aplica sobre almacén vacío y responde salud |

## 5. Prioridad y estimación

`Must` porque **es `PT-04`**, y una puerta que no pasa **detiene la planificación de las etapas que dependen de ella**.

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

**El tiempo de arranque en frío es uno de los cinco valores rotulados [ASUNCIÓN]** de `05` §8, y se usa como vigente. Es `PA-10` de [`../Product-Backlog.md`](../Product-Backlog.md) §6, elevado con BT-25, y **este backlog no inventa otro número**.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
