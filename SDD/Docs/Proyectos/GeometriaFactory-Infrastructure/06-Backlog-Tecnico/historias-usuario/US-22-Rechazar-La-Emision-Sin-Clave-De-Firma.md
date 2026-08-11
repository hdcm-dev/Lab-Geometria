# US-22 — Rechazar la emisión sin clave de firma, sin generar una al vuelo

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** US-22-Rechazar-La-Emision-Sin-Clave-De-Firma.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-02 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que la emisión del acceso se rechace cuando la clave de firma no llega**, y que **jamás se genere una al vuelo ni se emita sin firmar**, para **que nadie pueda falsificar un acceso sobre un sistema que arrancó igual**.

## 2. Contexto

`05` §9 declara este riesgo con impacto **muy alto**: si ante la ausencia de clave se generara una al vuelo o se emitiera sin firmar, **el sistema arranca, emite accesos y nadie lo nota hasta que alguien falsifica uno**. `PRODUCT-INTAKE` §17.3.P.5 fija que la clave **se provee o se genera en el primer arranque y vive fuera del repositorio de código y fuera de la imagen**, y `05` §5 que este proyecto de código **la recibe y no la busca**. El contrato de uso es [`CU-08`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-08-Emitir-El-Acceso-Firmado.md).

## 3. Criterios de aceptación

- Given una clave de firma ausente, When se pide emitir un acceso, Then se devuelve la condición correspondiente y se emiten exactamente **0** accesos.
- Given esa misma situación, When se busca en el mecanismo un camino que genere una clave, Then **no hay ninguno**.
- Given la clave provista, When se inspeccionan los mensajes y las trazas, Then **0** la contienen.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-02 |
| CU cubiertos | CU-08 |
| RN que ejerce | — directamente; sostiene `RA-03` en su parte de secretos |
| Componente de `05` §3.1 | Mecanismo de acceso firmado y preparación del almacén |
| Reglas conceptuales de modelo | — |
| ¿Toma alguna decisión de negocio? | **No** |
| ¿Toca el almacén? | **No** |
| BT derivadas | BT-15, BT-21, BT-22 |
| Tests previstos en 08 | Prueba de emisión sin clave de firma, comprobando que no produce acceso |

## 5. Prioridad y estimación

`Must` por el riesgo de impacto **muy alto** de `05` §9, y porque la configuración de esta capa **se recibe y no se busca**: por eso la ausencia de clave es una **condición** y no un valor por defecto.

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

**Es la segunda de las historias cuyo entregable es una terminación.** Junto con US-20 y US-25 forman el grupo que materializa la propiedad de que **un mecanismo que no puede cumplir su promesa se detiene y lo dice**.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
