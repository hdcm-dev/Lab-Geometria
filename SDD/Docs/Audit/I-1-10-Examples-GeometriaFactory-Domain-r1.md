# Informe de Fase I — incremento 1 · `GeometriaFactory-Domain`

**Producto:** Fábrica de Geometría
**Documento:** I-1-10-Examples-GeometriaFactory-Domain-r1.md
**Versión:** 1.0
**Fecha:** 2026-08-29
**Autor:** Orquestador de Fase I
**Nivel:** Unidad de entrega · `GeometriaFactory-Api`, proyecto de código `GeometriaFactory-Domain`
**Instrumento:** `Master-Prompt.md` §7, Fase I, pasos 0 a 7
**Incremento:** **1**, y es **la primera corrida de la Fase I de este destino**
**Veredicto:** **APROBADO CON HALLAZGOS**, y §0 declara antes que nada que el auditor no fue independiente

---

## 0. El auditor no fue independiente, y esto va antes del veredicto

**El paso 7 pide un audit independiente y esta corrida no lo tuvo.** El mismo agente implementó el
sample, corrió la fase y escribió este informe. `Master-Prompt.md` §10 declara qué compra la
independencia —la ausencia de compromiso con lo auditado— y acá **no se tiene sobre el sample ni sobre
el snapshot**, que son las dos cosas que este informe evalúa.

**Qué queda no verificado por correlación:** que el snapshot de §6 describa el comportamiento
*correcto* y no simplemente el comportamiento *observado*. La comparación línea por línea es mecánica
y se sostiene sola; **que las diez líneas sean las diez que había que producir, no**.

**Se recomienda formalmente una segunda ronda con auditor invocado desde cero**, acotada a esa
pregunta. Es la misma reserva que el informe de la migración 10.0 → 13.3 §0 declaró, y por el mismo
motivo.

---

## 1. Alcance del incremento, y por qué es uno solo

**La Fase I es re-ejecutable y corre una vez por incremento** (`Master-Prompt.md` §7.2). Este
incremento es **el sample `domain/01-basico` y su contrato `VER-02001`**.

**Por qué no los diecinueve.** Los otros dieciocho **no están implementados**, y el paso 1 pide
implementarlos, correrlos y completar su evidencia con la salida real. Hacerlo de a un proyecto de
código es lo que permite que cada corrida cierre con su audit, que es lo que la fase exige; hacerlo
todo junto produce diecinueve evidencias sin ninguna auditada.

**Lo que este incremento NO alcanza queda declarado y no se toca:** los samples
`domain/02-intermedio` y `domain/03-avanzado`, y los dieciséis restantes de los otros proyectos de
código.

---

## 2. Paso 0 — Precondición dura de §7.1

| # | Condición | Evidencia |
|---|---|---|
| 1 | Existe código fuente del proyecto de código | **6 proyectos** bajo `src/`, **12 484 líneas** |
| 2 | `/samples` tiene al menos un sample implementado | [`samples/domain/01-basico/`](../../../samples/domain/01-basico/), **7 archivos `.cs`**, implementado el 2026-08-27 |
| 3 | Los tests corren | `dotnet test` sobre la solución: **3 de 3 proyectos en verde, 311 casos** |

**Las tres se cumplen, y la segunda se cumplió el 2026-08-27 y no antes.** Hasta esa fecha
`/samples` tenía **20 archivos y los 20 eran `.md`**: la fase **no podía arrancar**, y llevaba
dieciocho días sin poder hacerlo con el código ya escrito.

---

## 3. Paso 1 — Pasada de ejecución de la 10

**`VER-02001` pasa de `No verificado — sin código` a `VERIFICADO`**, con su salida real, su fecha y su
exit code, en
[`../Unidades-Entrega/GeometriaFactory-Api/10-Examples/ejemplo-01-basico-dominio.md`](../Unidades-Entrega/GeometriaFactory-Api/10-Examples/ejemplo-01-basico-dominio.md)
**2.0** §9.

| Criterio de aceptación | Resultado |
|---|---|
| `exit_code: 0` | **Cumple** |
| Las **cuatro** líneas de `stdout_contiene` | **Cumplen las cuatro** |
| `stdout_no_contiene: "Excepciones: 1"` | **Cumple** — la corrida declara `Excepciones: 0` sobre 9 operaciones |
| Comparación completa contra el snapshot de §6 | **CONFORME · las 10 líneas coinciden** |

