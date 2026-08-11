# Estrategia de calidad — GeometriaFactory-Infrastructure

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** Estrategia-Calidad.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3, §4, §5 y §6; [`../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md`](../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md) §7; [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md) §7.1; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §3.1, §8, §9, §10.5 y §11; [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/Definition-Of-Ready.md) §5; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.19** §15, §17.3.P.6, §17.3.P.8, §17.3.P.10, §20, §21 y §22
**Trazabilidad downstream:** [`Estrategia-Testing.md`](Estrategia-Testing.md), [`Plan-Pruebas.md`](Plan-Pruebas.md), [`Criterios-Validacion.md`](Criterios-Validacion.md), [`Definition-Of-Done.md`](Definition-Of-Done.md); `09-Devops`, que materializa como etapas del pipeline los quality gates de §3; `11-Documentacion`, que cita esta estrategia sin redefinirla

---

## Tabla de contenido

- [1. Definición de calidad para este proyecto de código](#1-definición-de-calidad-para-este-proyecto-de-código)
- [2. Atributos de calidad priorizados](#2-atributos-de-calidad-priorizados)
- [3. Quality gates](#3-quality-gates)
  - [3.1 Qué significa que un gate esté condicionado](#31-qué-significa-que-un-gate-esté-condicionado)
  - [3.2 La batería del validador tiene diez casos y el intake escribe nueve en dos lugares](#32-la-batería-del-validador-tiene-diez-casos-y-el-intake-escribe-nueve-en-dos-lugares)
- [4. Roles de calidad dentro del equipo](#4-roles-de-calidad-dentro-del-equipo)
- [5. Cadencia de revisión](#5-cadencia-de-revisión)
- [6. Control de cambios](#6-control-de-cambios)

---

## 1. Definición de calidad para este proyecto de código

`GeometriaFactory-Infrastructure` tiene calidad cuando **el validador interpreta el texto real del alumno tal como su programa lo emite, con sus cuatro trampas de formato, y señala sin corregir y sin rechazar**; cuando **ninguna operación del almacén deja efecto parcial ni pierde el texto original**; y cuando **los dos mecanismos que el producto no puede permitirse mal hechos —la derivación de credenciales y la emisión del acceso firmado— fallan hacia el rechazo y nunca hacia un valor adivinable ni hacia una firma improvisada**.

Las tres partes tienen un rasgo común que conviene decir de una vez: **acá los defectos no se notan.** Una provisoria adivinable no se nota hasta que alguien la usa; un acceso emitido sin clave no se nota hasta que alguien lo falsifica; un almacén recreado en lugar de transformado deja el servicio impecable y **sin los trabajos de nadie**; y un validador escrito sin leer el análisis funciona con datos inventados y falla con el dato que existe. `05` §9 declara **cinco** riesgos de impacto muy alto o alto con ese perfil.

La consecuencia operativa es que esta categoría **no confía en la ausencia de síntomas**: cada una de esas propiedades tiene un caso de prueba con umbral numérico, y la mayoría de esos umbrales es exactamente **cero**.

## 2. Atributos de calidad priorizados

Clasificación ISO/IEC 25010, con la métrica de origen cuando existe. Los valores rotulados **[ASUNCIÓN]** vienen así desde el intake y **no son compromisos**: se usan como vigentes hasta que el Product Owner los confirme (§22 del intake, asunciones `A-3` y `A-5`).

| Atributo ISO 25010 | Prioridad | Métrica y origen |
| --- | --- | --- |
| Adecuación funcional | **Crítica** | **10 de 10** casos de la batería del validador con los **ocho** escenarios `E-1` a `E-8` como entrada (`05` §8 y §10.5); **10 de 10** casos de uso con caso de prueba; tolerancia **0.01** con operador **estricto**, que **no es asunción** |
| Seguridad | **Crítica** | **0** provisorias iguales en dos producciones consecutivas y entre cuentas; **0** emisiones de acceso sin clave de firma; **0** contraseñas guardadas o registradas en claro; **0** mensajes o trazas con un secreto, la ruta del almacén o el texto del alumno (`05` §8) |
| Fiabilidad | **Crítica** | **0** retiros parciales tras una baja interrumpida; **0** escrituras aceptadas que reemplacen el texto original conservado; **1 de 1** aplicación de transformaciones sobre almacén inexistente, sin paso manual |
| Eficiencia de desempeño | **Alta** | Interpretación del texto de **3** piezas de `E-1` en menos de **200 ms**, medida **sin almacén** [ASUNCIÓN del intake §17.3.P.10]; **0** componentes de pieza y **0** apariciones del texto original en una proyección de listado |
| Mantenibilidad | **Alta** | **95 %** de líneas en el validador de figuras [ASUNCIÓN del intake §17.3.P.6], **el número más alto del producto** y puesto donde la fuente señala el criterio que más veces se rompe; **0** advertencias de construcción |
| Compatibilidad | **Media** | Implementa los **cuatro** puertos que `GeometriaFactory-Application` declara, y no los redefine; provee **dos** mecanismos y **una** responsabilidad de arranque que ningún puerto declara |
| Usabilidad | **No aplica como atributo de interfaz** | `tiene_ui_final` es false (`PRODUCT-MANIFEST` §5). Su equivalente es la experiencia del desarrollador, que documenta [`../03-UX-UI-DX/DX-Developer-Experience.md`](../03-UX-UI-DX/DX-Developer-Experience.md) |
| Portabilidad | **Baja** | Plataforma única sin sufijo de sistema operativo (intake §17.3.P.9), con el motor de almacenamiento embebido y anclado en la etapa `a` |

**Los tres atributos críticos son los que el resto del producto no puede reparar.** [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §6 lo dice con precisión: **tres reglas tienen su tramo principal acá** —`RN-08`, `RN-09` y `RN-14`— y «si acá se hacen mal, ninguna capa de más adentro puede repararlas». Esa frase es la que fija esta prioridad.

## 3. Quality gates

Cada gate declara condición, cómo se verifica y qué pasa cuando no se cumple. Los cuatro primeros los declara el intake §17.3.P.8; los demás los deriva [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §8, con una fila por NFR.

| Id | Condición | Cómo se verifica | Consecuencia si no se cumple |
| --- | --- | --- | --- |
| QG-01 | El guion de construcción termina en **0 y sin advertencias** | Etapa `build` del pipeline | **Bloquea la fusión** (intake §17.3.P.8) |
| QG-02 | El guion de pruebas pasa **entero**: cero pruebas rojas y cero deshabilitadas sin motivo escrito | Etapa `test` del pipeline | Bloquea la fusión |
| QG-03 | **La batería del validador pasa entera: 10 de 10**, con los **ocho** escenarios como entrada | `TC-01` a `TC-10`, contra la tabla de `05` §10.5 | Bloquea la fusión. Ver §3.2 sobre el recuento |
| QG-04 | **Las transformaciones de esquema se aplican solas sobre un almacén inexistente**, sin paso manual | Etapa de verificación de transformaciones del pipeline, y `TC-32` | Bloquea la fusión. Es criterio de aceptación de la etapa `c` (intake §17.3.P.8) |
| QG-05 | La cobertura del proyecto de código alcanza **85 %** de líneas y **80 %** de ramas [ASUNCIÓN del intake §17.3.P.6] | Informe de cobertura de la etapa `test`, **por componente** | **Condicionado**, ver §3.1 |
| QG-06 | La cobertura del **validador de figuras** alcanza **95 %** de líneas [ASUNCIÓN del intake §17.3.P.6] | Informe de cobertura acotado a los **dos motores** | **Condicionado**, ver §3.1 |
| QG-07 | La comparación de valores usa tolerancia **0.01** absoluta con operador **estricto**: el escenario `E-1` da **exactamente 2** advertencias y no 3 | `TC-09` | **Bloquea la fusión, y no es condicionado.** El intake §22 declara expresamente que la tolerancia **no es asunción**: sale de que el emisor redondea a 2 decimales |
| QG-08 | Los **dos motores** originan exactamente **0** peticiones de red | `TC-14`, inspección de dependencias de los dos motores | Bloquea la fusión |
| QG-09 | **0** provisorias iguales en dos producciones consecutivas sobre la misma cuenta y entre cuentas distintas, y ninguna derivable del nombre, del correo ni de la fecha | `TC-27` | Bloquea la fusión |
| QG-10 | **0** componentes de pieza cargados y **0** apariciones del texto original en una proyección de listado | `TC-19` | Bloquea la fusión |
| QG-11 | **0** escrituras aceptadas que reemplacen el texto original conservado, y **0** retiros parciales tras una baja interrumpida | `TC-16` y `TC-21` | Bloquea la fusión |
| QG-12 | **0** emisiones de acceso sin clave de firma, y **0** claves generadas al vuelo | `TC-30` | Bloquea la fusión |
| QG-13 | **100 %** de las **17** condiciones del catálogo alcanzadas por prueba, **0** emitidas fuera del catálogo, y **0** mensajes o trazas con un secreto, la ruta del almacén o el texto del alumno | `TC-34` y `TC-35`, comparación en las dos direcciones | Bloquea la fusión |
| QG-14 | La interpretación del texto de **3** piezas de `E-1` termina en menos de **200 ms**, medida **sin almacén** [ASUNCIÓN del intake §17.3.P.10] | `TC-15` | **Condicionado**, ver §3.1 |

**Catorce gates, y ninguno inventado.** Los que no salen del intake salen de una fila de `05` §8, que declara los **catorce** NFR de este proyecto de código.

**Una puerta técnica del producto se mide en la etapa `a` de este proyecto de código**: [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §2 asigna `PT-04` a su épica `EP-01`. Su umbral y su consecuencia son los del intake §15 —una puerta que no pasa **detiene la planificación de las etapas que dependen de ella**— y esta categoría no los mueve.

### 3.1 Qué significa que un gate esté condicionado

`QG-05`, `QG-06` y `QG-14` son los tres gates cuyo umbral es un valor rotulado **[ASUNCIÓN]** en el intake §22 —`A-3` para las dos coberturas, `A-5` para los 200 ms—. `PA-11` de `05` §11 declara el tratamiento y esta estrategia lo adopta sin cambiarlo: **los tres valores se usan como vigentes y la puerta no se declara bloqueante en `09-Devops` hasta que el Product Owner los confirme sobre su propio documento**.

Condicionado no quiere decir opcional. La medición se hace igual y el resultado se registra; lo que queda en suspenso es la consecuencia automática.

**Lo que no es condicionado, y conviene no confundir.** `QG-07` mide un número —**0.01**— que **no está rotulado [ASUNCIÓN]**: el intake §22 lo enumera entre «lo que NO es asunción», con su fundamento. Un gate condicionado por arrastre de ese número sería un error de lectura, y esta estrategia lo declara para que no ocurra.

### 3.2 La batería del validador tiene diez casos y el intake escribe nueve en dos lugares

**Esta categoría aplica diez, y declara por qué.**

- El intake **§21** cruza la batería obligatoria contra los escenarios y su tabla tiene **diez** filas: las nueve de la fuente técnica original más **«Dimensión no legible → `E-8`»**, que la propia fila rotula **[DECISIÓN 2026-08-09]**.
- El intake **§17.3.P.8** escribe «las **nueve** pruebas del validador pasan», y **§17.5.P.8** repite «incluidas las **nueve** pruebas del validador» en el pipeline de `GeometriaFactory-Api`.
- [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §8 y §10.5 ya resolvieron la lectura: **la batería tiene 10 casos**, «los nueve obligatorios de la fuente más el décimo que §21 agregó con `E-8`».

**Esta categoría hereda esa lectura y no la reabre.** El texto de los dos gates del intake es **anterior** a la incorporación del décimo caso y quedó sin propagar; corregirlo es del Product Owner sobre su propio documento. **Lo que esta categoría no hace es bajar la batería a nueve para que coincida con la redacción de la puerta**: el décimo caso cubre `E-8`, que §21 declara como el escenario que cerró la única condición del contrato de fachada que no tenía dato de prueba. Queda declarado como hueco en [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §8.

## 4. Roles de calidad dentro del equipo

`equipo_n` es **1** (intake §2): la misma persona diseña las pruebas, las ejecuta y aprueba el cierre.

| Papel | Quién | Qué le corresponde |
| --- | --- | --- |
| AG-08, calidad y pruebas | La única persona del equipo, en este papel | Diseñar los casos de prueba, mantener la matriz de cobertura y la Definition of Done, y declarar si un criterio de validación se cumple |
| Product Owner | El docente de la cátedra, que es también quien ejecuta | Aprobar el cierre de cada etapa en su punto de control, confirmar los tres valores rotulados [ASUNCIÓN] y **decidir sobre los puntos abiertos que `05` §11 le derivó** |
| Revisión mecánica | El pipeline | Los catorce gates de §3, en sus cuatro etapas: `restore`, `build`, `test` y **verificación de transformaciones** |

**Lo que reemplaza al revisor humano independiente es el punto de control bloqueante de cada etapa** (intake §15, regla de delivery 2). Esta categoría no inventa un segundo revisor que no existe.

## 5. Cadencia de revisión

| Momento | Qué se revisa | Qué produce |
| --- | --- | --- |
| Al abrir la rama de cada etapa | Qué casos de prueba de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) entran en alcance | El alcance de testing de la etapa, en [`Plan-Pruebas.md`](Plan-Pruebas.md) §5 |
| Al cerrar cada etapa | La matriz de cobertura entera; el estado de cada `TC-XX`; y **la batería del validador completa** a partir de la etapa `f` | Matriz actualizada y la constancia de los gates medidos |
| Al cerrar la etapa `f` | Los **diez** casos de la batería contra los **ocho** escenarios, uno por uno | La tabla de `05` §10.5 verificada fila por fila |
| Al cerrar la etapa `c` | Los tres valores rotulados [ASUNCIÓN] | La confirmación del Product Owner, o su continuidad como asunción |
| Ante todo defecto cerrado | Que exista al menos un `TC-XX` nuevo o extendido que lo prevenga | La entrada correspondiente en el catálogo de casos de prueba |

**La cadencia es por etapa y no por sprint**, porque este producto no tiene sprints: la unidad de planificación es la etapa. **No se declara ninguna frecuencia calendaria**: el intake declara «sin plazo calendario; el avance se mide por etapas cerradas».

## 6. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara la definición de calidad en sus tres partes —validador fiel al dato real, almacén sin efecto parcial y mecanismos que fallan hacia el rechazo—, con la constancia de que acá **los defectos no se notan** y de que por eso ninguna propiedad se da por buena por ausencia de síntomas. Declara los ocho atributos ISO 25010, los **catorce** quality gates con condición, verificación y consecuencia —tres condicionados por depender de un valor rotulado [ASUNCIÓN] en el intake §22— y la constancia de que **la tolerancia de 0.01 no es asunción** y su gate no es condicionado. Su §3.2 declara que **la batería del validador tiene diez casos** y que el intake escribe «nueve» en dos lugares por residuo anterior a la incorporación del décimo, sin bajar la batería para que coincida. Declara el reparto de papeles, la puerta técnica que el backlog asigna a la etapa `a` de este proyecto de código, y la cadencia por etapa sin inventar ninguna frecuencia calendaria. |
