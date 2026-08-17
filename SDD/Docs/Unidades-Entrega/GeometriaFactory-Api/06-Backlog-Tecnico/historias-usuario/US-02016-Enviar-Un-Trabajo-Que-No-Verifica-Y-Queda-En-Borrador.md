# US-02016 — Enviar un trabajo que no verifica y queda en `Borrador` con sus errores

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-02016-Enviar-Un-Trabajo-Que-No-Verifica-Y-Queda-En-Borrador.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-02005 Interpretación y verificación del dato del alumno
**Etapa del producto:** `f`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca de dominio (`GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`)**, quiero **que el envío de un trabajo cuyo texto no verifica lo deje en `Borrador` con sus errores localizados**, para **que el alumno no pierda lo cargado y sepa exactamente qué corregir**.

## 2. Contexto

Es la otra mitad de la capacidad `F-22` del intake §4. `RN-02005` declara que un trabajo no pasa a estado `Pendiente` con errores de interpretación, e `INV-04` lo expresa como condición permanente.

## 3. Criterios de aceptación

- Given un trabajo en `Borrador` cuyo resultado de interpretación trae al menos un error de validación, When se envía, Then queda en `Borrador` y con sus errores adoptados y localizados.
- Given ese mismo trabajo, When se consulta después del envío fallido, Then conserva su texto original íntegro, por `RN-02008`.
- Given ese trabajo en `Borrador`, When el alumno lo reedita y lo vuelve a enviar con un texto que verifica, Then pasa a estado `Pendiente`, que es US-02015.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00003, NB-00004, NB-00005 |
| CU cubiertos | CU-02008 |
| RN e invariantes que ejerce | RN-02005, RN-02008; INV-04 |
| BT derivadas | BT-02012, BT-02013 |
| Etapa del producto | `f`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba unitaria sobre el escenario `E-5` del intake §20 como entrada con error, y verificación de que el estado no cambió. |

## 5. Prioridad y estimación

`Must` por derivar de `F-22`, `Must Have` en `PRODUCT-INTAKE` §4.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Cita por identificador toda regla e invariante que ejerce, sin volver a enunciarla
- [x] Las condiciones de rechazo que produce existen en el catálogo de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Sus tareas técnicas están identificadas y ninguna está bloqueada

## 7. Notas y supuestos

Que el trabajo quede en `Borrador` **no** es un fallo del envío: es su resultado declarado. La distinción importa porque el consumidor no debe traducirlo como error de la operación.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.3 previó con este mismo identificador y este mismo contenido. |
