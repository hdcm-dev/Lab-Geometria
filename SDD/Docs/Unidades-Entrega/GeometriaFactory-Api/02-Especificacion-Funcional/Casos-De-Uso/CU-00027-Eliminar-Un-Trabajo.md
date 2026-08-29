# CU-00027 — Eliminar un trabajo

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** CU-00027-Eliminar-Un-Trabajo.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-16
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-00003`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00003-Trabajo-Con-Dueno-Estado-Y-Persistencia.md); [`NB-00007`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00007-Revision-De-La-Comision-En-Un-Solo-Lugar.md); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4.1 (RN-02003, RN-02004, RN-02011), §4.2 (estados del trabajo), §17.1.P.2 · GeometriaFactory-Domain (INV-02, INV-03)
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico` y `08-Calidad-Y-Pruebas` de la unidad de entrega
**Consolida a:** `CU-00006` §A-12 y [`CU-04009`](../../../../_legacy/2026-08-16-consolidacion-8.5/GeometriaFactory-Api/CU-04009-Eliminar-Un-Trabajo.md), por `Audit/Migracion-8.5-Consolidacion-Decidida.md` 1.2 §2.1 y §2.1.2

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

Retirar un trabajo del laboratorio. Se ejerce por **un solo punto de acceso, A-12**, y tiene **dos
alcances con reglas opuestas**:

| Quién | Qué puede eliminar | Qué **no** puede |
| --- | --- | --- |
| **Alumno** | **Sólo lo propio**, y **sólo en `Borrador`** | Un trabajo suyo ya enviado, ni ningún trabajo ajeno |
| **Administrador** | **Cualquiera de los que ve**, en `Pendiente`, `Finalizado` y `Rechazado` | Un trabajo en `Borrador`, que **no forma parte de su flujo de trabajo** — ni para verlo ni para quitarlo |

**Mismo punto, mismo verbo, misma solicitud: lo que cambia es la regla que lo acota, y esa regla vive
adentro.** No hay dos endpoints ni un parámetro de modo. Los dos alcances son un solo caso de uso
porque responden **la misma pregunta** —si el retiro procede— con dos resoluciones que se eligen según
el papel de quien pide.

**Un trabajo `Rechazado` queda como registro del intento**, y por eso el alumno no puede quitarlo:
sólo el administrador. Corregir un rechazo significa **cargar un trabajo nuevo**, no borrar el
anterior.

**El retiro es físico y no deja marca.** El trabajo se va entero, con sus piezas y sus observaciones,
y no queda en ningún estado nuevo.

**Un alumno que pide eliminar un trabajo ajeno recibe exactamente lo mismo que ante un identificador
inexistente**, y el intake exige verificarlo **forzando la petición contra esta superficie**, no
comprobando que la pantalla oculte el botón.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Alumno | Primario | Elimina un borrador propio que ya no quiere |
| Administrador | Primario | Retira del laboratorio cualquier trabajo que ve |
| `GeometriaFactory-Web` | Intermediario | Arma la solicitud y la envía con la sesión firmada (RA-01) |
| Almacén de trabajos | Sistema | Recupera el trabajo y ejecuta el retiro efectivo |

**Los dos son actores primarios y ninguno es un caso especial del otro**: sus reglas no se contienen,
se excluyen.

## 3. Precondiciones

- La petición trae sesión firmada y **atravesó la guardia** de `CU-00022`.
- **El punto admite los dos papeles**, y la regla que acota cada alcance vive adentro. La guardia **no
  decide** quién puede eliminar qué.
- La solicitud aporta el identificador del trabajo, y **nada más**.

## 4. Flujo principal

1. El alumno decide descartar un borrador que ya no quiere y acciona eliminar.
2. Llega una petición a **A-12** con el identificador.
3. Se recupera el trabajo y se elige la resolución **según el papel de quien pide**. Es `Alumno`: se
   verifica **pertenencia** —el solicitante es el dueño— y **estado `Borrador`** (RN-02003, RN-02004,
   INV-02, INV-03).
