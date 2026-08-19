# Análisis — el refresco al restaurar la ventana, y qué alternativas hay

**Producto:** Fábrica de Geometría
**Documento:** Analisis-Refresco-Al-Restaurar-La-Ventana.md
**Versión:** 1.0
**Estado:** **Análisis. Ningún cambio aplicado**, por pedido explícito
**Fecha:** 2026-08-19
**Autor:** Orquestador SDD
**Nivel:** Unidad de entrega `GeometriaFactory-Web`

---

## 0. La premisa está reconstruida y NO confirmada

**Esto hay que leerlo antes que nada.** El pedido nombra «esa página particular» y un retoque ya
hecho para que **no se refresque**. **Ese retoque no está en ningún árbol de este workspace**, y se
buscó:

| Dónde se buscó | Resultado |
|---|---|
| `GeometriaFactory-Web`, árbol de trabajo | **Limpio**, sin cambios sin commitear |
| `ShouldRender`, `@key`, `firstRender`, `OnAfterRender` | **Cero ocurrencias** en toda la unidad |
| `Blazor.start`, `reconnectionHandler`, `autostart="false"` | **Ninguna**: `App.razor` carga `blazor.web.js` sin configuración |
| `setInterval`, `setTimeout`, `PeriodicTimer`, `http-equiv="refresh"` | **Ninguno** en la unidad |
| El resto del workspace | Los únicos `ReconnectModal.razor.js` son de plantilla, **sin modificar** |

**Entonces este documento analiza el mecanismo, no el retoque.** Todo lo de abajo se sostiene sobre
lo que el árbol **sí** dice de esta unidad, y es verificable; si la página en cuestión resulta ser de
otra aplicación, **lo que se cae es la ubicación de este documento, no su contenido**, porque el
mecanismo es el de Blazor Server y no el de este producto.

---

## 1. Por qué minimizar y restaurar produce un refresco

**Lo que el árbol declara de esta unidad:**

- **Interactividad por circuito.** `Program.cs` registra `AddInteractiveServerComponents()` y
  `AddInteractiveServerRenderMode()`, y **once pantallas declaran `@rendermode InteractiveServer`
  una por una**. La página vive de un circuito abierto contra el servidor.
- **Sin ninguna configuración de reconexión.** `App.razor` carga `_framework/blazor.web.js` a secas:
  **rigen los valores por omisión del marco**, no una decisión del producto.
- **El estado de la pantalla muere con el circuito.** `SessionState` está registrado como
  **`Scoped`**, que en Blazor Server es *por circuito*. `SessionTokenStore` es `Singleton` y
  sobrevive; `SessionState` no.

**La cadena, entonces:**

1. La ventana se minimiza. El navegador **estrangula los temporizadores** de la pestaña en segundo
   plano y, según el sistema, puede suspender el socket.
2. El *keep-alive* del circuito deja de llegar a tiempo.
3. El cliente entra en reconexión y agota sus reintentos por omisión; el servidor descarta el
   circuito pasada su retención por omisión.
4. **La reconexión fallida termina en una recarga completa de la página.** Circuito nuevo,
   `SessionState` nuevo, pantalla desde cero.

**No es un defecto de la página, y por eso ninguna corrección dentro de la página lo arregla.**

## 1.1 Y hay un hallazgo que conviene mirar antes de elegir alternativa

**`SUP-11`, el aviso de estado degradado y reconexión, existe y NUNCA SE ENCIENDE.** El propio
componente lo declara:

> *«ETAPA `b`. Los dos tramos que `CU-10` distingue —el servicio de datos que no responde y el
> circuito que se corta— se sostienen con estado que esta etapa no tiene, así que el aviso nunca se
> enciende.»*

**Consecuencia:** hoy la persona **no ve el aviso del producto** cuando el circuito se corta; ve el
overlay por omisión del marco, y después la recarga. La superficie que el producto diseñó para este
momento exacto está construida, aprobada y **desconectada**. Varias de las alternativas de abajo la
usan, y ésa es una de sus ventajas.

---

