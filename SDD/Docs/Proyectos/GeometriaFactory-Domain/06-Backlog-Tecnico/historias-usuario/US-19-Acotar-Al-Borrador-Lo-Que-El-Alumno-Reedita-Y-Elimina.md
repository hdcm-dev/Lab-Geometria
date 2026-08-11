# US-19 — Acotar al estado `Borrador` lo que el alumno reedita y elimina

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** US-19-Acotar-Al-Borrador-Lo-Que-El-Alumno-Reedita-Y-Elimina.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-04 Gestión del trabajo
**Etapa del producto:** `e`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca de dominio (`GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`)**, quiero **que la reedición y la eliminación por el alumno sólo procedan sobre un trabajo propio en `Borrador`**, para **que un trabajo ya enviado no se pueda modificar ni borrar por detrás de la revisión**.

## 2. Contexto

`RN-04` declara que el alumno elimina sus trabajos **sólo en `Borrador`**, e `INV-03` lo expresa deliberadamente acotado al alumno, porque el borrado del administrador alcanza cualquier estado. La capacidad `F-07` del intake §4 lo declara `Must Have`.

## 3. Criterios de aceptación

- Given un trabajo propio en `Borrador`, When el alumno lo elimina, Then la eliminación procede.
- Given un trabajo propio en estado `Pendiente`, `Finalizado` o `Rechazado`, When el alumno intenta eliminarlo, Then se rechaza.
- Given un trabajo ajeno en `Borrador`, When el alumno intenta eliminarlo, Then se rechaza con la misma condición que un trabajo inexistente, por `RN-03`.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-03 |
| CU cubiertos | CU-09 |
| RN e invariantes que ejerce | RN-03, RN-04; INV-02, INV-03 |
| BT derivadas | BT-12 |
| Etapa del producto | `e`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba unitaria por estado y por pertenencia, con la combinación de las dos guardas. |

## 5. Prioridad y estimación

`Must` por derivar de `F-07`, `Must Have` en `PRODUCT-INTAKE` §4, y porque el roadmap §5.2 lo verifica en la transición `e` → `f` forzando la petición contra el servicio de datos.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Cita por identificador toda regla e invariante que ejerce, sin volver a enunciarla
- [x] Las condiciones de rechazo que produce existen en el catálogo de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Sus tareas técnicas están identificadas y ninguna está bloqueada

## 7. Notas y supuestos

`INV-03` está acotado al alumno **por decisión declarada** del 2026-08-08 (`PRODUCT-INTAKE` §17.1.P.2): el enunciado anterior habría quedado falso al ampliarse el borrado del administrador.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.3 previó con este mismo identificador y este mismo contenido. |
