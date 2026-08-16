# ADR-12005 — Disposición determinista derivada del índice, y el determinismo es de posición y no de orientación

**Proyecto de código:** GeometriaFactory-Visor
**Documento:** ADR-12005-Disposicion-Determinista-Derivada-Del-Indice.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Estilo

---

## 1. Contexto

El visualizador previo ubica las piezas con un **ordenamiento aleatorio**, de modo que dos previsualizaciones del mismo trabajo se ven distintas. El intake decide reemplazarlo por **posición derivada del índice** (`PRODUCT-INTAKE` §17.2.P.2 · GeometriaFactory-Visor y §17.2.P.11 · GeometriaFactory-Visor punto 5), y declara la disposición determinista como uno de los requerimientos no funcionales del proyecto de código: procesar el mismo trabajo dos veces produce la misma disposición.

Hay dos hechos del producto que atan la decisión más de lo que parece a primera vista:

- **La identidad de una pieza es su índice en el conjunto raíz**, porque el dato del alumno no trae identificador propio. Ese índice es lo que permite seleccionarla y resaltarla, y es lo que sincroniza el árbol con la escena.
- **La posición es la del texto y no la del conjunto de piezas dibujadas.** Una figura que no se pudo reconstruir deja su posición reservada y ninguna pieza se renumera. Sin eso, un error informado sobre la figura de índice 1 no coincidiría con lo que la persona ve.

Y hay una tensión que la capacidad **F-25** introdujo el 2026-08-09: con el giro de las figuras prendido, dos personas que miran el mismo trabajo **no ven la misma orientación**.

Motivación upstream: NB-00006, y NB-00004 en su parte de piezas dibujadas; capacidades F-13 y F-25; garantía G-6.

## 2. Decisión

**La ubicación de cada pieza en la escena se deriva de su índice en el conjunto raíz del trabajo.** No hay ordenamiento aleatorio ni disposición dependiente del momento de carga.

**El determinismo que este proyecto de código garantiza es de la posición, no de la orientación en un instante.** El movimiento automático de la escena no lo afecta: dos personas que miran el mismo trabajo con el giro prendido ven la misma disposición aunque no vean la misma orientación.

**Al apagar el giro de las figuras, cada pieza vuelve a su orientación de partida.** Sin esa reposición, apagar el movimiento dejaría cada pieza donde el azar del tiempo la encontró, y dos personas que apagan el giro verían escenas distintas del mismo trabajo.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Posición derivada del índice (**adoptada**) | Dos procesados del mismo texto son comparables pieza por pieza; la sincronización con el árbol no necesita traducir identidades; se verifica comparando dos procesados | La disposición no se optimiza según el tamaño ni la forma de cada pieza: dos piezas muy distintas ocupan celdas equivalentes |
| Conservar el ordenamiento aleatorio del visualizador previo | Ninguno para este producto | Dos previsualizaciones del mismo trabajo se ven distintas, y quien compara se confunde. Es el defecto que el intake decide cerrar |
| Disposición optimizada por tamaño o por forma de cada pieza | Mejor aprovechamiento del espacio de la escena | Deja de ser derivable del índice: dos textos con las mismas figuras en distinto orden, o con una figura no reconstruida, producirían disposiciones incomparables |
| Renumerar las piezas dibujadas para que la disposición sea contigua | Escena sin huecos | **Desplazaría el índice que la persona ve** respecto del que el error informa. Contradice el escenario del intake donde el error se informa sobre la figura de índice 1 sin haberla reconstruido |
| Garantizar también el determinismo de la orientación | Dos personas verían exactamente lo mismo en todo momento | Sería incompatible con el giro automático de F-25, que es capacidad `Must Have`. La resolución adoptada acota el determinismo a la posición y **repone la orientación al apagar** |

## 5. Consecuencias positivas

1. **Dos procesados del mismo texto son comparables pieza por pieza**, que es lo que permite a quien mira dos previsualizaciones no confundirse.
2. La sincronización del árbol con la escena **no necesita traducir identidades**: el índice es el mismo de los dos lados.
3. Una figura no reconstruida **deja su posición reservada**, de modo que el índice que informa el error coincide con el que la persona ve.
4. La propiedad se verifica de forma barata: dos procesados y una comparación.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta una disposición que no optimiza el espacio.** Dos piezas de tamaños muy distintos ocupan celdas equivalentes.
2. **Se acepta que la escena tenga huecos** cuando una figura no se pudo reconstruir. Es deliberado: renumerar desplazaría el índice que la persona ve.
3. **Se acepta que el determinismo no alcance a la orientación**, y que eso haya que declararlo en cada lugar donde se afirma el determinismo, para que 08 no escriba una prueba que compare orientaciones.
4. **Se acepta el costo de reponer la orientación de partida al apagar el giro**, que obliga al servicio de dibujo a recordar la orientación inicial de cada pieza.

## 7. Implementación

- La posición de cada pieza se calcula a partir de su índice, en el servicio de dibujo.
- **La posición es la del texto**: el conjunto de piezas dibujadas admite huecos y ninguna pieza se renumera.
- El giro de las figuras rota cada pieza **sobre su eje vertical, en su lugar**, sin moverla de la celda que le asignó su índice.
- **Al apagar el giro, cada pieza vuelve a su orientación de partida** (regla 5 del gobierno del movimiento).
- Los dos movimientos **se detienen mientras la persona arrastra la cámara** y mientras la superficie de dibujo no está visible, y esa detención **no cambia el estado gobernado**.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Disposición entre dos procesados del mismo texto | **Idéntica**, comparable pieza por pieza | Comparación de dos procesados; **se compara posición, no orientación** |
| Determinismo con los movimientos prendidos | La disposición **no cambia** respecto de los movimientos apagados | Comparación con las cuatro combinaciones de los dos movimientos |
| Orientación tras apagar el giro | **100 %** de las piezas en su orientación de partida | Comparación contra la escena antes de prender el giro |
| Renumeración de piezas ante una figura no reconstruida | Exactamente **0** piezas renumeradas | Escenario con una figura de tipo desconocido, comprobando que el índice informado coincide con el que la persona ve |
| Efecto de `establecerMovimiento` sobre la disposición y la selección | Exactamente **0** cambios en las dos | Prueba de la sexta función sobre una instancia cargada y con selección vigente |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` 1.15 §17.2.P.2 · GeometriaFactory-Visor, §17.2.P.10 · GeometriaFactory-Visor, §17.2.P.11 · GeometriaFactory-Visor punto 5, §4 (F-13, F-25) y §20 E-5.
- [`../../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md) §3.2 (G-6), §5.4 y §5.5.
- [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §6, fila de disposición determinista.
- ADR relacionadas: [`ADR-12002`](ADR-12002-Superficie-De-Seis-Funciones-Planas.md), [`ADR-12003`](ADR-12003-Visualizador-Puro-Sin-Red-Ni-Identidad.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Registra la disposición derivada del índice, la acotación del determinismo a la posición y no a la orientación, la reposición de la orientación de partida al apagar el giro, cinco alternativas evaluadas —incluida la renumeración de piezas dibujadas, descartada porque desplazaría el índice que la persona ve— y cinco métricas de validación. |
