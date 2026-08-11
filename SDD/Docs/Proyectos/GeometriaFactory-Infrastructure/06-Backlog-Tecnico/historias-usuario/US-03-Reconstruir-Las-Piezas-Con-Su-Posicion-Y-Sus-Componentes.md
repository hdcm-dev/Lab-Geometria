# US-03 — Reconstruir las piezas con su posición, sus componentes y la posición reservada de las no reconstruidas

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** US-03-Reconstruir-Las-Piezas-Con-Su-Posicion-Y-Sus-Componentes.md
**Versión:** 1.0
**Estado:** Propuesta
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-05 Interpretación y verificación del dato del alumno
**Etapa del producto:** `f`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **recibir las piezas del texto con su posición y sus componentes, y que la posición de las que no se reconstruyeron quede reservada**, para **que el índice siga siendo la identidad de la pieza y el dibujo y el árbol puedan señalarse entre sí**.

## 2. Contexto

`NB-04` pide interpretación fiel y `NB-06` recibe de acá **la identidad posicional de la pieza**, que es el dato con el que después se dibuja y se arma el árbol (`02` §7.2). El contrato de uso es [`CU-01`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-01-Interpretar-El-Texto-Original-Y-Reconstruir-Las-Piezas.md), y `RC-02` declara la identidad posicional.

## 3. Criterios de aceptación

- Given un texto con varias figuras, When se lo interpreta, Then cada pieza viene con su **posición** y con sus componentes.
- Given una figura que no se pudo reconstruir, When se devuelve el conjunto, Then **su posición queda reservada** y el conjunto **no se renumera**.
- Given el escenario `E-7`, When se lo interpreta, Then se reconstruyen los **seis** tipos que el producto sabe dibujar, tres volumétricos y tres planos.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-04, NB-06 (parcial) |
| CU cubiertos | CU-01 |
| RN que ejerce | RN-09, con tramo principal acá |
| Componente de `05` §3.1 | Motor de interpretación de figuras |
| Reglas conceptuales de modelo | `RC-02`, `RC-04` —la familia no se persiste— |
| ¿Toma alguna decisión de negocio? | **No** |
| ¿Toca el almacén? | **No** |
| BT derivadas | BT-16, BT-18, BT-24 |
| Tests previstos en 08 | Escenario `E-7` como cobertura adicional declarada de los seis tipos |

## 5. Prioridad y estimación

`Must` porque la identidad posicional es lo que hace posible la sincronización árbol ⇄ escena de la etapa `g`, y porque sin componentes no hay valor derivado que verificar en US-05.

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

**Hasta dónde llega el conjunto de tipos reconstruibles sigue abierto.** Los **seis** que los escenarios ejercitan son los que la pieza que dibuja sabe dibujar, y **ninguna fuente enumera las clases de la actividad**; un tipo fuera del conjunto produce error de validación, que es correcto **pero puede no ser lo deseado**. Es `PA-04` de [`../Product-Backlog.md`](../Product-Backlog.md) §6, elevado con BT-24.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
