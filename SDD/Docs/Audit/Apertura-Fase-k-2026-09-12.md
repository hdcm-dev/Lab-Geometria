# Apertura de la fase `k` — evidencia de la corrida · 2026-09-12

**Producto:** Fábrica de Geometría
**Documento:** Apertura-Fase-k-2026-09-12.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-09-12
**Autor:** Orquestador SDD (Scrum Master / Agile Coach senior, AG-00060)
**Base de la corrida:** `9167e68f3a3f75992a0fff338ffcb00cd5936bc4` (`main`, limpio, al día; verificado con `git rev-parse HEAD` antes de abrir la rama `fase-k/backlog-tecnico-v2`)

---

## 1. Qué dispara esta apertura

El evento de `Rules-Backlog-Tecnico.md` §3.6: la entrada **4.3** del control de cambios de
`PRODUCT-INTAKE-Fabrica-De-Geometria.md` (2026-09-12), donde el Product Owner asienta el desenlace de
`E-02` («la API debe ser expuesta públicamente») y `E-04` (nomenclatura del versionado), convertido en
plan de diez ítems por `SDD/Docs/Audit/Mesa-2026-09-12-ciclo-2.md` §4 y §8. No hay ninguna corrida de
orquestador en curso: el intake es documento humano (`Migracion-Rules.md` §4.4, `Master-Prompt.md`
§13.1) y esta apertura del backlog técnico es la consecuencia declarada de esa escritura, no una
migración estructural.

## 2. Criterio de §3.6: ¿el hecho es estructural?

**Criterio:** el hecho nuevo modifica una fila de la matriz de `Roadmap-Producto.md` §3 —incluido el
contenido de una fila ya existente— o el conjunto de proyectos de código del manifiesto.

**Verificación, con comando:**

```
$ git show 706db57 -- SDD/Docs/00-Contexto/Roadmap-Producto.md | head -25
```

Salida (resumen): el commit `706db57` («Roadmap: abre la fase k…») agrega una fila nueva a la tabla de
§2.1 y a la matriz de §3 de `Roadmap-Producto.md`, pasando la versión del documento de **1.10** a
**1.11**. La fila `k` («Exposición pública de la API y versionado del contrato») no existía antes de
este commit.

**Resultado: estructural.** El hecho agrega una fila a la matriz de §3 — el caso más fuerte del
criterio, alta de fila y no sólo cambio de contenido —, de modo que corresponde el paso a `v2.0` (en
este destino, concretamente de `2.2` a `3.0`, porque el documento ya venía numerado por encima de
`1.x`) sobre `Backlog-Tecnico.md` de la unidad de entrega que ese trabajo alcanza.

**Por unidad de entrega:**

| Unidad de entrega | ¿La alcanza la fila `k`? | Fundamento |
| --- | --- | --- |
| `GeometriaFactory-Api` | Sí | Los diez ítems del plan de la mesa (`ADR-00008`, autenticación por cliente, rate limiting, CORS, OpenAPI, `/v1/`, MinVer, sample, deprecación) son decisiones y tareas del host REST y de sus capas internas — todas viven en `05-Arquitectura-Tecnica` y `06-Backlog-Tecnico` de esta unidad. |
| `GeometriaFactory-Web` | No | Ningún ítem del plan de la mesa toca el front Blazor. Verificado leyendo `Mesa-2026-09-12-ciclo-2.md` §4 ítem por ítem: los diez son de exposición del contrato REST hacia un cliente externo, no del consumo que `GeometriaFactory-Web` ya hace del contrato (que sigue siendo servidor a servidor con `Bearer` JWT, sin cambios). |

## 3. Identificador libre: bloque `BT-00xxx` de `GeometriaFactory-Api`

`BT` es de ámbito **producto** (`Root-Rules.md` §9.1) y este destino reparte su numeración por
proyecto de código dentro del documento consolidado de la unidad (`00xxx` Api, `02xxx` Domain, `04xxx`
Application, `06xxx` Infrastructure, `10xxx` Web, `12xxx` Visor — verificado por inspección, no hay un
documento de mapa de rangos separado en `PRODUCT-MANIFEST-Fabrica-De-Geometria.md`). Los diez ítems del
plan son del proyecto `GeometriaFactory-Api`, así que la numeración nueva sigue dentro de su bloque
`00xxx`.

