# Norma de nomenclatura — Fábrica de Geometría

**Producto:** Fábrica de Geometría
**Documento:** Norma-De-Nomenclatura.md
**Estado:** Aprobado
**Fecha:** 2026-08-15
**Autor:** Orquestador SDD (medición y redacción) · Product Owner (las tres decisiones de §5)
**Nivel:** Producto
**Origen:** Observación del Product Owner, 2026-08-12: el estándar nombra espacios de nombres, clases y variables en inglés, y el corpus se salió del estándar sin declararlo. La versión 1.1 incorpora las **tres decisiones tomadas por el Product Owner el 2026-08-12** sobre las zonas de frontera que la 1.0 elevó. La versión 1.2 **corrige el método**, no los nombres: el tramo de ensayo `R-1` de §8 se ejecutó y el método falló en cinco puntos, y ésta es la emisión que los repara antes de que arranque el tramo siguiente. La versión 1.3 **vuelve a corregir el método**, y por la misma vía: el tramo `R-1b` se ejecutó, **cerró limpio** —los siete controles aplicables cuadraron y las cifras de la 1.2 se reprodujeron exactas, lo que valida las correcciones de esa versión— y encontró **tres defectos nuevos**: `V-4` no admitía por escrito el motivo que cubrió diez de las diecisiete ocurrencias no renombradas, la fila de control de cambios del propio tramo rompía el cuadre literal, y el acto 2 de §8.2 no tenía lista previa aunque corre después del acto 1. Ésta los repara **antes de `R-2`**, que toca el intake. La versión 1.4 **corrige el método por tercera y última vez antes de `R-2`**, y no a partir de un tramo ejecutado sino de **la revisión de la propia 1.3**, que se preguntó qué otros controles prometen más de lo que pueden verificar —que es la clase de defecto que compartían los tres que la 1.3 reparó— y encontró **tres**: `V-1` no podía cuadrar §6.10 ni §6.11, que la propia norma agregó al glosario; `V-6` levantaba como falla las diez ocurrencias que la norma declaró correctas; y `V-7` ordenaba lo contrario que §8.2. Y declara, control por control, **cuál de los siete y de los tres barridos de §8.2 se verifica tal como está escrito** La versión 1.5 la trae una **decisión del Product Owner del 2026-08-13**, tomada al arrancar la etapa `a`: **los cinco tramos de renombre que quedaban se suspenden**, porque renombraban identificadores en documentos que describen código que no existe, y el glosario ya está completo para escribir ese código en inglés desde el primer archivo. §8 registra la suspensión y la regla que la reemplaza; §8.2 corrige además el defecto que `R-2` levantó al ejecutarse. La versión 1.12 la trae la **etapa `d`**, que construye el ciclo de vida de la cuenta de alumno del lado del servicio: agrega **§6.15** con 36 filas, amplía el rango del glosario a **doce tablas** y corrige dos recuentos que estaban desactualizados —el de `V-1`, que decía nueve tablas desde la 1.6, y la tabla de contenido, que no listaba §6.14—. La versión 1.11 la trae la **guardia de arranque de la clave de firma**, y su origen es un hecho del despliegue y no una revisión de escritorio: el servicio de datos se levantó sin la clave, se comportó como si estuviera sano y falló recién con una persona adelante intentando entrar; agrega dos filas y **reconcilia dos recuentos que estaban mal desde la 1.8**. La versión 1.10 la trae el **arreglo del cambio forzado tras la fusión**: **retira dos identificadores** —`PasswordChangeEmail` y `BeginPasswordChange`—, que **conservan su fila con el motivo del retiro** porque eso es lo que §4.1 y §6.8.5 mandan con un identificador retirado, y **agrega cinco filas** a §6.14 con lo que la pantalla del cambio forzado necesitó para dejar de depender del estado de la petición anterior.
**Trazabilidad upstream:** [`../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.30** §13.1 (que registra las tres decisiones de §5), §13, §17.1.P.11, §17.2.P.1, §17.3.P.4, §17.4.P.3, §17.7.P.3 (que declaraba los nombres de la fachada **a fijar en la etapa que la implementa**); [`../Handoff-Checkout.md`](../Handoff-Checkout.md) §6.2 `A-1` y `A-2`; [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.2 a §1.7; y **el informe del tramo `R-1`**, que es el mensaje del commit `1edccca` del 2026-08-12 —«tramo `R-1`: el ensayo del renombre, y las cinco correcciones que compró»—, única fuente de los cinco defectos que la 1.2 corrigió; y **el informe del tramo `R-1b`**, que es el mensaje del commit `c0b8b4f` del 2026-08-12 —«tramo `R-1b`: la deuda del ensayo, con los dos actos»—, única fuente de los tres defectos que la 1.3 corrigió y del apartamiento que registra; y **la revisión de método de la 1.3 que el orquestador ordenó antes de abrir `R-2`**, que no tiene informe aparte porque su resultado es esta emisión, y que es la única fuente de los tres defectos que la 1.4 corrige
**Trazabilidad downstream:** el punto de control de la etapa `a`; las siete categorías `05-Arquitectura-Tecnica`; la tanda de renombre que ordena §8; [`../Audit/Observacion-Desviacion-De-Nomenclatura.md`](../Audit/Observacion-Desviacion-De-Nomenclatura.md)

---

## Tabla de contenido

- [1. Qué fija esta norma y qué no](#1-qué-fija-esta-norma-y-qué-no)
- [2. El alcance real, contado](#2-el-alcance-real-contado)
  - [2.1 Cómo se contó](#21-cómo-se-contó)
  - [2.2 Las seis clases, remedidas](#22-las-seis-clases-remedidas)
  - [2.3 Las superficies derivadas, contadas](#23-las-superficies-derivadas-contadas)
  - [2.4 Lo que el recuento decide](#24-lo-que-el-recuento-decide)
- [3. Zona 1 · Identificadores de código, en inglés](#3-zona-1--identificadores-de-código-en-inglés)
- [4. Zona 2 · Texto, en castellano](#4-zona-2--texto-en-castellano)
  - [4.1 Citas, reportes de fuente ajena y uso propio](#41-citas-reportes-de-fuente-ajena-y-uso-propio)
- [5. Zona de frontera · Las tres decisiones tomadas](#5-zona-de-frontera--las-tres-decisiones-tomadas)
  - [5.1 `F-01` Las seis funciones de la fachada del visor · **decidida**](#51-f-01-las-seis-funciones-de-la-fachada-del-visor--decidida)
  - [5.2 `F-02` Los valores de los conjuntos cerrados · **decidida**](#52-f-02-los-valores-de-los-conjuntos-cerrados--decidida)
  - [5.3 `F-03` Los códigos de condición y de contrato · **decidida, y es cambio de contrato**](#53-f-03-los-códigos-de-condición-y-de-contrato--decidida-y-es-cambio-de-contrato)
  - [5.4 Lo que no es frontera y no se discute: el dato del alumno](#54-lo-que-no-es-frontera-y-no-se-discute-el-dato-del-alumno)
- [6. El glosario de correspondencia](#6-el-glosario-de-correspondencia)
  - [6.1 La regla del glosario](#61-la-regla-del-glosario)
  - [6.2 Cobertura del glosario, contada](#62-cobertura-del-glosario-contada)
  - [6.3 Clase 1 · Interfaces y puertos (5)](#63-clase-1--interfaces-y-puertos-5)
  - [6.4 Clase 2 · Entidades y tipos (31)](#64-clase-2--entidades-y-tipos-31)
  - [6.5 Clase 3 · Miembros y propiedades (2)](#65-clase-3--miembros-y-propiedades-2)
  - [6.6 Clase 4 · Las seis funciones de la fachada (6)](#66-clase-4--las-seis-funciones-de-la-fachada-6)
  - [6.7 Clase 5 · Valores de conjuntos cerrados (10)](#67-clase-5--valores-de-conjuntos-cerrados-10)
  - [6.8 Clase 6 · Códigos de condición y de contrato (101)](#68-clase-6--códigos-de-condición-y-de-contrato-101)
  - [6.9 Las dos unificaciones y las cuatro coincidencias de nombre](#69-las-dos-unificaciones-y-las-cuatro-coincidencias-de-nombre)
  - [6.10 Los espacios de nombres](#610-los-espacios-de-nombres)
  - [6.11 Las superficies derivadas: carpetas y nombres de archivo](#611-las-superficies-derivadas-carpetas-y-nombres-de-archivo)
  - [6.12 Agregados por la etapa `b` de `GeometriaFactory-Web`, fuera de los 155](#612-agregados-por-la-etapa-b-de-geometriafactory-web-fuera-de-los-155)
  - [6.13 Agregados por la etapa `c`, fuera de los 155](#613-agregados-por-la-etapa-c-fuera-de-los-155)
  - [6.14 Agregados por la sesión por marca de navegador, fuera de los 155](#614-agregados-por-la-sesión-por-marca-de-navegador-fuera-de-los-155)
  - [6.15 Agregados por la etapa `d`, fuera de los 155](#615-agregados-por-la-etapa-d-fuera-de-los-155)
- [7. Cómo se verifica esta norma](#7-cómo-se-verifica-esta-norma)
- [8. El plan de renombre](#8-el-plan-de-renombre)
  - [8.1 Los siete tramos](#81-los-siete-tramos)
  - [8.2 Los dos actos de cada tramo](#82-los-dos-actos-de-cada-tramo)
- [9. Control de cambios](#9-control-de-cambios)

---

## 1. Qué fija esta norma y qué no

**Fija el idioma de los identificadores de código y el idioma del texto, y separa las dos cosas.** Hasta hoy el producto no tenía una norma de nomenclatura: tenía una práctica. La práctica nació de transcribir el material del Product Owner y se propagó hasta parecer una convención; el detalle de cómo ocurrió está en [`../Audit/Observacion-Desviacion-De-Nomenclatura.md`](../Audit/Observacion-Desviacion-De-Nomenclatura.md).

**Registra las tres decisiones de frontera, que son del Product Owner.** La versión 1.0 las elevó con su costo contado y su alternativa real, que es lo que hizo posible decidirlas. **El Product Owner las decidió el 2026-08-12** y §5 las registra como decisiones tomadas, con fecha, fundamento y costo. Este documento no las toma: las asienta y las hace verificables.

**Y produce el glosario completo, que es el entregable que hace ejecutable al renombre.** §6 cubre los **155 identificadores** de las seis clases de §2.2, y desde la 1.2 también las **cinco superficies derivadas** de §6.11 —las carpetas y los nombres de archivo que ninguna de las seis clases contaba—. La regla que lo gobierna es una sola: **si un concepto no está en la tabla, no se traduce por criterio propio — se agrega primero**.

**No renombra nada.** Ninguna emisión de esta tanda modifica un identificador del corpus. Renombrar es un acto posterior, se ejecuta **contra el glosario de §6** y su orden es el de **§8**. Hacerlo antes del glosario es lo que produce tres nombres distintos para la misma cosa.

**Y no reabre el nombre del producto ni el de los siete proyectos de código.** Están declarados en el intake §13 y §16, ya son ingleses en su raíz (`GeometriaFactory`, `GeometriaFactory-Domain`, `GeometriaFactory-Api`…) y no son punto abierto — [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.1 lo verifica.

## 2. El alcance real, contado

### 2.1 Cómo se contó

Sobre el árbol `SDD/` del 2026-08-12, **excluidos `_legacy/` y `Docs/Audit/`**: **632 archivos** —616 `.md`, 12 `.html`, 3 `.js` y 1 `.css`—. La 1.0 y la 1.1 declararon **631**, y la diferencia está medida y no es un error de conteo: la 1.0 contó el árbol **antes de emitirse**, y este documento es el archivo 632. Contado sobre el árbol previo a la emisión de la 1.0 da 631; sobre el de hoy da 632.

**La medición corre sobre 631 de esos 632, porque se excluye este documento.** Sus §6.3 a §6.11 son la tabla de correspondencia, de modo que cada identificador castellano que aparece acá es un **reporte** y no un uso: §4.1 lo declara como regla general y no como excepción de conveniencia. La 1.1 ya lo hacía sin decirlo, y no decirlo es parte de lo que esta versión corrige.

**Qué cuenta cada cifra.** Tres unidades distintas, y confundir dos de ellas en un solo lugar es el defecto que trajo esta versión:

1. **Identificadores distintos**: cuántos nombres hay.
2. **Documentos**: en cuántos archivos aparece al menos una vez algún identificador del grupo.
3. **Ocurrencias**: cuántas veces en total. Un identificador citado quince veces en un archivo cuenta un documento y quince ocurrencias.

**El instrumento, exacto.** Cada cifra de §2.2, §2.3 y §8 sale de este procedimiento, aplicado con herramienta sobre los 631 archivos:

1. De cada archivo se extraen sus **regiones de código**: los bloques cercados con tres acentos graves y los tramos entre acentos graves simples. Es como el corpus marca un identificador, y es lo que separa `` `Pendiente` `` de «pendiente» en prosa.
2. Cada región se parte en átomos por espacio y puntuación, y **todo átomo que sea un nombre de archivo de documentación** —termina en `.md`, `.html` o `.css`— **se descarta entero**: §4 los declara castellanos y el renombre no los toca.
3. Cada átomo restante se parte en *tokens* por todo carácter que no sea letra, dígito o `_`. Así `Persistence/Configuraciones/AccountConfiguration.cs` da tres tokens y aporta **una ocurrencia de la carpeta `Configuraciones`**, y `Entities/{Cuenta,…}.cs` aporta **una ocurrencia del nombre de archivo `Cuenta`**.
4. Una **ocurrencia** es un token idéntico al identificador. La comparación es exacta y sensible a mayúsculas: `TRABAJO` no se confunde con `Trabajo`, ni `CORREO_YA_REGISTRADO` con `CONTRATO_CORREO_YA_REGISTRADO`.

**El paso 3 es la corrección de fondo del defecto de las rutas.** Ninguna cifra de la 1.1 contaba las carpetas ni los nombres de archivo, y §3 obliga a contarlas: §2.3 mide cuánto es eso.

**Y lo que se declara es la ocurrencia *candidata*, no la ocurrencia a renombrar.** Un token idéntico al identificador puede ser el identificador, o puede ser un homónimo: prosa marcada como código, una cita de fuente ajena (§4.1), u otro concepto con el mismo nombre. **§4.1 le da fila propia a ese tercer caso desde la 1.3**, porque era el que más pesaba y el que ningún motivo admitido cubría. **Esa separación no la hace una herramienta**: la hace el tramo, por escrito y **antes de editar**, y `V-4` de §7 la exige así. Donde ya está medida, se declara.

**El instrumento reproduce seis cifras de la 1.1 con exactitud**, lo que es la evidencia de que mide lo mismo que ella creía medir: clase 1 declarada (12 documentos y 56 ocurrencias), clase 1 propuesta (1 y 5, sobre el estado previo a `R-1`), clase 3 (3 y 8), los 21 `CONTRATO_*` (220 y 1201), el catálogo vivo de `GeometriaFactory-Infrastructure` (205 y 394) y los 21 documentos que declaran las seis funciones de la fachada. Las que **no** reproduce están abajo con su diferencia y su causa.

### 2.2 Las seis clases, remedidas

| Clase | Identificadores distintos | Documentos | Ocurrencias candidatas | Lo que decía la 1.1 |
| --- | --- | --- | --- | --- |
| **1. Interfaces y puertos** | **5** — `IRepositorioTrabajos`, `IValidadorFiguras`, `IRelojDelSistema` declarados; `IRepositorioCuentas` e `IRepositorioAlumnos` propuestos | 12 declarados · 0 propuestos hoy (1 antes de `R-1`) | 56 declarados · 0 propuestos hoy (5 antes de `R-1`) | 12 · 1 documentos; 56 · 5 ocurrencias — **coincide** |
| **2. Entidades y tipos** | **31** — 5 entidades, 5 tablas en mayúsculas, 7 tipos de figura, 14 tipos y adaptadores propuestos | 46 · 7 · 39 · 0 hoy (1 antes de `R-1`) | 297 · 34 · 358 · 0 hoy (30 antes de `R-1`) | 3 · 3 · 30 · 1 documentos; 37 · — · 201 · 18 ocurrencias |
| **3. Miembros y propiedades** | **2** — `HashContrasena`, `JsonOriginal` | 3 | 8 | 3 y 8 — **coincide** |
| **4. Funciones de la fachada del visor** | **6** | **53**, de los cuales **21** declaran las seis | **621** | 52 documentos y 593 ocurrencias |
| **5. Valores de conjuntos cerrados** | **10** | **399** | **4461** | 396 documentos y 4259 ocurrencias |
| **6. Códigos de condición y de contrato** | **101** | **330** | **2847** | 334 documentos y 2911 ocurrencias |

**El total sin solapamiento entre clases: 155 identificadores distintos en 464 de los 631 archivos medidos, con 8712 ocurrencias candidatas** —8682 medidas hoy, más las 30 de los 14 tipos propuestos que `R-1` ya renombró, medidas sobre el estado previo—. La 1.1 declaraba 155 identificadores en 459 de 631 archivos con 8111 ocurrencias.

Los desgloses que hacen falta para decidir:

| Desglose | Distintos | Documentos | Ocurrencias | 1.1 |
| --- | --- | --- | --- | --- |
| Clase 2, las **5 entidades** | 5 | 46 | 297 | 3 y 37 |
| Clase 2, las **5 tablas en mayúsculas** | 5 | 7 | 34 | 3 documentos, ocurrencias no declaradas |
| Clase 2, entidades **y** tablas, sin solapamiento | 10 | 49 | 331 | — |
| Clases 1, 2 y 3 juntas | 38 | 88 | 754 | 38 identificadores en 37 documentos |
| Clase 5, sólo los **seis estados** | 6 | 386 | 4030 | 384 y 3874 |
| Clase 5, sólo `Pendiente` | 1 | 351 | 1983 | 349 y 1919 |
| Clase 5, `Pendiente` en documentos que traen **los dos contextos** | 1 | 58 | 956 | — |
| «pendiente» **en prosa**, fuera de toda región de código | — | 254 | 934 | — |
| Clase 6, sólo los `CONTRATO_*` | 21 | 220 | 1201 | 220 y 1201 — **coincide** |
| Clase 6, catálogo de `GeometriaFactory-Domain` | 42 vivos + 5 retirados | 64 | 749 | 65 y 810 |
| Clase 6, catálogo de `GeometriaFactory-Application` | 36 vivos | 99 | 775 | 114 y 1059 |
| Clase 6, catálogo de `GeometriaFactory-Infrastructure` | 17 vivos | 205 | 394 | 205 y 394 — **coincide** |
| Clase 6, catálogo de `GeometriaFactory-Visor` | 7 vivos | 44 | 287 | 48 y 351 |
| Clase 6, los 4 retirados de §6.8.5 | 4 | 13 | 47 | — |

**Las tres diferencias, con su causa medida.**

**Primera, y es la grande: la clase 2 estaba mal contada por un orden de magnitud.** La 1.1 daba las cinco entidades en **3 documentos y 37 ocurrencias**; son **46 documentos y 297 ocurrencias**. La causa es que se contó sobre los documentos que **declaran** el modelo de dominio y no sobre el corpus que **usa** los nombres: `` `Trabajo` `` aparece marcado como identificador en 35 documentos —casos de uso, historias de usuario, contratos de datos de la maqueta, reglas de negocio— y `` `Cuenta` `` en 18. La consecuencia para el plan está en §8: esta clase **no era barata**, y por eso necesita tramo propio.

**Segunda: la clase 6 se midió por forma y no contra la lista.** La 1.1 contó todo token con forma `SCREAMING_SNAKE_CASE`. El corpus tiene **141 tokens distintos de esa forma, en 334 documentos y 3022 ocurrencias** —de ahí salen sus 334 documentos—, y **sólo 101 son códigos del catálogo**. Los otros 40 son constantes de la maqueta de `GeometriaFactory-Web` —`SELLO_MAQUETA`, `ROTULOS_DE_ESTADO`, `PASO_ANGULO`, `TEXTO_E1`, `ARBOL_E7`…— y **`TRANSICION_DE_TRABAJO_NO_ADMITIDA`**, con **1 ocurrencia**, que no es un huérfano nuevo: es un código que `RN-05` acuñó y retiró, y su única ocurrencia está **dentro de la fila de control de cambios que declara el retiro**. Es el caso que §4.1 existe para proteger.

**Tercera, y son menores: la clase 4 y la clase 5 suben.** La fachada pasa de 52 a 53 documentos y de 593 a 621 ocurrencias, y los conjuntos cerrados de 396 a 399 y de 4259 a 4461, porque el instrumento cuenta también las formas punteadas —`EstadoDeCuenta.Pendiente`— y las que viven dentro de bloques cercados, que el recuento anterior no alcanzaba.

**El recuento de la clase 6 se rehizo para la versión 1.1, sobre los seis catálogos, y cierra en 101.** El desglose está en §6.2 y conviene anticipar el término que no era obvio: los **80 internos** son **76 vivos** —la unión de los cuatro catálogos, descontado el solapamiento— **más 4 retirados** que ya no son condición de ningún catálogo y siguen apareciendo en el corpus. El quinto retirado que `GeometriaFactory-Domain` declara sigue **vivo en `GeometriaFactory-Application`** y por eso está entre los 76.

Los desgloses de la clase 6 **se solapan**: un mismo código lo declara `GeometriaFactory-Domain` y lo cita `GeometriaFactory-Application`. La cifra sin solapamiento es la de §2.2: **101 distintos en 330 documentos, 2847 ocurrencias**.

### 2.3 Las superficies derivadas, contadas

**Cada identificador se escribe además como carpeta y como nombre de archivo, y eso no es opcional: dos reglas ya declaradas lo obligan.** La regla de forma 3 de §3 fija que **el espacio de nombres coincide con la carpeta**, y la última fila de la tabla de §3 fija que **el nombre de archivo de código es igual al tipo que contiene**. Renombrar el tipo sin renombrar su archivo deja el corpus contra su propia norma; el ensayo `R-1` lo descubrió al ejecutarse, y ninguna cifra de la 1.1 lo contaba.

Lo que hay hoy, contado archivo por archivo con el instrumento de §2.1:

| Superficie escrita | Dónde | Ocurrencias | Quién la arrastra |
| --- | --- | --- | --- |
| `Entities/{Cuenta,Trabajo,Pieza,Componente,Observacion}.cs` — cinco nombres de archivo | [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.6 | 5 | El tramo `R-2b` de §8, con las cinco entidades |
| `Ports/{…,IRepositorioTrabajos,IValidadorFiguras,IRelojDelSistema}.cs` — tres nombres de archivo | [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.6 | 3 | El tramo `R-2`, con los tres puertos declarados |
| `VisorFiguras.razor` — nombre de archivo del componente Blazor que envuelve al visor | Intake §17.7.P.2 | 1 | El tramo `R-3`, con la fachada; §6.11 le da fila |
| `Persistence/Configuraciones/` — carpeta **por debajo** del nivel de espacio de nombres | [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.6 | 1 | §6.11. `R-1` la dejó sin renombrar porque ninguna regla la cubría |
| `Components/Paginas/` — carpeta por debajo del nivel de espacio de nombres | [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.6 y §1.7 | 2 | §6.11, ídem |
| `visor/src/visor/` — carpeta de la capa 3 del *bundle*, por debajo del nivel | [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.6 | 1 | §6.11, y no lo había detectado el ensayo |
| `Entities.Accounts.Internos` — segmento del contraejemplo de la regla de un solo nivel | [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.3 | 1 | §6.11, ídem |
| `Persistence/Migrations/` y `Components/Layout/` — carpetas por debajo del nivel, **ya en inglés** | [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.6 | 2 | Ninguno: se verifican y se dejan |

**Dieciséis ocurrencias en dos documentos**, y ésa es la medida real de la superficie derivada hoy: el corpus dibuja el árbol de código en **dos lugares y en ningún otro** —el plan de la etapa `a` y el intake—.

**Que hoy sea chica no la vuelve despreciable, y el ensayo lo probó.** Los 16 subsegmentos de espacio de nombres de §6.10 son, cada uno, **también una carpeta**, de modo que la superficie derivada duplicó el alcance del tramo `R-1` respecto de lo que su fila declaraba. La regla que la gobierna —y que faltaba— es **§6.11**, y existe para que el día que el código exista no haya que redescubrirla documento por documento.

### 2.4 Lo que el recuento decide

Tres cosas, y son las que ordenan el resto del documento.

**Primera: hay dos poblaciones y no una.** Los identificadores *propuestos* —los 14 tipos y adaptadores de [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.6 y §1.7, los dos puertos alternativos, los 16 subsegmentos de espacio de nombres de su §1.3 y los 6 derivados de §6.4— viven en **un solo documento** y son **38 identificadores con 80 ocurrencias candidatas**, de las cuales **2 son prosa** —`Estado` en el campo de cabecera del documento y en una frase— y **78 se renombran**. La 1.1 declaraba «41 ocurrencias», y era **la única cifra de la norma que no se midió con herramienta**: cuarenta y uno era un recuento de *identificadores*, no de ocurrencias. El tramo `R-1` la midió al ejecutarse —informó 81— y esta versión la remide con el instrumento de §2.1. Renombrarlos costó una edición; eso sigue siendo cierto.

**Segunda: el grueso del corpus sí está en juego, y la 1.1 decía lo contrario.** Las clases 1, 2 y 3 juntas —puertos, entidades, miembros— no son «38 identificadores en 37 documentos»: son **38 identificadores en 88 documentos, con 754 ocurrencias**. Lo que casi no existe todavía son los puertos y los miembros —15 documentos entre los dos—; las **entidades** están escritas en 46 documentos. La frase de la 1.1 —«ahí la norma se aplica sin negociación, porque casi nada existe todavía»— era verdadera para las clases 1 y 3 y **falsa para la clase 2**, y de esa falsedad salió el hueco del plan que §8 cierra con `R-2b`.

**Tercera: `Pendiente` sola pesa más que las clases 1, 2 y 3 juntas, y no se puede renombrar por cadena.** **1983 ocurrencias en 351 documentos**, y **nombra dos cosas distintas**: una cuenta que espera habilitación y un trabajo que espera revisión. Tres cifras más, medidas, que deciden cómo se ejecuta `R-4`: **58 documentos traen los dos contextos** —estados de cuenta y estados de trabajo en el mismo archivo— **con 956 ocurrencias entre ellos**, que es donde un renombre por cadena elige mal; y **«pendiente» aparece 934 veces en 254 documentos fuera de toda región de código**, es decir en prosa, donde §4 manda que se quede. [`../Proyectos/GeometriaFactory-Domain/02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md`](../Proyectos/GeometriaFactory-Domain/02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md) §2.1 ya tuvo que declarar una forma calificada obligatoria —«marca de cambio de contraseña pendiente»— justamente porque «`Pendiente` a secas nombra un estado de cuenta y un estado de trabajo». Un identificador en inglés no habría tenido esa colisión: `Pending` y `Submitted` son palabras distintas.

## 3. Zona 1 · Identificadores de código, en inglés

**Es el estándar y no se discute.** Todo identificador que el compilador o el intérprete lee va en inglés.

| Qué | Idioma | Forma | Ejemplo |
| --- | --- | --- | --- |
| Espacios de nombres | Inglés | `PascalCase`, segmentos separados por `.` | `GeometriaFactory.Domain.Entities` |
| Clases, `record`, `struct` | Inglés | `PascalCase` | `Account`, `Work` |
| Interfaces | Inglés | `PascalCase` con prefijo `I` | `IWorkRepository` |
| Enumeraciones y sus miembros | Inglés | `PascalCase` | `AccountStatus.Enabled` |
| Propiedades y métodos públicos | Inglés | `PascalCase` | `PasswordHash`, `OriginalJson` |
| Parámetros y variables locales | Inglés | `camelCase` | `accountId` |
| Campos privados | Inglés | `_camelCase` | `_clock` |
| Funciones y variables de TypeScript | Inglés | `camelCase`; `PascalCase` para clases | `loadJson` |
| Nombres de archivo de código | Inglés | Igual al tipo que contienen | `Account.cs` |

**Tres reglas de forma que acompañan:**

1. **La raíz del espacio de nombres no cambia.** Es `GeometriaFactory`, declarada por el intake §13 como `Raiz-Codigo`, y no es punto abierto.
2. **El proyecto ya es la capa.** No se repite el nombre de la capa en el subsegmento: nada de `GeometriaFactory.Domain.Domain`. Es la alternativa `P-2c` que [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.3 ya descartó, y se sostiene.
3. **Un solo nivel de subsegmento**, y el espacio de nombres coincide con la carpeta. También de §1.3, y también se sostiene: lo único que cambia es el idioma del subsegmento.

**Sin tildes ni eñes deja de ser un problema.** La consecuencia que [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.2 declaraba había que aceptar —escribir `Contrasena` sin eñe, `Descripcion` sin tilde, y convivir con la incomodidad— **desaparece**: `PasswordHash` y `Description` no tienen el problema. Es un beneficio lateral y conviene decirlo, porque el argumento de §1.2 lo trataba como costo inevitable.

## 4. Zona 2 · Texto, en castellano

**Todo lo que lee una persona va en castellano rioplatense neutro técnico, con tildes y eñes.** No cambia nada respecto de lo que el corpus ya hace; se escribe para que la separación quede explícita y para que nadie traduzca la documentación por simetría con §3.

| Qué | Idioma | Nota |
| --- | --- | --- |
| Documentación del corpus `SDD/` | Castellano | Es la invariante `D1` que las 33 auditorías ya verifican |
| Nombres de archivo de la documentación | Castellano, ASCII, `Título-Con-Guiones` | `Definicion-Modelo-De-Dominio.md`. Sin tildes en el nombre, con tildes en el cuerpo |
| Comentarios de código | Castellano | El código lo lee un alumno de Programación 2, y el producto es didáctico |
| Mensajes al usuario | Castellano, con tildes | El catálogo de condiciones **no transporta texto de presentación**: lo compone quien expone |
| Textos de la interfaz, rótulos, etiquetas | Castellano, con tildes | Incluye la etiqueta visible de todo valor de conjunto cerrado |
| Mensajes de commit y de registro técnico | Castellano | |
| Identificadores documentales (`CU-XX`, `RN-XX`, `BT-XX`, `ADR-XX`) | Se mantienen | No son identificadores de código: no los lee ningún compilador |

**La regla que las une:** si lo lee una persona, castellano; si lo lee una herramienta, inglés. La frontera es esa, y las tres decisiones de §5 son exactamente los lugares donde algo lo leen las dos.

### 4.1 Citas, reportes de fuente ajena y uso propio

**Un identificador escrito en un documento puede estar haciendo cinco cosas distintas, y el renombre las trata distinto.** Sin esta regla, un tramo que renombra «todas las ocurrencias» **corrige unas y falsifica otras**, y la diferencia no se ve en un recuento.

| Forma | Qué es | Qué hace el renombre | Cómo se reconoce |
| --- | --- | --- | --- |
| **Cita textual** | Palabras de otra fuente reproducidas literalmente, entre comillas angulares | **No se toca nunca.** Cambiar un nombre adentro de una cita la convierte en una cita falsa: la fuente no dijo eso | Está entre `«` y `»`, o entre comillas, y remite a un documento y a un parágrafo |
| **Reporte de fuente** | Una afirmación propia sobre **cómo nombra otra fuente**: «el intake §17.3.P.4 nombra las cinco tablas en mayúsculas» | Se renombra **si y sólo si esa fuente se renombra**, y en **el mismo tramo** que la fuente. Si la fuente no se renombra, el reporte conserva el nombre ajeno | Tiene un sujeto que no es este producto —el intake, `RT §7.1`, el programa de la Actividad 1— y un verbo de decir: *nombra*, *declara*, *emite*, *transcribe* |
| **Registro histórico** | Un acta de algo que ya pasó: fila de control de cambios, hallazgo de auditoría, ronda de corrección, retiro de un código | **No se toca nunca.** Es el registro de un hecho con su nombre de entonces; renombrarlo borra la trazabilidad que el acta existe para dar | Vive en la sección de control de cambios de su documento, o cita una ronda, un hallazgo o una versión |
| **Otro concepto con el mismo nombre** | Un token **idéntico** al identificador que nombra **otra cosa**, ajena a la población del tramo: la raíz `visor/` del proyecto de código frente a la carpeta `visor/` de la capa 3 del *bundle*, que son dos cosas distintas escritas igual | **No se toca nunca.** No es el identificador: es un homónimo, y renombrarlo cambia una cosa distinta de la que el tramo vino a cambiar. Es *uso propio* —de otro concepto—, y por eso el recuento no lo distingue solo | El concepto que nombra **no tiene fila en la población del tramo**: la tiene en otra sección del glosario, o está declarado intocable (§1, §5.4, §6.11), o no es un identificador de este producto |
| **Uso propio** | El documento usa el identificador para nombrar la cosa | **Se renombra.** Es la población de §8 | Todo lo que no es ninguna de las cuatro anteriores |

**Cuánto es esto, contado.** El corpus tiene **117 citas entrecomilladas que traen adentro un identificador del glosario, con 134 ocurrencias, repartidas en 58 documentos**, y **441 filas de control de cambios que nombran uno**. No es un caso de borde: es una población comparable a la de un tramo entero.

**Y el quinto caso es el que más pesó en el primer tramo que lo midió, que es por lo que la 1.3 le da fila.** §2.1 ya lo reconocía —«prosa marcada como código, una cita de fuente ajena (§4.1), u otro concepto con el mismo nombre»— y `V-4` no lo admitía por escrito, de modo que el tramo tenía que clasificar contra cuatro motivos una población que no entraba en ninguno. En `R-1b` sobre [`Plan-Etapa-A.md`](Plan-Etapa-A.md), medido con el instrumento de §2.1 sobre el estado previo a editar: **22 ocurrencias candidatas**, **5 renombradas** y **17 no renombradas**, y **10 de esas 17 son este caso** —**7 de la raíz `visor/`** del proyecto de código, que §1 declara fuera de discusión y que §6.11 distingue expresamente de la carpeta `visor/` de la capa 3, y **3 del guion `build-visor.sh`**—. Las otras 7 se reparten en **4 de registro histórico** —las filas de control de cambios de `R-1` y de la 1.0—, **2 de reporte de fuente** —`geometriafactory-visor`, la identidad npm del proyecto que declara el intake §13— y **1 de prosa** —la palabra «visor» dentro de un bloque cercado que rotula una capa—. Sin el quinto motivo, el motivo mayoritario del tramo no tenía dónde escribirse.

**El caso que lo vuelve urgente, y es el tramo siguiente.** `R-2` toca **el intake**, que es *la fuente* de la que el resto del corpus reporta. Ahí las tres formas conviven en el mismo párrafo, y la regla decide:

1. Cuando el intake **se renombra**, todo reporte que dice «el intake nombra `IRepositorioTrabajos`» pasa a decir `IWorkRepository` **en el mismo tramo**, porque si no queda reportando algo que la fuente ya no dice.
2. Cuando la fuente es **ajena al producto** —el JSON que emite el programa de la Actividad 1 (§5.4), o `RT §7.1` transcrito por el intake §17.3.P.4— el reporte **conserva el nombre ajeno**, aunque el identificador propio se renombre. `Modelo-Datos-Logico.md` §7 puede decir que la tabla `Account` corresponde a lo que `RT §7.1` llama `ALUMNO`: ésa es la forma correcta, y es la que §6.9 ya usa.
3. Cuando la fuente **cita a su vez** un nombre retirado, el reporte no se traduce: `RN-05` conserva `TRANSICION_DE_TRABAJO_NO_ADMITIDA` en su fila de control de cambios de la 1.1, porque esa fila **declara el retiro** de ese código y renombrarla haría ilegible el hallazgo `P2-02` que la produjo.

**La consecuencia operativa está en `V-4` de §7 y en §8.2**: la lista de ocurrencias que **no** se renombran —las cuatro primeras formas de la tabla, más la prosa de §4— se escribe **antes de editar**, y el cuadre se hace contra ella.

## 5. Zona de frontera · Las tres decisiones tomadas

Las tres tienen la misma forma: **un identificador que también es dato**. Se persiste, o viaja en una respuesta, o lo invoca otro extremo. Cambiarlo no es renombrar: es cambiar un contrato.

**La versión 1.0 las elevó como propuestas. El Product Owner las decidió el 2026-08-12, y esta versión las registra como decisiones tomadas**, cada una con su fecha, su fundamento y su costo contado. Lo que sigue ya no es una consulta: es la norma.

| Frontera | Decisión | Fecha | Alcance contado | ¿Cambia un contrato? |
| --- | --- | --- | --- | --- |
| `F-01` Fachada del visor | **`F-01a`: las seis funciones van a inglés** | 2026-08-12 | 53 documentos · 621 ocurrencias candidatas | No. Los nombres nunca estuvieron fijados |
| `F-02` Conjuntos cerrados | **`F-02a`: identificador en inglés, etiqueta en castellano** | 2026-08-12 | 399 documentos · 4461 ocurrencias candidatas, y 934 más de «pendiente» en prosa que **no** se tocan | Sí en la forma persistida, **con costo cero hoy**: no hay base poblada |
| `F-03` Códigos de condición | **Los 101 van a inglés: los 80 internos y los 21 de contrato** | 2026-08-12 | 330 documentos · 2847 ocurrencias candidatas | **Sí. Es un cambio de contrato y así se declara** |

### 5.1 `F-01` Las seis funciones de la fachada del visor · **decidida**

**Qué son.** `inicializar`, `cargarJson`, `seleccionarPieza`, `redimensionar`, `destruir`, `establecerMovimiento`. Son la superficie pública del *bundle* de TypeScript, expuesta como biblioteca en `window`, y **Blazor las invoca por interoperabilidad** contra `IJSRuntime`. Son el punto de extensión principal del producto: el sample `S-1` las ejerce enteras sin backend, que es lo que hace reemplazable al motor 3D.

**Decisión del Product Owner, 2026-08-12: `F-01a`. Las seis pasan a inglés.** La correspondencia está en §6.6 y es la única fuente de esos seis nombres.

**El fundamento decisivo, verificado en la fuente.** El intake §17.7.P.3 encabeza su tabla así: «Contrato de la fachada, **con los nombres definitivos a fijar en la etapa que la implementa** (RT §8.4)». **Los nombres de la fachada nunca estuvieron fijados**: fijarlos es, literalmente, lo que la etapa que implementa el visor debe hacer, y esta decisión es ese acto. [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.2 punto 5 afirma que «están fijadas por el intake §17.7.P.3», y eso contradice la letra de la fuente que cita; esa afirmación queda corregida por esta norma.

Los otros dos términos del fundamento, y son los que hacen que el momento sea el más barato que va a haber:

1. **El visor no existe como código.** La etapa `a` crea el proyecto y un *bundle* «vacío pero real» (intake §15), con la fachada declarada y sin lógica de dibujo. No hay una sola línea que renombrar.
2. **Su único consumidor está en la misma solución.** El *bundle* sólo lo invoca `GeometriaFactory-Web`, que se compila y se despliega junto con él. No hay consumidor externo al que avisarle.

**Costo contado, y es el que se paga.** **53 documentos** las nombran; **21** declaran las seis; **621 ocurrencias** en total, remedidas en la 1.2 —la 1.1 declaraba 52 y 593—. Los documentos que fijan su contrato son [`../Proyectos/GeometriaFactory-Visor/02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../Proyectos/GeometriaFactory-Visor/02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md), su [`ADR-02`](../Proyectos/GeometriaFactory-Visor/05-Arquitectura-Tecnica/Adrs/ADR-02-Superficie-De-Seis-Funciones-Planas.md), el intake §17.7.P.3, y las categorías 02, 03, 05 y 10 de `GeometriaFactory-Visor` y `GeometriaFactory-Web`. Es un renombre mecánico, verificable con recuento en las dos direcciones, **sin ninguna decisión por documento**.

