# CU-00025 — Configurar la cuenta de administrador en el primer arranque

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** CU-00025-Configurar-La-Cuenta-De-Administrador-En-El-Primer-Arranque.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-16
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-00001`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00001-Control-De-Admision-Al-Laboratorio.md) §5; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-01), §4.1 (RN-02001, RN-02002), §17.1.P.2 · GeometriaFactory-Domain (INV-01) y §17.1.P.5 · GeometriaFactory-Domain, y el guion de la etapa `c`
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico` y `08-Calidad-Y-Pruebas` de la unidad de entrega
**Consolida a:** `CU-00003` §A-03 y §A-17, [`CU-04010`](../../../../_legacy/2026-08-16-consolidacion-8.5/GeometriaFactory-Api/CU-04010-Configurar-La-Cuenta-De-Administrador.md) y [`CU-02012`](../../../../_legacy/2026-08-16-consolidacion-8.5/GeometriaFactory-Api/CU-02012-Configurar-La-Cuenta-De-Administrador.md), por `Audit/Migracion-8.5-Consolidacion-Decidida.md` 1.2 §2.1

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

Que el docente cree la **única** cuenta con papel `Administrador` del laboratorio, en el primer
arranque y **sólo mientras no exista ninguna**. Es el acto que crea la primera identidad de la
instancia, y por eso es el único camino de alta que **no espera la habilitación de nadie**: no hay
ninguna cuenta anterior que pudiera darla.

| Punto de acceso | Qué ejerce |
| --- | --- |
| **A-03** | Configura la cuenta de administrador. **Cierra la ventana de alta** |
| **A-17** | Responde **si el laboratorio ya tiene administrador, y nada más**. De sólo lectura. **Dice si la ventana sigue abierta** |

**A-17 es hermano de A-03, y por eso están en el mismo caso de uso: es la misma ventana de alta,
mirada desde afuera.** Existe porque el **guardián 1** de `Web ADR-00003` §2 —«mientras no exista la
cuenta de administrador, cualquier ruta pedida desvía al aprovisionamiento inicial; una vez que
existe, esa ruta deja de armar formulario para siempre y desvía de forma neutra»— **nunca se pudo
construir**: ninguno de los puntos anteriores servía para que un anónimo preguntara eso. A-03
configura —es escritura—, el punto de salud responde por el servicio y el listado exige ser
`Administrador`. El detalle de la decisión, lo que el punto revela y por qué no se le metió el dato al
punto de salud están en
[`Definicion-Superficie-HTTP.md`](../Definicion-Superficie-HTTP.md) §3.

**Es el segundo de los dos caminos de alta del producto, y tiene las reglas opuestas al otro.** El
auto-registro del alumno es `CU-00021` y deja la cuenta `Pendiente` y **sin** credencial; acá la
cuenta nace **`Habilitado` y con credencial**. Son dos caminos y no uno con un flujo alternativo
precisamente porque **ninguna de sus reglas coincide**: situación inicial, credencial y ventana de
alta.

**La configuración incluye la contraseña, y el auto-registro no.** El motivo es el guion de la etapa
`c`: el docente tiene que poder **entrar inmediatamente después de configurarse**, y no hay ninguna
otra cuenta que pudiera darle una provisoria.

**Ninguno de los dos puntos exige acceso, y en los dos es el requisito y no una concesión.** En el
primer arranque no hay ninguna cuenta con la que obtenerlo; y quien pregunta por A-17 es el guardián,
que corre **antes** de que haya con qué identificarse. Lo que acota a A-03 **no es un papel sino la
existencia**: sólo procede mientras no exista ninguna cuenta de administrador.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Docente, en su papel de administrador | Primario | Configura su cuenta en el primer arranque del laboratorio |
| `GeometriaFactory-Web` | Intermediario | Arma la solicitud desde el formulario de aprovisionamiento inicial y la envía servidor a servidor. **Y consulta A-17 desde su guardián, antes de que nadie se haya identificado**: lo consulta un intermediario del servidor del portal y **ningún guion del navegador lo alcanza** (RA-01) |
| Alumno | Sujeto de la regla | No interviene. Su alta es el otro camino, y su habilitación depende de que esta cuenta exista |
| Almacén de cuentas | Sistema | Responde si ya existe administrador y si el correo está libre, y materializa la cuenta |
| Mecanismo de credenciales | Sistema | Deriva la contraseña antes de que el valor llegue al modelo de dominio, que **nunca la conoce en claro** |
| Reloj del sistema | Sistema | Provee el sello de alta, para que sea verificable en prueba |

