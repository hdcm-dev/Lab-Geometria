# Wireframes — Credencial propia

**Unidad de entrega:** GeometriaFactory-Web
**Documento:** Wireframes-Credencial-Propia.md
**Versión:** 1.6
**Estado:** Aprobado
**Fecha:** 2026-08-15
**Autor:** UX/UI Designer + Frontend Lead (AG-03)
**Variante:** UX/UI
**Trazabilidad upstream:** `../02-Especificacion-Funcional/Casos-De-Uso/CU-10003-Establecer-Y-Cambiar-La-Contrasena-Propia.md` íntegro; `../02-Especificacion-Funcional/Casos-De-Uso/CU-10002-Iniciar-Y-Cerrar-Sesion-Sin-Exponer-La-Credencial.md` FA-02 y **FA-07**; `../02-Especificacion-Funcional/Especificacion-Funcional.md` §6 (RT-02, RT-03, RT-06, **RT-12**); `../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00002-Identidad-Propia-Del-Alumno-Sin-Correo.md` §1, §5 (segundo y cuarto criterio); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.8**, §4 (F-04, F-05, **F-26**), §4.1 (**RN-10013 precisada**), §6 (flujo 1), §7 (**CL-7 reescrito**), §9 (X-1, **X-2 retirada**), §11 (**RN-B6 tachado** el 2026-08-09 por el intake 1.10, porque F-26 dejó sin objeto su mitigación; lo que sostenía vive en §7 CL-7), §17.1.P.2 · GeometriaFactory-Domain (**INV-09**), §17.6 P.5; `Design-Rules-Web-Generico.md` §3.1, §4.4, §4.6, §4.9, §5, §7; `Design-Rules-Primer-Arranque.md` §4.5; `Design-Rules-Blazor-Mudblazor.md` §4.2
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

**Nombre canónico de superficie: `Credencial-Propia`.**

La persona fija su contraseña por primera vez, o la reemplaza presentando la vigente, o la reemplaza **obligada** después de que el administrador se la reseteó. Es la **única** forma que tiene de administrar su credencial dentro del laboratorio: no hay canal de correo y **no hay recuperación autónoma**.

**Una superficie con tres cursos y no tres superficies.** Es el mismo objeto —la credencial propia—, el mismo actor y el mismo formulario salvo un campo. Lo que sí cambia entre los cursos es el **shell**, y por eso los tres se declaran acá con su propio recorrido:

| Curso | Cuándo | Shell | Cómo se llega | A dónde va al terminar |
| --- | --- | --- | --- | --- |
| **Establecimiento** | Primer ingreso efectivo de una cuenta ya habilitada, todavía sin contraseña | **Acceso**, sin navegación: la persona todavía no tiene sesión | Derivada desde `Ingreso` | A `Ingreso`, con banda de confirmación |
| **Cambio** | La persona ya está dentro y quiere reemplazarla | **Trabajo**, con la barra lateral de su papel | Destino «Mi contraseña» de la barra lateral | Al panel de la persona, **con la sesión vigente** |
| **Cambio forzado** | El administrador le reseteó la contraseña y la persona presentó la provisoria | **Acceso**, y **sin sesión**: sin barra lateral y sin ninguna otra salida, porque ninguna otra ruta está disponible | Derivada desde `Ingreso`, o desde cualquier ruta que la persona intente | A `Ingreso`, con banda de confirmación y **la marca levantada** |

**Por qué el cambio forzado lleva el shell de acceso.** La barra lateral es la promesa de que hay a dónde ir, y acá no lo hay: mientras la marca esté puesta, **ninguna otra ruta se arma** (RT-12, RN-10013). Dibujarla y que todos sus destinos devuelvan a esta misma superficie sería mentir con la disposición y dejar a la persona probando puertas cerradas. La versión anterior de esta tabla llegaba a la misma disposición por un camino más largo, porque suponía que la persona **tenía sesión** y había que explicar por qué no se le mostraba la navegación; con la precisión de RN-10013 en el `PRODUCT-INTAKE` **1.8** —la cuenta se autentica y **no obtiene sesión de trabajo**— el shell de acceso deja de necesitar justificación: es el mismo que el del establecimiento, por la misma razón.

## 2. Layout

