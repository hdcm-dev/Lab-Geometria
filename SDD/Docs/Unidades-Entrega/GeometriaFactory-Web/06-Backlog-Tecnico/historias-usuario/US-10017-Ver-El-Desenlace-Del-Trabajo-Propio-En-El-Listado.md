# US-10017 — Ver el desenlace del trabajo propio en el listado, y el comentario al abrirlo

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** US-10017-Ver-El-Desenlace-Del-Trabajo-Propio-En-El-Listado.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master (AG-06)
**Épica:** EP-10008 Desenlace de la entrega
**Etapa del producto:** `h`
**Superficie de 03:** `Panel-De-Trabajos-Del-Alumno` y `Vista-De-Trabajo`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **alumno**, quiero **ver el desenlace de mi trabajo en mi propio listado y el comentario del docente al abrirlo**, para **que la entrega tenga un cierre explícito y no quede sólo depositada**.

## 2. Contexto

`NB-00009` pide desenlace explícito. Los casos de uso son [`CU-10006`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-10006-Consultar-El-Listado-Propio-Y-Operar-Sobre-El-Borrador.md) para el estado en el listado y [`CU-10007`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-10007-Abrir-Un-Trabajo-Y-Explorarlo-En-Escena-Y-Arbol.md) para el comentario al abrir. El roadmap 1.1 precisó esta distinción en sus dos apariciones, y la causa es del ensamblado de contratos: **el comentario no viaja en la proyección de listado**.

## 3. Criterios de aceptación

- Given un trabajo propio en `Finalizado` o en `Rechazado`, When se mira el listado, Then el **desenlace** está a la vista.
- Given ese mismo trabajo, When se lo abre desde el listado, Then el **comentario** del administrador aparece, si lo hay.
- Given un trabajo rechazado **sin** comentario, When se lo abre, Then el estado informa que no fue aceptado y **no hay explicación escrita**: el comentario es opcional en los dos desenlaces, y es una consecuencia que el Product Owner aceptó explícitamente.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00009, NB-00003 |
| CU cubiertos | CU-10006, CU-10007 |
| Restricciones transversales que la alcanzan | RT-06, RT-07 |
| Componente de `05` §3.1 | Superficies, Representaciones reutilizadas |
| Quién hace cumplir lo que esta historia sólo ofrece | La terminalidad y la exclusividad de la facultad son invariantes del dominio |
| BT derivadas | BT-10009, BT-10011 |
| Tests previstos en 08 | Paso del guion de la etapa `h`, con desenlace con comentario y sin comentario |

## 5. Prioridad y estimación

`Must` por derivar de `F-21` y `F-23`, `Must Have`, y porque el criterio de transición `h` → `i…` exige que el alumno vea **el desenlace en su propio listado** y **el comentario al abrir el trabajo** desde ese listado.

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

**El comentario no es una observación y la superficie no los mezcla.** Las observaciones son del validador sobre la geometría y son varias; el comentario del administrador es **uno solo, opcional y sin historial**. La categoría 03 ya ubicó el comentario **al abrir el trabajo** y no en el listado.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Numera y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 había repartido por necesidad de negocio con este identificador, y que su §3.2 dejó a la categoría 06 para redactar. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
