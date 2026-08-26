> **Artefacto archivado — estado `Superado`**
>
> Copia archivada de `US-00025-Responder-Sin-Exponer-Direcciones-Internas-Y-Registrar-En-El-Servidor.md` en su versión **1.1**, tomada el 2026-08-25 por el orquestador de migración normativa **antes** de aplicar el corte de la categoría 06 de la fase M4 (`Master-Prompt.md` §5 y §8).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.1
> - **Fecha de archivado:** 2026-08-25
> - **Versión vigente:** [`US-00025-Responder-Sin-Exponer-Direcciones-Internas-Y-Registrar-En-El-Servidor.md`](../../US-00025-Responder-Sin-Exponer-Direcciones-Internas-Y-Registrar-En-El-Servidor.md)
>
> El cuerpo que sigue **no se modifica**. Lo único que se tocó son **los enlaces relativos**, reescritos dos niveles para que sigan resolviendo desde esta ubicación.

---

# US-00025 — Responder sin exponer direcciones de servicios internos, y registrar del lado del servidor

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-00025-Responder-Sin-Exponer-Direcciones-Internas-Y-Registrar-En-El-Servidor.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master + API Product Owner (AG-06)
**Épica:** EP-00002 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Punto de acceso:** Ninguno propio: es transversal a los quince
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1)

## 1. Historia

Como **producto**, quiero **que ninguna respuesta de esta superficie lleve la dirección de un servicio interno, la ruta del almacén, la clave de firma o una traza, y que todo error quede registrado del lado del servidor**, para **no exponer la topología y a la vez poder diagnosticar**.

## 2. Contexto

`RA-03` es regla de nivel producto, y `05` §10.4 declara que **acá es donde se puede violar hacia afuera**: es **la última vez que un dato del backend es tocado antes de salir del servidor propio**. Su contracara obligatoria es el registro: **sin él, la prohibición de exponer se convierte en imposibilidad de diagnosticar**, y el operador que despliega a mano se queda sin nada que mirar. El contrato de uso es [`CU-00009`](../../../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-00009-Traducir-El-Motivo-Del-Contrato-A-Respuesta-De-Protocolo.md).

## 3. Criterios de aceptación

- Given las respuestas de fallo de los **quince** puntos, When se las inspecciona, Then **0** llevan dirección de servicio interno, ruta del almacén, clave de firma, contraseña, provisoria fuera del cuerpo del reseteo ni traza de implementación.
- Given cada uno de esos errores, When se mira el registro del servidor, Then quedó **registrado de forma estructurada**, junto con todo intento de acceso rechazado.
- Given el registro, When se lo inspecciona, Then **tampoco** contiene los secretos de la lista anterior.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00008 |
| CU cubiertos | CU-00009 |
| RN que ejerce | — directamente; ejerce `RA-03`, que es regla de arquitectura del producto |
| Componente de `05` §3.1 | Traductor de motivos y códigos |
| ¿Decide qué se dice? | **Decide cómo se dice** |
| Familia empobrecida | **No**, pero comparte con ellas el criterio de decir menos de lo que el servicio sabe |
| BT derivadas | BT-00013 |
| Tests previstos en 08 | **Prueba de inspección sobre las respuestas de fallo de los quince puntos y sobre el registro del servidor** |

## 5. Prioridad y estimación

`Must` porque `05` §10.4 declara que ésta es **la única de las siete Fases C del producto donde las tres reglas de arquitectura tienen tratamiento**, y `RA-03` es la que se viola hacia afuera desde acá.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1.

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

**`GeometriaFactory-Infrastructure` sostiene la misma pareja desde su lado** —cinco cosas que no entran en un mensaje ni en una traza, más el texto del alumno— y **es de disciplina y no de ignorancia**, porque esa capa **conoce** los secretos. Acá pasa lo mismo, un escalón más afuera.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
