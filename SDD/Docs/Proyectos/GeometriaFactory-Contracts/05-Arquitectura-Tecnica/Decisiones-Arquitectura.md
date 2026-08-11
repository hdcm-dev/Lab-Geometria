# Índice de decisiones de arquitectura — GeometriaFactory-Contracts

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** Decisiones-Arquitectura.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)

---

## 1. Qué es este documento

Índice navegable de las decisiones de arquitectura de `GeometriaFactory-Contracts`. **No contiene el cuerpo de ninguna decisión**: cada una vive en su archivo individual bajo [`Adrs/`](Adrs/), es inmutable una vez aceptada y, si evoluciona, se registra en una ADR nueva y la anterior pasa a `Superado por ADR-YY` sin reescribirse.

## 2. ADR vigentes

| ADR | Título | Categoría | Estado | Fecha |
| --- | --- | --- | --- | --- |
| [ADR-01](Adrs/ADR-01-Tipos-De-Transferencia-Planos-Sin-Dependencias.md) | Ensamblado de tipos de transferencia planos, sin comportamiento y sin dependencias | Estilo | Propuesto | 2026-08-10 |
| [ADR-02](Adrs/ADR-02-Tipo-De-Error-Unico-Con-Conjunto-Cerrado.md) | Un único tipo de error, con conjunto cerrado de quince códigos | Comunicación | Propuesto | 2026-08-10 |
| [ADR-03](Adrs/ADR-03-Versionado-Por-Compilacion-Compartida.md) | Versionado por compilación compartida y despliegue conjunto, sin versionado de rutas | Despliegue | Propuesto | 2026-08-10 |
| [ADR-04](Adrs/ADR-04-Regla-De-Exposicion-De-La-Frontera.md) | Regla de exposición: lista cerrada de lo que nunca cruza la frontera | Seguridad | Propuesto | 2026-08-10 |
| [ADR-05](Adrs/ADR-05-Proyeccion-De-Listado-Separada-Del-Detalle.md) | Proyección de listado separada del detalle, y el comentario como bloque propio | Comunicación | Propuesto | 2026-08-10 |

**Cinco ADR**, sobre el mínimo de tres que la regla de la categoría fija para el tipo `library`. Ninguna superada, ninguna rechazada.

## 3. Por qué son cinco y no tres

El mínimo de tres cubre estilo, superficie pública y estrategia de versionado, y acá son ADR-01, ADR-02 y ADR-03 —la superficie pública de este proyecto de código es, en buena medida, su tipo de error—. Las otras dos tienen origen declarado:

| ADR | Por qué existe |
| --- | --- |
| ADR-04 | Es la decisión central del proyecto de código. `PRODUCT-INTAKE` §17.4.P.5 declara que acá «es donde se decide qué se expone», y las tres reglas de arquitectura de nivel producto caen sobre esta frontera. Dejarla como viñeta del documento maestro habría enterrado la decisión más importante que este proyecto de código toma |
| ADR-05 | Es el único requerimiento no funcional propio que el intake le declara (§17.4.P.10), y la categoría 02 lo amplió y lo usó como criterio para separar dos contratos de uso. Merece registro con sus alternativas |

## 4. Cobertura de las categorías de decisión

| Categoría | ADR que la cubre | Observación |
| --- | --- | --- |
| Estilo | ADR-01 | — |
| Persistencia | **Ninguna** | `tiene_persistencia` es false y el intake declara «no aplica» en §17.4.P.4 |
| Comunicación | ADR-02, ADR-05 | Es la categoría dominante: este proyecto de código **es** el contrato de comunicación del producto |
| Seguridad | ADR-04 | — |
| Observabilidad | **Ninguna** | El intake no declara observabilidad propia en §17.4.P.10, y el ensamblado no ejecuta nada que instrumentar |
| Despliegue | ADR-03 | No hay unidad de despliegue propia; lo que la ADR gobierna es la construcción, el versionado y la obligación de despliegue conjunto |
| Extensibilidad | **Ninguna** | `tiene_extensibilidad` es false. El punto de extensión del producto es el contrato de la fachada del visor |

Las cuatro categorías sin ADR se declaran vacías con su motivo, para que nadie las complete más adelante con decisiones inventadas.

## 5. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Indexa las cinco ADR de `GeometriaFactory-Contracts` con su categoría, su estado y su fecha, declara por qué son cinco y no tres, y declara vacías con su motivo las cuatro categorías de decisión que este proyecto de código no toca. |
