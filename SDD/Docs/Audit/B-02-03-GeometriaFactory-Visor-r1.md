# Informe de auditoría — Fase B, categorías 02 y 03 de GeometriaFactory-Visor (ronda 1)

| Campo | Valor |
| --- | --- |
| Producto | Fábrica de Geometría |
| Proyecto de código auditado | **GeometriaFactory-Visor** (`tipo_proyecto_codigo`: `library`) |
| Fase | B — Especificación por proyecto de código |
| Alcance | `SDD/Docs/Proyectos/GeometriaFactory-Visor/02-Especificacion-Funcional/` (10 archivos) y `.../03-UX-UI-DX/` (5 archivos). **04-Prompts-AI omitida por gating** (`usa_llm` == false): su ausencia no es hallazgo |
| Auditor | Arquitecto de Soluciones + QA Senior, invocación independiente (no participó de la emisión) |
| Fecha | 2026-08-08 |
| Ronda | r1 |
| Normativa aplicada | `Rules-Especificacion-Funcional.md` §2.1, §2.2, §3.3, §4, §6; `Rules-UX-UI-DX.md` §1.2, §1.5, §2.1, §2.2, §3.3, §4, §6; `Vocabulario-Rules.md` §9 y §10; D1–D9 |
| Insumos de verificación | `SDD/Docs/00-Contexto/`, `SDD/Docs/01-Necesidades-Negocio/` (en particular `NB-06`), `SDD/Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md` v1.2, `SDD/Docs/Audit/A-00-01-r1.md` y `A-00-01-r2.md` (sólo lectura) |

---

## Tabla de contenido

