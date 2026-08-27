# PRODUCT-MANIFEST — Fábrica de Geometría

**Plantilla de referencia:** `PRODUCT-MANIFEST-template.md` versión **6.0** (Framework SDD), que es la que declara la procedencia de §1.1

Artefacto **derivado** por el orquestador SDD desde `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §13, según `Intake-Rules.md` §4 y `Master-Prompt.md` §3. No se completa a mano.

---

## §1 Bloque de producto

| Campo | Plano | Valor |
|---|---|---|
| `Nombre-Producto` | negocio | Fábrica de Geometría |
| `Slug-Producto` | documentación | `Fabrica-De-Geometria` |
| `Raiz-Codigo` | código | `GeometriaFactory` |
| `Artefacto-Agrupacion` | código | `GeometriaFactory.sln` |
| Unidad de entrega principal | — | `GeometriaFactory-Api` |
| Intake (origen) | — | `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **3.0** (de su §13.1, §13.2 y §13.3 se deriva este manifiesto) |
| Documento | — | `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` |
| Versión | — | 5.2 |
| Fecha | — | 2026-08-27 |
| Estado | — | **Aprobado** (confirmado por el Product Owner el 2026-08-16). Migraciones 6.0 → 8.6, 8.6 → 8.11, 8.11 → 9.9, 9.9 → 9.10, 9.10 → 9.12, 9.12 → 10.0 y **10.0 → 13.3** cerradas |

`Slug-Producto` es el único campo derivado: se obtiene de `Nombre-Producto` con el algoritmo de `Master-Prompt.md` §3.2 (`Fábrica de Geometría` → `Fabrica-De-Geometria`). `Raiz-Codigo` y `Artefacto-Agrupacion` se leen declarados del intake (cabecera y perfil de convención de §13), no se derivan.

**Independencia de planos verificada** (`Master-Prompt.md` §3.2): `Fabrica-De-Geometria` y `GeometriaFactory` no son la misma cadena salvo puntuación, y `Nombre-Producto` no contiene el separador de segmentos del perfil. La derivación procede.

### §1.1 Procedencia del framework

**Reescrita por la fase M5 de la migración 10.0 → 13.3, el 2026-08-25.** Es la **séptima** migración
normativa de este destino y **la primera que atraviesa tres saltos major**.

**Tres reglas suben major y las tres alcanzan artefactos**: `Root-Rules` **7.0 → 8.4**, que hace
cumplir a la familia `AG` el ancho de cinco dígitos y declara dos ámbitos de unicidad;
`Rules-Backlog-Tecnico` **4.4 → 5.1**, que parte prioridad y estimación en dos ítems; y `Rules-Devops`
**5.0 → 6.1**, que parte otros cuatro. Los tres bloques «Impacto sobre destinos existentes» que
importan son los de la **11.0** y la **12.0**; el de la **13.0** dice *«Ninguno. Ningún destino tiene
trabajo»*.

**Las tres superficies se midieron sobre el árbol y no se supusieron:**

| Superficie | Medida | Desenlace |
|---|---|---|
| La estimación como ítem propio, `Rules-Backlog-Tecnico` §4.4 punto 5.b | **144 archivos `US-*.md`**, los 144 con la sección empaquetada | **144 partidas** en `## 5. Prioridad` y `## 5.b Estimación`. La estimación **no se difiere: se declara «no aplica»**, cerrada **por lectura** — el producto no estima, y `PA-01` quedó cerrado en las **seis** tablas de los dos backlogs |
| Los cuatro ítems `.b` de `Rules-Devops` | **6 documentos** de la 09 en las dos unidades | **10 subsecciones `.b` emitidas.** Tres de los cuatro ítems no tenían trabajo de contenido —las dos mitades ya estaban declaradas—; el cuarto **destapó un ítem vencido**, el generador del inventario, que quedó como `PD-10` con evento en la fase `i` |
| La cita del rol con la forma nueva, `Root-Rules` §4.4 Tabla A | **5 filas** del `SDD/Docs/README.md` | **Migradas**, con el mapeo de catorce que declara la 12.0. **El destino no renumera las 550 ocurrencias del corpus**: `Migracion-Rules.md` §4.3.1 declara que esa renumeración **la hace el framework** |

**Y una consecuencia de esta migración que la procedencia tiene que declarar, porque no es de forma.**
La partición del ítem de estimación destapó que **`PA-01` estaba vencido desde el 2026-08-14** —diferido
al punto de control de la etapa `c`, que cerró sin registrarlo— y que **las 144 historias lo citaban**:
con la forma nueva habría entrado a cada una como hallazgo **P1**. Se cerró **contrastando contra el
hecho y no eligiendo un valor**: `equipo_n = 1`, ninguna capacidad numérica declarada, y **ocho etapas
planificadas, construidas, demostradas y cerradas sin una sola estimación**. La decisión de retirar el
punto es del Product Owner, del 2026-08-25, y está registrada como `D11` en
`Audit/A3-Decisiones-Del-Product-Owner.md` §4.

**Este destino vuelve a ser el que origina la regla que lo alcanza.** El reporte `14` del framework
nació de su prefijo de etiqueta diferido a un punto de control que cerró sin registrarlo; esta
migración deja anotado el **`HM-02`** de `Audit/Plan-Migracion-10.0-a-13.3.md` §5.2, candidato al
reporte `16`: **un ítem cuyo evento todavía no ocurrió pero cuya premisa ya murió** no lo detecta
ninguna comprobación del método.

