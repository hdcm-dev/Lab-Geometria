# Norma de nomenclatura — Fábrica de Geometría

**Producto:** Fábrica de Geometría
**Documento:** Norma-De-Nomenclatura.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-12
**Autor:** Orquestador SDD (medición y redacción) · Product Owner (las tres decisiones de §5)
**Nivel:** Producto
**Origen:** Observación del Product Owner, 2026-08-12: el estándar nombra espacios de nombres, clases y variables en inglés, y el corpus se salió del estándar sin declararlo. La versión 1.1 incorpora las **tres decisiones tomadas por el Product Owner el 2026-08-12** sobre las zonas de frontera que la 1.0 elevó
**Trazabilidad upstream:** [`../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.30** §13.1 (que registra las tres decisiones de §5), §13, §17.1.P.11, §17.2.P.1, §17.3.P.4, §17.4.P.3, §17.7.P.3 (que declaraba los nombres de la fachada **a fijar en la etapa que la implementa**); [`../Handoff-Checkout.md`](../Handoff-Checkout.md) §6.2 `A-1` y `A-2`; [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.2 a §1.7
**Trazabilidad downstream:** el punto de control de la etapa `a`; las siete categorías `05-Arquitectura-Tecnica`; la tanda de renombre que ordena §8; [`../Audit/Observacion-Desviacion-De-Nomenclatura.md`](../Audit/Observacion-Desviacion-De-Nomenclatura.md)

---

## Tabla de contenido

- [1. Qué fija esta norma y qué no](#1-qué-fija-esta-norma-y-qué-no)
- [2. El alcance real, contado](#2-el-alcance-real-contado)
  - [2.1 Cómo se contó](#21-cómo-se-contó)
  - [2.2 Las seis clases](#22-las-seis-clases)
  - [2.3 Lo que el recuento decide](#23-lo-que-el-recuento-decide)
- [3. Zona 1 · Identificadores de código, en inglés](#3-zona-1--identificadores-de-código-en-inglés)
- [4. Zona 2 · Texto, en castellano](#4-zona-2--texto-en-castellano)
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
- [7. Cómo se verifica esta norma](#7-cómo-se-verifica-esta-norma)
- [8. El plan de renombre](#8-el-plan-de-renombre)
- [9. Control de cambios](#9-control-de-cambios)

---

## 1. Qué fija esta norma y qué no

**Fija el idioma de los identificadores de código y el idioma del texto, y separa las dos cosas.** Hasta hoy el producto no tenía una norma de nomenclatura: tenía una práctica. La práctica nació de transcribir el material del Product Owner y se propagó hasta parecer una convención; el detalle de cómo ocurrió está en [`../Audit/Observacion-Desviacion-De-Nomenclatura.md`](../Audit/Observacion-Desviacion-De-Nomenclatura.md).

**Registra las tres decisiones de frontera, que son del Product Owner.** La versión 1.0 las elevó con su costo contado y su alternativa real, que es lo que hizo posible decidirlas. **El Product Owner las decidió el 2026-08-12** y §5 las registra como decisiones tomadas, con fecha, fundamento y costo. Este documento no las toma: las asienta y las hace verificables.

**Y produce el glosario completo, que es el entregable que hace ejecutable al renombre.** §6 cubre los **155 identificadores** de las seis clases de §2.2. La regla que lo gobierna es una sola: **si un concepto no está en la tabla, no se traduce por criterio propio — se agrega primero**.

**No renombra nada.** Ninguna emisión de esta tanda modifica un identificador del corpus. Renombrar es un acto posterior, se ejecuta **contra el glosario de §6** y su orden es el de **§8**. Hacerlo antes del glosario es lo que produce tres nombres distintos para la misma cosa.

**Y no reabre el nombre del producto ni el de los siete proyectos de código.** Están declarados en el intake §13 y §16, ya son ingleses en su raíz (`GeometriaFactory`, `GeometriaFactory-Domain`, `GeometriaFactory-Api`…) y no son punto abierto — [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.1 lo verifica.

## 2. El alcance real, contado

### 2.1 Cómo se contó

Sobre el árbol `SDD/` del 2026-08-12, **excluidos `_legacy/` y `Docs/Audit/`**: **631 archivos** —615 `.md`, 12 `.html`, 3 `.js` y 1 `.css`—. Cada cifra de §2.2 es un recuento con herramienta sobre ese conjunto, no una estimación.

Dos precisiones de método, porque cambian los números:

1. **Se cuenta el identificador, no la palabra.** `Pendiente` en prosa no cuenta; `` `Pendiente` `` entre acentos graves, que es como el corpus marca un valor de conjunto cerrado, sí. Los códigos de condición se cuentan por su forma completa.
2. **«Documentos» es en cuántos archivos aparece al menos una vez** el identificador de la clase, y «ocurrencias» es cuántas veces en total. Un identificador citado quince veces en un archivo cuenta un documento y quince ocurrencias.

### 2.2 Las seis clases

| Clase | Identificadores distintos | Documentos | Ocurrencias |
| --- | --- | --- | --- |
| **1. Interfaces y puertos** | **5** — `IRepositorioTrabajos`, `IValidadorFiguras`, `IRelojDelSistema` declarados; `IRepositorioCuentas` e `IRepositorioAlumnos` propuestos | 12 declarados · 1 propuestos | 56 declarados · 5 propuestos |
| **2. Entidades y tipos** | **31** — 5 entidades (`Cuenta`, `Trabajo`, `Pieza`, `Componente`, `Observacion`), 5 en mayúsculas del intake (`ALUMNO`…), 7 tipos de figura del dato del alumno, 14 tipos y adaptadores propuestos | 3 · 3 · 30 · 1 | 37 · — · 201 · 18 |
| **3. Miembros y propiedades** | **2** — `HashContrasena`, `JsonOriginal` | 3 | 8 |
| **4. Funciones de la fachada del visor** | **6** — `inicializar`, `cargarJson`, `seleccionarPieza`, `redimensionar`, `destruir`, `establecerMovimiento` | **52**, de los cuales **21** declaran las seis | **593** |
| **5. Valores de conjuntos cerrados** | **10** — 6 estados (`Pendiente`, `Habilitado`, `Bloqueado`, `Borrador`, `Finalizado`, `Rechazado`) y 4 de papel y especie (`Alumno`, `Administrador`, `Advertencia`, `ErrorDeValidacion`) | **396** | **4259** |
| **6. Códigos de condición y de contrato** | **101** — 21 `CONTRATO_*` de la frontera y 80 internos de los seis catálogos | **334** | **2911** |

Los desgloses que hacen falta para decidir:

| Desglose | Distintos | Documentos | Ocurrencias |
| --- | --- | --- | --- |
| Clase 5, sólo los **seis estados** | 6 | 384 | 3874 |
| Clase 5, sólo `Pendiente` | 1 | 349 | 1919 |
| Clase 6, sólo los `CONTRATO_*` | 21 | 220 | 1201 |
| Clase 6, catálogo de `GeometriaFactory-Domain` | 42 vivos + 5 retirados | 65 | 810 |
| Clase 6, catálogo de `GeometriaFactory-Application` | 36 vivos | 114 | 1059 |
| Clase 6, catálogo de `GeometriaFactory-Infrastructure` | 17 vivos | 205 | 394 |
| Clase 6, catálogo de `GeometriaFactory-Visor` | 7 vivos | 48 | 351 |

**El total sin solapamiento entre clases: 155 identificadores distintos en 459 de los 631 archivos, con 8111 ocurrencias.**

**El recuento de la clase 6 se rehizo para la versión 1.1, sobre los seis catálogos, y cierra en 101.** El desglose está en §6.2 y conviene anticipar el término que no era obvio: los **80 internos** son **76 vivos** —la unión de los cuatro catálogos, descontado el solapamiento— **más 4 retirados** que ya no son condición de ningún catálogo y siguen apareciendo en el corpus. El quinto retirado que `GeometriaFactory-Domain` declara sigue **vivo en `GeometriaFactory-Application`** y por eso está entre los 76.

Los desgloses de la clase 6 **se solapan**: un mismo código lo declara `GeometriaFactory-Domain` y lo cita `GeometriaFactory-Application`. La cifra sin solapamiento es la de §2.2: **101 distintos en 334 documentos, 2911 ocurrencias**.

### 2.3 Lo que el recuento decide

Tres cosas, y son las que ordenan el resto del documento.

**Primera: hay dos poblaciones y no una.** Los identificadores *propuestos* —los 14 tipos y adaptadores de [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.6 y §1.7, los dos puertos alternativos, los 16 subsegmentos de espacio de nombres de su §1.3— viven en **un solo documento** y suman **41 ocurrencias**. Renombrarlos cuesta una edición. Los identificadores *declarados* —los códigos de condición, los valores de estado, la fachada— viven en cientos de documentos. Son problemas distintos y merecen respuestas distintas.

**Segunda: el grueso del corpus no está en juego.** Las clases 1, 2 y 3 juntas —puertos, entidades, miembros— son **38 identificadores en 37 documentos**. Ahí la norma se aplica sin negociación, porque casi nada existe todavía.

**Tercera: `Pendiente` sola pesa más que las clases 1, 2 y 3 juntas.** 1919 ocurrencias en 349 documentos, y **nombra dos cosas distintas**: una cuenta que espera habilitación y un trabajo que espera revisión. [`../Proyectos/GeometriaFactory-Domain/02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md`](../Proyectos/GeometriaFactory-Domain/02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md) §2.1 ya tuvo que declarar una forma calificada obligatoria —«marca de cambio de contraseña pendiente»— justamente porque «`Pendiente` a secas nombra un estado de cuenta y un estado de trabajo». Un identificador en inglés no habría tenido esa colisión: `Pending` y `Submitted` son palabras distintas.

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

## 5. Zona de frontera · Las tres decisiones tomadas

Las tres tienen la misma forma: **un identificador que también es dato**. Se persiste, o viaja en una respuesta, o lo invoca otro extremo. Cambiarlo no es renombrar: es cambiar un contrato.

**La versión 1.0 las elevó como propuestas. El Product Owner las decidió el 2026-08-12, y esta versión las registra como decisiones tomadas**, cada una con su fecha, su fundamento y su costo contado. Lo que sigue ya no es una consulta: es la norma.

| Frontera | Decisión | Fecha | Alcance contado | ¿Cambia un contrato? |
| --- | --- | --- | --- | --- |
| `F-01` Fachada del visor | **`F-01a`: las seis funciones van a inglés** | 2026-08-12 | 52 documentos · 593 ocurrencias | No. Los nombres nunca estuvieron fijados |
| `F-02` Conjuntos cerrados | **`F-02a`: identificador en inglés, etiqueta en castellano** | 2026-08-12 | 396 documentos · 4259 ocurrencias | Sí en la forma persistida, **con costo cero hoy**: no hay base poblada |
| `F-03` Códigos de condición | **Los 101 van a inglés: los 80 internos y los 21 de contrato** | 2026-08-12 | 334 documentos · 2911 ocurrencias | **Sí. Es un cambio de contrato y así se declara** |

### 5.1 `F-01` Las seis funciones de la fachada del visor · **decidida**

**Qué son.** `inicializar`, `cargarJson`, `seleccionarPieza`, `redimensionar`, `destruir`, `establecerMovimiento`. Son la superficie pública del *bundle* de TypeScript, expuesta como biblioteca en `window`, y **Blazor las invoca por interoperabilidad** contra `IJSRuntime`. Son el punto de extensión principal del producto: el sample `S-1` las ejerce enteras sin backend, que es lo que hace reemplazable al motor 3D.

**Decisión del Product Owner, 2026-08-12: `F-01a`. Las seis pasan a inglés.** La correspondencia está en §6.6 y es la única fuente de esos seis nombres.

**El fundamento decisivo, verificado en la fuente.** El intake §17.7.P.3 encabeza su tabla así: «Contrato de la fachada, **con los nombres definitivos a fijar en la etapa que la implementa** (RT §8.4)». **Los nombres de la fachada nunca estuvieron fijados**: fijarlos es, literalmente, lo que la etapa que implementa el visor debe hacer, y esta decisión es ese acto. [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.2 punto 5 afirma que «están fijadas por el intake §17.7.P.3», y eso contradice la letra de la fuente que cita; esa afirmación queda corregida por esta norma.

Los otros dos términos del fundamento, y son los que hacen que el momento sea el más barato que va a haber:

1. **El visor no existe como código.** La etapa `a` crea el proyecto y un *bundle* «vacío pero real» (intake §15), con la fachada declarada y sin lógica de dibujo. No hay una sola línea que renombrar.
2. **Su único consumidor está en la misma solución.** El *bundle* sólo lo invoca `GeometriaFactory-Web`, que se compila y se despliega junto con él. No hay consumidor externo al que avisarle.

**Costo contado, y es el que se paga.** **52 documentos** las nombran; **21** declaran las seis; **593 ocurrencias** en total. Los documentos que fijan su contrato son [`../Proyectos/GeometriaFactory-Visor/02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../Proyectos/GeometriaFactory-Visor/02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md), su [`ADR-02`](../Proyectos/GeometriaFactory-Visor/05-Arquitectura-Tecnica/Adrs/ADR-02-Superficie-De-Seis-Funciones-Planas.md), el intake §17.7.P.3, y las categorías 02, 03, 05 y 10 de `GeometriaFactory-Visor` y `GeometriaFactory-Web`. Es un renombre mecánico, verificable con recuento en las dos direcciones, **sin ninguna decisión por documento**.