## 3. Precondiciones

- El servicio arrancó y dejó el almacén en condiciones.
- **La petición no trae acceso y no lo necesita**, en los dos puntos.
- Para **A-03**, la solicitud aporta **correo, nombre, apellido y contraseña**.
- Para **A-17**, la solicitud llega **sin cuerpo**. **No hay ningún estado del laboratorio que la
  vuelva inválida**: «todavía no» es una respuesta legítima y no un fallo.

## 4. Flujo principal

1. Alguien pide una ruta del portal en un laboratorio recién instalado. El guardián del portal
   consulta **A-17**, sin cuerpo y sin acceso.
2. Se consulta si existe cuenta con papel `Administrador` y se responde `200` con **ese único dato**.
   **No se escribe nada.**
3. Todavía no existe: el portal desvía al aprovisionamiento inicial y el docente completa correo,
   nombre, apellido y contraseña.
4. Llega una petición a **A-03** con esos datos.
5. Se consulta al almacén si **ya existe alguna cuenta con papel `Administrador`** (RN-02001,
   INV-05).
6. No existe: se consulta si el correo aportado ya está registrado (RN-02002, INV-01).
7. El correo está libre: se **deriva** la contraseña y se toma el sello de alta del reloj.
8. Se constituye la cuenta, declarando la ausencia de administrador previo y la verificación de
   unicidad del correo. Se verifica que correo, nombre y apellido estén presentes y no vacíos y que la
   credencial derivada no esté vacía; se fija el papel en `Administrador`, la situación en
   **`Habilitado`** —no en `Pendiente`: **esta cuenta es la que habilita a las demás**— y se adopta la
   credencial derivada. El conjunto de trabajos queda vacío.
9. Se materializa la cuenta **en una única unidad de trabajo** y se responde `201`.
10. Desde ese mismo momento la cuenta **admite acceso**: el docente entra por `CU-00022` y cambia su
    contraseña, y el guion de la etapa `c` —configurar, entrar, cambiar contraseña, salir— es
    recorrible de punta a punta.
11. Una consulta posterior a **A-17** responde `200` diciendo que ya existe, y **la ventana de alta
    queda cerrada para siempre**.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | Ya existe una cuenta de administrador y llega otra petición a **A-03** | Se responde `409` **sin consultar el correo y sin escribir nada**. **El contrato no ofrece camino alternativo y la respuesta no sugiere ninguno.** La ventana se cierra con la primera configuración y **no vuelve a abrirse** | Termina |
| FA-02 | El docente cambia su contraseña inmediatamente después de entrar, como pide el guion de la etapa `c` | **No es este caso de uso**: es el reemplazo de `CU-00022`, que exige la vigente verificada. Acá la credencial **nace fijada**, de modo que el camino de fijación por primera vez **no aplica a esta cuenta** | Termina; sigue `CU-00022` |
| FA-03 | El almacén rechaza la materialización por una colisión que las consultas de los pasos 5 y 6 no vieron | No se materializa nada y se devuelve el motivo. **Las comprobaciones previas no son la garantía por sí solas** | Termina |
| FA-04 | Se consulta **A-17** en un laboratorio que ya tiene administrador | Se responde `200` con ese dato. **Es el mismo código de respuesta que en el otro estado**: este punto tiene **un solo código**, porque los dos estados son respuestas legítimas | Termina |

## 6. Excepciones y errores

