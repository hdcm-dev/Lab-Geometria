# 05 · Arquitectura técnica — GeometriaFactory-Domain

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** README.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Tipo de proyecto de código (D8):** `library`

---

## 1. Punto de entrada

`GeometriaFactory-Domain` es una biblioteca **sin dependencias**: modelo, reglas e invariantes. No conoce persistencia, ni red, ni marco de aplicación. Es el nivel 0 del orden topológico del producto y el centro de la regla de dependencias.

Lo que hay que haber entendido antes de tocar esta sección, y que atraviesa los nueve documentos: **la superficie pública de este proyecto de código son sus guardas**, y lo que las gobierna son **dieciséis** reglas de negocio expresadas como **nueve** invariantes sobre **cinco** entidades. El punto de entrada es [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md).

## 2. Documentos de esta sección

| Documento | Propósito |
| --- | --- |
| [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) | Documento maestro: estilo, las cuatro vistas mínimas, cross-cutting, seis NFR, cinco riesgos, trazabilidad de las dieciséis reglas y de los nueve invariantes, y cuatro puntos abiertos |
| [`Decisiones-Arquitectura.md`](Decisiones-Arquitectura.md) | Índice de las seis ADR, con la declaración de las cuatro categorías de decisión que quedan vacías y por qué |
| [`Contratos-Abstractions.md`](Contratos-Abstractions.md) | Contrato de la superficie pública: trece operaciones, cinco entidades, nueve conjuntos cerrados, manejo de errores y versionado |
| [`Adrs/`](Adrs/) | Las seis decisiones, una por archivo |

## 3. ADR vigentes

| ADR | Título | Categoría | Estado |
| --- | --- | --- | --- |
| [ADR-01](Adrs/ADR-01-Modelo-De-Dominio-Rico-Con-Invariantes-Explicitas.md) | Modelo de dominio rico con invariantes explícitas y cero dependencias | Estilo | Propuesto |
| [ADR-02](Adrs/ADR-02-Superficie-Publica-De-Guardas-Y-Resultados-Tipados.md) | La superficie pública son guardas con resultado tipado, no excepciones | Estilo | Propuesto |
| [ADR-03](Adrs/ADR-03-Versionado-Y-Estabilidad-De-La-Superficie.md) | Versionado por versionado semántico sin publicación, y estabilidad de la superficie | Despliegue | Propuesto |
| [ADR-04](Adrs/ADR-04-Frontera-De-Autenticacion-Y-Autorizacion.md) | Frontera de autenticación: el dominio modela la condición y no el mecanismo | Seguridad | Propuesto |
| [ADR-05](Adrs/ADR-05-Guarda-Unica-De-Admisibilidad.md) | Puerta única de admisibilidad para las guardas de acceso de la cuenta | Seguridad | Propuesto |
| [ADR-06](Adrs/ADR-06-El-Dominio-No-Lee-El-Reloj-Ni-El-Conjunto.md) | El dominio no lee el reloj ni el conjunto de entidades: los dos entran por parámetro | Estilo | Propuesto |

Ninguna superada, ninguna rechazada.

## 4. NFR vigentes

Los seis, con su objetivo numérico y su mecanismo, están en [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) §8. En una línea: batería del dominio por debajo de **10 s**, cobertura **90 %** de líneas y **85 %** de ramas —los tres primeros rotulados `[ASUNCIÓN]` por el intake—, **0** dependencias salientes, **100 %** de las 42 condiciones y de los 9 invariantes ejercitados, y **0** advertencias de construcción.

## 5. Orden de lectura sugerido

1. [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) §2 y §3 — el estilo y los cinco componentes. Sin esto, el resto se lee como documentación de una biblioteca de utilidades, que es lo que este proyecto de código no es.
2. [`Adrs/ADR-01`](Adrs/ADR-01-Modelo-De-Dominio-Rico-Con-Invariantes-Explicitas.md) — la decisión de la que dependen todas las demás.
3. [`Contratos-Abstractions.md`](Contratos-Abstractions.md) §3 — las trece operaciones y, sobre todo, la columna de lo que cada una **exige resuelto** por el consumidor. Es la frontera del proyecto de código en forma de tabla.
4. [`Adrs/ADR-04`](Adrs/ADR-04-Frontera-De-Autenticacion-Y-Autorizacion.md) y [`ADR-05`](Adrs/ADR-05-Guarda-Unica-De-Admisibilidad.md) — juntas, porque la segunda es la forma concreta que toma la primera.
5. [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) §10 — la trazabilidad, para consultar por regla o por invariante.

## 6. Artefactos omitidos y su motivo

| Artefacto | Estado | Motivo |
| --- | --- | --- |
| `Modelo-Datos-Logico.md` | **Omitido** | La regla de la categoría lo omite para `library` puro sin estado. `tiene_persistencia` es false y el intake declara «no aplica» en §17.1.P.4. El modelo lógico que refleja a estas cinco entidades le corresponde a la categoría 05 de `GeometriaFactory-Infrastructure` |
| `Flujo-Ejecucion.md` | **Omitido** | La regla lo recomienda para `library` **con motor de procesamiento**, y este proyecto de código no lo tiene: el motor del producto es el validador de figuras, que vive detrás de un puerto de `GeometriaFactory-Application` y se implementa en `GeometriaFactory-Infrastructure` (`Definicion-Modelo-De-Dominio.md` §7). Acá no hay canalización que documentar: cada operación es una guarda que acepta o rechaza |
| `Extensibilidad.md` | **Omitido** | `tiene_extensibilidad` es false en el `PRODUCT-MANIFEST` §5. El punto de extensión declarado del producto es el contrato de la fachada del visor, no este proyecto de código |
| `_legacy/` | **No existe** | Es la primera emisión de esta categoría en este proyecto de código: no hay ninguna versión superada que archivar |

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Índice navegable de la sección: los cuatro documentos vigentes, las seis ADR con su estado, los NFR en una línea, el orden de lectura de cinco pasos y los cuatro artefactos omitidos con su motivo. |
