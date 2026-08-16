# CU-04005 — Enviar un trabajo e interpretar su texto

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** CU-04005-Enviar-Un-Trabajo-E-Interpretar-Su-Texto.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-12
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-00004`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00004-Interpretacion-Fiel-Del-Dato-Del-Alumno.md) §5 (localización del defecto, límite entre lo que no verifica y la entrega, acción única de guardado, conservación del original); [`NB-00005`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00005-Visibilidad-Del-Error-De-Calculo.md) §5 (carácter no bloqueante, advertencia explicativa, cobertura sobre el escenario semilla); [`NB-00003`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00003-Trabajo-Con-Dueno-Estado-Y-Persistencia.md) §5 (conservación del trabajo que no verifica); `00-Contexto/Vision-Producto.md` §9.1; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-09, F-10, F-22), §4.1 (RN-04005, RN-04008, RN-04009), §4.2, §6 (flujo 2 y flujo 4), §7 (CL-3, CL-4), §17.2.P.10, §17.2.P.11 puntos 1 y 2, §20.E-1; orquesta [`CU-02006`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Casos-De-Uso/CU-02006-Reconstruir-El-Conjunto-De-Piezas-Del-Trabajo.md), [`CU-02007`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Casos-De-Uso/CU-02007-Registrar-Las-Observaciones-Del-Trabajo.md), [`CU-02008`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Casos-De-Uso/CU-02008-Gobernar-El-Estado-Del-Trabajo.md) y [`CU-02009`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Casos-De-Uso/CU-02009-Resolver-El-Acceso-Del-Alumno-A-Un-Trabajo.md) de GeometriaFactory-Domain
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

Orquestar la **única acción de guardado** del alumno: pedirle al puerto de validación de figuras que interprete el texto original del trabajo, incorporar al dominio las piezas reconstruidas y las observaciones que produjo, y dejar que el dominio resuelva el estado resultante —`Borrador` si hubo errores de validación, estado `Pendiente` si no los hubo, aunque haya advertencias—.

Es el caso de uso más pesado de la capa y el que la prueba con dobles justifica: recorre todas las piezas y sus componentes **sin tocar la base de datos**, porque el validador está detrás de un puerto.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Consumidor de los casos de uso (`GeometriaFactory-Api`) | Primario | Invoca el envío del trabajo con la identidad del alumno solicitante |
| Puerto de validación de figuras | Sistema | Interpreta el texto original y devuelve **la cantidad de figuras del conjunto raíz**, las piezas reconstruidas y las observaciones, con su especie y su ubicación |
| Puerto de repositorio de trabajos | Sistema | Recupera el trabajo y materializa el resultado del envío |
| Puerto de reloj del sistema | Sistema | Provee el sello de modificación del trabajo, que es un metadato de orquestación de esta capa |
| Modelo de dominio (`GeometriaFactory-Domain`) | Sistema | Adopta piezas y observaciones, y resuelve el estado del trabajo |

El alumno es el sujeto de la regla: es quien envía.

## 3. Precondiciones

- El trabajo existe, está en estado `Borrador` y tiene texto original.
- El consumidor aporta la identidad del alumno solicitante.
- El puerto de validación de figuras devuelve, junto con el resultado, **la cantidad de figuras del conjunto raíz**. El dominio la exige como precondición y no es derivable de las piezas adoptadas, que admiten huecos.

## 4. Flujo principal

1. El consumidor solicita el envío de un trabajo.
2. El caso de uso lo recupera por el puerto de repositorio y consulta al dominio si el solicitante puede operarlo: pertenencia y estado `Borrador`.
3. El caso de uso entrega el texto original al puerto de validación de figuras, que devuelve **cuántas figuras trae el conjunto raíz** —incluidas las que no se pudieron reconstruir—, las piezas reconstruidas con su posición y sus valores declarado y derivado, y el conjunto de observaciones.
4. El caso de uso incorpora al trabajo el conjunto de piezas con su identidad posicional **y la cantidad de figuras del conjunto raíz**, que es el rango de posiciones válidas contra el que el dominio valida después cada observación.
5. El caso de uso incorpora al trabajo las observaciones, cada una con su especie —`Advertencia` o `Error de validación`— y su ubicación.
6. El caso de uso toma el sello de modificación del puerto de reloj e invoca el envío en el dominio.
7. El conjunto no tiene ninguna observación de especie error de validación: el dominio pasa el trabajo a estado `Pendiente` (RN-04005).
8. El caso de uso materializa el trabajo, sus piezas y sus observaciones en una única unidad de trabajo, y devuelve el estado resultante junto con las observaciones.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | La interpretación produjo al menos una observación de especie error de validación | El dominio deja el trabajo en `Borrador`. El caso de uso materializa el trabajo con sus observaciones y devuelve el estado `Borrador` con los errores localizados por posición de pieza y campo, para que el alumno corrija y vuelva a enviar (RN-04005, RN-04009) | Paso 8 |
| FA-02 | La interpretación produjo advertencias y ningún error de validación | El trabajo pasa a estado `Pendiente` **con** sus advertencias: una discrepancia entre el valor declarado y el derivado se señala, no bloquea ni se corrige | Paso 8 |
| FA-03 | Alguna figura del texto no se pudo reconstruir | Su posición queda reservada en el conjunto de piezas y la observación correspondiente la designa por esa posición, que pertenece al rango declarado aunque la pieza no exista. El envío sigue su curso y el estado lo decide la especie de las observaciones | Paso 5 |
| FA-04 | El solicitante no es el dueño del trabajo | El caso de uso devuelve no procede con el motivo `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE` y no invoca al validador | Termina el caso de uso |

## 6. Excepciones y errores

| Código | Causa | Respuesta del caso de uso |
| --- | --- | --- |
| `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE` | El solicitante no es el dueño, o el identificador no existe | Devuelve no procede sin invocar el validador ni escribir nada |
| `ENVIO_FUERA_DE_BORRADOR` | Se envía un trabajo en estado `Pendiente` | Propaga el rechazo del dominio y conserva el estado actual |
| `TRANSICION_DESDE_ESTADO_TERMINAL` | Se envía un trabajo en `Finalizado` o en `Rechazado` | Propaga el rechazo del dominio, que para los dos estados de cierre devuelve **este** motivo y no el anterior: el invariante de terminalidad no los distingue entre sí |
| `INTERPRETACION_NO_DISPONIBLE` | El puerto de validación de figuras no puede completar la interpretación | Termina de forma controlada, deja el trabajo en `Borrador` con su texto intacto y devuelve el estado degradado. No se inventan observaciones ni se pasa a estado `Pendiente` |
| `CONJUNTO_DE_PIEZAS_MAL_FORMADO` | El dominio rechaza la reconstrucción por posición inválida —repetida, negativa o fuera del rango declarado—, tipo de pieza desconocido, familia que contradice al tipo, o reconstrucción sobre un trabajo terminal | No materializa nada. **Es una condición agregada**, simétrica a la de las observaciones: los cuatro rechazos del dominio son defectos del validador o de la orquestación, y ninguno es un resultado que el alumno deba ver |
| `OBSERVACION_MAL_FORMADA` | El dominio rechaza el conjunto de observaciones por especie desconocida, error sin ubicación, advertencia sin los dos valores u observación sobre una posición inexistente | No materializa nada: un conjunto mal formado es un defecto del validador y no un resultado que el alumno deba ver |

Ninguno modifica el texto original y ninguno deja escritura parcial.

## 7. Postcondiciones

- **Éxito sin errores de validación:** el trabajo está en estado `Pendiente`, con su conjunto de piezas, la cantidad de figuras de su conjunto raíz y sus advertencias si las hubo, y con el sello de modificación del reloj.
- **Éxito con errores de validación:** el trabajo sigue en `Borrador`, con sus observaciones registradas y su texto original íntegro.
- **Fallo:** el trabajo queda exactamente como estaba, en `Borrador`.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un trabajo en `Borrador` del alumno A con el texto semilla de 3 piezas —cilindro, cubo y ortoedro—, y un validador doble que devuelve 3 piezas y 2 advertencias: área declarada 36.00 contra derivada 54.00 en el cubo, y volumen declarado 343.00 contra derivado 1029.00 en el ortoedro | El alumno A envía el trabajo | El caso de uso devuelve el trabajo en estado `Pendiente` con 3 piezas y 2 advertencias, y ninguna de las dos lo bloquea |
| CA-02 | Un trabajo en `Borrador` del alumno A y un validador doble que declara un conjunto raíz de 3 figuras, devuelve 2 piezas reconstruidas y 1 error de validación de tipo desconocido, en la posición 2 y en el campo `Tipo` | El alumno A envía el trabajo | El caso de uso devuelve el trabajo en `Borrador`, con cantidad de figuras del conjunto raíz 3, 2 piezas, la posición 2 reservada y 1 observación de especie error de validación que indica posición 2 y campo `Tipo` |
| CA-03 | El mismo trabajo del criterio CA-02, ya devuelto a `Borrador` | El alumno A corrige el texto por CU-04004 y vuelve a enviar, y ahora el validador doble no devuelve errores de validación | El caso de uso devuelve el trabajo en estado `Pendiente` y 0 observaciones de especie error de validación |
| CA-04 | Un trabajo en estado `Pendiente` del alumno A | El alumno A vuelve a enviarlo | El caso de uso devuelve el motivo `ENVIO_FUERA_DE_BORRADOR` y el estado no cambia |
| CA-05 | Un trabajo en `Borrador` del alumno A | El alumno B lo envía | El caso de uso devuelve el motivo `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE` y el validador doble registra 0 invocaciones |
| CA-06 | Un trabajo en `Borrador` del alumno A con el texto semilla de 3 piezas y un validador doble sin latencia añadida | El alumno A envía el trabajo | El caso de uso resuelve en menos de 500 ms, medido sin acceso a base de datos |
| CA-07 | Un trabajo en `Finalizado` del alumno A | El alumno A vuelve a enviarlo | El caso de uso devuelve el motivo `TRANSICION_DESDE_ESTADO_TERMINAL`, y no `ENVIO_FUERA_DE_BORRADOR`, que es el que el dominio reserva para el estado `Pendiente` |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-00004, NB-00005 y NB-00003 |
| Reglas de negocio aplicables | [RN-02005](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02005-Finalizacion-Sin-Errores-De-Validacion.md), [RN-02008](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02008-Texto-Original-Conservado-Integro.md), [RN-02009](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02009-Observacion-De-Error-Con-Posicion-Y-Campo.md), [RN-02003](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02003-Trabajo-Ajeno-Indistinguible-De-Inexistente.md) |
| Casos de uso de dominio orquestados | [CU-02006](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Casos-De-Uso/CU-02006-Reconstruir-El-Conjunto-De-Piezas-Del-Trabajo.md), [CU-02007](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Casos-De-Uso/CU-02007-Registrar-Las-Observaciones-Del-Trabajo.md), [CU-02008](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Casos-De-Uso/CU-02008-Gobernar-El-Estado-Del-Trabajo.md), [CU-02009](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Casos-De-Uso/CU-02009-Resolver-El-Acceso-Del-Alumno-A-Un-Trabajo.md) |
| Puertos que consume | Validación de figuras —que aporta además la cantidad de figuras del conjunto raíz—, repositorio de trabajos, reloj del sistema |
| Historias de usuario a generar en 06 | US-04013, US-04014, US-04015, US-04016 |
| Componentes esperados en 05 | Caso de uso de envío; contrato del puerto de validación de figuras con su resultado de piezas y observaciones |
| Tests previstos en 08 | Unitarias con validador y repositorio simulados sobre los escenarios de datos declarados en el intake: interpretación con advertencias, con errores localizados, con figura no reconstruida, y el envío fuera de `Borrador`. La interpretación real del texto la prueba `GeometriaFactory-Infrastructure` |

## 10. Notas y supuestos

- **Las dos negativas por estado del envío ya tienen código propio en el contrato.** `ENVIO_FUERA_DE_BORRADOR` y `TRANSICION_DESDE_ESTADO_TERMINAL` viajaban hasta hoy en el código genérico. El Product Owner incorporó `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` (`PRODUCT-INTAKE` **1.29** §17.4 P.3), que cubre las dos: un trabajo en `Pendiente`, `Finalizado` o `Rechazado` es de sólo lectura. **Este caso de uso no cambia y no colapsa sus dos motivos**: la distinción que esta capa hace sigue viva aguas arriba del contrato.
- **El validador de figuras es un puerto y no una dependencia concreta**: es lo que permite ejercer este caso de uso entero con dobles y aislar la lógica de tolerancia del formato, que vive en la implementación.
- **La verificación de valores produce observaciones de dos especies** y sólo el error de validación impide el paso a estado `Pendiente`. El caso de uso no decide el estado: le entrega al dominio el conjunto y el dominio lo resuelve.
- El texto original nunca se reescribe, ni siquiera cuando la interpretación falla: es lo que el alumno vuelve a ver al reeditar.
- El tiempo objetivo del criterio CA-06 se toma de la asunción de requerimientos no funcionales declarada aguas arriba y pendiente de confirmación del Product Owner; se usa como valor vigente.
- **La cantidad de figuras del conjunto raíz la produce el validador y la hace viajar este caso de uso.** Es el único orquestador de la reconstrucción y del registro de observaciones, de modo que es el único que puede aportarla; sin ella el dominio no tiene rango contra el cual validar la posición de una observación, y la posición reservada de una figura no reconstruida deja de ser comprobable.
- **Esta capa no colapsa los dos motivos del envío.** `ENVIO_FUERA_DE_BORRADOR` queda acotado al estado `Pendiente` y los dos estados de cierre devuelven `TRANSICION_DESDE_ESTADO_TERMINAL`, que es el motivo único del dominio para los dos y que no los distingue entre sí.
- **Dos rechazos del dominio son inalcanzables por construcción** y se nombran para que su ausencia en §6 no se lea como olvido: `ENVIO_SIN_INTERPRETACION`, porque el paso 3 interpreta siempre antes del paso 6, y `DESENLACE_NO_ADMITIDO_EN_ESTE_CONTRATO`, porque este caso de uso no ofrece aprobar ni rechazar, que son CU-04008.
- La previsualización en tres dimensiones y el árbol del texto no son de esta capa.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. |
| 1.0 | 2026-08-09 | **Correcciones de la ronda r1 del audit**, absorbidas sin subir versión por `Master-Prompt.md` §5, con el documento en estado `Propuesto`. **H-02**: `ENVIO_FUERA_DE_BORRADOR` se acota al estado `Pendiente` y §6 suma `TRANSICION_DESDE_ESTADO_TERMINAL`, que es el motivo que el dominio devuelve para los dos estados de cierre y que esta especificación no nombraba; el criterio CA-07 lo ancla. **H-03**: la **cantidad de figuras del conjunto raíz** entra al contrato del puerto de validación y viaja hasta el dominio, que la exige como precondición y la hereda a su registro de observaciones; se declara en §2, §3, §4 pasos 3 y 4, §7, §9, CA-02 y §10. **H-06**: la fecha de modificación pasa a llamarse **sello de modificación** y se declara metadato de orquestación. **H-14**: §6 suma la condición agregada `CONJUNTO_DE_PIEZAS_MAL_FORMADO`, simétrica a la de las observaciones, que da camino de vuelta a los cuatro rechazos de la reconstrucción del dominio, y §10 declara inalcanzables por construcción `ENVIO_SIN_INTERPRETACION` y `DESENLACE_NO_ADMITIDO_EN_ESTE_CONTRATO`. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |

## 17. Compatibilidad de la superficie pública

Agregar campos al resultado de la interpretación es compatible mientras la especie de la observación conserve sus dos valores. Admitir una tercera especie, o pasar a estado `Pendiente` con errores de validación, contradicen RN-04005 y suben versión mayor.
