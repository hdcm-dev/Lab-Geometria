# Vista de producto — Fábrica de Geometría

**Producto:** Fábrica de Geometría
**Documento:** Vista-Producto.md
**Versión:** 1.3
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Nivel:** Producto
**Trazabilidad upstream:** [`PRODUCT-MANIFEST-Fabrica-De-Geometria.md`](../../Intake/PRODUCT-MANIFEST-Fabrica-De-Geometria.md) **1.3** §1.2, §2, §3, §4 y §5; [`PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.26** §13 (composición del producto) y §14 (las tres reglas de arquitectura de nivel producto); las **siete** categorías `05-Arquitectura-Tecnica` emitidas bajo `Proyectos/`, con sus **45** ADR y sus **seis** contratos de superficie
**Trazabilidad downstream:** `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas`, `09-Devops` y `11-Documentacion` de los siete proyectos de código; [`Pipeline-Producto.md`](Pipeline-Producto.md) y [`../README.md`](../README.md), emitidos en la misma consolidación

---

## Tabla de contenido

- [1. Objetivo y alcance](#1-objetivo-y-alcance)
  - [1.1 Estado real de cierre](#11-estado-real-de-cierre)
- [2. Mapa de proyectos de código](#2-mapa-de-proyectos-de-código)
- [3. Grafo de dependencias](#3-grafo-de-dependencias)
  - [3.1 La discrepancia del grafo, y por qué sigue abierta](#31-la-discrepancia-del-grafo-y-por-qué-sigue-abierta)
- [4. Contratos inter-proyecto](#4-contratos-inter-proyecto)
- [5. Decisiones de nivel producto](#5-decisiones-de-nivel-producto)
- [6. Cross-cutting compartido](#6-cross-cutting-compartido)
- [7. Riesgos de integración inter-proyecto](#7-riesgos-de-integración-inter-proyecto)
- [8. Trazabilidad](#8-trazabilidad)
- [9. Control de cambios](#9-control-de-cambios)

---

## 1. Objetivo y alcance

**Este documento se sitúa por encima de la arquitectura de cada proyecto de código y no la duplica: referencia, no reescribe.** Lo que documenta es lo que ninguna de las siete categorías `05` puede documentar sola —el mapa, el grafo, los contratos que cruzan fronteras, lo transversal y los riesgos de integración—, y lo hace apuntando al documento que lo decide en cada caso.

**El detalle interno de cada proyecto de código vive en su propia `Arquitectura-Proyecto-Codigo.md`**, bajo `Proyectos/<Nombre-Proyecto-Codigo>/05-Arquitectura-Tecnica/`. Ninguna decisión se toma acá y ninguna se reabre: las **45** ADR emitidas son la fuente de toda decisión técnica del producto.

**Se emite una sola vez, al cierre del bucle de proyectos de código.** El bucle está cerrado: los siete están emitidos, en tres olas y en orden topológico.

**Para quién.** Para quien tiene que entender el producto entero antes de tocar una parte: el desarrollador que entra por un proyecto de código y necesita saber qué le impone el resto, y quien decide el orden de construcción y de despliegue.

### 1.1 Estado real de cierre

La versión 1.0 se emitió cuando sólo la Fase C estaba cerrada. Hoy el bucle completo de especificación por proyecto de código está emitido y auditado, y esta versión lo consolida. **Nada de lo que sigue redecide nada**: cada fila apunta al informe de auditoría que la cierra, bajo [`../Audit/`](../Audit/).

| Fase | Categorías | Ámbito | Informe que la cierra | Dictamen |
| --- | --- | --- | --- | --- |
| A | `00-Contexto`, `01-Necesidades-Negocio` | Producto | [`A-00-01-r3.md`](../Audit/A-00-01-r3.md) | Aprobado con observaciones |
| B | `02-Especificacion-Funcional`, `03-UX-UI-DX` | Por proyecto de código | `B-02-03-<Nombre-Proyecto-Codigo>-r<N>.md`, un informe por proyecto de código | Aprobado o aprobado con observaciones |
| B2 | Validación visual de maqueta | `GeometriaFactory-Web`, con la fachada del `Visor` integrada | [`B2-Maqueta-GeometriaFactory-Web-r2.md`](../Audit/B2-Maqueta-GeometriaFactory-Web-r2.md), que levanta el rechazo de [`B2-Maqueta-GeometriaFactory-Web-r1.md`](../Audit/B2-Maqueta-GeometriaFactory-Web-r1.md) | Rechazado en ronda 1; **aprobado en ronda 2** |
| C | `05-Arquitectura-Tecnica` | Los siete | [`C-05-Arquitectura-Siete-Proyectos-r2.md`](../Audit/C-05-Arquitectura-Siete-Proyectos-r2.md) | Aprobado |
| D | `06-Backlog-Tecnico`, `07-Plan-Sprint` | Los siete | [`D-06-07-Backlog-Siete-Proyectos-r1.md`](../Audit/D-06-07-Backlog-Siete-Proyectos-r1.md) | Aprobado |
| E | `08-Calidad-Y-Pruebas` | Los siete | [`E-08-Calidad-Siete-Proyectos-r2.md`](../Audit/E-08-Calidad-Siete-Proyectos-r2.md) | Aprobado |
| F | `09-Devops` | Los siete | [`F-09-Devops-Siete-Proyectos-r1.md`](../Audit/F-09-Devops-Siete-Proyectos-r1.md) | Aprobado |
| G | `10-Examples`, pasada de diseño | Los siete | [`G-10-Examples-Siete-Proyectos-r2.md`](../Audit/G-10-Examples-Siete-Proyectos-r2.md) | Aprobado |

**Ocho categorías por proyecto de código, no siete.** Cada uno de los siete tiene emitidas `02`, `03`, `05`, `06`, `07`, `08`, `09` y `10`. La novena, `04-Prompts-AI`, **está omitida por gating** en los siete: `usa_llm` es false en todos según `PRODUCT-MANIFEST` §5, y su ausencia no es hueco. La décima y última, `11-Documentacion`, entra en esta misma consolidación como plan documental —Momento 1— y todavía no tiene contenido redactado.

**El hueco de auditoría que esta vista registraba quedó cerrado.** Bajo `../Audit/` había un informe `B-02-03-` por cada proyecto de código **salvo `GeometriaFactory-Api`** —seis informes para siete—, y la vista lo declaraba abierto. El informe faltante se emitió: [`B-02-03-GeometriaFactory-Api-r1.md`](../Audit/B-02-03-GeometriaFactory-Api-r1.md) 1.0, del 2026-08-11, **tardío y declarándolo en su §0**, con dictamen **RECHAZADO** por quince hallazgos de recuento y de cita, ninguno sobre una decisión de contrato. Son siete informes para siete proyectos de código. **Esta vista registra el dictamen y no lo redecide**: el rechazo se levanta con la ronda 2 de aquel proyecto de código, y lo que sí queda cerrado acá es el hueco de proceso.

**Las magnitudes del producto, contadas sobre el instrumento y no heredadas de otro documento.** Cada fila se verificó el día de esta emisión contra el documento que la produce.

| Magnitud | Cantidad | Dónde se cuenta |
| --- | --- | --- |
| Proyectos de código | 7 | `PRODUCT-MANIFEST` §2 |
| Aristas de compilación | 7 u 8 según qué sección del manifiesto se lea | §3.1 de este documento |
| Casos de uso | 71 — `Domain` 13, `Api` 12, `Application` 11, `Web` 10, `Infrastructure` 10, `Contracts` 8, `Visor` 7 | `Proyectos/<Nombre>/02-Especificacion-Funcional/Casos-De-Uso/` |
| Reglas de negocio | 16 | `Proyectos/GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/`, `RN-01` a `RN-16` |
| Invariantes del dominio | 9 | `PRODUCT-INTAKE` §14, `INV-01` a `INV-09` |
| ADR | 45 | `Proyectos/<Nombre>/05-Arquitectura-Tecnica/Adrs/` |
| Contratos de superficie | 6 | §4 de este documento |
| Puntos de acceso de la superficie HTTP | 15 | [`Api/Contratos-REST.md`](../Proyectos/GeometriaFactory-Api/05-Arquitectura-Tecnica/Contratos-REST.md) §3 |
| Códigos del contrato | 15 vivos sobre 18 emitidos, 3 retirados | [`Contracts/Contratos-Abstractions.md`](../Proyectos/GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Contratos-Abstractions.md) §5.1 |
| Funciones de la fachada del visor | 6 | [`Visor/Contratos-Abstractions.md`](../Proyectos/GeometriaFactory-Visor/05-Arquitectura-Tecnica/Contratos-Abstractions.md) §3 |
| Escenarios de datos | 8, `E-1` a `E-8` | `PRODUCT-INTAKE` §20 |
| Casos de la batería del validador | 10 | [`Infrastructure/08-Calidad-Y-Pruebas/Criterios-Validacion.md`](../Proyectos/GeometriaFactory-Infrastructure/08-Calidad-Y-Pruebas/Criterios-Validacion.md) `CV-02` |
| Quality gates | 77 — `Api` 15, `Infrastructure` 14, `Application` 11, `Web` 11, `Contracts` 9, `Visor` 9, `Domain` 8 | `Proyectos/<Nombre>/09-Devops/` y `08-Calidad-Y-Pruebas/` |
| Sondas de verificación `VER-XX` | 19 | `Proyectos/<Nombre>/10-Examples/ejemplo-XX-*.md`, sección de contrato de verificación |

**Ninguna de las 19 sondas tiene evidencia todavía**, y es lo esperado: la Fase G es la pasada de diseño y su campo `evidencia` queda en `No verificado — sin código` hasta la Fase I. Un producto que declarara evidencia acá estaría afirmando lo que no ocurrió.

## 2. Mapa de proyectos de código

Refleja `PRODUCT-MANIFEST` **1.3** §2 y §5. **Ningún proyecto de código es `redistribuible`**, de modo que el prefijo de paquetes redistribuibles del perfil de nombres (§1.2 del manifiesto) queda sin uso.

| `Nombre-Proyecto-Codigo` | `Identidad-Codigo` | Tipo D8 | Rol en el producto | `redistribuible` | Arquitectura |
| --- | --- | --- | --- | --- | --- |
| `GeometriaFactory-Domain` | `GeometriaFactory.Domain` | `library` | Entidades e invariantes; centro de la regla de dependencias | false | [`05`](../Proyectos/GeometriaFactory-Domain/05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) |
| `GeometriaFactory-Contracts` | `GeometriaFactory.Contracts` | `library` | Tipos de transferencia; contrato compartido por los dos procesos desplegables | false | [`05`](../Proyectos/GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) |
| `GeometriaFactory-Visor` | `geometriafactory-visor` | `library` | Bundle JavaScript del visor 3D; visualizador puro (`RA-02`) | false | [`05`](../Proyectos/GeometriaFactory-Visor/05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) |
| `GeometriaFactory-Application` | `GeometriaFactory.Application` | `library` | Casos de uso y los cuatro puertos | false | [`05`](../Proyectos/GeometriaFactory-Application/05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) |
| `GeometriaFactory-Web` | `GeometriaFactory.Web` | `web-monolith` | Front en el hosting público; **único punto de contacto del navegador** | false | [`05`](../Proyectos/GeometriaFactory-Web/05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) |
| `GeometriaFactory-Infrastructure` | `GeometriaFactory.Infrastructure` | `library` | Adaptadores de los cuatro puertos, seguridad y validador de figuras | false | [`05`](../Proyectos/GeometriaFactory-Infrastructure/05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) |
| `GeometriaFactory-Api` | `GeometriaFactory.Api` | `rest-api` | Host en el servidor propio (**principal**) | false | [`05`](../Proyectos/GeometriaFactory-Api/05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) |

**Siete proyectos de código, tres tipos D8**: `library` (5), `web-monolith` (1) y `rest-api` (1). **Exactamente uno es principal**, `GeometriaFactory-Api`, como exige la validación bloqueante del manifiesto §4.

**Una excepción de nombre y de path, declarada con su fundamento en el manifiesto §2 y en el intake §13.** `GeometriaFactory-Visor` es el único proyecto de código fuera del ecosistema .NET: su `Identidad-Codigo` va en minúscula con guiones porque la forma general sería un nombre de paquete inválido en su gestor, y su carpeta es `visor/` en la raíz y no bajo `src/`, para que las dos cadenas de herramientas no compartan raíz. **Es apartamiento declarado, no incumplimiento del perfil de nombres.**

## 3. Grafo de dependencias

**Dependencias de compilación**, que apuntan siempre hacia adentro (`PRODUCT-MANIFEST` §3):

```text
GeometriaFactory-Domain     -> GeometriaFactory-Application -> GeometriaFactory-Infrastructure -> GeometriaFactory-Api
GeometriaFactory-Domain     -> GeometriaFactory-Infrastructure
GeometriaFactory-Contracts  -> GeometriaFactory-Api
GeometriaFactory-Contracts  -> GeometriaFactory-Web
GeometriaFactory-Visor      -> GeometriaFactory-Web
```

**El bloque de arriba transcribe el diagrama del manifiesto §3, que dibuja siete aristas. El propio manifiesto declara ocho en su §2.** La discrepancia sigue viva y se detalla en §3.1. El grafo es **acíclico bajo las dos lecturas**. [PRECISADO 2026-08-10 al cerrar `N-1` de `C-05-Arquitectura-Siete-Proyectos-r2.md`: este apartado declaraba **siete** aristas mientras las secciones 4 y 8 de este mismo documento enumeran **ocho**. La octava es `Application → Api`.]


**La arista que no está en el grafo, y por qué no introduce ciclo.** `GeometriaFactory-Web → GeometriaFactory-Api` es de **tiempo de ejecución**, no de compilación: el front alcanza el servicio por HTTP con los tipos de `GeometriaFactory-Contracts`, contra los que **los dos extremos compilan por separado**. Por eso no figura como dependencia y por eso el grafo sigue siendo un DAG. Es también la razón por la que el producto tiene **dos procesos desplegables** y no uno.

**Orden topológico de construcción**, en cuatro niveles, que es el mismo en el que se emitieron las tres olas de la Fase C:

```text
nivel 0: GeometriaFactory-Domain, GeometriaFactory-Contracts, GeometriaFactory-Visor   (paralelizables)
nivel 1: GeometriaFactory-Application, GeometriaFactory-Web                            (paralelizables)
nivel 2: GeometriaFactory-Infrastructure
nivel 3: GeometriaFactory-Api
```

### 3.1 La discrepancia del grafo, y por qué sigue abierta

**El producto tiene siete u ocho aristas de compilación según qué sección del manifiesto se lea, y la discrepancia no se resuelve acá.** Está elevada al Product Owner desde el 2026-08-10 y sigue sin desenlace. Lo que esta vista hace es dejarla legible, con las tres lecturas abiertas y contadas sobre el documento original.

| Sección de `PRODUCT-MANIFEST` **1.3** | Qué declara | Aristas que se derivan |
| --- | --- | --- |
| §2, columna `Dependencias` | `GeometriaFactory-Api` depende de `Application`, `Infrastructure` y `Contracts`; `Web` de `Contracts` y `Visor`; `Application` de `Domain`; `Infrastructure` de `Application` y `Domain` | **8**, con `Application → Api` **directa** |
| §3, bloque del grafo | Las cinco líneas que este apartado transcribe | **7**: no dibuja `Application → Api` |
| §4, validaciones bloqueantes | «Cada dependencia referencia un proyecto de código existente en §13 — Cumple: **las siete aristas resuelven**» | **7** |

**Dos contra uno no es una resolución.** La columna de dependencias de §2 es la que un archivo de proyecto materializa al compilar, y el diagrama de §3 es una vista de esa misma tabla; que la vista y la validación coincidan en siete no convierte a la tabla en errónea, porque un consumidor puede declarar una referencia directa a un ensamblado que además le llega transitivamente. **Cuál de las dos formas rige es exactamente lo que hay que decidir, y lo decide quien gobierna el manifiesto.**

**Qué depende de la respuesta, y qué no.** No depende ninguna decisión de arquitectura: el grafo es acíclico con siete y con ocho, el orden topológico de cuatro niveles es el mismo bajo las dos lecturas, y los seis contratos de §4 cubren las mismas fronteras. Lo que sí depende es la forma del archivo de proyecto de `GeometriaFactory-Api` cuando se lo escriba, y el recuento que cualquier documento aguas abajo cite. **Por eso ningún documento de este producto debe afirmar un número sin decir cuál de las tres secciones está leyendo.**

**Desenlace:** abierto. **Titular:** el Product Owner, sobre `PRODUCT-MANIFEST` §2, §3 y §4. **Sin fecha comprometida.**


## 4. Contratos inter-proyecto

**Seis contratos de superficie emitidos**: `Contratos-REST.md` para el `rest-api`, y `Contratos-Abstractions.md` para **las cinco bibliotecas**. Esta sección los indexa contra la arista que materializan; el detalle de cada uno vive en el documento del **proyecto de código productor** y no se reescribe acá.

| # | Arista | Contrato del productor | Qué cruza la frontera |
| --- | --- | --- | --- |
| 1 | `Domain` → `Application`, `Infrastructure` | [`Contratos-Abstractions.md`](../Proyectos/GeometriaFactory-Domain/05-Arquitectura-Tecnica/Contratos-Abstractions.md) | Entidades e invariantes del dominio. **El dominio no lee el reloj ni el conjunto**: lo que no cruza es tan contrato como lo que cruza |
| 2 | `Application` → `Infrastructure`, `Api` | [`Contratos-Abstractions.md`](../Proyectos/GeometriaFactory-Application/05-Arquitectura-Tecnica/Contratos-Abstractions.md) | Los casos de uso y **los cuatro puertos**, que son la frontera hacia afuera del dominio |
| 3 | `Infrastructure` → `Api` | [`Contratos-Abstractions.md`](../Proyectos/GeometriaFactory-Infrastructure/05-Arquitectura-Tecnica/Contratos-Abstractions.md) | Los adaptadores de los cuatro puertos y los dos mecanismos de seguridad, más la responsabilidad de arranque |
| 4 | `Contracts` → `Api` **y** `Contracts` → `Web` | [`Contratos-Abstractions.md`](../Proyectos/GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Contratos-Abstractions.md) | Los tipos de transferencia y el **conjunto cerrado de códigos del contrato**. Es el único contrato que **dos proyectos de código compilan a la vez**, y por eso es la red del producto |
| 5 | `Api` → `Web` (**tiempo de ejecución**) | [`Contratos-REST.md`](../Proyectos/GeometriaFactory-Api/05-Arquitectura-Tecnica/Contratos-REST.md) | Los **quince** puntos de acceso, los **diez** códigos de respuesta, la traducción de los **quince** códigos del contrato y el **formato de intercambio fijado para los dos extremos**. Es el único contrato del producto que cruza una frontera de proceso |
| 6 | `Visor` → `Web` | [`Contratos-Abstractions.md`](../Proyectos/GeometriaFactory-Visor/05-Arquitectura-Tecnica/Contratos-Abstractions.md) | Las **seis** funciones de la fachada del bundle. Es el **punto de extensión declarado del producto** (`tiene_extensibilidad` == true), detallado en [`Extensibilidad.md`](../Proyectos/GeometriaFactory-Visor/05-Arquitectura-Tecnica/Extensibilidad.md) |

**No se emite `Contratos-Inter-Proyecto.md`, y es decisión declarada.** La guía de la categoría admite integrarlo como sección de la vista de producto «si los contratos son pocos». Son **seis**, cada uno con un único documento productor ya emitido y ya auditado, y un séptimo documento que los reindexara sería una segunda fuente de verdad sobre las mismas fronteras —exactamente el defecto que este producto tiene documentado como el más repetido—. Esta sección **los indexa y no los reescribe**.

**El contrato que recorrió tres proyectos de código, y cómo se cerró.** El formato de intercambio es el caso que mejor muestra cómo funciona la frontera en este producto: `Contracts` decidió **no imponerlo** y aceptó por escrito el trade-off ([`Contracts ADR-01`](../Proyectos/GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Adrs/ADR-01-Tipos-De-Transferencia-Planos-Sin-Dependencias.md) §6); `Web` se negó a fijarlo de un solo lado y lo devolvió al productor; y `Api` lo cerró **para los dos extremos** ([`Api ADR-02`](../Proyectos/GeometriaFactory-Api/05-Arquitectura-Tecnica/Adrs/ADR-02-Formato-De-Intercambio-Y-Su-Configuracion.md)), con las **seis reglas de formato** más la notación y la prohibición de normalizar el texto original que publica `Contratos-REST.md` §2.2. **Ninguno de los tres decidió por otro.**

## 5. Decisiones de nivel producto

**No hay `Producto/Adrs/`, y se declara explícitamente en lugar de dejar el hueco.** Las **45** ADR del producto son **todas internas a un proyecto de código**: cada una la decide el proyecto de código que puede sostenerla, y las que alcanzan a más de uno lo hacen **desde el productor**, citadas y no reabiertas por los consumidores. Las tres candidatas naturales a ADR de nivel producto están resueltas así:

| Candidata | Dónde se decide, y por qué ahí | Quién la acata sin reabrirla |
| --- | --- | --- |
| **Estilo de composición** | No es una decisión libre: la fija la regla de dependencias hacia adentro que el intake §13 declara y que el grafo de §3 materializa | Los siete, cada uno en el §2 de su documento maestro |
| **Política de versionado inter-proyecto** | [`Contracts ADR-03`](../Proyectos/GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Adrs/ADR-03-Versionado-Por-Compilacion-Compartida.md) —versionado **por compilación compartida**— y [`Api ADR-08`](../Proyectos/GeometriaFactory-Api/05-Arquitectura-Tecnica/Adrs/ADR-08-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) —**sin versionado de rutas y con despliegue conjunto**—. Vive en el productor de cada contrato | `Api` y `Web`, los dos extremos |
| **Estrategia de comunicación entre proyectos de código** | [`Api ADR-02`](../Proyectos/GeometriaFactory-Api/05-Arquitectura-Tecnica/Adrs/ADR-02-Formato-De-Intercambio-Y-Su-Configuracion.md) para el formato, y [`Web ADR-07`](../Proyectos/GeometriaFactory-Web/05-Arquitectura-Tecnica/Adrs/ADR-07-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md) para la dirección del servicio, que llega por configuración | `Web`, que declaró por escrito que adopta el formato |

**Lo que sí es normativo de nivel producto son las tres reglas de arquitectura del intake §14**, que no son ADR porque **no las decide esta categoría: las recibe**. Los siete proyectos de código las sostienen y ninguna de las 45 ADR las contradice.

| Regla | Enunciado (`PRODUCT-INTAKE` §14) | Dónde tiene mecanismo, y no sólo declaración |
| --- | --- | --- |
| `RA-01` | Ningún JavaScript del navegador invoca la API | [`Contratos-REST.md`](../Proyectos/GeometriaFactory-Api/05-Arquitectura-Tecnica/Contratos-REST.md) §1 declara las **tres ausencias** que se derivan; [`Web ADR-01`](../Proyectos/GeometriaFactory-Web/05-Arquitectura-Tecnica/Adrs/ADR-01-Render-En-El-Servidor-Con-Circuito-Interactivo.md) la sostiene desde el otro lado con el render en el servidor |
| `RA-02` | El bundle del visor es un visualizador puro: sin configuración, sin red, sin conocimiento del sistema | [`Visor ADR-03`](../Proyectos/GeometriaFactory-Visor/05-Arquitectura-Tecnica/Adrs/ADR-03-Visualizador-Puro-Sin-Red-Ni-Identidad.md) fija una puerta bloqueante que se mide **sobre el bundle generado**, no sobre el código fuente |
| `RA-03` | Todo lo que el navegador deba obtener del backend pasa por el front; los mensajes de error nunca incluyen direcciones de servicios internos | [`Api ADR-04`](../Proyectos/GeometriaFactory-Api/05-Arquitectura-Tecnica/Adrs/ADR-04-Dos-Traducciones-Con-Tabla-Unica-Y-Sin-Codigos-Inventados.md) §2 y [`DX-Error-Messages.md`](../Proyectos/GeometriaFactory-Api/03-UX-UI-DX/DX-Error-Messages.md) §1.4, que es **el único lugar donde se puede violar hacia afuera**; [`Web ADR-06`](../Proyectos/GeometriaFactory-Web/05-Arquitectura-Tecnica/Adrs/ADR-06-Aislamiento-Del-Visor-Tras-Su-Fachada.md) aísla el visor tras su fachada |

## 6. Cross-cutting compartido

Convenciones transversales que el producto impone a todos sus proyectos de código. **Cada una se decide en un solo lugar**; acá se indexa dónde, que es lo que evita que se repitan y se desincronicen.

| Preocupación transversal | Qué impone el producto | Dónde se decide |
| --- | --- | --- |
| **Formato de errores común** | Un **conjunto cerrado** de códigos del contrato, un tipo de error único, y **ninguna capa inventa códigos**. La traducción a códigos de respuesta ocurre **una sola vez**, en una tabla única | [`Contracts ADR-02`](../Proyectos/GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Adrs/ADR-02-Tipo-De-Error-Unico-Con-Conjunto-Cerrado.md) y [`Api ADR-04`](../Proyectos/GeometriaFactory-Api/05-Arquitectura-Tecnica/Adrs/ADR-04-Dos-Traducciones-Con-Tabla-Unica-Y-Sin-Codigos-Inventados.md) |
| **Regla de exposición de la frontera** | Qué puede cruzar hacia afuera y qué no, con `RA-03` como piso | [`Contracts ADR-04`](../Proyectos/GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Adrs/ADR-04-Regla-De-Exposicion-De-La-Frontera.md) |
| **Formato de intercambio y su configuración** | **Una sola configuración declarada en todo el producto**, compartida por los dos extremos, elegida para que ninguna regla dependa de que dos configuraciones coincidan | [`Api ADR-02`](../Proyectos/GeometriaFactory-Api/05-Arquitectura-Tecnica/Adrs/ADR-02-Formato-De-Intercambio-Y-Su-Configuracion.md) y [`Contratos-REST.md`](../Proyectos/GeometriaFactory-Api/05-Arquitectura-Tecnica/Contratos-REST.md) §2.2 |
| **Normalización de correos** | **Exactamente un componente normaliza correos** en todo el producto, con el índice único que lo sostiene. La convención cierra por escrito la puerta por la que este defecto vuelve | [`Infrastructure ADR-03`](../Proyectos/GeometriaFactory-Infrastructure/05-Arquitectura-Tecnica/Adrs/ADR-03-Comparacion-De-Correos-Y-El-Indice-Que-La-Sostiene.md) e [`Modelo-Datos-Logico.md`](../Proyectos/GeometriaFactory-Infrastructure/05-Arquitectura-Tecnica/Modelo-Datos-Logico.md) §3 |
| **Unidad de trabajo y escritura** | **Un caso de uso, una unidad de trabajo** como alcance; **un archivo escritor único, una unidad de trabajo por operación** como mecanismo. Alcance y mecanismo se deciden en capas distintas y **no se pisan** | [`Application ADR-05`](../Proyectos/GeometriaFactory-Application/05-Arquitectura-Tecnica/Adrs/ADR-05-Un-Caso-De-Uso-Una-Unidad-De-Trabajo.md) e [`Infrastructure ADR-02`](../Proyectos/GeometriaFactory-Infrastructure/05-Arquitectura-Tecnica/Adrs/ADR-02-Un-Archivo-Escritor-Unico-Y-Una-Unidad-De-Trabajo-Por-Operacion.md) |
| **Gestión de versiones de los paquetes** | **Versionado por compilación compartida**: un cambio incompatible del contrato rompe la compilación de los dos extremos antes que el tiempo de ejecución, y el producto **no versiona rutas** porque no tiene clientes de terceros. La contrapartida aceptada es el **despliegue conjunto** | [`Contracts ADR-03`](../Proyectos/GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Adrs/ADR-03-Versionado-Por-Compilacion-Compartida.md) y [`Api ADR-08`](../Proyectos/GeometriaFactory-Api/05-Arquitectura-Tecnica/Adrs/ADR-08-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) |
| **Correlación de registro y trazas** | **No hay correlación distribuida, y es decisión y no omisión**: el manifiesto §5 declara `tiene_observabilidad_critica` **sólo en `Api`**, y el intake no declara SLO de disponibilidad en ningún proyecto de código. Lo que sí es obligatorio y transversal es el **registro estructurado del lado del servidor de cada error y de cada intento de acceso rechazado**, que es la contracara de la prohibición de `RA-03` | [`Api`](../Proyectos/GeometriaFactory-Api/05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §7 y §8, y [`DX-Error-Messages.md`](../Proyectos/GeometriaFactory-Api/03-UX-UI-DX/DX-Error-Messages.md) §1.4 |

## 7. Riesgos de integración inter-proyecto

Riesgos **de las fronteras**, no internos a un proyecto de código. Los internos están en el §9 de cada documento maestro y no se repiten acá.

| Id | Riesgo de integración | Impacto | Probabilidad | Mitigación, y dónde está escrita |
| --- | --- | --- | --- | --- |
| `RI-01` | **Los dos extremos se configuran distinto sin romper ninguna compilación.** Un campo llega nulo y un estado no se reconoce, en producción y no en construcción | **Alto**: es el único modo de falla del contrato que la compilación compartida **no** atrapa | Media si no se controla | Una **sola** configuración de intercambio en todo el producto, con reglas elegidas para que ninguna dependa de que dos configuraciones coincidan, y verificación **ejerciendo el servicio real** desde la batería de integración, no comparando dos archivos ([`Api ADR-02`](../Proyectos/GeometriaFactory-Api/05-Arquitectura-Tecnica/Adrs/ADR-02-Formato-De-Intercambio-Y-Su-Configuracion.md) §7 y §8) |
| `RI-02` | **Los dos procesos desplegables se despliegan desacoplados** tras un cambio de contrato, y la lectura estricta rompe ante el extremo desactualizado | Medio: rompe **ruidosamente**, con código de respuesta, y no en silencio | Media | **Despliegue conjunto** declarado como regla operativa, y sin versionado de rutas que invite a lo contrario ([`Api ADR-08`](../Proyectos/GeometriaFactory-Api/05-Arquitectura-Tecnica/Adrs/ADR-08-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md)) |
| `RI-03` | **Una capa de arriba reabre lo que una de abajo ya decidió**, y el producto termina con dos decisiones del mismo objeto | Alto: es el defecto que vuelve inauditable una arquitectura por capas | Baja hoy | Tabla de **decisiones heredadas** en el §2.2 de cada documento maestro de nivel superior —`Web` cuatro, `Infrastructure` cinco, `Api` siete—, con la ADR citada por su identificador y su archivo |
| `RI-04` | **El bundle del visor adquiere red, configuración o identidad** al portarse, y `RA-02` deja de ser cierta sin que nadie lo note | Alto: `RA-02` es regla de nivel producto y el bundle se sirve desde el front | Baja | Puerta bloqueante medida sobre el **bundle generado** y no sobre el código fuente ([`Visor ADR-03`](../Proyectos/GeometriaFactory-Visor/05-Arquitectura-Tecnica/Adrs/ADR-03-Visualizador-Puro-Sin-Red-Ni-Identidad.md)), y aislamiento del visor tras la fachada del anfitrión ([`Web ADR-06`](../Proyectos/GeometriaFactory-Web/05-Arquitectura-Tecnica/Adrs/ADR-06-Aislamiento-Del-Visor-Tras-Su-Fachada.md)) |
| `RI-05` | **Una dirección de servicio interno, una ruta del almacén o un secreto cruzan la frontera** dentro de un mensaje de error | Alto: es la violación directa de `RA-03`, y ocurre en el último tramo antes de salir del servidor propio | Baja | La superficie HTTP es **el único lugar donde se puede violar hacia afuera** y lo declara como tal, con tabla de prohibiciones y su contracara de registro ([`DX-Error-Messages.md`](../Proyectos/GeometriaFactory-Api/03-UX-UI-DX/DX-Error-Messages.md) §1.4) |
| `RI-06` | **Un identificador o un recuento del contrato envejece en un documento y no en el otro**, y las dos puntas dejan de cuadrar | Medio: no rompe ejecución, rompe la confianza en el corpus | **Alta, y con historia**: es el defecto que este producto acumuló en tres conjuntos distintos | Cada recuento se **cuenta sobre el instrumento** y cuadra contra el documento productor: el catálogo de la superficie cuadra explícitamente contra [`Contratos-REST.md`](../Proyectos/GeometriaFactory-Api/05-Arquitectura-Tecnica/Contratos-REST.md) §5, y las citas de la fuente se reverifican **el día de la emisión** y no el día en que se empezó a escribir |

## 8. Trazabilidad

Cada contrato inter-proyecto contra la dependencia del manifiesto que materializa y contra lo que cruza la frontera.

| Contrato | Arista del manifiesto §3 | Clase de arista | Qué la cruza |
| --- | --- | --- | --- |
| `Domain/Contratos-Abstractions.md` | `Domain → Application`, `Domain → Infrastructure` | Compilación | Entidades e invariantes; ninguna dependencia saliente |
| `Application/Contratos-Abstractions.md` | `Application → Infrastructure`, `Application → Api` | Compilación | Casos de uso y los cuatro puertos |
| `Infrastructure/Contratos-Abstractions.md` | `Infrastructure → Api` | Compilación | Adaptadores, seguridad y responsabilidad de arranque |
| `Contracts/Contratos-Abstractions.md` | `Contracts → Api`, `Contracts → Web` | Compilación, **en los dos extremos** | Tipos de transferencia y conjunto cerrado de códigos |
| `Api/Contratos-REST.md` | `Web → Api` | **Tiempo de ejecución**, HTTP servidor a servidor | Quince puntos de acceso; formato de intercambio; traducción de códigos |
| `Visor/Contratos-Abstractions.md` | `Visor → Web` | Compilación del empaquetado del front | Las seis funciones de la fachada del bundle |

**Los casos de uso que cruzan una frontera entre proyectos de código** son los que se materializan por `Api/Contratos-REST.md`: **once de los doce** de `GeometriaFactory-Api`, según declara ese contrato en su §1. **El doceavo —la colección de peticiones reproducible— ejercita el contrato en lugar de exponerlo**, y su lugar es el árbol de muestras del repositorio y no la superficie.

**Cobertura del grafo: 6 contratos sobre las aristas de compilación —siete u ocho según la lectura de §3.1— más 1 de tiempo de ejecución.** Las dos aristas de `Contracts` las cubre un solo contrato, porque es el mismo ensamblado el que los dos consumidores compilan; ésa es exactamente la propiedad de la que depende el versionado del producto.

## 9. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.3 | 2026-08-11 | **Cierra el hallazgo `NB2-01` (P1)** del informe [`B2-Maqueta-GeometriaFactory-Web-r2.md`](../Audit/B2-Maqueta-GeometriaFactory-Web-r2.md) 1.0. **§1.1**, fila **B2**: la fila remitía a `B2-Maqueta-GeometriaFactory-Web-r1.md` «cuyo rechazo levanta `F26-Propagacion-r2.md`», y eso es falso: abierto ese informe, su §7 dice «**APROBADO. Se levanta el rechazo de `F26-Propagacion-r1.md`**» y su alcance son tres commits de propagación de F-26, no la Fase B2. **Un informe no puede levantar el rechazo de una fase que no audita, y no dice haberlo hecho.** La fila pasa a citar **`B2-Maqueta-GeometriaFactory-Web-r2.md`**, emitido el 2026-08-11, que sí audita la fase y sí dice levantar el rechazo de la ronda 1, y el dictamen pasa a «Rechazado en ronda 1; **aprobado en ronda 2**». **§1.1**, párrafo del hueco de auditoría: se registra la emisión del informe faltante de Fase B de `GeometriaFactory-Api` —hallazgo que ningún informe levanta y que la búsqueda de propagación de esta tanda dejó al descubierto: el párrafo afirmaba que ese informe **no existe**, y desde el 2026-08-11 existe—, con su dictamen **RECHAZADO** citado y no redecidido. **Búsqueda de propagación hecha con `grep` sobre todo el corpus vivo**: la atribución falsa a `F26-Propagacion-r2.md` no aparece en ningún otro documento; la salvedad `B-2` de [`../Handoff-Checkout.md`](../Handoff-Checkout.md), que decía lo contrario **y con razón al escribirse**, se actualiza en la misma tanda. **Ninguna magnitud, ningún proyecto de código y ninguna decisión de arquitectura cambia.** |
| 1.2 | 2026-08-11 | **Consolidación de la Fase H.** Se revisa el documento entero contra `PRODUCT-MANIFEST` **1.3** y `PRODUCT-INTAKE` **1.26**, que son las versiones vivas; la 1.1 citaba el manifiesto 1.2 y el intake 1.18. **(a)** Nueva §1.1 con el estado real de cierre: las ocho fases con el informe de auditoría que cierra cada una y su dictamen, la constancia de que cada proyecto de código tiene **ocho** categorías emitidas y no siete —`04` omitida por gating con `usa_llm` false en los siete—, y las catorce magnitudes del producto **contadas sobre el instrumento** el día de esta emisión. **(b)** Se declara un **hueco de auditoría**: hay seis informes `B-02-03-` para siete proyectos de código, y el que falta es el de `GeometriaFactory-Api`. Queda registrado y **sin cerrar**: no es una decisión de arquitectura. **(c)** Nueva §3.1 con la discrepancia del grafo desplegada contra las tres secciones del manifiesto que la producen —§2 declara ocho aristas, §3 dibuja siete, §4 valida siete— y con lo que depende y lo que no depende de la respuesta. **Sigue abierta y elevada al Product Owner; esta versión no la cierra.** §3 y §8 dejan de afirmar un número único y remiten a §3.1. Sube minor: no reabre ninguna ADR, no altera el mapa, los contratos ni los riesgos, y agrega estado verificable. **Autor:** Arquitecto de Software Senior (AG-05) |
| 1.1 | 2026-08-10 | Cierra `N-1` de `SDD/Docs/Audit/C-05-Arquitectura-Siete-Proyectos-r2.md` 1.0: §3 declaraba **siete** aristas de compilación mientras §4 y §8 enumeran **ocho**. Se corrige a ocho y se declara que el desacuerdo nace en el `PRODUCT-MANIFEST`, entre su §2 —que declara `Application → Api` como dependencia directa— y el diagrama de su §3, que no la dibuja. Queda elevado al Product Owner; el grafo es acíclico bajo las dos lecturas. **Autor:** Orquestador SDD |
| 1.0 | 2026-08-10 | Emisión inicial de la vista de producto, **al cierre del bucle de proyectos de código**: los siete están emitidos, en tres olas y en orden topológico. **Cierra el hallazgo `C-05-06` (P3) del informe de auditoría [`../Audit/C-05-Arquitectura-Siete-Proyectos-r1.md`](../Audit/C-05-Arquitectura-Siete-Proyectos-r1.md) 1.0**, que levantó que el artefacto era obligatorio para productos de más de un proyecto de código y que **ningún artefacto declaraba su ausencia**. Declara las **ocho** secciones que la guía de la categoría exige: el mapa de los siete proyectos de código con su tipo D8 y la excepción de nombre y path del visor; el grafo con sus **siete** aristas de compilación, la arista de tiempo de ejecución que **no** introduce ciclo y el orden topológico de cuatro niveles; los **seis** contratos inter-proyecto indexados contra la arista que materializan, **sin reescribirlos**; la ausencia declarada de `Producto/Adrs/` con las tres candidatas naturales resueltas en el productor, más las tres reglas de arquitectura de nivel producto con el mecanismo que las sostiene; **siete** preocupaciones transversales con el único lugar donde se decide cada una; **seis** riesgos de integración con impacto, probabilidad y mitigación escrita; y la trazabilidad de cada contrato contra su arista. **No toma ninguna decisión y no reabre ninguna de las 45 ADR emitidas**: referencia, no reescribe, como exige §4.8 de la guía. Declara además, con su motivo, que **no se emite `Contratos-Inter-Proyecto.md`**: los contratos son seis y la guía admite integrarlos como sección de esta vista, y un séptimo documento que los reindexara sería una segunda fuente de verdad sobre las mismas fronteras. |
