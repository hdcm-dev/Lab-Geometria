# CU-00024 — Resetear la contraseña de un alumno

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** CU-00024-Resetear-La-Contrasena-De-Un-Alumno.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-16
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-00002`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00002-Identidad-Propia-Del-Alumno-Sin-Correo.md); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-26), §4.1 (RN-02012, RN-02013, RN-02014, RN-02015, RN-02016), §9 (retiro de la exclusión X-2 y reescritura del caso límite CL-7), §17.1.P.5
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico` y `08-Calidad-Y-Pruebas` de la unidad de entrega
**Consolida a:** [`CU-00005`](../../../../_legacy/2026-08-16-consolidacion-8.5/GeometriaFactory-Api/CU-00005-Exponer-El-Reseteo-De-La-Contrasena-De-Un-Alumno.md), [`CU-04011`](../../../../_legacy/2026-08-16-consolidacion-8.5/GeometriaFactory-Api/CU-04011-Resetear-La-Contrasena-De-Un-Alumno.md) y [`CU-02013`](../../../../_legacy/2026-08-16-consolidacion-8.5/GeometriaFactory-Api/CU-02013-Resetear-La-Contrasena-De-Una-Cuenta-De-Alumno.md), por `Audit/Migracion-8.5-Consolidacion-Decidida.md` 1.2 §2.1

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

Que un alumno que olvidó su contraseña vuelva a entrar sin perder nada. El administrador acciona el
reseteo desde su panel, el sistema produce una **contraseña provisoria**, la respuesta se la muestra
**una sola vez** para que se la comunique en persona, y la cuenta queda con **cambio de contraseña
pendiente** hasta que el alumno elija la suya por `CU-00022`.

Se ejerce por **A-09**, y es **el único punto de toda esta superficie que devuelve un valor de
credencial en su respuesta**. Por eso su contrato es el más estricto en lo que no se puede hacer con
esa respuesta.

**Resetear no es dar de baja, y este caso de uso existe para hacer esa distinción imposible de
confundir.** Hasta el `PRODUCT-INTAKE` 1.6 el único camino ante un olvido era dar de baja la cuenta y
volver a darla de alta, y por RN-02007 eso eliminaba **todos** los trabajos del alumno: **el primer
olvido costaba la cursada entera**. La capacidad F-26 cierra ese agujero, retira la exclusión X-2 y
reescribe el caso límite CL-7 sobre este camino. De ahí la propiedad que este punto tiene que hacer
observable desde afuera: **el reseteo conserva la cuenta, su situación, su papel, su identidad y todos
sus trabajos con sus estados y sus comentarios**.

**El reseteo no es una transición de la máquina de estados**, y por eso no está con las cuatro
operaciones de `CU-00023`: procede sobre las tres situaciones —`Pendiente`, `Habilitado`,
`Bloqueado`— **sin alterarlas**. Habilitar y resetear son independientes, y el administrador las
ejerce en el orden que quiera.

**El administrador no escribe la contraseña.** La produce el sistema, por decisión del Product Owner,
y él sólo la lee una vez y la comunica. El canal por el que se la comunica al alumno es del aula y no
del producto.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Administrador | Primario | Acciona el reseteo desde su panel y **comunica la provisoria en persona**. No la escribe, no la elige y no la conoce después de cerrar la pantalla |
| `GeometriaFactory-Web` | Intermediario | Arma la solicitud y **muestra la provisoria una vez** (RA-01) |
| Alumno | Sujeto de la regla | Recibe la provisoria y la cambia por la suya en su ingreso siguiente |
| Mecanismo de producción de la provisoria | Sistema | Produce el valor en claro, **no adivinable y sin repetirse** (RN-02014). Es el mismo que consume `CU-00023` |
| Mecanismo de credenciales | Sistema | Deriva la provisoria antes de que el valor llegue al modelo de dominio, que **nunca la conoce en claro** |
| Almacén de cuentas | Sistema | Recupera la cuenta destino y materializa credencial, marca y sello |
| Reloj del sistema | Sistema | Provee el sello de modificación de la cuenta |

