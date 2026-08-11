# US-01 — Transportar el canje de credenciales y la respuesta de sesión de cuatro campos

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** US-01-Transportar-El-Canje-De-Credenciales-Y-La-Respuesta-De-Sesion.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-02 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **extremo que compila contra el contrato (`GeometriaFactory-Api` y `GeometriaFactory-Web`)**, quiero **los tipos con los que viajan el correo y la contraseña presentada, y la respuesta de sesión con sus **cuatro** campos y ninguno más**, para **que la sesión se pueda establecer entre los dos procesos sin que ninguna forma de la contraseña almacenada ni la clave de firma crucen la frontera**.

## 2. Contexto

La capacidad `F-05` del intake §4 declara el inicio y el cierre de sesión. El contrato de uso es [`CU-01`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-01-Contrato-De-Canje-De-Credenciales-Y-Sesion.md). `PRODUCT-INTAKE` §17.4.P.5 declara que ningún tipo lleva el hash de la contraseña ni la clave de firma, y `05` §8 fija la respuesta de sesión en exactamente cuatro campos.

## 3. Criterios de aceptación

- Given una credencial presentada, When se arma la solicitud de canje, Then el tipo transporta el correo y la contraseña presentada, y **nada más**.
- Given un canje que procede, When se arma la respuesta de sesión, Then tiene exactamente **cuatro** campos, y ninguno de ellos transporta una condición que impida operar: esas viajan como respuesta de error con código propio (`RT-10`).
- Given la inspección de la superficie pública de esta familia, When se buscan campos capaces de transportar el hash de la contraseña o la clave de firma, Then hay exactamente **cero**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-01, NB-02 |
| CU cubiertos | CU-01 |
| Familia de tipos de `05` §3.1 | Familia de sesión |
| Restricciones transversales de `02` §6 | RT-01, RT-10, RT-11 |
| RN que refiere por identificador | RN-01, RN-06 |
| BT derivadas | BT-06, BT-08, BT-09 |
| Etapa del producto | `c`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba de integración que golpea el servicio real, más la prueba de inspección de superficie pública que verifica los cuatro campos y los campos prohibidos. |

## 5. Prioridad y estimación

`Must` por derivar de `F-05`, `Must Have` en `PRODUCT-INTAKE` §4, y porque el criterio de la transición `c` → `d` del roadmap §5.2 exige que la credencial de sesión no sea observable desde el navegador.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un contrato de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Declara la familia de tipos de `05` §3.1 que la sostiene
- [x] Ninguna regla de negocio se redacta acá: las refiere por identificador a `GeometriaFactory-Domain`
- [x] Se refinó contra la regla de exposición de `05` §3.2 y ningún campo que introduce puede transportar una dirección de servicio, una ruta de datos ni un secreto

## 7. Notas y supuestos

**Todas las solicitudes las arma el servidor de la unidad pública y viajan servidor a servidor, incluidas las que llevan credenciales en claro** (`RT-11`, `05` §3.2). Este tipo no habilita a que el navegador invoque el servicio de datos, y esa es una prohibición de forma y no de contenido.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 previó con este mismo identificador y esta misma pertenencia a necesidades de negocio. |
