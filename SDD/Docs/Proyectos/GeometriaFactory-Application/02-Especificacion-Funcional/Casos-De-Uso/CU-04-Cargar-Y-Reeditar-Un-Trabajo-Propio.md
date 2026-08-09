# CU-04 — Cargar y reeditar un trabajo propio

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** CU-04-Cargar-Y-Reeditar-Un-Trabajo-Propio.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-03`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-03-Trabajo-Con-Dueno-Estado-Y-Persistencia.md) §5 (trabajo con existencia propia, conservación del trabajo que no verifica, separación entre alumnos); [`NB-04`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-04-Interpretacion-Fiel-Del-Dato-Del-Alumno.md) §5 (conservación del original); `00-Contexto/Vision-Producto.md` §9.1; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-06, F-07), §4.1 (RN-03, RN-04, RN-08), §4.2, §6 (flujo 2), §17.2.P.5, §17.2.P.11 punto 3; orquesta [`CU-05`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Casos-De-Uso/CU-05-Crear-Y-Reeditar-Un-Trabajo.md) y [`CU-09`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Casos-De-Uso/CU-09-Resolver-El-Acceso-Del-Alumno-A-Un-Trabajo.md) de GeometriaFactory-Domain
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico` y `08-Calidad-Y-Pruebas` de GeometriaFactory-Application

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

Orquestar la carga de un trabajo del alumno —nombre, fecha, descripción y el texto original que emitió su programa— y su reedición mientras siga en estado `Borrador`, atribuyéndolo siempre al alumno que lo carga y conservando su texto tal como llegó. Es lo que convierte el esfuerzo de la Actividad 1 en una unidad con existencia propia y con dueño.

La resolución del estado **no ocurre acá**: el alumno tiene una sola acción de guardado, enviar, y es CU-05 el que interpreta el texto y decide entre `Borrador` y estado `Pendiente`.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Consumidor de los casos de uso (`GeometriaFactory-Api`) | Primario | Invoca la carga o la reedición aportando la identidad del alumno solicitante |
| Puerto de repositorio de trabajos | Sistema | Recupera el trabajo a reeditar y materializa el resultado |
| Puerto de reloj del sistema | Sistema | Provee el sello de alta y el de modificación del trabajo, que son metadatos de orquestación de esta capa |
| Modelo de dominio (`GeometriaFactory-Domain`) | Sistema | Constituye o reedita el trabajo y resuelve si la operación procede para ese solicitante |

El alumno es el sujeto de la regla: es quien carga y reedita.

## 3. Precondiciones

- El consumidor aporta la identidad del alumno solicitante, ya autenticado por la capa externa.
- Para la carga, aporta nombre, fecha, descripción y el texto original.
- Para la reedición, aporta además el identificador del trabajo.

## 4. Flujo principal

1. El consumidor solicita la carga de un trabajo con la identidad del alumno solicitante.
2. El caso de uso toma el sello de alta del puerto de reloj.
3. El caso de uso invoca la constitución del trabajo en el dominio, con el solicitante como dueño y el texto original tal cual llegó (RN-08).
4. El dominio devuelve el trabajo constituido en estado `Borrador`, sin piezas ni observaciones.
5. El caso de uso lo materializa por el puerto de repositorio de trabajos, en una única unidad de trabajo.
6. El caso de uso devuelve el identificador del trabajo y su estado.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El consumidor solicita **reeditar** un trabajo existente | El caso de uso lo recupera por el puerto de repositorio y consulta al dominio si el solicitante puede reeditarlo: pertenencia y estado `Borrador`. Si procede, aplica los datos nuevos, toma el sello de modificación del reloj, descarta las piezas y las observaciones de la interpretación anterior y materializa el resultado | Paso 6 |
| FA-02 | El solicitante no es el dueño del trabajo que quiere reeditar | El caso de uso devuelve no procede con el motivo `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`, deliberadamente indistinguible de la inexistencia (RN-03, INV-02) | Termina el caso de uso |
| FA-03 | El trabajo a reeditar no está en `Borrador` | El caso de uso devuelve no procede con el motivo `OPERACION_FUERA_DE_BORRADOR`: un trabajo enviado o con desenlace no se reedita | Termina el caso de uso |

## 6. Excepciones y errores

| Código | Causa | Respuesta del caso de uso |
| --- | --- | --- |
| `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE` | El solicitante no es el dueño, o el identificador no existe | Devuelve no procede. **Los dos casos comparten motivo por diseño**: es lo que impide averiguar por tanteo qué identificadores existen |
| `OPERACION_FUERA_DE_BORRADOR` | La reedición se pide sobre un trabajo en estado `Pendiente`, `Finalizado` o `Rechazado` | Devuelve no procede y el trabajo queda intacto |
| `DATO_OBLIGATORIO_AUSENTE` | Falta el nombre o la fecha del trabajo | Propaga el rechazo del dominio y no materializa nada |
| `TEXTO_ORIGINAL_ALTERADO` | El consumidor aporta como texto original una versión corregida del que pegó el alumno | Propaga el rechazo del dominio y no materializa nada: el producto no edita el dato del alumno (RN-08) |
| `TRABAJO_SIN_DUENO` | El consumidor no aporta la identidad del solicitante | Termina sin efecto: un trabajo sin dueño no es un trabajo |

Ninguno deja escritura parcial: la unidad de trabajo se abre recién en el paso 5.

