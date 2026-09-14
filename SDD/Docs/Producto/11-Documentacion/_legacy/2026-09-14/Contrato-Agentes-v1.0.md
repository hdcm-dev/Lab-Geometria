---
doc_id: DOC-PRODUCTO-CONTRATO-AGENTES-01
doc_type: contrato-agentes
title: Contrato de contexto para agentes — Fábrica de Geometría
status: Vigente
rol_intervencion: [integrador, mantenedor, operador]
owner: Technical Writer / Documentation Lead (AG-00110)
version: "1.0"
last_review: 2026-08-30
momento: 2
traces:
  - Rules-Documentacion-§2.1
  - Rules-Documentacion-§4.2
---

# Contrato de contexto para agentes

**Producto:** Fábrica de Geometría
**Nivel:** Producto
**Deriva:** `AGENTS.md`, en la raíz del repositorio

---

> **Este documento es la fuente y `AGENTS.md` es el derivado.** Si divergen, manda éste y el otro se regenera. `AGENTS.md` no se edita a mano: se emite en la primera corrida del Momento 2 y se refresca en cada una, porque es cuando los agentes codifican y es cuando más lo necesitan.

## 1. Qué es este repositorio

**Fábrica de Geometría** es un laboratorio de la cátedra: un alumno pega el texto JSON que emite su propio programa, el sistema lo interpreta, reconstruye las figuras, señala las discrepancias entre lo declarado y lo derivado, y las dibuja en tres dimensiones. El docente revisa y resuelve.

Son **dos unidades de entrega** —`GeometriaFactory-Api`, el servicio de datos, y `GeometriaFactory-Web`, la pieza pública con su visor— sobre **siete proyectos de código**. C# sobre .NET 10, Entity Framework Core sobre SQLite, y TypeScript con webpack para el visor.

**El repositorio no es sólo código.** `SDD/Docs/` es el corpus que lo gobierna, y está sujeto a las mismas reglas de identificador, versión y trazabilidad que el código a las suyas.

## 2. Cómo se construye

```bash
bash scripts/build.sh
```

Encadena la generación del bundle del visor con la compilación de la solución. Termina en **0 y sin advertencias**: `Directory.Build.props` fija `TreatWarningsAsErrors`, de modo que una advertencia detiene la construcción y no se arrastra. Produce **`Release`**.

Sólo el visor:

```bash
bash scripts/build-visor.sh
```

**La configuración se declara siempre y en los dos lados** —el que construye y el que ejecuta— aunque el valor por omisión coincida. Quien quiera comprobar que la regla se cumple en todo el árbol: `bash scripts/verify-explicit-configuration.sh`.

## 3. Cómo se corren los tests

```bash
bash scripts/test.sh
```

Corre `dotnet test GeometriaFactory.sln --configuration Release`. Devuelve 0 con las tres baterías en verde.

Un subconjunto, por proyecto de prueba:

```bash
dotnet test tests/GeometriaFactory.Domain.Tests --configuration Release
```

Cobertura y sus dos puertas:

```bash
bash scripts/coverage.sh
```

Sale **0** si las dos pasan, **1** si alguna no pasa, y **2** si **no se pudo medir**. Los tres son distintos y el `2` importa: una medición que no se hizo no es una medición que dio cero.

## 4. Convenciones

**Idioma.** Identificadores de código **en inglés**; texto para personas **en castellano**. La fuente de nombres es `SDD/Docs/Producto/Norma-De-Nomenclatura.md` §6: **un concepto que no está en la tabla no se traduce por criterio propio, se agrega primero**.

**Códigos de condición.** Van en inglés y sin prefijo `CONTRATO_`. Los declara `ConditionCode.cs` y sus tres pares por capa. **No se acuña un código nuevo sin agregarlo a su catálogo.**

**Comentarios.** Este repositorio comenta **el porqué y no el qué**. Un comentario que repite lo que la línea dice sobra; uno que explica por qué se descartó la alternativa obvia, no. Los comentarios que dicen «esto se probó y falló» se conservan.

**Commits.** Título en una línea, cuerpo que explica **qué se decidió y con qué fundamento**, no qué archivos cambiaron. Sin acentos en el cuerpo del mensaje.

**Una unidad de trabajo por pull request.** Cuando dos son inseparables, se declaran las dos y se dice cuál es revertible sin la otra.

## 5. Antes de dar por terminado un cambio

Los cuatro, en este orden:

