> **Artefacto archivado — estado `Superado`**
>
> Esta es una **copia archivada** del documento `README.md` en su versión **1.0**, tomada el 2026-08-09 por el orquestador SDD antes de que la versión vigente la superara (`Master-Prompt.md` §5 y §5.1).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-09
> - **Versión vigente:** [`README.md`](../../README.md)
>
> El cuerpo que sigue **no se modifica**: un registro que se corrige después deja de ser un registro. Este archivo no se renombra, no se reenlaza y no vuelve a tocarse.

---

# 02 · Especificación funcional — GeometriaFactory-Domain

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** README.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-08
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`Especificacion-Funcional.md`](Especificacion-Funcional.md) (índice maestro de esta categoría); `01-Necesidades-Negocio/Necesidades-Negocio.md`; `00-Contexto/Vision-Producto.md` §9
**Trazabilidad downstream:** `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico` y `08-Calidad-Y-Pruebas` de GeometriaFactory-Domain

---

## Tabla de contenido

- [1. Qué hay en esta carpeta](#1-qué-hay-en-esta-carpeta)
- [2. Los nueve casos de uso](#2-los-nueve-casos-de-uso)
- [3. Las siete reglas de negocio](#3-las-siete-reglas-de-negocio)
- [4. Orden de lectura sugerido](#4-orden-de-lectura-sugerido)
- [5. Artefactos omitidos y su motivo](#5-artefactos-omitidos-y-su-motivo)
- [6. Notas de uso de esta sección](#6-notas-de-uso-de-esta-sección)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Qué hay en esta carpeta

| Documento | Propósito | Estado |
| --- | --- | --- |
| [`Especificacion-Funcional.md`](Especificacion-Funcional.md) | Índice maestro: catálogos, matriz NB → CU → RN → US, criterio de recorte, omisiones y puntos abiertos. **Es el punto de entrada** | Propuesto |
| [`Definicion-Modelo-De-Dominio.md`](Definicion-Modelo-De-Dominio.md) | Documento de concepto central: cinco entidades, invariantes, máquinas de estado y fronteras del dominio | Propuesto |
| [`Glosario-Funcional.md`](Glosario-Funcional.md) | Vocabulario que esta categoría acuña y términos con más de un referente | Propuesto |
| `Casos-De-Uso/` | Nueve casos de uso, uno por archivo | Propuesto |
| `Reglas-De-Negocio/` | Siete reglas de negocio, una por archivo | Propuesto |
| `README.md` | Este archivo: índice navegable, orden de lectura y omisiones | Propuesto |

## 2. Los nueve casos de uso

Todos describen un **contrato de uso de la superficie pública**. El actor primario es siempre el proyecto de código que consume la biblioteca; el alumno y el administrador son sujetos de las reglas, no actores.

| CU | Título | NB que implementa |
| --- | --- | --- |
| CU-01 | [Registrar el alta de un alumno](Casos-De-Uso/CU-01-Registrar-El-Alta-De-Un-Alumno.md) | NB-02, NB-01 |
| CU-02 | [Gobernar el ciclo de vida de la cuenta del alumno](Casos-De-Uso/CU-02-Gobernar-El-Ciclo-De-Vida-De-La-Cuenta.md) | NB-01 |
| CU-03 | [Fijar y reemplazar la credencial derivada](Casos-De-Uso/CU-03-Fijar-Y-Reemplazar-La-Credencial-Derivada.md) | NB-02 |
| CU-04 | [Evaluar la admisibilidad de la cuenta](Casos-De-Uso/CU-04-Evaluar-La-Admisibilidad-De-La-Cuenta.md) | NB-01, NB-02 |
| CU-05 | [Crear y reeditar un trabajo](Casos-De-Uso/CU-05-Crear-Y-Reeditar-Un-Trabajo.md) | NB-03, NB-04 |
| CU-06 | [Reconstruir el conjunto de piezas del trabajo](Casos-De-Uso/CU-06-Reconstruir-El-Conjunto-De-Piezas-Del-Trabajo.md) | NB-04, NB-06 |
| CU-07 | [Registrar las observaciones del trabajo](Casos-De-Uso/CU-07-Registrar-Las-Observaciones-Del-Trabajo.md) | NB-05, NB-04 |
| CU-08 | [Gobernar el estado del trabajo](Casos-De-Uso/CU-08-Gobernar-El-Estado-Del-Trabajo.md) | NB-03, NB-04, NB-05 |
| CU-09 | [Resolver el acceso de un alumno a un trabajo](Casos-De-Uso/CU-09-Resolver-El-Acceso-Del-Alumno-A-Un-Trabajo.md) | NB-03 |

## 3. Las siete reglas de negocio

| RN | Título | Invariante que materializa |
| --- | --- | --- |
| RN-01 | [Administrador único y papeles fijos](Reglas-De-Negocio/RN-01-Administrador-Unico-Y-Papeles-Fijos.md) | INV-05 |
| RN-03 | [Un trabajo ajeno es indistinguible de uno inexistente](Reglas-De-Negocio/RN-03-Trabajo-Ajeno-Indistinguible-De-Inexistente.md) | INV-02 |
| RN-04 | [La eliminación de un trabajo está acotada al borrador](Reglas-De-Negocio/RN-04-Eliminacion-Acotada-Al-Borrador.md) | — |
| RN-05 | [Un trabajo no se finaliza con errores de validación](Reglas-De-Negocio/RN-05-Finalizacion-Sin-Errores-De-Validacion.md) | — |
| RN-07 | [La baja arrastra los trabajos y exige confirmación escrita](Reglas-De-Negocio/RN-07-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md) | — |
| RN-08 | [El texto original del alumno se conserva íntegro](Reglas-De-Negocio/RN-08-Texto-Original-Conservado-Integro.md) | INV-04 |
| RN-09 | [Toda observación de error indica la posición de la pieza y el campo](Reglas-De-Negocio/RN-09-Observacion-De-Error-Con-Posicion-Y-Campo.md) | — |

**La serie no es contigua a propósito:** faltan RN-02 y RN-06 porque el intake no transcribe su enunciado, y la numeración se conserva para no romper la trazabilidad con las fuentes. La causa está declarada en `Especificacion-Funcional.md` §8 y §9.

## 4. Orden de lectura sugerido

1. **`Especificacion-Funcional.md`** — primero siempre: da el alcance, la matriz y el criterio de recorte.
2. **`Definicion-Modelo-De-Dominio.md`** — las entidades, los invariantes y las dos máquinas de estado. Los nueve casos de uso se leen sobre él.
3. **CU-01 a CU-04** — el ciclo de vida de la cuenta y su admisibilidad.
4. **CU-05 a CU-08** — el ciclo de vida del trabajo: constitución, interpretación, observaciones y estado.
5. **CU-09** — la pertenencia, que atraviesa a los anteriores.
6. **`Reglas-De-Negocio/`** — se leen sueltas, en cualquier orden: son invariantes atemporales y cada una declara los casos de uso que alcanza.
7. **`Glosario-Funcional.md`** — conviene tenerlo a mano desde el principio si el lector viene de otra categoría, sobre todo por la desambiguación de `Pendiente`.

## 5. Artefactos omitidos y su motivo

| Artefacto | Motivo de la omisión |
| --- | --- |
| `Modelo-Datos/Modelo-Conceptual.md` | La regla de la categoría lo omite para el tipo `library` y el flag `tiene_persistencia` de este proyecto de código es false. El intake declara «no aplica» en §17.1.P.4: el dominio no conoce el motor de persistencia, que materializa `GeometriaFactory-Infrastructure`. El concepto central se documenta en `Definicion-Modelo-De-Dominio.md`, que **no** es un modelo de persistencia |
| `Modelo-Datos/reglas-conceptuales-de-modelo/RC-XX-<Nombre>.md` | La regla las omite para `library` y dependen del modelo conceptual, que está omitido. Las restricciones de integridad del dominio están declaradas como invariantes y como reglas de negocio |
| `Casos-De-Uso/_legacy/` y `Reglas-De-Negocio/_legacy/` | Emisión inicial: no hay versiones superadas que archivar |

## 6. Notas de uso de esta sección

- **Autoridad.** Nada se origina acá. Toda regla, todo invariante y todo valor numérico traza a su sección del intake o de las categorías 00 y 01. Lo que el intake no declara, no se inventa: los cuatro puntos abiertos están listados en `Especificacion-Funcional.md` §9.
- **Ubicación de responsabilidades.** Un enunciado de esta categoría que mencione persistencia, protocolo de transporte, serialización o emisión de acceso está mal ubicado: esas responsabilidades pertenecen a otros proyectos de código y su tabla está en `Definicion-Modelo-De-Dominio.md` §7.
- **Decisiones de otras categorías.** Los nombres de tipos y de espacios de nombres son de 05 y de la codificación; el backlog es de 06; las pruebas, de 08. Esta categoría las referencia y no las toma.
- **Vocabulario.** Los términos del dominio no se redefinen: `Vision-Producto.md` §9 es el glosario raíz. La palabra «proyecto» a secas no se usa en ninguna documentación de este producto.
- **Nombres de archivo.** Ningún archivo vivo lleva sufijo de versión: cada uno declara su versión en el campo `Versión` de su cabecera.

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial del índice de la sección. Enumera los nueve casos de uso con la necesidad de negocio que implementan, las siete reglas de negocio con su invariante, el orden de lectura, y registra la omisión del modelo conceptual y de las reglas conceptuales con su motivo. |
