# 05 · Arquitectura técnica — GeometriaFactory-Application

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** README.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Tipo de proyecto de código (D8):** `library`

---

## 1. Punto de entrada

`GeometriaFactory-Application` es la capa de **casos de uso y puertos**: orquesta el dominio, declara la frontera que la infraestructura implementa, y no conoce ni el protocolo de transporte ni la base de datos. Es nivel 1 del orden topológico y su única dependencia de compilación es `GeometriaFactory-Domain`.

Lo que hay que haber entendido antes de tocar esta sección, y que atraviesa los cuatro documentos: **la superficie de este proyecto de código tiene dos caras** —once casos de uso hacia arriba, cuatro puertos hacia abajo— y **acá se autoriza, no se autentica**. El punto de entrada es [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md).

## 2. Documentos de esta sección

| Documento | Propósito |
| --- | --- |
| [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) | Documento maestro: estilo, las cuatro vistas mínimas, cross-cutting, nueve NFR, seis riesgos, trazabilidad de las dieciséis reglas, de los nueve invariantes y de las tres reglas de arquitectura del producto, y seis puntos abiertos |
| [`Decisiones-Arquitectura.md`](Decisiones-Arquitectura.md) | Índice de las seis ADR, con la declaración de las dos categorías de decisión que quedan vacías y por qué |
| [`Contratos-Abstractions.md`](Contratos-Abstractions.md) | Contrato de la superficie de dos caras: once operaciones, cuatro puertos, la tabla de las cuatro comprobaciones contra cada operación, manejo de errores y versionado |
| [`Adrs/`](Adrs/) | Las seis decisiones, una por archivo |

## 3. ADR vigentes

| ADR | Título | Categoría | Estado |
| --- | --- | --- | --- |
| [ADR-01](Adrs/ADR-01-Casos-De-Uso-Con-Inversion-De-Dependencias.md) | Casos de uso con inversión de dependencias, con una sola dependencia saliente | Estilo | Propuesto |
| [ADR-02](Adrs/ADR-02-Cuatro-Puertos-Y-La-Frontera-Que-Declaran.md) | Cuatro puertos, y qué significa que el cuarto no tenga nombre declarado | Comunicación | Propuesto |
| [ADR-03](Adrs/ADR-03-Versionado-Y-Estabilidad-De-La-Superficie.md) | Versionado por compilación compartida y estabilidad de la superficie de dos caras | Despliegue | Propuesto |
| [ADR-04](Adrs/ADR-04-Orden-Fijo-De-Las-Cuatro-Comprobaciones.md) | Orden fijo de las cuatro comprobaciones de autorización, en un único componente | Seguridad | Propuesto |
| [ADR-05](Adrs/ADR-05-Un-Caso-De-Uso-Una-Unidad-De-Trabajo.md) | Un caso de uso, una unidad de trabajo: el alcance lo fija esta capa | Persistencia | Propuesto |
| [ADR-06](Adrs/ADR-06-Resultado-Tipado-Y-Catalogo-Cerrado-De-Condiciones.md) | Resultado tipado hacia arriba, con el catálogo de treinta y seis condiciones como conjunto cerrado | Estilo | Propuesto |

Ninguna superada, ninguna rechazada.

## 4. NFR vigentes

Los nueve, con su objetivo numérico y su mecanismo, están en [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) §8. En una línea: el caso de uso más pesado por debajo de **500 ms** sobre el texto semilla de **3** piezas de `E-1` y cobertura **85 %** de líneas y **80 %** de ramas —los dos rotulados `[ASUNCIÓN]` por el intake—, **0** pruebas que tocan la base real, **1** sola dependencia saliente, **0** componentes de pieza en las consultas de listado, **100 %** de las 36 condiciones ejercitadas en las dos direcciones, **4 de 4** comprobaciones probadas, **a lo sumo 1** unidad de trabajo por caso de uso y **0** advertencias de construcción.

## 5. Orden de lectura sugerido

