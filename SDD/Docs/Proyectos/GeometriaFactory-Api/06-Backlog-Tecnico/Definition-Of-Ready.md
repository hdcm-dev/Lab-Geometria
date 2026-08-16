# Definition of Ready — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** Definition-Of-Ready.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-12
**Autor:** Scrum Master + API Product Owner (AG-06)
**Trazabilidad upstream:** [`Product-Backlog.md`](Product-Backlog.md) 1.0 §5; [`Backlog-Tecnico.md`](Backlog-Tecnico.md) 1.0 §3; [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) 1.3 §3, §4 y §6; [`../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md`](../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md); [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md) 1.3; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.1 §3.1, §3.4, §8 y §9; [`../05-Arquitectura-Tecnica/Contratos-REST.md`](../05-Arquitectura-Tecnica/Contratos-REST.md); [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) 1.5 §5.1
**Trazabilidad downstream:** `07-Plan-Sprint` de GeometriaFactory-Api

---

## Tabla de contenido

- [1. Criterios DoR para historias de usuario](#1-criterios-dor-para-historias-de-usuario)
- [2. Criterios DoR para tareas técnicas](#2-criterios-dor-para-tareas-técnicas)
- [3. Excepciones admitidas](#3-excepciones-admitidas)
- [4. Aprobador](#4-aprobador)
- [5. Qué no es esta DoR](#5-qué-no-es-esta-dor)
- [6. Control de cambios](#6-control-de-cambios)

---

## 1. Criterios DoR para historias de usuario

Ocho criterios, todos respondibles con sí o no. Los cuatro últimos son propios de este proyecto de código.

1. **Traza a un caso de uso.** La historia declara al menos un `CU-XX` de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.
2. **Declara su necesidad de negocio y su etapa del producto.** Con **dos excepciones declaradas**: las historias de `CU-00010` y de `CU-00012`, porque `02` §7.2 declara que esos dos casos de uso **no trazan a ninguna necesidad** y explica por qué.
3. **Tiene criterios de aceptación en Given/When/Then, con al menos dos escenarios**, uno de camino feliz y uno de borde.
4. **Declara el punto de acceso que la realiza**, de los **quince** de `Definicion-Superficie-HTTP.md` §3, o declara que no realiza ninguno; y el componente de `05` §3.1 que lo aloja.
5. **Declara si su punto está bajo la guardia.** Si no lo está, declara **cuál de las cuatro ausencias declaradas** es y por qué. Una historia que agregue un punto y no diga nada de la guardia **no está lista**: es el defecto de omisión que rompe `RN-00013` **sin que nada falle**.
6. **Toda condición que la historia transporta es uno de los diecisiete códigos vivos** del conjunto cerrado de `GeometriaFactory-Contracts`, con su destino declarado en la tabla de traducción. Una historia que necesite un código nuevo **no está lista**: los códigos **no se acuñan acá**, y donde el conjunto no tiene código **se usa el genérico y se declara el hueco**.
7. **Declara que no decide qué se dice.** Una historia que decida un estado, una admisibilidad, una pertenencia sobre el dato o qué campos cruzan la frontera **está mal ubicada**: `02` §4 lo enuncia en una línea.
8. **Si su respuesta pertenece a una de las tres familias deliberadamente empobrecidas, lo declara.** Son tres —credenciales inválidas sin declarar qué campo falló, recurso que no se ve sin distinguir inexistente de ajeno de fuera de alcance, y correo ya registrado sin declarar situación ni papel—, y en las tres **es la decisión y no el defecto**.

## 2. Criterios DoR para tareas técnicas

Seis criterios, todos respondibles con sí o no.

1. **Declara su fuente upstream por identificador**: un componente de `05` §3.1, una ADR, un NFR de su §8, un riesgo de su §9, un punto abierto de su §11, un punto de acceso de la superficie de 02 o una regla de delivery del intake §15.
2. **Declara al menos una historia consumidora**, o se justifica como infraestructura compartida citando la ADR, la puerta o el punto abierto que la sostiene.
3. **Sus criterios de aceptación son verificables**, y cuando la propiedad que sostienen es una **ausencia** —cero puntos fuera de la guardia de más, cero códigos inventados, cero respuestas que exponen, cero eliminaciones fuera de alcance aceptadas, cero truncamientos— el criterio se expresa **con umbral cero y con la condición en que se mide**.
4. **Si la tarea toca la superficie, declara que se compara contra una lista en las dos direcciones.** El defecto característico de esta capa es de **omisión**, y `05` §9 le asigna probabilidad **alta**: no se detecta leyendo el punto nuevo.
5. **Sus dependencias están declaradas y ninguna es circular**, y ninguna cruza la regla de `05` §3.2: **ninguna superficie depende de otra superficie**, **el traductor está después de las cinco** y **la composición de raíz no atiende peticiones**.
6. **Si es de tipo indagación, tiene caja temporal expresada en etapas o en el punto de control que la cierra**, nunca en horas; y si la decisión **obliga a otro proyecto de código o pertenece a otro**, lo declara. BT-00008 obliga a `GeometriaFactory-Web`; BT-00026 la mide `09-Devops`; BT-00015 y BT-00021 elevan al Product Owner.

## 3. Excepciones admitidas

| Caso | Qué se flexibiliza | Quién lo aprueba |
| --- | --- | --- |
| Historias de `CU-00010` y de `CU-00012` | El criterio 2 de §1 se cumple **declarando que no trazan a ninguna necesidad y por qué**: conectar un puerto con su adaptador es construcción, y la colección de peticiones **no implementa nada, demuestra**. Inventarles una traza haría creer que hay una necesidad detrás de una decisión de estructura | El Product Owner |
| Historia de `CU-00012` | El criterio 4 de §1 se cumple **declarando que no realiza ningún punto de acceso**: `05` §3.3 declara que es el único de los doce casos de uso **sin componente**, porque es un artefacto del árbol de muestras y no código de producción | El Product Owner |
| Tarea de indagación que cierra o eleva un punto abierto de `05` §11 | El criterio 3 de §2 puede cumplirse con el **resultado esperado** en lugar de con un criterio verificable de antemano | El Product Owner, en el punto de control de la etapa que la contiene |
| Historia cuya verificación depende de uno de los **cinco** valores rotulados **[ASUNCIÓN]** de `05` §8 | El criterio 3 de §1 se cumple con el valor **vigente pero declarado como asunción**, hasta que `PA-10` del backlog se cierre con BT-00025. **No habilita a inventar otro número** | El Product Owner, o 08 al fijar su guion de medición |
| Historia que agrega un punto de acceso | Ninguno: **no se admite excepción al criterio 5 ni al 6**. Un punto fuera de la guardia rompe `RN-00013` sin que nada falle, y un código inventado rompe el conjunto cerrado del ensamblado de contratos, que **dos extremos compilan juntos** | — |
| Historia que traduce la negativa de pertenencia | Ninguno: **no se admite excepción al criterio 8**. Responder «no autorizado» donde corresponde «no encontrado» **confirma la existencia de un recurso ajeno**, permite averiguar por tanteo qué identificadores existen y **ninguna capa de adentro puede repararlo** | — |

## 4. Aprobador

| Papel | Quién | Qué aprueba |
| --- | --- | --- |
| Product Owner | El docente de la cátedra, que es también quien ejecuta (`PRODUCT-INTAKE` cabecera y §2) | Que un ítem cumple esta DoR antes de entrar, y las excepciones de §3 |
| AG-06, curaduría del backlog | La misma persona, en el papel de la categoría 06 | Que la historia o la tarea esté redactada, trazada y con sus criterios escritos |

**Con `equipo_n = 1` los dos papeles los ejerce la misma persona.** Lo que reemplaza al filtro de una segunda persona son tres cosas, y en este proyecto de código la tercera es la más dura:

1. El **punto de control bloqueante** de cada etapa (`PRODUCT-INTAKE` §15), que es además donde se validan las **rutas y los verbos** de los quince puntos.
2. La puerta de **imagen** del pipeline, que exige que se construya con el archivo multietapa, **arranque, aplique las transformaciones sobre un almacén vacío y responda salud**. Es `PT-04`.
3. **Las dos inspecciones en las dos direcciones**: los quince puntos contra la guardia, y los diecisiete códigos contra la tabla de traducción. No dependen de que alguien las revise: se corren, y son las únicas que detectan un defecto de **omisión**.

## 5. Qué no es esta DoR

**No es la Definition of Done.** La DoD del proyecto de código vive en `08-Calidad-Y-Pruebas` y **todavía no está emitida**; hasta que lo esté, lo que gobierna el cierre son los criterios de transición de [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §5. Esta DoR habla de **cuándo empezar**: no menciona la batería de integración corriendo con su proporción, ni las tres comparaciones indistinguibles, ni la prueba de eliminación forzada, que son condiciones de cierre.

**No define la superficie.** Los **quince** puntos de acceso, sus verbos y sus códigos de respuesta están en `Definicion-Superficie-HTTP.md` y en [`../05-Arquitectura-Tecnica/Contratos-REST.md`](../05-Arquitectura-Tecnica/Contratos-REST.md); esta DoR exige que la historia **declare** su punto, no que lo rediseñe.

**Y no redacta reglas de negocio ni códigos del contrato.** Las **dieciséis** reglas viven en `GeometriaFactory-Domain` y los **diecisiete** códigos vivos en `GeometriaFactory-Contracts`. Esta capa **no agrega, no renombra y no traduce a texto** ningún código.

## 6. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Declara **ocho** criterios de entrada para las historias —cuatro propios de este proyecto de código: el punto de acceso que la realiza, si ese punto está bajo la guardia, que la historia no decida qué se dice, y que declare si su respuesta pertenece a una de las tres familias deliberadamente empobrecidas— y **seis** para las tareas técnicas, incluido el que exige comparar contra una lista **en las dos direcciones** cuando la tarea toca la superficie. Declara seis casos de excepción, dos de ellos negativos y sin excepción posible, y dos que recogen los únicos dos casos de uso del proyecto de código que no trazan a ninguna necesidad. Declara el aprobador con la constancia de que el filtro más duro son las dos inspecciones en las dos direcciones, y la delimitación contra la Definition of Done, contra la superficie y contra el conjunto cerrado de códigos. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **3**. Sube minor. |
