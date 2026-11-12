# EXP-0005 · Actuación 002 — Constancia: crítica de diseño (7,5)

| Campo | Valor |
|---|---|
| Fecha | 2026-11-12 |
| Tipo | constancia |
| Autor | banco de traslado de Lab-Documentos (agente) |

## 1. Observaciones y resolución

1. **«Ver pistas» se ofrecía con las pistas apagadas.** La negativa (`AskConceptUseCase.Refusal`) sólo miraba si el
   trabajo tenía advertencias. Ahora `ConceptWriterSettings.HintsEnabled` toma `AI:HintsEnabled` del servicio de datos
   (la misma llave que lee la Web y que el manifiesto ya declara para las dos piezas, `false` por omisión). Apagada,
   la negativa **no ofrece el chip y no nombra las pistas** en su texto (`ConceptTexts.HintsSentence` sale).
2. **El rótulo prometía «tu advertencia» y abría la primera.** La negativa no sabe por qué advertencia se preguntó, así
   que el rótulo pasa a **«Ver pistas de la primera advertencia»**, que es lo que hace `?pistas=1` en `WorkView`.
3. **Capturas.** Las «-oscuro» de la act. 001 eran copias byte a byte de las «-claro» (no hay tema oscuro). Se
   retiran; quedan `ejemplo-1280-claro`, `ejemplo-390-claro`, `pistas-1280-claro`, `pistas-390-claro`, con SHA-256
   distintos. La act. 001 no se reescribe: esta constancia la corrige.

## 2. Pruebas y batería (E-004)

- Nueva: `Negativa_ChipDePistas_SigueAlEstadoDeLasPistas` (Integration): pistas apagadas → sin destino y sin «pistas»
  en el texto; encendidas → `ver-pistas`, rótulo «de la primera advertencia», `?pistas=1`. **Roja** con el código de
  `82eb21e` (el chip salía con las pistas apagadas). `ConceptRouter_TablaDeReferencia` corre ahora con
  `AI:HintsEnabled=true`, que es la condición en que la tabla espera `ver-pistas`.
- `scripts/test.sh` en contenedor: Domain 95/95, Application 170 + 16 omitidas, Integration 716 + 1 omitida. **0 rojas.**
- Batería v3 ×3 sin modelo, ahora con `AI__HintsEnabled=true` y sin servidor de modelos:
  [`v3-critica`](../../../ai-layer/ensayos/corrida-conceptos-2026-11-12-v3-critica/) 24/24 · 24/24 · 24/24.
  Control con la configuración por omisión: [`v3-pistas-apagadas`](../../../ai-layer/ensayos/corrida-conceptos-2026-11-12-v3-pistas-apagadas/)
  20/24 (los cuatro casos `ver-pistas`), que es lo esperado. Veredicto: PENDIENTE DE LECTURA DOCENTE.
- Ningún `appsettings` ni el manifiesto se tocaron: la IA sigue apagada por omisión.
