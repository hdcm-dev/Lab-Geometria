> **Artefacto archivado — estado `Superado`**
>
> Copia archivada de `US-10023-No-Pedir-Los-Borradores-Y-Responder-No-Encontrado.md` en su versión **1.1**, tomada el 2026-08-25 por el orquestador de migración normativa **antes** de aplicar el corte de la categoría 06 de la fase M4 (`Master-Prompt.md` §5 y §8).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.1
> - **Fecha de archivado:** 2026-08-25
> - **Versión vigente:** [`US-10023-No-Pedir-Los-Borradores-Y-Responder-No-Encontrado.md`](../../US-10023-No-Pedir-Los-Borradores-Y-Responder-No-Encontrado.md)
>
> El cuerpo que sigue **no se modifica**. Lo único que se tocó son **los enlaces relativos**, reescritos dos niveles para que sigan resolviendo desde esta ubicación.

---

# US-10023 — No pedir los trabajos en `Borrador` y responder «no encontrado» al pedirlos por dirección directa

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** US-10023-No-Pedir-Los-Borradores-Y-Responder-No-Encontrado.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master (AG-06)
**Épica:** EP-10005 Gestión del trabajo
**Etapa del producto:** `e`
**Superficie de 03:** `Listado-De-La-Comision`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1)

## 1. Historia

Como **administrador**, quiero **que el listado de la comisión no me traiga los trabajos que los alumnos todavía están armando**, para **revisar sólo lo que me entregaron**, y como **producto**, que pedir uno por dirección directa responda «no encontrado».

## 2. Contexto

`RN-10011` declara que el administrador **no ve los trabajos en `Borrador`**: no forman parte de su flujo de trabajo. El caso de uso es [`CU-10008`](../../../../02-Especificacion-Funcional/Casos-De-Uso/CU-10008-Recorrer-La-Entrega-De-La-Comision.md). `05` §10.3 declara qué hace esta pieza por `RN-10011`: **no los pide**, porque el listado se trae ya acotado.

## 3. Criterios de aceptación

- Given un alumno con un borrador y un trabajo en estado `Pendiente`, When el administrador abre el listado de la comisión, Then ve **sólo el pendiente**.
- Given un borrador pedido por **dirección directa**, When se lo solicita, Then la respuesta es **«no encontrado»** y no «no autorizado».
- Given la superficie, When se busca un control para pedir borradores, Then **no hay ninguno**: la puerta por la que la regla se rompería no se ofrece.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00007, NB-00009 |
| CU cubiertos | CU-10008 |
| Restricciones transversales que la alcanzan | RT-03, RT-07, RT-09 |
| Componente de `05` §3.1 | Superficies, Traductor de condiciones a presentación |
| Quién hace cumplir lo que esta historia sólo ofrece | El recorte lo decide el dominio, y `GeometriaFactory-Api` **no declara ningún parámetro** con el que se pueda pedir un borrador ajeno |
| BT derivadas | BT-10011, BT-10013 |
| Tests previstos en 08 | Paso del guion de la etapa `e`, sobre un alumno con un borrador y un pendiente |

## 5. Prioridad y estimación

`Must` por `RN-10011` y porque el criterio de transición `e` → `f` exige que el listado del administrador **no incluya los que están en estado `Borrador`**.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara la superficie de 03 que la aloja y el componente de `05` §3.1 que la sostiene
- [x] Declara qué restricciones transversales de `02` §6 la alcanzan
- [x] Toda condición que presenta es uno de los diecisiete códigos vivos del contrato, o el camino de ausencia de respuesta
- [x] Ninguna afirmación depende de que la pieza pública haga cumplir una regla
- [x] Se puede maquetar y validar sin servicio de datos

## 7. Notas y supuestos

**Que la respuesta sea «no encontrado» y no «no autorizado» es una decisión de traducción de `GeometriaFactory-Api`**, que declara ese punto como uno de los dos que puede romper una regla hacia afuera sin que ninguna capa de adentro se entere. Esta pieza sólo la presenta.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Numera y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 había repartido por necesidad de negocio con este identificador, y que su §3.2 dejó a la categoría 06 para redactar. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