**La verificación de facultad se ejerce en el servidor**, y no ocultando un control en la pantalla.

## 3. Precondiciones

- La petición trae sesión firmada con papel `Administrador` y **atravesó la guardia** de `CU-00022`.
- La solicitud lleva **el identificador de la cuenta y nada más**. En particular **no lleva
  contraseña**: el panel del administrador no tiene campo de contraseña.
- La cuenta destino existe y tiene papel `Alumno`. **Su situación es indistinta**, y **su credencial
  puede no existir todavía**.

## 4. Flujo principal

1. El alumno le avisa al administrador, en el aula, que olvidó su contraseña.
2. El administrador acciona el reseteo sobre esa cuenta y llega una petición a **A-09** con el
   identificador.
3. Se verifica que el papel de quien pide sea `Administrador` (RN-02001), **antes de recuperar la
   cuenta destino**, y se recupera la cuenta.
4. Se verifica que la cuenta destino tenga papel `Alumno`. **No se verifica su situación: el reseteo
   no la exige**, y este punto **no declara ningún parámetro de situación**.
5. Se pide la **provisoria** al mecanismo de producción, se la **deriva**, y se toma el sello del
   reloj.
6. Se comprueba que el valor aportado no esté vacío y que la solicitud no declare ningún efecto sobre
   los trabajos ni sobre la situación de la cuenta. Se **reemplaza** la credencial derivada y se pone
   la **marca de cambio de contraseña pendiente**, en **un solo acto**.
7. Se materializan credencial, marca y sello **en una única unidad de trabajo**.
8. Se responde `200` con **la contraseña provisoria**, la situación conservada de la cuenta y la
   declaración del cambio pendiente.
9. El administrador se la comunica al alumno, que la cambia por la suya en `CU-00022`. **Sólo ese
   cambio levanta la marca.**

**La provisoria en claro no se persiste en ninguna parte y no queda en ninguna traza del servidor, ni
siquiera parcialmente.** Lo que se guarda es su forma derivada.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | La cuenta está en situación `Pendiente` o `Bloqueado` | El reseteo **procede igual** y la cuenta **queda en la misma situación**. Sigue sin obtener acceso, pero por INV-06 y no por acá. El motivo declarado de esta independencia es **que el administrador no tenga que acordarse de una secuencia** | Paso 8 |
| FA-02 | La cuenta **nunca tuvo credencial**, porque es `Pendiente` y nunca fue habilitada | El reseteo **procede igual**: se **fija** la credencial provisoria en lugar de reemplazarla, y se pone la marca. Es el mismo acto con la misma postcondición | Paso 8 |
| FA-03 | Se resetea una cuenta **que ya tiene la marca puesta** | Procede igual: la provisoria nueva reemplaza a la anterior y **la marca sigue puesta**. Es el caso del alumno que perdió también la provisoria antes de usarla. **La marca no se acumula y no se levanta** | Paso 8 |
| FA-04 | Se resetea dos veces seguidas la misma cuenta | Las **dos** peticiones responden `200` y las provisorias son **distintas** | Paso 8 |
| FA-05 | La cuenta tiene trabajos en `Borrador`, en `Rechazado` con comentario y en `Finalizado` | El reseteo **no toca ninguno**: conserva los tres con sus estados y sus comentarios. Se verifica listándolos después | Paso 8 |

## 6. Excepciones y errores