**La alternativa que se descartó** era `F-01b` —quedan en castellano, declarado como apartamiento—: dejaba la única superficie pública del producto en un idioma distinto del de todo el resto del código, y obligaba a `GeometriaFactory.Web.Integration` a traducir en el punto de invocación, que es el defecto que `RI-06` de [`Vista-Producto.md`](Vista-Producto.md) §7 declara con historia en este producto.

### 5.2 `F-02` Los valores de los conjuntos cerrados · **decidida**

**Qué son.** Cuatro conjuntos: papel (`Alumno`, `Administrador`), estado de cuenta (`Pendiente`, `Habilitado`, `Bloqueado`), estado del trabajo (`Borrador`, `Pendiente`, `Finalizado`, `Rechazado`) y especie de observación (`Advertencia`, `Error de validación`). **Diez identificadores distintos.**

**Decisión del Product Owner, 2026-08-12: `F-02a`. Identificador en inglés, etiqueta en castellano, y la traducción en un solo lugar.** El código dice `Pending`; la pantalla dice «Pendiente». La correspondencia completa está en §6.7.

**Por qué era frontera, y no es una sutileza.** Los valores se persisten y se serializan **por su nombre, nunca por su posición** — [`../Proyectos/GeometriaFactory-Api/05-Arquitectura-Tecnica/Contratos-REST.md`](../Proyectos/GeometriaFactory-Api/05-Arquitectura-Tecnica/Contratos-REST.md) §2.2, y [`../Proyectos/GeometriaFactory-Infrastructure/05-Arquitectura-Tecnica/Modelo-Datos-Logico.md`](../Proyectos/GeometriaFactory-Infrastructure/05-Arquitectura-Tecnica/Modelo-Datos-Logico.md) §2.1 y §2.2, que los guarda como texto. El identificador **es** el dato guardado y el dato transmitido. Y además el alumno lo ve traducido en pantalla.

