# US-06015 — Responder si un correo está registrado y si ya existe una cuenta con papel `Administrador`

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** US-06015-Responder-Las-Dos-Preguntas-Sobre-El-Conjunto.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-06002 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **poder preguntar si un correo ya está registrado y si ya existe una cuenta con papel `Administrador`**, para **que las capas de adentro resuelvan sus precondiciones sin consultar conjuntos por su cuenta**.

## 2. Contexto

`Domain ADR-06006` declara que **el dominio no lee el conjunto de entidades**, y `05` §2.2 identifica esa decisión como el origen de las **dos** preguntas sobre el conjunto que este adaptador responde. El contrato de uso es [`CU-06005`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-06005-Guardar-Y-Recuperar-Las-Cuentas-De-La-Comision.md).

## 3. Criterios de aceptación

- Given un correo, When se pregunta si está registrado, Then la respuesta es un valor de verdad y **no expone la cuenta** que lo ocupa.
- Given el conjunto de cuentas, When se pregunta si existe una con papel `Administrador`, Then la respuesta es un valor de verdad.
- Given cualquiera de las dos preguntas, When se inspecciona lo que devuelven, Then **no devuelven la entidad**: son preguntas sobre el conjunto, que **ninguna entidad sola puede responder**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00001 |
| CU cubiertos | CU-06005 |
| RN que ejerce | RN-06001, RN-06002 |
| Componente de `05` §3.1 | Adaptador de repositorio de cuentas |
| Reglas conceptuales de modelo | — |
| ¿Toma alguna decisión de negocio? | **No.** Qué hacer con la respuesta lo decide la capa de aplicación |
| ¿Toca el almacén? | **Sí** |
| BT derivadas | BT-06009 |
| Tests previstos en 08 | Pruebas de integración sobre las dos preguntas |

## 5. Prioridad y estimación

`Must` porque sin estas dos respuestas ni el auto-registro ni la configuración del administrador pueden resolver sus precondiciones, y las dos son criterios de transición de las etapas `c` y `d`.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el componente de `05` §3.1 y, si toca el almacén, las reglas conceptuales de modelo que materializa
- [x] Declara que no toma ninguna decisión de negocio
- [x] Toda condición que produce existe en el catálogo de las 17 de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md), declarada como resultado o como fallo
- [x] Declara el camino en que el mecanismo se detiene en lugar de cumplir a medias, cuando puede fallar
- [x] Declara si toca el almacén y, en consecuencia, dónde vive su prueba

## 7. Notas y supuestos

**Estas dos preguntas y las dos restricciones de US-06014 son cosas distintas y las dos hacen falta.** La pregunta permite responder con un motivo legible; la restricción impide el resultado aunque la pregunta haya llegado tarde.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
