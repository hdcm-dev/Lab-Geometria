# US-21 — Sincronizar el árbol y la escena por índice de pieza

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** US-21-Sincronizar-El-Arbol-Y-La-Escena-Por-Indice-De-Pieza.md
**Versión:** 1.1
**Estado:** Propuesta
**Fecha:** 2026-08-11
**Autor:** Scrum Master (AG-06)
**Épica:** EP-07 Visualización del trabajo
**Etapa del producto:** `g`
**Superficie de 03:** `Vista-De-Trabajo`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **persona que abre un trabajo**, quiero **que al tocar una pieza en el árbol se resalte en la escena y al revés**, para **saber qué figura del texto corresponde a qué figura del dibujo**.

## 2. Contexto

`F-13` del intake §4 declara la sincronización árbol ⇄ escena por índice de pieza, y **desde `PRODUCT-INTAKE` 1.19 la declara `Must Have`**. El caso de uso es [`CU-07`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-07-Abrir-Un-Trabajo-Y-Explorarlo-En-Escena-Y-Arbol.md). **El índice es la identidad de la pieza**, porque el texto no trae identificador.

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

**`Must`, como las treinta.** Su capacidad de origen, `F-13`, es `Must Have` en `PRODUCT-INTAKE` §4 desde la versión **1.19** de esa fuente. Era la única `Should` de este backlog hasta el 2026-08-10.

**Cómo se resolvió la tensión que esta historia había elevado.** La historia está **dentro de lo que `PT-02` mide antes de comprometer la etapa `g`** —`PRODUCT-INTAKE` §17.7.P.8 nombra la sincronización por índice entre lo que la puerta verifica— y además es criterio de la transición `g` → `h` del roadmap §5.2, de modo que en la práctica **no era diferible** aunque su prioridad declarada lo admitiera. Este backlog **no le subió la prioridad por su cuenta**, porque eso habría sido reprioritizar una capacidad del Product Owner: elevó la tensión como `PA-02` en [`../Product-Backlog.md`](../Product-Backlog.md) §6. **El Product Owner la resolvió el 2026-08-10 promoviendo la capacidad**, con ese mismo fundamento, y `PA-02` quedó cerrado.

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

**`GeometriaFactory-Visor` elevó la misma tensión desde el otro lado de la fachada**, sobre las dos historias `Should` de su backlog —US-08 y US-09—, como `PA-06`. Las dos elevaciones apuntaban al mismo punto y **una sola decisión las cerró**: la promoción de `F-13` a `Must Have` del 2026-08-10. Las tres historias pasaron a `Must` por esa vía, y ninguno de los dos backlogs repriorizó por su cuenta.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Numera y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 había repartido por necesidad de negocio con este identificador, y que su §3.2 dejó a la categoría 06 para redactar. |
| 1.1 | 2026-08-11 | **Absorbe la promoción de `F-13` a `Must Have`**, decidida por el Product Owner y registrada en `PRODUCT-INTAKE` **1.19** §4. La cabecera pasa de `Should` a **`Must`**; **§2** deja de atribuirle `Should Have` a la capacidad; **§5** reescribe la justificación de prioridad y la tensión, que dejó de estar abierta y pasa a tener desenlace; **§7** recoge que la misma decisión cerró las dos elevaciones, la de este proyecto de código y la de `GeometriaFactory-Visor`. Ningún criterio de aceptación, ninguna trazabilidad y ningún ítem de la verificación de entrada cambia: la historia se especificó completa desde el principio, precisamente porque `PT-02` ya la medía. Sube minor. |
