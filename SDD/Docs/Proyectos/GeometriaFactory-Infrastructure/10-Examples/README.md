# 10 · Ejemplos — GeometriaFactory-Infrastructure

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** README.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Developer Advocate / Sample Engineer Senior (AG-10)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../02-Especificacion-Funcional/`](../02-Especificacion-Funcional/), los **diez** casos de uso, y [`../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md`](../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md); [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §3.1, los **ocho** componentes; [`../05-Arquitectura-Tecnica/Flujo-Ejecucion.md`](../05-Arquitectura-Tecnica/Flujo-Ejecucion.md) §5, la tabla de derivación por tipo; [`../06-Backlog-Tecnico/historias-usuario/`](../06-Backlog-Tecnico/historias-usuario/), las **veinticinco** historias; [`../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md`](../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md), los **treinta y cinco** casos, cuyos **diez** primeros son la batería; `PRODUCT-INTAKE` **1.25** §16.1, §18 **S-3**, §20 y §21
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

Tres markdown explicativos, uno por sample, con sus **diez** secciones obligatorias de `Rules-Examples.md` §4.2, y este índice. Cada markdown apunta a una carpeta ejecutable de [`/samples/infrastructure/`](../../../../../samples/infrastructure/) del repositorio, que esta pasada deja **esqueletada**: con su README local y su comando previsto, y sin corrida hecha. **Las tres carpetas existen** —[`01-basico/`](../../../../../samples/infrastructure/01-basico/), [`02-intermedio/`](../../../../../samples/infrastructure/02-intermedio/) y [`03-avanzado/`](../../../../../samples/infrastructure/03-avanzado/)—, cada una con su `README.md` local y el comando previsto de su contrato, y ninguna con código.

Esta emisión es la **pasada de diseño** de `Rules-Examples.md` §0.2. Los tres contratos de verificación están completos salvo el campo `evidencia`, que dice `No verificado — sin código` en los tres. **Ninguna carpeta de `/samples` promete una corrida que no se hizo.**

**Los tres samples sirven a las dos aristas de `Rules-Examples.md` §0.1.** La arista A —referencia de integración— le habla al consumidor de estos adaptadores, que dentro de este producto es la composición de raíz de `GeometriaFactory-Api`. La arista B —arnés de autovalidación— le habla al equipo que construye y a los agentes que codifican contra la especificación, y es la que aporta las sondas `VER-XX` de la matriz de sensado.

## 2. Tabla maestra de samples

| Sample | Nivel | Tiempo de setup | CU ilustrados | Ubicación |
| --- | --- | --- | --- | --- |
| [`ejemplo-01-basico.md`](ejemplo-01-basico.md) | Básico | < 5 min | CU-06001, CU-06002 | `/samples/infrastructure/01-basico/` |
| [`ejemplo-02-intermedio.md`](ejemplo-02-intermedio.md) | Intermedio | 10-15 min | CU-06003, CU-06004, CU-06005 | `/samples/infrastructure/02-intermedio/` |
| [`ejemplo-03-avanzado.md`](ejemplo-03-avanzado.md) | Avanzado | 10-15 min | CU-06006, CU-06007, CU-06008, CU-06009, CU-06010 | `/samples/infrastructure/03-avanzado/` |

**Tres samples, el piso que `Rules-Examples.md` §2.2 fija para `library`.** El tiempo de setup crece a partir del segundo porque los samples 02 y 03 **sí abren el almacén**, y el primero no.

**La partición entre los tres es la de `05` §2 punto 2, y no una elección de esta categoría.** «La mitad de esta capa no toca el almacén»: el sample 01 es exactamente esa mitad —interpretación y verificación de valores—, el 02 es la que sí lo abre, y el 03 recorre los mecanismos que no guardan nada pero sí producen algo que no se puede repetir.

**Cobertura de los diez casos de uso: 10 de 10.** Verificación uno por uno: `CU-06001` y `CU-06002` en el 01; `CU-06003`, `CU-06004` y `CU-06005` en el 02; `CU-06006`, `CU-06007`, `CU-06008`, `CU-06009` y `CU-06010` en el 03. Sin repeticiones y sin huecos: 2 + 3 + 5 = 10.

**Cobertura de los ocho escenarios reales: 8 de 8.** El sample 01 los recorre todos como **texto literal**, que es lo que este proyecto de código recibe y lo que ningún otro recibe igual: acá el escenario entra por su forma, no por su interpretación ya hecha. **Ningún escenario se sustituye por datos sintéticos.**

**Cobertura de las cuatro tolerancias `T1` a `T4`: 4 de 4**, todas en el sample 01. `T1` —la clave `Tapas` leída como sinónimo de `Bases`— con `E-2` y `E-7`; `T2` —las comas finales— con `E-2`; `T3` —caras `Cuadrado` y caras `Rectangulo` leídas igual— con `E-3` y `E-4`; `T4` —el valor declarado incorrecto que advierte y no rechaza— con `E-1`, `E-2` y `E-3`. Es el reparto que el `PRODUCT-INTAKE` §21 declara en su última fila.

## 3. Contratos de verificación

Vista de conjunto de la arista B, en el formato de `Rules-Examples.md` §4.4.

| Sonda | Sample | Verifica | Comando | Estado | Última corrida |
| --- | --- | --- | --- | --- | --- |
| `VER-06001` | [`ejemplo-01-basico.md`](ejemplo-01-basico.md) | CU-06001, CU-06002; US-06001 a US-06007 | `dotnet run --project samples/infrastructure/01-basico` | No verificado — sin código | — |
| `VER-06002` | [`ejemplo-02-intermedio.md`](ejemplo-02-intermedio.md) | CU-06003, CU-06004, CU-06005; US-06008 a US-06016 | `dotnet run --project samples/infrastructure/02-intermedio` | No verificado — sin código | — |
| `VER-06003` | [`ejemplo-03-avanzado.md`](ejemplo-03-avanzado.md) | CU-06006, CU-06007, CU-06008, CU-06009, CU-06010; US-06017 a US-06025 | `dotnet run --project samples/infrastructure/03-avanzado` | No verificado — sin código | — |

**Tres sondas, ninguna redundante**: los conjuntos de casos de uso y de historias que verifican son disjuntos, y entre las tres alcanzan a los diez casos de uso y a las veinticinco historias. Las tres entran a [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md) con estado `Sin verificar`, que es lo que `Deriva-Rules.md` §2.4 declara para un contrato en `No verificado — sin código`.

**Qué queda fuera de las tres sondas, y por qué no es un hueco.** Las **17** condiciones del catálogo de `03` recorridas en las dos direcciones, las **siete** reglas conceptuales del modelo, la cobertura de líneas y de ramas, y el piso propio del validador no los verifica ningún sample: los verifica la batería de `tests/` y el pipeline de `09-Devops`. Un sample que los duplicara sería el anti-patrón de `Rules-Examples.md` §4.5, «samples que duplican el `/src` sin agregar valor demostrativo».

**Y hay una relación que conviene decir con precisión, porque es la más fácil de confundir.** Los **diez** casos de la batería obligatoria del producto —los diez primeros de [`../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md`](../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md)— **no los reemplaza el sample 01**, aunque los dos usen los mismos ocho escenarios. La batería vive en `tests/` y corre en el pipeline; el sample corre a mano y muestra el resultado por consola, para que quien integra vea lo que el validador produce sin escribir una prueba. `VER-06001` sensa que **el sample siga produciendo lo que la batería exige**, no que la batería exista.

## 4. Convenciones de los samples

- **Ejecutables en entorno limpio en cinco pasos o menos**, dentro del entorno de desarrollo contenido del repositorio, que es donde ocurre todo el ciclo porque el host no tiene la plataforma.
- **El sample 01 no abre el almacén**, y eso es una propiedad verificada y no una omisión: es la partición de `05` §2 punto 2, la que hace que la batería obligatoria del producto sea barata de correr.
- **Ningún sample lleva una credencial real, una clave de firma ni una dirección concreta.** Es la prohibición que el catálogo de condiciones de `03` §1.4 declara para esta capa, que es **la que las conoce**, y de la que depende que la regla de exposición del contrato del producto siga siendo cierta.
- **Nivel declarado** en la §2 de cada markdown, y progresión por nivel y no por dominio.
- **Trazabilidad obligatoria** en la §8 de cada markdown, con al menos una fila por caso de uso, regla, regla conceptual o ADR.
- **Criterio de aceptación evaluable por una máquina**: exit code más líneas exactas de salida. Ninguno está redactado como prosa.
- **Los datos son reales**, transcriptos del `PRODUCT-INTAKE` §20 sin modificación, en archivos `.txt` y no `.json`, porque el texto de `E-2` **no es JSON estrictamente válido** y una herramienta que lo reformateara rompería justamente la tolerancia que ese escenario ejercita.
- **Los samples no acuñan vocabulario ni condiciones.** Todo término está declarado en [`../02-Especificacion-Funcional/Glosario-Funcional.md`](../02-Especificacion-Funcional/Glosario-Funcional.md), y las **17** condiciones tienen fuente única en [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md).

## 5. Estructura de `/samples` y su desvío declarado

| Tipo D8 | Estructura que `Rules-Examples.md` §2.3 fija | Estructura de este proyecto de código |
| --- | --- | --- |
| `library` | `/samples/01-basico-consola/`, `/samples/02-intermedio-con-extensiones/`, `/samples/03-avanzado-integracion-real/` | `/samples/infrastructure/01-basico/`, `/samples/infrastructure/02-intermedio/`, `/samples/infrastructure/03-avanzado/` |

Dos desvíos, los dos declarados acá y ninguno de nomenclatura por dominio:

1. **Un nivel de espacio de nombres por proyecto de código.** `Rules-Examples.md` §2.3 supone un proyecto de código por repositorio. Este producto tiene **siete** en un solo repositorio (`PRODUCT-INTAKE` §13 y §16), de modo que `/samples/01-basico/` colisionaría entre proyectos de código. Se agrega el segmento `infrastructure/`, que es carpeta extra y no renombre de las base, que es lo que §2.3 admite. Es el mismo criterio que ya aplicaron los otros proyectos de código al emitir su categoría 10.
2. **Los slugs son de nivel y no de capacidad.** Se usan `basico`, `intermedio` y `avanzado`, los tres admitidos por `Rules-Examples.md` §3.1. No se usa `-con-extensiones` porque el flag `tiene_extensibilidad` de este proyecto de código es **false** (`PRODUCT-MANIFEST` §5).

## 6. Por qué este proyecto de código emite samples, contra lo que §16.1 decía

El `PRODUCT-INTAKE` **1.23** §16.1 dejó la fila de `Application` e `Infrastructure` con la redacción anterior —«sin samples propios: no son consumidas por integradores externos, sólo por Api. Su verificación vive en `tests/`»— y le agregó una anotación viva: «**queda por revisar si les alcanza el mismo argumento que a Domain y Contracts** cuando su Fase G se emita». Esta sección hace esa revisión y **declara su resultado: el argumento alcanza, y acá alcanza con un motivo más que ninguno de los otros tres tiene.**

Primero, los dos términos del argumento que valió para `Domain` y `Contracts`:

| Término del argumento | ¿Se cumple en `GeometriaFactory-Infrastructure`? | Comprobación |
| --- | --- | --- |
| **Hay una segunda audiencia declarada por la guía de la categoría** | **Sí** | `Rules-Examples.md` §0.1 declara la arista B con su destinatario: «al equipo que construye, y a los agentes de IA que codifican contra la especificación». El motivo que §16.1 da —la ausencia de integradores externos— alcanza a la **arista A** y no a la B |
| **Sin categoría 10 el proyecto de código queda sin ninguna sonda de deriva** | **Sí** | `requiere_maqueta` es **false** (`PRODUCT-MANIFEST` §5): no hay Fase B2 ni línea de base visual. Y [`../08-Calidad-Y-Pruebas/README.md`](../08-Calidad-Y-Pruebas/README.md) §3 omitió la matriz de sensado por las **dos** condiciones juntas, con la frase de cierre «cuando se emita la categoría 10, la matriz se abre con sus filas `VER-XX`» |

**Y hubo un tercer motivo, propio de este proyecto de código: el intake ya le había asignado un sample en otra sección.** El `PRODUCT-INTAKE` §18 declara la muestra **S-3** —«**Juego de datos de los ocho escenarios** de la Parte D, en archivos sueltos, listos para pegar en el formulario de carga o para usar como cuerpo en S-2»— y en su columna de proyecto de código que ilustra dice **`GeometriaFactory-Infrastructure` (validador)**. Ésa **era** una divergencia interna del intake —§16.1 decía que este proyecto de código no produce samples y §18 le asignaba uno con nombre y contenido—, y esta categoría la declaró en lugar de resolverla por cuenta propia, adoptando la lectura de §18 y elevando la alineación al Product Owner.

**La alineación se hizo, y es la única de las cuatro que el intake declaró explícitamente al hacerla.** El **1.24** reescribió la fila de §16.1 nombrando este motivo —«**`Infrastructure` tiene además un motivo propio**: §18 le asigna la muestra **`S-3`**, de modo que la redacción anterior de esta fila contradecía a §18 dentro del mismo documento»—, y la **1.25** cerró el otro extremo de la contradicción en §18: «**Las tres muestras `S-1`, `S-2` y `S-3` no son el conjunto de las carpetas de `/samples`** [PRECISADO 2026-08-11]». **Las dos secciones dicen hoy lo mismo**, y la carpeta [`/samples/infrastructure/`](../../../../../samples/infrastructure/) existe con las tres suyas.

**Lo que la emisión no hace.** No reemplaza la verificación que vive en `tests/`, y §16.1 tiene razón en que ahí vive: las tres sondas **complementan** la batería desde afuera, y §3 de este README declara qué queda fuera de ellas y por qué el sample 01 no sustituye a los diez casos de la batería obligatoria.

**Los dos puntos que quedaban abiertos están cerrados, y se conservan con su desenlace.** Este README declaraba abiertas la consolidación de §16.1 y su alineación con §18 **S-3**, y las elevaba al Product Owner. **Las dos se hicieron**, en el intake **1.24** y **1.25** respectivamente, y el párrafo anterior las cita abriendo la fuente. De modo que **la fuente vinculante de la estructura de `/samples/infrastructure/` es §16.1 del `PRODUCT-INTAKE`**, y no §5 de este README. **No queda ningún punto abierto sobre §16.1 ni sobre §18 por parte de este proyecto de código.**

## 7. Cómo agregar un sample nuevo

1. Elegir el número correlativo siguiente y un slug de `Rules-Examples.md` §3.1, por nivel o por capacidad, **nunca por dominio**.
2. Copiar la cabecera de §4.1 y las **diez** secciones de §4.2 de esas reglas.
3. Declarar el contrato de verificación en la §9, con un `VER-XX` no usado en este proyecto de código, y criterio de aceptación evaluable.
4. Agregar la fila a las tablas de §2 y §3 de este README.
5. Dar de alta la sonda en [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), en `Sin verificar`, según `Deriva-Rules.md` §4.

## 8. Vínculo con 05 y con 11

Los tres samples consumen la superficie pública que declara [`../05-Arquitectura-Tecnica/Contratos-Abstractions.md`](../05-Arquitectura-Tecnica/Contratos-Abstractions.md) y no invocan componentes internos: los **ocho** componentes de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §3.1 son internos y ninguno se expone por separado. **La conexión de cada puerto con su adaptador no la hacen los samples**: es de la composición de raíz de `GeometriaFactory-Api` ([`ADR-06001`](../05-Arquitectura-Tecnica/Adrs/ADR-06001-Adaptadores-Por-Puerto-Sin-Repositorio-Generico.md)), y los samples instancian el adaptador que ejercitan y nada más.

**`11-Documentacion` todavía no está emitida** para este proyecto de código. Cuando lo esté, referencia estos samples y los contextualiza **sin duplicar su código**, que es la división que `Rules-Examples.md` §0 fija: 10 demuestra con código ejecutable y verificable, 11 explica, referencia y enlaza.

## 9. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **Correcciones del informe `G-10-Examples-Siete-Proyectos-r1.md` 1.0, contrastadas contra el texto vivo del `PRODUCT-INTAKE` 1.25.** **P0-1**: las tres carpetas de [`/samples/infrastructure/`](../../../../../samples/infrastructure/) se crean de verdad, cada una con su README local y su comando previsto, y §1 lo declara con el enlace; el comando de las tres filas `VER-XX` de la matriz de sensado queda coherente con lo que existe. **P1-1 y P1-3**: se cierran los **dos** puntos abiertos de §6 —la consolidación de §16.1 y su alineación con §18 **S-3**—, que el informe marcó como «medio verdadero» porque el primero ya estaba cerrado por el intake 1.24 y el segundo lo cerró la 1.25. Los dos se conservan con su desenlace y con las dos secciones del intake citadas desde la fuente y no a través de otro documento. Se actualiza la trazabilidad upstream a la versión **1.25** del intake. Ningún recuento, contrato, sample ni cobertura cambia. |
| 1.0 | 2026-08-11 | Emisión inicial de la categoría, en la **pasada de diseño** de `Rules-Examples.md` §0.2. Declara **tres** samples —el piso de §2.2 para `library`, partidos según el criterio de `05` §2 punto 2— con su tabla maestra, la tabla de contratos de verificación con las **tres** sondas `VER-06001` a `VER-06003` en `No verificado — sin código`, las convenciones, la estructura de `/samples/infrastructure/` con sus **dos** desvíos declarados respecto de §2.3, y **la revisión que el `PRODUCT-INTAKE` 1.23 §16.1 dejó pendiente**: se comprueban los dos términos del argumento que valió para `Domain` y `Contracts`, se declara un **tercer** motivo propio —que §18 **S-3** ya asignaba una muestra a este proyecto de código, en divergencia con §16.1— y se eleva la alineación al Product Owner. Verifica **10 de 10** casos de uso, **8 de 8** escenarios del intake §20 usados como texto literal y **4 de 4** tolerancias `T1` a `T4`. |
