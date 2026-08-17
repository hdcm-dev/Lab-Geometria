# ADR-10006 — El visor se opera sólo por sus seis funciones, y es esta pieza la que consulta el entorno

**Unidad de entrega:** GeometriaFactory-Web
**Documento:** ADR-10006-Aislamiento-Del-Visor-Tras-Su-Fachada.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior (AG-05)
**Categoría:** Comunicación

---

## 1. Contexto

Este proyecto de código es el **anfitrión del bundle del visor**: lo recibe como recurso estático generado, lo sirve al navegador y lo invoca por interoperabilidad. La Fase C de `GeometriaFactory-Visor` declaró que su superficie son **seis funciones planas** y que **el componente anfitrión —capa 1 de su arquitectura de tres capas— vive acá**. Es decir: una de las capas de aquella arquitectura es un componente de ésta.

`RA-02` exige que el bundle sea un **visualizador puro**: sin red, sin persistencia, sin configuración propia y sin identidad. Esa pureza es una propiedad suya, pero **es esta pieza la que la hace posible**. El caso concreto es la preferencia de movimiento reducido del navegador: alguien tiene que consultarla, y si la consultara el bundle dejaría de ser un visualizador sin configuración. La Fase C del visor lo dejó escrito: el anfitrión pasa **dos valores de verdad** y el bundle no consulta nada.

Hay además una obligación de ciclo de vida que la fuente marca como **no opcional**: la liberación de la instancia se invoca al descartar el componente que la aloja, porque sin eso navegar entre trabajos acumula contextos gráficos en el navegador. Es lo que la puerta técnica `PT-02` mide con diez recorridos de ida y vuelta.

Motivación upstream: NB-00006, NB-00004, NB-00005; `RA-01`, `RA-02`; `PRODUCT-INTAKE` §17.2.P.3 · GeometriaFactory-Web (regla de aislamiento del visor), §17.2.P.10 · GeometriaFactory-Web, §17.2.P.11 · GeometriaFactory-Web punto 5, §17.7 P.3 y P.10; restricciones transversales `RT-04`, `RT-05`, `RT-10`, `RT-11` y `RT-13` de la categoría 02.

## 2. Decisión

**El bundle se consume exclusivamente por las seis funciones de su fachada, y sólo desde el componente anfitrión del visor.** Ningún otro componente accede a su interior, y **ninguno manipula el elemento de dibujo por su cuenta**.

**Es esta pieza la que consulta el entorno del navegador.** Lee la preferencia de movimiento reducido, conserva la elección de la persona y manda **dos valores de verdad** por la función de gobierno del movimiento —uno por cada movimiento automático—. El bundle no consulta nada y no conserva nada.

**La liberación de la instancia se invoca al descartar el componente que la aloja, y no es opcional.**

Y dos consecuencias que las superficies no pueden invertir:

- **El texto del trabajo viaja del servidor al navegador una sola vez por trabajo**, y ni la escena ni el árbol se vuelven a componer desde el servidor.
- **Las piezas que la fachada no dibuja se enumeran junto a la escena, y no son observaciones del trabajo.** Quien decide si el trabajo verifica es el servicio de datos.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Aislamiento total tras las seis funciones, con el entorno consultado por esta pieza (**adoptada**) | El motor de dibujo queda reemplazable, que es el punto de extensión declarado del producto; `RA-02` se sostiene sin que el bundle tenga que abstenerse de nada que necesite | Cuando una superficie necesita algo que las seis funciones no dan, hay que ampliar la fachada en el otro proyecto de código |
| Invocar funciones internas del bundle cuando la fachada no alcanza | Resuelve de inmediato lo que falte, sin coordinar con otro proyecto de código | Ata las superficies a los nombres internos del motor de dibujo y lo vuelve irreemplazable, que es lo contrario del punto de extensión que el producto declara. **Descartada por `PRODUCT-INTAKE` §17.2.P.3 · GeometriaFactory-Web** |
| Que el bundle consulte la preferencia de movimiento reducido por su cuenta | Un ida y vuelta menos, y la preferencia se respetaría aunque el anfitrión se olvide | **Rompe `RA-02`**: el bundle pasaría a leer configuración del entorno. Y haría que la prueba de cero red midiera el caso fácil, porque un entorno de prueba que declara movimiento reducido dejaría el bucle apagado. **Descartada por la Fase C de `GeometriaFactory-Visor`** |
| Reconstruir la instancia para prender o apagar un movimiento | No haría falta la sexta función de la fachada | Perdería la selección de pieza vigente y volvería a cargar el texto, que ya viajó una sola vez. La sexta función existe precisamente para gobernar **sobre una instancia viva**. **Descartada por `PRODUCT-INTAKE` §17.7 P.3** |

## 5. Consecuencias positivas

