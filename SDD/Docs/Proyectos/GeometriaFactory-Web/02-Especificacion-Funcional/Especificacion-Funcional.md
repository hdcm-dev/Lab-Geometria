# Especificación funcional — GeometriaFactory-Web

**Proyecto de código:** GeometriaFactory-Web
**Documento:** Especificacion-Funcional.md
**Versión:** 1.4
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Analista Funcional senior (AG-02)
**Trazabilidad upstream:** `../../../00-Contexto/Vision-Producto.md` §3, §7, §9 (glosario raíz); `../../../00-Contexto/Alcance-Producto.md` §4.1, §5, §8; `../../../00-Contexto/Compatibilidad-Plataformas.md` §2.2 y §4; `../../../01-Necesidades-Negocio/Necesidades-Negocio.md` §2, §4, §5.3, y las nueve `NB-XX` de `../../../01-Necesidades-Negocio/Necesidades-De-Negocio/`; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.10**, §4 (capacidades, con **F-26** y **F-25** ya `Must Have`), §4.1 (**las quince reglas**, con **RN-13 precisada**), §4.2 (modelo de estados y tabla de quién puede qué), §5 (historias de usuario), §6 (flujos 1, 2, 2.1, 3 y 4), §7 (casos límite), §13 y §14 (composición y reglas RA-01 a RA-03), §17.1.P.2 (**INV-09**), §17.6 íntegro (P.1 a P.12) y §17.7 P.3 y P.10; `../../GeometriaFactory-Contracts/02-Especificacion-Funcional/` (**ocho** contratos de uso, **diecisiete** códigos de error y tres señales, con **CU-08 cubriendo F-26**: ver §6, RT-12); `../../GeometriaFactory-Visor/02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md` (**seis funciones** y siete condiciones)
**Trazabilidad downstream:** `../03-UX-UI-DX/` de este mismo proyecto de código, que es el downstream más directo y del que depende la fase de maqueta; `05-Arquitectura-Tecnica`; `06-Backlog-Tecnico`; `08-Calidad-Y-Pruebas`

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
- [5. Por qué esta sección no redacta reglas de negocio](#5-por-qué-esta-sección-no-redacta-reglas-de-negocio)
- [6. Restricciones transversales de la pieza pública](#6-restricciones-transversales-de-la-pieza-pública)
- [7. Consumo del contrato de fachada del visualizador](#7-consumo-del-contrato-de-fachada-del-visualizador)
- [8. Glosario](#8-glosario)
- [9. Artefactos de esta categoría que se omiten](#9-artefactos-de-esta-categoría-que-se-omiten)
- [10. Control de cambios](#10-control-de-cambios)

---

## 1. Propósito y alcance de esta especificación

`GeometriaFactory-Web` es la **pieza pública** del producto Fábrica de Geometría: el front desplegado en el hosting público y **el único punto de contacto del navegador**. Es una de las dos unidades de entrega del producto y nivel 1 del orden topológico; compila contra `GeometriaFactory-Contracts` y contra el bundle de `GeometriaFactory-Visor`, y habla con la pieza de datos **en tiempo de ejecución, servidor a servidor**, lo que no es dependencia de compilación.

Es el primer proyecto de código del producto cuyos casos de uso tienen **actores humanos**: el alumno de la comisión y el docente en su papel de administrador. Los cuatro proyectos de código ya especificados tenían por actor al código que los consume.

Lo que esta especificación **no** decide: las reglas del dominio, que viven y se hacen cumplir en `GeometriaFactory-Domain`; la forma de los puntos de acceso del servicio, que es de `GeometriaFactory-Api`; el diseño de las pantallas, que es de 03-UX-UI-DX; la arquitectura y los registros de decisión, que son de 05; y las pruebas, que son de 08. Tampoco decide nada del interior del bundle del visualizador, que se consume exclusivamente por su fachada.

## 2. Qué decide este proyecto de código

Decide **qué hace la persona y qué ve**, y con qué contrato de uso obtiene cada cosa. Tres decisiones de nivel producto lo gobiernan y son la razón de ser de la topología entera; un caso de uso que viole cualquiera de las tres es un defecto y no una alternativa de diseño:

| Regla | Enunciado | Cómo se manifiesta acá |
| --- | --- | --- |
| RA-01 | Ningún guion del navegador invoca la pieza de datos | Todas las llamadas de los diez casos de uso salen del servidor de la pieza pública. Se verifica contando peticiones del navegador: el umbral es exactamente 0 |
| RA-02 | El bundle del visualizador es un visualizador puro | La pieza pública lo invoca **sólo** por sus seis funciones, y es **ella** la que consulta el entorno del navegador y le manda el resultado: el bundle no consulta nada. Ningún componente toca su interior ni manipula el elemento de dibujo por su cuenta |
| RA-03 | Todo lo que el navegador deba obtener de la pieza de datos pasa por la pieza pública | Ningún mensaje mostrado incluye la dirección de un servicio interno, y toda respuesta de error del contrato se traduce a presentación propia |

La decisión más consecuente de las tres, en términos de lo que la persona puede observar, es que **la credencial de sesión vive en el estado del circuito, del lado del servidor, y nunca llega al navegador**. Está escrita como criterio de aceptación verificable en CU-02, CA-02.

## 3. Catálogo de casos de uso

| CU | Qué hace la persona | Actor primario | NB que sostiene | Estado | Enlace |
| --- | --- | --- | --- | --- | --- |
| CU-01 | Registrarse en el laboratorio con correo, nombre y apellido, sin elegir contraseña | Alumno | NB-02, NB-01 | Propuesto | [CU-01](Casos-De-Uso/CU-01-Registrar-La-Cuenta-De-Alumno.md) |
| CU-02 | Iniciar y cerrar sesión, con la credencial custodiada del lado del servidor y las rutas protegidas por papel | Persona con cuenta | NB-02, NB-01 | Propuesto | [CU-02](Casos-De-Uso/CU-02-Iniciar-Y-Cerrar-Sesion-Sin-Exponer-La-Credencial.md) |
| CU-03 | Establecer la contraseña en el primer ingreso efectivo, cambiarla presentando la vigente, y cambiarla **obligada** tras un reseteo del administrador | Persona con cuenta habilitada | NB-02 | Propuesto | [CU-03](Casos-De-Uso/CU-03-Establecer-Y-Cambiar-La-Contrasena-Propia.md) |
| CU-04 | Ver la lista de cuentas y habilitar, bloquear, rehabilitar, **resetear la contraseña** o dar de baja, con confirmación escrita en la baja | Administrador | NB-01, NB-02 | Propuesto | [CU-04](Casos-De-Uso/CU-04-Administrar-Las-Cuentas-De-La-Comision.md) |
| CU-05 | Cargar un trabajo, previsualizarlo y **enviarlo**, con el estado que la interpretación decide | Alumno | NB-04, NB-03, NB-05 | Propuesto | [CU-05](Casos-De-Uso/CU-05-Enviar-Un-Trabajo-Y-Ver-El-Resultado-De-La-Interpretacion.md) |
| CU-06 | Ver los trabajos propios con sus cuatro estados, y editar o eliminar sólo en borrador | Alumno | NB-03, NB-09 | Propuesto | [CU-06](Casos-De-Uso/CU-06-Consultar-El-Listado-Propio-Y-Operar-Sobre-El-Borrador.md) |
| CU-07 | Abrir un trabajo y explorarlo: datos, texto, escena y árbol sincronizados por índice | Alumno dueño, con el administrador en FA-01 | NB-06, NB-05, NB-07, NB-09, NB-04 | Propuesto | [CU-07](Casos-De-Uso/CU-07-Abrir-Un-Trabajo-Y-Explorarlo-En-Escena-Y-Arbol.md) |
| CU-08 | Recorrer la entrega de la comisión, agrupada y filtrada por alumno, sin los borradores | Administrador | NB-07, NB-09 | Propuesto | [CU-08](Casos-De-Uso/CU-08-Recorrer-La-Entrega-De-La-Comision.md) |
| CU-09 | Aprobar o rechazar un trabajo con comentario opcional, y retirar cualquiera que ve | Administrador | NB-09, NB-07 | Propuesto | [CU-09](Casos-De-Uso/CU-09-Resolver-Un-Trabajo-Con-Comentario-Opcional.md) |
| CU-10 | Seguir usando la aplicación cuando algo se corta, con aviso explícito y sin pantalla rota | Persona que usa el laboratorio | NB-08 | Propuesto | [CU-10](Casos-De-Uso/CU-10-Sostener-La-Aplicacion-En-Estado-Degradado-Y-Reconexion.md) |

Diez casos de uso, sobre el mínimo de **ocho** que `Rules-Especificacion-Funcional.md` §2.2 fija para `web-monolith`. El mínimo es piso; el techo lo fijó la cobertura completa de las nueve necesidades de negocio, que se verifica en §4.1.

### 3.1 Criterio de recorte

El recorte sigue **el objeto sobre el que la persona actúa y el papel con el que actúa** —la cuenta, la sesión, la credencial, el trabajo propio, el trabajo de la comisión, la disponibilidad—, y no las capacidades `F-XX` una por una: varias capacidades se ejercen en la misma pantalla y en el mismo acto, y partirlas por identificador habría producido casos de uso que son sub-flujos.

| Decisión | Fundamento |
| --- | --- |
| Se separó CU-02 de CU-01 y de CU-03 | El canje de credenciales es la superficie donde vive la exigencia de que la credencial de sesión no llegue al navegador, y tiene que leerse sola. Escondida dentro del registro o del establecimiento de contraseña, esa restricción no tendría dónde verificarse |
| Se fusionaron el establecimiento y el cambio de contraseña en CU-03 | Es el mismo objeto —la credencial propia—, el mismo actor y la misma pantalla salvo un campo. Dos casos de uso habrían duplicado la superficie sin declarar ninguna decisión distinta |
| Se absorbió el **cambio forzado** en CU-03 como tercer curso, y no como caso de uso propio | Mismo objeto, mismo actor y **el mismo formulario que el cambio**: lo único que cambia es de dónde se llega y que no hay salida. Lo que **sí** es decisión propia y por eso se declara con criterio de aceptación es el confinamiento, y ése no vive en el formulario sino en el guard, que CU-02 FA-07 y CU-03 FA-05 declaran |
| Se absorbió el **reseteo de contraseña** en CU-04, FA-06 | El intake lo pide explícitamente «desde el mismo panel» donde el administrador habilita, bloquea y da de baja (F-26). Mismo actor, misma lista, misma fila: un caso de uso propio para la quinta operación de la misma superficie habría sido un sub-flujo, por el mismo criterio con el que las otras cuatro quedaron juntas |
| Se fusionaron las **cinco** operaciones de cuenta en CU-04 | Habilitar, bloquear, rehabilitar, resetear la contraseña y dar de baja se ejercen desde la misma lista, con el mismo actor. La baja se distingue por su confirmación escrita, y el reseteo por su confirmación simple y su comunicación de la provisoria; las dos entran como flujo alternativo y no como caso de uso propio |
| Se absorbió el alta inicial del administrador en CU-04, FA-03 | Su actor primario es el administrador y su superficie es la misma ruta de administración. Un caso de uso propio para un formulario que se usa **una vez en la vida del laboratorio** habría sido un sub-flujo |
| Se separó CU-05 de CU-06 | Enviar y recorrer son actos distintos con resultados distintos: uno decide el estado del trabajo, el otro lo consulta. Fusionarlos habría escondido la propiedad central del producto —el envío es la única acción de guardado— dentro de un caso de uso de listado |
| Se absorbió el paso a estado `Pendiente` dentro de CU-05 | Con el envío como acción única no hay una operación separada que llevar a un caso de uso propio: el estado es una salida del mismo envío |
| Se separó CU-06 de CU-08 | Son dos listados con **alcance distinto y actor distinto**: el propio del alumno incluye sus borradores, el de la comisión los excluye por regla de dominio. Un solo caso de uso habría dejado el recorte sin lugar donde verificarse |
| Se emitió CU-07 como caso de uso propio y único para los dos papeles | La vista de trabajo es idéntica para el alumno dueño y para el administrador, y esa identidad es un criterio de éxito de negocio —«4 de 4 elementos»—. Dos casos de uso la habrían duplicado y habrían admitido que divergieran. El administrador entra como actor secundario en FA-01, no como segundo actor primario |
| Se emitió CU-09 como caso de uso propio | El desenlace tiene actor exclusivo, precondición de estado y regla de dominio propios. Absorberlo en CU-08 habría mezclado recorrer con decidir, que es exactamente la frontera que NB-07 y NB-09 declaran entre sí |
| Se fusionaron aprobar y rechazar en CU-09 | Comparten pantalla, precondición, errores y regla. Se distinguen por el valor de una decisión de conjunto cerrado |
| Se absorbió el retiro de trabajos por el administrador en CU-09, FA-03 | Mismo actor, mismo panel y misma solicitud de eliminación que ya usa el alumno; lo que difiere es la regla que la acota, y las reglas viven en el dominio |
| Se hizo transversal CU-10 | Los otros nueve comparten sus caminos de indisponibilidad. Es el caso de uso transversal de manejo de errores que sugiere §5.2 de las reglas, y concentra en un solo lugar la superficie donde RA-03 se puede violar |

### 3.2 Numeración local

Los identificadores `CU-XX`, `US-XX` y `CA-XX` de esta sección son **locales a `GeometriaFactory-Web`**. `Necesidades-Negocio.md` §5.3 prevé veintisiete casos de uso `CU-01` a `CU-27` a nivel producto; esa previsión se reparte entre las especificaciones funcionales de los siete proyectos de código, cada una con su numeración contigua desde `CU-01`, porque la categoría 02 es de nivel proyecto de código. La correspondencia se lee en §4.2.

La numeración de esta sección es contigua de `CU-01` a `CU-10`, sin huecos. Las historias de usuario previstas son una previsión de esta categoría; las confirma la categoría 06 al redactarlas. Eran veintisiete, `US-01` a `US-27`, y son **treinta** desde el `PRODUCT-INTAKE` 1.7: `US-28` —cambio forzado que levanta la marca—, `US-29` —confinamiento de la cuenta reseteada— y `US-30` —reseteo desde el panel, conservando la cuenta y sus trabajos—.

## 4. Matriz NB→CU→RN→US

Las `RN-XX` de la tercera columna **viven en `GeometriaFactory-Domain`** y se referencian por identificador con enlace relativo. Ninguna se redacta acá.

| NB | CU | RN | US a generar en 06 |
| --- | --- | --- | --- |
| [NB-01](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-01-Control-De-Admision-Al-Laboratorio.md) | CU-01, CU-02, CU-04 | [RN-01](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-01-Administrador-Unico-Y-Papeles-Fijos.md), [RN-02](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02-Correo-Del-Alumno-Unico.md), [RN-06](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-06-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md), [RN-07](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-07-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md) | US-01, US-02, US-03, US-04, US-05, US-08, US-09, US-10 |
| [NB-02](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md) | CU-01, CU-02, CU-03, CU-04 | [RN-02](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02-Correo-Del-Alumno-Unico.md), [RN-06](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-06-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md) | US-01, US-02, US-03, US-04, US-05, US-06, US-07 |
| [NB-03](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-03-Trabajo-Con-Dueno-Estado-Y-Persistencia.md) | CU-05, CU-06 | [RN-03](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-03-Trabajo-Ajeno-Indistinguible-De-Inexistente.md), [RN-04](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-04-Eliminacion-Acotada-Al-Borrador.md), [RN-05](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-05-Finalizacion-Sin-Errores-De-Validacion.md), [RN-08](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-08-Texto-Original-Conservado-Integro.md) | US-11, US-12, US-13, US-14, US-15, US-16, US-17 |
| [NB-04](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-04-Interpretacion-Fiel-Del-Dato-Del-Alumno.md) | CU-05, CU-07 | [RN-03](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-03-Trabajo-Ajeno-Indistinguible-De-Inexistente.md), [RN-05](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-05-Finalizacion-Sin-Errores-De-Validacion.md), [RN-08](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-08-Texto-Original-Conservado-Integro.md), [RN-09](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-09-Observacion-De-Error-Con-Posicion-Y-Campo.md) | US-11, US-12, US-13, US-14, US-18, US-19, US-20, US-21 |
| [NB-05](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-05-Visibilidad-Del-Error-De-Calculo.md) | CU-05, CU-07 | [RN-05](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-05-Finalizacion-Sin-Errores-De-Validacion.md), [RN-09](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-09-Observacion-De-Error-Con-Posicion-Y-Campo.md) | US-13, US-14, US-19, US-20 |
| [NB-06](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-06-Visualizacion-Dentro-Del-Producto.md) | CU-07 | [RN-03](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-03-Trabajo-Ajeno-Indistinguible-De-Inexistente.md), [RN-08](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-08-Texto-Original-Conservado-Integro.md), [RN-09](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-09-Observacion-De-Error-Con-Posicion-Y-Campo.md) | US-18, US-19, US-20, US-21 |
| [NB-07](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-07-Revision-De-La-Comision-En-Un-Solo-Lugar.md) | CU-07, CU-08, CU-09 | [RN-03](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-03-Trabajo-Ajeno-Indistinguible-De-Inexistente.md), [RN-11](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-11-El-Administrador-No-Ve-Los-Borradores.md) | US-18, US-19, US-20, US-21, US-22, US-23, US-24, US-25 |
| [NB-08](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-08-Alcance-Del-Laboratorio-Desde-El-Aula.md) | CU-10 | Ninguna, con el motivo declarado en §5 y en CU-10 §9 | US-26, US-27 |
| [NB-09](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-09-Desenlace-Explicito-De-La-Entrega.md) | CU-06, CU-07, CU-08, CU-09 | [RN-04](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-04-Eliminacion-Acotada-Al-Borrador.md), [RN-10](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-10-Desenlace-Exclusivo-Del-Administrador-Y-Terminalidad.md), [RN-11](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-11-El-Administrador-No-Ve-Los-Borradores.md) | US-15, US-16, US-17, US-18, US-19, US-20, US-21, US-22, US-23, US-24, US-25 |

### 4.1 Cobertura inversa: de NB a CU

Ninguna necesidad queda sin caso de uso y ningún caso de uso queda huérfano. Este proyecto de código no sostiene cada necesidad en la misma medida, y declararlo es más útil que una fila uniforme.

| NB | Grado en que la pieza pública la sostiene | Qué queda en otro proyecto de código |
| --- | --- | --- |
| NB-01 | Sostenida en lo que le corresponde: es el panel donde el administrador ve la lista y ejerce las **cinco** operaciones, donde la confirmación escrita de la baja se exige y donde se comunica la contraseña provisoria del reseteo | La unicidad del administrador y el arrastre de trabajos son invariantes de `GeometriaFactory-Domain` |
| NB-02 | Sostenida y decisiva: es donde el alumno se registra, establece su contraseña y obtiene sesión, y donde se verifica que la credencial no llegue al navegador | La derivación de la credencial es de `GeometriaFactory-Infrastructure`; la admisibilidad de la cuenta, del dominio |
| NB-03 | Sostenida en lo que le corresponde: la acción única de guardado, el listado con los cuatro estados y la acotación de lo que el alumno puede hacer en cada uno | La persistencia es de `GeometriaFactory-Infrastructure`; las transiciones, del dominio |
| NB-04 | Parcial: envía el texto sin tocar un carácter y presenta las observaciones con índice de figura y campo señalado | La tolerancia de formato y la interpretación son de `GeometriaFactory-Infrastructure` y del dominio |
| NB-05 | Parcial: muestra las advertencias con los dos valores y no las convierte en un bloqueo | El recálculo y la tolerancia de comparación son del dominio y de su implementación |
| NB-06 | **Sostenida casi por entero**: la vista de trabajo, la sincronización entre árbol y escena, la enumeración de las piezas no dibujadas y el ciclo de vida de la instancia son de esta pieza | El dibujo en sí es de `GeometriaFactory-Visor`, invocado por su fachada |
| NB-07 | Sostenida y decisiva: el listado de la comisión, su agrupación y su filtro, y la vista idéntica para los dos papeles | El alcance de lo que el administrador ve lo decide el dominio |
| NB-08 | Parcial: la presentación del estado degradado y el cartel de reconexión, que es lo único de esta necesidad que la persona ve | La verificación de acceso desde la red de la facultad y el despliegue de las dos piezas desplegables son de 09-Devops |
| NB-09 | Sostenida en lo que le corresponde: las dos decisiones con su comentario opcional, el retiro, y que el alumno vea el desenlace en su listado y el comentario al abrir el trabajo | La exclusividad de la facultad y la terminalidad son invariantes del dominio |

### 4.2 Correspondencia con la previsión de casos de uso de 01

`Necesidades-Negocio.md` §5.3 previó veintisiete casos de uso a nivel producto. Ésta es la confirmación que le corresponde a `GeometriaFactory-Web`. Para distinguir las dos series homónimas, la previsión de 01 se escribe con el prefijo **`P·`** y los identificadores sin prefijo son siempre los locales de esta sección.

| Previsión de 01 | NB | CU local que la realiza | Qué queda fuera de este proyecto de código |
| --- | --- | --- | --- |
| `P·CU-01` configurar la cuenta de administrador en el primer arranque | NB-01 | CU-04, FA-03 | La unicidad, que es invariante de dominio |
| `P·CU-02` habilitar, bloquear y rehabilitar una cuenta | NB-01 | CU-04, flujo principal | La transición admitida |
| `P·CU-03` dar de baja una cuenta con confirmación escrita | NB-01 | CU-04, FA-02 | El arrastre de trabajos |
| `P·CU-04` registrar una cuenta de alumno | NB-02 | CU-01, flujo principal | La unicidad del correo |
| `P·CU-05` establecer la contraseña en el primer ingreso efectivo | NB-02 | CU-03, flujo principal; CU-02 FA-02 para el desvío | La derivación de la clave |
| `P·CU-06` iniciar y cerrar sesión | NB-02 | CU-02 completo | La emisión de la credencial de sesión |
| `P·CU-07` cambiar la contraseña exigiendo la vigente | NB-02 | CU-03, FA-01 | La verificación de la contraseña vigente |
| `P·CU-08` cargar un trabajo | NB-03 | CU-05, flujo principal | La persistencia |
| `P·CU-09` reeditar un trabajo en estado `Borrador` | NB-03 | CU-05 FA-05, y CU-06 FA-01 | La acotación al estado |
| `P·CU-10` eliminar un trabajo propio en estado `Borrador` | NB-03 | CU-06, FA-02 | La acotación al estado y a la pertenencia |
| `P·CU-11` listar los trabajos propios | NB-03 | CU-06, flujo principal | El alcance de la colección |
| `P·CU-12` interpretar el texto y reportar el error con figura y campo | NB-04 | CU-05, paso 8, y CU-07 paso 10 | Toda la interpretación y la tolerancia de claves |
| `P·CU-13` resolver el estado del trabajo según el resultado de la interpretación | NB-04 | CU-05, FA-01 y FA-02 | La decisión del estado, que es del dominio |
| `P·CU-23` enviar un trabajo | NB-04 | CU-05, flujo principal completo | La interpretación |
| `P·CU-14` verificar los valores declarados contra los derivados | NB-05 | CU-05 FA-03, y CU-07 paso 10 | El recálculo y la tolerancia de comparación |
| `P·CU-15` previsualizar el trabajo en tres dimensiones | NB-06 | CU-07, pasos 5 a 7; CU-05 paso 4 para la previsualización previa al envío | El dibujo, que es del bundle |
| `P·CU-16` explorar la estructura como árbol colapsable | NB-06 | CU-07, paso 8 | La estructura del texto, que devuelve la fachada |
| `P·CU-17` sincronizar el árbol y la escena por índice de pieza | NB-06 | CU-07, paso 9 | El resaltado, que ejerce la fachada |
| `P·CU-18` listar los trabajos de la comisión sin los que están en estado `Borrador` | NB-07 | CU-08, flujo principal | El recorte, que decide el dominio |
| `P·CU-19` abrir un trabajo de un alumno para revisarlo | NB-07 | CU-07, FA-01 | La visibilidad, que decide el dominio |
| `P·CU-20` consultar el panel de resumen por alumno y por estado | NB-07 | CU-08, FA-04 | El recuento, que produce la pieza de datos |
| `P·CU-21` verificar el acceso al laboratorio desde la red de la facultad | NB-08 | **Ninguno.** No es un acto de la persona dentro del producto | Verificación de campo y despliegue: 09-Devops |
| `P·CU-22` presentar el estado degradado cuando el servicio de datos no responde | NB-08 | CU-10, flujo principal | La respuesta de error neutra, que declara el contrato |
| `P·CU-24` aprobar un trabajo en estado `Pendiente`, con comentario opcional | NB-09 | CU-09, flujo principal | La transición y su exclusividad |
| `P·CU-25` rechazar un trabajo en estado `Pendiente`, con comentario opcional | NB-09 | CU-09, FA-01 | Ídem, con el otro valor de la decisión |
| `P·CU-26` consultar el desenlace y el comentario del trabajo propio | NB-09 | CU-06 FA-03 para el estado en el listado; CU-07 paso 11 para el comentario | El transporte del comentario |
| `P·CU-27` eliminar un trabajo desde el panel del administrador | NB-09 | CU-09, FA-03 | La acotación por visibilidad |

Veintiséis de las veintisiete previsiones se realizan en esta sección; `P·CU-21` es la única que este proyecto de código no toca, porque no es un acto que la persona ejecute dentro del producto.

**Dos actos de esta sección no tienen previsión en 01, y es correcto que no la tengan**: el **reseteo de contraseña** de CU-04 FA-06 y el **cambio forzado** de CU-03 FA-04. Los dos nacen de la capacidad **F-26**, que el `PRODUCT-INTAKE` incorporó en su versión **1.7**, posterior a la emisión de `Necesidades-Negocio.md` §5.3. **No se los fuerza dentro de ninguna previsión existente**: `01-Necesidades-Negocio` decidirá si los incorpora a su catálogo, y hasta entonces la correspondencia se lee al revés, desde la capacidad.

## 5. Por qué esta sección no redacta reglas de negocio

Las **quince reglas** del producto viven en `GeometriaFactory-Domain`, que es donde se hacen cumplir. Acá se **referencian por identificador** y no se redactan. **RN-12 y RN-13 entraron con el `PRODUCT-INTAKE` 1.7, y RN-14 y RN-15 con el 1.10; las cuatro tienen archivo allá**: [`RN-12`](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-12-Reseteo-Conserva-La-Cuenta-Y-Sus-Trabajos.md) y [`RN-13`](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-13-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md), de modo que las filas de la matriz que las citan las enlazan como a las otras once. El motivo no es formal: **la pieza pública no puede ser la última defensa de ninguna regla, porque el navegador no es confiable.** Ocultar un botón, no armar una ruta o no ofrecer una acción son decisiones de presentación legítimas y necesarias, pero no hacen cumplir nada; por eso varios criterios de aceptación de esta sección verifican la acotación **forzando la solicitud sin pasar por la pantalla** —CU-06 CA-04, CU-09 CA-04 y CA-05—.

La única fila de la matriz sin regla referenciada es NB-08, en CU-10. Su motivo está declarado en el propio caso de uso: las quince reglas restringen el dominio —cuentas, trabajos, estados, observaciones y credenciales— y CU-10 no toca ninguno de esos objetos. Lo que sí lo restringe es la regla de arquitectura RA-03, cuyo enunciado vive en `PRODUCT-INTAKE` §14 y que se verifica en CU-10 CA-02. Inventar una `RN-XX` para llenar la celda habría sido redactar una regla que no existe aguas arriba.

Dos advertencias de lectura sobre los archivos de `GeometriaFactory-Domain`, heredadas de la especificación de `GeometriaFactory-Contracts` §5: `RN-04-Eliminacion-Acotada-Al-Borrador.md` **cubre hoy los dos caminos de eliminación**, el del alumno acotado al borrador y el del administrador sobre cualquier trabajo que ve, y `RN-05-Finalizacion-Sin-Errores-De-Validacion.md` **corta hoy en el envío y no en el cierre**. Los dos slugs quedaron desactualizados respecto de su enunciado y se decidió aguas arriba no renombrarlos. Se cita el contenido vigente, no el que sugiere el nombre.

## 6. Restricciones transversales de la pieza pública

Valen para los diez casos de uso y no se repiten en cada uno más allá de su criterio de aceptación correspondiente. Son **trece** desde el `PRODUCT-INTAKE` 1.7.

| Id | Restricción | Origen | Dónde se verifica |
| --- | --- | --- | --- |
| RT-01 | Ninguna llamada a la pieza de datos se origina en el navegador: todas salen del servidor de la pieza pública | `PRODUCT-INTAKE` §14 (RA-01), §17.6 P.3 | CU-01 CA-05, CU-02 CA-07, CU-05 CA-06, CU-07 CA-10 |
| RT-02 | **La credencial de sesión vive en el estado del circuito, del lado del servidor, y no aparece nunca en el navegador.** El navegador conserva sólo una marca de sesión que no la transporta | `PRODUCT-INTAKE` §17.6 P.4 y P.5 | CU-02 CA-02, y CU-03 CA-05 para las contraseñas |
| RT-03 | Ningún mensaje mostrado a la persona incluye la dirección de un servicio interno, un nombre de archivo de datos ni una traza de la implementación | `PRODUCT-INTAKE` §14 (RA-03), §17.6 P.5 | CU-10 CA-02, y las filas de estado degradado de los §6 de CU-01, CU-04 a CU-09 |
| RT-04 | El bundle del visualizador se invoca **exclusivamente** por sus **seis** funciones —las cinco del ciclo de vida y la selección, más `establecerMovimiento`—. Ningún componente accede a su interior ni manipula el elemento de dibujo por su cuenta | `PRODUCT-INTAKE` §14 (RA-02), §17.6 P.3 (regla de aislamiento del visor) | CU-05 §10, CU-07 §7 y §10 de este documento |
| RT-05 | `destruir` se invoca al descartar el componente que aloja la instancia del visualizador. **No es opcional**: sin eso, recorrer trabajos acumula contextos gráficos en el navegador | `PRODUCT-INTAKE` §17.6 P.11 punto 5 | CU-07 CA-05, CU-05 CA-07 |
| RT-06 | La pieza pública **no guarda estado propio**. No hay copia local de los datos, ni caché, ni réplica: cuando la pieza de datos no está, no hay nada que mostrar y se declara el estado degradado | `PRODUCT-INTAKE` §17.6 P.4, §7 (CL-8) | CU-04 §6, CU-06 §6, CU-08 §6, CU-10 §7 |
| RT-07 | La indisponibilidad se presenta siempre como **estado degradado explícito**, nunca como excepción sin manejar y nunca como pantalla rota. El listado vacío se distingue del fallo **por el tipo recibido y no por el conteo** | `PRODUCT-INTAKE` §7 (CL-2), §17.6 P.10 | CU-10 CA-01, CA-07; CU-06 CA-06; CU-08 CA-06 |
| RT-08 | El texto original del trabajo se envía **carácter por carácter** tal como la persona lo pegó, y no se reescribe en ningún punto del recorrido | `PRODUCT-INTAKE` §4.1 (RN-08), §9 (X-4) | CU-05 CA-02 |
| RT-09 | Ninguna ruta del panel es accesible sin sesión, y un alumno con sesión no accede a ninguna ruta de administrador. Esto **acota lo que se ofrece**; la verificación de pertenencia y de papel la hace la pieza de datos en cada solicitud | `PRODUCT-INTAKE` §17.6 P.5 | CU-02 CA-05, CU-04 CA-07 |
| RT-10 | Durante la interacción con la escena no hay tráfico de circuito hacia el servidor, y el texto del trabajo viaja del servidor al navegador **una sola vez por trabajo** | `PRODUCT-INTAKE` §17.6 P.10 | CU-07 CA-10 |
| RT-11 | Toda combinación de navegador sin capacidad gráfica tridimensional se considera no soportada **para la escena**, y el resto del producto sigue disponible | `PRODUCT-INTAKE` §17.6 P.9 | CU-05 FA-04, CU-07 FA-05 |
| RT-12 | **Una cuenta con cambio de contraseña pendiente no llega a ninguna ruta que no sea el cambio de su propia contraseña, y llega ahí sin sesión de trabajo**: el canje reconoce la provisoria y no emite sesión. La pieza pública **acota lo que se ofrece**; quien lo hace cumplir es la pieza de datos, que verifica la marca en cada solicitud | `PRODUCT-INTAKE` **1.8** §4.1 (RN-13 precisada), §17.1.P.2 (INV-09) | CU-02 CA-08, CU-03 CA-06 y CA-07 |
| RT-13 | **El anfitrión gobierna los dos movimientos automáticos mandando dos valores de verdad por la fachada, y el bundle no consulta nada.** En particular, **la preferencia de movimiento reducido del navegador la lee la pieza pública**, no el visor, y la traduce a esos dos booleanos | `PRODUCT-INTAKE` **1.7** §4 (F-25, `Must Have`), §17.7 P.3 y P.10 | CU-07 §7, y §7 de este documento |

## 7. Consumo del contrato de fachada del visualizador

El bundle expone **seis funciones** y siete códigos de condición, declarados en [`Definicion-Contrato-De-Fachada.md`](../../GeometriaFactory-Visor/02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md). La sexta, `establecerMovimiento`, la incorporó el `PRODUCT-INTAKE` **1.6** §17.7 P.3 para gobernar los dos movimientos automáticos de **F-25** sobre una instancia viva, sin reconstruirla. Esta tabla declara, una sola vez, qué función consume cada caso de uso; los casos de uso no la repiten más allá de su fila de trazabilidad.

| Caso de uso | `inicializar` | `cargarJson` | `seleccionarPieza` | `redimensionar` | `destruir` | `establecerMovimiento` |
| --- | --- | --- | --- | --- | --- | --- |
| CU-05, previsualización previa al envío | Sí | Sí | — | — | Sí | — |
| CU-07, vista de trabajo | Sí | Sí | Sí | Sí | Sí | Sí |
| Los otros ocho | — | — | — | — | — | — |

Tres consecuencias que las categorías aguas abajo no deben perder:

1. **El componente anfitrión de la pieza pública es quien opera el ciclo de vida.** La fachada no observa tamaños ni decide cuándo ajustar: por eso `redimensionar` lo invoca CU-07 y no ocurre solo.
2. **El resultado de dibujo no lleva observaciones.** Las piezas que la fachada no dibuja se enumeran por su índice, y eso **no** las convierte en errores del trabajo: quien decide si el trabajo verifica es la pieza de datos. CU-05 §10 y CU-07 §10 lo declaran para que la vista no las mezcle.
3. **El gobierno del movimiento automático es del anfitrión y viaja en un solo sentido** (RT-13). La pieza pública manda **dos valores de verdad** por `establecerMovimiento` —uno por la órbita de la cámara y otro por el giro de las piezas— y el bundle **no consulta nada**: ni la preferencia de movimiento reducido del navegador, ni configuración propia, ni almacenamiento. **Esa preferencia la lee la pieza pública** y la traduce a los dos booleanos, que es la única forma compatible con RA-02, un visualizador sin configuración y sin identidad. Es también lo que hace que la instancia no se reconstruya para prender o apagar un movimiento, y por lo tanto que no se pierda la selección de pieza.

## 8. Glosario

El vocabulario de esta categoría vive en [`Glosario-Funcional.md`](Glosario-Funcional.md), que declara los términos que la pieza pública acuña, **tres términos con más de un referente** —«vista», «pieza» y `Pendiente`— y los términos referenciados del glosario raíz de `Vision-Producto.md` §9, que no se redefinen.

Dos advertencias de lectura. **`Pendiente` nombra dos estados distintos** —el de una cuenta y el de un trabajo—, los dos aparecen en las mismas secciones de esta especificación, y por eso se escribe siempre calificado: «cuenta `Pendiente`» o «trabajo en estado `Pendiente`». No se califican las enumeraciones del conjunto cerrado ni los identificadores literales. Y **«vista» tiene tres referentes** dentro de este proyecto de código —la página, el componente y la perspectiva de datos—; la forma que corresponde a cada uno está en `Glosario-Funcional.md` §3.1.

## 9. Artefactos de esta categoría que se omiten

La tabla maestra de `Rules-Especificacion-Funcional.md` §2.1 tiene ocho filas: se emiten cuatro artefactos y **no se emiten cuatro**, agrupados en tres puntos. El motivo de cada omisión está desarrollado en el [`README.md`](README.md) §3 de esta sección; acá se enumeran para que el índice maestro no deje huecos:

- `Reglas-De-Negocio/RN-XX-<Nombre>.md`: las quince reglas viven en `GeometriaFactory-Domain`. El fundamento está en §5 de este documento.
- `Modelo-Datos/Modelo-Conceptual.md` y `Modelo-Datos/reglas-conceptuales-de-modelo/RC-XX-<Nombre>.md`, que son **dos** de las ocho filas: la regla los marca obligatorios para `web-monolith`, y se omiten igual **como decisión técnica declarada** —`tiene_persistencia` es false y es deliberado—. Corresponde una ADR en 05-Arquitectura-Tecnica que la registre.
- `Definicion-<Concepto-Central>.md`: el concepto central del producto ya está documentado aguas arriba, en `Definicion-Modelo-De-Dominio.md` de `GeometriaFactory-Domain` y en `Definicion-Contrato-De-Fachada.md` de `GeometriaFactory-Visor`. Un documento de concepto acá los duplicaría.

## 10. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. Índice maestro de los diez casos de uso de `GeometriaFactory-Web`, primer proyecto de código del producto con actores humanos. Declara el criterio de recorte por objeto y papel con sus doce decisiones de fusión y partición, la numeración local, la matriz NB→CU→RN→US con sus nueve filas y las reglas referenciadas por identificador, la cobertura inversa, la correspondencia con las veintisiete previsiones de 01, el fundamento de que las reglas de negocio no se redacten acá, once restricciones transversales con su punto de verificación, la tabla de consumo del contrato de fachada del visualizador y la enumeración de los cuatro artefactos omitidos. | Analista Funcional senior (AG-02) |
| 1.1 | 2026-08-09 | **Propagación del `PRODUCT-INTAKE` 1.7**, con dos decisiones. **(a) F-26, reseteo de contraseña por el administrador**, con sus reglas RN-12 y RN-13, el invariante INV-09, el caso límite CL-7 reescrito y la exclusión X-2 retirada: §3 declara la quinta operación de CU-04 y el tercer curso de CU-03; §3.1 suma las dos decisiones de recorte que explican por qué ninguno de los dos es caso de uso propio; §6 suma **RT-12**, el confinamiento de la cuenta reseteada, con la aclaración de que esta pieza acota lo que se ofrece y que quien lo hace cumplir es la pieza de datos; §4 pasa a hablar de **trece reglas**. **(b) F-25 sube a `Must Have` y su frontera queda fijada**: §6 suma **RT-13** —el anfitrión manda dos valores de verdad y el bundle no consulta nada, en particular no lee la preferencia de movimiento reducido, que lee esta pieza y traduce—; §2 corrige el enunciado de RA-02, que decía **cinco** funciones y son **seis** desde el intake 1.6; y §7 incorpora `establecerMovimiento` a la tabla de consumo con su tercera consecuencia. La cabecera de trazabilidad deja de declarar cinco funciones y declara que ninguno de los siete contratos de uso vigentes cubre todavía F-26. Sube minor: agrega dos restricciones transversales y una función consumida, sin invalidar ninguna decisión previa. | Analista Funcional senior (AG-02) |
| 1.2 | 2026-08-09 | **Reconciliación con el `PRODUCT-INTAKE` 1.8 y con lo que las categorías vecinas ya emitieron.** **(a) RT-12 se precisa**: el intake 1.8 §4.1 declara que la cuenta con contraseña provisoria **se autentica y no obtiene sesión de trabajo**, y la restricción pasa a decirlo; su cita al intake sube a **1.8**, que es donde la sección citada cambió. La cita de RT-13 se deja en 1.7, porque §4 F-25 no cambió. **(b) §5 cierra un punto abierto**: RN-12 y RN-13 **ya tienen archivo** en `GeometriaFactory-Domain` y se enlazan como las otras once. **(c)** La cabecera de trazabilidad deja de declarar que ningún contrato de uso cubre F-26: `GeometriaFactory-Contracts` emitió **CU-08** y su conjunto cerrado pasó a **dieciséis** códigos. Sube minor: precisa una restricción transversal y cierra un punto abierto, sin agregar ni quitar casos de uso. | Analista Funcional senior (AG-02) |
| 1.3 | 2026-08-09 | Actualización por las dos decisiones del Product Owner sobre **F-26** que [`CU-04`](Casos-De-Uso/CU-04-Administrar-Las-Cuentas-De-La-Comision.md) 1.3 aplica: **resetear no exige que la cuenta esté habilitada** —el administrador resetea y habilita en el orden que quiera— y **la contraseña provisoria la produce el sistema y no la escribe el administrador**. Cambio acá: la cabecera de trazabilidad actualiza el conjunto cerrado de `GeometriaFactory-Contracts` de dieciséis a **diecisiete** códigos, por el `CONTRATO_RESETEO_NO_APLICABLE_A_CUENTA_SIN_CONTRASENA` que CU-08 1.2 emitió. **Ningún caso de uso se agrega ni se quita, y RT-12 no cambia**: la cuenta con provisoria sigue sin llegar a ninguna ruta que no sea el cambio de su propia contraseña. | Analista Funcional senior (AG-02) |
| 1.4 | 2026-08-09 | **Absorbe el `PRODUCT-INTAKE` **1.10**, que lleva las reglas del producto de trece a quince, y deja constancia por el hallazgo `F26-25`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0. **(a) RN-14 y RN-15.** El intake 1.10 §4.1 incorpora **RN-14** —la contraseña provisoria la produce el sistema, no es adivinable y no se repite— y **RN-15** —resetear no exige cuenta habilitada—, las dos sobre la capacidad **F-26** que esta sección ya modela. **§5** pasa de trece a **quince** reglas, con las cuatro que entraron después de la emisión inicial y su versión de entrada, y **§9** actualiza el recuento de la omisión de `Reglas-De-Negocio/`. La nota de la fila NB-08 de la matriz corrige de paso su enumeración de lo que las reglas restringen, que ahora incluye la credencial. **Esta sección sigue sin redactar ninguna regla**: las quince viven en `GeometriaFactory-Domain` y acá se referencian por identificador. **(b) Constancia por `F26-25`**: el informe registra que cuatro pasajes de contenido de este documento —«cuatro operaciones»→«cinco» del panel, «veintisiete US»→«treinta», «once reglas»→«trece»— se cambiaron **sin fila propia**, y que las filas 1.2 y 1.3 describen otros cambios. Se deja escrito acá y **no se reescribe ninguna fila histórica**: los tres cambios son reales y están vigentes. **Ningún caso de uso, restricción transversal ni criterio de aceptación cambia.** Sube minor. | Analista Funcional senior (AG-02) |
