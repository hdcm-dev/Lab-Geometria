# US-15 — No exigir ni comprobar la situación de la cuenta al resetear

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** US-15-No-Exigir-Ni-Comprobar-La-Situacion-De-La-Cuenta-Al-Resetear.md
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

Como **producto**, quiero **que el punto de reseteo no declare ningún parámetro de situación y que su tabla de respuestas no tenga ninguna fila por cuenta no habilitada**, para **que el administrador resetee y habilite en el orden que quiera, sin acordarse de una secuencia**.

## 2. Contexto

`RN-15` declara que **resetear no exige que la cuenta esté habilitada**: procede sobre `Pendiente`, `Habilitado` y `Bloqueado`, porque **opera sobre la credencial y no es una transición de la máquina de estados de la cuenta**. `02` §6 declara que esta capa la ejerce **de forma estructural**. El contrato de uso es [`CU-05`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-05-Exponer-El-Reseteo-De-La-Contrasena-De-Un-Alumno.md).

## 3. Criterios de aceptación

- Given una cuenta `Bloqueado` y otra en estado `Pendiente`, When se las resetea, Then **el reseteo procede en las dos** y **ninguna cambia de situación**.
- Given el punto de acceso, When se inspeccionan sus parámetros, Then **no declara ninguno de situación**.
- Given su tabla de respuestas, When se la recorre, Then **no tiene ninguna fila por cuenta no habilitada**, porque esa causa **no existe**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-01 |
| CU cubiertos | CU-05 |
| RN que ejerce | **RN-15**, de forma estructural |
| Componente de `05` §3.1 | Superficie de gobierno de la comisión |
| ¿Decide qué se dice? | **No** |
| Familia empobrecida | **No** |
| BT derivadas | BT-17 |
| Tests previstos en 08 | Batería de integración sobre los dos estados |

## 5. Prioridad y estimación

`Must` por `RN-15`, y porque el criterio de transición `d` → `e` exige que el reseteo **proceda sobre `Bloqueado` y sobre `Pendiente` sin cambiarles la situación**, y que **no proceda sobre la cuenta de administrador**.

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

**Es una historia cuyo entregable es una ausencia en la superficie**: un parámetro que no existe y una fila de respuesta que no existe. Agregar cualquiera de los dos por prolijidad **rompería la regla**, y por eso el criterio se verifica sobre la superficie declarada y no sobre el comportamiento.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
