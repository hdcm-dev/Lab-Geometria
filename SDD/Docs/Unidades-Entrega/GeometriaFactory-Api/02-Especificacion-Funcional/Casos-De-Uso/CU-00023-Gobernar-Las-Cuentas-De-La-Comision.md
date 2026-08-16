# CU-00023 — Gobernar las cuentas de la comisión

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** CU-00023-Gobernar-Las-Cuentas-De-La-Comision.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-16
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-00001`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00001-Control-De-Admision-Al-Laboratorio.md) §5 (cobertura de las cuatro operaciones); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-03), §4.1 (RN-02001, RN-02007, RN-02014, RN-02016), §17.1.P.5
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico` y `08-Calidad-Y-Pruebas` de la unidad de entrega
**Consolida a:** [`CU-00004`](../../../../_legacy/2026-08-16-consolidacion-8.5/GeometriaFactory-Api/CU-00004-Exponer-El-Gobierno-De-Las-Cuentas-De-La-Comision.md), [`CU-04002`](../../../../_legacy/2026-08-16-consolidacion-8.5/GeometriaFactory-Api/CU-04002-Gobernar-Las-Cuentas-De-La-Comision.md) y [`CU-02002`](../../../../_legacy/2026-08-16-consolidacion-8.5/GeometriaFactory-Api/CU-02002-Gobernar-El-Ciclo-De-Vida-De-La-Cuenta.md), por `Audit/Migracion-8.5-Consolidacion-Decidida.md` 1.2 §2.1

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

Que el administrador vea las cuentas de su comisión y gobierne la admisión de cada una: **habilitar**,
**bloquear**, **rehabilitar** y **dar de baja**. Las cuatro son el mismo acto de admisión en cuatro
momentos de la vida de la cuenta, y por eso son un solo caso de uso.

| Punto de acceso | Qué ejerce |
| --- | --- |
| **A-06** | El listado de cuentas de la comisión |
| **A-07** | El cambio de situación: habilitar, bloquear, rehabilitar |
| **A-08** | La baja física |

**Habilitar produce la contraseña de la cuenta, y ése es el corazón de este caso de uso.**
**RN-02016** declara que habilitar —y rehabilitar— produce una contraseña **provisoria** con el mismo
mecanismo y el mismo tratamiento que el reseteo: el sistema la produce, la fija ya derivada, deja la
cuenta con **cambio de contraseña pendiente**, y la respuesta se la devuelve al administrador **una
sola vez** para que se la comunique en persona. La consecuencia estructural es que **ninguna cuenta
llega a `Habilitado` sin credencial**, y es lo que le permitió al producto suprimir el punto de
establecimiento anónimo de contraseña. La habilitación deja entonces de ser una transición pura de la
máquina de estados: **exige la credencial derivada provisoria**.

**Las cuatro operaciones alcanzan sólo a las cuentas con papel `Alumno`**, y no es una restricción de
este documento: es el enunciado literal de la capacidad F-03. Sobre la cuenta de administrador **no
procede ninguna de las cuatro**.

**La baja es la única operación destructiva de toda esta superficie.** Elimina la cuenta **y todos sus
trabajos**, cualquiera sea su estado, y no se deshace. Por eso su punto de acceso transporta un campo
que ningún otro tiene —**el correo escrito como confirmación**— y por eso este documento declara qué
pasa cuando no coincide antes que ninguna otra cosa.

**El reseteo de contraseña no es una quinta operación de acá, y es `CU-00024`.** Lo que lo separa no
es el tratamiento de la provisoria, que desde el intake 1.13 es el mismo, sino que **no es una
transición de la máquina de estados** y que procede sobre las tres situaciones sin alterarlas. Se
declara la frontera para que la ausencia no se lea como olvido.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Administrador | Primario | Ejerce las cuatro operaciones desde su panel |
| `GeometriaFactory-Web` | Intermediario | Arma las solicitudes y las envía con la sesión firmada (RA-01) |
| Alumno | Sujeto de la regla | Es el dueño de la cuenta gobernada. **Recibe su provisoria del administrador, en persona** |
| Almacén de cuentas y de trabajos | Sistema | Recupera la cuenta, materializa la transición y **retira los trabajos en la baja** |
| Mecanismo de producción de la provisoria | Sistema | Produce el valor en claro al habilitar o rehabilitar, **no adivinable y sin repetirse** (RN-02014). Es el mismo que consume `CU-00024` |
| Mecanismo de credenciales | Sistema | Deriva la provisoria antes de que el valor llegue al modelo de dominio, que **nunca la conoce en claro** |
| Reloj del sistema | Sistema | Provee el sello de modificación cuando la operación escribe credencial |

