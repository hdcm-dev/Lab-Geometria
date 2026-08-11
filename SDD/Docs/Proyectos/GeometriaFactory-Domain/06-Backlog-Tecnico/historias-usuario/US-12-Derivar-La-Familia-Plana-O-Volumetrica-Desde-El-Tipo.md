# US-12 — Derivar la familia plana o volumétrica desde el tipo

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** US-12-Derivar-La-Familia-Plana-O-Volumetrica-Desde-El-Tipo.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-05 Interpretación y verificación del dato del alumno
**Etapa del producto:** `f`
**Prioridad MoSCoW:** Should
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca de dominio (`GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`)**, quiero **que la familia plana o volumétrica de una pieza se derive de su tipo y no se guarde**, para **que no exista un segundo lugar donde esa clasificación pueda quedar desincronizada del tipo**.

## 2. Contexto

`PRODUCT-INTAKE` §17.1.P.11 punto 4 declara la decisión pre-tomada: la familia plana o volumétrica **no se persiste**, se deriva del tipo por tabla de consulta. No es una capacidad `F-XX` del intake §4 sino una decisión técnica, y de ahí su prioridad.

## 3. Criterios de aceptación

- Given una pieza de un tipo volumétrico, When se consulta su familia, Then es volumétrica, y el valor no está almacenado en la entidad.
- Given una pieza de un tipo plano, When se consulta su familia, Then es plana, por la misma vía.
- Given una pieza cuyo tipo no está en la tabla de consulta, When se consulta su familia, Then la operación devuelve la condición correspondiente en lugar de asumir una familia.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-04 |
| CU cubiertos | CU-06 |
| RN e invariantes que ejerce | — |
| BT derivadas | BT-06 |
| Etapa del producto | `f`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba unitaria por tipo, y prueba de que ningún atributo de la entidad guarda la familia. |

## 5. Prioridad y estimación

`Should` porque su origen no es una capacidad del intake §4 sino una decisión técnica pre-tomada de §17.1.P.11. El dominio funciona sin ella; lo que se pierde es una derivación de conveniencia y la garantía de que no haya un segundo lugar donde desincronizarse. Es la **única** historia no `Must` de este backlog, y el fundamento del reparto está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.2.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Cita por identificador toda regla e invariante que ejerce, sin volver a enunciarla
- [x] Las condiciones de rechazo que produce existen en el catálogo de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Sus tareas técnicas están identificadas y ninguna está bloqueada

## 7. Notas y supuestos

Los **seis tipos dibujables** —tres volumétricos y tres planos— los declara `GeometriaFactory-Visor` para su dibujo (`05` §6 de ese proyecto de código). Que el dominio use la misma partición no es duplicar la decisión: acá es clasificación de la entidad y allá es qué malla se construye.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.3 previó con este mismo identificador y este mismo contenido. |
