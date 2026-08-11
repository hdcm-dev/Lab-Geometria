# US-14 — Registrar errores de validación con posición de pieza y campo

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** US-14-Registrar-Errores-De-Validacion-Con-Posicion-De-Pieza-Y-Campo.md
**Versión:** 1.0
**Estado:** Propuesta
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-05 Interpretación y verificación del dato del alumno
**Etapa del producto:** `f`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca de dominio (`GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`)**, quiero **incorporar al trabajo los errores de validación, cada uno con la posición de la pieza y el campo señalado**, para **que el alumno sepa dónde está el problema en lugar de recibir un texto genérico**.

## 2. Contexto

`RN-09` declara que los mensajes de error de validación indican índice de figura y campo, **nunca un texto genérico**. El contrato de uso es [`CU-07`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-07-Registrar-Las-Observaciones-Del-Trabajo.md).

## 3. Criterios de aceptación

- Given un error de validación producido afuera con su posición de pieza y su campo, When se lo adopta, Then queda registrado con esas dos referencias.
- Given un error de validación al que le falta la posición o el campo, When se intenta adoptarlo, Then el conjunto se rechaza por mal formado: `RN-09` no admite la observación incompleta.
- Given un trabajo con al menos un error de validación adoptado, When se envía, Then **no** pasa a estado `Pendiente`, por `RN-05` e `INV-04`.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-04 |
| CU cubiertos | CU-07 |
| RN e invariantes que ejerce | RN-05, RN-09; INV-04 |
| BT derivadas | BT-13 |
| Etapa del producto | `f`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba unitaria sobre el escenario `E-5` del intake §20, que es el tipo desconocido con índice de figura y campo. |

## 5. Prioridad y estimación

`Must` por `RN-09`, declarada cerrada en `PRODUCT-INTAKE` §4.1, y por derivar de `F-09`, `Must Have`.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Cita por identificador toda regla e invariante que ejerce, sin volver a enunciarla
- [x] Las condiciones de rechazo que produce existen en el catálogo de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Sus tareas técnicas están identificadas y ninguna está bloqueada

## 7. Notas y supuestos

`RN-09` **no tiene invariante asociado**: describe cómo se compone una observación y no una condición permanente sobre el estado (`PRODUCT-INTAKE` §17.1.P.2).

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.3 previó con este mismo identificador y este mismo contenido. |
