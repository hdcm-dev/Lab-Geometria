> **Artefacto archivado — estado `Superado`**
>
> Copia archivada de `US-10016-Reeditar-Y-Eliminar-Solo-En-Borrador-Sin-Dibujar-El-Control.md` en su versión **1.1**, tomada el 2026-08-25 por el orquestador de migración normativa **antes** de aplicar el corte de la categoría 06 de la fase M4 (`Master-Prompt.md` §5 y §8).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.1
> - **Fecha de archivado:** 2026-08-25
> - **Versión vigente:** [`US-10016-Reeditar-Y-Eliminar-Solo-En-Borrador-Sin-Dibujar-El-Control.md`](../../US-10016-Reeditar-Y-Eliminar-Solo-En-Borrador-Sin-Dibujar-El-Control.md)
>
> El cuerpo que sigue **no se modifica**. Lo único que se tocó son **los enlaces relativos**, reescritos dos niveles para que sigan resolviendo desde esta ubicación.

---

# US-10016 — Reeditar y eliminar sólo en `Borrador`, sin dibujar el control cuando no corresponde

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** US-10016-Reeditar-Y-Eliminar-Solo-En-Borrador-Sin-Dibujar-El-Control.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master (AG-06)
**Épica:** EP-10005 Gestión del trabajo
**Etapa del producto:** `e`
**Superficie de 03:** `Panel-De-Trabajos-Del-Alumno`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1)

## 1. Historia

Como **alumno**, quiero **poder reeditar y eliminar mis trabajos mientras están en `Borrador` y no ver esos controles cuando no corresponde**, para **no intentar algo que el sistema me va a rechazar**.

## 2. Contexto

`RN-10004` acota la eliminación del alumno al estado `Borrador`. El caso de uso es [`CU-10006`](../../../../02-Especificacion-Funcional/Casos-De-Uso/CU-10006-Consultar-El-Listado-Propio-Y-Operar-Sobre-El-Borrador.md). `05` §10.3 declara qué hace esta pieza por `RN-10004`: **no dibujar el control** cuando el estado no lo admite, **en lugar de dibujarlo inhabilitado**.

## 3. Criterios de aceptación

- Given un trabajo en `Borrador`, When se lo mira en el panel, Then los controles de reeditar y eliminar están disponibles y funcionan.
- Given un trabajo en cualquier otro estado, When se lo mira, Then esos controles **no se dibujan**, y no se dibujan inhabilitados.
- Given una solicitud de eliminación **forzada sin pasar por la pantalla** sobre un trabajo que no está en `Borrador`, When se la envía, Then **el servicio de datos la rechaza igual**: la ausencia del control no es la defensa.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00003 |
| CU cubiertos | CU-10006 |
| Restricciones transversales que la alcanzan | RT-03, RT-09 |
| Componente de `05` §3.1 | Superficies, Servicios de aplicación de front |
| Quién hace cumplir lo que esta historia sólo ofrece | `GeometriaFactory-Application` con la verificación de pertenencia, y `GeometriaFactory-Api`, contra cuya superficie se fuerza la petición |
| BT derivadas | BT-10011, BT-10013 |
| Tests previstos en 08 | Paso del guion de la etapa `e`, **más** la prueba forzada contra la superficie, que la fuente exige explícitamente |

## 5. Prioridad y estimación

`Must` por derivar de `F-07`, `Must Have`, y porque el criterio de transición `e` → `f` exige verificar la acotación **forzando la petición al servicio de datos, no sólo por la interfaz**.

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

**Es el ejemplo más claro de por qué esta pieza no puede ser la última defensa de ninguna regla** (`02` §5): la superficie acota lo que ofrece, y el criterio de verificación del producto exige justamente saltear la superficie para comprobar que la regla se sostiene sin ella.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Numera y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 había repartido por necesidad de negocio con este identificador, y que su §3.2 dejó a la categoría 06 para redactar. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
