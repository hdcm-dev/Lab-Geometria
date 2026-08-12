# Reporte: primera aplicación del instrumento de corte

| Campo | Valor |
|---|---|
| Versión | 1.0 |
| Fecha | 2026-08-12 |
| Estado | **Aprobado** |
| Autor | Orquestador SDD |
| Origen | Pedido del Product Owner, 2026-08-12: dejar registro de si el instrumento funcionó, para análisis posterior y eventual incorporación al framework SDD |
| Instrumento reportado | `Observacion-Ciclo-De-Correccion-Sin-Corte.md` 1.1 §4 |
| Naturaleza | **Evidencia de una sola aplicación.** No es una validación: §5 declara qué no prueba |

---

## 1. Qué se reporta

El 2026-08-11 se adoptó un instrumento de tres partes para cortar un ciclo de corrección que no terminaba. El 2026-08-12 se aplicó **por primera y única vez**, en la pasada final sobre `Handoff-Checkout.md`. Este reporte deja la evidencia de esa aplicación en forma utilizable para un análisis posterior.

**El instrumento** —resumido para que este reporte se lea solo—:

| Parte | Qué dice |
|---|---|
| **Clasificación** | Todo hallazgo se clasifica antes de despachar: **sustantivo** (cambia una decisión, un contrato o un caso de prueba), **de fondo** (la fuente afirma algo falso que induce a error), **de forma** (recuento, nomenclatura, cita envejecida que no induce error). Sólo los dos primeros abren un ciclo; el tercero va a una lista |
| **Criterio de corte** | El ciclo cierra si: dos tandas sin hallazgos sustantivos ni de fondo; o una familia de defecto se descubre por tercera vez; o una tanda existe sólo para corregir la anterior |
| **Plan con estado** | Toda tanda se ejecuta contra un plan con partes marcables, y antes de cada parte se declara qué avanzó y cuánto falta |
| **Regla preventiva** (§4.4, aporte del Product Owner) | El intake cambia **por decisión**, no por inconsistencia. Tras cada decisión, y **antes** de propagar, una pasada de estabilización del intake contra sí mismo |

## 2. El problema que motivó el instrumento, medido

Datos del propio repositorio, no estimaciones.

| Medición | Valor |
|---|---|
| Versiones del `PRODUCT-INTAKE` en cuatro días | **28** (1.0 → 1.28) |
| De ellas, por **decisión** o conocimiento nuevo | **12** |
| De ellas, por **inconsistencia** (fondo + forma) | **16** |
| Versiones que existen **sólo para corregir la versión anterior** | **8 de 28** |
| Veces que la misma familia de defecto —«recuento congelado en celda o encabezado»— se descubrió como hallazgo nuevo | **6**, en seis fases distintas |
| Tandas consecutivas cuyos hallazgos resultaron **subcontados** al verificarlos | **13, sin una sola excepción** |

**El caso que resume el diagnóstico**: la versión **1.26** del intake corrige un error de conteo cometido en el párrafo que la **1.25** escribió para declarar ese mismo defecto. El instrumento creado para declarar el problema reprodujo el problema.

## 3. La aplicación

**Contexto**: pasada final sobre `Handoff-Checkout.md`, con el alcance declarado en el encargo y la instrucción explícita de **anotar y no corregir** lo que quedara fuera.

### 3.1 Lo que la clasificación decidió

| Clase | Cuántos | Qué se hizo | Detalle |
|---|---|---|---|
| Sustantivo | **0** | — | — |
| **De fondo** | **2** | Corregidos | (a) Un índice de la API declaraba «quince de las quince rutas no están decididas» donde son **catorce**, contradiciendo a su propia sección dos apartados más abajo. (b) El resumen de traspaso afirmaba que los nombres de tipos están abiertos en **los siete** proyectos **citando filas que no existen**: son seis, y el front no lo tiene porque su única superficie es la HTTP |
| **De forma** | **3** | **Anotados, no corregidos** | Filas de una tabla de cambios fuera de orden; un recuento viejo en un informe de auditoría; una cifra de contexto desactualizada en otra observación |

### 3.2 El contrafáctico

Con el método anterior, **cada uno de los tres hallazgos de forma habría abierto su propia tanda**, y cada tanda habría desactualizado algún documento, generando la siguiente. Es el mecanismo exacto que produjo las dieciséis versiones por inconsistencia de §2.

### 3.3 Lo que el instrumento dejó pasar, y era correcto que pasara

Los dos de fondo no eran cosméticos:

- El primero es **el residuo vivo de un hallazgo que el orquestador había declarado «no reproducible»** mirando líneas distintas de las citadas. Sobrevivió dos rondas de auditoría y dos informes lo reportaron.
- El segundo **no lo pidió nadie**: lo encontró el subagente al verificar una afirmación del documento contra sus fuentes. Es el más consecuente de los dos, porque esa fila **le llega al Product Owner como decisión pendiente**, y le declaraba un alcance mayor del real.

