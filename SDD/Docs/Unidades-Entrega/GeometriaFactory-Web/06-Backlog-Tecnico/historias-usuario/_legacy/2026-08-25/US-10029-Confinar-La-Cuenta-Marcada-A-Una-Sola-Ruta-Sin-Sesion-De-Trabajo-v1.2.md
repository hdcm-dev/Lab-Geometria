> **Artefacto archivado — estado `Superado`**
>
> Copia archivada de `US-10029-Confinar-La-Cuenta-Marcada-A-Una-Sola-Ruta-Sin-Sesion-De-Trabajo.md` en su versión **1.2**, tomada el 2026-08-25 por el orquestador de migración normativa **antes** de aplicar el corte de la categoría 06 de la fase M4 (`Master-Prompt.md` §5 y §8).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.2
> - **Fecha de archivado:** 2026-08-25
> - **Versión vigente:** [`US-10029-Confinar-La-Cuenta-Marcada-A-Una-Sola-Ruta-Sin-Sesion-De-Trabajo.md`](../../US-10029-Confinar-La-Cuenta-Marcada-A-Una-Sola-Ruta-Sin-Sesion-De-Trabajo.md)
>
> El cuerpo que sigue **no se modifica**. Lo único que se tocó son **los enlaces relativos**, reescritos dos niveles para que sigan resolviendo desde esta ubicación.

---

# US-10029 — Confinar la cuenta con cambio pendiente a una sola ruta, sin sesión de trabajo

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** US-10029-Confinar-La-Cuenta-Marcada-A-Una-Sola-Ruta-Sin-Sesion-De-Trabajo.md
**Versión:** 1.2
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master (AG-06)
**Épica:** EP-10004 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Superficie de 03:** `Credencial-Propia` y los dos shells
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1)

## 1. Historia

Como **producto**, quiero **que una cuenta con cambio de contraseña pendiente no alcance ninguna ruta que no sea el cambio de su propia contraseña, y que llegue ahí sin sesión de trabajo**, para **que una clave que el administrador conoce no sirva para operar como el alumno**.

## 2. Contexto

`RT-12` de `02` §6 lo declara con todas las letras, y `05` §10.2 lo asigna al **cuarto guardián** del armazón. Los casos de uso son [`CU-10002`](../../../../02-Especificacion-Funcional/Casos-De-Uso/CU-10002-Iniciar-Y-Cerrar-Sesion-Sin-Exponer-La-Credencial.md) FA-07 y [`CU-10003`](../../../../02-Especificacion-Funcional/Casos-De-Uso/CU-10003-Establecer-Y-Cambiar-La-Contrasena-Propia.md) FA-05. Es una de las **tres** historias que `02` §3.2 describió por contenido: «confinamiento de la cuenta con la contraseña reseteada».

## 3. Criterios de aceptación

- Given una cuenta con la marca puesta, When intenta alcanzar cualquier ruta del panel, Then el **cuarto guardián** la desvía al cambio de su propia contraseña.
- Given ese desvío, When se inspecciona la sesión, Then **no hay sesión de trabajo**: el canje reconoce la provisoria y **no emite sesión**.
- Given esa misma cuenta, When alguien fuerza una solicitud sin pasar por la pantalla, Then **el servicio de datos la rechaza igual**: esta pieza **acota lo que se ofrece** y quien lo hace cumplir es el servicio de datos, que verifica la marca en cada solicitud.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00002 |
| CU cubiertos | CU-10002 FA-07, CU-10003 FA-05 |
| Restricciones transversales que la alcanzan | RT-02, RT-09, RT-12 |
| Componente de `05` §3.1 | Armazón y encaminamiento, con su **cuarto guardián**; Sesión y estado del circuito |
| Quién hace cumplir lo que esta historia sólo ofrece | `GeometriaFactory-Application`, con la cuarta comprobación que **corta antes que las otras tres**, y `GeometriaFactory-Api`, que garantiza que ningún punto quede fuera de la guardia |
| BT derivadas | BT-10007, BT-10014 |
| Tests previstos en 08 | Paso del guion de la etapa `d`, con una ruta pedida por dirección directa desde una cuenta marcada |

## 5. Prioridad y estimación

`Must` por `RN-10013` e `INV-09`, y porque el criterio de transición `d` → `e` exige que **cualquier ruta que la cuenta con la contraseña reseteada intente termine en el cambio de contraseña**.

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

**El cuarto guardián se completa en la etapa `d` y no en la `b`**, aunque el armazón se construya allá: hasta la `d` no existe la marca sobre la que decidir. BT-10007 lo declara explícitamente.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **Unificación de nomenclatura del reseteo: se resetea la contraseña de la cuenta, no la cuenta.** Corrección pedida por el Product Owner —«ese resetear cuenta hay que corregirlo por resetear clave de cuenta de usuario alumno»— y corregida primero en la fuente, `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.28**: leído literal, «resetear la cuenta» sugiere darla de baja y volver a darla de alta, que es exactamente el remedio que **F-26** vino a reemplazar. Acá se reescriben **2** ocurrencias a «resetear / reseteo **de la contraseña** de la cuenta» y «cuenta **con la contraseña reseteada**». No cambia ninguna regla ni su verificación, y **no se toca ningún identificador** de código de error ni de regla —`RESETEO_ACOTADO_A_CUENTAS_DE_ALUMNO` y `CONTRATO_RESETEO_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` se conservan tal cual—. |
| 1.0 | 2026-08-10 | Emisión inicial. Numera y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 había repartido por necesidad de negocio con este identificador, y que su §3.2 dejó a la categoría 06 para redactar. |
| 1.2 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
