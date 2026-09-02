# El socket · qué es, qué no es, y qué se pudo hacer

El Product Owner señaló el aviso de la consola como causa de que «Aprobar» no funcionara.
**Tenía razón en que el socket está detrás, y el mecanismo era otro.**

## El anfitrión no tiene WebSocket

```text
POST /_blazor/negotiate  →  availableTransports: ServerSentEvents, LongPolling
GET  /_blazor?id=…  con Upgrade: websocket  →  HTTP/1.1 200 OK   (debería ser 101)
```

**No es la red del Product Owner, ni un proxy, ni una VPN** —que es lo que sugiere el texto del
aviso—: es el servidor, que no ofrece el transporte.

## Se intentó habilitarlo, y no alcanza

Se subió `<webSocket enabled="true" />` al `web.config` del sitio:

```text
el sitio siguió en pie:            HTTP 200   (el ajuste es válido, no rompe)
la actualización siguió dando:     HTTP 200   (no cambió nada)
la negociación siguió ofreciendo:  ServerSentEvents, LongPolling
```

**No se puede encender desde el sitio lo que el servidor no tiene instalado.** El archivo se
devolvió a como estaba.

## El socket NO es la causa de que el desenlace no se aplique

El sondeo largo entrega los eventos: hay un desenlace aplicado por ese transporte, dos veces, una
con la cuenta del Product Owner —`Submitted → Approved`—.

## El socket SÍ es la causa de la ventana muerta

Sin WebSocket, establecer el circuito cuesta segundos, y en esos segundos los controles no
responden. **Eso es lo que el Product Owner sufría.**

### La medición, contra el anfitrión real

```text
ANTES (despliegue #48)
   carga 1.4 s → control listo 3.9 s
   carga 1.0 s → control listo 3.1 s
   carga 0.9 s → control listo 3.0 s
   PROMEDIO hasta control listo: 3.3 s
```
