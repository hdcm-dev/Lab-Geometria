# Especificación funcional — GeometriaFactory-Web

**Proyecto de código:** GeometriaFactory-Web
**Documento:** Especificacion-Funcional.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Analista Funcional senior (AG-02)
**Trazabilidad upstream:** `../../../00-Contexto/Vision-Producto.md` §3, §7, §9 (glosario raíz); `../../../00-Contexto/Alcance-Producto.md` §4.1, §5, §8; `../../../00-Contexto/Compatibilidad-Plataformas.md` §2.2 y §4; `../../../01-Necesidades-Negocio/Necesidades-Negocio.md` §2, §4, §5.3, y las nueve `NB-XX` de `../../../01-Necesidades-Negocio/Necesidades-De-Negocio/`; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (capacidades), §4.1 (las once reglas), §4.2 (modelo de estados y tabla de quién puede qué), §5 (historias de usuario), §6 (flujos 1, 2, 2.1, 3 y 4), §7 (casos límite), §13 y §14 (composición y reglas RA-01 a RA-03), §17.6 íntegro (P.1 a P.12); `../../GeometriaFactory-Contracts/02-Especificacion-Funcional/` (siete contratos de uso, catorce códigos de error y tres señales); `../../GeometriaFactory-Visor/02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md` (cinco funciones y siete condiciones)
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
| RA-02 | El bundle del visualizador es un visualizador puro | La pieza pública lo invoca **sólo** por sus cinco funciones. Ningún componente toca su interior ni manipula el elemento de dibujo por su cuenta |
| RA-03 | Todo lo que el navegador deba obtener de la pieza de datos pasa por la pieza pública | Ningún mensaje mostrado incluye la dirección de un servicio interno, y toda respuesta de error del contrato se traduce a presentación propia |

La decisión más consecuente de las tres, en términos de lo que la persona puede observar, es que **la credencial de sesión vive en el estado del circuito, del lado del servidor, y nunca llega al navegador**. Está escrita como criterio de aceptación verificable en CU-02, CA-02.

## 3. Catálogo de casos de uso

| CU | Qué hace la persona | Actor primario | NB que sostiene | Estado | Enlace |
| --- | --- | --- | --- | --- | --- |
| CU-01 | Registrarse en el laboratorio con correo, nombre y apellido, sin elegir contraseña | Alumno | NB-02, NB-01 | Propuesto | [CU-01](../../Casos-De-Uso/CU-10001-Registrar-La-Cuenta-De-Alumno.md) |
| CU-02 | Iniciar y cerrar sesión, con la credencial custodiada del lado del servidor y las rutas protegidas por papel | Persona con cuenta | NB-02, NB-01 | Propuesto | [CU-02](../../Casos-De-Uso/CU-10002-Iniciar-Y-Cerrar-Sesion-Sin-Exponer-La-Credencial.md) |
| CU-03 | Establecer la contraseña en el primer ingreso efectivo y cambiarla presentando la vigente | Persona con cuenta habilitada | NB-02 | Propuesto | [CU-03](../../Casos-De-Uso/CU-10003-Establecer-Y-Cambiar-La-Contrasena-Propia.md) |
| CU-04 | Ver la lista de cuentas y habilitar, bloquear, rehabilitar o dar de baja, con confirmación escrita | Administrador | NB-01, NB-02 | Propuesto | [CU-04](../../Casos-De-Uso/CU-10004-Administrar-Las-Cuentas-De-La-Comision.md) |
| CU-05 | Cargar un trabajo, previsualizarlo y **enviarlo**, con el estado que la interpretación decide | Alumno | NB-04, NB-03, NB-05 | Propuesto | [CU-05](../../Casos-De-Uso/CU-10005-Enviar-Un-Trabajo-Y-Ver-El-Resultado-De-La-Interpretacion.md) |
| CU-06 | Ver los trabajos propios con sus cuatro estados, y editar o eliminar sólo en borrador | Alumno | NB-03, NB-09 | Propuesto | [CU-06](../../Casos-De-Uso/CU-10006-Consultar-El-Listado-Propio-Y-Operar-Sobre-El-Borrador.md) |
| CU-07 | Abrir un trabajo y explorarlo: datos, texto, escena y árbol sincronizados por índice | Alumno dueño, con el administrador en FA-01 | NB-06, NB-05, NB-07, NB-09, NB-04 | Propuesto | [CU-07](../../Casos-De-Uso/CU-10007-Abrir-Un-Trabajo-Y-Explorarlo-En-Escena-Y-Arbol.md) |
| CU-08 | Recorrer la entrega de la comisión, agrupada y filtrada por alumno, sin los borradores | Administrador | NB-07, NB-09 | Propuesto | [CU-08](../../Casos-De-Uso/CU-10008-Recorrer-La-Entrega-De-La-Comision.md) |
| CU-09 | Aprobar o rechazar un trabajo con comentario opcional, y retirar cualquiera que ve | Administrador | NB-09, NB-07 | Propuesto | [CU-09](../../Casos-De-Uso/CU-10009-Resolver-Un-Trabajo-Con-Comentario-Opcional.md) |
| CU-10 | Seguir usando la aplicación cuando algo se corta, con aviso explícito y sin pantalla rota | Persona que usa el laboratorio | NB-08 | Propuesto | [CU-10](../../Casos-De-Uso/CU-10010-Sostener-La-Aplicacion-En-Estado-Degradado-Y-Reconexion.md) |

