# DoR del tramo `k` — evaluación criterio por criterio · 2026-09-12

**Producto:** Fábrica de Geometría
**Documento:** DoR-Tramo-k-2026-09-12.md
**Versión:** 1.2
**Estado:** Aprobado
**Fecha:** 2026-09-12
**Autor:** Scrum Master / Backlog Curator (AG-06), en su papel de evaluación de DoR
**Base de la corrida:** `f2283a3a8ae9338ac00a6005e0460c2839f70b83` (`main`, limpio, al día; verificado con `git rev-parse HEAD` antes de abrir la rama `fase-k/mini-plan-tramo-k`)

---

## 0. Alcance y método

Evalúa `BT-00027` a `BT-00035` (`GeometriaFactory-Api/06-Backlog-Tecnico/tareas-tecnicas/`) contra los
seis criterios de `Definition-Of-Ready.md` §2.1 y las excepciones de su §3.1, con cita literal de la
ficha para cada respuesta. Las nueve fichas entraron en **`Estado: Borrador`** por extracción
(`BT-XXXXX` v1.0, control de cambios), sin evaluación previa.

**Regla seguida para las correcciones**: se corrige una ficha únicamente cuando existe en el árbol una
fuente **ya admitida** por el criterio 1 —un componente de `05` §3.1, una ADR, un NFR de `05` §8, un
riesgo de `05` §9, un punto abierto de `05` §11 (de cualquiera de las cuatro capas que consolida el
documento), un punto de acceso de la superficie de `02`, o una regla de delivery del intake §15— que
sostenga la misma tarea sin cambiar su alcance. Donde no existe, la ficha queda en `Borrador` y el
hueco se declara; no se inventa. **«Una ADR» incluye las de nivel Producto** (`SDD/Docs/Producto/Adrs/`,
`ADR-08xxx`): la 1.0 de este documento sólo miró las de `05` de la unidad, y ése fue el error que la
1.1 corrige en BT-00031 (ver §7).

**ORIGEN DEL HECHO**: de esta corrida, evaluando fichas ya emitidas por una corrida anterior
(`Apertura-Fase-k-2026-09-12.md`, base `9167e68`). **SI NO RESPONDÉS**: un hueco (BT-00028) no tiene
fuente admitida en el árbol y requiere que la categoría `05` (arquitectura) emita el artefacto que
falta —no una decisión de esta categoría—; se declara en el lote de detenciones de §4 y queda en
`Borrador`. (La 1.0 declaraba dos huecos; el segundo, BT-00031, era falso: ver §7.) **Respondido el
2026-09-12** (1.2): el Product Owner decidió, y la ADR que faltaba existe — [`ADR-00009`](../Unidades-Entrega/GeometriaFactory-Api/05-Arquitectura-Tecnica/Adrs/ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md) — y lo que
dice es que BT-00028 **no se construye** (`Descartada`). El lote de §4 queda vacío.

---

## 1. Los seis criterios de `Definition-Of-Ready.md` §2.1 (`GeometriaFactory-Api`, tareas técnicas)

1. Declara su fuente upstream por identificador (componente `05` §3.1, ADR, NFR `05` §8, riesgo `05`
   §9, punto abierto `05` §11, punto de acceso de la superficie de `02`, o regla de delivery del
   intake §15).
2. Declara al menos una historia consumidora, o se justifica como infraestructura compartida citando
   la ADR, la puerta o el punto abierto que la sostiene.
3. Sus criterios de aceptación son verificables, y si sostienen una ausencia, con umbral cero y
   condición de medición.
4. Si la tarea toca la superficie, declara la comparación contra una lista en las dos direcciones.
5. Sus dependencias están declaradas y ninguna es circular, sin cruzar la regla de `05` §3.2.
6. Si es indagación, caja temporal en etapas o punto de control, nunca en horas; y declara si obliga a
   otro proyecto de código o le pertenece.

**Excepción admitida de §3.1 relevante acá**: «Tarea de indagación que cierra o eleva un punto abierto
de `05` §11 — el criterio 3 puede cumplirse con el resultado esperado en lugar de un criterio
verificable de antemano — el Product Owner, en el punto de control de la etapa que la contiene».

---

## 2. Tabla BT × seis criterios

### BT-00027 — Reescribir `ADR-00008` adoptando `/v{MAJOR}/`

| # | Sí/No | Cita literal de la ficha |
| --- | --- | --- |
| 1 | **Sí** | §2: «[`ADR-00008`](...) vigente» — una ADR, categoría admitida |
| 2 | **Sí** | §7: «Fuente de arquitectura: ADR-00008 (a reescribir)» |
| 3 | **Sí** | §3: «El ADR declara el cambio de premisa... y adopta `/v{MAJOR}/`... el documento anterior pasa a `Superado`...» — verificable por inspección textual |
| 4 | N/A | §7: «Puntos de acceso que toca: Ninguno» |
| 5 | **Sí** | §4: «Ninguna» — sin dependencias, no hay ciclo posible |
| 6 | N/A | Tipo `docs`, no indagación |

**Resultado en 1.0: Ready.** Sin corrección de contenido; pasó a `Ready` en v1.1.

**Reevaluación tras la corrección de la ficha (v1.2).** La ficha reescribía la premisa «no hay clientes
de terceros» sólo en `ADR-00008`, pero la misma premisa vive en más lugares del árbol vivo, y uno de
ellos está **fuera de la unidad de entrega**: [`ADR-08003`](../Producto/Adrs/ADR-08003-Versionado-Por-Compilacion-Compartida.md)
(nivel Producto). Dos criterios cambian de respuesta o de cita:

| # | Sí/No | Cita literal de la ficha (v1.2) |
| --- | --- | --- |
| 3 | **Sí** | §3, tercer criterio: «la premisa «no hay clientes de terceros» se reescribe o se declara superada en el **conjunto medido** con `git grep -n -i "no hay clientes de terceros" -- SDD ':!*/_legacy/*' ':!SDD/Docs/Audit/*'` (...), que a la fecha de esta ficha devuelve exactamente estos dieciséis lugares en once documentos» — verificable con el comando que la propia ficha fija; sin él, la tarea podía cerrar habiendo reescrito una sola fuente y dejado quince lugares contradiciéndola |
| 6 | **Sí, con declaración** | La 1.0 respondía «N/A, tipo `docs`», pero el criterio 6 tiene dos mitades y la segunda («declara si obliga a otro proyecto de código o le pertenece») no depende del tipo. §2: «esta ficha declara que la decisión **alcanza fuera de esta unidad de entrega**: `ADR-08003` obliga también a `GeometriaFactory-Contracts`»; §7: «Alcance fuera de la unidad (criterio 6 de la DoR): **Sí**» |

Los criterios 1, 2, 4 y 5 no cambian. **Resultado: sigue Ready** (v1.2).

---

### BT-00028 — Autenticación por cliente para terceros

