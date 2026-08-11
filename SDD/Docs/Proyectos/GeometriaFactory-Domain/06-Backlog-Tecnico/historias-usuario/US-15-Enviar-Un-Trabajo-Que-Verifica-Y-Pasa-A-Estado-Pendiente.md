# US-15 — Enviar un trabajo que verifica y pasa a estado `Pendiente`

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** US-15-Enviar-Un-Trabajo-Que-Verifica-Y-Pasa-A-Estado-Pendiente.md
**Versión:** 1.0
**Estado:** Propuesta
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-05 Interpretación y verificación del dato del alumno
**Etapa del producto:** `f`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca de dominio (`GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`)**, quiero **que el envío de un trabajo cuyo texto verifica lo lleve a estado `Pendiente`**, para **que enviar sea la única acción de guardado y el alumno no tenga que acordarse de dos botones**.

## 2. Contexto

La capacidad `F-22` del intake §4 declara **enviar** como acción única que interpreta el texto y, si verifica, pasa el trabajo a estado `Pendiente`. `PRODUCT-INTAKE` §4.2 declara como consecuencia aceptada que el alumno **no puede** conservar en borrador un trabajo cuyo texto sí verifica.

## 3. Criterios de aceptación

- Given un trabajo en `Borrador` cuyo resultado de interpretación no trae errores de validación, When se envía, Then pasa a estado `Pendiente`.
- Given ese mismo trabajo con advertencias pero sin errores, When se envía, Then pasa a estado `Pendiente` igual, por `RN-05`.
- Given un trabajo ya en estado `Pendiente`, When se lo vuelve a enviar, Then se rechaza: el envío parte de `Borrador`, según el modelo de estados de `PRODUCT-INTAKE` §4.2.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-03, NB-04, NB-05 |
| CU cubiertos | CU-08 |
| RN e invariantes que ejerce | RN-05; INV-04 |
| BT derivadas | BT-12 |
| Etapa del producto | `f`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba unitaria de la transición admitida con y sin advertencias, y de la rechazada por estado de partida. |

## 5. Prioridad y estimación

`Must` por derivar de `F-22`, `Must Have` en `PRODUCT-INTAKE` §4, y por ser el corazón del criterio de transición `f` → `g` del roadmap §5.2.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Cita por identificador toda regla e invariante que ejerce, sin volver a enunciarla
- [x] Las condiciones de rechazo que produce existen en el catálogo de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Sus tareas técnicas están identificadas y ninguna está bloqueada

## 7. Notas y supuestos

El nombre del archivo `RN-05-Finalizacion-Sin-Errores-De-Validacion.md` conserva un slug anterior: su corte **se adelantó del cierre al envío** ([`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §8 punto 3).

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.3 previó con este mismo identificador y este mismo contenido. |