4. Procede: se retira el trabajo **con sus piezas y sus observaciones**, en **una única unidad de
   trabajo**.
5. Se responde `204`, **sin cuerpo**. El trabajo dejó de existir.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | Quien pide tiene papel **`Administrador`** | Se elige la resolución de su alcance, que **admite la eliminación en los tres estados que él ve** y la niega en `Borrador` (RN-02004, RN-02011). Si procede, el retiro sigue **el mismo camino** del paso 4 | Paso 4 |
| FA-02 | El **alumno** pide eliminar un trabajo **propio** que no está en `Borrador` | No procede: la respuesta **declara el estado actual**, para que sepa por qué. **Un trabajo `Rechazado` queda como registro del intento**, y sólo el administrador puede quitarlo | Termina |
| FA-03 | El **alumno** pide eliminar un trabajo **de otro alumno**, cuyo identificador conoce | Se responde **exactamente igual** que ante un identificador inexistente: mismo código de respuesta, mismo código del contrato y **cuerpos idénticos** | Termina |
| FA-04 | El **administrador** pide eliminar un trabajo en **`Borrador`** | No procede: los borradores **no forman parte de su flujo de trabajo**, ni para verlos ni para quitarlos | Termina |

## 6. Excepciones y errores

| Motivo interno | Código del contrato | Respuesta | Causa |
| --- | --- | --- | --- |
| — | `REQUIRED_FIELD_MISSING` | `400` | La solicitud llega sin identificador de trabajo |
| `WORK_NOT_FOUND_FOR_REQUESTER` | `WORK_NOT_FOUND` | `404` | El identificador **no existe, o no es del solicitante, o está fuera de lo que ve**. **Las tres respuestas son indistinguibles**, y se traducen a «no encontrado» **y nunca a «no autorizado»** |
| `WORK_OUTSIDE_ADMINISTRATOR_SCOPE` | `WORK_NOT_FOUND` | `404` | El administrador pide eliminar un trabajo en `Borrador`. **Es una de las tres del renglón anterior**: para él ese trabajo **no existe** |
| `OPERATION_OUTSIDE_DRAFT` | `STATE_FORBIDS_DELETE` | `409` | **El alumno** pide eliminar un trabajo **suyo** que no está en `Borrador`. La respuesta **declara el estado actual**. Es un motivo distinto del `404` porque acá **la existencia ya está admitida para su dueño**. **Este código no se produce nunca en el camino del administrador**, porque a él no lo acota ningún estado |
| `UNRECOGNIZED_ROLE` | — | `500` | El papel declarado no pertenece al conjunto cerrado de dos valores. **Termina sin evaluar ninguna de las dos resoluciones.** Inalcanzable desde la superficie mientras el reclamo de papel de la sesión pertenezca al conjunto, y se conserva declarado porque **es el único camino por el que se eliminaría sin resolver alcance** |
| — | `UNCLASSIFIED_ERROR` | `503` | El almacén no está disponible |

**Ninguna condición deja el trabajo a medio retirar:** o se va entero, con sus piezas y sus
observaciones, o no se toca.

**Las dos negativas no son intercambiables, y la diferencia es de diseño.** El `404` dice «esto no
existe para vos» y **no revela nada**; el `409` dice «esto es tuyo y su estado no lo permite» y
**declara el estado**, porque el dueño ya sabe que su trabajo existe y lo que necesita es entender por
qué no puede quitarlo. Colapsarlas en una sola convertiría el punto en un oráculo o dejaría al alumno
sin explicación.

## 7. Postcondiciones

- **Éxito:** el trabajo **no existe**, y tampoco sus piezas ni sus observaciones. **No queda ninguna
  marca de borrado**: el retiro es físico y definitivo, y no deja el trabajo en ningún estado nuevo.
