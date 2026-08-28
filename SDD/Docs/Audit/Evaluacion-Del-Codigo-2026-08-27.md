# Evaluación del estado de la codificación — Fábrica de Geometría

**Producto:** Fábrica de Geometría
**Documento:** Evaluacion-Del-Codigo-2026-08-27.md
**Versión:** 1.0
**Fecha:** 2026-08-27
**Autor:** Orquestador de reanudación SDD
**Nivel:** Producto
**Pedido por:** el Product Owner, el 2026-08-27, después de la quinta reanudación
**Complementa a:** [`Estado-Del-Destino-2026-08-27.md`](Estado-Del-Destino-2026-08-27.md) 1.2

---

## 0. Qué es este documento, y en qué se diferencia del informe de estado

**El informe de estado mide el árbol documental; éste mide el código y lo corre.** Todo lo que sigue
se midió el 2026-08-27 dentro del entorno contenido con el kit de .NET 10; **no hay una sola cifra
deducida de lo que un documento afirma**.

**No es un audit** y no emite veredicto sobre el producto. Es el insumo de una decisión de prioridad.

---

## 1. Lo que está construido

| | Archivos | Líneas |
|---|---|---|
| `src/` — seis proyectos de código .NET | 166 | **12 484** |
| `tests/` — tres proyectos de prueba | 47 | **11 102** |
| `visor/` — TypeScript, sin `node_modules` | 10 | **1 093** |

**La construcción termina en 0 y sin advertencias** —`QG-01`— y **la batería entera pasa**: `dotnet
test` corre **311 casos** y los 311 pasan. Domain 94, Application 56, Integración 161.

**Cero `TODO`, `FIXME` o `NotImplementedException`.** El barrido devuelve 17 aciertos y **los 17 son
la palabra castellana «todo» en comentarios**. Se abrieron uno por uno: el código no lleva deuda
marcada.

---

## 2. Lo que falta, y son cuatro frentes distintos

### 2.1 La etapa `i` — la única del roadmap que falta

Sin novedad respecto del informe de estado §10.3: los siete criterios **miden un despliegue que no
existe**, y lo que falta es todo del Product Owner. **Once de los doce ítems diferidos vigentes
esperan esta fase.**

### 2.2 Las Fases **I** y **J** del método — nunca corrieron

**La colisión de nombres es real y conviene decirla: la etapa `i` es del roadmap del producto y es el
despliegue; la Fase I es de `Master-Prompt.md` y es la pasada de ejecución de los contratos de
verificación y de la documentación viva.** Son cosas distintas y ninguna de las dos corrió.

| Qué debía producir la Fase I | Estado medido |
|---|---|
| **16 contratos `VER-*`** en los 19 documentos de `10-Examples` | Los **42 bloques de evidencia** dicen `estado: "No verificado — sin código"`, **fechados el 2026-08-11**. Hay 22 000 líneas de código desde entonces |
| **89 filas de sensado de deriva** — 12 en `-Api`, 77 en `-Web` | Las **89** en `Sin verificar`. La línea de base visual nunca se contrastó contra lo construido |
| **15 artefactos de la categoría 11** | Los **15** en `Planificado`. Los README lo declaran sin disimulo: *«Nada de lo que se enumera acá está redactado»* |
| **`AGENTS.md`** en la raíz | **No existe**, ni tampoco `Contrato-Agentes.md`, que es de donde se regenera |

**Su precondición dura acaba de quedar cumplida.** `Master-Prompt.md` §7.1 pide tres cosas y **hasta
hoy fallaba la segunda**:

| Condición | Antes | Ahora |
|---|---|---|
| Existe código fuente | ✅ | ✅ |
| Los tests corren | ✅ | ✅ |
| **`/samples` tiene al menos un sample implementado** | ❌ — 20 archivos y los 20 `.md` | ✅ — [`samples/domain/01-basico/`](../../../samples/domain/01-basico/), §4 |

### 2.3 Diez puertas de calidad pasaron a bloqueantes, y dos no tenían instrumento

**`D1` volvió bloqueantes a `QG-03`, `QG-04`, `QG-13` y `QG-14` el 2026-08-26**, y el propio `D1` §0
lo anticipó: *«nadie midió qué haría cada gate si frenara. Eso se ve en la primera corrida»*. Ésta es
la primera corrida.

