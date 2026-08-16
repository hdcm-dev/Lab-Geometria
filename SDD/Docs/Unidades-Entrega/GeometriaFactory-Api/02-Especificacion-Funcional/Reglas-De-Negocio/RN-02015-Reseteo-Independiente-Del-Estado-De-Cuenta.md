# RN-02015 — Resetear no exige que la cuenta esté habilitada

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** RN-02015-Reseteo-Independiente-Del-Estado-De-Cuenta.md
**Versión:** 1.2
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.13** §4.1 (enunciado de **RN-02015**, RN-02006, RN-02012, **RN-02016**), §4 (**F-26**, «el reseteo no exige que la cuenta esté habilitada»), §17.1.P.2 (**INV-06**, **INV-08**, y las reglas sin invariante asociado), §7 (**CL-7**); [`NB-00001`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00001-Control-De-Admision-Al-Laboratorio.md) §1, §4 y §5; [`NB-00002`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00002-Identidad-Propia-Del-Alumno-Sin-Correo.md) §1
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

**Resetear no exige que la cuenta esté habilitada.** Procede sobre `Pendiente`, `Habilitado` y `Bloqueado`: es una operación **sobre la credencial** y **no una transición de la máquina de estados de la cuenta**, de modo que no altera el estado ni requiere un orden previo. Sigue **sin admitirse sobre la cuenta de administrador** (INV-08).

## 2. Justificación

Es una regla que le saca una secuencia de la cabeza al administrador. Condicionar el reseteo al estado habría obligado a habilitar antes de resetear —y a acordarse de ese orden justo en el momento en que se está reparando el olvido de otra persona—, sin que ninguna fuente lo pidiera y sin que nada lo justificara: el reseteo cambia la credencial y no toca la situación de la cuenta, así que **habilitar y resetear en cualquiera de los dos órdenes tiene que terminar en el mismo lugar**.

Resetear la contraseña de una cuenta `Bloqueado` es inocuo, y ése es el punto: la cuenta sigue sin obtener acceso, pero **por INV-06 y no por el reseteo**. Confundir las dos cosas era el defecto que esta regla cierra — se rechazaba una operación por un motivo que otra regla ya sostiene mejor.

Lo que la regla **no** afloja es el cierre sobre la cuenta de administrador: ése no viene del estado sino de INV-08 y de RN-02001, y el intake lo deja escrito en el propio enunciado de RN-02015.

## 3. Ámbito de aplicación

- Se evalúa en **todo reseteo de contraseña**, y su efecto es negativo: declara una precondición que **no existe**. El conjunto de estados de cuenta admitidos es `Pendiente`, `Habilitado` y `Bloqueado`, que son **todos** los que el modelo declara (`Definicion-Modelo-De-Dominio.md` §5.1).
- **No cambia el estado de cuenta.** Después del reseteo la cuenta queda exactamente en el estado en el que estaba, que es lo que RN-02012 declara para la cuenta entera y lo que `CU-02013` §7 declara como postcondición.
- **El reseteo no es una transición de la máquina de estados de cuenta**, y por eso no aparece en ninguna arista de `Definicion-Modelo-De-Dominio.md` §5.1. La máquina que sí mueve es la de la marca de cambio de contraseña pendiente, §5.3, que es otra.
- **Sigue sin admitirse sobre la cuenta con papel `Administrador`**, y el fundamento es **INV-08** —esa cuenta está siempre `Habilitado` y no admite baja— junto con **RN-02001**, administrador único: un reseteo sobre sí mismo dejaría al laboratorio sin nadie capaz de habilitar, desbloquear ni revisar hasta que la marca de INV-09 se levante. El cierre es de papel, no de estado, y por eso esta regla no lo toca.
- **La exigencia de credencial ya fijada, que esta regla dejaba en pie, quedó retirada por RN-02016.** Hasta `PRODUCT-INTAKE` 1.12, `CU-02013` rechazaba el reseteo sobre una cuenta que nunca había establecido contraseña, porque el único camino para dársela era el primer ingreso anónimo. **RN-02016 suprimió ese camino**, y con él el fundamento del rechazo: hoy el reseteo sobre una cuenta `Pendiente` sin credencial **fija** la provisoria en lugar de reemplazarla, y procede. El retiro **resuelve a favor de esta regla** una tensión que la versión 1.0 registraba sin verla: el rechazo alcanzaba precisamente a la cuenta `Pendiente` que este enunciado declara admitida.
- **No tiene invariante asociado**, y el intake lo declara así en la prosa de §17.1.P.2: enuncia la ausencia de una precondición sobre una operación, no una condición permanente sobre los datos.

## 4. Consecuencia si se viola

Violar esta regla no produce un rechazo: **produce uno de más**. Una implementación que exigiera `Habilitado` rechazaría el reseteo de la contraseña de una cuenta `Pendiente` o `Bloqueado` con un motivo por la situación de la cuenta, y ese motivo **no existe** en ninguna de las capas del producto: `CU-02013` §6 no lo tiene, `GeometriaFactory-Application` `CU-02011` lo retiró con constancia escrita en su `DX-Error-Messages.md` §3.11, y `GeometriaFactory-Contracts` `CU-02008` `CA-08` verifica **0 códigos** devueltos por la situación de la cuenta. Desde el intake 1.13, `CU-02013` `CA-05` verifica además **0 rechazos** por la ausencia de credencial previa, que era el último motivo por el que una cuenta `Pendiente` podía ver rechazado su reseteo.

