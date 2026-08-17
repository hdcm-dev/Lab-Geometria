# ADR-02002 — La superficie pública son guardas con resultado tipado, no excepciones

**Unidad de entrega:** GeometriaFactory-Api
**Documento:** ADR-02002-Superficie-Publica-De-Guardas-Y-Resultados-Tipados.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Estilo

---

## 1. Contexto

La categoría 03 de este proyecto de código declara, como lo que hay que llevarse si se lee una sola línea, que **la superficie pública de un modelo de dominio son sus guardas**, y su catálogo de errores es casi entero el catálogo de invariantes violados ([`../../03-UX-UI-DX/README.md`](../../03-UX-UI-DX/README.md) §1). Ese catálogo tiene hoy **42 condiciones** distintas, derivadas una por una de la §6 de los trece casos de uso ([`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md)).

El producto define el **fallo silencioso** como el problema que viene a eliminar, y la necesidad NB-00005 lo declara sobre el error de cálculo. Un rechazo del dominio que el consumidor pueda descartar sin notarlo es exactamente esa clase de fallo, un nivel más abajo.

Además, el dominio distingue tres cosas que se parecen y no se pueden confundir: la **condición de error**, que es un rechazo del contrato; la **observación**, que es lo que la interpretación del texto emite y que puede ser advertencia o error de validación; y el **comentario del administrador**, que es texto de una persona. La distinción está declarada en la categoría 03 y esta decisión tiene que preservarla en la forma de los tipos.

Motivación upstream: NB-00004, NB-00005; RN-02003, RN-02004, RN-02005, RN-02009, RN-02010, RN-02011; INV-02, INV-03, INV-04, INV-07.

## 2. Decisión

Toda operación de la superficie pública que pueda rechazar devuelve un **resultado tipado** que expresa las dos salidas —efecto aplicado, o condición que lo impidió con su código estable— y **no lanza excepciones para expresar reglas de negocio**. Las excepciones quedan reservadas a defectos de programación del consumidor, como pasar un valor ausente donde el contrato exige uno.

El conjunto de códigos de condición es **cerrado y su fuente única es la categoría 03**: un código nuevo nace allá, y esta categoría no acuña ninguno.

Los tres conceptos —condición, observación y comentario— viajan en **tipos distintos y sin campos compartidos**, para que ninguno se pueda leer como otro.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Resultado tipado (**adoptada**) | El rechazo es parte del valor de retorno y el consumidor no lo puede ignorar sin que se note; el conjunto de condiciones es enumerable y verificable contra el catálogo de 03 | Más ceremonia en cada invocación; obliga al consumidor a decidir qué hace con cada condición |
| Excepción por regla de negocio violada | Escritura del camino feliz sin ruido; el rechazo se propaga solo | Un rechazo previsto pasa a costar como un fallo; el consumidor puede capturarlo genéricamente y perderlo, que es el fallo silencioso; y el conjunto de condiciones deja de ser enumerable por inspección de la firma |
| Valor booleano más un mensaje de texto | Muy simple de implementar | El mensaje no es un código estable: no se puede verificar el catálogo, no se puede traducir aguas abajo y se rompe con cada reescritura del texto |
| Registro de errores en una lista mutable de la entidad | Permite acumular varias condiciones de una operación | Deja la entidad modificada aunque la operación no proceda, lo que rompe la terminación controlada de [`../Arquitectura-Unidad-Entrega.md`](../Arquitectura-Unidad-Entrega.md) §4 |

## 5. Consecuencias positivas

1. El catálogo de **42** condiciones de 03 es verificable **en las dos direcciones**: ninguna condición del catálogo sin código que la produzca, y ningún código producido fuera del catálogo.
2. Un rechazo del dominio no puede convertirse en fallo silencioso por captura genérica.
3. La capa que expone puede traducir cada código a su respuesta de protocolo sin interpretar texto libre.
4. La separación de los tres tipos hace imposible que un comentario del administrador se cuente como observación, o que una advertencia se lea como rechazo.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta más ceremonia en el consumidor**, que tiene que desarmar el resultado en cada invocación.
2. **Se acepta que una operación devuelva una sola condición por vez** en los caminos donde el catálogo declara una sola. Donde la operación adopta un conjunto —la interpretación de un texto con varias piezas mal formadas— el resultado lleva la colección, y esa asimetría es deliberada.
3. **Se acepta que el catálogo lo gobierne otra categoría.** Si 03 renombra un código, esta categoría lo sigue; el precedente existe y está documentado: el catálogo registra **cinco identificadores retirados**, tres por renombre y dos por imposibilidad de su causa.

## 7. Implementación

- Cada operación de [`../Contratos-Abstractions.md`](../Contratos-Abstractions.md) §3 declara en su fila qué condiciones puede devolver.
- El código de condición es un valor de conjunto cerrado, no una cadena libre.
- Convención impuesta al consumidor: **el resultado no se descarta**. Una invocación cuyo resultado no se usa es un defecto de revisión, y es la contrapartida de no usar excepciones.
- El tipo de observación conserva la posición de pieza y el campo señalado, que es lo que RN-02009 exige; el tipo de condición no los necesita y no los lleva salvo donde la condición sea atribuible a una figura.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Correspondencia con el catálogo de 03 | Diferencia de conjuntos **vacía en las dos direcciones** sobre las **42** condiciones | Prueba de inspección que recorre los códigos emitidos y los compara con el catálogo |
| Excepciones usadas para expresar reglas de negocio | Exactamente **0** | Inspección de la superficie pública en revisión |
| Condiciones alcanzadas por prueba | **100 %** de las 42 | Matriz condición contra prueba en 08 |
| Campos compartidos entre condición, observación y comentario | Exactamente **0** | Inspección de los tres tipos |

## 9. Referencias

- [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md) —catálogo de 42 condiciones y los cinco identificadores retirados— y [`../../03-UX-UI-DX/README.md`](../../03-UX-UI-DX/README.md) §1.
- [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §3 y §4.
- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` 1.15 §4.1 (RN-02009) y §17.1.P.6 · GeometriaFactory-Domain.
- ADR relacionadas: [`ADR-02001`](ADR-02001-Modelo-De-Dominio-Rico-Con-Invariantes-Explicitas.md), [`ADR-02005`](ADR-02005-Guarda-Unica-De-Admisibilidad.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Registra el resultado tipado como forma de la superficie pública, la reserva de las excepciones a los defectos de programación, la fuente única del catálogo de condiciones en la categoría 03, las cuatro alternativas evaluadas y las cuatro métricas de validación. |