```
$ grep -oE "BT-00[0-9]{3}" SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/06-Backlog-Tecnico/_legacy/2026-09-12/Backlog-Tecnico-v2.2.md | sort -u | tail -3
BT-00024
BT-00025
BT-00026
```

**Siguiente identificador libre: `BT-00027`.** Verificado además contra todo el árbol, para descartar
colisión con cualquier otro documento (vivo o `_legacy/`) que ya use `BT-00027` a `BT-00035`:

```
$ grep -rl "BT-00027\|BT-00028\|BT-00029\|BT-00030\|BT-00031\|BT-00032\|BT-00033\|BT-00034\|BT-00035" SDD/Docs/Unidades-Entrega/ SDD/Docs/Audit/
(sin salida: cero colisiones antes de esta apertura)
```

Se asignaron **nueve** identificadores nuevos, `BT-00027` a `BT-00035`, uno por cada ítem del plan de
la mesa que produce tarea técnica (los diez ítems menos el ítem 2, que es el ítem diferido `D-01` y no
genera `BT`).

## 4. Por qué `Product-Backlog.md` de `GeometriaFactory-Api` no se reabre

`Rules-Backlog-Tecnico.md` §3.6 nombra a los dos documentos —`Product-Backlog.md` y
`Backlog-Tecnico.md`— como los que el evento puede reabrir. Se aplicó el mismo criterio de fondo del
§4.5/§6 de la regla (una `BT` sin historia consumidora se justifica como infraestructura compartida
con fuente upstream declarada, sin que eso obligue a inventar una `US`): las nueve tareas nuevas
(BT-00027 a BT-00035) no tienen una historia de usuario que las consuma porque **no son una capacidad
que un alumno, un administrador o el propio front ejerzan**: son la condición de acceso de un cliente
externo que hoy no está identificado (`D-01`, todavía sin tipo ni nombre). Es la misma figura que ya
usan, sin historia de usuario, once de las veintiséis tareas previas del mismo proyecto de código
(BT-00001, BT-00004, BT-00005, BT-00006, BT-00007, BT-00012, BT-00015, BT-00021, BT-00022, BT-00025 y
BT-00026). `Product-Backlog.md` **queda en su versión vigente 4.5, sin tocar**.

## 5. Por qué `GeometriaFactory-Web` no se reabre

Ninguno de los diez ítems del plan (`Mesa-2026-09-12-ciclo-2.md` §4) menciona ni afecta al proyecto
`GeometriaFactory-Web` ni a `GeometriaFactory-Visor`. `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` §2.A
declara que `GeometriaFactory-Web` consume la API «por HTTP con `Bearer` JWT, servidor a servidor», y
esa forma de consumo no cambia: la autenticación por cliente (API key / `client_credentials`) que
suma `E-02` es un mecanismo **adicional**, para un tercero, y no reemplaza el ROPC que ya usan los
alumnos ni el canal servidor a servidor que ya usa `GeometriaFactory-Web`. `Backlog-Tecnico.md` y
`Product-Backlog.md` de `GeometriaFactory-Web` **quedan en sus versiones vigentes (2.3 y la que
corresponda), sin tocar**.

## 6. No hay `Product-Backlog.md` de categoría de especificación funcional aparte

El destino no tiene un `Product-Backlog.md` de nivel producto fuera de las dos unidades de entrega: cada
unidad tiene el suyo (`GeometriaFactory-Api/06-Backlog-Tecnico/Product-Backlog.md` y el homónimo de
`GeometriaFactory-Web`), y ninguno de los dos corresponde reabrir por §4 y §5 de este documento.

## 7. Ítem diferido `D-01` — no se duplica

