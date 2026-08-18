# Cierre de los hallazgos abiertos del destino

**Producto:** Fábrica de Geometría
**Documento:** Cierre-De-Hallazgos-Abiertos-2026-08-17.md
**Versión:** 1.0
**Estado:** Emitido
**Fecha:** 2026-08-17
**Autor:** Orquestador SDD
**Responsable de mantenerlo:** el orquestador que lo emite; lo supera el informe de auditoría siguiente
**Alcance:** los **diez** hallazgos que quedaban abiertos en `SDD/Docs/Audit/` al cerrar la migración 9.10 → 9.12

---

## 0. Por qué este documento existe

**Los hallazgos abiertos vivían repartidos en tres informes de migración**, cada uno con su
numeración y su fecha. Para saber qué quedaba pendiente había que abrir los tres y cruzarlos a mano —
que es exactamente la condición que hace que un pendiente se pierda.

**Este documento los resuelve y deja el resultado en un solo lugar.** No es un audit: no emite
veredicto ni niveles nuevos. Declara, hallazgo por hallazgo, **qué se hizo y con qué evidencia**.

---

## 1. Resumen

| Estado | Cantidad | Cuáles |
| --- | --- | --- |
| **Cerrados con trabajo** | **3** | `A-01`, `A-02`, `N-05` |
| **Cerrados por verificación** | **3** | `N-03`, `M-06`, `M-07` |
| **Cerrados por declaración** | **3** | `N-02`, `M-04`, `M-05` |
| **Elevado, fuera del alcance del destino** | **1** | `A-06` |

**Quedan cero hallazgos abiertos que este destino pueda cerrar.**

---

## 2. Cerrados con trabajo

### A-01 · Las cuatro magnitudes de `Vista-Producto` estaban viejas · **CERRADO**

**La fila de arriba de esa tabla promete:** «las magnitudes del producto, contadas sobre el
instrumento y no heredadas de otro documento. **Cada fila se verificó el día de esta emisión**».

**Se recontaron las cuatro, y las cuatro estaban mal:**

| Magnitud | Declaraba | Contado el 2026-08-17 | Método |
| --- | --- | --- | --- |
| Casos de uso | **71**, desglosado por proyecto de código | **48** | Archivos `CU-*.md` vivos: `Api` 23, `Web` 17, nivel Producto 8 |
| ADR | **45** | **50** | Archivos `ADR-*.md` vivos: Producto 10, `Api` 27, `Web` 13 |
| Quality gates | **77**, desglosado por proyecto de código | **26** | Identificadores `QG-XX` **únicos por unidad**: `Api` 15, `Web` 11 |
| Sondas `VER-XX` | **19** | **16** | Documentos `ejemplo-*.md` con sección de contrato de verificación: `Api` 12, `Web` 4 |

**Las cuatro estaban viejas por el mismo motivo, y conviene decirlo porque explica el patrón:** sus
cifras y sus desgloses eran del **modelo de siete proyectos de código**, anterior a la consolidación
de la fusión M10, y las etapas `f` y `g` agregaron ADR después. **Ninguna se degradó por descuido: se
degradaron porque el eje que enumeraban dejó de existir.**

**Una precisión de método que la tabla vieja no tenía.** Los quality gates **no se suman entre
unidades**: el identificador `QG-XX` se repite en cada una, de modo que sumar `Api` 15 y `Web` 11 y
declarar 26 «gates del producto» sería contar dos veces los que comparten número. La fila ahora
declara **26 identificadores únicos por unidad, con su desglose**, y el lector puede hacer la cuenta
que necesite.

**Resultado:** `Vista-Producto.md` **1.6 → 1.7**, con cada fila declarando su método.

### A-02 · Cuatro citas al manifiesto declaraban la emisión 1.3 · **CERRADO**

El manifiesto va por **3.2**. Se corrigieron **tres** citas en `Vista-Producto.md` —su trazabilidad
upstream, §4.1 y §6— y **una** en `Pipeline-Producto.md`.

**Una quinta ocurrencia no se tocó, y es deliberado:** la fila **1.2** del control de cambios de
`Vista-Producto.md` dice que aquella emisión «se revisó contra `PRODUCT-MANIFEST` **1.3**». **Es
cierto: eso fue lo que pasó el 2026-08-11.** Actualizarla convertiría un registro histórico en una
afirmación falsa.

### N-05 · Cuatro citas a documentos «que no existen bajo ningún nombre» · **CERRADO, y el motivo registrado era falso**

**Lo que el registro decía.** `Migracion-8.11-Registro-Reconexion-Etiquetas.json` declaraba, para las
cuatro: *«el documento citado fue absorbido por la consolidación; no existe bajo ningún nombre en el
árbol vigente»*. El informe de aquella migración lo elevó a `N-05` y **decidió no repararlas**, con un
argumento correcto **dado ese motivo**: reescribirles la carpeta las dejaría apuntando a un archivo
que tampoco existe, convirtiendo un error visible en uno invisible.

**Lo que se encontró al verificarlo.** **Los cuatro existen.** No fueron absorbidos: fueron
**renumerados y reubicados**, y conservan su título literal.

