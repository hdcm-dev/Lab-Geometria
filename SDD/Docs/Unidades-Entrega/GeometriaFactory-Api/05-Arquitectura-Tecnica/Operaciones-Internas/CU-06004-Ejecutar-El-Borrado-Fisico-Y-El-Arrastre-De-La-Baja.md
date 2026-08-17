# CU-06004 — Ejecutar el borrado físico y el arrastre de la baja

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** CU-06004-Ejecutar-El-Borrado-Fisico-Y-El-Arrastre-De-La-Baja.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-00001`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00001-Control-De-Admision-Al-Laboratorio.md); [`NB-00003`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00003-Trabajo-Con-Dueno-Estado-Y-Persistencia.md); [`NB-00009`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00009-Desenlace-Explicito-De-La-Entrega.md); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.12** §4 (F-03, F-24), §4.1 (RN-06004, RN-06007), §7 (CL-6), §17.1.P.4 · GeometriaFactory-Infrastructure; implementa el retiro del puerto de repositorio de trabajos y del de cuentas de `Unidades-Entrega/GeometriaFactory-Api/02-Especificacion-Funcional/Especificacion-Funcional.md` §3, y materializa el arrastre que declara su [`CU-00023`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00023-Gobernar-Las-Cuentas-De-La-Comision.md)
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

Retirar del almacén, **físicamente**, un trabajo con todo lo que cuelga de él, y ejecutar la baja de una cuenta arrastrando **todos** sus trabajos en la misma unidad de trabajo.

Es la única operación destructiva del producto y la que no tiene vuelta atrás: no hay marca de borrado lógico, no hay papelera y no hay historial. Por eso este contrato existe separado del guardado: lo que hay que poder verificar es que **no queda nada**.

Lo que este caso de uso **no** hace: no comprueba quién pide el retiro ni desde qué estado procede —el alumno sólo en `Borrador`, el administrador en todo lo que ve—, y **no compara el correo escrito como confirmación**. Las tres comprobaciones son de la capa de aplicación y del dominio, y llegan resueltas.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Consumidor de los puertos de repositorio (`GeometriaFactory-Application`) | Primario | Pide el retiro de un trabajo, o la baja de una cuenta con su arrastre declarado |
| Almacén de datos | Sistema | El archivo único de SQLite |

El alumno y el administrador son sujetos de las reglas: uno pierde sus trabajos, el otro decide.

## 3. Precondiciones

- El consumidor ya resolvió la autorización y el estado: este contrato recibe un retiro **ya decidido**.
- En la baja de cuenta, el consumidor **declara el arrastre**: no se infiere acá. Es lo que hace que la ausencia de esa declaración sea una condición de §6 y no un valor por defecto.

## 4. Flujo principal

1. El consumidor pide el retiro, dentro de **una única unidad de trabajo**.
2. Si el retiro es de **un trabajo**, se retiran sus observaciones, sus componentes, sus piezas y la fila del trabajo. El borrado es **físico**: no queda fila marcada.
3. Si el retiro es la **baja de una cuenta**, se retiran **todos** sus trabajos —en cualquiera de los cuatro estados, con sus piezas, sus componentes, sus observaciones y su comentario— y después la cuenta.
4. La unidad de trabajo se cierra entera. **Si algo impide completarla, no se retira nada**: una baja a medias dejaría trabajos sin dueño, que es la forma más silenciosa de romper el modelo.
5. Se devuelve el resultado del retiro.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El trabajo pedido ya no está en el almacén | Se devuelve **nada retirado**. No es una condición de error: quién traduce eso —y con qué motivo, que no revela la existencia de un recurso ajeno— es el consumidor | Termina la operación |
| FA-02 | La cuenta que se da de baja no tiene ningún trabajo | Se retira la cuenta. El arrastre de cero trabajos es un arrastre válido | Paso 4 |
| FA-03 | La cuenta que se da de baja tiene trabajos en los cuatro estados | Se retiran los cuatro. **La baja no distingue estados**: `Finalizado` y `Rechazado` son terminales para las transiciones, no para el retiro | Paso 4 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del caso de uso |
| --- | --- | --- |
| `RETIRO_PARCIAL_NO_ADMITIDO` | Se pidió la baja de una cuenta **sin declarar el arrastre** de sus trabajos, o declarándolo sobre un subconjunto | Termina sin retirar nada. **La baja arrastra todo o no ocurre** (RN-06007). Aceptar un arrastre parcial dejaría trabajos sin dueño en el almacén, y el criterio con el que la regla se verifica es precisamente que **no quede ningún trabajo del alumno dado de baja** |
| `ALMACEN_NO_DISPONIBLE` | El archivo del almacén no está alcanzable | Termina de forma **degradada**, sin retirar nada. Tiene su entrada en `CU-06003` §6, con la misma causa |

## 7. Postcondiciones

- **Éxito en el retiro de un trabajo:** no queda en el almacén ninguna fila del trabajo, de sus piezas, de sus componentes ni de sus observaciones.
- **Éxito en la baja de una cuenta:** no queda la cuenta ni **ninguno** de sus trabajos, en ningún estado.
- **Fallo:** el almacén queda exactamente como estaba. **No hay retiro parcial en ningún camino.**

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un trabajo en estado `Pendiente` con 3 piezas, sus componentes y 2 observaciones | Se retira | No queda **ninguna** fila del trabajo, de sus piezas, de sus componentes ni de sus observaciones. El borrado es físico y no deja marca |
| CA-02 | Una cuenta de alumno con tres trabajos, uno en `Borrador`, uno en `Rechazado` —con comentario— y uno en `Finalizado` | Se da de baja con su arrastre declarado | No queda la cuenta ni **ninguno de los tres trabajos**, ni el comentario del rechazado. Es el criterio con el que RN-06007 se verifica |
| CA-03 | La misma cuenta | Se pide la baja **sin declarar el arrastre** | Devuelve `RETIRO_PARCIAL_NO_ADMITIDO` y **la cuenta y los tres trabajos siguen enteros** |
| CA-04 | Una cuenta sin ningún trabajo | Se da de baja con su arrastre declarado | La cuenta se retira y la operación es exitosa |
| CA-05 | Una baja de cuenta con tres trabajos, interrumpida después del primero | Se consulta el almacén | Están **los tres trabajos y la cuenta**: la unidad de trabajo no se cerró y no quedó retiro parcial |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-00001, NB-00003 y NB-00009 |
| Reglas de negocio aplicables | [RN-02007](../../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02007-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md) —en su mitad de arrastre; la confirmación escrita es del consumidor— y [RN-02004](../../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02004-Eliminacion-Acotada-Al-Borrador.md), en su mitad de **borrado físico** |
| Reglas conceptuales de modelo | [`RC-06005`](../../02-Especificacion-Funcional/Modelo-Datos/reglas-conceptuales-de-modelo/RC-06005-Retiro-Fisico-Con-Arrastre.md) |
| Puertos que implementa | Repositorio de trabajos y repositorio de cuentas, en su operación de retiro |
| Consumidor | `GeometriaFactory-Application`, sus CU-06002 y CU-06009 |
| Historias de usuario a generar en 06 | US-06012, US-06013 |
| Componentes esperados en 05 | Operación de retiro de los dos adaptadores de repositorio, con el alcance de la unidad de trabajo |
| Tests previstos en 08 | Pruebas de integración contra SQLite real: retiro de un trabajo con todo lo que cuelga, baja con arrastre en los cuatro estados, rechazo del arrastre no declarado y ausencia de retiro parcial |

## 10. Notas y supuestos

- **El reseteo de contraseña no pasa por acá y es deliberado.** Quien conserva la cuenta y **todos** sus trabajos es **RN-06012**, cuyo tramo en esta capa `Especificacion-Funcional.md` §6 asigna a `CU-06005` —que escribe la marca sin tocar el estado ni los trabajos— y a este `CU-06004` **por contraste**. El reseteo no dispara RN-06007, y separarlo del retiro es lo que impide que un olvido de contraseña cueste la cursada entera. Es la confusión que la capacidad de reseteo vino a cerrar y este contrato no la reintroduce.
- **La confirmación escrita del correo no se comprueba acá.** Es del consumidor, y llega resuelta. Este contrato no puede protegerse solo: si le piden retirar, retira.
- **El borrado del administrador alcanza cualquier estado que él ve.** Este contrato no distingue: quien acota el alcance es la capa de aplicación.
- **No hay borrado lógico y no se propone.** Agregar una marca de retirado cambiaría el modelo del producto y volvería falso el criterio con el que RN-06007 se verifica.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. |
| 1.1 | 2026-08-10 | Ronda 2 de auditoría: correcciones de `SDD/Docs/Audit/B-02-03-GeometriaFactory-Infrastructure-r1.md` contra el `PRODUCT-INTAKE` **1.12**. **H-05**: la primera nota de §10 atribuía a `CU-06007` la conservación de la cuenta y de todos sus trabajos, que el `CU-06007` local —«Producir la contraseña provisoria del reseteo»— no hace, porque no persiste, no toca la cuenta y no ve sus trabajos. La atribución pasa a **RN-06012**, cuyo tramo en esta capa `Especificacion-Funcional.md` §6 asigna a `CU-06005` y a este `CU-06004` por contraste. **H-02**: la trazabilidad upstream cita el `PRODUCT-INTAKE` **1.12**. |

## 17. Compatibilidad de la superficie pública

Agregar una operación de retiro para una entidad nueva es compatible. **Convertir el borrado físico en lógico, admitir un arrastre parcial o retirar la cuenta sin sus trabajos son cambios incompatibles** y suben versión mayor: los tres contradicen RN-06007 y el criterio con el que se verifica.
