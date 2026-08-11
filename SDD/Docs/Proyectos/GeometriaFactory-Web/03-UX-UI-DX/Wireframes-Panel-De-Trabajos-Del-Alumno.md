# Wireframes — Panel de trabajos del alumno

**Proyecto de código:** GeometriaFactory-Web
**Documento:** Wireframes-Panel-De-Trabajos-Del-Alumno.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-09
**Autor:** UX/UI Designer + Frontend Lead (AG-03)
**Variante:** UX/UI
**Trazabilidad upstream:** `../02-Especificacion-Funcional/Casos-De-Uso/CU-06-Consultar-El-Listado-Propio-Y-Operar-Sobre-El-Borrador.md` íntegro —§4, FA-01 a FA-05, §6, §6.1 y CA-01 a CA-07—; `../02-Especificacion-Funcional/Especificacion-Funcional.md` §6 (RT-03, RT-06, RT-07, RT-09); `../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-03-Trabajo-Con-Dueno-Estado-Y-Persistencia.md` §1, §5 (segundo, tercero y cuarto criterio); `NB-09` §5 (sexto criterio); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-07, F-08), §4.1 (RN-03, RN-04, RN-10), §4.2 (tabla de quién puede qué), §7 (CL-5, CL-10), §17.6 P.4; `Design-Rules-Web-Generico.md` §3, §4.3, §4.8, §4.9, §5, §7, §8; `Design-Rules-Blazor-Mudblazor.md` §4, §5
**Trazabilidad downstream:** Fase B2 de validación visual de maqueta; `05-Arquitectura-Tecnica`; `06-Backlog-Tecnico`; `08-Calidad-Y-Pruebas`

---

## Tabla de contenido

