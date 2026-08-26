> **Artefacto archivado — estado `Superado`**
>
> Copia archivada de `US-00009-Cambiar-La-Contrasena-Propia-Con-La-Provisoria-Como-Vigente.md` en su versión **1.2**, tomada el 2026-08-25 por el orquestador de migración normativa **antes** de aplicar el corte de la categoría 06 de la fase M4 (`Master-Prompt.md` §5 y §8).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.2
> - **Fecha de archivado:** 2026-08-25
> - **Versión vigente:** [`US-00009-Cambiar-La-Contrasena-Propia-Con-La-Provisoria-Como-Vigente.md`](../../US-00009-Cambiar-La-Contrasena-Propia-Con-La-Provisoria-Como-Vigente.md)
>
> El cuerpo que sigue **no se modifica**. Lo único que se tocó son **los enlaces relativos**, reescritos dos niveles para que sigan resolviendo desde esta ubicación.

---

# US-00009 — Cambiar la contraseña propia con la provisoria como vigente

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-00009-Cambiar-La-Contrasena-Propia-Con-La-Provisoria-Como-Vigente.md
**Versión:** 1.2
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master + API Product Owner (AG-06)
**Épica:** EP-00003 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Punto de acceso:** `A-05`, bajo la guardia, y **es su única excepción**
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1)

## 1. Historia

Como **código de `GeometriaFactory-Web`**, quiero **que el mismo punto de cambio de contraseña sirva cuando la vigente es la provisoria**, para **que el primer ingreso y el cambio posterior a un reseteo recorran un solo camino**.

## 2. Contexto

`RN-00016` unificó los dos mecanismos de credencial inicial del producto, y `02` §11 registra que **el punto abierto más importante de esta categoría quedó cerrado** por esa vía: la escritura anónima de contraseña **se suprimió** en lugar de resolverse, y `A-04` quedó **retirado y no se recicla**. El contrato de uso es [`CU-00022`](../../../../02-Especificacion-Funcional/Casos-De-Uso/CU-00022-Ingresar-Al-Laboratorio-Y-Sostener-La-Sesion.md).

## 3. Criterios de aceptación

- Given una cuenta con la marca puesta y su provisoria, When cambia su contraseña presentando la provisoria como vigente, Then el cambio procede.
- Given ese mismo punto, When lo pide una cuenta con la marca puesta, Then **la guardia del cambio pendiente lo deja pasar**: es su única excepción declarada.
- Given cualquier otro punto, When lo pide esa misma cuenta, Then se rechaza.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00002 |
| CU cubiertos | CU-00003 |
| RN que ejerce | RN-00013, RN-00016 |
| Componente de `05` §3.1 | Superficie de acceso y credencial propia, Guardia de admisión |
| ¿Decide qué se dice? | **No.** Levantar la marca es de la capa de aplicación |
| Familia empobrecida | **No** |
| BT derivadas | BT-00011, BT-00012, BT-00016 |
| Tests previstos en 08 | Batería de integración sobre una cuenta con la contraseña reseteada |

## 5. Prioridad y estimación

`Must` por derivar de `F-04` y `F-26`, `Must Have`, y porque el criterio de transición `d` → `e` exige que **ningún punto de acceso acepte un correo y una contraseña nueva sin credencial**.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el punto de acceso que la realiza y el componente de `05` §3.1 que lo aloja
- [x] Declara si su punto está bajo la guardia, y si no lo está, cuál de las cuatro ausencias declaradas es
- [x] Toda condición que transporta es uno de los diecisiete códigos vivos del contrato, con su destino declarado
- [x] Declara que no decide qué se dice
- [x] Declara si su respuesta pertenece a una de las tres familias deliberadamente empobrecidas

## 7. Notas y supuestos

**`A-04` está retirado y no se recicla.** Exponía la escritura de contraseña sin credencial, y `RN-00016` **suprimió la operación** en lugar de resolverla. Reciclarlo volvería a abrir el agujero que el punto abierto de esta categoría había levantado.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **Unificación de nomenclatura del reseteo: se resetea la contraseña de la cuenta, no la cuenta.** Corrección pedida por el Product Owner —«ese resetear cuenta hay que corregirlo por resetear clave de cuenta de usuario alumno»— y corregida primero en la fuente, `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.28**: leído literal, «resetear la cuenta» sugiere darla de baja y volver a darla de alta, que es exactamente el remedio que **F-26** vino a reemplazar. Acá se reescriben **1** ocurrencia a «resetear / reseteo **de la contraseña** de la cuenta» y «cuenta **con la contraseña reseteada**». No cambia ninguna regla ni su verificación, y **no se toca ningún identificador** de código de error ni de regla —`RESETEO_ACOTADO_A_CUENTAS_DE_ALUMNO` y `CONTRATO_RESETEO_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` se conservan tal cual—. |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
| 1.2 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
