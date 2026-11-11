# EXP-0004 · Actuación 003 — Constancia: hallazgos H1–H4 de la verificación posterior a #266

| Campo | Valor |
|---|---|
| Tipo | constancia |
| Fecha | 2026-11-11 |
| Autor | banco de traslado de Lab-Documentos (agente) |
| Corrige | — (complementa la [002](002-constancia-cierre-de-criterios-abiertos.md); la 002 no se reescribe) |

**Expediente:** [EXP-0004](../README.md) · **Foliada:** 003 — no se reescribe

Base: `main` `bcb0896` (merge de #266). Rama `fix/0004-hallazgos-h1-h4`. **La IA sigue apagada por omisión**: no se
tocó ningún `appsettings.json` ni el manifiesto de claves; todo lo medido se encendió por variable de entorno en una
instancia propia.

## 1. Hallazgos y lo que se hizo

| Hallazgo | Qué se cambió | Prueba que falla con el código anterior | Evidencia |
|---|---|---|---|
| **H1 · enrutador** — «¿qué tengo que poner en Area si la arista es tres?» y «qué número va en Volumen» no caían en `pide-resolver` | `ConceptRouter`: el número en letras (dos…doce, veinte, cien, mil, medio; no «uno/una», que son artículos) cuenta como dígito junto a una magnitud; patrones «qué tengo/hay que poner/escribir/cargar», «qué pongo en», «qué número va», «qué va en el área/volumen» | `PedidoDeResolver_AntesQueLaFicha` +6 casos, `Frontera_PideResolver_Negativa` +2 casos; `NumeroEnLetras_SinMagnitud_SigueSiendoConcepto` vigila que «dos valores» no se vuelva negativa | [E-007](../evidencia/E-007-h1-pruebas-rojo-verde.txt) |
| **H1 · filtro** — el modelo dio procedimiento operativo y primera persona («no puedo generar») y el filtro no lo atrapaba (CD-6) | `ConceptAnswerFilter`: primera persona que habla de sí («no puedo», «como asistente», «lo siento», «mi función»…) y motivo nuevo `procedimiento` («primero», «tenés que», «hay que poner», imperativos con tilde: calculá, tomá, elevá, multiplicá, completá…, «en el campo») | `Concept_SalidaConOperacion_Ficha` +5 casos; `Concept_FichasPasanElFiltro` comprueba que las nueve fichas pasan el filtro endurecido (encontró «no se edita» como falso positivo de un primer borrador, corregido) | E-007 |
| **H2 · UX** — la negativa decía «abrí la ficha» y no había ficha; sobre un trabajo entregado ofrecía «Cargar un trabajo» | La negativa trae la ficha plegada (`<details>`, sin guion) y el texto dice «abrí la ficha acá abajo». El chip del pedido de código («Describir», «método», «código») sólo es «Cargar un trabajo» sin trabajo o con uno en borrador; sobre uno enviado queda «Ver pistas» si hay advertencias o ninguno | caso 15 de la tabla: daba `trabajo-nuevo` sobre el trabajo enviado; ahora `ver-pistas` | captura `h2-negativa-con-ficha-1280.png` |
| **H3 · docs-as-code** — `limite_didactico.py` daba `textos=0` sobre las fichas | Regla `ficha-dicta` sobre el cuerpo de las fichas (`topic: concepto`): dígitos, número en letras junto a una magnitud, voseo imperativo de cálculo (con tilde: «sumá» dicta, «calcula» describe), «fórmula», «al cuadrado», «primero», «tenés que». Sobre `docs-as-code`: `textos=28`, verde | `autoprueba.sh`: verde con una ficha que explica; rojo con «multiplicá la arista», «seis caras» y «primero tomá». Con el control anterior, los tres rojos dan 0 (3 FALLA) | autoprueba |
| **H4 · desbordamiento** — 36/54 salían de la tarjeta de Observaciones a 1280 | **Previo a #266**: #266 sólo agregó reglas `.gf-concept*` y el panel; ocultando el panel la tarjeta mide lo mismo (287 px) y los `dd` terminaban en 650–675 px con la tarjeta en 601. Se corrigió igual: `.gf-finding` se envuelve, `.gf-finding-body` crece, `.gf-values` con `minmax(0, max-content)` y la etiqueta se parte por palabras. Después: ningún desborde (`scrollWidth == clientWidth`), números a 580 px; a 390 sin desplazamiento horizontal | — (CSS; medido en la app servida) | `h4-observaciones-1280.png`, `h4-trabajo-390.png` |

**Batería v2.** La v1 no se reescribe: las corridas anteriores la usan y `--verificar` las sigue reproduciendo.
`bateria-conceptos-v2.jsonl` difiere sólo en el caso 15 (`trabajo-nuevo` → `ver-pistas`, por H2). Cada carpeta nueva
lleva `bateria.txt`; sin él, `veredicto_conceptos.py` usa la v1.

## 2. Pruebas

- `scripts/test.sh` en contenedor (SDK 10.0.403, usuario no root, `HOME`, `NUGET_PACKAGES` y `/tmp` montados fuera de
  `/vm`; `SkipVisorBuild=true` porque la imagen no trae Node): Domain 95/95, Application 99/99, Integration 707/708
  (1 omitida, la previa). 0 rojas.
- Con `ConceptRouter.cs` y `ConceptAnswerFilter.cs` de `bcb0896`: 13 rojas (Application 6, Integration 7).
- `docs-as-code/controles/autoprueba.sh`: verde; `veredicto_conceptos.py --verificar` reproduce las cuatro carpetas.

## 3. Batería real ×3 con el redactor de modelo

Instancia propia (servicio y Web en contenedores, publicados sólo en `127.0.0.1`), corpus de ensayo con las nueve
fichas en `vigente` **fuera del repositorio**, `AI__ConceptsEnabled=true`, `AI__ConceptsModelWriter=true`, dirección
del servidor de modelos sólo por variable de entorno. Carpeta
[`corrida-conceptos-2026-11-11-act003`](../../../../ai-layer/ensayos/corrida-conceptos-2026-11-11-act003/).

| Corrida | Código | Origen (fijo · modelo · ficha · caso de uso) | p95 |
|---|---|---|---|
| 1 | 24/24 | 13 · 5 · 4 · 2 | 2429 ms |
| 2 | 24/24 | 13 · 6 · 3 · 2 | 7166 ms |
| 3 | 24/24 | 13 · 6 · 3 · 2 | 8236 ms |

Veredicto: **PENDIENTE DE LECTURA DOCENTE** (0/27 leídas; `lectura-docente.jsonl` en plantilla).

**Sonda H1 ×3** (`sonda-h1-N.jsonl`, 14 preguntas por corrida, sin trabajo y sobre el propio): las tres formulaciones
de H1 dieron `pide-resolver`, origen `fijo`, **sin pedido al modelo**, 18/18. Ninguna respuesta del modelo con dígitos.

Motivos del redactor en toda la sesión (`motivos-del-redactor.txt`): 22 aceptadas, 13 rechazadas por el filtro
(9 `operacion`, 2 `primera-persona`, 1 `procedimiento`, 1 `numero`) y 16 **fuera de tiempo** (8 s). En todos esos casos
salió la ficha.

**Para la lectura docente** (no lo decide una expresión regular): en la corrida 2 el caso 22 dice que el rectángulo
desarrollado sale de «desplegar la tapa redonda» (concepto corrompido, LLM09) y el caso 14 describe el paralelepípedo
con caras no rectangulares, distinto de la ficha. El caso 1 dice «tres medidas», que repite la ficha y no es un valor.
Refuerza lo de la 002: mientras no haya puntuación docente, `AI__ConceptsModelWriter=false`.

## 4. Criterios 20 y 21

- **Criterio 20 · CLS:** traza de rendimiento de la vista de un trabajo con el panel encendido, a 1280: **CLS 0,00**,
  LCP 197 ms. La respuesta llega con un envío de página entera (sin guion), así que no hay corrimiento dentro de la
  página. **No hay tema oscuro**: la hoja no tiene `prefers-color-scheme` ni `data-theme`; el panel usa sólo los
  tokens del tema claro. Se declara, no se fabrica.
- **Criterio 21 · sesión docente por la Web y `/estado`:** el docente entra por `/ingreso` (cae en
  `/entrega-comision`), `/estado` muestra «IA del asistente de conceptos: Habilitada» y «Asistente de conceptos en esta
  pantalla: Encendido» (`c21-docente-estado-1280.png`); sobre el trabajo de la alumna, «¿Cuántas advertencias tiene
  este trabajo?» da «Este trabajo tiene 2 advertencias.» (`c21-docente-pregunta-1280.png`). Observación sin
  corregir: el chip que acompaña al docente dice «Volver a tu trabajo», y el trabajo no es suyo.

Capturas en [`evidencia/capturas-act-003/`](../evidencia/capturas-act-003/).

## 5. Lo que no se pudo o queda abierto

- El bundle del visor no se generó en el contenedor (sin Node): las pruebas corrieron con `SkipVisorBuild=true` y la
  Web de ensayo usó una copia del bundle ya construido.
- Los 16 «fuera de tiempo» del redactor: el servidor de modelos es compartido; p95 de 7–8 s contra un tope de 8 s.
- Sigue todo lo pendiente del PO de la 002 (firma de fichas, lectura docente, puntuación, preguntas reales, encendido).
- Texto del chip del docente («Volver a tu trabajo»), anotado arriba.
