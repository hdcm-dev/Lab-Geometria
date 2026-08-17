# US-02023 — Eliminar por el administrador en los tres estados que ve

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-02023-Eliminar-Por-El-Administrador-En-Los-Tres-Estados-Que-Ve.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-02006 Desenlace de la entrega
**Etapa del producto:** `h`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca de dominio (`GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`)**, quiero **que el administrador elimine cualquier trabajo que ve, en cualquiera de los tres estados de su alcance**, para **que el administrador pueda limpiar los intentos acumulados sin depender del alumno**.

## 2. Contexto

La capacidad `F-24` del intake §4 declara la eliminación de cualquier trabajo que el administrador ve, en cualquier estado y con borrado físico. `RN-02004` lo enuncia junto con el camino acotado del alumno, y `PRODUCT-INTAKE` §4.1 declara que se verifica sobre un trabajo en estado `Pendiente`.

## 3. Criterios de aceptación

- Given un trabajo en estado `Pendiente` y la cuenta de administrador, When se lo elimina, Then la eliminación procede.
- Given un trabajo en `Finalizado` o en `Rechazado`, When el administrador lo elimina, Then la eliminación procede igual: eliminar **no** es una transición de la máquina de estados y no la impide `INV-07`.
- Given un trabajo en `Borrador`, When el administrador intenta eliminarlo, Then se rechaza, porque ese trabajo no está en su alcance, por `RN-02011`.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00009 |
| CU cubiertos | CU-02011 |
| RN e invariantes que ejerce | RN-02004, RN-02011; INV-03, INV-07 |
| BT derivadas | BT-02012, BT-02014 |
| Etapa del producto | `h`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba unitaria por cada uno de los cuatro estados, incluida la exclusión del borrador. |

## 5. Prioridad y estimación

`Must` por derivar de `F-24`, `Must Have` en `PRODUCT-INTAKE` §4, y porque es criterio de la transición `h` → `i…` del roadmap §5.2.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Cita por identificador toda regla e invariante que ejerce, sin volver a enunciarla
- [x] Las condiciones de rechazo que produce existen en el catálogo de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Sus tareas técnicas están identificadas y ninguna está bloqueada

## 7. Notas y supuestos

La distinción entre **eliminar** y **transicionar** es la que hace compatibles `INV-07` y esta historia, y conviene que quede escrita: `INV-07` prohíbe salir del estado terminal y cambiar el contenido; no prohíbe que el trabajo deje de existir.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.3 previó con este mismo identificador y este mismo contenido. |
