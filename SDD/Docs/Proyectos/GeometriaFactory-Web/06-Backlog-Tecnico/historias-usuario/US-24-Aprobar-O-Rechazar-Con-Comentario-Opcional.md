# US-24 — Aprobar o rechazar un trabajo en estado `Pendiente` con comentario opcional

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** US-24-Aprobar-O-Rechazar-Con-Comentario-Opcional.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master (AG-06)
**Épica:** EP-08 Desenlace de la entrega
**Etapa del producto:** `h`
**Superficie de 03:** `Resolucion-Del-Trabajo`, alojada en `Vista-De-Trabajo`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **administrador**, quiero **aprobar o rechazar cada trabajo que recibo y poder dejarle un comentario al alumno**, para **que la entrega tenga un desenlace explícito y no quede sólo depositada**.

## 2. Contexto

`NB-09` pide desenlace explícito; `F-23` y `F-21` del intake §4 son `Must Have`, y `F-21` es **texto libre, sin nota ni escala**: no es una calificación. El caso de uso es [`CU-09`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-09-Resolver-Un-Trabajo-Con-Comentario-Opcional.md), emitido como caso de uso propio porque el desenlace tiene actor exclusivo, precondición de estado y regla propios.

## 3. Criterios de aceptación

- Given un trabajo en estado `Pendiente` y quien mira es el administrador, When aprueba o rechaza, con o sin comentario, Then el desenlace se aplica y el estado resultante es terminal.
- Given un trabajo que **no** está en estado `Pendiente`, o quien mira no es el administrador, When se abre la vista, Then **el bloque de decisión no se aloja**.
- Given un trabajo ya resuelto, When se busca una salida de su estado, Then **la superficie no ofrece ninguna**: los dos estados de cierre son terminales.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-09, NB-07 |
| CU cubiertos | CU-09 |
| Restricciones transversales que la alcanzan | RT-03, RT-07, RT-09 |
| Componente de `05` §3.1 | Superficies, Servicios de aplicación de front |
| Quién hace cumplir lo que esta historia sólo ofrece | La exclusividad de la facultad y la terminalidad son invariantes del dominio |
| BT derivadas | BT-11, BT-13, BT-19 |
| Tests previstos en 08 | Paso del guion de la etapa `h`, con los dos desenlaces y con y sin comentario |

## 5. Prioridad y estimación

`Must` por derivar de `F-21` y `F-23`, `Must Have`, y porque el criterio de transición `h` → `i…` exige que el administrador apruebe y rechace, y que los dos desenlaces funcionen **sin** comentario.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../Product-Backlog.md) §4.1.

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

**`Resolucion-Del-Trabajo` es la única de las once superficies alojada dentro de otra** (`05` §3.4): vive en `Vista-De-Trabajo`, porque el administrador decide sobre un trabajo que está mirando entero, no sobre una fila de un listado.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Numera y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 había repartido por necesidad de negocio con este identificador, y que su §3.2 dejó a la categoría 06 para redactar. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
