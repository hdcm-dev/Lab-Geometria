# US-02020 — Aprobar un trabajo en estado `Pendiente`, con comentario opcional

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** US-02020-Aprobar-Un-Trabajo-En-Estado-Pendiente.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-02006 Desenlace de la entrega
**Etapa del producto:** `h`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca de dominio (`GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`)**, quiero **que el administrador apruebe un trabajo en estado `Pendiente` y lo lleve a `Finalizado`, con comentario opcional**, para **que el alumno sepa que su entrega fue aceptada y, si el administrador quiso, por qué**.

## 2. Contexto

La capacidad `F-23` del intake §4 declara aprobar y rechazar como facultad **exclusiva** del administrador, y la `F-21` declara el comentario escrito como opcional en los dos desenlaces. `RN-02010` e `INV-07` fijan la exclusividad y la terminalidad.

## 3. Criterios de aceptación

- Given un trabajo en estado `Pendiente` y la cuenta de administrador, When se aprueba, Then queda en `Finalizado`.
- Given ese mismo trabajo, When se aprueba **sin** comentario, Then la transición procede igual: el comentario es opcional, por `F-21`.
- Given un trabajo en estado `Pendiente` y una cuenta de alumno, When se intenta aprobarlo, Then se rechaza, por `RN-02010`.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00009, NB-00003 |
| CU cubiertos | CU-02010 |
| RN e invariantes que ejerce | RN-02005, RN-02010; INV-07 |
| BT derivadas | BT-02012 |
| Etapa del producto | `h`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba unitaria de la transición con y sin comentario, y del rechazo por papel. |

## 5. Prioridad y estimación

`Must` por derivar de `F-23` y `F-21`, las dos `Must Have` en `PRODUCT-INTAKE` §4, y porque la etapa `h` **cierra el alcance comprometido** (`Roadmap-Producto.md` §2.1).

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Cita por identificador toda regla e invariante que ejerce, sin volver a enunciarla
- [x] Las condiciones de rechazo que produce existen en el catálogo de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Sus tareas técnicas están identificadas y ninguna está bloqueada

## 7. Notas y supuestos

El comentario es **texto libre y no es calificación**: no hay nota ni escala (`PRODUCT-INTAKE` §4, `F-21`). El dominio lo conserva como bloque propio del trabajo y nunca como una observación más, que es la distinción que `GeometriaFactory-Contracts` sostiene en su contrato.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.3 previó con este mismo identificador y este mismo contenido. |
