# Especificación funcional — GeometriaFactory-Application

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** Especificacion-Funcional.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** `00-Contexto/Vision-Producto.md` §1, §3 y §9 (glosario raíz de la cadena); `00-Contexto/Alcance-Producto.md` §4.1 y §5; `01-Necesidades-Negocio/Necesidades-Negocio.md` §2, §4 y §5.3, y las necesidades NB-01 a NB-07 y NB-09; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §17.2 íntegro —en particular §17.2.P.2 (inversión de dependencias), §17.2.P.5 (verificación de pertenencia), §17.2.P.10 y §17.2.P.11—, §4, §4.1 (las once reglas), §4.2 (modelo de estados del trabajo), §6, §7, §12 y §14; `Proyectos/GeometriaFactory-Domain/02-Especificacion-Funcional/` completo, cuyos **doce** casos de uso esta categoría orquesta
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

Esta especificación tiene la forma de la variante `library` de la categoría: **cada caso de uso describe un contrato de uso de la superficie pública**, no un flujo de pantallas. El actor primario de los diez casos de uso es el código que consume la biblioteca; el alumno y el administrador aparecen como sujetos de las reglas, nunca como actores.

Dos rasgos distinguen a esta capa de la de dominio, y los dos recorren todos sus casos de uso:

1. **La dependencia se invierte.** Esta capa declara qué necesita —guardar y recuperar, interpretar el texto del alumno, saber qué hora es— y otra capa lo provee. Es lo que permite ejercer un caso de uso entero con dobles, sin base de datos ni frontera de proceso. Un caso de uso de esta categoría que mencionara el motor de persistencia, el mecanismo de acceso o el protocolo de transporte estaría mal ubicado.
2. **Acá se decide quién puede hacer qué.** El dominio declara las condiciones; esta capa las ejerce sobre el pedido concreto, antes de tocar el repositorio. Es autorización, no autenticación: no se comparan contraseñas ni se emiten accesos.

Lo que **no** está acá, y dónde está: las entidades, los invariantes y las máquinas de estado, en `GeometriaFactory-Domain`; la interpretación efectiva del texto, la derivación de la contraseña, la emisión del acceso y el guardado, en `GeometriaFactory-Infrastructure`; los datos que cruzan la frontera del proceso, en `GeometriaFactory-Contracts`; las páginas y el dibujo, en `GeometriaFactory-Web` y `GeometriaFactory-Visor`.

## 2. Documentos de esta categoría

| Documento | Propósito |
| --- | --- |
| `Especificacion-Funcional.md` | Este archivo: índice maestro, catálogos y matriz de trazabilidad |
| [`Glosario-Funcional.md`](Glosario-Funcional.md) | Vocabulario que esta categoría acuña, con los términos de más de un referente |
| `Casos-De-Uso/CU-XX-<Nombre>.md` | Diez casos de uso, uno por archivo |
| [`README.md`](README.md) | Índice navegable de la sección, con el orden de lectura y las omisiones y su motivo |

## 3. Los puertos que esta capa declara

Los puertos son la frontera de este proyecto de código: lo que declara acá lo implementa `GeometriaFactory-Infrastructure`, y la composición de raíz los provee. `PRODUCT-INTAKE` §17.2.P.1 y §14 los nombran una vez, y esa es la única cita de identificadores de código de esta categoría: `IRepositorioTrabajos`, `IValidadorFiguras` e `IRelojDelSistema`. En el resto de los artefactos los puertos se nombran en lenguaje de dominio, porque los nombres definitivos de tipos se validan en el punto de control de la etapa `a`.

| Puerto | Qué le pide esta capa | Casos de uso que lo consumen |
| --- | --- | --- |
| Repositorio de trabajos | Recuperar un trabajo, resolver una consulta ya acotada por dueño o por alcance, materializar el resultado y ejecutar el retiro | CU-02, CU-04, CU-05, CU-06, CU-07, CU-08, CU-09 |
| Validación de figuras | Interpretar el texto original y devolver **la cantidad de figuras del conjunto raíz**, las piezas reconstruidas y las observaciones, con su especie y su ubicación | CU-05 |
| Reloj del sistema | Los sellos de alta, de modificación y de desenlace, **para que sean verificables en prueba** | CU-01, CU-03, CU-04, CU-05, CU-08, CU-10 |
| Repositorio de cuentas | Recuperar una cuenta por su correo, responder si un correo ya está registrado y si ya existe una cuenta con papel `Administrador`, y materializar el resultado | CU-01, CU-02, CU-03, CU-07, CU-10 |

