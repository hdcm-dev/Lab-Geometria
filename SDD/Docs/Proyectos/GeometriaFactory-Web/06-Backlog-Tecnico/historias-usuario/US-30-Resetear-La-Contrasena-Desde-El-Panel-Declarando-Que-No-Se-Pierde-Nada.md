# US-30 — Resetear la contraseña desde el panel, declarando que no se pierde ningún trabajo

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** US-30-Resetear-La-Contrasena-Desde-El-Panel-Declarando-Que-No-Se-Pierde-Nada.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master (AG-06)
**Épica:** EP-04 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Superficie de 03:** `Panel-De-Cuentas`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **administrador**, quiero **resetear la contraseña de un alumno desde el mismo panel donde lo habilito, y que la pantalla me diga que no se pierde ningún trabajo**, para **resolver un olvido de contraseña sin darle de baja y sin destruir su entrega**.

## 2. Contexto

`F-26` del intake §4 es `Must Have` y **cierra un agujero que hacía inutilizable el laboratorio al primer olvido**: el único camino declarado era dar de baja y volver a dar de alta, y por `RN-07` eso eliminaba todos los trabajos del alumno. El caso de uso es [`CU-04`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-04-Administrar-Las-Cuentas-De-La-Comision.md) FA-06. Es una de las **tres** historias que `02` §3.2 describió por contenido.

## 3. Criterios de aceptación

- Given una cuenta de alumno, When el administrador acciona el reseteo, Then la pantalla le muestra **una contraseña provisoria que él no escribió** y el **panel no tiene ningún campo de contraseña**.
- Given la superficie del reseteo, When se la mira **antes del intento**, Then declara que la operación **conserva la cuenta y todos sus trabajos**.
- Given una cuenta en estado `Bloqueado` y otra en estado `Pendiente`, When se las resetea, Then **el reseteo procede en las dos** y la superficie **no condiciona la operación al estado de la cuenta**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-01, NB-02 |
| CU cubiertos | CU-04 FA-06 |
| Restricciones transversales que la alcanzan | RT-01, RT-03, RT-09, RT-12 |
| Componente de `05` §3.1 | Superficies, Servicios de aplicación de front, Cliente tipado |
| Quién hace cumplir lo que esta historia sólo ofrece | La producción de la provisoria es de `GeometriaFactory-Infrastructure`; la conservación de la cuenta y sus trabajos, de `GeometriaFactory-Application` |
| BT derivadas | BT-11, BT-13 |
| Tests previstos en 08 | Paso del guion de la etapa `d`, sobre un alumno con trabajos en tres estados distintos |

## 5. Prioridad y estimación

`Must` por derivar de `F-26`, `Must Have`, y porque la transición `d` → `e` incorpora **cinco** criterios verificables del reseteo, entre ellos que el panel **no tenga campo de contraseña** y que dos reseteos consecutivos produzcan **provisorias distintas**.

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

**Declarar en la superficie, antes del intento, que el reseteo no pierde ningún trabajo es lo que `05` §10.3 asigna a esta pieza por `RN-12`**, y es lo que corrige la fricción más cara que el producto tenía. US-10 declara lo contrario para la baja, y las dos operaciones viven en el mismo panel.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Numera y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 había repartido por necesidad de negocio con este identificador, y que su §3.2 dejó a la categoría 06 para redactar. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
