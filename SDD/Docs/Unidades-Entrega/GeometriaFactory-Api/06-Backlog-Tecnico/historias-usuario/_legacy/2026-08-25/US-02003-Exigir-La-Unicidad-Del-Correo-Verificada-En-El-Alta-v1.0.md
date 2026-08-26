> **Artefacto archivado — estado `Superado`**
>
> Copia archivada de `US-02003-Exigir-La-Unicidad-Del-Correo-Verificada-En-El-Alta.md` en su versión **1.0**, tomada el 2026-08-25 por el orquestador de migración normativa **antes** de aplicar el corte de la categoría 06 de la fase M4 (`Master-Prompt.md` §5 y §8).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-25
> - **Versión vigente:** [`US-02003-Exigir-La-Unicidad-Del-Correo-Verificada-En-El-Alta.md`](../../US-02003-Exigir-La-Unicidad-Del-Correo-Verificada-En-El-Alta.md)
>
> El cuerpo que sigue **no se modifica**. Lo único que se tocó son **los enlaces relativos**, reescritos dos niveles para que sigan resolviendo desde esta ubicación.

---

# US-02003 — Exigir la unicidad del correo verificada en el alta

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-02003-Exigir-La-Unicidad-Del-Correo-Verificada-En-El-Alta.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-02003 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca de dominio (`GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`)**, quiero **que el alta exija que la unicidad del correo venga ya verificada y la rechace si no lo está**, para **que dos alumnos no puedan quedar con el mismo correo y que el dominio no tenga que consultar el conjunto para saberlo**.

## 2. Contexto

`RN-02002` declara que el correo del alumno es único, e `INV-01` lo expresa como condición permanente. El dominio **declara** la condición y la unicidad efectiva sobre el conjunto la resuelve el consumidor con su puerto de repositorio (`05` §10.3). [`ADR-02006`](../../../../05-Arquitectura-Tecnica/Adrs/ADR-02006-El-Dominio-No-Lee-El-Reloj-Ni-El-Conjunto.md) es la decisión que hace que la unicidad entre por parámetro.

## 3. Criterios de aceptación

- Given una solicitud de alta cuyo correo el consumidor declara como ya usado, When se procesa, Then la operación devuelve el rechazo por correo duplicado y no constituye la cuenta.
- Given una solicitud de alta que **no trae** la verificación de unicidad resuelta, When se procesa, Then la operación la rechaza en lugar de asumirla: el dominio no consulta el conjunto.
- Given una solicitud de alta con la unicidad verificada como libre, When se procesa, Then la cuenta se constituye y el correo queda conservado tal como llegó.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00002, NB-00001 |
| CU cubiertos | CU-02001 |
| RN e invariantes que ejerce | RN-02002; INV-01 |
| BT derivadas | BT-02009, BT-02016 |
| Etapa del producto | `d`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Pruebas unitarias con la unicidad aportada en sus dos valores; sin dobles, porque el dato entra por parámetro. |

## 5. Prioridad y estimación

`Must` por `RN-02002`, que `PRODUCT-INTAKE` §4.1 declara cerrada, y porque el correo es la identidad con la que el alumno entra al laboratorio.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Cita por identificador toda regla e invariante que ejerce, sin volver a enunciarla
- [x] Las condiciones de rechazo que produce existen en el catálogo de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../../../03-UX-UI-DX/DX-Error-Messages.md)
- [ ] Sus tareas técnicas están identificadas y ninguna está bloqueada

## 7. Notas y supuestos

**El criterio de comparación de dos correos queda abierto** —si se comparan tal cual o normalizados— y no se decide acá: es el punto abierto que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../../02-Especificacion-Funcional/Especificacion-Funcional.md) §9 declara, y este backlog lo convierte en trabajo como BT-02016.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.3 previó con este mismo identificador y este mismo contenido. |
