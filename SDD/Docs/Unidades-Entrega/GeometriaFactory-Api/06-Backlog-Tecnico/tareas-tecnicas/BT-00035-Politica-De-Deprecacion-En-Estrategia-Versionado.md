# BT-00035 — Política de deprecación en `Estrategia-Versionado.md`

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** BT-00035-Politica-De-Deprecacion-En-Estrategia-Versionado.md
**Versión:** 2.0
**Estado:** Done
**Fecha:** 2026-09-13
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-T06 · Exposición pública y versionado del contrato
**Etapa del producto:** `k`
**Tipo:** docs
**Prioridad:** Media
**Estimación:** **No aplica** — el producto no estima; ver §6

## 1. Descripción

Política de deprecación en `Estrategia-Versionado.md`.

## 2. Justificación

`Mesa-2026-09-12-ciclo-2.md` §4 ítem 10; [`ADR-00008`](../../05-Arquitectura-Tecnica/Adrs/ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) §2 punto 1 vigente («no hay deprecación gradual: no hay a quién dársela»), premisa que BT-00027 reescribe y que esta tarea, dependiente de BT-00032, ejecuta sobre `Estrategia-Versionado.md`; `PRODUCT-INTAKE` **4.3**

## 3. Criterios de aceptación

- [`Estrategia-Versionado.md`](../../09-Devops/Estrategia-Versionado.md) suma una entrada con el plazo de convivencia de **un cuatrimestre** (`D-02`) entre versiones de ruta y el mecanismo de aviso de deprecación de la versión anterior

**Veredicto al 2026-09-13** (detalle en §8, fila 2.0). El criterio es uno y tiene tres partes; las tres se verifican sobre el árbol:

| Parte del criterio | Estado | Dónde |
| --- | --- | --- |
| `Estrategia-Versionado.md` suma la entrada | **Cumplido**: entra **§6.b «La deprecación del contrato REST»** como ítem propio, con ocho filas —cuándo se abre `/v{N+1}/`, convivencia mínima, aviso, retiro, cuántas conviven, qué no se depreca, cómo se materializa, el caso vigente— y con sus fuentes en cada una (`ADR-00010` §2.1 punto 4 y §7, `ADR-00008` §2 regla 1 derogada, mesa ciclo 2 §3.3 punto 7 y §8 `D-02`, `Contratos-REST.md` §3.1, §4 y §6) | [`../../09-Devops/Estrategia-Versionado.md`](../../09-Devops/Estrategia-Versionado.md) **6.0** §6.b; `grep -n "^### 6.b" Estrategia-Versionado.md` → `365` |
| El plazo de convivencia es **un cuatrimestre** (`D-02`) | **Cumplido, sin plazo distinto del decidido**: «un cuatrimestre desde la fecha en que `/v{N+1}/` entra a producción», contado como cuatro meses calendario —la lectura mínima del cuatrimestre lectivo—, prorrogable al fin del cuatrimestre lectivo y **nunca acortable**. `grep -c "cuatrimestre" Estrategia-Versionado.md` pasa de 2 a 8; ninguna ocurrencia nombra otra cifra | §6.b, fila «Convivencia mínima» |
| El mecanismo de aviso de la versión anterior | **Cumplido**: desde la fusión que publica `/v{N+1}/`, toda respuesta de `/v{N}/` lleva `Deprecation: @<epoch>` (`draft-ietf-httpapi-deprecation-header`, el borrador que la mesa citó) y `Sunset: <fecha HTTP>` (RFC 8594), sobre el grupo entero, y `changelog.md` anuncia las dos fechas en una entrada `BREAKING`. Se completa con lo que un aviso necesita para ser cumplible: el retiro al vencer (`410`, fundado contra el `404` de `Contratos-REST.md` §3.1 y §4, **sin agregar el código a los once**: es tarea propia del primer retiro) y las exenciones (`/salud`, `/openapi/v1.json`, `/documentacion`). **Hoy nada está deprecado**: medido contra producción, ninguna respuesta lleva las cabeceras | §6.b, filas «Aviso», «Retiro», «Qué no se depreca» y «El caso vigente»; `Contratos-REST.md` **1.10** §3.1 y §4 |

## 4. Dependencias

- BT-00032

## 5. Tipo

`docs`.

## 6. Estimación

**No aplica.** El producto no estima: planifica por etapas con punto de control bloqueante. `PA-01` de [`../Product-Backlog.md`](../Product-Backlog.md) §6 quedó **cerrado por lectura** el 2026-08-25, y el fundamento está en su §4.1 (ver también [`../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md`](../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) §5.b).

## 7. Trazabilidad a US

