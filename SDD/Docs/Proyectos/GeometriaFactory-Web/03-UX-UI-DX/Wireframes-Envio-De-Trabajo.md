# Wireframes — Envío de trabajo

**Proyecto de código:** GeometriaFactory-Web
**Documento:** Wireframes-Envio-De-Trabajo.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-09
**Autor:** UX/UI Designer + Frontend Lead (AG-03)
**Variante:** UX/UI
**Trazabilidad upstream:** `../02-Especificacion-Funcional/Casos-De-Uso/CU-05-Enviar-Un-Trabajo-Y-Ver-El-Resultado-De-La-Interpretacion.md` íntegro —§4, FA-01 a FA-06, §6, §6.1 y CA-01 a CA-08—; `../02-Especificacion-Funcional/Especificacion-Funcional.md` §6 (RT-03, RT-04, RT-05, RT-07, RT-08, RT-11) y §7; `../../GeometriaFactory-Visor/02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md` §4.1, §4.2, §4.5 y §6; `../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-04-Interpretacion-Fiel-Del-Dato-Del-Alumno.md` §1, §5 (tercero, cuarto, quinto y sexto criterio); `NB-03` §1, §5 (primero, segundo y quinto criterio); `NB-05` §5 (segundo y tercer criterio); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-06, F-09, F-10, F-22), §4.1 (RN-05, RN-08, RN-09), §4.2, §6 (flujos 2 y 4), §7 (CL-3, CL-4), §17.6 P.11 punto 4 y punto 5, §20.E-1 y §20.E-2; `Design-Rules-Web-Generico.md` §3, §4.4, §4.6, §4.9, §5, §7, §8; `Design-Rules-Blazor-Mudblazor.md` §2, §4, §5
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

**Nombre canónico de superficie: `Envio-De-Trabajo`.**

El alumno carga un trabajo con nombre, fecha, descripción y el texto que produjo su programa, lo previsualiza si quiere, y lo **envía**. El envío es la **única acción de guardado que el producto tiene**, y su resultado decide el estado del trabajo.

La superficie sirve a dos cursos con la misma disposición: **crear** un trabajo nuevo y **volver sobre** un borrador propio. Y tiene dos cosas que declarar con insistencia, porque son las dos que el alumno malinterpreta:

1. **No existe «guardar sin enviar».** De ahí que `Borrador` signifique exactamente «el texto no verificó».
2. **La previsualización dibuja y no verifica.** Que una pieza no se dibuje no dice nada sobre si el trabajo verifica, y que se dibujen todas, tampoco.

## 2. Layout

Shell de trabajo con la barra lateral del alumno. Dos columnas que anticipan la disposición de `Vista-De-Trabajo`, para que el alumno reconozca la superficie a la que va a llegar.

```text
+----------+----------------------------------------------------------------+
| Laborat. |  < Volver a mis trabajos                                       |
|          |  Trabajo nuevo                                                 |
| ·Mis     |  Pegá el texto tal como lo devolvió tu programa. No hace falta |
|  trabajos|  que le cambies nada.                                          |
| ·Trabajo +--------------------------+-------------------------------------+
|  nuevo   |  Nombre                  |  PREVISUALIZACIÓN                   |
| ·Mi      |  [____________________]  |  +-------------------------------+  |
|  contra- |  Fecha                   |  |                               |  |
|  seña    |  [__/__/____]            |  |   elemento de dibujo          |  |
|          |  Descripción             |  |                               |  |
| -------- |  [____________________]  |  +-------------------------------+  |
| Ana Diaz |  [____________________]  |  No se dibujaron:                   |
| [Cerrar] |                          |   · pieza 1 — tipo no dibujable     |
| v1.4.2   |  Texto del trabajo       |                                     |
|          |  +---------------------+ |  [ Previsualizar ]                  |
|          |  | [ { "Tipo": "Cil... | |                                     |
|          |  |   "Area": 36.00,    | |  El dibujo es sólo para que veas    |
|          |  |   ...               | |  lo que modelaste. No decide si tu  |
|          |  +---------------------+ |  trabajo verifica.                  |
|          |                          |                                     |
|          |  [======== Enviar =====] |                                     |
|          |  Enviar es la única forma|                                     |
|          |  de guardar. Si el texto |                                     |
|          |  no verifica, queda en   |                                     |
|          |  borrador y lo reenviás. |                                     |
+----------+--------------------------+-------------------------------------+
```

