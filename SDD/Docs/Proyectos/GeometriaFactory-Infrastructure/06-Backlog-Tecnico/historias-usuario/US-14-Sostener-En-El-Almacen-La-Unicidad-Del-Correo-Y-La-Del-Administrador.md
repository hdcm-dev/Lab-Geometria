# US-14 — Sostener en el almacén la unicidad del correo y la del administrador

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** US-14-Sostener-En-El-Almacen-La-Unicidad-Del-Correo-Y-La-Del-Administrador.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-02 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que el almacén impida por sí mismo dos cuentas con el mismo correo y dos cuentas con papel `Administrador`**, para **que la unicidad no dependa sólo de una consulta previa que puede llegar tarde**.

## 2. Contexto

`RN-01` y `RN-02` fijan las dos unicidades, e `INV-01` e `INV-05` las sostienen. `02` §4 precisión 2 declara que **las restricciones de unicidad del almacén sí son una segunda línea, y eso es deliberado**: la consulta previa del consumidor **no es una garantía por sí sola**. El contrato de uso es [`CU-05`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-05-Guardar-Y-Recuperar-Las-Cuentas-De-La-Comision.md).

## 3. Criterios de aceptación

- Given una cuenta con un correo ya registrado, When se intenta materializarla, Then **el índice único del almacén lo impide** y se devuelve la condición de correo ya registrado.
- Given una segunda cuenta con papel `Administrador`, When se intenta materializarla, Then la restricción del almacén lo impide y se devuelve su condición propia.
- Given cualquiera de las dos negativas, When se inspecciona el mensaje, Then **impide el resultado sin explicar el camino**: no revela nada de la cuenta que ocupa el correo.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-01, NB-02 |
| CU cubiertos | CU-05 |
| RN que ejerce | RN-01, RN-02 |
| Componente de `05` §3.1 | Adaptador de repositorio de cuentas |
| Reglas conceptuales de modelo | — |
| ¿Toma alguna decisión de negocio? | **No.** La verificación previa sobre el conjunto es de la capa de aplicación |
| ¿Toca el almacén? | **Sí** |
| BT derivadas | BT-05, BT-09 |
| Tests previstos en 08 | Pruebas de integración contra el almacén real |

## 5. Prioridad y estimación

`Must` porque `05` §9 declara como riesgo de impacto alto que la unicidad del correo se sostenga **sólo** con la consulta previa: dos cuentas con el mismo correo harían que el ingreso dejara de ser determinista e `INV-01` dejaría de valer.

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

**El criterio de comparación de dos correos se decide acá**, y es donde `GeometriaFactory-Domain` y `GeometriaFactory-Application` lo derivaron: es el índice el que lo materializa. La decisión vive en la ADR correspondiente de la categoría 05 de este proyecto de código y se ejecuta en BT-09.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
