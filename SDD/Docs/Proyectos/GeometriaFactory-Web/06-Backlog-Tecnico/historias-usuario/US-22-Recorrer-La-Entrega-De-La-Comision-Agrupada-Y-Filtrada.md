# US-22 — Recorrer la entrega de la comisión agrupada y filtrada por alumno

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** US-22-Recorrer-La-Entrega-De-La-Comision-Agrupada-Y-Filtrada.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master (AG-06)
**Épica:** EP-05 Gestión del trabajo
**Etapa del producto:** `e`
**Superficie de 03:** `Listado-De-La-Comision`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **administrador**, quiero **ver los trabajos de la comisión agrupados y filtrados por alumno**, para **revisar la entrega de una sola vez sin pedirle nada a nadie**.

## 2. Contexto

`NB-07` pide revisión desde un solo lugar y `F-12` del intake §4 lo declara `Must Have`, **con agrupación y filtro por alumno**. El caso de uso es [`CU-08`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-08-Recorrer-La-Entrega-De-La-Comision.md). `02` §4.1 declara que **la agrupación, el orden y el filtro tal como la persona los ejerce son decisiones de presentación de esta pieza**.

## 3. Criterios de aceptación

- Given trabajos de varios alumnos, When el administrador abre el listado de la comisión, Then los ve **agrupados por alumno** y puede filtrar por uno.
- Given un filtro aplicado sobre un alumno sin trabajos visibles, When se muestra el resultado, Then la superficie declara el vacío **como vacío** y no como fallo.
- Given el listado, When se busca en él el texto original o el comentario, Then **no están**: usa la proyección y no el detalle.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-07, NB-09 |
| CU cubiertos | CU-08 |
| Restricciones transversales que la alcanzan | RT-06, RT-07, RT-09 |
| Componente de `05` §3.1 | Superficies, Representaciones reutilizadas, Servicios de aplicación de front |
| Quién hace cumplir lo que esta historia sólo ofrece | El alcance lo decide el dominio y el listado llega **ya acotado** |
| BT derivadas | BT-09, BT-11, BT-22 |
| Tests previstos en 08 | Paso del guion de la etapa `e`, y filas de la matriz de deriva sobre esta superficie |

## 5. Prioridad y estimación

`Must` por derivar de `F-12`, `Must Have`, y porque el criterio de transición `e` → `f` exige que el administrador vea los trabajos **agrupados y filtrados por alumno**.

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

**El diseño de esta superficie supone decenas de trabajos y no cientos, y por eso no incorpora paginación.** El volumen de la comisión está rotulado **[A VERIFICAR]** y elevado como `PA-08` de [`../Product-Backlog.md`](../Product-Backlog.md) §6, con BT-22; si resultara mucho mayor, la superficie afectada es ésta y el cambio es acotado.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Numera y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 había repartido por necesidad de negocio con este identificador, y que su §3.2 dejó a la categoría 06 para redactar. |
