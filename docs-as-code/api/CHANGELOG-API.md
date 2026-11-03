---
doc_id: FIC-GEO-007
doc_type: anexo
title: Registro de cambios de la API
description: Cambios de la superficie HTTP descrita por la instantánea OpenAPI de la Api, en formato Keep a Changelog 1.1.0.
status: borrador
origin: ia-generated
confidence: media
owner: mantenimiento de docs-as-code (agente, expediente 0003)
last_review: 2026-11-03
audience: [desarrollo, agente]
delivery: [publico]
traces: [FIC-GEO-001]
---

# Registro de cambios de la API

Formato [Keep a Changelog 1.1.0](https://keepachangelog.com/es-ES/1.1.0/). Registra los cambios de la superficie que
describe la instantánea [`openapi-v1.json`](openapi-v1.json), artefacto CL-05 de la
[ficha de diseño documental](../ficha-diseno-documental.md).

## Cómo se mantiene

- La instantánea **no se edita a mano**. La regenera el job `instantanea-openapi` de `.github/workflows/docs.yml`
  en cada push a `main`; si el documento cambió, abre un PR automático desde la rama `automatico/instantanea-openapi`
  (decisión K-07 del expediente 0003). Ningún PR de producto tiene que regenerarla. Si no puede abrir el PR, el job
  falla y deja el aviso en el issue de etiqueta `instantanea-openapi`; la rama queda empujada para abrirlo a mano.
- La extracción es aislada (decisión K-12): [`extraer-openapi.sh`](extraer-openapi.sh) compila la Api y la levanta
  como proceso local con un almacén efímero, una clave de firma descartable y el puerto 5199 en `127.0.0.1`. No usa
  Docker ni ningún contenedor del despliegue. Quita `servers` del documento: la dirección local de la extracción no
  sirve a quien integra.
- Se describe la superficie de producción: entorno `Production` con `Documentacion__Publicada=true`, la llave que
  publica el documento fuera de desarrollo (`src/GeometriaFactory.Api/Composition/ApiDocumentation.cs:43`, `:109-110`).
  La ruta del documento es `/openapi/v1.json` (`ApiDocumentation.cs:37`).
- Quien revisa el PR automático agrega aquí la entrada del cambio, bajo «Sin publicar», antes de fusionar.
- Para regenerarla a mano (con el SDK de .NET 10): `bash docs-as-code/api/extraer-openapi.sh`.

## [Sin publicar]

### Añadido

- `summary` en las 18 operaciones y sus respuestas declaradas: la exitosa con su tipo del contrato (o `204` sin
  cuerpo), los errores con el cuerpo `ErrorResponse` y el `429` del límite de tasa (tramo T-12 del expediente 0003).
  Las rutas y las operaciones no cambian: siguen 14 rutas y 18 operaciones.

### Quitado

- `servers`: declaraba `http://127.0.0.1:5199/`, la dirección local de la extracción. Sin `servers`, las rutas se
  resuelven contra el origen desde el que se sirve el documento.

## 2026-11-02 — Línea de base

### Añadido

- Primera instantánea de la superficie `v1`: 14 rutas y 18 operaciones, generadas desde `origin/main`. Incluye
  `POST /v1/interpretaciones/pistas` (capa de IA, expediente 0002), que un diff contra esta base no marcará como nueva.
