# Wireframes — Listado de la comisión

**Proyecto de código:** GeometriaFactory-Web
**Documento:** Wireframes-Listado-De-La-Comision.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** UX/UI Designer + Frontend Lead (AG-03)
**Variante:** UX/UI
**Trazabilidad upstream:** `../02-Especificacion-Funcional/Casos-De-Uso/CU-08-Recorrer-La-Entrega-De-La-Comision.md` íntegro —§4, FA-01 a FA-04, §6, §6.1 y CA-01 a CA-07—; `../02-Especificacion-Funcional/Especificacion-Funcional.md` §6 (RT-03, RT-06, RT-07, RT-09); `../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-07-Revision-De-La-Comision-En-Un-Solo-Lugar.md` §1, §5 (los siete criterios); `NB-09` §5 (primer criterio); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-12, F-15), §4.1 (RN-03, RN-11), §4.2, §6 (flujos 2.1 y 3), §17.6 P.4; `Design-Rules-Web-Generico.md` §3, §4.3, §4.8, §4.10, §5, §7, §8; `Design-Rules-Blazor-Mudblazor.md` §4, §5
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

**Nombre canónico de superficie: `Listado-De-La-Comision`.**

Es la ruta inicial del administrador y el **único lugar** donde ve los trabajos de toda la comisión, agrupados y filtrables por alumno, para recorrer la entrega de una sola vez sin pedirle nada a nadie.

Dos características lo definen y las dos son de contrato:

1. **No muestra ningún trabajo en estado `Borrador`.** No forman parte del flujo de trabajo del administrador. El recorte lo decide el servicio de datos según el papel de quien pide: **esta superficie no filtra por su cuenta**, porque si lo hiciera la regla dependería de la pantalla.
2. **No ofrece resolver sin abrir.** Aprobar y rechazar sólo existen dentro de la superficie donde el trabajo está a la vista, y eso es deliberado: un desenlace se decide mirando el trabajo, no leyendo una fila.

## 2. Layout

Shell de trabajo con la barra lateral del administrador. Lista agrupada por alumno.

```text
+----------+----------------------------------------------------------------+
| Laborat. |  Entrega de la comisión                                        |
|          |  Los trabajos entregados, agrupados por alumno. Los borradores |
| ·Entrega |  que los alumnos están armando no aparecen acá.                 |
|  de la   |  ------------------------------------------------------------- |
|  comisión|  [ alumno: todos          v ]  [ estado: todos v ]  [resumen]  |
| ·Cuentas | --------------------------------------------------------------|
| ·Mi      |  v (AD) Ana Diaz — ana@ej.test                       3 trabajos|
|  contra- |     TRABAJO         FECHA      ESTADO       PIEZAS  ADV.       |
|  seña    |     Cubo y ortoedro 12/08/2026 [Pendiente]     3      2  [abrir]
|          |     Entrega 1       03/08/2026 [Finalizado]   1      0  [abrir]
|          |     Primer intento  01/08/2026 [Rechazado]     1      1  [abrir]
| -------- | --------------------------------------------------------------|
| Docente  |  v (BL) Beto Lopez — beto@ej.test                    1 trabajo |
| [Cerrar] |     Segundo intento 05/08/2026 [Rechazado]     1      0  [abrir]
| v1.4.2   | --------------------------------------------------------------|
|          |  Sólo figuran los alumnos con trabajos entregados. Si un      |
|          |  alumno no aparece, todavía no entregó nada.                   |
+----------+----------------------------------------------------------------+
```

Panel de resumen, capacidad de prioridad menor prevista para una etapa posterior. Se dibuja para que su lugar quede fijado y su incorporación no obligue a rediseñar la superficie:

```text
   +--------------------------------------------------------------+
   |  Resumen por alumno                             [ Cerrar ]   |
   |  ALUMNO           PENDIENTES   FINALIZADOS   RECHAZADOS       |
   |  Ana Diaz              1            1             1           |
   |  Beto Lopez            0            0             1           |
   +--------------------------------------------------------------+
```

