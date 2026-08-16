# 10 · Ejemplos — GeometriaFactory-Application

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** README.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Developer Advocate / Sample Engineer Senior (AG-10)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../02-Especificacion-Funcional/`](../02-Especificacion-Funcional/), los **once** casos de uso; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §3.1, los **ocho** componentes, y §3.4, los **cuatro** puertos; [`../05-Arquitectura-Tecnica/Contratos-Abstractions.md`](../05-Arquitectura-Tecnica/Contratos-Abstractions.md); [`../06-Backlog-Tecnico/historias-usuario/`](../06-Backlog-Tecnico/historias-usuario/), las **treinta y dos** historias; [`../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md`](../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md), los **treinta y un** casos de prueba; `PRODUCT-INTAKE` **1.25** §16.1, §18 y §20
**Trazabilidad downstream:** [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), que toma las tres sondas `VER-XX`; `11-Documentacion` cuando se emita, que referencia estos samples sin duplicar su código

---

## Tabla de contenido

- [1. Qué hay en esta carpeta](#1-qué-hay-en-esta-carpeta)
- [2. Tabla maestra de samples](#2-tabla-maestra-de-samples)
- [3. Contratos de verificación](#3-contratos-de-verificación)
- [4. Convenciones de los samples](#4-convenciones-de-los-samples)
- [5. Estructura de `/samples` y su desvío declarado](#5-estructura-de-samples-y-su-desvío-declarado)
- [6. Por qué este proyecto de código emite samples, contra lo que §16.1 decía](#6-por-qué-este-proyecto-de-código-emite-samples-contra-lo-que-161-decía)
- [7. Cómo agregar un sample nuevo](#7-cómo-agregar-un-sample-nuevo)
- [8. Vínculo con 05 y con 11](#8-vínculo-con-05-y-con-11)
- [9. Control de cambios](#9-control-de-cambios)

---

## 1. Qué hay en esta carpeta

Tres markdown explicativos, uno por sample, con sus **diez** secciones obligatorias de `Rules-Examples.md` §4.2, y este índice. Cada markdown apunta a una carpeta ejecutable de [`/samples/application/`](../../../../../samples/application/) del repositorio, que esta pasada deja **esqueletada**: con su README local y su comando previsto, y sin corrida hecha. **Las tres carpetas existen** —[`01-basico/`](../../../../../samples/application/01-basico/), [`02-intermedio/`](../../../../../samples/application/02-intermedio/) y [`03-avanzado/`](../../../../../samples/application/03-avanzado/)—, cada una con su `README.md` local y el comando previsto de su contrato, y ninguna con código.

Esta emisión es la **pasada de diseño** de `Rules-Examples.md` §0.2. Los tres contratos de verificación están completos salvo el campo `evidencia`, que dice `No verificado — sin código` en los tres. **Ninguna carpeta de `/samples` promete una corrida que no se hizo.**

**Los tres samples sirven a las dos aristas de `Rules-Examples.md` §0.1.** La arista A —referencia de integración— le habla al consumidor de esta biblioteca, que dentro de este producto es la composición de raíz de `GeometriaFactory-Api`. La arista B —arnés de autovalidación— le habla al equipo que construye y a los agentes que codifican contra la especificación, y es la que aporta las sondas `VER-XX` de la matriz de sensado.

## 2. Tabla maestra de samples

| Sample | Nivel | Tiempo de setup | CU ilustrados | Ubicación |
| --- | --- | --- | --- | --- |
| [`ejemplo-01-basico.md`](ejemplo-01-basico.md) | Básico | < 5 min | CU-04001, CU-04003, CU-04010 | `/samples/application/01-basico/` |
| [`ejemplo-02-intermedio.md`](ejemplo-02-intermedio.md) | Intermedio | < 5 min | CU-04004, CU-04005, CU-04006, CU-04009 | `/samples/application/02-intermedio/` |
| [`ejemplo-03-avanzado.md`](ejemplo-03-avanzado.md) | Avanzado | 10-15 min | CU-04002, CU-04007, CU-04008, CU-04011 | `/samples/application/03-avanzado/` |

**Tres samples, el piso que `Rules-Examples.md` §2.2 fija para `library`.** El tiempo de setup del tercero es mayor porque necesita el conjunto de cuentas y de trabajos que los dos primeros dejan armado, y lo reconstruye desde cero con los cuatro dobles de puerto.

**Cobertura de los once casos de uso: 11 de 11.** Verificación uno por uno: `CU-04001`, `CU-04003` y `CU-04010` en el 01; `CU-04004`, `CU-04005`, `CU-04006` y `CU-04009` en el 02; `CU-04002`, `CU-04007`, `CU-04008` y `CU-04011` en el 03. Sin repeticiones y sin huecos: 3 + 4 + 4 = 11.

**Cobertura de las cuatro comprobaciones de autorización: 4 de 4.** El orden fijo de [`ADR-04004`](../05-Arquitectura-Tecnica/Adrs/ADR-04004-Orden-Fijo-De-Las-Cuatro-Comprobaciones.md) lo recorre entero el sample 01 —que es donde se ve por qué la del cambio de contraseña pendiente corta antes que las otras tres—, y los samples 02 y 03 lo ejercen sobre pedidos concretos: pertenencia en el 02, facultad y alcance del administrador en el 03.

**Cobertura de los ocho escenarios reales: 8 de 8.** El sample 02 los recorre todos, en el orden `E-1` a `E-8`, y toma de cada uno lo que [`../08-Calidad-Y-Pruebas/Estrategia-Testing.md`](../08-Calidad-Y-Pruebas/Estrategia-Testing.md) §6 declara que aporta a este proyecto de código. **Y hay una precisión que esta categoría tiene que decir con exactitud**: a esta capa no le entra el texto del escenario sino **el resultado de interpretación que el doble del puerto devuelve** —piezas, observaciones y la cantidad de figuras del conjunto raíz—, porque la interpretación es de `GeometriaFactory-Infrastructure`. El texto original sí viaja íntegro por la capa, y eso es lo que `RN-04008` exige verificar. **Ningún escenario se sustituye por datos sintéticos.**

## 3. Contratos de verificación

Vista de conjunto de la arista B, en el formato de `Rules-Examples.md` §4.4.

| Sonda | Sample | Verifica | Comando | Estado | Última corrida |
| --- | --- | --- | --- | --- | --- |
| `VER-04001` | [`ejemplo-01-basico.md`](ejemplo-01-basico.md) | CU-04001, CU-04003, CU-04010; US-04001, US-04002, US-04003, US-04007, US-04009, US-04028, US-04030, US-04032 | `dotnet run --project samples/application/01-basico` | No verificado — sin código | — |
| `VER-04002` | [`ejemplo-02-intermedio.md`](ejemplo-02-intermedio.md) | CU-04004, CU-04005, CU-04006, CU-04009; US-04010 a US-04019, US-04026 | `dotnet run --project samples/application/02-intermedio` | No verificado — sin código | — |
| `VER-04003` | [`ejemplo-03-avanzado.md`](ejemplo-03-avanzado.md) | CU-04002, CU-04007, CU-04008, CU-04011; US-04004, US-04005, US-04006, US-04008, US-04020 a US-04025, US-04027, US-04029, US-04031 | `dotnet run --project samples/application/03-avanzado` | No verificado — sin código | — |

**Tres sondas, ninguna redundante**: los conjuntos de casos de uso que verifican son disjuntos, y los de historias también. Las tres entran a [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md) con estado `Sin verificar`, que es lo que `Deriva-Rules.md` §2.4 declara para un contrato en `No verificado — sin código`.

**Qué queda fuera de las tres sondas, y por qué no es un hueco.** El catálogo cerrado de **36** condiciones de la categoría 03 recorrido en las dos direcciones, la cobertura de líneas y de ramas, y la medición de mutación no los verifica ningún sample: los verifica la batería de `tests/GeometriaFactory.Application.Tests` y el pipeline de `09-Devops`. Un sample que los duplicara sería el anti-patrón de `Rules-Examples.md` §4.5, «samples que duplican el `/src` sin agregar valor demostrativo».

## 4. Convenciones de los samples

- **Autocontenidos.** Ninguno requiere servicios externos: el proyecto de código declara **una sola** dependencia saliente, `GeometriaFactory-Domain` (`PRODUCT-INTAKE` §17.2.P.1), y los **cuatro** puertos se satisfacen con dobles que viven dentro del propio sample.
- **Ejecutables en entorno limpio en cuatro pasos**, dentro del entorno de desarrollo contenido del repositorio, que es donde ocurre todo el ciclo porque el host no tiene la plataforma (`PRODUCT-INTAKE`, encabezado de la Parte C).
- **Los dobles son de puerto y de nada más**, que es la política que [`../08-Calidad-Y-Pruebas/Estrategia-Testing.md`](../08-Calidad-Y-Pruebas/Estrategia-Testing.md) §5 fija: ningún sample sustituye un componente interno de esta capa por un doble.
- **Nivel declarado** en la §2 de cada markdown, y progresión por nivel y no por dominio.
- **Trazabilidad obligatoria** en la §8 de cada markdown, con al menos una fila por caso de uso, regla, ADR o NFR.
- **Criterio de aceptación evaluable por una máquina**: exit code más líneas exactas de salida. Ninguno está redactado como prosa.
- **Los datos son reales.** Los resultados de interpretación de los escenarios del `PRODUCT-INTAKE` §20 se transcriben de la sección «qué verificar» del escenario correspondiente y no se sustituyen por datos sintéticos. Los datos de identidad y de orquestación que ningún escenario da —un correo, un nombre, un identificador, un momento— son valores evidentemente ficticios y se declaran como tales, que es la regla de [`../08-Calidad-Y-Pruebas/Estrategia-Testing.md`](../08-Calidad-Y-Pruebas/Estrategia-Testing.md) §6.
- **Los samples no acuñan vocabulario ni condiciones.** Todo término está declarado en [`../02-Especificacion-Funcional/Glosario-Funcional.md`](../02-Especificacion-Funcional/Glosario-Funcional.md), y las condiciones salen del catálogo cerrado de [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md).

## 5. Estructura de `/samples` y su desvío declarado

| Tipo D8 | Estructura que `Rules-Examples.md` §2.3 fija | Estructura de este proyecto de código |
| --- | --- | --- |
| `library` | `/samples/01-basico-consola/`, `/samples/02-intermedio-con-extensiones/`, `/samples/03-avanzado-integracion-real/` | `/samples/application/01-basico/`, `/samples/application/02-intermedio/`, `/samples/application/03-avanzado/` |

Dos desvíos, los dos declarados acá y ninguno de nomenclatura por dominio:

1. **Un nivel de espacio de nombres por proyecto de código.** `Rules-Examples.md` §2.3 supone un proyecto de código por repositorio. Este producto tiene **siete** en un solo repositorio (`PRODUCT-INTAKE` §13 y §16), de modo que `/samples/01-basico/` colisionaría entre proyectos de código. Se agrega el segmento `application/`, que es carpeta extra y no renombre de las base, que es lo que §2.3 admite. Es el mismo criterio que ya aplicaron `GeometriaFactory-Domain`, `GeometriaFactory-Contracts` y `GeometriaFactory-Visor` al emitir su categoría 10.
2. **Los slugs son de nivel y no de capacidad.** Se usan `basico`, `intermedio` y `avanzado`, los tres admitidos por `Rules-Examples.md` §3.1. No se usan `-con-extensiones` ni `-integracion-real` porque afirmarían algo falso de este proyecto de código: su flag `tiene_extensibilidad` es **false** (`PRODUCT-MANIFEST` §5) y su única integración real es con los adaptadores de `GeometriaFactory-Infrastructure`, que un sample de esta capa no puede usar sin dejar de probar lo que viene a probar —que un caso de uso entero se ejecuta con dobles, sin base de datos y sin frontera de proceso—.

## 6. Por qué este proyecto de código emite samples, contra lo que §16.1 decía

El `PRODUCT-INTAKE` **1.23** §16.1 dejó la fila de `Application` e `Infrastructure` con la redacción anterior —«sin samples propios: no son consumidas por integradores externos, sólo por Api. Su verificación vive en `tests/`»— y le agregó una anotación viva: «**queda por revisar si les alcanza el mismo argumento que a Domain y Contracts** cuando su Fase G se emita». Esta sección hace esa revisión y **declara su resultado: el argumento alcanza, y este proyecto de código emite samples.**

El argumento que valió para `Domain` y `Contracts` es doble, y hay que comprobar los dos términos por separado.

| Término del argumento | ¿Se cumple en `GeometriaFactory-Application`? | Comprobación |
| --- | --- | --- |
| **Hay una segunda audiencia declarada por la guía de la categoría** | **Sí** | `Rules-Examples.md` §0.1 declara la arista B con su destinatario: «al equipo que construye, y a los agentes de IA que codifican contra la especificación». El motivo que §16.1 da —la ausencia de integradores externos— alcanza a la **arista A** y no a la B |
| **Sin categoría 10 el proyecto de código queda sin ninguna sonda de deriva** | **Sí** | `requiere_maqueta` es **false** (`PRODUCT-MANIFEST` §5): no hay Fase B2 ni línea de base visual. Y [`../08-Calidad-Y-Pruebas/README.md`](../08-Calidad-Y-Pruebas/README.md) §3 omitió la matriz de sensado por las **dos** condiciones juntas, con la frase de cierre «cuando se emita la categoría 10, la matriz se abre con sus filas `VER-XX`» |

**Los dos términos se cumplen, y por eso la conclusión es la misma que para `Domain` y `Contracts`.** `Deriva-Rules.md` §2.4 describe exactamente este caso —«un proyecto de código con `requiere_maqueta` en false… antes de esta extensión, esos proyectos de código quedaban sin ningún instrumento de sensado»— y §2.3 obliga a emitir la matriz cuando hay categoría 10.

**Lo que la emisión no hace.** No reemplaza la verificación que vive en `tests/`, y §16.1 tiene razón en que ahí vive: las tres sondas **complementan** la batería desde afuera, ejercitando la superficie pública tal como la ve la composición de raíz, y §3 de este README declara qué queda fuera de ellas.

**El punto que quedaba abierto está cerrado, y se conserva con su desenlace.** Este README declaraba como abierta la consolidación de §16.1 «cuya fila para este proyecto de código sigue diciendo «sin samples propios»», y la elevaba al Product Owner. **Esa afirmación dejó de ser cierta el mismo día en que se escribió**: el `PRODUCT-INTAKE` **1.24** reescribió la fila en el mismo commit que entregó esta categoría, y la **1.25** vigente dice, leída en la fuente:

> **`/samples/application/` y `/samples/infrastructure/`** [AMPLIADO 2026-08-11]. La revisión que 1.23 dejó anotada se hizo al emitir su Fase G y los dos términos se cumplen: la segunda audiencia declarada por la guía, y que sin categoría 10 quedan **sin ninguna sonda de deriva** por no tener maqueta.

Es decir: §16.1 **adoptó** la conclusión de esta sección y con los mismos dos términos. De modo que **la fuente vinculante de la estructura de `/samples/application/` es §16.1 del `PRODUCT-INTAKE`**, y no §5 de este README. **No queda ningún punto abierto sobre §16.1 por parte de este proyecto de código**, y tampoco sobre §18: la 1.25 precisó que «las tres muestras `S-1`, `S-2` y `S-3` **no son el conjunto de las carpetas** de `/samples`».

## 7. Cómo agregar un sample nuevo

1. Elegir el número correlativo siguiente y un slug de `Rules-Examples.md` §3.1, por nivel o por capacidad, **nunca por dominio**.
2. Copiar la cabecera de §4.1 y las **diez** secciones de §4.2 de esas reglas.
3. Declarar el contrato de verificación en la §9, con un `VER-XX` no usado en este proyecto de código, y criterio de aceptación evaluable.
4. Agregar la fila a las tablas de §2 y §3 de este README.
5. Dar de alta la sonda en [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), en `Sin verificar`, según `Deriva-Rules.md` §4.

## 8. Vínculo con 05 y con 11

Los tres samples consumen la superficie pública que declara [`../05-Arquitectura-Tecnica/Contratos-Abstractions.md`](../05-Arquitectura-Tecnica/Contratos-Abstractions.md) y no invocan componentes internos: los **ocho** componentes de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §3.1 son internos y ninguno se expone por separado. Los **cuatro** puertos se satisfacen con dobles, que es lo que [`ADR-04002`](../05-Arquitectura-Tecnica/Adrs/ADR-04002-Cuatro-Puertos-Y-La-Frontera-Que-Declaran.md) hace posible.

**`11-Documentacion` todavía no está emitida** para este proyecto de código. Cuando lo esté, referencia estos samples y los contextualiza **sin duplicar su código**, que es la división que `Rules-Examples.md` §0 fija: 10 demuestra con código ejecutable y verificable, 11 explica, referencia y enlaza.

## 9. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **Correcciones del informe `G-10-Examples-Siete-Proyectos-r1.md` 1.0, contrastadas contra el texto vivo del `PRODUCT-INTAKE` 1.25.** **P0-1**: las tres carpetas de [`/samples/application/`](../../../../../samples/application/) se crean de verdad, cada una con su README local y su comando previsto, y §1 lo declara con el enlace; el comando de las tres filas `VER-XX` de la matriz de sensado queda coherente con lo que existe. **P1-1**: se cierra el punto abierto de §6 sobre §16.1, que era falso —la fila la reescribió el intake **1.24** en el mismo commit que entregó esta categoría—; la fila se conserva con su desenlace, con §16.1 citada desde la fuente, y se declara que la fuente vinculante de la estructura es §16.1 y no §5 de este README. **P1-2**: se retira la invocación del «residuo de §18 sobre el número de funciones de la fachada», que §18 no tiene: la sección vigente enumera las **seis** funciones y las rotula «las seis que §17.7 P.3 declara desde 1.6». **P1-3**: se registra que la 1.25 precisó que las tres muestras `S-X` de §18 no son el conjunto de las carpetas de `/samples`. Se corrige además, fuera del informe, la fila 1.0 de este control de cambios, que decía «**seis** de los **ocho** escenarios del intake §20 entran como material, con el motivo declarado de los dos que no» contra su propia §2, que declara **8 de 8** y es la correcta: el sample 02 los recorre todos. Se actualiza la trazabilidad upstream a la versión **1.25** del intake. Ningún recuento de cobertura, contrato ni sample cambia. |
| 1.0 | 2026-08-11 | Emisión inicial de la categoría, en la **pasada de diseño** de `Rules-Examples.md` §0.2. Declara **tres** samples —el piso de §2.2 para `library`— con su tabla maestra, la tabla de contratos de verificación con las **tres** sondas `VER-04001` a `VER-04003` en `No verificado — sin código`, las convenciones, la estructura de `/samples/application/` con sus **dos** desvíos declarados respecto de §2.3, y **la revisión que el `PRODUCT-INTAKE` 1.23 §16.1 dejó pendiente**: se comprueban los dos términos del argumento que valió para `Domain` y `Contracts`, se declara que los dos se cumplen acá y se emite en consecuencia, con la consolidación de §16.1 elevada al Product Owner. Verifica que los **once** casos de uso y las **cuatro** comprobaciones de autorización quedan cubiertos, y que los **ocho** escenarios del intake §20 entran como material, recorridos enteros por el sample 02. [Recuento corregido en 1.1: la fila decía «seis de los ocho», contra la §2 de este mismo README.] |
