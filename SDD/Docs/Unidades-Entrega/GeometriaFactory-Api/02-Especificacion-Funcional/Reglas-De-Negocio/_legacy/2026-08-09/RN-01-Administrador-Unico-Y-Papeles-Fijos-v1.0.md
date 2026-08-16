> **Artefacto archivado — estado `Superado`**
>
> Esta es una **copia archivada** del documento `RN-01-Administrador-Unico-Y-Papeles-Fijos.md` en su versión **1.0**, tomada el 2026-08-09 por el orquestador SDD antes de que la versión vigente la superara (`Master-Prompt.md` §5 y §5.1).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-09
> - **Versión vigente:** [`RN-01-Administrador-Unico-Y-Papeles-Fijos.md`](../../RN-02001-Administrador-Unico-Y-Papeles-Fijos.md)
>
> El cuerpo que sigue **no se modifica**: un registro que se corrige después deja de ser un registro. Este archivo no se renombra, no se reenlaza y no vuelve a tocarse.

---

# RN-01 — Administrador único y papeles fijos

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** RN-01-Administrador-Unico-Y-Papeles-Fijos.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-08
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §9 (X-3), §4 (F-01 y F-19), §17.3.P.4 (INV-05); [`NB-01`](../../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00001-Control-De-Admision-Al-Laboratorio.md) §4 y §5; `00-Contexto/Alcance-Producto.md` §5
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

Existe una sola cuenta con papel `Administrador` por instancia del producto, y el conjunto de papeles es cerrado y de dos valores: `Alumno` y `Administrador`. No hay permisos configurables ni papeles adicionales.

## 2. Justificación

Es una decisión de negocio del Product Owner, declarada como exclusión: el producto es deliberadamente básico y su modelo es de dos papeles fijos y un único administrador (PRODUCT-INTAKE §9, X-3, y §4, F-19). El invariante INV-05 la expresa además como propiedad de la instancia: un curso, un administrador (§17.3.P.4). Un segundo administrador configurado por error volvería ambiguo quién manda sobre la lista de la comisión (`NB-01` §4).

## 3. Ámbito de aplicación

- Se evalúa al constituir una cuenta con papel `Administrador`.
- Se evalúa al dar de baja una cuenta: la baja del administrador dejaría a la instancia sin él.
- Se evalúa cada vez que una capa consumidora deriva permisos: el papel es un valor de un conjunto cerrado y no una lista de permisos.
- El dominio hace cumplir lo que puede verificar sobre una entidad: la unicidad frente al conjunto de cuentas la ejerce `GeometriaFactory-Application`, porque el dominio no conoce ese conjunto.

## 4. Consecuencia si se viola

Rechazo. La constitución de una segunda cuenta con papel `Administrador` no procede, y la baja de la cuenta de administrador se rechaza con el código `CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA`. No hay compensación ni advertencia: la operación no se ejecuta.

## 5. CU afectados

- [CU-01](../../../Casos-De-Uso/CU-02001-Registrar-El-Alta-De-Un-Alumno.md) — Registrar el alta de un alumno.
- [CU-02](../../../Casos-De-Uso/CU-02002-Gobernar-El-Ciclo-De-Vida-De-La-Cuenta.md) — Gobernar el ciclo de vida de la cuenta del alumno.
- [CU-04](../../../Casos-De-Uso/CU-02004-Evaluar-La-Admisibilidad-De-La-Cuenta.md) — Evaluar la admisibilidad de la cuenta, en cuanto al conjunto cerrado de papeles.

## 6. Pruebas que la verifican

Pruebas unitarias puras de dominio previstas en 08: constitución rechazada de una segunda cuenta de administrador cuando el consumidor declara que ya existe una; rechazo de la baja de la cuenta de administrador; y comprobación de que el conjunto de papeles admite exactamente 2 valores. El criterio de éxito de negocio correspondiente es de `NB-01` §5: 0 cuentas de administrador admitidas cuando ya existe una.

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. |
