# Compatibilidad de Plataformas

**Producto:** Fábrica de Geometría
**Documento:** Compatibilidad-Plataformas.md
**Versión:** 1.5
**Estado:** Aprobado
**Fecha:** 2026-08-13
**Autor:** Product Manager Senior (AG-00), actuando también como Analista de Negocio Senior (AG-01) por `Rules-Contexto.md` §1.3
**Trazabilidad upstream:** PRODUCT-INTAKE §10 (restricciones del cliente: red, servidor propio, hosting sin estado, host de desarrollo sin SDK), §13 (los siete proyectos de código), §15 (puertas técnicas y dónde se miden), §17 P.9 de los siete bloques (compatibilidad y plataformas target), §17.5 P.8 y §17.6 P.8 (ambientes y canales de entrega), §22 (incógnitas marcadas para verificar)
**Trazabilidad downstream:** 09-Devops, 05-Arquitectura-Tecnica, 08-Calidad-Y-Pruebas, 03-UX-UI-DX

---

## Tabla de contenido

- [1. Resumen ejecutivo](#1-resumen-ejecutivo)
  - [1.1 Por qué este documento existe pese a no ser obligatorio para el tipo del principal](#11-por-qué-este-documento-existe-pese-a-no-ser-obligatorio-para-el-tipo-del-principal)
  - [1.2 Las tres matrices y cuál rige ante conflicto](#12-las-tres-matrices-y-cuál-rige-ante-conflicto)
- [2. Matriz de compatibilidad](#2-matriz-de-compatibilidad)
  - [2.1 Proyectos de código y sus plataformas target](#21-proyectos-de-código-y-sus-plataformas-target)
  - [2.2 Plataforma del navegador](#22-plataforma-del-navegador)
  - [2.3 Plataformas de construcción](#23-plataformas-de-construcción)
  - [2.4 Resultado medido del transporte de la sesión interactiva (PT-01.b)](#24-resultado-medido-del-transporte-de-la-sesión-interactiva-pt-01b)
  - [2.5 Resultado medido de la estabilidad y la reconexión de la sesión interactiva (PT-01.c)](#25-resultado-medido-de-la-estabilidad-y-la-reconexión-de-la-sesión-interactiva-pt-01c)
  - [2.6 Resultado medido sobre el hosting real (PT-01.a, y la corrección a §2.4)](#26-resultado-medido-sobre-el-hosting-real-pt-01a-y-la-corrección-a-24)
    - [2.6.1 PT-01.c sobre el hosting real: los veinte minutos y el corte de red](#261-pt-01c-sobre-el-hosting-real-los-veinte-minutos-y-el-corte-de-red)
- [3. Restricciones de plataforma justificadas](#3-restricciones-de-plataforma-justificadas)
- [4. Alternativas para plataformas no soportadas](#4-alternativas-para-plataformas-no-soportadas)
- [5. Estado de implementación por plataforma](#5-estado-de-implementación-por-plataforma)
- [6. Trazabilidad downstream](#6-trazabilidad-downstream)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Resumen ejecutivo

Fábrica de Geometría se ejecuta sobre **tres matrices de plataforma que no coinciden entre sí**: los proyectos de código de servidor apuntan a `net10.0` sobre Linux, la pieza pública corre sobre la versión de plataforma que soporte el hosting gratuito —dato que el intake deja explícitamente marcado para verificar— y el navegador debe proveer WebGL y un transporte de sesión interactiva. Este documento las declara juntas y fija cuál rige cuando divergen.

### 1.1 Por qué este documento existe pese a no ser obligatorio para el tipo del principal

`Rules-Contexto.md` §2.1 marca `Compatibilidad-Plataformas.md` como recomendado, y no obligatorio, para los tipos D8 presentes en este producto. Se incluye por **decisión del humano al aprobar el plan de fase**, con este motivo declarado: el producto tiene tres matrices de plataforma que no coinciden entre sí, y una de ellas —la versión de plataforma del hosting— es una incógnita abierta que condiciona el modelo entero de la pieza pública. Dejar esa divergencia sin documento propio la volvería invisible para la categoría 09, que es la que tiene que materializar la matriz de sistema operativo, entorno de ejecución e integración continua.

### 1.2 Las tres matrices y cuál rige ante conflicto

| Matriz | Alcance | Quién la fija |
|---|---|---|
| Servidor de datos | `net10.0` sobre Linux, en contenedor, en el servidor propio | Decisión técnica declarada en el intake |
| Pieza pública | `net10.0` como objetivo, **sujeto a la versión que soporte el hosting gratuito** (marcado para verificar, puerta técnica PT-01.a) | Se resuelve midiendo, no decidiendo |
| Navegador | Requisito declarado **por capacidad y no por número de versión**: WebGL, y WebSockets o repliegue a long polling | El intake declara que las fuentes no fijan versiones mínimas |

**Regla de precedencia ante conflicto, declarada aguas arriba y no decidida acá** (PRODUCT-INTAKE §17.6 P.9): si el hosting no soporta la versión objetivo, se **baja la versión objetivo de la pieza pública, no la del servicio de datos**. Son dos artefactos independientes que no comparten proceso ni servidor, y sólo comparten el ensamblado de contratos, que se compila para el mínimo común. No hay ambigüedad de precedencia: la regla está escrita.

## 2. Matriz de compatibilidad

### 2.1 Proyectos de código y sus plataformas target

Las siete filas cubren los siete proyectos de código del manifiesto, tomadas de PRODUCT-INTAKE §17 P.9 de cada bloque.

| Componente | Entorno de ejecución | Sistema operativo | Navegador | Notas |
|---|---|---|---|---|
| GeometriaFactory-Domain | `net10.0`, sin sufijo de plataforma | Linux | No aplica | Sin dependencias de plataforma. **No** apunta a `net10.0-windows`: eso pertenece a la Actividad 1, que es el emisor del dato y no forma parte del producto |
| GeometriaFactory-Application | `net10.0` | Linux | No aplica | Sin dependencias de plataforma |
| GeometriaFactory-Infrastructure | `net10.0` | Linux, en el entorno de desarrollo y en el servidor propio | No aplica | El motor de base embebido queda anclado en la versión que provee el proveedor de acceso a datos, fijada en la etapa `a` |
| GeometriaFactory-Contracts | `net10.0` | Linux | No aplica | Se carga en los **dos** procesos: el del hosting y el del servidor propio. Es la fila que obliga a que las dos matrices de servidor sean compatibles entre sí |
| GeometriaFactory-Api | `net10.0` | Linux exclusivamente: entorno de desarrollo, imagen de producción y servidor propio | No aplica | La imagen final lleva sólo el entorno de ejecución, sin SDK ni depurador, y no tiene linaje con la imagen del entorno de desarrollo. Un único puerto publicado hacia el enrutador |
| GeometriaFactory-Web | `net10.0` como objetivo, **marcado para verificar** contra el hosting gratuito | El del hosting público gratuito, con servidor de información, HTTPS y dominio | Requerido: WebGL y WebSockets o long polling | Es la única fila con la versión sujeta a verificación. Puerta técnica PT-01.a |
| GeometriaFactory-Visor | Archivo de guion servido como recurso estático; en tiempo de ejecución **no hay entorno Node** | El del navegador | Requerido: WebGL | Node.js en versión de soporte extendido anclada, **sólo en tiempo de construcción** |

### 2.2 Plataforma del navegador

| Capacidad requerida | Umbral | Qué pasa si falta |
|---|---|---|
| WebGL | Presente | Sin WebGL no hay visualización tridimensional: la combinación se considera **no soportada** |
| Transporte de sesión interactiva | WebSockets, o repliegue a long polling | El repliegue es aceptable y se documenta la latencia percibida; **no es motivo de rediseño**. La ausencia de los dos deja el producto sin sesión interactiva |
| HTTPS | Presente | Es el que provee el hosting público; el navegador nunca alcanza al servicio de datos |

**No se fija versión mínima de navegador y es deliberado:** el intake declara que ninguna fuente la fija, y expresa el requisito por capacidad. Enumerar versiones acá sería originar un compromiso de soporte que el Product Owner no tomó.

### 2.3 Plataformas de construcción

| Plataforma | Uso | Restricción declarada |
|---|---|---|
| Entorno de desarrollo contenido | Todo el ciclo de construcción, ejecución y prueba | El host de desarrollo **no tiene ni va a tener** el SDK instalado. Ningún guion puede asumir herramientas del SDK en el host |
| Node.js en versión de soporte extendido, anclada | Construcción del paquete de la visualización | Sólo en tiempo de construcción, y siempre dentro del entorno de desarrollo |
| Integración continua del proveedor de repositorio | Publicación de la pieza pública por transferencia de archivos | Restringida a los cambios de la pieza pública y de la visualización. Termina comprobando que la dirección pública responde, no en la subida |

### 2.4 Resultado medido del transporte de la sesión interactiva (`PT-01.b`)

`Roadmap-Producto.md` §5.2 exige, para pasar de la etapa `a` a la `b`, que **el transporte de la sesión interactiva esté medido y su resultado documentado, incluido el repliegue si ocurre**. Acá está el resultado. **Lo medido es el esqueleto de la etapa `a` corriendo en el entorno de desarrollo contenido, con un navegador real conducido por instrumentación, el 2026-08-13.** No es el hosting público: esa medición dependía de que `PT-01.a` publicara.

> **Corregido el 2026-08-13 por §2.6, y hay que leer las dos secciones juntas.** El front ya se publicó y **el hosting no ofrece WebSockets**: la negociación devuelve sólo `ServerSentEvents` y `LongPolling`. El transporte elegido que esta sección mide es el del **entorno de desarrollo** y **no es el que corre en producción**. Lo que sí se traslada es el repliegue, medido acá y funcionando. La latencia de la última fila **no es extrapolable** a la red real.

| Qué se midió | Resultado medido |
|---|---|
| Transportes que el servidor ofrece al negociar el circuito | **Tres**: `WebSockets`, `ServerSentEvents` y `LongPolling`. El servidor no recorta la oferta |
| Transporte que el navegador elige de verdad | **WebSockets.** El navegador negocia y sube el circuito a WebSocket; el servidor lo registra como `WebSocketsTransport` y **cero** veces como `LongPollingTransport` o `ServerSentEventsTransport` en ese recorrido |
| Qué viaja por el circuito | El evento de interfaz y el repintado: la pulsación del botón de la página de estado llega al servidor como invocación del circuito y la pantalla se actualiza con un dato nuevo del servidor |
| Repliegue cuando el WebSocket **no** está disponible | **Long polling**, y **no** `ServerSentEvents`. Con el túnel WebSocket bloqueado en la ruta del navegador, el cliente vuelve a negociar y sigue por `LongPolling`: **cero** trazas de `WebSocketsTransport` en ese recorrido. La sesión interactiva **sigue funcionando**: la pulsación se procesa y la pantalla se actualiza igual |
| Latencia percibida del repliegue | Medida dentro de la página, del clic hasta el repintado del DOM: **mediana 6 ms por WebSockets y 8 ms por long polling**, siete muestras cada uno. **El número vale para el bucle local y no para la red real**: acá el trayecto de red es despreciable, y long polling paga una petición completa por mensaje, que es donde el costo aparece cuando hay red de por medio |
| Semáforo | **Verde en el entorno de desarrollo contenido; revisado a amarillo en producción por §2.6.** El mejor de los tres estados posibles —WebSockets— es el que ocurre acá, y el estado intermedio —repliegue a long polling— quedó además ejercido y funcionando. El peor estado, «ninguna sesión interactiva», **no se observó**. Nada de esto obliga a cambiar el modelo de la pieza pública |

**Cómo se midió, para que se pueda volver a correr.** Las dos piezas se levantan dentro del entorno de desarrollo contenido; el front escucha en el puerto de desarrollo. La negociación del circuito es lo que el navegador hace antes de abrir el transporte, y se puede pedir a mano:

```bash
curl -sS -i -X POST "http://127.0.0.1:5090/_blazor/negotiate?negotiateVersion=1" -H "Content-Length: 0"
```

```text
HTTP/1.1 200 OK
{"negotiateVersion":1,"connectionId":"...","connectionToken":"...","availableTransports":[
 {"transport":"WebSockets","transferFormats":["Text","Binary"]},
 {"transport":"ServerSentEvents","transferFormats":["Text"]},
 {"transport":"LongPolling","transferFormats":["Text","Binary"]}]}
```

Que el transporte **elegido** sea WebSockets no se deduce de esa oferta: se lee del lado del servidor, subiendo el detalle de registro del transporte del circuito y contando qué transporte se instancia por recorrido.

```text
dbug: Microsoft.AspNetCore.Http.Connections.Internal.HttpConnectionDispatcher[4]
      Establishing new connection.
dbug: Microsoft.AspNetCore.Http.Connections.Internal.Transports.WebSocketsTransport[1]
      Socket opened using Sub-Protocol: '(null)'.
```

**El repliegue se provocó, no se supuso**: el navegador se condujo detrás de un intermediario que registra todo lo que pide y que **rechaza el túnel del WebSocket**, que es exactamente lo que hace una red que no lo deja pasar. Lo que quedó registrado es la secuencia del repliegue —negociar, intentar el túnel, recibir el rechazo, volver a negociar y seguir por long polling—, y del lado del servidor, `LongPollingTransport` y cero `WebSocketsTransport` en ese tramo.

### 2.5 Resultado medido de la estabilidad y la reconexión de la sesión interactiva (`PT-01.c`)

`Roadmap-Producto.md` §5.2 exige, para pasar de la etapa `a` a la `b`, **veinte minutos de navegación continua sin que el proceso recicle la sesión, y reconexión funcional al cortar y restablecer la red**. Son dos cosas y acá están las dos, medidas. **Lo medido es el esqueleto de la etapa `a` corriendo en el entorno de desarrollo contenido, con un navegador real conducido por instrumentación, el 2026-08-13.** **No es el hosting**, y §2.6 agrega que el circuito medido acá va por WebSockets, que **en producción no está disponible**. La salvedad no es formal: el riesgo que `PT-01.c` vigila —que el proceso del hosting gratuito recicle la sesión— **sólo se puede observar en el hosting**. Acá se mide lo que sí se puede medir hoy: que ni el producto ni el entorno contenido reciclan la sesión, y que la reconexión funciona.

| Qué se midió | Resultado medido |
|---|---|
| Duración real de la corrida continua | **1245 s = 20 min 45 s**, de las 09:39:44 a las 10:00:28. No se redondea: la corrida pasó los veinte minutos y por eso se declara el número |
| Interacción durante la corrida | **20 pulsaciones del botón de la página de estado, una por minuto**, sin recargar la página ni tocar la red. **Las 20 produjeron efecto observable**: el momento del servidor cambió en cada una |
| Identidad del circuito | **Un solo circuito para toda la corrida**, `nn5Vcf5NZO8NBrVtevlN69k13-blyi-doR66wTo1b4M`, sobre **una sola conexión**, `R4__pTktCbvIy5xdi29nfA`. **Cero** reconexiones y **cero** circuitos nuevos en el registro del front. La única desconexión aparece en la línea **1422 de 1428**, al cerrar el navegador: **después** de los veinte minutos |
| Que la página nunca se recargó | Marca puesta en la ventana del navegador a los 6 s de cargar, **presente e idéntica al final**; y `performance.now()`, que cuenta desde la **última** carga del documento, terminó en **1 251 023 ms**. Una recarga —que es lo que se ve cuando se pierde la sesión— habría reiniciado las dos cosas |
| Corte de red: qué se cortó | El navegador sale por un reenviador propio. **Se mató ese proceso**: las conexiones abiertas se cayeron y el puerto dejó de existir. La comprobación desde fuera devolvió `[Errno 111] Connection refused` **0,5 s** después. No se cerró la pestaña ni se detuvo el servidor |
| Corte de red: qué avisó el front | El aviso de reconexión de la propia pieza pública, `#components-reconnect-modal`, pasó a `display: block` **0,5 s** después del corte y **siguió en `display: block` en las ocho lecturas que cubren el corte**, desde t+0,5 s hasta el instante mismo del restablecimiento: los **18,1 s** que duró |
| Restablecimiento | Al volver a levantar el reenviador, el aviso volvió a `display: none` **por sí solo a los 3,0 s**, sin recargar la página y sin intervención |
| Que después sigue funcionando | La pulsación **posterior** al restablecimiento cambió el momento del servidor en pantalla de `13:02:50` a `13:03:15`: el evento viajó por el circuito y volvió con dato nuevo |
| Que vuelve **el mismo** circuito, no uno nuevo | El registro del front lo dice con todas las letras: `Attempting to reconnect to Circuit`, `Transferring disconnected circuit … to connection …` y `Reconnect to circuit … succeeded`, **con el mismo identificador de circuito antes y después del corte** y **cero** circuitos creados en ese tramo. El estado del lado del servidor sobrevivió al corte |
| Semáforo | **Verde en el entorno de desarrollo contenido.** Los dos requisitos del criterio se ejercieron y los dos pasaron. Cuando se escribió esta sección **`PT-01.c` no quedaba cerrada**, porque sobre el hosting seguía sin medir y ahí es donde vive el riesgo que el criterio vigila. **Eso se resolvió después, el mismo día: §2.6.1 lo mide sobre el hosting real y ahí está el resultado que cierra la puerta** |

**Cómo se midió, para que se pueda volver a correr.** Las dos piezas se levantan dentro del entorno de desarrollo contenido, el servicio de datos **sólo en el bucle local** y el front en el puerto de desarrollo, que es el único publicado. El front se levanta con el detalle de registro de circuitos subido, que es lo que permite identificar el circuito:

```bash
env 'Logging__LogLevel__Microsoft.AspNetCore.Components=Debug' ./scripts/run-web.sh
```

El navegador **no** habla con el front directamente: sale por un reenviador de una sola línea de vida —acepta en un puerto y reenvía al del front—. Ese proceso **es** el cable de red del navegador: matarlo es cortar, volver a arrancarlo es restablecer. La navegación de veinte minutos y el corte se conducen con instrumentación del navegador, que pulsa el botón y lee el DOM.

La identidad del circuito se lee del lado del servidor, y es la afirmación que sostiene todo el criterio:

```text
dbug: Microsoft.AspNetCore.Components.Server.Circuits.CircuitFactory[1]
      Created circuit nn5Vcf5NZO8NBrVtevlN69k13-blyi-doR66wTo1b4M for connection R4__pTktCbvIy5xdi29nfA
dbug: Microsoft.AspNetCore.Components.Server.Circuits.CircuitRegistry[102]
      Attempting to reconnect to Circuit with secret GoyD7KcHoUPHxBdHLpCso22jE2My5hlJ9HBm8ACcSDI.
dbug: Microsoft.AspNetCore.Components.Server.Circuits.CircuitRegistry[115]
      Reconnect to circuit with id GoyD7KcHoUPHxBdHLpCso22jE2My5hlJ9HBm8ACcSDI succeeded.
```

**Un hallazgo que la medición dejó y que conviene no perder.** En un primer montaje el reenviador tenía un tiempo de espera de 10 s sobre el socket de salida, y eso solo bastaba para que **el WebSocket se cayera cada diez segundos de silencio**. El circuito **sobrevivió igual**: el registro muestra desconexión y reconexión sucesivas **siempre con el mismo identificador de circuito**, y la página nunca se recargó. Es decir: el repliegue ante una red que corta el transporte inactivo **está ejercido, aunque no fuera lo que se buscaba medir**. La corrida de veinte minutos que se declara arriba es la del montaje **corregido**, sin ese defecto, y por eso muestra cero reconexiones.

### 2.6 Resultado medido sobre el hosting real (`PT-01.a`, y la corrección a §2.4)

**El front se publicó al hosting público y está en línea.** Lo que sigue es lo medido **contra el hosting real** el 2026-08-13, no contra el entorno de desarrollo contenido. La dirección pública se nombra porque **es pública**; ningún valor del canal de publicación aparece acá.

| Qué se midió | Resultado medido |
|---|---|
| **`PT-01.a` · la dirección pública responde** | **PASA.** `https://www.aplicada.somee.com/estado` responde **200** |
| **Versión de plataforma del hosting**, la incógnita `[A VERIFICAR]` del intake §17.6.P.9 | **RESUELTA: el hosting soporta `net10.0`.** **No hizo falta bajar la versión objetivo del front**, que es la salida que §17.6.P.10 declaraba para el caso contrario. La restricción `CP-03` de §3 deja de estar pendiente de medición |
| Raíz del sitio | **404.** La única ruta servida en la etapa `a` es la página de estado: hay **una sola** página con ruta declarada. No es un defecto de la publicación |
| `RA-03` en producción | **Se sostiene.** En el HTML público servido por el hosting hay **cero** apariciones de la dirección y del puerto internos |
| Estado degradado | **Se ve correctamente.** El servicio de datos corre en el servidor propio y el front público **no lo alcanza**, y la página de estado dice exactamente eso en lugar de mostrar un dato inventado. Es `US-29` funcionando desde el otro lado de la red |
| **`PT-01.b` sobre el hosting real** | **El hosting NO ofrece WebSockets.** La negociación del circuito devuelve **dos** transportes, `ServerSentEvents` y `LongPolling`. En desarrollo ofrecía los **tres** |

**Cómo se midió, para que se pueda volver a correr.** Las tres comprobaciones son sobre la dirección pública y no necesitan ningún secreto:

```bash
curl -sS -o /dev/null -w "%{http_code}\n" https://www.aplicada.somee.com/estado
curl -sS -o /dev/null -w "%{http_code}\n" https://www.aplicada.somee.com/
curl -sS -X POST "https://www.aplicada.somee.com/_blazor/negotiate?negotiateVersion=1" -H "Content-Length: 0"
```

```text
200
404
{"negotiateVersion":1,"connectionId":"...","connectionToken":"...","availableTransports":[
 {"transport":"ServerSentEvents","transferFormats":["Text"]},
 {"transport":"LongPolling","transferFormats":["Text","Binary"]}]}
```

#### La corrección a §2.4, escrita como corrección y no como matiz

**§2.4 midió en desarrollo que el transporte elegido era WebSockets y declaró semáforo verde. Sobre el hosting real, ese resultado no se sostiene: WebSockets no está en la oferta.** No es que el navegador prefiera otro: **el servidor del hosting no lo ofrece**, de modo que la sesión interactiva del producto **no va a usar WebSockets en producción**. Lo que §2.4 midió sigue siendo cierto de lo que midió —el entorno de desarrollo contenido—; lo que dejaba de sobra por bueno era la extrapolación al destino real.

**Qué de §2.4 sí sobrevive, y es lo que evita que esto sea un problema.** El repliegue **ya estaba medido y funcionando**: con el túnel del WebSocket bloqueado, el circuito repliega a long polling y **la sesión interactiva sigue funcionando** —la pulsación se procesa, la pantalla se actualiza—, y §2.5 agrega que los veinte minutos y la reconexión también se sostienen en ese modo. Es exactamente el escenario que §4 tenía declarado como **aceptable y no motivo de rediseño**, sólo que ahora es **el escenario real y no una contingencia**.

**Qué de §2.4 deja de ser aplicable, y hay que decirlo explícito.** La latencia percibida —mediana **6 ms** por WebSockets contra **8 ms** por long polling— **se midió en un bucle local y no es extrapolable a la red real**. §2.4 ya lo declaraba y acá se ratifica con más razón: en producción hay red de por medio, y long polling **paga una petición completa por mensaje**, que es justamente donde el costo aparece. **Los 8 ms no son la latencia de producción y no se los puede citar como tal.** La latencia percibida sobre el hosting real **no está medida**.

**Semáforo revisado.** **Amarillo, y estable.** El mejor de los tres estados posibles —WebSockets— **no ocurre en producción y no puede ocurrir**; el estado intermedio —repliegue a long polling— es el que ocurre, y está **ejercido y funcionando**. El peor estado, «ninguna sesión interactiva», **no se observó**. **Nada de esto obliga a cambiar el modelo de la pieza pública**, que es lo que el intake §17.6.P.10 fija como único disparador de rediseño.

#### 2.6.1 `PT-01.c` sobre el hosting real: los veinte minutos y el corte de red

Hasta la versión anterior de este documento, `PT-01.c` estaba medida **sólo en el entorno de desarrollo contenido** (§2.5) y se declaraba explícitamente sin medir sobre el hosting, que es donde vive el riesgo que el criterio vigila: **que el proceso del hosting gratuito recicle la sesión**. **Acá está medida sobre el hosting**, el 2026-08-13, contra `https://www.aplicada.somee.com/estado`, con navegador real conducido por instrumentación y con el servicio de datos alcanzable, de modo que la página mostraba **datos reales del servidor** durante toda la corrida.

**El método, y por qué concluye.** Sobre el hosting **no hay acceso a los registros del servidor**, así que la identidad del circuito —que en §2.5 se leyó del lado del servidor— **no se puede leer**. Hubo que encontrar la forma de observar el reciclado **desde el navegador**, y se combinaron **tres observables que se sostienen entre sí**:

1. **La identidad de la conexión, leída del propio tráfico del navegador.** Un circuito de Blazor Server está atado **uno a uno** a una conexión SignalR, y **toda** petición del circuito lleva su identificador de conexión en la consulta (`/_blazor?id=…`). Si el circuito se cae y se rearma —lo reanude el servidor o lo cree de nuevo— **hace falta una conexión nueva**, y una conexión nueva obliga a un `POST /_blazor/negotiate` nuevo y a un identificador nuevo. Ese tráfico se lee **sin tocar la página**, con la API estándar **Resource Timing** del navegador (`performance.getEntriesByType('resource')`), drenada cada 5 s para que el búfer no se llene. **Un solo identificador durante toda la corrida es la prueba de que la conexión nunca se rehizo, y por lo tanto de que el circuito nunca se cayó ni se recicló.** Y cubre también el reciclado del **proceso**: si el proceso del hosting se reiniciara, las conexiones abiertas se caerían y el navegador tendría que renegociar, cosa que se vería como identificador nuevo.
2. **El aviso de reconexión de la propia pieza pública** (`#components-reconnect-modal`), leído periódicamente —cada 5 s en la corrida larga, cada 2 s durante el corte—. Es lo que **vería la persona** si el circuito se cayera. El elemento **no existe en el documento hasta que hay una desconexión**: que no aparezca en toda la corrida es, por sí mismo, que el navegador nunca perdió el circuito.
3. **Marca en `window` y `performance.now()`**, que detectan **cualquier recarga del documento**. Importa porque la pérdida de la sesión, del lado de la persona, se ve como una página que se recarga y pierde su estado: una recarga reinicia las dos cosas.

**Control del instrumento, para que «no se vio nada» no se confunda con «no mide nada».** El observable 2 **está probado que dispara**: en el corte de red de más abajo el aviso apareció a los **2,0 s** y se mantuvo los **24 s** que duró el corte. El observable 1 registró **116** peticiones del circuito: no es un instrumento mudo.

**Lo medido, corrida larga.**

| Qué se midió | Resultado medido |
|---|---|
| Duración real de la corrida continua | **1206 s = 20 min 6 s**, de las 21:53:42 a las 22:13:48 de hora local. No se redondea |
| Interacción durante la corrida | **20 pulsaciones del botón de la página de estado, una por minuto**, sin recargar la página ni tocar la red. **Las 20 produjeron efecto observable**: el momento del servidor cambió en cada una. El de la primera lectura fue `2026-08-14T00:53:31Z` y el de la última `2026-08-14T01:13:45Z` |
| Identidad de la conexión, y con ella la del circuito | **Un solo identificador de conexión para toda la corrida**, `SsknfJRynM5Al5suRRx3hw`, con **116** peticiones observadas. **Cero** conexiones nuevas y **cero** negociaciones nuevas después del arranque: la única negociación es la del propio arranque de la página. **El circuito nunca se rearmó, y el proceso del hosting nunca recicló la sesión durante los veinte minutos** |
| Aviso de reconexión | **Nunca apareció.** El elemento no llegó a existir en el documento en ningún momento de la corrida, ni queda en el documento final |
| Que la página nunca se recargó | Marca puesta en la ventana **presente e idéntica al final**, y `performance.now()` —que cuenta desde la **última** carga del documento— terminó en **1 220 953 ms**. Además, el descriptor de prerenderizado del documento es **idéntico** al del inicio: la única diferencia entre el documento del principio y el del final son **el momento del servidor y la hora de lectura**, o sea el efecto de las pulsaciones sobre el mismo documento vivo |
| Semáforo de esta mitad | **Verde sobre el hosting real.** El peor escenario —que el hosting recicle la sesión antes de los veinte minutos— **no se observó** |

**Lo medido, corte y restablecimiento de la red, también sobre el hosting.** El navegador sale por un **cable propio**: un intermediario local de una sola línea de vida por el que pasan todas sus conexiones. **Matar ese proceso es cortarle la red**; volver a arrancarlo es restablecerla. Es el mismo montaje de §2.5, adaptado a un destino público.

| Qué se midió | Resultado medido |
|---|---|
| Qué se cortó, y control de que se cortó de veras | Se mató el proceso del cable. La comprobación inmediata desde fuera todavía dio `ABIERTO` **en el instante 0,0 s** —el proceso aún no había terminado de morir— y a partir de **0,5 s** dio `[Errno 111] Connection refused`, comprobado aparte con el mismo procedimiento. La prueba de que el navegador se quedó sin red es la de la fila siguiente |
| Qué avisó el front | El aviso de reconexión de la propia pieza pública pasó a `display: block` a los **2,0 s** del corte y **siguió en `block` en las doce lecturas** que cubren el corte, de t+2,0 s a t+24,1 s |
| Cuánto duró el corte | **26,1 s** |
| Restablecimiento | Al volver a levantar el cable, el aviso volvió a `display: none` **por sí solo a los 7,0 s**, sin recargar la página y sin intervención |
| Que después sigue funcionando | La pulsación **posterior** al restablecimiento cambió el momento del servidor de `2026-08-14T01:16:10Z` a `2026-08-14T01:16:49Z`: el evento viajó por el circuito y volvió con dato nuevo |
| Que es **el mismo** circuito | La marca de la ventana quedó **idéntica** y `performance.now()` siguió corriendo hasta **58 242 ms**: **la página no se recargó**. Y sin una carga nueva del documento **Blazor Server no puede crear un circuito nuevo**: el estado del lado del servidor sobrevivió al corte y la reconexión lo recuperó. **El identificador de circuito, a diferencia de §2.5, no se puede leer desde el cliente**, y no se afirma haberlo leído: lo que se afirma es lo que se observó |

**Cómo se midió, para que se pueda volver a correr.** Todo pasa contra la dirección pública y **no hace falta ningún secreto**. Primero se comprueba el terreno; el resultado de la negociación es el mismo de §2.6, o sea que **lo que sigue se midió sobre un circuito sin WebSockets**, que era el motivo extra para medirlo acá:

```bash
curl -sS -o /dev/null -w "%{http_code}\n" https://www.aplicada.somee.com/estado
curl -sS -X POST "https://www.aplicada.somee.com/_blazor/negotiate?negotiateVersion=1" -H "Content-Length: 0" \
  | python3 -c "import json,sys; print([t['transport'] for t in json.load(sys.stdin)['availableTransports']])"
```

```text
200
['ServerSentEvents', 'LongPolling']
```

La corrida y el corte se conducen con un navegador real por instrumentación, que pulsa el botón, lee el documento y drena el registro de recursos. El observable decisivo se lee así, y es una sola línea:

```js
performance.getEntriesByType('resource')
  .map(e => e.name).filter(n => n.includes('/_blazor'))
```

**Qué queda declarado, y qué no.** Lo que se ejerció es el criterio **tal como está escrito**: *veinte minutos de **navegación continua***, con interacción cada minuto. **No se midió el reciclado por inactividad**, que es el otro motivo por el que un hosting gratuito recicla un proceso, y este documento **no afirma nada sobre él**. Tampoco se midió la **latencia percibida** en producción, que sigue sin medir desde §2.6. Y la corrida es **una**: mide que el hosting **no recicló la sesión en esos veinte minutos**, no que no lo haga nunca.


## 3. Restricciones de plataforma justificadas

| Id | Restricción | Justificación declarada |
|---|---|---|
| CP-01 | Linux exclusivamente en los seis proyectos de código de servidor | Entorno de desarrollo, imagen de producción y servidor propio son Linux. Toda combinación no listada se considera no soportada |
| CP-02 | Ningún proyecto de código apunta a un objetivo específico de Windows | El único artefacto que lo usa es la Actividad 1, que emite el dato y **no forma parte de este producto** |
| CP-03 | La versión de plataforma de la pieza pública está sujeta a lo que soporte el hosting | El hosting es gratuito y su capacidad no es contrastable sin medirla. Es la puerta técnica PT-01.a, y se mide en la etapa `a` antes que cualquier otra cosa. **Medida el 2026-08-13: el hosting soporta `net10.0` y la versión objetivo del front no se bajó** (§2.6). La restricción sigue vigente como restricción —la versión la elige el panel de la cuenta del hosting—, pero **deja de estar pendiente de medición** |
| CP-04 | Requisito de navegador expresado por capacidad y no por versión | Ninguna fuente fija versiones mínimas; sin WebGL no hay visualización |
| CP-05 | El motor gráfico viaja dentro del paquete, no desde una red de distribución externa | La pieza pública debe funcionar sin acceso a redes de distribución de contenido externas. Es la puerta técnica PT-03 |
| CP-06 | En tiempo de ejecución no hay entorno Node | La visualización se sirve como archivo estático. Node existe sólo en tiempo de construcción |
| CP-07 | El host de desarrollo no tiene el SDK y no lo va a tener | Decisión declarada del propietario del entorno. Obliga a que todo el ciclo ocurra dentro del entorno contenido |
| CP-08 | La versión de todo paquete se ancla explícitamente y se registra en la etapa que lo introduce | Regla de anclaje de versiones declarada para los siete proyectos de código: un cambio de versión mayor es una decisión que se documenta, nunca el efecto colateral de una actualización |
| CP-09 | El servidor propio no tiene dirección fija y se admite apuntar a la dirección directa | Decisión declarada del propietario del servidor, con nombre dinámico como recomendación. Cada cambio de dirección obliga a volver a publicar la pieza pública |

## 4. Alternativas para plataformas no soportadas

| Plataforma o escenario no soportado | Alternativa declarada | Origen |
|---|---|---|
| El hosting no soporta la versión de plataforma objetivo | Bajar la versión objetivo **de la pieza pública**, no la del servicio de datos. **No se ejerció**: el hosting soporta `net10.0` (§2.6) | PT-01.a |
| El hosting no sostiene el transporte de sesión interactiva por WebSockets | **Es el caso real, no una contingencia** (§2.6): el hosting **no ofrece WebSockets**, sólo `ServerSentEvents` y `LongPolling`. La alternativa declarada —repliegue a long polling— **es lo que corre en producción**, y es **aceptable**: no es motivo de rediseño. Está **ejercida y funcionando** desde §2.4. **La latencia percibida sobre el hosting real sigue sin medir**, y la de §2.4 es de bucle local y no se puede citar como la de producción | PT-01.b |
| El hosting no sostiene ninguna sesión interactiva, o recicla el proceso | Es el peor escenario y **no tiene mitigación en el código**. Salidas documentadas: cambiar el modelo de la pieza pública a ejecución en el navegador con reenvío de peticiones, o servir la pieza pública desde el servidor propio, que reabre el bloqueo desde la facultad. **El escenario no se observó, ni en el entorno de desarrollo (§2.5) ni sobre el hosting real (§2.6.1)**: veinte minutos de navegación continua sobre la dirección pública con **una sola conexión** y **cero** rearmes del circuito, y la reconexión al cortar la red **ejercida y funcionando** en los dos entornos. **`PT-01.c` queda medida donde vive el riesgo.** Lo que sigue sin medir es el reciclado **por inactividad**, que la corrida no ejerce | PT-01.c y las salidas documentadas del intake |
| La pieza pública no alcanza al servicio de datos | Publicar el servicio en un puerto convencional. El reenvío de peticiones **no ayuda** en este caso | PT-01.d |
| Navegador sin WebGL | **No soportado.** No hay alternativa: sin WebGL no hay visualización, que es una de las capacidades comprometidas | PRODUCT-INTAKE §17.6 P.9 y §17.7 P.9 |
| Red de la facultad que bloquea el acceso directo al servidor propio | Es la premisa que ordena la partición del producto, no un escenario a mitigar. La salida alternativa —canal saliente con dominio propio— está **declarada y deliberadamente no adoptada**, porque debilitaría la premisa | Exclusión X-10 del alcance |
| Host de desarrollo sin SDK | Entorno de desarrollo contenido, obligatorio para todo el ciclo | CP-07 |

## 5. Estado de implementación por plataforma

Estado a la fecha de emisión 1.0: el producto no tiene código construido. La evidencia es `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` §1.1, bloque de decisiones de reconciliación, que declara que «`SDD/Docs/` estaba vacía al arrancar, de modo que la reconciliación normativa de `Master-Prompt.md` §2.1 no se disparó». Todas las filas estaban **declaradas y sin verificar**, y cada una indica dónde se verifica. **Desde 1.2 eso ya no vale para todas**: la etapa `a` construyó el esqueleto y dos filas cambiaron de estado, con la precisión de que lo medido es el **entorno de desarrollo contenido** y no el hosting público.

| Plataforma | Componentes que la usan | Estado | Dónde se verifica |
|---|---|---|---|
| `net10.0` sobre Linux, servidor propio | Api, Infrastructure, Application, Domain, Contracts | **Verificada en el entorno de desarrollo**: el producto compila entero y las dos piezas arrancan; PT-04 quedó medido con la imagen construida y arrancada | Etapa `a`: el producto compila y las dos piezas desplegables arrancan; PT-04 construye y arranca la imagen |
| Plataforma del hosting público | Web, Contracts | **Verificada sobre el hosting real el 2026-08-13: soporta `net10.0`.** La marca `[A VERIFICAR]` queda **resuelta** y la versión objetivo del front **no se bajó**. `PT-01.a` **pasa**: la ruta pública responde **200** | Etapa `a`, PT-01.a, **medida**. Evidencia en §2.6 |
| Transporte de sesión interactiva del hosting | Web | **Verificada sobre el hosting real el 2026-08-13: el hosting NO ofrece WebSockets.** La negociación devuelve sólo `ServerSentEvents` y `LongPolling`, de modo que **en producción la sesión interactiva va por long polling**. El repliegue está **ejercido y funcionando** desde §2.4, así que el escenario es el declarado aceptable y no obliga a rediseño. **La latencia percibida en producción sigue sin medir** | Etapa `a`, PT-01.b, **medida sobre la dirección pública** con la negociación del circuito. Evidencia y comandos en §2.6 |
| Estabilidad y reconexión de la sesión interactiva | Web | **Verificada sobre el hosting real el 2026-08-13: 20 min 6 s de navegación continua sobre una única conexión del circuito, con 20 interacciones y todas con efecto observable, cero rearmes y cero recargas; y reconexión automática tras un corte de red de 26,1 s, sin recargar la página y con la interacción posterior con efecto** (§2.6.1). En el entorno de desarrollo se había medido lo mismo antes, sobre WebSockets (§2.5); esta medición es sobre el circuito **sin WebSockets** que es el de producción | Etapa `a`, PT-01.c, **medida sobre la dirección pública**. Evidencia, método y comandos en §2.6.1. **Sigue sin medir el reciclado por inactividad** |
| Salida del hosting hacia el servidor propio | Web hacia Api | Declarada, sin verificar | Etapa `a`, PT-01.d |
| Navegador con WebGL | Web, Visor | Declarada, sin verificar | Etapa `g`, PT-02 y PT-03 |
| Node.js de construcción | Visor | Declarada, sin verificar | Etapa `a`, al generar el paquete por primera vez |
| Red de la facultad hacia el hosting | Producto completo | Declarada, sin verificar | Etapa `i`, PT-05, con el despliegue real. El intake recomienda no relegarla. La letra corrió de `h` a `i` el 2026-08-08 al insertarse el circuito de revisión como etapa `h`; la puerta sigue atada al despliegue real |

## 6. Trazabilidad downstream

| Contenido | Destino | Qué consume |
|---|---|---|
| §2 Matriz de compatibilidad | 09-Devops | Matriz de sistema operativo, entorno de ejecución e integración continua; imágenes base y objetivos de construcción |
| §2.3 Plataformas de construcción | 09-Devops | Definición del entorno de desarrollo contenido y de los guiones de construcción |
| §2.6 Resultado sobre el hosting real | 09-Devops, 05-Arquitectura-Tecnica | El mecanismo de publicación comprobado y el transporte disponible en producción. Lo consume [`../Proyectos/GeometriaFactory-Web/09-Devops/Guia-Publicacion-Front-Ftp.md`](../Proyectos/GeometriaFactory-Web/09-Devops/Guia-Publicacion-Front-Ftp.md) §2.1 |
| §3 Restricciones justificadas | 05-Arquitectura-Tecnica | Insumo de las decisiones de arquitectura sobre la partición del producto y sobre el aislamiento de la visualización |
| §4 Alternativas | 05-Arquitectura-Tecnica, 09-Devops | Salidas ya documentadas ante el resultado de cada puerta técnica |
| §5 Estado por plataforma | 08-Calidad-Y-Pruebas | Qué se mide, dónde y con qué umbral |
| §2.2 Plataforma del navegador | 03-UX-UI-DX | Capacidades que la experiencia de uso puede dar por presentes |

## 7. Control de cambios

| Versión | Fecha | Cambios | Autor |
|---|---|---|---|
| 1.0 | 2026-08-08 | Emisión inicial, incluida por decisión del humano al aprobar el plan de fase pese a no ser obligatoria para el tipo D8 del proyecto de código principal, con su motivo declarado en §1.1. Agrega las plataformas target de los siete proyectos de código, la matriz del navegador expresada por capacidad, las tres plataformas de construcción, nueve restricciones justificadas, siete alternativas para escenarios no soportados y el estado de verificación de cada plataforma con la puerta técnica que la mide. | Product Manager Senior (AG-00) |
| 1.0 | 2026-08-08 | Correcciones absorbidas del audit A-00-01-r1, sin subir versión por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **H-05**: el párrafo introductorio de §5 cita `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` §1.1, decisiones de reconciliación, como evidencia localizable de la única afirmación del documento sobre el estado del sistema. **H-01**: se califica la ocurrencia desnuda de «pieza» de la primera fila de §5, sobre la familia que declara `Vision-Producto.md` §9.2. | Product Manager Senior (AG-00) |
| 1.1 | 2026-08-08 | Absorbe la renumeración de etapas que trae `PRODUCT-INTAKE` 1.3 al insertar el circuito de revisión del administrador como etapa `h`. Sube minor y archiva el estado anterior porque el documento ya es citado como insumo por otras categorías (`Master-Prompt.md` §5). **§5**: la verificación de PT-05 pasa de la etapa `h` a la etapa `i`, con la nota de que la puerta sigue atada al despliegue real y no a la letra. Es el único cambio: **ninguna plataforma, versión, restricción ni alternativa se modifica**, porque el circuito de revisión no toca la matriz de plataformas. | Product Manager Senior (AG-00) |
| 1.2 | 2026-08-13 | **Documenta el resultado medido de la puerta técnica PT-01.b**, que `Roadmap-Producto.md` §5.2 exige medido y documentado —repliegue incluido— para pasar de la etapa `a` a la `b`. Agrega **§2.4** con el resultado y su evidencia: el servidor ofrece los **tres** transportes al negociar el circuito, el navegador real elige **WebSockets**, y con el túnel del WebSocket bloqueado el circuito **repliega a long polling —no a `ServerSentEvents`— y la sesión interactiva sigue funcionando**; semáforo **verde**. Incluye la latencia percibida de los dos transportes con su límite declarado: es un bucle local y no la red real. **§4** marca el repliegue como ejercido, y **§5** cambia el estado de **dos** filas —el transporte de la sesión interactiva y la plataforma de servidor— de «declarada, sin verificar» a medida **en el entorno de desarrollo contenido**, dejando escrito que sobre el hosting público sigue sin verificar y con qué comando se verificaría. **Ninguna plataforma, restricción ni alternativa se modifica**: sólo se registra qué se midió y qué falta. Sube minor y no mayor porque no cambia ninguna decisión de plataforma. | Product Manager Senior (AG-00) |
| 1.3 | 2026-08-13 | **Documenta el resultado medido de la puerta técnica PT-01.c**, que `Roadmap-Producto.md` §5.2 exige en sus **dos** mitades para pasar de la etapa `a` a la `b`. Agrega **§2.5** con las dos y su evidencia: **20 min 45 s de navegación continua** —el número real, sin redondear— con **20 interacciones, todas con efecto observable**, sobre **un único circuito** identificado por su identificador del lado del servidor, **cero** reconexiones y **cero** circuitos nuevos, y con la prueba de que la página **nunca se recargó**; y el **corte de red real** —matar el proceso por el que sale el navegador, con `Connection refused` comprobado— con el **aviso de reconexión de la propia pieza pública visible durante todo el corte**, la vuelta **automática** a los 3,0 s de restablecer, la interacción posterior **con efecto observable** y la reconexión **al mismo circuito**, no a uno nuevo. Deja escrito el hallazgo lateral: una red que corta el transporte inactivo cada diez segundos **no recicla la sesión**, el circuito sobrevive. **§4** marca que el peor escenario no se observó y **§5** agrega la fila de estabilidad y reconexión, medida en el entorno de desarrollo contenido. **`PT-01.c` NO queda cerrada**: sobre el hosting sigue sin medir, y ahí es donde vive el riesgo que el criterio vigila —que el proceso recicle la sesión—, cosa que sólo se puede observar en el hosting. **Ninguna plataforma, restricción ni alternativa se modifica**: sólo se registra qué se midió y qué falta. Sube minor y no mayor porque no cambia ninguna decisión de plataforma. | Product Manager Senior (AG-00) |
| 1.5 | 2026-08-13 | **Cierra `PT-01.c` midiéndola sobre el hosting real, que es lo que 1.3 y 1.4 declaraban pendiente y donde vive el riesgo que el criterio vigila.** Agrega **§2.6.1** con las **dos mitades** medidas contra `https://www.aplicada.somee.com/estado`: **20 min 6 s de navegación continua** —el número real, sin redondear— con **20 interacciones y las 20 con efecto observable**, sobre **una única conexión del circuito**, `SsknfJRynM5Al5suRRx3hw`, con **116** peticiones, **cero** negociaciones nuevas, **cero** rearmes y **cero** recargas de la página; y el **corte de red real** de **26,1 s** con el aviso de reconexión de la propia pieza pública visible de t+2,0 s a t+24,1 s, la vuelta **automática** a los **7,0 s** de restablecer, sin recargar, y la pulsación posterior **con efecto observable**. **Declara el método elegido para detectar el reciclado desde el cliente y por qué concluye**, que es la parte nueva: sobre el hosting **no hay registros del servidor**, así que la identidad del circuito se infiere de la **identidad de la conexión SignalR** leída del propio tráfico con la API estándar Resource Timing —un circuito está atado uno a uno a una conexión, y rearmarlo obliga a negociar de nuevo—, apoyada en el **aviso de reconexión** del front y en la **marca de ventana con `performance.now()`** que detecta cualquier recarga; con **control del instrumento**, porque el aviso sí disparó en el corte. **§4** deja el peor escenario declarado **no observado también sobre el hosting** y **§5** pasa la fila de estabilidad y reconexión a **verificada sobre el hosting real**. **Declara explícitamente lo que sigue sin medir**: el reciclado **por inactividad**, que la corrida no ejerce, y la **latencia percibida en producción**. **Ninguna plataforma, restricción ni alternativa se modifica**: sólo se registra qué se midió y qué falta. Sube minor. | Product Manager Senior (AG-00) |
| 1.4 | 2026-08-13 | **Documenta el resultado medido sobre el hosting real, y corrige lo que la medición de desarrollo daba por bueno.** Agrega **§2.6** con las seis mediciones sobre la dirección pública y sus comandos: **`PT-01.a` PASA** —la ruta que la etapa sirve responde **200**—, la raíz responde **404** porque en la etapa `a` hay una sola ruta servida, **`RA-03` se sostiene en producción** con cero apariciones de la dirección y del puerto internos en el HTML público, y el **estado degradado se ve correctamente** con el servicio de datos fuera del alcance del front público. **Marca como RESUELTA la incógnita `[A VERIFICAR]` del intake §17.6.P.9: el hosting soporta `net10.0`** y **no hizo falta bajar la versión objetivo del front**; `CP-03` de §3 deja de estar pendiente de medición y la primera alternativa de §4 queda declarada **no ejercida**. **Y corrige §2.4, como corrección y no como matiz: el hosting NO ofrece WebSockets** —la negociación devuelve sólo `ServerSentEvents` y `LongPolling`, contra los tres de desarrollo—, de modo que **la sesión interactiva del producto no usa WebSockets en producción**; el semáforo de `PT-01.b` se revisa de **verde** a **amarillo estable**, con el repliegue a long polling —ya ejercido y funcionando en §2.4— como **escenario real y no contingencia**, que es el que §4 tenía declarado aceptable y **no motivo de rediseño**. Declara explícitamente que **la latencia percibida de §2.4 es de bucle local y no es extrapolable**: la de producción **no está medida**. **`PT-01.c` sigue sin medir sobre el hosting**, ahora con un motivo más: lo medido fue sobre un circuito por WebSockets. **Ninguna plataforma ni decisión de plataforma se modifica.** Sube minor. | Product Manager Senior (AG-00) |
