> **Artefacto archivado — estado `Superado`**
>
> Copia archivada de `US-04018-Ver-El-Desenlace-Y-El-Comentario-Del-Trabajo-Propio.md` en su versión **1.0**, tomada el 2026-08-25 por el orquestador de migración normativa **antes** de aplicar el corte de la categoría 06 de la fase M4 (`Master-Prompt.md` §5 y §8).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-25
> - **Versión vigente:** [`US-04018-Ver-El-Desenlace-Y-El-Comentario-Del-Trabajo-Propio.md`](../../US-04018-Ver-El-Desenlace-Y-El-Comentario-Del-Trabajo-Propio.md)
>
> El cuerpo que sigue **no se modifica**. Lo único que se tocó son **los enlaces relativos**, reescritos dos niveles para que sigan resolviendo desde esta ubicación.

---

# US-04018 — Ver el desenlace y el comentario del trabajo propio

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-04018-Ver-El-Desenlace-Y-El-Comentario-Del-Trabajo-Propio.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-04006 Desenlace de la entrega
**Etapa del producto:** `h`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que el detalle de un trabajo propio traiga su desenlace y el comentario del administrador cuando lo hay**, para **que el alumno sepa si su entrega fue aceptada y por qué, sin tener que preguntar**.

## 2. Contexto

`NB-00009` pide desenlace explícito de la entrega. El contrato de uso es [`CU-00028`](../../../../02-Especificacion-Funcional/Casos-De-Uso/CU-00028-Consultar-El-Listado-Y-El-Detalle-De-Los-Trabajos.md). El roadmap §5.2 precisa, en la transición `h` → `i…`, que el alumno ve **el desenlace en su propio listado** y **el comentario al abrir el trabajo** desde ese listado; el comentario no viaja en la proyección de listado, por [`Contracts ADR-08005`](../../../../../../Producto/Adrs/ADR-08005-Proyeccion-De-Listado-Separada-Del-Detalle.md).

## 3. Criterios de aceptación

- Given un trabajo propio en estado `Finalizado` con comentario, When se pide su detalle, Then vienen el estado terminal y el comentario.
- Given un trabajo propio en estado `Rechazado` **sin** comentario, When se pide su detalle, Then viene el estado terminal y el comentario **ausente**, que es un caso válido: el comentario es opcional en los dos desenlaces.
- Given el listado propio, When se lo resuelve, Then trae el **estado** de cada trabajo y **no trae el comentario**: para eso está el detalle.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00009, NB-00003 |
| CU cubiertos | CU-04006 |
| RN e invariantes que ejerce | RN-04003, RN-04010; INV-07 |
| Componente de `05` §3.1 | Orquestación de la consulta, Guarda de autorización |
| Puertos que consume | Repositorio de trabajos |
| Comprobación de `02` §4 que la alcanza | Pertenencia, y cambio de contraseña pendiente antes que ella |
| BT derivadas | BT-04016 |
| Tests previstos en 08 | Prueba de detalle con y sin comentario, y prueba de que el listado no lo trae |

## 5. Prioridad y estimación

`Must` por derivar de `F-21` y `F-23`, `Must Have`, y porque es criterio de la transición `h` → `i…`.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el componente de `05` §3.1 y los puertos que consume
- [x] Declara qué comprobación de `02` §4 la alcanza
- [x] Las condiciones de rechazo que produce existen en el catálogo de las 36 de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Se puede verificar con dobles de los cuatro puertos, sin base de datos

## 7. Notas y supuestos

**El comentario no es una observación.** Las observaciones las emite el validador sobre la geometría y son varias por trabajo; el comentario del administrador es **uno solo, opcional y sin historial**, porque los dos estados de cierre son terminales (`INV-07`).

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador. |