## 2. Las alternativas, y qué cuesta cada una

### A · Ampliar la ventana de reconexión

**Qué es.** Subir los reintentos y el intervalo del lado del cliente
(`Blazor.start({ circuit: { reconnectionOptions: … } })`) y la retención del circuito huérfano del
lado del servidor (`CircuitOptions.DisconnectedCircuitRetentionPeriod`, por omisión **3 minutos**).

| | |
|---|---|
| **Qué resuelve** | El caso frecuente y corto: minimizar unos minutos y volver. El circuito **sigue vivo en el servidor** y la pantalla retoma con su estado intacto |
| **Qué NO resuelve** | Minimizar y volver **al rato largo**. Sólo corre el límite; no lo elimina |
| **Costo** | Bajo. Dos configuraciones, sin tocar ninguna pantalla |
| **Impacto adverso** | **Memoria del servidor**: cada circuito huérfano retenido ocupa lugar durante toda la ventana. En un laboratorio con una clase entera minimizando a la vez, eso se multiplica por la cantidad de alumnos |
| **Riesgo** | Bajo, y **medible**: es la única alternativa cuyo costo se puede estimar antes de aplicarla |

### B · Keep-alive más agresivo

**Qué es.** Bajar `HubOptions.KeepAliveInterval` y subir `ClientTimeoutInterval` para que el circuito
no se declare caído.

| | |
|---|---|
| **Qué resuelve** | **Poco, y es la trampa de esta lista.** El *keep-alive* del cliente es un temporizador de JavaScript, y **es exactamente lo que el navegador estrangula** en una pestaña en segundo plano. Parece la corrección directa del síntoma y ataca el extremo que no manda |
| **Costo** | Bajo de escribir, alto de diagnosticar después: deja la sensación de haberlo arreglado |
| **Recomendación** | **No como alternativa principal.** Sólo como acompañamiento de `A` |

### C · Que la reconexión fallida NO recargue

**Qué es.** Reemplazar el manejador de reconexión por omisión para que, agotados los reintentos,
**no llame a la recarga**: en su lugar enciende `SUP-11` y ofrece reintentar a mano.

| | |
|---|---|
| **Qué resuelve** | **El síntoma que se pidió conservar**: la página deja de refrescarse sola. Y **conecta `SUP-11`**, que hoy está construido y muerto |
| **Qué NO resuelve** | **El estado, si el servidor ya descartó el circuito.** La página queda en pantalla pero el circuito está muerto: los botones no responden, y **eso es peor que una recarga honesta si no se avisa**. Por eso `SUP-11` no es un adorno acá: es la condición para que esta alternativa sea aceptable |
| **Costo** | Medio. Un archivo de JavaScript propio, más encender el estado de `SUP-11`, que la etapa `b` dejó declarado como pendiente |
| **Impacto adverso** | **Es probablemente lo que el retoque actual hace a medias.** Si hoy la página no se refresca pero tampoco avisa, la persona está mirando una pantalla que ya no responde y no lo sabe |
| **Combina con** | `A`. Juntas cubren el caso corto sin recarga y el largo con aviso |

### D · Persistir el estado fuera del circuito

**Qué es.** Que lo que la pantalla necesita no viva sólo en `SessionState` (`Scoped`), sino en un
almacén atado a la cookie de sesión, de modo que **un circuito nuevo rehidrate** en vez de empezar de
cero.

| | |
|---|---|
| **Qué resuelve** | **El problema de fondo, y es el único que lo resuelve.** Deja de importar si el circuito se cayó: la recarga recupera la pantalla donde estaba |
| **Qué NO resuelve** | El parpadeo de la recarga. La página **sí** se refresca; lo que no se pierde es el trabajo |
| **Costo** | **Alto, y por pantalla.** Obliga a decidir, en cada una, qué merece sobrevivir. En el visor 3D eso es la pieza reconstruida y la cámara; en un formulario a medio llenar es el borrador |
| **Impacto adverso** | Estado duplicado en dos lugares, que es una fuente de divergencia nueva |
| **Nota** | `SessionTokenStore` ya es `Singleton` y sobrevive: **parte del camino está hecho** |

