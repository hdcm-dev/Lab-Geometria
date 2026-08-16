# US-08005 — Transportar la baja con su confirmación escrita

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** US-08005-Transportar-La-Baja-Con-Su-Confirmacion-Escrita.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-08003 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **extremo que compila contra el contrato (`GeometriaFactory-Api` y `GeometriaFactory-Web`)**, quiero **el tipo con el que viaja la baja de una cuenta, con la confirmación escrita como campo de la solicitud**, para **que la única operación destructiva del ciclo de vida no se dispare por accidente ni por una petición armada a mano**.

## 2. Contexto

`RN-08007` declara que la baja elimina la cuenta y **todos sus trabajos**, y que exige confirmación explícita escribiendo el correo de la cuenta. La capacidad `F-03` del intake §4 la declara `Must Have`.

## 3. Criterios de aceptación

- Given una baja pretendida, When se arma la solicitud, Then el tipo transporta la identidad de la cuenta y la **confirmación escrita** como campo propio.
- Given una confirmación escrita que no coincide con el correo de la cuenta, When el servicio responde, Then el rechazo viaja con su código propio del conjunto cerrado.
- Given la solicitud de baja, When se la compara con la de reseteo, Then son tipos distintos y de familias distintas: una destruye y la otra conserva, y el contrato no las confunde.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00001, NB-00002 |
| CU cubiertos | CU-08002 |
| Familia de tipos de `05` §3.1 | Familia de cuentas |
| Restricciones transversales de `02` §6 | RT-01 |
| RN que refiere por identificador | RN-08007, RN-08012 |
| BT derivadas | BT-08008, BT-08010, BT-08011 |
| Etapa del producto | `d`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba de integración de la baja con la confirmación correcta y con la incorrecta. |

## 5. Prioridad y estimación

`Must` por derivar de `F-03`, `Must Have` en `PRODUCT-INTAKE` §4, y porque el roadmap §5.2 verifica en la transición `d` → `e` que la baja exige escribir el correo de la cuenta.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un contrato de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Declara la familia de tipos de `05` §3.1 que la sostiene
- [x] Ninguna regla de negocio se redacta acá: las refiere por identificador a `GeometriaFactory-Domain`
- [x] Se refinó contra la regla de exposición de `05` §3.2 y ningún campo que introduce puede transportar una dirección de servicio, una ruta de datos ni un secreto

## 7. Notas y supuestos

**La separación entre esta historia y US-08021 es la que `F-26` vino a cerrar**: hasta el intake 1.7 el único camino ante un olvido de contraseña era dar de baja y volver a dar de alta, y por `RN-08007` eso eliminaba todos los trabajos del alumno. Que sean dos familias de tipos distintas es lo que hace difícil volver a confundirlas.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 previó con este mismo identificador y esta misma pertenencia a necesidades de negocio. |
