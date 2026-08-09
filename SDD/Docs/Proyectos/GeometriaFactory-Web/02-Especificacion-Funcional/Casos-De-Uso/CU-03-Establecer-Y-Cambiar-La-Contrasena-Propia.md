# CU-03 — Establecer y cambiar la contraseña propia

**Proyecto de código:** GeometriaFactory-Web
**Documento:** CU-03-Establecer-Y-Cambiar-La-Contrasena-Propia.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Analista Funcional senior (AG-02)
**Trazabilidad upstream:** `../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md` §1, §5 (segundo y cuarto criterio); `../../../../00-Contexto/Alcance-Producto.md` §4.1; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.7**, §4 (F-04, F-05, **F-26**), §4.1 (**RN-13**), §6 (flujo 1), §7 (**CL-7 reescrito**), §9 (X-1, **X-2 retirada**), §11 (RN-B6), §17.1.P.2 (**INV-09**), §17.6 P.5
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

---

## 1. Propósito

Permitir que la persona fije su contraseña en su primer ingreso efectivo, ya habilitada y sin que ninguna contraseña se haya transportado nunca, y que después la cambie cuando quiera presentando la vigente. Es la única forma que tiene de administrar su credencial dentro del laboratorio.

Desde el `PRODUCT-INTAKE` **1.7** sostiene además un tercer curso, el **cambio forzado**: el de la persona a la que el administrador le reseteó la contraseña (F-26). Es el mismo formulario del cambio, con una diferencia que lo gobierna todo: **hasta que no lo complete, no llega a ninguna otra parte del sistema** (RN-13, INV-09).

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Persona con cuenta habilitada | Primario | Elige su contraseña por primera vez, la reemplaza presentando la vigente, o la reemplaza obligada tras un reseteo, presentando la provisoria |
| Pieza pública | Sistema | Presenta el formulario, arma la solicitud contra la pieza de datos y no conserva ninguna contraseña |
| Pieza de datos | Secundario | Registra la credencial derivada y devuelve el resultado |

## 3. Precondiciones

- Para el establecimiento: la cuenta está habilitada y todavía no tiene contraseña, situación que CU-02 detecta en su FA-02.
- Para el cambio: la persona tiene sesión iniciada por CU-02 y conoce su contraseña vigente.
- Para el **cambio forzado**: la persona tiene sesión iniciada y su cuenta está marcada como con cambio de contraseña pendiente, situación que CU-02 detecta en su FA-07. Lo que presenta como vigente es la **provisoria** que el administrador le comunicó.
- La pieza pública no guarda estado propio: nada de lo que se escribe en este formulario sobrevive a la operación.

## 4. Flujo principal

1. La persona llega a la ruta de establecimiento de contraseña, derivada por CU-02 FA-02.
2. La pieza pública presenta el formulario de contraseña nueva y su repetición.
3. La persona completa los dos campos y confirma.
4. La pieza pública verifica que los dos valores coincidan antes de salir hacia la pieza de datos.
5. **La pieza pública invoca desde su servidor el contrato de establecimiento de contraseña** de `GeometriaFactory-Contracts` CU-02, pasos 7 y 8.
6. La pieza de datos responde con el resultado y la contraseña queda establecida.
7. La pieza pública informa el resultado y devuelve a la ruta de ingreso de CU-02, que a partir de ahora es el camino de entrada.

## 5. Flujos alternativos

