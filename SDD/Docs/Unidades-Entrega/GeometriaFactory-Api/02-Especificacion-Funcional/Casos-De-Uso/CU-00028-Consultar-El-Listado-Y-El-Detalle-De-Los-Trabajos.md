# CU-00028 — Consultar el listado y el detalle de los trabajos

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** CU-00028-Consultar-El-Listado-Y-El-Detalle-De-Los-Trabajos.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-16
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-00007`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00007-Revision-De-La-Comision-En-Un-Solo-Lugar.md); [`NB-00003`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00003-Trabajo-Con-Dueno-Estado-Y-Persistencia.md) (la verificación ocurre del lado del servidor); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4.1 (RN-02003, RN-02004, RN-02009, RN-02011), §17.1.P.2 · GeometriaFactory-Domain (INV-02, INV-03), §17.1.P.10 · GeometriaFactory-Contracts (la proyección de listado)
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico` y `08-Calidad-Y-Pruebas` de la unidad de entrega
**Consolida a:** [`CU-00007`](../../../../_legacy/2026-08-16-consolidacion-8.5/GeometriaFactory-Api/CU-00007-Exponer-El-Listado-Y-El-Detalle-De-Los-Trabajos.md), [`CU-04006`](../../../../_legacy/2026-08-16-consolidacion-8.5/GeometriaFactory-Api/CU-04006-Consultar-Los-Trabajos-Propios-Del-Alumno.md), [`CU-04007`](../../../../_legacy/2026-08-16-consolidacion-8.5/GeometriaFactory-Api/CU-04007-Revisar-Los-Trabajos-De-La-Comision.md), [`CU-02009`](../../../../_legacy/2026-08-16-consolidacion-8.5/GeometriaFactory-Api/CU-02009-Resolver-El-Acceso-Del-Alumno-A-Un-Trabajo.md) y [`CU-02011`](../../../../_legacy/2026-08-16-consolidacion-8.5/GeometriaFactory-Api/CU-02011-Resolver-El-Alcance-Del-Administrador-Sobre-Un-Trabajo.md), por `Audit/Migracion-8.5-Consolidacion-Decidida.md` 1.2 §2.1

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

Que el alumno vea sus trabajos y que el administrador vea los de la comisión, y que cualquiera de los
dos abra uno y encuentre **todo lo que hace falta para dibujar la escena, armar el árbol y leer las
observaciones**.

| Punto de acceso | Qué ejerce |
| --- | --- |
| **A-13** | El listado |
| **A-14** | El detalle de un trabajo |

**Son los dos únicos puntos de esta superficie que no escriben.**

**Los dos admiten los dos papeles, y en los dos el alcance de lo que se devuelve se resuelve en el
servidor.** Los dos alcances son **complementarios y no anidados**:

| Quién | Qué ve |
| --- | --- |
| **Alumno** | **Sólo lo suyo**, en sus **cuatro** estados, incluido `Borrador` |
| **Administrador** | Los de **toda la comisión**, en los **tres** estados que no son `Borrador` |

**La superficie no ofrece ningún parámetro con el que pedir borradores ajenos**, y por eso no hay nada
que forzar: el alcance **no llega como una opción que el solicitante elige**.

**El detalle es el mismo para los dos papeles.** El administrador ve exactamente lo que ve el alumno,
que es lo que le permite revisar **lo que el alumno entregó** y no una versión distinta de lo mismo.

**Ver no es lo mismo que operar.** La acotación al estado `Borrador` restringe lo que el alumno
**hace** con su trabajo, no lo que **ve**: ve sus cuatro estados, con su desenlace y su comentario si
los hay.

**La proyección de listado no arrastra el texto original, ni los componentes de las piezas, ni el
comentario.** Es un requisito **estructural** declarado por el intake §17.1.P.10 · GeometriaFactory-Contracts, y su motivo es
directo: **el listado del administrador cargaría el texto completo de cada trabajo de la comisión**.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Alumno | Primario | Mira sus trabajos y abre el que quiere revisar, con su desenlace si lo tiene |
| Administrador | Primario | Mira los de la comisión, **agrupa y filtra por alumno**, y abre el que va a revisar |
| `GeometriaFactory-Web` | Intermediario | Pide listado y detalle con la sesión firmada y arma con ellos el panel de cada papel (RA-01) |
| Almacén de trabajos | Sistema | Resuelve la consulta **con el recorte ya aplicado** y recupera el detalle |
| Almacén de cuentas | Sistema | Aporta el alumno dueño de cada trabajo, para agrupar y filtrar |

