# Plan de cierre de los pendientes — evaluación de los tres frentes abiertos

**Producto:** Fábrica de Geometría
**Documento:** Plan-Cierre-De-Pendientes.md
**Versión:** 1.0
**Fecha:** 2026-08-20
**Estado:** **Plan. Ningún punto abierto cerrado por este documento**
**Alcance:** los tres frentes que quedaron abiertos al cerrar la migración 9.12 → 10.0

---

## 0. La conclusión primero, porque cambia el reparto

**Los «90 pendientes» no son 90 decisiones del Product Owner.** La clasificación de §2 —hecha sobre el
árbol y no sobre la lista— muestra que **una parte importante ya está decidida en el código y lo único
que falta es escribirla de vuelta**. Eso no es una detención: por la pregunta previa de
`Master-Prompt.md` §8.1, *«si se contesta abriendo los documentos y contrastando, y la respuesta se
sostiene con una cita literal, no es una detención: es trabajo propio»*.

**Reparto estimado:** de 90 filas, **como máximo 25 son genuinamente tuyas**. El resto se cierra
leyendo.

---

## 1. Los tres frentes, evaluados

| Frente | Qué es | Quién puede cerrarlo | Bloquea a |
|---|---|---|---|
| **A · Los 90 puntos abiertos** | 76 vencidos + 14 sin evento, en 6 documentos | **Mixto**, y §2 lo reparte | Nada. Se puede hacer en paralelo con `i` |
| **B · La fase `i`** | Despliegue real y `PT-05` desde la red de la facultad | **Sólo vos** — secretos del hosting, acceso a `i7infra`, un alumno en la facultad | `j…` entero |
| **C · Reporte `12`** | Si el framework debe distribuir un verificador ejecutable | **Sólo vos** — decisión de alcance, `Master-Prompt.md` §8.1 | Nada de este destino |
| **D · Segunda ronda de M6** | Auditoría de la migración con auditor **independiente** | Vos, encargándola | Nada, pero **caduca**: cuanto más se construya encima, menos dice |

**El orden que recomiendo es `A` → `B`, con `D` en paralelo y `C` cuando quieras.** El motivo de poner
`A` primero no es su tamaño: es que **la fase `i` va a cerrar puntos abiertos** —`PD-01` de la 09
mide el mecanismo de construcción en destino, `PT-05` cierra el suyo— y hacerlo sobre una tabla que ya
está saneada evita repetir el trabajo.

---

## 2. Frente A · las 90 filas, repartidas por naturaleza

**La clasificación es por palabra clave sobre el enunciado y es aproximada.** Se declara así en lugar
de presentarla como exacta: las familias `F1` y `F4` se solapan y sólo la lectura fila por fila las
separa.

| Familia | Filas | Qué son | Quién cierra |
|---|---|---|---|
| **F1 · Ya decidido en el código** | **11** | Rutas y verbos, códigos del contrato, punto de quiebre, comparación de correos, derivación de clave, límites | **Trabajo propio.** Se lee del árbol y se escribe de vuelta |
| **F2 · Anclaje de herramienta o versión** | **20** | Herramienta de cada stage, versiones de paquetes, versión de plataforma, motor 3D | **Casi todo propio** — ver abajo |
| **F3 · Valor de producto sin decidir** | **25** | `[ASUNCIÓN]` y `[A VERIFICAR]`: umbral de respuesta, volumen de la comisión, unidad de estimación, vigencia del acceso | **Tuyo.** Ninguna fuente los declara |
| **F4 · A clasificar leyendo** | **34** | Nombres definitivos de funciones y clases, y otros que la palabra clave no separa | **A determinar**, y el primer paso del frente |

### 2.1 Por qué `F2` es casi todo trabajo propio, y está verificado

**Los anclajes que estos puntos piden ya existen en el árbol:**

| Qué pide el punto | Dónde está hoy |
|---|---|
| «La herramienta concreta de cada stage» | `scripts/*.sh` y el flujo: `dotnet build`, `dotnet test`, `npm ci`, `webpack`, `playwright` |
| «Las versiones exactas de los paquetes» | **7 `PackageReference` con `Version=`** en los `.csproj` |
| «La derivación de clave y su parámetro» | **`PasswordDerivation.cs`**, que elige **PBKDF2** y escribe el criterio |

**La única excepción medida es `PA-06`**, la herramienta que calcula la versión desde las etiquetas:
**no existe ninguna en el árbol** —los únicos hits son artefactos de compilación en `obj/`—, y **sigue
genuinamente abierta**. Es coherente con que el prefijo `v` se fijara sin elegirla.

### 2.2 Cómo se ejecuta el frente A