| # | Sí/No | Cita literal de la ficha (v1.0) |
| --- | --- | --- |
| 1 | **No, en la letra** | §2: «`Mesa-2026-09-12-ciclo-2.md` §4 ítem 3; `PRODUCT-INTAKE` 4.3 §17.1.P.5...; el ítem diferido D-01...» — un registro de mesa, una sección del intake que no es §15, y un ítem diferido que no es punto abierto de `05` §11: **ninguna de las tres está en la lista admitida** |
| 2 | **No** | §7: «habilita BT-00029 y BT-00030» — no cita ADR, puerta ni punto abierto |
| 3 | Sí | §3: cuatro escenarios verificables (200/401/revocación/ROPC intacto) |
| 4 | N/A | §7: «Puntos de acceso que toca: Ninguno bajo `/v1/` todavía» |
| 5 | Sí | §4: «BT-00027» — una dependencia, sin ciclo |
| 6 | N/A | Tipo `feature`, no indagación |

**Búsqueda de fuente admitida alternativa** (criterio 1): se revisaron `05` §3.1 (ocho componentes,
ninguno de autenticación de terceros), `05` §8.1 (NFR, ninguno de autenticación externa), `05` §9.1
(riesgos, ninguno de este tema), `05` §11.1 (`PA-01` a `PA-10`, ninguno sobre «quién es el cliente» ni
sobre el mecanismo de autenticación por clave), la superficie de `02` (los quince puntos son del ROPC
de alumnos, ninguno de clientes externos) y las cinco reglas de delivery del intake §15. **No existe
fuente admitida.** La decisión de diseño está tomada (`PRODUCT-INTAKE` §17.1.P.5, «API key o
`client_credentials`, RFC 6749 §4.4»), pero no tiene ADR propia en `05`: escribirla es trabajo de la
categoría 05, no de esta ficha.

**Resultado: Borrador.** Falta: criterio 1 (fuente admitida) y, en consecuencia, criterio 2. Se
declara en la ficha (v1.1) y en el lote de detenciones de §4.

---

### BT-00029 — Rate limiting por clave o por IP

| # | Sí/No | Cita literal de la ficha (v1.0, antes de corregir) |
| --- | --- | --- |
| 1 | **No, en la letra** | §2 y §7: «`05` §9 (único escritor SQLite, `RS-OPE-01`)» — **`RS-OPE-01` no existe en `05`**: es un identificador de `SDD/Docs/Audit/Mesa-2026-09-12.md` línea 119, y §9 (riesgos) de las cuatro capas de `05` no lleva identificadores de fila (verificado: `grep -rn "RS-OPE" SDD/` sólo lo encuentra en `Audit/Mesa-2026-09-12*.md`, en el intake §17.1.P.5, y en esta ficha y en `Backlog-Tecnico.md`) |
| 2 | **No, en la letra** | §7: «protege el recurso que BT-00006 y BT-00022 ya miden» — no cita ADR/puerta/punto abierto |
| 3 | Sí | §3: tres criterios verificables |
| 4 | N/A | §7: «Puntos de acceso que toca: Ninguno» |
| 5 | Sí | §4: «BT-00028» — una dependencia, sin ciclo (nota: BT-00028 queda en Borrador, ver destrabe en el Mini-Plan §3.1) |
| 6 | N/A | Tipo `feature`, no indagación |

**Fuente admitida encontrada**: `05` §8.1 (`GeometriaFactory-Api`), fila **«Caudal sostenido»**: «20
peticiones por minuto [ASUNCIÓN del intake]... derivado... de la limitación de escritor único del
almacén», con [`ADR-00005`](../Unidades-Entrega/GeometriaFactory-Api/05-Arquitectura-Tecnica/Adrs/ADR-00005-Sin-Paginacion-Con-Condicion-De-Reingreso-Declarada.md).
Es un NFR de `05` §8 — categoría admitida por el criterio 1 — y sostiene el mismo hecho técnico
(escritor único de SQLite) que `RS-OPE-01` pretendía citar.

**Corrección aplicada**: se reemplaza la cita por `05` §8.1, fila «Caudal sostenido» (`ADR-00005`), en
§2 y en §7. Ningún criterio de aceptación cambia.

**Resultado: Ready** (v1.1, tras la corrección).

---

### BT-00030 — Declarar CORS, condicional al cliente que aparezca (`D-01`)

| # | Sí/No | Cita literal de la ficha (v1.0, antes de corregir) |
| --- | --- | --- |
| 1 | **No, en la letra** | §2 y §7: «ítem diferido D-01 (`PRODUCT-INTAKE` §9/§17.1.P.3, `Mesa-2026-09-12.md` §8)» — un ítem diferido que no figura como punto abierto de `05` §11 (verificado: ninguna fila `PA-XX` de `05` §11.1 menciona `D-01`) |
| 2 | **No, en la letra** | §7: «depende del ítem diferido D-01 y no lo duplica» — no cita ADR/puerta/punto abierto |
| 3 | Sí | §3: declara explícitamente por qué CORS no aplica hoy y la condición de reingreso si `D-01` cierra con cliente de navegador |
| 4 | N/A | §7: «Puntos de acceso que toca: Ninguno» |
| 5 | Sí | §4: «BT-00028» — una dependencia, sin ciclo (misma nota que BT-00029) |
| 6 | **Sí, tras corregir** | Tipo `indagación`; §3 y §5 (v1.2): «**Caja temporal: la etapa `k`, que cierra en su punto de control, con `D-01` cerrado o con la declaración de por qué CORS no aplica**» — una etapa y su punto de control, que es lo que la letra del criterio exige. El «Sí» de la 1.0 descansaba en una **equivalencia no admitida**: la ficha decía «hasta que D-01 cierre», y este documento lo dio por equivalente al punto de control; no lo es, porque `D-01` es un ítem diferido «sin evento ocurrido todavía» y no fija techo |

**Fuente admitida encontrada**: `05` §9.1 (`GeometriaFactory-Api`), riesgo **«Que se agregue un punto
de acceso pensado para el navegador, o se configure el intercambio de origen cruzado»** (impacto muy
alto, probabilidad baja), mitigado hoy por «las tres ausencias declaradas de la superficie de 02» y por
«el hecho de que el único cliente legítimo esté declarado en el manifiesto y en el grafo». Es un riesgo
de `05` §9 — categoría admitida — y describe exactamente la condición que esta tarea declara (CORS no
aplica hoy, condicionado a que aparezca un cliente de navegador).

**Corrección aplicada** (v1.1): se agrega esa cita en §2 y en §7, conservando `D-01` como condición
adicional (quién es el cliente, no si CORS aplica hoy). Ningún criterio de aceptación cambia. La excepción
de tipo indagación de §3.1 no hacía falta invocarla: el criterio 3 ya estaba satisfecho con un criterio
verificable de antemano.

**Corrección aplicada** (v1.2, criterio 6): la caja temporal pasa, en los dos lugares donde aparecía
(criterios de aceptación y §5), a «la etapa `k`, que cierra en su punto de control, con `D-01` cerrado o
con la declaración de por qué CORS no aplica». El primer criterio de aceptación ya obligaba a esa
declaración mientras `D-01` siga abierto, así que no cambia ningún comportamiento exigido: repara la
letra del criterio 6. Las demás indagaciones de la unidad anclan la caja del mismo modo (`BT-00005`/`07`/
`09`/`10`: «la etapa `a`»; `BT-00025`: «antes de fijar la puerta en 09»).

