# CU-03 — Contrato de carga y edición del trabajo

**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** CU-03-Contrato-De-Carga-Y-Edicion-Del-Trabajo.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `01-Necesidades-Negocio/Necesidades-De-Negocio/NB-03-Trabajo-Con-Dueno-Estado-Y-Persistencia.md` §1, §5; `NB-04-Interpretacion-Fiel-Del-Dato-Del-Alumno.md` §1, §5; `00-Contexto/Vision-Producto.md` §9.1 (Trabajo, Pieza); `00-Contexto/Alcance-Producto.md` §4.1 (F-06, F-07) y §5 (X-4); `PRODUCT-INTAKE` §17.4 P.2, P.3, P.5 y P.11 (decisión 2), §17.5 P.3, §4 (F-06, F-07), §6 (flujo 2), §7 (CL-3), §20.E-2
**Trazabilidad downstream:** `05-Arquitectura-Tecnica` y `06-Backlog-Tecnico` de este proyecto de código; `08-Calidad-Y-Pruebas`

---

## Tabla de contenido

- [1. Propósito](#1-propósito)
- [2. Actores](#2-actores)
- [3. Precondiciones](#3-precondiciones)
- [4. Flujo principal](#4-flujo-principal)
- [5. Flujos alternativos](#5-flujos-alternativos)
- [6. Excepciones y errores](#6-excepciones-y-errores)
  - [6.1 Señales declaradas que no son error](#61-señales-declaradas-que-no-son-error)
- [7. Postcondiciones](#7-postcondiciones)
- [8. Criterios de aceptación](#8-criterios-de-aceptación)
- [9. Trazabilidad](#9-trazabilidad)
- [10. Notas y supuestos](#10-notas-y-supuestos)
- [11. Control de cambios](#11-control-de-cambios)
- [17. Compatibilidad de versión pública](#17-compatibilidad-de-versión-pública)

---

## 1. Propósito

Declarar los tipos de transferencia con los que un trabajo del alumno se crea, se reedita, se envía y se elimina a través de la frontera entre las dos piezas desplegables. La decisión central de este caso de uso es que **el texto original del trabajo viaja como una sola cadena, sin interpretarse en el contrato**: el contrato no declara piezas, ni componentes, ni valores derivados en la solicitud. La interpretación es responsabilidad de la pieza de datos.

Dos rasgos del modelo vigente ordenan el resto. **Enviar es la única acción de guardado**: no existe una solicitud de «guardar sin enviar», y el estado resultante lo decide la interpretación del texto, no el consumidor del contrato. Y **la solicitud de eliminación es un tipo único que usan los dos papeles con reglas opuestas**: al alumno lo acota la pertenencia y el estado `Borrador`; al administrador lo acota únicamente lo que ve.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Código de la pieza pública compilado contra el contrato | Primario | Arma la solicitud de alta, de envío o de eliminación con lo que la persona cargó, y consume el resultado |
| Código de la pieza de datos compilado contra el contrato | Sistema | Recibe la cadena tal cual y produce el resultado sobre los mismos tipos |
| Ensamblado de contratos | Sistema | Declara el campo de texto original como cadena y el conjunto cerrado de estados del trabajo |

## 3. Precondiciones

- Los dos extremos están compilados contra la misma versión del ensamblado de contratos.
- El código de la pieza pública tiene una credencial de sesión obtenida por CU-01.
- El contrato declara el conjunto cerrado de cuatro estados del trabajo: `Borrador`, `Pendiente`, `Finalizado` y `Rechazado`. Los dos últimos son terminales.

## 4. Flujo principal

1. El código de la pieza pública arma la solicitud de envío de trabajo con cuatro campos: nombre, fecha, descripción y texto original. Es la única solicitud de guardado que el contrato declara para el alumno.
2. El código de la pieza pública asigna al campo de texto original **la cadena exacta** que la persona pegó, sin normalizarla, sin reordenarla y sin quitarle caracteres.
3. El código de la pieza pública envía la solicitud a la pieza de datos.
4. El código de la pieza de datos interpreta el texto y responde con el resultado de envío, que trae el identificador propio del trabajo, **el estado resultante que la interpretación decidió** —`Pendiente` si el texto verificó, `Borrador` si no—, su fecha de registro y las observaciones que la interpretación produjo.
5. El código de la pieza pública arma la solicitud de envío con el identificador ya asignado y los mismos cuatro campos, para volver sobre un trabajo que quedó en estado `Borrador`.
6. El código de la pieza de datos responde con el mismo tipo de resultado, con el estado que la interpretación decide esta vez.

## 5. Flujos alternativos

| Id | Disparador | Curso | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El texto original está incompleto o no verifica | El contrato admite la solicitud sin cambio alguno: el campo de texto original es una cadena y el contrato no le impone forma. **El envío no falla**: el resultado devuelve estado `Borrador` y las observaciones de error de validación con su índice de figura y su campo | El flujo vuelve al paso 5 cuando la persona corrige y vuelve a enviar |
| FA-02 | El alumno elimina un trabajo suyo que está en estado `Borrador` | El contrato usa la solicitud de eliminación, con un único campo: el identificador del trabajo | El flujo termina; el trabajo deja de aparecer en el listado de CU-04 |
| FA-03 | El texto verifica y el trabajo pasa a estado `Pendiente` | El contrato es el mismo del paso 1: no hay una solicitud aparte de finalización, porque el envío es la única acción de guardado y el estado lo decide la interpretación. El trabajo deja de ser editable y de ser eliminable por el alumno | El flujo termina para el alumno y sigue en CU-07, donde el administrador lo resuelve |
| FA-04 | El **administrador** elimina un trabajo de los que ve, en cualquiera de sus tres estados | El contrato usa **la misma** solicitud de eliminación de FA-02, con el mismo campo único. Lo que cambia no es el tipo sino la regla que lo acota, y esa regla vive en el dominio: al administrador no lo limita el estado sino lo que ve | El flujo termina; el trabajo desaparece también del listado del alumno dueño |

## 6. Excepciones y errores

| Código | Causa | Respuesta del contrato |
| --- | --- | --- |
| `CONTRATO_CAMPO_REQUERIDO_AUSENTE` | La solicitud llega sin nombre, sin fecha o sin texto original | Respuesta de error de CU-06 que nombra el campo ausente. Recuperación por corrección y reintento |
| `CONTRATO_TRABAJO_NO_ENCONTRADO` | El identificador no corresponde a un trabajo del solicitante, o no existe | Respuesta de error de CU-06 con texto neutro que **no distingue** el caso de trabajo ajeno del de trabajo inexistente. Terminación controlada |
| `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` | **El alumno** pide eliminar un trabajo suyo que no está en estado `Borrador`. Enunciado revisado: el código expresa que el estado del trabajo no habilita al solicitante a eliminarlo, y sólo se produce en el camino del alumno | Respuesta de error de CU-06 que declara el estado actual del trabajo. Terminación controlada |
| `CONTRATO_SERVICIO_NO_DISPONIBLE` | La pieza de datos no responde | Respuesta de error de CU-06 con texto neutro y sin dirección del servicio que falló. Handoff al estado degradado |

**Por qué los dos caminos de eliminación comparten los códigos y no hacen falta más.** Al alumno lo acotan dos cosas: la pertenencia, que resuelve `CONTRATO_TRABAJO_NO_ENCONTRADO` sin revelar la existencia del trabajo ajeno, y el estado, que resuelve `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR`. Al administrador **no lo acota ningún estado**, de modo que ese segundo código no se produce nunca en su camino; lo único que lo acota es la visibilidad, y un trabajo en estado `Borrador` le resulta indistinguible de uno inexistente, que es exactamente lo que `CONTRATO_TRABAJO_NO_ENCONTRADO` ya expresa. Un código nuevo por papel no agregaría ninguna distinción verificable y sí agregaría superficie donde el contrato puede filtrar información sobre recursos que el solicitante no debería saber que existen.

### 6.1 Señales declaradas que no son error

Se separa de la tabla anterior porque no produce respuesta de error y no forma parte del conjunto cerrado de códigos de error de CU-06.

| Código | Causa | Respuesta del contrato |
| --- | --- | --- |
| `CONTRATO_TEXTO_NO_INTERPRETABLE` | El texto original enviado no verifica | El envío **procede**: el resultado trae estado `Borrador`, el texto conservado íntegro y las observaciones de error de validación con índice de figura y campo. Recuperación: la persona corrige y vuelve a enviar. Dejó de ser código de error en esta versión, porque con el envío como acción única ya no existe una operación que falle por este motivo |

## 7. Postcondiciones

- En caso de éxito: el código de la pieza pública tiene el identificador y el estado que la interpretación decidió, y el texto original que envió es idéntico carácter por carácter al que la persona pegó.
- En caso de éxito con texto que no verifica: el trabajo queda en estado `Borrador` con sus observaciones, que **no es un fallo** del envío.
- En caso de fallo: el código de la pieza pública tiene un tipo de error de CU-06; el contrato no transporta ninguna versión reescrita del texto original.
- En ningún caso: el contrato ofrece un camino para que el alumno lleve un trabajo a estado `Finalizado` o `Rechazado`, que son desenlaces de CU-07.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | El tipo de solicitud de envío de trabajo | Se inspecciona su superficie pública | El texto original está declarado como **una sola cadena**, y el tipo declara 0 campos de pieza, de componente, de valor derivado, de observación y de estado pretendido: el estado lo decide la interpretación, no el consumidor |
| CA-02 | El texto del escenario E-2 del intake —un ortoedro con dos comas finales y la clave `Tapas`— | El código de la pieza pública arma la solicitud de envío con ese texto | El campo de texto original transporta las dos comas finales y la clave `Tapas` sin modificación: 0 caracteres alterados respecto del original |
| CA-03 | Un trabajo propio en estado `Pendiente` y una sesión de papel alumno | El código de la pieza pública arma la solicitud de eliminación con su identificador | La respuesta es el tipo de error de CU-06 con código `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` y declara el estado actual |
| CA-04 | El trabajo de otra persona, cuyo identificador se conoce | El código de la pieza pública pide su envío con el identificador ajeno | La respuesta es el tipo de error de CU-06 con código `CONTRATO_TRABAJO_NO_ENCONTRADO`, con el mismo texto que produce un identificador inexistente |
| CA-05 | Un trabajo cuyo texto original es el escenario E-5 del intake, con una figura de tipo desconocido en la posición 1 | El código de la pieza pública lo envía | **El envío no produce error**: el resultado trae estado `Borrador` y una observación de error de validación con índice de figura 1 y campo `Tipo`; el trabajo **no** pasa a estado `Pendiente` |
| CA-06 | El texto del escenario E-4 del intake, un cubo cuyos valores declarados coinciden con los derivados | El código de la pieza pública lo envía | El resultado trae estado `Pendiente` y 0 observaciones: un texto que verifica **no puede** quedar en estado `Borrador` |
| CA-07 | Un trabajo en estado `Pendiente` de un alumno cualquiera y una sesión de papel administrador | El código de la pieza pública arma la solicitud de eliminación con su identificador | La eliminación procede y el resultado la confirma: al administrador no lo acota el estado. Es la misma solicitud de CA-03, que para el alumno falla |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-03, y NB-04 por el límite entre el texto que verifica y el que no |
| Reglas de negocio aplicables | Ninguna propia: este proyecto de código no las redacta. Aplican, todas de `GeometriaFactory-Domain`, [`RN-08`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-08-Texto-Original-Conservado-Integro.md) sobre CA-02, [`RN-04`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-04-Eliminacion-Acotada-Al-Borrador.md) sobre CA-03 y CA-07 —su enunciado vigente cubre los dos caminos de eliminación, el del alumno y el del administrador, aunque el slug del archivo nombre sólo el primero—, [`RN-03`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-03-Trabajo-Ajeno-Indistinguible-De-Inexistente.md) sobre CA-04, y [`RN-05`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-05-Finalizacion-Sin-Errores-De-Validacion.md) sobre CA-05 y CA-06 —su corte vigente es el envío, no el cierre, aunque el slug del archivo diga «finalización»—. Ver `Especificacion-Funcional.md` §5 |
| Historias de usuario a generar en 06 | US-06 tipo de solicitud de envío y su resultado con el estado que la interpretación decide; US-07 tipo de solicitud de eliminación, único para los dos papeles; US-19 conjunto cerrado de cuatro estados del trabajo con sus dos terminales |
| Componentes esperados en 05 | Familia de tipos de transferencia de trabajo del ensamblado de contratos |
| Tests previstos en 08 | Pruebas de integración del envío con el texto de E-2 sin alteración (CA-02), del envío que no verifica con E-5 y queda en estado `Borrador` (CA-05), del envío que verifica con E-4 y pasa a estado `Pendiente` (CA-06), de la eliminación del alumno fuera de estado `Borrador` forzando la petición al servicio (CA-03), del envío sobre un trabajo ajeno (CA-04) y de la eliminación por el administrador sobre un trabajo en estado `Pendiente` (CA-07); inspección de superficie pública para CA-01 |

## 10. Notas y supuestos

- El contrato no interpreta el texto: no tolera ni rechaza comas finales, ni resuelve claves. Toda la tolerancia pertenece a `GeometriaFactory-Infrastructure` y a `GeometriaFactory-Domain`.
- El contrato tampoco declara ninguna forma de corregir el texto del alumno: la edición del texto desde el producto está excluida (X-4 de `Alcance-Producto.md` §5). Lo que la solicitud de edición reemplaza es el texto entero que la persona vuelve a pegar.
- Supuesto de alcance: el contrato no declara carga de archivos; el texto llega como cadena dentro de la misma solicitud.

## 11. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Declara los tipos de alta, edición, eliminación y finalización del trabajo, con el texto original como cadena no interpretada en el contrato. |
| 1.0 | 2026-08-08 | Correcciones absorbidas de la ronda 1 de auditoría (`Audit/B-02-03-GeometriaFactory-Contracts-r1.md`), sin subir versión por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **H-07**: la fila de reglas de negocio de §9 pasa a referir por identificador `RN-03`, `RN-04`, `RN-05` y `RN-08` de `GeometriaFactory-Domain`, cada una contra el criterio de aceptación que sostiene, con enlaces relativos. **H-09**: la sección opcional se renumera de §12 a §17, el número que `Rules-Especificacion-Funcional.md` §4.3 le asigna para `library`. |
| 1.1 | 2026-08-09 | Actualización por contenido nuevo aguas arriba: `PRODUCT-INTAKE` 1.3 §4 (F-07, F-22, F-24), §4.1 (RN-04 y RN-05 con enunciado ampliado), §4.2 (modelo de estados y significado de `Borrador`), §7 (CL-3), y `NB-03` y `NB-04` de 01 en su versión 1.1. Cambios: el conjunto cerrado de estados pasa a **cuatro** con `Rechazado`, y sus dos terminales quedan declarados en §3; la solicitud de alta y la de edición se unifican en la **solicitud de envío**, única acción de guardado, cuyo resultado trae el estado que la interpretación decide; FA-03 deja de ser una finalización aparte; se agrega **FA-04**, la eliminación por el administrador sobre la misma solicitud de eliminación; `CONTRATO_TEXTO_NO_INTERPRETABLE` **sale del conjunto cerrado de códigos de error** y pasa a §6.1 como señal declarada, porque con el envío como acción única ya no hay operación que falle por ese motivo; `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` conserva su identificador y **revisa su enunciado** para acotarse al camino del alumno, con el fundamento de por qué un solo código cubre los dos caminos; se reescriben CA-01 a CA-05 y se agregan CA-06 y CA-07; §9 refiere el enunciado vigente de RN-04 y RN-05 advirtiendo que sus slugs quedaron desactualizados a propósito. **Precisión de la misma intervención**: la fila de necesidad de negocio de §9 nombraba «el límite entre guardar y finalizar», acción que el modelo vigente no tiene, y pasa a nombrar el límite entre el texto que verifica y el que no. | Analista Funcional + API Designer (AG-02) |

## 17. Compatibilidad de versión pública

Sección opcional de `Rules-Especificacion-Funcional.md` §4.3, que la numera **§17** y la reserva para `library`. Se conserva su número de la regla, aunque deje un hueco tras §11, para que un lector automatizado que busque §17 en cualquier caso de uso del producto encuentre siempre lo mismo.

- Cambiar el campo de texto original de cadena a una estructura interpretada es el cambio incompatible de mayor impacto de todo el ensamblado: contradice la decisión pre-tomada de `PRODUCT-INTAKE` §17.4 P.11 y obliga a rehacer los dos extremos.
- Agregar un estado al conjunto cerrado del trabajo se trata como incompatible: la pieza pública deja de cubrir todos los casos aunque compile.
- Agregar un campo opcional a la solicitud de alta es compatible.
