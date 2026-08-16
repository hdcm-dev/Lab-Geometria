# US-00028 — Detener el arranque en lugar de atender peticiones sobre un almacén en el que no se puede confiar

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** US-00028-Detener-El-Arranque-En-Lugar-De-Atender-Sobre-Un-Almacen-Dudoso.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master + API Product Owner (AG-06)
**Épica:** EP-00001 Esqueleto ambulante y verificación de viabilidad
**Etapa del producto:** `a`
**Punto de acceso:** Ninguno: es el arranque
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **producto**, quiero **que el servicio no atienda ninguna petición si la preparación del almacén no se completó**, para **que nunca se sirvan datos en los que no se puede confiar**.

## 2. Contexto

`05` §4 declara el arranque en dos fases y su desenlace: si la preparación del almacén falla, **el arranque se detiene y ninguna petición se atiende**. `02` §8 declara por qué la composición se separó del arranque: **terminan distinto**, y ésta es una forma de terminación que **ninguna otra parte de esta capa tiene**. El contrato de uso es [`CU-00011`](../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-00011-Arrancar-El-Servicio-Y-Dejar-El-Almacen-En-Condiciones.md).

## 3. Criterios de aceptación

- Given una preparación del almacén que no se completa, When se intenta arrancar, Then **el arranque se detiene** y se atienden exactamente **0** peticiones.
- Given ese estado, When se consulta el punto de salud, Then **la respuesta permite distinguirlo de un servicio sano**.
- Given el arranque detenido, When se busca un modo degradado, Then **no hay modo de sólo lectura ni arranque parcial**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00003, NB-00008 |
| CU cubiertos | CU-00011 |
| RN que ejerce | — |
| Componente de `05` §3.1 | Arranque y salud |
| ¿Decide qué se dice? | **No.** La condición de la preparación llega de `GeometriaFactory-Infrastructure` |
| Familia empobrecida | **No** |
| BT derivadas | BT-00003 |
| Tests previstos en 08 | **Prueba de arranque fallido contra el punto de salud** |

## 5. Prioridad y estimación

`Must` porque `GeometriaFactory-Infrastructure` declara el fundamento en una línea —**un servicio que atiende sobre un almacén equivocado es peor que un servicio que no arranca**— y porque esta capa es la que puede convertir esa terminación en un servicio que igual escucha.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap, o declara que su caso de uso no traza a ninguna y por qué
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el punto de acceso que la realiza, o declara que no realiza ninguno, y el componente de `05` §3.1
- [x] Declara si su punto está bajo la guardia, y si no lo está, cuál de las cuatro ausencias declaradas es
- [x] Toda condición que transporta es uno de los diecisiete códigos vivos del contrato, con su destino declarado
- [x] Declara que no decide qué se dice
- [x] Declara si su respuesta pertenece a una de las tres familias deliberadamente empobrecidas

## 7. Notas y supuestos

**El punto de salud tiene que poder responder también en este caso**, y por eso **no exige acceso**: es una de las cuatro ausencias declaradas de la guardia, y su motivo propio es que **tiene que poder responder cuando nadie puede autenticarse**.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
