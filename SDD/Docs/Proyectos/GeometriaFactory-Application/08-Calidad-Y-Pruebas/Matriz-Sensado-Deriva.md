# Matriz de sensado de deriva — GeometriaFactory-Application

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** Matriz-Sensado-Deriva.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Developer Advocate / Sample Engineer Senior (AG-10)
**Variante:** Sensado de deriva por contratos de verificación
**Trazabilidad upstream:** [`../10-Examples/README.md`](../10-Examples/README.md) 1.1 §3 y los tres contratos de verificación de [`../10-Examples/ejemplo-01-basico.md`](../10-Examples/ejemplo-01-basico.md), [`../10-Examples/ejemplo-02-intermedio.md`](../10-Examples/ejemplo-02-intermedio.md) y [`../10-Examples/ejemplo-03-avanzado.md`](../10-Examples/ejemplo-03-avanzado.md), los tres 1.0; `Deriva-Rules.md` §2.3, §2.4, §3 y §4
**Trazabilidad downstream:** [`README.md`](README.md) §3 y [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §8, que declaraban su ausencia y ahora la citan; `09-Devops`, que resuelve dónde corren los comandos

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

`GeometriaFactory-Application` tiene `requiere_maqueta` en **false** (`PRODUCT-MANIFEST` §5): no ejecutó la Fase B2 y no tiene línea de base visual ni contrato de datos de maqueta. Hasta la emisión de `10-Examples` no había ninguna fuente de sondas, y por eso [`README.md`](README.md) §3 y [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §8 declararon la omisión **condicionada y temporal**, con la frase de cierre «cuando se emita la categoría 10, la matriz se abre con sus filas `VER-XX`».

Eso es lo que hace este documento. `Deriva-Rules.md` §2.3 lo prevé, y su texto completo es «Cuando el proyecto de código no ejecuta Fase B2 pero sí tiene categoría 10, la matriz se emite igual: **la abre AG-08 en la Fase E**, poblada solo con sondas `VER-XX` tomadas de los contratos de verificación» —el fragmento sobre la titularidad **no se elide**: el párrafo siguiente declara y fundamenta por qué acá la abre AG-10—. Una matriz sin filas sería un proyecto de código sin instrumento de sensado, y eso es lo que había que evitar.

**Quién la abre.** `Deriva-Rules.md` §2.3 se la asigna a AG-08 en la Fase E, para el caso general. Acá la abre **AG-10** al cerrar la fase que genera la categoría 10, que es el segundo momento de sensado de `Deriva-Rules.md` §4 —«alta de una sonda `VER-XX` por cada contrato de verificación declarado en la pasada de diseño, todas en `Sin verificar`»—, porque la Fase E de este proyecto de código ya cerró y en ese momento las sondas todavía no existían. **Ninguna fila cambia de titular por eso**: la incorporación a la estrategia de testing sigue siendo de AG-08.

**Ninguna fila afirma nada sobre el estado del sistema construido.** Las tres nacen en `Sin verificar` y sin fecha, porque la biblioteca no está construida.

## 2. Contra qué se sensa

Contra los **contratos de verificación** de los tres samples de `10-Examples`, y contra nada más. No hay identificadores `SUP-XX`, `CMP-XX`, `EST-XX`, `NAV-XX` ni `DM-XX` que citar, porque este proyecto de código no tiene superficie visual ni maqueta.

Es el caso que `Deriva-Rules.md` §2.4 describe: «no requieren maqueta… antes de esta extensión, esos proyectos de código quedaban sin ningún instrumento de sensado». Las tres sondas traen **su propio comando y su propia aserción**, de modo que su método de verificación es automatizable sin desvío, y su evidencia es el campo `evidencia` del sample, citado por identificador y no transcripto acá.

## 3. La matriz

| ID | Elemento de línea de base | Afirmación a verificar | Método de verificación | Evidencia esperada | Umbral de deriva | Estado | Última verificación |
| --- | --- | --- | --- | --- | --- | --- | --- |
| `SD-01` | `VER-01` de [`../10-Examples/ejemplo-01-basico.md`](../10-Examples/ejemplo-01-basico.md) §9 | La entrada al laboratorio cierra entera: el alta de alumno constituye la cuenta **sin credencial** y en situación pendiente, el segundo administrador se rechaza, la admisibilidad devuelve sus **tres** desenlaces con motivo, y la cuenta marcada queda confinada al reemplazo de su propia credencial —**la única excepción** de `ADR-04` §2— que es además lo único que levanta la marca. Los **4** actos producen **5** rechazos tipados y **0** excepciones | El comando del contrato: `dotnet run --project samples/application/01-basico` | Campo `evidencia` de `VER-01`, con su fecha | **Menor**: cambia el texto de una línea de salida sin cambiar su semántica. **Mayor**: el `criterio_aceptacion` falla, cambia el comando sin actualizar el contrato, aparecen precondiciones no declaradas, la comprobación de cambio de contraseña pendiente deja de cortar primero, o alguno de `CU-01`, `CU-03` o `CU-10` deja de estar cubierto | `Sin verificar` | — |
| `SD-02` | `VER-02` de [`../10-Examples/ejemplo-02-intermedio.md`](../10-Examples/ejemplo-02-intermedio.md) §9 | Los **ocho** escenarios reales del intake §20 recorren el ciclo del trabajo con el resultado de interpretación que **el puerto** devuelve: `E-1` da 3 piezas y 2 advertencias, `E-3` la advertencia con el par 36.00 y 54.00, `E-4` **0** observaciones, `E-5` y `E-8` la observación de error en el **índice 1**; el recuento final es **6** envíos a `Pendiente` y **2** retenidos en `Borrador`; el listado propio distingue los cuatro estados; y el retiro de un trabajo ajeno responde como **inexistente para el solicitante** y no como falta de facultad | El comando del contrato: `dotnet run --project samples/application/02-intermedio` | Campo `evidencia` de `VER-02`, con su fecha | **Menor**: cambia el texto de una línea sin cambiar su semántica. **Mayor**: el `criterio_aceptacion` falla, un escenario se sustituye por datos sintéticos o se reformatea, el índice reportado pasa a ser 0, la negativa por pertenencia se colapsa con la de facultad, o alguno de `CU-04`, `CU-05`, `CU-06` o `CU-09` deja de estar cubierto | `Sin verificar` | — |
| `SD-03` | `VER-03` de [`../10-Examples/ejemplo-03-avanzado.md`](../10-Examples/ejemplo-03-avanzado.md) §9 | El gobierno del administrador cierra entero: la baja exige el correo escrito y arrastra **2** trabajos; la entrega de la comisión muestra **3** trabajos con **0** borradores visibles; la negativa por **alcance** y la negativa por **facultad** se distinguen y no se intercambian; el desenlace sobre un estado terminal se rechaza; y el reseteo sobre una cuenta **bloqueada** conserva la situación y los **2** trabajos. Los **4** actos producen **8** rechazos tipados y **0** excepciones | El comando del contrato: `dotnet run --project samples/application/03-avanzado` | Campo `evidencia` de `VER-03`, con su fecha | **Mayor, sin gradación** en el tramo de borradores visibles: `RN-11` no admite tolerancia y un solo borrador visible es deriva mayor. **Menor** en el resto: cambia el texto de una línea sin cambiar su semántica. **Mayor** además si el `criterio_aceptacion` falla, si la negativa por alcance se colapsa con la de pertenencia, o si alguno de `CU-02`, `CU-07`, `CU-08` o `CU-11` deja de estar cubierto | `Sin verificar` | — |

**Tres filas, una por contrato de verificación, sin contratos huérfanos ni filas sin contrato que las respalde**, que es lo que exige `Deriva-Rules.md` §6. La correspondencia es uno a uno: `SD-01`↔`VER-01`, `SD-02`↔`VER-02`, `SD-03`↔`VER-03`.

**El método de verificación de las tres es el comando declarado en su contrato, sin desvío.** No hay ninguna fila resuelta por inspección, porque `Deriva-Rules.md` §2.4 declara que una sonda `VER-XX` «trae su propio comando y su propia aserción».

## 4. Umbrales de deriva aplicados

Se toman de la fila «Contratos y comportamiento (`VER-XX`)» de `Deriva-Rules.md` §3, sin agregarle dimensiones.

| Dimensión | Deriva menor, se registra y no bloquea | Deriva mayor, bloquea y exige decisión | Filas |
| --- | --- | --- | --- |
| Contratos y comportamiento (`VER-XX`) | Cambia el texto de un mensaje de salida sin cambiar su semántica, o cambia el formato de un registro | El `criterio_aceptacion` falla; cambia el comando de ejecución sin actualizar el contrato; aparecen precondiciones no declaradas; o el caso de uso que la sonda ejercita deja de estar cubierto | `SD-01`, `SD-02`, `SD-03` |

**Un tramo sin gradación.** El de borradores visibles de `SD-03` declara deriva mayor ante cualquier diferencia, porque verifica una regla de negocio —`RN-11`— que no admite tolerancia. Las tres reglas de arquitectura del producto no aparecen acá porque **ninguna se ejerce en esta capa**: `RA-01` y `RA-03` se verifican en la superficie que expone y en la pieza pública, y `RA-02` en el visor.

**Toda deriva mayor se resuelve por una de dos vías y nunca por omisión** (`Deriva-Rules.md` §3): se corrige la biblioteca para volver a lo que el contrato dice, o se cambia la especificación con aprobación humana explícita, en cuyo caso la categoría 02 la modifica, el sample se rehace y esta matriz se actualiza. **Un caso de uso no se cambia desde acá.**

**Un `criterio_aceptacion` en `Falla` es un hallazgo del incremento en curso** y no se resuelve borrando la fila (`Rules-Examples.md` §4.4 y §4.5).

## 5. Qué no sensa esta matriz

Se declara para que no se lea como cobertura completa del proyecto de código:

| Elemento | Quién lo verifica |
| --- | --- |
| El catálogo cerrado de **36** condiciones de `03`, recorrido en las dos direcciones | La batería de `tests/GeometriaFactory.Application.Tests`, por los casos de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) |
| Las **dieciséis** reglas de negocio y los **nueve** invariantes, uno por uno | Las tablas de regla y de invariante de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) |
| Cobertura de líneas, de ramas y medición de mutación | El pipeline de `09-Devops`; los umbrales y su carácter están en [`Criterios-Validacion.md`](Criterios-Validacion.md) |
| Los **once** quality gates de [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3 | Esa sección, con su carácter bloqueante o condicionado por gate |

**Las tres sondas no reemplazan a la batería de pruebas**: la complementan desde afuera, ejercitando la superficie pública tal como la ve la composición de raíz. Es la asimetría que `Deriva-Rules.md` §4 declara en su cuarto momento.

## 6. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **Corrección del hallazgo P2-2 del informe `G-10-Examples-Siete-Proyectos-r1.md` 1.0.** La §1 citaba `Deriva-Rules.md` §2.3 **elidiendo sin marca** el fragmento «la abre AG-08 en la Fase E», que es justamente el que asigna la titularidad y del que este documento se desvía en el párrafo siguiente. Se restituye la cita **completa**, contrastada carácter por carácter contra la fuente, y se remite explícitamente al párrafo «Quién la abre», que ya declaraba y fundamentaba el desvío hacia AG-10 apoyándose en el segundo momento de sensado de §4. Se actualiza la trazabilidad upstream al [`../10-Examples/README.md`](../10-Examples/README.md) en su **1.1**. **Ninguna fila, umbral, método ni evidencia esperada cambia.** Contrastado contra el texto vivo del `PRODUCT-INTAKE` **1.25**, en particular §16.1 y §18, y no contra lo que otro documento dice de ellas. Sube minor: corrige la forma de una cita, no una afirmación de sensado. |
| 1.0 | 2026-08-11 | Emisión inicial, abierta por AG-10 al cerrar la fase que genera la categoría 10, que es el segundo momento de sensado de `Deriva-Rules.md` §4. Cierra el hueco que [`README.md`](README.md) §3 y [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §8 declararon como condicionado y temporal. Declara **tres** filas, `SD-01` a `SD-03`, una por cada contrato de verificación de `10-Examples`, con el comando del contrato como método, el campo `evidencia` del sample como evidencia esperada, el umbral de la fila «Contratos y comportamiento» de `Deriva-Rules.md` §3 —con un tramo sin gradación en `SD-03` por `RN-11`— y estado `Sin verificar` sin fecha. Declara además qué **no** sensa, para que la matriz no se lea como cobertura completa del proyecto de código. |