- **Fallo:** el trabajo queda **íntegro**, con su estado y su contenido, y el intento queda registrado
  del lado del servidor.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un trabajo en `Borrador` del alumno A, con **3** piezas y **2** observaciones | El alumno **A** lo elimina por A-12 | Responde `204` y quedan **0** trabajos, **0** piezas y **0** observaciones de ese trabajo |
| CA-02 | Un trabajo en `Pendiente` del alumno A | El **alumno A** lo elimina por A-12 | Responde `409` **declarando el estado actual**, y el trabajo **sigue existiendo**. Verificado **forzando la petición contra esta superficie**, no ocultando el control en una pantalla |
| CA-03 | El mismo trabajo en `Pendiente` | El **administrador** lo elimina por A-12 | Responde `204` y el trabajo deja de existir |
| CA-04 | Un trabajo en `Rechazado` del alumno A | El **alumno A** lo elimina por A-12 | Responde `409` y el trabajo sigue existiendo: **queda como registro del intento** |
| CA-05 | **3** trabajos del alumno A, uno en `Pendiente`, uno en `Finalizado` y uno en `Rechazado` | El **administrador** los elimina de a uno | Los **3 de 3** responden `204` y dejan de existir |
| CA-06 | Un trabajo en `Borrador` del alumno A | El **administrador** lo elimina por A-12 | Responde `404` y el trabajo sigue existiendo: **para él ese trabajo no existe** |
| CA-07 | Un trabajo del alumno **A** y un identificador **inexistente** | El alumno **B** pide eliminar cada uno | Las **2** respuestas son `404`, con el mismo código del contrato y cuerpos **idénticos**: **0 campos** permiten distinguirlos |
| CA-08 | El punto A-12 | Se inspecciona su superficie | Declara **1** identificador de trabajo y **0** parámetros de modo, de alcance o de papel: **el alcance se resuelve adentro** |
| CA-09 | El almacén interrumpido a mitad de una eliminación | Se invoca A-12 | Responde `503`, y el trabajo, sus piezas y sus observaciones siguen **enteros**: **0** retiros parciales |
| CA-10 | Cualquier respuesta de §6, con el cuerpo y el registro del servidor observados | Se produce la condición | **0 apariciones** de la ruta del almacén y de la dirección de cualquier servicio interno, y **0 apariciones** de la palabra «autorizado» o equivalente en el cuerpo del `404` |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | [NB-00003](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00003-Trabajo-Con-Dueno-Estado-Y-Persistencia.md), en la pertenencia que decide quién retira qué; [NB-00007](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00007-Revision-De-La-Comision-En-Un-Solo-Lugar.md), en el gobierno del administrador sobre lo que queda registrado |
| Reglas de negocio aplicables | [RN-02003](../Reglas-De-Negocio/RN-02003-Trabajo-Ajeno-Indistinguible-De-Inexistente.md), en la pertenencia y en la indistinguibilidad del ajeno. [RN-02004](../Reglas-De-Negocio/RN-02004-Eliminacion-Acotada-Al-Borrador.md), en el estado que acota al alumno. [RN-02011](../Reglas-De-Negocio/RN-02011-El-Administrador-No-Ve-Los-Borradores.md), en el estado que acota al administrador |
| Invariantes del producto | **INV-02**, todo trabajo tiene dueño y el ajeno es indistinguible del inexistente. **INV-03**, el alumno opera únicamente sobre sus borradores |
| Reglas de arquitectura del producto | **RA-01**, el único invocante legítimo es el portal, servidor a servidor. **RA-03**, ninguna respuesta expone rutas ni direcciones, y todo intento queda registrado |
| Puntos de acceso | **A-12**, **uno solo para los dos alcances** |
| Contrato de uso que transporta | `GeometriaFactory-Contracts` `CU-00005` |
| Puertos que consume | Almacén de trabajos |
| Historias de usuario a generar en 06 | US-00019, US-00020 |
| Componentes esperados en 05 | El punto de acceso; la elección de resolución por papel; el retiro con piezas y observaciones en una sola unidad de trabajo |
| Tests previstos en 08 | Integración por los diez criterios, **con las dos negativas ejercidas desde los dos papeles**; la prueba de indistinguibilidad de CA-07 **forzando la petición**, no inspeccionando la pantalla; inspección de superficie por CA-08; y el almacén interrumpido para CA-09 |

