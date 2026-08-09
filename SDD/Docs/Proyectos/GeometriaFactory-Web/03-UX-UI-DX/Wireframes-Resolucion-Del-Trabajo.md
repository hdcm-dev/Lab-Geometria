# Wireframes — Resolución del trabajo

**Proyecto de código:** GeometriaFactory-Web
**Documento:** Wireframes-Resolucion-Del-Trabajo.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** UX/UI Designer + Frontend Lead (AG-03)
**Variante:** UX/UI
**Trazabilidad upstream:** `../02-Especificacion-Funcional/Casos-De-Uso/CU-09-Resolver-Un-Trabajo-Con-Comentario-Opcional.md` íntegro —§4, FA-01 a FA-04, §6, CA-01 a CA-08 y §13—; `../02-Especificacion-Funcional/Casos-De-Uso/CU-07-Abrir-Un-Trabajo-Y-Explorarlo-En-Escena-Y-Arbol.md` FA-01 y FA-04; `../02-Especificacion-Funcional/Especificacion-Funcional.md` §6 (RT-03, RT-06, RT-07, RT-09); `../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-09-Desenlace-Explicito-De-La-Entrega.md` §1, §5 (los siete criterios); `NB-07` §1; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-21, F-23, F-24), §4.1 (RN-04, RN-10), §4.2, §5 (historia 7.1), §6 (flujo 2.1), §7 (CL-10, CL-11), §9 (retiro de X-5); `Design-Rules-Web-Generico.md` §4.4, §4.6, §4.9, §5, §7; `Design-Rules-Blazor-Mudblazor.md` §4, §5
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

> **Esto no es una pantalla. Es un bloque alojado dentro de `Vista-De-Trabajo`.**
> No tiene ruta propia, no tiene dirección propia, no se llega a él navegando y **no
> se construye como archivo de superficie ni como componente de página aparte**. Se
> llega a él abriendo un trabajo. Quien construya el código construye este bloque
> **dentro** de la vista de trabajo, y quien lo maquete lo dibuja ahí.

**Nombre canónico de superficie: `Resolucion-Del-Trabajo`. Es una superficie alojada**, en el sentido que `Glosario-UX.md` §2 le da al término: tiene nombre canónico, mapa de estados propio y lista de interacciones propia, y **por eso se emite como wireframe separado**, pero no tiene ruta. El nombre canónico se reusa sin cambios; lo que no existe es un destino de navegación que lo tenga por objeto.

El administrador le da desenlace a un trabajo en estado `Pendiente` —aprobarlo, con lo que pasa a `Finalizado`, o rechazarlo, con lo que pasa a `Rechazado`—, con un comentario escrito **opcional**, y puede además retirar cualquier trabajo que ve. Es lo que convierte una entrega depositada en una entrega resuelta.

**Dónde vive, con precisión.** Dentro de `Vista-De-Trabajo`, debajo de la cabecera del trabajo y **antes** de las cuatro partes —datos, texto, escena y árbol—, más dos diálogos con flujo propio. La decisión se toma después de mirar, pero el control tiene que estar donde se lo encuentre sin buscarlo. Ninguna entrada de `Experiencia-De-Uso.md` §3.1 lo nombra como destino, y ninguna barra lateral lo ofrece: **si aparece en un mapa de navegación, está mal**.

**Cómo se lo demuestra sin darle ruta.** Una maqueta o una demostración pueden necesitar exhibirlo aislado, con su mapa de estados completo, sin obligar a recorrer el camino del producto. Esa exhibición es un **instrumento de validación** y no crea una pantalla: el camino del producto sigue siendo entrega de la comisión → abrir un trabajo. Si la exhibición aislada se materializa como archivo propio, ese archivo **tiene que dibujar el trabajo entero debajo del bloque** —datos, comentario, observaciones, texto, escena y árbol— y **armarse con el mismo código** que usa la vista de trabajo, para que una corrección no haya que hacerla dos veces. Así lo resolvió la Fase B2 y así quedó aprobado.

**Sólo aparece para el administrador, y sólo mientras el trabajo está en estado `Pendiente`.** En cualquier otro caso el bloque **no se dibuja**.

## 2. Layout

Bloque de decisión, dentro de `Vista-De-Trabajo`:

