# RN-02006 — Una cuenta `Pendiente` o `Bloqueado` no obtiene acceso

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** RN-02006-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4.1 (enunciado de RN-02006), §4 (F-03 y F-04), §17.1.P.2 · GeometriaFactory-Domain (INV-06), §17.1.P.5 · GeometriaFactory-Domain, §17.1.P.5 · GeometriaFactory-Api, §6 (flujo 1); [`NB-00001`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00001-Control-De-Admision-Al-Laboratorio.md) §5; [`NB-00002`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00002-Identidad-Propia-Del-Alumno-Sin-Correo.md) §2 y §5; `00-Contexto/Vision-Producto.md` §9.2
**Trazabilidad downstream:** `05-Arquitectura-Tecnica` y `06-Backlog-Tecnico` de GeometriaFactory-Domain; `08-Calidad-Y-Pruebas`

---

## Tabla de contenido

- [1. Enunciado de la regla](#1-enunciado-de-la-regla)
- [2. Justificación](#2-justificación)
- [3. Ámbito de aplicación](#3-ámbito-de-aplicación)
- [4. Consecuencia si se viola](#4-consecuencia-si-se-viola)
- [5. CU afectados](#5-cu-afectados)
- [6. Pruebas que la verifican](#6-pruebas-que-la-verifican)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Enunciado de la regla

Una cuenta `Pendiente` o `Bloqueado` no obtiene acceso al laboratorio, y el motivo por el que no lo obtiene se le informa a la persona.

## 2. Justificación

La admisión al laboratorio es un acto explícito del administrador, porque no hay canal de correo con el que autorizar altas (PRODUCT-INTAKE §4.1 y §9, X-1). Sin esta regla, habilitar y bloquear no tendrían efecto y el control de admisión quedaría en la interfaz. La segunda mitad —que el motivo se informe— es lo que evita que el alumno no habilitado quede pensando que se equivocó de credencial o de dirección (`NB-00002` §2 y §5).

El invariante que la expresa como condición permanente es **INV-06**.

## 3. Ámbito de aplicación

- Se evalúa en cada intento de ingreso, antes de emitir cualquier acceso.
- Se evalúa sobre el estado de la cuenta y no sobre el papel: alcanza por igual al alumno y al administrador.
- Alcanza también a la fijación de la credencial derivada, que sólo procede estando `Habilitado`: una cuenta que no obtiene acceso tampoco llega a tener credencial útil.
- **El dominio modela la condición, no el mecanismo.** La emisión efectiva del acceso y su vigencia viven en `GeometriaFactory-Infrastructure` y en `GeometriaFactory-Api`; lo que el dominio aporta es el resultado de admisibilidad con su motivo.

## 4. Consecuencia si se viola

Rechazo del ingreso. El dominio devuelve no admisible con el motivo `ACCOUNT_PENDING` o `ACCOUNT_BLOCKED`, y el consumidor lo traduce en un aviso explícito, distinto del rechazo genérico por credencial inválida (PRODUCT-INTAKE §17.1.P.5 · GeometriaFactory-Api). Ningún acceso se emite.

## 5. CU afectados

- [CU-00022](../Casos-De-Uso/CU-00022-Ingresar-Al-Laboratorio-Y-Sostener-La-Sesion.md) — Evaluar la admisibilidad de la cuenta para acceder al laboratorio.
- [CU-00022](../Casos-De-Uso/CU-00022-Ingresar-Al-Laboratorio-Y-Sostener-La-Sesion.md) — Fijar y reemplazar la credencial derivada, que exige la cuenta `Habilitado`.
- [CU-00023](../Casos-De-Uso/CU-00023-Gobernar-Las-Cuentas-De-La-Comision.md) — Gobernar el ciclo de vida de la cuenta, que es donde el estado cambia.

## 6. Pruebas que la verifican

Pruebas unitarias de dominio previstas en 08: la evaluación de admisibilidad devuelve no admisible con su motivo para una cuenta `Pendiente` y para una `Bloqueado`, y admisible para una `Habilitado` con credencial derivada fijada. El criterio de verificación declarado por el intake es intentar ingresar con una cuenta recién registrada y recibir el motivo sin obtener sesión (§4.1). El criterio de éxito de negocio es de `NB-00002` §5: 100 % de los intentos de una cuenta `Pendiente` reciben aviso explícito.

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.1 | 2026-08-29 | **Tramo `R-3b` del renombre `F-03`**, reactivado por el Product Owner el 2026-08-29 y registrado en [`../../../../Producto/Norma-De-Nomenclatura.md`](../../../../Producto/Norma-De-Nomenclatura.md) §8. **1 línea(s)** de este documento pasan los códigos de condición de la forma castellana a la vigente, con el mapeo de **§6.8** —101 pares— y **sin elegir ninguno acá**. Se respeta **§4.1**: no se tocan las filas de control de cambios ni lo que está entre «…». **Ninguna palabra de prosa cambia**, verificado con el control de diff del tramo. |
| 1.0 | 2026-08-09 | Emisión inicial. La regla existía en la fuente funcional pero `PRODUCT-INTAKE` no transcribía su enunciado, y esta categoría la había elevado como ambigüedad en su versión anterior en lugar de inventarla. El intake 1.3 §4.1 la transcribe y §17.1.P.2 · GeometriaFactory-Domain declara INV-06 como el invariante que la expresa. El enunciado se redacta con «acceso» y no con el nombre del mecanismo de sesión, que es vocabulario de otro proyecto de código. |
