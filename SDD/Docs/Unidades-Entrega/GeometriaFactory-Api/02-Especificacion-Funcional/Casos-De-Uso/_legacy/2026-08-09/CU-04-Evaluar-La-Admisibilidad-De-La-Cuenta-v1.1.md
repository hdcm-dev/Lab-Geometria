# CU-04 — Evaluar la admisibilidad de la cuenta para acceder al laboratorio

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** CU-04-Evaluar-La-Admisibilidad-De-La-Cuenta.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-01`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-01-Control-De-Admision-Al-Laboratorio.md) §5; [`NB-02`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md) §2 y §5; `00-Contexto/Vision-Producto.md` §9.1 y §9.2; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4.1 (RN-06), §17.1.P.2 (INV-06), §17.1.P.5, §17.5.P.5, §6 (flujo 1)
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

Responder, sobre una cuenta concreta, si admite o no admite acceso al laboratorio y con qué motivo, para que la capa que emite el acceso no tenga que interpretar por su cuenta el estado de la cuenta. Materializa INV-06: un alumno con cuenta `Pendiente` o `Bloqueado` no obtiene acceso.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Capa de casos de uso del producto (`GeometriaFactory-Application`) | Primario | Consulta la admisibilidad antes de resolver un ingreso |
| Capa de infraestructura (`GeometriaFactory-Infrastructure`) | Secundario | Emite el acceso sólo si la evaluación fue admisible. El mecanismo de emisión no es del dominio |
| Modelo de dominio de `GeometriaFactory-Domain` | Sistema | Evalúa el estado de la cuenta y devuelve el motivo de la negativa |

## 3. Precondiciones

- El alumno existe y su estado de cuenta pertenece al conjunto `Pendiente`, `Habilitado`, `Bloqueado`.
- La comprobación de la credencial presentada ya ocurrió, o va a ocurrir, fuera del dominio: esta evaluación es sobre la cuenta, no sobre la credencial.

## 4. Flujo principal

1. La capa de aplicación consulta la admisibilidad de la cuenta de un alumno.
2. El dominio lee el estado de cuenta.
3. El dominio comprueba que el estado sea `Habilitado`.
4. El dominio comprueba que la credencial derivada tenga valor.
5. El dominio devuelve admisible, sin ningún motivo asociado.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El estado de cuenta es `Habilitado` pero la credencial derivada no tiene valor | El dominio devuelve **no admisible** con el motivo `CREDENCIAL_NO_ESTABLECIDA`. No es un rechazo: es la situación esperada del primer ingreso efectivo, en la que corresponde invocar CU-03 | Termina el caso de uso con resultado no admisible |
| FA-02 | La cuenta consultada tiene papel `Administrador` | La evaluación es la misma: se resuelve por estado y por credencial, no por papel. La autorización por papel es de la capa que expone los endpoints, no del dominio | Paso 3 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del dominio |
| --- | --- | --- |
| `CUENTA_PENDIENTE` | El estado de cuenta es `Pendiente` | Devuelve no admisible con este motivo, para que el consumidor pueda informarle a la persona su situación con todas las letras y no con un rechazo genérico |
| `CUENTA_BLOQUEADA` | El estado de cuenta es `Bloqueado` | Devuelve no admisible con este motivo |
| `CREDENCIAL_NO_ESTABLECIDA` | El estado es `Habilitado` y la credencial derivada no tiene valor | Devuelve no admisible con este motivo, que el consumidor traduce en el pedido de establecer la contraseña |

Los tres son terminaciones controladas y no excepciones de programa: la evaluación siempre devuelve un resultado, y ese resultado incluye el motivo. Ninguno es un código de protocolo: la traducción a respuesta pertenece a `GeometriaFactory-Api`.

## 7. Postcondiciones

- **Éxito:** el resultado es admisible o no admisible con exactamente un motivo. En ningún caso el dominio cambia el estado de la cuenta: la evaluación no tiene efecto.
- **Fallo:** no hay caso de fallo propio. Una cuenta inexistente no llega hasta acá, porque el dominio evalúa sobre una entidad ya constituida.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un alumno con cuenta `Habilitado` y credencial derivada con valor | La capa de aplicación consulta la admisibilidad | El dominio devuelve admisible, con 0 motivos, y la cuenta sigue en `Habilitado` |
| CA-02 | Un alumno con cuenta `Pendiente` | La capa de aplicación consulta la admisibilidad | El dominio devuelve no admisible con el motivo `CUENTA_PENDIENTE` |
| CA-03 | Un alumno con cuenta `Bloqueado` y credencial derivada con valor | La capa de aplicación consulta la admisibilidad | El dominio devuelve no admisible con el motivo `CUENTA_BLOQUEADA` |
| CA-04 | Un alumno con cuenta `Habilitado` y credencial derivada sin valor | La capa de aplicación consulta la admisibilidad | El dominio devuelve no admisible con el motivo `CREDENCIAL_NO_ESTABLECIDA` |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-01 en su criterio de admisión explícita, NB-02 en su criterio de explicación al alumno no habilitado |
| Reglas de negocio aplicables | [RN-06](../Reglas-De-Negocio/RN-06-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md), y [RN-01](../Reglas-De-Negocio/RN-01-Administrador-Unico-Y-Papeles-Fijos.md) en cuanto al conjunto cerrado de papeles |
| Invariantes | INV-06 |
| Historias de usuario a generar en 06 | US de ingreso de alumno habilitado, US de aviso de cuenta pendiente, US de aviso de cuenta bloqueada |
| Componentes esperados en 05 | Consulta de admisibilidad sobre la entidad de alumno, con su enumeración cerrada de motivos |
| Tests previstos en 08 | Cuatro pruebas unitarias, una por cada resultado posible, sin dobles |

## 10. Notas y supuestos

- INV-06 es una regla de dominio aunque el acceso se materialice en la infraestructura. El dominio modela **la condición**; el mecanismo por el que el acceso se emite y su vigencia pertenecen a `GeometriaFactory-Infrastructure` y a `GeometriaFactory-Api`.
- La distinción entre un rechazo genérico por credencial inválida y un aviso explícito por cuenta `Pendiente` o `Bloqueado` es una decisión declarada aguas arriba (PRODUCT-INTAKE §17.5.P.5) y este caso de uso le da al consumidor el dato para sostenerla.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. |
| 1.1 | 2026-08-09 | Absorbe `PRODUCT-INTAKE` 1.3 y la resolución de la ambigüedad de los invariantes. Sube minor y archiva el estado anterior por `Master-Prompt.md` §5. §9 incorpora **RN-06**, la regla que INV-06 sostiene y cuyo enunciado el intake anterior no transcribía. Se califican las ocurrencias de `Pendiente` según `Vision-Producto.md` §9.2. **Corrección de la ronda r1 del audit, hallazgo P3-04**: la sección opcional de compatibilidad se numera §17 y no §12, que es el número que `Rules-Especificacion-Funcional.md` §4.3 le asigna a la variante `library`. |

## 17. Compatibilidad de la superficie pública

El conjunto de motivos es cerrado y forma parte del contrato: agregar un motivo obliga a revisar a los consumidores que los traducen a mensajes, y por eso sube la versión mayor de este caso de uso.