Diez casos de uso, sobre el mínimo de **ocho** que `Rules-Especificacion-Funcional.md` §2.2 fija para `web-monolith`. El mínimo es piso; el techo lo fijó la cobertura completa de las nueve necesidades de negocio, que se verifica en §4.1.

### 3.1 Criterio de recorte

El recorte sigue **el objeto sobre el que la persona actúa y el papel con el que actúa** —la cuenta, la sesión, la credencial, el trabajo propio, el trabajo de la comisión, la disponibilidad—, y no las capacidades `F-XX` una por una: varias capacidades se ejercen en la misma pantalla y en el mismo acto, y partirlas por identificador habría producido casos de uso que son sub-flujos.

| Decisión | Fundamento |
| --- | --- |
| Se separó CU-02 de CU-01 y de CU-03 | El canje de credenciales es la superficie donde vive la exigencia de que la credencial de sesión no llegue al navegador, y tiene que leerse sola. Escondida dentro del registro o del establecimiento de contraseña, esa restricción no tendría dónde verificarse |
| Se fusionaron el establecimiento y el cambio de contraseña en CU-03 | Es el mismo objeto —la credencial propia—, el mismo actor y la misma pantalla salvo un campo. Dos casos de uso habrían duplicado la superficie sin declarar ninguna decisión distinta |
| Se fusionaron las cuatro operaciones de cuenta en CU-04 | Habilitar, bloquear, rehabilitar y dar de baja se ejercen desde la misma lista, con el mismo actor. La baja se distingue por su confirmación escrita, que entra como flujo alternativo y no como caso de uso propio |
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

La numeración de esta sección es contigua de `CU-01` a `CU-10`, sin huecos. Las veintisiete historias de usuario previstas, `US-01` a `US-27`, son una previsión de esta categoría; las confirma la categoría 06 al redactarlas.

## 4. Matriz NB→CU→RN→US

Las `RN-XX` de la tercera columna **viven en `GeometriaFactory-Domain`** y se referencian por identificador con enlace relativo. Ninguna se redacta acá.

