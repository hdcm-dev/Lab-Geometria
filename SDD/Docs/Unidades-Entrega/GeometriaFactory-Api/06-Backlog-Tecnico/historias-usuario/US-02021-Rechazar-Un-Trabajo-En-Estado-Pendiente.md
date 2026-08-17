# US-02021 — Rechazar un trabajo en estado `Pendiente`, con comentario opcional

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-02021-Rechazar-Un-Trabajo-En-Estado-Pendiente.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-02006 Desenlace de la entrega
**Etapa del producto:** `h`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca de dominio (`GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`)**, quiero **que el administrador rechace un trabajo en estado `Pendiente` y lo lleve a `Rechazado`, con comentario opcional**, para **que el alumno sepa que su entrega no fue aceptada, sin que el trabajo desaparezca**.

## 2. Contexto

Es el otro desenlace de la capacidad `F-23`. `PRODUCT-INTAKE` §4.2 declara como consecuencia aceptada que `Rechazado` es terminal y que corregir un rechazo significa cargar un trabajo nuevo, quedando el rechazado como registro del intento.

## 3. Criterios de aceptación

- Given un trabajo en estado `Pendiente` y la cuenta de administrador, When se lo rechaza, Then queda en `Rechazado`.
- Given ese trabajo ya en `Rechazado`, When el alumno intenta reeditarlo o eliminarlo, Then se rechaza, por `INV-07` y por `RN-02004`.
- Given un rechazo sin comentario, When se consulta el trabajo, Then el estado informa el desenlace y no hay comentario que leer: el motivo queda a criterio del administrador en cada caso.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00009, NB-00003 |
| CU cubiertos | CU-02010 |
| RN e invariantes que ejerce | RN-02004, RN-02010; INV-07 |
| BT derivadas | BT-02012 |
| Etapa del producto | `h`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba unitaria de la transición con y sin comentario, y de la terminalidad del estado alcanzado. |

## 5. Prioridad y estimación

`Must` por derivar de `F-23` y `F-21`, las dos `Must Have` en `PRODUCT-INTAKE` §4.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Cita por identificador toda regla e invariante que ejerce, sin volver a enunciarla
- [x] Las condiciones de rechazo que produce existen en el catálogo de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Sus tareas técnicas están identificadas y ninguna está bloqueada

## 7. Notas y supuestos

`PRODUCT-INTAKE` §4.2 declara explícitamente que un alumno que rebote varias veces acumula trabajos rechazados que **sólo el administrador** puede quitar. Esa consecuencia la ejerce US-02023.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.3 previó con este mismo identificador y este mismo contenido. |
