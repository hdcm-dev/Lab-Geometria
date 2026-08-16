# Catálogo de condiciones de error del dominio

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** DX-Error-Messages.md
**Versión:** 1.5
**Estado:** Aprobado
**Fecha:** 2026-08-09
**Autor:** DX Lead (AG-03)
**Variante:** DX
**Trazabilidad upstream:** §6 de los **trece** casos de uso de `02-Especificacion-Funcional/Casos-De-Uso/` (CU-02001 a CU-02013), de donde se deriva cada entrada, con sus §3, §5 y §9; `02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md` §2.1, §2.5, §4.1 (los **nueve** invariantes vigentes), §4.2 (recorrido de adopción de INV-08), §4.3 (correspondencia entre reglas e invariantes), §5.1, §5.2, §5.3 y §7; RN-02001 a **RN-02016** de `02-Especificacion-Funcional/Reglas-De-Negocio/`; `00-Contexto/Vision-Producto.md` §9.1 y §9.2; `01-Necesidades-Negocio/Necesidades-Negocio.md` §2; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.13** §4 (**F-26**, F-03, **F-04** precisada), §4.1 (**RN-02016**), §4.2, §7 (**CL-7**), §9 (**X-2 retirada**), §17.1.P.1, §17.1.P.2, §17.1.P.3, §17.1.P.5, §17.1.P.10
**Trazabilidad downstream:** `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas` y `11-Documentacion` de GeometriaFactory-Domain

---

## Tabla de contenido