| Id | Disparador | Curso | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | La persona, ya dentro del laboratorio, quiere cambiar su contraseña | La pieza pública presenta el formulario con tres campos —contraseña vigente, contraseña nueva y su repetición— e invoca el contrato de cambio de contraseña de `GeometriaFactory-Contracts` CU-02 FA-02. **La contraseña vigente es obligatoria por contrato** | El flujo vuelve al panel de la persona, con la sesión vigente |
| FA-02 | Las dos escrituras de la contraseña nueva no coinciden | La pieza pública lo señala sin salir hacia la pieza de datos | El flujo vuelve al paso 3 |
| FA-03 | La persona abandona la ruta de establecimiento sin completarla | No queda nada guardado: la cuenta sigue habilitada y sin contraseña, y el próximo intento de ingreso vuelve a derivar acá por CU-02 FA-02 | El flujo vuelve al paso 1 de CU-02 |
| FA-04 | La persona llega al **cambio forzado**, derivada por CU-02 FA-07 | La pieza pública presenta el mismo formulario de tres campos de FA-01 —contraseña actual, nueva y su repetición—, declarando **por qué** está ahí: le resetearon la contraseña y tiene que elegir una. **No hay «cancelar»**: no hay ningún estado previo al que volver, porque ninguna otra ruta está disponible. Con el cambio aplicado, la marca se levanta y la persona **continúa a su panel con la sesión vigente**, sin volver a ingresar | El flujo termina en el panel de la persona |
| FA-05 | La persona con cambio de contraseña pendiente pide **cualquier otra ruta** | La pieza pública la devuelve al cambio forzado, **sin revelar qué contenía la ruta pedida** y sin presentarlo como error: es la situación esperada. La verificación que la hace cumplir no es ésta —acotar lo que se ofrece no hace cumplir nada—, sino la de la pieza de datos en cada solicitud (§10) | El flujo vuelve a FA-04 |
| FA-06 | La persona con cambio de contraseña pendiente cierra sesión sin haberla cambiado | La sesión se cierra normalmente por CU-02. **La marca sigue puesta**: el próximo ingreso con la provisoria vuelve a derivar al cambio forzado. La provisoria no vence por cerrar sesión | El flujo vuelve al paso 1 de CU-02 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del sistema |
| --- | --- | --- |
| `CONTRATO_CREDENCIAL_INVALIDA` | El cambio llegó sin la contraseña vigente, o con una que no corresponde | La pieza pública lo informa sobre el campo de contraseña vigente y **no aplica el cambio**. Terminación controlada |
| `CONTRATO_CAMPO_REQUERIDO_AUSENTE` | Falta alguno de los campos del formulario | La pieza pública señala el campo que el contrato nombra. Recuperación por corrección y reintento |
| `CONTRATO_CUENTA_NO_HABILITADA` | La cuenta fue bloqueada entre la derivación y el envío del formulario | La pieza pública muestra el motivo y devuelve a la ruta de ingreso, sin establecer contraseña |
| `CONTRATO_CAMBIO_DE_CONTRASENA_PENDIENTE` [PENDIENTE, CU-04 §10] | La persona con la marca puesta pidió cualquier otra cosa que no fuera su propio cambio | La pieza pública la lleva al cambio forzado con el motivo declarado. **No es un error de la persona** y no se presenta como tal |
| `CONTRATO_SERVICIO_NO_DISPONIBLE` | La pieza de datos no responde | Handoff a CU-10: estado degradado explícito, sin dirección de servicio interno, y con la posibilidad de reintentar |

## 7. Postcondiciones

- En caso de éxito de establecimiento: la cuenta tiene contraseña y la persona puede ingresar por CU-02.
- En caso de éxito de cambio: la contraseña anterior deja de servir y la sesión vigente se conserva.
- En caso de éxito de **cambio forzado**: además de lo anterior, **la marca queda levantada**, la provisoria deja de servir y todas las rutas del papel de la persona vuelven a estar disponibles. **El administrador no conoce la contraseña nueva**: la eligió la persona y nunca pasó por su panel.
- En caso de fallo del cambio forzado: la marca sigue puesta y la persona sigue confinada al cambio. **Sus trabajos siguen todos ahí**: el reseteo no eliminó ninguno (RN-12).
- En caso de fallo: la credencial no cambia y la persona conserva la que tenía.
- En ningún caso: la pieza pública conserva ninguna contraseña, ni la escribe en el navegador, ni la incluye en ningún mensaje.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Una cuenta habilitada sin contraseña, derivada por CU-02 FA-02 | La persona escribe `clave-nueva-01` dos veces y confirma | La contraseña queda establecida y el siguiente ingreso con `clave-nueva-01` entrega el panel |
| CA-02 | Una persona con sesión iniciada | Pide cambiar su contraseña sin escribir la vigente | El cambio no se aplica y el mensaje señala el campo de contraseña vigente |
| CA-03 | Una persona con sesión iniciada y contraseña vigente `clave-nueva-01` | Cambia a `clave-nueva-02` presentando la vigente | El cambio se aplica, el ingreso con `clave-nueva-01` deja de funcionar y el ingreso con `clave-nueva-02` funciona |
| CA-04 | El formulario de establecimiento | La persona escribe dos valores distintos en contraseña y repetición | La pieza pública lo señala y **no** emite ninguna solicitud hacia la pieza de datos |
| CA-05 | Un recorrido completo de establecimiento y de cambio | Se inspecciona el navegador con las herramientas de desarrollo | Ninguna contraseña ni credencial de sesión queda observable en el navegador |
| CA-06 | Una cuenta a la que el administrador le reseteó la contraseña, con tres trabajos | La persona ingresa con la provisoria | Llega al cambio forzado, la superficie **declara por qué está ahí** y **no ofrece «cancelar»** |
| CA-07 | La misma persona en el cambio forzado | Pide por dirección directa el listado de sus trabajos, o cualquier otra ruta de su papel | Vuelve al cambio forzado, sin haber leído ni escrito nada, y sin que el mensaje revele qué contenía la ruta pedida |
| CA-08 | La misma persona | Completa el cambio presentando la provisoria como vigente | Continúa a su panel **con la sesión vigente**, sus tres trabajos siguen ahí con sus estados, la provisoria deja de funcionar y el administrador **no conoce** la contraseña nueva |
| CA-09 | La misma persona, antes de cambiarla | Cierra sesión y vuelve a ingresar con la provisoria | Vuelve a terminar en el cambio forzado: la marca no se levanta por cerrar sesión |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | [`NB-02`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md) |
| Reglas de negocio aplicables | [`RN-06`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-06-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md), y **`RN-13`** del `PRODUCT-INTAKE` 1.7 §4.1, todavía sin archivo en `GeometriaFactory-Domain`. La derivación de la credencial y su verificación viven en `GeometriaFactory-Infrastructure`; la admisibilidad de la cuenta y el invariante INV-09, en `GeometriaFactory-Domain` y en `GeometriaFactory-Application` |
| Contratos de uso consumidos | [`GeometriaFactory-Contracts` CU-02](../../../GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-02-Contrato-De-Administracion-De-Cuentas.md) pasos 7 y 8, y FA-02; [`CU-06`](../../../GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-06-Contrato-De-Respuesta-De-Error.md) |
| Fachada del visualizador | Ninguna función |
| Historias de usuario a generar en 06 | US-06, US-07, US-28, US-29 |
| Componentes esperados en 05 | Página de establecimiento de contraseña, página de cambio de contraseña dentro del panel, y el **guard de cambio pendiente** que confina a la cuenta reseteada |
| Tests previstos en 08 | Guion de demostración de la etapa `c` para el cambio con contraseña vigente, y de la etapa `d` para el establecimiento en el primer ingreso efectivo |

