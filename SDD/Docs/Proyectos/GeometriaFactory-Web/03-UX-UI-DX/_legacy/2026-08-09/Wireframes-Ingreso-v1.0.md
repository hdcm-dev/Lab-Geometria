# Wireframes — Ingreso

**Proyecto de código:** GeometriaFactory-Web
**Documento:** Wireframes-Ingreso.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** UX/UI Designer + Frontend Lead (AG-03)
**Variante:** UX/UI
**Trazabilidad upstream:** `../02-Especificacion-Funcional/Casos-De-Uso/CU-02-Iniciar-Y-Cerrar-Sesion-Sin-Exponer-La-Credencial.md` íntegro —§4, FA-01 a FA-06, §6 y CA-01 a CA-07—; `../02-Especificacion-Funcional/Especificacion-Funcional.md` §6 (RT-01, RT-02, RT-03, RT-09); `../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md` §1, §5 (tercer, cuarto y quinto criterio); `NB-01` §5 (segundo criterio); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-05), §4.1 (RN-01, RN-06), §6 (flujo 1), §9 (X-2), §14 (RA-01, RA-03), §17.6 P.5 y P.11 punto 1; `Design-Rules-Web-Generico.md` §3.1, §4.6, §4.9, §5, §7; `Design-Rules-Blazor-Mudblazor.md` §4.2; `Design-Rules-Identidad-De-Version.md` §4.2
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

**Nombre canónico de superficie: `Ingreso`.**

La persona presenta su correo y su contraseña y entra al panel de su papel. Es también la superficie a la que vuelve todo lo que no tiene sesión: el cierre voluntario, el guard de una ruta protegida, la sesión que no se pudo restablecer y la redirección neutra del aprovisionamiento ya completado.

Es la superficie que **acusa recibo** de los tres actos que ocurren fuera de ella —aprovisionamiento inicial, registro y establecimiento de contraseña— con una banda de confirmación. Sin ese acuse, esos tres actos terminarían en silencio.

Su decisión más consecuente no se ve: **la credencial de sesión queda del lado del servidor, en el estado del circuito, y no llega nunca al navegador**. El navegador conserva sólo una marca de sesión que no la transporta.

## 2. Layout

Shell de acceso, sin navegación.

```text
+---------------------- lienzo, sin chrome ------------------------+
|              +-------- ancho acotado ~380px --------+            |
|              |  [ico] Fábrica de Geometría          |            |
|              |                                      |            |
|              |  Ingresar al laboratorio             |  h1        |
|              |                                      |            |
|              |  [ banda de resultado  rol=alerta  ] |  condic.   |
|              |  [ banda de confirm.   rol=estado  ] |  condic.   |
|              |                                      |            |
|              |  Correo                              |            |
|              |  [____________________________]      |            |
|              |  Contraseña                          |            |
|              |  [____________________________]      |            |
|              |                                      |            |
|              |  [========= Ingresar =============]  |            |
|              |                                      |            |
|              |  ¿No tenés cuenta? Registrarte       |  enlace    |
|              |  Si olvidaste tu contraseña, pedile  |  nota, no  |
|              |  al docente que te dé de alta otra   |  es enlace |
|              |  vez. Tené en cuenta que eso borra   |            |
|              |  también tus trabajos.               |            |
|              +--------------------------------------+            |
|                    Versión 1.4.2   [preliminar]                  |
+------------------------------------------------------------------+
```

**No hay enlace de recuperación de contraseña y hay una nota en su lugar.** No existe canal de correo y por lo tanto no existe recuperación. Dibujar un enlace llevaría a un lugar que no existe; callarlo dejaría a la persona buscándolo. La nota dice qué hacer y **advierte la consecuencia**, que es la parte que importa: el remedio arrastra los trabajos.

## 3. Componentes principales

| Componente | Patrón del catálogo | Propósito | Datos que muestra | Comportamiento |
| --- | --- | --- | --- | --- |
| Tarjeta de acceso | Primer arranque §4.2, reusada | Contener el formulario | — | Ancho acotado, anclada arriba |
| Banda de resultado, variante error | Primer arranque §4.4 | Comunicar el rechazo | Texto resuelto desde el código del contrato | Condicional, con rol de alerta |
| Banda de resultado, variante confirmación | Primer arranque §4.4 | **Acusar recibo** de un acto ocurrido en otra superficie | Qué quedó hecho y qué sigue | Condicional, con rol de estado. Se retira al primer intento de ingreso |
| Campo de correo | Base §4.6 | Identidad | Lo escrito | Declara su propósito para el autocompletado |
| Campo de contraseña | Base §4.6 | Credencial | Enmascarado, con conmutador de visibilidad | Ídem, con propósito de contraseña vigente |
| Acción primaria | Base §4.9 | Ingresar | Verbo exacto: «Ingresar» | Ancho completo. Se inhabilita con indicador durante el canje |
| Enlace a `Registro-De-Cuenta` | Base §4.9 | Salida hacia la otra ruta pública | «¿No tenés cuenta? Registrarte» | — |
| Nota sobre la contraseña olvidada | Base §5 | Declarar que no hay recuperación y cuál es el remedio, con su consecuencia | Texto fijo | **Inerte: no es un enlace y no dispara nada** |
| Sello de versión | [`Representacion-Sello-De-Version.md`](Representacion-Sello-De-Version.md) | Identificar la instancia | Versión legible, distintivo, marcador | Al pie. **Ubicación obligatoria**: es la única información disponible sobre la instancia para quien no puede entrar, que es justamente cuando más se la necesita |