### 3.1 Y la primera corrida NO cumplió, que es el hallazgo del incremento

**El 2026-08-27 fallaron cuatro de los cinco criterios**, y el paso 1 declara que *«un
`criterio_aceptacion` que falla es un hallazgo del incremento, no un documento pendiente»*. Éste es
ese hallazgo, con su desenlace.

**No era un defecto del sistema.** El sistema emitía `ADMINISTRATOR_ALREADY_CONFIGURED` y el contrato
pedía `ADMINISTRADOR_YA_CONFIGURADO`. La forma inglesa es la que **decidió el Product Owner el
2026-08-12** —`Norma-De-Nomenclatura.md` §5.3, frontera **`F-03`**: *«los 101 van a inglés: los 80
internos y los 21 de contrato»*, declarado además **cambio de contrato**—, y cuyos tramos de renombre
documental **se suspendieron el 2026-08-13**. El código se escribió en inglés desde el primer archivo;
**la documentación quedó a mitad de camino y nadie lo notó durante dieciséis días**.

**Lo destapó el sample y no una revisión de escritorio**, que es la propiedad que la pasada de
ejecución aporta y que ninguna lectura del corpus tenía: el corpus es **coherente consigo mismo** en
castellano, y sólo deja de serlo cuando se lo corre contra el sistema.

**Se reparó en el documento y no en el código**, con la decisión del Product Owner del **2026-08-29**,
y **el mapeo se leyó de `ConditionCode.cs`** —donde cada comentario `<summary>` empareja la forma
castellana con su constante— en lugar de elegirse acá: **once ocurrencias, siete códigos**.

**Y una constancia de método que conviene dejar escrita.** Entre el 2026-08-27 y el 2026-08-29 el
snapshot del sample **quedó incumpliendo su contrato a propósito**. Ajustarlo habría sido un renglón y
habría decidido en silencio que el código le gana a veintiún documentos; debajo había una decisión del
Product Owner **a medio aplicar**, que sólo aparece si alguien se detiene.

---

## 4. Pasos 2 a 6

| Paso | Qué pide | Qué pasó |
|---|---|---|
| **2** · 11-Documentacion, Momento 2 | Actualizar los documentos que el incremento afecta | **Ninguno afectado.** Los 15 artefactos de la 11 siguen en `Planificado` y este incremento no toca ninguno: un sample de la 10 no es insumo de ellos. **Se declara en lugar de omitirse** |
| **3** · Triaje de la bitácora de eventualidades | Toda `EVE-XXXXX` abierta recibe destino | **No hay bitácora emitida**: cero `EVE-*` en el corpus vivo. Nada que triar, y **la ausencia de la bitácora queda como hallazgo `I-01`** |
| **4** · `AGENTS.md` en la raíz | Emitirlo, derivado de `Contrato-Agentes.md` | **NO SE PUDO.** `Contrato-Agentes.md` **no existe** y es su única fuente. Hallazgo **`I-02`**, y es el que bloquea al paso en las corridas siguientes |
| **5** · Ensayo de entrega automatizado | Correr los comandos documentados en entorno limpio y verificar sus aserciones | **Los tres comandos del README del sample corren y salen 0**: `bash scripts/build.sh`, `dotnet run --project samples/domain/01-basico` y su `--verificar`. Ver la reserva de abajo |
| **6** · Matriz de sensado de deriva | Actualizar las filas que el incremento toca | **`SD-02001` pasa a `Verificado`**, primera fila sensada de esa matriz. Quedan **88 en `Sin verificar`** — 11 en `-Api` y 77 en `-Web` |

**Reserva sobre el paso 5, y es del entorno y no del producto.** El ensayo corrió en un contenedor con
**sólo el kit de .NET**, y ahí `scripts/build.sh` sale **127**: invoca `build-visor.sh`, que necesita
Node. Repetido con Node 22 —que es lo que el entorno contenido del repositorio declara como
prerrequisito— **sale 0**. Se declara porque un ensayo de entrega que no dice en qué entorno corrió no
prueba lo que dice probar.

---

## 5. Hallazgos

