# Especificación funcional — GeometriaFactory-Contracts

**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** Especificacion-Funcional.md
**Versión:** 1.7
**Estado:** Aprobado
**Fecha:** 2026-08-12
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `00-Contexto/Vision-Producto.md` §3, §7 (R-03, R-04), §9; `00-Contexto/Alcance-Producto.md` §2.2, §4.1, §4.2, §5, §8; `01-Necesidades-Negocio/Necesidades-Negocio.md` 1.1 §2, §4, §5.3 y las nueve `NB-XX`; `PRODUCT-INTAKE` **1.14** §4 (con **F-26**), §4.1 (las **trece** reglas, con RN-08012 y RN-08013) y §4.2 (modelo de estados), §7 (**CL-7** reescrito), §9 (**X-2 retirada**), §17.1.P.2 (**INV-09**), §17.4 íntegro (P.1 a P.12), §13 y §14 (composición, contratos entre proyectos de código y reglas RA-01 a RA-03), §17.5 P.3 y P.5 (qué existe del otro lado del contrato), §4, §6, §7 y §20 (escenarios de instancia)
**Trazabilidad downstream:** `05-Arquitectura-Tecnica` y `06-Backlog-Tecnico` de este proyecto de código; `08-Calidad-Y-Pruebas`, donde vive su verificación

---

## Tabla de contenido

