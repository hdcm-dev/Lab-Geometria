# Matriz de cobertura de pruebas — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** Matriz-Cobertura-Pruebas.md
**Versión:** 1.2
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `rest-api` · **Proyecto de código principal del producto**
**Trazabilidad upstream:** [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) 1.0; [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §5, §6 y §7.3; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §3.1, §3.4, §8, §10.2 y §10.3; [`../05-Arquitectura-Tecnica/Contratos-REST.md`](../05-Arquitectura-Tecnica/Contratos-REST.md) §5
**Trazabilidad downstream:** [`Criterios-Validacion.md`](Criterios-Validacion.md), [`Definition-Of-Done.md`](Definition-Of-Done.md); `09-Devops`

---

## Tabla de contenido

- [1. Propósito y alcance](#1-propósito-y-alcance)
- [2. Trazabilidad CU ↔ tests](#2-trazabilidad-cu--tests)
  - [2.1 La inspección con umbral exacto que traza a una regla de arquitectura](#21-la-inspección-con-umbral-exacto-que-traza-a-una-regla-de-arquitectura)
- [3. Trazabilidad NFR ↔ tests](#3-trazabilidad-nfr--tests)
- [4. Trazabilidad RN ↔ tests](#4-trazabilidad-rn--tests)
- [5. Trazabilidad punto de acceso ↔ tests](#5-trazabilidad-punto-de-acceso--tests)
- [6. Trazabilidad invariante ↔ tests](#6-trazabilidad-invariante--tests)
- [7. Cobertura por capa](#7-cobertura-por-capa)
- [8. Huecos identificados](#8-huecos-identificados)
- [9. Control de cambios](#9-control-de-cambios)

---

## 1. Propósito y alcance

Es el documento bisagra de la categoría: relaciona los **doce** casos de uso, los **diecisiete** NFR, las **dieciséis** reglas de negocio, los **quince** puntos de acceso y los **nueve** invariantes con los **treinta y siete** casos de verificación de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md), y declara la cobertura por componente.

**Ninguna columna de estado afirma que algo esté verde.** Todas dicen `Pendiente`, porque el sistema no está construido.

Esta matriz **agrega dos tablas** a las tres que `Rules-Calidad-Y-Pruebas.md` §4.5 exige: la de punto de acceso contra prueba y la de invariante contra prueba. La primera, porque `Rules-Calidad-Y-Pruebas.md` §2.2 exige para el tipo `rest-api` **100 % de endpoints cubiertos**, y esa cobertura no cabe dentro de la tabla de casos de uso: un caso de uso agrupa varios puntos. La segunda, porque `05` §10.3 declara qué aporta esta capa a cada invariante, y en dos de ellos —`INV-02` e `INV-09`— **la propiedad observable se decide acá**.

## 2. Trazabilidad CU ↔ tests

Doce filas, una por caso de uso de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §5. Ninguna se agrupa.

| CU | Criterio Given-When-Then principal | Tests | Historias | Estado |
| --- | --- | --- | --- | --- |
| CU-01 Canjear credenciales por un acceso firmado | Given credenciales válidas, When se las canja, Then se emite el acceso; con credenciales inválidas, Then la respuesta **no declara cuál campo falló**; con la cuenta no admitida, Then **el motivo sí se dice** | `TC-01`, `TC-02`, `TC-03` | US-01, US-02, US-03 | `Pendiente` |
| CU-02 Admitir la petición: acceso, papel y marca | Given un acceso ausente, vencido o con firma ajena, Then la petición se rechaza; el papel se exige por punto; y **la guardia del cambio pendiente alcanza a todos los puntos salvo uno** | `TC-04`, `TC-05`, `TC-06`, `TC-07` | US-04, US-05, US-06 | `Pendiente` |
| CU-03 Exponer el alta de cuenta y la credencial propia | Given el registro **sin campo de contraseña** y la configuración del administrador con su ventana, When se los ejerce, Then proceden una sola vez cada uno; el cambio de contraseña propia recorre sus dos formas | `TC-08`, `TC-09`, `TC-10` | US-07, US-08, US-09, US-10 | `Pendiente` |
| CU-04 Exponer el gobierno de las cuentas de la comisión | Given el administrador, When lista, cambia la situación o da de baja, Then cada punto exige su papel y **la baja transporta el correo escrito sin compararlo acá** | `TC-11`, `TC-12`, `TC-13` | US-11, US-12, US-13 | `Pendiente` |
| CU-05 Exponer el reseteo de la contraseña de un alumno | Given una cuenta en cualquiera de sus tres estados, When se la resetea, Then procede, **la provisoria se devuelve una sola vez** y **no aparece en ninguna traza** | `TC-14`, `TC-15`, `TC-16` | US-14, US-15, US-16 | `Pendiente` |
| CU-06 Exponer el envío y la eliminación de un trabajo | Given un texto cuyo contenido no verifica, When se lo envía, Then **la respuesta es exitosa** con el estado decidido; el texto **no se normaliza y no se trunca**; la eliminación se verifica **forzando la petición** | `TC-17`, `TC-18`, `TC-19`, `TC-20` | US-17, US-18, US-19, US-20 | `Pendiente` |
| CU-07 Exponer el listado y el detalle de los trabajos | Given el listado, When se inspecciona su superficie, Then **no hay parámetro con el que pedir borradores ajenos**; el detalle trae piezas, componentes, observaciones y comentario | `TC-21`, `TC-22` | US-21, US-22 | `Pendiente` |
| CU-08 Exponer el desenlace de la revisión | Given un trabajo en estado `Pendiente` y el administrador, When lo aprueba o rechaza, Then alcanza su estado terminal; desde un terminal, desde un `Borrador` o con papel de alumno, Then se rechaza con códigos distintos | `TC-23` | US-23 | `Pendiente` |
| CU-09 Traducir el motivo del contrato a respuesta de protocolo | Given los **quince** códigos, When se recorre la tabla, Then **14** tienen destino y **1** está declarado sin él; las **tres** familias empobrecidas dan respuestas indistinguibles; **0** respuestas exponen la topología | `TC-24`, `TC-25`, `TC-26`, `TC-27` | US-24, US-25 | `Pendiente` |
| CU-10 Componer la aplicación y conectar los puertos con sus adaptadores | Given la composición, When se resuelve, Then **4 de 4** puertos tienen un adaptador y falta alguno **falla en construcción**; hay **1** sola configuración de intercambio en el producto | `TC-28`, `TC-29` | US-26 | `Pendiente` |
| CU-11 Arrancar el servicio y dejar el almacén en condiciones | Given un almacén inexistente, When arranca, Then dispara la preparación; si no puede completarse, **el arranque se detiene y no se atiende ninguna petición**; salud responde **sin exigir acceso** | `TC-30`, `TC-31`, `TC-32`, `TC-33` | US-27, US-28, US-29 | `Pendiente` |
| CU-12 Ejercitar la superficie con la colección de peticiones reproducible | Given la colección versionada, When se la ejecuta, Then recorre la superficie en **5 pasos o menos** con **0 datos de prueba inventados** | `TC-35` | US-30 | `Pendiente` |

**Doce de doce casos de uso con al menos un caso de verificación, y treinta de treinta historias cubiertas.**

**Treinta y seis de los treinta y siete `TC-XX` tienen fila en alguna de las cinco tablas de trazabilidad de esta matriz.** El restante es una inspección con umbral exacto cuya trazabilidad es hacia una **regla de arquitectura de nivel producto**, un **riesgo** y un **criterio de aceptación de etapa**, y por eso no aparece en ninguna de ellas. Está en §2.1, y **no queda sin instrumento de trazabilidad**.

### 2.1 La inspección con umbral exacto que traza a una regla de arquitectura

| Caso de verificación | Qué verifica | A qué traza, según su campo «Cubre» | Estado |
| --- | --- | --- | --- |
| `TC-36` Sin canal de sesión interactiva y sin intercambio de origen cruzado | **Tres ausencias**: no expone ni requiere canal de sesión interactiva, no tiene configuración de intercambio de origen cruzado, y ningún punto de acceso está pensado para el navegador | `RA-01`; el sexto riesgo de `05` §9; criterio de aceptación de la etapa `a` | `Pendiente` |

**Por qué no se le inventa un `CU-XX` ni un punto de acceso.** Lo que mide son **ausencias** en la superficie entera, no el comportamiento de un punto; reabrir cualquiera de las tres rompe `RA-01`, que es regla de nivel producto y no propiedad de un caso de uso. Lo que corresponde es que **esté enumerada**, y esta subsección es su instrumento.

## 3. Trazabilidad NFR ↔ tests

Diecisiete filas, una por cada NFR de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §8.

| NFR | Objetivo numérico | Cómo se verifica | Estado |
| --- | --- | --- | --- |
| Latencia del listado | **Percentil 99 por debajo de 500 ms**, medido **en el servidor** **[ASUNCIÓN]** | `TC-34`. Gate `QG-14`, condicionado | `Pendiente` |
| Caudal sostenido | **20 peticiones por minuto** **[ASUNCIÓN]** | `TC-34`. Gate `QG-14`, condicionado | `Pendiente` |
| Arranque en frío | Menos de **30 segundos** **[ASUNCIÓN]** | `TC-33`. Gate `QG-13`, condicionado | `Pendiente` |
| Cobertura del proyecto de código | **75 %** de líneas y **70 %** de ramas **[ASUNCIÓN]** | Informe del pipeline, **no un caso de verificación**. Gate `QG-03`, condicionado | `Pendiente` |
| Forma de la pirámide de pruebas | **60 %** integración y **40 %** unitarias **[ASUNCIÓN en cuanto al reparto]** | `TC-37`. Gate `QG-04`, condicionado. **La inversión no es asunción** | `Pendiente` |
| Puntos de acceso fuera de la guardia | Exactamente **4** sobre **15**, **ni uno más** | `TC-07`, inspección en las dos direcciones | `Pendiente` |
| Puntos que fijan una contraseña sobre una cuenta existente sin credencial | Exactamente **0** | `TC-10`, inspección de los cuatro puntos que no exigen acceso | `Pendiente` |
| Códigos del contrato con traducción declarada | **14 de 15**, con **1** sin destino y su motivo; **0** inventados y **0** renombrados | `TC-24` y `TC-27`, en las dos direcciones | `Pendiente` |
| Respuestas indistinguibles de las tres familias empobrecidas | **3 de 3** comparaciones idénticas, cuerpo y código | `TC-25` | `Pendiente` |
| Respuestas que exponen dirección, ruta, secreto o traza | Exactamente **0**, sobre los quince puntos y sobre el registro del servidor | `TC-26` | `Pendiente` |
| Configuraciones de intercambio declaradas en el producto | Exactamente **1**, compartida por los dos extremos | `TC-29` | `Pendiente` |
| Textos originales alterados en el borde | **0** caracteres de diferencia y **0** truncamientos silenciosos | `TC-19` | `Pendiente` |
| Puertos conectados a su adaptador | **4 de 4**, con **0** sin adaptador o con más de uno | `TC-28`, con fallo en construcción | `Pendiente` |
| Peticiones atendidas con la preparación del almacén incompleta | Exactamente **0** | `TC-31` | `Pendiente` |
| Eliminaciones fuera de alcance aceptadas al forzar la petición | Exactamente **0** | `TC-20`. Gate `QG-12` | `Pendiente` |
| Advertencias de construcción | Exactamente **0** | Gate `QG-01`, **no un caso de verificación** | `Pendiente` |
| Pasos de la colección de peticiones reproducible | **5 o menos**, con **0** datos de prueba inventados | `TC-35`. Gate `QG-15` | `Pendiente` |

**Los valores rotulados [ASUNCIÓN] se citan con su rótulo y no se convierten en compromiso.** Su confirmación está pendiente del Product Owner en el intake §22 —asunción `A-3` para la cobertura y la forma de la pirámide, `A-5` para el percentil, el caudal y el arranque en frío—.

**Dos de los diecisiete NFR no tienen caso de verificación propio y es correcto**: uno es el informe de cobertura del pipeline y el otro la puerta de construcción.

**No hay NFR de disponibilidad y esta matriz no le inventa fila.** El intake declara «sin SLO»: la caída del servidor domiciliario se responde con **estado degradado en el front**.

## 4. Trazabilidad RN ↔ tests

Dieciséis filas, una por regla. El tramo de cada una es el que `05` §10.2 le asigna **en esta capa**; esta matriz lo refleja y no lo redefine.

| RN | Tramo en esta capa | Tests | Estado |
| --- | --- | --- | --- |
| RN-01 Administrador único y papeles fijos | El punto de configuración con su negativa cuando ya existe una, y el papel exigido por punto | `TC-05`, `TC-09`, `TC-12` | `Pendiente` |
| RN-02 El correo del alumno es único | La traducción del correo ocupado a una respuesta que **no declara la situación ni el papel** de la cuenta que lo ocupa | `TC-08`, `TC-25` | `Pendiente` |
| **RN-03 Trabajo ajeno indistinguible de inexistente** | **Tramo de traducción, y es el que esta capa puede romper sola.** Los tres casos reciben **el mismo código y el mismo cuerpo** | `TC-20`, `TC-22`, `TC-25` | `Pendiente` |
| RN-04 Eliminación acotada al borrador | Los dos alcances sobre el mismo punto. **Es la única regla con un criterio de verificación que exige forzar la petición contra esta superficie** | `TC-20` | `Pendiente` |
| RN-05 No se pasa a estado `Pendiente` con errores de validación | **Sin tramo acá.** El estado llega decidido y viaja en una respuesta **exitosa**. Lo que se verifica es que esta capa **no lo convierta en un fallo** | `TC-17` | `Pendiente` |
| RN-06 Cuenta `Pendiente` o `Bloqueado` sin acceso | La respuesta **con motivo** del canje, distinta de la genérica de credenciales inválidas | `TC-03`, `TC-12` | `Pendiente` |
| RN-07 Baja con arrastre y confirmación escrita | El punto **transporta** el correo escrito y no procede sin él. **La comparación y el arrastre son de adentro** | `TC-13` | `Pendiente` |
| RN-08 Texto original conservado íntegro | **El borde es el primer lugar donde el texto puede alterarse**: no se normaliza, no se recodifica y el cuerpo que excede el límite **se rechaza, nunca se trunca** | `TC-18`, `TC-19` | `Pendiente` |
| RN-09 Observación de error con posición y campo | La ubicación **cruza la frontera sin recortarse**. Producirla es de adentro; **no perderla al traducir es de acá** | `TC-17`, `TC-22` | `Pendiente` |
| RN-10 Desenlace exclusivo del administrador y terminalidad | El papel exigido en el punto y la traducción del estado que no admite desenlace, **incluido el terminal** | `TC-23` | `Pendiente` |
| RN-11 El administrador no ve los borradores | **De forma negativa**: la superficie **no declara ningún parámetro** con el que pedir borradores | `TC-20`, `TC-21` | `Pendiente` |
| RN-12 El reseteo conserva la cuenta y sus trabajos | El reseteo y la baja son **dos puntos distintos con verbos distintos**, y el del reseteo **no toca ninguna ruta de retiro** | `TC-14` | `Pendiente` |
| **RN-13 Cambio forzado antes de toda otra capacidad** | **Tramo transversal, y es el otro que esta capa puede romper sola.** Un punto nuevo fuera de la guardia la rompe **sin que nada falle** | `TC-06`, `TC-07`, `TC-10` | `Pendiente` |
| RN-14 La provisoria la produce el sistema | **Sin tramo acá.** Lo que esta capa declara es **lo que no hace con el valor**: no lo registra en ninguna traza y lo devuelve **una sola vez** | `TC-14`, `TC-16` | `Pendiente` |
| RN-15 Resetear no exige cuenta habilitada | **De forma estructural**: el punto **no declara ningún parámetro de situación** y su tabla de respuestas no tiene ninguna fila por ese concepto | `TC-15` | `Pendiente` |
| RN-16 Habilitar produce la provisoria | **Sin tramo propio acá**, con dos efectos estructurales: un identificador de punto **retirado y no reciclado**, y el punto de situación devolviendo la provisoria. Lo que esta capa aporta es **no exponer ningún punto que la contradiga** | `TC-10`, `TC-12` | `Pendiente` |

**Trece de las dieciséis con tramo acá y tres sin él**, que es exactamente el reparto que `05` §10.2 declara. **Las tres sin tramo tienen caso de verificación igual**, y lo que verifican es una afirmación distinta: que esta capa **no deshaga** lo que otra decidió.

**Dos reglas están señaladas como las que esta capa puede romper sola** —`RN-03` y `RN-13`—, y son las que concentran los dos primeros riesgos de `05` §9. Sus casos son los que [`Plan-Pruebas.md`](Plan-Pruebas.md) §4 trata con la prioridad más alta.

## 5. Trazabilidad punto de acceso ↔ tests

Quince filas, una por punto de `05` §3.4. **Es la tabla que hace verificable el 100 % de puntos cubiertos que `Rules-Calidad-Y-Pruebas.md` §2.2 exige para el tipo `rest-api`.**

| Punto | Intención, en una línea | ¿Bajo la guardia? | Tests | Estado |
| --- | --- | --- | --- | --- |
| A-01 | Canjear correo y contraseña por un acceso firmado | **No** | `TC-01`, `TC-02`, `TC-03`, `TC-07` | `Pendiente` |
| A-02 | Registrar una cuenta de alumno, sin campo de contraseña | **No** | `TC-08`, `TC-07`, `TC-25` | `Pendiente` |
| A-03 | Configurar la cuenta de administrador, sólo mientras no exista ninguna | **No** | `TC-09`, `TC-07` | `Pendiente` |
| A-05 | Cambiar la contraseña propia exigiendo la vigente | **Sí**, y es la **única excepción** de la guardia del cambio pendiente | `TC-06`, `TC-10` | `Pendiente` |
| A-06 | Listar las cuentas de la comisión con su situación y su marca | **Sí** | `TC-11`, `TC-06` | `Pendiente` |
| A-07 | Cambiar la situación de una cuenta | **Sí** | `TC-12`, `TC-06` | `Pendiente` |
| A-08 | Dar de baja una cuenta con el correo escrito | **Sí** | `TC-13`, `TC-06` | `Pendiente` |
| A-09 | Resetear la contraseña de un alumno | **Sí** | `TC-14`, `TC-15`, `TC-16`, `TC-06` | `Pendiente` |
| A-10 | Enviar un trabajo nuevo | **Sí** | `TC-17`, `TC-19`, `TC-06` | `Pendiente` |
| A-11 | Reenviar un trabajo en `Borrador` | **Sí** | `TC-18`, `TC-06` | `Pendiente` |
| A-12 | Eliminar un trabajo, con los dos alcances | **Sí** | `TC-20`, `TC-06` | `Pendiente` |
| A-13 | Listar trabajos con el alcance que el papel determina | **Sí** | `TC-21`, `TC-06` | `Pendiente` |
| A-14 | Obtener el detalle de un trabajo interpretado | **Sí** | `TC-22`, `TC-06` | `Pendiente` |
| A-15 | Aprobar o rechazar un trabajo en estado `Pendiente` | **Sí** | `TC-23`, `TC-06` | `Pendiente` |
| A-16 | Responder por el estado del servicio | **No** | `TC-32`, `TC-07` | `Pendiente` |

**Quince de quince puntos con caso de verificación: cuatro fuera de la guardia y once bajo ella. Cuatro más once son quince.** El identificador retirado **no se recicla y no tiene fila**, porque no existe: la operación que exponía dejó de existir con `RN-16`.

**`TC-06` aparece en los once puntos bajo la guardia**, y no es redundancia: es la prueba que recorre los diez rechazos y la única excepción. **`TC-07` aparece en los cuatro exentos y recorre los quince** en las dos direcciones.

## 6. Trazabilidad invariante ↔ tests

Nueve filas. La columna de aporte es la de `05` §10.3: declara **qué hace esta capa por cada uno**, que es una cosa distinta de enunciarlo.

| Invariante | Qué aporta esta capa | Tests | Estado |
| --- | --- | --- | --- |
| INV-01 Correo único | Traducir la colisión a una respuesta que **no revela nada** de la cuenta que ocupa el correo | `TC-08`, `TC-25` | `Pendiente` |
| INV-02 Acceso sólo a los trabajos propios | **El aporte más delicado**: la comprobación es de adentro, pero **la propiedad observable se decide acá** | `TC-20`, `TC-22`, `TC-25` | `Pendiente` |
| INV-03 Eliminación por el alumno sólo en `Borrador` y sobre trabajo propio | Lo mismo, más el criterio que la fuente exige ejercer **forzando la petición** | `TC-20` | `Pendiente` |
| INV-04 Trabajo `Finalizado` sin errores de interpretación | **Nada propio, y es correcto**: lo que esta capa hace es **no convertir el estado en un fallo** | `TC-17` | `Pendiente` |
| INV-05 Exactamente un administrador | Exponer el punto de configuración **con su ventana** y traducir la negativa a conflicto de estado | `TC-09` | `Pendiente` |
| INV-06 Cuenta `Pendiente` o `Bloqueado` sin acceso | Responder **con motivo** en el canje, distinto de la respuesta genérica | `TC-03` | `Pendiente` |
| INV-07 Estado terminal sin salida ni cambio de contenido | Traducir el estado que no admite desenlace **incluido el terminal**, y **no sugerir ninguna forma de revertirlo** | `TC-23` | `Pendiente` |
| INV-08 La cuenta de administrador está siempre `Habilitado` | **Nada propio, y es correcto**: no hay punto de acceso que pueda cambiar su situación ni darla de baja | `TC-07`, `TC-11` | `Pendiente` |
| INV-09 Cuenta con la marca puesta sin ninguna otra capacidad | **El aporte más consecuente**: garantizar que **ningún punto quede fuera de la guardia**, que es la parte que se rompe agregando un punto y olvidándose | `TC-06`, `TC-07` | `Pendiente` |

**Nueve de nueve con caso de verificación.** Las dos filas que declaran «nada propio» tienen prueba igual, y lo que verifican es la **ausencia de una puerta**: que ningún punto de acceso permita lo que el invariante prohíbe.

## 7. Cobertura por capa

La partición es por los **ocho** componentes de `05` §3.1. Los umbrales son los de [`Estrategia-Testing.md`](Estrategia-Testing.md) §2.

| Componente | Líneas medidas | Ramas medidas | Mutation score medido | Umbral mínimo (líneas / ramas / mutación) |
| --- | --- | --- | --- | --- |
| Composición de raíz | Sin medir | Sin medir | **No aplica** | 75 / 70 / — |
| **Guardia de admisión** | Sin medir | Sin medir | Sin medir | **95 / 90** / 60 |
| **Traductor de motivos y códigos** | Sin medir | Sin medir | Sin medir | **95 / 90** / 60 |
| Superficie de acceso y credencial propia | Sin medir | Sin medir | Sin medir | 80 / 75 / 60 |
| Superficie de gobierno de la comisión | Sin medir | Sin medir | Sin medir | 75 / 70 / 60 |
| Superficie de trabajos | Sin medir | Sin medir | Sin medir | 80 / 75 / 60 |
| Superficie de desenlace | Sin medir | Sin medir | Sin medir | 75 / 70 / 60 |
| Arranque y salud | Sin medir | Sin medir | Sin medir | 85 / 80 / 60 |
| **Proyecto de código completo** | Sin medir | Sin medir | Sin medir | **75 / 70 / 60** |

**«Sin medir» y no «0 %».** No hay código construido: un cero sería una afirmación falsa sobre el estado del sistema.

**Además de la cobertura de líneas hay una cobertura contable que no admite promedio**, y es la que esta matriz reporta en sus §5 y §3: **15 de 15** puntos ejercidos, **15 de 15** códigos recorridos, **4** puntos fuera de la guardia, **3 de 3** familias indistinguibles y **4 de 4** puertos conectados.

**El umbral global de 75 / 70 viene rotulado [ASUNCIÓN] y es el piso más bajo del producto**, con el motivo declarado en la fuente: este proyecto de código es cableado. El **mutation score de 60 %** es el piso de `Rules-Calidad-Y-Pruebas.md` §2.2 y **no se le atribuye al intake**. La composición de raíz queda exenta con su fundamento: es declaración de cableado y su verificación real es el fallo en construcción de `TC-28`.

## 8. Huecos identificados

| Hueco | Consecuencia | Plan de remediación |
| --- | --- | --- |
| ~~**El intake escribía «nueve pruebas del validador» en el gate de este proyecto de código** —§17.5.P.8— **y esa batería tiene diez**~~ **CERRADO** | Un lector del gate podía dar la puerta por cumplida con nueve, dejando `E-8` sin cubrir | **Cerrado por el intake 1.20**, que corrigió §17.5.P.8 —y los otros cuatro lugares que decían nueve— sobre el hallazgo que levantó esta categoría. No queda nada derivado al Product Owner por este motivo. Esta categoría aplicó **diez** desde su emisión, siguiendo la Fase C de `GeometriaFactory-Infrastructure`. Ver [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3.2 |
| **El piso de cobertura de líneas baja el de la guía y no hay ADR** | `Rules-Calidad-Y-Pruebas.md` §2.2 fija **80 %** de aplicación para el tipo `rest-api` y este proyecto de código fija **75 %**, por el valor que el intake §17.5.P.6 declara con rótulo [ASUNCIÓN]. §2.2 exige un **ADR** para bajar cobertura, y no hay ninguna | **La categoría 05**, que es donde viven las ADR, con la constancia de que el número viene del intake y no de esta categoría. Mientras tanto la caída queda **declarada** en [`Estrategia-Testing.md`](Estrategia-Testing.md) §2 y compensada componente por componente, y **no se sube el número por cuenta propia** |
| **La medición de mutación no está en el pipeline** y su herramienta no está elegida | El umbral de 60 % no se puede exigir todavía en los siete componentes con umbral | Elección y anclaje junto con el resto del tooling de la etapa `a`; hasta que corra, se reporta «sin medir» y no bloquea |
| **Los valores rotulados [ASUNCIÓN]** —cobertura, forma de la pirámide, percentil, caudal y arranque en frío— siguen sin confirmar | Los gates `QG-03`, `QG-04`, `QG-13` y `QG-14` son condicionados y no bloquean la fusión | El Product Owner sobre el intake §22, antes de fijar las puertas en `09-Devops` |
| **El formato de intercambio y su configuración** no están fijados, y **la decisión es de esta categoría 05 como productor** | `TC-29` verifica que haya **1** sola configuración; **cuál sea** no está decidido, y los dos extremos tienen que coincidir o el contrato deja de ser el mismo | La categoría 05 de este proyecto de código, con `GeometriaFactory-Web` como consumidor |
| **La forma definitiva de las rutas se valida en el punto de control de la etapa `a`** | Los casos de verificación citan los puntos por su identificador `A-XX` y **no por su ruta**, precisamente para no atarse a un valor que todavía se valida | El punto de control de la etapa `a`. **Los identificadores no cambian** |
| **El mecanismo de construcción de la imagen en destino está rotulado [A VERIFICAR] por la fuente** | `PT-04` verifica que la imagen se construya y arranque **desde el contenedor de desarrollo**; que se construya **en destino desde el repositorio** es otra cosa y la fuente pide probarlo **una vez antes de depender de él** | El Product Owner y `09-Devops`, antes del despliegue real. **No es criterio de esta categoría**: el despliegue es manual y del Product Owner |
| ~~**Ninguna fila `VER-XX` y ninguna matriz de sensado de deriva**~~ · **Cerrado el 2026-08-11** | Se declaraba porque este proyecto de código no ejecutó la Fase B2 —`requiere_maqueta` es false— y no tenía categoría 10 emitida | **Cerrado**: se emitió [`../10-Examples/`](../10-Examples/) con **tres** contratos de verificación, `VER-01` a `VER-03` —el segundo es la **colección de peticiones reproducible** de `CU-12`—, y con ellos [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) 1.0, que declara **tres** filas, `SD-01` a `SD-03`, todas en `Sin verificar`. La matriz nace **sin ninguna fila de línea de base visual**, porque la Fase B2 sigue sin haberse ejecutado: es el caso de `Deriva-Rules.md` §2.3. La fila se conserva con su desenlace en lugar de retirarse |

## 9. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.2 | 2026-08-11 | **Cierre del hueco de sondas `VER-XX`** declarado en §8. Se emitió [`../10-Examples/`](../10-Examples/) con **tres** contratos de verificación y con ellos [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) 1.0, con **tres** filas en `Sin verificar`. La fila del hueco se **conserva** con su desenlace y su fecha. **Ninguna tabla de cobertura, ningún umbral y ningún caso de prueba cambian**: las sondas no sustituyen a ninguno, `TC-35` sigue siendo el caso que verifica la colección, y §5 de la propia matriz de sensado declara qué queda fuera de las sondas, incluido el punto de acceso **`A-08`**, que ningún sample ejercita. |
| 1.1 | 2026-08-11 | **`H-01`.** El primer hueco de §8 estaba **abierto con remediación pendiente del Product Owner** sobre algo que el Product Owner ya resolvió: el intake **1.20** corrigió los cinco lugares que decían nueve. La fila **se conserva** —para no dejar hueco de numeración— y queda **cerrada** con su desenlace. **`H-04`.** `TC-36` estaba definido en el catálogo y no tenía fila en ninguna tabla de esta matriz; se agrega **§2.1** con su trazabilidad hacia `RA-01`, el sexto riesgo de `05` §9 y el criterio de aceptación de la etapa `a`. **`H-06`.** §8 suma el hueco del piso de cobertura que baja el de la guía sin ADR. Ningún caso, umbral ni decisión de prueba cambia. Corrige contra [`../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md`](../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md) 1.0 y contra el texto vivo del intake **1.20**. |
| 1.0 | 2026-08-11 | Emisión inicial. Declara las tres tablas obligatorias —**doce** filas de caso de uso con sus **treinta** historias, **diecisiete** de NFR y **dieciséis** de regla de negocio, ninguna agrupada—, más dos propias: los **quince** puntos de acceso, que es lo que hace verificable el 100 % de puntos que la regla exige para el tipo `rest-api`, y los **nueve** invariantes. Refleja el reparto de `05` §10.2 —trece reglas con tramo acá y tres sin él, con caso de verificación igual— y señala las **dos** que esta capa puede romper sola. Declara la cobertura por los **ocho** componentes con «Sin medir» en lugar de cero, con la guardia de admisión y el traductor muy por encima del piso, y la cobertura contable que no admite promedio. Cita los valores rotulados **[ASUNCIÓN]** con su rótulo. Declara **siete** huecos, el primero de ellos la divergencia entre el gate del intake que escribe «nueve» y la batería del validador de **diez**. |
