# CU-04 — Administrar las cuentas de la comisión

**Proyecto de código:** GeometriaFactory-Web
**Documento:** CU-04-Administrar-Las-Cuentas-De-La-Comision.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Analista Funcional senior (AG-02)
**Trazabilidad upstream:** `../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-01-Control-De-Admision-Al-Laboratorio.md` §1, §5 (los cinco criterios); `../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md` §5 (tercer criterio); `../../../../00-Contexto/Alcance-Producto.md` §4.1, §5; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-01, F-03), §4.1 (RN-01, RN-06, RN-07), §6 (flujo 1), §7 (CL-6, CL-7), §9 (X-3), §11 (RN-B6), §17.6 P.5
**Trazabilidad downstream:** `03-UX-UI-DX` de este proyecto de código; `05-Arquitectura-Tecnica`; `06-Backlog-Tecnico`; `08-Calidad-Y-Pruebas`

---

## Tabla de contenido

- [1. Propósito](#1-propósito)
- [2. Actores](#2-actores)
- [3. Precondiciones](#3-precondiciones)
- [4. Flujo principal](#4-flujo-principal)
- [5. Flujos alternativos](#5-flujos-alternativos)
- [6. Excepciones y errores](#6-excepciones-y-errores)
- [7. Postcondiciones](#7-postcondiciones)
- [8. Criterios de aceptación](#8-criterios-de-aceptación)
- [9. Trazabilidad](#9-trazabilidad)
- [10. Notas y supuestos](#10-notas-y-supuestos)
- [11. Control de cambios](#11-control-de-cambios)
- [13. Interacción multiusuario y concurrencia](#13-interacción-multiusuario-y-concurrencia)

---

## 1. Propósito

Darle al administrador el control mínimo y suficiente sobre la lista de su comisión: ver las cuentas con su situación, habilitar, bloquear y rehabilitar, y dar de baja con una confirmación escrita que declara que la baja elimina también los trabajos de esa cuenta. Incluye la configuración de la cuenta de administrador en el primer arranque, que sólo es posible mientras no exista ninguna.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Administrador | Primario | Recorre la lista de cuentas y ejecuta las cuatro operaciones sobre cada una |
| Pieza pública | Sistema | Arma el panel de cuentas, exige la confirmación escrita de la baja e invoca los contratos correspondientes |
| Pieza de datos | Secundario | Aplica el cambio de situación o la baja, con su arrastre de trabajos |
| Alumno | Secundario | Padece el efecto: obtiene acceso, lo pierde o deja de existir en el laboratorio |

## 3. Precondiciones

- El administrador tiene sesión iniciada por CU-02 y su papel es el de administrador.
- Para el flujo principal existe la cuenta de administrador. Para FA-03, no existe ninguna.
- El producto admite **exactamente un** administrador y dos papeles fijos, sin permisos configurables.

## 4. Flujo principal

1. El administrador abre la ruta de cuentas de su panel.
2. **La pieza pública invoca desde su servidor el contrato de listado de cuentas** de `GeometriaFactory-Contracts` CU-02, pasos 3 y 4.
3. La pieza pública presenta la lista con correo, nombre, apellido, situación y fecha de registro de cada cuenta.
4. El administrador elige una cuenta y una de las tres operaciones de situación: habilitar, bloquear o rehabilitar.
5. La pieza pública invoca el contrato de cambio de situación de `GeometriaFactory-Contracts` CU-02, pasos 5 y 6.
6. La pieza de datos devuelve la situación resultante y la pieza pública actualiza la lista con esa situación, sin inventarla del lado del navegador.
7. El administrador continúa con la cuenta siguiente.

## 5. Flujos alternativos

| Id | Disparador | Curso | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El administrador habilita una cuenta `Pendiente` | Es el paso 4 con la operación de habilitar. A partir de ese momento el alumno puede ingresar y establecer su contraseña por CU-02 FA-02 y CU-03 | El flujo continúa en el paso 7 |
| FA-02 | El administrador da de baja una cuenta | La pieza pública **exige que el administrador escriba el correo de la cuenta** como confirmación, y declara en el mismo lugar que la baja elimina también todos los trabajos de esa cuenta. Con la confirmación completa invoca el contrato de baja de `GeometriaFactory-Contracts` CU-02 FA-01 | El flujo vuelve al paso 2, con la lista ya sin esa cuenta |
| FA-03 | Es el primer arranque del laboratorio y no existe cuenta de administrador | La pieza pública ofrece la ruta de configuración inicial, con correo y contraseña, e invoca el contrato de `GeometriaFactory-Contracts` CU-02 FA-03. Es el **único** momento en que esa ruta arma algo | El flujo continúa en CU-02, paso 1 |
| FA-04 | Alguien abre la ruta de configuración inicial cuando ya existe administrador | La pieza pública no arma el formulario y deriva a la ruta de ingreso | El flujo termina |
| FA-05 | El administrador bloquea una cuenta cuyo alumno tiene sesión iniciada | La situación cambia en la pieza de datos. La sesión ya establecida no se corta desde acá: la próxima solicitud que esa sesión emita recibe el motivo de la situación de cuenta y CU-02 FA-01 la devuelve a la ruta de ingreso | El flujo continúa en el paso 7 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del sistema |
| --- | --- | --- |
| `CONTRATO_CONFIRMACION_NO_COINCIDE` | El correo escrito como confirmación de la baja no coincide con el de la cuenta | La baja **no procede**. La pieza pública lo informa y deja reintentar con la confirmación correcta |
| `CONTRATO_ADMINISTRADOR_YA_CONFIGURADO` | Se intenta configurar una segunda cuenta de administrador | La pieza pública informa que ya existe y deriva a la ruta de ingreso. Terminación controlada: no hay camino alternativo |
| `CONTRATO_ALUMNO_NO_ENCONTRADO` | La cuenta sobre la que se opera ya no existe | La pieza pública informa y recarga la lista. Recuperación por reintento sobre la lista actualizada |
| `CONTRATO_CAMPO_REQUERIDO_AUSENTE` | Falta un campo de la configuración inicial o de la confirmación | La pieza pública señala el campo. Recuperación por corrección y reintento |
| `CONTRATO_SERVICIO_NO_DISPONIBLE` | La pieza de datos no responde | Handoff a CU-10: estado degradado explícito. **La lista no se muestra con datos viejos**, porque la pieza pública no guarda estado propio |

## 7. Postcondiciones

- En caso de éxito de cambio de situación: la cuenta queda en la situación que devolvió la pieza de datos, y la lista la refleja.
- En caso de éxito de baja: la cuenta y **todos sus trabajos** dejaron de existir, y la lista ya no la incluye.
- En caso de éxito de configuración inicial: existe la única cuenta de administrador del laboratorio y la ruta de configuración inicial deja de armar formulario para siempre.
- En caso de fallo: ninguna situación cambió y la lista sigue mostrando lo que la pieza de datos devolvió por última vez.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un laboratorio sin cuenta de administrador | Se abre la ruta de configuración inicial y se configura `docente@ejemplo.test` | La cuenta queda creada, y una segunda apertura de esa ruta ya no arma formulario y deriva al ingreso |
| CA-02 | Una cuenta de alumno en situación `Pendiente` | El administrador la habilita | La lista la muestra habilitada, y el alumno pasa a poder establecer su contraseña por CU-03 |
| CA-03 | El panel de cuentas | Se cuentan las operaciones disponibles sobre una cuenta de alumno | Son exactamente cuatro: habilitar, bloquear, rehabilitar y dar de baja |
| CA-04 | Una cuenta `alumno@ejemplo.test` con dos trabajos | El administrador pide la baja y escribe `alumno@otro.test` como confirmación | La baja no procede y el mensaje declara que la confirmación no coincide |
| CA-05 | La misma cuenta y la misma pantalla de confirmación | El administrador lee la confirmación antes de escribir | El texto declara explícitamente que la baja elimina también los trabajos de esa cuenta |
| CA-06 | La misma cuenta con dos trabajos | El administrador escribe `alumno@ejemplo.test` y confirma | La cuenta desaparece de la lista y sus dos trabajos ya no figuran en ningún listado del laboratorio |
| CA-07 | Un alumno con sesión iniciada | Se abre por dirección directa la ruta de cuentas | La pieza pública no arma la ruta y devuelve al panel del alumno |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | [`NB-01`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-01-Control-De-Admision-Al-Laboratorio.md), [`NB-02`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md) |
| Reglas de negocio aplicables | [`RN-01`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-01-Administrador-Unico-Y-Papeles-Fijos.md), [`RN-06`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-06-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md), [`RN-07`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-07-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md), [`RN-02`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02-Correo-Del-Alumno-Unico.md) |
| Contratos de uso consumidos | [`GeometriaFactory-Contracts` CU-02](../../../GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-02-Contrato-De-Administracion-De-Cuentas.md) pasos 3 a 6 y FA-01 y FA-03; [`CU-06`](../../../GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-06-Contrato-De-Respuesta-De-Error.md) |
| Fachada del visualizador | Ninguna función |
| Historias de usuario a generar en 06 | US-08, US-09, US-10 |
| Componentes esperados en 05 | Ruta de configuración inicial, panel de cuentas y diálogo de confirmación escrita de la baja |
| Tests previstos en 08 | Guion de demostración de la etapa `c` para FA-03 y FA-04, y de la etapa `d` para las cuatro operaciones y la confirmación escrita |

## 10. Notas y supuestos

- El arrastre de trabajos en la baja es una invariante del dominio y **no** algo que la pieza pública ejecute. Lo que sí le corresponde es hacer la operación difícil de ejecutar por accidente: por eso la confirmación escrita y el aviso explícito son criterios de aceptación acá.
- La baja es también el único remedio para un olvido de contraseña, por la ausencia de canal de correo. Es una consecuencia declarada y aceptada aguas arriba, no un defecto de este caso de uso.
- El producto no admite un segundo administrador ni permisos finos, por la exclusión X-3. Ninguna variante de este caso de uso los introduce.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. |

## 13. Interacción multiusuario y concurrencia

Sección opcional admitida por `Rules-Especificacion-Funcional.md` §4.3 para el tipo `web-monolith`.

El laboratorio tiene un solo administrador, de modo que no hay dos personas cambiando la situación de la misma cuenta a la vez. Lo que sí puede coincidir es un cambio de situación con una sesión de alumno ya establecida: FA-05 declara que la pieza pública no corta esa sesión y que el efecto se hace visible en la siguiente solicitud que esa sesión emita. La pieza pública no mantiene copia de la lista de cuentas entre operaciones: cada recorrido vuelve a pedirla.
