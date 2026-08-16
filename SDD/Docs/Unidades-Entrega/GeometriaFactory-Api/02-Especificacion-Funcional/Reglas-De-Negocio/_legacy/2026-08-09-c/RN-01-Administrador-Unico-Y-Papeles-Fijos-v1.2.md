> **Artefacto archivado — estado `Superado`**
>
> Copia archivada de `RN-01-Administrador-Unico-Y-Papeles-Fijos.md` en su versión **1.2**, tomada el 2026-08-09 por el orquestador SDD **antes** de despachar la corrección, según `Master-Prompt.md` §8.
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.2
> - **Fecha de archivado:** 2026-08-09
> - **Versión vigente:** [`RN-01-Administrador-Unico-Y-Papeles-Fijos.md`](../../RN-02001-Administrador-Unico-Y-Papeles-Fijos.md)
>
> El cuerpo que sigue **no se modifica**. Este archivo no se renombra, no se reenlaza y no vuelve a tocarse.

---

# RN-01 — Administrador único y papeles fijos

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** RN-01-Administrador-Unico-Y-Papeles-Fijos.md
**Versión:** 1.2
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4.1 (enunciado de RN-01), §4 (F-01 y F-19), §9 (X-3), §17.1.P.2 (INV-05), §17.3.P.4; [`NB-01`](../../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00001-Control-De-Admision-Al-Laboratorio.md) §4 y §5; `00-Contexto/Alcance-Producto.md` §5
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

Existe **exactamente un** administrador, y su alta sólo es posible mientras no exista ninguno. El conjunto de papeles es cerrado y de dos valores: `Alumno` y `Administrador`. No hay permisos configurables ni papeles adicionales.

## 2. Justificación

Es una decisión de negocio del Product Owner, declarada como exclusión: el producto es deliberadamente básico y su modelo es de dos papeles fijos y un único administrador (PRODUCT-INTAKE §4.1, §9 X-3 y §4 F-19). El invariante **INV-05** la expresa como condición permanente: existe exactamente un administrador configurado y su alta sólo es posible mientras no exista ninguno (§17.1.P.2). Un segundo administrador configurado por error volvería ambiguo quién manda sobre la lista de la comisión (`NB-01` §4).

## 3. Ámbito de aplicación

- Se evalúa al constituir la cuenta con papel `Administrador`, que es **CU-12** y ocurre una sola vez en la vida de la instancia. La regla fija la unicidad y la ventana de alta —«sólo mientras no exista ninguno»—; **no fija el estado con el que esa cuenta nace**, que lo declara la máquina de estados de `Definicion-Modelo-De-Dominio.md` §5.1.
- Se evalúa al dar de baja una cuenta: la baja del administrador dejaría a la instancia sin él.
- Se evalúa cada vez que una capa consumidora deriva permisos: el papel es un valor de un conjunto cerrado y no una lista de permisos.
- El dominio hace cumplir lo que puede verificar sobre una entidad: la unicidad frente al conjunto de cuentas la ejerce `GeometriaFactory-Application`, porque el dominio no conoce ese conjunto.

## 4. Consecuencia si se viola

Rechazo. La constitución de una segunda cuenta con papel `Administrador` no procede, y la baja de la cuenta de administrador se rechaza con el código `CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA`. No hay compensación ni advertencia: la operación no se ejecuta.

## 5. CU afectados

- [CU-12](../../../Casos-De-Uso/CU-04010-Configurar-La-Cuenta-De-Administrador.md) — Configurar la cuenta de administrador en el primer arranque.
- [CU-01](../../../Casos-De-Uso/CU-02001-Registrar-El-Alta-De-Un-Alumno.md) — Registrar el alta de un alumno, **sólo** en cuanto al conjunto cerrado de dos papeles.
- [CU-02](../../../Casos-De-Uso/CU-02002-Gobernar-El-Ciclo-De-Vida-De-La-Cuenta.md) — Gobernar el ciclo de vida de la cuenta del alumno.
- [CU-04](../../../Casos-De-Uso/CU-02004-Evaluar-La-Admisibilidad-De-La-Cuenta.md) — Evaluar la admisibilidad de la cuenta, en cuanto al conjunto cerrado de papeles.

## 6. Pruebas que la verifican

Pruebas unitarias puras de dominio previstas en 08: constitución rechazada de una segunda cuenta de administrador cuando el consumidor declara que ya existe una; rechazo de la baja de la cuenta de administrador; y comprobación de que el conjunto de papeles admite exactamente 2 valores. El criterio de verificación declarado por el intake es que intentar la ruta de alta inicial con administrador existente redirija al ingreso (§4.1), y esa mitad pertenece a las pruebas de las capas que exponen la ruta. El criterio de éxito de negocio correspondiente es de `NB-01` §5: 0 cuentas de administrador admitidas cuando ya existe una.

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. |
| 1.1 | 2026-08-09 | Absorbe el enunciado completo que `PRODUCT-INTAKE` 1.3 §4.1 transcribe. Sube minor y archiva el estado anterior por `Master-Prompt.md` §5. El enunciado incorpora la condición de alta —«su alta sólo es posible mientras no exista ninguno»—, la cita de INV-05 pasa de §17.3.P.4 a §17.1.P.2, que es donde el intake ahora enuncia los siete invariantes, y §6 suma el criterio de verificación declarado por la fuente. |
| 1.2 | 2026-08-09 | **Corrección del P0** reportado por `B-02-03-GeometriaFactory-Application-r1.md`. Esta regla se citaba en CU-01 como fundamento del estado inicial `Pendiente` de toda cuenta, y **no dice eso**: declara que existe exactamente un administrador y que su alta sólo es posible mientras no exista ninguno. §3 lo precisa explícitamente y §5 reasigna el alta del administrador a **CU-12**, que se emite con este cambio, dejando a CU-01 sólo por el conjunto cerrado de dos papeles. |
