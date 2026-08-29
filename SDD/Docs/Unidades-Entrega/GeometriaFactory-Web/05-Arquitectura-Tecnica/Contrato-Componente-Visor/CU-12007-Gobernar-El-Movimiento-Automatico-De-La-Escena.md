# CU-12007 — Gobernar el movimiento automático de la escena sobre una instancia viva

**Unidad de entrega:** GeometriaFactory-Web
**Documento:** CU-12007-Gobernar-El-Movimiento-Automatico-De-La-Escena.md
**Versión:** 1.2
**Estado:** Aprobado
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `NB-00006-Visualizacion-Dentro-Del-Producto.md` §1 (descripción de la necesidad) y §5 (criterios de éxito); `00-Contexto/Vision-Producto.md` §3 (diferenciador D-4) y §9 (glosario raíz); `00-Contexto/Alcance-Producto.md` §4.1 (capacidades comprometidas); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 capacidad **F-25** (movimiento automático de la escena con dos controles independientes), §17.7 P.3 (contrato de la fachada), §17.7 P.10 (el movimiento automático no altera la disposición), §17.7 P.4 y P.5 (prohibiciones de persistencia y de identidad), §14 (RA-02)
**Trazabilidad downstream:** 03-UX-UI-DX, 05-Arquitectura-Tecnica, 06-Backlog-Tecnico, 08-Calidad-Y-Pruebas, 10-Examples

---

## Tabla de contenido

