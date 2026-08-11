# Matriz de cobertura de pruebas — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** Matriz-Cobertura-Pruebas.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `web-monolith`
**Trazabilidad upstream:** [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) 1.1; [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) **1.2**; [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) 1.7 §3, §4 y §6; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §3.1, §8, §10.2 y §10.3; [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §3
**Trazabilidad downstream:** [`Criterios-Validacion.md`](Criterios-Validacion.md), [`Definition-Of-Done.md`](Definition-Of-Done.md); `09-Devops`

---

## Tabla de contenido

- [1. Propósito y alcance](#1-propósito-y-alcance)
- [2. Trazabilidad CU ↔ tests](#2-trazabilidad-cu--tests)
- [3. Trazabilidad NFR ↔ tests](#3-trazabilidad-nfr--tests)
- [4. Trazabilidad RN ↔ tests](#4-trazabilidad-rn--tests)
- [5. Trazabilidad restricción transversal ↔ tests](#5-trazabilidad-restricción-transversal--tests)
- [6. Cobertura por capa](#6-cobertura-por-capa)
- [7. Relación con la matriz de sensado de deriva](#7-relación-con-la-matriz-de-sensado-de-deriva)
- [8. Huecos identificados](#8-huecos-identificados)
- [9. Control de cambios](#9-control-de-cambios)

---

## 1. Propósito y alcance

Es el documento bisagra de la categoría: relaciona los **diez** casos de uso, los **catorce** NFR, las **dieciséis** reglas de negocio y las **trece** restricciones transversales con los **treinta y cinco** casos de verificación de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md), y declara la cobertura por componente.

**Ninguna columna de estado afirma que algo esté verde.** Todas dicen `Pendiente`, porque el sistema no está construido. La maqueta sí está aprobada y validada, y eso es una cosa distinta: lo aprobado es la **línea de base**, no el sistema.

Esta matriz **agrega una cuarta tabla** a las tres que `Rules-Calidad-Y-Pruebas.md` §4.5 exige: la de restricción transversal contra prueba. El motivo es que las **trece** restricciones de `02` §6 no son reglas de negocio —esta pieza no hace cumplir ninguna— y sin embargo son lo que este proyecto de código sí tiene que sostener, con un componente y una ADR asignados a cada una en `05` §10.2.

**Y declara en §7 su relación con [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md)**, que ya existía antes de esta fase y que **no se duplica acá**.

## 2. Trazabilidad CU ↔ tests

Diez filas, una por caso de uso de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3. Ninguna se agrupa.

| CU | Criterio Given-When-Then principal | Tests | Historias cubiertas | Estado |
| --- | --- | --- | --- | --- |
| CU-01 Registrar la cuenta de alumno | Given la superficie de registro **sin ningún campo de contraseña**, When se registra un correo libre, Then aparece el bloque de éxito; con un correo ya usado, Then error de operación **sin revelar de quién es la cuenta** | `TC-02` | US-01, US-02 | `Pendiente` |
| CU-02 Iniciar y cerrar sesión sin exponer la credencial | Given una sesión iniciada, When se inspecciona el navegador, Then la credencial **no aparece**; el ingreso lleva a la ruta inicial del papel, el cierre vuelve al ingreso con su banda, y una cuenta que no admite ingreso recibe **su motivo** | `TC-03`, `TC-04`, `TC-05`, `TC-07` | US-03, US-04, US-05, US-29 | `Pendiente` |
| CU-03 Establecer y cambiar la contraseña propia | Given los **tres** cursos —primer ingreso con la provisoria, cambio con la vigente y cambio forzado—, When se recorren, Then son **el mismo formulario y el mismo contrato**, y sólo el tercero no tiene salida | `TC-06`, `TC-07` | US-06, US-07, US-28 | `Pendiente` |
| CU-04 Administrar las cuentas de la comisión | Given el panel de cuentas, When se ejercen las **cinco** operaciones, Then la acción de situación ofrece **sólo la transición admitida**, la baja exige el correo escrito con su aviso de arrastre, y habilitar y resetear **comunican la provisoria** | `TC-01`, `TC-08`, `TC-09`, `TC-10` | US-08, US-09, US-10, US-30 | `Pendiente` |
| CU-05 Enviar un trabajo y ver el resultado de la interpretación | Given el texto pegado, When se previsualiza, Then **se dibuja sin verificar y sin ninguna petición**; When se envía, Then el texto viaja **carácter por carácter** y el resultado muestra advertencias con su par de valores o errores con índice y campo | `TC-11`, `TC-12`, `TC-13`, `TC-14` | US-11, US-12, US-13, US-14 | `Pendiente` |
| CU-06 Consultar el listado propio y operar sobre el borrador | Given trabajos en los cuatro estados, When se abre el panel propio, Then los controles **no se dibujan** fuera de `Borrador` y el desenlace se ve en la fila; el vacío se distingue del fallo **por el tipo recibido** | `TC-15`, `TC-16`, `TC-26`, `TC-28` | US-15, US-16, US-17 | `Pendiente` |
| CU-07 Abrir un trabajo y explorarlo en escena y árbol | Given un trabajo abierto, When se lo explora, Then la vista tiene **sus cuatro partes**, el árbol y la escena se sincronizan **por índice**, los dos movimientos se gobiernan desde el anfitrión y diez recorridos **no degradan** | `TC-16`, `TC-17`, `TC-18`, `TC-19`, `TC-20`, `TC-21`, `TC-22`, `TC-23` | US-18, US-19, US-20, US-21 | `Pendiente` |
| CU-08 Recorrer la entrega de la comisión | Given trabajos de dos alumnos en los cuatro estados, When el administrador abre el listado, Then está agrupado y filtrable, **ningún `Borrador` aparece**, y el filtrado sin resultados se distingue del vacío | `TC-24`, `TC-28` | US-22, US-23 | `Pendiente` |
| CU-09 Resolver un trabajo con comentario opcional | Given un trabajo en estado `Pendiente` abierto como administrador, When se lo aprueba o rechaza, Then procede con comentario opcional y se vuelve al listado actualizado; el bloque de decisión **no tiene ruta propia** y no aparece para el alumno | `TC-25`, `TC-26` | US-24, US-25 | `Pendiente` |
| CU-10 Sostener la aplicación en estado degradado y reconexión | Given el servicio detenido y el circuito cortado, When se opera, Then los **dos tramos** se distinguen, el aviso reemplaza el contenido **y no el armazón**, y lo escrito se conserva | `TC-27`, `TC-28` | US-26, US-27 | `Pendiente` |

**Diez de diez casos de uso con al menos un caso de verificación, y treinta de treinta historias cubiertas.** Ninguno queda huérfano.

## 3. Trazabilidad NFR ↔ tests

Catorce filas, una por cada NFR de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §8.

| NFR | Objetivo numérico | Cómo se verifica | Herramienta de medición | Estado |
| --- | --- | --- | --- | --- |
| `PT-01.a` arranque en la dirección pública | Respuesta **200** | `TC-34` | Comprobación al final del flujo de publicación | `Pendiente` |
| `PT-01.b` transporte del circuito | Semáforo; **amarillo aceptable** documentando la latencia percibida; sólo el rojo obliga a cambiar el modelo de front | `TC-34` | Inspección del transporte negociado | `Pendiente` |
| `PT-01.c` estabilidad del proceso | **20 minutos** de navegación continua sin reciclado, y reconexión funcional al cortar y restablecer | `TC-34` | Recorrido cronometrado. **No tiene mitigación en el código** (`R-06`) | `Pendiente` |
| `PT-01.d` salida hacia el backend | Una llamada de salud devuelve **datos reales** del servidor propio | `TC-34` | Recorrido en la etapa `a` | `Pendiente` |
| Pasos del guion de demostración | **100 %** de la etapa **y de todas las anteriores** **[ASUNCIÓN del intake §17.6.P.6 en cuanto a expresarlo como puerta]** | `TC-35`. Gate `QG-04`, bloqueante | Ejecución en el navegador del equipo anfitrión | `Pendiente` |
| Peticiones del navegador hacia el servicio de datos | Exactamente **0**, con los dos movimientos prendidos | `TC-29` | Conteo en la pestaña de red | `Pendiente` |
| Salidas del proyecto de código hacia el servicio de datos | Exactamente **1**, y **0** bibliotecas de guion que consulten | `TC-30` | Inspección del árbol de fuentes y de las dependencias de guion | `Pendiente` |
| Apariciones de la credencial de sesión en el navegador | Exactamente **0** | `TC-03` | Inspección del almacenamiento, de las marcas de sesión y del contenido servido | `Pendiente` |
| Mensajes que exponen dirección, ruta o traza | Exactamente **0** sobre los **quince** códigos vivos **y** sobre el camino de ausencia de respuesta | `TC-31` | Inspección del traductor de condiciones y barrido de la microcopy | `Pendiente` |
| Tráfico de circuito durante la interacción con la escena | Exactamente **0**, y el texto viaja **una sola vez por trabajo** | `TC-33` | Conteo en la pestaña de red mientras se rota y se acerca | `Pendiente` |
| Instancias del visor no liberadas | Exactamente **0** tras **10** recorridos, con los dos movimientos prendidos | `TC-21`, puerta `PT-02` | Recuento de recursos vivos al final de los diez | `Pendiente` |
| Invocaciones al interior del bundle | Exactamente **0**: **6 de 6** funciones como única vía | `TC-32` | Inspección del árbol de fuentes | `Pendiente` |
| Estados de la línea de base demostrados | **74 de 74** estados, **11 de 11** superficies, **73 de 73** componentes y **24 de 24** rutas | **Las 61 filas de [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md)**, no un caso de verificación. Ver §7 | Recorrido de la matriz al cerrar cada etapa | `Sin verificar` |
| Advertencias de construcción | Exactamente **0** | Gate `QG-01`, **no un caso de verificación** | Etapa de construcción del flujo de publicación | `Pendiente` |

**El único valor rotulado [ASUNCIÓN] se cita con su rótulo y no se convierte en compromiso.** Es la asunción `A-4` del intake §22, y lo que rotula es **expresar la regla acumulativa como puerta con umbral del 100 %**, no la regla en sí. **`QG-04` bloquea igual**: la columna «Si el Product Owner la cambia» de `A-4` dice que «cambia la forma del gate, no su carácter bloqueante», y §17.6.P.6 lo llama «gate bloqueante y numérico» ([`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3.1).

**Dos de los catorce NFR no tienen caso de verificación propio y es correcto que no lo tengan**: uno lo cubre la matriz de sensado, que es un instrumento distinto y ya emitido, y el otro es una puerta del flujo de publicación. Inventarles un `TC-XX` habría duplicado un instrumento que ya existe.

**No hay NFR de cobertura de líneas ni de tiempo de respuesta**, y `05` §8 declara el fundamento de las dos ausencias. Esta matriz no les inventa fila.

## 4. Trazabilidad RN ↔ tests

Dieciséis filas, una por regla. **Este proyecto de código no hace cumplir ninguna regla de negocio** (`02` §5, `05` §10.3): lo que esta tabla declara es **qué hace esta pieza por cada una** y con qué se verifica, que es una cosa distinta. Las reglas se enuncian en `GeometriaFactory-Domain`.

| RN | Qué hace esta pieza por ella | Tests | Estado |
| --- | --- | --- | --- |
| RN-01 Administrador único y papeles fijos | Ofrece el aprovisionamiento **una sola vez** y deja de armarlo; no dibuja el destino del otro papel **ni deshabilitado** | `TC-01`, `TC-05` | `Pendiente` |
| RN-02 El correo del alumno es único | Presenta el rechazo del registro como error de operación, **sin revelar de quién es la cuenta** | `TC-02` | `Pendiente` |
| RN-03 Trabajo ajeno indistinguible de inexistente | Presenta los dos con **el mismo mensaje**, y **verifica la acotación forzando la solicitud** | `TC-26` | `Pendiente` |
| RN-04 Eliminación acotada al borrador | **No dibuja el control** fuera de `Borrador`, en lugar de dibujarlo inhabilitado; y fuerza la solicitud para verificar la acotación | `TC-15`, `TC-25` | `Pendiente` |
| RN-05 No se pasa a estado `Pendiente` con errores de validación | Presenta el estado resultante del envío con sus observaciones, y declara que **la previsualización dibuja y no verifica** | `TC-12`, `TC-13`, `TC-14` | `Pendiente` |
| RN-06 Cuenta `Pendiente` o `Bloqueado` sin acceso | Muestra el motivo de la situación al intentar ingresar, sin sesión | `TC-04` | `Pendiente` |
| RN-07 Baja con arrastre y confirmación escrita | Exige el correo escrito y **declara el arrastre antes del intento, en el mismo lugar donde se confirma** | `TC-09` | `Pendiente` |
| RN-08 Texto original conservado íntegro | Envía el texto **carácter por carácter** y lo muestra sin reescribirlo | `TC-11` | `Pendiente` |
| RN-09 Observación de error con posición y campo | Presenta cada observación con índice y campo, y **nunca mezcla las piezas no dibujadas con las observaciones** | `TC-14`, `TC-18` | `Pendiente` |
| RN-10 Desenlace exclusivo del administrador y terminalidad | No ofrece salida de los estados terminales, y aloja el bloque de decisión **sólo** para el administrador y sobre un trabajo en estado `Pendiente` | `TC-16`, `TC-25` | `Pendiente` |
| RN-11 El administrador no ve los borradores | **No los pide**: el listado se trae ya acotado, y pedir un borrador por dirección directa devuelve «no encontrado» | `TC-24`, `TC-26` | `Pendiente` |
| RN-12 El reseteo conserva la cuenta y sus trabajos | **Declara en la superficie, antes del intento, que no se pierde ningún trabajo** | `TC-10` | `Pendiente` |
| RN-13 Cambio forzado antes de toda otra capacidad | El **cuarto guardián**: la única ruta alcanzable es el cambio de la propia contraseña, **sin sesión de trabajo** | `TC-06`, `TC-07` | `Pendiente` |
| RN-14 La provisoria la produce el sistema | **Ningún campo de contraseña** en el formulario de reseteo, y la provisoria producida se le muestra al administrador | `TC-10` | `Pendiente` |
| RN-15 Resetear no exige cuenta habilitada | **Por ausencia**: la superficie no condiciona el reseteo al estado de la cuenta ni declara ningún motivo por ese concepto | `TC-10` | `Pendiente` |
| RN-16 Habilitar produce la provisoria | Muestra la provisoria **también al habilitar**, y por eso el primer ingreso recorre **el mismo formulario** que los otros dos cursos | `TC-06`, `TC-08` | `Pendiente` |

**Dieciséis de dieciséis reglas con al menos un caso de verificación.** La columna del medio nunca dice «hace cumplir»: dice qué ofrece, qué no dibuja o qué declara. **Cuando lo que hace es acotar, el caso de verificación fuerza la solicitud**, porque acotar no prueba nada por sí solo.

## 5. Trazabilidad restricción transversal ↔ tests

Trece filas, una por restricción de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §6. El componente que la sostiene es el que `05` §10.2 le asigna.

| RT | Qué exige, en una línea | Componente que la sostiene | Tests | Estado |
| --- | --- | --- | --- | --- |
| RT-01 | Ninguna llamada al servicio de datos se origina en el navegador | Cliente tipado | `TC-29`, `TC-30` | `Pendiente` |
| RT-02 | La credencial de sesión vive en el estado del circuito y no aparece en el navegador | Sesión y estado del circuito | `TC-03` | `Pendiente` |
| RT-03 | Ningún mensaje mostrado incluye dirección, archivo de datos ni traza | Traductor de condiciones | `TC-31` | `Pendiente` |
| RT-04 | El bundle se invoca exclusivamente por sus **seis** funciones | Anfitrión del visor | `TC-32` | `Pendiente` |
| RT-05 | La liberación de la instancia **no es opcional** | Anfitrión del visor | `TC-21` | `Pendiente` |
| RT-06 | La pieza pública **no guarda estado propio**: ni copia local, ni caché, ni réplica | Servicios de aplicación de front | `TC-03`, `TC-27` | `Pendiente` |
| RT-07 | La indisponibilidad es **estado degradado explícito**, y el vacío se distingue del fallo **por el tipo recibido** | Traductor de condiciones, Superficies | `TC-27`, `TC-28` | `Pendiente` |
| RT-08 | El texto original se envía carácter por carácter y no se reescribe | Servicios de aplicación de front | `TC-11` | `Pendiente` |
| RT-09 | Ninguna ruta del panel es accesible sin sesión, y el alumno no alcanza rutas de administrador. **Acota lo que se ofrece** | Armazón y encaminamiento | `TC-05`, `TC-07` | `Pendiente` |
| RT-10 | Sin tráfico de circuito durante la interacción, y el texto viaja **una sola vez por trabajo** | Anfitrión del visor | `TC-33` | `Pendiente` |
| RT-11 | Sin capacidad gráfica la escena no es soportada, **y el resto sigue disponible** | Anfitrión del visor, Superficies | `TC-23` | `Pendiente` |
| RT-12 | Una cuenta marcada llega sólo al cambio de su propia contraseña, **y sin sesión de trabajo** | Armazón y encaminamiento | `TC-07` | `Pendiente` |
| RT-13 | El anfitrión manda **dos valores de verdad** y **lee él la preferencia de movimiento reducido** | Anfitrión del visor | `TC-22` | `Pendiente` |

**Trece de trece con caso de verificación.** Las que exigen una **ausencia** —`RT-01`, `RT-04`, `RT-06`, `RT-10`— se verifican con umbral cero y en la condición declarada, nunca por no haberse observado lo contrario.

## 6. Cobertura por capa

La partición es por los **ocho** componentes de `05` §3.1, agrupados en las **tres** capas de presentación que [`ADR-04`](../05-Arquitectura-Tecnica/Adrs/ADR-04-Tres-Capas-De-Presentacion.md) declara. **No hay porcentaje de líneas que reportar**, y el motivo está en [`Estrategia-Testing.md`](Estrategia-Testing.md) §2: no hay proyecto de pruebas propio. Lo que se declara es qué ejerce cada componente.

| Capa | Componente | Casos de verificación que lo ejercen | Cobertura de líneas | Umbral |
| --- | --- | --- | --- | --- |
| 1 | Armazón y encaminamiento | `TC-05`, `TC-07`, `TC-35` | **No aplica** | Sin umbral: no hay proyecto de pruebas propio |
| 1 | Superficies | `TC-35`, y los `TC-XX` de cada superficie | **No aplica** | Ídem. Su cobertura contable son las **11 de 11** superficies sensadas |
| 1 | Representaciones reutilizadas | `TC-15` (fila de trabajo), `TC-18` (lista de observaciones), `TC-35` (sello de versión) | **No aplica** | Ídem |
| 2 | Servicios de aplicación de front | `TC-28`, `TC-30` | **No aplica** | Ídem |
| 2 | Sesión y estado del circuito | `TC-03`, `TC-27` | **No aplica** | Ídem |
| 2 | Traductor de condiciones a presentación | `TC-28`, `TC-31` | **No aplica** | Ídem. Su cobertura contable son los **15 de 15** códigos vivos más el camino de ausencia |
| 3 | Cliente tipado del servicio de datos | `TC-29`, `TC-30` | **No aplica** | Ídem |
| 3 | Anfitrión del visor | `TC-20`, `TC-21`, `TC-22`, `TC-32`, `TC-33` | **No aplica** | Ídem. Su cobertura contable son las **6 de 6** funciones de la fachada |

**«No aplica» y no «0 %».** Un cero afirmaría que hay una medición cuyo resultado es cero; acá **no hay medición porque no hay instrumento**, y la fuente lo declara así. Si en alguna etapa se agregan pruebas automatizadas de componentes, esta tabla gana su columna de umbral con su fila de control de cambios.

**Los ocho componentes tienen al menos un caso de verificación que los ejerce, y ninguno queda sin ejercer.**

## 7. Relación con la matriz de sensado de deriva

[`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) 1.2 **ya existía en esta carpeta antes de esta fase** y es un artefacto vigente de la categoría. Esta matriz de cobertura **no la duplica**: la cita.

| Instrumento | Qué responde | Unidad | Cuántas |
| --- | --- | --- | --- |
| [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) | ¿Lo construido se sigue pareciendo a lo que el Product Owner aprobó mirando? | Sonda `SD-XX`, con su **umbral de deriva** | **61** |
| Esta matriz, sobre [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) | ¿El sistema hace lo que las historias dicen? | Caso de verificación `TC-XX`, con su **criterio de aceptación** | **35** |

**Ningún `TC-XX` redefine el umbral de una sonda**, y ninguna sonda declara un criterio de aceptación. Cuando los dos miran el mismo elemento, el caso de verificación **cita** la sonda en su columna de upstream, que es lo que hace [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) §2 en cada ficha. La resolución del método de verificación de las 61 filas, por familia y con su etapa, está en [`Estrategia-Testing.md`](Estrategia-Testing.md) §8.1.

**Los 74 estados, las 11 superficies, los 73 componentes y las 24 rutas de la línea de base son cobertura de la matriz de sensado y no de esta matriz.** Por eso la fila correspondiente de §3 remite a ella y no a un `TC-XX`.

## 8. Huecos identificados

| Hueco | Consecuencia | Plan de remediación |
| --- | --- | --- |
| **Los elementos de interfaz de la capacidad `F-26` no tienen sonda** en la matriz de sensado, porque son posteriores a la aprobación de la maqueta y no tienen identificador en la línea de base | El reseteo desde el panel, su diálogo, la comunicación de la provisoria y el **tercer curso** de la superficie de credencial se verifican contra los criterios de aceptación de `CU-03` y `CU-04` —`TC-06`, `TC-07`, `TC-10`— pero **no tienen umbral de deriva** | La **iteración 5** de maqueta y la reemisión de la línea de base que la propia matriz declara pendiente en su §4. **No se les inventa sonda acá**: una sonda anclada en un identificador inexistente diría comparar contra algo que la línea de base no contiene |
| **No hay proyecto de pruebas propio** ni umbral de cobertura de líneas | Toda la verificación funcional es observada, y su reproducibilidad depende de que el guion esté escrito paso por paso | Es lo que la fuente declara (intake §17.6.P.6). La compensación son las **cinco** inspecciones estructurales con umbral cero y las 61 sondas. Si se agregan pruebas automatizadas de componentes, su umbral se fija en ese momento |
| **El valor rotulado [ASUNCIÓN]** —la forma de la puerta del guion— sigue sin confirmar | **Ninguna sobre el carácter del gate**: `QG-04` bloquea, porque `A-4` declara que un cambio del Product Owner cambia la forma y no el carácter. Lo que puede cambiar es **cómo** se mide. **La regla acumulativa rige**: no es asunción de nadie | El Product Owner sobre el intake §22, asunción `A-4`, antes de fijar la forma de la puerta en `09-Devops` |
| **No hay umbral de tiempo de respuesta** (`05` §11 `PA-04`) | Ningún caso de verificación puede declarar que una pantalla tardó demasiado. Lo que sí se verifica es que el indicador de espera aparezca cuando corresponde | El Product Owner, o esta categoría al fijar su guion de medición, **después** de `PT-01`. **No se inventa uno acá**, por el mismo criterio con el que `05` §8 no lo inventó |
| **El formato de intercambio y su configuración** no están fijados (`05` §11 `PA-03`) | `TC-31` verifica la traducción de los quince códigos, pero la forma en que llegan depende de una decisión de los dos extremos | La categoría 05 de `GeometriaFactory-Api`, como productor, con esta pieza como consumidor |
| **Ninguna fila `VER-XX`** en la matriz de sensado | No hay sondas de contrato de verificación | `10-Examples` no está emitida para este proyecto de código, y la propia matriz lo declara. Cuando se emita, la matriz gana esas filas |

## 9. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **`H-02`.** §3 y el hueco correspondiente de §8 declaraban a `QG-04` **condicionado**. Pasa a **bloqueante**, con la forma de la puerta como lo único sujeto a confirmación, según §17.6.P.6 y la fila `A-4` del intake §22. Ninguna fila de cobertura ni ningún umbral cambia. Corrige contra [`../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md`](../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md) 1.0 y contra el texto vivo del intake **1.20**. |
| 1.0 | 2026-08-11 | Emisión inicial. Declara las tres tablas obligatorias —**diez** filas de caso de uso con sus **treinta** historias, **catorce** de NFR y **dieciséis** de regla de negocio, ninguna agrupada—, más una cuarta de **trece** restricciones transversales, que es lo que este proyecto de código sí sostiene dado que **no hace cumplir ninguna regla**. Declara la cobertura por los **ocho** componentes con «No aplica» en lugar de un porcentaje inventado, y con la cobertura contable de cada uno donde la tiene. Su §7 declara la **relación con la matriz de sensado de deriva ya emitida**, sin duplicarla y sin redefinir ningún umbral. Cita el único valor rotulado **[ASUNCIÓN]** con su rótulo y declara su gate como condicionado, precisando que lo rotulado es la forma de la puerta y no la regla acumulativa. Declara **seis** huecos con su plan de remediación, incluida la ausencia de sondas para la capacidad `F-26` con el motivo por el que no se le inventan. |