**El repositorio de cuentas no lleva identificador declarado en el intake**, que nombra los otros tres. No es una invención de esta categoría: `GeometriaFactory-Domain` §1 de su índice asigna explícitamente a esta capa la verificación de la unicidad del correo «sobre el conjunto de alumnos», y ninguna verificación sobre un conjunto es posible sin una frontera que lo alcance. Queda declarado como punto abierto en §11.

**Dos precisiones sobre lo que viaja por los puertos:**

- **Los sellos de alta, de modificación y de desenlace son metadatos de orquestación de esta capa.** No son la «Fecha» que el alumno declara en su trabajo, que sí modela el dominio como dato del alumno. El modelo del dominio declara la fecha de alta del alumno —que recibe del consumidor, sin leer el reloj— y **no declara** fecha de última modificación de la cuenta ni fecha de creación, de modificación o de desenlace del trabajo. La discrepancia está elevada al Product Owner: hasta que resuelva, estos sellos se leen como dato de esta capa y no como atributos del dominio.
- **La cantidad de figuras del conjunto raíz la produce el validador** al interpretar el texto, incluidas las figuras que no pudo reconstruir, y **no es derivable de las piezas adoptadas**, que admiten huecos. El dominio la exige como precondición de la reconstrucción y su registro de observaciones la hereda como rango de posiciones válidas, de modo que CU-05 —único orquestador de los dos— es quien la hace viajar.

**El alcance de la unidad de trabajo es un caso de uso, una transacción**: cada caso de uso abre a lo sumo una y no la reparte entre varias operaciones.

## 4. Autorización por pertenencia y verificación de facultad

Es lo que hace que el flag `tiene_auth` valga true en este proyecto de código, y es transversal a los diez casos de uso. No es autenticación: acá no se comparan contraseñas ni se emiten accesos, y quién es la persona llega ya resuelto desde afuera.

| Comprobación | Qué verifica | Respuesta cuando falla | Dónde se ejerce |
| --- | --- | --- | --- |
| **Pertenencia** | Que el trabajo pedido sea del alumno solicitante | `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`, que el consumidor traduce a «no encontrado» y **nunca** a «no autorizado» | CU-04, CU-05, CU-06, CU-09 |
| **Facultad** | Que quien pide una operación reservada tenga el papel `Administrador` | `FACULTAD_DE_ADMINISTRADOR_REQUERIDA`, que sí admite ser explícito: no hay recurso ajeno cuya existencia proteger | CU-02, CU-07, CU-08 |
| **Alcance del administrador** | Que el trabajo no esté en `Borrador`, porque los borradores no forman parte de su flujo de trabajo | `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR` | CU-07, CU-08, CU-09 |

**Una sola negativa de facultad, y dos motivos del dominio detrás.** El dominio declara dos códigos para la misma negativa —uno en su resolución de desenlace y otro en la de alcance del administrador—, y esta capa emite uno solo: corta con su propia verificación **antes** de invocar al dominio, de modo que ninguno de los dos llega a producirse. Quien lea las dos capas no debe leer tres negativas de facultad donde hay una.

Cuatro precisiones que rigen en toda la categoría:

1. **El papel no reemplaza a la pertenencia.** Son dos comprobaciones distintas: un alumno autenticado no debe poder leer el trabajo de otro cambiando el identificador de la petición, y ningún papel resuelve eso.
2. **La negativa por pertenencia y la negativa por facultad no se confunden.** La primera oculta la existencia del recurso; la segunda no tiene nada que ocultar.
3. **La comprobación se hace sobre el dato recuperado y antes de escribir.** No se resuelve ocultando un control en la pantalla, y por eso es verificable con dobles sin base de datos.
4. **El trabajo ajeno y el identificador inexistente comparten motivo por diseño.** Distinguirlos permitiría averiguar por tanteo qué identificadores existen.

## 5. Catálogo de casos de uso

