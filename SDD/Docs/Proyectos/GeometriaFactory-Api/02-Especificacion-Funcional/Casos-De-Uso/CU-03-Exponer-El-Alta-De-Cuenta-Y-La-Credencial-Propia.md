# CU-03 — Exponer el alta de cuenta y la credencial propia

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** CU-03-Exponer-El-Alta-De-Cuenta-Y-La-Credencial-Propia.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-01`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-01-Control-De-Admision-Al-Laboratorio.md), [`NB-02`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.12** §4 (F-01, F-02, F-04, F-05), §4.1 (RN-01, RN-02, RN-06, RN-13), §6 (flujo 1), §9 (X-1), §14 (RA-01, RA-03), §17.5.P.5; `Proyectos/GeometriaFactory-Contracts/.../CU-02-Contrato-De-Administracion-De-Cuentas.md`; `Proyectos/GeometriaFactory-Application/.../CU-01-Registrar-El-Alta-De-Una-Cuenta.md`, `.../CU-10-Configurar-La-Cuenta-De-Administrador.md` y `.../CU-03-Resolver-El-Ingreso-Y-La-Credencial-Del-Alumno.md`
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico` y `08-Calidad-Y-Pruebas` de GeometriaFactory-Api

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

Exponer los **cuatro** puntos de acceso que tienen en común un rasgo que ninguno de los demás tiene, y que es el motivo por el que están en un solo contrato: **se ejercen sin acceso firmado, o sin que el papel importe**.

| Punto | Intención | Quién lo ejerce |
| --- | --- | --- |
| **A-02** | Registrar una cuenta de alumno, **sin campo de contraseña** | Una persona que todavía no tiene cuenta |
| **A-03** | Configurar la cuenta de administrador, **sólo mientras no exista ninguna** | El docente, en el primer arranque del laboratorio |
| **A-04** | Establecer la contraseña propia en el primer ingreso efectivo | Una persona habilitada que todavía no tiene credencial |
| **A-05** | Cambiar la contraseña propia **exigiendo la vigente** | Cualquiera de los dos papeles, ya dentro del laboratorio |

Los cuatro son el circuito de identidad que el producto sostiene **sin canal de correo**: el intake §9 X-1 declara que el flujo está diseñado para evitar el envío de correo y que **la contraseña no se transporta nunca** desde el sistema hacia la persona. La única excepción a eso es la provisoria del reseteo, que es otro punto y otro contrato (CU-05).

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| `GeometriaFactory-Web` | Primario | Arma las cuatro solicitudes desde sus formularios y las envía servidor a servidor |
| Alumno y administrador | Sujetos de la regla | Nunca invocan estos puntos directamente (RA-01) |

## 3. Precondiciones

- El servicio arrancó y dejó el almacén en condiciones (CU-11).
- Para **A-05**, la petición trae un acceso firmado válido y atravesó la guardia de CU-02, con su excepción declarada.
- Para **A-02** y **A-03**, la petición **no** trae acceso y **no** lo necesita.
- Para **A-04**, la forma de identificación **está abierta**: ver §10.

## 4. Flujo principal

1. Llega una petición a **A-02** con correo, nombre y apellido, **sin campo de contraseña**.
2. Se ejerce el alta contra la capa de aplicación, que constituye la cuenta en situación `Pendiente` y sin credencial.
3. Se responde `201` con el resultado del registro, que declara la situación inicial de la cuenta.
4. Llega una petición a **A-05** con la contraseña vigente y la nueva.
5. Se ejerce el reemplazo de la credencial contra la capa de aplicación, que exige la verificación de la vigente.
6. Se responde `200`. **La contraseña nueva no vuelve en la respuesta y no queda registrada.**