- [1. Propósito y alcance de esta especificación](#1-propósito-y-alcance-de-esta-especificación)
- [2. Qué decide este proyecto de código](#2-qué-decide-este-proyecto-de-código)
- [3. Catálogo de casos de uso](#3-catálogo-de-casos-de-uso)
  - [3.1 Criterio de recorte](#31-criterio-de-recorte)
  - [3.2 Numeración local](#32-numeración-local)
- [4. Matriz NB→CU→RN→US](#4-matriz-nbcurnus)
  - [4.1 Cobertura inversa: de NB a CU](#41-cobertura-inversa-de-nb-a-cu)
  - [4.2 Correspondencia con la previsión de casos de uso de 01](#42-correspondencia-con-la-previsión-de-casos-de-uso-de-01)
- [5. Por qué la columna RN está vacía](#5-por-qué-la-columna-rn-está-vacía)
- [6. Restricciones transversales del contrato](#6-restricciones-transversales-del-contrato)
- [7. Glosario](#7-glosario)
- [8. Artefactos de esta categoría que se omiten](#8-artefactos-de-esta-categoría-que-se-omiten)
- [9. Control de cambios](#9-control-de-cambios)

---

## 1. Propósito y alcance de esta especificación

`GeometriaFactory-Contracts` es el ensamblado de tipos de transferencia del producto. Es el contrato compartido entre las dos piezas desplegables y el único tipo que atraviesa la frontera de servicio: es lo que impide que la pieza pública conozca el dominio (`PRODUCT-INTAKE` §13 y §14). No tiene dependencias: es nivel 0 del orden topológico y, en particular, **no referencia `GeometriaFactory-Domain`**, ausencia que el intake declara como quality gate bloqueante en §17.4 P.8.

Por eso cada caso de uso de esta especificación **describe un contrato de uso** y no un flujo de pantallas, y su actor es el código que compila contra el ensamblado: el de la pieza de datos de un lado, el de la pieza pública del otro. No hay actor humano en ningún caso de uso de esta categoría: las personas del producto aparecen en la especificación funcional de `GeometriaFactory-Web`.

Lo que esta especificación **no** decide: la forma de los puntos de acceso del servicio, que pertenece a `GeometriaFactory-Api`; las invariantes del dominio, que pertenecen a `GeometriaFactory-Domain`; la arquitectura y los ADR, que pertenecen a 05; y las pruebas, que pertenecen a 08.

## 2. Qué decide este proyecto de código

La decisión central es **qué se expone y qué no**. El intake la declara en §17.4 P.5: ningún tipo de transferencia incluye el hash de contraseña, la clave de firma ni ninguna dirección de servicio interno; la respuesta de error lleva texto neutro y, cuando corresponde, índice de figura y campo señalado, nunca la dirección del servicio que falló. Es la regla de arquitectura RA-03, una de las tres de nivel producto de `PRODUCT-INTAKE` §14.

Tres decisiones más, tomadas aguas arriba y que esta especificación respeta sin reabrir:

1. **El texto original del trabajo viaja como cadena, sin interpretarse en el contrato** (`PRODUCT-INTAKE` §17.4 P.11, decisión 2). La interpretación es de la pieza de datos y el dibujo es del bundle del visor.
2. **No hay generación de clientes a partir de una descripción formal del servicio** (§17.4 P.2), descartada por costo de cadena de herramientas frente a dos consumidores compilados juntos. Ninguna sección de esta especificación la presupone.
3. **Política de cambios incompatibles** (§17.4 P.3): como los dos extremos compilan contra el mismo ensamblado, un cambio incompatible rompe la compilación antes que el tiempo de ejecución. La regla operativa es el despliegue conjunto de las dos piezas desplegables. No hay versionado de rutas porque no hay consumidores de terceros.

## 3. Catálogo de casos de uso

| CU | Contrato de uso que describe | NB que sostiene | Estado | Enlace |
| --- | --- | --- | --- | --- |
| CU-08001 | Canje de credenciales y respuesta de sesión, sin exponer contraseña almacenada ni clave de firma | NB-00002, NB-00001 | Propuesto | [CU-08001](Casos-De-Uso/CU-08001-Contrato-De-Canje-De-Credenciales-Y-Sesion.md) |
| CU-08002 | Registro, credencial, listado de cuentas y cambio de situación de cuenta, con la confirmación escrita de la baja | NB-00001, NB-00002 | Propuesto | [CU-08002](Casos-De-Uso/CU-08002-Contrato-De-Administracion-De-Cuentas.md) |
| CU-08003 | Envío, eliminación y estado del trabajo, con el texto original como cadena no interpretada. El envío es la única acción de guardado y la solicitud de eliminación es única para los dos papeles | NB-00003, NB-00004 | Propuesto | [CU-08003](Casos-De-Uso/CU-08003-Contrato-De-Carga-Y-Edicion-Del-Trabajo.md) |
| CU-08004 | Listado de trabajos como proyección sin texto original ni componentes, con alcance distinto según el papel | NB-00003, NB-00007, NB-00009 | Propuesto | [CU-08004](Casos-De-Uso/CU-08004-Contrato-De-Listado-De-Trabajos.md) |
| CU-08005 | Detalle del trabajo interpretado: piezas, componentes, observaciones con severidad y par de valores, y comentario del administrador | NB-00004, NB-00005, NB-00006, NB-00007, NB-00009 | Propuesto | [CU-08005](Casos-De-Uso/CU-08005-Contrato-De-Detalle-Del-Trabajo-Interpretado.md) |
| CU-08006 | Respuesta de error neutra, transversal a los demás, con el conjunto cerrado de **diecisiete** códigos | NB-00004, NB-00008, NB-00009, NB-00002 | Propuesto | [CU-08006](Casos-De-Uso/CU-08006-Contrato-De-Respuesta-De-Error.md) |
| CU-08007 | Desenlace de la revisión: aprobar o rechazar un trabajo en estado `Pendiente`, con comentario opcional | NB-00009, NB-00007 | Propuesto | [CU-08007](Casos-De-Uso/CU-08007-Contrato-De-Desenlace-De-La-Revision.md) |
| CU-08008 | Reseteo de contraseña por el administrador y cambio obligatorio por la propia cuenta, con el resultado que **conserva** la cuenta y todos sus trabajos | NB-00001, NB-00002 | Propuesto | [CU-08008](Casos-De-Uso/CU-08008-Contrato-De-Reseteo-Y-Cambio-Obligatorio-De-Contrasena.md) |

Ocho casos de uso, sobre el mínimo de cinco que `Rules-Especificacion-Funcional.md` §2.2 fija para `library`.

### 3.1 Criterio de recorte

El recorte no sigue las capacidades `F-XX` del intake una por una, porque el ensamblado no implementa capacidades: transporta datos. Sigue las **familias de tipos de transferencia** que atraviesan la frontera de servicio, que es la unidad con la que un cambio incompatible se propaga.

| Decisión | Fundamento |
| --- | --- |
| Se separó CU-08001 de CU-08002 | El canje de credenciales es la superficie donde vive la prohibición de exponer contraseña almacenada y clave de firma, y se lee sola. Fusionarla con el ciclo de vida de la cuenta habría escondido esa restricción dentro de un caso de uso de administración |
| Se separó CU-08004 de CU-08005 | Es la separación que da sentido al requisito estructural de `PRODUCT-INTAKE` §17.4 P.10: la proyección de listado existe precisamente para **no** ser el detalle. Con un único caso de uso de lectura, la restricción no tendría dónde verificarse |
| Se fusionó el listado propio del alumno con el de la comisión en CU-08004 | Es el mismo tipo de transferencia con distinto alcance de datos. Dos casos de uso habrían duplicado la superficie sin agregar ninguna decisión de contrato |
| Se absorbió el paso a estado `Pendiente` dentro de CU-08003 | Con el envío como acción única de guardado no hay una operación separada que llevar a un caso de uso propio: el estado es una salida del mismo envío. Un caso de uso aparte habría sido un sub-flujo, que es el anti-patrón de `Rules-Especificacion-Funcional.md` §5.2 |
| Se emitió CU-08007 como caso de uso propio | El desenlace es una **familia de tipos nueva** que no existía: solicitud con conjunto cerrado de decisión y comentario opcional, resultado con estado terminal, y dos códigos de error propios. Absorberlo en CU-08003 habría mezclado la transición que ejerce el administrador con la acción de guardado del alumno, que tienen actor, precondición y regla de dominio distintos |
| Se fusionaron aprobar y rechazar en CU-08007 | Comparten tipo de solicitud, resultado, precondición, errores y regla de dominio: se distinguen sólo por el valor de un campo de conjunto cerrado. Dos casos de uso habrían duplicado la superficie sin declarar ninguna decisión de contrato distinta |
| Se absorbió la eliminación por el administrador en CU-08003, FA-04 | Reutiliza **el mismo tipo** de solicitud de eliminación que ya declaraba el alumno; lo que difiere es la regla que lo acota, y las reglas viven en `GeometriaFactory-Domain`. Un caso de uso nuevo habría declarado la misma superficie dos veces, que es lo que el criterio de familias de tipos evita |
| Se emitió CU-08008 como caso de uso propio | El reseteo es una **familia de tipos nueva**: solicitud con el identificador de cuenta y nada más, resultado que declara la situación conservada, el cambio pendiente y **la contraseña provisoria que el sistema produce**, y tres códigos de error propios. Es el mismo criterio con el que se emitió CU-08007. Absorberlo en CU-08002 habría puesto en el mismo contrato de uso la solicitud que **elimina** la cuenta y todos sus trabajos y la que los **conserva**, que es exactamente la confusión que F-26 viene a cerrar |
| Se reutilizó en CU-08008 la solicitud de cambio de contraseña de CU-08002 | El cambio obligatorio usa **el mismo tipo** que el cambio voluntario, con la provisoria como contraseña vigente; lo que difiere es la precondición, y las precondiciones son reglas de `GeometriaFactory-Domain`. Redeclararlo habría declarado la misma superficie dos veces, que es el criterio con el que CU-08003 FA-04 ya absorbe la eliminación por el administrador |
| Se hizo transversal CU-08006 | Los otros siete casos de uso comparten sus caminos de error. Es el caso de uso transversal de manejo de errores que sugiere §5.2 de las reglas, y además concentra en un solo tipo la superficie donde RA-03 se puede violar |

### 3.2 Numeración local

Los identificadores `CU-XX` y `US-XX` de esta sección son **locales a `GeometriaFactory-Contracts`**. `Necesidades-Negocio.md` §5.3 prevé veintisiete casos de uso `CU-08001` a `CU-27` a nivel producto; esa previsión se reparte entre las especificaciones funcionales de los siete proyectos de código, cada una con su propia numeración contigua desde `CU-08001`, porque la categoría 02 es de nivel proyecto de código (`Rules-Especificacion-Funcional.md`, cabecera). La correspondencia con la previsión de 01 se lee en §4.2 y no obliga a renumerar nada.

La numeración de esta sección es contigua de `CU-08001` a `CU-08008`, sin huecos.

## 4. Matriz NB→CU→RN→US

| NB | CU | RN | US a generar en 06 |
| --- | --- | --- | --- |
| NB-00001 | CU-08001, CU-08002, CU-08008 | — | US-08001, US-08002, US-08003, US-08004, US-08005, US-08021 |
| NB-00002 | CU-08001, CU-08002, CU-08006, CU-08008 | — | US-08001, US-08002, US-08003, US-08004, US-08005, US-08014, US-08021, US-08022 |
| NB-00003 | CU-08003, CU-08004 | — | US-08006, US-08007, US-08008, US-08009, US-08019 |
| NB-00004 | CU-08003, CU-08005, CU-08006 | — | US-08006, US-08007, US-08011, US-08012, US-08013, US-08014, US-08015, US-08016, US-08019 |
| NB-00005 | CU-08005 | — | US-08011, US-08013 |
| NB-00006 | CU-08005 | — | US-08011, US-08012 |
| NB-00007 | CU-08004, CU-08005, CU-08007 | — | US-08008, US-08009, US-08010, US-08011, US-08017 |
| NB-00008 | CU-08006 | — | US-08014, US-08016 |
| NB-00009 | CU-08007, CU-08005, CU-08004, CU-08006, CU-08003 | — | US-08017, US-08018, US-08019, US-08020 |

Las veintidós historias de usuario previstas son una previsión de esta categoría; las confirma la categoría 06 al redactarlas.

### 4.1 Cobertura inversa: de NB a CU

Ninguna necesidad queda sin caso de uso en esta categoría y ningún caso de uso queda huérfano. Ahora bien, este proyecto de código no sostiene cada necesidad en la misma medida, y declararlo es más útil que una fila uniforme.

| NB | Grado en que este proyecto de código la sostiene | Qué queda en otro proyecto de código |
| --- | --- | --- |
| NB-00001 | Parcial: transporta el listado de cuentas, la orden de cambio de situación, la confirmación escrita de la baja y la solicitud de reseteo de contraseña, cuyo resultado **no declara ningún campo por el que los trabajos se pierdan** | La unicidad del administrador y el arrastre de trabajos en la baja son invariantes de `GeometriaFactory-Domain`; el panel es de `GeometriaFactory-Web` |
| NB-00002 | Parcial: declara que ninguna forma de la contraseña almacenada cruza la frontera, que la situación de la cuenta viaja explicada y que el cambio de contraseña pendiente viaja como **respuesta de error con código propio** y no como campo de la respuesta de sesión | La derivación de clave es de `GeometriaFactory-Infrastructure`; el circuito de alta es de `GeometriaFactory-Web` |
| NB-00003 | Sostenida en lo que le corresponde: los tipos con los que un trabajo se envía, se elimina y se lista, con identificador, fecha y el conjunto cerrado de cuatro estados | La persistencia es de `GeometriaFactory-Infrastructure`; las transiciones de estado son de `GeometriaFactory-Domain` |
| NB-00004 | Parcial y decisiva: el texto original viaja íntegro como cadena, y el error viaja con índice de figura y campo señalado | La tolerancia de formato y la interpretación son de `GeometriaFactory-Infrastructure` y de `GeometriaFactory-Domain` |
| NB-00005 | Parcial: la observación transporta severidad y el par de valor declarado y valor derivado en campos propios | El recálculo y la tolerancia de comparación son del dominio y de su implementación |
| NB-00006 | Parcial: el detalle transporta las piezas y sus componentes, que es lo que el bundle del visor dibuja, y el texto original, que es lo que el árbol despliega | El dibujo es de `GeometriaFactory-Visor`; la integración y el árbol son de `GeometriaFactory-Web` |
| NB-00007 | Parcial: la proyección de listado trae los datos para agrupar y filtrar por alumno y excluye los trabajos en estado `Borrador` del alcance del administrador, y el detalle es el mismo para los dos papeles | El listado y su organización son de `GeometriaFactory-Web`; el alcance de lo que cada papel ve es del dominio |
| NB-00009 | Parcial y decisiva: transporta el desenlace con su conjunto cerrado de dos valores, el estado terminal alcanzado y el comentario opcional, y declara por construcción que el comentario no es una observación | La transición y su exclusividad son invariantes de `GeometriaFactory-Domain`; el panel de revisión es de `GeometriaFactory-Web` |
| NB-00008 | Marginal, y sólo por una arista: la respuesta de error neutra es lo que hace que la indisponibilidad se presente como estado degradado explícito y sin revelar la dirección del servicio que falló | La verificación de acceso desde la red de la facultad y el despliegue son de 09; la presentación del estado degradado es de `GeometriaFactory-Web` |

### 4.2 Correspondencia con la previsión de casos de uso de 01

`Necesidades-Negocio.md` §5.3 previó veintisiete casos de uso a nivel producto y declaró que «la numeración es una previsión de esta categoría y la confirma la categoría 02 al redactarlos». Ésta es la confirmación que le corresponde a `GeometriaFactory-Contracts`. Para distinguir las dos series homónimas, la previsión de 01 se escribe acá con el prefijo **`P·`** —`P·CU-01` a `P·CU-27`— y los identificadores sin prefijo son siempre los locales de esta sección.

Cada `P·CU-XX` es un flujo de producto completo. Este proyecto de código no lo realiza: aporta el contrato de uso con el que sus datos cruzan la frontera de servicio. Por eso la correspondencia es de muchos a uno y ninguna previsión se «absorbe» entera acá.

| Previsión de 01 | NB | CU local que aporta su contrato | Qué queda fuera de este proyecto de código |
| --- | --- | --- | --- |
| `P·CU-01` configurar la cuenta de administrador en el primer arranque | NB-00001 | CU-08002, FA-03 | El flujo y su pantalla |
| `P·CU-02` habilitar, bloquear y rehabilitar una cuenta de alumno | NB-00001 | CU-08002, flujo principal pasos 5 y 6 | El panel del administrador y la transición admitida |
| `P·CU-03` dar de baja una cuenta con confirmación escrita | NB-00001 | CU-08002, FA-01 | El arrastre de trabajos, que es invariante de dominio |
| `P·CU-04` registrar una cuenta de alumno | NB-00002 | CU-08002, flujo principal pasos 1 y 2 | El formulario y la unicidad del correo |
| `P·CU-05` establecer la contraseña en el primer ingreso, **con la provisoria como credencial vigente** | NB-00002 | CU-08002 FA-02 y flujo principal paso 8; CU-08001 FA-02 y CU-08006 para el desvío; CU-08008 por el circuito de cambio obligatorio, que es el mismo | La derivación de la clave. **Desde `PRODUCT-INTAKE` 1.13 no hay tipo de establecimiento anónimo**: se usa la solicitud de cambio de contraseña (RN-08016) |
| `P·CU-06` iniciar y cerrar sesión | NB-00002 | CU-08001 completo | El ciclo de vida de la sesión en la pieza pública |
| `P·CU-07` cambiar la contraseña exigiendo la vigente | NB-00002 | CU-08002, FA-02 | La verificación de la contraseña vigente |
| `P·CU-08` cargar un trabajo | NB-00003 | CU-08003, flujo principal | La pantalla de carga y la persistencia |
| `P·CU-09` reeditar un trabajo en estado `Borrador` | NB-00003 | CU-08003, flujo principal paso 5 | La acotación al estado, que es invariante de dominio |
| `P·CU-10` eliminar un trabajo propio en estado `Borrador` | NB-00003 | CU-08003, FA-02 | La acotación al estado y a la pertenencia |
| `P·CU-11` listar los trabajos propios | NB-00003 | CU-08004, FA-01 | La pantalla del listado |
| `P·CU-12` interpretar el texto y reportar el error con figura y campo | NB-00004 | CU-08005 y CU-08006 | Toda la interpretación y la tolerancia de claves |
| `P·CU-13` resolver el estado del trabajo según el resultado de la interpretación | NB-00004 | CU-08003, flujo principal paso 4 y FA-01 | La decisión del estado, que es del dominio |
| `P·CU-23` enviar un trabajo | NB-00004 | CU-08003, flujo principal completo | La acción y su pantalla |
| `P·CU-14` verificar los valores declarados contra los derivados | NB-00005 | CU-08005, §4 paso 4 | El recálculo y la tolerancia de comparación |
| `P·CU-15` previsualizar el trabajo en tres dimensiones | NB-00006 | CU-08005, la colección de piezas y componentes | El dibujo y la integración del bundle |
| `P·CU-16` explorar la estructura como árbol colapsable | NB-00006 | CU-08005, el texto original del detalle | La forma del árbol, que es presentación |
| `P·CU-17` sincronizar el árbol y la escena por índice de pieza | NB-00006 | CU-08005, el índice de figura de cada pieza | La sincronización, que es interacción |
| `P·CU-18` listar los trabajos de la comisión sin los que están en estado `Borrador` | NB-00007 | CU-08004, flujo principal paso 3, FA-02 y CA-06 | La agrupación y el filtro en pantalla |
| `P·CU-19` abrir un trabajo de un alumno para revisarlo | NB-00007 | CU-08005, FA-01 | La pantalla de revisión |
| `P·CU-20` consultar el panel de resumen por alumno y por estado | NB-00007 | CU-08004, FA-04 | El panel, y su prioridad menor de etapa `i` |
| `P·CU-21` verificar el acceso al laboratorio desde la red de la facultad | NB-00008 | **Ninguno.** No toca el contrato | Verificación de campo y despliegue: 09 |
| `P·CU-22` presentar el estado degradado cuando el servicio de datos no responde | NB-00008 | CU-08006, FA-02 | La presentación del estado degradado |
| `P·CU-24` aprobar un trabajo en estado `Pendiente`, con comentario opcional | NB-00009 | CU-08007, flujo principal | El panel de revisión y la transición de dominio |
| `P·CU-25` rechazar un trabajo en estado `Pendiente`, con comentario opcional | NB-00009 | CU-08007, FA-01 | Ídem, con el otro valor del desenlace |
| `P·CU-26` consultar el desenlace y el comentario del trabajo propio | NB-00009 | CU-08005, FA-04 y bloque de comentario; CU-08004 para el estado en el listado | La pantalla del alumno |
| `P·CU-27` eliminar un trabajo desde el panel del administrador | NB-00009 | CU-08003, FA-04 | La acotación por visibilidad, que es invariante de dominio |
| **Sin previsión en 01** · resetear la contraseña de un alumno y obligarlo a cambiarla | NB-00001, NB-00002 | CU-08008 completo | El panel del administrador, la pantalla de cambio obligatorio y el encaminamiento de toda otra ruta |

Veintiséis de las veintisiete previsiones tienen contrato en esta sección; `P·CU-21` es la única que este proyecto de código no toca. **La última fila no corresponde a ninguna previsión de 01, y es correcto que así sea**: la capacidad **F-26** entró con `PRODUCT-INTAKE` 1.7, después de que `Necesidades-Negocio.md` §5.3 emitiera su previsión de veintisiete. No se le asigna un `P·CU-28` acá, porque esa serie es de la categoría 01 y esta sección la confirma, no la extiende. Las cinco previsiones nuevas de 01 —`P·CU-23` de NB-00004 y `P·CU-24` a `P·CU-27` de NB-00009— quedan cubiertas por CU-08003, CU-08004, CU-08005 y CU-08007. La correspondencia de las veintidós con el resto de los proyectos de código no se decide acá: cada uno abre su serie local y confirma su parte, del mismo modo que ésta.

## 5. Por qué la columna RN está vacía

La columna de reglas de negocio de §4 está vacía en las nueve filas, y eso es correcto en este proyecto de código. El motivo no es que el producto no tenga reglas: las tiene, y son varias —unicidad del administrador, transiciones de estado del trabajo, verificación de pertenencia, conservación íntegra del texto original, carácter no bloqueante de la advertencia—. El motivo es **dónde viven**.

`GeometriaFactory-Contracts` es tipos de transferencia planos sin comportamiento (`PRODUCT-INTAKE` §17.4 P.2): no tiene estado y no puede sostener ninguna invariante. Es el caso que `Rules-Especificacion-Funcional.md` §2.1 nombra como «proyecto de código trivial sin estado ni invariantes» para omitir las `RN-XX`, y §2.2 confirma que para `library` las reglas de negocio no son obligatorias.

Las reglas de dominio viven en la especificación funcional de `GeometriaFactory-Domain`. Los casos de uso de esta sección las **refieren por identificador**, con enlace relativo al archivo de ese proyecto de código, en la fila «Reglas de negocio aplicables» de su §9: `RN-08001` y `RN-08006` en CU-08001; `RN-08001`, `RN-08002`, `RN-08006` y `RN-08007` en CU-08002; `RN-08003`, `RN-08004`, `RN-08005` y `RN-08008` en CU-08003; `RN-08003` y `RN-08011` en CU-08004; `RN-08003`, `RN-08009` y `RN-08010` en CU-08005; `RN-08003`, `RN-08009`, `RN-08010` y `RN-08011` en CU-08006 —`RN-08009` es la que `PRODUCT-INTAKE` §17.4 P.5 ancla al tipo de respuesta de error—; `RN-08010` y `RN-08011` en CU-08007; y `RN-08012`, `RN-08013`, `RN-08001` y `RN-08007` en CU-08008, esta última **por contraste**, porque es la regla que ese contrato existe para no disparar. Los ocho casos de uso refieren al menos una regla por identificador. Donde una invariante no tiene identificador nombrable, la fila la nombra en lenguaje natural y declara su proyecto de código destino. **Ninguna `RN-XX` se redacta ni se inventa en esta sección.**

Dos advertencias de lectura sobre los archivos de `GeometriaFactory-Domain`, para que ninguna categoría aguas abajo cite el enunciado equivocado: `RN-08004-Eliminacion-Acotada-Al-Borrador.md` **cubre hoy los dos caminos de eliminación**, el del alumno acotado al borrador y el del administrador sobre cualquier trabajo que ve, y `RN-08005-Finalizacion-Sin-Errores-De-Validacion.md` **corta hoy en el envío y no en el cierre**. Los dos slugs quedaron desactualizados respecto de su enunciado y se decidió aguas arriba no renombrarlos para no romper los enlaces de esta sección. Se cita el contenido vigente, no el que sugiere el nombre.

Lo que sí decide esta categoría es qué se expone y qué no, y eso baja a criterios de aceptación verificables por inspección de la superficie pública —CA-02 de CU-08001, CA-01 y CA-05 de CU-08002, CA-01 de CU-08004, CA-05 de CU-08005 y CA-01 de CU-08006—, no a reglas de negocio.

## 6. Restricciones transversales del contrato

Valen para los ocho casos de uso y no se repiten en cada uno más allá de su criterio de aceptación correspondiente.

| Id | Restricción | Origen | Dónde se verifica |
| --- | --- | --- | --- |
| RT-01 | Ningún tipo de transferencia incluye el hash de contraseña, la clave de firma ni ninguna dirección de servicio interno | `PRODUCT-INTAKE` §17.4 P.5, §14 RA-03 | CU-08001 CA-02, CU-08002 CA-05, CU-08005 CA-05, CU-08006 CA-01 |
| RT-02 | La respuesta de error lleva texto neutro y, cuando corresponde, índice de figura y campo señalado, nunca la dirección del servicio que falló | `PRODUCT-INTAKE` §17.4 P.5 | CU-08006 CA-01, CA-02, CA-04 |
| RT-03 | El texto original del trabajo viaja como cadena, sin interpretarse en el contrato | `PRODUCT-INTAKE` §17.4 P.11 | CU-08003 CA-01, CA-02 |
| RT-04 | La proyección de listado no incluye ni el texto original, ni los componentes de las piezas, ni el comentario del administrador, para que el listado no arrastre texto libre de cada trabajo | `PRODUCT-INTAKE` §17.4 P.10 | CU-08004 CA-01, CA-04, y CU-08004 §10 |
| RT-05 | El ensamblado no declara ninguna referencia hacia `GeometriaFactory-Domain` | `PRODUCT-INTAKE` §17.4 P.8 (quality gate bloqueante) | Precondición de los ocho casos de uso; su verificación pertenece a 09 |
| RT-06 | Un cambio incompatible de contrato obliga al despliegue conjunto de las dos piezas desplegables. **Esta versión lo ejerce**: el conjunto cerrado de estados y el de códigos de error cambiaron los dos | `PRODUCT-INTAKE` §17.4 P.3 y P.7 | §17 de cada caso de uso |
| RT-07 | Este proyecto de código no tiene pruebas propias: se ejercita íntegramente desde las pruebas de integración que golpean el servicio real. Su gate equivalente es que el 100 % de los tipos de transferencia esté ejercitado por al menos una prueba de integración; **el intake rotula ese valor `[ASUNCIÓN]` y lo lista en §22**, de modo que está completo y se usa como valor vigente hasta que el Product Owner lo confirme | `PRODUCT-INTAKE` §17.4 P.6 | 08-Calidad-Y-Pruebas |
| RT-08 | El conjunto cerrado de estados del trabajo tiene cuatro valores y dos de ellos son terminales: el contrato no declara ningún tipo que permita salir de `Finalizado` ni de `Rechazado` | `PRODUCT-INTAKE` §4.2 (modelo de estados), `RN-08010` | CU-08007 CA-03, CU-08003 §7, CU-08005 §3 |
| RT-09 | El comentario del administrador viaja en el detalle como bloque propio y **nunca** como elemento de la colección de observaciones: no comparten ni un campo | `PRODUCT-INTAKE` §12 (entrada «comentario»), §4 (F-21) | CU-08005 CA-07, CA-08, CA-09 |
| RT-10 | **Ninguna condición que impida operar viaja como campo de la respuesta de sesión.** El tipo de respuesta de sesión declara cuatro campos y ninguno más; la cuenta no habilitada, la que no estableció contraseña y la que tiene un cambio de contraseña pendiente viajan las tres como **respuesta de error con código propio** | `PRODUCT-INTAKE` §17.5 P.5 (los cuatro reclamos de la credencial), §17.1.P.2 (INV-09) | CU-08001 CA-02, CA-05 y CA-06; CU-08008 CA-04 |
| RT-11 | **Ningún tipo de transferencia habilita a que el navegador invoque la API.** Todas las solicitudes de este ensamblado las arma el servidor de la pieza pública y viajan servidor a servidor, incluidas las que llevan credenciales en claro —canje, establecimiento, cambio y reseteo— | `PRODUCT-INTAKE` §14 **RA-01**, §17.4 P.5 | CU-08008 §10; su verificación estructural pertenece a 05 y a 09 |

## 7. Glosario

El vocabulario de esta categoría vive en [`Glosario-Funcional.md`](Glosario-Funcional.md), que declara veintidós términos acuñados acá, tres términos con más de un referente —«contrato», «pieza» y **`Pendiente`**— y veinticuatro términos referenciados del glosario raíz de `Vision-Producto.md` §9, que no se redefinen.

Dos advertencias de lectura para las categorías aguas abajo. **`Pendiente` nombra dos estados distintos** —el de una cuenta y el de un trabajo—, los dos cruzan este mismo contrato, y por eso se escribe siempre calificado: «cuenta `Pendiente`» o «trabajo en estado `Pendiente`». La regla viene de `PRODUCT-INTAKE` §4.2, es vinculante para toda la documentación generada, y su alcance y sus dos excepciones están en `Glosario-Funcional.md` §3.3. Y **«contrato» tiene tres referentes en esta cadena** —el ensamblado de tipos de transferencia, el contrato de uso que describe cada caso de uso, y el contrato de verificación `VER-XX` de un sample en 10-Examples—. La forma que corresponde a cada uno está en `Glosario-Funcional.md` §3.1.

## 8. Artefactos de esta categoría que se omiten

La tabla maestra de `Rules-Especificacion-Funcional.md` §2.1 tiene ocho filas: se emiten cuatro artefactos y **no se emiten cuatro**, agrupados acá en tres puntos porque el modelo conceptual y sus reglas conceptuales se omiten por el mismo motivo. Es el mismo conteo que declara el [`README.md`](README.md) §4 de esta sección, donde está desarrollado el motivo de cada omisión; acá se enumera para que el índice maestro no deje huecos sin explicación:

- `Definicion-<Concepto-Central>.md`: la columna «Recomendado» de §2.1 nombra «library con superficie estrecha», que es este caso, de modo que la regla lo **recomienda** y no autoriza su omisión por esa vía. Es, por lo tanto, **una recomendación no seguida con motivo declarado**: el ensamblado no tiene un concepto técnico central separable de los siete contratos de uso —supuesto de la columna «Omitir para: tipos sin concepto central»—, y un documento aparte duplicaría lo que ya está en el §1 y el §17 de cada caso de uso.
- `Reglas-De-Negocio/RN-XX-<Nombre>.md`: omitido por §2.1 y §2.2; el fundamento está en §5 de este documento.
- `Modelo-Datos/Modelo-Conceptual.md` y `Modelo-Datos/reglas-conceptuales-de-modelo/RC-XX-<Nombre>.md`, que son **dos** de las ocho filas de §2.1: omitidos los dos por tipo `library` y por `tiene_persistencia` == false.

## 9. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Índice maestro de los seis casos de uso de `GeometriaFactory-Contracts`, con el criterio de recorte por familias de tipos de transferencia, la declaración de numeración local, la matriz NB→CU→RN→US con sus ocho filas, la cobertura inversa con el grado en que este proyecto de código sostiene cada necesidad, el fundamento de la columna RN vacía, nueve restricciones transversales del contrato con su punto de verificación y la enumeración de los tres artefactos omitidos. | Analista Funcional + API Designer (AG-02) |
| 1.0 | 2026-08-08 | Correcciones absorbidas de la ronda 1 de auditoría (`Audit/B-02-03-GeometriaFactory-Contracts-r1.md`), sin subir versión por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **H-03**: §3.2 prometía en §4.1 una correspondencia que §4.1 no contenía; se agrega **§4.2**, con las veintidós previsiones `P·CU-01` a `P·CU-22` de `Necesidades-Negocio.md` §5.3, el caso de uso local que aporta el contrato de cada una y lo que queda fuera de este proyecto de código, y el remite de §3.2 pasa a §4.2. **H-05**: §8 refunda la omisión de `Definicion-<Concepto-Central>.md`, que citaba como permiso una celda de la columna «Recomendado»; pasa a declararse como recomendación no seguida con motivo declarado. **H-07**: §5 deja de afirmar una referencia genérica y enumera las `RN-XX` de `GeometriaFactory-Domain` que cada caso de uso refiere por identificador, y declara el único que no refiere ninguna con su motivo. **H-11**: `RT-07` incorpora el rótulo `[ASUNCIÓN]` que el intake §17.4 P.6 le pone al gate del 100 %, con el mismo tratamiento que CU-08004 §10 ya daba al caso análogo. **H-09**, por arrastre: el punto de verificación de `RT-06` pasa de «§12» a «§17 de cada caso de uso». Se actualiza el conteo de términos del glosario en §7 por H-04. | Analista Funcional + API Designer (AG-02) |
| 1.0 | 2026-08-08 | Correcciones absorbidas de la ronda 2 de auditoría (`Audit/B-02-03-GeometriaFactory-Contracts-r2.md`), sin subir versión por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **N-02**: la fila `P·CU-05` de §4.2 atribuía el establecimiento de contraseña a «CU-08002 FA-02», que es el **cambio** de contraseña y corresponde a `P·CU-07`; pasa a apuntar al flujo principal de CU-08002, pasos 7 y 8, que es el que sostiene el establecimiento. Es la única celda corregida de las veintidós. **N-03**: §8 declaraba «tres artefactos… no se emiten» y el `README.md` §4 ya declaraba cuatro en tres filas; se unifica en **cuatro omitidos agrupados en tres puntos**, verificado contra las ocho filas de la tabla maestra de `Rules-Especificacion-Funcional.md` §2.1, y el tercer punto nombra las dos filas que agrupa. | Analista Funcional + API Designer (AG-02) |
| 1.1 | 2026-08-09 | Actualización por contenido nuevo aguas arriba, no por auditoría: `PRODUCT-INTAKE` 1.3 incorpora el circuito de revisión del administrador en §4 (F-07, F-08, F-12, F-21, F-22, F-23, F-24), §4.1 (once reglas, con RN-08002, RN-08006, RN-08010 y RN-08011 nuevas), §4.2 (modelo de cuatro estados con dos terminales), §6 (flujo 2.1), §7 (CL-3, CL-10, CL-11) y §9 (retiro de X-5); `00-Contexto` y `01-Necesidades-Negocio` en su versión 1.1, con **NB-00009** nueva. Cambios: el catálogo de §3 pasa a **siete** casos de uso con la emisión de **CU-08007**, contrato de desenlace de la revisión; §3.1 declara los tres criterios de recorte nuevos —por qué CU-08007 es propio, por qué aprobar y rechazar no se separan y por qué la eliminación por el administrador se absorbe en CU-08003—; la matriz de §4 suma la fila **NB-00009** y cuatro historias de usuario, y pasa a veinte; §4.1 suma la fila NB-00009 y ajusta NB-00003 y NB-00007; §4.2 pasa de veintidós a **veintisiete** previsiones, con las cinco nuevas mapeadas; §5 enumera las referencias `RN-XX` de los siete casos de uso y declara las dos advertencias sobre los slugs desactualizados de `RN-08004` y `RN-08005`; §6 suma **RT-08** —cuatro estados, dos terminales— y **RT-09** —el comentario nunca es una observación—, y RT-04 incorpora el comentario a lo que el listado no arrastra; §7 actualiza los conteos del glosario y suma la advertencia de la forma calificada obligatoria de `Pendiente`. **Precisión de la misma intervención**: el criterio de recorte de §3.1 seguía nombrando «la finalización» como flujo alternativo fusionado, acción que el modelo vigente no tiene, y pasa a nombrar el paso a estado `Pendiente` como salida del propio envío.  **Corrección de la ronda 3 de auditoría, hallazgo H-02**, absorbida sin subir versión: seis conteos habían quedado describiendo el catálogo anterior y se propagan —§3.1 «los cinco casos de uso anteriores» pasa a «los otros seis»; §3.2 el rango contiguo pasa a `CU-08001` a `CU-08007`; §4.2 la previsión de 01 pasa a veintisiete y el rango del prefijo a `P·CU-01` a `P·CU-27`; §5 «las ocho filas» de la matriz pasa a nueve; `RT-05` y §8 pasan a siete casos de uso y siete contratos de uso—. | Analista Funcional + API Designer (AG-02) |
| 1.2 | 2026-08-09 | Actualización por contenido nuevo aguas arriba, no por auditoría: `PRODUCT-INTAKE` **1.7** incorpora la capacidad **F-26** —reseteo de contraseña por el administrador, `Must Have`—, las reglas **RN-08012** y **RN-08013**, el invariante **INV-09**, el retiro de la exclusión **X-2** y la reescritura del caso límite **CL-7**. Cambios: el catálogo de §3 pasa a **ocho** casos de uso con la emisión de **CU-08008**, contrato de reseteo y de cambio obligatorio de contraseña; §3.1 declara dos criterios de recorte nuevos —por qué CU-08008 es propio, con el mismo fundamento con el que se emitió CU-08007, y por qué **la solicitud de cambio de contraseña de CU-08002 se reutiliza y no se redeclara**—; §3.2 extiende el rango contiguo a `CU-08008`; la matriz de §4 suma CU-08008 a NB-00001 y NB-00002 y pasa de veinte a **veintidós** historias previstas; §4.1 amplía las dos filas correspondientes; §4.2 suma una fila **sin previsión en 01**, con el motivo declarado —F-26 entró después de que 01 emitiera sus veintisiete previsiones— y sin extender esa serie ajena; §5 enumera las cuatro reglas que CU-08008 refiere, incluida `RN-08007` **por contraste**; §6 suma **RT-10** —ninguna condición que impida operar viaja como campo de la respuesta de sesión— y **RT-11** —ningún tipo habilita a que el navegador invoque la API, que es **RA-01**—, y actualiza los conteos de siete a ocho casos de uso. El conjunto cerrado de códigos de error de CU-08006 pasa de catorce a **dieciséis** y **las señales declaradas siguen siendo tres**. | Analista Funcional + API Designer (AG-02) |
| 1.3 | 2026-08-09 | **Absorbe dos decisiones del Product Owner sobre F-26**, que **CU-08008** 1.2 y **CU-08006** 1.3 aplican. **Decisión A: resetear no exige que la cuenta esté habilitada**, porque no es una transición de situación; el administrador resetea y habilita en el orden que quiera. **Decisión B: la contraseña provisoria la produce el sistema y no la escribe el administrador**, para que no termine siendo la misma clave para toda la comisión. Cambios acá: §3 actualiza la fila de CU-08006 a **diecisiete** códigos —entra `CONTRATO_RESETEO_NO_APLICABLE_A_CUENTA_SIN_CONTRASENA` y **no entra ninguno por cuenta no habilitada**, porque esa causa dejó de existir—; §3.1 corrige el criterio de recorte de CU-08008, cuya solicitud pasa a llevar **sólo el identificador de cuenta** y cuyo resultado pasa a llevar **la provisoria generada**. **Ningún caso de uso se agrega ni se quita, la matriz de §4 no cambia y las restricciones transversales de §6 siguen siendo once**: `RT-10` no se toca, porque las tres condiciones que enumera siguen viajando como respuesta de error. | Analista Funcional + API Designer (AG-02) |
| 1.4 | 2026-08-10 | **Absorbe `PRODUCT-INTAKE` 1.13 §4.1 (RN-08016) y la precisión de F-04.** Habilitar una cuenta produce su contraseña provisoria, con lo cual **no queda ninguna escritura anónima de credencial** y el producto tiene un solo mecanismo de credencial inicial. **§3** actualiza la línea de `CU-08006`: el conjunto cerrado pasa de diecisiete a **quince códigos**, porque salen `CONTRATO_CONTRASENA_NO_ESTABLECIDA` y `CONTRATO_RESETEO_NO_APLICABLE_A_CUENTA_SIN_CONTRASENA`, los dos por imposibilidad de su causa. **§6** reescribe la fila de `P·CU-05`: el establecimiento de contraseña del primer ingreso deja de tener tipo propio y usa la solicitud de cambio de `CU-08002` FA-02, con la provisoria como vigente, por el mismo circuito que `CU-08008` declara. **Los ocho contratos de uso no cambian de número ni de recorte.** Sube minor. | Analista Funcional + API Designer (AG-02) |
| 1.5 | 2026-08-10 | **Cierra el hallazgo `C-08` (P2) del informe de auditoría `SDD/Docs/Audit/Coherencia-Corpus-r1.md` 1.0.** La cabecera de trazabilidad declaraba derivarse del `PRODUCT-INTAKE` **1.7**, versión archivada, y pasa a declarar la **1.14**, vigente. La **1.7** es la versión cuya letra sobre **RN-08013** e **INV-09** fue precisada en la 1.8 y corregida en la 1.14, que es exactamente el punto donde el corpus más se equivocó. Se revisó el cuerpo antes de mover la cabecera y **no arrastra ninguna decisión de las versiones intermedias**: no queda en él ningún recuento de «quince reglas» ni de «diecisiete códigos», ninguna cita a la exclusión **X-2** como vigente y ninguna afirmación de que la marca de cambio de contraseña pendiente la ponga únicamente el reseteo. **Ningún contenido normativo de este documento cambia: la corrección es de trazabilidad.** Sube minor. | Analista Funcional + API Designer (AG-02) |
| 1.6 | 2026-08-10 | **Absorbe la corrección de `PRODUCT-INTAKE` 1.15 §4.1 (RN-08016)**, que declara falsa la afirmación de 1.13 según la cual la regla deja al producto sin ninguna escritura anónima: el **registro de cuenta** es anónimo por diseño y debe seguir siéndolo, y su solicitud es un tipo de este ensamblado. Lo que se eliminó es la escritura anónima **de credencial**. Único cambio acá: **la fila 1.4** de este control de cambios pasa a decir «de credencial». **Los ocho contratos de uso, sus tipos y el conjunto cerrado de quince códigos de `CU-08006` no cambian.** Sube minor. | Analista Funcional + API Designer (AG-02) |
| 1.7 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. | Orquestador SDD |
