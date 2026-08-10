# Wireframes — Panel de cuentas

**Proyecto de código:** GeometriaFactory-Web
**Documento:** Wireframes-Panel-De-Cuentas.md
**Versión:** 1.6
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** UX/UI Designer + Frontend Lead (AG-03)
**Variante:** UX/UI
**Trazabilidad upstream:** `../02-Especificacion-Funcional/Casos-De-Uso/CU-04-Administrar-Las-Cuentas-De-La-Comision.md` §4, FA-01, FA-02, **FA-06**, **FA-07**, FA-05, §6, CA-02 a **CA-10** y §13; `../02-Especificacion-Funcional/Especificacion-Funcional.md` §6 (RT-02, RT-03, RT-06, RT-07, RT-09, **RT-12**); `../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-01-Control-De-Admision-Al-Laboratorio.md` §1, §5 (los cinco criterios); `NB-02` §5 (tercer criterio); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.14**, §4 (F-03, **F-26**), §4.1 (RN-01, RN-06, RN-07, **RN-12**, **RN-13**), §6 (flujo 1), §7 (CL-6, **CL-7 reescrito**), §9 (X-3, **X-2 retirada**), §11 (**RN-B6 tachado** el 2026-08-09 por el intake 1.10, porque F-26 dejó sin objeto su mitigación; lo que sostenía vive en §7 CL-6), §17.1.P.2 (**INV-09**), §17.6 P.5; `Design-Rules-Web-Generico.md` §3, §4.3, §4.8, §4.9, §4.10, §5, §7, §8; `Design-Rules-Primer-Arranque.md` §4.4 y §4.6; `Design-Rules-Blazor-Mudblazor.md` §4, §5
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

El administrador ve la lista de cuentas de su comisión con su situación y ejerce sobre cada una **exactamente cinco operaciones**: habilitar, bloquear, rehabilitar, **resetear la contraseña** y dar de baja. Es donde se controla quién entra al laboratorio, sin depender de ningún canal de correo.

Es además el **destino al completar el aprovisionamiento**, declarado explícitamente y no dejado en la portada por omisión: es la primera cosa que el docente necesita hacer con el laboratorio recién configurado. Por eso aloja, sólo en ese momento, la **orientación posterior**.

La operación de mayor consecuencia del producto vive acá: la **baja física**, que elimina la cuenta **y todos sus trabajos**. El diseño no la impide —es una operación legítima— y hace lo que sí le corresponde: **hacerla difícil de ejecutar por accidente** y declarar su consecuencia antes de que ocurra.

Y acá vive también la operación que existe **para que la baja deje de usarse por el motivo equivocado**: el **reseteo de contraseña** (F-26). Hasta el `PRODUCT-INTAKE` 1.6, un alumno que olvidaba su clave sólo volvía al laboratorio por baja y alta nueva, y eso le costaba todos sus trabajos. **Las dos operaciones tienen que verse distintas, y esta superficie lo trata como requisito y no como matiz**: la destructiva es de peligro y exige transcribir el correo; la del reseteo es de acción normal, pide una confirmación simple y **declara en el mismo lugar que no se pierde ningún trabajo**.

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
|  seña    |                                                     [key] [x] |
|          |  (BL) Beto Lopez     beto@ej.test      [Habilitada] 03/08 [blq]|
| -------- |                                                     [key] [x] |
| Docente  |  (CM) Cora Mera      cora@ej.test      [Bloqueada]  02/08  [ok]|
| [Cerrar] |                                                     [key] [x] |
| v1.4.2   | --------------------------------------------------------------|
+----------+----------------------------------------------------------------+
[ok] = acción de situación · [key] = resetear la contraseña · [x] = dar de baja
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

Diálogo de reseteo de contraseña, **con confirmación simple y sin transcripción**:

```text
   +--------------------------------------------------------+
   |  Resetear la contraseña de Ana Diaz                     |
   |                                                        |
   |  El sistema le va a generar una contraseña provisoria. |
   |  Tenés que dársela vos: el laboratorio no manda correos.|
   |                                                        |
   |  [ banda informativa ]                                  |
   |  Ana conserva su cuenta y TODOS sus trabajos. Va a      |
   |  tener que elegir una contraseña nueva la primera vez   |
   |  que entre, y esa no la vas a saber vos.                |
   |                                                        |
   |             [ Cancelar ]   [ Resetear ]                 |
   +--------------------------------------------------------+
```

