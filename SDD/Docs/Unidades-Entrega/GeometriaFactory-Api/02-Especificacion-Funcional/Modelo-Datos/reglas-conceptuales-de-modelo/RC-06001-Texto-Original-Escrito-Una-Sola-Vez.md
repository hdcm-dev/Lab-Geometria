# RC-06001 — El texto original se escribe una sola vez y no se reescribe

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** RC-06001-Texto-Original-Escrito-Una-Sola-Vez.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`RN-02008`](../../Reglas-De-Negocio/RN-02008-Texto-Original-Conservado-Integro.md); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.12** §4.1 (RN-06008), §9 (X-4), §17.1.P.4 · GeometriaFactory-Infrastructure («`JsonOriginal` conservado íntegro y nunca reescrito»), §17.1.P.11 · GeometriaFactory-Infrastructure punto 2
**Trazabilidad downstream:** `05-Arquitectura-Tecnica` y `08-Calidad-Y-Pruebas` de GeometriaFactory-Infrastructure

---

## 1. Enunciado

El texto original del trabajo se escribe **una sola vez, al crearse el trabajo**, y **ninguna escritura posterior lo reemplaza**. El almacén lo conserva **literal**: no lo normaliza, no lo reindenta, no lo reordena y no lo corrige.

## 2. Justificación

Es la regla que hace que el producto sea fiel al dato del alumno, y la que sostiene su mayor valor didáctico: el texto conservado es la única fuente fiel del trabajo, y el formato de entrada es premisa fija. Tiene además una consecuencia operativa concreta: **si el validador mejora, el mismo trabajo se puede reprocesar**, porque el texto que el alumno pegó sigue ahí exactamente como estaba.

Y tiene una consecuencia negativa igual de deliberada: la edición o corrección del texto desde la aplicación está **excluida del producto**, y esa exclusión se justifica precisamente con esta conservación.

## 3. Ámbito de aplicación

- Alcanza al **texto que el alumno pegó**, guardado como texto en la fila del trabajo.
- **No alcanza a los datos del trabajo** —nombre, fecha declarada, descripción—, que sí se reeditan mientras el trabajo está en `Borrador`.
- **No alcanza a las piezas, los componentes ni las observaciones**, que se reemplazan enteros en cada envío: son el resultado de interpretar el texto, no el texto.
- El almacén **no consulta el contenido del texto**: no se indexa, no se recorre y no se filtra por él.
- **No se le agrega ninguna fecha.** El texto que el alumno produce no lleva fechas, y los sellos viven en la fila del trabajo.

## 4. Consecuencia si se viola

Una escritura que reemplace el texto conservado devuelve `ESCRITURA_QUE_REESCRIBE_EL_TEXTO_ORIGINAL` y **no escribe nada** ([`CU-06003`](../../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06003-Guardar-Y-Recuperar-Los-Trabajos.md) §6).

Violarla en silencio —normalizando el texto al guardarlo, por ejemplo— produce un daño que no se nota hasta que alguien compara: el alumno vuelve a abrir su trabajo y ve un texto que él no escribió, las comas finales desaparecen y el escenario que documenta la tolerancia del formato deja de ser reproducible desde el almacén.

## 5. CU afectados

- [`CU-06003`](../../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06003-Guardar-Y-Recuperar-Los-Trabajos.md) — Guardar y recuperar los trabajos: es donde se hace cumplir.
- [`CU-06001`](../../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06001-Interpretar-El-Texto-Original-Y-Reconstruir-Las-Piezas.md) — **por contraste**: el validador lee el texto y **no lo devuelve corregido**.

## 6. Pruebas que la verifican

`CU-06003` CA-01 —el texto del escenario **E-2** materializado y recuperado, idéntico carácter por carácter, con sus dos comas finales— y CA-02 —la escritura que intenta reemplazarlo, rechazada—. Las dos son de integración contra el almacén real, que es donde el intake ubica la verificación de la persistencia. `CU-06001` CA-09 la cubre del lado del validador.

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. |
| 1.1 | 2026-08-10 | Actualización de la cita del `PRODUCT-INTAKE` de **1.11** a **1.12** en la trazabilidad upstream: 1.11 quedó archivada al resolver el Product Owner el desenlace del envío del escenario `E-8`. Corrige el hallazgo **H-02** del informe de auditoría `SDD/Docs/Audit/B-02-03-GeometriaFactory-Infrastructure-r1.md` (ronda 1). El delta entre 1.11 y 1.12 se revisó y sólo alcanza a `E-8`, que no toca lo que este documento declara: sin cambios de contenido. |
