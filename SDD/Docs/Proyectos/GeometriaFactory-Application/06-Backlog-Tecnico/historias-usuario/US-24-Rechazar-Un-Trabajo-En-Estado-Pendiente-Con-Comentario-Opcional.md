# US-24 — Rechazar un trabajo en estado `Pendiente`, con comentario opcional

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** US-24-Rechazar-Un-Trabajo-En-Estado-Pendiente-Con-Comentario-Opcional.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-06 Desenlace de la entrega
**Etapa del producto:** `h`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **orquestar el rechazo de un trabajo en estado `Pendiente`, con un comentario opcional**, para **que el alumno sepa que su entrega no fue aceptada y, cuando el docente lo escriba, por qué**.

## 2. Contexto

`NB-09` pide desenlace explícito y `F-23` del intake §4 lo declara `Must Have`. El contrato de uso es [`CU-08`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-08-Dar-Desenlace-A-Un-Trabajo.md). El intake §4.2 declara como consecuencia aceptada que `Rechazado` es **terminal** y que corregir un rechazo significa **cargar un trabajo nuevo**.

## 3. Criterios de aceptación

- Given un trabajo en estado `Pendiente` y un solicitante con papel `Administrador`, When se lo rechaza, Then queda en `Rechazado` y el estado es **terminal**.
- Given ese rechazo **sin** comentario, When se resuelve, Then procede igual, y el alumno recibe el estado sin explicación escrita: es la tercera consecuencia que el Product Owner aceptó al decidir el modelo de estados.
- Given un trabajo en `Rechazado`, When el alumno pide reeditarlo o eliminarlo, Then se rechaza: ninguna transición sale de un estado terminal.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-09 |
| CU cubiertos | CU-08 |
| RN e invariantes que ejerce | RN-04, RN-10; INV-07 |
| Componente de `05` §3.1 | Orquestación del desenlace, Guarda de autorización |
| Puertos que consume | Repositorio de trabajos, reloj del sistema |
| Comprobación de `02` §4 que la alcanza | Facultad y alcance del administrador, y cambio de contraseña pendiente antes que las dos |
| BT derivadas | BT-10, BT-17 |
| Tests previstos en 08 | Prueba de rechazo con y sin comentario, y de reedición rechazada desde `Rechazado` |

## 5. Prioridad y estimación

`Must` por derivar de `F-23` y `F-21`, `Must Have`, y porque el criterio de transición `h` → `i…` lo exige junto con la aprobación.

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

**Aprobar y rechazar no se fusionaron en una sola historia** aunque compartan pantalla, precondición y regla, porque el resultado observable es distinto y cada uno tiene su criterio de transición propio en el roadmap. Lo que sí comparten es contrato de uso, y por eso los dos citan CU-08.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador. |
