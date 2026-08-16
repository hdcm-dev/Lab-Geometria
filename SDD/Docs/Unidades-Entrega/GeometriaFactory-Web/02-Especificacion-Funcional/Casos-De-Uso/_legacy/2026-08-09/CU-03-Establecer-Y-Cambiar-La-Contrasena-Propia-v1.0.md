# CU-03 — Establecer y cambiar la contraseña propia

**Proyecto de código:** GeometriaFactory-Web
**Documento:** CU-03-Establecer-Y-Cambiar-La-Contrasena-Propia.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Analista Funcional senior (AG-02)
**Trazabilidad upstream:** `../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md` §1, §5 (segundo y cuarto criterio); `../../../../00-Contexto/Alcance-Producto.md` §4.1; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-04, F-05), §6 (flujo 1), §9 (X-1, X-2), §11 (RN-B6), §17.6 P.5
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

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Persona con cuenta habilitada | Primario | Elige su contraseña por primera vez, o la reemplaza presentando la vigente |
| Pieza pública | Sistema | Presenta el formulario, arma la solicitud contra la pieza de datos y no conserva ninguna contraseña |
| Pieza de datos | Secundario | Registra la credencial derivada y devuelve el resultado |

## 3. Precondiciones

- Para el establecimiento: la cuenta está habilitada y todavía no tiene contraseña, situación que CU-02 detecta en su FA-02.
- Para el cambio: la persona tiene sesión iniciada por CU-02 y conoce su contraseña vigente.
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

## 6. Excepciones y errores

| Código | Causa | Respuesta del sistema |
| --- | --- | --- |
| `CONTRATO_CREDENCIAL_INVALIDA` | El cambio llegó sin la contraseña vigente, o con una que no corresponde | La pieza pública lo informa sobre el campo de contraseña vigente y **no aplica el cambio**. Terminación controlada |
| `CONTRATO_CAMPO_REQUERIDO_AUSENTE` | Falta alguno de los campos del formulario | La pieza pública señala el campo que el contrato nombra. Recuperación por corrección y reintento |
| `CONTRATO_CUENTA_NO_HABILITADA` | La cuenta fue bloqueada entre la derivación y el envío del formulario | La pieza pública muestra el motivo y devuelve a la ruta de ingreso, sin establecer contraseña |
| `CONTRATO_SERVICIO_NO_DISPONIBLE` | La pieza de datos no responde | Handoff a CU-10: estado degradado explícito, sin dirección de servicio interno, y con la posibilidad de reintentar |

## 7. Postcondiciones

- En caso de éxito de establecimiento: la cuenta tiene contraseña y la persona puede ingresar por CU-02.
- En caso de éxito de cambio: la contraseña anterior deja de servir y la sesión vigente se conserva.
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

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | [`NB-02`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md) |
| Reglas de negocio aplicables | [`RN-06`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-06-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md). La derivación de la credencial y su verificación viven en `GeometriaFactory-Infrastructure`; la admisibilidad de la cuenta, en `GeometriaFactory-Domain` |
| Contratos de uso consumidos | [`GeometriaFactory-Contracts` CU-02](../../../GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-02-Contrato-De-Administracion-De-Cuentas.md) pasos 7 y 8, y FA-02; [`CU-06`](../../../GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-06-Contrato-De-Respuesta-De-Error.md) |
| Fachada del visualizador | Ninguna función |
| Historias de usuario a generar en 06 | US-06, US-07 |
| Componentes esperados en 05 | Página de establecimiento de contraseña y página de cambio de contraseña dentro del panel |
| Tests previstos en 08 | Guion de demostración de la etapa `c` para el cambio con contraseña vigente, y de la etapa `d` para el establecimiento en el primer ingreso efectivo |

## 10. Notas y supuestos

- Este caso de uso existe porque **no hay canal de correo**: la contraseña no se transporta nunca y la elige la persona en su primer ingreso efectivo. Es la traducción directa del flujo 1 del intake.
- La consecuencia aceptada es que un olvido de contraseña sólo se resuelve por baja y alta nueva, operación del administrador que **arrastra los trabajos de la cuenta** (CU-04, FA-02). Conviene que el administrador lo advierta antes de ejecutarla.
- Las exigencias de forma de la contraseña —longitud, composición— no las fija esta categoría. Si el producto las adopta, se declaran en 05-Arquitectura-Tecnica y se hacen cumplir del lado de la pieza de datos.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. |
