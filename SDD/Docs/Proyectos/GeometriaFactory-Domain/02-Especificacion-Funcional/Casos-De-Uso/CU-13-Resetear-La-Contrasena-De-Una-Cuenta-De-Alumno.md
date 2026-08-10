# CU-13 — Resetear la contraseña de una cuenta de alumno

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** CU-13-Resetear-La-Contrasena-De-Una-Cuenta-De-Alumno.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-01`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-01-Control-De-Admision-Al-Laboratorio.md) §1, §4 y §5; [`NB-02`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md) §1 y §5; `00-Contexto/Vision-Producto.md` §9.1 y §9.2; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` 1.7 §4 (**F-26**, F-03), §4.1 (**RN-12**, **RN-13**, RN-01, RN-07), §17.1.P.2 (**INV-09**, INV-05, INV-08), §17.1.P.5, §7 (**CL-7**), §9 (**X-2 retirada**, X-1 vigente), §11 (RN-B6)
**Trazabilidad downstream:** `05-Arquitectura-Tecnica` y `06-Backlog-Tecnico` de GeometriaFactory-Domain; `08-Calidad-Y-Pruebas`

---

## Tabla de contenido

- [1. Propósito](#1-propósito)
- [2. Actores](#2-actores)
- [3. Precondiciones](#3-precondiciones)
- [4. Flujo principal](#4-flujo-principal)
- [5. Flujos alternativos](#5-flujos-alternativos)
- [6. Excepciones y errores](#6-excepciones-y-errores)
- [7. Postcondiciones](#7-postcondiciones)
- [8. Criterios de aceptación](#8-criterios-de-aceptación)
- [9. Trazabilidad](#9-trazabilidad)
- [10. Notas y supuestos](#10-notas-y-supuestos)
- [11. Control de cambios](#11-control-de-cambios)
- [17. Compatibilidad de la superficie pública](#17-compatibilidad-de-la-superficie-pública)

---

## 1. Propósito

Sostener el reseteo de contraseña que el administrador ejerce sobre una **cuenta de alumno**: fijarle una **contraseña provisoria** y dejar la cuenta marcada como con **cambio de contraseña pendiente**, sin tocar nada más. Es el contrato de uso que hace verificable la promesa central de la capacidad F-26: **la cuenta y todos sus trabajos se conservan**, con sus estados y sus comentarios (RN-12).

**Resetear no es dar de baja.** Es la distinción que este caso de uso existe para hacer imposible de confundir. Hasta `PRODUCT-INTAKE` 1.6 el único camino declarado ante una contraseña olvidada era dar de baja la cuenta y volver a darla de alta, y por RN-07 eso eliminaba **todos** los trabajos del alumno: el primer olvido costaba la cursada entera. La capacidad F-26 cierra ese agujero y retira la exclusión X-2; el caso límite CL-7 queda reescrito sobre este camino.

Lo que este caso de uso **no** hace: no cambia el estado de cuenta, no elimina ningún trabajo, no deriva la contraseña provisoria y no levanta la marca que pone. Levantarla es del reemplazo de credencial de [CU-03](CU-03-Fijar-Y-Reemplazar-La-Credencial-Derivada.md), y sólo lo hace la propia cuenta.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Capa de casos de uso del producto (`GeometriaFactory-Application`) | Primario | Solicita el reseteo de la credencial derivada de un alumno ya constituido |
| Capa de infraestructura (`GeometriaFactory-Infrastructure`) | Secundario | **Genera** la contraseña provisoria y la deriva antes de que el valor llegue al dominio, y materializa el resultado |
| Modelo de dominio de `GeometriaFactory-Domain` | Sistema | Admite o rechaza el reseteo, reemplaza la credencial derivada y pone la marca |

El administrador es el **sujeto** de la regla, no el actor: quien invoca la superficie pública de esta biblioteca es el código consumidor. El dominio **no maneja secretos**: la contraseña provisoria llega ya derivada (PRODUCT-INTAKE §17.1.P.5), de modo que **el dominio no conoce el valor que el administrador le va a comunicar al alumno**. Tampoco lo produce: **quien lo produce es el sistema y no el administrador**, por decisión del Product Owner, y la exigencia sobre ese valor vive en la capa que lo produce (§10).

## 3. Precondiciones

- El alumno existe y su estado de cuenta pertenece al conjunto `Pendiente`, `Habilitado`, `Bloqueado`.
- **La cuenta sobre la que se opera tiene papel `Alumno`.**
- La credencial derivada de la cuenta ya tiene valor.
- El valor de credencial provisoria que se aporta ya está derivado; el dominio no recibe texto en claro.

## 4. Flujo principal

1. La capa de aplicación solicita resetear la credencial derivada de un alumno, aportando el valor ya derivado de la contraseña provisoria.
2. El dominio comprueba que el papel de la cuenta sea `Alumno`.
3. El dominio comprueba que la credencial derivada tenga valor.
4. El dominio comprueba que el valor aportado no esté vacío.
5. El dominio comprueba que la solicitud no declare ningún efecto sobre los trabajos del alumno ni sobre su estado de cuenta.
6. El dominio reemplaza la credencial derivada con el valor aportado.
7. El dominio pone la marca de cambio de contraseña pendiente.
8. El dominio devuelve el alumno, con su estado de cuenta, su papel, su identidad y su conjunto de trabajos **sin ningún cambio**.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | Se resetea una cuenta que ya tiene la marca puesta | El dominio admite la operación: reemplaza la contraseña provisoria por la nueva y la marca sigue puesta. Es el caso del alumno que también olvida la provisoria, y no hay ningún motivo declarado para rechazarlo | Paso 8, con la marca sin cambio |
| FA-02 | Se resetea una cuenta `Bloqueado` o `Pendiente` | El dominio admite la operación: el reseteo **no** es una transición de la máquina de estados de cuenta y no exige que la cuenta esté `Habilitado`. La cuenta sigue sin obtener acceso, pero por INV-06 y no por este caso de uso | Paso 8, con el estado de cuenta sin cambio |
| FA-03 | El alumno olvidó su contraseña y su cuenta nunca tuvo credencial | No hay nada que resetear: la cuenta está esperando su primer ingreso efectivo, en el que la credencial se fija por CU-03. El dominio rechaza con el código de §6 en lugar de poner una marca que el camino de fijación no puede levantar | Paso 3, con el rechazo de §6 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del dominio |
| --- | --- | --- |
| `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` | Se solicita el reseteo sobre la cuenta con papel `Administrador` | Rechaza la operación y conserva la cuenta sin modificar. **Es el mismo código con el que CU-02 cierra sus cuatro operaciones**, y se reutiliza porque la causa es la misma: es una operación del administrador declarada sobre cuentas de alumno, y sobre la única cuenta de administrador no tiene quién la ejerza. El cambio de su propia contraseña entra por el reemplazo de CU-03, FA-01 |
| `RESETEO_SOBRE_CREDENCIAL_NO_FIJADA` | Se solicita resetear una cuenta cuya credencial derivada todavía no tiene valor | Rechaza la operación y no pone la marca. El camino correcto es el primer ingreso efectivo de CU-03 |
| `RESETEO_CON_ARRASTRE_DE_TRABAJOS` | La solicitud declara que el reseteo elimina los trabajos del alumno, o que cambia su estado de cuenta | Rechaza la operación. **Resetear no es dar de baja y no dispara RN-07**: la cuenta y todos sus trabajos se conservan (RN-12) |
| `VALOR_DERIVADO_VACIO` | El valor de credencial provisoria aportado está vacío | Rechaza la operación y conserva la credencial derivada anterior. Entrada declarada primero en CU-03 §6, con la misma causa |

Los cuatro rechazos dejan al alumno exactamente como estaba: con su credencial anterior, su marca anterior, su estado de cuenta y todos sus trabajos.

## 7. Postcondiciones

- **Éxito:** el alumno tiene la credencial derivada provisoria, la marca de cambio de contraseña pendiente puesta y **ningún otro atributo cambiado**. Su estado de cuenta, su papel, su identidad, su correo y su conjunto de trabajos —con sus estados, sus observaciones y sus comentarios— son los mismos que antes de la operación.
- **Fallo:** no hay efecto parcial. En particular, no existe ningún camino por el que la credencial se reemplace y la marca no se ponga, ni al revés: los dos son un solo acto.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un alumno con cuenta `Habilitado`, credencial derivada con valor y 3 trabajos —uno en `Borrador`, uno en `Rechazado` con comentario y uno en `Finalizado`— | La capa de aplicación solicita el reseteo con un valor derivado no vacío | El dominio devuelve el alumno con la credencial provisoria, la marca puesta, cuenta `Habilitado` y **los 3 trabajos con sus estados y sus comentarios**, con 0 eliminaciones |
| CA-02 | El mismo alumno, ya reseteado y con la marca puesta | La capa de aplicación consulta su admisibilidad | El dominio devuelve no admisible con el motivo `CAMBIO_DE_CONTRASENA_PENDIENTE` (CU-04) |
| CA-03 | El mismo alumno, ya reseteado | La propia cuenta reemplaza su credencial declarando verificada la provisoria (CU-03 FA-04) | La marca queda levantada y la cuenta vuelve a ser admisible, con sus 3 trabajos intactos |
| CA-04 | La cuenta con papel `Administrador` | La capa de aplicación solicita resetear su contraseña | El dominio rechaza con el código `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` y la cuenta sigue sin modificar |
| CA-05 | Un alumno con cuenta `Habilitado` y credencial derivada **sin valor** | La capa de aplicación solicita el reseteo | El dominio rechaza con el código `RESETEO_SOBRE_CREDENCIAL_NO_FIJADA` y la marca sigue levantada |
| CA-06 | Un alumno con cuenta `Bloqueado`, credencial con valor y 2 trabajos | La capa de aplicación solicita el reseteo declarando que los trabajos se eliminan | El dominio rechaza con el código `RESETEO_CON_ARRASTRE_DE_TRABAJOS` y los 2 trabajos siguen existiendo |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-01 en su criterio de gobierno de las cuentas desde un solo lugar, NB-02 en su criterio de identidad propia sin canal de correo |
| Reglas de negocio aplicables | [RN-12](../Reglas-De-Negocio/RN-12-Reseteo-Conserva-La-Cuenta-Y-Sus-Trabajos.md), que es la que este caso de uso materializa; [RN-13](../Reglas-De-Negocio/RN-13-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md), por la marca que pone; [RN-01](../Reglas-De-Negocio/RN-01-Administrador-Unico-Y-Papeles-Fijos.md), por el cierre sobre la cuenta de administrador; y [RN-07](../Reglas-De-Negocio/RN-07-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md) **por contraste**: es la regla que este caso de uso **no** dispara |
| Invariantes | INV-09, por la marca; INV-08, por el cierre sobre la cuenta de administrador |
| Historias de usuario a generar en 06 | US-26, resetear la contraseña de un alumno conservando su cuenta y todos sus trabajos |
| Componentes esperados en 05 | Operación de reseteo sobre la entidad de alumno, con el atributo de marca de cambio de contraseña pendiente |
| Tests previstos en 08 | Pruebas unitarias del reseteo con trabajos en los cuatro estados verificando que ninguno se pierde, del segundo reseteo sobre una cuenta ya marcada, y de los cuatro rechazos, sin dobles y sin infraestructura |

## 10. Notas y supuestos

- **El dominio no genera la contraseña provisoria y no la conoce.** **La produce el sistema** —no la escribe el administrador, por decisión del Product Owner: si la escribiera, terminaría siendo la misma clave para toda la comisión—, el administrador la comunica por fuera del producto y al dominio llega ya derivada (PRODUCT-INTAKE §17.1.P.5). Que no sea adivinable y que no se repita entre cuentas son **exigencias sobre el valor generado**, y se declaran donde el valor nace: `GeometriaFactory-Application` CU-11 y su puerto, y `GeometriaFactory-Contracts` CU-08 sobre lo que el resultado devuelve. **Acá no se declaran, porque acá el valor ya llegó derivado y el dominio no puede verificarlas.** No hay canal de correo: la exclusión **X-1** sigue vigente y lo que se retiró es **X-2**, la recuperación de contraseña olvidada, que ahora tiene este camino.
- **La provisoria es provisoria porque existe INV-09.** Sin la marca, una clave que el administrador conoce quedaría sirviendo indefinidamente para operar como el alumno. Es el fundamento que el intake declara al enunciar el invariante.
- **Decisión derivada: el reseteo exige credencial ya fijada.** Ninguna fuente declara qué pasa si se resetea una cuenta que nunca estableció contraseña. Esta categoría lo rechaza, y el fundamento es de consistencia del modelo: la marca la levanta **únicamente** el reemplazo (RN-13), y sobre una credencial sin valor el camino disponible es la fijación de CU-03, que rechazaría con `CREDENCIAL_YA_FIJADA` si el reseteo hubiera puesto valor. Admitirlo dejaría cuentas marcadas sin ningún camino declarado para levantar la marca. Si el Product Owner prefiere admitirlo, la salida limpia es que el reseteo sobre credencial sin valor **fije** la credencial sin poner la marca, y eso es exactamente CU-03 y no este caso de uso.
- **El reseteo no exige estado `Habilitado`, y ya no es una decisión derivada: el Product Owner la ratificó.** Ninguna fuente lo condicionaba al estado y condicionarlo habría sido inventar una precondición; el Product Owner resolvió expresamente que el reseteo es una operación sobre la credencial que **no toca el estado de la cuenta**, de modo que se puede resetear una cuenta `Pendiente` o `Bloqueado` y el administrador puede resetear y habilitar **en el orden que quiera**, sin acordarse de una secuencia. Resetear una cuenta `Bloqueado` es inocuo: la cuenta sigue sin obtener acceso por INV-06. Lo que **no** cambia es el cierre sobre la cuenta de administrador de §6, que es de INV-08 y no del estado.
- La confirmación con la que el administrador ejerce esta operación en pantalla, si la hubiera, es de la categoría 03 del proyecto de código de la pieza pública. **Este caso de uso no exige confirmación escrita**, a diferencia de la baja: RN-12 no la pide, y pedirla sería trasladar a una operación conservadora la guarda de una destructiva.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial, por la capacidad **F-26** que `PRODUCT-INTAKE` 1.7 incorpora como `Must Have`, con las reglas **RN-12** y **RN-13**, el invariante **INV-09**, el retiro de la exclusión **X-2** y la reescritura del caso límite **CL-7**. Declara el reseteo como acto que conserva la cuenta, su estado, su papel, su identidad y todos sus trabajos con sus estados y comentarios; el reemplazo de la credencial derivada y la puesta de la marca como un solo acto sin efecto parcial; los tres flujos alternativos —segundo reseteo, cuenta no habilitada y cuenta sin credencial—; y cuatro condiciones de rechazo, dos de ellas reutilizadas de CU-02 y CU-03 con la misma causa. Deja declaradas dos **decisiones derivadas** que ninguna fuente enuncia: que el reseteo exige credencial ya fijada, y que no exige estado `Habilitado`. |
| 1.1 | 2026-08-09 | **Absorbe dos decisiones del Product Owner sobre F-26.** **Decisión A: resetear no exige que la cuenta esté habilitada.** Este caso de uso **ya lo declaraba así** —FA-02 y §7—, de modo que **ningún flujo, código de rechazo ni criterio de aceptación cambia**: lo único que cambia es el estatuto de la afirmación. La nota de §10 deja de rotularse **decisión derivada** y pasa a registrar la **ratificación del Product Owner**, con su fundamento —el administrador no tiene que acordarse de una secuencia— y con la precisión de que el cierre sobre la cuenta de administrador de §6 **no** se afloja, porque es de INV-08 y no del estado. **Decisión B: la contraseña provisoria la produce el sistema y no la escribe el administrador**, porque una provisoria escrita por el docente termina siendo la misma clave para toda la comisión. Acá corrige una afirmación que quedó **falsa**: §10 y §2 decían que la provisoria «la elige el administrador». §2 declara que la genera la infraestructura antes de derivarla, y §10 declara que **no ser adivinable y no repetirse entre cuentas son exigencias de la capa que produce el valor** —`GeometriaFactory-Application` CU-11 y `GeometriaFactory-Contracts` CU-08— y **no de ésta**, porque acá el valor llega ya derivado y el dominio no puede verificarlas. Sube minor: corrige una afirmación de contexto y ratifica otra, sin tocar la superficie del caso de uso. |

## 17. Compatibilidad de la superficie pública

- Quitar la puesta de la marca del efecto de esta operación es un cambio **incompatible** y además rompe INV-09: la contraseña provisoria dejaría de ser provisoria.
- Admitir que esta operación cambie el estado de cuenta, o que alcance a los trabajos del alumno, es un cambio de alcance que contradice RN-12 y sube la versión mayor de este caso de uso.
- Agregar una condición de rechazo al conjunto de §6 obliga a revisar a los consumidores que las traducen y sube la versión mayor, con el mismo criterio que CU-04 aplica a su conjunto de motivos.
