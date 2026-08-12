# US-16 — Cerrar el conjunto con el código no clasificado

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** US-16-Cerrar-El-Conjunto-Con-El-Codigo-No-Clasificado.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-02 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **extremo que compila contra el contrato (`GeometriaFactory-Api` y `GeometriaFactory-Web`)**, quiero **que exista un código de contrato para el fallo que no encaja en ningún otro**, para **que ningún fallo llegue a la persona sin representación en el contrato, ni siquiera cuando el servicio no responde**.

## 2. Contexto

`05` §4 declara que el código `CONTRATO_ERROR_NO_CLASIFICADO` **cierra el conjunto**: no hay camino por el que un fallo llegue a la persona sin representación en el contrato. `02` §4.1 declara además que ésta es la arista por la que este proyecto de código toca marginalmente a `NB-08`: es lo que hace que la indisponibilidad se presente como estado degradado explícito y sin revelar la dirección del servicio que falló.

## 3. Criterios de aceptación

- Given un fallo que no corresponde a ninguno de los otros dieciséis códigos vivos, When se arma la respuesta, Then usa el código no clasificado y no un código inventado.
- Given una indisponibilidad del servicio de datos, When la unidad pública la presenta, Then lo hace como estado degradado explícito, y el texto **no revela la dirección del servicio que falló**.
- Given la inspección del conjunto, When se cuenta este código entre los vivos, Then está incluido en los **diecisiete**: cerrar el conjunto no lo pone fuera de él.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-04, NB-08 |
| CU cubiertos | CU-06 |
| Familia de tipos de `05` §3.1 | Familia de error |
| Restricciones transversales de `02` §6 | RT-02 |
| RN que refiere por identificador | — |
| BT derivadas | BT-07, BT-08 |
| Etapa del producto | `c`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba de integración del camino de indisponibilidad y prueba de inspección de que ninguna capa produce un código fuera del conjunto. |

## 5. Prioridad y estimación

`Must` porque sin este código el conjunto no está cerrado, y un conjunto abierto habilita exactamente el fallo silencioso que el producto viene a eliminar.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un contrato de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Declara la familia de tipos de `05` §3.1 que la sostiene
- [x] Ninguna regla de negocio se redacta acá: las refiere por identificador a `GeometriaFactory-Domain`
- [x] Se refinó contra la regla de exposición de `05` §3.2 y ningún campo que introduce puede transportar una dirección de servicio, una ruta de datos ni un secreto

## 7. Notas y supuestos

**La presentación del estado degradado es de `GeometriaFactory-Web`** (`02` §4.2). Lo que este proyecto de código aporta es que exista un código con el que representarlo, y que ese código sea neutro.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 previó con este mismo identificador y esta misma pertenencia a necesidades de negocio. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **2**. Sube minor. |
