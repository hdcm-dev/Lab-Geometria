# CU-06008 — Emitir el acceso firmado

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** CU-06008-Emitir-El-Acceso-Firmado.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-00002`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00002-Identidad-Propia-Del-Alumno-Sin-Correo.md); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.12** §4 (F-05), §4.1 (RN-06006, RN-06013), §14 (RA-03), §17.1.P.1 · GeometriaFactory-Infrastructure, §17.1.P.5 · GeometriaFactory-Infrastructure, §17.1.P.5 · GeometriaFactory-Api; el flujo que lo consume vive en `Proyectos/GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-06001-Contrato-De-Canje-De-Credenciales-Y-Sesion.md`
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico` y `08-Calidad-Y-Pruebas` de GeometriaFactory-Infrastructure

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
- [17. Compatibilidad de la superficie pública](#17-compatibilidad-de-la-superficie-pública)

---

## 1. Propósito

Emitir el **acceso firmado** con el que la pieza pública opera contra la pieza de datos, y verificar uno recibido. El acceso lleva el identificador de la cuenta, su correo, su **papel** y su expiración, y va firmado con clave simétrica.

Es la segunda pieza sensible de este proyecto de código, junto con la derivación de credenciales. Lo que la vuelve sensible no es la firma sino **dónde vive la clave**: fuera del repositorio de código y fuera de la imagen, en una variable de entorno o en un archivo montado. Ningún secreto entra al repositorio, tampoco en la construcción automatizada.

Lo que este caso de uso **no** hace: no decide si la cuenta admite el acceso —eso lo resuelve el dominio con el estado y la marca, y llega resuelto—, no compara credenciales —eso es `CU-06006`— y **no sostiene sesión**: el acceso no tiene estado del lado de la pieza de datos.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Consumidor (composición de raíz de `GeometriaFactory-Api`) | Primario | Pide emitir un acceso para una cuenta ya admitida, o verificar uno recibido |
| Clave de firma | Sistema | Se genera o se provee en el primer arranque y **vive fuera del repositorio de código y fuera de la imagen** |

## 3. Precondiciones

- La admisibilidad de la cuenta **ya está resuelta**: una cuenta `Pendiente`, `Bloqueado` o con la marca de cambio de contraseña pendiente **no llega acá**.
- La clave de firma está provista.

## 4. Flujo principal

1. El consumidor pide emitir un acceso para una cuenta ya admitida, aportando su identificador, su correo y su papel.
2. Se compone el acceso con esos reclamos más su **expiración**, de vigencia corta.
3. Se firma con la clave simétrica provista.
4. Se devuelve el acceso.
5. Para verificar, se comprueba la firma y la expiración de un acceso recibido y se devuelven sus reclamos, o el motivo por el que no es válido.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | Se verifica un acceso vencido | Se responde **no válido, por expiración**. No es una condición de error de este contrato: la renovación del producto es **por reingreso** y **no hay acceso de refresco** en este alcance | Termina la operación |
| FA-02 | Se verifica un acceso cuya firma no corresponde | Se responde **no válido**. Tampoco es una condición de error: es exactamente lo que este contrato existe para detectar | Termina la operación |
| FA-03 | La cuenta a la que se le emite el acceso tiene papel `Administrador` | Se emite igual, con el papel en su reclamo. **El papel viaja en el acceso; qué habilita cada papel lo deciden las capas de adentro** | Paso 4 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del caso de uso |
| --- | --- | --- |
| `CLAVE_DE_FIRMA_AUSENTE` | No hay clave de firma provista en el arranque | Termina sin emitir y sin verificar. **No se genera una clave de reemplazo al vuelo y no se emite sin firmar**: un acceso sin firma verificable es peor que ningún acceso, porque el sistema seguiría funcionando y nadie lo notaría hasta que alguien lo falsifique |
| `RECLAMOS_INCOMPLETOS` | Se pidió emitir sin identificador de cuenta, sin correo, sin papel o sin expiración | Termina sin emitir. **Ninguno de los cuatro se completa con un valor por defecto**: un acceso sin papel dejaría a las capas de adentro decidiendo sobre un dato que nadie declaró, y uno sin expiración no vencería nunca |

**Ninguna de las dos escribe nada** y **ninguna incluye en su respuesta la clave de firma ni la dirección de ningún servicio interno**.

## 7. Postcondiciones

- **Éxito al emitir:** el consumidor recibe el acceso firmado, con sus cuatro reclamos.
- **Éxito al verificar:** el consumidor recibe los reclamos, o el motivo por el que el acceso no es válido.
- **Fallo:** el consumidor recibe el código y **ningún acceso**.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Una cuenta de alumno admitida y una clave de firma provista | Se emite el acceso | El acceso lleva **identificador, correo, papel `Alumno` y expiración**, y verifica con la misma clave |
| CA-02 | El mismo acceso y **otra** clave de firma | Se verifica | Responde **no válido** |
| CA-03 | Un acceso cuya expiración ya pasó | Se verifica | Responde **no válido por expiración**, y **no** un código de error de este contrato |
| CA-04 | Un arranque sin clave de firma provista | Se pide emitir | Devuelve `CLAVE_DE_FIRMA_AUSENTE` y **0 accesos emitidos**. En particular **no** se emite un acceso sin firma ni con una clave generada al vuelo |
| CA-05 | Una petición de emisión sin papel | Se emite | Devuelve `RECLAMOS_INCOMPLETOS` y **0 accesos emitidos** |
| CA-06 | Cualquiera de los códigos de §6, con el mensaje que llega al consumidor observado | Se produce la condición | El mensaje **no contiene la clave de firma, ni la dirección de ningún servicio interno, ni la ruta del archivo del almacén**, y el error queda registrado del lado del servidor |
| CA-07 | El repositorio de código y la imagen de despliegue | Se inspeccionan | **No contienen ninguna clave de firma.** El valor llega por variable de entorno o por archivo montado |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-00002 |
| Reglas de negocio aplicables | [RN-02006](../../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02006-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md) y [RN-02013](../../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02013-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md), **por lo que este contrato no decide**: las dos se resuelven antes y por eso una cuenta no admitida nunca llega acá |
| Regla de arquitectura del producto | **RA-03**: ningún mensaje que se muestre incluye direcciones de servicios internos. Acá se ejerce en las respuestas de §6, y su registro del lado del servidor es el que permite diagnosticar sin exponer |
| Mecanismo que provee | La emisión y la verificación del acceso firmado |
| Consumidor | La composición de raíz de `GeometriaFactory-Api` |
| Historias de usuario a generar en 06 | US-06021, US-06022 |
| Componentes esperados en 05 | Emisor y verificador del acceso, y la provisión de la clave de firma desde configuración |
| Tests previstos en 08 | Unitarias sin base de datos sobre los cuatro reclamos, la firma y la expiración; y una verificación de que ni el repositorio de código ni la imagen contienen la clave |

## 10. Notas y supuestos

- **La vigencia es corta y la renovación es por reingreso.** No hay acceso de refresco en este alcance, y esta categoría no lo agrega. **El valor exacto de la vigencia no está declarado por ninguna fuente** y queda como punto abierto.
- **El acceso no llega al navegador.** Vive del lado del servidor de la pieza pública, y eso es un criterio verificable de una etapa del producto. Este contrato no lo puede garantizar solo —lo garantiza quien lo guarda— pero sí lo condiciona: un acceso de vigencia corta acota el daño si alguna vez se filtrara.
- **La respuesta genérica ante credenciales inválidas es del consumidor**, no de acá: este contrato no distingue hacia afuera cuál campo falló porque **no ve los campos**, sólo recibe una cuenta ya admitida.
- **La clave de firma no se versiona, no se registra y no aparece en ningún mensaje.** Es la única forma de que la aceptación del riesgo del tramo en claro siga siendo un riesgo acotado y no una puerta abierta.
- **Este contrato no sostiene sesión.** La pieza de datos es sin estado; lo que se parece a una sesión vive en el circuito de la pieza pública.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. |
| 1.1 | 2026-08-10 | Actualización de la cita del `PRODUCT-INTAKE` de **1.11** a **1.12** en la trazabilidad upstream: 1.11 quedó archivada al resolver el Product Owner el desenlace del envío del escenario `E-8`. Corrige el hallazgo **H-02** del informe de auditoría `SDD/Docs/Audit/B-02-03-GeometriaFactory-Infrastructure-r1.md` (ronda 1). El delta entre 1.11 y 1.12 se revisó y sólo alcanza a `E-8`, que no toca lo que este documento declara: sin cambios de contenido. |

## 17. Compatibilidad de la superficie pública

Agregar un reclamo al acceso es compatible mientras los cuatro declarados se conserven. **Emitir sin firmar, generar la clave al vuelo cuando no está provista, quitar la expiración, incorporar la clave al repositorio de código o a la imagen, y devolver la dirección de un servicio interno en un mensaje son cambios incompatibles** y suben versión mayor; el último contradice además RA-03, que es una regla de nivel producto.