## 10. Notas y supuestos

- Este caso de uso existe porque **no hay canal de correo**: la contraseña no se transporta nunca y la elige la persona en su primer ingreso efectivo. Es la traducción directa del flujo 1 del intake.
- **Un olvido de contraseña ya no se resuelve por baja y alta nueva.** Hasta el `PRODUCT-INTAKE` 1.6 ésa era la consecuencia aceptada y **arrastraba todos los trabajos de la cuenta**; 1.7 retiró la exclusión X-2, reescribió CL-7 e incorporó el **reseteo** de CU-04 FA-06, que conserva la cuenta y sus trabajos. Lo que sigue sin existir es la **recuperación autónoma**: no hay canal de correo (X-1), y el remedio pasa siempre por el administrador.
- **Acotar rutas no hace cumplir el confinamiento.** FA-05 describe lo que esta pieza ofrece; quien impide efectivamente que una cuenta marcada lea o escriba es la pieza de datos, que verifica la marca en cada solicitud (invariante INV-09, ejercido en `GeometriaFactory-Application`). Es el mismo criterio con el que RT-09 trata la protección de rutas por papel, y por eso CA-07 fuerza la solicitud **sin pasar por la pantalla**.
- **La provisoria no vence por tiempo ni por cierre de sesión.** Lo único que la termina es el cambio efectivo. El producto no declara vencimiento y esta categoría **no lo inventa**: si el Product Owner lo quisiera, sería una regla nueva aguas arriba.
- Las exigencias de forma de la contraseña —longitud, composición— no las fija esta categoría. Si el producto las adopta, se declaran en 05-Arquitectura-Tecnica y se hacen cumplir del lado de la pieza de datos.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. |
| 1.1 | 2026-08-09 | **Propagación del `PRODUCT-INTAKE` 1.7**, capacidad **F-26** con su regla **RN-13**, el invariante **INV-09**, el caso límite **CL-7 reescrito** y la exclusión **X-2 retirada**. **§1**: la superficie suma un tercer curso, el **cambio forzado** de quien fue reseteado. **§3**: precondición del cambio forzado, con la provisoria como credencial vigente. **§5**: **FA-04**, **FA-05** y **FA-06** nuevas —llegada al cambio forzado sin salida, cualquier otra ruta devuelta sin revelar su contenido, y el cierre de sesión que no levanta la marca—. **§6**: `CONTRATO_CAMBIO_DE_CONTRASENA_PENDIENTE`, rotulado **pendiente** porque el contrato todavía no existe en `GeometriaFactory-Contracts` (CU-04 §10). **§7**: dos postcondiciones nuevas, incluida la que declara que **el administrador no conoce la contraseña nueva**. **§8**: CA-06 a CA-09 nuevas, una de ellas forzando la solicitud sin pasar por la pantalla. **§10**: la nota que declaraba a la baja como único remedio del olvido **se reescribe**, porque 1.7 la volvió falsa; se suman la precisión de que acotar rutas no hace cumplir el confinamiento, y la de que la provisoria no vence por tiempo, que esta categoría **no inventa**. Sube minor: agrega un curso, tres flujos alternativos y cuatro criterios de aceptación, sin invalidar ninguna decisión previa. |
