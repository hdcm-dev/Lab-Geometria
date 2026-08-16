# Representación — Lista de observaciones

**Proyecto de código:** GeometriaFactory-Web
**Documento:** Representacion-Lista-De-Observaciones.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-09
**Autor:** UX/UI Designer + Frontend Lead (AG-03)
**Variante:** UX/UI
**Trazabilidad upstream:** `../02-Especificacion-Funcional/Casos-De-Uso/CU-10007-Abrir-Un-Trabajo-Y-Explorarlo-En-Escena-Y-Arbol.md` §4 pasos 10 y 11, FA-03, FA-04, §6, CA-01, CA-04 y CA-09; `../02-Especificacion-Funcional/Casos-De-Uso/CU-10005-Enviar-Un-Trabajo-Y-Ver-El-Resultado-De-La-Interpretacion.md` §4 paso 8, FA-02, FA-03, §6, CA-03 y CA-04; `../02-Especificacion-Funcional/Glosario-Funcional.md` §4; `../../../00-Contexto/Vision-Producto.md` §9.1, entradas «observación», «advertencia», «error de validación», «comentario» y «valor declarado / valor derivado»; `../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00005-Visibilidad-Del-Error-De-Calculo.md` §5 (segundo y tercer criterio); `NB-00004` §5 (sexto criterio); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4.1 (RN-10005, RN-10009), §7 (CL-4), §20.E-1 y §20.E-2; `Design-Rules-Web-Generico.md` §2.1, §5, §7
**Trazabilidad downstream:** la **Fase B2** de validación visual de maqueta, que la materializa como componente reutilizado del inventario identificado y cuyo contrato de datos de maqueta exhibe el par declarado/derivado; `05-Arquitectura-Tecnica`, componente de lista de observaciones; `06-Backlog-Tecnico`, **`US-10013`, `US-10014`** heredadas de `Wireframes-Envio-De-Trabajo.md` y **`US-10019`, `US-10020`** de `Wireframes-Vista-De-Trabajo.md`; `08-Calidad-Y-Pruebas`, con cuatro escenarios propios: **el texto semilla de tres piezas que produce dos advertencias** con 36.00 contra 54.00 y 343.00 contra 1029.00, **el tipo desconocido que produce un error de validación con índice de figura y campo** `Tipo`, **el anuncio del par declarado/derivado con su rótulo** por lector de pantalla, y **la revisión de las dos severidades en escala de grises**

---

## Tabla de contenido

