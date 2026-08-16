# CU-06006 — Derivar la contraseña y verificar una credencial

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** CU-06006-Derivar-La-Contrasena-Y-Verificar-Una-Credencial.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-00002`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00002-Identidad-Propia-Del-Alumno-Sin-Correo.md); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.12** §4 (F-04, F-05), §4.1 (RN-06006, RN-06013), §11 (RN-B5), §17.3.P.1, §17.3.P.5; provee el mecanismo que `Proyectos/GeometriaFactory-Application/02-Especificacion-Funcional/Especificacion-Funcional.md` §4 y su [`CU-04003`](../../../GeometriaFactory-Application/02-Especificacion-Funcional/Casos-De-Uso/CU-04003-Resolver-El-Ingreso-Y-La-Credencial-Del-Alumno.md) declaran fuera de su alcance
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico` y `08-Calidad-Y-Pruebas` de GeometriaFactory-Infrastructure

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

Convertir una contraseña en claro en el **valor derivado** que el producto guarda, y responder si una contraseña en claro se corresponde con un valor derivado ya guardado.

Es el mecanismo que las tres capas de adentro delegaron acá con nombre y apellido: el dominio modela que la credencial llega ya derivada, la capa de aplicación declara que **no compara credenciales** y exige que la verificación **se declare** al invocar, y el contrato de la API no transporta nunca la forma almacenada. **Este contrato es el único lugar del producto donde una contraseña en claro se convierte en el valor que se guarda, y el único que la compara.** No es el único donde el valor en claro **existe**: la elección de autenticación del producto hace que la contraseña atraviese en claro la pieza pública y la pieza de datos —está registrado aguas arriba como decisión consciente, con su riesgo aceptado por escrito—. Lo propio de acá es que es **el último punto del recorrido**: de este contrato para adentro sólo circula el valor derivado.

Lo que este caso de uso **no** hace: no decide si la cuenta admite ingreso —eso es del dominio, con el estado y la marca—, no emite el acceso —eso es `CU-06008`— y no produce contraseñas —eso es `CU-06007`.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Consumidor (`GeometriaFactory-Application`, y la composición de `GeometriaFactory-Api`) | Primario | Pide derivar una contraseña o verificar una credencial |
| Biblioteca de derivación de clave | Sistema | Ejecuta la derivación. El intake declara **PBKDF2 o Argon2**, y cuál de las dos es punto abierto |

El alumno y el administrador son sujetos de la regla: son quienes eligen la contraseña.

## 3. Precondiciones

- Para derivar: llega una contraseña **en claro**, no vacía.
- Para verificar: llegan una contraseña en claro y un valor derivado ya guardado.
- **Nada de esto se registra.** La contraseña en claro no se escribe en ningún archivo, en ninguna traza y en ningún mensaje.

## 4. Flujo principal

1. El consumidor pide derivar una contraseña en claro.
2. Se deriva con la función de derivación de clave anclada, **nunca en claro y nunca con un resumen simple**.
3. Se devuelve el valor derivado, que es lo único que el producto guarda.
4. Para verificar, el consumidor aporta la contraseña en claro y el valor derivado guardado; se responde **sí o no**, y nada más.
5. La contraseña en claro se descarta al terminar la operación.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | La verificación no coincide | Se responde **no**. No es una condición de error: una contraseña equivocada es el caso normal, y el consumidor la traduce a la respuesta genérica que **no revela cuál campo falló** | Termina la operación |
| FA-02 | Se verifica la contraseña **provisoria** que produjo `CU-06007` | Se responde igual que con cualquier otra: este contrato **no distingue** una provisoria de una elegida por la persona. Quién la trata distinto es el consumidor, por la marca que lleva la cuenta | Termina la operación |
| FA-03 | Se deriva dos veces la misma contraseña | Los dos valores derivados **pueden ser distintos entre sí** y los dos verifican. No es un defecto: es la propiedad esperada de una derivación con material aleatorio por credencial | Paso 3 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del caso de uso |
| --- | --- | --- |
| `CONTRASENA_EN_CLARO_AUSENTE` | Se pidió derivar o verificar sin contraseña: nula o cadena vacía | Termina sin derivar y sin verificar. **No se deriva la cadena vacía**: produciría un valor derivado válido para una credencial que nadie eligió, y `GeometriaFactory-Application` `CU-06003` §6 ya rechaza el valor derivado vacío del otro lado de la frontera |
| `CREDENCIAL_DERIVADA_ILEGIBLE` | El valor derivado guardado no permite verificar: no lleva los parámetros con los que se produjo, o su forma no corresponde a la función anclada | Termina sin responder sí ni no. **Es un defecto del almacén o de una migración de parámetros, no de quien intenta entrar**, y responder «no» lo haría indistinguible de una contraseña equivocada: la cuenta quedaría inaccesible sin que nadie supiera por qué |

**Ninguna de las dos escribe nada** —este contrato no persiste— y **ninguna incluye la contraseña ni el valor derivado en lo que devuelve**.

