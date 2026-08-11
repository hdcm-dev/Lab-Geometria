# US-05 — Dar de baja una cuenta arrastrando sus trabajos en cualquier estado

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** US-05-Dar-De-Baja-Una-Cuenta-Arrastrando-Sus-Trabajos.md
**Versión:** 1.0
**Estado:** Propuesta
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-03 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca de dominio (`GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`)**, quiero **la baja de una cuenta con su confirmación escrita y con el arrastre de todos sus trabajos**, para **que la única operación destructiva del ciclo de vida no se dispare por accidente y que no queden trabajos huérfanos**.

## 2. Contexto

`RN-07` declara que la baja elimina la cuenta y **todos sus trabajos**, y que exige confirmación explícita escribiendo el correo de la cuenta. Es la regla que `F-26` vino a dejar de ser el remedio del olvido de contraseña: desde el intake 1.7, resetear conserva y dar de baja destruye, y los dos caminos no se confunden.

## 3. Criterios de aceptación

- Given una cuenta de alumno con trabajos en tres estados distintos y la confirmación escrita que coincide con su correo, When el administrador la da de baja, Then la cuenta y **todos** sus trabajos quedan alcanzados por el arrastre.
- Given la misma cuenta y una confirmación escrita que **no** coincide con su correo, When se intenta la baja, Then se rechaza y ni la cuenta ni ningún trabajo se tocan.
- Given la cuenta con papel `Administrador`, When se intenta darla de baja, Then se rechaza, porque `INV-08` declara que esa cuenta no admite baja.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-01 |
| CU cubiertos | CU-02 |
| RN e invariantes que ejerce | RN-07, RN-01; INV-08 |
| BT derivadas | BT-10, BT-12 |
| Etapa del producto | `d`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba unitaria de la confirmación en sus dos resultados y del alcance del arrastre sobre trabajos en estados distintos. |

## 5. Prioridad y estimación

`Must` por derivar de `F-03`, `Must Have` en `PRODUCT-INTAKE` §4, y por ser la única operación irreversible del ciclo de vida.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Cita por identificador toda regla e invariante que ejerce, sin volver a enunciarla
- [x] Las condiciones de rechazo que produce existen en el catálogo de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Sus tareas técnicas están identificadas y ninguna está bloqueada

## 7. Notas y supuestos

`RN-07` **no tiene invariante asociado** y es correcto: describe un comportamiento y no una condición permanente sobre el estado (`PRODUCT-INTAKE` §17.1.P.2). La eliminación efectiva de las filas es del consumidor; lo que el dominio declara es el alcance del arrastre.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.3 previó con este mismo identificador y este mismo contenido. |
