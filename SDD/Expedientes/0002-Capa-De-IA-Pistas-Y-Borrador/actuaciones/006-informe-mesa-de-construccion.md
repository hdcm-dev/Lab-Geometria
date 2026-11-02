# EXP-0002 · Actuación 006 — Informe: mesa de construcción de V1

| Campo | Valor |
|---|---|
| Tipo | informe |
| Fecha | 2026-11-02 |
| Autor | mesa de construcción (cinco lentes: arquitectura, seguridad, pruebas, dominio didáctico, norma) |
| Corrige | — |

**Expediente:** [EXP-0002](../README.md) · **Foliada:** 006 — no se reescribe

---

## Índice

- [1. Contrato de entrada](#1-contrato-de-entrada)
- [2. Acta: decisiones y votos](#2-acta-decisiones-y-votos)
- [3. Vetos](#3-vetos)
- [4. Riesgos que la mesa deja escritos](#4-riesgos-que-la-mesa-deja-escritos)

## 1. Contrato de entrada

- Especificación: `ai-layer/01-Especificacion-V1-Pistas.md` (16 requisitos), `02-…-V2` (8), `03-ADR.md`
  (seis ADR), `04-Propuesta-Norma-De-Nomenclatura.md` (19 filas), `05-Checklist-Completitud.md` y el ensayo
  de `ai-layer/ensayos/` (0/30 fugas del valor; 24/30 con la operación en palabras en el nivel 3).
- Estado del lote de la actuación 004: sin respuesta. Encargo de la actuación 005: construir por delegación.
- Línea base medida antes de tocar nada: árbol de solución completo, build con 0 advertencias y 591 pruebas
  sin E2E en verde (Application 61, Domain 94, Integration 436), en contenedor `dotnet/sdk:10.0.403`.
- Restricción: fusionar a `main` despliega; la capacidad va apagada por defecto.

Cada lente votó cada pregunta; la lente más afín decide los empates de criterio (forma de `Mesa-Rules.md`).

## 2. Acta: decisiones y votos

| Id | Pregunta | Decisión | Votos | Motivo |
|---|---|---|---|---|
| D-1 (PO-A) | ¿Se construye V1 sin respuesta al punto A? | Sí, como `Could` y con el flag apagado; el intake no se edita y el ingreso como F-n queda pendiente del PO | 5-0 | El PO pidió implementar; el «si no respondés» de A protege el intake, no prohíbe el código |
| D-2 (PO-F) | ¿Quién redacta las doce plantillas? | El agente, sobre el modelo Cubo·Area de 01 §5.2, rotuladas «provisional, pendiente del docente»; una prueba exige que las doce existan y pasen el filtro | 5-0 | Sin plantillas no hay respaldo, y la prueba impide que el respaldo filtre la solución |
| D-3 (PO-G) | ¿Se asientan ya las ADR y la tabla de la norma? | Sí, antes del código: tabla nueva de la Norma §6 y ADR-14006 a 14010; ADR-IA-06 (V2) queda propuesta y sin número | 5-0 asentar; 4-1 sin número para IA-06 | §6.1 y ADR-IA-05 lo exigen; V2 se difiere |
| D-FLAG | Con el flag apagado, ¿la ruta da 404 o plantilla? | Doble llave: sin `AI:Endpoint` el puerto queda degradado y la ruta responde 200 sólo con plantilla; en la Web `AI:HintsEnabled=false` (por omisión) oculta el botón y no pide nada | 4-1 (arquitectura pidió 404) | Ruta aditiva sin consumidores; cumple REQ-IA-15 y REQ-IA-07 tal como están; la Web queda como hoy |
| D-ADAPT | ¿Qué adaptador concreto? | OllamaSharp con versión fija, registrado sólo en la composición y sólo con dirección válida; sin clave | 5-0 | — |
| D-CUPO | ¿Cupo y tiempo? | Cupo singleton `SemaphoreSlim(1)` con `WaitAsync(0)`, sin cola; 20 s y 5 observaciones configurables; las pruebas inyectan 50 ms | 5-0 | Un cupo de ámbito por petición no limita por instancia |
| D-V2 | ¿Se construye V2? | No: se difiere con condición de reapertura (piloto V1, banco V2 construido, umbral de REQ-IA-26 fijado) | 5-0 | Estado «después del piloto V1» |
| D-FOLIOS | ¿Qué folios abren la etapa? | Providencia 005, informe 006, resolución 007; después constancias | 5-0 | Continuar sin reescribir; un `Corrige` exigiría un ancla de contradicción que no existe |
| D-COMMITS | ¿Orden de commits? | Uno por tramo, «EXP-0002 Tn» | 5-0 | Hace verificables §6.1 de la norma y A14 en la historia |
| D-NIVEL3 | ¿Cómo se muestra el nivel 3? | Divulgación progresiva: el nivel 3 sólo tras un botón explícito; el prompt es el literal de 01 §5.2, sin ajustes; el origen de cada pista se ve | 5-0 | Cambiar el prompt exige otra corrida del banco |
| D-MODELO-REAL | ¿Dónde va la prueba con modelo real? | Fuera de la CI: corrida manual rotulada como evidencia, redactada por R-PRIV | 5-0 | REQ-IA-12 es conducta, no compuerta |

## 3. Vetos

No construir V2; no tocar `IFigureValidator` ni A-18; la IA no altera observaciones ni persiste nada; al
modelo sólo viajan tipo, campo, valor declarado y sentido; el SDK de IA sólo en `Infrastructure/AI` y en la
composición, con versiones fijas; ningún proyecto nuevo; ningún valor por omisión de dirección ni
`HintsEnabled=true` en configuración, `compose` o `Dockerfile`; el arranque no falla por la sección `AI`;
con la configuración por omisión la Web no cambia su HTML; el nivel 3 nunca sin acción explícita ni con
marcado crudo; ninguna dirección, puerto, usuario ni nombre de servidor privado en el repositorio; nada de
contenido de mensajes en registros fuera de desarrollo; ninguna prueba con modelo real en la CI; no se
relajan aserciones ni umbrales; sin librerías de mocking; no se presentan las plantillas como definitivas;
no se editan el intake, `_legacy`, Audit histórico, los folios 001 a 004 ni IA.SDD.

## 4. Riesgos que la mesa deja escritos

1. El nivel 3 suele decir la operación en palabras; el filtro no lo ve. Control: se muestra a pedido y con
   origen visible; la utilidad la puntúa el docente (REQ-IA-13, fuera del alcance del agente).
2. Las plantillas son del agente: quedan rotuladas como provisionales hasta que el docente las fije.
3. Desvío de nombre respecto del plan de la mesa, declarado acá para que no sorprenda: el plan hablaba de
   `HintRequest` y `Hint`; se usan los nombres de las filas propuestas en `ai-layer/04`
   (`ObservationHintRequest`, `Hints`, `Level1..3`), que son los que la norma recibe.

Sigue: resolución con el plan por tramos (actuación 007), a cargo del coordinador.
