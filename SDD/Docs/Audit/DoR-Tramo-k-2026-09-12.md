# DoR del tramo `k` — evaluación criterio por criterio · 2026-09-12

**Producto:** Fábrica de Geometría
**Documento:** DoR-Tramo-k-2026-09-12.md
**Versión:** 1.0
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
hueco se declara; no se inventa.

**ORIGEN DEL HECHO**: de esta corrida, evaluando fichas ya emitidas por una corrida anterior
(`Apertura-Fase-k-2026-09-12.md`, base `9167e68`). **SI NO RESPONDÉS**: dos huecos (BT-00028, BT-00031)
no tienen fuente admitida en el árbol y requieren que la categoría `05` (arquitectura) emita el
artefacto que falta —no una decisión de esta categoría—; se declaran en el lote de detenciones de §4 y
quedan en `Borrador`.

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

**Resultado: Ready.** Sin corrección de contenido; pasa a `Ready` en v1.1.

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
| 6 | Sí | Tipo `indagación`; §6: «Caja temporal: hasta que D-01 cierre» — no está en horas, y equivale al punto de control que la cierra (la decisión de quién es el cliente) |

**Fuente admitida encontrada**: `05` §9.1 (`GeometriaFactory-Api`), riesgo **«Que se agregue un punto
de acceso pensado para el navegador, o se configure el intercambio de origen cruzado»** (impacto muy
alto, probabilidad baja), mitigado hoy por «las tres ausencias declaradas de la superficie de 02» y por
«el hecho de que el único cliente legítimo esté declarado en el manifiesto y en el grafo». Es un riesgo
de `05` §9 — categoría admitida — y describe exactamente la condición que esta tarea declara (CORS no
aplica hoy, condicionado a que aparezca un cliente de navegador).

**Corrección aplicada**: se agrega esa cita en §2 y en §7, conservando `D-01` como condición adicional
(quién es el cliente, no si CORS aplica hoy). Ningún criterio de aceptación cambia. La excepción de tipo
indagación de §3.1 no hacía falta invocarla: el criterio 3 ya estaba satisfecho con un criterio
verificable de antemano.

**Resultado: Ready** (v1.1, tras la corrección).

---

### BT-00031 — Publicar OpenAPI/Scalar en el ambiente de producción

| # | Sí/No | Cita literal de la ficha (v1.0) |
| --- | --- | --- |
| 1 | **No** | §2 y §7: «`Mesa-2026-09-12-ciclo-2.md` §4 ítem 6» — únicamente un registro de mesa |
| 2 | **No** | §7: «ya construido, sólo cambia la configuración de publicación» — no cita ADR/puerta/punto abierto |
| 3 | Sí | §3: tres criterios verificables (`Documentacion__Publicada=true`, `/openapi/v1.json` responde `200`, Scalar sin credencial) |
| 4 | N/A | §7: «Puntos de acceso que toca: Ninguno» |
| 5 | Sí | §4: «Ninguna» |
| 6 | N/A | Tipo `devops`, no indagación |

**Búsqueda de fuente admitida alternativa**: `grep -n "Scalar\|OpenAPI\|openapi" 05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`
no encuentra ninguna coincidencia; tampoco la superficie de `02` ni las cinco reglas de delivery del
intake §15 mencionan la publicación pública de la documentación o la variable
`Documentacion__Publicada`. **No existe fuente admitida.**

**Resultado: Borrador.** Falta: criterio 1 y, en consecuencia, criterio 2. Se declara en la ficha
(v1.1) y en el lote de detenciones de §4.

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
§1 punto 1, vigente: «no hay deprecación gradual: no hay a quién dársela». Es exactamente la premisa que
`BT-00027` reescribe y que esta política ejecuta sobre `Estrategia-Versionado.md` una vez reescrita.

**Corrección aplicada**: se agrega esa cita en §2 y en §7. Ningún criterio de aceptación cambia.