**La verificación ocurre del lado del servidor y no ocultando un control en la pantalla** ([`NB-00003`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00003-Trabajo-Con-Dueno-Estado-Y-Persistencia.md)).

## 3. Precondiciones

- La petición trae sesión firmada y **atravesó la guardia** de `CU-00022`.
- **El punto admite los dos papeles**, y el recorte que cada uno determina se resuelve adentro.
- Para el detalle, la solicitud aporta el identificador del trabajo.

## 4. Flujo principal

1. El alumno abre su panel.
2. Llega una petición a **A-13**. El recorte que el papel determina es **por dueño**: se piden al
   almacén los trabajos **cuyo dueño es ese alumno**, y **no se filtra después sobre un conjunto
   mayor**.
3. Se responde `200` con la colección: identificador, nombre, fecha, estado, dueño y recuento de
   observaciones de cada trabajo, **sin texto original, sin componentes de las piezas y sin
   comentario**.
4. El alumno abre uno y llega una petición a **A-14** con su identificador.
5. Se recupera el trabajo y se resuelve el acceso: se compara la identidad del solicitante con la del
   dueño (INV-02). Coinciden, y **ver procede en cualquiera de los cuatro estados**.
6. Se responde `200` con el detalle: los datos del trabajo, **el texto original**, las piezas con sus
   componentes, las observaciones **con su especie y su par de valores**, y **el comentario del
   administrador cuando lo hay, en su propio bloque**.

**La ubicación de cada observación cruza la frontera sin recortarse**: el índice de figura y el campo
llegan al otro lado **tal como se produjeron**.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El listado se pide con papel **`Administrador`** | El recorte **excluye los borradores** (RN-02011) y la colección trae los de toda la comisión **con el dato de dueño**, que es lo que después permite agrupar y filtrar en la pantalla | Paso 3 |
| FA-02 | El administrador pide el listado **filtrado por un alumno** | El filtro se traslada al almacén, que lo resuelve **junto con** el recorte de alcance. **El recorte de los borradores sigue rigiendo dentro del filtro** | Paso 3 |
| FA-03 | El listado **no tiene ningún elemento** que devolver | Se responde `200` con una colección **vacía**. **Un listado vacío no es un fallo**: el portal lo distingue por el tipo recibido y no por el conteo, y **una comisión sin entregas todavía es un caso normal** | Termina |
| FA-04 | El **administrador** pide el detalle de un trabajo en **`Borrador`** | Responde **exactamente igual** que ante un identificador inexistente: **el trabajo que no ve le resulta indistinguible del que no existe** | Termina |
| FA-05 | El detalle pedido corresponde a un trabajo **ya resuelto, con comentario** | El detalle lo trae **en su bloque propio**. **El comentario nunca viaja como una observación más**: no comparten ni un campo | Paso 6 |
| FA-06 | El **alumno** pide el detalle de un trabajo suyo en `Finalizado` o `Rechazado` | **Procede**: ve el desenlace y el comentario de su propio trabajo. Lo que la acotación por estado restringe es **operar** sobre él, no verlo | Paso 6 |

## 6. Excepciones y errores

