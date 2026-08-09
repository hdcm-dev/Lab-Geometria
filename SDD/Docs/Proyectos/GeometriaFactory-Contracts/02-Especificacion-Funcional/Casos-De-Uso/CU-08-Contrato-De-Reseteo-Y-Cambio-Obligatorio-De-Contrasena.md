# CU-08 — Contrato de reseteo y de cambio obligatorio de contraseña

**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** CU-08-Contrato-De-Reseteo-Y-Cambio-Obligatorio-De-Contrasena.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `01-Necesidades-Negocio/Necesidades-De-Negocio/NB-01-Control-De-Admision-Al-Laboratorio.md` §1, §5; `NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md` §1, §5; `00-Contexto/Vision-Producto.md` §9; `00-Contexto/Alcance-Producto.md` §4.1 y §5; `PRODUCT-INTAKE` 1.7 §4 (**F-26**, F-03), §4.1 (**RN-12**, **RN-13**, RN-07), §17.1.P.2 (**INV-09**), §7 (**CL-7** reescrito), §9 (**X-2 retirada**, X-1 vigente), §17.4 P.2, P.3, P.5 y P.8, §17.5 P.3 y P.5, §14 (**RA-01**, RA-03)
**Trazabilidad downstream:** `05-Arquitectura-Tecnica` y `06-Backlog-Tecnico` de este proyecto de código; `08-Calidad-Y-Pruebas`

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
- [17. Compatibilidad de versión pública](#17-compatibilidad-de-versión-pública)

---

## 1. Propósito

Declarar los tipos de transferencia del circuito que la capacidad **F-26** incorpora: el **reseteo de contraseña**, con el que el administrador le fija una contraseña provisoria a una cuenta de alumno, y el **cambio obligatorio**, con el que esa cuenta la reemplaza antes de poder hacer cualquier otra cosa.

Las dos mitades forman un solo contrato de uso porque forman un solo circuito: la primera **pone** una condición que sólo la segunda **levanta**, y ninguna de las dos se entiende sin la otra. El criterio de recorte de esta categoría es por familias de tipos de transferencia, y ésta es una familia nueva —solicitud de reseteo, resultado de reseteo y el código de error que desvía al cambio— que no existía en el ensamblado.

Lo que este contrato fija, sobre todo, es **qué se conserva**: el resultado del reseteo declara la situación de la cuenta y **no declara ningún campo por el que los trabajos del alumno se pierdan**. Es la contracara exacta de la solicitud de baja de CU-02 FA-01, que sí los arrastra.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Código de la pieza pública compilado contra el contrato | Primario | Arma la solicitud de reseteo cuando actúa para el administrador, y la de cambio de contraseña cuando actúa para el alumno reseteado |
| Código de la pieza de datos compilado contra el contrato | Sistema | Produce los resultados y el código de error que desvía al cambio, sobre los mismos tipos |
| Ensamblado de contratos | Sistema | Declara los campos de cada solicitud y la ausencia de todo campo que permita conservar o descartar trabajos |

No hay actor humano. El administrador y el alumno pertenecen a los casos de uso de `GeometriaFactory-Web`.

## 3. Precondiciones

- Los dos extremos están compilados contra la misma versión del ensamblado de contratos.
- El contrato ya declara los dos papeles fijos del producto y el conjunto cerrado de situaciones de cuenta de CU-02 §3, que **este contrato no amplía**: el reseteo no agrega una cuarta situación.
- El contrato ya declara la solicitud de cambio de contraseña de CU-02 FA-02, con sus dos campos —contraseña vigente y contraseña nueva—. **Este contrato la reutiliza y no la redeclara.**

## 4. Flujo principal

1. El código de la pieza pública, actuando para el administrador, arma la **solicitud de reseteo** con dos campos: identificador de la cuenta y contraseña provisoria elegida por el administrador.
2. El código de la pieza de datos responde con el **resultado del reseteo**, que declara la situación de la cuenta —la misma que tenía— y que la cuenta quedó con **cambio de contraseña pendiente**.
3. El administrador le comunica la contraseña provisoria al alumno **por fuera del producto**: no hay canal de correo y el contrato no declara ningún tipo que la transporte hacia el alumno.
4. El código de la pieza pública, actuando ahora para el alumno, canja credenciales por CU-01 con la contraseña provisoria.
5. El código de la pieza de datos **no produce respuesta de sesión**: produce el tipo de error de CU-06 con el código `CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO`.
6. El código de la pieza pública deriva a la **solicitud de cambio de contraseña** de CU-02 FA-02, con la provisoria como contraseña vigente y la nueva elegida por el alumno.
7. El código de la pieza de datos responde con el resultado del cambio, y a partir de ahí CU-01 vuelve a ser el camino de entrada, ya con respuesta de sesión.

## 5. Flujos alternativos

| Id | Disparador | Curso | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El administrador resetea una cuenta que ya tiene el cambio de contraseña pendiente | El contrato usa la misma solicitud de reseteo. El resultado declara la misma situación de cuenta y el mismo cambio pendiente: no hay campo que distinga un primer reseteo de un segundo, y no hace falta | El flujo continúa en el paso 3 |
| FA-02 | El alumno reseteado intenta cualquier otra operación del producto en lugar del cambio | La pieza de datos produce el tipo de error de CU-06 con el mismo código `CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO`, cualquiera sea la operación pedida. **El código es uno solo** y no se multiplica por operación: lo que el consumidor tiene que hacer es siempre lo mismo, derivar al cambio | El flujo vuelve al paso 6 |
| FA-03 | El administrador cambia su propia contraseña | No es este contrato: usa la solicitud de cambio de contraseña de CU-02 FA-02, con su vigente. **El reseteo no procede sobre la cuenta de administrador** y el contrato lo declara con código propio en §6 | Termina el flujo |

## 6. Excepciones y errores

| Código | Causa | Respuesta del contrato |
| --- | --- | --- |
| `CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO` | La cuenta tiene una contraseña provisoria sin cambiar y se pide el canje de credenciales o cualquier otra operación | Respuesta de error de CU-06 con texto neutro y su motivo. **No** se produce respuesta de sesión. Handoff al contrato de cambio de contraseña de CU-02 FA-02 |
| `CONTRATO_RESETEO_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` | Se pide el reseteo sobre la cuenta con papel `Administrador` | Respuesta de error de CU-06 con texto neutro. Terminación controlada: el contrato no ofrece camino alternativo, y el cambio de la propia contraseña es CU-02 FA-02 |
| `CONTRATO_CAMPO_REQUERIDO_AUSENTE` | La solicitud de reseteo llega sin identificador de cuenta o sin contraseña provisoria | Respuesta de error de CU-06 que nombra el campo ausente. Recuperación: el código de la pieza pública corrige y reintenta |
| `CONTRATO_CREDENCIAL_INVALIDA` | El cambio llega sin la contraseña vigente o con una que no corresponde a la provisoria | Respuesta de error de CU-06 con texto neutro. Terminación controlada, y **el cambio pendiente sigue puesto** |
| `CONTRATO_SERVICIO_NO_DISPONIBLE` | La pieza de datos no responde | Respuesta de error de CU-06 con texto neutro y sin dirección del servicio que falló. Handoff al estado degradado |

**Dos códigos nuevos y tres reutilizados.** El conjunto cerrado de CU-06 pasa de catorce a **dieciséis**; los otros tres ya existían con la misma causa. No se declara ninguna **señal declarada que no es error**: las tres del ensamblado siguen siendo las de CU-03 §6.1, CU-04 §6.1 y CU-05 §6.1, y este contrato no agrega ninguna, porque el cambio pendiente **sí** impide la operación pedida y por lo tanto es un error transportado y no una señal.

## 7. Postcondiciones

- En caso de éxito del reseteo: el código de la pieza pública tiene la situación de la cuenta **sin cambio** y la declaración de cambio de contraseña pendiente. Ningún campo del resultado transporta la contraseña almacenada, ni la provisoria derivada, ni ninguna referencia a los trabajos de la cuenta.
- En caso de éxito del cambio: el código de la pieza pública tiene el resultado del cambio y la cuenta vuelve a canjear credenciales con normalidad por CU-01.
- En caso de fallo: el código de la pieza pública tiene un tipo de error de CU-06 y **el cambio pendiente queda como estaba**. El contrato no deja estado parcial, porque los tipos de transferencia no tienen comportamiento.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | El tipo de solicitud de reseteo del contrato | Se inspecciona su superficie pública | Declara exactamente dos campos —identificador de cuenta y contraseña provisoria— y **0 campos** que permitan conservar, descartar o referenciar los trabajos de la cuenta: el reseteo no puede expresarse como una baja |
| CA-02 | El tipo de resultado del reseteo | Se inspecciona su superficie pública | Declara la situación de la cuenta y el cambio de contraseña pendiente, y **0 campos** con la contraseña almacenada, con la provisoria derivada o con una dirección de servicio interno (`RT-01`) |
| CA-03 | Una cuenta de alumno habilitada con 3 trabajos, uno de ellos en estado `Finalizado` | El administrador la resetea | El resultado devuelve la situación `Habilitada`, el cambio pendiente, y el listado de trabajos de CU-04 sigue trayendo los **3** con sus mismos estados |
| CA-04 | Una cuenta con cambio de contraseña pendiente | El código de la pieza pública canja sus credenciales con la provisoria | La respuesta es el tipo de error de CU-06 con código `CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO`; **no** se produce respuesta de sesión, y el tipo de respuesta de sesión sigue declarando cuatro campos, sin ninguno agregado para este caso |
| CA-05 | La misma cuenta con cambio pendiente | El código de la pieza pública pide el listado de trabajos, el detalle de un trabajo y el envío de uno nuevo | Las 3 respuestas son el tipo de error de CU-06 con el **mismo** código `CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO`: 1 código para todas las operaciones, y 0 trabajos leídos o escritos |
| CA-06 | La misma cuenta | El alumno cambia la contraseña por la solicitud de CU-02 FA-02, con la provisoria como vigente | El resultado es exitoso y el canje siguiente produce respuesta de sesión; el cambio pendiente ya no se declara |
| CA-07 | La cuenta con papel `Administrador` | Se arma la solicitud de reseteo con su identificador | La respuesta es el tipo de error de CU-06 con código `CONTRATO_RESETEO_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` y la cuenta no se resetea |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-01, NB-02 |
| Reglas de negocio aplicables | Ninguna propia: este proyecto de código no las redacta. Aplican [`RN-12`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-12-Reseteo-Conserva-La-Cuenta-Y-Sus-Trabajos.md) —el reseteo conserva la cuenta y sus trabajos, que es lo que CA-01 y CA-03 verifican—, [`RN-13`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-13-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md) —que sostiene el código `CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO` y CA-05—, [`RN-01`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-01-Administrador-Unico-Y-Papeles-Fijos.md) —por el cierre sobre la cuenta de administrador— y [`RN-07`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-07-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md) **por contraste**: es la regla que este contrato existe para no disparar. Las cuatro de `GeometriaFactory-Domain`. Ver `Especificacion-Funcional.md` §5 |
| Historias de usuario a generar en 06 | US-21 tipos de reseteo de contraseña con conservación de trabajos; US-22 desvío al cambio obligatorio con código propio |
| Componentes esperados en 05 | Familia de tipos de transferencia de reseteo y cambio obligatorio del ensamblado de contratos |
| Tests previstos en 08 | Pruebas de integración del circuito de punta a punta —reseteo, canje que devuelve el código de desvío, cambio, canje exitoso—; prueba de que los trabajos sobreviven al reseteo en los cuatro estados; prueba de que las operaciones de lectura y de escritura devuelven el mismo código con el cambio pendiente; e inspección de superficie pública para CA-01 y CA-02 |

## 10. Notas y supuestos

- **RA-01 no se afloja acá, y conviene decirlo porque es un circuito de credenciales.** Ningún JavaScript del navegador invoca la API: la solicitud de reseteo la arma el **servidor** de la pieza pública y viaja servidor a servidor, exactamente igual que el canje de CU-01 y que la baja de CU-02. El navegador nunca alcanza la API (`PRODUCT-INTAKE` §14 RA-01). Un formulario de reseteo que llamara por su cuenta a la pieza de datos reabriría de una vez el contenido mixto, el CORS y la exposición de la dirección del servidor propio, que es lo que RA-01 sostiene.
- **La contraseña provisoria viaja en claro dentro de la solicitud de reseteo**, del mismo modo que la contraseña presentada viaja en la solicitud de canje de CU-01 y la elegida en la de establecimiento de CU-02. Lo que `RT-01` prohíbe es transportar la **contraseña almacenada** —su forma derivada— y ninguna respuesta de este contrato la lleva. La derivación es de `GeometriaFactory-Infrastructure`.
- **El contrato no transporta la provisoria hacia el alumno.** El administrador se la comunica por fuera del producto: no hay canal de correo, la exclusión **X-1** sigue vigente y la que se retiró es **X-2**. Ningún tipo de este ensamblado declara un enlace de recuperación.
- **Un solo código para todas las operaciones bloqueadas**, y es una decisión de contrato. Multiplicarlo por operación —uno para el listado, otro para el envío— daría al consumidor información que no usa: el trabajo que le queda es siempre el mismo, derivar al cambio. Es el mismo criterio con el que `CONTRATO_TRABAJO_NO_ENCONTRADO` cubre tres causas distintas en CU-06.
- **Decisión derivada: el desvío viaja como respuesta de error y no como campo de la respuesta de sesión.** Ninguna fuente declara la forma. Se adopta la del precedente exacto del ensamblado: `CONTRATO_CONTRASENA_NO_ESTABLECIDA`, que CU-01 §10 fundamenta en que `PRODUCT-INTAKE` §17.5 P.5 enumera **cuatro** reclamos de la credencial de sesión y ningún quinto dato. Una respuesta de sesión con una marca sería un quinto dato, y además emitiría una credencial de sesión a una cuenta que por **INV-09** no ejerce ninguna capacidad. **Tensión declarada**: RN-13 dice que la cuenta reseteada «ingresa, y lo único que puede hacer es cambiar la contraseña». Este contrato lo modela sin sesión, y la diferencia es observable; está elevada como punto abierto en `GeometriaFactory-Domain` `Especificacion-Funcional.md` §9.
- La forma de los puntos de acceso —rutas y verbos— pertenece a `GeometriaFactory-Api` (`PRODUCT-INTAKE` §17.5 P.3).

## 11. Control de cambios

| Versión | Fecha | Descripción | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial, por la capacidad **F-26** que `PRODUCT-INTAKE` 1.7 incorpora como `Must Have`, con las reglas **RN-12** y **RN-13**, el invariante **INV-09**, el retiro de la exclusión **X-2** y la reescritura del caso límite **CL-7**. Declara la familia de tipos del reseteo y del cambio obligatorio, con la solicitud de cambio de contraseña de CU-02 FA-02 **reutilizada y no redeclarada**; dos códigos de error nuevos que llevan el conjunto cerrado de CU-06 de catorce a **dieciséis**, y tres reutilizados; siete criterios de aceptación, con CA-01 y CA-03 verificando que **los trabajos se conservan** y CA-05 que un solo código cubre todas las operaciones bloqueadas. Deja declaradas la vigencia de **RA-01** sobre este circuito y una **decisión derivada** con su tensión: el desvío viaja como respuesta de error y no como campo de la respuesta de sesión, por el precedente de `CONTRATO_CONTRASENA_NO_ESTABLECIDA`, aunque RN-13 hable de «ingresa». | Analista Funcional + API Designer (AG-02) |

## 17. Compatibilidad de versión pública

Sección opcional de `Rules-Especificacion-Funcional.md` §4.3, que la numera **§17** y la reserva para `library`. Se conserva su número de la regla, aunque deje un hueco tras §11, para que un lector automatizado que busque §17 en cualquier caso de uso del producto encuentre siempre lo mismo.

- **Esta emisión es un cambio incompatible** y obliga al despliegue conjunto de las dos piezas desplegables (`RT-06`): entran dos códigos al conjunto cerrado de CU-06 y una familia de tipos nueva.
- Agregar a la solicitud de reseteo cualquier campo que alcance a los trabajos de la cuenta se **rechaza aunque compile**: contradice RN-12 y el criterio CA-01.
- Agregar el cambio de contraseña pendiente como campo de la respuesta de sesión de CU-01 se rechaza aunque compile: contradice CA-04 y el fundamento de §10.
- Agregar un campo opcional al resultado del reseteo es compatible, siempre que no viole CA-02.