**No había con qué medir dos de ellos.** Los tres proyectos de prueba **no tenían recolector de
cobertura anclado** —traían `xunit`, `Microsoft.NET.Test.Sdk` y `Mvc.Testing`, y nada más— y
`scripts/test.sh` es un `dotnet test` pelado. **Una puerta bloqueante sin instrumento no frena: pasa
por omisión**, que es peor que no tenerla.

**Y es el mismo defecto que `HM-01`, con una segunda herramienta que nadie había nombrado.** El paso
`A2b` cerró «por lectura» la fila *«la herramienta concreta de cada stage —ejecutor de pruebas,
**recolector de cobertura**, generador del inventario»* con `dotnet build`, `dotnet test`, `npm ci`,
`webpack` y `playwright`. La mesa del 2026-08-27 reparó el generador **porque `HM-01` lo nombraba**;
el recolector de cobertura tampoco estaba en esa lista y **nadie lo levantó**.

Anclado el recolector (§4), los números existen por primera vez:

| Proyecto de código | Líneas | Ramas | Umbral `QG-03` | Veredicto |
|---|---|---|---|---|
| `GeometriaFactory.Domain` | 1516/2076 · **73,0 %** | 604/960 · **62,9 %** | 90 / 85 | **NO PASA**, en las dos |
| `GeometriaFactory.Application` | 1778/2064 · **86,1 %** | 432/592 · **73,0 %** | 85 / 80 | **NO PASA** en ramas |
| `GeometriaFactory.Infrastructure` | 3226/3342 · **96,5 %** | 346/488 · **70,9 %** | 85 / 80 | **NO PASA** en ramas |
| `GeometriaFactory.Api` | 2410/2746 · **87,8 %** | 338/814 · **41,5 %** | 75 / 70 | **NO PASA** en ramas |
| `GeometriaFactory.Contracts` | 284/284 · **100 %** | — | sin umbral de líneas | — |
| `GeometriaFactory.Web` | 2650/3122 · **84,9 %** | 992/1504 · **66,0 %** | sin umbral de líneas | — |

**`QG-04`, la pirámide invertida: 161 de integración contra 150 unitarias → 51,8 %, y pide 60 %.**
**NO PASA**, y no es cuestión de redondeo.

**Cómo leer el número de ramas antes de sacar conclusiones.** Coverlet cuenta también las ramas que
**genera el compilador** —máquinas de estado de `async`, comprobaciones de nulo—, que ningún test
escribe a propósito. El número de ramas es por lo tanto un **piso**, y calibrar cuánto de la
diferencia es código propio es trabajo de la próxima corrida. **El de líneas no tiene esa reserva**:
`Domain` en **73 %** contra un umbral de **90 %** es una distancia real.

**De los quince gates, la canalización automatiza dos.** El único flujo del repositorio,
`deploy-front-ftp.yml`, corre `QG-01` y `QG-02` antes de publicar el front. **`QG-03` a `QG-15` no
están automatizados en ningún lado**, y `QG-05` —«exactamente cuatro puntos fuera de la guardia, ni
uno más»— es, por su propio documento, «el gate más caro de olvidar».

### 2.4 Lo que ya estaba declarado

Los **9 ítems vencidos** (`E-02` y `E-03` de la mesa), los **11 sin evento** (`E-04`), y `HM-02` y
`HM-03` para el reporte del framework.

---

## 3. Tres hallazgos que ninguna corrida anterior tenía

### 3.1 Una prueba intermitente en una puerta bloqueante — **REPARADA**

**La primera corrida de la batería salió en rojo.**
`ProvisionalPasswordTests.NoProvisionalDerivesFromAccountDataNorFromTheClock` falló con la provisoria
`NfB2afFrrE9F`, que contiene `FrrE`; `frre` está en la lista de datos de cuenta que la prueba barre
sin distinguir mayúsculas. **La segunda corrida pasó.**

**El comentario de la propia prueba razonaba el piso y no lo medía:** decía que tres caracteres
seguidos serían intermitentes y cuatro no, sobre «un alfabeto de 56 caracteres». El alfabeto real
tiene **57** y la comparación ignora mayúsculas, de modo que cada letra vale **dos** de 57.

Medido con veinte mil corridas simuladas sobre el mismo alfabeto:

| Configuración | Fallos por azar |
|---|---|
| La lista como estaba, piso de 4 caracteres | **0,44 %** — una de cada 227 |
| Sólo los dos fragmentos de 4, `frre` y `Diaz` | **0,40 %** — aportan casi todo |
| Con el piso subido a **6 caracteres** | **0,000 %** |