**Resultado: Ready** (v1.1 tras la primera corrección; sigue Ready en v1.2).

---

### BT-00031 — Publicar OpenAPI/Scalar en el ambiente de producción

| # | Sí/No | Cita literal de la ficha (v1.0, antes de corregir) |
| --- | --- | --- |
| 1 | **No, en la letra** | §2 y §7: «`Mesa-2026-09-12-ciclo-2.md` §4 ítem 6» — únicamente un registro de mesa |
| 2 | **No, en la letra** | §7: «ya construido, sólo cambia la configuración de publicación» — no cita ADR/puerta/punto abierto |
| 3 | Sí | §3: tres criterios verificables (`Documentacion__Publicada=true`, `/openapi/v1.json` responde `200`, Scalar sin credencial) |
| 4 | N/A | §7: «Puntos de acceso que toca: Ninguno» |
| 5 | Sí | §4: «Ninguna» |
| 6 | N/A | Tipo `devops`, no indagación |

**Lo que la 1.0 de este documento afirmó, y era falso.** La búsqueda de fuente admitida se hizo con
`grep -n "Scalar\|OpenAPI\|openapi" 05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md` —es decir,
**sólo sobre `05` de la unidad de entrega**— y sobre la superficie de `02` y el intake §15. No miró
`SDD/Docs/Producto/Adrs/`, que también son ADR y por lo tanto fuente admitida por el criterio 1. Sobre esa
búsqueda incompleta se declaró «no existe fuente admitida», se dejó la ficha en `Borrador` y se la puso
en el lote de detenciones de §4. **La ausencia afirmada era falsa.**

**Fuente admitida encontrada** (corrección detectada por el orquestador contra el árbol):
[`ADR-08008`](../Producto/Adrs/ADR-08008-La-Superficie-HTTP-Se-Describe-Y-El-Explorador-No-Se-Publica-Solo.md),
**nivel Producto, `Estado: Aceptado`**, §2 punto 2: «El explorador navegable existe —Scalar, en
`/documentacion`— y no se publica solo. En desarrollo está siempre; fuera de desarrollo hace falta decir
`Documentacion__Publicada=true`.» Es una ADR —categoría admitida— y declara exactamente la decisión que
esta tarea ejecuta en producción. Verificado con
`grep -n "Documentacion__Publicada" SDD/Docs/Producto/Adrs/ADR-08008*.md` → líneas 29 y 76.

**Corrección aplicada** (ficha v1.2): se agrega esa cita en §2 y en §7, y un cuarto criterio de
aceptación con umbral cero y condición de medición: los puntos de acceso fuera de la guardia siguen en
exactamente **4** (`05` §8.1, §3.4: `A-01`, `A-02`, `A-03`, `A-16`) después de publicar, porque
`/openapi/v1.json` y `/documentacion` no son puntos de la superficie de `02` sino del explorador que
`ADR-08008` §2 declara.

**Reevaluación completa con la ficha v1.2:**

| # | Sí/No | Cita literal de la ficha (v1.2) |
| --- | --- | --- |
| 1 | **Sí** | §2: «[`ADR-08008`](...), **Estado: Aceptado**, §2 punto 2: «El explorador navegable existe —Scalar, en `/documentacion`— y no se publica solo. (...)» Es una ADR, fuente admitida por el criterio 1 de la DoR» |
| 2 | **Sí** | §7: «**Infraestructura compartida**, sostenida por `ADR-08008` (criterio 2 de la DoR): ya construido, sólo cambia la configuración de publicación» |
| 3 | **Sí** | §3: «`Documentacion__Publicada=true` en el ambiente de producción»; «`/openapi/v1.json` responde `200`»; «Scalar sirve la documentación pública sin exigir credencial de alumno»; «el recuento de puntos de acceso fuera de la guardia sigue en exactamente **4** (`05` §8.1, §3.4: `A-01`, `A-02`, `A-03`, `A-16`) después de publicar» — tres verificables por petición HTTP o inspección de configuración, y el cuarto sostiene una ausencia con umbral cero y condición de medición |
| 4 | N/A | §7: «Puntos de acceso que toca: Ninguno» — y el cuarto criterio de §3 fija que no agrega ninguno a la superficie de `02` |
| 5 | **Sí** | §4: «Ninguna.» — sin dependencias, no hay ciclo posible |
| 6 | N/A | §5: «`devops`.» — no es indagación; `ADR-08008` es de nivel Producto pero la tarea sólo cambia configuración de despliegue de esta unidad, no obliga a otro proyecto de código |

**Resultado: Ready** (v1.2, tras la corrección). Sale del lote de detenciones de §4.

---

### BT-00032 — Versionar las rutas públicas bajo `/v1/`

| # | Sí/No | Cita literal de la ficha (v1.0, antes de corregir) |
| --- | --- | --- |
| 1 | **Sí** | §7: «Fuente de arquitectura: ADR-00008 (a reescribir)» — una ADR, categoría admitida (aunque §2 sólo cita el registro de mesa) |
| 2 | **Sí** | §7: «habilita BT-00034 y BT-00035», sostenido por la misma ADR-00008 que la ficha ya trae |
| 3 | Sí | §3: tres criterios verificables |
| 4 | **No, hasta corregir** | §7: «Puntos de acceso que toca: Los quince, bajo `/v1/`» — toca la superficie entera y **no declaraba** la comparación en las dos direcciones que exige el criterio 4 |
| 5 | Sí | §4: «BT-00027, BT-00028, BT-00029, BT-00030» — cuatro dependencias, sin ciclo (verificado con `tsort`, §5 de este documento); nota: BT-00028 queda en Borrador |
| 6 | N/A | Tipo `feature`, no indagación |

**Corrección aplicada** (criterio 4): se agrega un cuarto criterio de aceptación en §3, citando `05`
§3.4 (la tabla ya existente de los quince puntos de acceso contra su componente): «las rutas
efectivamente publicadas bajo `/v1/` se comparan en las dos direcciones contra los quince puntos... ni
uno queda sin `/v1/` y ningún prefijo `/v1/` corresponde a una ruta que `05` §3.4 no declare». Es la
comparación bidireccional que el propio criterio de aceptación original ya implicaba («no queda ninguna
ruta pública sin versión de MAJOR»), hecha explícita y anclada a una fuente admitida ya existente.

**Resultado: Ready** (v1.1, tras la corrección).

---

### BT-00033 — Adoptar MinVer y etiquetar los commits de producción

