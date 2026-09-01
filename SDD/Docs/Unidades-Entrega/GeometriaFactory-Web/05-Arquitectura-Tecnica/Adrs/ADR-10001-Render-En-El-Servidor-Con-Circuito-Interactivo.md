# ADR-10001 — Render en el servidor con circuito interactivo, y una sola salida hacia el servicio de datos

**Unidad de entrega:** GeometriaFactory-Web
**Documento:** ADR-10001-Render-En-El-Servidor-Con-Circuito-Interactivo.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior (AG-05)
**Categoría:** Estilo

---

## 1. Contexto

La composición del producto responde a una restricción externa y no a una preferencia de estilo. El servidor propio **no tiene dirección estática** y la red de la facultad bloquea el acceso a direcciones dinámicas; el hosting gratuito, en cambio, tiene dominio público y transporte seguro pero **resetea el estado persistente**. De ahí la partición: el front vive donde no lo bloquean y los datos viven donde persisten.

Esa partición produce una consecuencia que gobierna a este proyecto de código: si el navegador llamara al servicio de datos, harían falta tres cosas que no existen —transporte seguro válido en un servidor de dirección dinámica, permiso de origen cruzado, y exponer la dirección del servidor propio al navegador—. Es lo que `RA-01` prohíbe, y es la regla que sostiene las tres propiedades de la topología: **romperla en un solo lugar las reabre las tres**.

Este proyecto de código es el único del producto que puede violar `RA-01`, porque es el único que sirve al navegador.

Motivación upstream: NB-00008; `RA-01`, `RA-03`; `PRODUCT-INTAKE` §14, §17.2.P.2 · GeometriaFactory-Web, §17.2.P.3 · GeometriaFactory-Web, §17.2.P.10 · GeometriaFactory-Web (`PT-01.a`, `PT-01.b`), §17.2.P.11 · GeometriaFactory-Web punto 1 y §17.2.P.12 · GeometriaFactory-Web; restricción transversal `RT-01` de la categoría 02.

## 2. Decisión

**La aplicación se renderiza en el servidor y la interacción viaja por un circuito**; el navegador no ejecuta lógica de la aplicación. Lo único que corre en el navegador es el dibujo del visor, que no hace red.

**Toda llamada al servicio de datos sale del servidor de esta pieza**, por un **único** componente —el cliente tipado—, en modo petición-respuesta y con la credencial adjunta del lado del servidor. En consecuencia:

- **No se agregan bibliotecas de guion que consulten servicios por su cuenta.**
- **Ninguna validación consulta al servidor mientras la persona escribe.**
- **No hay actualización parcial iniciada por el servicio de datos, ni sondeo de estado desde el navegador.**

El repliegue del transporte a uno de mayor latencia es **aceptable y no se anuncia a la persona**: es un trade-off aceptado aguas arriba y no es una degradación del laboratorio. **Sólo la ausencia total de circuito obliga a cambiar el modelo de front.**

### 2.1 El reparto de modos de render, que entra a la decisión el 2026-08-31

**«La interacción viaja por un circuito» describía a las once pantallas de marcador de posición de la
etapa `b`, y dejó de describir al producto en la etapa `c`.** Al construirles comportamiento, la mayoría
de las superficies pasó a **render estático de servidor** con formularios que hacen `POST` de verdad, y
cada una dejó su motivo escrito en la cabecera de su componente. **Lo que faltaba no era el criterio:
faltaba que estuviera acá, en la capa que decide.** Es `MI-06` de la mesa del 2026-08-31.

**El reparto real, contado sobre el árbol el 2026-08-31** —`grep -rln '^@rendermode' Components/Pages/`—:
**quince rutas sobre catorce componentes**, de los cuales **seis son interactivos y ocho estáticos**.

