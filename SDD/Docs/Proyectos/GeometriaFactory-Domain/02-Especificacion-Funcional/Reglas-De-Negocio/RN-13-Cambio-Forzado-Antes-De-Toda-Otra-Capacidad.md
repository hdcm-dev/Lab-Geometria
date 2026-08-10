# RN-13 — Con la contraseña provisoria sin cambiar, la cuenta no llega a ninguna otra parte

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** RN-13-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md
**Versión:** 1.2
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.13** §4.1 (enunciado precisado de **RN-13**, y enunciados de RN-12 y **RN-16**), §4 (**F-26**, F-03, **F-04** precisada), §17.1.P.2 (**INV-09**, INV-06), §17.1.P.5, §7 (**CL-7** reescrito), §9 (X-1 vigente, **X-2 retirada**); [`NB-02`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md) §1 y §5; `00-Contexto/Vision-Producto.md` §9.2
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

Mientras la contraseña provisoria no se cambie, la cuenta **no llega a ninguna otra parte del sistema**: **se autentica pero no obtiene sesión de trabajo** —el sistema reconoce la credencial provisoria y, en lugar de admitirla, la deriva al cambio de contraseña—, lo único que puede hacer es cambiarla, y cualquier otra ruta la devuelve al cambio. Al cambiarla, la marca se levanta y la cuenta opera con normalidad. **La contraseña nueva la elige el alumno y el administrador no la conoce.**

## 2. Justificación

Es lo que hace que la provisoria sea provisoria. Sin esta regla, una clave que el administrador conoce quedaría sirviendo indefinidamente para operar como el alumno: el reseteo dejaría de ser una reparación y pasaría a ser una puerta permanente a la identidad de otra persona. El fundamento está declarado en el propio enunciado de **INV-09** (`PRODUCT-INTAKE` §17.1.P.2).

Es además lo que sostiene la promesa de identidad propia del alumno de `NB-02` en el único momento en que el producto la pone en riesgo: el reseteo la reduce a una ventana que la propia cuenta cierra.

## 3. Ámbito de aplicación

- Se evalúa sobre toda cuenta con la **marca de cambio de contraseña pendiente** puesta, cualquiera sea su papel y su estado de cuenta.
- La marca la ponen **la habilitación** (CU-02) y **el reseteo** (CU-13), los dos actos del administrador, y la levanta **únicamente** el reemplazo de credencial hecho por la propia cuenta (CU-03 FA-04). Hasta `PRODUCT-INTAKE` 1.12 la ponía sólo el reseteo; **RN-16** le agrega la habilitación, con lo cual esta regla pasa a gobernar **también el primer ingreso del alumno** y no sólo la reparación de un olvido. Ni el administrador la levanta, ni la levanta el paso del tiempo: ninguna fuente declara vencimiento de la provisoria (`Definicion-Modelo-De-Dominio.md` §5.3).
- **Dónde la ejerce el dominio, y es una decisión derivada declarada.** El enunciado alcanza a todas las capacidades del sistema, y el dominio no tiene una puerta única por la que pasen todas: la guarda se concentra en la evaluación de admisibilidad de [CU-04](../Casos-De-Uso/CU-04-Evaluar-La-Admisibilidad-De-La-Cuenta.md), con el fundamento de que ninguna capacidad se ejerce sin admisión resuelta. Es el mismo lugar donde vive INV-06. Está declarada en `Definicion-Modelo-De-Dominio.md` §4.1 y elevada como punto abierto en `Especificacion-Funcional.md` §9.
- **Qué queda fuera del dominio.** «Cualquier otra ruta la devuelve al cambio» es una afirmación sobre rutas, y las rutas no son de este proyecto de código: la exigencia baja a `GeometriaFactory-Api` y a `GeometriaFactory-Web`. El dominio declara la condición; el encaminamiento lo ejerce quien atiende peticiones, exactamente como con INV-06.
- **Sin sesión de trabajo, y desde `PRODUCT-INTAKE` 1.13 ya no hay dos formas que comparar.** El intake precisó en su versión **1.8** que la 1.7 decía «ingresa», que se leía como que la cuenta obtenía sesión; emitir sesión a una cuenta que por INV-09 no ejerce ninguna capacidad es contradictorio, y la diferencia es observable desde la capa que emite el acceso. Se resolvió del lado de no emitirla, **por paralelismo** con la forma en que el producto resolvía entonces el primer ingreso con contraseña no fijada, cuyo motivo era `CREDENCIAL_NO_ESTABLECIDA`. Con **RN-16** el paralelismo dejó de ser una analogía y pasó a ser una identidad: el primer ingreso recorre este mismo camino, aquel motivo quedó retirado de CU-04 y **el único encaminamiento vivo es el de esta regla**.
- **Su invariante es INV-09**, que comparte con RN-12 (`Definicion-Modelo-De-Dominio.md` §4.3).

