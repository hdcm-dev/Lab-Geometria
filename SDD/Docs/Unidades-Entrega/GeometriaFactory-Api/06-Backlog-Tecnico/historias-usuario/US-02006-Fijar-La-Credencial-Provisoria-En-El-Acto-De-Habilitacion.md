# US-02006 — Fijar la credencial derivada provisoria en el acto de habilitación

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** US-02006-Fijar-La-Credencial-Provisoria-En-El-Acto-De-Habilitacion.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-02003 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca de dominio (`GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`)**, quiero **que la habilitación fije la credencial derivada provisoria y deje puesta la marca de cambio pendiente**, para **que no exista ninguna escritura anónima de contraseña y que el primer ingreso del alumno use el mismo mecanismo que el reseteo**.

## 2. Contexto

`RN-02016`, que el intake 1.13 incorporó, declara que habilitar produce una contraseña provisoria con el mismo tratamiento que la del reseteo y deja la cuenta con cambio de contraseña pendiente. Es lo que hizo que la fijación de la credencial dejara de ser un acto del alumno anónimo ([`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §9).

## 3. Criterios de aceptación

- Given una cuenta de alumno en estado `Pendiente` y una credencial ya derivada aportada por el consumidor, When el administrador la habilita, Then la cuenta queda `Habilitado`, con esa credencial y con la marca de cambio de contraseña pendiente puesta.
- Given una habilitación a la que **no** se le aporta credencial derivada, When se procesa, Then se rechaza con la condición que 03 declara para ese caso: no existe cuenta `Habilitado` sin credencial.
- Given una cuenta habilitada por este camino, When se evalúa su admisibilidad, Then no es admisible por cambio de contraseña pendiente, que es `INV-09` y se ejerce en US-02027.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00002, NB-00001 |
| CU cubiertos | CU-02003, CU-02002 |
| RN e invariantes que ejerce | RN-02016, RN-02014, RN-02006; INV-09 |
| BT derivadas | BT-02010, BT-02011 |
| Etapa del producto | `d`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba unitaria de la habilitación con y sin credencial aportada, y de la marca puesta como efecto. |

## 5. Prioridad y estimación

`Must` por derivar de `F-04`, `Must Have` en `PRODUCT-INTAKE` §4, y porque `RN-02016` es lo que cierra la escritura anónima de contraseña.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Cita por identificador toda regla e invariante que ejerce, sin volver a enunciarla
- [x] Las condiciones de rechazo que produce existen en el catálogo de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Sus tareas técnicas están identificadas y ninguna está bloqueada

## 7. Notas y supuestos

**La contraseña llega ya derivada** y el dominio no ve valores en claro, no deriva y no compara credenciales por su cuenta (`05` §7, `PRODUCT-INTAKE` §17.1.P.5). Que la provisoria no sea adivinable y no se repita es `RN-02014`, y **su mecanismo vive en `GeometriaFactory-Infrastructure`**, no acá.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.3 previó con este mismo identificador y este mismo contenido. |
