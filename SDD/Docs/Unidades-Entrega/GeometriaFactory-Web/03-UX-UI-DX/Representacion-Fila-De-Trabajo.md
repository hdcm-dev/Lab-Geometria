# Representación — Fila de trabajo

**Unidad de entrega:** GeometriaFactory-Web
**Documento:** Representacion-Fila-De-Trabajo.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-09
**Autor:** UX/UI Designer + Frontend Lead (AG-03)
**Variante:** UX/UI
**Trazabilidad upstream:** `../02-Especificacion-Funcional/Casos-De-Uso/CU-10006-Consultar-El-Listado-Propio-Y-Operar-Sobre-El-Borrador.md` §4 pasos 3 y 4, FA-03, CA-01 a CA-03; `../02-Especificacion-Funcional/Casos-De-Uso/CU-10008-Recorrer-La-Entrega-De-La-Comision.md` §4 paso 3, CA-01 y CA-05; `../02-Especificacion-Funcional/Casos-De-Uso/CU-10009-Resolver-Un-Trabajo-Con-Comentario-Opcional.md` §10; `../../../00-Contexto/Vision-Producto.md` §9.1, entrada «estado del trabajo»; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4.2 (modelo de estados y tabla de quién puede qué), §4.1 (RN-10004, RN-10010, RN-10011); `Design-Rules-Web-Generico.md` §4.3, §4.8, §5, §7
**Trazabilidad downstream:** la **Fase B2** de validación visual de maqueta, que la materializa como componente reutilizado del inventario identificado; `05-Arquitectura-Tecnica`, componente de fila de listado con sus acciones por estado; `06-Backlog-Tecnico`, **`US-10015`, `US-10016`, `US-10017`** heredadas de `Wireframes-Panel-De-Trabajos-Del-Alumno.md` y **`US-10022`, `US-10023`** de `Wireframes-Listado-De-La-Comision.md`; `08-Calidad-Y-Pruebas`, con tres escenarios propios: **recuento de acciones ofrecidas por estado** —tres en `Borrador`, una en los otros tres—, **cuatro estados distinguibles entre sí en un mismo listado**, y **revisión de las cuatro insignias en escala de grises**, más el reflujo de fila a tarjeta apilada por debajo del punto de quiebre

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

La **fila de trabajo** es la representación de un trabajo dentro de una colección: su nombre, su fecha, su estado y sus dos recuentos, más las acciones que ese estado admite para quien mira.

Se centraliza acá porque aparece en **tres superficies** y porque concentra la regla que el diseño tiene que hacer obvia sin que nadie lea nada: **qué se puede hacer con un trabajo depende de su estado y del papel de quien mira**. Si esa regla se redibujara en cada superficie, divergiría, y la divergencia se leería como un permiso.

Su parte más pequeña, la **insignia de estado**, se usa además en dos superficies de detalle, y por eso se declara acá con su tabla completa en lugar de repetirse.

## 2. Apariencia esquemática

Fila completa, versión ancha:

```text
| Cubo y ortoedro       12/08/2026   [Pendiente]    3 piezas   2 adv.   [abrir] |
   ^ nombre             ^ fecha      ^ insignia     ^ recuentos          ^ acciones
     type.body-strong     caption      pill+texto     caption             por estado
```

Insignia de estado, sus cuatro valores. El texto está **siempre** presente; el color es refuerzo y nunca el único canal:

```text
[Borrador]      neutro       · el texto todavía no verificó, o recién se creó
[Pendiente]     atención     · entregado, a la espera de revisión
[Finalizado]    éxito        · aprobado. Terminal
[Rechazado]     peligro      · rechazado. Terminal
```

Versión angosta: la fila se convierte en tarjeta apilada.

```text
+--------------------------------------------------+
|  Cubo y ortoedro                    [Pendiente]  |
|  12/08/2026 · 3 piezas · 2 advertencias          |
|  [ Abrir ]                                       |
+--------------------------------------------------+
```

## 3. Variantes

| Variante | Condición de uso | Diferencias esperadas |
| --- | --- | --- |
| **Propia, en estado `Borrador`** | Listado del alumno | Tres acciones: abrir, volver sobre él, eliminar. Insignia neutra |
| **Propia, en estado `Pendiente`** | Listado del alumno | **Sólo abrir.** Ni editar ni eliminar |
| **Propia, terminal** | Listado del alumno, estados `Finalizado` y `Rechazado` | **Sólo abrir.** La única salida ante un rechazo es cargar un trabajo nuevo |
| **De la comisión** | Listado del administrador | **Sólo abrir**, más el nombre del alumno dueño, que aporta la cabecera del grupo. **Nunca en estado `Borrador`** |
| **Insignia suelta** | Cabecera de la vista de trabajo y bloque de resultado del envío | Sólo la insignia, sin nombre, fecha, recuentos ni acciones |
| **Tarjeta apilada** | Por debajo del punto de quiebre principal, en las dos superficies de listado | Nombre e insignia en la primera línea; fecha y recuentos en la segunda; acciones al pie, a ancho completo |