### 3.4 La medida de verificación previa, aplicada al propio orquestador

La medida `C-1` de `Observacion-Ejecucion-De-La-Orquestacion.md` —verificar con herramienta antes de incorporar— detectó **un defecto del propio orquestador**: la fila de control de cambios que había insertado quedó con **una celda de más y fuera de orden cronológico**. Se corrigió antes del commit.

Con el método anterior habría entrado al repositorio y la habría encontrado una auditoría posterior. Es la misma clase de defecto que §2 registra seis veces.

## 4. Efectos observados

| # | Observación | Evidencia |
|---|---|---|
| O-1 | La clasificación **redujo el trabajo despachado** de cinco hallazgos a dos, sin dejar pasar ninguno que cambiara una decisión, un contrato o un caso de prueba | §3.1 |
| O-2 | El criterio de corte **se cumplió dos veces antes de aplicarse**, y las dos son verificables: la pasada existía sólo porque la tanda anterior desactualizó el documento (señal A), y la familia «recuento congelado» iba por su sexta aparición (condición 2) | `Observacion-Ciclo-De-Correccion-Sin-Corte.md` §5 |
| O-3 | Declarar el alcance **como parte del encargo** produjo el efecto buscado: el subagente encontró cinco cosas fuera de alcance y **no corrigió ninguna**, las clasificó | §3.1 |
| O-4 | La verificación previa capturó un defecto del orquestador en su primera aplicación | §3.4 |
| O-5 | El hallazgo de más valor de la pasada **no estaba en el encargo**: surgió de verificar una afirmación contra su fuente, que es la medida `C-1` operando sobre el contenido y no sobre el recuento | §3.3 |

## 5. Qué NO prueba este reporte

Se declara para que el análisis posterior no le atribuya más de lo que sostiene.

1. **Es una sola aplicación.** No hay serie, no hay grupo de control, y la pasada fue **la última del ciclo**: no se puede saber si el instrumento habría cortado un ciclo en curso o sólo confirmó uno que ya se había decidido cortar.
2. **La clasificación la hizo el mismo actor que se beneficia de ella.** Clasificar un hallazgo como «de forma» reduce el trabajo propio. Los tres clasificados así lo estaban correctamente —cualquiera puede verificarlo en §3.1—, pero el sesgo existe y en una serie larga habría que medirlo.
3. **La regla preventiva de §4.4 no se aplicó nunca.** La pasada de estabilización del intake contra sí mismo se adoptó pero no se ejecutó: el ciclo cerró antes. **Es la parte del instrumento con más potencial y cero evidencia.**
4. **No se midió el costo.** No hay dato sobre cuánto ahorró en tokens ni en tiempo, sólo el contrafáctico cualitativo de §3.2.

## 6. Material para el análisis de incorporación al framework

Lo que este producto sugiere que valdría estudiar, sin afirmar que corresponde incorporarlo.

| Candidato | Dónde encajaría | Qué habría evitado en este producto |
|---|---|---|
| **Pasada de estabilización del intake** tras cada decisión y antes de propagar | Entre la actualización del intake y el despacho de la propagación | La familia «recuento congelado» apareció **6 veces en 6 fases**; una sola pasada la habría capturado entera |
| **Clasificación obligatoria de hallazgos** en el informe de auditoría, no en la corrección | En la plantilla de informe: que el auditor clasifique además de asignar severidad | Habría separado desde el origen lo que abre ciclo de lo que va a una lista |
| **Criterio de corte del ciclo de corrección** | En la definición de fase: cuándo una fase deja de aceptar correcciones | El ciclo lo cortó el Product Owner desde afuera, no el método |
| **Registro de proceso** fase × proyecto × ronda × veredicto | Como artefacto de nivel producto, obligatorio y consultado antes de abrir cada tanda | **Dos controles no se ejecutaron** —la Fase B del proyecto principal y la ronda 2 de la maqueta— y los detectó el inventario de traspaso |
| **Alcance declarado en el encargo**, con obligación de anotar y no corregir lo que quede fuera | En la forma de despacho a subagentes | Es lo que produjo O-3 |
| **Verificación con herramienta antes de incorporar** una entrega | En la definición del ciclo de despacho | 13 tandas con hallazgos subcontados **sin excepción** |
| **Distinguir dos ejes de estado** —ciclo de vida del contenido y situación de aprobación— con enum propio cada uno | En las guías que hoy los mezclan | Ocho documentos declararon «tres estados contradictorios» que eran **dos ejes bien puestos**; igualarlos habría destruido información |

## 7. Control de cambios

| Versión | Fecha | Cambios | Autor |
|---|---|---|---|
| 1.0 | 2026-08-12 | Emisión inicial, a pedido del Product Owner. Registra la medición del problema, la única aplicación del instrumento con su contrafáctico, los cinco efectos observados, los cuatro límites de lo que la evidencia sostiene, y siete candidatos de incorporación al framework con lo que cada uno habría evitado en este producto. | Orquestador SDD |