**El fundamento decisivo: deshace la colisión ya declarada de `Pendiente`.** Hoy `Pendiente` nombra **dos cosas distintas** —una cuenta que espera habilitación y un trabajo que espera revisión— y el corpus ya tuvo que pagar por eso: [`../Proyectos/GeometriaFactory-Domain/02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md`](../Proyectos/GeometriaFactory-Domain/02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md) §2.1 declaró obligatoria una forma calificada —«marca de cambio de contraseña pendiente»— justamente porque «`Pendiente` a secas nombra un estado de cuenta y un estado de trabajo». En inglés son dos palabras: la cuenta está `Pending` y el trabajo está `Submitted` —se envió y espera revisión—. **La forma calificada obligatoria deja de hacer falta**, y §6.7 declara cuál de los dos nombres va en cada contexto.

**Costo contado, y es alto.** **396 documentos, 4259 ocurrencias**: es la clase más grande del corpus. Sólo `Pendiente` son 349 documentos y 1919 ocurrencias.

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

**Costo contado.** **334 documentos, 2911 ocurrencias.** Los `CONTRATO_*` solos: 220 documentos, 1201 ocurrencias. Es el renombre más caro de los tres y el más mecánico: no hay ninguna decisión por documento, sólo correspondencia uno a uno contra §6.8, y los seis catálogos son la fuente única contra la que se verifica. La verificación en las dos direcciones que `ADR-06` de `GeometriaFactory-Application` ya exige —comparar los códigos emitidos contra el catálogo— **sirve tal cual** después del renombre.