| Artefacto del framework | Versión |
|---|---|
| Framework SDD (conjunto) | **13.3** |
| `Master-Prompt` | 8.12 |
| `Master-Prompt-Migracion` | 2.8 |
| `Master-Prompt-Reanudacion` | 1.9 |
| `Root-Rules` | **8.4** |
| `Rules-Contexto` | 4.5 |
| `Rules-Necesidades-Negocio` | 4.4 |
| `Rules-Especificacion-Funcional` | 5.5 |
| `Rules-UX-UI-DX` | 5.5 |
| `Rules-Arquitectura-Tecnica` | 4.5 |
| `Rules-Backlog-Tecnico` | **5.1** |
| `Rules-Plan-Sprint` | 5.5 |
| `Rules-Calidad-Y-Pruebas` | 4.6 |
| `Rules-Devops` | **6.1** |
| `Rules-Examples` | 6.5 |
| `Rules-Documentacion` | 5.5 |
| Reglas transversales aplicadas | `Intake-Rules` 4.2, `Vocabulario-Rules` 3.2, `Maqueta-Rules` 4.4 y `Deriva-Rules` 5.4 (Fase B2 confirmada para la unidad de entrega `GeometriaFactory-Web`). **`Migracion-Rules` 3.19 sí aplica**: este árbol atravesó **siete** migraciones normativas —6.0 → 8.6, 8.6 → 8.11, 8.11 → 9.9, 9.9 → 9.10, 9.10 → 9.12, 9.12 → 10.0 y 10.0 → 13.3—, con sus informes en `SDD/Docs/Audit/` |
| `Catalogo-De-Criterios` | 1.13 — **índice, no regla**: no define criterios y no gobierna ningún artefacto de este destino |
| `Rules-Base-Conocimiento` | 2.0 — **entra por primera vez a esta tabla y no alcanza al destino**: no hay orquestador que lea la regla y las **dos** unidades de entrega declaran `usa_llm == false`. Se lista para que su ausencia no se lea como omisión |
| `PRODUCT-INTAKE-template` | **3.5** |
| `PRODUCT-MANIFEST-template` | 6.0 |

`Rules-Prompts-AI` no lleva fila: la categoría 04 queda omitida por gating (`usa_llm` == false en las **dos** unidades de entrega, ver §5).

**Qué cambió respecto de la procedencia anterior.** **Se movieron veintiuno de los veintitrés
artefactos listados**, y `Rules-Base-Conocimiento` entra nuevo. Los dos que no se movieron son
`Master-Prompt-Migracion`, en 2.8, y `PRODUCT-MANIFEST-template`, en 6.0.

**Y una corrección sobre el propio plan, encontrada en la verificación de M5.** La emisión 1.0 del
plan midió `PRODUCT-INTAKE-template` como **«3.4 → 3.4, sin cambio»**, y el árbol lo tiene en **3.5**
desde el framework **13.2**. **No cambió el trabajo** —el bloque de impacto de la 13.2 declara
«Ninguno» y la subsección nueva, `§17.P.13`, es **opcional**— pero la fila era falsa y esta tabla iba
a apoyarse en ella. Corregida en `Plan-Migracion-10.0-a-13.3.md` **1.5**, con los otros **23
artefactos re-verificados uno por uno**.

**Por qué esta procedencia se puede escribir, y por qué el 2026-08-25 todavía no se podía.** No se
actualizó por el número de conjunto: se verificó artefacto por artefacto qué alcanzaba al destino y se
ejecutaron los **tres cortes** de M4 —la 09, el README raíz y la 06—, cada uno con su audit.

**Pero cuando esta tabla se escribió, la cadena no estaba completa, y la frase que decía que sí era
falsa.** El audit de **M6** lo levantó como **P0**: el plan mandaba a M4 escribir el **campo 6 de los
tres ADR de apartamiento** y **M4 nunca los tocó**; la verificación de M5 cerró cuatro de las cinco
filas del plan y declaró que no quedaba ninguna. **La procedencia se apoyó en esa declaración.**

**Quedó completa el mismo día, y se declara en lugar de disimularse.** Los tres ADR pasaron a **1.2**
con sus contadores en **3, 3 y 2**, y los dos `README.md` de la 09 recibieron el archivado que les
faltaba. Recién con eso se cumple la condición de `Migracion-Rules.md` §4.6 — y el orden en que
ocurrió queda escrito, porque **una procedencia que no dice cuándo se volvió cierta no se puede
auditar después**.

#### Decisiones de reconciliación

Sin filas. `SDD/Docs/` estaba vacía al arrancar, de modo que la reconciliación normativa de `Master-Prompt.md` §2.1 no se disparó.

### §1.2 Perfil de convención de nombres

| Parámetro | Valor | Notas |
|---|---|---|
| `Raiz-Codigo` | `GeometriaFactory` | Declarado por el Product Owner (intake §13, cabecera) |
| Separador de segmentos | `.` | Convención de espacios de nombres de .NET |
| Prefijo de paquetes redistribuibles | `Aplicada` | Sin uso: ningún proyecto de código es `redistribuible` |
| Extensión del agrupador | `.sln` | Compone `GeometriaFactory.sln` |

---

## §2 Los dos ejes del producto

El producto tiene dos ejes que no coinciden: el de **entrega**, que dice qué se despliega, y el de
**construcción**, que dice qué se compila. Se derivan de §13.1, §13.2 y §13.3 del intake **3.0**, que es el vigente y el que §1 declara como origen. **La cita decía 2.0 y era falsa**: la levantó el audit de M6 como **P2**, en el artefacto que declara la conformidad de todo lo demás.

### §2.A Tabla de unidades de entrega

