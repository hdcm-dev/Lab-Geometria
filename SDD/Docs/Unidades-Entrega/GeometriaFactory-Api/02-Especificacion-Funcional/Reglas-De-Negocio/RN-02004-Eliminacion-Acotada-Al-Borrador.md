# RN-02004 — El alumno elimina sólo en borrador; el administrador, cualquier trabajo que ve

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** RN-02004-Eliminacion-Acotada-Al-Borrador.md
**Versión:** 1.2
**Estado:** Aprobado
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4.1 (enunciado de RN-02004), §4 (F-07 y F-24), §4.2 (tabla de quién puede qué en cada estado), §17.1.P.2 · GeometriaFactory-Domain (INV-03), §17.1.P.6 · GeometriaFactory-Api; [`NB-00003`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00003-Trabajo-Con-Dueno-Estado-Y-Persistencia.md) §4 y §5; [`NB-00009`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00009-Desenlace-Explicito-De-La-Entrega.md) §5; `00-Contexto/Alcance-Producto.md` §4.1
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

El alumno elimina sus trabajos **sólo en estado `Borrador`**. El administrador elimina **cualquier trabajo que ve**, en cualquier estado, con borrado físico.

## 2. Justificación

Las dos mitades son decisión declarada del Product Owner (PRODUCT-INTAKE §4.1, con la ampliación del 2026-08-08). La primera protege la entrega: un trabajo que el alumno ya envió salió de sus manos, y borrarlo dejaría a la revisión y a la métrica de cierre del circuito sin sustento (`NB-00003` §4). La segunda resuelve el residuo que introduce la terminalidad: un alumno que rebota varias veces acumula trabajos rechazados, y alguien tiene que poder limpiarlos (`NB-00009` §1).

El invariante que la expresa como condición permanente es **INV-03**, deliberadamente acotado: «un trabajo eliminado por un alumno estaba en `Borrador` y le pertenecía». Sin ese recorte, el borrado del administrador —que alcanza cualquier estado— volvería falso el enunciado.

## 3. Ámbito de aplicación

- Se evalúa cada vez que se consulta si una eliminación procede, y el resultado depende del papel de quien la pide.
- Para el alumno alcanza también a la **reedición**: el borrador es el único estado que edita (PRODUCT-INTAKE §4.2).
- Para el administrador alcanza a los tres estados que ve —`Pendiente`, `Finalizado` y `Rechazado`— y **no** a `Borrador`, que está fuera de su alcance por RN-02011.
- Se evalúa también cuando la solicitud llega forzando la petición al servicio de datos y no desde la pantalla: la regla es del dominio y no de la interfaz.
- No se evalúa en la baja de una cuenta, que arrastra los trabajos del alumno cualquiera sea su estado y cuya regla es RN-02007.

## 4. Consecuencia si se viola

Rechazo. Para el alumno, con el motivo `OPERATION_OUTSIDE_DRAFT`; para el administrador, con el motivo `WORK_OUTSIDE_ADMINISTRATOR_SCOPE` cuando el trabajo está en `Borrador`. En los dos casos el trabajo queda intacto, con su estado, su texto original, sus piezas y sus observaciones.

## 5. CU afectados

- [CU-00028](../Casos-De-Uso/CU-00028-Consultar-El-Listado-Y-El-Detalle-De-Los-Trabajos.md) — Resolver el acceso de un alumno a un trabajo.
- [CU-00028](../Casos-De-Uso/CU-00028-Consultar-El-Listado-Y-El-Detalle-De-Los-Trabajos.md) — Resolver el alcance del administrador sobre un trabajo.
- [CU-00026](../Casos-De-Uso/CU-00026-Enviar-Un-Trabajo-Y-Ver-Sus-Observaciones.md) — Crear y reeditar un trabajo, en cuanto al único estado que el alumno edita.
- [CU-00026](../Casos-De-Uso/CU-00026-Enviar-Un-Trabajo-Y-Ver-Sus-Observaciones.md) — Gobernar el estado del trabajo en el envío, en cuanto al estado desde el que el alumno opera.

## 6. Pruebas que la verifican

Pruebas unitarias de dominio previstas en 08: eliminación por el alumno admitida en `Borrador` y rechazada en `Pendiente`, en `Finalizado` y en `Rechazado`; eliminación por el administrador admitida en los tres estados que ve y rechazada en `Borrador`. El criterio bloqueante de verificarlo **forzando la petición** pertenece a las pruebas de integración de `GeometriaFactory-Api` (PRODUCT-INTAKE §17.1.P.6 · GeometriaFactory-Api), que además debe ejercer el borrado del administrador sobre un trabajo en estado `Pendiente`. Los criterios de éxito de negocio son de `NB-00003` §5 —0 eliminaciones del alumno fuera de `Borrador`— y de `NB-00009` §5 —3 de 3 estados en los que el administrador puede eliminar—.

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. |
| 1.1 | 2026-08-09 | Absorbe el enunciado completo que `PRODUCT-INTAKE` 1.3 §4.1 transcribe y la ampliación del 2026-08-08. Sube minor y archiva el estado anterior por `Master-Prompt.md` §5. La regla pasa de una sola mitad —el alumno elimina sólo en `Borrador`— a **las dos**, con el borrado físico del administrador en cualquier estado que ve. Se declara **INV-03** como el invariante que la expresa, con su recorte a la eliminación por parte de un alumno y el motivo de ese recorte. El ámbito suma la reedición y la exclusión de `Borrador` del alcance del administrador por RN-02011, y §5 suma CU-02011. **El nombre del archivo se conserva** aunque el enunciado se amplió: otras categorías ya lo citan por esta ruta y renombrarlo rompería sus enlaces. |
| 1.2 | 2026-08-29 | **Tramo `R-3b` del renombre `F-03`**, reactivado por el Product Owner el 2026-08-29 y registrado en [`../../../../Producto/Norma-De-Nomenclatura.md`](../../../../Producto/Norma-De-Nomenclatura.md) §8. **1 línea(s)** de este documento pasan los códigos de condición de la forma castellana a la vigente, con el mapeo de **§6.8** —101 pares— y **sin elegir ninguno acá**. Se respeta **§4.1**: no se tocan las filas de control de cambios ni lo que está entre «…». **Ninguna palabra de prosa cambia**, verificado con el control de diff del tramo. |
