# US-06010 — Resolver la consulta con el recorte ya trasladado al pedido

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-06010-Resolver-La-Consulta-Con-El-Recorte-Ya-Trasladado-Al-Pedido.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-06004 Gestión del trabajo
**Etapa del producto:** `e`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que una consulta de trabajos sólo se resuelva si trae su recorte declarado**, para **que no exista ningún camino por el que se pueda traer el conjunto completo de la comisión**.

## 2. Contexto

`RN-06003` y `RN-06011` acotan lo que cada papel ve, y `02` §6 declara que esta capa las ejerce **de forma negativa**: **la consulta sin recorte declarado no se resuelve**. Esta capa **no comprueba pertenencia**; lo que hace es **no ofrecer el camino** por el que la regla se rompería. El contrato de uso es [`CU-06003`](../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06003-Guardar-Y-Recuperar-Los-Trabajos.md).

## 3. Criterios de aceptación

- Given una consulta con su recorte declarado —por dueño o por alcance—, When se la resuelve, Then devuelve exactamente lo que ese recorte incluye.
- Given una consulta **sin** recorte declarado, When se la intenta, Then **no se resuelve** y se devuelve su condición.
- Given un pedido con alcance de administrador, When se lo resuelve, Then **el borrador no viaja**: el predicado llega en el pedido y no se aplica después.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00003, NB-00007 (parcial), NB-00009 (parcial) |
| CU cubiertos | CU-06003 |
| RN que ejerce | RN-06003 y RN-06011, **de forma negativa** |
| Componente de `05` §3.1 | Adaptador de repositorio de trabajos |
| Reglas conceptuales de modelo | — |
| ¿Toma alguna decisión de negocio? | **No.** El recorte llega decidido; duplicarlo acá crearía un segundo lugar donde la regla puede decir otra cosa |
| ¿Toca el almacén? | **Sí** |
| BT derivadas | BT-06010 |
| Tests previstos en 08 | Prueba de consulta sin recorte declarado, comprobando que no se resuelve |

## 5. Prioridad y estimación

`Must` porque `05` §2.1 descartó el repositorio genérico precisamente porque **obligaría a que el recorte se arme del lado del consumidor**, que es justo lo que esta historia impide.

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

**El traslado del recorte no es una comprobación de autorización** (`02` §4, precisión 1). Que una consulta llegue acotada es una decisión ya tomada afuera; acá se resuelve el pedido tal como viene.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