## 7. Postcondiciones

- **Éxito al derivar:** el consumidor recibe el valor derivado. La contraseña en claro no quedó en ninguna parte.
- **Éxito al verificar:** el consumidor recibe sí o no.
- **Fallo:** el consumidor recibe el código. Nada se guardó y nada se reveló.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Una contraseña en claro | Se deriva | El valor devuelto **no contiene la contraseña** y **no es su resumen simple**: dos contraseñas distintas de la misma longitud producen valores sin relación reconocible entre sí |
| CA-02 | Una contraseña y su valor derivado | Se verifica con la misma contraseña | Responde **sí** |
| CA-03 | El mismo valor derivado | Se verifica con otra contraseña | Responde **no**, y la respuesta **no dice** en qué se diferencian |
| CA-04 | Una misma contraseña derivada dos veces | Se comparan los dos valores derivados | Son **distintos entre sí** y **los dos verifican** contra la contraseña original |
| CA-05 | Una petición de derivación con cadena vacía | Se deriva | Devuelve `CONTRASENA_EN_CLARO_AUSENTE` y **0 valores derivados** |
| CA-06 | Un valor derivado guardado que no corresponde a la función anclada | Se verifica | Devuelve `CREDENCIAL_DERIVADA_ILEGIBLE`, y **no** «no coincide» |
| CA-07 | Cualquiera de las operaciones anteriores, con el registro del servidor observado | Se ejecutan | En el registro **no aparece la contraseña en claro ni el valor derivado**, en ninguna forma |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-00002 |
| Reglas de negocio aplicables | [RN-02006](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02006-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md) y [RN-02013](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02013-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md), las dos **por lo que este contrato no decide**: acá se responde si la credencial coincide, y si la cuenta llega a alguna parte lo resuelven el dominio y la capa de aplicación |
| Puerto o mecanismo que provee | La derivación y la verificación de credenciales, que `GeometriaFactory-Application` declara explícitamente fuera de su alcance |
| Consumidor | `GeometriaFactory-Application` CU-06003, CU-06010 y CU-04011, y la composición de raíz de `GeometriaFactory-Api` |
| Historias de usuario a generar en 06 | US-06017, US-06018 |
| Componentes esperados en 05 | Servicio de derivación y verificación, con la función de derivación anclada y sus parámetros |
| Tests previstos en 08 | Unitarias sin base de datos: derivación no reversible, verificación positiva y negativa, dos derivaciones distintas de la misma contraseña, rechazo del vacío y valor derivado ilegible. Y una inspección de que ninguna traza contiene los dos valores |

## 10. Notas y supuestos

- **Cuál de las dos funciones de derivación se ancla es un punto abierto.** El intake declara «PBKDF2 o Argon2» y no elige; la elección y sus parámetros son de `05-Arquitectura-Tecnica` y se anclan en la primera etapa, con la regla de anclaje de versiones del producto. Este contrato declara la propiedad —nunca en claro, nunca resumen simple— y **no el mecanismo**.
- **El valor derivado se guarda con lo que haga falta para verificarlo.** De ahí la segunda condición de §6: un valor sin sus parámetros no es verificable, y ese caso hay que poder distinguirlo de una contraseña equivocada.
- **La contraseña viaja en claro en el tramo entre el front y la API si ese salto es HTTP plano.** Es un riesgo **aceptado por escrito** aguas arriba, con su salida documentada y no adoptada. Este contrato no lo agrava ni lo resuelve: recibe el valor cuando ya llegó.
- **La elección de autenticación del producto está registrada como decisión consciente y no como omisión**, con su nota de seguridad. Este contrato es su pieza más sensible junto con `CU-06008`.
- **Nada de acá distingue una contraseña provisoria de una elegida.** La provisoria es provisoria por la **marca** que lleva la cuenta, no por su forma.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. |
| 1.1 | 2026-08-10 | Actualización de la cita del `PRODUCT-INTAKE` de **1.11** a **1.12** en la trazabilidad upstream: 1.11 quedó archivada al resolver el Product Owner el desenlace del envío del escenario `E-8`. Corrige el hallazgo **H-02** del informe de auditoría `SDD/Docs/Audit/B-02-03-GeometriaFactory-Infrastructure-r1.md` (ronda 1). El delta entre 1.11 y 1.12 se revisó y sólo alcanza a `E-8`, que no toca lo que este documento declara: sin cambios de contenido. |

## 17. Compatibilidad de la superficie pública

Cambiar los parámetros de la función de derivación es compatible **sólo si los valores ya guardados siguen siendo verificables**; si no lo son, es un cambio incompatible y exige un camino de migración declarado, porque de otro modo toda la comisión queda afuera. **Devolver la contraseña en claro, guardarla, registrarla en una traza o distinguir hacia afuera «contraseña equivocada» de «cuenta inexistente» son cambios incompatibles** y suben versión mayor.