Curso de establecimiento, sobre el shell de acceso:

```text
+---------------------- lienzo, sin chrome ------------------------+
|              +-------- ancho acotado ~380px --------+            |
|              |  [ico] Fábrica de Geometría          |            |
|              |  Elegí tu contraseña                 |  h1        |
|              |  Es la primera vez que entrás. El    |  por qué   |
|              |  laboratorio nunca te envió una      |  está acá  |
|              |  contraseña: la elegís vos ahora.    |            |
|              |  [ banda de resultado  rol=alerta  ] |            |
|              |  Contraseña nueva                    |            |
|              |  [____________________________]      |            |
|              |  <requisito declarado>               |  §4.5      |
|              |  Repetir contraseña nueva            |            |
|              |  [____________________________]      |            |
|              |  [====== Guardar contraseña ======]  |            |
|              +--------------------------------------+            |
|                    Versión 1.4.2                                 |
+------------------------------------------------------------------+
```

Curso de cambio, sobre el shell de trabajo:

```text
+----------+----------------------------------------------------------+
| Laborat. |  Mi contraseña                                           |
|          |  Para cambiarla tenés que escribir la que usás hoy.      |
| ·Mis     |  ------------------------------------------------------- |
|  trabajos|  +--------------- ancho acotado ----------------+        |
| ·Trabajo |  |  [ banda de resultado ]                      |        |
|  nuevo   |  |  Contraseña actual                           |        |
| ·Mi      |  |  [__________________________________]        |        |
|  contra- |  |  Contraseña nueva                            |        |
|  seña    |  |  [__________________________________]        |        |
|          |  |  <requisito declarado>                       |        |
| -------- |  |  Repetir contraseña nueva                    |        |
| Ana Diaz |  |  [__________________________________]        |        |
| [Cerrar] |  |         [ Cancelar ]  [ Guardar contraseña ] |        |
| v1.4.2   |  +----------------------------------------------+        |
+----------+----------------------------------------------------------+
```

Curso de cambio forzado, sobre el shell de acceso y **sin sesión**:

```text
+---------------------- lienzo, sin chrome ------------------------+
|              +-------- ancho acotado ~380px --------+            |
|              |  [ico] Fábrica de Geometría          |            |
|              |  Elegí una contraseña nueva          |  h1        |
|              |  El docente te reseteó la clave. La  |  por qué   |
|              |  que te pasó sirve sólo para esto:   |  está acá  |
|              |  para que elijas la tuya ahora. Él   |            |
|              |  no va a saber cuál elegís.          |            |
|              |  [ banda de resultado  rol=alerta  ] |            |
|              |  Contraseña provisoria               |            |
|              |  [____________________________]      |            |
|              |  Contraseña nueva                    |            |
|              |  [____________________________]      |            |
|              |  <requisito declarado>               |  §4.5      |
|              |  Repetir contraseña nueva            |            |
|              |  [____________________________]      |            |
|              |  [====== Guardar contraseña ======]  |            |
|              |  Volver al ingreso                   |  enlace    |
|              +--------------------------------------+            |
|                    Versión 1.4.2                                 |
+------------------------------------------------------------------+
```

