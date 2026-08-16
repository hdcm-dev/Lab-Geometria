# ADR-10001 — Render en el servidor con circuito interactivo, y una sola salida hacia el servicio de datos

**Proyecto de código:** GeometriaFactory-Web
**Documento:** ADR-10001-Render-En-El-Servidor-Con-Circuito-Interactivo.md
**Versión:** 1.0
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

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Render en el servidor con circuito interactivo, salida única desde el servidor (**adoptada**) | Elimina contenido mixto, origen cruzado y exposición de la dirección del servidor propio de una sola vez; la credencial nunca necesita llegar al navegador; la topología se sostiene sin certificado en el servidor propio | Ata la experiencia a la estabilidad del proceso del hosting, que la fuente declara incógnita y sin mitigación en el código; y cada interacción cruza el circuito |
| Ejecutar la aplicación dentro del navegador | Menos carga en el hosting, sin circuito que sostener, sin reciclado que temer | Reabre las tres propiedades de la topología y obliga a transporte seguro válido en un servidor de dirección dinámica. **Descartada por `PRODUCT-INTAKE` §17.2.P.2 · GeometriaFactory-Web**, y registrada como **salida preferente** si `PT-01.b` o `PT-01.c` dan rojo |
| Servir el front desde el propio contenedor del servidor propio | Un solo despliegue, sin hosting externo | Pierde el motivo por el que existe la topología: el bloqueo desde la red de la facultad. **Descartada por `PRODUCT-INTAKE` §17.2.P.2 · GeometriaFactory-Web** |
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

- El componente **Cliente tipado del servicio de datos** de [`../Arquitectura-Proyecto-Codigo.md`](../Arquitectura-Proyecto-Codigo.md) §3.1 es la **única** salida hacia el servicio de datos.
- Ninguna superficie invoca al cliente tipado directamente: entre una superficie y la salida hay siempre un servicio de aplicación de front ([`ADR-10004`](ADR-10004-Tres-Capas-De-Presentacion.md)).
- El circuito **termina en el servidor de esta pieza**: no llega al servicio de datos.
- Convención impuesta: agregar una dependencia de guion al proyecto exige comprobar que no consulta servicios por su cuenta, y esa comprobación es bloqueante en revisión.
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
| 1.0 | 2026-08-10 | Emisión inicial. Registra el render en el servidor con circuito interactivo y la salida única hacia el servicio de datos como materialización de `RA-01`, evalúa cuatro alternativas —dos descartadas por el intake, una por esta categoría— con la salida preferente registrada por si la medición la supera, declara cuatro trade-offs y fija seis métricas de validación. |