| Motivo interno | Código del contrato | Respuesta | Punto | Causa |
| --- | --- | --- | --- | --- |
| `REQUIRED_FIELD_MISSING` | `REQUIRED_FIELD_MISSING` | `400` | A-03 | Falta el correo, el nombre, el apellido o la contraseña. La respuesta **nombra el campo ausente** |
| `ADMINISTRATOR_ALREADY_CONFIGURED` | `ADMINISTRATOR_ALREADY_CONFIGURED` | `409` | A-03 | Ya existe una cuenta con papel `Administrador` |
| `EMAIL_ALREADY_REGISTERED` | `EMAIL_ALREADY_REGISTERED` | `409` | A-03 | El correo ya pertenece a una cuenta. **La respuesta no declara el papel ni la situación de esa cuenta** |
| `SETUP_WITHOUT_CREDENTIAL` | `REQUIRED_FIELD_MISSING` | `400` | A-03 | No se aporta contraseña, o su valor derivado está vacío. **Una cuenta de administrador sin credencial no podría entrar, y no hay ninguna otra que pudiera resolverlo** |
| `EMAIL_UNIQUENESS_NOT_VERIFIED` | — | — | A-03 | Se solicita la constitución sin declarar que la unicidad fue comprobada. **Inalcanzable por construcción**: el paso 8 declara siempre la verificación que el paso 6 hizo |
| `INITIAL_STATUS_NOT_NEGOTIABLE` | — | — | A-03 | Se pide constituirla en una situación distinta de `Habilitado`. **Inalcanzable desde A-03 por construcción**: la superficie no declara la situación. Sigue declarado porque protege a la cuenta de cualquier consumidor interno: una cuenta de administrador `Pendiente` **dejaría a la instancia sin salida**, porque nadie podría habilitarla y por INV-06 tampoco obtendría acceso |
| — | `UNCLASSIFIED_ERROR` | `503` | A-03 | El almacén no está disponible. **La respuesta no incluye su ruta** |

**A-17 no tiene ninguna condición de error, y su ausencia es informativa.** Es de sólo lectura, no
declara ningún campo obligatorio y **no hay estado del laboratorio que vuelva inválida la petición**:
por eso tiene **un solo código de respuesta**, `200`. La única condición que podría alcanzarlo es la
indisponibilidad del almacén, que es la misma fila de arriba.

**Ninguna condición deja efecto parcial:** la unidad de trabajo no se abre hasta el paso 9. Y **ninguna
devuelve una contraseña, en claro ni derivada, ni la registra**.

## 7. Postcondiciones

- **A-03 con éxito:** existe **exactamente una** cuenta con papel `Administrador`, situación
  `Habilitado`, credencial derivada con valor, sello de alta y **0 trabajos**. **Admite acceso desde
  ese mismo momento**, sin ningún motivo pendiente. **Ninguna petición posterior a ese punto puede
  crear otra.**
- **A-17 con éxito:** el portal sabe si la ventana sigue abierta, y **nada cambió**: este punto no
  escribe.
- **Fallo de A-03:** no se constituye ninguna cuenta, **la instancia sigue sin administrador** y este
  mismo caso de uso **vuelve a estar disponible** — que es lo que lo distingue de todos los demás
  fallos de esta superficie.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Una instancia **sin** ninguna cuenta con papel `Administrador` y el reloj fijado en 2026-03-01 | Se invoca A-03 con `docente@ejemplo.edu`, «Fernando», «Filipuzzi» y una contraseña | Responde `201`; la cuenta queda con papel `Administrador`, situación **`Habilitado`**, credencial derivada con valor, sello de alta 2026-03-01 y **0** trabajos — **la situación inicial opuesta a la de A-02** |
