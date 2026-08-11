# Wireframes — Aprovisionamiento inicial

**Proyecto de código:** GeometriaFactory-Web
**Documento:** Wireframes-Aprovisionamiento-Inicial.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-09
**Autor:** UX/UI Designer + Frontend Lead (AG-03)
**Variante:** UX/UI
**Trazabilidad upstream:** `../02-Especificacion-Funcional/Casos-De-Uso/CU-04-Administrar-Las-Cuentas-De-La-Comision.md` §3, FA-03, FA-04, §6 y CA-01; `../02-Especificacion-Funcional/Especificacion-Funcional.md` §6 (RT-03, RT-06, RT-07); `../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-01-Control-De-Admision-Al-Laboratorio.md` §1, §5 (los cinco criterios); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-01), §4.1 (RN-01), §14 (RA-03), §17.6 P.5; `Design-Rules-Primer-Arranque.md` §1 a §9; `Design-Rules-Web-Generico.md` §3.1, §4.6, §4.9, §5, §7; `Design-Rules-Blazor-Mudblazor.md` §4.2
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

**Nombre canónico de superficie: `Aprovisionamiento-Inicial`.**

Es la única puerta de un laboratorio recién desplegado. Crea la **única cuenta de administrador** que la instancia va a tener, y con eso el laboratorio pasa de inutilizable a operable. Se usa **una vez en la vida de la instancia** y después deja de armar formulario para siempre.

Es la superficie de mayor consecuencia del producto en relación con su tamaño: si falla, no hay administrador, y sin administrador nadie habilita cuentas y el laboratorio no existe. Por eso se diseña con el catálogo de primer arranque completo y no como un caso especial improvisado.

## 2. Layout

Shell de acceso: **sin barra lateral, sin barra superior y sin navegación**, porque mientras el sistema no está aprovisionado no hay a dónde ir y ofrecer destinos sería mostrar puertas que no abren. Tarjeta de ancho acotado anclada a la franja superior del área visible, no centrada verticalmente.

```text
+---------------------- lienzo, sin chrome ------------------------+
|                                                                  |
|              +-------- ancho acotado ~380px --------+            |
|              |  [ico] Fábrica de Geometría          |  identidad |
|              |                                      |            |
|              |  Configurar el laboratorio           |  h1        |
|              |  Vas a crear la única cuenta de      |  subtítulo |
|              |  administrador de este laboratorio.  |  de alcance|
|              |  No se puede crear otra después.     |  y unicidad|
|              |                                      |            |
|              |  [ banda de resultado  rol=alerta  ] |  condicional
|              |                                      |            |
|              |  Correo                              |  etiqueta  |
|              |  [____________________________]      |            |
|              |                                      |            |
|              |  Contraseña                          |            |
|              |  [____________________________]      |            |
|              |  Vas a usarla para entrar; no hay    |  requisito |
|              |  forma de recuperarla.               |  declarado |
|              |                                      |            |
|              |  Repetir contraseña                  |            |
|              |  [____________________________]      |            |
|              |                                      |            |
|              |  [==== Crear la cuenta de admin. ==] |  ancho     |
|              |                                      |  completo  |
|              +--------------------------------------+            |
|                                                                  |
|                 Versión 1.4.2   [preliminar]                     |
|                                                                  |
+------------------------------------------------------------------+
```

**No hay acción secundaria y no hay «cancelar».** En el primer arranque no existe un estado previo al que volver, y ofrecer una salida dejaría el laboratorio a medio configurar sin que nadie lo note.

Superficie previa de resolución, que la persona ve por menos de un segundo y que igual es un estado del sistema y se muestra como tal:

```text
+---------------------- lienzo, sin chrome ------------------------+
|                                                                  |
|              [=========== barra indeterminada ==========]        |
|                                                                  |
+------------------------------------------------------------------+
```

## 3. Componentes principales