```bash
bash scripts/build.sh        # 0 y sin advertencias
bash scripts/test.sh         # 0
bash scripts/coverage.sh     # 0, y 2 no es aprobación
git status --short           # el árbol dice sólo lo que se tocó
```

Si el cambio toca una etapa con puerta propia, además su verificador: `scripts/verify-stage-<letra>.sh`.

**Si algo del entorno se resistió** —un puerto, un permiso, una herramienta que no está— la eventualidad va a `Producto/11-Documentacion/Bitacora-Eventualidades.md` **con su campo de intentos descartados**. Es lo que evita que el siguiente pierda el mismo tiempo.

## 6. Límites de intervención

**Lo que no se toca sin confirmación humana:**

| Límite | Por qué |
| --- | --- |
| **`PROMPTs/` de cualquier repositorio** | Es del Product Owner. Se lee, no se escribe |
| **`SDD/Docs/_legacy/`** | Es registro histórico. Reescribirlo le hace decir a una emisión vieja algo que no dijo |
| **El intake y los requerimientos técnicos** | Son documentos humanos. Lo que la construcción encuentra desalineado **se eleva, no se corrige** |
| **`PRODUCT-MANIFEST` y las decisiones de `Root-Rules.md` §11** | Un apartamiento se declara, no se ejerce por conveniencia |
| **El almacén de trabajo** | `scripts/store-path.sh` lo explica: el 2026-08-15 una corrida de guiones se llevó la cuenta de administrador. Toda rutina destructiva usa **archivo propio** |
| **Los contenedores `gf-api`, `gf-web`, `gf-back` y `gf-tunnel`** | Son el despliegue local del Product Owner. Un servicio de prueba se levanta **aparte**, en puerto libre y con almacén propio |
| **Fusionar un pull request y borrar su rama** | Son del Product Owner. El agente entrega el enlace y espera |

**Y una regla que no es sobre archivos:** cuando una medición y otra del mismo hecho no coinciden, **no se elige la que conviene**. Las dos se revisan, y la experiencia de este repositorio dice que suele estar mal la más elaborada.

## 7. A dónde ir, por intención

| Quiero… | Documento |
| --- | --- |
| entender qué es el producto y por dónde entrar | `Producto/11-Documentacion/Vision-General-Sistema.md` |
| levantarlo entero en una máquina limpia | `Producto/11-Documentacion/Guia-Inicio-Rapido.md` |
| desplegarlo, en orden, y saber cómo volver atrás | `Producto/11-Documentacion/Guia-Despliegue.md` |
| saber si algo ya le pasó a alguien | `Producto/11-Documentacion/Bitacora-Eventualidades.md` |
| correr el servicio en un contenedor | `Unidades-Entrega/GeometriaFactory-Api/11-Documentacion/Guia-Contenedor.md` |
| diagnosticar una falla en ejecución | `Runbook-Operacion.md` del proyecto de código afectado |
| ubicar en el repositorio lo que la arquitectura llama componente | `Recorrido-Codigo.md` del proyecto de código |
| agregar una funcionalidad sin romper el diseño | `Guia-Contribucion.md` del proyecto de código |
| entender el modelo mental de una pieza | `Conceptos-Fundamentales.md` del proyecto de código |
| ver la superficie HTTP exacta | `Unidades-Entrega/GeometriaFactory-Api/05-Arquitectura-Tecnica/Contratos-REST.md` |
| ver un ejemplo que corre | `samples/<capa>/<nivel>/`, gobernado por su `10-Examples` |
| saber cómo se nombra algo | `Producto/Norma-De-Nomenclatura.md` §6 |

**Los ejemplos de `samples/` no son ilustrativos: corren y se comparan contra el §6 de su documento.** Donde no coinciden, lo declaran renglón por renglón con su motivo. Es el lugar más rápido para entender qué hace de verdad una capa.

---

## Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-30 | Emisión inicial, que cierra el hallazgo `I-02` del incremento `I-1` de la Fase I —**el único bloqueante**: sin este contrato el paso 4 de la fase no se podía ejecutar en ninguna corrida—. Los siete puntos de `Rules-Documentacion.md` §4.2, con los comandos tomados de los guiones vigentes y los límites de intervención tomados de decisiones ya registradas: el incidente del almacén del 2026-08-15, la propiedad del `PROMPTs/`, y el despliegue local del Product Owner. De acá se deriva el `AGENTS.md` de la raíz. |