| Motivo interno | Código del contrato | Respuesta | Punto | Causa |
| --- | --- | --- | --- | --- |
| `WORK_NOT_FOUND_FOR_REQUESTER` | `WORK_NOT_FOUND` | `404` | A-14 | El identificador **no existe, o no es del solicitante, o está fuera de lo que ve**. **Las tres respuestas son indistinguibles**, y se traducen a «no encontrado» **y nunca a «no autorizado»**: confirmar que el recurso existe pero es ajeno **ya sería informar de más** |
| `WORK_OUTSIDE_ADMINISTRATOR_SCOPE` | `WORK_NOT_FOUND` | `404` | A-14 | El administrador pide el detalle de un trabajo en `Borrador`. **Es una de las tres del renglón anterior** |
| `ACCOUNT_NOT_FOUND` | `STUDENT_NOT_FOUND` | `404` | A-13 | El filtro por alumno referencia un identificador que no existe. **Recuperación: reintentar sin filtro** |
| `ADMINISTRATOR_ROLE_REQUIRED` | `OPERATION_ADMIN_ONLY` | `403` | A-13 | Se pide el listado de la comisión con papel `Alumno`. **No se consulta el almacén.** Es una negativa **por facultad**, distinta de la negativa **por pertenencia** del renglón primero, y **no tiene nada que ocultar**: el recurso no es ajeno, lo que no alcanza es el papel |
| `UNKNOWN_OPERATION` | — | `500` | A-13, A-14 | La operación consultada no pertenece al conjunto declarado. **Inalcanzable desde la superficie por construcción**: los dos puntos declaran su operación |
| — | `UNCLASSIFIED_ERROR` | `503` | A-13, A-14 | El almacén no está disponible |

**El listado vacío no está en esta tabla, y es deliberado**: el ensamblado de contratos lo declara
**señal y no error**, y acá viaja en una **respuesta exitosa**.

**Ninguna condición modifica nada.** Los dos puntos son consultas.

**Por qué el mismo `404` cubre tres causas distintas y el `403` no las acompaña.** Las tres del `404`
comparten una propiedad: **revelar cuál de ellas ocurrió le diría al solicitante algo sobre un trabajo
que no le corresponde**. El `403` del listado no la comparte: quien pide **no está pidiendo un recurso
ajeno** sino ejerciendo una facultad que no tiene, y ahí no hay nada que ocultar.

## 7. Postcondiciones

- **Listado con éxito:** el portal tiene la colección con el recorte **ya aplicado**, y **sin ningún
  dato que permita inferir la existencia de trabajos fuera de ese recorte**.
- **Detalle con éxito:** el portal tiene todo lo necesario para **dibujar la escena, armar el árbol y
  mostrar las observaciones y el comentario**.
