# US-06017 — Derivar una contraseña sin guardarla ni registrarla en claro

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** US-06017-Derivar-Una-Contrasena-Sin-Guardarla-Ni-Registrarla-En-Claro.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-06002 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que la contraseña en claro se convierta en un valor derivado y no quede guardada ni registrada en ningún lado**, para **que el producto no conserve las contraseñas de nadie**.

## 2. Contexto

`02` §1 declara que acá viven **las dos piezas sensibles** del producto, y §4 que **éste es el único punto donde la contraseña en claro se convierte en el valor guardado**. `PRODUCT-INTAKE` §17.3.P.5 lo fija: derivación **nunca en claro ni con resumen simple**. El contrato de uso es [`CU-06006`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-06006-Derivar-La-Contrasena-Y-Verificar-Una-Credencial.md).

## 3. Criterios de aceptación

- Given una contraseña en claro, When se la deriva, Then el resultado es el valor derivado y **la contraseña en claro no se guarda**.
- Given esa derivación, When se inspeccionan los mensajes y las trazas, Then **0** contienen la contraseña en claro y **0** el valor derivado.
- Given el valor derivado, When se lo guarda, Then **los parámetros de derivación se versionan junto a él** y **no hay ningún valor por defecto silencioso**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00002 |
| CU cubiertos | CU-06006 |
| RN que ejerce | — directamente; sostiene la credencial que `RN-06013` y `RN-06016` gobiernan |
| Componente de `05` §3.1 | Mecanismo de credenciales |
| Reglas conceptuales de modelo | — |
| ¿Toma alguna decisión de negocio? | **No.** Decidir si una cuenta admite el acceso llega resuelto |
| ¿Toca el almacén? | **No.** Su prueba es unitaria |
| BT derivadas | BT-06003, BT-06013, BT-06022 |
| Tests previstos en 08 | Prueba de inspección de que ningún mensaje ni traza lleva la contraseña ni el valor derivado |

## 5. Prioridad y estimación

`Must` porque es una de las dos piezas que **el producto no puede permitirse mal hechas**, y porque la elección de la función de derivación es una decisión que el intake asigna a este proyecto de código y **no elige por él**.

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

**Cuál de las dos funciones candidatas se ancla es `PA-03`** de [`../Product-Backlog.md`](../Product-Backlog.md) §6, cerrado como trabajo en BT-06003, con caja temporal en la etapa `a`. La ADR correspondiente fija la **forma** —parámetros versionados, sin valor por defecto silencioso— y el **criterio de elección**; la elección concreta es del equipo.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
