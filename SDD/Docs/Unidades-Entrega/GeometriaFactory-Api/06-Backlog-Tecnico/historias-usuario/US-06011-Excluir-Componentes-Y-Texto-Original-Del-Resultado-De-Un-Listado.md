# US-06011 — Excluir componentes y texto original del resultado de un listado

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** US-06011-Excluir-Componentes-Y-Texto-Original-Del-Resultado-De-Un-Listado.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-06004 Gestión del trabajo
**Etapa del producto:** `e`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que el resultado de un listado no traiga los componentes de las piezas ni el texto original**, para **que la pantalla más pesada del producto no arrastre el trabajo entero de cada alumno**.

## 2. Contexto

`PRODUCT-INTAKE` §17.3.P.12 declara que los componentes se persisten pese a su redundancia **porque son parte del ejercicio**, y que se compensa **no cargándolos nunca en las consultas de listado**. La proyección separada del detalle es además la decisión de [`Contracts ADR-08005`](../../../../Producto/Adrs/ADR-08005-Proyeccion-De-Listado-Separada-Del-Detalle.md). El contrato de uso es [`CU-06003`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-06003-Guardar-Y-Recuperar-Los-Trabajos.md).

## 3. Criterios de aceptación

- Given una consulta de listado, When se la resuelve, Then los componentes cargados son exactamente **0** y las apariciones del texto original, **0**.
- Given una consulta de detalle, When se la resuelve, Then **sí** trae piezas, componentes y observaciones.
- Given el resultado de listado, When se inspecciona la colección de componentes, Then **no viene materializada**, y no sólo viene vacía.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00003, NB-00007 (parcial) |
| CU cubiertos | CU-06003 |
| RN que ejerce | — |
| Componente de `05` §3.1 | Adaptador de repositorio de trabajos |
| Reglas conceptuales de modelo | `RC-06004` |
| ¿Toma alguna decisión de negocio? | **No** |
| ¿Toca el almacén? | **Sí** |
| BT derivadas | BT-06010 |
| Tests previstos en 08 | Inspección de la proyección devuelta, comprobando que la colección no viene materializada |

## 5. Prioridad y estimación

`Must` porque `05` §9 declara con probabilidad **media-alta** el riesgo de que una consulta de listado arrastre los componentes o el texto —es el comportamiento por defecto de cualquier carga completa de entidad— y porque de eso depende el requerimiento de tiempo del listado del administrador.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el componente de `05` §3.1 y, si toca el almacén, las reglas conceptuales de modelo que materializa
- [x] Declara que no toma ninguna decisión de negocio
- [x] Toda condición que produce existe en el catálogo de las 17 de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md), declarada como resultado o como fallo
- [x] Declara el camino en que el mecanismo se detiene en lugar de cumplir a medias, cuando puede fallar
- [x] Declara si toca el almacén y, en consecuencia, dónde vive su prueba

## 7. Notas y supuestos

**Un cubo de lado 3 guarda seis caras idénticas para expresar un solo número**, y eso es deliberado: la redundancia **es parte del ejercicio** del alumno. La compensación es exactamente esta historia.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
