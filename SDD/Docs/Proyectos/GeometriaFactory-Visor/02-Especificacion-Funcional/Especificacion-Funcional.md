# Especificación Funcional — GeometriaFactory-Visor

**Proyecto de código:** GeometriaFactory-Visor
**Documento:** Especificacion-Funcional.md
**Versión:** 1.2
**Estado:** Propuesto
**Fecha:** 2026-08-08
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `../../../00-Contexto/Vision-Producto.md` §3 (propuesta de valor) y §9 (glosario raíz); `../../../00-Contexto/Alcance-Producto.md` §4.1 (capacidades comprometidas) y §4.2 (capacidades de prioridad menor); `../../../00-Contexto/Compatibilidad-Plataformas.md` §2.2 (plataforma del navegador) y §4 (alternativas para plataformas no soportadas); `../../../01-Necesidades-Negocio/Necesidades-Negocio.md` §2 (catálogo de necesidades); `../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-06-Visualizacion-Dentro-Del-Producto.md` §1, §4, §5 y §7; `../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-04-Interpretacion-Fiel-Del-Dato-Del-Alumno.md` §4 (problema específico), en su parte de piezas dibujadas; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.10** §4 (capacidad F-25), §14 (RA-01, RA-02, RA-03), §16.1, §17.7 completo, §18, §20 E-1 y E-7
**Trazabilidad downstream:** 03-UX-UI-DX (variante DX) de este proyecto de código, 05-Arquitectura-Tecnica, 06-Backlog-Tecnico, 08-Calidad-Y-Pruebas, 10-Examples (sample S-1)

---

## Tabla de contenido

