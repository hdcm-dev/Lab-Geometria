# Plan de migración normativa — 9.9 → 9.10

**Producto:** Fábrica de Geometría
**Documento:** Plan-Migracion-9.9-a-9.10.md
**Versión:** 1.0
**Estado:** Emitido
**Fecha:** 2026-08-17
**Autor:** Orquestador de migración normativa SDD
**Responsable de mantenerlo:** el orquestador de migración que lo emite; lo cierra el informe de M6
**Instrumento normativo:** `Master-Prompt-Migracion.md` **2.7** §5, con `Migracion-Rules.md` **3.8**
**Origen:** SDD **9.9** · **Vigente:** SDD **9.10**
**Conjunto de origen:** **disponible** en `_legacy/9.9/` del repositorio del framework

---

## 1. Por qué este plan es corto, y qué lo hace verificable

**El salto es de una versión y alcanza un solo artefacto del framework.** Eso no lo vuelve trivial:
lo vuelve **verificable de una sola pasada**, que es distinto. El riesgo de un salto chico no es el
volumen sino **darlo por inocuo sin abrirlo**, que es el anti-patrón «actualizar la procedencia
porque el delta parece chico» de `Master-Prompt-Reanudacion.md` §7.

**Este plan existe para que la afirmación «no toca nada» tenga con qué comprobarse.**

---

## 2. Diff normativo, artefacto por artefacto

**Método.** Se comparó la tabla de procedencia de `PRODUCT-MANIFEST` **3.0** §1.1 contra la versión
que declara hoy la cabecera de cada archivo del framework, y se leyó la entrada **9.10** del
`CHANGELOG` completa.

| Artefacto del framework | Procedencia (9.9) | Vigente (9.10) | Cambió | Severidad para este destino |
| --- | --- | --- | --- | --- |
| `Migracion-Rules` | 3.7 | **3.8** | **Sí** | **Nula sobre artefactos.** Ver §3 |
| `Master-Prompt` | 8.4 | 8.4 | No | Nula |
| `Master-Prompt-Migracion` | 2.7 | 2.7 | No | Nula |
| `Master-Prompt-Reanudacion` | 1.6 | 1.6 | No | Nula |
| `Root-Rules` | 6.1 | 6.1 | No | Nula |
| `Rules-Contexto` | 4.3 | 4.3 | No | Nula |
| `Rules-Necesidades-Negocio` | 4.2 | 4.2 | No | Nula |
| `Rules-Especificacion-Funcional` | 5.3 | 5.3 | No | Nula |
| `Rules-UX-UI-DX` | 5.3 | 5.3 | No | Nula |
| `Rules-Arquitectura-Tecnica` | 4.3 | 4.3 | No | Nula |
| `Rules-Backlog-Tecnico` | 4.3 | 4.3 | No | Nula |
| `Rules-Plan-Sprint` | 5.3 | 5.3 | No | Nula |
| `Rules-Calidad-Y-Pruebas` | 4.4 | 4.4 | No | Nula |
| `Rules-Devops` | 4.5 | 4.5 | No | Nula |
| `Rules-Examples` | 6.3 | 6.3 | No | Nula |
| `Rules-Documentacion` | 5.3 | 5.3 | No | Nula |
| `Intake-Rules` | 4.1 | 4.1 | No | Nula |
| `Vocabulario-Rules` | 3.1 | 3.1 | No | Nula |
| `Maqueta-Rules` | 4.2 | 4.2 | No | Nula |
| `Deriva-Rules` | 5.2 | 5.2 | No | Nula |
| `PRODUCT-INTAKE-template` | 3.4 | 3.4 | No | Nula |
| `PRODUCT-MANIFEST-template` | 6.0 | 6.0 | No | Nula |

**Veintiuno de veintidós artefactos no se movieron.** **Las once reglas de categoría aplicadas, las
tres transversales de forma y las dos plantillas están en la misma versión que la procedencia
declara.** Es el renglón que decide el alcance: una regla de categoría que no cambia no puede obligar
a reemitir ningún documento de su categoría.

---

## 3. El único artefacto que cambió, y por qué no alcanza a este destino

**`Migracion-Rules.md` 3.7 → 3.8** agrega a su **§4.3.2** las cinco reglas de la **consolidación al
fundir árboles** —`C1` a `C5`— y el criterio enumerable correspondiente en §6.

