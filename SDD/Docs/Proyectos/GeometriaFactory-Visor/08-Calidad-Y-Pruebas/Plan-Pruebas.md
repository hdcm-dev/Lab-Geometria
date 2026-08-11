# Plan de pruebas — GeometriaFactory-Visor

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Visor
**Documento:** Plan-Pruebas.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`Estrategia-Testing.md`](Estrategia-Testing.md) 1.1; [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) 1.0; [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) 1.1 §2 y §2.1; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §9; [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md); [`../07-Plan-Sprint/Mini-Plan.md`](../07-Plan-Sprint/Mini-Plan.md)
**Trazabilidad downstream:** [`Criterios-Validacion.md`](Criterios-Validacion.md), [`Definition-Of-Done.md`](Definition-Of-Done.md); `09-Devops` y `10-Examples`

---

## Tabla de contenido

- [1. Alcance del plan](#1-alcance-del-plan)
- [2. Criterios de entrada](#2-criterios-de-entrada)
- [3. Criterios de salida](#3-criterios-de-salida)
- [4. Riesgos de calidad](#4-riesgos-de-calidad)
- [5. Plan por momento del producto](#5-plan-por-momento-del-producto)
- [6. Recursos](#6-recursos)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Alcance del plan

**Qué cubre.** Los **veintiún** casos de prueba de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md), repartidos entre los **tres** momentos que [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §2 declara como épicas: la etapa `a`, el **momento de medición de `PT-02` y `PT-03`** —antes de comprometer la etapa `g`— y la etapa `g`.

**Por qué §5 se titula «plan por momento del producto» y no «plan por etapa».** Porque el momento central de este proyecto de código **no es una etapa**: es el punto en que las dos puertas técnicas se miden, que el roadmap §2.2 ubica **antes de comprometer la fase `g`** y que su §5.2 incluye entre los criterios de la transición `f` → `g`. `06` §2.1 declara que EP-02 no crea una etapa ni altera el orden de las ocho comprometidas, y este plan hereda esa forma sin inventar una etapa nueva.

**Qué no cubre, y dónde se cubre.** Toda decisión sobre el trabajo del alumno —si es válido, qué produce advertencia, quién ve qué— en el backend; la presentación del árbol, los controles de movimiento y la accesibilidad de la superficie visible, en `GeometriaFactory-Web`; el desenlace del envío, en `GeometriaFactory-Domain` y `GeometriaFactory-Infrastructure`.

**Las etapas `b` a `f` y la `h` no producen filas de trabajo en este plan**, y es declaración y no olvido: ninguna dibuja nada, y la fachada dibuja el mismo trabajo para el alumno y para el administrador sin saber cuál de los dos lo mira.

**Sin fechas y sin duraciones.** El intake declara «sin plazo calendario; el avance se mide por etapas cerradas». El único umbral contado de este plan son los **diez recorridos** de ida y vuelta, que se cuentan en recorridos y no en segundos.

## 2. Criterios de entrada

- [ ] `BT-01` está cerrada: la cadena de construcción es reproducible y produce un archivo **vacío pero real**.
- [ ] `BT-02` está cerrada: existe el guion propio del bundle, para no encadenar la construcción del resto del producto en cada iteración.
- [ ] `BT-09` está cerrada antes del momento de medición: la versión del motor de dibujo está anclada y registrada, y si es posterior a la del visualizador previo, el cambio de interfaz que exija está documentado.
- [ ] Las historias del momento cumplen los **siete** criterios de [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/Definition-Of-Ready.md) §1, incluidos el quinto —qué garantías ejerce— y el sexto —todo código usado es uno de los siete—.
- [ ] Existe un navegador con **capacidad gráfica tridimensional** en el entorno donde se ejecutan las pruebas de extremo a extremo, y un conductor capaz de contar peticiones de red y de leer el almacenamiento del navegador.
- [ ] El conductor puede **prender los dos movimientos** aunque el entorno declare preferencia de movimiento reducido. Sin esto, las mediciones de ausencia no se pueden hacer en su peor caso.

## 3. Criterios de salida

- [ ] Todos los `TC-XX` en alcance del momento están escritos, ejecutados y en verde.
- [ ] Cada `TC-XX` de una propiedad de **ausencia** se ejecutó **con su condición de medición declarada**, y la condición quedó registrada junto al resultado. Un umbral cero medido sin su condición **no cuenta**.
- [ ] **Ningún `TC-XX` que estaba en verde pasó a rojo** sin justificación escrita.
- [ ] Los gates `QG-01`, `QG-04` a `QG-09` de [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3 pasan.
- [ ] En el momento de medición: **`PT-02` y `PT-03` pasan enteras**. Si alguna no pasa, **la etapa `g` no se compromete**; no se arrastra como deuda.
- [ ] [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) está actualizada, con sus cinco tablas.
- [ ] [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) tiene el estado de cada fila actualizado con su fecha de verificación.
- [ ] Todo defecto cerrado generó al menos un `TC-XX` nuevo o extendió uno existente.
- [ ] Si el momento propuso una función nueva en la fachada, los seis pasos de [`../05-Arquitectura-Tecnica/Extensibilidad.md`](../05-Arquitectura-Tecnica/Extensibilidad.md) §5 se recorrieron enteros, incluida la consolidación en el intake.
- [ ] El punto de control tiene el OK explícito del Product Owner.

## 4. Riesgos de calidad

Alineados con los **seis** riesgos arquitectónicos de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §9, más dos propios de esta categoría.

| Id | Riesgo | Impacto | Probabilidad | Mitigación en este plan |
| --- | --- | --- | --- | --- |
| RQ-01 | Que aparezca una petición de red en el bundle, **por comodidad o por una dependencia que la haga por dentro** | **Muy alto**: rompe `RA-01` a través de `RA-02` | Baja para la primera causa, **media para la segunda** | `TC-18` inspecciona **el bundle generado y no sólo la fuente**; `TC-16` cuenta peticiones con los movimientos prendidos |
| RQ-02 | Que el anfitrión termine dependiendo de nombres internos del motor y el motor deje de ser reemplazable | Alto: se pierde el punto de extensión declarado del producto | Media | `TC-18` verifica que la superficie son **6** funciones y nada más; [`Guia-Testing-Extensibilidad.md`](Guia-Testing-Extensibilidad.md) declara los ocho compromisos de un reemplazo |
| RQ-03 | Que un bucle de dibujo sobreviva a la destrucción y se acumule al recorrer trabajos | Alto: degradación progresiva, que es lo que `PT-02` mide | Media | `TC-04`, con los diez recorridos medidos **con los movimientos prendidos**, que es su peor caso |
| RQ-04 | Que la versión del motor exija una interfaz distinta de la del visualizador previo | Medio: retrabajo acotado a la capa 3 | **Alta**: el intake ya lo anticipa | `BT-09` como criterio de entrada de §2, cerrada **antes** del momento de medición |
| RQ-05 | Que una pieza deje de dibujarse sin quedar enumerada | Alto: es el defecto original que `NB-06` viene a cerrar | Baja | `TC-07`, con los escenarios `E-5`, `E-8` y `E-6`, incluida la comprobación negativa de que la pieza de dimensión cero **sí** se dibuja |
| RQ-06 | Que se acuñe un código de condición fuera de la categoría 02 | Medio: el conjunto deja de ser cerrado y 03 y 08 se desincronizan | Media, y el catálogo de 03 ya creció de doce a **trece** entradas **sin** que creciera el conjunto de códigos | `TC-21`, que compara en las dos direcciones y verifica la distinción entre código y curso |
| RQ-07 | **Que una medición de ausencia se haga sin su condición** y quede en verde midiendo el caso fácil | **Muy alto**: el gate más importante del proyecto de código pasaría sin haber ejercitado nunca el bucle | **Alta**, porque los entornos de prueba automatizados suelen declarar preferencia de movimiento reducido | Criterio de entrada de §2 —el conductor puede prender los movimientos— y criterio de salida de §3 —la condición queda registrada junto al resultado— |
| RQ-08 | **Que se invente un umbral numérico de fluidez** para poder cerrar un criterio | Medio: un número inventado acá se propagaría como si fuera del producto | Media | `05` §8 se niega a inventarlo y esta categoría también; `BT-18` deja las dos salidas admitidas, y ninguna es inventar un número |

## 5. Plan por momento del producto

Sin fechas y sin duraciones, por lo declarado en §1.

| Momento | Épica | Alcance de testing | Casos de prueba en alcance | Entregable de esta categoría |
| --- | --- | --- | --- | --- |
| Etapa `a` | EP-01 Esqueleto ambulante | La cadena de construcción y el artefacto vacío pero real. Ninguna capacidad funcional | Ninguno de los veintiuno: no hay fachada todavía. Se pone en pie el ejecutor y se mide `QG-01` | Batería que corre; `BT-01`, `BT-02` y `BT-03` cerradas |
| **Antes de comprometer la etapa `g`** | EP-02 Medición de las puertas técnicas | Todo lo que `PT-02` y `PT-03` exigen que ya funcione: crear instancia, dibujar `E-1` con el ortoedro, sincronizar por índice y liberar recursos | `TC-01`, `TC-02`, `TC-04`, `TC-05`, `TC-06`, `TC-07`, `TC-08`, `TC-09`, `TC-10`, `TC-11`, `TC-12`, `TC-16`, `TC-17`, `TC-18`, `TC-19`, `TC-20`, `TC-21` | **Las dos puertas medidas.** Si alguna no pasa, la etapa `g` no se compromete |
| Etapa `g` | EP-03 Visualización del trabajo | Lo que la etapa integra: el movimiento automático de `F-25`, el árbol y la página integradora sin backend | `TC-03`, `TC-13`, `TC-14`, `TC-15`, y reejecución de `TC-16`, `TC-17` y `TC-09` con los movimientos gobernados en vivo | Sample **S-1** en pie; las **seis** propiedades transversales verificadas juntas; `BT-18` cerrada o elevada |

**La suma cubre los veintiún casos de prueba.** `TC-16`, `TC-17` y `TC-09` aparecen dos veces porque la etapa `g` incorpora el gobierno en vivo de los movimientos, y las tres propiedades tienen que seguir sosteniéndose con esa capacidad presente.

**El grueso del trabajo de este proyecto de código cae antes de que la etapa `g` se abra**, y este plan lo refleja en lugar de esconderlo: diecisiete de los veintiún casos de prueba se ejecutan en el momento de medición, porque una puerta que no pasa detiene la planificación de la etapa que depende de ella.

## 6. Recursos

| Recurso | Detalle |
| --- | --- |
| Personas | **Una**, `equipo_n = 1` |
| Ambiente de construcción | El contenedor de desarrollo, con el entorno de ejecución de la cadena de herramientas |
| Ambiente de ejecución | Un navegador con **capacidad gráfica tridimensional**, más un conductor capaz de contar peticiones y de leer el almacenamiento. **No hay backend**, y su ausencia es una propiedad exigida y no una carencia |
| Datos | Los textos de los **ocho** escenarios del intake §20, transcriptos sin modificación, y el elemento de dibujo de tamaño cero de [`Estrategia-Testing.md`](Estrategia-Testing.md) §5 |
| Herramientas | Las de [`Estrategia-Testing.md`](Estrategia-Testing.md) §3, nombradas por función |
| Página de prueba | El sample **S-1**, que es a la vez ejemplo y material de prueba, y cuyo desarrollo pertenece a `10-Examples` |

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara el alcance sobre los **tres** momentos del producto que este proyecto de código toca, con el fundamento de por qué el central no es una etapa sino el momento de medición de `PT-02` y `PT-03`, y con las etapas que no producen trabajo declaradas. Declara **seis** criterios de entrada —incluido que el conductor pueda prender los movimientos aunque el entorno declare preferencia de movimiento reducido— y **diez** de salida —incluido que toda medición de ausencia quede registrada con su condición—. Declara **ocho** riesgos de calidad, alineados con los seis de `05` §9 más dos propios: la medición de ausencia sin su condición y la invención de un umbral de fluidez. El plan por momento reparte los veintiún casos de prueba **sin fechas ni duraciones** y hace visible que diecisiete de ellos caen antes de que la etapa `g` se abra. |
