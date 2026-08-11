# Matriz de sensado de deriva — GeometriaFactory-Domain

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** Matriz-Sensado-Deriva.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Developer Advocate / Sample Engineer Senior (AG-10)
**Variante:** Sensado de deriva por contratos de verificación
**Trazabilidad upstream:** [`../10-Examples/README.md`](../10-Examples/README.md) 1.0 §3 y los tres contratos de verificación de [`../10-Examples/ejemplo-01-basico.md`](../10-Examples/ejemplo-01-basico.md), [`../10-Examples/ejemplo-02-intermedio.md`](../10-Examples/ejemplo-02-intermedio.md) y [`../10-Examples/ejemplo-03-avanzado.md`](../10-Examples/ejemplo-03-avanzado.md), los tres 1.0; `Deriva-Rules.md` §2.3, §2.4, §3 y §4
**Trazabilidad downstream:** [`README.md`](README.md) §3 y [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §7, que declaraban su ausencia y ahora la citan; `09-Devops`, que resuelve dónde corren los comandos

---

## Tabla de contenido

- [1. Por qué existe esta matriz y por qué recién ahora](#1-por-qué-existe-esta-matriz-y-por-qué-recién-ahora)
- [2. Contra qué se sensa](#2-contra-qué-se-sensa)
- [3. La matriz](#3-la-matriz)
- [4. Umbrales de deriva aplicados](#4-umbrales-de-deriva-aplicados)
- [5. Qué no sensa esta matriz](#5-qué-no-sensa-esta-matriz)
- [6. Control de cambios](#6-control-de-cambios)

---

## 1. Por qué existe esta matriz y por qué recién ahora

`GeometriaFactory-Domain` tiene `requiere_maqueta` en **false** (`PRODUCT-MANIFEST` §5): no ejecutó la Fase B2 y no tiene línea de base visual ni contrato de datos de maqueta. Hasta la emisión de `10-Examples` no había ninguna fuente de sondas, y por eso [`README.md`](README.md) §3 y [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §7 declararon la omisión **condicionada y temporal**, con la frase de cierre «cuando se emita la categoría 10, la matriz se abre con sus filas `VER-XX`».

Eso es lo que hace este documento. `Deriva-Rules.md` §2.3 lo prevé literalmente: «cuando el proyecto de código no ejecuta Fase B2 pero sí tiene categoría 10, la matriz se emite igual, poblada solo con sondas `VER-XX` tomadas de los contratos de verificación». Y §6 lo exige: «ningún proyecto de código con categoría 10 queda sin `Matriz-Sensado-Deriva.md`, aunque no haya ejecutado Fase B2».

**Quién la abre.** `Deriva-Rules.md` §2.3 se la asigna a AG-08 en la Fase E, para el caso general. Acá la abre **AG-10** al cerrar la fase que genera la categoría 10, que es el segundo momento de sensado de `Deriva-Rules.md` §4 —«alta de una sonda `VER-XX` por cada contrato de verificación declarado en la pasada de diseño, todas en `Sin verificar`»—, porque la Fase E de este proyecto de código ya cerró y en ese momento las sondas todavía no existían. **Ninguna fila cambia de titular por eso**: la incorporación a la estrategia de testing sigue siendo de AG-08.

**Ninguna fila afirma nada sobre el estado del sistema construido.** Las tres nacen en `Sin verificar` y sin fecha, porque la biblioteca no está construida.

## 2. Contra qué se sensa

Contra los **contratos de verificación** de los tres samples de `10-Examples`, y contra nada más. No hay identificadores `SUP-XX`, `CMP-XX`, `EST-XX`, `NAV-XX` ni `DM-XX` que citar, porque este proyecto de código no tiene superficie visual ni maqueta.

Es el caso que `Deriva-Rules.md` §2.4 describe: «no requieren maqueta… antes de esta extensión, esos proyectos de código quedaban sin ningún instrumento de sensado». Las tres sondas traen **su propio comando y su propia aserción**, de modo que su método de verificación es automatizable sin desvío, y su evidencia es el campo `evidencia` del sample, citado por identificador y no transcripto acá.

## 3. La matriz

| ID | Elemento de línea de base | Afirmación a verificar | Método de verificación | Evidencia esperada | Umbral de deriva | Estado | Última verificación |
| --- | --- | --- | --- | --- | --- | --- | --- |
| `SD-01` | `VER-01` de [`../10-Examples/ejemplo-01-basico.md`](../10-Examples/ejemplo-01-basico.md) §9 | El ciclo de vida de la cuenta cierra entero: la admisibilidad devuelve los **tres** desenlaces de `CU-04` sobre la misma cuenta —`CUENTA_PENDIENTE`, `CAMBIO_DE_CONTRASENA_PENDIENTE` y admisible—, y las **9** operaciones invocadas producen **2** rechazos tipados y **0** excepciones | El comando del contrato: `dotnet run --project samples/domain/01-basico` | Campo `evidencia` de `VER-01`, con su fecha | **Menor**: cambia el texto de una línea de salida sin cambiar su semántica. **Mayor**: el `criterio_aceptacion` falla, cambia el comando sin actualizar el contrato, aparecen precondiciones no declaradas, o alguno de `CU-01`, `CU-02`, `CU-03`, `CU-04` o `CU-12` deja de estar cubierto | `Sin verificar` | — |
| `SD-02` | `VER-02` de [`../10-Examples/ejemplo-02-intermedio.md`](../10-Examples/ejemplo-02-intermedio.md) §9 | Los **seis** escenarios reales del intake §20 recorren el ciclo del trabajo: `E-1` adopta **3** piezas y **2** advertencias y pasa a `Pendiente`; `E-3` produce la advertencia con el par 36.00 y 54.00 y `E-4` produce **0** observaciones; `E-5` y `E-8` quedan retenidos en `Borrador` con la observación localizada en el **índice 1**; el recuento final es **4** envíos a `Pendiente` y **2** retenidos | El comando del contrato: `dotnet run --project samples/domain/02-intermedio` | Campo `evidencia` de `VER-02`, con su fecha | **Menor**: cambia el texto de una línea sin cambiar su semántica. **Mayor**: el `criterio_aceptacion` falla, un escenario se sustituye por datos sintéticos, el índice reportado pasa a ser 0, o alguno de `CU-05` a `CU-08` deja de estar cubierto | `Sin verificar` | — |
| `SD-03` | `VER-03` de [`../10-Examples/ejemplo-03-avanzado.md`](../10-Examples/ejemplo-03-avanzado.md) §9 | El trabajo ajeno y el inexistente devuelven resultados **idénticos campo por campo**; el alcance del administrador excluye el **1** borrador y admite eliminación en los **3** estados que ve; el reseteo conserva la situación y los **4** trabajos; y las tres inspecciones dan **0** dependencias salientes, resultado idéntico en dos corridas sin fijar el reloj, y **12** condiciones devueltas por valor con **0** excepciones de negocio | El comando del contrato: `dotnet run --project samples/domain/03-avanzado` | Campo `evidencia` de `VER-03`, con su fecha | **Menor**: cambia el texto de una línea sin cambiar su semántica. **Mayor**: el `criterio_aceptacion` falla, aparece una excepción de negocio, el recuento de dependencias salientes deja de ser 0, o alguno de `CU-09`, `CU-10`, `CU-11` o `CU-13` deja de estar cubierto | `Sin verificar` | — |

**Tres filas, una por contrato de verificación, sin contratos huérfanos ni filas sin contrato que las respalde**, que es lo que exige `Deriva-Rules.md` §6. La correspondencia es uno a uno: `SD-01`↔`VER-01`, `SD-02`↔`VER-02`, `SD-03`↔`VER-03`.

**El método de verificación de las tres es el comando declarado en su contrato, sin desvío.** No hay ninguna fila resuelta por inspección, porque `Deriva-Rules.md` §2.4 declara que una sonda `VER-XX` «trae su propio comando y su propia aserción».

## 4. Umbrales de deriva aplicados

Se toman de la fila «Contratos y comportamiento (`VER-XX`)» de `Deriva-Rules.md` §3, sin agregarle dimensiones.

| Dimensión | Deriva menor, se registra y no bloquea | Deriva mayor, bloquea y exige decisión | Filas |
| --- | --- | --- | --- |
| Contratos y comportamiento (`VER-XX`) | Cambia el texto de un mensaje de salida sin cambiar su semántica, o cambia el formato de un registro | El `criterio_aceptacion` falla; cambia el comando de ejecución sin actualizar el contrato; aparecen precondiciones no declaradas; o el caso de uso que la sonda ejercita deja de estar cubierto | `SD-01`, `SD-02`, `SD-03` |

**Toda deriva mayor se resuelve por una de dos vías y nunca por omisión** (`Deriva-Rules.md` §3): se corrige la biblioteca para volver a lo que el contrato dice, o se cambia la especificación con aprobación humana explícita, en cuyo caso la categoría 02 la modifica, el sample se rehace y esta matriz se actualiza. **Un caso de uso no se cambia desde acá.**

**Un `criterio_aceptacion` en `Falla` es un hallazgo del incremento en curso** y no se resuelve borrando la fila (`Rules-Examples.md` §4.4 y §4.5).

## 5. Qué no sensa esta matriz

Se declara para que no se lea como cobertura completa del proyecto de código:

| Elemento | Quién lo verifica |
| --- | --- |
| Las **42** condiciones del catálogo de `03`, en las dos direcciones | `TC-23` de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md), en la batería de `tests/` |
| Los **nueve** invariantes ejercidos sin dobles | `TC-26`, sobre §5 de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) |
| Cobertura de líneas y de ramas, y tiempo de la batería | El pipeline de `09-Devops`; son los dos gates condicionados `QG-03` y `QG-07` |
| Las **dieciséis** reglas de negocio, una por una | Los **veintisiete** casos de prueba de `TC-01` a `TC-27` |

**Las tres sondas no reemplazan a la batería de pruebas**: la complementan desde afuera, ejercitando la superficie pública tal como la ve un consumidor. Es la asimetría que `Deriva-Rules.md` §4 declara en su cuarto momento.

## 6. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial, abierta por AG-10 al cerrar la fase que genera la categoría 10, que es el segundo momento de sensado de `Deriva-Rules.md` §4. Cierra el hueco que [`README.md`](README.md) §3 y [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §7 declararon como condicionado y temporal. Declara **tres** filas, `SD-01` a `SD-03`, una por cada contrato de verificación de `10-Examples`, con el comando del contrato como método, el campo `evidencia` del sample como evidencia esperada, el umbral de la fila «Contratos y comportamiento» de `Deriva-Rules.md` §3 y estado `Sin verificar` sin fecha. Declara además qué **no** sensa, para que la matriz no se lea como cobertura completa del proyecto de código. |
