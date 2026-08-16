# Contrato de la superficie pública — GeometriaFactory-Visor

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Visor
**Documento:** Contratos-Abstractions.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)

---

## Tabla de contenido

- [1. Alcance del contrato](#1-alcance-del-contrato)
- [2. Formato](#2-formato)
- [3. Operaciones: las seis funciones](#3-operaciones-las-seis-funciones)
- [4. Esquemas de datos](#4-esquemas-de-datos)
  - [4.1 Identificador de instancia](#41-identificador-de-instancia)
  - [4.2 Resultado de dibujo](#42-resultado-de-dibujo)
  - [4.3 Opciones de movimiento](#43-opciones-de-movimiento)
  - [4.4 Conjuntos cerrados](#44-conjuntos-cerrados)
- [5. Manejo de errores](#5-manejo-de-errores)
- [6. Versionado del contrato](#6-versionado-del-contrato)
- [7. Trazabilidad](#7-trazabilidad)
- [8. Control de cambios](#8-control-de-cambios)

---

## 1. Alcance del contrato

Este documento declara la superficie pública de `GeometriaFactory-Visor` **desde el plano arquitectónico**: qué expone el bundle, con qué compromisos y con qué política de compatibilidad.

**No redefine la semántica de las funciones.** Ésa vive en [`../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md), que es el documento de concepto central de este proyecto de código y la fuente única de las **siete garantías**, los **siete códigos de condición** y la semántica de cada función. Acá se declara lo que corresponde a 05: qué componente sostiene qué, qué se verifica y qué constituye un cambio incompatible.

Los casos de uso que se materializan a través de este contrato son los **siete** de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3, y su único consumidor es el componente anfitrión, que vive en `GeometriaFactory-Web`.

Este contrato es además **el punto de extensión declarado del producto**; su tratamiento como tal está en [`Extensibilidad.md`](Extensibilidad.md).

## 2. Formato

**Contrato de fachada de biblioteca cargada en el navegador, declarado en prosa estructurada.** No hay descripción formal de servicio ni esquema de mensajes: no hay protocolo, porque no hay red.

La salida se expone como **biblioteca bajo un solo nombre propio en el objeto global del navegador**, sin identificadores globales sueltos.

**Los nombres de las seis funciones están fijados** por `PRODUCT-INTAKE` §17.7.P.3 y no se cambian. Los nombres de las funciones internas, de las clases y de los campos del resultado de dibujo **no se fijan acá**: se anclan en la etapa que implementa la fachada.

## 3. Operaciones: las seis funciones

| # | Función | Qué recibe | Qué devuelve | Componente que la sirve |
| --- | --- | --- | --- | --- |
| 1 | `inicializar(elemento, opciones)` | El elemento de dibujo y opciones de presentación, dos de ellas de gobierno del movimiento | Un identificador de instancia | Fachada, Registro de instancias, Servicio de dibujo |
| 2 | `cargarJson(id, texto)` | Identificador de una instancia viva y el texto del trabajo | El resultado de dibujo | Fachada, Lector del texto, Servicio de dibujo |
| 3 | `seleccionarPieza(id, indice)` | Identificador y el índice de la pieza en el conjunto raíz | La confirmación de la selección efectiva, o la condición que lo impidió | Fachada, Servicio de dibujo |
| 4 | `redimensionar(id)` | Identificador | La confirmación de que la escena quedó ajustada al tamaño vigente | Fachada, Servicio de dibujo |
| 5 | `destruir(id)` | Identificador | La confirmación de la liberación | Fachada, Registro de instancias, Servicio de dibujo |
| 6 | `establecerMovimiento(id, opciones)` | Identificador y el estado deseado de uno de los dos movimientos, o de los dos | El estado efectivo de **los dos** movimientos, o la condición que impidió aplicarlo | Fachada, Servicio de dibujo |

**Seis funciones.** `inicializar` es la única que se invoca **sin** identificador; las otras cinco lo exigen, y por eso `INSTANCIA_DESCONOCIDA` se presenta en cinco funciones.

Tres reglas de ciclo de vida que el contrato impone y que la arquitectura tiene que sostener:

1. **Una instancia liberada no vuelve a la vida.** Para volver a dibujar sobre el mismo elemento, el anfitrión invoca `inicializar` otra vez y obtiene un identificador nuevo.
2. **Cada invocación posterior de `cargarJson` reemplaza por completo el contenido dibujado y libera lo anterior.** Es lo que sostiene el requerimiento de no degradar tras diez recorridos.
3. **El estado de los movimientos sobrevive a `cargarJson`.** Cargar otro texto reemplaza el contenido dibujado, no el gobierno de la escena.

## 4. Esquemas de datos

### 4.1 Identificador de instancia

**Valor opaco.** Su forma no se fija ni en 02 ni acá, y cambiarla **no es cambio de contrato** mientras conserve sus tres propiedades semánticas: identifica una instancia viva y sólo una; deja de ser válido en cuanto `destruir` retorna y **no se reutiliza**; y un identificador que no corresponde a ninguna instancia viva produce `INSTANCIA_DESCONOCIDA` y ninguna otra consecuencia.

**Que el anfitrión dependa de su forma es un defecto del anfitrión.**

### 4.2 Resultado de dibujo

Es lo que `cargarJson` devuelve. Se llama **resultado de dibujo** y no «resultado de la interpretación» para que no se confunda con el que emite el backend, que lleva observaciones y decide si el trabajo puede finalizar. **El resultado de dibujo no lleva observaciones.**

| Elemento | Semántica | Garantía que sostiene |
| --- | --- | --- |
| Piezas dibujadas | Una entrada por pieza que produjo malla, con su índice en el conjunto raíz y su tipo | G-6 |
| Piezas no dibujadas | Una entrada por pieza que no produjo malla, con su índice y el código que lo explica | **G-5** |
| Estructura del texto | La representación jerárquica del texto recibido, para que el anfitrión la presente como árbol colapsable | — |
| Condición general | El código de la invocación completa cuando no se pudo dibujar nada | G-7 |

**Admite entradas nuevas sin subir mayor**, siempre que las cuatro declaradas conserven su semántica.

### 4.3 Opciones de movimiento

Las mismas dos opciones en las dos funciones que las reciben, **con una diferencia de semántica que corresponde al momento**:

| Función | Qué pasa con la opción ausente |
| --- | --- |
| `inicializar` | **Arranca apagada.** Ante opciones ausentes o parciales, la instancia nace con los dos movimientos apagados: la fachada no consulta preferencias del sistema y el arranque quieto es el que no sorprende |
| `establecerMovimiento` | **Conserva el estado que tenía**, porque la escena ya tiene uno |

`establecerMovimiento` es **idempotente**: fijar el estado que ya estaba no cambia nada.

### 4.4 Conjuntos cerrados

| Conjunto | Valores | Cantidad |
| --- | --- | --- |
| Tipos de pieza dibujables | Tres volumétricos —`Cilindro`, `Cubo`, `Ortoedro`— y tres planos —`Rectangulo`, `Cuadrado`, `Circulo`— | 6 |
| Movimientos automáticos gobernables | Órbita de la cámara, giro de las figuras | 2 |
| Códigos de condición | Los siete de §5 | 7 |

`RectanguloDesarrollado` **no es un tipo dibujable como pieza**: aparece únicamente como componente de un cilindro, y la fachada lo usa para leer una dimensión, no para dibujar una pieza suelta.

## 5. Manejo de errores

**Siete códigos de condición**, cuya fuente única es §6 de [`../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md). Un código que no figure allá no existe, y **un código nuevo sólo puede nacer en la categoría 02**.

| # | Código | Cuándo se produce | Efecto sobre la instancia |
| --- | --- | --- | --- |
| 1 | `CAPACIDAD_GRAFICA_AUSENTE` | El navegador no provee la capacidad gráfica tridimensional requerida | No se crea instancia |
| 2 | `ELEMENTO_DE_DIBUJO_INVALIDO` | **Dos cursos**: en creación, el elemento no sirve como superficie; en ajuste, el elemento de una instancia viva dejó de servir | En creación, no se crea instancia. En ajuste, **la instancia sigue viva** con su escena y su selección intactas |
| 3 | `INSTANCIA_DESCONOCIDA` | El identificador no corresponde a ninguna instancia viva. Se presenta en **cinco** funciones | Ninguno |
| 4 | `TEXTO_NO_LEGIBLE` | El texto recibido no permite obtener un conjunto de piezas | La instancia queda viva y vacía |
| 5 | `TIPO_NO_DIBUJABLE` | Una pieza declara un tipo que no está entre los seis dibujables | Esa pieza no se dibuja; las demás sí |
| 6 | `DIMENSION_NO_LEGIBLE` | Una pieza de tipo dibujable **no expone** la dimensión necesaria. **Un valor de `0.00` no produce esta condición** | Esa pieza no se dibuja; las demás sí |
| 7 | `INDICE_FUERA_DE_RANGO` | El índice recibido no corresponde a ninguna **pieza dibujada** del resultado vigente | Ninguno: la selección vigente se conserva |

**Curso y código no son lo mismo.** `ELEMENTO_DE_DIBUJO_INVALIDO` tiene **dos cursos y un solo código**, porque la causa y la reacción que le queda al anfitrión son las mismas y lo que cambia es el momento del ciclo de vida. Un curso nuevo se agrega como fila de curso; un código nuevo, nunca aguas abajo. La consecuencia de esa distinción se ve en la categoría 03: su catálogo creció de **doce a trece** entradas al entrar la sexta función, **sin** que el conjunto de códigos creciera, porque su unidad de catalogación es la **función** y la de este contrato es la **condición**.

**Ninguno de los dos movimientos automáticos emite condición.** Un movimiento que no arranca porque la instancia no existe es `INSTANCIA_DESCONOCIDA` y nada más.

## 6. Versionado del contrato

Aplica el criterio de [`ADR-12006`](Adrs/ADR-12006-Bundle-Generado-Y-Versionado-Del-Punto-De-Extension.md) §7. Lo esencial:

| Cambio | Clase |
| --- | --- |
| Quitar una función, renombrarla o cambiar qué recibe | Mayor |
| **Perder cualquiera de las siete garantías**, aunque las seis firmas no se toquen | Mayor |
| Cambiar la semántica de una entrada ya declarada del resultado de dibujo | Mayor |
| Agregar una función | Menor |
| Agregar una entrada nueva al resultado de dibujo | Menor |
| Agregar un código de condición | Menor |
| Cambiar la forma interna del identificador de instancia, conservando sus tres propiedades | Sin efecto de contrato |
| Corregir el interior de la capa 3 sin cambiar la superficie ni las garantías | Parche |

**Compatibilidad hacia atrás y deprecación.** El anfitrión **no compila contra este artefacto**: lo carga y lo invoca por interoperabilidad, de modo que **ningún cambio mayor lo detecta una compilación**. La mitigación es doble: la revisión, y el sample **S-1**, que ejerce el contrato entero sin ninguna pieza del backend y por eso detecta la ruptura sin necesidad de levantar el producto.

## 7. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| CU que lo consumen | CU-12001 a CU-12007, los siete de la categoría 02 de este proyecto de código |
| NB que sostiene | NB-00006, y NB-00004 parcialmente |
| RN que cubre | **Ninguna.** Un visualizador puro no tiene reglas de dominio |
| Garantías que compromete | G-1 a G-7, con el reparto de [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) §10.2 |
| ADR que lo gobiernan | ADR-12001, ADR-12002, ADR-12003, ADR-12004, ADR-12005, ADR-12006 |
| Consumidor | El componente anfitrión, que vive en `GeometriaFactory-Web` |
| Ejemplos | Sample **S-1**, la página integradora sin backend, que ejerce las seis funciones |
| Tests previstos en 08 | Las siete garantías; las seis propiedades transversales con sus condiciones de medición; los escenarios E-1 y E-7; las puertas `PT-02` y `PT-03` |

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Declara las seis funciones con el componente que sirve a cada una y las tres reglas de ciclo de vida, los cuatro elementos del resultado de dibujo con la garantía que sostienen, la asimetría de las opciones de movimiento entre `inicializar` y `establecerMovimiento`, los tres conjuntos cerrados, los siete códigos con la distinción entre curso y código, y el criterio de versionado con la constancia de que ningún cambio mayor lo detecta una compilación. |