**Reparada** subiendo el piso a seis, con la medición escrita en el comentario y una aserción que
impide que vuelva a bajar. Se pierde el barrido del apellido suelto, y **no importa**: lo que
garantiza la propiedad es que la invocación **no recibe ninguno de esos datos**; el barrido es la
comprobación de segunda mano, y una derivación real dejaría un rastro mucho más largo.

### 3.2 El catálogo de códigos de error documentado no es el que el sistema emite

**Lo encontró el primer sample en su primera corrida, que es exactamente para lo que sirve un
sample.** Seis de las diez líneas del snapshot de §6 coinciden; **cuatro no**, y las cuatro por el
mismo motivo.

| Lo que la documentación declara | Lo que el sistema emite |
|---|---|
| `ADMINISTRADOR_YA_CONFIGURADO` | `ADMINISTRATOR_ALREADY_CONFIGURED` |
| `DATO_OBLIGATORIO_AUSENTE` | `REQUIRED_FIELD_MISSING` |
| `CUENTA_PENDIENTE` | `ACCOUNT_PENDING` |
| `CAMBIO_DE_CONTRASENA_PENDIENTE` | `PASSWORD_CHANGE_PENDING` |

**No es un desvío del sample y no es cosmético.** La forma castellana aparece **21 veces en el corpus
vivo y la inglesa ninguna** —casos de uso, casos de prueba referenciales, mensajes de error de la
`03`—; la forma inglesa es la que **viaja por el cable**, desde `ConditionCode` del dominio hasta
`ErrorCode` de la capa de contratos, la traducción de `ContractTranslation` y el consumo de la pieza
pública.

**Esto no se repara acá y es una escalada.** Elegir cuál de las dos formas es la buena **cambia el
contrato público de errores** del producto: es el disparador **3** de `Mesa-Rules.md` §7, cambio de
alcance, y es del Product Owner. **El snapshot del sample NO se ajustó al código**, porque ajustarlo
habría decidido en silencio que el código le gana a veintiún documentos, que es lo que un snapshot
existe para impedir. El sample sale **0** y su `--verificar` sale **1**, con el diff a la vista.

**Las dos salidas y qué cuesta cada una:**

| Salida | Qué implica |
|---|---|
| **Manda el código, la documentación se corrige** | 21 ocurrencias en el corpus. **No toca ni una línea de código** ni el contrato que el front ya consume |
| **Manda la documentación, el código se corrige** | Cambia el valor que la Api devuelve. Alcanza a `ConditionCode`, `ErrorCode`, `ContractTranslation`, la pieza pública y **las pruebas que los afirman**. Es un cambio de contrato público |

### 3.3 `samples/` quedó apuntando al árbol documental anterior

**57 de los 79 enlaces de `samples/` no resuelven.** Apuntan a `SDD/Docs/Proyectos/<proyecto>/…`, que
**la consolidación de las unidades de entrega retiró**: hoy el árbol es
`SDD/Docs/Unidades-Entrega/<unidad>/`, y los documentos de la 10 se renombraron además con el sufijo
del proyecto de código —`ejemplo-01-basico.md` pasó a `ejemplo-01-basico-dominio.md`—.

**Ninguna comprobación lo veía**: la compuerta de la mesa barre `SDD/Docs/` y `samples/` está afuera.

**Y no se reparan en masa, con un motivo medido.** De los 57, **38 tienen un único candidato por
nombre de archivo y 19 tienen más de uno**. Se intentó el recálculo automático sobre este mismo
documento y **mandó el enlace al archivo equivocado**: `ejemplo-01-basico.md` resolvía sin ambigüedad
a la unidad `-Web`, cuando el destino correcto era `ejemplo-01-basico-dominio.md` de `-Api`. **La
regla de `Master-Prompt.md` §10.0 recalcula por IDENTIFICADOR y estos enlaces no llevan ninguno**;
por nombre de archivo, «único» no quiere decir «correcto».

**Hay algo peor debajo, y conviene mirarlo antes de reparar.** Tres de los siete segmentos de
`samples/` —`contracts`, `visor` y parte de `web`— **son de proyectos de código que dejaron de ser
unidad de entrega**, y puede que su documento de gobierno ya no exista bajo ningún nombre. Repararlos
uno por uno sin resolver eso antes produciría enlaces que resuelven a documentos que no los
gobiernan.