```text
+----------+----------------------------------------------------------------+
| ...      |  < Volver a la entrega de la comisión                          |
|          |  Cubo y ortoedro          [Pendiente]                          |
|          |  12/08/2026 · Ana Diaz · 3 piezas · 2 advertencias              |
|          |  ------------------------------------------------------------- |
|          |  +----------------------------------------------------------+  |
|          |  |  Resolver esta entrega                                   |  |
|          |  |  Comentario para el alumno (opcional)                    |  |
|          |  |  [__________________________________________________]    |  |
|          |  |  Lo va a ver al abrir el trabajo. Podés dejarlo vacío.   |  |
|          |  |                                                          |  |
|          |  |            [ Rechazar ]      [ Aprobar ]     [ Retirar ] |  |
|          |  +----------------------------------------------------------+  |
|          |  ... las cuatro partes de la vista de trabajo ...              |
+----------+----------------------------------------------------------------+
```

Bloque cuando el trabajo ya tiene desenlace:

```text
   +----------------------------------------------------------+
   |  Resuelto el 13/08/2026 · [Finalizado]                   |
   |  Esta entrega ya tiene desenlace y no se puede cambiar.  |
   |                                            [ Retirar ]   |
   +----------------------------------------------------------+
```

Diálogo de confirmación del desenlace:

```text
   +--------------------------------------------------------+
   |  Rechazar «Cubo y ortoedro»                            |
   |  El trabajo pasa a Rechazado. Es definitivo: no se     |
   |  puede volver atrás. Si Ana quiere corregir, tiene que |
   |  cargar un trabajo nuevo.                              |
   |  Comentario: «Revisá el área del cubo.»                |
   |             [ Cancelar ]      [ Rechazar ]             |
   +--------------------------------------------------------+
```

Diálogo de confirmación del retiro:

```text
   +--------------------------------------------------------+
   |  Retirar «Cubo y ortoedro»                             |
   |  [ banda de atención ]                                 |
   |  El trabajo deja de existir y desaparece también del   |
   |  listado de Ana. No se puede deshacer.                 |
   |             [ Cancelar ]      [ Retirar ]              |
   +--------------------------------------------------------+
```

## 3. Componentes principales

| Componente | Patrón del catálogo | Propósito | Datos que muestra | Comportamiento |
| --- | --- | --- | --- | --- |
| Bloque de decisión | Base §4.4 | Alojar las dos decisiones y el comentario | — | Sólo para el administrador y sólo en estado `Pendiente` |
| Campo de comentario | Base §4.6 | Recibir el texto libre | Lo escrito | **Opcional, sin longitud mínima.** La etiqueta lleva la palabra «opcional» a la vista |
| Nota de destino del comentario | Base §5 | Declarar **dónde lo va a leer el alumno** | Texto fijo | Inerte. Dice que se lee al abrir el trabajo, no en el listado |
| Acción de aprobar | Base §4.9, primaria | Aprobar | Verbo exacto: «Aprobar» | Abre el diálogo de confirmación |
| Acción de rechazar | Base §4.9, secundaria | Rechazar | Verbo exacto: «Rechazar» | Abre el mismo diálogo con el otro valor. **No es un control destructivo**: rechazar es una decisión legítima, no una destrucción |
| Acción de retirar | Base §4.9, destructiva | Eliminar el trabajo | «Retirar» | Color y borde de peligro, **visualmente separada de las dos decisiones**. Abre su propio diálogo |
| Diálogo de confirmación del desenlace | Base §4.4 | **Declarar la terminalidad antes de aplicarla** | Nombre del trabajo, decisión, y el comentario tal como quedó escrito | La confirmación no es escrita: la operación es reversible en el sentido de que el trabajo sigue existiendo |
| Diálogo de confirmación del retiro | Base §4.4 | Evitar el retiro accidental | Nombre del trabajo, y el aviso de que desaparece también del listado del alumno | Aviso en estado de atención |
| Bloque de trabajo resuelto | Base §5 | Declarar el desenlace ya aplicado | Fecha del desenlace y estado alcanzado | **No ofrece las dos decisiones.** La única acción disponible es retirar |

**Lo que esta superficie no dibuja:** ninguna acción de revertir un estado terminal, ningún control para reemplazar un comentario ya registrado, ninguna nota ni escala de calificación, ningún campo obligatorio de motivo.

