> **Artefacto archivado — estado `Superado`**
>
> Esta es una **copia archivada** del documento `CU-03-Fijar-Y-Reemplazar-La-Credencial-Derivada.md` en su versión **1.0**, tomada el 2026-08-09 por el orquestador SDD antes de que la versión vigente la superara (`Master-Prompt.md` §5 y §5.1).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-09
> - **Versión vigente:** [`CU-03-Fijar-Y-Reemplazar-La-Credencial-Derivada.md`](../../CU-02003-Fijar-Y-Reemplazar-La-Credencial-Derivada.md)
>
> El cuerpo que sigue **no se modifica**: un registro que se corrige después deja de ser un registro. Este archivo no se renombra, no se reenlaza y no vuelve a tocarse.

---

# CU-03 — Fijar y reemplazar la credencial derivada del alumno

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** CU-03-Fijar-Y-Reemplazar-La-Credencial-Derivada.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-08
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-02`](../../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00002-Identidad-Propia-Del-Alumno-Sin-Correo.md) §1, §4 y §5; `00-Contexto/Vision-Producto.md` §9.1; `00-Contexto/Alcance-Producto.md` §5; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §17.1.P.5, §4 (F-04 y F-05), §6 (flujo 1), §9 (X-1 y X-2)
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
- [12. Compatibilidad de la superficie pública](#12-compatibilidad-de-la-superficie-pública)

---

## 1. Propósito

Fijar por primera vez la credencial derivada de un alumno habilitado, y reemplazarla más adelante, sin que el dominio conozca nunca la contraseña en claro. Es el contrato de uso que sostiene la promesa de que **ninguna credencial se transporta**: el alumno la establece en su primer ingreso efectivo y el producto no tiene canal de correo por el que enviarla.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Capa de casos de uso del producto (`GeometriaFactory-Application`) | Primario | Solicita fijar o reemplazar la credencial derivada del alumno |
| Capa de infraestructura (`GeometriaFactory-Infrastructure`) | Secundario | Deriva la contraseña antes de que el valor llegue al dominio y materializa el resultado |
| Modelo de dominio de `GeometriaFactory-Domain` | Sistema | Admite o rechaza la operación según el estado de la cuenta y la presencia previa de credencial |

El alumno es el sujeto de la regla. El dominio **no maneja secretos**: la contraseña llega ya derivada (PRODUCT-INTAKE §17.1.P.5).

## 3. Precondiciones

- El alumno existe y su estado de cuenta es `Habilitado`.
- El valor de credencial que se aporta ya está derivado; el dominio no recibe texto en claro.
- Para el reemplazo, el consumidor ya comprobó la credencial vigente contra la que el alumno presentó. Esa comparación exige el mecanismo de derivación y por eso vive en la infraestructura.

## 4. Flujo principal — fijación en el primer ingreso efectivo

1. La capa de aplicación solicita fijar la credencial derivada de un alumno.
2. El dominio comprueba que el estado de cuenta sea `Habilitado`.
3. El dominio comprueba que la credencial derivada no tenga todavía valor.
4. El dominio comprueba que el valor aportado no esté vacío.
5. El dominio fija la credencial derivada con el valor aportado.
6. El dominio devuelve el alumno, que a partir de acá admite acceso (CU-04).

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El alumno cambia su contraseña estando ya dentro del laboratorio | El dominio admite el reemplazo cuando la credencial derivada ya tiene valor y el estado es `Habilitado`, y exige que el consumidor declare que verificó la credencial vigente. El valor anterior se reemplaza y no se conserva historial: ninguna fuente declara historial de credenciales | Paso 5, con reemplazo en lugar de fijación |
| FA-02 | El administrador resuelve el caso del alumno que olvidó su contraseña | No hay recuperación en el dominio: la salida declarada es dar de baja la cuenta y volver a darla de alta (CU-02 y CU-01). Este caso de uso no ofrece un camino de restablecimiento | Termina el caso de uso sin efecto |

## 6. Excepciones y errores

| Código | Causa | Respuesta del dominio |
| --- | --- | --- |
| `CUENTA_NO_HABILITADA_PARA_CREDENCIAL` | El estado de cuenta es `Pendiente` o `Bloqueado` | Rechaza la operación y conserva la credencial derivada como estaba |
| `CREDENCIAL_YA_FIJADA` | Se solicita fijar por primera vez una credencial que ya tiene valor | Rechaza la operación: el camino correcto es el reemplazo de FA-01 |
| `CREDENCIAL_VIGENTE_NO_VERIFICADA` | Se solicita reemplazar sin que el consumidor declare la verificación de la credencial vigente | Rechaza la operación |
| `VALOR_DERIVADO_VACIO` | El valor de credencial aportado está vacío | Rechaza la operación |

Los cuatro rechazos dejan al alumno exactamente como estaba.

## 7. Postcondiciones

- **Éxito de la fijación:** el alumno tiene credencial derivada con valor y sigue en estado `Habilitado`.
- **Éxito del reemplazo:** el alumno tiene la credencial derivada nueva y ningún otro atributo cambió.
- **Fallo:** la credencial derivada conserva su valor anterior, tenga o no valor.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un alumno en estado `Habilitado` con credencial derivada sin valor | La capa de aplicación solicita fijar la credencial con un valor derivado no vacío | El dominio devuelve el alumno con credencial derivada con valor y estado `Habilitado` |
| CA-02 | Un alumno en estado `Pendiente` con credencial derivada sin valor | La capa de aplicación solicita fijar la credencial | El dominio rechaza con el código `CUENTA_NO_HABILITADA_PARA_CREDENCIAL` |
| CA-03 | Un alumno en estado `Habilitado` con credencial derivada ya fijada | La capa de aplicación solicita fijarla por primera vez | El dominio rechaza con el código `CREDENCIAL_YA_FIJADA` |
| CA-04 | Un alumno en estado `Habilitado` con credencial derivada ya fijada | La capa de aplicación solicita reemplazarla sin declarar la verificación de la vigente | El dominio rechaza con el código `CREDENCIAL_VIGENTE_NO_VERIFICADA` y 0 cambios de credencial se aplican |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-02 |
| Reglas de negocio aplicables | Ninguna RN declarada por el intake restringe específicamente este caso de uso. Lo restringen la máquina de estados de cuenta e INV-06, declarados en [`Definicion-Modelo-De-Dominio.md`](../Definicion-Modelo-De-Dominio.md) §4 y §5.1 |
| Invariantes | INV-06, por la condición de estado |
| Historias de usuario a generar en 06 | US de establecimiento de contraseña en el primer ingreso, US de cambio de contraseña exigiendo la vigente |
| Componentes esperados en 05 | Atributo de credencial derivada de la entidad de alumno, con su condición de estado |
| Tests previstos en 08 | Pruebas unitarias de fijación, de reemplazo y de los cuatro rechazos, sin dobles y sin infraestructura |

## 10. Notas y supuestos

- El dominio **no deriva** la contraseña ni la compara: la derivación de clave vive en `GeometriaFactory-Infrastructure` (PRODUCT-INTAKE §17.3.P.5). Este caso de uso modela la condición, no el mecanismo.
- No hay recuperación de contraseña olvidada, porque no hay canal de correo (`Alcance-Producto.md` §5, exclusiones X-1 y X-2).
- Ninguna credencial de sesión es observable desde el navegador: eso lo sostiene la pieza pública del producto y no este proyecto de código (`NB-02` §5, quinto criterio).

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. |

## 12. Compatibilidad de la superficie pública

La operación recibe un valor ya derivado y no un texto en claro. Cambiar esa premisa haría que el dominio maneje secretos y contradiría §17.1.P.5 del intake: sería un cambio incompatible y de alcance, no una evolución menor.
