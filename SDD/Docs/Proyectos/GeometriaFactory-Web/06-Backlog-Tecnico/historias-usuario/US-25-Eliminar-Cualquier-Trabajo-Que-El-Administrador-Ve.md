# US-25 — Eliminar cualquier trabajo que el administrador ve, verificado forzando la solicitud

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** US-25-Eliminar-Cualquier-Trabajo-Que-El-Administrador-Ve.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master (AG-06)
**Épica:** EP-08 Desenlace de la entrega
**Etapa del producto:** `h`
**Superficie de 03:** `Resolucion-Del-Trabajo`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **administrador**, quiero **retirar cualquier trabajo que veo, en cualquiera de los tres estados que mi alcance incluye**, para **limpiar la entrega de la comisión sin depender del alumno**.

## 2. Contexto

`RN-04` declara que el administrador elimina cualquier trabajo que ve, con borrado físico, y `F-24` del intake §4 lo declara `Must Have`. El caso de uso es [`CU-09`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-09-Resolver-Un-Trabajo-Con-Comentario-Opcional.md) FA-03, absorbido ahí porque es **el mismo panel y la misma solicitud de eliminación que ya usa el alumno**; lo que difiere es la regla que la acota.

## 3. Criterios de aceptación

- Given un trabajo en `Pendiente`, `Finalizado` o `Rechazado`, When el administrador lo elimina, Then el trabajo **desaparece**.
- Given un trabajo en `Borrador`, When el administrador lo busca para eliminarlo, Then **no lo ve**: no forma parte de su alcance.
- Given una solicitud de eliminación **forzada sin pasar por la pantalla** sobre un trabajo fuera de alcance, When se la envía, Then el servicio de datos la rechaza: la verificación se hace **forzando la petición**, que es lo que la fuente exige.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-09 |
| CU cubiertos | CU-09 FA-03 |
| Restricciones transversales que la alcanzan | RT-03, RT-09 |
| Componente de `05` §3.1 | Superficies, Servicios de aplicación de front |
| Quién hace cumplir lo que esta historia sólo ofrece | `GeometriaFactory-Application` con el alcance del administrador, y `GeometriaFactory-Api`, contra cuya superficie se fuerza la petición |
| BT derivadas | BT-11, BT-13 |
| Tests previstos en 08 | Paso del guion de la etapa `h` **más** la prueba forzada contra la superficie, sobre un trabajo en `Pendiente` |

## 5. Prioridad y estimación

`Must` por derivar de `F-24`, `Must Have`, y porque el criterio de transición `h` → `i…` exige que el administrador elimine un trabajo en estado `Pendiente` y el trabajo desaparezca.

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

**Es la única regla del producto con un criterio de verificación que exige forzar la petición contra la superficie del servicio de datos**, y así lo declara `GeometriaFactory-Api`. Esta historia y US-16 son sus dos mitades: el alcance del alumno y el del administrador.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Numera y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 había repartido por necesidad de negocio con este identificador, y que su §3.2 dejó a la categoría 06 para redactar. |