**Los datos de ejemplo de esta superficie tienen un solo dueño, y es el panel del alumno.** Los trabajos de Ana Diaz son los mismos que declara `Wireframes-Panel-De-Trabajos-Del-Alumno.md` §2, menos el borrador, que esta superficie no muestra: tres entregados, y por eso el recuento del grupo dice **3 trabajos** y la fila del resumen dice **1 / 1 / 1**. Los recuentos de piezas y de advertencias de cada fila son los del escenario de datos que ese trabajo materializa —`E-1` para `Cubo y ortoedro`, `E-4` para `Entrega 1`, `E-3` para `Primer intento` y `E-6` para `Segundo intento`, del `PRODUCT-INTAKE` §20—, y no cifras compuestas para llenar la columna: un recuento que no sale de un escenario declarado hace que la superficie no se pueda probar contra ningún dato.

**Por qué `Primer intento` es de Ana y no de Beto.** Es de Ana, como lo declara el panel del alumno. La validación visual de la Fase B2 expuso que este documento se lo atribuía a Beto Lopez con la misma fecha, el mismo estado y los mismos recuentos, de modo que el mismo trabajo tenía dos dueños según qué documento se leyera. El segundo alumno con entrega conserva su grupo con un trabajo propio, `Segundo intento`, que además demuestra el **rechazo sin comentario escrito**: el nombre se compuso en la Fase B2 y el Product Owner lo aprobó al aprobar la maqueta.

## 3. Componentes principales

| Componente | Patrón del catálogo | Propósito | Datos que muestra | Comportamiento |
| --- | --- | --- | --- | --- |
| Encabezado de la superficie | Base §4.3 | Nombrar la superficie y **declarar el recorte** | Título y subtítulo que dice que los borradores no aparecen | El título es el encabezado de primer nivel |
| Barra de filtros | Base §4.10 | Acotar por alumno y por estado | Selector de alumno, selector de estado | El filtro por alumno **vuelve a pedir la colección** con el criterio poblado; el de estado acota lo ya recibido |
| Cabecera de grupo | Base §3.2 | Agrupar por alumno | Iniciales, nombre, correo, recuento de trabajos | Colapsable. El grupo colapsado conserva su recuento a la vista |
| Fila de trabajo | [`Representacion-Fila-De-Trabajo.md`](Representacion-Fila-De-Trabajo.md) | Presentar un trabajo | Nombre, fecha, insignia de estado, piezas, advertencias | Acción única: abrir. **No hay acciones de decisión** |
| Insignia de estado | Representación §2 | Declarar el estado | `Pendiente`, `Finalizado` o `Rechazado`. **Nunca `Borrador`** | Siempre con texto |
| Nota de ausencia | Base §5 | Evitar que la ausencia de un alumno se lea como un trabajo perdido | Texto fijo al pie de la lista | Inerte. Resuelve una pregunta que el docente se hace siempre |
| Acción de resumen | Base §4.9, secundaria | Abrir el recuento por alumno y por estado | «Resumen» | Capacidad de prioridad menor, prevista para una etapa posterior |
| Panel de resumen | Base §4.3 | Recuento por alumno y por estado | Una fila por alumno y una columna por estado | Se abre y se cierra sin dejar la superficie |
| Aviso de indisponibilidad | [`Wireframes-Estado-Degradado-Y-Reconexion.md`](Wireframes-Estado-Degradado-Y-Reconexion.md) | Declarar que no hay datos | — | **Reemplaza la lista. No se muestran datos viejos** |

**Lo que esta superficie no dibuja:** ninguna acción de aprobar o rechazar, ningún trabajo en estado `Borrador`, ningún control de retiro. El retiro se ejerce desde la superficie del trabajo abierto, con su confirmación, y se documenta en [`Wireframes-Resolucion-Del-Trabajo.md`](Wireframes-Resolucion-Del-Trabajo.md).

## 4. Interacciones