> ### ⚠ APARTAMIENTO A CONFIRMAR POR EL PRODUCT OWNER — el curso forzado se construyó con **cuatro** campos
>
> **Qué dice este documento y qué se construyó.** El tercer esquema de §2, la fila «Guardar en el curso forzado» de §4 y el estado «Curso de cambio forzado» de §5 dibujan **tres** campos: provisoria, nueva y repetición. Lo construido tiene **cuatro**: se le agrega **el correo**. Los tres campos dibujados **no cambian**, ni de rótulo ni de orden ni de comportamiento; lo que se agrega es un campo **arriba** de ellos.
>
> **Por qué, y no es una preferencia de quien construye.** El esquema de tres campos supone que **de qué cuenta se trata lo sabe el estado previo**: el ingreso lo anota al recibir el desvío y esta pantalla lo lee. Eso no puede funcionar, y no funcionaba. `Ingreso` **no es una superficie interactiva** —es la única que tiene que escribir la marca de sesión del navegador, y una marca se escribe en una cabecera—, de modo que su envío es **una petición HTTP** y la derivación a esta ruta abre **otra**, con su propio estado de ámbito, vacío. Lo que el ingreso anotara antes de derivar llegaba acá **en nulo**, y la pantalla mostraba un callejón que no es ningún estado de §5 —«entrá primero con la que te pasó el docente»—: **el alumno reseteado volvía a quedarse sin puerta**, que es exactamente lo que `F-26` vino a resolver. Llevar el correo **en la dirección** lo dejaría escrito en la barra del navegador y en el historial, que es donde §5 no lo quiere.
>
> **Qué se gana.** La pantalla pasa a ser **autosuficiente**: funciona con **recarga**, con **enlace guardado** y con quien llega **de frente** sin haber pasado por el ingreso —los tres son, vistos desde el servidor, la misma cosa: una petición sin estado previo—. Y desaparece el callejón. La fila «Llegar al cambio forzado» de §4 admite ya la llegada «desde cualquier ruta que la persona intente»; con cuatro campos esa llegada **se puede completar**, y con tres no.
>
> **No afloja nada.** `RN-10013` e `INV-09` siguen exactamente igual: esta pantalla **no emite ninguna sesión**, la marca se levanta **sólo** con el cambio efectivo hecho por la propia cuenta, y la persona obtiene su sesión de trabajo recién al volver a entrar con la contraseña nueva. El contrato del servicio de datos **no se toca**: `OwnPasswordChangeRequest` ya lleva el correo como campo opcional desde el `PRODUCT-INTAKE` **1.34**, y la forma sin sesión del punto `A-05` ya lo recibe.
>
> **Segundo apartamiento, del mismo origen y también a confirmar: el estado «Enviando» de §5 no se dibuja.** §7 de este mismo documento manda que «el envío va por petición al punto correspondiente y **no por interactividad de componente**», y un envío por petición no tiene dónde pintar el paso intermedio: la acción no se inhabilita y no hay indicador. Es el mismo trato que ya tiene `Ingreso`. La prevención del doble envío queda del lado del servicio de datos, que rechaza la segunda provisoria por no corresponder.
>
> **Qué falta para cerrarlo.** La confirmación del Product Owner, y con ella la corrección del tercer esquema de §2, de la fila de §4 y de los dos estados de §5 que dicen «tres campos». **Hasta que eso ocurra este documento sigue dibujando tres y lo construido tiene cuatro, y la diferencia se declara acá en lugar de callarse.** Este curso además **ya venía sin validación visual**: [`Linea-Base-Visual.md`](Linea-Base-Visual.md) §6.1, fila de `F-26`, declara que nadie lo miró y que no tiene `CMP-XX` ni `EST-XX` propios. La vía para cerrar los dos desfases juntos es la misma que esa sección ya pide: **iteración 5** de maqueta y reemisión de la línea de base.

**El curso de establecimiento no tiene «cancelar», el de cambio sí, y el forzado tampoco.** En el establecimiento no hay estado previo al que volver: la persona no tiene sesión y abandonar la deja fuera. En el cambio hay un panel al que volver, y la salida es legítima. En el forzado tampoco hay sesión ni estado previo, y por eso lo único que se ofrece es **volver al ingreso**: no es una cancelación —no deja a la persona adentro, porque adentro no estuvo—, es la salida honesta de quien prefiere resolverlo después. **La marca no se levanta por irse**: el próximo ingreso con la provisoria vuelve acá.

## 3. Componentes principales