| Motivo interno | Código del contrato | Respuesta | Causa |
| --- | --- | --- | --- |
| — | `CONTRATO_CAMPO_REQUERIDO_AUSENTE` | `400` | La solicitud llega sin identificador de cuenta |
| `CUENTA_INEXISTENTE` | `CONTRATO_ALUMNO_NO_ENCONTRADO` | `404` | La cuenta referenciada no existe. **Acá no se oculta nada**, porque la operación ya exigió la facultad de administrador |
| `RESETEO_ACOTADO_A_CUENTAS_DE_ALUMNO` | `CONTRATO_RESETEO_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` | `409` | Se pidió el reseteo sobre la cuenta con papel `Administrador`. **No es `403`**: quien pide **tiene** la facultad, y lo que no procede es la operación sobre esa cuenta. El camino que sí existe es el cambio de la propia contraseña, por **A-05** |
| `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` | `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` | `403` | Quien pide no tiene papel `Administrador`. **No se recupera ni se modifica nada.** El código entró al conjunto cerrado por el `PRODUCT-INTAKE` 1.29 y **nombra el reseteo** entre sus tres caminos: ver §10 |
| `VALOR_DERIVADO_VACIO` | — | `500` | El valor derivado de la provisoria llegó vacío. **Desde que la provisoria la produce el sistema, esta condición no puede nacer de lo que escriba una persona sino de un defecto de quien la produce**; se conserva declarada en lugar de suponerla imposible, **porque suponerla imposible es como se termina escribiendo una credencial vacía** |
| `RESETEO_CON_ARRASTRE_DE_TRABAJOS` | — | — | La solicitud declara que el reseteo elimina los trabajos o cambia la situación. **Inalcanzable desde A-09 por construcción**: la superficie no declara ninguno de los dos efectos |
| — | `CONTRATO_ERROR_NO_CLASIFICADO` | `503` | El almacén no está disponible **o el mecanismo de producción de la provisoria no respondió** |
| ~~`CUENTA_NO_HABILITADA_PARA_CREDENCIAL`~~ | ~~`CONTRATO_RESETEO_NO_APLICABLE_A_CUENTA_SIN_CONTRASENA`~~ | — | **Retirados.** El primero exigía que la cuenta estuviera habilitada, y el Product Owner resolvió que el reseteo no lo exige; el segundo rechazaba el reseteo sobre una cuenta sin credencial, porque el camino declarado para ella era el primer ingreso anónimo, que **RN-02016** suprimió. En los dos casos **es una causa que dejó de existir, no un rechazo que se relaja**, y su retiro cierra la tensión con **RN-02015**, que declara que el reseteo procede sobre `Pendiente`. **Ninguno de los identificadores se recicla.** Se conservan tachados para que una cita vieja no quede sin respuesta |

**Ninguna fila por cuenta no habilitada, y su ausencia es informativa.** El ensamblado de contratos lo
declara explícitamente: de las dos causas de reseteo rechazado que se habían llegado a plantear, la de
la cuenta no habilitada **dejó de existir**. Esta superficie no la repone, y **CA-09 lo verifica sobre
la superficie**.

**Cuando la provisoria no se pudo producir, el reseteo no se completa.** El mecanismo termina de forma
degradada en lugar de componer el valor por otro medio, y esta superficie lo transporta como fallo.
**Un reseteo que no se completa es recuperable —se vuelve a pedir— y una provisoria adivinable no se
nota hasta que alguien la usa.**

**Ninguna condición deja efecto parcial:** el reseteo escribe credencial, marca y sello, o no escribe
nada. **No existe ningún camino por el que la credencial se reemplace y la marca no se ponga, ni al
revés.**

## 7. Postcondiciones

- **Éxito:** el portal tiene la provisoria **una sola vez**; la cuenta tiene la credencial provisoria
  como derivada vigente, la **marca puesta** y el sello del reloj. **Su situación, su papel, su
  identidad, su correo y todos sus trabajos —con sus estados, sus observaciones y sus comentarios—
  quedan exactamente como estaban** (RN-02012).
- **Éxito, y es la mitad que importa:** mientras la marca esté puesta, la cuenta **no ejerce ninguna
  otra capacidad del sistema** (INV-09) y cualquier otra petición suya recibe el `403` de la guardia,
  **aunque su sesión siga siendo válida**. La marca la levanta **únicamente** el cambio efectivo que
  hace la propia cuenta, en `CU-00022`.
- **Fallo:** el almacén queda como estaba, **la credencial anterior sigue vigente** y no se produjo
  ninguna provisoria.
