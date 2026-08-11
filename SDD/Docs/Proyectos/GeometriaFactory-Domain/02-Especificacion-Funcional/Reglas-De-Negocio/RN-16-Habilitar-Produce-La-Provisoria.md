# RN-16 — Habilitar una cuenta produce su contraseña provisoria

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** RN-16-Habilitar-Produce-La-Provisoria.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.15** §4.1 (enunciado de **RN-16** corregido, y RN-12, RN-13, RN-14 con las que forma circuito), §4 (**F-03**, **F-04** precisada, F-26), §17.1.P.2 (**INV-09**, INV-06, INV-08), §15 (etapa `d`), §7 (CL-7), §9 (X-1 vigente); [`NB-01`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-01-Control-De-Admision-Al-Laboratorio.md) §1 y §5; [`NB-02`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md) §1 y §5
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

**Habilitar una cuenta produce una contraseña provisoria**, con las mismas propiedades y el mismo tratamiento que la del reseteo (RN-14): el sistema la produce, la superficie se la muestra al administrador para que se la comunique, y la cuenta queda con **cambio de contraseña pendiente** (INV-09). En consecuencia **no existe ninguna escritura anónima de credencial**: toda operación que fija o cambia una contraseña ocurre con la cuenta ya autenticada. **Lo que la regla no toca es el registro de la cuenta**, que es anónimo por diseño y así debe seguir: es la puerta por la que el alumno entra al laboratorio (`CU-01`, F-02) [PRECISADO por `PRODUCT-INTAKE` **1.15** §4.1: la redacción de 1.13 decía «ninguna escritura anónima **en el sistema**», y era falsa].

## 2. Justificación

El producto tenía **dos** mecanismos de credencial inicial que hacían lo mismo. El primer ingreso del alumno recién habilitado fijaba una contraseña sin que nadie hubiera probado quién era; el reseteo la producía y obligaba a cambiarla. Con RN-16 son el mismo camino, y el que queda es el que ya estaba probado.

El defecto concreto que cierra lo levantó la emisión de la Fase B de `GeometriaFactory-Api`: establecer la contraseña del primer ingreso era **la única escritura de contraseña de la superficie que ocurría sin credencial**, y ninguna fuente declaraba cómo viajaba la identidad en esa operación. Un punto de acceso anónimo que aceptara un correo y una contraseña nueva le habría dejado fijar la contraseña a cualquiera que conociera un correo habilitado, **antes que su dueño**.

Lo que la regla **no** cambia es quién elige la contraseña definitiva. El alumno la sigue eligiendo: lo que cambia es que llega a elegirla **identificado**, por el camino que RN-13 ya fija, en lugar de llegar anónimo.

## 3. Ámbito de aplicación

- Se evalúa en **toda habilitación de una cuenta de alumno** (`CU-02`): la transición a `Habilitado` no se admite sin la credencial derivada provisoria aportada, y deja puesta la marca de cambio de contraseña pendiente.
- **Toda cuenta de alumno en estado `Habilitado` tiene credencial derivada con valor.** Es la consecuencia estructural de la regla y lo que retira del modelo la situación «habilitada y sin credencial», que hasta el intake 1.12 era la situación esperada del primer ingreso.
- **La marca la ponen dos actos y no uno**: la habilitación de `CU-02` y el reseteo de `CU-13`. La levanta, como antes, **únicamente** el reemplazo hecho por la propia cuenta (`CU-03` FA-04). Rehabilitar una cuenta `Bloqueado` es también una habilitación a estos efectos y produce provisoria nueva.
- **No alcanza a la cuenta de administrador**, que nace `Habilitado` con su credencial aportada en el mismo acto (`CU-12`) y a la que INV-08 le cierra las cuatro operaciones de `CU-02`.
- **Su invariante es INV-09**, que ya sostenía a RN-12 y RN-13 y que esta regla amplía en su origen: la marca deja de tener una sola fuente.
- **Lo que la regla no declara es el mecanismo.** Cómo se produce un valor no adivinable y no repetido es de RN-14 y de `GeometriaFactory-Infrastructure`; el dominio recibe la credencial **ya derivada** y nunca la contraseña en claro.

## 4. Consecuencia si se viola

Violarla tiene dos formas, y las dos son silenciosas.

