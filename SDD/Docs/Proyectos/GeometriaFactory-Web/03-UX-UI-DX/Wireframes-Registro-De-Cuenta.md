# Wireframes — Registro de cuenta

**Proyecto de código:** GeometriaFactory-Web
**Documento:** Wireframes-Registro-De-Cuenta.md
**Versión:** 1.2
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** UX/UI Designer + Frontend Lead (AG-03)
**Variante:** UX/UI
**Trazabilidad upstream:** `../02-Especificacion-Funcional/Casos-De-Uso/CU-01-Registrar-La-Cuenta-De-Alumno.md` íntegro; `../02-Especificacion-Funcional/Especificacion-Funcional.md` §6 (RT-01, RT-03, RT-06, RT-07); `../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md` §1, §5 (primero y tercer criterio); `NB-01` §1, §5 (segundo criterio); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.14**, §4 (F-02), §4.1 (RN-02, RN-06), §6 (flujo 1), §9 (X-1, **X-2 retirada**), §17.6 P.3 y P.5; `Design-Rules-Web-Generico.md` §3.1, §4.6, §4.9, §5, §7; `Design-Rules-Blazor-Mudblazor.md` §4.2
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

**Nombre canónico de superficie: `Registro-De-Cuenta`.**

El alumno se da de alta en el laboratorio con su correo, su nombre y su apellido, **sin elegir contraseña**, y se entera en el mismo acto de que su cuenta queda a la espera de que el administrador la habilite. Es, junto con `Ingreso`, una de las **dos únicas rutas públicas** del producto.

Su trabajo real no es recoger tres campos: es **evitar que el alumno se quede esperando un correo que no va a llegar**. Todo el diseño de la superficie está orientado a que esa expectativa no se forme.

## 2. Layout

Shell de acceso, sin navegación.

```text
+---------------------- lienzo, sin chrome ------------------------+
|              +-------- ancho acotado ~380px --------+            |
|              |  [ico] Fábrica de Geometría          |            |
|              |                                      |            |
|              |  Registrarte en el laboratorio       |  h1        |
|              |  Tu cuenta queda a la espera de que  |  qué va a  |
|              |  el docente la habilite. El          |  pasar,    |
|              |  laboratorio no envía correos.       |  antes     |
|              |                                      |            |
|              |  [ banda de resultado  rol=alerta  ] |  condic.   |
|              |                                      |            |
|              |  Correo                              |            |
|              |  [____________________________]      |            |
|              |  Nombre                              |            |
|              |  [____________________________]      |            |
|              |  Apellido                            |            |
|              |  [____________________________]      |            |
|              |                                      |            |
|              |  [======== Registrarme ===========]  |            |
|              |                                      |            |
|              |  ¿Ya tenés cuenta? Ingresar          |  enlace    |
|              +--------------------------------------+            |
|                    Versión 1.4.2                                 |
+------------------------------------------------------------------+
```

**Tres campos y ningún campo de contraseña.** Es criterio de aceptación verificable por recuento, y la ausencia es la característica principal de la superficie, no un olvido.

Estado de éxito, que reemplaza el contenido de la tarjeta:

```text
              +-------- ancho acotado ~380px --------+
              |  [ico check]                         |
              |  Tu cuenta quedó registrada          |  h1
              |  Todavía no podés ingresar: el       |
              |  docente tiene que habilitarla.      |
              |  No vas a recibir ningún correo.     |
              |  [========= Ir a ingresar =========] |
              +--------------------------------------+
```

## 3. Componentes principales

| Componente | Patrón del catálogo | Propósito | Datos que muestra | Comportamiento |
| --- | --- | --- | --- | --- |
| Tarjeta de acceso | Primer arranque §4.2, reusada | Contener el formulario | — | Ancho acotado, anclada arriba |
| Subtítulo de expectativa | Base §2.2 | **Declarar antes del intento** que la cuenta queda a la espera y que no hay correo | Texto fijo | Inerte. No se colapsa en ningún ancho |
| Banda de resultado | Primer arranque §4.4 | Comunicar el resultado | Texto resuelto desde el código del contrato | Condicional, con rol de alerta |
| Campo de correo | Base §4.6 | Identidad de la cuenta | Lo escrito | Etiqueta visible arriba. Declara su propósito para el autocompletado del navegador |
| Campos de nombre y apellido | Base §4.6 | Identificar a la persona en la lista del docente | Lo escrito | Ídem |
| Acción primaria | Base §4.9 | Registrar | Verbo exacto: «Registrarme» | Ancho completo. Se inhabilita con indicador durante el envío |
| Enlace a `Ingreso` | Base §4.9 | Salida hacia la otra ruta pública | «¿Ya tenés cuenta? Ingresar» | Es la **única** salida de la superficie |
| Bloque de éxito | Base §5 | Cerrar el lazo | Qué quedó creado y qué falta | Reemplaza el formulario. Su acción lleva a `Ingreso` |
| Sello de versión | [`Representacion-Sello-De-Version.md`](Representacion-Sello-De-Version.md) | Identificar la instancia | Versión legible | Al pie. Ubicación obligatoria de superficie de acceso |

