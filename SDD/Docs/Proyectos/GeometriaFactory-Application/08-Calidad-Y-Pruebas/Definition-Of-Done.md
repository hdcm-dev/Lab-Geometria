# Definition of Done — GeometriaFactory-Application

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** Definition-Of-Done.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`Criterios-Validacion.md`](Criterios-Validacion.md) 1.0; [`Estrategia-Calidad.md`](Estrategia-Calidad.md) 1.0 §3; [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/Definition-Of-Ready.md) §5, que declara que la DoD vive acá; [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §5; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.19** §15
**Trazabilidad downstream:** [`../07-Plan-Sprint/Mini-Plan.md`](../07-Plan-Sprint/Mini-Plan.md), que **referencia** esta DoD y no la redefine; `09-Devops`, que materializa sus criterios mecánicos como etapas del pipeline

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

**Por qué la tercera capa se llama «etapa» y no «sprint».** Este producto no tiene sprints: la unidad de planificación es la **etapa**, y así lo declaran el roadmap y [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §2. Llamarla sprint habría creado una unidad que ninguna fuente tiene. La cuarta capa se llama «entrega del proyecto de código» y no «release» porque **este proyecto de código no se publica**: `redistribuible` es false y no viaja a ningún repositorio de paquetes (`05` §5).

Cada criterio responde a «¿cómo se valida?» con una operación concreta.

### 1.1 Historia de usuario

- [ ] Todos los criterios Given/When/Then de la historia están cubiertos por al menos un `TC-XX` de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md). **Se valida** leyendo la columna de tests de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2 para el `CU-XX` de la historia.
- [ ] Esos `TC-XX` están escritos y **en verde**. **Se valida** con la salida de `scripts/test.sh`.
- [ ] **La comprobación de autorización que la historia declaró en su Definition of Ready criterio 5 tiene prueba de su negativa.** **Se valida** leyendo la matriz §5. Una historia que dijo «ninguna me alcanza» y resultó tocar una operación que lee o escribe **no está terminada**.
- [ ] Toda regla e invariante que la historia declara ejercer tiene su fila en la matriz §4 y §6 con este `TC-XX` entre sus tests. **Se valida** leyendo esas dos tablas.
- [ ] Toda condición de rechazo que la historia produce está en el catálogo de las **36** y alcanzada por prueba. **Se valida** con `TC-04028`.
- [ ] La historia **no introdujo ninguna prueba que abra el almacén real** ni ninguna dependencia saliente nueva. **Se valida** con `TC-04026` y `TC-04027`.
- [ ] Los `TC-XX` de la historia usan **dobles de puerto y no dobles de componente interno**. **Se valida** por inspección del código de prueba, contra [`Estrategia-Testing.md`](Estrategia-Testing.md) §5.
- [ ] La construcción termina en 0 y sin advertencias. **Se valida** con la salida de `scripts/build.sh`.
- [ ] La cobertura del componente que la historia toca no bajó respecto de la medición anterior. **Se valida** comparando el informe de cobertura por componente.

### 1.2 Tarea técnica

- [ ] Los criterios de aceptación que la tarea declara en [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md) se cumplen uno por uno. **Se valida** por inspección, por prueba automatizada o por medición de la puerta que la tarea nombra, según lo exige la Definition of Ready §2 criterio 3.
- [ ] Si la propiedad que la tarea sostiene es una **ausencia** —cero dependencias de más, cero pruebas que tocan la base, cero componentes cargados—, el criterio se midió **con umbral cero y en la condición declarada**, y no se dio por cumplido por no haberse observado lo contrario. **Se valida** con el `TC-XX` de inspección correspondiente.
- [ ] Si la tarea es de tipo indagación, la decisión que produjo está **registrada** en el documento que corresponde, y no sólo tomada. **Se valida** leyendo ese documento.
- [ ] Si la tarea **acompaña** un punto abierto cuya titularidad es de otro proyecto de código —`BT-04020`, `BT-04021`—, declaró de quién es la decisión y no la tomó por su cuenta. **Se valida** leyendo la fila de la tarea.
- [ ] Si la tarea es una puerta —`BT-04004`, `BT-04005`, `BT-04006`, `BT-04018`, `BT-04019`— la puerta se midió al menos una vez y su resultado quedó registrado. **Se valida** con la salida del pipeline.
- [ ] La construcción y la batería pasan enteras. **Se valida** con `scripts/build.sh` y `scripts/test.sh`.

### 1.3 Etapa

- [ ] Todas las historias de la épica de la etapa cumplen §1.1, y todas sus tareas técnicas cumplen §1.2. **Se valida** recorriendo el índice de la épica en [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §3.
- [ ] Los **once** criterios de salida de [`Plan-Pruebas.md`](Plan-Pruebas.md) §3 se cumplen. **Se valida** con esa lista.
- [ ] Los criterios bloqueantes de [`Criterios-Validacion.md`](Criterios-Validacion.md) —`CV-11` a `CV-17`, `CV-25` a `CV-28`— se cumplen. **Se valida** con el informe del pipeline y con `TC-04011`, `TC-04026`, `TC-04027`, `TC-04028`, `TC-04029`, `TC-04030` y `TC-04031`.
- [ ] Los criterios condicionados —`CV-09`, `CV-10`, `CV-23`— **se midieron y se registraron**, aunque no bloqueen. **Se valida** con la presencia de la medición en el informe de cierre. Registrar «sin medir» cuando la medición era posible **no cumple**.
- [ ] La batería completa —y no sólo lo que la etapa tocó— corre y pasa. **Se valida** con `CV-18`.
- [ ] Ningún `TC-XX` que estaba en verde pasó a rojo sin justificación escrita. **Se valida** con `CV-19`.
- [ ] [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) está actualizada: ninguna fila dice `Pendiente` para un elemento que la etapa cerró. **Se valida** comparando la matriz contra el índice de la épica.
- [ ] Todo defecto cerrado en la etapa generó al menos un `TC-XX`. **Se valida** con `CV-20`.
- [ ] El punto de control de la etapa tiene el **OK explícito del Product Owner**, con constancia escrita. **Se valida** con el informe de cierre de la etapa (intake §15, regla de delivery 2 y 3).

### 1.4 Entrega del proyecto de código

Se aplica cuando las **seis** etapas que este proyecto de código toca —`a`, `c`, `d`, `e`, `f` y `h`— están cerradas.

- [ ] Los **veintiocho** criterios de [`Criterios-Validacion.md`](Criterios-Validacion.md) están evaluados uno por uno, con su resultado registrado. **Se valida** con ese documento.
- [ ] **11 de 11** casos de uso, **16 de 16** reglas, **4 de 4** comprobaciones, **9 de 9** invariantes y **32 de 32** historias con caso de prueba en verde. **Se valida** con los recuentos de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md).
- [ ] **36 de 36** condiciones alcanzadas y **0** fuera del catálogo. **Se valida** con `TC-04028`.
- [ ] Los **ocho** escenarios del intake §20 siguen siendo el material de los casos de prueba que los usan, sin sustitución por datos inventados. **Se valida** con `CV-07` y con el recuento de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) §3.
- [ ] Los dos valores rotulados **[ASUNCIÓN]** están confirmados por el Product Owner, o su continuidad como asunción está declarada. **Se valida** leyendo el intake §22 y el estado de `BT-04018`.
- [ ] No queda ningún punto abierto de `05` §11 sin desenlace declarado, **incluido el nombre del cuarto puerto**. **Se valida** leyendo esa tabla.
- [ ] La versión de la biblioteca está calculada según la estrategia de versionado del intake §17.2.P.7 y la etiqueta de la etapa existe. **Se valida** con el registro de la etiqueta.

