# ADR-10009 — El front le dice al servicio de datos de dónde llegó el navegador

**Unidad de entrega:** GeometriaFactory-Web
**Documento:** ADR-10009-El-Front-Le-Dice-Al-Servicio-De-Datos-De-Donde-Llego-El-Navegador.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-09-14
**Autor:** Arquitecto de Software Senior (AG-05), sobre el hallazgo R-02 de la mesa a pedido del 2026-09-14
**Categoría:** Comunicación

---

## 1. Contexto

El servicio de datos limita el canje de credenciales y las peticiones anónimas **por dirección de origen** ([`Api ADR-00011`](../../../GeometriaFactory-Api/05-Arquitectura-Tecnica/Adrs/ADR-00011-El-Canje-Se-Protege-Por-Cuenta-Y-El-Origen-Tolera-Una-Comision.md) §2 punto 4). Los topes están dimensionados para una comisión detrás de una dirección.

**Detrás del front, esa dirección era la del front para todas las personas.** El ingreso, el registro y el cambio obligado de contraseña los procesa el front del lado del servidor y llama al servicio de datos con su propia dirección. Un solo cliente anónimo que inundara el ingreso agotaba la cuota compartida y dejaba a la comisión entera sin entrar, que es lo que `RN-B1` prohíbe. La mesa lo registró como R-02 (`../../../../Audit/Mesa-2026-09-14.md`).

`Api ADR-00011`:45 dejó la decisión explícitamente de este lado: «que el front reenvíe `X-Forwarded-For` … es decisión de `GeometriaFactory.Web` y queda fuera de esta ADR».

Además, **el front corre detrás del túnel de publicación**, y sin declarar ese proxy ve a todos los navegadores con la dirección del túnel: reenviar esa dirección no separaría a nadie.

## 2. Decisión

1. **El front resuelve la dirección real del navegador** con `UseForwardedHeaders`, primero en la tubería, confiando sólo en las redes declaradas en `ForwardedHeaders:KnownNetworks`, la misma llave que usa el servicio de datos. Sin ninguna declarada, la cabecera entrante se ignora.
2. **El cliente del servicio de datos agrega `X-Forwarded-For` con esa dirección** cuando la llamada corre dentro de una petición HTTP del navegador (`BrowserOriginForwardingHandler`). Escribe la dirección resuelta y reemplaza cualquier valor previo: un navegador no elige su partición escribiendo la cabecera.
3. **En el circuito interactivo no se agrega nada.** No hay petición del navegador de la cual tomar la dirección, y esas llamadas llevan sesión: el servicio de datos las particiona por persona.
4. **Que la cabecera cuente es decisión del despliegue del servicio de datos**: su composición declara la red del front en `ForwardedHeaders__KnownNetworks`. Sin eso la ignora, y el comportamiento es el de antes.

## 3. Estado

**Aprobado** desde el 2026-09-14, dentro del lote 1 del plan de la mesa, aprobado por el Product Owner.

## 4. Alternativas consideradas

| Alternativa | Por qué no |
| --- | --- |
| **Un limitador propio en el front** | Duplica en otra pieza la política que ya vive, configurada y probada, en el servicio de datos, y la desincroniza al primer ajuste de `PT-05` |
| **Subir los topes por origen** | No separa a nadie: sólo agranda lo que un solo cliente puede consumir antes de dejar afuera a la clase |
| **Reenviar el `X-Forwarded-For` que trae el navegador** | Le deja al cliente elegir su partición |

## 5. Consecuencias positivas

1. Cada navegador consume su propia cuota en el servicio de datos, y un cliente abusivo agota la suya.
2. No hay política duplicada: los topes siguen en un solo lugar, `Api ADR-00011`.

## 6. Consecuencias negativas y trade-offs

1. **Sin las dos redes declaradas en el despliegue, no cambia nada.** El front tiene que confiar en el túnel, y el servicio de datos en el front. Es configuración del host y no se versiona acá.
2. **Varios navegadores detrás del NAT de la facultad siguen compartiendo dirección.** Es el caso para el que los topes por origen ya están dimensionados.

## 7. Implementación

- `src/GeometriaFactory.Web/Program.cs`: `ForwardedHeadersOptions` desde `ForwardedHeaders:KnownNetworks`, con detención del arranque ante una red mal escrita, y `UseForwardedHeaders` primero en la tubería.
- `src/GeometriaFactory.Web/Integration/BrowserOriginForwardingHandler.cs`, conectado al cliente tipado del servicio de datos.

## 8. Métricas de validación

- `BrowserOriginForwardingTests`: la dirección del navegador viaja; la cabecera escrita por el navegador no viaja; fuera de una petición del navegador no se inventa ninguna dirección. **Vistas fallar** contra el front anterior en las dos primeras.
- Del lado del servicio de datos, `RateLimitingTests.BehindADeclaredProxyTheForwardedAddressIsTheOneThatCounts` y `WithoutADeclaredProxyTheForwardedHeaderIsIgnored` ya fijan que la cabecera cuenta sólo desde una red declarada.

## 9. Referencias

- [`Api ADR-00011`](../../../GeometriaFactory-Api/05-Arquitectura-Tecnica/Adrs/ADR-00011-El-Canje-Se-Protege-Por-Cuenta-Y-El-Origen-Tolera-Una-Comision.md) — los topes por origen y la decisión que dejó de este lado.
- [`ADR-10007`](ADR-10007-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md) — la dirección del servicio de datos llega por configuración, por el mismo cliente tipado.
- `../../../../Audit/Mesa-2026-09-14.md` — R-02.

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-09-14 | Emisión inicial, con las pruebas que la verifican. |
