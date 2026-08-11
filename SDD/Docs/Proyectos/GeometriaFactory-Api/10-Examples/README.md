# 10 · Ejemplos — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** README.md
**Versión:** 1.2
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Developer Advocate / Sample Engineer Senior (AG-10)
**Tipo de proyecto de código (D8):** `rest-api`
**Trazabilidad upstream:** [`../02-Especificacion-Funcional/`](../02-Especificacion-Funcional/), los **doce** casos de uso, y [`../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md`](../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md), los **quince** puntos de acceso y los **quince** códigos de contrato vivos; [`../02-Especificacion-Funcional/Casos-De-Uso/CU-12-Ejercitar-La-Superficie-Con-La-Coleccion-De-Peticiones-Reproducible.md`](../02-Especificacion-Funcional/Casos-De-Uso/CU-12-Ejercitar-La-Superficie-Con-La-Coleccion-De-Peticiones-Reproducible.md) 1.2; [`../05-Arquitectura-Tecnica/Contratos-REST.md`](../05-Arquitectura-Tecnica/Contratos-REST.md) y [`ADR-08`](../05-Arquitectura-Tecnica/Adrs/ADR-08-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md); [`../06-Backlog-Tecnico/historias-usuario/`](../06-Backlog-Tecnico/historias-usuario/), las **treinta** historias; [`../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md`](../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md), los **treinta y siete** casos; `PRODUCT-INTAKE` **1.25** §16.1, §18 **S-2**, §20 y §21
**Trazabilidad downstream:** [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), que toma las tres sondas `VER-XX`; `11-Documentacion` cuando se emita, que referencia estos samples sin duplicar su código

---

## Tabla de contenido

