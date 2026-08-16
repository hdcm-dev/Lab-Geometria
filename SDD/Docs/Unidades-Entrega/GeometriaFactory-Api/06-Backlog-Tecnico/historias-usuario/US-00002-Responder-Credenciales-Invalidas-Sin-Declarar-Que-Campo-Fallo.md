# US-00002 — Responder credenciales inválidas **sin declarar cuál de los dos campos falló**

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** US-00002-Responder-Credenciales-Invalidas-Sin-Declarar-Que-Campo-Fallo.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master + API Product Owner (AG-06)
**Épica:** EP-00002 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Punto de acceso:** `A-01`, fuera de la guardia
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **producto**, quiero **que el rechazo por credenciales inválidas no diga si falló el correo o la contraseña**, para **que la superficie de ingreso no sirva para averiguar qué correos existen**.

## 2. Contexto

`PRODUCT-INTAKE` §17.1.P.5 · GeometriaFactory-Api declara la respuesta **genérica ante credenciales inválidas, sin revelar cuál campo falló**. `05` §7 la cataloga como una de las **tres familias deliberadamente empobrecidas**: **tres respuestas dicen menos de lo que el servicio sabe, y en las tres es la decisión y no el defecto**. El contrato de uso es [`CU-00022`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00022-Ingresar-Al-Laboratorio-Y-Sostener-La-Sesion.md).

## 3. Criterios de aceptación

- Given un correo que no existe, When se intenta el canje, Then la respuesta es genérica y no declara qué falló.
- Given un correo que existe con una contraseña equivocada, When se intenta el canje, Then la respuesta es **indistinguible** de la anterior, en cuerpo y en código.
- Given las dos respuestas, When se las compara, Then son idénticas: es una de las **3 de 3** comparaciones que la prueba de familias empobrecidas verifica.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00002 |
| CU cubiertos | CU-00001 |
| RN que ejerce | — directamente; protege la superficie de identidad |
| Componente de `05` §3.1 | Superficie de acceso y credencial propia, Traductor de motivos y códigos |
| ¿Decide qué se dice? | **Decide cómo se dice, que es lo propio de esta capa**: el veredicto llega resuelto y acá se elige la respuesta |
| Familia empobrecida | **Sí**, es la segunda de las tres |
| BT derivadas | BT-00013, BT-00014, BT-00016 |
| Tests previstos en 08 | Prueba que compara las dos respuestas y verifica que son indistinguibles |

## 5. Prioridad y estimación

`Must` porque el intake lo declara como criterio de la superficie y porque, sin esta historia, el punto de canje se vuelve un modo de enumerar cuentas.

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

**Convive con US-00003, que dice lo contrario y también es correcto**: la respuesta por **situación de la cuenta** sí declara el motivo, porque la persona necesita saber si tiene que esperar la habilitación. Las dos viven en el mismo punto de acceso y la distinción es deliberada.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