- [1. Pantalla y propósito](#1-pantalla-y-propósito)
- [2. Layout](#2-layout)
- [3. Componentes principales](#3-componentes-principales)
- [4. Interacciones](#4-interacciones)
- [5. Estados](#5-estados)
- [6. Versión angosta](#6-versión-angosta)
- [7. Notas de implementación](#7-notas-de-implementación)
- [8. Trazabilidad](#8-trazabilidad)
- [9. Control de cambios](#9-control-de-cambios)

---

## 1. Pantalla y propósito

**Nombre canónico de superficie: `Panel-De-Trabajos-Del-Alumno`.**

Es la ruta inicial del alumno con sesión y el lugar donde ve **todos** sus trabajos con su estado, incluidos los que quedaron en borrador, vuelve sobre los que todavía puede editar, los elimina si quiere, y **se entera del desenlace**.

Su trabajo de diseño es hacer obvio, sin leer nada, qué se puede hacer con cada trabajo. Son cuatro estados, dos de ellos terminales, y las acciones que un estado no admite **no se dibujan**.

## 2. Layout

Shell de trabajo con la barra lateral del alumno.

```text
+----------+----------------------------------------------------------------+
| Laborat. |  Mis trabajos                            [ + Trabajo nuevo ]   |
|          |  Todo lo que cargaste, con el estado en que está.              |
| ·Mis     |  ------------------------------------------------------------- |
|  trabajos|  [ buscar por nombre    ]  [ estado: todos v ]                 |
| ·Trabajo |  --------------------------------------------------------------|
|  nuevo   |  TRABAJO         FECHA      ESTADO      PIEZAS  ADV.   ACCIONES|
| ·Mi      |  --------------------------------------------------------------|
|  contra- |  Cubo y ortoedro 12/08/2026 [Pendiente]    3      2    [abrir] |
|  seña    |  Prueba 2        10/08/2026 [Borrador]     1      0    [abrir] |
|          |                                                       [editar]|
| -------- |                                                       [elimin]|
| Ana Diaz |  Entrega 1       03/08/2026 [Finalizado]   1      0    [abrir] |
| [Cerrar  |  Primer intento  01/08/2026 [Rechazado]    1      1    [abrir] |
|  sesión] |  --------------------------------------------------------------|
| v1.4.2   |                                                                |
+----------+----------------------------------------------------------------+
```

**Los cuatro trabajos del ejemplo son de Ana Diaz y ningún otro documento se los atribuye a nadie más.** Este documento es el dueño del conjunto de datos de ejemplo del alumno: `Wireframes-Listado-De-La-Comision.md` §2 lo reusa quitando el borrador, que esa superficie no muestra. Los recuentos de piezas y de advertencias de cada fila salen del escenario de datos que el trabajo materializa —`E-1` para `Cubo y ortoedro`, `E-5` para `Prueba 2`, `E-4` para `Entrega 1` y `E-3` para `Primer intento`, del `PRODUCT-INTAKE` §20— y no se componen para llenar la columna.

Estado vacío, que es el primero que el alumno ve en su vida en el producto:

```text
              +--------------------------------------------+
              |            [ ilustración neutra ]          |
              |     Todavía no cargaste ningún trabajo     |
              |  Cargá el texto que produjo tu programa     |
              |  de la Actividad 1 y mirálo dibujado.       |
              |          [ + Cargar mi primer trabajo ]     |
              +--------------------------------------------+
```

## 3. Componentes principales

| Componente | Patrón del catálogo | Propósito | Datos que muestra | Comportamiento |
| --- | --- | --- | --- | --- |
| Encabezado de la superficie | Base §4.3 | Nombrar la superficie y alojar la acción primaria | Título, subtítulo de una línea | El título es el encabezado de primer nivel |
| Acción primaria | Base §4.9 | Cargar un trabajo nuevo | «Trabajo nuevo» | Lleva a `Envio-De-Trabajo`. **Presente en todos los estados, incluido el vacío** |
| Barra de filtros | Base §4.10 | Acotar el listado cuando hay muchos | Campo de búsqueda por nombre y selector de estado | **Filtra sobre lo ya recibido. No consulta al servicio de datos**, porque eso implicaría una llamada del navegador y la prohíbe la regla de arquitectura |
| Fila de trabajo | [`Representacion-Fila-De-Trabajo.md`](Representacion-Fila-De-Trabajo.md) | Presentar un trabajo y sus acciones | Nombre, fecha, insignia de estado, cantidad de piezas, cantidad de advertencias | Las acciones dependen del estado. Ver §4.3 de la representación |
| Insignia de estado | Representación §2 | Declarar el estado | Uno de los cuatro valores, **siempre con texto** | El color es refuerzo |
| Estado vacío | Base §5 | Invitar a la primera carga | Ilustración vectorial, texto orientativo y acción siguiente | **Es una invitación, no un adorno ni un hueco** |
| Diálogo de confirmación de eliminación | Base §4.4, Blazor §4 | Evitar la eliminación accidental | Nombre del trabajo | Sólo se abre desde una fila en estado `Borrador` |
| Aviso de indisponibilidad | [`Wireframes-Estado-Degradado-Y-Reconexion.md`](Wireframes-Estado-Degradado-Y-Reconexion.md) | Declarar que no hay datos | — | **Reemplaza el listado. No se muestran datos viejos** |

**Lo que el listado no muestra, y es deliberado:** el texto original, las observaciones una por una y el **comentario del administrador**. La proyección es pobre a propósito y por eso es barata de recorrer; el comentario viaja al alumno **únicamente en el detalle del trabajo**, y esa decisión está tomada aguas arriba. Lo que sí llega al listado es el **estado**, que es donde el alumno se entera del desenlace sin abrir nada.

## 4. Interacciones

| Acción | Disparador | Resultado esperado | Precondición |
| --- | --- | --- | --- |
| Abrir la superficie | Ingreso, o destino «Mis trabajos» | Se pide el listado acotado al solicitante, **con sus borradores incluidos, que son suyos** | Sesión de alumno |
| Abrir un trabajo | Activación de la fila o de «abrir» | Navega a `Vista-De-Trabajo` | Cualquier estado |
| Volver sobre un borrador | Activación de «editar» | Navega a `Envio-De-Trabajo` con los datos y el texto tal como quedaron | **Sólo en estado `Borrador`** |
| Eliminar un borrador | Activación de «eliminar» | Se abre el diálogo de confirmación; al confirmar, se elimina y se vuelve a pedir el listado | **Sólo en estado `Borrador`** |
| Cargar un trabajo nuevo | Acción primaria | Navega a `Envio-De-Trabajo` | — |
| Filtrar o buscar | Tecleo o selección | El listado recibido se acota. **Sin ida y vuelta al servidor** | — |
| Volver desde un trabajo | Regreso desde `Vista-De-Trabajo` o desde el envío | **Se vuelve a pedir el listado**, que refleja el estado actual | — |
| Pedir por dirección directa un trabajo ajeno | Entrada directa | Mensaje neutro que **no distingue** el trabajo ajeno del inexistente, y regreso acá. No se confirma que ese trabajo exista | — |

**Por qué el estado nuevo no aparece solo.** Cuando el administrador resuelve un trabajo, el listado del alumno **no se actualiza por su cuenta**: el alumno ve el estado nuevo la próxima vez que lo pida. Lo impone la regla de que ningún guion del navegador consulta al servicio de datos, y es una limitación aceptada del producto, no un descuido. La superficie no simula lo contrario ni promete avisos que no puede dar.

## 5. Estados

| Estado | Condición que lo produce | Representación esperada |
| --- | --- | --- |
| **Vacío** | La colección llegó con cero elementos, declarado **por el tipo recibido y no por el conteo** | Ilustración neutra, texto orientativo y la acción de cargar el primer trabajo. **Distinguible del aviso de indisponibilidad** |
| **Cargando** | El listado está en camino | Esqueleto por fila. **Nunca una tabla vacía mientras carga**: se confundiría con el estado vacío |
| **Con datos** | Hay trabajos | Filas con su insignia y sus acciones por estado |
| **Filtrado sin resultados** | El filtro no deja ninguna fila | Estado vacío de filtro, con la acción de limpiar el filtro. **Distinto del vacío de colección**: acá sí hay trabajos |
| **Acciones por estado** | Cada fila, según su estado | `Borrador`: abrir, editar, eliminar. `Pendiente`, `Finalizado` y `Rechazado`: **sólo abrir**. Lo que el estado no admite **no se dibuja**, ni siquiera inhabilitado |
| **Confirmando eliminación** | Se pidió eliminar un borrador | Diálogo con el nombre del trabajo y las dos acciones. Foco dentro del diálogo |
| **Eliminando** | La eliminación está en curso | Acción del diálogo inhabilitada con indicador |
| **Éxito de eliminación** | El trabajo dejó de existir | Confirmación sutil y listado vuelto a pedir, ya sin esa fila |
| **Error de operación** | Se fuerza la eliminación de un trabajo que no está en estado `Borrador` | La eliminación no procede. Se declara **el estado actual del trabajo** y se recarga el listado. Terminación controlada |
| **Trabajo ajeno o inexistente** | Se pidió por dirección directa un trabajo que no es del alumno | Mensaje neutro que no distingue los dos casos, y regreso al listado, **que no cambia** |
| **Indisponible** | El servicio de datos no responde | Aviso de indisponibilidad en lugar del listado. **No se muestra ningún listado con datos viejos**, porque la pieza pública no guarda copia |
| **Reconectando** | Se corta el circuito | Cartel de reconexión superpuesto; el listado permanece a la vista |

## 6. Versión angosta

Punto de quiebre principal en 768 px [ASUNCIÓN].

- **Las filas dejan de ser filas de tabla y pasan a ser tarjetas apiladas**, una por trabajo: nombre y estado arriba, fecha y recuentos debajo, acciones al pie. Comprimir seis columnas en 320 px produciría celdas ilegibles.
- La insignia de estado sube junto al nombre: es lo que el alumno viene a mirar.
- Las acciones pasan a ancho completo dentro de la tarjeta, con al menos 24×24 px de objetivo.
- La barra de filtros se apila y el selector de estado pasa a ancho completo.
- La acción primaria «Trabajo nuevo» se mantiene visible en el encabezado.
- La barra lateral colapsa según el patrón del documento base.
- Legible sin desplazamiento horizontal a 320 px.

## 7. Notas de implementación

**Accesibilidad.** El listado se marca como tabla con encabezados de columna asociados en la versión ancha, y como lista de artículos en la angosta. **Cada acción por fila declara sobre qué trabajo actúa**, no sólo su verbo: una fila de acciones que anuncia «abrir, editar, eliminar» tres veces seguidas es inutilizable con lector de pantalla. Las cuatro insignias llevan texto y no se comunican sólo por color. El diálogo de confirmación toma el foco al abrirse y lo devuelve al control que lo abrió al cerrarse, sin trampa de foco. El estado vacío tiene su ilustración marcada como decorativa: **la información crítica está en el texto**, no en el dibujo. Objetivos de toque de al menos 24×24 px en todas las acciones por fila.

**Performance percibida.** Esqueleto por fila por encima de 400 ms, con la cantidad de filas del último recorrido conocido o tres si no hay ninguno. El filtro es local y responde de inmediato.

**Internacionalización.** La fecha del trabajo la declara el alumno y se rotula como tal, distinta de cualquier fecha que produzca el sistema. Los recuentos usan números tabulares para que las columnas alineen.

**Restricciones de arquitectura.** El listado llega **por el servidor de la pieza pública**. El filtro y la búsqueda actúan sobre lo ya recibido y **no originan ninguna petición**. La pieza pública **no guarda copia de los datos**: cuando el servicio no está, no hay listado que mostrar. Ocultar una acción **no es** hacer cumplir una regla: la acotación se sostiene también cuando la solicitud se fuerza sin pasar por la pantalla, y quien la hace cumplir es el servicio de datos.

## 8. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Persona objetivo | El alumno de la comisión |
| CU origen | [`CU-06`](../02-Especificacion-Funcional/Casos-De-Uso/CU-06-Consultar-El-Listado-Propio-Y-Operar-Sobre-El-Borrador.md) íntegro |
| Reglas de negocio relevantes | `RN-03` (trabajo ajeno indistinguible de inexistente), `RN-04` (eliminación acotada al borrador), `RN-10` (desenlace exclusivo y terminalidad) |
| Restricciones transversales | `RT-03`, `RT-06`, `RT-07`, `RT-09` |
| Marco aplicado | [`Experiencia-De-Uso.md`](Experiencia-De-Uso.md) §2.4, §3.2, §4.3, §5, §7 |
| Representaciones que invoca | [`Representacion-Fila-De-Trabajo.md`](Representacion-Fila-De-Trabajo.md), [`Representacion-Sello-De-Version.md`](Representacion-Sello-De-Version.md) |
| Catálogo de diseño aplicado | `Design-Rules-Web-Generico.md`, `Design-Rules-Blazor-Mudblazor.md` |
| US a generar en 06 | `US-15`, `US-16`, `US-17` |
| Tests previstos en 08 | Guion de demostración de la etapa `e`: cuatro trabajos en los cuatro estados distinguibles entre sí; recuento de acciones por estado; eliminación forzada sobre un estado que no la admite; trabajo ajeno por dirección directa; listado vacío distinguible del estado degradado; etapa `h` para el desenlace visible en el listado; recorrido por teclado y revisión en escala de grises de las cuatro insignias |

## 9. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. Listado propio con los cuatro estados y sus acciones por estado dibujadas por ausencia y no por inhabilitación, filtro local que no origina peticiones, estado vacío como invitación distinguible del aviso de indisponibilidad por el tipo recibido, declaración explícita de que el comentario no viaja en el listado y de por qué el estado nuevo no aparece solo, versión angosta con reflujo de filas a tarjetas, y doce estados declarados para la Fase B2. |
| 1.0 | 2026-08-09 | Correcciones absorbidas del audit `B-02-03-GeometriaFactory-Web-r1.md` (ronda 1), **sin subir versión** por `Master-Prompt.md` §5, que lo admite mientras el documento está en estado `Propuesto`. **H-06**: las `NB-03` y `NB-09` de la cabecera pasan a citarse con sección y criterio numerado. |
| 1.0 | 2026-08-09 | Retroalimentación de la Fase B2 de validación de maqueta del proyecto de código `GeometriaFactory-Web`, **sin subir versión** por `Master-Prompt.md` §5, que lo admite mientras el documento está en estado `Propuesto`. **H-04** (recuentos sin dato declarado): las columnas de piezas y de advertencias de `Entrega 1` pasan de `4 / 1` a `1 / 0` y las de `Primer intento` de `2 / 3` a `1 / 1`, que son los valores de los escenarios `E-4` y `E-3` del `PRODUCT-INTAKE` §20 que esos trabajos materializan; las cifras anteriores no correspondían a ningún escenario declarado. **H-03**: §2 suma la nota que declara a este documento dueño único del conjunto de datos de ejemplo del alumno, del que `Wireframes-Listado-De-La-Comision.md` deriva el suyo. |
