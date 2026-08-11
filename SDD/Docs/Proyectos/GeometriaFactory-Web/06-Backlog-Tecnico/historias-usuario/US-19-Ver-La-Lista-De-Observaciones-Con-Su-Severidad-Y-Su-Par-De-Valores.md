# US-19 — Ver la lista de observaciones con su severidad y su par de valores

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** US-19-Ver-La-Lista-De-Observaciones-Con-Su-Severidad-Y-Su-Par-De-Valores.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master (AG-06)
**Épica:** EP-07 Visualización del trabajo
**Etapa del producto:** `g`
**Superficie de 03:** `Vista-De-Trabajo`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **persona que abre un trabajo**, quiero **ver sus observaciones con su severidad, su ubicación y el par de valores cuando corresponde**, para **entender qué encontró el sistema sobre ese trabajo sin volver a enviarlo**.

## 2. Contexto

`NB-05` pide visibilidad del error de cálculo y `RN-09` exige la ubicación. El caso de uso es [`CU-07`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-07-Abrir-Un-Trabajo-Y-Explorarlo-En-Escena-Y-Arbol.md). La **lista de observaciones con el par declarado y derivado** es una de las **tres** representaciones reutilizadas que la categoría 03 declara.

## 3. Criterios de aceptación

- Given un trabajo con advertencias y errores, When se lo abre, Then cada observación aparece con su **severidad**, su **índice de figura y campo** y, en las advertencias, el **par declarado y derivado**.
- Given esa lista, When se busca en ella una pieza que la fachada no dibujó, Then **no está**: las piezas no dibujadas se enumeran aparte y **no son observaciones del trabajo**.
- Given la severidad, When se la comunica, Then usa **al menos dos canales** y nunca sólo color.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-05, NB-06, NB-04 |
| CU cubiertos | CU-07 |
| Restricciones transversales que la alcanzan | RT-03, RT-07 |
| Componente de `05` §3.1 | Representaciones reutilizadas, Superficies |
| Quién hace cumplir lo que esta historia sólo ofrece | La producción de la observación es de `GeometriaFactory-Infrastructure`; su transporte sin recortes, de `GeometriaFactory-Api` |
| BT derivadas | BT-09, BT-13 |
| Tests previstos en 08 | Filas de la matriz de sensado de deriva sobre la lista de observaciones |

## 5. Prioridad y estimación

`Must` por derivar de `F-11` y `F-10`, `Must Have`, y porque es donde `NB-05` se vuelve visible sobre el trabajo ya entregado.

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

**Que las piezas no dibujadas y las observaciones no se mezclen está declarado en los dos casos de uso que consumen la fachada** (`02` §7, consecuencia 2), precisamente porque son dos cosas que se parecen y tienen orígenes distintos.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Numera y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 había repartido por necesidad de negocio con este identificador, y que su §3.2 dejó a la categoría 06 para redactar. |