**Las dos alternativas que se descartaron.** `F-03b` —quedan como están, declarado como apartamiento— dejaba el producto con una regla partida —identificadores en inglés salvo estos 101— que hay que explicar cada vez, y la explicación es histórica y no técnica. `F-03c` —sólo los 80 internos, y los 21 `CONTRATO_*` quedan— era la salida intermedia, y la 1.0 ya había medido por qué compra poco: reduce el alcance de 334 documentos a 318, que **no es una reducción real**, porque los mismos documentos citan las dos familias; y obliga a `GeometriaFactory-Api` a traducir de un catálogo inglés a uno castellano en la frontera, que es una tabla más que mantener.

**El hallazgo lateral de la 1.0 sigue abierto y se confirma.** Dos identificadores `CONTRATO_*` —`CONTRATO_CAMBIO_DE_CONTRASENA_PENDIENTE` y `CONTRATO_RESETEO_NO_ADMITIDO`— aparecen en **tres casos de uso de `GeometriaFactory-Web`** —`CU-02`, `CU-03` y `CU-04`— y **no figuran en el catálogo de `GeometriaFactory-Contracts`, ni en el de `GeometriaFactory-Api`, ni en `Contratos-REST.md`**. Es un defecto de fondo preexistente y ajeno al idioma: el conjunto real citado por el corpus es **23** y el conjunto declarado es **21**. §6.8 les da fila con la marca `huérfano` y **no los traduce**, porque §6.1 exige que el concepto exista antes que el nombre: quien los declare formalmente los agrega, y recién ahí entran al renombre.

