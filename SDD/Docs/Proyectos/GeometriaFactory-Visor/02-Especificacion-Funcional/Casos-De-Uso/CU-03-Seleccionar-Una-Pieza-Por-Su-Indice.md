# CU-03 — Seleccionar una pieza por su índice

**Proyecto de código:** GeometriaFactory-Visor
**Documento:** CU-03-Seleccionar-Una-Pieza-Por-Su-Indice.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `NB-06-Visualizacion-Dentro-Del-Producto.md` §1, §4 y §5 (quinto criterio, sincronización entre el árbol y la escena); `00-Contexto/Vision-Producto.md` §9 (entrada «pieza»); `00-Contexto/Alcance-Producto.md` **§4.1** (capacidad F-13, `Must Have` desde `PRODUCT-INTAKE` **1.19**); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §17.7 P.3, §17.7 P.8 (criterio de sincronización por índice), §14 (RA-02), §20 E-1 y §20 E-7
**Trazabilidad downstream:** 03-UX-UI-DX, 05-Arquitectura-Tecnica, 06-Backlog-Tecnico, 08-Calidad-Y-Pruebas, 10-Examples

---

## Tabla de contenido

- [1. Propósito](#1-propósito)
- [2. Actores](#2-actores)
- [3. Precondiciones](#3-precondiciones)
- [4. Flujo principal](#4-flujo-principal)
- [5. Flujos alternativos](#5-flujos-alternativos)
- [6. Excepciones y errores](#6-excepciones-y-errores)
- [7. Postcondiciones](#7-postcondiciones)
- [8. Criterios de aceptación](#8-criterios-de-aceptación)
- [9. Trazabilidad](#9-trazabilidad)
- [10. Notas y supuestos](#10-notas-y-supuestos)
- [11. Control de cambios](#11-control-de-cambios)

---

## 1. Propósito

Permitir que el componente anfitrión resalte en la escena la pieza que corresponde a un índice del conjunto raíz del trabajo, de modo que el árbol que él presenta y la escena que dibuja la fachada queden sincronizados sin traducir identidades: el índice es el mismo de los dos lados.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Componente anfitrión | Primario | Decide qué índice resaltar —por ejemplo, porque alguien tocó un elemento del árbol que él presenta— e invoca `seleccionarPieza` |
| Fachada del visor | Sistema | Resalta la pieza de ese índice en la escena, desresalta la anterior y confirma la selección efectiva |

## 3. Precondiciones

1. Existe una instancia viva y el componente anfitrión tiene su identificador (`CU-01`).
2. Esa instancia tiene un resultado de dibujo vigente producido por una carga previa (`CU-02`).
3. El índice que el componente anfitrión pasa proviene del propio resultado de dibujo. La fachada no le impone al anfitrión cómo obtuvo ese índice.

## 4. Flujo principal

| Paso | Actor | Acción |
| --- | --- | --- |
| 1 | Componente anfitrión | Invoca `seleccionarPieza(id, indice)` con el identificador de una instancia viva y el índice de la pieza |
| 2 | Fachada del visor | Verifica que el índice corresponde a una pieza dibujada del resultado de dibujo vigente |
| 3 | Fachada del visor | Quita el resaltado de la pieza que estuviera resaltada, si había alguna |
| 4 | Fachada del visor | Resalta la pieza del índice indicado, sin moverla ni cambiar su tamaño |
| 5 | Fachada del visor | Confirma la selección efectiva, informando el índice que quedó resaltado |

## 5. Flujos alternativos

| Id | Disparador | Curso | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 · Selección del mismo índice ya resaltado | El componente anfitrión invoca con el índice que ya estaba resaltado | La operación es idempotente: la pieza sigue resaltada, sin parpadeo de estado ni doble resaltado | Paso 5 del flujo principal |
| FA-02 · Índice de una pieza no dibujada | El índice corresponde a una pieza que el resultado de dibujo enumera como **no dibujada** | No hay malla que resaltar: la selección vigente se conserva y se informa la condición con el índice, para que el componente anfitrión pueda explicar por qué esa pieza no se resalta | Paso 5, con la selección previa intacta |
| FA-03 · Limpiar la selección | El componente anfitrión invoca con el valor que su contrato reserva para «ninguna pieza» | Se quita el resaltado vigente y la escena queda sin pieza resaltada. No es una condición de error | Paso 5 del flujo principal |
| FA-04 · Selección después de una carga nueva | Se invocó `cargarJson` sobre la instancia y luego `seleccionarPieza` | La selección se aplica sobre el resultado de dibujo nuevo: los índices son los del trabajo recién cargado y no los del anterior | Paso 2 del flujo principal |

## 6. Excepciones y errores

| Código | Causa | Respuesta de la fachada |
| --- | --- | --- |
| `INSTANCIA_DESCONOCIDA` | El identificador no corresponde a ninguna instancia viva | Ninguna instancia cambia y se informa el código |
| `INDICE_FUERA_DE_RANGO` | El índice no corresponde a ninguna pieza del resultado de dibujo vigente, o no hay resultado de dibujo vigente porque no se cargó nada | La selección vigente se conserva y se informa el código. No se resalta ninguna pieza por aproximación |

Las dos condiciones terminan de forma controlada y dejan la escena exactamente como estaba.

## 7. Postcondiciones

- **Éxito:** hay a lo sumo una pieza resaltada en la instancia, y es la del índice indicado; la disposición, el resultado de dibujo y el encuadre no cambiaron; hubo 0 peticiones de red.
- **Fallo:** la selección vigente antes de la invocación se conserva y la escena queda intacta.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Una instancia con el texto del escenario E-1 cargado, con tres piezas de índices 0, 1 y 2, y ninguna pieza resaltada | El componente anfitrión invoca `seleccionarPieza(id, 2)` | Queda resaltada la pieza de índice 2, que es el ortoedro, y ninguna otra |
| CA-02 | La misma instancia, con la pieza de índice 2 resaltada | El componente anfitrión invoca `seleccionarPieza(id, 0)` | Queda resaltada únicamente la pieza de índice 0, que es el cilindro: el resaltado es exclusivo |
| CA-03 | Una instancia con el texto del escenario E-7 cargado, con seis piezas de índices 0 a 5 | El componente anfitrión invoca `seleccionarPieza(id, 6)` | La fachada informa `INDICE_FUERA_DE_RANGO`, la selección vigente se conserva y ninguna pieza se resalta por aproximación |
| CA-04 | Una instancia con el texto del escenario E-7 cargado y la pieza de índice 3 resaltada | El componente anfitrión invoca `seleccionarPieza(id, 3)` otra vez | La pieza de índice 3 sigue resaltada y no hay dos piezas resaltadas a la vez |
| CA-05 | Una instancia con el texto del escenario E-1 cargado y la pestaña de red vacía | El componente anfitrión invoca `seleccionarPieza(id, 1)` | Queda resaltada la pieza de índice 1, que es el cubo, y la pestaña de red registra exactamente 0 peticiones originadas por la fachada |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-06, quinto criterio de éxito (sincronización entre el árbol y la escena) |
| Reglas de negocio aplicables | Ninguna. Este proyecto de código no declara RN (ver `README.md` de la sección) |
| Historias de usuario a generar | US de resaltado exclusivo por índice y de sincronización del árbol con la escena, en 06-Backlog-Tecnico |
| Componentes esperados | Fachada plana y servicio de dibujo, con el registro de índice por malla; 05-Arquitectura-Tecnica fija la composición |
| Tests previstos | 08-Calidad-Y-Pruebas: selección por índice sobre E-1 y E-7, exclusividad del resaltado, idempotencia, índice fuera de rango y conteo de peticiones en 0 |
| Concepto central | `Definicion-Contrato-De-Fachada.md` §4.3, §5.2 y §6 |

## 10. Notas y supuestos

- «Pieza» se usa acá en el referente del dominio declarado en `Vision-Producto.md` §9.1: cada figura del conjunto raíz del trabajo. El segundo referente del término —cada uno de los artefactos desplegables del producto— se escribe siempre calificado y no aparece en este caso de uso.
- La identidad de una pieza es su **posición en el conjunto raíz**, porque el dato del alumno no trae identificador propio. Ese es el motivo por el que la fachada opera por índice y no por nombre.
- Cómo se ve el resaltado —color, contorno, opacidad— es decisión de 03-UX-UI-DX y no se fija acá. Lo que este caso de uso fija es que hay a lo sumo un resaltado por instancia y que corresponde al índice pedido.
- La fachada no origina la selección: no escucha gestos sobre la escena para decidir qué resaltar. Es el componente anfitrión el que decide y el que invoca.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Contrato de uso de `seleccionarPieza`, con cuatro flujos alternativos, dos condiciones de error y cinco criterios de aceptación anclados en los índices de los escenarios E-1 y E-7. |
| 1.1 | 2026-08-11 | **Absorbe la promoción de F-13 a `Must Have`**, decidida por el Product Owner y registrada en `PRODUCT-INTAKE` **1.19** §4. La trazabilidad upstream remitía a `Alcance-Producto.md` **§4.2**, «capacidades declaradas con prioridad menor», y esa remisión quedó falsa: F-13 pasó a **§4.1** con el resto del alcance comprometido en la versión 1.6 de ese documento. Se corrige la sección y se declara la prioridad vigente. Ningún flujo, condición de error ni criterio de aceptación de este contrato de uso cambia: la sincronización por índice ya se especificaba entera, porque `PRODUCT-INTAKE` §17.7 P.8 la incluye entre lo que `PT-02` mide antes de comprometer la etapa `g`, que es precisamente el fundamento de la promoción. Sube minor. |
