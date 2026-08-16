# CU-12001 — Inicializar una instancia del visor sobre un elemento de dibujo

**Proyecto de código:** GeometriaFactory-Visor
**Documento:** CU-12001-Inicializar-Instancia-Del-Visor.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-08
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `NB-00006-Visualizacion-Dentro-Del-Producto.md` §1 (descripción de la necesidad) y §5 (criterios de éxito); `00-Contexto/Vision-Producto.md` §3 (diferenciador D-4) y §9 (glosario raíz); `00-Contexto/Alcance-Producto.md` §4.1 (capacidades comprometidas); `00-Contexto/Compatibilidad-Plataformas.md` §2.2 (plataforma del navegador) y §4 (alternativas para plataformas no soportadas); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §17.7 P.2 y P.3, §14 (RA-02)
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

Permitir que el componente anfitrión cree una instancia del visor sobre un elemento de dibujo que él provee, y obtenga el identificador con el que va a operar las otras cinco funciones de la fachada. Es la puerta de entrada del contrato: sin instancia viva no hay nada que cargar, seleccionar, redimensionar ni liberar.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Componente anfitrión | Primario | Provee el elemento de dibujo y las opciones de presentación, invoca `inicializar` y conserva el identificador de instancia devuelto |
| Fachada del visor | Sistema | Crea la escena sobre el elemento de dibujo, la aísla de las demás instancias y devuelve el identificador |
| Capacidad gráfica del navegador | Secundario | Provee el contexto gráfico tridimensional sobre el que se monta la escena |

## 3. Precondiciones

1. El archivo de guion de la fachada está cargado en la página y su biblioteca es alcanzable por su nombre propio.
2. El componente anfitrión dispone de un elemento de dibujo ya presente en la página, con tamaño distinto de cero.
3. El navegador provee la capacidad gráfica tridimensional declarada en `Compatibilidad-Plataformas.md`.
4. No se requiere ninguna condición de sesión, de identidad ni de conectividad: la fachada no las conoce.

## 4. Flujo principal

| Paso | Actor | Acción |
| --- | --- | --- |
| 1 | Componente anfitrión | Invoca `inicializar(elemento, opciones)` pasando el elemento de dibujo y las opciones de presentación que él decide. **Dos de esas opciones están declaradas** en `Definicion-Contrato-De-Fachada.md` §4.1 y son de gobierno del movimiento automático de la escena (§5.5 del mismo documento): el estado inicial de la **órbita de la cámara** y el estado inicial del **giro de las figuras** |
| 2 | Fachada del visor | Verifica que el elemento recibido sirve como superficie de dibujo y que tiene tamaño distinto de cero |
| 3 | Fachada del visor | Obtiene el contexto gráfico tridimensional del elemento de dibujo |
| 4 | Fachada del visor | Crea la escena con su iluminación y su cámara orbital, con los dos movimientos automáticos en el estado que las opciones declaran, y la deja sin ninguna pieza dibujada |
| 5 | Fachada del visor | Asocia la escena a un identificador de instancia nuevo y lo devuelve |
| 6 | Componente anfitrión | Conserva el identificador para las invocaciones posteriores |

## 5. Flujos alternativos

| Id | Disparador | Curso | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 · Segunda instancia sobre otro elemento de dibujo | El componente anfitrión invoca `inicializar` con un segundo elemento de dibujo mientras hay una instancia viva | Se crea una instancia independiente, con identificador propio, escena propia y selección propia. Ninguna de las dos ve el estado de la otra | Paso 5 del flujo principal, con el identificador de la segunda instancia |
| FA-02 · Reinicialización sobre el mismo elemento de dibujo | El componente anfitrión vuelve a inicializar sobre un elemento cuya instancia ya destruyó | Se crea una instancia nueva, con identificador nuevo. El identificador anterior sigue sin ser válido | Paso 5 del flujo principal |
| FA-03 · Opciones ausentes o parciales | El componente anfitrión invoca `inicializar` sin opciones, o con parte de ellas | La instancia se crea con la presentación por defecto de la fachada para lo no provisto. **Los dos movimientos automáticos arrancan apagados**: la fachada no consulta la preferencia de movimiento reducido del sistema —eso violaría G-3— y el arranque quieto es el que no sorprende. No se lee configuración propia de ninguna otra fuente | Paso 4 del flujo principal |

