# Estrategia de versionado — GeometriaFactory-Infrastructure

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** Estrategia-Versionado.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero DevOps Senior + Release Engineer (AG-09)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/_fusion/Infrastructure/Arquitectura-Proyecto-Codigo.md) §5 y §11; [`../05-Arquitectura-Tecnica/Adrs/ADR-06007-Transformaciones-Al-Arrancar-Con-Linaje-Inmutable.md`](../../../05-Arquitectura-Tecnica/Adrs/ADR-06007-Transformaciones-Al-Arrancar-Con-Linaje-Inmutable.md); [`../05-Arquitectura-Tecnica/Adrs/ADR-06004-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md`](../../../05-Arquitectura-Tecnica/Adrs/ADR-06004-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md); [`../08-Calidad-Y-Pruebas/Definition-Of-Done.md`](../../../08-Calidad-Y-Pruebas/Definition-Of-Done.md) 1.1 §1.3; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.21** §10, §13, §15, §17.1.P.7 · GeometriaFactory-Domain, §17.1.P.4 · GeometriaFactory-Infrastructure y §17.1.P.7 · GeometriaFactory-Infrastructure
**Trazabilidad downstream:** [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md), [`Entornos-Deploy.md`](Entornos-Deploy.md)

---

## Tabla de contenido