**La verificación de facultad se ejerce en el servidor**, y no ocultando un control en la pantalla.

## 3. Precondiciones

- La petición trae sesión firmada con papel `Administrador` y **atravesó la guardia** de `CU-00022`.
- El servicio arrancó y dejó el almacén en condiciones.
- La cuenta destino existe y **tiene papel `Alumno`**.
- Para la baja, el administrador escribió el correo de la cuenta como confirmación (RN-02007).

## 4. Flujo principal

1. El administrador abre el panel de cuentas de su comisión.
2. Llega una petición a **A-06** y se responde `200` con la colección: correo, nombre, apellido,
   situación y fecha de registro de cada cuenta, **y su marca de cambio de contraseña pendiente**.
3. El administrador elige una cuenta `Pendiente` y pide habilitarla.
4. Llega una petición a **A-07** con el identificador de la cuenta y la situación pretendida.
5. Se verifica que el papel de quien pide sea `Administrador` (RN-02001), **antes de recuperar la
   cuenta destino**, y se recupera la cuenta.
6. La situación pretendida es habilitada: se pide la **provisoria** al mecanismo de producción, se la
   **deriva**, y se toma el sello del reloj.
7. Se comprueba que el papel de la cuenta sea `Alumno`, que el par situación actual y transición
   figure en la tabla de transiciones admitidas, y que la credencial derivada provisoria venga
   aportada. Se aplica la transición, se **fija la credencial** y se pone la **marca de cambio de
   contraseña pendiente** (INV-09).
8. Se materializa la situación resultante, la credencial, la marca y el sello **en una única unidad de
   trabajo**.
9. Se responde `200` con la situación resultante y **el valor en claro de la provisoria, una sola
   vez**. El administrador se lo comunica al alumno en persona; el alumno la cambia por `CU-00022`.

**La provisoria en claro no se persiste en ninguna parte y no queda en ninguna traza del servidor.**
Lo que se guarda es su forma derivada.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El administrador **bloquea** una cuenta habilitada | La transición se aplica igual, **sin pedir provisoria**: la credencial y la marca quedan como estaban, y la respuesta **no trae ninguna provisoria** | Paso 9 |
| FA-02 | El administrador **rehabilita** una cuenta bloqueada | Es una habilitación a los efectos de RN-02016: produce una provisoria **nueva**, la fija y pone la marca. La credencial resultante **no es la anterior** | Paso 9 |
| FA-03 | Se pide habilitar o rehabilitar una cuenta que **ya tiene la marca puesta** por un reseteo anterior | Procede igual: provisoria nueva, credencial fijada y **la marca queda puesta**. No hay estado intermedio que distinguir y **la marca no se acumula** | Paso 9 |
| FA-04 | Se pide **habilitar** una cuenta que ya está `Habilitado` | Es **sin efecto respecto de la situación**, y **no se pide provisoria al mecanismo**: producir una sin que nadie la haya pedido dejaría al alumno fuera de su propia cuenta. Para eso está el reseteo de `CU-00024`, que es explícito | Paso 9 |
| FA-05 | El administrador da de **baja** una cuenta por **A-08** | La solicitud trae el identificador **y el correo escrito como confirmación**. Se comparan; si coinciden, se retiran **todos** los trabajos de esa cuenta —en cualquier estado, incluidos los terminales— y recién después la cuenta, **todo en la misma unidad de trabajo** (RN-02007). Se responde `204`, sin cuerpo | Termina |
| FA-06 | El listado se pide sobre una comisión sin ninguna cuenta de alumno | Se responde `200` con una colección **vacía**. **Un listado vacío no es un fallo**: el portal distingue vacío de fallo por el tipo recibido y no por el conteo | Termina |

## 6. Excepciones y errores