| # | Sí/No | Cita literal de la ficha (v1.0, antes de corregir) |
| --- | --- | --- |
| 1 | **Ambiguo/incorrecto, corregido** | §2 y §7: «`05` §11 `PA-06`» sin calificar capa. El `PA-06` de **esta misma capa** (`GeometriaFactory-Api` §11.1) es «RESUELTO. El alcance de la colección de peticiones» (`§18`, ocho escenarios) — **sin relación con MinVer**. El punto abierto correcto es el de `GeometriaFactory-Application` §11.3 `PA-06`: «La herramienta que calcula la versión... no está elegida», **vigente**, reasignado a la fase `i`; `GeometriaFactory-Domain` §11.2 trae el mismo punto como `PA-04` |
| 2 | **Sí, tras corregir** | §7 corregido cita los dos puntos abiertos por capa |
| 3 | Sí | §3: tres criterios verificables, incluida la declaración explícita si dos commits puntuales no quedan etiquetados |
| 4 | N/A | §7: «Puntos de acceso que toca: Ninguno» |
| 5 | Sí | §4: «Ninguna» |
| 6 | N/A | Tipo `devops`, no indagación |

**Corrección aplicada**: se corrige la cita en §2 y en §7 a `05` §11.3 (`GeometriaFactory-Application`)
`PA-06` y §11.2 (`GeometriaFactory-Domain`) `PA-04`, calificados por capa, en lugar del `PA-06` sin
calificar que —leído desde el archivo donde vive la ficha— apunta al punto abierto equivocado. Ningún
criterio de aceptación cambia.

**Resultado: Ready** (v1.1, tras la corrección).

---

### BT-00034 — Sample de onboarding para un cliente externo contra `/v1/`

| # | Sí/No | Cita literal de la ficha (v1.0, antes de corregir) |
| --- | --- | --- |
| 1 | **No, en la letra** | §2 y §7: «`Mesa-2026-09-12-ciclo-2.md` §4 ítem 9» — únicamente un registro de mesa |
| 2 | **No, en la letra** | §7: «extiende BT-00020 al consumidor externo» — no cita ADR/puerta/punto abierto |
| 3 | Sí | §3: un criterio verificable (cinco pasos o menos, análogo a BT-00020) |
| 4 | N/A | §7: «Puntos de acceso que toca: Ninguno» |
| 5 | Sí | §4: «BT-00032» — una dependencia, sin ciclo |
| 6 | N/A | Tipo `docs`, no indagación |

**Fuente admitida encontrada**: la propia ficha declara ser «análoga a BT-00020», y `BT-00020`
(`Construir-La-Coleccion-De-Peticiones-Reproducible.md`, `Backlog-Tecnico.md` fila 333) cita como fuente
`05` §8, última fila («Pasos de la colección de peticiones reproducible»: 5 o menos, 0 datos inventados)
y [`ADR-00008`](../Unidades-Entrega/GeometriaFactory-Api/05-Arquitectura-Tecnica/Adrs/ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md).
El mismo NFR sostiene, sin cambio de alcance, la analogía que BT-00034 ya reclama.

**Corrección aplicada**: se agrega esa cita en §2 y en §7. Ningún criterio de aceptación cambia.

**Resultado: Ready** (v1.1, tras la corrección).

---

### BT-00035 — Política de deprecación en `Estrategia-Versionado.md`

| # | Sí/No | Cita literal de la ficha (v1.0, antes de corregir) |
| --- | --- | --- |
| 1 | **No, en la letra** | §2 y §7: «`Mesa-2026-09-12-ciclo-2.md` §4 ítem 10; `PRODUCT-INTAKE` 4.3» — un registro de mesa y una versión del intake citada sin sección |
| 2 | **No, en la letra** | §7: «cierra el ítem 10 del plan de la fila k» — no cita ADR/puerta/punto abierto |
| 3 | Sí | §3: un criterio verificable (entrada con plazo de un cuatrimestre y mecanismo de aviso) |
| 4 | N/A | §7: «Puntos de acceso que toca: Ninguno» |
| 5 | Sí | §4: «BT-00032» — una dependencia, sin ciclo |
| 6 | N/A | Tipo `docs`, no indagación |

**Fuente admitida encontrada**: [`ADR-00008`](../Unidades-Entrega/GeometriaFactory-Api/05-Arquitectura-Tecnica/Adrs/ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md)
**§2 «Decisión», regla 1** (la ficha lo escribe «§2 punto 1»), vigente: «no hay deprecación gradual: no
hay a quién dársela». Es exactamente la premisa que `BT-00027` reescribe y que esta política ejecuta
sobre `Estrategia-Versionado.md` una vez reescrita. La 1.0 de este documento —y la ficha v1.1— citaban
«§1 punto 1»: la frase no vive en §1 («Contexto») sino en §2, línea 27 del ADR, verificado con
`grep -n "no hay deprecación gradual" .../ADR-00008*.md` → `27:1. **Una sola versión de la superficie
vive a la vez.** (...) no hay deprecación gradual: no hay a quién dársela.`

**Corrección aplicada**: se agrega esa cita en §2 y en §7 (v1.1); la ficha v1.2 corrige la sección
citada a §2 regla 1. Ningún criterio de aceptación cambia.

**Resultado: Ready** (v1.1, tras la corrección; sigue Ready en v1.2).

---

## 3. Resumen de resultados

| BT | Resultado | Corrección aplicada | Fuente admitida usada |
| --- | --- | --- | --- |
| BT-00027 | **Ready** | Criterio de aceptación agregado (conjunto medido de 16 lugares) y declaración de alcance fuera de la unidad (criterio 6) | `ADR-00008` (ya citada); `ADR-08003` (nivel Producto, misma premisa) |
| BT-00028 | **Descartada** (1.2; era **Borrador** en 1.0/1.1) | Ninguna posible sobre la ficha; la resolvió una decisión de producto | [`ADR-00009`](../Unidades-Entrega/GeometriaFactory-Api/05-Arquitectura-Tecnica/Adrs/ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md) (**Aceptado**, 2026-09-12): la API autentica personas, no aplicaciones; no hay nada que construir |
| BT-00029 | **Ready** | Cita corregida | `05` §8.1, «Caudal sostenido» (`ADR-00005`) |
| BT-00030 | **Ready** | Cita agregada; caja temporal corregida a «la etapa `k`, en su punto de control» (criterio 6) | `05` §9.1, riesgo de CORS/navegador |
| BT-00031 | **Ready** | Cita agregada y criterio de aceptación agregado (umbral cero: 4 puntos fuera de la guardia) | `ADR-08008` §2 punto 2 (nivel Producto, Aceptado) |
| BT-00032 | **Ready** | Criterio de aceptación agregado (criterio 4) | `05` §3.4 (los quince puntos) |
| BT-00033 | **Ready** | Cita corregida (capa) | `05` §11.3 `PA-06` (Application) y §11.2 `PA-04` (Domain) |
| BT-00034 | **Ready** | Cita agregada | `05` §8, última fila (`ADR-00008`), por analogía con BT-00020 |
| BT-00035 | **Ready** | Cita agregada; sección corregida (§2 regla 1, no §1) | `ADR-00008` §2 «Decisión», regla 1 |

**Ocho de nueve están en `Ready`; una queda en `Borrador`** (BT-00028) por ausencia de fuente admitida
en el árbol, no por defecto de redacción. Medido, no heredado (1.1):

```
$ git grep -h "^\*\*Estado:\*\*" -- */tareas-tecnicas/BT-000{27..35}* | sort | uniq -c
      1 **Estado:** Borrador
      8 **Estado:** Ready
```

