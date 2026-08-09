> **Artefacto archivado — estado `Superado`**
>
> Esta es una **copia archivada** del documento `RN-07-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md` en su versión **1.0**, tomada el 2026-08-09 por el orquestador SDD antes de que la versión vigente la superara (`Master-Prompt.md` §5 y §5.1).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-09
> - **Versión vigente:** [`RN-07-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md`](../../RN-07-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md)
>
> El cuerpo que sigue **no se modifica**: un registro que se corrige después deja de ser un registro. Este archivo no se renombra, no se reenlaza y no vuelve a tocarse.

---

# RN-07 — La baja de una cuenta arrastra sus trabajos y exige confirmación escrita

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** RN-07-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-08
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §7 (CL-6), §4 (F-03), §11 (RN-B6); [`NB-01`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-01-Control-De-Admision-Al-Laboratorio.md) §2, §4 y §5; `00-Contexto/Vision-Producto.md` §8 (RG-06)
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

La baja de una cuenta de alumno elimina la cuenta **y todos sus trabajos**, es irreversible, y sólo procede cuando el administrador confirmó la operación escribiendo el correo de esa cuenta.

## 2. Justificación

Es la respuesta declarada del cliente al caso límite de la eliminación de una cuenta y de sus datos (PRODUCT-INTAKE §7, CL-6). La baja es además la única salida disponible ante una contraseña olvidada, porque el producto no tiene canal de correo, de modo que es una operación frecuente y destructiva a la vez: por eso el producto tiene que hacerla difícil de ejecutar por accidente (`NB-01` §1 y §4). La pérdida de los trabajos está declarada como riesgo residual aceptado (`Vision-Producto.md` §8, RG-06).

## 3. Ámbito de aplicación

- Se evalúa en toda baja de una cuenta de alumno, cualquiera sea su estado de cuenta.
- El arrastre de los trabajos es parte de la operación y no un efecto posterior: no existe una baja que deje trabajos sin dueño.
- La confirmación escrita del correo la recoge la pieza pública del producto; el dominio **exige** que la operación llegue declarada como confirmada, y esa exigencia es la que esta regla fija.
- No se aplica a la cuenta con papel `Administrador`, cuya baja rechaza RN-01.

## 4. Consecuencia si se viola

Rechazo. Una baja que declare conservar los trabajos se rechaza con el código `BAJA_SIN_ARRASTRE_DE_TRABAJOS`, y una baja que llegue sin la confirmación escrita no procede. En ninguno de los dos casos se elimina nada.

## 5. CU afectados

- [CU-02](../Casos-De-Uso/CU-02-Gobernar-El-Ciclo-De-Vida-De-La-Cuenta.md) — Gobernar el ciclo de vida de la cuenta del alumno, en su flujo alternativo de baja.

## 6. Pruebas que la verifican

Pruebas unitarias de dominio previstas en 08: rechazo de la baja que declara conservar los trabajos; rechazo de la baja sin confirmación declarada; y baja admitida de una cuenta con trabajos, verificando que la operación los incluye. Los criterios de éxito de negocio son de `NB-01` §5: 0 bajas ejecutadas sin que el administrador escriba el correo de la cuenta, y 100 % de las confirmaciones declarando que se eliminan también los trabajos.

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. |
