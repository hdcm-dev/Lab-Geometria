# Plan de la etapa `a` — Esqueleto ambulante y verificación de viabilidad

**Producto:** Fábrica de Geometría
**Documento:** Plan-Etapa-A.md
**Versión:** 1.10
**Estado:** Propuesto
**Fecha:** 2026-08-13
**Nivel:** Producto
**Trazabilidad upstream:** [`Norma-De-Nomenclatura.md`](Norma-De-Nomenclatura.md) **1.2** §3, §4 y §5 (las decisiones que superan a `D-01`), §6.3, §6.4, §6.10 y §6.11 (el glosario contra el que se renombró), §7 (`V-4`, `V-6` y `V-7`) y §8 con §8.2, tramos `R-1` y `R-1b`; [`PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.29** §13, §14, §15, §16, §16.1 y §17.1 a §17.7; [`PRODUCT-MANIFEST-Fabrica-De-Geometria.md`](../../Intake/PRODUCT-MANIFEST-Fabrica-De-Geometria.md) **1.3** §1, §1.2, §2 y §3; [`../00-Contexto/Roadmap-Producto.md`](../00-Contexto/Roadmap-Producto.md) §2.2, §4 y §5; [`Pipeline-Producto.md`](Pipeline-Producto.md) §2, §6 y §9; [`Vista-Producto.md`](Vista-Producto.md) §3.1, §4 y §5; [`../Handoff-Checkout.md`](../Handoff-Checkout.md) §4 y §6; las **siete** categorías `05-Arquitectura-Tecnica` y las `09-Devops` bajo `Unidades-Entrega/`
**Trazabilidad downstream:** la rama de la etapa `a` y su solicitud de incorporación, que **es** el punto de control (`PRODUCT-INTAKE` §15, regla de delivery 4)

---

## Tabla de contenido

- [0. Qué es este documento y qué no es](#0-qué-es-este-documento-y-qué-no-es)
- [1. La decisión que le toca al Product Owner: los nombres](#1-la-decisión-que-le-toca-al-product-owner-los-nombres)
  - [1.1 Lo que NO está abierto: la solución y los siete proyectos](#11-lo-que-no-está-abierto-la-solución-y-los-siete-proyectos)
  - [1.2 `D-01` · Idioma y forma de los identificadores](#12-d-01--idioma-y-forma-de-los-identificadores)
  - [1.3 `D-02` · Esquema de espacios de nombres](#13-d-02--esquema-de-espacios-de-nombres)
  - [1.4 `D-03` · El nombre de la entidad de cuenta: una divergencia real del corpus](#14-d-03--el-nombre-de-la-entidad-de-cuenta-una-divergencia-real-del-corpus)
  - [1.5 `D-04` · El nombre del cuarto puerto](#15-d-04--el-nombre-del-cuarto-puerto)
  - [1.6 `D-05` · El criterio de nombrado de los adaptadores](#16-d-05--el-criterio-de-nombrado-de-los-adaptadores)
  - [1.7 `D-06` · Los nombres de los tipos centrales que el esqueleto necesita](#17-d-06--los-nombres-de-los-tipos-centrales-que-el-esqueleto-necesita)
  - [1.8 Las otras decisiones ancladas al mismo punto de control](#18-las-otras-decisiones-ancladas-al-mismo-punto-de-control)
- [2. El árbol de archivos exacto de la etapa `a`](#2-el-árbol-de-archivos-exacto-de-la-etapa-a)
  - [2.1 El árbol](#21-el-árbol)
  - [2.2 Qué es cada archivo](#22-qué-es-cada-archivo)
  - [2.3 Apartamientos declarados del árbol del intake §16](#23-apartamientos-declarados-del-árbol-del-intake-16)
- [3. El orden de construcción](#3-el-orden-de-construcción)
- [4. Los guiones que la etapa necesita](#4-los-guiones-que-la-etapa-necesita)
- [5. Cómo se verifica el cierre de la etapa](#5-cómo-se-verifica-el-cierre-de-la-etapa)
  - [5.1 Lo que ya se midió corriendo, y con qué](#51-lo-que-ya-se-midió-corriendo-y-con-qué)
  - [5.2 El estado de cierre completo, con los ocho criterios propios cerrados](#52-el-estado-de-cierre-completo-con-los-ocho-criterios-propios-cerrados)
- [6. Lo que la etapa `a` NO hace](#6-lo-que-la-etapa-a-no-hace)
- [7. Riesgos y contradicciones que este plan eleva sin resolver](#7-riesgos-y-contradicciones-que-este-plan-eleva-sin-resolver)
- [8. Control de cambios](#8-control-de-cambios)

---

## 0. Qué es este documento y qué no es

**Es un plan y no es código.** Se emite antes de escribir la primera línea, porque la etapa `a` contiene una decisión que no es del equipo: los nombres. `Handoff-Checkout.md` §6.2 la registra como `A-1` y `A-2` y declara su titular; `Producto/11-Documentacion/README.md` §7 declara que **mientras esos nombres estén abiertos, ningún `Recorrido-Codigo.md` puede escribir una ruta verificable**, de modo que la decisión bloquea además todo el cuerpo documental de la categoría 11.

**Este documento propone y no decide.** Cada propuesta lleva su fundamento en la especificación, o el rótulo **[PROPUESTA SIN BASE DECLARADA]** cuando ninguna fuente la sostiene. Lo que ninguna fuente permite ni siquiera proponer está en §7 y se declara como tal en lugar de rellenarse.

**Estado del repositorio el 2026-08-12, verificado con `find`:** existen `SDD/`, `samples/` —diecinueve carpetas esqueletadas con su README y **cero** archivos de código—, `README.md` y `.gitignore`. **No hay `src/`, ni `tests/`, ni `visor/`, ni `deploy/`, ni `scripts/`, ni `.devcontainer/`, ni `.vscode/`, ni `.github/`, ni `GeometriaFactory.sln`, ni `changelog.md`.** La etapa `a` los crea todos.

**Carga de trabajo comprometida:** `Handoff-Checkout.md` §4 enumera **48 ítems** de etapa `a` sobre los siete proyectos de código —**42 tareas técnicas y 6 historias**—, repartidos 5 · 3 · 3 · 6 · 7 · 10 · 14. Sumados dan 48; contados por tipo, 42 + 6 = 48. Este plan no agrega ítems: los ordena.

---

## 1. La decisión que le toca al Product Owner: los nombres

### 1.1 Lo que NO está abierto: la solución y los siete proyectos

**Antes de proponer nada hay que decir qué ya está decidido, porque de otro modo el punto de control gastaría tiempo en confirmar lo aprobado.**

El nombre de la solución y los nombres, identidades de código y rutas de los siete proyectos **no son un punto abierto**: los declara `PRODUCT-INTAKE` §13 y §16 y los deriva `PRODUCT-MANIFEST` **1.3** §1, §1.2 y §2, cuyo estado es **`Aprobado`, confirmado por el Product Owner el 2026-08-08**. Se transcriben acá para que el punto de control los confirme de un vistazo y no los reabra:

| Qué | Valor declarado | Dónde se declara |
| --- | --- | --- |
| `Raiz-Codigo` | `GeometriaFactory` | Manifiesto §1 y §1.2; intake §13, perfil de convención. **Declarado, no derivado** |
| Separador de segmentos | `.` | Manifiesto §1.2 |
| Solución (`Artefacto-Agrupacion`) | **`GeometriaFactory.sln`**, en la raíz del repositorio | Manifiesto §1; intake §16 |
| Repositorio | `Lab-Geometria` | Intake §16. **Es el nombre del repositorio, no el del producto** |
| Proyecto principal | `GeometriaFactory-Api` | Manifiesto §1; intake §13 |

| # | `Nombre-Proyecto-Codigo` | `Identidad-Codigo` | Ruta | Tipo D8 |
| --- | --- | --- | --- | --- |
| 1 | `GeometriaFactory-Domain` | `GeometriaFactory.Domain` | `src/GeometriaFactory.Domain/` | `library` |
| 2 | `GeometriaFactory-Contracts` | `GeometriaFactory.Contracts` | `src/GeometriaFactory.Contracts/` | `library` |
| 3 | `GeometriaFactory-Visor` | `geometriafactory-visor` | `visor/` | `library` (paquete Node) |
| 4 | `GeometriaFactory-Application` | `GeometriaFactory.Application` | `src/GeometriaFactory.Application/` | `library` |
| 5 | `GeometriaFactory-Web` | `GeometriaFactory.Web` | `src/GeometriaFactory.Web/` | `web-monolith` |
| 6 | `GeometriaFactory-Infrastructure` | `GeometriaFactory.Infrastructure` | `src/GeometriaFactory.Infrastructure/` | `library` |
| 7 | `GeometriaFactory-Api` | `GeometriaFactory.Api` | `src/GeometriaFactory.Api/` | `rest-api` |

**La excepción de `GeometriaFactory-Visor` es declarada y no se reabre**: su identidad sigue la convención de `package.json` —minúscula con guiones— porque `GeometriaFactory.Visor` sería un nombre de paquete npm inválido, y su carpeta es `visor/` en la raíz y no bajo `src/` para que la solución .NET y el proyecto Node no compartan raíz de herramientas (intake §13; manifiesto §2).

**Los tres proyectos de prueba tampoco son un punto abierto**, y tampoco son proyectos de código del producto: el intake §16 los fija como `tests/GeometriaFactory.Domain.Tests/`, `tests/GeometriaFactory.Application.Tests/` y `tests/GeometriaFactory.Integration.Tests/`, y declara por qué no aparecen en §13.

**Entonces, qué queda realmente abierto.** Las seis filas `PA` que `Handoff-Checkout.md` §6.2 `A-2` cuenta —`PA-01` de `Domain` y `Contracts`, `PA-02` de `Visor`, `Application` e `Infrastructure`, `PA-07` de `Api`— **no dicen «los nombres de los proyectos»: dicen los nombres de los tipos y de los espacios de nombres**. `GeometriaFactory-Web` no tiene esa fila, y con fundamento declarado: su única superficie es la HTTP, cuyos nombres decide `GeometriaFactory-Api`. Eso es lo que este bloque propone, más el cuarto puerto de `Application` `PA-01`.

### 1.2 `D-01` · Idioma y forma de los identificadores

> **Esta decisión quedó tomada, y no como este documento la proponía.** El 2026-08-12 el Product Owner aprobó [`Norma-De-Nomenclatura.md`](Norma-De-Nomenclatura.md), cuyo §3 fija que **todo identificador de código va en inglés** y cuyo §4 fija que **el texto va en castellano**. Es la alternativa `P-1b` de esta sección la que rige, no la propuesta principal `P-1a`. Lo que sigue **se conserva como registro de la propuesta y de su fundamento**, con la marca de qué quedó superado y por qué: un argumento borrado deja la decisión sin por qué.

**Propuesta principal `P-1a` · SUPERADA el 2026-08-12 por la norma §3: identificadores de código en castellano, sin tildes ni eñes, `PascalCase` para tipos y miembros, prefijo `I` para los contratos que se implementan.**

Fundamento que se dio entonces, con su estado hoy:

1. Los **tres puertos que el intake sí declara** son `IWorkRepository`, `IFigureValidator` e `ISystemClock` (intake §13, §14 y §17.1.P.1 · GeometriaFactory-Application). ~~Están en castellano y llevan `I`.~~ **Superado el 2026-08-13 por la norma §6.3, y ya ejecutado:** el tramo `R-2` renombró **la fuente y este reporte con ella**, en la misma edición y por la norma §4.1 punto 1, de modo que el intake ya no los nombra en castellano y este renglón reporta lo que la fuente dice hoy. Del fundamento sobrevive **sólo el prefijo `I`**, que es forma y no idioma (norma §3).
2. Los **campos del modelo de datos que el intake nombra literalmente** son `PasswordHash` (§17.1.P.5 · GeometriaFactory-Domain) y `OriginalJson` (§17.1.P.4 · GeometriaFactory-Infrastructure). ~~Están en castellano, sin tildes, en `PascalCase`.~~ **Ídem, superado el 2026-08-13 por la norma §6.5 y ya ejecutado en el mismo tramo `R-2`:** la fuente y este reporte se renombraron juntos, y del fundamento sobrevive **sólo la forma `PascalCase`**, que no es idioma. La incomodidad que el fundamento arrastraba —escribir sin eñe ni tilde— desapareció con el idioma, como declara el párrafo de más abajo.
3. Las **cinco entidades** que el intake nombra son `ALUMNO`, `TRABAJO`, `PIEZA`, `COMPONENTE` y `OBSERVACION` (§17.1.P.4 · GeometriaFactory-Infrastructure), y `Definicion-Modelo-De-Dominio.md` §2 las declara en castellano. **Ídem:** la norma §6.4 y §6.9 **unifican** las cinco tablas en mayúsculas con las cinco entidades en cinco nombres ingleses —`Account`, `Work`, `Piece`, `Component`, `Observation`— en el tramo `R-2b`.
4. Los **conjuntos cerrados** están declarados por sus valores en castellano y se guardan y se serializan **por su nombre, nunca por su posición**: papel (`Alumno`, `Administrador`), estado de cuenta (`Pendiente`, `Habilitado`, `Bloqueado`), estado del trabajo (`Borrador`, `Pendiente`, `Finalizado`, `Rechazado`) y especie de observación (`Advertencia`, `Error de validación`) — `Contratos-REST.md` §2.2 y `Modelo-Datos-Logico.md` §2. **El hecho es cierto y por eso era zona de frontera, no fundamento:** la norma §5.2 lo decidió como `F-02a` —identificador en inglés, etiqueta en castellano— con costo cero mientras no haya base poblada, y su §6.7 trae los diez pares. El renombre es el tramo `R-4`.
5. ~~Las **seis funciones de la fachada del visor** están fijadas por el intake §17.2.P.3 · GeometriaFactory-Visor.~~ **Refutado por la norma §5.1, contra la letra de la fuente:** el intake §17.2.P.3 · GeometriaFactory-Visor encabeza su tabla «Contrato de la fachada, **con los nombres definitivos a fijar en la etapa que la implementa** (RT §8.4)». **Nunca estuvieron fijados.** La norma §5.1 los fija por `F-01a` y su §6.6 los declara: `initialize`, `loadJson`, `selectPiece`, `resize`, `destroy`, `setMotion`, en el tramo `R-3`. Se conserva la convención de `camelCase`, que es de TypeScript y no de idioma.

**Alternativa real `P-1b`: identificadores en inglés. Es la que rige desde el 2026-08-12** (norma §3). El costo que esta sección le atribuía —romper la coincidencia con los cinco grupos de arriba y obligar a traducir en cada punto de la cadena— **se pagó una sola vez y de un modo declarado**: la norma §6 emite el glosario de correspondencia completo, de modo que la traducción vive en **una tabla única** y no en cada punto de la cadena, que es exactamente la forma en que `Vista-Producto.md` §7 `RI-06` pide evitar el defecto. El plan de renombre de su §8 lo ejecuta en siete tramos contra ese glosario.

**La consecuencia que esta sección declaraba inevitable dejó de existir.** Decía que había que aceptar identificadores en castellano sin tildes —`Contrasena`, `Descripcion`, `Observacion`— y convivir con esa incomodidad. **La norma §3 lo declara beneficio lateral y no costo:** `PasswordHash`, `Description` y `Observation` no tienen el problema, porque en inglés no hay tilde ni eñe que sacar. Lo que no cambia es la otra mitad: **el texto que ve la persona lleva tildes** (norma §4), y todo valor de conjunto cerrado tiene etiqueta castellana además de identificador inglés (norma §5.2, control `V-3`).

**Regla de convivencia para el visor:** TypeScript conserva su propia convención —`camelCase` para funciones, `PascalCase` para clases—, porque el intake §13 ya declaró que ese proyecto no sigue la convención .NET.

### 1.3 `D-02` · Esquema de espacios de nombres

**Propuesta principal `P-2a`: espacio de nombres raíz igual a la `Identidad-Codigo`, y un solo nivel de subsegmento por área, con el nombre del área tomado del vocabulario que la categoría 05 de cada proyecto ya usa.**

```text
GeometriaFactory.Domain                        tipos raíz del dominio
GeometriaFactory.Domain.Entities               las cinco entidades
GeometriaFactory.Domain.Values                 los cuatro conjuntos cerrados
GeometriaFactory.Domain.Guards                 guardas y resultado tipado

GeometriaFactory.Contracts                     tipo de error y conjunto cerrado de códigos
GeometriaFactory.Contracts.Accounts            tipos de transferencia por familia
GeometriaFactory.Contracts.Works
GeometriaFactory.Contracts.Service             estado del servicio

GeometriaFactory.Application.Ports             los cuatro puertos
GeometriaFactory.Application.Accounts          orquestaciones de alta, gobierno e ingreso
GeometriaFactory.Application.Works             orquestaciones de trabajo, consulta y desenlace

GeometriaFactory.Infrastructure.Persistence    contexto, mapeos y transformaciones de esquema
GeometriaFactory.Infrastructure.Security       derivación de clave y acceso firmado
GeometriaFactory.Infrastructure.Validation     motor de interpretación y verificación
GeometriaFactory.Infrastructure.Time           adaptador de reloj

GeometriaFactory.Api.Endpoints                 los quince puntos de acceso
GeometriaFactory.Api.Composition               composición de raíz, arranque y salud

GeometriaFactory.Web.Components                armazón, superficies y representaciones
GeometriaFactory.Web.Services                  servicios de aplicación de front, sesión, traductor
GeometriaFactory.Web.Integration               cliente tipado y anfitrión del visor
```

**Tres reglas que acompañan la propuesta:**

1. **Un solo nivel de subsegmento.** Sin `…​.Entities.Accounts.Internal`. La profundidad crece por carpeta de archivo si hace falta, no por espacio de nombres.
2. **El espacio de nombres coincide con la carpeta.** Es lo que hace verificable la regla de `Recorrido-Codigo.md` de que toda ruta citada exista (`Producto/11-Documentacion/README.md` §7).
3. **El subsegmento no es la partición de componentes.** `Application/05` §3.1 declara expresamente que su partición en **ocho** componentes «es de responsabilidad y no de espacios de nombres»; `Web/05` §3.1 declara **ocho** componentes y `Infrastructure` los suyos. Los subsegmentos de arriba **agrupan** componentes, no los espejan uno a uno, y el mapeo se registra en el punto de control.

**Alternativa real `P-2b`: espacio de nombres plano, sólo la `Identidad-Codigo`.** Pro: cero decisiones, cero deriva entre carpeta y espacio de nombres. Contra, y es el costo concreto: `GeometriaFactory-Infrastructure` tiene **26 tareas técnicas** en su backlog y cinco áreas sin relación entre sí —persistencia, seguridad, validación, tiempo, arranque—; un solo espacio de nombres las mezcla, y la puerta de `Application` de **cero pruebas que tocan la base de datos real** (`BT-10006`, etapa `a`) queda sin una frontera de nombres donde apoyarse. Es viable en `Contracts` y en `Domain`; no lo es en `Infrastructure` ni en `Api`.

**Alternativa `P-2c`: subsegmento por capa de Clean Architecture dentro de cada proyecto** (`…​.Aplicacion`, `…​.Dominio`). Se descarta: el proyecto **ya es** la capa; el subsegmento repetiría el nombre del ensamblado.

### 1.4 `D-03` · El nombre de la entidad de cuenta: una divergencia real del corpus

**Este punto no está registrado como `PA-XX` en ninguna categoría y aparece al comparar dos documentos aprobados. Se eleva.**

| Fuente | Cómo nombra la entidad |
| --- | --- |
| `PRODUCT-INTAKE` §13 y §17.1.P.4 · GeometriaFactory-Infrastructure | Entidad **`ALUMNO`**, dentro de las cinco: `ALUMNO`, `TRABAJO`, `PIEZA`, `COMPONENTE`, `OBSERVACION` |
| `Domain/02` `Definicion-Modelo-De-Dominio.md` §2.1 | Entidad **`Alumno`**, con atributo `Role` que vale `Alumno` o `Administrador` |
| `Infrastructure/02` `Modelo-Conceptual.md` §3.1 y §3.2 | Entidad **`CUENTA`**, en el diagrama y en la tabla |
| `Infrastructure/05` `Modelo-Datos-Logico.md` §2.1 | Tabla **`Cuenta`**, «entidad conceptual de origen: **Cuenta**» |
| `Application/05` §3.1 y `ADR-04002` | «repositorio de **cuentas**», «orquestación del alta de **cuentas**», «gobierno de **cuentas**» |
| `Api` `Contratos-REST.md` §3 | «Listar las **cuentas** de la comisión», «cambiar la situación de una **cuenta**» |

**Verificado con `grep` el 2026-08-12 sobre el árbol vivo de `GeometriaFactory-Infrastructure`, excluido `_legacy/`: la cadena `ALUMNO` aparece en cuatro lugares y en los cuatro es un fragmento de un código del contrato, nunca un nombre de entidad ni de tabla.** El renombre a `Cuenta` ocurrió y no está declarado como apartamiento en ninguna parte.

**Propuesta principal `P-3a`: la entidad se llama `Cuenta`.** Fundamento: es el término que usan cinco de las seis fuentes de la tabla, es el que hace verdadero el invariante `INV-08` —la cuenta con papel `Administrador` no es un alumno y sin embargo es una fila de esa misma entidad— y es el que evita que el tipo `Alumno` tenga un miembro `Role` cuyo valor pueda ser `Administrador`, que es una contradicción legible en la primera línea del archivo.

**Alternativa `P-3b`: la entidad se llama `Alumno`.** Pro: es la letra del intake §13 y §17.1.P.4 · GeometriaFactory-Infrastructure, que es la fuente de mayor rango. Costo: obliga a corregir `Modelo-Conceptual.md`, `Modelo-Datos-Logico.md` y las tablas de trazabilidad que los citan, y deja el nombre del tipo en contradicción con su propio atributo `Role`.

**Cualquiera de las dos que se elija, la otra fuente queda con una corrección pendiente.** Este plan no puede corregir el intake: `Master-Prompt.md` §15 reserva ese acto a su autor.

### 1.5 `D-04` · El nombre del cuarto puerto

**Es el punto abierto que más documentos toca.** `Application/05` `PA-01` lo declara, `Infrastructure/05` `PA-01` declara que **no lo fija** porque no puede nombrar un tipo que no declara, `Application ADR-06002` §2 confirma que **el puerto existe** y deja el nombre abierto, y `Producto/11-Documentacion/README.md` §7 lo lista como uno de los seis puntos heredados que bloquean su cuerpo documental.

**Qué hace el puerto, declarado por `Application ADR-04002` §2 punto 1 y por su §3.1:** recuperar una cuenta por su correo, responder si un correo ya está registrado, responder si ya existe una cuenta con papel `Administrador`, y materializar el resultado incluida la marca de cambio de contraseña pendiente.

**Propuesta principal `P-4a`: `IAccountRepository`.**

Fundamento, y es literal: `Infrastructure ADR-06003` §6 punto 4 declara que la propuesta que llega al punto de control es que **el identificador del cuarto puerto siga el patrón de los tres que el intake sí declara**, y que esos tres «empiezan por la misma letra de contrato y nombran la cosa, no el mecanismo». Aplicado:

| Puerto | Identificador | Origen |
| --- | --- | --- |
| Repositorio de trabajos | `IWorkRepository` | **Declarado** por el intake §13, §14 y §17.1.P.1 · GeometriaFactory-Application |
| Validación de figuras | `IFigureValidator` | **Declarado** por las mismas |
| Reloj del sistema | `ISystemClock` | **Declarado** por las mismas |
| Repositorio de cuentas | **`IAccountRepository`** | **Propuesta** de este documento, por el criterio de `Infrastructure ADR-06003` §6 |

Además, `Application ADR-04002` §2 nombra al puerto en lenguaje de dominio como **«repositorio de cuentas»** y `Application/05` §3.1 lo repite en su componente de declaración de puertos. La propuesta es el mismo sintagma llevado a identificador, sin acuñar nada.

**Alternativa real `P-4b`: `IStudentRepository`.** Sólo tiene sentido si `D-03` se resuelve por `P-3b`. Costo: el puerto responde también por la cuenta con papel `Administrador` —`INV-05` exige saber si ya existe una, y ése es exactamente el puerto que lo responde según `ADR-04002` §5 punto 2—, de modo que un puerto llamado «de alumnos» que se consulta para saber si existe el administrador miente en su nombre. **`P-4a` y `P-3a` se sostienen mutuamente y se recomienda decidirlas juntas.**

**Alternativa `P-4c`: no abrir el cuarto puerto y resolver la unicidad dentro de `IWorkRepository`.** Está considerada y **descartada** por `Application ADR-04002` §4: sería una frontera con dos dominios adentro, y haría que el alta de cuenta dependiera del repositorio de trabajos. No se reabre acá.

### 1.6 `D-05` · El criterio de nombrado de los adaptadores

`Infrastructure ADR-06003` §6 punto 4 **ya fija el criterio y no hace falta decidirlo**: el adaptador «se nombra por el puerto que implementa y por el mecanismo que usa, en ese orden». Lo que este plan aporta es la aplicación del criterio, para que el punto de control la confirme de una vez:

| Puerto | Adaptador propuesto | Mecanismo declarado |
| --- | --- | --- |
| `IAccountRepository` | `EfCoreAccountRepository` | EF Core sobre SQLite (intake §17.1.P.4 · GeometriaFactory-Infrastructure) |
| `IWorkRepository` | `EfCoreWorkRepository` | EF Core sobre SQLite |
| `IFigureValidator` | `LocalFigureValidator` | Motor propio, **sin red** (intake §17.1.P.3 · GeometriaFactory-Infrastructure) |
| `ISystemClock` | `UtcSystemClock` | Momento en tiempo universal coordinado (`Modelo-Datos-Logico.md` §2.1 y `RC-06006`) |

Las cuatro filas son **propuesta** derivada del criterio; el criterio es declarado. **Los adaptadores se nombran en la etapa `a` pero sólo dos se construyen después**: `BT-06009` (cuentas) y `BT-06012` (reloj) son de etapa `c`, `BT-06010` (trabajos) de etapa `e` y `BT-06016` (validador) de etapa `f`.

### 1.7 `D-06` · Los nombres de los tipos centrales que el esqueleto necesita

**El esqueleto necesita menos tipos de los que el producto tendrá, y más de los que «esqueleto» sugiere.** Lo que sigue sale de los 48 ítems de etapa `a` de `Handoff-Checkout.md` §4, no de una idea de qué debería tener un andamiaje.

**`GeometriaFactory.Domain`** — su etapa `a` es `BT-06001` a `BT-06005`: crear el proyecto y sus puertas. **`BT-06006`, «construir el núcleo de entidades con las cinco entidades del modelo», es de etapa `c`.** Ver `R-02` en §7: `Infrastructure BT-06005`, que es de etapa `a`, mapea esas cinco entidades. La propuesta de este plan es que la etapa `a` cree **los tipos, sin invariantes**, y que `BT-06006` los llene en `c`:

| Tipo propuesto | Qué es | Base declarada |
| --- | --- | --- |
| `Cuenta` (o `Alumno`, según `D-03`) | Entidad | `Definicion-Modelo-De-Dominio.md` §2.1 |
| `Trabajo` | Entidad | §2.2 |
| `Pieza` | Entidad | §2.3 |
| `Componente` | Entidad | §2.4 |
| `Observacion` | Entidad | §2.5 |
| `Role` | Conjunto cerrado de **2**: `Alumno`, `Administrador` | §2.1; `Contratos-REST.md` §2.2 |
| `AccountStatus` | Conjunto cerrado de **3**: `Pendiente`, `Habilitado`, `Bloqueado` | §2.1; `Modelo-Datos-Logico.md` §2.1 |
| `WorkStatus` | Conjunto cerrado de **4**: `Borrador`, `Pendiente`, `Finalizado`, `Rechazado` | §2.2; `Modelo-Datos-Logico.md` §2.2 |
| `ObservationKind` | Conjunto cerrado de **2**: `Advertencia`, `ErrorDeValidacion` | §2.5 |

Los nombres de los cuatro conjuntos cerrados y el de `ObservationKind` son **propuesta**: las fuentes declaran los **valores** y no el nombre del tipo que los agrupa. El valor `Error de validación` se propone como `ErrorDeValidacion` por `D-01`; **su nombre al serializar es el que `Contratos-REST.md` §2.2 exige que sea literal**, y por eso la forma exacta de ese nombre entra al punto de control.

**`GeometriaFactory.Application`** — su etapa `a` es `BT-04001` a `BT-04006`. Los tipos que necesita son los cuatro puertos de §1.5 y nada más: sus ocho componentes de orquestación pertenecen a etapas posteriores.

**`GeometriaFactory.Infrastructure`** — su etapa `a` incluye `BT-06005` (contexto y mapeo de las cinco entidades), `BT-06006` (preparación del almacén), `BT-06008` (zona horaria y precisión de los sellos) y las historias `US-06024` y `US-06025`:

| Tipo propuesto | Qué es | Base declarada |
| --- | --- | --- |
| `GeometriaFactoryDbContext` | Contexto de persistencia, uno por operación | Intake §17.1.P.4 · GeometriaFactory-Infrastructure; `Infrastructure/05` §3.1 |
| `StorePreparation` | Aplica las transformaciones al arrancar y **detiene el arranque** ante un esquema que no corresponde | `ADR-06007`; `BT-06006`; `US-06024`, `US-06025` |
| `AccountConfiguration`, `WorkConfiguration`, `PieceConfiguration`, `ComponentConfiguration`, `ObservationConfiguration` | Los cinco mapeos, uno por entidad | `BT-06005`; `Modelo-Datos-Logico.md` §2 |

**`GeometriaFactory.Api`** — su etapa `a` incluye `BT-00002` (composición de raíz con los cuatro puertos), `BT-00003` (arranque en dos fases con el punto de salud sin acceso), y `US-00026` a `US-00029`:

| Tipo propuesto | Qué es | Base declarada |
| --- | --- | --- |
| `CompositionRoot` | Conecta **4 de 4** puertos con su adaptador, **0** sin adaptador y **0** con más de uno | `ADR-00006`; `QG-10` de `Api/09` `Pipeline-CI-CD.md` §2.1 |
| `TwoPhaseStartup` | Prepara el almacén antes de atender la primera petición; **0** peticiones atendidas con la preparación incompleta | `ADR-00007`; `QG-11`; `US-00027`, `US-00028` |
| `HealthEndpoint` | Realiza `A-16`, **fuera de la guardia** | `Contratos-REST.md` §3; `US-00029` |

**`GeometriaFactory.Contracts`** — su etapa `a` es `BT-08001` a `BT-08003`: crear el ensamblado y sus dos puertas. **`PA-01` de `Contracts` ancla sus nombres de tipos «etapa `c` en adelante, según la familia»**, de modo que la etapa `a` **no crea ningún tipo de transferencia**. Ver `R-03` en §7: el cuerpo de la respuesta de `A-16` no tiene tipo declarado en ninguna parte.

**`GeometriaFactory.Web`** — su etapa `a` incluye `BT-10003`, la página de salud. Un solo tipo, y es andamiaje:

| Tipo propuesto | Qué es | Base declarada |
| --- | --- | --- |
| `Status` (componente Blazor) | Página de estado que consume el punto de salud y muestra datos reales | Intake §15; `Roadmap-Producto.md` §5.2, transición `a` → `b`; `Web BT-10003` |
| `DataServiceClient` | La **única** salida hacia el servicio de datos | `Web/05` §3.1, capa 3; §3.2 punto 3 |

**`GeometriaFactory-Visor`** — sus seis funciones de fachada **quedaron fijadas por [`Norma-De-Nomenclatura.md`](Norma-De-Nomenclatura.md) §5.1 y §6.6 el 2026-08-12, y hasta entonces no lo estaban por ninguna fuente**: el intake §17.2.P.3 · GeometriaFactory-Visor las dejaba «a fijar en la etapa que la implementa», contra lo que este documento afirmaba en su §1.2 punto 5. Son `initialize`, `loadJson`, `selectPiece`, `resize`, `destroy` y `setMotion`, y se renombran en el tramo `R-3`. `Visor/05` `PA-02` declara abiertos únicamente los nombres internos, y los ata a la **etapa `g`**, no a la `a`. La etapa `a` crea el proyecto y un bundle «vacío pero real» (intake §15), con la fachada declarada y sin lógica de dibujo.

### 1.8 Las otras decisiones ancladas al mismo punto de control

**No son nombres, y llegan al mismo punto de control.** Se listan para que el Product Owner las vea juntas y no las descubra de a una:

| # | Qué falta decidir | Titular declarado | Dónde |
| --- | --- | --- | --- |
| `A-3` | Cuál de las dos funciones de derivación de clave se ancla, y con qué parámetros. **El intake declara «PBKDF2 o Argon2» y no elige** | Product Owner y equipo | `Infrastructure/05` `PA-03`; `ADR-06004` §7; `BT-06003` |
| `A-4` | Las rutas y los verbos definitivos de los **quince** puntos de acceso. La fuente declara **dos** cosas: la ruta del canje y la **existencia** del punto de salud | Product Owner y equipo | `Api/05` `PA-01`; `BT-00007` |
| `A-6` | La vigencia exacta del acceso firmado. **El intake dice «corta» y no fija número** | El equipo, y el Product Owner | `Api/05` `PA-04`; `BT-00010` |
| `A-7` | El valor del límite de tamaño del cuerpo. `ADR-00002` §2 punto 6 fija la forma y deja el número | El equipo | `Api/05` `PA-05`; `BT-00009` |
| `A-21` | La zona horaria y la precisión del campo de momento del tipo de error | El equipo | `Contracts/05` `PA-02` |
| `V-4` | La versión de plataforma que soporta el hosting. **Se resuelve midiendo: es `PT-01.a`** | La medición | `Web/05` `PA-02` |
| `V-5` | La versión exacta de la biblioteca de componentes de interfaz | El equipo | `Web/05` `PA-01`; `BT-10002` |
| — | La herramienta que calcula la versión desde los mensajes de confirmación | El equipo | `Domain PA-04`, `Application PA-06`, `Visor`; `Pipeline-Producto.md` §9 |
| `X-1` | **Siete u ocho aristas de compilación.** Decide la forma del archivo de proyecto de `Api` | Product Owner, sobre el manifiesto | `Vista-Producto.md` §3.1 |

---

## 2. El árbol de archivos exacto de la etapa `a`

### 2.1 El árbol

**Se sigue el árbol del intake §16, que a su vez se toma literal de RT §4.2, y no se reinventa.** Lo que este plan agrega es el nivel de archivo dentro de cada carpeta, que §16 no baja, y los apartamientos de §2.3.

```text
Lab-Geometria/
├── GeometriaFactory.sln                              agrupador; los 6 proyectos .NET + los 3 de prueba
├── changelog.md                                      se actualiza en la rama de la etapa
├── Directory.Build.props                             [APARTAMIENTO AP-01]
├── .editorconfig                                     [APARTAMIENTO AP-01]
├── .gitignore                                        EXISTE; se amplía  [APARTAMIENTO AP-02]
├── README.md                                         EXISTE
├── .devcontainer/
│   └── devcontainer.json                             único ambiente donde ocurre todo el ciclo
├── .vscode/
│   └── launch.json                                   depuración por F5, separada de los guiones
├── .github/workflows/
│   └── deploy-front-ftp.yml                          publicación del front; filtro de 3 rutas
├── deploy/
│   ├── Dockerfile                                    backend, multietapa, sin linaje con el devcontainer
│   └── compose.yaml                                  despliegue en destino, con healthcheck
├── scripts/
│   ├── build.sh
│   ├── build-visor.sh
│   ├── test.sh
│   ├── run-api.sh
│   ├── run-web.sh
│   ├── migrate.sh                                    [SIN PROPÓSITO DECLARADO — ver §4]
│   └── reset-db.sh
├── src/
│   ├── GeometriaFactory.Domain/
│   │   ├── GeometriaFactory.Domain.csproj            0 dependencias salientes (puerta BT-02004)
│   │   ├── Entities/{Cuenta,Trabajo,Pieza,Componente,Observacion}.cs
│   │   └── Values/{Role,AccountStatus,WorkStatus,ObservationKind}.cs
│   ├── GeometriaFactory.Contracts/
│   │   └── GeometriaFactory.Contracts.csproj         0 referencias hacia Domain (puerta BT-08002)
│   ├── GeometriaFactory.Application/
│   │   ├── GeometriaFactory.Application.csproj       1 sola dependencia saliente: Domain
│   │   └── Ports/{IAccountRepository,IWorkRepository,IFigureValidator,ISystemClock}.cs
│   ├── GeometriaFactory.Infrastructure/
│   │   ├── GeometriaFactory.Infrastructure.csproj    2 dependencias: Application y Domain
│   │   └── Persistence/
│   │       ├── GeometriaFactoryDbContext.cs
│   │       ├── StorePreparation.cs
│   │       ├── Configurations/{Account,Work,Piece,Component,Observation}Configuration.cs
│   │       └── Migrations/                           generadas; se versionan con el código de su etapa
│   ├── GeometriaFactory.Api/
│   │   ├── GeometriaFactory.Api.csproj               3 referencias, o 2 + transitiva (X-1)
│   │   ├── Program.cs                                host delgado
│   │   ├── Composition/{CompositionRoot,TwoPhaseStartup}.cs
│   │   ├── Endpoints/HealthEndpoint.cs               realiza A-16
│   │   ├── appsettings.json
│   │   └── appsettings.Development.json              escucha por HTTP sin certificado
│   └── GeometriaFactory.Web/
│       ├── GeometriaFactory.Web.csproj               referencia Contracts; consume el bundle
│       ├── Program.cs
│       ├── App.razor · Routes.razor · _Imports.razor
│       ├── Components/Layout/MainLayout.razor
│       ├── Components/Pages/Status.razor             página de estado  [APARTAMIENTO AP-03]
│       ├── Integration/DataServiceClient.cs          la única salida
│       ├── appsettings.json                          sin la dirección real del servicio
│       └── wwwroot/
│           ├── js/                                   destino del bundle; no se edita a mano
│           └── (recursos estáticos del andamiaje)
├── visor/
│   ├── package.json                                  nombre: geometriafactory-visor
│   ├── package-lock.json                             instalación reproducible
│   ├── tsconfig.json
│   ├── webpack.config.js                             salida como biblioteca en window
│   ├── src/
│   │   ├── main.ts                                   fachada: las SEIS funciones declaradas
│   │   └── viewer/                                   capa 3; vacía en la etapa a
│   └── dist/                                         GENERADO; ignorado  [APARTAMIENTO AP-04]
├── tests/
│   ├── GeometriaFactory.Domain.Tests/
│   ├── GeometriaFactory.Application.Tests/
│   └── GeometriaFactory.Integration.Tests/
├── samples/                                          EXISTE, esqueletado; la etapa a NO lo toca
└── SDD/                                              EXISTE; la etapa a NO lo toca
```

### 2.2 Qué es cada archivo

| Archivo o carpeta | Para qué es | Base declarada |
| --- | --- | --- |
| `GeometriaFactory.sln` | Agrupador único del producto. **Todas las aristas de compilación se resuelven por build conjunto dentro de él**, y ésa es la propiedad que vuelve inofensiva la discrepancia `X-1` | Manifiesto §1; `Pipeline-Producto.md` §4 |
| `changelog.md` | Se actualiza **en la rama de la etapa, no después de la fusión** | Intake §16 y §17.1.P.7 · GeometriaFactory-Api |
| `.devcontainer/devcontainer.json` | **El único ambiente que existe.** El host no tiene ni va a tener el kit de desarrollo, y ningún guion corre fuera de él | Intake §16 y encabezado de la Parte C; `Domain/09` `Entornos-Deploy.md` §2 |
| `.vscode/launch.json` | Depuración por F5, **separada de los guiones** | Intake §16 |
| `.github/workflows/deploy-front-ftp.yml` | Publica el front por transferencia al hosting. Su filtro de rutas lleva **tres** entradas —el front, el visor y **los contratos**—, y la tercera entró por una corrección declarada: sin ella, un cambio del contrato no dispara la publicación y las dos unidades quedan desalineadas sin que nada falle. **No termina en la subida: termina comprobando que la dirección pública responde** | Intake §17.2.P.7 · GeometriaFactory-Web y §17.2.P.8 · GeometriaFactory-Web; `Pipeline-Producto.md` §4 |
| `deploy/Dockerfile` | Imagen del servicio de datos, **multietapa**, con sólo el entorno de ejecución y **sin linaje con la imagen del contenedor de desarrollo**. Es lo que `PT-04` mide | Intake §17.1.P.8 · GeometriaFactory-Api y §17.1.P.9 · GeometriaFactory-Api |
| `deploy/compose.yaml` | Despliegue en destino construyendo desde el repositorio, con `healthcheck` contra el punto de salud. **El acto de desplegar es manual y del Product Owner**: la canalización no lo ejecuta | Intake §17.1.P.7 · GeometriaFactory-Api y §17.1.P.8 · GeometriaFactory-Api; `Api/09` `Pipeline-CI-CD.md` §1 |
| `scripts/` (7 guiones) | Ver §4 | Intake §16 |
| `*.csproj` (6) | Un archivo de proyecto por carpeta, con las dependencias de compilación exactas del manifiesto §2 y **las versiones de paquete ancladas explícitamente**: toda versión se fija en el archivo, y un cambio mayor es una decisión que se documenta, nunca el efecto de una actualización | Manifiesto §2; intake, encabezado de la Parte C |
| `Domain/Entities/` y `Domain/Values/` | Los cinco tipos y los cuatro conjuntos cerrados, **sin invariantes** en esta etapa. Ver `R-02` | `Definicion-Modelo-De-Dominio.md` §2; `Infrastructure BT-00005` |
| `Application/Ports/` | Los **cuatro** puertos, que son **la única frontera del proyecto de código** | `Application ADR-04002` §2 |
| `Infrastructure/Persistence/` | Contexto por operación, mapeo de las cinco entidades con sus índices y sus restricciones, **sin ninguna columna de pertenencia a instancia**, con modo de diario por delante y escritor único declarados | `BT-06005`; `Modelo-Datos-Logico.md` §2 y §3 |
| `…/Persistence/Migrations/` | Transformaciones de esquema, versionadas con el código de su etapa. **Una transformación ya fusionada no se edita** | Intake §17.1.P.7 · GeometriaFactory-Infrastructure; `ADR-06007` |
| `Api/Composition/` | `4 de 4` puertos conectados, `0` sin adaptador, `0` con más de uno, y `1` sola configuración de intercambio declarada en todo el producto — es `QG-10`, y **falla en construcción** cuando falta un puerto | `ADR-00006`; `Api/09` `Pipeline-CI-CD.md` §2.1 |
| `Api/Endpoints/HealthEndpoint.cs` | Realiza `A-16`. **Una de las cuatro ausencias declaradas de la guardia** —las otras tres son `A-01`, `A-02` y `A-03`—, y su respuesta no lleva dirección de servicio, ruta del almacén ni traza | `Contratos-REST.md` §3; `US-00029` |
| `Web/Components/Pages/Status.razor` | Consume el punto de salud y muestra datos reales. Es lo que hace medible `PT-01.d` | Intake §15; `Web BT-10003`; `US-10029` §2 |
| `Web/Integration/DataServiceClient.cs` | **La única salida** hacia el servicio de datos. Si aparece una segunda, `RA-01` se queda sin lugar donde verificarse. La dirección base **llega por configuración, nunca embebida** | `Web/05` §3.1 y §3.2 punto 3; `Web ADR-10007` |
| `Web/wwwroot/js/` | Destino del bundle. **Artefacto generado, no se edita a mano** | Intake §13 y §16 |
| `visor/src/main.ts` | Fachada externa con las **seis** funciones declaradas. **No contiene lógica de dibujo** | Intake §17.2.P.2 · GeometriaFactory-Visor y §17.2.P.3 · GeometriaFactory-Visor |
| `visor/webpack.config.js` | Empaqueta con el motor gráfico **dentro del bundle, sin red de distribución externa** (es `PT-03`), y expone la salida **como biblioteca en `window` con un nombre propio, sin globales sueltas** | Intake §17.2.P.1 · GeometriaFactory-Visor y §17.2.P.3 · GeometriaFactory-Visor |
| `tests/*.Tests/` (3) | Materialización de la estrategia de prueba de cada proyecto. **No son proyectos de código del producto.** En la etapa `a` existen y corren vacíos | Intake §16; `Infrastructure BT-06001` |

### 2.3 Apartamientos declarados del árbol del intake §16

**Cuatro, y ninguno cambia una carpeta declarada.** Tres son agregados y uno es una precisión sobre una carpeta que el árbol dibuja.

| # | Apartamiento | Por qué se propone | Riesgo de no hacerlo |
| --- | --- | --- | --- |
| `AP-01` | **Agregar `Directory.Build.props` y `.editorconfig` en la raíz.** No están en el árbol de §16 | Los **seis** proyectos .NET tienen la misma puerta bloqueante: la construcción termina «en 0 y **sin advertencias**» (intake §17.1.P.8 · GeometriaFactory-Domain, §17.1.P.8 · GeometriaFactory-Application, §17.1.P.8 · GeometriaFactory-Infrastructure, §17.1.P.8 · GeometriaFactory-Contracts, §17.1.P.8 · GeometriaFactory-Api; `QG-00001` en las cinco canalizaciones que lo declaran). Sin un lugar único que la imponga, la puerta vive repetida en seis archivos de proyecto y se desincroniza | Que un proyecto quede sin la puerta y **nada falle**. Es la forma exacta del riesgo `RI-06` de `Vista-Producto.md` §7 |
| `AP-02` | **Ampliar el `.gitignore` existente** con la salida del visor, el árbol de dependencias de Node y el archivo del almacén | `Visor/09` `Entornos-Deploy.md` §2 **decidió** que el bundle no se versiona: se ignora y lo genera la canalización antes de publicar, y `Web/09` `Entornos-Deploy.md` §2 adopta la decisión desde el anfitrión. La decisión existe; el archivo que la ejecuta no | Que el bundle se versione, contra una decisión cerrada, y que el archivo del almacén entre al repositorio |
| `AP-03` | **La página de estado del front NO es una de las once superficies aprobadas.** `Linea-Base-Visual.md` inventaría once superficies y la de estado no está entre ellas | La exige el intake §15 y la transición `a` → `b` del roadmap. Es **andamiaje de etapa `a`**, no superficie del producto | Que entre a la línea de base visual y la matriz de sensado marque deriva sobre una pantalla que nadie diseñó. **Se declara explícitamente fuera de la línea de base** |
| `AP-04` | **`visor/dist/` se dibuja en el árbol de §16 pero no se versiona.** Existe en el disco de quien construye y no en el repositorio | Misma decisión de `AP-02`: `Visor/09` §2, con cuatro fundamentos y cuatro exigencias operativas | Que alguien lea §16 y versione el bundle |

**Y una constancia que no es apartamiento:** `samples/` ya está materializado con **diecinueve** carpetas, cada una con su README y su comando previsto, y **cero** archivos de código. Es la pasada de diseño de la categoría 10. **La etapa `a` no toca `samples/`**: las muestras son de las etapas que producen lo que muestran.

---

## 3. El orden de construcción

**El orden es topológico y ya está declarado. Este plan lo verifica y lo cita; no lo elige.**

`Pipeline-Producto.md` §2 lo fija en cuatro niveles. Se contrastó fila por fila contra `PRODUCT-MANIFEST` §3 y contra `PRODUCT-INTAKE` §13, y **los tres coinciden**:

| Nivel | Proyectos de código | Paralelizables | Qué habilita al terminar |
| --- | --- | --- | --- |
| 0 | `GeometriaFactory-Domain`, `GeometriaFactory-Contracts`, `GeometriaFactory-Visor` | Sí, los tres | El dominio, los tipos de transferencia y el bundle quedan disponibles para sus consumidores |
| 1 | `GeometriaFactory-Application`, `GeometriaFactory-Web` | Sí, los dos | Los casos de uso y los cuatro puertos, y el front con el bundle ya embebido |
| 2 | `GeometriaFactory-Infrastructure` | — | Los adaptadores de los cuatro puertos, la seguridad y el validador |
| 3 | `GeometriaFactory-Api` | — | La unidad desplegable del servidor propio, con la composición de raíz conectada |

`Handoff-Checkout.md` §4 cierra con el mismo orden de despacho: «los tres de nivel 0 en paralelo, después `Application` y `Web`, después `Infrastructure`, y `Api` al final», y da el motivo: **la etapa `a` de `Api` depende de que existan los ensamblados que su composición de raíz conecta.**

**Tres precisiones que el orden trae y que conviene tener escritas:**

1. **El nivel 0 no publica nada y es el que más condiciona.** `GeometriaFactory-Visor` es el único de los tres cuyo artefacto **es un archivo que se entrega**: su bundle se copia al directorio de recursos estáticos del front. Un nivel 1 construido sobre un bundle viejo produce un front que se ve bien y dibuja mal (`Pipeline-Producto.md` §2).
2. **`Web` está en el nivel 1 por compilación, y su verificación es del final.** La página de estado sólo se puede recorrer cuando `Api` —nivel 3— arranca. Construir y verificar no ocurren en el mismo momento.
3. **La arista `Web` → `Api` es de tiempo de ejecución y no introduce ciclo.** La dirección del servicio llega **por configuración**, no por referencia (`Web ADR-12007`; manifiesto §3).

**La discrepancia `X-1` no altera este orden.** `Vista-Producto.md` §3.1 lo declara: el grafo es acíclico con siete aristas y con ocho, y **el orden topológico de cuatro niveles es el mismo bajo las dos lecturas**. Lo único que depende de la respuesta es la forma del archivo de proyecto de `Api`.

---

## 4. Los guiones que la etapa necesita

**Siete guiones, los que el intake §16 enumera. Este plan los declara; no los escribe.**

| Guion | Qué hace | Criterio de éxito declarado | Dónde lo declara la especificación |
| --- | --- | --- | --- |
| `build-visor.sh` | **Ciclo corto del visor.** Instalación reproducible de dependencias → empaquetado → **copia al directorio de recursos estáticos del front**. Sólo el bundle, sin encadenar el resto | El bundle **se genera sin errores** (`QG-12001` de `Visor`) | Intake §17.2.P.8 · GeometriaFactory-Visor; `Visor/09` `Pipeline-CI-CD.md` §2 y `Guia-Publicacion-Bundle-Visor.md` |
| `build.sh` | **Construcción del producto.** Encadena la generación del bundle con la compilación de la solución | Termina en **0 y sin advertencias** (`QG-12001` en `Domain`, `Contracts`, `Application`, `Infrastructure` y `Api`) | Intake §17.x.P.8 de los cinco; `Visor/09` `Guia-Publicacion-Bundle-Visor.md` §, fila «construcción del producto» |
| `test.sh` | **Batería completa.** Es **el mismo guion** en la máquina de quien construye y en la canalización | La batería pasa entera: **0** rojas y **0** deshabilitadas sin motivo escrito. Es `QG-02` en **cuatro** proyectos —`Domain`, `Application`, `Infrastructure` y `Api`—; `Contracts` corre el mismo guion y su `QG-02` verifica otra cosa | Intake §17.x.P.8; las cuatro `09-Devops/Pipeline-CI-CD.md` §2; `Contracts/09` `Pipeline-CI-CD.md` §, «con `scripts/test.sh`, el mismo guion del producto» |
| `reset-db.sh` | Deja el almacén **en su estado de primer arranque**: vacío, sin ninguna cuenta y sin ningún trabajo, con su esquema al día | El almacén queda vacío y con su esquema al día | Intake §17.1.P.8 · GeometriaFactory-Infrastructure (reversión); `Api/03` `Guia-Onboarding-Developer.md` §3.2 |
| `run-api.sh` | Ejecuta el servicio de datos dentro del contenedor de desarrollo | **Arranca, aplica las transformaciones y el punto de salud responde** | `Api/03` `Guia-Onboarding-Developer.md` §3.2 y `DX-Developer-Experience.md`; `Web/10` `ejemplo-01-datos-seed.md` |
| `run-web.sh` | Ejecuta la pieza pública dentro del contenedor de desarrollo | **[PROPUESTA SIN BASE DECLARADA]**: que el front arranque y su página de estado responda | Intake §16, que lo enumera y **no declara su contenido**. Ningún otro documento del corpus lo menciona |
| `migrate.sh` | **Su propósito no está declarado por ninguna fuente.** Ver abajo | — | Intake §16, y **sólo ahí** |

**El caso de `migrate.sh`, que hay que decir y no tapar.** Es el único de los siete que **ninguna fuente del corpus menciona fuera del árbol de §16**, verificado con `grep` sobre `SDD/Docs/` y `SDD/Intake/`. Y hay un motivo por el que su propósito no es obvio: **las transformaciones de esquema se aplican solas al arrancar** (intake §17.1.P.4 · GeometriaFactory-Infrastructure y §17.1.P.4 · GeometriaFactory-Api; `ADR-12007`), de modo que no hay un paso de aplicación manual que un guion tenga que envolver. **Propuesta:** que sea el guion de **generación** de una transformación nueva durante el desarrollo —lo que hace falta para que `reset-db.sh` y `run-api.sh` tengan un esquema que aplicar—, y **no** de aplicación. Va rotulada **[PROPUESTA SIN BASE DECLARADA]** y entra al punto de control.

**Orden de ejecución en la etapa `a`:**

```text
1. build-visor.sh    →  el bundle existe y está copiado al front
2. build.sh          →  el producto compila entero, 0 advertencias
3. test.sh           →  la batería pasa entera (vacía todavía, y corre)
4. migrate.sh        →  la transformación inicial del esquema existe
5. reset-db.sh       →  el almacén queda en estado de primer arranque
6. run-api.sh        →  el servicio arranca, transforma y el punto de salud responde
7. run-web.sh        →  la pieza pública arranca y su página de estado consume el punto de salud
```

**Dos reglas que gobiernan a los siete y no son de este plan:**

- **Todo corre dentro del contenedor de desarrollo.** El host no tiene ni va a tener el kit de desarrollo, y ningún guion corre fuera (intake, encabezado de la Parte C, y §10; `Domain/09` `Entornos-Deploy.md` §2).
- **Los guiones son los mismos en la máquina de quien construye y en la canalización.** Es la propiedad que las cinco `Supply-Chain-Seguridad.md` declaran cumplida por construcción reproducible por guion.

**Y una regla de detención declarada:** si la construcción termina con advertencias, **no se sigue**; la puerta del producto es cero advertencias, y arrastrarlas hace que la siguiente sea invisible (`Api/03` `Guia-Onboarding-Developer.md` §2).

---

## 5. Cómo se verifica el cierre de la etapa

`Roadmap-Producto.md` §5.2 declara **ocho** criterios propios de la transición `a` → `b`, y §5.1 declara **siete** comunes a toda transición. Quince en total, y ninguno se puede dar por cumplido por declaración.

**Los ocho propios:**

| # | Criterio de transición | Qué lo demuestra | Guion o instrumento |
| --- | --- | --- | --- |
| 1 | El producto **compila entero** y las dos piezas desplegables **arrancan desde sus guiones** dentro del entorno de desarrollo | `build.sh` termina en 0 y sin advertencias sobre los siete proyectos; `run-api.sh` y `run-web.sh` levantan las dos piezas | `build.sh`, `run-api.sh`, `run-web.sh` |
| 2 | La **página de estado** de la pieza pública consume el punto de salud y **muestra datos reales** | Recorrido en el navegador del host, con el servicio de datos corriendo y con el servicio de datos **detenido** —el segundo caso demuestra que el dato es real y no un literal— | `Web BT-10003`; `US-10029` |
| 3 | `PT-01.a`: la dirección pública responde correctamente | Publicación del front al hosting y comprobación de la dirección pública. **Si no pasa, la salida declarada es bajar la versión objetivo del front y no la del backend**. **CERRADO el 2026-08-13**: el front se publicó al hosting y `https://www.aplicada.somee.com/estado` responde **200**. **La salida declarada no se ejerció**: el hosting soporta `net10.0` y la versión objetivo del front no se bajó. Evidencia en [`../00-Contexto/Compatibilidad-Plataformas.md`](../00-Contexto/Compatibilidad-Plataformas.md) §2.6 | Flujo de publicación; `V-4` |
| 4 | `PT-01.b`: el transporte de la sesión interactiva está **medido y su resultado documentado, incluido el repliegue si ocurre** | Medición con semáforo. **Sólo el peor resultado obliga a cambiar el modelo de front; un repliegue de transporte no es motivo de rediseño**. **CERRADO el 2026-08-13, y con el resultado corregido por la medición sobre el hosting real**: en desarrollo el transporte elegido es WebSockets con repliegue a long polling ejercido ([`../00-Contexto/Compatibilidad-Plataformas.md`](../00-Contexto/Compatibilidad-Plataformas.md) §2.4), pero **el hosting no ofrece WebSockets** —sólo `ServerSentEvents` y `LongPolling`—, de modo que **en producción la sesión interactiva va por long polling** (§2.6 del mismo documento). Semáforo revisado a **amarillo estable**: es el repliegue ya declarado aceptable, y **no obliga a cambiar el modelo de front** | `Web BT-10004` |
| 5 | `PT-01.c`: **veinte minutos** de navegación continua sin que el proceso recicle la sesión, y **reconexión funcional** al cortar y restablecer la red | Recorrido cronometrado sobre el front publicado. Es el peor escenario y **no tiene mitigación en el código**. **CERRADO el 2026-08-13, y medido sobre el hosting real, que es donde vive el riesgo.** Primero en el entorno de desarrollo —20 min 45 s sobre un único circuito y reconexión al **mismo** circuito, [`../00-Contexto/Compatibilidad-Plataformas.md`](../00-Contexto/Compatibilidad-Plataformas.md) §2.5— y después **contra `https://www.aplicada.somee.com/estado`**: **20 min 6 s** de navegación continua con **20 interacciones, todas con efecto observable**, sobre **una única conexión del circuito**, **cero** rearmes y **cero** recargas, y **corte de red real de 26,1 s** con reconexión automática a los **7,0 s** y pulsación posterior con efecto (**§2.6.1**). **Sigue sin medir el reciclado por inactividad**, que la corrida no ejerce | `Web BT-10004` |
| 6 | `PT-01.d`: una llamada de salud devuelve **datos reales del servidor propio** | La página de estado contra el servicio de datos real, no contra un doble | `US-10029`; `Web BT-10004` |
| 7 | `PT-04`: la imagen se **construye, arranca, aplica sus actualizaciones de esquema sobre base vacía y responde salud** | `deploy/Dockerfile` construido y arrancado **desde el contenedor de desarrollo**, con el almacén recién creado | `Api BT-00004`; `Infrastructure BT-00007`; etapa `imagen` de `Api/09` `Pipeline-CI-CD.md` §2.1 |
| 8 | Está verificado que **la sesión interactiva no llega al servicio de datos** | Inspección de red desde el navegador durante el recorrido: **cero** peticiones del navegador hacia el servicio de datos. Es `RA-01`. **Verificado corriendo el 2026-08-13**: §5.1 dice con qué método y con qué control | Intake §17.1.P.3 · GeometriaFactory-Api, fila de WebSockets, que lo declara criterio de aceptación de la etapa `a` |

**Los siete comunes** (`Roadmap-Producto.md` §5.1). En la etapa `a` dos de ellos tienen una lectura propia que conviene dejar escrita:

| # | Criterio común | Lectura en la etapa `a` |
| --- | --- | --- |
| 1 | Los guiones de demostración de todas las fases anteriores vuelven a pasar | **No hay fases anteriores.** Se cumple de forma degenerada y se declara en lugar de omitirse |
| 2 | La fase incorporó pruebas automatizadas de las reglas de negocio que introdujo | **La etapa `a` no introduce ninguna regla de negocio** (§6). Lo que sí incorpora son las puertas de `US-00024`, `US-00025` y `US-00026` a `US-00029`, con **3** criterios `Given/When/Then` cada una |
| 3 | El informe de cierre está escrito, es autocontenido e indizado | Con las **trece** secciones obligatorias, en `Avances/<orden>-<etapa>.md`, y su índice en `Avances/README.md` |
| 4 | La rama tiene su solicitud de incorporación abierta: **esa solicitud es el punto de control** | Una rama y una solicitud por etapa; etapas en serie |
| 5 | El Product Owner dio **OK explícito** | Es donde se cierran las decisiones de §1 |
| 6 | La rama está incorporada antes de abrir la siguiente | — |
| 7 | Todo guion que involucre el texto de figuras usa datos verificados del intake | **La etapa `a` no involucra ningún texto de figuras.** Se declara y no se rellena |

**Puertas de calidad que además se ejercen en la etapa `a`**, tomadas de las canalizaciones ya especificadas: `QG-02001` en cinco proyectos —`Domain`, `Contracts`, `Application`, `Infrastructure` y `Api`— con umbral de cero advertencias, `QG-02002` con umbral de batería entera en **cuatro** —los mismos menos `Contracts`, cuyo `QG-02002` es otro: **cero** referencias hacia `GeometriaFactory-Domain`—, `QG-02010` en `Api` (**4 de 4** puertos conectados, **0** sin adaptador, **1** sola configuración de intercambio) y `QG-02011` en `Api` (**0** peticiones atendidas con la preparación del almacén incompleta).

**Una puerta que no pasa detiene la planificación de las etapas que dependen de ella; no se arrastra como deuda.** Lo declara el intake §15 y el roadmap §2.2 lo hereda sin ablandarlo.

### 5.1 Lo que ya se midió corriendo, y con qué

Esta sección registra los criterios de §5 que **ya se ejercieron sobre el producto en ejecución**, no los que se dan por buenos por declaración. Lo que no está acá, no se midió.

| Criterio | Estado | Dónde está el resultado |
| --- | --- | --- |
| 4 · `PT-01.b`, transporte de la sesión interactiva | **Medido en desarrollo y también sobre el hosting real. CERRADO, con el resultado corregido**: el hosting **no ofrece WebSockets** y la sesión va por long polling. Semáforo **amarillo estable** | [`../00-Contexto/Compatibilidad-Plataformas.md`](../00-Contexto/Compatibilidad-Plataformas.md) §2.4 —oferta, transporte elegido, repliegue provocado y latencia— y **§2.6**, que corrige el resultado con la negociación pedida contra la dirección pública |
| 8 · `RA-01`, la sesión interactiva no llega al servicio de datos | **Verificado corriendo, con instrumento y con control** | Acá abajo |
| 5 · `PT-01.c`, estabilidad de la sesión y reconexión | **CERRADO. Medido en el entorno de desarrollo contenido y también sobre el hosting real, con navegador real en los dos casos. Las dos mitades del criterio pasaron en los dos entornos** | [`../00-Contexto/Compatibilidad-Plataformas.md`](../00-Contexto/Compatibilidad-Plataformas.md) §2.5 —desarrollo, con la identidad del circuito leída del lado del servidor— y **§2.6.1** —hosting real, con el método de detección **desde el cliente** y su control del instrumento— |
| 3 · `PT-01.a`, la dirección pública responde | **CERRADO.** El front se publicó al hosting y la ruta que la etapa sirve responde **200** | [`../00-Contexto/Compatibilidad-Plataformas.md`](../00-Contexto/Compatibilidad-Plataformas.md) §2.6, y §5.2 de acá |

**El método elegido para el criterio 8, y por qué concluye.** «Cero peticiones del navegador hacia la pieza de datos» es una afirmación negativa, y una inspección a ojo de las herramientas del navegador no la cierra: no prueba que no haya una petición que no se vio. Se combinaron por eso **tres cosas que se sostienen entre sí**:

1. **Imposibilidad topológica.** El servicio de datos se levanta escuchando **sólo en el bucle local del contenedor**, y el contenedor publica **únicamente** el puerto del front. El navegador corre fuera del contenedor: aunque algún guion quisiera llamar al servicio de datos, **no tiene camino**. Verificado desde donde corre el navegador: la conexión al servicio de datos es rechazada por las dos direcciones posibles, y la del front responde.
2. **Enumeración completa de lo que el navegador pide.** El navegador se condujo con instrumentación, forzado a salir por un **único intermediario que registra toda petición**, sin excepción para el bucle local. El registro completo del recorrido —cargar la página de estado, arrancar el circuito y pulsar el botón— tiene **una sola familia de destinos del producto: el front**. Ninguna línea apunta al servicio de datos.
3. **Control del instrumento.** Un instrumento que no registra nada no distingue «no pasó» de «no lo vi». Se le hizo pasar lo prohibido a propósito: desde el guion de la propia página se pidió el punto de salud del servicio de datos. El intermediario **sí lo registró**, y la petición **falló por conexión rechazada**. O sea: el instrumento ve las violaciones cuando las hay, y el camino está efectivamente cerrado.

A eso se suma la **inspección del contenido servido**: en todo lo que el navegador recibe —el documento de la página de estado, la hoja de estilo, el guion del armazón interactivo y la respuesta de inicializadores— hay **cero** apariciones de la dirección del servicio de datos, de su puerto y de la ruta del punto de salud; las únicas direcciones absolutas del guion del armazón son las tres del espacio de nombres de gráficos vectoriales. Y el bundle de la visualización, que en la etapa `a` **ninguna página referencia**, no contiene **ninguna** primitiva de red.

**Qué demuestra el recorrido, además.** La página de estado mostró datos reales del servicio —almacén preparado, versión con el identificador de revisión y momento del servidor— y **al pulsar el botón sobre el circuito interactivo el momento del servidor cambió**: la acción del navegador viajó por el circuito, el **proceso del front** llamó al servicio de datos y devolvió el dato nuevo. Es `RA-01` en positivo: la llamada existe, y sale del servidor.

**Qué queda fuera del alcance de lo medido para `PT-01.c`, y por qué importa.** El criterio vigila un riesgo del **hosting gratuito**: que el proceso recicle la sesión, y eso **no se puede observar en el entorno de desarrollo contenido**, donde el proceso es propio y nadie lo recicla. Por eso la medición se repitió **sobre el hosting real** el mismo 2026-08-13, y ahí el riesgo **quedó ejercido y no se materializó**: `Compatibilidad-Plataformas.md` **§2.6.1**. El método tuvo que cambiar, porque sobre el hosting **no hay registros del servidor**: la identidad del circuito se infiere de la **identidad de la conexión SignalR**, leída del propio tráfico del navegador, y se apoya en el aviso de reconexión del front y en una marca de ventana que detecta cualquier recarga. **Lo que sigue sin medir, y no se da por bueno: el reciclado por inactividad** —la corrida interactúa cada minuto, así que no lo ejerce— y la **latencia percibida en producción**. `R-07` de §7 **no desaparece**: sigue siendo cierto que el escenario no tendría mitigación en el código; lo que cambia es que **está medido y no ocurrió**.

### 5.2 El estado de cierre completo, con los ocho criterios propios cerrados

**Los ocho criterios propios están cerrados.** El último en cerrarse fue el **5**, `PT-01.c`, cuya mitad de hosting se midió el 2026-08-13 contra la dirección pública. Esta sección deja el estado entero en un solo lugar, con dónde vive la evidencia de cada uno.

**Lo que cambió el 2026-08-13, y es lo que mueve la cuenta.** El front se publicó al hosting público por FTP y **está en línea**: `https://www.aplicada.somee.com/estado` responde **200**. Con eso se cerró `PT-01.a` —criterio **3**, que hasta esta versión era el único sin medir— y **también** la mitad de hosting de `PT-01.b` —criterio **4**—, que se pudo medir contra la dirección pública. Y después se cerró **también `PT-01.c`** —criterio **5**—, corriendo **contra la dirección pública** los veinte minutos cronometrados y el corte de red, que es donde vive el riesgo que el criterio vigila.

| # | Criterio | Estado al **2026-08-13** | Dónde está la evidencia |
| --- | --- | --- | --- |
| 1 | Compila entero y las dos piezas arrancan desde sus guiones | **Medido** | Confirmación `b93a51d`, «etapa a: el andamiaje verificado dentro de un contenedor» |
| 2 | Página de estado con datos reales | **Medido** | Confirmación `cf10c7c`, y el recorrido de §5.1 |
| 3 | **`PT-01.a`: la dirección pública responde correctamente** | **CERRADO.** `https://www.aplicada.somee.com/estado` responde **200**; la raíz responde **404**, que es lo esperado con una sola ruta servida. **La incógnita de versión de plataforma quedó resuelta: el hosting soporta `net10.0`**, y la salida declarada —bajar la versión objetivo del front— **no se ejerció** | [`../00-Contexto/Compatibilidad-Plataformas.md`](../00-Contexto/Compatibilidad-Plataformas.md) §2.6; [`../Audit/Reporte-Despliegue-Somee.md`](../Audit/Reporte-Despliegue-Somee.md) |
| 4 | `PT-01.b`: transporte medido, repliegue incluido | **CERRADO, en desarrollo y sobre el hosting real, con el resultado corregido**: el hosting **no ofrece WebSockets** y la sesión interactiva va por **long polling**, que es el repliegue ya ejercido y declarado aceptable | [`../00-Contexto/Compatibilidad-Plataformas.md`](../00-Contexto/Compatibilidad-Plataformas.md) §2.4 y **§2.6** |
| 5 | `PT-01.c`: veinte minutos y reconexión | **CERRADO.** Medido en el entorno de desarrollo y **sobre el hosting real**: **20 min 6 s** de navegación continua con **20 interacciones, todas con efecto**, **una única conexión del circuito**, cero rearmes y cero recargas; y **corte de red de 26,1 s** con reconexión automática a los **7,0 s** y pulsación posterior con efecto. **Sigue sin medir el reciclado por inactividad** | [`../00-Contexto/Compatibilidad-Plataformas.md`](../00-Contexto/Compatibilidad-Plataformas.md) §2.5 y **§2.6.1** |
| 6 | `PT-01.d`: salud con datos reales del servidor propio | **Medido** contra el servicio de datos real, no contra un doble | Confirmación `b93a51d`; el recorrido de §5.1 |
| 7 | `PT-04`: imagen del servicio de datos | **Medido** | Confirmación `1fa6ba0`, «imagen del backend: PT-04 verificado» |
| 8 | `RA-01`: la sesión interactiva no llega al servicio de datos | **Verificado corriendo, con instrumento y con control** | §5.1 |

**Qué es exactamente `PT-01.a`, leído en las fuentes y no interpretado.** El intake §17.2.P.10 · GeometriaFactory-Web le da criterio y umbral: «el front publicado arranca y sirve la página inicial», umbral **200 en la URL pública**, y si no pasa, la salida es **bajar la versión objetivo del front y no la del backend**. §17.2.P.9 · GeometriaFactory-Web dice **qué incógnita resuelve**: la versión de plataforma que soporta el hosting está **[A VERIFICAR]** en la fuente, «es PT-01.a», y **se resuelve midiendo, no decidiendo**. **Quién la mide**: el paso **8** del flujo de publicación, que es el propio flujo y no una persona ([`../Unidades-Entrega/GeometriaFactory-Web/09-Devops/Pipeline-CI-CD.md`](../Unidades-Entrega/GeometriaFactory-Web/09-Devops/Pipeline-CI-CD.md) §2.1 y `QG-10003`). **Sobre qué se mide**: §9 de ese mismo documento lo declara sin ambigüedad —sobre el front **ya publicado** en el hosting, **no sobre una ejecución local**—, y agrega la consecuencia de orden: `PT-01` exige que **el flujo de publicación exista y haya corrido** antes de que se pueda medir.

**Por eso `PT-01.a` no se puede medir en el entorno de desarrollo, y no es una limitación del método.** Lo que la puerta interroga es una propiedad del hosting —qué versión de plataforma soporta—, y un servidor propio contestaría por sí mismo, no por él. Una comprobación local del mismo comando prueba que el comando funciona; **no prueba `PT-01.a`**.

**Qué tuvo que existir para poder medirla, quién lo proveyó y en qué orden. Los cuatro se cumplieron el 2026-08-13**, y por eso el criterio está cerrado. Nada de esto lo podía aportar quien construye el código:

| # | Qué tiene que existir | Quién lo provee | Por qué, con su fuente |
| --- | --- | --- | --- |
| 1 | **Cuenta en el hosting público** con servidor de información, transporte seguro y dominio | El Product Owner. Se contrata y se configura **por fuera del repositorio**: no hay infraestructura declarativa | Intake §17.2.P.9 · GeometriaFactory-Web; [`../Unidades-Entrega/GeometriaFactory-Web/09-Devops/Entornos-Deploy.md`](../Unidades-Entrega/GeometriaFactory-Web/09-Devops/Entornos-Deploy.md) §3 |
| 2 | **Los secretos del repositorio que el flujo consume**, nombrados por su función: la dirección base del servicio de datos, las credenciales y el destino del canal de publicación, y la dirección pública que el paso 8 interroga. **Ningún valor vive en el repositorio ni en esta cadena de documentos** | El mismo, cargándolos en el almacén de secretos del repositorio | Intake §17.2.P.5 · GeometriaFactory-Web; [`../Unidades-Entrega/GeometriaFactory-Web/09-Devops/Guia-Publicacion-Front-Ftp.md`](../Unidades-Entrega/GeometriaFactory-Web/09-Devops/Guia-Publicacion-Front-Ftp.md) §1 |
| 3 | **Una corrida del flujo de publicación entero**, por fusión a la rama principal con cambios bajo las rutas del filtro o por disparo manual | El mismo, que es quien fusiona y quien aprueba | Intake §17.2.P.7 · GeometriaFactory-Web; `Guia-Publicacion-Front-Ftp.md` §2 |
| 4 | **Que la dirección que el paso 8 interroga corresponda a una ruta que el front sirva.** En la etapa `a` la pieza pública tiene **una sola ruta**, la página de estado: la raíz **no está servida todavía**, y las rutas navegables son de la etapa `b` (§6) | El mismo, al fijar el valor del secreto de la dirección pública | Comprobado corriendo, más abajo; [`../Unidades-Entrega/GeometriaFactory-Web/09-Devops/Guia-Publicacion-Front-Ftp.md`](../Unidades-Entrega/GeometriaFactory-Web/09-Devops/Guia-Publicacion-Front-Ftp.md) §3 |

**El orden es el de la tabla y no es arbitrario**: sin cuenta no hay destino, sin secretos el flujo se detiene en el paso 6 —y lo hace a propósito—, y sin corrida no hay nada que interrogar.

**Y una precisión sobre cómo se ejerció el punto 4, porque el hallazgo previsto se confirmó.** La dirección que se interroga es **la ruta que la etapa sirve**, no la raíz: sobre el hosting real la raíz responde **404** y la página de estado responde **200**. Lo que 1.6 anticipó midiendo la publicación levantada en local **se confirmó contra el destino real**, y por eso el criterio da verde sin haber ablandado nada.

**El mecanismo del hosting, que era la parte que no se conocía, quedó documentado.** Publicación por FTP **a la carpeta raíz del sitio sin subcarpeta**, **`web.config` como requisito duro** cuyo modo de falla es **500**, y **la versión de plataforma elegida en el panel de la cuenta**, que el `web.config` tiene que igualar: está en [`../Unidades-Entrega/GeometriaFactory-Web/09-Devops/Guia-Publicacion-Front-Ftp.md`](../Unidades-Entrega/GeometriaFactory-Web/09-Devops/Guia-Publicacion-Front-Ftp.md) §2.1 y §2.2, con el procedimiento de respaldo previo. La experiencia entera, en forma reutilizable por otro producto, está en [`../Audit/Reporte-Despliegue-Somee.md`](../Audit/Reporte-Despliegue-Somee.md).

**La publicación destrabó dos de los tres criterios que dependían de ella, y el tercero no.** `PT-01.b` **quedó cerrado** midiendo la negociación del circuito contra la dirección pública, y el resultado **corrigió** lo que la medición de desarrollo daba por bueno: **el hosting no ofrece WebSockets**, sólo `ServerSentEvents` y `LongPolling`, de modo que **la sesión interactiva del producto va por long polling en producción**. No es motivo de rediseño —el repliegue estaba ejercido y funcionando, y §4 de `Compatibilidad-Plataformas.md` lo tenía declarado aceptable—, pero **sí invalida citar la latencia de §2.4 como latencia de producción**: se midió en bucle local. `PT-01.c` **se midió después sobre el hosting**, y precisamente por ese motivo: lo medido en desarrollo fue sobre un circuito por WebSockets y el de producción no lo es, de modo que la corrida de §2.6.1 es la que ejerce el circuito **real**, sin WebSockets.

**Qué se comprobó corriendo el 2026-08-13 antes de publicar, sin hosting y sin ningún secreto.** No medía `PT-01.a`; medía que **el camino que la llevaría hasta ahí estaba bien formado**, y se conserva porque es lo que explica por qué la publicación no encontró sorpresas en el lado del repositorio. Todo corrió sobre una copia de trabajo del árbol, con las dos cadenas de herramientas dentro de contenedores, y el árbol del repositorio quedó intacto:

| Qué se comprobó | Cómo | Resultado |
| --- | --- | --- |
| El flujo de publicación es un documento válido y tiene los **ocho** pasos declarados, en el orden de `Pipeline-CI-CD.md` §2.1 | Análisis del documento del flujo con un lector de su formato | **Válido**; **8** pasos, un solo trabajo, y los pasos 4, 5, 6, 7 y 8 en el orden declarado |
| El **filtro de rutas** es el que la fuente declara desde el intake **1.22** | El mismo análisis | **Tres** rutas: la del front, la del visor y **la del ensamblado de contratos**; disparo por fusión a la rama principal y disparo manual, los dos presentes |
| El paso 4 **genera el bundle en el mismo flujo** y lo copia a los recursos estáticos (`QG-02`) | Se borró el bundle de la copia y se corrió el guion del paso 4 | El guion tomó la rama de **instalación reproducible** —hay archivo de bloqueo, y no imprimió el aviso de la rama sin él—, empaquetó y **volvió a dejar el bundle** en el directorio de recursos estáticos |
| El paso 5 **publica sin advertencias** (`QG-01`) | Publicación en configuración de entrega sobre árbol limpio, contando advertencias en la salida | Terminó en **0**, con **0** advertencias |
| El paso 6 **inyecta la dirección desde el secreto** y **se detiene si el secreto está vacío** | Se corrió el paso con un valor de relleno inexistente, y después con el valor vacío | Con valor: el archivo de configuración publicado quedó con la dirección inyectada. Vacío: **el paso falla** antes de escribir nada |
| El paso 8 **hace lo que dice**: interroga una dirección y exige respuesta correcta | Se levantó la publicación resultante en local y se corrió **el mismo comando del paso 8**, apuntado a esa instancia | El comando funciona: informó el código de respuesta y **exigió** el valor correcto |
| **Qué ruta responde en la publicación de la etapa `a`** | El mismo montaje, pidiendo la raíz y la página de estado | **La raíz responde 404** y **la página de estado responde 200**. Es el hallazgo de la fila 4 de la tabla anterior |

**El hallazgo de la raíz merece leerse despacio, porque toca el criterio.** El paso 8 pide la dirección pública y exige respuesta correcta; el intake §17.2.P.10 · GeometriaFactory-Web escribe el criterio como «el front publicado arranca y sirve **la página inicial**». En la etapa `a` **no hay página inicial**: §6 declara que las once superficies son de la etapa `b` y que lo único que existe es la página de estado, que es andamiaje. De modo que, si la dirección que el paso 8 interroga es la raíz desnuda, **el paso dará rojo con una publicación perfectamente correcta**. **No es una falla del flujo y no se arregla ablandando el paso**: se resuelve al fijar el valor del secreto de la dirección pública, y en la etapa `b`, cuando la raíz exista, deja de ser un caso. Queda declarado acá para que el punto de control lo decida y no lo descubra en rojo.

**Lo que esta sección no hace: dar por cerrado lo que no lo está.** De los ocho criterios propios, **los ocho están cerrados**: el último fue el **5**, `PT-01.c`, corrido sobre la dirección pública con la misma corrida cronometrada de veinte minutos y el mismo corte de red que §2.5 de [`../00-Contexto/Compatibilidad-Plataformas.md`](../00-Contexto/Compatibilidad-Plataformas.md) describe, y con el resultado en **§2.6.1** del mismo documento. **Lo que sigue sin medir y no se da por bueno** es el reciclado del proceso **por inactividad**, que la corrida no ejerce, y la **latencia percibida en producción**. `R-07` de §7 **sigue en pie como riesgo declarado** —no tendría mitigación en el código si ocurriera—, pero **está ejercido y no se materializó**.

---

---

## 6. Lo que la etapa `a` NO hace

**Es un esqueleto que camina. Esta lista existe para que el punto de control no evalúe la etapa contra un producto que todavía no existe.**

| La etapa `a` **no** … | Dónde vive eso realmente |
| --- | --- |
| **No implementa ninguna regla de negocio.** Ninguna de las **dieciséis** `RN` se ejerce | `Domain BT-02006` (entidades) es etapa `c`; `BT-02010` (guardas de cuenta) etapa `c`; `BT-02012` (máquina de estados) etapa `e` |
| **No hace cumplir ningún invariante.** Los **nueve** `INV` no tienen código en esta etapa | `Domain BT-02014`, matriz de ejercicio de los nueve invariantes, etapa `d` |
| **No valida JSON.** El motor de interpretación con las cuatro trampas del formato no existe | `Infrastructure BT-06016`, etapa `f` |
| **No verifica valores declarados contra derivados**, ni emite advertencias, ni la tolerancia de 0.01 con operador estricto | `Infrastructure`, etapa `f`; criterios de la transición `f` → `g` |
| **No persiste ningún trabajo.** El esquema de las cinco entidades **existe**; el adaptador que escribe trabajos **no** | `Infrastructure BT-06010`, adaptador de repositorio de trabajos, **etapa `e`** |
| **No persiste ninguna cuenta.** El adaptador de repositorio de cuentas y su índice único no existen | `Infrastructure BT-06009`, **etapa `c`** |
| **No autentica a nadie.** No hay canje de credenciales, ni acceso firmado, ni derivación de clave construida | `Infrastructure BT-06015`, etapa `c`. En la etapa `a` sólo se **ancla** la función de derivación (`BT-06003`, decisión `A-3`) |
| **No expone catorce de los quince puntos de acceso.** Sólo `A-16`, el de salud | `Contratos-REST.md` §3; los demás entran con sus etapas |
| **No emite ningún tipo de transferencia.** `Contracts PA-01` ancla sus nombres «etapa `c` en adelante» | `GeometriaFactory-Contracts`, etapas `c` en adelante |
| **No tiene las once superficies del front.** Sólo la página de estado, que es andamiaje y **no** superficie del producto | Etapa `b`: todas las rutas navegables con marcadores de posición |
| **No dibuja nada en tres dimensiones.** El bundle es «vacío pero real»: la fachada existe, la capa 3 no | Etapa `g`, con `PT-02` y `PT-03` medidas antes de comprometerla |
| **No despliega.** La canalización termina en **un artefacto verificado y no en un servicio corriendo**; poner la imagen en el servidor propio es un acto manual del Product Owner | `Api/09` `Pipeline-CI-CD.md` §1; `PT-05` en la etapa `i` |
| **No produce ninguna muestra ejecutable.** Las diecinueve carpetas de `samples/` siguen sin código | `samples/README.md` §1; la pasada de ejecución es de las etapas que producen lo que muestran |
| **No es demostrable al cliente.** Es un **hito interno**, igual que la `b`. De la `c` en adelante, todas son demostrables sin excepción | Intake §15; roadmap §2 |

---

## 7. Riesgos y contradicciones que este plan eleva sin resolver

| # | Qué | Por qué importa ahora |
| --- | --- | --- |
| `R-01` | **`X-1`: siete u ocho aristas de compilación.** El manifiesto declara ocho en §2, dibuja siete en §3 y valida siete en §4 | Decide si el archivo de proyecto de `Api` declara la referencia a `Application` o la recibe transitivamente. **Se materializa el primer día de la etapa `a`** y hoy no tiene desenlace |
| `R-02` | **Contradicción de orden entre dos backlogs aprobados.** `Infrastructure BT-00005` —«construir el contexto de persistencia y **el mapeo de las cinco entidades**»— es de **etapa `a`**, y `Domain BT-00006` —«construir el núcleo de entidades con **las cinco entidades del modelo**»— es de **etapa `c`**. No se puede mapear lo que no existe | Es el ítem más pesado de la etapa `a` de `Infrastructure` y de él cuelgan `US-00024`, `US-00025`, `BT-00006`, `BT-00007` y `PT-04`. La propuesta de §1.7 —crear los tipos en `a` sin invariantes, llenarlos en `c`— **es propuesta de este plan y no está declarada por ninguna fuente** |
| `R-03` | **El cuerpo de la respuesta de salud no tiene tipo declarado.** `US-08029` exige «datos reales del servidor propio» y **no dice cuáles**; `Contratos-REST.md` da a `A-16` los códigos `200` y `503` y ningún tipo; `Contracts` no crea ningún tipo de transferencia en la etapa `a` | Es el criterio 2 y el criterio 6 del cierre. Sin decidir qué son «datos reales», `PT-01.d` se verifica contra un criterio que nadie escribió. **Ninguna fuente permite proponerlo con fundamento** |
| `R-04` | **La ruta del punto de salud no está declarada.** La fuente declara **la existencia** del punto y no su ruta | El `healthcheck` de `compose.yaml`, la página de estado y la comprobación de la publicación del front la necesitan las tres. Es `A-4` / `Api BT-00007` |
| `R-05` | **La divergencia `Alumno` / `Cuenta`** de §1.4, que ninguna categoría registró como punto abierto | Es el nombre de un tipo del dominio, de una tabla y del cuarto puerto a la vez |
| `R-06` | **`B-4` sigue abierto en la firma.** El corte de fase con confirmación humana del Product Owner —y con él la aprobación formal del `PRODUCT-INTAKE`— es lo único que un equipo que arranque **no puede cerrar por su cuenta**, y **no le impide construir** | `Handoff-Checkout.md` §6.1 |
| `R-07` | **`PT-01.c` no tiene mitigación en el código.** Si el proceso del hosting recicla la sesión antes de los veinte minutos, no hay nada que escribir para arreglarlo. **Ejercido sobre el hosting real el 2026-08-13 y no materializado**: 20 min 6 s continuos sobre una única conexión del circuito ([`../00-Contexto/Compatibilidad-Plataformas.md`](../00-Contexto/Compatibilidad-Plataformas.md) §2.6.1). Queda en pie para el reciclado **por inactividad**, que no se midió | Roadmap §5.2; intake §17.2.P.12 · GeometriaFactory-Web, riesgo `R-06` de la fuente |

---

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.10 | 2026-08-29 | **Tramo `R-4` · renumerado de `QG` y `CV` al mapa de bloques del destino**, decidido por el Product Owner el 2026-08-29 al **retirar el `ADR-14005`** en lugar de aceptarlo. **5 línea(s)** pasan de `QG-NN` a `QG-<bloque>NNN`, con el bloque **deducido de la línea o de la sección y nunca inventado** — `00` Api, `02` Domain, `04` Application, `06` Infrastructure, `08` Contracts, `10` Web, `12` Visor. Con esto las dos familias **dejan de necesitar apartamiento**: cumplen [`Norma-De-Nomenclatura.md`](Norma-De-Nomenclatura.md) y `Root-Rules.md` §9.1 y §9.2. Las referencias cuyo bloque no estaba en el texto **conservan la forma vieja a propósito** y quedan inventariadas en [`../Audit/Inventario-Renumerado-R-4-2026-08-29.md`](../Audit/Inventario-Renumerado-R-4-2026-08-29.md). Se respeta §4.1: no se tocan las filas de control de cambios ni lo que está entre «…». |

| 1.9 | 2026-08-17 | **Migración normativa 8.11 → 9.9, fase M4.** **Este documento no figuraba en el plan**: entró al abrirse el barrido de la superficie S-3 y llevaba el mismo defecto que los tres planificados, de modo que se declara acá y en el informe de M6 en lugar de corregirse en silencio (`Migracion-Rules.md` §4.2). La trazabilidad upstream de la cabecera ubicaba las categorías `05-Arquitectura-Tecnica` y `09-Devops` bajo `Proyectos/`, el layout anterior a la 8.0, y pasa a `Unidades-Entrega/`. **Se retiró el número «siete»**, porque el cambio de ruta lo habría vuelto falso. Ningún apartamiento `AP-01` a `AP-04`, ninguna decisión y ningún criterio de la etapa `a` cambia. Estado anterior archivado en `_legacy/2026-08-17/`. Sube **minor**. |
| 1.8 | 2026-08-13 | **Cierra `PT-01.c`, el único criterio propio que 1.7 dejaba abierto, midiéndolo sobre el hosting real.** La fila **5** de la tabla de los ocho criterios de **§5**, la de **§5.1** y la de **§5.2** pasan de **NO CERRADO** a **CERRADO**: contra `https://www.aplicada.somee.com/estado` se corrieron **20 min 6 s** de navegación continua —el número real, sin redondear— con **20 interacciones y las 20 con efecto observable**, sobre **una única conexión del circuito**, **cero** negociaciones nuevas, **cero** rearmes y **cero** recargas de la página; y un **corte de red real de 26,1 s** con el aviso de reconexión del propio front visible durante todo el corte, vuelta **automática** a los **7,0 s** de restablecer sin recargar, y pulsación posterior **con efecto observable**. El resultado y su evidencia **no se escriben acá**: viven en [`../00-Contexto/Compatibilidad-Plataformas.md`](../00-Contexto/Compatibilidad-Plataformas.md) **§2.6.1**, que además declara **el método de detección del reciclado desde el cliente** —obligado, porque sobre el hosting no hay registros del servidor— y su **control del instrumento**. **§5.1** reescribe el párrafo de alcance: lo que queda fuera de lo medido ya no es la mitad de hosting sino el reciclado **por inactividad**, que la corrida no ejerce, y la **latencia percibida en producción**. **§5.2** reescribe encabezado, entrada y cierre: **los ocho criterios propios quedan cerrados**. **`R-07` de §7 no se borra**: sigue declarado como riesgo sin mitigación en el código, con la marca de que quedó **ejercido y no materializado** para el reciclado por navegación continua. **Recuento: ocho de ocho.** **No cambia ninguna decisión del plan, ningún ítem, ningún árbol y ningún nombre**: sólo agrega evidencia. Sube minor. |
| 1.7 | 2026-08-13 | **Cierra `PT-01.a` con el despliegue real al hosting público, y con él la cuenta de criterios cambia.** La fila **3** de la tabla de los ocho criterios propios de **§5**, la de **§5.1** y la de **§5.2** pasan de **SIN MEDIR** a **CERRADO**: el front se publicó por FTP y `https://www.aplicada.somee.com/estado` responde **200**, la raíz responde **404** —lo esperado con una sola ruta servida—, y **la incógnita de versión de plataforma quedó resuelta: el hosting soporta `net10.0`**, de modo que la salida declarada —bajar la versión objetivo del front, nunca la del backend— **no se ejerció**. La fila **4**, `PT-01.b`, también pasa a **CERRADO** y **con el resultado corregido**: sobre el hosting real **no hay WebSockets** —la negociación devuelve sólo `ServerSentEvents` y `LongPolling`—, de modo que **la sesión interactiva va por long polling en producción**; es el repliegue ya ejercido y declarado aceptable, **no obliga a cambiar el modelo de front**, y deja la latencia de desarrollo explícitamente **no extrapolable**. **Recuento: siete de los ocho criterios propios quedan cerrados —1, 2, 3, 4, 6, 7 y 8— y uno no: el 5, `PT-01.c`**, cuya mitad de hosting sigue sin medir y es donde vive el riesgo de que el proceso recicle la sesión. **§5.2** reescribe su encabezado y su cierre en consecuencia, conserva la comprobación previa a publicar como registro de por qué la publicación no encontró sorpresas, y enlaza el mecanismo del hosting ya documentado y el reporte de experiencia. **No cambia ninguna decisión del plan, ningún ítem y ningún árbol.** Sube minor. |
| 1.6 | 2026-08-13 | **Deja documentado el estado de cierre de la etapa `a` completo y establece con precisión qué falta para medir `PT-01.a`**, que es el único de los **ocho** criterios propios sin medir. Agrega **§5.2** con cuatro cosas: la tabla del estado de los ocho, cada uno con dónde vive su evidencia —confirmación o sección de [`../00-Contexto/Compatibilidad-Plataformas.md`](../00-Contexto/Compatibilidad-Plataformas.md)—; **qué es exactamente `PT-01.a`** leído en el intake §17.2.P.9 · GeometriaFactory-Web y §17.2.P.10 · GeometriaFactory-Web y en `Web/09` `Pipeline-CI-CD.md` §2.1 y §9 —umbral, incógnita que resuelve, quién la mide y sobre qué artefacto—, con la constancia de que **es la única puerta del producto que se mide sobre algo ya desplegado** y de que por eso **no se puede medir en el entorno de desarrollo**; **qué tiene que existir, quién lo provee y en qué orden** —cuenta en el hosting, secretos del repositorio nombrados por su función, una corrida del flujo entero, y que la dirección interrogada corresponda a una ruta servida—, todo del Product Owner y **nada de ello código**; y **qué se comprobó corriendo el 2026-08-13 sin hosting y sin ningún secreto**: el flujo es un documento válido de **ocho** pasos en el orden declarado, su filtro tiene las **tres** rutas del intake **1.22**, el paso 4 regenera el bundle en el mismo flujo, el paso 5 publica con **0 advertencias**, el paso 6 inyecta y **se detiene con el secreto vacío**, y el comando del paso 8 hace lo que dice. Declara el **hallazgo** de que en la etapa `a` la publicación **sirve la página de estado y no la raíz**, de modo que un paso 8 apuntado a la raíz desnuda daría rojo con una publicación correcta, y deja la resolución donde corresponde: el valor del secreto de la dirección pública, y la etapa `b`. La fila **3** de la tabla de los ocho criterios y la de §5.1 quedan marcadas **sin medir** y apuntan a §5.2. **No cambia ningún criterio, ningún árbol, ningún nombre y ningún riesgo, y no da por medido nada**: sólo registra estado, evidencia y dependencias. |
| 1.5 | 2026-08-13 | **Registra la medición corrida de `PT-01.c`**, el criterio 5 de §5, que 1.4 declaraba sin medir. La fila **5** de la tabla de los ocho criterios propios y la tabla de **§5.1** pasan de «sin medir» a **medido en el entorno de desarrollo contenido con navegador real, con las dos mitades del criterio ejercidas y pasadas**: **20 min 45 s** de navegación continua con **20 interacciones**, todas con efecto observable, sobre **un único circuito** identificado por su identificador del lado del servidor y **sin una sola reconexión**, y **corte de red real** —matando el proceso por el que sale el navegador— con aviso visible del front, vuelta automática y reconexión **al mismo circuito**. El resultado y su evidencia **no se escriben acá**: viven en [`../00-Contexto/Compatibilidad-Plataformas.md`](../00-Contexto/Compatibilidad-Plataformas.md) §2.5. Agrega el párrafo que declara **qué queda fuera de lo medido**: el reciclado del proceso es un riesgo **del hosting** y sólo se puede observar ahí, de modo que `PT-01.c` **no queda cerrado** y **`R-07` de §7 sigue en pie**. **No cambia ningún criterio, ningún árbol, ningún nombre y ningún riesgo**: sólo agrega evidencia. |
| 1.4 | 2026-08-13 | **Registra lo que ya se midió corriendo sobre el producto en ejecución, que hasta acá el plan sólo declaraba cómo se mediría.** Agrega **§5.1** con tres cosas: la tabla de qué criterios de §5 están medidos y cuáles no —`PT-01.a` y `PT-01.c` siguen sin medir porque dependen de la publicación al hosting, que la etapa no hizo—; **el método elegido para el criterio 8, `RA-01`**, con sus tres patas —imposibilidad topológica, enumeración completa de lo que el navegador pide y **control del instrumento**, haciéndole pasar la violación a propósito para probar que la registra— más la inspección del contenido servido; y lo que el recorrido demuestra en positivo, que la llamada al servicio de datos **existe y sale del proceso del front**. Las filas **4** y **8** de la tabla de los ocho criterios propios quedan marcadas con su estado real y apuntan al resultado. El resultado de **`PT-01.b`** no se escribe acá: vive en [`../00-Contexto/Compatibilidad-Plataformas.md`](../00-Contexto/Compatibilidad-Plataformas.md) §2.4, que es el documento cuya §5 declaraba ese transporte «sin verificar». **No cambia ningún criterio, ningún árbol, ningún nombre y ningún riesgo**: sólo agrega evidencia. | 
| 1.3 | 2026-08-13 | **Tramo `R-2` del plan de renombre de [`Norma-De-Nomenclatura.md`](Norma-De-Nomenclatura.md) 1.4 §8, ejecutado contra el glosario de su §6 y no por criterio propio.** **Acto 1 · el renombre** de los **tres puertos declarados** de su §6.3 —`IRepositorioTrabajos` ⟶ `IWorkRepository`, `IValidadorFiguras` ⟶ `IFigureValidator` e `IRelojDelSistema` ⟶ `ISystemClock`— y los **dos miembros** de su §6.5 —`HashContrasena` ⟶ `PasswordHash` y `JsonOriginal` ⟶ `OriginalJson`—. Acá son **15 ocurrencias**: los tres puertos declarados de §1.5 y su tabla de adaptadores, la alternativa `P-4c` de §1.5, los dos fundamentos 1 y 2 de §1.2, y los **tres nombres de archivo** de `Ports/` en el árbol de §2.1, que §2.3 de la norma cuenta y `V-6` cuadra. **Acto 2 · la corrección del texto que quedaba argumentando por el nombre viejo**, sobre este mismo documento: los **fundamentos 1 y 2 de `P-1a` en §1.2** decían «están en castellano» de identificadores que este tramo acaba de pasar a inglés en la fuente. **Se conservan, no se borran**: el texto refutado queda tachado y el párrafo lleva la marca que `V-7` exige —estado, fecha y sección de la norma que lo supera—, y se declara qué sobrevive de cada fundamento: el prefijo `I` en el primero y la forma `PascalCase` en el segundo, que son forma y no idioma. Los fundamentos 3, 4 y 5 **no se tocan**: son de `R-2b`, `R-4` y `R-3`. **Cuadre `V-4` en las dos direcciones, contra la lista escrita antes de editar:** 64 ocurrencias candidatas medidas en 13 documentos con el instrumento de la norma §2.1, **63 renombradas y 1 no renombrada** —la cita textual de la línea de trazabilidad upstream de `RC-10001-Texto-Original-Escrito-Una-Sola-Vez.md`, que atribuye al `PRODUCT-INTAKE` **1.12** las palabras «`JsonOriginal` conservado íntegro y nunca reescrito» y que **renombrar falsificaría**—. `V-6` cuadró los tres nombres de archivo de `Ports/`. **Esta fila queda fuera del cuadre**, por el punto 4 de `V-4`: al describir lo que hizo reintroduce los identificadores viejos. |
| 1.2 | 2026-08-12 | **Tramo `R-1b` de [`Norma-De-Nomenclatura.md`](Norma-De-Nomenclatura.md) 1.2 §8, que salda la deuda del tramo de ensayo `R-1` y es condición de entrada de `R-2`. Son los dos actos de su §8.2.** **Acto 1 · las cuatro superficies derivadas que ninguna regla cubría cuando corrió `R-1` y que §6.11 ahora sí cubre**, renombradas contra sus cuatro filas: la carpeta `Configuraciones/` ⟶ `Configurations/` de §2.1, la carpeta `Paginas/` ⟶ `Pages/` de §2.1 y §2.2, la carpeta `visor/` de la **capa 3** del bundle ⟶ `viewer/` en §2.1 —**no** la raíz `visor/` del proyecto de código, que §1 y §6.11 declaran intocable— y el segmento `Internos` ⟶ `Internal` del contraejemplo de la regla 1 de §1.3. Son **5 ocurrencias**, las que §2.3 de la norma mide. Rige su §6.11: **por debajo del nivel de espacio de nombres el idioma no se afloja**, y la regla de §4 sobre nombres de archivo en castellano alcanza **sólo** a `SDD/` y no a nada bajo `src/`, `tests/` ni `visor/`. **Acto 2 · la corrección del texto que quedaba argumentando por el nombre viejo**, que es la mitad que §8.2 agregó y que `R-1` no hizo, con sus tres barridos sobre este único documento: **§1.2 `D-01` queda declarada superada, no borrada.** La propuesta `P-1a` —identificadores en castellano sin tildes ni eñes— se marca **SUPERADA el 2026-08-12 por la norma §3**, y sus **cinco fundamentos se conservan** con el estado de cada uno: los cuatro primeros siguen siendo ciertos como reporte de sus fuentes y dejan de ser fundamento, con el tramo donde se renombran —`R-2`, `R-2b` y `R-4`—; **el quinto queda refutado por §5.1**, porque el intake §17.2.P.3 · GeometriaFactory-Visor declara los nombres de la fachada «a fijar en la etapa que la implementa» y por lo tanto **nunca estuvieron fijados**. `P-1b` —identificadores en inglés— pasa a ser **la alternativa que rige**, con el fundamento nuevo: la traducción vive en **una tabla única**, el glosario de §6, y no en cada punto de la cadena, que es la forma en que `RI-06` de `Vista-Producto.md` §7 pide evitar el defecto. **La consecuencia que §1.2 declaraba inevitable —escribir sin tildes ni eñes— deja de existir** y §3 de la norma la declara beneficio lateral. La misma afirmación refutada en **§1.7** se corrige citando §5.1 y §6.6, con los seis nombres definitivos. **Controles:** `V-4` cuadró contra la lista escrita antes de editar —22 ocurrencias candidatas, 5 renombradas, 17 no renombradas por §4.1: 4 de registro histórico y 13 de otro concepto, reporte de fuente o prosa—; `V-6` cuadró las cuatro superficies; `V-7` cuadró la coherencia interna. **No renombra ninguna otra clase** por la regla 1 de §8: `VisorFiguras.razor` ⟶ `FigureViewer.razor` es de `R-3` y no de este tramo. **No toca las filas de control de cambios**, que son registro histórico (§4.1). |
| 1.1 | 2026-08-12 | **Tramo `R-1` del plan de renombre de [`Norma-De-Nomenclatura.md`](Norma-De-Nomenclatura.md) 1.1 §8, ejecutado contra el glosario de su §6 y no por criterio propio.** Renombra a inglés **los identificadores que este documento propone** y sólo ésos: los **18 espacios de nombres** de §1.3 —16 subsegmentos distintos— por §6.10; los **14 tipos y adaptadores** de §1.6 y §1.7 y los **6 derivados** —los cinco mapeos de `BT-12005` y el componente de la página de estado— por §6.4; y los **2 puertos propuestos** de §1.5, `P-4a` y `P-4b`, por §6.3. Alcanza también las **rutas de carpeta** que la regla 2 de §1.3 obliga a hacer coincidir con el subsegmento —`Entities/`, `Values/`, `Ports/`, `Persistence/`, `Composition/`, `Endpoints/`, `Components/`, `Integration/`— y los **nombres de archivo** de §2.1, que la norma §3 fija iguales al tipo que contienen. **Cuadre `V-4` en las dos direcciones: 0 ocurrencias de los nombres viejos y 81 de los nuevos, contra 81 medidas antes de editar.** **No toca ninguna otra clase**, por la regla 1 de §8: las cinco entidades, los tres puertos declarados, los dos miembros, las seis funciones de la fachada y los diez valores de conjunto cerrado siguen en castellano hasta su propio tramo. **No toca la prosa**, ni los identificadores documentales, ni el dato del alumno. **Y no toca `D-01` §1.2**, cuya propuesta `P-1a` —identificadores en castellano— quedó superada por la norma 1.1 §3: corregirla es contenido y no renombre, y se eleva al punto de control. Quedan sin traducir, y se declaran, **tres identificadores sin fila en el glosario**: las carpetas `Configuraciones/` y `Paginas/`, que no son subsegmento de espacio de nombres, y el segmento `Internos` del contraejemplo de la regla 1 de §1.3. |
| 1.0 | 2026-08-12 | **Emisión inicial.** Plan de la etapa `a` para el punto de control del Product Owner, emitido **antes de escribir código**. Propone, sin decidir, las seis decisiones de nombres que `Handoff-Checkout.md` §6.2 registra como `A-1` y `A-2` —idioma y forma de los identificadores, esquema de espacios de nombres, nombre de la entidad de cuenta, nombre del cuarto puerto, criterio de nombrado de adaptadores y tipos centrales del esqueleto—, cada una con su alternativa real y su costo donde la hay. **Declara que el nombre de la solución y los de los siete proyectos NO son un punto abierto**: están declarados en `PRODUCT-INTAKE` §13 y §16 y derivados en `PRODUCT-MANIFEST` **1.3** §1 y §2, con estado `Aprobado`. Fija el árbol de archivos siguiendo el del intake §16, con **cuatro** apartamientos declarados —`Directory.Build.props` y `.editorconfig`, ampliación del `.gitignore`, la página de estado fuera de la línea de base visual, y `visor/dist/` generado y no versionado—. **Verifica y cita** el orden de construcción en los cuatro niveles topológicos de `Pipeline-Producto.md` §2, contrastado contra `PRODUCT-MANIFEST` §3 y `PRODUCT-INTAKE` §13, que coinciden. Declara los **siete** guiones del intake §16 con su criterio de éxito y su fuente, y marca que **`run-web.sh` y `migrate.sh` no tienen contenido declarado por ninguna fuente**. Mapea los **ocho** criterios propios de la transición `a` → `b` y los **siete** comunes contra lo que los demuestra. Enumera **catorce** cosas que la etapa `a` no hace, cada una con la etapa donde sí ocurre. Eleva **siete** riesgos y contradicciones sin resolver, entre ellos la contradicción de orden entre `Infrastructure BT-12005` (etapa `a`) y `Domain BT-12006` (etapa `c`) y la divergencia `Alumno` / `Cuenta`, **ninguno de los cuales existía como punto abierto registrado**. **No toma ninguna decisión, no reabre ninguna de las 45 ADR emitidas y no modifica ningún otro documento del corpus.** |
