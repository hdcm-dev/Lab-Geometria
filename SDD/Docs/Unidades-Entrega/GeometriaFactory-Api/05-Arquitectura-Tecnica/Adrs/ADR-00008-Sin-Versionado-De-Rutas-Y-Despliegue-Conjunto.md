# ADR-00008 — Sin versionado de rutas, con despliegue conjunto como regla operativa

**Unidad de entrega:** GeometriaFactory-Api
**Documento:** ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Despliegue

---

## 1. Contexto

Una superficie de servicio expuesta normalmente versiona sus rutas, porque tiene clientes que no controla. **Ésta no tiene ninguno.** El intake lo declara sin ambigüedad: el único consumidor es `GeometriaFactory-Web`, servidor a servidor, el navegador nunca la alcanza (`RA-01`), y **no hay versionado de rutas porque no hay clientes de terceros** (§17.1.P.3 · GeometriaFactory-Api).

Lo que sostiene esa ausencia es una propiedad de composición: los dos extremos **compilan contra el mismo ensamblado de tipos de transferencia**, de modo que un cambio incompatible **rompe la compilación antes de romper el tiempo de ejecución**. Es toda la red que este producto tiene, y `GeometriaFactory-Contracts` la declaró como su política: «los dos extremos se despliegan juntos ante un cambio de contrato».

Lo que esta ADR tiene que dejar dicho es **qué reemplaza al versionado**, porque una ausencia sin sustituto es un pendiente y no una decisión. Y hay un punto donde la red no alcanza y conviene nombrarlo: **la configuración de intercambio no rompe ninguna compilación** —por eso [`ADR-00002`](ADR-00002-Formato-De-Intercambio-Y-Su-Configuracion.md) la fija de un solo lado— y **el esquema del almacén tampoco se recompila**, porque sobrevive al despliegue.

Motivación upstream: NB-00008; `PRODUCT-INTAKE` §9 (X-9), §14 (RA-01), §17.1.P.3 · GeometriaFactory-Contracts, §17.1.P.3 · GeometriaFactory-Api, §17.1.P.7 · GeometriaFactory-Api, §17.1.P.8 · GeometriaFactory-Api.

## 2. Decisión

**No se versionan las rutas, y lo que lo reemplaza es el despliegue conjunto de las dos piezas desplegables ante todo cambio de contrato.** Cinco reglas:

1. **Una sola versión de la superficie vive a la vez.** No hay prefijo de versión en las rutas, no hay convivencia de dos formas de un punto y no hay deprecación gradual: no hay a quién dársela.
2. **Todo cambio del ensamblado de contratos obliga al despliegue conjunto** de esta pieza y de la pieza pública. Es la regla operativa que reemplaza al versionado, y ya está declarada por `GeometriaFactory-Contracts`.
3. **Tres clases de cambio no las detecta la compilación, y las tres tienen su propio mecanismo**: la **configuración de intercambio**, que se declara una sola vez para los dos extremos ([`ADR-00002`](ADR-00002-Formato-De-Intercambio-Y-Su-Configuracion.md)); el **esquema del almacén**, que se verifica al arrancar con su linaje y detiene el arranque si no cierra; y **las rutas**, que sólo el consumidor conoce y que la batería de integración ejerce contra el servicio real.
4. **Cada etapa cerrada y fusionada recibe una etiqueta**, para poder volver a cualquier demostración. La reversión es volver a la etiqueta anterior y reconstruir; el registro de cambios se actualiza **en la rama de la etapa, no después de fusionar**.
5. **La colección de peticiones reproducible es parte del contrato hacia afuera**, y no un accesorio: es la forma de demostración que el intake declara para este tipo de proyecto de código, y su obligación propia es **reproducirse en cinco pasos o menos y no inventar ningún dato de prueba**. Cuando la superficie cambia, la colección cambia con ella.

**Y una ausencia declarada que esta ADR sostiene: la pasarela de reenvío del front no se implementa.** El intake la declara **especificada y no implementada**, porque hoy ningún código del navegador toca esta superficie y la pasarela sólo consumiría el recurso más escaso del plan gratuito. Su condición de reingreso está escrita: descarga de archivos, carga directa desde el navegador o migración del front a ejecución en el navegador.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Sin versionado de rutas, con despliegue conjunto y tres mecanismos para lo que la compilación no detecta (**adoptada**) | Ninguna superficie duplicada que mantener; el fallo de contrato aparece en la construcción, que es lo más temprano posible; los tres huecos de la red están nombrados y cubiertos | Obliga a desplegar dos piezas juntas, con la ventana de indisponibilidad de la que ya está en el servidor propio |
| Versionado de rutas con prefijo | Permitiría desplegar los dos extremos en orden distinto y convivir dos versiones | **Descartada por el intake §17.1.P.3 · GeometriaFactory-Api**: no hay clientes de terceros. Duplicaría la superficie que hay que proteger con la guardia, que es exactamente donde este proyecto de código tiene su defecto característico |
| Versionado por negociación de contenido | No ensucia las rutas | **Descartada** por lo mismo, y con el agravante de que la elección de versión pasaría a ser una configuración del cliente, que es la clase de cosa que **no rompe ninguna compilación** |
| Despliegue independiente de las dos piezas, con tolerancia en la lectura | El front y el servicio se despliegan cuando cada uno esté listo | **Descartada.** La tolerancia en la lectura convierte un desajuste de versión en un dato perdido en silencio, que es lo que [`ADR-00002`](ADR-00002-Formato-De-Intercambio-Y-Su-Configuracion.md) descartó por escrito |
| Implementar la pasarela de reenvío ahora | Dejaría el camino listo si algún día el navegador necesita el backend | **Descartada por el intake §9 X-9**: está especificada y no implementada, y su condición de reingreso está declarada |