## 4. Interacciones

| Acción | Disparador | Resultado esperado | Precondición |
| --- | --- | --- | --- |
| Abrir la ruta | Entrada directa, o redirección desde cualquier guard | La superficie se arma. Si viene de un guard, **no se revela qué había en la ruta pedida** | El laboratorio está aprovisionado |
| Ingresar | Acción primaria o ingreso desde la contraseña | Se canjean las credenciales desde el servidor; con éxito, la persona aterriza en la ruta inicial de su papel | Los dos campos completos |
| Conmutar la visibilidad de la contraseña | Activación del control | El campo alterna entre enmascarado y visible | — |
| Ingresar con la cuenta a la espera de habilitación, o bloqueada | Acción primaria | Se muestra el motivo tal como corresponde a la situación de la cuenta y **no se otorga sesión**. El navegador no recibe marca de sesión | — |
| Ingresar con la contraseña todavía sin establecer | Acción primaria | Derivación a `Credencial-Propia` en su curso de establecimiento. **No se otorga sesión en ese intento** | Cuenta habilitada y sin contraseña |
| Cerrar sesión | Acción de la barra lateral, desde cualquier superficie del shell de trabajo | Se descarta la credencial del estado del circuito, se invalida la marca de sesión y se vuelve acá con la banda de confirmación | Sesión iniciada |
| Pedir una ruta protegida sin sesión | Entrada directa por dirección | Se vuelve acá **sin revelar qué contenía la ruta pedida** | — |
| Abrir el detalle de diagnóstico | Activación del sello | Se despliega el contrato completo con copiado en un solo gesto | — |

**Lo que ninguna interacción hace:** consultar al servicio de datos mientras se escribe, y distinguir en el mensaje si falló el correo o la contraseña.

## 5. Estados

| Estado | Condición que lo produce | Representación esperada |
| --- | --- | --- |
| **Vacío** | **No aplica**: no presenta ninguna colección | Se declara para que la ausencia sea deliberada |
| **Cargando** | La superficie se está armando | Esqueleto de dos campos |
| **Con datos** | Formulario listo | Tarjeta completa, foco inicial en el correo |
| **Enviando** | El canje está en curso | Acción inhabilitada con indicador dentro. **Previene el doble envío** |
| **Requisito no cumplido** | Falta el correo o la contraseña | Borde de peligro en el campo y banda de error |
| **Credencial rechazada** | El correo o la contraseña no corresponden | **Un único mensaje que no declara cuál de los dos falló.** Distinguirlos confirmaría la existencia de la identidad. Sin reintento automático |
| **Cuenta no habilitada** | La cuenta está a la espera de habilitación, o bloqueada | Banda de error con el motivo que corresponde a la situación de la cuenta. **No hay sesión y el navegador no recibe marca de sesión** |
| **Contraseña sin establecer** | Primer ingreso efectivo de una cuenta ya habilitada | Derivación a `Credencial-Propia`. No es un error y no se presenta como tal |
| **Confirmación de aprovisionamiento** | Se acaba de crear la cuenta de administrador | Banda de confirmación que declara qué quedó creado y que el paso siguiente es entrar |
| **Confirmación de registro** | Se acaba de registrar una cuenta | Banda de confirmación que recuerda que falta la habilitación |
| **Confirmación de contraseña establecida** | Se acaba de fijar la contraseña | Banda de confirmación que declara que ya se puede entrar |
| **Sesión cerrada** | La persona cerró sesión | Banda de confirmación de cierre. Toda ruta del panel vuelve a exigir ingreso |
| **Sesión vencida o no restablecible** | El circuito se perdió y la sesión no se pudo restablecer | Banda que declara el estado de la sesión. **No es un error arbitrario en una acción cualquiera**: se vuelve acá con el motivo declarado |
| **Éxito** | El canje procedió | Navegación a la ruta inicial del papel: el listado propio si es alumno, el de la comisión si es administrador |
| **Indisponible** | El servicio de datos no responde | Aviso de indisponibilidad dentro de la tarjeta, con reintento y sin dirección de servicio interno. Ver [`Wireframes-Estado-Degradado-Y-Reconexion.md`](Wireframes-Estado-Degradado-Y-Reconexion.md) |
| **Reconectando** | Se corta el circuito | Cartel de reconexión superpuesto |
| **Versión preliminar** / **Origen indeterminado** | Según el contrato de identidad de versión | Sello con distintivo o con marcador, textual en los dos casos |

