# Especificación funcional — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** Especificacion-Funcional.md
**Versión:** 2.0
**Estado:** Propuesto
**Fecha:** 2026-08-16
**`tipo_unidad_entrega` (D8):** `web-monolith`
**Proyectos de código que la componen:** `GeometriaFactory-Web`, `GeometriaFactory-Visor` y `GeometriaFactory-Contracts`
**Consolida a:** el documento homónimo de `GeometriaFactory-Visor`, por `Audit/Migracion-M10-Consolidacion-Fusion.md` 1.2 §4

---

## 0. Cómo leer este documento

**La unidad de entrega tiene un solo documento de esta clase**, y cada sección lleva **una subsección
por proyecto de código**, con su texto **transpuesto sin reescritura**.

**Las dos secciones de cada apartado son la del portal y la del bundle del visor.** **7 secciones existen sólo en `GeometriaFactory-Visor`** —«Propósito y alcance de esta categoría», «Qué es y qué no es este proyecto de código», «Documento de concepto central»…—, y son las que el portal no podía declarar porque describen el componente empaquetado que viaja adentro.

---

## 1. Propósito y alcance de esta especificación

### 1.1 `GeometriaFactory-Web`

`GeometriaFactory-Web` es la **pieza pública** del producto Fábrica de Geometría: el front desplegado en el hosting público y **el único punto de contacto del navegador**. Es una de las dos unidades de entrega del producto y nivel 1 del orden topológico; compila contra `GeometriaFactory-Contracts` y contra el bundle de `GeometriaFactory-Visor`, y habla con la pieza de datos **en tiempo de ejecución, servidor a servidor**, lo que no es dependencia de compilación.

Es el primer proyecto de código del producto cuyos casos de uso tienen **actores humanos**: el alumno de la comisión y el docente en su papel de administrador. Los cuatro proyectos de código ya especificados tenían por actor al código que los consume.

Lo que esta especificación **no** decide: las reglas del dominio, que viven y se hacen cumplir en `GeometriaFactory-Domain`; la forma de los puntos de acceso del servicio, que es de `GeometriaFactory-Api`; el diseño de las pantallas, que es de 03-UX-UI-DX; la arquitectura y los registros de decisión, que son de 05; y las pruebas, que son de 08. Tampoco decide nada del interior del bundle del visualizador, que se consume exclusivamente por su fachada.

## 2. Qué decide este proyecto de código

### 2.1 `GeometriaFactory-Web`

Decide **qué hace la persona y qué ve**, y con qué contrato de uso obtiene cada cosa. Tres decisiones de nivel producto lo gobiernan y son la razón de ser de la topología entera; un caso de uso que viole cualquiera de las tres es un defecto y no una alternativa de diseño:

| Regla | Enunciado | Cómo se manifiesta acá |
| --- | --- | --- |
| RA-01 | Ningún guion del navegador invoca la pieza de datos | Todas las llamadas de los diez casos de uso salen del servidor de la pieza pública. Se verifica contando peticiones del navegador: el umbral es exactamente 0 |
| RA-02 | El bundle del visualizador es un visualizador puro | La pieza pública lo invoca **sólo** por sus seis funciones, y es **ella** la que consulta el entorno del navegador y le manda el resultado: el bundle no consulta nada. Ningún componente toca su interior ni manipula el elemento de dibujo por su cuenta |
| RA-03 | Todo lo que el navegador deba obtener de la pieza de datos pasa por la pieza pública | Ningún mensaje mostrado incluye la dirección de un servicio interno, y toda respuesta de error del contrato se traduce a presentación propia |

La decisión más consecuente de las tres, en términos de lo que la persona puede observar, es que **la credencial de sesión vive en el estado del circuito, del lado del servidor, y nunca llega al navegador**. Está escrita como criterio de aceptación verificable en CU-10002, CA-02.

## 3. Catálogo de casos de uso

### 3.1 `GeometriaFactory-Web`

| CU | Qué hace la persona | Actor primario | NB que sostiene | Estado | Enlace |
| --- | --- | --- | --- | --- | --- |
| CU-10001 | Registrarse en el laboratorio con correo, nombre y apellido, sin elegir contraseña | Alumno | NB-00002, NB-00001 | Propuesto | [CU-10001](Casos-De-Uso/CU-10001-Registrar-La-Cuenta-De-Alumno.md) |
| CU-10002 | Iniciar y cerrar sesión, con la credencial custodiada del lado del servidor y las rutas protegidas por papel | Persona con cuenta | NB-00002, NB-00001 | Propuesto | [CU-10002](Casos-De-Uso/CU-10002-Iniciar-Y-Cerrar-Sesion-Sin-Exponer-La-Credencial.md) |
| CU-10003 | Cambiar la contraseña en el primer ingreso presentando la **provisoria** como vigente, cambiarla después presentando la vigente, y cambiarla **obligada** tras un reseteo del administrador. **Los tres cursos son el mismo formulario y el mismo contrato** desde `PRODUCT-INTAKE` 1.13 (RN-10016) | Persona con cuenta habilitada | NB-00002 | Propuesto | [CU-10003](Casos-De-Uso/CU-10003-Establecer-Y-Cambiar-La-Contrasena-Propia.md) |
| CU-10004 | Ver la lista de cuentas y habilitar, bloquear, rehabilitar, **resetear la contraseña** o dar de baja, con confirmación escrita en la baja. **Habilitar y resetear le muestran al administrador la contraseña provisoria para que la comunique** (RN-10016, RN-10014) | Administrador | NB-00001, NB-00002 | Propuesto | [CU-10004](Casos-De-Uso/CU-10004-Administrar-Las-Cuentas-De-La-Comision.md) |
| CU-10005 | Cargar un trabajo, previsualizarlo y **enviarlo**, con el estado que la interpretación decide | Alumno | NB-00004, NB-00003, NB-00005 | Propuesto | [CU-10005](Casos-De-Uso/CU-10005-Enviar-Un-Trabajo-Y-Ver-El-Resultado-De-La-Interpretacion.md) |
| CU-10006 | Ver los trabajos propios con sus cuatro estados, y editar o eliminar sólo en borrador | Alumno | NB-00003, NB-00009 | Propuesto | [CU-10006](Casos-De-Uso/CU-10006-Consultar-El-Listado-Propio-Y-Operar-Sobre-El-Borrador.md) |
| CU-10007 | Abrir un trabajo y explorarlo: datos, texto, escena y árbol sincronizados por índice | Alumno dueño, con el administrador en FA-01 | NB-00006, NB-00005, NB-00007, NB-00009, NB-00004 | Propuesto | [CU-10007](Casos-De-Uso/CU-10007-Abrir-Un-Trabajo-Y-Explorarlo-En-Escena-Y-Arbol.md) |
| CU-10008 | Recorrer la entrega de la comisión, agrupada y filtrada por alumno, sin los borradores | Administrador | NB-00007, NB-00009 | Propuesto | [CU-10008](Casos-De-Uso/CU-10008-Recorrer-La-Entrega-De-La-Comision.md) |
| CU-10009 | Aprobar o rechazar un trabajo con comentario opcional, y retirar cualquiera que ve | Administrador | NB-00009, NB-00007 | Propuesto | [CU-10009](Casos-De-Uso/CU-10009-Resolver-Un-Trabajo-Con-Comentario-Opcional.md) |
| CU-10010 | Seguir usando la aplicación cuando algo se corta, con aviso explícito y sin pantalla rota | Persona que usa el laboratorio | NB-00008 | Propuesto | [CU-10010](Casos-De-Uso/CU-10010-Sostener-La-Aplicacion-En-Estado-Degradado-Y-Reconexion.md) |