**El comentario no es una observación y no es una calificación.** Lo escribe una persona, hay a lo sumo uno por trabajo, no lleva nota ni escala, y en la superficie del alumno vive en un bloque propio, separado de lo que emite la interpretación del texto. Lo que sigue excluido del producto es la **calificación**.

## 4. Interacciones

| Acción | Disparador | Resultado esperado | Precondición |
| --- | --- | --- | --- |
| Ver el bloque | Apertura del trabajo | El bloque aparece **sólo** si quien mira es el administrador y el trabajo está en estado `Pendiente` | — |
| Escribir el comentario | Tecleo | Sin ida y vuelta al servidor. **Nunca es requisito para resolver** | — |
| Aprobar | Acción primaria | Se abre el diálogo con la decisión y el comentario tal como quedó escrito | Trabajo en estado `Pendiente` |
| Rechazar | Acción secundaria | **Es la misma solicitud con el otro valor de la decisión.** No hay una superficie distinta para cada una | Ídem |
| Confirmar el desenlace | Acción del diálogo | Se aplica la transición, se muestra el estado alcanzado y se vuelve al listado, donde el trabajo ya figura con su estado nuevo | Diálogo abierto |
| Resolver sin escribir comentario | Confirmar con el campo vacío | El desenlace procede igual. **El estado expresa el desenlace por sí solo** | Ídem |
| Retirar | Acción destructiva | Se abre su diálogo; al confirmar, el trabajo deja de existir y se vuelve al listado | El trabajo es visible para el administrador, **en cualquiera de sus tres estados visibles** |
| Cancelar cualquiera de los dos diálogos | Acción secundaria, tecla de escape o cierre | Nada cambia. El foco vuelve al control que abrió el diálogo. **El comentario escrito se conserva** | — |
| Abrir un trabajo ya resuelto | Apertura | El bloque muestra el desenlace aplicado y **no ofrece las dos decisiones** | El trabajo está en un estado terminal |

**Que la pantalla deje de ofrecer las decisiones no es lo que hace cumplir la regla.** La acotación se verifica forzando la solicitud sin pasar por la pantalla, y quien la hace cumplir es el servicio de datos. Lo que le corresponde a esta superficie es no ofrecer lo que no corresponde y declarar la terminalidad antes de aplicarla.

## 5. Estados

| Estado | Condición que lo produce | Representación esperada |
| --- | --- | --- |
| **Vacío** | **No aplica**: el bloque no presenta ninguna colección | Se declara para que la ausencia sea deliberada |
| **Cargando** | El trabajo se está trayendo | El bloque aparece con el resto de la superficie que lo aloja |
| **Con datos · resoluble** | Administrador y trabajo en estado `Pendiente` | Campo de comentario, dos decisiones y la acción de retirar. **Es la forma que el estado canónico «con datos» toma en esta superficie** |
| **Con datos · no resoluble por papel** | Quien mira es el alumno dueño | **El bloque no se dibuja**, ni siquiera inhabilitado |
| **Con datos · no resoluble por estado** | El trabajo ya tiene desenlace | Bloque de trabajo resuelto: fecha, estado alcanzado y sólo la acción de retirar |
| **Comentario escrito** | Hay texto en el campo | El diálogo de confirmación lo muestra tal como quedó, para que se lea antes de aplicarlo |
| **Comentario vacío** | El campo no se escribió | El desenlace procede igual. **Cero de los dos desenlaces exigen comentario** |
| **Confirmando desenlace** | Diálogo abierto | Diálogo con la decisión, la terminalidad declarada y el comentario. Foco dentro |
| **Aplicando** | La transición está en curso | Acción del diálogo inhabilitada con indicador. **Previene el doble disparo** |
| **Éxito** | La transición se aplicó | Se muestra el estado alcanzado y se vuelve al listado. El bloque **deja de ofrecer las dos decisiones** sobre ese trabajo |
| **Confirmando retiro** | Diálogo de retiro abierto | Aviso en estado de atención que declara que el trabajo desaparece también del listado del alumno |
| **Error · el estado no admite desenlace** | El trabajo no está en estado `Pendiente`: nunca lo estuvo, o ya fue resuelto en otra pestaña | Se declara **el estado actual del trabajo** y se recarga el listado. Terminación controlada: **no hay camino para revertir un estado terminal** |
| **Error · desenlace no ejercido por el administrador** | Quien pide el desenlace no es el administrador | La solicitud no procede. Regreso al panel de quien la pidió con un mensaje neutro |
| **Error · trabajo inexistente o no visible** | El identificador no corresponde a un trabajo que quien pide vea | Mensaje neutro que **no distingue** los casos, y regreso al listado |
| **Indisponible** | El servicio de datos no responde | Aviso de indisponibilidad. **El trabajo conserva el estado que tenía** y se puede reintentar. Ver [`Wireframes-Estado-Degradado-Y-Reconexion.md`](Wireframes-Estado-Degradado-Y-Reconexion.md) |
| **Reconectando** | Se corta el circuito | Cartel de reconexión superpuesto; el bloque permanece con lo escrito |