1. [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) §2 y §3 — el estilo, los ocho componentes y, sobre todo, **§2.2**, las tres decisiones del nivel 0 que esta capa hereda y no reabre. Sin eso, la cuarta comprobación de autorización se lee como una precaución y no como el cierre de una puerta que el dominio declaró que no podía cerrar.
2. [`Adrs/ADR-01`](Adrs/ADR-01-Casos-De-Uso-Con-Inversion-De-Dependencias.md) — la decisión de la que dependen todas las demás.
3. [`Contratos-Abstractions.md`](Contratos-Abstractions.md) §3 y §4 — las dos caras. La columna de lo que cada operación **exige resuelto** por el consumidor es la frontera de arriba en forma de tabla; §4 es la de abajo.
4. [`Adrs/ADR-04`](Adrs/ADR-04-Orden-Fijo-De-Las-Cuatro-Comprobaciones.md) junto con [`Contratos-Abstractions.md`](Contratos-Abstractions.md) §5 — la decisión y su tabla de aplicación, que se leen mejor juntas.
5. [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) §10 — la trazabilidad, para consultar por regla, por invariante o por regla de arquitectura.

## 6. Artefactos omitidos y su motivo

| Artefacto | Estado | Motivo |
| --- | --- | --- |
| `Modelo-Datos-Logico.md` | **Omitido** | La regla de la categoría lo omite para `library` sin estado. `tiene_persistencia` es false y el intake declara «no aplica directamente» en §17.2.P.4. Lo que esta capa sí decide sobre persistencia es el **alcance** de la unidad de trabajo, y eso vive en [`ADR-05`](Adrs/ADR-05-Un-Caso-De-Uso-Una-Unidad-De-Trabajo.md). El modelo lógico le corresponde a la categoría 05 de `GeometriaFactory-Infrastructure` |
| `Flujo-Ejecucion.md` | **Omitido** | La regla lo recomienda para `library` **con motor de procesamiento**, y este proyecto de código no lo tiene: el motor del producto es el validador de figuras, que acá vive **detrás de un puerto** y se implementa en `GeometriaFactory-Infrastructure`. Lo que esta capa tiene son once orquestaciones de un solo paso lógico cada una, y la más compleja —el envío— se lee entera en `CU-05` §4 |
| `Extensibilidad.md` | **Omitido** | `tiene_extensibilidad` es false en el `PRODUCT-MANIFEST` §5. El punto de extensión declarado del producto es el contrato de la fachada del visor, no este proyecto de código |
| `_legacy/` | **No existe** | Es la primera emisión de esta categoría en este proyecto de código: no hay ninguna versión superada que archivar |

## 7. Lo que esta sección resolvió de lo que aguas arriba quedó abierto

Tres puntos abiertos llegaron a esta categoría explícitamente, y conviene decir qué pasó con cada uno en lugar de dejarlo repartido:

| Punto que llegó | Qué hizo esta sección |
| --- | --- |
| El **identificador del puerto de repositorio de cuentas**, elevado por la categoría 02 | **Resuelto a medias, y declarado así.** [`ADR-02`](Adrs/ADR-02-Cuatro-Puertos-Y-La-Frontera-Que-Declaran.md) confirma que el puerto **existe** y por qué es estructuralmente necesario, y declara que **su nombre no se inventa acá**: queda en el punto de control de la etapa `a`, con los demás nombres de tipos. Sigue como `PA-01` |
| Los **nombres de tipos y de espacios de nombres** | **No resuelto, y correctamente**: el intake los ató a la etapa `a` y esta categoría no los adelanta. Sigue como `PA-02` |
| El **criterio de comparación de dos correos** | **Reasignado con fundamento.** No es una decisión de esta capa: el adaptador del puerto de repositorio de cuentas es quien la materializa, junto con el índice que la sostenga. Pasa a la categoría 05 de `GeometriaFactory-Infrastructure` como `PA-03` |

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Índice navegable de la sección: los cuatro documentos vigentes, las seis ADR con su estado, los NFR en una línea, el orden de lectura de cinco pasos, los tres artefactos omitidos con su motivo y el destino de los tres puntos abiertos que las categorías 02 y 03 derivaron a ésta. |