| Motivo interno | Código del contrato | Respuesta | Punto | Causa |
| --- | --- | --- | --- | --- |
| — | `CONTRATO_CAMPO_REQUERIDO_AUSENTE` | `400` | A-07, A-08 | Falta el identificador, la situación pretendida o el correo de confirmación. La respuesta **nombra el campo ausente** |
| `CONFIRMACION_DE_BAJA_NO_COINCIDE` | `CONTRATO_CONFIRMACION_NO_COINCIDE` | `400` | A-08 | El correo escrito no coincide con el de la cuenta. **La baja no procede y la respuesta no devuelve el correo esperado.** La unidad de trabajo **no se abre** |
| `CUENTA_INEXISTENTE` | `CONTRATO_ALUMNO_NO_ENCONTRADO` | `404` | A-07, A-08 | La cuenta referenciada no existe |
| `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` | `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` | `403` | Los tres | Quien pide no tiene papel `Administrador`. **No se recupera ni se modifica nada.** Es una negativa **por facultad y no por pertenencia**: acá la existencia de la cuenta destino no se oculta, porque quien pregunta no está pidiendo un recurso ajeno sino ejerciendo una facultad que no tiene |
| `TRANSICION_DE_CUENTA_NO_ADMITIDA` | `CONTRATO_ERROR_NO_CLASIFICADO` | `500` | A-07 | El par situación actual y transición no figura en la tabla. Conserva la situación actual |
| `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` | `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` | `403` | A-07, A-08 | Se pide **cualquiera de las cuatro operaciones** sobre la cuenta con papel `Administrador`. Ninguna tiene inversa posible: la instancia quedaría sin nadie capaz de habilitar, desbloquear y revisar (INV-05, RN-02001) |
| `HABILITACION_SIN_CREDENCIAL_PROVISORIA` | — | `500` | A-07 | Se llegó a la transición de habilitación **sin la credencial derivada provisoria**, porque el mecanismo de producción o el de derivación no la entregaron. Conserva la situación actual: **no hay camino por el que una cuenta quede `Habilitado` sin credencial** |
| `VALOR_DERIVADO_VACIO` | — | `500` | A-07 | El valor derivado de la provisoria llegó vacío. Desde que la provisoria la produce el sistema, esta condición **no puede nacer de lo que escriba una persona** sino de un defecto de quien la produce |
| `BAJA_SIN_ARRASTRE_DE_TRABAJOS` | — | — | A-08 | Se pide la baja declarando que los trabajos se conservan. **Inalcanzable desde A-08 por construcción**: la superficie no declara ninguna opción de conservarlos, y la baja arrastra siempre |
| — | `CONTRATO_ERROR_NO_CLASIFICADO` | `503` | Los tres | El almacén no está disponible. **La respuesta no incluye su ruta** |

**Los dos `403` de facultad llevan código propio desde el `PRODUCT-INTAKE` 1.29.**
`CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` entró al conjunto cerrado el 2026-08-12 y nombra el
gobierno de cuentas entre sus tres caminos. **Fue un punto abierto de esta unidad hasta esa fecha**:
ver §10. Los dos `500` no llevan código del contrato porque **son defectos, no resultados que el
administrador deba ver**.

**La baja no deja retiro parcial.** El adaptador de almacenamiento lo garantiza —todo o nada, en una
sola unidad de trabajo— y esta superficie lo hace observable: **una baja interrumpida responde con
fallo y la cuenta y sus trabajos quedan enteros**. No hay ninguna respuesta que signifique «se borró
una parte».

## 7. Postcondiciones

- **Listado con éxito:** el portal tiene la colección, **sin ninguna forma de la credencial de
  ninguna cuenta**.
- **Habilitación o rehabilitación con éxito:** la cuenta quedó `Habilitado`, con **credencial derivada
  nueva**, la **marca puesta** y el sello del reloj; y la respuesta trajo **el valor en claro de la
  provisoria, exactamente una vez**. **Sus trabajos no se tocaron.** El valor en claro **no quedó
  persistido en ninguna parte** y esta unidad no lo conserva después de devolverlo.
- **Bloqueo con éxito:** la cuenta quedó en la situación resultante; su credencial y su marca **no
  cambiaron** y la respuesta **no trajo ninguna provisoria**.
- **Baja con éxito:** no queda ninguna cuenta con ese correo **ni ningún trabajo cuyo dueño fuera esa
  cuenta**, en ningún estado. Se verifica comprobando que **no queda ningún trabajo** del alumno dado
  de baja.
- **Fallo:** la cuenta y sus trabajos quedan exactamente como estaban, y el intento queda registrado
  del lado del servidor.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Una sesión con papel `Administrador` y tres cuentas de alumno | Se invoca A-06 | Responde `200` con **3** elementos, cada uno con situación y marca, y **0 campos** que transporten una credencial en cualquiera de sus formas |
