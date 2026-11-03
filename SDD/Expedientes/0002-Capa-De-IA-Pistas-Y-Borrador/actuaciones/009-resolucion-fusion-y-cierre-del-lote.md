# EXP-0002 · Actuación 009 — Resolución: fusión de V1 y cierre del lote de la actuación 004

| Campo | Valor |
|---|---|
| Tipo | resolucion |
| Fecha | 2026-11-03 |
| Autor | mesa de Geometría: relator y especialistas en didáctica, seguridad de LLM, evaluación de modelos y despliegue (agentes, por delegación del Product Owner) |
| Corrige | — |

**Expediente:** [EXP-0002](../README.md) · **Foliada:** 009 — no se reescribe

---

## Índice

- [1. Fusión](#1-fusión)
- [2. Marco de la mesa](#2-marco-de-la-mesa)
- [3. Puntos del lote resueltos](#3-puntos-del-lote-resueltos)
- [4. Modelo, servidor y tiempos (E y GEO-04)](#4-modelo-servidor-y-tiempos-e-y-geo-04)
- [5. Utilidad y juez (REQ-IA-13 y REQ-IA-14)](#5-utilidad-y-juez-req-ia-13-y-req-ia-14)
- [6. V2](#6-v2)
- [7. Puertas y E2E con el flag encendido](#7-puertas-y-e2e-con-el-flag-encendido)
- [8. Lo que queda abierto, con dueño](#8-lo-que-queda-abierto-con-dueño)

## 1. Fusión

El PR #237 (`ai-layer/implementacion-v1`) se fusionó en `main` como `056801d` con la CI en verde. La
capacidad quedó **apagada por omisión** (`AI__Endpoint` vacía, `AI__HintsEnabled=false`): el producto
publicado no cambió. La carátula decía «sin pull request»; se corrige en esta actuación.

## 2. Marco de la mesa

Cada decisión se tomó contra un estándar nombrado, para que quien estudie el caso pueda reconstruir el
porqué:

| Estándar | Qué se tomó | Dónde se aplica |
|---|---|---|
| NIST AI RMF 1.0 (*Govern, Map, Measure, Manage*) | dueño por decisión (*Govern*); riesgo en contexto (*Map*); medición con rúbrica previa y oráculo recalculable (*Measure*); respaldo y flag como tratamiento (*Manage*) | §3 a §5 |
| OWASP Top 10 para aplicaciones LLM 2025 | LLM01 inyección (dos casos en el banco), LLM02 divulgación (el valor derivado es el secreto que se protege), LLM09 desinformación (pista geométricamente falsa) | §4, §5 |
| ISO/IEC 42001 | no se adopta el sistema de gestión en PROTO; se toma el registro de decisiones con dueño y fecha y la condición de revisión («deja de valer si») | toda la actuación |
| Andamiaje (Wood, Bruner y Ross, 1976) y retroalimentación formativa (Hattie y Timperley, 2007; Shute, 2008) | tres niveles que retiran ayuda; feedback sobre la tarea, específico, breve y sin la respuesta | §3, punto F |

## 3. Puntos del lote resueltos

| Punto | Resolución | Por qué |
|---|---|---|
| **A** · ingreso al intake | fila **F-27**, Could Have, nivel A0 | el producto cumple su objetivo sin ella y la plantilla garantiza el piso; A0 porque el modelo sólo redacta |
| **B** · revisor 2 del checklist | firmado por la mesa, V1 conforme, V2 conforme con observaciones; contrafirma docente optativa | la segunda lectura es independiente (otra sesión, contra el código fusionado); se declara que no es humana |
| **D** · ¿el borrador de V2 puede contener el valor derivado? | **no** | el comentario lo lee el alumno; la herramienta no escribe la solución (LLM02). El docente puede escribirla él al editar |
| **E** · servidor y modelo de producción (ACT-SEC) | servidor de modelos de la cátedra, sólo CPU; `qwen3.5:35b-a3b` con `think=false` | §4. No viaja ningún dato de persona (`01` §2.2), así que la ubicación se decide por capacidad y costo |
| **F** · texto de las doce plantillas | vigente, en `TemplateHints.cs`; rótulo provisional retirado (la 010 lo versiona como contrato de pistas 1.1, sin rótulo «definitivo») | `01` §5.3. Cada terna: dónde mirar, idea geométrica, comprobación propia. Sin dígitos: `EveryTemplatePassesTheFilter` |
| umbral de REQ-IA-26 | 60 % de borradores enviados con edición menor | debajo de eso el borrador no ahorra trabajo frente a escribir desde cero |
| número de ADR-IA-06 | reservado `ADR-14011` | correlativo después de `ADR-14010`; se escribe al reabrir V2 |

**Cómo se escribieron las plantillas.** Las provisionales decían «Revisá cómo calcula…» en los tres niveles
y repetían la idea entre el 1 y el 2. Las definitivas separan funciones: el nivel 1 sólo localiza (el alumno
sabe dónde mirar sin que se le diga qué está mal); el nivel 2 nombra la magnitud y la confusión típica
(área contra perímetro, superficie contra borde, cara contra cuerpo); el nivel 3 propone una acción de
autocomprobación («contá cuántas veces aparece la arista como factor») que el alumno hace y que lo lleva a
encontrar el error sin que se lo den resuelto.

## 4. Modelo, servidor y tiempos (E y GEO-04)

**Evaluación.** El 2026-11-03 la mesa corrió un banco de 10 casos de Geometría (8 de `ensayos/banco.json` y
2 de inyección) contra el servidor de modelos de la cátedra, con la rúbrica escrita antes de correr y el
filtro replicado para recalcular sin modelo.

| Modelo | Pasan el filtro | Inyecciones resistidas | Frío | p50 | p95 |
|---|---|---|---|---|---|
| `qwen3.5:35b-a3b`, `think=false` | 10/10 (IC 95 % Clopper-Pearson [0,69; 1]) | 2/2 | 33,6 s | 13,1 s | 14,9 s |

De los siete candidatos previstos sólo se midió éste; los seis restantes quedan pendientes (§8). La decisión
es **provisional por evidencia incompleta** y se registra así a propósito: es mejor un modelo medido que uno
supuesto, y la condición de revisión queda escrita.

**Tiempos del despliegue.** `AI__TimeoutSeconds=40` (cubre el frío de 33,6 s; el p95 caliente queda 2,7
veces por debajo), `AI__MaxObservations=3` (con el cupo de un pedido a la vez la espera del alumno queda
cerca de 40 s en lugar de 65 s) y el cupo en 1 (CPU: dos pedidos a la vez se reparten el mismo cómputo). Los
valores por omisión del código **no cambian**: son los del piloto y los fija cada entorno
(`09-Devops/Entornos-Deploy.md` §11.x.1). Se agrega un calentamiento al desplegar.

**Criterio de GEO-04, todavía no cumplido:** una corrida rotulada de 30 casos con 40 s que dé 30/30 Ok sin
«fuera de tiempo». Se corre con la prueba `ModeloReal` (la misma de E-010) apuntando al servidor elegido por
variable de entorno; su registro va a `ensayos/` y su resumen a una evidencia E-011.

## 5. Utilidad y juez (REQ-IA-13 y REQ-IA-14)

La mesa puntuó con la rúbrica las diez salidas del modelo y las plantillas de los mismos pares
(`ai-layer/ensayos/corrida-2026-11-03/Revision-Utilidad.md`): media del modelo 4,2 / 5 y de la plantilla
4,5 / 5. **Esta puntuación es de un juez (la mesa), no del docente**, y REQ-IA-14 prohíbe usar un juez que no
haya mostrado concordancia ≥ 80 % con el docente. Por eso no cierra REQ-IA-13: deja la planilla lista, con
la columna del docente vacía, y una conclusión que vale como hipótesis: **con el prompt de la corrida, la
plantilla definitiva enseña igual o mejor que el modelo**, y el modelo gana en variedad pero pierde en voseo y
a veces da la operación completa. Los ajustes al prompt (`01` §5.2, reglas 6 y 7) apuntan a eso.

## 6. V2

Condición 1 (piloto de V1 cerrado con puntuación docente): no cumplida. Condición 2 (banco de V2): no
cumplida. Condición 3 (umbral de REQ-IA-26): **cumplida** en §3. Punto D: resuelto. V2 sigue diferida.

## 7. Puertas y E2E con el flag encendido

No corrieron: necesitan la Api y la Web levantadas contra el servidor de modelos y un navegador. Se deja
fijado el entorno de prueba que las corre (`Entornos-Deploy.md` §11.x.1, columna «Prueba»: flag encendido,
40 s, tres observaciones) y que la evidencia es la salida de la CI y de las E2E con ese entorno, foliada como
E-012.

## 8. Lo que queda abierto, con dueño

| Qué | Dueño | Cierra con |
|---|---|---|
| Seis candidatos restantes del banco | mesa de evaluación | tabla completa en `01` §4.7 y confirmación o cambio del modelo |
| Corrida rotulada de 30 casos con 40 s | implementador | E-011 con 30/30 Ok sin «fuera de tiempo» |
| Puntuación docente de REQ-IA-13 | docente | columna «Docente» de la planilla; media ≥ 4 y mayor que la plantilla |
| Concordancia del juez (REQ-IA-14) | mesa, con la planilla del docente | proporción de casos con diferencia ≤ 1 punto, ≥ 80 % |
| Puertas c, f, g, i y E2E con el flag | implementador | E-012 |
| `AI__HintsEnabled=true` en producción | Product Owner, con lo anterior en verde | cambio de entorno, sin cambio de código |
