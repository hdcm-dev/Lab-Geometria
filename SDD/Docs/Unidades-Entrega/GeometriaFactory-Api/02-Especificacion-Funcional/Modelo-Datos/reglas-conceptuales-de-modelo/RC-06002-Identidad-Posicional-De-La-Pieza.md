# RC-06002 — La identidad de la pieza es su posición en el conjunto raíz

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** RC-06002-Identidad-Posicional-De-La-Pieza.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`RN-02009`](../../Reglas-De-Negocio/RN-02009-Observacion-De-Error-Con-Posicion-Y-Campo.md); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.12** §12 (entrada «Pieza»), §17.1.P.11 · GeometriaFactory-Domain punto 2, §20.E-5; `Unidades-Entrega/GeometriaFactory-Api/02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md`
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

- [`CU-06001`](../../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06001-Interpretar-El-Texto-Original-Y-Reconstruir-Las-Piezas.md) — Interpretar y reconstruir: es donde la posición se calcula y se reserva.
- [`CU-06003`](../../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06003-Guardar-Y-Recuperar-Los-Trabajos.md) — Guardar y recuperar: es donde se conserva.

## 6. Pruebas que la verifican

`CU-06001` CA-04 sobre el escenario **E-5**, que exige que el índice reportado sea **1 y no 0** —la forma de comprobar que la posición se calcula y no se informa siempre la primera—, y `CU-06003` CA-08, que exige que la observación de una figura no reconstruida conserve su posición después de materializar y recuperar.

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. |
| 1.1 | 2026-08-10 | Actualización de la cita del `PRODUCT-INTAKE` de **1.11** a **1.12** en la trazabilidad upstream: 1.11 quedó archivada al resolver el Product Owner el desenlace del envío del escenario `E-8`. Corrige el hallazgo **H-02** del informe de auditoría `SDD/Docs/Audit/B-02-03-GeometriaFactory-Infrastructure-r1.md` (ronda 1). El delta entre 1.11 y 1.12 se revisó y sólo alcanza a `E-8`, que no toca lo que este documento declara: sin cambios de contenido. |