Estado de resultado, que reemplaza el contenido tras el envío y antes de volver al listado:

```text
   +----------------------------------------------------------------+
   |  [ico]  Tu trabajo quedó en estado Pendiente                   |
   |         Ya está entregado. No lo podés editar ni eliminar.     |
   |                                                                |
   |  OBSERVACIONES (2)                                             |
   |  [advertencia] figura 1 · Area                                 |
   |                declarado 36.00 · derivado 54.00                |
   |  [advertencia] figura 2 · Volumen                              |
   |                declarado 343.00 · derivado 1029.00             |
   |  Las advertencias no impiden la entrega. Mirálas: te dicen que |
   |  el valor que calculó tu programa no coincide con la geometría.|
   |                                                                |
   |  [ Ver el trabajo ]   [ Volver a mis trabajos ]                |
   +----------------------------------------------------------------+
```

**Los valores `declarado` y `derivado` se escriben con punto, también en este esquema.** Es la excepción declarada de §7 a la convención de coma decimal: se muestran exactamente como el texto del alumno los trae y como el sistema los recalcula, **sin reformatear**. El esquema los dibuja así a propósito, para que quien lo copie no reintroduzca el formateo que la regla prohíbe.

## 3. Componentes principales

| Componente | Patrón del catálogo | Propósito | Datos que muestra | Comportamiento |
| --- | --- | --- | --- | --- |
| Encabezado y subtítulo | Base §2.2 | Nombrar la tarea y **declarar que el texto no se toca** | Texto fijo por curso | El título es el encabezado de primer nivel |
| Campos de nombre, fecha y descripción | Base §4.4, §4.6 | Datos declarados del trabajo | Lo escrito | Etiqueta visible arriba. La fecha la declara el alumno |
| Área de texto del trabajo | Base §4.6 | Recibir el texto pegado | El texto **tal cual** | Avance uniforme por carácter, para que el texto del alumno se lea sin desalineación, y ancho suficiente para una línea del texto sin cortar. **No se normaliza, no se reordena y no se le quita ningún carácter** |
| Bloque de previsualización | — | Alojar el elemento de dibujo | La escena de la instancia | Es el **componente anfitrión**: provee el elemento, invoca la creación, la carga y la liberación |
| Acción de previsualizar | Base §4.9, secundaria | Dibujar sin enviar | «Previsualizar» | **Sin ninguna llamada al servicio de datos** |
| Nota de alcance de la previsualización | Base §5 | Declarar que el dibujo **no verifica** | Texto fijo | Inerte. Es la nota que evita la malinterpretación principal de la superficie |
| Lista de piezas no dibujadas | Base §5 | Enumerar lo que no se dibujó | Índice y motivo, por pieza | **Junto a la escena, nunca como observación del trabajo** |
| Acción primaria | Base §4.9 | Enviar | Verbo exacto: «Enviar» | **Es la única acción de guardado.** Se inhabilita con indicador durante el envío |
| Nota de acción única | Base §5 | Declarar que no hay «guardar sin enviar» y qué pasa si el texto no verifica | Texto fijo | Inerte, contigua a la acción primaria |
| Bloque de resultado | Base §5 | Presentar el estado alcanzado y las observaciones | Estado, y la lista completa | Reemplaza el contenido. Ver [`Representacion-Lista-De-Observaciones.md`](Representacion-Lista-De-Observaciones.md) |

**Lo que esta superficie no dibuja:** ninguna acción de guardar distinta del envío, ningún control que corrija o reformatee el texto del alumno, ningún indicador que anticipe si el trabajo va a verificar.

## 4. Interacciones