- [1. Principios de redacción de errores](#1-principios-de-redacción-de-errores)
  - [1.1 Qué pasó, por qué pasó, qué hacer](#11-qué-pasó-por-qué-pasó-qué-hacer)
  - [1.2 Una condición de error no es una observación](#12-una-condición-de-error-no-es-una-observación)
  - [1.3 Qué emite el dominio y qué compone el consumidor](#13-qué-emite-el-dominio-y-qué-compone-el-consumidor)
  - [1.4 Un mismo código con dos causas opuestas: los dos caminos de alta](#14-un-mismo-código-con-dos-causas-opuestas-los-dos-caminos-de-alta)
  - [1.5 Resetear no es dar de baja](#15-resetear-no-es-dar-de-baja)
- [2. Taxonomía](#2-taxonomía)
  - [2.1 Las categorías en uso](#21-las-categorías-en-uso)
  - [2.2 Las dos categorías vacías, con su motivo](#22-las-dos-categorías-vacías-con-su-motivo)
  - [2.3 Forma de terminación](#23-forma-de-terminación)
- [3. Catálogo](#3-catálogo)
  - [3.1 CU-02001 Registrar el alta de un alumno](#31-cu-01-registrar-el-alta-de-un-alumno)
  - [3.2 CU-02002 Gobernar el ciclo de vida de la cuenta](#32-cu-02-gobernar-el-ciclo-de-vida-de-la-cuenta)
  - [3.3 CU-02003 Fijar y reemplazar la credencial derivada](#33-cu-03-fijar-y-reemplazar-la-credencial-derivada)
  - [3.4 CU-02004 Evaluar la admisibilidad de la cuenta](#34-cu-04-evaluar-la-admisibilidad-de-la-cuenta)
  - [3.5 CU-02005 Crear y reeditar un trabajo](#35-cu-05-crear-y-reeditar-un-trabajo)
  - [3.6 CU-02006 Reconstruir el conjunto de piezas del trabajo](#36-cu-06-reconstruir-el-conjunto-de-piezas-del-trabajo)
  - [3.7 CU-02007 Registrar las observaciones del trabajo](#37-cu-07-registrar-las-observaciones-del-trabajo)
  - [3.8 CU-02008 Gobernar el estado del trabajo en el envío](#38-cu-08-gobernar-el-estado-del-trabajo-en-el-envío)
  - [3.9 CU-02009 Resolver el acceso del alumno a un trabajo](#39-cu-09-resolver-el-acceso-del-alumno-a-un-trabajo)
  - [3.10 CU-02010 Resolver el desenlace del trabajo](#310-cu-10-resolver-el-desenlace-del-trabajo)
  - [3.11 CU-02011 Resolver el alcance del administrador sobre un trabajo](#311-cu-11-resolver-el-alcance-del-administrador-sobre-un-trabajo)
  - [3.12 CU-02012 Configurar la cuenta de administrador](#312-cu-12-configurar-la-cuenta-de-administrador)
  - [3.13 CU-02013 Resetear la contraseña de una cuenta de alumno](#313-cu-13-resetear-la-contraseña-de-una-cuenta-de-alumno)
- [4. Tono y voz](#4-tono-y-voz)
- [5. Localización](#5-localización)
- [6. Cobertura y trazabilidad](#6-cobertura-y-trazabilidad)
  - [6.1 Recuento](#61-recuento)
  - [6.2 Tabla de cobertura](#62-tabla-de-cobertura)
  - [6.3 Trazabilidad del artefacto](#63-trazabilidad-del-artefacto)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Principios de redacción de errores

### 1.1 Qué pasó, por qué pasó, qué hacer

Las tres partes son obligatorias en cada entrada y se corresponden con las tres columnas del catálogo: **mensaje** dice qué pasó, **causa probable** dice por qué pasó, **acción sugerida** dice qué hacer al respecto.

La tercera parte tiene acá una forma particular, y es la que le da sentido al catálogo entero:

> El diagnóstico accionable dice siempre **qué hacer del lado del consumidor**, porque el dominio no resuelve nada por su cuenta: no consulta, no reintenta, no completa el dato que falta y no corrige el dato del alumno.

Cuatro reglas de redacción que ninguna entrada incumple:

1. **Lenguaje plano y sin culpar a nadie.** El enunciado describe la guarda que se negó, no la torpeza de quien invocó.
2. **Nada genérico.** No hay «operación inválida» ni «error interno». Un rechazo dice qué guarda se negó. Es la misma exigencia que RN-02009 le impone al producto frente al alumno, aplicada frente al consumidor.
3. **Nada que la regla oculte se filtra.** `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE` es deliberadamente indistinguible de la inexistencia (RN-02003, INV-02).
4. **Ningún código es un código de protocolo.** La traducción a respuesta pertenece a `GeometriaFactory-Api` (CU-02001 §6, CU-02004 §6).

### 1.2 Una condición de error no es una observación

Es la distinción que sostiene todo lo demás, y confundirla lleva a modelar mal dos cosas a la vez. Las tres nociones son distintas y ninguna es especie de otra:

| Noción | Qué es | Cuántas hay | Quién la produce | Se guarda |
| --- | --- | --- | --- | --- |
| **Condición de error del dominio** | Una guarda que impide una operación ilegítima del consumidor. Es lo que este catálogo enumera | Una por invocación rechazada, y no sobrevive a la invocación | El dominio, al negarse | No |
| **Observación** | Entidad del dominio con dos especies, advertencia y error de validación, que el producto emite **al interpretar el texto del alumno** y al verificar sus valores | Varias por trabajo, tantas como defectos | El validador de figuras, fuera del dominio; el dominio la adopta por CU-02007 | Sí, como entidad |
| **Comentario** | Texto libre y opcional que el administrador deja al aprobar o al rechazar | A lo sumo uno por trabajo | Una persona | Sí, como atributo del trabajo |

Consecuencia práctica: un trabajo que vuelve en `Borrador` porque su texto trajo un error de validación **no produjo ninguna condición de error de este catálogo**. Es el resultado declarado del envío (CU-02008 FA-01), y traducirlo hacia afuera como fallo sería un defecto del consumidor.

En el sentido inverso: `ERROR_SIN_UBICACION` y `ADVERTENCIA_SIN_LOS_DOS_VALORES` **sí** son condiciones de este catálogo, aunque hablen de observaciones. Lo que rechazan no es la observación en sí: es un conjunto de observaciones mal formado que el consumidor intenta adoptar.

### 1.3 Qué emite el dominio y qué compone el consumidor

El dominio emite un **código**, no un texto. No produce mensajes para personas, no los formatea y no los traduce: no conoce ningún formato de serialización (`PRODUCT-INTAKE` §17.1.P.1) y no cruza ninguna frontera de proceso (§17.1.P.3).

La columna «mensaje» de este catálogo es el **enunciado canónico en lenguaje plano** de cada condición: la base sobre la que la capa que expone compone lo que una persona lee. No es una cadena que la biblioteca produzca ni un recurso que exista en el código.

### 1.4 Un mismo código con dos causas opuestas: los dos caminos de alta

Hay **dos caminos de alta** de una cuenta, cada uno con su caso de uso, su estado inicial y su tratamiento de la credencial, y **cada uno rechaza el del otro**:

| Camino | Caso de uso | Estado inicial | Credencial |
| --- | --- | --- | --- |
| Auto-registro del alumno | CU-02001 | `Pendiente` | No se aporta: se fija en el primer ingreso efectivo, por CU-02003 |
| Configuración del administrador en el primer arranque | CU-02012 | **`Habilitado`** | Se aporta en el mismo acto, ya derivada |

**La cuenta del administrador nace habilitada porque es la que habilita a las demás.** Ninguna cuenta anterior podría habilitarla a ella: si naciera `Pendiente`, por INV-06 no obtendría acceso y no habría nadie capaz de sacarla de ahí, de modo que la instancia quedaría inutilizable en el primer arranque. Esa generalización —un estado inicial uniforme para toda cuenta— es exactamente el defecto que la corrección del P0 resolvió, y es el error que un lector de este catálogo tiene que salir sin poder cometer.

**Consecuencia sobre el catálogo, y es la primera vez que ocurre en este proyecto de código.** El identificador `ESTADO_INICIAL_NO_NEGOCIABLE` aparece en los dos caminos con **causas opuestas**:

| Caso de uso | Qué rechaza | Estado que impone |
| --- | --- | --- |
| CU-02001, auto-registro | Constituir la cuenta en un estado **distinto de `Pendiente`** | `Pendiente` |
| CU-02012, configuración del administrador | Constituir la cuenta en un estado **distinto de `Habilitado`** | `Habilitado` |

No es una inconsistencia y no hay que unificarlo: el enunciado del código es «el estado inicial de este camino no se elige», y cuál es ese estado lo fija el camino. Por eso es el único código del catálogo que lleva **fila completa en dos subsecciones de §3** en lugar de una entrada única con nota, y las dos filas se leen juntas. Los otros cuatro códigos declarados en más de un caso de uso conservan la misma causa en todos y siguen con entrada única.

Dos códigos más existen sólo para que ninguno de los dos caminos se cuele por el otro: `PAPEL_DE_ADMINISTRADOR_FUERA_DE_ESTE_CAMINO`, que impide constituir un administrador por el auto-registro, y `CREDENCIAL_NO_ADMITIDA_EN_EL_ALTA`, que quedó **acotado al auto-registro** porque en la configuración del administrador la credencial sí se aporta.

**El alta es sólo una de las dos puertas, y la otra es el ciclo de vida posterior.** La misma condición sin salida se alcanza sin tocar el alta: basta con **bloquear** la cuenta del administrador ya configurada. Una cuenta bloqueada no obtiene acceso por INV-06, y el único que puede desbloquearla es él mismo. Por eso las **cuatro** operaciones de CU-02002 —habilitar, bloquear, rehabilitar y dar de baja— alcanzan **sólo a las cuentas con papel `Alumno`**, que es el enunciado literal de la capacidad F-03, y sobre la cuenta de administrador ninguna procede: es lo que rechaza `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` (§3.2).

Y el efecto no se agota en el acceso, que es lo que dimensiona el problema: **sin administrador nadie aprueba ni rechaza**, así que todo trabajo enviado queda en estado `Pendiente` para siempre y **el circuito de revisión entero se detiene** (RN-02010, CU-02010). La instancia sigue aceptando entregas que ya nadie puede resolver.

### 1.5 Resetear no es dar de baja

Es la distinción más cara de este catálogo, porque las dos operaciones las ejerce la misma persona desde el mismo panel y **una de las dos es irreversible**.

| | Baja de la cuenta (CU-02002) | Reseteo de contraseña (CU-02013) |
| --- | --- | --- |
| Qué pasa con la cuenta | Deja de existir | Se conserva, con su estado, su papel y su identidad |
| Qué pasa con los trabajos | **Se eliminan todos**, en los cuatro estados y con sus comentarios (RN-02007) | **Se conservan todos**, con sus estados y sus comentarios (RN-02012) |
| Reversible | No | Sí: la cuenta cambia la provisoria y sigue operando |
| Exige confirmación escrita | Sí, el correo de la cuenta | No, y no es un olvido: la guarda protege de un accidente destructivo |
| Efecto sobre la marca | No aplica | La **pone**, y sólo el reemplazo de la propia cuenta la levanta (RN-02013, INV-09) |

Hasta `PRODUCT-INTAKE` 1.6 la baja era el único camino declarado ante una contraseña olvidada, y por eso el primer olvido costaba todos los trabajos del alumno. La capacidad **F-26** cierra ese agujero, retira la exclusión **X-2** y reescribe el caso límite **CL-7**. Un consumidor que resuelva un olvido de contraseña invocando CU-02002 en lugar de CU-02013 **no recibe ninguna condición de error de este catálogo**: la baja procede, y es correcta como operación. Es el mismo tipo de defecto silencioso que §1.2 describe para el trabajo que vuelve en `Borrador`, y por eso está declarado acá y no en una fila de tabla.

## 2. Taxonomía

### 2.1 Las categorías en uso

| Categoría | Qué agrupa | Cuántas condiciones |
| --- | --- | --- |
| **Entrada inválida** | El dato que llega está ausente, vacío, no admitido en esta operación, o contradice a lo que el propio dato declara | 20 |
| **Recurso ausente** | Lo que la operación referencia no existe, o todavía no tiene valor | 2 |
| **Conflicto de estado** | La operación es legítima, pero el estado actual de la cuenta, del trabajo o de la instancia no la admite | 15 |
| **Conflicto de facultad** | La operación es legítima y el estado la admitiría, pero el papel declarado no la ejerce, o el camino por el que se pide no es el suyo | 5 |

Sobre **conflicto de facultad**, que es una categoría agregada a la enumeración de referencia: se declara aparte porque las cinco condiciones que agrupa no se resuelven mirando el dato ni el estado, sino el papel, y confundirlas con un conflicto de estado llevaría a buscar el remedio en una transición que no existe. La distinción es la misma que separa a CU-02009, que responde por el alumno, de CU-02011, que responde por el administrador.

### 2.2 Las dos categorías vacías, con su motivo

Se declaran vacías en lugar de omitirse, para que nadie las complete más adelante con condiciones inventadas.

| Categoría | Condiciones | Motivo |
| --- | --- | --- |
| **Error transitorio** | Ninguna | Un error transitorio supone una operación que puede volver a intentarse y a veces sale bien. Este proyecto de código no atiende peticiones, no abre conexiones y no ejecuta entrada ni salida (`PRODUCT-INTAKE` §17.1.P.10): ninguna de sus guardas depende del momento en que se la invoque. **El dominio nunca pide reintentar** |
| **Error interno** | Ninguna | Todo rechazo del dominio es una guarda declarada, con su caso de uso y su regla. Una falla no declarada no sería una condición de este catálogo: sería un defecto del proyecto de código, y su lugar es una prueba que falla, no una entrada acá |

### 2.3 Forma de terminación

Dimensión ortogonal a la categoría, y hay que leerla junto con ella porque cambia lo que el consumidor tiene que hacer:

| Forma | Qué significa | Dónde aparece |
| --- | --- | --- |
| **Rechazo** | El dominio se niega a la operación. No construye la entidad, o la deja exactamente como estaba. No hay efecto parcial ni estado intermedio, porque el dominio no guarda nada | CU-02001, CU-02002, CU-02003, CU-02005, CU-02006, CU-02007, CU-02008, CU-02010, CU-02013 |
| **Motivo de resultado** | La operación es una consulta y **siempre devuelve un resultado**; el código es el motivo por el que ese resultado es «no admisible» o «no procede». No es una excepción de programa y no modifica nada | CU-02004, CU-02009, CU-02011 |

La diferencia importa: ante un rechazo, el consumidor corrige la invocación; ante un motivo de resultado, el consumidor **informa** o **encamina** a la operación que corresponde. `CAMBIO_DE_CONTRASENA_PENDIENTE` es el ejemplo canónico: no es un fallo, es la situación esperada de toda cuenta de alumno recién habilitada o recién reseteada. Hasta la versión 1.4 el ejemplo canónico era `CREDENCIAL_NO_ESTABLECIDA`, que **RN-02016 retiró** del catálogo junto con el primer ingreso anónimo que lo producía.

## 3. Catálogo

Cuarenta y tres condiciones, derivadas una por una de la §6 de los trece casos de uso. Ninguna se inventó y ninguna quedó afuera; el recuento y la verificación están en §6.

Siete condiciones aparecen declaradas en más de un caso de uso. Seis de ellas conservan la misma causa en todos y llevan **una sola entrada**, en el caso de uso donde se declaran primero, con la nota de sus otras apariciones. La séptima, `ESTADO_INICIAL_NO_NEGOCIABLE`, lleva **fila completa en §3.1 y en §3.12** porque sus dos causas son opuestas según el camino de alta: el motivo está en §1.4.

### 3.1 CU-02001 Registrar el alta de un alumno

Es el **auto-registro del alumno**, uno de los dos caminos de alta (§1.4). Forma de terminación: rechazo. En los cinco casos no se produce ninguna instancia y no hay efecto parcial.

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `DATO_OBLIGATORIO_AUSENTE` | Entrada inválida | Falta un dato obligatorio del alta: correo, nombre o apellido | Uno de los tres llegó vacío o no se proveyó | Completar el dato faltante antes de invocar. El dominio no lo infiere ni lo deja en blanco. Esta condición vuelve a declararse en CU-02005, sobre el nombre y la fecha del trabajo |
| `UNICIDAD_DE_CORREO_NO_VERIFICADA` | Entrada inválida | La unicidad del correo no viene declarada como comprobada | El consumidor invocó sin afirmar que comprobó que el correo esté libre | Resolver la unicidad en `GeometriaFactory-Application` con el puerto de repositorio y declararla al invocar. El correo es único en todo el sistema (INV-01, RN-02002) y esa comprobación se afirma sobre el conjunto de alumnos, que el dominio no conoce. Esta condición vuelve a declararse en CU-02012, con la misma causa |
| `CREDENCIAL_NO_ADMITIDA_EN_EL_ALTA` | Entrada inválida | El **auto-registro** no admite credencial derivada | Se aportó una credencial derivada junto con los datos del auto-registro | Registrar sin credencial: en este camino se fija recién en el primer ingreso efectivo, por CU-02003. **En la configuración del administrador la credencial sí se aporta**, y eso es CU-02012: el código está acotado a este camino |
| `ESTADO_INICIAL_NO_NEGOCIABLE` | Entrada inválida | El estado inicial de **este camino** no se elige | Se pidió constituir la cuenta del auto-registro en un estado distinto de `Pendiente` | Constituir sin pedir estado. Toda cuenta de alumno nace `Pendiente` y sólo el administrador la habilita, con acto explícito (CU-02002). **Mismo identificador, causa opuesta en CU-02012**, donde el estado impuesto es `Habilitado`: ver §3.12 y §1.4 |
| `PAPEL_DE_ADMINISTRADOR_FUERA_DE_ESTE_CAMINO` | Conflicto de facultad | El auto-registro no constituye cuentas con papel `Administrador` | Se pidió constituir un administrador por la vía del alumno | Usar CU-02012, que es el camino que la fuente declara para la configuración del administrador. Constituirlo acá lo dejaría con la cuenta `Pendiente` y sin salida, porque ninguna otra cuenta podría habilitarlo (§1.4) |

### 3.2 CU-02002 Gobernar el ciclo de vida de la cuenta

Las cuatro operaciones de este contrato —habilitar, bloquear, rehabilitar y dar de baja— alcanzan **sólo a las cuentas con papel `Alumno`** (F-03). Forma de terminación: rechazo. En los **cuatro** casos la cuenta queda exactamente como estaba.

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `TRANSICION_DE_CUENTA_NO_ADMITIDA` | Conflicto de estado | La transición pedida no figura en la tabla de transiciones de la cuenta | El par estado actual y operación no está declarado; el caso típico es bloquear una cuenta `Pendiente` sin haber pasado por `Habilitado` | Consultar la tabla de `Definicion-Modelo-De-Dominio.md` §5.1 y encadenar las transiciones declaradas. El dominio no infiere transiciones que ninguna fuente declara |
| `BAJA_SIN_ARRASTRE_DE_TRABAJOS` | Entrada inválida | La baja no admite conservar los trabajos del alumno | Se solicitó la baja declarando que los trabajos se conservan | Solicitar la baja con arrastre y materializar cuenta y trabajos como una sola unidad. El arrastre alcanza a los trabajos en cualquier estado, incluidos `Finalizado` y `Rechazado`, y es una consecuencia aceptada por escrito aguas arriba (RN-02007) |
| `HABILITACION_SIN_CREDENCIAL_PROVISORIA` | Entrada inválida | Habilitar o rehabilitar exige la credencial derivada provisoria | Se invocó la transición sin aportar el valor derivado de la contraseña provisoria que el sistema produce | Producir la provisoria en la capa que corresponde, derivarla y aportarla en la misma invocación. Desde **RN-02016**, fijar la credencial y poner la marca son efectos del mismo acto que la habilitación, y admitirla sin credencial dejaría la cuenta `Habilitado` sin nada con que autenticarse: es la ventana que RN-02016 cierra. Bloquear y dar de baja **no** exigen credencial |
| `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` | Conflicto de facultad | Ninguna de las cuatro operaciones procede sobre la cuenta con papel `Administrador` | Se pidió **habilitar, bloquear, rehabilitar o dar de baja** al administrador de la instancia | No hay camino, y no lo hay para ninguna de las cuatro: las cuatro están declaradas sobre cuentas de alumno (F-03) y sobre la única cuenta de administrador ninguna tiene inversa posible (RN-02001, INV-05). Bloquearla o darla de baja deja a la instancia **sin nadie capaz de habilitar, desbloquear y revisar**, y con el circuito de revisión detenido: todo trabajo enviado queda en estado `Pendiente` para siempre. Ver §1.4 |

### 3.3 CU-02003 Fijar y reemplazar la credencial derivada

Forma de terminación: rechazo. En los cuatro casos el alumno queda exactamente como estaba.

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `CUENTA_NO_HABILITADA_PARA_CREDENCIAL` | Conflicto de estado | La credencial derivada sólo se fija con la cuenta `Habilitado` | El estado de cuenta es `Pendiente` o `Bloqueado` | Habilitar o rehabilitar la cuenta primero, por CU-02002. Es la misma condición que INV-06 expresa desde el lado del acceso |
| `CREDENCIAL_YA_FIJADA` | Conflicto de estado | La credencial derivada ya tiene valor | Se pidió fijar por primera vez algo que ya está fijado | Usar el camino de reemplazo, declarando verificada la credencial vigente. El valor anterior se reemplaza y no se conserva historial |
| `CREDENCIAL_VIGENTE_NO_VERIFICADA` | Entrada inválida | El reemplazo exige declarar verificada la credencial vigente | Se pidió el reemplazo sin esa declaración | Verificar la credencial vigente en la capa que sí puede compararla, `GeometriaFactory-Infrastructure`, y declararlo al invocar. El dominio no compara credenciales |
| `VALOR_DERIVADO_VACIO` | Entrada inválida | El valor de credencial derivada llegó vacío | Se invocó con un valor sin contenido | Aportar el valor ya derivado. El dominio no deriva la contraseña y nunca la conoce en claro (`PRODUCT-INTAKE` §17.1.P.5) |

### 3.4 CU-02004 Evaluar la admisibilidad de la cuenta

Forma de terminación: **motivo de resultado**. Los **tres** son terminaciones controladas y no excepciones de programa: la evaluación siempre devuelve un resultado, y ese resultado incluye el motivo.

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `CUENTA_PENDIENTE` | Conflicto de estado | La cuenta está registrada y todavía no fue habilitada | El estado de cuenta es `Pendiente` (INV-06, RN-02006) | Informar la situación con todas las letras y no con un rechazo genérico: la persona tiene que saber que espera la habilitación del administrador. No emitir acceso |
| `CUENTA_BLOQUEADA` | Conflicto de estado | La cuenta está bloqueada | El estado de cuenta es `Bloqueado` (INV-06, RN-02006) | Informar el motivo y no emitir acceso. La rehabilitación es un acto explícito del administrador, por CU-02002 |
| `CAMBIO_DE_CONTRASENA_PENDIENTE` | Conflicto de estado | La cuenta tiene una contraseña provisoria sin cambiar | El administrador la **habilitó** por CU-02002 o la **reseteó** por CU-02013, y la marca sigue puesta. Desde **RN-02016** los dos actos la producen, y es también el motivo con el que llega todo alumno a su primer ingreso | **No es un fallo.** Encaminar al cambio de contraseña, que es el reemplazo de CU-02003 FA-04 y **lo único** que la cuenta puede hacer hasta que la marca se levante (INV-09, RN-02013). No emitir acceso, y no ofrecer ninguna otra ruta: la contraseña nueva la elige el alumno y el administrador no la conoce |

### 3.5 CU-02005 Crear y reeditar un trabajo

Forma de terminación: rechazo.

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `TRABAJO_SIN_DUENO` | Entrada inválida | El trabajo no trae dueño | Se invocó la constitución sin el alumno al que pertenece | Aportar el dueño. Un trabajo sin dueño no es un trabajo, y la pertenencia no es transferible (INV-02) |
| `DATO_OBLIGATORIO_AUSENTE` | Entrada inválida | Falta el nombre o la fecha del trabajo | Uno de los dos no se proveyó | Completar el dato. La fecha es el dato que el alumno declara, no el del reloj del sistema: el dominio no lee el reloj. Entrada única en §3.1; ésta es su segunda declaración |
| `REEDICION_FUERA_DE_BORRADOR` | Conflicto de estado | Sólo se reedita un trabajo en `Borrador` | Se pidió reeditar un trabajo en estado `Pendiente`, `Finalizado` o `Rechazado` | No hay reedición fuera del borrador, y en los dos estados terminales el contenido tampoco cambia (INV-07). Si el trabajo fue rechazado, el camino es cargar uno nuevo |
| `TEXTO_ORIGINAL_ALTERADO` | Entrada inválida | El texto original no admite versiones corregidas | El consumidor aportó un texto que declara ser una corrección del que pegó el alumno | Conservar el texto tal como el alumno lo pegó. El producto no edita el dato del alumno (RN-02008), y es justamente lo que hace posible reprocesar el mismo trabajo cuando el validador mejora |

### 3.6 CU-02006 Reconstruir el conjunto de piezas del trabajo

Forma de terminación: rechazo. Salvo donde se indica, el rechazo alcanza a la reconstrucción entera.

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `POSICION_DE_PIEZA_INVALIDA` | Entrada inválida | Una posición del conjunto de piezas está repetida, es negativa o cae fuera del rango del conjunto raíz declarado | El conjunto llegó sin identidad posicional estable | Entregar cada pieza con **la posición que su figura ocupa en el conjunto raíz del texto del alumno**, sin recalcularla. La identidad de la pieza es esa posición, porque el dato del alumno no trae identificador propio. **Un hueco no es un defecto**: es la posición reservada de una figura que no se pudo reconstruir (CU-02006 FA-03), y renumerar las adoptadas para dejarlas contiguas desplazaría el índice que el alumno ve y que la observación tiene que informar |
| `TIPO_DE_PIEZA_DESCONOCIDO` | Entrada inválida | El tipo de la pieza no pertenece al conjunto conocido | El texto del alumno declaró un tipo fuera de `Cilindro`, `Cubo`, `Ortoedro`, `Rectangulo`, `Cuadrado`, `Circulo` | No es un rechazo del conjunto: esa pieza no se adopta y las demás sí, porque un defecto en un elemento no descarta el resto. **La posición de la figura no adoptada queda reservada** y las demás conservan la suya. Registrar la observación de especie error de validación por CU-02007, sobre esa misma posición y con su campo |
| `FAMILIA_DECLARADA_CONTRADICE_AL_TIPO` | Entrada inválida | La familia plana o volumétrica aportada contradice a la que el tipo deriva | Se aportó la familia como dato | No aportar la familia: **se deriva del tipo y no se guarda**. `Cilindro`, `Cubo` y `Ortoedro` son volumétricos; `Rectangulo`, `Cuadrado` y `Circulo` son planos |
| `RECONSTRUCCION_SOBRE_TRABAJO_TERMINAL` | Conflicto de estado | Un trabajo en estado terminal no admite reconstrucción | El trabajo está `Finalizado` o `Rechazado` | No reconstruir: los dos estados terminales no cambian de estado ni de contenido (INV-07). Si hay que reprocesar, el camino es un trabajo nuevo |

### 3.7 CU-02007 Registrar las observaciones del trabajo

Forma de terminación: rechazo. En los cuatro casos se rechaza el conjunto entero de observaciones.

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `ESPECIE_DE_OBSERVACION_DESCONOCIDA` | Entrada inválida | La especie de la observación no es `Advertencia` ni `Error de validación` | Llegó un tercer valor | Usar una de las dos especies. La especie es lo que decide el efecto sobre el envío y el conjunto es cerrado (RN-02005) |
| `ERROR_SIN_UBICACION` | Entrada inválida | Una observación de especie error de validación no indica posición de pieza ni campo, siendo atribuible a una figura | El validador emitió un defecto sin ubicarlo | Emitirlo con su posición de pieza y su campo. Un mensaje genérico es exactamente lo que el producto viene a eliminar (RN-02009). Cuando el defecto **no** es atribuible a ninguna figura, la observación se adopta sin posición y con el campo que el consumidor indique |
| `ADVERTENCIA_SIN_LOS_DOS_VALORES` | Entrada inválida | Una advertencia de discrepancia no trae el valor declarado o no trae el derivado | Se emitió con un solo número | Emitir los dos. Sin el par la advertencia no explica nada, y mostrar el par es el mayor valor didáctico del producto |
| `OBSERVACION_SOBRE_PIEZA_INEXISTENTE` | Recurso ausente | La posición indicada no pertenece al rango de posiciones del conjunto raíz interpretado | La observación designa una figura que el texto del alumno no trae | Ubicar la observación dentro del rango del conjunto raíz, o emitirla sin posición si el defecto no es atribuible a ninguna figura. **Una posición reservada no es una posición inexistente**: la de una figura que no se pudo reconstruir sí pertenece al rango y sí admite observación, que es precisamente el caso insignia de RN-02009 (CU-02006 FA-03, CU-02007 FA-04) |

### 3.8 CU-02008 Gobernar el estado del trabajo en el envío

Forma de terminación: rechazo. En los cuatro casos se conserva el estado actual.

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `ENVIO_FUERA_DE_BORRADOR` | Conflicto de estado | Sólo se envía un trabajo en `Borrador` | Se pidió enviar un trabajo que ya salió de las manos del alumno | No reenviar. Ninguna fuente declara una reentrada al envío desde estado `Pendiente`, y el dominio no la infiere |
| `TRANSICION_DESDE_ESTADO_TERMINAL` | Conflicto de estado | De un trabajo `Finalizado` o `Rechazado` no sale ninguna transición | Se pidió cualquier cambio de estado sobre un trabajo terminal | No hay camino de vuelta (INV-07, RN-02010). Corregir un rechazo significa cargar un trabajo nuevo; lo único que un trabajo terminal admite es que el administrador lo elimine. Esta condición vuelve a declararse en CU-02010, sobre un desenlace nuevo |
| `ENVIO_SIN_INTERPRETACION` | Conflicto de estado | El trabajo se envía sin que su texto original haya sido interpretado | Se invocó el envío antes de incorporar el resultado de la interpretación | Invocar CU-02006 y CU-02007 con lo que produjo el validador de figuras, y recién después enviar. El envío decide **sobre** ese resultado: sin él no hay nada que decidir |
| `DESENLACE_NO_ADMITIDO_EN_ESTE_CONTRATO` | Conflicto de facultad | Aprobar y rechazar no se ejercen por la vía del envío | Se pidió un desenlace en el contrato del alumno | Usar CU-02010. El desenlace es facultad exclusiva del administrador y el alumno no lo ejerce ni sobre su propio trabajo (RN-02010) |

### 3.9 CU-02009 Resolver el acceso del alumno a un trabajo

Forma de terminación: **motivo de resultado**. Ninguno de los tres tiene efecto sobre el trabajo: la consulta no modifica nada.

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE` | Recurso ausente | El trabajo no existe para quien lo pide | El solicitante no es el dueño del trabajo | Traducirlo a «no encontrado» y **nunca** a «no autorizado»: confirmar la existencia de un trabajo ajeno es exactamente lo que RN-02003 e INV-02 impiden. La indistinguibilidad es deliberada |
| `OPERACION_FUERA_DE_BORRADOR` | Conflicto de estado | El dueño no reedita ni elimina fuera de `Borrador` | Se consultó reeditar o eliminar un trabajo propio en estado `Pendiente`, `Finalizado` o `Rechazado` | Informar la acotación al borrador (RN-02004, INV-03). Es un motivo distinto del anterior porque acá la existencia del trabajo ya está admitida para su dueño. **Ver** un trabajo propio sí procede en los cuatro estados, incluidos el desenlace y el comentario |
| `OPERACION_DESCONOCIDA` | Entrada inválida | La operación consultada no pertenece al conjunto declarado | Se consultó algo distinto de ver, reeditar o eliminar | Consultar una de las operaciones declaradas. El dominio devuelve no procede sin evaluar siquiera la pertenencia. Esta condición vuelve a declararse en CU-02011 |

### 3.10 CU-02010 Resolver el desenlace del trabajo

Forma de terminación: rechazo. En los cuatro casos el trabajo queda intacto.

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `DESENLACE_FUERA_DE_PENDIENTE` | Conflicto de estado | Sólo se aprueba o se rechaza un trabajo en estado `Pendiente` | Se pidió el desenlace de un trabajo en otro estado | Un trabajo en `Borrador` no se aprueba ni se rechaza: el administrador ni siquiera lo ve (RN-02011). Un trabajo terminal ya tuvo su desenlace |
| `DESENLACE_SIN_PAPEL_DE_ADMINISTRADOR` | Conflicto de facultad | El desenlace exige papel `Administrador` | El papel declarado al invocar no es `Administrador` | Comprobar el papel antes de invocar. La facultad es exclusiva y no se delega, ni siquiera sobre el trabajo propio (RN-02010) |
| `TRANSICION_DESDE_ESTADO_TERMINAL` | Conflicto de estado | De un trabajo `Finalizado` o `Rechazado` no sale ninguna transición | Se pidió un desenlace nuevo sobre un trabajo que ya lo tuvo | No hay camino para corregir un desenlace aplicado (INV-07). Lo que el administrador sí puede hacer es eliminar el trabajo, por CU-02011. Entrada única en §3.8 |
| `DESENLACE_DESCONOCIDO` | Entrada inválida | El desenlace pedido no es aprobar ni rechazar | Llegó un tercer valor | Usar uno de los dos. Aprobar lleva a `Finalizado` y rechazar a `Rechazado`; los dos admiten comentario opcional y los dos son terminales |

### 3.11 CU-02011 Resolver el alcance del administrador sobre un trabajo

Forma de terminación: **motivo de resultado**. La consulta no modifica nada en ningún caso.

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR` | Conflicto de estado | El trabajo está en `Borrador` y no entra en el alcance del administrador | Se consultó un borrador | Excluirlo de la vista de revisión y de la eliminación. A diferencia del motivo equivalente de CU-02009, **éste no oculta la existencia del trabajo**: expresa que está fuera del flujo de trabajo del administrador (RN-02011). Los tres estados que sí ve admiten eliminación, incluidos los dos terminales |
| `ALCANCE_SIN_PAPEL_DE_ADMINISTRADOR` | Conflicto de facultad | La consulta de alcance exige papel `Administrador` | El papel declarado no es `Administrador` | La pregunta por lo que puede un alumno es CU-02009. El dominio devuelve no procede sin evaluar siquiera el estado |
| `OPERACION_DESCONOCIDA` | Entrada inválida | La operación consultada no pertenece al conjunto declarado | Se consultó algo distinto de las operaciones declaradas | Consultar una de las declaradas. Entrada única en §3.9 |

### 3.12 CU-02012 Configurar la cuenta de administrador

Es la **configuración del administrador en el primer arranque**, el otro camino de alta (§1.4). Forma de terminación: rechazo. En los cinco casos no se constituye ninguna entidad y la instancia sigue sin administrador, de modo que este mismo contrato vuelve a estar disponible.

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `DATO_OBLIGATORIO_AUSENTE` | Entrada inválida | Falta un dato obligatorio: correo, nombre o apellido | Uno de los tres llegó vacío o no se proveyó | Completar el dato faltante antes de invocar. Entrada única en §3.1; ésta es su tercera declaración, con la misma causa |
| `ADMINISTRADOR_YA_CONFIGURADO` | Conflicto de estado | La instancia ya tiene su cuenta de administrador, o el consumidor no declara que no la tiene | Se pidió configurar una segunda cuenta con papel `Administrador`, o se invocó sin declarar la ausencia de administrador previo | Comprobar sobre el conjunto de cuentas que no existe ninguna con ese papel y declararlo al invocar; el dominio no conoce ese conjunto. Si ya existe una, **no hay camino**: la ventana de alta se cerró y la instancia tiene exactamente un administrador (RN-02001, INV-05) |
| `UNICIDAD_DE_CORREO_NO_VERIFICADA` | Entrada inválida | La unicidad del correo no viene declarada como comprobada | El consumidor invocó sin afirmar que comprobó que el correo esté libre | Igual que en el auto-registro: resolverla en la capa de aplicación con el puerto de repositorio y declararla al invocar (INV-01, RN-02002). Entrada única en §3.1 |
| `CONFIGURACION_SIN_CREDENCIAL` | Entrada inválida | La configuración del administrador exige la credencial derivada | No se aportó credencial derivada, o el valor aportado está vacío | Aportar el valor **ya derivado**, que el dominio nunca conoce en claro. Una cuenta de administrador sin credencial no podría entrar y **no hay ninguna otra cuenta que pudiera resolverlo**: por eso acá la credencial es obligatoria y en el auto-registro está prohibida |
| `ESTADO_INICIAL_NO_NEGOCIABLE` | Entrada inválida | El estado inicial de **este camino** no se elige | Se pidió constituir la cuenta de administrador en un estado distinto de `Habilitado` | Constituir sin pedir estado. **Mismo identificador, causa opuesta en CU-02001**, donde el estado impuesto es `Pendiente`: ver §3.1 y §1.4. Una cuenta de administrador `Pendiente` o `Bloqueado` dejaría a la instancia sin salida, porque por INV-06 no obtendría acceso y nadie podría habilitarla |

### 3.13 CU-02013 Resetear la contraseña de una cuenta de alumno

Es la **operación conservadora** del administrador sobre una cuenta ajena: fija una contraseña provisoria y pone la marca, sin tocar el estado de cuenta ni ninguno de los trabajos (§1.5). Forma de terminación: rechazo. En los **tres** casos la cuenta queda exactamente como estaba, con su credencial anterior y su marca anterior.

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` | Conflicto de facultad | El reseteo no procede sobre la cuenta con papel `Administrador` | Se pidió resetear la contraseña del administrador de la instancia | No hay camino, y es el mismo que ya cerraba las cuatro operaciones de CU-02002: **el código se reutiliza porque la causa es la misma**, una operación del administrador declarada sobre cuentas de alumno y sin nadie que la ejerza sobre la suya (RN-02001, INV-05, INV-08). Su cambio de contraseña entra por el reemplazo de CU-02003 FA-01. Entrada única en §3.2; ésta es su segunda declaración |
| `RESETEO_CON_ARRASTRE_DE_TRABAJOS` | Entrada inválida | El reseteo no admite eliminar los trabajos del alumno ni cambiar su estado de cuenta | Se armó la solicitud tratando el reseteo como si fuera una baja | **Resetear no es dar de baja** (RN-02012): la cuenta conserva su habilitación, su papel, su identidad y **todos** sus trabajos con sus estados y comentarios. La operación que sí los elimina es la baja de CU-02002, y es irreversible. Ver §1.5 |
| `VALOR_DERIVADO_VACIO` | Entrada inválida | El valor de credencial derivada llegó vacío | Se invocó con un valor sin contenido | Aportar el valor de la contraseña provisoria **ya derivado**. El dominio no deriva la contraseña y **nunca conoce la provisoria en claro**, que es el valor que el administrador le comunica al alumno por fuera del producto. Entrada única en §3.3; ésta es su segunda declaración |

## 4. Tono y voz

Coherente con la guía de estilo del producto: español rioplatense neutro técnico, sin marketing y sin emojis.

| Regla | Sí | No |
| --- | --- | --- |
| Describir la guarda, no juzgar a quien invocó | «La unicidad del correo no viene declarada como comprobada» | «Olvidaste verificar la unicidad» |
| Nombrar la entidad y el estado con el vocabulario del dominio | «Sólo se envía un trabajo en `Borrador`» | «El registro está en un estado no editable» |
| Decir la acción en imperativo, del lado del consumidor | «Invocar CU-02006 y CU-02007 y recién después enviar» | «El sistema debería haber interpretado antes» |
| Calificar siempre `Pendiente` | «cuenta `Pendiente`», «trabajo en estado `Pendiente`» | «pendiente» a secas |
| Nombrar la marca con la palabra «marca» | «la marca de cambio de contraseña pendiente está puesta» | «la cuenta está pendiente» para nombrarla |
| No llamar baja al reseteo, ni al revés | «resetear la contraseña», «dar de baja la cuenta» | «resetear la cuenta», «borrar la contraseña» |
| No prometer lo que el dominio no hace | «No emitir acceso» | «Reintentar en unos segundos» |

Una excepción declarada a la regla de calificación: **los nombres de los códigos son identificadores literales del contrato** y no se califican ni se traducen. `CUENTA_PENDIENTE` se escribe así, y su enunciado en prosa sí califica. Es la excepción que `Glosario-Funcional.md` §3.3 ya declara, y calificarla sería el falso positivo que `Vocabulario-Rules.md` §9.1 tipifica como defecto.

## 5. Localización

**El dominio no localiza nada.** Política, en tres reglas:

1. **Los códigos son identificadores estables**, en mayúsculas y sin acentos, y **no se traducen nunca**. Son parte de la superficie pública: renombrar uno es un cambio incompatible para los consumidores y rompe su compilación, que es la señal más temprana posible (`PRODUCT-INTAKE` §17.2.P.3).
2. **El texto que una persona lee no se compone acá.** La traducción de un código a mensaje y a respuesta de protocolo pertenece a `GeometriaFactory-Api` y a la superficie que lo muestra.
3. **Un solo idioma en el producto v1**: español rioplatense. No hay compromiso de traducción y no hay catálogo de recursos que mantener. Si alguna vez lo hubiera, viviría en la capa que compone el mensaje y no acá.

## 6. Cobertura y trazabilidad

### 6.1 Recuento

| Magnitud | Valor |
| --- | --- |
| Casos de uso con sección de excepciones | 13 (CU-02001 a CU-02013) |
| Filas de condición declaradas en la §6 de los trece casos de uso | 50 |
| Condiciones declaradas en más de un caso de uso | 7: `DATO_OBLIGATORIO_AUSENTE` en 3 (CU-02001, CU-02005, CU-02012), y `UNICIDAD_DE_CORREO_NO_VERIFICADA`, `ESTADO_INICIAL_NO_NEGOCIABLE`, `TRANSICION_DESDE_ESTADO_TERMINAL`, `OPERACION_DESCONOCIDA`, `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` (CU-02002, CU-02013) y `VALOR_DERIVADO_VACIO` (CU-02003, CU-02013) en 2 cada una |
| Filas excedentes por repetición | 8 |
| **Condiciones distintas catalogadas** | **42** (50 − 8) |
| Condiciones inventadas por esta categoría | **0** |
| Condiciones de los casos de uso sin entrada en el catálogo | **0** |

Verificación: cada entrada de §3 se lee contra la §6 del caso de uso que la titula, y no hay entrada de §3 que no esté ahí ni fila de esas §6 que falte acá.

**Cinco identificadores retirados**, que aparecen en la cadena y que **no son condiciones de este catálogo**. La constancia va en prosa y no en tabla, deliberadamente: una fila encabezada por un identificador la lee como condición viva cualquier recuento automático sobre las tablas de este documento, y el total daría **47** en lugar de **42**. **Tres se retiraron por renombre y dos por imposibilidad de su causa**, que es un motivo distinto y conviene no mezclarlo.

`RECONSTRUCCION_SOBRE_TRABAJO_FINALIZADO` fue reemplazado por `RECONSTRUCCION_SOBRE_TRABAJO_TERMINAL` en CU-02006 1.1, que lo amplió para alcanzar también a `Rechazado`. `POSICION_DE_PIEZA_NO_CONTIGUA` fue reemplazado por `POSICION_DE_PIEZA_INVALIDA` en CU-02006 1.1, corrección de la ronda r1: un hueco dejó de ser un defecto, porque la posición de una figura que no se pudo reconstruir queda reservada, y lo que se rechaza pasó a ser la posición repetida, negativa o fuera de rango. `CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA` fue reemplazado por `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` en CU-02002 1.2, corrección de la ronda r3, hallazgo H-01: cubría una sola de las cuatro operaciones y dejaba las otras tres sin guarda, de modo que nada impedía bloquear al administrador.

**Los otros dos no los reemplazó ningún identificador: dejó de ser posible su causa.** `CREDENCIAL_NO_ESTABLECIDA`, de CU-02004, describía la cuenta `Habilitado` sin credencial derivada, y `RESETEO_SOBRE_CREDENCIAL_NO_FIJADA`, de CU-02013, el reseteo sobre una cuenta que nunca había fijado ninguna. **RN-02016** (`PRODUCT-INTAKE` 1.13 §4.1) hizo que habilitar produzca y fije la contraseña provisoria, de modo que ninguna cuenta de alumno llega a `Habilitado` sin credencial y el reseteo sobre una cuenta sin credencial simplemente la fija. **Ninguno de los dos se recicla**, y quien busque hoy el encaminamiento del primer ingreso encuentra `CAMBIO_DE_CONTRASENA_PENDIENTE` en §3.4.

Toda cita anterior de cualquiera de los tres resuelve al identificador que lo reemplaza. **Ninguno de los tres se recicla para otra condición**, para que una referencia vieja no resuelva en silencio a un código distinto del que nombraba. Los tres renombres **no alteran el recuento**: en cada caso la condición sigue siendo una sola, con nombre nuevo.

### 6.2 Tabla de cobertura

| Código | CU que lo declara | Regla de negocio | Invariante | Forma |
| --- | --- | --- | --- | --- |
| `DATO_OBLIGATORIO_AUSENTE` | CU-02001, CU-02005, CU-02012 | — | — | Rechazo |
| `UNICIDAD_DE_CORREO_NO_VERIFICADA` | CU-02001, CU-02012 | RN-02002 | INV-01 | Rechazo |
| `CREDENCIAL_NO_ADMITIDA_EN_EL_ALTA` | CU-02001 | — | — | Rechazo |
| `ESTADO_INICIAL_NO_NEGOCIABLE` | CU-02001, CU-02012 | — | INV-08 | Rechazo |
| `PAPEL_DE_ADMINISTRADOR_FUERA_DE_ESTE_CAMINO` | CU-02001 | RN-02001 | INV-05, INV-08 | Rechazo |
| `TRANSICION_DE_CUENTA_NO_ADMITIDA` | CU-02002 | — | — | Rechazo |
| `BAJA_SIN_ARRASTRE_DE_TRABAJOS` | CU-02002 | RN-02007 | — | Rechazo |
| `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` | CU-02002, CU-02013 | RN-02001 | INV-05, INV-08 | Rechazo |
| `HABILITACION_SIN_CREDENCIAL_PROVISORIA` | CU-02002 | RN-02016, RN-02014 | INV-09 | Rechazo |
| `CUENTA_NO_HABILITADA_PARA_CREDENCIAL` | CU-02003 | RN-02006 | INV-06 | Rechazo |
| `CREDENCIAL_YA_FIJADA` | CU-02003 | — | — | Rechazo |
| `CREDENCIAL_VIGENTE_NO_VERIFICADA` | CU-02003 | — | — | Rechazo |
| `VALOR_DERIVADO_VACIO` | CU-02003, CU-02013 | — | — | Rechazo |
| `CUENTA_PENDIENTE` | CU-02004 | RN-02006 | INV-06 | Motivo de resultado |
| `CUENTA_BLOQUEADA` | CU-02004 | RN-02006 | INV-06 | Motivo de resultado |
| `CAMBIO_DE_CONTRASENA_PENDIENTE` | CU-02004 | RN-02013, RN-02016 | INV-09 | Motivo de resultado |
| `TRABAJO_SIN_DUENO` | CU-02005 | RN-02003 | INV-02 | Rechazo |
| `REEDICION_FUERA_DE_BORRADOR` | CU-02005 | RN-02004 | INV-03, INV-07 | Rechazo |
| `TEXTO_ORIGINAL_ALTERADO` | CU-02005 | RN-02008 | — | Rechazo |
| `POSICION_DE_PIEZA_INVALIDA` | CU-02006 | — | — | Rechazo |
| `TIPO_DE_PIEZA_DESCONOCIDO` | CU-02006 | RN-02009 | — | Rechazo parcial |
| `FAMILIA_DECLARADA_CONTRADICE_AL_TIPO` | CU-02006 | — | — | Rechazo |
| `RECONSTRUCCION_SOBRE_TRABAJO_TERMINAL` | CU-02006 | RN-02010 | INV-07 | Rechazo |
| `ESPECIE_DE_OBSERVACION_DESCONOCIDA` | CU-02007 | RN-02005 | INV-04 | Rechazo |
| `ERROR_SIN_UBICACION` | CU-02007 | RN-02009 | — | Rechazo |
| `ADVERTENCIA_SIN_LOS_DOS_VALORES` | CU-02007 | — | — | Rechazo |
| `OBSERVACION_SOBRE_PIEZA_INEXISTENTE` | CU-02007 | RN-02009 | — | Rechazo |
| `ENVIO_FUERA_DE_BORRADOR` | CU-02008 | RN-02005 | INV-04 | Rechazo |
| `TRANSICION_DESDE_ESTADO_TERMINAL` | CU-02008, CU-02010 | RN-02010 | INV-07 | Rechazo |
| `ENVIO_SIN_INTERPRETACION` | CU-02008 | RN-02005 | INV-04 | Rechazo |
| `DESENLACE_NO_ADMITIDO_EN_ESTE_CONTRATO` | CU-02008 | RN-02010 | INV-07 | Rechazo |
| `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE` | CU-02009 | RN-02003 | INV-02 | Motivo de resultado |
| `OPERACION_FUERA_DE_BORRADOR` | CU-02009 | RN-02004 | INV-03 | Motivo de resultado |
| `OPERACION_DESCONOCIDA` | CU-02009, CU-02011 | — | — | Motivo de resultado |
| `DESENLACE_FUERA_DE_PENDIENTE` | CU-02010 | RN-02010, RN-02011 | INV-07 | Rechazo |
| `DESENLACE_SIN_PAPEL_DE_ADMINISTRADOR` | CU-02010 | RN-02010, RN-02001 | INV-07, INV-05 | Rechazo |
| `DESENLACE_DESCONOCIDO` | CU-02010 | RN-02010 | — | Rechazo |
| `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR` | CU-02011 | RN-02011, RN-02004 | — | Motivo de resultado |
| `ALCANCE_SIN_PAPEL_DE_ADMINISTRADOR` | CU-02011 | RN-02001, RN-02011 | INV-05 | Motivo de resultado |
| `ADMINISTRADOR_YA_CONFIGURADO` | CU-02012 | RN-02001 | INV-05 | Rechazo |
| `CONFIGURACION_SIN_CREDENCIAL` | CU-02012 | — | — | Rechazo |
| `RESETEO_CON_ARRASTRE_DE_TRABAJOS` | CU-02013 | RN-02012 | INV-09 | Rechazo |

Las **dieciséis** reglas quedan alcanzadas y los nueve invariantes vigentes también. Las columnas con guion no son un vacío a completar: hay condiciones que sostienen una precondición del contrato de uso sin que ninguna regla de negocio las enuncie por separado, como `CREDENCIAL_YA_FIJADA` o `DESENLACE_DESCONOCIDO`. Inventarles una regla sería el defecto contrario al que este catálogo evita.

Dos guiones tienen origen declarado fuera de las dieciséis reglas y conviene dejarlo escrito, porque la atribución equivocada sería fácil de reponer:

| Condición | Origen de la exigencia | Por qué **no** es RN-02009 |
| --- | --- | --- |
| `POSICION_DE_PIEZA_INVALIDA` | `PRODUCT-INTAKE` §17.1.P.11 punto 2: la identidad de la pieza es su posición en el conjunto raíz | RN-02009 gobierna la **ubicación de la observación**, no la identidad de la pieza. La distinción está declarada en CU-02006 §9 |
| `ADVERTENCIA_SIN_LOS_DOS_VALORES` | `NB-00005` §5, tercer criterio de éxito: el 100 % de las advertencias se muestran con los dos valores expresados, el declarado y el derivado | El §3 de RN-02009 **excluye explícitamente de su ámbito** a las advertencias de discrepancia de valor, que llevan su propia exigencia. Ninguna de las dieciséis reglas la enuncia |
| `ESTADO_INICIAL_NO_NEGOCIABLE` | El estado inicial de cada camino de alta, declarado por CU-02001 §4 paso 6 y por CU-02012 §4, sobre `Definicion-Modelo-De-Dominio.md` §5.1 | Ninguna de las dieciséis reglas enuncia con qué estado nace una cuenta, y por eso la columna de regla queda en guion. **La columna de invariante sí se llenó**: INV-08, ya adoptado, es exactamente esa condición. La cita de INV-05 como fundamento se retiró en 02: ese invariante habla de la **unicidad** del administrador y de su ventana de alta, no del estado con el que nace |

**Sobre la columna de invariante e INV-08.** La versión anterior de este catálogo anotaba INV-08 entre paréntesis y no lo contaba, porque `Definicion-Modelo-De-Dominio.md` §4.2 lo **proponía** como candidato no vigente. `PRODUCT-INTAKE` §17.1.P.2 lo **adoptó**, con el enunciado ampliado a todo el ciclo de vida: la cuenta con papel `Administrador` está **siempre** `Habilitado` y toda cuenta con papel `Alumno` nace `Pendiente`. En consecuencia, las tres filas que lo anotaban entre paréntesis pasan a declararlo como invariante vigente, y los invariantes alcanzados por el catálogo son **los nueve**, INV-01 a INV-09. El recorrido de la adopción queda registrado en `Definicion-Modelo-De-Dominio.md` §4.2. **INV-09 es el invariante nuevo del intake 1.7** y lo sostienen tres condiciones: el motivo `CAMBIO_DE_CONTRASENA_PENDIENTE` de CU-02004, que es donde el dominio ejerce la guarda; el rechazo `RESETEO_CON_ARRASTRE_DE_TRABAJOS` de CU-02013; y, desde el intake 1.13, `HABILITACION_SIN_CREDENCIAL_PROVISORIA` de CU-02002, que es la condición con la que **RN-02016** impide que una cuenta llegue a `Habilitado` sin credencial. El rechazo `RESETEO_SOBRE_CREDENCIAL_NO_FIJADA`, que lo sostenía hasta la versión 1.4, quedó retirado por imposibilidad de su causa (§6.1).

### 6.3 Trazabilidad del artefacto

**Quick-start: no aplicable en este documento, y el motivo es explícito.** El criterio de `Rules-UX-UI-DX.md` §6 pide un quick-start verificable en cada documento `dx-`; acá no corresponde porque este artefacto es del modo **reference** y se consulta por código, no se recorre de principio a fin: no hay una secuencia de pasos que produzca un primer resultado. El quick-start del proyecto de código es único y vive en [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §3, con su compromiso de verificación por punto de control en §3.2, y su recorrido guiado en [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) §2 y §3. Duplicarlo acá crearía una segunda fuente de verdad sobre pasos ejecutables, que es exactamente lo que se desincroniza primero. **No se da por cumplido: se declara no aplicable.**

| Dimensión | Referencia |
| --- | --- |
| Rol de intervención | Mantenedor del dominio e integrador de capa (`DX-Developer-Experience.md` §1.1) |
| Superficie pública que se documenta | Las 42 condiciones de error de los trece contratos de uso |
| CU origen | CU-02001 a CU-02013, §6 de cada uno |
| Reglas de negocio relevantes | RN-02001 a RN-02016; invariantes INV-01 a INV-09 |
| Necesidades de negocio | NB-00001, NB-00002, NB-00003, NB-00004, NB-00005, NB-00009 |
| Wireframes asociados | N/A. `tiene_ui_final` == false |
| US a generar en 06 | US del catálogo de condiciones mantenido junto al código, US de traducción de código a respuesta en la capa que expone, US de la indistinguibilidad de `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`, US de los dos caminos de alta con su estado inicial propio |
| Tests previstos en 08 | Una prueba unitaria pura y sin dobles por condición, más una prueba de cobertura que verifique que ninguna condición del catálogo quedó sin ejercitar |
| Catálogo de diseño aplicado | N/A para variante DX |
| Configuración dirigida por esquema, primer arranque, acceso de operador único, identidad de versión | N/A. Ninguna de las cuatro extensiones aplica a este proyecto de código |
| Validación visual de maqueta y línea de base | N/A. `requiere_maqueta` == false |

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. Cataloga las **37 condiciones de error distintas** derivadas una por una de la §6 de los once casos de uso, sin inventar ninguna y sin dejar ninguna sin cubrir, sobre 40 filas declaradas con 3 condiciones que aparecen en dos casos de uso. Declara la distinción entre condición de error, observación y comentario; la taxonomía en cuatro categorías en uso, con «conflicto de facultad» agregada y justificada, y dos categorías declaradas vacías con su motivo; la forma de terminación como dimensión ortogonal, que separa el rechazo del motivo de resultado; el tono y voz con la excepción de calificación de los identificadores literales; y la política de localización, que no traduce códigos y no compone mensajes acá. Registra que `RECONSTRUCCION_SOBRE_TRABAJO_FINALIZADO` fue renombrada por CU-02006 1.1 y no es una condición faltante. **Corrección de la ronda r1 del audit, sobre esta misma emisión**: alineación con el cierre de AG-02 del hallazgo P1-01, que decidió que la posición de una figura que no se pudo reconstruir **queda reservada** y que el conjunto de piezas adoptadas admite huecos. En consecuencia, `POSICION_DE_PIEZA_NO_CONTIGUA` pasa a `POSICION_DE_PIEZA_INVALIDA` con su causa reescrita —posición repetida, negativa o fuera de rango— y con la advertencia de que renumerar las piezas adoptadas desplazaría el índice que el alumno ve; la causa de `OBSERVACION_SOBRE_PIEZA_INEXISTENTE` se reescribe sobre el rango del conjunto raíz interpretado, porque la redacción anterior tipificaba como defecto el caso legítimo del escenario canónico; y §6.1 pasa a registrar los dos identificadores retirados por renombre, con la constancia de que el recuento sigue en 37. **P3-02**: la columna de regla de `ADVERTENCIA_SIN_LOS_DOS_VALORES` deja de ser RN-02009, cuyo §3 excluye explícitamente de su ámbito a las advertencias de discrepancia de valor, y §6.2 suma la tabla que declara el origen real de esa exigencia, `NB-00005` §5 tercer criterio, y el de `POSICION_DE_PIEZA_INVALIDA`, `PRODUCT-INTAKE` §17.1.P.11 punto 2. §6.3 declara el quick-start **no aplicable con su motivo** en lugar de omitirlo en silencio. |
| 1.1 | 2026-08-09 | Alineación con la **corrección del P0** que reporta `B-02-03-GeometriaFactory-Application-r1.md` y que AG-02 resolvió emitiendo **CU-02012**, la configuración de la cuenta de administrador en el primer arranque. El catálogo pasa de **37 a 40 condiciones** sobre **doce** casos de uso y 46 filas declaradas, con 5 condiciones repetidas y 6 filas excedentes. **§1.4 nueva**: declara los dos caminos de alta con su estado inicial y su tratamiento de la credencial, el fundamento de que la cuenta del administrador nace `Habilitado` porque es la que habilita a las demás, y la advertencia de que el mismo identificador `ESTADO_INICIAL_NO_NEGOCIABLE` tiene **causas opuestas** según el camino. **§3.1** acota `CREDENCIAL_NO_ADMITIDA_EN_EL_ALTA` y `ESTADO_INICIAL_NO_NEGOCIABLE` al auto-registro y da de alta `PAPEL_DE_ADMINISTRADOR_FUERA_DE_ESTE_CAMINO`. **§3.12 nueva** cataloga las cinco condiciones de CU-02012, con las altas `ADMINISTRADOR_YA_CONFIGURADO` y `CONFIGURACION_SIN_CREDENCIAL`. Es el primer código del catálogo con **fila completa en dos subsecciones**, y §3 declara esa excepción con su motivo. §2.1 actualiza los recuentos por categoría —18, 3, 14 y 5—; §6.1 y §6.2 el recuento y la cobertura; §6.2 suma además el origen del estado inicial, que **ninguna de las once reglas enuncia**, y la nota de que el invariante candidato **INV-08 no es vigente** y no se cuenta. |
| 1.2 | 2026-08-09 | Alineación con la corrección del **P1** de la ronda r3, informe `B-02-03-GeometriaFactory-Domain-r3.md`, hallazgo **H-01**: nada impedía **bloquear** la cuenta del administrador, con lo que se alcanzaba por otra puerta la misma condición sin salida del P0. **§3.2** renombra `CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA` a `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR`, con la causa ampliada a las **cuatro** operaciones —habilitar, bloquear, rehabilitar y dar de baja—, su regla RN-02001 y el fundamento citado de la capacidad F-03, que ya las declaraba sobre cuentas **de alumno**; el intro de la subsección declara ese alcance. **§1.4** suma el cierre de la familia: el alta es sólo una de las dos puertas, y el efecto no se agota en el acceso —**sin administrador nadie aprueba ni rechaza y el circuito de revisión entero se detiene**, con todo trabajo enviado en estado `Pendiente` para siempre—. **§6.1** pasa la constancia de identificadores retirados **de tabla a prosa**, para que ningún recuento automático la lea como condición viva, y suma el tercer retiro con la declaración de que ninguno se recicla. §6.2 actualiza la fila y la nota del candidato **INV-08**, cuyo enunciado se amplió a todo el ciclo de vida y que **sigue propuesto y no vigente**. **El recuento no cambia: 40 condiciones distintas**, un identificador retirado y uno nuevo. |
| 1.3 | 2026-08-09 | Alineación con `PRODUCT-INTAKE` **1.7** y con la categoría 02 en su versión 1.4, que emite **CU-02013** —reseteo de contraseña por el administrador, capacidad **F-26**—, las reglas **RN-02012** y **RN-02013**, el invariante **INV-09** y el flujo alternativo del cambio obligatorio en CU-02003. El catálogo pasa de **40 a 43 condiciones** sobre **trece** casos de uso y 51 filas declaradas, con 7 condiciones repetidas y 8 filas excedentes. **§1.5 nueva**: la tabla que separa la baja del reseteo en cinco planos, con la advertencia de que resolver un olvido de contraseña por CU-02002 **no produce ninguna condición de este catálogo** —la baja procede— y es el mismo defecto silencioso que §1.2 describe. **§3.13 nueva** cataloga las cuatro condiciones de CU-02013, con las altas `RESETEO_SOBRE_CREDENCIAL_NO_FIJADA` y `RESETEO_CON_ARRASTRE_DE_TRABAJOS` y con dos códigos **reutilizados con la misma causa**, `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` y `VALOR_DERIVADO_VACIO`. **§3.4** suma el motivo `CAMBIO_DE_CONTRASENA_PENDIENTE`, que como `CREDENCIAL_NO_ESTABLECIDA` **no es un fallo** sino un encaminamiento. §2.1 actualiza los recuentos por categoría —19, 3, 16 y 5—; §2.3 suma CU-02013 a la forma de rechazo; §4 suma dos reglas de voz, la de nombrar la marca con la palabra «marca» y la de no llamar baja al reseteo. **§6.2 retira la anotación de INV-08 como candidato**: el intake lo adoptó, de modo que las tres filas que lo llevaban entre paréntesis lo declaran vigente y los invariantes alcanzados pasan a ser los nueve. |
| 1.4 | 2026-08-09 | Absorbe el `PRODUCT-INTAKE` **1.10** y **cierra la fila de este archivo del hallazgo `F26-20`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0. **Intake 1.10**: las reglas del producto pasan de trece a **quince** con **RN-02014** y **RN-02015**; §6.2 y la trazabilidad de §6.3 actualizan el recuento y el rango, que pasa a **`RN-02001` a `RN-02016`**. **`F26-20`**: **§6.1** advertía que un recuento automático sobre las tablas daría «**41** en lugar de **40**» si los tres identificadores retirados llevaran fila propia, números que son de la versión 1.2 de este catálogo; con **43** condiciones vivas y tres retiradas, los números correctos son **46 en lugar de 43**, y así se escriben. **Ninguna condición entra ni sale del catálogo: siguen siendo 43 sobre 51 filas declaradas, con 0 inventadas y 0 sin entrada.** Sube minor. |
| 1.5 | 2026-08-10 | Absorbe el `PRODUCT-INTAKE` **1.13** y su regla **RN-02016** —habilitar una cuenta produce su contraseña provisoria, la fija y deja la cuenta con cambio de contraseña pendiente—, con la precisión de **F-04** que la acompaña. El catálogo pasa de **43 a 42 condiciones** sobre los mismos trece casos de uso y **50** filas declaradas, con las mismas 7 condiciones repetidas y 8 filas excedentes. **§3.2** da de alta `HABILITACION_SIN_CREDENCIAL_PROVISORIA`, y su intro pasa de tres casos a cuatro. **§3.4** retira `CREDENCIAL_NO_ESTABLECIDA` —su causa, la cuenta `Habilitado` sin credencial, dejó de ser posible— y su intro pasa de cuatro motivos a tres; la fila de `CAMBIO_DE_CONTRASENA_PENDIENTE` declara los **dos** actos que la producen y que es también el motivo del primer ingreso. **§3.13** retira `RESETEO_SOBRE_CREDENCIAL_NO_FIJADA` por el mismo motivo, y su intro pasa de cuatro casos a tres. **§1.3** cambia el ejemplo canónico de motivo de resultado, que era justamente el código retirado. **§2.1** actualiza los recuentos por categoría —**20, 2, 15 y 5**—. **§6.1** pasa los identificadores retirados de tres a **cinco** y distingue los tres que se retiraron por **renombre** de los dos que se retiraron por **imposibilidad de su causa**, con el número corregido del recuento automático, **47 en lugar de 42**. **§6.2** rehace las tres filas alcanzadas, declara las dieciséis reglas y reescribe las condiciones que sostienen INV-09. **0 condiciones inventadas y 0 condiciones de los casos de uso sin entrada.** Sube minor. |
