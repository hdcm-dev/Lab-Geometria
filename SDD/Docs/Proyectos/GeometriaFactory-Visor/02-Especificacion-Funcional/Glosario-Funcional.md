# Glosario Funcional — GeometriaFactory-Visor

**Proyecto de código:** GeometriaFactory-Visor
**Documento:** Glosario-Funcional.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-08
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `00-Contexto/Vision-Producto.md` §9 (glosario raíz de la cadena, §9.1 términos del dominio del cliente, §9.2 términos que esa categoría precisa, §9.3 resolución del choque de vocabulario); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §12, §12.1, §14 (RA-02) y §17.7; `Vocabulario-Rules.md` §2, §4 y §9
**Trazabilidad downstream:** 03-UX-UI-DX de este proyecto de código (`Glosario-UX.md` referencia estos términos en lugar de duplicarlos), 05-Arquitectura-Tecnica, 06-Backlog-Tecnico, 08-Calidad-Y-Pruebas, 10-Examples

---

## Tabla de contenido

- [1. Alcance de este glosario](#1-alcance-de-este-glosario)
- [2. Tabla de términos que esta categoría acuña](#2-tabla-de-términos-que-esta-categoría-acuña)
- [3. Términos con más de un referente](#3-términos-con-más-de-un-referente)
- [4. Términos referenciados y no redefinidos](#4-términos-referenciados-y-no-redefinidos)
- [5. Control de cambios](#5-control-de-cambios)

---

## 1. Alcance de este glosario

Declara el vocabulario que la especificación funcional de `GeometriaFactory-Visor` **acuña**: el de la escena, la malla, el árbol, la selección y la instancia. Todo lo que ya declara el glosario raíz del producto —`Vision-Producto.md` §9— se referencia en §4 y **no se redefine acá**.

La regla de inclusión aplicada es la de `Rules-Especificacion-Funcional.md` §3.3: entra todo término que aparece en más de un artefacto de esta categoría. Un término que vive en un solo artefacto se define ahí y no entra.

## 2. Tabla de términos que esta categoría acuña

| Término | Definición operativa | Artefactos de 02 donde aparece | Sinónimos y alias |
| --- | --- | --- | --- |
| Fachada | Superficie pública del archivo de guion: las **seis** funciones planas que el componente anfitrión puede invocar, y nada más | `Definicion-Contrato-De-Fachada.md`, CU-01 a CU-07 | «Contrato de fachada» cuando se nombra el conjunto de funciones más sus garantías |
| Componente anfitrión | El componente que embebe el archivo de guion e invoca sus funciones. Es el actor primario de los siete casos de uso. No es una persona, y la fachada no sabe qué componente es | `Definicion-Contrato-De-Fachada.md`, CU-01 a CU-07 | «Componente anfitrión mínimo» cuando se trata de la página integradora sin backend de CU-06 |
| Elemento de dibujo | Elemento de la página, provisto por el componente anfitrión, sobre el que una instancia monta su escena | `Definicion-Contrato-De-Fachada.md`, CU-01, CU-04, CU-05, CU-06 | — |
| Instancia del visor | Escena viva asociada a un elemento de dibujo. Nace con `inicializar` y termina con `destruir` | `Definicion-Contrato-De-Fachada.md`, CU-01 a CU-07 | «Instancia», en su forma corta, dentro de esta categoría |
| Identificador de instancia | Valor opaco que `inicializar` devuelve y que las otras cinco funciones exigen. Identifica una instancia viva y deja de ser válido cuando se la libera | `Definicion-Contrato-De-Fachada.md`, CU-01 a CU-07 | — |
| Escena | Espacio tridimensional de una instancia, con su iluminación y su cámara orbital, donde se ubican las mallas | `Definicion-Contrato-De-Fachada.md`, CU-01 a CU-07 | — |
| Malla | Representación tridimensional que la fachada construye para una pieza dibujable y ubica en la escena | `Definicion-Contrato-De-Fachada.md`, CU-02, CU-03, CU-05 | — |
| Tipo dibujable | Cada uno de los seis tipos de pieza que la fachada sabe convertir en malla: `Cilindro`, `Cubo`, `Ortoedro`, `Rectangulo`, `Cuadrado` y `Circulo` | `Definicion-Contrato-De-Fachada.md`, CU-02, CU-06 | «Pieza dibujable» para la pieza cuyo tipo lo es |
| Resultado de dibujo | Lo que `cargarJson` devuelve: piezas dibujadas con su índice y su tipo, piezas no dibujadas con su índice y su código de condición, y la estructura del texto. **No lleva observaciones** | `Definicion-Contrato-De-Fachada.md`, CU-02, CU-03, CU-06 | El intake lo nombra «el resultado de la interpretación» (§17.7 P.3); ver la nota de §3 |
| Estructura del texto | Representación jerárquica del texto recibido que la fachada devuelve para que el componente anfitrión la presente como árbol colapsable | `Definicion-Contrato-De-Fachada.md`, CU-02, CU-06 | — |
| Árbol | Presentación colapsable de la estructura del texto. La arma el componente anfitrión con lo que la fachada le devuelve | `Definicion-Contrato-De-Fachada.md`, CU-02, CU-03, CU-06 | «Árbol colapsable», forma completa del glosario raíz de la necesidad NB-06 |
| Selección | Estado de a lo sumo una pieza resaltada por instancia. Se fija por índice y se descarta al cargar un trabajo nuevo o al destruir la instancia | `Definicion-Contrato-De-Fachada.md`, CU-03, CU-04, CU-05 | «Resaltado» para el efecto visible de la selección |
| Índice de pieza | Posición de una pieza en el conjunto raíz del trabajo. Es su identidad, porque el dato del alumno no trae identificador propio, y es la clave con la que el árbol y la escena se sincronizan | `Definicion-Contrato-De-Fachada.md`, CU-02, CU-03, CU-06 | — |
| Disposición | Ubicación relativa de las piezas en la escena. Se **deriva del índice** de cada pieza, de modo que dos cargas del mismo texto producen la misma disposición | `Definicion-Contrato-De-Fachada.md`, CU-02, CU-04, CU-06 | «Disposición determinista» cuando se enuncia la propiedad |
| Texto del trabajo | Dato de entrada de `cargarJson`. La fachada lo lee para construir mallas, y ni lo pide, ni lo guarda, ni lo reescribe | `Definicion-Contrato-De-Fachada.md`, CU-02, CU-06 | — |
| Código de condición | Código con el que la fachada informa por qué una invocación no surtió efecto, o por qué una pieza no se dibujó. Es una condición de contrato, **no** una observación de dominio | `Definicion-Contrato-De-Fachada.md`, CU-01 a CU-07 | — |
| Cero red | Propiedad de la fachada: ninguna función origina una petición de red. El umbral es exactamente 0, medido contando peticiones | `Definicion-Contrato-De-Fachada.md`, CU-01 a CU-07 | — |
| Cero persistencia | Propiedad de la fachada: no guarda estado entre páginas ni escribe en el almacenamiento del navegador | `Definicion-Contrato-De-Fachada.md`, CU-01, CU-02, CU-05, CU-06 | — |
| Página integradora | Página sin ninguna pieza del backend que carga el archivo de guion, recibe un texto pegado a mano y ejerce las seis funciones. Es el componente anfitrión de CU-06 y el sample S-1 del producto | `Definicion-Contrato-De-Fachada.md`, CU-06 | «Página de prueba del visor» en el intake §18 |
| Capacidad gráfica tridimensional | Capacidad que el navegador debe proveer para que exista una instancia. Se declara por capacidad y no por número de versión | `Definicion-Contrato-De-Fachada.md`, CU-01, CU-06 | — |
| Movimiento automático | Movimiento que la escena ejerce sola, sin que la persona la toque. Son **dos e independientes** —órbita de la cámara y giro de las figuras—, los gobierna la fachada y ninguno altera la disposición | `Definicion-Contrato-De-Fachada.md`, CU-01, CU-02, CU-04, CU-05, CU-06, CU-07 | Capacidad **F-25** del alcance del producto. «Los dos movimientos», en su forma corta dentro de esta categoría |
| Órbita de la cámara | Movimiento automático en el que **la cámara gira sola** alrededor del conjunto y las piezas quedan quietas. Existe en el visualizador previo y se porta | `Definicion-Contrato-De-Fachada.md`, CU-01, CU-06, CU-07 | No confundir con la **cámara orbital** de la escena, que es la que responde al arrastre de la persona y existe con la órbita apagada |
| Giro de las figuras | Movimiento automático en el que **cada pieza rota sobre su eje vertical, en su lugar**, sin salir de la celda que le asignó su índice. Al apagarlo, cada pieza vuelve a su orientación de partida. Es capacidad nueva: no existe en el visualizador previo | `Definicion-Contrato-De-Fachada.md`, CU-01, CU-06, CU-07 | — |
| Estado efectivo del movimiento | Estado en que quedan los dos movimientos después de gobernarlos, que `establecerMovimiento` devuelve para que el componente anfitrión sincronice sus controles con lo que la escena hace | `Definicion-Contrato-De-Fachada.md`, CU-07 | — |

## 3. Términos con más de un referente

### 3.1 «Pieza»

Es el término polisémico verificado de esta categoría, y su colisión ya está declarada aguas arriba en `Vision-Producto.md` §9.1 y §9.2. Los dos referentes:

| Referente | Qué designa | Forma que le corresponde |
| --- | --- | --- |
| Del dominio | Cada figura del conjunto raíz del trabajo, identificada por su índice | **Forma desnuda**: «pieza». Es el único referente que usan los siete casos de uso de esta categoría, porque `seleccionarPieza` opera sobre él |
| De la composición del producto | Cada uno de los artefactos del producto que se despliegan por separado | **Siempre calificado**: «pieza pública», «pieza de datos», «piezas desplegables» |

Evidencia de la colisión y su resolución: el glosario raíz ya verificó que los dos sentidos comparten contexto de lectura dentro del producto y por eso obligó a calificar el segundo (`Vision-Producto.md` §9.2, corrección H-01). Esta categoría **no reabre la verificación ni agrega calificaciones**: adopta la resolución vigente. En los artefactos de 02 de este proyecto de código el segundo referente no aparece, salvo en esta entrada, que existe para que un subagente que lea una sección suelta sepa a qué apunta la forma desnuda.

### 3.2 «Resultado de la interpretación», precisión que no es polisemia declarada

`PRODUCT-INTAKE` §17.7 P.3 describe lo que `cargarJson` devuelve como «el resultado de la interpretación». Dentro de este proyecto de código ese valor se nombra **resultado de dibujo**, porque el resultado de la interpretación del producto es otra cosa: lo produce el backend, lleva observaciones y decide si un trabajo puede finalizarse. No es una polisemia que esta categoría declare abierta, es una precisión de nombre para evitar que se cree una: la fachada no interpreta trabajos, dibuja piezas.

### 3.3 «Órbita», precisión que tampoco es polisemia declarada

La escena de toda instancia tiene una **cámara orbital**: es la que responde al arrastre de quien mira, existe desde `inicializar` y no depende de ningún movimiento automático. La **órbita de la cámara** de §2 es otra cosa: el movimiento automático que hace girar sola esa misma cámara, prendido o apagado por la fachada. No es una polisemia abierta —los dos usos apuntan al mismo objeto, la cámara, y se distinguen por quién la mueve—, pero se declara acá porque confundirlos llevaría a leer que apagar el movimiento automático deja la escena sin cámara orbital, que es falso.

### 3.4 Verificación negativa

Se revisaron los demás términos acuñados en §2 buscando referentes múltiples dentro de esta categoría. **Ninguno verificado** además de los tratados arriba. En particular, no se califican «escena», «malla», «árbol» ni «instancia», cuyos contextos de uso son disjuntos de cualquier otro sentido presente en el corpus del producto: calificarlos sería el falso positivo que `Vocabulario-Rules.md` §9.1 declara defecto.

## 4. Términos referenciados y no redefinidos

Los siguientes términos ya están declarados en el glosario raíz del producto y **se usan con esa misma semántica**. Puntero único: `../../../00-Contexto/Vision-Producto.md` §9.

| Término | Dónde está declarado | Cómo lo usa esta categoría |
| --- | --- | --- |
| Trabajo | `Vision-Producto.md` §9.1 | Lo que el alumno entrega en el laboratorio. La fachada nunca lo guarda ni lo conoce como registro: sólo recibe su texto |
| Pieza (referente del dominio) | `Vision-Producto.md` §9.1 | Cada figura del conjunto raíz del trabajo. Ver §3.1 |
| Componente (figura plana de una pieza) | `Vision-Producto.md` §9.1 | Tapa, cara, base, lateral o lado, de donde la fachada lee las dimensiones del volumen. **No confundir con «componente anfitrión»**, que es término de §2 y designa a quien invoca la fachada |
| Observación, advertencia, error de validación | `Vision-Producto.md` §9.1 | Se nombran únicamente para declarar que **este proyecto de código no emite ninguna de las tres**: son del backend |
| Valor declarado / valor derivado | `Vision-Producto.md` §9.1 | Se nombran para declarar que la fachada no los compara ni los recalcula |
| Tapa | `Vision-Producto.md` §9.1 | Nombre de clave del que la fachada lee dimensiones, aceptando la variante del dominio del emisor |
| Rectángulo desarrollado | `Vision-Producto.md` §9.1 | Componente `Lado` del cilindro. La fachada lo usa para leer una dimensión y no lo dibuja como pieza del conjunto raíz |
| Coma final | `Vision-Producto.md` §9.1 | Particularidad del texto del alumno. Ver la nota de CU-02 §10 sobre qué escenario la ejercita |
| Fallo silencioso | `Vision-Producto.md` §9.1 | Es lo que la garantía de enumeración de piezas no dibujadas elimina |
| Laboratorio | `Vision-Producto.md` §9.1 | Nombre corriente del producto en uso |
| Actividad 1, `Describir()` | `Vision-Producto.md` §9.1 | Emisor del dato. No forma parte del producto |
| Pieza en su segundo referente | `Vision-Producto.md` §9.2 | Forma siempre calificada. Ver §3.1 |
| Capacidad (`F-XX`) | `Vision-Producto.md` §9.2 | Ítem del alcance funcional del intake. No es sinónimo de caso de uso |

**Choque de vocabulario vigente** (`Vision-Producto.md` §9.3, `PRODUCT-INTAKE` §12.1), respetado en los siete casos de uso y en el documento de concepto: «proyecto de código» designa exclusivamente una unidad de compilación, la palabra «proyecto» a secas no se usa, y las dos unidades de la Actividad 1 que emiten el dato se nombran `Ejemplo1` y `Ejemplo2`.

## 5. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Declara veinte términos acuñados por la especificación funcional de este proyecto de código, resuelve el término polisémico «pieza» adoptando la resolución del glosario raíz, precisa por qué el retorno de `cargarJson` se nombra «resultado de dibujo», declara la verificación negativa de los demás términos y lista los trece términos referenciados y no redefinidos. |
| 1.0 | 2026-08-08 | Corrección absorbida del audit `B-02-03-GeometriaFactory-Visor-r1.md`, sin subir versión por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **H-05**: la fila anterior decía «diecinueve términos» y la tabla de §2 tiene **veinte** filas de datos, contadas una a una. El conteo queda corregido a veinte. La tabla no se modifica: el error era del conteo, no de la completitud. Las tres repeticiones de la cifra en `03-UX-UI-DX/` las corrige AG-03 sobre este valor. |
| 1.0 | 2026-08-09 | Absorción de la **Fase B2** y de las dos decisiones del Product Owner. **Sin subir versión** por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **(a)** La tabla de §2 pasa de veinte a **veinticuatro** términos: entran **Movimiento automático**, **Órbita de la cámara**, **Giro de las figuras** y **Estado efectivo del movimiento**, los cuatro por la regla de inclusión de `Rules-Especificacion-Funcional.md` §3.3 —aparecen en más de un artefacto de la categoría desde que nació la capacidad F-25 y su sexta función—. **(b)** Se actualizan las definiciones que contaban la superficie pública: «Fachada» pasa a **seis** funciones planas y su cobertura a CU-01 a CU-07, «Identificador de instancia» a «las otras **cinco** funciones», «Componente anfitrión» a los **siete** casos de uso y «Página integradora» a las **seis** funciones. **(c)** Nace **§3.3**, que precisa que la **cámara orbital** de la escena —la que responde al arrastre— y la **órbita de la cámara** —el movimiento automático— no son la misma cosa, sin declararlo polisemia; la verificación negativa pasa a §3.4. **Aviso a 03-UX-UI-DX**: la cifra de términos que ese glosario repite pasa de veinte a **veinticuatro**. |