1. El motor de dibujo sigue siendo reemplazable sin tocar ninguna superficie, que es el punto de extensión declarado del producto.
2. `RA-02` se sostiene desde este lado: el bundle no necesita consultar nada porque el anfitrión ya le manda lo que necesita.
3. `RA-01` se refuerza por el mismo camino: el único guion del navegador del producto es un bundle que no hace red, y esta pieza no le agrega otro.
4. La interacción con la escena no genera tráfico de circuito, que es el único lugar del producto con respuesta inmediata.
5. Prender o apagar un movimiento **no pierde la selección de pieza**, porque la instancia no se reconstruye.
6. Sólo **dos** de las once superficies tocan el bundle, lo que hace que este aislamiento sea barato de sostener y fácil de auditar.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta que ampliar la fachada exija coordinar con otro proyecto de código.** Cuando a una superficie le falte algo, el camino está declarado en [`Visor Extensibilidad.md`](../Extensibilidad.md) §5, y no es invocar el interior.
2. **Se acepta que la preferencia de movimiento reducido se respete sólo si esta pieza la consulta.** Es una obligación de acá, y por eso tiene métrica propia: si el anfitrión se olvida, el bundle no lo compensa.
3. **Se acepta que la liberación sea responsabilidad de un componente que la persona no ve.** Es la clase de omisión que no falla la primera vez, y por eso `PT-02` la mide con diez recorridos y no con uno.
4. **Se acepta enumerar las piezas no dibujadas en un lugar distinto del de las observaciones**, aunque las dos hablen de piezas que algo tuvo con ellas. Mezclarlas haría que el alumno leyera un problema de dibujo como un error de su trabajo.

## 7. Implementación

- El componente **Anfitrión del visor** de [`../Arquitectura-Proyecto-Codigo.md`](../Arquitectura-Proyecto-Codigo.md) §3.1 es el único que invoca la fachada, y es la **capa 1** del contrato de fachada de `GeometriaFactory-Visor`.
- Las **dos** superficies que lo consumen son `Envio-De-Trabajo`, en la previsualización previa al envío, y `Vista-De-Trabajo`, que es la única que usa las seis funciones.
- **El anfitrión opera el ciclo de vida completo**: crear la instancia después de que el elemento de dibujo tiene tamaño, cargar el texto una sola vez, resaltar por índice, recalcular la relación de aspecto cuando corresponde, gobernar los dos movimientos y liberar al descartar.
- **La fachada no observa tamaños y no decide cuándo ajustar**: el recálculo lo invoca el anfitrión.
- La **previsualización dibuja y no verifica**, y la superficie lo declara: las piezas que no se dibujan **no son** errores del trabajo.
- El bundle llega como **artefacto generado** al directorio de recursos estáticos y **nunca se edita a mano**.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Invocaciones al interior del bundle | Exactamente **0**: **6 de 6** funciones de la fachada son la única vía | Inspección del árbol de fuentes |
| Componentes que manipulan el elemento de dibujo por fuera del anfitrión | Exactamente **0** | Inspección del árbol de fuentes |
| Instancias no liberadas tras **10** recorridos de ida y vuelta entre trabajos | Exactamente **0**, sin degradación | Puerta técnica `PT-02`, medida **con los dos movimientos prendidos**, que es su peor caso |
| Tráfico de circuito durante la interacción con la escena | Exactamente **0** | Conteo en la pestaña de red mientras se rota y se acerca |
| Veces que el texto del trabajo viaja del servidor al navegador | Exactamente **1** por trabajo | Conteo en la pestaña de red durante una sesión de exploración |
| Consultas del bundle al entorno del navegador | Exactamente **0**: la preferencia de movimiento reducido la lee **esta** pieza y la manda como **2** valores de verdad | Inspección conjunta del anfitrión y del bundle generado |
| Selecciones de pieza perdidas al prender o apagar un movimiento | Exactamente **0** | Recorrido que selecciona una pieza y luego cambia cada movimiento |
| Superficies que enumeran las piezas no dibujadas dentro de la lista de observaciones | Exactamente **0** de **2** | Inspección de las dos superficies que consumen el visor |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.16** §14 (`RA-01`, `RA-02`), §17.2.P.3 · GeometriaFactory-Web, §17.2.P.10 · GeometriaFactory-Web, §17.2.P.11 · GeometriaFactory-Web punto 5, §17.7 P.3 y §17.7 P.10.
- [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §6 (`RT-04`, `RT-05`, `RT-10`, `RT-11`, `RT-13`) y §7, con sus tres consecuencias.
- [`../../../GeometriaFactory-Visor/05-Arquitectura-Tecnica/Adrs/ADR-12002-Superficie-De-Seis-Funciones-Planas.md`](ADR-12002-Superficie-De-Seis-Funciones-Planas.md) y [`ADR-12003`](ADR-12003-Visualizador-Puro-Sin-Red-Ni-Identidad.md).
- [`../../../GeometriaFactory-Visor/05-Arquitectura-Tecnica/Extensibilidad.md`](../Extensibilidad.md) §5, el procedimiento para cuando al anfitrión le falta algo.
- ADR relacionadas: [`ADR-10001`](ADR-10001-Render-En-El-Servidor-Con-Circuito-Interactivo.md), [`ADR-10004`](ADR-10004-Tres-Capas-De-Presentacion.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Registra el aislamiento del bundle tras sus seis funciones y la obligación, propia de esta pieza, de consultar el entorno del navegador y mandarle dos valores de verdad, que es lo que hace posible `RA-02` desde este lado. Declara la liberación de la instancia como no opcional, evalúa cuatro alternativas, declara cuatro trade-offs y fija ocho métricas de validación. |
