# US-15 — Transportar el detalle de ubicación con índice de figura y campo

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** US-15-Transportar-El-Detalle-De-Ubicacion-Con-Indice-Y-Campo.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-05 Interpretación y verificación del dato del alumno
**Etapa del producto:** `f`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **extremo que compila contra el contrato (`GeometriaFactory-Api` y `GeometriaFactory-Web`)**, quiero **que la respuesta de error y la observación transporten, cuando corresponde, el índice de la figura y el campo señalado**, para **que el alumno sepa dónde está el problema en lugar de recibir un texto genérico**.

## 2. Contexto

`RN-09` declara que los mensajes de error de validación indican **índice de figura y campo**, nunca un texto genérico, y `PRODUCT-INTAKE` §17.4.P.5 la ancla explícitamente al tipo de respuesta de error de este proyecto de código. Es la restricción transversal `RT-02` de `02` §6.

## 3. Criterios de aceptación

- Given un error de validación con su ubicación, When viaja, Then transporta el índice de la figura y el campo señalado en campos propios.
- Given un error que no tiene ubicación —porque su causa no es una figura—, When viaja, Then la colección de detalles de ubicación va vacía y el error sigue siendo válido.
- Given cualquiera de los dos casos, When se inspecciona el texto del error, Then es **neutro**: no lleva rutas de archivos de datos ni trazas de la implementación.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-04 |
| CU cubiertos | CU-06, CU-05 |
| Familia de tipos de `05` §3.1 | Familia de error, Familia de detalle |
| Restricciones transversales de `02` §6 | RT-02 |
| RN que refiere por identificador | RN-09 |
| BT derivadas | BT-07, BT-14 |
| Etapa del producto | `f`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba de integración con el escenario `E-5` del intake §20, que es el tipo desconocido con índice de figura y campo. |

## 5. Prioridad y estimación

`Must` por `RN-09`, declarada cerrada en `PRODUCT-INTAKE` §4.1, y por derivar de `F-09`, `Must Have`.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un contrato de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Declara la familia de tipos de `05` §3.1 que la sostiene
- [x] Ninguna regla de negocio se redacta acá: las refiere por identificador a `GeometriaFactory-Domain`
- [x] Se refinó contra la regla de exposición de `05` §3.2 y ningún campo que introduce puede transportar una dirección de servicio, una ruta de datos ni un secreto

## 7. Notas y supuestos

La ubicación viaja como **colección**, no como dos campos sueltos: un mismo error puede señalar más de una figura. Es la forma que `05` §7 declara para el tipo de error.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 previó con este mismo identificador y esta misma pertenencia a necesidades de negocio. |