| Componente | Patrón del catálogo | Propósito | Datos que muestra | Comportamiento |
| --- | --- | --- | --- | --- |
| Tarjeta de credencial | Primer arranque §4.2 / Base §4.4 | Contener el formulario | — | Ancho acotado en los dos cursos |
| Subtítulo de motivo | Base §2.2 | Explicar **por qué** la persona está acá | Texto distinto por curso | Inerte. En el establecimiento declara que el laboratorio nunca envió una contraseña; en el forzado, que el docente reseteó la clave y que **no va a conocer la nueva** |
| Banda de resultado | Primer arranque §4.4 | Comunicar el resultado | Texto resuelto desde el código del contrato | Condicional, rol de alerta |
| Campo de contraseña actual | Base §4.6 | Presentar la vigente | Enmascarado | **En el curso de cambio y en el forzado. Es obligatorio por contrato.** En el forzado se rotula «Contraseña provisoria», que es lo que la persona tiene en la mano, y no «actual» |
| Campos de contraseña nueva y repetición | Base §4.6 | Fijar la credencial | Enmascarados, con conmutador de visibilidad | La coincidencia se verifica **antes** de salir hacia el servicio de datos |
| Requisito declarado | Primer arranque §4.5 | Enunciar la regla de forma **antes** de que la persona escriba | Texto derivado de la política del sistema | Asociado al campo. **No aparece recién al fallar** |
| Acción primaria | Base §4.9 | Guardar | Verbo exacto: «Guardar contraseña» | Se inhabilita con indicador durante el envío |
| Acción secundaria | Base §4.9 | Volver sin cambiar nada | «Cancelar» | **Sólo en el curso de cambio.** El forzado no la lleva: no hay estado al que volver |
| Salida al ingreso | Base §4.9, terciaria | Irse sin resolverlo ahora | «Volver al ingreso» | **Sólo en el curso forzado.** No es una cancelación: no hay sesión que cerrar ni panel al que volver, y **la marca sigue puesta** para el próximo ingreso |
| Sello de versión | [`Representacion-Sello-De-Version.md`](Representacion-Sello-De-Version.md) | Identificar la instancia | Versión legible | Al pie de la tarjeta en el establecimiento; en la barra lateral en el cambio |

**Lo que esta superficie no dibuja:** ningún medidor de fortaleza que prometa una política que el producto no fija, ninguna opción de «recordarme», ningún enlace de recuperación autónoma —que el producto sigue sin tener—, y, en el curso forzado, **ninguna barra lateral y ningún atajo a ninguna otra ruta**: no existen mientras la marca esté puesta.

Sobre el requisito declarado: **las exigencias de forma de la contraseña no las fija esta categoría.** Si el producto adopta alguna, se declara aguas abajo y se hace cumplir del lado del servicio de datos; el requisito de la superficie **se deriva de esa política y no se transcribe como literal en la vista**. Mientras no haya política declarada, la línea enuncia la única regla que sí existe hoy: que no hay forma de recuperarla.

## 4. Interacciones

| Acción | Disparador | Resultado esperado | Precondición |
| --- | --- | --- | --- |
| Llegar al establecimiento | Derivación desde `Ingreso` | La superficie se arma sobre el shell de acceso | Cuenta habilitada y sin contraseña |
| Abrir el cambio | Destino «Mi contraseña» | La superficie se arma sobre el shell de trabajo | Sesión iniciada |
| Guardar | Acción primaria o ingreso desde el último campo | Se verifica que las dos escrituras coincidan; si coinciden, se envía | Campos completos |
| Escribir | Tecleo | Sin ida y vuelta al servidor | — |
| Cancelar el cambio | Acción secundaria | Vuelve al panel sin tocar la credencial | Curso de cambio |
| Abandonar el establecimiento | Navegación fuera | **No queda nada guardado**: la cuenta sigue habilitada y sin contraseña, y el próximo intento de ingreso vuelve a derivar acá | Curso de establecimiento |
| Cambiar con la contraseña actual equivocada | Acción primaria | El cambio **no se aplica** y el mensaje señala el campo de contraseña actual. Terminación controlada | Curso de cambio |
| Llegar al cambio forzado | Derivación desde `Ingreso` tras presentar la provisoria, o desde cualquier ruta intentada | La superficie se arma sobre el shell de acceso, **sin sesión**, y declara por qué | Cuenta con cambio de contraseña pendiente |
| Intentar cualquier otra ruta | Dirección directa, atajo del navegador, historial | **Vuelve acá**, sin revelar qué contenía la ruta pedida y **sin presentarse como error**: es la situación esperada | Curso forzado |
| Guardar en el curso forzado | Acción primaria | La contraseña queda reemplazada, **la marca se levanta** y la persona vuelve a `Ingreso` con banda de confirmación, donde el ingreso con la nueva **sí entrega sesión y panel**. Sus trabajos siguen todos ahí | Los tres campos completos |
| Volver al ingreso desde el curso forzado | Salida al ingreso | Vuelve a `Ingreso` sin tocar la credencial. **La marca no se levanta**: el próximo ingreso con la provisoria vuelve acá | Curso forzado |

