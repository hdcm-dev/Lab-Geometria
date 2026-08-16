# US-08007 — Transportar la solicitud única de eliminación del trabajo

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** US-08007-Transportar-La-Solicitud-Unica-De-Eliminacion.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-08004 Gestión del trabajo
**Etapa del producto:** `e`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **extremo que compila contra el contrato (`GeometriaFactory-Api` y `GeometriaFactory-Web`)**, quiero **una **única** solicitud de eliminación de trabajo, la misma para el alumno y para el administrador**, para **que la superficie no declare dos veces lo mismo y que la diferencia entre los dos caminos viva donde vive la regla**.

## 2. Contexto

`02` §3.1 declara que la eliminación por el administrador se absorbió en el mismo contrato de uso porque **reutiliza el mismo tipo** de solicitud que ya declaraba el alumno; lo que difiere es la regla que lo acota, y las reglas viven en `GeometriaFactory-Domain`. `RN-08004` enuncia los dos caminos.

## 3. Criterios de aceptación

- Given una eliminación pretendida por cualquiera de los dos papeles, When se arma la solicitud, Then es el **mismo** tipo, con la identidad del trabajo.
- Given una eliminación por el alumno sobre un trabajo que no está en `Borrador`, When el servicio responde, Then el rechazo viaja con su código propio; el tipo de la solicitud no cambia.
- Given una eliminación sobre un trabajo ajeno, When el servicio responde, Then el código y el texto son **los mismos** que para un trabajo inexistente: nada permite distinguirlos, por `RN-08003`.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00003, NB-00004 |
| CU cubiertos | CU-08003 |
| Familia de tipos de `05` §3.1 | Familia de trabajo |
| Restricciones transversales de `02` §6 | RT-03 |
| RN que refiere por identificador | RN-08003, RN-08004 |
| BT derivadas | BT-08008, BT-08012 |
| Etapa del producto | `e`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba de integración de la eliminación admitida y de las tres rechazadas: por estado, por pertenencia y por inexistencia. |

## 5. Prioridad y estimación

`Must` por derivar de `F-07` y de `F-24`, las dos `Must Have` en `PRODUCT-INTAKE` §4.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un contrato de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Declara la familia de tipos de `05` §3.1 que la sostiene
- [x] Ninguna regla de negocio se redacta acá: las refiere por identificador a `GeometriaFactory-Domain`
- [x] Se refinó contra la regla de exposición de `05` §3.2 y ningún campo que introduce puede transportar una dirección de servicio, una ruta de datos ni un secreto

## 7. Notas y supuestos

Que el trabajo ajeno y el inexistente compartan código **y** texto es una decisión de contrato y no una casualidad: si el texto difiriera, la distinción volvería por el mensaje. Es la lectura de `RN-08003` que `02` §4.1 declara para este proyecto de código.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 previó con este mismo identificador y esta misma pertenencia a necesidades de negocio. |