- **En los dos: nada cambió.** Son los dos únicos puntos de esta superficie que no escriben.
- **Fallo:** un código de respuesta, y **ninguna información sobre lo que el solicitante no ve**.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un alumno **A** con **4** trabajos, uno en cada estado, y un alumno **B** con 2 | El alumno A invoca A-13 | Responde `200` con **4** elementos, los suyos, **0** del alumno B, y **los 4 estados quedan distinguibles** |
| CA-02 | La misma situación | El **administrador** invoca A-13 | La colección trae los del alumno A y del B que **no** estén en `Borrador`, y **0 borradores** |
| CA-03 | La misma situación | El administrador invoca A-13 **filtrado por el alumno A** | Devuelve los del alumno A que no están en `Borrador`, y **0 borradores**: el recorte **sigue rigiendo dentro del filtro** |
| CA-04 | El punto A-13 | Se inspecciona su superficie de parámetros | **0 parámetros** permiten pedir trabajos en `Borrador` ajenos, y **0** permiten ampliar el recorte que el papel determina |
| CA-05 | Un elemento cualquiera del listado | Se inspecciona | Trae identificador, nombre, fecha, estado, dueño y recuento de observaciones, y **0 campos** de texto original, de componentes de pieza y de comentario |
| CA-06 | Una sesión con papel `Alumno` | Se invoca A-13 pidiendo el listado de la comisión | Responde `403` y el almacén registra **0** consultas |
| CA-07 | Un trabajo enviado con el texto del escenario **E-1** | Se invoca A-14 sobre él | El detalle trae **3** piezas con sus componentes, el texto original **idéntico al enviado** y **2** observaciones de especie advertencia, cada una **con su valor declarado y su valor derivado** |
| CA-08 | Un trabajo enviado con el texto del escenario **E-5** | Se invoca A-14 sobre él | La observación de error de validación llega con **índice de figura 1** y campo `Tipo`: **la ubicación no se recortó al cruzar** |
| CA-09 | Un trabajo del alumno A y un identificador inexistente | El alumno **B** invoca A-14 sobre cada uno | Las **2** respuestas son `404` con cuerpos **idénticos**: **0 campos** permiten distinguirlos |
| CA-10 | Un trabajo en `Borrador` del alumno A | El **administrador** invoca A-14 sobre él | Responde `404`, **con el mismo cuerpo** que ante un identificador inexistente |
| CA-11 | Un trabajo del alumno A en `Rechazado` con el comentario «Revisá el área del cubo» | El **alumno A** invoca A-14 sobre él | Responde `200` con el estado `Rechazado` y **ese comentario**, en **su propio bloque**, y **0** elementos de la colección de observaciones lo contienen |
| CA-12 | Un trabajo en `Pendiente` del alumno A con 3 piezas y 2 advertencias | El **administrador** y el **alumno A** invocan A-14 sobre él | Los **2** detalles son **idénticos**: el administrador ve **exactamente lo que ve el alumno** |
| CA-13 | Una comisión **sin ningún trabajo** | El administrador invoca A-13 | Responde `200` con **0** elementos, **y no un código de fallo** |
| CA-14 | Cualquier respuesta de §6, con el cuerpo y el registro del servidor observados | Se produce la condición | **0 apariciones** de la ruta del almacén, de la dirección de cualquier servicio interno, y **0 apariciones** de la palabra «autorizado» o equivalente en el cuerpo del `404` |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | [NB-00007](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00007-Revision-De-La-Comision-En-Un-Solo-Lugar.md), en que el administrador revise la comisión **en un solo lugar**; [NB-00003](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00003-Trabajo-Con-Dueno-Estado-Y-Persistencia.md), en que el trabajo tiene dueño y la verificación ocurre del lado del servidor |
| Reglas de negocio aplicables | [RN-02003](../Reglas-De-Negocio/RN-02003-Trabajo-Ajeno-Indistinguible-De-Inexistente.md), en el recorte por dueño y en la indistinguibilidad del ajeno. [RN-02004](../Reglas-De-Negocio/RN-02004-Eliminacion-Acotada-Al-Borrador.md), **por contraste**: acota lo que el alumno **opera**, no lo que ve. [RN-02011](../Reglas-De-Negocio/RN-02011-El-Administrador-No-Ve-Los-Borradores.md), en el recorte del administrador. [RN-02009](../Reglas-De-Negocio/RN-02009-Observacion-De-Error-Con-Posicion-Y-Campo.md), en la ubicación que el detalle **no recorta** |
| Invariantes del producto | **INV-02**, todo trabajo tiene dueño y el ajeno es indistinguible del inexistente. **INV-03**, el alumno **opera** únicamente sobre sus borradores — y **ve** los cuatro estados |
| Reglas de arquitectura del producto | **RA-01**, el único invocante legítimo es el portal, servidor a servidor. **RA-03**, ninguna respuesta expone rutas ni direcciones |
| Puntos de acceso | **A-13** el listado, **A-14** el detalle |
| Contrato de uso que transporta | `GeometriaFactory-Contracts` `CU-00006` |
| Puertos que consume | Almacén de trabajos, almacén de cuentas |
| Historias de usuario a generar en 06 | US-00021, US-00022, US-00023 |
| Componentes esperados en 05 | Los dos puntos de acceso; la resolución de alcance por papel; **la proyección de listado, que es un componente propio y no una serialización recortada del detalle** |
| Tests previstos en 08 | Integración por los catorce criterios, **con los dos papeles sobre el mismo repositorio**; la prueba de indistinguibilidad de CA-09 y CA-10 **forzando la petición**, no inspeccionando la pantalla; inspección de superficie por CA-04 y de proyección por CA-05; y la comparación byte a byte de los dos detalles en CA-12 |

## 10. Notas y supuestos