| CU | Nombre | Contrato que describe | Estado |
| --- | --- | --- | --- |
| CU-01 | [Registrar el alta de una cuenta](../../../../Casos-De-Uso/CU-04001-Registrar-El-Alta-De-Una-Cuenta.md) | Auto-registro del alumno: correo libre, cuenta constituida en estado `Pendiente` y sin credencial | Propuesto |
| CU-02 | [Gobernar las cuentas de la comisión](../../../../Casos-De-Uso/CU-04002-Gobernar-Las-Cuentas-De-La-Comision.md) | Habilitar, bloquear, rehabilitar y dar de baja, con confirmación escrita y arrastre de los trabajos | Propuesto |
| CU-03 | [Resolver el ingreso y la credencial del alumno](../../../../Casos-De-Uso/CU-04003-Resolver-El-Ingreso-Y-La-Credencial-Del-Alumno.md) | Admisibilidad de la cuenta con su motivo, y fijación y reemplazo de la credencial derivada | Propuesto |
| CU-04 | [Cargar y reeditar un trabajo propio](../../../../Casos-De-Uso/CU-04004-Cargar-Y-Reeditar-Un-Trabajo-Propio.md) | Constituir el trabajo con dueño y texto original íntegro, y reeditarlo sólo en `Borrador` | Propuesto |
| CU-05 | [Enviar un trabajo e interpretar su texto](../../../../Casos-De-Uso/CU-04005-Enviar-Un-Trabajo-E-Interpretar-Su-Texto.md) | La única acción de guardado: interpretar por el puerto, incorporar piezas y observaciones y dejar que el dominio resuelva el estado | Propuesto |
| CU-06 | [Consultar los trabajos propios del alumno](../../../../Casos-De-Uso/CU-04006-Consultar-Los-Trabajos-Propios-Del-Alumno.md) | Listado acotado al dueño y sin componentes, y detalle con desenlace y comentario | Propuesto |
| CU-07 | [Revisar los trabajos de la comisión](../../../../Casos-De-Uso/CU-04007-Revisar-Los-Trabajos-De-La-Comision.md) | Listado de la comisión sin borradores, con dueño para agrupar y filtrar, y detalle equivalente al del alumno | Propuesto |
| CU-08 | [Dar desenlace a un trabajo](../../../../Casos-De-Uso/CU-04008-Dar-Desenlace-A-Un-Trabajo.md) | Aprobar o rechazar desde estado `Pendiente`, con comentario opcional y terminalidad | Propuesto |
| CU-09 | [Eliminar un trabajo](../../../../Casos-De-Uso/CU-04009-Eliminar-Un-Trabajo.md) | Retiro con los dos alcances opuestos: el alumno sólo en `Borrador`, el administrador en todo lo que ve | Propuesto |
| CU-10 | [Configurar la cuenta de administrador](../../../../Casos-De-Uso/CU-04010-Configurar-La-Cuenta-De-Administrador.md) | El segundo camino de alta: cuenta única con papel `Administrador`, `Habilitado` y con credencial, sólo mientras no exista ninguna | Propuesto |

Diez casos de uso, sobre un mínimo de cinco para el tipo `library`.

## 6. Reglas de negocio que esta capa hace cumplir

**Las once reglas del producto viven en `GeometriaFactory-Domain` y acá se referencian, no se redactan.** Lo que esta tabla declara es dónde se ejerce cada una en esta capa, que es una cosa distinta de dónde está enunciada. **Las once tienen tramo acá**, y en dos el tramo principal está en otra capa: RN-05, que resuelve el dominio sobre el conjunto de observaciones que esta capa le entrega, y RN-09, cuyo mensaje ubicado lo produce el validador detrás del puerto. Las dos filas lo declaran.

