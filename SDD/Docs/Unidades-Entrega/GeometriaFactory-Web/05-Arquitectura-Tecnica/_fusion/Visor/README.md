# 05 · Arquitectura técnica — GeometriaFactory-Visor

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Visor
**Documento:** README.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Tipo de proyecto de código (D8):** `library`

---

## 1. Punto de entrada

`GeometriaFactory-Visor` es el bundle del visualizador tridimensional del producto: **seis** funciones de fachada, sin red, sin configuración y sin identidad. Es nivel 0 del orden topológico, no tiene dependencias dentro del producto y es el único proyecto de código fuera del ecosistema de los otros seis.

Lo que hay que haber entendido antes de tocar esta sección: **su contribución a la seguridad del producto es negativa por diseño** —no hacer red es lo que hace imposible violar `RA-01` desde el navegador— y **el contrato de su fachada es el punto de extensión declarado del producto**. El punto de entrada es [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md).

## 2. Documentos de esta sección

| Documento | Propósito |
| --- | --- |
| [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) | Documento maestro: estilo de tres capas, los seis componentes, qué se porta y qué no del visualizador previo, las cuatro vistas mínimas, cross-cutting, ocho NFR, seis riesgos, trazabilidad de las siete garantías y de las tres reglas de arquitectura, y cinco puntos abiertos |
| [`Decisiones-Arquitectura.md`](Decisiones-Arquitectura.md) | Índice de las seis ADR, con las tres categorías de decisión declaradas vacías y su motivo |
| [`Contratos-Abstractions.md`](../../Contratos-Abstractions.md) | Contrato de la superficie pública: las seis funciones, el resultado de dibujo, los tres conjuntos cerrados, los siete códigos y el versionado |
| [`Flujo-Ejecucion.md`](../../Flujo-Ejecucion.md) | La canalización de dibujo paso a paso, con sus transformaciones, el bucle y la liberación de recursos |
| [`Extensibilidad.md`](../../Extensibilidad.md) | El punto de extensión del producto: qué se reemplaza, los ocho compromisos de un reemplazo y cómo crece la fachada |
| [`Adrs/`](Adrs/) | Las seis decisiones, una por archivo |

## 3. ADR vigentes

| ADR | Título | Categoría | Estado |
| --- | --- | --- | --- |
| [ADR-12001](../../Adrs/ADR-12001-Tres-Capas-Con-Fachada-Plana.md) | Tres capas con fachada plana, y el motor de dibujo confinado a la capa interna | Estilo | Propuesto |
| [ADR-12002](../../Adrs/ADR-12002-Superficie-De-Seis-Funciones-Planas.md) | La superficie pública son seis funciones planas, siete garantías y siete códigos | Estilo | Propuesto |
| [ADR-12003](../../Adrs/ADR-12003-Visualizador-Puro-Sin-Red-Ni-Identidad.md) | Visualizador puro: cero red, cero persistencia, cero configuración y cero identidad | Seguridad | Propuesto |
| [ADR-12004](../../Adrs/ADR-12004-Motor-De-Dibujo-Empaquetado-Y-Aislado.md) | Motor de dibujo empaquetado dentro del bundle y aislado tras la capa 3 | Despliegue | Propuesto |
| [ADR-12005](../../Adrs/ADR-12005-Disposicion-Determinista-Derivada-Del-Indice.md) | Disposición determinista derivada del índice, y el determinismo es de posición y no de orientación | Estilo | Propuesto |
| [ADR-12006](../../Adrs/ADR-12006-Bundle-Generado-Y-Versionado-Del-Punto-De-Extension.md) | El artefacto es un bundle generado, y su versionado es el del punto de extensión | Despliegue | Propuesto |

Ninguna superada, ninguna rechazada.

## 4. NFR vigentes

Los ocho, con su objetivo numérico, su mecanismo y **sus condiciones de medición**, están en [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) §8. Las seis primeras son las **seis propiedades transversales verificables** que la categoría 02 declara como lugar único; las dos últimas las deriva esta categoría.

En una línea: **0** peticiones de red y **0** claves escritas, **6 de 6** funciones ejercitables sin backend, disposición determinista comparando **posición y no orientación**, **10** recorridos sin degradación, **100 %** de las piezas no dibujadas enumeradas, **0** dependencias traídas de una red externa y exactamente **6** funciones bajo **1** nombre propio.

**Cero red y liberación de recursos se miden con los dos movimientos automáticos prendidos**, que es su peor caso. Sin esa condición, un entorno de prueba que declare preferencia de movimiento reducido dejaría las dos en verde sin haber ejercitado nunca el bucle de dibujo.

**No hay NFR de latencia con umbral numérico**, y es deliberado: ninguna fuente lo declara y esta categoría no lo inventa. Queda como punto abierto PA-03.

## 5. Orden de lectura sugerido

1. [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) §2 y §3 — el estilo de tres capas y los seis componentes.
2. [`Adrs/ADR-12003`](../../Adrs/ADR-12003-Visualizador-Puro-Sin-Red-Ni-Identidad.md) — las cuatro ausencias. Es lo que define al proyecto de código, y lo que explica por qué el anfitrión carga con tanto trabajo.
3. [`Flujo-Ejecucion.md`](../../Flujo-Ejecucion.md) §3 — la canalización de dibujo. Es donde se ve qué se lee del texto y qué deliberadamente no.
4. [`Contratos-Abstractions.md`](../../Contratos-Abstractions.md) §5 — los siete códigos, con la distinción entre **curso** y **código**, que es lo que más se malinterpreta al bajar a 03 y a 08.
5. [`Extensibilidad.md`](../../Extensibilidad.md) — el punto de extensión, y sobre todo su §5, el proceso por el que la fachada crece sin romperse.

## 6. Artefactos omitidos y su motivo

| Artefacto | Estado | Motivo |
| --- | --- | --- |
| `Modelo-Datos-Logico.md` | **Omitido** | La regla de la categoría lo omite para `library` puro sin estado. `tiene_persistencia` es false y el intake declara «no aplica, y es prohibición explícita» en §17.7.P.4: el bundle no guarda estado entre páginas ni escribe en el almacenamiento del navegador |
| `_legacy/` | **No existe** | Es la primera emisión de esta categoría en este proyecto de código: no hay ninguna versión superada que archivar |

**Esta sección no omite ni el flujo de ejecución ni la extensibilidad**, a diferencia de los otros dos proyectos de código de nivel 0: el primero porque `cargarJson` es un motor de procesamiento con transformaciones declarables, y el segundo porque `tiene_extensibilidad` es **true** y el punto de extensión del producto es precisamente el contrato de esta fachada.

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Índice navegable de la sección: los seis documentos vigentes, las seis ADR con su estado, los NFR en una línea con sus condiciones de medición y la declaración de por qué no hay umbral de latencia, el orden de lectura de cinco pasos y los artefactos omitidos con su motivo. |
