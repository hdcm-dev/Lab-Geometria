# CU-10001 — Registrar la cuenta de alumno

**Unidad de entrega:** GeometriaFactory-Web
**Documento:** CU-10001-Registrar-La-Cuenta-De-Alumno.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Analista Funcional senior (AG-02)
**Trazabilidad upstream:** `../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00002-Identidad-Propia-Del-Alumno-Sin-Correo.md` §1, §5 (primero y tercer criterio); `../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00001-Control-De-Admision-Al-Laboratorio.md` §1, §5 (segundo criterio); `../../../../00-Contexto/Vision-Producto.md` §9.1 y §9.2; `../../../../00-Contexto/Alcance-Producto.md` §4.1; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-02), §4.1 (RN-10002, RN-10006), §6 (flujo 1), §9 (**X-1** vigente y **X-2 retirada**), §17.6 P.3 y P.5
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

Permitir que un alumno de la comisión se dé de alta en el laboratorio con su correo, su nombre y su apellido, sin elegir contraseña y sin que el producto envíe ningún correo, y que reciba de inmediato la explicación de que su cuenta queda `Pendiente` hasta que el administrador la habilite.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Alumno | Primario | Completa el formulario de registro y recibe la explicación de la situación de su cuenta |
| Pieza pública | Sistema | Presenta el formulario, valida su completitud y arma la solicitud de registro contra la pieza de datos |
| Pieza de datos | Secundario | Registra la cuenta con situación pendiente y devuelve el resultado |

## 3. Precondiciones

- La pieza pública está publicada y su página inicial responde.
- El correo del alumno no pertenece todavía a ninguna cuenta del laboratorio.
- La ruta de registro es de acceso público: es la única del producto que no exige sesión, junto con la de ingreso.

## 4. Flujo principal

1. El alumno abre la ruta de registro sin tener sesión.
2. La pieza pública presenta el formulario con tres campos —correo, nombre y apellido— y **ningún campo de contraseña**.
3. El alumno completa los tres campos y confirma.
4. La pieza pública verifica que los tres campos estén completos antes de salir hacia la pieza de datos.
5. **La pieza pública, desde su propio servidor, invoca el contrato de registro** de `GeometriaFactory-Contracts` CU-10002, pasos 1 y 2. Ningún guion del navegador participa de esa llamada.
6. La pieza de datos registra la cuenta y devuelve el resultado con la situación inicial pendiente.
7. La pieza pública le muestra al alumno que su cuenta quedó registrada y que **no puede ingresar hasta que el administrador la habilite**, y le ofrece volver a la ruta de ingreso.

## 5. Flujos alternativos

| Id | Disparador | Curso | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El alumno intenta ingresar antes de que su cuenta esté habilitada | La pieza pública lo deriva a CU-10002, donde el intento de ingreso devuelve el motivo declarado de una cuenta `Pendiente` y no otorga sesión | El alumno vuelve más tarde al paso 1 de CU-10002 |
| FA-02 | El alumno deja un campo vacío | La pieza pública señala el campo faltante sin salir hacia la pieza de datos, y el formulario conserva lo ya escrito | El flujo vuelve al paso 3 |
| FA-03 | El alumno ya registrado vuelve a la ruta de registro | La pieza pública no bloquea el acceso a la ruta: la unicidad del correo la resuelve la pieza de datos y la respuesta llega como error de contrato, tratado en §6 | El flujo vuelve al paso 7 con el mensaje correspondiente |

## 6. Excepciones y errores

| Código | Causa | Respuesta del sistema |
| --- | --- | --- |
| `CONTRATO_CORREO_YA_REGISTRADO` | El correo ya pertenece a una cuenta | La pieza pública muestra un mensaje explícito sobre el campo de correo, sin revelar ningún dato de la cuenta existente. Recuperación: el alumno corrige el correo y reintenta |
| `CONTRATO_CAMPO_REQUERIDO_AUSENTE` | La solicitud llegó incompleta pese a la verificación del paso 4 | La pieza pública señala el campo que el contrato nombra. Recuperación por corrección y reintento |
| `CONTRATO_SERVICIO_NO_DISPONIBLE` | La pieza de datos no responde | La pieza pública entra en estado degradado según CU-10010: informa que el laboratorio no tiene los datos en este momento, **sin nombrar ninguna dirección de servicio interno**, conserva lo escrito y deja reintentar. Nunca una excepción sin manejar |
| `CONTRATO_ERROR_NO_CLASIFICADO` | Fallo que el contrato no previó | Handoff a CU-10010, con el mismo tratamiento de estado degradado |

## 7. Postcondiciones

