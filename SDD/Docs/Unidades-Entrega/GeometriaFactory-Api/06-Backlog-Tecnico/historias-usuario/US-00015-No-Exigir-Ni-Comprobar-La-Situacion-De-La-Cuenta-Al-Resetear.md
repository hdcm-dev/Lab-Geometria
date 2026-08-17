# US-00015 — No exigir ni comprobar la situación de la cuenta al resetear

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-00015-No-Exigir-Ni-Comprobar-La-Situacion-De-La-Cuenta-Al-Resetear.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master + API Product Owner (AG-06)
**Épica:** EP-00003 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Punto de acceso:** `A-09`, bajo la guardia
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **producto**, quiero **que el punto de reseteo no declare ningún parámetro de situación y que su tabla de respuestas no tenga ninguna fila por cuenta no habilitada**, para **que el administrador resetee y habilite en el orden que quiera, sin acordarse de una secuencia**.

## 2. Contexto

`RN-00015` declara que **resetear no exige que la cuenta esté habilitada**: procede sobre `Pendiente`, `Habilitado` y `Bloqueado`, porque **opera sobre la credencial y no es una transición de la máquina de estados de la cuenta**. `02` §6 declara que esta capa la ejerce **de forma estructural**. El contrato de uso es [`CU-00024`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00024-Resetear-La-Contrasena-De-Un-Alumno.md).

## 3. Criterios de aceptación

- Given una cuenta `Bloqueado` y otra en estado `Pendiente`, When se las resetea, Then **el reseteo procede en las dos** y **ninguna cambia de situación**.
- Given el punto de acceso, When se inspeccionan sus parámetros, Then **no declara ninguno de situación**.
- Given su tabla de respuestas, When se la recorre, Then **no tiene ninguna fila por cuenta no habilitada**, porque esa causa **no existe**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00001 |
| CU cubiertos | CU-00005 |
| RN que ejerce | **RN-00015**, de forma estructural |
| Componente de `05` §3.1 | Superficie de gobierno de la comisión |
| ¿Decide qué se dice? | **No** |
| Familia empobrecida | **No** |
| BT derivadas | BT-00017 |
| Tests previstos en 08 | Batería de integración sobre los dos estados |

## 5. Prioridad y estimación

`Must` por `RN-00015`, y porque el criterio de transición `d` → `e` exige que el reseteo **proceda sobre `Bloqueado` y sobre `Pendiente` sin cambiarles la situación**, y que **no proceda sobre la cuenta de administrador**.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el punto de acceso que la realiza y el componente de `05` §3.1 que lo aloja
- [x] Declara si su punto está bajo la guardia, y si no lo está, cuál de las cuatro ausencias declaradas es
- [x] Toda condición que transporta es uno de los diecisiete códigos vivos del contrato, con su destino declarado
- [x] Declara que no decide qué se dice
- [x] Declara si su respuesta pertenece a una de las tres familias deliberadamente empobrecidas

## 7. Notas y supuestos

**Es una historia cuyo entregable es una ausencia en la superficie**: un parámetro que no existe y una fila de respuesta que no existe. Agregar cualquiera de los dos por prolijidad **rompería la regla**, y por eso el criterio se verifica sobre la superficie declarada y no sobre el comportamiento.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