**Desde la 1.2: ocho `Ready`, cero `Borrador`, una `Descartada`.** BT-00028 no se destrabó: se
descartó, porque la decisión que faltaba ([`ADR-00009`](../Unidades-Entrega/GeometriaFactory-Api/05-Arquitectura-Tecnica/Adrs/ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md)) dice que la API autentica
personas y no aplicaciones, y que no existe una tercera clase de identidad a la que darle una clave. Las
otras ocho no cambian de resultado; tres de ellas (BT-00029, BT-00030, BT-00032) retiraron la dependencia
de BT-00028 y siguen cumpliendo el criterio 5 (verificación 3 de §6.9). Medido, no heredado:

```
$ git grep -h "^\*\*Estado:\*\*" -- */tareas-tecnicas/BT-000{27..35}* | sort | uniq -c
      1 **Estado:** Descartada
      8 **Estado:** Ready
```

---

## 4. Lote de detenciones — intención de producto

**Desde la 1.2 el lote está vacío.** La única detención que quedaba (BT-00028) se resolvió el 2026-09-12
por decisión del Product Owner —«no hay tercero — es re simple: un administrador, y luego usuarios
generales, que pueden ser cualquiera, entre estos alumnos»— asentada en [`ADR-00009`](../Unidades-Entrega/GeometriaFactory-Api/05-Arquitectura-Tecnica/Adrs/ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md)
(**Aceptado**) y en `PRODUCT-INTAKE` **4.4**: la API autentica personas, no aplicaciones. La ADR que la
detención pedía existe, y lo que dice es que **BT-00028 no se construye** (`Descartada` en su 1.2); las
cinco BT que esperaban su destrabe ya no esperan nada (`Mini-Plan.md` 3.1 §3.5). El texto original de la
detención se conserva a continuación como registro de qué se pidió y por qué.

---

*Texto de la 1.1:* Queda **una** detención (la 1.0 declaraba dos; la segunda, BT-00031, se retira en la 1.1 porque su
ausencia de fuente era falsa, ver la sección BT-00031 y §7). Exige una decisión de arquitectura
(categoría `05`), no de esta categoría, y no se resuelve leyendo:

1. **BT-00028 (autenticación por cliente).** La decisión de diseño ya está en
   `PRODUCT-INTAKE` §17.1.P.5 (API key o `client_credentials`, revocación por cliente), pero **no tiene
   ADR propia en `05`**. `ORIGEN DEL HECHO`: de esta corrida, al no encontrar fuente admitida. **SI NO
   RESPONDÉS**: BT-00028 permanece en `Borrador`, y con ella BT-00029, BT-00030 y BT-00032 (que
   dependen de BT-00028) no pueden **ejecutarse** aunque su ficha individual sea `Ready`, porque el
   orden de construcción del §4 del Mini-Plan las pone después de BT-00028. Destrabe: que la categoría
   05 emita una ADR de autenticación de clientes externos, análoga a `ADR-00003` para el ROPC de
   alumnos, y BT-00028 cite esa ADR.

La detención no se resuelve en este documento: requiere un artefacto nuevo de la categoría 05, que está
fuera del alcance de esta evaluación de DoR y de la planificación del tramo. Para BT-00028 la búsqueda
**sí** incluyó `SDD/Docs/Producto/Adrs/` en la 1.1: `grep -l -i "client_credentials\|api key\|clave de
cliente\|autenticaci" SDD/Docs/Producto/Adrs/*.md` no devuelve ningún archivo (ver §6, verificación 8), de modo que la
detención se sostiene.

---

## 5. Verificación de ausencia de ciclo entre las BT del tramo `k`

Comando y salida:

```
$ cd /tmp && cat > tramo-k-deps.txt << 'EOF'
BT-00027 BT-00028
BT-00028 BT-00029
BT-00028 BT-00030
BT-00027 BT-00032
BT-00028 BT-00032
BT-00029 BT-00032
BT-00030 BT-00032
BT-00032 BT-00034
BT-00032 BT-00035
EOF
$ tsort tramo-k-deps.txt; echo "exit: $?"
BT-00027
BT-00028
BT-00030
BT-00029
BT-00032
BT-00035
BT-00034
exit: 0
```

`tsort` produce un orden topológico y termina con código `0` (sin reportar ciclo en `stderr`).
`BT-00031` y `BT-00033` no tienen dependencias declaradas (`§4: Ninguna` en las dos fichas) y quedan
fuera del grafo por no tener aristas: trivialmente sin ciclo. La 1.1 repite la corrida leyendo las
aristas **desde las nueve fichas** en lugar de transcribirlas a mano (§6, verificación 6): mismo grafo,
mismo resultado.

---

## 6. Verificación tras la corrección

Corridas sobre el árbol de trabajo de la rama `fase-k/mini-plan-tramo-k` después de editar este
documento, `Mini-Plan.md` y `README.md` de `07-Plan-Sprint`, con las fichas en su versión 1.2. Los
scripts viven en el scratchpad de la corrida y no se versionan; el comando de cada una va al lado de su
salida (regla de la 13.12: toda afirmación de recuento lleva su comando). Las ediciones de los tres
documentos se hicieron por script con una **guarda**: aborta antes de escribir si alguna cabecera de
tabla no tiene el número de columnas esperado, y la verificación 5 confirma después que ninguna tabla
cambió de ancho. En la verificación 3, las coincidencias que quedan
son filas de control de cambios (fichas `BT-00035` v1.1/v1.2 y la fila 1.1 de este documento), que
declaran el error y no lo repiten como cita vigente, más la línea de este documento que lleva el
comando; en la 5 se listan las tablas de este documento y las cinco editadas de los otros dos (las
demás dieron `OK` sin cambio y se omiten por legibilidad); la 8 no estaba pedida y sostiene la
detención de §4.