## 5. Consecuencias positivas

1. La superficie no se duplica, y la guardia de admisión sigue teniendo **una** lista de quince puntos que recorrer y no dos.
2. El fallo de contrato aparece en la construcción, que es el momento más barato y más visible del ciclo.
3. Los **tres** lugares donde la compilación no protege están nombrados, y cada uno tiene su mecanismo en lugar de quedar como riesgo difuso.
4. La colección de peticiones queda atada al contrato: si la superficie cambia y la colección no, la demostración de la etapa falla, que es la señal correcta.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta el despliegue conjunto**, con la ventana de indisponibilidad de la pieza que vive en el servidor propio.
2. **Se acepta no poder desplegar el front y el servicio en momentos distintos** ante un cambio de contrato.
3. **Se acepta que la reversión sea reconstruir desde una etiqueta**, sin imagen publicada a la que volver: el canal de entrega declarado construye **en destino desde el repositorio**, y ese mecanismo lleva marca **[A VERIFICAR]** de la fuente.
4. **Se acepta que un cambio de rutas no sea detectable por compilación**, y se compensa con la batería de integración, que ejerce el servicio real.

## 7. Implementación

- **Convención impuesta:** ninguna ruta lleva prefijo ni sufijo de versión, y ningún punto de acceso convive con una forma anterior de sí mismo.
- **Convención impuesta:** todo cambio del ensamblado de contratos entra con el despliegue de las dos piezas en la misma etapa.
- **Convención impuesta:** la colección de peticiones se actualiza en la misma intervención en que cambia la superficie, y **no inventa datos de prueba**: usa los escenarios del anexo del intake.
- El alcance de la colección lo adopta la categoría 02 —**los ocho** escenarios— como derivación declarada, sobre dos lugares de la fuente que no dicen lo mismo. Esta categoría **hereda esa lectura y no la reabre**.
- Versionado semántico y convenciones de mensaje de confirmación **sin excepciones**, una rama y un pedido de fusión por etapa, y una etiqueta por etapa cerrada.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Rutas con prefijo o sufijo de versión | Exactamente **0** | Inspección de los quince puntos |
| Formas conviviendo de un mismo punto de acceso | Exactamente **0** | Inspección de la superficie |
| Etapas cerradas sin etiqueta | Exactamente **0** | Inspección del historial |
| Pasos de la colección de peticiones reproducible | **5 o menos** | Ejecución en la demostración de etapa |
| Datos de prueba inventados en la colección | Exactamente **0** | Comparación contra los escenarios del anexo del intake |
| Cambios del ensamblado de contratos desplegados sin la pieza pública | Exactamente **0** | Revisión de cada etapa que toque el ensamblado |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.17** §9 (X-9), §14 (RA-01), §16.1, §17.1.P.3 · GeometriaFactory-Contracts, §17.1.P.3 · GeometriaFactory-Api, §17.1.P.7 · GeometriaFactory-Api y §17.1.P.8 · GeometriaFactory-Api.
- [`../../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md`](../../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md) §7, que declara las siete ausencias de la superficie con lo que las repone.
- [`../../02-Especificacion-Funcional/Casos-De-Uso/CU-00012-Ejercitar-La-Superficie-Con-La-Coleccion-De-Peticiones-Reproducible.md`](../../10-Examples/CU-00012-Ejercitar-La-Superficie-Con-La-Coleccion-De-Peticiones-Reproducible.md).
- [`../../../GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Adrs/ADR-08003-Versionado-Por-Compilacion-Compartida.md`](../../../../Producto/Adrs/ADR-08003-Versionado-Por-Compilacion-Compartida.md), que es la política que esta ADR aplica en la frontera.
- ADR relacionadas: [`ADR-00002`](ADR-00002-Formato-De-Intercambio-Y-Su-Configuracion.md), [`ADR-00005`](ADR-00005-Sin-Paginacion-Con-Condicion-De-Reingreso-Declarada.md), [`ADR-00007`](ADR-00007-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Registra la ausencia de versionado de rutas **con su sustituto** —el despliegue conjunto— y nombra los **tres** lugares donde la compilación compartida no protege, cada uno con su mecanismo: la configuración de intercambio, el esquema del almacén y las rutas. Sostiene la ausencia declarada de la pasarela de reenvío con su condición de reingreso. Evalúa cinco alternativas, declara cuatro trade-offs y fija seis métricas de validación. |