| NB | CU | RN | US a generar en 06 |
| --- | --- | --- | --- |
| [NB-01](../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00001-Control-De-Admision-Al-Laboratorio.md) | CU-01, CU-02, CU-04 | [RN-01](../../../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02001-Administrador-Unico-Y-Papeles-Fijos.md), [RN-02](../../../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02002-Correo-Del-Alumno-Unico.md), [RN-06](../../../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02006-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md), [RN-07](../../../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02007-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md) | US-01, US-02, US-03, US-04, US-05, US-08, US-09, US-10 |
| [NB-02](../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00002-Identidad-Propia-Del-Alumno-Sin-Correo.md) | CU-01, CU-02, CU-03, CU-04 | [RN-02](../../../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02002-Correo-Del-Alumno-Unico.md), [RN-06](../../../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02006-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md) | US-01, US-02, US-03, US-04, US-05, US-06, US-07 |
| [NB-03](../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00003-Trabajo-Con-Dueno-Estado-Y-Persistencia.md) | CU-05, CU-06 | [RN-03](../../../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02003-Trabajo-Ajeno-Indistinguible-De-Inexistente.md), [RN-04](../../../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02004-Eliminacion-Acotada-Al-Borrador.md), [RN-05](../../../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02005-Finalizacion-Sin-Errores-De-Validacion.md), [RN-08](../../../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02008-Texto-Original-Conservado-Integro.md) | US-11, US-12, US-13, US-14, US-15, US-16, US-17 |
| [NB-04](../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00004-Interpretacion-Fiel-Del-Dato-Del-Alumno.md) | CU-05, CU-07 | [RN-03](../../../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02003-Trabajo-Ajeno-Indistinguible-De-Inexistente.md), [RN-05](../../../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02005-Finalizacion-Sin-Errores-De-Validacion.md), [RN-08](../../../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02008-Texto-Original-Conservado-Integro.md), [RN-09](../../../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02009-Observacion-De-Error-Con-Posicion-Y-Campo.md) | US-11, US-12, US-13, US-14, US-18, US-19, US-20, US-21 |
| [NB-05](../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00005-Visibilidad-Del-Error-De-Calculo.md) | CU-05, CU-07 | [RN-05](../../../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02005-Finalizacion-Sin-Errores-De-Validacion.md), [RN-09](../../../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02009-Observacion-De-Error-Con-Posicion-Y-Campo.md) | US-13, US-14, US-19, US-20 |
| [NB-06](../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00006-Visualizacion-Dentro-Del-Producto.md) | CU-07 | [RN-03](../../../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02003-Trabajo-Ajeno-Indistinguible-De-Inexistente.md), [RN-08](../../../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02008-Texto-Original-Conservado-Integro.md), [RN-09](../../../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02009-Observacion-De-Error-Con-Posicion-Y-Campo.md) | US-18, US-19, US-20, US-21 |
| [NB-07](../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00007-Revision-De-La-Comision-En-Un-Solo-Lugar.md) | CU-07, CU-08, CU-09 | [RN-03](../../../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02003-Trabajo-Ajeno-Indistinguible-De-Inexistente.md), [RN-11](../../../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02011-El-Administrador-No-Ve-Los-Borradores.md) | US-18, US-19, US-20, US-21, US-22, US-23, US-24, US-25 |
| [NB-08](../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00008-Alcance-Del-Laboratorio-Desde-El-Aula.md) | CU-10 | Ninguna, con el motivo declarado en §5 y en CU-10 §9 | US-26, US-27 |
| [NB-09](../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00009-Desenlace-Explicito-De-La-Entrega.md) | CU-06, CU-07, CU-08, CU-09 | [RN-04](../../../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02004-Eliminacion-Acotada-Al-Borrador.md), [RN-10](../../../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02010-Desenlace-Exclusivo-Del-Administrador-Y-Terminalidad.md), [RN-11](../../../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02011-El-Administrador-No-Ve-Los-Borradores.md) | US-15, US-16, US-17, US-18, US-19, US-20, US-21, US-22, US-23, US-24, US-25 |

### 4.1 Cobertura inversa: de NB a CU

Ninguna necesidad queda sin caso de uso y ningún caso de uso queda huérfano. Este proyecto de código no sostiene cada necesidad en la misma medida, y declararlo es más útil que una fila uniforme.

| NB | Grado en que la pieza pública la sostiene | Qué queda en otro proyecto de código |
| --- | --- | --- |
| NB-01 | Sostenida en lo que le corresponde: es el panel donde el administrador ve la lista y ejerce las cuatro operaciones, y donde la confirmación escrita de la baja se exige | La unicidad del administrador y el arrastre de trabajos son invariantes de `GeometriaFactory-Domain` |
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

## 5. Por qué esta sección no redacta reglas de negocio

Las **once reglas** del producto viven en `GeometriaFactory-Domain`, que es donde se hacen cumplir. Acá se **referencian por identificador** y no se redactan. El motivo no es formal: **la pieza pública no puede ser la última defensa de ninguna regla, porque el navegador no es confiable.** Ocultar un botón, no armar una ruta o no ofrecer una acción son decisiones de presentación legítimas y necesarias, pero no hacen cumplir nada; por eso varios criterios de aceptación de esta sección verifican la acotación **forzando la solicitud sin pasar por la pantalla** —CU-06 CA-04, CU-09 CA-04 y CA-05—.

La única fila de la matriz sin regla referenciada es NB-08, en CU-10. Su motivo está declarado en el propio caso de uso: las once reglas restringen el dominio —cuentas, trabajos, estados y observaciones— y CU-10 no toca ninguno de esos objetos. Lo que sí lo restringe es la regla de arquitectura RA-03, cuyo enunciado vive en `PRODUCT-INTAKE` §14 y que se verifica en CU-10 CA-02. Inventar una `RN-XX` para llenar la celda habría sido redactar una regla que no existe aguas arriba.