| Superficie | Ruta | Modo | Por qué |
| --- | --- | --- | --- |
| `InitialDestination` | `/` | **Interactiva** | Decide destino según la sesión y redirige sin intervención de la persona |
| `InitialProvisioning` | `/aprovisionamiento-inicial` | **Interactiva** | Sondea la existencia del administrador y cambia de estado sola |
| `OwnCredentialSetup` | `/credencial-propia/establecer` | **Interactiva** | Valida los requisitos de la contraseña **mientras se escribe**, sin consultar al servidor |
| `OwnCredentialChange` | `/mi-contrasena` | **Interactiva** | Ídem |
| `Status` | `/estado` | **Interactiva** | Es la pantalla que **muestra** el estado del circuito; sin circuito no tendría qué mostrar |
| `NotFoundPage` | `/no-encontrado` | **Interactiva** | Heredada de la etapa `b`; **no tiene motivo propio y es la única sin justificación en su cabecera** |
| `SignIn` | `/ingreso` | Estática | `POST` con antifalsificación; la credencial no debe vivir en un circuito |
| `AccountRegistration` | `/registro-de-cuenta` | Estática | Apartamiento declarado en su cabecera: pasó de interactiva a estática al construirle comportamiento |
| `OwnCredentialForcedChange` | `/credencial-propia/cambio-obligado` | Estática | Mismo patrón que `SignIn` |
| `ClassSubmissionList` | `/entrega-comision` | Estática | El filtro viaja por la dirección: es un `<form method="get">` |
| `StudentWorkPanel` | `/mis-trabajos` | Estática | Ídem |
| `AccountsPanel` | `/cuentas` | Estática | Ídem, con `POST` por operación |
| `WorkSubmission` | `/trabajo-nuevo` y `/editar` | Estática | `POST` del texto pegado |
| `WorkView` | `/trabajos/{WorkId}` | Estática | El visor dibuja en el navegador y **no usa el circuito** (`RA-02`) |

**La convención, que es la parte vinculante de este apartado.** **Una superficie nueva es ESTÁTICA salvo
que declare en su cabecera por qué no puede serlo.** El motivo se escribe en el componente, no acá: esta
ADR gobierna el criterio y no la lista, que envejece. Las tres razones admitidas hoy son las que las seis
interactivas ejercen: **decidir y redirigir sin intervención**, **validar mientras se escribe sin
consultar al servidor**, y **mostrar el estado del propio circuito**.

**`NotFoundPage` queda señalada y no se toca.** Es interactiva sin motivo declarado —residuo de la etapa
`b`— y volverla estática es un cambio de código que esta ADR no ordena. Queda como deuda con disparador
en [`../../../../Audit/Decisiones-Y-Frases-Retiradas.md`](../../../../Audit/Decisiones-Y-Frases-Retiradas.md) §3.

**Y esto NO relaja `RA-01`.** Ninguna superficie, interactiva o estática, llama al servicio de datos
desde el navegador. El reparto es sobre **dónde se procesa la interacción**, no sobre **quién habla con
los datos**: eso sigue siendo el cliente tipado y sólo él.

## 3. Estado

**Aprobado** desde 2026-08-10. *[CORREGIDO el 2026-08-31: este apartado decía «Propuesto» mientras la
cabecera del documento decía «Aprobado» desde la emisión inicial. La mesa lo levantó como parte de
`MI-06`. Rige la cabecera: la decisión está en el código desde la etapa `b`.]*

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Render en el servidor con circuito interactivo, salida única desde el servidor (**adoptada**) | Elimina contenido mixto, origen cruzado y exposición de la dirección del servidor propio de una sola vez; la credencial nunca necesita llegar al navegador; la topología se sostiene sin certificado en el servidor propio | Ata la experiencia a la estabilidad del proceso del hosting, que la fuente declara incógnita y sin mitigación en el código; y cada interacción cruza el circuito |
| Ejecutar la aplicación dentro del navegador | Menos carga en el hosting, sin circuito que sostener, sin reciclado que temer | Reabre las tres propiedades de la topología y obliga a transporte seguro válido en un servidor de dirección dinámica. **Descartada por `PRODUCT-INTAKE` §17.2.P.2 · GeometriaFactory-Web**, y registrada como **salida preferente** si `PT-01.b` o `PT-01.c` dan rojo |
| Servir el front desde el propio contenedor del servidor propio | Un solo despliegue, sin hosting externo | Pierde el motivo por el que existe la topología: el bloqueo desde la red de la facultad. **Descartada por `PRODUCT-INTAKE` §17.2.P.2 · GeometriaFactory-Web** |
| **Render en el servidor con reparto por superficie: interactivo donde la interacción lo exige, estático donde alcanza un `POST`** (**la que el código adoptó, incorporada el 2026-08-31**) | Sostiene `RA-01` igual que la adoptada; **una superficie estática no abre circuito y por lo tanto no puede perderlo**, lo que la vuelve inmune al reciclado del proceso del hosting —que es el trade-off más caro de la primera fila—; y el documento pesa lo que pesa sin depender de un canal | Obliga a decidir el modo superficie por superficie y a justificarlo, que es trabajo por cada pantalla nueva; y **las promesas escritas para «todas las superficies» dejan de aplicar a todas** — es lo que `U-06` va a acotar |
| Render en el servidor, pero con algunas llamadas hechas desde el navegador para lo que «no es sensible» | Menos ida y vuelta en operaciones frecuentes | **Una excepción a `RA-01` la anula entera**: bastaría una sola llamada para reabrir origen cruzado y exponer la dirección. Además obligaría a mantener dos criterios de qué es sensible, que es donde el defecto entra. **Descartada por esta categoría** |

