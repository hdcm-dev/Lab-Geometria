# US-10009 — Ver la lista de cuentas y habilitar, bloquear y rehabilitar, comunicando la provisoria

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** US-10009-Ver-La-Lista-De-Cuentas-Y-Habilitar-Bloquear-Y-Rehabilitar.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master (AG-06)
**Épica:** EP-10004 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Superficie de 03:** `Panel-De-Cuentas`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **administrador**, quiero **ver la lista de cuentas de la comisión con su situación y su marca, y habilitar, bloquear o rehabilitar desde la fila**, para **controlar quién entra al laboratorio sin depender del correo**, y **recibir en pantalla la contraseña provisoria para comunicársela al alumno**.

## 2. Contexto

`NB-00001` pide control de admisión, `F-03` del intake §4 lo declara `Must Have` y `RN-10016` agrega que **habilitar produce la provisoria**. El caso de uso es [`CU-10004`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-10004-Administrar-Las-Cuentas-De-La-Comision.md) y la superficie es `Panel-De-Cuentas`, que aloja las **cinco** operaciones sobre una cuenta.

## 3. Criterios de aceptación

- Given el panel de cuentas, When se lo abre, Then muestra las cuentas con su **situación** y su **marca de cambio de contraseña pendiente**.
- Given una cuenta en estado `Pendiente`, When el administrador la habilita, Then la pantalla le muestra **una contraseña provisoria que él no escribió**, para que se la comunique al alumno.
- Given el formulario de esas operaciones, When se lo inspecciona, Then **no tiene ningún campo de contraseña**: la provisoria la produce el sistema.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00001 |
| CU cubiertos | CU-10004 |
| Restricciones transversales que la alcanzan | RT-01, RT-03, RT-06, RT-09 |
| Componente de `05` §3.1 | Superficies, Servicios de aplicación de front, Cliente tipado |
| Quién hace cumplir lo que esta historia sólo ofrece | La transición la resuelve el dominio; la producción de la provisoria, `GeometriaFactory-Infrastructure` |
| BT derivadas | BT-10008, BT-10011, BT-10013 |
| Tests previstos en 08 | Paso del guion de la etapa `d`, con la habilitación y la comunicación de la provisoria |

## 5. Prioridad y estimación

`Must` por derivar de `F-03` y `F-04`, `Must Have`, y porque el criterio de transición `d` → `e` exige que al habilitar el producto muestre al administrador **una contraseña provisoria que él no escribió**.

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

**La provisoria se muestra una sola vez y no se registra en ninguna traza.** Es lo que `GeometriaFactory-Api` declara sobre el resultado del punto de cambio de situación, y lo que hace que el docente tenga que comunicarla en el momento.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Numera y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 había repartido por necesidad de negocio con este identificador, y que su §3.2 dejó a la categoría 06 para redactar. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