| Acción | Disparador | Resultado esperado | Precondición |
| --- | --- | --- | --- |
| Abrir en curso de creación | Acción «Trabajo nuevo» | Formulario vacío | Sesión de alumno |
| Abrir en curso de reedición | «Editar» desde una fila en estado `Borrador` | Formulario con los datos y el texto **tal como quedaron** | El trabajo es propio y está en estado `Borrador` |
| Pegar el texto | Pegado o tecleo | El texto entra **sin ninguna transformación**. Ninguna verificación consulta al servicio de datos mientras se escribe | — |
| Previsualizar | Acción secundaria | Se crea la instancia sobre el elemento de dibujo y se carga el texto. **La escena se dibuja en el navegador sin ninguna llamada al servicio de datos** | El área de texto no está vacía |
| Girar, acercar y encuadrar | Interacción con la escena | La cámara se mueve. **Cero tráfico de circuito** | Hay instancia viva |
| Previsualizar otra vez tras editar el texto | Acción secundaria | La carga **reemplaza por completo** lo dibujado y libera lo anterior | Ídem |
| Enviar | Acción primaria | Se envía el texto **carácter por carácter** tal como se pegó, y el resultado decide el estado | Nombre, fecha y texto completos |
| Volver a enviar tras un texto que no verificó | Acción primaria | Mismo contrato, con el identificador ya asignado. **Cuantas veces haga falta** | El trabajo está en estado `Borrador` |
| Abandonar la superficie | Navegación fuera | Se **libera la instancia**. Nada de lo escrito sobrevive del lado de la pieza pública | — |

## 5. Estados

| Estado | Condición que lo produce | Representación esperada |
| --- | --- | --- |
| **Vacío** | Curso de creación, formulario recién abierto | Campos vacíos y bloque de previsualización con su marco y su leyenda de que todavía no hay nada que dibujar. **No es un hueco sin explicar** |
| **Cargando** | Curso de reedición, trayendo el borrador | Esqueleto en los campos y en el área de texto |
| **Con datos** | Formulario poblado | Campos y texto a la vista |
| **Previsualizado** | Se dibujó la escena | Escena viva, y la lista de piezas no dibujadas si corresponde |
| **Enviando** | El envío está en curso | Acción primaria inhabilitada con indicador y texto que declara que se está interpretando el trabajo. **Previene el doble envío.** Sin cuenta regresiva |
| **Verificó** | El resultado trae estado `Pendiente` | Bloque de resultado que declara el estado y **que el trabajo deja de ser editable y eliminable** |
| **Verificó con advertencias** | Estado `Pendiente` con discrepancias de valor | Las advertencias se muestran con **los dos valores**, y el texto declara que **no bloquean nada**. No se presentan como faltas |
| **No verificó** | El resultado trae estado `Borrador` | **El envío no falla.** Se muestran los errores de validación con su **índice de figura y su campo señalado**, nunca un texto genérico, y se ofrece volver a enviar. Los datos y el texto quedan tal como estaban |
| **Requisito no cumplido** | Falta el nombre, la fecha o el texto | Borde de peligro en el campo y banda de error. **El texto vacío es campo ausente, no texto que no verifica**, y el mensaje lo distingue |
| **Escena no disponible** | El navegador no provee la capacidad gráfica tridimensional | El bloque de previsualización se reemplaza por un bloque explicativo. **El envío sigue disponible**: no depende del dibujo |
| **Texto no legible en la previsualización** | La fachada no obtuvo piezas del texto | La instancia queda viva y vacía. Se avisa que la previsualización no pudo dibujar nada y **no se deduce de eso ningún estado del trabajo** |
| **Piezas no dibujadas** | Alguna pieza no produjo dibujo | Lista con índice y motivo junto a la escena. **No se califican de error del trabajo** |
| **Elemento de dibujo sin tamaño** | El elemento no sirve como superficie | No hay instancia. Se informa que la previsualización no está disponible y se conserva el resto |
| **Trabajo ajeno o inexistente** | Se envía sobre un identificador que no corresponde | Mensaje neutro que **no distingue** los dos casos, y regreso al listado |
| **Indisponible** | El servicio de datos no responde | Aviso de indisponibilidad. **Conserva a la vista el texto y los datos escritos** para el reintento, y no los guarda en ningún lado. Ver [`Wireframes-Estado-Degradado-Y-Reconexion.md`](Wireframes-Estado-Degradado-Y-Reconexion.md) |
| **Reconectando** | Se corta el circuito | Cartel de reconexión superpuesto. La escena permanece a la vista y sigue girándose |

