# Wireframes — Credencial propia

**Proyecto de código:** GeometriaFactory-Web
**Documento:** Wireframes-Credencial-Propia.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** UX/UI Designer + Frontend Lead (AG-03)
**Variante:** UX/UI
**Trazabilidad upstream:** `../02-Especificacion-Funcional/Casos-De-Uso/CU-03-Establecer-Y-Cambiar-La-Contrasena-Propia.md` íntegro; `../02-Especificacion-Funcional/Casos-De-Uso/CU-02-Iniciar-Y-Cerrar-Sesion-Sin-Exponer-La-Credencial.md` FA-02; `../02-Especificacion-Funcional/Especificacion-Funcional.md` §6 (RT-02, RT-03, RT-06); `../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md` §1, §5 (segundo y cuarto criterio); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-04, F-05), §6 (flujo 1), §9 (X-1, X-2), §11 (RN-B6), §17.6 P.5; `Design-Rules-Web-Generico.md` §3.1, §4.4, §4.6, §4.9, §5, §7; `Design-Rules-Primer-Arranque.md` §4.5; `Design-Rules-Blazor-Mudblazor.md` §4.2
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

La persona fija su contraseña por primera vez, o la reemplaza presentando la vigente. Es la **única** forma que tiene de administrar su credencial dentro del laboratorio: no hay recuperación y no hay canal de correo.

**Una superficie con dos cursos y no dos superficies.** Es el mismo objeto —la credencial propia—, el mismo actor y el mismo formulario salvo un campo. Lo que sí cambia entre los dos cursos es el **shell**, y por eso los dos se declaran acá con su propio recorrido:

| Curso | Cuándo | Shell | Cómo se llega | A dónde va al terminar |
| --- | --- | --- | --- | --- |
| **Establecimiento** | Primer ingreso efectivo de una cuenta ya habilitada, todavía sin contraseña | **Acceso**, sin navegación: la persona todavía no tiene sesión | Derivada desde `Ingreso` | A `Ingreso`, con banda de confirmación |
| **Cambio** | La persona ya está dentro y quiere reemplazarla | **Trabajo**, con la barra lateral de su papel | Destino «Mi contraseña» de la barra lateral | Al panel de la persona, **con la sesión vigente** |

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

**El curso de establecimiento no tiene «cancelar» y el de cambio sí.** En el establecimiento no hay estado previo al que volver: la persona no tiene sesión y abandonar la deja fuera. En el cambio hay un panel al que volver, y la salida es legítima.

## 3. Componentes principales

| Componente | Patrón del catálogo | Propósito | Datos que muestra | Comportamiento |
| --- | --- | --- | --- | --- |
| Tarjeta de credencial | Primer arranque §4.2 / Base §4.4 | Contener el formulario | — | Ancho acotado en los dos cursos |
| Subtítulo de motivo | Base §2.2 | Explicar **por qué** la persona está acá | Texto distinto por curso | Inerte. En el establecimiento declara que el laboratorio nunca envió una contraseña |
| Banda de resultado | Primer arranque §4.4 | Comunicar el resultado | Texto resuelto desde el código del contrato | Condicional, rol de alerta |
| Campo de contraseña actual | Base §4.6 | Presentar la vigente | Enmascarado | **Sólo en el curso de cambio. Es obligatorio por contrato** |
| Campos de contraseña nueva y repetición | Base §4.6 | Fijar la credencial | Enmascarados, con conmutador de visibilidad | La coincidencia se verifica **antes** de salir hacia el servicio de datos |
| Requisito declarado | Primer arranque §4.5 | Enunciar la regla de forma **antes** de que la persona escriba | Texto derivado de la política del sistema | Asociado al campo. **No aparece recién al fallar** |
| Acción primaria | Base §4.9 | Guardar | Verbo exacto: «Guardar contraseña» | Se inhabilita con indicador durante el envío |
| Acción secundaria | Base §4.9 | Volver sin cambiar nada | «Cancelar» | **Sólo en el curso de cambio** |
| Sello de versión | [`Representacion-Sello-De-Version.md`](Representacion-Sello-De-Version.md) | Identificar la instancia | Versión legible | Al pie de la tarjeta en el establecimiento; en la barra lateral en el cambio |

**Lo que esta superficie no dibuja:** ningún medidor de fortaleza que prometa una política que el producto no fija, ninguna opción de «recordarme», ningún enlace de recuperación.

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

## 5. Estados

