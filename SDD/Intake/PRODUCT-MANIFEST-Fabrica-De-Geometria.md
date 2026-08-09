# PRODUCT-MANIFEST — Fábrica de Geometría

**Plantilla de referencia:** `PRODUCT-MANIFEST-template.md` versión 4.1 (Framework SDD)

Artefacto **derivado** por el orquestador SDD desde `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §13, según `Intake-Rules.md` §4 y `Master-Prompt.md` §3. No se completa a mano.

---

## §1 Bloque de producto

| Campo | Plano | Valor |
|---|---|---|
| `Nombre-Producto` | negocio | Fábrica de Geometría |
| `Slug-Producto` | documentación | `Fabrica-De-Geometria` |
| `Raiz-Codigo` | código | `GeometriaFactory` |
| `Artefacto-Agrupacion` | código | `GeometriaFactory.sln` |
| Proyecto de código principal | — | `GeometriaFactory-Api` |
| Intake (origen) | — | `PRODUCT-INTAKE-Fabrica-De-Geometria.md` (de su §13 se deriva este manifiesto) |
| Documento | — | `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` |
| Versión | — | 1.1 |
| Fecha | — | 2026-08-09 |
| Estado | — | Aprobado (confirmado por el Product Owner el 2026-08-08) |

`Slug-Producto` es el único campo derivado: se obtiene de `Nombre-Producto` con el algoritmo de `Master-Prompt.md` §3.2 (`Fábrica de Geometría` → `Fabrica-De-Geometria`). `Raiz-Codigo` y `Artefacto-Agrupacion` se leen declarados del intake (cabecera y perfil de convención de §13), no se derivan.

**Independencia de planos verificada** (`Master-Prompt.md` §3.2): `Fabrica-De-Geometria` y `GeometriaFactory` no son la misma cadena salvo puntuación, y `Nombre-Producto` no contiene el separador de segmentos del perfil. La derivación procede.

### §1.1 Procedencia del framework

| Artefacto del framework | Versión |
|---|---|
| Framework SDD (conjunto) | 6.0 |
| `Master-Prompt` | 5.2 |
| `Root-Rules` | 3.1 |
| `Rules-Contexto` | 3.1 |
| `Rules-Necesidades-Negocio` | 3.1 |
| `Rules-Especificacion-Funcional` | 4.0 |
| `Rules-UX-UI-DX` | 4.0 |
| `Rules-Arquitectura-Tecnica` | 3.1 |
| `Rules-Backlog-Tecnico` | 3.1 |
| `Rules-Plan-Sprint` | 3.1 |
| `Rules-Calidad-Y-Pruebas` | 3.1 |
| `Rules-Devops` | 3.1 |
| `Rules-Examples` | 4.1 |
| `Rules-Documentacion` | 4.1 |
| Reglas transversales aplicadas | `Intake-Rules` 3.2, `Vocabulario-Rules` 2.1, `Maqueta-Rules` 3.1 y `Deriva-Rules` 3.1 (Fase B2 confirmada para `GeometriaFactory-Web` y `GeometriaFactory-Visor`). `Migracion-Rules` no aplica: el árbol no atravesó migración normativa |
| `PRODUCT-INTAKE-template` | 2.1 |
| `PRODUCT-MANIFEST-template` | 4.1 |

`Rules-Prompts-AI` no lleva fila: la categoría 04 queda omitida por gating (`usa_llm` == false en los siete proyectos de código, ver §5).

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

## §2 Tabla de proyectos de código

| `Nombre-Proyecto-Codigo` | `Identidad-Codigo` | `tipo_proyecto_codigo` (D8) | Rol en el producto | `redistribuible` | Dependencias | Path |
|---|---|---|---|---|---|---|
| `GeometriaFactory-Api` | `GeometriaFactory.Api` | `rest-api` | Host REST en el servidor propio: endpoints, autenticación JWT y migraciones al arrancar (**principal**) | false | `GeometriaFactory-Application`, `GeometriaFactory-Infrastructure`, `GeometriaFactory-Contracts` | `src/GeometriaFactory.Api/` |
| `GeometriaFactory-Web` | `GeometriaFactory.Web` | `web-monolith` | Front Blazor Interactive Server con MudBlazor en el hosting público; único punto de contacto del navegador | false | `GeometriaFactory-Contracts`, `GeometriaFactory-Visor` | `src/GeometriaFactory.Web/` |
| `GeometriaFactory-Domain` | `GeometriaFactory.Domain` | `library` | Entidades e invariantes del dominio; centro de la regla de dependencias | false | — | `src/GeometriaFactory.Domain/` |
| `GeometriaFactory-Application` | `GeometriaFactory.Application` | `library` | Casos de uso y puertos (`IRepositorioTrabajos`, `IValidadorFiguras`, `IRelojDelSistema`) | false | `GeometriaFactory-Domain` | `src/GeometriaFactory.Application/` |
| `GeometriaFactory-Infrastructure` | `GeometriaFactory.Infrastructure` | `library` | EF Core sobre SQLite, seguridad (derivación de clave y emisión de JWT) y validador de figuras | false | `GeometriaFactory-Application`, `GeometriaFactory-Domain` | `src/GeometriaFactory.Infrastructure/` |
| `GeometriaFactory-Contracts` | `GeometriaFactory.Contracts` | `library` | DTOs de la API; contrato compartido por los dos procesos desplegables | false | — | `src/GeometriaFactory.Contracts/` |
| `GeometriaFactory-Visor` | `geometriafactory-visor` | `library` | Bundle JavaScript del visor 3D; visualizador puro (RA-02) | false | — | `visor/` |

**Excepción de nombre y de path declarada en el intake §13**, con su fundamento: `GeometriaFactory-Visor` es el único proyecto de código fuera del ecosistema .NET (paquete Node.js con TypeScript y webpack). Su `Identidad-Codigo` sigue la convención de `package.json` —minúscula con guiones— porque `GeometriaFactory.Visor` sería un nombre de paquete npm inválido, y su carpeta es `visor/` en la raíz y no `src/`, para que la solución .NET y el proyecto Node no compartan raíz de herramientas (intake §13 y §16, RT §4.2). La regla general `<Raiz-Codigo>.<Sufijo>` y el path `src/<Identidad-Codigo>/` rigen para los otros seis.

---

## §3 Grafo de dependencias

Dependencias de compilación (Clean Architecture: apuntan siempre hacia adentro):

```text
GeometriaFactory-Domain     -> GeometriaFactory-Application -> GeometriaFactory-Infrastructure -> GeometriaFactory-Api
GeometriaFactory-Domain     -> GeometriaFactory-Infrastructure
GeometriaFactory-Contracts  -> GeometriaFactory-Api
GeometriaFactory-Contracts  -> GeometriaFactory-Web
GeometriaFactory-Visor      -> GeometriaFactory-Web
```

La arista `GeometriaFactory-Web → GeometriaFactory-Api` es de **runtime** (HTTP con `HttpClient` y tipos de `Contracts`), no de compilación. Por eso no figura en la columna de dependencias y no introduce ciclo (intake §13, RT §4.1).

Orden topológico de generación y de construcción:

```text
nivel 0: GeometriaFactory-Domain, GeometriaFactory-Contracts, GeometriaFactory-Visor   (paralelizables)
nivel 1: GeometriaFactory-Application, GeometriaFactory-Web                            (paralelizables)
nivel 2: GeometriaFactory-Infrastructure
nivel 3: GeometriaFactory-Api
```

---

## §4 Resultado de las validaciones bloqueantes

| Validación | Resultado |
|---|---|
| Cada `tipo_proyecto_codigo` pertenece al conjunto cerrado D8 | Cumple: `rest-api` (1), `web-monolith` (1), `library` (5) |
| Exactamente un proyecto de código principal | Cumple: `GeometriaFactory-Api` |
| Sin colisión de `Nombre-Proyecto-Codigo` ni de `Identidad-Codigo` | Cumple: siete nombres y siete identidades distintas |
| Cada dependencia referencia un proyecto de código existente en §13 | Cumple: las siete aristas resuelven |
| Grafo acíclico | Cumple: orden topológico de cuatro niveles derivable |
| `Nombre-Producto` en prosa de negocio, independiente de `Raiz-Codigo` | Cumple |
| `Raiz-Codigo` declarado en el intake | Cumple: declarado, no asumido |
| §13 recorrible (sin filas de ejemplo, con perfil de convención) | Cumple |

---

## §5 Flags derivados (`Master-Prompt.md` §4)

Flags del producto:

| Flag | Valor | Origen |
|---|---|---|
| `equipo_n` | 1 | Intake §2: «1 docente + agente IA»; el agente no es persona del equipo. Efecto: la categoría 07 emite únicamente `Mini-Plan.md` |

Flags por proyecto de código:

| Proyecto de código | tipo D8 | `usa_llm` | `tiene_ui_final` | `multi_tenant` | `tiene_auth` | `tiene_portal_developers` | `tiene_extensibilidad` | `tiene_persistencia` | `requiere_compliance` | `tiene_observabilidad_critica` | `requiere_maqueta` |
|---|---|---|---|---|---|---|---|---|---|---|---|
| `GeometriaFactory-Domain` | `library` | false | false (DX) | false | **true** | false | false | false | false | false | false |
| `GeometriaFactory-Contracts` | `library` | false | false (DX) | false | false | false | false | false | false | false | false |
| `GeometriaFactory-Visor` | `library` | false | false (DX) | false | false | false | true | false | false | false | true |
| `GeometriaFactory-Application` | `library` | false | false (DX) | false | **true** | false | false | false | false | false | false |
| `GeometriaFactory-Web` | `web-monolith` | false | true (UX/UI) | false | true | false | false | false | false | false | true |
| `GeometriaFactory-Infrastructure` | `library` | false | false (DX) | false | true | false | false | true | false | false | false |
| `GeometriaFactory-Api` | `rest-api` | false | false (DX) | false | true | false | false | true | false | true | false |

Fundamento de los valores que no son el trivial:

- `usa_llm` false en los siete: ninguna sección §17 P.10 ni P.11 declara uso de LLM en el producto. El agente IA interviene en la construcción, no en el sistema construido. La categoría 04 se omite para todos.
- `multi_tenant` false: intake §17.3 P.4 lo declara explícitamente («Una instancia, un curso, un administrador», INV-05).
- `tiene_auth`: true en `Infrastructure` (derivación de clave y emisión de JWT HS256), en `Api` (ROPC con JWT Bearer, `POST /auth/token`) y en `Web` (canje de credenciales, token en el circuito, cookie de sesión). **Corregido el 2026-08-09 a true también en `Domain` y en `Application`**, alineando el manifiesto a lo que declara la sección de trazabilidad del `PRODUCT-INTAKE`, que es la fuente. La derivación original leía sólo el P.5 de cada bloque técnico y ponía false en los dos, razonando que modelar la condición no es implementar el mecanismo. El flag no distingue mecanismo de regla: `Domain` contiene la regla que condiciona la autenticación (INV-06, una cuenta `Pendiente` o `Bloqueada` no obtiene token) y `Application` contiene la **autorización** por pertenencia (INV-02, INV-03), que es precisamente lo que el flag habilita documentar. El efecto es que la categoría 05 de esos dos proyectos de código emite su ADR de autenticación, que con el valor anterior se habría omitido. Sin retrabajo: `Domain` ya había emitido los casos de uso correspondientes y `Application` no había arrancado.
- `tiene_persistencia`: true en `Infrastructure` (SQLite con EF Core) y en `Api` (toma de configuración la ruta del archivo y aplica migraciones al arrancar). Los otros cinco declaran «No aplica».
- `tiene_extensibilidad` true en `Visor`: el intake §18 declara que el punto de extensión del producto es el contrato de la fachada del visor (`inicializar`, `cargarJson`, `seleccionarPieza`, `redimensionar`, `destruir`), ejercido entero por el sample S-1.
- `requiere_compliance` false: intake §10 declara que no aplica ninguna normativa.
- `tiene_observabilidad_critica` true en `Api`: §17.5 P.10 declara latencia p99 con métrica numérica (500 ms). No hay SLO de disponibilidad en ningún proyecto de código; los demás no declaran p99.
- `requiere_maqueta`: true en `Web` por `tiene_ui_final` == true, y true en `Visor` por ser librería de componentes visuales. **Confirmado por el Product Owner el 2026-08-08**: los dos ejecutan la Fase B2, cada uno con su maqueta propia. `Maqueta-Rules.md` y `Deriva-Rules.md` pasan a ser reglas aplicadas del producto.

Los flags quedan **inmutables** desde esta confirmación (`Master-Prompt.md` §4). Un cambio posterior obliga a retroceder a la fase más temprana afectada del proyecto de código correspondiente.

---

## §6 Control de cambios

| Versión | Fecha | Cambios | Autor |
|---|---|---|---|
| 1.1 | 2026-08-09 | Corrección de `tiene_auth` en `GeometriaFactory-Domain` y `GeometriaFactory-Application`, de false a **true**, alineando el manifiesto a la sección de trazabilidad del `PRODUCT-INTAKE`, que es la fuente y declara true en los dos. La derivación original de §5 leía sólo el P.5 de cada bloque técnico. Detectada al emitir la categoría 03 de `GeometriaFactory-Domain`, que documentó la frontera de autenticación y encontró la divergencia contra el manifiesto. **No dispara el retroceso de fase de `Master-Prompt.md` §4**: el efecto del flag es habilitar casos de uso de autenticación en 02 y un ADR en 05; `Domain` ya había emitido esos casos de uso por otra vía y `Application` todavía no arrancó, de modo que ninguna fase generada queda inválida. Sube minor: corrige un valor derivado sin cambiar la composición ni la jerarquía. | Orquestador SDD |
| 1.0 | 2026-08-08 | Confirmado por el Product Owner en la misma fecha de emisión: manifiesto aprobado como canónico y flags fijados, con `requiere_maqueta` == true en `GeometriaFactory-Web` y en `GeometriaFactory-Visor`, y `tiene_observabilidad_critica` == true en `GeometriaFactory-Api` sin invertir. La confirmación no sube versión: el documento cierra su emisión inicial. Derivación inicial desde `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §13 y su perfil de convención, según `Intake-Rules.md` §4. Incluye bloque de procedencia del framework 6.0, tabla de siete proyectos de código, grafo y orden topológico, resultado de las ocho validaciones bloqueantes y flags derivados de `Master-Prompt.md` §4. | Orquestador SDD |
