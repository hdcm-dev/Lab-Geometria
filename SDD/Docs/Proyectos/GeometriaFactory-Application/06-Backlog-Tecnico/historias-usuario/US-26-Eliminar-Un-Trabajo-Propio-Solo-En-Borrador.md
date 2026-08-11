# US-26 — Eliminar un trabajo propio sólo en `Borrador`

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** US-26-Eliminar-Un-Trabajo-Propio-Solo-En-Borrador.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-04 Gestión del trabajo
**Etapa del producto:** `e`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que el alumno pueda retirar un trabajo propio únicamente mientras está en `Borrador`**, para **que pueda descartar lo que todavía no entregó y no pueda borrar lo que ya entregó**.

## 2. Contexto

`RN-04` acota la eliminación del alumno al estado `Borrador` y `F-07` del intake §4 lo declara `Must Have`. El contrato de uso es [`CU-09`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-09-Eliminar-Un-Trabajo.md), que `02` §8 mantuvo **en un solo caso de uso con sus dos alcances**, porque los dos responden la misma pregunta y el actor primario del contrato es uno solo.

## 3. Criterios de aceptación

- Given un trabajo propio en `Borrador`, When el alumno pide eliminarlo, Then el trabajo queda retirado.
- Given un trabajo propio en cualquier otro estado, When el alumno pide eliminarlo, Then se rechaza con su motivo y **el trabajo sigue existiendo**.
- Given un trabajo de otro alumno en `Borrador`, When el solicitante pide eliminarlo, Then se devuelve el motivo de **inexistencia para el solicitante**, el mismo que produce un identificador inexistente.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-03 |
| CU cubiertos | CU-09 |
| RN e invariantes que ejerce | RN-03, RN-04; INV-02, INV-03 |
| Componente de `05` §3.1 | Orquestación del trabajo, Guarda de autorización |
| Puertos que consume | Repositorio de trabajos |
| Comprobación de `02` §4 que la alcanza | **Pertenencia**, y cambio de contraseña pendiente antes que ella |
| BT derivadas | BT-09, BT-10, BT-11, BT-15 |
| Tests previstos en 08 | Prueba con dobles de los dos rechazos, y su contraparte forzada contra la superficie en la batería de `GeometriaFactory-Api` |

## 5. Prioridad y estimación

`Must` por derivar de `F-07`, `Must Have`, y porque el criterio de transición `e` → `f` exige verificar la acotación **forzando la petición al servicio de datos, no sólo por la interfaz**.

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

**La verificación forzando la petición no ocurre en esta capa sino contra la superficie de `GeometriaFactory-Api`**, que la declara como el único criterio del producto que la fuente exige ejercer así. Lo que esta capa aporta es que la comprobación esté hecha **sobre el dato recuperado y antes de escribir**, de modo que no dependa de que la pantalla oculte un control.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador. |
