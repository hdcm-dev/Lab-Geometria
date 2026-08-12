# Norma de nomenclatura — Fábrica de Geometría

**Producto:** Fábrica de Geometría
**Documento:** Norma-De-Nomenclatura.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-12
**Autor:** Orquestador SDD
**Nivel:** Producto
**Origen:** Observación del Product Owner, 2026-08-12: el estándar nombra espacios de nombres, clases y variables en inglés, y el corpus se salió del estándar sin declararlo
**Trazabilidad upstream:** [`../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) §13, §17.1.P.11 (que declara **abiertos** los nombres de tipos y espacios de nombres), §17.2.P.1, §17.3.P.4, §17.7.P.3 (que declara los nombres de la fachada **a fijar en la etapa que la implementa**); [`../Handoff-Checkout.md`](../Handoff-Checkout.md) §6.2 `A-1` y `A-2`; [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.2 a §1.7
**Trazabilidad downstream:** el punto de control de la etapa `a`; las siete categorías `05-Arquitectura-Tecnica`; [`../Audit/Observacion-Desviacion-De-Nomenclatura.md`](../Audit/Observacion-Desviacion-De-Nomenclatura.md)

---

## Tabla de contenido

- [1. Qué fija esta norma y qué no](#1-qué-fija-esta-norma-y-qué-no)
- [2. El alcance real, contado](#2-el-alcance-real-contado)
  - [2.1 Cómo se contó](#21-cómo-se-contó)
  - [2.2 Las seis clases](#22-las-seis-clases)
  - [2.3 Lo que el recuento decide](#23-lo-que-el-recuento-decide)
- [3. Zona 1 · Identificadores de código, en inglés](#3-zona-1--identificadores-de-código-en-inglés)
- [4. Zona 2 · Texto, en castellano](#4-zona-2--texto-en-castellano)
- [5. Zona de frontera · Tres decisiones del Product Owner](#5-zona-de-frontera--tres-decisiones-del-product-owner)
  - [5.1 `F-01` Las seis funciones de la fachada del visor](#51-f-01-las-seis-funciones-de-la-fachada-del-visor)
  - [5.2 `F-02` Los valores de los conjuntos cerrados](#52-f-02-los-valores-de-los-conjuntos-cerrados)
  - [5.3 `F-03` Los códigos de condición y de contrato](#53-f-03-los-códigos-de-condición-y-de-contrato)
  - [5.4 Lo que no es frontera y no se discute: el dato del alumno](#54-lo-que-no-es-frontera-y-no-se-discute-el-dato-del-alumno)
- [6. El glosario de correspondencia](#6-el-glosario-de-correspondencia)
  - [6.1 La regla del glosario](#61-la-regla-del-glosario)
  - [6.2 Tabla](#62-tabla)
  - [6.3 Los espacios de nombres](#63-los-espacios-de-nombres)
- [7. Cómo se verifica esta norma](#7-cómo-se-verifica-esta-norma)
- [8. Control de cambios](#8-control-de-cambios)

---

## 1. Qué fija esta norma y qué no

**Fija el idioma de los identificadores de código y el idioma del texto, y separa las dos cosas.** Hasta hoy el producto no tenía una norma de nomenclatura: tenía una práctica. La práctica nació de transcribir el material del Product Owner y se propagó hasta parecer una convención; el detalle de cómo ocurrió está en [`../Audit/Observacion-Desviacion-De-Nomenclatura.md`](../Audit/Observacion-Desviacion-De-Nomenclatura.md).

**No decide la zona de frontera.** Las tres decisiones de §5 son del Product Owner. Este documento las presenta con su costo contado y su alternativa real, que es lo que hace posible decidirlas; no las toma.

**No renombra nada.** Ninguna emisión de esta tanda modifica un identificador del corpus. Renombrar es un acto posterior a la decisión de §5, y su alcance es el de §2.

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

## 5. Zona de frontera · Tres decisiones del Product Owner

Las tres tienen la misma forma: **un identificador que también es dato**. Se persiste, o viaja en una respuesta, o lo invoca otro extremo. Cambiarlo no es renombrar: es cambiar un contrato.

**Ninguna se decide en este documento.** Cada una trae su propuesta, su costo contado y su alternativa real.

### 5.1 `F-01` Las seis funciones de la fachada del visor

**Qué son.** `inicializar`, `cargarJson`, `seleccionarPieza`, `redimensionar`, `destruir`, `establecerMovimiento`. Son la superficie pública del *bundle* de TypeScript, expuesta como biblioteca en `window`, y **Blazor las invoca por interoperabilidad** contra `IJSRuntime`. Son el punto de extensión principal del producto: el sample `S-1` las ejerce enteras sin backend, que es lo que hace reemplazable al motor 3D.

**Costo contado.** **52 documentos** las nombran; **21** declaran las seis; **593 ocurrencias** en total. Los documentos que fijan su contrato son [`../Proyectos/GeometriaFactory-Visor/02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../Proyectos/GeometriaFactory-Visor/02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md), su [`ADR-02`](../Proyectos/GeometriaFactory-Visor/05-Arquitectura-Tecnica/Adrs/ADR-02-Superficie-De-Seis-Funciones-Planas.md), el intake §17.7.P.3, y las categorías 02, 03, 05 y 10 de `GeometriaFactory-Visor` y `GeometriaFactory-Web`.

