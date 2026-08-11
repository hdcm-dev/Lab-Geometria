# US-03 — Responder con motivo a la cuenta `Pendiente` o `Bloqueado`

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** US-03-Responder-Con-Motivo-A-La-Cuenta-Pendiente-O-Bloqueada.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + API Product Owner (AG-06)
**Épica:** EP-02 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Punto de acceso:** `A-01`, fuera de la guardia
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código de `GeometriaFactory-Web`**, quiero **recibir un motivo cuando la cuenta no admite acceso por su situación**, para **poder decirle a la persona si su cuenta todavía no fue habilitada o si fue bloqueada**.

## 2. Contexto

`RN-06` fija que una cuenta `Pendiente` o `Bloqueado` no obtiene acceso, y `PRODUCT-INTAKE` §17.5.P.5 declara la respuesta **con motivo**, distinta de la genérica de credenciales inválidas. El contrato de uso es [`CU-01`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-01-Canjear-Credenciales-Por-Un-Acceso-Firmado.md).

## 3. Criterios de aceptación

- Given una cuenta en estado `Pendiente` con credenciales correctas, When se intenta el canje, Then la respuesta **declara el motivo** y no emite acceso.
- Given una cuenta `Bloqueado`, When se intenta el canje, Then el motivo es **distinguible** del anterior.
- Given cualquiera de los dos, When se compara con la respuesta de credenciales inválidas, Then **son distintas**: acá el motivo sí se declara, y ahí no.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-02 |
| CU cubiertos | CU-01 |
| RN que ejerce | RN-06; sostiene `INV-06` |
| Componente de `05` §3.1 | Superficie de acceso y credencial propia |
| ¿Decide qué se dice? | **No.** La admisibilidad y su motivo llegan resueltos del dominio |
| Familia empobrecida | **No**, y es deliberado: es el contraejemplo de US-02 |
| BT derivadas | BT-13, BT-16 |
| Tests previstos en 08 | Batería de integración con cuentas en las dos situaciones |

## 5. Prioridad y estimación

`Must` por `RN-06`, y porque el criterio de transición `d` → `e` exige que un alumno cuya cuenta está en estado `Pendiente` reciba **un aviso explícito**.

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

**Que un mismo punto de acceso tenga una respuesta que declara el motivo y otra que deliberadamente no lo declara no es una inconsistencia**: lo que se protege en un caso es la existencia de la cuenta, y en el otro lo que se informa es la situación de una cuenta cuya existencia la persona ya conoce, porque es la suya.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