- **En ningún caso se retira un trabajo:** el reseteo **no es una baja** y **no dispara RN-02007**.
- **En los dos casos:** la provisoria **no aparece en ninguna traza**, ni siquiera parcialmente.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un alumno `Habilitado` con credencial, **3** trabajos —1 en `Borrador`, 1 en `Rechazado` con comentario y 1 en `Finalizado`— y el reloj fijado en 2026-03-20 | Se invoca A-09 | Responde `200` con la provisoria; la cuenta queda `Habilitado`, con la credencial provisoria, la marca puesta y sello 2026-03-20, y **conserva los 3 trabajos con sus 3 estados y su comentario**: **0 pérdidas** |
| CA-02 | Una cuenta en situación `Bloqueado` con 2 trabajos | Se invoca A-09 | Responde `200` y la cuenta **sigue en `Bloqueado`** con sus 2 trabajos: **0 rechazos** por la situación de la cuenta |
| CA-03 | La misma cuenta | Se invoca A-09 **dos** veces seguidas | Las 2 respuestas son `200`, las **2** provisorias son **distintas** y la marca queda puesta las dos veces sin acumularse |
| CA-04 | La cuenta con papel `Administrador` | Se invoca A-09 sobre ella | Responde **`409`** con su código propio —**no `403`**— y su credencial **no cambió** |
| CA-05 | Una cuenta de alumno `Pendiente`, nunca habilitada y **sin credencial** | Se invoca A-09 | Responde **`200`** con su provisoria y la situación `Pendiente` sin cambio: **0 rechazos** por la ausencia de credencial previa |
| CA-06 | Una cuenta con la contraseña reseteada | Se invoca **cualquier otro punto** que exija sesión, con una sesión obtenida **antes** del reseteo | Responde `403` con el código de cambio requerido: **la marca corta aunque la sesión siga siendo válida** |
| CA-07 | La misma cuenta reseteada | El alumno cambia su contraseña por A-05 declarando la provisoria como vigente | La marca queda levantada, los puntos de CA-06 vuelven a proceder y sus trabajos siguen intactos |
| CA-08 | El mecanismo de producción de la provisoria que no responde | Se invoca A-09 | Responde con fallo, **0** provisorias producidas y la credencial anterior **sigue vigente** |
| CA-09 | El punto A-09 | Se inspecciona su superficie | **0 parámetros** de situación de cuenta, **0 parámetros** de efecto sobre los trabajos, **0 campos** de contraseña y **0 filas** de respuesta por cuenta no habilitada |
| CA-10 | Una cuenta `Pendiente` con credencial | Se la resetea y **después** se la habilita; y en otra corrida se la habilita y después se la resetea | Los **2** órdenes terminan en el mismo lugar: cuenta `Habilitado`, credencial provisoria y marca puesta. **El orden no cambia el resultado** |
| CA-11 | Una sesión con papel `Alumno` | Se invoca A-09 | Responde `403` y **0** credenciales de la cuenta destino cambian |
| CA-12 | La respuesta de un reseteo con éxito y el registro del servidor | Se inspeccionan | La provisoria aparece **exactamente 1 vez**, en el cuerpo de la respuesta, y **0 veces** en el registro del servidor |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | [NB-00002](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00002-Identidad-Propia-Del-Alumno-Sin-Correo.md), en el circuito de identidad sin canal de correo |
| Reglas de negocio aplicables | [RN-02012](../Reglas-De-Negocio/RN-02012-Reseteo-Conserva-La-Cuenta-Y-Sus-Trabajos.md), que es la promesa que este caso de uso hace observable. [RN-02013](../Reglas-De-Negocio/RN-02013-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md), en la marca que el reseteo pone y que sólo la propia cuenta levanta. [RN-02001](../Reglas-De-Negocio/RN-02001-Administrador-Unico-Y-Papeles-Fijos.md), en la facultad exigida y en que el reseteo está acotado a cuentas de alumno. [RN-02007](../Reglas-De-Negocio/RN-02007-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md), **por contraste**: el reseteo no la dispara |
| Invariantes del producto | **INV-06**, la cuenta no habilitada no obtiene acceso, y por eso la situación no cambia acá. **INV-09**, la cuenta marcada no ejerce ninguna capacidad salvo cambiar su propia contraseña |
| Reglas de arquitectura del producto | **RA-01**, el único invocante legítimo es el portal, servidor a servidor. **RA-03**, ninguna respuesta expone secretos ni direcciones, y todo intento queda registrado **sin la provisoria** |
| Puntos de acceso | **A-09** |
| Contrato de uso que transporta | `GeometriaFactory-Contracts` `CU-00004` |
| Puertos que consume | Almacén de cuentas, mecanismo de producción de la provisoria, mecanismo de credenciales, reloj del sistema |
| Historias de usuario a generar en 06 | US-00014, US-00015 |
| Componentes esperados en 05 | El punto de acceso; la orquestación del reseteo con su verificación de facultad; el reemplazo de credencial y la puesta de marca como un solo acto |
| Tests previstos en 08 | Integración por los doce criterios, con un mecanismo de provisoria que no responde para CA-08; inspección de la superficie por CA-09; inspección de que la provisoria aparece exactamente una vez y nunca en el registro (CA-03, CA-12); y la prueba de conmutatividad de habilitar y resetear (CA-10) |

