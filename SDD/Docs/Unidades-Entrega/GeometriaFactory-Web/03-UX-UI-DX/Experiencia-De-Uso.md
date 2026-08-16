# Experiencia de uso — GeometriaFactory-Web

**Proyecto de código:** GeometriaFactory-Web
**Documento:** Experiencia-De-Uso.md
**Versión:** 1.4
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** UX/UI Designer + Frontend Lead (AG-03)
**Variante:** UX/UI
**Trazabilidad upstream:** `../02-Especificacion-Funcional/Especificacion-Funcional.md` §2, §3, §6 (las **trece** restricciones transversales) y §7 (consumo de la fachada); los diez casos de uso `CU-10001` a `CU-10010` de `../02-Especificacion-Funcional/Casos-De-Uso/`; `../02-Especificacion-Funcional/Glosario-Funcional.md` §2, §3 y §4; `../../../00-Contexto/Vision-Producto.md` §2, §3 y §9 (glosario raíz); `../../../00-Contexto/Alcance-Producto.md` §4.1; `../../../00-Contexto/Compatibilidad-Plataformas.md` §2.2 y §4; las nueve `NB-XX` de `../../../01-Necesidades-Negocio/Necesidades-De-Negocio/`; `../../GeometriaFactory-Visor/02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md` §3.2, §4 y §6; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §2 (audiencia), §4 (capacidades), §4.2 (modelo de estados y tabla de quién puede qué), §5 (historias de usuario), §6 (flujos 1, 2, 2.1, 3 y 4), §7 (casos límite), §14 (RA-01 a RA-03), §17.6 P.1 a P.12; catálogo de diseño `Design-Rules-Web-Generico.md`, `Design-Rules-Blazor-Mudblazor.md`, `Design-Rules-Primer-Arranque.md` y `Design-Rules-Identidad-De-Version.md`
**Trazabilidad downstream:** la **Fase B2** de validación visual de maqueta, que materializa estas superficies y estos flujos en una maqueta navegable; `05-Arquitectura-Tecnica`; `06-Backlog-Tecnico`; `08-Calidad-Y-Pruebas`; `11-Documentacion`

---

## Tabla de contenido

