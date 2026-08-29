# CU-00029 — Dar desenlace a la revisión

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** CU-00029-Dar-Desenlace-A-La-Revision.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-16
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-00009`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00009-Desenlace-Explicito-De-La-Entrega.md); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4.1 (RN-02010, RN-02011), §4.2 y su **consecuencia 3** (el comentario es opcional), §17.1.P.2 · GeometriaFactory-Domain (INV-07)
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico` y `08-Calidad-Y-Pruebas` de la unidad de entrega
**Consolida a:** [`CU-00008`](../../../../_legacy/2026-08-16-consolidacion-8.5/GeometriaFactory-Api/CU-00008-Exponer-El-Desenlace-De-La-Revision.md), [`CU-04008`](../../../../_legacy/2026-08-16-consolidacion-8.5/GeometriaFactory-Api/CU-04008-Dar-Desenlace-A-Un-Trabajo.md) y [`CU-02010`](../../../../_legacy/2026-08-16-consolidacion-8.5/GeometriaFactory-Api/CU-02010-Resolver-El-Desenlace-Del-Trabajo.md), por `Audit/Migracion-8.5-Consolidacion-Decidida.md` 1.2 §2.1

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

Que el administrador **apruebe o rechace** un trabajo que el alumno entregó, con un comentario escrito
opcional. Es lo que **convierte una entrega depositada en una entrega con respuesta**, y se ejerce por
**A-15**.

**Es el único punto de la superficie que produce una transición irreversible.** Los dos estados a los
que lleva —`Finalizado` y `Rechazado`— son **terminales**, y **ningún punto de esta superficie sale de
ellos**.

**Aprobar y rechazar son un solo punto de acceso, no dos.** Comparten el tipo de solicitud, el
resultado, la precondición, los errores y la regla que los gobierna, y se distinguen **sólo por el
valor de un campo de conjunto cerrado de dos valores**. Dos puntos habrían declarado la misma
superficie dos veces. Es el mismo criterio con el que el ensamblado de contratos los fusionó.

**El comentario es opcional en los dos desenlaces, y el intake acepta su consecuencia por escrito: un
alumno puede recibir un rechazo sin explicación escrita.** El estado le informa que no fue aceptado.

**La facultad es exclusiva del administrador y no se delega, ni siquiera sobre el trabajo propio.** Es
el **único** camino de toda la superficie que tiene un código de facultad propio en el conjunto
cerrado del contrato.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Administrador | Primario | Revisa el trabajo con la evidencia a la vista y le da desenlace. **Es el único que puede** |
| `GeometriaFactory-Web` | Intermediario | Arma la solicitud desde el panel de revisión, con el desenlace pretendido y el comentario cuando lo hay (RA-01) |
| Alumno | Sujeto de la regla | Recibe el desenlace, lo ve en su propio panel, y **no lo puede revertir por ningún punto de esta superficie** |
| Almacén de trabajos | Sistema | Recupera el trabajo y materializa el estado resultante y el comentario |
| Reloj del sistema | Sistema | Provee la fecha del desenlace |

## 3. Precondiciones

- La petición trae sesión firmada con papel `Administrador` y **atravesó la guardia** de `CU-00022`.
- La solicitud aporta el identificador del trabajo y **el desenlace pretendido**, que pertenece al
  conjunto cerrado de dos valores. El comentario **puede no venir**.
- El trabajo existe, **está dentro del alcance del administrador** —es decir, no está en `Borrador`—
  y **está en `Pendiente`**.

## 4. Flujo principal

1. El administrador abre el trabajo entregado, ve la escena, el árbol y las observaciones, y decide.
2. Llega una petición a **A-15** con el identificador, el desenlace pretendido y, opcionalmente, el
   comentario.
3. Se verifica que el papel de quien decide sea `Administrador` (RN-02010), **antes de recuperar el
   trabajo**.
4. Se recupera el trabajo y se comprueba que esté **dentro del alcance del administrador**, es decir
   que no esté en `Borrador` (RN-02011).
5. Se comprueba que el estado actual sea **`Pendiente`** y que el desenlace pertenezca al conjunto
   cerrado. Se toma el sello del desenlace del reloj.