## 10. Notas y supuestos

- **El punto abierto del papel insuficiente está cerrado.**
  `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` entró al conjunto cerrado el **2026-08-12**
  (`PRODUCT-INTAKE` 1.29) y **nombra el reseteo** entre sus tres caminos. `CU-00005` 1.3 seguía
  declarándolo abierto: el alcance de esa propagación incompleta está en `CU-00023` §10.
- **`409` y `403` no son intercambiables acá, y la distinción es de diseño.** El `403` dice que quien
  pide no puede pedir esto; el `409` dice que quien pide puede, y que la operación no procede **sobre
  esa cuenta**. Confundirlos le haría creer al administrador que le falta un permiso.
- **La provisoria se produce, no se escribe.** Que la produzca el sistema es lo que sostiene RN-02014
  —no adivinable y sin repetirse—, y es lo que un campo de texto en el panel no puede garantizar.
- **El único punto de la superficie que devuelve una credencial.** Por eso CA-12 cuenta apariciones en
  lugar de comprobar una ausencia: acá la respuesta correcta **no es cero**, es exactamente una.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-16 | Emisión inicial, como **caso de uso consolidado** de la unidad de entrega por `Audit/Migracion-8.5-Consolidacion-Decidida.md` 1.2 §2.1. Absorbe `CU-00005` 1.3, `CU-04011` 1.5 y `CU-02013` 1.3, que eran **tres vistas de la misma capacidad**. La unión no es la suma: el actor primario pasa a ser el administrador, con el alumno como sujeto y el aula como canal; el flujo declara de punta a punta la producción, derivación, fijación y devolución **por una sola vez** de la provisoria; §6 queda en **una sola tabla** con el motivo interno y su traducción, con `RESETEO_CON_ARRASTRE_DE_TRABAJOS` marcado **inalcanzable por construcción** y **los dos códigos retirados en una sola fila tachada** en lugar de en tres prosas separadas; y los criterios se rehacen sobre la capacidad y quedan **doce**, con **CA-09** verificando en la superficie las cuatro ausencias que las tres vistas afirmaban en prosa. Los tres documentos absorbidos quedan archivados en `_legacy/2026-08-16-consolidacion-8.5/` y citados desde la cabecera. |

## 17. Compatibilidad de la superficie pública

Agregar un parámetro de situación de cuenta, o una fila de respuesta por cuenta no habilitada, repone
una causa que el Product Owner declaró inexistente y contradice CA-02, CA-05 y CA-09. Agregar un campo
de contraseña al punto devuelve al administrador la elección del valor y contradice RN-02014. Devolver
la provisoria más de una vez, persistirla en claro o dejarla en una traza contradice CA-12. Hacer que
el reseteo toque los trabajos o la situación contradice RN-02012 y es exactamente el agujero que F-26
cerró. Responder `403` donde corresponde `409` le hace creer al administrador que le falta un permiso.