```
### 1. Estados de las nueve fichas
$ git grep -h "^\*\*Estado:\*\*" -- */tareas-tecnicas/BT-000{27..35}* | sort | uniq -c
      1 **Estado:** Borrador
      8 **Estado:** Ready
$ git grep -H "^\*\*Estado:\*\*" -- */tareas-tecnicas/BT-000{27..35}* | sed "s|.*/\(BT-[0-9]*\)-.*Estado:\*\* |\1 |"
BT-00027 Ready
BT-00028 Borrador
BT-00029 Ready
BT-00030 Ready
BT-00031 Ready
BT-00032 Ready
BT-00033 Ready
BT-00034 Ready
BT-00035 Ready

### 2. Enlaces relativos de los tres documentos editados
$ for d in <los tres>; do grep -o "](\.\.\?/[^)#]*" $d | sed "s|](||" | sort -u | while read l; do [ -e "$(dirname $d)/$l" ] || echo "ROTO $d -> $l"; done; done; echo "rotos: $?"
enlaces relativos revisados: 17 distintos; rotos: 0

### 3. Cita vieja de ADR-00008 (sección equivocada, §1) fuera de filas de control de cambios
$ git grep -n "ADR-00008.*§1 punto 1" -- SDD ":!*/_legacy/*" | grep -v -E "^[^:]+:[0-9]+:\| [0-9]+\.[0-9]+ \|" | grep -v "^SDD/Docs/Audit/DoR-Tramo-k-2026-09-12.md:[0-9]*:\\$ git grep" | wc -l
0
(coincidencias totales, sin filtrar, y dónde:)
SDD/Docs/Audit/DoR-Tramo-k-2026-09-12.md:426
SDD/Docs/Audit/DoR-Tramo-k-2026-09-12.md:501
.../tareas-tecnicas/BT-00035-Politica-De-Deprecacion-En-Estrategia-Versionado.md:54
.../tareas-tecnicas/BT-00035-Politica-De-Deprecacion-En-Estrategia-Versionado.md:55

### 4. Caja temporal vieja en BT-00030 fuera de control de cambios
$ git grep -n -i "caja temporal: hasta que" -- "*/BT-00030*" | grep -v -E "^[^:]+:[0-9]+:\| [0-9]+\.[0-9]+ \|" | wc -l
0
(sin filtrar:)

### 5. Número de columnas de cada tabla de los tres documentos, HEAD contra árbol de trabajo
$ python3 tablas.py (cabeceras y columnas de cada tabla, comparando `git show HEAD:doc` con el archivo editado)
OK DoR-Tramo-k-2026-09-12.md l.61 3 col (HEAD=3), 6 filas: | # | Sí/No | Cita literal de la ficha |
OK DoR-Tramo-k-2026-09-12.md l.77 3 col (nueva), 2 filas: | # | Sí/No | Cita literal de la ficha (v1.2) |
OK DoR-Tramo-k-2026-09-12.md l.88 3 col (HEAD=3), 6 filas: | # | Sí/No | Cita literal de la ficha (v1.0) |
OK DoR-Tramo-k-2026-09-12.md l.113 3 col (HEAD=3), 6 filas: | # | Sí/No | Cita literal de la ficha (v1.0, antes de corre
OK DoR-Tramo-k-2026-09-12.md l.137 3 col (HEAD=3), 6 filas: | # | Sí/No | Cita literal de la ficha (v1.0, antes de corre
OK DoR-Tramo-k-2026-09-12.md l.171 3 col (HEAD=3), 6 filas: | # | Sí/No | Cita literal de la ficha (v1.0, antes de corre
OK DoR-Tramo-k-2026-09-12.md l.203 3 col (nueva), 6 filas: | # | Sí/No | Cita literal de la ficha (v1.2) |
OK DoR-Tramo-k-2026-09-12.md l.218 3 col (HEAD=3), 6 filas: | # | Sí/No | Cita literal de la ficha (v1.0, antes de corre
OK DoR-Tramo-k-2026-09-12.md l.240 3 col (HEAD=3), 6 filas: | # | Sí/No | Cita literal de la ficha (v1.0, antes de corre
OK DoR-Tramo-k-2026-09-12.md l.260 3 col (HEAD=3), 6 filas: | # | Sí/No | Cita literal de la ficha (v1.0, antes de corre
OK DoR-Tramo-k-2026-09-12.md l.283 3 col (HEAD=3), 6 filas: | # | Sí/No | Cita literal de la ficha (v1.0, antes de corre
OK DoR-Tramo-k-2026-09-12.md l.309 4 col (HEAD=4), 9 filas: | BT | Resultado | Corrección aplicada | Fuente admitida usa
OK DoR-Tramo-k-2026-09-12.md l.498 3 col (HEAD=3), 2 filas: | Versión | Fecha | Cambios |
OK Mini-Plan.md l.459 8 col (HEAD=8), 8 filas: | Etapa | ID | Tipo | Descripción corta | Prioridad | Estima
OK Mini-Plan.md l.478 3 col (HEAD=3), 1 filas: | ID | Qué le falta | Qué la destraba |
OK Mini-Plan.md l.852 3 col (HEAD=3), 3 filas: | Versión | Fecha | Cambios |
OK README.md l.41 2 col (HEAD=2), 9 filas: | Aspecto | Valor al 2026-09-12 |
OK README.md l.74 3 col (HEAD=3), 5 filas: | Versión | Fecha | Descripción |

### 6. tsort sobre las dependencias leídas de las nueve fichas
$ for f in .../tareas-tecnicas/BT-000{27..35}*; do id=$(basename $f | cut -c1-8); sed -n "/^## 4. Dependencias/,/^## 5/p" $f | grep -o "BT-000[0-9][0-9]" | while read d; do echo "$d $id"; done; done > tramo-k-deps.txt
$ cat tramo-k-deps.txt
BT-00027 BT-00028
BT-00028 BT-00029
BT-00028 BT-00030
BT-00027 BT-00032
BT-00028 BT-00032
BT-00029 BT-00032
BT-00030 BT-00032
BT-00032 BT-00034
BT-00032 BT-00035
$ tsort tramo-k-deps.txt; echo "exit: $?"
BT-00027
BT-00028
BT-00030
BT-00029
BT-00032
BT-00035
BT-00034
exit: 0

### 7. Comprometidas de Mini-Plan §3.5 contra las fichas en Ready
$ diff <(sed -n "/^### 3.5/,/^## 4/p" Mini-Plan.md | grep -o "^| \`k\` | BT-[0-9]*" | grep -o "BT-[0-9]*" | sort) <(git grep -l "^\*\*Estado:\*\* Ready" -- */tareas-tecnicas/BT-000{27..35}* | grep -o "BT-000[0-9]*" | sort) && echo "coinciden"
coinciden
(la lista, y su tamaño:)
BT-00027 BT-00029 BT-00030 BT-00031 BT-00032 BT-00033 BT-00034 BT-00035 
8

### 8. BT-00028: la detención se sostiene también mirando las ADR de Producto
$ grep -l -i "client_credentials\|api key\|clave de cliente\|autenticaci" SDD/Docs/Producto/Adrs/*.md; echo "exit: $?"
exit: 1
```

### 6.9 Verificación tras `ADR-00009` (1.2)

Corridas sobre el árbol de trabajo de la rama `fase-k/adr-00009-autentica-personas`, base `9d33d6d`
(`main`), después de editar la ADR, el intake (4.4), las dos mesas, las fichas BT-00027/28/29/30/32/34,
`Backlog-Tecnico.md` (3.2), `Mini-Plan.md` (3.1), `README.md` de 07 (2.2) y este documento. Todas las
ediciones fueron por script con **guarda**: cada cadena a reemplazar debe aparecer exactamente una vez
y ninguna fila de tabla puede cambiar de ancho; si una premisa falla, el script aborta sin escribir. Son
las siete verificaciones del mandato más la del término «tercero».

