# US-17 — Rechazar toda transición desde un estado terminal

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** US-17-Rechazar-Toda-Transicion-Desde-Un-Estado-Terminal.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-05 Interpretación y verificación del dato del alumno
**Etapa del producto:** `f`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca de dominio (`GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`)**, quiero **que ninguna transición salga de `Finalizado` ni de `Rechazado`, y que su contenido no cambie**, para **que un trabajo cerrado quede como registro de lo que efectivamente se entregó**.

## 2. Contexto

`RN-10` declara que `Finalizado` y `Rechazado` son terminales, e `INV-07` declara que un trabajo en cualquiera de los dos no cambia de estado ni de contenido. `PRODUCT-INTAKE` §4.2 registra como consecuencia aceptada que corregir un rechazo significa **cargar un trabajo nuevo**.

## 3. Criterios de aceptación

- Given un trabajo en `Finalizado`, When se intenta cualquier transición sobre él, Then se rechaza y ni su estado ni su contenido cambian.
- Given un trabajo en `Rechazado`, When se intenta reeditarlo o volver a enviarlo, Then se rechaza por la misma vía.
- Given un trabajo en cualquiera de los dos terminales, When el administrador lo elimina, Then la eliminación **sí** procede: eliminar no es una transición de la máquina de estados, y es US-23.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-03, NB-09 |
| CU cubiertos | CU-08 |
| RN e invariantes que ejerce | RN-04, RN-10; INV-07 |
| BT derivadas | BT-12, BT-14 |
| Etapa del producto | `f`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba unitaria por cada transición intentada desde cada uno de los dos terminales, dentro de la matriz de ejercicio de los nueve invariantes. |

## 5. Prioridad y estimación

`Must` por `RN-10`, que `PRODUCT-INTAKE` §4.1 declara como regla del circuito de revisión, y porque la terminalidad es criterio de la transición `h` → `i…` del roadmap §5.2.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Cita por identificador toda regla e invariante que ejerce, sin volver a enunciarla
- [x] Las condiciones de rechazo que produce existen en el catálogo de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Sus tareas técnicas están identificadas y ninguna está bloqueada

## 7. Notas y supuestos

La historia vive en la etapa `f` y no en la `h` porque la máquina de estados del trabajo se construye entera al resolverse el envío; lo que la etapa `h` agrega son las dos transiciones de desenlace, US-20 y US-21, no la terminalidad.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.3 previó con este mismo identificador y este mismo contenido. |