**Apartamiento vigente sobre la fila «Guardar en el curso forzado»:** su precondición dice «los tres campos completos» y lo construido pide **cuatro**, con el correo. El motivo, lo que se gana y lo que falta para cerrarlo están en el recuadro de §2, y el apartamiento está **a confirmar por el Product Owner**.

## 5. Estados

| Estado | Condición que lo produce | Representación esperada |
| --- | --- | --- |
| **Vacío** | **No aplica**: no presenta ninguna colección | Se declara para que la ausencia sea deliberada |
| **Cargando** | La superficie se está armando | Esqueleto de dos o tres campos según el curso **PARCIALMENTE APLICABLE desde el 2026-08-31** (`ADR-10001` §2.1): esta superficie tiene **tres componentes** y sólo dos son interactivos —`OwnCredentialSetup` y `OwnCredentialChange`—. `OwnCredentialForcedChange` es de **render estático** y en él este estado **no existe**. |
| **Con datos** | Formulario listo | Tarjeta completa, foco inicial en el primer campo |
| **Enviando** | El cambio está en curso | Acción inhabilitada con indicador. **Previene el doble envío** |
| **Curso de primer ingreso** | Cuenta **recién habilitada**, con su contraseña provisoria y **sin sesión** | **Tres** campos —provisoria como vigente, nueva y su repetición—, sin «cancelar», sobre el shell de acceso, con el subtítulo que declara por qué está ahí. **Es el mismo formulario que el curso de cambio forzado** y sólo cambia el texto del subtítulo: acá dice que la clave con la que entró es provisoria porque el docente acaba de habilitarla |
| **Curso de cambio** | Persona con sesión | Tres campos, con «cancelar», sobre el shell de trabajo |
| **Curso de cambio forzado** | Persona **sin sesión** y con **cambio de contraseña pendiente** | Tres campos, **sin «cancelar» y sin barra lateral**, sobre el shell de acceso, con el subtítulo que declara el reseteo y la salida al ingreso al pie |
| **Provisoria rechazada** | Lo escrito como provisoria no corresponde | Mensaje **sobre el campo de la provisoria**. El cambio no se aplica y **la marca sigue puesta** |
| **Éxito de cambio forzado** | La contraseña quedó reemplazada y la marca levantada | Navegación a `Ingreso` con banda de confirmación, como en el establecimiento. **El ingreso siguiente sí entrega sesión**, la provisoria deja de servir y el administrador **no conoce** la contraseña nueva |
| **Requisito no cumplido** | Falta un campo | Borde de peligro en el campo y banda de error |
| **Confirmación no coincidente** | Las dos escrituras de la nueva difieren | Banda de error que declara la discrepancia y qué hacer. **No sale ninguna solicitud hacia el servicio de datos** |
| **Contraseña actual rechazada** | La vigente no corresponde, o llegó ausente | Mensaje **sobre el campo de contraseña actual**. El cambio no se aplica |
| **Cuenta bloqueada entre la derivación y el envío** | La situación de la cuenta cambió mientras tanto | Se muestra el motivo y se vuelve a `Ingreso`, **sin cambiar la contraseña** |
| **Éxito de primer ingreso** | La contraseña quedó reemplazada y la marca levantada | Navegación a `Ingreso` con banda de confirmación. Es el camino de entrada a partir de ahora, y **la provisoria deja de servir** |
| **Éxito de cambio** | La contraseña quedó reemplazada | Vuelta al panel con confirmación. **La sesión vigente se conserva** y la contraseña anterior deja de servir |
| **Indisponible** | El servicio de datos no responde | Aviso de indisponibilidad con reintento, sin dirección de servicio interno. Ver [`Wireframes-Estado-Degradado-Y-Reconexion.md`](Wireframes-Estado-Degradado-Y-Reconexion.md) |
| **Reconectando** | Se corta el circuito | Cartel de reconexión superpuesto **PARCIALMENTE APLICABLE desde el 2026-08-31** (`ADR-10001` §2.1): esta superficie tiene **tres componentes** y sólo dos son interactivos —`OwnCredentialSetup` y `OwnCredentialChange`—. `OwnCredentialForcedChange` es de **render estático** y en él este estado **no existe**. |

