# US-23 — No pedir los trabajos en `Borrador` y responder «no encontrado» al pedirlos por dirección directa

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** US-23-No-Pedir-Los-Borradores-Y-Responder-No-Encontrado.md
**Versión:** 1.0
**Estado:** Propuesta
**Fecha:** 2026-08-10
**Autor:** Scrum Master (AG-06)
**Épica:** EP-05 Gestión del trabajo
**Etapa del producto:** `e`
**Superficie de 03:** `Listado-De-La-Comision`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **administrador**, quiero **que el listado de la comisión no me traiga los trabajos que los alumnos todavía están armando**, para **revisar sólo lo que me entregaron**, y como **producto**, que pedir uno por dirección directa responda «no encontrado».

## 2. Contexto

`RN-11` declara que el administrador **no ve los trabajos en `Borrador`**: no forman parte de su flujo de trabajo. El caso de uso es [`CU-08`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-08-Recorrer-La-Entrega-De-La-Comision.md). `05` §10.3 declara qué hace esta pieza por `RN-11`: **no los pide**, porque el listado se trae ya acotado.

## 3. Criterios de aceptación

- Given un alumno con un borrador y un trabajo en estado `Pendiente`, When el administrador abre el listado de la comisión, Then ve **sólo el pendiente**.
- Given un borrador pedido por **dirección directa**, When se lo solicita, Then la respuesta es **«no encontrado»** y no «no autorizado».
- Given la superficie, When se busca un control para pedir borradores, Then **no hay ninguno**: la puerta por la que la regla se rompería no se ofrece.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-07, NB-09 |
| CU cubiertos | CU-08 |
| Restricciones transversales que la alcanzan | RT-03, RT-07, RT-09 |
| Componente de `05` §3.1 | Superficies, Traductor de condiciones a presentación |
| Quién hace cumplir lo que esta historia sólo ofrece | El recorte lo decide el dominio, y `GeometriaFactory-Api` **no declara ningún parámetro** con el que se pueda pedir un borrador ajeno |
| BT derivadas | BT-11, BT-13 |
| Tests previstos en 08 | Paso del guion de la etapa `e`, sobre un alumno con un borrador y un pendiente |

## 5. Prioridad y estimación

`Must` por `RN-11` y porque el criterio de transición `e` → `f` exige que el listado del administrador **no incluya los que están en estado `Borrador`**.

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

**Que la respuesta sea «no encontrado» y no «no autorizado» es una decisión de traducción de `GeometriaFactory-Api`**, que declara ese punto como uno de los dos que puede romper una regla hacia afuera sin que ninguna capa de adentro se entere. Esta pieza sólo la presenta.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Numera y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 había repartido por necesidad de negocio con este identificador, y que su §3.2 dejó a la categoría 06 para redactar. |