## 4. Consecuencia si se viola

No hay un código de rechazo propio, y el motivo importa: esta regla no se viola pidiendo algo mal formado, se viola **dejando pasar** a una cuenta marcada. Su materialización es el motivo de resultado `CAMBIO_DE_CONTRASENA_PENDIENTE` de CU-04, que devuelve la cuenta como **no admisible**. Una implementación que emitiera acceso a una cuenta marcada rompería INV-09 sin producir ninguna condición de error: es un defecto que sólo una prueba detecta, y por eso §6 declara la suya.

## 5. CU afectados

- [CU-04](../Casos-De-Uso/CU-04-Evaluar-La-Admisibilidad-De-La-Cuenta.md) — Evaluar la admisibilidad de la cuenta, donde la guarda se ejerce y donde vive el motivo.
- [CU-03](../Casos-De-Uso/CU-03-Fijar-Y-Reemplazar-La-Credencial-Derivada.md) — Fijar y reemplazar la credencial derivada, en su flujo alternativo FA-04, que es el único acto que levanta la marca.
- [CU-13](../Casos-De-Uso/CU-13-Resetear-La-Contrasena-De-Una-Cuenta-De-Alumno.md) — Resetear la contraseña, uno de los dos actos que la ponen.
- [CU-02](../Casos-De-Uso/CU-02-Gobernar-El-Ciclo-De-Vida-De-La-Cuenta.md) — Gobernar el ciclo de vida de la cuenta, en su transición de **habilitación**, que es el otro acto que la pone desde **RN-16**.

## 6. Pruebas que la verifican

Pruebas unitarias de dominio previstas en 08: cuenta reseteada que se evalúa **no admisible** con el motivo `CAMBIO_DE_CONTRASENA_PENDIENTE`; la misma cuenta admisible después del reemplazo; reemplazo rechazado por credencial vigente no verificada que **deja la marca puesta**; y la prueba negativa que cierra §4, que verifica que ninguna operación distinta del reemplazo levanta la marca. El dato de prueba que el intake declara es el alumno reseteado que intenta abrir el listado de trabajos o enviar uno y termina en el cambio de contraseña, **sin haber leído ni escrito nada** (§4.1, columna de verificación), y desde 1.13 el de **RN-16**, que es el mismo recorrido sobre un alumno **recién habilitado**; esa verificación es de punta a punta y su lugar es 08 sobre el producto integrado, no el dominio solo.

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial, por la regla **RN-13** que `PRODUCT-INTAKE` 1.7 §4.1 transcribe junto con la capacidad **F-26** y el invariante **INV-09**. Declara el enunciado, la justificación —es lo que hace que la provisoria sea provisoria—, el ámbito con los dos únicos actos que mueven la marca, la **decisión derivada** de concentrar la guarda del dominio en CU-04, lo que queda fuera del dominio porque habla de rutas, y la consecuencia, que no es un rechazo sino un motivo de resultado y una prueba negativa. |
| 1.1 | 2026-08-09 | **Absorbe la precisión de `PRODUCT-INTAKE` 1.8 §4.1.** El enunciado de §1 suma que la cuenta con contraseña provisoria **se autentica pero no obtiene sesión de trabajo**, que es lo que la versión 1.7 dejaba ambiguo al decir «ingresa». §3 suma el punto que declara el fundamento —emitir sesión a una cuenta que por INV-09 no ejerce ninguna capacidad es contradictorio, y la forma adoptada es el paralelo del primer ingreso con contraseña no fijada— y la cabecera cita el intake **1.8**. **Ni el ámbito, ni la consecuencia, ni los CU afectados, ni las pruebas cambian**: CU-04 ya devolvía **no admisible**. Sube minor: precisa un enunciado sin alterar lo que la regla exige. |
| 1.2 | 2026-08-10 | **Absorbe `PRODUCT-INTAKE` 1.13 §4.1 (RN-16).** El enunciado de §1 no cambia; lo que cambia es **su alcance**, porque la marca que la regla gobierna deja de tener una sola fuente. **§3** declara que la ponen **dos** actos —la habilitación de CU-02 y el reseteo de CU-13— y que la sigue levantando uno solo, el reemplazo de la propia cuenta; y reescribe el punto del paralelismo con el primer ingreso, que dejó de ser una analogía para ser una identidad, con la constancia de que `CREDENCIAL_NO_ESTABLECIDA` quedó retirado de CU-04. **§5** suma **CU-02** a los CU afectados. **§6** suma el dato de prueba de RN-16. **Ni la consecuencia ni el motivo de CU-04 que la materializa cambian.** Sube minor: amplía el alcance de una regla sin alterar lo que exige. |
