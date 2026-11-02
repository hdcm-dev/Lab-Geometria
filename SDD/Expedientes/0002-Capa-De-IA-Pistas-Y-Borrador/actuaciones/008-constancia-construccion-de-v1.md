# EXP-0002 · Actuación 008 — Constancia: construcción de V1, tramos T1 a T8

| Campo | Valor |
|---|---|
| Tipo | constancia |
| Fecha | 2026-11-02 |
| Autor | implementador de la capa de IA (agente) |
| Corrige | — |

**Expediente:** [EXP-0002](../README.md) · **Foliada:** 008 — no se reescribe

---

## Índice

- [1. Tramos ejecutados](#1-tramos-ejecutados)
- [2. Cifras medidas](#2-cifras-medidas)
- [3. Corrida rotulada con modelo real](#3-corrida-rotulada-con-modelo-real)
- [4. Apartamientos y desvíos, declarados](#4-apartamientos-y-desvíos-declarados)
- [5. Lo que no se hizo](#5-lo-que-no-se-hizo)

## 1. Tramos ejecutados

Un commit por tramo en la rama `ai-layer/implementacion-v1`, en el orden del plan de la actuación 007. El
commit de la norma (T1) precede a todo commit que toque `src/`.

| Tramo | Commit | Qué quedó |
|---|---|---|
| U0 | `0b5fb78` | Folios 005 a 007 y carátula. Los folios 001 a 004 y E-001 a E-006 sin tocar |
| T1 | `fbc6b46` | Norma 1.32 (§6.27: 19 filas de `ai-layer/04` más 25) y `ADR-14006` a `ADR-14010`; nota de correspondencia en `ai-layer/03-ADR.md` |
| T2 | `d151de5` | `IObservationHintWriter`, `ObservationHintRequest`, `HintResult`, `Hints`, `HintOrigin`, `DiscrepancyDirection`, `ExplainObservationsUseCase` |
| T3 | `30ab575` | `Infrastructure/AI`: `HintFilter` (paridad 8/8 con `pistas.py`), `TemplateHints` (doce, provisionales), `ModelObservationHintWriter`, `HintConcurrencyGate`, `AiOptions` |
| T4 | `5651f35` | Composición: quinto puerto, cliente sólo con dirección y modelo, cupo singleton; `DomainReferencesNoAiSdk` y `AiSdkOnlyInInfrastructureAiAndComposition` |
| T5 | `2880a7f` | `POST /v1/interpretaciones/pistas`, contratos `WorkHintsRequest`/`WorkHintsResponse`, `Contratos-REST.md` 1.12 |
| T6 | `4259a07`, `b49fcb2` | Norma 1.33 (§6.27.3) y la Web detrás de `AI:HintsEnabled` (wireframes 1.3, arquitectura de la Web 3.10) |
| T7 | `272518d` | Backlog técnico 3.3, matriz de cobertura 2.4, entornos 3.6, changelog, `ai-layer/README` y checklist |
| T8 | el commit que asienta esta constancia | Prueba rotulada `ModeloReal`, evidencias E-007 a E-010, carátula |

**Mutación de REQ-IA-02 (T4).** Con `using Microsoft.Extensions.AI;` agregado a
`Infrastructure/Persistence/EfCoreAccountRepository.cs`, `AiSdkOnlyInInfrastructureAiAndComposition` dio
**rojo** (1 de 1 fallida); se revirtió y volvió a verde. `git diff c5afa0e -- deploy .github` queda vacío.

## 2. Cifras medidas

- Build con **0 advertencias** y batería sin E2E: **741 en verde, 0 fallos, 1 salteada** (la rotulada
  `ModeloReal`, que sin `AI__Endpoint` no corre). Application 70, Domain 95, Integration 576 (+1 salteada).
  Línea base 591: **150 pruebas nuevas** ([E-007](../evidencia/E-007-build-y-pruebas.txt)).
- Cobertura: QG-03, QG-06 y QG-04 **conformes**; Infrastructure 97,2 % de líneas, Api 97,0 %, Application
  96,0 %; integración 77,8 % de la pirámide ([E-008](../evidencia/E-008-cobertura.txt)).
- `verify-stage-d`, `verify-stage-e` y `verify-stage-h` **conformes** en contenedor.
- Autoprueba de referencia `python3 -I ai-layer/ensayos/pistas.py --autoprueba`: **8/8**.
- R-PRIV y A11 sobre el diff de la rama, la evidencia y los mensajes de commit: **0 coincidencias**
  ([E-009](../evidencia/E-009-rpriv.txt)).

## 3. Corrida rotulada con modelo real

Contra el adaptador real compuesto como en producción y el servidor Ollama local del laboratorio (sólo CPU;
su dirección no se asienta), con el modelo del ensayo `qwen3.5:4b` y la variante A, sobre los 30 casos de
`ai-layer/ensayos/banco.json` ([E-010](../evidencia/E-010-corrida-rotulada-modelo-real.txt)):

- **30/30 `Ok` de origen modelo**: ninguna respuesta rechazada por el filtro ni degradada.
- **0/30 fugas entregadas** (derivado, JSON o código); IC 95 % de Clopper-Pearson **[0,000; 0,116]**.
- Latencia de **26 a 89 s** por llamada, con el servidor atendiendo a la vez a otra sesión.

**Hallazgo que se eleva:** la corrida usó `AI:TimeoutSeconds=180`. Con el tope por omisión de **20 s**, las
treinta llamadas habrían caído a plantilla por «fuera-de-tiempo». En un servidor sólo CPU el tope del piloto
no deja pasar al modelo; subirlo o cambiar de servidor es del Product Owner (punto E del lote). La capa queda
apagada por omisión, así que no afecta lo publicado.

## 4. Apartamientos y desvíos, declarados

1. **Nombres.** El plan de la mesa decía `HintRequest` y `Hint`; se usaron los de las filas de `ai-layer/04`
   (`ObservationHintRequest`, `Hints`, `Level1..3`). El pedido **lleva** `DerivedValue`, como dice
   `ai-layer/01` §4.2 («el adaptador conoce el derivado aunque no lo mande al modelo»); el plan decía que no
   entraba. Nunca viaja al modelo: lo prueba `HintRequest_CarriesOnlyTypeFieldDeclaredDirection`.
2. **El puerto tiene dos miembros**, no uno: `TemplateFor` sirve la plantilla de las advertencias que
   exceden el tope sin tocar el modelo (REQ-IA-10), porque Application no ve `TemplateHints`.
3. **Filas de la norma con atraso.** Los miembros privados de T3 y T4 se escribieron antes de tener fila;
   entraron en §6.27.3 (norma 1.33) antes del código de la Web. Apartamiento de §6.1, sin renombres.
4. **Rótulo de origen.** El plan decía «pista del docente»; se rotula **«Pista de plantilla»**, porque las
   plantillas son del agente y provisionales: llamarlas del docente sería falso hasta que él las fije.
5. **Aviso de dirección inválida** por la salida de error del proceso, sin el valor (la composición no tiene
   todavía registro): nombra la llave, nunca lo que traía.
6. **Folios y evidencias anteriores.** Los folios 001 a 004 usan la cabecera en negrita y no la tabla que mide
   A4, y las evidencias E-001 a E-006 no abren con las tres líneas que mide A9; no se reescriben (§5.1). Los
   folios 005 a 008 y las evidencias E-007 a E-010 cumplen A3, A4, A6, A8 y A9.
7. **`ContractRoutePrefixTests`** y **`CompositionGateTests`**: altas declarativas (dieciocho puntos,
   cinco puertos); las pruebas se renombraron para que el nombre siga diciendo la cifra. No se relajó ninguna
   aserción ni umbral.

## 5. Lo que no se hizo

- **V2** no se construyó (veto de la mesa; condición de reapertura en la actuación 007).
- **REQ-IA-13 y REQ-IA-14** (puntuación docente y juez): fuera del alcance del agente.
- Puertas **c, f, g e i** y E2E: necesitan las dos piezas levantadas, navegador o el despliegue; quedan para
  la CI y el punto de control. El bundle del visor no se construyó localmente (sin Node en la imagen).
- El ingreso de V1 al intake como F-n y la revisión humana del checklist siguen pendientes del Product Owner.
- No se abrió pull request ni se fusionó: es del coordinador.

Sigue: pull request de `ai-layer/implementacion-v1` a `main`, a cargo del coordinador; después, el punto A del lote al Product Owner.