| Aspecto | Contenido |
| --- | --- |
| US que la consumen | **Infraestructura compartida**: cierra el ítem 10 del plan de la fila `k` |
| CU upstream | — (sin CU: política de deprecación) |
| Puntos de acceso que toca | Ninguno |
| Fuente de arquitectura | [`ADR-00008`](../../05-Arquitectura-Tecnica/Adrs/ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) §2 punto 1 (a reescribir por BT-00027) |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | **Extracción a archivo individual** de la fila BT-00035 de [`../Backlog-Tecnico.md`](../Backlog-Tecnico.md) v3.0 §3.1, por cruce del umbral de treinta `BT` que fija `Rules-Backlog-Tecnico.md` §3.3 para el bloque `00xxx` (`GeometriaFactory-Api`, 35 tareas con la apertura de la fase `k`). **Contenido transpuesto sin reescritura** desde la fila del catálogo (§3.1), la épica EP-T06 de §2.1 y la fila correspondiente de la matriz de trazabilidad (§4.1). Autocorrección de la misma corrida (`Master-Prompt.md` §8.1), `ORIGEN DEL HECHO: de la corrida`, base `9167e68`. Evidencia en [`../../../../Audit/Apertura-Fase-k-2026-09-12.md`](../../../../Audit/Apertura-Fase-k-2026-09-12.md). |
| 1.1 | 2026-09-12 | **Evaluación de DoR** (`Definition-Of-Ready.md` §2.1) y **corrección de fuente**. §2 y §7 citaban el registro de mesa y «`PRODUCT-INTAKE` 4.3» sin sección, ninguno admitido. `ADR-00008` §1 punto 1, vigente, declara hoy «no hay deprecación gradual: no hay a quién dársela» — es la premisa exacta que BT-00027 reescribe y que esta política ejecuta sobre `Estrategia-Versionado.md`. Se agrega esa cita. **Ningún criterio de aceptación cambia**. Pasa a **Ready**. Evidencia en [`../../../../Audit/DoR-Tramo-k-2026-09-12.md`](../../../../Audit/DoR-Tramo-k-2026-09-12.md). |
| 1.2 | 2026-09-12 | **Corrección de cita, detectada por el orquestador contra el árbol.** §2 y §7 (v1.1) citaban «`ADR-00008` §1 punto 1» para la frase «no hay deprecación gradual: no hay a quién dársela». Esa frase vive en **§2 «Decisión», regla 1** (línea 27 del ADR), no en §1 («Contexto»). Se corrigen las dos citas vigentes a `ADR-00008` §2 punto 1. **No se reescribe la fila 1.1**: esta fila declara la corrección. **Ningún criterio de aceptación cambia**. Sigue **Ready**. Evidencia y corrección en [`../../../../Audit/DoR-Tramo-k-2026-09-12.md`](../../../../Audit/DoR-Tramo-k-2026-09-12.md), sección BT-00035. |
| 2.0 | 2026-09-13 | **Ejecución y cierre: pasa a `Done`.** Rama `fase-k/bt-00035-deprecacion` sobre `main` = `1748503` (`v1.1.0`), tarea `docs`: **sin `src/`**. **Hecho**: [`../../09-Devops/Estrategia-Versionado.md`](../../09-Devops/Estrategia-Versionado.md) **6.0** —entra **§6.b** como ítem propio, con la política en ocho filas; §1.1 y §6.1 dejan de decir que `BT-00035` «la escribe» y remiten a §6.b; §6.2, §8.1 y §10.1 no se tocan porque sus «no hay plazo» siguen siendo ciertos para quien compila contra el ensamblado; sube major por el precedente de la 5.0, entra una regla y no sólo estructura—; [`../../05-Arquitectura-Tecnica/Contratos-REST.md`](../../05-Arquitectura-Tecnica/Contratos-REST.md) **1.10** —§3.1 remite a la política, §4 declara `Deprecation` y `Sunset` como cabeceras **futuras** que ninguna respuesta lleva hoy y que `410` **no** entra a los once códigos por esta tarea, con la medición transcripta; §6 precisa su remisión—; [`../../05-Arquitectura-Tecnica/Adrs/ADR-00010-Version-En-La-Ruta-Solo-Major-Para-La-Superficie-Publica.md`](../../05-Arquitectura-Tecnica/Adrs/ADR-00010-Version-En-La-Ruta-Solo-Major-Para-La-Superficie-Publica.md) **1.2** —§7 pasa de «remitido» a «decidido por `BT-00035`» con la cita; el cuerpo no cambia—; `changelog.md`. **Lo que la política fija, en cinco líneas**: (1) `/v{N+1}/` se abre sólo por un cambio Mayor de `Contratos-REST.md` §6, que sube `MAJOR` del producto (`ADR-00010` §2.1 punto 2); (2) `/v{N}/` convive **un cuatrimestre como mínimo desde que `/v{N+1}/` entra a producción** (`D-02`), cuatro meses calendario, prorrogable y nunca acortable; (3) toda respuesta de `/v{N}/` lleva `Deprecation: @<epoch>` y `Sunset: <fecha HTTP>`, y `changelog.md` lo anuncia; (4) al vencer, `/v{N}/` responde `410` —código que agrega la tarea del retiro, no ésta—; (5) `/salud`, `/openapi/v1.json` y `/documentacion` no se deprecan, y **hoy sólo existe `/v1/` y nada está deprecado**. **Verificación**: `curl -s -D - -o /dev/null https://api-geometria.aplicada.stream/salud` y `…/v1/aprovisionamiento` → `HTTP/2 200` sin `Deprecation` ni `Sunset` (`grep -i -c` → `0`; `HEAD` da `405`, `allow: GET`); `git grep -n -i "deprecaci" -- SDD ':!*/_legacy/*'` → 64 líneas en 24 archivos antes y **72 líneas en los mismos 24 archivos** después: las ocho líneas nuevas están en los cuatro documentos tocados (`Estrategia-Versionado.md` 7 → 11, `Contratos-REST.md` 2 → 4, esta ficha 8 → 10, `ADR-00010` 8 → 8) y ningún archivo entra ni sale de la lista; enlaces relativos de los cuatro `.md` resueltos contra el árbol, **0 rotos**; anchos de tabla: ninguna fila nueva con recuento de celdas distinto de su encabezado; `git tag -l` → seis etiquetas, **sin cambios**; `git diff --stat main` **sin `src/`**. **Lo que queda fuera y se declara**: la celda `Estado` de `BT-00035` en `Mini-Plan.md` §3.5 sigue en `Pendiente` —la cierra el orquestador al fusionar, como con `BT-00032`—; el `410` y la prueba de integración de las cabeceras son de la tarea que abra `/v{N+1}/` y de la del primer retiro, que hoy no existen porque no hay nada que deprecar. Sube **major** porque el paso a `Done` cambia el estado de vida de la tarea, como `BT-00033` (2.0). |
