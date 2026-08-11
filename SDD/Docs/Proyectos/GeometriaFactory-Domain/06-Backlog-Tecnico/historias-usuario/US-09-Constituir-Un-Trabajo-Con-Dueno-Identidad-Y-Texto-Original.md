# US-09 — Constituir un trabajo con dueño, identidad propia y texto original

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** US-09-Constituir-Un-Trabajo-Con-Dueno-Identidad-Y-Texto-Original.md
**Versión:** 1.0
**Estado:** Propuesta
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-04 Gestión del trabajo
**Etapa del producto:** `e`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca de dominio (`GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`)**, quiero **constituir un trabajo con su dueño, su identidad propia y el texto original del alumno conservado íntegro**, para **que el trabajo del alumno quede guardado con su nombre y no se pierda, y que el texto que él escribió nunca se reescriba**.

## 2. Contexto

La capacidad `F-06` del intake §4 declara la carga del trabajo con nombre, fecha, descripción y el texto de figuras, con identificador propio. `RN-08` declara que el texto original se conserva íntegro y nunca se reescribe. El contrato de uso es [`CU-05`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-05-Crear-Y-Reeditar-Un-Trabajo.md).

## 3. Criterios de aceptación

- Given los datos de un trabajo y su texto original, When se constituye el trabajo, Then queda con dueño, con identidad propia, en estado `Borrador` y con el texto **idéntico** al que llegó.
- Given un trabajo ya constituido, When se lee su texto original, Then es carácter por carácter el que se aportó, sin normalización ni reescritura, por `RN-08`.
- Given una solicitud de constitución sin dueño, When se procesa, Then se rechaza: un trabajo sin dueño no existe, que es lo que sostiene `INV-02`.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-03, NB-04 |
| CU cubiertos | CU-05 |
| RN e invariantes que ejerce | RN-03, RN-08; INV-02 |
| BT derivadas | BT-06, BT-12 |
| Etapa del producto | `e`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba unitaria de la constitución y de la integridad del texto original; el material de prueba son los escenarios del intake §20 y **no se inventan textos** (`PRODUCT-INTAKE` §15, regla de delivery 5). |

## 5. Prioridad y estimación

`Must` por derivar de `F-06`, `Must Have` en `PRODUCT-INTAKE` §4, y porque toda la etapa `e` cuelga de que exista un trabajo con dueño.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Cita por identificador toda regla e invariante que ejerce, sin volver a enunciarla
- [x] Las condiciones de rechazo que produce existen en el catálogo de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Sus tareas técnicas están identificadas y ninguna está bloqueada

## 7. Notas y supuestos

**Las dos fechas del trabajo —creación y última modificación— las produce el consumidor a través del puerto de reloj** y son distintas de la fecha que el alumno declara (`PRODUCT-INTAKE` §17.3.P.4, decisión del Product Owner). El dominio no lee el reloj ([`ADR-06`](../../05-Arquitectura-Tecnica/Adrs/ADR-06-El-Dominio-No-Lee-El-Reloj-Ni-El-Conjunto.md)).

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.3 previó con este mismo identificador y este mismo contenido. |
