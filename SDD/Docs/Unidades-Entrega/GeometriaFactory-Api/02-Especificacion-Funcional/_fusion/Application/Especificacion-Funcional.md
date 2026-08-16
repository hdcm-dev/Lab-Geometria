# Especificación funcional — GeometriaFactory-Application

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** Especificacion-Funcional.md
**Versión:** 1.8
**Estado:** Aprobado
**Fecha:** 2026-08-13
**Autor:** Analista Funcional + API Designer (AG-02)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** `00-Contexto/Vision-Producto.md` §1, §3 y §9 (glosario raíz de la cadena); `00-Contexto/Alcance-Producto.md` §4.1 y §5; `01-Necesidades-Negocio/Necesidades-Negocio.md` §2, §4 y §5.3, y las necesidades NB-00001 a NB-00007 y NB-00009; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.14**, §17.2 íntegro —en particular §17.2.P.2 (inversión de dependencias), §17.2.P.5 (verificación de pertenencia), §17.2.P.10 y §17.2.P.11—, §4 (con F-26), §4.1 (**las dieciséis reglas**, con **RN-04016**), §4.2 (modelo de estados del trabajo), §6, §7 (CL-7), §9 (X-2 retirada), §12, §14 y §17.1.P.2 (INV-09); `Proyectos/GeometriaFactory-Domain/02-Especificacion-Funcional/` completo, cuyos **trece** casos de uso esta categoría orquesta
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico` y `08-Calidad-Y-Pruebas` de GeometriaFactory-Application

---

## Tabla de contenido

- [1. Alcance funcional de este proyecto de código](#1-alcance-funcional-de-este-proyecto-de-código)
- [2. Documentos de esta categoría](#2-documentos-de-esta-categoría)
- [3. Los puertos que esta capa declara](#3-los-puertos-que-esta-capa-declara)
- [4. Autorización por pertenencia y verificación de facultad](#4-autorización-por-pertenencia-y-verificación-de-facultad)
- [5. Catálogo de casos de uso](#5-catálogo-de-casos-de-uso)
- [6. Reglas de negocio que esta capa hace cumplir](#6-reglas-de-negocio-que-esta-capa-hace-cumplir)
- [7. Matriz NB → CU → RN → US](#7-matriz-nb--cu--rn--us)
  - [7.1 Matriz](#71-matriz)
  - [7.2 Cobertura bidireccional](#72-cobertura-bidireccional)
  - [7.3 Historias de usuario previstas](#73-historias-de-usuario-previstas)
  - [7.4 Casos de uso de dominio orquestados](#74-casos-de-uso-de-dominio-orquestados)
- [8. Criterio de recorte aplicado](#8-criterio-de-recorte-aplicado)
- [9. Omisiones declaradas](#9-omisiones-declaradas)
- [10. Numeración y nombres de archivo](#10-numeración-y-nombres-de-archivo)
- [11. Puntos abiertos](#11-puntos-abiertos)
- [12. Control de cambios](#12-control-de-cambios)

---

## 1. Alcance funcional de este proyecto de código

`GeometriaFactory-Application` contiene los **casos de uso** del producto y los **puertos** que la infraestructura implementa. Depende únicamente de `GeometriaFactory-Domain` y de nada más; lo consumen `GeometriaFactory-Api`, por sus casos de uso, y `GeometriaFactory-Infrastructure`, por sus puertos. Es el nivel 1 del orden topológico del producto.

Esta especificación tiene la forma de la variante `library` de la categoría: **cada caso de uso describe un contrato de uso de la superficie pública**, no un flujo de pantallas. El actor primario de los **once** casos de uso es el código que consume la biblioteca; el alumno y el administrador aparecen como sujetos de las reglas, nunca como actores.

Dos rasgos distinguen a esta capa de la de dominio, y los dos recorren todos sus casos de uso:

1. **La dependencia se invierte.** Esta capa declara qué necesita —guardar y recuperar, interpretar el texto del alumno, saber qué hora es— y otra capa lo provee. Es lo que permite ejercer un caso de uso entero con dobles, sin base de datos ni frontera de proceso. Un caso de uso de esta categoría que mencionara el motor de persistencia, el mecanismo de acceso o el protocolo de transporte estaría mal ubicado.
2. **Acá se decide quién puede hacer qué.** El dominio declara las condiciones; esta capa las ejerce sobre el pedido concreto, antes de tocar el repositorio. Es autorización, no autenticación: no se comparan contraseñas ni se emiten accesos.

Lo que **no** está acá, y dónde está: las entidades, los invariantes y las máquinas de estado, en `GeometriaFactory-Domain`; la interpretación efectiva del texto, la derivación de la contraseña, la emisión del acceso y el guardado, en `GeometriaFactory-Infrastructure`; los datos que cruzan la frontera del proceso, en `GeometriaFactory-Contracts`; las páginas y el dibujo, en `GeometriaFactory-Web` y `GeometriaFactory-Visor`.

## 2. Documentos de esta categoría

| Documento | Propósito |
| --- | --- |
| `Especificacion-Funcional.md` | Este archivo: índice maestro, catálogos y matriz de trazabilidad |
| [`Glosario-Funcional.md`](Glosario-Funcional.md) | Vocabulario que esta categoría acuña, con los términos de más de un referente |
| `Casos-De-Uso/CU-XX-<Nombre>.md` | Once casos de uso, uno por archivo |
| [`README.md`](README.md) | Índice navegable de la sección, con el orden de lectura y las omisiones y su motivo |

## 3. Los puertos que esta capa declara

Los puertos son la frontera de este proyecto de código: lo que declara acá lo implementa `GeometriaFactory-Infrastructure`, y la composición de raíz los provee. `PRODUCT-INTAKE` §17.2.P.1 y §14 los nombran una vez, y esa es la única cita de identificadores de código de esta categoría: `IWorkRepository`, `IFigureValidator` e `ISystemClock`. En el resto de los artefactos los puertos se nombran en lenguaje de dominio, porque los nombres definitivos de tipos se validan en el punto de control de la etapa `a`.

| Puerto | Qué le pide esta capa | Casos de uso que lo consumen |
| --- | --- | --- |
| Repositorio de trabajos | Recuperar un trabajo, resolver una consulta ya acotada por dueño o por alcance, materializar el resultado y ejecutar el retiro | CU-04002, CU-04004, CU-04005, CU-04006, CU-04007, CU-04008, CU-04009 |
| Validación de figuras | Interpretar el texto original y devolver **la cantidad de figuras del conjunto raíz**, las piezas reconstruidas y las observaciones, con su especie y su ubicación | CU-04005 |
| Reloj del sistema | Los sellos de alta, de modificación y de desenlace, **para que sean verificables en prueba** | CU-04001, CU-04003, CU-04004, CU-04005, CU-04008, CU-04010, CU-04011 |
| Repositorio de cuentas | Recuperar una cuenta por su correo, responder si un correo ya está registrado y si ya existe una cuenta con papel `Administrador`, y materializar el resultado, **incluida la marca de cambio de contraseña pendiente** | CU-04001, CU-04002, CU-04003, CU-04007, CU-04010, CU-04011 |

**El repositorio de cuentas no lleva identificador declarado en el intake**, que nombra los otros tres. No es una invención de esta categoría: `GeometriaFactory-Domain` §1 de su índice asigna explícitamente a esta capa la verificación de la unicidad del correo «sobre el conjunto de alumnos», y ninguna verificación sobre un conjunto es posible sin una frontera que lo alcance. Queda declarado como punto abierto en §11.

**Dos precisiones sobre lo que viaja por los puertos:**

- **Los sellos de alta, de modificación y de desenlace son metadatos de orquestación de esta capa.** No son la «Fecha» que el alumno declara en su trabajo, que sí modela el dominio como dato del alumno. El modelo del dominio declara la fecha de alta del alumno —que recibe del consumidor, sin leer el reloj— y **no declara** fecha de última modificación de la cuenta ni fecha de creación, de modificación o de desenlace del trabajo. La discrepancia está elevada al Product Owner: hasta que resuelva, estos sellos se leen como dato de esta capa y no como atributos del dominio.
- **La cantidad de figuras del conjunto raíz la produce el validador** al interpretar el texto, incluidas las figuras que no pudo reconstruir, y **no es derivable de las piezas adoptadas**, que admiten huecos. El dominio la exige como precondición de la reconstrucción y su registro de observaciones la hereda como rango de posiciones válidas, de modo que CU-04005 —único orquestador de los dos— es quien la hace viajar.

**El alcance de la unidad de trabajo es un caso de uso, una transacción**: cada caso de uso abre a lo sumo una y no la reparte entre varias operaciones.

## 4. Autorización por pertenencia y verificación de facultad

Es lo que hace que el flag `tiene_auth` valga true en este proyecto de código, y es transversal a los once casos de uso. No es autenticación: acá no se comparan contraseñas ni se emiten accesos, y quién es la persona llega ya resuelto desde afuera.

| Comprobación | Qué verifica | Respuesta cuando falla | Dónde se ejerce |
| --- | --- | --- | --- |
| **Pertenencia** | Que el trabajo pedido sea del alumno solicitante | `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`, que el consumidor traduce a «no encontrado» y **nunca** a «no autorizado» | CU-04004, CU-04005, CU-04006, CU-04009 |
| **Facultad** | Que quien pide una operación reservada tenga el papel `Administrador` | `FACULTAD_DE_ADMINISTRADOR_REQUERIDA`, que sí admite ser explícito: no hay recurso ajeno cuya existencia proteger | CU-04002, CU-04007, CU-04008, CU-04011 |
| **Alcance del administrador** | Que el trabajo no esté en `Borrador`, porque los borradores no forman parte de su flujo de trabajo | `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR` | CU-04007, CU-04008, CU-04009 |
| **Cambio de contraseña pendiente** | Que la cuenta solicitante **no** esté marcada por un reseteo del administrador. Es la comprobación que hace exigible el invariante **INV-09** del intake §17.1.P.2 | `CAMBIO_DE_CONTRASENA_PENDIENTE`. No lee ni escribe nada: la cuenta **se autentica y no obtiene sesión de trabajo**, y lo único que puede hacer es cambiar su contraseña (RN-04013, intake 1.8 §4.1) | **Todos**, con una sola excepción declarada: el reemplazo de CU-04003 FA-05, que es lo único que la levanta. Ver la precisión 5 |

**Una sola negativa de facultad, y dos motivos del dominio detrás.** El dominio declara dos códigos para la misma negativa —uno en su resolución de desenlace y otro en la de alcance del administrador—, y esta capa emite uno solo: corta con su propia verificación **antes** de invocar al dominio, de modo que ninguno de los dos llega a producirse. Quien lea las dos capas no debe leer tres negativas de facultad donde hay una.

Cinco precisiones que rigen en toda la categoría:

1. **El papel no reemplaza a la pertenencia.** Son dos comprobaciones distintas: un alumno autenticado no debe poder leer el trabajo de otro cambiando el identificador de la petición, y ningún papel resuelve eso.
2. **La negativa por pertenencia y la negativa por facultad no se confunden.** La primera oculta la existencia del recurso; la segunda no tiene nada que ocultar.
3. **La comprobación se hace sobre el dato recuperado y antes de escribir.** No se resuelve ocultando un control en la pantalla, y por eso es verificable con dobles sin base de datos.
4. **El trabajo ajeno y el identificador inexistente comparten motivo por diseño.** Distinguirlos permitiría averiguar por tanteo qué identificadores existen.
5. **La cuarta comprobación corta antes que las otras tres, y tiene una sola excepción.** Una cuenta marcada como con cambio de contraseña pendiente no ejerce **ninguna** capacidad del sistema —ni siquiera las que su papel y su pertenencia admitirían—, salvo cambiar su propia contraseña por el reemplazo de CU-04003 FA-05. La marca la ponen **las dos** operaciones que producen una contraseña provisoria —la **habilitación** de CU-04002 (RN-04016) y el **reseteo** de CU-04011 (RN-04014)— y la levanta **únicamente** ese cambio, hecho por la propia cuenta: eso es INV-09, y es lo que hace que la provisoria sea provisoria. Sin él, una clave que el administrador conoce quedaría sirviendo indefinidamente para operar como el alumno. **Es una comprobación de esta capa y no una decisión de ruteo del front**: ocultar rutas acota lo que se ofrece y no hace cumplir nada.

## 5. Catálogo de casos de uso

| CU | Nombre | Contrato que describe | Estado |
| --- | --- | --- | --- |
| CU-04001 | [`CU-04001` · Registrar el alta de una cuenta](../../Casos-De-Uso/CU-04001-Registrar-El-Alta-De-Una-Cuenta.md) | Auto-registro del alumno: correo libre, cuenta constituida en estado `Pendiente` y sin credencial | Propuesto |
| CU-04002 | [`CU-04002` · Gobernar las cuentas de la comisión](../../Casos-De-Uso/CU-04002-Gobernar-Las-Cuentas-De-La-Comision.md) | Habilitar, bloquear, rehabilitar y dar de baja, con confirmación escrita y arrastre de los trabajos; **habilitar y rehabilitar producen además la contraseña provisoria** (RN-04016) | Propuesto |
| CU-04003 | [`CU-04003` · Resolver el ingreso y la credencial del alumno](../../Casos-De-Uso/CU-04003-Resolver-El-Ingreso-Y-La-Credencial-Del-Alumno.md) | Admisibilidad de la cuenta con su motivo, y fijación —solicitada por CU-04002 al habilitar— y reemplazo de la credencial derivada | Propuesto |
| CU-04004 | [`CU-04004` · Cargar y reeditar un trabajo propio](../../Casos-De-Uso/CU-04004-Cargar-Y-Reeditar-Un-Trabajo-Propio.md) | Constituir el trabajo con dueño y texto original íntegro, y reeditarlo sólo en `Borrador` | Propuesto |
| CU-04005 | [`CU-04005` · Enviar un trabajo e interpretar su texto](../../Casos-De-Uso/CU-04005-Enviar-Un-Trabajo-E-Interpretar-Su-Texto.md) | La única acción de guardado: interpretar por el puerto, incorporar piezas y observaciones y dejar que el dominio resuelva el estado | Propuesto |
| CU-04006 | [`CU-04006` · Consultar los trabajos propios del alumno](../../Casos-De-Uso/CU-04006-Consultar-Los-Trabajos-Propios-Del-Alumno.md) | Listado acotado al dueño y sin componentes, y detalle con desenlace y comentario | Propuesto |
| CU-04007 | [`CU-04007` · Revisar los trabajos de la comisión](../../Casos-De-Uso/CU-04007-Revisar-Los-Trabajos-De-La-Comision.md) | Listado de la comisión sin borradores, con dueño para agrupar y filtrar, y detalle equivalente al del alumno | Propuesto |
| CU-04008 | [`CU-04008` · Dar desenlace a un trabajo](../../Casos-De-Uso/CU-04008-Dar-Desenlace-A-Un-Trabajo.md) | Aprobar o rechazar desde estado `Pendiente`, con comentario opcional y terminalidad | Propuesto |
| CU-04009 | [`CU-04009` · Eliminar un trabajo](../../Casos-De-Uso/CU-04009-Eliminar-Un-Trabajo.md) | Retiro con los dos alcances opuestos: el alumno sólo en `Borrador`, el administrador en todo lo que ve | Propuesto |
| CU-04010 | [`CU-04010` · Configurar la cuenta de administrador](../../Casos-De-Uso/CU-04010-Configurar-La-Cuenta-De-Administrador.md) | El segundo camino de alta: cuenta única con papel `Administrador`, `Habilitado` y con credencial, sólo mientras no exista ninguna | Propuesto |
| CU-04011 | [`CU-04011` · Resetear la contraseña de un alumno](../../Casos-De-Uso/CU-04011-Resetear-La-Contrasena-De-Un-Alumno.md) | Contraseña provisoria **producida por el sistema** y devuelta una vez, con marca de cambio pendiente, conservando la cuenta, **su estado —cualquiera sea—** y **todos sus trabajos** | Propuesto |

Once casos de uso, sobre un mínimo de cinco para el tipo `library`.

## 6. Reglas de negocio que esta capa hace cumplir

**Las reglas del producto viven en `GeometriaFactory-Domain` y acá se referencian, no se redactan.** Lo que esta tabla declara es dónde se ejerce cada una en esta capa, que es una cosa distinta de dónde está enunciada. **Quince de las dieciséis tienen tramo acá** —la excepción es RN-04014, que se explica más abajo—, y en dos el tramo principal está en otra capa: RN-04005, que resuelve el dominio sobre el conjunto de observaciones que esta capa le entrega, y RN-04009, cuyo mensaje ubicado lo produce el validador detrás del puerto. Las dos filas lo declaran.

**Dos de las dieciséis —RN-04012 y RN-04013— entraron con el `PRODUCT-INTAKE` 1.7, otras dos —RN-04014 y RN-04015— con el 1.10 y **RN-04016** con el 1.13; las cinco tienen archivo en `GeometriaFactory-Domain`**, de modo que se enlazan como las once anteriores y el punto abierto de §11 quedó cerrado. **Esta categoría no las redacta**: hacerlo crearía dos enunciados de la misma regla en la misma cadena documental, que es exactamente lo que §9 evita.

**Las dos reglas nuevas merecen una precisión, y la primera es la única sin tramo acá.** **RN-04014** —la provisoria la produce el sistema, no es adivinable y no se repite— sí se **exige por escrito** en `CU-04011` §10, pero **no se ejerce acá**: el valor llega a esta capa ya producido y ya derivado, del mismo lado de la frontera desde el que llega la contraseña que el alumno elige, de modo que quien la ejerce es `GeometriaFactory-Infrastructure` y quien la verifica en prueba es `GeometriaFactory-Contracts` `CU-04008` CA-10. **RN-04015** —resetear no exige cuenta habilitada— se ejerce de forma **negativa**: lo que esta capa hace por ella es **no comprobar** el estado de la cuenta en `CU-04011` §4 y no devolver ningún motivo por ese concepto, que es lo que `CA-06` y `CA-07` verifican.

| RN | Enunciado en una línea | Dónde se ejerce en esta capa |
| --- | --- | --- |
| [RN-02001](../../Reglas-De-Negocio/RN-02001-Administrador-Unico-Y-Papeles-Fijos.md) | Administrador único y papeles fijos | CU-04010 (ventana de alta y su negativa), CU-04001 (rechazo del papel `Administrador` por el auto-registro), CU-04002, CU-04003, CU-04007, CU-04008, CU-04011 (verificación de facultad; en CU-04011, además, el acotamiento del reseteo a cuentas de alumno) |
| [RN-02002](../../Reglas-De-Negocio/RN-02002-Correo-Del-Alumno-Unico.md) | El correo del alumno es único | CU-04001 y CU-04010: la verificación sobre el conjunto de cuentas es de esta capa, en los dos caminos de alta |
| [RN-02003](../../Reglas-De-Negocio/RN-02003-Trabajo-Ajeno-Indistinguible-De-Inexistente.md) | Un alumno sólo ve y opera sus propios trabajos | CU-04004, CU-04005, CU-04006, CU-04009: la verificación de pertenencia |
| [RN-02004](../../Reglas-De-Negocio/RN-02004-Eliminacion-Acotada-Al-Borrador.md) | El alumno elimina sólo en borrador; el administrador, cualquier trabajo que ve | CU-04009 en sus dos alcances, y CU-04002 en el arrastre de la baja |
| [RN-02005](../../Reglas-De-Negocio/RN-02005-Finalizacion-Sin-Errores-De-Validacion.md) | Un trabajo no pasa a estado `Pendiente` con errores de validación | CU-04005, **con el tramo principal en el dominio**: esta capa entrega el conjunto de observaciones y el dominio resuelve el estado |
| [RN-02006](../../Reglas-De-Negocio/RN-02006-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md) | Una cuenta `Pendiente` o `Bloqueado` no obtiene acceso | CU-04003: la consulta de admisibilidad con su motivo. CU-04001 y CU-04010 en cuanto fijan estados iniciales opuestos, que es lo que decide si la cuenta admite acceso desde el alta |
| [RN-02007](../../Reglas-De-Negocio/RN-02007-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md) | La baja arrastra los trabajos y exige confirmación escrita | CU-04002: la comparación del correo escrito y el retiro de todos los trabajos en la misma unidad de trabajo. **CU-04011 por contraste**: el reseteo no la dispara |
| [RN-02008](../../Reglas-De-Negocio/RN-02008-Texto-Original-Conservado-Integro.md) | El texto original del alumno se conserva íntegro | CU-04004 y CU-04005: el texto se entrega tal cual y no se reescribe ni cuando la interpretación falla |
| [RN-02009](../../Reglas-De-Negocio/RN-02009-Observacion-De-Error-Con-Posicion-Y-Campo.md) | Toda observación de error indica la posición de la pieza y el campo | CU-04005, **con el tramo principal en el validador**, que produce el mensaje ubicado detrás del puerto. Lo que esta capa aporta es la cantidad de figuras del conjunto raíz, que es el rango contra el que la posición se valida, y el rechazo del conjunto mal formado, que no llega al alumno |
| [RN-02010](../../Reglas-De-Negocio/RN-02010-Desenlace-Exclusivo-Del-Administrador-Y-Terminalidad.md) | El desenlace es exclusivo del administrador y es terminal | CU-04008: la verificación de facultad y la propagación de la terminalidad |
| [RN-02011](../../Reglas-De-Negocio/RN-02011-El-Administrador-No-Ve-Los-Borradores.md) | El administrador no ve los trabajos en borrador | CU-04007, CU-04008 y CU-04009: el predicado de alcance trasladado a la consulta |
| [**RN-02012**](../../Reglas-De-Negocio/RN-02012-Reseteo-Conserva-La-Cuenta-Y-Sus-Trabajos.md) | El reseteo de contraseña conserva la cuenta y sus trabajos, y no es una baja | CU-04011: la postcondición que deja intactos estado de habilitación, papel, identidad y **todos los trabajos con sus estados y comentarios**, y la ausencia deliberada de todo retiro |
| [**RN-02014**](../../Reglas-De-Negocio/RN-02014-Provisoria-Producida-Por-El-Sistema.md) | La contraseña provisoria la produce el sistema: no es adivinable y no se repite entre cuentas ni entre reseteos | **No se ejerce acá**, y `CU-04011` §10 la **exige por escrito** para que no se pierda al bajar de contrato a implementación: el valor llega ya producido y ya derivado. La ejerce `GeometriaFactory-Infrastructure` y la verifica `GeometriaFactory-Contracts` `CU-04008` CA-10 |
| [**RN-02015**](../../Reglas-De-Negocio/RN-02015-Reseteo-Independiente-Del-Estado-De-Cuenta.md) | Resetear no exige que la cuenta esté habilitada | CU-04011, **de forma negativa**: §4 no comprueba el estado de la cuenta, §6 no declara ningún motivo por ese concepto y CA-06 y CA-07 lo verifican. Es también la fuente del cierre sobre la cuenta de administrador que `RESETEO_ACOTADO_A_CUENTAS_DE_ALUMNO` ejerce |
| [**RN-02013**](../../Reglas-De-Negocio/RN-02013-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md) | Mientras la provisoria no se cambie, la cuenta no llega a ninguna otra parte del sistema: **se autentica y no obtiene sesión de trabajo** | La cuarta comprobación transversal de §4, en **todos** los casos de uso; CU-04003 FA-06, donde la consulta de admisibilidad devuelve no admisible; CU-04003 FA-05, que es el único lugar donde la marca se levanta; CU-04011, que es el único donde se pone |
| [**RN-02016**](../../Reglas-De-Negocio/RN-02016-Habilitar-Produce-La-Provisoria.md) | Habilitar una cuenta produce su contraseña provisoria, con el mismo mecanismo y el mismo tratamiento que la del reseteo, y la deja con **cambio de contraseña pendiente** | CU-04002, en sus operaciones de **habilitar** y **rehabilitar**: piden el valor al puerto de producción, lo derivan y solicitan fijar la credencial derivada provisoria, de modo que la cuenta queda con la marca puesta y sin ninguna ruta que fije una contraseña sin credencial vigente. **CU-04003 por contraste**: FA-02 es donde la fijación se ejerce y FA-05 el único lugar donde la marca se levanta, y lo hace la propia cuenta |

## 7. Matriz NB → CU → RN → US

### 7.1 Matriz

| NB | CU de este proyecto de código | RN aplicables | US previstas en 06 |
| --- | --- | --- | --- |
| [NB-00001](../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00001-Control-De-Admision-Al-Laboratorio.md) · Control de admisión y de bajas del laboratorio | CU-04010, CU-04001, CU-04002, CU-04011 | RN-04001, RN-04002, RN-04006, RN-04007, RN-04012 | US-04003, US-04028, US-04001, US-04004, US-04005, US-04006, US-04029, US-04031 |
| [NB-00002](../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00002-Identidad-Propia-Del-Alumno-Sin-Correo.md) · Identidad propia del alumno sin canal de correo | CU-04001, CU-04003, CU-04011 | RN-04002, RN-04006, RN-04013 | US-04001, US-04002, US-04007, US-04008, US-04009, US-04030, US-04032 |
| [NB-00003](../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00003-Trabajo-Con-Dueno-Estado-Y-Persistencia.md) · Trabajo con dueño, estado y persistencia | CU-04004, CU-04005, CU-04006, CU-04009 | RN-04003, RN-04004, RN-04005, RN-04008 | US-04010, US-04011, US-04012, US-04015, US-04017, US-04018, US-04026 |
| [NB-00004](../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00004-Interpretacion-Fiel-Del-Dato-Del-Alumno.md) · Interpretación fiel del dato del alumno | CU-04004, CU-04005 | RN-04005, RN-04008, RN-04009 | US-04011, US-04013, US-04014, US-04015, US-04016 |
| [NB-00005](../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00005-Visibilidad-Del-Error-De-Calculo.md) · Visibilidad del error de cálculo | CU-04005 | RN-04005, RN-04009 | US-04013, US-04015 |
| [NB-00006](../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00006-Visualizacion-Dentro-Del-Producto.md) · Visualización del trabajo dentro del producto | CU-04006 (parcial: entrega de piezas con identidad posicional) | RN-04003 | US-04019 |
| [NB-00007](../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00007-Revision-De-La-Comision-En-Un-Solo-Lugar.md) · Revisión de la comisión desde un solo lugar | CU-04007 | RN-04001, RN-04011 | US-04020, US-04021, US-04022 |
| [NB-00008](../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00008-Alcance-Del-Laboratorio-Desde-El-Aula.md) · Alcance del laboratorio desde el aula | — | — | — |
| [NB-00009](../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00009-Desenlace-Explicito-De-La-Entrega.md) · Desenlace explícito de la entrega | CU-04008, CU-04009, CU-04006 (parcial), CU-04007 (parcial) | RN-04004, RN-04010, RN-04011 | US-04018, US-04022, US-04023, US-04024, US-04025, US-04027 |

### 7.2 Cobertura bidireccional

**De CU a NB.** Los once casos de uso trazan al menos a una necesidad de negocio; no hay ninguno huérfano.

| CU | NB que implementa |
| --- | --- |
| CU-04001 | NB-00002, NB-00001 |
| CU-04010 | NB-00001 |
| CU-04002 | NB-00001 |
| CU-04003 | NB-00002 |
| CU-04004 | NB-00003, NB-00004 |
| CU-04005 | NB-00004, NB-00005, NB-00003 |
| CU-04006 | NB-00003, NB-00009, NB-00006 |
| CU-04007 | NB-00007, NB-00009 |
| CU-04008 | NB-00009 |
| CU-04009 | NB-00003, NB-00009 |
| CU-04011 | NB-00001, NB-00002 |

**De NB a CU.** Ocho de las nueve necesidades reciben al menos un caso de uso en este proyecto de código. La restante **no la toca este proyecto de código**, y esto es una alerta explícita y no un silencio:

| NB sin CU acá | Por qué | Dónde se cubre |
| --- | --- | --- |
| NB-00008 · Alcance del laboratorio desde el aula | Su dolor no es funcional sino de acceso: viabilidad medida, despliegue y estado degradado explícito. Esta capa no atiende peticiones, no abre conexiones y no conoce la frontera de proceso | 02 de `GeometriaFactory-Web` y `GeometriaFactory-Api`; `09-Devops` |

Dos necesidades quedan cubiertas **parcialmente**, y conviene que se lea así:

- **NB-00006.** Lo que esta capa aporta es la entrega de las piezas con su identidad posicional y sus componentes en el detalle, que es el dato con el que después se dibuja y se arma el árbol. El dibujo, el árbol y la sincronización son de `GeometriaFactory-Visor` y de `GeometriaFactory-Web`.
- **NB-00007.** Lo que esta capa aporta es el listado con el predicado de alcance ya aplicado y el dato de dueño. La agrupación, el orden y el filtro tal como la persona los ejerce son decisiones de presentación de `GeometriaFactory-Web`, y el panel de resumen es una capacidad de prioridad menor con plazo posterior.

### 7.3 Historias de usuario previstas

La numeración es una **previsión** de esta categoría, y la confirma la categoría 06 al redactarlas.

| US prevista | Contenido | CU de origen |
| --- | --- | --- |
| US-04001 | Constituir una cuenta de alumno en estado `Pendiente`, sin credencial | CU-04001 |
| US-04002 | Rechazar el alta con un correo ya registrado | CU-04001 |
| US-04003 | Configurar la cuenta del administrador sólo mientras no exista ninguna, habilitada y con credencial | CU-04010 |
| US-04004 | Habilitar, bloquear y rehabilitar una cuenta con verificación de facultad | CU-04002 |
| US-04005 | Dar de baja una cuenta exigiendo el correo escrito como confirmación | CU-04002 |
| US-04006 | Arrastrar en la baja todos los trabajos de la cuenta, en cualquier estado | CU-04002 |
| US-04007 | Devolver el motivo de una cuenta que no admite ingreso | CU-04003 |
| US-04008 | Fijar la credencial derivada provisoria dentro de la habilitación | CU-04003, CU-04002 |
| US-04009 | Reemplazar la credencial derivada exigiendo la verificación de la vigente | CU-04003 |
| US-04010 | Cargar un trabajo con dueño, identificador propio y fecha tomada del reloj | CU-04004 |
| US-04011 | Conservar el texto original íntegro al cargar y al reeditar | CU-04004 |
| US-04012 | Reeditar sólo un trabajo propio en `Borrador`, descartando la interpretación anterior | CU-04004 |
| US-04013 | Enviar un trabajo con advertencias y que pase a estado `Pendiente` | CU-04005 |
| US-04014 | Enviar un trabajo con errores de validación y que quede en `Borrador` con su ubicación | CU-04005 |
| US-04015 | Interpretar el texto por el puerto de validación, sin tocar la base de datos | CU-04005 |
| US-04016 | Terminar de forma controlada cuando la interpretación no está disponible | CU-04005 |
| US-04017 | Listar los trabajos propios con los cuatro estados distinguibles | CU-04006 |
| US-04018 | Ver el desenlace y el comentario del trabajo propio | CU-04006 |
| US-04019 | Devolver el detalle con piezas y componentes, y el listado sin componentes | CU-04006 |
| US-04020 | Listar los trabajos de la comisión excluyendo los borradores | CU-04007 |
| US-04021 | Filtrar el listado de la comisión por alumno, con el recorte vigente | CU-04007 |
| US-04022 | Abrir el detalle de un trabajo de la comisión con los mismos elementos que ve el alumno | CU-04007 |
| US-04023 | Aprobar un trabajo en estado `Pendiente`, con comentario opcional | CU-04008 |
| US-04024 | Rechazar un trabajo en estado `Pendiente`, con comentario opcional | CU-04008 |
| US-04025 | Rechazar toda transición pedida por quien no tiene la facultad o desde un estado terminal | CU-04008 |
| US-04026 | Eliminar un trabajo propio sólo en `Borrador` | CU-04009 |
| US-04027 | Eliminar por el administrador en los tres estados que ve | CU-04009 |
| US-04028 | Rechazar la configuración de un segundo administrador | CU-04010 |
| US-04029 | Resetear la contraseña de un alumno fijando una provisoria, con verificación de facultad | CU-04011 |
| US-04030 | Impedir que una cuenta con cambio de contraseña pendiente ejerza cualquier otra capacidad | CU-04011, y la comprobación transversal de §4 |
| US-04031 | Conservar la cuenta, su estado de habilitación y todos sus trabajos después del reseteo | CU-04011 |
| US-04032 | Levantar la marca con el cambio efectivo hecho por la propia cuenta, y sólo con él | CU-04003 |

### 7.4 Casos de uso de dominio orquestados

Los **doce** casos de uso de `GeometriaFactory-Domain` quedan orquestados por los once de esta capa. Ninguno queda sin orquestar.

| CU de esta capa | CU de dominio que orquesta |
| --- | --- |
| CU-04001 | [CU-02001](../../Casos-De-Uso/CU-02001-Registrar-El-Alta-De-Un-Alumno.md) |
| CU-04010 | [CU-02012](../../Casos-De-Uso/CU-02012-Configurar-La-Cuenta-De-Administrador.md) |
| CU-04002 | [CU-02002](../../Casos-De-Uso/CU-02002-Gobernar-El-Ciclo-De-Vida-De-La-Cuenta.md) |
| CU-04003 | [CU-02004](../../Casos-De-Uso/CU-02004-Evaluar-La-Admisibilidad-De-La-Cuenta.md), [CU-02003](../../Casos-De-Uso/CU-02003-Fijar-Y-Reemplazar-La-Credencial-Derivada.md) |
| CU-04004 | [CU-02005](../../Casos-De-Uso/CU-02005-Crear-Y-Reeditar-Un-Trabajo.md), [CU-02009](../../Casos-De-Uso/CU-02009-Resolver-El-Acceso-Del-Alumno-A-Un-Trabajo.md) |
| CU-04005 | [CU-02006](../../Casos-De-Uso/CU-02006-Reconstruir-El-Conjunto-De-Piezas-Del-Trabajo.md), [CU-02007](../../Casos-De-Uso/CU-02007-Registrar-Las-Observaciones-Del-Trabajo.md), [CU-02008](../../Casos-De-Uso/CU-02008-Gobernar-El-Estado-Del-Trabajo.md), CU-04009 |
| CU-04006 | CU-04009 |
| CU-04007 | [CU-02011](../../Casos-De-Uso/CU-02011-Resolver-El-Alcance-Del-Administrador-Sobre-Un-Trabajo.md) |
| CU-04008 | [CU-02010](../../Casos-De-Uso/CU-02010-Resolver-El-Desenlace-Del-Trabajo.md), CU-04011 |
| CU-04009 | CU-04009, CU-04011 |
| CU-04011 | [CU-02013](../../Casos-De-Uso/CU-02013-Resetear-La-Contrasena-De-Una-Cuenta-De-Alumno.md), la operación de reseteo del dominio |

**CU-04003 del dominio ya no queda orquestado desde dos casos de uso de esta capa.** La versión anterior declaraba que CU-04011 invocaba el reemplazo de CU-04003 «por facultad y sin conocer la credencial vigente»; el dominio tiene una operación propia para eso —**CU-02013**—, que no exige estado `Habilitado` ni declaración de credencial vigente verificada, y CU-04011 pasa a invocarla. La distinción de sujeto y de autorización que aquella nota describía **sigue siendo cierta y ahora la sostiene el dominio**, con dos operaciones en lugar de dos invocaciones de la misma.

## 8. Criterio de recorte aplicado

- **Piso y techo.** El mínimo para `library` es de cinco casos de uso; el techo lo da la cobertura de las necesidades de negocio que este proyecto de código toca. Quedaron **once**. El décimo es CU-04010 y el undécimo es CU-04011, y las dos causas están declaradas abajo.
- **La partición del reseteo.** **CU-04011 no se fusionó con CU-04002** aunque el administrador ejerza las dos cosas desde el mismo panel, ni con CU-04003 aunque las dos escriban la credencial derivada. Contra CU-04002: el reseteo **no es una transición de la máquina de estados de la cuenta**, escribe credencial, consume el puerto de reloj —que CU-04002 no consume— y deja una marca que las cuatro operaciones de admisión no conocen. Contra CU-04003: el sujeto es otro —el administrador y no la propia persona—, la autorización es otra —facultad y no conocimiento de la credencial vigente— y la postcondición es opuesta, porque CU-04003 FA-05 **levanta** la marca que CU-04011 **pone**. Fusionarlo en cualquiera de los dos habría producido un contrato con postcondiciones contradictorias, que es el defecto que la partición de CU-04001 y CU-04010 ya corrigió una vez.
- **Fusiones.** Las cuatro operaciones del administrador sobre una cuenta quedaron en CU-04002, por el mismo criterio con el que `NB-00001` §5 las trata como un único conjunto de cobertura. El listado y el detalle quedaron juntos —CU-04006 para el alumno, CU-04007 para el administrador— porque comparten la comprobación que los gobierna y se distinguen sólo por la forma del resultado. **La eliminación quedó en un solo caso de uso, CU-04009, con sus dos alcances**, porque los dos responden la misma pregunta y el actor primario del contrato es uno solo: el código consumidor.
- **Particiones.** El envío se separó de la carga —CU-04005 frente a CU-04004— porque son el momento en que el texto entra y el momento en que se interpreta, con reglas distintas y con un puerto distinto de por medio; es la misma partición con la que el dominio separó su CU-04005 de su CU-04008. **Los dos caminos de alta se separaron —CU-04010 frente a CU-04001—**, y la emisión inicial de esta categoría los tenía fusionados. El fundamento de la partición es que no comparten casi nada: el estado inicial es opuesto —`Habilitado` contra `Pendiente`—, la credencial se aporta en uno y se prohíbe en el otro, la ventana de alta existe en uno y no en el otro, y uno se ejerce una sola vez en la vida de la instancia mientras el otro se ejerce una vez por alumno. Lo único que comparten es constituir una cuenta. `GeometriaFactory-Domain` llegó a la misma conclusión y partió su CU-04001 dando de alta su CU-02012; **mantenerlos fusionados acá obligaría a un solo caso de uso a orquestar dos casos de uso de dominio con postcondiciones contradictorias**, que es exactamente lo que produjo el defecto que la ronda r1 del audit levantó. El desenlace se separó de la revisión —CU-04008 frente a CU-04007— por sujetos y reglas distintos, siguiendo la partición que `01-Necesidades-Negocio` §3.2 justificó entre NB-00007 y NB-00009. La consulta del alumno se separó de la del administrador —CU-04006 frente a CU-04007— porque las comprobaciones que las acotan son opuestas: pertenencia contra facultad, y todo lo propio contra todo menos el borrador.
- **Lo que no se convirtió en caso de uso.** La autorización por pertenencia no recibió caso de uso propio aunque se repita en cuatro: es una comprobación transversal declarada en §4, y convertirla en contrato separado duplicaría lo que el dominio ya resuelve en sus CU-04009 y CU-04011. Tampoco lo recibieron la interpretación efectiva del texto, la derivación de la contraseña, **su generación cuando el reseteo la necesita**, la emisión del acceso ni el guardado: son de `GeometriaFactory-Infrastructure` y del consumidor, del mismo lado de la frontera que la derivación. **Los puertos siguen siendo cuatro**: la provisoria llega acá ya producida y ya derivada, exactamente como la contraseña que el alumno elige, de modo que no hace falta una frontera nueva para que el sistema la produzca en lugar del administrador.

## 9. Omisiones declaradas

| Artefacto | Estado | Motivo |
| --- | --- | --- |
| `Definicion-<Concepto-Central>.md` | **Omitido** | El concepto central de esta capa son los **puertos**, y los casos de uso ya los describen: cada uno declara cuáles consume y qué le pide a cada uno, y §3 los reúne en una sola tabla. Un documento aparte repetiría eso sin agregar semántica, y la regla lo declara recomendado y no obligatorio para `library` con superficie estrecha |
| `Reglas-De-Negocio/RN-XX-<Nombre>.md` | **Omitido** | **Las dieciséis reglas del producto viven en `GeometriaFactory-Domain`** y son atemporales: redactarlas de nuevo acá crearía dos enunciados de la misma regla en la misma cadena documental. Esta categoría las **referencia** por identificador y con enlace, y declara en §6 dónde se ejerce cada una |
| `Modelo-Datos/Modelo-Conceptual.md` y `Modelo-Datos/reglas-conceptuales-de-modelo/RC-XX-<Nombre>.md` | **Omitidos** | La regla de la categoría los omite para `library`, y el flag `tiene_persistencia` de este proyecto de código es false: el intake declara «no aplica directamente» en §17.2.P.4. El modelo del dominio vive en `Definicion-Modelo-De-Dominio.md` de `GeometriaFactory-Domain` |

## 10. Numeración y nombres de archivo

1. **Los identificadores `CU-XX` de esta carpeta son locales al proyecto de código.** El `CU-04009` de esta categoría no es el `CU-04009` de `GeometriaFactory-Domain` ni el que previó el catálogo de necesidades; la correspondencia se lee por la matriz de §7.1 y por la tabla de §7.4, nunca por número.
2. **La serie es contigua de CU-04001 a CU-04011**, sin huecos. **CU-04010 y CU-04011 se numeraron al final y no junto a los casos de uso con los que forman par temático** —CU-04001 y CU-04002 respectivamente—, con el que forma par temático, para no renumerar los ocho casos de uso intermedios que otras categorías ya citan por su identificador. Es la misma decisión con la que `GeometriaFactory-Domain` incorporó su CU-02012.
3. **El nombre de archivo de CU-04001 se conserva** —`CU-04001-Registrar-El-Alta-De-Una-Cuenta.md`— aunque su alcance se acotó al auto-registro, por estabilidad de citación: otras categorías ya lo citan por esa ruta. Es el mismo criterio con el que `GeometriaFactory-Domain` conservó dos nombres de regla.
4. **Las `RN-XX` que se citan conservan la numeración del intake y la de `GeometriaFactory-Domain`**, que son la misma. Dos de esos archivos llevan un slug que ya no describe del todo su enunciado y se citan igual por su ruta vigente, por la decisión de estabilidad de citación que ese proyecto de código declaró. **RN-04012 y RN-04013 se citan por enlace como las once anteriores**, porque su archivo aguas arriba ya existe.
5. **Las `US-XX` de §7.3 son una previsión local** de esta categoría, no la numeración de las veintisiete que previó `01-Necesidades-Negocio`.

## 11. Puntos abiertos

| Punto | Situación | Quién lo resuelve |
| --- | --- | --- |
| Identificador del puerto de repositorio de cuentas | El intake nombra tres puertos y no éste, que la orquestación de las cuentas y la verificación de unicidad del correo hacen necesario (§3). **No es una regla nueva ni una decisión de alcance**: es un nombre. Acá se lo nombra en lenguaje de dominio | `05-Arquitectura-Tecnica` y el punto de control de la etapa `a` |
| Nombres de tipos y de espacios de nombres | Declarados abiertos aguas arriba y validados en el punto de control de la etapa `a`. **No es ambigüedad de esta categoría**: acá los conceptos se nombran en lenguaje de dominio | `05-Arquitectura-Tecnica` |
| Criterio de comparación de dos correos | La unicidad del correo exige decidir si dos correos se comparan tal cual o normalizados. `GeometriaFactory-Domain` lo dejó abierto y esta categoría **no lo reabre**: lo cita en CU-04001 y lo deja donde está | `05-Arquitectura-Tecnica`, junto con la capa que ejerce la verificación |
| Sellos de alta, de modificación y de desenlace | El intake los sostiene como puertos verificables en prueba, pero **el modelo del dominio no los declara como atributos**: declara la fecha de alta del alumno y la «Fecha» que el alumno declara en su trabajo, y nada más. Esta capa los trata como metadatos de orquestación (§3) y la discrepancia está elevada al Product Owner por `GeometriaFactory-Domain` | Product Owner, y `GeometriaFactory-Domain` si decide incorporarlos a su modelo |
| Valores numéricos de los requerimientos no funcionales | El tiempo de 500 ms del criterio CA-06 de CU-04005 está rotulado como asunción aguas arriba y pendiente de confirmación del Product Owner. Se usa como valor vigente | Product Owner, y `08-Calidad-Y-Pruebas` al verificarlo |

## 12. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. Índice maestro de nueve casos de uso con la matriz NB → CU → RN → US, la verificación bidireccional de cobertura, la necesidad de negocio que este proyecto de código no toca con su justificación y las dos que cubre parcialmente, la tabla de los cuatro puertos que esta capa declara, la sección transversal de autorización por pertenencia y verificación de facultad, la tabla de las once reglas de negocio referenciadas con el lugar donde se ejerce cada una, la correspondencia con los casos de uso de dominio orquestados —once en ese momento, doce desde que el dominio emitió el de configuración del administrador: ver §7.4—, el criterio de recorte con sus fusiones y particiones, las tres omisiones con su motivo y los cuatro puntos abiertos. |
| 1.0 | 2026-08-09 | **Correcciones de la ronda r1 del audit**, absorbidas sin subir versión por `Master-Prompt.md` §5, con el documento en estado `Propuesto`. **H-01**: el catálogo pasa de nueve a **diez** casos de uso al partirse CU-04001 en los dos caminos de alta, con **CU-04010** nuevo para la configuración del administrador, espejando la partición del dominio entre su CU-04001 y su CU-02012; §5, §7.1, §7.2, §7.3 —con US-04028 nueva y US-04003 reasignada—, §7.4 y §8 se actualizan, y §8 declara el fundamento de la partición. **H-04**: el preámbulo de §6 deja de afirmar que dos reglas se ejercen enteras en otra capa —que ninguna fila declaraba— y pasa a describir la tabla: las once tienen tramo acá, y RN-04005 y RN-04009 tienen el principal en otra capa, marcado en sus filas. **H-05**: CU-04002 sale de la fila del reloj, que no consume. **H-06**: §3 declara que los sellos son **metadatos de orquestación** de esta capa, distintos de la «Fecha» del alumno, y §11 suma el punto abierto. **H-03**: la fila del puerto de validación suma la cantidad de figuras del conjunto raíz, con la precisión de por qué no es derivable de las piezas adoptadas. **H-13**: §4 declara que esta capa corta antes del dominio y colapsa en un motivo los dos que el dominio declara para la misma negativa de facultad. **H-07** y **H-14** se resuelven en las §9 y §6 de los casos de uso. |
| 1.0 | 2026-08-09 | **Corrección de la ronda r2 del audit, hallazgo H-15**, absorbida sin subir versión por `Master-Prompt.md` §5, con el documento en estado `Propuesto`. Las **dos** ocurrencias del conteo viejo de casos de uso del dominio quedan alineadas con §7.4: la cabecera pasa a declarar **doce**, y la fila de la emisión inicial precisa que eran once en ese momento y son doce desde que el dominio emitió el de configuración del administrador, de modo que el registro histórico se conserva sin contradecir al documento. |
| 1.1 | 2026-08-09 | **Propagación del `PRODUCT-INTAKE` 1.7**, capacidad **F-26** —reseteo de contraseña por el administrador—, con sus reglas **RN-04012** y **RN-04013**, el invariante **INV-09** de §17.1.P.2, el caso límite **CL-7** reescrito y la exclusión **X-2 retirada**. **§5**: el catálogo pasa de diez a **once** casos de uso, con **CU-04011** nuevo. **§4**: se suma la **cuarta comprobación transversal** —cambio de contraseña pendiente—, con su motivo y con la precisión 5, que declara que corta antes que las otras tres, que tiene una sola excepción y que es de esta capa y no del ruteo del front. La verificación de facultad suma CU-04011. **§3**: el puerto de reloj y el de repositorio de cuentas suman CU-04011, y este último suma la **marca** a lo que materializa. **§6**: pasa de once a **trece** reglas con tramo acá, con RN-04012 y RN-04013 referenciadas contra el intake y no por enlace, porque su archivo aguas arriba todavía no existe. **§7.1, §7.2, §7.3 y §7.4**: NB-00001 y NB-00002 suman CU-04011, la cobertura suma su fila, se agregan **US-04029 a US-04032**, y §7.4 declara que **CU-04003 del dominio queda orquestado desde dos casos de uso de esta capa**, con el motivo. **§8**: se declara la **partición del reseteo** frente a CU-04002 y frente a CU-04003, con sus fundamentos. **§10**: la serie contigua llega a CU-04011 y se declara cómo se citan RN-04012 y RN-04013. **§11**: dos puntos abiertos nuevos —el archivo de las dos reglas y la marca en el modelo del dominio—. Sube minor: agrega un caso de uso, una comprobación transversal y dos reglas referenciadas, sin invalidar ninguna decisión previa. |
| 1.2 | 2026-08-09 | **Reconciliación con el `PRODUCT-INTAKE` 1.8 y con lo que `GeometriaFactory-Domain` ya emitió.** **(a) Dos puntos abiertos de §11 quedan cerrados y se retiran de la tabla**: RN-04012 y RN-04013 **ya tienen archivo** en `GeometriaFactory-Domain` —`Reglas-De-Negocio/RN-04012-...` y `RN-04013-...`—, de modo que §6 las enlaza como a las once anteriores y §10 punto 4 deja de decir que se citan contra el intake; y la **marca de cambio de contraseña pendiente ya está en el modelo del dominio**, declarada como atributo de la cuenta en `Definicion-Modelo-De-Dominio.md` §2.1, con su máquina propia en §5.3, de modo que deja de correr la suerte de los sellos. **(b)** La cuarta comprobación transversal de §4 y la fila de RN-04013 en §6 se corrigen a la precisión que el intake 1.8 §4.1 introduce: la cuenta reseteada **se autentica y no obtiene sesión de trabajo**, y la consulta de admisibilidad de CU-04003 la devuelve **no admisible**, que es lo que ese caso de uso corrige en su versión 1.2 y lo que `GeometriaFactory-Domain` CU-04004 FA-03 ya declaraba. La cabecera cita el intake **1.8**. Sube minor: cierra dos puntos abiertos y precisa una comprobación transversal, sin agregar ni quitar casos de uso. |
| 1.3 | 2026-08-09 | **Absorbe dos decisiones del Product Owner sobre F-26**, que **CU-04011** 1.2 aplica. **Decisión A: resetear no exige que la cuenta esté habilitada** —es una operación sobre la credencial y no toca el estado de la cuenta, de modo que el administrador resetea y habilita en el orden que quiera—. **Decisión B: la contraseña provisoria la produce el sistema y no la escribe el administrador**, porque una provisoria escrita por el docente termina siendo la misma clave para toda la comisión. Cambios acá: **§5** actualiza la decisión de contrato de CU-04011 —provisoria producida por el sistema y estado de cuenta conservado **cualquiera sea**—; **§7.4** corrige una afirmación que la decisión A volvió insostenible, la de que **CU-04003 del dominio queda orquestado desde dos casos de uso de esta capa**: el reemplazo de CU-04003 exige estado `Habilitado`, de modo que CU-04011 pasa a orquestar **CU-02013**, la operación de reseteo del dominio, que no lo exige; **§8** declara que la generación de la provisoria es de infraestructura y del consumidor, del mismo lado de la frontera que la derivación, y que **los puertos siguen siendo cuatro**. **§3 no cambia**: no se agrega ningún puerto. El catálogo sigue con **once** casos de uso y las comprobaciones transversales, con **cuatro**. **Autor:** Analista Funcional + API Designer (AG-02) |
| 1.4 | 2026-08-09 | **Absorbe el `PRODUCT-INTAKE` 1.10**, que lleva las reglas del producto de trece a quince, y cierra dos hallazgos del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0.** **(a) RN-04014 y RN-04015.** El intake 1.10 §4.1 incorpora **RN-04014** —la contraseña provisoria la produce el sistema, no es adivinable y no se repite— y **RN-04015** —resetear no exige cuenta habilitada—, que son las dos decisiones del Product Owner que esta capa venía aplicando sin fuente. **§6** suma las dos filas con **dónde se ejerce cada una** y declara la precisión que las separa: **RN-04014 es la única de las quince sin tramo en esta capa** —`CU-04011` §10 la **exige por escrito**, pero no la ejerce, porque el valor llega ya producido y derivado—, mientras que **RN-04015 sí tiene tramo, y es negativo**: consiste en **no** comprobar el estado de la cuenta, que es lo que `CU-04011` CA-06 y CA-07 verifican. El recuento de reglas pasa de trece a **quince** en §6 y en §9, y la cabecera cita el intake 1.10. Se corrige de paso «los **doce** casos de uso de `GeometriaFactory-Domain`», que son **trece** desde CU-02013 (hallazgo **`F26-20`**). **(b) `F26-27`**: la fila 1.3 de este control de cambios tenía **cuatro celdas en una tabla de tres columnas**; el texto se conserva íntegro y el autor pasa a leerse dentro de la celda de cambios. **(c) Constancia por `F26-25`**: el informe registra que cuatro pasajes de contenido de este documento —«cuatro operaciones»→«cinco», «veintisiete US»→«treinta», «once reglas»→«trece»— se cambiaron **sin fila propia** y que las filas 1.2 y 1.3 describen otros cambios. Se deja escrito acá porque no corresponde reescribir filas históricas: esos tres cambios son reales, están vigentes en §5, §7.3 y §6, y esta fila es su registro. **Ningún caso de uso, puerto, comprobación transversal ni criterio de aceptación cambia.** Sube minor. |
| 1.5 | 2026-08-10 | **Absorbe el `PRODUCT-INTAKE` 1.13**, regla **RN-04016** y precisión de **F-04**: habilitar una cuenta produce y fija su contraseña provisoria y la deja con cambio de contraseña pendiente, con lo que **no queda ninguna escritura anónima de credencial en el producto**. **§3** actualiza las líneas de **CU-04002** —que pasa a producir la provisoria y a consumir tres puertos nuevos— y de **CU-04003**, cuya fijación deja de tener solicitante anónimo. **§5.3** reescribe **US-04008**. **Los once casos de uso no cambian de número ni de recorte**, y las condiciones distintas del proyecto de código siguen siendo **36**: entra `HABILITACION_SIN_CREDENCIAL_PROVISORIA` en CU-04002 y sale `CREDENCIAL_NO_ESTABLECIDA` de CU-04003 y CU-04011. Sube minor. |
| 1.6 | 2026-08-10 | **Cierra los hallazgos `C-02` (P0) y `C-08` (P2) del informe de auditoría `SDD/Docs/Audit/Coherencia-Corpus-r1.md` 1.0 en este archivo, contra `PRODUCT-INTAKE` 1.14, y levanta un defecto de la misma familia que el informe no registra.** **`C-02`**: la **cabecera de trazabilidad** y las dos notas de encabezado de **§6** decían **quince** reglas; son **dieciséis**, `RN-04001` a `RN-04016`, contadas sobre los archivos de `GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/`. **Defecto no registrado por el informe**: la tabla de §6 **no tenía fila para `RN-04016`** —quince filas, `RN-04001` a `RN-04015`—, de modo que la regla decimosexta no tenía declarado dónde se ejerce en esta capa pese a que `CU-04002` la materializa desde su emisión y `CU-04003` la trata en FA-02, en FA-05 y en su motivo `CAMBIO_DE_CONTRASENA_PENDIENTE`. **Entra la fila de `RN-04016`**, con su tramo en `CU-04002` —habilitar y rehabilitar piden la provisoria, la derivan y la fijan— y el contraste de `CU-04003`, y el recuento de la nota pasa a **quince de las dieciséis con tramo acá**, con RN-04014 como única excepción. La enumeración de las entradas tardías suma **RN-04016**, que entró con el intake 1.13. **`C-08`**: la cabecera declaraba el intake **1.10**, archivado, y pasa a **1.14**. **Cierra además el hallazgo `C-01` (P0) en una ocurrencia de esta capa que el informe no registra**: el quinto punto de **§4** declaraba que «la marca la pone **únicamente** el reseteo de CU-04011», cuando `CU-04002` la pone al habilitar y al rehabilitar (RN-04016) y `CU-04003` lo declara en FA-02, en FA-05 y en su motivo `CAMBIO_DE_CONTRASENA_PENDIENTE`. Pasa a declarar los **dos** orígenes. **Ningún caso de uso, ningún puerto, ningún motivo y ninguna fila de la matriz NB → CU → RN → US cambia**: lo que entra es una fila de correspondencia que faltaba y una frase que se alinea con la decisión que esta capa ya ejercía. Sube minor. |
| 1.7 | 2026-08-10 | **Absorbe la corrección de `PRODUCT-INTAKE` 1.15 §4.1 (RN-04016)**, que declara falsa la afirmación de 1.13 según la cual la regla deja al producto sin ninguna escritura anónima: el **registro de cuenta** de `CU-04001` es anónimo por diseño y debe seguir siéndolo, porque es como el alumno entra al laboratorio. Lo que la regla elimina es la escritura anónima **de credencial**. Único cambio acá: **la fila 1.5 de este control de cambios** transcribía la afirmación ancha y pasa a decir «de credencial en el producto». **Ningún caso de uso, puerto, condición ni recuento cambia**, y las 36 condiciones distintas siguen siendo 36. Sube minor. |
| 1.8 | 2026-08-13 | **Tramo `R-2` del plan de renombre de [`Norma-De-Nomenclatura.md`](../../../../../Producto/Norma-De-Nomenclatura.md) 1.4 §8, ejecutado contra el glosario de su §6 y no por criterio propio.** **Acto 1 · el renombre** de los **tres puertos declarados** de su §6.3 —`IRepositorioTrabajos` ⟶ `IWorkRepository`, `IValidadorFiguras` ⟶ `IFigureValidator` e `IRelojDelSistema` ⟶ `ISystemClock`—. Acá son **3 ocurrencias**, las de §3, que son **reporte de la fuente** (norma §4.1): esta categoría no declara los identificadores, reporta cómo los nombra el intake, y el intake se renombró en este mismo tramo. **No cambia ningún contenido**: los puertos, su alcance y el punto abierto del cuarto siguen exactamente igual. **Cuadre `V-4` en las dos direcciones, contra la lista escrita antes de editar:** 64 ocurrencias candidatas medidas en 13 documentos con el instrumento de la norma §2.1, **63 renombradas y 1 no renombrada** —la cita textual de la línea de trazabilidad upstream de `RC-04001-Texto-Original-Escrito-Una-Sola-Vez.md`, que atribuye al `PRODUCT-INTAKE` **1.12** las palabras «`JsonOriginal` conservado íntegro y nunca reescrito» y que **renombrar falsificaría**—. `V-6` cuadró los tres nombres de archivo de `Ports/`. **Esta fila queda fuera del cuadre**, por el punto 4 de `V-4`: al describir lo que hizo reintroduce los identificadores viejos. |
