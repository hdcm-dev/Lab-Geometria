# Estrategia de calidad — GeometriaFactory-Visor

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Visor
**Documento:** Estrategia-Calidad.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) 1.2 §3 y §6 (las **seis** propiedades transversales con sus condiciones de medición); [`../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md) 1.1 §3.2 (las **siete** garantías) y §6 (los **siete** códigos); [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §5, §8, §9 y §11; [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md) §3; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.19** §14 (`RA-01`, `RA-02`), §15 (puertas `PT-02` y `PT-03`), §17.7.P.6, §17.7.P.8, §17.7.P.10 y §22
**Trazabilidad downstream:** [`Estrategia-Testing.md`](Estrategia-Testing.md), [`Plan-Pruebas.md`](Plan-Pruebas.md), [`Criterios-Validacion.md`](Criterios-Validacion.md), [`Definition-Of-Done.md`](Definition-Of-Done.md), [`Guia-Testing-Extensibilidad.md`](Guia-Testing-Extensibilidad.md), [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md); `09-Devops`, `10-Examples` y `11-Documentacion`

---

## Tabla de contenido

- [1. Definición de calidad para este proyecto de código](#1-definición-de-calidad-para-este-proyecto-de-código)
- [2. Atributos de calidad priorizados](#2-atributos-de-calidad-priorizados)
- [3. Quality gates](#3-quality-gates)
  - [3.1 Las dos puertas técnicas son vinculantes](#31-las-dos-puertas-técnicas-son-vinculantes)
- [4. Roles de calidad dentro del equipo](#4-roles-de-calidad-dentro-del-equipo)
- [5. Cadencia de revisión](#5-cadencia-de-revisión)
- [6. Control de cambios](#6-control-de-cambios)

---

## 1. Definición de calidad para este proyecto de código

`GeometriaFactory-Visor` tiene calidad cuando **las siete garantías de su contrato de fachada se sostienen sobre las seis funciones**, cuando **ninguna pieza deja de dibujarse sin quedar enumerada con su índice y su código**, y cuando el archivo de guion **no origina ni una sola petición de red** mientras los dos movimientos automáticos corren.

Las tres partes de esa definición tienen peso distinto y conviene decir por qué. La segunda es el problema original que el producto viene a cerrar: hoy, en la visualización previa, una figura simplemente no aparece y nadie se entera. La tercera es negativa por diseño —no hacer red— y es lo que hace **imposible** violar `RA-01` desde el navegador: la contribución de este proyecto de código a la seguridad del producto es una ausencia, y las ausencias se verifican con umbral cero y con la condición en que se miden.

**Este proyecto de código es el único del producto con `tiene_extensibilidad` true**, y su fachada es el punto de extensión declarado del producto (intake §18). Eso agrega una exigencia propia: la calidad incluye que **un reemplazo de la capa 3 se pueda evaluar sin backend**, con los ocho compromisos de [`../05-Arquitectura-Tecnica/Extensibilidad.md`](../05-Arquitectura-Tecnica/Extensibilidad.md) §4. Su verificación vive en [`Guia-Testing-Extensibilidad.md`](Guia-Testing-Extensibilidad.md).

## 2. Atributos de calidad priorizados

Clasificación ISO/IEC 25010. Las **seis propiedades transversales** de `02` §6 son la fuente de los umbrales, y esta tabla **las toma como están y no las redefine**.

| Atributo ISO 25010 | Prioridad | Métrica y origen |
| --- | --- | --- |
| Seguridad | **Crítica**, y negativa por diseño | **0 peticiones** originadas por el archivo de guion, medidas **con los dos movimientos prendidos**, que es su peor caso (`02` §6; intake §17.7.P.10). Es el NFR más importante del proyecto de código según el propio intake |
| Fiabilidad | **Crítica** | **100 %** de las piezas no dibujadas enumeradas con su índice y su código, y **0** piezas que desaparezcan sin registro (garantía `G-5`) |
| Adecuación funcional | **Crítica** | Los **seis** tipos dibujables, los **siete** códigos de condición y las **siete** garantías, sostenidos por las **seis** funciones |
| Eficiencia de desempeño | **Alta** | **10** recorridos de ida y vuelta entre trabajos sin degradación, medidos **con los dos movimientos prendidos** (`PT-02`) |
| Mantenibilidad | **Alta** | Superficie de exactamente **6** funciones, bajo **1** nombre propio en el objeto global y **0** identificadores globales sueltos (`05` §8) |
| Compatibilidad | **Alta** | **0** dependencias traídas de una red de distribución externa en tiempo de ejecución (`PT-03`). Navegadores con capacidad gráfica tridimensional; sin ella el visor **no es soportado** y la fachada informa `CAPACIDAD_GRAFICA_AUSENTE` |
| Usabilidad | **Media, y ajena en su mayor parte** | La superficie visible la dibuja el componente anfitrión, que vive en `GeometriaFactory-Web`. Lo que este proyecto de código aporta es el equivalente accesible: la estructura del texto y la enumeración de piezas no dibujadas |
| Portabilidad | **Media** | Requisito declarado **por capacidad** y no por versión de navegador, porque la fuente no la fija (`05` §5 y §11 `PA-04`) |

**Sobre el atributo de eficiencia y su umbral ausente.** El intake §17.7.P.10 declara «interacción fluida al rotar y acercar, sin tráfico de circuito durante el gesto» y **no fija un valor numérico**. `05` §8 se niega explícitamente a inventarlo y lo deja como punto abierto `PA-03`. **Esta categoría tampoco lo inventa**: la fluidez se verifica de forma cualitativa declarada junto con `PT-02`, y el umbral numérico queda abierto. Ver §3.

## 3. Quality gates

| Id | Condición | Cómo se verifica | Consecuencia si no se cumple |
| --- | --- | --- | --- |
| QG-01 | El bundle **se genera sin errores** | Etapa de empaquetado del pipeline (intake §17.7.P.8) | Bloquea la fusión |
| QG-02 | **`PT-03`**: el motor de dibujo tridimensional queda **dentro** del bundle y la página funciona **sin acceso a redes de distribución externas**; **0** dependencias traídas de una red externa en tiempo de ejecución | `TC-19`, sobre el bundle generado | **Bloqueante, y detiene la planificación de la etapa `g`**. Ver §3.1 |
| QG-03 | **`PT-02`**: el bundle carga en una página del anfitrión, la creación de instancia arma la escena, la carga del texto dibuja las **tres** figuras de `E-1` **incluido el ortoedro**, **diez** recorridos de ida y vuelta no degradan, y el árbol y la escena **se sincronizan por índice** | `TC-20`, con los recorridos medidos **con los dos movimientos prendidos** | **Bloqueante, y detiene la planificación de la etapa `g`**. Ver §3.1 |
| QG-04 | **Cero red**: exactamente **0** peticiones originadas por el archivo de guion, y **0** ocurrencias de las tres formas de petición en el código fuente **y en el bundle generado** | `TC-16` y `TC-18`, con la medición **con los dos movimientos prendidos y sostenidos** | Bloqueante, sin gradación. Es `RA-02`, y a través de ella `RA-01` |
| QG-05 | **Cero persistencia**: **0** claves escritas en el almacenamiento del navegador y ningún estado conservado entre páginas | `TC-17` | Bloqueante, sin gradación |
| QG-06 | Superficie del bundle: exactamente **6** funciones expuestas, bajo **1** nombre propio en el objeto global y **0** identificadores globales sueltos | `TC-18` | Bloqueante |
| QG-07 | **Ausencia de fallo silencioso**: **100 %** de las piezas no dibujadas enumeradas con su índice y su código, y **0** sin registro | `TC-06` | Bloqueante, sin gradación. Es la garantía `G-5` |
| QG-08 | Los códigos de condición son exactamente **siete** y **ninguno se acuña aguas abajo**; un curso nuevo se agrega como fila de curso y no como código | `TC-21`, contra §6 del contrato de fachada | Se rechaza en revisión |
| QG-09 | El bundle **nunca se edita a mano**: es un artefacto generado y reproducible | Revisión del pull request de la etapa (intake §17.7.P.7) | Se rechaza en revisión |

**No hay gate de cobertura de líneas**, y su ausencia está declarada aguas arriba: el intake §17.7.P.6 fija como gate «verificable por inspección, **en lugar de cobertura de líneas**», la ausencia de las tres formas de petición de red. `QG-04` es ese gate.

**No hay gate de fluidez numérica**, por lo declarado en §2.

### 3.1 Las dos puertas técnicas son vinculantes

`PT-02` y `PT-03` **no son criterios de esta categoría**: las declara el intake §15 y §17.7.P.8, y el roadmap §2.2 las ubica **antes de comprometer la etapa `g`**. Su carácter vinculante tiene una consecuencia que este documento hereda y no relaja:

**Una puerta que no pasa detiene la planificación de la etapa `g` y no se arrastra como deuda.** Es el mismo fundamento por el que el Product Owner promovió la capacidad `F-13` a `Must Have` en el intake **1.19**: una capacidad citada por una puerta técnica deja de ser diferible. Esta categoría **no puede convertir `PT-02` ni `PT-03` en gates condicionados**, ni cambiar lo que miden, ni agregarles criterios.

Lo que sí hace esta categoría es **declarar con qué caso de prueba se mide cada una** —`TC-19` y `TC-20`— y **con qué condiciones**: los diez recorridos, con los dos movimientos prendidos, porque un bucle de dibujo que sobreviviera a la destrucción es exactamente la degradación que la puerta tiene que descartar y con los movimientos apagados no se ejercitaría.

## 4. Roles de calidad dentro del equipo

`equipo_n` es **1** (intake §2).

| Papel | Quién | Qué le corresponde |
| --- | --- | --- |
| AG-08, calidad y pruebas | La única persona del equipo, en este papel | Los casos de prueba, la matriz de cobertura, la matriz de sensado de deriva, la guía de testing de extensibilidad y la DoD |
| Product Owner | El docente de la cátedra, que es también quien ejecuta | El OK del punto de control, y el umbral de fluidez si alguna vez lo fija (`PA-03`) |
| Medición mecánica | El pipeline y el navegador | `QG-01` lo da el pipeline; `QG-02` a `QG-07` se miden sobre el bundle generado y sobre una página, no sobre el código fuente solamente |

**En este proyecto de código el filtro más duro no es la revisión: son las dos puertas medidas sobre el artefacto generado.** Lo declara [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/Definition-Of-Ready.md) §4 y esta estrategia lo adopta: no dependen de que alguien las revise, se miden.

## 5. Cadencia de revisión

| Momento | Qué se revisa | Qué produce |
| --- | --- | --- |
| Al abrir la rama de la etapa `a` | Que la cadena de construcción sea reproducible y produzca un archivo **vacío pero real** | `BT-01` cerrada |
| **Antes de comprometer la etapa `g`** | `PT-02` y `PT-03` enteras | Las dos puertas medidas, o la etapa `g` sin comprometer |
| Al cerrar la etapa `g` | Las **seis** propiedades transversales con sus condiciones de medición, las **siete** garantías y los **siete** códigos | Matriz de cobertura actualizada y matriz de sensado de deriva con su estado |
| **Ante todo cambio del bundle** | `QG-04`, `QG-05` y `QG-06`, sobre el **bundle generado** y no sólo sobre la fuente | La constancia de la medición en el pull request |
| Ante toda propuesta de función nueva en la fachada | Los seis pasos de [`../05-Arquitectura-Tecnica/Extensibilidad.md`](../05-Arquitectura-Tecnica/Extensibilidad.md) §5 | La especificación en 02, o el rechazo con su motivo |

**La revisión sobre el bundle generado y no sólo sobre la fuente es propia de este proyecto de código.** Una dependencia que hace una petición por dentro no aparece en el código fuente y sí en el bundle; `05` §9 declara esa causa como de probabilidad **media**, más alta que la de escribir la petición a mano.

**No se declara ninguna frecuencia calendaria**: el intake declara «sin plazo calendario; el avance se mide por etapas cerradas».

## 6. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara la definición de calidad en sus tres partes —garantías sostenidas, ausencia de fallo silencioso y cero red—, los ocho atributos ISO 25010 con su métrica tomada de las **seis** propiedades transversales de `02` §6 sin redefinirlas, y los **nueve** quality gates. Declara que `PT-02` y `PT-03` son **vinculantes y no convertibles en condicionadas**, con el fundamento de que una puerta que no pasa detiene la planificación de la etapa `g`, y que esta categoría sólo declara con qué caso de prueba y con qué condiciones se miden. Declara la ausencia de gate de cobertura de líneas —sustituido aguas arriba por el gate de inspección de cero red— y la ausencia de umbral numérico de fluidez, que esta categoría **no inventa**. Suma a la cadencia la revisión **sobre el bundle generado** ante todo cambio. |
