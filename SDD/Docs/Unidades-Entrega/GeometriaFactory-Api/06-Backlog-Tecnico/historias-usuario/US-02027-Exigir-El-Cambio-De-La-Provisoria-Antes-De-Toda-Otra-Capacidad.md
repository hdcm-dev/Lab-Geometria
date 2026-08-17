# US-02027 — Exigir el cambio de la contraseña provisoria antes de toda otra capacidad, y levantar la marca al cambiarla

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-02027-Exigir-El-Cambio-De-La-Provisoria-Antes-De-Toda-Otra-Capacidad.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-02003 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca de dominio (`GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`)**, quiero **que una cuenta con la marca de cambio pendiente no ejerza ninguna capacidad salvo cambiar su propia contraseña**, para **que una clave que el administrador conoce no quede sirviendo indefinidamente para operar como el alumno**.

## 2. Contexto

`RN-02013` declara que mientras la provisoria no se cambie la cuenta **se autentica pero no obtiene sesión de trabajo**, e `INV-09` lo expresa como condición permanente. El enunciado consolidado de `INV-09` en `PRODUCT-INTAKE` §17.1.P.2 · GeometriaFactory-Domain, desde su versión 1.14, declara que la marca la ponen únicamente el reseteo y la habilitación, y que la levanta únicamente el cambio efectivo hecho por la propia cuenta.

## 3. Criterios de aceptación

- Given una cuenta con la marca puesta, When se evalúa su admisibilidad para cualquier capacidad, Then no es admisible, con el motivo de cambio de contraseña pendiente.
- Given esa misma cuenta, When solicita reemplazar su credencial aportando la provisoria como vigente verificada, Then el reemplazo procede y la marca se levanta.
- Given una cuenta sin la marca, When se evalúa su admisibilidad, Then este motivo no aparece: la marca sólo la ponen el reseteo y la habilitación.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00002, NB-00001 |
| CU cubiertos | CU-02004, CU-02003 |
| RN e invariantes que ejerce | RN-02013, RN-02016; INV-09 |
| BT derivadas | BT-02011, BT-02014 |
| Etapa del producto | `d`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba unitaria del ciclo completo poner-marca, no-admisible, cambiar, admisible, con los dos orígenes de la marca. |

## 5. Prioridad y estimación

`Must` por `RN-02013`, que `PRODUCT-INTAKE` §4.1 declara con verificación propia, y porque es criterio de la transición `d` → `e` del roadmap §5.2.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Cita por identificador toda regla e invariante que ejerce, sin volver a enunciarla
- [x] Las condiciones de rechazo que produce existen en el catálogo de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Sus tareas técnicas están identificadas y ninguna está bloqueada

## 7. Notas y supuestos

**La lectura de `INV-09` que sostiene a `RN-02012` proviene de la columna del invariante y no de la prosa del intake, que dice lo contrario**; la ambigüedad está declarada en [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 y su consolidación es del Product Owner. Ninguna de las tres afirmaciones de esta historia depende de cuál lectura rija.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.3 previó con este mismo identificador y este mismo contenido. |
