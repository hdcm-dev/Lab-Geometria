# US-06 — Aplicar la guardia del cambio de contraseña pendiente a todos los puntos salvo uno

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** US-06-Aplicar-La-Guardia-Del-Cambio-Pendiente-A-Todos-Los-Puntos-Salvo-Uno.md
**Versión:** 1.0
**Estado:** Propuesta
**Fecha:** 2026-08-10
**Autor:** Scrum Master + API Product Owner (AG-06)
**Épica:** EP-03 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Punto de acceso:** Los once puntos bajo la guardia, con `A-05` como **única excepción**
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **producto**, quiero **que ningún punto de acceso quede fuera de la guardia del cambio de contraseña pendiente, salvo el cambio de la propia contraseña**, para **que una cuenta con la marca puesta no ejerza ninguna capacidad**.

## 2. Contexto

`RN-13` e `INV-09` lo exigen. `02` §6 declara que ésta es **una de las dos reglas que esta capa puede romper sola**: **un punto nuevo que quede fuera de la guardia la rompe sin que nada falle**. `05` §9 le asigna probabilidad **alta** e impacto **muy alto**, y agrega el diagnóstico: **los defectos de omisión no se ven leyendo el punto nuevo**. El contrato de uso es [`CU-02`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-02-Admitir-La-Peticion-Acceso-Papel-Y-Marca.md).

## 3. Criterios de aceptación

- Given una cuenta con la marca puesta, When pide **cualquiera** de los once puntos bajo la guardia salvo el cambio de su propia contraseña, Then se rechaza.
- Given esa misma cuenta, When pide el cambio de su propia contraseña, Then se admite: es la **única excepción declarada**.
- Given los quince puntos, When se los recorre **en las dos direcciones** contra la lista de la guardia, Then los que quedan fuera son exactamente **4**, **ni uno más**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-02 |
| CU cubiertos | CU-02 |
| RN que ejerce | **RN-13**, con tramo transversal acá; `INV-09` |
| Componente de `05` §3.1 | Guardia de admisión |
| ¿Decide qué se dice? | **No.** La comprobación es de `GeometriaFactory-Application`; **que ningún punto quede fuera de ella es de acá** |
| Familia empobrecida | **No** |
| BT derivadas | BT-11, BT-12 |
| Tests previstos en 08 | **Prueba de inspección que recorre los quince puntos y compara contra la lista, en las dos direcciones** |

## 5. Prioridad y estimación

`Must` por el riesgo de impacto **muy alto** de `05` §9, y porque `05` §10.3 llama a esta garantía **el aporte más consecuente de esta capa** a `INV-09`.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el punto de acceso que la realiza y el componente de `05` §3.1 que lo aloja
- [x] Declara si su punto está bajo la guardia, y si no lo está, cuál de las cuatro ausencias declaradas es
- [x] Toda condición que transporta es uno de los quince códigos vivos del contrato, con su destino declarado
- [x] Declara que no decide qué se dice
- [x] Declara si su respuesta pertenece a una de las tres familias deliberadamente empobrecidas

## 7. Notas y supuestos

**Esta historia vive en la etapa `d` aunque la guardia se construya en la `c`**, y el motivo es que **hasta la `d` no existe la marca** sobre la que decidir: la produce la habilitación, por `RN-16`. `GeometriaFactory-Web` declara la misma dependencia sobre su cuarto guardián.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