**Apartamiento vigente sobre dos estados de esta tabla, a confirmar por el Product Owner.** El estado **«Curso de cambio forzado»** dice «tres campos» y lo construido dibuja **cuatro**, con el correo arriba de los tres. Y el estado **«Enviando»** —acción inhabilitada con indicador— **no se dibuja** en el curso forzado, porque §7 manda enviar por petición y no por interactividad de componente. Los dos salen del mismo recuadro de §2, que declara el motivo y qué falta para cerrarlos. **El estado «Curso de primer ingreso» no se toca**: nadie lo construyó todavía, y sigue diciendo lo que este documento dice.

## 6. Versión angosta

- La tarjeta toma el ancho disponible menos un margen. En el curso de establecimiento conserva el anclaje superior y **no se centra verticalmente**, por el teclado en pantalla.
- En el curso de cambio, las dos acciones del pie pasan a ancho completo apiladas, con la primaria **arriba**: es la que se busca, y dejarla debajo del pliegue en pantalla baja obliga a desplazarse para completar la tarea.
- **El requisito declarado no se colapsa.** Es lo que evita el juego de adivinanzas al fallar.
- La barra lateral colapsa según el patrón del documento base en el curso de cambio. **En el forzado no hay barra lateral que colapsar**, y el subtítulo que declara por qué la persona está ahí **no se colapsa ni se recorta**: sin él, la pantalla se lee como un pedido arbitrario.
- Legible sin desplazamiento horizontal a 320 px.

## 7. Notas de implementación

**Accesibilidad.** Encabezado de primer nivel en los tres cursos. El requisito declarado se asocia a su campo por descripción accesible, para que se anuncie **junto al control y antes del intento**. La banda de error se anuncia como alerta y la confirmación como estado. Foco inicial en el primer campo; tras un error, en el primer campo inválido. Los tres campos declaran su propósito —contraseña vigente y contraseña nueva— para que el gestor del navegador colabore en vez de ofrecer la anterior. **En el curso forzado, la llegada se anuncia como estado**: quien no ve la pantalla tiene que enterarse de que fue derivado y por qué, y no descubrirlo por la ausencia de la barra lateral. El conmutador de visibilidad declara su estado.

**Performance percibida.** Acción inhabilitada con indicador desde el primer instante.

**Internacionalización.** Español rioplatense, segunda persona. El requisito declarado tolera expansión sin romper la tarjeta.

**Restricciones de arquitectura.** El envío va por petición al punto correspondiente y **no por interactividad de componente**. La pieza pública **no conserva ninguna contraseña**: nada de lo que se escribe acá sobrevive a la operación, no se escribe en el navegador y no se incluye en ningún mensaje. Ningún mensaje incluye la dirección de un servicio interno.

## 8. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Persona objetivo | Alumno y docente por igual |
| CU origen | [`CU-10003`](../02-Especificacion-Funcional/Casos-De-Uso/CU-10003-Establecer-Y-Cambiar-La-Contrasena-Propia.md) íntegro, con [`CU-10002`](../02-Especificacion-Funcional/Casos-De-Uso/CU-10002-Iniciar-Y-Cerrar-Sesion-Sin-Exponer-La-Credencial.md) FA-02 y **FA-07** como vías de llegada |
| Reglas de negocio relevantes | `RN-10006` (cuenta pendiente o bloqueada sin acceso) y **`RN-10013`** (la provisoria confina hasta que se cambie, y la cuenta llega al cambio **sin sesión de trabajo**), del `PRODUCT-INTAKE` **1.8** §4.1, con archivo en [`RN-02013` · `GeometriaFactory-Domain`](../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02013-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md) |
| Restricciones transversales | `RT-02`, `RT-03`, `RT-06`, **`RT-12`** |
| Marco aplicado | [`Experiencia-De-Uso.md`](Experiencia-De-Uso.md) §3.2, §3.4, §4.1, §8 |
| Representaciones que invoca | [`Representacion-Sello-De-Version.md`](Representacion-Sello-De-Version.md) |
| Catálogo de diseño aplicado | `Design-Rules-Web-Generico.md`, `Design-Rules-Primer-Arranque.md` §4.5, `Design-Rules-Blazor-Mudblazor.md` §4.2 |
| US a generar en 06 | `US-10006`, `US-10007`, `US-10028`, `US-10029` |
| Tests previstos en 08 | Guion de demostración de la etapa `d` para el **primer ingreso con la provisoria como vigente**, con la comprobación de que recorre el mismo formulario de tres campos que el cambio forzado, para el **cambio forzado** —incluida la ruta pedida por dirección directa, que vuelve acá— y de la etapa `c` para el cambio exigiendo la vigente; dos escrituras distintas sin solicitud emitida; inspección del navegador sin contraseña observable; recorrido por teclado |

