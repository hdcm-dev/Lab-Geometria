# US-00010 — Cambiar la contraseña propia exigiendo la vigente

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** US-00010-Cambiar-La-Contrasena-Propia-Exigiendo-La-Vigente.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master + API Product Owner (AG-06)
**Épica:** EP-00002 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Punto de acceso:** `A-05`, bajo la guardia
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código de `GeometriaFactory-Web`**, quiero **exponer el cambio de contraseña exigiendo la vigente**, para **que sólo el dueño de una cuenta pueda cambiar su propia clave**.

## 2. Contexto

`F-05` del intake §4 declara `Must Have` el cambio de contraseña exigiendo la actual. El contrato de uso es [`CU-00003`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00003-Exponer-El-Alta-De-Cuenta-Y-La-Credencial-Propia.md). `02` §8 declara por qué los cuatro puntos de alta y de credencial propia quedaron en un solo contrato de uso: **son los únicos que se ejercen sin acceso firmado o sin que el papel importe**.

## 3. Criterios de aceptación

- Given una sesión con acceso válido y la contraseña vigente correcta, When se pide el cambio, Then procede.
- Given una contraseña vigente equivocada, When se pide el cambio, Then se rechaza y **la contraseña no cambia**.
- Given el punto, When se inspecciona qué papel exige, Then **no exige ninguno en particular**: se ejerce sobre la propia cuenta, y el acceso identifica de quién es.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00002 |
| CU cubiertos | CU-00003 |
| RN que ejerce | RN-00013 en su excepción |
| Componente de `05` §3.1 | Superficie de acceso y credencial propia |
| ¿Decide qué se dice? | **No.** La verificación de la vigente es de `GeometriaFactory-Infrastructure` |
| Familia empobrecida | **No** |
| BT derivadas | BT-00011, BT-00016 |
| Tests previstos en 08 | Batería de integración con contraseña vigente correcta y equivocada |

## 5. Prioridad y estimación

`Must` por derivar de `F-05`, `Must Have`, y porque el criterio de transición `c` → `d` exige que el cambio funcione y **persista entre reinicios**.

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

**Es el mismo punto de acceso que US-00009**, y ésa es la consecuencia de `RN-00016`: un solo camino para los tres cursos. Lo que las distingue es qué se presenta como contraseña vigente y de dónde viene la persona.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
