# US-02 — Rechazar el registro con un correo ya usado, sin revelar de quién es

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** US-02-Rechazar-El-Registro-Con-Un-Correo-Ya-Usado.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master (AG-06)
**Épica:** EP-04 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Superficie de 03:** `Registro-De-Cuenta`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **alumno de la comisión**, quiero **saber que el correo que escribí ya está registrado**, para **usar otro o darme cuenta de que ya me había registrado antes**, y como **producto**, no revelar nada de la cuenta que lo ocupa.

## 2. Contexto

`RN-02` fija que el correo del alumno es único. El caso de uso es [`CU-01`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-01-Registrar-La-Cuenta-De-Alumno.md). `05` §10.3 declara qué hace esta pieza por `RN-02`: presentar el rechazo **como error de operación, sin revelar de quién es el correo**.

## 3. Criterios de aceptación

- Given un correo ya registrado, When la persona envía el formulario, Then la superficie muestra el rechazo con **qué pasó, por qué y qué hacer**, y el formulario conserva lo escrito.
- Given ese mensaje, When se lo inspecciona, Then **no declara la situación ni el papel** de la cuenta que ocupa el correo, y **no lleva dirección de servicio, ruta de datos ni traza**.
- Given dos correos ocupados por cuentas en situaciones distintas, When se comparan los dos rechazos, Then son **indistinguibles**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-01, NB-02 |
| CU cubiertos | CU-01 |
| Restricciones transversales que la alcanzan | RT-03, RT-07 |
| Componente de `05` §3.1 | Traductor de condiciones a presentación, Superficies |
| Quién hace cumplir lo que esta historia sólo ofrece | La unicidad la sostienen la capa de aplicación y el almacén; acá sólo se presenta el rechazo |
| BT derivadas | BT-13 |
| Tests previstos en 08 | Inspección del traductor sobre el código correspondiente, y comparación de las dos respuestas |

## 5. Prioridad y estimación

`Must` por `RN-02`, y porque el mensaje es una de las **tres familias deliberadamente empobrecidas** que `GeometriaFactory-Api` declara: correo ya registrado sin declarar la situación ni el papel de la cuenta que lo ocupa.

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

**El traductor de condiciones es el único lugar por el que un mensaje llega a la persona** (`05` §7), y por eso es también el único lugar donde `RA-03` se puede verificar en un solo punto.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Numera y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 había repartido por necesidad de negocio con este identificador, y que su §3.2 dejó a la categoría 06 para redactar. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
