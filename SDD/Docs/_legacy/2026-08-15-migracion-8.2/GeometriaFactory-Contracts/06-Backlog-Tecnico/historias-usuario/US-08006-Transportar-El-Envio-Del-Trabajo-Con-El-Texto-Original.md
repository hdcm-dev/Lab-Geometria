# US-08006 — Transportar el envío del trabajo con el texto original como cadena

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** US-08006-Transportar-El-Envio-Del-Trabajo-Con-El-Texto-Original.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-08004 Gestión del trabajo
**Etapa del producto:** `e`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **extremo que compila contra el contrato (`GeometriaFactory-Api` y `GeometriaFactory-Web`)**, quiero **el tipo con el que viaja el trabajo del alumno, con su texto original como cadena **no interpretada****, para **que el texto que el alumno escribió llegue íntegro al backend y nunca se reescriba en el camino**.

## 2. Contexto

La capacidad `F-06` del intake §4 declara la carga del trabajo con nombre, fecha, descripción y el texto de figuras. `PRODUCT-INTAKE` §17.4.P.11 punto 2 declara que el texto viaja como cadena **sin interpretarse en el contrato**: la interpretación es del backend y el dibujo, del bundle del visor. Es la restricción transversal `RT-03` de `02` §6.

## 3. Criterios de aceptación

- Given un trabajo con su texto original, When se arma la solicitud de envío, Then el tipo transporta nombre, fecha, descripción y el texto como **cadena**, sin ninguna estructura interpretada.
- Given un texto original con las particularidades de formato del emisor real, When viaja en los dos sentidos, Then llega **idéntico**, por `RN-08008`.
- Given la superficie de esta familia, When se busca algún campo que obligue a interpretar el texto antes de enviarlo, Then no existe ninguno: interpretar es del backend.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00003, NB-00004 |
| CU cubiertos | CU-08003 |
| Familia de tipos de `05` §3.1 | Familia de trabajo |
| Restricciones transversales de `02` §6 | RT-03, RT-08 |
| RN que refiere por identificador | RN-08003, RN-08004, RN-08005, RN-08008 |
| BT derivadas | BT-08012 |
| Etapa del producto | `e`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba de integración con los escenarios `E-1` a `E-8` del intake §20 como cuerpo; **no se inventan textos de prueba** (`PRODUCT-INTAKE` §15, regla de delivery 5). |

## 5. Prioridad y estimación

`Must` por derivar de `F-06` y de `F-22`, las dos `Must Have` en `PRODUCT-INTAKE` §4.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un contrato de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Declara la familia de tipos de `05` §3.1 que la sostiene
- [x] Ninguna regla de negocio se redacta acá: las refiere por identificador a `GeometriaFactory-Domain`
- [x] Se refinó contra la regla de exposición de `05` §3.2 y ningún campo que introduce puede transportar una dirección de servicio, una ruta de datos ni un secreto

## 7. Notas y supuestos

**Enviar es la única acción de guardado** (`F-22`): no hay una operación separada de guardar y otra de enviar, y por eso el contrato no declara dos tipos. El estado resultante es una salida del mismo envío y viaja en la respuesta, que es US-08019.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 previó con este mismo identificador y esta misma pertenencia a necesidades de negocio. |
