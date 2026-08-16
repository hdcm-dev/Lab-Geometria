# Definition of Done — GeometriaFactory-Infrastructure

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** Definition-Of-Done.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`Criterios-Validacion.md`](Criterios-Validacion.md) 1.1; [`Estrategia-Calidad.md`](Estrategia-Calidad.md) 1.1 §3; [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../../../06-Backlog-Tecnico/_fusion/Infrastructure/Definition-Of-Ready.md) §5, que declara que la DoD vive acá; [`../../../00-Contexto/Roadmap-Producto.md`](../../../../../00-Contexto/Roadmap-Producto.md) §5; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.20** §15 y §17.1.P.8 · GeometriaFactory-Infrastructure
**Trazabilidad downstream:** [`../07-Plan-Sprint/Mini-Plan.md`](../../../07-Plan-Sprint/_fusion/Infrastructure/Mini-Plan.md), que **referencia** esta DoD y no la redefine; `09-Devops`, que materializa sus criterios mecánicos como etapas del pipeline

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

**Por qué la tercera capa se llama «etapa» y no «sprint».** Este producto no tiene sprints: la unidad de planificación es la **etapa**. La cuarta se llama «entrega del proyecto de código» y no «release» porque **este proyecto de código no se publica**: `redistribuible` es false y viaja embebido en el proceso de `GeometriaFactory-Api`.

Cada criterio responde a «¿cómo se valida?» con una operación concreta.

### 1.1 Historia de usuario

- [ ] Todos los criterios Given/When/Then de la historia están cubiertos por al menos un `TC-XX` de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md). **Se valida** leyendo la columna de tests de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2.
- [ ] Esos `TC-XX` están escritos y **en verde**. **Se valida** con la salida de `scripts/test.sh`.
- [ ] Toda regla de negocio, invariante y **regla conceptual de modelo** que la historia declara ejercer tiene su fila en la matriz §4 y §5 con este `TC-XX` entre sus tests. **Se valida** leyendo esas dos tablas.
- [ ] **Si la historia usa un texto de figuras, ese texto sale del intake §20 y no está escrito a mano.** **Se valida** por inspección del fixture, contra `CV-34`. Una historia con un texto propio **no está terminada**: es el modo en que el riesgo de negocio que la fuente pone primero se materializa sin que nadie lo note.
- [ ] Toda condición de rechazo que la historia produce está en el catálogo de las **17** y alcanzada por prueba. **Se valida** con `TC-06034`.
- [ ] **Si la historia introduce una propiedad de ausencia** —cero peticiones de red, cero retiros parciales, cero provisorias repetidas, cero secretos en mensajes—, se midió **con umbral cero y en la condición declarada**, y no se dio por cumplida por no haberse observado lo contrario. **Se valida** con el `TC-XX` correspondiente.
- [ ] **Si la historia toca el almacén, su prueba crea y descarta su propio almacén efímero.** **Se valida** por inspección, contra `CV-33`.
- [ ] La construcción termina en 0 y sin advertencias. **Se valida** con la salida de `scripts/build.sh`.
- [ ] La cobertura del componente que la historia toca no bajó respecto de la medición anterior, **y si el componente es uno de los dos motores, se reporta también en el informe acotado**. **Se valida** comparando los dos informes de cobertura.

### 1.2 Tarea técnica

- [ ] Los criterios de aceptación que la tarea declara en [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../../../06-Backlog-Tecnico/_fusion/Infrastructure/Backlog-Tecnico.md) se cumplen uno por uno. **Se valida** por inspección, por prueba automatizada o por medición de la puerta que la tarea nombra.
- [ ] Si la tarea es de tipo indagación, la decisión que produjo está **registrada** en el documento que corresponde, y no sólo tomada. **Se valida** leyendo ese documento.
- [ ] Si la tarea cierra un punto abierto de `05` §11, ese punto queda declarado cerrado con su desenlace. **Se valida** leyendo esa tabla.
- [ ] Si la tarea es una puerta del pipeline —construcción, batería, cobertura, **verificación de transformaciones**— la puerta se midió al menos una vez y su resultado quedó registrado. **Se valida** con la salida del pipeline.
- [ ] La construcción y la batería pasan enteras, **incluida la etapa de verificación de transformaciones**. **Se valida** con `scripts/build.sh` y `scripts/test.sh`.

### 1.3 Etapa