| Acción | Disparador | Resultado esperado | Precondición |
| --- | --- | --- | --- |
| Abrir la superficie | Ingreso como administrador, o destino «Entrega de la comisión» | Se pide la colección **sin criterio de filtro poblado**, con el alcance que el papel determina | Sesión de administrador |
| Filtrar por alumno | Selección | Se vuelve a pedir la colección con el criterio poblado | — |
| Filtrar por estado | Selección | Acota lo ya recibido, **sin ida y vuelta al servidor** | — |
| Colapsar o expandir un grupo | Activación de la cabecera | El grupo cambia de estado. El recuento sigue a la vista | — |
| Abrir un trabajo | Activación de la fila o de «abrir» | Navega a `Vista-De-Trabajo` con la superficie **idéntica a la que ve el alumno** | El trabajo es visible para el administrador |
| Volver desde un trabajo | Regreso | **Se vuelve a pedir la colección**, que ya refleja los estados actualizados | — |
| Abrir el resumen | Acción secundaria | Se muestra el recuento por alumno y por estado | Capacidad disponible |
| Pedir por dirección directa un trabajo en estado `Borrador` | Entrada directa | Mensaje neutro que **no distingue** el trabajo inexistente del no visible, y regreso acá. **No se confirma que ese trabajo exista** | — |

## 5. Estados

| Estado | Condición que lo produce | Representación esperada |
| --- | --- | --- |
| **Vacío** | Ningún trabajo entregado en toda la comisión, declarado **por el tipo recibido y no por el conteo** | Ilustración neutra y texto que explica que los trabajos aparecen cuando los alumnos envían. **Sin acción de crear**: el administrador no carga trabajos |
| **Cargando** | La colección está en camino | Esqueleto por fila dentro de cada grupo. **Nunca una tabla vacía mientras carga** |
| **Con datos** | Hay trabajos entregados | Grupos por alumno con sus filas |
| **Filtrado sin resultados** | El filtro no deja ningún trabajo | Estado vacío de filtro con la acción de limpiarlo. **Distinto del vacío de colección** |
| **Grupo colapsado** | El administrador colapsó un alumno | Cabecera con su recuento; las filas se ocultan |
| **Alumno del filtro inexistente** | El filtro referencia un alumno que ya no existe, por ejemplo porque fue dado de baja | Se informa y se recarga la lista **sin filtro**. Recuperación por reintento |
| **Cero borradores** | Siempre | **Ningún trabajo en estado `Borrador` figura en ningún grupo.** Es un estado permanente de la superficie y se verifica por recuento con umbral 0 |
| **Resumen abierto** | Se pidió el recuento | Panel con una fila por alumno y una columna por cada uno de los tres estados visibles |
| **Indisponible** | El servicio de datos no responde | Aviso de indisponibilidad en lugar de la lista. **No se muestra ninguna lista con datos viejos.** Ver [`Wireframes-Estado-Degradado-Y-Reconexion.md`](Wireframes-Estado-Degradado-Y-Reconexion.md) |
| **Reconectando** | Se corta el circuito | Cartel de reconexión superpuesto; la lista permanece a la vista |

## 6. Versión angosta

Punto de quiebre principal en 768 px [ASUNCIÓN].

- **Las filas pasan a tarjetas apiladas dentro de su grupo**, con el nombre del trabajo y su insignia arriba y los recuentos debajo.
- **Las cabeceras de grupo se mantienen y quedan adheridas al borde superior al desplazarse**: en pantalla angosta es fácil perder de vista a qué alumno pertenece lo que se está mirando, y eso es exactamente el dato que el docente necesita.
- Los dos selectores de filtro se apilan a ancho completo.
- El panel de resumen pasa a una tarjeta por alumno con sus tres recuentos, en lugar de una tabla de cuatro columnas.
- La nota de ausencia se mantiene al pie.
- La barra lateral colapsa según el patrón del documento base.
- Legible sin desplazamiento horizontal a 320 px.

## 7. Notas de implementación

**Accesibilidad.** Cada grupo se marca como región con su alumno por nombre, y su cabecera declara su estado de expansión. Cada acción de abrir declara **sobre qué trabajo actúa**. Las tres insignias llevan texto. Al volver de un trabajo, el foco vuelve a la fila desde la que se salió, y no al principio de la lista: sin eso, recorrer treinta trabajos con teclado obliga a treinta recorridos completos. El resultado de un cambio de filtro se anuncia con el recuento resultante. Objetivos de toque de al menos 24×24 px.

**Performance percibida.** Es la superficie con más volumen del producto. Esqueleto por fila por encima de 400 ms. **No hay paginación** [A VERIFICAR, ver §10 de `Experiencia-De-Uso.md`]: el alcance declarado es una comisión, y la agrupación con el filtro es la forma de organización que el negocio pidió. Si el volumen resultara mayor, la superficie afectada es ésta y el cambio es acotado.