## 5. Consecuencias positivas

1. Las tres propiedades de la topología —sin contenido mixto, sin origen cruzado, sin exposición de la dirección del servidor propio— se sostienen con **una sola** decisión verificable.
2. La credencial de sesión no necesita llegar al navegador, lo que hace posible [`ADR-10003`](ADR-10003-Credencial-De-Sesion-En-El-Estado-Del-Circuito.md).
3. `RA-01` tiene un lugar único donde verificarse: el conteo de peticiones del navegador, cuyo umbral es exactamente **0**.
4. El diseño de 03 pudo prescindir de actualizaciones parciales, de validación remota al escribir y de sondeo, lo que además protege la experiencia cuando el transporte repliega.
5. El bundle del visor puede ser un visualizador puro sin que ninguna pantalla pierda función, porque el dato ya llegó por el circuito.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta el reciclado del proceso del hosting como riesgo sin mitigación en el código.** Es `R-06` y es el peor escenario declarado. Lo que hay es tratamiento —el estado «sesión no restablecible» y el envío como única acción de guardado—, no mitigación.
2. **Se acepta que el repliegue a un transporte de mayor latencia degrade la latencia percibida al escribir**, y que **no se le anuncie a la persona**: avisarlo sería alarmar sin darle a nadie nada que hacer.
3. **Se acepta que toda interacción cruce el circuito**, incluidas las que en otro modelo se resolverían en el navegador. La contrapartida es que la escena tridimensional es el único lugar del producto con respuesta inmediata, y 03 decidió no desperdiciarlo con animaciones de entrada.
4. **Se acepta que esta decisión pueda ser superada por medición y no por opinión.** Si `PT-01.b` o `PT-01.c` dan rojo, la salida ya está elegida aguas arriba, y en ese caso corresponde una ADR nueva que supere a ésta.

## 7. Implementación

