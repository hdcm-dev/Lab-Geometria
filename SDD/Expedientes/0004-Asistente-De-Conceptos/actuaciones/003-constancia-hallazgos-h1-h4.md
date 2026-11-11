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
- ~~Texto del chip del docente («Volver a tu trabajo»), anotado arriba.~~ Corregido en el §6.

## 6. Verificación de esta actuación: cuatro defectos y lo que se hizo

La verificación posterior a `e6b0371` encontró cuatro defectos en lo de arriba. Se agregan acá (la actuación es la misma
rama y todavía no se fusionó); lo de los §1–§5 no se reescribe salvo el tachado del último punto del §5.

| Defecto | Qué se cambió | Prueba que falla con `e6b0371` | Evidencia |
|---|---|---|---|
| **1 · regresión de H1** — el número en letras junto a una magnitud mandaba a `pide-resolver` preguntas de concepto («qué es el área de las dos bases de un prisma», «por qué el volumen tiene tres dimensiones», «la altura, la que pasa por el medio», «un cubo de seis caras») | `ConceptRouter.SpokenValue`: el número en letras cuenta como dato **sólo cuando es valor de una medida** — detrás de «es/mide/vale/igual a/=», del nombre de la medida («arista tres»), de «de» («de tres por cuatro»), o delante de una unidad o de «por» y otro número — y **nunca** cuando lo sigue un sustantivo de partes (caras, bases, dimensiones, lados, aristas, medidas…). «medio» suelto no es número. El dígito sigue contando como antes | `NumeroEnLetras_CardinalDePartes_SigueSiendoConcepto`, 13 frases (las cuatro de la verificación y nueve propias): **12 rojas** con `e6b0371` (la del radio no lleva número; queda como guarda). `NumeroEnLetras_ValorDeUnaMedida_PideResolver`, 12 frases (incluida la de H1) verdes; 2 rojas con `e6b0371` («diecisiete», «trece por catorce por noventa») | [E-008](../evidencia/E-008-verificacion-rojo-verde.txt) |
| **2 · lista incompleta** | `SpokenNumber`: parser y no lista — unidades, diez a quince, dieci-, veinti-, decenas «y» unidad, cien/ciento y centenas, «mil», «y medio», «coma». «un/una» no abren número (artículos) | `SpokenNumber_Parsea` (13 casos: 13, 17, 23, 35, 99, 100, 120, 241, 1000, 2300, 3,5, 2,5) y `SpokenNumber_NoEsNumero` | E-008 |
| **3 · filtro demasiado amplio** | Corpus de respuestas buenas `ConceptFilterCorpus`: las **9 fichas**, **30 paráfrasis** propias que usan a propósito «primero», «finalmente», «deberías», «usá», «yo», «mí», «podría», «calcular» en tercera persona, y las **17 respuestas aceptadas** (origen `modelo`) de las cuatro corridas del 2026-11-11 — 56 textos. **Con `e6b0371`: 13/56 falsos positivos** (9 `procedimiento`, 3 `primera-persona`, 1 `operacion`: «dos caras vecinas»), todos en las paráfrasis; 0 en fichas y en respuestas del modelo. Ajuste: conectores de secuencia sólo pegados a un verbo de cálculo; «tenés/deberías/hay que» sólo con el verbo que dicta; fuera «usá» y el infinitivo; «yo/mí» sólo pegados a lo que el asistente haría; «podría» sólo con «no/te/le»; fuera «dos caras» de la lista de operaciones. **Después: 0/56.** Las que dictan (`Reds`, 14, incluidas las 8 de H1) siguen rechazadas: 14/14 | `Concept_FiltroSinFalsosPositivos` (roja con `e6b0371`: 13/56), `Concept_FiltroAtrapaLoQueDicta` | E-008 |
| **4 · chip del docente** | `DestinationCatalog.Resolve`: `trabajo-actual` dice «Volver a tu trabajo» al alumno y **«Volver al trabajo del alumno»** al docente; la especificación (`ai-layer/06-…`, tabla del catálogo) lo declara | `Catalogo_TrabajoActual_TextoSegunRol` (roja con `e6b0371`) | E-008 |

**Pruebas.** `scripts/test.sh` en contenedor (SDK 10.0.403, usuario no root; `HOME`, `NUGET_PACKAGES` y `/tmp` en el
disco del sistema, fuera de `/vm`; `SkipVisorBuild=true`): Domain 95/95, Application 142/142, Integration 709/710
(1 omitida, la previa). 0 rojas. Con `ConceptRouter.cs`, `DestinationCatalog.cs` y `ConceptAnswerFilter.cs` de
`e6b0371`: **16 rojas** (Application 15, Integration 1).

**Batería v2 ×3, con modelo y sin modelo.** Instancia propia del servicio en contenedor (publicada sólo en
`127.0.0.1`), corpus de ensayo con las nueve fichas en `vigente` fuera del repositorio, `AI__ConceptsEnabled=true`,
dirección del servidor de modelos sólo por variable de entorno, modelo de ADR-14012. Ningún `appsettings` ni el
manifiesto se tocaron: **la IA sigue apagada por omisión**. Un primer intento corrió las tres corridas seguidas y la
tercera dio 4 × `429` (límite de tasa por persona de la instancia, 60/min: el ensayo sin modelo es más rápido que la
ventana); se descartó y se repitió con 65 s entre corridas.

| Carpeta | Corrida | Código | Origen (fijo · modelo · ficha · caso de uso) | p95 |
|---|---|---|---|---|
| [`verif-sin-modelo`](../../../../ai-layer/ensayos/corrida-conceptos-2026-11-11-verif-sin-modelo/) (`AI__ConceptsModelWriter=false`) | 1 · 2 · 3 | 24/24 · 24/24 · 24/24 | 13 · 0 · 9 · 2 (las tres) | 42 · 15 · 13 ms |
| [`verif-modelo`](../../../../ai-layer/ensayos/corrida-conceptos-2026-11-11-verif-modelo/) (`AI__ConceptsModelWriter=true`) | 1 · 2 · 3 | 24/24 · 24/24 · 24/24 | 13 · 6 · 3 · 2 (las tres) | 1640 · 2505 · 1605 ms |

Veredicto de las dos carpetas: **PENDIENTE DE LECTURA DOCENTE** (0/27 leídas cada una; `lectura-docente.jsonl` en
plantilla). Motivos del redactor con modelo: 18 aceptadas, 9 rechazadas por el filtro (6 `operacion`, 3 `numero`),
0 `primera-persona`, 0 `procedimiento`, 0 fuera de tiempo; ninguna respuesta del modelo con dígitos.
`veredicto_conceptos.py --verificar` reproduce las seis carpetas.

**Abierto.** El dígito junto a una magnitud sigue contando siempre («el volumen tiene 3 dimensiones» va a la
negativa): la verificación habló del número en letras y no se amplió el alcance. El filtro sigue sin ver la operación
dicha con otras palabras ni el concepto corrompido: eso lo decide la lectura docente.
