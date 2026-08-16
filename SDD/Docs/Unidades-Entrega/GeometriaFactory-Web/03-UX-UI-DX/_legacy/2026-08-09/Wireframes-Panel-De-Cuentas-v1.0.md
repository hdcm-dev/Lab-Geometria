# Wireframes — Panel de cuentas

**Proyecto de código:** GeometriaFactory-Web
**Documento:** Wireframes-Panel-De-Cuentas.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** UX/UI Designer + Frontend Lead (AG-03)
**Variante:** UX/UI
**Trazabilidad upstream:** `../02-Especificacion-Funcional/Casos-De-Uso/CU-04-Administrar-Las-Cuentas-De-La-Comision.md` §4, FA-01, FA-02, FA-05, §6, CA-02 a CA-07 y §13; `../02-Especificacion-Funcional/Especificacion-Funcional.md` §6 (RT-03, RT-06, RT-07, RT-09); `../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-01-Control-De-Admision-Al-Laboratorio.md` §1, §5 (los cinco criterios); `NB-02` §5 (tercer criterio); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-03), §4.1 (RN-01, RN-06, RN-07), §6 (flujo 1), §7 (CL-6, CL-7), §9 (X-3), §11 (RN-B6), §17.6 P.5; `Design-Rules-Web-Generico.md` §3, §4.3, §4.8, §4.9, §4.10, §5, §7, §8; `Design-Rules-Primer-Arranque.md` §4.4 y §4.6; `Design-Rules-Blazor-Mudblazor.md` §4, §5
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

**Nombre canónico de superficie: `Panel-De-Cuentas`.**

El administrador ve la lista de cuentas de su comisión con su situación y ejerce sobre cada una **exactamente cuatro operaciones**: habilitar, bloquear, rehabilitar y dar de baja. Es donde se controla quién entra al laboratorio, sin depender de ningún canal de correo.

Es además el **destino al completar el aprovisionamiento**, declarado explícitamente y no dejado en la portada por omisión: es la primera cosa que el docente necesita hacer con el laboratorio recién configurado. Por eso aloja, sólo en ese momento, la **orientación posterior**.

La operación de mayor consecuencia del producto vive acá: la **baja física**, que elimina la cuenta **y todos sus trabajos**. El diseño no la impide —es una operación legítima— y hace lo que sí le corresponde: **hacerla difícil de ejecutar por accidente** y declarar su consecuencia antes de que ocurra.

## 2. Layout

Shell de trabajo con la barra lateral del administrador.

```text
+----------+----------------------------------------------------------------+
| Laborat. |  Cuentas de la comisión                                        |
|          |  Habilitá las cuentas que se registran y controlá el acceso.   |
| ·Entrega |  ------------------------------------------------------------- |
|  de la   |  [ buscar por correo o nombre ]  [ situación: todas v ]        |
|  comisión| --------------------------------------------------------------|
| ·Cuentas |  CUENTA              CORREO            SITUACIÓN   REGISTRO  ...|
| ·Mi      | --------------------------------------------------------------|
|  contra- |  (AD) Ana Diaz       ana@ej.test       [Pendiente]  05/08  [ok]|
|  seña    |                                                            [x] |
|          |  (BL) Beto Lopez     beto@ej.test      [Habilitada] 03/08 [blq]|
| -------- |                                                            [x] |
| Docente  |  (CM) Cora Mera      cora@ej.test      [Bloqueada]  02/08  [ok]|
| [Cerrar] |                                                            [x] |
| v1.4.2   | --------------------------------------------------------------|
+----------+----------------------------------------------------------------+
```

Diálogo de baja, con **confirmación escrita**:

```text
   +--------------------------------------------------------+
   |  Dar de baja la cuenta de Ana Diaz                     |
   |                                                        |
   |  [ banda de atención ]                                 |
   |  Esta baja elimina la cuenta y también TODOS sus       |
   |  trabajos. No se puede deshacer y no hay forma de      |
   |  recuperarlos.                                         |
   |                                                        |
   |  Para confirmar, escribí el correo de la cuenta:       |
   |  ana@ej.test                                           |
   |  [________________________________]                    |
   |                                                        |
   |             [ Cancelar ]   [ Dar de baja ]             |
   +--------------------------------------------------------+
```

Orientación posterior, sólo la primera vez, sobre el mismo shell:

