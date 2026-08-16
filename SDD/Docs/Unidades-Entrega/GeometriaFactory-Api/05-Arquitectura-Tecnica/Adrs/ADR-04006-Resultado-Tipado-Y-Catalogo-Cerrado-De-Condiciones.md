# ADR-04006 — Resultado tipado hacia arriba, con el catálogo de treinta y seis condiciones como conjunto cerrado

**Proyecto de código:** GeometriaFactory-Application
**Documento:** ADR-04006-Resultado-Tipado-Y-Catalogo-Cerrado-De-Condiciones.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-12
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Estilo

---

## 1. Contexto

El nivel 0 decidió que la superficie pública del dominio son guardas con **resultado tipado y no excepciones**, y que el rechazo es un valor de retorno. Esta capa está justo encima: si convirtiera esos rechazos en excepciones, la propiedad que aquella decisión compró se perdería en el primer consumidor.

Además, esta capa **produce condiciones propias** que el dominio no tiene: las cuatro negativas de autorización, la indisponibilidad de un puerto, el conjunto de observaciones mal formado. La categoría 03 las catalogó una por una a partir de la §6 de los once casos de uso, y son **36** condiciones distintas.

Hay un tercer elemento que hace falta decidir y que no es obvio: **quién puede acuñar una condición nueva**. Un catálogo que crece desde varios lugares deja de ser un conjunto cerrado, y aguas abajo `GeometriaFactory-Api` tiene que traducir cada condición a una respuesta de protocolo. Si aparece una condición sin traducción declarada, el producto vuelve a tener un fallo que llega a la persona sin representación.

Motivación upstream: NB-00004, NB-00005, NB-00006; RN-04003, RN-04005, RN-04008, RN-04009; `PRODUCT-INTAKE` §17.1.P.11 · GeometriaFactory-Application punto 2 y §17.1.P.12 · GeometriaFactory-Application; [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md).

## 2. Decisión

**Toda condición prevista de esta capa viaja como resultado tipado con su código estable, y nunca como excepción.** Las excepciones quedan reservadas a defectos de programación del consumidor —un argumento nulo donde el contrato exige valor— y nunca a reglas de negocio, a negativas de autorización ni a indisponibilidad de un puerto.

**El conjunto de condiciones es cerrado y su fuente única es la categoría 03 de este proyecto de código**: las **36** de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md). Una condición nueva **sólo puede nacer allá**, derivada de la §6 de un caso de uso. Esta arquitectura no acuña ninguna y no las transcribe: las referencia.

Y una regla de forma sobre lo que la condición lleva: **ninguna condición de esta capa transporta texto de presentación**. Devuelve el código y, cuando corresponde, los datos de ubicación —índice de figura y campo—. La composición del mensaje para una persona es de quien expone, y la traducción a respuesta de protocolo, de `GeometriaFactory-Api`.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Resultado tipado con catálogo cerrado de fuente única (**adoptada**) | Continúa hacia arriba la decisión del nivel 0; el consumidor no puede ignorar un rechazo sin que se note en revisión; el catálogo es contable y verificable en las dos direcciones | El consumidor tiene que tratar el resultado en cada invocación, lo que es más ceremonioso que dejar que una excepción suba |
| Excepciones tipadas por familia de condición | El consumidor no puede olvidarse de tratarla: si no la trata, el proceso falla | Convierte reglas de negocio en control de flujo excepcional, que es exactamente lo que el nivel 0 descartó. Además el costo de la excepción se paga en el camino de las negativas de autorización, que son frecuentes por diseño |
| Resultado tipado, pero permitiendo acuñar condiciones desde esta categoría o desde 08 | Más ágil: quien encuentra una condición nueva la agrega donde está | El conjunto deja de ser cerrado y 03, 05 y 08 se desincronizan. Es el mismo modo de falla que la Fase C de `GeometriaFactory-Visor` registró como riesgo para su conjunto de siete códigos |
| Devolver la condición ya con su texto para la persona | Menos trabajo aguas abajo: el mensaje viaja armado | Metería texto de presentación en el nivel 1 y ataría el idioma y el tono a una capa que no los conoce. Además abriría la puerta a que un mensaje llevara una ruta o una traza, que es lo que `RA-03` cierra |

## 5. Consecuencias positivas

