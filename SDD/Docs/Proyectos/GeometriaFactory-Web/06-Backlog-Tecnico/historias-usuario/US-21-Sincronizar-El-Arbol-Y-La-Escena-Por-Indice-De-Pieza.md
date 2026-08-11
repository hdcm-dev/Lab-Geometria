# US-21 — Sincronizar el árbol y la escena por índice de pieza

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** US-21-Sincronizar-El-Arbol-Y-La-Escena-Por-Indice-De-Pieza.md
**Versión:** 1.0
**Estado:** Propuesta
**Fecha:** 2026-08-10
**Autor:** Scrum Master (AG-06)
**Épica:** EP-07 Visualización del trabajo
**Etapa del producto:** `g`
**Superficie de 03:** `Vista-De-Trabajo`
**Prioridad MoSCoW:** **Should**
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **persona que abre un trabajo**, quiero **que al tocar una pieza en el árbol se resalte en la escena y al revés**, para **saber qué figura del texto corresponde a qué figura del dibujo**.

## 2. Contexto

`F-13` del intake §4 declara `Should Have` la sincronización árbol ⇄ escena por índice de pieza. El caso de uso es [`CU-07`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-07-Abrir-Un-Trabajo-Y-Explorarlo-En-Escena-Y-Arbol.md). **El índice es la identidad de la pieza**, porque el texto no trae identificador.

## 3. Criterios de aceptación

- Given una escena con piezas dibujadas y su árbol, When la persona toca una pieza en el árbol, Then esa pieza queda resaltada **en exclusiva** en la escena.
- Given ese mismo estado, When toca una pieza en la escena, Then el árbol señala la misma pieza: la sincronización va **en los dos sentidos**.
- Given un índice que no corresponde a ninguna pieza dibujada, When se lo selecciona, Then la escena **queda como estaba** y la superficie informa la condición sin romperse.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-06 |
| CU cubiertos | CU-07 |
| Restricciones transversales que la alcanzan | RT-04, RT-10 |
| Componente de `05` §3.1 | Anfitrión del visor, Superficies |
| Quién hace cumplir lo que esta historia sólo ofrece | El resaltado lo ejerce la fachada del bundle; la presentación del árbol es de esta pieza |
| BT derivadas | BT-16, BT-17, BT-18 |
| Tests previstos en 08 | Puerta técnica `PT-02`, en su parte de sincronización por índice |

## 5. Prioridad y estimación

**`Should`, y es la única de las treinta.** Su capacidad de origen, `F-13`, es la **única `Should Have`** que toca a este proyecto de código.

**Y hay una tensión que corresponde declarar en lugar de taparla**: esta historia está **dentro de lo que `PT-02` mide antes de comprometer la etapa `g`** —`PRODUCT-INTAKE` §17.7.P.8 nombra la sincronización por índice entre lo que la puerta verifica— y además es criterio de la transición `g` → `h` del roadmap §5.2. En la práctica **no es diferible**, aunque su prioridad declarada lo admita. **No se le sube la prioridad**, porque eso sería reprioritizar una capacidad del Product Owner: la tensión está elevada como `PA-02` en [`../Product-Backlog.md`](../Product-Backlog.md) §6.

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

**`GeometriaFactory-Visor` elevó la misma tensión desde el otro lado de la fachada**, sobre las dos historias `Should` de su backlog. Las dos elevaciones apuntan al mismo punto y esperan la misma decisión del Product Owner sobre `F-13`.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Numera y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 había repartido por necesidad de negocio con este identificador, y que su §3.2 dejó a la categoría 06 para redactar. |