| RN | Enunciado en una línea | Dónde se ejerce en esta capa |
| --- | --- | --- |
| [RN-01](../../../../Reglas-De-Negocio/RN-02001-Administrador-Unico-Y-Papeles-Fijos.md) | Administrador único y papeles fijos | CU-10 (ventana de alta y su negativa), CU-01 (rechazo del papel `Administrador` por el auto-registro), CU-02, CU-03, CU-07, CU-08 (verificación de facultad) |
| [RN-02](../../../../Reglas-De-Negocio/RN-02002-Correo-Del-Alumno-Unico.md) | El correo del alumno es único | CU-01 y CU-10: la verificación sobre el conjunto de cuentas es de esta capa, en los dos caminos de alta |
| [RN-03](../../../../Reglas-De-Negocio/RN-02003-Trabajo-Ajeno-Indistinguible-De-Inexistente.md) | Un alumno sólo ve y opera sus propios trabajos | CU-04, CU-05, CU-06, CU-09: la verificación de pertenencia |
| [RN-04](../../../../Reglas-De-Negocio/RN-02004-Eliminacion-Acotada-Al-Borrador.md) | El alumno elimina sólo en borrador; el administrador, cualquier trabajo que ve | CU-09 en sus dos alcances, y CU-02 en el arrastre de la baja |
| [RN-05](../../../../Reglas-De-Negocio/RN-02005-Finalizacion-Sin-Errores-De-Validacion.md) | Un trabajo no pasa a estado `Pendiente` con errores de validación | CU-05, **con el tramo principal en el dominio**: esta capa entrega el conjunto de observaciones y el dominio resuelve el estado |
| [RN-06](../../../../Reglas-De-Negocio/RN-02006-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md) | Una cuenta `Pendiente` o `Bloqueado` no obtiene acceso | CU-03: la consulta de admisibilidad con su motivo. CU-01 y CU-10 en cuanto fijan estados iniciales opuestos, que es lo que decide si la cuenta admite acceso desde el alta |
| [RN-07](../../../../Reglas-De-Negocio/RN-02007-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md) | La baja arrastra los trabajos y exige confirmación escrita | CU-02: la comparación del correo escrito y el retiro de todos los trabajos en la misma unidad de trabajo |
| [RN-08](../../../../Reglas-De-Negocio/RN-02008-Texto-Original-Conservado-Integro.md) | El texto original del alumno se conserva íntegro | CU-04 y CU-05: el texto se entrega tal cual y no se reescribe ni cuando la interpretación falla |
| [RN-09](../../../../Reglas-De-Negocio/RN-02009-Observacion-De-Error-Con-Posicion-Y-Campo.md) | Toda observación de error indica la posición de la pieza y el campo | CU-05, **con el tramo principal en el validador**, que produce el mensaje ubicado detrás del puerto. Lo que esta capa aporta es la cantidad de figuras del conjunto raíz, que es el rango contra el que la posición se valida, y el rechazo del conjunto mal formado, que no llega al alumno |
| [RN-10](../../../../Reglas-De-Negocio/RN-02010-Desenlace-Exclusivo-Del-Administrador-Y-Terminalidad.md) | El desenlace es exclusivo del administrador y es terminal | CU-08: la verificación de facultad y la propagación de la terminalidad |
| [RN-11](../../../../Reglas-De-Negocio/RN-02011-El-Administrador-No-Ve-Los-Borradores.md) | El administrador no ve los trabajos en borrador | CU-07, CU-08 y CU-09: el predicado de alcance trasladado a la consulta |

## 7. Matriz NB → CU → RN → US

### 7.1 Matriz

| NB | CU de este proyecto de código | RN aplicables | US previstas en 06 |
| --- | --- | --- | --- |
| [NB-01](../../../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00001-Control-De-Admision-Al-Laboratorio.md) · Control de admisión y de bajas del laboratorio | CU-10, CU-01, CU-02 | RN-01, RN-02, RN-06, RN-07 | US-03, US-28, US-01, US-04, US-05, US-06 |
| [NB-02](../../../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00002-Identidad-Propia-Del-Alumno-Sin-Correo.md) · Identidad propia del alumno sin canal de correo | CU-01, CU-03 | RN-02, RN-06 | US-01, US-02, US-07, US-08, US-09 |
| [NB-03](../../../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00003-Trabajo-Con-Dueno-Estado-Y-Persistencia.md) · Trabajo con dueño, estado y persistencia | CU-04, CU-05, CU-06, CU-09 | RN-03, RN-04, RN-05, RN-08 | US-10, US-11, US-12, US-15, US-17, US-18, US-26 |
| [NB-04](../../../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00004-Interpretacion-Fiel-Del-Dato-Del-Alumno.md) · Interpretación fiel del dato del alumno | CU-04, CU-05 | RN-05, RN-08, RN-09 | US-11, US-13, US-14, US-15, US-16 |
| [NB-05](../../../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00005-Visibilidad-Del-Error-De-Calculo.md) · Visibilidad del error de cálculo | CU-05 | RN-05, RN-09 | US-13, US-15 |
| [NB-06](../../../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00006-Visualizacion-Dentro-Del-Producto.md) · Visualización del trabajo dentro del producto | CU-06 (parcial: entrega de piezas con identidad posicional) | RN-03 | US-19 |
| [NB-07](../../../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00007-Revision-De-La-Comision-En-Un-Solo-Lugar.md) · Revisión de la comisión desde un solo lugar | CU-07 | RN-01, RN-11 | US-20, US-21, US-22 |
| [NB-08](../../../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00008-Alcance-Del-Laboratorio-Desde-El-Aula.md) · Alcance del laboratorio desde el aula | — | — | — |
| [NB-09](../../../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00009-Desenlace-Explicito-De-La-Entrega.md) · Desenlace explícito de la entrega | CU-08, CU-09, CU-06 (parcial), CU-07 (parcial) | RN-04, RN-10, RN-11 | US-18, US-22, US-23, US-24, US-25, US-27 |

