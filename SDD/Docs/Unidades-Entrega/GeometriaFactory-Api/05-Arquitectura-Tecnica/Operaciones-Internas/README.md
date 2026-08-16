# Operaciones internas de la unidad de entrega

| Campo | Valor |
| --- | --- |
| Nivel | Unidad de entrega |
| Versión | 1.0 |
| Fecha | 2026-08-16 |
| Estado | Vigente |

---

## 1. Qué hay acá

Trece documentos que hasta la migración a SDD 8.x vivían en `02-Especificacion-Funcional` como casos
de uso de los proyectos de código `GeometriaFactory-Infrastructure` y `GeometriaFactory-Api`.

**No son casos de uso de esta unidad de entrega.** Un caso de uso lo ejecuta una persona contra algo
desplegado; estos describen **operaciones internas** que ninguna persona ejecuta: materializar y
recuperar del almacén, derivar una contraseña, emitir un acceso firmado, proveer el sello del reloj,
componer la aplicación al arrancar o traducir un motivo de contrato a una respuesta de protocolo.

En el modelo anterior estaban bien donde estaban: cada proyecto de código tenía su propia categoría
02 y cada capa describía sus operaciones ahí. Con la unidad de entrega como nivel, las capas son
internas y su detalle es arquitectura.

## 2. Qué conservan

Todo. Su contenido no se tocó: siguen declarando sus actores, sus precondiciones, sus flujos y sus
criterios de aceptación, y conservan su identificador, que hace legible de qué capa vienen —`CU-06…`
de infraestructura, `CU-00000…` de la capa de exposición—.

Lo que cambia es **dónde viven y qué son**: el contrato interno de una capa, no la especificación de
lo que el producto hace.

## 3. Los trece

| Documento | Qué describe |
| --- | --- |
| `CU-06001`, `CU-06002` | Interpretación del texto original y verificación de valores derivados |
| `CU-06003`, `CU-06005` | Persistencia de trabajos y de cuentas |
| `CU-06004` | Borrado físico y arrastre de la baja |
| `CU-06006`, `CU-06007`, `CU-06008` | Derivación de contraseña, contraseña provisoria y emisión del acceso firmado |
| `CU-06009`, `CU-06010` | Sello del reloj y preparación del almacén |
| `CU-00009` | Traducción del motivo del contrato a respuesta de protocolo |
| `CU-00010`, `CU-00011` | Composición de la aplicación y arranque del servicio |

`CU-00012`, que ejercitaba la superficie con una colección de peticiones reproducible, no está acá:
es un **sample** y su lugar es la categoría 10.
