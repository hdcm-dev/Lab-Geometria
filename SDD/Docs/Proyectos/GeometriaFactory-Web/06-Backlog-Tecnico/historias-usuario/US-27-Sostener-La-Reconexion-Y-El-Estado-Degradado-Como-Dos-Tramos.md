# US-27 — Sostener la reconexión y el estado degradado como dos tramos independientes

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** US-27-Sostener-La-Reconexion-Y-El-Estado-Degradado-Como-Dos-Tramos.md
**Versión:** 1.0
**Estado:** Propuesta
**Fecha:** 2026-08-10
**Autor:** Scrum Master (AG-06)
**Épica:** EP-03 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Superficie de 03:** `Estado-Degradado-Y-Reconexion`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **persona que usa el laboratorio**, quiero **que la aplicación distinga si se cortó mi conexión con ella o si el servicio de datos no responde**, para **saber si tengo que esperar, recargar o avisarle al docente**, y no encontrarme con una pantalla rota.

## 2. Contexto

`NB-08` pide alcance del laboratorio desde el aula. El caso de uso es [`CU-10`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-10-Sostener-La-Aplicacion-En-Estado-Degradado-Y-Reconexion.md), **transversal a los otros nueve**: concentra en un solo lugar la superficie donde `RA-03` se puede violar. `05` §4 declara los dos tramos como **independientes** y advierte que confundirlos es el error de lectura más probable de la pieza.

## 3. Criterios de aceptación

- Given una sesión abierta, When el circuito se corta y se restablece, Then la superficie muestra el cartel de reconexión y la persona vuelve a operar.
- Given una sesión abierta, When el servicio de datos deja de responder, Then la superficie declara el **estado degradado**, que es un tramo distinto del anterior y **no se mezcla con él**.
- Given cualquiera de los dos, When se inspecciona el mensaje, Then **no incluye la dirección de un servicio interno, un nombre de archivo de datos ni una traza**, y **nunca es una excepción sin manejar**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-08 |
| CU cubiertos | CU-10 |
| Restricciones transversales que la alcanzan | RT-03, RT-06, RT-07 |
| Componente de `05` §3.1 | Traductor de condiciones a presentación, Superficies |
| Quién hace cumplir lo que esta historia sólo ofrece | La disponibilidad del servicio no la sostiene esta pieza: el servidor es domiciliario y su caída es un riesgo aceptado |
| BT derivadas | BT-13 |
| Tests previstos en 08 | Recorrido con el servicio de datos detenido, y recorrido con la red cortada y restablecida —parte de `PT-01.c`— |

## 5. Prioridad y estimación

`Must` porque el estado degradado explícito es lo único de `NB-08` que la persona ve, y porque `PT-01.c` exige **reconexión funcional al cortar y restablecer la red**.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara la superficie de 03 que la aloja y el componente de `05` §3.1 que la sostiene
- [x] Declara qué restricciones transversales de `02` §6 la alcanzan
- [x] Toda condición que presenta es uno de los quince códigos vivos del contrato, o el camino de ausencia de respuesta
- [x] Ninguna afirmación depende de que la pieza pública haga cumplir una regla
- [x] Se puede maquetar y validar sin servicio de datos

## 7. Notas y supuestos

**El reciclado del proceso del hosting es el peor escenario del producto y la fuente declara que no tiene mitigación en el código** (`R-06`). Lo que sí hay es tratamiento: el estado «sesión no restablecible» está diseñado como estado propio de esta superficie, y **el envío es la única acción de guardado**, de modo que un corte no deja un trabajo a medias.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Numera y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 había repartido por necesidad de negocio con este identificador, y que su §3.2 dejó a la categoría 06 para redactar. |