---

## 4. Lo que esta pasada dejó hecho

| Qué | Dónde |
|---|---|
| **Recolector de cobertura anclado** en los tres proyectos de prueba | `coverlet.collector` **6.0.4**, con su versión fijada como el intake exige |
| **El informe de `TC-00037`**, que es el instrumento de `QG-03` y `QG-04` | [`../../../scripts/coverage.sh`](../../../scripts/coverage.sh) y [`../../../tools/informe-cobertura.cs`](../../../tools/informe-cobertura.cs) |
| **El primer sample implementado**, con su snapshot y su comparación | [`../../../samples/domain/01-basico/`](../../../samples/domain/01-basico/) |
| **La prueba intermitente, reparada y medida** | `ProvisionalPasswordTests` |

**El análisis del informe corre con `dotnet run` sobre un archivo suelto de C# y NO agrega ninguna
dependencia**: el mismo kit que compila el producto. Se descartó a propósito hacerlo en otro lenguaje
o con una herramienta de informes, en un repositorio cuyo intake declara que **ninguna versión de
paquete se resuelve sola**.

**Dos defectos del propio instrumento, encontrados corriéndolo y declarados en lugar de corregirse en
silencio.** Con `--logger trx`, VSTest deja el informe de cobertura **dos veces** —en su carpeta y
copiado bajo `<informe>/In/<host>/`— y la primera versión daba **todos los contadores al doble**; y
el reparto de la pirámide daba **0 %** porque el informe escribe la ruta del ensamblado en
minúsculas y la comparación distinguía mayúsculas. Los dos están reparados, con su motivo escrito en
el código.

---

## 5. Cómo conviene ordenarlo

| | Qué | Por qué en ese lugar | ¿Depende del Product Owner? |
|---|---|---|---|
| **1** | **Decidir el idioma de los códigos de condición** (§3.2) | Es la única escalada que **bloquea a las demás**: la Fase I va a escribir evidencia de los 16 contratos, y la mitad la afirmaría sobre un catálogo que no se sabe cuál es | **Sí, y es barata**: una decisión, y la salida más probable no toca código |
| **2** | **Correr la Fase I** | Su precondición dura ya está cumplida. 16 contratos, 89 filas de deriva y 15 documentos dejan de decir «sin código» sobre un sistema de 22 000 líneas | No |
| **3** | **Cerrar la brecha de `QG-03` y `QG-04`**, o mover el umbral con su motivo | Son bloqueantes desde el 2026-08-26 y hoy **no pasan**. Las dos salidas son legítimas; lo que no lo es es dejar una puerta que frena sin que nadie sepa que frena | **Sí para mover el umbral**, no para cerrar la brecha |
| **4** | **Automatizar los gates que faltan** en la canalización | Trece de quince no corren en ningún lado | No |
| **5** | **Reparar `samples/`** (§3.3), después de resolver qué gobierna a `contracts`, `visor` y `web` | Reparar antes produce enlaces que resuelven a documentos equivocados | No, salvo la pregunta de gobierno |
| **6** | **La etapa `i`** | Cierra el producto y destraba **once de los doce** ítems vigentes | **Sí, entera** |

---

## 6. Control de cambios

| Versión | Fecha | Cambios | Autor |
|---|---|---|---|
| 1.0 | 2026-08-27 | Emisión inicial, a pedido del Product Owner después de la quinta reanudación. **Todo se midió corriendo el código** dentro del entorno contenido: 311 casos, cobertura por proyecto y reparto de la pirámide. Declara los **cuatro frentes** que la pregunta «qué falta de codeo» mezcla, y separa la **etapa `i`** del roadmap de la **Fase I** del método, que comparten letra y no son lo mismo. **Tres hallazgos nuevos**: la prueba intermitente de una puerta bloqueante —medida en **0,44 %**, una de cada 227, y reparada—; **el catálogo de códigos de error documentado no es el que el sistema emite**, con 21 ocurrencias contra ninguna, que es **escalada por cambio de alcance**; y los **57 enlaces rotos de `samples/`**, que ninguna compuerta miraba y que **no se reparan en masa** porque el recálculo por nombre de archivo se comprobó que manda al documento equivocado. Deja anclado el recolector de cobertura, emitido el informe de `TC-00037` y **el primer sample implementado**, con lo que la precondición dura de la Fase I queda cumplida. | Orquestador de reanudación SDD |