1. Un rechazo de esta capa no se pierde: el consumidor tiene que mirarlo para seguir, y no puede confundirlo con un fallo del proceso.
2. El catálogo de 36 condiciones es contable, y por eso se puede verificar en las **dos direcciones**: que toda condición del catálogo tenga prueba, y que ninguna condición emitida esté fuera del catálogo.
3. La indisponibilidad de un puerto se trata como cualquier otra condición, de modo que el caso de uso de envío termina de forma controlada y el texto original queda intacto.
4. `RA-03` se sostiene sin esfuerzo: si la condición no lleva texto, no puede llevar una dirección de servicio dentro del texto.
5. Aguas abajo, `GeometriaFactory-Api` tiene un conjunto finito que traducir, y `GeometriaFactory-Contracts` puede mantener su propio conjunto cerrado de **diecisiete** códigos vivos sin que esta capa lo desborde.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta la ceremonia en el consumidor.** Cada invocación devuelve algo que hay que mirar. Se compra a cambio de que ningún rechazo se convierta en fallo silencioso, que es lo que el producto viene a eliminar.
2. **Se acepta que el catálogo tenga que actualizarse antes que el código.** Una condición nueva nace en 03; si alguien la emite primero desde acá, la prueba de inspección en la dirección «emitidas contra catálogo» la va a levantar, y eso es deliberado: el orden correcto es el otro.
3. **Se acepta que los 36 códigos de esta capa no coincidan con los 17 códigos vivos del contrato del producto.** No es una divergencia: son dos conjuntos con destinatarios distintos, y el mapeo de uno a otro es de `GeometriaFactory-Api`. Colapsarlos habría obligado a esta capa a hablar en el vocabulario de la frontera HTTP, que no conoce.

## 7. Implementación

- Toda operación de [`../Contratos-Abstractions.md`](../Contratos-Abstractions.md) §3 que pueda rechazar devuelve un resultado con dos salidas posibles: efecto aplicado, o condición que lo impidió.
- **La admisibilidad devuelve varios motivos**, no uno: una cuenta puede ser no admisible por más de una causa a la vez, y colapsarlas perdería información que la capa que expone usa.
- **El envío devuelve el estado resuelto por el dominio más la colección de observaciones**, y las observaciones **no son condiciones de error de esta capa**: son datos del trabajo. La categoría 03 lo declara en su §1.2 y esta ADR lo repite porque es la confusión más probable del catálogo.
- **El comentario del administrador no es una observación** y no comparte ni un campo con ellas.
- Verificación sugerida a 08: prueba de inspección que compara el conjunto de códigos emitidos por la biblioteca contra el catálogo de 03, en las dos direcciones, y falla en cualquiera de los dos sentidos.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Condiciones del catálogo alcanzadas por al menos una prueba | **100 %** de las **36** | Matriz condición contra prueba en 08 |
| Condiciones emitidas por la biblioteca que no figuran en el catálogo | Exactamente **0** | Prueba de inspección en la dirección inversa |
| Reglas de negocio o negativas de autorización propagadas como excepción | Exactamente **0** | Inspección de los seis orquestadores y de la guarda |
| Condiciones que transportan texto de presentación | Exactamente **0** | Inspección de la forma del resultado |
| Condiciones que transportan una dirección de servicio, una ruta de datos o una traza | Exactamente **0** | Inspección de la forma del resultado (`RA-03`) |
| Identificadores de condición reciclados para otra causa | Exactamente **0** | Inspección del catálogo contra su historial de retirados |

## 9. Referencias

- [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md) §1.2, §2.3, §2.4 y §7, fuente única de las 36 condiciones.
- [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 y §6.
- [`../../../GeometriaFactory-Domain/05-Arquitectura-Tecnica/Adrs/ADR-02002-Superficie-Publica-De-Guardas-Y-Resultados-Tipados.md`](ADR-02002-Superficie-Publica-De-Guardas-Y-Resultados-Tipados.md), la decisión del nivel 0 que ésta continúa.
- [`../../../GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Adrs/ADR-08002-Tipo-De-Error-Unico-Con-Conjunto-Cerrado.md`](../../../../Producto/Adrs/ADR-08002-Tipo-De-Error-Unico-Con-Conjunto-Cerrado.md), el conjunto cerrado del otro lado de la frontera.
- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.16** §14 (`RA-03`), §17.1.P.11 · GeometriaFactory-Application punto 2 y §17.1.P.12 · GeometriaFactory-Application.
- ADR relacionadas: [`ADR-04003`](ADR-04003-Versionado-Y-Estabilidad-De-La-Superficie.md), [`ADR-04004`](ADR-04004-Orden-Fijo-De-Las-Cuatro-Comprobaciones.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Registra el resultado tipado como continuación de la decisión del nivel 0, declara el catálogo de 36 condiciones como conjunto cerrado de fuente única en la categoría 03, prohíbe el texto de presentación en la condición, evalúa cuatro alternativas, declara tres trade-offs incluida la no coincidencia con los quince códigos vivos del contrato del producto, y fija seis métricas de validación. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **2**. Sube minor. |