- [1. Versionado semántico](#1-versionado-semántico)
- [2. Convenciones de mensaje de confirmación](#2-convenciones-de-mensaje-de-confirmación)
- [3. Herramienta de cálculo de la versión](#3-herramienta-de-cálculo-de-la-versión)
- [4. Los dos linajes que este proyecto de código versiona además del suyo](#4-los-dos-linajes-que-este-proyecto-de-código-versiona-además-del-suyo)
- [5. Modelo de ramas](#5-modelo-de-ramas)
- [6. Canales y política de cambios incompatibles](#6-canales-y-política-de-cambios-incompatibles)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Versionado semántico

Se adopta el **versionado semántico 2.0.0**, con el formato `MAJOR.MINOR.PATCH[-PRERELEASE][+BUILDMETADATA]`. El intake §17.1.P.7 · GeometriaFactory-Infrastructure declara la estrategia **idéntica a la de §17.1.P.7 · GeometriaFactory-Domain** —versionado semántico, convenciones de mensaje, sin publicación en feed, una rama y una etiqueta por etapa— **y le agrega una obligación propia**, que es el eje de este documento: **cada transformación de esquema se versiona con el código de su etapa, y no se editan transformaciones ya fusionadas**.

**Qué gobierna la compatibilidad de la superficie de código.** Este proyecto de código **implementa** los cuatro puertos que `GeometriaFactory-Application` declara, y su único consumidor es la composición de raíz de `GeometriaFactory-Api` (intake §14). La compatibilidad se protege por **compilación compartida**: un cambio incompatible rompe la construcción del artefacto de agrupación antes que la ejecución.

| Clase de cambio sobre la superficie de código | Ejemplo | ¿Lo detecta la compilación? |
| --- | --- | --- |
| **Mayor** | Un adaptador deja de implementar una operación del puerto que declara | Sí |
| **Mayor** | Cambia el comportamiento observable de un adaptador sin cambiar su firma: una consulta de listado empieza a cargar componentes de pieza | **No.** Lo detecta `QG-10`, con umbral **0** |
| **Mayor** | Cambia lo que se conserva del texto original del alumno | **No.** Lo detecta `QG-11`, con umbral **0** |
| **Menor** | Se agrega un adaptador para un puerto nuevo que la capa de aplicación declaró | Sí, si falta |
| **Parche** | Se corrige un adaptador para que cumpla lo que ya declaraba | — |

**Las dos filas del medio son las que importan acá.** Son cambios mayores **que compilan**, y las dos tocan lo que la fuente protege con más fuerza: la regla de no cargar componentes en los listados (intake §17.1.P.12 · GeometriaFactory-Infrastructure) y la conservación íntegra del texto original (`RN-06008`, intake §17.1.P.11 · GeometriaFactory-Infrastructure punto 2). Ninguna herramienta de comparación de superficie las vería; las ven `QG-10` y `QG-11`, y por eso son gates.

## 2. Convenciones de mensaje de confirmación

Se adoptan las **Conventional Commits 1.0.0**, con el mismo efecto sobre la versión que en el resto del producto:

| Prefijo del mensaje | Efecto sobre la versión |
| --- | --- |
| `feat` | Sube **MINOR** |
| `fix` | Sube **PATCH** |
| `feat!`, o `BREAKING CHANGE` en el pie del mensaje | Sube **MAJOR** |
| `refactor`, `perf`, `test`, `chore`, `docs`, `style`, `build`, `ci` | No sube nada |

**Precisión propia de este proyecto de código.** Toda confirmación que **agregue una transformación de esquema** queda atada a la etapa en la que entra, por la obligación de §1. En la práctica eso significa que el mensaje nombra la etapa, y que **una transformación no viaja sola a una rama de otra etapa**: sería un linaje distinto del que se aplicó en cualquier almacén ya existente.

## 3. Herramienta de cálculo de la versión

**Se declara por su función, y esta categoría no la elige**: el intake §17.1.P.7 · GeometriaFactory-Infrastructure remite a §17.1.P.7 · GeometriaFactory-Domain, que la ancla en la etapa `a`, y ninguna fuente la nombra.

| Aspecto | Decisión |
| --- | --- |
| Función | Calcular la versión desde las etiquetas del repositorio y los mensajes de confirmación desde la última etiqueta |
| Prefijo de etiqueta | El que se fije al anclarla, registrado en el punto de control de la etapa `a` |
| Qué **no** calcula la herramienta | **Las dos clases mayores que compilan** de §1, y **el linaje de transformaciones**, que no es una versión semántica sino una secuencia ordenada |

**Y dos versiones que se anclan y no se calculan**, las dos con efecto sobre la ejecución y no sobre el número de versión de este ensamblado:

| Qué se ancla | Dónde vive el anclaje | Fundamento |
| --- | --- | --- |
| La **herramienta de transformaciones de esquema**, instalada como **herramienta local del repositorio** para que su versión quede versionada junto al código | El archivo de herramientas del repositorio, anclado en la etapa `a` | Intake §17.1.P.1 · GeometriaFactory-Infrastructure |
| El **motor de almacenamiento en su versión embebida** por el proveedor de acceso a datos | El archivo de proyecto, anclado en la etapa `a` | Intake §17.1.P.9 · GeometriaFactory-Infrastructure |

## 4. Los dos linajes que este proyecto de código versiona además del suyo

Es lo que distingue a este documento de los de las otras cuatro bibliotecas del producto: **acá hay dos secuencias que sobreviven al despliegue y que no son la versión del ensamblado**.

| Linaje | Qué es | Regla que lo gobierna | Qué pasa si se rompe |
| --- | --- | --- | --- |
| **Transformaciones de esquema** | La secuencia ordenada que lleva un almacén desde inexistente hasta el esquema en uso | **Se versiona con el código de su etapa y no se edita una ya fusionada** (intake §17.1.P.7 · GeometriaFactory-Infrastructure); el linaje es **inmutable** ([`ADR-06007`](../../../05-Arquitectura-Tecnica/Adrs/ADR-06007-Transformaciones-Al-Arrancar-Con-Linaje-Inmutable.md)) | Un almacén existente tiene aplicado un linaje que ya no coincide con el del código. **Volver a una etiqueta anterior no lo deshace**: el esquema del almacén no se recompila |
| **Parámetros de la derivación de clave** | Los parámetros con los que se derivó cada contraseña guardada | **Se versionan junto al valor derivado, sin valor por defecto silencioso** ([`ADR-06004`](../../../05-Arquitectura-Tecnica/Adrs/ADR-06004-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md)) | Un cambio de parámetros dejaría sin verificar las contraseñas ya guardadas si no se conservara con qué se derivó cada una |

**Los dos son la razón por la que la reversión de este proyecto de código no es simétrica.** [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §7 lo declara: volver a la etiqueta anterior revierte el código, **no el almacén**. Una transformación equivocada se corrige **con otra transformación**, nunca editando la anterior; y el guion de restablecimiento, que sí deja el almacén como en el primer arranque, **no es un camino de producción** (`05` §5).

**Y una consecuencia que el producto ya declaró y esta categoría no reabre**: el intake §17.1.P.4 · GeometriaFactory-Infrastructure declara el respaldo como **copia del archivo con el diario activo**, consistente, con **frecuencia a definir por el docente**. Es el único mecanismo declarado para volver atrás sobre datos, y su cadencia **no la fija esta categoría**.

## 5. Modelo de ramas

El del producto, heredado entero y sin variantes: **una rama por etapa** a partir de la principal, con etiqueta al fusionar; **un pull request por etapa, que es el punto de control**; **etapas en serie**; y sin OK explícito no se avanza (intake §10, §15 y §17.1.P.7 · GeometriaFactory-Domain).

**Reglas de protección de la rama principal**, que es lo que esta categoría aporta:

- La fusión exige los gates bloqueantes de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.1.
- **Todo pull request que agregue o cambie una transformación de esquema ejecuta el stage `verificar-transformaciones` sobre un almacén inexistente y sobre el linaje completo**, y no sólo sobre la transformación nueva. Es la cadencia propia de este proyecto de código.
- **Ninguna fusión edita una transformación ya fusionada.** Se rechaza en revisión, y su fundamento es de la fuente y no de esta categoría.
- No se exige revisor humano independiente: `equipo_n` es 1 y el filtro es el punto de control bloqueante.

**Las etapas que este proyecto de código toca son cinco** —`a`, `c`, `d`, `e` y `f`—, según [`../06-Backlog-Tecnico/Product-Backlog.md`](../../../06-Backlog-Tecnico/_fusion/Infrastructure/Product-Backlog.md) §2, citado por [`../08-Calidad-Y-Pruebas/README.md`](../../../08-Calidad-Y-Pruebas/README.md) §5.

## 6. Canales y política de cambios incompatibles

**No hay canales de publicación.** El intake §17.1.P.7 · GeometriaFactory-Infrastructure, por remisión a §17.1.P.7 · GeometriaFactory-Domain, declara que no se publica en ningún feed, y §13 lo generaliza al producto. `05` §5 lo repite en su última fila. `Rules-Devops.md` §2.2 fija para el tipo `library` el modelo `preview` / `stable` sobre feed único; el apartamiento queda desarrollado en [`Entornos-Deploy.md`](Entornos-Deploy.md) §1. **Tampoco se usan sufijos de anticipo**: no hay canal donde publicar uno ni integrador que lo consuma.

Esta sección reemplaza además a la política de obsolescencia que `Rules-Devops.md` §4.3 pide, con el mismo fundamento que en el resto del producto —**no hay integrador externo a quien dar plazo**— y con las obligaciones que sí rigen:

| Obligación | Cómo se verifica | Fundamento |
| --- | --- | --- |
| **Ninguna transformación ya fusionada se edita** | Revisión del pull request de la etapa | Intake §17.1.P.7 · GeometriaFactory-Infrastructure; [`ADR-06007`](../../../05-Arquitectura-Tecnica/Adrs/ADR-06007-Transformaciones-Al-Arrancar-Con-Linaje-Inmutable.md) |
| Las transformaciones **se aplican solas sobre un almacén inexistente**, sin paso manual | `QG-04`, en el stage `verificar-transformaciones` | Intake §17.1.P.8 · GeometriaFactory-Infrastructure, criterio de aceptación de la etapa `c` |
| **0** advertencias de construcción | `QG-01`, en `build` | Intake §17.1.P.8 · GeometriaFactory-Infrastructure |
| **0** componentes de pieza y **0** apariciones del texto original en una proyección de listado | `QG-10`, con `TC-06019` | Es una de las dos clases mayores que compilan (§1) |
| **0** escrituras que reemplacen el texto original conservado | `QG-11`, con `TC-06016` y `TC-06021` | La otra clase mayor que compila (§1) |
| **0** etapas cerradas sin etiqueta | Inspección del historial contra el índice de informes de cierre | Intake §15 y §17.1.P.7 · GeometriaFactory-Infrastructure |
| Todo cambio mayor recibe su fila en el registro de cambios del producto | Revisión del pull request, que **es** el punto de control | Intake §15, regla de delivery 3 |
| Los parámetros de derivación **viajan junto al valor derivado**, sin valor por defecto silencioso | Revisión, contra [`ADR-06004`](../../../05-Arquitectura-Tecnica/Adrs/ADR-06004-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md) | El mismo ADR |

**La primera fila es la única obligación de versionado de todo el producto que alcanza a un dato que sobrevive al código.** Las demás protegen la construcción o la ejecución; ésa protege **almacenes que ya existen y que ninguna canalización toca**.

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Adopta el versionado semántico 2.0.0 y las Conventional Commits 1.0.0 que el intake §17.1.P.7 · GeometriaFactory-Infrastructure hereda de §17.1.P.7 · GeometriaFactory-Domain, **con la obligación propia que esa sección agrega**: cada transformación de esquema se versiona con el código de su etapa y no se edita una ya fusionada. Declara las clases de cambio de la superficie de código con la columna de qué detecta la compilación, dejando a la vista **dos clases mayores que compilan** —cargar componentes en un listado y alterar el texto original conservado— y los gates que las ven. Declara la herramienta de cálculo por su función sin elegirla, y **dos versiones que se anclan y no se calculan**. Declara los **dos linajes** que este proyecto de código versiona además del suyo —transformaciones de esquema y parámetros de derivación de clave— y por qué hacen que su reversión **no sea simétrica**: volver a una etiqueta revierte el código y no el almacén. Declara el modelo de ramas con la cadencia propia, la ausencia de canales, y **ocho** obligaciones en lugar de una política de obsolescencia. |