| Componente | Patrón del catálogo | Propósito | Datos que muestra | Comportamiento |
| --- | --- | --- | --- | --- |
| Tarjeta de aprovisionamiento | `Design-Rules-Primer-Arranque` §4.2 | Contener el acto completo | — | Ancho acotado, anclada arriba. Estados: normal, con error, enviando |
| Identidad del laboratorio | Base §6.3 | Declarar dónde está la persona | Nombre del producto, marca vectorial | Inerte |
| Encabezado y subtítulo de alcance | Base §2.2 | Nombrar la tarea y **declarar la unicidad antes del intento** | Texto fijo | Inerte. El encabezado es el de primer nivel de la superficie |
| Banda de resultado | Primer arranque §4.4 | Comunicar el resultado del intento | Texto resuelto desde el código de resultado del contrato | Condicional. Variante de error con rol de alerta; variante de confirmación con rol de estado |
| Campo de correo | Base §4.6 | Identidad de la cuenta | Lo escrito | Etiqueta visible arriba. Sin texto de ejemplo que sustituya la etiqueta |
| Campo de contraseña y su repetición | Base §4.6 | Fijar la credencial | Enmascarados | Dos campos. La coincidencia se verifica **antes** de salir hacia el servicio de datos |
| Requisito declarado | Primer arranque §4.5 | Enunciar la regla en positivo antes de que la persona escriba | Texto derivado de la política del sistema | Asociado al campo que describe. **No aparece recién al fallar** |
| Acción primaria | Base §4.9 | Ejecutar el acto | Verbo exacto: «Crear la cuenta de administrador» | Ancho completo. Se inhabilita con indicador durante el envío |
| Sello de versión | [`Representacion-Sello-De-Version.md`](Representacion-Sello-De-Version.md) | Identificar la instancia | Versión legible, distintivo y marcador según corresponda | Al pie. Es **una de las dos ubicaciones obligatorias**: la superficie de acceso |
| Superficie de resolución | Primer arranque §4.3 | Mostrar que el destino se está resolviendo | — | Barra indeterminada. La navegación resultante **reemplaza** la entrada del historial en vez de apilarla |

## 4. Interacciones

| Acción | Disparador | Resultado esperado | Precondición |
| --- | --- | --- | --- |
| Abrir cualquier ruta del laboratorio | Entrada de la persona | Se resuelve el predicado de aprovisionamiento y se redirige. Con el predicado en falso, a esta superficie | Ninguna |
| Escribir en un campo | Tecleo | Sin ida y vuelta al servidor. **Ninguna validación consulta al servicio de datos mientras se escribe** | — |
| Confirmar el acto | Acción primaria o ingreso desde el último campo | Se verifica la coincidencia de las dos contraseñas; si coincide, se ejecuta el alta | Los tres campos completos |
| Reintentar tras un error | Acción primaria | La banda se reemplaza y el foco vuelve al primer campo inválido. **Lo escrito se conserva**, salvo las contraseñas, que se vuelven a pedir | Hubo un error previo |
| Abrir esta ruta con el laboratorio ya aprovisionado | Entrada directa por dirección | Redirección **neutra** a `Ingreso`, sin explicar el motivo | El predicado es verdadero |
| Enviar el formulario después de que otro lo aprovisionó | Envío tardío | Redirección neutra a `Ingreso`. **No se devuelve un error**: el intento tardío es una condición de carrera esperable y no una falta de la persona | El predicado pasó a verdadero entre la carga y el envío |
| Abrir el detalle de diagnóstico | Activación del sello | Se despliega el contrato completo con copiado en un solo gesto | — |

**Las tres capas del corte**, deliberadamente redundantes y todas contra el mismo predicado: el guard de ruteo, que corta la navegación a cualquier ruta protegida; el guard de superficie, que impide abrir ésta con el laboratorio ya aprovisionado y abrir `Ingreso` cuando todavía no lo está; y el guard de la acción, que es el único no evitable y que **redirige en vez de errar**. Su mecánica técnica es de `05-Arquitectura-Tecnica`; acá se declara el comportamiento observable.

## 5. Estados

| Estado | Condición que lo produce | Representación esperada |
| --- | --- | --- |
| **Resolviendo destino** | El predicado todavía no respondió | Barra indeterminada sobre el lienzo. Sin texto: la espera es breve por contrato. Anunciada como región activa |
| **Vacío** | **No aplica.** La superficie no presenta ninguna colección | Se declara para que la ausencia sea deliberada |
| **Cargando** | La superficie se está armando | Tarjeta con esqueleto de tres campos |
| **Con datos** | El predicado es falso y la superficie está lista | Tarjeta completa, foco inicial en el campo de correo |
| **Enviando** | El acto está en curso | Acción primaria inhabilitada con indicador dentro; los campos quedan en solo lectura. **Previene el doble envío** |
| **Requisito no cumplido** | Un campo vacío | Borde de peligro en el campo, más banda de error. El texto es el mismo del requisito declarado |
| **Confirmación no coincidente** | Las dos contraseñas difieren | Banda de error que declara cuál es la discrepancia y qué hacer. **No sale ninguna solicitud hacia el servicio de datos** |
| **Error de operación** | El contrato responde que ya existe administrador | Banda de error, y derivación a `Ingreso`. Terminación controlada: no hay camino alternativo |
| **Éxito** | El alta se concretó | Navegación a `Ingreso`, que **acusa recibo** con la banda de confirmación. El lazo lo cierra la superficie siguiente, no ésta |
| **Ya aprovisionado** | El predicado es verdadero al abrir | La superficie **no arma formulario**. Redirección neutra a `Ingreso`, sin motivo a la vista |
| **Indisponible** | El servicio de datos no responde | Aviso de indisponibilidad dentro de la tarjeta, con reintento. Lo escrito se conserva salvo las contraseñas. Ver [`Wireframes-Estado-Degradado-Y-Reconexion.md`](Wireframes-Estado-Degradado-Y-Reconexion.md) |
| **Reconectando** | Se corta el circuito | Cartel de reconexión superpuesto. La tarjeta permanece a la vista |
| **Versión preliminar** | El artefacto no proviene de una línea de publicación estable | Sello con su distintivo textual contiguo |
| **Origen indeterminado** | La identidad no pudo derivarse de la construcción | Sello con marcador explícito, sin disimulo |

