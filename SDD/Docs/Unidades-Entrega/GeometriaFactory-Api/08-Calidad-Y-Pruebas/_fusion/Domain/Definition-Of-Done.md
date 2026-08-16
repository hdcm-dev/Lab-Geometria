# Definition of Done — GeometriaFactory-Domain

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** Definition-Of-Done.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`Criterios-Validacion.md`](Criterios-Validacion.md) 1.0; [`Estrategia-Calidad.md`](Estrategia-Calidad.md) 1.0 §3; [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../../../06-Backlog-Tecnico/_fusion/Domain/Definition-Of-Ready.md) 1.0 §5, que declara que la DoD vive acá; [`../../../00-Contexto/Roadmap-Producto.md`](../../../../../00-Contexto/Roadmap-Producto.md) §5; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.19** §15
**Trazabilidad downstream:** [`../07-Plan-Sprint/Mini-Plan.md`](../../../07-Plan-Sprint/_fusion/Domain/Mini-Plan.md), que **referencia** esta DoD y no la redefine; `09-Devops`, que materializa sus criterios mecánicos como etapas del pipeline

---

## Tabla de contenido

- [1. DoD por capa](#1-dod-por-capa)
  - [1.1 Historia de usuario](#11-historia-de-usuario)
  - [1.2 Tarea técnica](#12-tarea-técnica)
  - [1.3 Etapa](#13-etapa)
  - [1.4 Entrega del proyecto de código](#14-entrega-del-proyecto-de-código)
- [2. Excepciones admitidas](#2-excepciones-admitidas)
- [3. Vigencia](#3-vigencia)
- [4. Control de cambios](#4-control-de-cambios)

---

## 1. DoD por capa

**Por qué la tercera capa se llama «etapa» y no «sprint».** Este producto no tiene sprints: la unidad de planificación es la **etapa**, y así lo declaran el roadmap §1.2 y [`../06-Backlog-Tecnico/Product-Backlog.md`](../../../06-Backlog-Tecnico/_fusion/Domain/Product-Backlog.md) §4.1. Llamarla sprint habría creado una unidad que ninguna fuente tiene. La cuarta capa se llama «entrega del proyecto de código» y no «release» porque **este proyecto de código no se publica**: `redistribuible` es false y no viaja a ningún repositorio de paquetes (`05` §5).

Cada criterio responde a «¿cómo se valida?» con una operación concreta.

### 1.1 Historia de usuario

- [ ] Todos los criterios Given/When/Then de la historia están cubiertos por al menos un `TC-XX` de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md). **Se valida** leyendo la columna de test de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2 para el `CU-XX` de la historia.
- [ ] Esos `TC-XX` están escritos y **en verde**. **Se valida** con la salida de `scripts/test.sh`.
- [ ] Toda regla e invariante que la historia declara ejercer tiene su fila en la matriz §4 y §5 con este `TC-XX` entre sus tests. **Se valida** leyendo esas dos tablas.
- [ ] Toda condición de rechazo que la historia produce está en el catálogo de [`../03-UX-UI-DX/DX-Error-Messages.md`](../../../03-UX-UI-DX/_fusion/Domain/DX-Error-Messages.md) y alcanzada por prueba. **Se valida** con `TC-02023`.
- [ ] La historia no introdujo ninguna dependencia saliente. **Se valida** con `TC-02024`.
- [ ] La construcción termina en 0 y sin advertencias. **Se valida** con la salida de `scripts/build.sh`.
- [ ] La cobertura del componente que la historia toca no bajó respecto de la medición anterior. **Se valida** comparando el informe de cobertura por componente.

### 1.2 Tarea técnica

- [ ] Los criterios de aceptación que la tarea declara en [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../../../06-Backlog-Tecnico/_fusion/Domain/Backlog-Tecnico.md) se cumplen uno por uno. **Se valida** por inspección, por prueba automatizada o por medición de la puerta que la tarea nombra, según lo exige la DoR §2 criterio 3.
- [ ] Si la tarea es de tipo indagación, la decisión que produjo está **registrada** en el documento que corresponde, y no sólo tomada. **Se valida** leyendo ese documento.
- [ ] Si la tarea cierra un punto abierto de `05` §11 o de [`../06-Backlog-Tecnico/Product-Backlog.md`](../../../06-Backlog-Tecnico/_fusion/Domain/Product-Backlog.md) §6, ese punto queda declarado cerrado con su desenlace. **Se valida** leyendo la tabla de puntos abiertos.
- [ ] Si la tarea es una puerta —`BT-02004`, `BT-02005`, `BT-02008`, `BT-02014`— la puerta se midió al menos una vez y su resultado quedó registrado. **Se valida** con la salida del pipeline.
- [ ] La construcción y la batería pasan enteras. **Se valida** con `scripts/build.sh` y `scripts/test.sh`.

### 1.3 Etapa

- [ ] Todas las historias de la épica de la etapa cumplen §1.1, y todas sus tareas técnicas cumplen §1.2. **Se valida** recorriendo el índice de la épica en [`../06-Backlog-Tecnico/Product-Backlog.md`](../../../06-Backlog-Tecnico/_fusion/Domain/Product-Backlog.md) §3.
- [ ] Los **nueve** criterios de salida de [`Plan-Pruebas.md`](Plan-Pruebas.md) §3 se cumplen. **Se valida** con esa lista.
- [ ] Los criterios bloqueantes de [`Criterios-Validacion.md`](Criterios-Validacion.md) —`CV-10` a `CV-13`, `CV-20` a `CV-22`— se cumplen. **Se valida** con el informe del pipeline y con `TC-02023`, `TC-02024`, `TC-02026` y `TC-02027`.
- [ ] Los criterios condicionados —`CV-08`, `CV-09`, `CV-18`— **se midieron y se registraron**, aunque no bloqueen. **Se valida** con la presencia de la medición en el informe de cierre. Registrar «sin medir» cuando la medición era posible **no cumple**.
- [ ] La batería completa —y no sólo lo que la etapa tocó— corre y pasa. **Se valida** con `CV-14`.
- [ ] Ningún `TC-XX` que estaba en verde pasó a rojo sin justificación escrita. **Se valida** con `CV-15`.
- [ ] [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) está actualizada: ninguna fila dice `Pendiente` para un elemento que la etapa cerró. **Se valida** comparando la matriz contra el índice de la épica.
- [ ] Todo defecto cerrado en la etapa generó al menos un `TC-XX`. **Se valida** con `CV-16`.
- [ ] El punto de control de la etapa tiene el **OK explícito del Product Owner**, con constancia escrita. **Se valida** con el informe de cierre de la etapa (intake §15).

### 1.4 Entrega del proyecto de código

Se aplica cuando las **seis** etapas que este proyecto de código toca —`a`, `c`, `d`, `e`, `f` y `h`— están cerradas.

- [ ] Los **veintidós** criterios de [`Criterios-Validacion.md`](Criterios-Validacion.md) están evaluados uno por uno, con su resultado registrado. **Se valida** con ese documento.
- [ ] **13 de 13** casos de uso, **16 de 16** reglas, **9 de 9** invariantes y **27 de 27** historias con caso de prueba en verde. **Se valida** con los recuentos de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md).
- [ ] **42 de 42** condiciones alcanzadas y **0** fuera del catálogo. **Se valida** con `TC-02023`.
- [ ] Los **ocho** escenarios del intake §20 siguen siendo el material de los casos de prueba que los usan, sin sustitución por datos sintéticos. **Se valida** con `CV-06` y con el recuento de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) §3.
- [ ] Los dos valores rotulados **[ASUNCIÓN]** están confirmados por el Product Owner, o su continuidad como asunción está declarada. **Se valida** leyendo el intake §22 y el estado de `BT-02015`.
- [ ] No queda ningún punto abierto de `05` §11 sin desenlace declarado. **Se valida** leyendo esa tabla.
- [ ] La versión de la biblioteca está calculada según la estrategia de versionado del intake §17.1.P.7 · GeometriaFactory-Domain y la etiqueta de la etapa existe. **Se valida** con el registro de la etiqueta.

## 2. Excepciones admitidas

| Caso | Qué se flexibiliza | Quién lo aprueba | Qué queda registrado |
| --- | --- | --- | --- |
| Criterio **condicionado** no alcanzado | Deja de bloquear el cierre de la etapa, porque su umbral es un valor rotulado [ASUNCIÓN] sin confirmar | Nadie: es el tratamiento declarado en [`Criterios-Validacion.md`](Criterios-Validacion.md) §6, no una excepción concedida | La medición y su distancia al umbral, en el informe de cierre |
| Mutation score **no exigible todavía** | El criterio `CV-19` se reporta «sin medir» hasta que la herramienta esté elegida y corra | — | El hueco de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §7 |
| Deuda técnica que una etapa no alcanza a cerrar | El criterio se difiere **una sola vez**, y sólo si no es de los bloqueantes de §1.3 | El Product Owner, en el punto de control | Una `BT-XX` nueva en el backlog técnico, con la etapa en que se cierra |
| Caso de prueba deshabilitado | **No se admite sin motivo escrito en su fila** del catálogo. Un caso deshabilitado sin motivo incumple `CV-21` | — | — |
| Historia que la etapa sólo ejerce parcialmente | **No se admite.** Es la misma regla que la DoR §3 declara para la entrada: una historia que no cabe entera en su etapa está mal cortada y se redivide | — | — |

## 3. Vigencia

**Este documento es la fuente canónica de la Definition of Done de `GeometriaFactory-Domain`.**

- [`../07-Plan-Sprint/Mini-Plan.md`](../../../07-Plan-Sprint/_fusion/Domain/Mini-Plan.md) y cualquier plan de etapa **referencian** esta DoD y no la redefinen. Una lista de criterios de cierre escrita en un plan es un hallazgo, y el que rige es éste.
- [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../../../06-Backlog-Tecnico/_fusion/Domain/Definition-Of-Ready.md) §5 declara que la DoD vive en esta categoría y que hasta su emisión regían los criterios de transición del roadmap §5, que son de nivel producto. **Con esta emisión ese interinato termina**: los criterios de transición del roadmap siguen valiendo a nivel producto y esta DoD los complementa a nivel de proyecto de código, sin contradecirlos.
- Todo cambio en los criterios de §1 se registra en §4 y se comunica en el punto de control de la etapa siguiente.
- La DoD **no habla de cuándo empezar**: eso es la DoR, y las dos no se solapan.

## 4. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara la DoD en **cuatro** capas —historia, tarea técnica, **etapa** y entrega del proyecto de código—, con el fundamento de por qué la tercera no se llama sprint y la cuarta no se llama release. Cada criterio responde «cómo se valida» con una operación concreta: un guion, una tabla de la matriz o un caso de prueba nombrado. Declara **cinco** casos de excepción, dos de ellos negativos y sin excepción posible, y la vigencia como fuente canónica, con la constancia de que cierra el interinato que la Definition of Ready §5 había declarado. |