- El componente **Cliente tipado del servicio de datos** de [`../Arquitectura-Unidad-Entrega.md`](../Arquitectura-Unidad-Entrega.md) §3.1 es la **única** salida hacia el servicio de datos.
- Ninguna superficie invoca al cliente tipado directamente: entre una superficie y la salida hay siempre un servicio de aplicación de front ([`ADR-10004`](ADR-10004-Tres-Capas-De-Presentacion.md)).
- El circuito **termina en el servidor de esta pieza**: no llega al servicio de datos.
- Convención impuesta: agregar una dependencia de guion al proyecto exige comprobar que no consulta servicios por su cuenta, y esa comprobación es bloqueante en revisión.
- **Convención impuesta desde el 2026-08-31: una superficie nueva es ESTÁTICA salvo que declare en su cabecera por qué no puede serlo** (§2.1). Las tres razones admitidas son decidir y redirigir sin intervención, validar mientras se escribe sin consultar al servidor, y mostrar el estado del propio circuito.
- **La salida preferente alcanza a SEIS superficies y no a quince.** Si `PT-01.b` o `PT-01.c` dieran rojo, lo que habría que rehacer es el modo de las seis interactivas; las ocho estáticas **no abren circuito y no se enteran**. La contingencia de §4 quedaba dimensionada sobre el producto de la etapa `b`, cuando las once pantallas eran interactivas.
- El árbol de fuentes no contiene ninguna forma de petición de red escrita en guion de navegador, salvo las que el bundle del visor **no** tiene, porque su propia arquitectura las prohíbe.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Peticiones del navegador hacia el servicio de datos | Exactamente **0** | Conteo en la pestaña de red durante un recorrido completo, incluida la interacción con la escena con los dos movimientos prendidos |
| Salidas del proyecto de código hacia el servicio de datos | Exactamente **1** | Inspección del árbol de fuentes |
| Bibliotecas de guion agregadas que consulten servicios por su cuenta | Exactamente **0** | Inspección de las dependencias de guion |
| `PT-01.a` · dirección pública responde | **200** | Comprobación al final del flujo de publicación |
| `PT-01.b` · transporte del circuito | Verde, o **amarillo aceptable** con la latencia percibida documentada | Inspección del transporte negociado en la etapa `a` |
| Validaciones que consultan al servidor mientras se escribe | Exactamente **0** | Inspección de las superficies con campos |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.16** §14 (`RA-01`, `RA-03`), §17.2.P.2 · GeometriaFactory-Web, §17.2.P.3 · GeometriaFactory-Web, §17.2.P.10 · GeometriaFactory-Web y §17.2.P.12 · GeometriaFactory-Web.
- [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §2 y §6 (`RT-01`).
- [`../../03-UX-UI-DX/Experiencia-De-Uso.md`](../../03-UX-UI-DX/Experiencia-De-Uso.md) §2.4 y §7.
- [`../../../GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Adrs/ADR-08004-Regla-De-Exposicion-De-La-Frontera.md`](../../../../Producto/Adrs/ADR-08004-Regla-De-Exposicion-De-La-Frontera.md), que declara del otro lado que **todas** las solicitudes las arma el servidor de la unidad pública.
- ADR relacionadas: [`ADR-10003`](ADR-10003-Credencial-De-Sesion-En-El-Estado-Del-Circuito.md), [`ADR-10004`](ADR-10004-Tres-Capas-De-Presentacion.md), [`ADR-10006`](ADR-10006-Aislamiento-Del-Visor-Tras-Su-Fachada.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-31 | **El reparto de modos de render entra a la capa que decide, que es `U-05` del plan de la mesa y cierra `MI-06`.** §2 declaraba «la interacción viaja por un circuito», y eso describía a las **once pantallas de marcador de posición de la etapa `b`**: al construirles comportamiento en la etapa `c`, la mayoría pasó a render estático con `POST` de verdad, cada una con su motivo en la cabecera de su componente. **El criterio existía y no estaba acá.** Entra **§2.1** con el reparto contado sobre el árbol —**quince rutas sobre catorce componentes, seis interactivos y ocho estáticos**— y con la convención vinculante: **una superficie nueva es estática salvo que declare por qué no puede serlo**. Entra la **quinta alternativa**, que es la que el código adoptó y que §4 no evaluaba. **§3 pasa de «Propuesto» a «Aprobado»**, que es lo que la cabecera decía desde la emisión inicial. Y §7 **re-dimensiona la contingencia**: la salida preferente alcanza a **seis** superficies, no a quince, porque una superficie estática no abre circuito y por lo tanto no puede perderlo. **La dirección de la corrección es vinculante y se declara: se actualiza la ADR al código, el código no se toca** — alinear al revés volvería interactivas nueve superficies sobre un hosting sin WebSocket, y eso toca topología cerrada. `NotFoundPage` queda señalada como la única interactiva **sin motivo declarado**, y no se toca: es deuda con disparador. **`RA-01` no se relaja**: el reparto es sobre dónde se procesa la interacción, no sobre quién habla con los datos. |
| 1.0 | 2026-08-10 | Emisión inicial. Registra el render en el servidor con circuito interactivo y la salida única hacia el servicio de datos como materialización de `RA-01`, evalúa cuatro alternativas —dos descartadas por el intake, una por esta categoría— con la salida preferente registrada por si la medición la supera, declara cuatro trade-offs y fija seis métricas de validación. |
