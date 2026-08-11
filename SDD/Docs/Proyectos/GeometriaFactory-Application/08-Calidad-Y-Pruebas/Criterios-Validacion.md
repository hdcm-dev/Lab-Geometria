# Criterios de validación — GeometriaFactory-Application

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** Criterios-Validacion.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) 1.1; [`Estrategia-Calidad.md`](Estrategia-Calidad.md) 1.0 §3; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §8; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.19** §15, §17.2.P.6, §17.2.P.8, §17.2.P.10 y §22
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

Define qué significa que `GeometriaFactory-Application` está **validado**. Como este proyecto de código no es una unidad de despliegue —no tiene proceso propio y no se publica en ningún repositorio de paquetes (`05` §5)—, «validado» no quiere decir «liberado»: quiere decir **que la capa de casos de uso puede sostener la etapa que la usa y las dos capas que dependen de ella**.

El momento en que se aplican estos criterios es el **punto de control de cada etapa**, que el intake §15 declara bloqueante. No hay una fecha de liberación que preparar, porque el intake declara sin plazo calendario.

**Un criterio de este documento se cumple o no se cumple; no hay cumplimiento parcial.** Cuando uno no se cumple, la salida es la de §6 y nunca el silencio.

## 2. Criterios funcionales

| Id | Criterio | Cómo se comprueba | Umbral |
| --- | --- | --- | --- |
| CV-01 | Los **once** casos de uso tienen al menos un caso de prueba en verde, y cada criterio Given-When-Then declarado en sus historias está cubierto | [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2, columna de estado | **11 de 11** |
| CV-02 | Las **dieciséis** reglas de negocio tienen al menos un caso de prueba en verde | Matriz §4 | **16 de 16** |
| CV-03 | Las **cuatro** comprobaciones de autorización tienen prueba de su negativa **sin base de datos**, y existe **una sola** prueba de que la cuarta corta antes que las otras tres | Matriz §5, y `TC-11` | **4 de 4**, con **1** prueba de orden |
| CV-04 | Los **nueve** invariantes tienen al menos un caso de prueba que verifica lo que esta capa aporta a cada uno | Matriz §6 | **9 de 9** |
| CV-05 | Las **36** condiciones del catálogo están alcanzadas por al menos una prueba, y no se emite ninguna condición fuera del catálogo | `TC-28`, comparación en las dos direcciones | **36 de 36** y **0** fuera |
| CV-06 | Ninguna condición prevista viaja como excepción de control de flujo | `TC-31` | **0** excepciones de negocio |
| CV-07 | Los **ocho** escenarios del intake §20 están ejercitados como resultado de interpretación, **sin sustituirlos por datos inventados** | `TC-15`, `TC-16`, `TC-17` y `TC-22`, verificados uno por uno en [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) §3 | **8 de 8** |
| CV-08 | Las **treinta y dos** historias de usuario tienen su caso de prueba | Matriz §2, columna de historias, cruzada con [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §3 | **32 de 32** |

## 3. Criterios no funcionales

Uno por cada NFR de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §8. Los dos primeros llevan su rótulo **[ASUNCIÓN]** porque así vienen del intake y **no son compromisos** hasta que el Product Owner los confirme.

| Id | Criterio | Umbral | Cómo se mide | Carácter |
| --- | --- | --- | --- | --- |
| CV-09 | El caso de uso más pesado resuelve el envío del texto de **3** piezas de `E-1` **sin acceso a base** | **500 ms** **[ASUNCIÓN del intake §17.2.P.10, asunción `A-5` de §22]** | Cronometrado dentro de la batería unitaria con doble del puerto de validación, por `BT-19` | **Condicionado**: se mide y se registra; no bloquea hasta la confirmación |
| CV-10 | La cobertura alcanza **85 %** de líneas y **80 %** de ramas, **por componente y no como número global** | 85 / 80 **[ASUNCIÓN del intake §17.2.P.6, asunción `A-3` de §22]**, con los cuatro componentes que suben declarados en [`Estrategia-Testing.md`](Estrategia-Testing.md) §2 | Informe de cobertura por componente de la etapa `test` | **Condicionado** |
| CV-11 | Ninguna prueba de esta capa toca la base de datos real | **0** | `TC-26` y revisión del pull request | **Bloqueante.** Es la puerta propia que el intake §17.2.P.8 declara |
| CV-12 | El archivo de proyecto declara **1** referencia al producto y **0** a persistencia, transporte, serialización o marco web | 1 y 0 | `TC-27` | **Bloqueante** |
| CV-13 | Las consultas de listado no materializan componentes de pieza | **0** en los dos listados | `TC-30` | **Bloqueante** |
| CV-14 | El catálogo de condiciones cierra en las dos direcciones | 36 de 36 y 0 fuera | `TC-28` | **Bloqueante** |
| CV-15 | Las cuatro comprobaciones están ejercidas sin base de datos, con la prueba de orden presente | 4 de 4, 1 de orden | `TC-11` y matriz §5 | **Bloqueante** |
| CV-16 | Ningún caso de uso reparte su efecto entre dos unidades de trabajo | **A lo sumo 1** por caso de uso | `TC-29`, con la baja como caso testigo | **Bloqueante** |
| CV-17 | La construcción termina en 0 y **sin advertencias** | 0 advertencias | Etapa `build`; intake §17.2.P.8 | **Bloqueante** |

**No hay criterio de throughput ni de disponibilidad, y es correcto que no lo haya**: este proyecto de código no atiende peticiones ni abre conexiones (`05` §8, cierre de la sección). Inventar un umbral de esos dos sería inventar un sujeto que no existe.

**No se declara ningún tiempo de ejecución de la batería.** El único tiempo de este proyecto de código es el de `CV-09`, que es por caso de uso y viene del intake con su rótulo. Ninguna fuente da un tiempo de suite para esta capa.

## 4. Criterios de regresión

| Id | Criterio | Umbral |
| --- | --- | --- |
| CV-18 | La batería completa se ejecuta entera al cerrar cada etapa, y no sólo los casos de prueba que la etapa tocó | 100 % de los `TC-XX` escritos hasta ese momento |
| CV-19 | **Ningún caso de prueba que estaba en verde en la etapa anterior pasa a rojo** sin justificación escrita en el informe de cierre de la etapa | 0 regresiones sin justificar |
| CV-20 | Todo defecto cerrado generó al menos un `TC-XX` nuevo o extendió uno existente, con su fila en la matriz | 1 caso de prueba por defecto cerrado, como mínimo |
| CV-21 | `TC-11` —la prueba de orden de la cuarta comprobación— se ejecuta en **todas** las etapas a partir de la `d` | Presente en cada ejecución. Es la prueba de regresión del riesgo de impacto **muy alto** de `05` §9 |
| CV-22 | `TC-26` y `TC-27` se ejecutan en **todas** las etapas, incluida la `a` | Presentes en cada ejecución. Una dependencia nueva o una prueba que abra el almacén se detectan en la etapa que las introduce y no al final |

**La regla de no regresión es acumulativa por diseño.** El intake §15, regla de delivery 1, declara que al cerrar cada etapa deben seguir pasando los guiones de todas las anteriores, sin correcciones.

## 5. Criterios de calidad de código

| Id | Criterio | Umbral | Carácter |
| --- | --- | --- | --- |
| CV-23 | Cobertura por componente cumplida, con los siete componentes con umbral reportados por separado | Tabla de [`Estrategia-Testing.md`](Estrategia-Testing.md) §2 | **Condicionado**, por depender de `CV-10` |
| CV-24 | Mutation score | **60 %**, piso de `Rules-Calidad-Y-Pruebas.md` §2.2 para el tipo `library`. **Ninguna fuente del producto lo declara** | **No exigible todavía**: la herramienta no está elegida ni corre en el pipeline (hueco declarado en [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §8). Hasta entonces se reporta «sin medir» |
| CV-25 | El análisis estático no introduce advertencias nuevas | 0 advertencias nuevas | **Bloqueante**, por `CV-17` |
| CV-26 | Ningún caso de prueba está deshabilitado sin motivo escrito en su fila del catálogo | 0 deshabilitados sin motivo | **Bloqueante** |
| CV-27 | Ningún caso de prueba depende del orden de ejecución ni del reloj del entorno | 0 dependencias de orden; el momento entra siempre por el doble del puerto de reloj (`TC-13`) | **Bloqueante** |
| CV-28 | Ninguna prueba sustituye un componente interno con un doble: los dobles son **sólo de puerto** | 0 dobles de componente interno | **Bloqueante**, por [`Estrategia-Testing.md`](Estrategia-Testing.md) §5 |

## 6. Excepciones documentadas

**Un criterio no cumplido no se acepta en silencio.** Las tres únicas salidas admitidas:

| Situación | Salida admitida | Quién la aprueba |
| --- | --- | --- |
| Criterio **condicionado** —`CV-09`, `CV-10`, `CV-23`— no alcanzado | Se registra la medición y su distancia al umbral en el informe de cierre de la etapa, y **no bloquea**, porque el umbral es un valor rotulado [ASUNCIÓN] que el Product Owner todavía no confirmó (`BT-18`) | Nadie: es el tratamiento declarado, no una excepción concedida |
| Criterio **no exigible todavía** —`CV-24`— | Se reporta «sin medir» con el hueco citado. **No se reporta un número inventado ni se declara cumplido** | — |
| Criterio **bloqueante** no cumplido | Se abre una tarea técnica en [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md) con la remediación, y la etapa **no cierra** hasta que se cumpla o hasta que el Product Owner acepte la excepción por escrito en el punto de control | El Product Owner, con constancia escrita en el informe de cierre |

**Lo que no es una excepción admitida:** bajar un umbral para que cierre, deshabilitar un caso de prueba para que la batería pase, mover una prueba a la batería de integración de `GeometriaFactory-Api` **para esquivar `CV-11`** en lugar de porque ahí es donde pertenece, sustituir un escenario del intake por un resultado que dé el desenlace esperado, o declarar cumplido un criterio cuya medición no se hizo.

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara **veintiocho** criterios de validación numerados `CV-01` a `CV-28`, repartidos en funcionales, no funcionales, de regresión y de calidad de código, cada uno con su umbral y su forma de medición. Distingue tres caracteres —bloqueante, condicionado y no exigible todavía— y ata los condicionados a los dos valores rotulados **[ASUNCIÓN]** del intake §22, que se citan con su rótulo y no se convierten en compromiso. Declara que no hay criterio de throughput ni de disponibilidad porque no tienen sujeto acá, y que no se declara ningún tiempo de ejecución de suite que ninguna fuente dé. Declara las tres salidas admitidas ante un criterio no cumplido y lo que explícitamente no lo es, incluida la de mudar una prueba a la batería de integración para esquivar la puerta propia del intake. |