| # | Nivel | Hallazgo | Quién lo cierra |
|---|---|---|---|
| **`I-01`** | **P2** | **No hay bitácora de eventualidades**: cero `EVE-*` en el corpus vivo, y el paso 3 de la fase la da por existente. Sin ella, una eventualidad de la codificación no tiene dónde registrarse y el triaje de cada corrida es vacío por construcción | La categoría 11 del destino, emitiéndola |
| **`I-02`** | **P1** | **`Contrato-Agentes.md` no existe**, y es la única fuente de `AGENTS.md`. El paso 4 **no se puede ejecutar** en ésta ni en ninguna corrida siguiente hasta que se emita | La categoría 11 del destino |
| **`I-03`** | **P2** | **El residuo documental de `F-03` es de 644 ocurrencias en 70 documentos, sobre 31 códigos**, medido sobre el corpus vivo el 2026-08-29. Este incremento reparó **11 en 1 documento**; el resto sigue citando códigos que el sistema no emite. **Cada uno es un `criterio_aceptacion` que va a fallar** cuando su sample se implemente | Una unidad de trabajo propia, con el mapeo de `ConditionCode.cs` |
| **`I-04`** | **P3** | La corrida **no tuvo auditor independiente**, y §0 declara qué queda no verificado por correlación | Una segunda ronda desde cero |

**`I-03` es el que más conviene mirar**, porque es predictivo y no descriptivo: **el defecto que este
incremento encontró de casualidad está esperando en los otros dieciocho samples**, y ahora se sabe
dónde y cuántos.

---

## 6. Veredicto

**APROBADO CON HALLAZGOS.** El incremento cumple lo que la fase le pide: la precondición se verificó y
se reportó con su evidencia, el contrato del incremento pasó de promesa a evidencia con su salida
real, la fila de deriva que le corresponde quedó sensada, y **el criterio que falló se trató como
hallazgo del incremento y no como documento pendiente**, que es literalmente lo que el paso 1 exige.

**Los dos pasos que no se ejecutaron —3 y 4— no se declaran cumplidos**: se declaran imposibles, con
su hallazgo y su dueño.

**Lo que este veredicto NO dice.** No dice que el snapshot de §6 sea correcto: dice que el sistema lo
cumple. La diferencia es de §0 y es la que una segunda ronda tendría que cerrar.

---

## 7. Punto de continuación

| Qué sigue | Por qué en ese orden |
|---|---|
| **`I-02`**, emitir `Contrato-Agentes.md` | Desbloquea el paso 4 para todas las corridas siguientes. Mientras no exista, cada incremento arrastra el mismo hallazgo |
| **`I-03`**, terminar el renombre de `F-03` | **644 ocurrencias en 70 documentos.** Hacerlo antes de implementar los samples siguientes evita que cada uno repita el fallo de éste |
| **Incremento 2**: `domain/02-intermedio` y `domain/03-avanzado` | Cierran el proyecto de código `GeometriaFactory-Domain` completo |
| Incrementos 3 y siguientes | Un proyecto de código por vez, en el orden topológico de `Pipeline-Producto.md` §2 |

---

## 8. Control de cambios

| Versión | Fecha | Cambios | Autor |
|---|---|---|---|
| 1.0 | 2026-08-29 | Emisión inicial. **Primera corrida de la Fase I de este destino**, incremento 1, acotada al sample `domain/01-basico` y a su contrato `VER-02001`. **§0 declara antes que nada que el auditor no fue independiente** y qué queda no verificado por correlación. La precondición dura de §7.1 se verificó con su evidencia y **se cumplió el 2026-08-27 y no antes**. `VER-02001` pasa a **VERIFICADO** con su salida real; **su primera corrida incumplió cuatro de cinco criterios** y §3.1 desarrolla el hallazgo: no era un defecto del sistema sino el **residuo del renombre `F-03`**, decidido el 2026-08-12 y suspendido el 2026-08-13, reparado **en el documento** con la reconfirmación del Product Owner del 2026-08-29. `SD-02001` es la **primera fila sensada** de la matriz de deriva. **Los pasos 3 y 4 no se ejecutaron y no se declaran cumplidos**: no hay bitácora de eventualidades ni `Contrato-Agentes.md`. Cuatro hallazgos, de los cuales **`I-03` es predictivo**: el residuo de `F-03` son **644 ocurrencias en 70 documentos** y está esperando en los otros dieciocho samples. | Orquestador de Fase I |