### 7.2 Cobertura bidireccional

**De CU a NB.** Los diez casos de uso trazan al menos a una necesidad de negocio; no hay ninguno huérfano.

| CU | NB que implementa |
| --- | --- |
| CU-01 | NB-02, NB-01 |
| CU-10 | NB-01 |
| CU-02 | NB-01 |
| CU-03 | NB-02 |
| CU-04 | NB-03, NB-04 |
| CU-05 | NB-04, NB-05, NB-03 |
| CU-06 | NB-03, NB-09, NB-06 |
| CU-07 | NB-07, NB-09 |
| CU-08 | NB-09 |
| CU-09 | NB-03, NB-09 |

**De NB a CU.** Ocho de las nueve necesidades reciben al menos un caso de uso en este proyecto de código. La restante **no la toca este proyecto de código**, y esto es una alerta explícita y no un silencio:

| NB sin CU acá | Por qué | Dónde se cubre |
| --- | --- | --- |
| NB-08 · Alcance del laboratorio desde el aula | Su dolor no es funcional sino de acceso: viabilidad medida, despliegue y estado degradado explícito. Esta capa no atiende peticiones, no abre conexiones y no conoce la frontera de proceso | 02 de `GeometriaFactory-Web` y `GeometriaFactory-Api`; `09-Devops` |

Dos necesidades quedan cubiertas **parcialmente**, y conviene que se lea así:

- **NB-06.** Lo que esta capa aporta es la entrega de las piezas con su identidad posicional y sus componentes en el detalle, que es el dato con el que después se dibuja y se arma el árbol. El dibujo, el árbol y la sincronización son de `GeometriaFactory-Visor` y de `GeometriaFactory-Web`.
- **NB-07.** Lo que esta capa aporta es el listado con el predicado de alcance ya aplicado y el dato de dueño. La agrupación, el orden y el filtro tal como la persona los ejerce son decisiones de presentación de `GeometriaFactory-Web`, y el panel de resumen es una capacidad de prioridad menor con plazo posterior.

### 7.3 Historias de usuario previstas

La numeración es una **previsión** de esta categoría, y la confirma la categoría 06 al redactarlas.

| US prevista | Contenido | CU de origen |
| --- | --- | --- |
| US-01 | Constituir una cuenta de alumno en estado `Pendiente`, sin credencial | CU-01 |
| US-02 | Rechazar el alta con un correo ya registrado | CU-01 |
| US-03 | Configurar la cuenta del administrador sólo mientras no exista ninguna, habilitada y con credencial | CU-10 |
| US-04 | Habilitar, bloquear y rehabilitar una cuenta con verificación de facultad | CU-02 |
| US-05 | Dar de baja una cuenta exigiendo el correo escrito como confirmación | CU-02 |
| US-06 | Arrastrar en la baja todos los trabajos de la cuenta, en cualquier estado | CU-02 |
| US-07 | Devolver el motivo de una cuenta que no admite ingreso | CU-03 |
| US-08 | Fijar la credencial derivada en el primer ingreso efectivo | CU-03 |
| US-09 | Reemplazar la credencial derivada exigiendo la verificación de la vigente | CU-03 |
| US-10 | Cargar un trabajo con dueño, identificador propio y fecha tomada del reloj | CU-04 |
| US-11 | Conservar el texto original íntegro al cargar y al reeditar | CU-04 |
| US-12 | Reeditar sólo un trabajo propio en `Borrador`, descartando la interpretación anterior | CU-04 |
| US-13 | Enviar un trabajo con advertencias y que pase a estado `Pendiente` | CU-05 |
| US-14 | Enviar un trabajo con errores de validación y que quede en `Borrador` con su ubicación | CU-05 |
| US-15 | Interpretar el texto por el puerto de validación, sin tocar la base de datos | CU-05 |
| US-16 | Terminar de forma controlada cuando la interpretación no está disponible | CU-05 |
| US-17 | Listar los trabajos propios con los cuatro estados distinguibles | CU-06 |
| US-18 | Ver el desenlace y el comentario del trabajo propio | CU-06 |
| US-19 | Devolver el detalle con piezas y componentes, y el listado sin componentes | CU-06 |
| US-20 | Listar los trabajos de la comisión excluyendo los borradores | CU-07 |
| US-21 | Filtrar el listado de la comisión por alumno, con el recorte vigente | CU-07 |
| US-22 | Abrir el detalle de un trabajo de la comisión con los mismos elementos que ve el alumno | CU-07 |
| US-23 | Aprobar un trabajo en estado `Pendiente`, con comentario opcional | CU-08 |
| US-24 | Rechazar un trabajo en estado `Pendiente`, con comentario opcional | CU-08 |
| US-25 | Rechazar toda transición pedida por quien no tiene la facultad o desde un estado terminal | CU-08 |
| US-26 | Eliminar un trabajo propio sólo en `Borrador` | CU-09 |
| US-27 | Eliminar por el administrador en los tres estados que ve | CU-09 |
| US-28 | Rechazar la configuración de un segundo administrador | CU-10 |

