# Observación: el ciclo de corrección no tenía criterio de corte

| Campo | Valor |
|---|---|
| Versión | 1.2 |
| Fecha | 2026-08-11 |
| Estado | **Aprobado** |
| Autor | Orquestador SDD |
| Origen | Observación del Product Owner, 2026-08-11: «¿no estarás entrando en una suerte de recursividad?» |
| Relacionado | `Observacion-Ejecucion-De-La-Orquestacion.md` 1.2 |

---

## 1. La anomalía

El orquestador **entró en un ciclo de corrección sin criterio de corte**, y **no lo detectó**: lo detectó el Product Owner. Cada tanda de correcciones dejaba documentos desactualizados que motivaban la tanda siguiente, y el orquestador la despachaba sin preguntarse si el hallazgo cambiaba algo que alguien fuera a construir.

Un corpus de seiscientos cuarenta documentos **siempre** tiene una inconsistencia más si se la busca. Sin criterio de corte, el ciclo no termina por agotamiento: termina cuando alguien lo interrumpe desde afuera.

## 2. La medición

El `PRODUCT-INTAKE` fue de la versión **1.0 a la 1.28** en cuatro días. Clasificadas por lo que cada versión hizo:

| Clase | Versiones | Cuántas |
|---|---|---|
| **Decisión del Product Owner** o conocimiento nuevo del producto | 1.1, 1.3, 1.4, 1.5, 1.6, 1.7, 1.12, 1.13, 1.19, 1.22, 1.23, 1.24, 1.27 | 13 |
| **Corrección de fondo**: la fuente afirmaba algo falso que cambiaba una lectura | 1.2 (×2), 1.9, 1.15, 1.20 | 5 |
| **Corrección de forma**: recuento congelado, nomenclatura, rótulo, orden de tabla | 1.8, 1.10, 1.11, 1.14, 1.16, 1.17, 1.18, 1.21, 1.25, 1.26, 1.28 | 11 |

### 2.1 Las dos señales que había que mirar y nadie miró

**Señal A — versiones que existen sólo para arreglar la versión anterior.** Son **ocho de veintiocho**: 1.8 corrige 1.7, 1.10 corrige residuos de 1.9, 1.11 y 1.14 corrigen residuos de la incorporación de F-26, 1.16 y 1.17 corrigen la misma tabla dos veces, 1.21 la misma familia por quinta vez, y **1.26 corrige un error de conteo cometido en el párrafo que 1.25 escribió para declarar ese mismo defecto**.

Ese último caso es el diagnóstico entero en una línea: **el instrumento creado para declarar el defecto reprodujo el defecto**.

**Señal B — la misma familia de defecto descubierta N veces.** «Recuento congelado en una celda o encabezado» se descubrió como hallazgo nuevo en **1.16, 1.17, 1.18, 1.20, 1.21 y 1.25** — seis veces, en seis lugares distintos, tratada cada vez como incidente y nunca como propiedad del documento. Recién en 1.21 quedó escrito que era un patrón, y aun así siguió apareciendo.

### 2.2 Qué muestran juntas

El rendimiento cayó y la naturaleza de lo encontrado cambió: las primeras versiones incorporaban decisiones del Product Owner; las últimas corrigen nomenclatura, recuentos y el orden de una tabla. **Ninguna de las once correcciones de forma cambió una decisión, un contrato ni un caso de prueba.**

## 3. Causa

**Ampliado el 2026-08-12**, a pedido del Product Owner: «lo que me interesa saber es por qué caíste en ese ciclo». La versión 1.0 nombraba dos causas; son **cinco**, y las tres que faltaban son las que explican por qué el orquestador **no lo vio desde adentro**.

### 3.1 No había un paso de decisión entre el hallazgo y el despacho

La salida de la auditoría se convertía **automáticamente** en la entrada del ciclo siguiente. No existía el momento de preguntarse *si vale corregir esto*. Y sobre un corpus de seiscientos cuarenta documentos, una auditoría **siempre** encuentra algo: mientras el ciclo se alimente de «hay un hallazgo», no termina, porque siempre lo hay.

Es la causa que el instrumento de §4.1 ataca directamente.

### 3.2 Los puntos abiertos se leyeron como lista de tareas, y son un mecanismo de diferimiento

**Es la causa más específica y probablemente la principal.**

Una fila `PA-XX` existe **para que una fase pueda avanzar sin decidir**. Su función es **no bloquear**: es lo que permite emitir una categoría cuando falta un dato que no le corresponde inventar. El framework admite explícitamente el traspaso con puntos abiertos declarados, y este mismo corpus sostiene que un producto que declara lo que no está decidido vale más que uno que aparenta estar completo.

