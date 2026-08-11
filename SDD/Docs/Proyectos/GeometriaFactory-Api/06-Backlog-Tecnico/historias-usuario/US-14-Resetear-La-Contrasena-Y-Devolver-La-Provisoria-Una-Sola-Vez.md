# US-14 — Resetear la contraseña de un alumno y devolver la provisoria **una sola vez**

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** US-14-Resetear-La-Contrasena-Y-Devolver-La-Provisoria-Una-Sola-Vez.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + API Product Owner (AG-06)
**Épica:** EP-03 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Punto de acceso:** `A-09`, bajo la guardia
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código de `GeometriaFactory-Web`**, quiero **exponer el reseteo de la contraseña de un alumno y recibir la provisoria una sola vez**, para **que el administrador se la comunique en el momento y no quede disponible para consultarla después**.

## 2. Contexto

`F-26` del intake §4 es `Must Have` y cierra un agujero que hacía inutilizable el laboratorio al primer olvido. `02` §5 declara este contrato de uso como **el punto que devuelve la provisoria una sola vez y no la registra**. El contrato de uso es [`CU-05`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-05-Exponer-El-Reseteo-De-La-Contrasena-De-Un-Alumno.md).

## 3. Criterios de aceptación

- Given un acceso con papel `Administrador` y una cuenta de alumno, When se pide el reseteo, Then la respuesta trae la provisoria.
- Given ese reseteo ya hecho, When se vuelve a consultar la cuenta por cualquier punto, Then **la provisoria no está**: se devuelve **una sola vez**.
- Given la cuenta de administrador, When se pide su reseteo, Then se rechaza: el reseteo está **acotado a cuentas de alumno**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-01, NB-02 |
| CU cubiertos | CU-05 |
| RN que ejerce | RN-01, RN-12, RN-14 en su parte de lo que **no** se hace con el valor; `INV-08` |
| Componente de `05` §3.1 | Superficie de gobierno de la comisión |
| ¿Decide qué se dice? | **No.** El valor llega **producido y derivado** desde `GeometriaFactory-Infrastructure` |
| Familia empobrecida | **No** |
| BT derivadas | BT-11, BT-17 |
| Tests previstos en 08 | Batería de integración, con la segunda consulta que ya no trae la provisoria |

## 5. Prioridad y estimación

`Must` por derivar de `F-26`, `Must Have`, y porque la transición `d` → `e` incorpora **cinco** criterios verificables del reseteo.

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

**`RN-14` no tiene tramo en esta capa** (`02` §6): el valor llega ya producido y ya derivado. Lo que esta historia sí declara es **lo que no se hace con él**, que es la parte que esta superficie puede romper.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