**Internacionalización.** Recuentos con números tabulares para que las columnas alineen. Las fechas de los trabajos las declaran los alumnos.

**Restricciones de arquitectura.** La colección llega **por el servidor de la pieza pública**, y el filtro por alumno la vuelve a pedir por ese mismo camino: **ningún guion del navegador consulta el servicio de datos**. La pieza pública **no guarda copia**: cuando el servicio no está, no hay lista. **El recorte que excluye los borradores lo decide el servicio de datos**, no la pantalla. Ningún mensaje incluye la dirección de un servicio interno.

## 8. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Persona objetivo | El docente como administrador, en tanda de revisión |
| CU origen | [`CU-08`](../02-Especificacion-Funcional/Casos-De-Uso/CU-08-Recorrer-La-Entrega-De-La-Comision.md) íntegro |
| Reglas de negocio relevantes | `RN-11` (el administrador no ve los borradores), `RN-03` (trabajo ajeno indistinguible de inexistente) |
| Restricciones transversales | `RT-03`, `RT-06`, `RT-07`, `RT-09` |
| Marco aplicado | [`Experiencia-De-Uso.md`](Experiencia-De-Uso.md) §3.2, §3.7, §4.3, §7 |
| Representaciones que invoca | [`Representacion-Fila-De-Trabajo.md`](Representacion-Fila-De-Trabajo.md), [`Representacion-Sello-De-Version.md`](Representacion-Sello-De-Version.md) |
| Catálogo de diseño aplicado | `Design-Rules-Web-Generico.md`, `Design-Rules-Blazor-Mudblazor.md` |
| US a generar en 06 | `US-22`, `US-23` |
| Tests previstos en 08 | Guion de demostración de la etapa `e`: dos alumnos con trabajos entregados y un borrador que ningún grupo muestra, con cero borradores en todo el listado, y con el recuento de cada grupo igual a la cantidad de filas que ese grupo dibuja; agrupación y filtro disponibles; filtro por un alumno; recorrido completo sin solicitudes externas; el listado sin ofrecer aprobar ni rechazar; listado vacío distinguible del estado degradado; etapa `i` para el resumen |

## 9. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. Listado de la comisión agrupado por alumno con filtro, con el recorte de borradores declarado en el subtítulo y verificable por recuento con umbral 0, la nota de ausencia que evita que un alumno sin trabajos se lea como un trabajo perdido, la declaración de que el listado no ofrece resolver sin abrir, el panel de resumen dibujado como capacidad de prioridad menor con su lugar ya fijado, y diez estados declarados para la Fase B2. |
| 1.0 | 2026-08-09 | Correcciones absorbidas del audit `B-02-03-GeometriaFactory-Web-r1.md` (ronda 1), **sin subir versión** por `Master-Prompt.md` §5, que lo admite mientras el documento está en estado `Propuesto`. **H-06**: las `NB-07` y `NB-09` de la cabecera pasan a citarse con sección y criterio numerado. |
| 1.0 | 2026-08-09 | Retroalimentación de la Fase B2 de validación de maqueta del proyecto de código `GeometriaFactory-Web`, **sin subir versión** por `Master-Prompt.md` §5, que lo admite mientras el documento está en estado `Propuesto`. **H-03** (dueño contradictorio de un trabajo): `Primer intento` pasa al grupo de Ana Diaz, que es de quien lo declara `Wireframes-Panel-De-Trabajos-Del-Alumno.md` §2; el grupo de Beto Lopez conserva un trabajo propio, `Segundo intento`, que además demuestra el rechazo sin comentario escrito. §2 suma la nota que declara al panel del alumno como dueño único del conjunto de datos de ejemplo. **H-04** (recuentos sin dato declarado): el recuento del grupo de Ana pasa de «2 trabajos» a «3 trabajos», la fila del resumen de Ana pasa de `1 / 1 / 0` a `1 / 1 / 1`, y las columnas de piezas y de advertencias de las cuatro filas pasan a los valores del escenario del `PRODUCT-INTAKE` §20 que cada trabajo materializa. §8 reescribe el guion de demostración, que describía un caso que el §2 no contenía. |