```
### 1. Estados de BT-00027…35
$ git grep -h "^\*\*Estado:\*\*" -- */tareas-tecnicas/BT-000{27..35}* | sort | uniq -c
      1 **Estado:** Descartada
      8 **Estado:** Ready

### 2. BT-00028 en las fichas 29–35 y en el catálogo: sólo en filas de control de cambios o con «descartada»
$ git grep -n "BT-00028" -- $TT/BT-000{29..35}* $API/06-Backlog-Tecnico/Backlog-Tecnico.md | grep -v -i "descartada" | grep -v "^[^:]*:[0-9]*:| [0-9]\.[0-9] | 2026-09-12 |"
(vacío = ninguna mención vigente fuera de esas dos formas; exit del grep final: 1)

### 3. tsort sobre las dependencias leídas del §4 de las fichas en Ready
$ for f in $TT/BT-000{27..35}*; do grep -q "^\*\*Estado:\*\* Ready" "$f" || continue; id=$(basename $f | cut -c1-8); sed -n "/^## 4. Dependencias/,/^## 5/p" $f | grep -o "BT-000[0-9][0-9]" | while read d; do echo "$d $id"; done; done > tramo-k-deps.txt; cat tramo-k-deps.txt; tsort tramo-k-deps.txt; echo "exit: $?"
BT-00027 BT-00032
BT-00029 BT-00032
BT-00030 BT-00032
BT-00032 BT-00034
BT-00032 BT-00035
BT-00027
BT-00029
BT-00030
BT-00032
BT-00035
BT-00034
exit: 0

### 4. Todo enlace relativo de los documentos editados resuelve
$ for f in <editados>; do d=$(dirname $f); grep -o "](\.\{1,2\}/[^)#]*)" $f | sed "s/](//;s/)//" | sort -u | while read l; do test -e "$d/$l" || echo "ROTO en $f: $l"; done; done; echo "fin"
fin (vacío arriba = todos resuelven; 16 documentos)

### 5. Número de columnas de cada tabla, base 9d33d6d contra árbol de trabajo (sólo tablas de documentos editados)
$ python3 tablas.py
tablas con problema: 0

OK Decisiones-Arquitectura.md: 14 tablas sin cambio de ancho (omitidas por legibilidad)
OK README.md: 11 tablas sin cambio de ancho (omitidas por legibilidad)
OK PRODUCT-INTAKE-Fabrica-De-Geometria.md: 52 tablas sin cambio de ancho (omitidas por legibilidad)
OK Mesa-2026-09-12-ciclo-2.md: 7 tablas sin cambio de ancho (omitidas por legibilidad)
OK Mesa-2026-09-12.md: 7 tablas sin cambio de ancho (omitidas por legibilidad)
OK DoR-Tramo-k-2026-09-12.md: 13 tablas sin cambio de ancho (omitidas por legibilidad)
OK Backlog-Tecnico.md: 30 tablas sin cambio de ancho (omitidas por legibilidad)
OK Mini-Plan.md: 26 tablas sin cambio de ancho (omitidas por legibilidad)
OK BT-00027-Reescribir-Adr-00008-Adoptando-V-Major-Para-La-Superficie-Publica.md: 2 tablas sin cambio de ancho (omitidas por legibilidad)
OK BT-00028-Autenticacion-Por-Cliente-Para-Terceros-Api-Key-O-Client-Credentials.md: 2 tablas sin cambio de ancho (omitidas por legibilidad)
OK BT-00029-Rate-Limiting-Por-Clave-O-Por-Ip.md: 2 tablas sin cambio de ancho (omitidas por legibilidad)
OK BT-00030-Declarar-Cors-Condicional-Al-Cliente-Que-Aparezca-D-01.md: 2 tablas sin cambio de ancho (omitidas por legibilidad)
OK BT-00032-Versionar-Las-Rutas-Publicas-Bajo-V1.md: 2 tablas sin cambio de ancho (omitidas por legibilidad)
OK BT-00034-Sample-De-Onboarding-Para-Un-Cliente-Externo-Contra-V1.md: 2 tablas sin cambio de ancho (omitidas por legibilidad)
OK ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md l.48 3 col (nueva), 5 filas: | Alternativa | Pros | Contras |
OK ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md l.79 3 col (nueva), 5 filas: | Métrica | Objetivo | Cómo se mide |
OK ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md l.144 3 col (nueva), 1 filas: | Versión | Fecha | Descripción |
OK PRODUCT-INTAKE-Fabrica-De-Geometria.md l.1932 4 col (base=4), 50 filas, filas con otro ancho: 3 (preexistentes en la base, no tocadas): | Versión | Fecha | Cambios | Autor |
OK Backlog-Tecnico.md l.586 3 col (base=3), 6 filas, filas con otro ancho: 1 (preexistentes en la base, no tocadas): | Versión | Fecha | Cambios |
OK Mini-Plan.md l.478 3 col (nueva), 1 filas: | ID | Por qué no se construye | Fundamento |
### 6. Intake: la frase superada sólo puede quedar en el control de cambios
$ sed "/^## Control de cambios/,\$d" SDD/Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md | grep -c "Los clientes externos no usan este endpoint"
0

### 7. Comprometidas de Mini-Plan §3.5 contra las fichas en Ready
$ diff <(sed -n "/^### 3.5/,/^## 4/p" Mini-Plan.md | grep -o "^| \`k\` | BT-[0-9]*" | grep -o "BT-[0-9]*" | sort) <(git grep -l "^\*\*Estado:\*\* Ready" -- */tareas-tecnicas/BT-000{27..35}* | grep -o "BT-000[0-9]*" | sort) && echo "coinciden"
coinciden
BT-00027 BT-00029 BT-00030 BT-00031 BT-00032 BT-00033 BT-00034 BT-00035 
8

### 8. «tercero» en la ADR nueva, el intake y las fichas 27–35: sólo en frases que lo descartan o en filas de control de cambios
$ git grep -n -i "tercero" -- <ADR> <intake> $TT/BT-000{27..35}* | cut -c1-<ancho>
ADR-00009-La-Api-Autentica-Personas-No-A:17 [cuerpo] …tarea técnica sin fuente: `BT-00028` —«Autenticación por cliente para terceros»— no pasó la Definition of Ready porque l…
ADR-00009-La-Api-Autentica-Personas-No-A:23 [cuerpo] …Y, sobre un tercero que no sea del producto:…
ADR-00009-La-Api-Autentica-Personas-No-A:25 [cuerpo] …> «No hay tercero — es re simple: un administrador, y luego …
ADR-00009-La-Api-Autentica-Personas-No-A:37 [cuerpo] …il— **no cambia la autenticación**. El Product Owner descartó modelar terceros: quien usa la API es una persona con uno …
ADR-00009-La-Api-Autentica-Personas-No-A:94 [cuerpo] …cklog-Tecnico/tareas-tecnicas/BT-00028-Autenticacion-Por-Cliente-Para-Terceros-Api-Key-O-Client-Credentials.md) (descart…
BT-00027-Reescribir-Adr-00008-Adoptando-:22 [cuerpo] …d) vigente. La premisa que `ADR-00008` reescribe —«no hay clientes de terceros»— no vive sólo en esa ADR: también la sos…
BT-00027-Reescribir-Adr-00008-Adoptando-:28 [cuerpo] …- la premisa «no hay clientes de terceros» se reescribe o se declara superada en el…
BT-00027-Reescribir-Adr-00008-Adoptando-:76 [control de cambios] …contra el árbol.** La ficha reescribía la premisa «no hay clientes de terceros» sólo en `ADR-00008`, pero la misma premi…
BT-00027-Reescribir-Adr-00008-Adoptando-:77 [control de cambios] …demás a la lista de §3 una constancia: la premisa «no hay clientes de terceros» que esta tarea reescribe se lee ahora co…
BT-00028-Autenticacion-Por-Cliente-Para-:1 [cuerpo] …# BT-00028 — Autenticación por cliente para terceros (API key o `client_credentials`)…
BT-00028-Autenticacion-Por-Cliente-Para-:5 [cuerpo] …**Documento:** BT-00028-Autenticacion-Por-Cliente-Para-Terceros-Api-Key-O-Client-Credentials.md…
BT-00028-Autenticacion-Por-Cliente-Para-:18 [cuerpo] …Autenticación por cliente para terceros (API key o `client_credentials`).…
BT-00028-Autenticacion-Por-Cliente-Para-:60 [control de cambios] …, **Aceptado** por decisión del Product Owner del 2026-09-12: «no hay tercero — es re simple: un administrador, y luego …
BT-00030-Declarar-Cors-Condicional-Al-Cl:57 [control de cambios] …io que sea JavaScript de navegador desde otro origen**; sin mención a terceros. La caja temporal (criterio 6 de la DoR) …
PRODUCT-INTAKE-Fabrica-De-Geometria.md:789 [cuerpo] …dido** [DECISIÓN 2026-09-12, PO, `ADR-00009`, cierra `D-01`]: «no hay tercero — es re simple: un administrador, y luego …
PRODUCT-INTAKE-Fabrica-De-Geometria.md:794 [cuerpo] …y fija «sin versionado de rutas» sobre la premisa «no hay clientes de terceros», ya no cierta) y `Estrategia-Versionado.…
PRODUCT-INTAKE-Fabrica-De-Geometria.md:798 [cuerpo] … no cambia** [DECISIÓN 2026-09-12]: la afirmación «no hay clientes de terceros», cierta hasta la versión 4.2, **deja de …
PRODUCT-INTAKE-Fabrica-De-Geometria.md:886 [cuerpo] …ción propia del producto que actúa en nombre de una de ellas: «no hay tercero — es re simple: un administrador, y luego …
PRODUCT-INTAKE-Fabrica-De-Geometria.md:1176 [cuerpo] …hay versionado de endpoints en este alcance porque no hay clientes de terceros.…
PRODUCT-INTAKE-Fabrica-De-Geometria.md:1934 [control de cambios] …entre alumno y usuario general; no importa que sea alumno», y «no hay tercero — es re simple: un administrador, y luego …
PRODUCT-INTAKE-Fabrica-De-Geometria.md:1935 [control de cambios] …, y `GeometriaFactory-Contracts`) deja de afirmar «no hay clientes de terceros»; §17.1.P.5 suma autenticación por client…
```

---

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | Emisión inicial. Evalúa `BT-00027` a `BT-00035` contra los seis criterios de `Definition-Of-Ready.md` §2.1, con cita literal por criterio. Corrige siete fichas (`27` sin cambio de contenido; `29`, `30`, `32`, `33`, `34`, `35` con corrección de fuente o de criterio de aceptación) y deja dos en `Borrador` (`28`, `31`) por ausencia de fuente admitida, declarada en el lote de detenciones de §4. Verifica ausencia de ciclo entre las nueve BT con `tsort`. |
| 1.1 | 2026-09-12 | **Corrige cuatro afirmaciones de la 1.0, detectadas por el orquestador contra el árbol** (las fichas ya iban en v1.2 al momento de esta fila; este documento las alcanza). **(a) BT-00031: la ausencia afirmada era falsa.** La 1.0 declaró «no existe fuente admitida» sobre una búsqueda que se limitó a `05` (`Arquitectura-Unidad-Entrega.md`) de la unidad de entrega, la superficie de `02` y el intake §15, y **no miró `SDD/Docs/Producto/Adrs/`**, cuyas ADR de nivel Producto también son «una ADR» para el criterio 1. Existe [`ADR-08008`](../Producto/Adrs/ADR-08008-La-Superficie-HTTP-Se-Describe-Y-El-Explorador-No-Se-Publica-Solo.md) §2 punto 2, `Aceptado`, que declara exactamente la decisión. Se reevalúan los seis criterios con la ficha v1.2 y pasa a **Ready**; sale del lote de detenciones de §4, que queda con **sólo BT-00028**, y §0 pasa de «dos huecos» a uno. **(b) BT-00030: el «Sí» del criterio 6 era una equivalencia no admitida.** «Hasta que `D-01` cierre» no es una etapa ni un punto de control (`D-01` es un ítem diferido sin evento ocurrido); la 1.0 lo dio por equivalente. La fila pasa a «Sí, tras corregir» con la cita nueva de la ficha v1.2: «la etapa `k`, que cierra en su punto de control». **(c) BT-00035: citaba otra sección.** «`ADR-00008` §1 punto 1» apuntaba a «Contexto»; la frase vive en §2 «Decisión», regla 1 (línea 27). Se corrigen la sección BT-00035 y la fila de §3. **(d) BT-00027: el criterio de aceptación alcanzaba un documento de dieciséis lugares.** La ficha reescribía la premisa sólo en `ADR-00008`; la v1.2 fija el conjunto medido con `git grep` (dieciséis lugares en once documentos) y declara, por el criterio 6, que la decisión alcanza fuera de la unidad (`ADR-08003`, nivel Producto, obliga a `GeometriaFactory-Contracts`); la 1.0 respondía «N/A» al criterio 6 por tipo `docs` y omitía esa segunda mitad del criterio. §3 pasa a **8 `Ready`, 1 `Borrador`**, medido con `git grep`. Se agrega §6 «Verificación tras la corrección» con siete comandos y sus salidas; el control de cambios pasa a §7. Sube minor: corrige resultados de la evaluación sin cambiar el método ni los criterios. |
| 1.2 | 2026-09-12 | **La detención de §4 se resolvió: el lote queda vacío.** El Product Owner decidió el 2026-09-12 que la API autentica personas y no aplicaciones ([`ADR-00009`](../Unidades-Entrega/GeometriaFactory-Api/05-Arquitectura-Tecnica/Adrs/ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md), **Aceptado**; `PRODUCT-INTAKE` **4.4**): no hay tercera clase de identidad, luego **BT-00028 no se construye** y pasa a `Descartada` (ficha 1.2). §0 y §3 registran el desenlace sin borrar el resultado de la 1.1; §4 conserva el texto de la detención como registro, precedido por cómo se resolvió; §6 suma la verificación 6.9 con las siete comprobaciones del mandato (estados 8/0/1, `BT-00028` sólo en control de cambios o con «descartada», `tsort` con salida `0` sobre cinco aristas, enlaces, anchos de tabla, frase superada del intake, comprometidas == `Ready`) y la del término «tercero». Sube minor: no cambia el método ni los criterios; cambia el resultado de una ficha por una decisión de producto posterior. |