- [1. Audiencia y contexto de uso](#1-audiencia-y-contexto-de-uso)
  - [1.1 Las dos personas](#11-las-dos-personas)
  - [1.2 Contexto físico, emocional y de frecuencia](#12-contexto-físico-emocional-y-de-frecuencia)
  - [1.3 Lo que la audiencia no incluye](#13-lo-que-la-audiencia-no-incluye)
- [2. Principios de diseño](#2-principios-de-diseño)
  - [2.1 Heurísticas de Nielsen aplicadas](#21-heurísticas-de-nielsen-aplicadas)
  - [2.2 Leyes UX aplicadas](#22-leyes-ux-aplicadas)
  - [2.3 Catálogo de diseño aplicado y qué aporta cada documento](#23-catálogo-de-diseño-aplicado-y-qué-aporta-cada-documento)
  - [2.4 Las tres reglas de arquitectura como restricción de diseño](#24-las-tres-reglas-de-arquitectura-como-restricción-de-diseño)
- [3. Flujos clave](#3-flujos-clave)
  - [3.1 Mapa de superficies y su recorte](#31-mapa-de-superficies-y-su-recorte)
  - [3.2 Los dos shells](#32-los-dos-shells)
  - [3.3 Flujo A — Primer arranque del laboratorio](#33-flujo-a--primer-arranque-del-laboratorio)
  - [3.4 Flujo B — Alta del alumno de punta a punta](#34-flujo-b--alta-del-alumno-de-punta-a-punta)
  - [3.5 Flujo C — Cargar y enviar un trabajo](#35-flujo-c--cargar-y-enviar-un-trabajo)
  - [3.6 Flujo D — Explorar un trabajo](#36-flujo-d--explorar-un-trabajo)
  - [3.7 Flujo E — Revisión de la comisión y desenlace](#37-flujo-e--revisión-de-la-comisión-y-desenlace)
  - [3.8 Flujo F — Algo se corta](#38-flujo-f--algo-se-corta)
- [4. Estados y feedback](#4-estados-y-feedback)
  - [4.1 Tabla canónica de estados](#41-tabla-canónica-de-estados)
  - [4.2 Mapa de estados por superficie](#42-mapa-de-estados-por-superficie)
  - [4.3 Cómo se muestran los cuatro estados del trabajo](#43-cómo-se-muestran-los-cuatro-estados-del-trabajo)
- [5. Accesibilidad](#5-accesibilidad)
  - [5.1 Compromiso y criterios prioritarios](#51-compromiso-y-criterios-prioritarios)
  - [5.2 La escena tridimensional y su equivalente accesible](#52-la-escena-tridimensional-y-su-equivalente-accesible)
  - [5.3 Cómo se verifica](#53-cómo-se-verifica)
- [6. Internacionalización](#6-internacionalización)
- [7. Performance percibida](#7-performance-percibida)
- [8. Errores y recuperación](#8-errores-y-recuperación)
  - [8.1 Taxonomía de lo que la persona ve](#81-taxonomía-de-lo-que-la-persona-ve)
  - [8.2 Tono de los mensajes](#82-tono-de-los-mensajes)
  - [8.3 Lo que ningún mensaje contiene](#83-lo-que-ningún-mensaje-contiene)
- [9. Trazabilidad](#9-trazabilidad)
- [10. Notas y supuestos](#10-notas-y-supuestos)
- [11. Control de cambios](#11-control-de-cambios)

---

## 1. Audiencia y contexto de uso

### 1.1 Las dos personas

La audiencia de este producto es cerrada y conocida: una comisión de una materia de programación y su docente. No hay público general, no hay usuarios anónimos y no hay integradores. Las dos personas se derivan de `PRODUCT-INTAKE` §2 y de `Vision-Producto.md` §2.

| | Persona primaria — el alumno de la comisión | Persona secundaria — el docente como administrador |
| --- | --- | --- |
| Quién es | Estudiante de una materia de programación que acaba de escribir, en su Actividad 1, un programa que describe figuras como texto | El docente de la cátedra. Es a la vez Product Owner, único desarrollador humano y administrador del laboratorio, y **es una sola persona**: el producto admite exactamente un administrador |
| Qué sabe | Sabe programar y sabe leer un texto estructurado. **No** necesariamente sabe que su fórmula del área del cubo está mal, y ése es el punto | Conoce el producto entero. No necesita orientación de novato, sí necesita recorrer muchos trabajos rápido |
| Qué viene a hacer | Cargar el texto que produjo su programa, mirarlo dibujado y entregarlo | Habilitar cuentas, recorrer la entrega de la comisión y resolver cada trabajo |
| Qué lo frustra | Que su figura no aparezca y no le digan por qué. Es el fallo silencioso que el producto viene a eliminar | Tener que pedirle el trabajo a cada alumno, o abrir cada uno en un lugar distinto |
| Cuántos son | Una comisión: decenas, no miles | Uno |

### 1.2 Contexto físico, emocional y de frecuencia

- **Dónde.** Aula o laboratorio de la facultad, y casa. Pantallas de portátil de 13 a 15 pulgadas en el caso más frecuente; monitor de escritorio en el caso del docente. La red de la facultad es la restricción que ordena todo el producto y el producto puede estar lento sin estar caído.
- **Con qué.** Navegador con capacidad gráfica tridimensional y transporte de sesión interactiva. Es requisito **por capacidad y no por versión** (`Compatibilidad-Plataformas.md` §2.2). Un navegador sin esa capacidad gráfica no ve la escena y **sigue usando todo lo demás**: es una restricción de diseño, no una pantalla de rechazo.
- **Cuándo y cuánto.** El alumno entra en ráfagas: carga un trabajo, lo mira, corrige su programa afuera, vuelve y reenvía. Sesiones de cinco a veinte minutos, varias por semana de entrega. El docente entra en tandas largas: una sentada de treinta a noventa minutos para recorrer la comisión entera.
- **Estado emocional.** El alumno llega con una entrega pendiente y con la sospecha de que algo de su programa está mal. El primer contacto con un error de interpretación es un momento de riesgo: si el mensaje es genérico, el alumno abandona; si nombra la figura y el campo, corrige. El docente llega con prisa y con muchas unidades por revisar.
- **Momento crítico único.** El **primer arranque del laboratorio** ocurre una sola vez en la vida de la instancia y, si esa pantalla falla, el producto queda inutilizable: no hay administrador hasta que alguien lo configura, y esa superficie es la única puerta.

### 1.3 Lo que la audiencia no incluye

Se declara para que ninguna superficie invente un destinatario que el producto no tiene:

- No hay visitante anónimo con contenido que mirar. Las **únicas dos rutas públicas** son registro e ingreso.
- No hay un segundo administrador, ni papeles configurables, ni permisos finos (exclusión X-3).
- No hay integrador externo ni portal de developers. El flag `tiene_portal_developers` es false.
- No hay canal de correo: ni notificaciones, ni confirmación de dirección, ni recuperación **autónoma** de contraseña (exclusión **X-1**; **X-2** fue retirada el 2026-08-09 al entrar **F-26**, de modo que la recuperación existe, pero la ejerce el administrador reseteando desde su panel). Ninguna superficie ofrece «olvidé mi contraseña».

## 2. Principios de diseño

### 2.1 Heurísticas de Nielsen aplicadas

| Heurística | Aplicación en el producto | Verificación |
| --- | --- | --- |
| Visibilidad del estado del sistema | El estado del trabajo se lee en la fila del listado y en la cabecera de la vista de trabajo, siempre con texto explícito. El sello de versión declara qué instancia se está usando en las dos ubicaciones obligatorias. Toda acción que cruza al servidor muestra indicador de espera | Inspección heurística sobre la maqueta de la Fase B2, superficie por superficie |
| Correspondencia entre el sistema y el mundo real | El vocabulario es el de la cátedra: trabajo, pieza, enviar, aprobar, rechazar, advertencia. **No** aparece el nombre del formato del texto, ni «commit», ni «deploy», ni «request» | Revisión de microcopy contra `Glosario-Funcional.md` §2 y §4 |
| Control y libertad del usuario | El alumno puede abandonar el envío sin consecuencias y volver sobre su borrador cuantas veces quiera. El administrador confirma antes de una baja o de un retiro. **Excepción declarada**: el desenlace y el aprovisionamiento no tienen deshacer, y la superficie lo dice antes, no después | CA de CU-10004, CU-10005, CU-10009 |
| Prevención de errores | Las acciones que el estado no admite **no se dibujan**, ni siquiera deshabilitadas con un motivo escondido. La baja y el retiro exigen confirmación; la baja, además, confirmación escrita. Los requisitos de un campo se enuncian antes del intento | CU-10006 CA-02 y CA-03, CU-10004 CA-04 y CA-05 |
| Reconocer antes que recordar | La vista de trabajo pone lado a lado el texto que el alumno escribió y el dibujo que produjo: no hay que recordar qué figura era el índice 2, se selecciona y se resalta | CU-10007 CA-03 |
| Flexibilidad y eficiencia | El listado de la comisión se agrupa y se filtra por alumno, que es lo que convierte una tanda de revisión en una sentada | CU-10008 CA-02 y CA-03 |
| Diseño estético y minimalista | Una acción primaria por superficie. El listado es una proyección deliberadamente pobre; el detalle vive en la vista de trabajo | Recuento de acciones primarias por wireframe |
| Ayudar a reconocer, diagnosticar y recuperarse | Todo error de interpretación nombra el índice de figura y el campo. Toda indisponibilidad ofrece reintentar y **conserva lo escrito** | CU-10005 CA-04 y CA-08, CU-10010 CA-03 |
| Ayuda y documentación | El producto no lleva manual: la ayuda es contextual y mínima —el requisito bajo el campo, la leyenda del estado vacío, el texto de la confirmación escrita— | Presencia declarada en cada wireframe |

La heurística de **visibilidad del estado del sistema** tiene acá un uso doble que conviene separar: el estado del **trabajo**, que es dominio, y el estado del **sistema**, que es disponibilidad y versión. Las dos se resuelven con canales distintos y ninguna se comunica sólo por color.

### 2.2 Leyes UX aplicadas

| Ley | Aplicación |
| --- | --- |
| Hick | La navegación lateral tiene **tres** destinos por papel, no una lista que obligue a elegir. El desenlace ofrece exactamente dos decisiones |
| Fitts | La acción primaria de cada tarjeta de acceso ocupa el ancho completo. Los objetivos de toque nunca bajan de 24×24 px |
| Miller | Ninguna agrupación de primer nivel supera cinco elementos. La vista de trabajo tiene cuatro partes y no cinco, y ese recorte viene decidido aguas arriba |
| Jakob | El listado con filas y acciones por fila, el formulario con etiqueta arriba y control abajo, y el aviso en banda son las formas que la persona ya conoce. No se inventa ninguna interacción propia |
| Ley de la región común | Cada bloque de la vista de trabajo vive en su propio contenedor con borde tenue: lo que separa el comentario de las observaciones es un límite visible, no un espacio |

### 2.3 Catálogo de diseño aplicado y qué aporta cada documento

Los tokens, patrones y estados del catálogo son normativos. **Este proyecto de código no define ningún token visual propio** y referencia los patrones por su nombre del catálogo.

| Documento del catálogo | Aplica | Qué aporta acá |
| --- | --- | --- |
| `Design-Rules-Web-Generico.md` | Sí, siempre | Tokens de color, tipografía y espaciado; shell con barra lateral; patrones de grilla de listado, formulario, insignia de estado, botones, búsqueda y filtros; tabla canónica de estados; iconografía vectorial con `currentColor`; piso WCAG 2.2 AA; puntos de quiebre |
| `Design-Rules-Blazor-Mudblazor.md` | Sí, por el stack declarado en `PRODUCT-INTAKE` §17.6 P.1 | Materialización de cada patrón en el sistema de componentes declarado, la regla de que **todo color sale del tema y ninguno se escribe suelto**, el feedback obligatorio en cada acción que cruza el circuito, la prevención de doble envío, la prohibición de almacenamiento de navegador improvisado, y la nota de que los formularios de identidad se envían por petición y no por interactividad de componente |
| `Design-Rules-Primer-Arranque.md` | Sí. El producto se despliega por instancia y arranca sin la configuración mínima que lo hace utilizable: no hay administrador hasta que alguien lo configura | El predicado único de aprovisionamiento y su contrato, el corte en tres capas, el shell partido sin chrome y sin cancelar, el requisito declarado antes del intento, la banda de resultado, el destino al completar y la orientación posterior |
| `Design-Rules-Identidad-De-Version.md` | Sí. El producto produce dos artefactos desplegables identificables y esta pieza es la que tiene pantallas | El contrato de identidad de versión, las dos ubicaciones obligatorias del sello, el distintivo de artefacto preliminar, el marcador de origen indeterminado y el detalle de diagnóstico con copiado en un solo gesto |
| `Design-Rules-Config-Esquema.md` | **No aplica** | El producto **no tiene superficies de configuración que la persona fije**. El único parámetro configurable del proyecto de código es la dirección de la pieza de datos, que es configuración de entorno —se inyecta al publicar— y no configuración de aplicación. Por la frontera que fija `Rules-UX-UI-DX.md` §1.4, un parámetro que la superficie no gobierna **no se dibuja, ni siquiera deshabilitado** |
| `Design-Rules-Acceso-Monousuario.md` | **No aplica** | El producto declara **dos papeles** —alumno y administrador— y tiene gestión de cuentas con registro, habilitación, bloqueo y baja. La condición de carga de la extensión es una sola identidad de operación sin gestión de usuarios, y no se cumple. Rige el patrón de acceso general del documento base. **Lo que sí se hereda por coincidencia de forma, y se declara para que no parezca omisión**: el shell partido de acceso, que la extensión de primer arranque también fija, y el rechazo de credenciales indiferenciado, que acá viene exigido por `CU-10002` §6 y no por esta extensión |

Ningún modelo UX-UI de `Devs/Modelos-UX-UI/` está elegido a esta fecha: esa elección es del paso 1 de la Fase B2 y la toma el humano.

### 2.4 Las tres reglas de arquitectura como restricción de diseño

Son de nivel producto y **una superficie que las viole es un defecto**, no una alternativa. Se enuncian acá porque condicionan lo que se puede diseñar, no sólo lo que se puede implementar.

| Regla | Qué prohíbe en el diseño | Qué obliga en su lugar |
| --- | --- | --- |
| RA-01 · Ningún guion del navegador invoca la pieza de datos | Prohíbe toda actualización parcial que implique una llamada del navegador al servicio de datos: nada de autocompletado que consulte, nada de validación remota al escribir, nada de listado que se rellene solo, nada de sondeo de estado | Todo dato llega por el servidor de la pieza pública, en el ciclo de la interacción. Un listado se refresca cuando la persona vuelve a él, no solo. El estado nuevo de un trabajo aparece en el listado del alumno la próxima vez que lo pida, y eso se declara en el estado vacío y en la microcopy en lugar de simularse |
| RA-02 · El bundle del visualizador es un visualizador puro | Prohíbe que cualquier superficie manipule el elemento de dibujo por su cuenta: nada de superponer marcas sobre la escena, nada de leer su contenido, nada de capturarla, nada de tocar su interior | La escena se opera **sólo** por las **seis** funciones, y es la pieza pública la que consulta el entorno del navegador y le manda los dos valores de verdad del movimiento automático: el bundle **no consulta nada**. El resaltado de una pieza se pide por `seleccionarPieza`; el ajuste al cambiar el tamaño disponible se pide por `redimensionar` y **no ocurre solo**; la liberación se pide por `destruir` al descartar el componente. La lista de piezas no dibujadas se presenta **al lado** de la escena, no encima |
| RA-03 · Todo lo que el navegador deba obtener pasa por la pieza pública | Prohíbe que un mensaje visible incluya la dirección de un servicio interno, un nombre de archivo de datos o una traza. Prohíbe también enlazar recursos a un origen que no sea el de la pieza pública | El detalle técnico de una falla vive del lado del servidor. Lo que la persona ve es qué pasó y qué puede hacer. El detalle de diagnóstico del sello de versión expone la identidad del artefacto, **no** la topología |

Consecuencia de diseño que conviene tener presente al leer los wireframes: **no hay optimismo de interfaz**. Ninguna superficie muestra un resultado antes de que el servidor lo confirme, porque el ida y vuelta es el único canal que hay y adelantarlo produciría estados que después habría que retirar.

## 3. Flujos clave

### 3.1 Mapa de superficies y su recorte

Una **superficie** es la unidad maquetable: una ruta con su conjunto propio de estados, o un diálogo con flujo propio. El criterio de recorte es **una superficie por caso de uso**, con dos ajustes declarados. Da once superficies, sobre el mínimo de cuatro que `Rules-UX-UI-DX.md` §2.2 fija para `web-monolith`.

| Nombre canónico de superficie | Wireframe | Caso de uso origen | Shell |
| --- | --- | --- | --- |
| `Aprovisionamiento-Inicial` | [`Wireframes-Aprovisionamiento-Inicial.md`](Wireframes-Aprovisionamiento-Inicial.md) | CU-10004 FA-03 y FA-04 | Acceso |
| `Registro-De-Cuenta` | [`Wireframes-Registro-De-Cuenta.md`](Wireframes-Registro-De-Cuenta.md) | CU-10001 | Acceso |
| `Ingreso` | [`Wireframes-Ingreso.md`](Wireframes-Ingreso.md) | CU-10002 | Acceso |
| `Credencial-Propia` | [`Wireframes-Credencial-Propia.md`](Wireframes-Credencial-Propia.md) | CU-10003 | Acceso en establecimiento **y en cambio forzado**, trabajo en cambio voluntario |
| `Panel-De-Trabajos-Del-Alumno` | [`Wireframes-Panel-De-Trabajos-Del-Alumno.md`](Wireframes-Panel-De-Trabajos-Del-Alumno.md) | CU-10006 | Trabajo |
| `Envio-De-Trabajo` | [`Wireframes-Envio-De-Trabajo.md`](Wireframes-Envio-De-Trabajo.md) | CU-10005 | Trabajo |
| `Vista-De-Trabajo` | [`Wireframes-Vista-De-Trabajo.md`](Wireframes-Vista-De-Trabajo.md) | CU-10007 | Trabajo |
| `Resolucion-Del-Trabajo` | [`Wireframes-Resolucion-Del-Trabajo.md`](Wireframes-Resolucion-Del-Trabajo.md) | CU-10009 | Trabajo, alojada en `Vista-De-Trabajo` |
| `Panel-De-Cuentas` | [`Wireframes-Panel-De-Cuentas.md`](Wireframes-Panel-De-Cuentas.md) | CU-10004 flujo principal, FA-01, FA-02, **FA-06**, **FA-07** y FA-05 | Trabajo |
| `Listado-De-La-Comision` | [`Wireframes-Listado-De-La-Comision.md`](Wireframes-Listado-De-La-Comision.md) | CU-10008 | Trabajo |
| `Estado-Degradado-Y-Reconexion` | [`Wireframes-Estado-Degradado-Y-Reconexion.md`](Wireframes-Estado-Degradado-Y-Reconexion.md) | CU-10010 | Los dos, por superposición |

Los dos ajustes al criterio de una superficie por caso de uso:

1. **CU-10004 se parte en dos superficies.** Su flujo principal es una lista de cuentas dentro del shell de trabajo; su FA-03 es el aprovisionamiento inicial, que vive en el shell partido, sin navegación, se usa **una vez en la vida de la instancia** y tiene un guard propio. Compartir un archivo habría obligado a mezclar dos mapas de estados que no se tocan, y la maqueta no habría podido nombrar la superficie de arranque. La partición es de presentación y **no** contradice la fusión que 02 decidió: allá el criterio era el objeto sobre el que se actúa, acá es la unidad maquetable.
2. **CU-10010 se emite como superficie propia aunque sea transversal.** El estado degradado es una superficie, no un error: la aplicación sigue en pie y lo informa. Se documenta una vez, con sus dos tratamientos independientes, y las otras diez superficies la referencian en lugar de redibujarla.

Y una fusión: **CU-10009 no tiene ruta propia.** El bloque de decisión vive dentro de `Vista-De-Trabajo` cuando quien mira es el administrador y el trabajo está en estado `Pendiente`, y sus dos diálogos de confirmación son diálogos con flujo propio. Se emite como wireframe separado porque tiene su propio mapa de estados y su propia lista de interacciones, y porque la maqueta lo va a tener que demostrar como recorrido; su alojamiento se declara en su sección 1.

### 3.2 Los dos shells

El producto tiene exactamente dos armazones, y la frontera entre ellos es tener sesión y sistema operable. Es el **shell partido** de `Design-Rules-Primer-Arranque` §4.1, con la regla del documento base de no ofrecer puertas que todavía no abren.

**Shell de acceso.** Sin barra lateral, sin barra superior, sin navegación. Lienzo con una tarjeta de ancho acotado, anclada a la franja superior y no al centro vertical, con el sello de versión al pie. Lo usan `Aprovisionamiento-Inicial`, `Registro-De-Cuenta`, `Ingreso` y `Credencial-Propia` en sus cursos de establecimiento **y de cambio forzado**. El cambio forzado lo usa por el mismo motivo que el establecimiento: **la persona todavía no tiene sesión de trabajo**. El `PRODUCT-INTAKE` **1.8** precisa RN-10013 en ese sentido —la contraseña provisoria se reconoce y encamina, y no otorga sesión—, de modo que la frontera de este párrafo no tiene excepciones: mientras la provisoria no se cambie **no hay ninguna otra ruta a la que ir**, y una barra lateral prometería destinos que no existen.

```text
+------------------- lienzo, sin chrome -------------------+
|                                                          |
|            +--------- ancho acotado --------+            |
|            |  identidad del laboratorio     |            |
|            |  <título de la tarea>          |            |
|            |  [ banda de resultado        ] |            |
|            |  <campos>                      |            |
|            |  [===== acción primaria =====] |            |
|            |  <enlace a la otra ruta pública>|           |
|            +--------------------------------+            |
|                  <sello de versión>                      |
+----------------------------------------------------------+
```

**Shell de trabajo.** Barra lateral fija con la identidad del laboratorio arriba, tres destinos según el papel al medio, y al pie —separados por una línea tenue— la identidad de la persona, el cierre de sesión y el sello de versión. Área de contenido sobre el lienzo. Lo usan las seis superficies restantes.

```text
+--------------+-------------------------------------------+
| Laboratorio  |  <título de la superficie>                |
|              |  <subtítulo de una línea>   [ acción 1ª ] |
| · destino 1  |  ---------------------------------------- |
| · destino 2  |                                           |
| · destino 3  |  <contenido de la superficie>             |
|              |                                           |
| ------------ |                                           |
| <persona>    |                                           |
| [Cerrar ses.]|                                           |
| <sello>      |                                           |
+--------------+-------------------------------------------+
```

Los tres destinos por papel, que aplican la ley de Hick y respetan que el producto no tiene más rutas que éstas:

| Papel | Destino 1 | Destino 2 | Destino 3 |
| --- | --- | --- | --- |
| Alumno | Mis trabajos (`Panel-De-Trabajos-Del-Alumno`) | Trabajo nuevo (`Envio-De-Trabajo`) | Mi contraseña (`Credencial-Propia`) |
| Administrador | Entrega de la comisión (`Listado-De-La-Comision`) | Cuentas (`Panel-De-Cuentas`) | Mi contraseña (`Credencial-Propia`) |

**Ninguna barra lateral muestra el destino del otro papel**, ni siquiera deshabilitado: la ruta no se arma, y ofrecerla sería mostrar una puerta que no abre.

### 3.3 Flujo A — Primer arranque del laboratorio

**Disparador.** Alguien abre la instancia recién desplegada. El predicado único de aprovisionamiento —«existe la cuenta de administrador»— es falso.

**Pasos.** Cualquier ruta que se pida resuelve el predicado y redirige, con indicador de progreso mientras resuelve y reemplazando la entrada del historial. Con el predicado en falso, el destino es `Aprovisionamiento-Inicial`: una sola superficie, sin navegación y **sin acción de cancelar**, donde se declara el correo y la contraseña de la única cuenta de administrador que el laboratorio va a tener. El acto es explícito, indivisible e irreversible desde la interfaz. Al completarse, el destino declarado es `Ingreso`, que **acusa recibo** con una banda de confirmación, y el primer ingreso lleva a `Panel-De-Cuentas` con la orientación posterior.

**Fricción anticipada.** Que la persona no entienda que ese formulario se usa una vez y no vuelve. Se resuelve con el subtítulo, que declara la unicidad antes del intento, y con la ausencia de escape, que dice lo mismo con la forma.

**Salida.** Existe la cuenta de administrador y la ruta de aprovisionamiento **deja de armar formulario para siempre**. Cualquier apertura posterior redirige de forma neutra a `Ingreso`, sin explicar por qué.

### 3.4 Flujo B — Alta del alumno de punta a punta

**Disparador.** Empieza la cursada y el alumno recibe la dirección del laboratorio.

**Pasos.** `Registro-De-Cuenta` con tres campos y **ningún campo de contraseña**; la superficie declara antes del intento que la cuenta va a quedar a la espera de habilitación. El alumno intenta `Ingreso` y recibe el motivo de su situación de cuenta, sin sesión. El docente habilita desde `Panel-De-Cuentas`. El alumno vuelve a `Ingreso` y es derivado a `Credencial-Propia` en su curso de establecimiento. Establece la contraseña, vuelve a `Ingreso` con la banda de confirmación, entra y aterriza en `Panel-De-Trabajos-Del-Alumno` **vacío**.

**Fricción anticipada.** Tres, y las tres tienen tratamiento declarado:

1. *El alumno espera un correo que no llega.* La superficie de registro declara explícitamente que el laboratorio **no envía correos** y que la habilitación la hace el docente.
2. *El alumno olvida su contraseña.* **No hay recuperación autónoma** y ninguna superficie ofrece una: no hay canal de correo. Lo que sí hay desde el `PRODUCT-INTAKE` 1.7 es el **reseteo por el administrador** (F-26): el texto de `Ingreso` declara que hay que pedírselo al docente y que **no se pierde ningún trabajo**, `Panel-De-Cuentas` lo ofrece como quinta operación de la fila, y el alumno **se autentica con la provisoria, no obtiene sesión de trabajo** y elige la suya en `Credencial-Propia`, en su curso de cambio forzado. **Hasta 1.6 el único remedio era la baja y el alta nueva, que arrastraba todos los trabajos**: esa fricción era la más cara del producto y dejó de existir.
3. *El alumno llega a un panel vacío y no sabe qué hacer.* El estado vacío no es un hueco: es una invitación con la acción siguiente.

**Salida.** El alumno tiene identidad propia, sesión y un panel donde cargar.

### 3.5 Flujo C — Cargar y enviar un trabajo

**Disparador.** El alumno terminó de correr su programa y tiene el texto en el portapapeles.

**Pasos.** `Envio-De-Trabajo` con nombre, fecha, descripción y el área donde se pega el texto. El alumno pega **tal como su programa lo emitió**, con sus comas finales y sus claves: ninguna superficie le pide una corrección y ninguna reescribe un carácter. Pide previsualizar y la escena se dibuja **sin ninguna llamada al servicio de datos**. Envía, que es la **única acción de guardado que existe**. El resultado decide el estado y la superficie lo presenta con sus observaciones. Vuelve al listado, donde el trabajo ya figura.

**Fricción anticipada.** La grande es que el alumno busque «guardar sin enviar» y no lo encuentre. Se resuelve declarándolo: la superficie tiene **una** acción de guardado, y el texto de apoyo dice que un envío que no verifica queda en borrador y se puede reenviar cuantas veces haga falta. La segunda es que confunda la previsualización con una validación; la superficie declara que la previsualización **dibuja y no verifica**, y que las piezas que no se dibujan **no son** errores del trabajo.

**Salida.** El trabajo existe, con dueño, identificador y estado. Si verificó, está en estado `Pendiente` y **deja de ser editable y eliminable por el alumno**; si no, quedó en `Borrador` con sus errores localizados por índice de figura y campo.

### 3.6 Flujo D — Explorar un trabajo

**Disparador.** El alumno abre un trabajo suyo desde su listado, o el administrador abre uno de la comisión desde el suyo. **La superficie es la misma para los dos**, y esa identidad es un criterio de éxito de negocio.

**Pasos.** La superficie se arma con **cuatro partes y una disposición ya decidida y probada en el aula**, que este documento precisa y no reinventa: a la izquierda los datos del trabajo y su texto original; a la derecha el elemento de dibujo arriba y el árbol de la estructura abajo. Se inicializa la instancia, se carga el texto **una sola vez por trabajo**, se dibuja, se presenta el árbol y se enumeran junto a la escena las piezas que no se dibujaron con su índice: **ninguna desaparece sin dejar registro**. Seleccionar un elemento del árbol resalta esa pieza y sólo esa, por el mismo índice. Al abandonar la superficie se libera la instancia.

**Fricción anticipada.** Que el alumno lea las piezas no dibujadas como errores de su trabajo. Se resuelve por ubicación y por rótulo: viven junto a la escena, en el bloque del dibujo, y **nunca** en la lista de observaciones.

**Salida.** La persona vio los datos, el texto íntegro, la escena y el árbol, con las observaciones y, si lo hay, el comentario en bloque propio.

### 3.7 Flujo E — Revisión de la comisión y desenlace

**Disparador.** El docente se sienta a revisar la entrega.

**Pasos.** `Listado-De-La-Comision` agrupado por alumno y filtrable por alumno, **sin ningún trabajo en estado `Borrador`**. Abre uno, lo revisa con la misma superficie que vio el alumno y decide: aprobar o rechazar, con un comentario escrito **opcional**. Vuelve al listado, que ya refleja el estado nuevo. Puede además retirar cualquier trabajo que ve, en cualquiera de sus tres estados visibles, con confirmación.

**Fricción anticipada.** Que resuelva desde el listado sin abrir el trabajo. **El listado no ofrece aprobar ni rechazar**: la decisión sólo existe dentro de la superficie donde el trabajo está a la vista, y eso es deliberado. La segunda es que el docente crea que la ausencia de un alumno en el listado agrupado significa un trabajo perdido; la superficie lo declara.

**Salida.** El trabajo está en un estado terminal. **Ninguna transición sale de ahí**, y la superficie deja de ofrecer las decisiones sobre él. El alumno se entera del desenlace por el **estado en su listado**, y lee el comentario **al abrir el trabajo**: el listado no arrastra texto libre, y es una decisión de contrato ya tomada que el diseño respeta.

### 3.8 Flujo F — Algo se corta

**Disparador.** Dos disparadores distintos, en dos tramos independientes, que se avisan distinto. Confundirlos es el error de lectura más probable de todo este documento.

| Tramo | Qué se corta | Qué ve la persona | Quién lo detecta |
| --- | --- | --- | --- |
| Navegador ↔ pieza pública | La conexión viva que sostiene la interacción | **Cartel de reconexión**: banda superpuesta en el borde superior, propia del circuito, con reintento y cuenta de intentos | El propio circuito |
| Pieza pública → pieza de datos | La obtención de datos | **Aviso de indisponibilidad**: el estado degradado, dentro del área de contenido de la superficie donde la persona estaba, con el shell intacto y la acción de reintentar | La pieza pública, al recibir el código correspondiente |

**Pasos del segundo tramo.** La acción falla, **no se propaga ninguna excepción sin manejar**, la superficie conserva a la vista lo que la persona escribió y ofrece reintentar. Al volver el servicio, el reintento procede y el aviso desaparece **sin volver a ingresar**.

**Fricción anticipada.** Que un listado sin elementos se lea como una caída. Se resuelve con dos representaciones distintas y con la regla de que se distinguen **por el tipo recibido y no por el conteo**: un estado vacío tiene ilustración neutra y acción siguiente; un aviso de indisponibilidad tiene color de peligro y reintento.

**Salida.** La aplicación siguió en pie y lo dijo. Ninguna acción quedó a medio aplicar y nadie vio una pantalla rota.

## 4. Estados y feedback

### 4.1 Tabla canónica de estados

Hereda la tabla del documento base del catálogo y le suma las cinco filas que este producto necesita. **Todo estado se comunica por al menos dos canales**: nunca sólo por color.

| Estado | Condición que lo produce | Feedback visual | Feedback textual |
| --- | --- | --- | --- |
| Vacío | La colección llegó con cero elementos, declarado por el tipo recibido | Ilustración vectorial neutra centrada y acción siguiente | Qué falta y cuál es el paso siguiente |
| Cargando | Operación en curso contra el servidor | Esqueleto por fila en listados; indicador en el control y control inhabilitado en acciones puntuales | Qué se está trayendo |
| Con datos | Hay contenido | Presentación normal | — |
| Error de entrada | Un campo viola una regla que la superficie declaró antes | Borde de peligro en el campo y banda de resultado en la superficie | Qué regla, enunciada igual que en el requisito declarado |
| Error de operación | La acción no procedió por una condición del contrato | Banda de resultado con estado de peligro | Qué pasó, por qué y qué hacer |
| Éxito | La acción se completó | Confirmación sutil, o banda de confirmación cuando cierra el lazo entre dos superficies | El verbo del aviso coincide con el del control |
| Sin acción disponible | El estado del trabajo o el papel no admiten la acción | **El control no se dibuja** | Cuando la ausencia puede leerse como falla, una línea la explica |
| **Indisponible** | La pieza de datos no responde | Aviso de indisponibilidad en el área de contenido, shell intacto | Que el laboratorio no tiene los datos ahora, y reintentar |
| **Reconectando** | Se cortó el circuito | Cartel de reconexión superpuesto en el borde superior | Que la conexión se cortó y se está reintentando |
| **Escena no disponible** | El navegador no provee la capacidad gráfica tridimensional | El área de la escena se reemplaza por un bloque explicativo | Que la escena no está disponible en ese navegador, y que el resto sigue |
| **Sin observaciones** | La colección de observaciones llegó con cero elementos | Línea explícita en el bloque de observaciones | «Sin observaciones», nunca un bloque en blanco |

### 4.2 Mapa de estados por superficie

Cada celda marcada es un estado que **la maqueta de la Fase B2 va a tener que demostrar**. Un estado no declarado acá y en la sección 5 de su wireframe no se maqueta y por lo tanto no se valida.

| Superficie | Vacío | Cargando | Con datos | Error entrada | Error operación | Éxito | Indisponible | Reconectando | Propios |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| `Aprovisionamiento-Inicial` | — | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Resolviendo destino; ya aprovisionado |
| `Registro-De-Cuenta` | — | Sí | Sí | Sí | Sí | Sí | Sí | Sí | — |
| `Ingreso` | — | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Cuenta no habilitada; contraseña sin establecer; sesión cerrada |
| `Credencial-Propia` | — | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Establecimiento; cambio; **cambio forzado**; provisoria rechazada |
| `Panel-De-Trabajos-Del-Alumno` | Sí | Sí | Sí | — | Sí | Sí | Sí | Sí | Acciones por estado del trabajo |
| `Envio-De-Trabajo` | — | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Previsualizado; no verificó; verificó con advertencias; escena no disponible |
| `Vista-De-Trabajo` | — | Sí | Sí | — | Sí | — | Sí | Sí | Sin observaciones; sin comentario; piezas no dibujadas; texto no legible; escena no disponible; índice sin representación |
| `Resolucion-Del-Trabajo` | — | Sí | Sí | — | Sí | Sí | Sí | Sí | Resuelto; no resoluble; confirmando retiro |
| `Panel-De-Cuentas` | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Confirmación escrita pendiente; confirmación no coincide; orientación posterior; **confirmación de reseteo pendiente; provisoria a la vista; reseteo no admitido** |
| `Listado-De-La-Comision` | Sí | Sí | Sí | — | Sí | — | Sí | Sí | Filtrado sin resultados; alumno del filtro inexistente |
| `Estado-Degradado-Y-Reconexion` | — | — | — | — | — | Sí | Sí | Sí | Recuperado; sesión no restablecible |

### 4.3 Cómo se muestran los cuatro estados del trabajo

El conjunto es cerrado y tiene cuatro valores, dos de ellos terminales. **El diseño tiene que hacer obvio qué se puede hacer en cada uno**, y lo hace por ausencia de control y no por control inhabilitado.

| Estado | Insignia | Qué ofrece la superficie al alumno | Qué ofrece la superficie al administrador |
| --- | --- | --- | --- |
| `Borrador` | Neutra | Abrir, volver sobre él, eliminar | **Nada: no lo ve.** No aparece en su listado y pedirlo por dirección directa devuelve «no encontrado» |
| `Pendiente` | Atención | Sólo abrir | Abrir, aprobar, rechazar, retirar |
| `Finalizado` | Éxito | Sólo abrir. Al abrirlo, el comentario si lo hay | Abrir, retirar. **No** ofrece salir del estado |
| `Rechazado` | Peligro | Sólo abrir. Al abrirlo, el comentario si lo hay. Corregir significa **cargar un trabajo nuevo** | Abrir, retirar. **No** ofrece salir del estado |

Tres reglas de presentación que se derivan de la tabla y que valen en todas las superficies:

1. **La insignia lleva siempre su texto.** El color es refuerzo y nunca el único canal.
2. **Lo que el estado no admite no se dibuja.** Un control inhabilitado invita a averiguar por qué; un control ausente no. La única excepción son las acciones que se inhabilitan **durante** el envío para evitar el doble disparo, que es una condición momentánea y no una regla.
3. **La insignia de `Rechazado` no reprocha.** Es color de peligro por convención de estado, y su texto es el estado, no un juicio.

## 5. Accesibilidad

### 5.1 Compromiso y criterios prioritarios

**WCAG 2.2 nivel AA es el piso obligatorio de este producto**, no una mejora deseable. Es un producto educativo de una universidad pública y la audiencia es una comisión entera: la probabilidad de que alguien navegue con teclado, con lector de pantalla o con baja visión no es hipotética. Ninguna superficie se da por terminada sin cumplirlo.

| Criterio | Cómo se aplica acá |
| --- | --- |
| Contraste de texto 4.5:1, texto grande 3:1, componentes y foco 3:1 | Se hereda de los tokens del catálogo. **El sello de versión cumple el piso pese a su jerarquía baja**: información secundaria no significa información ilegible |
| Foco visible en todo elemento interactivo | Anillo de al menos 2 px que no depende sólo del color. No se suprime el indicador del sistema de componentes |
| Navegación completa por teclado, en el orden lógico de lectura, sin trampas | Incluye los diálogos de confirmación, el árbol de la estructura y los controles del listado. El árbol se recorre con flechas y se activa con la barra o el ingreso |
| Objetivos de toque de al menos 24×24 px | Se aplica a las acciones por fila de los dos listados y del panel de cuentas, que es donde el riesgo de objetivos pequeños es real |
| Semántica y puntos de referencia | Cada superficie tiene un encabezado de primer nivel que nombra su tarea, incluido el shell de acceso, que **no puede quedar sin estructura por no tener navegación**. Puntos de referencia de navegación y de contenido principal en el shell de trabajo |
| Etiquetas asociadas a cada control | Etiqueta visible arriba del control. **El texto de ejemplo no sustituye a la etiqueta** en ningún campo |
| Mensajes de error asociados al campo y anunciados | El requisito declarado y el error se asocian al control que describen; la banda de error se anuncia como alerta y la de confirmación como estado |
| Cambios dinámicos anunciados | El aviso de indisponibilidad, el cartel de reconexión, el resultado del envío y la confirmación de copiado del diagnóstico se anuncian como regiones activas. Un cambio sólo visual no alcanza |
| El color no es el único canal | Las cuatro insignias de estado llevan texto. Las observaciones llevan su severidad escrita, no sólo su color |
| Movimiento reducido respetado | Las transiciones no esenciales se desactivan cuando el sistema lo pide. La escena tridimensional **sí puede moverse sola** —capacidad F-25, `Must Have` desde el `PRODUCT-INTAKE` 1.7, con sus dos movimientos independientes: la órbita de la cámara y el giro de las figuras—, y por eso el movimiento ambiental se resuelve y no se evita: los dos movimientos se gobiernan con **casillas visibles al pie del área de dibujo**, se detienen mientras la persona arrastra y con la pestaña oculta, y **arrancan destildados cuando el sistema declara preferencia de movimiento reducido**, con el control declarando por qué. Con las dos destildadas la escena sólo se mueve por acción de la persona. **Quien consulta esa preferencia es la pieza pública**, que la traduce a los **dos valores de verdad** que le manda al visor por la fachada: el visor **no consulta nada** —ni esa preferencia, ni configuración, ni almacenamiento—, porque es un visualizador puro (RA-02, RT-13) |
| Foco gestionado | Foco inicial en el primer campo de las tarjetas de acceso. Tras un error, el foco vuelve a la banda o al primer campo inválido. Al abrir un diálogo, el foco entra; al cerrarlo, vuelve al control que lo abrió |

### 5.2 La escena tridimensional y su equivalente accesible

Es el punto donde este producto tiene un riesgo de accesibilidad que ningún catálogo resuelve solo, y la resolución es de diseño.

- **La escena no es la única vía de acceso a la información del trabajo.** El árbol de la estructura presenta el mismo contenido en forma navegable por teclado y anunciable, y la selección va en los dos sentidos por el mismo índice. Quien no puede ver la escena tiene el árbol; quien no puede recorrer el árbol tiene la escena.
- **Ninguna pieza desaparece sin quedar enumerada en texto.** Las piezas no dibujadas figuran con su índice junto a la escena. Es la eliminación del fallo silencioso, y de paso es lo que hace que la información del dibujo exista fuera del dibujo.
- **El elemento de dibujo declara su naturaleza y su contenido en una alternativa textual**, que se compone con el recuento de piezas dibujadas y no dibujadas del resultado. La alternativa la arma el componente anfitrión con lo que la fachada devuelve: **no se lee del interior de la escena**, porque eso violaría RA-02.
- **La ausencia de capacidad gráfica no rompe nada.** Es un estado declarado en tres superficies y su tratamiento es reemplazar el área de la escena por un bloque explicativo, manteniendo las otras partes.

### 5.3 Cómo se verifica

La verificación es de `08-Calidad-Y-Pruebas`; acá se declara qué tiene que poder verificarse, para que esa categoría tenga contra qué escribir.

| Qué se verifica | Cómo | Cuándo |
| --- | --- | --- |
| Contraste de cada par texto/fondo y de cada estado de foco | Medición sobre los tokens del tema, una sola vez, porque los tokens son la fuente única y ninguna superficie define color propio | Al fijar el tema, y ante cualquier cambio del tema |
| Recorrido completo por teclado de las once superficies, sin trampa de foco | Guion manual de teclado por superficie, incorporado al guion de demostración de la etapa que la introduce | Por etapa, y acumulativo por la regla de no regresión |
| Semántica, etiquetas, puntos de referencia y regiones activas | Análisis automatizado por superficie sobre la maqueta y sobre la aplicación, más revisión manual de las regiones activas, que el análisis automatizado no puede juzgar solas | En la Fase B2 sobre la maqueta, y por etapa sobre la aplicación |
| Anuncio efectivo del aviso de indisponibilidad, del resultado del envío y del cartel de reconexión | Prueba manual con lector de pantalla sobre los tres escenarios | Una vez por escenario, en la etapa que lo introduce |
| Que ningún estado se comunique sólo por color | Revisión en escala de grises de las cuatro insignias y de las dos severidades de observación | En la Fase B2 |
| Objetivos de toque de las acciones por fila | Medición sobre las tres superficies con acciones por fila | En la Fase B2 |

**Criterio de cierre.** Una superficie no se da por aprobada en la Fase B2 si no pasó el recorrido por teclado y la revisión en escala de grises. Los dos son baratos y son los que atrapan la mayor parte de los defectos de esta clase.

## 6. Internacionalización

- **Un solo idioma: español rioplatense neutro técnico.** No hay ni va a haber selector de idioma en esta versión, y ninguna superficie lo dibuja. La audiencia es una comisión de una materia dictada en español.
- **Dirección de lectura de izquierda a derecha**, única soportada. No se declara compromiso con la dirección inversa porque no hay a quién servírselo.
- **Expansión de texto.** Aunque no haya traducción, los textos se tratan como variables y no como medidas: ninguna superficie depende de que una etiqueta ocupe un ancho exacto. Los contenedores toleran una expansión del 30 %, que es el margen que evita que un cambio de redacción rompa una fila.
- **Formato de fecha.** Día, mes y año en el orden corriente del país, con el mes en letras cuando el espacio lo permite, para que no se confunda con el orden inverso. La fecha del trabajo la declara el alumno; la fecha de registro la produce el sistema, y **las dos se rotulan distinto** para que no se lean como la misma cosa.
- **Formato de número.** Coma decimal, que es la convención del país. **Excepción declarada y deliberada**: los valores declarado y derivado de una advertencia se muestran **exactamente como el texto del alumno los trae y como el sistema los recalcula**, sin reformatear. Reescribirlos rompería la comparación que es el mayor valor didáctico del producto, y contradiría que el texto original se conserva íntegro.
- **Vocabulario.** El de la cátedra, declarado aguas arriba. Ninguna superficie introduce un anglicismo donde el glosario tiene un término, y **no aparece en ninguna pantalla el nombre del formato del texto del alumno**: el producto habla del texto del trabajo, no de su sintaxis.

## 7. Performance percibida

Toda interacción cruza el circuito hacia el servidor de la pieza pública, y desde ahí puede cruzar hacia la pieza de datos. Además, el producto se despliega en un lugar público gratuito cuya latencia y estabilidad son incógnitas medidas por puertas técnicas. El diseño no puede suponer respuesta inmediata.

| Acción | Tolerancia percibida | Tratamiento |
| --- | --- | --- |
| Abrir un listado | Hasta 400 ms sin indicador; más allá, esqueleto por fila | Esqueleto con la cantidad de filas del último recorrido conocido, o tres si no hay ninguno. **Nunca una tabla vacía mientras carga**: se confundiría con el estado vacío |
| Abrir la vista de trabajo | Hasta 400 ms sin indicador | Indicador fino en la parte superior del contenido y esqueleto en las cuatro partes. La escena se inicializa **después** de que el elemento de dibujo tiene tamaño |
| Enviar un trabajo | Es la acción más cara y la persona lo sabe | Control inhabilitado con indicador dentro, **prevención de doble envío**, y texto que declara que se está interpretando el trabajo. Sin límite de tiempo visible: inventar una cuenta regresiva sería prometer algo que la topología no garantiza |
| Cambiar la situación de una cuenta o resolver un trabajo | Acción puntual | Control inhabilitado con indicador; el resultado llega como confirmación y la lista se vuelve a pedir |
| Dibujar la escena y girarla | **Cero espera de red por contrato** | La escena se opera en el navegador y **no hay tráfico de circuito durante la interacción**. Es el único lugar del producto donde la respuesta es inmediata, y conviene no desperdiciarlo con animaciones de entrada |
| Escribir en un campo | Sin ida y vuelta | **Ninguna validación consulta al servidor mientras se escribe.** Lo prohíbe RA-01 y además lo desaconseja el repliegue del transporte, que degrada la latencia al tipear |

Reglas transversales:

- **Sin optimismo de interfaz.** Ninguna superficie muestra el resultado antes de la confirmación del servidor. El ida y vuelta es el único canal, y adelantar un estado obligaría a retirarlo.
- **El texto del trabajo viaja del servidor al navegador una sola vez por trabajo.** Ni el árbol ni la escena se vuelven a componer desde el servidor. Esto es contrato, y una superficie que lo rompa es un defecto.
- **Transiciones de 150 a 250 ms**, al servicio del cambio de estado. Sin movimiento ambiental permanente.
- **El repliegue a un transporte de mayor latencia no se anuncia a la persona.** Es un trade-off aceptado aguas arriba y no es una degradación del laboratorio: avisarlo sería alarmar sin darle a nadie nada que hacer.

## 8. Errores y recuperación

### 8.1 Taxonomía de lo que la persona ve

Cinco clases, y la distinción entre ellas es de diseño y no de implementación: cada una tiene su ubicación, su tono y su vía de salida.

| Clase | Qué es | Dónde se muestra | Vía de recuperación |
| --- | --- | --- | --- |
| Requisito no cumplido | Un campo vacío o dos escrituras que no coinciden, detectado sin salir hacia el servicio de datos | Junto al campo, más banda de resultado si hay más de uno | Corregir y reintentar. **Lo escrito se conserva** |
| Condición del contrato | El servicio respondió que la operación no procede: correo ya registrado, confirmación que no coincide, estado que no admite la acción, credencial inválida | Banda de resultado de la superficie, o junto al campo cuando el contrato nombra uno | Corregir y reintentar, o volver al listado cuando la terminación es controlada |
| **Observación del trabajo** | Lo que el producto emite al interpretar el texto: **advertencia**, que no impide el paso a estado `Pendiente`, y **error de validación**, que sí lo impide | Lista de observaciones, con severidad, índice de figura y campo señalado, y con los dos valores cuando es advertencia | El alumno corrige su programa y vuelve a enviar. **Una advertencia no pide corrección**: informa |
| Condición del dibujo | Una pieza que la fachada no dibujó, o un texto del que no obtuvo piezas | **Junto a la escena, nunca en la lista de observaciones** | Ninguna acción del producto. No es un defecto del trabajo |
| Indisponibilidad | El servicio de datos no responde, o un fallo que el contrato no previó | Aviso de indisponibilidad en el área de contenido | Reintentar. Lo escrito se conserva a la vista |

**La tercera y la cuarta clase son distintas y la interfaz no las mezcla nunca.** Una pieza que no se dibuja no es un error del trabajo: quien decide si el trabajo verifica es la pieza de datos. Mezclarlas le diría al alumno que corrija algo que está bien.

Y una sexta cosa que **no es** un error y que no se presenta como tal: el **comentario del administrador**. No es una observación, no es una calificación, no lleva severidad, ni índice, ni campo. Vive en un bloque propio, separado de las observaciones, y su tono visual es neutro.

### 8.2 Tono de los mensajes

- **Voz activa y segunda persona.** «Todavía no cargaste ningún trabajo», no «no se han encontrado trabajos».
- **Sin disculpas y sin culpas.** Ni «lo sentimos», ni «usted ingresó un valor inválido». El mensaje dice qué pasó, por qué y qué hacer.
- **El verbo del aviso coincide con el del control.** El control dice «Enviar», la confirmación dice «Enviado».
- **Los errores del alumno son didácticos, no sancionatorios.** Una advertencia de valor declarado contra derivado es el mayor valor del producto: se presenta como un dato que el alumno mira, con los dos números a la vista, y **no como una falta que hay que corregir para avanzar**. Ninguna advertencia bloquea nada, y el texto no sugiere que sí.
- **Sin jerga de implementación.** No aparecen números de código, nombres de contrato ni identificadores internos en el cuerpo de un mensaje. El código del contrato decide **qué** texto se muestra; no se muestra el código.
- **El rechazo de credenciales no distingue.** Un único mensaje que no declara si falló el correo o la contraseña, porque distinguirlos confirma la existencia de la identidad.

### 8.3 Lo que ningún mensaje contiene

Es la aplicación directa de RA-03 y se verifica leyendo, no inspeccionando:

- La dirección de un servicio interno, en ninguna forma.
- El nombre de un archivo de datos.
- Una traza de la implementación, ni completa ni recortada.
- El motivo por el que un guard redirigió. La redirección es **neutra**: quien abre el aprovisionamiento con el laboratorio ya aprovisionado va a `Ingreso` sin explicación, y quien pide una ruta del otro papel vuelve a la suya sin que se le revele qué había.

**No hay handoff humano ni canal de soporte.** El producto no tiene mesa de ayuda y ninguna superficie ofrece una: la única vía de escalamiento es el docente, en persona, y eso no se dibuja. Lo que sí se dibuja, y para eso existe, es el **detalle de diagnóstico** del sello de versión, con copiado en un solo gesto, para que un reporte de problema empiece por identificar la instancia y no por averiguar cuál es.

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Persona objetivo | Alumno de la comisión y docente como administrador, en `../../../00-Contexto/Vision-Producto.md` §2 y `PRODUCT-INTAKE` §2 |
| CU origen | Los diez: `CU-10001` a `CU-10010` de `../02-Especificacion-Funcional/Casos-De-Uso/`, más `Especificacion-Funcional.md` §6 (RT-01 a RT-13) y §7 |
| Reglas de negocio relevantes | `RN-10001` a `RN-10016`, que viven en `GeometriaFactory-Domain` y se referencian por identificador. Condicionan la presentación `RN-10003`, `RN-10004`, `RN-10005`, `RN-10006`, `RN-10007`, `RN-10009`, `RN-10010` y `RN-10011` |
| Reglas de arquitectura | `RA-01`, `RA-02` y `RA-03`, en `PRODUCT-INTAKE` §14, aplicadas en §2.4 |
| Contrato de fachada del visualizador | `../../GeometriaFactory-Visor/02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md` §3.2 (siete garantías), §4 (**seis** funciones, incluida `establecerMovimiento` de §4.6) y §6 (siete códigos) |
| Wireframes asociados | Los once de §3.1 |
| Representaciones asociadas | [`Representacion-Fila-De-Trabajo.md`](Representacion-Fila-De-Trabajo.md), [`Representacion-Lista-De-Observaciones.md`](Representacion-Lista-De-Observaciones.md), [`Representacion-Sello-De-Version.md`](Representacion-Sello-De-Version.md) |
| US a generar en 06 | `US-10001` a `US-10027`, según la matriz de `../02-Especificacion-Funcional/Especificacion-Funcional.md` §4 |
| Tests previstos en 08 | Guiones de demostración de las etapas `a` a `i`, acumulativos por la regla de no regresión; recorrido por teclado por superficie; análisis de semántica y de contraste; revisión en escala de grises; recuento de peticiones originadas por el navegador, con umbral exactamente 0 |
| Catálogo de diseño aplicado | `Design-Rules-Web-Generico.md`, `Design-Rules-Blazor-Mudblazor.md`, `Design-Rules-Primer-Arranque.md`, `Design-Rules-Identidad-De-Version.md`. Ver §2.3, con el motivo de las dos extensiones que no aplican |
| Configuración dirigida por esquema aplicada | **N/A.** No hay superficies de configuración que la persona fije; la dirección de la pieza de datos es configuración de entorno. Ver §2.3 |
| Primer arranque aplicado | **Sí.** Predicado único, guard en tres capas y destino al completar, en §3.3 y en [`Wireframes-Aprovisionamiento-Inicial.md`](Wireframes-Aprovisionamiento-Inicial.md) |
| Acceso de operador único aplicado | **N/A.** Dos papeles y gestión de cuentas. Ver §2.3 |
| Identidad de versión aplicada | **Sí.** Contrato, dos ubicaciones obligatorias y detalle de diagnóstico, en [`Representacion-Sello-De-Version.md`](Representacion-Sello-De-Version.md) |
| Modelo UX-UI aplicado en la Fase B2 | Sin definir a esta fecha. Lo elige el humano en el paso 1 de la Fase B2 |
| Validación visual de maqueta | Pendiente. `requiere_maqueta` es true y la Fase B2 corre después del audit de esta fase |
| Línea de base emitida | Pendiente. `Linea-Base-Visual.md` y `Contrato-Datos-Maqueta.md` los emite AG-03M en la Fase B2 |

## 10. Notas y supuestos

- **La disposición de la vista de trabajo no se rediseña.** Viene decidida aguas arriba y probada en el aula: datos y texto a la izquierda, elemento de dibujo arriba y árbol abajo a la derecha. Este documento y su wireframe la **precisan** —qué pasa en pantalla angosta, qué pasa mientras carga, qué pasa si el texto no verifica— y no la cambian.
- **[ASUNCIÓN] El punto de quiebre principal es 768 px**, tomado del documento base del catálogo. No hay dato de campo sobre los dispositivos de la comisión; se asume el valor del catálogo y se declara para que la Fase B2 pueda desmentirlo con la maqueta a la vista.
- **[ASUNCIÓN] La escena mantiene una relación de aspecto próxima a 4:3 dentro de su bloque.** El contrato de fachada garantiza que la relación se recalcula contra el tamaño vigente y que las piezas no se deforman, pero no fija una proporción. Se asume una que deja al árbol espacio suficiente debajo, y queda sujeta a la validación visual.
- **[A VERIFICAR] La cantidad de trabajos por alumno y de alumnos por comisión.** El diseño de los dos listados supone decenas y no cientos, y por eso **no incorpora paginación**. Si la comisión resultara mucho mayor, la superficie afectada es `Listado-De-La-Comision` y el cambio es acotado. Lo que sí está declarado aguas arriba es que la agrupación y el filtro son suficientes para el alcance de aula.
- **El recuento por alumno y por estado es capacidad de prioridad menor**, prevista para una etapa posterior. Su lugar está fijado dentro de `Listado-De-La-Comision` y su wireframe lo dibuja como estado propio, para que incorporarlo no obligue a rediseñar la superficie.
- **Esta sección no decide nada de otra categoría.** La arquitectura de la capa de presentación y los registros de decisión son de 05; la construcción y la publicación, de 09; las pruebas, de 08; las historias de usuario, de 06. Y **no se especifica la maqueta**: eso es de la Fase B2.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. Marco de experiencia de la primera superficie del producto con personas reales. Declara las dos personas y su contexto, nueve heurísticas y cinco leyes UX aplicadas, los cuatro documentos del catálogo de diseño aplicados y los dos que no aplican con su motivo, las tres reglas de arquitectura traducidas a restricción de diseño, el mapa de once superficies con su criterio de recorte, los dos shells, seis flujos clave, la tabla canónica de estados con cinco filas propias y el mapa de estados por superficie que la Fase B2 tiene que demostrar, el compromiso WCAG 2.2 AA con la resolución de accesibilidad de la escena tridimensional y su plan de verificación, la política de un solo idioma con la excepción de los valores declarado y derivado, los criterios de performance percibida sin optimismo de interfaz, y la taxonomía de cinco clases de error con su tono y sus tres prohibiciones de contenido. |
| 1.0 | 2026-08-09 | Correcciones absorbidas del audit `B-02-03-GeometriaFactory-Web-r1.md` (ronda 1), **sin subir versión** por `Master-Prompt.md` §5, que lo admite mientras el documento está en estado `Propuesto`. **H-04**: §3.1 deja de usar el anglicismo que el propio `Glosario-UX.md` §4 registra como prohibido; los dos diálogos de confirmación de `Resolucion-Del-Trabajo` se nombran «diálogos con flujo propio», que es la traducción que esta sección adoptó de «un modal con flujo propio» de `Rules-UX-UI-DX.md` §3.2. |
| 1.0 | 2026-08-09 | Retroalimentación de la Fase B2 de validación de maqueta del proyecto de código `GeometriaFactory-Web`, **sin subir versión** por `Master-Prompt.md` §5, que lo admite mientras el documento está en estado `Propuesto`. **F-25**: §7 reescribe el criterio «Movimiento reducido respetado», que afirmaba que la escena tridimensional no gira sola en ningún momento. La capacidad nueva vuelve falsa esa afirmación, de modo que el movimiento ambiental pasa de evitarse a resolverse: dos movimientos independientes gobernados por casillas visibles, detenidos durante el arrastre y con la pestaña oculta, y destildados de arranque cuando el sistema declara preferencia de movimiento reducido. §3.1 ya declaraba a `Resolucion-Del-Trabajo` como alojada en `Vista-De-Trabajo` y no requiere corrección. |
| 1.1 | 2026-08-09 | **Propagación del `PRODUCT-INTAKE` 1.7**, con sus dos decisiones. **(a) F-26**: §3.1 declara el tercer curso de `Credencial-Propia` y las dos operaciones nuevas de `Panel-De-Cuentas`; §3.2 declara que el **cambio forzado es el único uso del shell de acceso con sesión iniciada**, con su fundamento; §3.4 reescribe la segunda fricción anticipada —«no hay recuperación» pasa a «no hay recuperación **autónoma**», y el remedio deja de ser la baja que arrastra los trabajos y pasa a ser el reseteo que los conserva—; §4.2 suma cuatro estados de superficie. **(b) F-25**: §7 completa el criterio de movimiento reducido con **quién consulta la preferencia** —la pieza pública, que la traduce a dos valores de verdad— y con el enunciado de que el visor **no consulta nada**, que es lo que RA-02 exige. Sube minor: agrega un curso, dos operaciones y cuatro estados al marco, y precisa un criterio de accesibilidad, sin invalidar ninguna decisión previa. |
| 1.2 | 2026-08-09 | **Reconciliación con el `PRODUCT-INTAKE` 1.8.** §3.2 declaraba que el **cambio forzado era el único uso del shell de acceso con sesión iniciada**, y hacía de esa excepción una decisión deliberada del marco. El intake 1.8 §4.1 precisa RN-10013: la cuenta con contraseña provisoria **se autentica y no obtiene sesión de trabajo**. El párrafo se reescribe y **la excepción desaparece**: el cambio forzado usa el shell de acceso por el mismo motivo que el establecimiento, y la frontera entre los dos armazones —tener sesión y sistema operable— vuelve a valer sin salvedades. La segunda fricción anticipada de §3.4 **no cambia**: su relato del reseteo sigue siendo correcto, y su cita a 1.6 y 1.7 es histórica. |
| 1.3 | 2026-08-09 | **Cierra la parte del hallazgo `F26-30`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0 que alcanza a este archivo, contra `PRODUCT-INTAKE` **1.10**. §3.4 decía, sobre la fricción de la contraseña olvidada, que «el alumno **entra** con una provisoria y elige la suya en `Credencial-Propia`». No afirmaba sesión y no contradecía a nadie —por eso el informe lo clasifica como residuo de vocabulario y no como contradicción—, pero «entra» es exactamente el verbo que el `PRODUCT-INTAKE` 1.8 sacó de RN-10013 por leerse como que la cuenta obtiene sesión. Pasa a decir que **se autentica, no obtiene sesión de trabajo** y elige la suya en el curso de cambio forzado. **Ningún armazón, ninguna superficie, ningún flujo y ningún recuento de este marco cambia.** Sube minor. |
| 1.4 | 2026-08-10 | **Cierra el hallazgo `C-02` (P0) del informe de auditoría `SDD/Docs/Audit/Coherencia-Corpus-r1.md` 1.0 en tres rangos vivos de este archivo que el informe no registra, contra `PRODUCT-INTAKE` 1.14.** Los tres son de la clase que el propio informe señala como la que sobrevive —celdas de tabla y cabecera— y los tres quedaron en el número de una tanda anterior. **§9**, fila «Reglas de negocio relevantes», declaraba el rango `RN-10001` a **`RN-10011`**: son **dieciséis**, `RN-10001` a `RN-10016`, contadas sobre los archivos de `GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/`; la enumeración de las que condicionan la presentación se verificó y no cambia. **§9**, fila «CU origen», citaba `RT-01` a **`RT-11`** y la **cabecera** «las **once** restricciones transversales»: son **trece**, `RT-01` a `RT-13`, contadas sobre §6 del índice maestro de la categoría 02, que las llevó a trece cuando entraron RT-12 —confinamiento de la cuenta reseteada— y RT-13 —frontera del movimiento automático—. **Cierra además el hallazgo `C-06` (P1) en una ocurrencia que el informe no registra**: **§2** declaraba «ni recuperación de contraseña (exclusiones **X-1 y X-2**)», y el intake §9 muestra la fila de X-2 tachada y rotulada «Exclusión retirada el 2026-08-09». Pasa a citar **sólo X-1**, a acotar lo excluido a la recuperación **autónoma** y a declarar que la recuperación existe y la ejerce el administrador con el reseteo de **F-26**. **Ninguna superficie, ningún flujo, ningún patrón de interacción y ningún criterio de esta categoría cambia**: ninguna superficie ofrece «olvidé mi contraseña» ni antes ni ahora. Sube minor. |
