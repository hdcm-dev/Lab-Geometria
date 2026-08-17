# US-00005 — Exigir el papel declarado por cada punto de acceso

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-00005-Exigir-El-Papel-Declarado-Por-Cada-Punto-De-Acceso.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master + API Product Owner (AG-06)
**Épica:** EP-00002 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Punto de acceso:** Los once puntos bajo la guardia
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **producto**, quiero **que cada punto de acceso declare qué papel exige y que la guardia lo verifique**, para **que un alumno no alcance una operación de administrador**, sin que eso reemplace la comprobación sobre el dato.

## 2. Contexto

`02` §4 precisión 1 lo enuncia: **exigir el papel no es autorizar**. El papel viaja en el acceso firmado y esta capa lo exige por punto; **la verificación de pertenencia y la de facultad se hacen sobre el dato recuperado y son de la capa de aplicación**, y duplicarlas acá **crearía un segundo lugar donde la regla puede decir otra cosa**. El contrato de uso es [`CU-00022`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00022-Ingresar-Al-Laboratorio-Y-Sostener-La-Sesion.md).

## 3. Criterios de aceptación

- Given un punto que exige `Administrador` y un acceso con papel `Alumno`, When se lo presenta, Then se rechaza antes de ejercer ningún caso de uso.
- Given ese mismo punto con el papel correcto, When se lo presenta, Then la petición se admite **y la capa de aplicación vuelve a comprobar sobre el dato**: el papel **no la exime**.
- Given los quince puntos, When se los recorre, Then **cada uno declara qué papel exige**, o declara que no exige ninguno.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00002 |
| CU cubiertos | CU-00002 |
| RN que ejerce | RN-00001, RN-00010 en su parte de exclusividad |
| Componente de `05` §3.1 | Guardia de admisión |
| ¿Decide qué se dice? | **No.** La facultad sobre el dato la decide `GeometriaFactory-Application` |
| Familia empobrecida | **No** |
| BT derivadas | BT-00011, BT-00012 |
| Tests previstos en 08 | Batería de integración con acceso de papel insuficiente sobre cada punto que exige `Administrador` |

## 5. Prioridad y estimación

`Must` por `RN-00001` y `RN-00010`, y porque es la parte de la admisión que hace que la superficie no ofrezca lo que la persona no puede ejercer.

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

**Que un punto exija `Administrador` no exime a la capa de adentro de comprobar.** Es la precisión que las dos categorías repiten porque es la más fácil de perder: el papel es una credencial, la pertenencia es un hecho sobre el dato, y ninguna reemplaza a la otra.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
