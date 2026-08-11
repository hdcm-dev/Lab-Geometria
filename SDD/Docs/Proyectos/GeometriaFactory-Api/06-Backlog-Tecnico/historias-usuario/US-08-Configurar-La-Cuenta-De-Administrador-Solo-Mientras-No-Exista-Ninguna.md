# US-08 — Configurar la cuenta de administrador sólo mientras no exista ninguna

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** US-08-Configurar-La-Cuenta-De-Administrador-Solo-Mientras-No-Exista-Ninguna.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + API Product Owner (AG-06)
**Épica:** EP-02 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Punto de acceso:** `A-03`, **fuera de la guardia**: no hay todavía identidad que pueda autenticarse
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código de `GeometriaFactory-Web`**, quiero **exponer la configuración de la cuenta de administrador y que deje de proceder en cuanto exista una**, para **que el laboratorio tenga quien lo gobierne desde el primer arranque y esa puerta se cierre para siempre**.

## 2. Contexto

`RN-01` declara que existe **exactamente un** administrador y que su alta sólo es posible mientras no exista ninguno; `INV-05` lo sostiene. El contrato de uso es [`CU-03`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-03-Exponer-El-Alta-De-Cuenta-Y-La-Credencial-Propia.md).

## 3. Criterios de aceptación

- Given una instancia sin administrador, When se envía la configuración, Then la cuenta queda constituida y habilitada.
- Given una instancia que **ya tiene** administrador, When se envía de nuevo, Then se traduce a **conflicto de estado** y no procede.
- Given ese rechazo, When se lo inspecciona, Then **no revela nada** de la cuenta de administrador existente.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-01, NB-02 |
| CU cubiertos | CU-03 |
| RN que ejerce | RN-01; sostiene `INV-05` |
| Componente de `05` §3.1 | Superficie de acceso y credencial propia |
| ¿Decide qué se dice? | **No.** La ventana de alta la ejerce la capa de aplicación |
| Familia empobrecida | **No** |
| BT derivadas | BT-13, BT-16 |
| Tests previstos en 08 | Batería de integración con la segunda configuración rechazada |

## 5. Prioridad y estimación

`Must` por `RN-01`, y porque el criterio de transición `c` → `d` exige que el administrador se configure en el primer arranque y **sólo** mientras no exista ninguno.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el punto de acceso que la realiza y el componente de `05` §3.1 que lo aloja
- [x] Declara si su punto está bajo la guardia, y si no lo está, cuál de las cuatro ausencias declaradas es
- [x] Toda condición que transporta es uno de los quince códigos vivos del contrato, con su destino declarado
- [x] Declara que no decide qué se dice
- [x] Declara si su respuesta pertenece a una de las tres familias deliberadamente empobrecidas

## 7. Notas y supuestos

**Es el segundo de los cuatro puntos fuera de la guardia, y su ausencia de acceso tiene un motivo propio**: en el momento en que se ejerce **no hay todavía identidad que pueda autenticarse**. `05` §3.4 lo declara así, punto por punto.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