## 6. Versión angosta

Punto de quiebre principal en 768 px [ASUNCIÓN, tomada del documento base].

- La tarjeta pasa a ocupar el ancho disponible menos un margen, conservando su anclaje superior. **No se centra verticalmente en ningún ancho**: en pantalla baja, centrar dejaría los campos fuera de vista al abrirse el teclado en pantalla.
- El subtítulo de alcance **no se recorta ni se colapsa**. Es lo que declara la unicidad antes del intento y es la parte de la superficie que no puede perderse.
- Los campos pasan a ancho completo, que ya es su comportamiento en la versión ancha.
- El sello de versión se mantiene al pie, debajo de la tarjeta.
- Contenido legible sin desplazamiento horizontal a 320 px.

## 7. Notas de implementación

**Accesibilidad.** La superficie **mantiene un encabezado de primer nivel que nombra la tarea** pese a no tener navegación: la ausencia de chrome no puede dejar la página sin estructura semántica. El requisito declarado se asocia a su campo por descripción accesible, de modo que el lector de pantalla lo anuncie junto al control y **antes** del intento. La banda de error se anuncia como alerta y la de confirmación como estado. Foco inicial en el primer campo; tras un error, el foco vuelve a la banda o al primer campo inválido. El estado de resolución se anuncia como región activa: la espera no puede ser sólo un cambio visual. Los dos campos de contraseña declaran su propósito de credencial nueva para que el gestor de contraseñas del navegador colabore en vez de estorbar.

**Performance percibida.** La resolución del predicado es la primera cosa que ocurre en la vida de la instancia y tiene que verse resuelta, no en blanco: barra indeterminada desde el primer instante y **nunca contenido que después se retire**. El alta puede tardar: acción inhabilitada con indicador y sin cuenta regresiva.

**Internacionalización.** Español rioplatense. El texto de unicidad tolera expansión sin romper la tarjeta.

**Restricciones de arquitectura.** El formulario sale hacia el servicio de datos **desde el servidor de la pieza pública**; ningún guion del navegador participa. El mensaje de la banda **no nombra ninguna dirección de servicio interno**. El envío de credenciales se hace por petición al punto de autenticación y no por interactividad de componente, porque la credencial de sesión se emite en el ciclo de la petición y fuera del circuito de dibujo interactivo.

## 8. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Persona objetivo | El docente, en su primer contacto con la instancia |
| CU origen | [`CU-04`](../02-Especificacion-Funcional/Casos-De-Uso/CU-04-Administrar-Las-Cuentas-De-La-Comision.md), FA-03 y FA-04, con CA-01 |
| Reglas de negocio relevantes | `RN-01`, administrador único y papeles fijos |
| Restricciones transversales | `RT-03` (ningún mensaje con dirección interna), `RT-06` (sin estado propio), `RT-07` (indisponibilidad como estado degradado) |
| Marco aplicado | [`Experiencia-De-Uso.md`](Experiencia-De-Uso.md) §3.2, §3.3, §4.1, §5 |
| Representaciones que invoca | [`Representacion-Sello-De-Version.md`](Representacion-Sello-De-Version.md) |
| Catálogo de diseño aplicado | `Design-Rules-Web-Generico.md`, `Design-Rules-Blazor-Mudblazor.md` §4.2, `Design-Rules-Primer-Arranque.md` completo, `Design-Rules-Identidad-De-Version.md` §4.2 |
| US a generar en 06 | `US-08` |
| Tests previstos en 08 | Guion de demostración de la etapa `c`: alta inicial, y segunda apertura de la ruta que ya no arma formulario. Recorrido por teclado. Verificación de que ningún mensaje contiene dirección de servicio interno |

## 9. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. Superficie de primer arranque con shell partido sin chrome y sin cancelar, predicado único con corte en tres capas y redirección neutra, requisito declarado antes del intento, banda de resultado con el lazo cerrado en la superficie siguiente, destino al completar declarado explícitamente, y sello de versión en la primera de sus dos ubicaciones obligatorias. Catorce estados declarados para la Fase B2. |
| 1.0 | 2026-08-09 | Correcciones absorbidas del audit `B-02-03-GeometriaFactory-Web-r1.md` (ronda 1), **sin subir versión** por `Master-Prompt.md` §5, que lo admite mientras el documento está en estado `Propuesto`. **H-06**: la `NB-01` de la cabecera pasa a citarse con sección y criterio —§1, §5 (los cinco criterios)—, con la forma que ya usan los casos de uso de la categoría 02. |