**Regla que gobierna todas las variantes: lo que el estado no admite no se dibuja**, ni siquiera inhabilitado. Un control inhabilitado invita a averiguar por qué; un control ausente no. La única excepción es la inhabilitación **momentánea** durante una operación en curso, que evita el doble disparo y no es una regla de estado.

**Regla que gobierna la variante de la comisión: la fila no transporta ningún campo de decisión.** El listado no ofrece aprobar ni rechazar: la decisión sólo existe dentro de la superficie donde el trabajo está a la vista.

## 4. Datos que consume

Todos llegan en la proyección de listado, que es **deliberadamente pobre** y por eso barata de recorrer.

| Dato | Origen | Nota |
| --- | --- | --- |
| Nombre del trabajo | Proyección de listado | Lo declara el alumno |
| Fecha del trabajo | Proyección de listado | La declara el alumno. **Se rotula distinto** de cualquier fecha que produzca el sistema |
| Estado | Proyección de listado | Conjunto cerrado de cuatro valores, dos terminales |
| Cantidad de piezas | Proyección de listado | Número tabular |
| Cantidad de advertencias | Proyección de listado | Número tabular. Cero se muestra como cero, no como hueco |
| Identificación del alumno dueño | Proyección de listado, **sólo en el listado de la comisión** | Alimenta la cabecera del grupo |

**Lo que la fila no consume y no muestra, y es deliberado:** el texto original, las observaciones una por una y el **comentario del administrador**. El comentario viaja al alumno **únicamente en el detalle del trabajo**: el listado no arrastra el texto libre de cada trabajo, y esa es una decisión de contrato tomada aguas arriba que esta representación respeta. Lo que sí llega al listado es el **estado**, que es donde el alumno se entera del desenlace sin abrir nada.

## 5. Restricciones de accesibilidad

- **El estado se comunica por texto y por color, nunca sólo por color.** Las cuatro insignias llevan su etiqueta escrita, y la representación se revisa en escala de grises antes de darse por aprobada.
- **Cada acción declara sobre qué trabajo actúa**, no sólo su verbo. Una lista de veinte filas que anuncia «abrir, editar, eliminar» veinte veces seguidas es inutilizable con lector de pantalla.
- La fila se marca como fila de tabla con encabezados de columna asociados en la versión ancha, y como artículo de lista en la angosta.
- Objetivos de toque de al menos 24×24 px en todas las acciones, en las dos versiones.
- Los recuentos se anuncian con su unidad —«3 piezas», «2 advertencias»— y no como números sueltos.
- Al volver de un trabajo a su listado, el foco vuelve a la fila desde la que se salió.
- La insignia de `Rechazado` usa color de peligro **por convención de estado y no como reproche**; su texto es el estado y no un juicio.

## 6. Reutilización

| Artefacto que la invoca | Cómo |
| --- | --- |
| [`Wireframes-Panel-De-Trabajos-Del-Alumno.md`](Wireframes-Panel-De-Trabajos-Del-Alumno.md) | Fila completa, en sus cuatro variantes de estado propio |
| [`Wireframes-Listado-De-La-Comision.md`](Wireframes-Listado-De-La-Comision.md) | Fila completa, variante de la comisión, dentro de grupos por alumno |
| [`Wireframes-Vista-De-Trabajo.md`](Wireframes-Vista-De-Trabajo.md) | Sólo la insignia, en la cabecera del trabajo y en el bloque de datos |
| [`Wireframes-Envio-De-Trabajo.md`](Wireframes-Envio-De-Trabajo.md) | Sólo la insignia, en el bloque de resultado del envío |
| [`Wireframes-Resolucion-Del-Trabajo.md`](Wireframes-Resolucion-Del-Trabajo.md) | Sólo la insignia, en el bloque de trabajo resuelto |
| [`Experiencia-De-Uso.md`](Experiencia-De-Uso.md) §4.3 | La tabla de qué ofrece cada estado a cada papel, que esta representación materializa |

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. Centraliza la fila de trabajo y su insignia de estado, usadas en cinco artefactos de esta sección, con las seis variantes, la regla de que lo que el estado no admite no se dibuja, la regla de que la fila no transporta campo de decisión, la declaración de que el comentario no viaja en el listado, y las restricciones de accesibilidad que hacen legible una lista larga con lector de pantalla. |
| 1.0 | 2026-08-09 | Correcciones absorbidas del audit `B-02-03-GeometriaFactory-Web-r1.md` (ronda 1), **sin subir versión** por `Master-Prompt.md` §5, que lo admite mientras el documento está en estado `Propuesto`. **H-03**: la cabecera completa su `Trazabilidad downstream`, que declaraba categorías sin identificadores, con las `US-XX` que hereda de sus dos wireframes invocantes y con tres escenarios de prueba propios —recuento de acciones por estado, cuatro estados distinguibles y revisión de las insignias en escala de grises—. |