- [ ] Todas las historias de la épica de la etapa cumplen §1.1, y todas sus tareas técnicas cumplen §1.2. **Se valida** recorriendo el índice de la épica en [`../06-Backlog-Tecnico/Product-Backlog.md`](../../../06-Backlog-Tecnico/_fusion/Infrastructure/Product-Backlog.md) §3.
- [ ] Los **once** criterios de salida de [`Plan-Pruebas.md`](Plan-Pruebas.md) §3 se cumplen. **Se valida** con esa lista.
- [ ] **A partir de la etapa `f`: la batería del validador pasa entera, 10 de 10.** **Se valida** con la tabla de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §6, recorrida fila por fila. **Nueve casos no cumplen**, y el motivo está en [`Criterios-Validacion.md`](Criterios-Validacion.md) §6.
- [ ] Los criterios bloqueantes de [`Criterios-Validacion.md`](Criterios-Validacion.md) —`CV-13` a `CV-23`, `CV-31` a `CV-35`— se cumplen. **Se valida** con el informe del pipeline y con los casos de prueba nombrados.
- [ ] Los criterios condicionados —`CV-10`, `CV-11`, `CV-12`, `CV-29`— **se midieron y se registraron**, aunque no bloqueen. **Se valida** con la presencia de la medición en el informe de cierre. Registrar «sin medir» cuando la medición era posible **no cumple**.
- [ ] La batería completa —y no sólo lo que la etapa tocó— corre y pasa. **Se valida** con `CV-24`.
- [ ] Ningún `TC-XX` que estaba en verde pasó a rojo sin justificación escrita. **Se valida** con `CV-25`.
- [ ] [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) está actualizada: ninguna fila dice `Pendiente` para un elemento que la etapa cerró. **Se valida** comparando la matriz contra el índice de la épica.
- [ ] Todo defecto cerrado en la etapa generó al menos un `TC-XX`. **Se valida** con `CV-27`.
- [ ] El punto de control de la etapa tiene el **OK explícito del Product Owner**, con constancia escrita. **Se valida** con el informe de cierre (intake §15, reglas de delivery 2 y 3).

### 1.4 Entrega del proyecto de código

Se aplica cuando las **cinco** etapas que este proyecto de código toca —`a`, `c`, `d`, `e` y `f`— están cerradas.

- [ ] Los **treinta y cinco** criterios de [`Criterios-Validacion.md`](Criterios-Validacion.md) están evaluados uno por uno, con su resultado registrado. **Se valida** con ese documento.
- [ ] **10 de 10** casos de uso, **16 de 16** reglas de negocio con su tramo verificado, **7 de 7** reglas conceptuales de modelo y **25 de 25** historias con caso de prueba en verde. **Se valida** con los recuentos de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md).
- [ ] **10 de 10** casos de la batería del validador, con los **ocho** escenarios como entrada. **Se valida** con la matriz §6.
- [ ] **17 de 17** condiciones alcanzadas y **0** fuera del catálogo. **Se valida** con `TC-06034`.
- [ ] Los **ocho** escenarios del intake §20 siguen siendo el material de los casos que los usan, **como texto literal**. **Se valida** con `CV-07` y con el recuento de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) §3.
- [ ] Los **tres** valores rotulados **[ASUNCIÓN]** están confirmados por el Product Owner, o su continuidad como asunción está declarada. **Se valida** leyendo el intake §22 y `PA-11` de `05` §11.
- [ ] No queda ningún punto abierto de `05` §11 sin desenlace declarado. **Se valida** leyendo esa tabla, que ya tiene una fila **resuelta** con su fecha.
- [ ] La versión de la biblioteca está calculada según la estrategia de versionado del intake §17.1.P.7 · GeometriaFactory-Infrastructure, la etiqueta de la etapa existe y **ninguna transformación de esquema ya fusionada fue editada**. **Se valida** con el registro de la etiqueta y con el historial de las transformaciones.

## 2. Excepciones admitidas

