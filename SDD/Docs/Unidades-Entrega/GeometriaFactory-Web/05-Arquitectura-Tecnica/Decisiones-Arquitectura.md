# Decisiones de arquitectura — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** Decisiones-Arquitectura.md
**Versión:** 2.0
**Estado:** Propuesto
**Fecha:** 2026-08-16
**`tipo_unidad_entrega` (D8):** `web-monolith`
**Proyectos de código que la componen:** `GeometriaFactory-Web`, `GeometriaFactory-Visor` y `GeometriaFactory-Contracts`
**Consolida a:** el documento homónimo de `GeometriaFactory-Visor`, por `Audit/Migracion-M10-Consolidacion-Fusion.md` 1.2 §4

---

## 0. Cómo leer este documento

**La unidad de entrega tiene un solo documento de esta clase**, y cada sección lleva **una subsección
por proyecto de código**, con su texto **transpuesto sin reescritura**.

**Las dos secciones de cada apartado son la del portal y la del bundle del visor.** **1 secciones existen sólo en `GeometriaFactory-Visor`** —«Por qué son seis y no tres»—, y son las que el portal no podía declarar porque describen el componente empaquetado que viaja adentro.

---

## 1. Qué es este documento

### 1.1 `GeometriaFactory-Web`

Índice navegable de las decisiones de arquitectura de `GeometriaFactory-Web`. **No contiene el cuerpo de ninguna decisión**: cada una vive en su archivo individual bajo [`Adrs/`](Adrs/), es inmutable una vez aceptada y, si evoluciona, se registra en una ADR nueva y la anterior pasa a `Superado por ADR-YY` sin reescribirse.

### 1.2 `GeometriaFactory-Visor`

Índice navegable de las decisiones de arquitectura de `GeometriaFactory-Visor`. **No contiene el cuerpo de ninguna decisión**: cada una vive en su archivo individual bajo [`Adrs/`](Adrs/), es inmutable una vez aceptada y, si evoluciona, se registra en una ADR nueva y la anterior pasa a `Superado por ADR-YY` sin reescribirse.

## 2. ADR vigentes

### 2.1 `GeometriaFactory-Web`

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

### 2.2 `GeometriaFactory-Visor`

| ADR | Título | Categoría | Estado | Fecha |
| --- | --- | --- | --- | --- |
| [ADR-12001](Adrs/ADR-12001-Tres-Capas-Con-Fachada-Plana.md) | Tres capas con fachada plana, y el motor de dibujo confinado a la capa interna | Estilo | Propuesto | 2026-08-10 |
| [ADR-12002](Adrs/ADR-12002-Superficie-De-Seis-Funciones-Planas.md) | La superficie pública son seis funciones planas, siete garantías y siete códigos | Estilo | Propuesto | 2026-08-10 |
| [ADR-12003](Adrs/ADR-12003-Visualizador-Puro-Sin-Red-Ni-Identidad.md) | Visualizador puro: cero red, cero persistencia, cero configuración y cero identidad | Seguridad | Propuesto | 2026-08-10 |
| [ADR-12004](Adrs/ADR-12004-Motor-De-Dibujo-Empaquetado-Y-Aislado.md) | Motor de dibujo empaquetado dentro del bundle y aislado tras la capa 3 | Despliegue | Propuesto | 2026-08-10 |
| [ADR-12005](Adrs/ADR-12005-Disposicion-Determinista-Derivada-Del-Indice.md) | Disposición determinista derivada del índice, y el determinismo es de posición y no de orientación | Estilo | Propuesto | 2026-08-10 |
| [ADR-12006](Adrs/ADR-12006-Bundle-Generado-Y-Versionado-Del-Punto-De-Extension.md) | El artefacto es un bundle generado, y su versionado es el del punto de extensión | Despliegue | Propuesto | 2026-08-10 |

**Seis ADR**, sobre el mínimo de tres que la regla de la categoría fija para el tipo `library`. Ninguna superada, ninguna rechazada.

## 3. Las cinco decisiones que la regla exige, y dónde están

### 3.1 `GeometriaFactory-Web`

La regla fija cinco decisiones obligatorias para este tipo D8. Ninguna queda sin ADR, y dos comparten archivo con otra cosa:

| Decisión exigida | ADR que la cubre |
| --- | --- |
| Estilo | [ADR-10001](Adrs/ADR-10001-Render-En-El-Servidor-Con-Circuito-Interactivo.md), el modelo de front; y [ADR-10004](Adrs/ADR-10004-Tres-Capas-De-Presentacion.md), la organización interna |
| Persistencia | [ADR-10002](Adrs/ADR-10002-Sin-Estado-Propio-Y-Sin-Persistencia.md), que decide **no tenerla** y registra la omisión del modelo lógico |
| Autenticación | [ADR-10003](Adrs/ADR-10003-Credencial-De-Sesion-En-El-Estado-Del-Circuito.md) |
| Separación de capas | [ADR-10004](Adrs/ADR-10004-Tres-Capas-De-Presentacion.md) |
| Manejo de errores | [ADR-10005](Adrs/ADR-10005-Estado-Degradado-Como-Superficie.md) |

## 4. Por qué son siete y no cinco

### 4.1 `GeometriaFactory-Web`