- [1. Resumen ejecutivo](#1-resumen-ejecutivo)
- [2. Matriz D1–D9 por documento](#2-matriz-d1d9-por-documento)
- [3. Matriz de estructura obligatoria por documento](#3-matriz-de-estructura-obligatoria-por-documento)
- [4. Coherencia cross-doc y gobierno del glosario](#4-coherencia-cross-doc-y-gobierno-del-glosario)
- [5. Hallazgos](#5-hallazgos)
- [6. Veredicto y condiciones para promover](#6-veredicto-y-condiciones-para-promover)

---

## 1. Resumen ejecutivo

Se auditaron los quince entregables de las categorías 02 y 03 contra §6 de sus reglas ítem por ítem, contra el upstream de nivel producto y contra el intake 1.2. **Doce hallazgos: 0 P0, 0 P1, 3 P2 y 9 P3.** El eje de la fase —la derivación de los siete códigos de condición de `Definicion-Contrato-De-Fachada.md` §6 hacia las doce entradas `E-VIS-01` a `E-VIS-12` de `DX-Error-Messages.md`— **resiste la verificación uno por uno: ningún código inventado, ninguno sin cubrir, y la función atribuida a cada entrada coincide con la del contrato salvo en un caso** (H-01). La regla de arquitectura `RA-02` se respeta sin excepción: ninguna sección de ninguno de los quince documentos atribuye al archivo de guion red, configuración propia, persistencia, identidad de la persona ni participación en decisiones de autorización. Los tres P2 son desincronizaciones internas de nomenclatura y de invariante léxica, no de contrato. **Veredicto: APROBADO CON OBSERVACIONES.**

---

## 2. Matriz D1–D9 por documento

Leyenda: **C** cumple · **O** cumple con observación · **X** no cumple.

Criterios: **D1** idioma rioplatense neutro técnico con tildes y eñes en el cuerpo · **D2** filenames ASCII · **D3** UTF-8 y LF · **D4** archivo vivo sin sufijo de versión, versión en cabecera · **D5** slug en Título-Con-Guiones · **D6** trazabilidad upstream/downstream con sección concreta · **D7** sin vocabulario del dominio fuente del framework (stacks, productos comerciales, protocolos) · **D8** tipo y variante dentro del conjunto cerrado · **D9** evidencia verificable de las afirmaciones sobre el estado del sistema.

### 2.1 Categoría 02 — Especificación Funcional

| Documento | D1 | D2 | D3 | D4 | D5 | D6 | D7 | D8 | D9 |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| `README.md` | C | C | C | C | C | **O** (H-10) | C | C | C |
| `Especificacion-Funcional.md` | C | C | C | C | C | **O** (H-10) | C | C | **O** (H-02) |
| `Definicion-Contrato-De-Fachada.md` | C | C | C | C | C | **O** (H-10) | C | C | **O** (H-01, H-09) |
| `Glosario-Funcional.md` | C | C | C | C | C | C | C | C | **O** (H-05) |
| `CU-01-Inicializar-Instancia-Del-Visor.md` | C | C | C | C | C | **O** (H-10) | C | C | C |
| `CU-02-Cargar-El-Texto-Del-Trabajo-Y-Dibujar.md` | C | C | C | C | C | C | C | C | C |
| `CU-03-Seleccionar-Una-Pieza-Por-Su-Indice.md` | C | C | C | C | C | C | C | C | C |
| `CU-04-Redimensionar-La-Escena.md` | C | C | C | C | C | **O** (H-10) | C | C | C |
| `CU-05-Destruir-La-Instancia-Y-Liberar-Recursos.md` | C | C | C | C | C | C | C | C | C |
| `CU-06-Ejercitar-La-Fachada-Sin-Backend.md` | C | C | C | C | C | C | C | C | C |

### 2.2 Categoría 03 — UX / UI / DX (variante DX)

| Documento | D1 | D2 | D3 | D4 | D5 | D6 | D7 | D8 | D9 |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| `README.md` | C | C | C | C | C | C | C | C | **O** (H-03, H-05, H-06, H-07) |
| `DX-Developer-Experience.md` | C | C | C | C | C | C | C | C | **O** (H-03) |
| `Guia-Onboarding-Developer.md` | C | C | C | C | C | C | C | C | **O** (H-06) |
| `DX-Error-Messages.md` | C | C | C | C | C | C | C | C | **O** (H-01, H-08) |
| `Glosario-UX.md` | C | C | C | C | C | C | C | C | **O** (H-04, H-05) |

### 2.3 Verificaciones globales efectivamente ejecutadas

| Verificación | Resultado |
| --- | --- |
| Codificación y fin de línea | **15 de 15 archivos UTF-8, cero retornos de carro, salto de línea final presente.** Verificado con `file --mime-encoding` y conteo de `\r` |
| Filenames ASCII con cuerpo acentuado | **Cumple.** Ningún nombre de archivo lleva tildes ni eñes; el cuerpo las lleva sistemáticamente («especificación», «guion», «diseño», «árbol») |
| Sufijo de versión en el nombre | **Cero ocurrencias.** Los quince declaran `Versión: 1.0` en cabecera; el campo `Documento:` coincide con el nombre real en los quince |
| `_legacy/` | No existe en ninguna de las dos categorías, y ambos README lo declaran así explícitamente. Correcto: no hay versiones superadas |
| D7 — dominio fuente | **Cero ocurrencias** de `Three.js`, `WebGL`, `webpack`, `TypeScript`, `JavaScript`, `Blazor`, `npm`, `Node.js`, `canvas`, `jQuery`, `Bootstrap`, `SQLite`, `JWT`, `fetch`, `XMLHttpRequest`, `WebSocket` ni `localStorage` en los quince archivos. El vocabulario neutral («archivo de guion», «motor de dibujo tridimensional», «capacidad gráfica tridimensional», «elemento de dibujo», «entorno de desarrollo contenido») se sostiene sin filtración. Las únicas cadenas literales son los cinco nombres de función y los siete códigos —ambos fijados por el intake— y los dos guiones de construcción de §16 |
| Choque de vocabulario (`Vision-Producto.md` §9.3) | **Cumple.** Cero ocurrencias de «proyecto» a secas: las 20 ocurrencias de la raíz son todas «proyecto de código». Cero ocurrencias de «solución» designando el agrupador |
| Estructura de carpetas | **Correcta.** Las dos categorías viven bajo `SDD/Docs/Proyectos/GeometriaFactory-Visor/`, no en la raíz de `SDD/Docs/` |
| Convención de nombre del proyecto de código | La identidad en minúscula con guiones y la carpeta fuera de `src/` son **excepción declarada y fundamentada** en intake §13, «Excepción declarada para GeometriaFactory-Visor», puntos 1 y 2. **No se reporta** |
| D9 sobre afirmaciones de diseño | Se auditó D9 **sólo** sobre afirmaciones acerca del estado del sistema. Las afirmaciones de diseño —«la fachada garantiza», «la instancia queda viva»— no se convirtieron en hallazgo por no citar evidencia: esta fase es de especificación y no hay sistema construido |

---

## 3. Matriz de estructura obligatoria por documento

### 3.1 Categoría 02, contra `Rules-Especificacion-Funcional.md` §2.1, §2.2 y §4

| Documento | Exigencia | Resultado |
| --- | --- | --- |
| `Especificacion-Funcional.md` | Obligatorio para los ocho tipos D8; índice maestro de CU y matriz NB→CU→RN→US | **Cumple.** §3 catálogo de seis CU, §5.1 matriz de cinco filas con la columna RN vacía y su motivo en §5.2, §5.3 cobertura declarada de las ocho NB del producto |
| Mínimo de CU para `library` (§2.2) | 5 | **Cumple: 6** (`CU-01` a `CU-06`), numeración contigua |
| Cabecera §4.1 en cada CU | H1 + Proyecto de código, Documento, Versión, Estado, Fecha, Autor | **Cumple en los seis** |
| Once secciones obligatorias §4.2 en cada CU | Propósito, Actores, Precondiciones, Flujo principal, Flujos alternativos, Excepciones, Postcondiciones, CA, Trazabilidad, Notas, Control de cambios | **Cumple en los seis, en orden y sin faltantes** |
| Mínimo tres CA Given/When/Then con valores concretos | ≥3 | **Cumple.** CU-01: 5 · CU-02: 7 · CU-03: 5 · CU-04: 5 · CU-05: 5 · CU-06: 6. Con valores concretos verificables (800 × 600, 400 × 300, 1200 × 400, índices 0 a 5, 10 recorridos, 0 peticiones) |
| Al menos una excepción por CU (anti-patrón §4.5) | ≥1 | **Cumple.** CU-01: 2 · CU-02: 4 · CU-03: 2 · CU-04: 2 · CU-05: 1 · CU-06: 4 |
| Un solo actor primario por CU (anti-patrón §4.5) | 1 | **Cumple en los seis**: «componente anfitrión» (en CU-06, «componente anfitrión mínimo») |
| `Definicion-<Concepto>.md` (§2.1, opcional) | Admitido para `library` con superficie estrecha | **Cumple.** `Definicion-Contrato-De-Fachada.md`, con justificación explícita en `Especificacion-Funcional.md` §4 |
| `Glosario-Funcional.md`, cinco secciones §4.2.4 | Cabecera, tabla, polisemia, referenciados, control de cambios | **Cumple.** §2 tabla (20 filas, no vacía), §3 polisemia con verificación negativa declarada, §4 referenciados con puntero único, §5 control de cambios. Ver H-05 sobre el conteo |
| `Reglas-De-Negocio/RN-XX` | No obligatorias para `library` (§2.2) | **Omisión declarada con motivo** en `README.md` §3 y `Especificacion-Funcional.md` §5.2 y §7. Correcto |
| `Modelo-Datos/Modelo-Conceptual.md` y `RC-XX` | Omitir para `library` puro sin estado (§2.1, §2.2) | **Omisión declarada con motivo** en `README.md` §3 y `Especificacion-Funcional.md` §7, con el flag `tiene_persistencia` = false como fundamento. Correcto |
| `README.md` de sección (§3.4) | Recomendado; lista artefactos con propósito y estado | **Cumple.** Nueve documentos con propósito de una línea y estado `Propuesto` |
| Secciones opcionales §4.3 | «Compatibilidad de versión pública, sólo para `library`» | Presente como §7 del documento de concepto. Ver H-12 |
| Tabla de contenido (>3 secciones de primer nivel) | Obligatoria | **Cumple en los diez**, inmediatamente después de la cabecera, con anclas de primer y segundo nivel |

### 3.2 Categoría 03, contra `Rules-UX-UI-DX.md` §2.1, §2.2 y §4 — variante DX

| Documento | Exigencia | Resultado |
| --- | --- | --- |
| `DX-Developer-Experience.md` | Obligatorio para `library`; nueve secciones §4.2.3 | **Cumple exacto y en orden**: 1 Rol de intervención · 2 Onboarding por tramos · 3 Quick-start · 4 Diátaxis · 5 Mensajes de error · 6 Métricas DX · 7 Feedback loop · 8 Trazabilidad · 9 Control de cambios |
| `Guia-Onboarding-Developer.md` | Obligatorio para `library`; seis secciones §4.2.4 | **Cumple**: §1 Rol de intervención y prerrequisitos (equivale a «audiencia y prerrequisitos», sustitución de término declarada en `Glosario-UX.md` §2.1) · §2 Acceso al archivo de guion · §3 Primer ejemplo ejecutable · §4 Diagnóstico de problemas frecuentes · §5 Próximos pasos · §7 Control de cambios. §6 Trazabilidad es sección adicional admisible |
| `DX-Error-Messages.md` | Obligatorio para `library`; seis secciones §4.2.5 | **Cumple con adaptación declarada**: §1 Principios · §2 Taxonomía · §3 Catálogo · §5 Tono y voz · §6 Localización · §8 Control de cambios. §4 y §7 son adicionales. La columna «mensaje» que pide §4.2.5.3 **no existe y su ausencia está fundada**: §1 principio 3 y §6.2 declaran que la fachada emite código y el anfitrión compone la frase. Las columnas «Qué pasó / Por qué pasó / Qué hacer» cubren la semántica exigida. **No se reporta** |
| `Glosario-UX.md` | Obligatorio para los ocho tipos D8, también en DX | **Cumple.** §2 con 19 términos en cuatro tablas (conteo propio verificado y correcto), §3 polisemia, §4 referenciados en tres capas, §5 control de cambios |
| Mínimo de wireframes para `library` (§2.2) | **0** | **Correcto.** La ausencia no es hallazgo, y el `README.md` §3 **declara la omisión con su motivo**: «Omitidos por no haber superficies que dibujar… hay cinco funciones». Requisito cumplido |
| `Experiencia-De-Uso.md` | Omitir para `library` (§2.1) | **Omisión declarada con motivo** en `README.md` §3, con remisión a `DX-Developer-Experience.md` como equivalente DX |
| `DX-Portal-Developers.md` | Omitir para tipos sin portal | **Omisión declarada con motivo** (`tiene_portal_developers` = false, `redistribuible` = false) |
| `DX-Operability.md` | Obligatorio sólo para `worker-service` | **Omisión declarada con motivo** |
| `representacion-<concepto>.md` | Condicional | **Declarado «no aplica» con motivo** |
| `Linea-Base-Visual.md`, `Contrato-Datos-Maqueta.md`, `Bitacora-Validacion-Maqueta.md` | `requiere_maqueta` = true, Fase B2 no corrió | **Ausencia correcta y correctamente calificada.** `README.md` §4 «Artefactos previstos para la Fase B2» los declara **previstos, no omitidos**, con la distinción explícita («un artefacto omitido no vuelve y éstos sí»), el emisor (AG-03M) y la carpeta de destino. `DX-Developer-Experience.md` §8 los repite como «Prevista para la Fase B2». **Requisito satisfecho sin reserva** |
| `README.md` de sección (§3.4) | Recomendado; artefactos, variante y estado | **Cumple**, y suma §6 de autoverificación ítem por ítem contra los dieciocho criterios de §6 |
| Cabecera §4.1 con `Variante:` | Obligatoria en DX | **Cumple en los cinco**: `Variante: DX` |
| Tabla de contenido | Obligatoria | **Cumple en los cinco** |

### 3.3 Autoverificación de `README.md` §6 de 03 contra `Rules-UX-UI-DX.md` §6, contraverificada

Se recorrieron los dieciocho criterios de §6 de forma independiente y se contrastaron contra lo que el README declara. **La numeración y el reparto son correctos: los 18 ítems del README corresponden uno a uno con los 18 del checklist de la regla.**

| # | Criterio | Declara el README | Contraverificación del auditor |
| --- | --- | --- | --- |
| 1 | Variante declarada y coherente con D8 | Cumple | **Confirmado** |
| 2 | `Experiencia-De-Uso.md` con once secciones | No aplicable (UX/UI) | **Confirmado**: `tiene_ui_final` = false |
| 3 | Wireframes con nueve secciones | No aplicable (UX/UI) | **Confirmado**: mínimo 0 para `library` |
| 4 | `DX-Developer-Experience.md` con nueve secciones §4.2.3 | Cumple | **Confirmado**, secciones verificadas una a una |
| 5 | WCAG 2.2 AA como piso | No aplicable (UX/UI) | **Confirmado**: el criterio es condicional («toda accesibilidad declarada») y no hay antecedente. El razonamiento del README es correcto |
| 6 | Estados por wireframe | No aplicable (UX/UI) | **Confirmado** |
| 7 | Quick-start verificable y reproducible | Cumple | **Confirmado**: `DX-Developer-Experience.md` §3.2, cinco pasos con resultado observable; `Guia-Onboarding-Developer.md` §3.1 lo ejecuta |
| 8 | Trazabilidad upstream y downstream | Cumple | **Confirmado en los cinco**, con secciones concretas |
| 9 | Sin sufijo de versión; versión en cabecera; Título-Con-Guiones | Cumple | **Confirmado** |
| 10 | Un archivo por nombre lógico; `_legacy/` | Cumple | **Confirmado** |
| 11 | `Glosario-UX.md` con tabla no vacía | Cumple | **Confirmado**: 19 términos |
| 12 | Todo término en más de un artefacto, en el glosario | Cumple | **Confirmado**: columna «artefactos donde aparece» en cada fila |
| 13 | No duplica términos de 02; los reusados se referencian | Cumple | **Confirmado en sustancia**; el numeral de la afirmación es incorrecto (H-05) |
| 14 | Ninguna polisemia con contextos disjuntos reportada ni sobrecorregida | Cumple | **Confirmado en su mitad negativa** —no hay sobrecorrección— pero **la parte positiva no se sostiene**: la invariante de «recorrido» que la fila invoca está incumplida (H-03) y su evidencia de colisión no es verificable (H-04) |
| 15 | Sin stacks concretos ni protocolos del dominio fuente | Cumple | **Confirmado**: cero ocurrencias |
| 16 | `requiere_maqueta` = true: nombre canónico por wireframe y estados | No aplicable (UX/UI) | **Confirmado**: no hay wireframes que declaren superficie |
| 17 | Maqueta ya aprobada: artefactos retroalimentados y tres documentos presentes | **No aplicable todavía** | **Confirmado y correcto.** La Fase B2 no corrió; la calificación «no aplicable todavía» —distinta de «no aplicable» y de «incumple»— es exactamente la que corresponde |
| 18 | Tabla de contenido | Cumple | **Confirmado** |

---

## 4. Coherencia cross-doc y gobierno del glosario

### 4.1 Verificación uno por uno de los siete códigos de condición, de 02 a 03

`Definicion-Contrato-De-Fachada.md` §6 fija **siete** códigos. `DX-Error-Messages.md` §3 los desarrolla en **doce** entradas y declara la derivación en su §3.5. Se verificó cada código en las tres capas —contrato §6, caso de uso que lo declara, entrada de catálogo— comparando función atribuida, causa y efecto sobre la instancia.

| # | Código del contrato §6 | Entradas de 03 | Función atribuida por 03 | Función y semántica fijadas por 02 | Veredicto |
| --- | --- | --- | --- | --- | --- |
| 1 | `CAPACIDAD_GRAFICA_AUSENTE` | `E-VIS-01` | `inicializar` | Contrato §6 + `CU-01` §6 y CA-03: no se crea instancia, no hay identificador | **Coincide** |
| 2 | `ELEMENTO_DE_DIBUJO_INVALIDO` | `E-VIS-02` | `inicializar` | Contrato §6 («el elemento recibido por `inicializar`») + `CU-01` §6 y CA-04 | **Coincide** |
| 2b | `ELEMENTO_DE_DIBUJO_INVALIDO` | `E-VIS-07` | `redimensionar` | `CU-04` §6 sí lo declara para `redimensionar`; **el contrato §6 no**, y su columna de efecto dice «No se crea instancia», que es falso en este curso | **Desincronizado → H-01 (P2)** |
| 3 | `INSTANCIA_DESCONOCIDA` | `E-VIS-03`, `E-VIS-04`, `E-VIS-05`, `E-VIS-06` | `cargarJson`, `seleccionarPieza`, `redimensionar`, `destruir` | Contrato §5.1 punto 3 y §6; `CU-02` §6, `CU-03` §6, `CU-04` §6, `CU-05` §6 y CA-02 | **Coincide en las cuatro.** La exclusión de `inicializar`, justificada en §3.5 de 03 por ser la única función sin identificador, es correcta contra contrato §3.1 regla 1 |
| 4 | `TEXTO_NO_LEGIBLE` | `E-VIS-08` | `cargarJson` | Contrato §6 («la instancia queda viva y vacía») + `CU-02` §6 y CA-06 | **Coincide**, incluido el efecto «viva y vacía» y la prohibición de presentarlo como veredicto sobre el trabajo |
| 5 | `TIPO_NO_DIBUJABLE` | `E-VIS-09` | `cargarJson`, por pieza | Contrato §6 y §5.3 + `CU-02` FA-02, §6 y CA-05 | **Coincide.** Los seis tipos dibujables se enumeran idénticos en las tres capas: `Cilindro`, `Cubo`, `Ortoedro`, `Rectangulo`, `Cuadrado`, `Circulo` |
| 6 | `DIMENSION_NO_LEGIBLE` | `E-VIS-10` | `cargarJson`, por pieza | Contrato §6 + `CU-02` §6 | **Coincide** |
| 7 | `INDICE_FUERA_DE_RANGO` | `E-VIS-11`, `E-VIS-12` | `seleccionarPieza` | Contrato §6 + `CU-03` §6 y CA-03 (`E-VIS-11`); `CU-03` §5 FA-02 (`E-VIS-12`) | **Coincide en `E-VIS-11`.** En `E-VIS-12` el encaje es correcto en intención pero fuerza el enunciado literal del contrato → **H-09 (P3)** |

Verificaciones de cierre:

- **Ningún código inventado.** Se buscaron cadenas en mayúscula con guion bajo en los cinco artefactos de 03: aparecen exactamente los siete del contrato y ninguno más. `DX-Error-Messages.md` §1 lo declara («Son siete códigos, ni uno más») y §3 lo sostiene.
- **Ningún código sin cubrir.** Los siete tienen al menos una entrada. La tabla §3.5 de 03 —«7 de 7 códigos cubiertos, en 12 entradas»— es **exacta**, contada fila por fila.
- **Ninguna entrada huérfana.** Las doce entradas remiten a un código del contrato; ninguna a un código propio.
- **El mecanismo de contención está declarado.** `DX-Error-Messages.md` §3, nota sobre `E-VIS-12`, y `DX-Developer-Experience.md` §7, vía 2, fijan que un código nuevo se pide a 02 y no se inventa en 03. Es la salvaguarda correcta.
- **Coherencia de los conjuntos numéricos del contrato**: cinco funciones (§4.1–§4.5), siete garantías (§3.2 G-1 a G-7), seis prohibiciones (§3.3, seis filas), cuatro elementos del concepto (§5.1–§5.4), siete códigos (§6). **Los cinco conteos verificados fila por fila y correctos**, y repetidos sin desvío en `Especificacion-Funcional.md` §4, en `README.md` de 03 §1 y en `Glosario-UX.md` §2.1.

### 4.2 Fidelidad al upstream y a los escenarios del intake 1.2

| Punto verificado | Resultado |
| --- | --- |
| **Numeración local de los CU** | **Correcta y declarada.** `Especificacion-Funcional.md` §3.2 explica que `CU-01` a `CU-06` es numeración propia del proyecto de código y que `CU-15`, `CU-16` y `CU-17` son la serie de nivel producto anticipada por `NB-06`. §5.1 declara la correspondencia fila por fila. Contraverificado contra `NB-06` §7 y `Necesidades-Negocio.md` §3.3: `CU-15`, `CU-16` y `CU-17` quedan todas asignadas, y `CU-12` de `NB-04` queda asignada parcialmente con su recorte explícito. **Ningún identificador cuelga** |
| **E-1, números y propiedades del intake 1.2** | **Correctos en los seis artefactos que lo citan.** `CU-02` CA-01: «3 piezas dibujadas con los índices 0, 1 y 2, incluido el ortoedro, y 0 piezas no dibujadas». `Especificacion-Funcional.md` §6: «tres piezas, `Cilindro`, `Cubo` y `Ortoedro`, con el ortoedro dibujado». `Glosario-UX.md` §4.3, `DX-Developer-Experience.md` §2 y `Guia-Onboarding-Developer.md` §3.3 paso 3: «3 piezas, ortoedro incluido». Contrastado contra intake §20.E-1 punto 7 |
| **E-1 no ejercita las tolerancias del formato** | **Declarado correctamente.** `CU-02` §10: «tiene su texto editado a mano —nombra las bases del ortoedro con una clave distinta de la que emite el programa del alumno y no trae comas finales—, de modo que ejercita el camino feliz de la lectura de claves y **no** las tolerancias del formato. Las trampas del formato las ejercita E-2». Idéntico en `Especificacion-Funcional.md` §6. Contrastado contra intake §20.E-1, párrafo «Qué ejercita». **Ninguna cita a los números anteriores** |
| **E-7, seis tipos y lectura del ortoedro** | **Correctos.** `CU-02` CA-02 (6 piezas, índices 0 a 5) y CA-03 («bases de 6.00 × 4.00 y laterales de altura 8.00 … ancho 6, profundidad 4, altura 8, coherente con el volumen declarado de 192.00») reproducen literalmente la verificación de intake §20.E-7. Contrato §5.3 lo generaliza sin desviarse |
| **`NB-06` §5, criterios por ordinal tras la renumeración de la Fase A** | **Correctos en las cinco citas.** `CU-03` §9 (quinto, sincronización), `CU-05` §9 (tercero, 10 recorridos), `CU-06` §9 (segundo, tercero, cuarto y quinto), `Guia-Onboarding-Developer.md` §6 (primero, segundo, tercero, cuarto). Contrastado contra `NB-06` §5 en su versión de siete criterios post-H-04. **Ninguna referencia por ordinal quedó rota** |
| **`NB-06` §4, ortoedros que no se dibujan** | Cita fiel. `DX-Error-Messages.md` §7 la invoca correctamente: `NB-06` §4 dice «Los ortoedros generados por la aplicación del alumno no se dibujan, y su ausencia no produce ningún mensaje». **No hay contradicción con E-1**, cuyo texto editado a mano usa la clave alternativa y sí se dibuja |
| **Regla `RA-02` — visualizador puro** | **Respetada sin excepción en los quince documentos.** No hay una sola sección que atribuya al archivo de guion petición de red, lectura de configuración propia, escritura en el almacenamiento del navegador, conocimiento de la persona o participación en decisión de autorización. Al contrario: contrato §3.2 G-1 a G-3, §3.3 filas 1, 2 y 5; `Especificacion-Funcional.md` §2; `README.md` de 03 §5; `DX-Error-Messages.md` §2.2 (categorías «Error de autorización» y «Recurso ausente» declaradas ausentes con motivo); `DX-Developer-Experience.md` §1.3 enunciado 2 y §8 (cuatro filas «N/A» con fundamento). **El gate de cero red se declara con umbral exacto 0 en siete artefactos.** Ningún hallazgo de gravedad por esta vía |
| **Sin integrador externo** | **Correctamente resuelto.** `DX-Developer-Experience.md` §1.2 lo funda en `redistribuible` = false y en los dos consumidores internos, y nombra a los dos lectores reales (el propio developer y el agente de IA). No es hallazgo |
| **Aspecto del resaltado diferido de `CU-03` §10 a 03** | **Cerrado correctamente.** `README.md` de 03 §4 deja constancia de que la categoría no fija valores visuales —variante DX y anti-patrón de wireframe con detalle visual— y difiere los valores a la Fase B2, fijando en cambio la propiedad de contrato consumible. No queda un diferimiento sin destinatario |

### 4.3 Gobierno del glosario — `Vocabulario-Rules.md` §10, cuatro criterios

Las tres capas de glosario son `Vision-Producto.md` §9 (raíz), `Glosario-Funcional.md` de 02 y `Glosario-UX.md` de 03. La regla es referenciar, no redefinir.

| Criterio | Resultado |
| --- | --- |
| **Sin contradicciones** | **Cumple.** Se contrastaron los 13 términos de `Glosario-Funcional.md` §4 y los 7 de `Glosario-UX.md` §4.2 contra `Vision-Producto.md` §9.1 y §9.2, uno por uno: ninguno redefine, todos remiten con puntero único y ninguno altera la semántica raíz. `Glosario-UX.md` §4.1 referencia los términos de 02 sin duplicar ninguna definición |
| **Completitud** | **Cumple.** Todo término que aparece en más de un artefacto de cada categoría está declarado con su columna «artefactos donde aparece». La regla de inclusión está enunciada en `Glosario-Funcional.md` §1 y en `Glosario-UX.md` §1. La única observación es de conteo, no de completitud (H-05) |
| **Polisemia gobernada**, con la sección como contexto de lectura | **Cumple parcialmente.** «Pieza» está gobernada correctamente: `Glosario-Funcional.md` §3.1 adopta sin reabrir la resolución del glosario raíz (`Vision-Producto.md` §9.2, corrección H-01 de `A-00-01-r1.md`, cita contraverificada y exacta), reserva la forma desnuda al referente del dominio y declara que el segundo referente no aparece en los artefactos. Verificado: **cero formas desnudas de «pieza» con el referente del artefacto desplegable** en los quince archivos. «Recorrido» está declarada con forma calificada obligatoria en `Glosario-UX.md` §3.1, pero **la invariante está incumplida en la propia categoría** (H-03) y su evidencia de colisión no se sostiene (H-04) |
| **Criterio negativo** — ninguna polisemia con contextos disjuntos reportada como defecto ni sobrecorregida | **Cumple, y este informe lo respeta.** Ver §4.4 |

Tres decisiones de vocabulario cerradas en la Fase A, verificadas en los quince artefactos:

1. **«Trabajo» no es «unidad de entrega».** `Especificacion-Funcional.md` §8 punto 1 y `Glosario-UX.md` §4.2 lo declaran explícitamente. **Cero ocurrencias** de «trabajo» usado como término normativo. **Cumple.**
2. **«Pieza» con dos referentes, el segundo siempre calificado.** Verificado por barrido: todas las ocurrencias desnudas designan una figura del conjunto raíz. El segundo referente sólo aparece en las entradas de glosario que lo declaran, siempre calificado. **Cumple.**
3. **«Observación» superordinado de «advertencia» y «error de validación», y este proyecto de código no emite ninguna de las tres.** Declarado en `Especificacion-Funcional.md` §2 y §8 punto 3, `Glosario-Funcional.md` §4, `Glosario-UX.md` §4.2, `DX-Error-Messages.md` §2.2 y §5, `DX-Developer-Experience.md` §4.3, `CU-02` §6 y §10, `CU-06` §6. **Ninguna condición de contrato se nombra «advertencia» ni «error de validación» en ninguno de los quince archivos. Cumple sin reserva.**

### 4.4 Polisemias evaluadas y descartadas — enumeración obligatoria

Se evaluaron los candidatos siguientes y **se descartaron todos**. Reportarlos habría sido el defecto de informe que `Vocabulario-Rules.md` §10 tipifica.

| Candidato | Referentes considerados | Por qué se descarta |
| --- | --- | --- |
| **«Escena»** | Espacio tridimensional de una instancia (02 §2) · cualquier otro sentido del corpus | **Descartado.** Contextos disjuntos: no hay segundo referente en el corpus del producto. `Glosario-Funcional.md` §3.3 y `Glosario-UX.md` §3.3 lo resolvieron así y **acertaron**. Exigir su calificación sería falso positivo |
| **«Malla»** | Representación tridimensional de una pieza dibujable | **Descartado**, mismo fundamento. Un único referente en todo el corpus |
| **«Árbol»** | Presentación colapsable de la estructura del texto | **Descartado.** El único otro uso posible —estructura de datos genérica— no aparece en el corpus. Contextos disjuntos |
| **«Instancia»** | Instancia del visor (forma corta declarada) · instancia desplegable de un servicio | **Descartado.** El segundo referente **no aparece** en ninguno de los quince artefactos; la única mención vecina, `DX-Developer-Experience.md` §8 («No hay instancia desplegable de este proyecto de código»), es una negación explícita que no compite. Contextos disjuntos. `Glosario-UX.md` §3.3 adopta la resolución de 02 sin reabrirla, que es lo correcto |
| **«Componente»** | Figura plana de una pieza —tapa, cara, base, lateral, lado— en forma desnuda · «componente anfitrión», siempre calificado · «componente del producto» | **Descartado.** Los dos glosarios lo resuelven explícitamente con la advertencia «No confundir con “componente anfitrión”» (`Glosario-Funcional.md` §4 y `Glosario-UX.md` §4.2). El referente del anfitrión **nunca aparece desnudo** en los quince archivos; el referente de la figura plana sólo aparece desnudo en secciones donde el otro no está presente. No hay sección con colisión: no corresponde corregir |
| **«Resultado de la interpretación» / «resultado de dibujo»** | Retorno de `cargarJson` · retorno del backend con observaciones | **Descartado como polisemia.** `Glosario-Funcional.md` §3.2 lo trata correctamente como **precisión de nombre para no crear una polisemia**, no como polisemia declarada. La distinción está bien planteada |
| **«Reference»** | Modo de Diátaxis · referencia bibliográfica o trazabilidad | **Descartado.** `Glosario-UX.md` §3.2 lo resuelve con calificador obligatorio («modo reference») y declara que no es polisemia sino precisión. Verificado: los cuatro modos aparecen siempre calificados |
| **«Selección» / «resaltado»** | Estado de a lo sumo una pieza resaltada · acto de elegir en el árbol | **Descartado.** El glosario los declara sinónimo y efecto visible del mismo referente, no referentes distintos |
| **«Condición»** | Código de condición de la fachada · precondición de un CU | **Descartado.** «Precondición» y «condición de contrato» son formas distintas y no comparten sección: los CU usan «precondiciones» sólo en su §3 y «condición» con el sentido del contrato en §6 |
| **«Tapa»** | Círculo del cilindro · uso erróneo del emisor para las bases del ortoedro | **Descartado**, y ya gobernado aguas arriba: `Vision-Producto.md` §9.1 lo declara «polisemia del dominio del emisor, no del producto», y `A-00-01-r1.md` lo dio por cumplido. `Glosario-Funcional.md` §4 lo referencia sin reabrirlo |
| **«Recorrido»** | De integración · de ida y vuelta entre trabajos | **No descartado**: es la única desambiguación nueva de la categoría 03 y su gobierno **falla en la ejecución**. Ver H-03 y H-04 |

---

## 5. Hallazgos

**Total: 12 — P0: 0 · P1: 0 · P2: 3 · P3: 9.**

### H-01 · **P2** · `ELEMENTO_DE_DIBUJO_INVALIDO` en `redimensionar` no está en el contrato §6, que es la fuente única declarada

- **Archivo y sección.** `02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md` §6, línea 211; con impacto en `02-Especificacion-Funcional/Casos-De-Uso/CU-04-Redimensionar-La-Escena.md` §6 y en `03-UX-UI-DX/DX-Error-Messages.md` §3.3 entrada `E-VIS-07` (línea 109) y §3.5 (línea 128).
- **Evidencia.** El contrato §6 declara: «`ELEMENTO_DE_DIBUJO_INVALIDO` | El elemento recibido por **`inicializar`** no sirve como superficie de dibujo, o tiene tamaño nulo | **No se crea instancia**». `CU-04` §6 lo usa para otra función y con otro efecto: «El elemento de dibujo de la instancia pasó a tener tamaño cero, por ejemplo porque quedó oculto | No se recalcula nada, **la instancia sigue viva** con su escena y su selección». `DX-Error-Messages.md` `E-VIS-07` deriva de ahí: «`redimensionar` | No se recalculó la relación de aspecto. **La instancia sigue viva**, con su escena y su selección intactas». El contrato §4.4, «Qué no hace» de `redimensionar`, tampoco menciona esta condición. La columna «Efecto sobre la instancia» de §6 es, por lo tanto, **falsa para uno de los dos cursos** en que el código se produce.
- **Por qué importa.** `DX-Error-Messages.md` §1 y su cabecera declaran a §6 del contrato «fuente única de este catálogo», y §3.5 se presenta como tabla de cobertura contra esa fuente. La entrada `E-VIS-07` no es derivable de §6 tal como está escrita: se derivó de `CU-04`. Es exactamente el punto donde una fase de dos categorías se desincroniza, y 08-Calidad-Y-Pruebas va a escribir su aserción contra §6.
- **Recomendación.** Corregir `Definicion-Contrato-De-Fachada.md` §6, fila `ELEMENTO_DE_DIBUJO_INVALIDO`, para que la columna «Cuándo se produce» cubra los dos cursos —el elemento recibido por `inicializar`, y el elemento de una instancia viva que pasó a tamaño cero al invocar `redimensionar`— y para que la columna «Efecto sobre la instancia» los distinga («no se crea instancia» en el primero; «la instancia sigue viva, con su escena y su selección» en el segundo). Sumar la mención en §4.4. No tocar `CU-04` ni `E-VIS-07`, que ya son correctos.

### H-02 · **P2** · El conjunto de «cinco propiedades transversales» se enuncia con dos membresías distintas dentro de 02

- **Archivo y sección.** `02-Especificacion-Funcional/Especificacion-Funcional.md` §6 (líneas 119–125) contra §3.1 punto 2 (línea 69), `Definicion-Contrato-De-Fachada.md` §4.5 (línea 165) y `Casos-De-Uso/CU-06-Ejercitar-La-Fachada-Sin-Backend.md` §1.
- **Evidencia.** Tres lugares nombran el mismo conjunto de cinco: «cero red, cero persistencia, determinismo, liberación de recursos y **ausencia de fallo silencioso**» (`Especificacion-Funcional.md` §3.1 punto 2; contrato §4.5; `CU-06` §1). Pero la tabla de `Especificacion-Funcional.md` §6, que es la que **declara los umbrales «para que 08-Calidad-Y-Pruebas las tome como están»**, lista otras cinco: Cero red · Cero persistencia · **Se ejercita sin backend** · Disposición determinista · Liberación de recursos. «Ausencia de fallo silencioso» —la garantía G-5 del contrato, que es la que cierra el problema original de `NB-06`— **no tiene fila ni umbral en §6**, y «se ejercita sin backend» no figura en el conjunto que los otros tres lugares enumeran.
- **Por qué importa.** La categoría 08 va a derivar sus pruebas de la tabla de §6. Tal como está, la propiedad que materializa la eliminación del fallo silencioso queda sin umbral declarado en el único lugar donde los umbrales se declaran.
- **Recomendación.** Unificar el conjunto: agregar a `Especificacion-Funcional.md` §6 una fila «Ausencia de fallo silencioso» con su umbral verificable —por ejemplo, «100 % de las piezas no dibujadas enumeradas con su índice en el resultado de dibujo», con `CU-02` CA-05 y `CU-06` CA-01 como lugar de verificación— y ajustar el enunciado de §3.1 punto 2, del contrato §4.5 y de `CU-06` §1 para que nombren las **seis** propiedades, o bien para que declaren explícitamente que «se ejercita sin backend» es la propiedad contenedora de las otras cinco.

### H-03 · **P2** · La invariante «la forma desnuda “recorrido” no se usa en esta categoría» está incumplida en la propia categoría

- **Archivo y sección.** `03-UX-UI-DX/Glosario-UX.md` §3.1 (línea 98); incumplida en `03-UX-UI-DX/DX-Developer-Experience.md` §3.3, §6 y §7, `03-UX-UI-DX/Guia-Onboarding-Developer.md` §1.2, §3.3, §6 y §7, y `03-UX-UI-DX/DX-Error-Messages.md` §4.
- **Evidencia.** `Glosario-UX.md` §3.1 declara: «**La forma desnuda «recorrido» no se usa en esta categoría.** Es exactamente el corolario de `Vocabulario-Rules.md` §9.2: cuando conviven dos formas calificadas, el término desnudo es el defecto». Ocurrencias desnudas verificadas por barrido:
  1. `DX-Developer-Experience.md` §3.3, **en el título de sección**: «Recorrido mínimo de la fachada que el paso 4 ejerce».
  2. `DX-Developer-Experience.md` §3.3: «Tres reglas **del recorrido** que un integrador nuevo suele romper».
  3. `DX-Developer-Experience.md` §6, definición de la métrica de peticiones: «Peticiones contadas en la pestaña de red **durante el recorrido completo**».
  4. `DX-Developer-Experience.md` §6, columna «Cómo se mide» de la tasa de error de onboarding: «**Recorrido del onboarding** registrado en la bitácora de la etapa».
  5. `DX-Developer-Experience.md` §7, primera vía: «**Recorrido del quick-start** al cerrar cada etapa que toca el visor».
  6. `DX-Developer-Experience.md` §8: «**recorrido del quick-start** como prueba de humo».
  7. `Guia-Onboarding-Developer.md` §1.2: «entonces **el recorrido** deja de demostrar lo que vino a demostrar».
  8. `Guia-Onboarding-Developer.md` §3.3 paso 5: «**Los diez recorridos** terminan con todas sus piezas dibujadas».
  9. `Guia-Onboarding-Developer.md` §6 y §7, y `DX-Error-Messages.md` §4: «**durante el recorrido**», «US de **recorrido** de verificación del contrato», «los tramos de §3 como **recorrido** de humo».
  Además, `README.md` §6 criterio 14 declara **«Cumple»** para el gobierno de esta polisemia, y `README.md` §5 punto 4 afirma que el glosario «es donde se resuelve por qué «recorrido» **nunca aparece** en su forma desnuda».
- **Por qué importa.** No es una cuestión de estilo: la invariante fue elegida por el propio entregable como **calificada obligatoria** en lugar de mera entrada de glosario, precisamente porque el glosario «no alcanza a resolver la lectura». Una invariante declarada y no cumplida es peor que no declararla, y la autoverificación la da por cumplida.
- **Recomendación.** Dos caminos, y el segundo es el más barato. **(a)** Calificar por ocurrencia —nunca por sustitución global, `Vocabulario-Rules.md` §9.5— las nueve ocurrencias enumeradas, y registrar ocurrencias revisadas y ocurrencias cambiadas. **(b)** Rebajar la invariante de `Glosario-UX.md` §3.1 a lo que efectivamente se sostiene: calificación obligatoria **sólo en las secciones donde los dos sentidos conviven**, más entrada de glosario para el resto, que es el escalón que `Vocabulario-Rules.md` §9.3 prescribe cuando no hay colisión de sección. En cualquiera de los dos casos, corregir el criterio 14 de `README.md` §6 y el punto 4 de §5.

### H-04 · **P3** · La evidencia de colisión que justifica la desambiguación de «recorrido» no es verificable en las secciones que cita

- **Archivo y sección.** `03-UX-UI-DX/Glosario-UX.md` §3.1, línea 91.
- **Evidencia.** El documento afirma: «la colisión es real: los dos sentidos aparecen en la misma sección de `Guia-Onboarding-Developer.md` §3.3 y de `DX-Developer-Experience.md` §2». Contraverificado: **`DX-Developer-Experience.md` §2 no contiene ninguna ocurrencia del sustantivo «recorrido» ni de «recorridos»** —sólo la forma verbal «los tres se recorren»—. En `Guia-Onboarding-Developer.md` §3.3 aparece únicamente el sentido de continuidad («Los diez recorridos», paso 5); el sentido de integración no aparece en esa sección.
- **Por qué importa.** `Vocabulario-Rules.md` §10 exige que «toda invariante de desambiguación declarada cite la verificación de colisión que la justifica (§9.4)». Acá la cita existe pero no resiste la lectura de las secciones citadas.
- **Recomendación.** O bien reemplazar las dos referencias por secciones donde la colisión sea efectivamente observable, o bien —si no existe ninguna— declarar la desambiguación como preventiva y bajarla al escalón de entrada de glosario, resolviendo H-03 por la vía (b).

### H-05 · **P3** · `Glosario-Funcional.md` acuña 20 términos y cuatro lugares dicen «diecinueve»

- **Archivo y sección.** `02-Especificacion-Funcional/Glosario-Funcional.md` §5, control de cambios; propagado a `03-UX-UI-DX/Glosario-UX.md` cabecera (línea 11) y §4.1 (línea 116), y a `03-UX-UI-DX/README.md` §6 criterio 13 (línea 101).
- **Evidencia.** La tabla de `Glosario-Funcional.md` §2 tiene **20 filas de datos** (líneas 34 a 53): Fachada, Componente anfitrión, Elemento de dibujo, Instancia del visor, Identificador de instancia, Escena, Malla, Tipo dibujable, Resultado de dibujo, Estructura del texto, Árbol, Selección, Índice de pieza, Disposición, Texto del trabajo, Código de condición, Cero red, Cero persistencia, Página integradora, Capacidad gráfica tridimensional. El control de cambios afirma «Declara **diecinueve** términos acuñados»; `Glosario-UX.md` repite «los **diecinueve** términos que acuña la categoría 02» dos veces, y `README.md` de 03 lo repite en su autoverificación. Nótese que el «diecinueve» de `Glosario-UX.md` §2 referido a **sus propios** términos **sí es correcto** (9 + 2 + 6 + 2 = 19): el error está sólo en las referencias al glosario de 02.
- **Por qué importa.** Es un dato que un revisor usa para comprobar completitud sin contar, y la cifra viaja por referencia a tres artefactos de otra categoría.
- **Recomendación.** Corregir a «veinte» en los cuatro lugares.

### H-06 · **P3** · La tabla de diagnóstico de la guía tiene 10 síntomas y dos lugares dicen «once»

- **Archivo y sección.** `03-UX-UI-DX/Guia-Onboarding-Developer.md` §4 y §7 (control de cambios, «once síntomas de diagnóstico»); `03-UX-UI-DX/README.md` §2 («Incluye diagnóstico de **once** síntomas frecuentes»).
- **Evidencia.** La tabla de §4 tiene **10 filas de datos**, verificadas una a una: página que no dibuja · `inicializar` sin identificador por capacidad · `inicializar` sin identificador con capacidad presente · `INSTANCIA_DESCONOCIDA` generalizado · escena vacía tras cargar · menos piezas de las esperadas · ortoedro no dibujado que el backend sí interpreta · elemento del árbol que no resalta · petición en la pestaña de red · degradación tras idas y vueltas.
- **Recomendación.** Corregir a «diez» en los dos lugares, o sumar el síntoma faltante si la intención era once.

### H-07 · **P3** · `README.md` de 03 §3 anuncia «los tres de esta tabla» sobre una tabla de cinco filas

- **Archivo y sección.** `03-UX-UI-DX/README.md` §3, línea 50.
- **Evidencia.** «Un artefacto omitido **no vuelve**. **Los tres de esta tabla** no se van a emitir en ninguna fase posterior, y esa es la diferencia con §4». La tabla que sigue tiene **cinco** filas: `Experiencia-De-Uso.md`, `wireframes-<superficie>.md`, `DX-Portal-Developers.md`, `DX-Operability.md` y `representacion-<concepto>.md`. El propio control de cambios del documento dice «declaración de las **cinco** omisiones con su motivo».
- **Por qué importa.** §3 y §4 son justamente la sección donde la distinción omitido/previsto tiene que leerse sin ambigüedad, porque de ella depende que la Fase B2 no se lea como pendiente cancelado.
- **Recomendación.** Corregir «Los tres» por «Los cinco».

### H-08 · **P3** · `E-VIS-07` está archivada bajo un encabezado que no le corresponde

- **Archivo y sección.** `03-UX-UI-DX/DX-Error-Messages.md` §3.3 «Condiciones de carga del texto del trabajo», línea 109.
- **Evidencia.** La sección agrupa las condiciones de `cargarJson` (`E-VIS-08`, `E-VIS-09`, `E-VIS-10`), pero abre con `E-VIS-07`, cuyo código es `ELEMENTO_DE_DIBUJO_INVALIDO` y cuya función es **`redimensionar`**, que no tiene nada que ver con la carga del texto. Por su naturaleza correspondería a §3.1 «Condiciones de creación de la instancia» —o a una sección propia de condiciones de ajuste—.
- **Por qué importa.** `DX-Developer-Experience.md` §1.2 declara que cada sección de la categoría se escribe para leerse suelta, y `Glosario-UX.md` §2.2 lo eleva a término («lectura por sección»). Una entrada archivada bajo el encabezado equivocado rompe esa propiedad para el lector que entra por §3.3.
- **Recomendación.** Mover `E-VIS-07` a §3.1, renombrando esa sección a «Condiciones del elemento de dibujo», o abrir una §3.x propia. Actualizar la tabla de contenido.

### H-09 · **P3** · El enunciado de `INDICE_FUERA_DE_RANGO` en el contrato obliga a 03 a reinterpretarlo para `E-VIS-12`

- **Archivo y sección.** `02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md` §6, línea 216; con impacto en `03-UX-UI-DX/DX-Error-Messages.md` §3.4, nota sobre `E-VIS-12` (línea 121).
- **Evidencia.** El contrato §6 dice: «El índice recibido por `seleccionarPieza` no corresponde a **ninguna pieza del resultado de dibujo vigente**». Pero una pieza no dibujada **sí figura** en el resultado de dibujo vigente: contrato §5.2 la incluye explícitamente («Piezas no dibujadas | Una entrada por pieza que no produjo malla, con su índice y el código de condición»). El curso de `CU-03` §5 FA-02 —índice de una pieza enumerada como no dibujada— por lo tanto **no cae** bajo la letra de §6. `DX-Error-Messages.md` lo resuelve reinterpretando el enunciado: «encaja en `INDICE_FUERA_DE_RANGO` por su enunciado —no hay **pieza dibujada** con ese índice en el resultado de dibujo vigente—». El flujo principal de `CU-03` paso 2 usa la formulación correcta: «Verifica que el índice corresponde a una **pieza dibujada** del resultado de dibujo vigente».
- **Mérito reconocido.** 03 hizo lo correcto: declaró la tensión, no inventó código y devolvió la decisión a 02 («si 05-Arquitectura-Tecnica necesitara distinguir los dos casos, la decisión vuelve a 02-Especificacion-Funcional, que es la dueña de §6»). El defecto es del contrato, no del catálogo.
- **Recomendación.** Reemplazar en el contrato §6 «ninguna pieza del resultado de dibujo vigente» por «ninguna pieza **dibujada** del resultado de dibujo vigente», alineándolo con `CU-03` paso 2, y anotar en la misma fila que el curso de la pieza enumerada como no dibujada queda cubierto por el mismo código.

### H-10 · **P3** · Referencias upstream a nivel de carpeta o de documento sin sección concreta en cuatro artefactos de 02

- **Archivo y sección.** Cabeceras de `02-Especificacion-Funcional/README.md` (línea 9), `Especificacion-Funcional.md` (línea 9), `Definicion-Contrato-De-Fachada.md` (línea 9), `CU-01` (línea 9) y `CU-04` (línea 9).
- **Evidencia.** `README.md` declara como upstream «`../../../00-Contexto/`, `../../../01-Necesidades-Negocio/`»: **dos carpetas enteras, sin documento ni sección**. `Especificacion-Funcional.md` cita «`../../../00-Contexto/Compatibilidad-Plataformas.md`» y «`NB-04-Interpretacion-Fiel-Del-Dato-Del-Alumno.md`» sin sección. `Definicion-Contrato-De-Fachada.md` y `CU-01` citan «`00-Contexto/Compatibilidad-Plataformas.md` (requisito de capacidad gráfica del navegador)»; `CU-04` cita «`00-Contexto/Compatibilidad-Plataformas.md` (matriz del navegador)». Ninguna de las cinco lleva `§`. El contraste es visible dentro de la misma fase: los cinco artefactos de 03 sí citan `Compatibilidad-Plataformas.md` **§2.2, §2.3 y §4**, que son secciones existentes y correctas.
- **Por qué importa.** `Rules-Especificacion-Funcional.md` §3.3 y `Rules-UX-UI-DX.md` §3.3 exigen trazabilidad por vínculo verificable; una carpeta entera no es un vínculo verificable. El resto de las cabeceras de esta fase son ejemplares —incluidas las 30 citas al intake, **todas con `§` concreto: cero ocurrencias de «PRODUCT-INTAKE» sin sección en los quince archivos**—, lo que hace que estas cinco desentonen.
- **Recomendación.** Sustituir por secciones concretas: `Compatibilidad-Plataformas.md` §2.2 (plataforma del navegador) y §4 (alternativas para plataformas no soportadas); `NB-04` §4; y en el `README.md` de 02, los documentos y secciones efectivamente consumidos en lugar de las dos carpetas.

### H-11 · **P3** · `Especificacion-Funcional.md` §2 enuncia una prohibición absoluta que su propio catálogo incumple

- **Archivo y sección.** `02-Especificacion-Funcional/Especificacion-Funcional.md` §2, línea 51.
- **Evidencia.** «Un caso de uso de esta categoría que **mencionara** al alumno, al docente, al **backend**, a un servicio o a una credencial estaría **mal escrito por definición**». Sin embargo: `CU-06` se titula «**Ejercitar la fachada completa sin backend**» y menciona el backend en su §1, §3, §7, §8 y §10; `CU-02` §6 y §10 mencionan «el backend del producto» y «el programa del alumno»; `CU-03` §10 menciona «el dato del alumno». En todos los casos la mención es **para declarar exclusión**, que es correcto y necesario —es lo que impide que un lector aguas abajo le atribuya validación a la fachada—; lo que está mal es el enunciado, que prohíbe la mención en lugar de prohibir la participación.
- **Recomendación.** Reescribir como: «Un caso de uso de esta categoría en el que el alumno, el docente, el backend, un servicio o una credencial **intervinieran como actor o condicionaran un flujo** estaría mal escrito por definición. Nombrarlos para declarar qué queda fuera del contrato es, en cambio, obligatorio».

### H-12 · **P3** · El fundamento normativo de la sección de compatibilidad del contrato remite a una regla que gobierna otro artefacto

- **Archivo y sección.** `02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md` §7, línea 220.
- **Evidencia.** «Sección opcional admitida para el tipo `library` (`Rules-Especificacion-Funcional.md` §4.3)». Pero §4.3 de esa regla se titula «Secciones opcionales **por tipo de proyecto de código**» y enumera secciones numeradas §12 a §17 que se agregan **a un caso de uso**, a continuación de las once obligatorias de §4.2 —«sin desplazar las obligatorias»—; la que corresponde es «§17 Compatibilidad de versión pública, sólo para `library`». El documento de concepto `Definicion-<Concepto>.md` no tiene lista de secciones prescrita en ningún §4.2.x, de modo que **la sección es libremente admisible y no necesita esa cita**; la cita, tal como está, atribuye a §4.3 un alcance que no tiene, y además la numera §7 en vez de §17.
- **Por qué importa.** Un lector aguas abajo puede concluir que los seis CU deberían llevar una §17 de compatibilidad, que ninguno lleva y ninguno necesita.
- **Recomendación.** Reformular como «Sección propia de este documento de concepto. `Rules-Especificacion-Funcional.md` §4.3 prevé una sección homóloga, §17, como sección opcional de los CU de tipo `library`; acá el contenido se declara una sola vez en el documento de concepto y los CU no la repiten».

---

## 6. Veredicto y condiciones para promover

### 6.1 Veredicto

**APROBADO CON OBSERVACIONES.**

Sin ningún hallazgo P0 ni P1. Los tres P2 son desincronizaciones internas —una en la tabla de códigos del contrato, una en la membresía de un conjunto de propiedades, una en una invariante léxica autoimpuesta— que no comprometen el contrato de fachada, ni la regla de arquitectura del producto, ni la fidelidad al upstream.

Lo que sostiene el veredicto, y que se verificó y no se dio por supuesto:

- **La derivación de los siete códigos de condición está bien hecha**, uno por uno: cero códigos inventados, cero códigos sin cubrir, semántica coincidente en seis de los siete y en once de las doce entradas. El único desvío, H-01, es un defecto del contrato y no del catálogo.
- **`RA-02` se respeta sin excepción en los quince documentos.** No hay una sola afirmación que atribuya al archivo de guion red, configuración propia, persistencia, conocimiento de la persona o participación en autorización. El gate de cero red se declara con umbral exacto 0 en siete artefactos.
- **Los dos puntos que el orquestador señaló están efectivamente resueltos:** la numeración local `CU-01` a `CU-06` está declarada con su correspondencia a `CU-15`/`CU-16`/`CU-17` y a `CU-12`, sin identificadores colgando; y los números y propiedades de **E-1 del intake 1.2** —tres piezas, ortoedro dibujado, texto editado a mano con la clave alternativa y sin comas finales, tolerancias del formato en E-2— se citan correctamente en los seis artefactos que los invocan.
- **Las tres decisiones de vocabulario cerradas en la Fase A se respetan**, incluida la más delicada: este proyecto de código no emite observación, advertencia ni error de validación, y ninguna condición de contrato lleva esos nombres en ninguno de los quince archivos.
- **Las omisiones están declaradas con motivo**, y la distinción entre omitido y previsto está correctamente planteada.

### 6.2 Condiciones para promover

Ninguna condición es bloqueante. Se recomienda absorber las correcciones **dentro de la versión en curso**, sin subir versión, mientras el estado de los documentos siga siendo `Propuesto` (`Master-Prompt.md` §5), como se hizo en la Fase A.

| Prioridad | Condición | Artefactos a tocar |
| --- | --- | --- |
| Antes de despachar 05 y 08 | Cerrar **H-01** y **H-02**: son las dos que las categorías aguas abajo van a leer literalmente para escribir aserciones | `Definicion-Contrato-De-Fachada.md` §4.4 y §6; `Especificacion-Funcional.md` §3.1 y §6; `Definicion-Contrato-De-Fachada.md` §4.5; `CU-06` §1 |
| Antes de cerrar la fase | Cerrar **H-03** y **H-04** por cualquiera de los dos caminos propuestos, y alinear la autoverificación del `README.md` de 03 | `Glosario-UX.md` §3.1; `DX-Developer-Experience.md`; `Guia-Onboarding-Developer.md`; `DX-Error-Messages.md` §4; `README.md` de 03 §5 y §6 |
| Antes de cerrar la fase | Cerrar **H-09** y **H-12**, que son del mismo documento de concepto y se corrigen en una sola pasada | `Definicion-Contrato-De-Fachada.md` §6 y §7 |
| Barrido de higiene | Cerrar **H-05**, **H-06**, **H-07**, **H-08**, **H-10** y **H-11**: conteos, ubicación de una entrada, cinco cabeceras y un enunciado | Los siete archivos citados en cada hallazgo |

### 6.3 Sobre el arranque de la Fase B2

**La Fase B2 puede arrancar.** No hay ninguna condición previa incumplida:

- Los tres artefactos de maqueta —`Linea-Base-Visual.md`, `Contrato-Datos-Maqueta.md` y `Bitacora-Validacion-Maqueta.md`— **correctamente no existen todavía**, y su ausencia no es hallazgo.
- El `README.md` de 03 los declara en su §4 como **previstos para la Fase B2**, con emisor (AG-03M), momento («después del audit de esta fase, con su propia detención con el humano») y carpeta de destino, y separa explícitamente esa condición de la de artefacto omitido. `DX-Developer-Experience.md` §8 y `DX-Error-Messages.md` §7 lo repiten sin contradecirse.
- El criterio 17 de `Rules-UX-UI-DX.md` §6 está declarado **«no aplicable todavía»** y no como incumplido, que es la calificación correcta; el criterio 16 está declarado no aplicable por no haber wireframes, con fundamento en el mínimo de cero para `library`.
- El insumo que la Fase B2 necesita está disponible y es estable: el contrato de las cinco funciones, los siete códigos y la propiedad de resaltado que `README.md` §4 fijó al recoger el diferimiento de `CU-03` §10.
- Ninguno de los doce hallazgos toca material que AG-03M vaya a consumir de forma bloqueante. **H-03**, si se cierra por la vía de calificar ocurrencias, conviene resolverlo antes para que la Fase B2 no herede la forma desnuda al escribir sus tres artefactos en esta misma carpeta —`Glosario-UX.md` declara en su trazabilidad downstream que la Fase B2 hereda este vocabulario—, pero no impide arrancar.

---

## 7. Control de cambios de este informe

| Versión | Fecha | Cambios |
| --- | --- | --- |
| r1 | 2026-08-08 | Emisión inicial. Auditoría de las categorías 02 y 03 de `GeometriaFactory-Visor` en la Fase B: matriz D1–D9 sobre quince documentos, matriz de estructura obligatoria contra §2.1, §2.2 y §4 de las dos reglas, contraverificación ítem por ítem de los dieciocho criterios de `Rules-UX-UI-DX.md` §6, verificación uno por uno de los siete códigos de condición entre el contrato de fachada y el catálogo de doce entradas, gobierno del glosario con los cuatro criterios de `Vocabulario-Rules.md` §10 y enumeración de once polisemias evaluadas y descartadas. Doce hallazgos: 0 P0, 0 P1, 3 P2 y 9 P3. Veredicto **APROBADO CON OBSERVACIONES**, con la Fase B2 habilitada para arrancar. |
