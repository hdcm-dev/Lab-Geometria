# US-08014 — Transportar el error neutro con el conjunto cerrado de diecisiete códigos

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** US-08014-Transportar-El-Error-Neutro-Con-El-Conjunto-Cerrado-De-Codigos.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-08002 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **extremo que compila contra el contrato (`GeometriaFactory-Api` y `GeometriaFactory-Web`)**, quiero **un **único** tipo de error, con cuatro campos y un conjunto cerrado de **diecisiete** códigos vivos**, para **que exista un solo lugar en el que un fallo pueda cruzar la frontera, y por lo tanto un solo lugar donde vigilarlo**.

## 2. Contexto

[`ADR-08002`](../../05-Arquitectura-Tecnica/Adrs/ADR-08002-Tipo-De-Error-Unico-Con-Conjunto-Cerrado.md) decide el tipo único con conjunto cerrado, y `05` §2.1 declara por qué se descartó un tipo de error por familia: multiplicaría por ocho los lugares donde se puede filtrar una dirección de servicio. `05` §7 fija los cuatro campos —código, texto neutro, colección de detalles de ubicación y momento— y los **diecisiete** códigos vivos sobre **veinte** identificadores emitidos.

## 3. Criterios de aceptación

- Given cualquier fallo que cruce la frontera, When se arma la respuesta, Then usa **el mismo** tipo de error, con sus cuatro campos.
- Given la inspección del conjunto de códigos, When se cuentan los vivos, Then son exactamente **diecisiete**, y **cero** códigos se producen fuera del conjunto.
- Given un identificador retirado —de los **tres** que hay—, When se busca reutilizarlo para otra condición, Then la regla de no reciclado lo impide: un consumidor viejo lo interpretaría con la causa anterior.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00002, NB-00004, NB-00008 |
| CU cubiertos | CU-08006 |
| Familia de tipos de `05` §3.1 | Familia de error |
| Restricciones transversales de `02` §6 | RT-02, RT-10 |
| RN que refiere por identificador | RN-08003, RN-08009, RN-08010, RN-08011, RN-08013, RN-08015 |
| BT derivadas | BT-08007, BT-08008 |
| Etapa del producto | `c`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba de inspección del conjunto cerrado, que `02` declara como criterio de aceptación de `CU-08006`, más la de superficie pública sobre los campos prohibidos. |

## 5. Prioridad y estimación

`Must` porque es transversal a los otros siete contratos de uso: sin el tipo de error, ninguno de ellos tiene camino de rechazo declarado.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un contrato de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Declara la familia de tipos de `05` §3.1 que la sostiene
- [x] Ninguna regla de negocio se redacta acá: las refiere por identificador a `GeometriaFactory-Domain`
- [x] Se refinó contra la regla de exposición de `05` §3.2 y ningún campo que introduce puede transportar una dirección de servicio, una ruta de datos ni un secreto

## 7. Notas y supuestos

**El texto del error es neutro y nunca lleva la dirección del servicio que falló** (`RA-03`, `PRODUCT-INTAKE` §17.4.P.5). `05` §9 declara que la forma habitual en que ese defecto entra es **agregando un campo de diagnóstico**, y que entra sin que nadie lo note porque compila.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 previó con este mismo identificador y esta misma pertenencia a necesidades de negocio. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. Se cierran con su fila, su desenlace y su fecha los puntos abiertos que estas decisiones resolvían. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **4**. Sube minor. |