6. Se fija el estado en `Finalizado` si el desenlace es aprobar, o en `Rechazado` si es rechazar, y se
   adopta el comentario si vino. **El trabajo queda terminal: a partir de acá no cambia de estado ni
   de contenido** (INV-07). El dueño, el texto original, las piezas y las observaciones **no
   cambiaron**.
7. Se materializa el resultado **en una única unidad de trabajo** y se responde `200` con **el estado
   terminal alcanzado** y el comentario **tal como quedó registrado**.
8. El alumno ve el desenlace y el comentario al abrir su trabajo, por `CU-00028`.

**El desenlace pretendido pertenece a un conjunto cerrado de dos valores.** Un valor fuera de ese
conjunto **no es un desenlace desconocido que haya que interpretar: es una petición mal formada**.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El desenlace pretendido es **el rechazo** | El flujo es idéntico y **sólo cambia el valor del campo**. El estado terminal alcanzado es el otro, y **es igual de terminal** | Paso 7 |
| FA-02 | El desenlace llega **sin comentario** | Procede igual, en los **dos** desenlaces. **El alumno ve el estado y sabe que no fue aceptado, aunque no tenga el motivo por escrito**, y el intake acepta esa consecuencia explícitamente | Paso 7 |
| FA-03 | El alumno quiere **corregir un trabajo rechazado** | **No hay camino en esta superficie.** El estado es terminal: lo que el alumno hace es **cargar un trabajo nuevo** por A-10, y el rechazado **queda como registro del intento** hasta que el administrador lo elimine por A-12 | Termina fuera de este caso de uso |
| FA-04 | El administrador quiere **corregir un desenlace ya aplicado** | **Tampoco hay camino.** No se corrige una aprobación ni se revisa un rechazo. Lo único que sí puede hacer es **eliminar el trabajo**, por A-12 (`CU-00027`) | Termina con el rechazo de §6 |

## 6. Excepciones y errores

| Motivo interno | Código del contrato | Respuesta | Causa |
| --- | --- | --- | --- |
| — | `REQUIRED_FIELD_MISSING` | `400` | Falta el identificador o el desenlace pretendido. **Nunca por el comentario**, que es opcional |
| `UNKNOWN_OUTCOME` | — | `400` | El desenlace pretendido **no pertenece al conjunto cerrado de dos valores**. **No lleva código del contrato porque la petición nunca llega a ser el tipo del contrato**: es el mismo tratamiento que el `401` de la guardia |
| `WORK_OUTSIDE_ADMINISTRATOR_SCOPE` | `WORK_NOT_FOUND` | `404` | El identificador no existe o **está fuera de lo que el administrador ve, incluido el trabajo en `Borrador`**. **Las respuestas son indistinguibles**: un borrador **no se aprueba ni se rechaza**, y él ni siquiera lo ve |
| `OUTCOME_OUTSIDE_SUBMITTED`, `TRANSITION_FROM_TERMINAL_STATUS` | `STATE_FORBIDS_OUTCOME` | `409` | El trabajo no está en `Pendiente`: o nunca lo estuvo, o **ya recibió su desenlace y está en un estado terminal**. La respuesta **declara el estado actual y no sugiere ninguna forma de revertirlo** |
| `ADMINISTRATOR_ROLE_REQUIRED` | `OUTCOME_ADMIN_ONLY` | `403` | Quien pide no es el administrador, **aun sobre un trabajo propio en `Pendiente`**. **No se recupera ni se modifica el trabajo.** Es el **único** código de facultad del conjunto cerrado, y **éste es el único punto donde se produce** |
| — | `UNCLASSIFIED_ERROR` | `503` | El almacén no está disponible |

**Ninguna condición deja escritura parcial**, y en particular **un desenlace rechazado por estado
terminal no altera el comentario existente**.

**Los dos motivos internos que comparten el `409` describen la misma situación desde afuera** —el
trabajo no está en `Pendiente`— y lo que el administrador necesita saber es **cuál es su estado
actual**, que la respuesta declara. Distinguirlos en la respuesta no le serviría para nada: en los dos
casos **no hay nada que pueda hacer**.

## 7. Postcondiciones

