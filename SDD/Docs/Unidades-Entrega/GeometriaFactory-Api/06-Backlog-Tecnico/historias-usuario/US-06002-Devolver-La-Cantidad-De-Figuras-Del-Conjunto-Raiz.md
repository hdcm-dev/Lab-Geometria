# US-06002 — Devolver la cantidad de figuras del conjunto raíz, incluidas las no reconstruidas

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-06002-Devolver-La-Cantidad-De-Figuras-Del-Conjunto-Raiz.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-06005 Interpretación y verificación del dato del alumno
**Etapa del producto:** `f`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **recibir cuántas figuras traía el conjunto raíz del texto, contando también las que no se pudieron reconstruir**, para **tener el rango contra el que validar la posición de cada observación**.

## 2. Contexto

`GeometriaFactory-Application` §3 declara que **la cantidad de figuras del conjunto raíz la produce el validador** y que **no es derivable de las piezas adoptadas**, que admiten huecos. El dominio la exige como precondición de la reconstrucción y su registro de observaciones la hereda como rango de posiciones válidas. El contrato de uso es [`CU-06001`](../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06001-Interpretar-El-Texto-Original-Y-Reconstruir-Las-Piezas.md).

## 3. Criterios de aceptación

- Given un texto con figuras de las que algunas no se reconstruyen, When se lo interpreta, Then la cantidad devuelta es la del **conjunto raíz** y no la de las piezas adoptadas.
- Given ese resultado, When se compara la cantidad con la lista de piezas, Then pueden diferir, y esa diferencia es información y no un defecto.
- Given un conjunto vacío, When se lo interpreta, Then la cantidad es **cero** y el resultado se distingue de un texto que no se pudo leer.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00004 |
| CU cubiertos | CU-06001 |
| RN que ejerce | RN-06009, con tramo principal acá |
| Componente de `05` §3.1 | Motor de interpretación de figuras |
| Reglas conceptuales de modelo | `RC-06002`, identidad posicional de la pieza |
| ¿Toma alguna decisión de negocio? | **No** |
| ¿Toca el almacén? | **No** |
| BT derivadas | BT-06016, BT-06018 |
| Tests previstos en 08 | Caso 9 de la batería, con el escenario `E-1` completo |

## 5. Prioridad y estimación

`Must` porque sin este dato la posición de una observación **no se puede validar contra ningún rango**, y un conjunto mal formado pasaría inadvertido hasta la capa de aplicación.

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

**El rechazo del conjunto mal formado no llega al alumno**: es un defecto de la interpretación y no de su trabajo, y así lo declara la capa de aplicación al describir su tramo de `RN-06009`.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