Diez casos de uso, sobre el mínimo de **ocho** que `Rules-Especificacion-Funcional.md` §2.2 fija para `web-monolith`. El mínimo es piso; el techo lo fijó la cobertura completa de las nueve necesidades de negocio, que se verifica en §4.1.

### 3.1 Criterio de recorte

El recorte sigue **el objeto sobre el que la persona actúa y el papel con el que actúa** —la cuenta, la sesión, la credencial, el trabajo propio, el trabajo de la comisión, la disponibilidad—, y no las capacidades `F-XX` una por una: varias capacidades se ejercen en la misma pantalla y en el mismo acto, y partirlas por identificador habría producido casos de uso que son sub-flujos.

| Decisión | Fundamento |
| --- | --- |
| Se separó CU-10002 de CU-10001 y de CU-10003 | El canje de credenciales es la superficie donde vive la exigencia de que la credencial de sesión no llegue al navegador, y tiene que leerse sola. Escondida dentro del registro o del establecimiento de contraseña, esa restricción no tendría dónde verificarse |
| Se fusionaron el establecimiento y el cambio de contraseña en CU-10003 | Es el mismo objeto —la credencial propia—, el mismo actor y la misma pantalla salvo un campo. Dos casos de uso habrían duplicado la superficie sin declarar ninguna decisión distinta |
| Se absorbió el **cambio forzado** en CU-10003 como tercer curso, y no como caso de uso propio | Mismo objeto, mismo actor y **el mismo formulario que el cambio**: lo único que cambia es de dónde se llega y que no hay salida. Lo que **sí** es decisión propia y por eso se declara con criterio de aceptación es el confinamiento, y ése no vive en el formulario sino en el guard, que CU-10002 FA-07 y CU-10003 FA-05 declaran |
| Se absorbió el **reseteo de contraseña** en CU-10004, FA-06 | El intake lo pide explícitamente «desde el mismo panel» donde el administrador habilita, bloquea y da de baja (F-26). Mismo actor, misma lista, misma fila: un caso de uso propio para la quinta operación de la misma superficie habría sido un sub-flujo, por el mismo criterio con el que las otras cuatro quedaron juntas |
| Se fusionaron las **cinco** operaciones de cuenta en CU-10004 | Habilitar, bloquear, rehabilitar, resetear la contraseña y dar de baja se ejercen desde la misma lista, con el mismo actor. La baja se distingue por su confirmación escrita, y el reseteo por su confirmación simple y su comunicación de la provisoria; las dos entran como flujo alternativo y no como caso de uso propio |
| Se absorbió el alta inicial del administrador en CU-10004, FA-03 | Su actor primario es el administrador y su superficie es la misma ruta de administración. Un caso de uso propio para un formulario que se usa **una vez en la vida del laboratorio** habría sido un sub-flujo |
| Se separó CU-10005 de CU-10006 | Enviar y recorrer son actos distintos con resultados distintos: uno decide el estado del trabajo, el otro lo consulta. Fusionarlos habría escondido la propiedad central del producto —el envío es la única acción de guardado— dentro de un caso de uso de listado |
| Se absorbió el paso a estado `Pendiente` dentro de CU-10005 | Con el envío como acción única no hay una operación separada que llevar a un caso de uso propio: el estado es una salida del mismo envío |
| Se separó CU-10006 de CU-10008 | Son dos listados con **alcance distinto y actor distinto**: el propio del alumno incluye sus borradores, el de la comisión los excluye por regla de dominio. Un solo caso de uso habría dejado el recorte sin lugar donde verificarse |
| Se emitió CU-10007 como caso de uso propio y único para los dos papeles | La vista de trabajo es idéntica para el alumno dueño y para el administrador, y esa identidad es un criterio de éxito de negocio —«4 de 4 elementos»—. Dos casos de uso la habrían duplicado y habrían admitido que divergieran. El administrador entra como actor secundario en FA-01, no como segundo actor primario |
| Se emitió CU-10009 como caso de uso propio | El desenlace tiene actor exclusivo, precondición de estado y regla de dominio propios. Absorberlo en CU-10008 habría mezclado recorrer con decidir, que es exactamente la frontera que NB-00007 y NB-00009 declaran entre sí |
| Se fusionaron aprobar y rechazar en CU-10009 | Comparten pantalla, precondición, errores y regla. Se distinguen por el valor de una decisión de conjunto cerrado |
| Se absorbió el retiro de trabajos por el administrador en CU-10009, FA-03 | Mismo actor, mismo panel y misma solicitud de eliminación que ya usa el alumno; lo que difiere es la regla que la acota, y las reglas viven en el dominio |
| Se hizo transversal CU-10010 | Los otros nueve comparten sus caminos de indisponibilidad. Es el caso de uso transversal de manejo de errores que sugiere §5.2 de las reglas, y concentra en un solo lugar la superficie donde RA-03 se puede violar |

### 3.2 Numeración local

Los identificadores `CU-XX`, `US-XX` y `CA-XX` de esta sección son **locales a `GeometriaFactory-Web`**. `Necesidades-Negocio.md` §5.3 prevé veintisiete casos de uso `CU-10001` a `CU-10027` a nivel producto; esa previsión se reparte entre las especificaciones funcionales de los siete proyectos de código, cada una con su numeración contigua desde `CU-10001`, porque la categoría 02 es de nivel proyecto de código. La correspondencia se lee en §4.2.

La numeración de esta sección es contigua de `CU-10001` a `CU-10010`, sin huecos. Las historias de usuario previstas son una previsión de esta categoría; las confirma la categoría 06 al redactarlas. Eran veintisiete, `US-10001` a `US-10027`, y son **treinta** desde el `PRODUCT-INTAKE` 1.7: `US-10028` —cambio forzado que levanta la marca—, `US-10029` —confinamiento de la cuenta con la contraseña reseteada— y `US-10030` —reseteo desde el panel, conservando la cuenta y sus trabajos—.

### 3.2 `GeometriaFactory-Visor`

| ID | Caso de uso | Función de la fachada | Estado | Enlace |
| --- | --- | --- | --- | --- |
| CU-12001 | Inicializar una instancia del visor sobre un elemento de dibujo | `inicializar(elemento, opciones)` | Propuesto | [CU-12001](../05-Arquitectura-Tecnica/Contrato-Componente-Visor/CU-12001-Inicializar-Instancia-Del-Visor.md) |
| CU-12002 | Cargar el texto del trabajo y dibujar sus piezas | `cargarJson(id, texto)` | Propuesto | [CU-12002](../05-Arquitectura-Tecnica/Contrato-Componente-Visor/CU-12002-Cargar-El-Texto-Del-Trabajo-Y-Dibujar.md) |
| CU-12003 | Seleccionar una pieza por su índice | `seleccionarPieza(id, indice)` | Propuesto | [CU-12003](../05-Arquitectura-Tecnica/Contrato-Componente-Visor/CU-12003-Seleccionar-Una-Pieza-Por-Su-Indice.md) |
| CU-12004 | Redimensionar la escena al elemento de dibujo | `redimensionar(id)` | Propuesto | [CU-12004](../05-Arquitectura-Tecnica/Contrato-Componente-Visor/CU-12004-Redimensionar-La-Escena.md) |
| CU-12005 | Destruir la instancia y liberar sus recursos | `destruir(id)` | Propuesto | [CU-12005](../05-Arquitectura-Tecnica/Contrato-Componente-Visor/CU-12005-Destruir-La-Instancia-Y-Liberar-Recursos.md) |
| CU-12006 | Ejercitar la fachada completa sin backend | Las seis, en recorrido | Propuesto | [CU-12006](../05-Arquitectura-Tecnica/Contrato-Componente-Visor/CU-12006-Ejercitar-La-Fachada-Sin-Backend.md) |
| CU-12007 | Gobernar el movimiento automático de la escena sobre una instancia viva | `establecerMovimiento(id, opciones)` | Propuesto | [CU-12007](../05-Arquitectura-Tecnica/Contrato-Componente-Visor/CU-12007-Gobernar-El-Movimiento-Automatico-De-La-Escena.md) |

