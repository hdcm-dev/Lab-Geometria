# US-02018 — Resolver la pertenencia de un trabajo a su dueño

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** US-02018-Resolver-La-Pertenencia-De-Un-Trabajo-A-Su-Dueno.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-02004 Gestión del trabajo
**Etapa del producto:** `e`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca de dominio (`GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`)**, quiero **un predicado que resuelva si un trabajo pertenece a la cuenta que lo pide**, para **que un alumno no pueda distinguir el trabajo de otro de uno que no existe**.

## 2. Contexto

`RN-02003` declara que un alumno sólo ve y opera sus propios trabajos, y que pedir el trabajo de otro devuelve «no encontrado» y no «no autorizado». `INV-02` lo expresa como condición permanente y `05` §10.3 aclara que se ejerce como predicado de pertenencia sobre una entidad, **no como consulta**.

## 3. Criterios de aceptación

- Given un trabajo y la cuenta de su dueño, When se resuelve la pertenencia, Then el predicado es verdadero.
- Given un trabajo y una cuenta de alumno que no es su dueño, When se resuelve la pertenencia, Then el predicado es falso, y el resultado **no distingue** ese caso del de un trabajo inexistente.
- Given una cuenta con papel `Administrador`, When se resuelve el acceso, Then el predicado que aplica es el del alcance del administrador y no éste, que es US-02022.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00003 |
| CU cubiertos | CU-02009 |
| RN e invariantes que ejerce | RN-02003; INV-02 |
| BT derivadas | BT-02012 |
| Etapa del producto | `e`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba unitaria del predicado en sus dos valores, y prueba de que la condición emitida es la misma para el trabajo ajeno y para el inexistente. |

## 5. Prioridad y estimación

`Must` por `RN-02003`, declarada cerrada en `PRODUCT-INTAKE` §4.1, y porque es criterio de la transición `e` → `f` del roadmap §5.2, verificado **forzando la petición** y no sólo por la interfaz.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Cita por identificador toda regla e invariante que ejerce, sin volver a enunciarla
- [x] Las condiciones de rechazo que produce existen en el catálogo de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Sus tareas técnicas están identificadas y ninguna está bloqueada

## 7. Notas y supuestos

El dominio **no ejecuta consultas**: la búsqueda del trabajo por identificador y su filtrado son del consumidor. Lo que se construye acá es el predicado que esa consulta aplica.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.3 previó con este mismo identificador y este mismo contenido. |
