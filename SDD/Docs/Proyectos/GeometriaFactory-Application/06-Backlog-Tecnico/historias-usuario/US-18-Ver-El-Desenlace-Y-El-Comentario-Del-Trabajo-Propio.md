# US-18 — Ver el desenlace y el comentario del trabajo propio

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** US-18-Ver-El-Desenlace-Y-El-Comentario-Del-Trabajo-Propio.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-06 Desenlace de la entrega
**Etapa del producto:** `h`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que el detalle de un trabajo propio traiga su desenlace y el comentario del administrador cuando lo hay**, para **que el alumno sepa si su entrega fue aceptada y por qué, sin tener que preguntar**.

## 2. Contexto

`NB-09` pide desenlace explícito de la entrega. El contrato de uso es [`CU-06`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-06-Consultar-Los-Trabajos-Propios-Del-Alumno.md). El roadmap §5.2 precisa, en la transición `h` → `i…`, que el alumno ve **el desenlace en su propio listado** y **el comentario al abrir el trabajo** desde ese listado; el comentario no viaja en la proyección de listado, por [`Contracts ADR-05`](../../../GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Adrs/ADR-05-Proyeccion-De-Listado-Separada-Del-Detalle.md).

## 3. Criterios de aceptación

- Given un trabajo propio en estado `Finalizado` con comentario, When se pide su detalle, Then vienen el estado terminal y el comentario.
- Given un trabajo propio en estado `Rechazado` **sin** comentario, When se pide su detalle, Then viene el estado terminal y el comentario **ausente**, que es un caso válido: el comentario es opcional en los dos desenlaces.
- Given el listado propio, When se lo resuelve, Then trae el **estado** de cada trabajo y **no trae el comentario**: para eso está el detalle.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-09, NB-03 |
| CU cubiertos | CU-06 |
| RN e invariantes que ejerce | RN-03, RN-10; INV-07 |
| Componente de `05` §3.1 | Orquestación de la consulta, Guarda de autorización |
| Puertos que consume | Repositorio de trabajos |
| Comprobación de `02` §4 que la alcanza | Pertenencia, y cambio de contraseña pendiente antes que ella |
| BT derivadas | BT-16 |
| Tests previstos en 08 | Prueba de detalle con y sin comentario, y prueba de que el listado no lo trae |

## 5. Prioridad y estimación

`Must` por derivar de `F-21` y `F-23`, `Must Have`, y porque es criterio de la transición `h` → `i…`.

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

**El comentario no es una observación.** Las observaciones las emite el validador sobre la geometría y son varias por trabajo; el comentario del administrador es **uno solo, opcional y sin historial**, porque los dos estados de cierre son terminales (`INV-07`).

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador. |