Siete casos de uso, sobre un mínimo de cinco declarado para el tipo `library` en `Rules-Especificacion-Funcional.md` §2.2.

### 3.1 Criterio de recorte

1. **Una función de la fachada, un caso de uso.** Cada una de las seis funciones es un contrato de uso independiente: tiene su propio actor invocante, sus propias precondiciones y su propio conjunto de condiciones de error. Fusionarlas habría producido un caso de uso con más de un actor primario y con flujos que no se disparan entre sí. Es este criterio el que obliga a emitir `CU-12007` cuando el Product Owner agrega la sexta función el 2026-08-09: `establecerMovimiento` no se dispara desde ninguno de los otros seis y no cabe como flujo alternativo de `CU-12001`, porque su precondición es una instancia **ya viva** cuyo estado de movimiento se quiere cambiar, y no la creación de una instancia.
2. **Un caso de uso transversal, y sólo uno.** `CU-12006` recorre las seis funciones desde una página integradora sin backend. Existe porque las **seis** propiedades que verifica —cero red, cero persistencia, se ejercita sin backend, disposición determinista, liberación de recursos y ausencia de fallo silencioso, enumeradas con su umbral en §6— son transversales: repartidas como excepciones de los otros seis, ninguno las verificaría juntas, y es además el sample S-1 del producto, que el intake declara como el que demuestra el punto de extensión.
3. **Nada más.** No hay casos de uso de configuración, de sesión, de obtención de datos ni de validación, porque el proyecto de código no hace ninguna de esas cosas. Rotar y acercar con el mouse tampoco es caso de uso: son gestos que la instancia atiende sobre la escena ya creada y no atraviesan ninguna de las seis funciones. El **movimiento automático**, en cambio, sí atraviesa la fachada —`inicializar` lo fija al nacer y `establecerMovimiento` lo cambia en vivo— y por eso tiene caso de uso, `CU-12007`.

### 3.2 Numeración

La numeración `CU-12001` a `CU-12007` es **contigua y propia de este proyecto de código**. `CU-12007` nace después que el transversal `CU-12006` porque se emitió más tarde, con la sexta función: **no se renumera**, porque renumerar rompería las referencias ya emitidas aguas abajo por un motivo puramente cosmético. El orden de lectura, en cambio, es el del ciclo de vida —`CU-12001` a `CU-12005`, después `CU-12007` y por último `CU-12006`, que los recorre juntos—, y así lo declara el `README.md` de la sección. Las `CU-15`, `CU-16` y `CU-17` que `NB-00006` declara previstas son la numeración de nivel producto que la necesidad anticipó antes de repartirse por proyecto de código; la parte que le toca a este proyecto de código son estos **siete** contratos de uso, y la correspondencia queda declarada en §5.1 para que la trazabilidad no se pierda.

## 4. Matriz NB→CU→RN→US

### 4.1 `GeometriaFactory-Web`

Las `RN-XX` de la tercera columna **viven en `GeometriaFactory-Domain`** y se referencian por identificador con enlace relativo. Ninguna se redacta acá.

| NB | CU | RN | US a generar en 06 |
| --- | --- | --- | --- |
| [NB-00001](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00001-Control-De-Admision-Al-Laboratorio.md) | CU-10001, CU-10002, CU-10004 | [RN-02001](../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02001-Administrador-Unico-Y-Papeles-Fijos.md), [RN-02002](../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02002-Correo-Del-Alumno-Unico.md), [RN-02006](../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02006-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md), [RN-02007](../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02007-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md) | US-10001, US-10002, US-10003, US-10004, US-10005, US-10008, US-10009, US-10010 |
| [NB-00002](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00002-Identidad-Propia-Del-Alumno-Sin-Correo.md) | CU-10001, CU-10002, CU-10003, CU-10004 | [RN-02002](../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02002-Correo-Del-Alumno-Unico.md), [RN-02006](../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02006-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md) | US-10001, US-10002, US-10003, US-10004, US-10005, US-10006, US-10007 |
| [NB-00003](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00003-Trabajo-Con-Dueno-Estado-Y-Persistencia.md) | CU-10005, CU-10006 | [RN-02003](../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02003-Trabajo-Ajeno-Indistinguible-De-Inexistente.md), [RN-02004](../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02004-Eliminacion-Acotada-Al-Borrador.md), [RN-02005](../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02005-Finalizacion-Sin-Errores-De-Validacion.md), [RN-02008](../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02008-Texto-Original-Conservado-Integro.md) | US-10011, US-10012, US-10013, US-10014, US-10015, US-10016, US-10017 |
| [NB-00004](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00004-Interpretacion-Fiel-Del-Dato-Del-Alumno.md) | CU-10005, CU-10007 | [RN-02003](../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02003-Trabajo-Ajeno-Indistinguible-De-Inexistente.md), [RN-02005](../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02005-Finalizacion-Sin-Errores-De-Validacion.md), [RN-02008](../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02008-Texto-Original-Conservado-Integro.md), [RN-02009](../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02009-Observacion-De-Error-Con-Posicion-Y-Campo.md) | US-10011, US-10012, US-10013, US-10014, US-10018, US-10019, US-10020, US-10021 |
| [NB-00005](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00005-Visibilidad-Del-Error-De-Calculo.md) | CU-10005, CU-10007 | [RN-02005](../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02005-Finalizacion-Sin-Errores-De-Validacion.md), [RN-02009](../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02009-Observacion-De-Error-Con-Posicion-Y-Campo.md) | US-10013, US-10014, US-10019, US-10020 |
| [NB-00006](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00006-Visualizacion-Dentro-Del-Producto.md) | CU-10007 | [RN-02003](../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02003-Trabajo-Ajeno-Indistinguible-De-Inexistente.md), [RN-02008](../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02008-Texto-Original-Conservado-Integro.md), [RN-02009](../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02009-Observacion-De-Error-Con-Posicion-Y-Campo.md) | US-10018, US-10019, US-10020, US-10021 |
| [NB-00007](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00007-Revision-De-La-Comision-En-Un-Solo-Lugar.md) | CU-10007, CU-10008, CU-10009 | [RN-02003](../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02003-Trabajo-Ajeno-Indistinguible-De-Inexistente.md), [RN-02011](../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02011-El-Administrador-No-Ve-Los-Borradores.md) | US-10018, US-10019, US-10020, US-10021, US-10022, US-10023, US-10024, US-10025 |
| [NB-00008](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00008-Alcance-Del-Laboratorio-Desde-El-Aula.md) | CU-10010 | Ninguna, con el motivo declarado en §5 y en CU-10010 §9 | US-10026, US-10027 |
| [NB-00009](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00009-Desenlace-Explicito-De-La-Entrega.md) | CU-10006, CU-10007, CU-10008, CU-10009 | [RN-02004](../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02004-Eliminacion-Acotada-Al-Borrador.md), [RN-02010](../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02010-Desenlace-Exclusivo-Del-Administrador-Y-Terminalidad.md), [RN-02011](../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02011-El-Administrador-No-Ve-Los-Borradores.md) | US-10015, US-10016, US-10017, US-10018, US-10019, US-10020, US-10021, US-10022, US-10023, US-10024, US-10025 |