## 6. Versión angosta

Punto de quiebre principal en 768 px [ASUNCIÓN].

- **Las dos columnas se apilan**, en el orden: datos → texto del trabajo → previsualización con su acción y su nota → acción primaria con su nota.
- **Acá la previsualización no sube al primer lugar**, al revés que en `Vista-De-Trabajo`, y es deliberado: en esta superficie la tarea es cargar y enviar, y el dibujo es una comprobación optativa. Alterar el orden pondría una acción secundaria antes de los campos que la tarea exige.
- El área de texto conserva altura suficiente para ver varias líneas y **se desplaza dentro de su propio contenedor**, nunca haciendo desplazar la página.
- La acción primaria y su nota quedan al final, siempre juntas: separar la nota de la acción rompería lo que la nota explica.
- **Al cruzar el punto de quiebre, el componente anfitrión ajusta la escena** si hay instancia viva. No ocurre solo.
- La barra lateral colapsa según el patrón del documento base.
- Legible sin desplazamiento horizontal a 320 px.

## 7. Notas de implementación

**Accesibilidad.** El área de texto declara su etiqueta y su propósito, y **no se le aplica corrección ortográfica ni autocapitalización**: el texto del alumno es un dato exacto y el navegador no debe alterarlo. El elemento de dibujo lleva alternativa textual compuesta con el recuento de piezas dibujadas y no dibujadas del resultado. El bloque de resultado se anuncia como región activa: es el desenlace de la acción más importante del producto y perderlo por no mirarlo sería grave. Las observaciones llevan su severidad **escrita**. Foco: al aparecer el bloque de resultado, el foco se lleva a su encabezado. Las notas de alcance y de acción única se asocian por descripción accesible a los controles que explican.

**Performance percibida.** El envío es la acción más cara del producto y la persona lo sabe: control inhabilitado con indicador dentro, texto que declara que se está interpretando, y **sin cuenta regresiva**, porque prometer un tiempo sería prometer algo que la topología no garantiza. La previsualización, en cambio, es inmediata: no cruza la red.

**Internacionalización.** Los valores declarado y derivado de cada advertencia se muestran **exactamente como el texto los trae y como el sistema los recalcula**, sin reformatear. La fecha del trabajo la declara el alumno y se rotula como tal.

**Restricciones de arquitectura.** El envío sale **desde el servidor de la pieza pública**; la previsualización **no origina ninguna petición**. El texto viaja carácter por carácter y **no se reescribe en ningún punto del recorrido**. La escena se opera sólo por las funciones de la fachada, y la instancia **se libera al descartar el componente**. Ningún mensaje incluye la dirección de un servicio interno.

