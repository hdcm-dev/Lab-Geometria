# US-02001 — Constituir un alumno con cuenta `Pendiente` y sin credencial

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-02001-Constituir-Un-Alumno-Con-Cuenta-Pendiente-Y-Sin-Credencial.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-02003 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca de dominio (`GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`)**, quiero **constituir un alumno con su correo, su nombre y su apellido, sin elegir contraseña**, para **que el alumno entre al laboratorio por su cuenta y quede esperando la habilitación del administrador, sin ningún canal de correo de por medio**.

## 2. Contexto

`NB-00002` pide que el alumno tenga identidad propia sin depender de un canal de correo, y la capacidad `F-02` del intake §4 la declara `Must Have`. El contrato de uso es [`CU-00021`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00021-Dar-De-Alta-Una-Cuenta-De-Alumno.md). Sin esta historia no hay ninguna cuenta de alumno sobre la que ejercer ninguna otra: es la primera del ciclo de vida.

## 3. Criterios de aceptación

- Given los datos de alta completos y un correo que el consumidor ya verificó como no usado, When se solicita constituir el alumno, Then la cuenta queda constituida con papel `Alumno`, en estado `Pendiente` y **sin credencial derivada**.
- Given una cuenta de alumno recién constituida, When se consulta su estado inicial, Then es `Pendiente` y nunca `Habilitado`, que es lo que `INV-08` fija para toda cuenta con papel `Alumno`.
- Given una solicitud de alta, When se procesa, Then la operación **no fija ninguna credencial**: la credencial provisoria la produce la habilitación, por `RN-02016`, y no el alta.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00002, NB-00001 |
| CU cubiertos | CU-02001 |
| RN e invariantes que ejerce | RN-02001, RN-02002; INV-01, INV-08 |
| BT derivadas | BT-02006, BT-02010 |
| Etapa del producto | `d`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Pruebas unitarias puras sobre el núcleo de entidades y las guardas de cuenta, sin dobles (`PRODUCT-INTAKE` §17.1.P.6 · GeometriaFactory-Domain). El caso concreto lo fija 08. |

## 5. Prioridad y estimación

`Must` porque su capacidad de origen, `F-02`, está declarada `Must Have` en `PRODUCT-INTAKE` §4, y porque toda la etapa `d` cuelga de que exista una cuenta de alumno.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Cita por identificador toda regla e invariante que ejerce, sin volver a enunciarla
- [x] Las condiciones de rechazo que produce existen en el catálogo de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Sus tareas técnicas están identificadas y ninguna está bloqueada

## 7. Notas y supuestos

La **unicidad efectiva** del correo sobre el conjunto de alumnos no se resuelve acá: el dominio no consulta conjuntos, y la unicidad se la aporta el consumidor (`05` §10.3, `INV-01`). Lo que esta historia sí exige es que el dato llegue con esa verificación hecha, y eso es US-02003.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.3 previó con este mismo identificador y este mismo contenido. |