### 4.1 Cobertura inversa: de NB a CU

Ninguna necesidad queda sin caso de uso y ningún caso de uso queda huérfano. Este proyecto de código no sostiene cada necesidad en la misma medida, y declararlo es más útil que una fila uniforme.

| NB | Grado en que la pieza pública la sostiene | Qué queda en otro proyecto de código |
| --- | --- | --- |
| NB-00001 | Sostenida en lo que le corresponde: es el panel donde el administrador ve la lista y ejerce las **cinco** operaciones, donde la confirmación escrita de la baja se exige y donde se comunica la contraseña provisoria del reseteo | La unicidad del administrador y el arrastre de trabajos son invariantes de `GeometriaFactory-Domain` |
| NB-00002 | Sostenida y decisiva: es donde el alumno se registra, establece su contraseña y obtiene sesión, y donde se verifica que la credencial no llegue al navegador | La derivación de la credencial es de `GeometriaFactory-Infrastructure`; la admisibilidad de la cuenta, del dominio |
| NB-00003 | Sostenida en lo que le corresponde: la acción única de guardado, el listado con los cuatro estados y la acotación de lo que el alumno puede hacer en cada uno | La persistencia es de `GeometriaFactory-Infrastructure`; las transiciones, del dominio |
| NB-00004 | Parcial: envía el texto sin tocar un carácter y presenta las observaciones con índice de figura y campo señalado | La tolerancia de formato y la interpretación son de `GeometriaFactory-Infrastructure` y del dominio |
| NB-00005 | Parcial: muestra las advertencias con los dos valores y no las convierte en un bloqueo | El recálculo y la tolerancia de comparación son del dominio y de su implementación |
| NB-00006 | **Sostenida casi por entero**: la vista de trabajo, la sincronización entre árbol y escena, la enumeración de las piezas no dibujadas y el ciclo de vida de la instancia son de esta pieza | El dibujo en sí es de `GeometriaFactory-Visor`, invocado por su fachada |
| NB-00007 | Sostenida y decisiva: el listado de la comisión, su agrupación y su filtro, y la vista idéntica para los dos papeles | El alcance de lo que el administrador ve lo decide el dominio |
| NB-00008 | Parcial: la presentación del estado degradado y el cartel de reconexión, que es lo único de esta necesidad que la persona ve | La verificación de acceso desde la red de la facultad y el despliegue de las dos piezas desplegables son de 09-Devops |
| NB-00009 | Sostenida en lo que le corresponde: las dos decisiones con su comentario opcional, el retiro, y que el alumno vea el desenlace en su listado y el comentario al abrir el trabajo | La exclusividad de la facultad y la terminalidad son invariantes del dominio |

### 4.2 Correspondencia con los flujos de producto de 01

`Necesidades-Negocio.md` §5.3 enumera los flujos de producto que cada necesidad de negocio pide. Esta
tabla declara cuáles realiza `GeometriaFactory-Web` y con qué caso de uso.

Hasta la migración a SDD 8.x, la previsión de 01 llevaba su propia numeración y esta sección tuvo que
acuñar la familia `P·CU-XX` para poder citarla sin confundirla con los casos de uso locales, que se
numeraban por proyecto de código. Con el **ámbito de unicidad en el producto** de `Root-Rules.md`
§9.1 los identificadores ya no se pisan, la previsión numerada se retiró de §5.3 y **la familia
`P·CU` se retira con ella**: los flujos se nombran, y quien los realiza se cita por su identificador.

| Flujo de producto que pide 01 | NB | Caso de uso que lo realiza | Qué queda fuera de esta unidad de entrega |
| --- | --- | --- | --- |
| configurar la cuenta de administrador en el primer arranque | NB-00001 | CU-10004, FA-03 | La unicidad, que es invariante de dominio |
| habilitar, bloquear y rehabilitar una cuenta | NB-00001 | CU-10004, flujo principal | La transición admitida |
| dar de baja una cuenta con confirmación escrita | NB-00001 | CU-10004, FA-02 | El arrastre de trabajos |
| registrar una cuenta de alumno | NB-00002 | CU-10001, flujo principal | La unicidad del correo |
| elegir la contraseña propia en el primer ingreso, **con la provisoria como vigente** | NB-00002 | CU-10003, flujo principal; CU-10002 FA-02 para el desvío | La derivación de la clave y la producción de la provisoria |
| iniciar y cerrar sesión | NB-00002 | CU-10002 completo | La emisión de la credencial de sesión |
| cambiar la contraseña exigiendo la vigente | NB-00002 | CU-10003, FA-01 | La verificación de la contraseña vigente |
| cargar un trabajo | NB-00003 | CU-10005, flujo principal | La persistencia |
| reeditar un trabajo en estado `Borrador` | NB-00003 | CU-10005 FA-05, y CU-10006 FA-01 | La acotación al estado |
| eliminar un trabajo propio en estado `Borrador` | NB-00003 | CU-10006, FA-02 | La acotación al estado y a la pertenencia |
| listar los trabajos propios | NB-00003 | CU-10006, flujo principal | El alcance de la colección |
| interpretar el texto y reportar el error con figura y campo | NB-00004 | CU-10005, paso 8, y CU-10007 paso 10 | Toda la interpretación y la tolerancia de claves |
| resolver el estado del trabajo según el resultado de la interpretación | NB-00004 | CU-10005, FA-01 y FA-02 | La decisión del estado, que es del dominio |
| enviar un trabajo | NB-00004 | CU-10005, flujo principal completo | La interpretación |
| verificar los valores declarados contra los derivados | NB-00005 | CU-10005 FA-03, y CU-10007 paso 10 | El recálculo y la tolerancia de comparación |
| previsualizar el trabajo en tres dimensiones | NB-00006 | CU-10007, pasos 5 a 7; CU-10005 paso 4 para la previsualización previa al envío | El dibujo, que es del bundle |
| explorar la estructura como árbol colapsable | NB-00006 | CU-10007, paso 8 | La estructura del texto, que devuelve la fachada |
| sincronizar el árbol y la escena por índice de pieza | NB-00006 | CU-10007, paso 9 | El resaltado, que ejerce la fachada |
| listar los trabajos de la comisión sin los que están en estado `Borrador` | NB-00007 | CU-10008, flujo principal | El recorte, que decide el dominio |
| abrir un trabajo de un alumno para revisarlo | NB-00007 | CU-10007, FA-01 | La visibilidad, que decide el dominio |
| consultar el panel de resumen por alumno y por estado | NB-00007 | CU-10008, FA-04 | El recuento, que produce la pieza de datos |
| verificar el acceso al laboratorio desde la red de la facultad | NB-00008 | **Ninguno.** No es un acto de la persona dentro del producto | Verificación de campo y despliegue: 09-Devops |
| presentar el estado degradado cuando el servicio de datos no responde | NB-00008 | CU-10010, flujo principal | La respuesta de error neutra, que declara el contrato |
| aprobar un trabajo en estado `Pendiente`, con comentario opcional | NB-00009 | CU-10009, flujo principal | La transición y su exclusividad |
| rechazar un trabajo en estado `Pendiente`, con comentario opcional | NB-00009 | CU-10009, FA-01 | Ídem, con el otro valor de la decisión |
| consultar el desenlace y el comentario del trabajo propio | NB-00009 | CU-10006 FA-03 para el estado en el listado; CU-10007 paso 11 para el comentario | El transporte del comentario |
| eliminar un trabajo desde el panel del administrador | NB-00009 | CU-10009, FA-03 | La acotación por visibilidad |

