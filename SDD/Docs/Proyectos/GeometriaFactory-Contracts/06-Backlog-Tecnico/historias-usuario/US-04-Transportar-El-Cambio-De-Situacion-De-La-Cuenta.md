# US-04 — Transportar el cambio de situación de la cuenta

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** US-04-Transportar-El-Cambio-De-Situacion-De-La-Cuenta.md
**Versión:** 1.0
**Estado:** Propuesta
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-03 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **extremo que compila contra el contrato (`GeometriaFactory-Api` y `GeometriaFactory-Web`)**, quiero **el tipo con el que viaja la orden de habilitar, bloquear o rehabilitar una cuenta, y su resultado**, para **que el administrador gobierne quién entra al laboratorio y que la respuesta le diga qué situación quedó y qué provisoria comunicar**.

## 2. Contexto

La capacidad `F-03` del intake §4 declara las cuatro operaciones del administrador sobre una cuenta; ésta transporta las tres no destructivas. `RN-16` declara que habilitar produce la provisoria y que la pantalla se la muestra al administrador para que se la comunique.

## 3. Criterios de aceptación

- Given una orden de cambio de situación sobre una cuenta de alumno, When se arma la solicitud, Then el tipo transporta la identidad de la cuenta y la situación pretendida, tomada de un conjunto cerrado.
- Given una habilitación que procede, When se arma el resultado, Then transporta la **provisoria producida** para que el administrador se la comunique, y la marca de cambio pendiente de la cuenta.
- Given una orden sobre la cuenta de administrador, When el servicio responde, Then el rechazo viaja con su código propio del conjunto cerrado, y no con un texto libre.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-01, NB-02 |
| CU cubiertos | CU-02 |
| Familia de tipos de `05` §3.1 | Familia de cuentas |
| Restricciones transversales de `02` §6 | RT-01 |
| RN que refiere por identificador | RN-01, RN-06, RN-14, RN-16 |
| BT derivadas | BT-08, BT-10 |
| Etapa del producto | `d`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba de integración por cada transición admitida y por la rechazada sobre la cuenta de administrador. |

## 5. Prioridad y estimación

`Must` por derivar de `F-03` y de `F-04`, las dos `Must Have` en `PRODUCT-INTAKE` §4, y porque el roadmap §5.2 verifica en la transición `d` → `e` que al habilitar el producto muestra una provisoria que el administrador no escribió.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un contrato de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Declara la familia de tipos de `05` §3.1 que la sostiene
- [x] Ninguna regla de negocio se redacta acá: las refiere por identificador a `GeometriaFactory-Domain`
- [x] Se refinó contra la regla de exposición de `05` §3.2 y ningún campo que introduce puede transportar una dirección de servicio, una ruta de datos ni un secreto

## 7. Notas y supuestos

**La provisoria la produce el sistema y no la escribe el administrador** (`RN-14`); su mecanismo —que no sea adivinable y que no se repita— es de `GeometriaFactory-Infrastructure`. Lo que este tipo hace es transportarla una sola vez, hacia el administrador que la comunica.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 previó con este mismo identificador y esta misma pertenencia a necesidades de negocio. |
