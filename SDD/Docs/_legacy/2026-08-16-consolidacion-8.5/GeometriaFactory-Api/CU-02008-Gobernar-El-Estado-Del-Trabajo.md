# CU-02008 — Gobernar el estado del trabajo en el envío

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** CU-02008-Gobernar-El-Estado-Del-Trabajo.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-00003`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00003-Trabajo-Con-Dueno-Estado-Y-Persistencia.md) §5 (visibilidad del avance sobre los 4 estados y cierre del circuito de entrega en el estado `Pendiente` del trabajo); [`NB-00004`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00004-Interpretacion-Fiel-Del-Dato-Del-Alumno.md) §5 (límite entre lo que no verifica y la entrega, y acción única de guardado); [`NB-00005`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00005-Visibilidad-Del-Error-De-Calculo.md) §5 (carácter no bloqueante); `00-Contexto/Vision-Producto.md` §9.1 (estado del trabajo, enviar) y §9.2; `00-Contexto/Alcance-Producto.md` §4.1; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-08 y F-22), §4.1 (RN-02005 y RN-02010), §4.2 (modelo de estados del trabajo), §17.1.P.2 (INV-04 e INV-07), §6 (flujos 2 y 4), §7 (CL-3 y CL-4), §20.E-1, §20.E-2, §20.E-5
**Trazabilidad downstream:** `05-Arquitectura-Tecnica` y `06-Backlog-Tecnico` de GeometriaFactory-Domain; `08-Calidad-Y-Pruebas`

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

Resolver el estado de un trabajo cuando el alumno lo envía, que es su **única acción de guardado**: el dominio decide entre `Borrador` y `Pendiente` según las observaciones que dejó la interpretación, y hace cumplir que un trabajo no llegue a estado `Pendiente` con errores de validación mientras que las advertencias no lo impidan.

El desenlace del trabajo —aprobar o rechazar— **no está acá**: es facultad exclusiva del administrador y vive en CU-02010.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Capa de casos de uso del producto (`GeometriaFactory-Application`) | Primario | Solicita el envío del trabajo después de haber incorporado el resultado de la interpretación |
| Capa de infraestructura (`GeometriaFactory-Infrastructure`) | Secundario | Materializa el estado resultante fuera del dominio |
| Modelo de dominio de `GeometriaFactory-Domain` | Sistema | Resuelve el estado y rechaza toda transición no declarada |

El alumno es el sujeto de la regla: es quien envía. El actor del contrato es el código consumidor.

## 3. Precondiciones

- El trabajo existe y su estado pertenece al conjunto cerrado `Borrador`, `Pendiente`, `Finalizado`, `Rechazado`.
- El envío se solicita sobre un trabajo en estado `Borrador`, que es el único desde el que el alumno opera.
- Las piezas y las observaciones del envío en curso ya fueron incorporadas por CU-02006 y CU-02007.

## 4. Flujo principal

1. La capa de aplicación solicita el envío del trabajo.
2. El dominio comprueba que el estado actual sea `Borrador`.
3. El dominio comprueba si el trabajo tiene al menos una observación de especie error de validación.
4. Si no la tiene, el dominio pasa el trabajo a estado `Pendiente`.
5. El dominio conserva las observaciones de especie advertencia, que no impiden el paso (RN-02005).
6. El dominio devuelve el trabajo con su estado resuelto.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El trabajo enviado tiene al menos una observación de especie error de validación | El dominio **deja el trabajo en `Borrador`**, con sus errores localizados por posición de pieza y campo. No es un rechazo de la operación: es el resultado declarado del envío, y el alumno corrige y vuelve a enviar cuantas veces haga falta (PRODUCT-INTAKE §6, flujo 2) | Paso 6, con estado `Borrador` |
| FA-02 | El trabajo enviado tiene advertencias y ningún error de validación | El dominio lo pasa a estado `Pendiente` **con** sus advertencias asociadas. Es el caso de los escenarios E-1 y E-2, y el carácter no bloqueante es deliberado | Paso 4 |
| FA-03 | Se solicita enviar un trabajo que ya está en estado `Pendiente` | Ninguna fuente declara una reentrada al envío desde `Pendiente`: el trabajo ya salió de las manos del alumno. El dominio la rechaza y no la infiere | Termina con el rechazo de §6 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del dominio |
| --- | --- | --- |
| `ENVIO_FUERA_DE_BORRADOR` | Se solicita enviar un trabajo que no está en `Borrador` | Rechaza la operación y conserva el estado actual |
| `TRANSICION_DESDE_ESTADO_TERMINAL` | Se solicita cualquier transición sobre un trabajo en estado `Finalizado` o `Rechazado` | Rechaza la operación: los dos estados de cierre son terminales y de ellos no sale ninguna transición (INV-07, RN-02010) |
| `ENVIO_SIN_INTERPRETACION` | Se solicita enviar un trabajo cuyo texto original nunca fue interpretado | Rechaza la operación: el envío decide sobre el resultado de la interpretación, y sin ese resultado no hay nada que decidir |
| `DESENLACE_NO_ADMITIDO_EN_ESTE_CONTRATO` | Se solicita aprobar o rechazar por esta vía | Rechaza la operación: el desenlace es facultad exclusiva del administrador y se ejerce por CU-02010 |

## 7. Postcondiciones

- **Éxito con texto que verifica:** el trabajo está en estado `Pendiente`, con sus advertencias conservadas y a la espera de la revisión del administrador.
- **Éxito con texto que no verifica:** el trabajo sigue en `Borrador`, con sus observaciones de especie error de validación y su ubicación.
- En los dos casos el dueño, el texto original y las piezas no cambiaron.
- **Fallo:** el estado se conserva. Ningún rechazo altera el texto original ni descarta las observaciones ya registradas.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un trabajo en estado `Borrador` con las 2 advertencias del escenario E-1 y 0 errores de validación | La capa de aplicación solicita enviarlo | El dominio devuelve el trabajo en estado `Pendiente`, con sus 2 advertencias conservadas |
| CA-02 | Un trabajo en estado `Borrador` con 1 observación de especie error de validación, la del escenario E-5 con posición de pieza 1 y campo `Tipo` | La capa de aplicación solicita enviarlo | El dominio devuelve el trabajo en estado `Borrador`, con su error localizado, y no lo pasa a estado `Pendiente` |
| CA-03 | Un trabajo en estado `Borrador` con el texto del escenario E-2, cuya única observación es la advertencia de volumen 343.00 contra 1029.00 | La capa de aplicación solicita enviarlo | El dominio devuelve el trabajo en estado `Pendiente`: la advertencia no lo impide |
| CA-04 | Un trabajo en estado `Finalizado` | La capa de aplicación solicita enviarlo de nuevo | El dominio rechaza con el código `TRANSICION_DESDE_ESTADO_TERMINAL` y el estado sigue siendo `Finalizado` |
| CA-05 | Un trabajo en estado `Rechazado` | La capa de aplicación solicita devolverlo a `Borrador` | El dominio rechaza con el código `TRANSICION_DESDE_ESTADO_TERMINAL`: corregir un rechazo significa cargar un trabajo nuevo |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-00003, NB-00004, NB-00005 |
| Reglas de negocio aplicables | [RN-02005](../Reglas-De-Negocio/RN-02005-Finalizacion-Sin-Errores-De-Validacion.md), [RN-02010](../Reglas-De-Negocio/RN-02010-Desenlace-Exclusivo-Del-Administrador-Y-Terminalidad.md) en cuanto a la terminalidad, [RN-02004](../Reglas-De-Negocio/RN-02004-Eliminacion-Acotada-Al-Borrador.md) en cuanto al estado que el alumno opera |
| Invariantes | INV-04, INV-07 |
| Historias de usuario a generar en 06 | US de envío que pasa a estado `Pendiente`, US de envío que queda en `Borrador` con sus errores, US de terminalidad de los dos estados de cierre |
| Componentes esperados en 05 | Máquina de transiciones de estado del trabajo dentro de la entidad de trabajo |
| Tests previstos en 08 | Pruebas unitarias de las transiciones admitidas y de las inadmisibles, con E-1 y E-2 como envíos que pasan a estado `Pendiente` con advertencias, E-5 como envío que queda en `Borrador`, y E-4 y E-6 como envíos sin observación bloqueante |

## 10. Notas y supuestos

- **Guardar y enviar son la misma acción** (PRODUCT-INTAKE §4, F-22). Por eso `Borrador` significa exactamente «el texto no verificó»: un texto que verifica no puede quedarse en borrador.
- El corte de RN-02005 **se adelantó del cierre al envío** con el modelo de estados vigente: antes se verificaba al finalizar, y ahora se verifica al pasar a estado `Pendiente`. INV-04 sigue siendo verdadero por consecuencia: si ningún trabajo llega a estado `Pendiente` con errores y `Finalizado` sólo se alcanza desde `Pendiente`, entonces todo trabajo `Finalizado` tiene el texto interpretado sin errores.
- El estado no se deriva de las observaciones en todo momento: es un atributo propio del trabajo, y las observaciones sólo deciden el resultado del envío.
- La eliminación no es una transición de estado y no vive acá: la del alumno, acotada al `Borrador`, está en CU-02009, y la del administrador, que alcanza los tres estados que ve, en CU-02011.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. |
| 1.1 | 2026-08-09 | Reescrito para absorber el circuito de revisión de `PRODUCT-INTAKE` 1.3. Sube minor y archiva el estado anterior por `Master-Prompt.md` §5. El caso de uso pasa a gobernar **el envío** y no el cierre: la transición de `Pendiente` a `Finalizado` sale de acá y se emite como **CU-02010**, junto con el rechazo. El conjunto de estados pasa a cuatro con `Rechazado`; **RN-02005 cambia de momento**, del cierre al envío, de modo que un trabajo con errores de validación queda en `Borrador` en lugar de ser rechazada la operación; se incorpora la **terminalidad** de `Finalizado` y `Rechazado` con INV-07 y el código `TRANSICION_DESDE_ESTADO_TERMINAL`; y se declara que guardar y enviar son la misma acción. Los cinco criterios de aceptación se reescribieron sobre los escenarios E-1, E-2 y E-5. **Correcciones de la ronda r1 del audit**: hallazgo **P3-08**, §10 remitía la eliminación sólo a CU-02009 y ahora reparte entre CU-02009 —la del alumno, acotada al `Borrador`— y CU-02011 —la del administrador, en los tres estados que ve—; hallazgo **P3-04**, la sección opcional se numera §17. |

## 17. Compatibilidad de la superficie pública

Agregar un estado al conjunto cerrado, admitir una salida de `Finalizado` o de `Rechazado`, o reintroducir una acción de guardado separada del envío son cambios de alcance de este caso de uso y de la máquina de estados del modelo: suben versión mayor y exigen revisar RN-02004, RN-02005 y RN-02010.