### 5.4 Lo que no es frontera y no se discute: el dato del alumno

**El JSON que el alumno pega no se toca, y sus claves no son un identificador de este producto.** `Tipo`, `Tapas`, `Bases`, `Radio`, `Largo`, `Ancho`, `Area`, `Volumen`, y los valores `Cilindro`, `Cubo`, `Ortoedro`, `Rectangulo`, `Cuadrado`, `Circulo`, `RectanguloDesarrollado`.

Los emite el programa de escritorio de la Actividad 1, que **no forma parte de este producto**: el intake §17.1.P.10 lo dice con todas las letras —«eso es de la Actividad 1, que es el emisor del dato y no forma parte de este producto»— y la decisión `D-1` fija que el JSON se acepta **tal como lo emite su programa**, con sus comas finales y su clave `Tapas`. **30 documentos, 201 ocurrencias** para los siete tipos de figura.

**Consecuencia para la norma:** el tipo de C# que lee ese JSON se llama en inglés y **mapea explícitamente** al nombre castellano de la clave. La traducción vive en el mapeo, declarada, en un solo lugar — que es precisamente donde el producto ya decidió que viven las trampas del formato. §6.4 trae las quince filas con su marca `no se renombra`.

## 6. El glosario de correspondencia

### 6.1 La regla del glosario