Veintiséis de los veintisiete flujos se realizan en esta sección. El único que esta unidad de entrega no toca es **verificar el acceso al laboratorio desde la red de la facultad**, porque no es un acto que la persona ejecute dentro del producto: es verificación de campo y despliegue, y vive en 09-Devops.

**Dos actos de esta sección no tienen previsión en 01, y es correcto que no la tengan**: el **reseteo de contraseña** de CU-10004 FA-06 y el **cambio forzado** de CU-10003 FA-04. Los dos nacen de la capacidad **F-26**, que el `PRODUCT-INTAKE` incorporó en su versión **1.7**, posterior a la emisión de `Necesidades-Negocio.md` §5.3. **No se los fuerza dentro de ninguna previsión existente**: `01-Necesidades-Negocio` decidirá si los incorpora a su catálogo, y hasta entonces la correspondencia se lee al revés, desde la capacidad.

## 5. Por qué esta sección no redacta reglas de negocio

### 5.1 `GeometriaFactory-Web`

Las **dieciséis reglas** del producto viven en `GeometriaFactory-Domain`, que es donde se hacen cumplir. Acá se **referencian por identificador** y no se redactan. **RN-10012 y RN-10013 entraron con el `PRODUCT-INTAKE` 1.7, RN-10014 y RN-10015 con el 1.10 y RN-10016 con el 1.13; las cinco tienen archivo allá**: [`RN-02012`](../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02012-Reseteo-Conserva-La-Cuenta-Y-Sus-Trabajos.md) y [`RN-02013`](../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02013-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md), de modo que las filas de la matriz que las citan las enlazan como a las otras once. El motivo no es formal: **la pieza pública no puede ser la última defensa de ninguna regla, porque el navegador no es confiable.** Ocultar un botón, no armar una ruta o no ofrecer una acción son decisiones de presentación legítimas y necesarias, pero no hacen cumplir nada; por eso varios criterios de aceptación de esta sección verifican la acotación **forzando la solicitud sin pasar por la pantalla** —CU-10006 CA-04, CU-10009 CA-04 y CA-05—.

La única fila de la matriz sin regla referenciada es NB-00008, en CU-10010. Su motivo está declarado en el propio caso de uso: las dieciséis reglas restringen el dominio —cuentas, trabajos, estados, observaciones y credenciales— y CU-10010 no toca ninguno de esos objetos. Lo que sí lo restringe es la regla de arquitectura RA-03, cuyo enunciado vive en `PRODUCT-INTAKE` §14 y que se verifica en CU-10010 CA-02. Inventar una `RN-XX` para llenar la celda habría sido redactar una regla que no existe aguas arriba.

Dos advertencias de lectura sobre los archivos de `GeometriaFactory-Domain`, heredadas de la especificación de `GeometriaFactory-Contracts` §5: `RN-10004-Eliminacion-Acotada-Al-Borrador.md` **cubre hoy los dos caminos de eliminación**, el del alumno acotado al borrador y el del administrador sobre cualquier trabajo que ve, y `RN-10005-Finalizacion-Sin-Errores-De-Validacion.md` **corta hoy en el envío y no en el cierre**. Los dos slugs quedaron desactualizados respecto de su enunciado y se decidió aguas arriba no renombrarlos. Se cita el contenido vigente, no el que sugiere el nombre.

## 6. Restricciones transversales de la pieza pública

### 6.1 `GeometriaFactory-Web`

Valen para los diez casos de uso y no se repiten en cada uno más allá de su criterio de aceptación correspondiente. Son **trece** desde el `PRODUCT-INTAKE` 1.7.

| Id | Restricción | Origen | Dónde se verifica |
| --- | --- | --- | --- |
| RT-01 | Ninguna llamada a la pieza de datos se origina en el navegador: todas salen del servidor de la pieza pública | `PRODUCT-INTAKE` §14 (RA-01), §17.6 P.3 | CU-10001 CA-05, CU-10002 CA-07, CU-10005 CA-06, CU-10007 CA-10 |
| RT-02 | **La credencial de sesión vive en el estado del circuito, del lado del servidor, y no aparece nunca en el navegador.** El navegador conserva sólo una marca de sesión que no la transporta | `PRODUCT-INTAKE` §17.6 P.4 y P.5 | CU-10002 CA-02, y CU-10003 CA-05 para las contraseñas |
| RT-03 | Ningún mensaje mostrado a la persona incluye la dirección de un servicio interno, un nombre de archivo de datos ni una traza de la implementación | `PRODUCT-INTAKE` §14 (RA-03), §17.6 P.5 | CU-10010 CA-02, y las filas de estado degradado de los §6 de CU-10001, CU-10004 a CU-10009 |
| RT-04 | El bundle del visualizador se invoca **exclusivamente** por sus **seis** funciones —las cinco del ciclo de vida y la selección, más `establecerMovimiento`—. Ningún componente accede a su interior ni manipula el elemento de dibujo por su cuenta | `PRODUCT-INTAKE` §14 (RA-02), §17.6 P.3 (regla de aislamiento del visor) | CU-10005 §10, CU-10007 §7 y §10 de este documento |
| RT-05 | `destruir` se invoca al descartar el componente que aloja la instancia del visualizador. **No es opcional**: sin eso, recorrer trabajos acumula contextos gráficos en el navegador | `PRODUCT-INTAKE` §17.6 P.11 punto 5 | CU-10007 CA-05, CU-10005 CA-07 |
| RT-06 | La pieza pública **no guarda estado propio**. No hay copia local de los datos, ni caché, ni réplica: cuando la pieza de datos no está, no hay nada que mostrar y se declara el estado degradado | `PRODUCT-INTAKE` §17.6 P.4, §7 (CL-8) | CU-10004 §6, CU-10006 §6, CU-10008 §6, CU-10010 §7 |
| RT-07 | La indisponibilidad se presenta siempre como **estado degradado explícito**, nunca como excepción sin manejar y nunca como pantalla rota. El listado vacío se distingue del fallo **por el tipo recibido y no por el conteo** | `PRODUCT-INTAKE` §7 (CL-2), §17.6 P.10 | CU-10010 CA-01, CA-07; CU-10006 CA-06; CU-10008 CA-06 |
| RT-08 | El texto original del trabajo se envía **carácter por carácter** tal como la persona lo pegó, y no se reescribe en ningún punto del recorrido | `PRODUCT-INTAKE` §4.1 (RN-10008), §9 (X-4) | CU-10005 CA-02 |
| RT-09 | Ninguna ruta del panel es accesible sin sesión, y un alumno con sesión no accede a ninguna ruta de administrador. Esto **acota lo que se ofrece**; la verificación de pertenencia y de papel la hace la pieza de datos en cada solicitud | `PRODUCT-INTAKE` §17.6 P.5 | CU-10002 CA-05, CU-10004 CA-07 |
| RT-10 | Durante la interacción con la escena no hay tráfico de circuito hacia el servidor, y el texto del trabajo viaja del servidor al navegador **una sola vez por trabajo** | `PRODUCT-INTAKE` §17.6 P.10 | CU-10007 CA-10 |
| RT-11 | Toda combinación de navegador sin capacidad gráfica tridimensional se considera no soportada **para la escena**, y el resto del producto sigue disponible | `PRODUCT-INTAKE` §17.6 P.9 | CU-10005 FA-04, CU-10007 FA-05 |
| RT-12 | **Una cuenta con cambio de contraseña pendiente no llega a ninguna ruta que no sea el cambio de su propia contraseña, y llega ahí sin sesión de trabajo**: el canje reconoce la provisoria y no emite sesión. La pieza pública **acota lo que se ofrece**; quien lo hace cumplir es la pieza de datos, que verifica la marca en cada solicitud | `PRODUCT-INTAKE` **1.8** §4.1 (RN-10013 precisada), §17.1.P.2 · GeometriaFactory-Domain (INV-09) | CU-10002 CA-08, CU-10003 CA-06 y CA-07 |
| RT-13 | **El anfitrión gobierna los dos movimientos automáticos mandando dos valores de verdad por la fachada, y el bundle no consulta nada.** En particular, **la preferencia de movimiento reducido del navegador la lee la pieza pública**, no el visor, y la traduce a esos dos booleanos | `PRODUCT-INTAKE` **1.7** §4 (F-25, `Must Have`), §17.7 P.3 y P.10 | CU-10007 §7, y §7 de este documento |

