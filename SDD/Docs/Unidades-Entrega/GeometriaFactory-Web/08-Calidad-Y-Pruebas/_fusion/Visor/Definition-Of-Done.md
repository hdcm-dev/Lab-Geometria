# Definition of Done — GeometriaFactory-Visor

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Visor
**Documento:** Definition-Of-Done.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`Criterios-Validacion.md`](Criterios-Validacion.md) 1.0; [`Estrategia-Calidad.md`](Estrategia-Calidad.md) 1.1 §3; [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../../../06-Backlog-Tecnico/_fusion/Visor/Definition-Of-Ready.md) 1.0 §5, que declara que la DoD vive acá; [`../../../00-Contexto/Roadmap-Producto.md`](../../../../../00-Contexto/Roadmap-Producto.md) §2.2 y §5; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.19** §15 y §17.7.P.8
**Trazabilidad downstream:** [`../07-Plan-Sprint/Mini-Plan.md`](../../../07-Plan-Sprint/_fusion/Visor/Mini-Plan.md), que **referencia** esta DoD y no la redefine; `09-Devops` y `10-Examples`

---

## Tabla de contenido

- [1. DoD por capa](#1-dod-por-capa)
  - [1.1 Historia de usuario](#11-historia-de-usuario)
  - [1.2 Tarea técnica](#12-tarea-técnica)
  - [1.3 Momento del producto](#13-momento-del-producto)
  - [1.4 Entrega del proyecto de código](#14-entrega-del-proyecto-de-código)
- [2. Excepciones admitidas](#2-excepciones-admitidas)
- [3. Vigencia](#3-vigencia)
- [4. Control de cambios](#4-control-de-cambios)

---

## 1. DoD por capa

**Por qué la tercera capa se llama «momento del producto».** El producto no tiene sprints y este proyecto de código no se organiza sólo por etapas: su momento central es el de la **medición de `PT-02` y `PT-03`**, que el roadmap §2.2 ubica antes de comprometer la etapa `g` y que `06` §2.1 declara como épica sin crear etapa nueva. Llamar «sprint» o «etapa» a esa capa habría inventado una unidad que ninguna fuente tiene.

**Por qué la cuarta se llama «entrega del proyecto de código».** El bundle **no se publica en ningún repositorio de paquetes**: `redistribuible` es false y su artefacto se copia al directorio de recursos estáticos del anfitrión.

### 1.1 Historia de usuario

- [ ] Todos los criterios Given/When/Then de la historia están cubiertos por al menos un `TC-XX`. **Se valida** leyendo la columna de test de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2 para su `CU-XX`.
- [ ] Esos `TC-XX` están escritos y **en verde**.
- [ ] **Toda garantía que la historia declara ejercer tiene su fila en la matriz §5 con este `TC-XX` entre sus tests**, y ninguna afirmación de la historia contradice a otra garantía. **Se valida** leyendo esa tabla. Es el criterio 5 de la DoR verificado del lado del cierre.
- [ ] Todo código de condición que la historia usa es **uno de los siete**, y la historia **no acuñó ninguno**. **Se valida** con `TC-12021`.
- [ ] Si la historia entrega una **ausencia**, su criterio se verificó con **umbral cero y con su condición de medición registrada**. **Se valida** leyendo el registro de la medición. Un umbral cero sin condición **no cumple**.
- [ ] Ninguna persona, papel, servicio ni credencial interviene como actor ni condiciona un flujo. **Se valida** leyendo la historia y su caso de prueba.
- [ ] El bundle se genera sin errores. **Se valida** con el guion de construcción del bundle.

### 1.2 Tarea técnica

- [ ] Los criterios de aceptación que la tarea declara en [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../../../06-Backlog-Tecnico/_fusion/Visor/Backlog-Tecnico.md) se cumplen uno por uno. **Se valida** por inspección, por prueba automatizada o por medición de la puerta que la tarea nombra.
- [ ] Si la tarea sostiene una **ausencia**, su criterio se expresó con umbral cero **y con la condición en que se mide**. **Se valida** leyendo el criterio: sin condición, la tarea ni siquiera cumplía la DoR §2.
- [ ] Si la tarea es de tipo indagación, la decisión que produjo está **registrada** y no sólo tomada. **Se valida** leyendo el documento donde quedó.
- [ ] Ninguna dependencia introducida cruza la regla de dependencias entre capas. **Se valida** con `CV-29`.
- [ ] Si la tarea mide una puerta —`BT-12013`, `BT-12014`, `BT-12016`— el resultado quedó registrado con su condición de medición. **Se valida** con el informe.

### 1.3 Momento del producto

- [ ] Todas las historias de la épica cumplen §1.1, y todas sus tareas técnicas cumplen §1.2.
- [ ] Los **diez** criterios de salida de [`Plan-Pruebas.md`](Plan-Pruebas.md) §3 se cumplen.
- [ ] Los criterios bloqueantes de [`Criterios-Validacion.md`](Criterios-Validacion.md) —`CV-29` a `CV-31`— se cumplen.
- [ ] **En el momento de medición: `PT-02` y `PT-03` pasan enteras**, en sus **seis** tramos `CV-18` a `CV-23`. **Se valida** con `TC-12019` y `TC-12020`. **Si alguna no pasa, la etapa `g` no se compromete**: no hay diferimiento, no hay deuda y no hay carácter condicionado.
- [ ] Toda medición de ausencia se hizo **con su condición** y quedó registrada junto al resultado. **Se valida** con el informe de cierre.
- [ ] La batería completa —y no sólo lo que el momento tocó— corre y pasa. **Se valida** con `CV-24`.
- [ ] Ningún `TC-XX` que estaba en verde pasó a rojo sin justificación escrita. **Se valida** con `CV-25`.
- [ ] [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) está actualizada en sus cinco tablas.
- [ ] [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) tiene el estado y la fecha de verificación de cada fila que el momento toca. **Se valida** leyendo su columna de estado.
- [ ] Todo defecto cerrado generó al menos un `TC-XX`. **Se valida** con `CV-26`.
- [ ] Si el momento propuso una función nueva en la fachada, los **seis** pasos de [`../05-Arquitectura-Tecnica/Extensibilidad.md`](../../../05-Arquitectura-Tecnica/Extensibilidad.md) §5 se recorrieron enteros, incluida la consolidación en el intake. **Se valida** leyendo el intake §17.7.P.3.
- [ ] El punto de control tiene el **OK explícito del Product Owner**.

### 1.4 Entrega del proyecto de código

Se aplica cuando la etapa `a`, el momento de medición y la etapa `g` están cerrados.

- [ ] Los **treinta y cuatro** criterios de [`Criterios-Validacion.md`](Criterios-Validacion.md) están evaluados uno por uno, con su resultado registrado.
- [ ] **7 de 7** casos de uso, **6 de 6** funciones, **7 de 7** garantías, **7 de 7** códigos en sus **8** cursos, **14 de 14** historias y **8 de 8** NFR con caso de prueba en verde.
- [ ] Las **seis** propiedades transversales verificadas **con sus condiciones de medición** y reverificadas después de incorporar el gobierno en vivo de los movimientos. **Se valida** con `CV-27`.
- [ ] Los **ocho** escenarios del intake §20 siguen siendo el material de los casos de prueba que los usan. **Se valida** con `CV-08`.
- [ ] **`PT-02` y `PT-03` pasadas**, con su registro.
- [ ] Los **ocho** compromisos de un reemplazo de la capa 3 están verificables sin backend. **Se valida** con [`Guia-Testing-Extensibilidad.md`](../../Guia-Testing-Extensibilidad.md) §3.
- [ ] El sample **S-1** ejerce las **seis** funciones enteras, en **cinco pasos o menos**. **Se valida** con `TC-12015`.
- [ ] Los puntos abiertos de `05` §11 tienen desenlace declarado, o su continuidad como abiertos está registrada: hoy son **cinco**, `PA-01` a `PA-05`.
- [ ] El bundle es un **artefacto generado y reproducible**, nunca editado a mano. **Se valida** con `CV-30`.

## 2. Excepciones admitidas

| Caso | Qué se flexibiliza | Quién lo aprueba | Qué queda registrado |
| --- | --- | --- | --- |
| **Umbral de fluidez inexistente** | La verificación es cualitativa declarada junto con `PT-02`. **No habilita a inventar un número** | El Product Owner, o esta categoría al fijar su guion de medición (`BT-12018`) | El guion cualitativo y su resultado, rotulado como cualitativo |
| Deuda técnica que un momento no alcanza a cerrar | Se difiere **una sola vez**, y nunca si es de los bloqueantes de §1.3 | El Product Owner | Una `BT-XX` nueva con el momento en que se cierra |
| **`PT-02` o `PT-03` que no pasan** | **Ninguna excepción.** La etapa `g` no se compromete | — | — |
| Medición de ausencia **sin su condición** | **No se admite.** No cuenta como medición | — | — |
| Historia que rompe una garantía o acuña un código | **No se admite**, y es la misma prohibición que la DoR §3 declara del lado de la entrada | — | — |

## 3. Vigencia

**Este documento es la fuente canónica de la Definition of Done de `GeometriaFactory-Visor`.**

- [`../07-Plan-Sprint/Mini-Plan.md`](../../../07-Plan-Sprint/_fusion/Visor/Mini-Plan.md) y cualquier plan **referencian** esta DoD y no la redefinen.
- [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../../../06-Backlog-Tecnico/_fusion/Visor/Definition-Of-Ready.md) §5 declara que la DoD vive en esta categoría y que hasta su emisión gobernaban el cierre los criterios de transición del roadmap §5 y las dos puertas técnicas. **Con esta emisión ese interinato termina**: los criterios de transición del roadmap siguen valiendo a nivel producto, las dos puertas siguen siendo del intake, y esta DoD las incorpora sin redefinirlas.
- Esa misma sección de la DoR nombra tres condiciones de cierre que **no son suyas**: los diez recorridos sin degradación, la medición de peticiones con los movimientos prendidos y la página integradora funcionando. **Las tres viven acá**, en `CV-14`, `CV-10` y `CV-12` respectivamente, y su ubicación queda así confirmada.
- Todo cambio en los criterios de §1 se registra en §4 y se comunica en el punto de control siguiente.

## 4. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara la DoD en **cuatro** capas —historia, tarea técnica, **momento del producto** y entrega del proyecto de código—, con el fundamento de por qué la tercera no se llama sprint ni etapa y la cuarta no se llama release. Cada criterio responde «cómo se valida» con una operación concreta. Incorpora en las cuatro capas la exigencia de que **toda medición de ausencia se haga con su condición y quede registrada**, y declara que `PT-02` y `PT-03` **no admiten excepción**: una puerta que no pasa impide comprometer la etapa `g`. Declara **cinco** casos de excepción, tres de ellos sin excepción posible, y la vigencia como fuente canónica, confirmando la ubicación de las tres condiciones de cierre que la Definition of Ready §5 había nombrado como ajenas. |
