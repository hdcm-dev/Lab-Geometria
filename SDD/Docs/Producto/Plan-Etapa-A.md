# Plan de la etapa `a` — Esqueleto ambulante y verificación de viabilidad

**Producto:** Fábrica de Geometría
**Documento:** Plan-Etapa-A.md
**Versión:** 1.3
**Estado:** Propuesto
**Fecha:** 2026-08-13
**Nivel:** Producto
**Trazabilidad upstream:** [`Norma-De-Nomenclatura.md`](Norma-De-Nomenclatura.md) **1.2** §3, §4 y §5 (las decisiones que superan a `D-01`), §6.3, §6.4, §6.10 y §6.11 (el glosario contra el que se renombró), §7 (`V-4`, `V-6` y `V-7`) y §8 con §8.2, tramos `R-1` y `R-1b`; [`PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.29** §13, §14, §15, §16, §16.1 y §17.1 a §17.7; [`PRODUCT-MANIFEST-Fabrica-De-Geometria.md`](../../Intake/PRODUCT-MANIFEST-Fabrica-De-Geometria.md) **1.3** §1, §1.2, §2 y §3; [`../00-Contexto/Roadmap-Producto.md`](../00-Contexto/Roadmap-Producto.md) §2.2, §4 y §5; [`Pipeline-Producto.md`](Pipeline-Producto.md) §2, §6 y §9; [`Vista-Producto.md`](Vista-Producto.md) §3.1, §4 y §5; [`../Handoff-Checkout.md`](../Handoff-Checkout.md) §4 y §6; las **siete** categorías `05-Arquitectura-Tecnica` y las **siete** `09-Devops` bajo `Proyectos/`
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

1. Los **tres puertos que el intake sí declara** son `IWorkRepository`, `IFigureValidator` e `ISystemClock` (intake §13, §14 y §17.2.P.1). ~~Están en castellano y llevan `I`.~~ **Superado el 2026-08-13 por la norma §6.3, y ya ejecutado:** el tramo `R-2` renombró **la fuente y este reporte con ella**, en la misma edición y por la norma §4.1 punto 1, de modo que el intake ya no los nombra en castellano y este renglón reporta lo que la fuente dice hoy. Del fundamento sobrevive **sólo el prefijo `I`**, que es forma y no idioma (norma §3).
2. Los **campos del modelo de datos que el intake nombra literalmente** son `PasswordHash` (§17.1.P.5) y `OriginalJson` (§17.3.P.4). ~~Están en castellano, sin tildes, en `PascalCase`.~~ **Ídem, superado el 2026-08-13 por la norma §6.5 y ya ejecutado en el mismo tramo `R-2`:** la fuente y este reporte se renombraron juntos, y del fundamento sobrevive **sólo la forma `PascalCase`**, que no es idioma. La incomodidad que el fundamento arrastraba —escribir sin eñe ni tilde— desapareció con el idioma, como declara el párrafo de más abajo.
3. Las **cinco entidades** que el intake nombra son `ALUMNO`, `TRABAJO`, `PIEZA`, `COMPONENTE` y `OBSERVACION` (§17.3.P.4), y `Definicion-Modelo-De-Dominio.md` §2 las declara en castellano. **Ídem:** la norma §6.4 y §6.9 **unifican** las cinco tablas en mayúsculas con las cinco entidades en cinco nombres ingleses —`Account`, `Work`, `Piece`, `Component`, `Observation`— en el tramo `R-2b`.
4. Los **conjuntos cerrados** están declarados por sus valores en castellano y se guardan y se serializan **por su nombre, nunca por su posición**: papel (`Alumno`, `Administrador`), estado de cuenta (`Pendiente`, `Habilitado`, `Bloqueado`), estado del trabajo (`Borrador`, `Pendiente`, `Finalizado`, `Rechazado`) y especie de observación (`Advertencia`, `Error de validación`) — `Contratos-REST.md` §2.2 y `Modelo-Datos-Logico.md` §2. **El hecho es cierto y por eso era zona de frontera, no fundamento:** la norma §5.2 lo decidió como `F-02a` —identificador en inglés, etiqueta en castellano— con costo cero mientras no haya base poblada, y su §6.7 trae los diez pares. El renombre es el tramo `R-4`.
5. ~~Las **seis funciones de la fachada del visor** están fijadas por el intake §17.7.P.3.~~ **Refutado por la norma §5.1, contra la letra de la fuente:** el intake §17.7.P.3 encabeza su tabla «Contrato de la fachada, **con los nombres definitivos a fijar en la etapa que la implementa** (RT §8.4)». **Nunca estuvieron fijados.** La norma §5.1 los fija por `F-01a` y su §6.6 los declara: `initialize`, `loadJson`, `selectPiece`, `resize`, `destroy`, `setMotion`, en el tramo `R-3`. Se conserva la convención de `camelCase`, que es de TypeScript y no de idioma.

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

**Alternativa real `P-2b`: espacio de nombres plano, sólo la `Identidad-Codigo`.** Pro: cero decisiones, cero deriva entre carpeta y espacio de nombres. Contra, y es el costo concreto: `GeometriaFactory-Infrastructure` tiene **26 tareas técnicas** en su backlog y cinco áreas sin relación entre sí —persistencia, seguridad, validación, tiempo, arranque—; un solo espacio de nombres las mezcla, y la puerta de `Application` de **cero pruebas que tocan la base de datos real** (`BT-06`, etapa `a`) queda sin una frontera de nombres donde apoyarse. Es viable en `Contracts` y en `Domain`; no lo es en `Infrastructure` ni en `Api`.

**Alternativa `P-2c`: subsegmento por capa de Clean Architecture dentro de cada proyecto** (`…​.Aplicacion`, `…​.Dominio`). Se descarta: el proyecto **ya es** la capa; el subsegmento repetiría el nombre del ensamblado.

### 1.4 `D-03` · El nombre de la entidad de cuenta: una divergencia real del corpus

**Este punto no está registrado como `PA-XX` en ninguna categoría y aparece al comparar dos documentos aprobados. Se eleva.**

| Fuente | Cómo nombra la entidad |
| --- | --- |
| `PRODUCT-INTAKE` §13 y §17.3.P.4 | Entidad **`ALUMNO`**, dentro de las cinco: `ALUMNO`, `TRABAJO`, `PIEZA`, `COMPONENTE`, `OBSERVACION` |
| `Domain/02` `Definicion-Modelo-De-Dominio.md` §2.1 | Entidad **`Alumno`**, con atributo `Role` que vale `Alumno` o `Administrador` |
| `Infrastructure/02` `Modelo-Conceptual.md` §3.1 y §3.2 | Entidad **`CUENTA`**, en el diagrama y en la tabla |
| `Infrastructure/05` `Modelo-Datos-Logico.md` §2.1 | Tabla **`Cuenta`**, «entidad conceptual de origen: **Cuenta**» |
| `Application/05` §3.1 y `ADR-02` | «repositorio de **cuentas**», «orquestación del alta de **cuentas**», «gobierno de **cuentas**» |
| `Api` `Contratos-REST.md` §3 | «Listar las **cuentas** de la comisión», «cambiar la situación de una **cuenta**» |

**Verificado con `grep` el 2026-08-12 sobre el árbol vivo de `GeometriaFactory-Infrastructure`, excluido `_legacy/`: la cadena `ALUMNO` aparece en cuatro lugares y en los cuatro es un fragmento de un código del contrato, nunca un nombre de entidad ni de tabla.** El renombre a `Cuenta` ocurrió y no está declarado como apartamiento en ninguna parte.

**Propuesta principal `P-3a`: la entidad se llama `Cuenta`.** Fundamento: es el término que usan cinco de las seis fuentes de la tabla, es el que hace verdadero el invariante `INV-08` —la cuenta con papel `Administrador` no es un alumno y sin embargo es una fila de esa misma entidad— y es el que evita que el tipo `Alumno` tenga un miembro `Role` cuyo valor pueda ser `Administrador`, que es una contradicción legible en la primera línea del archivo.

**Alternativa `P-3b`: la entidad se llama `Alumno`.** Pro: es la letra del intake §13 y §17.3.P.4, que es la fuente de mayor rango. Costo: obliga a corregir `Modelo-Conceptual.md`, `Modelo-Datos-Logico.md` y las tablas de trazabilidad que los citan, y deja el nombre del tipo en contradicción con su propio atributo `Role`.

**Cualquiera de las dos que se elija, la otra fuente queda con una corrección pendiente.** Este plan no puede corregir el intake: `Master-Prompt.md` §15 reserva ese acto a su autor.

### 1.5 `D-04` · El nombre del cuarto puerto

**Es el punto abierto que más documentos toca.** `Application/05` `PA-01` lo declara, `Infrastructure/05` `PA-01` declara que **no lo fija** porque no puede nombrar un tipo que no declara, `Application ADR-02` §2 confirma que **el puerto existe** y deja el nombre abierto, y `Producto/11-Documentacion/README.md` §7 lo lista como uno de los seis puntos heredados que bloquean su cuerpo documental.

**Qué hace el puerto, declarado por `Application ADR-02` §2 punto 1 y por su §3.1:** recuperar una cuenta por su correo, responder si un correo ya está registrado, responder si ya existe una cuenta con papel `Administrador`, y materializar el resultado incluida la marca de cambio de contraseña pendiente.

**Propuesta principal `P-4a`: `IAccountRepository`.**

Fundamento, y es literal: `Infrastructure ADR-03` §6 punto 4 declara que la propuesta que llega al punto de control es que **el identificador del cuarto puerto siga el patrón de los tres que el intake sí declara**, y que esos tres «empiezan por la misma letra de contrato y nombran la cosa, no el mecanismo». Aplicado:

| Puerto | Identificador | Origen |
| --- | --- | --- |
| Repositorio de trabajos | `IWorkRepository` | **Declarado** por el intake §13, §14 y §17.2.P.1 |
| Validación de figuras | `IFigureValidator` | **Declarado** por las mismas |
| Reloj del sistema | `ISystemClock` | **Declarado** por las mismas |
| Repositorio de cuentas | **`IAccountRepository`** | **Propuesta** de este documento, por el criterio de `Infrastructure ADR-03` §6 |

Además, `Application ADR-02` §2 nombra al puerto en lenguaje de dominio como **«repositorio de cuentas»** y `Application/05` §3.1 lo repite en su componente de declaración de puertos. La propuesta es el mismo sintagma llevado a identificador, sin acuñar nada.

**Alternativa real `P-4b`: `IStudentRepository`.** Sólo tiene sentido si `D-03` se resuelve por `P-3b`. Costo: el puerto responde también por la cuenta con papel `Administrador` —`INV-05` exige saber si ya existe una, y ése es exactamente el puerto que lo responde según `ADR-02` §5 punto 2—, de modo que un puerto llamado «de alumnos» que se consulta para saber si existe el administrador miente en su nombre. **`P-4a` y `P-3a` se sostienen mutuamente y se recomienda decidirlas juntas.**

**Alternativa `P-4c`: no abrir el cuarto puerto y resolver la unicidad dentro de `IWorkRepository`.** Está considerada y **descartada** por `Application ADR-02` §4: sería una frontera con dos dominios adentro, y haría que el alta de cuenta dependiera del repositorio de trabajos. No se reabre acá.

### 1.6 `D-05` · El criterio de nombrado de los adaptadores

`Infrastructure ADR-03` §6 punto 4 **ya fija el criterio y no hace falta decidirlo**: el adaptador «se nombra por el puerto que implementa y por el mecanismo que usa, en ese orden». Lo que este plan aporta es la aplicación del criterio, para que el punto de control la confirme de una vez:

| Puerto | Adaptador propuesto | Mecanismo declarado |
| --- | --- | --- |
| `IAccountRepository` | `EfCoreAccountRepository` | EF Core sobre SQLite (intake §17.3.P.4) |
| `IWorkRepository` | `EfCoreWorkRepository` | EF Core sobre SQLite |
| `IFigureValidator` | `LocalFigureValidator` | Motor propio, **sin red** (intake §17.3.P.3) |
| `ISystemClock` | `UtcSystemClock` | Momento en tiempo universal coordinado (`Modelo-Datos-Logico.md` §2.1 y `RC-06`) |

Las cuatro filas son **propuesta** derivada del criterio; el criterio es declarado. **Los adaptadores se nombran en la etapa `a` pero sólo dos se construyen después**: `BT-09` (cuentas) y `BT-12` (reloj) son de etapa `c`, `BT-10` (trabajos) de etapa `e` y `BT-16` (validador) de etapa `f`.

### 1.7 `D-06` · Los nombres de los tipos centrales que el esqueleto necesita

**El esqueleto necesita menos tipos de los que el producto tendrá, y más de los que «esqueleto» sugiere.** Lo que sigue sale de los 48 ítems de etapa `a` de `Handoff-Checkout.md` §4, no de una idea de qué debería tener un andamiaje.

**`GeometriaFactory.Domain`** — su etapa `a` es `BT-01` a `BT-05`: crear el proyecto y sus puertas. **`BT-06`, «construir el núcleo de entidades con las cinco entidades del modelo», es de etapa `c`.** Ver `R-02` en §7: `Infrastructure BT-05`, que es de etapa `a`, mapea esas cinco entidades. La propuesta de este plan es que la etapa `a` cree **los tipos, sin invariantes**, y que `BT-06` los llene en `c`:

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

**`GeometriaFactory.Application`** — su etapa `a` es `BT-01` a `BT-06`. Los tipos que necesita son los cuatro puertos de §1.5 y nada más: sus ocho componentes de orquestación pertenecen a etapas posteriores.

**`GeometriaFactory.Infrastructure`** — su etapa `a` incluye `BT-05` (contexto y mapeo de las cinco entidades), `BT-06` (preparación del almacén), `BT-08` (zona horaria y precisión de los sellos) y las historias `US-24` y `US-25`:

| Tipo propuesto | Qué es | Base declarada |
| --- | --- | --- |
| `GeometriaFactoryDbContext` | Contexto de persistencia, uno por operación | Intake §17.3.P.4; `Infrastructure/05` §3.1 |
| `StorePreparation` | Aplica las transformaciones al arrancar y **detiene el arranque** ante un esquema que no corresponde | `ADR-07`; `BT-06`; `US-24`, `US-25` |
| `AccountConfiguration`, `WorkConfiguration`, `PieceConfiguration`, `ComponentConfiguration`, `ObservationConfiguration` | Los cinco mapeos, uno por entidad | `BT-05`; `Modelo-Datos-Logico.md` §2 |

**`GeometriaFactory.Api`** — su etapa `a` incluye `BT-02` (composición de raíz con los cuatro puertos), `BT-03` (arranque en dos fases con el punto de salud sin acceso), y `US-26` a `US-29`:

| Tipo propuesto | Qué es | Base declarada |
| --- | --- | --- |
| `CompositionRoot` | Conecta **4 de 4** puertos con su adaptador, **0** sin adaptador y **0** con más de uno | `ADR-06`; `QG-10` de `Api/09` `Pipeline-CI-CD.md` §2.1 |
| `TwoPhaseStartup` | Prepara el almacén antes de atender la primera petición; **0** peticiones atendidas con la preparación incompleta | `ADR-07`; `QG-11`; `US-27`, `US-28` |
| `HealthEndpoint` | Realiza `A-16`, **fuera de la guardia** | `Contratos-REST.md` §3; `US-29` |

**`GeometriaFactory.Contracts`** — su etapa `a` es `BT-01` a `BT-03`: crear el ensamblado y sus dos puertas. **`PA-01` de `Contracts` ancla sus nombres de tipos «etapa `c` en adelante, según la familia»**, de modo que la etapa `a` **no crea ningún tipo de transferencia**. Ver `R-03` en §7: el cuerpo de la respuesta de `A-16` no tiene tipo declarado en ninguna parte.

**`GeometriaFactory.Web`** — su etapa `a` incluye `BT-03`, la página de salud. Un solo tipo, y es andamiaje:

| Tipo propuesto | Qué es | Base declarada |
| --- | --- | --- |
| `Status` (componente Blazor) | Página de estado que consume el punto de salud y muestra datos reales | Intake §15; `Roadmap-Producto.md` §5.2, transición `a` → `b`; `Web BT-03` |
| `DataServiceClient` | La **única** salida hacia el servicio de datos | `Web/05` §3.1, capa 3; §3.2 punto 3 |

**`GeometriaFactory-Visor`** — sus seis funciones de fachada **quedaron fijadas por [`Norma-De-Nomenclatura.md`](Norma-De-Nomenclatura.md) §5.1 y §6.6 el 2026-08-12, y hasta entonces no lo estaban por ninguna fuente**: el intake §17.7.P.3 las dejaba «a fijar en la etapa que la implementa», contra lo que este documento afirmaba en su §1.2 punto 5. Son `initialize`, `loadJson`, `selectPiece`, `resize`, `destroy` y `setMotion`, y se renombran en el tramo `R-3`. `Visor/05` `PA-02` declara abiertos únicamente los nombres internos, y los ata a la **etapa `g`**, no a la `a`. La etapa `a` crea el proyecto y un bundle «vacío pero real» (intake §15), con la fachada declarada y sin lógica de dibujo.

### 1.8 Las otras decisiones ancladas al mismo punto de control

**No son nombres, y llegan al mismo punto de control.** Se listan para que el Product Owner las vea juntas y no las descubra de a una:

| # | Qué falta decidir | Titular declarado | Dónde |
| --- | --- | --- | --- |
| `A-3` | Cuál de las dos funciones de derivación de clave se ancla, y con qué parámetros. **El intake declara «PBKDF2 o Argon2» y no elige** | Product Owner y equipo | `Infrastructure/05` `PA-03`; `ADR-04` §7; `BT-03` |
| `A-4` | Las rutas y los verbos definitivos de los **quince** puntos de acceso. La fuente declara **dos** cosas: la ruta del canje y la **existencia** del punto de salud | Product Owner y equipo | `Api/05` `PA-01`; `BT-07` |
| `A-6` | La vigencia exacta del acceso firmado. **El intake dice «corta» y no fija número** | El equipo, y el Product Owner | `Api/05` `PA-04`; `BT-10` |
| `A-7` | El valor del límite de tamaño del cuerpo. `ADR-02` §2 punto 6 fija la forma y deja el número | El equipo | `Api/05` `PA-05`; `BT-09` |
| `A-21` | La zona horaria y la precisión del campo de momento del tipo de error | El equipo | `Contracts/05` `PA-02` |
| `V-4` | La versión de plataforma que soporta el hosting. **Se resuelve midiendo: es `PT-01.a`** | La medición | `Web/05` `PA-02` |
| `V-5` | La versión exacta de la biblioteca de componentes de interfaz | El equipo | `Web/05` `PA-01`; `BT-02` |
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
│   │   ├── GeometriaFactory.Domain.csproj            0 dependencias salientes (puerta BT-04)
│   │   ├── Entities/{Cuenta,Trabajo,Pieza,Componente,Observacion}.cs
│   │   └── Values/{Role,AccountStatus,WorkStatus,ObservationKind}.cs
│   ├── GeometriaFactory.Contracts/
│   │   └── GeometriaFactory.Contracts.csproj         0 referencias hacia Domain (puerta BT-02)
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
| `changelog.md` | Se actualiza **en la rama de la etapa, no después de la fusión** | Intake §16 y §17.5.P.7 |
| `.devcontainer/devcontainer.json` | **El único ambiente que existe.** El host no tiene ni va a tener el kit de desarrollo, y ningún guion corre fuera de él | Intake §16 y encabezado de la Parte C; `Domain/09` `Entornos-Deploy.md` §2 |
| `.vscode/launch.json` | Depuración por F5, **separada de los guiones** | Intake §16 |
| `.github/workflows/deploy-front-ftp.yml` | Publica el front por transferencia al hosting. Su filtro de rutas lleva **tres** entradas —el front, el visor y **los contratos**—, y la tercera entró por una corrección declarada: sin ella, un cambio del contrato no dispara la publicación y las dos unidades quedan desalineadas sin que nada falle. **No termina en la subida: termina comprobando que la dirección pública responde** | Intake §17.6.P.7 y §17.6.P.8; `Pipeline-Producto.md` §4 |
| `deploy/Dockerfile` | Imagen del servicio de datos, **multietapa**, con sólo el entorno de ejecución y **sin linaje con la imagen del contenedor de desarrollo**. Es lo que `PT-04` mide | Intake §17.5.P.8 y §17.5.P.9 |
| `deploy/compose.yaml` | Despliegue en destino construyendo desde el repositorio, con `healthcheck` contra el punto de salud. **El acto de desplegar es manual y del Product Owner**: la canalización no lo ejecuta | Intake §17.5.P.7 y §17.5.P.8; `Api/09` `Pipeline-CI-CD.md` §1 |
| `scripts/` (7 guiones) | Ver §4 | Intake §16 |
| `*.csproj` (6) | Un archivo de proyecto por carpeta, con las dependencias de compilación exactas del manifiesto §2 y **las versiones de paquete ancladas explícitamente**: toda versión se fija en el archivo, y un cambio mayor es una decisión que se documenta, nunca el efecto de una actualización | Manifiesto §2; intake, encabezado de la Parte C |
| `Domain/Entities/` y `Domain/Values/` | Los cinco tipos y los cuatro conjuntos cerrados, **sin invariantes** en esta etapa. Ver `R-02` | `Definicion-Modelo-De-Dominio.md` §2; `Infrastructure BT-05` |
| `Application/Ports/` | Los **cuatro** puertos, que son **la única frontera del proyecto de código** | `Application ADR-02` §2 |
| `Infrastructure/Persistence/` | Contexto por operación, mapeo de las cinco entidades con sus índices y sus restricciones, **sin ninguna columna de pertenencia a instancia**, con modo de diario por delante y escritor único declarados | `BT-05`; `Modelo-Datos-Logico.md` §2 y §3 |
| `…/Persistence/Migrations/` | Transformaciones de esquema, versionadas con el código de su etapa. **Una transformación ya fusionada no se edita** | Intake §17.3.P.7; `ADR-07` |
| `Api/Composition/` | `4 de 4` puertos conectados, `0` sin adaptador, `0` con más de uno, y `1` sola configuración de intercambio declarada en todo el producto — es `QG-10`, y **falla en construcción** cuando falta un puerto | `ADR-06`; `Api/09` `Pipeline-CI-CD.md` §2.1 |
| `Api/Endpoints/HealthEndpoint.cs` | Realiza `A-16`. **Una de las cuatro ausencias declaradas de la guardia** —las otras tres son `A-01`, `A-02` y `A-03`—, y su respuesta no lleva dirección de servicio, ruta del almacén ni traza | `Contratos-REST.md` §3; `US-29` |
| `Web/Components/Pages/Status.razor` | Consume el punto de salud y muestra datos reales. Es lo que hace medible `PT-01.d` | Intake §15; `Web BT-03`; `US-29` §2 |
| `Web/Integration/DataServiceClient.cs` | **La única salida** hacia el servicio de datos. Si aparece una segunda, `RA-01` se queda sin lugar donde verificarse. La dirección base **llega por configuración, nunca embebida** | `Web/05` §3.1 y §3.2 punto 3; `Web ADR-07` |
| `Web/wwwroot/js/` | Destino del bundle. **Artefacto generado, no se edita a mano** | Intake §13 y §16 |
| `visor/src/main.ts` | Fachada externa con las **seis** funciones declaradas. **No contiene lógica de dibujo** | Intake §17.7.P.2 y §17.7.P.3 |
| `visor/webpack.config.js` | Empaqueta con el motor gráfico **dentro del bundle, sin red de distribución externa** (es `PT-03`), y expone la salida **como biblioteca en `window` con un nombre propio, sin globales sueltas** | Intake §17.7.P.1 y §17.7.P.3 |
| `tests/*.Tests/` (3) | Materialización de la estrategia de prueba de cada proyecto. **No son proyectos de código del producto.** En la etapa `a` existen y corren vacíos | Intake §16; `Infrastructure BT-01` |

### 2.3 Apartamientos declarados del árbol del intake §16

**Cuatro, y ninguno cambia una carpeta declarada.** Tres son agregados y uno es una precisión sobre una carpeta que el árbol dibuja.

| # | Apartamiento | Por qué se propone | Riesgo de no hacerlo |
| --- | --- | --- | --- |
| `AP-01` | **Agregar `Directory.Build.props` y `.editorconfig` en la raíz.** No están en el árbol de §16 | Los **seis** proyectos .NET tienen la misma puerta bloqueante: la construcción termina «en 0 y **sin advertencias**» (intake §17.1.P.8, §17.2.P.8, §17.3.P.8, §17.4.P.8, §17.5.P.8; `QG-01` en las cinco canalizaciones que lo declaran). Sin un lugar único que la imponga, la puerta vive repetida en seis archivos de proyecto y se desincroniza | Que un proyecto quede sin la puerta y **nada falle**. Es la forma exacta del riesgo `RI-06` de `Vista-Producto.md` §7 |
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
3. **La arista `Web` → `Api` es de tiempo de ejecución y no introduce ciclo.** La dirección del servicio llega **por configuración**, no por referencia (`Web ADR-07`; manifiesto §3).

**La discrepancia `X-1` no altera este orden.** `Vista-Producto.md` §3.1 lo declara: el grafo es acíclico con siete aristas y con ocho, y **el orden topológico de cuatro niveles es el mismo bajo las dos lecturas**. Lo único que depende de la respuesta es la forma del archivo de proyecto de `Api`.

---

## 4. Los guiones que la etapa necesita

**Siete guiones, los que el intake §16 enumera. Este plan los declara; no los escribe.**

| Guion | Qué hace | Criterio de éxito declarado | Dónde lo declara la especificación |
| --- | --- | --- | --- |
| `build-visor.sh` | **Ciclo corto del visor.** Instalación reproducible de dependencias → empaquetado → **copia al directorio de recursos estáticos del front**. Sólo el bundle, sin encadenar el resto | El bundle **se genera sin errores** (`QG-01` de `Visor`) | Intake §17.7.P.8; `Visor/09` `Pipeline-CI-CD.md` §2 y `Guia-Publicacion-Bundle-Visor.md` |
| `build.sh` | **Construcción del producto.** Encadena la generación del bundle con la compilación de la solución | Termina en **0 y sin advertencias** (`QG-01` en `Domain`, `Contracts`, `Application`, `Infrastructure` y `Api`) | Intake §17.x.P.8 de los cinco; `Visor/09` `Guia-Publicacion-Bundle-Visor.md` §, fila «construcción del producto» |
| `test.sh` | **Batería completa.** Es **el mismo guion** en la máquina de quien construye y en la canalización | La batería pasa entera: **0** rojas y **0** deshabilitadas sin motivo escrito. Es `QG-02` en **cuatro** proyectos —`Domain`, `Application`, `Infrastructure` y `Api`—; `Contracts` corre el mismo guion y su `QG-02` verifica otra cosa | Intake §17.x.P.8; las cuatro `09-Devops/Pipeline-CI-CD.md` §2; `Contracts/09` `Pipeline-CI-CD.md` §, «con `scripts/test.sh`, el mismo guion del producto» |
| `reset-db.sh` | Deja el almacén **en su estado de primer arranque**: vacío, sin ninguna cuenta y sin ningún trabajo, con su esquema al día | El almacén queda vacío y con su esquema al día | Intake §17.3.P.8 (reversión); `Api/03` `Guia-Onboarding-Developer.md` §3.2 |
| `run-api.sh` | Ejecuta el servicio de datos dentro del contenedor de desarrollo | **Arranca, aplica las transformaciones y el punto de salud responde** | `Api/03` `Guia-Onboarding-Developer.md` §3.2 y `DX-Developer-Experience.md`; `Web/10` `ejemplo-01-datos-seed.md` |
| `run-web.sh` | Ejecuta la pieza pública dentro del contenedor de desarrollo | **[PROPUESTA SIN BASE DECLARADA]**: que el front arranque y su página de estado responda | Intake §16, que lo enumera y **no declara su contenido**. Ningún otro documento del corpus lo menciona |
| `migrate.sh` | **Su propósito no está declarado por ninguna fuente.** Ver abajo | — | Intake §16, y **sólo ahí** |

**El caso de `migrate.sh`, que hay que decir y no tapar.** Es el único de los siete que **ninguna fuente del corpus menciona fuera del árbol de §16**, verificado con `grep` sobre `SDD/Docs/` y `SDD/Intake/`. Y hay un motivo por el que su propósito no es obvio: **las transformaciones de esquema se aplican solas al arrancar** (intake §17.3.P.4 y §17.5.P.4; `ADR-07`), de modo que no hay un paso de aplicación manual que un guion tenga que envolver. **Propuesta:** que sea el guion de **generación** de una transformación nueva durante el desarrollo —lo que hace falta para que `reset-db.sh` y `run-api.sh` tengan un esquema que aplicar—, y **no** de aplicación. Va rotulada **[PROPUESTA SIN BASE DECLARADA]** y entra al punto de control.

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
| 2 | La **página de estado** de la pieza pública consume el punto de salud y **muestra datos reales** | Recorrido en el navegador del host, con el servicio de datos corriendo y con el servicio de datos **detenido** —el segundo caso demuestra que el dato es real y no un literal— | `Web BT-03`; `US-29` |
| 3 | `PT-01.a`: la dirección pública responde correctamente | Publicación del front al hosting y comprobación de la dirección pública. **Si no pasa, la salida declarada es bajar la versión objetivo del front y no la del backend** | Flujo de publicación; `V-4` |
| 4 | `PT-01.b`: el transporte de la sesión interactiva está **medido y su resultado documentado, incluido el repliegue si ocurre** | Medición con semáforo. **Sólo el peor resultado obliga a cambiar el modelo de front; un repliegue de transporte no es motivo de rediseño** | `Web BT-04` |
| 5 | `PT-01.c`: **veinte minutos** de navegación continua sin que el proceso recicle la sesión, y **reconexión funcional** al cortar y restablecer la red | Recorrido cronometrado sobre el front publicado. Es el peor escenario y **no tiene mitigación en el código** | `Web BT-04` |
| 6 | `PT-01.d`: una llamada de salud devuelve **datos reales del servidor propio** | La página de estado contra el servicio de datos real, no contra un doble | `US-29`; `Web BT-04` |
| 7 | `PT-04`: la imagen se **construye, arranca, aplica sus actualizaciones de esquema sobre base vacía y responde salud** | `deploy/Dockerfile` construido y arrancado **desde el contenedor de desarrollo**, con el almacén recién creado | `Api BT-04`; `Infrastructure BT-07`; etapa `imagen` de `Api/09` `Pipeline-CI-CD.md` §2.1 |
| 8 | Está verificado que **la sesión interactiva no llega al servicio de datos** | Inspección de red desde el navegador durante el recorrido: **cero** peticiones del navegador hacia el servicio de datos. Es `RA-01` | Intake §17.5.P.3, fila de WebSockets, que lo declara criterio de aceptación de la etapa `a` |

**Los siete comunes** (`Roadmap-Producto.md` §5.1). En la etapa `a` dos de ellos tienen una lectura propia que conviene dejar escrita:

| # | Criterio común | Lectura en la etapa `a` |
| --- | --- | --- |
| 1 | Los guiones de demostración de todas las fases anteriores vuelven a pasar | **No hay fases anteriores.** Se cumple de forma degenerada y se declara en lugar de omitirse |
| 2 | La fase incorporó pruebas automatizadas de las reglas de negocio que introdujo | **La etapa `a` no introduce ninguna regla de negocio** (§6). Lo que sí incorpora son las puertas de `US-24`, `US-25` y `US-26` a `US-29`, con **3** criterios `Given/When/Then` cada una |
| 3 | El informe de cierre está escrito, es autocontenido e indizado | Con las **trece** secciones obligatorias, en `Avances/<orden>-<etapa>.md`, y su índice en `Avances/README.md` |
| 4 | La rama tiene su solicitud de incorporación abierta: **esa solicitud es el punto de control** | Una rama y una solicitud por etapa; etapas en serie |
| 5 | El Product Owner dio **OK explícito** | Es donde se cierran las decisiones de §1 |
| 6 | La rama está incorporada antes de abrir la siguiente | — |
| 7 | Todo guion que involucre el texto de figuras usa datos verificados del intake | **La etapa `a` no involucra ningún texto de figuras.** Se declara y no se rellena |

**Puertas de calidad que además se ejercen en la etapa `a`**, tomadas de las canalizaciones ya especificadas: `QG-01` en cinco proyectos —`Domain`, `Contracts`, `Application`, `Infrastructure` y `Api`— con umbral de cero advertencias, `QG-02` con umbral de batería entera en **cuatro** —los mismos menos `Contracts`, cuyo `QG-02` es otro: **cero** referencias hacia `GeometriaFactory-Domain`—, `QG-10` en `Api` (**4 de 4** puertos conectados, **0** sin adaptador, **1** sola configuración de intercambio) y `QG-11` en `Api` (**0** peticiones atendidas con la preparación del almacén incompleta).

**Una puerta que no pasa detiene la planificación de las etapas que dependen de ella; no se arrastra como deuda.** Lo declara el intake §15 y el roadmap §2.2 lo hereda sin ablandarlo.

---

## 6. Lo que la etapa `a` NO hace

**Es un esqueleto que camina. Esta lista existe para que el punto de control no evalúe la etapa contra un producto que todavía no existe.**

| La etapa `a` **no** … | Dónde vive eso realmente |
| --- | --- |
| **No implementa ninguna regla de negocio.** Ninguna de las **dieciséis** `RN` se ejerce | `Domain BT-06` (entidades) es etapa `c`; `BT-10` (guardas de cuenta) etapa `c`; `BT-12` (máquina de estados) etapa `e` |
| **No hace cumplir ningún invariante.** Los **nueve** `INV` no tienen código en esta etapa | `Domain BT-14`, matriz de ejercicio de los nueve invariantes, etapa `d` |
| **No valida JSON.** El motor de interpretación con las cuatro trampas del formato no existe | `Infrastructure BT-16`, etapa `f` |
| **No verifica valores declarados contra derivados**, ni emite advertencias, ni la tolerancia de 0.01 con operador estricto | `Infrastructure`, etapa `f`; criterios de la transición `f` → `g` |
| **No persiste ningún trabajo.** El esquema de las cinco entidades **existe**; el adaptador que escribe trabajos **no** | `Infrastructure BT-10`, adaptador de repositorio de trabajos, **etapa `e`** |
| **No persiste ninguna cuenta.** El adaptador de repositorio de cuentas y su índice único no existen | `Infrastructure BT-09`, **etapa `c`** |
| **No autentica a nadie.** No hay canje de credenciales, ni acceso firmado, ni derivación de clave construida | `Infrastructure BT-15`, etapa `c`. En la etapa `a` sólo se **ancla** la función de derivación (`BT-03`, decisión `A-3`) |
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
| `R-02` | **Contradicción de orden entre dos backlogs aprobados.** `Infrastructure BT-05` —«construir el contexto de persistencia y **el mapeo de las cinco entidades**»— es de **etapa `a`**, y `Domain BT-06` —«construir el núcleo de entidades con **las cinco entidades del modelo**»— es de **etapa `c`**. No se puede mapear lo que no existe | Es el ítem más pesado de la etapa `a` de `Infrastructure` y de él cuelgan `US-24`, `US-25`, `BT-06`, `BT-07` y `PT-04`. La propuesta de §1.7 —crear los tipos en `a` sin invariantes, llenarlos en `c`— **es propuesta de este plan y no está declarada por ninguna fuente** |
| `R-03` | **El cuerpo de la respuesta de salud no tiene tipo declarado.** `US-29` exige «datos reales del servidor propio» y **no dice cuáles**; `Contratos-REST.md` da a `A-16` los códigos `200` y `503` y ningún tipo; `Contracts` no crea ningún tipo de transferencia en la etapa `a` | Es el criterio 2 y el criterio 6 del cierre. Sin decidir qué son «datos reales», `PT-01.d` se verifica contra un criterio que nadie escribió. **Ninguna fuente permite proponerlo con fundamento** |
| `R-04` | **La ruta del punto de salud no está declarada.** La fuente declara **la existencia** del punto y no su ruta | El `healthcheck` de `compose.yaml`, la página de estado y la comprobación de la publicación del front la necesitan las tres. Es `A-4` / `Api BT-07` |
| `R-05` | **La divergencia `Alumno` / `Cuenta`** de §1.4, que ninguna categoría registró como punto abierto | Es el nombre de un tipo del dominio, de una tabla y del cuarto puerto a la vez |
| `R-06` | **`B-4` sigue abierto en la firma.** El corte de fase con confirmación humana del Product Owner —y con él la aprobación formal del `PRODUCT-INTAKE`— es lo único que un equipo que arranque **no puede cerrar por su cuenta**, y **no le impide construir** | `Handoff-Checkout.md` §6.1 |
| `R-07` | **`PT-01.c` no tiene mitigación en el código.** Si el proceso del hosting recicla la sesión antes de los veinte minutos, no hay nada que escribir para arreglarlo | Roadmap §5.2; intake §17.6.P.12, riesgo `R-06` de la fuente |

---

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.3 | 2026-08-13 | **Tramo `R-2` del plan de renombre de [`Norma-De-Nomenclatura.md`](Norma-De-Nomenclatura.md) 1.4 §8, ejecutado contra el glosario de su §6 y no por criterio propio.** **Acto 1 · el renombre** de los **tres puertos declarados** de su §6.3 —`IRepositorioTrabajos` ⟶ `IWorkRepository`, `IValidadorFiguras` ⟶ `IFigureValidator` e `IRelojDelSistema` ⟶ `ISystemClock`— y los **dos miembros** de su §6.5 —`HashContrasena` ⟶ `PasswordHash` y `JsonOriginal` ⟶ `OriginalJson`—. Acá son **15 ocurrencias**: los tres puertos declarados de §1.5 y su tabla de adaptadores, la alternativa `P-4c` de §1.5, los dos fundamentos 1 y 2 de §1.2, y los **tres nombres de archivo** de `Ports/` en el árbol de §2.1, que §2.3 de la norma cuenta y `V-6` cuadra. **Acto 2 · la corrección del texto que quedaba argumentando por el nombre viejo**, sobre este mismo documento: los **fundamentos 1 y 2 de `P-1a` en §1.2** decían «están en castellano» de identificadores que este tramo acaba de pasar a inglés en la fuente. **Se conservan, no se borran**: el texto refutado queda tachado y el párrafo lleva la marca que `V-7` exige —estado, fecha y sección de la norma que lo supera—, y se declara qué sobrevive de cada fundamento: el prefijo `I` en el primero y la forma `PascalCase` en el segundo, que son forma y no idioma. Los fundamentos 3, 4 y 5 **no se tocan**: son de `R-2b`, `R-4` y `R-3`. **Cuadre `V-4` en las dos direcciones, contra la lista escrita antes de editar:** 64 ocurrencias candidatas medidas en 13 documentos con el instrumento de la norma §2.1, **63 renombradas y 1 no renombrada** —la cita textual de la línea de trazabilidad upstream de `RC-01-Texto-Original-Escrito-Una-Sola-Vez.md`, que atribuye al `PRODUCT-INTAKE` **1.12** las palabras «`JsonOriginal` conservado íntegro y nunca reescrito» y que **renombrar falsificaría**—. `V-6` cuadró los tres nombres de archivo de `Ports/`. **Esta fila queda fuera del cuadre**, por el punto 4 de `V-4`: al describir lo que hizo reintroduce los identificadores viejos. |
| 1.2 | 2026-08-12 | **Tramo `R-1b` de [`Norma-De-Nomenclatura.md`](Norma-De-Nomenclatura.md) 1.2 §8, que salda la deuda del tramo de ensayo `R-1` y es condición de entrada de `R-2`. Son los dos actos de su §8.2.** **Acto 1 · las cuatro superficies derivadas que ninguna regla cubría cuando corrió `R-1` y que §6.11 ahora sí cubre**, renombradas contra sus cuatro filas: la carpeta `Configuraciones/` ⟶ `Configurations/` de §2.1, la carpeta `Paginas/` ⟶ `Pages/` de §2.1 y §2.2, la carpeta `visor/` de la **capa 3** del bundle ⟶ `viewer/` en §2.1 —**no** la raíz `visor/` del proyecto de código, que §1 y §6.11 declaran intocable— y el segmento `Internos` ⟶ `Internal` del contraejemplo de la regla 1 de §1.3. Son **5 ocurrencias**, las que §2.3 de la norma mide. Rige su §6.11: **por debajo del nivel de espacio de nombres el idioma no se afloja**, y la regla de §4 sobre nombres de archivo en castellano alcanza **sólo** a `SDD/` y no a nada bajo `src/`, `tests/` ni `visor/`. **Acto 2 · la corrección del texto que quedaba argumentando por el nombre viejo**, que es la mitad que §8.2 agregó y que `R-1` no hizo, con sus tres barridos sobre este único documento: **§1.2 `D-01` queda declarada superada, no borrada.** La propuesta `P-1a` —identificadores en castellano sin tildes ni eñes— se marca **SUPERADA el 2026-08-12 por la norma §3**, y sus **cinco fundamentos se conservan** con el estado de cada uno: los cuatro primeros siguen siendo ciertos como reporte de sus fuentes y dejan de ser fundamento, con el tramo donde se renombran —`R-2`, `R-2b` y `R-4`—; **el quinto queda refutado por §5.1**, porque el intake §17.7.P.3 declara los nombres de la fachada «a fijar en la etapa que la implementa» y por lo tanto **nunca estuvieron fijados**. `P-1b` —identificadores en inglés— pasa a ser **la alternativa que rige**, con el fundamento nuevo: la traducción vive en **una tabla única**, el glosario de §6, y no en cada punto de la cadena, que es la forma en que `RI-06` de `Vista-Producto.md` §7 pide evitar el defecto. **La consecuencia que §1.2 declaraba inevitable —escribir sin tildes ni eñes— deja de existir** y §3 de la norma la declara beneficio lateral. La misma afirmación refutada en **§1.7** se corrige citando §5.1 y §6.6, con los seis nombres definitivos. **Controles:** `V-4` cuadró contra la lista escrita antes de editar —22 ocurrencias candidatas, 5 renombradas, 17 no renombradas por §4.1: 4 de registro histórico y 13 de otro concepto, reporte de fuente o prosa—; `V-6` cuadró las cuatro superficies; `V-7` cuadró la coherencia interna. **No renombra ninguna otra clase** por la regla 1 de §8: `VisorFiguras.razor` ⟶ `FigureViewer.razor` es de `R-3` y no de este tramo. **No toca las filas de control de cambios**, que son registro histórico (§4.1). |
| 1.1 | 2026-08-12 | **Tramo `R-1` del plan de renombre de [`Norma-De-Nomenclatura.md`](Norma-De-Nomenclatura.md) 1.1 §8, ejecutado contra el glosario de su §6 y no por criterio propio.** Renombra a inglés **los identificadores que este documento propone** y sólo ésos: los **18 espacios de nombres** de §1.3 —16 subsegmentos distintos— por §6.10; los **14 tipos y adaptadores** de §1.6 y §1.7 y los **6 derivados** —los cinco mapeos de `BT-05` y el componente de la página de estado— por §6.4; y los **2 puertos propuestos** de §1.5, `P-4a` y `P-4b`, por §6.3. Alcanza también las **rutas de carpeta** que la regla 2 de §1.3 obliga a hacer coincidir con el subsegmento —`Entities/`, `Values/`, `Ports/`, `Persistence/`, `Composition/`, `Endpoints/`, `Components/`, `Integration/`— y los **nombres de archivo** de §2.1, que la norma §3 fija iguales al tipo que contienen. **Cuadre `V-4` en las dos direcciones: 0 ocurrencias de los nombres viejos y 81 de los nuevos, contra 81 medidas antes de editar.** **No toca ninguna otra clase**, por la regla 1 de §8: las cinco entidades, los tres puertos declarados, los dos miembros, las seis funciones de la fachada y los diez valores de conjunto cerrado siguen en castellano hasta su propio tramo. **No toca la prosa**, ni los identificadores documentales, ni el dato del alumno. **Y no toca `D-01` §1.2**, cuya propuesta `P-1a` —identificadores en castellano— quedó superada por la norma 1.1 §3: corregirla es contenido y no renombre, y se eleva al punto de control. Quedan sin traducir, y se declaran, **tres identificadores sin fila en el glosario**: las carpetas `Configuraciones/` y `Paginas/`, que no son subsegmento de espacio de nombres, y el segmento `Internos` del contraejemplo de la regla 1 de §1.3. |
| 1.0 | 2026-08-12 | **Emisión inicial.** Plan de la etapa `a` para el punto de control del Product Owner, emitido **antes de escribir código**. Propone, sin decidir, las seis decisiones de nombres que `Handoff-Checkout.md` §6.2 registra como `A-1` y `A-2` —idioma y forma de los identificadores, esquema de espacios de nombres, nombre de la entidad de cuenta, nombre del cuarto puerto, criterio de nombrado de adaptadores y tipos centrales del esqueleto—, cada una con su alternativa real y su costo donde la hay. **Declara que el nombre de la solución y los de los siete proyectos NO son un punto abierto**: están declarados en `PRODUCT-INTAKE` §13 y §16 y derivados en `PRODUCT-MANIFEST` **1.3** §1 y §2, con estado `Aprobado`. Fija el árbol de archivos siguiendo el del intake §16, con **cuatro** apartamientos declarados —`Directory.Build.props` y `.editorconfig`, ampliación del `.gitignore`, la página de estado fuera de la línea de base visual, y `visor/dist/` generado y no versionado—. **Verifica y cita** el orden de construcción en los cuatro niveles topológicos de `Pipeline-Producto.md` §2, contrastado contra `PRODUCT-MANIFEST` §3 y `PRODUCT-INTAKE` §13, que coinciden. Declara los **siete** guiones del intake §16 con su criterio de éxito y su fuente, y marca que **`run-web.sh` y `migrate.sh` no tienen contenido declarado por ninguna fuente**. Mapea los **ocho** criterios propios de la transición `a` → `b` y los **siete** comunes contra lo que los demuestra. Enumera **catorce** cosas que la etapa `a` no hace, cada una con la etapa donde sí ocurre. Eleva **siete** riesgos y contradicciones sin resolver, entre ellos la contradicción de orden entre `Infrastructure BT-05` (etapa `a`) y `Domain BT-06` (etapa `c`) y la divergencia `Alumno` / `Cuenta`, **ninguno de los cuales existía como punto abierto registrado**. **No toma ninguna decisión, no reabre ninguna de las 45 ADR emitidas y no modifica ningún otro documento del corpus.** |