- [1. Propósito](#1-propósito)
- [2. Actores](#2-actores)
- [3. Precondiciones](#3-precondiciones)
- [4. Flujo principal](#4-flujo-principal)
- [5. Flujos alternativos](#5-flujos-alternativos)
- [6. Excepciones y errores](#6-excepciones-y-errores)
- [7. Postcondiciones](#7-postcondiciones)
- [8. Criterios de aceptación](#8-criterios-de-aceptación)
- [9. Trazabilidad](#9-trazabilidad)
- [10. Notas y supuestos](#10-notas-y-supuestos)
- [11. Control de cambios](#11-control-de-cambios)

---

## 1. Propósito

Permitir que el componente anfitrión prenda o apague, **por separado y con la instancia ya andando**, los dos movimientos automáticos de la escena —la órbita de la cámara y el giro de las figuras—, sin reconstruir la instancia, sin volver a leer el texto del trabajo y sin perder la selección vigente. Es el contrato de uso de la sexta función de la fachada, `establecerMovimiento`, y la única vía por la que esos dos movimientos se gobiernan después de `inicializar`.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Componente anfitrión | Primario | Dibuja los controles con los que quien mira prende y apaga cada movimiento, consulta por su cuenta la preferencia de movimiento reducido del sistema, conserva la elección e invoca `establecerMovimiento` con el estado deseado |
| Fachada del visor | Sistema | Prende o apaga cada movimiento sobre la escena viva, repone la orientación de partida de las piezas al apagar el giro y devuelve el estado efectivo de los dos movimientos |

## 3. Precondiciones

1. Existe una instancia viva y el componente anfitrión tiene su identificador (`CU-12001`).
2. El componente anfitrión ya decidió, por sus propios medios, cuál de los dos movimientos quiere prender o apagar. **La fachada no participa de esa decisión**: no dibuja controles y no consulta la preferencia de movimiento reducido del sistema, porque leerla sería leer configuración propia y violaría la garantía G-3.
3. **No se requiere que haya un texto cargado**: la función opera igual sobre una instancia viva y vacía.
4. No se requiere ninguna condición de sesión, de identidad ni de conectividad: la fachada no las conoce.

## 4. Flujo principal

| Paso | Actor | Acción |
| --- | --- | --- |
| 1 | Componente anfitrión | Invoca `establecerMovimiento(id, opciones)` con el identificador de una instancia viva y el estado deseado de uno de los dos movimientos, o de los dos |
| 2 | Fachada del visor | Verifica que el identificador corresponde a una instancia viva |
| 3 | Fachada del visor | Prende o apaga cada movimiento **nombrado** en las opciones. El movimiento **no nombrado conserva el estado que tenía**: son dos gobiernos independientes (`Definicion-Contrato-De-Fachada.md` §5.5 regla 1) |
| 4 | Fachada del visor | Si la operación apaga el giro de las figuras, **repone la orientación de partida de cada pieza**, de modo que la escena quede igual para cualquiera que apague el giro (§5.5 regla 5) |
| 5 | Fachada del visor | Deja intactos la disposición derivada del índice, la selección vigente, el encuadre, el resultado de dibujo vigente y el identificador de instancia: **no reconstruye la escena, no la recarga y no vuelve a leer el texto** |
| 6 | Fachada del visor | Devuelve el estado efectivo de los dos movimientos, para que el componente anfitrión sincronice sus controles con lo que la escena está haciendo |

## 5. Flujos alternativos

| Id | Disparador | Curso | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 · Opciones parciales | El componente anfitrión nombra un solo movimiento | Se aplica el nombrado y el otro conserva su estado. **No hay estado por defecto acá**: el arranque apagado ante opciones ausentes es de `inicializar` (`CU-12001` FA-03) y no de esta función, que opera sobre una escena que ya tiene un estado | Paso 5 del flujo principal |
| FA-02 · Fijar el estado que ya estaba | Se pide prender un movimiento ya prendido, o apagar uno ya apagado | La operación es **idempotente**: la escena no cambia y se devuelve el mismo estado efectivo. Apagar un giro ya apagado no reposiciona nada, porque las piezas ya están en su orientación de partida | Paso 6 del flujo principal |
| FA-03 · Instancia viva sin texto cargado | Se invoca sobre una instancia que todavía no cargó ningún texto | El estado queda fijado igual, sobre la escena vacía. **La carga posterior de un texto no altera el estado de los movimientos**: `cargarJson` reemplaza el contenido dibujado, no el gobierno de la escena | Paso 6 del flujo principal |
| FA-04 · La persona arrastra la cámara con un movimiento prendido | Quien mira toma el control de la cámara mientras la órbita o el giro están prendidos | Los dos movimientos quedan suspendidos mientras dura el arrastre y retoman al soltar (§5.5 regla 6). **El estado gobernado no cambia**: la suspensión es del efecto, no del gobierno, y esta función sigue devolviendo prendido lo que está prendido | Paso 6 del flujo principal |
| FA-05 · Superficie de dibujo no visible | La escena queda fuera de vista mientras hay un movimiento prendido | El movimiento deja de consumir recursos mientras no se ve y retoma cuando vuelve a verse (§5.5 regla 6), sin que el estado gobernado cambie | Paso 6 del flujo principal |

## 6. Excepciones y errores

| Código | Causa | Respuesta de la fachada |
| --- | --- | --- |
| `UNKNOWN_INSTANCE` | El identificador no corresponde a ninguna instancia viva, o corresponde a una ya liberada | Ninguna instancia cambia y se informa el código. Es la condición esperable cuando el anfitrión mueve su control después de haber destruido la instancia |

**Esta función no emite ninguna condición nueva.** La lista de `Definicion-Contrato-De-Fachada.md` §6 sigue cerrada en **siete** códigos: un movimiento que no arranca porque la instancia no existe es `UNKNOWN_INSTANCE` y nada más. Un estado de movimiento que no se puede satisfacer no es una condición concebible del contrato, porque prender y apagar no admite fallo parcial: o la instancia existe y el estado queda fijado, o la instancia no existe (garantía G-7).

## 7. Postcondiciones

- **Éxito:** cada movimiento nombrado quedó en el estado pedido y el no nombrado en el que tenía; si se apagó el giro, cada pieza está en su orientación de partida; la disposición, la selección vigente, el encuadre, el resultado de dibujo y el identificador de instancia son exactamente los de antes de la invocación; hubo 0 peticiones de red y ninguna clave escrita en el almacenamiento del navegador.
- **Fallo:** ninguna instancia cambió de estado; ni la escena, ni la selección, ni los movimientos en curso se alteraron.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Una instancia viva con el texto del escenario E-1 cargado, sus 3 piezas dibujadas, la pieza de índice 2 resaltada y los dos movimientos apagados | El componente anfitrión invoca `establecerMovimiento(id, opciones)` prendiendo la órbita de la cámara y el giro de las figuras | Los dos movimientos corren; la pieza de índice 2 **sigue siendo la única resaltada**; la disposición de las 3 piezas es idéntica pieza por pieza a la previa; el identificador de instancia sigue siendo el mismo y sigue siendo válido |
| CA-02 | La misma instancia, con el giro de las figuras prendido desde hace un tiempo y las piezas en una orientación cualquiera | Dos personas apagan el giro en momentos distintos, cada una invocando `establecerMovimiento(id, opciones)` | Las dos escenas quedan **iguales**: cada pieza vuelve a su orientación de partida, y la disposición no se movió |
| CA-03 | Una instancia viva con la órbita apagada y el giro prendido | El componente anfitrión invoca `establecerMovimiento(id, opciones)` nombrando **sólo** la órbita, para prenderla | La órbita queda prendida y el giro **sigue prendido**: el movimiento no nombrado conserva su estado, y el estado efectivo devuelto declara los dos |
| CA-04 | Una instancia ya liberada con `destruir` | El componente anfitrión invoca `establecerMovimiento(id, opciones)` con ese identificador | La fachada informa `UNKNOWN_INSTANCE`, ninguna otra instancia viva se altera y **no aparece ningún código de condición fuera de los siete declarados** |
| CA-05 | Una instancia viva con el texto del escenario E-7 cargado y la pestaña de red abierta y vacía | El componente anfitrión prende los dos movimientos con `establecerMovimiento(id, opciones)` y deja la escena moviéndose durante 60 segundos | La pestaña de red registra exactamente **0 peticiones** originadas por la fachada durante todo el movimiento, y el almacenamiento del navegador queda sin ninguna clave nueva |
| CA-06 | Una instancia viva con el texto del escenario E-7 cargado, 6 piezas dibujadas y el resultado de dibujo vigente en manos del anfitrión | El componente anfitrión prende y apaga los dos movimientos cinco veces seguidas | En ninguna de las diez invocaciones se vuelve a leer el texto ni se recrea el contexto gráfico: el resultado de dibujo vigente es el mismo, las 6 piezas conservan sus índices y su disposición, y no hubo parpadeo de reconstrucción |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-00006, en su parte de que la previsualización se mire con comodidad dentro del producto, prevista allá como **CU-10028**. La capacidad de origen es **F-25** del intake §4, **`Must Have`** desde el intake 1.7 |
| Reglas de negocio aplicables | Ninguna. Este proyecto de código no declara RN (ver `README.md` de la sección) |
| Historias de usuario a generar | US de gobierno en vivo de los dos movimientos automáticos, sin reconstrucción de la instancia y sin pérdida de la selección, en 06-Backlog-Tecnico |
| Componentes esperados | Fachada plana y servicio de dibujo, en su parte de bucle de dibujo y de orientación de las mallas (capas 2 y 3 de PRODUCT-INTAKE §17.7 P.2); 05-Arquitectura-Tecnica fija la composición |
| Tests previstos | 08-Calidad-Y-Pruebas: gobierno independiente de los dos movimientos, conservación de la selección y de la disposición, reposición de la orientación de partida al apagar el giro, idempotencia, identificador liberado, y conteo de peticiones en 0 **con los dos movimientos prendidos** |
| Concepto central | `Definicion-Contrato-De-Fachada.md` §4.6, §5.5 y §6 |

## 10. Notas y supuestos

- Este caso de uso existe porque el criterio de recorte de la categoría es **una función de la fachada, un caso de uso** (`Especificacion-Funcional.md` §3.1 punto 1): `establecerMovimiento` tiene su propia precondición —una instancia viva cuyo estado de movimiento se quiere cambiar—, su propio flujo y su propia condición de error, y no se dispara desde ninguno de los otros seis.
- La sexta función **no reemplaza** las dos opciones de gobierno que `inicializar` recibe (`CU-12001`): esas fijan el estado con el que la instancia nace, y ésta lo cambia después. Las dos vías gobiernan lo mismo y no se contradicen.
- La vía que esta función deja atrás era reconstruir la instancia —`destruir`, `inicializar` con las opciones nuevas y `cargarJson` con el mismo texto—. Era correcta en cuanto a disposición, por la garantía G-6, pero **perdía la selección vigente** y producía un parpadeo de reconstrucción para un cambio que no lo necesita. El Product Owner decidió el 2026-08-09 agregar la sexta función.
- El determinismo del contrato **no se toca acá**: la posición de cada pieza sigue derivada de su índice y esta función no la roza (G-6). Lo que el giro cambia es la orientación en un instante, que nunca fue parte de lo comprometido.
- La preferencia de quien mira **no vive en la fachada**. Quién dibuja el control, quién consulta la preferencia de movimiento reducido del sistema y quién conserva la elección entre páginas es decisión del componente anfitrión y de 03-UX-UI-DX. Guardarla en la fachada violaría la garantía G-2.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial, originada en la **Fase B2** de validación de maqueta del proyecto de código `GeometriaFactory-Web` y en la decisión del Product Owner del 2026-08-09 de agregar una **sexta función** a la fachada. Contrato de uso de `establecerMovimiento(id, opciones)`, con cinco flujos alternativos, una sola condición de error —`INSTANCIA_DESCONOCIDA`, ya declarada— y seis criterios de aceptación. Resuelve el punto abierto que `Definicion-Contrato-De-Fachada.md` §5.5 había elevado sobre el cambio de movimiento con la instancia viva. |
| 1.1 | 2026-08-09 | **Cierra la parte del hallazgo `F26-11`** que alcanza a este caso de uso, del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0, contra `PRODUCT-INTAKE` **1.9**. **§9** declaraba la capacidad de origen **F-25** como `Should Have`, y el Product Owner la subió a **`Must Have`** en el intake 1.7, con la constancia escrita en la propia celda de §4 de la fuente; la fila registra además que la necesidad de negocio le prevé caso de uso propio a nivel producto, **CU-10028** de `NB-00006` §7. Ningún flujo, condición de error ni criterio de aceptación cambia. |
| 1.2 | 2026-08-29 | **Tramo `R-3c` del renombre `F-03`**, reactivado por el Product Owner el 2026-08-29 y registrado en [`../../../../Producto/Norma-De-Nomenclatura.md`](../../../../Producto/Norma-De-Nomenclatura.md) §8. **3 línea(s)** pasan los códigos de condición de la forma castellana a la vigente, con el mapeo de **§6.8** —101 pares— y **sin elegir ninguno acá**. Se respeta **§4.1**: no se tocan las filas de control de cambios, ni lo que está entre «…», ni los informes de `Audit/`. **Ninguna palabra de prosa cambia**, verificado con el control de diff del tramo. |
