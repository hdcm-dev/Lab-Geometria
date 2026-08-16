# RN-02012 — El reseteo de contraseña conserva la cuenta y sus trabajos

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** RN-02012-Reseteo-Conserva-La-Cuenta-Y-Sus-Trabajos.md
**Versión:** 1.4
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.10** §4.1 (enunciado de **RN-02012**, **RN-02014** y **RN-02015**), §4 (**F-26**), §17.1.P.2 (**INV-09**), §7 (**CL-7** reescrito), §9 (**X-2 retirada**), §11 (**RN-B6 tachado** el 2026-08-09 por este mismo 1.10, precisamente porque F-26 dejó sin objeto su mitigación), §4.2 (modelo de estados del trabajo); [`NB-00001`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00001-Control-De-Admision-Al-Laboratorio.md) §1, §4 y §5; [`NB-00002`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00002-Identidad-Propia-Del-Alumno-Sin-Correo.md) §1; `00-Contexto/Vision-Producto.md` §8 (RG-06)
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

El reseteo de **la contraseña** de una cuenta de alumno **conserva la cuenta y todos sus trabajos**. Lo que se resetea es la contraseña; la cuenta no se resetea, no se da de baja y no se vuelve a crear. El **sistema produce** una contraseña **provisoria** (RN-02014); la cuenta queda marcada como **con cambio de contraseña pendiente** y **conserva su estado de habilitación**, su papel, su identidad y **todos sus trabajos con sus estados y comentarios**. Resetear la contraseña no es dar de baja la cuenta: **no dispara RN-02007**.

## 2. Justificación

Cierra un agujero de diseño y no agrega comodidad. Hasta `PRODUCT-INTAKE` 1.6 el único camino declarado ante una contraseña olvidada era dar de baja la cuenta y volver a darla de alta, y por RN-02007 esa baja **elimina la cuenta y todos sus trabajos**: el primer olvido de contraseña costaba la cursada entera. El producto no tiene canal de correo y la recuperación autónoma sigue excluida por X-1, de modo que sin esta regla no había ninguna salida que conservara el trabajo del alumno.

Es la respuesta declarada del Product Owner al caso límite de la contraseña olvidada (§7, **CL-7** reescrito) y el motivo por el que la exclusión **X-2** queda retirada.

## 3. Ámbito de aplicación

- Se evalúa en todo reseteo de contraseña de una cuenta de alumno, **cualquiera sea su estado de cuenta**: el reseteo no es una transición de la máquina de estados de cuenta y no la exige `Habilitado`. Esa independencia tiene desde el `PRODUCT-INTAKE` 1.10 regla propia, [RN-02015](RN-02015-Reseteo-Independiente-Del-Estado-De-Cuenta.md), y acá se cita en lugar de enunciarse de nuevo.
- Alcanza a los trabajos en los **cuatro** estados, incluidos los dos terminales y sus comentarios: el reseteo no toca ninguno.
- **No se aplica a la cuenta con papel `Administrador`**, sobre la que el reseteo no procede (CU-02013 §6, RN-02001, INV-08). El administrador cambia su propia contraseña por el reemplazo de CU-02003.
- **Su invariante es INV-09**, que comparte con RN-02013. Las dos son las dos mitades de la misma condición: ésta declara qué conserva el reseteo, y RN-02013 qué no puede la cuenta hasta cambiar la provisoria (`Definicion-Modelo-De-Dominio.md` §4.3).
- El dominio **no conoce la contraseña provisoria y no la produce**: **la produce el sistema** —no la escribe el administrador—, el administrador la comunica por fuera del producto y al dominio llega ya derivada (PRODUCT-INTAKE §17.1.P.5). Quién la produce y qué se le exige al valor tienen desde el `PRODUCT-INTAKE` 1.10 regla propia, [RN-02014](RN-02014-Provisoria-Producida-Por-El-Sistema.md), y acá se citan en lugar de enunciarse de nuevo.

## 4. Consecuencia si se viola

Rechazo. Un reseteo que declare eliminar los trabajos del alumno o cambiar su estado de cuenta se rechaza con el código `RESETEO_CON_ARRASTRE_DE_TRABAJOS`, y no se reemplaza ninguna credencial ni se pone ninguna marca. El daño que la regla evita es exactamente el que hacía inutilizable al laboratorio: perder trabajos ya aprobados por un olvido de contraseña.

## 5. CU afectados

- [CU-02013](../Casos-De-Uso/CU-02013-Resetear-La-Contrasena-De-Una-Cuenta-De-Alumno.md) — Resetear la contraseña de una cuenta de alumno, que es donde la regla se materializa.
- [CU-02002](../Casos-De-Uso/CU-02002-Gobernar-El-Ciclo-De-Vida-De-La-Cuenta.md) — **por contraste**: es el contrato donde vive la baja con arrastre de RN-02007, y esta regla existe para que el reseteo no se confunda con ella.

## 6. Pruebas que la verifican

