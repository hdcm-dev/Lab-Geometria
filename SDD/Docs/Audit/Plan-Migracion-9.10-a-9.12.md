# Plan de migración normativa — 9.10 → 9.12

**Producto:** Fábrica de Geometría
**Documento:** Plan-Migracion-9.10-a-9.12.md
**Versión:** 1.0
**Estado:** Emitido
**Fecha:** 2026-08-17
**Autor:** Orquestador de migración normativa SDD
**Responsable de mantenerlo:** el orquestador de migración que lo emite; lo cierra el informe de M6
**Instrumento normativo:** `Master-Prompt-Migracion.md` **2.7** §5, con `Migracion-Rules.md` **3.9**
**Origen:** SDD **9.10** · **Vigente:** SDD **9.12**
**Conjunto de origen:** **disponible** en `_legacy/9.10/` del repositorio del framework

---

## 1. El salto que parece grande y no lo es, y cómo se comprobó

**Este salto mueve dieciocho artefactos del framework**, incluidas **las once reglas de categoría
aplicadas** y `Root-Rules`. Por el número, es el segundo salto más grande que este destino atravesó.

**Y su alcance sobre el árbol es cero.** La diferencia entre afirmar eso y comprobarlo es el método
de §3: **no se leyó el `CHANGELOG` y se dedujo — se comparó el snapshot `_legacy/9.10/` contra los
archivos vivos, archivo por archivo, y se midió qué líneas cambiaron y dónde.**

Es la forma que `Master-Prompt-Reanudacion.md` §7 exige para no caer en «elegir por el número de
versión», y su inverso: acá el número asusta y la medición tranquiliza.

---

## 2. Diff normativo, artefacto por artefacto

| Artefacto del framework | Procedencia (9.10) | Vigente (9.12) | Cambió | Severidad |
| --- | --- | --- | --- | --- |
| `Root-Rules` | 6.1 | **6.2** | Sí | **Nula.** Sólo su tabla de anti-patrones |
| `Rules-Contexto` | 4.3 | **4.4** | Sí | **Nula.** Ídem |
| `Rules-Necesidades-Negocio` | 4.2 | **4.3** | Sí | **Nula.** Ídem |
| `Rules-Especificacion-Funcional` | 5.3 | **5.4** | Sí | **Nula.** Ídem |
| `Rules-UX-UI-DX` | 5.3 | **5.4** | Sí | **Nula.** Ídem |
| `Rules-Arquitectura-Tecnica` | 4.3 | **4.4** | Sí | **Nula.** Ídem |
| `Rules-Backlog-Tecnico` | 4.3 | **4.4** | Sí | **Nula.** Ídem |
| `Rules-Plan-Sprint` | 5.3 | **5.4** | Sí | **Nula.** Ídem |
| `Rules-Calidad-Y-Pruebas` | 4.4 | **4.5** | Sí | **Nula.** Ídem |
| `Rules-Devops` | 4.5 | **4.6** | Sí | **Nula.** Ídem |
| `Rules-Examples` | 6.3 | **6.4** | Sí | **Nula.** Ídem |
| `Rules-Documentacion` | 5.3 | **5.4** | Sí | **Nula.** Ídem |
| `Maqueta-Rules` | 4.2 | **4.3** | Sí | **Nula.** Ídem |
| `Deriva-Rules` | 5.2 | **5.3** | Sí | **Nula.** Ídem |
| `Migracion-Rules` | 3.8 | **3.9** | Sí | **Nula sobre artefactos.** Ídem; gobierna esta corrida |
| **`Catalogo-De-Criterios`** | — (no existía) | **1.1** | **Nuevo** | **Nula.** Ver §4 |
| `Master-Prompt` | 8.4 | 8.4 | **No** | Nula |
| `Master-Prompt-Migracion` | 2.7 | 2.7 | **No** | Nula |
| `Master-Prompt-Reanudacion` | 1.6 | 1.6 | **No** | Nula |
| `Intake-Rules` | 4.1 | 4.1 | **No** | Nula |
| `Vocabulario-Rules` | 3.1 | 3.1 | **No** | Nula |
| `PRODUCT-INTAKE-template` | 3.4 | 3.4 | **No** | Nula |
| `PRODUCT-MANIFEST-template` | 6.0 | 6.0 | **No** | Nula |

---

## 3. Qué cambió exactamente en las quince reglas, medido y no leído

**La 9.11 agregó a cada tabla de anti-patrones una columna `Detección`**, con la marca `[enumerable]`
o `[interpretativo]` por fila. La 9.12 corrigió el catálogo que las indexa.

