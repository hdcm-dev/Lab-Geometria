# RN-02011 — El administrador no ve los trabajos en borrador

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** RN-02011-El-Administrador-No-Ve-Los-Borradores.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4.1 (enunciado de RN-02011), §4 (F-12), §4.2 (tabla de quién puede qué en cada estado), §17.1.P.2 · GeometriaFactory-Domain (las reglas sin invariante asociado), §6 (flujo 2.1); [`NB-00007`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00007-Revision-De-La-Comision-En-Un-Solo-Lugar.md) §5; [`NB-00009`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00009-Desenlace-Explicito-De-La-Entrega.md) §5
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

Un trabajo en estado `Borrador` está fuera del alcance del administrador: no forma parte de su flujo de trabajo, ni para verlo ni para operar sobre él.

## 2. Justificación

`Borrador` significa exactamente que el texto todavía no verifica, o que el trabajo recién se creó: es material en curso del alumno y no una entrega (PRODUCT-INTAKE §4.2). Mostrarlo llenaría la revisión de la comisión con intentos a medio hacer y le quitaría sentido al listado, que existe para que el docente recorra la entrega de una sola vez (`NB-00007` §5). La regla es además la que hace coherente a RN-02004: el administrador elimina «cualquier trabajo que ve», y los borradores no lo son.

**Esta regla no tiene invariante asociado**, y el intake lo declara explícitamente: es una regla de alcance de consulta y no una condición permanente sobre los datos (§17.1.P.2 · GeometriaFactory-Domain).

## 3. Ámbito de aplicación

- Se evalúa cada vez que se resuelve si un trabajo entra en el alcance del administrador, sea para verlo, para darle desenlace o para eliminarlo.
- No restringe al alumno, que sí ve y opera sus propios borradores.
- **El dominio no ejecuta la consulta.** No conoce el conjunto de trabajos: lo que declara es el predicado que decide, trabajo por trabajo, si está dentro del alcance. La consulta que lo aplica sobre el conjunto vive en `GeometriaFactory-Application` y en `GeometriaFactory-Infrastructure`, y el listado que la muestra en la pieza pública del producto.

## 4. Consecuencia si se viola

Exclusión, no rechazo con explicación: el trabajo en `Borrador` simplemente no está en el alcance. Cuando se consulta explícitamente sobre uno, el dominio devuelve no procede con el motivo `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR`. Un listado del administrador que incluya un borrador viola la regla aunque no se opere sobre él.

## 5. CU afectados

- [CU-00028](../Casos-De-Uso/CU-00028-Consultar-El-Listado-Y-El-Detalle-De-Los-Trabajos.md) — Resolver el alcance del administrador sobre un trabajo.
- [CU-00029](../Casos-De-Uso/CU-00029-Dar-Desenlace-A-La-Revision.md) — Resolver el desenlace del trabajo, que por esta regla nunca alcanza a un borrador.

## 6. Pruebas que la verifican

Pruebas unitarias de dominio previstas en 08: el predicado de alcance devuelve falso para un trabajo en `Borrador` y verdadero para los tres estados restantes. El criterio de verificación declarado por el intake es que el listado del administrador sobre un alumno con un borrador y un trabajo en estado `Pendiente` devuelva **sólo el segundo** (§4.1), y esa comprobación pertenece a la capa que hace la consulta y a las pruebas de integración de `GeometriaFactory-Api`. El criterio de éxito de negocio es de `NB-00007` §5, en su alcance de la vista del administrador.

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. Regla nueva del circuito de revisión que el Product Owner incorporó el 2026-08-08 y que `PRODUCT-INTAKE` 1.3 §4.1 declara. No existía en la fuente funcional original y no tiene invariante asociado, según §17.1.P.2 · GeometriaFactory-Domain. |
