# 10 · Ejemplos — GeometriaFactory-Domain

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** README.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Developer Advocate / Sample Engineer Senior (AG-10)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../02-Especificacion-Funcional/`](../02-Especificacion-Funcional/), los **trece** casos de uso; [`../05-Arquitectura-Tecnica/Contratos-Abstractions.md`](../../../05-Arquitectura-Tecnica/Contratos-Abstractions.md), las **trece** operaciones; [`../06-Backlog-Tecnico/historias-usuario/`](../06-Backlog-Tecnico/historias-usuario/), las **veintisiete** historias; [`../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md`](../../../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md) 1.0, los **veintisiete** casos de prueba; `PRODUCT-INTAKE` **1.25** §16.1, §18 y §20
**Trazabilidad downstream:** [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../../../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), que toma las tres sondas `VER-XX`; `11-Documentacion` cuando se emita, que referencia estos samples sin duplicar su código

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

Tres markdown explicativos, uno por sample, con sus **diez** secciones obligatorias de `Rules-Examples.md` §4.2, y este índice. Cada markdown apunta a una carpeta ejecutable de [`/samples/domain/`](../../../../../samples/domain/) del repositorio, que esta pasada deja **esqueletada**: con su README local y su comando previsto, y sin corrida hecha. **Las tres carpetas existen** —[`01-basico/`](../../../../../samples/domain/01-basico/), [`02-intermedio/`](../../../../../samples/domain/02-intermedio/) y [`03-avanzado/`](../../../../../samples/domain/03-avanzado/)—, cada una con su `README.md` local y el comando previsto de su contrato, y ninguna con código.

Esta emisión es la **pasada de diseño** de `Rules-Examples.md` §0.2. Los tres contratos de verificación están completos salvo el campo `evidencia`, que dice `No verificado — sin código` en los tres. **Ninguna carpeta de `/samples` promete una corrida que no se hizo.**

**Los tres samples sirven a las dos aristas de `Rules-Examples.md` §0.1.** La arista A —referencia de integración— le habla al consumidor de esta biblioteca, que dentro de este producto es `GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`. La arista B —arnés de autovalidación— le habla al equipo que construye y a los agentes que codifican contra la especificación, y es la que aporta las sondas `VER-XX` de la matriz de sensado.

## 2. Tabla maestra de samples

| Sample | Nivel | Tiempo de setup | CU ilustrados | Ubicación |
| --- | --- | --- | --- | --- |
| [`ejemplo-01-basico.md`](ejemplo-01-basico.md) | Básico | < 5 min | CU-02001, CU-02002, CU-02003, CU-02004, CU-02012 | `/samples/domain/01-basico/` |
| [`ejemplo-02-intermedio.md`](ejemplo-02-intermedio.md) | Intermedio | < 5 min | CU-02005, CU-02006, CU-02007, CU-02008 | `/samples/domain/02-intermedio/` |
| [`ejemplo-03-avanzado.md`](ejemplo-03-avanzado.md) | Avanzado | 10-15 min | CU-02009, CU-02010, CU-02011, CU-02013 | `/samples/domain/03-avanzado/` |

**Tres samples, el piso que `Rules-Examples.md` §2.2 fija para `library`.** El tiempo de setup del tercero es mayor porque agrega la lectura del archivo de proyecto y dos corridas consecutivas sin fijar el reloj.

**Cobertura de los trece casos de uso.** Los tres samples cubren **trece de trece**: `CU-02001` a `CU-02013`, sin repeticiones y sin huecos. Verificación uno por uno: `CU-02001`, `CU-02002`, `CU-02003`, `CU-02004` y `CU-02012` en el 01; `CU-02005`, `CU-02006`, `CU-02007` y `CU-02008` en el 02; `CU-02009`, `CU-02010`, `CU-02011` y `CU-02013` en el 03.

**Cobertura de los ocho escenarios reales.** El sample 02 usa **seis** de los ocho del `PRODUCT-INTAKE` §20 —`E-1`, `E-3`, `E-4`, `E-5`, `E-6` y `E-8`—, transcriptos sin modificación. Los dos que no aparecen son `E-2` y `E-7`: `E-2` es el mismo ortoedro con volumen declarado incorrecto que `E-1` ya trae dentro, y su valor propio está en las dos trampas de formato del texto —clave `Tapas` y comas finales—, que son de la lectura del texto y no del dominio, que adopta la interpretación ya hecha; `E-7` ejercita los **seis** tipos dibujables, que es materia del proyecto de código que dibuja. Los dos sí están cubiertos por casos de prueba del proyecto de código: `E-7` en `TC-02013` y `E-2` en `TC-02017` de [`../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md`](../../../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md). **Ningún escenario se sustituye por datos sintéticos.**

## 3. Contratos de verificación

Vista de conjunto de la arista B, en el formato de `Rules-Examples.md` §4.4.

| Sonda | Sample | Verifica | Comando | Estado | Última corrida |
| --- | --- | --- | --- | --- | --- |
| `VER-02001` | [`ejemplo-01-basico.md`](ejemplo-01-basico.md) | CU-02001, CU-02002, CU-02003, CU-02004, CU-02012; US-02001, US-02004, US-02006, US-02024, US-02027 | `dotnet run --project samples/domain/01-basico` | No verificado — sin código | — |
| `VER-02002` | [`ejemplo-02-intermedio.md`](ejemplo-02-intermedio.md) | CU-02005, CU-02006, CU-02007, CU-02008; US-02009 a US-02016 | `dotnet run --project samples/domain/02-intermedio` | No verificado — sin código | — |
| `VER-02003` | [`ejemplo-03-avanzado.md`](ejemplo-03-avanzado.md) | CU-02009, CU-02010, CU-02011, CU-02013; US-02018 a US-02023, US-02026 | `dotnet run --project samples/domain/03-avanzado` | No verificado — sin código | — |

**Tres sondas, ninguna redundante**: los conjuntos de casos de uso que verifican son disjuntos. Las tres entran a [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../../../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md) con estado `Sin verificar`, que es lo que `Deriva-Rules.md` §2.4 declara para un contrato en `No verificado — sin código`.

**Qué queda fuera de las tres sondas, y por qué no es un hueco.** Las **42** condiciones del catálogo de `03`, los **nueve** invariantes ejercidos sin dobles y los umbrales de cobertura y de tiempo de la batería no los verifica ningún sample: los verifica la batería de `tests/GeometriaFactory.Domain.Tests` por `TC-02023`, `TC-02026` y el pipeline. Un sample que los duplicara sería el anti-patrón de `Rules-Examples.md` §4.5, «samples que duplican el `/src` sin agregar valor demostrativo».

## 4. Convenciones de los samples

- **Autocontenidos.** Ninguno requiere servicios externos: el proyecto de código declara **0** dependencias salientes (`PRODUCT-INTAKE` §17.1.P.1 · GeometriaFactory-Domain).
- **Ejecutables en entorno limpio en cuatro pasos**, dentro del entorno de desarrollo contenido del repositorio, que es donde ocurre todo el ciclo porque el host no tiene la plataforma (`PRODUCT-INTAKE`, encabezado de la Parte C).
- **Nivel declarado** en la §2 de cada markdown, y progresión por nivel y no por dominio.
- **Trazabilidad obligatoria** en la §8 de cada markdown, con al menos una fila por caso de uso, regla, ADR o NFR.
- **Criterio de aceptación evaluable por una máquina**: exit code más líneas exactas de salida. Ninguno está redactado como prosa.
- **Los datos son reales.** Los escenarios del `PRODUCT-INTAKE` §20 se transcriben sin modificación y no se sustituyen por datos sintéticos. **Los archivos de escenario llevan extensión `.txt` y no `.json`**, que es la convención de los siete proyectos de código del producto: el texto de `E-2` **no es JSON estrictamente válido** —trae dos comas finales— y nombrar `.json` a un archivo de escenario invita a que una herramienta lo reformatee al abrirlo. En este proyecto de código `E-2` no está entre los seis que el sample 02 usa, de modo que el riesgo no se materializa; la convención se sigue igual, porque una carpeta de escenarios con dos extensiones distintas según el proyecto de código es la clase de detalle que después nadie recuerda por qué está.
- **Los samples no acuñan vocabulario.** Todo término que usan está declarado en [`../02-Especificacion-Funcional/Glosario-Funcional.md`](../../../02-Especificacion-Funcional/Glosario-Funcional.md).

## 5. Estructura de `/samples` y su desvío declarado

| Tipo D8 | Estructura que `Rules-Examples.md` §2.3 fija | Estructura de este proyecto de código |
| --- | --- | --- |
| `library` | `/samples/01-basico-consola/`, `/samples/02-intermedio-con-extensiones/`, `/samples/03-avanzado-integracion-real/` | `/samples/domain/01-basico/`, `/samples/domain/02-intermedio/`, `/samples/domain/03-avanzado/` |

Dos desvíos, los dos declarados acá y ninguno de nomenclatura por dominio:

1. **Un nivel de espacio de nombres por proyecto de código.** `Rules-Examples.md` §2.3 supone un proyecto de código por repositorio. Este producto tiene **siete** en un solo repositorio (`PRODUCT-INTAKE` §13 y §16), de modo que `/samples/01-basico/` colisionaría entre proyectos de código. Se agrega el segmento `domain/`, que es carpeta extra y no renombre de las base, que es lo que §2.3 admite.
2. **Los slugs son de nivel y no de capacidad.** Se usan `basico`, `intermedio` y `avanzado`, los tres admitidos por `Rules-Examples.md` §3.1. No se usan `-consola`, `-con-extensiones` ni `-integracion-real` porque los dos últimos afirmarían algo falso de este proyecto de código: su flag `tiene_extensibilidad` es **false** (`PRODUCT-MANIFEST` §5) y no tiene integración real que demostrar, ya que declara **0** dependencias salientes.

**Tensión con `PRODUCT-INTAKE` §16.1, declarada, elevada y resuelta.** La redacción de §16.1 anterior al 2026-08-11 decía que Domain, Application, Infrastructure y Contracts iban «sin samples propios: no son consumidas por integradores externos, sólo por Api. Su verificación vive en `tests/`». Esta categoría emitió igual, con tres fundamentos verificables:

- El motivo que §16.1 da —la ausencia de integradores externos— alcanza a la **arista A** de `Rules-Examples.md` §0.1. La **arista B** tiene otro destinatario, declarado en esa misma sección: «al equipo que construye, y a los agentes de IA que codifican contra la especificación». Ese destinatario existe en este proyecto de código.
- `Deriva-Rules.md` §2.4 declara que los proyectos de código con `requiere_maqueta` en false «quedaban sin ningún instrumento de sensado» antes de la extensión `VER-XX`, y §6 exige que ninguno con categoría 10 quede sin matriz. `GeometriaFactory-Domain` es exactamente ese caso.
- Los propios artefactos de `08` declararon la omisión de su matriz como **condicionada y temporal**, no como definitiva: «cuando se emita la categoría 10, la matriz se abre con sus filas `VER-XX` y esta fila del README se retira» ([`../08-Calidad-Y-Pruebas/README.md`](../../../08-Calidad-Y-Pruebas/README.md) §3).

**El punto que quedaba abierto está cerrado, y se conserva con su desenlace.** Este README declaraba como abierta la consolidación de §16.1 y la elevaba al Product Owner. **La consolidación se hizo**: el `PRODUCT-INTAKE` **1.23** reescribió esa fila el mismo 2026-08-11, y el **1.25** vigente la deja así —abierta en el documento y leída, no citada a través de otro artefacto—:

> **`/samples/domain/` y `/samples/contracts/`** [AMPLIADO 2026-08-11]. La redacción anterior —«sin samples propios: no son consumidas por integradores externos»— resolvía bien la pregunta de la **audiencia externa** […]. La Fase G mostró que hay una **segunda audiencia declarada** por la guía de la categoría […]. Su verificación sigue viviendo en `tests/`; los samples no la reemplazan, la ilustran.

De modo que **la fuente vinculante de la estructura de `/samples/domain/` es §16.1 del `PRODUCT-INTAKE`**, y no esta sección: acá se conserva el fundamento con el que la categoría emitió antes de que la consolidación se escribiera. **No queda ningún punto abierto sobre §16.1 por parte de este proyecto de código.** Tampoco queda uno sobre §18: la 1.25 precisó que «las tres muestras `S-1`, `S-2` y `S-3` **no son el conjunto de las carpetas** de `/samples`», con lo cual la aparente contradicción entre las dos secciones ya no existe.

## 6. Cómo agregar un sample nuevo

1. Elegir el número correlativo siguiente y un slug de `Rules-Examples.md` §3.1, por nivel o por capacidad, **nunca por dominio**.
2. Copiar la estructura de las **diez** secciones de `Rules-Examples.md` §4.2 y la cabecera de §4.1.
3. Declarar el contrato de verificación en la §9, con un `VER-XX` no usado en este proyecto de código, y criterio de aceptación evaluable.
4. Agregar la fila a las tablas de §2 y §3 de este README.
5. Dar de alta la sonda en [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../../../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), en `Sin verificar`, según `Deriva-Rules.md` §4.

## 7. Vínculo con 05 y con 11

Los tres samples respetan la superficie pública que declara [`../05-Arquitectura-Tecnica/Contratos-Abstractions.md`](../../../05-Arquitectura-Tecnica/Contratos-Abstractions.md) y no invocan componentes internos: los **cinco** componentes de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §3.1 son internos y ninguno se expone por separado.

**`11-Documentacion` todavía no está emitida** para este proyecto de código. Cuando lo esté, referencia estos samples y los contextualiza **sin duplicar su código**, que es la división que `Rules-Examples.md` §0 fija: 10 demuestra con código ejecutable y verificable, 11 explica, referencia y enlaza.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **Correcciones del informe `G-10-Examples-Siete-Proyectos-r1.md` 1.0, contrastadas contra el texto vivo del `PRODUCT-INTAKE` 1.25.** **P0-1**: las tres carpetas de [`/samples/domain/`](../../../../../samples/domain/) se crean de verdad, cada una con su README local y su comando previsto, y §1 lo declara con el enlace; el comando de las tres filas `VER-XX` de la matriz de sensado queda coherente con lo que existe. **P1-1**: se cierra el punto abierto sobre §16.1, que era falso —la consolidación la hizo el intake 1.23 en el mismo commit que emitió esta categoría—; la fila se conserva con su desenlace y con la cita de §16.1 abierta en la fuente, y se declara que la fuente vinculante de la estructura es §16.1 y no este README. **P1-2**: se retira la invocación del «residuo de §18 sobre el número de funciones de la fachada», que §18 no tiene: la sección vigente enumera las **seis** funciones y las rotula «las seis que §17.7 P.3 declara desde 1.6». **P1-3**: se registra que la 1.25 precisó que las tres muestras `S-X` de §18 no son el conjunto de las carpetas de `/samples`, con lo cual esa contradicción tampoco queda abierta. **P3-1**: §4 declara el fundamento de la extensión `.txt` de los archivos de escenario, que faltaba en toda la categoría de este proyecto de código. Se actualiza la trazabilidad upstream a la versión **1.25** del intake. Ningún recuento, contrato, sample ni cobertura cambia. |
| 1.0 | 2026-08-11 | Emisión inicial de la categoría, en la **pasada de diseño** de `Rules-Examples.md` §0.2. Declara **tres** samples —el piso de §2.2 para `library`— con su tabla maestra, la tabla de contratos de verificación con las **tres** sondas `VER-02001` a `VER-02003` en `No verificado — sin código`, las convenciones, la estructura de `/samples/domain/` con sus **dos** desvíos declarados respecto de §2.3, y la tensión con `PRODUCT-INTAKE` §16.1 elevada al Product Owner con su fundamento. Verifica que los **trece** casos de uso quedan cubiertos y que **seis** de los **ocho** escenarios del intake §20 entran como material, con el motivo declarado de los dos que no. |