**Verificación mecánica, sobre las quince reglas:**

```
diff _legacy/9.10/SDD/Devs/Rules/<regla>.md  SDD/Devs/Rules/<regla>.md
   filtrando la cabecera de versión, la fila de control de cambios
   y las filas de la tabla de anti-patrones
```

| Regla | Líneas cambiadas fuera de la tabla de anti-patrones y su cabecera |
| --- | --- |
| Las **once** de categoría aplicadas | **0** cada una |
| `Root-Rules` | **0** |
| `Deriva-Rules`, `Maqueta-Rules` | **0** cada una |
| `Migracion-Rules` | **0** |

**Cero en las quince.** Ninguna sección §4.1 —la cabecera que todo documento generado copia—, ninguna
§4.2 de secciones obligatorias, ningún criterio de aceptación y ningún nombre de artefacto se movió.

**Una tabla de anti-patrones no describe la forma de un artefacto**: describe qué evitar al
generarlo o al auditarlo. Cambiarla no obliga a reemitir ningún documento ya emitido. **Cero
documentos alcanzados.**

---

## 4. El artefacto nuevo, y la decisión que se toma sobre él

**`Catalogo-De-Criterios.md` 1.1 no existía en 9.10.** Vive en `SDD/Devs/Rules/` del framework y su
propia cabecera lo define: **«un índice, no una regla. No define ningún criterio: dice dónde vive
cada uno y qué decide.»**

**Decisión: entra en la tabla de procedencia, con su naturaleza declarada.** Podría omitirse —no
gobierna ningún artefacto de este destino— pero omitirlo obligaría a la próxima migración a
redescubrir la pregunta y a resolverla de nuevo, quizá distinto. **Un índice declarado como índice
cuesta una fila y ahorra una deliberación.**

---

## 5. Tabla de documentos

| Conjunto | Cantidad | Clasificación | Fundamento |
| --- | --- | --- | --- |
| `SDD/Docs/` vivo | **459** | **No tocar** | Ninguna regla cambió nada fuera de su tabla de anti-patrones |
| `SDD/Intake/PRODUCT-INTAKE-…` | 1 | **No tocar** | Su plantilla no cambió |
| `SDD/Intake/PRODUCT-MANIFEST-…` | 1 | **Revisar** | Su §1.1, en **M5**, más la corrección de §6 |

**M2, M3 y M4 quedan sin filas.**

---

## 6. Una corrección propia, declarada y no arreglada al pasar

**La migración anterior dejó un recuento mal, y lo escribió este mismo orquestador.** La fila de
reglas transversales del manifiesto **3.1** dice «este árbol atravesó **tres** migraciones
normativas» y a continuación **enumera cuatro**: 6.0 → 8.6, 8.6 → 8.11, 8.11 → 9.9 y 9.9 → 9.10.

**Causa.** Al escribir la M5 de 9.9 → 9.10 se agregó el cuarto salto a la enumeración **y no se
actualizó la palabra que los cuenta**. Es exactamente la forma que este destino ya tiene registrada
tres veces —«la decisión llega, el recuento sobrevive»— y esta vez la cometió el agente, en el mismo
documento cuya integridad venía auditando.

**Qué se hace.** Se corrige en **M5**, junto con la fila que de todos modos hay que reescribir para
subir `Migracion-Rules` a 3.9, y **se declara acá** para que no aparezca como una edición silenciosa.
Con este salto pasan a ser **cinco**.

---

## 7. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-17 | Emisión inicial. Fase **M1** de la migración **9.10 → 9.12**, la quinta de este destino y la segunda consecutiva de **alcance documental cero**. **Dieciocho artefactos del framework se movieron** —las once reglas de categoría, `Root-Rules`, `Deriva`, `Maqueta`, `Migracion-Rules` y el nuevo `Catalogo-De-Criterios`— y **ninguno alcanza al árbol**: la 9.11 agregó a cada tabla de anti-patrones una columna **`Detección`**, y la verificación **mecánica** —`diff` del snapshot `_legacy/9.10/` contra los archivos vivos— da **cero líneas cambiadas fuera de esa tabla** en las quince reglas. Se declara la decisión sobre `Catalogo-De-Criterios`: **entra en la procedencia con su naturaleza de índice declarada**, para que la próxima migración no redescubra la pregunta. Y se declara una **corrección propia**: la fila de transversales del manifiesto 3.1 dice «tres migraciones» y enumera cuatro, error escrito por este mismo orquestador en el salto anterior. | Orquestador de migración normativa SDD |
