# US-22 — Excluir los trabajos en `Borrador` del alcance del administrador

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** US-22-Excluir-Los-Borradores-Del-Alcance-Del-Administrador.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-04 Gestión del trabajo
**Etapa del producto:** `e`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca de dominio (`GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`)**, quiero **un predicado de alcance que deje fuera de la vista del administrador los trabajos en `Borrador`**, para **que el administrador vea la comisión sin el ruido de lo que todavía no fue entregado**.

## 2. Contexto

`RN-11` declara que el administrador **no ve los trabajos en `Borrador`**: no forman parte de su flujo de trabajo. La capacidad `F-12` del intake §4 lo declara `Must Have`. Es el aporte parcial de este proyecto de código a `NB-07`: la consulta que aplica el predicado vive en las capas de arriba ([`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.2).

## 3. Criterios de aceptación

- Given un trabajo en estado `Pendiente`, `Finalizado` o `Rechazado`, When se evalúa el alcance del administrador, Then el trabajo entra en su alcance.
- Given un trabajo en `Borrador`, When se evalúa el alcance del administrador, Then el trabajo **no** entra, por `RN-11`.
- Given un alumno con un borrador y un trabajo en estado `Pendiente`, When se evalúa el alcance del administrador sobre los dos, Then sólo el segundo queda dentro.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-09, NB-07 |
| CU cubiertos | CU-11 |
| RN e invariantes que ejerce | RN-11 |
| BT derivadas | BT-12 |
| Etapa del producto | `e`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba unitaria del predicado por cada uno de los cuatro estados del trabajo. |

## 5. Prioridad y estimación

`Must` por derivar de `F-12`, `Must Have` en `PRODUCT-INTAKE` §4, y porque es criterio de la transición `e` → `f` del roadmap §5.2.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Cita por identificador toda regla e invariante que ejerce, sin volver a enunciarla
- [x] Las condiciones de rechazo que produce existen en el catálogo de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Sus tareas técnicas están identificadas y ninguna está bloqueada

## 7. Notas y supuestos

`RN-11` **no tiene invariante asociado**: es una regla de alcance de consulta y no una condición permanente sobre el estado (`PRODUCT-INTAKE` §17.1.P.2).

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.3 previó con este mismo identificador y este mismo contenido. |
