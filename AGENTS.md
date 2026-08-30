# AGENTS.md — Fábrica de Geometría

> **Derivado y no fuente.** Este archivo se regenera completo desde
> `SDD/Docs/Producto/11-Documentacion/Contrato-Agentes.md`. Si los dos divergen, manda el contrato.
> **No editar acá.**

## Qué es este repositorio

**Fábrica de Geometría** es un laboratorio de la cátedra: un alumno pega el texto JSON que emite su propio programa, el sistema lo interpreta, reconstruye las figuras, señala las discrepancias entre lo declarado y lo derivado, y las dibuja en tres dimensiones. El docente revisa y resuelve.

Dos unidades de entrega —`GeometriaFactory-Api`, el servicio de datos, y `GeometriaFactory-Web`, la pieza pública con su visor— sobre siete proyectos de código. C# sobre .NET 10, EF Core sobre SQLite, TypeScript con webpack en el visor.

**El repositorio no es sólo código.** `SDD/Docs/` es el corpus que lo gobierna y tiene sus propias reglas.

## Construir

```bash
bash scripts/build.sh          # producto entero, Release
bash scripts/build-visor.sh    # sólo el bundle del visor
```

Termina en **0 y sin advertencias**: `TreatWarningsAsErrors` está puesto y una advertencia detiene la construcción.

## Probar

```bash
bash scripts/test.sh                                    # las tres baterías, Release
dotnet test tests/GeometriaFactory.Domain.Tests -c Release   # un subconjunto
bash scripts/coverage.sh                                # cobertura y sus dos puertas
```

`coverage.sh` sale **0** si las dos puertas pasan, **1** si alguna no pasa y **2** si **no se pudo medir**. El `2` no es aprobación.

## Convenciones

- **Identificadores de código en inglés, texto para personas en castellano.** La fuente de nombres es `SDD/Docs/Producto/Norma-De-Nomenclatura.md` §6: un concepto que no está en la tabla **se agrega primero**, no se traduce por criterio propio.
- **Códigos de condición** en inglés, sin prefijo `CONTRATO_`, declarados en su catálogo. No se acuña uno nuevo sin agregarlo.
- **Comentarios: el porqué, no el qué.** Los que dicen «esto se probó y falló» se conservan.
- **Commits**: título en una línea; cuerpo con qué se decidió y con qué fundamento, no qué archivos cambiaron. Sin acentos en el cuerpo.
- **Una unidad de trabajo por pull request.**

## Antes de terminar un cambio

```bash
bash scripts/build.sh        # 0 y sin advertencias
bash scripts/test.sh         # 0
bash scripts/coverage.sh     # 0, y 2 no es aprobación
git status --short           # el árbol dice sólo lo que se tocó
```

Si el cambio toca una etapa con puerta propia, además `scripts/verify-stage-<letra>.sh`.

**Si el entorno se resistió** —un puerto, un permiso, una herramienta que no está—, la eventualidad va a `SDD/Docs/Producto/11-Documentacion/Bitacora-Eventualidades.md`, **con lo que se probó y no funcionó**.

## Límites: qué no se toca sin confirmación humana

| Límite | Por qué |
| --- | --- |
| `PROMPTs/` de cualquier repositorio | Es del Product Owner. Se lee, no se escribe |
| `SDD/Docs/_legacy/` | Registro histórico. Reescribirlo le hace decir a una emisión vieja algo que no dijo |
| El intake y los requerimientos técnicos | Documentos humanos. Lo desalineado **se eleva, no se corrige** |
| `PRODUCT-MANIFEST` y los apartamientos | Se declaran, no se ejercen por conveniencia |
| El almacén de trabajo | El 2026-08-15 una corrida de guiones se llevó una cuenta. Toda rutina destructiva usa **archivo propio** |
| Los contenedores `gf-api`, `gf-web`, `gf-back`, `gf-tunnel` | Son el despliegue local del Product Owner. Un servicio de prueba se levanta **aparte**, puerto libre y almacén propio |
| Fusionar un PR y borrar su rama | Son del Product Owner. Se entrega el enlace y se espera |

**Y una que no es sobre archivos:** cuando dos mediciones del mismo hecho no coinciden, **no se elige la que conviene**. Se revisan las dos — y en este repositorio suele estar mal la más elaborada.

## Notas del entorno que cuestan tiempo

- El servicio **escucha en 5080**, no en 8080: la configuración de Kestrel gana sobre `ASPNETCORE_URLS`. Publicar `-p <libre>:5080`.
- Correr contenedores con `-u "$(id -u):$(id -g)"` y `DOTNET_CLI_HOME`, o dejan archivos de root en el árbol.
- La imagen del SDK **no trae** `jq`, `python3` ni `sqlite3`. No se agregan: hay un escapador `awk` y aplicaciones de un solo archivo de C#.
- Una aplicación de un solo archivo necesita `#:project` para referenciar un proyecto, y **no puede usar `JsonSerializer` con reflexión** —`IL2026`/`IL3050` son errores acá—.

Las siete eventualidades completas, con lo que se descartó, están en la bitácora.

## A dónde ir, por intención

| Quiero… | Documento |
| --- | --- |
| entender el producto | `SDD/Docs/Producto/11-Documentacion/Vision-General-Sistema.md` |
| levantarlo en limpio | `SDD/Docs/Producto/11-Documentacion/Guia-Inicio-Rapido.md` |
| desplegarlo y saber volver atrás | `SDD/Docs/Producto/11-Documentacion/Guia-Despliegue.md` |
| saber si ya le pasó a alguien | `SDD/Docs/Producto/11-Documentacion/Bitacora-Eventualidades.md` |
| la superficie HTTP exacta | `SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/05-Arquitectura-Tecnica/Contratos-REST.md` |
| un ejemplo que corre | `samples/<capa>/<nivel>/` |
| saber cómo se nombra algo | `SDD/Docs/Producto/Norma-De-Nomenclatura.md` §6 |

**Los ejemplos de `samples/` corren y se comparan contra el §6 de su documento.** Donde no coinciden lo declaran, renglón por renglón, con su motivo. Es el lugar más rápido para entender qué hace de verdad una capa.