**El hecho que cambia la discusión, y hay que leerlo antes de decidir.** El intake §17.7.P.3 encabeza su tabla así: «Contrato de la fachada, **con los nombres definitivos a fijar en la etapa que la implementa** (RT §8.4)». **Los nombres de la fachada nunca estuvieron fijados.** [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.2 punto 5 afirma que «están fijadas por el intake §17.7.P.3», y eso contradice la letra de la fuente que cita. Fijarlos es, literalmente, lo que la etapa que implementa el visor debe hacer.

**Propuesta `F-01a` — se renombran a inglés.** `initialize`, `loadJson`, `selectPiece`, `resize`, `destroy`, `setMotion`.

- **A favor:** es la única de las tres fronteras donde **la fuente pide expresamente que se fijen ahora**; TypeScript en `camelCase` inglés es la convención universal de una biblioteca de navegador; no hay ningún consumidor fuera de este producto, porque el *bundle* sólo lo invoca `GeometriaFactory-Web` de la misma solución; y el momento es el más barato que va a haber, porque **el visor todavía no existe como código**.
- **Costo:** 52 documentos a tocar, de los cuales 21 llevan las seis. Es un renombre mecánico, verificable con recuento en las dos direcciones, sin ninguna decisión por documento.

**Alternativa `F-01b` — quedan en castellano, declarado como apartamiento.** Costo: la única superficie pública del producto queda en un idioma distinto del de todo el resto del código, y el ensamblado que la invoca —`GeometriaFactory.Web.Integration`— tiene que traducir en el punto de invocación, que es exactamente el defecto que `RI-06` de [`Vista-Producto.md`](Vista-Producto.md) §7 declara con historia en este producto. A favor: cero ediciones.

**Recomendación del orquestador: `F-01a`.** Es la frontera con más argumento y menos riesgo: la fuente lo pide, no hay consumidor externo, y el código no está escrito.

### 5.2 `F-02` Los valores de los conjuntos cerrados

**Qué son.** Cuatro conjuntos: papel (`Alumno`, `Administrador`), estado de cuenta (`Pendiente`, `Habilitado`, `Bloqueado`), estado del trabajo (`Borrador`, `Pendiente`, `Finalizado`, `Rechazado`) y especie de observación (`Advertencia`, `Error de validación`). **Diez identificadores distintos.**

**Por qué son frontera, y no es una sutileza.** Se persisten y se serializan **por su nombre, nunca por su posición** — [`../Proyectos/GeometriaFactory-Api/05-Arquitectura-Tecnica/Contratos-REST.md`](../Proyectos/GeometriaFactory-Api/05-Arquitectura-Tecnica/Contratos-REST.md) §2.2, y [`../Proyectos/GeometriaFactory-Infrastructure/05-Arquitectura-Tecnica/Modelo-Datos-Logico.md`](../Proyectos/GeometriaFactory-Infrastructure/05-Arquitectura-Tecnica/Modelo-Datos-Logico.md) §2.1 y §2.2, que los guarda como texto. El identificador **es** el dato guardado y el dato transmitido. Y además el alumno lo ve traducido en pantalla.

**Costo contado.** **396 documentos, 4259 ocurrencias.** Es la clase más grande del corpus. Sólo `Pendiente` son 349 documentos y 1919 ocurrencias.

**Propuesta `F-02a` — identificador en inglés, etiqueta en castellano, y la traducción en un solo lugar.**

| Conjunto | Identificador | Etiqueta que ve la persona |
| --- | --- | --- |
| `Role` | `Student`, `Administrator` | «Alumno», «Administrador» |
| `AccountStatus` | `Pending`, `Enabled`, `Blocked` | «Pendiente», «Habilitado», «Bloqueado» |
| `WorkStatus` | `Draft`, `Submitted`, `Approved`, `Rejected` | «Borrador», «Pendiente», «Finalizado», «Rechazado» |
| `ObservationKind` | `Warning`, `ValidationError` | «Advertencia», «Error de validación» |

- **A favor, y es el argumento fuerte:** deshace la colisión de `Pendiente`. En inglés el estado de cuenta es `Pending` y el del trabajo es `Submitted`, que es lo que cada uno significa —el trabajo se envió y espera revisión, la cuenta espera habilitación— y son palabras distintas. La forma calificada obligatoria que el modelo de dominio tuvo que inventar deja de hacer falta.
- **Costo, y es alto:** 396 documentos. Y hay un costo que no es documental: **el identificador es el dato persistido**, de modo que una base ya poblada exigiría una transformación de esquema. Hoy no hay ninguna base poblada —`GeometriaFactory-Infrastructure` no está construido—, así que ese costo es **cero si se decide ahora** y deja de serlo después.
- **Y un costo que hay que decir:** obliga a que exista un traductor de etiqueta. Ya existe: [`Plan-Etapa-A.md`](Plan-Etapa-A.md) §1.3 declara un componente `Servicios` en `GeometriaFactory-Web` que incluye «traductor», y el catálogo de condiciones ya establece que el texto para la persona lo compone quien expone. La norma lo aprovecha; no lo inventa.

**Alternativa `F-02b` — quedan en castellano, y se declara que el identificador es también la etiqueta.** A favor: cero ediciones, cero traductor, y el valor guardado se lee igual que el valor mostrado, que en un producto didáctico tiene mérito propio. Costo: la colisión de `Pendiente` queda para siempre, con la forma calificada obligatoria como parche permanente; y el producto queda con un tipo `WorkStatus` cuyos miembros son `Borrador` y `Finalizado`, que es media traducción y suele envejecer peor que ninguna.

**Alternativa `F-02c` — identificador en inglés y valor serializado en castellano**, con una anotación de serialización por miembro. Se desaconseja: introduce dos nombres para el mismo valor y una configuración que los dos extremos tienen que compartir, que es exactamente lo que `Contratos-REST.md` §2.2 eligió evitar al fijar «nombres de campo tal como los declara el tipo, sin transformación de estilo».

### 5.3 `F-03` Los códigos de condición y de contrato

**Qué son.** **101 identificadores distintos** en seis catálogos: 42 de `GeometriaFactory-Domain`, 36 de `GeometriaFactory-Application`, 17 de `GeometriaFactory-Infrastructure`, 7 de `GeometriaFactory-Visor`, y **21 con prefijo `CONTRATO_`** que son los de la frontera HTTP, declarados por `GeometriaFactory-Contracts` y traducidos por `GeometriaFactory-Api`.

**Por qué son frontera.** Son constantes —identificadores— pero **los `CONTRATO_*` viajan dentro de las respuestas** y los cita `GeometriaFactory-Web` para decidir qué le muestra a la persona. Renombrarlos es cambiar el contrato, no renombrar un símbolo. Y los internos son la base de una verificación declarada: `ADR-06` de `GeometriaFactory-Application` exige una prueba de inspección que compare los códigos emitidos contra el catálogo **en las dos direcciones**.

**Costo contado.** **334 documentos, 2911 ocurrencias.** Los `CONTRATO_*` solos: 220 documentos, 1201 ocurrencias.

**Propuesta `F-03` — se dividen en dos, porque no son una sola cosa.**

| Sub-clase | Propuesta | Fundamento |
| --- | --- | --- |
| **Los 80 códigos internos** | **Se renombran a inglés**, `SCREAMING_SNAKE_CASE`: `REQUIRED_FIELD_MISSING`, `WORK_NOT_IN_DRAFT`, `ACCOUNT_NOT_ENABLED`… | No cruzan ninguna frontera de proceso: nacen y mueren dentro de la solución, que compila junta. Son identificadores puros y les corresponde §3 sin excepción |
| **Los 21 `CONTRATO_*`** | **Se renombran a inglés y se elimina el prefijo en castellano**: `OPERATION_ADMIN_ONLY`, `STATE_FORBIDS_UPDATE`… | El contrato lo consumen **dos proyectos de código de esta misma solución, compilados juntos** — es la razón por la que el intake §17.4.P.12 descartó generar clientes desde una descripción formal. **No hay ningún consumidor externo, y el producto todavía no emitió una sola respuesta.** Cambiar el contrato hoy no rompe nada; después sí |

**Costo:** 334 documentos. Es el renombre más caro de los tres y el más mecánico: no hay ninguna decisión por documento, sólo correspondencia uno a uno, y los seis catálogos son la fuente única contra la que se verifica.

**Alternativa `F-03b` — quedan como están, y se declara el apartamiento.** A favor: cero ediciones sobre 334 documentos, y los catálogos están cerrados, verificados y auditados en su forma actual. Costo: el producto queda con una regla partida —identificadores en inglés salvo estos 101— que hay que explicar cada vez, y la explicación es histórica y no técnica.

**Alternativa `F-03c` — sólo los 80 internos, y los 21 `CONTRATO_*` quedan.** Es la salida intermedia honesta: renombra lo que es puramente interno y respeta que un contrato declarado es un contrato. Costo: `GeometriaFactory-Api` traduce de un catálogo inglés a uno castellano en la frontera, y esa traducción es una tabla más que mantener. Ventaja: reduce el alcance de 334 documentos a 318, que **no es una reducción real** — porque los mismos documentos citan las dos familias. Decir esto es parte de la propuesta: `F-03c` cuesta casi lo mismo que `F-03` y compra menos.

**Un hallazgo lateral, que se declara acá porque salió del recuento.** Dos identificadores `CONTRATO_*` —`CONTRATO_CAMBIO_DE_CONTRASENA_PENDIENTE` y `CONTRATO_RESETEO_NO_ADMITIDO`— aparecen en **tres casos de uso de `GeometriaFactory-Web`** y **no figuran en el catálogo de `GeometriaFactory-Contracts` ni en el de `GeometriaFactory-Api` ni en `Contratos-REST.md`**. Es un defecto de fondo preexistente y ajeno al idioma; se anota para que quien decida `F-03` sepa que el conjunto real citado es 23 y no 21.

### 5.4 Lo que no es frontera y no se discute: el dato del alumno

**El JSON que el alumno pega no se toca, y sus claves no son un identificador de este producto.** `Tipo`, `Tapas`, `Bases`, `Radio`, `Largo`, `Ancho`, `Area`, `Volumen`, y los valores `Cilindro`, `Cubo`, `Ortoedro`, `Rectangulo`, `Cuadrado`, `Circulo`, `RectanguloDesarrollado`.

Los emite el programa de escritorio de la Actividad 1, que **no forma parte de este producto**: el intake §17.1.P.10 lo dice con todas las letras —«eso es de la Actividad 1, que es el emisor del dato y no forma parte de este producto»— y la decisión `D-1` fija que el JSON se acepta **tal como lo emite su programa**, con sus comas finales y su clave `Tapas`. **30 documentos, 201 ocurrencias** para los siete tipos de figura.

**Consecuencia para la norma:** el tipo de C# que lee ese JSON se llama en inglés y **mapea explícitamente** al nombre castellano de la clave. La traducción vive en el mapeo, declarada, en un solo lugar — que es precisamente donde el producto ya decidió que viven las trampas del formato.

## 6. El glosario de correspondencia

### 6.1 La regla del glosario

> **Si un concepto del dominio no está en la tabla de §6.2, no se traduce por criterio propio: se agrega primero a esta tabla y recién después se escribe el identificador.**

Y sus tres corolarios, que son lo que hace que la regla sirva:

1. **Un concepto, un identificador.** No se admiten dos traducciones del mismo concepto en dos proyectos de código. Si aparecen, una está mal y se corrige acá, no en la hoja.
2. **Agregar a la tabla es un acto declarado**, con su fila de control de cambios en §8. Quien traduce sin agregar produce un identificador que nadie puede verificar, y ése es el defecto que esta norma existe para impedir.
3. **La tabla es la fuente única de la correspondencia.** Ningún otro documento la redeclara; los demás la citan.

### 6.2 Tabla

Los identificadores propuestos son **propuesta**; el estado de este documento es `Propuesto` y las tres decisiones de §5 pueden cambiar filas enteras. Se emite con los conceptos que ya existen y son derivables de la especificación, y no con ninguno inventado para completarla.

| Concepto del dominio | Identificador propuesto | De dónde sale el concepto |
| --- | --- | --- |
| Cuenta | `Account` | `Modelo-Conceptual.md` §3.1 y §3.2; `Modelo-Datos-Logico.md` §2.1 |
| Alumno (papel) | `Student` | `Definicion-Modelo-De-Dominio.md` §2.1 |
| Administrador (papel) | `Administrator` | `Definicion-Modelo-De-Dominio.md` §2.1 |
| Papel | `Role` | `Definicion-Modelo-De-Dominio.md` §2.1 |
| Estado de cuenta | `AccountStatus` | `Definicion-Modelo-De-Dominio.md` §2.1; `Modelo-Datos-Logico.md` §2.1 |
| Pendiente (cuenta) | `Pending` | `Definicion-Modelo-De-Dominio.md` §5.1 |
| Habilitado | `Enabled` | `Definicion-Modelo-De-Dominio.md` §5.1 |
| Bloqueado | `Blocked` | `Definicion-Modelo-De-Dominio.md` §5.1 |
| Credencial derivada | `PasswordHash` | Intake §17.1.P.5, que ya la nombra `HashContrasena` |
| Marca de cambio de contraseña pendiente | `MustChangePassword` | `Definicion-Modelo-De-Dominio.md` §2.1; RN-12, RN-16 |
| Correo escrito | `Email` | `Modelo-Datos-Logico.md` §2.1 |
| Correo normalizado | `NormalizedEmail` | `Modelo-Datos-Logico.md` §2.1; `Infrastructure ADR-03` |
| Trabajo | `Work` | `Definicion-Modelo-De-Dominio.md` §2.2 |
| Estado del trabajo | `WorkStatus` | `Definicion-Modelo-De-Dominio.md` §5.2 |
| Borrador | `Draft` | `Definicion-Modelo-De-Dominio.md` §5.2 |
| Pendiente (trabajo) | `Submitted` | `Definicion-Modelo-De-Dominio.md` §5.2. **Deliberadamente distinto de `Pending`**: son dos conceptos y el castellano los colapsa |
| Finalizado | `Approved` | `Definicion-Modelo-De-Dominio.md` §5.2: es el desenlace de aprobación del administrador |
| Rechazado | `Rejected` | `Definicion-Modelo-De-Dominio.md` §5.2 |
| Texto original | `OriginalJson` | Intake §17.3.P.4, que ya lo nombra `JsonOriginal` |
| Fecha declarada por el alumno | `DeclaredDate` | `Modelo-Datos-Logico.md` §2.2 |
| Momento de creación | `CreatedAt` | `Modelo-Datos-Logico.md` §2.2 |
| Momento de última modificación | `UpdatedAt` | `Modelo-Datos-Logico.md` §2.2 |
| Comentario del administrador | `AdministratorComment` | `Definicion-Modelo-De-Dominio.md` §2.2 |
| Cantidad de figuras del conjunto raíz | `RootFigureCount` | `Definicion-Modelo-De-Dominio.md` §2.2; RN-09 |
| Pieza | `Piece` | `Definicion-Modelo-De-Dominio.md` §2.3 |
| Posición (identidad de la pieza) | `Position` | `Definicion-Modelo-De-Dominio.md` §2.3; intake §17.1.P.11 punto 2 |
| Área declarada / derivada | `DeclaredArea` / `DerivedArea` | `Definicion-Modelo-De-Dominio.md` §2.3; intake §17.1.P.11 punto 3 |
| Volumen declarado / derivado | `DeclaredVolume` / `DerivedVolume` | `Definicion-Modelo-De-Dominio.md` §2.3 |
| Componente | `Component` | `Definicion-Modelo-De-Dominio.md` §2.4. Es término del glosario del cliente y **no se renombra en el texto** |
| Observación | `Observation` | `Definicion-Modelo-De-Dominio.md` §2.5 |
| Especie de observación | `ObservationKind` | `Definicion-Modelo-De-Dominio.md` §2.5 |
| Advertencia | `Warning` | `Definicion-Modelo-De-Dominio.md` §2.5 |
| Error de validación | `ValidationError` | `Definicion-Modelo-De-Dominio.md` §2.5 |
| Repositorio de trabajos (puerto) | `IWorkRepository` | Intake §13, §14, §17.2.P.1, que lo nombra `IRepositorioTrabajos` |
| Repositorio de cuentas (puerto) | `IAccountRepository` | `Application ADR-02` §2; `Handoff-Checkout.md` §6.2 `A-1`, que sigue abierto |
| Validador de figuras (puerto) | `IFigureValidator` | Intake §17.2.P.1, que lo nombra `IValidadorFiguras` |
| Reloj del sistema (puerto) | `ISystemClock` | Intake §17.2.P.11 punto 3, que lo nombra `IRelojDelSistema` |
| Contexto de persistencia | `GeometriaFactoryDbContext` | `Plan-Etapa-A.md` §1.7 |
| Preparación del almacén | `StorePreparation` | `Plan-Etapa-A.md` §1.7; `Infrastructure ADR-07` |
| Composición de raíz | `CompositionRoot` | `Plan-Etapa-A.md` §1.7; `Api ADR-06` |
| Arranque en dos fases | `TwoPhaseStartup` | `Plan-Etapa-A.md` §1.7; `Api ADR-07` |
| Punto de salud | `HealthEndpoint` | `Plan-Etapa-A.md` §1.7 |

**Lo que la tabla no trae, y por qué.** No trae los 101 códigos de condición: son la decisión `F-03` y su correspondencia se emite **con la decisión**, en el mismo acto, tomando cada catálogo como fuente. Emitir 101 filas antes de saber si se renombran sería fabricar trabajo que puede no servir.

### 6.3 Los espacios de nombres

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

Una norma sin instrumento de verificación es una intención. Tres controles, y los tres son mecánicos:

| # | Control | Cuándo | Qué detecta |
| --- | --- | --- | --- |
| `V-1` | **Recuento de identificadores fuera del glosario.** Todo identificador de código declarado en el corpus tiene que resolver contra una fila de §6.2 | En cada auditoría de categoría 05 y en el punto de control de cada etapa | Un concepto traducido por criterio propio, que es lo que §6.1 prohíbe |
| `V-2` | **Inspección de idioma de identificador.** Ningún identificador de código nuevo en castellano, salvo los que §5 haya dejado explícitamente en castellano | En cada emisión que declare un identificador | La reaparición de la desviación por el mismo camino por el que apareció |
| `V-3` | **Cuadre de la etiqueta.** Todo valor de conjunto cerrado tiene identificador **y** etiqueta, y la etiqueta está en castellano | Al construir `GeometriaFactory.Web.Services` | Un identificador inglés que se filtró a la pantalla |

**Y una condición previa que hay que decir:** hasta hoy **ninguna de las 33 auditorías del corpus verificó el idioma de un identificador**, porque ninguna invariante lo pedía. La invariante `D1` que todas ejercen dice «idioma español rioplatense neutro técnico» y se refiere **a la prosa**. `V-2` es el control que faltaba, y es la única razón por la que la desviación pudo propagarse a 334 documentos sin que nadie la nombrara.

## 8. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-12 | **Emisión inicial**, a pedido del Product Owner, que observó que el corpus nombra identificadores de código en castellano contra el estándar. Fija las dos zonas que no se discuten —**identificadores de código en inglés** (§3) y **texto en castellano** (§4)— y separa las tres **zonas de frontera** que no decide y eleva: `F-01` las seis funciones de la fachada del visor, `F-02` los diez valores de los cuatro conjuntos cerrados, `F-03` los 101 códigos de condición y de contrato, cada una con propuesta, costo contado y alternativa real. Emite el **glosario de correspondencia** de §6.2 con 42 conceptos derivables de la especificación, la regla de que **un concepto no listado no se traduce por criterio propio**, y la correspondencia de los 18 espacios de nombres. Cuenta el alcance real sobre **631 archivos** de `SDD/` excluidos `_legacy/` y `Docs/Audit/`: **396 documentos y 4259 ocurrencias** de valores de conjunto cerrado, **334 documentos y 2911 ocurrencias** de códigos de condición, **52 documentos y 593 ocurrencias** de la fachada. Declara **dos hechos verificados que cambian la discusión**: que el intake §17.7.P.3 nunca fijó los nombres de la fachada sino que los dejó «a fijar en la etapa que la implementa», contra lo que `Plan-Etapa-A.md` §1.2 afirma; y que **ninguna de las 33 auditorías** verificó jamás el idioma de un identificador. **No renombra nada.** | Product Owner (observación) · Orquestador SDD (medición y redacción) |