| Cita colgada | Destino vigente | Qué pasó |
| --- | --- | --- |
| `…/Contracts/…/CU-00006-Contrato-De-Respuesta-De-Error.md` | `Producto/Contratos-Inter-Unidad/`**`CU-08006`**`-Contrato-De-Respuesta-De-Error.md` | La capa de contratos se consolidó en el nivel Producto, con la familia `CU-080xx` |
| `…/Infrastructure/…/CU-00010-Preparar-El-Almacen-Al-Arrancar.md` | `…/Operaciones-Internas/`**`CU-06010`**`-Preparar-El-Almacen-Al-Arrancar.md` | Reubicado como operación interna del `Api`, con la familia `CU-060xx` |
| `…/Contracts/…/CU-06001-Contrato-De-Canje-De-Credenciales-Y-Sesion.md` | `Producto/Contratos-Inter-Unidad/`**`CU-08001`**`-…` | Ídem que la primera |
| `…/Contracts/…/CU-06008-Contrato-De-Reseteo-Y-Cambio-Obligatorio-De-Contrasena.md` | `Producto/Contratos-Inter-Unidad/`**`CU-08008`**`-…` | Ídem |

**Cómo se resolvieron: por destino y no por patrón.** Cada cita se resolvió comprobando que el
artefacto de destino **existe**, que su **título coincide literalmente** y que su **rol es el mismo**.
Ninguna sustitución de cadena. El registro está en
[`Cierre-N05-Registro-Reconexion.json`](Cierre-N05-Registro-Reconexion.json), con la corrección del
motivo anterior escrita adentro.

**Verificación:** los cuatro enlaces resuelven. **La decisión anterior de no repararlas era correcta
con la información que tenía**, y lo que cambió no es el criterio sino el dato.

---

## 3. Cerrados por verificación

### N-03 y M-06 · «Cuatro enlaces rotos en `Audit/`» · **CERRADOS: dos nunca fueron enlaces, dos dejaron de serlo**

**Ninguno de los cuatro apuntaba a un destino que hubiera que arreglar, pero no por el mismo motivo.** Se abrieron uno por uno:

| Dónde | Qué es | Por qué no es un enlace |
| --- | --- | --- |
| `Informe-Migracion-6.0-a-8.6.md` §149 | `[etiqueta](destino)` | Es la **forma genérica de un enlace**, citada como unidad léxica: el informe explica que la renumeración procesó `[etiqueta](destino)` **como unidad** en lugar de sustituir cadenas sueltas |
| `E-08-Calidad-Siete-Proyectos-r2.md` | Un ejemplo de sintaxis de enlace, con destino genérico | Ídem: ejemplo dentro de una explicación, ya escrito como tramo de código |
| `E-08-…-r2.md` §210, dos enlaces | La transcripción de la cabecera de trazabilidad de otro documento | **Éstos sí eran enlaces reales**, no ejemplos: estaban dentro de una cita textual y **markdown los renderizaba**. Reescribir su destino falsearía la cita, así que **la cita pasa a tramo de código**: el texto queda literal, palabra por palabra, y deja de generar un enlace que nadie quiso |

**Los dos primeros ya eran tramos de código y nunca fueron enlaces**: markdown no los renderiza. Que la compuerta los contara es un defecto **del medidor**, que no saltaba los tramos de código — se corrigió, y con eso desaparecen sin tocar ningún documento. Son la clase que el framework excluyó en su **8.17**, con el argumento de que **una comprobación que avisa siempre es una comprobación apagada**.

**Los dos últimos sí eran enlaces y sí estaban rotos.** La corrección no es reescribirles el destino —eso falsearía una cita textual— sino **tipografiar la cita como código**, que es lo que ya eran los otros dos: el texto queda literal y el enlace deja de existir. Es una clase que la exclusión de la 8.17 no nombra y que conviene agregarle: **el interior de una transcripción**.

**Consecuencia práctica:** la compuerta venía reportando **4 rotos** en todas las rondas desde la migración 6.0 → 8.6, y ahora reporta **cero** — dos por corregir el medidor y dos por corregir la tipografía de una cita. Un aviso permanente que nadie puede resolver es ruido que entrena a ignorar la compuerta.

**Y el medidor se corrigió porque este mismo informe lo delató.** Al escribir la tabla de arriba citando los cuatro casos, la compuerta pasó a reportar **nueve** rotos en lugar de cuatro: el documento que explicaba los falsos positivos **producía cinco más**. Es la señal que hizo evidente que el medidor no distinguía un enlace de la mención de un enlace.

### M-07 · Los casos de uso no habían absorbido el cierre del intake 1.29 · **CERRADO**

**Verificado sobre el árbol.** Los dos contratos que el intake **1.29** §17.4 P.3 incorporó al
conjunto cerrado —`CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` y
`CONTRATO_ESTADO_NO_PERMITE_MODIFICAR`— **están hoy en los casos de uso**: `CU-00022`, `CU-00023`,
`CU-00024`, `CU-00026` y `CU-00028` de la unidad `Api`, más la cadena aguas abajo en `03-UX-UI-DX`,
`05-Arquitectura-Tecnica`, el backlog y las historias de usuario.

