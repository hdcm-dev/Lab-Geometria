# US-17 — Transportar el desenlace con su conjunto cerrado de dos valores

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** US-17-Transportar-El-Desenlace-Con-Su-Conjunto-Cerrado-De-Dos-Valores.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-07 Desenlace de la entrega
**Etapa del producto:** `h`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **extremo que compila contra el contrato (`GeometriaFactory-Api` y `GeometriaFactory-Web`)**, quiero **el tipo con el que viaja la aprobación o el rechazo de un trabajo, con el desenlace como conjunto cerrado de dos valores**, para **que el administrador cierre la entrega y que el estado terminal alcanzado viaje de vuelta sin ambigüedad**.

## 2. Contexto

La capacidad `F-23` del intake §4 declara aprobar y rechazar como facultad **exclusiva** del administrador. `02` §3.1 declara que aprobar y rechazar se fusionaron en un solo contrato de uso porque comparten tipo de solicitud, resultado, precondición, errores y regla de dominio, y se distinguen sólo por el valor de un campo de conjunto cerrado.

## 3. Criterios de aceptación

- Given un desenlace pretendido sobre un trabajo en estado `Pendiente`, When se arma la solicitud, Then transporta la identidad del trabajo y el desenlace, tomado de un conjunto cerrado de **dos** valores.
- Given un desenlace que procede, When se arma el resultado, Then transporta el estado terminal alcanzado, de los **cuatro** estados del conjunto cerrado del trabajo.
- Given un desenlace intentado por un alumno o sobre un trabajo que no está en estado `Pendiente`, When el servicio responde, Then el rechazo viaja con uno de los **dos** códigos propios de esta familia.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-07, NB-09 |
| CU cubiertos | CU-07 |
| Familia de tipos de `05` §3.1 | Familia de desenlace |
| Restricciones transversales de `02` §6 | RT-08 |
| RN que refiere por identificador | RN-10, RN-11 |
| BT derivadas | BT-15 |
| Etapa del producto | `h`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba de integración de los dos desenlaces y de los dos rechazos propios. |

## 5. Prioridad y estimación

`Must` por derivar de `F-23`, `Must Have` en `PRODUCT-INTAKE` §4, y porque la etapa `h` **cierra el alcance comprometido** (`Roadmap-Producto.md` §2.1).

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un contrato de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Declara la familia de tipos de `05` §3.1 que la sostiene
- [x] Ninguna regla de negocio se redacta acá: las refiere por identificador a `GeometriaFactory-Domain`
- [x] Se refinó contra la regla de exposición de `05` §3.2 y ningún campo que introduce puede transportar una dirección de servicio, una ruta de datos ni un secreto

## 7. Notas y supuestos

**La transición y su exclusividad son invariantes de `GeometriaFactory-Domain`** (`02` §4.1). Lo que este contrato garantiza es que **ningún tipo permite salir de un estado terminal** (`RT-08`): la superficie no ofrece el camino, aunque la guarda viva en el dominio.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 previó con este mismo identificador y esta misma pertenencia a necesidades de negocio. |