Cuando el resumen de traspaso devolvió las **cuarenta y siete** filas juntas, el orquestador las leyó como un pendiente a vaciar antes de entregar. **Nadie pidió eso.** Convirtió un instrumento de *seguir adelante* en uno de *no poder entregar*, y con eso se dio a sí mismo una lista de trabajo que el método no exigía.

El síntoma que lo delata: la auditoría de las cuarenta y siete encontró **treinta y seis verdaderas y abiertas**. O sea que el setenta y seis por ciento de la lista **estaba correctamente abierta** y no había nada que hacer con ella.

### 3.3 Cada corrección fabrica trabajo, estructuralmente

En un corpus donde los documentos se citan entre sí **con su versión**, cualquier edición desactualiza a otro. Corregir el intake deja viejas las citas de las categorías; corregir las categorías deja viejo el resumen de traspaso; corregir el resumen deja viejo algo más.

Sin una regla que declare que **una cita envejecida no amerita un ciclo**, la sucesión no converge. Esa regla no existió hasta §4.1.

### 3.4 No había definición de terminado

El framework la tiene: la Fase H y el traspaso de §12. El orquestador la pasó de largo. **Sin un «terminado», cualquier defecto es motivo suficiente para seguir**, porque no hay contra qué comparar el estado actual.

### 3.5 Y un sesgo del orquestador, que es el que impidió verlo

Cada rechazo de auditoría se vivió como algo a remediar, y cerrarlo se sintió como avanzar. El orquestador **optimizó para «cero hallazgos abiertos» en lugar de «la especificación sirve para construir»**.

Son objetivos distintos y el segundo era el real. Con el primero el trabajo no termina nunca; con el segundo había terminado varios días antes.

Este sesgo es el que explica la observación más incómoda del registro: **el ciclo se cortó dos veces y las dos desde afuera** —el Product Owner exigiendo cerrar los huecos de proceso, y después preguntando si no había recursividad—. El mecanismo no tenía freno propio porque quien debía frenarlo medía su avance con la métrica que el ciclo alimentaba.

### 3.6 Qué NO fue causa

Se declara para no sobreextender el diagnóstico. **Los tres huecos de proceso cerrados el 2026-08-11 no eran el ciclo**: eran controles que no se habían ejecutado, y la Fase B de `GeometriaFactory-Api` —el proyecto principal— rechazó con un P0 real cuando por fin se auditó. **Y las decisiones del Product Owner tampoco**: una decisión que cambia el intake y se propaga es el método funcionando, según §4.4.

**No había criterio de corte.** El framework fija el cierre de la especificación en la Fase H, y el orquestador lo pasó de largo: siguió corrigiendo porque cada auditoría entregaba algo corregible, sin preguntar si valía corregirlo.

**No había plan con estado.** Las correcciones se despachaban de a una, según lo que la última auditoría hubiera dicho. Sin un plan con partes marcables, no hay forma de saber **cuánto falta** ni de notar que se está volviendo sobre lo mismo. Es la misma carencia que `Observacion-Ejecucion-De-La-Orquestacion.md` §6.2 ya identificó para el consumo de contexto; acá se manifiesta como pérdida de terminación.

## 4. Instrumento adoptado

### 4.1 Clasificación obligatoria de todo hallazgo

Antes de despachar una corrección, el hallazgo se clasifica:

| Clase | Definición | Qué se hace |
|---|---|---|
| **Sustantivo** | Cambia una **decisión**, un **contrato** entre capas, o un **caso de prueba** | Se corrige, y la corrección se propaga |
| **De fondo** | La fuente afirma algo **falso** que induce a una lectura equivocada, aunque no cambie una decisión | Se corrige en la fuente y se propaga |
| **De forma** | Recuento, nomenclatura, rótulo, orden, cita envejecida que no induce error | **Se anota, no se corrige.** Se arregla cuando alguien toque ese documento por otro motivo |

**La regla**: sólo lo sustantivo y lo de fondo abren un ciclo de corrección. Lo de forma va a una lista.

### 4.2 Criterio de corte

El ciclo se cierra cuando se cumple **cualquiera** de estas tres:

1. **Dos tandas consecutivas sin hallazgos sustantivos ni de fondo.**
2. **Una familia de defecto se descubre por tercera vez.** No es un hallazgo: es una propiedad del documento, y corresponde declararla en el documento en vez de perseguirla.
3. **Una tanda existe sólo para corregir la anterior.** Es la señal A, y obliga a detenerse y revisar el método, no a despachar otra tanda.

### 4.3 Plan con estado

Toda tanda se ejecuta contra un plan con partes marcables, según `Observacion-Ejecucion-De-La-Orquestacion.md` §6.2. Antes de despachar cada parte, el orquestador declara **qué parte del plan avanzó y cuánto falta**. Un ciclo sin plan no puede detectar que está volviendo sobre lo mismo.