```text
   [ banda de confirmación  rol=estado ]
   Tu cuenta de administrador quedó creada. El laboratorio ya está en marcha.

   +----------------+  +----------------+  +----------------+
   | [ico] Cuentas  |  | [ico] Entrega  |  | [ico] Mi clave |
   | Habilitá a los |  | Recorré los    |  | Cambiá tu      |
   | alumnos que se |  | trabajos de la |  | contraseña     |
   | registren.     |  | comisión.      |  | cuando quieras.|
   | [ Abrir ]      |  | [ Abrir ]      |  | [ Abrir ]      |
   +----------------+  +----------------+  +----------------+
```

## 3. Componentes principales

| Componente | Patrón del catálogo | Propósito | Datos que muestra | Comportamiento |
| --- | --- | --- | --- | --- |
| Encabezado de la superficie | Base §4.3 | Nombrar la superficie | Título y subtítulo de una línea | El título es el encabezado de primer nivel |
| Barra de filtros | Base §4.10 | Acotar la lista | Búsqueda por correo o nombre, selector de situación | **Filtra sobre lo ya recibido. No consulta al servicio de datos** |
| Fila de cuenta | Base §4.3 | Presentar una cuenta y sus operaciones | Iniciales, nombre, apellido, correo, insignia de situación, fecha de registro | Acciones por fila alineadas a la derecha |
| Iniciales de la cuenta | Base §6.3 | Identificar de un vistazo | Dos letras sobre círculo con tinte | Vectorial. **No hay foto y no hace falta** |
| Insignia de situación | Base §4.8 | Declarar la situación de la cuenta | Uno de los tres valores, **siempre con texto** | El color es refuerzo. Se llama «situación» y no «estado», para no colisionar con el estado del trabajo |
| Acción de situación | Base §4.3 | Habilitar, bloquear o rehabilitar | Verbo exacto según la situación vigente | **Se ofrece la transición que la situación admite**, no las tres a la vez |
| Acción de baja | Base §4.3, destructiva | Dar de baja | Ícono con rótulo accesible propio | Color y borde de peligro. Abre el diálogo de confirmación escrita |
| Diálogo de confirmación escrita | Primer arranque §4.4, Base §4.4 | **Hacer la baja difícil de ejecutar por accidente** | Nombre de la cuenta, el correo a transcribir | La acción destructiva permanece inhabilitada hasta que lo escrito coincide |
| Aviso de arrastre | Base §5 | **Declarar que la baja elimina también los trabajos** | Texto fijo, en estado de atención | **En el mismo lugar donde se pide la confirmación**, no en otra superficie |
| Orientación posterior | Primer arranque §4.6 | Sugerir los pasos siguientes tras el aprovisionamiento | Tres tarjetas de acceso | **Orienta, no bloquea.** No es un asistente ni una lista de tareas con progreso |
| Banda de confirmación | Primer arranque §4.4 | Acusar recibo del aprovisionamiento | Qué quedó creado | Rol de estado. Aparece una sola vez |

**Lo que esta superficie no dibuja:** ninguna acción de crear una segunda cuenta de administrador, ningún selector de papel o de permisos, ningún restablecimiento de contraseña de un alumno. El producto tiene **dos papeles fijos y un solo administrador**, y no hay canal para restablecer credenciales ajenas.

## 4. Interacciones

| Acción | Disparador | Resultado esperado | Precondición |
| --- | --- | --- | --- |
| Abrir la superficie | Destino «Cuentas», o llegada desde el aprovisionamiento | Se pide la lista de cuentas | Sesión de administrador |
| Habilitar una cuenta a la espera | Acción de situación | La situación cambia. A partir de ahí el alumno puede ingresar y establecer su contraseña | La situación lo admite |
| Bloquear una cuenta habilitada | Acción de situación | La situación cambia. **La sesión ya establecida no se corta desde acá**: el efecto se hace visible en la siguiente solicitud que esa sesión emita | Ídem |
| Rehabilitar una cuenta bloqueada | Acción de situación | La situación cambia | Ídem |
| Dar de baja | Acción destructiva | Se abre el diálogo de confirmación escrita | — |
| Escribir la confirmación | Tecleo en el diálogo | La acción destructiva se habilita **sólo** cuando lo escrito coincide con el correo de la cuenta | Diálogo abierto |
| Confirmar la baja | Acción destructiva del diálogo | La cuenta y **todos sus trabajos** dejan de existir. Se vuelve a pedir la lista | La confirmación coincide |
| Cancelar la baja | Acción secundaria, tecla de escape o cierre | Nada cambia. El foco vuelve al control que abrió el diálogo | — |
| Filtrar o buscar | Tecleo o selección | La lista recibida se acota. **Sin ida y vuelta al servidor** | — |
| Abrir una tarjeta de la orientación posterior | Activación | Navega al destino sugerido. **Ninguna es obligatoria** | Sólo tras el aprovisionamiento |

