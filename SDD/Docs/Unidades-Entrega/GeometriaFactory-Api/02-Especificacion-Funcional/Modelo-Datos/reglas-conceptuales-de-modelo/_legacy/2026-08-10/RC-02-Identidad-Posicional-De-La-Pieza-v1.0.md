# RC-02 — La identidad de la pieza es su posición en el conjunto raíz

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** RC-02-Identidad-Posicional-De-La-Pieza.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`RN-09`](../../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-09-Observacion-De-Error-Con-Posicion-Y-Campo.md); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.11** §12 (entrada «Pieza»), §17.1.P.11 punto 2, §20.E-5; `Proyectos/GeometriaFactory-Domain/02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md`
**Trazabilidad downstream:** `05-Arquitectura-Tecnica` y `08-Calidad-Y-Pruebas` de GeometriaFactory-Infrastructure

---

## 1. Enunciado

Cada pieza se guarda **con su posición en el conjunto raíz del texto**, que es su identidad. El almacén **conserva las posiciones tal como el validador las declaró** y **no las compacta**: la posición de una figura que no se pudo reconstruir queda **reservada** y ninguna pieza posterior la ocupa.

Además se guarda, en la fila del trabajo, **cuántas figuras trae el conjunto raíz**, incluidas las que no se reconstruyeron. Es el rango de posiciones válidas del trabajo.

## 2. Justificación

El texto del alumno **no trae identificador de figura**: la única identidad disponible es el índice en el conjunto raíz, y alcanza para seleccionar y resaltar.

De ahí se sigue lo que hace que la regla importe: la observación de un error de validación **designa a la figura por su posición**, y si el almacén compactara, esa posición dejaría de coincidir con la figura que el alumno escribió. El mensaje que le dice «la figura 1 no tiene un tipo reconocible» apuntaría a otra figura, y la regla que exige ubicar el defecto quedaría vacía.

Guardar la cantidad de figuras del conjunto raíz es la otra mitad: **no es derivable de las piezas guardadas**, porque el conjunto admite huecos, y sin ella no hay rango contra el cual comprobar que una posición es válida.

## 3. Ámbito de aplicación

- Alcanza a las **piezas** del trabajo y a las **observaciones**, que se guardan con la posición que designan.
- Los **componentes** cuelgan de su pieza y no llevan posición propia en el conjunto raíz.
- Una observación puede designar **una posición sin pieza**: es el caso de la figura no reconstruida, y es válido mientras la posición pertenezca al rango declarado.
- No alcanza al orden de dibujo ni a la disposición en la escena, que son de la pieza que dibuja.

## 4. Consecuencia si se viola

Compactar las posiciones no produce ningún rechazo del almacén: **produce mensajes que apuntan a la figura equivocada**. Es un defecto silencioso, y por eso la verificación es explícita.

Guardar una observación sobre una posición fuera del rango declarado sí produce rechazo, y no es de este proyecto de código: el dominio lo rechaza, y la capa de aplicación lo agrega bajo un motivo propio.

## 5. CU afectados

- [`CU-01`](../../Casos-De-Uso/CU-01-Interpretar-El-Texto-Original-Y-Reconstruir-Las-Piezas.md) — Interpretar y reconstruir: es donde la posición se calcula y se reserva.
- [`CU-03`](../../Casos-De-Uso/CU-03-Guardar-Y-Recuperar-Los-Trabajos.md) — Guardar y recuperar: es donde se conserva.

## 6. Pruebas que la verifican

`CU-01` CA-04 sobre el escenario **E-5**, que exige que el índice reportado sea **1 y no 0** —la forma de comprobar que la posición se calcula y no se informa siempre la primera—, y `CU-03` CA-08, que exige que la observación de una figura no reconstruida conserve su posición después de materializar y recuperar.

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. |
