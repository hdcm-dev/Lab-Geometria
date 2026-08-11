# US-13 — Transportar la observación con su severidad y su par de valores

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** US-13-Transportar-La-Observacion-Con-Severidad-Y-Par-De-Valores.md
**Versión:** 1.0
**Estado:** Propuesta
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-05 Interpretación y verificación del dato del alumno
**Etapa del producto:** `f`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **extremo que compila contra el contrato (`GeometriaFactory-Api` y `GeometriaFactory-Web`)**, quiero **que cada observación viaje con su severidad y, cuando corresponde, con el valor declarado y el derivado en campos propios**, para **que el alumno vea en qué se equivocó su cálculo, con los dos números a la vista y sin que el sistema le corrija el suyo**.

## 2. Contexto

`NB-05` es la visibilidad del error de cálculo. La capacidad `F-10` del intake §4 declara la verificación de los valores recalculándolos y las advertencias **que no bloquean**. `02` §4.1 declara que este proyecto de código transporta la severidad y el par de valores en campos propios.

## 3. Criterios de aceptación

- Given una advertencia de valor, When viaja en el detalle, Then transporta su severidad y el **par** de valor declarado y valor derivado en campos separados.
- Given un error de validación, When viaja en el detalle, Then transporta su severidad y su ubicación, y **no** un par de valores: no todos los tipos de observación llevan los dos campos.
- Given un trabajo con advertencias y sin errores, When se consulta su estado en el mismo detalle, Then es `Pendiente`: la advertencia no bloquea, por `RN-05`.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-04, NB-05 |
| CU cubiertos | CU-05 |
| Familia de tipos de `05` §3.1 | Familia de detalle |
| Restricciones transversales de `02` §6 | RT-02 |
| RN que refiere por identificador | RN-05, RN-08, RN-09 |
| BT derivadas | BT-14 |
| Etapa del producto | `f`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba de integración con los escenarios `E-3` y `E-4` del intake §20, que son los dos cubos con y sin advertencia de área. |

## 5. Prioridad y estimación

`Must` por derivar de `F-10`, `Must Have` en `PRODUCT-INTAKE` §4, y porque el roadmap §5.2 lo verifica en la transición `f` → `g` con los dos valores expresados.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un contrato de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Declara la familia de tipos de `05` §3.1 que la sostiene
- [x] Ninguna regla de negocio se redacta acá: las refiere por identificador a `GeometriaFactory-Domain`
- [x] Se refinó contra la regla de exposición de `05` §3.2 y ningún campo que introduce puede transportar una dirección de servicio, una ruta de datos ni un secreto

## 7. Notas y supuestos

**El recálculo y la tolerancia de comparación son del dominio y de su implementación** (`02` §4.1). Este contrato transporta el resultado; no compara nada.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 previó con este mismo identificador y esta misma pertenencia a necesidades de negocio. |