- **Éxito:** el trabajo quedó en uno de los dos estados terminales, con la fecha del desenlace y con
  su comentario si lo hubo, y **ninguna petición posterior a este punto lo puede mover**. El dueño, el
  texto original, las piezas y las observaciones **quedaron como estaban**.
- **Fallo:** el trabajo queda en el estado en que estaba, **con su comentario anterior intacto**.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un trabajo en `Pendiente` con las **2** advertencias del escenario **E-1**, y el reloj fijado en 2026-04-10 | El administrador lo **aprueba** por A-15 con el comentario «Revisá la fórmula del área del cubo» | Responde `200`, el estado resultante es `Finalizado`, con fecha 2026-04-10 y ese comentario registrado, y sus **2** advertencias **conservadas** |
| CA-02 | Otro trabajo en `Pendiente` | El administrador lo **rechaza** por A-15, **sin comentario** | Responde `200`, el estado resultante es `Rechazado` y el trabajo queda con **0** comentarios. **Es válido y no se exige ninguno** |
| CA-03 | Un trabajo ya `Rechazado`, con comentario | Se invoca A-15 sobre él con **cualquiera** de los dos desenlaces | Responde `409` **declarando el estado actual**, el cuerpo trae **0 campos** que sugieran una forma de revertirlo, y **el comentario anterior queda intacto** |
| CA-04 | Un trabajo en `Borrador` de un alumno | El **administrador** invoca A-15 sobre él | Responde `404`, **con el mismo cuerpo** que ante un identificador inexistente |
| CA-05 | Un trabajo **propio** en `Pendiente` y una sesión de papel `Alumno` | El alumno invoca A-15 | Responde `403` **con el código de facultad**, y el trabajo **sigue en `Pendiente`**: la facultad **no se delega ni sobre lo propio** |
| CA-06 | Una petición con un desenlace pretendido **fuera del conjunto cerrado** | Se invoca A-15 | Responde `400` y ocurren **0** transiciones |
| CA-07 | Un trabajo `Finalizado` y otro `Rechazado` | Se invoca A-15 sobre cada uno, y después se intenta cualquier otra transición sobre ellos | **0 transiciones** ocurren: los dos estados de cierre **son igual de terminales** |
| CA-08 | Un trabajo `Rechazado` con comentario | El alumno dueño lo abre por A-14 | Ve el estado `Rechazado` y **ese comentario**: el desenlace **llega a quien lo recibe** |
| CA-09 | Cualquier respuesta de §6, con el cuerpo y el registro del servidor observados | Se produce la condición | **0 apariciones** de la ruta del almacén y de la dirección de cualquier servicio interno |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | [NB-00009](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00009-Desenlace-Explicito-De-La-Entrega.md), que es la necesidad que este caso de uso cierra: la entrega deja de estar depositada y pasa a tener respuesta |
| Reglas de negocio aplicables | [RN-02010](../Reglas-De-Negocio/RN-02010-Desenlace-Exclusivo-Del-Administrador-Y-Terminalidad.md), en la facultad exclusiva y en la terminalidad. [RN-02011](../Reglas-De-Negocio/RN-02011-El-Administrador-No-Ve-Los-Borradores.md), en el alcance que hace que un borrador no se pueda aprobar |
| Invariantes del producto | **INV-07**, `Finalizado` y `Rechazado` son terminales: **no cambian de estado ni de contenido** |
| Reglas de arquitectura del producto | **RA-01**, el único invocante legítimo es el portal, servidor a servidor. **RA-03**, ninguna respuesta expone rutas ni direcciones |
| Puntos de acceso | **A-15**, **uno solo para los dos desenlaces** |
| Contrato de uso que transporta | `GeometriaFactory-Contracts` `CU-00007` |
| Puertos que consume | Almacén de trabajos, reloj del sistema |
| Historias de usuario a generar en 06 | US-00024, US-00025 |
| Componentes esperados en 05 | El punto de acceso; la orquestación del desenlace con su verificación de facultad y de alcance; la transición terminal |
| Tests previstos en 08 | Integración por los nueve criterios, **con los dos desenlaces sobre trabajos distintos**; la prueba de irreversibilidad de CA-07 sobre los **dos** estados de cierre; la prueba de facultad de CA-05 **forzando la petición** con un trabajo propio, no inspeccionando la pantalla; y la prueba de punta a punta de CA-08, que verifica que el desenlace **llega al alumno** |

