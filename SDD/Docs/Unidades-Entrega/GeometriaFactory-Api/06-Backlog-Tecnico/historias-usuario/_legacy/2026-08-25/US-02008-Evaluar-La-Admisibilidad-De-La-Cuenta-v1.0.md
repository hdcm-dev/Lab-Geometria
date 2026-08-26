> **Artefacto archivado — estado `Superado`**
>
> Copia archivada de `US-02008-Evaluar-La-Admisibilidad-De-La-Cuenta.md` en su versión **1.0**, tomada el 2026-08-25 por el orquestador de migración normativa **antes** de aplicar el corte de la categoría 06 de la fase M4 (`Master-Prompt.md` §5 y §8).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-25
> - **Versión vigente:** [`US-02008-Evaluar-La-Admisibilidad-De-La-Cuenta.md`](../../US-02008-Evaluar-La-Admisibilidad-De-La-Cuenta.md)
>
> El cuerpo que sigue **no se modifica**. Lo único que se tocó son **los enlaces relativos**, reescritos dos niveles para que sigan resolviendo desde esta ubicación.

---

# US-02008 — Evaluar la admisibilidad de la cuenta y devolver su motivo

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-02008-Evaluar-La-Admisibilidad-De-La-Cuenta.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-02002 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca de dominio (`GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`)**, quiero **una puerta única que responda si una cuenta admite acceso y, si no lo admite, con qué motivo**, para **que la misma condición no se compruebe de tres maneras distintas en tres lugares distintos, que es como se abre el agujero**.

## 2. Contexto

`RN-02006` declara que una cuenta `Pendiente` o `Bloqueado` no obtiene sesión, e `INV-06` lo expresa como condición permanente. [`ADR-02005`](../../../../05-Arquitectura-Tecnica/Adrs/ADR-02005-Guarda-Unica-De-Admisibilidad.md) decide que `INV-06` e `INV-09` se ejerzan **en un solo lugar** y no repetidos en cada operación; `05` §9 registra que la familia de defectos que la ausencia de esa puerta habilita ya se abrió con precedente documentado.

## 3. Criterios de aceptación

- Given una cuenta de alumno `Habilitado`, con credencial fijada y sin la marca de cambio pendiente, When se evalúa su admisibilidad, Then es admisible.
- Given una cuenta de alumno en estado `Pendiente` o en `Bloqueado`, When se evalúa su admisibilidad, Then no es admisible y el resultado trae el motivo, que es lo que el consumidor traduce hacia afuera.
- Given una cuenta con la marca de cambio de contraseña pendiente puesta, When se evalúa su admisibilidad, Then no es admisible por ese motivo, aunque su estado de cuenta sea `Habilitado`.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00001, NB-00002 |
| CU cubiertos | CU-02004 |
| RN e invariantes que ejerce | RN-02006, RN-02013, RN-02015, RN-02016; INV-06, INV-09 |
| BT derivadas | BT-02011 |
| Etapa del producto | `c`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba unitaria por cada motivo de no admisión y por el caso admisible, más la matriz de ejercicio de `INV-06` e `INV-09` (BT-02014). |

## 5. Prioridad y estimación

`Must` por `RN-02006`, declarada cerrada en `PRODUCT-INTAKE` §4.1, y porque es la puerta de la que dependen la sesión y el resto de las capacidades.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Cita por identificador toda regla e invariante que ejerce, sin volver a enunciarla
- [x] Las condiciones de rechazo que produce existen en el catálogo de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Sus tareas técnicas están identificadas y ninguna está bloqueada

## 7. Notas y supuestos

**El alcance efectivo de `INV-09` fuera de la admisibilidad queda abierto** ([`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../../02-Especificacion-Funcional/Especificacion-Funcional.md) §9): el dominio no tiene una puerta única por la que pasen todas las capacidades, y la guarda se concentra acá con el fundamento de que ninguna capacidad se ejerce sin admisión resuelta. Si la capa que expone habilitara un camino que no pase por acá, la marca tendría que volver a comprobarse ahí.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.3 previó con este mismo identificador y este mismo contenido. |
