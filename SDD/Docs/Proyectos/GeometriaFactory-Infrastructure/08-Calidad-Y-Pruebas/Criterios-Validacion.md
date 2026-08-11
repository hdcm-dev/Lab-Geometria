# Criterios de validación — GeometriaFactory-Infrastructure

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** Criterios-Validacion.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) 1.1; [`Estrategia-Calidad.md`](Estrategia-Calidad.md) 1.1 §3; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §8 y §11; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.20** §15, §17.3.P.6, §17.3.P.8, §17.3.P.10, §21 y §22
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

Define qué significa que `GeometriaFactory-Infrastructure` está **validado**. Como este proyecto de código no es una unidad de despliegue —viaja embebido en el proceso de `GeometriaFactory-Api`—, «validado» no quiere decir «liberado»: quiere decir **que el borde del sistema puede sostener la etapa que lo usa sin perder un dato, sin producir un secreto adivinable y sin engañar al alumno sobre por qué su texto no se interpretó**.

El momento en que se aplican estos criterios es el **punto de control de cada etapa**, que el intake §15 declara bloqueante.

**Un criterio de este documento se cumple o no se cumple; no hay cumplimiento parcial.** Cuando uno no se cumple, la salida es la de §6 y nunca el silencio.

## 2. Criterios funcionales