### 7.4 Casos de uso de dominio orquestados

Los **doce** casos de uso de `GeometriaFactory-Domain` quedan orquestados por los diez de esta capa. Ninguno queda sin orquestar.

| CU de esta capa | CU de dominio que orquesta |
| --- | --- |
| CU-01 | [CU-01](../../../../Casos-De-Uso/CU-02001-Registrar-El-Alta-De-Un-Alumno.md) |
| CU-10 | [CU-12](../../../../Casos-De-Uso/CU-04010-Configurar-La-Cuenta-De-Administrador.md) |
| CU-02 | [CU-02](../../../../Casos-De-Uso/CU-02002-Gobernar-El-Ciclo-De-Vida-De-La-Cuenta.md) |
| CU-03 | [CU-04](../../../../Casos-De-Uso/CU-02004-Evaluar-La-Admisibilidad-De-La-Cuenta.md), [CU-03](../../../../Casos-De-Uso/CU-02003-Fijar-Y-Reemplazar-La-Credencial-Derivada.md) |
| CU-04 | [CU-05](../../../../Casos-De-Uso/CU-02005-Crear-Y-Reeditar-Un-Trabajo.md), [CU-09](../../../../Casos-De-Uso/CU-02009-Resolver-El-Acceso-Del-Alumno-A-Un-Trabajo.md) |
| CU-05 | [CU-06](../../../../Casos-De-Uso/CU-02006-Reconstruir-El-Conjunto-De-Piezas-Del-Trabajo.md), [CU-07](../../../../Casos-De-Uso/CU-02007-Registrar-Las-Observaciones-Del-Trabajo.md), [CU-08](../../../../Casos-De-Uso/CU-02008-Gobernar-El-Estado-Del-Trabajo.md), CU-09 |
| CU-06 | CU-09 |
| CU-07 | [CU-11](../../../../Casos-De-Uso/CU-02011-Resolver-El-Alcance-Del-Administrador-Sobre-Un-Trabajo.md) |
| CU-08 | [CU-10](../../../../Casos-De-Uso/CU-02010-Resolver-El-Desenlace-Del-Trabajo.md), CU-11 |
| CU-09 | CU-09, CU-11 |

## 8. Criterio de recorte aplicado