**Qué gobiernan.** Cómo decidir qué secciones difieren entre las versiones de un grupo que se
consolida, y cómo verificar línea por línea que la transposición no perdió contenido: no normalizar
el nombre del proyecto de código en el cuerpo, verificación literal, correrla antes de re-derivar
enlaces, verificar cada marca contra el texto, y las cuatro clases que no transponen.

**Por qué no toca a este destino, verificado sobre el árbol y no supuesto:**

| Verificación | Resultado |
| --- | --- |
| ¿Hay una consolidación en curso? | **No.** `find . -type d -name "_fusion*"` → **0 carpetas** |
| ¿La migración anterior fundió árboles? | **No.** Las siete filas del `Plan-Migracion-8.11-a-9.9.md` se clasificaron **«revisar»**; ninguna «regenerar», ninguna consolidación |
| ¿Alguna fase de este destino tiene consolidación pendiente? | **No.** La única del producto fue la de la migración 6.0 → 8.6, **cerrada** con sus 67 grupos y su informe |
| ¿La 3.8 obliga a re-verificar consolidaciones ya cerradas? | **No.** Una regla de cómo migrar gobierna las migraciones que se ejecutan bajo ella. La consolidación de 6.0 → 8.6 se ejecutó y se auditó bajo su normativa, y su informe la declara completa |

**`Migracion-Rules` es la regla que gobierna esta corrida, no un artefacto del destino.** Cambia cómo
se migra, no la forma de ningún documento migrado. **Cero documentos alcanzados.**

---

## 4. Renombres de artefacto aplicables

**Ninguno.** La 9.10 **no es major** y **no tiene bloque «Impacto sobre destinos existentes»**, que es
donde el método declara los renombres. Se comprobó **por lectura de la entrada completa** y no por
ausencia de noticia, como exige `Migracion-Rules.md` §111.

---

## 5. Tabla de documentos

| Conjunto | Cantidad | Clasificación | Fundamento |
| --- | --- | --- | --- |
| `SDD/Docs/` vivo | **459** | **No tocar** | Ninguna regla de categoría cambió de versión |
| `SDD/Intake/PRODUCT-INTAKE-…` | 1 | **No tocar** | `PRODUCT-INTAKE-template` no cambió |
| `SDD/Intake/PRODUCT-MANIFEST-…` | 1 | **Revisar** | Sólo su bloque de procedencia §1.1, en **M5** |
| `SDD/Docs/**/_legacy/**` | — | Fuera de alcance | Snapshots, por §2.2 |

**Ninguna fila requiere M2, M3 ni M4.** El intake no se toca porque su plantilla no cambió; el
manifiesto no se re-deriva porque el intake no cambió. **La única escritura de esta migración es la
procedencia**, y es exactamente el caso que `Master-Prompt-Reanudacion.md` §4 nombra: **actualizar la
procedencia sin migrar documentos**, que **sólo procede cuando se verificó artefacto por artefacto que
el salto no alcanza al destino**. Esa verificación es §2 y §3 de este plan, y está escrita **antes** de
tocar la tabla.

---

## 6. Degradación declarada

**No aplica.** La procedencia está declarada y el conjunto de origen es reconstruible desde
`_legacy/9.9/`.

---

## 7. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-17 | Emisión inicial. Fase **M1** de la migración **9.9 → 9.10**, la cuarta migración normativa de este destino y la primera con **alcance documental cero**. **Veintiuno de veintidós artefactos del framework sin cambio de versión**; el único que se movió es `Migracion-Rules` 3.7 → 3.8, cuyas cinco reglas nuevas gobiernan **la consolidación al fundir árboles** y **no alcanzan a este destino**, verificado sobre el árbol: **cero carpetas `_fusion/`**, ninguna fila del salto anterior clasificada «regenerar» y la única consolidación del producto —la de 6.0 → 8.6— cerrada y auditada bajo su propia normativa. **Cero renombres**, comprobados por lectura de la entrada completa del `CHANGELOG` y no por ausencia de noticia. **Ninguna fila requiere M2, M3 ni M4**: la única escritura es la procedencia, en M5. | Orquestador de migración normativa SDD |
