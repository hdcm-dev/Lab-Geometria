# RN-07 — La baja de una cuenta arrastra sus trabajos y exige confirmación escrita

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** RN-07-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md
**Versión:** 1.3
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `PRODUCT-INTAKE-Fabrica-De-Geometria.md` 1.7 §4.1 (enunciado de RN-07, reglas sin invariante, y **RN-12**), §4 (F-03, **F-26**), §4.2 (modelo de estados del trabajo), §7 (CL-6, **CL-7** reescrito), §9 (**X-2 retirada**), §11 (**RN-B6 tachado** el 2026-08-09; lo que sostenía vive en §7 CL-6), §17.1.P.2; [`NB-01`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-01-Control-De-Admision-Al-Laboratorio.md) §2, §4 y §5; `00-Contexto/Vision-Producto.md` §8 (RG-06)
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

Es la respuesta declarada del cliente al caso límite de la eliminación de una cuenta y de sus datos (PRODUCT-INTAKE §7, CL-6). La baja **fue** además, hasta `PRODUCT-INTAKE` 1.6, la única salida disponible ante una contraseña olvidada, de modo que era una operación frecuente y destructiva a la vez: por eso el producto tiene que hacerla difícil de ejecutar por accidente (`NB-01` §1 y §4). **Ese motivo ya no vale y la exigencia sigue en pie.** El intake 1.7 incorpora la capacidad **F-26**, retira la exclusión **X-2** y reescribe el caso límite **CL-7** sobre el reseteo de contraseña, que conserva la cuenta y todos sus trabajos (**RN-12**, CU-13). La baja deja de ser frecuente por ese motivo y sigue siendo irreversible: la confirmación escrita la protege de un accidente, no de un olvido de contraseña. La pérdida de los trabajos está declarada como riesgo residual aceptado (`Vision-Producto.md` §8, RG-06).

## 3. Ámbito de aplicación

- Se evalúa en toda baja de una cuenta de alumno, cualquiera sea su estado de cuenta.
- El arrastre de los trabajos es parte de la operación y no un efecto posterior: no existe una baja que deje trabajos sin dueño. Alcanza a los cuatro estados del trabajo, **incluidos los dos terminales**: la terminalidad de `Finalizado` y `Rechazado` impide que el trabajo cambie de estado o de contenido (INV-07), no que la baja de su dueño lo arrastre.
- **Esta regla no tiene invariante asociado**, y el intake lo declara explícitamente: describe un comportamiento y no una condición permanente sobre el estado (§17.1.P.2).
- La confirmación escrita del correo la recoge la pieza pública del producto; el dominio **exige** que la operación llegue declarada como confirmada, y esa exigencia es la que esta regla fija.
- No se aplica a la cuenta con papel `Administrador`, cuya baja rechaza RN-01.
- **El reseteo de contraseña no dispara esta regla**, y el intake lo dice con todas las letras: «resetear no es dar de baja» (**RN-12**). Un reseteo conserva la cuenta, su estado, su papel y todos sus trabajos con sus estados y comentarios. Confundir las dos operaciones es exactamente el defecto que F-26 viene a cerrar.

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
| 1.1 | 2026-08-09 | Absorbe el enunciado que `PRODUCT-INTAKE` 1.3 §4.1 transcribe y el modelo de estados de §4.2. Sube minor y archiva el estado anterior por `Master-Prompt.md` §5. §3 precisa que el arrastre alcanza a los cuatro estados del trabajo, incluidos los dos terminales que el modelo nuevo introduce, y distingue ese arrastre de INV-07; y declara que **esta regla no tiene invariante asociado**, según §17.1.P.2. |
| 1.2 | 2026-08-09 | Absorbe `PRODUCT-INTAKE` **1.7**. **El enunciado de la regla no cambia**; lo que cambia es una premisa de su justificación que quedó falsa: §2 declaraba que la baja era «la única salida disponible ante una contraseña olvidada», y el intake incorpora **F-26**, retira **X-2** y reescribe **CL-7** sobre el reseteo, que conserva la cuenta y sus trabajos (**RN-12**, CU-13). Se reescribe ese párrafo dejando en pie la exigencia de confirmación escrita, cuyo fundamento es la irreversibilidad y no la frecuencia. §3 suma que **el reseteo no dispara esta regla**. |
| 1.3 | 2026-08-10 | **Cierra la parte del hallazgo `N-5`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r2.md` 1.0 que alcanza a este archivo. La **trazabilidad de cabecera** citaba «§11 (RN-B6)» como riesgo vigente; el intake **1.10** lo tachó el 2026-08-09 al quedar sin objeto su mitigación, porque F-26 conserva la cuenta y sus trabajos. La cita se conserva con la constancia de que está tachada y remite a **§7 CL-6**, que es donde vive hoy lo que sostenía. **El enunciado de esta regla, su ámbito, su verificación y su relación con el reseteo no cambian**: la baja sigue arrastrando los trabajos y sigue exigiendo confirmación escrita. Sube minor: corrige una referencia a una fila retirada. |
