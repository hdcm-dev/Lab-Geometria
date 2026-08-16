# US-10028 — Cambiar la contraseña obligada tras un reseteo y levantar la marca

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** US-10028-Cambiar-La-Contrasena-Obligada-Y-Levantar-La-Marca.md
**Versión:** 1.2
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master (AG-06)
**Épica:** EP-10004 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Superficie de 03:** `Credencial-Propia`, en su curso de cambio forzado
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **alumno al que le resetearon la contraseña**, quiero **cambiarla en el primer ingreso y recién ahí poder usar el laboratorio**, para **que la clave que el docente me comunicó deje de servir en cuanto elijo la mía**.

## 2. Contexto

`RN-10013` declara que mientras la provisoria no se cambie la cuenta **se autentica y no obtiene sesión de trabajo**, y que al cambiarla la marca se levanta. El caso de uso es [`CU-10003`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-10003-Establecer-Y-Cambiar-La-Contrasena-Propia.md) FA-04. Es una de las **tres** historias que `02` §3.2 describió por contenido: «cambio forzado que levanta la marca».

## 3. Criterios de aceptación

- Given una cuenta con la marca puesta, When la persona ingresa con la provisoria, Then llega al formulario de cambio **en el shell de acceso y sin barra lateral**, y no a ninguna otra ruta.
- Given ese formulario, When la persona cambia su contraseña, Then la marca **se levanta** y a partir de ahí opera con normalidad.
- Given el curso de cambio forzado, When se busca una salida del formulario, Then **no hay ninguna**: es lo único que la cuenta puede hacer.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00002 |
| CU cubiertos | CU-10003 FA-04 |
| Restricciones transversales que la alcanzan | RT-02, RT-12 |
| Componente de `05` §3.1 | Superficies, Armazón y encaminamiento, Sesión y estado del circuito |
| Quién hace cumplir lo que esta historia sólo ofrece | `GeometriaFactory-Application` con su cuarta comprobación transversal, y `GeometriaFactory-Api` con su guardia sobre todos los puntos salvo uno |
| BT derivadas | BT-10007, BT-10013, BT-10014 |
| Tests previstos en 08 | Paso del guion de la etapa `d`, sobre una cuenta con la contraseña reseteada |

## 5. Prioridad y estimación

`Must` por derivar de `F-26` y `F-04`, `Must Have`, y porque el criterio de transición `d` → `e` exige que la cuenta con la contraseña reseteada **se autentique y no obtenga sesión de trabajo**, y que recién al cambiarla opere con normalidad.

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

**Los tres cursos de `Credencial-Propia` son el mismo formulario de tres campos** desde `RN-10016`; lo que distingue a éste es **de dónde se llega y que no hay salida**. `02` §3.1 declara que lo que sí es decisión propia y por eso lleva criterio de aceptación es **el confinamiento**, y que ése no vive en el formulario sino en el guardián: es US-10029.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **Unificación de nomenclatura del reseteo: se resetea la contraseña de la cuenta, no la cuenta.** Corrección pedida por el Product Owner —«ese resetear cuenta hay que corregirlo por resetear clave de cuenta de usuario alumno»— y corregida primero en la fuente, `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.28**: leído literal, «resetear la cuenta» sugiere darla de baja y volver a darla de alta, que es exactamente el remedio que **F-26** vino a reemplazar. Acá se reescriben **2** ocurrencias a «resetear / reseteo **de la contraseña** de la cuenta» y «cuenta **con la contraseña reseteada**». No cambia ninguna regla ni su verificación, y **no se toca ningún identificador** de código de error ni de regla —`RESETEO_ACOTADO_A_CUENTAS_DE_ALUMNO` y `CONTRATO_RESETEO_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` se conservan tal cual—. |
| 1.0 | 2026-08-10 | Emisión inicial. Numera y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 había repartido por necesidad de negocio con este identificador, y que su §3.2 dejó a la categoría 06 para redactar. |
| 1.2 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
