# Índice de decisiones de arquitectura — GeometriaFactory-Application

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** Decisiones-Arquitectura.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)

---

## 1. Qué es este documento

Índice navegable de las decisiones de arquitectura de `GeometriaFactory-Application`. **No contiene el cuerpo de ninguna decisión**: cada una vive en su archivo individual bajo [`Adrs/`](Adrs/), es inmutable una vez aceptada y, si evoluciona, se registra en una ADR nueva y la anterior pasa a `Superado por ADR-YY` sin reescribirse.

## 2. ADR vigentes

| ADR | Título | Categoría | Estado | Fecha |
| --- | --- | --- | --- | --- |
| [ADR-04001](../../Adrs/ADR-04001-Casos-De-Uso-Con-Inversion-De-Dependencias.md) | Casos de uso con inversión de dependencias, con una sola dependencia saliente | Estilo | Propuesto | 2026-08-10 |
| [ADR-04002](../../Adrs/ADR-04002-Cuatro-Puertos-Y-La-Frontera-Que-Declaran.md) | Cuatro puertos, y qué significa que el cuarto no tenga nombre declarado | Comunicación | Propuesto | 2026-08-10 |
| [ADR-04003](../../Adrs/ADR-04003-Versionado-Y-Estabilidad-De-La-Superficie.md) | Versionado por compilación compartida y estabilidad de la superficie de dos caras | Despliegue | Propuesto | 2026-08-10 |
| [ADR-04004](../../Adrs/ADR-04004-Orden-Fijo-De-Las-Cuatro-Comprobaciones.md) | Orden fijo de las cuatro comprobaciones de autorización, en un único componente | Seguridad | Propuesto | 2026-08-10 |
| [ADR-04005](../../Adrs/ADR-04005-Un-Caso-De-Uso-Una-Unidad-De-Trabajo.md) | Un caso de uso, una unidad de trabajo: el alcance lo fija esta capa | Persistencia | Propuesto | 2026-08-10 |
| [ADR-04006](../../Adrs/ADR-04006-Resultado-Tipado-Y-Catalogo-Cerrado-De-Condiciones.md) | Resultado tipado hacia arriba, con el catálogo de treinta y seis condiciones como conjunto cerrado | Estilo | Propuesto | 2026-08-10 |

**Seis ADR**, sobre el mínimo de tres que la regla de la categoría fija para el tipo `library`. Ninguna superada, ninguna rechazada.

## 3. Por qué son seis y no tres

El mínimo de tres cubre estilo, superficie pública y estrategia de versionado, y acá son ADR-04001, ADR-04002 y ADR-04003 —la superficie pública de este proyecto de código tiene **dos caras**, y la de abajo son los puertos—. Las otras tres tienen origen declarado:

| ADR | Por qué existe |
| --- | --- |
| ADR-04004 | El flag `tiene_auth` es **true** en este proyecto de código, y el `PRODUCT-MANIFEST` §5 declara explícitamente que el efecto de esa corrección «es que la categoría 05 de esos dos proyectos de código emite su ADR de autenticación, que con el valor anterior se habría omitido». Además cierra la dependencia de disciplina que [`GeometriaFactory-Domain ADR-02005`](../../Adrs/ADR-02005-Guarda-Unica-De-Admisibilidad.md) §6 declaró que el dominio no podía cerrar: dejarla como viñeta habría enterrado la decisión que sostiene `INV-09` |
| ADR-04005 | El intake declara la persistencia de este proyecto de código como «no aplica directamente», pero le asigna **el alcance de la unidad de trabajo** (§17.2.P.4). Es la única decisión de persistencia que esta capa toma, y sin ella el límite de consistencia quedaría sin dueño entre el dominio —que no abre unidades— y el adaptador —que no sabe qué operaciones forman un acto— |
| ADR-04006 | La categoría 03 catalogó **36** condiciones y ninguna fuente declaraba quién puede acuñar una nueva. Un catálogo que crece desde varios lugares deja de ser cerrado, y aguas abajo `GeometriaFactory-Api` tiene que traducir cada condición a una respuesta de protocolo |

## 4. Cobertura de las categorías de decisión

| Categoría | ADR que la cubre | Observación |
| --- | --- | --- |
| Estilo | ADR-04001, ADR-04006 | — |
| Persistencia | ADR-04005 | No hay persistencia propia: lo que la ADR gobierna es el **alcance** de la unidad de trabajo y la forma de las dos lecturas del puerto de repositorio |
| Comunicación | ADR-04002 | Los cuatro puertos son la única frontera del proyecto de código. No hay comunicación entre procesos: §17.2.P.3 declara «no aplica» hacia afuera |
| Seguridad | ADR-04004 | Autorización, no autenticación: acá no se comparan contraseñas ni se emiten accesos |
| Observabilidad | **Ninguna** | `tiene_observabilidad_critica` es false y §17.2.P.10 no declara observabilidad propia. Esta capa no instrumenta: la correlación la lleva `GeometriaFactory-Api`, que es quien tiene petición que correlacionar |
| Despliegue | ADR-04003 | No hay unidad de despliegue propia; lo que la ADR gobierna es la construcción, el versionado y la asimetría de las dos caras |
| Extensibilidad | **Ninguna** | `tiene_extensibilidad` es false en el `PRODUCT-MANIFEST` §5. El punto de extensión declarado del producto es el contrato de la fachada del visor, que es de `GeometriaFactory-Visor` |

Las dos categorías sin ADR se declaran vacías con su motivo, para que nadie las complete más adelante con decisiones inventadas.

## 5. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Indexa las seis ADR de `GeometriaFactory-Application` con su categoría, su estado y su fecha, declara por qué son seis y no tres —con el ADR de autenticación que el manifiesto exige por `tiene_auth` == true—, y declara vacías con su motivo las dos categorías de decisión que este proyecto de código no toca. |