## 6. Excepciones y errores

| Código | Causa | Respuesta de la fachada |
| --- | --- | --- |
| `CAPACIDAD_GRAFICA_AUSENTE` | El navegador no provee la capacidad gráfica tridimensional | No se crea instancia, no se devuelve identificador y se informa el código. La combinación está declarada no soportada en `Compatibilidad-Plataformas.md`; el componente anfitrión decide qué mostrar en su lugar |
| `ELEMENTO_DE_DIBUJO_INVALIDO` | El elemento recibido no sirve como superficie de dibujo, o su tamaño es cero | No se crea instancia y se informa el código. La página queda como estaba: la fachada no crea, no mueve ni redimensiona elementos de la página |

Las dos excepciones terminan de forma controlada: no dejan escena a medio crear ni contexto gráfico tomado sin dueño (garantía G-7 del contrato de fachada).

## 7. Postcondiciones

- **Éxito:** existe una instancia viva asociada al elemento de dibujo, con escena, iluminación y cámara orbital, sin ninguna pieza dibujada; el componente anfitrión tiene un identificador válido; no hubo ninguna petición de red y no se escribió nada en el almacenamiento del navegador.
- **Fallo:** no existe instancia, no hay identificador válido, no se tomó contexto gráfico y el estado de la página es idéntico al previo a la invocación.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Una página con el archivo de guion cargado y un elemento de dibujo de 800 × 600 en un navegador con capacidad gráfica tridimensional | El componente anfitrión invoca `inicializar(elemento, opciones)` | La fachada devuelve un identificador de instancia y la escena queda creada con 0 piezas dibujadas |
| CA-02 | La misma página, con la instancia ya creada por CA-01 | El componente anfitrión invoca `inicializar` sobre un segundo elemento de dibujo de 400 × 300 | La fachada devuelve un segundo identificador distinto del primero, y las dos instancias quedan vivas y aisladas: seleccionar una pieza en una no altera la otra |
| CA-03 | Una página cuyo navegador no provee la capacidad gráfica tridimensional | El componente anfitrión invoca `inicializar(elemento, opciones)` | La fachada no devuelve identificador, informa `CAPACIDAD_GRAFICA_AUSENTE` y no deja escena creada |
| CA-04 | Una página con un elemento de dibujo de 0 × 0 | El componente anfitrión invoca `inicializar(elemento, opciones)` | La fachada no devuelve identificador e informa `ELEMENTO_DE_DIBUJO_INVALIDO` |
| CA-05 | Una página con el archivo de guion cargado y la pestaña de red abierta y vacía | El componente anfitrión invoca `inicializar(elemento, opciones)` | La pestaña de red registra exactamente 0 peticiones originadas por la fachada, y el almacenamiento del navegador queda sin ninguna clave nueva |
| CA-06 | Una página con un elemento de dibujo válido y opciones que declaran la órbita de la cámara prendida y el giro de las figuras apagado | El componente anfitrión invoca `inicializar(elemento, opciones)` y luego `cargarJson` con el texto del escenario E-1 | La cámara gira sola alrededor del conjunto, las tres piezas quedan quietas, y la disposición es la misma que con los dos movimientos apagados, comparable pieza por pieza |
| CA-07 | La misma página, invocando `inicializar` **sin** la parte de opciones que gobierna el movimiento | El componente anfitrión invoca `inicializar(elemento, opciones)` | Los dos movimientos quedan apagados: la escena sólo se mueve por acción de la persona, y el almacenamiento del navegador sigue sin ninguna clave nueva |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-00006 |
| Reglas de negocio aplicables | Ninguna. Este proyecto de código no declara RN (ver `README.md` de la sección) |
| Historias de usuario a generar | US de creación de la instancia del visor y de su aislamiento entre instancias, en 06-Backlog-Tecnico |
| Componentes esperados | Fachada plana y servicio de dibujo (capas 2 y 3 de PRODUCT-INTAKE §17.7 P.2); la composición concreta la fija 05-Arquitectura-Tecnica |
| Tests previstos | 08-Calidad-Y-Pruebas: creación de instancia, aislamiento entre dos instancias, ausencia de capacidad gráfica, elemento de tamaño nulo y conteo de peticiones de red en 0 |
| Concepto central | `Definicion-Contrato-De-Fachada.md` §4.1, §5.1 y §6 |