- [1. Concepto representado y propósito](#1-concepto-representado-y-propósito)
- [2. Apariencia esquemática](#2-apariencia-esquemática)
- [3. Variantes](#3-variantes)
- [4. Datos que consume](#4-datos-que-consume)
- [5. Restricciones de accesibilidad](#5-restricciones-de-accesibilidad)
- [6. Reutilización](#6-reutilización)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Concepto representado y propósito

La **lista de observaciones** presenta lo que el producto emite al interpretar el texto del alumno. Una observación tiene dos especies con efectos distintos: la **advertencia**, que no impide que el trabajo pase a estado `Pendiente`, y el **error de validación**, que sí lo impide.

Se centraliza acá porque aparece en **dos superficies** —el resultado del envío y la vista de trabajo— y porque es donde vive el mayor valor didáctico del producto: **la advertencia que muestra que el cubo declara 36.00 donde la geometría da 54.00**. Que esa comparación se lea bien es el propósito de esta representación, y no un detalle de presentación.

Tres cosas que esta representación tiene que mantener separadas, porque mezclarlas cambia lo que el producto le dice al alumno:

| Qué es | Quién lo emite | Dónde vive | Qué pide |
| --- | --- | --- | --- |
| **Observación** | El producto, al interpretar el texto | **Esta lista** | La advertencia informa; el error de validación pide corregir el programa y reenviar |
| **Condición del dibujo** | La fachada del visualizador, al no poder dibujar una pieza | **Junto a la escena, nunca acá** | Nada. **No es un error del trabajo** |
| **Comentario del administrador** | Una persona | **Bloque propio, separado** | Nada. **No es una observación y no es una calificación** |

## 2. Apariencia esquemática

Lista con las dos especies:

```text
OBSERVACIONES (3)

  [error]        figura 3 · campo Tipo
                 El tipo declarado no se pudo interpretar.

  [advertencia]  figura 1 · campo Area
                 declarado   36.00
                 derivado    54.00

  [advertencia]  figura 2 · campo Volumen
                 declarado   343.00
                 derivado    1029.00
```

**El esquema compone tres observaciones sobre tres figuras distintas, y eso no es casual.** Una figura cuyo tipo el producto declara no haber podido interpretar **no puede además tener un valor derivado**: sin tipo no hay geometría de la cual derivarlo. Un esquema que pusiera el error de tipo y la advertencia de área sobre la misma figura enseñaría a construir una lista que el producto no puede emitir.

**Los valores `declarado` y `derivado` se escriben con punto, también en el esquema.** Es la excepción declarada de §4 a la convención de coma decimal del producto: se muestran tal como llegan, sin reformatear. El esquema los dibuja así a propósito, porque es el ejemplo canónico que se copia.

**El par de valores se dispone en dos líneas alineadas y no en prosa.** Es la comparación que el alumno tiene que poder hacer de un vistazo, y una frase corrida la esconde. Los dos valores usan números tabulares para que las cifras alineen verticalmente.

Estado sin observaciones, que **no** es un hueco:

```text
OBSERVACIONES

  Sin observaciones.
```

Bloque de comentario, que se dibuja aparte y que esta representación **no** contiene, y que se muestra acá sólo para fijar la diferencia visual:

```text
COMENTARIO DEL DOCENTE

  Revisá el área del cubo.
```

## 3. Variantes

| Variante | Condición de uso | Diferencias esperadas |
| --- | --- | --- |
| **Advertencia con par de valores** | Discrepancia entre el valor declarado y el derivado | Distintivo de atención, índice de figura, campo señalado y **los dos valores en dos líneas alineadas**. Tono informativo: **no pide corregir nada** |
| **Advertencia sin par de valores** | Discrepancia que el contrato reporta sin los dos valores | Igual, sin el bloque de valores. El bloque **no se dibuja vacío** |
| **Error de validación** | Defecto que impide interpretar el texto como figuras | Distintivo de peligro, índice de figura, campo señalado y qué se esperaba. **Nunca un texto genérico** |
| **Sin observaciones** | La colección llegó con cero elementos, no ausente | Línea explícita «Sin observaciones». **Nunca un bloque en blanco ni una sección ausente** |
| **En el resultado del envío** | Tras enviar | Precedida por el estado alcanzado. Cuando el estado es `Pendiente` con advertencias, lleva una línea que declara que **no bloquean nada** |
| **En la vista de trabajo** | Al abrir un trabajo | **Sin filtrar por severidad.** Se muestran todas, del estado que sea el trabajo |

**Regla que gobierna todas las variantes: la lista no se filtra por severidad.** Ni en la vista de trabajo ni en el resultado del envío. Ocultar las advertencias cuando hay errores, o al revés, escondería exactamente lo que el producto viene a mostrar.

**Regla de tono: una advertencia no reprocha.** Es un dato que el alumno mira, con los dos números a la vista, y el texto no sugiere que haya que corregir para avanzar, porque no hay que hacerlo. Un trabajo con advertencias se entrega igual, y que después se apruebe o se rechace es una decisión del administrador y no del validador.

## 4. Datos que consume

| Dato | Origen | Nota |
| --- | --- | --- |
| Severidad | Colección de observaciones del detalle o del resultado del envío | Determina la variante. **Se muestra escrita**, no sólo por color |
| Índice de figura | Ídem | **Obligatorio en toda observación.** Es el mismo índice con el que la pieza figura en el resultado de dibujo |
| Campo señalado | Ídem | Ídem. Nunca se sustituye por un texto genérico |
| Valor declarado | Ídem, sólo en advertencias | **Se muestra exactamente como el texto del alumno lo trae**, sin reformatear |
| Valor derivado | Ídem, sólo en advertencias | **Se muestra exactamente como el sistema lo recalcula**, sin reformatear |
| Cantidad de observaciones | Recuento de la colección | Encabeza la lista |

**Sobre el formato de los números.** La convención del producto es coma decimal. **Los valores declarado y derivado son la excepción declarada**: se muestran tal como llegan, sin reformatear. Reescribirlos rompería la comparación —el alumno tiene que reconocer el número que su propio programa emitió— y contradiría que el texto original se conserva íntegro.

**Lo que esta representación no consume:** el texto original completo, las condiciones del dibujo y el comentario del administrador.

## 5. Restricciones de accesibilidad

- **La severidad se comunica por texto y por color, nunca sólo por color.** Cada entrada lleva su distintivo escrito, y la representación se revisa en escala de grises antes de darse por aprobada.
- **El par de valores se anuncia con su rótulo**: «declarado 36 punto 00, derivado 54 punto 00», no dos números sueltos. Sin el rótulo, la comparación se pierde por completo para quien no la ve.
- La lista se marca como lista, con su recuento en el encabezado, para que quien la recorre sepa cuántas entradas tiene antes de entrar.
- Cada entrada se anuncia completa —severidad, figura, campo y valores— como una unidad, y no como cuatro fragmentos sueltos.
- El bloque de comentario tiene **encabezado propio** y no se distingue de la lista sólo por posición: quien recorre la página linealmente tiene que enterarse de que cambió de clase de contenido.
- El estado «Sin observaciones» es texto y forma parte del recorrido; **no es una sección ausente**, que sería indistinguible de un fallo de carga.

## 6. Reutilización

| Artefacto que la invoca | Cómo |
| --- | --- |
| [`Wireframes-Vista-De-Trabajo.md`](Wireframes-Vista-De-Trabajo.md) | Lista completa en la columna izquierda, sin filtrar por severidad, con el bloque de comentario **aparte** |
| [`Wireframes-Envio-De-Trabajo.md`](Wireframes-Envio-De-Trabajo.md) | Lista completa dentro del bloque de resultado, precedida por el estado alcanzado |
| [`Experiencia-De-Uso.md`](Experiencia-De-Uso.md) §8.1 | La taxonomía de cinco clases de error, de la que esta representación materializa la tercera |
| [`Glosario-UX.md`](Glosario-UX.md) | Los términos de superficie que esta representación acuña |

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. Centraliza la lista de observaciones usada en las dos superficies que la muestran, con la tabla que separa observación, condición del dibujo y comentario del administrador, el par de valores dispuesto en dos líneas alineadas y sin reformatear como excepción declarada de la convención numérica, la regla de no filtrar por severidad, la regla de tono de que una advertencia no reprocha, y las restricciones de accesibilidad que preservan la comparación para quien no la ve. |
| 1.0 | 2026-08-09 | Correcciones absorbidas del audit `B-02-03-GeometriaFactory-Web-r1.md` (ronda 1), **sin subir versión** por `Master-Prompt.md` §5, que lo admite mientras el documento está en estado `Propuesto`. **H-03**: la cabecera completa su `Trazabilidad downstream` con las `US-XX` heredadas de sus dos wireframes invocantes y con cuatro escenarios de prueba propios. **H-06**: las `NB-XX` de la cabecera pasan a citarse con sección y criterio numerado, con la forma que ya usan los casos de uso de la categoría 02. |
| 1.0 | 2026-08-09 | Retroalimentación de la Fase B2 de validación de maqueta del proyecto de código `GeometriaFactory-Web`, **sin subir versión** por `Master-Prompt.md` §5, que lo admite mientras el documento está en estado `Propuesto`. **H-02** (coma contra punto): §1, §2 y §5 escriben `declarado` y `derivado` **con punto** —incluidas la trazabilidad downstream de la cabecera y la locución del lector de pantalla, que pasa a «36 punto 00»—, y §2 suma la nota que declara el punto como deliberado. Este documento es el ejemplo canónico que las dos superficies copian, de modo que su esquema enseñaba lo contrario de la regla que su propio §4 enuncia. **H-04** (recuentos sin dato declarado): el error de tipo del esquema de §2 pasa de `figura 1` a `figura 3`, para que las tres observaciones caigan sobre tres figuras distintas: una figura sin tipo interpretable no puede tener valor derivado, y el esquema componía una lista que el producto no puede emitir. |