> **Si un concepto del dominio no está en el glosario de §6.3 a §6.8, no se traduce por criterio propio: se agrega primero a la tabla que le corresponde y recién después se escribe el identificador.**

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
| **Total** | **155** | **155** | — |

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

## 7. Cómo se verifica esta norma

Una norma sin instrumento de verificación es una intención. Cinco controles, y los cinco son mecánicos:

| # | Control | Cuándo | Qué detecta |
| --- | --- | --- | --- |
| `V-1` | **Recuento de identificadores fuera del glosario.** Todo identificador de código declarado en el corpus tiene que resolver contra una fila de §6.3 a §6.8 | En cada auditoría de categoría 05 y en el punto de control de cada etapa | Un concepto traducido por criterio propio, que es lo que §6.1 prohíbe |
| `V-2` | **Inspección de idioma de identificador.** Ningún identificador de código nuevo en castellano | En cada emisión que declare un identificador | La reaparición de la desviación por el mismo camino por el que apareció |
| `V-3` | **Cuadre de la etiqueta.** Todo valor de conjunto cerrado tiene identificador **y** etiqueta, y la etiqueta está en castellano | Al construir `GeometriaFactory.Web.Services` | Un identificador inglés que se filtró a la pantalla |
| `V-4` | **Cuadre del renombre, en las dos direcciones.** Después de cada tramo de §8: cero ocurrencias del identificador viejo y tantas del nuevo como había del viejo | Al cerrar cada tramo de §8 | Un renombre a medias, que es el modo de falla que el corpus ya mostró con la sexta función de la fachada |
| `V-5` | **Catálogo contra tipo, no contra nombre.** La prueba de inspección de `ADR-06` de `GeometriaFactory-Application` compara los códigos emitidos contra **su** catálogo, no contra el conjunto de nombres | Al construir `GeometriaFactory-Application` | Los cuatro homónimos de §6.9 leídos como si fueran el mismo código |

**Y una condición previa que hay que decir:** hasta el 2026-08-12 **ninguna de las 33 auditorías del corpus verificó el idioma de un identificador**, porque ninguna invariante lo pedía. La invariante `D1` que todas ejercen dice «idioma español rioplatense neutro técnico» y se refiere **a la prosa**. `V-2` es el control que faltaba, y es la única razón por la que la desviación pudo propagarse a 334 documentos sin que nadie la nombrara.

## 8. El plan de renombre

**Esta norma no ejecuta el renombre. Lo ordena.** El renombre es una tanda posterior y se ejecuta **contra el glosario de §6**, nunca contra el criterio de quien edita: es la única forma de que la misma cosa no termine con tres nombres, que es exactamente lo que pasaría si cada tramo tradujera por su cuenta.

**Tres reglas que gobiernan los cinco tramos.**

1. **Un tramo, una clase, un pull request.** No se mezclan clases en un mismo tramo: el cuadre de `V-4` deja de ser mecánico si en la misma edición cambiaron dos poblaciones.
2. **Se renombra de menor a mayor alcance.** Los tramos baratos primero, no por comodidad, sino porque validan el procedimiento sobre una población chica antes de aplicarlo sobre 396 documentos.
3. **Ningún tramo empieza si el anterior no cuadró.** `V-4` es bloqueante.