**Por qué la situación se actualiza con lo que devolvió el servicio y no con lo que la pantalla supone.** La fila se repinta con la situación que el servicio devolvió, no con la que el navegador dedujo del verbo del control. Es la aplicación de la regla de que no hay optimismo de interfaz, y evita listas que muestran una situación que el sistema no tiene.

## 5. Estados

| Estado | Condición que lo produce | Representación esperada |
| --- | --- | --- |
| **Vacío** | No hay ninguna cuenta de alumno todavía | Ilustración neutra y texto que explica que las cuentas aparecen cuando los alumnos se registran, **sin ofrecer una acción de crear**: el administrador no crea cuentas de alumno |
| **Cargando** | La lista está en camino | Esqueleto por fila. **Nunca una tabla vacía mientras carga** |
| **Con datos** | Hay cuentas | Filas con su insignia y su acción de situación |
| **Filtrado sin resultados** | El filtro no deja ninguna fila | Estado vacío de filtro con la acción de limpiarlo. **Distinto del vacío de colección** |
| **Aplicando un cambio de situación** | La operación está en curso | Acción de esa fila inhabilitada con indicador. **Previene el doble disparo** |
| **Confirmación escrita pendiente** | Diálogo abierto y el campo todavía no coincide | La acción destructiva permanece inhabilitada. El aviso de arrastre a la vista |
| **Confirmación no coincidente** | Se confirma con un correo que no es el de la cuenta | **La baja no procede.** Se informa y se deja reintentar con la confirmación correcta |
| **Ejecutando la baja** | La baja está en curso | Acción del diálogo inhabilitada con indicador |
| **Éxito** | La operación se aplicó | Confirmación sutil y lista vuelta a pedir. Tras una baja, la cuenta ya no figura y **sus trabajos no figuran en ningún listado del laboratorio** |
| **Error de operación · cuenta inexistente** | La cuenta sobre la que se opera ya no existe | Se informa y se recarga la lista. Recuperación por reintento sobre la lista actualizada |
| **Error de operación · administrador ya configurado** | Se intenta configurar una segunda cuenta de administrador | Se informa que ya existe y se deriva a `Ingreso`. **Terminación controlada: no hay camino alternativo** |
| **Orientación posterior** | Primera llegada tras el aprovisionamiento | Banda de confirmación y grilla de tres tarjetas de acceso. **No bloquea nada** |
| **Indisponible** | El servicio de datos no responde | Aviso de indisponibilidad en lugar de la lista. **La lista no se muestra con datos viejos.** Ver [`Wireframes-Estado-Degradado-Y-Reconexion.md`](Wireframes-Estado-Degradado-Y-Reconexion.md) |
| **Reconectando** | Se corta el circuito | Cartel de reconexión superpuesto; la lista permanece a la vista |

## 6. Versión angosta

Punto de quiebre principal en 768 px [ASUNCIÓN].

- **Las filas pasan a tarjetas apiladas**: iniciales y nombre arriba, correo y fecha de registro debajo, insignia de situación destacada, acciones al pie con al menos 24×24 px de objetivo.
- **La acción de baja se mantiene visualmente separada** de la de situación, con su color de peligro: en pantalla angosta el riesgo de disparo accidental es mayor, y la confirmación escrita es la que finalmente lo evita.
- El diálogo de baja pasa a ocupar el ancho disponible. **El aviso de arrastre no se colapsa ni se recorta en ningún ancho.**
- Las tres tarjetas de la orientación posterior se apilan.
- La barra de filtros se apila.
- La barra lateral colapsa según el patrón del documento base.
- Legible sin desplazamiento horizontal a 320 px.

## 7. Notas de implementación