## 10. Notas y supuestos

- **Un solo punto para dos alcances es una decisión, no una economía.** Dos endpoints —uno «de
  alumno» y otro «de administrador»— habrían puesto la regla en la superficie, donde cualquiera puede
  probar el otro; con un solo punto **la regla vive adentro** y el papel de la sesión es lo único que
  la elige.
- **El `404` del administrador sobre un borrador es coherente con el listado.** Él **no ve**
  borradores (RN-02011), de modo que decirle que ese trabajo no existe **no es una mentira
  defensiva**: es exactamente lo que su alcance declara.
- **`UNRECOGNIZED_ROLE` se conserva declarado aunque la superficie lo vuelva inalcanzable**, porque
  es **el único camino por el que un retiro ocurriría sin resolver alcance**. Suponerlo imposible es
  como se termina borrando sin verificar.
- **La eliminación no arrastra nada fuera del trabajo.** Es la operación opuesta a la baja de cuenta
  de `CU-00023`, que sí arrastra: acá lo que se va es un trabajo y lo que cuelga de él.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.1 | 2026-08-29 | **Tramo `R-3b` del renombre `F-03`**, reactivado por el Product Owner el 2026-08-29 y registrado en [`../../../../Producto/Norma-De-Nomenclatura.md`](../../../../Producto/Norma-De-Nomenclatura.md) §8. **7 línea(s)** de este documento pasan los códigos de condición de la forma castellana a la vigente, con el mapeo de **§6.8** —101 pares— y **sin elegir ninguno acá**. Se respeta **§4.1**: no se tocan las filas de control de cambios ni lo que está entre «…». **Ninguna palabra de prosa cambia**, verificado con el control de diff del tramo. |
| 1.0 | 2026-08-16 | Emisión inicial, como **caso de uso consolidado** de la unidad de entrega por `Audit/Migracion-8.5-Consolidacion-Decidida.md` 1.2 §2.1. Absorbe el punto **A-12** de `CU-00006` 1.2 y `CU-04009` 1.1. **La versión 1.0 de la tabla de consolidación no le daba fila propia**: la llevaba como cola de «cargar, reeditar y eliminar un trabajo». El motivo del recorte está en §2.1.2 del documento de consolidación: la eliminación la ejercen **dos actores con reglas que se excluyen**, no interpreta ningún texto y no resuelve ningún estado. La unión no es la suma: los **dos** actores quedan declarados como primarios y **ninguno como caso especial del otro**; §6 declara en una sola tabla por qué `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR` **comparte respuesta** con el trabajo ajeno y por qué el `409` del dueño **no es intercambiable** con el `404`; y los criterios se rehacen sobre la capacidad y quedan **diez**, con **CA-08** verificando en la superficie que el alcance no es un parámetro. El documento absorbido entero queda archivado en `_legacy/2026-08-16-consolidacion-8.5/` y citado desde la cabecera. |

## 17. Compatibilidad de la superficie pública

Partir A-12 en dos puntos de acceso, o agregarle un parámetro de alcance, de modo o de papel, saca la
regla de adentro y contradice CA-08. Distinguir en la respuesta el trabajo ajeno del inexistente
contradice INV-02 y CA-07, y convierte el punto en un oráculo de qué identificadores existen.
Responder `403` donde corresponde `404` revela la existencia del trabajo ajeno. Colapsar el `409` del
dueño en el `404` deja al alumno sin saber por qué no puede quitar su propio trabajo. Abrirle al
alumno la eliminación fuera de `Borrador` contradice INV-03 y borra el registro del intento que un
`Rechazado` conserva. Abrirle al administrador los borradores contradice RN-02011. Dejar una marca de
borrado en lugar de retirar contradice la naturaleza física del retiro.