- **Piso y techo.** El mínimo para `library` es de cinco casos de uso; el techo lo da la cobertura de las necesidades de negocio que este proyecto de código toca. Quedaron **diez**, en el borde de la guía orientativa de «library con menos de diez». El décimo es CU-10, y su causa está declarada abajo.
- **Fusiones.** Las cuatro operaciones del administrador sobre una cuenta quedaron en CU-02, por el mismo criterio con el que `NB-01` §5 las trata como un único conjunto de cobertura. El listado y el detalle quedaron juntos —CU-06 para el alumno, CU-07 para el administrador— porque comparten la comprobación que los gobierna y se distinguen sólo por la forma del resultado. **La eliminación quedó en un solo caso de uso, CU-09, con sus dos alcances**, porque los dos responden la misma pregunta y el actor primario del contrato es uno solo: el código consumidor.
- **Particiones.** El envío se separó de la carga —CU-05 frente a CU-04— porque son el momento en que el texto entra y el momento en que se interpreta, con reglas distintas y con un puerto distinto de por medio; es la misma partición con la que el dominio separó su CU-05 de su CU-08. **Los dos caminos de alta se separaron —CU-10 frente a CU-01—**, y la emisión inicial de esta categoría los tenía fusionados. El fundamento de la partición es que no comparten casi nada: el estado inicial es opuesto —`Habilitado` contra `Pendiente`—, la credencial se aporta en uno y se prohíbe en el otro, la ventana de alta existe en uno y no en el otro, y uno se ejerce una sola vez en la vida de la instancia mientras el otro se ejerce una vez por alumno. Lo único que comparten es constituir una cuenta. `GeometriaFactory-Domain` llegó a la misma conclusión y partió su CU-01 dando de alta su CU-12; **mantenerlos fusionados acá obligaría a un solo caso de uso a orquestar dos casos de uso de dominio con postcondiciones contradictorias**, que es exactamente lo que produjo el defecto que la ronda r1 del audit levantó. El desenlace se separó de la revisión —CU-08 frente a CU-07— por sujetos y reglas distintos, siguiendo la partición que `01-Necesidades-Negocio` §3.2 justificó entre NB-07 y NB-09. La consulta del alumno se separó de la del administrador —CU-06 frente a CU-07— porque las comprobaciones que las acotan son opuestas: pertenencia contra facultad, y todo lo propio contra todo menos el borrador.
- **Lo que no se convirtió en caso de uso.** La autorización por pertenencia no recibió caso de uso propio aunque se repita en cuatro: es una comprobación transversal declarada en §4, y convertirla en contrato separado duplicaría lo que el dominio ya resuelve en sus CU-09 y CU-11. Tampoco lo recibieron la interpretación efectiva del texto, la derivación de la contraseña, la emisión del acceso ni el guardado: son de `GeometriaFactory-Infrastructure`, detrás de los puertos.

## 9. Omisiones declaradas

| Artefacto | Estado | Motivo |
| --- | --- | --- |
| `Definicion-<Concepto-Central>.md` | **Omitido** | El concepto central de esta capa son los **puertos**, y los casos de uso ya los describen: cada uno declara cuáles consume y qué le pide a cada uno, y §3 los reúne en una sola tabla. Un documento aparte repetiría eso sin agregar semántica, y la regla lo declara recomendado y no obligatorio para `library` con superficie estrecha |
| `Reglas-De-Negocio/RN-XX-<Nombre>.md` | **Omitido** | **Las once reglas del producto viven en `GeometriaFactory-Domain`** y son atemporales: redactarlas de nuevo acá crearía dos enunciados de la misma regla en la misma cadena documental. Esta categoría las **referencia** por identificador y con enlace, y declara en §6 dónde se ejerce cada una |
| `Modelo-Datos/Modelo-Conceptual.md` y `Modelo-Datos/reglas-conceptuales-de-modelo/RC-XX-<Nombre>.md` | **Omitidos** | La regla de la categoría los omite para `library`, y el flag `tiene_persistencia` de este proyecto de código es false: el intake declara «no aplica directamente» en §17.2.P.4. El modelo del dominio vive en `Definicion-Modelo-De-Dominio.md` de `GeometriaFactory-Domain` |

## 10. Numeración y nombres de archivo

1. **Los identificadores `CU-XX` de esta carpeta son locales al proyecto de código.** El `CU-09` de esta categoría no es el `CU-09` de `GeometriaFactory-Domain` ni el que previó el catálogo de necesidades; la correspondencia se lee por la matriz de §7.1 y por la tabla de §7.4, nunca por número.
2. **La serie es contigua de CU-01 a CU-10**, sin huecos. **CU-10 se numeró al final y no junto a CU-01**, con el que forma par temático, para no renumerar los ocho casos de uso intermedios que otras categorías ya citan por su identificador. Es la misma decisión con la que `GeometriaFactory-Domain` incorporó su CU-12.
3. **El nombre de archivo de CU-01 se conserva** —`CU-01-Registrar-El-Alta-De-Una-Cuenta.md`— aunque su alcance se acotó al auto-registro, por estabilidad de citación: otras categorías ya lo citan por esa ruta. Es el mismo criterio con el que `GeometriaFactory-Domain` conservó dos nombres de regla.
4. **Las `RN-XX` que se citan conservan la numeración del intake y la de `GeometriaFactory-Domain`**, que son la misma. Dos de esos archivos llevan un slug que ya no describe del todo su enunciado y se citan igual por su ruta vigente, por la decisión de estabilidad de citación que ese proyecto de código declaró.
5. **Las `US-XX` de §7.3 son una previsión local** de esta categoría, no la numeración de las veintisiete que previó `01-Necesidades-Negocio`.

## 11. Puntos abiertos