- **El punto abierto del papel insuficiente está cerrado.**
  `OPERATION_ADMIN_ONLY` entró al conjunto cerrado el **2026-08-12**
  (`PRODUCT-INTAKE` 1.29) y **nombra el listado de la comisión** entre sus tres caminos. `CU-00007`
  1.2 seguía respondiendo con el genérico: el alcance de esa propagación incompleta está en
  `CU-00023` §10.
- **La proyección de listado es estructural y no una omisión de campos al serializar.** Si se
  implementara recortando el detalle, la consulta traería igual el texto completo de cada trabajo de
  la comisión y el requisito quedaría incumplido **sin que nada falle**. Por eso §9 la declara
  componente propio.
- **Los dos alcances son complementarios, no anidados.** El administrador **no** es un alumno con más
  permisos: hay algo que el alumno ve y él no —los borradores—. Modelarlo como una jerarquía de
  permisos rompería RN-02011.
- **El comentario del administrador nunca viaja como una observación más.** Las observaciones las
  produce el motor de interpretación; el comentario lo escribe una persona. **No comparten ni un
  campo**, y CA-11 lo verifica.
- **Ver un trabajo propio procede en los cuatro estados, y es lo que hace útil el desenlace.** Un
  alumno que no pudiera abrir su trabajo rechazado no podría leer el comentario que explica por qué.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.1 | 2026-08-29 | **Tramo `R-3b` del renombre `F-03`**, reactivado por el Product Owner el 2026-08-29 y registrado en [`../../../../Producto/Norma-De-Nomenclatura.md`](../../../../Producto/Norma-De-Nomenclatura.md) §8. **7 línea(s)** de este documento pasan los códigos de condición de la forma castellana a la vigente, con el mapeo de **§6.8** —101 pares— y **sin elegir ninguno acá**. Se respeta **§4.1**: no se tocan las filas de control de cambios ni lo que está entre «…». **Ninguna palabra de prosa cambia**, verificado con el control de diff del tramo. |
| 1.0 | 2026-08-16 | Emisión inicial, como **caso de uso consolidado** de la unidad de entrega por `Audit/Migracion-8.5-Consolidacion-Decidida.md` 1.2 §2.1. Absorbe `CU-00007` 1.2, `CU-04006` 1.1, `CU-04007` 1.1, `CU-02009` 1.1 y `CU-02011` 1.1, que eran **cinco vistas de la misma capacidad**: el origen tenía **una vista por papel** en la capa de aplicación y **una resolución de alcance por papel** en el dominio, y acá los dos papeles quedan en un solo documento con sus alcances declarados como **complementarios y no anidados**. La unión no es la suma: los **dos** actores quedan como primarios; §6 queda en una sola tabla y declara **por qué tres causas comparten el `404` y el `403` del listado no las acompaña**, que las cinco vistas afirmaban por separado sin poder compararlas; y los criterios se rehacen sobre la capacidad y quedan **catorce**, con **CA-12** nuevo, que compara los dos detalles y verifica la afirmación —presente en las tres vistas y sin criterio en ninguna— de que el administrador ve exactamente lo que ve el alumno. Los cinco documentos absorbidos quedan archivados en `_legacy/2026-08-16-consolidacion-8.5/` y citados desde la cabecera. |

## 17. Compatibilidad de la superficie pública

Agregar a A-13 cualquier parámetro que amplíe el recorte —de estado, de dueño, de alcance— saca la
regla de adentro y contradice CA-04. Agregar a la proyección de listado el texto original, los
componentes o el comentario contradice el requisito estructural del intake §17.1.P.10 · GeometriaFactory-Contracts y CA-05.
Distinguir en la respuesta el trabajo ajeno, el fuera de alcance y el inexistente contradice INV-02 y
CA-09. Responder `403` en A-14 donde corresponde `404` revela la existencia del trabajo ajeno. Recortar
la ubicación de una observación al cruzar la frontera contradice RN-02009 y CA-08, y deja al alumno
con un mensaje genérico, que es lo que el producto viene a eliminar. Devolver el comentario dentro de
la colección de observaciones contradice CA-11. Devolverle al administrador un detalle distinto del
que ve el alumno contradice CA-12 y le hace revisar algo que no es lo que el alumno entregó.