**La alternativa que se descartó** era `F-01b` —quedan en castellano, declarado como apartamiento—: dejaba la única superficie pública del producto en un idioma distinto del de todo el resto del código, y obligaba a `GeometriaFactory.Web.Integration` a traducir en el punto de invocación, que es el defecto que `RI-06` de [`Vista-Producto.md`](Vista-Producto.md) §7 declara con historia en este producto.

### 5.2 `F-02` Los valores de los conjuntos cerrados · **decidida**

**Qué son.** Cuatro conjuntos: papel (`Alumno`, `Administrador`), estado de cuenta (`Pendiente`, `Habilitado`, `Bloqueado`), estado del trabajo (`Borrador`, `Pendiente`, `Finalizado`, `Rechazado`) y especie de observación (`Advertencia`, `Error de validación`). **Diez identificadores distintos.**

**Decisión del Product Owner, 2026-08-12: `F-02a`. Identificador en inglés, etiqueta en castellano, y la traducción en un solo lugar.** El código dice `Pending`; la pantalla dice «Pendiente». La correspondencia completa está en §6.7.

**Por qué era frontera, y no es una sutileza.** Los valores se persisten y se serializan **por su nombre, nunca por su posición** — [`../Proyectos/GeometriaFactory-Api/05-Arquitectura-Tecnica/Contratos-REST.md`](../Proyectos/GeometriaFactory-Api/05-Arquitectura-Tecnica/Contratos-REST.md) §2.2, y [`../Proyectos/GeometriaFactory-Infrastructure/05-Arquitectura-Tecnica/Modelo-Datos-Logico.md`](../Proyectos/GeometriaFactory-Infrastructure/05-Arquitectura-Tecnica/Modelo-Datos-Logico.md) §2.1 y §2.2, que los guarda como texto. El identificador **es** el dato guardado y el dato transmitido. Y además el alumno lo ve traducido en pantalla.

**El fundamento decisivo: deshace la colisión ya declarada de `Pendiente`.** Hoy `Pendiente` nombra **dos cosas distintas** —una cuenta que espera habilitación y un trabajo que espera revisión— y el corpus ya tuvo que pagar por eso: [`../Proyectos/GeometriaFactory-Domain/02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md`](../Proyectos/GeometriaFactory-Domain/02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md) §2.1 declaró obligatoria una forma calificada —«marca de cambio de contraseña pendiente»— justamente porque «`Pendiente` a secas nombra un estado de cuenta y un estado de trabajo». En inglés son dos palabras: la cuenta está `Pending` y el trabajo está `Submitted` —se envió y espera revisión—. **La forma calificada obligatoria deja de hacer falta**, y §6.7 declara cuál de los dos nombres va en cada contexto.

**Costo contado, y es alto.** **399 documentos, 4461 ocurrencias candidatas**: es la clase más grande del corpus. Sólo `Pendiente` son **351 documentos y 1983 ocurrencias**, de las cuales **956 viven en los 58 documentos que traen los dos contextos a la vez**; y hay **934 ocurrencias más de «pendiente» en prosa, en 254 documentos**, que §4 deja donde están. Las cifras de la 1.1 eran 396 y 4259, y 349 y 1919.

**Y el costo que no es documental, con su ventana.** El identificador es el dato persistido, de modo que una base ya poblada exigiría una transformación de esquema. **Hoy no hay ninguna base poblada** —`GeometriaFactory-Infrastructure` no está construido—, así que ese costo es **cero si se ejecuta ahora**, y deja de serlo el día que exista la primera fila. Es la razón por la que la decisión se toma hoy y no después.

