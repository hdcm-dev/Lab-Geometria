# US-10007 — Recorrer el mismo formulario de tres campos en los tres cursos de la credencial

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** US-10007-Recorrer-El-Mismo-Formulario-En-Los-Tres-Cursos-De-La-Credencial.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master (AG-06)
**Épica:** EP-10004 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Superficie de 03:** `Credencial-Propia`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **alumno que entra por primera vez con la provisoria que le comunicaron**, quiero **cambiarla en el mismo formulario con el que después voy a cambiarla voluntariamente**, para **no tener que aprender dos pantallas distintas para la misma cosa**.

## 2. Contexto

`RN-10016` unificó los dos mecanismos de credencial inicial del producto: habilitar produce la provisoria, con las mismas propiedades y el mismo tratamiento que la del reseteo. `02` §3 declara en consecuencia que **los tres cursos son el mismo formulario y el mismo contrato** desde `PRODUCT-INTAKE` 1.13. El caso de uso es [`CU-10003`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-10003-Establecer-Y-Cambiar-La-Contrasena-Propia.md).

## 3. Criterios de aceptación

- Given un alumno recién habilitado y la provisoria que el docente le comunicó, When ingresa, Then llega al formulario de tres campos y **presenta la provisoria como vigente**.
- Given los tres cursos —primer ingreso, cambio voluntario y cambio obligado—, When se comparan sus formularios, Then son **el mismo**: tres campos, mismo contrato.
- Given el curso de primer ingreso, When se busca en el producto un punto que acepte un correo y una contraseña nueva **sin credencial**, Then no hay ninguno.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00002 |
| CU cubiertos | CU-10003 |
| Restricciones transversales que la alcanzan | RT-02, RT-12 |
| Componente de `05` §3.1 | Superficies, Armazón y encaminamiento |
| Quién hace cumplir lo que esta historia sólo ofrece | `GeometriaFactory-Api`, que retiró el punto de acceso que exponía la escritura anónima de contraseña, y `GeometriaFactory-Application`, que verifica la marca |
| BT derivadas | BT-10007, BT-10013 |
| Tests previstos en 08 | Paso del guion de la etapa `d`, sobre el circuito de primer ingreso |

## 5. Prioridad y estimación

`Must` por derivar de `F-04`, `Must Have`, y porque el criterio de transición `d` → `e` exige que **ningún punto de acceso del producto acepte un correo y una contraseña nueva sin credencial**.

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

**El título del caso de uso de 02 se conservó aunque su alcance cambió**, por estabilidad de citación (`02` 1.6). Esta historia describe el estado vigente: un solo formulario para los tres cursos.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Numera y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 había repartido por necesidad de negocio con este identificador, y que su §3.2 dejó a la categoría 06 para redactar. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
