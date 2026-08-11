# US-14 — Transportar el error neutro con el conjunto cerrado de quince códigos

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** US-14-Transportar-El-Error-Neutro-Con-El-Conjunto-Cerrado-De-Codigos.md
**Versión:** 1.0
**Estado:** Propuesta
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-02 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **extremo que compila contra el contrato (`GeometriaFactory-Api` y `GeometriaFactory-Web`)**, quiero **un **único** tipo de error, con cuatro campos y un conjunto cerrado de **quince** códigos vivos**, para **que exista un solo lugar en el que un fallo pueda cruzar la frontera, y por lo tanto un solo lugar donde vigilarlo**.

## 2. Contexto

[`ADR-02`](../../05-Arquitectura-Tecnica/Adrs/ADR-02-Tipo-De-Error-Unico-Con-Conjunto-Cerrado.md) decide el tipo único con conjunto cerrado, y `05` §2.1 declara por qué se descartó un tipo de error por familia: multiplicaría por ocho los lugares donde se puede filtrar una dirección de servicio. `05` §7 fija los cuatro campos —código, texto neutro, colección de detalles de ubicación y momento— y los **quince** códigos vivos sobre **dieciocho** identificadores emitidos.

## 3. Criterios de aceptación

- Given cualquier fallo que cruce la frontera, When se arma la respuesta, Then usa **el mismo** tipo de error, con sus cuatro campos.
- Given la inspección del conjunto de códigos, When se cuentan los vivos, Then son exactamente **quince**, y **cero** códigos se producen fuera del conjunto.
- Given un identificador retirado —de los **tres** que hay—, When se busca reutilizarlo para otra condición, Then la regla de no reciclado lo impide: un consumidor viejo lo interpretaría con la causa anterior.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-02, NB-04, NB-08 |
| CU cubiertos | CU-06 |
| Familia de tipos de `05` §3.1 | Familia de error |
| Restricciones transversales de `02` §6 | RT-02, RT-10 |
| RN que refiere por identificador | RN-03, RN-09, RN-10, RN-11, RN-13, RN-15 |
| BT derivadas | BT-07, BT-08 |
| Etapa del producto | `c`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba de inspección del conjunto cerrado, que `02` declara como criterio de aceptación de `CU-06`, más la de superficie pública sobre los campos prohibidos. |

## 5. Prioridad y estimación

`Must` porque es transversal a los otros siete contratos de uso: sin el tipo de error, ninguno de ellos tiene camino de rechazo declarado.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un contrato de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Declara la familia de tipos de `05` §3.1 que la sostiene
- [x] Ninguna regla de negocio se redacta acá: las refiere por identificador a `GeometriaFactory-Domain`
- [x] Se refinó contra la regla de exposición de `05` §3.2 y ningún campo que introduce puede transportar una dirección de servicio, una ruta de datos ni un secreto

## 7. Notas y supuestos

**El texto del error es neutro y nunca lleva la dirección del servicio que falló** (`RA-03`, `PRODUCT-INTAKE` §17.4.P.5). `05` §9 declara que la forma habitual en que ese defecto entra es **agregando un campo de diagnóstico**, y que entra sin que nadie lo note porque compila.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 previó con este mismo identificador y esta misma pertenencia a necesidades de negocio. |
