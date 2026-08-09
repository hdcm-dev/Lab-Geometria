# RN-02 — El correo del alumno es único

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** RN-02-Correo-Del-Alumno-Unico.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4.1 (enunciado de RN-02), §4 (F-02), §17.1.P.2 (INV-01), §6 (flujo 1), §7 (CL-6 y CL-7); [`NB-02`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md) §1, §4 y §5; `00-Contexto/Vision-Producto.md` §9.1
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

El correo del alumno es único en todo el sistema: dos cuentas no comparten correo en ningún momento.

## 2. Justificación

El correo es lo único que identifica a la persona dentro del laboratorio, porque el producto no tiene canal de correo y no hay ningún otro dato con el que distinguir dos cuentas (PRODUCT-INTAKE §4.1). De esa unicidad dependen tres cosas declaradas: que el ingreso resuelva a una sola cuenta, que la atribución de un trabajo a su dueño sea inequívoca, y que la confirmación escrita del correo antes de una baja identifique exactamente a la cuenta que se va a eliminar (RN-07).

El invariante que la expresa como condición permanente es **INV-01**.

## 3. Ámbito de aplicación

- Se evalúa en el alta de toda cuenta, de alumno y de administrador.
- Se evalúa también cuando una cuenta dada de baja vuelve a darse de alta con el mismo correo: la baja es física, de modo que el correo queda libre y el alta procede.
- **El dominio no la puede verificar solo.** La unicidad se afirma sobre el conjunto de alumnos, y una entidad no conoce a ese conjunto: el dominio exige que el consumidor declare la comprobación hecha, y quien la ejerce es `GeometriaFactory-Application` con el puerto de repositorio. Es la misma frontera que el intake declara para todo lo que exige consultar el conjunto (§17.1.P.4).
- El criterio con el que dos correos se consideran el mismo —si se comparan tal cual o normalizados— es una decisión de 05: el dominio conserva el dato como lo recibe.

## 4. Consecuencia si se viola

Rechazo del alta, con mensaje explícito hacia la persona que se registra: el intake declara que registrar dos veces el mismo correo «se rechaza con mensaje explícito» (§4.1). En el dominio, el alta invocada sin la comprobación declarada se rechaza con el código `UNICIDAD_DE_CORREO_NO_VERIFICADA` y no se constituye ninguna entidad.

## 5. CU afectados

- [CU-01](../Casos-De-Uso/CU-01-Registrar-El-Alta-De-Un-Alumno.md) — Registrar el alta de un alumno.
- [CU-12](../Casos-De-Uso/CU-12-Configurar-La-Cuenta-De-Administrador.md) — Configurar la cuenta de administrador en el primer arranque, que es el otro camino de alta y también evalúa esta regla.

## 6. Pruebas que la verifican

Pruebas unitarias de dominio previstas en 08: el alta invocada sin declarar la comprobación de unicidad se rechaza. La verificación de la unicidad efectiva sobre el conjunto —registrar dos veces `ana@example.com` y recibir un rechazo explícito— pertenece a las pruebas de `GeometriaFactory-Application` y a las de integración de `GeometriaFactory-Api`, que son las capas que conocen el conjunto. El criterio de verificación declarado por el intake es exactamente ese registro repetido (§4.1).

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. La regla existía en la fuente funcional pero `PRODUCT-INTAKE` no transcribía su enunciado, y esta categoría la había elevado como ambigüedad en su versión anterior en lugar de inventarla. El intake 1.3 §4.1 la transcribe y §17.1.P.2 declara INV-01 como el invariante que la expresa. |
| 1.1 | 2026-08-09 | Alcanzada por la **corrección del P0** reportado por `B-02-03-GeometriaFactory-Application-r1.md`. §3 ya declaraba que la regla se evalúa «en el alta de toda cuenta, de alumno y de administrador», pero §5 listaba un solo caso de uso porque el alta del administrador no tenía el suyo. Con **CU-12** emitido, §5 lo suma y la trazabilidad regla → caso de uso vuelve a ser bidireccional. |
