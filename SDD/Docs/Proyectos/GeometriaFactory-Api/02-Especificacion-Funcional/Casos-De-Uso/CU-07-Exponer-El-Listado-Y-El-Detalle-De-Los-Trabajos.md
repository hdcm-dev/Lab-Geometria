# CU-07 — Exponer el listado y el detalle de los trabajos

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** CU-07-Exponer-El-Listado-Y-El-Detalle-De-Los-Trabajos.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-03`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-03-Trabajo-Con-Dueno-Estado-Y-Persistencia.md), [`NB-05`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-05-Visibilidad-Del-Error-De-Calculo.md), [`NB-06`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-06-Visualizacion-Dentro-Del-Producto.md), [`NB-07`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-07-Revision-De-La-Comision-En-Un-Solo-Lugar.md), [`NB-09`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-09-Desenlace-Explicito-De-La-Entrega.md); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.12** §4 (F-08, F-11, F-12, F-21), §4.1 (RN-03, RN-09, RN-11), §4.2, §7 (CL-5), §14 (RA-03), §17.4.P.10, §17.5.P.10; `Proyectos/GeometriaFactory-Contracts/.../CU-04-Contrato-De-Listado-De-Trabajos.md` y `.../CU-05-Contrato-De-Detalle-Del-Trabajo-Interpretado.md`; `Proyectos/GeometriaFactory-Application/.../CU-06-Consultar-Los-Trabajos-Propios-Del-Alumno.md` y `.../CU-07-Revisar-Los-Trabajos-De-La-Comision.md`
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

Exponer los **dos** puntos de lectura de la superficie: el listado de trabajos (**A-13**) y el detalle de uno (**A-14**). Los dos admiten los dos papeles y **en los dos el alcance de lo que se devuelve llega decidido**: el alumno ve lo suyo, el administrador ve todo menos los borradores, y **esta capa no elige nada de eso**.

Lo que sí es propio de este contrato, y es lo que hay que poder verificar sobre él:

- **La superficie no ofrece ningún parámetro con el que pedir borradores ajenos.** El alcance no llega como una opción que el solicitante elige, y por eso no hay nada que forzar.
- **La proyección de listado no arrastra el texto original, ni los componentes de las piezas, ni el comentario del administrador.** Es un requisito estructural declarado por el intake §17.4.P.10, y su motivo es directo: el listado del administrador cargaría el texto completo de cada trabajo de la comisión.
- **La ubicación de cada observación cruza la frontera sin recortarse**: el índice de figura y el campo llegan al otro lado tal como se produjeron.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| `GeometriaFactory-Web` | Primario | Pide el listado y el detalle con el acceso firmado, y arma con ellos el panel del alumno o el del administrador |
| Alumno | Sujeto de la regla | Ve **sólo sus trabajos**, en sus cuatro estados posibles |
| Administrador | Sujeto de la regla | Ve los de toda la comisión **menos los que están en `Borrador`** |

## 3. Precondiciones

- La petición trae acceso firmado y atravesó la guardia de CU-02.
- El servicio arrancó y dejó el almacén en condiciones (CU-11).

## 4. Flujo principal

1. Llega una petición a **A-13**.
2. Se pide el listado a la capa de aplicación, que traslada a la consulta el recorte que el papel determina.
3. Se responde `200` con la colección de elementos de listado, **sin texto original, sin componentes y sin comentario**.
4. Llega una petición a **A-14** con el identificador de un trabajo.
5. Se pide el detalle a la capa de aplicación, que verifica la pertenencia o el alcance sobre el dato recuperado.
6. Se responde `200` con el detalle: los datos del trabajo, **el texto original**, las piezas con sus componentes, las observaciones con su severidad y su par de valores, y **el comentario del administrador cuando lo hay**.

**El detalle es el mismo para los dos papeles.** El administrador ve exactamente lo que ve el alumno, que es lo que le permite revisar lo que el alumno entregó y no una versión distinta de lo mismo.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El listado se pide con un acceso de papel `Alumno` | El recorte que llega es por dueño, y la colección trae **sólo** sus trabajos, en los cuatro estados | Paso 3 |
| FA-02 | El listado se pide con un acceso de papel `Administrador` | El recorte que llega excluye los borradores, y la colección trae los de toda la comisión **con el dato de dueño**, que es lo que después permite agrupar y filtrar en la pantalla | Paso 3 |
| FA-03 | El listado no tiene ningún elemento que devolver | Se responde `200` con una colección vacía. **Un listado vacío no es un fallo**: la pieza pública lo distingue del fallo por el tipo recibido y no por el conteo, y una comisión sin entregas todavía es un caso normal | Termina |
| FA-04 | El administrador pide el detalle de un trabajo que está en `Borrador` | Responde **exactamente igual** que ante un identificador inexistente: el trabajo que no ve le resulta indistinguible del que no existe | Termina |
| FA-05 | El detalle pedido corresponde a un trabajo ya resuelto, con comentario | El detalle lo trae en su bloque propio. **El comentario nunca viaja como una observación más**: no comparten ni un campo | Paso 6 |

## 6. Excepciones y errores

| Código del contrato | Respuesta | Punto | Causa |
| --- | --- | --- | --- |
| `CONTRATO_ALUMNO_NO_ENCONTRADO` | `404` | A-13 | El filtro por alumno referencia un identificador que no existe. Recuperación: reintentar sin filtro |
| `CONTRATO_TRABAJO_NO_ENCONTRADO` | `404` | A-14 | El identificador no existe, **o no es del solicitante, o está fuera de lo que ve**. Las tres respuestas son **indistinguibles** |
| `CONTRATO_ERROR_NO_CLASIFICADO` | `403` o `503` | A-13, A-14 | `403` cuando el papel no alcanza para el filtro pedido y el contrato no tiene código propio; `503` cuando el almacén no está disponible |

**El listado vacío no está en esta tabla, y es deliberado**: el ensamblado de contratos lo declara señal y no error, y en esta superficie viaja en una respuesta exitosa.

## 7. Postcondiciones

- **Éxito en A-13:** la pieza pública tiene la colección con el recorte ya aplicado, **sin ningún dato que permita inferir la existencia de trabajos fuera de ese recorte**.
- **Éxito en A-14:** la pieza pública tiene todo lo que hace falta para dibujar la escena, armar el árbol y mostrar las observaciones y el comentario.
- **En los dos:** **nada cambió**. Son los dos únicos puntos de esta superficie que no escriben.
- **Fallo:** código de respuesta, y ninguna información sobre lo que el solicitante no ve.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un alumno con un trabajo en `Borrador` y uno en `Pendiente`, y otro alumno con dos trabajos | El primero invoca A-13 | Responde `200` con **2** elementos, los suyos, y **0** del otro alumno |
| CA-02 | La misma situación | El administrador invoca A-13 | La colección trae **el trabajo en `Pendiente` del primero y los del segundo que no estén en `Borrador`**, y **0 borradores** |
| CA-03 | El punto A-13 | Se inspecciona su superficie de parámetros | **0 parámetros** permiten pedir trabajos en `Borrador` ajenos, y 0 permiten ampliar el recorte que el papel determina |
| CA-04 | Un elemento cualquiera del listado | Se inspecciona | Trae identificador, nombre, fecha, estado y dueño, y **0 campos** de texto original, de componentes de pieza y de comentario |
| CA-05 | Un trabajo enviado con el texto del escenario **E-1** | Se invoca A-14 sobre él | El detalle trae **3** piezas con sus componentes, el texto original **idéntico al enviado** y **2** observaciones de especie advertencia, cada una con su valor declarado y su valor derivado |
| CA-06 | Un trabajo enviado con el texto del escenario **E-5** | Se invoca A-14 sobre él | La observación de error de validación llega con **índice de figura 1** y campo `Tipo`: la ubicación **no se recortó al cruzar** |
| CA-07 | Un trabajo del alumno A y un identificador inexistente | El alumno B invoca A-14 sobre cada uno | Las **2** respuestas son `404` con cuerpos **idénticos**: 0 campos permiten distinguirlos |
| CA-08 | Un trabajo en `Borrador` del alumno A | El **administrador** invoca A-14 sobre él | Responde `404`, con el mismo cuerpo que ante un identificador inexistente |
| CA-09 | Un trabajo ya resuelto con comentario del administrador | Se invoca A-14 | El comentario llega en **su propio bloque** y **0** elementos de la colección de observaciones lo contienen |
| CA-10 | Una comisión sin ningún trabajo | El administrador invoca A-13 | Responde `200` con **0** elementos, y **no** un código de fallo |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-03; NB-05, NB-06 y NB-07 **parcialmente**, con el reparto declarado en el índice maestro §7.2; NB-09 parcialmente, por el desenlace y el comentario que el detalle transporta |
| Reglas de negocio aplicables | [**RN-03**](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-03-Trabajo-Ajeno-Indistinguible-De-Inexistente.md), en la traducción que hace indistinguible el trabajo ajeno del inexistente. [RN-11](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-11-El-Administrador-No-Ve-Los-Borradores.md), **de forma negativa**: la superficie no ofrece la puerta. [RN-09](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-09-Observacion-De-Error-Con-Posicion-Y-Campo.md), porque la ubicación cruza sin recortarse |
| Regla de arquitectura del producto | **RA-03** en las condiciones de §6 |
| Puntos de acceso | A-13, A-14 |
| Contratos de uso que transporta | `GeometriaFactory-Contracts` `CU-04` y `CU-05` |
| Escenarios que lo ejercitan | **E-1** y **E-5** del intake §20 |
| Historias de usuario a generar en 06 | US-21, US-22 |
| Componentes esperados en 05 | Dos puntos de acceso de lectura, con la proyección de listado separada del detalle |
| Tests previstos en 08 | Integración por los diez criterios, **incluida la inspección estructural de los parámetros de A-13** y la de los campos del elemento de listado |

## 10. Notas y supuestos

- **Que la proyección de listado sea distinta del detalle no es una optimización, es lo que hace verificable el requisito.** Con un único punto de lectura, la exigencia de que el listado no arrastre el texto de cada trabajo no tendría dónde comprobarse. Es el mismo fundamento con el que el ensamblado de contratos separó sus dos familias de tipos.
- **El alcance no es un parámetro y por eso no hay nada que forzar.** Es la diferencia entre una superficie que ofrece pedir de más y confía en que alguien lo rechace, y una que **no lo ofrece**. Las dos son verificables, pero la segunda se verifica por inspección y la primera sólo forzándola.
- **El detalle es el mismo para los dos papeles, y eso está declarado como valor y no como economía.** El administrador revisa exactamente lo que el alumno entregó; una vista distinta para él abriría la posibilidad de que revise algo que el alumno no vio.
- **El comentario del administrador no es una observación**, y esta superficie lo transporta en un bloque propio. Confundirlos haría que un comentario apareciera entre las advertencias de geometría del alumno.
- **El listado de trabajos y el listado de cuentas son dos puntos distintos**, y ninguno de los dos transporta lo del otro. El resumen por alumno y por estado que el intake menciona como capacidad de prioridad menor **no está en esta superficie** y no se anticipa acá.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. |
