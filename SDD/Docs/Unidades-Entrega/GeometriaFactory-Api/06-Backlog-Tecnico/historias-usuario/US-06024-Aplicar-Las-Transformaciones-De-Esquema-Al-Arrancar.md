# US-06024 — Aplicar las transformaciones de esquema al arrancar, sobre base inexistente

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** US-06024-Aplicar-Las-Transformaciones-De-Esquema-Al-Arrancar.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-06001 Esqueleto ambulante y verificación de viabilidad
**Etapa del producto:** `a`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que el almacén se cree y se transforme solo al arrancar el servicio**, para **que el laboratorio se pueda levantar desde cero sin ningún paso manual de despliegue**.

## 2. Contexto

`PRODUCT-INTAKE` §17.1.P.11 · GeometriaFactory-Infrastructure punto 3 declara las transformaciones **aplicadas al arrancar y no por un paso manual**, y §17.1.P.8 · GeometriaFactory-Infrastructure las declara **criterio de aceptación de la etapa `c`**. Y `PT-04`, que se mide en la etapa `a`, exige que la imagen del servicio de datos **arranque, aplique sus actualizaciones de esquema sobre base vacía y responda salud**. El contrato de uso es [`CU-06010`](../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06010-Preparar-El-Almacen-Al-Arrancar.md).

## 3. Criterios de aceptación

- Given un almacén inexistente, When arranca el servicio, Then las transformaciones se aplican **solas** y el almacén queda en condiciones: **1 de 1** intento exitoso, sin paso manual.
- Given un almacén desactualizado con linaje compatible, When arranca, Then se le aplican las transformaciones que le faltan.
- Given una transformación ya fusionada, When se la mira, Then **no se edita**: cada una se versiona con el código de su etapa.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00003, NB-00008 (parcial) |
| CU cubiertos | CU-06010 |
| RN que ejerce | — |
| Componente de `05` §3.1 | Mecanismo de acceso firmado y preparación del almacén, Contexto de persistencia y mapeo |
| Reglas conceptuales de modelo | Materializa el esquema que las siete gobiernan |
| ¿Toma alguna decisión de negocio? | **No** |
| ¿Toca el almacén? | **Sí**, y es la operación que lo crea |
| BT derivadas | BT-06005, BT-06006, BT-06007 |
| Tests previstos en 08 | Etapa de verificación de transformaciones del pipeline, sobre un almacén recién creado |

## 5. Prioridad y estimación

`Must` porque es parte de lo que **`PT-04` mide en la etapa `a`**, y una puerta que no pasa **detiene la planificación de las etapas que dependen de ella**; y porque el criterio de transición `c` → `d` exige que las actualizaciones de esquema **se apliquen solas sobre una base inexistente**.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el componente de `05` §3.1 y, si toca el almacén, las reglas conceptuales de modelo que materializa
- [x] Declara que no toma ninguna decisión de negocio
- [x] Toda condición que produce existe en el catálogo de las 17 de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md), declarada como resultado o como fallo
- [x] Declara el camino en que el mecanismo se detiene en lugar de cumplir a medias, cuando puede fallar
- [x] Declara si toca el almacén y, en consecuencia, dónde vive su prueba

## 7. Notas y supuestos

**El guion de restablecimiento que el intake declara no es un camino de producción**: reproduce el estado de primer arranque, o sea **un almacén vacío**. `05` §5 lo dice explícitamente para que no se lo use como reversión.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