## 7. Consumo del contrato de fachada del visualizador

### 7.1 `GeometriaFactory-Web`

El bundle expone **seis funciones** y siete códigos de condición, declarados en [`Definicion-Contrato-De-Fachada.md`](Definicion-Contrato-De-Fachada.md). La sexta, `establecerMovimiento`, la incorporó el `PRODUCT-INTAKE` **1.6** §17.7 P.3 para gobernar los dos movimientos automáticos de **F-25** sobre una instancia viva, sin reconstruirla. Esta tabla declara, una sola vez, qué función consume cada caso de uso; los casos de uso no la repiten más allá de su fila de trazabilidad.

| Caso de uso | `inicializar` | `cargarJson` | `seleccionarPieza` | `redimensionar` | `destruir` | `establecerMovimiento` |
| --- | --- | --- | --- | --- | --- | --- |
| CU-10005, previsualización previa al envío | Sí | Sí | — | — | Sí | — |
| CU-10007, vista de trabajo | Sí | Sí | Sí | Sí | Sí | Sí |
| Los otros ocho | — | — | — | — | — | — |

Tres consecuencias que las categorías aguas abajo no deben perder:

1. **El componente anfitrión de la pieza pública es quien opera el ciclo de vida.** La fachada no observa tamaños ni decide cuándo ajustar: por eso `redimensionar` lo invoca CU-10007 y no ocurre solo.
2. **El resultado de dibujo no lleva observaciones.** Las piezas que la fachada no dibuja se enumeran por su índice, y eso **no** las convierte en errores del trabajo: quien decide si el trabajo verifica es la pieza de datos. CU-10005 §10 y CU-10007 §10 lo declaran para que la vista no las mezcle.
3. **El gobierno del movimiento automático es del anfitrión y viaja en un solo sentido** (RT-13). La pieza pública manda **dos valores de verdad** por `establecerMovimiento` —uno por la órbita de la cámara y otro por el giro de las piezas— y el bundle **no consulta nada**: ni la preferencia de movimiento reducido del navegador, ni configuración propia, ni almacenamiento. **Esa preferencia la lee la pieza pública** y la traduce a los dos booleanos, que es la única forma compatible con RA-02, un visualizador sin configuración y sin identidad. Es también lo que hace que la instancia no se reconstruya para prender o apagar un movimiento, y por lo tanto que no se pierda la selección de pieza.

## 8. Glosario

### 8.1 `GeometriaFactory-Web`

El vocabulario de esta categoría vive en [`Glosario-Funcional.md`](Glosario-Funcional.md), que declara los términos que la pieza pública acuña, **tres términos con más de un referente** —«vista», «pieza» y `Pendiente`— y los términos referenciados del glosario raíz de `Vision-Producto.md` §9, que no se redefinen.

Dos advertencias de lectura. **`Pendiente` nombra dos estados distintos** —el de una cuenta y el de un trabajo—, los dos aparecen en las mismas secciones de esta especificación, y por eso se escribe siempre calificado: «cuenta `Pendiente`» o «trabajo en estado `Pendiente`». No se califican las enumeraciones del conjunto cerrado ni los identificadores literales. Y **«vista» tiene tres referentes** dentro de este proyecto de código —la página, el componente y la perspectiva de datos—; la forma que corresponde a cada uno está en `Glosario-Funcional.md` §3.1.

## 9. Artefactos de esta categoría que se omiten

### 9.1 `GeometriaFactory-Web`

La tabla maestra de `Rules-Especificacion-Funcional.md` §2.1 tiene ocho filas: se emiten cuatro artefactos y **no se emiten cuatro**, agrupados en tres puntos. El motivo de cada omisión está desarrollado en el [`README.md`](README.md) §3 de esta sección; acá se enumeran para que el índice maestro no deje huecos:

- `Reglas-De-Negocio/RN-XX-<Nombre>.md`: las **dieciséis** reglas viven en `GeometriaFactory-Domain`. El fundamento está en §5 de este documento.
- `Modelo-Datos/Modelo-Conceptual.md` y `Modelo-Datos/reglas-conceptuales-de-modelo/RC-XX-<Nombre>.md`, que son **dos** de las ocho filas: la regla los marca obligatorios para `web-monolith`, y se omiten igual **como decisión técnica declarada** —`tiene_persistencia` es false y es deliberado—. Corresponde una ADR en 05-Arquitectura-Tecnica que la registre.
- `Definicion-<Concepto-Central>.md`: el concepto central del producto ya está documentado aguas arriba, en `Definicion-Modelo-De-Dominio.md` de `GeometriaFactory-Domain` y en `Definicion-Contrato-De-Fachada.md` de `GeometriaFactory-Visor`. Un documento de concepto acá los duplicaría.

## 10. Propósito y alcance de esta categoría

### 10.1 `GeometriaFactory-Visor`

Este documento es el índice maestro de la especificación funcional de **GeometriaFactory-Visor**, el proyecto de código de tipo `library` que produce el archivo de guion del visualizador tridimensional del producto **Fábrica de Geometría**.

La categoría es de **nivel proyecto de código** y su superficie es angosta y declarada: seis funciones planas —las cinco que declara PRODUCT-INTAKE §17.7 P.3 y la sexta que el Product Owner agregó el 2026-08-09, acuñada en `Definicion-Contrato-De-Fachada.md` §4.6—. Por eso cada caso de uso describe **un contrato de uso** y no un flujo de pantallas, según la variante de `Rules-Especificacion-Funcional.md` §1.2 para el tipo `library`.