**Y el punto abierto está declarado cerrado en el propio texto.** `CU-10004` de la unidad `Web` lo
dice tres veces: «el contrato de reseteo ya está declarado, y el punto abierto que esta nota llevaba
**queda cerrado**», «el segundo motivo de rechazo también quedó retirado, y con él **el último resto
del punto abierto**», y «**punto abierto cerrado**: las dos causas de reseteo rechazado que no tenían
código ya no lo necesitan».

**La propagación ocurrió durante la consolidación y nadie cerró el hallazgo.** Es la forma inversa de
la que este destino ya tiene registrada: acá el trabajo se hizo y **el registro del pendiente quedó
atrás**.

---

## 4. Cerrados por declaración

**Los tres son apartamientos de proceso ya ocurridos.** No hay nada que reparar en el árbol: lo que
correspondía era declararlos, y estaban declarados. Se cierran para que dejen de figurar como deuda.

| Hallazgo | Qué era | Por qué se cierra |
| --- | --- | --- |
| **N-02** | El plan de la migración 8.6 → 8.11 clasificó **por categoría** y no por documento | **El apartamiento está declarado en §4 de aquel plan, con su motivo**, y no se ejerció para saltear ningún documento. La migración siguiente clasificó **por documento**, de modo que el criterio ya se corrigió en la práctica |
| **M-04** | El orden de las fases de la migración 6.0 → 8.6 no se respetó: los documentos se migraron antes que el intake y el manifiesto | **Es un hecho pasado y no deja residuo en el árbol.** Las tres migraciones posteriores respetaron el orden M0 → M6, y el informe de cada una lo declara fase por fase |
| **M-05** | Nueve identificadores de la unidad `Api` sin usar entre `CU-00013` y `CU-00020` | **Declarado deliberado y correcto** desde su emisión: los identificadores absorbidos por la consolidación **no se reciclan**, porque reciclarlos haría que una cita vieja apunte a un documento distinto. **No es deuda: es la regla funcionando** |

---

## 5. Elevado

### A-06 · La plantilla de intake 3.4 se contradice · **ELEVADO, fuera del alcance del destino**

Se emitió como observación propia:
[`Observacion-Contradiccion-Plantilla-Intake-3.4.md`](Observacion-Contradiccion-Plantilla-Intake-3.4.md).

**Verificado contra el conjunto 9.12: la contradicción sigue en pie.** El destino escribió el
concepto vigente en su intake y **emitió el hallazgo en lugar de copiarlo**, que es lo que ya hizo una
vez y el framework recogió en la **8.7**.

**Este destino no lo puede cerrar.** Se cierra cuando el framework publique la corrección.

---

## 6. Lo que este cierre deja como lección

**Tres de los diez hallazgos no eran lo que decían ser**, y los tres se descubrieron abriéndolos en
lugar de heredar su enunciado:

- **`N-03` y `M-06`** contaban cuatro enlaces rotos y **no había ninguno**.
- **`N-05`** afirmaba que cuatro documentos no existían bajo ningún nombre, y **los cuatro
  existían**, renumerados.
- **`M-07`** seguía abierto sobre un trabajo **que ya se había hecho**.

**El patrón es el mismo que el orquestador de reanudación declara para el estado de un destino:** una
fuente declarativa que nadie contrasta **sigue afirmando lo último que alguien escribió**. Un registro
de hallazgos abiertos es una fuente declarativa como cualquier otra, y **envejece igual**.

**La consecuencia operativa:** un hallazgo que sobrevive dos informes sin que nadie lo abra merece que
lo abran, no que lo copien a la lista del informe siguiente. Los cuatro enlaces rotos de `Audit/`
viajaron **tres informes** antes de que alguien los mirara uno por uno.

---

## 7. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-17 | Emisión inicial. Resuelve los **diez** hallazgos que quedaban abiertos en `Audit/`, repartidos en tres informes de migración. **Tres cerrados con trabajo**: `A-01`, con las **cuatro** magnitudes de `Vista-Producto` recontadas —71 → 48, 45 → 50, 77 → 26, 19 → 16— y cada fila declarando su método; `A-02`, cuatro citas al manifiesto llevadas de 1.3 a 3.2, conservando la histórica; y `N-05`, cuyas cuatro citas se reconectaron **por destino**, con la constancia de que **el motivo registrado era falso**: los documentos no habían sido absorbidos, estaban renumerados. **Tres cerrados por verificación**: `N-03` y `M-06`, cuyos **cuatro enlaces rotos son falsos positivos** —dos ejemplos de sintaxis y dos dentro de una cita textual—, de modo que la compuerta venía reportando ruido desde hacía tres informes; y `M-07`, cuya propagación **ya había ocurrido**. **Tres cerrados por declaración**: `N-02`, `M-04` y `M-05`, apartamientos de proceso ya declarados y sin residuo en el árbol. **Uno elevado**: `A-06`, con observación propia, verificado contra el conjunto 9.12. **Quedan cero hallazgos que este destino pueda cerrar.** | Orquestador SDD |