**El registro no elige contraseña, y eso no es un detalle de formulario.** Es lo que hace posible el flujo sin correo: la cuenta nace sin credencial, el administrador la habilita, y recién en el primer ingreso efectivo la persona establece la suya.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | Es el primer arranque del laboratorio y no existe cuenta de administrador | Llega una petición a **A-03** con correo y contraseña. La capa de aplicación constituye la cuenta con papel `Administrador`, **`Habilitado` y con credencial**, que es el estado inicial opuesto al del registro. Se responde `201` | Termina |
| FA-02 | Ya existe una cuenta de administrador y llega otra petición a **A-03** | Se responde `409`. **El contrato no ofrece camino alternativo y la respuesta no sugiere ninguno** | Termina |
| FA-03 | Una persona habilitada y sin credencial establece su contraseña por **A-04** | Se ejerce la fijación contra la capa de aplicación y se responde `200`. A partir de ahí el camino de entrada vuelve a ser **A-01** | Termina |
| FA-04 | Una cuenta con la marca de cambio pendiente cambia su contraseña por **A-05**, presentando la provisoria como vigente | **Es el cambio forzado.** El reemplazo procede y **levanta la marca** en la misma unidad de trabajo. La contraseña nueva la elige la persona y **el administrador no la conoce** | Termina |
| FA-05 | La cuenta que cambia su contraseña por **A-05** tiene papel `Administrador` | El flujo es idéntico: este punto **no distingue papeles**, y es el único camino por el que el administrador cambia su propia contraseña | Paso 6 |

## 6. Excepciones y errores

| Código del contrato | Respuesta | Punto | Causa |
| --- | --- | --- | --- |
| `CONTRATO_CAMPO_REQUERIDO_AUSENTE` | `400` | A-02, A-03, A-04, A-05 | Falta el correo, el nombre o el apellido en el registro; falta una de las dos contraseñas en el cambio. La respuesta **nombra el campo ausente** |
| `CONTRATO_CORREO_YA_REGISTRADO` | `409` | A-02 | El correo ya pertenece a una cuenta. **La respuesta no declara la situación ni el papel de esa cuenta** |
| `CONTRATO_ADMINISTRADOR_YA_CONFIGURADO` | `409` | A-03 | Ya existe una cuenta con papel `Administrador` |
| `CONTRATO_CREDENCIAL_INVALIDA` | `401` | A-05 | La contraseña vigente presentada no corresponde. Texto neutro, **y la marca de cambio pendiente, si estaba, sigue puesta** |
| `CONTRATO_CONTRASENA_NO_ESTABLECIDA` | `403` | A-04 | No aplica al camino feliz; corresponde cuando la cuenta pedida no está en condiciones de establecer credencial |
| `CONTRATO_ERROR_NO_CLASIFICADO` | `503` | Los cuatro | El almacén no está disponible. **La respuesta no incluye su ruta** |

**Ninguna de las seis devuelve una contraseña, en claro ni derivada, y ninguna la registra.** El único valor de credencial que esta superficie devuelve alguna vez es la provisoria del reseteo, en CU-05, y ése tampoco se registra.

## 7. Postcondiciones

