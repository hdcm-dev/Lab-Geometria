# Guía de testing de extensibilidad — GeometriaFactory-Visor

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Visor
**Documento:** Guia-Testing-Extensibilidad.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../05-Arquitectura-Tecnica/Extensibilidad.md`](../05-Arquitectura-Tecnica/Extensibilidad.md) 1.0 §2 a §7; [`../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md) 1.1 §3.2, §4, §6 y §7; [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) 1.0; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.19** §16.1, §17.7.P.8 y §18
**Trazabilidad downstream:** `10-Examples`, que desarrolla el sample **S-1**; `11-Documentacion`, que cita esta guía sin redefinirla

---

## Tabla de contenido

- [1. Por qué esta guía existe y qué clase de extensibilidad cubre](#1-por-qué-esta-guía-existe-y-qué-clase-de-extensibilidad-cubre)
- [2. Qué se prueba de un reemplazo y qué no](#2-qué-se-prueba-de-un-reemplazo-y-qué-no)
- [3. La batería de aceptación de un reemplazo](#3-la-batería-de-aceptación-de-un-reemplazo)
- [4. Cómo se prueba una función nueva de la fachada](#4-cómo-se-prueba-una-función-nueva-de-la-fachada)
- [5. Errores de prueba que rompen el punto de extensión](#5-errores-de-prueba-que-rompen-el-punto-de-extensión)
- [6. Control de cambios](#6-control-de-cambios)

---

## 1. Por qué esta guía existe y qué clase de extensibilidad cubre

`tiene_extensibilidad` es **true** en este proyecto de código y **sólo en éste** de los siete del producto (`PRODUCT-MANIFEST` §5). `Rules-Calidad-Y-Pruebas.md` §2.1 exige esta guía para `library` con puntos de extensión.

**La clase de extensibilidad no es la habitual, y eso cambia qué hay que probar.** [`../05-Arquitectura-Tecnica/Extensibilidad.md`](../05-Arquitectura-Tecnica/Extensibilidad.md) §1 lo declara: no hay complementos que se registren, ni ganchos que un tercero implemente, ni mecanismo de descubrimiento. Lo que hay es **un contrato angosto y estable de seis funciones que hace reemplazable la pieza que está detrás**. La extensión no se agrega desde afuera: se sustituye desde adentro sin que nadie de afuera se entere.

De ahí se sigue qué prueba esta guía y qué no:

| Lo que esta guía prueba | Lo que no prueba, y por qué |
| --- | --- |
| Que **un reemplazo de la capa 3** —otro motor de dibujo, u otra implementación del servicio— siga cumpliendo el contrato | El registro de complementos, los ganchos y el descubrimiento: **no existen**, y `05` `Extensibilidad.md` §7 los declara inexistentes para que ninguna categoría los busque ni los invente |
| Que el motor de dibujo **nunca se exponga al anfitrión** | El interior del motor de dibujo. Probarlo lo volvería irreemplazable, que es lo contrario del punto de extensión |
| Que la superficie siga siendo de **seis** funciones y **siete** códigos | Una configuración externa que altere el comportamiento: violaría la garantía `G-3` |
| Que el sample **S-1** ejerza el contrato entero **sin backend** | Un catálogo abierto de tipos de pieza: los tipos dibujables son **seis** y son conjunto cerrado |

## 2. Qué se prueba de un reemplazo y qué no

La tabla de `05` `Extensibilidad.md` §3 declara qué es reemplazable. Esta guía la traduce a alcance de prueba.

| Elemento | ¿Reemplazable? | Qué verifica la batería |
| --- | --- | --- |
| El **motor de dibujo tridimensional** | Sí, y es el propósito del punto de extensión | Que las **siete** garantías y los **siete** códigos se sostengan igual, y que el motor siga confinado a la capa 3 |
| El **servicio de dibujo** entero, capa 3 | Sí | Lo mismo, más la disposición derivada del índice |
| La **forma interna del identificador** de instancia | Sí, y **no es cambio de contrato** | Que las cinco funciones que lo exigen sigan resolviéndolo, y que un identificador liberado siga produciendo `INSTANCIA_DESCONOCIDA`. **Que el anfitrión dependa de su forma es un defecto del anfitrión**, y esta batería no lo cubre |
| La **disposición** de las piezas | No libremente | Que siga derivándose del índice y que dos procesados produzcan la misma **posición** |
| Las **seis** funciones y sus nombres | No | Que sigan siendo seis, con los nombres que el intake §17.7.P.3 fija |
| Las **siete** garantías | No | Que ninguna se pierda: perder una es cambio mayor aunque las firmas no se toquen |
| Los **siete** códigos de condición | No aguas abajo | Que sigan siendo siete y que ninguno se acuñe fuera de la categoría 02 |

## 3. La batería de aceptación de un reemplazo

`05` `Extensibilidad.md` §4 declara **ocho compromisos** que un reemplazo tiene que sostener, cada uno con su forma de verificación. Esta sección los traduce a los casos de prueba de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md), **sin agregar compromisos ni cambiar los declarados**.

| # | Compromiso de `05` §4 | Casos de prueba que lo verifican | Material |
| --- | --- | --- | --- |
| 1 | Las **seis** funciones, con sus nombres y con lo que cada una recibe y devuelve | `TC-15`, `TC-18` | Sample **S-1** |
| 2 | Las **siete** garantías, verificadas con las **seis** propiedades transversales **y sus condiciones de medición** | `TC-16`, `TC-17`, `TC-15`, `TC-09`, `TC-04`, `TC-07`, más la tabla de garantías de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §5 | `E-1`, `E-7` |
| 3 | Los **siete** códigos, ni uno más ni uno menos, con los **dos cursos** de `ELEMENTO_DE_DIBUJO_INVALIDO` | `TC-21`, `TC-02`, `TC-12` | Elemento de dibujo de tamaño cero |
| 4 | Los **seis** tipos de pieza dibujables, **con el cero como dimensión legible** | `TC-05`, `TC-07` | `E-7` y `E-6` |
| 5 | La disposición derivada del índice, con **posición reservada** para las figuras no reconstruidas | `TC-09`, `TC-07` | `E-1` y `E-5` |
| 6 | Los **dos** movimientos gobernables por separado, con **reposición de la orientación de partida** al apagar el giro | `TC-13`, `TC-14`, `TC-03` | `E-1` |
| 7 | Liberación completa al destruir, **incluido el corte del bucle** | `TC-04` | Dos trabajos entre los que ir y volver |
| 8 | Empaquetado **sin dependencias traídas de una red externa** en tiempo de ejecución | `TC-19`, `TC-18` | Página sin acceso a redes externas |

**Los ocho compromisos son verificables sin backend**, que es lo que hace barato evaluar un reemplazo: alcanza con el sample **S-1** y los escenarios `E-1`, `E-5`, `E-6` y `E-7`. `05` `Extensibilidad.md` §4 lo declara así y esta guía no lo relaja.

**Cómo se corre la batería sobre un reemplazo, en orden:**

1. Generar el bundle con el reemplazo, con el guion propio del bundle.
2. Correr `TC-18` y `TC-19`: si la superficie cambió o si aparecieron dependencias de red externa, **no hay nada más que probar**.
3. Correr `TC-21`: si el conjunto de códigos cambió, el reemplazo **no cumple** el compromiso 3.
4. Abrir el sample **S-1** y correr `TC-15`, con `E-1` y con `E-7`.
5. Correr las propiedades de ausencia —`TC-16` y `TC-17`— **con los dos movimientos prendidos**.
6. Correr `TC-04`, `TC-07`, `TC-09`, `TC-13` y `TC-14`.
7. Registrar el resultado de los ocho compromisos, uno por uno.

**Los dos primeros pasos son de descarte rápido y están puestos en ese orden a propósito**: un reemplazo que cambia la superficie o que trae una dependencia de red no cumple, y correr la batería entera antes de saberlo sería trabajo perdido.

## 4. Cómo se prueba una función nueva de la fachada

`05` `Extensibilidad.md` §5 declara el proceso de **seis pasos** por el que la fachada crece. Esta sección declara qué le corresponde a esta categoría en cada uno, sin reabrir ninguno.

| Paso de `05` §5 | Qué le corresponde a esta categoría |
| --- | --- |
| 1 · Comprobar que no se resuelve del lado del anfitrión | Nada: es del equipo, antes de que haya algo que probar |
| 2 · Comprobar que no cabe como flujo alternativo | Nada |
| 3 · Especificar la función en la categoría 02 | Nada: los criterios de aceptación nacen allá |
| 4 · Comprobar si acuña **garantía** o **código** nuevos | **Acá sí.** Si acuña código, la tabla de códigos de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §6 suma una fila y `TC-21` cambia su umbral. Si acuña garantía, la tabla §5 suma una fila. **Si no acuña ninguno de los dos, ninguna de las dos tablas cambia**, y eso hay que verificarlo en lugar de suponerlo |
| 5 · Consolidarla en el intake §17.7.P.3 | Nada, salvo verificar que la consolidación ocurrió antes de declarar la función terminada (`Definition-Of-Done.md` §1.3) |
| 6 · Declararla cambio menor y registrarla | **Acá sí.** Un `TC-XX` nuevo por la función, y la verificación de que **ningún anfitrión escrito contra la superficie anterior se rompe**, que es la definición de cambio menor |

**El proceso ya se recorrió entero una vez**, con la sexta función `establecerMovimiento`: la categoría 02 la acuñó con caso de uso propio `CU-07`, **no acuñó garantía ni código** —la condición que puede informar, `INSTANCIA_DESCONOCIDA`, ya existía y pasó a presentarse en **cinco** funciones—, y el intake la consolidó en su versión **1.6**. Del lado de esta categoría, el efecto es que **`TC-13` y `TC-14` existen y que `TC-21` verifica que el conjunto sigue cerrado en siete**: la superficie pasó de cinco funciones a seis **sin romper a ningún anfitrión escrito contra las cinco anteriores**.

**El catálogo de diagnóstico de 03 sí creció**, de doce a **trece** entradas, porque su unidad de catalogación es la **función** y no el código. `TC-21` protege esa distinción: crecer el catálogo no es crecer el conjunto de códigos, y confundir las dos cifras es el defecto que `05` §9 declara como sexto riesgo.

## 5. Errores de prueba que rompen el punto de extensión

Se declaran para que ninguna prueba escrita más adelante los cometa. Los cinco son formas de atar el proyecto de código a la pieza que tiene que ser reemplazable.

| Error | Por qué rompe el punto de extensión | Qué hacer en su lugar |
| --- | --- | --- |
| **Probar el interior del motor de dibujo** | Ata la batería a un motor concreto: reemplazarlo obligaría a reescribir las pruebas, y el costo de reemplazo es exactamente lo que el punto de extensión viene a bajar | Probar contra la fachada y contra el **resultado de dibujo**, que son contrato |
| **Sustituir el motor por un doble** | La prueba verificaría el doble y no la escena: se perderían el contexto gráfico, su liberación y el bucle, que son las tres cosas que `PT-02` mide | Usar una página real con capacidad gráfica ([`Estrategia-Testing.md`](Estrategia-Testing.md) §5) |
| **Depender de la forma interna del identificador de instancia** | Es opaco por decisión, y su forma puede cambiar sin que sea cambio de contrato. Una prueba que la presuponga fallaría ante un cambio legítimo | Tratarlo como valor opaco: sólo verificar que resuelve, que se invalida y que produce `INSTANCIA_DESCONOCIDA` |
| **Comparar imágenes de la escena** | No distinguiría un cambio legítimo de **orientación** de una deriva de **posición**, y el determinismo comprometido por `G-6` es de posición | Comparar dos procesados pieza por pieza, por posición |
| **Medir las propiedades de ausencia con los movimientos apagados** | Quedaría en verde sin haber ejercitado el bucle de dibujo, que es el caso donde una petición se colaría. Es el modo en que el gate más importante del proyecto de código pasa sin verificar nada | Prender los dos movimientos y sostenerlos, que es la condición de medición que `02` §6 declara |

## 6. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara qué clase de extensibilidad cubre esta guía —contrato angosto que hace reemplazable la pieza de atrás, y no registro de complementos— y qué queda fuera con su motivo. Traduce los **ocho** compromisos de `05` `Extensibilidad.md` §4 a los casos de prueba que los verifican, sin agregar compromisos ni cambiar los declarados, y fija el orden en que la batería se corre sobre un reemplazo, con dos pasos de descarte rápido al principio. Declara qué le corresponde a esta categoría en cada uno de los **seis** pasos del proceso por el que la fachada crece, con el precedente de la sexta función recorrido entero y con su efecto sobre las tablas de esta categoría. Declara **cinco** errores de prueba que romperían el punto de extensión, con lo que hay que hacer en su lugar. |