**Lo que esta superficie no dibuja, y se declara para que la ausencia sea deliberada:** ningún campo de contraseña, ningún enlace de recuperación —**no existe recuperación autónoma en este producto**: sin canal de correo no hay forma de que la persona lo resuelva sola, y desde el `PRODUCT-INTAKE` 1.7 el remedio de un olvido es el **reseteo que ejerce el docente** desde `Panel-De-Cuentas`, que tampoco se pide desde acá—, ninguna casilla de términos, ninguna verificación de dirección de correo.

## 4. Interacciones

| Acción | Disparador | Resultado esperado | Precondición |
| --- | --- | --- | --- |
| Abrir la ruta | Entrada directa o enlace desde `Ingreso` | La superficie se arma sin sesión. Es ruta pública | El laboratorio está aprovisionado |
| Escribir en un campo | Tecleo | Sin ida y vuelta. **Ninguna verificación de unicidad del correo mientras se escribe**: la decide el servicio de datos y consultarla al tipear violaría la regla de que ningún guion del navegador lo invoca | — |
| Registrarse | Acción primaria o ingreso desde el último campo | Se verifica que los tres campos estén completos y, si lo están, se envía el alta | Tres campos completos |
| Corregir tras un error | Acción primaria | Se reemplaza la banda y el foco vuelve al campo señalado. **El formulario conserva lo ya escrito** | Hubo un error previo |
| Volver a registrarse con un correo ya usado | Acción primaria | La superficie **no bloquea el acceso a la ruta**: el intento sale y vuelve con el mensaje sobre el campo de correo, **sin revelar ningún dato de la cuenta existente** | — |
| Ir a ingresar | Enlace o acción del bloque de éxito | Navegación a `Ingreso` | — |

## 5. Estados

| Estado | Condición que lo produce | Representación esperada |
| --- | --- | --- |
| **Vacío** | **No aplica**: la superficie no presenta ninguna colección | Se declara para que la ausencia sea deliberada |
| **Cargando** | La superficie se está armando | Esqueleto de tres campos dentro de la tarjeta |
| **Con datos** | Formulario listo | Tarjeta completa, foco inicial en el campo de correo |
| **Enviando** | El alta está en curso | Acción primaria inhabilitada con indicador dentro. **Previene el doble envío** |
| **Requisito no cumplido** | Falta uno de los tres campos | Borde de peligro en el campo faltante y banda de error. **No sale ninguna solicitud hacia el servicio de datos** |
| **Error de operación · correo ya registrado** | El correo ya pertenece a una cuenta | Mensaje explícito **sobre el campo de correo**, sin revelar ningún dato de la cuenta existente. Recuperación: corregir y reintentar |
| **Error de operación · campo que el contrato nombra** | La solicitud llegó incompleta pese a la verificación previa | Se señala el campo que el contrato nombra |
| **Éxito** | La cuenta quedó creada | El formulario se reemplaza por el bloque de éxito, que declara que **todavía no puede ingresar** y que **no se envió ningún correo** |
| **Indisponible** | El servicio de datos no responde | Aviso de indisponibilidad dentro de la tarjeta. **Conserva lo escrito** y deja reintentar. El mensaje no nombra ninguna dirección de servicio interno. Ver [`Wireframes-Estado-Degradado-Y-Reconexion.md`](Wireframes-Estado-Degradado-Y-Reconexion.md) |
| **Reconectando** | Se corta el circuito | Cartel de reconexión superpuesto; la tarjeta permanece |

## 6. Versión angosta

- La tarjeta toma el ancho disponible menos un margen, conservando el anclaje superior.
- **El subtítulo de expectativa no se recorta.** Es la parte que evita la espera de un correo inexistente y es lo último que puede perderse.
- Los tres campos y la acción primaria ya son de ancho completo.
- El enlace a `Ingreso` y el sello de versión se mantienen al pie.
- Legible sin desplazamiento horizontal a 320 px.

## 7. Notas de implementación

