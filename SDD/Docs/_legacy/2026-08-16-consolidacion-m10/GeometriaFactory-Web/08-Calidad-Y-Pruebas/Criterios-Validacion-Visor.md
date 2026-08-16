# Criterios de validación — GeometriaFactory-Visor

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Visor
**Documento:** Criterios-Validacion.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) 1.1; [`Estrategia-Calidad.md`](Estrategia-Calidad.md) 1.1 §3; [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../02-Especificacion-Funcional/_fusion/Visor/Especificacion-Funcional.md) 1.2 §6; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/_fusion/Visor/Arquitectura-Proyecto-Codigo.md) 1.0 §8 y §11; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.19** §15, §17.2.P.6 · GeometriaFactory-Visor, §17.2.P.8 · GeometriaFactory-Visor y §17.2.P.10 · GeometriaFactory-Visor
**Trazabilidad downstream:** [`Definition-Of-Done.md`](Definition-Of-Done.md); `09-Devops`

---

## Tabla de contenido

- [1. Propósito](#1-propósito)
- [2. Criterios funcionales](#2-criterios-funcionales)
- [3. Criterios no funcionales](#3-criterios-no-funcionales)
- [4. Criterios de las puertas técnicas](#4-criterios-de-las-puertas-técnicas)
- [5. Criterios de regresión](#5-criterios-de-regresión)
- [6. Criterios de calidad de código y de artefacto](#6-criterios-de-calidad-de-código-y-de-artefacto)
- [7. Excepciones documentadas](#7-excepciones-documentadas)
- [8. Control de cambios](#8-control-de-cambios)

---

## 1. Propósito

Define qué significa que `GeometriaFactory-Visor` está **validado**. Su artefacto es **un archivo de guion generado** que se copia al directorio de recursos estáticos de `GeometriaFactory-Web` y viaja dentro del despliegue de esa unidad, de modo que «validado» no quiere decir «publicado»: quiere decir **que las siete garantías se sostienen sobre el bundle generado y que las dos puertas técnicas pasan**.

Este documento tiene una sección que los otros dos proyectos de código de nivel topológico 0 no tienen: **§4, las puertas técnicas**. Es la consecuencia de que el intake §15 declare `PT-02` y `PT-03` sobre este proyecto de código, y de que el roadmap las ubique antes de comprometer la etapa `g`.

## 2. Criterios funcionales

| Id | Criterio | Cómo se comprueba | Umbral |
| --- | --- | --- | --- |
| CV-01 | Los **siete** casos de uso tienen al menos un caso de prueba en verde, y cada criterio Given-When-Then declarado en sus historias está cubierto | [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2 | **7 de 7** |
| CV-02 | Las **seis** funciones de la fachada están ejercitadas | Matriz §2 y `TC-12015` | **6 de 6** |
| CV-03 | Las **siete** garantías del contrato de fachada tienen caso de prueba en verde | Matriz §5 | **7 de 7** |
| CV-04 | Los **siete** códigos de condición están cubiertos en sus **ocho** cursos, y **ninguno se acuña aguas abajo** | Matriz §6 y `TC-12021` | **7 de 7** códigos, **8 de 8** cursos, **0** acuñados |
| CV-05 | Las **catorce** historias tienen su caso de prueba | Matriz §2 cruzada con [`../06-Backlog-Tecnico/Product-Backlog.md`](../../../06-Backlog-Tecnico/_fusion/Visor/Product-Backlog.md) §3.1 | **14 de 14** |
| CV-06 | Los **seis** tipos dibujables se dibujan, tres volumétricos y tres planos | `TC-12005`, con el texto del escenario `E-7` | **6 de 6** |
| CV-07 | Las variantes de clave del emisor se leen como sinónimos, y **el cero es una dimensión legible** | `TC-12006` y `TC-12007`, con los escenarios `E-2`, `E-7` y `E-6` | Las dos claves aceptadas; la figura de `E-6` **entre las dibujadas** |
| CV-08 | Los **ocho** escenarios del intake §20 están ejercitados **como texto**, sin sustituirlos por datos sintéticos | Verificación uno por uno de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) §3 | **8 de 8** |
| CV-09 | **Ninguna regla de negocio se verifica en este proyecto de código** | Matriz §4 | **0 de 16**, y es el resultado correcto |

## 3. Criterios no funcionales

Uno por cada NFR de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/_fusion/Visor/Arquitectura-Proyecto-Codigo.md) §8. **Los seis primeros son las seis propiedades transversales de `02` §6 y llevan su condición de medición, que es vinculante.**

| Id | Criterio | Umbral | Condición de medición | Test |
| --- | --- | --- | --- | --- |
| CV-10 | Cero red | Exactamente **0** peticiones originadas por el archivo de guion | **Con los dos movimientos prendidos y sostenidos**, y durante los gestos de rotar y acercar | `TC-12016`, `TC-12018` |
| CV-11 | Cero persistencia | **0** claves escritas y ningún estado conservado entre páginas | Cualquier estado de los movimientos; recargar no repone la preferencia; la exclusión de claves ajenas se hace **por espacio de nombres declarado y no por prefijo** | `TC-12017` |
| CV-12 | Se ejercita sin backend | Recorrido de las **seis** funciones con **0** servicios del backend disponibles | Sin condición adicional | `TC-12015` |
| CV-13 | Disposición determinista | Dos procesados del mismo texto producen la **misma disposición** | **Se compara posición, no orientación**; vale con cualquier estado de los movimientos | `TC-12009` |
| CV-14 | Liberación de recursos | **10 recorridos** de ida y vuelta sin degradación | **Con los dos movimientos prendidos** | `TC-12004` |
| CV-15 | Ausencia de fallo silencioso | **100 %** de las piezas no dibujadas enumeradas con índice y código, y **0** sin registro | Sin condición adicional | `TC-12007` |
| CV-16 | Dependencias traídas de una red externa en tiempo de ejecución | Exactamente **0** | Página abierta sin acceso a redes externas | `TC-12019` |
| CV-17 | Superficie pública del bundle | **6** funciones, **1** nombre propio en el objeto global, **0** globales sueltas | Inspección del **bundle generado** | `TC-12018` |

**Una medición hecha sin su condición no cuenta como medición.** Es el criterio más importante de esta sección, y su fundamento está en `02` §6: sin condiciones declaradas, la prueba mediría el caso fácil y quedaría en verde sin haber ejercitado nunca un bucle de dibujo corriendo.

**No hay criterio de fluidez con umbral numérico**, y **esta categoría no lo inventa**. `05` §8 declara que la fuente no fija un valor y lo deja abierto como `PA-03`. Hasta que exista, la fluidez se verifica **de forma cualitativa declarada** junto con `PT-02`, y esa verificación cualitativa **no se reporta como si fuera un número**.

## 4. Criterios de las puertas técnicas

Las dos puertas las declara el intake §15 y §17.2.P.8 · GeometriaFactory-Visor. **Esta sección no las redefine, no las relaja y no les agrega criterios**: declara con qué caso de prueba se mide cada tramo.

| Id | Puerta y tramo | Umbral | Test |
| --- | --- | --- | --- |
| CV-18 | **`PT-03`** · El motor de dibujo tridimensional queda **dentro** del bundle | El motor dentro; **0** dependencias de red externa en tiempo de ejecución | `TC-12019` |
| CV-19 | **`PT-03`** · La página funciona **sin acceso a redes de distribución externas** | La fachada se ejerce entera sin ese acceso | `TC-12019` |
| CV-20 | **`PT-02`** · El bundle carga en una página del anfitrión y la creación de instancia **arma la escena** | Carga y escena viva | `TC-12020` |
| CV-21 | **`PT-02`** · La carga del texto dibuja las **tres** figuras de `E-1`, **ortoedro incluido** | 3 de 3 | `TC-12020`, `TC-12005` |
| CV-22 | **`PT-02`** · **Diez** recorridos de ida y vuelta **no degradan** | 10 sin degradación, **con los dos movimientos prendidos** | `TC-12020`, `TC-12004` |
| CV-23 | **`PT-02`** · El árbol y la escena **se sincronizan por índice** | Sincronización verificada en los dos sentidos | `TC-12020`, `TC-12011` |

**Las dos puertas son vinculantes y no admiten carácter condicionado.** Una que no pasa **detiene la planificación de la etapa `g`** y no se arrastra como deuda. Es el mismo fundamento con el que el Product Owner promovió `F-13` a `Must Have` en el intake **1.19**: una capacidad citada por una puerta técnica deja de ser diferible, y `CV-23` es exactamente esa capacidad.

## 5. Criterios de regresión

| Id | Criterio | Umbral |
| --- | --- | --- |
| CV-24 | La batería completa se ejecuta entera al cerrar cada momento del producto, y no sólo los casos que el momento tocó | 100 % de los `TC-XX` escritos hasta ese punto |
| CV-25 | **Ningún caso de prueba que estaba en verde pasa a rojo** sin justificación escrita | 0 regresiones sin justificar |
| CV-26 | Todo defecto cerrado generó al menos un `TC-XX` nuevo o extendió uno existente | 1 por defecto cerrado, como mínimo |
| CV-27 | Las **seis** propiedades transversales se reverifican **después** de incorporar el gobierno en vivo de los movimientos, y no sólo antes | 6 de 6 reverificadas en la etapa `g` |
| CV-28 | `TC-12005` —las tres figuras de `E-1` con el ortoedro dibujado— se ejecuta en **todos** los momentos a partir de la medición de puertas | Presente en cada ejecución. Es la regresión del defecto original: hoy, en el visualizador previo, **ningún ortoedro generado por la aplicación se dibuja** |

## 6. Criterios de calidad de código y de artefacto

| Id | Criterio | Umbral | Carácter |
| --- | --- | --- | --- |
| CV-29 | La regla de dependencias entre capas se respeta: la capa 1 no conoce el interior, la capa 2 **no contiene lógica de dibujo** y la capa 3 no conoce al anfitrión | 0 violaciones | **Bloqueante** |
| CV-30 | El bundle **nunca se editó a mano**: es un artefacto generado y reproducible | 100 % generado | **Bloqueante** |
| CV-31 | El motor de dibujo **nunca se expone al anfitrión** | 0 exposiciones | **Bloqueante** |
| CV-32 | **Cobertura de líneas: no aplica como criterio.** El intake §17.2.P.6 · GeometriaFactory-Visor fija el gate de inspección de cero red **en lugar de** la cobertura de líneas | — | **No aplicable, declarado** |
| CV-33 | **Mutation score: no aplica.** No hay forma de matar los mutantes del código de dibujo sin recurrir a la comparación de imágenes, que [`Estrategia-Testing.md`](../../Estrategia-Testing.md) §1 descarta con su fundamento | — | **No aplicable, declarado** |
| CV-34 | **Snapshot de la escena: no aplica.** Una comparación de imágenes sería frágil y **no distinguiría un cambio legítimo de orientación de una deriva de posición**, cuando el determinismo comprometido es de posición | — | **No aplicable, declarado** |

**Las tres «no aplicable» se declaran en lugar de omitirse.** Un lector que no encuentre cobertura de líneas ni mutation score en un proyecto de código de tipo `library` tiene que poder leer por qué.

## 7. Excepciones documentadas

| Situación | Salida admitida | Quién la aprueba |
| --- | --- | --- |
| **Umbral de fluidez inexistente** | La verificación es **cualitativa declarada** junto con `PT-02`, y se registra como tal. **No habilita a inventar un número**, y así lo declara la excepción correspondiente de la DoR §3 | El Product Owner, o esta categoría al fijar su guion de medición (`BT-12018`) |
| Criterio **bloqueante** no cumplido | Se abre una tarea técnica con la remediación y **el momento no cierra** hasta que se cumpla | El Product Owner, con constancia escrita |
| **`PT-02` o `PT-03` que no pasan** | **Ninguna excepción.** La etapa `g` no se compromete. No se arrastra como deuda, no se difiere y no se convierte en condicionada | — |
| Medición de ausencia hecha **sin su condición** | **No se admite.** No cuenta como medición: mediría el caso fácil | — |
| Historia que introduce comportamiento en la capa 3 que rompe una garantía | **No se admite**, y es la misma prohibición que la DoR §3 declara del lado de la entrada: perder una garantía es cambio mayor aunque las seis firmas no se toquen | — |

**Lo que no es una excepción admitida:** acuñar un código de condición fuera de la categoría 02, editar el bundle a mano, medir cero red con los movimientos apagados, o reportar la fluidez con un número que ninguna fuente da.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara **treinta y cuatro** criterios de validación, `CV-01` a `CV-34`, repartidos en funcionales, no funcionales, **de puertas técnicas**, de regresión y de calidad de código y de artefacto. La sección de puertas técnicas es propia de este proyecto de código y declara los **seis** tramos de `PT-02` y `PT-03` con el caso de prueba que mide cada uno, sin redefinirlas ni relajarlas, y con la constancia de que **no admiten carácter condicionado**. Los seis criterios no funcionales transcriben las condiciones de medición de `02` §6 y declaran que una medición sin su condición **no cuenta**. Declara tres criterios **no aplicables** con su motivo —cobertura de líneas, mutation score y snapshot— en lugar de omitirlos, y **cinco** salidas ante un criterio no cumplido, tres de ellas sin excepción posible. |
