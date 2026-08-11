# ADR-02 — Sin estado propio y sin persistencia, y por qué se omite el modelo de datos lógico

**Proyecto de código:** GeometriaFactory-Web
**Documento:** ADR-02-Sin-Estado-Propio-Y-Sin-Persistencia.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior (AG-05)
**Categoría:** Persistencia

---

## 1. Contexto

La regla de esta categoría marca el modelo de datos lógico **obligatorio** para el tipo `web-monolith`, y la categoría 02 de este proyecto de código declaró en su §9 que se omite igual, **como decisión técnica declarada**, y que «corresponde una ADR en 05-Arquitectura-Tecnica que la registre». Ésta es esa ADR.

El fundamento no es que no haga falta modelar: es que **este proyecto de código no guarda nada, y no guardar es el punto**. El hosting gratuito donde vive **resetea el estado persistente**: es exactamente el problema que la topología del producto evita, poniendo el dato del otro lado. Un almacén del lado del front sería un segundo lugar donde vive el dato del producto, con la pregunta abierta de qué pasa cuando las dos copias difieren, y encima sobre un soporte que la fuente declara que se resetea.

Hay una segunda consecuencia, menos obvia y que conviene decidir junto con la primera: **si no hay copia local, la ausencia de datos y el fallo tienen que distinguirse por otra vía**. Un listado vacío y un listado que no se pudo traer se ven igual si lo único que se mira es el conteo.

Motivación upstream: NB-03, NB-07, NB-08; `PRODUCT-INTAKE` §7 (`CL-2`, `CL-8`), §17.6.P.4, §17.6.P.10 (`PT-01.c`) y §17.6.P.12; `PRODUCT-MANIFEST` §5 (`tiene_persistencia` == false); restricciones transversales `RT-06`, `RT-07` y `RT-08` de la categoría 02.

## 2. Decisión

**Este proyecto de código no guarda estado propio: ni base de datos, ni copia local, ni caché, ni réplica de los datos del producto.** Cuando el servicio de datos no responde, **no hay nada que mostrar** y se declara el estado degradado.

En consecuencia, **`Modelo-Datos-Logico.md` se omite**, y la omisión se registra acá y no en el catálogo de artefactos omitidos: no es la omisión que la regla admite para el tipo D8, es una **decisión técnica** que contradice el valor por defecto de la regla y por eso lleva ADR.

Y la consecuencia que la primera obliga: **el listado vacío se distingue del fallo por el tipo recibido y no por el conteo.** Cero elementos es un dato; ausencia de respuesta es otra cosa, y las dos se presentan distinto.

**Lo único que vive del lado del front es el estado del circuito**, en memoria del servidor del hosting, mientras la sesión dura.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Sin estado propio, con el estado degradado como respuesta (**adoptada**) | El dato del producto vive en un solo lugar; no hay coherencia que mantener; el reciclado del proceso no corrompe nada porque no hay nada que corromper | Cuando el servicio de datos no está, la aplicación no puede mostrar ni lo último visto |
| Caché de listados en el servidor del front | Mostraría lo último conocido durante una caída y aliviaría el ida y vuelta | Convierte a la pieza pública en un **segundo lugar donde vive el dato**, que es lo que la topología evita; abre la pregunta de qué se muestra cuando la caché y el servicio difieren; y **no sobreviviría al reciclado**, que es el escenario que más se querría cubrir. **Descartada por esta categoría** |
| Almacenamiento en el navegador para lo ya visto | Sobreviviría al reciclado del proceso, que la caché del servidor no cubre | Pondría datos de otras personas al alcance de cualquier guion del navegador, en un producto donde ni siquiera la credencial llega ahí; y contradice el mismo criterio con el que el bundle del visor tiene **cero** escrituras de almacenamiento. **Descartada por esta categoría** |
| Base de datos propia del front | Permitiría funcionar desconectado | El hosting **resetea el estado persistente**: la base se perdería, y con ella la confianza en lo que muestra. Es literalmente el problema que la partición del producto vino a resolver. **Descartada por `PRODUCT-INTAKE` §17.6.P.4** |

## 5. Consecuencias positivas