**Accesibilidad.** Encabezado de primer nivel que nombra la tarea, pese a la ausencia de navegación. Etiqueta visible por campo; **el texto de ejemplo no sustituye a la etiqueta**. El mensaje de error se asocia al campo que describe. La banda de error se anuncia como alerta y el bloque de éxito como estado. Foco inicial en el correo; tras un error, foco en el primer campo inválido. Los campos declaran su propósito para que el autocompletado del navegador colabore.

**Performance percibida.** El alta cruza dos saltos. Acción inhabilitada con indicador desde el primer instante y sin cuenta regresiva.

**Internacionalización.** Español rioplatense, segunda persona. El texto de expectativa tolera expansión.

**Restricciones de arquitectura.** El alta sale **desde el servidor de la pieza pública**; el recorrido completo tiene que producir cero peticiones originadas por el navegador hacia el servicio de datos. Ningún mensaje incluye la dirección de un servicio interno. La superficie **no guarda estado propio**: si el envío falla, no queda cuenta a medio crear de este lado.

## 8. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Persona objetivo | El alumno de la comisión, en su primer contacto con el producto |
| CU origen | [`CU-01`](../02-Especificacion-Funcional/Casos-De-Uso/CU-01-Registrar-La-Cuenta-De-Alumno.md) íntegro |
| Reglas de negocio relevantes | `RN-02` (correo único), `RN-06` (cuenta pendiente o bloqueada sin acceso) |
| Restricciones transversales | `RT-01`, `RT-03`, `RT-06`, `RT-07` |
| Marco aplicado | [`Experiencia-De-Uso.md`](Experiencia-De-Uso.md) §1.3, §3.2, §3.4, §4.1, §8 |
| Representaciones que invoca | [`Representacion-Sello-De-Version.md`](Representacion-Sello-De-Version.md) |
| Catálogo de diseño aplicado | `Design-Rules-Web-Generico.md`, `Design-Rules-Blazor-Mudblazor.md` §4.2 |
| US a generar en 06 | `US-01`, `US-02` |
| Tests previstos en 08 | Guion de demostración de la etapa `d`: recuento de tres campos y cero campos de contraseña; alta con correo nuevo; alta repetida con el mismo correo; servicio detenido con el mensaje sin dirección interna; recuento de peticiones del navegador con umbral 0 |

## 9. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. Superficie pública de alta, con el subtítulo de expectativa que declara antes del intento que la cuenta queda a la espera y que el laboratorio no envía correos, el recuento verificable de tres campos y ningún campo de contraseña, la enumeración explícita de lo que la superficie no dibuja —incluida la ausencia de recuperación— y diez estados declarados para la Fase B2. |
| 1.0 | 2026-08-09 | Correcciones absorbidas del audit `B-02-03-GeometriaFactory-Web-r1.md` (ronda 1), **sin subir versión** por `Master-Prompt.md` §5, que lo admite mientras el documento está en estado `Propuesto`. **H-06**: las `NB-02` y `NB-01` de la cabecera pasan a citarse con sección y criterio numerado. |
| 1.1 | 2026-08-09 | **Propagación del `PRODUCT-INTAKE` 1.7**, que **retiró la exclusión X-2**. §3 corrige la única afirmación de este documento que 1.7 volvió falsa: decía que «no existe recuperación en este producto» y lo que no existe es la recuperación **autónoma**; el reseteo por el docente sí existe, aunque **tampoco se pide desde esta superficie**. Es el único cambio: la superficie de registro no gana ni pierde ningún componente, y sigue teniendo tres campos y ningún campo de contraseña. |
| 1.2 | 2026-08-10 | **Cierra el hallazgo `C-08` (P2) del informe de auditoría `SDD/Docs/Audit/Coherencia-Corpus-r1.md` 1.0.** La cabecera de trazabilidad declaraba derivarse del `PRODUCT-INTAKE` **1.7**, versión archivada, y pasa a declarar la **1.14**, vigente. La **1.7** es la versión cuya letra sobre **RN-13** e **INV-09** fue precisada en la 1.8 y corregida en la 1.14, que es exactamente el punto donde el corpus más se equivocó. Se revisó el cuerpo antes de mover la cabecera y **no arrastra ninguna decisión de las versiones intermedias**: no queda en él ningún recuento de «quince reglas» ni de «diecisiete códigos», ninguna cita a la exclusión **X-2** como vigente y ninguna afirmación de que la marca de cambio de contraseña pendiente la ponga únicamente el reseteo. **Ningún contenido normativo de este documento cambia: la corrección es de trazabilidad.** Sube minor. |