Dos advertencias de lectura sobre los archivos de `GeometriaFactory-Domain`, heredadas de la especificación de `GeometriaFactory-Contracts` §5: `RN-04-Eliminacion-Acotada-Al-Borrador.md` **cubre hoy los dos caminos de eliminación**, el del alumno acotado al borrador y el del administrador sobre cualquier trabajo que ve, y `RN-05-Finalizacion-Sin-Errores-De-Validacion.md` **corta hoy en el envío y no en el cierre**. Los dos slugs quedaron desactualizados respecto de su enunciado y se decidió aguas arriba no renombrarlos. Se cita el contenido vigente, no el que sugiere el nombre.

## 6. Restricciones transversales de la pieza pública

Valen para los diez casos de uso y no se repiten en cada uno más allá de su criterio de aceptación correspondiente.

| Id | Restricción | Origen | Dónde se verifica |
| --- | --- | --- | --- |
| RT-01 | Ninguna llamada a la pieza de datos se origina en el navegador: todas salen del servidor de la pieza pública | `PRODUCT-INTAKE` §14 (RA-01), §17.6 P.3 | CU-01 CA-05, CU-02 CA-07, CU-05 CA-06, CU-07 CA-10 |
| RT-02 | **La credencial de sesión vive en el estado del circuito, del lado del servidor, y no aparece nunca en el navegador.** El navegador conserva sólo una marca de sesión que no la transporta | `PRODUCT-INTAKE` §17.6 P.4 y P.5 | CU-02 CA-02, y CU-03 CA-05 para las contraseñas |
| RT-03 | Ningún mensaje mostrado a la persona incluye la dirección de un servicio interno, un nombre de archivo de datos ni una traza de la implementación | `PRODUCT-INTAKE` §14 (RA-03), §17.6 P.5 | CU-10 CA-02, y las filas de estado degradado de los §6 de CU-01, CU-04 a CU-09 |
| RT-04 | El bundle del visualizador se invoca **exclusivamente** por sus cinco funciones. Ningún componente accede a su interior ni manipula el elemento de dibujo por su cuenta | `PRODUCT-INTAKE` §14 (RA-02), §17.6 P.3 (regla de aislamiento del visor) | CU-05 §10, CU-07 §7 y §10 de este documento |
| RT-05 | `destruir` se invoca al descartar el componente que aloja la instancia del visualizador. **No es opcional**: sin eso, recorrer trabajos acumula contextos gráficos en el navegador | `PRODUCT-INTAKE` §17.6 P.11 punto 5 | CU-07 CA-05, CU-05 CA-07 |
| RT-06 | La pieza pública **no guarda estado propio**. No hay copia local de los datos, ni caché, ni réplica: cuando la pieza de datos no está, no hay nada que mostrar y se declara el estado degradado | `PRODUCT-INTAKE` §17.6 P.4, §7 (CL-8) | CU-04 §6, CU-06 §6, CU-08 §6, CU-10 §7 |
| RT-07 | La indisponibilidad se presenta siempre como **estado degradado explícito**, nunca como excepción sin manejar y nunca como pantalla rota. El listado vacío se distingue del fallo **por el tipo recibido y no por el conteo** | `PRODUCT-INTAKE` §7 (CL-2), §17.6 P.10 | CU-10 CA-01, CA-07; CU-06 CA-06; CU-08 CA-06 |
| RT-08 | El texto original del trabajo se envía **carácter por carácter** tal como la persona lo pegó, y no se reescribe en ningún punto del recorrido | `PRODUCT-INTAKE` §4.1 (RN-08), §9 (X-4) | CU-05 CA-02 |
| RT-09 | Ninguna ruta del panel es accesible sin sesión, y un alumno con sesión no accede a ninguna ruta de administrador. Esto **acota lo que se ofrece**; la verificación de pertenencia y de papel la hace la pieza de datos en cada solicitud | `PRODUCT-INTAKE` §17.6 P.5 | CU-02 CA-05, CU-04 CA-07 |
| RT-10 | Durante la interacción con la escena no hay tráfico de circuito hacia el servidor, y el texto del trabajo viaja del servidor al navegador **una sola vez por trabajo** | `PRODUCT-INTAKE` §17.6 P.10 | CU-07 CA-10 |
| RT-11 | Toda combinación de navegador sin capacidad gráfica tridimensional se considera no soportada **para la escena**, y el resto del producto sigue disponible | `PRODUCT-INTAKE` §17.6 P.9 | CU-05 FA-04, CU-07 FA-05 |

