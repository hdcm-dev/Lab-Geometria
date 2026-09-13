# CU-08007 — Contrato de desenlace de la revisión

**Producto:** Fábrica de Geometría
**Documento:** CU-08007-Contrato-De-Desenlace-De-La-Revision.md
**Versión:** 1.3
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00009-Desenlace-Explicito-De-La-Entrega.md` §1, §5; `NB-00007-Revision-De-La-Comision-En-Un-Solo-Lugar.md` §5; `00-Contexto/Vision-Producto.md` §9.1 (Estado del trabajo, Aprobar / Rechazar, Comentario) y §9.2 (`Pendiente`, forma calificada obligatoria); `00-Contexto/Alcance-Producto.md` §4.1; `PRODUCT-INTAKE` **1.14** §4 (F-21, F-23), §4.1 (RN-08010), §4.2 (modelo de estados), §6 (flujo 2.1), §7 (CL-10, CL-11), §9 (retiro de X-5), §12 (glosario), §17.4 P.3, P.5 y P.10
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

Declarar el tipo de transferencia con el que el administrador resuelve un trabajo en estado `Pendiente`: lo aprueba, con lo que pasa a `Finalizado`, o lo rechaza, con lo que pasa a `Rechazado`, y en cualquiera de los dos casos puede acompañar la decisión con un comentario escrito **opcional**. Los dos estados de desenlace son terminales, de modo que el contrato transporta una decisión que se toma una sola vez por trabajo.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Código de la pieza pública compilado contra el contrato | Primario | Arma la solicitud de desenlace con la decisión del administrador y su comentario, si lo escribió, y consume el resultado |
| Código de la pieza de datos compilado contra el contrato | Sistema | Produce el resultado con el estado alcanzado, sobre los mismos tipos |
| Ensamblado de contratos | Sistema | Declara el conjunto cerrado de desenlaces y el carácter opcional del comentario |

## 3. Precondiciones

- Los dos extremos están compilados contra la misma versión del ensamblado de contratos.
- El código de la pieza pública tiene una credencial de sesión obtenida por CU-08001 cuyo papel es de administrador.
- El código de la pieza pública tiene un identificador de trabajo obtenido por CU-08004, que es el listado que sólo le muestra al administrador trabajos en estado `Pendiente`, `Finalizado` o `Rechazado`.
- El contrato declara el conjunto cerrado de dos desenlaces: aprobar y rechazar.

## 4. Flujo principal

1. El código de la pieza pública arma la solicitud de desenlace con tres campos: identificador del trabajo, desenlace pretendido y comentario.
2. El código de la pieza pública deja el campo de comentario sin poblar cuando el administrador no escribió nada: el contrato lo declara opcional y no impone longitud mínima.
3. El código de la pieza pública envía la solicitud a la pieza de datos.
4. El código de la pieza de datos responde con el resultado de desenlace, que trae el estado alcanzado por el trabajo —`Finalizado` si el desenlace fue aprobar, `Rechazado` si fue rechazar— y el momento en que se registró.
5. El código de la pieza pública vuelve al listado de CU-08004, donde el trabajo ya figura con su estado nuevo.
6. El comentario, si se escribió, queda disponible para el alumno dueño en el detalle del trabajo de CU-08005, que es el único tipo del ensamblado que lo transporta.

## 5. Flujos alternativos

| Id | Disparador | Curso | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El administrador rechaza en lugar de aprobar | El contrato es la misma solicitud: sólo cambia el valor del campo de desenlace pretendido. No hay un tipo distinto para cada decisión | El flujo continúa en el paso 4, con estado alcanzado `Rechazado` |
| FA-02 | El administrador escribe un comentario | El campo de comentario viaja poblado con el texto libre tal como lo escribió, sin estructura, sin severidad y sin ubicación | El flujo continúa en el paso 4 |
| FA-03 | El administrador quiere quitar de su listado un trabajo ya resuelto | El desenlace no ofrece camino: la eliminación por el administrador es la solicitud de eliminación de CU-08003, FA-04, que alcanza cualquier estado que él ve | El flujo termina; el trabajo desaparece del listado de CU-08004 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del contrato |
| --- | --- | --- |
| `STATE_FORBIDS_OUTCOME` | El trabajo no está en estado `Pendiente`: o nunca lo estuvo, o ya recibió su desenlace y está en un estado terminal | Respuesta de error de CU-08006 que declara el estado actual del trabajo. Terminación controlada: el contrato no ofrece camino para revertir un estado terminal |
| `OUTCOME_ADMIN_ONLY` | Quien solicita el desenlace no es el administrador, aun sobre un trabajo propio en estado `Pendiente` | Respuesta de error de CU-08006 con texto neutro. Terminación controlada |
| `WORK_NOT_FOUND` | El identificador no corresponde a ningún trabajo que el solicitante vea, o no existe. Incluye el trabajo en estado `Borrador`, que el administrador no ve | Respuesta de error de CU-08006 con texto neutro que no distingue los casos. Terminación controlada |
| `REQUIRED_FIELD_MISSING` | La solicitud llega sin identificador o sin desenlace pretendido. **Nunca por el comentario**, que es opcional | Respuesta de error de CU-08006 que nombra el campo ausente. Recuperación por corrección y reintento |
| `SERVICE_UNAVAILABLE` | La pieza de datos no responde | Respuesta de error de CU-08006 con texto neutro y sin dirección del servicio que falló. Handoff al estado degradado |

## 7. Postcondiciones

- En caso de éxito: el código de la pieza pública tiene el estado terminal alcanzado por el trabajo, y el comentario, si se escribió, queda accesible desde el detalle de CU-08005 para el alumno dueño.
- En caso de fallo: el código de la pieza pública tiene un tipo de error de CU-08006 y el trabajo conserva el estado que tenía; el contrato no transporta ningún desenlace parcial.
- En ningún caso: el contrato ofrece una forma de salir de un estado terminal ni de reemplazar un comentario ya registrado.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un trabajo en estado `Pendiente` y una sesión de papel administrador | El código de la pieza pública solicita el desenlace con el valor aprobar y el comentario sin poblar | El resultado trae estado alcanzado `Finalizado`, y la solicitud es válida con **0 campos de comentario poblados** |
| CA-02 | El mismo trabajo en estado `Pendiente` | El código de la pieza pública solicita el desenlace con el valor rechazar y el comentario poblado con `Revisá la fórmula del área del cubo` | El resultado trae estado alcanzado `Rechazado`, y el comentario queda accesible con ese texto exacto en el detalle de CU-08005 |
| CA-03 | Un trabajo en estado `Finalizado` | El código de la pieza pública solicita cualquiera de los dos desenlaces | La respuesta es el tipo de error de CU-08006 con código `STATE_FORBIDS_OUTCOME` y declara el estado actual `Finalizado`: **0 transiciones salen de un estado terminal** |
| CA-04 | Un trabajo propio en estado `Pendiente` y una sesión de papel alumno | El código de la pieza pública solicita el desenlace forzando la petición al servicio de datos | La respuesta es el tipo de error de CU-08006 con código `OUTCOME_ADMIN_ONLY`: **0 desenlaces ejecutados por un alumno** |
| CA-05 | El tipo de solicitud de desenlace | Se inspecciona su superficie pública | Declara tres campos —identificador, desenlace pretendido y comentario—, el conjunto cerrado del desenlace tiene exactamente **2 valores**, y el comentario declara **0 campos de nota, de escala y de puntaje**: no es una calificación |
| CA-06 | Un trabajo en estado `Borrador` de un alumno cualquiera | El administrador solicita su desenlace con el identificador que consiguió por otra vía | La respuesta es el tipo de error de CU-08006 con código `WORK_NOT_FOUND`, con el mismo texto que produce un identificador inexistente |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-00009, y NB-00007 por el listado desde el que se ejerce |
| Reglas de negocio aplicables | Ninguna propia: este proyecto de código no las redacta. Aplican [`RN-02010`](../../Unidades-Entrega/GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02010-Desenlace-Exclusivo-Del-Administrador-Y-Terminalidad.md) —el desenlace es exclusivo del administrador y es terminal— sobre CA-03 y CA-04, y [`RN-02011`](../../Unidades-Entrega/GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02011-El-Administrador-No-Ve-Los-Borradores.md) sobre CA-06, las dos de `GeometriaFactory-Domain`. Ver `Especificacion-Funcional.md` §5 |
| Historias de usuario a generar en 06 | **Pronóstico de la pasada de diseño, superado y no acuñado.** Esta celda anunciaba las 2 historia(s) `US-08017`, `US-08018` «a generar en 06» cuando `GeometriaFactory-Contracts` era un proyecto de código con rango propio. **La consolidación de las unidades de entrega lo retiró y esas historias nunca se acuñaron con ese identificador**: las que cubren este contrato viven hoy en los dos [`Product-Backlog.md`](../../Unidades-Entrega/) con la numeración de su unidad. **La correspondencia una a una NO se reconstruye acá**: ningún registro de reconexión la conserva, y deducirla del texto sería inventarla. Queda como ítem diferido — ver la nota de abajo |
| Componentes esperados en 05 | Familia de tipos de transferencia de desenlace del ensamblado de contratos |
| Tests previstos en 08 | Pruebas de integración contra el servicio real de la aprobación sin comentario (CA-01), del rechazo con comentario (CA-02), del desenlace sobre estado terminal (CA-03), del desenlace forzado por un alumno (CA-04) y del desenlace sobre un trabajo en estado `Borrador` (CA-06); inspección de superficie pública para CA-05 |


> **Ítem diferido (`Root-Rules.md` §12.2) · la correspondencia de las historias pronosticadas.**
> **1 · Qué falta:** el mapeo de `US-08017`, `US-08018` a las historias vigentes que cubren este contrato.
> **2 · Por qué no se puede hoy:** **ningún registro de reconexión de la consolidación lo conserva**, y reconstruirlo comparando prosa es interpretación y no evidencia. El pronóstico se escribió antes de que existieran las historias reales.
> **3 · Quién lo cierra:** la categoría 06 de las dos unidades de entrega, que es la que las acuñó.
> **4 · En qué evento se cierra:** la **próxima emisión de la 06**, o la **Fase J**, lo que ocurra primero.

## 10. Notas y supuestos

- **Un solo caso de uso para aprobar y para rechazar, y el motivo.** Las dos decisiones comparten tipo de solicitud, tipo de resultado, precondición, conjunto de errores y regla de dominio: se distinguen sólo por el valor de un campo de conjunto cerrado. Dos casos de uso habrían duplicado la superficie sin declarar ninguna decisión de contrato distinta, que es el criterio de fusión que `Especificacion-Funcional.md` §3.1 ya aplicó al listado propio y al de la comisión.
- **La eliminación por el administrador no está acá.** Reutiliza el tipo de solicitud de eliminación que CU-08003 ya declara y por eso vive ahí, en FA-04, con su propio fundamento. Este caso de uso transporta una transición de estado; aquél, un retiro.
- El contrato transporta el comentario y **no lo interpreta**: no le impone longitud, no lo estructura y no lo asocia a ninguna pieza ni a ningún campo del texto del alumno.
- El contrato no declara ningún tipo para revertir un desenlace ni para editar un comentario, porque el modelo de estados de `PRODUCT-INTAKE` §4.2 no tiene ninguna transición de salida de `Finalizado` ni de `Rechazado`. Ausencia deliberada, no olvido.
- Que un rechazo pueda llegar sin comentario es consecuencia aceptada del carácter opcional del campo (`PRODUCT-INTAKE` §7, CL-11), y el contrato la sostiene sin agregar ninguna exigencia propia.
- La forma del punto de acceso de desenlace pertenece a `GeometriaFactory-Api`; quién puede ejercerlo se verifica en la pieza de datos. El contrato transporta el papel, no lo hace cumplir.

## 11. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.3 | 2026-08-29 | **Tramo `R-3c` del renombre `F-03`**, reactivado por el Product Owner el 2026-08-29 y registrado en [`../Norma-De-Nomenclatura.md`](../Norma-De-Nomenclatura.md) §8. **8 línea(s)** pasan los códigos de condición de la forma castellana a la vigente, con el mapeo de **§6.8** —101 pares— y **sin elegir ninguno acá**. Se respeta **§4.1**: no se tocan las filas de control de cambios, ni lo que está entre «…», ni los informes de `Audit/`. **Ninguna palabra de prosa cambia**, verificado con el control de diff del tramo. |
| 1.2 | 2026-08-29 | **Parche `P-02` de la mesa evaluadora del 2026-08-29** ([`../../Audit/Mesa-2026-08-29.md`](../../Audit/Mesa-2026-08-29.md), hallazgo `H-02`, evidencia **E2**, severidad **S2**). La fila «Historias de usuario a generar en 06» de §9 anunciaba historias del rango `08` **que nunca se acuñaron**: la consolidación de las unidades de entrega retiró ese rango y las historias que cubren este contrato se generaron con la numeración de su unidad. La celda pasa a declarar el hecho en lugar de seguir prometiendo artefactos inexistentes, y **la correspondencia una a una NO se reconstruye**: ningún registro de reconexión la conserva y deducirla del texto sería inventarla. Queda como **ítem diferido** con sus cuatro campos, con evento de cierre en la próxima emisión de la 06 o en la Fase J. **Ninguna otra sección cambia.** |
| 1.0 | 2026-08-09 | Emisión inicial, derivada de la incorporación del circuito de revisión del administrador en `PRODUCT-INTAKE` 1.3 §4 (F-21, F-23), §4.1 (RN-08010) y §4.2, y de `NB-00009` de 01. Declara la solicitud de desenlace con su conjunto cerrado de dos valores, el comentario opcional, el resultado con el estado terminal alcanzado y los dos códigos de error nuevos del ensamblado. |
| 1.1 | 2026-08-10 | **Cierra el hallazgo `C-08` (P2) del informe de auditoría `SDD/Docs/Audit/Coherencia-Corpus-r1.md` 1.0.** La cabecera de trazabilidad declaraba derivarse del `PRODUCT-INTAKE` **1.3**, versión archivada, y pasa a declarar la **1.14**, vigente. Entre la **1.3** y la **1.14** el intake atravesó once emisiones, entre ellas las que incorporaron **F-25**, **F-26** y las reglas **RN-08012** a **RN-08016**: una cabecera que declaraba 1.3 declaraba derivarse de un intake que no conocía ni el reseteo ni la habilitación con contraseña provisoria. Se revisó el cuerpo antes de mover la cabecera y **no arrastra ninguna decisión de las versiones intermedias**: no queda en él ningún recuento de «quince reglas» ni de «diecisiete códigos», ninguna cita a la exclusión **X-2** como vigente y ninguna afirmación de que la marca de cambio de contraseña pendiente la ponga únicamente el reseteo. **Ningún contenido normativo de este documento cambia: la corrección es de trazabilidad.** Sube minor. |

## 17. Compatibilidad de versión pública

Sección opcional de `Rules-Especificacion-Funcional.md` §4.3, que la numera **§17** y la reserva para `library`. Se conserva su número de la regla, aunque deje un hueco tras §11, para que un lector automatizado que busque §17 en cualquier caso de uso del producto encuentre siempre lo mismo.

- Agregar un valor al conjunto cerrado de desenlaces se trata como **cambio incompatible**, aunque compile: la pieza pública dejaría de cubrir todos los casos, y además cambiaría el modelo de estados, que es decisión de producto y no de contrato.
- Volver obligatorio el campo de comentario es incompatible y además contradice el criterio CA-01 y la decisión de `PRODUCT-INTAKE` §7 CL-11.
- Agregar al comentario cualquier campo de nota, escala o puntaje se rechaza aunque compile: convertiría en calificación lo que el intake declara que no lo es, y viola CA-05.
- Agregar un campo opcional al resultado de desenlace es compatible.