## 2. Excepciones admitidas

| Caso | Qué se flexibiliza | Quién lo aprueba | Qué queda registrado |
| --- | --- | --- | --- |
| Criterio **condicionado** no alcanzado | Deja de bloquear el cierre de la etapa, porque su umbral es un valor rotulado [ASUNCIÓN] sin confirmar | Nadie: es el tratamiento declarado en [`Criterios-Validacion.md`](Criterios-Validacion.md) §6, no una excepción concedida | La medición y su distancia al umbral, en el informe de cierre |
| Mutation score **no exigible todavía** | El criterio `CV-24` se reporta «sin medir» hasta que la herramienta esté elegida y corra | — | El hueco de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §8 |
| Deuda técnica que una etapa no alcanza a cerrar | El criterio se difiere **una sola vez**, y sólo si no es de los bloqueantes de §1.3 | El Product Owner, en el punto de control | Una `BT-XX` nueva en el backlog técnico, con la etapa en que se cierra |
| Caso de prueba deshabilitado | **No se admite sin motivo escrito en su fila** del catálogo. Un caso deshabilitado sin motivo incumple `CV-26` | — | — |
| Prueba que abre el almacén real | **No se admite en ninguna forma.** El intake §17.2.P.8 declara la puerta propia y bloqueante, y la salida correcta es **reubicar la prueba** en la batería de integración de `GeometriaFactory-Api` porque ahí es donde pertenece, no para esquivar la puerta | — | — |
| Historia que la etapa sólo ejerce parcialmente | **No se admite.** Es la misma regla que la Definition of Ready declara para la entrada: una historia que no cabe entera en su etapa está mal cortada y se redivide | — | — |

## 3. Vigencia

**Este documento es la fuente canónica de la Definition of Done de `GeometriaFactory-Application`.**

- [`../07-Plan-Sprint/Mini-Plan.md`](../07-Plan-Sprint/Mini-Plan.md) y cualquier plan de etapa **referencian** esta DoD y no la redefinen. Una lista de criterios de cierre escrita en un plan es un hallazgo, y el que rige es éste.
- [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/Definition-Of-Ready.md) §5 declara que la DoD vive en esta categoría y que hasta su emisión regían los criterios de transición del roadmap §5, que son de nivel producto. **Con esta emisión ese interinato termina**: los criterios de transición del roadmap siguen valiendo a nivel producto y esta DoD los complementa a nivel de proyecto de código, sin contradecirlos.
- Todo cambio en los criterios de §1 se registra en §4 y se comunica en el punto de control de la etapa siguiente.
- La DoD **no habla de cuándo empezar**: eso es la Definition of Ready, y las dos no se solapan.

## 4. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara la DoD en **cuatro** capas —historia, tarea técnica, **etapa** y entrega del proyecto de código—, con el fundamento de por qué la tercera no se llama sprint y la cuarta no se llama release. Cada criterio responde «cómo se valida» con una operación concreta: un guion, una tabla de la matriz o un caso de prueba nombrado. Incorpora como criterio de historia la comprobación de autorización que la Definition of Ready exigió declarar, y como criterio de tarea técnica que las propiedades de **ausencia** se midan con umbral cero y en su condición. Declara **seis** casos de excepción, tres de ellos negativos y sin excepción posible —entre ellos la prueba que abre el almacén real—, y la vigencia como fuente canónica, con la constancia de que cierra el interinato que la Definition of Ready §5 había declarado. |