## 11. Qué es y qué no es este proyecto de código

### 11.1 `GeometriaFactory-Visor`

`GeometriaFactory-Visor` es un **visualizador puro**: la regla de arquitectura `RA-02` del producto lo define como un archivo de guion sin configuración, sin red y sin conocimiento del sistema (PRODUCT-INTAKE §14). De esa definición se siguen los límites que toda esta categoría respeta:

| Este proyecto de código | Sí | No |
| --- | --- | --- |
| Actores | El componente que lo embebe y el texto que recibe | Ninguna persona, ningún papel, ningún servicio |
| Datos | Los que recibe por parámetro en cada invocación | No pide datos por red ni lee configuración propia |
| Decisiones | De qué dimensión saca una malla | Ninguna decisión de validez, de autorización ni de negocio |
| Estado | El de sus instancias vivas, mientras la página vive | Ningún estado entre páginas, ninguna escritura en el almacenamiento del navegador |
| Salida | Mallas en una escena, y el resultado de dibujo | Ninguna observación: ni advertencias ni errores de validación, que son del backend |

Un caso de uso de esta categoría en el que el alumno, el docente, el backend, un servicio o una credencial **intervinieran como actor o condicionaran un flujo** estaría mal escrito por definición. Nombrarlos para declarar qué queda fuera del contrato es, en cambio, **obligatorio**: es lo que impide que un lector aguas abajo le atribuya a la fachada una validación, una decisión de autorización o una obtención de datos que no hace. Por eso `CU-12006` se titula «sin backend» y por eso `CU-12002` nombra al backend en sus notas.

## 12. Documento de concepto central

### 12.1 `GeometriaFactory-Visor`

[`Definicion-Contrato-De-Fachada.md`](Definicion-Contrato-De-Fachada.md) es el documento de concepto central de esta categoría, admitido por `Rules-Especificacion-Funcional.md` §2.1. Define el vocabulario, el ciclo de vida de una instancia, las siete garantías transversales, las siete prohibiciones, la semántica de las **seis** funciones, los cinco elementos del concepto, los siete códigos de condición y la política de compatibilidad de la superficie pública. Es además el lugar donde se **acuñó la sexta función**, `establecerMovimiento` (§4.6), que el Product Owner **ya consolidó en el intake**: `PRODUCT-INTAKE` §17.7 P.3 la declara desde su versión **1.6**, con la nota «**Sexta función** [DECISIÓN 2026-08-09]» y remitiendo a §4.6 de ese documento por su especificación.

Existe porque el contrato de la fachada es el **punto de extensión declarado del producto** (PRODUCT-INTAKE §18), y porque los siete casos de uso comparten su vocabulario y sus códigos: declararlos una vez evita siete definiciones que se desincronizan.

## 13. Matriz de trazabilidad NB → CU → RN → US

### 13.1 `GeometriaFactory-Visor`

### 5.1 Matriz

| NB | CU previsto a nivel producto | CU de este proyecto de código | RN | US a generar en 06 |
| --- | --- | --- | --- | --- |
| NB-00006 | CU-15 previsualizar el trabajo en tres dimensiones | CU-12001, CU-12002, CU-12004, CU-12005 | — | US de creación de instancia, de dibujo del trabajo, de ajuste al espacio disponible y de liberación de recursos |
| NB-00006 | CU-16 explorar la estructura del trabajo como árbol colapsable | CU-12002 (la fachada devuelve la estructura del texto; la presentación del árbol es del componente anfitrión) | — | US de entrega de la estructura del texto para el árbol |
| NB-00006 | CU-17 sincronizar el árbol y la escena por índice de pieza | CU-12003, y la disposición determinista de CU-12002 | — | US de resaltado exclusivo por índice y de disposición derivada del índice |
| NB-00006 | — (criterios segundo, tercero y cuarto de su §5, verificados juntos) | CU-12006 | — | US de la página integradora sin backend y de la verificación de cero red y cero persistencia |
| NB-00006 | — (capacidad **F-25** del intake §4, **`Must Have`** desde el intake 1.7, incorporada el 2026-08-09; su CU de nivel producto es **CU-28**, previsto por `NB-00006` §7 después de aquel reparto) | CU-12007, y las dos opciones de gobierno de CU-12001 | — | US de gobierno en vivo de los dos movimientos automáticos, sin reconstrucción de la instancia y sin pérdida de la selección |
| NB-00004 | CU-12 interpretar el texto del trabajo y reportar los errores con figura y campo | CU-12002, **sólo en su parte de piezas efectivamente dibujadas**: la fachada lee las mismas variantes de clave para que ninguna pieza que el producto interpreta quede sin dibujar. La interpretación, los errores con índice y campo y las observaciones **no** son de este proyecto de código | — | US de lectura de dimensiones con las variantes de clave del emisor |

### 5.2 Por qué la columna RN queda vacía

La columna está vacía en las **seis** filas de §5.1 y **es correcto**: `GeometriaFactory-Visor` es un visualizador puro y **no tiene reglas de dominio**. Las que rigen el trabajo del alumno —qué se puede finalizar, qué produce advertencia, quién ve qué— las decide el backend, y este proyecto de código no participa de ninguna de esas decisiones (PRODUCT-INTAKE §14 RA-02, §17.7 P.5 y P.11 punto 4).

Lo que sí tiene son **condiciones de contrato**, que no son reglas de negocio: están declaradas una sola vez en `Definicion-Contrato-De-Fachada.md` §6 y referenciadas por cada caso de uso. Escribirlas como `RN-XX` habría sido el anti-patrón inverso al de «RN escrita como CU»: una condición técnica del contrato disfrazada de invariante del dominio.

### 5.3 Cobertura de las NB del producto

| NB | ¿La toca este proyecto de código? | Fundamento |
| --- | --- | --- |
| NB-00001 Control de admisión y de bajas | No | Admisión y bajas de cuentas. La fachada no sabe quién es la persona ni qué papel cumple |
| NB-00002 Identidad propia del alumno sin correo | No | Credenciales e identidad. Prohibición explícita de PRODUCT-INTAKE §17.7 P.5 |
| NB-00003 Trabajo con dueño, estado y persistencia | No | Persistencia y estado del trabajo. Prohibición explícita de PRODUCT-INTAKE §17.7 P.4 |
| NB-00004 Interpretación fiel del dato del alumno | **Parcialmente**, sólo en la parte de que las piezas se dibujen | La interpretación, la localización del error y el límite entre guardar y entregar son del backend |
| NB-00005 Visibilidad del error de cálculo | No | Recalcular valores y emitir advertencias es del backend |
| NB-00006 Visualización dentro del producto | **Sí, es su necesidad** | Los siete casos de uso la implementan desde el archivo de guion |
| NB-00007 Revisión de la comisión en un solo lugar | No | Listar, filtrar y agrupar trabajos es del backend y del componente anfitrión. La fachada ya sirve al administrador por ser la misma para los dos papeles, sin saberlo |
| NB-00008 Alcance del laboratorio desde el aula | No | Disponibilidad y despliegue. Este proyecto de código contribuye de forma negativa —no hacer red—, lo que se verifica en CU-12006, pero no implementa ninguna capacidad de la necesidad |
| NB-00009 Desenlace explícito de la entrega | No | Aprobar, rechazar y comentar un trabajo es del backend y del componente anfitrión. La fachada dibuja el mismo trabajo para el alumno y para el administrador **sin saber cuál de los dos lo mira** ni en qué estado está, que es exactamente lo que RA-02 exige |

