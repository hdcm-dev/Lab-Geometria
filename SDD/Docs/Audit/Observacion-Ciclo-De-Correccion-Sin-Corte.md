# Observación: el ciclo de corrección no tenía criterio de corte

| Campo | Valor |
|---|---|
| Versión | 1.0 |
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

Faltaban dos cosas, y son distintas.

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

## 5. Aplicación inmediata

Al momento de emitirse esta observación, el criterio §4.2 se cumple **dos veces**: la señal A (la pasada pendiente sobre `Handoff-Checkout.md` existe **sólo** porque la tanda anterior lo desactualizó) y la condición 2 (la familia «recuento congelado» va por su sexta aparición).

En consecuencia: **se ejecuta esa última pasada, porque `Handoff-Checkout.md` es el documento sobre el que el Product Owner decide y hoy declara abierto lo que se cerró, y el ciclo se cierra ahí.** Lo que aparezca después se anota y no se corrige, salvo que sea sustantivo o de fondo.

## 6. Control de cambios

| Versión | Fecha | Cambios | Autor |
|---|---|---|---|
| 1.0 | 2026-08-11 | Emisión inicial, a pedido del Product Owner, que detectó la recursividad que el orquestador no vio. Incluye la medición sobre las veintiocho versiones del intake, las dos señales que la revelan, y el instrumento de tres partes: clasificación de hallazgos, criterio de corte y plan con estado. | Orquestador SDD |