La primera es **dejar habilitar sin credencial**: la cuenta queda en el estado que el producto declara admisible sin tener con qué autenticarse, y el único camino para darle una sería reponer el punto anónimo que la regla suprime. `CU-02` la cierra con un rechazo propio.

La segunda es **habilitar sin poner la marca**: la provisoria que el administrador conoce queda sirviendo indefinidamente para operar como el alumno, que es exactamente lo que INV-09 existe para impedir y lo que RN-13 declara para el reseteo. No produce ningún error; sólo una prueba lo detecta.

## 5. CU afectados

- [CU-02](../Casos-De-Uso/CU-02-Gobernar-El-Ciclo-De-Vida-De-La-Cuenta.md) — Gobernar el ciclo de vida de la cuenta, donde la regla se materializa: la habilitación exige la credencial derivada provisoria y deja la marca puesta.
- [CU-03](../Casos-De-Uso/CU-03-Fijar-Y-Reemplazar-La-Credencial-Derivada.md) — Fijar y reemplazar la credencial derivada: la fijación deja de ser un acto del alumno anónimo y pasa a ejercerse dentro de la habilitación; el alumno usa el **reemplazo**, que es el que levanta la marca.
- [CU-04](../Casos-De-Uso/CU-04-Evaluar-La-Admisibilidad-De-La-Cuenta.md) — Evaluar la admisibilidad, de donde la regla **retira** el motivo `CREDENCIAL_NO_ESTABLECIDA`: con la regla puesta, una cuenta `Habilitado` sin credencial no puede existir.
- [CU-13](../Casos-De-Uso/CU-13-Resetear-La-Contrasena-De-Una-Cuenta-De-Alumno.md) — Resetear la contraseña, de donde la regla **retira** el rechazo `RESETEO_SOBRE_CREDENCIAL_NO_FIJADA`, por el mismo motivo.

## 6. Pruebas que la verifican

Pruebas unitarias de dominio previstas en 08: habilitar una cuenta `Pendiente` aportando la credencial derivada provisoria y verificar que devuelve `Habilitado` **con la marca puesta**; habilitar sin aportarla y verificar el rechazo propio de `CU-02` §6, con la cuenta **sin cambio de estado**; rehabilitar una cuenta `Bloqueado` y verificar que también deja la marca; y la prueba negativa de que no existe ningún camino del dominio que lleve una cuenta de alumno a `Habilitado` sin credencial derivada. El dato de prueba que el intake declara para esta regla es de punta a punta (§4.1, columna de verificación): habilitar a un alumno y entrar con la provisoria lleva al cambio obligatorio y a ninguna otra ruta, y no hay ningún punto de acceso que acepte un correo y una contraseña nueva sin credencial; su lugar es 08 sobre el producto integrado y `GeometriaFactory-Api`.

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial, por la regla **RN-16** que `PRODUCT-INTAKE` **1.13** §4.1 incorpora al transcribir la decisión del Product Owner sobre la identificación de la cuenta en el primer ingreso, y por la precisión de **F-04** que la acompaña. Declara el enunciado, la justificación —dos mecanismos de credencial inicial que hacían lo mismo pasan a ser uno, y el que queda es el que ya estaba probado—, el ámbito con la consecuencia estructural de que **toda cuenta de alumno `Habilitado` tiene credencial** y con el origen doble de la marca de INV-09, la consecuencia en sus dos formas silenciosas, los cuatro CU afectados —dos que la materializan y dos de los que **retira** una condición— y las pruebas. (Analista Funcional + API Designer, AG-02). |
| 1.1 | 2026-08-10 | **Absorbe la corrección de `PRODUCT-INTAKE` 1.15 §4.1**, que precisa el enunciado de esta misma regla. La 1.13 cerraba RN-16 afirmando que «no existe ninguna escritura anónima **en el sistema**», y eso es falso: el **registro de cuenta** de RF-03 (F-02, `CU-01`) es anónimo por diseño y debe seguir siéndolo, porque es como el alumno entra al laboratorio. Lo que la regla elimina es la escritura anónima **de credencial**. **§1** transcribe el enunciado corregido y deja escrito qué queda fuera de su alcance. **§2** acota del mismo modo el defecto que la regla cierra: lo que era único era la escritura de **contraseña** sin credencial de la superficie, no toda escritura sin credencial. **Ninguna decisión cambia y ningún ámbito de aplicación, consecuencia ni CU afectado se modifica**: es la letra de la fuente, corregida. Sube minor. |