### 4.4 La distinción que faltaba: el intake cambia por decisión, no por inconsistencia

Aportada por el Product Owner al leer la medición, y **es la regla que evita la anomalía en vez de detectarla tarde**.

**El intake debe cambiar cuando hay una decisión.** Es su función. Y las decisiones aparecen a mitad de camino con toda legitimidad: la validación de la maqueta de la Fase B2 produjo cambios aprobados que obligaron a replantear parte de la solución, y eso está bien —es el método funcionando, no fallando—. Un replanteo así se propaga en orden y nadie debería resistirlo.

**El intake no debería cambiar por inconsistencia.** Que una sección enumere cinco funciones donde otra declara seis, que un rango quede congelado en `E-7`, que la prosa nombre `RN-12` donde va `RN-11`: **eso no es conocimiento nuevo, es un defecto de emisión del propio intake**. Corregirlo es necesario, pero que llegue como versión nueva significa que se descubrió tarde, de a uno, y por una fase que estaba haciendo otra cosa.

**La medición lo confirma.** De las veintiocho versiones, **dieciséis fueron por inconsistencia** —las cinco de fondo y las once de forma— y **doce por decisión o conocimiento nuevo**. Más de la mitad de la historia del documento no debería haber existido como historia del documento.

**Consecuencia operativa.** Después de toda decisión que modifique el intake, y **antes** de propagar nada:

1. **Pasada de estabilización sobre el intake solo**, contra sí mismo: que sus conjuntos cierren, que sus rangos alcancen lo que hoy existe, que ninguna sección contradiga a otra, y que ninguna enumeración enumere de menos. Es una sola pasada, mecánica, y encuentra de una vez lo que si no aparece de a seis.
2. **Recién entonces se propaga**, y por orden de categorías.

La familia «recuento congelado» habría caído entera en **una** pasada de estas. Apareció **seis veces** repartida en seis fases distintas porque nunca se hizo.

**Y da un criterio de diagnóstico**: si el intake sube versión y el motivo **no es una decisión**, hay que preguntarse por qué la inconsistencia no se detectó antes de consumirlo. La respuesta es siempre la misma —porque no hubo pasada de estabilización— y es lo que hay que corregir, no el síntoma.

## 5. Aplicación inmediata

Al momento de emitirse esta observación, el criterio §4.2 se cumple **dos veces**: la señal A (la pasada pendiente sobre `Handoff-Checkout.md` existe **sólo** porque la tanda anterior lo desactualizó) y la condición 2 (la familia «recuento congelado» va por su sexta aparición).

En consecuencia: **se ejecuta esa última pasada, porque `Handoff-Checkout.md` es el documento sobre el que el Product Owner decide y hoy declara abierto lo que se cerró, y el ciclo se cierra ahí.** Lo que aparezca después se anota y no se corrige, salvo que sea sustantivo o de fondo.

## 6. Control de cambios

| Versión | Fecha | Cambios | Autor |
|---|---|---|---|
| 1.2 | 2026-08-12 | **§3 ampliada de dos causas a cinco**, a pedido del Product Owner. Las tres que faltaban explican por qué el orquestador no vio el ciclo desde adentro: los **puntos abiertos leídos como lista de tareas** cuando son un mecanismo de diferimiento —la causa principal, y el 76 % de esa lista estaba correctamente abierta—; que **cada corrección fabrica trabajo** en un corpus cuyos documentos se citan con su versión; y el **sesgo de optimizar para cero hallazgos abiertos** en lugar de para una especificación que sirva para construir, que es el que explica que el ciclo se cortara dos veces desde afuera. Se agrega §3.6 con lo que **no** fue causa, para no sobreextender el diagnóstico. | Product Owner (pregunta) · Orquestador SDD (análisis) |
| 1.1 | 2026-08-11 | **§4.4 nueva**, aportada por el Product Owner: **el intake cambia por decisión, no por inconsistencia**. Un replanteo a mitad de camino por una decisión —como el que produjo la validación de la maqueta— es el método funcionando; una versión nueva por un recuento congelado es un defecto de emisión descubierto tarde. La medición lo confirma: **dieciséis de las veintiocho versiones fueron por inconsistencia**. Se adopta la pasada de estabilización del intake contra sí mismo antes de propagar, que habría capturado en una sola vez la familia que apareció seis veces. | Product Owner (aporte) · Orquestador SDD (redacción) |
| 1.0 | 2026-08-11 | Emisión inicial, a pedido del Product Owner, que detectó la recursividad que el orquestador no vio. Incluye la medición sobre las veintiocho versiones del intake, las dos señales que la revelan, y el instrumento de tres partes: clasificación de hallazgos, criterio de corte y plan con estado. | Orquestador SDD |
