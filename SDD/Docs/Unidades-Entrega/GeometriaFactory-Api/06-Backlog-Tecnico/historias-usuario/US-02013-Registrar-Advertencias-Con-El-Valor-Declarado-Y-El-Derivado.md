# US-02013 — Registrar advertencias con el valor declarado y el derivado

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-02013-Registrar-Advertencias-Con-El-Valor-Declarado-Y-El-Derivado.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-02005 Interpretación y verificación del dato del alumno
**Etapa del producto:** `f`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca de dominio (`GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`)**, quiero **incorporar al trabajo las advertencias de valor, con el valor declarado y el derivado en campos separados**, para **que el alumno vea en qué se equivocó su cálculo sin que el sistema le corrija el número, y que la advertencia no le bloquee la entrega**.

## 2. Contexto

`NB-00005` es la visibilidad del error de cálculo. La capacidad `F-10` del intake §4 declara la verificación de los valores recalculándolos y las advertencias **que no bloquean**. `PRODUCT-INTAKE` §17.1.P.11 · GeometriaFactory-Domain punto 3 declara que el valor declarado y el derivado **se guardan por separado**.

## 3. Criterios de aceptación

- Given un trabajo cuyo cálculo declarado difiere del derivado más allá de la tolerancia, When se adoptan sus observaciones, Then queda registrada una advertencia con los **dos** valores en campos propios.
- Given un trabajo con advertencias y sin errores de validación, When se envía, Then pasa a estado `Pendiente` igual: la advertencia no bloquea, por `RN-02005` e `INV-04`.
- Given una advertencia registrada, When se lee la pieza que la produjo, Then conserva su valor declarado sin modificar: el sistema no reescribe el dato del alumno, por `RN-02008`.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00005, NB-00004 |
| CU cubiertos | CU-02007 |
| RN e invariantes que ejerce | RN-02005, RN-02008; INV-04 |
| BT derivadas | BT-02013 |
| Etapa del producto | `f`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba unitaria sobre los escenarios `E-3` y `E-4` del intake §20, que son los dos cubos con y sin advertencia de área. |

## 5. Prioridad y estimación

`Must` por derivar de `F-10`, `Must Have` en `PRODUCT-INTAKE` §4, y por ser el aporte de este proyecto de código a `NB-00005`.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Cita por identificador toda regla e invariante que ejerce, sin volver a enunciarla
- [x] Las condiciones de rechazo que produce existen en el catálogo de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Sus tareas técnicas están identificadas y ninguna está bloqueada

## 7. Notas y supuestos

**La tolerancia de comparación y el recálculo no son de este proyecto de código**: llegan como resultado de la interpretación producida afuera. `PRODUCT-INTAKE` §22 declara además que la tolerancia **no es una asunción** sino un dato de las fuentes.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.3 previó con este mismo identificador y este mismo contenido. |