El daño concreto es el que la regla evita: el administrador que resetea antes de habilitar recibe un rechazo que no entiende, y termina dando de baja la cuenta —que es el procedimiento destructivo que F-26 vino a reemplazar—.

## 5. CU afectados

- [CU-00024](../Casos-De-Uso/CU-00024-Resetear-La-Contrasena-De-Un-Alumno.md) — Resetear la contraseña de una cuenta de alumno, donde la regla se materializa: §3 admite los tres estados, **FA-02** declara el reseteo sobre `Bloqueado` o `Pendiente` y §7 devuelve el estado sin cambio.
- [CU-00023](../Casos-De-Uso/CU-00023-Gobernar-Las-Cuentas-De-La-Comision.md) — **por contraste**: es el contrato de las transiciones de la máquina de estados de cuenta, que son precisamente lo que el reseteo **no** es.
- [CU-00022](../Casos-De-Uso/CU-00022-Ingresar-Al-Laboratorio-Y-Sostener-La-Sesion.md) — porque es ahí, y no en el reseteo, donde una cuenta `Pendiente` o `Bloqueado` sigue sin obtener acceso, por INV-06.

## 6. Pruebas que la verifican

Pruebas unitarias de dominio previstas en 08: reseteo sobre cuenta `Pendiente` y sobre cuenta `Bloqueado`, verificando que **procede** y que el estado de cuenta vuelve **sin cambio**; y el rechazo del reseteo sobre la cuenta con papel `Administrador`, que sigue en pie por INV-08. El dato de prueba que el intake declara para esta regla es doble (§4.1, columna de verificación): resetear la contraseña de una cuenta `Bloqueado` funciona y la deja `Bloqueado`, y **habilitar y resetear la contraseña en cualquiera de los dos órdenes termina igual** — esta segunda es de punta a punta y su lugar es `GeometriaFactory-Application` `CU-02011` `CA-07`, que ya la declara.

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.2 | 2026-08-11 | **Unificación de nomenclatura del reseteo: se resetea la contraseña de la cuenta, no la cuenta.** Corrección pedida por el Product Owner —«ese resetear cuenta hay que corregirlo por resetear clave de cuenta de usuario alumno»— y corregida primero en la fuente, `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.28**: leído literal, «resetear la cuenta» sugiere darla de baja y volver a darla de alta, que es exactamente el remedio que **F-26** vino a reemplazar. Acá se reescriben **4** ocurrencias a «resetear / reseteo **de la contraseña** de la cuenta» y «cuenta **con la contraseña reseteada**». No cambia ninguna regla ni su verificación, y **no se toca ningún identificador** de código de error ni de regla —`RESETEO_ACOTADO_A_CUENTAS_DE_ALUMNO` y `CONTRATO_RESETEO_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` se conservan tal cual—. |
| 1.0 | 2026-08-09 | Emisión inicial, por la regla **RN-02015** que `PRODUCT-INTAKE` **1.10** §4.1 incorpora al transcribir la decisión del Product Owner del 2026-08-09 sobre el estado de cuenta que exige el reseteo de la capacidad **F-26**. Declara el enunciado sobre los tres estados de cuenta, la justificación —el administrador no tiene que acordarse de una secuencia, y una cuenta `Bloqueado` sigue sin acceso por INV-06 y no por el reseteo—, el ámbito con la distinción entre operación sobre la credencial y transición de la máquina de estados, el cierre sobre la cuenta de administrador anclado en **INV-08** y RN-02001, la exigencia de credencial ya fijada que **no** se retira, la consecuencia —que es un rechazo de más y no de menos, con los tres lugares donde se verifica que ese motivo no existe— y las pruebas. El contenido no se origina acá: estaba ya modelado en `CU-02013` §3, FA-02, §7 y §10 de esta categoría, en `GeometriaFactory-Application` `CU-02011` CA-06 y CA-07 y en `GeometriaFactory-Contracts` `CU-02008` FA-04 y CA-08, y esta emisión lo recoge bajo el identificador que la fuente le dio. |
| 1.1 | 2026-08-10 | **Absorbe `PRODUCT-INTAKE` 1.13 §4.1 (RN-02016).** El enunciado de §1 **no cambia**. Lo que cambia es una salvedad de §3: la emisión 1.0 dejaba en pie la exigencia de **credencial ya fijada** como una condición distinta y con otro fundamento, y **RN-02016 la retiró** al suprimir el primer ingreso anónimo, que era el camino alternativo del que colgaba. §3 registra el retiro y deja escrito que **resuelve a favor de esta regla** una tensión que la 1.0 no había visto: aquel rechazo alcanzaba justo a la cuenta `Pendiente` que este enunciado declara admitida. §4 suma el tercer lugar donde se verifica que el motivo no existe, `CU-02013` `CA-05`. **Ni el ámbito de los tres estados, ni el cierre sobre la cuenta de administrador, ni las pruebas cambian.** Sube minor. |
