# Matriz de sensado de deriva — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** Matriz-Sensado-Deriva.md
**Versión:** 2.0
**Estado:** Propuesto
**Fecha:** 2026-08-16
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**`tipo_unidad_entrega` (D8):** `rest-api` · **Unidad de entrega principal del producto**
**Proyectos de código que la componen:** `GeometriaFactory-Api`, `GeometriaFactory-Domain`, `GeometriaFactory-Application`, `GeometriaFactory-Infrastructure` y `GeometriaFactory-Contracts`
**Trazabilidad upstream:** [`Estrategia-Calidad.md`](Estrategia-Calidad.md); [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **2.1** §17.1.P.6 y §22
**Trazabilidad downstream:** `09-Devops` y `11-Documentacion`
**Consolida a:** los documentos homónimos de `GeometriaFactory-Domain`, `GeometriaFactory-Application` e `GeometriaFactory-Infrastructure`, por `Audit/Migracion-M10-Consolidacion-Fusion.md` 1.1 §4

---

## 0. Cómo leer este documento

**La unidad de entrega tiene un solo documento de esta clase, y sus cuatro proyectos de código tenían
el suyo.** Cada sección lleva **una subsección por proyecto**, con su texto **transpuesto sin
reescritura**: lo que cambia es el orden y no el contenido.

**Las cinco secciones son comunes a las cuatro capas.** La matriz de sensado de deriva es una
**colección derivada** (`Root-Rules.md` §9.4): su tamaño es la suma de las tablas de línea de base
que la alimentan, de modo que la de la unidad de entrega es la unión de las cuatro **por
construcción**, no por decisión.

---

## 1. Por qué existe esta matriz y por qué recién ahora

### 1.1 `GeometriaFactory-Api`

`GeometriaFactory-Api` tiene `requiere_maqueta` en **false** (`PRODUCT-MANIFEST` §5): no ejecutó la Fase B2 y no tiene línea de base visual ni contrato de datos de maqueta. Hasta la emisión de `10-Examples` no había ninguna fuente de sondas, y por eso [`README.md`](README.md) §3 y [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §8 declararon la omisión **condicionada y temporal**, con la frase de cierre «cuando se emita la categoría 10, la matriz se abre con sus filas `VER-XX`».

Eso es lo que hace este documento. `Deriva-Rules.md` §2.3 lo prevé, y su texto completo es «Cuando el proyecto de código no ejecuta Fase B2 pero sí tiene categoría 10, la matriz se emite igual: **la abre AG-08 en la Fase E**, poblada solo con sondas `VER-XX` tomadas de los contratos de verificación» —el fragmento sobre la titularidad **no se elide**: el párrafo siguiente declara y fundamenta por qué acá la abre AG-10—. Una matriz sin filas sería un proyecto de código sin instrumento de sensado, y eso es lo que había que evitar.

**Quién la abre.** `Deriva-Rules.md` §2.3 se la asigna a AG-08 en la Fase E, para el caso general. Acá la abre **AG-10** al cerrar la fase que genera la categoría 10, que es el segundo momento de sensado de `Deriva-Rules.md` §4 —«alta de una sonda `VER-XX` por cada contrato de verificación declarado en la pasada de diseño, todas en `Sin verificar`»—, porque la Fase E de este proyecto de código ya cerró y en ese momento las sondas todavía no existían. **Ninguna fila cambia de titular por eso**: la incorporación a la estrategia de testing sigue siendo de AG-08.

**Ninguna fila afirma nada sobre el estado del sistema construido.** Las tres nacen en `Sin verificar` y sin fecha, porque la biblioteca no está construida.

### 1.2 `GeometriaFactory-Domain`

`GeometriaFactory-Domain` tiene `requiere_maqueta` en **false** (`PRODUCT-MANIFEST` §5): no ejecutó la Fase B2 y no tiene línea de base visual ni contrato de datos de maqueta. Hasta la emisión de `10-Examples` no había ninguna fuente de sondas, y por eso [`README.md`](README.md) §3 y [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §7 declararon la omisión **condicionada y temporal**, con la frase de cierre «cuando se emita la categoría 10, la matriz se abre con sus filas `VER-XX`».

Eso es lo que hace este documento. `Deriva-Rules.md` §2.3 lo prevé, y su texto completo es «Cuando el proyecto de código no ejecuta Fase B2 pero sí tiene categoría 10, la matriz se emite igual: **la abre AG-08 en la Fase E**, poblada solo con sondas `VER-XX` tomadas de los contratos de verificación» —el fragmento sobre la titularidad **no se elide**: el párrafo siguiente declara y fundamenta por qué acá la abre AG-10—. Y §6 lo exige: «ningún proyecto de código con categoría 10 queda sin `Matriz-Sensado-Deriva.md`, aunque no haya ejecutado Fase B2».

**Quién la abre.** `Deriva-Rules.md` §2.3 se la asigna a AG-08 en la Fase E, para el caso general. Acá la abre **AG-10** al cerrar la fase que genera la categoría 10, que es el segundo momento de sensado de `Deriva-Rules.md` §4 —«alta de una sonda `VER-XX` por cada contrato de verificación declarado en la pasada de diseño, todas en `Sin verificar`»—, porque la Fase E de este proyecto de código ya cerró y en ese momento las sondas todavía no existían. **Ninguna fila cambia de titular por eso**: la incorporación a la estrategia de testing sigue siendo de AG-08.

**Ninguna fila afirma nada sobre el estado del sistema construido.** Las tres nacen en `Sin verificar` y sin fecha, porque la biblioteca no está construida.

### 1.3 `GeometriaFactory-Application`

`GeometriaFactory-Application` tiene `requiere_maqueta` en **false** (`PRODUCT-MANIFEST` §5): no ejecutó la Fase B2 y no tiene línea de base visual ni contrato de datos de maqueta. Hasta la emisión de `10-Examples` no había ninguna fuente de sondas, y por eso [`README.md`](README.md) §3 y [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §8 declararon la omisión **condicionada y temporal**, con la frase de cierre «cuando se emita la categoría 10, la matriz se abre con sus filas `VER-XX`».

Eso es lo que hace este documento. `Deriva-Rules.md` §2.3 lo prevé, y su texto completo es «Cuando el proyecto de código no ejecuta Fase B2 pero sí tiene categoría 10, la matriz se emite igual: **la abre AG-08 en la Fase E**, poblada solo con sondas `VER-XX` tomadas de los contratos de verificación» —el fragmento sobre la titularidad **no se elide**: el párrafo siguiente declara y fundamenta por qué acá la abre AG-10—. Una matriz sin filas sería un proyecto de código sin instrumento de sensado, y eso es lo que había que evitar.

**Quién la abre.** `Deriva-Rules.md` §2.3 se la asigna a AG-08 en la Fase E, para el caso general. Acá la abre **AG-10** al cerrar la fase que genera la categoría 10, que es el segundo momento de sensado de `Deriva-Rules.md` §4 —«alta de una sonda `VER-XX` por cada contrato de verificación declarado en la pasada de diseño, todas en `Sin verificar`»—, porque la Fase E de este proyecto de código ya cerró y en ese momento las sondas todavía no existían. **Ninguna fila cambia de titular por eso**: la incorporación a la estrategia de testing sigue siendo de AG-08.

**Ninguna fila afirma nada sobre el estado del sistema construido.** Las tres nacen en `Sin verificar` y sin fecha, porque la biblioteca no está construida.

### 1.4 `GeometriaFactory-Infrastructure`

`GeometriaFactory-Infrastructure` tiene `requiere_maqueta` en **false** (`PRODUCT-MANIFEST` §5): no ejecutó la Fase B2 y no tiene línea de base visual ni contrato de datos de maqueta. Hasta la emisión de `10-Examples` no había ninguna fuente de sondas, y por eso [`README.md`](README.md) §3 y [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §8 declararon la omisión **condicionada y temporal**, con la frase de cierre «cuando se emita la categoría 10, la matriz se abre con sus filas `VER-XX`».

Eso es lo que hace este documento. `Deriva-Rules.md` §2.3 lo prevé, y su texto completo es «Cuando el proyecto de código no ejecuta Fase B2 pero sí tiene categoría 10, la matriz se emite igual: **la abre AG-08 en la Fase E**, poblada solo con sondas `VER-XX` tomadas de los contratos de verificación» —el fragmento sobre la titularidad **no se elide**: el párrafo siguiente declara y fundamenta por qué acá la abre AG-10—. Una matriz sin filas sería un proyecto de código sin instrumento de sensado, y eso es lo que había que evitar.

**Quién la abre.** `Deriva-Rules.md` §2.3 se la asigna a AG-08 en la Fase E, para el caso general. Acá la abre **AG-10** al cerrar la fase que genera la categoría 10, que es el segundo momento de sensado de `Deriva-Rules.md` §4 —«alta de una sonda `VER-XX` por cada contrato de verificación declarado en la pasada de diseño, todas en `Sin verificar`»—, porque la Fase E de este proyecto de código ya cerró y en ese momento las sondas todavía no existían. **Ninguna fila cambia de titular por eso**: la incorporación a la estrategia de testing sigue siendo de AG-08.

**Ninguna fila afirma nada sobre el estado del sistema construido.** Las tres nacen en `Sin verificar` y sin fecha, porque la biblioteca no está construida.

## 2. Contra qué se sensa

### 2.1 `GeometriaFactory-Api`

Contra los **contratos de verificación** de los tres samples de `10-Examples`, y contra nada más. No hay identificadores `SUP-XX`, `CMP-XX`, `EST-XX`, `NAV-XX` ni `DM-XX` que citar, porque este proyecto de código no tiene superficie visual ni maqueta.

Es el caso que `Deriva-Rules.md` §2.4 describe: «no requieren maqueta… antes de esta extensión, esos proyectos de código quedaban sin ningún instrumento de sensado». Las tres sondas traen **su propio comando y su propia aserción**, de modo que su método de verificación es automatizable sin desvío, y su evidencia es el campo `evidencia` del sample, citado por identificador y no transcripto acá.

### 2.2 `GeometriaFactory-Domain`

Contra los **contratos de verificación** de los tres samples de `10-Examples`, y contra nada más. No hay identificadores `SUP-XX`, `CMP-XX`, `EST-XX`, `NAV-XX` ni `DM-XX` que citar, porque este proyecto de código no tiene superficie visual ni maqueta.

Es el caso que `Deriva-Rules.md` §2.4 describe: «no requieren maqueta… antes de esta extensión, esos proyectos de código quedaban sin ningún instrumento de sensado». Las tres sondas traen **su propio comando y su propia aserción**, de modo que su método de verificación es automatizable sin desvío, y su evidencia es el campo `evidencia` del sample, citado por identificador y no transcripto acá.

### 2.3 `GeometriaFactory-Application`

Contra los **contratos de verificación** de los tres samples de `10-Examples`, y contra nada más. No hay identificadores `SUP-XX`, `CMP-XX`, `EST-XX`, `NAV-XX` ni `DM-XX` que citar, porque este proyecto de código no tiene superficie visual ni maqueta.

Es el caso que `Deriva-Rules.md` §2.4 describe: «no requieren maqueta… antes de esta extensión, esos proyectos de código quedaban sin ningún instrumento de sensado». Las tres sondas traen **su propio comando y su propia aserción**, de modo que su método de verificación es automatizable sin desvío, y su evidencia es el campo `evidencia` del sample, citado por identificador y no transcripto acá.

### 2.4 `GeometriaFactory-Infrastructure`

Contra los **contratos de verificación** de los tres samples de `10-Examples`, y contra nada más. No hay identificadores `SUP-XX`, `CMP-XX`, `EST-XX`, `NAV-XX` ni `DM-XX` que citar, porque este proyecto de código no tiene superficie visual ni maqueta.

Es el caso que `Deriva-Rules.md` §2.4 describe: «no requieren maqueta… antes de esta extensión, esos proyectos de código quedaban sin ningún instrumento de sensado». Las tres sondas traen **su propio comando y su propia aserción**, de modo que su método de verificación es automatizable sin desvío, y su evidencia es el campo `evidencia` del sample, citado por identificador y no transcripto acá.

## 3. La matriz

### 3.1 `GeometriaFactory-Api`

| ID | Elemento de línea de base | Afirmación a verificar | Método de verificación | Evidencia esperada | Umbral de deriva | Estado | Última verificación |
| --- | --- | --- | --- | --- | --- | --- | --- |
| `SD-00001` | `VER-00001` de [`../10-Examples/ejemplo-01-basico.md`](../10-Examples/ejemplo-01-basico-api.md) §9 | La frontera de la capa se sostiene en el recorrido corto: el canje devuelve **200** con acceso firmado y **401** genérico sin declarar qué campo falló; la guardia rechaza sin acceso, con acceso vencido, con firma ajena, con papel insuficiente y con la marca de cambio pendiente; y los envíos de `E-5` y `E-8` responden **201** con el trabajo en **`Borrador`** y la observación localizada en el **índice 1**. Las respuestas con dirección, ruta, traza o secreto son **0** | El comando del contrato: `bash samples/api/01-basico/run.sh` | Campo `evidencia` de `VER-00001`, con su fecha | **Mayor, sin gradación** en el tramo de umbral cero de `RA-03` y en el del código de respuesta del envío: un `400` ante un texto que no verifica confunde la petición con el texto del alumno. **Menor** en el resto: cambia el texto de una línea sin cambiar su semántica. **Mayor** además si el `criterio_aceptacion` falla, si el índice reportado pasa a ser 0, si el canje declara qué campo falló, o si `CU-00001`, `CU-00002` o `CU-00009` dejan de estar cubiertos | `Sin verificar` | — |
| `SD-00002` | `VER-00002` de [`../10-Examples/ejemplo-02-intermedio.md`](../10-Examples/ejemplo-02-intermedio-api.md) §9 | La **colección de peticiones reproducible** —muestra `S-2`— recorre la superficie ensamblada en **3** pasos con **0** cuerpos inventados y **0** modificados: los **8** escenarios responden con **8** éxitos, **6** trabajos en `Pendiente` y **2** en `Borrador`; el listado del administrador devuelve **6** con **0** borradores visibles; los **4** caminos prohibidos se rechazan **contra el servicio y no contra una pantalla**, con `404` para el trabajo ajeno y `409` para el estado que no lo permite; el reseteo confina al cambio y el cambio lo levanta; y los puntos ejercitados son **13 de 15** | El comando del contrato: `bash samples/api/02-intermedio/run.sh` | Campo `evidencia` de `VER-00002`, con su fecha | **Mayor, sin gradación** en tres tramos: un cuerpo inventado o modificado (regla de delivery de `PRODUCT-INTAKE` §15, umbral **0**), un borrador visible en el listado del administrador (`RN-00011`) y un `403` donde corresponde `404` (`RN-00003`). **Menor** en el resto. **Mayor** además si el `criterio_aceptacion` falla, si los pasos de la colección pasan de **5**, o si alguno de `CU-00003` a `CU-00008` o `CU-00012` deja de estar cubierto. **Si el Product Owner declara que rige el alcance de §18 S-2 —dos cuerpos en lugar de ocho—, esta fila se actualiza con el contrato y el cambio no es deriva** | `Sin verificar` | — |
| `SD-00003` | `VER-00003` de [`../10-Examples/ejemplo-03-avanzado.md`](../10-Examples/ejemplo-03-avanzado-api.md) §9 | Lo que ocurre **antes** de la primera petición se sostiene: la fase 1 aplica las transformaciones con **0** peticiones atendidas, la fase 2 expone **15** puntos de acceso, el punto de salud responde **200** sin acceso firmado y **503** con el almacén indisponible, el arranque sobre un linaje desconocido se **detiene** con **0** peticiones atendidas y su mensaje no lleva ruta, dirección ni traza, los puertos conectados son **4 de 4** con **0** fuera de la composición de raíz, la configuración de intercambio declarada es **1**, y no hay intercambio de origen cruzado ni canal de sesión interactiva | El comando del contrato: `bash samples/api/03-avanzado/run.sh` | Campo `evidencia` de `VER-00003`, con su fecha | **Mayor, sin gradación** en los tramos de umbral cero, porque verifican `RA-01` y `RA-03`, que son reglas de nivel producto: peticiones atendidas en la fase 1, puertos conectados fuera de la composición de raíz, arranque que continúa sobre un almacén dudoso, y aparición de intercambio de origen cruzado o de canal de sesión interactiva. **Menor** en el resto. **Mayor** además si el `criterio_aceptacion` falla, si los puntos expuestos dejan de ser **15**, o si `CU-00010` o `CU-00011` dejan de estar cubiertos | `Sin verificar` | — |

**Tres filas, una por contrato de verificación, sin contratos huérfanos ni filas sin contrato que las respalde**, que es lo que exige `Deriva-Rules.md` §6. La correspondencia es uno a uno: `SD-00001`↔`VER-00001`, `SD-00002`↔`VER-00002`, `SD-00003`↔`VER-00003`.

**El método de verificación de las tres es el comando declarado en su contrato, sin desvío.** No hay ninguna fila resuelta por inspección, porque `Deriva-Rules.md` §2.4 declara que una sonda `VER-XX` «trae su propio comando y su propia aserción».

### 3.2 `GeometriaFactory-Domain`

| ID | Elemento de línea de base | Afirmación a verificar | Método de verificación | Evidencia esperada | Umbral de deriva | Estado | Última verificación |
| --- | --- | --- | --- | --- | --- | --- | --- |
| `SD-02001` | `VER-02001` de [`../10-Examples/ejemplo-01-basico.md`](../10-Examples/ejemplo-01-basico-dominio.md) §9 | El ciclo de vida de la cuenta cierra entero: la admisibilidad devuelve los **tres** desenlaces de `CU-02004` sobre la misma cuenta —`CUENTA_PENDIENTE`, `CAMBIO_DE_CONTRASENA_PENDIENTE` y admisible—, y las **9** operaciones invocadas producen **2** rechazos tipados y **0** excepciones | El comando del contrato: `dotnet run --project samples/domain/01-basico` | Campo `evidencia` de `VER-02001`, con su fecha | **Menor**: cambia el texto de una línea de salida sin cambiar su semántica. **Mayor**: el `criterio_aceptacion` falla, cambia el comando sin actualizar el contrato, aparecen precondiciones no declaradas, o alguno de `CU-02001`, `CU-02002`, `CU-02003`, `CU-02004` o `CU-02012` deja de estar cubierto | `Sin verificar` | — |
| `SD-02002` | `VER-02002` de [`../10-Examples/ejemplo-02-intermedio.md`](../10-Examples/ejemplo-02-intermedio-dominio.md) §9 | Los **seis** escenarios reales del intake §20 recorren el ciclo del trabajo: `E-1` adopta **3** piezas y **2** advertencias y pasa a `Pendiente`; `E-3` produce la advertencia con el par 36.00 y 54.00 y `E-4` produce **0** observaciones; `E-5` y `E-8` quedan retenidos en `Borrador` con la observación localizada en el **índice 1**; el recuento final es **4** envíos a `Pendiente` y **2** retenidos | El comando del contrato: `dotnet run --project samples/domain/02-intermedio` | Campo `evidencia` de `VER-02002`, con su fecha | **Menor**: cambia el texto de una línea sin cambiar su semántica. **Mayor**: el `criterio_aceptacion` falla, un escenario se sustituye por datos sintéticos, el índice reportado pasa a ser 0, o alguno de `CU-02005` a `CU-02008` deja de estar cubierto | `Sin verificar` | — |
| `SD-02003` | `VER-02003` de [`../10-Examples/ejemplo-03-avanzado.md`](../10-Examples/ejemplo-03-avanzado-dominio.md) §9 | El trabajo ajeno y el inexistente devuelven resultados **idénticos campo por campo**; el alcance del administrador excluye el **1** borrador y admite eliminación en los **3** estados que ve; el reseteo conserva la situación y los **4** trabajos; y las tres inspecciones dan **0** dependencias salientes, resultado idéntico en dos corridas sin fijar el reloj, y **12** condiciones devueltas por valor con **0** excepciones de negocio | El comando del contrato: `dotnet run --project samples/domain/03-avanzado` | Campo `evidencia` de `VER-02003`, con su fecha | **Menor**: cambia el texto de una línea sin cambiar su semántica. **Mayor**: el `criterio_aceptacion` falla, aparece una excepción de negocio, el recuento de dependencias salientes deja de ser 0, o alguno de `CU-02009`, `CU-02010`, `CU-02011` o `CU-02013` deja de estar cubierto | `Sin verificar` | — |

**Tres filas, una por contrato de verificación, sin contratos huérfanos ni filas sin contrato que las respalde**, que es lo que exige `Deriva-Rules.md` §6. La correspondencia es uno a uno: `SD-02001`↔`VER-02001`, `SD-02002`↔`VER-02002`, `SD-02003`↔`VER-02003`.

**El método de verificación de las tres es el comando declarado en su contrato, sin desvío.** No hay ninguna fila resuelta por inspección, porque `Deriva-Rules.md` §2.4 declara que una sonda `VER-XX` «trae su propio comando y su propia aserción».

### 3.3 `GeometriaFactory-Application`

| ID | Elemento de línea de base | Afirmación a verificar | Método de verificación | Evidencia esperada | Umbral de deriva | Estado | Última verificación |
| --- | --- | --- | --- | --- | --- | --- | --- |
| `SD-04001` | `VER-04001` de [`../10-Examples/ejemplo-01-basico.md`](../10-Examples/ejemplo-01-basico-aplicacion.md) §9 | La entrada al laboratorio cierra entera: el alta de alumno constituye la cuenta **sin credencial** y en situación pendiente, el segundo administrador se rechaza, la admisibilidad devuelve sus **tres** desenlaces con motivo, y la cuenta marcada queda confinada al reemplazo de su propia credencial —**la única excepción** de `ADR-04004` §2— que es además lo único que levanta la marca. Los **4** actos producen **5** rechazos tipados y **0** excepciones | El comando del contrato: `dotnet run --project samples/application/01-basico` | Campo `evidencia` de `VER-04001`, con su fecha | **Menor**: cambia el texto de una línea de salida sin cambiar su semántica. **Mayor**: el `criterio_aceptacion` falla, cambia el comando sin actualizar el contrato, aparecen precondiciones no declaradas, la comprobación de cambio de contraseña pendiente deja de cortar primero, o alguno de `CU-04001`, `CU-04003` o `CU-04010` deja de estar cubierto | `Sin verificar` | — |
| `SD-04002` | `VER-04002` de [`../10-Examples/ejemplo-02-intermedio.md`](../10-Examples/ejemplo-02-intermedio-aplicacion.md) §9 | Los **ocho** escenarios reales del intake §20 recorren el ciclo del trabajo con el resultado de interpretación que **el puerto** devuelve: `E-1` da 3 piezas y 2 advertencias, `E-3` la advertencia con el par 36.00 y 54.00, `E-4` **0** observaciones, `E-5` y `E-8` la observación de error en el **índice 1**; el recuento final es **6** envíos a `Pendiente` y **2** retenidos en `Borrador`; el listado propio distingue los cuatro estados; y el retiro de un trabajo ajeno responde como **inexistente para el solicitante** y no como falta de facultad | El comando del contrato: `dotnet run --project samples/application/02-intermedio` | Campo `evidencia` de `VER-04002`, con su fecha | **Menor**: cambia el texto de una línea sin cambiar su semántica. **Mayor**: el `criterio_aceptacion` falla, un escenario se sustituye por datos sintéticos o se reformatea, el índice reportado pasa a ser 0, la negativa por pertenencia se colapsa con la de facultad, o alguno de `CU-04004`, `CU-04005`, `CU-04006` o `CU-04009` deja de estar cubierto | `Sin verificar` | — |
| `SD-04003` | `VER-04003` de [`../10-Examples/ejemplo-03-avanzado.md`](../10-Examples/ejemplo-03-avanzado-aplicacion.md) §9 | El gobierno del administrador cierra entero: la baja exige el correo escrito y arrastra **2** trabajos; la entrega de la comisión muestra **3** trabajos con **0** borradores visibles; la negativa por **alcance** y la negativa por **facultad** se distinguen y no se intercambian; el desenlace sobre un estado terminal se rechaza; y el reseteo sobre una cuenta **bloqueada** conserva la situación y los **2** trabajos. Los **4** actos producen **8** rechazos tipados y **0** excepciones | El comando del contrato: `dotnet run --project samples/application/03-avanzado` | Campo `evidencia` de `VER-04003`, con su fecha | **Mayor, sin gradación** en el tramo de borradores visibles: `RN-04011` no admite tolerancia y un solo borrador visible es deriva mayor. **Menor** en el resto: cambia el texto de una línea sin cambiar su semántica. **Mayor** además si el `criterio_aceptacion` falla, si la negativa por alcance se colapsa con la de pertenencia, o si alguno de `CU-04002`, `CU-04007`, `CU-04008` o `CU-04011` deja de estar cubierto | `Sin verificar` | — |

**Tres filas, una por contrato de verificación, sin contratos huérfanos ni filas sin contrato que las respalde**, que es lo que exige `Deriva-Rules.md` §6. La correspondencia es uno a uno: `SD-04001`↔`VER-04001`, `SD-04002`↔`VER-04002`, `SD-04003`↔`VER-04003`.

**El método de verificación de las tres es el comando declarado en su contrato, sin desvío.** No hay ninguna fila resuelta por inspección, porque `Deriva-Rules.md` §2.4 declara que una sonda `VER-XX` «trae su propio comando y su propia aserción».

### 3.4 `GeometriaFactory-Infrastructure`

| ID | Elemento de línea de base | Afirmación a verificar | Método de verificación | Evidencia esperada | Umbral de deriva | Estado | Última verificación |
| --- | --- | --- | --- | --- | --- | --- | --- |
| `SD-06001` | `VER-06001` de [`../10-Examples/ejemplo-01-basico.md`](../10-Examples/ejemplo-01-basico-infraestructura.md) §9 | La mitad de la capa que **no abre el almacén** hace lo que promete sobre los **ocho** escenarios reales del intake §20 leídos como texto literal: `E-1` da **3** figuras del conjunto raíz, **3** piezas y **2** observaciones, con el cilindro en **0**; `E-2` parsea con comas finales y lee `Tapas` como bases; `E-4` da **0** observaciones frente a la advertencia de `E-3`; `E-6` no descarta la figura de dimensión `0.00`; `E-7` reconstruye **6** piezas; y `E-5` y `E-8` localizan el error en el **índice 1**. El recuento final es **2** observaciones de error, **4** advertencias y **0** excepciones | El comando del contrato: `dotnet run --project samples/infrastructure/01-basico` | Campo `evidencia` de `VER-06001`, con su fecha | **Menor**: cambia el texto de una línea de salida sin cambiar su semántica. **Mayor**: el `criterio_aceptacion` falla, cambia el comando sin actualizar el contrato, aparecen precondiciones no declaradas, el operador de tolerancia deja de ser estricto, un escenario se sustituye o se reformatea, el índice reportado pasa a ser 0, o `CU-06001` o `CU-06002` dejan de estar cubiertos | `Sin verificar` | — |
| `SD-06002` | `VER-06002` de [`../10-Examples/ejemplo-02-intermedio.md`](../10-Examples/ejemplo-02-intermedio-infraestructura.md) §9 | La mitad que **sí abre el almacén** sostiene sus cuatro propiedades: el trabajo de `E-1` se materializa con **3** piezas, **15** componentes y **2** observaciones y su texto se guarda **literal**; el listado devuelve **0** componentes y sin texto original, y el detalle los lleva; la consulta **sin alcance declarado** se rechaza en lugar de resolverse afuera; el retiro deja **0** dependientes; y el arrastre de la baja es todo o nada —**0** trabajos si se completa, **2** si se interrumpe, nunca **1**— | El comando del contrato: `dotnet run --project samples/infrastructure/02-intermedio` | Campo `evidencia` de `VER-06002`, con su fecha | **Mayor, sin gradación** en el tramo del texto original y en el del arrastre: `RN-06008` y el todo o nada de `ADR-06002` no admiten tolerancia. **Menor** en el resto: cambia el texto de una línea sin cambiar su semántica. **Mayor** además si el `criterio_aceptacion` falla, si el listado empieza a traer componentes, o si alguno de `CU-06003`, `CU-06004` o `CU-06005` deja de estar cubierto | `Sin verificar` | — |
| `SD-06003` | `VER-06003` de [`../10-Examples/ejemplo-03-avanzado.md`](../10-Examples/ejemplo-03-avanzado-infraestructura.md) §9 | Los **cinco** mecanismos que sólo esta capa provee se detienen en lugar de cumplir a medias: la derivación no guarda nada en claro y distingue el derivado **ilegible** del veredicto falso; **100** provisorias con **0** repetidas y ninguna derivada de un dato de la cuenta, y **0** valores producidos cuando la fuente de aleatoriedad no responde; el acceso lleva sus **4** reclamos y **0** accesos se emiten sin clave de firma; el sello del reloj llega por el puerto y dos corridas con el puerto fijado dan el mismo valor; y el arranque se **detiene** ante un linaje desconocido. Las **2** inspecciones de umbral cero dan **0** y **0** | El comando del contrato: `dotnet run --project samples/infrastructure/03-avanzado` | Campo `evidencia` de `VER-06003`, con su fecha | **Mayor, sin gradación** en los cuatro tramos de modo de falla silencioso y en las **2** inspecciones de umbral cero: contraseña en claro guardada, provisoria repetida, provisoria derivada de un dato de la cuenta, valor producido sin aleatoriedad, y toda aparición de clave, contraseña o ruta del almacén. **Menor** en el resto. **Mayor** además si el `criterio_aceptacion` falla, si una corrida se hace **sin** las condiciones de medición declaradas en las precondiciones, o si alguno de `CU-06006` a `CU-06010` deja de estar cubierto | `Sin verificar` | — |

**Tres filas, una por contrato de verificación, sin contratos huérfanos ni filas sin contrato que las respalde**, que es lo que exige `Deriva-Rules.md` §6. La correspondencia es uno a uno: `SD-06001`↔`VER-06001`, `SD-06002`↔`VER-06002`, `SD-06003`↔`VER-06003`.

**El método de verificación de las tres es el comando declarado en su contrato, sin desvío.** No hay ninguna fila resuelta por inspección, porque `Deriva-Rules.md` §2.4 declara que una sonda `VER-XX` «trae su propio comando y su propia aserción».

## 4. Umbrales de deriva aplicados

### 4.1 `GeometriaFactory-Api`

Se toman de la fila «Contratos y comportamiento (`VER-XX`)» de `Deriva-Rules.md` §3, sin agregarle dimensiones.

| Dimensión | Deriva menor, se registra y no bloquea | Deriva mayor, bloquea y exige decisión | Filas |
| --- | --- | --- | --- |
| Contratos y comportamiento (`VER-XX`) | Cambia el texto de un mensaje de salida sin cambiar su semántica, o cambia el formato de un registro | El `criterio_aceptacion` falla; cambia el comando de ejecución sin actualizar el contrato; aparecen precondiciones no declaradas; o el caso de uso que la sonda ejercita deja de estar cubierto | `SD-00001`, `SD-00002`, `SD-00003` |

**Varios tramos sin gradación**, y conviene decir por qué son más que en los otros proyectos de código. **`RA-01` y `RA-03` se ejercen acá**: ésta es la superficie que el navegador no debe alcanzar y la última capa que toca un dato del backend antes de que salga del servidor propio. Los tramos de umbral cero de `SD-00001` y de `SD-00003` las verifican, y una regla de nivel producto no admite tolerancia. **`RA-02` no se ejerce acá**: es del visor. Los tramos sin gradación de `SD-00002` verifican en cambio reglas de negocio —`RN-00003` y `RN-00011`— y la regla de delivery de `PRODUCT-INTAKE` §15 sobre datos de prueba inventados.

**Toda deriva mayor se resuelve por una de dos vías y nunca por omisión** (`Deriva-Rules.md` §3): se corrige la biblioteca para volver a lo que el contrato dice, o se cambia la especificación con aprobación humana explícita, en cuyo caso la categoría 02 la modifica, el sample se rehace y esta matriz se actualiza. **Un caso de uso no se cambia desde acá.**

**Un `criterio_aceptacion` en `Falla` es un hallazgo del incremento en curso** y no se resuelve borrando la fila (`Rules-Examples.md` §4.4 y §4.5).

### 4.2 `GeometriaFactory-Domain`

Se toman de la fila «Contratos y comportamiento (`VER-XX`)» de `Deriva-Rules.md` §3, sin agregarle dimensiones.

| Dimensión | Deriva menor, se registra y no bloquea | Deriva mayor, bloquea y exige decisión | Filas |
| --- | --- | --- | --- |
| Contratos y comportamiento (`VER-XX`) | Cambia el texto de un mensaje de salida sin cambiar su semántica, o cambia el formato de un registro | El `criterio_aceptacion` falla; cambia el comando de ejecución sin actualizar el contrato; aparecen precondiciones no declaradas; o el caso de uso que la sonda ejercita deja de estar cubierto | `SD-02001`, `SD-02002`, `SD-02003` |

**Toda deriva mayor se resuelve por una de dos vías y nunca por omisión** (`Deriva-Rules.md` §3): se corrige la biblioteca para volver a lo que el contrato dice, o se cambia la especificación con aprobación humana explícita, en cuyo caso la categoría 02 la modifica, el sample se rehace y esta matriz se actualiza. **Un caso de uso no se cambia desde acá.**

**Un `criterio_aceptacion` en `Falla` es un hallazgo del incremento en curso** y no se resuelve borrando la fila (`Rules-Examples.md` §4.4 y §4.5).

### 4.3 `GeometriaFactory-Application`

Se toman de la fila «Contratos y comportamiento (`VER-XX`)» de `Deriva-Rules.md` §3, sin agregarle dimensiones.

| Dimensión | Deriva menor, se registra y no bloquea | Deriva mayor, bloquea y exige decisión | Filas |
| --- | --- | --- | --- |
| Contratos y comportamiento (`VER-XX`) | Cambia el texto de un mensaje de salida sin cambiar su semántica, o cambia el formato de un registro | El `criterio_aceptacion` falla; cambia el comando de ejecución sin actualizar el contrato; aparecen precondiciones no declaradas; o el caso de uso que la sonda ejercita deja de estar cubierto | `SD-04001`, `SD-04002`, `SD-04003` |

**Un tramo sin gradación.** El de borradores visibles de `SD-04003` declara deriva mayor ante cualquier diferencia, porque verifica una regla de negocio —`RN-04011`— que no admite tolerancia. Las tres reglas de arquitectura del producto no aparecen acá porque **ninguna se ejerce en esta capa**: `RA-01` y `RA-03` se verifican en la superficie que expone y en la pieza pública, y `RA-02` en el visor.

**Toda deriva mayor se resuelve por una de dos vías y nunca por omisión** (`Deriva-Rules.md` §3): se corrige la biblioteca para volver a lo que el contrato dice, o se cambia la especificación con aprobación humana explícita, en cuyo caso la categoría 02 la modifica, el sample se rehace y esta matriz se actualiza. **Un caso de uso no se cambia desde acá.**

**Un `criterio_aceptacion` en `Falla` es un hallazgo del incremento en curso** y no se resuelve borrando la fila (`Rules-Examples.md` §4.4 y §4.5).

### 4.4 `GeometriaFactory-Infrastructure`

Se toman de la fila «Contratos y comportamiento (`VER-XX`)» de `Deriva-Rules.md` §3, sin agregarle dimensiones.

| Dimensión | Deriva menor, se registra y no bloquea | Deriva mayor, bloquea y exige decisión | Filas |
| --- | --- | --- | --- |
| Contratos y comportamiento (`VER-XX`) | Cambia el texto de un mensaje de salida sin cambiar su semántica, o cambia el formato de un registro | El `criterio_aceptacion` falla; cambia el comando de ejecución sin actualizar el contrato; aparecen precondiciones no declaradas; o el caso de uso que la sonda ejercita deja de estar cubierto | `SD-06001`, `SD-06002`, `SD-06003` |

**Varios tramos sin gradación.** Los declaran `SD-06002` —texto original y todo o nada del arrastre— y `SD-06003` —los cuatro modos de falla silenciosos y las dos inspecciones de umbral cero—, porque verifican reglas de negocio y prohibiciones de exposición que no admiten tolerancia. **`RA-03` sí se ejerce en esta capa**, y por eso está adentro de `SD-06003`: es la capa que conoce el valor derivado de una credencial, la clave de firma y la ruta del almacén, y de que no los exponga depende que la regla siga siendo cierta aguas arriba. `RA-01` y `RA-02` no se ejercen acá: la primera se verifica en la superficie que expone y en la pieza pública, y la segunda en el visor.

**Toda deriva mayor se resuelve por una de dos vías y nunca por omisión** (`Deriva-Rules.md` §3): se corrige la biblioteca para volver a lo que el contrato dice, o se cambia la especificación con aprobación humana explícita, en cuyo caso la categoría 02 la modifica, el sample se rehace y esta matriz se actualiza. **Un caso de uso no se cambia desde acá.**

**Un `criterio_aceptacion` en `Falla` es un hallazgo del incremento en curso** y no se resuelve borrando la fila (`Rules-Examples.md` §4.4 y §4.5).

## 5. Qué no sensa esta matriz

### 5.1 `GeometriaFactory-Api`

Se declara para que no se lea como cobertura completa del proyecto de código:

| Elemento | Quién lo verifica |
| --- | --- |
| Los **diecisiete** códigos de contrato vivos, uno por uno y en las dos direcciones, y la tabla de traducción | La batería de `tests/GeometriaFactory.Integration.Tests`, por los casos de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) |
| Los umbrales de latencia y de caudal, y la forma de la pirámide | `TC-00034` y `TC-00037`, con sus gates condicionados por venir rotulados **[ASUNCIÓN]** |
| El punto de acceso **`A-08`**, la baja física de una cuenta | Las pruebas de integración. Ningún sample lo ejercita, y `CU-00012` §10 declara el motivo: dejaría a la colección sin el alumno con el que sigue el recorrido |
| Cobertura de líneas y de ramas | El pipeline de `09-Devops`; los umbrales y su carácter están en [`Criterios-Validacion.md`](Criterios-Validacion.md) |
| Los **quince** quality gates de [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3 | Esa sección, con su carácter por gate. **`QG-15` sí queda ejercido por `SD-00002`**, porque mide los pasos de la colección y los datos inventados |

**Las tres sondas no reemplazan a la batería de pruebas.** `CU-00012` §9 lo declara para la colección —«no reemplaza a las pruebas de integración y no se cuenta como cobertura»— y esta matriz no lo cambia: lo que las sondas agregan es una **aserción evaluable** sobre una demostración que hasta ahora se leía a ojo. Es la asimetría que `Deriva-Rules.md` §4 declara en su cuarto momento.

### 5.2 `GeometriaFactory-Domain`

Se declara para que no se lea como cobertura completa del proyecto de código:

| Elemento | Quién lo verifica |
| --- | --- |
| Las **42** condiciones del catálogo de `03`, en las dos direcciones | `TC-02023` de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md), en la batería de `tests/` |
| Los **nueve** invariantes ejercidos sin dobles | `TC-02026`, sobre §5 de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) |
| Cobertura de líneas y de ramas, y tiempo de la batería | El pipeline de `09-Devops`; son los dos gates condicionados `QG-03` y `QG-07` |
| Las **dieciséis** reglas de negocio, una por una | Los **veintisiete** casos de prueba de `TC-02001` a `TC-02027` |

**Las tres sondas no reemplazan a la batería de pruebas**: la complementan desde afuera, ejercitando la superficie pública tal como la ve un consumidor. Es la asimetría que `Deriva-Rules.md` §4 declara en su cuarto momento.

### 5.3 `GeometriaFactory-Application`

Se declara para que no se lea como cobertura completa del proyecto de código:

| Elemento | Quién lo verifica |
| --- | --- |
| El catálogo cerrado de **36** condiciones de `03`, recorrido en las dos direcciones | La batería de `tests/GeometriaFactory.Application.Tests`, por los casos de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) |
| Las **dieciséis** reglas de negocio y los **nueve** invariantes, uno por uno | Las tablas de regla y de invariante de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) |
| Cobertura de líneas, de ramas y medición de mutación | El pipeline de `09-Devops`; los umbrales y su carácter están en [`Criterios-Validacion.md`](Criterios-Validacion.md) |
| Los **once** quality gates de [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3 | Esa sección, con su carácter bloqueante o condicionado por gate |

**Las tres sondas no reemplazan a la batería de pruebas**: la complementan desde afuera, ejercitando la superficie pública tal como la ve la composición de raíz. Es la asimetría que `Deriva-Rules.md` §4 declara en su cuarto momento.

### 5.4 `GeometriaFactory-Infrastructure`

Se declara para que no se lea como cobertura completa del proyecto de código:

| Elemento | Quién lo verifica |
| --- | --- |
| Las **17** condiciones del catálogo de `03`, recorridas en las dos direcciones | La batería de `tests/`, por los casos de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) |
| Las **siete** reglas conceptuales `RC-06001` a `RC-06007` del modelo | §5 de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) |
| Los **diez** casos de la batería obligatoria del producto | Los diez primeros de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md), que corren en el pipeline. **`SD-06001` no los reemplaza**: sensa que el sample siga produciendo lo que esa batería exige |
| Cobertura de líneas y de ramas, y el piso propio del validador | El pipeline de `09-Devops`; los umbrales y su carácter están en [`Criterios-Validacion.md`](Criterios-Validacion.md) |
| Los **catorce** quality gates de [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3 | Esa sección, con su carácter por gate |

**Las tres sondas no reemplazan a la batería de pruebas**: la complementan desde afuera, ejercitando la superficie pública tal como la ve la composición de raíz de `GeometriaFactory-Api`. Es la asimetría que `Deriva-Rules.md` §4 declara en su cuarto momento.

## 6. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 2.0 | 2026-08-16 | **Consolidación de la fusión** (`Audit/Migracion-M10-Consolidacion-Fusion.md` 1.1 §4). Pasa de ser el documento del proyecto de código `GeometriaFactory-Api` a ser el de la **unidad de entrega**, absorbiendo los homónimos de `GeometriaFactory-Domain`, `-Application` e `-Infrastructure`. Cada sección lleva **una subsección por proyecto de código**, con su texto transpuesto **sin reescritura**. Entra **§0** con lo que sólo se ve con los cuatro juntos. Los tres documentos absorbidos quedan archivados en `_legacy/2026-08-16-consolidacion-m10/`. Sube **major**. |