- **A-02 con éxito:** existe una cuenta en situación `Pendiente`, sin credencial, que **todavía no obtiene acceso** (RN-06).
- **A-03 con éxito:** existe **exactamente una** cuenta con papel `Administrador`, habilitada y con credencial, y ninguna petición posterior a ese punto puede crear otra.
- **A-04 y A-05 con éxito:** la cuenta tiene una credencial derivada nueva; en el caso del cambio forzado, **la marca quedó levantada**.
- **Fallo:** la pieza pública recibe su código de respuesta y **el almacén queda como estaba**.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | La solicitud de registro del ensamblado de contratos | Se inspecciona su superficie | Declara **correo, nombre y apellido**, y **0 campos** de contraseña |
| CA-02 | Un correo ya registrado | Se registra de nuevo por A-02 | Responde `409`, y el cuerpo **no declara la situación ni el papel** de la cuenta que lo ocupa |
| CA-03 | Una instancia con administrador ya configurado | Se invoca A-03 | Responde `409` y **sigue existiendo exactamente 1** cuenta con papel `Administrador` |
| CA-04 | Una instancia sin ninguna cuenta de administrador | Se invoca A-03 | Responde `201`, y la cuenta queda **`Habilitado` y con credencial**, que es el estado inicial opuesto al de A-02 |
| CA-05 | Una cuenta con la marca de cambio pendiente | Se invoca A-05 con la provisoria como vigente | Responde `200`, la marca queda levantada y una petición posterior a cualquier otro punto **ya no recibe** el `403` de la guardia |
| CA-06 | La misma cuenta marcada | Se invoca A-05 con una vigente equivocada | Responde `401` y **la marca sigue puesta** |
| CA-07 | Cualquiera de los cuatro puntos, con la respuesta y el registro del servidor observados | Se ejerce con éxito y con fallo | **0 apariciones** de cualquier contraseña recibida o elegida, del valor derivado de una credencial, de la clave de firma y de la ruta del almacén |
| CA-08 | El punto A-05 | Se invoca con un acceso de papel `Administrador` y con uno de papel `Alumno` | Las **2** peticiones se admiten: este punto **no distingue papeles** |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-01 por el alta y por la configuración inicial; NB-02 por la credencial propia sin canal de correo |
| Reglas de negocio aplicables | [RN-01](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-01-Administrador-Unico-Y-Papeles-Fijos.md), en el punto de configuración y su negativa. [RN-02](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02-Correo-Del-Alumno-Unico.md), en la traducción del correo ocupado. [RN-06](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-06-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md), por la situación inicial que el alta fija. [RN-13](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-13-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md), porque **A-05 es la única excepción de la guardia** |
| Regla de arquitectura del producto | **RA-03** en las seis condiciones de §6; **RA-01** en los cuatro puntos, que la pieza pública ejerce servidor a servidor aunque tres de ellos no exijan acceso |
| Puntos de acceso | A-02, A-03, A-04, A-05 |
| Contrato de uso que transporta | `GeometriaFactory-Contracts` `CU-02`, incluida su reutilización por el cambio obligatorio |
| Historias de usuario a generar en 06 | US-07, US-08, US-09, US-10 |
| Componentes esperados en 05 | Cuatro puntos de acceso; la resolución del punto abierto de identificación de A-04 |
| Tests previstos en 08 | Integración por los ocho criterios; y una inspección de que ninguna traza contiene contraseñas |

## 10. Notas y supuestos

- **La identidad en A-04 es un punto abierto, y es el más importante de esta categoría.** El ensamblado de contratos declara la solicitud de establecimiento con «la contraseña elegida» y **no declara cómo viaja la identidad de la cuenta**. Es la única escritura de la superficie que ocurre **antes** de que la persona pueda obtener un acceso firmado, de modo que no puede identificarse con uno. Las dos salidas visibles, y **esta categoría no elige entre ellas**:
  1. **Punto anónimo que transporta también la identidad**, con alguna prueba de posesión que ninguna fuente declara. Sin esa prueba, cualquiera podría fijarle la contraseña a cualquier cuenta habilitada que todavía no la tenga, y el circuito sin correo se convertiría en un circuito sin credencial.
  2. **Acceso de alcance acotado emitido para ese único paso**, que traslada el problema al momento de emitirlo pero deja el punto bajo la misma guardia que los demás.
  Es una decisión de seguridad y no de forma: está elevada al **Product Owner** y registrada en el índice maestro §11 y en `Definicion-Superficie-HTTP.md` §9.
- **A-03 no exige acceso, y es correcto.** En el primer arranque no hay ninguna cuenta con la que obtenerlo. Lo que lo acota no es un papel sino la existencia: **sólo procede mientras no exista ninguna cuenta de administrador**, y la ventana la gobierna la capa de aplicación.
- **El producto no envía correo, y este contrato es donde eso se nota.** El intake §9 X-1 declara la exclusión: la contraseña la elige la persona en su primer ingreso efectivo y **no se transporta nunca** desde el sistema. Incorporar el envío de correo cambiaría el flujo de alta entero.
- **El cambio de la propia contraseña exige la vigente, por contrato.** No hay ningún camino en esta superficie por el que alguien cambie su contraseña sin conocer la anterior; el único que se le parece es el reseteo, que **lo ejerce el administrador, produce una provisoria y no permite elegirla** (CU-05).

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. |
