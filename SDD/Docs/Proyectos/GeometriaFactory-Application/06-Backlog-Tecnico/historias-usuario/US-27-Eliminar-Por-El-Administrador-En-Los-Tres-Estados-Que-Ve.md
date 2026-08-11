# US-27 — Eliminar por el administrador en los tres estados que ve

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** US-27-Eliminar-Por-El-Administrador-En-Los-Tres-Estados-Que-Ve.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-06 Desenlace de la entrega
**Etapa del producto:** `h`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que el administrador pueda retirar cualquier trabajo que ve, en los tres estados que su alcance incluye**, para **que pueda limpiar la entrega de la comisión sin depender del alumno**.

## 2. Contexto

`RN-04` declara que el administrador elimina cualquier trabajo que ve, en cualquier estado, con borrado físico, y `F-24` del intake §4 lo declara `Must Have`. El contrato de uso es [`CU-09`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-09-Eliminar-Un-Trabajo.md), en su segundo alcance.

## 3. Criterios de aceptación

- Given un solicitante con papel `Administrador` y un trabajo en `Pendiente`, `Finalizado` o `Rechazado`, When pide eliminarlo, Then el trabajo queda retirado, con borrado **físico**.
- Given un trabajo en `Borrador`, When el administrador pide eliminarlo, Then se devuelve el motivo de **fuera del alcance del administrador**: los borradores no forman parte de su flujo de trabajo.
- Given un solicitante sin el papel `Administrador`, When pide eliminar un trabajo ajeno en `Pendiente`, Then recibe el motivo de **inexistencia para el solicitante** y no el de facultad: la pertenencia se comprueba antes y oculta la existencia del recurso.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-09, NB-03 |
| CU cubiertos | CU-09 |
| RN e invariantes que ejerce | RN-04, RN-11; INV-03 |
| Componente de `05` §3.1 | Orquestación del trabajo, Guarda de autorización |
| Puertos que consume | Repositorio de trabajos |
| Comprobación de `02` §4 que la alcanza | **Alcance del administrador** y **facultad**, y cambio de contraseña pendiente antes que las dos |
| BT derivadas | BT-10, BT-15 |
| Tests previstos en 08 | Prueba de borrado sobre un trabajo en `Pendiente`, que es el caso que la fuente exige verificar |

## 5. Prioridad y estimación

`Must` por derivar de `F-24`, `Must Have`, y porque el criterio de transición `h` → `i…` exige que el administrador elimine un trabajo en estado `Pendiente` y el trabajo desaparezca.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el componente de `05` §3.1 y los puertos que consume
- [x] Declara qué comprobación de `02` §4 la alcanza
- [x] Las condiciones de rechazo que produce existen en el catálogo de las 36 de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Se puede verificar con dobles de los cuatro puertos, sin base de datos

## 7. Notas y supuestos

**Los dos alcances de la eliminación son opuestos y viven en el mismo contrato de uso**: el alumno sólo en `Borrador`, el administrador en todo lo que ve, que es exactamente lo que el alumno no puede tocar. Es la partición que `RN-04` declara en una sola regla.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador. |
