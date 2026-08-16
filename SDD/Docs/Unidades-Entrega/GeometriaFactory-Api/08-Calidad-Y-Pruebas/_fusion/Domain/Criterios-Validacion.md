# Criterios de validación — GeometriaFactory-Domain

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** Criterios-Validacion.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) 1.1; [`Estrategia-Calidad.md`](Estrategia-Calidad.md) 1.0 §3; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/_fusion/Domain/Arquitectura-Proyecto-Codigo.md) 1.0 §8; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.19** §15, §17.1.P.6 · GeometriaFactory-Domain, §17.1.P.8 · GeometriaFactory-Domain, §17.1.P.10 · GeometriaFactory-Domain y §22
**Trazabilidad downstream:** [`Definition-Of-Done.md`](Definition-Of-Done.md); `09-Devops`

---

## Tabla de contenido

- [1. Propósito](#1-propósito)
- [2. Criterios funcionales](#2-criterios-funcionales)
- [3. Criterios no funcionales](#3-criterios-no-funcionales)
- [4. Criterios de regresión](#4-criterios-de-regresión)
- [5. Criterios de calidad de código](#5-criterios-de-calidad-de-código)
- [6. Excepciones documentadas](#6-excepciones-documentadas)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Propósito

Define qué significa que `GeometriaFactory-Domain` está **validado**. Como este proyecto de código no es una unidad de despliegue —no tiene proceso propio y no se publica en ningún repositorio de paquetes (`05` §5)—, «validado» no quiere decir «liberado»: quiere decir **que la biblioteca puede sostener la etapa que la usa**.

El momento en que se aplican estos criterios es el **punto de control de cada etapa**, que el intake §15 declara bloqueante. No hay una fecha de liberación que preparar, porque el intake declara sin plazo calendario.

**Un criterio de este documento se cumple o no se cumple; no hay cumplimiento parcial.** Cuando uno no se cumple, la salida es la de §6 y nunca el silencio.

## 2. Criterios funcionales

| Id | Criterio | Cómo se comprueba | Umbral |
| --- | --- | --- | --- |
| CV-01 | Los **trece** casos de uso tienen al menos un caso de prueba en verde, y cada criterio Given-When-Then declarado en sus historias está cubierto | [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2, columna de estado | **13 de 13** |
| CV-02 | Las **dieciséis** reglas de negocio tienen al menos un caso de prueba en verde | Matriz §4 | **16 de 16** |
| CV-03 | Los **nueve** invariantes tienen al menos una prueba que verifica **su violación rechazada**, y ninguna de esas pruebas usa dobles | Matriz §5, recorrida por `TC-02026` | **9 de 9**, con **0** dobles |
| CV-04 | Las **42** condiciones del catálogo están alcanzadas por al menos una prueba, y no se emite ninguna condición fuera del catálogo | `TC-02023`, comparación en las dos direcciones | **42 de 42** y **0** fuera |
| CV-05 | Ninguna condición prevista viaja como excepción de control de flujo | `TC-02027` | **0** excepciones de negocio |
| CV-06 | Los **ocho** escenarios del intake §20 están ejercitados como fixture, con sus resultados declarados y **sin sustituirlos por datos sintéticos** | `TC-02013` a `TC-02018`, verificados uno por uno en [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) §3 | **8 de 8** |
| CV-07 | Las **veintisiete** historias de usuario tienen su caso de prueba | Matriz §2, columna de test, cruzada con [`../06-Backlog-Tecnico/Product-Backlog.md`](../../../06-Backlog-Tecnico/_fusion/Domain/Product-Backlog.md) §3 | **27 de 27** |

## 3. Criterios no funcionales

Uno por cada NFR de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/_fusion/Domain/Arquitectura-Proyecto-Codigo.md) §8. Los dos primeros llevan su rótulo **[ASUNCIÓN]** porque así vienen del intake y **no son compromisos** hasta que el Product Owner los confirme.

| Id | Criterio | Umbral | Cómo se mide | Carácter |
| --- | --- | --- | --- | --- |
| CV-08 | La batería de dominio completa termina en menos de **10 segundos** | 10 s **[ASUNCIÓN del intake §17.1.P.10 · GeometriaFactory-Domain, asunción `A-5` de §22]** | Duración total reportada por el ejecutor en la etapa `test` | **Condicionado**: se mide y se registra; no bloquea hasta la confirmación |
| CV-09 | La cobertura alcanza **90 %** de líneas y **85 %** de ramas, **por componente y no como número global** | 90 / 85 **[ASUNCIÓN del intake §17.1.P.6 · GeometriaFactory-Domain, asunción `A-3` de §22]**, con los tres componentes que suben declarados en [`Estrategia-Testing.md`](Estrategia-Testing.md) §2 | Informe de cobertura por componente de la etapa `test` | **Condicionado** |
| CV-10 | El archivo de proyecto declara **0** referencias a otros proyectos de código del producto y **0** a bibliotecas de persistencia, transporte o serialización | 0 y 0 | `TC-02024` y revisión del pull request | **Bloqueante** |
| CV-11 | El catálogo de condiciones cierra en las dos direcciones | 42 de 42 y 0 fuera | `TC-02023` | **Bloqueante** |
| CV-12 | Los nueve invariantes están ejercidos sin dobles | 9 de 9, 0 dobles | `TC-02026` | **Bloqueante** |
| CV-13 | La construcción termina en 0 y **sin advertencias** | 0 advertencias | Etapa `build`; intake §17.1.P.8 · GeometriaFactory-Domain | **Bloqueante** |

**No hay criterio de latencia, de throughput ni de disponibilidad, y es correcto que no lo haya**: este proyecto de código no atiende peticiones ni abre conexiones (`05` §8, cierre de la sección). Inventar un umbral de esos tres sería inventar un sujeto que no existe.

**No se declara ningún otro tiempo de ejecución.** El único que existe es el de `CV-08`, y viene del intake con su rótulo.

## 4. Criterios de regresión

| Id | Criterio | Umbral |
| --- | --- | --- |
| CV-14 | La batería completa se ejecuta entera al cerrar cada etapa, y no sólo los casos de prueba que la etapa tocó | 100 % de los `TC-XX` escritos hasta ese momento |
| CV-15 | **Ningún caso de prueba que estaba en verde en la etapa anterior pasa a rojo** sin justificación escrita en el informe de cierre de la etapa | 0 regresiones sin justificar |
| CV-16 | Todo defecto cerrado generó al menos un `TC-XX` nuevo o extendió uno existente, con su fila en la matriz | 1 caso de prueba por defecto cerrado, como mínimo |
| CV-17 | `TC-02005` —las cinco operaciones rechazadas sobre la cuenta de administrador— se ejecuta en **todas** las etapas a partir de la `d` | Presente en cada ejecución. Es la prueba de regresión de la familia de defectos que en este producto **se abrió dos veces** |

**La regla de no regresión es acumulativa por diseño.** El intake declara que cada etapa reejecuta lo anterior, y eso es lo que hace caro que la batería crezca en tiempo: es el motivo por el que `CV-08` existe.

## 5. Criterios de calidad de código

| Id | Criterio | Umbral | Carácter |
| --- | --- | --- | --- |
| CV-18 | Cobertura por componente cumplida, con los cinco componentes reportados por separado | Tabla de [`Estrategia-Testing.md`](Estrategia-Testing.md) §2 | **Condicionado**, por depender de `CV-09` |
| CV-19 | Mutation score en dominio | **60 %**, piso de `Rules-Calidad-Y-Pruebas.md` §2.2 para el tipo `library`. **Ninguna fuente del producto lo declara** | **No exigible todavía**: la herramienta no está elegida ni corre en el pipeline (hueco declarado en [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §7). Hasta entonces se reporta «sin medir» |
| CV-20 | El análisis estático no introduce advertencias nuevas | 0 advertencias nuevas | **Bloqueante**, por `CV-13` |
| CV-21 | Ningún caso de prueba está deshabilitado sin motivo escrito en su fila del catálogo | 0 deshabilitados sin motivo | **Bloqueante** |
| CV-22 | Ningún caso de prueba depende del orden de ejecución ni de un reloj del entorno | 0 dependencias de orden; dos ejecuciones consecutivas con resultado idéntico (`TC-02025`) | **Bloqueante** |

## 6. Excepciones documentadas

**Un criterio no cumplido no se acepta en silencio.** Las tres únicas salidas admitidas:

| Situación | Salida admitida | Quién la aprueba |
| --- | --- | --- |
| Criterio **condicionado** —`CV-08`, `CV-09`, `CV-18`— no alcanzado | Se registra la medición y su distancia al umbral en el informe de cierre de la etapa, y **no bloquea**, porque el umbral es un valor rotulado [ASUNCIÓN] que el Product Owner todavía no confirmó (`BT-02015`) | Nadie: es el tratamiento declarado, no una excepción concedida |
| Criterio **no exigible todavía** —`CV-19`— | Se reporta «sin medir» con el hueco citado. **No se reporta un número inventado ni se declara cumplido** | — |
| Criterio **bloqueante** no cumplido | Se abre una tarea técnica en [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../../../06-Backlog-Tecnico/_fusion/Domain/Backlog-Tecnico.md) con la remediación, y la etapa **no cierra** hasta que se cumpla o hasta que el Product Owner acepte la excepción por escrito en el punto de control | El Product Owner, con constancia escrita en el informe de cierre |

**Lo que no es una excepción admitida:** bajar un umbral para que cierre, deshabilitar un caso de prueba para que la batería pase, sustituir un escenario del intake por un dato que dé el resultado esperado, o declarar cumplido un criterio cuya medición no se hizo.

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara **veintidós** criterios de validación numerados `CV-01` a `CV-22`, repartidos en funcionales, no funcionales, de regresión y de calidad de código, cada uno con su umbral y su forma de medición. Distingue tres caracteres —bloqueante, condicionado y no exigible todavía— y ata los condicionados a los dos valores rotulados **[ASUNCIÓN]** del intake §22, que se citan con su rótulo y no se convierten en compromiso. Declara que no hay criterio de latencia, throughput ni disponibilidad porque no tienen sujeto acá, y que no se declara ningún tiempo de ejecución que ninguna fuente dé. Declara las tres salidas admitidas ante un criterio no cumplido y lo que explícitamente no lo es. |