## 8. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Persona objetivo | El alumno de la comisión, en el flujo más frecuente de la cursada |
| CU origen | [`CU-05`](../02-Especificacion-Funcional/Casos-De-Uso/CU-05-Enviar-Un-Trabajo-Y-Ver-El-Resultado-De-La-Interpretacion.md) íntegro, con [`CU-06`](../02-Especificacion-Funcional/Casos-De-Uso/CU-06-Consultar-El-Listado-Propio-Y-Operar-Sobre-El-Borrador.md) FA-01 como vía de llegada al curso de reedición |
| Reglas de negocio relevantes | `RN-05` (sin errores de validación no hay paso a estado `Pendiente`; corta hoy en el envío), `RN-08` (texto conservado íntegro), `RN-09` (observación con posición y campo), `RN-03` |
| Restricciones transversales | `RT-03`, `RT-04`, `RT-05`, `RT-07`, `RT-08`, `RT-11` |
| Contrato de fachada | Creación, carga y liberación, con sus códigos de condición |
| Marco aplicado | [`Experiencia-De-Uso.md`](Experiencia-De-Uso.md) §2.4, §3.5, §4.1, §7, §8.1 |
| Representaciones que invoca | [`Representacion-Lista-De-Observaciones.md`](Representacion-Lista-De-Observaciones.md), [`Representacion-Fila-De-Trabajo.md`](Representacion-Fila-De-Trabajo.md) §2 para la insignia |
| Catálogo de diseño aplicado | `Design-Rules-Web-Generico.md`, `Design-Rules-Blazor-Mudblazor.md` |
| US a generar en 06 | `US-11`, `US-12`, `US-13`, `US-14` |
| Tests previstos en 08 | Guion de demostración de la etapa `f` con los escenarios de datos verificados del intake: el ortoedro con sus dos comas finales y una advertencia de volumen; el texto guardado idéntico carácter por carácter; el texto semilla con dos advertencias; el tipo desconocido con índice de figura y campo; recuento de una sola acción de guardado; recuento de peticiones del navegador con umbral 0; liberación de la instancia en diez recorridos; servicio detenido conservando lo escrito |

## 9. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. Superficie de la acción única de guardado, con las dos notas que desarman las malinterpretaciones principales —que no hay «guardar sin enviar» y que la previsualización dibuja pero no verifica—, dos cursos sobre la misma disposición, bloque de resultado que presenta las advertencias con los dos valores sin convertirlas en faltas, dieciséis estados declarados para la Fase B2, y una versión angosta que deliberadamente **no** sube la escena al primer lugar, al revés que la vista de trabajo, con el motivo declarado. |
| 1.0 | 2026-08-09 | Correcciones absorbidas del audit `B-02-03-GeometriaFactory-Web-r1.md` (ronda 1), **sin subir versión** por `Master-Prompt.md` §5, que lo admite mientras el documento está en estado `Propuesto`. **H-06**: las `NB-04`, `NB-03` y `NB-05` de la cabecera pasan a citarse con sección y criterio numerado. **H-08**: §3 reformula la especificación tipográfica de la fila «Área de texto del trabajo» como **avance uniforme por carácter, para que el texto del alumno se lea sin desalineación**, en lugar de nombrar una clase de fuente, que es detalle del sistema visual y no de un wireframe. |
| 1.0 | 2026-08-09 | Retroalimentación de la Fase B2 de validación de maqueta del proyecto de código `GeometriaFactory-Web`, **sin subir versión** por `Master-Prompt.md` §5, que lo admite mientras el documento está en estado `Propuesto`. **H-02** (coma contra punto): §2 escribe los cuatro valores `declarado` y `derivado` del bloque de resultado **con punto**, y suma la nota que lo declara deliberado, con la misma redacción que llevan `Wireframes-Vista-De-Trabajo.md` §2 y `Representacion-Lista-De-Observaciones.md` §1. *(La nota se incorporó efectivamente en la ronda de corrección de la auditoría B2: hasta entonces esta entrada la declaraba y §2 no la tenía —hallazgo `AB2-09`—.)* El defecto era interno al propio documento: §2 reformateaba a coma los mismos valores que el bloque de texto del alumno de §2 dibuja con punto, y que §7 prohíbe reformatear. |
| 1.0 | 2026-08-09 | Corrección absorbida de la auditoría `B2-Maqueta-GeometriaFactory-Web-r1.md`, **sin subir versión** por `Master-Prompt.md` §5. **`AB2-09`**: §2 suma la nota que declara deliberado el punto decimal de `declarado` y `derivado`, con la misma redacción que llevan `Wireframes-Vista-De-Trabajo.md` §2 y `Representacion-Lista-De-Observaciones.md` §1. La entrada de control de cambios de la retroalimentación de la Fase B2 declaraba esa nota como incorporada y **la nota no estaba en el documento**: la entrada quedó anotada con esa constancia, para no borrar la traza de la discrepancia. |