## 10. Notas y supuestos

- El actor primario es un **componente que embebe el archivo de guion**, no una persona. La fachada no sabe quién está del otro lado ni qué papel cumple, por prohibición explícita de PRODUCT-INTAKE §17.7 P.5.
- Las opciones de presentación son un dato de entrada del componente anfitrión. La fachada no lee configuración propia de ninguna fuente (garantía G-3).
- El motor de dibujo tridimensional concreto y su versión no se deciden acá: son decisión de 05-Arquitectura-Tecnica. Este caso de uso está redactado para que cambiarlos no lo altere.
- Las dos opciones de gobierno del movimiento fijan el estado **con el que la instancia nace**. Cambiarlo después, con la instancia viva, no se hace reinicializando: es la sexta función de la fachada, `establecerMovimiento`, con su contrato de uso en `CU-12007`.
- La política de compatibilidad de la superficie pública vive en `Definicion-Contrato-De-Fachada.md` §7.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Contrato de uso de `inicializar`, con tres flujos alternativos, dos condiciones de error y cinco criterios de aceptación con valores concretos. |
| 1.0 | 2026-08-08 | Corrección absorbida del audit `B-02-03-GeometriaFactory-Visor-r1.md`, sin subir versión por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **H-10**: la cabecera sustituye la referencia a `Compatibilidad-Plataformas.md` sin sección por §2.2 y §4, y completa con su sección concreta las referencias a `NB-00006`, `Vision-Producto.md` y `Alcance-Producto.md`. |
| 1.0 | 2026-08-09 | Retroalimentación de la Fase B2 de validación de maqueta del proyecto de código `GeometriaFactory-Web`, dentro de la cual se validó la fachada de este proyecto de código. **Sin subir versión** por `Master-Prompt.md` §5, que lo admite mientras el documento está en estado `Propuesto`. **Capacidad F-25, movimiento automático de la escena**: §4 paso 1 declara las **dos opciones de gobierno** que `inicializar` recibe y el paso 4 las ejerce al crear la escena; **FA-03** precisa que con opciones ausentes o parciales los dos movimientos arrancan apagados, porque consultar la preferencia de movimiento reducido del sistema violaría la garantía G-3; y nacen **CA-06** y **CA-07**, que verifican el gobierno y el arranque apagado sin romper el determinismo de la disposición ni la garantía G-2. Ningún código de condición nuevo. Concuerda con `Definicion-Contrato-De-Fachada.md` §4.1 y §5.5. |
| 1.0 | 2026-08-09 | Segunda absorción de la **Fase B2**, por la decisión del Product Owner de agregar una **sexta función** a la fachada. **Sin subir versión** por `Master-Prompt.md` §5 (documento en estado `Propuesto`). §1 pasa a decir «las otras **cinco** funciones» y §10 suma la nota que declara que estas opciones fijan el estado de nacimiento del movimiento y que cambiarlo con la instancia viva es de `establecerMovimiento` (`CU-12007`), no de una reinicialización. Este caso de uso **no se amplía** para cubrir el cambio en vivo: su precondición es la creación de una instancia y la del cambio es una instancia ya viva. |