Pruebas unitarias de dominio previstas en 08: reseteo de la contraseña de una cuenta con trabajos en los cuatro estados verificando que **ninguno** se elimina y que los comentarios se conservan; reseteo sobre cuentas `Pendiente`, `Habilitado` y `Bloqueado` verificando que el estado no cambia; y rechazo del reseteo que declara arrastre. El dato de prueba que el intake declara para esta regla es el alumno con tres trabajos —uno en `Borrador`, uno en `Rechazado` y uno en `Finalizado`— que los conserva los tres después del reseteo (§4.1, columna de verificación).

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.4 | 2026-08-11 | **Unificación de nomenclatura del reseteo: se resetea la contraseña de la cuenta, no la cuenta.** Corrección pedida por el Product Owner —«ese resetear cuenta hay que corregirlo por resetear clave de cuenta de usuario alumno»— y corregida primero en la fuente, `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.28**: leído literal, «resetear la cuenta» sugiere darla de baja y volver a darla de alta, que es exactamente el remedio que **F-26** vino a reemplazar. Acá se reescriben **1** ocurrencia a «resetear / reseteo **de la contraseña** de la cuenta» y «cuenta **con la contraseña reseteada**». No cambia ninguna regla ni su verificación, y **no se toca ningún identificador** de código de error ni de regla —`RESETEO_ACOTADO_A_CUENTAS_DE_ALUMNO` y `CONTRATO_RESETEO_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` se conservan tal cual—. **§1 se precisa** para que el sujeto quede explícito —«el reseteo de **la contraseña** de una cuenta de alumno conserva la cuenta y **todos** sus trabajos», y «resetear la contraseña no es dar de baja la cuenta»—, **sin perder el enunciado ni su verificación**. |
| 1.0 | 2026-08-09 | Emisión inicial, por la regla **RN-02012** que `PRODUCT-INTAKE` 1.7 §4.1 transcribe junto con la capacidad **F-26**. Declara el enunciado, la justificación como cierre de un agujero de diseño con el retiro de **X-2** y la reescritura de **CL-7**, el ámbito sobre los tres estados de cuenta y los cuatro de trabajo, el cierre sobre la cuenta de administrador, la correspondencia con **INV-09** compartida con RN-02013, y el código de rechazo con el que se verifica. |
| 1.1 | 2026-08-09 | Absorbe la decisión del Product Owner sobre **quién produce la contraseña provisoria**: **la produce el sistema y no la escribe el administrador**, porque una provisoria escrita por el docente termina siendo la misma clave para toda la comisión. §3 corrige la última viñeta, que decía que «la elige el administrador» y quedó falsa. **El enunciado de la regla, su ámbito sobre los estados de cuenta, su consecuencia y su código de rechazo no cambian**; en particular, la viñeta que declara que el reseteo procede **cualquiera sea el estado de cuenta** ya era correcta y el Product Owner la ratificó. |
| 1.2 | 2026-08-09 | **Absorbe el `PRODUCT-INTAKE` 1.10**, que reescribe el enunciado de RN-02012 y reparte en dos reglas nuevas —**RN-02014** y **RN-02015**— lo que esta regla arrastraba como contexto. Es la fuente registrando las dos decisiones del Product Owner cuya ausencia el informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0 levantó como `F26-01`. **§1**: el enunciado decía «**el administrador fija** una contraseña provisoria», que era lo que la fuente decía hasta 1.9 y que el intake 1.10 invirtió: pasa a decir que **el sistema produce** la provisoria, con la remisión a **RN-02014**. **§3**: las dos viñetas que declaraban de quién es el valor provisorio y por qué el reseteo no exige estado habilitado **remiten ahora a las reglas que la fuente les dio** —[RN-02014](RN-02014-Provisoria-Producida-Por-El-Sistema.md) y [RN-02015](RN-02015-Reseteo-Independiente-Del-Estado-De-Cuenta.md)— en lugar de enunciarlas de nuevo acá. **Ni el ámbito, ni la consecuencia, ni el código de rechazo, ni los CU afectados, ni las pruebas cambian**: lo que esta regla promete —que la cuenta y todos sus trabajos se conservan— es exactamente lo mismo. Sube minor: corrige un enunciado contra la fuente y reparte contexto a dos reglas hermanas, sin alterar lo que exige. |
| 1.3 | 2026-08-10 | **Cierra la parte del hallazgo `N-5`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r2.md` 1.0 que alcanza a este archivo. La **trazabilidad de cabecera** citaba «§11 (RN-B6)» como riesgo vigente **declarando al mismo tiempo el intake 1.10**, que es exactamente la versión que lo tachó. La cita se conserva con la constancia de que está tachada y con el motivo, que es esta misma regla: F-26 conserva la cuenta y sus trabajos, de modo que la baja dejó de ser el remedio del olvido y la mitigación de `RN-B6` quedó sin objeto. **El enunciado de la regla no cambia.** Sube minor: corrige una referencia a una fila retirada. |
