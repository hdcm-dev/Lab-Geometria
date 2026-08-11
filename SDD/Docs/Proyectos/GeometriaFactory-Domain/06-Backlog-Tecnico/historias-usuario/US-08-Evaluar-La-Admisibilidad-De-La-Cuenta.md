# US-08 — Evaluar la admisibilidad de la cuenta y devolver su motivo

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** US-08-Evaluar-La-Admisibilidad-De-La-Cuenta.md
**Versión:** 1.0
**Estado:** Propuesta
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-02 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca de dominio (`GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`)**, quiero **una puerta única que responda si una cuenta admite acceso y, si no lo admite, con qué motivo**, para **que la misma condición no se compruebe de tres maneras distintas en tres lugares distintos, que es como se abre el agujero**.

## 2. Contexto

`RN-06` declara que una cuenta `Pendiente` o `Bloqueado` no obtiene sesión, e `INV-06` lo expresa como condición permanente. [`ADR-05`](../../05-Arquitectura-Tecnica/Adrs/ADR-05-Guarda-Unica-De-Admisibilidad.md) decide que `INV-06` e `INV-09` se ejerzan **en un solo lugar** y no repetidos en cada operación; `05` §9 registra que la familia de defectos que la ausencia de esa puerta habilita ya se abrió con precedente documentado.

## 3. Criterios de aceptación

- Given una cuenta de alumno `Habilitado`, con credencial fijada y sin la marca de cambio pendiente, When se evalúa su admisibilidad, Then es admisible.
- Given una cuenta de alumno en estado `Pendiente` o en `Bloqueado`, When se evalúa su admisibilidad, Then no es admisible y el resultado trae el motivo, que es lo que el consumidor traduce hacia afuera.
- Given una cuenta con la marca de cambio de contraseña pendiente puesta, When se evalúa su admisibilidad, Then no es admisible por ese motivo, aunque su estado de cuenta sea `Habilitado`.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-01, NB-02 |
| CU cubiertos | CU-04 |
| RN e invariantes que ejerce | RN-06, RN-13, RN-15, RN-16; INV-06, INV-09 |
| BT derivadas | BT-11 |
| Etapa del producto | `c`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba unitaria por cada motivo de no admisión y por el caso admisible, más la matriz de ejercicio de `INV-06` e `INV-09` (BT-14). |

## 5. Prioridad y estimación

`Must` por `RN-06`, declarada cerrada en `PRODUCT-INTAKE` §4.1, y porque es la puerta de la que dependen la sesión y el resto de las capacidades.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Cita por identificador toda regla e invariante que ejerce, sin volver a enunciarla
- [x] Las condiciones de rechazo que produce existen en el catálogo de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Sus tareas técnicas están identificadas y ninguna está bloqueada

## 7. Notas y supuestos

**El alcance efectivo de `INV-09` fuera de la admisibilidad queda abierto** ([`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §9): el dominio no tiene una puerta única por la que pasen todas las capacidades, y la guarda se concentra acá con el fundamento de que ninguna capacidad se ejerce sin admisión resuelta. Si la capa que expone habilitara un camino que no pase por acá, la marca tendría que volver a comprobarse ahí.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.3 previó con este mismo identificador y este mismo contenido. |
