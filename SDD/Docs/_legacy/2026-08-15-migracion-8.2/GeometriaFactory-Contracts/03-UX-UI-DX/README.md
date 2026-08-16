# 03 · UX / UI / DX — GeometriaFactory-Contracts

**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** README.md
**Versión:** 1.8
**Estado:** Aprobado
**Fecha:** 2026-08-12
**Autor:** DX Lead (AG-03)
**Variante:** DX
**Trazabilidad upstream:** `02-Especificacion-Funcional/` completo —`Especificacion-Funcional.md`, los **siete** contratos de uso `CU-08001` a `CU-08007` y `Glosario-Funcional.md`—; `00-Contexto/Vision-Producto.md` §9.1 y §9.2; `00-Contexto/Alcance-Producto.md` §2.2 y §8; `01-Necesidades-Negocio/Necesidades-Negocio.md` §2 y `NB-00009`; `PRODUCT-INTAKE` **1.14** §4, §4.1, §4.2, §16.1 y §17.4; `PRODUCT-MANIFEST` §5 (flags del proyecto de código)
**Trazabilidad downstream:** `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas`, `10-Examples` y `11-Documentacion` de este proyecto de código

---

## Tabla de contenido

- [1. Qué hay en esta carpeta](#1-qué-hay-en-esta-carpeta)
- [2. Variante aplicada y por qué](#2-variante-aplicada-y-por-qué)
- [3. Orden de lectura sugerido](#3-orden-de-lectura-sugerido)
- [4. Artefactos omitidos y su motivo](#4-artefactos-omitidos-y-su-motivo)
- [5. Criterios de aceptación no aplicables a la variante DX](#5-criterios-de-aceptación-no-aplicables-a-la-variante-dx)
- [6. Notas de uso de esta sección](#6-notas-de-uso-de-esta-sección)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Qué hay en esta carpeta

| Documento | Propósito | Estado |
| --- | --- | --- |
| [`DX-Developer-Experience.md`](DX-Developer-Experience.md) | Marco DX: rol de intervención, la regla de exposición, los tres tramos de onboarding, el quick-start dentro del contenedor de desarrollo, el plan de Diátaxis, las dos clases de error, las métricas y el lazo de retroalimentación. **Es el punto de entrada** | Propuesto |
| [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) | El recorrido de la primera hora, paso a paso y con hitos verificables. Es el modo tutorial | Propuesto |
| [`DX-Error-Messages.md`](DX-Error-Messages.md) | Catálogo de errores en sus dos clases —**quince** entradas de construcción del contrato y **diecisiete** códigos transportados—, más las tres señales declaradas que no son error, con diagnóstico accionable por entrada. Es el modo how-to para quien llega con un síntoma | Propuesto |
| [`Glosario-UX.md`](Glosario-UX.md) | Vocabulario que esta categoría acuña, la resolución de «error» con sus tres referentes y los términos que se referencian sin redefinir | Propuesto |
| `README.md` | Este archivo: índice navegable, variante aplicada, orden de lectura y declaración de las omisiones | Propuesto |

**Cero wireframes**, que es exactamente el mínimo que `Rules-UX-UI-DX.md` §2.2 fija para `library`.

## 2. Variante aplicada y por qué

**Variante DX**, leída de `Rules-UX-UI-DX.md` §1.2, fila `library`: el producto de este proyecto de código se consume por código, y el foco está en la superficie pública, en la documentación de cada tipo y en los ejemplos ejecutables. El flag `tiene_ui_final` es false, de modo que no hay una combinación con la variante UX/UI.

Lo que hace atípico a este proyecto de código, y que atraviesa los cuatro documentos: **no hay integradores externos**. Los dos únicos consumidores del contrato son `GeometriaFactory-Api` y `GeometriaFactory-Web`, del mismo producto y compilados contra el mismo ensamblado, y `redistribuible` es false. El destinatario real de esta documentación son el mantenedor futuro —la misma persona, meses después— y el agente de construcción por etapas. Eso no reduce el rigor: cambia a quién se le habla, y sobre todo cambia qué hace falta escribir, porque un agente sin memoria entre sesiones **no infiere una prohibición que no esté escrita**.

## 3. Orden de lectura sugerido

1. [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §1.2 — la regla de exposición. Son cinco líneas y es lo primero que hay que haber entendido antes de tocar el proyecto de código.
2. [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) — el recorrido completo, si lo que corresponde es empezar a trabajar hoy.
3. [`DX-Error-Messages.md`](DX-Error-Messages.md) §2.1 — la separación en dos clases de error. Conviene leerla temprano aunque no haya ningún síntoma: es lo que explica por qué la señal más valiosa de este proyecto de código aparece al compilar. Y §3.3, las tres señales declaradas que **no** son error, que es lo que más se malinterpreta de este contrato.
4. [`Glosario-UX.md`](Glosario-UX.md) — se puede leer suelto, y conviene abrirlo primero si el lector viene de otra categoría, por los tres referentes de «error».
5. El resto de `DX-Developer-Experience.md`, para el plan de Diátaxis, las métricas y el lazo de retroalimentación.

## 4. Artefactos omitidos y su motivo

`Rules-UX-UI-DX.md` §2.1 define trece artefactos posibles para esta categoría. Se emiten cinco y se omiten ocho, cada uno con el fundamento que la propia regla declara.

| Artefacto omitido | Regla que lo admite u obliga | Motivo de la omisión |
| --- | --- | --- |
| `Experiencia-De-Uso.md` | §2.1 lo marca obligatorio para tipos con UI final y expresamente **omitible para `library`** | `tiene_ui_final` es false. Este proyecto de código no tiene superficie visible: su actor es el código que compila contra el ensamblado, y las personas del producto aparecen en la categoría 03 de `GeometriaFactory-Web` |
| `wireframes-<superficie>.md` | §2.1 y §2.2: mínimo **0** para `library` | No hay pantallas que esquematizar. Dibujar una sería inventar una superficie que este proyecto de código no tiene |
| `representacion-<concepto>.md` | §2.1 lo condiciona a que exista una representación visual o estructural reutilizada | No hay ninguna. Las representaciones estructurales del producto —el árbol del texto, la escena del visor— pertenecen a `GeometriaFactory-Web` y a `GeometriaFactory-Visor` |
| `DX-Portal-Developers.md` | §2.1 lo marca obligatorio para `rest-api` con portal visible y recomendado para «library con portal hospedado» | `tiene_portal_developers` es false y **no hay integradores externos**. Un portal no tendría a quién servirle: la documentación vive en la propia cadena documental, con su plan de Diátaxis declarado en `DX-Developer-Experience.md` §4 |
| `DX-Operability.md` | §2.1 lo marca obligatorio para `worker-service` | El tipo D8 de este proyecto de código es `library`. No hay proceso que operar: el ensamblado se carga dentro de las dos piezas desplegables y no tiene ciclo de vida propio |
| `Linea-Base-Visual.md` | §2.1 y §1.5, condicionados a `requiere_maqueta` == true | `requiere_maqueta` es false. No hay Fase B2 de validación visual de maqueta para este proyecto de código |
| `Contrato-Datos-Maqueta.md` | Ídem | Ídem |
| `Bitacora-Validacion-Maqueta.md` | Ídem | Ídem |

## 5. Criterios de aceptación no aplicables a la variante DX

`Rules-UX-UI-DX.md` §6 mezcla criterios de las dos variantes. Los que corresponden a la variante UX/UI se declaran **no aplicables con su motivo**, en lugar de darse por cumplidos.

| Criterio de §6 | Estado | Motivo |
| --- | --- | --- |
| Existe `Experiencia-De-Uso.md` con las once secciones del §4.2 | No aplicable | Sólo obligatorio para tipos con UI final; `tiene_ui_final` es false. Ver §4 |
| Existe al menos un `wireframes-<superficie>.md` por superficie clave, con las nueve secciones del §4.2.1 | No aplicable | El mínimo de wireframes para `library` es 0 |
| Cada wireframe enumera al menos los estados vacío, cargando, con datos y error | No aplicable | No hay wireframes. La enumeración equivalente en variante DX son las dos clases de error del catálogo, que sí está cubierta |
| Toda accesibilidad declarada toma WCAG 2.2 nivel AA como piso mínimo | No aplicable | Esta sección **no declara ninguna accesibilidad**, porque no hay superficie que un lector de pantalla o un teclado recorran. El compromiso WCAG 2.2 AA del producto rige en la categoría 03 de `GeometriaFactory-Web`, que es donde hay pantallas |
| En proyectos de código con `requiere_maqueta` == true: nombre canónico de superficie y estados de la maqueta | No aplicable | `requiere_maqueta` es false |
| En proyectos de código con `requiere_maqueta` == true y maqueta aprobada: los tres artefactos de la Fase B2 | No aplicable | `requiere_maqueta` es false |

Los demás criterios de §6 aplican y se cumplen: variante declarada en cada cabecera, `DX-Developer-Experience.md` con las nueve secciones del §4.2.3 incluyendo Diátaxis y tramos verificables de 5, 30 y 60 minutos, quick-start reproducible en cada documento DX, trazabilidad upstream y downstream por artefacto, nomenclatura sin sufijo de versión en el nombre, un solo archivo por nombre lógico, glosario existente y no vacío, no duplicación con `Glosario-Funcional.md`, polisemia declarada sólo donde los contextos colisionan, y tabla de contenido en los cinco documentos.

## 6. Notas de uso de esta sección

- **Autoridad.** Esta sección no origina ninguna capacidad, prioridad ni exclusión. Todo se deriva de los ocho contratos de uso de `02-Especificacion-Funcional/`, de las once restricciones transversales `RT-01` a `RT-11` y del `PRODUCT-INTAKE` 1.7, y traza a su sección de origen.
- **Fronteras.** La arquitectura y los ADR son de `05-Arquitectura-Tecnica`; las historias de usuario, de `06-Backlog-Tecnico`; el pipeline y la automatización de los quality gates, de `09`; los samples y sus contratos de verificación, de `10-Examples`; el cuerpo documental de entrega, de `11-Documentacion`. Esta sección refiere y delega, no redacta.
- **Samples.** `PRODUCT-INTAKE` §16.1 declara que este proyecto de código **no produce samples propios**, porque no lo consumen integradores externos: su verificación vive en `tests/`. Por eso el quick-start no incluye un fragmento que instancie un tipo de transferencia, y lo declara explícitamente en lugar de dejar el hueco sin explicar.
- **Entorno.** Ningún paso de esta documentación asume herramientas en el host de desarrollo. El ciclo entero ocurre dentro del contenedor de desarrollo, y esa restricción es del producto, no una preferencia de esta categoría.
- **Vocabulario.** Los términos del dominio están en `Vision-Producto.md` §9 y los del contrato en `02-Especificacion-Funcional/Glosario-Funcional.md`; acá no se redefinen. «Contrato» tiene tres referentes, «pieza» dos y `Pendiente` dos, resueltos aguas arriba —`Pendiente` va **siempre calificado**, salvo en las enumeraciones del conjunto cerrado y en los identificadores literales de código—; «error» tiene tres referentes y se resuelve en [`Glosario-UX.md`](Glosario-UX.md) §3.1. La palabra «proyecto» a secas no se usa, «solución» a secas tampoco, y el **comentario** del administrador no se nombra como observación ni como calificación.
- **Nombres de archivo.** Ningún archivo vivo de esta carpeta lleva sufijo de versión: cada uno declara su versión en el campo `Versión` de su cabecera. Las versiones superadas viven en `_legacy/2026-08-09/` con el sufijo `-v1.0.md`, archivadas al absorber el circuito de revisión del administrador.

## 7. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial del índice de la sección. Enumera los cuatro documentos emitidos más este README, declara la variante DX con su fundamento y el destinatario real de la documentación, fija el orden de lectura, declara las ocho omisiones con la regla que las admite y su motivo, declara los seis criterios de §6 que son de la variante UX/UI como no aplicables, y las notas de autoridad, fronteras, samples, entorno, vocabulario y nombres de archivo. | DX Lead (AG-03) |
| 1.1 | 2026-08-09 | **Actualización por contenido nuevo aguas arriba**: `PRODUCT-INTAKE` 1.3 incorporó el circuito de revisión del administrador, 00 y 01 pasaron a 1.1 con `NB-00009`, y la categoría 02 emitió `CU-08007`, `RT-08` y `RT-09`. Sube minor y archiva el estado anterior por `Master-Prompt.md` §5. Cambian los conteos y las notas, no la estructura: §1 describe el catálogo con sus doce entradas de construcción, catorce códigos transportados y tres señales; §3 suma §3.3 al orden de lectura; §6 pasa a siete contratos de uso y nueve restricciones, declara la forma calificada de `Pendiente` con sus dos excepciones y la regla de que el comentario no se nombra como observación ni como calificación, y registra el archivado en `_legacy/2026-08-09/`. **Las ocho omisiones y los seis criterios no aplicables de §4 y §5 no cambian**: el circuito de revisión no altera ningún flag del proyecto de código, que sigue siendo `library` con `tiene_ui_final`, `tiene_portal_developers` y `requiere_maqueta` en false. | DX Lead (AG-03) |
| 1.2 | 2026-08-09 | **Actualización por contenido nuevo aguas arriba**: `PRODUCT-INTAKE` **1.7** incorpora la capacidad **F-26** y la categoría 02 emite **CU-08008** y las restricciones transversales `RT-10` y `RT-11`. §2 actualiza el catálogo de errores a **catorce** entradas de construcción y **dieciséis** códigos transportados, con las tres señales declaradas sin cambio; §5 actualiza la nota de autoridad a ocho contratos de uso, once restricciones transversales y el intake 1.7. Ningún artefacto se agrega ni se omite. | DX Lead (AG-03) |
| 1.3 | 2026-08-09 | **Actualización por las dos decisiones del Product Owner sobre F-26** que `CU-08008` 1.2 y `CU-08006` 1.3 absorben: **resetear no exige que la cuenta esté habilitada**, y **la contraseña provisoria la produce el sistema y no la escribe el administrador**. §2 actualiza el catálogo de errores a **quince** entradas de construcción y **diecisiete** códigos transportados, con las tres señales declaradas sin cambio. Ningún artefacto se agrega ni se omite. | DX Lead (AG-03) |
| 1.4 | 2026-08-09 | **Cierra la parte del hallazgo `F26-28`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0 que alcanza a este archivo. El informe lo registra sobre `DX-Developer-Experience.md` y `DX-Error-Messages.md`; **este archivo tiene el mismo defecto** y se corrige en la misma pasada para que la carpeta quede pareja. **Cierra la parte del hallazgo `F26-28`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0 que alcanza a este archivo: las filas de este control de cambios estaban **fuera de orden cronológico** (1.1, 1.0, 1.2, 1.3) y se reordenan por versión, **sin tocar el texto de ninguna**. **Ningún artefacto, recuento ni omisión declarada de esta sección cambia.** Sube minor: repara el orden de esta tabla sin alterar lo que sus filas dicen. | DX Lead (AG-03) |
| 1.5 | 2026-08-10 | **Actualización por `PRODUCT-INTAKE` 1.13 §4.1 (RN-08016)** y la precisión de **F-04**, que `CU-08002` 1.4, `CU-08001` 1.5, `CU-08008` 1.4 y `CU-08006` 1.6 absorben: habilitar una cuenta produce su contraseña provisoria y el ensamblado pierde el único tipo que expresaba una escritura anónima de credencial. §2 actualiza el catálogo de errores a **quince** entradas de construcción y **quince** códigos transportados, con las tres señales declaradas sin cambio. Ningún artefacto se agrega ni se omite. | DX Lead (AG-03) |
| 1.6 | 2026-08-10 | **Cierra el hallazgo `C-08` (P2) del informe de auditoría `SDD/Docs/Audit/Coherencia-Corpus-r1.md` 1.0.** La cabecera de trazabilidad declaraba derivarse del `PRODUCT-INTAKE` **1.3**, versión archivada, y pasa a declarar la **1.14**, vigente. Entre la **1.3** y la **1.14** el intake atravesó once emisiones, entre ellas las que incorporaron **F-25**, **F-26** y las reglas **RN-08012** a **RN-08016**: una cabecera que declaraba 1.3 declaraba derivarse de un intake que no conocía ni el reseteo ni la habilitación con contraseña provisoria. Se revisó el cuerpo antes de mover la cabecera y **no arrastra ninguna decisión de las versiones intermedias**: no queda en él ningún recuento de «quince reglas» ni de «diecisiete códigos», ninguna cita a la exclusión **X-2** como vigente y ninguna afirmación de que la marca de cambio de contraseña pendiente la ponga únicamente el reseteo. **Ningún contenido normativo de este documento cambia: la corrección es de trazabilidad.** Sube minor. | DX Lead (AG-03) |
| 1.7 | 2026-08-10 | **Absorbe la corrección de `PRODUCT-INTAKE` 1.15 §4.1 (RN-08016)**: lo que la regla elimina es la escritura anónima **de credencial**, y no toda escritura anónima —el **registro de cuenta** es anónimo por diseño y su solicitud sigue siendo un tipo anónimo del ensamblado—. Único cambio acá: **la fila 1.5** de este control de cambios pasa a decir «de credencial». Ningún artefacto se agrega ni se omite y el catálogo de quince entradas no cambia. | DX Lead (AG-03) |
| 1.8 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. | Orquestador SDD |