| Id | Criterio | Cómo se comprueba | Umbral |
| --- | --- | --- | --- |
| CV-01 | Los **diez** casos de uso tienen al menos un caso de prueba en verde, y cada criterio Given-When-Then de sus historias está cubierto | [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2 | **10 de 10** |
| CV-02 | **La batería del validador pasa entera**, con los **ocho** escenarios del intake §20 como entrada | Matriz §6 y `TC-01` a `TC-10` | **10 de 10.** Ver §6 sobre el recuento |
| CV-03 | Las **veinticinco** historias de usuario tienen su caso de prueba | Matriz §2, columna de historias | **25 de 25** |
| CV-04 | Las **dieciséis** reglas de negocio tienen verificado el tramo que esta capa ejerce, y las **dos** sin tramo tienen verificado que **esta capa guarda el dato y no lo comprueba** | Matriz §4 | **16 de 16**, con **14** con tramo y **2** sin él |
| CV-05 | Las **siete** reglas conceptuales de modelo tienen caso de prueba | Matriz §5 | **7 de 7** |
| CV-06 | Las **17** condiciones del catálogo están alcanzadas por al menos una prueba, y no se emite ninguna condición fuera del catálogo | `TC-34`, comparación en las dos direcciones | **17 de 17** y **0** fuera |
| CV-07 | Los **ocho** escenarios del intake §20 están ejercitados **como texto literal**, sin sustituirlos por textos escritos a mano | `TC-01` a `TC-11` y `TC-16`, verificados uno por uno en [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) §3 | **8 de 8** |
| CV-08 | El escenario `E-1` produce **3 piezas y exactamente 2 advertencias**, y el cilindro **no produce ninguna observación** | `TC-09` | 3 y **2**. **Una tercera advertencia significa que el operador de tolerancia dejó de ser estricto** |
| CV-09 | Un texto **ilegible** produce una observación de validación y **no** la condición de motor no disponible | `TC-13` | Tres resultados distintos y **0** confusiones entre resultado y fallo |

## 3. Criterios no funcionales

Uno por cada NFR de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §8. Los tres primeros llevan su rótulo **[ASUNCIÓN]** porque así vienen del intake.

| Id | Criterio | Umbral | Cómo se mide | Carácter |
| --- | --- | --- | --- | --- |
| CV-10 | La interpretación del texto de **3** piezas de `E-1` termina **sin almacén** | **200 ms** **[ASUNCIÓN del intake §17.3.P.10, asunción `A-5` de §22]** | `TC-15` | **Condicionado**: se mide y se registra; no bloquea hasta la confirmación |
| CV-11 | La cobertura del proyecto de código, **por componente y no como número global** | **85 %** de líneas y **80 %** de ramas **[ASUNCIÓN del intake §17.3.P.6, asunción `A-3`]** | Informe de cobertura por componente | **Condicionado** |
| CV-12 | La cobertura del **validador de figuras**, medida sobre los **dos motores** | **95 %** de líneas **[ASUNCIÓN del intake §17.3.P.6]**. Es el número más alto del producto | Informe acotado a los dos motores | **Condicionado** |
| CV-13 | La comparación de valores usa tolerancia **0.01** absoluta con operador **estricto** | 0.01, estricto | `TC-09` | **Bloqueante, y no condicionado.** El intake §22 lo excluye expresamente de las asunciones |
| CV-14 | Peticiones de red originadas por los dos motores | **0** | `TC-14` | **Bloqueante** |
| CV-15 | Aplicación de transformaciones sobre un almacén inexistente, sin paso manual | **1 de 1** | `TC-32` | **Bloqueante.** Es criterio de aceptación de la etapa `c` |
| CV-16 | Provisorias iguales en dos producciones consecutivas, sobre la misma cuenta y entre cuentas | **0**, y **0** derivables del nombre, del correo o de la fecha | `TC-27` | **Bloqueante** |
| CV-17 | Componentes de pieza y apariciones del texto original en una proyección de listado | **0** y **0** | `TC-19` | **Bloqueante** |
| CV-18 | Escrituras aceptadas que reemplacen el texto original conservado | **0** | `TC-16` | **Bloqueante** |
| CV-19 | Retiros parciales tras una baja interrumpida | **0** | `TC-21`, con el almacén interrumpido a mitad de operación | **Bloqueante** |
| CV-20 | Emisiones de acceso sin clave de firma, y claves generadas al vuelo | **0** y **0** | `TC-30` | **Bloqueante** |
| CV-21 | Mensajes y trazas con un secreto, la ruta del almacén o el texto del alumno, **en las dos direcciones** —mensaje y registro del servidor— | **0** y **0** | `TC-35` | **Bloqueante** |
| CV-22 | Cobertura del catálogo de condiciones, en las dos direcciones | 17 de 17 y 0 fuera | `TC-34` | **Bloqueante** |
| CV-23 | La construcción termina en 0 y **sin advertencias** | 0 advertencias | Etapa `build`; intake §17.3.P.8 | **Bloqueante** |

**No hay criterio de disponibilidad ni de caudal, y es correcto que no lo haya.** El intake §17.3.P.10 declara «sin SLO» para este proyecto de código, y quien tiene sujeto para el caudal es `GeometriaFactory-Api`, que es el que recibe peticiones.

**No se declara ningún tiempo de ejecución de la batería.** El único tiempo de este proyecto de código es el de `CV-10`, que es de **interpretación** y viene del intake con su rótulo.

## 4. Criterios de regresión

| Id | Criterio | Umbral |
| --- | --- | --- |
| CV-24 | La batería completa se ejecuta entera al cerrar cada etapa, y no sólo los casos que la etapa tocó | 100 % de los `TC-XX` escritos hasta ese momento |
| CV-25 | **Ningún caso de prueba que estaba en verde en la etapa anterior pasa a rojo** sin justificación escrita | 0 regresiones sin justificar |
| CV-26 | **Los diez casos de la batería del validador se reejecutan en toda etapa posterior a la `f`**, y no sólo en ella | 10 de 10 en cada ejecución. Es el riesgo de negocio que la fuente pone primero |
| CV-27 | Todo defecto cerrado generó al menos un `TC-XX` nuevo o extendió uno existente | 1 caso de prueba por defecto cerrado, como mínimo |
| CV-28 | Los casos de los tres modos de falla que **no se notan** —`TC-28`, `TC-30`, `TC-33`— se ejecutan en **todas** las etapas a partir de aquella en que su sujeto existe | Presentes en cada ejecución. Son los que `05` §9 declara de impacto muy alto |

**La regla de no regresión es acumulativa por diseño.** El intake §15, regla de delivery 1, la declara: al cerrar cada etapa deben seguir pasando los guiones de todas las anteriores, sin correcciones.

## 5. Criterios de calidad de código

| Id | Criterio | Umbral | Carácter |
| --- | --- | --- | --- |
| CV-29 | Cobertura por componente cumplida, con los **ocho** componentes reportados por separado y **el informe de los dos motores reportado aparte** | Tabla de [`Estrategia-Testing.md`](Estrategia-Testing.md) §2 | **Condicionado**, por depender de `CV-11` y `CV-12` |
| CV-30 | Mutation score | **60 %**, piso de `Rules-Calidad-Y-Pruebas.md` §2.2 para el tipo `library`. **Ninguna fuente del producto lo declara** | **No exigible todavía**: la herramienta no está elegida ni corre en el pipeline. Hasta entonces se reporta «sin medir». **El adaptador de reloj queda exento con su fundamento** |
| CV-31 | El análisis estático no introduce advertencias nuevas | 0 advertencias nuevas | **Bloqueante**, por `CV-23` |
| CV-32 | Ningún caso de prueba está deshabilitado sin motivo escrito en su fila del catálogo | 0 deshabilitados sin motivo | **Bloqueante** |
| CV-33 | Ninguna prueba de integración interna usa el almacén de desarrollo ni el de producción: cada una **crea y descarta el suyo** | 0 usos del almacén compartido | **Bloqueante** |
| CV-34 | Ningún texto de figuras usado como dato de prueba está escrito a mano: **todos salen del intake §20** | 0 textos escritos a mano | **Bloqueante**. Es la mitigación del riesgo de negocio que la fuente pone primero |
| CV-35 | Ninguna prueba deja un secreto real en el repositorio: la clave de firma de prueba es **evidentemente ficticia** y llega por configuración | 0 secretos reales | **Bloqueante** |

## 6. Excepciones documentadas

**Un criterio no cumplido no se acepta en silencio.** Las tres únicas salidas admitidas:

| Situación | Salida admitida | Quién la aprueba |
| --- | --- | --- |
| Criterio **condicionado** —`CV-10`, `CV-11`, `CV-12`, `CV-29`— no alcanzado | Se registra la medición y su distancia al umbral en el informe de cierre, y **no bloquea**, porque el umbral es un valor rotulado [ASUNCIÓN] que el Product Owner todavía no confirmó | Nadie: es el tratamiento declarado, no una excepción concedida |
| Criterio **no exigible todavía** —`CV-30`— | Se reporta «sin medir» con el hueco citado. **No se reporta un número inventado** | — |
| Criterio **bloqueante** no cumplido | Se abre una tarea técnica en [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md) con la remediación, y la etapa **no cierra** hasta que se cumpla o hasta que el Product Owner acepte la excepción por escrito | El Product Owner, con constancia escrita |

**Sobre `CV-02` y el recuento de la batería.** El intake **1.20** escribe «las **diez** pruebas del validador pasan» en §17.3.P.8 e «incluidas las **diez** pruebas del validador» en §17.5.P.8, y su §21 tiene **diez** filas, la décima incorporada con `E-8` bajo el rótulo **[DECISIÓN 2026-08-09]**. **Hasta 1.19 los dos gates escribían nueve**, y esta categoría aplicó diez igual, apoyada en `05` §8 y §10.5, que ya habían resuelto la lectura; la fuente lo corrigió en 1.20 y la divergencia está cerrada. **Cerrar la etapa con nueve casos y declarar cumplido `CV-02` no es una excepción admitida**: dejaría sin cubrir el escenario que cerró la única condición del contrato de fachada que no tenía dato de prueba.

**Lo que tampoco es una excepción admitida:** bajar un umbral para que cierre; deshabilitar un caso de prueba para que la batería pase; **escribir a mano un texto de figuras porque el del intake es largo**; declarar cumplido un NFR de umbral cero por no haber observado lo contrario, sin haberlo medido en su condición; o dejar un secreto real en una prueba.

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **`H-01`.** La nota de §6 sobre `CV-02` afirmaba en presente que el intake escribe «nueve»; el intake **1.20** dice **diez** en §17.3.P.8 y §17.5.P.8. Reescrita contra el texto vivo, con el nueve ubicado **hasta 1.19**, y la remisión de `CV-02` sin la referencia al recuento viejo. El umbral sigue siendo **10 de 10**. Corrige contra [`../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md`](../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md) 1.0 y contra el texto vivo del intake **1.20**. |
| 1.0 | 2026-08-11 | Emisión inicial. Declara **treinta y cinco** criterios de validación numerados `CV-01` a `CV-35`, repartidos en funcionales, no funcionales, de regresión y de calidad de código, cada uno con su umbral y su forma de medición. Distingue tres caracteres —bloqueante, condicionado y no exigible todavía— y ata los condicionados a los **tres** valores rotulados **[ASUNCIÓN]** del intake §22, separando expresamente la **tolerancia de 0.01**, que el intake excluye de las asunciones y cuyo criterio es bloqueante. Declara que no hay criterio de disponibilidad ni de caudal porque no tienen sujeto acá, y que no se declara ningún tiempo de suite. Su §6 declara, además de las tres salidas admitidas, que **cerrar con nueve casos de batería amparándose en la redacción del gate del intake no es una excepción admitida**, y cinco situaciones más que tampoco lo son. |