## 10. Notas y supuestos

- **Es el único punto con código de facultad propio en el conjunto cerrado**, y es coherente con que
  sea el único cuyo efecto es irreversible: en los demás, un papel insuficiente responde `403` sin
  código, que es el punto abierto de `CU-00023` §10.
- **La consecuencia aceptada del comentario opcional está registrada aguas arriba**, en el intake §4.2
  consecuencia 3. No es un descuido de este documento: es una decisión del producto, y CA-02 la
  ejercita en lugar de suponerla.
- **El `404` sobre un borrador es coherente con el listado.** El administrador **no ve** borradores, de
  modo que decirle que ese trabajo no existe es exactamente lo que su alcance declara — el mismo
  criterio que `CU-00027` §10 y `CU-00028` §6.
- **La terminalidad no impide el retiro.** Un trabajo `Finalizado` no cambia de estado ni de
  contenido, pero el administrador **sí puede eliminarlo** por `CU-00027`. Son cosas distintas y
  conviene no confundirlas: **la terminalidad es sobre el contenido, no sobre la existencia**.
- **El desenlace no toca la evidencia.** Las advertencias del trabajo se conservan después de aprobar,
  y CA-01 lo verifica: **lo que el administrador aprueba queda tal como el alumno lo entregó**.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.1 | 2026-08-29 | **Tramo `R-3b` del renombre `F-03`**, reactivado por el Product Owner el 2026-08-29 y registrado en [`../../../../Producto/Norma-De-Nomenclatura.md`](../../../../Producto/Norma-De-Nomenclatura.md) §8. **6 línea(s)** de este documento pasan los códigos de condición de la forma castellana a la vigente, con el mapeo de **§6.8** —101 pares— y **sin elegir ninguno acá**. Se respeta **§4.1**: no se tocan las filas de control de cambios ni lo que está entre «…». **Ninguna palabra de prosa cambia**, verificado con el control de diff del tramo. |
| 1.0 | 2026-08-16 | Emisión inicial, como **caso de uso consolidado** de la unidad de entrega por `Audit/Migracion-8.5-Consolidacion-Decidida.md` 1.2 §2.1. Absorbe `CU-00008` 1.2, `CU-04008` 1.1 y `CU-02010` 1.1, que eran **tres vistas de la misma capacidad**. La unión no es la suma: el actor primario pasa a ser el administrador y el alumno queda como **destinatario del desenlace**, no como espectador; §6 queda en una sola tabla que **agrupa los dos motivos internos que comparten el `409`** y declara por qué distinguirlos no le serviría de nada a quien los recibe; y los criterios se rehacen sobre la capacidad y quedan **nueve**, con **CA-07** cubriendo la irreversibilidad de **los dos** estados de cierre en un solo criterio y **CA-08** nuevo, que verifica de punta a punta que el desenlace **llega al alumno** —afirmado por las tres vistas y sin criterio en ninguna—. Los tres documentos absorbidos quedan archivados en `_legacy/2026-08-16-consolidacion-8.5/` y citados desde la cabecera. |

## 17. Compatibilidad de la superficie pública

Partir A-15 en dos puntos —uno para aprobar y otro para rechazar— declara la misma superficie dos
veces y hace que una regla nueva tenga que escribirse dos veces, con el riesgo de que se escriba en
uno solo. Admitir cualquier transición desde `Finalizado` o `Rechazado` contradice INV-07 y CA-07, y
convierte el desenlace en algo revisable, que es exactamente lo que el producto decidió que no fuera.
Exigir el comentario contradice la consecuencia 3 del intake y CA-02. Abrirle el desenlace a un papel
que no sea `Administrador`, aun sobre el trabajo propio, contradice RN-02010 y CA-05. Permitir aprobar
un trabajo en `Borrador` contradice RN-02011 y CA-04. Alterar las observaciones o el texto al aplicar
el desenlace contradice CA-01: lo que se aprueba tiene que ser lo que el alumno entregó.
