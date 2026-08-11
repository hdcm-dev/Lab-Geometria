# Índice de decisiones de arquitectura — GeometriaFactory-Domain

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** Decisiones-Arquitectura.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)

---

## 1. Qué es este documento

Índice navegable de las decisiones de arquitectura de `GeometriaFactory-Domain`. **No contiene el cuerpo de ninguna decisión**: cada una vive en su archivo individual bajo [`Adrs/`](Adrs/), es inmutable una vez aceptada y, si evoluciona, se registra en una ADR nueva y la anterior pasa a estado `Superado por ADR-YY` sin reescribirse.

## 2. ADR vigentes

| ADR | Título | Categoría | Estado | Fecha |
| --- | --- | --- | --- | --- |
| [ADR-01](Adrs/ADR-01-Modelo-De-Dominio-Rico-Con-Invariantes-Explicitas.md) | Modelo de dominio rico con invariantes explícitas y cero dependencias | Estilo | Propuesto | 2026-08-10 |
| [ADR-02](Adrs/ADR-02-Superficie-Publica-De-Guardas-Y-Resultados-Tipados.md) | La superficie pública son guardas con resultado tipado, no excepciones | Estilo | Propuesto | 2026-08-10 |
| [ADR-03](Adrs/ADR-03-Versionado-Y-Estabilidad-De-La-Superficie.md) | Versionado por versionado semántico sin publicación, y estabilidad de la superficie | Despliegue | Propuesto | 2026-08-10 |
| [ADR-04](Adrs/ADR-04-Frontera-De-Autenticacion-Y-Autorizacion.md) | Frontera de autenticación: el dominio modela la condición y no el mecanismo | Seguridad | Propuesto | 2026-08-10 |
| [ADR-05](Adrs/ADR-05-Guarda-Unica-De-Admisibilidad.md) | Puerta única de admisibilidad para las guardas de acceso de la cuenta | Seguridad | Propuesto | 2026-08-10 |
| [ADR-06](Adrs/ADR-06-El-Dominio-No-Lee-El-Reloj-Ni-El-Conjunto.md) | El dominio no lee el reloj ni el conjunto de entidades: los dos entran por parámetro | Estilo | Propuesto | 2026-08-10 |

**Seis ADR**, sobre el mínimo de tres que la regla de la categoría fija para el tipo `library`. Ninguna está superada y ninguna rechazada.

## 3. Por qué son seis y no tres

El mínimo de tres para `library` cubre estilo, superficie pública y estrategia de versionado, y son ADR-01, ADR-02 y ADR-03. Las otras tres tienen origen declarado:

| ADR | Por qué existe |
| --- | --- |
| ADR-04 | El `PRODUCT-MANIFEST` §5 declara que corregir `tiene_auth` a true en este proyecto de código tiene por efecto que **la categoría 05 emita su ADR de autenticación**, que con el valor anterior se habría omitido |
| ADR-05 | La categoría 02 tomó una decisión derivada —concentrar la guarda de INV-09 en la admisibilidad— y la declaró como tal. Una decisión de esa clase enterrada como viñeta del documento maestro es el anti-patrón que la regla de la categoría nombra primero |
| ADR-06 | Que el momento y la unicidad entren por parámetro es lo que hace reproducible la batería del dominio y lo que sostiene las cero dependencias. Sin ADR, se lee como un detalle de firma en vez de como la decisión que es |

## 4. Cobertura de las categorías de decisión

| Categoría | ADR que la cubre | Observación |
| --- | --- | --- |
| Estilo | ADR-01, ADR-02, ADR-06 | — |
| Persistencia | **Ninguna** | El flag `tiene_persistencia` es false y el intake declara «no aplica» en §17.1.P.4. No hay decisión de persistencia que tomar acá |
| Comunicación | **Ninguna** | Este proyecto de código no expone protocolos ni cruza fronteras de proceso (`PRODUCT-INTAKE` §17.1.P.3) |
| Seguridad | ADR-04, ADR-05 | — |
| Observabilidad | **Ninguna** | El intake declara «sin observabilidad propia» en §17.1.P.10: no registra ni instrumenta |
| Despliegue | ADR-03 | No hay unidad de despliegue propia; lo que la ADR gobierna es la construcción y el versionado |
| Extensibilidad | **Ninguna** | El flag `tiene_extensibilidad` es false. El punto de extensión del producto es el contrato de la fachada del visor, no este proyecto de código |

Las cuatro categorías sin ADR se declaran vacías con su motivo, en lugar de omitirse, para que nadie las complete más adelante con decisiones inventadas.

## 5. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Indexa las seis ADR de `GeometriaFactory-Domain` con su categoría, su estado y su fecha, declara por qué son seis y no tres, y declara vacías con su motivo las cuatro categorías de decisión que este proyecto de código no toca. |
