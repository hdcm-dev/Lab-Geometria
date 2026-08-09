> **Artefacto archivado — estado `Superado`**
>
> Esta es una **copia archivada** del documento `CU-01-Contrato-De-Canje-De-Credenciales-Y-Sesion.md` en su versión **1.0**, tomada el 2026-08-09 por el orquestador SDD antes de que la versión vigente la superara (`Master-Prompt.md` §5 y §5.1).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-09
> - **Versión vigente:** [`CU-01-Contrato-De-Canje-De-Credenciales-Y-Sesion.md`](../../CU-01-Contrato-De-Canje-De-Credenciales-Y-Sesion.md)
>
> El cuerpo que sigue **no se modifica**: un registro que se corrige después deja de ser un registro. Este archivo no se renombra, no se reenlaza y no vuelve a tocarse.

---

# CU-01 — Contrato de canje de credenciales y de sesión

**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** CU-01-Contrato-De-Canje-De-Credenciales-Y-Sesion.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-08
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `01-Necesidades-Negocio/Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md` §1, §5; `NB-01-Control-De-Admision-Al-Laboratorio.md` §5; `00-Contexto/Vision-Producto.md` §3 y §9; `00-Contexto/Alcance-Producto.md` §4.1 (F-04, F-05) y §8; `PRODUCT-INTAKE` §17.4 P.2, P.3, P.5 y P.11, §17.5 P.3 y P.5, §14 (RA-03), §4 (F-02, F-04, F-05), §6 (flujo 1)
**Trazabilidad downstream:** `05-Arquitectura-Tecnica` y `06-Backlog-Tecnico` de este proyecto de código; `08-Calidad-Y-Pruebas`

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
- [17. Compatibilidad de versión pública](#17-compatibilidad-de-versión-pública)

---

## 1. Propósito

Declarar los tipos de transferencia con los que la pieza pública presenta las credenciales de una persona a la pieza de datos y recibe de vuelta la credencial de sesión y los datos mínimos de identidad que necesita para armar sus pantallas. El caso de uso fija, sobre todo, **qué no viaja**: ninguna forma de la contraseña almacenada, ninguna clave de firma y ninguna dirección de servicio interno.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Código de la pieza pública compilado contra el contrato | Primario | Construye la solicitud de canje con lo que la persona escribió y consume la respuesta |
| Código de la pieza de datos compilado contra el contrato | Sistema | Produce la respuesta sobre los mismos tipos, sin agregar campos fuera del contrato |
| Ensamblado de contratos | Sistema | Declara los tipos y su superficie pública; su compilación es la que valida el acuerdo |

No hay actor humano. La persona que escribe las credenciales pertenece a los casos de uso de `GeometriaFactory-Web`.

## 3. Precondiciones

- Los dos extremos están compilados contra la misma versión del ensamblado de contratos.
- El ensamblado no declara ninguna referencia hacia `GeometriaFactory-Domain` (quality gate bloqueante de `PRODUCT-INTAKE` §17.4 P.8).
- El contrato ya declara los dos papeles del producto, alumno y administrador, como valores admitidos del campo de papel.

## 4. Flujo principal

1. El código de la pieza pública instancia el tipo de solicitud de canje con dos campos: correo y contraseña presentada.
2. El código de la pieza pública envía la solicitud a la pieza de datos sobre la frontera de servicio.
3. El código de la pieza de datos recibe la solicitud como el mismo tipo del contrato, sin traducción intermedia.
4. El código de la pieza de datos produce el tipo de respuesta de sesión con cuatro campos: credencial de sesión, identificador de la persona, correo y papel.
5. El código de la pieza pública lee la respuesta y guarda la credencial de sesión en el estado de servidor de la propia pieza pública.
6. El código de la pieza pública usa el campo de papel para decidir qué panel arma, sin volver a preguntar.

## 5. Flujos alternativos

| Id | Disparador | Curso | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | La cuenta todavía no fue habilitada por el administrador | La pieza de datos produce el tipo de respuesta de error de CU-06 con el código `CONTRATO_CUENTA_NO_HABILITADA` y el motivo declarado en texto neutro | El código de la pieza pública vuelve al paso 1 tras informar a la persona el motivo recibido |
| FA-02 | La persona ya habilitada todavía no estableció su contraseña, porque el registro no la elige | El canje **no produce respuesta de sesión**: la pieza de datos produce el tipo de respuesta de error de CU-06 con el código `CONTRATO_CONTRASENA_NO_ESTABLECIDA` y su motivo. El tipo de respuesta de sesión no declara ningún indicador de contraseña pendiente; ver §10 | El código de la pieza pública deriva al contrato de establecimiento de contraseña de CU-02 y luego reanuda en el paso 1 |
| FA-03 | La persona canjea credenciales de la cuenta de administrador | El flujo es idéntico; sólo cambia el valor del campo de papel | El flujo continúa en el paso 6 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del contrato |
| --- | --- | --- |
| `CONTRATO_CREDENCIAL_INVALIDA` | El correo o la contraseña presentada no se corresponden con ninguna cuenta | Respuesta de error de CU-06 con texto neutro que **no** declara cuál de los dos campos falló. Terminación controlada: no hay reintento automático |
| `CONTRATO_CUENTA_NO_HABILITADA` | La cuenta está pendiente de habilitación o bloqueada | Respuesta de error de CU-06 con el motivo, para que la persona sepa en qué situación está su cuenta. Handoff al flujo de admisión de CU-02 |
| `CONTRATO_CONTRASENA_NO_ESTABLECIDA` | La cuenta está habilitada pero la persona todavía no estableció su contraseña en su primer ingreso efectivo | Respuesta de error de CU-06 con el motivo. Handoff al contrato de establecimiento de contraseña de CU-02, que es el camino que el flujo de alta del intake declara |
| `CONTRATO_CAMPO_REQUERIDO_AUSENTE` | La solicitud llega sin correo o sin contraseña presentada | Respuesta de error de CU-06 que nombra el campo ausente. Recuperación: el código de la pieza pública corrige la solicitud y reintenta |
| `CONTRATO_SERVICIO_NO_DISPONIBLE` | La pieza de datos no responde | Respuesta de error de CU-06 con texto neutro y **sin** dirección del servicio que falló. Handoff al estado degradado de la pieza pública |

## 7. Postcondiciones

- En caso de éxito: el código de la pieza pública tiene una credencial de sesión y un papel, y ningún campo de la respuesta contiene la contraseña almacenada, la clave de firma ni una dirección de servicio interno.
- En caso de fallo: el código de la pieza pública tiene un tipo de error de CU-06 y ninguna credencial de sesión; el contrato no deja estado parcial, porque los tipos de transferencia no tienen comportamiento.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Una cuenta habilitada con correo `alumna@ejemplo.edu` y contraseña ya establecida | El código de la pieza pública canjea las credenciales | La respuesta de sesión trae exactamente cuatro campos poblados —credencial de sesión, identificador, correo `alumna@ejemplo.edu` y papel `Alumno`— y ninguno más |
| CA-02 | La misma cuenta habilitada | Se inspecciona la superficie pública del tipo de respuesta de sesión | El tipo **no declara** ningún campo de contraseña almacenada, de clave de firma ni de dirección de servicio interno: 0 campos de esas tres clases |
| CA-03 | Una cuenta existente con la contraseña presentada equivocada | El código de la pieza pública canjea las credenciales | La respuesta es el tipo de error de CU-06 con código `CONTRATO_CREDENCIAL_INVALIDA` y un texto que no nombra ni el campo de correo ni el de contraseña |
| CA-04 | Una cuenta registrada y todavía no habilitada | El código de la pieza pública canjea las credenciales | La respuesta es el tipo de error de CU-06 con código `CONTRATO_CUENTA_NO_HABILITADA` y un motivo legible, no un texto genérico |
| CA-05 | Una cuenta habilitada que todavía no estableció contraseña | El código de la pieza pública canjea las credenciales | La respuesta es el tipo de error de CU-06 con código `CONTRATO_CONTRASENA_NO_ESTABLECIDA` y su motivo; **no** se produce respuesta de sesión, y el tipo de respuesta de sesión sigue declarando cuatro campos, sin ninguno agregado para este caso |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-02, y NB-01 por el papel de administrador |
| Reglas de negocio aplicables | Ninguna propia: este proyecto de código no las redacta. Aplica [`RN-01`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-01-Administrador-Unico-Y-Papeles-Fijos.md) de `GeometriaFactory-Domain`, que fija los dos papeles del producto y sostiene el conjunto cerrado del campo de papel. Ver `Especificacion-Funcional.md` §5 |
| Historias de usuario a generar en 06 | US-01 tipos de canje de credenciales; US-02 tipo de respuesta de sesión sin campos sensibles |
| Componentes esperados en 05 | Familia de tipos de transferencia de sesión del ensamblado de contratos |
| Tests previstos en 08 | Pruebas de integración contra el servicio real que ejercitan los cuatro desenlaces del canje: el exitoso (CA-01), el inválido (CA-03), el de cuenta no habilitada (CA-04) y el de **cuenta habilitada que todavía no estableció su contraseña**, que verifica FA-02 y CA-05 —código `CONTRATO_CONTRASENA_NO_ESTABLECIDA` con su motivo, y ausencia de respuesta de sesión—; ningún otro caso de uso cubre ese cuarto camino. Más la inspección de superficie pública para CA-02 |

## 10. Notas y supuestos

- El contrato no decide dónde vive la credencial de sesión después de recibida: esa decisión es de `GeometriaFactory-Web` y de 05.
- La forma del punto de acceso de canje —su ruta y su verbo— pertenece a `GeometriaFactory-Api` (`PRODUCT-INTAKE` §17.5 P.3) y no se especifica acá.
- El contrato no describe cómo se deriva ni cómo se verifica la contraseña: eso pertenece a `GeometriaFactory-Infrastructure`.
- **Fundamento de que la contraseña no establecida viaje como respuesta de error y no como campo de la respuesta de sesión.** Es la forma que el intake sostiene, y se declara acá para que 03, 05, 06 y 08 no vuelvan a abrir la pregunta. Descansa en dos apoyos distintos, y conviene no mezclarlos. **El primero fija el conteo**: `PRODUCT-INTAKE` §17.5 P.5 enumera los reclamos que la credencial transporta —identificador de usuario, correo, rol y expiración—, que son exactamente los cuatro campos de la respuesta de sesión de este contrato, y **no declara ningún quinto dato de sesión**. **El segundo fija el desenlace**: `PRODUCT-INTAKE` §6 flujo 1 y §4 F-04 describen el primer ingreso efectivo como el momento en que «el sistema le pide establecer su contraseña», es decir un desenlace distinto del ingreso y no una sesión con una marca; son esas dos secciones, y no §17.5 P.5, las que sostienen que la condición no viaje dentro de la sesión. Lo que §17.5 P.5 sí aporta es la **forma** de la respuesta que no es sesión, y lo hace para dos casos nombrados y sólo dos: respuesta genérica ante credenciales inválidas «sin revelar cuál campo falló», y respuesta **con motivo** ante cuenta `Pendiente` o `Bloqueada`. Una cuenta habilitada sin contraseña establecida **no es ninguna de las dos**, y por eso este contrato le da código propio en lugar de reutilizar `CONTRATO_CUENTA_NO_HABILITADA`: adopta la forma de respuesta con motivo que el intake ya usa para situaciones de cuenta, sin atribuirle al intake una categoría que no enumera.
- Supuesto de alcance: el contrato no declara ningún tipo de renovación de sesión, porque el intake no la incluye en este alcance (`PRODUCT-INTAKE` §17.5 P.5).

## 11. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Declara el contrato de canje de credenciales y de sesión, con la prohibición explícita de exponer contraseña almacenada, clave de firma y direcciones de servicio interno. |
| 1.0 | 2026-08-08 | Correcciones absorbidas de la ronda 1 de auditoría (`Audit/B-02-03-GeometriaFactory-Contracts-r1.md`), sin subir versión por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **H-02**: se resuelve la contradicción sobre la superficie del tipo de respuesta de sesión. El conjunto de campos queda en cuatro, y la condición de contraseña todavía no establecida deja de viajar como indicador de la respuesta de sesión y pasa a viajar como respuesta de error de CU-06 con el código propio `CONTRATO_CONTRASENA_NO_ESTABLECIDA`; se reescriben FA-02 y CA-05, se agrega la fila del código nuevo en §6 y §10 declara el fundamento con la cita del intake, para que 03, 05, 06 y 08 no reabran la pregunta. `CA-01`, §4 paso 4 y la sección de compatibilidad no cambian su conteo. **H-07**: la fila de reglas de negocio de §9 pasa a referir por identificador `RN-01` de `GeometriaFactory-Domain`, con enlace relativo. **H-09**: la sección opcional se renumera de §12 a §17, que es el número que `Rules-Especificacion-Funcional.md` §4.3 le asigna para `library`, con el motivo declarado en su encabezado. |
| 1.0 | 2026-08-08 | Correcciones absorbidas de la ronda 2 de auditoría (`Audit/B-02-03-GeometriaFactory-Contracts-r2.md`), sin subir versión por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **N-01**: la fila de tests previstos de §9 enumeraba tres desenlaces del canje y la corrección de H-02 había creado un cuarto; se agrega la prueba prevista del canje de una cuenta habilitada sin contraseña establecida, que verifica FA-02 y CA-05, y se declara que ningún otro caso de uso cubre ese camino. **N-04**: §10 parafraseaba `PRODUCT-INTAKE` §17.5 P.5 como «una cuenta que no está en condiciones de iniciar sesión», generalización que esa sección no hace —enumera `Pendiente` o `Bloqueada` y nada más—; el fundamento se reordena en sus dos apoyos reales, el conteo de reclamos por §17.5 P.5 y el desenlace por §6 flujo 1 y §4 F-04, y se declara explícitamente que la cuenta habilitada sin contraseña no es ninguna de las dos situaciones que el intake enumera, que es el motivo del código propio. La decisión de fondo no cambia. |

## 17. Compatibilidad de versión pública

Sección opcional de `Rules-Especificacion-Funcional.md` §4.3, que la numera **§17** y la reserva para `library`. Se conserva su número de la regla, aunque deje un hueco tras §11, para que un lector automatizado que busque §17 en cualquier caso de uso del producto encuentre siempre lo mismo.

- Agregar un campo opcional a la respuesta de sesión es compatible: la pieza pública que no lo lee sigue compilando.
- Quitar o renombrar cualquiera de los cuatro campos de la respuesta, o cambiar el conjunto de valores admitidos del campo de papel, es **cambio incompatible**: rompe la compilación de los dos extremos antes que el tiempo de ejecución.
- Ante un cambio incompatible, la pieza pública y la pieza de datos se despliegan juntas. No hay versionado de rutas porque no hay consumidores de terceros (`PRODUCT-INTAKE` §17.4 P.3).