- [1. Propósito y alcance de esta categoría](#1-propósito-y-alcance-de-esta-categoría)
- [2. Qué es y qué no es este proyecto de código](#2-qué-es-y-qué-no-es-este-proyecto-de-código)
- [3. Catálogo de casos de uso](#3-catálogo-de-casos-de-uso)
  - [3.1 Criterio de recorte](#31-criterio-de-recorte)
  - [3.2 Numeración](#32-numeración)
- [4. Documento de concepto central](#4-documento-de-concepto-central)
- [5. Matriz de trazabilidad NB → CU → RN → US](#5-matriz-de-trazabilidad-nb--cu--rn--us)
  - [5.1 Matriz](#51-matriz)
  - [5.2 Por qué la columna RN queda vacía](#52-por-qué-la-columna-rn-queda-vacía)
  - [5.3 Cobertura de las NB del producto](#53-cobertura-de-las-nb-del-producto)
- [6. Propiedades transversales verificables](#6-propiedades-transversales-verificables)
- [7. Artefactos omitidos y su motivo](#7-artefactos-omitidos-y-su-motivo)
- [8. Vocabulario](#8-vocabulario)
- [9. Control de cambios](#9-control-de-cambios)

---

## 1. Propósito y alcance de esta categoría

Este documento es el índice maestro de la especificación funcional de **GeometriaFactory-Visor**, el proyecto de código de tipo `library` que produce el archivo de guion del visualizador tridimensional del producto **Fábrica de Geometría**.

La categoría es de **nivel proyecto de código** y su superficie es angosta y declarada: seis funciones planas —las cinco que declara PRODUCT-INTAKE §17.7 P.3 y la sexta que el Product Owner agregó el 2026-08-09, acuñada en `Definicion-Contrato-De-Fachada.md` §4.6—. Por eso cada caso de uso describe **un contrato de uso** y no un flujo de pantallas, según la variante de `Rules-Especificacion-Funcional.md` §1.2 para el tipo `library`.

## 2. Qué es y qué no es este proyecto de código

`GeometriaFactory-Visor` es un **visualizador puro**: la regla de arquitectura `RA-02` del producto lo define como un archivo de guion sin configuración, sin red y sin conocimiento del sistema (PRODUCT-INTAKE §14). De esa definición se siguen los límites que toda esta categoría respeta:

| Este proyecto de código | Sí | No |
| --- | --- | --- |
| Actores | El componente que lo embebe y el texto que recibe | Ninguna persona, ningún papel, ningún servicio |
| Datos | Los que recibe por parámetro en cada invocación | No pide datos por red ni lee configuración propia |
| Decisiones | De qué dimensión saca una malla | Ninguna decisión de validez, de autorización ni de negocio |
| Estado | El de sus instancias vivas, mientras la página vive | Ningún estado entre páginas, ninguna escritura en el almacenamiento del navegador |
| Salida | Mallas en una escena, y el resultado de dibujo | Ninguna observación: ni advertencias ni errores de validación, que son del backend |

Un caso de uso de esta categoría en el que el alumno, el docente, el backend, un servicio o una credencial **intervinieran como actor o condicionaran un flujo** estaría mal escrito por definición. Nombrarlos para declarar qué queda fuera del contrato es, en cambio, **obligatorio**: es lo que impide que un lector aguas abajo le atribuya a la fachada una validación, una decisión de autorización o una obtención de datos que no hace. Por eso `CU-06` se titula «sin backend» y por eso `CU-02` nombra al backend en sus notas.

## 3. Catálogo de casos de uso

| ID | Caso de uso | Función de la fachada | Estado | Enlace |
| --- | --- | --- | --- | --- |
| CU-01 | Inicializar una instancia del visor sobre un elemento de dibujo | `inicializar(elemento, opciones)` | Propuesto | [CU-01](Casos-De-Uso/CU-01-Inicializar-Instancia-Del-Visor.md) |
| CU-02 | Cargar el texto del trabajo y dibujar sus piezas | `cargarJson(id, texto)` | Propuesto | [CU-02](Casos-De-Uso/CU-02-Cargar-El-Texto-Del-Trabajo-Y-Dibujar.md) |
| CU-03 | Seleccionar una pieza por su índice | `seleccionarPieza(id, indice)` | Propuesto | [CU-03](Casos-De-Uso/CU-03-Seleccionar-Una-Pieza-Por-Su-Indice.md) |
| CU-04 | Redimensionar la escena al elemento de dibujo | `redimensionar(id)` | Propuesto | [CU-04](Casos-De-Uso/CU-04-Redimensionar-La-Escena.md) |
| CU-05 | Destruir la instancia y liberar sus recursos | `destruir(id)` | Propuesto | [CU-05](Casos-De-Uso/CU-05-Destruir-La-Instancia-Y-Liberar-Recursos.md) |
| CU-06 | Ejercitar la fachada completa sin backend | Las seis, en recorrido | Propuesto | [CU-06](Casos-De-Uso/CU-06-Ejercitar-La-Fachada-Sin-Backend.md) |
| CU-07 | Gobernar el movimiento automático de la escena sobre una instancia viva | `establecerMovimiento(id, opciones)` | Propuesto | [CU-07](Casos-De-Uso/CU-07-Gobernar-El-Movimiento-Automatico-De-La-Escena.md) |

Siete casos de uso, sobre un mínimo de cinco declarado para el tipo `library` en `Rules-Especificacion-Funcional.md` §2.2.

### 3.1 Criterio de recorte

1. **Una función de la fachada, un caso de uso.** Cada una de las seis funciones es un contrato de uso independiente: tiene su propio actor invocante, sus propias precondiciones y su propio conjunto de condiciones de error. Fusionarlas habría producido un caso de uso con más de un actor primario y con flujos que no se disparan entre sí. Es este criterio el que obliga a emitir `CU-07` cuando el Product Owner agrega la sexta función el 2026-08-09: `establecerMovimiento` no se dispara desde ninguno de los otros seis y no cabe como flujo alternativo de `CU-01`, porque su precondición es una instancia **ya viva** cuyo estado de movimiento se quiere cambiar, y no la creación de una instancia.
2. **Un caso de uso transversal, y sólo uno.** `CU-06` recorre las seis funciones desde una página integradora sin backend. Existe porque las **seis** propiedades que verifica —cero red, cero persistencia, se ejercita sin backend, disposición determinista, liberación de recursos y ausencia de fallo silencioso, enumeradas con su umbral en §6— son transversales: repartidas como excepciones de los otros seis, ninguno las verificaría juntas, y es además el sample S-1 del producto, que el intake declara como el que demuestra el punto de extensión.
3. **Nada más.** No hay casos de uso de configuración, de sesión, de obtención de datos ni de validación, porque el proyecto de código no hace ninguna de esas cosas. Rotar y acercar con el mouse tampoco es caso de uso: son gestos que la instancia atiende sobre la escena ya creada y no atraviesan ninguna de las seis funciones. El **movimiento automático**, en cambio, sí atraviesa la fachada —`inicializar` lo fija al nacer y `establecerMovimiento` lo cambia en vivo— y por eso tiene caso de uso, `CU-07`.

### 3.2 Numeración

La numeración `CU-01` a `CU-07` es **contigua y propia de este proyecto de código**. `CU-07` nace después que el transversal `CU-06` porque se emitió más tarde, con la sexta función: **no se renumera**, porque renumerar rompería las referencias ya emitidas aguas abajo por un motivo puramente cosmético. El orden de lectura, en cambio, es el del ciclo de vida —`CU-01` a `CU-05`, después `CU-07` y por último `CU-06`, que los recorre juntos—, y así lo declara el `README.md` de la sección. Las `CU-15`, `CU-16` y `CU-17` que `NB-06` declara previstas son la numeración de nivel producto que la necesidad anticipó antes de repartirse por proyecto de código; la parte que le toca a este proyecto de código son estos **siete** contratos de uso, y la correspondencia queda declarada en §5.1 para que la trazabilidad no se pierda.

## 4. Documento de concepto central

[`Definicion-Contrato-De-Fachada.md`](Definicion-Contrato-De-Fachada.md) es el documento de concepto central de esta categoría, admitido por `Rules-Especificacion-Funcional.md` §2.1. Define el vocabulario, el ciclo de vida de una instancia, las siete garantías transversales, las siete prohibiciones, la semántica de las **seis** funciones, los cinco elementos del concepto, los siete códigos de condición y la política de compatibilidad de la superficie pública. Es además el lugar donde se **acuñó la sexta función**, `establecerMovimiento` (§4.6), que el Product Owner **ya consolidó en el intake**: `PRODUCT-INTAKE` §17.7 P.3 la declara desde su versión **1.6**, con la nota «**Sexta función** [DECISIÓN 2026-08-09]» y remitiendo a §4.6 de ese documento por su especificación.

Existe porque el contrato de la fachada es el **punto de extensión declarado del producto** (PRODUCT-INTAKE §18), y porque los siete casos de uso comparten su vocabulario y sus códigos: declararlos una vez evita siete definiciones que se desincronizan.

## 5. Matriz de trazabilidad NB → CU → RN → US

### 5.1 Matriz

| NB | CU previsto a nivel producto | CU de este proyecto de código | RN | US a generar en 06 |
| --- | --- | --- | --- | --- |
| NB-06 | CU-15 previsualizar el trabajo en tres dimensiones | CU-01, CU-02, CU-04, CU-05 | — | US de creación de instancia, de dibujo del trabajo, de ajuste al espacio disponible y de liberación de recursos |
| NB-06 | CU-16 explorar la estructura del trabajo como árbol colapsable | CU-02 (la fachada devuelve la estructura del texto; la presentación del árbol es del componente anfitrión) | — | US de entrega de la estructura del texto para el árbol |
| NB-06 | CU-17 sincronizar el árbol y la escena por índice de pieza | CU-03, y la disposición determinista de CU-02 | — | US de resaltado exclusivo por índice y de disposición derivada del índice |
| NB-06 | — (criterios segundo, tercero y cuarto de su §5, verificados juntos) | CU-06 | — | US de la página integradora sin backend y de la verificación de cero red y cero persistencia |
| NB-06 | — (capacidad **F-25** del intake §4, **`Must Have`** desde el intake 1.7, incorporada el 2026-08-09; su CU de nivel producto es **CU-28**, previsto por `NB-06` §7 después de aquel reparto) | CU-07, y las dos opciones de gobierno de CU-01 | — | US de gobierno en vivo de los dos movimientos automáticos, sin reconstrucción de la instancia y sin pérdida de la selección |
| NB-04 | CU-12 interpretar el texto del trabajo y reportar los errores con figura y campo | CU-02, **sólo en su parte de piezas efectivamente dibujadas**: la fachada lee las mismas variantes de clave para que ninguna pieza que el producto interpreta quede sin dibujar. La interpretación, los errores con índice y campo y las observaciones **no** son de este proyecto de código | — | US de lectura de dimensiones con las variantes de clave del emisor |

### 5.2 Por qué la columna RN queda vacía

La columna está vacía en las **seis** filas de §5.1 y **es correcto**: `GeometriaFactory-Visor` es un visualizador puro y **no tiene reglas de dominio**. Las que rigen el trabajo del alumno —qué se puede finalizar, qué produce advertencia, quién ve qué— las decide el backend, y este proyecto de código no participa de ninguna de esas decisiones (PRODUCT-INTAKE §14 RA-02, §17.7 P.5 y P.11 punto 4).

Lo que sí tiene son **condiciones de contrato**, que no son reglas de negocio: están declaradas una sola vez en `Definicion-Contrato-De-Fachada.md` §6 y referenciadas por cada caso de uso. Escribirlas como `RN-XX` habría sido el anti-patrón inverso al de «RN escrita como CU»: una condición técnica del contrato disfrazada de invariante del dominio.

### 5.3 Cobertura de las NB del producto

| NB | ¿La toca este proyecto de código? | Fundamento |
| --- | --- | --- |
| NB-01 Control de admisión y de bajas | No | Admisión y bajas de cuentas. La fachada no sabe quién es la persona ni qué papel cumple |
| NB-02 Identidad propia del alumno sin correo | No | Credenciales e identidad. Prohibición explícita de PRODUCT-INTAKE §17.7 P.5 |
| NB-03 Trabajo con dueño, estado y persistencia | No | Persistencia y estado del trabajo. Prohibición explícita de PRODUCT-INTAKE §17.7 P.4 |
| NB-04 Interpretación fiel del dato del alumno | **Parcialmente**, sólo en la parte de que las piezas se dibujen | La interpretación, la localización del error y el límite entre guardar y entregar son del backend |
| NB-05 Visibilidad del error de cálculo | No | Recalcular valores y emitir advertencias es del backend |
| NB-06 Visualización dentro del producto | **Sí, es su necesidad** | Los siete casos de uso la implementan desde el archivo de guion |
| NB-07 Revisión de la comisión en un solo lugar | No | Listar, filtrar y agrupar trabajos es del backend y del componente anfitrión. La fachada ya sirve al administrador por ser la misma para los dos papeles, sin saberlo |
| NB-08 Alcance del laboratorio desde el aula | No | Disponibilidad y despliegue. Este proyecto de código contribuye de forma negativa —no hacer red—, lo que se verifica en CU-06, pero no implementa ninguna capacidad de la necesidad |
| NB-09 Desenlace explícito de la entrega | No | Aprobar, rechazar y comentar un trabajo es del backend y del componente anfitrión. La fachada dibuja el mismo trabajo para el alumno y para el administrador **sin saber cuál de los dos lo mira** ni en qué estado está, que es exactamente lo que RA-02 exige |

Cobertura bidireccional dentro del alcance de este proyecto de código: **ningún caso de uso queda huérfano** —los siete trazan a NB-06, CU-02 traza además a NB-04 y CU-07 traza además a la capacidad F-25 del intake §4— y **la única NB que este proyecto de código implementa, NB-06, tiene casos de uso**. Las **siete** NB restantes se implementan en otros proyectos de código del producto, no quedan sin cubrir por esta declaración.

## 6. Propiedades transversales verificables

**Seis** propiedades atraviesan los siete casos de uso. Esta tabla es el **lugar único** donde se declaran su membresía, su umbral y **las condiciones en que se miden**, para que 08-Calidad-Y-Pruebas las tome como están; §3.1 punto 2, `Definicion-Contrato-De-Fachada.md` §4.6 y `CU-06` §1 enumeran las mismas seis y remiten acá.

| Propiedad | Umbral verificable | Condiciones de medición | Dónde se verifica |
| --- | --- | --- | --- |
| Cero red | Exactamente **0 peticiones** originadas por el archivo de guion, contadas en la pestaña de red | **Con los dos movimientos automáticos prendidos** —órbita de la cámara y giro de las figuras, `Definicion-Contrato-De-Fachada.md` §5.5—, sostenidos el tiempo suficiente para que el bucle de dibujo corra, y también durante los gestos de rotar y acercar. Ver la nota de abajo | CA de red de CU-01 a CU-07; CU-06 CA-02; CU-07 CA-05 |
| Cero persistencia | **0 claves** escritas en el almacenamiento del navegador, y ningún estado conservado entre páginas | Cualquier estado de los movimientos. La preferencia de movimiento **no se guarda** en la fachada: se comprueba que prender y apagar con `establecerMovimiento` no escribe ninguna clave, y que recargar la página no la repone | CU-06 CA-03; CU-07 CA-05 |
| Se ejercita sin backend | Recorrido completo de las **seis** funciones con un texto pegado a mano, con **0 servicios del backend disponibles** | Sin condición adicional | CU-06 CA-01 |
| Disposición determinista | Dos procesados del mismo texto producen la **misma disposición**, comparable pieza por pieza | **Se compara posición, no orientación** (garantía G-6). La propiedad vale con cualquier estado de los movimientos, y prenderlos o apagarlos con la instancia viva no la altera | CU-02 CA-04; CU-06 CA-04; CU-07 CA-01 |
| Liberación de recursos | **10 recorridos** de ida y vuelta entre trabajos sin degradación | **Con los dos movimientos prendidos** durante los recorridos: un bucle de dibujo que sobreviviera a `destruir` es exactamente la forma de degradación que esta propiedad tiene que descartar, y con los movimientos apagados no se ejercitaría | CU-05 CA-04; CU-06 CA-05 |
| Ausencia de fallo silencioso | **100 %** de las piezas no dibujadas enumeradas en el resultado de dibujo con su índice y su código de condición, y **0 piezas** que dejen de aparecer sin quedar registradas. Es la garantía G-5 del contrato, y es la propiedad que cierra el problema original de NB-06: hoy, en la visualización previa, la figura simplemente no aparece y nadie se entera | Sin condición adicional: la enumeración es del resultado de dibujo y el movimiento no la toca | CU-02 CA-05 y FA-02; CU-06 CA-01 y FA-03 |

**Por qué la propiedad de cero red declara sus condiciones.** El umbral no cambia —sigue siendo **exactamente 0**— pero sin condiciones declaradas la prueba mediría el caso fácil. Los entornos de prueba automatizados suelen declarar preferencia de movimiento reducido; el componente anfitrión que la respeta invoca `inicializar` con los dos movimientos apagados, y una prueba escrita ahí quedaría en verde **sin haber ejercitado nunca un bucle de dibujo corriendo sesenta veces por segundo**, que es el caso donde una petición de red se colaría. Por eso la medición vale y se realiza con los dos movimientos prendidos, que es su peor caso. Que la fachada no consulte esa preferencia por su cuenta (G-3) es lo que hace que la prueba pueda prenderlos aunque el entorno la declare.

**Verificación de las demás.** Se revisaron las otras cinco buscando la misma indeterminación. **Liberación de recursos** la tenía y queda precisada arriba, por el mismo motivo: el peor caso es un bucle en curso al momento de `destruir`. **Disposición determinista** exigía la precisión inversa —qué se compara— y también queda declarada. **Cero persistencia** suma la comprobación de que la preferencia de movimiento no se guarda. Las dos restantes —se ejercita sin backend y ausencia de fallo silencioso— no dependen del estado de los movimientos y se declaran **sin condición adicional**, para que no se les invente una aguas abajo.

Material de dibujo declarado: el escenario **E-1** del intake —tres piezas, `Cilindro`, `Cubo` y `Ortoedro`, con el ortoedro dibujado— y el escenario **E-7** —seis piezas que cubren los seis tipos dibujables—. E-1 tiene su texto editado a mano y no ejercita las tolerancias del formato; las trampas del formato las ejercita E-2, que es material del backend.

## 7. Artefactos omitidos y su motivo

| Artefacto | Estado | Motivo |
| --- | --- | --- |
| `Reglas-De-Negocio/RN-XX-<Nombre>.md` | Omitido | Un visualizador puro no tiene reglas de dominio: las decide el backend. `Rules-Especificacion-Funcional.md` §2.2 no las exige para `library`. Ver §5.2 |
| `Modelo-Datos/Modelo-Conceptual.md` y sus `RC-XX` | Omitido | `Rules-Especificacion-Funcional.md` §2.1 y §2.2 los omiten para `library` sin estado, y el flag `tiene_persistencia` de este proyecto de código es **false** |

El `README.md` de la sección repite las dos omisiones con su motivo, según exige el encargo de la categoría.

## 8. Vocabulario

El vocabulario que esta categoría acuña vive en [`Glosario-Funcional.md`](Glosario-Funcional.md), obligatorio para los ocho tipos D8. Los términos ya declarados en el glosario raíz del producto —`../../../00-Contexto/Vision-Producto.md` §9— se referencian y no se redefinen.

Tres decisiones de vocabulario rigen todos los artefactos de esta categoría:

1. **«Trabajo»** es lo que el alumno entrega en el laboratorio. No es una «unidad de entrega» en el sentido normativo.
2. **«Pieza»** en su forma desnuda designa cada figura del conjunto raíz del trabajo. El segundo referente —cada artefacto desplegable del producto— se escribe siempre calificado y no aparece en estos artefactos.
3. **«Observación»** es el superordinado de «advertencia» y «error de validación». Este proyecto de código **no emite ninguna de las tres**, y las nombra sólo para declararlo.

## 9. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Índice maestro de la especificación funcional de `GeometriaFactory-Visor`: catálogo de seis casos de uso con su criterio de recorte, documento de concepto central, matriz NB → CU → RN → US con la columna RN vacía y su motivo declarado, cobertura declarada de las ocho NB del producto, propiedades transversales con umbral verificable y las dos omisiones de artefacto con su fundamento. |
| 1.0 | 2026-08-08 | Correcciones absorbidas del audit `B-02-03-GeometriaFactory-Visor-r1.md`, sin subir versión por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **H-02**: la membresía de las propiedades transversales se unifica en **seis** —§3.1 punto 2 y §6 nombran las mismas—, §6 se declara lugar único de membresía y umbral, y suma la fila «Ausencia de fallo silencioso» con umbral verificable (100 % de las piezas no dibujadas enumeradas con índice y código; 0 piezas sin registro), que es la garantía G-5 y la que cierra el problema original de NB-06. **H-10**: la cabecera sustituye las referencias sin sección por `Compatibilidad-Plataformas.md` §2.2 y §4, `NB-06` §1, §4, §5 y §7, y `NB-04` §4, y completa el resto con su sección concreta. **H-11**: §2 deja de prohibir *mencionar* al alumno, al docente, al backend, a un servicio o a una credencial, y pasa a prohibir que **intervengan como actor o condicionen un flujo**, declarando que nombrarlos para excluirlos es obligatorio. |
| 1.0 | 2026-08-09 | Absorción de las **dos decisiones del Product Owner** tomadas al cerrar la validación visual de la **Fase B2** del proyecto de código `GeometriaFactory-Web`, dentro de la cual se validó la fachada de este proyecto de código. **Sin subir versión** por `Master-Prompt.md` §5, que lo admite mientras el documento está en estado `Propuesto`. **(a) Sexta función de la fachada**, `establecerMovimiento(id, opciones)`, que gobierna los dos movimientos de la capacidad **F-25** sin reconstruir la instancia y sin perder la selección de pieza: **§3** suma **`CU-07`** al catálogo y pasa a **siete** casos de uso; **§3.1 punto 1** declara el fundamento de emitir caso de uso nuevo en lugar de ampliar `CU-01` —precondición propia, una instancia ya viva— y el punto 2 pasa a decir que `CU-06` recorre las **seis** funciones; **§3.2** declara por qué `CU-07` no se renumera antes del transversal y cuál es el orden de lectura; **§4** actualiza el conteo de la superficie y declara que el concepto central es donde la sexta función se acuña; **§5.1** suma la fila de F-25 y **§5.3** los conteos. La consolidación en el intake §17.7 P.3, que declara cinco funciones, **queda pendiente del orquestador** y no se hace desde acá. **(b) Condiciones de medición de las propiedades transversales**: **§6** suma una columna de **condiciones de medición** y declara que la propiedad de **cero red** se mide **con los dos movimientos prendidos** —su peor caso—, con el fundamento de que un entorno de prueba con preferencia de movimiento reducido los tendría apagados y mediría el caso fácil; el umbral no cambia y sigue siendo exactamente 0. La misma revisión precisa **liberación de recursos** —también con los movimientos prendidos—, **disposición determinista** —se compara posición y no orientación— y **cero persistencia** —la preferencia de movimiento no se guarda—, y declara **sin condición adicional** las dos restantes. Se corrigen de paso dos conteos de §4 que la propagación anterior había dejado desactualizados: las prohibiciones del concepto central son **siete** y sus elementos son **cinco**. |
| 1.1 | 2026-08-09 | **Cierra el hallazgo `F26-11`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0, contra `PRODUCT-INTAKE` **1.9**. Dos afirmaciones de este documento habían quedado escritas contra versiones anteriores del intake y dejaron de ser ciertas. **§4**: decía que la sexta función se acuña acá «**mientras el intake declara cinco**»; el intake la **consolidó en su versión 1.6** y su §17.7 P.3 declara seis, con la sexta rotulada como decisión del 2026-08-09. **§5.1**: declaraba F-25 como capacidad `Should Have` y el intake la subió a **`Must Have`** en su versión 1.7, con la constancia escrita en la propia celda de §4; la fila registra además que la capacidad **sí tiene CU previsto a nivel producto**, `CU-28`, que `NB-06` §7 prevé. Ninguna función, garantía, condición ni caso de uso de este proyecto de código cambia: la superficie sigue siendo de seis funciones y siete códigos. |
| 1.2 | 2026-08-09 | **Cierra las tres filas de este proyecto de código del hallazgo `F26-20`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0, contra `PRODUCT-INTAKE` **1.10**. Los tres son recuentos que la emisión de `CU-07` y el crecimiento del catálogo de necesidades dejaron atrás, y ninguno cambia una función, un código ni un caso de uso. **§3.2** decía «estos **seis** contratos de uso» donde el catálogo tiene **siete** desde que entró `CU-07`. **§5.2** decía «las **cinco** filas» donde la matriz de §5.1 tiene **seis**, la sexta desde que se sumó la fila de F-25. **§5.3** declaraba la cobertura sobre **ocho** necesidades de negocio y el producto tiene **nueve**: entra la fila de **`NB-09`**, desenlace explícito de la entrega, declarada **fuera del alcance** de este proyecto de código con su fundamento —aprobar, rechazar y comentar es del backend y del anfitrión, y la fachada dibuja el mismo trabajo sin saber quién lo mira, que es lo que RA-02 exige—, y el recuento de las restantes pasa de seis a **siete**. Sube minor: corrige recuentos declarados y suma una fila de cobertura negativa, sin tocar la superficie de seis funciones ni los siete códigos de condición. |