| CA-02 | Una cuenta `Pendiente` de `ana.perez@ejemplo.edu` | Se la habilita por A-07 | Responde `200` con la situación resultante, **1** contraseña provisoria en claro y el cambio pendiente declarado; la cuenta queda con credencial derivada y marca puesta; sus trabajos quedan **sin ningún cambio**; y quedan **0** provisorias persistidas en claro |
| CA-03 | Dos cuentas `Pendiente` distintas | Se habilitan las dos por A-07, y después se bloquea y se rehabilita la primera | Las **3** provisorias devueltas son **distintas entre sí**, el bloqueo devuelve **0**, y en el registro del servidor la provisoria aparece **0 veces** |
| CA-04 | Una cuenta `Bloqueado` con credencial y sin marca | Se la rehabilita por A-07 | Responde `200` con la marca puesta y una credencial derivada que **no es la anterior** |
| CA-05 | Una cuenta `Pendiente` y un mecanismo de producción de provisoria que falla | Se la habilita por A-07 | Responde con fallo, la cuenta sigue `Pendiente` y quedan **0** cuentas `Habilitado` sin credencial derivada |
| CA-06 | Una cuenta ya `Habilitado` | Se la habilita otra vez por A-07 | La situación no cambia y se piden **0** provisorias al mecanismo: el alumno **no queda fuera de su propia cuenta** |
| CA-07 | Una cuenta `Pendiente` | Se pide bloquearla por A-07 | Responde `403` con transición no admitida y la cuenta sigue `Pendiente` |
| CA-08 | Un alumno con 3 trabajos: 1 en `Borrador`, 1 en `Pendiente` y 1 en `Finalizado` | Se invoca A-08 con el correo correcto | Responde `204` y quedan **0** trabajos de ese alumno |
| CA-09 | El mismo alumno | Se invoca A-08 con un correo de confirmación distinto | Responde `400`, la cuenta **sigue existiendo** y sus **3** trabajos siguen ahí |
| CA-10 | La cuenta con papel `Administrador` | Se invocan sobre ella las **4** operaciones: habilitar, bloquear, rehabilitar y dar de baja | Las **4** responden `403` con el mismo motivo y la cuenta queda `Habilitado` y existiendo: bloquearla dejaría a la instancia sin ninguna cuenta capaz de desbloquearla |
| CA-11 | Una sesión con papel `Alumno` | Se invocan los **3** puntos | Las 3 responden `403` y **0 de ellas leen o modifican** ninguna cuenta |
| CA-12 | Un identificador de cuenta que no existe | Se invocan A-07 y A-08 | Las **2** responden `404` |
| CA-13 | El almacén interrumpido a mitad de una baja | Se invoca A-08 | Responde `503`, y la cuenta y **todos** sus trabajos siguen enteros: **0 retiros parciales** |
| CA-14 | Cualquier respuesta de §6, con el cuerpo y el registro del servidor observados | Se produce la condición | **0 apariciones** de la provisoria en claro, de su valor derivado, de la clave de firma, de la ruta del almacén y de la dirección de cualquier servicio interno |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | [NB-00001](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00001-Control-De-Admision-Al-Laboratorio.md), en su criterio de cobertura de las cuatro operaciones |
| Reglas de negocio aplicables | [RN-02001](../Reglas-De-Negocio/RN-02001-Administrador-Unico-Y-Papeles-Fijos.md), en la facultad exigida y en que las cuatro operaciones quedan cerradas sobre la cuenta de administrador. [RN-02007](../Reglas-De-Negocio/RN-02007-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md), en la baja con arrastre y su confirmación escrita. [RN-02006](../Reglas-De-Negocio/RN-02006-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md), en lo que la situación resultante habilita. [RN-02013](../Reglas-De-Negocio/RN-02013-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md), en la marca que la habilitación deja puesta |
| Invariantes del producto | **INV-05**, la instancia conserva siempre su cuenta de administrador. **INV-09**, la cuenta con la marca no ejerce ninguna capacidad salvo cambiar su propia contraseña |
| Reglas de arquitectura del producto | **RA-01**, el único invocante legítimo es el portal, servidor a servidor. **RA-03**, ninguna respuesta expone secretos ni direcciones, y todo intento queda registrado |
| Puntos de acceso | **A-06** el listado, **A-07** el cambio de situación, **A-08** la baja |
| Contrato de uso que transporta | `GeometriaFactory-Contracts` `CU-00003` |
| Puertos que consume | Almacén de cuentas, almacén de trabajos, mecanismo de producción de la provisoria, mecanismo de credenciales, reloj del sistema |
| Historias de usuario a generar en 06 | US-00011, US-00012, US-00013 |
| Componentes esperados en 05 | Los tres puntos de acceso; la orquestación de las cuatro operaciones con su verificación de facultad; la máquina de transiciones de situación; el arrastre de trabajos en una sola unidad de trabajo |
| Tests previstos en 08 | Integración por los catorce criterios, con el almacén interrumpido para CA-13 y un mecanismo de provisoria que falla para CA-05; inspección de que la provisoria no aparece en ninguna traza (CA-03, CA-14); y la prueba de que las **cuatro** operaciones están cerradas sobre la cuenta de administrador (CA-10) |

