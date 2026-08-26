> **Artefacto archivado — estado `Superado`**
>
> Copia archivada de `US-00003-Responder-Con-Motivo-A-La-Cuenta-Pendiente-O-Bloqueada.md` en su versión **1.1**, tomada el 2026-08-25 por el orquestador de migración normativa **antes** de aplicar el corte de la categoría 06 de la fase M4 (`Master-Prompt.md` §5 y §8).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.1
> - **Fecha de archivado:** 2026-08-25
> - **Versión vigente:** [`US-00003-Responder-Con-Motivo-A-La-Cuenta-Pendiente-O-Bloqueada.md`](../../US-00003-Responder-Con-Motivo-A-La-Cuenta-Pendiente-O-Bloqueada.md)
>
> El cuerpo que sigue **no se modifica**. Lo único que se tocó son **los enlaces relativos**, reescritos dos niveles para que sigan resolviendo desde esta ubicación.

---

# US-00003 — Responder con motivo a la cuenta `Pendiente` o `Bloqueado`

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-00003-Responder-Con-Motivo-A-La-Cuenta-Pendiente-O-Bloqueada.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master + API Product Owner (AG-06)
**Épica:** EP-00002 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Punto de acceso:** `A-01`, fuera de la guardia
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1)

## 1. Historia

Como **código de `GeometriaFactory-Web`**, quiero **recibir un motivo cuando la cuenta no admite acceso por su situación**, para **poder decirle a la persona si su cuenta todavía no fue habilitada o si fue bloqueada**.

## 2. Contexto

`RN-00006` fija que una cuenta `Pendiente` o `Bloqueado` no obtiene acceso, y `PRODUCT-INTAKE` §17.1.P.5 · GeometriaFactory-Api declara la respuesta **con motivo**, distinta de la genérica de credenciales inválidas. El contrato de uso es [`CU-00022`](../../../../02-Especificacion-Funcional/Casos-De-Uso/CU-00022-Ingresar-Al-Laboratorio-Y-Sostener-La-Sesion.md).

## 3. Criterios de aceptación

- Given una cuenta en estado `Pendiente` con credenciales correctas, When se intenta el canje, Then la respuesta **declara el motivo** y no emite acceso.
- Given una cuenta `Bloqueado`, When se intenta el canje, Then el motivo es **distinguible** del anterior.
- Given cualquiera de los dos, When se compara con la respuesta de credenciales inválidas, Then **son distintas**: acá el motivo sí se declara, y ahí no.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00002 |
| CU cubiertos | CU-00001 |
| RN que ejerce | RN-00006; sostiene `INV-06` |
| Componente de `05` §3.1 | Superficie de acceso y credencial propia |
| ¿Decide qué se dice? | **No.** La admisibilidad y su motivo llegan resueltos del dominio |
| Familia empobrecida | **No**, y es deliberado: es el contraejemplo de US-00002 |
| BT derivadas | BT-00013, BT-00016 |
| Tests previstos en 08 | Batería de integración con cuentas en las dos situaciones |

## 5. Prioridad y estimación

`Must` por `RN-00006`, y porque el criterio de transición `d` → `e` exige que un alumno cuya cuenta está en estado `Pendiente` reciba **un aviso explícito**.

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

**Que un mismo punto de acceso tenga una respuesta que declara el motivo y otra que deliberadamente no lo declara no es una inconsistencia**: lo que se protege en un caso es la existencia de la cuenta, y en el otro lo que se informa es la situación de una cuenta cuya existencia la persona ya conoce, porque es la suya.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