## 9. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. Una superficie con dos cursos y dos shells, con la tabla que declara cuándo rige cada uno y por qué sólo el de cambio tiene salida. Requisito declarado antes del intento y derivado de la política del sistema en lugar de transcrito, enumeración de lo que la superficie no dibuja, y catorce estados declarados para la Fase B2. |
| 1.0 | 2026-08-09 | Correcciones absorbidas del audit `B-02-03-GeometriaFactory-Web-r1.md` (ronda 1), **sin subir versión** por `Master-Prompt.md` §5, que lo admite mientras el documento está en estado `Propuesto`. **H-06**: la `NB-00002` de la cabecera pasa a citarse con sección y criterio —§1, §5 (segundo y cuarto criterio)—, con la forma que ya usan los casos de uso de la categoría 02. |
| 1.1 | 2026-08-09 | **Propagación del `PRODUCT-INTAKE` 1.7**, capacidad **F-26** con su regla **RN-10013** y el invariante **INV-09**. La superficie pasa de **dos a tres cursos**, con el **cambio forzado** de quien fue reseteado. **§1**: tabla de cursos ampliada y **decisión de esta versión declarada**: el forzado lleva el **shell de acceso aunque haya sesión**, porque la barra lateral prometería destinos que no existen mientras la marca esté puesta. **§2**: esquema nuevo, y el enunciado de «cancelar» pasa a cubrir los tres cursos, con la salida de sesión como única salida del forzado y con la aclaración de que **cerrar sesión no levanta la marca**. **§3**: componente nuevo, el campo de la provisoria rotulado como tal, y la enumeración de lo que la superficie no dibuja se precisa: lo que no existe es la recuperación **autónoma**. **§4**: cuatro interacciones nuevas. **§5**: cuatro estados nuevos. **§6** y **§7**: versión angosta y accesibilidad del curso forzado, con la llegada anunciada como estado. **§8**: RN-10013 y RT-12 nuevas. Sube minor: agrega un curso a la superficie, sin cambiar ninguno de los dos existentes. |
| 1.2 | 2026-08-09 | **Reconciliación con el `PRODUCT-INTAKE` 1.8.** La versión 1.1 modelaba el cambio forzado **con sesión iniciada** y hacía de esa combinación —shell de acceso teniendo sesión— la decisión declarada de la versión. El intake 1.8 §4.1 precisa RN-10013: la cuenta con provisoria **se autentica y no obtiene sesión de trabajo**. **La disposición no cambia**, y ése es el punto: el shell de acceso deja de necesitar justificación y pasa a ser el mismo que el del establecimiento, por la misma razón. **§1** corrige la fila del curso y reescribe su párrafo de fundamento; **§2** corrige el rótulo del esquema; **§3** renombra el componente terciario de «Salida de sesión» a **«Salida al ingreso»**, con «Volver al ingreso» en lugar de «Cerrar sesión», porque no hay sesión que cerrar; **§4** corrige las tres interacciones del curso —llegada sin sesión, guardado que devuelve a `Ingreso` y salida—; **§5** corrige los dos estados del curso. El destino al terminar pasa a ser `Ingreso` con banda de confirmación, que es el mismo desenlace del establecimiento. La cabecera cita el intake **1.8**. Sube minor: corrige la premisa de un curso existente y renombra un componente, sin agregar ni quitar cursos. |
| 1.3 | 2026-08-10 | **Cierra la parte del hallazgo `N-5`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r2.md` 1.0 que alcanza a este archivo, contra `PRODUCT-INTAKE` **1.11**. La **trazabilidad de cabecera** citaba «§11 (RN-B6)» como riesgo **vigente**; el intake **1.10** tachó esa fila el 2026-08-09, porque **F-26** conserva la cuenta y todos sus trabajos, de modo que la baja dejó de ser el remedio del olvido y la mitigación que `RN-B6` declaraba —advertir al alumno antes de darlo de baja— **quedó sin objeto**. La cita **se conserva** con la constancia de que la fila está tachada y con el motivo, y remite a §7 CL-7, que declara que no hay recuperación **autónoma**, que es donde vive hoy lo que sostenía, en lugar de borrarse, para que no se lea como si el riesgo nunca hubiera existido. **Ningún curso, esquema, componente, interacción ni estado de esta superficie cambia**: §3 ya declara que lo que no existe es la recuperación autónoma. Sube minor: corrige una referencia a una fila retirada. |
| 1.5 | 2026-08-15 | **Declara un apartamiento del curso de cambio forzado, a confirmar por el Product Owner, y no cambia ninguna especificación.** Lo trae la construcción de ese curso, que al ejercitarse **sobre HTTP y atravesando la derivación** encontró que el esquema de **tres** campos no puede funcionar: supone que de qué cuenta se trata lo sabe el estado previo, y `Ingreso` es una superficie **no interactiva** cuyo envío es una petición, con lo cual la derivación abre otra y el correo anotado llega **en nulo**. La pantalla quedaba mostrando un callejón que no es ningún estado de §5 y **el alumno reseteado volvía a quedarse sin puerta**, que es lo que `F-26` vino a resolver. Lo construido tiene **cuatro** campos —el correo, arriba de los tres dibujados, que no cambian—, con lo que la pantalla pasa a funcionar **con recarga, con enlace guardado y con quien llega de frente**. **§2** suma el recuadro del apartamiento, con su motivo, lo que gana, lo que **no** afloja —`RN-10013` e `INV-09` intactos, contrato del servicio de datos sin tocar— y qué falta para cerrarlo; **§4** y **§5** llevan la marca al pie de sus tablas, sobre la fila y los dos estados que dicen «tres campos». Se declara además un **segundo apartamiento del mismo origen**: el estado **«Enviando»** no se dibuja en el curso forzado, porque §7 de este mismo documento manda enviar **por petición y no por interactividad de componente**. **Ningún esquema, componente, interacción ni estado se corrige, se agrega ni se retira**: hasta la confirmación del Product Owner este documento sigue dibujando tres campos y la diferencia queda declarada en lugar de callada, con la misma vía que [`Linea-Base-Visual.md`](Linea-Base-Visual.md) §6.1 ya pide para este curso —iteración 5 de maqueta y reemisión de la línea de base—. Sube minor: declara un desfase, sin cambiar la especificación. |
| 1.4 | 2026-08-10 | **Absorbe `PRODUCT-INTAKE` 1.13 §4.1 (RN-10016) y la precisión de F-04**: habilitar una cuenta produce su contraseña provisoria, con lo cual **el curso de establecimiento de dos campos deja de existir**. §4 lo reemplaza por el **curso de primer ingreso**, de **tres** campos, que es el mismo formulario del cambio forzado y sólo cambia el texto del subtítulo; §5 reescribe su estado de éxito, que ahora levanta la marca, y precisa el de cuenta bloqueada. §9 suma a las pruebas previstas la comprobación de que los dos cursos sin sesión comparten formulario. **La superficie sigue teniendo tres cursos y dos shells**, y ningún elemento visual se agrega ni se retira. Sube minor. (DX Lead (AG-03)). |
| 1.6 | 2026-08-31 | **`U-06` del plan de la mesa: las promesas que este modo de render no puede cumplir se acotan, y no se retiran.** `ADR-10001` **1.1** §2.1 declaró el reparto real —**seis superficies interactivas de catorce**— y de ahí se sigue que en las estáticas **no existe el instante que un esqueleto ocupa** ni circuito que se pueda cortar: el servidor entrega el documento completo o nada. Las filas **Cargando** y **Reconectando** de las superficies estáticas quedan marcadas **NO APLICA**, y `Credencial-Propia` **parcialmente aplicable**, porque tiene tres componentes y uno es estático. **Ninguna fila se borra**: un estado prometido que se retira sin dejar rastro obliga al próximo lector a redescubrir por qué. Cierra `MI-07`, `MI-10` y `MI-12`. |
