# D1 — Las asunciones numéricas, en una tabla

**Producto:** Fábrica de Geometría
**Documento:** D1-Confirmacion-De-Asunciones.md
**Versión:** 1.0
**Fecha:** 2026-08-26
**Instrumento:** decisión `D1` de [`A3-Decisiones-Del-Product-Owner.md`](A3-Decisiones-Del-Product-Owner.md) §2
**Estado:** **Detención.** Presenta la decisión; **no toma ninguna**

---

## 0. Por qué existe este documento

**`D1` es la decisión más grande del frente `A` y la más barata**, y hasta hoy había que perseguirla
por **doce filas vencidas repartidas en cinco documentos**. Este documento la trae a una tabla: cada
valor, de dónde salió, qué se rompe si cambia y qué se destraba al confirmarlo.

**No decide nada.** Los valores que siguen son los que **ya están en el árbol y en el código**: se
transcriben para que confirmarlos sea leer una página, no reconstruirlos.

**Qué se destraba al confirmar.** `PD-03` de `Pipeline-CI-CD.md` §10 lo declara sin ambigüedad:

> *«La **confirmación de los valores rotulados [ASUNCIÓN]** que hoy dejan condicionados a `QG-03`,
> `QG-04`, `QG-13` y `QG-14`. **Confirmados, los cuatro pasan a bloqueantes sin ningún otro cambio de
> este documento.**»*

Cuatro puertas de calidad que hoy corren **sin poder frenar nada** pasan a frenar, sin tocar una línea
de configuración.

---

## 1. Lo primero, porque cambia una fila: el caudal perdió su fundamento

**No es una asunción más, y conviene decidirla sabiendo esto.** El caudal de **20 peticiones por
minuto** se derivaba de *«una comisión operando durante una clase»*. El 2026-08-20 cerraste `D5` —el
volumen de la comisión— **por incognoscible**: el dato no se sabe ni se puede saber de antemano.

**Con eso, el fundamento del número se cayó**, y el árbol ya lo dice:

> *«| Caudal sostenido | **20 peticiones por minuto** [ASUNCIÓN del intake], **provisorio desde el
> 2026-08-20**, derivado del uso previsto —una comisión operando durante una clase— …|»*
> — `Arquitectura-Unidad-Entrega.md` de `GeometriaFactory-Api`, §8

**Su valor definitivo sale de lo que `PT-05` mida sobre el uso real**, no de confirmarlo acá. Lo
declaro primero para que no lo confirmes junto con los demás por arrastre, que es exactamente el
defecto que la 11.0 vino a corregir.

---

## 2. Los valores, agrupados por lo que gobiernan

### 2.1 Umbrales de latencia y de arranque · `A-5` del intake §22

| Valor | Dónde rige | De dónde salió | Si lo cambiás |
|---|---|---|---|
| **500 ms** — validación en `Application` | `05` §8 de `-Api`; **20 documentos** lo citan | Ninguna fuente lo declara: `RT` §12 define puertas medidas y **no umbrales**. Se eligió alto donde la fuente señala criticidad | Cambia el umbral de `QG-03` y las mediciones que lo citan |
| **200 ms** — el validador de figuras | `05` §8; **16 documentos** | Ídem | Ídem |
| **p99 de 500 ms** — la Api | `05` §8 | Ídem | Ídem |
| **30 s** — arranque en frío | `05` §8; **13 documentos** | Ídem | Alcanza a `PT-04` y al arranque de la imagen |
| **10 s** — batería de dominio | `05` §8; **28 documentos** | Ídem | Alcanza al gate de tiempo de la batería |
| ~~**20 peticiones/minuto**~~ | — | **Su fundamento se cayó con `D5`.** Ver §1 | **No se confirma acá**: sale de `PT-05` |

### 2.2 Coberturas mínimas · `A-3`

| Proyecto de código | Umbral | Nota |
|---|---|---|
| `Domain` | **90 / 85** | Los números son altos donde la fuente señala criticidad |
| `Application` | **85 / 80** | |
| `Infrastructure` | **85 / 80**, con **95** en el validador | El validador es el que `RN-B3` señala como el que más se rompe |
| `Api` | **75 / 70** | |
| `Api` — forma de la pirámide | **60 % integración / 40 % unitarias**, invertida | Incorporada el 2026-08-12; llevaba el rótulo `[ASUNCIÓN]` y no tenía fila en §22 |