### E · Sacar esa página del circuito

**Qué es.** Que la pantalla no dependa de un circuito: **render estático** si no necesita
interactividad, o **WebAssembly** si la necesita pero puede resolverla en el navegador.

| | |
|---|---|
| **Qué resuelve** | **Elimina la clase entera de problema.** Sin circuito no hay circuito que se caiga, y minimizar deja de significar nada |
| **Para el visor 3D es la más natural** | La escena ya vive en el navegador —el visor es un paquete de JavaScript de tres capas— y **el circuito no aporta nada mientras se mira una figura** |
| **Costo** | **El más alto.** Cambia una decisión de arquitectura que este producto ya tomó y documentó: la rama `codigo/decision-signalr` y la corrección de la etapa `c`, que pasó **la interactividad a global** para que la credencial viajara en el circuito |
| **Impacto adverso** | Si la página necesita el acceso firmado, sacarla del circuito **obliga a resolver cómo llega la credencial**, que es exactamente lo que aquella decisión vino a simplificar |
| **Cuándo gana** | Si «esa página particular» es de sólo lectura una vez cargada. Si escribe, no |

---

## 3. Recomendación

**`A` + `C`, en ese orden, y `D` sólo para lo que duela perder.**

1. **`A` primero**, porque es barata, es la única con costo estimable y cubre el caso real más
   frecuente —minimizar un rato y volver—, **conservando el estado sin escribir una línea de
   pantalla**.
2. **`C` después**, porque es lo que se pidió conservar —que no se refresque— **y sólo es honesta con
   `SUP-11` encendido**. Sin el aviso, no refrescar es esconder que la pantalla murió.
3. **`D` acotada**, no general: para la pantalla concreta donde perder el trabajo duela, y no como
   política del producto.
4. **`B` no**, salvo como acompañamiento. Ataca el extremo que el navegador estrangula.
5. **`E` sólo si la página es de sólo lectura una vez cargada**, y con la constancia de que reabre
   una decisión de arquitectura ya tomada.

**Lo que hay que confirmar antes de decidir nada:** cuál es la página, cuánto tiempo típico pasa
minimizada y **si escribe o sólo muestra**. Las tres cambian la recomendación, y ninguna sale del
árbol.

---

## 4. Lo que este análisis no sabe

- **Cuál es la página, y si es de este producto.** §0 lo declara.
- **Qué hace el retoque actual.** No está en el árbol. Si sólo suprime la recarga sin avisar, el
  riesgo declarado en `C` **ya está corriendo**.
- **Cuánta memoria cuesta `A` acá.** Depende de cuántos alumnos hay a la vez, que es un dato del
  laboratorio y no del código.
- **Si `StaleFormMiddleware` alcanza al caso.** Traduce el fallo de testigo **tras un reinicio del
  proceso**; si una reconexión larga produce el mismo síntoma, ese intermediario ya lo cubriría, y no
  se verificó.

---

## 5. Control de cambios

| Versión | Fecha | Cambios | Autor |
|---|---|---|---|
| 1.0 | 2026-08-19 | Emisión inicial, **con la premisa declarada como reconstruida y no confirmada** (§0): el retoque que suprime el refresco no está en ningún árbol del workspace, y §0 lista dónde se buscó. Analiza el mecanismo sobre lo que el árbol sí declara de `GeometriaFactory-Web` —interactividad por circuito, sin configuración de reconexión, `SessionState` `Scoped`— y aporta un hallazgo que cambia la elección: **`SUP-11` está construido, aprobado y nunca se enciende**, de modo que hoy la persona no ve el aviso del producto cuando el circuito se corta. Cinco alternativas con su impacto, su costo y lo que **no** resuelven. Recomienda **`A` + `C`** con `D` acotada, descarta `B` como principal por atacar el temporizador que el navegador estrangula, y acota `E` al caso de sólo lectura por reabrir una decisión de arquitectura ya tomada. **Ningún cambio de código aplicado**, por pedido explícito. | Orquestador SDD |