| `Nombre-Unidad-Entrega` | `tipo_unidad_entrega` (D8) | Rol en el producto | `redistribuible` | Integra con (runtime) | Estado | Path de documentación |
|---|---|---|---|---|---|---|
| `GeometriaFactory-Api` (principal) | `rest-api` | Host REST desplegado en el servidor propio: endpoints, autenticación JWT y aplicación de migraciones al arrancar. Sostiene el dato, las reglas y la única base de datos | false | — | vigente | `SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/` |
| `GeometriaFactory-Web` | `web-monolith` | Front Blazor Interactive Server con MudBlazor, en el hosting público. Único punto de contacto del navegador | false | `GeometriaFactory-Api`, por HTTP con `Bearer` JWT, servidor a servidor | vigente | `SDD/Docs/Unidades-Entrega/GeometriaFactory-Web/` |

**Dos unidades de entrega, y la partición responde a la topología y no a una preferencia de estilo**
(intake §14): el front vive donde no lo bloquean y los datos viven donde persisten.

### §2.B Tabla de proyectos de código

**Solución de código `GeometriaFactory.sln`** — seis proyectos, un solo comando de construcción:

| `Nombre-Proyecto-Codigo` | `Identidad-Codigo` | Stack | Rol en la arquitectura | Dependencias de compilación | Path `/src` |
|---|---|---|---|---|---|
| `GeometriaFactory-Api` | `GeometriaFactory.Api` | ASP.NET Core sobre .NET 10 | Host REST: endpoints, autenticación y composición de raíz | `GeometriaFactory-Application`, `GeometriaFactory-Infrastructure`, `GeometriaFactory-Contracts` | `src/GeometriaFactory.Api/` |
| `GeometriaFactory-Web` | `GeometriaFactory.Web` | Blazor Interactive Server sobre .NET 10, con MudBlazor | Front: páginas y componentes. Hoja del grafo | `GeometriaFactory-Contracts`, `GeometriaFactory-Visor` | `src/GeometriaFactory.Web/` |
| `GeometriaFactory-Domain` | `GeometriaFactory.Domain` | C# sobre .NET 10 | Entidades e invariantes. Centro de la regla de dependencias | — | `src/GeometriaFactory.Domain/` |
| `GeometriaFactory-Application` | `GeometriaFactory.Application` | C# sobre .NET 10 | Casos de uso y puertos | `GeometriaFactory-Domain` | `src/GeometriaFactory.Application/` |
| `GeometriaFactory-Infrastructure` | `GeometriaFactory.Infrastructure` | C# sobre .NET 10, EF Core con proveedor SQLite | Persistencia, seguridad y validador de figuras | `GeometriaFactory-Application`, `GeometriaFactory-Domain` | `src/GeometriaFactory.Infrastructure/` |
| `GeometriaFactory-Contracts` | `GeometriaFactory.Contracts` | C# sobre .NET 10 | DTOs de la API. **Proyecto compartido** | — | `src/GeometriaFactory.Contracts/` |

**Sin solución de código** — un proyecto Node.js independiente:

| `Nombre-Proyecto-Codigo` | `Identidad-Codigo` | Stack | Rol en la arquitectura | Dependencias de compilación | Path |
|---|---|---|---|---|---|
| `GeometriaFactory-Visor` | `geometriafactory-visor` | Node.js con TypeScript y webpack | Produce el bundle del visor 3D. Visualizador puro (RA-02) | — | `visor/` |

**Ningún proyecto de código lleva valor D8**: el tipo es atributo de la entrega, no de la compilación.
Los cinco que la emisión 1.4 declaraba `library` no perdieron rol: perdieron un atributo que el modelo
de dos ejes le asigna a la unidad que los contiene.

**El visor no pertenece a `GeometriaFactory.sln`**, y por eso su grafo de compilación es propio: su
artefacto llega a `GeometriaFactory-Web` como **bundle copiado a `wwwroot/js/`**, no como referencia
de proyecto. Es un consumo de artefacto entre soluciones, no una arista del grafo de la solución .NET.

### §2.C Matriz de composición

Derivada de la columna «Compone» de §13.2 del intake:

| | Api | Web | Domain | Application | Infrastructure | **Contracts** | Visor |
|---|---|---|---|---|---|---|---|
| **GeometriaFactory-Api** | X | | X | X | X | **X** | |
| **GeometriaFactory-Web** | | X | | | | **X** | X |

**`GeometriaFactory-Contracts` es el único proyecto compartido.** Un cambio sobre él **alcanza a las
dos entregas**, y por eso viaja marcado en el despacho de las dos.

### §2.1 Regla de nombres de código

Cada proyecto de código se nombra `<Raiz-Codigo>.<Sufijo>`. Ningún proyecto es `redistribuible`, de
modo que el prefijo de organización del perfil no se usa. `GeometriaFactory-Visor` es la **excepción
declarada** del intake §13.3: es un paquete Node.js y su identidad sigue la convención de
`package.json`, minúscula con guiones.

---

## §3 Los dos grafos

**Grafo de integración**, derivado de la columna «Integra con» de §2.A. Ordena la **generación de la
documentación**:

```text
[GeometriaFactory-Api]  ->  [GeometriaFactory-Web]
```

Orden topológico de generación: nivel 0 `GeometriaFactory-Api`; nivel 1 `GeometriaFactory-Web`. La
arista es de **runtime**: el front habla con la API por HTTP.

**Grafo de compilación de `GeometriaFactory.sln`**, derivado de la columna de dependencias de §2.B.
Acíclico. Ordena el **build**:

```text
[Domain, Contracts]  ->  [Application]  ->  [Infrastructure]  ->  [Api]
[Contracts]  ->  [Web]
```

Orden topológico de compilación: nivel 0 `GeometriaFactory-Domain` y `GeometriaFactory-Contracts`;
nivel 1 `GeometriaFactory-Application` y `GeometriaFactory-Web`; nivel 2
`GeometriaFactory-Infrastructure`; nivel 3 `GeometriaFactory-Api`.

**Grafo de compilación del proyecto Node.js:** un solo nodo, `GeometriaFactory-Visor`, sin
dependencias de compilación. Su salida se consume como artefacto.

