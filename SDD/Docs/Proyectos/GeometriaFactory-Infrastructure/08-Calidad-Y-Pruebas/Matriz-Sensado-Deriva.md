# Matriz de sensado de deriva — GeometriaFactory-Infrastructure

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
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

`GeometriaFactory-Infrastructure` tiene `requiere_maqueta` en **false** (`PRODUCT-MANIFEST` §5): no ejecutó la Fase B2 y no tiene línea de base visual ni contrato de datos de maqueta. Hasta la emisión de `10-Examples` no había ninguna fuente de sondas, y por eso [`README.md`](README.md) §3 y [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §8 declararon la omisión **condicionada y temporal**, con la frase de cierre «cuando se emita la categoría 10, la matriz se abre con sus filas `VER-XX`».

Eso es lo que hace este documento. `Deriva-Rules.md` §2.3 lo prevé, y su texto completo es «Cuando el proyecto de código no ejecuta Fase B2 pero sí tiene categoría 10, la matriz se emite igual: **la abre AG-08 en la Fase E**, poblada solo con sondas `VER-XX` tomadas de los contratos de verificación» —el fragmento sobre la titularidad **no se elide**: el párrafo siguiente declara y fundamenta por qué acá la abre AG-10—. Una matriz sin filas sería un proyecto de código sin instrumento de sensado, y eso es lo que había que evitar.

**Quién la abre.** `Deriva-Rules.md` §2.3 se la asigna a AG-08 en la Fase E, para el caso general. Acá la abre **AG-10** al cerrar la fase que genera la categoría 10, que es el segundo momento de sensado de `Deriva-Rules.md` §4 —«alta de una sonda `VER-XX` por cada contrato de verificación declarado en la pasada de diseño, todas en `Sin verificar`»—, porque la Fase E de este proyecto de código ya cerró y en ese momento las sondas todavía no existían. **Ninguna fila cambia de titular por eso**: la incorporación a la estrategia de testing sigue siendo de AG-08.

**Ninguna fila afirma nada sobre el estado del sistema construido.** Las tres nacen en `Sin verificar` y sin fecha, porque la biblioteca no está construida.

## 2. Contra qué se sensa

Contra los **contratos de verificación** de los tres samples de `10-Examples`, y contra nada más. No hay identificadores `SUP-XX`, `CMP-XX`, `EST-XX`, `NAV-XX` ni `DM-XX` que citar, porque este proyecto de código no tiene superficie visual ni maqueta.

Es el caso que `Deriva-Rules.md` §2.4 describe: «no requieren maqueta… antes de esta extensión, esos proyectos de código quedaban sin ningún instrumento de sensado». Las tres sondas traen **su propio comando y su propia aserción**, de modo que su método de verificación es automatizable sin desvío, y su evidencia es el campo `evidencia` del sample, citado por identificador y no transcripto acá.

## 3. La matriz

| ID | Elemento de línea de base | Afirmación a verificar | Método de verificación | Evidencia esperada | Umbral de deriva | Estado | Última verificación |
| --- | --- | --- | --- | --- | --- | --- | --- |
| `SD-01` | `VER-01` de [`../10-Examples/ejemplo-01-basico.md`](../10-Examples/ejemplo-01-basico.md) §9 | La mitad de la capa que **no abre el almacén** hace lo que promete sobre los **ocho** escenarios reales del intake §20 leídos como texto literal: `E-1` da **3** figuras del conjunto raíz, **3** piezas y **2** observaciones, con el cilindro en **0**; `E-2` parsea con comas finales y lee `Tapas` como bases; `E-4` da **0** observaciones frente a la advertencia de `E-3`; `E-6` no descarta la figura de dimensión `0.00`; `E-7` reconstruye **6** piezas; y `E-5` y `E-8` localizan el error en el **índice 1**. El recuento final es **2** observaciones de error, **4** advertencias y **0** excepciones | El comando del contrato: `dotnet run --project samples/infrastructure/01-basico` | Campo `evidencia` de `VER-01`, con su fecha | **Menor**: cambia el texto de una línea de salida sin cambiar su semántica. **Mayor**: el `criterio_aceptacion` falla, cambia el comando sin actualizar el contrato, aparecen precondiciones no declaradas, el operador de tolerancia deja de ser estricto, un escenario se sustituye o se reformatea, el índice reportado pasa a ser 0, o `CU-01` o `CU-02` dejan de estar cubiertos | `Sin verificar` | — |
| `SD-02` | `VER-02` de [`../10-Examples/ejemplo-02-intermedio.md`](../10-Examples/ejemplo-02-intermedio.md) §9 | La mitad que **sí abre el almacén** sostiene sus cuatro propiedades: el trabajo de `E-1` se materializa con **3** piezas, **15** componentes y **2** observaciones y su texto se guarda **literal**; el listado devuelve **0** componentes y sin texto original, y el detalle los lleva; la consulta **sin alcance declarado** se rechaza en lugar de resolverse afuera; el retiro deja **0** dependientes; y el arrastre de la baja es todo o nada —**0** trabajos si se completa, **2** si se interrumpe, nunca **1**— | El comando del contrato: `dotnet run --project samples/infrastructure/02-intermedio` | Campo `evidencia` de `VER-02`, con su fecha | **Mayor, sin gradación** en el tramo del texto original y en el del arrastre: `RN-08` y el todo o nada de `ADR-02` no admiten tolerancia. **Menor** en el resto: cambia el texto de una línea sin cambiar su semántica. **Mayor** además si el `criterio_aceptacion` falla, si el listado empieza a traer componentes, o si alguno de `CU-03`, `CU-04` o `CU-05` deja de estar cubierto | `Sin verificar` | — |
| `SD-03` | `VER-03` de [`../10-Examples/ejemplo-03-avanzado.md`](../10-Examples/ejemplo-03-avanzado.md) §9 | Los **cinco** mecanismos que sólo esta capa provee se detienen en lugar de cumplir a medias: la derivación no guarda nada en claro y distingue el derivado **ilegible** del veredicto falso; **100** provisorias con **0** repetidas y ninguna derivada de un dato de la cuenta, y **0** valores producidos cuando la fuente de aleatoriedad no responde; el acceso lleva sus **4** reclamos y **0** accesos se emiten sin clave de firma; el sello del reloj llega por el puerto y dos corridas con el puerto fijado dan el mismo valor; y el arranque se **detiene** ante un linaje desconocido. Las **2** inspecciones de umbral cero dan **0** y **0** | El comando del contrato: `dotnet run --project samples/infrastructure/03-avanzado` | Campo `evidencia` de `VER-03`, con su fecha | **Mayor, sin gradación** en los cuatro tramos de modo de falla silencioso y en las **2** inspecciones de umbral cero: contraseña en claro guardada, provisoria repetida, provisoria derivada de un dato de la cuenta, valor producido sin aleatoriedad, y toda aparición de clave, contraseña o ruta del almacén. **Menor** en el resto. **Mayor** además si el `criterio_aceptacion` falla, si una corrida se hace **sin** las condiciones de medición declaradas en las precondiciones, o si alguno de `CU-06` a `CU-10` deja de estar cubierto | `Sin verificar` | — |

**Tres filas, una por contrato de verificación, sin contratos huérfanos ni filas sin contrato que las respalde**, que es lo que exige `Deriva-Rules.md` §6. La correspondencia es uno a uno: `SD-01`↔`VER-01`, `SD-02`↔`VER-02`, `SD-03`↔`VER-03`.

**El método de verificación de las tres es el comando declarado en su contrato, sin desvío.** No hay ninguna fila resuelta por inspección, porque `Deriva-Rules.md` §2.4 declara que una sonda `VER-XX` «trae su propio comando y su propia aserción».

## 4. Umbrales de deriva aplicados

Se toman de la fila «Contratos y comportamiento (`VER-XX`)» de `Deriva-Rules.md` §3, sin agregarle dimensiones.

| Dimensión | Deriva menor, se registra y no bloquea | Deriva mayor, bloquea y exige decisión | Filas |
| --- | --- | --- | --- |
| Contratos y comportamiento (`VER-XX`) | Cambia el texto de un mensaje de salida sin cambiar su semántica, o cambia el formato de un registro | El `criterio_aceptacion` falla; cambia el comando de ejecución sin actualizar el contrato; aparecen precondiciones no declaradas; o el caso de uso que la sonda ejercita deja de estar cubierto | `SD-01`, `SD-02`, `SD-03` |

**Varios tramos sin gradación.** Los declaran `SD-02` —texto original y todo o nada del arrastre— y `SD-03` —los cuatro modos de falla silenciosos y las dos inspecciones de umbral cero—, porque verifican reglas de negocio y prohibiciones de exposición que no admiten tolerancia. **`RA-03` sí se ejerce en esta capa**, y por eso está adentro de `SD-03`: es la capa que conoce el valor derivado de una credencial, la clave de firma y la ruta del almacén, y de que no los exponga depende que la regla siga siendo cierta aguas arriba. `RA-01` y `RA-02` no se ejercen acá: la primera se verifica en la superficie que expone y en la pieza pública, y la segunda en el visor.

**Toda deriva mayor se resuelve por una de dos vías y nunca por omisión** (`Deriva-Rules.md` §3): se corrige la biblioteca para volver a lo que el contrato dice, o se cambia la especificación con aprobación humana explícita, en cuyo caso la categoría 02 la modifica, el sample se rehace y esta matriz se actualiza. **Un caso de uso no se cambia desde acá.**

**Un `criterio_aceptacion` en `Falla` es un hallazgo del incremento en curso** y no se resuelve borrando la fila (`Rules-Examples.md` §4.4 y §4.5).

## 5. Qué no sensa esta matriz

Se declara para que no se lea como cobertura completa del proyecto de código:

| Elemento | Quién lo verifica |
| --- | --- |
| Las **17** condiciones del catálogo de `03`, recorridas en las dos direcciones | La batería de `tests/`, por los casos de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) |
| Las **siete** reglas conceptuales `RC-01` a `RC-07` del modelo | §5 de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) |
| Los **diez** casos de la batería obligatoria del producto | Los diez primeros de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md), que corren en el pipeline. **`SD-01` no los reemplaza**: sensa que el sample siga produciendo lo que esa batería exige |
| Cobertura de líneas y de ramas, y el piso propio del validador | El pipeline de `09-Devops`; los umbrales y su carácter están en [`Criterios-Validacion.md`](Criterios-Validacion.md) |
| Los **catorce** quality gates de [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3 | Esa sección, con su carácter por gate |

**Las tres sondas no reemplazan a la batería de pruebas**: la complementan desde afuera, ejercitando la superficie pública tal como la ve la composición de raíz de `GeometriaFactory-Api`. Es la asimetría que `Deriva-Rules.md` §4 declara en su cuarto momento.

## 6. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **Corrección del hallazgo P2-2 del informe `G-10-Examples-Siete-Proyectos-r1.md` 1.0.** La §1 citaba `Deriva-Rules.md` §2.3 **elidiendo sin marca** el fragmento «la abre AG-08 en la Fase E», que es justamente el que asigna la titularidad y del que este documento se desvía en el párrafo siguiente. Se restituye la cita **completa**, contrastada carácter por carácter contra la fuente, y se remite explícitamente al párrafo «Quién la abre», que ya declaraba y fundamentaba el desvío hacia AG-10 apoyándose en el segundo momento de sensado de §4. Se actualiza la trazabilidad upstream al [`../10-Examples/README.md`](../10-Examples/README.md) en su **1.1**. **Ninguna fila, umbral, método ni evidencia esperada cambia.** Contrastado contra el texto vivo del `PRODUCT-INTAKE` **1.25**, en particular §16.1 y §18, y no contra lo que otro documento dice de ellas. Sube minor: corrige la forma de una cita, no una afirmación de sensado. |
| 1.0 | 2026-08-11 | Emisión inicial, abierta por AG-10 al cerrar la fase que genera la categoría 10, que es el segundo momento de sensado de `Deriva-Rules.md` §4. Cierra el hueco que [`README.md`](README.md) §3 y [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §8 declararon como condicionado y temporal. Declara **tres** filas, `SD-01` a `SD-03`, una por cada contrato de verificación de `10-Examples`, con el comando del contrato como método, el campo `evidencia` del sample como evidencia esperada, el umbral de la fila «Contratos y comportamiento» de `Deriva-Rules.md` §3 —con **varios** tramos sin gradación en `SD-02` y en `SD-03`— y estado `Sin verificar` sin fecha. Declara además qué **no** sensa, para que la matriz no se lea como cobertura completa del proyecto de código. |