Cobertura bidireccional dentro del alcance de este proyecto de código: **ningún caso de uso queda huérfano** —los siete trazan a NB-00006, CU-12002 traza además a NB-00004 y CU-12007 traza además a la capacidad F-25 del intake §4— y **la única NB que este proyecto de código implementa, NB-00006, tiene casos de uso**. Las **siete** NB restantes se implementan en otros proyectos de código del producto, no quedan sin cubrir por esta declaración.

## 14. Propiedades transversales verificables

### 14.1 `GeometriaFactory-Visor`

**Seis** propiedades atraviesan los siete casos de uso. Esta tabla es el **lugar único** donde se declaran su membresía, su umbral y **las condiciones en que se miden**, para que 08-Calidad-Y-Pruebas las tome como están; §3.1 punto 2, `Definicion-Contrato-De-Fachada.md` §4.6 y `CU-12006` §1 enumeran las mismas seis y remiten acá.

| Propiedad | Umbral verificable | Condiciones de medición | Dónde se verifica |
| --- | --- | --- | --- |
| Cero red | Exactamente **0 peticiones** originadas por el archivo de guion, contadas en la pestaña de red | **Con los dos movimientos automáticos prendidos** —órbita de la cámara y giro de las figuras, `Definicion-Contrato-De-Fachada.md` §5.5—, sostenidos el tiempo suficiente para que el bucle de dibujo corra, y también durante los gestos de rotar y acercar. Ver la nota de abajo | CA de red de CU-12001 a CU-12007; CU-12006 CA-02; CU-12007 CA-05 |
| Cero persistencia | **0 claves** escritas en el almacenamiento del navegador, y ningún estado conservado entre páginas | Cualquier estado de los movimientos. La preferencia de movimiento **no se guarda** en la fachada: se comprueba que prender y apagar con `establecerMovimiento` no escribe ninguna clave, y que recargar la página no la repone | CU-12006 CA-03; CU-12007 CA-05 |
| Se ejercita sin backend | Recorrido completo de las **seis** funciones con un texto pegado a mano, con **0 servicios del backend disponibles** | Sin condición adicional | CU-12006 CA-01 |
| Disposición determinista | Dos procesados del mismo texto producen la **misma disposición**, comparable pieza por pieza | **Se compara posición, no orientación** (garantía G-6). La propiedad vale con cualquier estado de los movimientos, y prenderlos o apagarlos con la instancia viva no la altera | CU-12002 CA-04; CU-12006 CA-04; CU-12007 CA-01 |
| Liberación de recursos | **10 recorridos** de ida y vuelta entre trabajos sin degradación | **Con los dos movimientos prendidos** durante los recorridos: un bucle de dibujo que sobreviviera a `destruir` es exactamente la forma de degradación que esta propiedad tiene que descartar, y con los movimientos apagados no se ejercitaría | CU-12005 CA-04; CU-12006 CA-05 |
| Ausencia de fallo silencioso | **100 %** de las piezas no dibujadas enumeradas en el resultado de dibujo con su índice y su código de condición, y **0 piezas** que dejen de aparecer sin quedar registradas. Es la garantía G-5 del contrato, y es la propiedad que cierra el problema original de NB-00006: hoy, en la visualización previa, la figura simplemente no aparece y nadie se entera | Sin condición adicional: la enumeración es del resultado de dibujo y el movimiento no la toca | CU-12002 CA-05 y FA-02; CU-12006 CA-01 y FA-03 |

**Por qué la propiedad de cero red declara sus condiciones.** El umbral no cambia —sigue siendo **exactamente 0**— pero sin condiciones declaradas la prueba mediría el caso fácil. Los entornos de prueba automatizados suelen declarar preferencia de movimiento reducido; el componente anfitrión que la respeta invoca `inicializar` con los dos movimientos apagados, y una prueba escrita ahí quedaría en verde **sin haber ejercitado nunca un bucle de dibujo corriendo sesenta veces por segundo**, que es el caso donde una petición de red se colaría. Por eso la medición vale y se realiza con los dos movimientos prendidos, que es su peor caso. Que la fachada no consulte esa preferencia por su cuenta (G-3) es lo que hace que la prueba pueda prenderlos aunque el entorno la declare.

**Verificación de las demás.** Se revisaron las otras cinco buscando la misma indeterminación. **Liberación de recursos** la tenía y queda precisada arriba, por el mismo motivo: el peor caso es un bucle en curso al momento de `destruir`. **Disposición determinista** exigía la precisión inversa —qué se compara— y también queda declarada. **Cero persistencia** suma la comprobación de que la preferencia de movimiento no se guarda. Las dos restantes —se ejercita sin backend y ausencia de fallo silencioso— no dependen del estado de los movimientos y se declaran **sin condición adicional**, para que no se les invente una aguas abajo.

Material de dibujo declarado: el escenario **E-1** del intake —tres piezas, `Cilindro`, `Cubo` y `Ortoedro`, con el ortoedro dibujado— y el escenario **E-7** —seis piezas que cubren los seis tipos dibujables—. E-1 tiene su texto editado a mano y no ejercita las tolerancias del formato; las trampas del formato las ejercita E-2, que es material del backend.

## 15. Artefactos omitidos y su motivo

### 15.1 `GeometriaFactory-Visor`

| Artefacto | Estado | Motivo |
| --- | --- | --- |
| `Reglas-De-Negocio/RN-XX-<Nombre>.md` | Omitido | Un visualizador puro no tiene reglas de dominio: las decide el backend. `Rules-Especificacion-Funcional.md` §2.2 no las exige para `library`. Ver §5.2 |
| `Modelo-Datos/Modelo-Conceptual.md` y sus `RC-XX` | Omitido | `Rules-Especificacion-Funcional.md` §2.1 y §2.2 los omiten para `library` sin estado, y el flag `tiene_persistencia` de este proyecto de código es **false** |

El `README.md` de la sección repite las dos omisiones con su motivo, según exige el encargo de la categoría.

## 16. Vocabulario

### 16.1 `GeometriaFactory-Visor`

El vocabulario que esta categoría acuña vive en [`Glosario-Funcional.md`](Glosario-Funcional.md), obligatorio para los ocho tipos D8. Los términos ya declarados en el glosario raíz del producto —`../00-Contexto/Vision-Producto.md` §9— se referencian y no se redefinen.

Tres decisiones de vocabulario rigen todos los artefactos de esta categoría:

1. **«Trabajo»** es lo que el alumno entrega en el laboratorio. No es una «unidad de entrega» en el sentido normativo.
2. **«Pieza»** en su forma desnuda designa cada figura del conjunto raíz del trabajo. El segundo referente —cada artefacto desplegable del producto— se escribe siempre calificado y no aparece en estos artefactos.
3. **«Observación»** es el superordinado de «advertencia» y «error de validación». Este proyecto de código **no emite ninguna de las tres**, y las nombra sólo para declararlo.

## 17. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 2.0 | 2026-08-16 | **Consolidación de la fusión.** Pasa a ser el documento de la **unidad de entrega**, absorbiendo el de `GeometriaFactory-Visor`, con su texto transpuesto sin reescritura. Entra §0. Sube **major**. |
