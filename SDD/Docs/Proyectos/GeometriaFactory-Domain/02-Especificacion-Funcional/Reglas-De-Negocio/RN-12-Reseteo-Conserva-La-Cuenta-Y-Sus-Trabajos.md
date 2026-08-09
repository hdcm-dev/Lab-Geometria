# RN-12 — El reseteo de contraseña conserva la cuenta y sus trabajos

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** RN-12-Reseteo-Conserva-La-Cuenta-Y-Sus-Trabajos.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `PRODUCT-INTAKE-Fabrica-De-Geometria.md` 1.7 §4.1 (enunciado de **RN-12**), §4 (**F-26**), §17.1.P.2 (**INV-09**), §7 (**CL-7** reescrito), §9 (**X-2 retirada**), §11 (RN-B6), §4.2 (modelo de estados del trabajo); [`NB-01`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-01-Control-De-Admision-Al-Laboratorio.md) §1, §4 y §5; [`NB-02`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md) §1; `00-Contexto/Vision-Producto.md` §8 (RG-06)
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

El administrador fija una contraseña **provisoria** a una cuenta de alumno; la cuenta queda marcada como **con cambio de contraseña pendiente** y **conserva su estado de habilitación**, su papel, su identidad y **todos sus trabajos con sus estados y comentarios**. Resetear no es dar de baja: **no dispara RN-07**.

## 2. Justificación

Cierra un agujero de diseño y no agrega comodidad. Hasta `PRODUCT-INTAKE` 1.6 el único camino declarado ante una contraseña olvidada era dar de baja la cuenta y volver a darla de alta, y por RN-07 esa baja **elimina la cuenta y todos sus trabajos**: el primer olvido de contraseña costaba la cursada entera. El producto no tiene canal de correo y la recuperación autónoma sigue excluida por X-1, de modo que sin esta regla no había ninguna salida que conservara el trabajo del alumno.

Es la respuesta declarada del Product Owner al caso límite de la contraseña olvidada (§7, **CL-7** reescrito) y el motivo por el que la exclusión **X-2** queda retirada.

## 3. Ámbito de aplicación

- Se evalúa en todo reseteo de contraseña de una cuenta de alumno, **cualquiera sea su estado de cuenta**: el reseteo no es una transición de la máquina de estados de cuenta y no la exige `Habilitado`.
- Alcanza a los trabajos en los **cuatro** estados, incluidos los dos terminales y sus comentarios: el reseteo no toca ninguno.
- **No se aplica a la cuenta con papel `Administrador`**, sobre la que el reseteo no procede (CU-13 §6, RN-01, INV-08). El administrador cambia su propia contraseña por el reemplazo de CU-03.
- **Su invariante es INV-09**, que comparte con RN-13. Las dos son las dos mitades de la misma condición: ésta declara qué conserva el reseteo, y RN-13 qué no puede la cuenta hasta cambiar la provisoria (`Definicion-Modelo-De-Dominio.md` §4.3).
- El dominio **no conoce la contraseña provisoria**: la elige el administrador, la comunica por fuera del producto y llega ya derivada (PRODUCT-INTAKE §17.1.P.5).

## 4. Consecuencia si se viola

Rechazo. Un reseteo que declare eliminar los trabajos del alumno o cambiar su estado de cuenta se rechaza con el código `RESETEO_CON_ARRASTRE_DE_TRABAJOS`, y no se reemplaza ninguna credencial ni se pone ninguna marca. El daño que la regla evita es exactamente el que hacía inutilizable al laboratorio: perder trabajos ya aprobados por un olvido de contraseña.

## 5. CU afectados

- [CU-13](../Casos-De-Uso/CU-13-Resetear-La-Contrasena-De-Una-Cuenta-De-Alumno.md) — Resetear la contraseña de una cuenta de alumno, que es donde la regla se materializa.
- [CU-02](../Casos-De-Uso/CU-02-Gobernar-El-Ciclo-De-Vida-De-La-Cuenta.md) — **por contraste**: es el contrato donde vive la baja con arrastre de RN-07, y esta regla existe para que el reseteo no se confunda con ella.

## 6. Pruebas que la verifican

Pruebas unitarias de dominio previstas en 08: reseteo de una cuenta con trabajos en los cuatro estados verificando que **ninguno** se elimina y que los comentarios se conservan; reseteo sobre cuentas `Pendiente`, `Habilitado` y `Bloqueado` verificando que el estado no cambia; y rechazo del reseteo que declara arrastre. El dato de prueba que el intake declara para esta regla es el alumno con tres trabajos —uno en `Borrador`, uno en `Rechazado` y uno en `Finalizado`— que los conserva los tres después del reseteo (§4.1, columna de verificación).

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial, por la regla **RN-12** que `PRODUCT-INTAKE` 1.7 §4.1 transcribe junto con la capacidad **F-26**. Declara el enunciado, la justificación como cierre de un agujero de diseño con el retiro de **X-2** y la reescritura de **CL-7**, el ámbito sobre los tres estados de cuenta y los cuatro de trabajo, el cierre sobre la cuenta de administrador, la correspondencia con **INV-09** compartida con RN-13, y el código de rechazo con el que se verifica. |