| Tramo | Qué renombra | Alcance contado | Proyectos alcanzados | Qué se verifica al cerrarlo |
| --- | --- | --- | --- | --- |
| **`R-1`** | **Los propuestos**: los 18 espacios de nombres de §6.10, los 14 tipos y adaptadores de §6.4, los 6 derivados, y los 2 puertos propuestos de §6.3 | **1 documento** —[`Plan-Etapa-A.md`](Plan-Etapa-A.md)— y **41 ocurrencias** (§2.3) | Ninguno: son propuesta, nada los cita | `V-1` y `V-2` sobre `Plan-Etapa-A.md`. `V-4`: 0 ocurrencias viejas. **Es el tramo de ensayo**: valida el procedimiento con costo de una edición |
| **`R-2`** | **Clases 1 y 3**: los 3 puertos declarados de §6.3 y los 2 miembros de §6.5 | **5 identificadores**, 12 + 3 documentos, 56 + 8 ocurrencias (§2.2) | `Domain`, `Application`, `Infrastructure`, y el intake §17.1.P.5, §17.2.P.1, §17.3.P.4 | `V-4` por identificador. **El intake se toca acá y no antes**, porque es la fuente y su cambio arrastra a los siete proyectos |
| **`R-3`** | **Clase 4**: las 6 funciones de la fachada (§6.6) | **52 documentos, 593 ocurrencias**; 21 documentos llevan las seis | `Visor` (02, 03, 05, 10), `Web` (02, 03, 05, 10), el intake §14, §17.6.P.3, §17.7.P.3, §18 | `V-4` por función, **y el recuento de «6 de 6»**: los 21 documentos que declaran las seis tienen que seguir declarando seis. Es el conjunto que ya envejeció mal tres veces |
| **`R-4`** | **Clase 5**: los 10 valores de conjunto cerrado (§6.7), **con su etiqueta** | **396 documentos, 4259 ocurrencias**; sólo `Pendiente` son 349 documentos y 1919 ocurrencias | Los siete, y el intake §4.2, §12, §17.1.P.2, §17.3.P.4 | `V-3` **en cada documento que muestre el valor a una persona**, y `V-4` por valor. **`Pendiente` se verifica en dos direcciones separadas**, una por concepto: ninguna ocurrencia puede quedar sin decidir si era `Pending` o `Submitted` |
| **`R-5`** | **Clase 6**: los 101 códigos (§6.8) | **334 documentos, 2911 ocurrencias**; los 21 de contrato son 220 documentos y 1201 ocurrencias | Los siete. Los seis catálogos `03-UX-UI-DX/DX-Error-Messages.md` son la fuente y se renombran **primero** | `V-4` por código, `V-5` sobre los cuatro homónimos de §6.9, y la verificación en las dos direcciones de `ADR-06`. **Condición de entrada: los dos huérfanos de §6.8.7 tienen que estar resueltos** —declarados o corregida la cita— antes de empezar |

**El orden dentro de `R-5`, porque 101 códigos en una sola pasada no se cuadran.** Cinco pasos, uno por catálogo, en orden de dependencia: `Domain` (42), después `Application` (los 12 propios), después `Infrastructure` (los 15 propios), después `Visor` (7), y **`Contracts` al final** (21), que es el único que cambia el contrato y el que arrastra a `Api` y a `Web` en la misma edición, por `RT-06`.

**Lo que el renombre no toca, y conviene tenerlo a la vista mientras se ejecuta:** la prosa (§4), los identificadores documentales `CU-XX`, `RN-XX`, `BT-XX`, `ADR-XX`, `E-VIS-XX`, `DXT-XX`, `DXC-XX`, el nombre del producto, el de los siete proyectos de código, y **el dato del alumno de §5.4**. Un tramo que toque cualquiera de esos se detiene y se revierte.

