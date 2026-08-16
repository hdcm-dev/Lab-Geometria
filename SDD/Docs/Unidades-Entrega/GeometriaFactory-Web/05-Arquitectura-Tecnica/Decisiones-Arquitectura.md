# Índice de decisiones de arquitectura — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** Decisiones-Arquitectura.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior (AG-05)

---

## 1. Qué es este documento

Índice navegable de las decisiones de arquitectura de `GeometriaFactory-Web`. **No contiene el cuerpo de ninguna decisión**: cada una vive en su archivo individual bajo [`Adrs/`](Adrs/), es inmutable una vez aceptada y, si evoluciona, se registra en una ADR nueva y la anterior pasa a `Superado por ADR-YY` sin reescribirse.

## 2. ADR vigentes

| ADR | Título | Categoría | Estado | Fecha |
| --- | --- | --- | --- | --- |
| [ADR-10001](Adrs/ADR-10001-Render-En-El-Servidor-Con-Circuito-Interactivo.md) | Render en el servidor con circuito interactivo, y una sola salida hacia el servicio de datos | Estilo | Propuesto | 2026-08-10 |
| [ADR-10002](Adrs/ADR-10002-Sin-Estado-Propio-Y-Sin-Persistencia.md) | Sin estado propio y sin persistencia, y por qué se omite el modelo de datos lógico | Persistencia | Propuesto | 2026-08-10 |
| [ADR-10003](Adrs/ADR-10003-Credencial-De-Sesion-En-El-Estado-Del-Circuito.md) | La credencial de sesión vive en el estado del circuito, y las rutas acotan sin hacer cumplir | Seguridad | Propuesto | 2026-08-10 |
| [ADR-10004](Adrs/ADR-10004-Tres-Capas-De-Presentacion.md) | Tres capas de presentación: ninguna superficie llega sola al servicio de datos | Estilo | Propuesto | 2026-08-10 |
| [ADR-10005](Adrs/ADR-10005-Estado-Degradado-Como-Superficie.md) | Un traductor único de condiciones, y el estado degradado como superficie y no como error | Comunicación | Propuesto | 2026-08-10 |
| [ADR-10006](Adrs/ADR-10006-Aislamiento-Del-Visor-Tras-Su-Fachada.md) | El visor se opera sólo por sus seis funciones, y es esta pieza la que consulta el entorno | Comunicación | Propuesto | 2026-08-10 |
| [ADR-10007](Adrs/ADR-10007-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md) | La dirección del servicio de datos viene de configuración, y el despliegue termina comprobando | Despliegue | Propuesto | 2026-08-10 |

**Siete ADR**, sobre el mínimo de cinco que la regla de la categoría fija para el tipo `web-monolith`. Ninguna superada, ninguna rechazada.

## 3. Las cinco decisiones que la regla exige, y dónde están

La regla fija cinco decisiones obligatorias para este tipo D8. Ninguna queda sin ADR, y dos comparten archivo con otra cosa:

| Decisión exigida | ADR que la cubre |
| --- | --- |
| Estilo | [ADR-10001](Adrs/ADR-10001-Render-En-El-Servidor-Con-Circuito-Interactivo.md), el modelo de front; y [ADR-10004](Adrs/ADR-10004-Tres-Capas-De-Presentacion.md), la organización interna |
| Persistencia | [ADR-10002](Adrs/ADR-10002-Sin-Estado-Propio-Y-Sin-Persistencia.md), que decide **no tenerla** y registra la omisión del modelo lógico |
| Autenticación | [ADR-10003](Adrs/ADR-10003-Credencial-De-Sesion-En-El-Estado-Del-Circuito.md) |
| Separación de capas | [ADR-10004](Adrs/ADR-10004-Tres-Capas-De-Presentacion.md) |
| Manejo de errores | [ADR-10005](Adrs/ADR-10005-Estado-Degradado-Como-Superficie.md) |

## 4. Por qué son siete y no cinco

Las dos que exceden el mínimo tienen origen declarado:

| ADR | Por qué existe |
| --- | --- |
| ADR-10006 | Este proyecto de código es el **anfitrión del bundle del visor**, y la Fase C de `GeometriaFactory-Visor` declaró que **el componente anfitrión —capa 1 de su arquitectura— vive acá**. Es decir: una capa de otra arquitectura es un componente de ésta. Además, `RA-02` sólo se sostiene si **esta** pieza consulta el entorno del navegador. Dejarlo como viñeta habría enterrado la obligación de la que depende la pureza del visor |
| ADR-10007 | La fuente declara tres hechos que ninguna otra ADR alcanza: que la dirección del servidor propio **no se versiona**, que la subida al hosting **no es transaccional** (`R-03`), y que el flujo de publicación **termina comprobando** y no subiendo. Es la única unidad desplegable de las dos del producto que se publica en un hosting de terceros |

## 5. Cobertura de las categorías de decisión

| Categoría | ADR que la cubre | Observación |
| --- | --- | --- |
| Estilo | ADR-10001, ADR-10004 | — |
| Persistencia | ADR-10002 | La decisión es **no tener persistencia**, y por eso lleva ADR: contradice el valor por defecto de la regla para este tipo D8 |
| Comunicación | ADR-10005, ADR-10006 | Las dos fronteras de esta pieza: hacia el servicio de datos —lo que vuelve como condición— y hacia el bundle del visor |
| Seguridad | ADR-10003 | Custodia de la credencial y guardianes de ruta que **acotan sin hacer cumplir** |
| Observabilidad | **Ninguna** | `tiene_observabilidad_critica` es false y §17.6.P.10 no declara instrumentación. Lo que la fuente sí exige es **manejo explícito** del cartel de reconexión y del estado degradado, y eso vive en ADR-10005. Un registro del lado del front no tendría consumidor: no hay operador mirando el hosting |
| Despliegue | ADR-10007 | — |
| Extensibilidad | **Ninguna** | `tiene_extensibilidad` es false en el `PRODUCT-MANIFEST` §5. El punto de extensión del producto es el contrato de la fachada del visor, y **este proyecto de código es su consumidor, no su dueño**: cómo crece esa fachada lo declara [`Extensibilidad.md`](Extensibilidad.md) §5 de `GeometriaFactory-Visor` |

Las dos categorías sin ADR se declaran vacías con su motivo, para que nadie las complete más adelante con decisiones inventadas.

## 6. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Indexa las siete ADR de `GeometriaFactory-Web` con su categoría, su estado y su fecha, declara dónde vive cada una de las cinco decisiones que la regla exige para el tipo `web-monolith`, declara por qué son siete y no cinco, y declara vacías con su motivo las dos categorías de decisión que este proyecto de código no toca. |
