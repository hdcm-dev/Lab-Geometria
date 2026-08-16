# US-08010 — Transportar el resumen por alumno y por estado del panel del administrador

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** US-08010-Transportar-El-Resumen-Por-Alumno-Y-Por-Estado.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-08008 Capacidades de prioridad menor
**Etapa del producto:** `i…`
**Prioridad MoSCoW:** Could
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **extremo que compila contra el contrato (`GeometriaFactory-Api` y `GeometriaFactory-Web`)**, quiero **el tipo con el que viaja el resumen de cantidad de trabajos por alumno y por estado**, para **que el administrador vea de un vistazo cómo va la comisión, sin recorrer el listado entero**.

## 2. Contexto

La capacidad `F-15` del intake §4 es **`Could Have`** y cae en la fase `i…`. `02` §4.2 la ubica ahí explícitamente al declarar que la previsión de producto correspondiente queda fuera del tramo comprometido **con su prioridad menor de etapa `i`**.

## 3. Criterios de aceptación

- Given el conjunto de trabajos de la comisión, When se arma el resumen, Then transporta la cantidad por alumno y por estado, y ningún dato de trabajo individual.
- Given ese resumen, When se lo compara con la proyección de listado, Then son tipos distintos: el resumen agrega y el listado enumera.
- Given el estado `Borrador`, When se arma el resumen para el administrador, Then esos trabajos no se cuentan, por `RN-08011`, con el mismo alcance que el listado.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00007 |
| CU cubiertos | CU-08004 |
| Familia de tipos de `05` §3.1 | Familia de listado |
| Restricciones transversales de `02` §6 | RT-04 |
| RN que refiere por identificador | RN-08011 |
| BT derivadas | — |
| Etapa del producto | `i…`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | A definir al planificar la fase `i…`, que el roadmap §2.1 declara que se planifica con la plantilla completa cuando `h` esté cerrada y demostrada. |

## 5. Prioridad y estimación

`Could` porque su capacidad de origen, `F-15`, es `Could Have` en `PRODUCT-INTAKE` §4. **Está fuera del tramo comprometido de ocho etapas** y se declara acá para que la previsión de 02 quede completa, no para comprometerla.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un contrato de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Declara la familia de tipos de `05` §3.1 que la sostiene
- [x] Ninguna regla de negocio se redacta acá: las refiere por identificador a `GeometriaFactory-Domain`
- [ ] Se refinó contra la regla de exposición de `05` §3.2 y ningún campo que introduce puede transportar una dirección de servicio, una ruta de datos ni un secreto

## 7. Notas y supuestos

**Esta historia no entra en el objetivo de 8 de 8 etapas.** Si la fase `i…` no se planifica, la historia no se construye y eso no afecta el cierre del alcance comprometido, que la etapa `h` cierra (`Roadmap-Producto.md` §2.1).

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 previó con este mismo identificador y esta misma pertenencia a necesidades de negocio. |