| Estado | Condición que lo produce | Representación esperada |
| --- | --- | --- |
| **Vacío** | **No aplica**: no presenta ninguna colección | Se declara para que la ausencia sea deliberada |
| **Cargando** | La superficie se está armando | Esqueleto de dos o tres campos según el curso |
| **Con datos** | Formulario listo | Tarjeta completa, foco inicial en el primer campo |
| **Enviando** | El cambio está en curso | Acción inhabilitada con indicador. **Previene el doble envío** |
| **Curso de establecimiento** | Cuenta habilitada sin contraseña | Dos campos, sin «cancelar», sobre el shell de acceso |
| **Curso de cambio** | Persona con sesión | Tres campos, con «cancelar», sobre el shell de trabajo |
| **Requisito no cumplido** | Falta un campo | Borde de peligro en el campo y banda de error |
| **Confirmación no coincidente** | Las dos escrituras de la nueva difieren | Banda de error que declara la discrepancia y qué hacer. **No sale ninguna solicitud hacia el servicio de datos** |
| **Contraseña actual rechazada** | La vigente no corresponde, o llegó ausente | Mensaje **sobre el campo de contraseña actual**. El cambio no se aplica |
| **Cuenta bloqueada entre la derivación y el envío** | La situación de la cuenta cambió mientras tanto | Se muestra el motivo y se vuelve a `Ingreso`, **sin establecer contraseña** |
| **Éxito de establecimiento** | La contraseña quedó fijada | Navegación a `Ingreso` con banda de confirmación. Es el camino de entrada a partir de ahora |
| **Éxito de cambio** | La contraseña quedó reemplazada | Vuelta al panel con confirmación. **La sesión vigente se conserva** y la contraseña anterior deja de servir |
| **Indisponible** | El servicio de datos no responde | Aviso de indisponibilidad con reintento, sin dirección de servicio interno. Ver [`Wireframes-Estado-Degradado-Y-Reconexion.md`](Wireframes-Estado-Degradado-Y-Reconexion.md) |
| **Reconectando** | Se corta el circuito | Cartel de reconexión superpuesto |

## 6. Versión angosta

- La tarjeta toma el ancho disponible menos un margen. En el curso de establecimiento conserva el anclaje superior y **no se centra verticalmente**, por el teclado en pantalla.
- En el curso de cambio, las dos acciones del pie pasan a ancho completo apiladas, con la primaria **arriba**: es la que se busca, y dejarla debajo del pliegue en pantalla baja obliga a desplazarse para completar la tarea.
- **El requisito declarado no se colapsa.** Es lo que evita el juego de adivinanzas al fallar.
- La barra lateral colapsa según el patrón del documento base en el curso de cambio.
- Legible sin desplazamiento horizontal a 320 px.

## 7. Notas de implementación

**Accesibilidad.** Encabezado de primer nivel en los dos cursos. El requisito declarado se asocia a su campo por descripción accesible, para que se anuncie **junto al control y antes del intento**. La banda de error se anuncia como alerta y la confirmación como estado. Foco inicial en el primer campo; tras un error, en el primer campo inválido. Los tres campos declaran su propósito —contraseña vigente y contraseña nueva— para que el gestor del navegador colabore en vez de ofrecer la anterior. El conmutador de visibilidad declara su estado.

**Performance percibida.** Acción inhabilitada con indicador desde el primer instante.

**Internacionalización.** Español rioplatense, segunda persona. El requisito declarado tolera expansión sin romper la tarjeta.

**Restricciones de arquitectura.** El envío va por petición al punto correspondiente y **no por interactividad de componente**. La pieza pública **no conserva ninguna contraseña**: nada de lo que se escribe acá sobrevive a la operación, no se escribe en el navegador y no se incluye en ningún mensaje. Ningún mensaje incluye la dirección de un servicio interno.

## 8. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Persona objetivo | Alumno y docente por igual |
| CU origen | [`CU-03`](../02-Especificacion-Funcional/Casos-De-Uso/CU-03-Establecer-Y-Cambiar-La-Contrasena-Propia.md) íntegro, con [`CU-02`](../02-Especificacion-Funcional/Casos-De-Uso/CU-02-Iniciar-Y-Cerrar-Sesion-Sin-Exponer-La-Credencial.md) FA-02 como vía de llegada |
| Reglas de negocio relevantes | `RN-06` (cuenta pendiente o bloqueada sin acceso) |
| Restricciones transversales | `RT-02`, `RT-03`, `RT-06` |
| Marco aplicado | [`Experiencia-De-Uso.md`](Experiencia-De-Uso.md) §3.2, §3.4, §4.1, §8 |
| Representaciones que invoca | [`Representacion-Sello-De-Version.md`](Representacion-Sello-De-Version.md) |
| Catálogo de diseño aplicado | `Design-Rules-Web-Generico.md`, `Design-Rules-Primer-Arranque.md` §4.5, `Design-Rules-Blazor-Mudblazor.md` §4.2 |
| US a generar en 06 | `US-06`, `US-07` |
| Tests previstos en 08 | Guion de demostración de la etapa `d` para el establecimiento en el primer ingreso efectivo, y de la etapa `c` para el cambio exigiendo la vigente; dos escrituras distintas sin solicitud emitida; inspección del navegador sin contraseña observable; recorrido por teclado |

## 9. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. Una superficie con dos cursos y dos shells, con la tabla que declara cuándo rige cada uno y por qué sólo el de cambio tiene salida. Requisito declarado antes del intento y derivado de la política del sistema en lugar de transcrito, enumeración de lo que la superficie no dibuja, y catorce estados declarados para la Fase B2. |
| 1.0 | 2026-08-09 | Correcciones absorbidas del audit `B-02-03-GeometriaFactory-Web-r1.md` (ronda 1), **sin subir versión** por `Master-Prompt.md` §5, que lo admite mientras el documento está en estado `Propuesto`. **H-06**: la `NB-02` de la cabecera pasa a citarse con sección y criterio —§1, §5 (segundo y cuarto criterio)—, con la forma que ya usan los casos de uso de la categoría 02. |
