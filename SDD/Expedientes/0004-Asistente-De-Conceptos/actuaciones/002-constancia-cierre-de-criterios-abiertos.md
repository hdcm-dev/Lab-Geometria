# EXP-0004 · Actuación 002 — Constancia: cierre de los criterios abiertos de la verificación

| Campo | Valor |
|---|---|
| Tipo | constancia |
| Fecha | 2026-11-11 |
| Autor | banco de traslado de Lab-Documentos (agente) |
| Corrige | — (complementa la [001](001-constancia-construccion-de-la-rebanada.md); la 001 no se reescribe) |

**Expediente:** [EXP-0004](../README.md) · **Foliada:** 002 — no se reescribe

Conocimientos aplicados: los de la capa de IA de Lab-Documentos en `main` `abb2e5b` (PR #68: K3 R2.2a lectura
experta, R2.6 batería antes de la firma; K7 R5.4 con el despliegue en otro repo).

## 1. Qué se cerró

| Criterio | Qué quedó | Evidencia |
|---|---|---|
| 8 | `Concept_Inyeccion_LlevameACuentas_SinDestino`: cuatro formas de «llevame a /cuentas» (alumno y docente), con el doble del modelo devolviendo un enlace a `/cuentas`: forma `Outside`, intención `inyeccion`, sin destino, sin ruta en el texto y **sin pedido al modelo** | pruebas |
| 13 | `Concept_Apagada_PantallaSinCambios` (Integration) afirma además que no hay `gf-concept` ni `ConceptInput` en la carga ni en la vista | pruebas |
| 27 | `docs-as-code/controles/recall_conceptos.py` en la compuerta de docs, **bloquea**, con par rojo/verde (ficha ladrona, pregunta duplicada, ficha que contesta «perímetro»); `ConceptRecallTests` mide lo mismo con el enrutador real en `ci.yml`. Alumno 32/32 y ninguna 6/6; docente 28/28 y ninguna 7/7 | [E-004](../evidencia/E-004-recall-por-rol.txt) |
| 29 | `ai-layer/ensayos/bateria-conceptos-v1.jsonl` es la batería; `conceptos.py` guarda sólo el registro crudo y `veredicto_conceptos.py` recalcula el veredicto, con la **lectura docente** (`lectura-docente.jsonl`) contando: una `dicta` rechaza, una sin leer deja PENDIENTE. CI lo reproduce y corre su par rojo/verde | [E-005](../evidencia/E-005-bateria-v2-veredicto.txt) |
| 30 | Revertida la regla en `ConceptRouter.cs:125`: 14 rojas (`Frontera_PideResolver_Negativa` ×10, `PedidoDeResolver_AntesQueLaFicha` ×4); restaurada: 14 verdes | [E-003](../evidencia/E-003-mutacion-pide-resolver.txt) |
| 31 | `ConfigurationKeysManifestTests`: las claves que lee el código (propiedades de `AiOptions` en el servicio, literales `"AI:…"` en la Web) están en el `appsettings.json` de cada pieza y en `deploy/manifiesto-claves-ia.json`; el manifiesto queda apagado y sin dirección. Encontró que el `appsettings.json` del servicio no traía `KeepAlive`, `ConceptsEnabled` ni `ConceptsModelWriter`: se agregaron con su valor por omisión (apagado) | pruebas |

## 2. Batería real ×3 (segunda vuelta)

Instancia propia en contenedor (5080 adentro, publicado sólo en `127.0.0.1`), corpus de ensayo con las nueve fichas
en `vigente` **fuera del repositorio** (K3 R2.6), dirección del servidor de modelos por variable de entorno.

| Configuración | Corridas (código) | p95 | Lectura docente | Veredicto |
|---|---|---|---|---|
| redactor con modelo (`AI__ConceptsModelWriter=true`) | 24/24 · 24/24 · 24/24 | 1575 · 1618 · 1625 ms (caliente) | 0/27 leídas | PENDIENTE DE LECTURA DOCENTE |
| sólo fichas (la configuración por omisión al encender) | 24/24 · 24/24 · 24/24 | 40 · 26 · 17 ms | 0/27 leídas | PENDIENTE DE LECTURA DOCENTE |
| primera vuelta (act. 001), recalculada | 24/24 · 24/24 · 24/24 | — | 0/27 leídas | PENDIENTE DE LECTURA DOCENTE |

- **Un intento descartado y conservado.** La primera tercera corrida con modelo dio 20/24: cuatro `429` del límite de
  tasa por persona (60 por minuto), porque las corridas iban seguidas. Se conservó como
  `intento-limite-de-tasa-corrida-3.jsonl` y se repitieron las tres con una pausa de 65 s entre corridas. No es un
  defecto del asistente; sí es un dato: la batería tiene que respetar el límite de la app.
- Respuestas del modelo con dígitos: 0 de 15. Composición antes y después: 1 modelo en memoria, con aceleración.
- **Frío no medido:** el servidor de modelos es compartido y el modelo ya estaba cargado; vale el frío de la sonda.
- La versión del servicio en el registro es `0.0.0-alpha.0` porque la copia de ensayo no lleva `.git`.

## 3. Pendientes del PO (no se fingen)

1. **Firma de las nueve fichas** (`status: vigente`, H-10, criterios 3 y 4): hasta entonces el servicio real se abstiene.
2. **Lectura docente** de las 81 respuestas de ficha o modelo en `lectura-docente.jsonl` de las tres carpetas (criterios
   16 y 29): `no-dicta` o `dicta`, con `lector`. El veredicto queda PENDIENTE hasta entonces.
3. **Puntuación docente** del modelo contra la ficha (criterio 17): mientras no la haya, `AI__ConceptsModelWriter=false`.
4. **Preguntas reales de alumnos** (criterio 26): las `preguntas:` y las del docente en `medicion-conceptos.jsonl`
   las redactó el banco; reemplazarlas por las de los ensayos o de clase y repetir la medición.
5. **Encender** (`AI__ConceptsEnabled=true` en la Web y en el servicio, D-6): decisión del docente con su puntuación.
   Fusionar no enciende.

## 4. Pendientes del agente o de otro repo

- Capturas de la app servida a 1280 y 390 (criterio 20) y sesión por rol en la Web con `/estado` (criterio 21).
- Crítica de un agente distinto del autor (criterio 22) y PR con checks en verde (criterio 24).
- **Container.Lab-Geometria**: su `docker/.env-example` no nombra `AI__ConceptsEnabled` ni `AI__ConceptsModelWriter`;
  su CI tiene que contrastar la plantilla con `deploy/manifiesto-claves-ia.json` (K7 R5.4). Es otro repositorio.