## 6. Versión angosta

- La tarjeta toma el ancho disponible menos un margen, anclada arriba. **No se centra verticalmente**: con el teclado en pantalla abierto, centrar deja los campos fuera de vista.
- **La nota sobre la contraseña olvidada no se colapsa ni se recorta.** Es lo que evita que la persona busque un enlace que no existe, y su advertencia sobre el arrastre de trabajos es la parte que no puede perderse.
- El sello de versión se mantiene al pie, debajo de la tarjeta.
- Legible sin desplazamiento horizontal a 320 px.

## 7. Notas de implementación

**Accesibilidad.** Encabezado de primer nivel pese a la ausencia de navegación. Etiqueta visible por campo. La banda de error se anuncia como alerta y la de confirmación como estado: **las cuatro confirmaciones que esta superficie acusa se pierden si sólo cambian visualmente**. Foco inicial en el correo; tras un rechazo, el foco vuelve a la banda. El conmutador de visibilidad de la contraseña declara su estado. Los campos declaran su propósito de credencial vigente.

**Performance percibida.** El canje cruza dos saltos y es de los más lentos del producto. Acción inhabilitada con indicador y sin cuenta regresiva.

**Internacionalización.** Español rioplatense, segunda persona. El mensaje de rechazo es único y corto, y no se compone con partes que dependan del idioma del contrato.

**Restricciones de arquitectura.** El formulario se envía por petición al punto de autenticación y **no por interactividad de componente**: la credencial de sesión se emite en el ciclo de la petición, fuera del circuito de dibujo interactivo. El cierre de sesión es también un envío y no un enlace de navegación. **La credencial de sesión no aparece en el navegador**, ni en el documento, ni en el almacenamiento, ni en el cuerpo de ninguna respuesta que el navegador reciba: es criterio verificable con las herramientas de desarrollo. Ningún mensaje incluye la dirección de un servicio interno. **El rechazo del guard es neutro**: no se revela qué contenía la ruta pedida.

## 8. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Persona objetivo | Alumno y docente por igual: la superficie es la misma y sólo cambia el destino |
| CU origen | [`CU-02`](../02-Especificacion-Funcional/Casos-De-Uso/CU-02-Iniciar-Y-Cerrar-Sesion-Sin-Exponer-La-Credencial.md) íntegro |
| Reglas de negocio relevantes | `RN-06` (cuenta pendiente o bloqueada sin acceso), `RN-01` (papeles fijos) |
| Restricciones transversales | `RT-01`, `RT-02`, `RT-03`, `RT-09` |
| Marco aplicado | [`Experiencia-De-Uso.md`](Experiencia-De-Uso.md) §3.2, §3.4, §4.1, §8.2 |
| Representaciones que invoca | [`Representacion-Sello-De-Version.md`](Representacion-Sello-De-Version.md) |
| Catálogo de diseño aplicado | `Design-Rules-Web-Generico.md`, `Design-Rules-Blazor-Mudblazor.md` §4.2, `Design-Rules-Identidad-De-Version.md` §4.2 |
| US a generar en 06 | `US-03`, `US-04`, `US-05` |
| Tests previstos en 08 | Guion de demostración de la etapa `c`, con la inspección del navegador que verifica cero apariciones de la credencial de sesión; etapa `d` para la cuenta a la espera de habilitación y para la derivación al establecimiento de contraseña; ruta de administrador pedida por un alumno con sesión; recorrido por teclado |

## 9. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. Superficie de acceso con las cuatro bandas de confirmación que acusan recibo de los actos ocurridos fuera de ella, rechazo de credenciales indiferenciado, nota sobre la contraseña olvidada que declara la ausencia de recuperación y advierte el arrastre de trabajos en lugar de dibujar un enlace inexistente, sello de versión en su segunda ubicación obligatoria con su detalle de diagnóstico, y diecisiete estados declarados para la Fase B2. |
| 1.0 | 2026-08-09 | Correcciones absorbidas del audit `B-02-03-GeometriaFactory-Web-r1.md` (ronda 1), **sin subir versión** por `Master-Prompt.md` §5, que lo admite mientras el documento está en estado `Propuesto`. **H-06**: las `NB-02` y `NB-01` de la cabecera pasan a citarse con sección y criterio numerado. |