| CA-02 | La cuenta configurada por CA-01 | El docente entra por A-01 con su contraseña | Responde `200` con su sesión y **0** motivos pendientes: **entra inmediatamente después de configurarse**, como exige el guion de la etapa `c` |
| CA-03 | Una instancia con administrador ya configurado | Se invoca A-03 | Responde `409` y **sigue existiendo exactamente 1** cuenta con papel `Administrador` |
| CA-04 | Una instancia **sin** administrador pero con una cuenta cuyo correo es `docente@ejemplo.edu` | Se invoca A-03 con ese correo | Responde `409`, el cuerpo **no declara el papel ni la situación** de la cuenta que lo ocupa, y **0** cuentas de administrador quedan creadas |
| CA-05 | Una instancia sin administrador | Se invoca A-03 **sin contraseña** | Responde `400` nombrando el campo ausente, y la instancia **sigue sin administrador**: el caso de uso **vuelve a estar disponible** |
| CA-06 | Una instancia **sin** administrador y la misma instancia **con** administrador ya configurado | Se invoca **A-17 sin acceso** en cada estado | Responde **`200` en los dos**, con el dato que corresponde; y el cuerpo lleva **exactamente un dato**: **0 apariciones** de correo, nombre, fecha o cantidad de cuentas |
| CA-07 | Una instancia sin administrador | Se invoca A-17 y después se comprueba el almacén | El almacén queda **sin ningún cambio**: el punto **no escribe** |
| CA-08 | El punto A-17 | Se inspecciona su superficie | Declara **1** código de respuesta, **0** campos de cuerpo en la petición y **0** exigencias de acceso |
| CA-09 | La superficie de A-03 | Se inspecciona | Declara correo, nombre, apellido y contraseña, y **0** campos de papel y de situación |
| CA-10 | Cualquier respuesta de §6, con el cuerpo y el registro del servidor observados | Se produce la condición | **0 apariciones** de la contraseña recibida, de su valor derivado, de la clave de firma y de la ruta del almacén |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | [NB-00001](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00001-Control-De-Admision-Al-Laboratorio.md), por la configuración inicial que hace posible toda admisión posterior |
| Reglas de negocio aplicables | [RN-02001](../Reglas-De-Negocio/RN-02001-Administrador-Unico-Y-Papeles-Fijos.md), en la ventana de alta y en su negativa. [RN-02002](../Reglas-De-Negocio/RN-02002-Correo-Del-Alumno-Unico.md), en la traducción del correo ocupado. [RN-02006](../Reglas-De-Negocio/RN-02006-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md), **por contraste**: es la única cuenta del producto que nace habilitada |
| Invariantes del producto | **INV-01**, correo único en todo el sistema. **INV-05**, la instancia tiene exactamente una cuenta de administrador. **INV-06**, por lo que una cuenta de administrador `Pendiente` sería un estado sin salida |
| Reglas de arquitectura del producto | **RA-01**, los dos puntos los ejerce el portal servidor a servidor aunque **ninguno** exija acceso. **A-17 es el caso donde RA-01 más importa**: ningún guion del navegador lo alcanza. **RA-03**, en las condiciones de §6 |
| Puntos de acceso | **A-03** la configuración, **A-17** la consulta de si ya existe |
| Contrato de uso que transporta | `GeometriaFactory-Contracts` `CU-00002` |
| Decisión que habilita | `Web ADR-00003` §2, **guardián 1**, que **no se podía construir** sin A-17 |
| Puertos que consume | Almacén de cuentas, mecanismo de credenciales, reloj del sistema |
| Historias de usuario a generar en 06 | US-00008, US-00010 |
| Componentes esperados en 05 | Los dos puntos de acceso; la constitución de la cuenta con su ventana de alta; el guardián del portal que consume A-17 |
| Tests previstos en 08 | Integración por los diez criterios, con la instancia en **los dos estados** para CA-06; inspección de superficie por CA-08 y CA-09; la prueba de que A-17 no escribe (CA-07); y la prueba de punta a punta del guion de la etapa `c` (CA-01 más CA-02) |

## 10. Notas y supuestos

- **La ventana de alta se cierra por existencia, no por papel.** No hay ningún permiso que proteja a
  A-03, porque en el primer arranque no hay con qué tenerlo. Lo que lo protege es que **sólo procede
  mientras no exista ninguna cuenta de administrador**, y esa comprobación es sobre el conjunto de
  cuentas.