## 7. Consumo del contrato de fachada del visualizador

El bundle expone **cinco funciones** y siete códigos de condición, declarados en [`Definicion-Contrato-De-Fachada.md`](../../GeometriaFactory-Visor/02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md). Esta tabla declara, una sola vez, qué función consume cada caso de uso; los casos de uso no la repiten más allá de su fila de trazabilidad.

| Caso de uso | `inicializar` | `cargarJson` | `seleccionarPieza` | `redimensionar` | `destruir` |
| --- | --- | --- | --- | --- | --- |
| CU-05, previsualización previa al envío | Sí | Sí | — | — | Sí |
| CU-07, vista de trabajo | Sí | Sí | Sí | Sí | Sí |
| Los otros ocho | — | — | — | — | — |

Dos consecuencias que las categorías aguas abajo no deben perder:

1. **El componente anfitrión de la pieza pública es quien opera el ciclo de vida.** La fachada no observa tamaños ni decide cuándo ajustar: por eso `redimensionar` lo invoca CU-07 y no ocurre solo.
2. **El resultado de dibujo no lleva observaciones.** Las piezas que la fachada no dibuja se enumeran por su índice, y eso **no** las convierte en errores del trabajo: quien decide si el trabajo verifica es la pieza de datos. CU-05 §10 y CU-07 §10 lo declaran para que la vista no las mezcle.

## 8. Glosario

El vocabulario de esta categoría vive en [`Glosario-Funcional.md`](Glosario-Funcional.md), que declara los términos que la pieza pública acuña, **tres términos con más de un referente** —«vista», «pieza» y `Pendiente`— y los términos referenciados del glosario raíz de `Vision-Producto.md` §9, que no se redefinen.

Dos advertencias de lectura. **`Pendiente` nombra dos estados distintos** —el de una cuenta y el de un trabajo—, los dos aparecen en las mismas secciones de esta especificación, y por eso se escribe siempre calificado: «cuenta `Pendiente`» o «trabajo en estado `Pendiente`». No se califican las enumeraciones del conjunto cerrado ni los identificadores literales. Y **«vista» tiene tres referentes** dentro de este proyecto de código —la página, el componente y la perspectiva de datos—; la forma que corresponde a cada uno está en `Glosario-Funcional.md` §3.1.

## 9. Artefactos de esta categoría que se omiten

La tabla maestra de `Rules-Especificacion-Funcional.md` §2.1 tiene ocho filas: se emiten cuatro artefactos y **no se emiten cuatro**, agrupados en tres puntos. El motivo de cada omisión está desarrollado en el [`README.md`](README.md) §3 de esta sección; acá se enumeran para que el índice maestro no deje huecos:

- `Reglas-De-Negocio/RN-XX-<Nombre>.md`: las once reglas viven en `GeometriaFactory-Domain`. El fundamento está en §5 de este documento.
- `Modelo-Datos/Modelo-Conceptual.md` y `Modelo-Datos/reglas-conceptuales-de-modelo/RC-XX-<Nombre>.md`, que son **dos** de las ocho filas: la regla los marca obligatorios para `web-monolith`, y se omiten igual **como decisión técnica declarada** —`tiene_persistencia` es false y es deliberado—. Corresponde una ADR en 05-Arquitectura-Tecnica que la registre.
- `Definicion-<Concepto-Central>.md`: el concepto central del producto ya está documentado aguas arriba, en `Definicion-Modelo-De-Dominio.md` de `GeometriaFactory-Domain` y en `Definicion-Contrato-De-Fachada.md` de `GeometriaFactory-Visor`. Un documento de concepto acá los duplicaría.

## 10. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. Índice maestro de los diez casos de uso de `GeometriaFactory-Web`, primer proyecto de código del producto con actores humanos. Declara el criterio de recorte por objeto y papel con sus doce decisiones de fusión y partición, la numeración local, la matriz NB→CU→RN→US con sus nueve filas y las reglas referenciadas por identificador, la cobertura inversa, la correspondencia con las veintisiete previsiones de 01, el fundamento de que las reglas de negocio no se redacten acá, once restricciones transversales con su punto de verificación, la tabla de consumo del contrato de fachada del visualizador y la enumeración de los cuatro artefactos omitidos. | Analista Funcional senior (AG-02) |