| Punto | Situación | Quién lo resuelve |
| --- | --- | --- |
| Identificador del puerto de repositorio de cuentas | El intake nombra tres puertos y no éste, que la orquestación de las cuentas y la verificación de unicidad del correo hacen necesario (§3). **No es una regla nueva ni una decisión de alcance**: es un nombre. Acá se lo nombra en lenguaje de dominio | `05-Arquitectura-Tecnica` y el punto de control de la etapa `a` |
| Nombres de tipos y de espacios de nombres | Declarados abiertos aguas arriba y validados en el punto de control de la etapa `a`. **No es ambigüedad de esta categoría**: acá los conceptos se nombran en lenguaje de dominio | `05-Arquitectura-Tecnica` |
| Criterio de comparación de dos correos | La unicidad del correo exige decidir si dos correos se comparan tal cual o normalizados. `GeometriaFactory-Domain` lo dejó abierto y esta categoría **no lo reabre**: lo cita en CU-01 y lo deja donde está | `05-Arquitectura-Tecnica`, junto con la capa que ejerce la verificación |
| Sellos de alta, de modificación y de desenlace | El intake los sostiene como puertos verificables en prueba, pero **el modelo del dominio no los declara como atributos**: declara la fecha de alta del alumno y la «Fecha» que el alumno declara en su trabajo, y nada más. Esta capa los trata como metadatos de orquestación (§3) y la discrepancia está elevada al Product Owner por `GeometriaFactory-Domain` | Product Owner, y `GeometriaFactory-Domain` si decide incorporarlos a su modelo |
| Valores numéricos de los requerimientos no funcionales | El tiempo de 500 ms del criterio CA-06 de CU-05 está rotulado como asunción aguas arriba y pendiente de confirmación del Product Owner. Se usa como valor vigente | Product Owner, y `08-Calidad-Y-Pruebas` al verificarlo |

## 12. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. Índice maestro de nueve casos de uso con la matriz NB → CU → RN → US, la verificación bidireccional de cobertura, la necesidad de negocio que este proyecto de código no toca con su justificación y las dos que cubre parcialmente, la tabla de los cuatro puertos que esta capa declara, la sección transversal de autorización por pertenencia y verificación de facultad, la tabla de las once reglas de negocio referenciadas con el lugar donde se ejerce cada una, la correspondencia con los casos de uso de dominio orquestados —once en ese momento, doce desde que el dominio emitió el de configuración del administrador: ver §7.4—, el criterio de recorte con sus fusiones y particiones, las tres omisiones con su motivo y los cuatro puntos abiertos. |
| 1.0 | 2026-08-09 | **Correcciones de la ronda r1 del audit**, absorbidas sin subir versión por `Master-Prompt.md` §5, con el documento en estado `Propuesto`. **H-01**: el catálogo pasa de nueve a **diez** casos de uso al partirse CU-01 en los dos caminos de alta, con **CU-10** nuevo para la configuración del administrador, espejando la partición del dominio entre su CU-01 y su CU-12; §5, §7.1, §7.2, §7.3 —con US-28 nueva y US-03 reasignada—, §7.4 y §8 se actualizan, y §8 declara el fundamento de la partición. **H-04**: el preámbulo de §6 deja de afirmar que dos reglas se ejercen enteras en otra capa —que ninguna fila declaraba— y pasa a describir la tabla: las once tienen tramo acá, y RN-05 y RN-09 tienen el principal en otra capa, marcado en sus filas. **H-05**: CU-02 sale de la fila del reloj, que no consume. **H-06**: §3 declara que los sellos son **metadatos de orquestación** de esta capa, distintos de la «Fecha» del alumno, y §11 suma el punto abierto. **H-03**: la fila del puerto de validación suma la cantidad de figuras del conjunto raíz, con la precisión de por qué no es derivable de las piezas adoptadas. **H-13**: §4 declara que esta capa corta antes del dominio y colapsa en un motivo los dos que el dominio declara para la misma negativa de facultad. **H-07** y **H-14** se resuelven en las §9 y §6 de los casos de uso. |
| 1.0 | 2026-08-09 | **Corrección de la ronda r2 del audit, hallazgo H-15**, absorbida sin subir versión por `Master-Prompt.md` §5, con el documento en estado `Propuesto`. Las **dos** ocurrencias del conteo viejo de casos de uso del dominio quedan alineadas con §7.4: la cabecera pasa a declarar **doce**, y la fila de la emisión inicial precisa que eran once en ese momento y son doce desde que el dominio emitió el de configuración del administrador, de modo que el registro histórico se conserva sin contradecir al documento. |