- **A-17 revela un solo bit, y es deliberado.** Decir «todavía no hay administrador» en un laboratorio
  recién instalado no es información sensible: es exactamente lo que el portal necesita para no armar
  un formulario que va a fallar. Decir cuántas cuentas hay, o quién es el administrador, sí lo sería,
  y por eso CA-06 cuenta **0 apariciones** de esos datos.
- **Por qué el dato no se agregó al punto de salud.** El punto de salud responde por el servicio, y
  este dato es del laboratorio. Mezclarlos habría hecho que el guardián dependiera de un punto cuya
  forma cambia por otros motivos. Está registrado en
  [`Definicion-Superficie-HTTP.md`](../Definicion-Superficie-HTTP.md) §3.
- **`INITIAL_STATUS_NOT_NEGOTIABLE` protege el único estado sin salida del producto.** Una cuenta de
  administrador `Pendiente` no obtendría acceso por INV-06 y nadie podría habilitarla, porque la única
  cuenta capaz de hacerlo **sería ella misma**.
- **La decisión de incorporar A-17 la tomó el orquestador, con el Product Owner avisado, y quedaba a
  ratificación** en `CU-00003` 1.5. Este documento la hereda con el mismo estado.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.1 | 2026-08-29 | **Tramo `R-3b` del renombre `F-03`**, reactivado por el Product Owner el 2026-08-29 y registrado en [`../../../../Producto/Norma-De-Nomenclatura.md`](../../../../Producto/Norma-De-Nomenclatura.md) §8. **8 línea(s)** de este documento pasan los códigos de condición de la forma castellana a la vigente, con el mapeo de **§6.8** —101 pares— y **sin elegir ninguno acá**. Se respeta **§4.1**: no se tocan las filas de control de cambios ni lo que está entre «…». **Ninguna palabra de prosa cambia**, verificado con el control de diff del tramo. |
| 1.0 | 2026-08-16 | Emisión inicial, como **caso de uso consolidado** de la unidad de entrega por `Audit/Migracion-8.5-Consolidacion-Decidida.md` 1.2 §2.1. Absorbe los puntos **A-03 y A-17** de `CU-00003` 1.5, `CU-04010` 1.0 y `CU-02012` 1.2. **Es el caso de uso que la tabla de consolidación 1.0 dejaba sin ningún documento de la capa `Api`**, y ése fue el indicio que llevó a descubrir que los documentos de esa capa agrupan por perfil de autenticación y no por capacidad (§2.1.1 del documento de consolidación). La unión no es la suma: el actor primario pasa a ser el docente; el flujo empieza en la consulta del guardián y termina en la ventana cerrada, que es lo que hace visible por qué **A-17 y A-03 son el mismo caso de uso**; §6 queda en una sola tabla con los dos motivos **inalcanzables por construcción** marcados y con la constancia de que **A-17 no tiene condiciones de error**; y los criterios se rehacen sobre la capacidad y quedan **diez**, con **CA-08** y **CA-09** verificando en la superficie lo que las tres vistas afirmaban en prosa. Los dos documentos absorbidos enteros quedan archivados en `_legacy/2026-08-16-consolidacion-8.5/` y citados desde la cabecera. |

## 17. Compatibilidad de la superficie pública

Abrir la ventana de alta después de la primera configuración —por cualquier vía— contradice RN-02001 e
INV-05 y es el cambio incompatible más grave de esta superficie. Exigirle acceso a A-03 la vuelve
inalcanzable en el primer arranque, que es el único momento en que se ejerce. Exigirle acceso a A-17
rompe el guardián, porque corre antes de que haya con qué identificarse. Agregarle a A-17 cualquier
dato más que el bit —cuántas cuentas hay, quién es el administrador, desde cuándo— contradice CA-06.
Agregarle a A-03 un campo de situación o de papel contradice CA-09 y reabre el estado sin salida.
Declarar en la respuesta del `409` por correo ocupado el papel o la situación de la cuenta que lo ocupa
contradice CA-04.