- En caso de éxito: existe una cuenta de alumno con situación pendiente, el alumno sabe que no puede ingresar todavía y **no se envió ningún correo**.
- En caso de fallo: no queda cuenta a medio crear del lado de la pieza pública, que no guarda estado propio; el formulario conserva lo escrito para el reintento.
- En ningún caso: el navegador recibió una credencial de sesión, ni la pieza pública guardó contraseña alguna.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | La ruta de registro abierta sin sesión | El alumno mira el formulario | Presenta exactamente tres campos —correo, nombre y apellido— y ningún campo de contraseña |
| CA-02 | Un correo `alumno@ejemplo.test` no registrado | El alumno se registra con nombre `Ana` y apellido `Diaz` | La cuenta queda creada con situación pendiente y el mensaje declara que el administrador debe habilitarla |
| CA-03 | El correo `alumno@ejemplo.test` ya registrado | El alumno se registra otra vez con ese correo | La pieza pública muestra el mensaje de correo ya registrado sobre el campo de correo, y no crea una segunda cuenta |
| CA-04 | El servicio de datos detenido | El alumno confirma el registro | La página sigue en pie, muestra el estado degradado y el mensaje **no contiene ninguna dirección de servicio interno**; no se presenta ninguna excepción sin manejar |
| CA-05 | Un recorrido completo de registro | Se inspecciona el tráfico del navegador con las herramientas de desarrollo | Cero peticiones del navegador hacia la pieza de datos: la única llamada de registro sale del servidor de la pieza pública |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | [`NB-00002`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00002-Identidad-Propia-Del-Alumno-Sin-Correo.md), [`NB-00001`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00001-Control-De-Admision-Al-Laboratorio.md) |
| Reglas de negocio aplicables | [`RN-02002`](../../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02002-Correo-Del-Alumno-Unico.md), [`RN-02006`](../../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02006-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md). Se hacen cumplir en `GeometriaFactory-Domain`; acá se respetan y se explican, nunca se reimplementan |
| Contratos de uso consumidos | [`GeometriaFactory-Contracts` CU-08002](../../../../Producto/Contratos-Inter-Unidad/CU-08002-Contrato-De-Administracion-De-Cuentas.md) pasos 1 y 2; [`CU-08006`](../../../../Producto/Contratos-Inter-Unidad/CU-08006-Contrato-De-Respuesta-De-Error.md) |
| Fachada del visualizador | Ninguna función. Este caso de uso no dibuja |
| Historias de usuario a generar en 06 | US-10001, US-10002 |
| Componentes esperados en 05 | Página pública de registro y cliente tipado de la pieza de datos, ambos del lado del servidor de la pieza pública |
| Tests previstos en 08 | Guion de demostración de la etapa `d`, pasos de registro; verificación de tráfico cero del navegador hacia la pieza de datos |

## 10. Notas y supuestos

- La ausencia de canal de correo es decisión declarada del Product Owner: la **exclusión X-1**, notificaciones por correo, que sigue vigente. **X-2 —recuperación de contraseña olvidada— fue retirada el 2026-08-09**, cuando el Product Owner incorporó **F-26**, el reseteo desde el panel del administrador; ya no sostiene nada acá, y lo que sigue excluido es la recuperación **autónoma** por correo, que es lo que impide X-1. En este acto no hay confirmación de dirección ni contraseña provisoria: es el motivo por el que este caso de uso termina en una cuenta `Pendiente` y no en una sesión. La contraseña provisoria del producto existe, pero la produce la **habilitación** (RN-10016) y no este registro.
- La unicidad del correo la decide la pieza de datos. La pieza pública **no puede** ser la última defensa de esa regla, porque el navegador no es confiable y la ruta de registro es pública.
- La forma del formulario, su distribución y sus textos definitivos pertenecen a 03-UX-UI-DX. Acá sólo se declara qué campos hay y qué se le informa al alumno.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. |
| 1.1 | 2026-08-10 | **Cierra el hallazgo `C-06` (P1) del informe de auditoría `SDD/Docs/Audit/Coherencia-Corpus-r1.md` 1.0, contra `PRODUCT-INTAKE` 1.14.** La nota de **§10** fundaba la ausencia de canal de correo en «las exclusiones **X-1 y X-2**», y `PRODUCT-INTAKE` §9 muestra la fila de **X-2 tachada**, con el texto «Exclusión retirada el 2026-08-09» al incorporarse **F-26**: la nota citaba como vigente y como fundamento una exclusión que la fuente había retirado. Pasa a citar **sólo X-1**, con la constancia de que X-2 fue retirada, de qué sigue excluido —la recuperación autónoma por correo— y de que la contraseña provisoria del producto la produce la habilitación (RN-10016) y no este registro. La **cabecera de trazabilidad**, que citaba «§9 (X-1, X-2)» con el mismo defecto y que el informe no registra, se corrige igual. **Ningún curso, ningún criterio de aceptación y ningún desenlace cambia**: el registro sigue terminando en una cuenta `Pendiente` y no en una sesión. Sube minor. |