**Sobre el estado canónico «con datos» en esta superficie.** No tiene una sola forma, porque lo que el bloque presenta depende del papel de quien mira y del estado del trabajo: se materializa en las **tres** filas calificadas de arriba, y las tres son estados que la maqueta de la Fase B2 tiene que demostrar por separado. Se nombran con el estado canónico por delante y su calificador detrás, en lugar de con un nombre propio, para que la correspondencia con el mapa de [`Experiencia-De-Uso.md`](Experiencia-De-Uso.md) §4.2 sea directa y no haya que inferirla.

## 6. Versión angosta

Punto de quiebre principal en 768 px [ASUNCIÓN].

- El bloque conserva su posición debajo de la cabecera, **antes de la escena**: es la primera cosa accionable de la superficie para el administrador y no puede quedar al final de una pila larga.
- Las tres acciones pasan a ancho completo apiladas, en el orden **Aprobar → Rechazar → Retirar**. La primaria arriba, la destructiva abajo y **separada por un espacio mayor**, para que el pulgar no la alcance por inercia.
- El campo de comentario conserva altura para dos o tres líneas y crece con el contenido.
- Los dos diálogos pasan a ocupar el ancho disponible. **La declaración de terminalidad y el aviso de arrastre al listado del alumno no se colapsan en ningún ancho.**
- Legible sin desplazamiento horizontal a 320 px.

## 7. Notas de implementación

**Accesibilidad.** Los dos diálogos toman el foco al abrirse, lo confinan mientras están abiertos y lo devuelven al control que los abrió al cerrarse; se cierran con la tecla de escape. **La declaración de terminalidad se asocia por descripción accesible a la acción que confirma**, para que se anuncie junto con ella y antes de activarla. La etiqueta del campo de comentario lleva la palabra «opcional» **en la etiqueta y no sólo en el texto de apoyo**: un lector de pantalla que anuncie sólo la etiqueta tiene que enterarse igual. El resultado del desenlace se anuncia como región activa. La acción de retirar tiene rótulo accesible que nombra el trabajo, no sólo el verbo. Objetivos de toque de al menos 24×24 px.

**Performance percibida.** El desenlace es una acción puntual: control del diálogo inhabilitado con indicador, sin bloquear la superficie entera.

**Internacionalización.** La fecha del desenlace la produce el sistema y se rotula como tal, distinta de la fecha del trabajo, que la declara el alumno.

**Restricciones de arquitectura.** El desenlace y el retiro salen **desde el servidor de la pieza pública**. La superficie **no deduce el estado alcanzado**: lo muestra tal como lo devolvió el servicio. Ningún mensaje incluye la dirección de un servicio interno. **El listado del alumno no se entera solo** del desenlace: el alumno ve el estado nuevo la próxima vez que pida su listado, y el comentario al abrir el trabajo.

**Concurrencia.** Hay un solo administrador, de modo que no existen dos desenlaces simultáneos sobre el mismo trabajo. El caso que sí colisiona —resolver un trabajo ya resuelto en otra pestaña— lo cierra el error de estado, que declara el estado actual y no aplica nada.

