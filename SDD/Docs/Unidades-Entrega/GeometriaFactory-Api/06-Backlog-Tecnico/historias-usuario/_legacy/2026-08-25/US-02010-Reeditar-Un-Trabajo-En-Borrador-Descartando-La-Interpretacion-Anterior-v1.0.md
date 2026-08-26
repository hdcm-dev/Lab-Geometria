> **Artefacto archivado — estado `Superado`**
>
> Copia archivada de `US-02010-Reeditar-Un-Trabajo-En-Borrador-Descartando-La-Interpretacion-Anterior.md` en su versión **1.0**, tomada el 2026-08-25 por el orquestador de migración normativa **antes** de aplicar el corte de la categoría 06 de la fase M4 (`Master-Prompt.md` §5 y §8).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-25
> - **Versión vigente:** [`US-02010-Reeditar-Un-Trabajo-En-Borrador-Descartando-La-Interpretacion-Anterior.md`](../../US-02010-Reeditar-Un-Trabajo-En-Borrador-Descartando-La-Interpretacion-Anterior.md)
>
> El cuerpo que sigue **no se modifica**. Lo único que se tocó son **los enlaces relativos**, reescritos dos niveles para que sigan resolviendo desde esta ubicación.

---

# US-02010 — Reeditar un trabajo en `Borrador` descartando la interpretación anterior

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-02010-Reeditar-Un-Trabajo-En-Borrador-Descartando-La-Interpretacion-Anterior.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-02004 Gestión del trabajo
**Etapa del producto:** `e`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca de dominio (`GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`)**, quiero **reeditar un trabajo que está en `Borrador`, descartando la interpretación anterior**, para **que el alumno corrija su trabajo sin arrastrar piezas ni observaciones de un texto que ya no es el suyo**.

## 2. Contexto

La capacidad `F-07` del intake §4 acota la reedición al estado `Borrador`. `PRODUCT-INTAKE` §4.2 declara que `Borrador` significa exactamente que el texto todavía no verificó, o que el trabajo recién se creó.

## 3. Criterios de aceptación

- Given un trabajo en estado `Borrador`, When se lo reedita con un texto nuevo, Then el texto queda reemplazado y el conjunto de piezas y las observaciones anteriores quedan descartados.
- Given un trabajo en estado `Pendiente`, `Finalizado` o `Rechazado`, When se intenta reeditarlo, Then se rechaza, por `RN-02004` y por `INV-07` en los dos terminales.
- Given un trabajo reeditado, When se consulta su estado, Then sigue en `Borrador`: la reedición no es un envío.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00003, NB-00004 |
| CU cubiertos | CU-02005 |
| RN e invariantes que ejerce | RN-02004, RN-02008, RN-02010; INV-03, INV-07 |
| BT derivadas | BT-02012, BT-02013 |
| Etapa del producto | `e`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba unitaria de la reedición admitida y de las tres rechazadas por estado. |

## 5. Prioridad y estimación

`Must` por derivar de `F-07`, `Must Have` en `PRODUCT-INTAKE` §4.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Cita por identificador toda regla e invariante que ejerce, sin volver a enunciarla
- [x] Las condiciones de rechazo que produce existen en el catálogo de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Sus tareas técnicas están identificadas y ninguna está bloqueada

## 7. Notas y supuestos

El nombre del archivo `RN-02004-Eliminacion-Acotada-Al-Borrador.md` conserva un slug que ya no describe del todo su enunciado, que hoy cubre también el borrado del administrador; se cita el contenido vigente y no lo que sugiere el nombre ([`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../../02-Especificacion-Funcional/Especificacion-Funcional.md) §8 punto 3).

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.3 previó con este mismo identificador y este mismo contenido. |