**Los dos grafos no coinciden, y es lo que hay que no confundir.** La arista `Web → Api` está en el de
integración y **no** en el de compilación; las cuatro aristas internas del backend están en el de
compilación y **no** en el de integración, porque esas capas no se despliegan.

---

## §4 Resultado de las validaciones bloqueantes

| Validación | Resultado |
|---|---|
| Cada `tipo_unidad_entrega` pertenece al conjunto cerrado D8 | Cumple: `rest-api` (1) y `web-monolith` (1), sobre las **dos** unidades de entrega |
| Exactamente una unidad de entrega principal | Cumple: `GeometriaFactory-Api` |
| Sin colisión de `Nombre-Proyecto-Codigo` ni de `Identidad-Codigo` | Cumple: siete nombres y siete identidades distintas |
| Cada dependencia de compilación referencia un proyecto de código existente | Cumple: las siete aristas resuelven |
| Grafo de compilación acíclico, por solución de código | Cumple: cuatro niveles en `GeometriaFactory.sln`; un solo nodo en el proyecto Node |
| Todo proyecto de código compone al menos una unidad de entrega | Cumple: los siete tienen marca en §2.C |
| `Nombre-Producto` en prosa de negocio, independiente de `Raiz-Codigo` | Cumple |
| `Raiz-Codigo` declarado en el intake | Cumple: declarado, no asumido |
| §13 del intake recorrible, con sus tres subsecciones y su perfil de convención | Cumple |

---

## §5 Flags derivados (`Master-Prompt.md` §4)

**Los flags se calculan por unidad de entrega, no por proyecto de código.** Es el cambio de fondo
respecto de la emisión 1.4: un proyecto de código **no emite categorías**, de modo que no tiene flags
de gating. Su stack y sus dependencias son datos del inventario de §2.B.

Flags del producto:

| Flag | Valor | Origen |
|---|---|---|
| `equipo_n` | 1 | Intake §2: «1 docente + agente IA»; el agente no es persona del equipo. Efecto: la categoría 07 emite únicamente `Mini-Plan.md` |
| `requiere_compliance` | false | Intake §10 declara que no aplica ninguna normativa |

Flags por unidad de entrega:

| Unidad de entrega | `tipo_unidad_entrega` | `redistribuible` | `entrega_diferida` | `usa_llm` | `tiene_ui_final` | `multi_tenant` | `tiene_auth` | `tiene_portal_developers` | `tiene_extensibilidad` | `tiene_persistencia` | `tiene_observabilidad_critica` | `requiere_maqueta` |
|---|---|---|---|---|---|---|---|---|---|---|---|---|
| `GeometriaFactory-Api` | `rest-api` | false | false | false | false (DX) | false | **true** | false | false | **true** | **true** | false |
| `GeometriaFactory-Web` | `web-monolith` | false | false | false | **true** (UX/UI) | false | **true** | false | **true** | false | false | **true** |

Fundamento de los valores que no son el trivial, y **qué cambió al pasar de siete filas a dos**:

- **`tiene_persistencia` true en `Api`, y es el caso que la evaluación por proyecto de código rompía.** La persistencia del producto vive en `GeometriaFactory-Infrastructure`, que **no se despliega**. Evaluado por proyecto de código, la entrega `Api` habría quedado en false y se habría omitido su modelo de datos. La regla vigente lo evalúa sobre la unidad —«true si **alguno de los proyectos de código que la componen** declara un motor de persistencia distinto a *No aplica*»— y el resultado es el correcto. `Web` queda en false: el intake §14 declara que «el front no tiene base de datos».
- **`tiene_auth` true en las dos.** En `Api` por la derivación de clave y la emisión de JWT de `Infrastructure`, la guardia y el canje del host, las reglas de `Domain` (INV-06) y la autorización por pertenencia de `Application` (INV-02, INV-03) —los cuatro proyectos que la componen la declaran—. En `Web` por el canje de credenciales, el acceso en el circuito y la cookie de sesión. La emisión 1.4 lo tenía true en cinco de sus siete filas; acá **no se pierde nada**: las dos entregas emiten su ADR de autenticación.
- **`tiene_extensibilidad` pasa de `Visor` a `Web`.** El punto de extensión del producto es la fachada del visor, con sus **seis** funciones (intake §17.2.P.3 · `GeometriaFactory-Visor`), y `GeometriaFactory-Visor` compone `GeometriaFactory-Web`. El flag viaja con la entrega que lo publica: `Extensibilidad.md` en 05 y la guía de testing en 08 se emiten en `Web`, que es donde el contrato es observable.
- **`requiere_maqueta` pasa de dos proyectos de código a una unidad de entrega, y refleja lo que efectivamente pasó.** La emisión 1.4 lo tenía true en `Web` y en `Visor`, y su propio fundamento declaraba que **hubo una sola maqueta**, la de `Web`, porque «la fachada no dibuja superficie propia y lo único observable de ella es la escena embebida en su anfitrión». El modelo de unidad de entrega hace que eso deje de ser una excepción declarada y pase a ser la derivación: **una entrega, una maqueta**. Los tres artefactos de línea de base ya viven en la categoría 03 de `Web`, y no se mueven.
- **`tiene_observabilidad_critica` true en `Api`**: sus NFR declaran latencia p99 con métrica numérica (500 ms). Ninguna otra entrega declara p99 ni SLO de disponibilidad.
- **`usa_llm` false en las dos**: ninguna subsección P.10 ni P.11 de ninguna de las dos declara uso de LLM. El agente IA interviene en la construcción, no en el sistema construido. **La categoría 04 se omite para las dos.**
- **`multi_tenant` false**: intake §17.1.P.4 · `GeometriaFactory-Infrastructure` lo declara explícitamente —«una instancia, un curso, un administrador», INV-05—.
- **`tiene_ui_final`**: se deriva de `tipo_unidad_entrega`. `web-monolith` → true, variante UX/UI. `rest-api` sin portal → false, variante DX.
- **`redistribuible` false en las dos**: no hay publicación en ningún feed. Los dos artefactos entregables son una imagen Docker y una publicación por FTP.
- **`entrega_diferida` false en las dos**: ninguna fuente declara una entrega planificada para otra etapa, y las dos tienen sus once categorías emitidas (decisión de la batería M2, B-1).