| Paso | Qué | Quién | Detención |
|---|---|---|---|
| **A1** | Leer las **34** de `F4` y repartirlas entre `F1`, `F2` y `F3` | Propio | No |
| **A2** | Cerrar `F1` + `F2` **escribiendo de vuelta lo que el código ya decidió**, con cita al archivo | Propio | No |
| **A3** | Presentar `F3` agrupada por decisión, **no fila por fila** | Propio | **Sí** — es tuya |
| **A4** | Las **14 sin evento**: declararles el evento con la forma de §12.2, o **retirarlas** si ya no aplican | Tuyo, con propuesta mía | **Sí** |

**A2 no cierra ningún punto por criterio propio.** Escribe lo que el árbol ya dice, con su cita, que
es exactamente lo que `§8.1` llama trabajo propio. Si al leer aparece un punto **sin** respuesta en el
código, **no se inventa**: pasa a `F3`.

---

## 3. Frente B · la fase `i`

**No hay nada que yo pueda adelantar**, y ya está todo lo que se podía dejar preparado:
`verify-stage-i.sh` mide los criterios mecánicos y `Medicion-PT-05.md` espera el resultado en
`SIN MEDIR`.

**Lo único que este plan agrega es una advertencia de orden.** `PT-05` valida `RN-B1`, cuyo impacto es
**Alto** —«sin acceso el laboratorio no existe»—, y **es la única medición del producto que puede
obligar a mover la topología**. Cuanto más se construya antes de medirla, más caro es que salga mal.

---

## 4. Frente C · el reporte `12`

**No es de este destino y no bloquea nada de él.** La pregunta —si el `Framework SDD` debe pasar de
corpus de reglas a distribuir un verificador ejecutable— es intención de producto sobre el framework.

**Lo que sí conviene saber para decidirla:** el reporte `15`, ya aplicado en **10.1**, resolvió por
regla un caso que un verificador habría resuelto por código. **Es un dato a favor de postergar `12`**:
el método viene cerrando sus huecos sin ejecutable.

---

## 5. Frente D · la segunda ronda de M6

**El informe de M6 declara en su §0 que su auditor no fue independiente**, y §3 declara **tres cosas
que no verificó**: si el evento de cierre elegido es el adecuado para cada punto, las 14 filas sin
evento una por una, y M5 contra el intake.

**Encargarla tiene una condición y una fecha.** La condición es la de `Master-Prompt.md` §10: se pide
**refutar y no verificar**, con **cita literal o el veredicto no vale**, y **«no concluyente» admitido
explícitamente**. La fecha es que **el frente A la vuelve obsoleta**: si A2 reescribe 30 filas, la
auditoría tiene que correr después, no antes.

**Recomendación: encargarla al terminar A**, no ahora.

---

## 6. Lo que este plan no sabe

- **El reparto exacto de las 90.** La clasificación es por palabra clave y las familias `F1` y `F4` se
  solapan. **El paso A1 existe para eso**, y hasta que corra, los números de §2 son estimaciones.
- **Cuántas de las 90 ya no aplican.** Ninguna se evaluó por vigencia. Es probable que varias sean de
  decisiones que el producto superó, y **retirarlas es tan válido como cerrarlas** — pero es tuyo.
- **Si `F3` son 25 decisiones o menos.** Se contaron filas, no decisiones: varias repiten el mismo
  valor en documentos distintos, como pasó con los anclajes.

## 7. Control de cambios

| Versión | Fecha | Cambios | Autor |
|---|---|---|---|
| 1.0 | 2026-08-20 | Emisión inicial. Evalúa los **cuatro** frentes abiertos al cerrar la migración 9.12 → 10.0 —los 90 puntos abiertos, la fase `i`, el reporte `12` y la segunda ronda de M6— y reparte el trabajo por **quién puede cerrarlo**. **La conclusión que cambia el reparto**: los 90 pendientes **no son 90 decisiones del Product Owner**; verificado sobre el árbol, una parte ya está decidida en el código y escribirla de vuelta es **trabajo propio** por la pregunta previa de `Master-Prompt.md` §8.1. Aporta la evidencia de que los anclajes de `F2` **ya existen** —siete `PackageReference` con versión, las herramientas de cada stage en los guiones, y `PasswordDerivation.cs` eligiendo **PBKDF2** con su criterio escrito— y la **única excepción medida**, `PA-06`, que sigue genuinamente abierta. Declara el orden recomendado **A → B, con D en paralelo y C cuando quiera**, y el motivo de que D **caduque**: el frente A la vuelve obsoleta si corre antes. §6 declara que la clasificación es **aproximada y por palabra clave**, que ninguna fila se evaluó por **vigencia**, y que se contaron **filas y no decisiones**. | Orquestador SDD |
