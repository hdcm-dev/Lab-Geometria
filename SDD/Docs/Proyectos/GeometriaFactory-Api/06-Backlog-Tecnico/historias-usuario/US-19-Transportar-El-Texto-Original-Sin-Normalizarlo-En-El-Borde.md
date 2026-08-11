# US-19 — Transportar el texto original **sin normalizarlo en el borde**

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** US-19-Transportar-El-Texto-Original-Sin-Normalizarlo-En-El-Borde.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + API Product Owner (AG-06)
**Épica:** EP-04 Gestión del trabajo
**Etapa del producto:** `e`
**Punto de acceso:** `A-10` y `A-11`, bajo la guardia
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **producto**, quiero **que el texto del alumno cruce la frontera del proceso sin recodificarse, sin normalizarse y sin truncarse**, para **que lo que se guarda sea exactamente lo que su programa produjo**.

## 2. Contexto

`RN-08` conserva el texto íntegro, y `02` §6 declara el tramo propio de esta capa: **el borde del proceso es el primer lugar donde el texto puede alterarse** —por codificación, por normalización o por recorte de tamaño—. `05` §9 declara el riesgo con impacto alto: truncar **rompe `RN-08` en silencio**, el trabajo se guarda con el texto mutilado y el alumno lo descubre al ver el dibujo. El contrato de uso es [`CU-06`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-06-Exponer-El-Envio-Y-La-Eliminacion-De-Un-Trabajo.md).

## 3. Criterios de aceptación

- Given el texto del escenario `E-1`, When se lo envía y se lo guarda, Then la comparación **byte a byte** entre lo enviado y lo guardado da **0** caracteres de diferencia.
- Given un cuerpo por encima del límite configurado, When se lo envía, Then **se rechaza y no se trunca**: **0** truncamientos silenciosos.
- Given el texto, When se inspecciona el tramo de transporte, Then **no se recodifica, no se recortan espacios, no se normalizan saltos de línea y no se reescriben separadores**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-03, NB-04 |
| CU cubiertos | CU-06 |
| RN que ejerce | **RN-08**, en el punto donde esta capa puede romperla |
| Componente de `05` §3.1 | Superficie de trabajos |
| ¿Decide qué se dice? | **No.** El texto viaja como cadena |
| Familia empobrecida | **No** |
| BT derivadas | BT-08, BT-09, BT-18, BT-24 |
| Tests previstos en 08 | Prueba byte a byte con `E-1`, y prueba de cuerpo excedido que comprueba **rechazo y no truncamiento** |

## 5. Prioridad y estimación

`Must` por `RN-08`, y porque el criterio de transición `f` → `g` exige que el texto original **se conserve íntegro y nunca se reescriba**.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el punto de acceso que la realiza y el componente de `05` §3.1 que lo aloja
- [x] Declara si su punto está bajo la guardia, y si no lo está, cuál de las cuatro ausencias declaradas es
- [x] Toda condición que transporta es uno de los quince códigos vivos del contrato, con su destino declarado
- [x] Declara que no decide qué se dice
- [x] Declara si su respuesta pertenece a una de las tres familias deliberadamente empobrecidas

## 7. Notas y supuestos

**El valor del límite sigue abierto y la forma de rechazo no.** `GeometriaFactory-Infrastructure` decidió que el motor **no impone límite propio** y **exigió por escrito que el borde rechace y no trunque**; esta categoría fija la forma y deja el número en la etapa `a`, calibrado sobre el texto más grande que la fuente documenta. Es `PA-06` de [`../Product-Backlog.md`](../Product-Backlog.md) §6, con BT-09.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
