# Estrategia de versionado — GeometriaFactory-Visor

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Visor
**Documento:** Estrategia-Versionado.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero DevOps Senior + Release Engineer (AG-09)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../05-Arquitectura-Tecnica/Adrs/ADR-12006-Bundle-Generado-Y-Versionado-Del-Punto-De-Extension.md`](../../../05-Arquitectura-Tecnica/Adrs/ADR-12006-Bundle-Generado-Y-Versionado-Del-Punto-De-Extension.md) 1.0; [`../05-Arquitectura-Tecnica/Extensibilidad.md`](../../../05-Arquitectura-Tecnica/Extensibilidad.md); [`../08-Calidad-Y-Pruebas/Criterios-Validacion.md`](../../../08-Calidad-Y-Pruebas/_fusion/Visor/Criterios-Validacion.md) 1.0 §6; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.20** §10, §13, §15, §17.1.P.7 · GeometriaFactory-Domain, §17.2.P.3 · GeometriaFactory-Visor, §17.2.P.7 · GeometriaFactory-Visor y §18
**Trazabilidad downstream:** [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md), [`Entornos-Deploy.md`](Entornos-Deploy.md), [`Guia-Publicacion-Bundle-Visor.md`](../../Guia-Publicacion-Bundle-Visor.md)

---

## Tabla de contenido

- [1. Versionado semántico](#1-versionado-semántico)
- [2. Convenciones de mensaje de confirmación](#2-convenciones-de-mensaje-de-confirmación)
- [3. Herramienta de cálculo de la versión](#3-herramienta-de-cálculo-de-la-versión)
- [4. Modelo de ramas](#4-modelo-de-ramas)
- [5. Canales](#5-canales)
- [6. Política de crecimiento del punto de extensión](#6-política-de-crecimiento-del-punto-de-extensión)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Versionado semántico

Se adopta el **versionado semántico 2.0.0** en el archivo de manifiesto del paquete, junto con las convenciones de mensaje de confirmación, igual que el resto del producto (intake §17.2.P.7 · GeometriaFactory-Visor).

**Qué gobierna la versión acá, y por qué es distinto de los otros dos proyectos de código de nivel topológico 0.** [`ADR-12006`](../../../05-Arquitectura-Tecnica/Adrs/ADR-12006-Bundle-Generado-Y-Versionado-Del-Punto-De-Extension.md) §2 lo decide: gobierna **la superficie pública del punto de extensión** —las **seis** funciones, las **siete** garantías y los **siete** códigos de condición—, que es el punto de extensión declarado del producto (intake §18) y el único proyecto de código con `tiene_extensibilidad` en true.

Y hay una asimetría que ordena todo lo demás, declarada en `ADR-12006` §1: **el anfitrión no compila contra este artefacto**. Lo carga en el navegador e invoca sus funciones por interoperabilidad, de modo que **un cambio incompatible no rompe ninguna compilación: se manifiesta en tiempo de ejecución**.

Criterio de clase de cambio, transcripto de `ADR-12006` §7 **sin agregarle ni quitarle nada**:

| Clase | Qué la produce | ¿Lo detecta una compilación? |
| --- | --- | --- |
| **Mayor** | Quitar una función, renombrarla o cambiar qué recibe: rompe al anfitrión y al sample S-1 | No |
| **Mayor** | **Perder cualquiera de las siete garantías**, aunque las seis firmas no se toquen | No |
| **Mayor** | Cambiar la semántica de una entrada ya declarada del resultado de dibujo | No |
| **Menor** | Agregar una función. Así entró la sexta, sin romper a ningún anfitrión escrito contra las cinco anteriores | — |
| **Menor** | Agregar una entrada nueva al resultado de dibujo, conservando la semántica de las declaradas | — |
| **Menor** | Agregar un código de condición, que sólo puede nacer en la categoría 02 | — |
| **Sin efecto de contrato** | Cambiar la forma interna del identificador de instancia, mientras siga siendo opaco y cumpla sus tres propiedades semánticas. Que el anfitrión dependa de su forma es un defecto del anfitrión | — |
| **Parche** | Corregir el interior de la capa 3 sin cambiar la superficie ni las garantías | — |

**Ninguna de las tres clases mayores la detecta una compilación**, y es la diferencia operativa más importante frente a `GeometriaFactory-Domain` y `GeometriaFactory-Contracts`, donde al menos una clase mayor se manifiesta al construir. La mitigación que `ADR-12006` §2 declara es **la revisión más el sample S-1**, que ejerce el contrato entero sin ninguna pieza del backend, y esta categoría la hace operativa en [`Guia-Publicacion-Bundle-Visor.md`](../../Guia-Publicacion-Bundle-Visor.md) §3.

## 2. Convenciones de mensaje de confirmación

**Conventional Commits 1.0.0**, con el mismo efecto sobre la versión que en el resto del producto:

| Prefijo del mensaje | Efecto sobre la versión |
| --- | --- |
| `feat` | Sube **MINOR** |
| `fix` | Sube **PATCH** |
| `feat!`, o `BREAKING CHANGE` en el pie del mensaje | Sube **MAJOR** |
| `refactor`, `perf`, `test`, `chore`, `docs`, `style`, `build`, `ci` | No sube nada |

**Regla propia, y es la consecuencia directa de §1**: como **ninguna** clase mayor la detecta una compilación, el marcador de cambio incompatible se escribe **porque el criterio de `ADR-12006` §7 dice que corresponde**, y nunca porque algo se haya roto al construir. En particular, **perder una garantía es cambio mayor aunque las seis firmas queden intactas y el bundle compile y dibuje**: el archivo de confirmación es el único lugar donde eso se declara antes de que un anfitrión lo descubra en ejecución.

## 3. Herramienta de cálculo de la versión

**Se declara por su función**, como en el resto del producto: el intake §17.1.P.7 · GeometriaFactory-Domain —al que §17.2.P.7 · GeometriaFactory-Visor se alinea— ata la elección al anclaje de la etapa `a`, y `ADR-12006` §6 acepta explícitamente que **la versión no la verifique ninguna herramienta** y que sea una convención sostenida por disciplina.

| Aspecto | Decisión |
| --- | --- |
| Función | Calcular la versión desde las etiquetas del repositorio y los mensajes de confirmación desde la última etiqueta, y reflejarla en el manifiesto del paquete |
| Qué **no** puede calcular ninguna herramienta | La pérdida de una garantía y el cambio de semántica de una entrada del resultado de dibujo. Los dos son cambios mayores que no dejan rastro en ninguna firma |
| Qué lo sustituye | La revisión del pull request de la etapa —que **es** el punto de control— y la batería de la categoría 08 sobre las **siete** garantías, con objetivo **7 de 7** verificadas antes de fusionar |

Las tres filas se apoyan en `ADR-12006` §6 y §8.

## 4. Modelo de ramas

El del producto, sin variantes: una rama por etapa a partir de la principal, etiqueta al fusionar, un pull request por etapa que **es** el punto de control, etapas en serie y sin OK explícito no se avanza (intake §10, §15 y §17.1.P.7 · GeometriaFactory-Domain; `ADR-12006` §7, primera viñeta).

**Los momentos de este proyecto de código no son sólo etapas**, y el modelo de ramas tiene que convivir con eso. [`../08-Calidad-Y-Pruebas/Plan-Pruebas.md`](../../../08-Calidad-Y-Pruebas/_fusion/Visor/Plan-Pruebas.md) §1 declara **tres** momentos: la etapa `a`, el **momento de medición de `PT-02` y `PT-03`** —que no es una etapa y no crea una nueva— y la etapa `g`. La consecuencia para esta categoría es que **la medición de las dos puertas no espera a la rama de la etapa `g`**: si esperara, mediría después de comprometerla, que es justo lo que el intake §15 prohíbe al declarar que una puerta que no pasa **detiene la planificación** de lo que depende de ella.

**Reglas de protección de la rama principal:** los gates bloqueantes de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.1 en verde, incluidas las tres inspecciones **sobre el bundle generado**, y la constancia del OK del punto de control.

## 5. Canales

**No hay canales de publicación.** El intake §17.2.P.7 · GeometriaFactory-Visor declara que **no se publica** en ningún repositorio de paquetes del ecosistema del navegador, y [`ADR-12006`](../../../05-Arquitectura-Tecnica/Adrs/ADR-12006-Bundle-Generado-Y-Versionado-Del-Punto-De-Extension.md) §4 descartó la alternativa con su fundamento. El apartamiento frente a `Rules-Devops.md` §2.2 queda registrado en [`Entornos-Deploy.md`](Entornos-Deploy.md) §1.1.

**Sin sufijos de anticipo.** No hay canal donde publicar un anticipo del punto de extensión ni integrador que lo consuma; el anfitrión carga el archivo que la construcción produjo.

**Y una consecuencia de la resolución de `PA-05`**: como el bundle **no se versiona en el repositorio** ([`Entornos-Deploy.md`](Entornos-Deploy.md) §2), no existe la figura de «la versión del bundle que está en el repositorio». La versión que importa es la del **estado del fuente**, y el artefacto se regenera desde ahí. Es lo que hace verificable la métrica de reproducibilidad de `ADR-12006` §8: dos construcciones desde el mismo estado producen el mismo artefacto.

## 6. Política de crecimiento del punto de extensión

Reemplaza a la política de obsolescencia de `Rules-Devops.md` §4.3, y el reemplazo tiene fundamento: no hay integrador externo a quien dar plazo de migración —el único anfitrión es `GeometriaFactory-Web`, del mismo producto—. Lo que sí hay, y es más exigente que un plazo, es un **procedimiento para que la superficie crezca**.

| Obligación | Cómo se verifica | Fundamento |
| --- | --- | --- |
| Una función nueva en la fachada recorre **los seis pasos** de [`../05-Arquitectura-Tecnica/Extensibilidad.md`](../../../05-Arquitectura-Tecnica/Extensibilidad.md) §5 **enteros**, incluida la consolidación en el intake | Criterio de salida de [`../08-Calidad-Y-Pruebas/Plan-Pruebas.md`](../../../08-Calidad-Y-Pruebas/_fusion/Visor/Plan-Pruebas.md) §3, y Definition of Done §1.3 | `08` y `05` |
| Un código de condición **sólo puede nacer en la categoría 02**; ninguno se acuña aguas abajo | `QG-08`, con `TC-12021`, comparando en las dos direcciones | `08` `Estrategia-Calidad.md` §3 |
| **Perder una garantía es cambio mayor**, y las **siete** se verifican antes de fusionar | Objetivo **7 de 7** | `ADR-12006` §7 y §8 |
| El bundle **nunca se edita a mano**; objetivo: exactamente **0** ediciones manuales | `QG-09` y `CV-30` | `ADR-12006` §8; `08` |
| Todo cambio mayor recibe su fila en el registro de cambios del producto; objetivo: **0** cambios mayores sin registro | Revisión del pull request de la etapa | `ADR-12006` §8 |

**El antecedente que muestra que el procedimiento funciona ya ocurrió**: la sexta función de la fachada entró como cambio menor **sin romper a ningún anfitrión escrito contra las cinco anteriores** (`ADR-12006` §6, punto 3, y §7). El intake la consolidó en §17.2.P.3 · GeometriaFactory-Visor en su versión 1.6. Es el recorrido completo de los seis pasos, hecho una vez y registrado.

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Adopta el versionado semántico 2.0.0 y las Conventional Commits 1.0.0, y transcribe el criterio de clase de cambio de `ADR-12006` §7 **con su columna de qué detecta una compilación**, dejando a la vista que **ninguna de las tres clases mayores la detecta**, que es la asimetría de este proyecto de código frente a los otros dos de nivel topológico 0. Declara la herramienta por su función y precisa qué **ninguna** herramienta puede calcular. Declara el modelo de ramas del producto con la precisión de que **la medición de las dos puertas no espera a la rama de la etapa `g`**. Declara la ausencia de canales, y que tras resolverse `PA-05` no existe «la versión del bundle que está en el repositorio»: existe el estado del fuente y el artefacto se regenera. Reemplaza la política de obsolescencia por la **política de crecimiento del punto de extensión**, con **cinco** obligaciones y el antecedente de la sexta función. |