**Resultado: Ready** (v1.1, tras la corrección).

---

## 3. Resumen de resultados

| BT | Resultado | Corrección aplicada | Fuente admitida usada |
| --- | --- | --- | --- |
| BT-00027 | **Ready** | Ninguna | `ADR-00008` (ya citada) |
| BT-00028 | **Borrador** | Ninguna posible | — (no existe en el árbol) |
| BT-00029 | **Ready** | Cita corregida | `05` §8.1, «Caudal sostenido» (`ADR-00005`) |
| BT-00030 | **Ready** | Cita agregada | `05` §9.1, riesgo de CORS/navegador |
| BT-00031 | **Borrador** | Ninguna posible | — (no existe en el árbol) |
| BT-00032 | **Ready** | Criterio de aceptación agregado (criterio 4) | `05` §3.4 (los quince puntos) |
| BT-00033 | **Ready** | Cita corregida (capa) | `05` §11.3 `PA-06` (Application) y §11.2 `PA-04` (Domain) |
| BT-00034 | **Ready** | Cita agregada | `05` §8, última fila (`ADR-00008`), por analogía con BT-00020 |
| BT-00035 | **Ready** | Cita agregada | `ADR-00008` §1 punto 1 |

**Siete de nueve pasan a `Ready`; dos quedan en `Borrador`** por ausencia de fuente admitida en el
árbol, no por defecto de redacción.

---

## 4. Lote de detenciones — intención de producto

Las dos detenciones exigen una decisión de arquitectura (categoría `05`), no de esta categoría, y no se
resuelven leyendo:

1. **BT-00028 (autenticación por cliente).** La decisión de diseño ya está en
   `PRODUCT-INTAKE` §17.1.P.5 (API key o `client_credentials`, revocación por cliente), pero **no tiene
   ADR propia en `05`**. `ORIGEN DEL HECHO`: de esta corrida, al no encontrar fuente admitida. **SI NO
   RESPONDÉS**: BT-00028 permanece en `Borrador`, y con ella BT-00029, BT-00030 y BT-00032 (que
   dependen de BT-00028) no pueden **ejecutarse** aunque su ficha individual sea `Ready`, porque el
   orden de construcción del §4 del Mini-Plan las pone después de BT-00028. Destrabe: que la categoría
   05 emita una ADR de autenticación de clientes externos, análoga a `ADR-00003` para el ROPC de
   alumnos, y BT-00028 cite esa ADR.
2. **BT-00031 (publicar OpenAPI/Scalar en producción).** Ninguna fuente del árbol declara la
   publicación pública de la documentación como NFR, riesgo, componente, punto abierto o regla de
   delivery. `ORIGEN DEL HECHO`: de esta corrida. **SI NO RESPONDÉS**: BT-00031 permanece en `Borrador`
   y no se compromete en el tramo `k`; no bloquea a ninguna otra BT (`Dependencias: Ninguna`, y ninguna
   otra BT depende de ella). Destrabe: que la categoría 05 registre esta decisión (un NFR nuevo en §8 o
   una fila en §11) o que el Product Owner la eleve como punto abierto explícito.

Ninguna de las dos detenciones se resuelve en este documento: ambas requieren un artefacto nuevo de la
categoría 05, que está fuera del alcance de esta evaluación de DoR y de la planificación del tramo.

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
fuera del grafo por no tener aristas: trivialmente sin ciclo.

---

## 6. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | Emisión inicial. Evalúa `BT-00027` a `BT-00035` contra los seis criterios de `Definition-Of-Ready.md` §2.1, con cita literal por criterio. Corrige siete fichas (`27` sin cambio de contenido; `29`, `30`, `32`, `33`, `34`, `35` con corrección de fuente o de criterio de aceptación) y deja dos en `Borrador` (`28`, `31`) por ausencia de fuente admitida, declarada en el lote de detenciones de §4. Verifica ausencia de ciclo entre las nueve BT con `tsort`. |