## 8. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Persona objetivo | El docente como administrador. El alumno dueño es beneficiario y no opera acá |
| CU origen | [`CU-09`](../02-Especificacion-Funcional/Casos-De-Uso/CU-09-Resolver-Un-Trabajo-Con-Comentario-Opcional.md) íntegro, alojado en [`CU-07`](../02-Especificacion-Funcional/Casos-De-Uso/CU-07-Abrir-Un-Trabajo-Y-Explorarlo-En-Escena-Y-Arbol.md) FA-01 |
| Reglas de negocio relevantes | `RN-10` (desenlace exclusivo del administrador y terminalidad), `RN-04` (eliminación; cubre hoy también la del administrador sobre cualquier trabajo que ve), `RN-11` |
| Restricciones transversales | `RT-03`, `RT-06`, `RT-07`, `RT-09` |
| Superficie que lo aloja | [`Wireframes-Vista-De-Trabajo.md`](Wireframes-Vista-De-Trabajo.md) |
| Marco aplicado | [`Experiencia-De-Uso.md`](Experiencia-De-Uso.md) §3.7, §4.3, §8.1 |
| Representaciones que invoca | [`Representacion-Fila-De-Trabajo.md`](Representacion-Fila-De-Trabajo.md) §2 para la insignia |
| Catálogo de diseño aplicado | `Design-Rules-Web-Generico.md`, `Design-Rules-Blazor-Mudblazor.md` |
| US a generar en 06 | `US-24`, `US-25` |
| Tests previstos en 08 | Guion de demostración de la etapa `h` completo: recuento de exactamente dos decisiones; aprobación sin comentario; rechazo con comentario leído después por el alumno en bloque aparte; desenlace forzado sobre un estado terminal; desenlace forzado por un alumno; retiro en los tres estados visibles; servicio detenido conservando el estado del trabajo; recorrido por teclado de los dos diálogos |

## 9. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. Bloque de decisión alojado en la vista de trabajo, con su fundamento de ubicación, más dos diálogos con flujo propio. Declara el comentario como opcional en la etiqueta y no sólo en el texto de apoyo, la terminalidad enunciada antes de aplicarse y asociada a la acción que confirma, la separación visual del retiro respecto de las dos decisiones, la enumeración de lo que la superficie no dibuja —incluida la calificación, que sigue excluida— y dieciséis estados declarados para la Fase B2. |
| 1.0 | 2026-08-09 | Correcciones absorbidas del audit `B-02-03-GeometriaFactory-Web-r1.md` (ronda 1), **sin subir versión** por `Master-Prompt.md` §5, que lo admite mientras el documento está en estado `Propuesto`. **H-01**: §5 pasa a nombrar el estado canónico **«con datos»** en las tres filas que lo materializan —«con datos · resoluble», «con datos · no resoluble por papel» y «con datos · no resoluble por estado»—, en lugar del nombre propio «Resoluble», y suma una nota que declara por qué el estado tiene tres formas en esta superficie. Cierra el desajuste con el criterio de `Rules-UX-UI-DX.md` §6 y la contradicción con el mapa de `Experiencia-De-Uso.md` §4.2, que ya lo marcaba presente. **H-06**: las `NB-09` y `NB-07` de la cabecera pasan a citarse con sección y criterio numerado. **H-10**: §4 sustituye una forma desnuda de «pantalla» en el referente de superficie por «superficie». |
| 1.0 | 2026-08-09 | Retroalimentación de la Fase B2 de validación de maqueta del proyecto de código `GeometriaFactory-Web`, **sin subir versión** por `Master-Prompt.md` §5, que lo admite mientras el documento está en estado `Propuesto`. **Refuerzo del enunciado de alojamiento**, motivado por la validación visual: la maqueta construyó este bloque como página suelta pese a que §1 ya declaraba que se aloja dentro de `Vista-De-Trabajo`. El enunciado era correcto pero llegaba tarde y en subordinada, después del propósito y del nombre canónico, y el propio rótulo de la sección dice «Pantalla». §1 pasa a abrir con la negación destacada y dirigida a quien construya el código, califica el nombre canónico como **superficie alojada** —término que `Glosario-UX.md` §2 acuña en esta misma pasada—, precisa que ninguna entrada de `Experiencia-De-Uso.md` §3.1 lo nombra como destino, y declara cómo se lo puede exhibir aislado sin darle ruta: como instrumento de validación, con el trabajo entero debajo y armado con el mismo código que la vista de trabajo. No cambia ninguna decisión de diseño: cierra la lectura que la validación demostró posible. |