Resultado del reseteo, **la única vez que la provisoria se ve**:

```text
   +--------------------------------------------------------+
   |  Contraseña provisoria de Ana Diaz                      |
   |                                                        |
   |  Dictásela, o pasásela por el canal que uses con la     |
   |  comisión. No se vuelve a mostrar.                      |
   |                                                        |
   |     +-------------------------------+                   |
   |     |  7fk2-qm93-tt        [copiar] |                   |
   |     +-------------------------------+                   |
   |                                                        |
   |  Ana la va a tener que cambiar apenas entre.            |
   |                        [ Listo ]                        |
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
| Acción de reseteo | Base §4.3 | **Resetear la contraseña** de esa cuenta | Ícono con rótulo accesible propio que nombra la cuenta | **Acción normal, no destructiva y no de peligro**: no elimina nada. Se ubica **antes** de la de baja y separada de ella. Se ofrece sobre toda cuenta de alumno con contraseña ya establecida, **cualquiera sea su situación**: resetear no cambia la situación y no la exige, de modo que condicionar la acción a que la cuenta esté habilitada obligaría al docente a recordar una secuencia. Sobre la que todavía no estableció contraseña no se dibuja, porque ahí sí no hay nada que resetear |
| Diálogo de reseteo | Base §4.4 | Evitar el disparo accidental **sobre la fila equivocada** | Nombre de la cuenta, qué va a pasar y qué tiene que hacer el docente después | Confirmación **simple**: dos acciones y ninguna transcripción. Exigir el correo acá desalentaría la operación que el producto quiere que se use, y **no protege nada**, porque no hay pérdida que evitar |
| Aviso de conservación | Base §5 | **Declarar que el reseteo no elimina ningún trabajo** | Texto fijo, en estado informativo y **no** de atención | Es la contracara exacta del aviso de arrastre de la baja, y por eso comparte lugar y forma pero **no color**: uno advierte una pérdida y el otro descarta una pérdida que la persona podría suponer |
| Comunicación de la provisoria | Base §4.4 | Entregarle al administrador **lo único que tiene que transmitir** | La contraseña provisoria, con acción de copiado en un solo gesto | **Se muestra una sola vez y se declara que no se vuelve a mostrar.** No se envía por ningún canal: el laboratorio no tiene correo, y quien la comunica es la persona |
| Acción de baja | Base §4.3, destructiva | Dar de baja | Ícono con rótulo accesible propio | Color y borde de peligro. Abre el diálogo de confirmación escrita |
| Diálogo de confirmación escrita | Primer arranque §4.4, Base §4.4 | **Hacer la baja difícil de ejecutar por accidente** | Nombre de la cuenta, el correo a transcribir | La acción destructiva permanece inhabilitada hasta que lo escrito coincide |
| Aviso de arrastre | Base §5 | **Declarar que la baja elimina también los trabajos** | Texto fijo, en estado de atención | **En el mismo lugar donde se pide la confirmación**, no en otra superficie |
| Orientación posterior | Primer arranque §4.6 | Sugerir los pasos siguientes tras el aprovisionamiento | Tres tarjetas de acceso | **Orienta, no bloquea.** No es un asistente ni una lista de tareas con progreso |
| Banda de confirmación | Primer arranque §4.4 | Acusar recibo del aprovisionamiento | Qué quedó creado | Rol de estado. Aparece una sola vez |

**Lo que esta superficie no dibuja:** ninguna acción de crear una segunda cuenta de administrador, ningún selector de papel o de permisos, y **ningún campo donde el administrador escriba la contraseña de un alumno**. El producto tiene **dos papeles fijos y un solo administrador**; y la provisoria del reseteo **la produce el servicio y la superficie sólo la muestra una vez**, que era una decisión derivada de [`CU-04`](../02-Especificacion-Funcional/Casos-De-Uso/CU-04-Administrar-Las-Cuentas-De-La-Comision.md) §10 y que **el Product Owner ratificó**, con el mismo fundamento: si la escribe el docente, termina siendo la misma clave para toda la comisión.

**Lo que esta superficie tampoco vuelve a mostrar**: la contraseña provisoria, una vez cerrada la confirmación. Y **nunca** la contraseña que el alumno elija después, que el administrador no conoce y no va a conocer (RN-13).

## 4. Interacciones

| Acción | Disparador | Resultado esperado | Precondición |
| --- | --- | --- | --- |
| Abrir la superficie | Destino «Cuentas», o llegada desde el aprovisionamiento | Se pide la lista de cuentas | Sesión de administrador |
| Habilitar una cuenta a la espera | Acción de situación | La situación cambia y **la superficie muestra la contraseña provisoria para comunicarla**, una sola vez, con el mismo tratamiento que la del reseteo (**RN-16**, RN-14): el panel **no lleva campo de contraseña**. A partir de ahí el alumno ingresa con esa provisoria y la cambia | La situación lo admite |
| Bloquear una cuenta habilitada | Acción de situación | La situación cambia. **La sesión ya establecida no se corta desde acá**: el efecto se hace visible en la siguiente solicitud que esa sesión emita | Ídem |
| Rehabilitar una cuenta bloqueada | Acción de situación | La situación cambia | Ídem |
| Resetear la contraseña | Acción de reseteo de la fila | Se abre el diálogo de reseteo, que declara qué va a pasar y **que no se pierde ningún trabajo** | **Cualquier cuenta de alumno, en cualquiera de las tres situaciones y tenga o no contraseña**: desde `PRODUCT-INTAKE` 1.13 el reseteo sobre una cuenta sin contraseña **procede** y la fija |
| Confirmar el reseteo | Acción primaria del diálogo | La contraseña de esa cuenta pasa a ser una provisoria **que produce el servicio**, **la cuenta y todos sus trabajos se conservan** y la superficie muestra la provisoria para comunicarla. La lista vuelve a pedirse y la fila sigue en la misma situación, **sea cual sea** | Diálogo abierto |
| Copiar la provisoria | Acción del bloque de comunicación | Queda copiada en un solo gesto, con acuse visible y anunciado | Reseteo aplicado |
| Cerrar la comunicación de la provisoria | «Listo», tecla de escape o cierre | **La provisoria no se vuelve a mostrar.** El foco vuelve a la fila de la cuenta | Ídem |
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
| **Confirmación de reseteo pendiente** | Diálogo de reseteo abierto | Dos acciones, sin campo de transcripción, con el aviso de conservación a la vista |
| **Ejecutando el reseteo** | El reseteo está en curso | Acción del diálogo inhabilitada con indicador |
| **Provisoria a la vista** | El reseteo se aplicó | Bloque con la contraseña provisoria y su acción de copiado, con el aviso de que **no se vuelve a mostrar**. La fila conserva su situación |
| **Reseteo no admitido** | Se pidió resetear una cuenta que no lo admite: la que todavía no estableció contraseña, o la del propio administrador. **La cuenta no habilitada dejó de estar en esta lista**: su reseteo procede | Se informa el motivo y **el camino que sí existe**: esperar a que el alumno establezca la suya, o «Mi contraseña» para la propia. No se ofrece un camino que no existe |
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
- **La acción de baja se mantiene visualmente separada** de la de situación y **de la de reseteo**, con su color de peligro: en pantalla angosta el riesgo de disparo accidental es mayor, y la confirmación escrita es la que finalmente lo evita. **La de reseteo no toma color de peligro en ningún ancho**: no es destructiva, y teñirla de rojo enseñaría a temerle a la operación que evita la pérdida.
- El diálogo de reseteo y el bloque de la provisoria pasan a ocupar el ancho disponible. **El aviso de conservación y la advertencia de que la provisoria no se vuelve a mostrar no se colapsan ni se recortan en ningún ancho.**
- El diálogo de baja pasa a ocupar el ancho disponible. **El aviso de arrastre no se colapsa ni se recorta en ningún ancho.**
- Las tres tarjetas de la orientación posterior se apilan.
- La barra de filtros se apila.
- La barra lateral colapsa según el patrón del documento base.
- Legible sin desplazamiento horizontal a 320 px.

## 7. Notas de implementación

**Accesibilidad.** Cada acción por fila declara **sobre qué cuenta actúa**, no sólo su verbo: sin eso, la lista suena como una sucesión de «habilitar, resetear la contraseña, dar de baja» indistinguibles. El diálogo de baja y el de reseteo toman el foco al abrirse, lo confinan mientras están abiertos y lo devuelven al control que los abrió al cerrarse; se cierran con la tecla de escape. **La contraseña provisoria se anuncia como texto y es seleccionable**: quien usa lector de pantalla tiene que poder oírla carácter por carácter, y el copiado no puede ser el único camino. **El aviso de arrastre se asocia por descripción accesible al campo de confirmación**, de modo que se anuncie antes de que la persona escriba. Las tres insignias de situación llevan texto. El resultado de cada operación se anuncia como región activa. Objetivos de toque de al menos 24×24 px.

**Performance percibida.** Esqueleto por fila por encima de 400 ms. El cambio de situación es puntual: control inhabilitado con indicador dentro de la propia fila, sin bloquear la lista entera.

**Internacionalización.** «Situación» y no «estado» para la cuenta, por decisión de vocabulario aguas arriba. Fecha de registro producida por el sistema, rotulada como tal.

**Restricciones de arquitectura.** La lista y las **cinco** operaciones salen **desde el servidor de la pieza pública**. **La contraseña provisoria no la produce el navegador ni la escribe el administrador**: llega desde el servicio en la respuesta del reseteo, se muestra una vez y **no se escribe en el almacenamiento del navegador** (RT-02, y CU-04 §10 para la decisión derivada). El filtro es local y **no origina ninguna petición**. La pieza pública **no guarda copia de la lista** entre operaciones: cada recorrido vuelve a pedirla. Ningún mensaje incluye la dirección de un servicio interno. La confirmación escrita **acota lo que se ofrece**; el arrastre de trabajos es una invariante del dominio y no algo que esta superficie ejecute.

## 8. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Persona objetivo | El docente como administrador |
| CU origen | [`CU-04`](../02-Especificacion-Funcional/Casos-De-Uso/CU-04-Administrar-Las-Cuentas-De-La-Comision.md) flujo principal, FA-01, FA-02, **FA-06**, **FA-07** y FA-05. Su FA-03 y FA-04 viven en [`Wireframes-Aprovisionamiento-Inicial.md`](Wireframes-Aprovisionamiento-Inicial.md) |
| Reglas de negocio relevantes | `RN-01` (administrador único y papeles fijos), `RN-06` (cuenta pendiente o bloqueada sin acceso), `RN-07` (baja con arrastre y confirmación escrita), `RN-02`, **`RN-12`** (el reseteo conserva la cuenta y sus trabajos) y **`RN-13`** (la provisoria confina hasta que se cambie), las dos del `PRODUCT-INTAKE` 1.7 §4.1 |
| Restricciones transversales | `RT-02`, `RT-03`, `RT-06`, `RT-07`, `RT-09`, **`RT-12`** |
| Marco aplicado | [`Experiencia-De-Uso.md`](Experiencia-De-Uso.md) §3.2, §3.3, §3.4, §4.1, §8 |
| Representaciones que invoca | [`Representacion-Sello-De-Version.md`](Representacion-Sello-De-Version.md) |
| Catálogo de diseño aplicado | `Design-Rules-Web-Generico.md`, `Design-Rules-Primer-Arranque.md` §4.4 y §4.6, `Design-Rules-Blazor-Mudblazor.md` |
| US a generar en 06 | `US-08`, `US-09`, `US-10`, `US-30` |
| Tests previstos en 08 | Guion de demostración de la etapa `d`: recuento de exactamente **cinco** operaciones; **habilitación que muestra la provisoria una sola vez y sin campo de contraseña en el panel**; reseteo que conserva la cuenta y sus tres trabajos con sus estados y comentarios; provisoria mostrada una sola vez; comparación de las dos confirmaciones, que verifica que la del reseteo no exige transcripción y no declara pérdida de trabajos; habilitación que desbloquea el establecimiento de contraseña; baja con confirmación que no coincide; lectura del aviso de arrastre antes de escribir; baja que elimina la cuenta y sus dos trabajos; ruta de cuentas pedida por un alumno con sesión; recorrido por teclado del diálogo |

## 9. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. Lista de cuentas con las cuatro operaciones, insignia de situación con vocabulario propio para no colisionar con el estado del trabajo, diálogo de baja con confirmación escrita y aviso de arrastre en el mismo lugar donde se pide la confirmación, orientación posterior al aprovisionamiento que sugiere sin bloquear, enumeración de lo que la superficie no dibuja por los dos papeles fijos y el administrador único, y catorce estados declarados para la Fase B2. |
| 1.0 | 2026-08-09 | Correcciones absorbidas del audit `B-02-03-GeometriaFactory-Web-r1.md` (ronda 1), **sin subir versión** por `Master-Prompt.md` §5, que lo admite mientras el documento está en estado `Propuesto`. **H-06**: las `NB-01` y `NB-02` de la cabecera pasan a citarse con sección y criterio numerado. **H-10**: §3 sustituye una forma desnuda de «pantalla» en el referente de superficie, que `Glosario-UX.md` §4 prohíbe, por «superficie». Las demás ocurrencias de la palabra en el documento designan el dispositivo o la interfaz como capa y **no se tocan**, por `Vocabulario-Rules.md` §9.1. |
| 1.1 | 2026-08-09 | **Propagación del `PRODUCT-INTAKE` 1.7**, capacidad **F-26** con sus reglas RN-12 y RN-13 y el invariante INV-09. **§1**: las operaciones pasan de cuatro a **cinco** y se declara por qué el reseteo y la baja **tienen que verse distintas**, que es requisito y no matiz. **§2**: la fila suma la acción de reseteo con su leyenda, y se agregan los dos esquemas nuevos —el diálogo de reseteo, con confirmación simple y sin transcripción, y la comunicación de la provisoria, que se muestra una sola vez—. **§3**: cuatro componentes nuevos, y la enumeración de lo que la superficie no dibuja **se corrige**: decía que no dibuja «ningún restablecimiento de contraseña de un alumno», que 1.7 volvió falso, y pasa a declarar que no dibuja ningún campo donde el administrador escriba una contraseña ajena. **§4**: cuatro interacciones nuevas. **§5**: cuatro estados nuevos, incluido el de reseteo no admitido con el camino que sí existe. **§6**: la acción de reseteo **no toma color de peligro en ningún ancho**, con su motivo. **§7**: accesibilidad de la provisoria, que se anuncia como texto y es seleccionable, y la restricción de que no la produce el navegador ni se escribe en su almacenamiento. **§8**: RN-12, RN-13 y RT-12 nuevas. Sube minor: agrega una operación a la superficie, sin cambiar ninguna de las cuatro existentes. |
| 1.2 | 2026-08-09 | **Absorbe dos decisiones del Product Owner sobre F-26**, que [`CU-04`](../02-Especificacion-Funcional/Casos-De-Uso/CU-04-Administrar-Las-Cuentas-De-La-Comision.md) 1.3 aplica. **Decisión A: resetear no exige que la cuenta esté habilitada**; **decisión B: la contraseña provisoria la produce el sistema y no la escribe el administrador**. **§3**: la acción de reseteo dejaba de dibujarse sobre las cuentas que no estaban habilitadas y **pasa a ofrecerse cualquiera sea la situación**, con el motivo: condicionarla obligaría al docente a recordar una secuencia que la decisión A elimina. Sobre la cuenta que todavía no estableció contraseña sigue sin dibujarse, porque ahí sí no hay nada que resetear. **§4**: la precondición de la acción y la del diálogo se corrigen, y la confirmación declara que la provisoria **la produce el servicio**. **§5**: el estado «reseteo no admitido» pasa de tres causas a **dos** —la cuenta no habilitada sale de la lista, porque su reseteo procede— y su texto deja de ofrecer «rehabilitar primero», que dejó de ser un paso necesario. **§3** registra que lo que era una decisión derivada de esta categoría **quedó ratificado por el Product Owner**. **Ningún esquema, componente ni criterio de accesibilidad se agrega o se quita**: la superficie sigue sin ningún campo donde el administrador escriba una contraseña ajena, y la provisoria sigue mostrándose una sola vez. Sube minor: amplía el alcance de una acción existente sin cambiar ninguna otra. **Autor:** UX Lead (AG-03) |
| 1.3 | 2026-08-09 | **Cierra la parte de los hallazgos `F26-30` y `F26-27`** que alcanzan a este archivo y **deja constancia por el `F26-25`**, del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0, contra `PRODUCT-INTAKE` **1.10**. **`F26-30`**: el diálogo de reseteo de §3 decía «**Le vas a fijar** una contraseña provisoria», que es la formulación anterior a la decisión del Product Owner sobre quién la produce. No afirmaba un campo de escritura y el resultado del diálogo ya mostraba una provisoria generada —por eso el informe lo clasifica como residuo de vocabulario—, pero contradice el verbo de **RN-14**, que el intake 1.10 incorpora: pasa a decir «**El sistema le va a generar** una contraseña provisoria». El resto del texto del diálogo no cambia. **`F26-27`**: la fila 1.2 de este control de cambios tenía **cuatro celdas en una tabla de tres columnas**; su texto se conserva íntegro y el autor pasa a leerse dentro de la celda de cambios. **Constancia por `F26-25`**: el informe registra que **`US-30` se agregó a la trazabilidad de esta superficie sin registro propio**. Se deja escrito acá, sin reescribir la fila histórica: `US-30` es la historia del reseteo desde el panel, corresponde a la quinta operación de la fila de cuentas que la versión 1.1 incorporó, y está vigente en §7. **Ningún componente, estado, ruta ni criterio de aceptación de esta superficie cambia.** Sube minor. |
| 1.4 | 2026-08-10 | **Cierra la parte del hallazgo `N-5`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r2.md` 1.0 que alcanza a este archivo, contra `PRODUCT-INTAKE` **1.11**. La **trazabilidad de cabecera** citaba «§11 (RN-B6)» como riesgo **vigente**; el intake **1.10** tachó esa fila el 2026-08-09, porque **F-26** conserva la cuenta y todos sus trabajos, de modo que la baja dejó de ser el remedio del olvido y la mitigación que `RN-B6` declaraba —advertir al alumno antes de darlo de baja— **quedó sin objeto**. La cita **se conserva** con la constancia de que la fila está tachada y con el motivo, y remite a §7 CL-6, que declara que la baja arrastra los trabajos y funda el aviso de arrastre del diálogo de baja, que es donde vive hoy lo que sostenía, en lugar de borrarse, para que no se lea como si el riesgo nunca hubiera existido. **Ningún esquema, componente, interacción, estado ni criterio de accesibilidad de esta superficie cambia.** Sube minor: corrige una referencia a una fila retirada. |
| 1.5 | 2026-08-10 | **Absorbe `PRODUCT-INTAKE` 1.13 §4.1 (RN-16) y la precisión de F-04**: habilitar una cuenta **produce su contraseña provisoria**, con las mismas propiedades y el mismo tratamiento que el reseteo. §4 reescribe dos filas de comportamiento: **habilitar** pasa a mostrar la provisoria una sola vez, con el panel sin campo de contraseña, y **resetear** deja de exigir que la cuenta ya tenga contraseña. §9 suma la prueba prevista de la habilitación. **Las cinco operaciones del panel, el diálogo de reseteo y la disposición de la superficie no cambian.** Sube minor. (DX Lead (AG-03)). |
| 1.6 | 2026-08-10 | **Cierra el hallazgo `C-08` (P2) del informe de auditoría `SDD/Docs/Audit/Coherencia-Corpus-r1.md` 1.0.** La cabecera de trazabilidad declaraba derivarse del `PRODUCT-INTAKE` **1.7**, versión archivada, y pasa a declarar la **1.14**, vigente. La **1.7** es la versión cuya letra sobre **RN-13** e **INV-09** fue precisada en la 1.8 y corregida en la 1.14, que es exactamente el punto donde el corpus más se equivocó. Se revisó el cuerpo antes de mover la cabecera y **no arrastra ninguna decisión de las versiones intermedias**: no queda en él ningún recuento de «quince reglas» ni de «diecisiete códigos», ninguna cita a la exclusión **X-2** como vigente y ninguna afirmación de que la marca de cambio de contraseña pendiente la ponga únicamente el reseteo. **Ningún contenido normativo de este documento cambia: la corrección es de trazabilidad.** Sube minor. |
