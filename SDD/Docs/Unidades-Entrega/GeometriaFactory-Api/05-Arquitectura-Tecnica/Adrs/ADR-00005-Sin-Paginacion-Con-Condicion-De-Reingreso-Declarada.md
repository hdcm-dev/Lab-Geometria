# ADR-00005 — Sin paginación, con su condición de reingreso declarada

**Proyecto de código:** GeometriaFactory-Api
**Documento:** ADR-00005-Sin-Paginacion-Con-Condicion-De-Reingreso-Declarada.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Comunicación

---

## 1. Contexto

Esta superficie tiene **tres** puntos de lectura de colección: el listado de las cuentas de la comisión, el listado de trabajos —con el alcance que el papel determina— y, dentro de este último, el caso del administrador, que es la pantalla más pesada del producto porque ve los trabajos de toda la comisión menos los borradores.

La guía de esta categoría exige registrar una decisión de paginación para el tipo `rest-api`. Ninguna fuente del producto la declara: ni el intake, ni la superficie de la categoría 02, ni el ensamblado de contratos, que declara una proyección de listado y **no un tipo de página**. Y hay un requerimiento numérico que sí está declarado y que es el que la decisión tiene que sostener: **percentil 99 del listado por debajo de 500 ms, medido en el servidor**.

Lo que hace que la pregunta tenga respuesta es lo que ya está decidido aguas arriba: **la proyección de listado no arrastra el texto original ni los componentes de las piezas**, y ésa —y no la cantidad de filas— es la razón por la que ese percentil es alcanzable. Un listado de una comisión con las piezas cargadas sería pesado con paginación o sin ella.

Motivación upstream: NB-00007, NB-00009; RN-00003, RN-00011; INV-02; `PRODUCT-INTAKE` §17.5.P.10, §17.4.P.10, §17.3.P.12.

## 2. Decisión

**Los tres puntos de lectura de colección devuelven el conjunto completo, ya acotado por el alcance que llega decidido, y no se pagina.** Cuatro precisiones:

1. **El recorte que existe es de alcance, no de tamaño.** El alumno ve lo suyo; el administrador ve la comisión **sin borradores**. Los dos recortes llegan decididos desde la capa de aplicación y esta superficie **no ofrece ningún parámetro** para ampliarlos: es la forma negativa en que `RN-00003` y `RN-00011` se sostienen acá.
2. **Lo que sostiene el requerimiento de tiempo es la proyección, no la cantidad.** El listado no lleva el texto original ni los componentes de las piezas, y esta capa **no recompone** lo que la proyección dejó afuera.
3. **Tampoco hay filtro ni orden como parámetro de la superficie.** La agrupación por alumno, el orden y el filtro tal como la persona los ejerce son decisiones de presentación de `GeometriaFactory-Web`, que recibe el conjunto y los aplica. Agregarlos acá duplicaría en la frontera algo que la pieza pública ya hace.
4. **La condición de reingreso está declarada y es medible**: **cuando la medición del percentil 99 del listado deje de cumplirse con el alcance real de una comisión**, entra paginación. No es una decisión de esta capa sola: obliga a un tipo de transferencia nuevo en el ensamblado de contratos y, por lo tanto, al despliegue conjunto de los dos extremos.

**Y tampoco hay límite de caudal**, por el mismo razonamiento: ninguna fuente lo declara, el caudal previsto es de **20 peticiones por minuto** —una comisión durante una clase— y agregarlo sería una decisión que nadie tomó, con un código de respuesta que esta superficie no usa.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Conjunto completo acotado por alcance, sin paginación, con condición de reingreso declarada (**adoptada**) | El consumidor recibe todo lo que puede ver y agrupa como quiera; no se agrega un tipo de transferencia que nadie pidió; la condición de reingreso es **medible** y no una opinión | Si la comisión crece mucho, la respuesta crece con ella hasta que la medición lo detecte |
| Paginación por desplazamiento y tamaño | Acota la respuesta y protege el percentil de entrada | **Descartada.** Ninguna fuente la declara; obliga a un tipo nuevo en un ensamblado que dos extremos compilan juntos; y complica la agrupación por alumno, que es **la forma en que el administrador usa la pantalla**: una página cortaría a un alumno por la mitad |
| Paginación por cursor | Estable ante inserciones y más eficiente en conjuntos grandes | **Descartada** por lo mismo, y además su complejidad no se paga: el conjunto es de una comisión y el orden lo decide el front |
| Límite duro de elementos devueltos, sin paginación | Protege el percentil con un solo número | **Descartada, y es la peor.** Un listado truncado en silencio le oculta trabajos al administrador sin decírselo, que es el mismo modo de falla que truncar el texto de un alumno: nada falla y el dato no está |
| Filtro y orden como parámetros de la superficie | Menos datos por la red y menos trabajo del front | **Descartada.** La agrupación y el filtro tal como la persona los ejerce son de `GeometriaFactory-Web`; ponerlos acá agrega superficie que hay que proteger y **abre la puerta por la que el administrador podría pedir borradores**, que es lo que `RN-00011` cierra |

