# Matriz de sensado de deriva — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** Matriz-Sensado-Deriva.md
**Versión:** 1.1
**Estado:** Aprobado
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

`GeometriaFactory-Api` tiene `requiere_maqueta` en **false** (`PRODUCT-MANIFEST` §5): no ejecutó la Fase B2 y no tiene línea de base visual ni contrato de datos de maqueta. Hasta la emisión de `10-Examples` no había ninguna fuente de sondas, y por eso [`README.md`](README.md) §3 y [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §8 declararon la omisión **condicionada y temporal**, con la frase de cierre «cuando se emita la categoría 10, la matriz se abre con sus filas `VER-XX`».

Eso es lo que hace este documento. `Deriva-Rules.md` §2.3 lo prevé, y su texto completo es «Cuando el proyecto de código no ejecuta Fase B2 pero sí tiene categoría 10, la matriz se emite igual: **la abre AG-08 en la Fase E**, poblada solo con sondas `VER-XX` tomadas de los contratos de verificación» —el fragmento sobre la titularidad **no se elide**: el párrafo siguiente declara y fundamenta por qué acá la abre AG-10—. Una matriz sin filas sería un proyecto de código sin instrumento de sensado, y eso es lo que había que evitar.

**Quién la abre.** `Deriva-Rules.md` §2.3 se la asigna a AG-08 en la Fase E, para el caso general. Acá la abre **AG-10** al cerrar la fase que genera la categoría 10, que es el segundo momento de sensado de `Deriva-Rules.md` §4 —«alta de una sonda `VER-XX` por cada contrato de verificación declarado en la pasada de diseño, todas en `Sin verificar`»—, porque la Fase E de este proyecto de código ya cerró y en ese momento las sondas todavía no existían. **Ninguna fila cambia de titular por eso**: la incorporación a la estrategia de testing sigue siendo de AG-08.

**Ninguna fila afirma nada sobre el estado del sistema construido.** Las tres nacen en `Sin verificar` y sin fecha, porque la biblioteca no está construida.

## 2. Contra qué se sensa

Contra los **contratos de verificación** de los tres samples de `10-Examples`, y contra nada más. No hay identificadores `SUP-XX`, `CMP-XX`, `EST-XX`, `NAV-XX` ni `DM-XX` que citar, porque este proyecto de código no tiene superficie visual ni maqueta.

Es el caso que `Deriva-Rules.md` §2.4 describe: «no requieren maqueta… antes de esta extensión, esos proyectos de código quedaban sin ningún instrumento de sensado». Las tres sondas traen **su propio comando y su propia aserción**, de modo que su método de verificación es automatizable sin desvío, y su evidencia es el campo `evidencia` del sample, citado por identificador y no transcripto acá.

## 3. La matriz

| ID | Elemento de línea de base | Afirmación a verificar | Método de verificación | Evidencia esperada | Umbral de deriva | Estado | Última verificación |
| --- | --- | --- | --- | --- | --- | --- | --- |
| `SD-01` | `VER-01` de [`../10-Examples/ejemplo-01-basico.md`](../10-Examples/ejemplo-01-basico.md) §9 | La frontera de la capa se sostiene en el recorrido corto: el canje devuelve **200** con acceso firmado y **401** genérico sin declarar qué campo falló; la guardia rechaza sin acceso, con acceso vencido, con firma ajena, con papel insuficiente y con la marca de cambio pendiente; y los envíos de `E-5` y `E-8` responden **201** con el trabajo en **`Borrador`** y la observación localizada en el **índice 1**. Las respuestas con dirección, ruta, traza o secreto son **0** | El comando del contrato: `bash samples/api/01-basico/run.sh` | Campo `evidencia` de `VER-01`, con su fecha | **Mayor, sin gradación** en el tramo de umbral cero de `RA-03` y en el del código de respuesta del envío: un `400` ante un texto que no verifica confunde la petición con el texto del alumno. **Menor** en el resto: cambia el texto de una línea sin cambiar su semántica. **Mayor** además si el `criterio_aceptacion` falla, si el índice reportado pasa a ser 0, si el canje declara qué campo falló, o si `CU-01`, `CU-02` o `CU-09` dejan de estar cubiertos | `Sin verificar` | — |
| `SD-02` | `VER-02` de [`../10-Examples/ejemplo-02-intermedio.md`](../10-Examples/ejemplo-02-intermedio.md) §9 | La **colección de peticiones reproducible** —muestra `S-2`— recorre la superficie ensamblada en **3** pasos con **0** cuerpos inventados y **0** modificados: los **8** escenarios responden con **8** éxitos, **6** trabajos en `Pendiente` y **2** en `Borrador`; el listado del administrador devuelve **6** con **0** borradores visibles; los **4** caminos prohibidos se rechazan **contra el servicio y no contra una pantalla**, con `404` para el trabajo ajeno y `409` para el estado que no lo permite; el reseteo confina al cambio y el cambio lo levanta; y los puntos ejercitados son **13 de 15** | El comando del contrato: `bash samples/api/02-intermedio/run.sh` | Campo `evidencia` de `VER-02`, con su fecha | **Mayor, sin gradación** en tres tramos: un cuerpo inventado o modificado (regla de delivery de `PRODUCT-INTAKE` §15, umbral **0**), un borrador visible en el listado del administrador (`RN-11`) y un `403` donde corresponde `404` (`RN-03`). **Menor** en el resto. **Mayor** además si el `criterio_aceptacion` falla, si los pasos de la colección pasan de **5**, o si alguno de `CU-03` a `CU-08` o `CU-12` deja de estar cubierto. **Si el Product Owner declara que rige el alcance de §18 S-2 —dos cuerpos en lugar de ocho—, esta fila se actualiza con el contrato y el cambio no es deriva** | `Sin verificar` | — |
| `SD-03` | `VER-03` de [`../10-Examples/ejemplo-03-avanzado.md`](../10-Examples/ejemplo-03-avanzado.md) §9 | Lo que ocurre **antes** de la primera petición se sostiene: la fase 1 aplica las transformaciones con **0** peticiones atendidas, la fase 2 expone **15** puntos de acceso, el punto de salud responde **200** sin acceso firmado y **503** con el almacén indisponible, el arranque sobre un linaje desconocido se **detiene** con **0** peticiones atendidas y su mensaje no lleva ruta, dirección ni traza, los puertos conectados son **4 de 4** con **0** fuera de la composición de raíz, la configuración de intercambio declarada es **1**, y no hay intercambio de origen cruzado ni canal de sesión interactiva | El comando del contrato: `bash samples/api/03-avanzado/run.sh` | Campo `evidencia` de `VER-03`, con su fecha | **Mayor, sin gradación** en los tramos de umbral cero, porque verifican `RA-01` y `RA-03`, que son reglas de nivel producto: peticiones atendidas en la fase 1, puertos conectados fuera de la composición de raíz, arranque que continúa sobre un almacén dudoso, y aparición de intercambio de origen cruzado o de canal de sesión interactiva. **Menor** en el resto. **Mayor** además si el `criterio_aceptacion` falla, si los puntos expuestos dejan de ser **15**, o si `CU-10` o `CU-11` dejan de estar cubiertos | `Sin verificar` | — |

**Tres filas, una por contrato de verificación, sin contratos huérfanos ni filas sin contrato que las respalde**, que es lo que exige `Deriva-Rules.md` §6. La correspondencia es uno a uno: `SD-01`↔`VER-01`, `SD-02`↔`VER-02`, `SD-03`↔`VER-03`.

**El método de verificación de las tres es el comando declarado en su contrato, sin desvío.** No hay ninguna fila resuelta por inspección, porque `Deriva-Rules.md` §2.4 declara que una sonda `VER-XX` «trae su propio comando y su propia aserción».

## 4. Umbrales de deriva aplicados

Se toman de la fila «Contratos y comportamiento (`VER-XX`)» de `Deriva-Rules.md` §3, sin agregarle dimensiones.

| Dimensión | Deriva menor, se registra y no bloquea | Deriva mayor, bloquea y exige decisión | Filas |
| --- | --- | --- | --- |
| Contratos y comportamiento (`VER-XX`) | Cambia el texto de un mensaje de salida sin cambiar su semántica, o cambia el formato de un registro | El `criterio_aceptacion` falla; cambia el comando de ejecución sin actualizar el contrato; aparecen precondiciones no declaradas; o el caso de uso que la sonda ejercita deja de estar cubierto | `SD-01`, `SD-02`, `SD-03` |

**Varios tramos sin gradación**, y conviene decir por qué son más que en los otros proyectos de código. **`RA-01` y `RA-03` se ejercen acá**: ésta es la superficie que el navegador no debe alcanzar y la última capa que toca un dato del backend antes de que salga del servidor propio. Los tramos de umbral cero de `SD-01` y de `SD-03` las verifican, y una regla de nivel producto no admite tolerancia. **`RA-02` no se ejerce acá**: es del visor. Los tramos sin gradación de `SD-02` verifican en cambio reglas de negocio —`RN-03` y `RN-11`— y la regla de delivery de `PRODUCT-INTAKE` §15 sobre datos de prueba inventados.

**Toda deriva mayor se resuelve por una de dos vías y nunca por omisión** (`Deriva-Rules.md` §3): se corrige la biblioteca para volver a lo que el contrato dice, o se cambia la especificación con aprobación humana explícita, en cuyo caso la categoría 02 la modifica, el sample se rehace y esta matriz se actualiza. **Un caso de uso no se cambia desde acá.**

**Un `criterio_aceptacion` en `Falla` es un hallazgo del incremento en curso** y no se resuelve borrando la fila (`Rules-Examples.md` §4.4 y §4.5).

## 5. Qué no sensa esta matriz

Se declara para que no se lea como cobertura completa del proyecto de código:

| Elemento | Quién lo verifica |
| --- | --- |
| Los **quince** códigos de contrato vivos, uno por uno y en las dos direcciones, y la tabla de traducción | La batería de `tests/GeometriaFactory.Integration.Tests`, por los casos de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) |
| Los umbrales de latencia y de caudal, y la forma de la pirámide | `TC-34` y `TC-37`, con sus gates condicionados por venir rotulados **[ASUNCIÓN]** |
| El punto de acceso **`A-08`**, la baja física de una cuenta | Las pruebas de integración. Ningún sample lo ejercita, y `CU-12` §10 declara el motivo: dejaría a la colección sin el alumno con el que sigue el recorrido |
| Cobertura de líneas y de ramas | El pipeline de `09-Devops`; los umbrales y su carácter están en [`Criterios-Validacion.md`](Criterios-Validacion.md) |
| Los **quince** quality gates de [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3 | Esa sección, con su carácter por gate. **`QG-15` sí queda ejercido por `SD-02`**, porque mide los pasos de la colección y los datos inventados |

**Las tres sondas no reemplazan a la batería de pruebas.** `CU-12` §9 lo declara para la colección —«no reemplaza a las pruebas de integración y no se cuenta como cobertura»— y esta matriz no lo cambia: lo que las sondas agregan es una **aserción evaluable** sobre una demostración que hasta ahora se leía a ojo. Es la asimetría que `Deriva-Rules.md` §4 declara en su cuarto momento.

## 6. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **Corrección del hallazgo P2-2 del informe `G-10-Examples-Siete-Proyectos-r1.md` 1.0.** La §1 citaba `Deriva-Rules.md` §2.3 **elidiendo sin marca** el fragmento «la abre AG-08 en la Fase E», que es justamente el que asigna la titularidad y del que este documento se desvía en el párrafo siguiente. Se restituye la cita **completa**, contrastada carácter por carácter contra la fuente, y se remite explícitamente al párrafo «Quién la abre», que ya declaraba y fundamentaba el desvío hacia AG-10 apoyándose en el segundo momento de sensado de §4. Se actualiza la trazabilidad upstream al [`../10-Examples/README.md`](../10-Examples/README.md) en su **1.1**. **Ninguna fila, umbral, método ni evidencia esperada cambia.** Contrastado contra el texto vivo del `PRODUCT-INTAKE` **1.25**, en particular §16.1 y §18, y no contra lo que otro documento dice de ellas. Sube minor: corrige la forma de una cita, no una afirmación de sensado. |
| 1.0 | 2026-08-11 | Emisión inicial del proyecto de código **principal** del producto, abierta por AG-10 al cerrar la fase que genera la categoría 10, que es el segundo momento de sensado de `Deriva-Rules.md` §4. Cierra el hueco que [`README.md`](README.md) §3 y [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §8 declararon como condicionado y temporal. Declara **tres** filas, `SD-01` a `SD-03`, una por cada contrato de verificación de `10-Examples`, con el comando del contrato como método, el campo `evidencia` del sample como evidencia esperada, el umbral de la fila «Contratos y comportamiento» de `Deriva-Rules.md` §3 —con un tramo sin gradación en `SD-03` por `RN-11`— y estado `Sin verificar` sin fecha. Declara además qué **no** sensa, para que la matriz no se lea como cobertura completa del proyecto de código. |
