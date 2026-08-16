# US-06008 — Conservar el texto original literal y rechazar toda escritura que lo reemplace

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** US-06008-Conservar-El-Texto-Original-Literal-Y-Rechazar-Toda-Escritura-Que-Lo-Reemplace.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-06004 Gestión del trabajo
**Etapa del producto:** `e`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que el texto original de un trabajo se guarde literal y que ninguna escritura posterior lo reemplace**, para **que el trabajo guardado sea siempre el que el programa del alumno produjo**.

## 2. Contexto

`RN-06008` fija que el texto original se conserva íntegro y **nunca se reescribe**, y `02` §6 declara que **el tramo principal de esa regla está acá**: es la capa donde el texto se escribe y por lo tanto donde puede perderse. `RC-06001` lo declara como regla conceptual de modelo: el texto original se escribe **una sola vez**. El contrato de uso es [`CU-06003`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-06003-Guardar-Y-Recuperar-Los-Trabajos.md).

## 3. Criterios de aceptación

- Given un trabajo con su texto, When se lo materializa, Then el texto guardado es **idéntico** al recibido, carácter por carácter.
- Given un trabajo existente, When se intenta materializarlo con un texto distinto, Then **la escritura se rechaza** con su condición: exactamente **0** escrituras aceptadas que reemplacen el texto conservado.
- Given el texto guardado, When se lo consulta, Then **no se consulta por su contenido**: se guarda como texto en la fila del trabajo, que es lo que permite reprocesarlo si el validador mejora.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00003, NB-00004 |
| CU cubiertos | CU-06003 |
| RN que ejerce | RN-06008, con tramo principal acá |
| Componente de `05` §3.1 | Adaptador de repositorio de trabajos |
| Reglas conceptuales de modelo | `RC-06001`, texto original escrito una sola vez |
| ¿Toma alguna decisión de negocio? | **No** |
| ¿Toca el almacén? | **Sí.** Su prueba de integración pertenece a `GeometriaFactory-Api` |
| BT derivadas | BT-06010 |
| Tests previstos en 08 | Prueba que materializa un trabajo existente con un texto distinto y comprueba el rechazo |

## 5. Prioridad y estimación

`Must` por `RN-06008`, y porque el criterio de transición `f` → `g` exige que el texto original se conserve íntegro y nunca se reescriba.

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

**La reedición de un trabajo en `Borrador` no contradice esta historia**: lo que la capa de aplicación pide es constituir el texto del trabajo reeditado, y lo que esta historia impide es que una escritura **reemplace** el texto conservado de un trabajo ya materializado. La distinción la fija `RC-06001`.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
