# CU-00006 — Exponer el envío y la eliminación de un trabajo

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** CU-00006-Exponer-El-Envio-Y-La-Eliminacion-De-Un-Trabajo.md
**Versión:** 1.2
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-00003`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00003-Trabajo-Con-Dueno-Estado-Y-Persistencia.md), [`NB-00004`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00004-Interpretacion-Fiel-Del-Dato-Del-Alumno.md), [`NB-00009`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00009-Desenlace-Explicito-De-La-Entrega.md); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.26** §4 (F-06, F-07, F-22, F-24), §4.1 (RN-00003, RN-00004, RN-00005, RN-00008, RN-00009), §4.2, §6 (flujo 2), §7 (CL-3, CL-5), §12 (entradas «Enviar» y «Error de validación»), §14 (RA-03), §17.5.P.6, §20.E-2, §20.E-5, §20.E-8, §21; `Proyectos/GeometriaFactory-Contracts/.../CU-00003-Contrato-De-Carga-Y-Edicion-Del-Trabajo.md`; `Proyectos/GeometriaFactory-Application/.../CU-00004-Cargar-Y-Reeditar-Un-Trabajo-Propio.md`, `.../CU-00005-Enviar-Un-Trabajo-E-Interpretar-Su-Texto.md` y `.../CU-00009-Eliminar-Un-Trabajo.md`
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

Exponer los **tres** puntos con los que se escribe sobre un trabajo: el envío de uno nuevo (**A-10**), el reenvío de uno que quedó en `Borrador` (**A-11**) y la eliminación (**A-12**), que tiene dos alcances y **un solo punto de acceso**.

Este contrato lleva las dos cosas que esta capa puede arruinar sin que nada falle, y las dos están declaradas en su §10:

- **El texto original cruza la frontera del proceso, y el borde es el primer lugar donde puede alterarse.** El texto que el alumno pega no es JSON estrictamente válido: trae comas finales y claves que un lector ingenuo rechaza. Cualquier normalización en el borde —de codificación, de espacios, de saltos de línea— rompe la conservación íntegra sin producir ningún error.
- **Un envío cuyo texto no verifica es una respuesta exitosa.** Es la confusión más cara de esta capa, y §10 la desarrolla.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| `GeometriaFactory-Web` | Primario | Arma las solicitudes con el texto **exacto** que la persona pegó, sin normalizarlo, y las envía con el acceso firmado |
| Alumno | Sujeto de la regla | Envía y reenvía sus trabajos, y elimina **sólo los que están en `Borrador`** |
| Administrador | Sujeto de la regla | Elimina **cualquier trabajo que ve**, en cualquiera de sus tres estados |

## 3. Precondiciones

- La petición trae acceso firmado y atravesó la guardia de CU-00002.
- El envío y el reenvío exigen papel `Alumno`; la eliminación admite los dos papeles y **la regla que la acota vive adentro**.
- El servicio arrancó y dejó el almacén en condiciones (CU-00011).

## 4. Flujo principal

1. Llega una petición a **A-10** con nombre, fecha, descripción y el texto original.
2. **El texto se transporta tal como llegó**: no se normaliza, no se reordena y no se le quita ningún carácter.
3. Se ejerce el envío contra la capa de aplicación, que verifica la pertenencia, interpreta el texto por su puerto de validación y deja que el dominio resuelva el estado.
4. Se responde `201` con el resultado del envío: el identificador, **el estado que la interpretación decidió**, la fecha de registro y las observaciones que la interpretación produjo, **con su índice de figura y su campo**.
5. Llega una petición a **A-11** con el identificador y los mismos cuatro campos, para volver sobre un trabajo que quedó en `Borrador`.
6. Se responde `200` con el mismo tipo de resultado, con el estado que la interpretación decide esta vez.

**El estado llega decidido y esta capa no lo interpreta.** Que el resultado traiga `Borrador` no cambia el código de respuesta: la petición se cumplió y el trabajo se guardó.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El texto enviado **no verifica** —por ejemplo el de **E-5**, con un tipo desconocido, o el de **E-8**, con una dimensión no legible— | **El envío procede.** Se responde con éxito, el resultado trae estado `Borrador`, el texto conservado íntegro y las observaciones de error de validación **con índice de figura y campo**. La persona corrige y vuelve a enviar por A-11 | Termina con éxito |
| FA-02 | El texto verifica y produce advertencias —por ejemplo el de **E-2**, con su volumen declarado 343.00 contra el derivado 1029.00— | El envío procede igual, el resultado trae estado `Pendiente` y las advertencias, **que no bloquean nada** | Termina con éxito |
| FA-03 | El alumno elimina por **A-12** un trabajo suyo que está en `Borrador` | Se responde `204`, sin cuerpo. El trabajo deja de existir | Termina |
| FA-04 | El **administrador** elimina por **A-12** un trabajo en `Pendiente`, `Finalizado` o `Rechazado` | **Mismo punto, mismo verbo, misma solicitud.** Lo que cambia es la regla que lo acota, y esa regla vive adentro. Se responde `204` | Termina |
| FA-05 | Un alumno pide eliminar el trabajo de otro, cuyo identificador conoce | Se responde **exactamente igual** que ante un identificador inexistente: `404`, mismo código del contrato y mismo cuerpo. **Es el criterio que el intake exige verificar forzando la petición contra esta superficie** | Termina |

## 6. Excepciones y errores

| Código del contrato | Respuesta | Punto | Causa |
| --- | --- | --- | --- |
| `CONTRATO_CAMPO_REQUERIDO_AUSENTE` | `400` | A-10, A-11 | Falta el nombre, la fecha o el texto original. La respuesta **nombra el campo ausente** |
| `CONTRATO_TRABAJO_NO_ENCONTRADO` | `404` | A-11, A-12 | El identificador no existe, **o no es del solicitante, o está fuera de lo que ve**. Las tres respuestas son **indistinguibles** |
| `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` | `409` | A-12 | **El alumno** pide eliminar un trabajo suyo que no está en `Borrador`. La respuesta **declara el estado actual**. Este código **no se produce nunca en el camino del administrador**, porque a él no lo acota ningún estado |
| `CONTRATO_ERROR_NO_CLASIFICADO` | `409`, `403` o `503` | A-11, A-12 | `409` cuando se fuerza un reenvío sobre un trabajo que no está en `Borrador`, **camino para el que el contrato no declara código propio** (§10); `403` cuando el papel no alcanza; `503` cuando el almacén no está disponible o rechazó una escritura concurrente |

**Un texto que no verifica no aparece en esta tabla, y no es un olvido.** El ensamblado de contratos lo declara **señal y no error**, y §10 explica por qué convertirlo en un fallo sería el peor defecto posible de esta capa.

## 7. Postcondiciones

- **Envío o reenvío con éxito:** existe un trabajo con dueño, con identificador propio, con **el texto exactamente como llegó** y con el estado que la interpretación decidió. Las observaciones viajaron con su ubicación.
- **Eliminación con éxito:** el trabajo dejó de existir, con todo lo que colgaba de él, y **no queda ninguna marca de borrado**: el retiro es físico.
- **Fallo:** el almacén queda como estaba. En particular, **un reenvío rechazado no reemplaza el texto guardado**.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | El texto del escenario **E-2**, con sus **2** comas finales y su clave `"Tapas"`, tal como lo emite el programa del alumno | Se envía por A-10 | Responde con éxito, el estado resultante es `Pendiente`, y el texto guardado es **idéntico carácter por carácter** al enviado: **0 normalizaciones** |
| CA-02 | El texto del escenario **E-5**, con `"Tipo": "Piramide"` en la segunda figura | Se envía por A-10 | **Responde con éxito**, no con fallo. El resultado trae estado `Borrador` y **1** observación de error de validación con **índice de figura 1** y campo `Tipo` |
| CA-03 | El texto del escenario **E-8**, con `"Largo": "3,50"` como cadena | Se envía por A-10 | **Responde con éxito.** El resultado trae estado `Borrador` y la observación localizada por índice de figura y campo, según lo que el `PRODUCT-INTAKE` **1.12** fija en §20.E-8 punto 5 |
| CA-04 | El texto del escenario **E-1**, de tres piezas | Se envía por A-10 | Responde con éxito con estado `Pendiente` y **2** advertencias, y **0** de ellas impide el paso de estado |
| CA-05 | Un trabajo en `Pendiente` de un alumno | El **alumno** lo elimina por A-12 | Responde `409` declarando el estado actual, y el trabajo **sigue existiendo**. Verificado **forzando la petición contra esta superficie**, no ocultando el control en una pantalla |
| CA-06 | El mismo trabajo | El **administrador** lo elimina por A-12 | Responde `204` y el trabajo deja de existir |
| CA-07 | Un trabajo del alumno A y un identificador inexistente | El alumno B pide **eliminar** cada uno | Las **2** respuestas son `404`, con el mismo código y cuerpos **idénticos**: **0 campos** permiten distinguirlos |
| CA-08 | Un trabajo en `Borrador` con su texto guardado | Se reenvía por A-11 con un texto nuevo y la petición falla por almacén no disponible | Responde `503` y el texto guardado **sigue siendo el anterior**: 0 reemplazos parciales |
| CA-09 | Cualquier respuesta de §6, con el cuerpo y el registro del servidor observados | Se produce | **0 apariciones** de la ruta del almacén y de la dirección de cualquier servicio interno, y **0 apariciones del texto completo del alumno** en el registro |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-00003, NB-00004, y NB-00009 por la eliminación del administrador |
| Reglas de negocio aplicables | [**RN-02003**](../Reglas-De-Negocio/RN-02003-Trabajo-Ajeno-Indistinguible-De-Inexistente.md), **con el tramo que esta capa puede romper sola**: elegir un código que distinga el trabajo ajeno del inexistente. [RN-02004](../Reglas-De-Negocio/RN-02004-Eliminacion-Acotada-Al-Borrador.md), en los dos alcances, y **es la única regla del producto cuyo criterio de verificación exige forzar la petición contra esta superficie**. [RN-02008](../Reglas-De-Negocio/RN-02008-Texto-Original-Conservado-Integro.md), en el borde donde el texto puede alterarse. [RN-02005](../Reglas-De-Negocio/RN-02005-Finalizacion-Sin-Errores-De-Validacion.md), **sin tramo acá y declarada por lo que esta capa no hace**. [RN-02009](../Reglas-De-Negocio/RN-02009-Observacion-De-Error-Con-Posicion-Y-Campo.md), porque la ubicación cruza la frontera sin recortarse |
| Regla de arquitectura del producto | **RA-03**, con una exigencia propia: **el texto del alumno no entra al registro del servidor**, ni entero ni en parte |
| Puntos de acceso | A-10, A-11, A-12 |
| Contrato de uso que transporta | `GeometriaFactory-Contracts` `CU-00003`, incluida su señal declarada |
| Escenarios que lo ejercitan | **E-1**, **E-2**, **E-5** y **E-8** del intake §20, sin renumerar y **sin inventar ningún texto** |
| Historias de usuario a generar en 06 | US-00017, US-00018, US-00019, US-00020 |
| Componentes esperados en 05 | Tres puntos de acceso; y la decisión sobre el límite de tamaño del cuerpo, que es punto abierto |
| Tests previstos en 08 | Integración por los nueve criterios, con los textos de los escenarios como datos; **y la prueba que fuerza la eliminación contra la superficie**, que la fuente declara bloqueante |

## 10. Notas y supuestos

- **La confusión más cara de esta capa, en una línea: un envío cuyo texto no verifica es una respuesta exitosa.** Si respondiera con un código de fallo, el producto le estaría diciendo a la persona que su petición estaba mal, cuando lo que pasó es que **su programa emitió algo que no se puede interpretar** —y el trabajo, mientras tanto, quedó guardado con su texto y sus observaciones—. La persona vería un fallo y no vería lo único que le sirve, que es en qué figura y en qué campo está el problema. Es exactamente el defecto que el producto viene a eliminar, reintroducido en el último tramo.
- **El borde del proceso es el primer lugar donde el texto del alumno puede perderse**, y ninguna capa de adentro puede repararlo: cuando la interpretación lo recibe, ya está alterado. Las tres formas de romperlo sin que nada falle son **normalizar la codificación**, **recortar por un límite de tamaño** y **reserializar el texto como si fuera JSON**. Las tres dejan el sistema funcionando.
- **El límite de tamaño del cuerpo es un punto abierto.** Ninguna fuente lo declara. Un límite mal elegido trunca el texto de un alumno y rompe la conservación íntegra en silencio; no tener ninguno deja la superficie sin corte declarado. Está elevado en el índice maestro §11.
- **El reenvío forzado sobre un trabajo que no está en `Borrador` no tiene código propio en el contrato.** El código análogo del conjunto cerrado está **acotado por su enunciado a la eliminación y al camino del alumno**. Mientras eso no se resuelva, el camino disponible es el código genérico con respuesta `409`, y es un caso donde el producto sabe decir algo mejor en la operación vecina que en ésta. Está elevado al Product Owner en el índice maestro §11.
- **Dos puntos para el envío y uno para la eliminación no es una asimetría casual.** El alta y el reenvío se distinguen por lo que puede fallar —un reenvío puede referirse a un trabajo que no existe o que ya no está en `Borrador`, y un alta no—, mientras que las dos eliminaciones no se distinguen en nada que la superficie deba conocer: **el mismo tipo, el mismo verbo y la misma respuesta**, con la regla que los separa viviendo adentro. Dos puntos de eliminación habrían puesto el papel en la ruta, que es información sobre el solicitante en un lugar donde no hace falta.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. |
| 1.1 | 2026-08-10 | **Realineación de la cita viva al `PRODUCT-INTAKE` 1.13.** Este proyecto de código se emitió contra la **1.12** y la fuente está hoy en **1.13**, que incorpora la regla **RN-00016** —habilitar una cuenta produce su contraseña provisoria— y precisa la capacidad **F-04**. La cabecera de trazabilidad pasa a citar **1.13**; la cita de la emisión inicial se conserva en la fila 1.0, que es trazabilidad y no una referencia desactualizada. **Ninguna sección de este contrato de uso se toca**: la decisión de 1.13 alcanza al circuito de credenciales y este caso de uso no lo expone. Sube minor: corrige una cita de trazabilidad. |
| 1.2 | 2026-08-11 | **Cierra el hallazgo `B-API-13` (P3)** del informe [`B-02-03-GeometriaFactory-Api-r1.md`](../../../../Audit/B-02-03-GeometriaFactory-Api-r1.md) 1.0, en la extensión que la búsqueda de propagación que el propio informe exige dejó al descubierto: la cabecera citaba `PRODUCT-INTAKE` **1.13** y pasa a citar **1.26**, vigentes hoy. El informe listaba **nueve** cabeceras envejecidas y sólo una de esta carpeta, `CU-00012`; el `grep` sobre las categorías 02 y 03 devuelve **diecinueve** archivos con la cita vieja, **los doce casos de uso entre ellos**, y los diecinueve se corrigen en esta tanda. Se abrieron las secciones del intake que este caso de uso cita y **su contenido no cambió** entre 1.13 y 1.26 en nada que este documento afirme, de modo que **no había ninguna afirmación falsa**: lo que se repara es la trazabilidad. **Ningún paso, código, regla, criterio de aceptación ni recuento cambia.** Sube minor. |