**La consecuencia que hay que declarar: obliga a que exista un traductor de etiqueta.** Ya existe: [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.3 declara un componente `Servicios` en `GeometriaFactory-Web` que incluye «traductor», y el catálogo de condiciones ya establece que el texto para la persona lo compone quien expone. La norma lo aprovecha; no lo inventa. El control `V-3` de §7 lo verifica.

**Las dos alternativas que se descartaron.** `F-02b` —quedan en castellano y el identificador es también la etiqueta— dejaba la colisión de `Pendiente` para siempre, con la forma calificada como parche permanente, y un tipo `WorkStatus` con miembros `Borrador` y `Finalizado`, que es media traducción. `F-02c` —identificador en inglés y valor serializado en castellano, con anotación por miembro— introducía dos nombres para el mismo valor y una configuración que los dos extremos tienen que compartir, que es exactamente lo que `Contratos-REST.md` §2.2 eligió evitar al fijar «nombres de campo tal como los declara el tipo, sin transformación de estilo».

### 5.3 `F-03` Los códigos de condición y de contrato · **decidida, y es cambio de contrato**

**Qué son.** **101 identificadores distintos** en seis catálogos: los internos de `GeometriaFactory-Domain`, `GeometriaFactory-Application`, `GeometriaFactory-Infrastructure` y `GeometriaFactory-Visor`, y los **21 con prefijo `CONTRATO_`** de la frontera HTTP, declarados por `GeometriaFactory-Contracts` y traducidos por `GeometriaFactory-Api`. El desglose exacto, recontado para esta versión, está en §6.8.

**Decisión del Product Owner, 2026-08-12: TODOS van a inglés. Los ochenta internos y los veintiuno de contrato.** El Product Owner eligió **la consistencia total sobre la conservación del contrato**. La correspondencia completa —las 101 filas— está en §6.8.

**El fundamento, y son dos hechos verificables.**

1. **El producto no emitió una sola respuesta todavía.** Ningún cliente recibió jamás uno de estos códigos, porque no hay servicio corriendo: `GeometriaFactory-Api` no está construido. Un contrato que nunca se ejerció se cambia sin romper nada.
2. **Los dos consumidores compilan juntos.** El contrato lo consumen `GeometriaFactory-Web` y `GeometriaFactory-Api`, **de esta misma solución, compilados contra el mismo ensamblado** — es la razón por la que el intake §17.4.P.12 descartó generar clientes desde una descripción formal. No hay ningún consumidor externo al que el cambio le llegue sin aviso.

> **Declaración explícita: `F-03` es un cambio de contrato.**
>
> Los `CONTRATO_*` no son símbolos internos: **viajan dentro de las respuestas** y los cita `GeometriaFactory-Web` para decidir qué le muestra a la persona. Renombrarlos cambia el conjunto cerrado de valores que la frontera HTTP transporta, que es la definición misma de cambio incompatible según `DXC-03` del catálogo de `GeometriaFactory-Contracts`. Se declara así, y no como renombre, para que quede registrado que **la regla operativa `RT-06` aplica**: los dos extremos se cambian juntos y se despliegan juntos. Que hoy el cambio sea gratuito no lo convierte en otra cosa; lo convierte en un cambio de contrato barato.

**Lo que la decisión hace con el prefijo.** **Se elimina el prefijo `CONTRATO_`.** No aporta información que el tipo no dé ya: los códigos del contrato viven en el conjunto cerrado que declara `GeometriaFactory-Contracts` y ningún otro catálogo comparte ese tipo. La consecuencia —cuatro pares que quedan con el mismo nombre en dos catálogos distintos— está contada y declarada en §6.9, y **no es una colisión**: es un concepto con un nombre, que es lo que §6.1 pide.

**La convención de forma, exacta.** Los códigos **conservan su forma de constante y sólo cambian de idioma**: `SCREAMING_SNAKE_CASE`, palabras en inglés separadas por `_`, sin artículos, sin preposiciones sueltas y sin prefijo de proyecto. `DATO_OBLIGATORIO_AUSENTE` pasa a `REQUIRED_FIELD_MISSING`; `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` pasa a `STATE_FORBIDS_UPDATE`. La forma no se toca porque no está en discusión: lo que estaba en discusión era el idioma.

**Costo contado.** **330 documentos, 2847 ocurrencias candidatas**, remedidas en la 1.2 —la 1.1 declaraba 334 y 2911, contando por forma y no contra la lista de los 101; §2.2 lo detalla—. Los `CONTRATO_*` solos: **220 documentos, 1201 ocurrencias**, cifra que la remedición reproduce exacta. Es el renombre más caro de los tres y el más mecánico: no hay ninguna decisión por documento, sólo correspondencia uno a uno contra §6.8, y los seis catálogos son la fuente única contra la que se verifica. La verificación en las dos direcciones que `ADR-06` de `GeometriaFactory-Application` ya exige —comparar los códigos emitidos contra el catálogo— **sirve tal cual** después del renombre.

**Las dos alternativas que se descartaron.** `F-03b` —quedan como están, declarado como apartamiento— dejaba el producto con una regla partida —identificadores en inglés salvo estos 101— que hay que explicar cada vez, y la explicación es histórica y no técnica. `F-03c` —sólo los 80 internos, y los 21 `CONTRATO_*` quedan— era la salida intermedia, y la 1.0 ya había medido por qué compra poco: reduce el alcance de 330 documentos a 314, que **no es una reducción real**, porque los mismos documentos citan las dos familias; y obliga a `GeometriaFactory-Api` a traducir de un catálogo inglés a uno castellano en la frontera, que es una tabla más que mantener.

**El hallazgo lateral de la 1.0 sigue abierto y se confirma.** Dos identificadores `CONTRATO_*` —`CONTRATO_CAMBIO_DE_CONTRASENA_PENDIENTE` y `CONTRATO_RESETEO_NO_ADMITIDO`— aparecen en **tres casos de uso de `GeometriaFactory-Web`** —`CU-02`, `CU-03` y `CU-04`— y **no figuran en el catálogo de `GeometriaFactory-Contracts`, ni en el de `GeometriaFactory-Api`, ni en `Contratos-REST.md`**. Es un defecto de fondo preexistente y ajeno al idioma: el conjunto real citado por el corpus es **23** y el conjunto declarado es **21**. §6.8 les da fila con la marca `huérfano` y **no los traduce**, porque §6.1 exige que el concepto exista antes que el nombre: quien los declare formalmente los agrega, y recién ahí entran al renombre.

### 5.4 Lo que no es frontera y no se discute: el dato del alumno

**El JSON que el alumno pega no se toca, y sus claves no son un identificador de este producto.** `Tipo`, `Tapas`, `Bases`, `Radio`, `Largo`, `Ancho`, `Area`, `Volumen`, y los valores `Cilindro`, `Cubo`, `Ortoedro`, `Rectangulo`, `Cuadrado`, `Circulo`, `RectanguloDesarrollado`.

Los emite el programa de escritorio de la Actividad 1, que **no forma parte de este producto**: el intake §17.1.P.10 lo dice con todas las letras —«eso es de la Actividad 1, que es el emisor del dato y no forma parte de este producto»— y la decisión `D-1` fija que el JSON se acepta **tal como lo emite su programa**, con sus comas finales y su clave `Tapas`. **39 documentos, 358 ocurrencias** para los siete tipos de figura —la 1.1 declaraba 30 y 201—.

**Consecuencia para la norma:** el tipo de C# que lee ese JSON se llama en inglés y **mapea explícitamente** al nombre castellano de la clave. La traducción vive en el mapeo, declarada, en un solo lugar — que es precisamente donde el producto ya decidió que viven las trampas del formato. §6.4 trae las quince filas con su marca `no se renombra`.

## 6. El glosario de correspondencia

### 6.1 La regla del glosario

> **Si un concepto del dominio no está en el glosario —§6.3 a §6.8, §6.10, §6.11, §6.12, §6.13, §6.14 y §6.15—, no se traduce por criterio propio: se agrega primero a la tabla que le corresponde y recién después se escribe el identificador.**

**El rango son las doce tablas, y desde la 1.4 se escribe entero.** La novena la agrega la 1.6: **§6.12**, con los identificadores que la etapa `b` de `GeometriaFactory-Web` necesitó y que ninguna de las ocho anteriores contaba. **La décima la agrega la 1.7: §6.13**, con los que la etapa `c` escribió al construir la primera rebanada vertical del producto. **La undécima la agrega la 1.8: §6.14**, con los que exigió construir la marca de sesión del navegador que `Web ADR-03` §2 declaraba y que la etapa `c` no había construido. **La duodécima la agrega la 1.12: §6.15**, con los que la etapa `d` escribió al construir el ciclo de vida de la cuenta de alumno. Hasta la 1.3 la regla decía «§6.3 a §6.8» y dejaba afuera **§6.10** —los 16 subsegmentos de espacio de nombres— y **§6.11** —las cinco superficies derivadas—, que son tablas del mismo glosario y con la misma forma de fila. El corolario de §6.11 manda agregar ahí toda carpeta que no nombre un concepto listado, de modo que la regla ordenaba agregar filas a una tabla que ella misma no nombraba. §6.9 **no** entra en el rango porque no agrega filas: declara las dos unificaciones y los cuatro homónimos de nombres que ya están en §6.4 y en §6.8.

Y sus cuatro corolarios, que son lo que hace que la regla sirva:

1. **Un concepto, un nombre.** No se admiten dos traducciones del mismo concepto en dos proyectos de código. Si el corpus tiene hoy dos identificadores castellanos para la misma cosa, **el glosario lo declara y los unifica en un solo nombre inglés** — §6.9 trae los dos casos que la medición encontró.
2. **Un nombre, un concepto.** Ningún nombre inglés cubre dos conceptos distintos. La colisión de `Pendiente` se resuelve con **dos nombres**, `Pending` y `Submitted`, y §6.7 declara cuál va en cada contexto. La única excepción admitida es la de §6.9: **el mismo concepto** declarado por dos catálogos distintos lleva el mismo nombre en los dos, y el tipo que lo contiene los separa.
3. **Los códigos conservan su forma y cambian de idioma.** `SCREAMING_SNAKE_CASE`, palabras inglesas separadas por `_`, sin artículos ni preposiciones sueltas, sin prefijo de proyecto y **sin el prefijo `CONTRATO_`**, que §5.3 elimina.
4. **Agregar una fila es un acto declarado**, con su entrada en el control de cambios de §9. Quien traduce sin agregar produce un identificador que nadie puede verificar, y ése es el defecto que esta norma existe para impedir.

**Y la regla de forma de cada fila:** castellano, inglés, clase y **dónde está declarado el concepto**. La cuarta columna no es decorativa: es lo que permite que `V-1` de §7 verifique la fila contra su fuente en lugar de creerle.

**La tabla es la fuente única de la correspondencia.** Ningún otro documento la redeclara; los demás la citan.

### 6.2 Cobertura del glosario, contada

La versión 1.0 emitió **42 conceptos** y dejó fuera los 101 códigos, porque su correspondencia dependía de una decisión que todavía no estaba tomada. Tomada `F-03`, **esta versión cubre las seis clases enteras**.

| Clase | Identificadores distintos | Filas en esta versión | Sección |
| --- | --- | --- | --- |
| 1. Interfaces y puertos | 5 | 5 | §6.3 |
| 2. Entidades y tipos | 31 | 31 | §6.4 |
| 3. Miembros y propiedades | 2 | 2 | §6.5 |
| 4. Funciones de la fachada del visor | 6 | 6 | §6.6 |
| 5. Valores de conjuntos cerrados | 10 | 10 | §6.7 |
| 6. Códigos de condición y de contrato | 101 | 101 | §6.8 |
| **Total de las seis clases** | **155** | **155** | — |
| Superficies derivadas por debajo del nivel de espacio de nombres | 5 | 5 | §6.11 |
| Agregados por la etapa `a` (1.5), **fuera de los 155** | 5 | 5 | §6.4 y §6.5 |
| Agregados por la etapa `b` (1.6), **fuera de los 155** | 214 | 214 | §6.12 |
| Agregados por la etapa `c` (1.7) más las tres que la 1.8 sumó, **fuera de los 155** | 109 | 109 | §6.13 |
| Agregados por la sesión por marca de navegador (1.8), por el guardián de sesión (1.9), por el arreglo del cambio forzado (1.10) y por la guardia de arranque de la clave de firma (1.11), **fuera de los 155** | 39 | 39 | §6.14 |
| Agregados por la etapa `d` (1.12), **fuera de los 155** | 36 | 36 | §6.15 |

**Las cinco filas agregadas por la etapa `a` tampoco entran en los 155, y por el mismo motivo que las de §6.11:** son conceptos que **no existían** cuando se contaron las seis clases —el cuerpo de la respuesta del punto de salud y el nombre propio del *bundle* en `window`—, y entran por el corolario 4 de §6.1, que es lo que esta norma manda hacer cuando aparece un concepto sin fila. **Los recuentos de las seis clases no cambian**: 155 sigue siendo 155, y el control `V-1` cuadra contra las ocho tablas más estas cinco filas, que llevan su marca. **Lo mismo vale para las 214 que agrega la etapa `b` en §6.12**, que desde la 1.6 es la novena tabla del rango, **para las 109 que agrega la etapa `c` en §6.13** —106 propias más las tres que la 1.8 sumó—, que desde la 1.7 es la décima, **para las 39 que agrega §6.14**, que desde la 1.8 es la undécima —27 las trajo la marca de sesión, 5 más el guardián de sesión de la 1.9, 5 más el arreglo del cambio forzado de la 1.10 y 2 más la guardia de arranque de la clave de firma de la 1.11— **y para las 36 que agrega §6.15**, que desde la 1.12 es la duodécima —16 tipos y 20 miembros del ciclo de vida de la cuenta de alumno—. Y la etapa `c` deja además una constancia que vale la pena leer al revés: **de los veinte códigos de condición que escribió, cero necesitaron fila nueva**, porque los veinte ya estaban en §6.8 con su nombre inglés fijado por `F-03`. El glosario hizo exactamente lo que §6.1 promete. **La etapa `d` reproduce la constancia sobre una población mayor**: de los **quince** códigos que escribió —siete del dominio, tres propios de la aplicación, uno de infraestructura y cuatro del contrato— **cero necesitaron fila nueva**, y entre ellos están los dos casos difíciles que `F-03` había dejado resueltos de antemano: la **unificación** de §6.9, que le da a `CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA` y a `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` el mismo nombre inglés, y los **dos identificadores retirados por RN-16** de §6.8.5, que la etapa `d` **no recicló**.

**Las cinco filas de §6.11 no entran en los 155** y por eso van aparte: ninguna de las seis clases de §2.2 las contaba, porque las seis clases cuentan *identificadores* y éstas son *superficies* —carpetas y nombres de archivo— que ninguna regla alcanzaba. Se agregan por el corolario 4 de §6.1, que es lo que esta norma pide hacer cuando aparece un concepto sin fila.

**El recuento se rehizo para esta versión y reproduce el de §2.2.** Se contó sobre los seis catálogos, que son la fuente, y no sobre el corpus entero: un catálogo declara sus códigos en la primera celda de las filas de su §3, y ésa es la única forma de distinguir un código **declarado** de un código **citado**. El desglose de la clase 6, que es donde el número podía no cerrar:

| Origen | Distintos | Nota |
| --- | --- | --- |
| `GeometriaFactory-Domain`, catálogo vivo | 42 | Su §6.1 lo declara: 50 filas menos 8 repeticiones |
| `GeometriaFactory-Application`, catálogo vivo | 36 | Su §7.1 lo declara: 37 filas de tabla con una excedente |
| `GeometriaFactory-Infrastructure`, catálogo vivo | 17 | Su §7.1 lo declara: 19 filas menos 2 reapariciones |
| `GeometriaFactory-Visor`, catálogo vivo | 7 | Las siete condiciones de la fachada |
| **Menos el solapamiento entre catálogos** | **−26** | 24 códigos que Domain y Application declaran los dos, y 2 que Infrastructure comparte con Application (`CORREO_YA_REGISTRADO`, `INTERPRETACION_NO_DISPONIBLE`) |
| **Internos vivos, distintos** | **76** | |
| Internos **retirados** que siguen apareciendo en el corpus | 4 | Los cinco que declara `Domain` §6.1, menos `CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA`, que sigue **vivo en el catálogo de `Application`** y ya está contado entre los 76 — ver §6.9 |
| **Internos, total** | **80** | 76 + 4 |
| `CONTRATO_*` de `GeometriaFactory-Contracts` | 21 | 17 códigos de error vivos + `CONTRATO_LISTADO_VACIO`, que es señal y no error + 3 retirados. La cifra de «diecisiete vivos sobre veinte identificadores emitidos» del intake §17.4.P.3 cuenta **códigos de error**; ésta cuenta **identificadores**, e incluye la señal |
| **Clase 6, total** | **101** | 80 + 21 |

**Lo que quedó fuera del glosario, con su motivo, y son dos cosas contadas.** Primero, los **dos `CONTRATO_*` huérfanos** de §5.3: tienen fila en §6.8 con la marca `huérfano` y **sin nombre inglés**, porque §6.1 no permite traducir un concepto que ningún catálogo declara. Segundo, los **siete tipos de figura del dato del alumno** de §5.4: cuentan dentro de los 31 de la clase 2 y tienen fila en §6.4, pero llevan la marca `no se renombra`, porque no son identificadores de este producto; lo que el glosario declara ahí es el miembro inglés del tipo de C# que los lee y el mapeo explícito hacia el literal castellano. Las **ocho claves del JSON** —`Tipo`, `Tapas`, `Bases`, `Radio`, `Largo`, `Ancho`, `Area`, `Volumen`— van con ellos y **no entran en el recuento de 155**, por el mismo motivo.

### 6.3 Clase 1 · Interfaces y puertos (5)

| Castellano | Inglés | Clase | Dónde está declarado el concepto |
| --- | --- | --- | --- |
| `IRepositorioTrabajos` | `IWorkRepository` | Puerto, declarado | Intake §13, §14, §17.2.P.1 |
| `IValidadorFiguras` | `IFigureValidator` | Puerto, declarado | Intake §17.2.P.1 |
| `IRelojDelSistema` | `ISystemClock` | Puerto, declarado | Intake §17.2.P.11 punto 3 |
| `IRepositorioCuentas` | `IAccountRepository` | Puerto, propuesto (`P-4a`) | [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.5; `Application ADR-02` §2; [`../Handoff-Checkout.md`](../Handoff-Checkout.md) §6.2 `A-1` |
| `IRepositorioAlumnos` | `IStudentRepository` | Puerto, alternativa (`P-4b`) | [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.5, sólo si `D-03` se resuelve por `P-3b` |

### 6.4 Clase 2 · Entidades y tipos (31)

**Las cinco entidades del modelo de dominio.**

| Castellano | Inglés | Clase | Dónde está declarado el concepto |
| --- | --- | --- | --- |
| `Cuenta` | `Account` | Entidad | `Definicion-Modelo-De-Dominio.md` §2.1; `Modelo-Conceptual.md` §3.1 y §3.2 |
| `Trabajo` | `Work` | Entidad | `Definicion-Modelo-De-Dominio.md` §2.2 |
| `Pieza` | `Piece` | Entidad | `Definicion-Modelo-De-Dominio.md` §2.3 |
| `Componente` | `Component` | Entidad | `Definicion-Modelo-De-Dominio.md` §2.4. Es término del glosario del cliente (intake §12) y **no se renombra en el texto** |
| `Observacion` | `Observation` | Entidad | `Definicion-Modelo-De-Dominio.md` §2.5 |

**Las cinco tablas del modelo de datos**, que el intake §17.3.P.4 nombra en mayúsculas transcribiendo `RT §7.1`. **Nombran las mismas cinco cosas que las entidades**, y el downstream ya las escribe así: `Modelo-Datos-Logico.md` §2 titula sus cinco tablas `Cuenta`, `Trabajo`, `Pieza`, `Componente` y `Observación`, y su §7 declara la correspondencia una a una con las entidades conceptuales. El glosario **unifica** (§6.9): un concepto, un nombre.

| Castellano | Inglés | Clase | Dónde está declarado el concepto |
| --- | --- | --- | --- |
| `ALUMNO` | `Account` | Tabla, **unificada con la entidad `Cuenta`** | Intake §17.3.P.4 (RT §7.1); `Modelo-Datos-Logico.md` §2.1 y §7, que ya la llama `Cuenta` |
| `TRABAJO` | `Work` | Tabla, **unificada con la entidad `Trabajo`** | Intake §17.3.P.4; `Modelo-Datos-Logico.md` §2.2 y §7 |
| `PIEZA` | `Piece` | Tabla, **unificada con la entidad `Pieza`** | Intake §17.3.P.4; `Modelo-Datos-Logico.md` §2.3 y §7 |
| `COMPONENTE` | `Component` | Tabla, **unificada con la entidad `Componente`** | Intake §17.3.P.4; `Modelo-Datos-Logico.md` §2.4 y §7 |
| `OBSERVACION` | `Observation` | Tabla, **unificada con la entidad `Observacion`** | Intake §17.3.P.4; `Modelo-Datos-Logico.md` §2.5 y §7 |

**Los siete tipos de figura del dato del alumno.** El literal castellano **no se renombra**: es el valor que emite el programa de la Actividad 1 y el producto lo acepta tal cual (§5.4). Lo que el glosario fija es el miembro inglés del tipo que lo lee y **el mapeo explícito** hacia el literal.

| Castellano (literal del JSON) | Inglés (miembro de `FigureType`) | Clase | Dónde está declarado el concepto |
| --- | --- | --- | --- |
| `Cilindro` | `Cylinder` ⟶ mapea a `"Cilindro"`, **no se renombra el literal** | Valor del dato del alumno | Intake §20.E-7; §17.1.P.10 |
| `Cubo` | `Cube` ⟶ mapea a `"Cubo"`, **no se renombra el literal** | Valor del dato del alumno | Intake §20.E-3, §20.E-4 |
| `Ortoedro` | `Box` ⟶ mapea a `"Ortoedro"`, **no se renombra el literal** | Valor del dato del alumno | Intake §20.E-2 |
| `Rectangulo` | `Rectangle` ⟶ mapea a `"Rectangulo"`, **no se renombra el literal** | Valor del dato del alumno | Intake §20.E-4, §20.E-7 |
| `Cuadrado` | `Square` ⟶ mapea a `"Cuadrado"`, **no se renombra el literal** | Valor del dato del alumno | Intake §20.E-3, §20.E-7 |
| `Circulo` | `Circle` ⟶ mapea a `"Circulo"`, **no se renombra el literal** | Valor del dato del alumno | Intake §20.E-7 |
| `RectanguloDesarrollado` | `UnfoldedRectangle` ⟶ mapea al literal, **no se renombra** | Valor del dato del alumno, **no dibujable** | Intake §17.1.P.10. Es el séptimo tipo; los **seis dibujables** son los anteriores (§20.E-7) |

**Las ocho claves del JSON del alumno** —`Tipo`, `Tapas`, `Bases`, `Radio`, `Largo`, `Ancho`, `Area`, `Volumen`— siguen la misma regla y **no entran en el recuento de 155**: el tipo de C# las lee con un mapeo explícito (`Type`, `Caps`, `Bases`, `Radius`, `Length`, `Width`, `Area`, `Volume`) y el literal del JSON no se toca. `Tapas` y `Bases` son sinónimos aceptados por decisión `T1`, y esa equivalencia vive en el mapeo.

**Los catorce tipos y adaptadores propuestos**, todos de [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.6 y §1.7. Viven en **un solo documento** y renombrarlos cuesta una edición (§2.3).

| Castellano | Inglés | Clase | Dónde está declarado el concepto |
| --- | --- | --- | --- |
| `Papel` | `Role` | Conjunto cerrado (tipo) | [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.7; `Definicion-Modelo-De-Dominio.md` §2.1 |
| `EstadoDeCuenta` | `AccountStatus` | Conjunto cerrado (tipo) | [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.7; `Modelo-Datos-Logico.md` §2.1 |
| `EstadoDeTrabajo` | `WorkStatus` | Conjunto cerrado (tipo) | [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.7; `Definicion-Modelo-De-Dominio.md` §5.2 |
| `EspecieDeObservacion` | `ObservationKind` | Conjunto cerrado (tipo) | [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.7; `Definicion-Modelo-De-Dominio.md` §2.5 |
| `RepositorioCuentasEfCore` | `EfCoreAccountRepository` | Adaptador | [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.6; criterio de `Infrastructure ADR-03` §6 punto 4 |
| `RepositorioTrabajosEfCore` | `EfCoreWorkRepository` | Adaptador | [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.6 |
| `ValidadorFigurasLocal` | `LocalFigureValidator` | Adaptador | [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.6; intake §17.3.P.3 |
| `RelojDelSistemaUtc` | `UtcSystemClock` | Adaptador | [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.6; `Modelo-Datos-Logico.md` §2.1, `RC-06` |
| `ContextoDeGeometriaFactory` | `GeometriaFactoryDbContext` | Contexto de persistencia | [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.7; intake §17.3.P.4 |
| `PreparacionDelAlmacen` | `StorePreparation` | Tipo de arranque | [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.7; `Infrastructure ADR-07` |
| `ComposicionDeRaiz` | `CompositionRoot` | Tipo de composición | [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.7; `Api ADR-06` |
| `ArranqueEnDosFases` | `TwoPhaseStartup` | Tipo de arranque | [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.7; `Api ADR-07` |
| `PuntoDeSalud` | `HealthEndpoint` | Punto de acceso | [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.7; `Contratos-REST.md` §3 |
| `ClienteDelServicioDeDatos` | `DataServiceClient` | Cliente del front | [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.7; `Web/05` §3.1 capa 3 |

**Seis identificadores más que [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.7 propone y que §2.2 no contó como distintos**, porque son derivados de nombres ya listados. Se les da fila igual, para que nadie los traduzca por criterio propio: `ConfiguracionDeCuenta` ⟶ `AccountConfiguration`, `ConfiguracionDeTrabajo` ⟶ `WorkConfiguration`, `ConfiguracionDePieza` ⟶ `PieceConfiguration`, `ConfiguracionDeComponente` ⟶ `ComponentConfiguration`, `ConfiguracionDeObservacion` ⟶ `ObservationConfiguration` —los cinco mapeos de `BT-05`— y `Estado`, el componente Blazor de la página de salud, ⟶ `Status`.

**Dos identificadores más, agregados por la etapa `a` el 2026-08-13** (corolario 4 de §6.1). **No entran en los 31 de la clase 2 ni en los 155**: son conceptos que ninguna de las seis clases contó porque **no existían** cuando se contaron. Se agregan **antes** de escribir el identificador, que es exactamente para lo que §6.1 existe.

| Castellano | Inglés | Clase | Dónde está declarado el concepto |
| --- | --- | --- | --- |
| `EstadoDelServicio` | `ServiceHealth` | Tipo de transferencia de `GeometriaFactory.Contracts.Service` | `US-29` de `GeometriaFactory-Api` §3; [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §7 `R-03`, que declara que **ninguna fuente le da tipo al cuerpo de la respuesta de salud**. El identificador se agrega acá; **qué datos lleva sigue siendo del punto de control** |
| `VisorDeGeometriaFactory` | `GeometriaFactoryViewer` | Nombre propio de la biblioteca que el *bundle* expone en `window` | Intake §17.7.P.1 y §17.7.P.3, que exigen **salida como biblioteca en `window` con un nombre propio, sin globales sueltas**, y **no dan el nombre**. Sigue a `VisorFiguras` ⟶ `FigureViewer` de §6.11: el inglés de «visor» en este producto es `viewer` |

### 6.5 Clase 3 · Miembros y propiedades (2)

Son los dos únicos miembros que el corpus nombra hoy con identificador propio. Las demás filas de esta tabla son **conceptos derivables de la especificación** que la 1.0 ya emitió y que se conservan, porque son los nombres que la etapa `c` va a escribir.

| Castellano | Inglés | Clase | Dónde está declarado el concepto |
| --- | --- | --- | --- |
| `HashContrasena` | `PasswordHash` | Miembro, **declarado** | Intake §17.1.P.5 |
| `JsonOriginal` | `OriginalJson` | Miembro, **declarado** | Intake §17.3.P.4; §13 |
| Marca de cambio de contraseña pendiente | `MustChangePassword` | Miembro, derivable | `Definicion-Modelo-De-Dominio.md` §2.1; RN-12, RN-16, INV-09 |
| Correo escrito | `Email` | Miembro, derivable | `Modelo-Datos-Logico.md` §2.1 |
| Correo normalizado | `NormalizedEmail` | Miembro, derivable | `Modelo-Datos-Logico.md` §2.1; `Infrastructure ADR-03` |
| Fecha declarada por el alumno | `DeclaredDate` | Miembro, derivable | `Modelo-Datos-Logico.md` §2.2 |
| Momento de creación | `CreatedAt` | Miembro, derivable | `Modelo-Datos-Logico.md` §2.2 |
| Momento de última modificación | `UpdatedAt` | Miembro, derivable | `Modelo-Datos-Logico.md` §2.2 |
| Comentario del administrador | `AdministratorComment` | Miembro, derivable | `Definicion-Modelo-De-Dominio.md` §2.2; intake §12 |
| Cantidad de figuras del conjunto raíz | `RootFigureCount` | Miembro, derivable | `Definicion-Modelo-De-Dominio.md` §2.2; RN-09 |
| Posición (identidad de la pieza) | `Position` | Miembro, derivable | `Definicion-Modelo-De-Dominio.md` §2.3; intake §17.1.P.11 punto 2 |
| Área declarada / derivada | `DeclaredArea` / `DerivedArea` | Miembro, derivable | `Definicion-Modelo-De-Dominio.md` §2.3; intake §17.1.P.11 punto 3 |
| Volumen declarado / derivado | `DeclaredVolume` / `DerivedVolume` | Miembro, derivable | `Definicion-Modelo-De-Dominio.md` §2.3 |

**Sólo las dos primeras cuentan en los 155**: son las únicas que el corpus declara hoy como identificador. Las once restantes son la parte del glosario que existe **antes** de que el identificador se escriba, que es exactamente para lo que §6.1 existe.

**Tres miembros más, agregados por la etapa `a` el 2026-08-13** (corolario 4 de §6.1). Son los de `ServiceHealth`, y **tampoco entran en los 155**. Los tres son **propuesta de la etapa `a`** en cuanto a *qué* se publica; lo que sí está declarado, y es lo que los acota, es lo que la respuesta **no puede llevar**: ninguna dirección de servicio interno, ninguna ruta del almacén y ninguna traza (`US-29` §3, tercer criterio).

| Castellano | Inglés | Clase | Dónde está declarado el concepto |
| --- | --- | --- | --- |
| Almacén preparado | `Ready` | Miembro de `ServiceHealth` | `Infrastructure ADR-07`; `Api ADR-07`; `QG-11`, que exige cero peticiones atendidas con la preparación incompleta |
| Versión del servicio | `Version` | Miembro de `ServiceHealth` | `US-29` §3, «datos reales del servidor propio» |
| Momento del servidor en tiempo universal coordinado | `ServerTimeUtc` | Miembro de `ServiceHealth` | `Modelo-Datos-Logico.md` §2.1 y `RC-06`, que fijan el tiempo universal coordinado como el momento del producto |

### 6.6 Clase 4 · Las seis funciones de la fachada (6)

Decididas por `F-01` el 2026-08-12. **Ésta es la fuente de los seis nombres definitivos** que el intake §17.7.P.3 dejó «a fijar en la etapa que la implementa».

| Castellano | Inglés | Clase | Dónde está declarado el concepto |
| --- | --- | --- | --- |
| `inicializar(elemento, opciones)` | `initialize(element, options)` | Función de fachada | Intake §17.7.P.3; `Definicion-Contrato-De-Fachada.md`; `Visor ADR-02` |
| `cargarJson(id, texto)` | `loadJson(id, text)` | Función de fachada | Intake §17.7.P.3; `Definicion-Contrato-De-Fachada.md` |
| `seleccionarPieza(id, indice)` | `selectPiece(id, index)` | Función de fachada | Intake §17.7.P.3; F-13 |
| `redimensionar(id)` | `resize(id)` | Función de fachada | Intake §17.7.P.3 |
| `destruir(id)` | `destroy(id)` | Función de fachada | Intake §17.7.P.3 |
| `establecerMovimiento(id, opciones)` | `setMotion(id, options)` | Función de fachada | Intake §17.7.P.3 (sexta función, decisión 2026-08-09); `Definicion-Contrato-De-Fachada.md` §4.6; F-25 |

### 6.7 Clase 5 · Valores de conjuntos cerrados (10)

Decididos por `F-02` el 2026-08-12: **identificador en inglés, etiqueta en castellano**. La etiqueta es la que ve la persona y la compone `GeometriaFactory.Web.Services`; el identificador es el que se persiste y se serializa.

| Castellano | Inglés (identificador) | Etiqueta que ve la persona | Clase | Dónde está declarado el concepto |
| --- | --- | --- | --- | --- |
| `Alumno` | `Student` | «Alumno» | Valor de `Role` | `Definicion-Modelo-De-Dominio.md` §2.1; `Contratos-REST.md` §2.2 |
| `Administrador` | `Administrator` | «Administrador» | Valor de `Role` | `Definicion-Modelo-De-Dominio.md` §2.1 |
| `Pendiente` **(cuenta)** | `Pending` | «Pendiente» | Valor de `AccountStatus` | `Definicion-Modelo-De-Dominio.md` §5.1 |
| `Habilitado` | `Enabled` | «Habilitado» | Valor de `AccountStatus` | `Definicion-Modelo-De-Dominio.md` §5.1 |
| `Bloqueado` | `Blocked` | «Bloqueado» | Valor de `AccountStatus` | `Definicion-Modelo-De-Dominio.md` §5.1 |
| `Borrador` | `Draft` | «Borrador» | Valor de `WorkStatus` | `Definicion-Modelo-De-Dominio.md` §5.2 |
| `Pendiente` **(trabajo)** | `Submitted` | «Pendiente» | Valor de `WorkStatus` | `Definicion-Modelo-De-Dominio.md` §5.2. **Deliberadamente distinto de `Pending`**: son dos conceptos y el castellano los colapsa |
| `Finalizado` | `Approved` | «Finalizado» | Valor de `WorkStatus` | `Definicion-Modelo-De-Dominio.md` §5.2: es el desenlace de aprobación del administrador |
| `Rechazado` | `Rejected` | «Rechazado» | Valor de `WorkStatus` | `Definicion-Modelo-De-Dominio.md` §5.2 |
| `Advertencia` | `Warning` | «Advertencia» | Valor de `ObservationKind` | `Definicion-Modelo-De-Dominio.md` §2.5 |
| `Error de validación` / `ErrorDeValidacion` | `ValidationError` | «Error de validación» | Valor de `ObservationKind` | `Definicion-Modelo-De-Dominio.md` §2.5; [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.7 |

**Son diez identificadores en once filas**: `Pendiente` ocupa dos, porque nombra dos conceptos, y ésa es exactamente la colisión que `F-02` deshace. La regla operativa que se desprende: **en prosa, «pendiente» a secas sigue prohibido** —la forma calificada que `Definicion-Modelo-De-Dominio.md` §2.1 exige sigue vigente **para el texto**—; lo que deja de hacer falta es calificarlo en el código, porque ahí ya son dos palabras distintas.

### 6.8 Clase 6 · Códigos de condición y de contrato (101)

Decididos por `F-03` el 2026-08-12: **todos van a inglés**. La convención es la del corolario 3 de §6.1 — `SCREAMING_SNAKE_CASE`, palabras inglesas, sin prefijo de proyecto y **sin `CONTRATO_`**.

**Cómo leer la columna de clase.** `Domain`, `Application`, `Infrastructure` y `Visor` nombran el catálogo que **declara** el código en la primera celda de una fila de su §3; un código declarado por dos catálogos lleva los dos. `Contracts` es el conjunto cerrado de la frontera HTTP. `retirado` es un identificador que ya no es condición viva de ningún catálogo pero **sigue apareciendo en el corpus**, y por eso necesita nombre: sin él, una cita vieja se traduce por criterio propio.

#### 6.8.1 Los 42 del catálogo de `GeometriaFactory-Domain`

| Castellano | Inglés | Clase | Dónde está declarado el concepto |
| --- | --- | --- | --- |
| `ADMINISTRADOR_YA_CONFIGURADO` | `ADMINISTRATOR_ALREADY_CONFIGURED` | Domain · Application | `Domain/03` §3.12 · CU-12 · RN-01 |
| `ADVERTENCIA_SIN_LOS_DOS_VALORES` | `WARNING_MISSING_BOTH_VALUES` | Domain | `Domain/03` §3.7 · CU-07 |
| `ALCANCE_SIN_PAPEL_DE_ADMINISTRADOR` | `SCOPE_REQUIRES_ADMINISTRATOR_ROLE` | Domain | `Domain/03` §3.11 · CU-11 · RN-01, RN-11 |
| `BAJA_SIN_ARRASTRE_DE_TRABAJOS` | `DELETION_WITHOUT_WORK_CASCADE` | Domain | `Domain/03` §3.2 · CU-02 · RN-07 |
| `CAMBIO_DE_CONTRASENA_PENDIENTE` | `PASSWORD_CHANGE_PENDING` | Domain · Application | `Domain/03` §3.4 · CU-04 · RN-13, RN-16 |
| `CONFIGURACION_SIN_CREDENCIAL` | `SETUP_WITHOUT_CREDENTIAL` | Domain · Application | `Domain/03` §3.12 · CU-12 |
| `CREDENCIAL_NO_ADMITIDA_EN_EL_ALTA` | `CREDENTIAL_NOT_ALLOWED_ON_REGISTRATION` | Domain · Application | `Domain/03` §3.1 · CU-01 |
| `CREDENCIAL_VIGENTE_NO_VERIFICADA` | `CURRENT_CREDENTIAL_NOT_VERIFIED` | Domain · Application | `Domain/03` §3.3 · CU-03 |
| `CREDENCIAL_YA_FIJADA` | `CREDENTIAL_ALREADY_SET` | Domain · Application | `Domain/03` §3.3 · CU-03 |
| `CUENTA_BLOQUEADA` | `ACCOUNT_BLOCKED` | Domain · Application | `Domain/03` §3.4 · CU-04 · RN-06 |
| `CUENTA_NO_HABILITADA_PARA_CREDENCIAL` | `ACCOUNT_NOT_ENABLED_FOR_CREDENTIAL` | Domain · Application | `Domain/03` §3.3 · CU-03 · RN-06 |
| `CUENTA_PENDIENTE` | `ACCOUNT_PENDING` | Domain · Application | `Domain/03` §3.4 · CU-04 · RN-06 |
| `DATO_OBLIGATORIO_AUSENTE` | `REQUIRED_FIELD_MISSING` | Domain · Application | `Domain/03` §3.1, §3.12, §3.5 · CU-01, CU-05, CU-12 |
| `DESENLACE_DESCONOCIDO` | `UNKNOWN_OUTCOME` | Domain · Application | `Domain/03` §3.10 · CU-10 · RN-10 |
| `DESENLACE_FUERA_DE_PENDIENTE` | `OUTCOME_OUTSIDE_SUBMITTED` | Domain · Application | `Domain/03` §3.10 · CU-10 · RN-10, RN-11 |
| `DESENLACE_NO_ADMITIDO_EN_ESTE_CONTRATO` | `OUTCOME_NOT_ALLOWED_BY_CONTRACT` | Domain | `Domain/03` §3.8 · CU-08 · RN-10 |
| `DESENLACE_SIN_PAPEL_DE_ADMINISTRADOR` | `OUTCOME_REQUIRES_ADMINISTRATOR_ROLE` | Domain | `Domain/03` §3.10 · CU-10 · RN-10, RN-01 |
| `ENVIO_FUERA_DE_BORRADOR` | `SUBMISSION_OUTSIDE_DRAFT` | Domain · Application | `Domain/03` §3.8 · CU-08 · RN-05 |
| `ENVIO_SIN_INTERPRETACION` | `SUBMISSION_WITHOUT_PARSE_RESULT` | Domain | `Domain/03` §3.8 · CU-08 · RN-05 |
| `ERROR_SIN_UBICACION` | `ERROR_WITHOUT_LOCATION` | Domain | `Domain/03` §3.7 · CU-07 · RN-09 |
| `ESPECIE_DE_OBSERVACION_DESCONOCIDA` | `UNKNOWN_OBSERVATION_KIND` | Domain | `Domain/03` §3.7 · CU-07 · RN-05 |
| `ESTADO_INICIAL_NO_NEGOCIABLE` | `INITIAL_STATUS_NOT_NEGOTIABLE` | Domain · Application | `Domain/03` §3.1, §3.12 · CU-01, CU-12 |
| `FAMILIA_DECLARADA_CONTRADICE_AL_TIPO` | `DECLARED_FAMILY_CONTRADICTS_TYPE` | Domain | `Domain/03` §3.6 · CU-06 |
| `HABILITACION_SIN_CREDENCIAL_PROVISORIA` | `ENABLE_WITHOUT_TEMPORARY_CREDENTIAL` | Domain · Application | `Domain/03` §3.2 · CU-02 · RN-16, RN-14 |
| `OBSERVACION_SOBRE_PIEZA_INEXISTENTE` | `OBSERVATION_ON_MISSING_PIECE` | Domain | `Domain/03` §3.7 · CU-07 · RN-09 |
| `OPERACION_DESCONOCIDA` | `UNKNOWN_OPERATION` | Domain | `Domain/03` §3.11, §3.9 · CU-09, CU-11 |
| `OPERACION_FUERA_DE_BORRADOR` | `OPERATION_OUTSIDE_DRAFT` | Domain · Application | `Domain/03` §3.9 · CU-09 · RN-04 |
| `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` | `OPERATION_NOT_APPLICABLE_TO_ADMINISTRATOR_ACCOUNT` | Domain | `Domain/03` §3.13, §3.2 · CU-02, CU-13 · RN-01 · **unifica a `CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA`, §6.9** |
| `PAPEL_DE_ADMINISTRADOR_FUERA_DE_ESTE_CAMINO` | `ADMINISTRATOR_ROLE_OUTSIDE_THIS_PATH` | Domain · Application | `Domain/03` §3.1 · CU-01 · RN-01 |
| `POSICION_DE_PIEZA_INVALIDA` | `INVALID_PIECE_POSITION` | Domain | `Domain/03` §3.6 · CU-06 |
| `RECONSTRUCCION_SOBRE_TRABAJO_TERMINAL` | `REBUILD_ON_TERMINAL_WORK` | Domain | `Domain/03` §3.6 · CU-06 · RN-10 |
| `REEDICION_FUERA_DE_BORRADOR` | `EDIT_OUTSIDE_DRAFT` | Domain | `Domain/03` §3.5 · CU-05 · RN-04 |
| `RESETEO_CON_ARRASTRE_DE_TRABAJOS` | `RESET_WITH_WORK_CASCADE` | Domain | `Domain/03` §3.13 · CU-13 · RN-12 |
| `TEXTO_ORIGINAL_ALTERADO` | `ORIGINAL_JSON_ALTERED` | Domain · Application | `Domain/03` §3.5 · CU-05 · RN-08 |
| `TIPO_DE_PIEZA_DESCONOCIDO` | `UNKNOWN_PIECE_TYPE` | Domain | `Domain/03` §3.6 · CU-06 · RN-09 |
| `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR` | `WORK_OUTSIDE_ADMINISTRATOR_SCOPE` | Domain · Application | `Domain/03` §3.11 · CU-11 · RN-11, RN-04 |
| `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE` | `WORK_NOT_FOUND_FOR_REQUESTER` | Domain · Application | `Domain/03` §3.9 · CU-09 · RN-03 |
| `TRABAJO_SIN_DUENO` | `WORK_WITHOUT_OWNER` | Domain · Application | `Domain/03` §3.5 · CU-05 · RN-03 |
| `TRANSICION_DESDE_ESTADO_TERMINAL` | `TRANSITION_FROM_TERMINAL_STATUS` | Domain · Application | `Domain/03` §3.10, §3.8 · CU-08, CU-10 · RN-10 |
| `TRANSICION_DE_CUENTA_NO_ADMITIDA` | `ACCOUNT_TRANSITION_NOT_ALLOWED` | Domain · Application | `Domain/03` §3.2 · CU-02 |
| `UNICIDAD_DE_CORREO_NO_VERIFICADA` | `EMAIL_UNIQUENESS_NOT_VERIFIED` | Domain | `Domain/03` §3.1, §3.12 · CU-01, CU-12 · RN-02 |
| `VALOR_DERIVADO_VACIO` | `EMPTY_DERIVED_VALUE` | Domain · Application | `Domain/03` §3.13, §3.3 · CU-03, CU-13 |

#### 6.8.2 Los 12 propios del catálogo de `GeometriaFactory-Application`

Los otros 24 de su catálogo de 36 son los que comparte con `Domain` y ya están arriba.

| Castellano | Inglés | Clase | Dónde está declarado el concepto |
| --- | --- | --- | --- |
| `CONFIRMACION_DE_BAJA_NO_COINCIDE` | `DELETION_CONFIRMATION_MISMATCH` | Application | `Application/03` §3.2 · CU-02 · RN-07 |
| `CONJUNTO_DE_PIEZAS_MAL_FORMADO` | `MALFORMED_PIECE_SET` | Application | `Application/03` §3.5 · CU-05 · RN-09 |
| `CORREO_YA_REGISTRADO` | `EMAIL_ALREADY_REGISTERED` | Application · Infrastructure | `Application/03` §3.1 · CU-01, CU-10 · RN-02 |
| `CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA` | `OPERATION_NOT_APPLICABLE_TO_ADMINISTRATOR_ACCOUNT` | Application | `Application/03` §3.2 · CU-02 · RN-01 · **unificado con el de `Domain`, §6.9** |
| `CUENTA_INEXISTENTE` | `ACCOUNT_NOT_FOUND` | Application | `Application/03` §3.2 · CU-02, CU-03, CU-11 |
| `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` | `ADMINISTRATOR_ROLE_REQUIRED` | Application | `Application/03` §3.2 · CU-02, CU-07, CU-08, CU-11 · RN-01, RN-10 |
| `INTERPRETACION_NO_DISPONIBLE` | `PARSE_RESULT_UNAVAILABLE` | Application · Infrastructure | `Application/03` §3.5 · CU-05 · RN-08 |
| `OBSERVACION_MAL_FORMADA` | `MALFORMED_OBSERVATION` | Application | `Application/03` §3.5 · CU-05 · RN-09, RN-05 |
| `PAPEL_NO_RECONOCIDO` | `UNRECOGNIZED_ROLE` | Application | `Application/03` §3.9 · CU-09 · RN-01 |
| `RESETEO_ACOTADO_A_CUENTAS_DE_ALUMNO` | `RESET_LIMITED_TO_STUDENT_ACCOUNTS` | Application | `Application/03` §3.11 · CU-11 · RN-15, RN-01 |
| `SOLICITANTE_NO_DECLARADO` | `REQUESTER_NOT_DECLARED` | Application | `Application/03` §3.6 · CU-06 · RN-03 |
| `TRABAJO_INEXISTENTE` | `WORK_NOT_FOUND` | Application | `Application/03` §3.7 · CU-07 |

#### 6.8.3 Los 15 propios del catálogo de `GeometriaFactory-Infrastructure`

Los otros 2 de su catálogo de 17 son `CORREO_YA_REGISTRADO` e `INTERPRETACION_NO_DISPONIBLE`, que ya están arriba.

| Castellano | Inglés | Clase | Dónde está declarado el concepto |
| --- | --- | --- | --- |
| `ALMACEN_NO_DISPONIBLE` | `STORE_UNAVAILABLE` | Infrastructure | `Infrastructure/03` §3.3 · CU-03, CU-04, CU-05 |
| `CLAVE_DE_FIRMA_AUSENTE` | `SIGNING_KEY_MISSING` | Infrastructure | `Infrastructure/03` §3.8 · CU-08 |
| `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO` | `PIECE_SET_NOT_REBUILT` | Infrastructure | `Infrastructure/03` §3.2 · CU-02 |
| `CONSULTA_SIN_ALCANCE_DECLARADO` | `QUERY_WITHOUT_DECLARED_SCOPE` | Infrastructure | `Infrastructure/03` §3.3 · CU-03 · RN-03, RN-11 |
| `CONTRASENA_EN_CLARO_AUSENTE` | `PLAINTEXT_PASSWORD_MISSING` | Infrastructure | `Infrastructure/03` §3.6 · CU-06 |
| `CREDENCIAL_DERIVADA_ILEGIBLE` | `UNREADABLE_PASSWORD_HASH` | Infrastructure | `Infrastructure/03` §3.6 · CU-06 |
| `ESCRITURA_CONCURRENTE_RECHAZADA` | `CONCURRENT_WRITE_REJECTED` | Infrastructure | `Infrastructure/03` §3.3 · CU-03 |
| `ESCRITURA_QUE_REESCRIBE_EL_TEXTO_ORIGINAL` | `WRITE_REWRITES_ORIGINAL_JSON` | Infrastructure | `Infrastructure/03` §3.3 · CU-03 · RN-08 |
| `FUENTE_DE_ALEATORIEDAD_NO_DISPONIBLE` | `RANDOMNESS_SOURCE_UNAVAILABLE` | Infrastructure | `Infrastructure/03` §3.7 · CU-07 · RN-14 |
| `MIGRACION_NO_APLICABLE` | `MIGRATION_NOT_APPLICABLE` | Infrastructure | `Infrastructure/03` §3.9 · CU-10 |
| `RECLAMOS_INCOMPLETOS` | `INCOMPLETE_CLAIMS` | Infrastructure | `Infrastructure/03` §3.8 · CU-08 |
| `RETIRO_PARCIAL_NO_ADMITIDO` | `PARTIAL_DELETION_NOT_ALLOWED` | Infrastructure | `Infrastructure/03` §3.4 · CU-04 · RN-07, RN-04 |
| `RUTA_DEL_ALMACEN_NO_DISPONIBLE` | `STORE_PATH_UNAVAILABLE` | Infrastructure | `Infrastructure/03` §3.9 · CU-10 |
| `TEXTO_ORIGINAL_AUSENTE` | `ORIGINAL_JSON_MISSING` | Infrastructure | `Infrastructure/03` §3.1 · CU-01 |
| `UNICIDAD_DE_ADMINISTRADOR_VIOLADA` | `ADMINISTRATOR_UNIQUENESS_VIOLATED` | Infrastructure | `Infrastructure/03` §3.5 · CU-05 · RN-01 |

#### 6.8.4 Los 7 del catálogo de `GeometriaFactory-Visor`

Son las **siete condiciones de la fachada**, y las siete tienen escenario en el intake §21.

| Castellano | Inglés | Clase | Dónde está declarado el concepto |
| --- | --- | --- | --- |
| `CAPACIDAD_GRAFICA_AUSENTE` | `GRAPHICS_CAPABILITY_MISSING` | Visor | `Visor/03` §3.1, entrada `E-VIS-01`, función `inicializar` |
| `DIMENSION_NO_LEGIBLE` | `UNREADABLE_DIMENSION` | Visor | `Visor/03` §3.3, entrada `E-VIS-10`, función `cargarJson` por pieza; intake §20.E-8 |
| `ELEMENTO_DE_DIBUJO_INVALIDO` | `INVALID_CANVAS_ELEMENT` | Visor | `Visor/03` §3.1, entradas `E-VIS-02` y `E-VIS-07` |
| `INDICE_FUERA_DE_RANGO` | `INDEX_OUT_OF_RANGE` | Visor | `Visor/03` §3.4, entradas `E-VIS-11` y `E-VIS-12`, función `seleccionarPieza` |
| `INSTANCIA_DESCONOCIDA` | `UNKNOWN_INSTANCE` | Visor | `Visor/03` §3.2, entradas `E-VIS-03` a `E-VIS-06` y `E-VIS-13`, en cinco funciones |
| `TEXTO_NO_LEGIBLE` | `UNREADABLE_TEXT` | Visor | `Visor/03` §3.3, entrada `E-VIS-08`, función `cargarJson` |
| `TIPO_NO_DIBUJABLE` | `NON_DRAWABLE_TYPE` | Visor | `Visor/03` §3.3, entrada `E-VIS-09`; intake §20.E-5 |

#### 6.8.5 Los 4 identificadores internos retirados

Ninguno es condición viva de ningún catálogo, y los cuatro **siguen apareciendo en la cadena documental**. Llevan nombre inglés por una sola razón: para que una cita vieja resuelva contra la tabla y no contra el criterio de quien la lea. **Ninguno se recicla** — es la regla que `Domain/03` §6.1 ya fija.

| Castellano | Inglés | Clase | Dónde está declarado el concepto |
| --- | --- | --- | --- |
| `RECONSTRUCCION_SOBRE_TRABAJO_FINALIZADO` | `REBUILD_ON_APPROVED_WORK` | retirado por **renombre** ⟶ `REBUILD_ON_TERMINAL_WORK` | `Domain/03` §6.1; `Domain CU-06` 1.1 |
| `POSICION_DE_PIEZA_NO_CONTIGUA` | `NON_CONTIGUOUS_PIECE_POSITION` | retirado por **renombre** ⟶ `INVALID_PIECE_POSITION` | `Domain/03` §6.1; `Domain CU-06` 1.1, ronda r1 |
| `CREDENCIAL_NO_ESTABLECIDA` | `CREDENTIAL_NOT_SET` | retirado por **imposibilidad de su causa** (RN-16) | `Domain/03` §6.1; `Application/03` §7.1, que lo saca del catálogo en su 1.6 |
| `RESETEO_SOBRE_CREDENCIAL_NO_FIJADA` | `RESET_ON_UNSET_CREDENTIAL` | retirado por **imposibilidad de su causa** (RN-16) | `Domain/03` §6.1 |

**El quinto retirado de `Domain` no está acá y no falta**: `CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA` sigue **vivo en el catálogo de `Application`** y tiene su fila en §6.8.2, unificado. Es el caso de §6.9.

#### 6.8.6 Los 21 `CONTRATO_*` de `GeometriaFactory-Contracts`

**Es la parte de `F-03` que cambia el contrato** (§5.3). El prefijo `CONTRATO_` desaparece: la identidad del código la da el conjunto cerrado que lo declara, no un prefijo dentro del nombre.

| Castellano | Inglés | Clase | Dónde está declarado el concepto |
| --- | --- | --- | --- |
| `CONTRATO_CAMPO_REQUERIDO_AUSENTE` | `REQUIRED_FIELD_MISSING` | Contracts, vivo (`DXT-01`) | `Contracts/03` §3.2; `Contratos-REST.md` |
| `CONTRATO_CREDENCIAL_INVALIDA` | `INVALID_CREDENTIALS` | Contracts, vivo (`DXT-02`) | `Contracts/03` §3.2 |
| `CONTRATO_CUENTA_NO_HABILITADA` | `ACCOUNT_NOT_ENABLED` | Contracts, vivo (`DXT-03`) | `Contracts/03` §3.2; RN-06 |
| `CONTRATO_CORREO_YA_REGISTRADO` | `EMAIL_ALREADY_REGISTERED` | Contracts, vivo (`DXT-04`) | `Contracts/03` §3.2; RN-02 |
| `CONTRATO_CONFIRMACION_NO_COINCIDE` | `CONFIRMATION_MISMATCH` | Contracts, vivo (`DXT-05`) | `Contracts/03` §3.2; RN-07 |
| `CONTRATO_ADMINISTRADOR_YA_CONFIGURADO` | `ADMINISTRATOR_ALREADY_CONFIGURED` | Contracts, vivo (`DXT-06`) | `Contracts/03` §3.2; RN-01 |
| `CONTRATO_TRABAJO_NO_ENCONTRADO` | `WORK_NOT_FOUND` | Contracts, vivo (`DXT-07`) | `Contracts/03` §3.2; RN-03, RN-11 |
| `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` | `STATE_FORBIDS_DELETE` | Contracts, vivo (`DXT-08`) | `Contracts/03` §3.2; RN-04 |
| `CONTRATO_ALUMNO_NO_ENCONTRADO` | `STUDENT_NOT_FOUND` | Contracts, vivo (`DXT-10`) | `Contracts/03` §3.2 |
| `CONTRATO_SERVICIO_NO_DISPONIBLE` | `SERVICE_UNAVAILABLE` | Contracts, vivo (`DXT-11`) | `Contracts/03` §3.2 |
| `CONTRATO_ERROR_NO_CLASIFICADO` | `UNCLASSIFIED_ERROR` | Contracts, vivo (`DXT-12`) | `Contracts/03` §3.2; `DXC-03` |
| `CONTRATO_ESTADO_NO_PERMITE_DESENLACE` | `STATE_FORBIDS_OUTCOME` | Contracts, vivo (`DXT-14`) | `Contracts/03` §3.2; RN-10, `RT-08` |
| `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` | `OUTCOME_ADMIN_ONLY` | Contracts, vivo (`DXT-15`) | `Contracts/03` §3.2; RN-10 |
| `CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO` | `PASSWORD_CHANGE_REQUIRED` | Contracts, vivo (`DXT-16`) | `Contracts/03` §3.2; RN-13, RN-16, INV-09 |
| `CONTRATO_RESETEO_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` | `RESET_NOT_APPLICABLE_TO_ADMINISTRATOR_ACCOUNT` | Contracts, vivo (`DXT-17`) | `Contracts/03` §3.2; RN-15, INV-08 |
| `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` | `OPERATION_ADMIN_ONLY` | Contracts, vivo (`DXT-19`) | `Contracts/03` §3.2; intake **1.29** §17.4.P.3 |
| `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` | `STATE_FORBIDS_UPDATE` | Contracts, vivo (`DXT-20`) | `Contracts/03` §3.2; intake **1.29** §17.4.P.3 |
| `CONTRATO_LISTADO_VACIO` | `EMPTY_LIST` | Contracts, **señal declarada que no es error** (`DXT-N1`) | `Contracts/03` §3.3; `CU-04` §6.1 |
| `CONTRATO_TEXTO_NO_INTERPRETABLE` | `TEXT_NOT_PARSEABLE` | Contracts, **señal** (`DXT-N2`, `DXT-N3`); retirado como código de error en `DXT-09` | `Contracts/03` §3.2 y §3.3 |
| `CONTRATO_CONTRASENA_NO_ESTABLECIDA` | `PASSWORD_NOT_SET` | Contracts, **retirado** (`DXT-13`, por RN-16) | `Contracts/03` §3.2 |
| `CONTRATO_RESETEO_NO_APLICABLE_A_CUENTA_SIN_CONTRASENA` | `RESET_NOT_APPLICABLE_TO_PASSWORDLESS_ACCOUNT` | Contracts, **retirado** (`DXT-18`, por RN-16) | `Contracts/03` §3.2 |

**Cuadre con el intake.** 17 códigos de error vivos + 1 señal que nunca fue error (`EMPTY_LIST`) + 1 señal que dejó de ser error (`TEXT_NOT_PARSEABLE`) + 2 retirados = **21 identificadores**. El intake §17.4.P.3 dice «diecisiete vivos sobre veinte identificadores emitidos» y cuenta **códigos de error**: 17 + 3 retirados = 20. Las dos cifras son correctas y cuentan cosas distintas; se declara acá para que nadie las cruce.

#### 6.8.7 Los dos huérfanos, que **no se traducen todavía**

| Castellano | Inglés | Clase | Dónde está declarado el concepto |
| --- | --- | --- | --- |
| `CONTRATO_CAMBIO_DE_CONTRASENA_PENDIENTE` | **sin nombre: no se traduce** | `huérfano` | Citado por `Web CU-02` y `Web CU-03`. **No lo declara ningún catálogo** |
| `CONTRATO_RESETEO_NO_ADMITIDO` | **sin nombre: no se traduce** | `huérfano` | Citado por `Web CU-04`. **No lo declara ningún catálogo** |

Es el defecto de fondo de §5.3, preexistente y ajeno al idioma. **La regla operativa de §6.1 se aplica sin excepción**: primero los declara `GeometriaFactory-Contracts` —o se corrige la cita hacia el código que sí existe—, después entran a esta tabla, y recién después se renombran. El tramo `R-5` de §8 los toma como condición de entrada.

### 6.9 Las dos unificaciones y las cuatro coincidencias de nombre

**Dos conceptos que el corpus nombra hoy con dos identificadores distintos, y que el glosario unifica** (corolario 1 de §6.1):

| Los dos nombres castellanos | El nombre inglés único | Por qué son el mismo concepto |
| --- | --- | --- |
| `CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA` (vivo en `Application`) y `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` (vivo en `Domain`) | `OPERATION_NOT_APPLICABLE_TO_ADMINISTRATOR_ACCOUNT` | `Domain/03` §6.1 declara que el primero **fue reemplazado** por el segundo en `CU-02` 1.2, ronda r3, hallazgo H-01, porque cubría una sola de las cuatro operaciones. **`Application` no absorbió el renombre** y sigue declarando el nombre viejo. La unificación es la corrección |
| `ALUMNO`, `TRABAJO`, `PIEZA`, `COMPONENTE`, `OBSERVACION` (intake §17.3.P.4, de `RT §7.1`) y las entidades `Cuenta`, `Trabajo`, `Pieza`, `Componente`, `Observacion` | `Account`, `Work`, `Piece`, `Component`, `Observation` | `Modelo-Datos-Logico.md` §7 ya declara la correspondencia una a una entre cada tabla y su entidad conceptual, y §2 ya titula las tablas con el nombre de la entidad. La forma en mayúsculas es transcripción de la fuente, no un segundo concepto |

**Cuatro pares que quedan con el mismo nombre en dos catálogos distintos, y no es colisión.** Al eliminar el prefijo `CONTRATO_`, cuatro códigos del contrato quedan con el nombre que ya lleva su equivalente interno. **Es lo correcto**: son el mismo concepto visto desde dos capas, y el corolario 1 pide un nombre. Lo que los separa es el tipo que los contiene —el conjunto cerrado de `GeometriaFactory.Contracts` por un lado, el catálogo interno del proyecto por el otro—, que es exactamente cómo C# separa dos constantes homónimas.

| Nombre inglés | Lo declara el contrato como | Y el catálogo interno como |
| --- | --- | --- |
| `REQUIRED_FIELD_MISSING` | `CONTRATO_CAMPO_REQUERIDO_AUSENTE` | `DATO_OBLIGATORIO_AUSENTE` |
| `EMAIL_ALREADY_REGISTERED` | `CONTRATO_CORREO_YA_REGISTRADO` | `CORREO_YA_REGISTRADO` |
| `ADMINISTRATOR_ALREADY_CONFIGURED` | `CONTRATO_ADMINISTRADOR_YA_CONFIGURADO` | `ADMINISTRADOR_YA_CONFIGURADO` |
| `WORK_NOT_FOUND` | `CONTRATO_TRABAJO_NO_ENCONTRADO` | `TRABAJO_INEXISTENTE` |

**Y una advertencia que hay que dejar escrita**: la prueba de inspección que `ADR-06` de `GeometriaFactory-Application` exige —comparar los códigos emitidos contra el catálogo **en las dos direcciones**— tiene que comparar **contra su catálogo**, no contra el conjunto de nombres. Con el prefijo, un recuento textual alcanzaba; sin él, hay que mirar el tipo. El tramo `R-5` de §8 lo verifica.

### 6.10 Los espacios de nombres

Los **18 espacios de nombres** que [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.3 propone —**16 subsegmentos distintos**, porque `Cuentas` y `Trabajos` se repiten en dos proyectos de código—, llevados a inglés. Las tres reglas que los acompañan —un solo nivel, coincidencia con la carpeta, el subsegmento no espeja la partición de componentes— **no cambian**.

| Propuesto en `Plan-Etapa-A.md` §1.3 | Esta norma |
| --- | --- |
| `GeometriaFactory.Domain.Entidades` | `GeometriaFactory.Domain.Entities` |
| `GeometriaFactory.Domain.Valores` | `GeometriaFactory.Domain.Values` |
| `GeometriaFactory.Domain.Guardas` | `GeometriaFactory.Domain.Guards` |
| `GeometriaFactory.Contracts.Cuentas` | `GeometriaFactory.Contracts.Accounts` |
| `GeometriaFactory.Contracts.Trabajos` | `GeometriaFactory.Contracts.Works` |
| `GeometriaFactory.Contracts.Servicio` | `GeometriaFactory.Contracts.Service` |
| `GeometriaFactory.Application.Puertos` | `GeometriaFactory.Application.Ports` |
| `GeometriaFactory.Application.Cuentas` | `GeometriaFactory.Application.Accounts` |
| `GeometriaFactory.Application.Trabajos` | `GeometriaFactory.Application.Works` |
| `GeometriaFactory.Infrastructure.Persistencia` | `GeometriaFactory.Infrastructure.Persistence` |
| `GeometriaFactory.Infrastructure.Seguridad` | `GeometriaFactory.Infrastructure.Security` |
| `GeometriaFactory.Infrastructure.Validacion` | `GeometriaFactory.Infrastructure.Validation` |
| `GeometriaFactory.Infrastructure.Tiempo` | `GeometriaFactory.Infrastructure.Time` |
| `GeometriaFactory.Api.Puntos` | `GeometriaFactory.Api.Endpoints` |
| `GeometriaFactory.Api.Composicion` | `GeometriaFactory.Api.Composition` |
| `GeometriaFactory.Web.Componentes` | `GeometriaFactory.Web.Components` |
| `GeometriaFactory.Web.Servicios` | `GeometriaFactory.Web.Services` |
| `GeometriaFactory.Web.Integracion` | `GeometriaFactory.Web.Integration` |

**Costo: una edición.** Los 18 espacios de nombres viven en **un solo documento** —[`Plan-Etapa-A.md`](Plan-Etapa-A.md)— con 18 ocurrencias. Son propuesta, no declaración: nada los cita todavía.

**Una nota sobre `GeometriaFactory.Api.Endpoints`.** El glosario funcional de `GeometriaFactory-Api` §2 fija que **en la prosa se dice «punto de acceso» y no «endpoint»**, con fundamento. Esta norma no lo toca: la prosa sigue diciendo «punto de acceso», y el espacio de nombres dice `Endpoints`. Es exactamente la separación de §3 y §4 funcionando, y conviene que el primer caso donde se nota quede escrito.

### 6.11 Las superficies derivadas: carpetas y nombres de archivo

**La clase que faltaba no es una clase de identificadores: es la segunda y la tercera escritura de todos ellos.** Cada identificador del glosario se escribe además como **segmento de carpeta** y como **nombre de archivo**, y el renombre las arrastra porque dos reglas ya declaradas lo obligan: la regla de forma 3 de §3 —**el espacio de nombres coincide con la carpeta**— y la última fila de la tabla de §3 —**el nombre de archivo de código es igual al tipo que contiene**—. §2.3 cuenta lo que hay hoy.

> **La regla, y cubre la clase entera.** Las dos escrituras derivadas de un identificador —su carpeta y su archivo— **son parte del identificador**: se renombran **con él, en el mismo tramo y bajo el mismo cuadre**, y nunca en un tramo aparte.
>
> **Y por debajo del nivel de espacio de nombres el idioma no se afloja.** Toda carpeta y todo archivo que viva bajo `src/`, `tests/` o `visor/` va en **inglés**, con la forma de §3 —`PascalCase` para carpetas y para archivos que contienen un tipo; la convención del marco donde el marco la impone, como `wwwroot/js/`—, **incluidas las que ningún espacio de nombres nombra**: las que están por debajo de ese nivel, y las que el marco no proyecta a espacio de nombres.
>
> **La regla de §4 no las alcanza.** «Nombres de archivo de la documentación, castellano, `Título-Con-Guiones`» rige **sólo** los archivos de `SDD/`. No rige nada bajo `src/`, `tests/` ni `visor/`. Ésa es la confusión que dejó `Configuraciones/` y `Paginas/` sin renombrar en el ensayo `R-1`: no estaban exentas, estaban sin regla.
>
> **Corolario.** Una carpeta que **no** nombra ningún concepto del glosario no está exenta: es nombre de código, va en inglés y **se agrega al glosario** como cualquier otro, por el corolario 4 de §6.1.

**Y la frontera, declarada, para que el control no la vuelva a plantear.** La **maqueta** de `SDD/Maquetas/GeometriaFactory-Web/` es HTML, CSS y JavaScript, y sus archivos y sus constantes están en castellano —`Maqueta.js`, `Datos-Maqueta.js`, `Visor-Tridimensional.js`, `Estilos-Maqueta.css`, y las 40 constantes `SCREAMING_SNAKE_CASE` que §2.2 cuenta—. **No se renombran, y no es un apartamiento.** Su propio `README.md` §1 declara que **no es el producto** y que **no es documentación viva**: es «la línea de base de un momento», aprobada por el Product Owner el 2026-08-11. Renombrarla alteraría una línea de base ya aprobada, que es exactamente lo que un registro histórico no admite (§4.1).

**Las cinco filas que la regla obliga a agregar**, que son los casos vivos que el corpus tiene hoy (§2.3):

| Castellano | Inglés | Clase | Dónde está declarado el concepto |
| --- | --- | --- | --- |
| `Configuraciones/` | `Configurations/` | Carpeta, por debajo del nivel de espacio de nombres, dentro de `Persistence/` | [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.6; contiene los cinco mapeos de `BT-05` que §6.4 ya nombra |
| `Paginas/` | `Pages/` | Carpeta, por debajo del nivel, dentro de `Components/` | [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.6 y §1.7 |
| `visor/` (la de `visor/src/`) | `viewer/` | Carpeta de la **capa 3** del *bundle*, por debajo del nivel | [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.6; `Visor ADR-01`, que declara las tres capas. **No es** la raíz `visor/` del proyecto de código, que no se renombra (§1) |
| `Internos` | `Internal` | Segmento del **contraejemplo** de la regla de un solo nivel | [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.3. Es un nombre que la norma prohíbe usar; lleva fila igual, porque el contraejemplo se escribe y hay que escribirlo en inglés |
| `VisorFiguras` ⟶ `VisorFiguras.razor` | `FigureViewer` ⟶ `FigureViewer.razor` | Componente Blazor que envuelve al visor: **tipo y nombre de archivo** | Intake §17.7.P.2, que lo declara como la **capa 1** de las tres; `Visor ADR-01`. Es el único identificador de tipo que ninguna de las seis clases había contado |

**El ensayo encontró tres huecos y la remedición encontró cinco.** `Configuraciones/`, `Paginas/` e `Internos` los reportó el tramo `R-1`; `visor/` de la capa 3 y `VisorFiguras.razor` aparecieron al barrer el corpus entero con el instrumento de §2.1. Se verificó además que **`Migrations/` y `Layout/` ya están en inglés** y no requieren acción, y se declara acá para que el control `V-6` de §7 no las vuelva a levantar.

### 6.12 Agregados por la etapa `b` de `GeometriaFactory-Web`, fuera de los 155

**Por qué existe esta sección.** La etapa `b` construye las once superficies como pantallas de
marcador de posición y porta el sistema visual de la maqueta aprobada, y eso escribe identificadores
de código que **ninguna de las ocho tablas anteriores tenía**: los tipos de componente de las
superficies y de los dos armazones, sus miembros, los iconos que la barra lateral usa y —el grueso—
**los nombres de clase CSS**. Entran por el **corolario 4 de §6.1**, con el mismo criterio con el que
la 1.5 agregó las cinco filas de la etapa `a`: **no cuentan dentro de los 155**, porque no existían
cuando se contaron las seis clases.

**Y la frontera con la maqueta se mantiene.** §6.11 declara que los archivos y las constantes de
`SDD/Maquetas/GeometriaFactory-Web/` **no se renombran**, porque son línea de base ya aprobada. Esta
sección **no los renombra**: declara la correspondencia entre el nombre castellano que la maqueta usa
y el nombre inglés con el que el producto lo escribe. Las dos escrituras conviven, cada una en su
lado de la frontera, y la tabla es lo que permite verificar que el porte fue fiel.

#### 6.12.1 Tipos de componente (24)

| Castellano | Inglés | Clase | Dónde está declarado el concepto |
| --- | --- | --- | --- |
| Shell de acceso | `AccessShell` | Tipo, componente de armazón | `Experiencia-De-Uso.md` §3.2, primer diagrama |
| Shell de trabajo | `WorkShell` | Tipo, componente de armazón | `Experiencia-De-Uso.md` §3.2, segundo diagrama |
| Resolución del destino inicial | `InitialDestination` | Tipo, componente de página | `Linea-Base-Visual.md` §5, `NAV-01` y `NAV-03` |
| `Aprovisionamiento-Inicial` | `InitialProvisioning` | Tipo, componente de página | `Linea-Base-Visual.md` §2, `SUP-01` |
| `Registro-De-Cuenta` | `AccountRegistration` | Tipo, componente de página | `Linea-Base-Visual.md` §2, `SUP-02` |
| `Ingreso` | `SignIn` | Tipo, componente de página | `Linea-Base-Visual.md` §2, `SUP-03` |
| `Credencial-Propia`, curso de establecimiento | `OwnCredentialSetup` | Tipo, componente de página | `Wireframes-Credencial-Propia.md` §1, primera fila de los tres cursos |
| `Credencial-Propia`, curso de cambio forzado | `OwnCredentialForcedChange` | Tipo, componente de página | `Wireframes-Credencial-Propia.md` §1, tercera fila. **Sin validación visual**: `Linea-Base-Visual.md` §6.1, fila de `F-26` |
| `Credencial-Propia`, curso de cambio | `OwnCredentialChange` | Tipo, componente de página | `Wireframes-Credencial-Propia.md` §1, segunda fila |
| `Panel-De-Trabajos-Del-Alumno` | `StudentWorkPanel` | Tipo, componente de página | `Linea-Base-Visual.md` §2, `SUP-05` |
| `Envio-De-Trabajo` | `WorkSubmission` | Tipo, componente de página | `Linea-Base-Visual.md` §2, `SUP-06` |
| `Vista-De-Trabajo` | `WorkView` | Tipo, componente de página | `Linea-Base-Visual.md` §2, `SUP-07` |
| `Resolucion-Del-Trabajo` | `WorkResolution` | Tipo, componente **alojado**, sin ruta | `Linea-Base-Visual.md` §2, `SUP-08` |
| `Panel-De-Cuentas` | `AccountsPanel` | Tipo, componente de página | `Linea-Base-Visual.md` §2, `SUP-09` |
| `Listado-De-La-Comision` | `ClassSubmissionList` | Tipo, componente de página | `Linea-Base-Visual.md` §2, `SUP-10` |
| `Estado-Degradado-Y-Reconexion` | `DegradedStateOverlay` | Tipo, componente **superpuesto**, sin ruta | `Linea-Base-Visual.md` §2 y §5, `SUP-11` |
| Sello de versión | `VersionSeal` | Tipo, componente compartido | `Representacion-Sello-De-Version.md` §2; `CMP-09` |
| Icono | `Icon` | Tipo, componente compartido | Constante `ICONOS` de `assets/js/Maqueta.js` de la maqueta aprobada |
| Rótulo de marcador de posición | `StagePlaceholder` | Tipo, componente compartido | **Propuesta de la etapa `b`**: `Roadmap-Producto.md`, primer criterio de transición, «pantallas de marcador de posición» |
| Contenido previsto de la superficie | `SurfaceOutline` | Tipo, componente compartido | **Propuesta de la etapa `b`**, sobre `Linea-Base-Visual.md` §3 |
| Superficie de dirección no encontrada | `NotFoundSurface` | Tipo, componente compartido | **Propuesta declarada de la etapa `b`**: ninguna fuente la declara, y por eso **no lleva `SUP-XX`** |
| Página de dirección no encontrada | `NotFoundPage` | Tipo, componente de página | **Propuesta declarada de la etapa `b`**: punto de reejecución del código 404 |
| Carpeta de componentes compartidos | `Shared/` | Carpeta, por debajo del nivel de espacio de nombres, dentro de `Components/` | Corolario de §6.11. Aloja las representaciones reutilizadas de `Arquitectura-Proyecto-Codigo.md` §3.1 |
| Carpeta de componentes del trabajo | `Work/` | Carpeta, por debajo del nivel, dentro de `Components/` | Corolario de §6.11. Aloja `SUP-08`, que no es página porque no tiene ruta |

#### 6.12.2 Miembros y parámetros (13)

| Castellano | Inglés | Clase | Dónde está declarado el concepto |
| --- | --- | --- | --- |
| Destino de la barra lateral | `Destination` | Tipo anidado | `Experiencia-De-Uso.md` §3.2, tabla de los tres destinos por papel; constante `DESTINOS` de `Maqueta.js` |
| Destinos del alumno | `StudentDestinations` | Miembro | Misma tabla, fila «Alumno» |
| Destinos del administrador | `AdministratorDestinations` | Miembro | Misma tabla, fila «Administrador» |
| Destinos vigentes | `Destinations` | Propiedad | Misma tabla |
| Rótulo del papel | `RoleLabel` | Propiedad | `Experiencia-De-Uso.md` §3.2, pie de la barra lateral |
| Es administrador | `_isAdministrator` | Campo privado | Ídem; el papel es el de `Glosario-Funcional.md` |
| Identificador del trabajo | `WorkId` | Parámetro de ruta | `Work` ya está en §6.4; el identificador es el de `Representacion-Fila-De-Trabajo.md` |
| Identificador de superficie | `SurfaceId` | Parámetro | `Linea-Base-Visual.md` §2, columna `ID` |
| Nombre canónico de superficie | `SurfaceName` | Parámetro | `Linea-Base-Visual.md` §2, columna «Nombre canónico» |
| Caso de uso | `UseCase` | Parámetro | `Linea-Base-Visual.md` §2, columna «CU que la origina» |
| Componentes de la superficie | `Components` | Parámetro | `Linea-Base-Visual.md` §3 |
| Clase de tamaño del icono | `SizeClass` | Parámetro | Función `icono(nombre, clase)` de `Maqueta.js` |
| Encabezado de la superficie | `Heading` | Propiedad | `CMP-16` y `CMP-34` |

#### 6.12.3 Iconos (9)

Los nombres del catálogo `ICONOS` de `assets/js/Maqueta.js`. Se portan **los nueve que la etapa `b`
usa**; los otros doce entran cuando entre la superficie que los necesita.

| Castellano | Inglés | Clase | Dónde está declarado el concepto |
| --- | --- | --- | --- |
| `marca` | `Brand` | Valor de conjunto cerrado, catálogo de iconos | `ICONOS` de `assets/js/Maqueta.js` |
| `trabajos` | `Works` | Valor de conjunto cerrado, catálogo de iconos | Ídem; destino 1 del alumno |
| `nuevo` | `New` | Valor de conjunto cerrado, catálogo de iconos | Ídem; destino 2 del alumno |
| `llave` | `Key` | Valor de conjunto cerrado, catálogo de iconos | Ídem; destino 3 de los dos papeles |
| `cuentas` | `Accounts` | Valor de conjunto cerrado, catálogo de iconos | Ídem; destino 2 del administrador |
| `comision` | `ClassList` | Valor de conjunto cerrado, catálogo de iconos | Ídem; destino 1 del administrador |
| `salir` | `SignOut` | Valor de conjunto cerrado, catálogo de iconos | Ídem; cierre de sesión, `NAV-24` |
| `volver` | `Back` | Valor de conjunto cerrado, catálogo de iconos | Ídem; `CMP-65`, barra de regreso |
| `alerta` | `Alert` | Valor de conjunto cerrado, catálogo de iconos | Ídem; `CMP-25` y `CMP-27` |

#### 6.12.4 Nombres de clase CSS y de animación (168)

**Por qué llevan fila.** Una clase CSS la lee una herramienta —el navegador—, de modo que cae del lado
de §3 y va en inglés. Ninguna de las ocho tablas la contaba, porque hasta la etapa `b` no había hoja
de estilos del producto. La convención de forma es **`gf-` + el concepto en inglés**, en `kebab-case`,
con `--variante` y `__parte` tal como la maqueta ya las usa; `gf-` es la abreviatura de la raíz
`GeometriaFactory` que §3 declara intocable.

**La columna castellana es la de la maqueta, y la maqueta no se renombra** (§6.11). La tabla declara
la correspondencia; es lo que hace verificable que el porte no inventó ni perdió nada.

**Ciento sesenta y ocho, contadas.** **165** son clases y llevan selector propio en
`src/GeometriaFactory.Web/wwwroot/css/app.css` —el control `C-3` de `scripts/verify-visual-system.sh`
las cuadra—; las **3** restantes son nombres de animación de `@keyframes`, que también los lee el
navegador: `gf-spin`, `gf-shimmer` y `gf-sweep`.

| Castellano (maqueta) | Inglés (producto) | Clase | Dónde está declarado el concepto |
| --- | --- | --- | --- |
| `mq-tarjeta-acceso` | `gf-access-card` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-tarjeta-acceso-modulo` | `gf-access-card-module` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-grilla-acceso` | `gf-access-grid` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-insignia` | `gf-badge` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-insignia--peligro` | `gf-badge--danger` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-insignia--info` | `gf-badge--info` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-insignia--neutro` | `gf-badge--neutral` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-insignia--exito` | `gf-badge--success` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-insignia--atencion` | `gf-badge--warning` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-banda` | `gf-banner` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-banda--confirmacion` | `gf-banner--confirmation` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-banda--error` | `gf-banner--error` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-banda--info` | `gf-banner--info` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-banda--atencion` | `gf-banner--warning` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-bloque` | `gf-block` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-body` | `gf-body` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-body-strong` | `gf-body-strong` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-btn` | `gf-btn` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-btn--destructivo` | `gf-btn--destructive` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-btn--ancho` | `gf-btn--full` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-btn--pill` | `gf-btn--pill` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-btn--primario` | `gf-btn--primary` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-btn--secundario` | `gf-btn--secondary` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-lienzo` | `gf-canvas` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-caption` | `gf-caption` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-tarjeta` | `gf-card` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-clicable` | `gf-clickable` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-columna` | `gf-column` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-columna--datos` | `gf-column--data` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-columna--escena` | `gf-column--scene` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-comentario` | `gf-comment` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-diagnostico` | `gf-diagnostics` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-dialogo` | `gf-dialog` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-disclosure` | `gf-disclosure` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-separador` | `gf-divider` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-vacio` | `gf-empty` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-campo` | `gf-field` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-campo--error` | `gf-field--error` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-campo-busqueda` | `gf-field-search` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-filtros` | `gf-filters` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-observacion` | `gf-finding` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-observacion-cuerpo` | `gf-finding-body` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-observacion-ubicacion` | `gf-finding-location` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-observaciones` | `gf-findings` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-lista-plana` | `gf-flat-list` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-lista-plana--sin-hueco` | `gf-flat-list--tight` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-fila-flex` | `gf-flex-row` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-fila-flex--envuelve` | `gf-flex-row--wrap` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-flexible` | `gf-flexible` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-acciones-pie` | `gf-footer-actions` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-grupo` | `gf-group` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-grupo-recuento` | `gf-group-count` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-grupo-cabecera` | `gf-group-header` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-ico` | `gf-icon` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-ico--16` | `gf-icon--16` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-ico--20` | `gf-icon--20` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-ico--24` | `gf-icon--24` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-ico-contenedor` | `gf-icon-holder` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-ico-contenedor--exito` | `gf-icon-holder--success` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-identidad` | `gf-identity` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-sangria-2` | `gf-indent-2` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-barra-indeterminada` | `gf-indeterminate-bar` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-inerte` | `gf-inert` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-iniciales` | `gf-initials` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-input` | `gf-input` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-json-cuenta` | `gf-json-count` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-json-clave` | `gf-json-key` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-json-numero` | `gf-json-number` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-json-otro` | `gf-json-other` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-json-cadena` | `gf-json-string` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-justificar-inicio` | `gf-justify-start` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-clave-valor` | `gf-key-value` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-margen-bloque-5` | `gf-margin-block-5` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-margen-arriba-1` | `gf-margin-top-1` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-margen-arriba-2` | `gf-margin-top-2` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-mb-4` | `gf-mb-4` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-mb-5` | `gf-mb-5` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-meta` | `gf-meta` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-ml-auto` | `gf-ml-auto` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-mt-2` | `gf-mt-2` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-mt-3` | `gf-mt-3` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-mt-4` | `gf-mt-4` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-mt-5` | `gf-mt-5` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-mt-6` | `gf-mt-6` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-mt-7` | `gf-mt-7` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-mt-9` | `gf-mt-9` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-mt-auto` | `gf-mt-auto` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-atenuado` | `gf-muted` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-nav` | `gf-nav` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-lista-anidada` | `gf-nested-list` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-sin-margen` | `gf-no-margin` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-nodo` | `gf-node` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-nodo--rama` | `gf-node--branch` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-nodo--hoja` | `gf-node--leaf` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-nodo-flecha` | `gf-node-arrow` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-nodo-indice` | `gf-node-index` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-num` | `gf-num` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-texto-original` | `gf-original-text` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-relleno-4` | `gf-padding-4` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-encabezado` | `gf-page-header` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-pieza` | `gf-piece` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-pieza-cuerpo` | `gf-piece-body` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-pieza-cuerpo--contorno` | `gf-piece-body--outline` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-pieza-etiqueta` | `gf-piece-label` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-cartel-reconexion` | `gf-reconnect-notice` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-requisito` | `gf-requirement` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-acciones-fila` | `gf-row-actions` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-tarjeta-fila` | `gf-row-card` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-tarjeta-fila-cabecera` | `gf-row-card-header` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-escena` | `gf-scene` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-escena-lienzo` | `gf-scene-canvas` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-escena-leyenda` | `gf-scene-caption` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-escena-rotulo` | `gf-scene-label` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-escena-movimiento` | `gf-scene-motion` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-escena-movimiento__nota` | `gf-scene-motion__note` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-escena-movimiento__opcion` | `gf-scene-motion__option` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-caja-desplazable` | `gf-scroll-box` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-sello` | `gf-seal` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-sello-boton` | `gf-seal-button` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-select` | `gf-select` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-shell` | `gf-shell` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-shell-contenido` | `gf-shell-content` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-shell-sidebar` | `gf-shell-sidebar` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-brillo` | `gf-shimmer` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-sidebar-pie` | `gf-sidebar-footer` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-sidebar-persona` | `gf-sidebar-person` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-sidebar-papel` | `gf-sidebar-role` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-esqueleto` | `gf-skeleton` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-esqueleto--bloque` | `gf-skeleton--block` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-esqueleto--campo` | `gf-skeleton--field` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-esqueleto--fila` | `gf-skeleton--row` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-esqueleto--escena` | `gf-skeleton--scene` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-esqueleto--alto` | `gf-skeleton--tall` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-skip` | `gf-skip-link` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-giro` | `gf-spin` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-spinner` | `gf-spinner` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-sr-only` | `gf-sr-only` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-tarjetas-apiladas` | `gf-stacked-cards` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-subtitulo` | `gf-subtitle` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-recorrido` | `gf-sweep` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-tabla` | `gf-table` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-tabla-envoltorio` | `gf-table-wrapper` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-td-acciones` | `gf-td-actions` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-textarea` | `gf-textarea` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-textarea--media` | `gf-textarea--medium` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-textarea--corta` | `gf-textarea--short` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-textarea--texto-trabajo` | `gf-textarea--work-text` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-th-plano` | `gf-th-plain` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-th-plano--cuerpo` | `gf-th-plain--body` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-th-plano--breve` | `gf-th-plain--brief` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-th-plano--fuerte` | `gf-th-plain--strong` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-title` | `gf-title` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-arbol` | `gf-tree` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-arbol-hijos` | `gf-tree-children` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-dos-columnas` | `gf-two-columns` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-dos-columnas--envio` | `gf-two-columns--submission` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-aviso-indisponible` | `gf-unavailable-notice` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-valores` | `gf-values` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-ancho-30` | `gf-width-30` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-ancho-34` | `gf-width-34` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-ancho-38` | `gf-width-38` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-ancho-40` | `gf-width-40` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-ancho-45` | `gf-width-45` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-ancho-46` | `gf-width-46` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-ancho-campo` | `gf-width-field` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-ancho-medio` | `gf-width-medium` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-ancho-corto` | `gf-width-short` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |
| `mq-ancho-ancho` | `gf-width-wide` | Clase CSS | `Estilos-Maqueta.css` de la maqueta aprobada; el componente que la usa está en `Linea-Base-Visual.md` §3 |

**Las dieciséis que NO se portaron, y por qué.** `mq-panel-fachada`, `mq-panel-fachada__rotulo`,
`mq-fa-acciones`, `mq-fa-bitacora`, `mq-fa-linea`, `mq-fa-propiedades`, `mq-fa-recuentos`,
`mq-sello-maqueta`, `mq-barra-validacion`, `mq-barra-validacion__fila`, `mq-barra-validacion__rotulo`,
`mq-nota`, `mq-conmutador`, `mq-portada`, `mq-lista-superficies` y `mq-prosa`. Las catorce primeras
—incluidas `mq-nota` y `mq-conmutador`, que sólo tienen regla **dentro** del bloque de la barra de
validación— y las dos de la portada son las clases de los tres bloques que `Linea-Base-Visual.md` §6
declara **instrumento de la maqueta y no producto**. No tienen nombre inglés porque no tienen
concepto de producto que nombrar.

### 6.13 Agregados por la etapa `c`, fuera de los 155

**Por qué existe esta sección.** La etapa `c` construye la rebanada vertical de la identidad del
administrador y de la sesión —capacidades `F-01` y `F-05`— y al hacerlo escribe los primeros
identificadores de **dominio, aplicación, contratos, infraestructura y superficie HTTP** del
producto. Entran por el **corolario 4 de §6.1**, con el mismo criterio con el que la 1.5 agregó
las cinco filas de la etapa `a` y la 1.6 las 214 de la etapa `b`: **no cuentan dentro de los
155**, porque no existían cuando se contaron las seis clases.

**Y hay una ausencia que conviene declarar, porque es la que prueba que el glosario sirvió:
NINGÚN CÓDIGO DE CONDICIÓN LLEVA FILA NUEVA.** Los veinte códigos que la etapa `c` escribe
—doce del dominio, dos propios de la aplicación, uno de infraestructura y ocho del contrato,
con solapamiento— **ya estaban los veinte** en §6.8, con su nombre inglés fijado por `F-03`. La
etapa `c` los tomó de ahí y no tradujo ninguno por criterio propio, que es exactamente lo que
§6.1 manda.

**109 filas, contadas:** 30 tipos, 78 miembros y propiedades, y 1 subsegmento de espacio de nombres. **Las tres últimas las agrega la 1.8**, con la rebanada del cambio forzado de contraseña que `PRODUCT-INTAKE` **1.34** hizo alcanzable, y llevan su marca en la tabla. **Dos de esas tres quedan retiradas por la 1.10** —`PasswordChangeEmail` y `BeginPasswordChange`—, y **conservan su fila con el motivo del retiro**, que es lo que esta norma manda con un identificador retirado: §4.1 declara intocable el registro de lo que hubo y §6.8.5 lo hace con los cuatro códigos internos retirados, por la misma razón —para que una cita vieja resuelva contra la tabla y no contra el criterio de quien la lea—. **El recuento no se mueve**: retirar no borra la fila, y §6.13.2 sigue teniendo 78.

**Tres nombres que la etapa `c` NO agrega porque ya tenían fila**, y se declara para que el
control `V-1` no los levante como huecos: `Account`, `Role` y `AccountStatus` están en §6.4 con
sus valores en §6.7; `EfCoreAccountRepository`, `UtcSystemClock` y `AccountConfiguration` están
en §6.4; `IAccountRepository` está en §6.3; y los dieciocho subsegmentos de espacio de nombres
que la etapa usa están en §6.10.

#### 6.13.1 Tipos (30)

| Castellano | Inglés | Clase | Dónde está declarado el concepto |
| --- | --- | --- | --- |
| Resultado de dominio | `DomainResult` / `DomainResult<TValue>` | Tipo, guarda del dominio | `Domain ADR-05`; `Domain CU-12` §4 y `CU-03` §7, que devuelven aplicado o rechazado con motivo |
| Admisibilidad | `Admission` | Tipo, guarda del dominio | `Domain CU-04`, que evalúa si la cuenta admite acceso y con qué motivo si no |
| Código de condición | `ConditionCode` | Tipo, catálogo de códigos del dominio | Catálogo de `GeometriaFactory-Domain` §3; §6.8.1 declara los 42 códigos que contiene |
| Identidad por correo | `EmailIdentity` | Tipo, valor del dominio | `Infrastructure ADR-03`; `Modelo-Datos-Logico.md` §2.1, que declara la forma normalizada como la que decide la identidad |
| Resultado de aplicación | `ApplicationResult` / `ApplicationResult<TValue>` | Tipo, resultado de la capa de aplicación | Catálogo de `GeometriaFactory-Application` §3, que da a cada caso de uso un desenlace con motivo |
| Identidad de la cuenta | `AccountIdentity` | Tipo, resultado de la capa de aplicación | `Application CU-04` §4, que devuelve identificador, correo y papel |
| Comprobación de credencial | `CredentialCheck` | Tipo, conjunto cerrado de la capa de aplicación | `Infrastructure CU-06`, que declara los tres desenlaces: coincide, no coincide y valor ilegible |
| Código de condición de aplicación | `ApplicationConditionCode` | Tipo, catálogo de códigos propios de la capa de aplicación | Catálogo de `GeometriaFactory-Application` §7.1; §6.8.2 declara los 12 propios |
| Código de condición de infraestructura | `InfrastructureConditionCode` | Tipo, catálogo de códigos de infraestructura visibles desde la aplicación | Catálogo de `GeometriaFactory-Infrastructure` §7.1; §6.8.3 declara los 15 propios |
| Caso de uso de configurar el administrador | `ConfigureAdministratorUseCase` | Tipo, caso de uso | `Application CU-10` |
| Caso de uso de resolver el ingreso | `ResolveSignInUseCase` | Tipo, caso de uso | `Application CU-04` |
| Caso de uso de cambiar la contraseña propia | `ChangeOwnPasswordUseCase` | Tipo, caso de uso | `Application CU-03` |
| Solicitud de configuración del administrador | `AdministratorSetupRequest` | Tipo, contrato de solicitud | `Contracts CU-02` FA-03; `Definicion-Superficie-HTTP.md` §3, punto `A-03` |
| Respuesta de configuración de cuenta | `AccountSetupResponse` | Tipo, contrato de respuesta | `Definicion-Superficie-HTTP.md` §3, punto `A-03`, que responde `201` |
| Solicitud de canje de credenciales | `CredentialExchangeRequest` | Tipo, contrato de solicitud | `Contracts CU-01` §4 paso 1 |
| Respuesta de sesión | `SessionResponse` | Tipo, contrato de respuesta | `Contracts CU-01` §4 paso 4 y `CA-01` |
| Solicitud de cambio de la contraseña propia | `OwnPasswordChangeRequest` | Tipo, contrato de solicitud | `Contracts CU-02` FA-02; `PRODUCT-INTAKE` 1.13, que la deja como tipo único de las tres situaciones |
| Código de error del contrato | `ErrorCode` | Tipo, conjunto cerrado de códigos del contrato | `Contracts CU-06`; §6.8.6 declara los 21 `CONTRATO_*` |
| Detalle del error | `ErrorDetail` | Tipo, contrato de respuesta | `Contracts CU-06` §4 paso 3 |
| Respuesta de error | `ErrorResponse` | Tipo, contrato de respuesta | `Contracts CU-06` `CA-01`, que le fija cuatro campos |
| Derivación de contraseña | `PasswordDerivation` | Tipo, mecanismo de infraestructura | `Infrastructure CU-06` y `ADR-04` |
| Opciones de firma | `SigningOptions` | Tipo, configuración de infraestructura | `Infrastructure ADR-04` §2 puntos 3 y 5; intake §17.3.P.5 |
| Emisor del acceso firmado | `AccessTokenIssuer` | Tipo, mecanismo de infraestructura | `Infrastructure CU-08` y `ADR-04` §2 punto 4 |
| Puntos de autenticación | `AuthenticationEndpoints` | Tipo, agrupador de puntos de acceso | `Api CU-01`; `Definicion-Superficie-HTTP.md` §3, punto `A-01` |
| Puntos de cuenta | `AccountEndpoints` | Tipo, agrupador de puntos de acceso | `Api CU-03`; `Definicion-Superficie-HTTP.md` §3, puntos `A-03` y `A-05` |
| Traducción al contrato | `ContractTranslation` | Tipo, traducción de la capa que expone | `Api CU-09` y `ADR-04`; `Definicion-Superficie-HTTP.md` §5 |
| Traducción | `Translation` | Tipo anidado en `ContractTranslation` | `Definicion-Superficie-HTTP.md` §5, que declara el recorrido de dos traducciones |
| Estado de la sesión | `SessionState` | Tipo, servicio de la pieza pública | `Web ADR-03`, que ubica la credencial en el estado del circuito |
| Desenlace del servicio de datos | `DataServiceOutcome<TValue>` | Tipo, resultado de la pieza pública | `Web CU-10`; `Contracts CU-06` FA-02, que declara que el error de transporte lo produce la propia pieza pública |
| Andamiaje del servicio de datos | `DataServiceHarness` | Tipo, andamiaje de la batería de pruebas | Intake §17.5.P.6, que exige golpear la aplicación real por HTTP. **[propuesta de la etapa `c`]** |

**Los nombres de los tipos de prueba no llevan fila, y es una regla y no un olvido.** Una clase
de prueba se llama como el tipo que ejercita más el sufijo `Tests` —`AccountTests`,
`AccountUseCaseTests`, `SessionCredentialTests`—: no nombra ningún concepto nuevo, nombra al que
ya tiene fila. La única excepción es `DataServiceHarness`, que sí nombra un concepto propio —el
andamiaje que levanta la pieza de datos en memoria— y por eso está en la tabla.

#### 6.13.2 Miembros y propiedades (78)

| Castellano | Inglés | Clase | Dónde está declarado el concepto |
| --- | --- | --- | --- |
| Identificador | `Id` | Propiedad de `Account` y de `AccountIdentity` | `Definicion-Modelo-De-Dominio.md` §2.1 |
| Correo | `Email` | Propiedad de `Account`, `AccountIdentity`, los contratos de cuenta y `ForcedChangeForm` | `Definicion-Modelo-De-Dominio.md` §2.1; `Wireframes-Credencial-Propia.md` §2, tercer esquema **[el alcance se amplía en la 1.10, con el campo de correo de la pantalla del cambio forzado; es el mismo concepto y por eso no lleva fila nueva]** |
| Correo normalizado | `NormalizedEmail` | Propiedad de `Account` | `Modelo-Datos-Logico.md` §2.1; `INV-01` |
| Nombre | `FirstName` | Propiedad de `Account` y de `AdministratorSetupRequest` | `Definicion-Modelo-De-Dominio.md` §2.1 |
| Apellido | `LastName` | Propiedad de `Account` y de `AdministratorSetupRequest` | `Definicion-Modelo-De-Dominio.md` §2.1 |
| Papel | `Role` | Propiedad de `Account`, `AccountIdentity` y los contratos de cuenta | `Definicion-Modelo-De-Dominio.md` §2.1; `RN-01` |
| Situación | `Status` | Propiedad de `Account` | `Definicion-Modelo-De-Dominio.md` §2.1; `RN-06` |
| Credencial derivada | `PasswordHash` | Propiedad de `Account` | Intake §17.1.P.5, que prohíbe la contraseña en claro en el dominio |
| Marca de cambio de contraseña pendiente | `MustChangePassword` | Propiedad de `Account` | `INV-09`; `RN-13`, `RN-14`, `RN-16` |
| Momento de alta | `CreatedAt` | Propiedad de `Account` | `Modelo-Datos-Logico.md` `RC-06` |
| Constituir el administrador | `ConfigureAdministrator` | Operación de `Account` | `Domain CU-12` |
| Reemplazar la credencial | `ReplaceCredential` | Operación de `Account` | `Domain CU-03` FA-01 y FA-04 |
| Evaluar la admisibilidad | `EvaluateAdmission` | Operación de `Account` | `Domain CU-04` |
| Se aplicó | `Succeeded` | Miembro de `DomainResult`, `ApplicationResult` y `DataServiceOutcome` | `Domain ADR-05` |
| Motivo | `ConditionCode` | Miembro de `DomainResult` y de `ApplicationResult` | `Domain ADR-05` |
| Valor | `Value` | Miembro de `DomainResult<TValue>`, `ApplicationResult<TValue>` y `DataServiceOutcome<TValue>` | `Domain ADR-05` |
| Aplicado | `Applied` | Constructor con nombre de `DomainResult` y de `ApplicationResult` | `Domain ADR-05` |
| Rechazado | `Rejected` | Constructor con nombre de `DomainResult` y de `ApplicationResult` | `Domain ADR-05` |
| Admite | `IsAdmissible` | Miembro de `Admission` | `Domain CU-04` §4 |
| Motivo de la no admisión | `Reason` | Miembro de `Admission` | `Domain CU-04` §4 |
| Admisible | `Admissible` | Constructor con nombre de `Admission` | `Domain CU-04` §4 |
| No admisible | `NotAdmissible` | Constructor con nombre de `Admission` | `Domain CU-04` §5 |
| Normalizar | `Normalize` | Operación de `EmailIdentity` | `Infrastructure ADR-03` |
| Recuperar por correo normalizado | `FindByNormalizedEmailAsync` | Miembro de `IAccountRepository` | `Application ADR-02` §3.1 |
| Recuperar por identificador | `FindByIdAsync` | Miembro de `IAccountRepository` | `Application ADR-02` §3.1 |
| Existe administrador | `AdministratorExistsAsync` | Miembro de `IAccountRepository` | `Application ADR-02` §2 punto 1; `RN-01` |
| Correo ya registrado | `EmailIsRegisteredAsync` | Miembro de `IAccountRepository` | `Application ADR-02` §2 punto 1; `RN-02` |
| Materializar el alta | `AddAsync` | Miembro de `IAccountRepository` | `Application ADR-02` §3.1 |
| Materializar el cambio | `UpdateAsync` | Miembro de `IAccountRepository` | `Application ADR-02` §3.1 |
| Ejecutar | `ExecuteAsync` | Operación única de los tres casos de uso | Catálogo de `GeometriaFactory-Application` §3 |
| Ejecutar con la contraseña actual | `ExecuteWithCurrentCredentialAsync` | Segunda operación de `ChangeOwnPasswordUseCase` | `PRODUCT-INTAKE` **1.34**, que declara las **dos formas de autenticarse** del cambio de contraseña: con sesión de trabajo y con la contraseña actual. **[agregado por la 1.8]** |
| Coincide | `Matches` | Valor de `CredentialCheck` | `Infrastructure CU-06` §4 |
| No coincide | `DoesNotMatch` | Valor de `CredentialCheck` | `Infrastructure CU-06` §5 |
| Ilegible | `Unreadable` | Valor de `CredentialCheck` | `Infrastructure CU-06` §5; `ADR-04` §2 punto 1 |
| Identificador de la cuenta | `AccountId` | Miembro de `AccountSetupResponse` y de `SessionResponse` | `Contracts CU-01` `CA-01` |
| Credencial de sesión | `AccessToken` | Miembro de `SessionResponse` | `Contracts CU-01` `CA-01` |
| Contraseña | `Password` | Miembro de `CredentialExchangeRequest` y de `AdministratorSetupRequest` | `Contracts CU-01` §4 paso 1 |
| Contraseña vigente | `CurrentPassword` | Miembro de `OwnPasswordChangeRequest` | `Contracts CU-02` FA-02; `RN-16` |
| Contraseña nueva | `NewPassword` | Miembro de `OwnPasswordChangeRequest` y de `ForcedChangeForm` | `Contracts CU-02` FA-02 **[el alcance se amplía en la 1.10]** |
| Código | `Code` | Miembro de `ErrorResponse` | `Contracts CU-06` `CA-01` |
| Texto | `Message` | Miembro de `ErrorResponse` | `Contracts CU-06` `CA-01` |
| Detalles | `Details` | Miembro de `ErrorResponse` | `Contracts CU-06` `CA-01` |
| Momento | `OccurredAt` | Miembro de `ErrorResponse` | `Contracts CU-06` `CA-01` |
| Campo | `Field` | Miembro de `ErrorDetail` | `Contracts CU-06` §4 paso 3 |
| Índice de figura | `FigureIndex` | Miembro de `ErrorDetail` | `Contracts CU-06` §4 paso 3; `RN-09` |
| Derivar | `Derive` | Operación de `PasswordDerivation` | `Infrastructure CU-06` §4 |
| Comprobar | `Verify` | Operación de `PasswordDerivation` | `Infrastructure CU-06` §4 |
| Función anclada | `AnchoredFunction` | Miembro de `PasswordDerivation` | `Infrastructure ADR-04` §6 punto 1. **[decisión de la etapa `c`: PBKDF2 sobre SHA-256]** |
| Iteraciones ancladas | `AnchoredIterations` | Miembro de `PasswordDerivation` | `Infrastructure ADR-04` §7. **[propuesta de la etapa `c`]** |
| Emitir | `Issue` | Operación de `AccessTokenIssuer` | `Infrastructure CU-08` §4 |
| Parámetros de verificación | `ValidationParameters` | Miembro de `AccessTokenIssuer` | `Api CU-02` §4 |
| Hay clave de firma | `SigningKeyIsProvided` | Miembro de `AccessTokenIssuer` | `Infrastructure ADR-04` §2 punto 3 |
| Longitud mínima de la clave de firma | `MinimumSigningKeySizeInBytes` | Miembro de `AccessTokenIssuer` | **[propuesta de la etapa `c`: ninguna fuente da longitud]** |
| Reclamo del papel | `RoleClaim` | Miembro de `AccessTokenIssuer` | `Infrastructure ADR-04` §2 punto 4 |
| Clave de firma | `SigningKey` | Miembro de `SigningOptions` | Intake §17.3.P.5 y §17.5.P.5 |
| Vigencia en minutos | `LifetimeInMinutes` | Miembro de `SigningOptions` | `Infrastructure ADR-04` §2 punto 5. **[propuesta de la etapa `c`: ocho horas]** |
| Emisor | `Issuer` | Miembro de `SigningOptions` | `Infrastructure ADR-04` §2 punto 4 |
| Audiencia | `Audience` | Miembro de `SigningOptions` | `Infrastructure ADR-04` §2 punto 4 |
| Nombre de la sección | `SectionName` | Miembro de `SigningOptions` | `Infrastructure ADR-04` §2 punto 3 |
| Mapear los puntos de autenticación | `MapAuthenticationEndpoints` | Operación de `AuthenticationEndpoints` | `Definicion-Superficie-HTTP.md` §3, punto `A-01` |
| Mapear los puntos de cuenta | `MapAccountEndpoints` | Operación de `AccountEndpoints` | `Definicion-Superficie-HTTP.md` §3, puntos `A-03` y `A-05` |
| Identificador de cuenta del acceso | `AccountIdOf` | Operación de `AuthenticationEndpoints` | `Api CU-02` §7 |
| Traducir | `Translate` | Operación de `ContractTranslation` | `Definicion-Superficie-HTTP.md` §5 |
| Problema | `Problem` | Operación de `ContractTranslation` | `Definicion-Superficie-HTTP.md` §5 y §6 |
| Hay sesión | `IsOpen` | Miembro de `SessionState` | `Web ADR-03` |
| Es administrador | `IsAdministrator` | Miembro de `SessionState` | `Experiencia-De-Uso.md` §3.2, que reparte destinos por papel |
| Abrir | `Open` | Operación de `SessionState` | `Web CU-02` §4 paso 6 |
| Cerrar | `Close` | Operación de `SessionState` | `Web CU-02`; `Experiencia-De-Uso.md` §3.2, cierre de sesión |
| Usar la credencial | `UseAccessToken` | Operación de `SessionState` | `Web ADR-03`. **[propuesta de la etapa `c`: es método y no propiedad, para que no sea interpolable en el marcado]** |
| Correo de la cuenta derivada al cambio forzado | `PasswordChangeEmail` | Miembro de `SessionState`, **retirado por imposibilidad de su mecanismo** | `RN-13`, que deriva al cambio sin emitir sesión de trabajo; `Wireframes-Credencial-Propia.md` §4, «Llegar al cambio forzado». **[agregado por la 1.8; retirado por la 1.10]**: `SessionState` tiene alcance de **petición**, el ingreso es estático y la redirección al cambio abre una petición nueva, con lo cual el correo anotado llegaba siempre en nulo. **No se recicla.** |
| Anotar el desvío al cambio forzado | `BeginPasswordChange` | Operación de `SessionState`, **retirada por imposibilidad de su mecanismo** | `PRODUCT-INTAKE` **1.34**; `Wireframes-Credencial-Propia.md` §4. **[propuesta de la 1.8; retirada por la 1.10]**: anotaba lo que la petición siguiente no podía leer. La pantalla del cambio forzado pide el correo ella misma. **No se recicla.** |
| Resuelto | `Resolved` | Constructor con nombre de `DataServiceOutcome` | `Web CU-10` |
| Fallido | `Failed` | Constructor con nombre de `DataServiceOutcome` | `Web CU-10` |
| Error | `Error` | Miembro de `DataServiceOutcome` | `Contracts CU-06` FA-02 |
| Configurar el administrador | `ConfigureAdministratorAsync` | Operación de `DataServiceClient` | `Definicion-Superficie-HTTP.md` §3, punto `A-03` |
| Canjear credenciales | `ExchangeCredentialsAsync` | Operación de `DataServiceClient` | `Definicion-Superficie-HTTP.md` §3, punto `A-01` |
| Cambiar la contraseña propia | `ChangeOwnPasswordAsync` | Operación de `DataServiceClient` | `Definicion-Superficie-HTTP.md` §3, punto `A-05` |
| Puertos conectados | `ConnectedPorts` | Miembro de `CompositionRoot` | `Api` §2.1, puerta `QG-10` |

#### 6.13.3 Subsegmento de espacio de nombres (1)

**Uno solo, y los otros dieciocho ya estaban en §6.10.** La etapa `c` necesitó agrupar los tipos
del contrato de error, que ninguna de las filas de §6.10 nombraba.

| Castellano | Inglés | Clase | Dónde está declarado el concepto |
| --- | --- | --- | --- |
| `GeometriaFactory.Contracts.Errores` | `GeometriaFactory.Contracts.Errors` | Subsegmento de espacio de nombres, y la carpeta que le corresponde por §6.11 | `Contracts CU-06`, que declara el tipo único con el que un fallo cruza la frontera |

### 6.14 Agregados por la sesión por marca de navegador, fuera de los 155

**Por qué existe esta sección.** La etapa `c` dejó la sesión viviendo **sólo** en el estado del
circuito, y `Web ADR-03` §2 ya declaraba que el navegador conserva **una marca de sesión que no
transporta la credencial**. Construir esa marca —una cookie opaca, con el testigo firmado quedándose
en un almacén del lado del servidor— escribe identificadores que ninguna de las diez tablas
anteriores tenía. Entran por el **corolario 4 de §6.1**, con el mismo criterio con el que la 1.5
agregó las cinco de la etapa `a`, la 1.6 las 214 de la etapa `b` y la 1.7 las 106 de la etapa `c`:
**no cuentan dentro de los 155**, porque no existían cuando se contaron las seis clases.

**Lo que NO lleva fila, y se declara para que `V-1` no lo levante como hueco:** `Email`, `Role`,
`Password` y `AccountId` ya están en §6.13; `SessionState`, `IsOpen`, `IsAdministrator`,
`UseAccessToken`, `Open` y `Close` también. **Tampoco lo llevan los dos campos que la 1.10 escribe
en `ForcedChangeForm` y que ya son concepto listado**: el **correo** de la pantalla del cambio
forzado es `Email` de §6.13 —es la misma cosa, y el corolario 1 de §6.1 prohíbe darle un segundo
nombre— y la **contraseña nueva** es `NewPassword`, también de §6.13; las dos filas amplían su
tercera columna para nombrar el tipo nuevo, en lugar de duplicarse. `Open` y `Close` **no se retiran**: la operación cambia
de forma —pasa a necesitar el contexto de la petición para escribir y borrar la marca— y por eso
esta sección agrega `OpenAsync` y `CloseAsync` como filas propias, con el nombre viejo conservado
en §6.13 como registro de lo que hubo (§4.1, cuarta forma intocable). Y **no lleva fila el
`InvokeAsync` del intermediario**: no es un concepto del producto sino la forma que el marco
impone para escribir uno.

**37 filas, contadas:** 8 tipos y 29 miembros, propiedades y valores. Las 27 primeras las trajo
la marca de sesión (1.8); las **5 siguientes** las trae el **guardián 2 de `Web ADR-03` §2** —«ninguna
ruta del panel es accesible sin sesión»— y su **puerta de servicio de desarrollo** (1.9), que la
marca por sí sola no había cerrado: con la marca construida, las rutas del panel seguían
respondiendo sin sesión. Las **5 últimas** las trae el **arreglo del cambio forzado tras la fusión**
(1.10): la pantalla del cambio forzado pasa a enviar por **POST de verdad**, como `Ingreso`, y deja
de depender del estado de la petición anterior, con lo que escribe su propio modelo de formulario,
sus dos campos sin fila previa, su olvido de contraseñas y el nombre de formulario que el marco lee
para encaminar el envío.

#### 6.14.1 Tipos (9)

| Castellano | Inglés | Clase | Dónde está declarado el concepto |
| --- | --- | --- | --- |
| Almacén de testigos de sesión | `SessionTokenStore` | Tipo, servicio de la pieza pública, alcance de aplicación | `Web ADR-03` §2, que manda que la credencial **no** viaje al navegador, y §6.1, que acepta perderla con el reciclado del proceso |
| Valores por defecto de la marca de sesión | `SessionCookieDefaults` | Tipo, constantes de la pieza pública | Intake §17.6 (`RT` §9.2), que declara la cookie `HttpOnly`, `Secure` y `SameSite=Strict` |
| Declaraciones de la marca de sesión | `SessionClaims` | Tipo, constantes de la pieza pública | `Web ADR-03` §2: la marca lleva identidad y papel, y **no** la credencial |
| Intermediario de sesión no restablecible | `UnrestorableSessionMiddleware` | Tipo, intermediario de la pieza pública | `Web ADR-03` §6.1; `Wireframes-Ingreso.md` §5, estado «Sesión vencida o no restablecible»; `Linea-Base-Visual.md` §2, `EST-34` |
| Formulario de ingreso | `SignInForm` | Tipo anidado, modelo del formulario de `SignIn` | `Wireframes-Ingreso.md` §3, los dos campos de la tarjeta de ingreso |
| Andamiaje de la pieza pública | `PublicPieceHarness` | Tipo, andamiaje de prueba | **[propuesta de esta etapa, como `DataServiceHarness` de §6.13]**: levanta la pieza pública de verdad para mirar la cabecera real de la marca |
| Intermediario del guardián de sesión del panel | `PanelSessionGateMiddleware` | Tipo, intermediario de la pieza pública | `Web ADR-03` §2, guardián 2: «ninguna ruta del panel es accesible sin sesión», que **acota y no hace cumplir** (§6.2 de la misma ADR) |
| Formulario del cambio forzado | `ForcedChangeForm` | Tipo anidado, modelo del formulario de `OwnCredentialForcedChange` | `Wireframes-Credencial-Propia.md` §2, tercer esquema, y §5. **[agregado por la 1.10; el modelo lleva CUATRO campos y el wireframe dibuja tres: el apartamiento del cuarto campo está declarado en §2 de ese wireframe y en la cabecera del componente, y está a confirmar por el Product Owner]** |
| Arranque que exige la clave de firma | `SigningKeyStartupTests` | Tipo, andamiaje de prueba | **[propuesta de este arreglo, como `PublicPieceHarness`]**: fija que el arranque se detiene cuando la clave de firma no llegó, y no un momento después. **[agregado por la 1.11]** |

#### 6.14.2 Miembros, propiedades y valores (30)

| Castellano | Inglés | Clase | Dónde está declarado el concepto |
| --- | --- | --- | --- |
| Esquema de la marca de sesión | `Scheme` | Miembro de `SessionCookieDefaults` | Intake §17.6 (`RT` §9.2) |
| Nombre de la marca de sesión | `CookieName` | Miembro de `SessionCookieDefaults` | Intake §17.6 (`RT` §9.2). **[decisión de esta etapa: el valor no nombra la tecnología que la emite]** |
| Identificador de sesión | `SessionId` | Miembro de `SessionClaims`, y propiedad de `SessionState` | `Web ADR-03` §2: la marca es un identificador **opaco**, no el testigo |
| Guardar el testigo | `Keep` | Operación de `SessionTokenStore` | `Web ADR-03` §7, custodia del lado del servidor |
| Buscar el testigo | `Find` | Operación de `SessionTokenStore` | Ídem |
| Descartar el testigo | `Discard` | Operación de `SessionTokenStore` | `Web CU-02`, cierre de sesión |
| Está guardado | `Contains` | Operación de `SessionTokenStore` | `Web ADR-03` §6.1, que es el caso en que **no** está |
| Vaciar el almacén | `Clear` | Operación de `SessionTokenStore` | `Web ADR-03` §6.1. **[propuesta de esta etapa: el reciclado del proceso lo hace solo; la operación existe para poder ejercitarlo en prueba]** |
| Cargar la identidad de la marca | `LoadAsync` | Operación de `SessionState` | `Web ADR-03` §2 |
| Abrir la sesión | `OpenAsync` | Operación de `SessionState` | `Web CU-02` §4 paso 6. Reemplaza a `Open` de §6.13 |
| Cerrar la sesión | `CloseAsync` | Operación de `SessionState` | `Web CU-02`; `Experiencia-De-Uso.md` §3.2, cierre de sesión. Reemplaza a `Close` de §6.13 |
| Datos escritos en el formulario | `Input` | Propiedad de `SignIn` y de `OwnCredentialForcedChange`, ligada al envío del formulario | `Wireframes-Ingreso.md` §3; `Wireframes-Credencial-Propia.md` §2, tercer esquema **[el alcance se amplía en la 1.10]** |
| Enviar el formulario | `SubmitAsync` | Operación de `SignIn` y de `OwnCredentialForcedChange` | `Wireframes-Ingreso.md` §4, fila «Enviar el formulario»; `Wireframes-Credencial-Propia.md` §4, fila «Guardar en el curso forzado» **[el alcance se amplía en la 1.10]** |
| Salir | `SignOutAsync` | Operación de `WorkShell` | `Experiencia-De-Uso.md` §3.2, pie de la barra lateral; `NAV-24` |
| `ingreso` | `sign-in` | Valor, nombre del formulario que el marco usa para encaminar el envío | `Linea-Base-Visual.md` §2, `SUP-03` |
| `salir` | `sign-out` | Valor, nombre del formulario que el marco usa para encaminar el envío | `Experiencia-De-Uso.md` §3.2, `NAV-24` |
| Testigos del proceso | `Tokens` | Miembro de `PublicPieceHarness` | `Web ADR-03` §6.1, que es lo que el andamiaje ejercita al vaciarlo |
| Ingreso con el motivo declarado | `SignInWithReason` | Miembro de `UnrestorableSessionMiddleware` | `Wireframes-Ingreso.md` §5: «se vuelve acá con el motivo declarado» |
| Lo que no se mira | `Untouched` | Miembro de `UnrestorableSessionMiddleware` | **[decisión de esta etapa]**: los recursos del marco y la propia superficie de ingreso, que es el destino |
| Identidad vigente | `Principal` | Propiedad de `SessionState` | `Web ADR-03` §2: identidad y papel, lo único que la marca lleva |
| Formulario vigente | `Form` | Propiedad de `SignIn` y de `OwnCredentialForcedChange` | `Wireframes-Ingreso.md` §3; `Wireframes-Credencial-Propia.md` §2, tercer esquema **[el alcance se amplía en la 1.10]** |
| Las rutas del panel | `PanelRoutes` | Miembro de `PanelSessionGateMiddleware` | `Web ADR-03` §2, guardián 2; `Experiencia-De-Uso.md` §3.2, los tres destinos por papel más el cambio de la propia contraseña |
| A dónde se desvía sin sesión | `SignInPath` | Miembro de `PanelSessionGateMiddleware` | `Web ADR-03` §2, guardián 2: el destino es `Ingreso` |
| Opción de la puerta de servicio | `WalkthroughSetting` | Miembro de `PanelSessionGateMiddleware` | **[decisión de esta etapa]**: la opción de configuración que habilita el paseo sin sesión, y que **sólo tiene efecto en el entorno de desarrollo** |
| `paseo del panel sin sesión` | `PanelWalkthroughWithoutSession` | Valor, clave de configuración que `WalkthroughSetting` nombra | **[decisión de esta etapa]**: el nombre dice qué abre y para qué, y no se lee como un interruptor de seguridad genérico. Fuera de desarrollo **no rige**, aunque esté puesta |
| Contraseña provisoria | `ProvisionalPassword` | Miembro de `ForcedChangeForm` | `Wireframes-Credencial-Propia.md` §3, fila «Campo de contraseña actual», que en el curso forzado **se rotula «Contraseña provisoria»** y no «actual». **[agregado por la 1.10]** |
| Repetición de la contraseña nueva | `NewPasswordRepeat` | Miembro de `ForcedChangeForm` | `Wireframes-Credencial-Propia.md` §3, fila «Campos de contraseña nueva y repetición», y §5, «Confirmación no coincidente». **[agregado por la 1.10]** |
| Olvidar las contraseñas escritas | `Forget` | Operación de `OwnCredentialForcedChange` | `Wireframes-Credencial-Propia.md` §7: «la pieza pública **no conserva ninguna contraseña**». **[agregado por la 1.10]** |
| `cambio de contraseña obligado` | `forced-password-change` | Valor, nombre del formulario que el marco usa para encaminar el envío | `Linea-Base-Visual.md` §2, `SUP-04`, curso de cambio forzado; misma forma que `sign-in` y `sign-out`. **[agregado por la 1.10]** |
| Componer para la prueba | `Compose` | Miembro de `SigningKeyStartupTests` | arma la composición con la configuración justa que cada prueba quiere medir. **[agregado por la 1.11]** |

### 6.15 Agregados por la etapa `d`, fuera de los 155

**Por qué existe esta sección.** La etapa `d` construye el **ciclo de vida de la cuenta de alumno**
—capacidades `F-02`, `F-03`, `F-04` y `F-26`— y al hacerlo escribe los identificadores del
auto-registro, de las cuatro operaciones del administrador, del reseteo de contraseña y de la
producción de la contraseña provisoria. Entran por el **corolario 4 de §6.1**, con el mismo
criterio con el que la 1.5 agregó las cinco filas de la etapa `a`, la 1.6 las 214 de la `b`, la
1.7 las 106 de la `c` y la 1.8 las 27 de la marca de sesión: **no cuentan dentro de los 155**,
porque no existían cuando se contaron las seis clases.

**Y la misma ausencia que declaró la etapa `c`, sobre una población mayor: NINGÚN CÓDIGO DE
CONDICIÓN LLEVA FILA NUEVA.** Los **quince** códigos que la etapa `d` escribe —siete del dominio,
tres propios de la aplicación, uno de infraestructura y cuatro del contrato— **ya estaban los
quince** en §6.8. Entre ellos, los dos casos que podrían haberse traducido mal por criterio
propio y que el glosario tenía resueltos de antemano:

- **La unificación de §6.9.** `CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA`, que el catálogo de
  `GeometriaFactory-Application` declara, y `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR`,
  que declara el de `GeometriaFactory-Domain`, llevan **el mismo nombre inglés**. La etapa `d`
  escribió **una sola constante** —`OperationNotApplicableToAdministratorAccount`, en el catálogo
  del dominio— y la capa de aplicación la **propaga** en lugar de redeclararla, que es lo que
  §6.9 y el corolario 1 de §6.1 mandan: un concepto, un nombre.
- **Los dos retirados por RN-16 de §6.8.5.** `CREDENCIAL_NO_ESTABLECIDA` y
  `RESETEO_SOBRE_CREDENCIAL_NO_FIJADA` describen causas que dejaron de ser posibles, y la etapa
  `d` **no los escribió y no los recicló**: su fila existe para que una cita vieja resuelva contra
  la tabla, no para que un identificador nuevo la ocupe.

**Los nombres que la etapa `d` NO agrega porque ya tenían fila**, y se declara para que el
control `V-1` no los levante como huecos: `Account`, `Role` y `AccountStatus` están en §6.4 con
sus valores en §6.7; `IAccountRepository` está en §6.3; `Id`, `Email`, `FirstName`, `LastName`,
`Role`, `Status`, `MustChangePassword`, `CreatedAt`, `AccountId`, `ExecuteAsync`, `Succeeded`,
`ConditionCode`, `Value`, `Applied`, `Rejected`, `Derive` y `Translate` están en §6.13.2; y
`ProvisionalPassword` está en §6.14.2 —es **el mismo concepto** que el campo de la pantalla del
cambio forzado, y el corolario 1 de §6.1 prohíbe darle un segundo nombre, de modo que esa fila
**amplía su tercera columna** en lugar de duplicarse—.

**Y una regla que se aplica igual que en §6.13: los nombres de los tipos de prueba no llevan
fila.** `AccountLifecycleTests`, `AccountLifecycleUseCaseTests`, `AccountLifecycleSurfaceTests` y
`ProvisionalPasswordTests` se llaman como lo que ejercitan y no nombran ningún concepto nuevo.
**Tampoco llevan fila las constantes de ruta** —`RegistrationRoute`, `AccountsRoute`,
`StatusRoute`, `DeletionRoute`, `PasswordResetRoute`—, por el mismo criterio con el que la etapa
`c` dejó fuera `TokenRoute`, `AdministratorSetupRoute` y `OwnPasswordRoute`: la ruta es una
derivación de `Definicion-Superficie-HTTP.md` §3 y no un concepto del dominio.

**36 filas, contadas:** 16 tipos y 20 miembros y propiedades.

#### 6.15.1 Tipos (16)

| Castellano | Inglés | Clase | Dónde está declarado el concepto |
| --- | --- | --- | --- |
| Solicitud de registro de cuenta | `AccountRegistrationRequest` | Tipo, contrato de solicitud | `Contracts CU-02` §4 paso 1 y `CA-01`, que le fija tres campos y **0 de contraseña**; `Definicion-Superficie-HTTP.md` §3, punto `A-02` |
| Respuesta de registro de cuenta | `AccountRegistrationResponse` | Tipo, contrato de respuesta | `Contracts CU-02` §4 paso 2, que declara la situación inicial de la cuenta |
| Elemento de listado de cuenta | `AccountListItem` | Tipo, contrato de respuesta | `Contracts CU-02` §4 paso 4 y `CA-05`; `Api CU-04` §4 paso 3, que le suma la marca |
| Solicitud de cambio de situación de cuenta | `AccountStatusChangeRequest` | Tipo, contrato de solicitud | `Contracts CU-02` §4 paso 5 y `CA-06`, que le fija dos campos y **0 de contraseña** (`RN-14`) |
| Resultado del cambio de situación de cuenta | `AccountStatusChangeResponse` | Tipo, contrato de respuesta | `Contracts CU-02` §4 paso 6 y `CA-02`; `RN-16`, que hace que la habilitación devuelva la provisoria |
| Solicitud de baja de cuenta | `AccountDeletionRequest` | Tipo, contrato de solicitud | `Contracts CU-02` FA-01; `RN-07`, que exige la confirmación escrita del correo |
| Solicitud de reseteo de contraseña | `PasswordResetRequest` | Tipo, contrato de solicitud | `Contracts CU-08` §4 paso 1 y `CA-01`, que le fija **un** campo |
| Resultado del reseteo de contraseña | `PasswordResetResponse` | Tipo, contrato de respuesta | `Contracts CU-08` §4 paso 2 y `CA-02` |
| Foto de la cuenta | `AccountSnapshot` | Tipo, resultado de la capa de aplicación | `Api CU-04` §4 pasos 2 y 3, que le pide el listado a la capa de aplicación con situación y marca. **[propuesta de la etapa `d`: es el complemento de `AccountIdentity`, que no lleva situación ni marca]** |
| Resultado de la credencial provisoria | `ProvisionalCredentialOutcome` | Tipo, resultado de la capa de aplicación | `Application CU-02` §7 y `CU-11` §7; `Contracts CU-02` §10, que declara que la provisoria de la habilitación y la del reseteo son **el mismo mecanismo con dos disparadores** |
| Caso de uso de registrar el alta de una cuenta | `RegisterAccountUseCase` | Tipo, caso de uso | `Application CU-01` |
| Caso de uso de gobernar las cuentas de la comisión | `GovernCommissionAccountsUseCase` | Tipo, caso de uso | `Application CU-02` |
| Caso de uso de resetear la contraseña de un alumno | `ResetStudentPasswordUseCase` | Tipo, caso de uso | `Application CU-11` |
| Productor de contraseñas provisorias | `ProvisionalPasswordFactory` | Tipo, mecanismo de infraestructura | `Infrastructure CU-07`; `ADR-05` §7, que lo declara «el único lugar donde una provisoria existe» |
| Puntos de las cuentas de la comisión | `CommissionAccountEndpoints` | Tipo, agrupador de puntos de acceso | `Api CU-04` y `CU-05`; `Definicion-Superficie-HTTP.md` §3, puntos `A-06` a `A-09` |
| Guardia del cambio de contraseña pendiente | `PendingPasswordChangeGuard` | Tipo, intermediario de la pieza de datos | `Api CU-02` §4 paso 5 y `CA-05`; `INV-09`. **[propuesta de la etapa `d`: es intermediario y no filtro por punto, porque el defecto que la guardia tiene que impedir es olvidarse de un punto]** |

#### 6.15.2 Miembros y propiedades (20)

| Castellano | Inglés | Clase | Dónde está declarado el concepto |
| --- | --- | --- | --- |
| Registrar el alta | `Register` | Operación de `Account` | `Domain CU-01`, el segundo de los dos caminos de alta del producto |
| Habilitar | `Enable` | Operación de `Account` | `Domain CU-02` §4 pasos 4 y 5; `RN-16`. **Cubre habilitar y rehabilitar, que son la misma transición** (`Contracts CU-02` FA-05) |
| Bloquear | `Block` | Operación de `Account` | `Domain CU-02` §4, tabla de transiciones |
| Admitir la baja | `AdmitDeletion` | Operación de `Account` | `Domain CU-02` FA-01; `RN-07`. **Admite o rechaza, y no elimina**: la baja es física y la ejecuta la infraestructura |
| Resetear la contraseña | `ResetPassword` | Operación de `Account` | `Domain CU-13`; `RN-12`, `RN-15` |
| Listar las cuentas | `ListAsync` | Miembro de `IAccountRepository` y operación de `GovernCommissionAccountsUseCase` | `Api CU-04` §4 paso 2, que le pide el listado a la capa de aplicación; `Definicion-Superficie-HTTP.md` §3, punto `A-06` |
| Retirar | `RemoveAsync` | Miembro de `IAccountRepository` | `Application CU-02` FA-02; `RN-07`, que hace del retiro de la cuenta y de sus trabajos **una sola unidad de trabajo** |
| Tomar la foto | `Of` | Constructor con nombre de `AccountSnapshot` | `Api CU-04` `CA-01`, que exige el listado **sin ninguna forma de la credencial** |
| Cambiar la situación | `ChangeStatusAsync` | Operación de `GovernCommissionAccountsUseCase` | `Application CU-02` §4; `Definicion-Superficie-HTTP.md` §3, punto `A-07` |
| Dar de baja | `DeleteAsync` | Operación de `GovernCommissionAccountsUseCase` | `Application CU-02` FA-02; `Definicion-Superficie-HTTP.md` §3, punto `A-08` |
| Producir | `Produce` | Operación de `ProvisionalPasswordFactory` | `Infrastructure CU-07` §4; `ADR-05` §7, que le impone **no declarar ningún parámetro** |
| Longitud | `Length` | Miembro de `ProvisionalPasswordFactory` | `Infrastructure ADR-05` §2 punto 2. **[derivación de `05-Arquitectura-Tecnica`, elevada al Product Owner en `PA-06`: doce caracteres]** |
| Alfabeto | `Alphabet` | Miembro de `ProvisionalPasswordFactory` | `Infrastructure ADR-05` §2 punto 2 y §4. **[derivación de `05-Arquitectura-Tecnica`, elevada en `PA-06`: letras y dígitos sin los pares que se confunden al dictarlos]** |
| Situación pretendida | `IntendedStatus` | Miembro de `AccountStatusChangeRequest` | `Contracts CU-02` §4 paso 5 y `CA-06` |
| Situación resultante | `ResultingStatus` | Miembro de `AccountStatusChangeResponse` | `Contracts CU-02` §4 paso 6 |
| Correo escrito como confirmación | `ConfirmationEmail` | Miembro de `AccountDeletionRequest` | `Contracts CU-02` FA-01; `RN-07`, que hace de la confirmación escrita la condición de la baja |
| Fecha de registro | `RegisteredAt` | Miembro de `AccountListItem` | `Contracts CU-02` `CA-05` |
| Mapear los puntos de las cuentas de la comisión | `MapCommissionAccountEndpoints` | Operación de `CommissionAccountEndpoints` | `Definicion-Superficie-HTTP.md` §3, puntos `A-06` a `A-09` |
| Cuenta no encontrada | `AccountNotFound` | Operación de `ContractTranslation` | `Api CU-04` §10, **adopción de causa declarada** de `CONTRATO_ALUMNO_NO_ENCONTRADO` para la cuenta que un punto de administración referencia y no existe. **[propuesta de la etapa `d`: sale de `Translate` porque el mismo motivo interno tiene dos destinos según el punto que lo produzca]** |
| Punto exento de la guardia | `ExemptEndpointName` | Miembro de `PendingPasswordChangeGuard` | `Api CU-02` FA-02 y FA-04, que declaran que la excepción **es una** y es el cambio de la propia contraseña |

## 7. Cómo se verifica esta norma

Una norma sin instrumento de verificación es una intención. Siete controles, y **la 1.4 declara cuál de ellos se verifica tal como está escrito y cuál exige que alguien lo interprete** —la tabla está debajo de las tres formulaciones completas—:

| # | Control | Cuándo | Qué detecta |
| --- | --- | --- | --- |
| `V-1` | **Recuento de identificadores fuera del glosario.** Todo identificador de código declarado en el corpus tiene que resolver contra una fila del glosario, y el glosario son **doce tablas**: §6.3 a §6.8 —las seis clases—, **§6.10** —los subsegmentos de espacio de nombres—, **§6.11** —las superficies derivadas— y las cuatro que agregaron las etapas, **§6.12** a **§6.15**. **La cifra estaba desactualizada desde la 1.6** y la corrige la 1.12: decía nueve cuando la 1.6, la 1.7 y la 1.8 ya habían agregado tres tablas al rango que §6.1 declara. §6.9 no entra: no agrega filas, declara unificaciones y homónimos de nombres que ya están en §6.4 y §6.8 | En cada auditoría de categoría 05 y en el punto de control de cada etapa | Un concepto traducido por criterio propio, que es lo que §6.1 prohíbe — **y, desde la 1.4, también el que se agregó por el corolario de §6.11, que hasta entonces caía fuera del rango que el control miraba** |
| `V-2` | **Inspección de idioma de identificador.** Ningún identificador de código nuevo en castellano | En cada emisión que declare un identificador | La reaparición de la desviación por el mismo camino por el que apareció |
| `V-3` | **Cuadre de la etiqueta.** Todo valor de conjunto cerrado tiene identificador **y** etiqueta, y la etiqueta está en castellano | Al construir `GeometriaFactory.Web.Services` | Un identificador inglés que se filtró a la pantalla |
| `V-4` | **Cuadre del renombre, contra la lista declarada de antemano.** Ver la formulación completa abajo: no es «cero ocurrencias viejas» | Al cerrar cada tramo de §8, y su primera mitad **antes de editar y antes del acto 1**, cubriendo los dos actos | Un renombre a medias, que es el modo de falla que el corpus ya mostró con la sexta función de la fachada |
| `V-5` | **Catálogo contra tipo, no contra nombre.** La prueba de inspección de `ADR-06` de `GeometriaFactory-Application` compara los códigos emitidos contra **su** catálogo, no contra el conjunto de nombres | Al construir `GeometriaFactory-Application` | Los cuatro homónimos de §6.9 leídos como si fueran el mismo código |
| `V-6` | **Cuadre de la superficie derivada.** Por cada identificador renombrado en el tramo, su carpeta y su nombre de archivo quedan en inglés, y ninguna carpeta ni archivo bajo `src/`, `tests/` o `visor/` queda en castellano — **salvo las ocurrencias que la lista previa de `V-4` declara por el quinto motivo de §4.1**, que son las mismas cinco exclusiones y se cuadran contra la misma lista. Ver la formulación completa abajo | Al cerrar cada tramo de §8, junto con `V-4` | El defecto que `R-1` mostró: el tipo renombrado y su carpeta no (§6.11) |
| `V-7` | **Coherencia interna del documento renombrado.** Todo texto que argumentaba a favor del nombre anterior queda **conservado y marcado como superado**, nunca borrado: es lo que ordena §8.2 barrido 2, porque un argumento borrado deja la decisión sin por qué. **Pasa** el argumento conservado **con su marca**; **falla** el argumento vivo, el que no la lleva. Se busca por los términos del argumento, no por el identificador: «castellano», «español», «sin tildes», «sin eñes», «tilde», «eñe», el nombre viejo escrito **fuera** de región de código, y toda comparación entre los dos idiomas. Ver la formulación completa abajo | Al cerrar el **acto 2** de cada tramo (§8.2) | El defecto que `R-1` dejó vivo: `Plan-Etapa-A.md` con sus identificadores en inglés y su §1.2 argumentando por el castellano con cinco fundamentos **sin ninguna marca** |

> **`V-4`, en su forma completa.** La 1.1 lo enunciaba como «cero ocurrencias del identificador viejo», y el ensayo `R-1` demostró que **es imposible de cumplir**: en `Plan-Etapa-A.md`, `Estado` aparece cinco veces y **dos son prosa** —el campo de cabecera del documento y una frase—. El agente cuadró el tramo separando los homónimos **a mano y sin dejar registro**, que es exactamente lo que un control mecánico existe para evitar. La formulación que rige es ésta —los puntos 1, 3 y 5 son de la 1.2; los puntos **2** y **4** los agrega la 1.3, con lo que `R-1b` encontró al aplicarla—:
>
> 1. **Antes de editar, y antes del acto 1 de §8.2**, el tramo escribe **la lista de ocurrencias que no se renombran**, ocurrencia por ocurrencia, con su documento, su línea y su motivo. Los motivos admitidos son **cinco**, y desde la 1.3 el quinto está escrito: **prosa** (§4), **cita textual**, **reporte de fuente que no se renombra**, **registro histórico** y **otro concepto con el mismo nombre** —los cuatro últimos son las cuatro formas intocables de §4.1—.
> 2. **La lista es una sola y cubre los dos actos.** Se mide **antes del acto 1**, sobre el estado previo a cualquier edición, y el acto 2 **no abre una lista nueva**: el barrido 1 de §8.2 clasifica contra ésta. Es lo que la 1.2 no ordenaba y `R-1b` tuvo que resolver por su cuenta —midió el barrido 1 antes de tocar nada, y funcionó—; medirla después del acto 1 sería medir sobre un corpus ya editado, que es la lista escrita después que el punto 5 prohíbe.
> 3. **Al cerrar**, el cuadre es: `ocurrencias viejas restantes = las de esa lista`, celda por celda, y no cero. Y en la otra dirección: `ocurrencias nuevas = candidatas medidas − las de la lista`.
> 4. **Queda fuera del cuadre, en las dos direcciones, la fila de control de cambios que el propio tramo agrega.** Al describir lo que hizo, esa fila **reintroduce el identificador viejo**, y esas ocurrencias **no podían estar en la lista previa**, porque la fila se escribe **después** de editar. La de `R-1b` en [`Plan-Etapa-A.md`](Plan-Etapa-A.md) agregó **6 ocurrencias** —una de `Configuraciones`, una de `Paginas`, una de `Internos` y tres de `visor`—: con la fila adentro el documento quedaba en **23** ocurrencias viejas contra una lista de **17**, y sin ella da **17 exactas**. La exclusión es de **esa fila y sólo de esa fila**: el resto del control de cambios sigue dentro del cuadre, y sus ocurrencias son registro histórico y entran en la lista previa como cualquier otra —en `R-1b` fueron 4—.
> 5. **Sin la lista escrita antes, el tramo no se puede cerrar.** Una lista escrita después no es un control: es una explicación de lo que salió.
>
> **Para los tramos de población grande, el cuadre es por concepto y no por cadena.** `Pendiente` nombra dos cosas —una cuenta que espera habilitación y un trabajo que espera revisión— y son **1983 ocurrencias en 351 documentos**, de las cuales **956 están en los 58 documentos que traen los dos contextos a la vez** (§2.2). Un renombre por cadena elegiría un solo nombre inglés para las dos y **destruiría prosa en cientos de documentos**: las 934 ocurrencias de «pendiente» en prosa, en 254 documentos, son intocables por §4. De modo que `R-4` cuadra **dos poblaciones separadas** —`Pending` y `Submitted`—, cada una con su lista previa, y **la suma de las dos más la lista de no renombradas tiene que dar las 1983 candidatas**. Ninguna ocurrencia puede quedar sin concepto asignado; una sola que quede, bloquea el cierre.

> **`V-6`, en su forma completa.** La 1.2 y la 1.3 lo enunciaban como «ninguna carpeta ni archivo bajo `src/`, `tests/` o `visor/` queda en castellano», **sin excepción escrita**, y esa forma **levanta como falla lo que esta misma norma declaró correcto**: la **raíz `visor/` del proyecto de código** no se renombra —§1 no reabre el nombre de los siete proyectos, y §6.11 la distingue expresamente de la carpeta `visor/` de la capa 3, que sí se renombró a `viewer/`—, y el guion `scripts/build-visor.sh` lleva esa raíz adentro del nombre por la misma razón. Son las **10 ocurrencias** que `R-1b` clasificó a mano —**7 de la raíz** y **3 del guion**, de las 17 no renombradas (§4.1)—, y con la forma anterior el control las marcaba a las diez.
>
> 1. **`V-6` admite los mismos cinco motivos que `V-4`**, y **contra la misma lista previa**: prosa (§4), cita textual, reporte de fuente que no se renombra, registro histórico y **otro concepto con el mismo nombre** (§4.1). No hay lista aparte para la superficie derivada: es una sola lista y ya cubre los dos actos (`V-4` punto 2).
> 2. **El cuadre es el de `V-4` y con la misma forma:** `carpetas y archivos con el nombre viejo restantes = los de esa lista`, celda por celda, y no cero. Lo que no esté en la lista, falla.
> 3. **Y queda fuera del cuadre la fila de control de cambios del propio tramo**, por el mismo motivo del punto 4 de `V-4`: al describir lo que hizo, esa fila reintroduce el nombre viejo. En `R-1b` fueron 6 ocurrencias, tres de ellas de `visor`.

> **`V-7`, en su forma completa.** La 1.2 y la 1.3 lo enunciaban como «ningún documento tocado conserva texto que argumenta a favor del nombre anterior», y esa forma **contradice a §8.2**, cuyo barrido 2 ordena lo contrario: el argumento **se reescribe con el fundamento nuevo, no se borra**. Con la forma anterior, un tramo que cumple §8.2 no puede cerrar `V-7` más que interpretándolo, que es lo que `R-1b` hizo. Lo que rige desde la 1.4:
>
> 1. **Pasa el argumento conservado y marcado como superado.** La marca es literal y va **en el mismo párrafo o en el encabezado de la propuesta que lo contiene**: el estado —`SUPERADA`, `Refutado`—, la **fecha**, y **la sección de esta norma que lo supera**. El tachado del texto refutado acompaña a la marca; no la reemplaza.
> 2. **Falla el argumento vivo: el que no lleva marca.** Un término del barrido 2 que aparece en un párrafo que argumenta por el nombre anterior y no trae estado, fecha y sección, no cierra el control.
> 3. **Falla también el argumento borrado.** Si el acto 2 quitó un fundamento en lugar de marcarlo, `V-7` no cierra: §8.2 lo prohíbe, y el borrado no deja rastro que el control pueda leer, sólo la diferencia contra el estado previo.
> 4. **Es lo que `R-1b` hizo, y ahora es lo que el control dice.** En [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.2 los **cinco fundamentos** de `P-1a` quedaron conservados con su estado —los cuatro primeros «sigue siendo cierto como reporte de la fuente, y ya no es fundamento», con el tramo que los renombra— y **el quinto tachado y marcado «Refutado por la norma §5.1»**. Bajo la 1.3 eso cerraba por interpretación; bajo ésta cierra por aplicación.

**Qué tan mecánico es cada control, declarado.** La 1.4 revisó los siete controles y los tres barridos de §8.2 con una sola pregunta —**¿se verifica tal como está escrito, o exige que alguien lo interprete?**—, porque sobre 4461 ocurrencias la interpretación varía. No se agregó ningún control; se declara el estado de cada uno:

| Control | Veredicto | Qué lo hace mecánico, o qué le falta |
| --- | --- | --- |
| `V-1` | **Verificable con la precisión de la 1.4** | La población es la del instrumento de §2.1 —tokens de región de código— y el rango es el glosario entero: **doce tablas desde la 1.12**. El único residuo es distinguir el identificador **declarado** del **citado**, y esa distinción ya está escrita: es la de §4.1 entre uso propio y reporte de fuente |
| `V-2` | **No verificable como está** | «Ningún identificador nuevo en castellano» no tiene forma de pasa/falla: decidir si un token es castellano exige leerlo. Haría falta el criterio de decisión escrito —resolver el token contra la columna castellana del glosario, y contra las marcas de tilde y eñe que §3 ya prohíbe—, que esta versión no agrega porque no se agregan controles nuevos |
| `V-3` | **Verificable como está** | Población cerrada de **10** valores contra las 10 filas de §6.7: para cada uno, identificador inglés y etiqueta castellana declarada. Se cuenta, no se interpreta |
| `V-4` | **Verificable como está** | El cierre es aritmético en las dos direcciones y celda por celda contra una lista escrita antes de editar, con la fila de control de cambios del propio tramo excluida. El **motivo** de cada ocurrencia lo escribe una persona, pero el control no verifica el motivo: verifica que la ocurrencia esté en la lista |
| `V-5` | **Verificable como está, y todavía no ejecutable** | Compara códigos emitidos contra el catálogo del proyecto, por tipo y no por nombre; es la prueba de inspección que `ADR-06` de `GeometriaFactory-Application` ya define. No corre hasta que exista el código |
| `V-6` | **Verificable con la precisión de la 1.4** | Cuadra contra la misma lista previa de `V-4`, con los mismos cinco motivos y la misma exclusión de la fila de control de cambios. Antes prometía «ninguna en castellano» sin excepción, y las diez ocurrencias que la norma declara correctas lo rompían |
| `V-7` | **Verificable con la precisión de la 1.4** | Pasa/falla definido sobre la **marca** —estado, fecha y sección que lo supera—, no sobre la presencia del argumento. Antes pedía lo contrario de lo que ordena §8.2 |
| §8.2 barrido 1 | **Verificable como está** | Es una búsqueda del nombre viejo fuera de región de código con el instrumento de §2.1, y toda aparición se clasifica contra la lista previa de `V-4`. Lo que no está en la lista, falla, y el criterio no depende de quién mire |
| §8.2 barrido 2 | **Verificable con la precisión de la 1.4** | La búsqueda ya era mecánica —la lista de términos está escrita—; lo que no lo era es el destino de cada hallazgo. Con `V-7` reformulado, el hallazgo cierra si el párrafo trae la marca y falla si no la trae |
| §8.2 barrido 3 | **No verificable como está** | «Las afirmaciones que la norma refutó» son **tres** y están enumeradas con la sección que las corrige, pero reconocer una afirmación dentro de un párrafo exige leerlo: no hay cadena que buscar. Haría falta, por cada afirmación refutada, **su texto literal a buscar**, escrito por la emisión que la refutó. Hoy lo cubre el barrido 2 sólo cuando la afirmación usa alguno de sus términos de idioma, y las tres no siempre lo usan |

**Los dos que quedan sin forma mecánica no bloquean a `R-2`, y conviene decir por qué.** `V-2` es un control de **emisión** y no de tramo: `R-2` no declara identificadores nuevos, los renombra contra el glosario, y ahí quien verifica es `V-1`. El **barrido 3** sí corre en `R-2` —y sobre el intake, que es donde vive la afirmación de que los nombres de la fachada estaban fijados (§5.1)—: mientras no tenga texto literal a buscar, **lo que el tramo declare como cubierto por el barrido 3 es una afirmación de quien lo ejecuta y no una medición**, y así debe escribirse en su informe.

**Y una condición previa que hay que decir:** hasta el 2026-08-12 **ninguna de las 33 auditorías del corpus verificó el idioma de un identificador**, porque ninguna invariante lo pedía. La invariante `D1` que todas ejercen dice «idioma español rioplatense neutro técnico» y se refiere **a la prosa**. `V-2` es el control que faltaba, y es la única razón por la que la desviación pudo propagarse a 334 documentos sin que nadie la nombrara.

## 8. El plan de renombre

> ### La decisión del Product Owner del 2026-08-13: los tramos que quedaban se **suspenden**
>
> **Los tramos ejecutados —`R-1`, `R-1b` y `R-2`— quedan como están.** Los que quedaban pendientes —`R-2b`, `R-3`, `R-4` y `R-5`— pasan a **suspendidos**.
>
> **El fundamento, y es el que dio el Product Owner:** esos tramos renombraban identificadores **en documentos que describen código que no existe**. Miles de ocurrencias de edición documental que no producen nada ejecutable. **El glosario de §6 ya está completo**, y con él alcanza para escribir el código en inglés **desde el primer archivo**, que es lo que la etapa `a` hace desde el 2026-08-13.
>
> **La regla que los reemplaza:**
>
> > **El glosario es la fuente de nombres para todo código nuevo.** Lo que no está en la tabla no se traduce por criterio propio: **se agrega primero**. Los documentos que describen conceptos con su nombre castellano **se actualizan cuando alguien los toca por otro motivo**, no en una tanda propia.
>
> **Qué NO cambia con la suspensión, y conviene tenerlo escrito:**
>
> 1. **§6 sigue siendo obligatorio y `V-1` y `V-2` siguen corriendo.** Suspender el renombre del corpus no afloja el idioma del código: §3 rige entero.
> 2. **El corolario 4 de §6.1 es ahora el mecanismo principal**, y no el excepcional: cada etapa que escriba un identificador sin fila **agrega la fila antes**, con su entrada en §9. La etapa `a` ya lo ejercitó con las cinco filas que §6.2 cuenta aparte.
> 3. **Los controles `V-4`, `V-6` y `V-7` quedan sin población mientras no corra ningún tramo.** No se retiran: gobiernan cualquier tramo que el Product Owner reactive, y gobiernan también la actualización por contacto de la regla de arriba cuando alcanza más de un documento.
> 4. **Un documento tocado por otro motivo se actualiza contra el glosario**, y ahí sí rigen los dos actos de §8.2: renombrar y corregir el texto que argumentaba por el nombre viejo, en la misma edición.
>
> **Dos cifras que no cuadran y que se declaran en lugar de acomodarse.** La decisión se comunicó como «los **cinco** tramos restantes» y como «**ocho mil seiscientas** ocurrencias». Contra la tabla de §8.1, los tramos pendientes son **cuatro** —`R-2b`, `R-3`, `R-4` y `R-5`; 3 ejecutados + 4 pendientes = los 7 de §8.1— y sus ocurrencias candidatas medidas suman **8260** —331 + 621 + 4461 + 2847—. Ninguna de las dos diferencias cambia la decisión ni su fundamento; se registran porque §2.1 declara que las cifras de este documento son **medidas** y no aproximadas, y porque acomodar una cifra a la frase con que se comunicó es exactamente el defecto que la 1.2 corrigió.

**Esta norma no ejecuta el renombre. Lo ordena.** El renombre es una tanda posterior y se ejecuta **contra el glosario de §6**, nunca contra el criterio de quien edita: es la única forma de que la misma cosa no termine con tres nombres, que es exactamente lo que pasaría si cada tramo tradujera por su cuenta.

**Cuatro reglas que gobiernan los siete tramos.**

1. **Un tramo, una clase, un pull request.** No se mezclan clases en un mismo tramo: el cuadre de `V-4` deja de ser mecánico si en la misma edición cambiaron dos poblaciones.
2. **Se renombra de menor a mayor alcance, salvo cuando una dependencia lo invierta**, y la inversión se declara. Los tramos baratos primero, no por comodidad, sino porque validan el procedimiento sobre una población chica antes de aplicarlo sobre cientos de documentos. **Hay una inversión y es `R-4` antes de `R-5`**: `R-4` alcanza más documentos que `R-5` —399 contra 330—, y va primero porque **23 de los 101 códigos llevan adentro el nombre inglés de un valor de conjunto cerrado** —`DESENLACE_FUERA_DE_PENDIENTE` ⟶ `OUTCOME_OUTSIDE_SUBMITTED`, `ENVIO_FUERA_DE_BORRADOR` ⟶ `SUBMISSION_OUTSIDE_DRAFT`, `CONTRATO_ALUMNO_NO_ENCONTRADO` ⟶ `STUDENT_NOT_FOUND`—, de modo que el valor se fija antes que el código que lo cita.
3. **Ningún tramo empieza si el anterior no cuadró.** `V-4` es bloqueante, y desde la 1.2 también lo son `V-6` y `V-7`.
4. **Cada tramo son dos actos y no uno**, y el segundo es tan obligatorio como el primero. §8.2 los define.

### 8.1 Los siete tramos

| # | Tramo | Qué renombra | Alcance remedido (§2.2 y §2.3) | Proyectos alcanzados | Qué se verifica al cerrarlo |
| --- | --- | --- | --- | --- | --- |
| 1 | **`R-1`** · **ejecutado el 2026-08-12** | **Los propuestos**: los 16 subsegmentos de espacio de nombres de §6.10, los 14 tipos y adaptadores de §6.4, los 6 derivados, y los 2 puertos propuestos de §6.3 — **38 identificadores**, de los cuales **16 son de los 155** | **1 documento** —[`Plan-Etapa-A.md`](Plan-Etapa-A.md)— y **80 ocurrencias candidatas**: 2 de prosa (`Estado`) y **78 renombradas**. La fila de la 1.1 declaraba «41 ocurrencias» y era la única cifra sin medir (§2.4) | Ninguno: son propuesta, nada los cita | Cerró `V-1` y `V-2`. **No cerró `V-6` ni `V-7`**, que no existían: dejó cuatro superficies derivadas sin renombrar y su §1.2 argumentando por el castellano. Ésa es la deuda que toma `R-1b` |
| 2 | **`R-1b`** · **ejecutado el 2026-08-12** | Las **4 superficies derivadas** que ninguna regla cubría —`Configuraciones/`, `Paginas/`, `visor/` de la capa 3, `Internos`— contra §6.11, **y el acto 2 de `R-1`** | **1 documento** —[`Plan-Etapa-A.md`](Plan-Etapa-A.md)—: **22 ocurrencias candidatas** medidas antes de editar, **5 renombradas** —las de superficie que §2.3 cuenta— y **17 no renombradas**, más el texto del acto 2 | Ninguno | **Cerró limpio el 2026-08-12.** `V-4` cuadró contra la lista escrita antes de editar, celda por celda y en las dos direcciones —17 no renombradas: 10 de otro concepto, 4 de registro histórico, 2 de reporte de fuente y 1 de prosa (§4.1)—; `V-6` cuadró las cuatro superficies, y las diez ocurrencias del quinto motivo se clasificaron a mano porque el control no las admitía por escrito; `V-7` cuadró la coherencia interna de §1.2 y §1.7 **interpretando el control, no aplicándolo** —conservó los cinco fundamentos con su estado y tachó el refutado, que es lo que §8.2 ordena y lo que `V-7` prohibía—. La 1.4 reformula `V-6` y `V-7` para que ese mismo resultado cierre por aplicación. **Reprodujo exactas las cifras de la 1.2.** Con eso **el ensayo queda cerrado y `R-2` habilitado**. Apartamiento declarado: los dos actos fueron en **un solo commit** (§8.2). Informe: commit `c0b8b4f` |
| 3 | **`R-2`** · **ejecutado el 2026-08-13** | **Clases 1 y 3**: los 3 puertos declarados de §6.3 y los 2 miembros de §6.5 — **5 identificadores** | **13 documentos, 64 ocurrencias candidatas**, más **3 nombres de archivo** de §2.3 | `Application`, `Infrastructure`, el intake §17.1.P.5, §17.2.P.1, §17.3.P.4 y el manifiesto | `V-4` por identificador, con su lista previa; `V-6` sobre los tres archivos. **El intake se toca acá y no antes**, porque es la fuente: §4.1 punto 1 rige, y todo reporte que dice «el intake nombra X» se renombra en este mismo tramo |
| 4 | **`R-2b`** · **SUSPENDIDO el 2026-08-13** | **Clase 2, la parte que ningún tramo tomaba**: las 5 entidades (`Cuenta`, `Trabajo`, `Pieza`, `Componente`, `Observacion`) y las 5 tablas en mayúsculas del intake (`ALUMNO`…), **unificadas por §6.9 en 5 nombres ingleses** — **10 identificadores** | **49 documentos, 331 ocurrencias candidatas**, más **5 nombres de archivo** de §2.3. La 1.1 daba las entidades en 3 documentos y 37 ocurrencias, y por eso el plan las daba por cubiertas (§2.4) | Los siete, y el intake §12, §17.3.P.4 | `V-4` por identificador. **Y un cuadre propio de la unificación**: `ALUMNO` y `Cuenta` van los dos a `Account`, así que el cuadre es `Account = ALUMNO + Cuenta − no renombradas`. §4.1 punto 2 rige fuerte acá: todo reporte de lo que `RT §7.1` llama `ALUMNO` **conserva el nombre ajeno** |
| 5 | **`R-3`** · **SUSPENDIDO el 2026-08-13** | **Clase 4**: las 6 funciones de la fachada (§6.6) | **53 documentos, 621 ocurrencias candidatas**; 21 documentos llevan las seis | `Visor` (02, 03, 05, 10), `Web` (02, 03, 05, 10), el intake §14, §17.6.P.3, §17.7.P.2, §17.7.P.3, §18 | `V-4` por función, **y el recuento de «6 de 6»**: los 21 documentos que declaran las seis tienen que seguir declarando seis. `V-6` incluye `VisorFiguras.razor` ⟶ `FigureViewer.razor` (§6.11). Es el conjunto que ya envejeció mal tres veces |
| 6 | **`R-4`** · **SUSPENDIDO el 2026-08-13** | **Clase 5**: los 10 valores de conjunto cerrado (§6.7), **con su etiqueta** | **399 documentos, 4461 ocurrencias candidatas**; sólo `Pendiente` son 351 documentos y 1983 ocurrencias, **956 de ellas en los 58 documentos que traen los dos contextos**; y 934 ocurrencias de «pendiente» **en prosa**, en 254 documentos, que no se tocan | Los siete, y el intake §4.2, §12, §17.1.P.2, §17.3.P.4 | `V-3` **en cada documento que muestre el valor a una persona**, y `V-4` **por concepto y no por cadena**: `Pending` y `Submitted` cuadran como dos poblaciones separadas, y la suma de las dos más la lista previa tiene que dar 1983 |
| 7 | **`R-5`** · **SUSPENDIDO el 2026-08-13** | **Clase 6**: los 101 códigos (§6.8) | **330 documentos, 2847 ocurrencias candidatas**; los 21 de contrato son 220 documentos y 1201 ocurrencias | Los siete. Los seis catálogos `03-UX-UI-DX/DX-Error-Messages.md` son la fuente y se renombran **primero** | `V-4` por código, `V-5` sobre los cuatro homónimos de §6.9, y la verificación en las dos direcciones de `ADR-06`. **Condición de entrada: los dos huérfanos de §6.8.7 tienen que estar resueltos**. Y §4.1 punto 3: los 4 retirados de §6.8.5 son 47 ocurrencias en 13 documentos, y las que viven en una fila de control de cambios **no se tocan** |

**Cobertura del plan, contada. Lo que sigue describe el plan tal como se emitió; desde el 2026-08-13 los cuatro tramos suspendidos no se ejecutan, y lo que cubre su población es la regla de actualización por contacto del recuadro de arriba.** Los siete tramos cubren **148 de los 155 identificadores**: 16 en `R-1`, 5 en `R-2`, **10 en `R-2b`**, 6 en `R-3`, 10 en `R-4` y 101 en `R-5`. Los **7 restantes** son los tipos de figura del dato del alumno, que **no se renombran** por la decisión declarada en §5.4 y llevan esa marca en §6.4: **148 renombrados + 7 declarados intocables = 155**, y no queda ninguno sin tramo. La 1.1 sumaba **138** y no lo decía. Fuera de los 155, el plan cubre además los 16 subsegmentos de espacio de nombres y los 6 derivados —los dos en `R-1`— y las **5 superficies derivadas** de §6.11, repartidas entre `R-1b`, `R-2`, `R-2b` y `R-3`.

**Por qué `R-2b` se llama así y no `R-3`.** Los nombres `R-1` a `R-5` ya están citados por [`Plan-Etapa-A.md`](Plan-Etapa-A.md) en su trazabilidad upstream, por §6.9 de este documento y por el informe del tramo `R-1`. Renumerar los rompería. El tramo nuevo entra **entre `R-2` y `R-3`** por las dos razones que la regla 2 pide: por **alcance**, 49 documentos está entre los 13 de `R-2` y los 53 de `R-3`; y por **dependencia**, porque las cinco entidades son la raíz de la que cuelgan los nombres que `R-1` y `R-2` ya escribieron —`IAccountRepository`, `EfCoreAccountRepository`, `AccountStatus`— y que `R-5` va a citar adentro de sus códigos.

**El orden dentro de `R-5`, porque 101 códigos en una sola pasada no se cuadran.** Cinco pasos, uno por catálogo, en orden de dependencia: `Domain` (42), después `Application` (los 12 propios), después `Infrastructure` (los 15 propios), después `Visor` (7), y **`Contracts` al final** (21), que es el único que cambia el contrato y el que arrastra a `Api` y a `Web` en la misma edición, por `RT-06`.

### 8.2 Los dos actos de cada tramo

**Renombrar un identificador y corregir el texto que argumentaba por el nombre viejo son dos actos distintos**, y la 1.1 ordenaba sólo el primero. El ensayo mostró el costo: `Plan-Etapa-A.md` quedó con sus identificadores en inglés y **con su §1.2 todavía argumentando a favor del castellano, con cinco fundamentos**, incluido uno que esta norma ya había refutado —que los nombres de la fachada «están fijados por el intake §17.7.P.3» (§5.1)—. El documento quedó **internamente contradictorio**, y ningún control lo miraba.

**Acto 1 · El renombre.** Es lo que §8.1 describe. Toca **regiones de código**: identificadores, carpetas y nombres de archivo. Cierra con `V-4` y `V-6`.

**Acto 2 · La corrección del texto que quedó argumentando por lo anterior.** Toca **prosa**, y sólo prosa. Cierra con `V-7`.

**Cómo se detecta el texto que quedó argumentando, y son tres barridos mecánicos sobre los documentos que el acto 1 tocó** —nunca sobre el corpus entero, porque el resto no cambió—:

| # | Barrido | Qué busca | Qué se hace con lo que aparece |
| --- | --- | --- | --- |
| 1 | **El identificador viejo fuera de región de código** | El nombre castellano escrito en prosa, sin acentos graves, en un documento donde el acto 1 ya lo renombró | Se clasifica por §4.1 **contra la lista previa de `V-4`, que ya lo cubre**: esa lista se escribe antes del acto 1 y alcanza a los dos actos, así que todo lo que aparezca acá ya tiene motivo escrito. Si es uso propio, se reescribe. Si es cita, reporte, registro histórico u otro concepto con el mismo nombre, **se deja**. Si aparece una ocurrencia que **no** está en la lista, `V-4` no cuadra y el tramo no cierra: la lista previa era incompleta, que es justo el defecto que el control existe para detectar |
| 2 | **Los términos del argumento de idioma** | `castellano`, `español`, `inglés`, `tilde`, `tildes`, `eñe`, `eñes`, `sin tildes`, `sin eñes`, `idioma`, y toda comparación entre los dos idiomas | Se lee el párrafo entero. Si argumenta por el nombre anterior, se reescribe **con el fundamento nuevo y con la marca que `V-7` exige** —estado, fecha y sección de esta norma que lo supera—, y **no se borra**: un argumento borrado deja la decisión sin por qué, y desde la 1.4 tampoco cierra `V-7` |
| 3 | **Las afirmaciones que la norma refutó** | Las que §3 y §5 corrigieron por escrito: que los nombres de la fachada estaban fijados (§5.1), que escribir sin tildes ni eñes era un costo inevitable (§3), que la forma calificada de `Pendiente` es permanente (§5.2) | Se reescriben citando la sección de esta norma que las corrige, para que la corrección quede trazable |

**Cuándo se ejecuta el acto 2.** **Dentro del mismo tramo y antes de cerrarlo**, y **nunca en un tramo posterior**: un documento contradictorio consultado entre dos tramos propaga el argumento viejo, que es el camino por el que la desviación llegó a 334 documentos sin que nadie la nombrara (§7).

**Y la lista previa de `V-4` se mide antes del acto 1, no antes de cada acto.** Es una sola lista y cubre los dos: el barrido 1 clasifica contra ella y no abre otra. La 1.2 no lo ordenaba —pedía la lista «antes de editar» y ponía el acto 2 después del acto 1, que son dos exigencias que no cierran juntas—; `R-1b` lo resolvió midiendo el barrido 1 antes de tocar nada, y funcionó. El punto 2 de `V-4` en §7 lo ordena desde la 1.3.

**En cuántos commits, y la 1.3 acota la regla.** La 1.2 pedía los dos actos en **commits separados, siempre**, «para que el cuadre de `V-4` siga siendo mecánico». `R-1b` fue en **un solo commit** y lo declaró como **apartamiento**, con este motivo: los cambios de los dos actos vivían en el mismo archivo —[`Plan-Etapa-A.md`](Plan-Etapa-A.md)— y separarlos después de hecho exigía reeditar el documento, que es más riesgo que el que la separación evita. ~~**La regla se acota, y el fundamento es el instrumento**: §2.1 cuenta tokens **dentro de regiones de código**, y el acto 2 toca **prosa y sólo prosa**; sobre un mismo archivo, entonces, el acto 2 **no puede mover ninguna cifra de `V-4`**, y separarlo no compraba ahí ninguna garantía mecánica —compraba legibilidad del diff, que no es lo que la regla decía comprar—.~~

> **SUPERADO el 2026-08-13 por esta §8.2, con lo que `R-2` midió al ejecutarse.** El fundamento tachado es **falso en su premisa**: «el acto 2 toca prosa y sólo prosa» no es cierto, y por eso la conclusión —«no puede mover ninguna cifra de `V-4`»— tampoco lo es. Corregir un párrafo que argumentaba por el nombre viejo **casi siempre toca también las regiones de código de ese párrafo**: el identificador viejo suele estar entre acentos graves dentro de la misma oración que se reescribe. `R-2` lo mostró sobre `Plan-Etapa-A.md` §1.2, donde el acto 2 dejó tachados los fundamentos 1 y 2 y **movió con ellos ocurrencias en región de código**.
>
> **Lo que sobrevive del párrafo tachado, y no es poco:** el caso de `R-1b` —dos actos sobre el mismo archivo, un solo commit— sigue siendo admisible, y sigue siendo cierto que separar compraba legibilidad del diff. Lo que cae es la garantía mecánica que se le atribuía. El fundamento que rige hoy es **la condición escrita en la regla de abajo**, que es una condición verificable y no una premisa sobre lo que el acto 2 puede tocar.

> **La regla que rige desde la 1.5, y corrige la de la 1.3.** Los dos actos van en **commits separados cuando tocan archivos distintos**. Cuando los dos caen sobre **el mismo archivo**, se admite **un solo commit**, con dos condiciones: el mensaje **declara los dos actos por separado**, cada uno con su cuadre; y si el acto 2 **escribe, borra o mueve** —cualquiera de las tres— un identificador de la población del tramo **dentro de una región de código**, **se separa igual**, y si ya se hizo junto, el tramo **vuelve a medir las dos direcciones del cuadre después del acto 2**.
>
> **Qué corrige respecto de la 1.3, y quién lo levantó.** La 1.3 sólo preveía que el acto 2 **escribiera** un identificador en región de código. **`R-2` midió que borrarlo mueve el cuadre igual**, y en la dirección contraria: una ocurrencia nueva que desaparece hace que `ocurrencias nuevas = candidatas − lista previa` deje de dar, exactamente como la haría fallar una ocurrencia nueva que aparece. El control `V-4` es aritmético **en las dos direcciones** (punto 3 de su formulación completa en §7), de modo que **toda alteración de la población en región de código lo mueve, sin importar el signo**. Tachar un párrafo —que es lo que §8.2 barrido 2 ordena hacer— es justamente la operación que borra.

Bajo la regla acotada, `R-1b` no habría sido apartamiento. **Se registra como apartamiento igual**, porque la regla vigente cuando el tramo corrió era la otra, y el registro de un hecho no se reescribe con la norma de después (§4.1).

**La excepción que hubo, y ya está saldada:** el acto 2 de `R-1` no se ejecutó, porque la regla no existía cuando el tramo corrió. Se ejecutó en **`R-1b`** el 2026-08-12, junto con el acto 1 de ese tramo, y con eso `R-2` queda habilitado.

**Lo que el renombre no toca, y conviene tenerlo a la vista mientras se ejecuta:** la prosa (§4), las citas, los reportes de fuente ajena, los registros históricos y los homónimos que nombran otro concepto (§4.1), los identificadores documentales `CU-XX`, `RN-XX`, `BT-XX`, `ADR-XX`, `E-VIS-XX`, `DXT-XX`, `DXC-XX`, el nombre del producto, el de los siete proyectos de código, **la maqueta de `SDD/Maquetas/`** (§6.11) y **el dato del alumno de §5.4**. Un tramo que toque cualquiera de esos se detiene y se revierte.

## 9. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.12 | 2026-08-15 | **Agrega §6.15, la duodécima tabla del glosario: las 36 filas que la etapa `d` necesitó, y no renombra nada.** La trae la etapa `d`, que construye el **ciclo de vida de la cuenta de alumno** —`F-02`, `F-03`, `F-04` y `F-26`— del lado del servicio: el auto-registro sin contraseña, las cuatro operaciones del administrador, el reseteo que conserva la cuenta y sus trabajos, y la producción de la contraseña provisoria. **36 filas: 16 tipos y 20 miembros**, agregadas **antes** de escribir los identificadores, como manda el corolario 4 de §6.1. **§6.15** trae además tres declaraciones de ausencia, para que `V-1` no las levante como huecos: los diecisiete nombres que ya tenían fila en §6.13.2 y en §6.14.2 y que esta etapa **reusa en lugar de duplicar** —`ProvisionalPassword` entre ellos, que es el mismo concepto del campo de la pantalla del cambio forzado y por el corolario 1 de §6.1 **no puede llevar un segundo nombre**—; los cuatro tipos de prueba, que se llaman como lo que ejercitan; y las cinco constantes de ruta, con el mismo criterio con el que la etapa `c` dejó fuera las suyas. **Y una constancia que reproduce la de la etapa `c` sobre una población mayor: de los quince códigos de condición que la etapa `d` escribió, CERO necesitaron fila nueva**, incluidos los dos casos que el glosario tenía resueltos de antemano —la **unificación** de §6.9, que hace que `CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA` y `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` compartan nombre inglés y que la etapa escribiera **una sola constante**, y los **dos retirados por RN-16** de §6.8.5, que **no se reciclaron**—. **§6.1** amplía su rango de once tablas a doce y **§6.2** suma la fila de §6.15 con su recuento y reescribe la prosa que enumera las tablas de etapa. **Y corrige dos defectos preexistentes que esta emisión encontró al cuadrar:** el rango de **`V-1`** en §7 decía **nueve tablas** desde la 1.6 —cuando la 1.6, la 1.7 y la 1.8 ya habían agregado §6.12, §6.13 y §6.14 al rango que §6.1 declara— y pasa a **doce**; y la **tabla de contenido** no listaba §6.14, agregada por la 1.8, de modo que entran las dos entradas que faltaban. Ninguna decisión de §5 se reabre y ningún identificador se renombra. | Orquestador SDD (etapa `d`) |
| 1.11 | 2026-08-15 | **Agrega dos filas a §6.14 y reconcilia dos recuentos que estaban mal desde la 1.8.** Las trae la **guardia de arranque de la clave de firma**: `SigningKeyStartupTests` y su `Compose`, agregadas **antes** de escribir los identificadores. El origen es un hecho del despliegue del 2026-08-15 y no una revisión de escritorio: el servicio de datos se levantó sin la variable de entorno de la clave, respondió el punto de salud como si estuviera sano, y el fallo apareció recién cuando una persona intentó entrar. **Los dos recuentos reconciliados son defectos ajenos a este arreglo y se corrigen con la evidencia a la vista**: §6.2 daba **106** filas a §6.13 cuando la 1.8 le agregó tres y §6.13 declara «109 filas, contadas» —queda en **109**—, y el encabezado de §6.14.1 decía **7** con **8** filas escritas —queda en **9** con la de esta emisión—. §6.14 pasa de 37 a **39**. **No renombra nada, no retira nada y no mueve ninguna cifra de §2 ni el total de 155.** | Guardia de arranque de la clave de firma |
| 1.10 | 2026-08-15 | **Retira dos identificadores de §6.13.2 y agrega cinco filas a §6.14, y no renombra nada.** La trae el **arreglo del cambio forzado tras la fusión**, que corrigió un defecto que el compilador no veía: la pantalla del cambio forzado leía el correo de la cuenta de un `SessionState` de **alcance de petición** que el ingreso —superficie estática— había escrito en **otra** petición, de modo que atravesando la redirección llegaba siempre en nulo y el alumno reseteado volvía a quedarse sin puerta. **Los dos retirados son `PasswordChangeEmail` y `BeginPasswordChange`**, y **conservan su fila en §6.13.2 con el motivo del retiro escrito en la tercera columna**, que es lo que esta norma manda con un identificador retirado: §4.1 declara **intocable** el registro de lo que hubo y §6.8.5 hace exactamente eso con los cuatro códigos internos retirados, «para que una cita vieja resuelva contra la tabla y no contra el criterio de quien la lea». **Ninguno se recicla**, y **el recuento de §6.13.2 no se mueve**: retirar no borra la fila, y sigue en 78, como el total de 109 de §6.13. **Las cinco filas nuevas de §6.14** son **1 tipo** —`ForcedChangeForm`, el modelo del formulario de `OwnCredentialForcedChange`— y **4 miembros y valores** —`ProvisionalPassword`, `NewPasswordRepeat`, `Forget` y el valor `forced-password-change`, el nombre de formulario que el marco lee para encaminar el envío, con la misma forma que `sign-in` y `sign-out`—. Entran por el **corolario 4 de §6.1** y **fuera de los 155**, con el mismo criterio de la 1.5, la 1.6, la 1.7, la 1.8 y la 1.9. **Dos conceptos NO llevan fila nueva y se declara por qué**: el **correo** de la pantalla es `Email` y la **contraseña nueva** es `NewPassword`, los dos ya en §6.13, y el corolario 1 de §6.1 prohíbe darle un segundo nombre al mismo concepto; sus filas, y las de `Input`, `Form` y `SubmitAsync`, **amplían su tercera columna** para nombrar el tipo y el componente nuevos, sin agregar filas. **Una fila lleva marca de apartamiento**: `ForcedChangeForm` declara **cuatro** campos donde `Wireframes-Credencial-Propia.md` dibuja **tres**, apartamiento **a confirmar por el Product Owner**, escrito en §2 de ese wireframe y en la cabecera del componente. §6.14 pasa de **32 a 37 filas** —de **7 a 8 tipos** y de **25 a 29 miembros, propiedades y valores**—, y §6.2 y la prosa de §6.2 y §6.14 mueven ese recuento. **El rango del glosario sigue siendo once tablas**, porque no agrega ninguna. **No renombra nada del corpus, no toca las diez tablas anteriores y no mueve ninguna cifra de §2 ni el total de 155.** | Arreglo del cambio forzado tras la fusión (retiro, agregado y recuento) |
| 1.9 | 2026-08-15 | **Agrega a §6.14 las cinco filas del guardián 2 de `Web ADR-03` §2 y de su puerta de servicio, y no renombra nada.** La marca de sesión de la 1.8 dejó construida la sesión, pero **no cerró el guardián**: las siete rutas del panel seguían respondiendo sin sesión. Cerrarlo escribe **1 tipo** —`PanelSessionGateMiddleware`— y **4 miembros y valores** —`PanelRoutes`, `SignInPath`, `WalkthroughSetting` y su valor `PanelWalkthroughWithoutSession`—. Entran por el **corolario 4 de §6.1** y **fuera de los 155**, con el mismo criterio de la 1.5, la 1.6, la 1.7 y la 1.8. **Dos filas llevan marca de decisión de esta etapa**: `WalkthroughSetting`, que es la puerta de servicio que habilita el paseo sin sesión y **sólo tiene efecto en el entorno de desarrollo**, y su valor `PanelWalkthroughWithoutSession`, cuyo nombre dice qué abre y para qué en lugar de leerse como un interruptor de seguridad genérico. §6.14 pasa de **27 a 32 filas** —de **6 a 7 tipos** y de **21 a 25 miembros, propiedades y valores**—, y §6.2 y la prosa de §6.2 y §6.14 mueven ese recuento. **El rango del glosario sigue siendo once tablas**, porque no agrega ninguna. **No renombra nada del corpus, no toca las diez tablas anteriores, no toca las 27 filas de la 1.8 y no mueve ninguna cifra de §2 ni el total de 155.** | Guardián de sesión del panel (construcción y recuento) |
| 1.8 | 2026-08-14 | **Agrega §6.14, la undécima tabla del glosario: los 27 identificadores que exigió construir la marca de sesión del navegador, y no renombra nada.** La trae el trabajo que completa `Web ADR-03` §2 —«el navegador conserva una marca de sesión que **no la transporta**»—, mitad que la etapa `c` había dejado sin construir: la sesión vivía sólo en el estado del circuito y no sobrevivía a una recarga. **6 tipos** —`SessionTokenStore`, `SessionCookieDefaults`, `SessionClaims`, `UnrestorableSessionMiddleware`, `SignInForm` y el andamiaje `PublicPieceHarness`— y **21 miembros, propiedades y valores**, incluidos los dos nombres de formulario que el marco lee para encaminar un envío, `sign-in` y `sign-out`. Entran por el **corolario 4 de §6.1** y **fuera de los 155**, con el mismo criterio con el que la 1.5 agregó las cinco de la etapa `a`, la 1.6 las 214 de la etapa `b` y la 1.7 las 106 de la etapa `c`. **Dos filas reemplazan forma y no concepto**: `OpenAsync` y `CloseAsync`, porque abrir y cerrar pasan a necesitar el contexto de la petición para escribir y borrar la marca; `Open` y `Close` **quedan en §6.13** como registro de lo que hubo (§4.1). **Una fila lleva marca de decisión de esta etapa** —`CookieName`, cuyo valor no nombra la tecnología que la emite— y **una lleva marca de propuesta** —`Clear`, que existe para poder ejercitar en prueba lo que el reciclado del proceso hace solo—. La regla de §6.1 y el rango del glosario pasan de **diez tablas a once**, y §6.2 suma la fila del recuento. **No renombra nada del corpus, no toca las diez tablas anteriores y no mueve ninguna cifra de §2 ni el total de 155.** | Sesión por marca de navegador (construcción y recuento) |
| 1.7 | 2026-08-14 | **Agrega §6.13, la décima tabla del glosario: los 106 identificadores que la etapa `c` necesitó, y no renombra nada.** La trae la etapa `c`, que construye la primera **rebanada vertical** del producto —identidad del administrador y sesión, capacidades `F-01` y `F-05`— y que al hacerlo escribe los primeros identificadores de **dominio, aplicación, contratos, infraestructura y superficie HTTP**: **30 tipos**, **75 miembros y propiedades** y **1 subsegmento de espacio de nombres**, `GeometriaFactory.Contracts.Errors`. Entran por el **corolario 4 de §6.1** y **fuera de los 155**, con el mismo criterio con el que la 1.5 agregó las cinco de la etapa `a` y la 1.6 las 214 de la etapa `b`. **Lo que más importa de esta emisión es una ausencia: ningún código de condición lleva fila nueva.** Los veinte que la etapa `c` escribió —doce del dominio, dos propios de la aplicación, uno de infraestructura y ocho del contrato— **ya estaban los veinte** en §6.8 con su nombre inglés fijado por `F-03`, y la etapa los tomó de ahí sin traducir ninguno por criterio propio: es la primera vez que el glosario se usa para lo que fue escrito, y funcionó. **Seis filas llevan marca de propuesta o de decisión de etapa y no de fuente**: `DataServiceHarness`, `AnchoredFunction` —donde se ancla **PBKDF2 sobre SHA-256**, elección que la etapa `c` toma aplicando el criterio de `Infrastructure ADR-04` §2, que el intake §17.3.P.1 dejó abierto entre PBKDF2 y Argon2—, `AnchoredIterations`, `MinimumSigningKeySizeInBytes`, `LifetimeInMinutes` y `UseAccessToken`, que es método y no propiedad justamente para que no sea interpolable en el marcado. La regla de §6.1 y el control `V-1` pasan de **nueve tablas a diez**, y §6.2 suma la fila del recuento. **No renombra nada del corpus, no toca las nueve tablas anteriores y no mueve ninguna cifra de §2 ni el total de 155.** | Etapa `c` (construcción y recuento) |
| 1.6 | 2026-08-14 | **Agrega §6.12, la novena tabla del glosario: los 214 identificadores que la etapa `b` de `GeometriaFactory-Web` necesitó, y no renombra nada.** La trae la etapa `b`, que construye las once superficies como pantallas de marcador de posición y **porta el sistema visual de la maqueta aprobada**, y que al hacerlo escribe cuatro familias de identificadores que **ninguna de las ocho tablas anteriores contaba**: **24 tipos de componente** —los dos armazones, las once superficies con los tres cursos de `Credencial-Propia` desdoblados por su shell, los compartidos y las dos carpetas nuevas—, **13 miembros y parámetros**, **9 iconos** del catálogo `ICONOS` de la maqueta, y **168 nombres de clase CSS y de animación** —165 clases y 3 `@keyframes`—, que son el grueso y la familia que obligó a abrir la sección: una clase CSS la lee el navegador, cae del lado de §3 y hasta ahora no tenía regla porque no había hoja de estilos del producto. Entran por el **corolario 4 de §6.1** y **fuera de los 155**, con el mismo criterio con el que la 1.5 agregó las cinco de la etapa `a`. **La frontera de §6.11 se sostiene sin excepción**: la maqueta conserva sus nombres castellanos y no se renombra —es línea de base aprobada—, y lo que §6.12 declara es la **correspondencia** entre las dos escrituras, que es lo que vuelve verificable que el porte fue fiel. Se declaran además **las catorce clases que NO se portaron**, con su motivo: son las de los tres bloques que `Linea-Base-Visual.md` §6 rotula instrumento de la maqueta. La regla de §6.1 y el control `V-1` pasan de **ocho tablas a nueve**, y §6.2 suma la fila del recuento. **Tres filas llevan marca de propuesta y no de fuente** —`StagePlaceholder`, `SurfaceOutline` y `NotFoundSurface`/`NotFoundPage`—, porque ninguna fuente las declara; `NotFoundSurface` además **no lleva `SUP-XX`**, por la misma razón por la que `Linea-Base-Visual.md` §6.1 no le inventa identificador a lo que nadie miró. **No renombra nada del corpus, no toca las ocho tablas anteriores y no mueve ninguna cifra de §2 ni el total de 155.** | Etapa `b` de `GeometriaFactory-Web` (porte y recuento) |
| 1.5 | 2026-08-13 | **Suspende los cuatro tramos de renombre que quedaban y emite la regla que los reemplaza; corrige un defecto de método que `R-2` levantó al ejecutarse; y agrega al glosario las cinco filas que la etapa `a` necesitó.** **Primero, y es la decisión del Product Owner del 2026-08-13 (§8):** los tramos ejecutados —`R-1`, `R-1b` y `R-2`— **quedan como están**, y `R-2b`, `R-3`, `R-4` y `R-5` pasan a **suspendidos**. El fundamento es del Product Owner y se transcribe: esos tramos renombraban identificadores **en documentos que describen código que no existe**, y **el glosario ya está completo** para escribir ese código en inglés desde el primer archivo. La regla que los reemplaza es la del recuadro de §8: **el glosario es la fuente de nombres para todo código nuevo**; lo que no está en la tabla **se agrega primero** y no se traduce por criterio propio; y los documentos que describen conceptos con su nombre castellano **se actualizan cuando alguien los toca por otro motivo**, no en una tanda propia. §8 declara además los cuatro puntos que la suspensión **no** afloja —§6 y §3 siguen rigiendo, el corolario 4 de §6.1 pasa a ser el mecanismo principal, `V-4`, `V-6` y `V-7` quedan sin población pero no se retiran, y la actualización por contacto ejecuta los dos actos de §8.2— y **deja escritas dos cifras que no cuadran** con la forma en que la decisión se comunicó: los tramos pendientes son **cuatro** y no cinco, y sus ocurrencias candidatas suman **8260** y no ocho mil seiscientas. No se acomoda ninguna de las dos: las cifras de este documento son medidas (§2.1). **Segundo, el defecto de método (§8.2):** la regla acotada de la 1.3 fundaba la admisión de un solo commit en que «el acto 2 toca prosa y sólo prosa» y por lo tanto «no puede mover ninguna cifra de `V-4`». **`R-2` midió lo contrario**: corregir un párrafo que argumentaba por el nombre viejo toca también las regiones de código de ese párrafo, y **borrar** una ocurrencia mueve el cuadre igual que escribirla, porque `V-4` es aritmético **en las dos direcciones**. El párrafo queda **tachado y marcado, no borrado** (§8.2 barrido 2 y `V-7`), con lo que sobrevive de él declarado, y la regla pasa a decir **«escribe, borra o mueve»**, con la obligación de volver a medir las dos direcciones después del acto 2 si los actos ya fueron juntos. **Tercero, el glosario (§6.2, §6.4 y §6.5):** la etapa `a` ejerció el corolario 4 de §6.1 y agregó **cinco filas**, declaradas **fuera de los 155** igual que las cinco de §6.11, porque ninguna de las seis clases las contó y los conceptos no existían cuando se contaron: `EstadoDelServicio` ⟶ **`ServiceHealth`** y `VisorDeGeometriaFactory` ⟶ **`GeometriaFactoryViewer`** en §6.4, y los tres miembros de `ServiceHealth` —**`Ready`**, **`Version`** y **`ServerTimeUtc`**— en §6.5. **Los recuentos de las seis clases no cambian.** Que el cuerpo de la respuesta de salud lleve esos tres datos sigue siendo **propuesta de la etapa `a`** y decisión del punto de control: acá se fija **cómo se llaman**, no **qué se publica**. | Orquestador SDD · Product Owner (la decisión de §8) |
| 1.4 | 2026-08-12 | **Corrige el método por tercera vez, y es la última pasada antes de `R-2`.** No la trae un tramo ejecutado: la trae **la revisión de la 1.3 que el orquestador ordenó antes de abrir `R-2`** —sin informe aparte, porque su resultado es esta emisión—, que se preguntó **qué otros controles prometen más de lo que pueden verificar**, que es la clase de defecto que compartían los tres que la 1.3 reparó. Encontró **tres**, y los tres están verificados contra la fuente antes de corregirse. **Primero: `V-1` no podía cuadrar lo que la propia norma había agregado al glosario.** Exigía resolver todo identificador declarado contra «una fila de §6.3 a §6.8», y el glosario tiene además **§6.10** —16 subsegmentos de espacio de nombres— y **§6.11** —5 superficies derivadas, entre ellas el tipo `FigureViewer`—, las dos **fuera de ese rango**; y el corolario de §6.11 manda agregar ahí toda carpeta que no nombre un concepto listado, de modo que la regla ordenaba escribir filas en una tabla que el control no miraba. §2.1 ya decía «§6.3 a §6.11» y §6.1 y `V-1` decían «§6.3 a §6.8»: la contradicción estaba adentro del documento. **Los dos pasan a nombrar el glosario entero —ocho tablas—**, con §6.9 explícitamente fuera, porque no agrega filas. **Segundo: `V-6` levantaba como falla lo que la norma declaró correcto.** Prometía que ninguna carpeta ni archivo bajo `src/`, `tests/` o `visor/` queda en castellano, **sin excepción escrita**, mientras §1 y §6.11 declaran intocable la **raíz `visor/`** del proyecto de código y `scripts/build-visor.sh` la lleva adentro del nombre por la misma razón: son las **10 ocurrencias** —7 de la raíz y 3 del guion— que `R-1b` clasificó a mano sobre sus 17 no renombradas. **`V-6` pasa a admitir los mismos cinco motivos de §4.1 que `V-4`, contra la misma lista previa**, con el mismo cuadre celda por celda y la misma exclusión de la fila de control de cambios del propio tramo. **Tercero: `V-7` contradecía a §8.2.** Prometía que ningún documento tocado conserva texto que argumenta a favor del nombre anterior, mientras §8.2 barrido 2 ordena lo contrario —se reescribe **con el fundamento nuevo, no se borra**, porque un argumento borrado deja la decisión sin por qué—; `R-1b` hizo lo correcto en [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.2 —conservó los cinco fundamentos de `P-1a` con su estado y tachó el quinto, refutado por §5.1— y **cerró el control interpretándolo**. **`V-7` se reformula sobre la marca**: pasa el argumento conservado y marcado como superado —estado, fecha y sección que lo supera—, falla el argumento vivo sin marca, y falla también el borrado. El barrido 2 de §8.2 pasa a exigir esa marca. **Y declara qué tan mecánico es cada control.** §7 trae la tabla de los **siete controles y los tres barridos**, con tres veredictos posibles: verificable como está —`V-3`, `V-4`, `V-5` cuando exista código, y el barrido 1—, verificable con la precisión de esta versión —`V-1`, `V-6`, `V-7` y el barrido 2— y **no verificable**, que son **dos**: `V-2`, cuyo «ningún identificador nuevo en castellano» no tiene forma de pasa/falla, y el **barrido 3**, que enumera tres afirmaciones refutadas pero no da texto literal a buscar. Se declara qué haría falta para cada uno y **no se agrega ningún control nuevo**. Consecuencia para `R-2`: `V-2` es control de emisión y el tramo no declara identificadores nuevos; el barrido 3 sí corre, y lo que el tramo declare como cubierto por él **es afirmación de quien lo ejecuta y no medición**, y así debe escribirse en su informe. **No renombra nada, no toca el glosario, no agrega ni quita filas y no mueve ninguna cifra de §2.** | Orquestador SDD (revisión de método, correcciones y redacción) |
| 1.3 | 2026-08-12 | **Vuelve a corregir el método, y no los nombres.** Emitida a raíz del **informe del tramo `R-1b`** —commit `c0b8b4f` del 2026-08-12, «tramo `R-1b`: la deuda del ensayo, con los dos actos»—. El tramo **cerró limpio**: los siete controles aplicables cuadraron, `V-4` cuadró por primera vez contra una lista escrita antes de editar y **las cifras de la 1.2 se reprodujeron exactas**, lo que valida las correcciones de esa versión. Y encontró **tres defectos nuevos del método**, que ésta repara antes de `R-2`. **Primero: `V-4` no admitía por escrito el motivo que más se usó.** §7 admitía «los cuatro de §4.1», pero §2.1 ya reconocía un quinto caso —**otro concepto con el mismo nombre**— y **10 de las 17 ocurrencias no renombradas de `R-1b` caen ahí**: 7 de la raíz `visor/` del proyecto de código, que §1 declara fuera de discusión y §6.11 distingue de la carpeta de la capa 3, y 3 del guion `build-visor.sh`. §4.1 le da **fila propia** en su tabla, con su definición y su ejemplo medido, y `V-4` pasa a admitir **cinco motivos**: prosa, cita textual, reporte de fuente que no se renombra, registro histórico y otro concepto con el mismo nombre. **Segundo: la fila de control de cambios del propio tramo rompía el cuadre literal.** Al describir lo que hizo, esa fila **reintroduce el identificador viejo** —la de `R-1b` agregó **6 ocurrencias**: una de `Configuraciones`, una de `Paginas`, una de `Internos` y tres de `visor`— y **ninguna podía estar en una lista escrita antes de editar**, porque la fila se escribe después: con la fila adentro el cuadre daba 23 contra 17, y sin ella **17 exactas**. `V-4` declara ahora que **esa fila, y sólo esa fila, queda fuera del cuadre en las dos direcciones**; el resto del control de cambios sigue adentro como registro histórico. **Tercero: el acto 2 no tenía lista previa.** §8.2 barrido 1 mandaba clasificar por §4.1 y «entrar en la lista de `V-4`», pero `V-4` exige la lista **antes de editar** y el acto 2 corre **después** del acto 1. `R-1b` lo resolvió midiendo el barrido 1 antes de tocar nada; la norma no lo ordenaba y ahora sí: **la lista es una sola, cubre los dos actos y se mide antes del acto 1**, y el barrido 1 clasifica contra ella. **Registro.** §8.1 marca **`R-1b` como ejecutado el 2026-08-12** con su cuadre —22 candidatas, 5 renombradas, 17 no renombradas—, su resultado y la remisión al informe; el ensayo queda cerrado y **`R-2` habilitado**. Y se registra el **apartamiento** del tramo: §8.2 pedía los dos actos en commits separados y `R-1b` fue en uno solo, porque los dos vivían en el mismo archivo y separarlos después exigía reeditar. **La regla de los dos commits se acota, no se conserva ni se reemplaza**: van separados **cuando tocan archivos distintos**; sobre el mismo archivo se admite uno solo si el mensaje declara los dos actos por separado y si el acto 2 no escribe, dentro de una región de código, un identificador de la población del tramo. El fundamento es el instrumento de §2.1: cuenta tokens **dentro de regiones de código**, y el acto 2 toca **sólo prosa**, de modo que sobre el mismo archivo no puede mover ninguna cifra de `V-4`. Bajo la regla acotada `R-1b` no habría sido apartamiento; se registra como tal igual, porque la regla vigente al ejecutarse era la otra. **No renombra nada, no toca el glosario y no mueve ninguna cifra de §2.** | Orquestador SDD (medición, correcciones y redacción) |
| 1.2 | 2026-08-12 | **Corrige el método, no los nombres.** Emitida a raíz del **informe del tramo de ensayo `R-1`** —commit `1edccca` del 2026-08-12, «tramo `R-1`: el ensayo del renombre, y las cinco correcciones que compró»—, que ejecutó el tramo y reportó cinco fallas del método. **Primera: las cifras estaban mal medidas y se remidieron las cinco poblaciones con herramienta.** §2.1 declara el instrumento paso por paso, la unidad de cada cifra y que lo que se declara es la **ocurrencia candidata** y no la ocurrencia a renombrar; reproduce **seis** cifras de la 1.1 con exactitud y corrige las demás. La cifra que el informe encontró —«41 ocurrencias» del tramo `R-1`, que era un recuento de identificadores y **la única de la norma que no se midió con herramienta**— queda en **38 identificadores, 80 ocurrencias candidatas, 2 de prosa, 78 renombradas**. El corpus pasa de 631 a **632 archivos**, con la diferencia explicada: la 1.0 se contó a sí misma fuera. Las correcciones grandes: la **clase 2** pasa de 3 documentos y 37 ocurrencias a **46 y 297** —se había contado sobre quien declara y no sobre quien usa—, y la **clase 6** de 334 y 2911 a **330 y 2847**, porque la 1.1 midió por forma `SCREAMING_SNAKE_CASE` y no contra la lista de los 101: el corpus tiene 141 tokens de esa forma y 40 no son códigos. La clase 4 pasa a 53 y 621, la clase 5 a 399 y 4461, y el total a **155 identificadores en 464 de 632 archivos con 8712 ocurrencias**. **Segunda: falta una clase entera en el glosario, y es §6.11, las superficies derivadas.** Cada identificador se escribe además como carpeta y como nombre de archivo, y dos reglas ya declaradas lo obligan; la regla nueva fija que **por debajo del nivel de espacio de nombres el idioma no se afloja** y que §4 no alcanza a nada bajo `src/`, `tests/` ni `visor/`. §2.3 cuenta la superficie —**16 ocurrencias en dos documentos**— y §6.11 agrega **cinco filas**: los tres huecos que el ensayo reportó (`Configuraciones/`, `Paginas/`, `Internos`) y **dos más que la remedición encontró** (`visor/` de la capa 3 y `VisorFiguras.razor` ⟶ `FigureViewer.razor`), con la maqueta de `SDD/Maquetas/` declarada fuera por ser línea de base aprobada. **Tercera: `V-4` era inaplicable y se reformula.** Deja de ser «cero ocurrencias del identificador viejo» —imposible de cumplir, como probó `Estado`, que aparece cinco veces y dos son prosa— y pasa a ser **ocurrencias viejas restantes = las declaradas de antemano**, con **la lista escrita antes de editar**, ocurrencia por ocurrencia y con su motivo; sin esa lista el tramo no cierra. Y para los conjuntos cerrados el cuadre es **por concepto y no por cadena**: `Pendiente` son 1983 ocurrencias, 956 de ellas en los 58 documentos que traen los dos contextos, y hay 934 ocurrencias de «pendiente» en prosa que un renombre por cadena destruiría. **Cuarta: se escribe §4.1**, la regla de **citas, reportes de fuente ajena, registros históricos y uso propio**, con lo que hace el renombre con cada forma y cómo se reconoce; el corpus tiene **117 citas entrecomilladas con 134 ocurrencias de identificador en 58 documentos** y **441 filas de control de cambios** que nombran uno. Importa ya, porque `R-2` toca **el intake, que es la fuente**. **Quinta: el plan no cubría las seis clases y ahora sí.** Los cinco tramos sumaban **138 de 155**; §8 pasa a **siete tramos** con **`R-2b`**, que renombra las cinco entidades y las cinco tablas en mayúsculas, ubicado entre `R-2` y `R-3` por alcance y por dependencia, y **`R-1b`**, que salda la deuda del ensayo. La cobertura queda en **148 renombrados + 7 declarados intocables (§5.4) = 155**. Suma además **`V-6`** —cuadre de la superficie derivada— y **`V-7`** —coherencia interna del documento renombrado— a §7, y **§8.2, los dos actos de cada tramo**: renombrar y corregir el texto que quedaba argumentando por el nombre viejo, con los **tres barridos** que lo detectan y la regla de que el acto 2 corre dentro del mismo tramo y antes de cerrarlo. El acto 2 de `R-1` —`Plan-Etapa-A.md` con sus identificadores en inglés y su §1.2 argumentando por el castellano con cinco fundamentos— queda asignado a `R-1b`, condición de entrada de `R-2`. **No renombra nada.** | Orquestador SDD (remedición, reglas y redacción) |
| 1.1 | 2026-08-12 | **Las tres zonas de frontera dejan de ser propuestas elevadas y pasan a decisiones tomadas**, con su fecha, su fundamento y su costo contado (§5): **`F-01a`**, las seis funciones de la fachada del visor van a inglés, con el fundamento decisivo de que el intake §17.7.P.3 las declaraba «a fijar en la etapa que la implementa» y por lo tanto **nunca estuvieron fijadas**; **`F-02a`**, los conjuntos cerrados llevan identificador en inglés y etiqueta en castellano, con el fundamento de que deshace la colisión ya declarada de `Pendiente` —que hoy nombra dos cosas y en inglés se separa en `Pending` y `Submitted`— y con la ventana de costo cero mientras no haya base poblada; y **`F-03`**, **todos** los códigos de condición van a inglés, los 80 internos y los 21 de contrato, por consistencia total, con el fundamento de que el producto no emitió una sola respuesta todavía y los dos consumidores compilan juntos. **`F-03` se declara explícitamente como cambio de contrato** y no como renombre, con la regla operativa `RT-06` —los dos extremos se cambian y se despliegan juntos— y con la eliminación del prefijo `CONTRATO_`. **El glosario pasa de 42 conceptos a las seis clases completas: 155 identificadores en 155 filas** (§6.2 a §6.8), con el recuento rehecho sobre los seis catálogos y el desglose que reconcilia los 101 códigos —76 internos vivos + 4 retirados + 21 de contrato—. Suma **la regla operativa** de que un concepto no listado no se traduce por criterio propio sino que se agrega primero, sus cuatro corolarios y la convención exacta de forma de los códigos; **dos unificaciones** de conceptos que el corpus nombra hoy dos veces —`CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA` con `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR`, y las cinco tablas en mayúsculas del intake con las cinco entidades— y **cuatro homónimos declarados** que el retiro del prefijo produce (§6.9); y **dos huérfanos que no se traducen**, `CONTRATO_CAMBIO_DE_CONTRASENA_PENDIENTE` y `CONTRATO_RESETEO_NO_ADMITIDO`, que tres casos de uso de `GeometriaFactory-Web` citan y ningún catálogo declara. Agrega **`V-4`** —cuadre del renombre en las dos direcciones— y **`V-5`** —catálogo contra tipo y no contra nombre— a §7, y **§8, el plan de renombre en cinco tramos** con su alcance contado, sus proyectos alcanzados y lo que se verifica al cerrar cada uno, del más barato al más caro. El documento pasa de `Propuesto` a **`Aprobado`**. **No renombra nada**: el renombre es la tanda que ejecuta §8. | Product Owner (las tres decisiones) · Orquestador SDD (recuento, glosario y redacción) |
| 1.0 | 2026-08-12 | **Emisión inicial**, a pedido del Product Owner, que observó que el corpus nombra identificadores de código en castellano contra el estándar. Fija las dos zonas que no se discuten —**identificadores de código en inglés** (§3) y **texto en castellano** (§4)— y separa las tres **zonas de frontera** que no decide y eleva: `F-01` las seis funciones de la fachada del visor, `F-02` los diez valores de los cuatro conjuntos cerrados, `F-03` los 101 códigos de condición y de contrato, cada una con propuesta, costo contado y alternativa real. Emite el **glosario de correspondencia** con 42 conceptos derivables de la especificación, la regla de que **un concepto no listado no se traduce por criterio propio**, y la correspondencia de los 18 espacios de nombres. Cuenta el alcance real sobre **631 archivos** de `SDD/` excluidos `_legacy/` y `Docs/Audit/`: **396 documentos y 4259 ocurrencias** de valores de conjunto cerrado, **334 documentos y 2911 ocurrencias** de códigos de condición, **52 documentos y 593 ocurrencias** de la fachada. Declara **dos hechos verificados que cambian la discusión**: que el intake §17.7.P.3 nunca fijó los nombres de la fachada sino que los dejó «a fijar en la etapa que la implementa», contra lo que `Plan-Etapa-A.md` §1.2 afirma; y que **ninguna de las 33 auditorías** verificó jamás el idioma de un identificador. **No renombra nada.** | Product Owner (observación) · Orquestador SDD (medición y redacción) |