`D-01` (quién es el cliente externo) ya está registrado con la forma completa de `Root-Rules.md` §12.2
en dos lugares: `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §9/§17.1.P.3 y
`SDD/Docs/Audit/Mesa-2026-09-12-ciclo-2.md` §8, con su ciclo de origen (**mesa ciclo 2, 2026-09-12,
producto, base `5c95dab`**). Por `Master-Prompt.md` §8.2 («lo que se puede ubicar en un registro ya
vigente no se declara aparte»), este documento no lo repite: lo cita desde
`Backlog-Tecnico.md` **3.0** §1.1 y desde la fila `BT-00030`, que es la única tarea nueva que depende
de su cierre.

## 8. Lote de detenciones (`Master-Prompt.md` §8.1)

**Sin detenciones.** Todo lo que esta apertura necesitaba decidir tenía respuesta en el árbol: el
criterio de estructuralidad se resolvió con `git show` sobre la base; el rango de identificadores se
resolvió contando el bloque `00xxx` vigente; las tres decisiones de producto que condicionan el
alcance (`D-01`, `D-02`, `D-03`) ya están asentadas por el Product Owner en `PRODUCT-INTAKE` 4.3 y en
`Mesa-2026-09-12-ciclo-2.md` §8. No hay ninguna pregunta que dependa de una intención de producto
todavía no declarada.

## 9. Lo no verificado

- ~~**El umbral de treinta `BT` de `Rules-Backlog-Tecnico.md` §3.3**~~ **Resuelto por §11 de este mismo
  documento, misma corrida.** Esta entrada decía que no quedaba claro si el umbral se mide por unidad
  de entrega o por proyecto de código dentro de un documento consolidado, y que resolverlo excedía el
  mandato de esta apertura. Eso era correcto en el momento en que se escribió —el hecho que lo dispara
  es otro, y el §8.1 no admite mezclar dos hechos en una corrida— pero **la lectura por proyecto de
  código ya regía de hecho en el propio documento** (`Backlog-Tecnico.md` §1.2 a §1.4, sin ambigüedad) y
  la apertura que esta misma entrada describe fue la que cruzó el umbral para `GeometriaFactory-Api`.
  Eso convierte el hecho en **de la corrida** y no en uno nuevo: §11 lo trata como autocorrección sobre
  el conjunto, con la forma de `Master-Prompt.md` §8.1.
- **`Plan-Sprint` (07)**: no se generó. `Rules-Backlog-Tecnico.md` §3.6 no exige emitirlo como
  consecuencia directa del mismo evento — exige al menos una `BT` con criterio de aceptación o un ítem
  diferido, que esta apertura ya produjo — así que queda como siguiente paso declarado, no como hueco
  de esta corrida.
- **La batería de integración contra las rutas `/v1/`, la carga sintética del rate limiting y la
  restauración de prueba del SQLite (`RN-B8`)**: nada de esto se ejecutó, porque las tareas que lo
  exigen (BT-00029, BT-00032) todavía no entraron a un sprint.

## 10. Siguiente paso declarado

1. Planificar el sprint que ejecuta EP-T06 (`07-Plan-Sprint/Mini-Plan.md` de `GeometriaFactory-Api`),
   respetando el orden que la propia mesa fijó en su «Refutación aplicada»: 6 y 2 (acá, `D-01`) se
   pueden adelantar sin riesgo; 3, 4, 5 y 7 no se saltan.
2. Ejecutar BT-00027 (reescritura de `ADR-00008` o alta de `ADR-00009`) y BT-00035 (entrada nueva en
   `Estrategia-Versionado.md`): son los dos documentos que esta apertura declara fuera de alcance a
   propósito.
3. Cuando el Product Owner cierre `D-01` (aparezca el primer cliente externo real), reabrir BT-00030
   para decidir si corresponde una política CORS por origen explícito.

## 11. Autocorrección de la misma corrida: las 35 `BT` del bloque `00xxx` pasan a archivo individual

**Qué la dispara.** El §9 de este mismo documento, en su versión 1.0, dejó sin resolver si el umbral de
treinta `BT` de `Rules-Backlog-Tecnico.md` §3.3 se mide por unidad de entrega o por proyecto de código.
Revisando `Backlog-Tecnico.md` §1.2, §1.3 y §1.4 —que ya escriben, sin ambigüedad, «el proyecto de
código está por debajo del umbral de treinta»— la lectura por proyecto de código **ya regía de hecho**
en este mismo documento antes de esta apertura, y `Audit/D-06-07-Backlog-Siete-Proyectos-r1.md` l.306 la
confirma de forma independiente sobre el backlog de otra unidad de entrega del mismo producto. Con esa
lectura, la apertura de la fase `k` —que llevó el bloque `00xxx` de 26 a **35** `BT`— cruzó el umbral que
esta misma corrida no había cruzado antes de agregar BT-00027 a BT-00035. Es un hecho **de la corrida**
(`Master-Prompt.md` §8.1) y se resuelve como autocorrección sobre el conjunto, no como hallazgo de una
auditoría posterior.

**Alcance.** Sólo el bloque `00xxx` (`GeometriaFactory-Api`, 35 `BT`). Los bloques `02xxx`
(`GeometriaFactory-Domain`, 16), `04xxx` (`GeometriaFactory-Application`, 21) y `06xxx`
(`GeometriaFactory-Infrastructure`, 26) siguen bajo el umbral y siguen inline, sin tocar.

**Forma aplicada y su origen.** La cabecera obligatoria y las siete secciones de
`Rules-Backlog-Tecnico.md` §4.1 y §4.5, con el ejemplo de §7.2 como referencia de forma; como precedente
de estilo del propio destino, [`../Unidades-Entrega/GeometriaFactory-Api/06-Backlog-Tecnico/historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md`](../Unidades-Entrega/GeometriaFactory-Api/06-Backlog-Tecnico/historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md),
que fija las convenciones de cabecera (`Producto`, `Unidad de entrega`, `Documento`, `Versión`, `Estado`,
`Fecha`, `Autor`, `Épica`, `Etapa del producto`) y de control de cambios que este destino ya usa para
artefactos individuales de esta misma categoría. Cada uno de los 35 archivos suma dos campos que la
cabecera de Rules no prevé pero que la fila del catálogo ya declaraba (`Tipo` y `Prioridad`), y una
sección 7 «Trazabilidad a US» ampliada con las columnas que trae la matriz de §4.1
(`CU upstream`, `Puntos de acceso que toca`, `Fuente de arquitectura`) — nada de la fila y sus secciones
asociadas queda afuera.

**Campos de cabecera sin valor propio por `BT` en la fuente, y cómo se resolvieron.** La fila inline no
declara `Estado` ni `Autor` por `BT`, y el documento consolidado tampoco declara `Autor` a nivel de
documento. No se trata como referencia pendiente de `Root-Rules.md` §12.1 porque el destino **ya
resolvió la misma ausencia** para el mismo tipo de extracción: las 114 fichas de
[`historias-usuario/`](../Unidades-Entrega/GeometriaFactory-Api/06-Backlog-Tecnico/historias-usuario/)
usan `Estado: Aprobada` y `Autor: Scrum Master + … (AG-06)` sin que `Product-Backlog.md` declare ninguno
de los dos a nivel de fila ni de documento (`grep -h "^\*\*Autor:\*\*" historias-usuario/US-*.md | sort -u`
→ dos variantes, ambas con el rol `Scrum Master + … (AG-06)`; `grep -h "^\*\*Estado:\*\*"
historias-usuario/US-*.md | sort | uniq -c` → 114 en `Aprobada`). Aplicar `Root-Rules.md` §12.1 acá habría
dejado 35 `BT` con una forma de ausencia distinta de las 114 `US` del mismo backlog, para el mismo hueco.
Se usó `Autor: Scrum Master + Backlog Curator (AG-06)` —variante ya en uso en esta misma carpeta para
artefactos de la categoría técnica— y `Estado: Aprobada`.

**Verificación, con comando y salida.**

1. **35 archivos, cada `BT-000NN` una sola vez.**

   ```
   $ ls SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/06-Backlog-Tecnico/tareas-tecnicas | wc -l
   35
   $ ls tareas-tecnicas | sed -E 's/^(BT-[0-9]{5}).*/\1/' | sort | uniq -c | awk '$1!=1'
   (sin salida: ningún identificador aparece más de una vez)
   ```

2. **Todo enlace relativo nuevo resuelve**, desde `Backlog-Tecnico.md` y el `README.md` a cada archivo,
   y desde cada archivo hacia arriba (`Product-Backlog.md`, `historias-usuario/US-00001…`, los ADR y
   contratos de `05`, y este mismo documento de evidencia):

   ```
   $ python3 - <<'PY'
   # recorre Backlog-Tecnico.md, README.md y los 35 archivos de tareas-tecnicas/,
   # resuelve cada "](destino)" relativo contra el filesystem
   PY
   enlaces relativos verificados: 317
   rotos: 0
   ```

3. **Cero pérdida de contenido en los criterios de aceptación**, comparando el texto de la fila v3.0
   contra el de cada archivo (con el único ajuste declarado: la profundidad relativa de los enlaces que
   la fila transportaba, `../0…` → `../../0…`, porque el archivo vive un nivel más adentro que
   `Backlog-Tecnico.md`; afecta a un solo enlace, en el criterio de BT-00035):

   ```
   $ python3 verify_criterios.py   # reconstruye el bloque de bullets de "## 3. Criterios de
                                    # aceptación" de cada archivo, revierte el ajuste de
                                    # profundidad y compara contra la celda de la fila v3.0
   ALL CRITERIOS MATCH
   ```

4. **Ningún otro documento del destino quedó apuntando a un ancla que ya no existe.** Los identificadores
   `BT-000NN` no cambiaron (siguen siendo válidos como texto en cualquier documento que los nombre); lo
   único que podría haberse roto es un enlace con ancla hacia una fila de `Backlog-Tecnico.md` que ya no
   está en ese documento del mismo modo, y no existe ninguno:

   ```
   $ git grep -c "BT-000[0-3][0-9]" -- ':!*/_legacy/*' | wc -l
   82
   $ git grep -noE '\]\([^)]*Backlog-Tecnico\.md#[^)]*\)' -- ':!*/_legacy/*' | wc -l
   0
   ```

**Qué no se tocó.** El contenido sustantivo de las 35 `BT` (título, tipo, épica, etapa, prioridad,
fuente upstream, dependencias, criterios de aceptación, US consumidoras, justificación de infraestructura
compartida) es el mismo que en la fila v3.0, transpuesto sin reescritura. `Backlog-Tecnico.md` §2
(épicas técnicas) y §4.1 (matriz de trazabilidad) no cambian de contenido; §3.1 pasa de tabla de detalle
a índice con enlace por fila, sin perder ninguna de sus once columnas.

---

## Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | Emisión inicial. Evidencia de la apertura de la fase `k` sobre `Backlog-Tecnico.md` de `GeometriaFactory-Api` (2.2 → 3.0): criterio de §3.6 verificado con `git show`, rango de identificadores verificado por conteo, justificación de por qué `Product-Backlog.md` y `GeometriaFactory-Web` no se reabren, referencia sin duplicar al ítem diferido `D-01`, lote de detenciones vacío y lo no verificado. |
| 1.1 | 2026-09-12 | **Autocorrección de la misma corrida** (`Master-Prompt.md` §8.1; `ORIGEN DEL HECHO: de la corrida`; base `9167e68`). Suma §11: resuelve la lectura del umbral de `Rules-Backlog-Tecnico.md` §3.3 que el §9 de la versión 1.0 había dejado sin verificar, declara que la apertura de la fase `k` lo cruzó para el bloque `00xxx` de `GeometriaFactory-Api`, y registra la extracción de las 35 `BT` a [`tareas-tecnicas/`](../Unidades-Entrega/GeometriaFactory-Api/06-Backlog-Tecnico/tareas-tecnicas/) con sus cuatro verificaciones y comando. §9 tacha la entrada que queda resuelta y remite a §11. Sube **minor**. |