**Los flags quedan inmutables desde la confirmación de esta emisión** (`Master-Prompt.md` §4). Un
cambio posterior obliga a retroceder a la fase más temprana afectada de la unidad de entrega
correspondiente.

---

## §6 Control de cambios

| Versión | Fecha | Cambios | Autor |
|---|---|---|---|
| 5.2 | 2026-08-27 | **Parche `P-04` de la mesa de evaluación del 2026-08-27** ([`../Docs/Audit/Mesa-2026-08-27.md`](../Docs/Audit/Mesa-2026-08-27.md), hallazgo **H-04**, ancla **E2**, nivel **P2**). La línea 3 declaraba «plantilla de referencia **5.0**» mientras **§1.1 de este mismo documento** declara la **6.0**: dos lecturas del mismo dato dentro del mismo archivo, y la de arriba es la que un lector ve primero. Se corrige la línea 3 y se la remite a §1.1, que es la fuente. **La procedencia de §1.1 no se toca** y sigue en SDD **13.3**. | Orquestador de reanudación SDD |
| 5.1 | 2026-08-25 | **Cierra los tres `P0` del audit de M6, que había RECHAZADO la migración.** La emisión 5.0 declaró la cadena completa cuando no lo estaba: **la revisión de apartamientos de §4.7 nunca se ejecutó sobre el árbol** —los tres ADR seguían con sus contadores en 1, 1 y 0, del 17 y 18 de agosto— y **dos `README.md` de la 09 habían subido de versión sin archivar su estado previo**. §1.1 deja de afirmar que ninguna fila del plan quedó sin resolver y **declara el orden real**: qué faltaba, cuándo se completó y con qué. **Y se corrige la cita del intake de §2**, que decía **2.0** con el vigente en **3.0** (**P2** del mismo audit), en el artefacto que declara la conformidad de todo lo demás. Sube **minor**: no cambia ninguna versión de la tabla de procedencia, corrige lo que el documento afirmaba sobre sí mismo. |
| 5.0 | 2026-08-25 | **Fase M5 de la séptima migración normativa, 10.0 → 13.3, y la primera que atraviesa tres saltos major.** §1.1 se reescribe con los **23 artefactos** en su versión viva, más `Rules-Base-Conocimiento` **2.0**, que entra por primera vez a la tabla **declarando que no alcanza** —no hay orquestador que lea la regla y las dos unidades tienen `usa_llm == false`—, para que su ausencia no se lea como omisión. **Las tres superficies del salto, medidas sobre el árbol**: **144** historias de usuario con su §5.b, **10** subsecciones `.b` en los seis documentos de la 09, y **5** filas del mapa de documentación con la forma nueva de `AG`. Se declara que **el destino no renumera las 550 ocurrencias del corpus**, por `Migracion-Rules.md` §4.3.1. **Y la consecuencia que no es de forma**: la partición del ítem de estimación destapó que `PA-01` estaba **vencido desde el 2026-08-14** y que **las 144 historias lo citaban** —habrían sido 144 hallazgos `P1`—; se cerró **contrastando contra el hecho**, con ocho etapas cerradas sin una sola estimación, y el retiro quedó registrado como `D11`. **La procedencia se escribe porque la cadena está completa**: tres cortes de M4, cada uno con su audit independiente, y **ninguna fila del plan sin resolver**; reescribirla con migración parcial es **P0** de M6. Estado previo archivado en `_legacy/2026-08-25/`. Sube **major**. |
| 4.0 | 2026-08-19 | **Migración normativa 9.12 → 10.0, fase M5**: cierre de procedencia. **Sexta** migración del destino y **la primera desde la 8.11 → 9.9 que alcanza artefactos**, después de dos saltos consecutivos de alcance cero. §1.1 se reescribe entera: el conjunto pasa de **9.12** a **10.0**, con `Root-Rules` **6.2 → 7.0** y `Rules-Devops` **4.6 → 5.0** como los **dos únicos** artefactos que alcanzan al árbol; los siete saltos minor 9.13 a 9.19 tienen **alcance documental cero**, verificado antes en la reanudación del 2026-08-18 y de nuevo en el plan. **Las tres superficies se midieron y no se supusieron**: las citas a §12 dan **cero**, el prefijo de tag es **un documento** —`Estrategia-Versionado.md` **3.0** §3.b— y los ítems diferidos son **116 filas** en seis documentos. **Se escribe recién después de verificar que M4 quedó completa**: ninguna tabla conserva la columna «Cuándo» y las 116 filas llevan su evento como artefacto y sección. §1.1 declara además el resultado que la migración destapó y que no es un detalle de forma: **76 puntos abiertos vencidos** —32 desde el 2026-08-13— y **14 sin evento declarado, elevados al Product Owner**; ninguno se cerró y ninguno se inventó. **M2 y M3 quedaron sin filas**: el intake no declara puntos abiertos con esta forma ni cita §12, de modo que el manifiesto no se re-deriva. Estado anterior archivado en `_legacy/2026-08-19/`. Sube **major**: el bloque de procedencia se reescribe entero. | Orquestador de migración normativa SDD |
| 3.2 | 2026-08-17 | **Migración normativa 9.10 → 9.12, fase M5**: cierre de procedencia. Quinta migración del destino y **segunda consecutiva de alcance documental cero**. **Dieciocho artefactos del framework se movieron y ninguno alcanza al árbol**: la 9.11 agregó a cada tabla de anti-patrones una columna **`Detección`**, y la verificación **mecánica** —`diff` del snapshot `_legacy/9.10/` contra los archivos vivos— da **cero líneas cambiadas fuera de esa tabla en las quince reglas**. Entra `Catalogo-De-Criterios` **1.1**, artefacto nuevo del framework, **con su naturaleza de índice declarada**: no define ningún criterio y no gobierna ningún artefacto del destino; se lista para que la próxima migración no redescubra la pregunta. **Corrección propia:** la fila de reglas transversales de la emisión **3.1** decía «este árbol atravesó **tres** migraciones normativas» y enumeraba **cuatro** —el recuento no se actualizó al agregar el cuarto salto, error escrito por este mismo orquestador—; pasa a **cinco**, enumeradas, y queda declarado en `Audit/Plan-Migracion-9.10-a-9.12.md` §6 en lugar de corregirse en silencio. **Ningún documento del árbol se tocó**: M2, M3 y M4 quedaron sin filas. Sube **minor**. |
| 3.1 | 2026-08-17 | **Migración normativa 9.9 → 9.10, fase M5**: cierre de procedencia. Es la **cuarta** migración de este destino y la primera con **alcance documental cero**. **Veintiuno de los veintidós artefactos del framework no cambiaron de versión**; el único que se movió es `Migracion-Rules` **3.7 → 3.8**, cuyas cinco reglas nuevas gobiernan **la consolidación al fundir árboles**: cambian cómo se migra, no la forma de ningún documento migrado. **Verificado sobre el árbol y no supuesto**: cero carpetas `_fusion/`, ninguna fila del salto anterior clasificada «regenerar», y la única consolidación del producto cerrada y auditada bajo su propia normativa. **Cero renombres**, comprobados por lectura de la entrada completa del `CHANGELOG`. La verificación artefacto por artefacto quedó escrita en `Audit/Plan-Migracion-9.9-a-9.10.md` §2 y §3 **antes** de tocar esta tabla, que es lo que `Master-Prompt-Reanudacion.md` §4 exige para actualizar la procedencia sin migrar documentos, y lo que la fase M5 existe para hacer cumplir. **Ningún documento del árbol se tocó**: las fases M2, M3 y M4 quedaron sin filas. Sube **minor**: cambia una fila de la tabla de procedencia y su nota, no el bloque entero. |
| 3.0 | 2026-08-17 | **Migración normativa 8.11 → 9.9, fase M5** (`Master-Prompt-Migracion.md` 2.7 §9): **cierre de procedencia**. §1.1 pasa del conjunto **8.11** al **9.9**, con las versiones leídas del snapshot **`_legacy/9.9/`** del repositorio del framework y no de sus archivos vivos — el framework publicó **9.10** durante la corrida y el plan había congelado el objetivo en 9.9 declarando que no se reabre. **Se escribió recién después de verificar que las siete filas del plan quedaron resueltas**: intake 3.0 (M2), este manifiesto re-derivado 2.3 (M3), los dos apartamientos con su estado y su contador, y las once rutas del layout anterior en cuatro documentos de nivel producto (M4). Ninguna sección quedó pendiente por falta de fuente y ninguna fila sin resolver: reescribir la procedencia con migración parcial es **P0** por `Migracion-Rules.md` §4.6. **A diferencia del salto anterior, éste sí alcanzó artefactos**: cambiaron los veintidós artefactos del framework, aunque la mayor parte de su superficie —la cabecera de nivel de los 313 documentos generados y el renombre del artefacto de `05`— ya estaba reparada por la salida A del orquestador de reanudación, el mismo día y antes de que la migración empezara. §1.1 declara las tres superficies que sí abrió el salto y cómo se cerró cada una. Sube **major**: el bloque de procedencia se reescribe entero. |
| 2.3 | 2026-08-17 | **Migración normativa 8.11 → 9.9, fase M3** (`Master-Prompt-Migracion.md` 2.7 §7): el manifiesto **se re-deriva** del intake migrado **3.0**, no se migra. **La re-derivación no produjo ningún cambio de contenido**, y eso es el resultado y no la ausencia de uno: la M2 corrigió metadatos del intake —el árbol de §16, el encabezado de la Parte C y el checklist de §19— y **no tocó §13**, que es lo único de lo que este documento se deriva. Se verificó campo por campo contra §13.1, §13.2 y §13.3: las **dos** unidades de entrega con su D8 y su `redistribuible`, los **siete** proyectos de código con su solución, su stack y sus dependencias, la matriz con `GeometriaFactory-Contracts` como único compartido, los dos grafos y las **nueve** validaciones bloqueantes, todas **Cumple**. Lo único que cambia es la **referencia al intake de origen**, que citaba la emisión **2.0** —desactualizada ya antes de esta migración, porque el intake iba por 2.2— y ahora cita **3.0**. **La procedencia de §1.1 NO se toca y sigue declarando el conjunto 8.11**: reescribirla es trabajo de la fase **M5**, y adelantarlo produciría una afirmación falsa mientras `SDD/Docs/` sigue sin migrar. El estado anterior queda archivado en `_legacy/2026-08-17/`. Sube **minor**: el artefacto derivado no cambia de forma ni de contenido. |
| 2.2 | 2026-08-16 | **Cierre de procedencia por la fase M5 de la migración 8.6 → 8.11** (`Master-Prompt-Migracion.md` **2.3** §9), invocada desde la salida **B** del orquestador de reanudación con la decisión del Product Owner del 2026-08-16. **§1.1 se reescribe**: el conjunto pasa de **8.6 a 8.11**. **Ninguna de las catorce reglas de categoría ni de las cuatro transversales cambió de versión**, de modo que **ningún documento del árbol requirió migración**: las 450 filas del plan se clasificaron `no tocar` y las 450 quedaron resueltas. Cambian cinco filas de instrumento de proceso —`Master-Prompt` 7.4 → 7.7, `Master-Prompt-Migracion` 2.0 → 2.3, `Migracion-Rules` 3.2 → 3.4, `PRODUCT-INTAKE-template` 3.0 → 3.1— y entra **una fila nueva**, `Master-Prompt-Reanudacion` 1.1, que en la 8.6 no existía. **La verificación es artefacto por artefacto y no por número de conjunto**, escrita antes de tocar la tabla en `SDD/Docs/Audit/Plan-Migracion-8.6-a-8.11.md` §2, §3 y §5; el único cambio con superficie sobre un artefacto —la plantilla de intake 3.1— encuentra al intake **ya conforme**, porque el defecto que corrige se descubrió migrando este mismo destino. **M2 sin cambios, M3 sin corresponder, M4 sin trabajo.** El intake **no se tocó** y sigue en **2.0**, de modo que ninguna tabla de §2 a §5 de este manifiesto cambia: M5 sólo toca la procedencia. Sin archivado previo, por la misma razón y con el mismo criterio que la fila 2.1. Sube minor. | Orquestador de migración normativa SDD |
| 2.1 | 2026-08-16 | **Cierre de procedencia por la fase M5 de la migración** (`Master-Prompt-Migracion.md` 2.0 §9), después de que el Product Owner confirmara la re-derivación de M3. La verificación de M5 encontró **la cadena completa**: intake **2.0** sobre plantilla 3.0, manifiesto **2.0** sobre plantilla 5.0, `SDD/Docs/` migrado, **0 filas del plan sin resolver**, **0 secciones pendientes sin respuesta del humano**, **0 enlaces rotos** y **1145 de 1145 referencias ancladas**. **Con dos conformidades de forma abiertas y declaradas**, que el informe de M6 levanta como P1 y que **no son documentos sin migrar**: dos documentos de referencia cruzada no se reconectaron en M4, y las familias de identificadores del propio intake conservan su ancho de origen. **§1.1 se reescribe**: el conjunto pasa de **6.0 a 8.6**, con las catorce reglas de categoría y las cuatro transversales en su versión vigente, y **dos filas nuevas** —`Master-Prompt-Migracion` 2.0, que en la 6.0 no existía, y `Migracion-Rules` 3.2, que **pasa de «no aplica» a aplicar**, porque este destino ya no es un árbol generado desde cero sino uno migrado—. El estado pasa a **Aprobado**. Ninguna tabla de §2 a §5 cambia: M5 sólo toca la procedencia. Sube minor. | Orquestador SDD |
| 2.0 | 2026-08-16 | **Re-derivación por la fase M3 de la migración 6.0 → 8.6** (`Master-Prompt-Migracion.md` 2.0 §7), desde el intake **2.0** y sobre la plantilla **5.0**. El manifiesto pasa de una tabla de siete proyectos de código a **los dos ejes**: **§2.A** las dos unidades de entrega con su D8, su integración en runtime y su estado; **§2.B** los siete proyectos de código **agrupados por solución de código** —seis en `GeometriaFactory.sln` y `GeometriaFactory-Visor` sin solución, como proyecto Node independiente—, **sin valor D8**; y **§2.C** la matriz, que declara a `GeometriaFactory-Contracts` como **único proyecto compartido**. **§3** declara **dos grafos** en lugar de uno: el de integración, que ordena la generación, y el de compilación **por solución**, que ordena el build; la arista `Web → Api` vive en el primero y no en el segundo. **§4** valida sobre las unidades de entrega y suma la validación de que todo proyecto de código componga al menos una. **§5** pasa de **siete filas de flags a dos**, porque un proyecto de código no emite categorías y no tiene gating: con eso `tiene_persistencia` de `Api` pasa a **true** —evaluado por proyecto de código habría quedado en false y se habría omitido su modelo de datos—, `tiene_extensibilidad` viaja de `Visor` a `Web`, y `requiere_maqueta` deja de ser una excepción declarada y pasa a ser la derivación. **El bloque de procedencia de §1.1 no se toca y sigue declarando 6.0**: es trabajo de M5 y sólo procede con la cadena completa. **Estado `Propuesto`**: espera confirmación explícita del Product Owner. Sube **major**. | Orquestador SDD |
| 1.4 | 2026-08-13 | **Tramo `R-2` del plan de renombre de [`Norma-De-Nomenclatura.md`](../Docs/Producto/Norma-De-Nomenclatura.md) 1.4 §8, ejecutado contra el glosario de su §6 y no por criterio propio.** **Acto 1 · el renombre** de los **tres puertos declarados** de su §6.3 —`IRepositorioTrabajos` ⟶ `IWorkRepository`, `IValidadorFiguras` ⟶ `IFigureValidator` e `IRelojDelSistema` ⟶ `ISystemClock`—. Acá son **3 ocurrencias**, las de la columna «qué expone» de `GeometriaFactory-Application` en §2, **derivadas del intake §13**, que se renombró en la misma edición: si la derivación no se moviera con su fuente quedaría reportando algo que §13 ya no dice. **Ningún flag, ningún tipo, ninguna arista del grafo y ningún orden topológico cambian.** **Cuadre `V-4` en las dos direcciones, contra la lista escrita antes de editar:** 64 ocurrencias candidatas medidas en 13 documentos con el instrumento de la norma §2.1, **63 renombradas y 1 no renombrada** —la cita textual de la línea de trazabilidad upstream de `RC-04001-Texto-Original-Escrito-Una-Sola-Vez.md`, que atribuye al `PRODUCT-INTAKE` **1.12** las palabras «`JsonOriginal` conservado íntegro y nunca reescrito» y que **renombrar falsificaría**—. `V-6` cuadró los tres nombres de archivo de `Ports/`. **Esta fila queda fuera del cuadre**, por el punto 4 de `V-4`: al describir lo que hizo reintroduce los identificadores viejos. | Orquestador SDD |
| 1.3 | 2026-08-11 | **Cierra el hallazgo `P1-2`** del informe de auditoría `SDD/Docs/Audit/G-10-Examples-Siete-Proyectos-r1.md` 1.0. La línea de §5 sobre el punto de extensión afirmaba que «la enumeración de §18 del intake sigue nombrando cinco» funciones de la fachada y la trataba como residuo pendiente de corrección por el Product Owner. **La afirmación era falsa**: §18 del `PRODUCT-INTAKE` **1.25**, abierto y leído, enumera las **seis** por nombre y las rotula «las seis que §17.7 P.3 declara desde 1.6»; la corrección venía de la versión 1.11 del intake y este manifiesto no la registró. El defecto no quedó acá: **cuatro documentos de la Fase G tomaron la afirmación de esta línea en vez de abrir §18** y la repitieron como viva. Se reescribe la línea con el texto de §18 citado desde la fuente y se deja constancia expresa de la corrección, para que la próxima fase no la vuelva a copiar. **Ningún flag, ningún tipo D8 y ningún proyecto de código del manifiesto cambia**: la corrección es de una afirmación sobre otra sección del intake. | Product Owner |
| 1.2 | 2026-08-10 | **Cierra los hallazgos `F26-21` y `N-2`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r2.md` 1.0, que registró que este documento fue el único del corpus al que la propagación de F-26 no llegó. Revisado entero contra el `PRODUCT-INTAKE` **1.10** y contra los documentos vivos de los cinco proyectos de código con documentación emitida. **(a) El punto de extensión declara seis funciones y no cinco.** El fundamento de `tiene_extensibilidad` en `GeometriaFactory-Visor` enumeraba `inicializar`, `cargarJson`, `seleccionarPieza`, `redimensionar` y `destruir`, la enumeración de §18 del intake, que es anterior a que su §17.7 P.3 sumara **`establecerMovimiento(id, opciones)`** en la versión **1.6**. Se pasa a citar §17.7 P.3, que es la sección que lleva el contrato, se declara que la enumeración de §18 quedó como residuo de la fuente, y se ancla en `Visor` `CU-12006` —CA-01 y CA-06— que el sample S-1 ejerce las seis. Se deja escrito que la sexta función confirma **RA-02** en lugar de aflojarla: el anfitrión pasa dos valores de verdad y el bundle no consulta la preferencia de movimiento reducido ni conserva la elección. **(b) El fundamento de `requiere_maqueta` describía una Fase B2 que no ocurrió así.** Decía que los dos proyectos de código la ejecutan «cada uno con su maqueta propia», contra `GeometriaFactory-Visor/03-UX-UI-DX/README.md` §4, que declara desde el 2026-08-09 que **no tuvo maqueta propia por decisión del Product Owner** y que su validación se integró en la maqueta de `GeometriaFactory-Web`. **Ningún flag cambia de valor**: `requiere_maqueta` sigue true en los dos y la inmutabilidad de `Master-Prompt.md` §4 no se toca; lo que se corrige es la descripción de cómo se ejerció y dónde quedaron los tres artefactos de línea de base. **Nada más quedó desactualizado**: se recontaron los siete proyectos de código de §2, las siete aristas de §3, las ocho validaciones bloqueantes de §4 y los flags de §5 contra el intake 1.10, y F-26 —que es lo que la tanda propagaba— no altera composición, jerarquía ni ningún flag, porque entra como capacidad dentro de proyectos de código ya declarados. Sube minor: corrige dos fundamentos derivados sin cambiar composición, jerarquía ni valores de flag. | Orquestador SDD |
| 1.1 | 2026-08-09 | Corrección de `tiene_auth` en `GeometriaFactory-Domain` y `GeometriaFactory-Application`, de false a **true**, alineando el manifiesto a la sección de trazabilidad del `PRODUCT-INTAKE`, que es la fuente y declara true en los dos. La derivación original de §5 leía sólo el P.5 de cada bloque técnico. Detectada al emitir la categoría 03 de `GeometriaFactory-Domain`, que documentó la frontera de autenticación y encontró la divergencia contra el manifiesto. **No dispara el retroceso de fase de `Master-Prompt.md` §4**: el efecto del flag es habilitar casos de uso de autenticación en 02 y un ADR en 05; `Domain` ya había emitido esos casos de uso por otra vía y `Application` todavía no arrancó, de modo que ninguna fase generada queda inválida. Sube minor: corrige un valor derivado sin cambiar la composición ni la jerarquía. | Orquestador SDD |
| 1.0 | 2026-08-08 | Confirmado por el Product Owner en la misma fecha de emisión: manifiesto aprobado como canónico y flags fijados, con `requiere_maqueta` == true en `GeometriaFactory-Web` y en `GeometriaFactory-Visor`, y `tiene_observabilidad_critica` == true en `GeometriaFactory-Api` sin invertir. La confirmación no sube versión: el documento cierra su emisión inicial. Derivación inicial desde `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §13 y su perfil de convención, según `Intake-Rules.md` §4. Incluye bloque de procedencia del framework 6.0, tabla de siete proyectos de código, grafo y orden topológico, resultado de las ocho validaciones bloqueantes y flags derivados de `Master-Prompt.md` §4. | Orquestador SDD |