## 5. Consecuencias positivas

1. `RN-00011` se sostiene por **ausencia de parámetro**: no hay forma de pedir borradores ajenos, porque no hay nada que pedir.
2. El ensamblado de contratos no crece con tipos que ninguna fuente pidió, y el despliegue conjunto no se dispara por una decisión de comodidad.
3. La agrupación por alumno —que es como el administrador usa la pantalla— no se rompe por un corte de página.
4. La condición de reingreso queda **atada a una medición ya declarada**, de modo que la revisión de la decisión no depende de que alguien se acuerde.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta que la respuesta del listado crezca con la comisión**, y que la protección sea una medición y no un tope.
2. **Se acepta que el front reciba el conjunto y haga el trabajo de agrupar, ordenar y filtrar.** Ya lo hace, y es donde está la persona.
3. **Se acepta no tener límite de caudal**, con la consecuencia de que un cliente que insista puede saturar al escritor único. Se acepta porque **el único cliente legítimo es la pieza pública** y el alcance es de aula.
4. **Se acepta que reponer la paginación sea caro** —tipo nuevo y despliegue conjunto—, y por eso la condición de reingreso está escrita en lugar de quedar implícita.

## 7. Implementación

- Los tres puntos de lectura de colección son `A-06`, `A-13` y —como caso del segundo— el del administrador, en [`../Arquitectura-Proyecto-Codigo.md`](../Arquitectura-Proyecto-Codigo.md) §3.4.
- **Convención impuesta:** ninguno de los tres declara parámetro de página, de tamaño, de orden ni de filtro.
- **Convención impuesta:** esta capa **no recompone** lo que la proyección de listado dejó afuera. Si una pantalla necesita el detalle, pide el detalle.
- La medición del percentil 99 vive en la batería de integración y es la que dispara la condición de reingreso de §2 punto 4.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Latencia del listado | **Percentil 99 por debajo de 500 ms**, medida en el servidor [ASUNCIÓN del intake] | Medición sobre el punto de listado con el alcance del administrador |
| Caudal sostenido | **20 peticiones por minuto** [ASUNCIÓN del intake] | Prueba de carga acotada |
| Parámetros de página, tamaño, orden o filtro en la superficie | Exactamente **0** | Inspección de los tres puntos de lectura de colección |
| Componentes de pieza o texto original en una respuesta de listado | Exactamente **0** | Prueba de integración que compara el cuerpo del listado contra el del detalle |
| Elementos omitidos en silencio de un listado | Exactamente **0** | Prueba que compara la cantidad devuelta contra la cantidad esperada del conjunto acotado |
| Parámetros con los que el administrador pueda pedir borradores | Exactamente **0** | Inspección de la superficie, y prueba que fuerza el pedido |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.17** §4.1 (RN-00003, RN-00011), §17.3.P.12, §17.4.P.10 y §17.5.P.10.
- [`../../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md`](../../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md) §3 y §4.
- [`../../../GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Adrs/ADR-08005-Proyeccion-De-Listado-Separada-Del-Detalle.md`](../../../../Producto/Adrs/ADR-08005-Proyeccion-De-Listado-Separada-Del-Detalle.md), que es la decisión que sostiene el requerimiento de tiempo.
- ADR relacionadas: [`ADR-00002`](ADR-00002-Formato-De-Intercambio-Y-Su-Configuracion.md), [`ADR-00008`](ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Registra la decisión de paginación que el tipo `rest-api` exige: **no se pagina**, porque lo que sostiene el requerimiento de tiempo es la proyección y no la cantidad, y porque paginar rompería la agrupación por alumno con la que el administrador usa la pantalla. Declara la condición de reingreso **medible** y la ausencia de límite de caudal con su fundamento. Evalúa cinco alternativas, declara cuatro trade-offs y fija seis métricas de validación. |