Las dos que exceden el mínimo tienen origen declarado:

| ADR | Por qué existe |
| --- | --- |
| ADR-10006 | Este proyecto de código es el **anfitrión del bundle del visor**, y la Fase C de `GeometriaFactory-Visor` declaró que **el componente anfitrión —capa 1 de su arquitectura— vive acá**. Es decir: una capa de otra arquitectura es un componente de ésta. Además, `RA-02` sólo se sostiene si **esta** pieza consulta el entorno del navegador. Dejarlo como viñeta habría enterrado la obligación de la que depende la pureza del visor |
| ADR-10007 | La fuente declara tres hechos que ninguna otra ADR alcanza: que la dirección del servidor propio **no se versiona**, que la subida al hosting **no es transaccional** (`R-03`), y que el flujo de publicación **termina comprobando** y no subiendo. Es la única unidad desplegable de las dos del producto que se publica en un hosting de terceros |

## 5. Cobertura de las categorías de decisión

### 5.1 `GeometriaFactory-Web`

| Categoría | ADR que la cubre | Observación |
| --- | --- | --- |
| Estilo | ADR-10001, ADR-10004 | — |
| Persistencia | ADR-10002 | La decisión es **no tener persistencia**, y por eso lleva ADR: contradice el valor por defecto de la regla para este tipo D8 |
| Comunicación | ADR-10005, ADR-10006 | Las dos fronteras de esta pieza: hacia el servicio de datos —lo que vuelve como condición— y hacia el bundle del visor |
| Seguridad | ADR-10003 | Custodia de la credencial y guardianes de ruta que **acotan sin hacer cumplir** |
| Observabilidad | **Ninguna** | `tiene_observabilidad_critica` es false y §17.2.P.10 · GeometriaFactory-Web no declara instrumentación. Lo que la fuente sí exige es **manejo explícito** del cartel de reconexión y del estado degradado, y eso vive en ADR-10005. Un registro del lado del front no tendría consumidor: no hay operador mirando el hosting |
| Despliegue | ADR-10007 | — |
| Extensibilidad | **Ninguna** | `tiene_extensibilidad` es false en el `PRODUCT-MANIFEST` §5. El punto de extensión del producto es el contrato de la fachada del visor, y **este proyecto de código es su consumidor, no su dueño**: cómo crece esa fachada lo declara [`Extensibilidad.md`](Extensibilidad.md) §5 de `GeometriaFactory-Visor` |

Las dos categorías sin ADR se declaran vacías con su motivo, para que nadie las complete más adelante con decisiones inventadas.

### 5.2 `GeometriaFactory-Visor`

| Categoría | ADR que la cubre | Observación |
| --- | --- | --- |
| Estilo | ADR-12001, ADR-12002, ADR-12005 | — |
| Persistencia | **Ninguna, y es prohibición explícita** | `tiene_persistencia` es false y el intake declara «no aplica, y es prohibición explícita» en §17.2.P.4 · GeometriaFactory-Visor. La ausencia está registrada como garantía G-2 dentro de ADR-12003, que es su lugar |
| Comunicación | **Ninguna** | Este proyecto de código no se comunica con nada: la ausencia de red es una decisión de seguridad y está en ADR-12003 |
| Seguridad | ADR-12003 | Su contribución a la seguridad del producto es **negativa por diseño**: no hacer red |
| Observabilidad | **Ninguna** | El bundle no instrumenta ni emite registros. `tiene_observabilidad_critica` es false |
| Despliegue | ADR-12004, ADR-12006 | No hay unidad de despliegue propia: el artefacto viaja dentro del despliegue del anfitrión |
| Extensibilidad | ADR-12002 y ADR-12006, con su desarrollo en [`Extensibilidad.md`](Extensibilidad.md) | `tiene_extensibilidad` es **true**, y es el único proyecto de código del producto en el que lo es |

Las tres categorías sin ADR se declaran vacías con su motivo, para que nadie las complete más adelante con decisiones inventadas.

## 6. Por qué son seis y no tres

### 6.1 `GeometriaFactory-Visor`

El mínimo de tres cubre estilo, superficie pública y estrategia de versionado, y acá son ADR-12001, ADR-12002 y ADR-12006. Las otras tres tienen origen declarado:

| ADR | Por qué existe |
| --- | --- |
| ADR-12003 | Es la regla de arquitectura del producto que este proyecto de código materializa. `RA-02` no es una preferencia de diseño: es lo que hace **imposible** violar `RA-01` desde el navegador, y romperla en un solo proyecto de código reabre las tres propiedades de la topología |
| ADR-12004 | Es lo que mide la puerta técnica `PT-03`, y es la única dependencia externa real del proyecto de código. Su tratamiento condiciona que la página funcione desde la red del aula |
| ADR-12005 | Reemplaza una conducta del visualizador previo y, desde la capacidad F-25, tiene una acotación que hay que declarar en cada lugar donde se afirma el determinismo: es de posición y no de orientación. Sin ADR, la acotación se lee como un detalle |

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 2.0 | 2026-08-16 | **Consolidación de la fusión.** Pasa a ser el documento de la **unidad de entrega**, absorbiendo el de `GeometriaFactory-Visor`, con su texto transpuesto sin reescritura. Entra §0. Sube **major**. |
