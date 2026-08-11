# US-09 — Transportar el alcance del listado según el papel, con los datos para agrupar y filtrar

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** US-09-Transportar-El-Alcance-Del-Listado-Segun-El-Papel.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-04 Gestión del trabajo
**Etapa del producto:** `e`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **extremo que compila contra el contrato (`GeometriaFactory-Api` y `GeometriaFactory-Web`)**, quiero **que la misma proyección sirva a los dos papeles con alcance distinto, y que traiga los datos con los que el administrador agrupa y filtra por alumno**, para **que el administrador vea la comisión organizada sin que el contrato duplique una superficie por cada papel**.

## 2. Contexto

`02` §3.1 declara que el listado propio del alumno y el de la comisión se fusionaron en un solo contrato de uso porque son el mismo tipo con distinto alcance de datos. La capacidad `F-12` del intake §4 declara la agrupación y el filtro por alumno, y `RN-11` que el administrador **no ve los trabajos en `Borrador`**.

## 3. Criterios de aceptación

- Given una consulta de un alumno, When se arma la proyección, Then su alcance son sus propios trabajos, por `RN-03`.
- Given una consulta del administrador, When se arma la proyección, Then su alcance son los trabajos de la comisión **menos los que están en `Borrador`**, por `RN-11`.
- Given la proyección del administrador, When se la usa para agrupar y filtrar, Then trae el dueño de cada trabajo y su estado, que es lo que la agrupación y el filtro necesitan.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-03, NB-07 |
| CU cubiertos | CU-04 |
| Familia de tipos de `05` §3.1 | Familia de listado |
| Restricciones transversales de `02` §6 | RT-04 |
| RN que refiere por identificador | RN-03, RN-11 |
| BT derivadas | BT-13 |
| Etapa del producto | `e`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba de integración del listado por cada papel, con un alumno que tenga un borrador y un trabajo en estado `Pendiente`. |

## 5. Prioridad y estimación

`Must` por derivar de `F-08` y de `F-12`, las dos `Must Have` en `PRODUCT-INTAKE` §4, y porque el roadmap §5.2 lo verifica en la transición `e` → `f`.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un contrato de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Declara la familia de tipos de `05` §3.1 que la sostiene
- [x] Ninguna regla de negocio se redacta acá: las refiere por identificador a `GeometriaFactory-Domain`
- [x] Se refinó contra la regla de exposición de `05` §3.2 y ningún campo que introduce puede transportar una dirección de servicio, una ruta de datos ni un secreto

## 7. Notas y supuestos

**El alcance de lo que cada papel ve lo decide el dominio**, no este contrato (`02` §4.1). Lo que este proyecto de código declara es que la proyección tiene alcance variable y que trae los datos de agrupación; la agrupación en pantalla es de `GeometriaFactory-Web`.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 previó con este mismo identificador y esta misma pertenencia a necesidades de negocio. |