| Caso | Qué se flexibiliza | Quién lo aprueba | Qué queda registrado |
| --- | --- | --- | --- |
| Criterio **condicionado** no alcanzado | Deja de bloquear el cierre, porque su umbral es un valor rotulado [ASUNCIÓN] sin confirmar | Nadie: es el tratamiento declarado en [`Criterios-Validacion.md`](Criterios-Validacion.md) §6 | La medición y su distancia al umbral, en el informe de cierre |
| Mutation score **no exigible todavía** | El criterio `CV-30` se reporta «sin medir» hasta que la herramienta esté elegida y corra | — | El hueco de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §8 |
| **Adaptador de reloj sin mutation score** | Queda exento con su fundamento declarado: un umbral de mutación sobre una operación de una línea no aporta información | — | La fila correspondiente de [`Estrategia-Testing.md`](../../Estrategia-Testing.md) §2 |
| Deuda técnica que una etapa no alcanza a cerrar | El criterio se difiere **una sola vez**, y sólo si no es de los bloqueantes de §1.3 | El Product Owner, en el punto de control | Una `BT-XX` nueva, con la etapa en que se cierra |
| Caso de prueba deshabilitado | **No se admite sin motivo escrito en su fila** del catálogo | — | — |
| **Batería cerrada con nueve casos** | **No se admite.** La batería tiene **diez** y el décimo cubre `E-8`. El intake **1.20** lo dice así en §17.1.P.6 · GeometriaFactory-Infrastructure, §17.1.P.8 · GeometriaFactory-Infrastructure y §17.1.P.8 · GeometriaFactory-Api; la redacción de nueve fue de versiones anteriores al décimo caso y ya está corregida | — | — |
| **Texto de figuras escrito a mano** | **No se admite en ninguna forma.** Los ocho escenarios existen precisamente porque nadie los escribió pensando en el validador | — | — |
| **NFR de umbral cero dado por cumplido sin medición** | **No se admite.** No haber observado lo contrario no es una medición | — | — |

## 3. Vigencia

**Este documento es la fuente canónica de la Definition of Done de `GeometriaFactory-Infrastructure`.**

- [`../07-Plan-Sprint/Mini-Plan.md`](../../../07-Plan-Sprint/_fusion/Infrastructure/Mini-Plan.md) y cualquier plan de etapa **referencian** esta DoD y no la redefinen. Una lista de criterios de cierre escrita en un plan es un hallazgo, y el que rige es éste.
- [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../../../06-Backlog-Tecnico/_fusion/Infrastructure/Definition-Of-Ready.md) §5 declara que la DoD vive en esta categoría y que hasta su emisión regían los criterios de transición del roadmap §5, que son de nivel producto. **Con esta emisión ese interinato termina**: los criterios del roadmap siguen valiendo a nivel producto y esta DoD los complementa a nivel de proyecto de código.
- **El recuento de la batería no se cambia desde este documento.** Esta DoD exige **diez**, siguiendo `05` §8 y §10.5; si el Product Owner corrigiera la redacción de sus gates en otro sentido, el cambio bajaría por el intake y no por acá.
- Todo cambio en los criterios de §1 se registra en §4 y se comunica en el punto de control de la etapa siguiente.
- La DoD **no habla de cuándo empezar**: eso es la Definition of Ready, y las dos no se solapan.

## 4. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **`H-01`.** El caso de excepción «batería cerrada con nueve casos» describía la redacción del gate del intake sin decir que ya está corregida; ahora cita el intake **1.20** §17.1.P.6 · GeometriaFactory-Infrastructure, §17.1.P.8 · GeometriaFactory-Infrastructure y §17.1.P.8 · GeometriaFactory-Api, que dicen **diez**. No se admite igual, y el umbral sigue siendo **10 de 10** desde la etapa `f`. Corrige contra [`../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md`](../../../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md) 1.0 y contra el texto vivo del intake **1.20**. |
| 1.0 | 2026-08-11 | Emisión inicial. Declara la DoD en **cuatro** capas —historia, tarea técnica, **etapa** y entrega del proyecto de código—, con el fundamento de por qué la tercera no se llama sprint y la cuarta no se llama release. Cada criterio responde «cómo se valida» con una operación concreta. Incorpora como criterio de historia que **todo texto de figuras salga del intake §20 y no esté escrito a mano**, que las propiedades de **ausencia** se midan en su condición declarada, y que toda prueba que toque el almacén **cree y descarte el suyo**. Su §1.3 exige la batería **10 de 10** a partir de la etapa `f` y declara que **nueve no cumplen**. Declara **ocho** casos de excepción, **cuatro** de ellos sin excepción posible, y la vigencia como fuente canónica, con la constancia de que el recuento de la batería **no se cambia desde este documento**. |