**Accesibilidad.** Cada acción por fila declara **sobre qué cuenta actúa**, no sólo su verbo: sin eso, la lista suena como una sucesión de «habilitar, dar de baja» indistinguibles. El diálogo de baja toma el foco al abrirse, lo confina mientras está abierto y lo devuelve al control que lo abrió al cerrarse; se cierra con la tecla de escape. **El aviso de arrastre se asocia por descripción accesible al campo de confirmación**, de modo que se anuncie antes de que la persona escriba. Las tres insignias de situación llevan texto. El resultado de cada operación se anuncia como región activa. Objetivos de toque de al menos 24×24 px.

**Performance percibida.** Esqueleto por fila por encima de 400 ms. El cambio de situación es puntual: control inhabilitado con indicador dentro de la propia fila, sin bloquear la lista entera.

**Internacionalización.** «Situación» y no «estado» para la cuenta, por decisión de vocabulario aguas arriba. Fecha de registro producida por el sistema, rotulada como tal.

**Restricciones de arquitectura.** La lista y las cuatro operaciones salen **desde el servidor de la pieza pública**. El filtro es local y **no origina ninguna petición**. La pieza pública **no guarda copia de la lista** entre operaciones: cada recorrido vuelve a pedirla. Ningún mensaje incluye la dirección de un servicio interno. La confirmación escrita **acota lo que se ofrece**; el arrastre de trabajos es una invariante del dominio y no algo que esta superficie ejecute.

## 8. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Persona objetivo | El docente como administrador |
| CU origen | [`CU-04`](../../../02-Especificacion-Funcional/Casos-De-Uso/CU-10004-Administrar-Las-Cuentas-De-La-Comision.md) flujo principal, FA-01, FA-02 y FA-05. Su FA-03 y FA-04 viven en [`Wireframes-Aprovisionamiento-Inicial.md`](Wireframes-Aprovisionamiento-Inicial.md) |
| Reglas de negocio relevantes | `RN-01` (administrador único y papeles fijos), `RN-06` (cuenta pendiente o bloqueada sin acceso), `RN-07` (baja con arrastre y confirmación escrita), `RN-02` |
| Restricciones transversales | `RT-03`, `RT-06`, `RT-07`, `RT-09` |
| Marco aplicado | [`Experiencia-De-Uso.md`](Experiencia-De-Uso.md) §3.2, §3.3, §3.4, §4.1, §8 |
| Representaciones que invoca | [`Representacion-Sello-De-Version.md`](Representacion-Sello-De-Version.md) |
| Catálogo de diseño aplicado | `Design-Rules-Web-Generico.md`, `Design-Rules-Primer-Arranque.md` §4.4 y §4.6, `Design-Rules-Blazor-Mudblazor.md` |
| US a generar en 06 | `US-08`, `US-09`, `US-10` |
| Tests previstos en 08 | Guion de demostración de la etapa `d`: recuento de exactamente cuatro operaciones; habilitación que desbloquea el establecimiento de contraseña; baja con confirmación que no coincide; lectura del aviso de arrastre antes de escribir; baja que elimina la cuenta y sus dos trabajos; ruta de cuentas pedida por un alumno con sesión; recorrido por teclado del diálogo |

## 9. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. Lista de cuentas con las cuatro operaciones, insignia de situación con vocabulario propio para no colisionar con el estado del trabajo, diálogo de baja con confirmación escrita y aviso de arrastre en el mismo lugar donde se pide la confirmación, orientación posterior al aprovisionamiento que sugiere sin bloquear, enumeración de lo que la superficie no dibuja por los dos papeles fijos y el administrador único, y catorce estados declarados para la Fase B2. |
| 1.0 | 2026-08-09 | Correcciones absorbidas del audit `B-02-03-GeometriaFactory-Web-r1.md` (ronda 1), **sin subir versión** por `Master-Prompt.md` §5, que lo admite mientras el documento está en estado `Propuesto`. **H-06**: las `NB-01` y `NB-02` de la cabecera pasan a citarse con sección y criterio numerado. **H-10**: §3 sustituye una forma desnuda de «pantalla» en el referente de superficie, que `Glosario-UX.md` §4 prohíbe, por «superficie». Las demás ocurrencias de la palabra en el documento designan el dispositivo o la interfaz como capa y **no se tocan**, por `Vocabulario-Rules.md` §9.1. |