- [1. Qué hay en esta carpeta](#1-qué-hay-en-esta-carpeta)
- [2. Tabla maestra de samples](#2-tabla-maestra-de-samples)
- [3. Contratos de verificación](#3-contratos-de-verificación)
- [4. La divergencia viva sobre el alcance de la colección](#4-la-divergencia-viva-sobre-el-alcance-de-la-colección)
- [5. Convenciones de los samples](#5-convenciones-de-los-samples)
- [6. Estructura de `/samples` y su desvío declarado](#6-estructura-de-samples-y-su-desvío-declarado)
- [7. Cómo agregar un sample nuevo](#7-cómo-agregar-un-sample-nuevo)
- [8. Vínculo con 05 y con 11](#8-vínculo-con-05-y-con-11)
- [9. Control de cambios](#9-control-de-cambios)

---

## 1. Qué hay en esta carpeta

Tres markdown explicativos, uno por sample, con sus **diez** secciones obligatorias de `Rules-Examples.md` §4.2, y este índice. Cada markdown apunta a una carpeta ejecutable de [`/samples/api/`](../../../../../samples/api/) del repositorio, que esta pasada deja **esqueletada**: con su README local y su comando previsto, y sin corrida hecha. **Las tres carpetas existen** —[`01-basico/`](../../../../../samples/api/01-basico/), [`02-intermedio/`](../../../../../samples/api/02-intermedio/) y [`03-avanzado/`](../../../../../samples/api/03-avanzado/)—, cada una con su `README.md` local y el comando previsto de su contrato, y ninguna con código.

Esta emisión es la **pasada de diseño** de `Rules-Examples.md` §0.2. Los tres contratos de verificación están completos salvo el campo `evidencia`, que dice `No verificado — sin código` en los tres. **Ninguna carpeta de `/samples` promete una corrida que no se hizo.**

**Éste es el proyecto de código principal del producto** (`PRODUCT-MANIFEST` §2), y es el único cuya carpeta de muestras el `PRODUCT-INTAKE` §16.1 describe con contenido concreto desde su primera versión: «colección de peticiones HTTP reproducible con los escenarios **E-1 a E-8** como cuerpo». El sample 02 **es** esa colección, y es la muestra **S-2** de §18.

## 2. Tabla maestra de samples

| Sample | Nivel | Tiempo de setup | CU ilustrados | Ubicación |
| --- | --- | --- | --- | --- |
| [`ejemplo-01-basico.md`](ejemplo-01-basico.md) | Básico | 5-10 min | CU-01, CU-02, CU-09 | `/samples/api/01-basico/` |
| [`ejemplo-02-intermedio.md`](ejemplo-02-intermedio.md) | Intermedio | 5-10 min | CU-03, CU-04, CU-05, CU-06, CU-07, CU-08, CU-12 | `/samples/api/02-intermedio/` |
| [`ejemplo-03-avanzado.md`](ejemplo-03-avanzado.md) | Avanzado | 10-15 min | CU-10, CU-11 | `/samples/api/03-avanzado/` |

**Tres samples, el piso que `Rules-Examples.md` §2.2 fija para `rest-api`.** El tiempo de setup de los dos primeros es el mismo porque los dos necesitan lo mismo: el almacén reiniciado y el servicio levantado. El del tercero es mayor porque recorre el arranque **dos veces**, una sobre un almacén sano y otra sobre uno que no se puede entender.

**Cobertura de los doce casos de uso: 12 de 12.** Verificación uno por uno: `CU-01`, `CU-02` y `CU-09` en el 01; `CU-03`, `CU-04`, `CU-05`, `CU-06`, `CU-07`, `CU-08` y `CU-12` en el 02; `CU-10` y `CU-11` en el 03. Sin repeticiones y sin huecos: 3 + 7 + 2 = 12.

**Cobertura de los quince puntos de acceso, en dos recuentos que no hay que confundir.** El **sample 02** ejercita **13 de 15**, que es exactamente lo que `CU-12` §8 `CA-08` declara para la colección, y los dos que quedan fuera de su archivo son los que esa misma sección nombra: **`A-08`**, la baja física de una cuenta, porque ejercitarla dejaría a la colección sin el alumno con el que sigue el recorrido, y **`A-16`**, el punto de salud. **Entre los tres samples el recuento es 14 de 15**, porque el sample 03 sí ejercita `A-16`: es su acto de arranque. **El único que ningún sample ejercita es `A-08`**, y su verificación vive en las pruebas de integración, tal como `CU-12` §10 lo declara. 14 + 1 = 15. El identificador `A-04` está **retirado** de la superficie desde `PRODUCT-INTAKE` 1.13 y no se recicla: ni se ejercita ni se cuenta.

**Un residuo de la fuente aguas arriba, que esta categoría declaró y que ya quedó cerrado en su origen.** La §9 de `CU-12` **1.2** escribía «Puntos de acceso que ejercita | **13 de 16**» y «Los tres que no, en §10», mientras que su propia §8 `CA-08` y su §10 decían **13 de 15** y **dos** puntos no ejercitados —`A-08` y `A-16`— más una precisión sobre un tercero. **La cuenta viva es 13 de 15**, coherente con [`../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md`](../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md) §3, que declara **quince** puntos desde su versión 1.1. **`CU-12` 1.3 corrigió esa fila** al cerrar el hallazgo `B-API-02` de [`../../../Audit/B-02-03-GeometriaFactory-Api-r1.md`](../../../Audit/B-02-03-GeometriaFactory-Api-r1.md) 1.0; el recuento de esta categoría no cambia, porque **siempre escribió 15**.

**Cobertura de los ocho escenarios reales: 8 de 8.** El sample 01 usa `E-5` y `E-8`, y el sample 02 los ocho como cuerpo de petición, transcriptos sin modificación. **Ningún escenario se sustituye por datos sintéticos**, que es una regla de delivery del producto (`PRODUCT-INTAKE` §15) y no una preferencia de esta categoría.

## 3. Contratos de verificación

Vista de conjunto de la arista B, en el formato de `Rules-Examples.md` §4.4.

| Sonda | Sample | Verifica | Comando | Estado | Última corrida |
| --- | --- | --- | --- | --- | --- |
| `VER-01` | [`ejemplo-01-basico.md`](ejemplo-01-basico.md) | CU-01, CU-02, CU-09; US-01 a US-06, US-24, US-25 | `bash samples/api/01-basico/run.sh` | No verificado — sin código | — |
| `VER-02` | [`ejemplo-02-intermedio.md`](ejemplo-02-intermedio.md) | CU-03 a CU-08, CU-12; US-07 a US-23, US-30 | `bash samples/api/02-intermedio/run.sh` | No verificado — sin código | — |
| `VER-03` | [`ejemplo-03-avanzado.md`](ejemplo-03-avanzado.md) | CU-10, CU-11; US-26 a US-29 | `bash samples/api/03-avanzado/run.sh` | No verificado — sin código | — |

**Tres sondas, ninguna redundante**: los conjuntos de casos de uso y de historias que verifican son disjuntos, y entre las tres alcanzan a los doce casos de uso y a las treinta historias. Las tres entran a [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md) con estado `Sin verificar`, que es lo que `Deriva-Rules.md` §2.4 declara para un contrato en `No verificado — sin código`.

**`VER-02` es la sonda que la categoría 08 anticipó.** [`../08-Calidad-Y-Pruebas/README.md`](../08-Calidad-Y-Pruebas/README.md) §3 declaró, al omitir la matriz de sensado, que este proyecto de código «tiene un candidato natural, la **colección de peticiones reproducible** de `CU-12`». Éste es ese candidato, y la matriz se abre con él y con los otros dos.

**Qué relación tiene la colección con las pruebas, dicho con precisión.** `CU-12` §9 declara que **la colección no reemplaza a las pruebas de integración y no se cuenta como cobertura**, y [`../08-Calidad-Y-Pruebas/Estrategia-Testing.md`](../08-Calidad-Y-Pruebas/Estrategia-Testing.md) §3 la registra como «no es una prueba automatizada: es la forma de demostración que el intake declara». **Eso no cambia con esta emisión.** Lo que `VER-02` agrega es una **aserción evaluable** sobre esa demostración, para que su resultado sea un veredicto y no una impresión. Lo que comparten con las pruebas de integración son los datos, y nada más.

**Qué queda fuera de las tres sondas, y por qué no es un hueco.** Los **quince** códigos de contrato vivos recorridos uno por uno en las dos direcciones, la tabla de traducción, los umbrales de latencia y de caudal, la forma de la pirámide y la cobertura no los verifica ningún sample: los verifica la batería de `tests/GeometriaFactory.Integration.Tests` y el pipeline de `09-Devops`. Un sample que los duplicara sería el anti-patrón de `Rules-Examples.md` §4.5.

## 4. La divergencia viva sobre el alcance de la colección

El `PRODUCT-INTAKE` describe la colección **en dos lugares con alcances distintos**, y ninguno de los dos está envejecido:

| Lugar de la fuente | Qué dice | Alcance |
| --- | --- | --- |
| §16.1, fila de `GeometriaFactory-Api` | «Colección de peticiones HTTP reproducible con los escenarios **E-1 a E-8** como cuerpo» | **Ocho** |
| §18, muestra **S-2** | «con los cuerpos de **E-2 y E-5** y los códigos de respuesta esperados» | **Dos** |

**La divergencia es de alcance y no de antigüedad**, y la fuente no declara cuál manda. Está verificada contra el texto vivo del intake y **ya está elevada al Product Owner** por la categoría 02: `CU-12` §10 la declara y [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §11 la registra como punto abierto `PA-06`, con el Product Owner como responsable de declarar cuál rige y alinear §16.1 con §18 S-2.

**Esta categoría no la resuelve por su cuenta: hereda la lectura de la categoría 02.** `CU-12` §10 adopta **los ocho** con su fundamento —«`E-8` es el modo de falla que el propio intake llama el más probable de todos», porque lo produce la configuración regional de la máquina del alumno y no un error de programación— y [`ADR-08`](../05-Arquitectura-Tecnica/Adrs/ADR-08-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) declara explícitamente que la categoría 05 **hereda esa lectura y no la reabre**. La categoría 10 hace lo mismo: el sample 02 lleva los ocho.

**Qué pasaría si el Product Owner declarara que rige §18 S-2.** El sample 02 pasaría a llevar **dos** cuerpos, su contrato `VER-02` cambiaría sus aserciones de recuento, y la fila `SD-02` de la matriz de sensado se actualizaría con él. **Nada más cambiaría**: la estructura de `/samples/api/`, los tres samples y los otros dos contratos quedan igual. Se declara acá para que el costo de la decisión esté a la vista cuando se tome.

## 5. Convenciones de los samples

- **Los tres corren contra el servicio real**, levantado dentro del entorno de desarrollo contenido, sobre un almacén llevado a su estado de primer arranque. **No hay pantalla, no hay circuito y no hay visor**: es la contracara exacta del sample del `Visor`, que ejercita el visor **sin backend**.
- **Ejecutables en entorno limpio en cinco pasos o menos.** Es la obligación propia que el `PRODUCT-INTAKE` §18 le fija a las tres muestras del producto, y que `05` §8 mide como NFR.
- **Ningún dato de prueba inventado.** Los cuerpos salen de los escenarios del intake §20 y las identidades son valores evidentemente ficticios, declarados como tales. Es una regla de delivery del producto (`PRODUCT-INTAKE` §15), medida como NFR con umbral **0** y verificada por `TC-35`.
- **Ninguna clave de firma, ninguna contraseña real y ninguna dirección de servidor de producción**, que es `CA-07` de `CU-12` §8 y `RA-03`. La dirección del servicio llega del entorno.
- **`RA-01` no se toca.** Estos samples **no son el navegador**: corren servidor a servidor dentro del entorno contenido. La superficie no recibe peticiones del navegador y por eso no hay configuración de intercambio de origen cruzado, que es lo que `TC-36` verifica.
- **Nivel declarado** en la §2 de cada markdown, y progresión por nivel y no por dominio.
- **Criterio de aceptación evaluable por una máquina**: código de respuesta, cuerpo y líneas exactas de salida. Ninguno está redactado como prosa.
- **Los samples no acuñan códigos.** Los **quince** códigos de contrato vivos tienen fuente única en `GeometriaFactory-Contracts` y los **diez** códigos de respuesta en [`../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md`](../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md).

## 6. Estructura de `/samples` y su desvío declarado

| Tipo D8 | Estructura que `Rules-Examples.md` §2.3 fija | Estructura de este proyecto de código |
| --- | --- | --- |
| `rest-api` | `/samples/01-cliente-http-basico/`, `/samples/02-postman-collection/`, `/samples/03-sdk-tipado-generado/` | `/samples/api/01-basico/`, `/samples/api/02-intermedio/`, `/samples/api/03-avanzado/` |

Tres desvíos, los tres declarados acá y ninguno de nomenclatura por dominio:

1. **Un nivel de espacio de nombres por proyecto de código.** `Rules-Examples.md` §2.3 supone un proyecto de código por repositorio. Este producto tiene **siete** en un solo repositorio (`PRODUCT-INTAKE` §13 y §16), de modo que `/samples/01-basico/` colisionaría entre proyectos de código. Se agrega el segmento `api/`, que es carpeta extra y no renombre de las base. Es el mismo criterio que ya aplicaron los otros seis proyectos de código.
2. **El slug del segundo no nombra una herramienta.** El valor que §3.1 admite para ese lugar es el nombre de un producto comercial concreto, y el prompt de la propia categoría prohíbe introducirlos (`Rules-Examples.md` §8, restricciones). La colección se declara **por su papel** —colección de peticiones reproducible—, que es como la nombran el intake §16.1, `CU-12` y `ADR-08`, y el slug queda en `intermedio`, admitido por §3.1.
3. **El slug del tercero no es `-sdk-tipado-generado`, y ese sample no existiría.** El cliente tipado del producto **ya existe y no se genera**: es `GeometriaFactory-Contracts`, un proyecto de código propio que `GeometriaFactory-Web` consume compilado (`PRODUCT-INTAKE` §16, árbol de `src/`). Además **ningún proyecto de código del producto es `redistribuible`** (`PRODUCT-MANIFEST` §2), de modo que no hay paquete que publicar ni integrador externo que necesite un cliente generado. Un sample de SDK generado acá afirmaría una capacidad que el producto no tiene. En su lugar, el tercero recorre **la composición de raíz y el arranque**, que es lo que este proyecto de código tiene de propio y ningún otro tiene.

## 7. Cómo agregar un sample nuevo

1. Elegir el número correlativo siguiente y un slug de `Rules-Examples.md` §3.1, por nivel o por capacidad, **nunca por dominio y nunca por producto comercial**.
2. Copiar la cabecera de §4.1 y las **diez** secciones de §4.2 de esas reglas.
3. Declarar el contrato de verificación en la §9, con un `VER-XX` no usado en este proyecto de código, y criterio de aceptación evaluable: código de respuesta y cuerpo, o snapshot comparable.
4. Agregar la fila a las tablas de §2 y §3 de este README, y **recontar los puntos de acceso ejercitados** contra los quince de `Definicion-Superficie-HTTP.md` §3.
5. Dar de alta la sonda en [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), en `Sin verificar`, según `Deriva-Rules.md` §4.

## 8. Vínculo con 05 y con 11

Los tres samples ejercitan la superficie que declara [`../05-Arquitectura-Tecnica/Contratos-REST.md`](../05-Arquitectura-Tecnica/Contratos-REST.md) y no invocan nada de adentro del proceso. **`CU-12` no tiene componente de tiempo de ejecución y es correcto que no lo tenga** ([`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §3.3): la colección no implementa, demuestra. **La forma del archivo de la colección es de la categoría 05**, y su contenido vive acá; [`ADR-08`](../05-Arquitectura-Tecnica/Adrs/ADR-08-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) agrega la convención de que la colección se actualiza **en la misma intervención** en que cambia la superficie.

**`11-Documentacion` todavía no está emitida** para este proyecto de código. Cuando lo esté, referencia estos samples y los contextualiza **sin duplicar su código**, que es la división que `Rules-Examples.md` §0 fija: 10 demuestra con código ejecutable y verificable, 11 explica, referencia y enlaza.

## 9. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.2 | 2026-08-11 | **Absorbe el cierre del hallazgo `B-API-02` (P1)** del informe [`../../../Audit/B-02-03-GeometriaFactory-Api-r1.md`](../../../Audit/B-02-03-GeometriaFactory-Api-r1.md) 1.0 sobre `../02-Especificacion-Funcional/Casos-De-Uso/CU-12-…` §9. **§2**: la nota del residuo declarado deja de decir que la fila de §9 sigue mal y que corregirla no es de acá, y pasa a registrar que **`CU-12` 1.3 la corrigió** a «13 de 15, los dos que no: `A-08` y `A-16`». Se llega acá por la búsqueda de propagación que el informe exige para todo recuento corregido: éste era **el único consumidor vivo** del recuento erróneo en todo el corpus. **Ningún sample, ninguna sonda, ningún recuento y ningún contrato de verificación cambia**: esta categoría ya escribía **15**. Sube minor. |
| 1.1 | 2026-08-11 | **Correcciones del informe `G-10-Examples-Siete-Proyectos-r1.md` 1.0, contrastadas contra el texto vivo del `PRODUCT-INTAKE` 1.25.** **P0-1**: las tres carpetas de [`/samples/api/`](../../../../../samples/api/) se crean de verdad, cada una con su README local y su comando previsto, y §1 lo declara con el enlace; el comando de las tres filas `VER-XX` de la matriz de sensado queda coherente con lo que existe. **P1-3**: se verifica contra §18 vigente que la **divergencia de alcance** que §4 declara —ocho cuerpos en §16.1 contra dos en §18 **S-2**— **sigue viva y sin resolver**, y que la precisión que la 1.25 agregó a §18 no la toca: precisa que las tres muestras `S-X` no son el conjunto de las carpetas, no cuántos cuerpos lleva `S-2`. §4 se conserva entero, con `PA-06` abierto y el Product Owner como responsable. Se actualiza la trazabilidad upstream a la versión **1.25** del intake. Ningún recuento, contrato, sample ni cobertura cambia. |
| 1.0 | 2026-08-11 | Emisión inicial de la categoría del proyecto de código **principal** del producto, en la **pasada de diseño** de `Rules-Examples.md` §0.2. Declara **tres** samples —el piso de §2.2 para `rest-api`—, con el segundo materializando la muestra **S-2** del `PRODUCT-INTAKE` §18, la tabla de contratos de verificación con las **tres** sondas `VER-01` a `VER-03` en `No verificado — sin código`, las convenciones y la estructura de `/samples/api/` con sus **tres** desvíos declarados respecto de §2.3. Declara la **divergencia viva** entre §16.1 y §18 S-2 sobre el alcance de la colección —**ocho** cuerpos contra **dos**—, hereda la lectura de la categoría 02 sin reabrirla, y deja escrito qué cambiaría si el Product Owner declarara el otro alcance. Verifica **12 de 12** casos de uso, **14 de 15** puntos de acceso entre los tres samples —**13 de 15** en la colección del sample 02— con el restante declarado, y **8 de 8** escenarios del intake §20; y declara el **residuo** de `CU-12` §9, que escribe «13 de 16» contra el recuento vivo de quince puntos, sin corregirlo desde acá. |
