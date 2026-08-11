# US-03 — Transportar el listado de cuentas del panel del administrador

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** US-03-Transportar-El-Listado-De-Cuentas-Del-Panel-Del-Administrador.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-03 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **extremo que compila contra el contrato (`GeometriaFactory-Api` y `GeometriaFactory-Web`)**, quiero **el tipo con el que viaja el listado de cuentas que el administrador ve en su panel**, para **que el administrador vea a todos sus alumnos con su situación, y desde ahí los habilite, los bloquee, los dé de baja y les resetee la credencial**.

## 2. Contexto

La capacidad `F-03` del intake §4 declara el panel del administrador, y `F-26` declara que el reseteo se acciona **desde el mismo panel**. El contrato de uso es [`CU-02`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-02-Contrato-De-Administracion-De-Cuentas.md).

## 3. Criterios de aceptación

- Given el conjunto de cuentas de la comisión, When se arma la proyección del listado, Then cada entrada transporta la identidad de la cuenta y su situación.
- Given esa misma proyección, When se inspecciona su superficie, Then **ninguna entrada transporta ninguna forma de la contraseña almacenada**, por `RT-01`.
- Given una cuenta con la marca de cambio de contraseña pendiente, When se arma su entrada, Then la marca no viaja como campo que impida operar en la respuesta de sesión: ese camino es el de `RT-10` y es US-14.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-01, NB-02 |
| CU cubiertos | CU-02 |
| Familia de tipos de `05` §3.1 | Familia de cuentas |
| Restricciones transversales de `02` §6 | RT-01, RT-10 |
| RN que refiere por identificador | RN-01, RN-06 |
| BT derivadas | BT-08, BT-10 |
| Etapa del producto | `d`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba de integración del listado, más la prueba de inspección de superficie pública sobre los campos prohibidos. |

## 5. Prioridad y estimación

`Must` por derivar de `F-03` y de `F-26`, las dos `Must Have` en `PRODUCT-INTAKE` §4.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un contrato de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Declara la familia de tipos de `05` §3.1 que la sostiene
- [x] Ninguna regla de negocio se redacta acá: las refiere por identificador a `GeometriaFactory-Domain`
- [x] Se refinó contra la regla de exposición de `05` §3.2 y ningún campo que introduce puede transportar una dirección de servicio, una ruta de datos ni un secreto

## 7. Notas y supuestos

**El panel en sí es de `GeometriaFactory-Web`**; lo que este proyecto de código aporta es el tipo con el que sus datos cruzan la frontera. La unicidad del administrador y el arrastre de trabajos en la baja son invariantes de `GeometriaFactory-Domain` (`02` §4.1).

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 previó con este mismo identificador y esta misma pertenencia a necesidades de negocio. |