## 7. Postcondiciones

- **Éxito, carga:** existe un trabajo en estado `Borrador`, con dueño, identificador propio, sello de alta del reloj y el texto original íntegro.
- **Éxito, reedición:** el trabajo conserva su identificador y su dueño, tiene los datos nuevos, el sello de modificación del reloj y ninguna pieza ni observación de la interpretación anterior.
- **Fallo:** el repositorio queda exactamente como estaba.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un alumno A autenticado y un reloj fijado en 2026-04-02 | El consumidor solicita cargar un trabajo llamado «Actividad 1 — entrega» con el texto de 3 piezas del escenario semilla | El caso de uso devuelve un trabajo en estado `Borrador`, con dueño A, sello de alta 2026-04-02 y el texto guardado idéntico al recibido, carácter por carácter |
| CA-02 | Un trabajo en estado `Borrador` cuyo dueño es el alumno A | El consumidor solicita reeditarlo declarando como solicitante al alumno B | El caso de uso devuelve el motivo `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE` y el trabajo no cambia |
| CA-03 | Un trabajo en estado `Pendiente` cuyo dueño es el alumno A | El consumidor solicita reeditarlo declarando como solicitante al alumno A | El caso de uso devuelve el motivo `OPERACION_FUERA_DE_BORRADOR` |
| CA-04 | Un trabajo en estado `Borrador` del alumno A, con 3 piezas y 2 observaciones de una interpretación anterior | El alumno A lo reedita con un texto nuevo | El trabajo queda con 0 piezas y 0 observaciones, en estado `Borrador`, y con el texto nuevo íntegro |
| CA-05 | Un alumno A autenticado | El consumidor solicita cargar un trabajo sin nombre | El caso de uso devuelve el motivo `DATO_OBLIGATORIO_AUSENTE` y el repositorio no recibe ninguna escritura |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-03, y NB-04 en su criterio de conservación del original |
| Reglas de negocio aplicables | [RN-03](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-03-Trabajo-Ajeno-Indistinguible-De-Inexistente.md), [RN-04](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-04-Eliminacion-Acotada-Al-Borrador.md), [RN-08](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-08-Texto-Original-Conservado-Integro.md) |
| Casos de uso de dominio orquestados | [CU-05](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Casos-De-Uso/CU-05-Crear-Y-Reeditar-Un-Trabajo.md), [CU-09](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Casos-De-Uso/CU-09-Resolver-El-Acceso-Del-Alumno-A-Un-Trabajo.md) |
| Puertos que consume | Repositorio de trabajos, reloj del sistema |
| Historias de usuario a generar en 06 | US-10, US-11, US-12 |
| Componentes esperados en 05 | Caso de uso de carga y caso de uso de reedición, con la resolución de pertenencia intercalada antes de tocar el repositorio |
| Tests previstos en 08 | Unitarias con repositorio simulado: carga con sello del reloj, texto conservado carácter por carácter, reedición ajena, reedición fuera de `Borrador` y descarte de la interpretación anterior |

## 10. Notas y supuestos

- **La pertenencia se verifica antes de cualquier escritura y sobre el dato recuperado**, no sobre lo que declara la petición. Que el consumidor haya autenticado a la persona no alcanza: el papel no dice de quién es el trabajo.
- El producto **no edita el dato del alumno**: la reedición cambia los datos del trabajo y el texto que el alumno vuelve a pegar, nunca el texto ya guardado.
- La reedición descarta la interpretación anterior porque el texto cambió; volver a interpretarlo es CU-05.
- **El sello de alta y el de modificación son metadatos de orquestación** que esta capa aporta al materializar. **No son la «Fecha» del trabajo**, que es dato que declara el alumno y que el modelo del dominio sí modela como tal; el modelo no declara fecha de creación ni de última modificación del trabajo, y la discrepancia está elevada al Product Owner. Hasta que resuelva, estos dos sellos se leen como dato de esta capa.
- **`REEDICION_FUERA_DE_BORRADOR` del dominio y `OPERACION_FUERA_DE_BORRADOR` de este contrato son la misma negativa.** Esta capa corta antes, con la resolución de acceso del dominio, de modo que el rechazo de la constitución nunca llega a producirse; se declara la equivalencia para que no se lean como dos condiciones distintas.
- La eliminación no está acá: es CU-09.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. |
| 1.0 | 2026-08-09 | **Correcciones de la ronda r1 del audit**, absorbidas sin subir versión por `Master-Prompt.md` §5, con el documento en estado `Propuesto`. **H-06**: la fecha de alta y la de modificación del trabajo pasan a llamarse **sellos** y se declaran metadatos de orquestación en §2, §4, §7, §8 y §10, distintos de la «Fecha» que el alumno declara y que el modelo del dominio sí modela; el modelo no declara esos dos atributos y la discrepancia está elevada al Product Owner. **H-14**: §6 suma `TEXTO_ORIGINAL_ALTERADO`, propagado del dominio, y §10 declara la equivalencia entre `REEDICION_FUERA_DE_BORRADOR` del dominio y `OPERACION_FUERA_DE_BORRADOR` de este contrato. |

## 17. Compatibilidad de la superficie pública

Agregar datos opcionales al trabajo es compatible. Distinguir hacia afuera el trabajo ajeno del inexistente, o admitir la reedición fuera de `Borrador`, contradicen RN-03 y RN-04.