## 10. Notas y supuestos

- **El punto abierto del papel insuficiente está cerrado, y las vistas de origen no lo habían
  absorbido.** Hasta el `PRODUCT-INTAKE` 1.29 el conjunto cerrado declaraba
  `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` sólo para el desenlace, y estos caminos respondían
  `403` **sin código propio**. La decisión del Product Owner del **2026-08-12** incorporó
  `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR`, con destino `403`, y nombra exactamente tres
  caminos: **gobierno de cuentas, listado de la comisión y reseteo**. La propagación llegó a
  [`Definicion-Superficie-HTTP.md`](../Definicion-Superficie-HTTP.md) §6 y §9, al índice maestro, a
  `05-Arquitectura-Tecnica` y a `03-UX-UI-DX`, **pero no a los casos de uso**, que seguían
  declarándolo abierto. **La consolidación lo absorbe**, y el hecho queda registrado porque es el tipo
  de propagación incompleta que sólo se ve al leer las vistas juntas.
- **`CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA` está retirado y no se recicla.** Cubría una sola de las
  cuatro operaciones y dejaba las otras tres sin guarda, que es como se llegó al hallazgo H-01 de la
  ronda r3. Toda cita anterior resuelve a `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR`, que
  cubre las cuatro.
- **La marca no se acumula.** Habilitar sobre una cuenta que ya la tenía puesta produce una provisoria
  nueva y **deja la marca puesta**: no hay estado intermedio que distinguir.
- **La idempotencia de habilitar es respecto de la situación, no de la credencial.** Por eso FA-04 no
  pide provisoria: si la pidiera, cada clic repetido dejaría al alumno fuera de su cuenta.
- **El producto no envía correo.** La provisoria viaja del sistema al **administrador**, una sola vez,
  y de él al alumno **en persona, por fuera del producto**. Es lo que sostiene el circuito sin canal
  de correo.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-16 | Emisión inicial, como **caso de uso consolidado** de la unidad de entrega por `Audit/Migracion-8.5-Consolidacion-Decidida.md` 1.2 §2.1. Absorbe `CU-00004` 1.3, `CU-04002` 1.3 y `CU-02002` 1.3, que eran **tres vistas de la misma capacidad**. La unión no es la suma: el actor primario pasa a ser el administrador; el flujo declara de punta a punta la producción, derivación, fijación y devolución **por una sola vez** de la provisoria, que las tres vistas contaban por tramos; §6 queda en **una sola tabla** con el motivo interno y su traducción, con `BAJA_SIN_ARRASTRE_DE_TRABAJOS` marcado **inalcanzable por construcción** desde A-08 en lugar de omitido, y con los tres `403` sin código del contrato declarados como **punto abierto** en un solo lugar; y los criterios se rehacen sobre la capacidad y quedan **catorce**, **con los dos `CA-08` del origen desambiguados** —eran dos criterios distintos con el mismo identificador— y con **CA-10** cubriendo las cuatro operaciones sobre la cuenta de administrador en un solo criterio. Los tres documentos absorbidos quedan archivados en `_legacy/2026-08-16-consolidacion-8.5/` y citados desde la cabecera. |

## 17. Compatibilidad de la superficie pública

Quitar el correo de confirmación de la baja, o dejar de compararlo, contradice RN-02007 y CA-09.
Admitir una baja que conserve los trabajos deja trabajos huérfanos y contradice el invariante que la
sostiene. Devolver la provisoria más de una vez, persistirla en claro o dejarla en una traza
contradice CA-03 y CA-14. Hacer que habilitar una cuenta ya habilitada produzca una provisoria nueva
contradice CA-06 y deja al alumno fuera de su cuenta. Abrir cualquiera de las cuatro operaciones sobre
la cuenta de administrador contradice INV-05. Agregar al listado cualquier campo que transporte una
credencial, en la forma que sea, contradice CA-01.