1. Hay **un solo lugar** donde vive el dato del producto, de modo que no existe la clase de defecto en que dos copias difieren y la persona ve la vieja.
2. El reciclado del proceso del hosting no corrompe ni pierde nada del producto: pierde la sesión, que es recuperable entrando de nuevo.
3. El estado degradado se puede diseñar como **superficie** y no como error, porque es una respuesta prevista y no una excepción ([`ADR-05`](ADR-05-Estado-Degradado-Como-Superficie.md)).
4. La omisión del modelo lógico queda registrada con fundamento, y no como un hueco que una auditoría tenga que interpretar.
5. El texto original del trabajo **no se guarda ni se reescribe** en ningún punto del recorrido, lo que hace que `RN-08` no tenga acá ningún lugar donde romperse.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta que durante una caída del servicio de datos no se pueda mostrar ni lo último visto.** Es deliberado: mostrar datos viejos sin poder decir de cuándo son sería peor que decir que ahora no están.
2. **Se acepta que el reciclado del proceso corte la sesión**, y que eso no tenga mitigación en el código (`R-06`, medido por `PT-01.c`). Lo que hay es tratamiento diseñado: el estado «sesión no restablecible».
3. **Se acepta que cada apertura de una superficie vuelva a pedir sus datos**, con el ida y vuelta que eso implica. La contrapartida está en el diseño de la espera de 03, no en una caché.
4. **Se acepta contradecir el valor por defecto de la regla de la categoría**, que marca el modelo lógico obligatorio para este tipo D8. La contradicción es explícita y esta ADR es su registro; el modelo lógico del producto le corresponde a `GeometriaFactory-Infrastructure`.

## 7. Implementación

- Los **servicios de aplicación de front** de [`../Arquitectura-Proyecto-Codigo.md`](../Arquitectura-Proyecto-Codigo.md) §3.1 piden los datos al abrir cada superficie y no los conservan entre superficies.
- **El tipo recibido decide el estado a mostrar**: colección con cero elementos produce el estado vacío, con su ilustración y su acción siguiente; ausencia de respuesta produce el estado indisponible. Nunca se decide por el conteo.
- **Nunca una tabla vacía mientras carga**: el esqueleto por fila es lo que impide que la espera se confunda con el estado vacío.
- El texto original del trabajo se entrega tal como la persona lo pegó, en el envío y en la presentación, **sin reescribir un carácter**.
- La única memoria del lado del front es el estado del circuito, y lo que guarda está acotado por [`ADR-03`](ADR-03-Credencial-De-Sesion-En-El-Estado-Del-Circuito.md).

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Almacenes de datos propios del front | Exactamente **0**: sin base, sin caché, sin réplica | Inspección del árbol de fuentes y de la configuración de la unidad desplegable |
| Claves escritas en el almacenamiento del navegador | Exactamente **0** por esta pieza | Inspección del almacenamiento tras un recorrido completo |
| Superficies que distinguen vacío de fallo por el conteo | Exactamente **0**: **11 de 11** lo distinguen por el tipo recibido | Inspección de las superficies con colección, y recorrido con el servicio de datos apagado |
| `PT-01.c` · estabilidad del proceso | **20 minutos** de navegación continua sin reciclado, y reconexión funcional al cortar y restablecer la red | Recorrido cronometrado en la etapa `a` |
| Caracteres del texto original alterados en el recorrido | Exactamente **0** | Prueba de ida y vuelta con el texto del escenario semilla, comparando carácter por carácter |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.16** §7 (`CL-2`, `CL-8`), §17.6.P.4, §17.6.P.10 y §17.6.P.12.
- `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` **1.2** §5, `tiene_persistencia` == false.
- [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §6 (`RT-06`, `RT-07`, `RT-08`) y §9, que pide esta ADR por su nombre.
- [`../../03-UX-UI-DX/Experiencia-De-Uso.md`](../../03-UX-UI-DX/Experiencia-De-Uso.md) §4.1 y §7.
- ADR relacionadas: [`ADR-03`](ADR-03-Credencial-De-Sesion-En-El-Estado-Del-Circuito.md), [`ADR-05`](ADR-05-Estado-Degradado-Como-Superficie.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Registra la ausencia deliberada de estado propio y de persistencia, y con ella la **omisión del modelo de datos lógico como decisión técnica** que contradice el valor por defecto de la regla para el tipo `web-monolith`, tal como la categoría 02 §9 lo pidió. Evalúa cuatro alternativas —tres descartadas por esta categoría y una por el intake—, declara cuatro trade-offs, fija la distinción de vacío contra fallo por el tipo recibido y cinco métricas de validación. |