**El motivo declarado:** *«`RT` §11 declara qué se prueba pero **no con qué umbral**»*.

### 2.3 Gates que no son de cobertura de líneas · `A-4`

| Proyecto de código | Gate | Por qué no es de líneas |
|---|---|---|
| `Contracts` | **100 % de DTOs ejercitados** | Son proyectos **sin lógica propia que cubrir** |
| `Web` | **100 % de pasos de guion** | La fuente define su verificación de otra forma |
| `Visor` | **cero llamadas de red** | Ídem |

**Confirmar `A-4` cambia la forma del gate, no su carácter bloqueante**, según declara el propio §22.

### 2.4 Métricas de negocio · `A-2`

| Métrica | Target |
|---|---|
| Etapas cerradas | **8 de 8** |
| Alumnos que entregan | **≥ 80 %** |
| Entregas revisadas | **100 %** |
| Advertencias por alumno | **≥ 1** |

**Es la de menor alcance:** *«Sólo cambia §8 y lo que la categoría 01 derive de ahí»*.

---

## 3. Qué NO entra en esta decisión, y conviene no confundir

El propio intake §22 lo separa, y se transcribe para que no se confirme de más:

> *«la tolerancia de **0.01** (sale de que el emisor redondea a 2 decimales), los **20 minutos** de
> `PT-01.c`, el semáforo de `PT-01.b` y los umbrales de las **cinco puertas técnicas** están declarados
> en las fuentes y **se transcriben sin cambio**».*

Y las marcas `[A VERIFICAR]` **tampoco son asunciones**: son incógnitas que las fuentes declaran y que
**se resuelven midiendo, no decidiendo** — la versión de plataforma del hosting (`D6`) es una de ellas,
y la contesta la fase `i`.

---

## 4. Las tres salidas, y qué deja cada una

| Salida | Qué pasa | Qué queda |
|---|---|---|
| **Confirmar todo** salvo el caudal | Las **doce filas vencidas** se cierran, y `QG-03`, `QG-04`, `QG-13` y `QG-14` **pasan a bloqueantes** sin ningún otro cambio | El caudal, esperando `PT-05` |
| **Confirmar por grupo** | Se cierra lo confirmado y lo demás sigue abierto **con su evento** | Los grupos no confirmados, que hay que volver a mirar |
| **Cambiar algún valor** | El valor nuevo se propaga a los documentos que lo citan, con su fila de control de cambios | Lo mismo que arriba, más la propagación |

**Lo que no es una salida:** dejarlo como está. Las doce filas están **vencidas** desde el 2026-08-15,
y con la forma de `Root-Rules.md` §12.2 cada una es un hallazgo **P1**.

---

## 5. Lo que este documento no sabe

- **Si los umbrales son los correctos.** Sólo declara de dónde salieron: ninguna fuente los fija, y se
  eligieron altos donde la fuente señala criticidad. **Confirmarlos no los vuelve medidos.**
- **Cuánto de lo que hoy pasa en verde seguiría pasando con los gates bloqueantes.** Los cuatro `QG`
  corren hoy sin poder frenar; **nadie midió qué haría cada uno si frenara**.
- **Si el caudal provisorio alcanza.** Es lo que `PT-05` va a decir.

---

## 6. Control de cambios

| Versión | Fecha | Cambios | Autor |
|---|---|---|---|
| 1.0 | 2026-08-26 | Emisión inicial. Trae la decisión `D1` —**doce filas vencidas en cinco documentos**— a una sola tabla, agrupada por lo que cada valor gobierna: umbrales de latencia y arranque (`A-5`), coberturas (`A-3`), gates que no son de líneas (`A-4`) y métricas de negocio (`A-2`). **§1 separa el caudal del resto antes de la tabla**, porque su fundamento **se cayó con el cierre de `D5`** y confirmarlo por arrastre sería el defecto que la 11.0 vino a corregir: su valor sale de `PT-05`, no de esta decisión. Declara lo que se destraba —**cuatro `QG` pasan a bloqueantes sin ningún otro cambio**—, lo que **no** entra —los valores que las fuentes sí declaran, y las marcas `[A VERIFICAR]`, que se resuelven midiendo—, y **lo que este documento no sabe**: si los umbrales son correctos, y qué frenaría cada gate si frenara. | Orquestador SDD |