## 9. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.1 | 2026-08-12 | **Las tres zonas de frontera dejan de ser propuestas elevadas y pasan a decisiones tomadas**, con su fecha, su fundamento y su costo contado (§5): **`F-01a`**, las seis funciones de la fachada del visor van a inglés, con el fundamento decisivo de que el intake §17.7.P.3 las declaraba «a fijar en la etapa que la implementa» y por lo tanto **nunca estuvieron fijadas**; **`F-02a`**, los conjuntos cerrados llevan identificador en inglés y etiqueta en castellano, con el fundamento de que deshace la colisión ya declarada de `Pendiente` —que hoy nombra dos cosas y en inglés se separa en `Pending` y `Submitted`— y con la ventana de costo cero mientras no haya base poblada; y **`F-03`**, **todos** los códigos de condición van a inglés, los 80 internos y los 21 de contrato, por consistencia total, con el fundamento de que el producto no emitió una sola respuesta todavía y los dos consumidores compilan juntos. **`F-03` se declara explícitamente como cambio de contrato** y no como renombre, con la regla operativa `RT-06` —los dos extremos se cambian y se despliegan juntos— y con la eliminación del prefijo `CONTRATO_`. **El glosario pasa de 42 conceptos a las seis clases completas: 155 identificadores en 155 filas** (§6.2 a §6.8), con el recuento rehecho sobre los seis catálogos y el desglose que reconcilia los 101 códigos —76 internos vivos + 4 retirados + 21 de contrato—. Suma **la regla operativa** de que un concepto no listado no se traduce por criterio propio sino que se agrega primero, sus cuatro corolarios y la convención exacta de forma de los códigos; **dos unificaciones** de conceptos que el corpus nombra hoy dos veces —`CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA` con `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR`, y las cinco tablas en mayúsculas del intake con las cinco entidades— y **cuatro homónimos declarados** que el retiro del prefijo produce (§6.9); y **dos huérfanos que no se traducen**, `CONTRATO_CAMBIO_DE_CONTRASENA_PENDIENTE` y `CONTRATO_RESETEO_NO_ADMITIDO`, que tres casos de uso de `GeometriaFactory-Web` citan y ningún catálogo declara. Agrega **`V-4`** —cuadre del renombre en las dos direcciones— y **`V-5`** —catálogo contra tipo y no contra nombre— a §7, y **§8, el plan de renombre en cinco tramos** con su alcance contado, sus proyectos alcanzados y lo que se verifica al cerrar cada uno, del más barato al más caro. El documento pasa de `Propuesto` a **`Aprobado`**. **No renombra nada**: el renombre es la tanda que ejecuta §8. | Product Owner (las tres decisiones) · Orquestador SDD (recuento, glosario y redacción) |
| 1.0 | 2026-08-12 | **Emisión inicial**, a pedido del Product Owner, que observó que el corpus nombra identificadores de código en castellano contra el estándar. Fija las dos zonas que no se discuten —**identificadores de código en inglés** (§3) y **texto en castellano** (§4)— y separa las tres **zonas de frontera** que no decide y eleva: `F-01` las seis funciones de la fachada del visor, `F-02` los diez valores de los cuatro conjuntos cerrados, `F-03` los 101 códigos de condición y de contrato, cada una con propuesta, costo contado y alternativa real. Emite el **glosario de correspondencia** con 42 conceptos derivables de la especificación, la regla de que **un concepto no listado no se traduce por criterio propio**, y la correspondencia de los 18 espacios de nombres. Cuenta el alcance real sobre **631 archivos** de `SDD/` excluidos `_legacy/` y `Docs/Audit/`: **396 documentos y 4259 ocurrencias** de valores de conjunto cerrado, **334 documentos y 2911 ocurrencias** de códigos de condición, **52 documentos y 593 ocurrencias** de la fachada. Declara **dos hechos verificados que cambian la discusión**: que el intake §17.7.P.3 nunca fijó los nombres de la fachada sino que los dejó «a fijar en la etapa que la implementa», contra lo que `Plan-Etapa-A.md` §1.2 afirma; y que **ninguna de las 33 auditorías** verificó jamás el idioma de un identificador. **No renombra nada.** | Product Owner (observación) · Orquestador SDD (medición y redacción) |
