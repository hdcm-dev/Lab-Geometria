> **Artefacto archivado — estado `Superado`**
>
> Copia archivada de `US-02015-Enviar-Un-Trabajo-Que-Verifica-Y-Pasa-A-Estado-Pendiente.md` en su versión **1.0**, tomada el 2026-08-25 por el orquestador de migración normativa **antes** de aplicar el corte de la categoría 06 de la fase M4 (`Master-Prompt.md` §5 y §8).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-25
> - **Versión vigente:** [`US-02015-Enviar-Un-Trabajo-Que-Verifica-Y-Pasa-A-Estado-Pendiente.md`](../../US-02015-Enviar-Un-Trabajo-Que-Verifica-Y-Pasa-A-Estado-Pendiente.md)
>
> El cuerpo que sigue **no se modifica**. Lo único que se tocó son **los enlaces relativos**, reescritos dos niveles para que sigan resolviendo desde esta ubicación.

---

# US-02015 — Enviar un trabajo que verifica y pasa a estado `Pendiente`

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-02015-Enviar-Un-Trabajo-Que-Verifica-Y-Pasa-A-Estado-Pendiente.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-02005 Interpretación y verificación del dato del alumno
**Etapa del producto:** `f`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca de dominio (`GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`)**, quiero **que el envío de un trabajo cuyo texto verifica lo lleve a estado `Pendiente`**, para **que enviar sea la única acción de guardado y el alumno no tenga que acordarse de dos botones**.

## 2. Contexto

La capacidad `F-22` del intake §4 declara **enviar** como acción única que interpreta el texto y, si verifica, pasa el trabajo a estado `Pendiente`. `PRODUCT-INTAKE` §4.2 declara como consecuencia aceptada que el alumno **no puede** conservar en borrador un trabajo cuyo texto sí verifica.

## 3. Criterios de aceptación

- Given un trabajo en `Borrador` cuyo resultado de interpretación no trae errores de validación, When se envía, Then pasa a estado `Pendiente`.
- Given ese mismo trabajo con advertencias pero sin errores, When se envía, Then pasa a estado `Pendiente` igual, por `RN-02005`.
- Given un trabajo ya en estado `Pendiente`, When se lo vuelve a enviar, Then se rechaza: el envío parte de `Borrador`, según el modelo de estados de `PRODUCT-INTAKE` §4.2.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00003, NB-00004, NB-00005 |
| CU cubiertos | CU-02008 |
| RN e invariantes que ejerce | RN-02005; INV-04 |
| BT derivadas | BT-02012 |
| Etapa del producto | `f`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba unitaria de la transición admitida con y sin advertencias, y de la rechazada por estado de partida. |

## 5. Prioridad y estimación

`Must` por derivar de `F-22`, `Must Have` en `PRODUCT-INTAKE` §4, y por ser el corazón del criterio de transición `f` → `g` del roadmap §5.2.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Cita por identificador toda regla e invariante que ejerce, sin volver a enunciarla
- [x] Las condiciones de rechazo que produce existen en el catálogo de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Sus tareas técnicas están identificadas y ninguna está bloqueada

## 7. Notas y supuestos

El nombre del archivo `RN-02005-Finalizacion-Sin-Errores-De-Validacion.md` conserva un slug anterior: su corte **se adelantó del cierre al envío** ([`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../../02-Especificacion-Funcional/Especificacion-Funcional.md) §8 punto 3).

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.3 previó con este mismo identificador y este mismo contenido. |
