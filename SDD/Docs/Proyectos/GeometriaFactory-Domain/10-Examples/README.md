# 10 · Ejemplos — GeometriaFactory-Domain

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** README.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Developer Advocate / Sample Engineer Senior (AG-10)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../02-Especificacion-Funcional/`](../02-Especificacion-Funcional/), los **trece** casos de uso; [`../05-Arquitectura-Tecnica/Contratos-Abstractions.md`](../05-Arquitectura-Tecnica/Contratos-Abstractions.md), las **trece** operaciones; [`../06-Backlog-Tecnico/historias-usuario/`](../06-Backlog-Tecnico/historias-usuario/), las **veintisiete** historias; [`../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md`](../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md) 1.0, los **veintisiete** casos de prueba; `PRODUCT-INTAKE` 1.22 §16.1, §18 y §20
**Trazabilidad downstream:** [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), que toma las tres sondas `VER-XX`; `11-Documentacion` cuando se emita, que referencia estos samples sin duplicar su código

---

## Tabla de contenido

- [1. Qué hay en esta carpeta](#1-qué-hay-en-esta-carpeta)
- [2. Tabla maestra de samples](#2-tabla-maestra-de-samples)
- [3. Contratos de verificación](#3-contratos-de-verificación)
- [4. Convenciones de los samples](#4-convenciones-de-los-samples)
- [5. Estructura de `/samples` y su desvío declarado](#5-estructura-de-samples-y-su-desvío-declarado)
- [6. Cómo agregar un sample nuevo](#6-cómo-agregar-un-sample-nuevo)
- [7. Vínculo con 05 y con 11](#7-vínculo-con-05-y-con-11)
- [8. Control de cambios](#8-control-de-cambios)

---

## 1. Qué hay en esta carpeta

Tres markdown explicativos, uno por sample, con sus **diez** secciones obligatorias de `Rules-Examples.md` §4.2, y este índice. Cada markdown apunta a una carpeta ejecutable de `/samples/domain/` del repositorio, que esta pasada deja **esqueletada**: con su README local y su comando previsto, y sin corrida hecha.

Esta emisión es la **pasada de diseño** de `Rules-Examples.md` §0.2. Los tres contratos de verificación están completos salvo el campo `evidencia`, que dice `No verificado — sin código` en los tres. **Ninguna carpeta de `/samples` promete una corrida que no se hizo.**

**Los tres samples sirven a las dos aristas de `Rules-Examples.md` §0.1.** La arista A —referencia de integración— le habla al consumidor de esta biblioteca, que dentro de este producto es `GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`. La arista B —arnés de autovalidación— le habla al equipo que construye y a los agentes que codifican contra la especificación, y es la que aporta las sondas `VER-XX` de la matriz de sensado.

## 2. Tabla maestra de samples

| Sample | Nivel | Tiempo de setup | CU ilustrados | Ubicación |
| --- | --- | --- | --- | --- |
| [`ejemplo-01-basico.md`](ejemplo-01-basico.md) | Básico | < 5 min | CU-01, CU-02, CU-03, CU-04, CU-12 | `/samples/domain/01-basico/` |
| [`ejemplo-02-intermedio.md`](ejemplo-02-intermedio.md) | Intermedio | < 5 min | CU-05, CU-06, CU-07, CU-08 | `/samples/domain/02-intermedio/` |
| [`ejemplo-03-avanzado.md`](ejemplo-03-avanzado.md) | Avanzado | 10-15 min | CU-09, CU-10, CU-11, CU-13 | `/samples/domain/03-avanzado/` |

**Tres samples, el piso que `Rules-Examples.md` §2.2 fija para `library`.** El tiempo de setup del tercero es mayor porque agrega la lectura del archivo de proyecto y dos corridas consecutivas sin fijar el reloj.

**Cobertura de los trece casos de uso.** Los tres samples cubren **trece de trece**: `CU-01` a `CU-13`, sin repeticiones y sin huecos. Verificación uno por uno: `CU-01`, `CU-02`, `CU-03`, `CU-04` y `CU-12` en el 01; `CU-05`, `CU-06`, `CU-07` y `CU-08` en el 02; `CU-09`, `CU-10`, `CU-11` y `CU-13` en el 03.

**Cobertura de los ocho escenarios reales.** El sample 02 usa **seis** de los ocho del `PRODUCT-INTAKE` §20 —`E-1`, `E-3`, `E-4`, `E-5`, `E-6` y `E-8`—, transcriptos sin modificación. Los dos que no aparecen son `E-2` y `E-7`: `E-2` es el mismo ortoedro con volumen declarado incorrecto que `E-1` ya trae dentro, y su valor propio está en las dos trampas de formato del texto —clave `Tapas` y comas finales—, que son de la lectura del texto y no del dominio, que adopta la interpretación ya hecha; `E-7` ejercita los **seis** tipos dibujables, que es materia del proyecto de código que dibuja. Los dos sí están cubiertos por casos de prueba del proyecto de código: `E-7` en `TC-13` y `E-2` en `TC-17` de [`../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md`](../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md). **Ningún escenario se sustituye por datos sintéticos.**

## 3. Contratos de verificación

Vista de conjunto de la arista B, en el formato de `Rules-Examples.md` §4.4.

| Sonda | Sample | Verifica | Comando | Estado | Última corrida |
| --- | --- | --- | --- | --- | --- |
| `VER-01` | [`ejemplo-01-basico.md`](ejemplo-01-basico.md) | CU-01, CU-02, CU-03, CU-04, CU-12; US-01, US-04, US-06, US-24, US-27 | `dotnet run --project samples/domain/01-basico` | No verificado — sin código | — |
| `VER-02` | [`ejemplo-02-intermedio.md`](ejemplo-02-intermedio.md) | CU-05, CU-06, CU-07, CU-08; US-09 a US-16 | `dotnet run --project samples/domain/02-intermedio` | No verificado — sin código | — |
| `VER-03` | [`ejemplo-03-avanzado.md`](ejemplo-03-avanzado.md) | CU-09, CU-10, CU-11, CU-13; US-18 a US-23, US-26 | `dotnet run --project samples/domain/03-avanzado` | No verificado — sin código | — |

**Tres sondas, ninguna redundante**: los conjuntos de casos de uso que verifican son disjuntos. Las tres entran a [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md) con estado `Sin verificar`, que es lo que `Deriva-Rules.md` §2.4 declara para un contrato en `No verificado — sin código`.

**Qué queda fuera de las tres sondas, y por qué no es un hueco.** Las **42** condiciones del catálogo de `03`, los **nueve** invariantes ejercidos sin dobles y los umbrales de cobertura y de tiempo de la batería no los verifica ningún sample: los verifica la batería de `tests/GeometriaFactory.Domain.Tests` por `TC-23`, `TC-26` y el pipeline. Un sample que los duplicara sería el anti-patrón de `Rules-Examples.md` §4.5, «samples que duplican el `/src` sin agregar valor demostrativo».

## 4. Convenciones de los samples

- **Autocontenidos.** Ninguno requiere servicios externos: el proyecto de código declara **0** dependencias salientes (`PRODUCT-INTAKE` §17.1.P.1).
- **Ejecutables en entorno limpio en cuatro pasos**, dentro del entorno de desarrollo contenido del repositorio, que es donde ocurre todo el ciclo porque el host no tiene la plataforma (`PRODUCT-INTAKE`, encabezado de la Parte C).
- **Nivel declarado** en la §2 de cada markdown, y progresión por nivel y no por dominio.
- **Trazabilidad obligatoria** en la §8 de cada markdown, con al menos una fila por caso de uso, regla, ADR o NFR.
- **Criterio de aceptación evaluable por una máquina**: exit code más líneas exactas de salida. Ninguno está redactado como prosa.
- **Los datos son reales.** Los escenarios del `PRODUCT-INTAKE` §20 se transcriben sin modificación y no se sustituyen por datos sintéticos.
- **Los samples no acuñan vocabulario.** Todo término que usan está declarado en [`../02-Especificacion-Funcional/Glosario-Funcional.md`](../02-Especificacion-Funcional/Glosario-Funcional.md).

## 5. Estructura de `/samples` y su desvío declarado

| Tipo D8 | Estructura que `Rules-Examples.md` §2.3 fija | Estructura de este proyecto de código |
| --- | --- | --- |
| `library` | `/samples/01-basico-consola/`, `/samples/02-intermedio-con-extensiones/`, `/samples/03-avanzado-integracion-real/` | `/samples/domain/01-basico/`, `/samples/domain/02-intermedio/`, `/samples/domain/03-avanzado/` |

Dos desvíos, los dos declarados acá y ninguno de nomenclatura por dominio:

1. **Un nivel de espacio de nombres por proyecto de código.** `Rules-Examples.md` §2.3 supone un proyecto de código por repositorio. Este producto tiene **siete** en un solo repositorio (`PRODUCT-INTAKE` §13 y §16), de modo que `/samples/01-basico/` colisionaría entre proyectos de código. Se agrega el segmento `domain/`, que es carpeta extra y no renombre de las base, que es lo que §2.3 admite.
2. **Los slugs son de nivel y no de capacidad.** Se usan `basico`, `intermedio` y `avanzado`, los tres admitidos por `Rules-Examples.md` §3.1. No se usan `-consola`, `-con-extensiones` ni `-integracion-real` porque los dos últimos afirmarían algo falso de este proyecto de código: su flag `tiene_extensibilidad` es **false** (`PRODUCT-MANIFEST` §5) y no tiene integración real que demostrar, ya que declara **0** dependencias salientes.

**Tensión con `PRODUCT-INTAKE` §16.1, declarada y elevada.** Esa sección dice que Domain, Application, Infrastructure y Contracts van «sin samples propios: no son consumidas por integradores externos, sólo por Api. Su verificación vive en `tests/`». Esta categoría emite igual, con tres fundamentos verificables:

- El motivo que §16.1 da —la ausencia de integradores externos— alcanza a la **arista A** de `Rules-Examples.md` §0.1. La **arista B** tiene otro destinatario, declarado en esa misma sección: «al equipo que construye, y a los agentes de IA que codifican contra la especificación». Ese destinatario existe en este proyecto de código.
- `Deriva-Rules.md` §2.4 declara que los proyectos de código con `requiere_maqueta` en false «quedaban sin ningún instrumento de sensado» antes de la extensión `VER-XX`, y §6 exige que ninguno con categoría 10 quede sin matriz. `GeometriaFactory-Domain` es exactamente ese caso.
- Los propios artefactos de `08` declararon la omisión de su matriz como **condicionada y temporal**, no como definitiva: «cuando se emita la categoría 10, la matriz se abre con sus filas `VER-XX` y esta fila del README se retira» ([`../08-Calidad-Y-Pruebas/README.md`](../08-Calidad-Y-Pruebas/README.md) §3).

**Lo que queda abierto:** la consolidación de `PRODUCT-INTAKE` §16.1, que hoy sigue diciendo que este proyecto de código no tiene samples propios. Corregirlo es del Product Owner sobre su propio documento, con el mismo criterio con que el `PRODUCT-MANIFEST` §5 trata el residuo de §18 sobre el número de funciones de la fachada. **Hasta que se consolide, la fuente vinculante de la estructura de `/samples/domain/` es esta sección.**

## 6. Cómo agregar un sample nuevo

1. Elegir el número correlativo siguiente y un slug de `Rules-Examples.md` §3.1, por nivel o por capacidad, **nunca por dominio**.
2. Copiar la estructura de las **diez** secciones de `Rules-Examples.md` §4.2 y la cabecera de §4.1.
3. Declarar el contrato de verificación en la §9, con un `VER-XX` no usado en este proyecto de código, y criterio de aceptación evaluable.
4. Agregar la fila a las tablas de §2 y §3 de este README.
5. Dar de alta la sonda en [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), en `Sin verificar`, según `Deriva-Rules.md` §4.

## 7. Vínculo con 05 y con 11

Los tres samples respetan la superficie pública que declara [`../05-Arquitectura-Tecnica/Contratos-Abstractions.md`](../05-Arquitectura-Tecnica/Contratos-Abstractions.md) y no invocan componentes internos: los **cinco** componentes de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §3.1 son internos y ninguno se expone por separado.

**`11-Documentacion` todavía no está emitida** para este proyecto de código. Cuando lo esté, referencia estos samples y los contextualiza **sin duplicar su código**, que es la división que `Rules-Examples.md` §0 fija: 10 demuestra con código ejecutable y verificable, 11 explica, referencia y enlaza.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial de la categoría, en la **pasada de diseño** de `Rules-Examples.md` §0.2. Declara **tres** samples —el piso de §2.2 para `library`— con su tabla maestra, la tabla de contratos de verificación con las **tres** sondas `VER-01` a `VER-03` en `No verificado — sin código`, las convenciones, la estructura de `/samples/domain/` con sus **dos** desvíos declarados respecto de §2.3, y la tensión con `PRODUCT-INTAKE` §16.1 elevada al Product Owner con su fundamento. Verifica que los **trece** casos de uso quedan cubiertos y que **seis** de los **ocho** escenarios del intake §20 entran como material, con el motivo declarado de los dos que no. |
