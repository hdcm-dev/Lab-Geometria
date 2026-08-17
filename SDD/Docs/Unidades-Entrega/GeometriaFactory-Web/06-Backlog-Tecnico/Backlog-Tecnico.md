# Backlog técnico — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** Backlog-Tecnico.md
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

**Las dos secciones de cada apartado son la del portal y la del bundle del visor.** Las dos declaran las mismas secciones: la unidad de entrega es una y el visor viaja adentro.

---

## 1. Cómo se lee este backlog

### 1.1 `GeometriaFactory-Web`

Las **veintitrés** tareas técnicas viven **inline**, porque el proyecto de código está por debajo del umbral de treinta.

**Ninguna tarea inventa alcance.** Cada una nace de un componente de [`../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`](../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md) §3.1, de una ADR, de un NFR de su §8, de un riesgo de su §9, de un punto abierto de su §11 o de un artefacto ya emitido de la categoría 03. **Siete** convierten en trabajo un punto abierto: BT-10002, BT-10004, BT-10010, BT-10012, BT-10021, BT-10022 y BT-10023.

**Tres particularidades de este backlog técnico, que conviene decir antes de leerlo.**

1. **Tres tareas son puertas de conteo con umbral cero y no funcionalidades.** BT-10015 —cero peticiones del navegador y una sola salida—, y las inspecciones que acompañan a BT-10013 y BT-10016. Existen porque **este es el único proyecto de código del producto que puede violar las tres reglas de arquitectura** (`05` §1), y una prohibición que no se cuenta no se audita.
2. **Una tarea adopta una decisión que se toma en otro proyecto de código.** BT-10012 adopta el formato de intercambio que fija la categoría 05 de `GeometriaFactory-Api`; `05` §11 `PA-03` declara que **no se puede decidir de un solo lado** y que esta pieza es el consumidor. La tarea **adopta y no decide**.
3. **La verificación de este proyecto de código no es cobertura de líneas: es un guion acumulativo y una matriz de deriva.** `PRODUCT-INTAKE` §17.2.P.6 · GeometriaFactory-Web declara que no hay proyecto de pruebas propio, y `05` §8 fija como NFR **74 de 74** estados, **11 de 11** superficies, **73 de 73** componentes y **24 de 24** rutas de la línea de base, verificados por las **61** filas de la matriz de sensado de deriva. BT-10019 y BT-10020 son esas dos tareas.

**Estimación: sin fijar**, por el fundamento de [`Product-Backlog.md`](Product-Backlog.md) §4.1.

### 1.2 `GeometriaFactory-Visor`

Las **dieciocho** tareas técnicas viven **inline**, porque el proyecto de código está por debajo del umbral de treinta.

**Ninguna tarea inventa alcance.** Cada una nace de un componente de [`../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`](../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md) §3.1, de una ADR, de un NFR de su §8, de una puerta técnica del intake §15 o de un punto abierto de `05` §11. Las cuatro que cierran un punto abierto son BT-12003, BT-12009, BT-12017 y BT-12018.

**Dos particularidades de este backlog técnico, que conviene decir antes de leerlo.**

1. **Varias tareas se verifican sobre el bundle generado y no sobre el código fuente.** `05` §9 declara que la causa más probable de que aparezca una petición de red no es la comodidad del programador sino **una dependencia que la haga por dentro**, y por eso la verificación se hace sobre el artefacto que se sirve. BT-12013 y BT-12016 son de esa clase.
2. **Una parte del trabajo es decidir qué del visualizador previo no se porta.** `05` §3.3 declara qué se conserva y qué no, y el motivo de cada exclusión; ese documento es fuente de las tareas de la capa 3 y no una nota de contexto.

**Estimación: sin fijar**, por el fundamento de [`Product-Backlog.md`](Product-Backlog.md) §4.1.

## 2. Épicas técnicas y sus tareas

### 2.1 `GeometriaFactory-Web`

### 2.1 EP-T01 · Fundaciones, publicación y viabilidad

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que el front exista, se publique en el hosting con su dirección pública respondiendo, consuma el punto de salud del servicio de datos, y que las **cuatro** partes de `PT-01` queden medidas antes que cualquier otra cosa |
| Alcance | Proyecto, biblioteca de componentes anclada, página de salud, dirección del servicio de datos desde configuración, flujo de publicación y sus puertas |
| Fuente upstream | `PRODUCT-INTAKE` §15 (etapa `a`, puertas `PT-01` y `PT-04`), §17.2.P.7 · GeometriaFactory-Web a P.10; `05` §5, §8 filas de `PT-01`, §11 `PA-01` y `PA-02`; [`ADR-10001`](../05-Arquitectura-Tecnica/Adrs/ADR-10001-Render-En-El-Servidor-Con-Circuito-Interactivo.md), [`ADR-10007`](../05-Arquitectura-Tecnica/Adrs/ADR-10007-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md) |
| Etapa | `a` |
| BT contenidas | BT-10001, BT-10002, BT-10003, BT-10004, BT-10005, BT-10006 |

### 2.2 EP-T02 · Armazón, superficies y línea de base

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que los **dos** shells, el mapa de rutas, los **cuatro** guardianes, las **once** superficies y las **tres** representaciones existan sobre la línea de base visual aprobada, con pantallas de marcador de posición |
| Alcance | Armazón y encaminamiento, superficies, representaciones reutilizadas y los dos valores rotulados como asunción de la categoría 03 |
| Fuente upstream | `05` §3.1 (componentes de capa 1) y §3.4; [`ADR-10004`](../05-Arquitectura-Tecnica/Adrs/ADR-10004-Tres-Capas-De-Presentacion.md); [`../03-UX-UI-DX/Linea-Base-Visual.md`](../03-UX-UI-DX/Linea-Base-Visual.md) y [`../03-UX-UI-DX/Experiencia-De-Uso.md`](../03-UX-UI-DX/Experiencia-De-Uso.md); `05` §11 `PA-05` |
| Etapa | `b`, salvo el cuarto guardián, que se completa en la `d` porque hasta entonces no existe la marca |
| BT contenidas | BT-10007, BT-10008, BT-10009, BT-10010 |

### 2.3 EP-T03 · Salida única, sesión y traducción

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que exista **una sola** salida hacia el servicio de datos, que la credencial de sesión **nunca** llegue al navegador y que los **diecisiete** códigos vivos del contrato se traduzcan a mensaje de superficie en un único lugar |
| Alcance | Cliente tipado, formato de intercambio adoptado, sesión y estado del circuito, traductor de condiciones y la puerta de cero peticiones del navegador |
| Fuente upstream | `05` §3.1 (componentes de capa 2 y 3), §7 filas de salida, autenticación y manejo de errores, §8 filas de peticiones del navegador, de salidas y de apariciones de la credencial; [`ADR-10001`](../05-Arquitectura-Tecnica/Adrs/ADR-10001-Render-En-El-Servidor-Con-Circuito-Interactivo.md), [`ADR-10003`](../05-Arquitectura-Tecnica/Adrs/ADR-10003-Credencial-De-Sesion-En-El-Estado-Del-Circuito.md), [`ADR-10005`](../05-Arquitectura-Tecnica/Adrs/ADR-10005-Estado-Degradado-Como-Superficie.md); `05` §11 `PA-03` |
| Etapa | `c`, que es la primera con una llamada real al servicio de datos |
| BT contenidas | BT-10011, BT-10012, BT-10013, BT-10014, BT-10015 |

### 2.4 EP-T04 · Anfitrión del visor

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que el componente anfitrión —**que es la capa 1 del contrato de fachada del visor y vive en este proyecto de código**— opere el ciclo de vida de la instancia por las **seis** funciones, gobierne los dos movimientos y libere sus recursos |
| Alcance | Anfitrión, consulta de la preferencia de movimiento reducido, liberación y la medición de `PT-02` desde el lado del anfitrión |
| Fuente upstream | `05` §2.2 (las dos decisiones heredadas del visor), §3.1 componente «Anfitrión del visor», §8 filas de tráfico de circuito, de instancias no liberadas y de invocaciones al interior; [`ADR-10006`](../05-Arquitectura-Tecnica/Adrs/ADR-10006-Aislamiento-Del-Visor-Tras-Su-Fachada.md); `PRODUCT-INTAKE` §17.2.P.11 · GeometriaFactory-Web punto 5 y §17.7 P.3 |
| Etapa | `f` la previsualización previa al envío, `g` la vista de trabajo entera |
| BT contenidas | BT-10016, BT-10017, BT-10018 |

### 2.5 EP-T05 · Verificación, deriva y puntos abiertos

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que la deriva contra la línea de base aprobada se sense en cada corte, que el guion de demostración acumulativo sea puerta, y que los tres puntos abiertos que no son de esta categoría queden elevados con su plazo |
| Alcance | Matriz de sensado de deriva, guion acumulativo, umbral de tiempo de respuesta, volumen de la comisión y versionado del bundle generado |
| Fuente upstream | `05` §8 filas de estados de la línea de base y de pasos del guion; [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md); `05` §11 `PA-04`, `PA-06` y `PA-07` |
| Etapa | Acumulativa de `b` a `h`; `PA-06` después de la `a`, `PA-08` antes de comprometer la `e` |
| BT contenidas | BT-10019, BT-10020, BT-10021, BT-10022, BT-10023 |

### 2.2 `GeometriaFactory-Visor`

### 2.1 EP-T01 · Fundaciones y cadena de construcción

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que el proyecto del bundle exista, que su construcción sea reproducible y que produzca en la etapa `a` un archivo **vacío pero real** |
| Alcance | Estructura del proyecto, instalación reproducible de dependencias, empaquetado, copia al directorio de recursos estáticos del anfitrión, guion de ciclo corto y la decisión de versionar o ignorar el artefacto generado |
| Fuente upstream | `PRODUCT-INTAKE` §15 (etapa `a`), §16 y §17.2.P.8 · GeometriaFactory-Visor; `05` §5 y §11 `PA-05` |
| Momento | Etapa `a` |
| BT contenidas | BT-12001, BT-12002, BT-12003 |

### 2.2 EP-T02 · Capa 2, fachada y registro de instancias

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que exista una única puerta al interior, con las **seis** funciones planas, el registro que resuelve el identificador y los **siete** códigos de condición tomados de su fuente única |
| Alcance | Fachada plana, registro de instancias e incorporación de los códigos de condición |
| Fuente upstream | `05` §3.1 (fachada plana, registro de instancias); [`ADR-12001`](../05-Arquitectura-Tecnica/Adrs/ADR-12001-Tres-Capas-Con-Fachada-Plana.md), [`ADR-12002`](../05-Arquitectura-Tecnica/Adrs/ADR-12002-Superficie-De-Seis-Funciones-Planas.md); [`../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md) §6 |
| Momento | Antes de comprometer la etapa `g` |
| BT contenidas | BT-12004, BT-12005, BT-12006 |

### 2.3 EP-T03 · Capa 3, lectura, dibujo y movimiento

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que la escena exista, que se dibuje lo que el texto trae, que la disposición sea determinista y que los dos movimientos automáticos vivan en el bucle de dibujo sin que el anfitrión conozca el interior |
| Alcance | Lector del texto, servicio de dibujo, motor confinado a la capa 3, disposición por índice, movimientos y liberación de recursos |
| Fuente upstream | `05` §3.1 (lector del texto, servicio de dibujo, motor de dibujo), §3.3, §4 y §6; [`ADR-12004`](../05-Arquitectura-Tecnica/Adrs/ADR-12004-Motor-De-Dibujo-Empaquetado-Y-Aislado.md), [`ADR-12005`](../05-Arquitectura-Tecnica/Adrs/ADR-12005-Disposicion-Determinista-Derivada-Del-Indice.md); `05` §11 `PA-01` y `PA-02` |
| Momento | Antes de comprometer la etapa `g`, salvo BT-12011 y BT-12017, que son de la etapa `g` |
| BT contenidas | BT-12007, BT-12008, BT-12009, BT-12010, BT-12011, BT-12012, BT-12017 |

### 2.4 EP-T04 · Puertas, sample e inspección del bundle

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que las dos puertas técnicas del proyecto de código se puedan medir, que el punto de extensión tenga su demostración y que la superficie del bundle generado sea la declarada y ninguna otra |
| Alcance | `PT-02`, `PT-03`, la página integradora sin backend, la inspección del bundle generado y el umbral de fluidez |
| Fuente upstream | `PRODUCT-INTAKE` §15 (puertas), §16.1 y §18 (sample `S-1`); `05` §8 (NFR de dependencias externas y de superficie pública) y §11 `PA-03`; [`ADR-12003`](../05-Arquitectura-Tecnica/Adrs/ADR-12003-Visualizador-Puro-Sin-Red-Ni-Identidad.md), [`ADR-12006`](../05-Arquitectura-Tecnica/Adrs/ADR-12006-Bundle-Generado-Y-Versionado-Del-Punto-De-Extension.md) |
| Momento | `PT-02` y `PT-03` antes de comprometer la etapa `g`; el sample y el umbral, en la etapa `g` |
| BT contenidas | BT-12013, BT-12014, BT-12015, BT-12016, BT-12018 |

## 3. Detalle de las tareas técnicas

### 3.1 `GeometriaFactory-Web`

| BT | Título | Tipo | Épica | Etapa | Prioridad | Estimación | Fuente upstream | Dependencias | Criterios de aceptación | US que la consumen |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| BT-10001 | Crear el proyecto del front con su flujo de publicación | feature | EP-T01 | `a` | Alta | Sin fijar | `PRODUCT-INTAKE` §16 y §17.2.P.8 · GeometriaFactory-Web; `05` §5 | Ninguna | El proyecto compila dentro del artefacto de agrupación; el flujo de publicación corre de punta a punta: obtención del código, preparación de las **dos** cadenas de herramientas, empaquetado del bundle con copia al directorio de recursos estáticos, publicación, inyección de la dirección del servicio de datos desde secretos y subida | **Infraestructura compartida**: la sostiene `05` §5. Habilita a las 30 |
| BT-10002 | Anclar la versión de la biblioteca de componentes de interfaz | indagación | EP-T01 | `a` | Alta | Sin fijar | `05` §11 `PA-01`; `PRODUCT-INTAKE` §17.2.P.1 · GeometriaFactory-Web, rotulado **[A VERIFICAR]** | BT-10001 | La versión queda anclada según la regla de anclaje de versiones del producto y registrada en el momento del andamiaje; **la interfaz no usa estilos improvisados fuera del sistema visual adoptado**, que es criterio de aceptación de la etapa `b`. **Caja temporal: la etapa `a`** | **Infraestructura compartida**: condiciona las once superficies |
| BT-10003 | Construir la página de salud que consume el punto de salud del servicio de datos | feature | EP-T01 | `a` | Alta | Sin fijar | `PRODUCT-INTAKE` §15 (etapa `a`) y §17.1.P.3 · GeometriaFactory-Api; `Roadmap-Producto.md` §5.2, transición `a` → `b` | BT-10001, BT-10005 | La página de salud **muestra datos reales del servidor propio**; la llamada la hace el **servidor de esta pieza** y no el navegador; es la primera verificación de que el camino existe antes de recorrerlo con peso | **Infraestructura compartida**: es el esqueleto ambulante de esta pieza |
| BT-10004 | Medir `PT-01` en sus cuatro partes | devops | EP-T01 | `a` | Alta | Sin fijar | `PRODUCT-INTAKE` §15 y §17.2.P.10 · GeometriaFactory-Web; `05` §8, cuatro primeras filas; `05` §11 `PA-02` | BT-10001, BT-10003 | `PT-01.a`: la dirección pública responde **200**. `PT-01.b`: el transporte queda medido y documentado, **con el repliegue de mayor latencia aceptado y no motivo de rediseño**. `PT-01.c`: **20 minutos** de navegación continua sin reciclado y reconexión funcional al cortar y restablecer la red. `PT-01.d`: una llamada de salud devuelve datos reales. **Una puerta que no pasa detiene la planificación de las etapas que dependen de ella.** **Caja temporal: la etapa `a`, antes que cualquier otra cosa** | **Infraestructura compartida**: `PT-01` condiciona el modelo entero de esta pieza |
| BT-10005 | Tomar la dirección del servicio de datos de configuración, con secretos inyectados al publicar | feature | EP-T01 | `a` | Alta | Sin fijar | [`ADR-10007`](../05-Arquitectura-Tecnica/Adrs/ADR-10007-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md); `05` §7, fila de configuración y secretos; `PRODUCT-INTAKE` §17.2.P.5 · GeometriaFactory-Web | BT-10001 | La dirección viene de configuración y **nunca embebida en el código**; se inyecta al publicar desde secretos del repositorio; **la dirección real del servidor propio no se versiona**; ninguna superficie la dibuja, ni siquiera deshabilitada | **Infraestructura compartida**: sin ella no hay llamada posible |
| BT-10006 | Puerta de publicación que termina comprobando que la dirección pública responde | devops | EP-T01 | `a` | Alta | Sin fijar | `05` §5, filas de puertas bloqueantes; `05` §9, sexto riesgo; `PRODUCT-INTAKE` §17.2.P.8 · GeometriaFactory-Web | BT-10001 | Construcción **sin advertencias**; el bundle se genera **en el mismo flujo** y nunca se toma de un artefacto viejo; **el flujo no termina en la subida, termina comprobando que la dirección pública responde**. Se despliega **fuera del horario de uso**, porque la subida **no es transaccional** | **Infraestructura compartida**: una subida que deja la aplicación caída y se reporta como exitosa es peor que una falla visible |
| BT-10007 | Construir los dos shells, el mapa de rutas y los cuatro guardianes | feature | EP-T02 | `b` | Alta | Sin fijar | `05` §3.1, componente «Armazón y encaminamiento»; [`ADR-10004`](../05-Arquitectura-Tecnica/Adrs/ADR-10004-Tres-Capas-De-Presentacion.md); `05` §10.2 `RT-09` y `RT-12` | BT-10001, BT-10002 | Los **dos** shells —acceso y trabajo— y el mapa de rutas existen; los **cuatro** guardianes son aprovisionamiento resuelto, sesión, papel y cambio de contraseña pendiente; **ninguna ruta del panel es alcanzable sin sesión y un alumno con sesión no alcanza ninguna ruta de administrador**. El cuarto guardián **se completa en la etapa `d`**, porque hasta entonces no existe la marca. **Esto acota lo que se ofrece y no hace cumplir nada** | US-10005, US-10008, US-10029 |
| BT-10008 | Construir las once superficies con marcador de posición, sobre la línea de base visual | feature | EP-T02 | `b` | Alta | Sin fijar | `05` §3.1, componente «Superficies», y §3.4; [`../03-UX-UI-DX/Linea-Base-Visual.md`](../03-UX-UI-DX/Linea-Base-Visual.md) | BT-10007 | Las **once** superficies son alcanzables por su ruta, con pantallas de marcador de posición; cada una lleva su nombre canónico de 03; **ninguna superficie invoca al cliente tipado**: entre una superficie y la salida hay siempre un servicio de aplicación de front | **Infraestructura compartida**: es el mapa de navegación recorrible de la etapa `b`. Habilita a las 30 |
| BT-10009 | Construir las tres representaciones reutilizadas | feature | EP-T02 | `b` | Media | Sin fijar | `05` §3.1, componente «Representaciones reutilizadas»; [`../03-UX-UI-DX/Representacion-Fila-De-Trabajo.md`](../03-UX-UI-DX/Representacion-Fila-De-Trabajo.md), [`../03-UX-UI-DX/Representacion-Lista-De-Observaciones.md`](../03-UX-UI-DX/Representacion-Lista-De-Observaciones.md), [`../03-UX-UI-DX/Representacion-Sello-De-Version.md`](../03-UX-UI-DX/Representacion-Sello-De-Version.md) | BT-10008 | Las **tres** piezas compartidas existen: fila de trabajo con su insignia, lista de observaciones con el par declarado y derivado, y sello de versión; **todo estado se comunica por al menos dos canales y nunca sólo por color** | US-10013, US-10015, US-10019, US-10022 |
| BT-10010 | Confirmar el punto de quiebre principal y la proporción de la escena | indagación | EP-T02 | `g` | Media | Sin fijar | `05` §11 `PA-05`; los dos valores rotulados **[ASUNCIÓN]** por la categoría 03 | BT-10008, BT-10016 | Los dos valores quedan **confirmados como valores del producto o corregidos**, sobre la línea de base visual ya aprobada. **La maqueta se aprobó, de modo que quedaron ejercidos**; lo que sigue abierto es si se confirman. **Caja temporal: antes de cerrar la etapa `g`** | **Infraestructura compartida**: la decisión es del Product Owner sobre la línea de base |
| BT-10011 | Construir el cliente tipado como única salida hacia el servicio de datos | feature | EP-T03 | `c` | Alta | Sin fijar | `05` §3.1, componente «Cliente tipado»; [`ADR-10001`](../05-Arquitectura-Tecnica/Adrs/ADR-10001-Render-En-El-Servidor-Con-Circuito-Interactivo.md); `05` §10.2 `RT-01` | BT-10001, BT-10005 | La solicitud **se arma en el servidor** de esta pieza y lleva la credencial adjunta; existe **exactamente 1** salida hacia el servicio de datos y **0** bibliotecas de guion agregadas que consulten servicios por su cuenta; el cliente devuelve el tipo del contrato o su tipo de error, **sin agregar ni recortar campos** | US-10001, US-10003, US-10009, US-10011, US-10015, US-10022, US-10024 |
| BT-10012 | Adoptar el formato de intercambio que fija la categoría 05 de `GeometriaFactory-Api` | indagación | EP-T03 | `a` | Alta | Sin fijar | `05` §11 `PA-03`; [`Api ADR-00002`](../../GeometriaFactory-Api/05-Arquitectura-Tecnica/Adrs/ADR-00002-Formato-De-Intercambio-Y-Su-Configuracion.md), que declara que la configuración **obliga a los dos extremos** | BT-10011 | El cliente de esta pieza usa **la misma** configuración que el productor: nombres de campo literales, valores de conjunto cerrado por su **nombre** y no por su posición, campos nulos emitidos, números sin cultura y lectura estricta. **Exactamente 1** configuración de intercambio declarada en el producto. **Esta tarea adopta y no decide**: la decisión pertenece al productor, y esta pieza declaró que la adopta. La coincidencia **se verifica ejerciendo el servicio real** y no comparando dos archivos. **Caja temporal: la etapa `a`** | **Infraestructura compartida**: un desajuste rompe en producción y **no lo detecta la compilación** |
| BT-10013 | Construir el traductor de las diecisiete condiciones vivas a mensaje de superficie | feature | EP-T03 | `c` | Alta | Sin fijar | `05` §3.1, componente «Traductor de condiciones»; [`ADR-10005`](../05-Arquitectura-Tecnica/Adrs/ADR-10005-Estado-Degradado-Como-Superficie.md); `05` §8, fila de mensajes que exponen; `05` §10.2 `RT-03` y `RT-07` | BT-10011 | Cada uno de los **diecisiete** códigos vivos del conjunto cerrado tiene su mensaje con **qué pasó, por qué y qué hacer**; **0** mensajes llevan dirección de servicio, ruta de datos o traza, verificado sobre los diecisiete **y** sobre el camino de ausencia de respuesta; **nunca una excepción sin manejar y nunca una pantalla rota**; el traductor **no habla con el servicio de datos**, recibe el tipo de error ya traído | US-10002, US-10004, US-10014, US-10023, US-10026, US-10027 |
| BT-10014 | Custodiar la credencial de sesión en el estado del circuito | feature | EP-T03 | `c` | Alta | Sin fijar | `05` §3.1, componente «Sesión y estado del circuito»; [`ADR-10003`](../05-Arquitectura-Tecnica/Adrs/ADR-10003-Credencial-De-Sesion-En-El-Estado-Del-Circuito.md); `05` §8, fila de apariciones de la credencial; `05` §10.2 `RT-02` | BT-10011 | La credencial vive **en el estado del circuito, del lado del servidor**; el navegador conserva **sólo** una marca de sesión que **no la transporta y no es legible por guion**; **exactamente 0** apariciones de la credencial en el navegador, verificable con las herramientas de desarrollo en la etapa `c` | US-10003, US-10005, US-10006, US-10028, US-10029 |
| BT-10015 | Puerta de cero peticiones del navegador y una sola salida | devops | EP-T03 | `c` | Alta | Sin fijar | `05` §8, filas de peticiones del navegador y de salidas; `05` §9, primer riesgo; `PRODUCT-INTAKE` §14 (`RA-01`) | BT-10011, BT-10016 | **Exactamente 0** peticiones del navegador hacia el servicio de datos, contadas en la pestaña de red durante un recorrido completo **incluida la interacción con la escena y con los dos movimientos automáticos prendidos**, que es el peor caso declarado; **exactamente 1** salida y **0** bibliotecas de guion que consulten por su cuenta. **Se mide en cada etapa y no sólo en la que la introdujo** | **Infraestructura compartida**: `RA-01` es la regla que sostiene la topología entera |
| BT-10016 | Construir el anfitrión del visor con las seis funciones y el ciclo de vida de la instancia | feature | EP-T04 | `f` | Alta | Sin fijar | `05` §2.2 (el anfitrión **es la capa 1 del contrato de fachada y vive acá**), §3.1 componente «Anfitrión del visor»; [`ADR-10006`](../05-Arquitectura-Tecnica/Adrs/ADR-10006-Aislamiento-Del-Visor-Tras-Su-Fachada.md); [`Definicion-Contrato-De-Fachada.md`](../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md) | BT-10008 | El bundle se invoca **exclusivamente** por sus **6** funciones; **0** invocaciones al interior y **0** accesos al elemento de dibujo por fuera del anfitrión; **0** tráfico de circuito durante la interacción con la escena, y el texto del trabajo viaja del servidor al navegador **una sola vez por trabajo**; el anfitrión **decide cuándo ajustar**: la fachada no observa tamaños por su cuenta | US-10012, US-10018, US-10020, US-10021 |
| BT-10017 | Leer la preferencia de movimiento reducido y traducirla a dos valores de verdad | feature | EP-T04 | `g` | Alta | Sin fijar | `05` §2.2 y §10.2 `RT-13`; `PRODUCT-INTAKE` §4 (`F-25`) y §17.7 P.3; [`Visor ADR-12003`](../05-Arquitectura-Tecnica/Adrs/ADR-12003-Visualizador-Puro-Sin-Red-Ni-Identidad.md) | BT-10016 | **Es esta pieza la que consulta el entorno del navegador** y manda **dos valores de verdad** por la fachada, uno por la órbita de la cámara y otro por el giro de las piezas; **el bundle no consulta nada**; los dos movimientos se gobiernan **por separado** y prenderlos o apagarlos **no reconstruye la instancia** ni pierde la selección vigente. **La ignorancia del bundle es una obligación de esta pieza, no una comodidad** | US-10018, US-10021 |
| BT-10018 | Verificar la liberación de la instancia con diez recorridos de ida y vuelta | devops | EP-T04 | `g` | Alta | Sin fijar | `05` §8, fila de instancias no liberadas; `05` §9, quinto riesgo; `05` §10.2 `RT-05`; `PRODUCT-INTAKE` §17.2.P.11 · GeometriaFactory-Web punto 5 | BT-10016, BT-10017 | La liberación se invoca **al descartar el componente que aloja la instancia** y **no es opcional**; exactamente **0** instancias no liberadas tras **10** recorridos de ida y vuelta entre trabajos, **sin degradación**, medidos **con los dos movimientos prendidos**, que es su peor caso; es la parte de `PT-02` que se mide desde el lado del anfitrión | US-10018, US-10021 |
| BT-10019 | Ejecutar las 61 filas de la matriz de sensado de deriva | docs | EP-T05 | `b` | Alta | Sin fijar | `05` §8, fila de estados de la línea de base; [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md) | BT-10008, BT-10009 | **74 de 74** estados, **11 de 11** superficies, **73 de 73** componentes y **24 de 24** rutas de la línea de base visual aprobada quedan demostrados; las **61** filas de la matriz se verifican **al cierre de cada corte** y no una sola vez | **Infraestructura compartida**: es la verificación de que la interfaz construida sigue siendo la maqueta aprobada |
| BT-10020 | Fijar el guion de demostración acumulativo como puerta del punto de control | devops | EP-T05 | `b` | Alta | Sin fijar | `05` §8, fila de pasos del guion, rotulada **[ASUNCIÓN]** en cuanto a expresarla como puerta; `PRODUCT-INTAKE` §17.2.P.6 · GeometriaFactory-Web; `Roadmap-Producto.md` §5.1 | BT-10008 | **100 %** de los pasos del guion de la etapa **y de todas las anteriores** se ejecutan y pasan antes del punto de control, en el navegador del equipo anfitrión; la regla acumulativa es de la fuente y **el hecho de expresarla como puerta es la parte rotulada como asunción**; este proyecto de código **no tiene proyecto de pruebas propio** y ésta es su verificación | **Infraestructura compartida**: reemplaza a la cobertura de líneas, que esta pieza no tiene |
| BT-10021 | Elevar el umbral numérico de tiempo de respuesta | indagación | EP-T05 | `c` | Media | Sin fijar | `05` §8, cierre, y §11 `PA-04` | BT-10004, BT-10013 | O bien el Product Owner fija un umbral, o bien 08 fija su guion de medición después de `PT-01`. **Ninguna de las dos salidas es inventar un número acá**: `05` §8 se niega explícitamente, porque las **tolerancias percibidas de 400 ms** de la categoría 03 dicen a partir de cuándo se muestra un indicador y **no cuánto puede tardar el servidor**. **Caja temporal: después de la etapa `a`** | **Infraestructura compartida**: condiciona el guion de medición de 08 |
| BT-10022 | Elevar el volumen de la comisión y la ausencia de paginación | indagación | EP-T05 | `e` | Media | Sin fijar | `05` §11 `PA-06`, rotulado **[A VERIFICAR]** | BT-10008 | Queda declarado si el volumen es de decenas o de cientos. El diseño de los **dos** listados supone decenas y por eso **no incorpora paginación**; si resultara mucho mayor, la superficie afectada es `Listado-De-La-Comision` y el cambio es acotado. **Caja temporal: antes de comprometer la etapa `e`** | **Infraestructura compartida**: la decisión es del Product Owner |
| BT-10023 | Acompañar la decisión de versionar o ignorar el bundle generado | indagación | EP-T05 | `g` | Media | Sin fijar | `05` §11 `PA-07`; [`Visor Arquitectura-Unidad-Entrega.md`](../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md) §11 | BT-10001 | Queda decidido y registrado. **La decisión no es de este proyecto de código**: la Fase C de `GeometriaFactory-Visor` la derivó a 09, y alcanza a esta pieza porque el bundle vive en su directorio de recursos estáticos y **nunca se edita a mano**. **Caja temporal: al emitirse 09** | **Infraestructura compartida**: la titularidad es de `09-Devops` |

**Quince tareas se justifican como infraestructura compartida** —BT-10001, BT-10002, BT-10003, BT-10004, BT-10005, BT-10006, BT-10008, BT-10010, BT-10012, BT-10015, BT-10019, BT-10020, BT-10021, BT-10022 y BT-10023— y las **ocho** restantes —BT-10007, BT-10009, BT-10011, BT-10013, BT-10014, BT-10016, BT-10017 y BT-10018— declaran al menos una historia consumidora. **Quince más ocho son veintitrés**, y ninguna queda sin una cosa ni la otra.

La proporción es alta y tiene una causa declarable: en esta pieza **el armazón, la salida única, la publicación y las puertas de conteo no pertenecen a ninguna historia en particular** porque las sostienen todas. Es la contracara de que las once superficies compartan dos shells, un cliente tipado y un traductor.

### 3.2 `GeometriaFactory-Visor`

| BT | Título | Tipo | Épica | Momento | Prioridad | Estimación | Fuente upstream | Dependencias | Criterios de aceptación | US que la consumen |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| BT-12001 | Crear el proyecto del bundle con su cadena de construcción reproducible | feature | EP-T01 | `a` | Alta | Sin fijar | `PRODUCT-INTAKE` §16 y §17.2.P.8 · GeometriaFactory-Visor; `05` §5 | Ninguna | Las etapas de instalación reproducible de dependencias, empaquetado y copia al directorio de recursos estáticos del anfitrión corren de punta a punta; el bundle se genera **sin errores**; en la etapa `a` el archivo es **vacío pero real** | **Infraestructura compartida**: la sostiene `05` §5. Habilita a las 14 |
| BT-12002 | Guion de construcción propio del bundle, para el ciclo corto de trabajo | devops | EP-T01 | `a` | Media | Sin fijar | `05` §5, fila de ciclo corto de trabajo | BT-12001 | Un guion genera **sólo** el bundle, sin encadenar la construcción del resto del producto; el guion general sigue encadenando los dos | **Infraestructura compartida**: es lo que hace barato iterar sobre la capa 3 |
| BT-12003 | Decidir si el bundle generado se versiona en el repositorio o se ignora | indagación | EP-T01 | `a` | Media | Sin fijar | `05` §11 `PA-05`; `PRODUCT-INTAKE` §17.2.P.7 · GeometriaFactory-Visor | BT-12001 | Queda decidido y registrado: si se versiona, se versiona **como salida reproducible**; si se ignora, el guion de construcción lo genera antes de publicar. En los dos casos **el artefacto nunca se edita a mano**. **Caja temporal: la etapa `a`** | **Infraestructura compartida**: la decisión es de 09 y este backlog la eleva con su plazo |
| BT-12004 | Construir la fachada plana con las seis funciones | feature | EP-T02 | Antes de `g` | Alta | Sin fijar | `05` §3.1, componente «Fachada plana»; [`ADR-12002`](../05-Arquitectura-Tecnica/Adrs/ADR-12002-Superficie-De-Seis-Funciones-Planas.md) | BT-12001 | Las **seis** funciones existen con los nombres que `PRODUCT-INTAKE` §17.2.P.3 · GeometriaFactory-Visor fija; la capa 2 **no contiene lógica de dibujo**; toda condición se informa por su código y ninguna operación deja la instancia en estado indeterminado (garantía `G-7`) | US-12001, US-12002, US-12003, US-12007, US-12009, US-12010, US-12012 |
| BT-12005 | Construir el registro de instancias con su invalidación | feature | EP-T02 | Antes de `g` | Alta | Sin fijar | `05` §3.1, componente «Registro de instancias» | BT-12004 | Cada identificador resuelve a su instancia viva; al liberarla el identificador **queda invalidado** y toda invocación posterior informa `INSTANCIA_DESCONOCIDA`; dos instancias vivas no comparten escena, ni selección, ni disposición (garantía `G-4`) | US-12001, US-12009, US-12010, US-12011 |
| BT-12006 | Incorporar los siete códigos de condición desde su fuente única | feature | EP-T02 | Antes de `g` | Alta | Sin fijar | [`../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md) §6; `05` §7 y §9, sexto riesgo | BT-12004 | Los códigos son exactamente **siete**; **ninguno se acuña aguas abajo**; un curso nuevo se agrega como fila de curso y **no** como código; el catálogo de 03 puede crecer sin que crezca el conjunto de códigos, y esa distinción queda escrita | US-12003, US-12005, US-12006, US-12009, US-12010, US-12011 |
| BT-12007 | Construir el lector del texto con las variantes de clave del emisor | feature | EP-T03 | Antes de `g` | Alta | Sin fijar | `05` §3.1, componente «Lector del texto»; `PRODUCT-INTAKE` §17.2.P.11 · GeometriaFactory-Visor punto 4 | BT-12001 | Obtiene piezas, componentes y dimensiones tolerando las variantes de clave del emisor real; lo que produce `DIMENSION_NO_LEGIBLE` es la **ausencia** de la clave o del componente, **nunca el valor que trae**; el cero es una dimensión legible | US-12004, US-12005, US-12006, US-12007 |
| BT-12008 | Construir el servicio de dibujo | feature | EP-T03 | Antes de `g` | Alta | Sin fijar | `05` §3.1, componente «Servicio de dibujo»; [`Flujo-Ejecucion.md`](../05-Arquitectura-Tecnica/Flujo-Ejecucion.md) | BT-12005, BT-12007, BT-12009 | Escena, mallas, disposición, selección, encuadre y bucle de dibujo funcionan; la capa 3 **no conoce al anfitrión**; se dibujan los **seis** tipos dibujables, tres volumétricos y tres planos | US-12001, US-12004, US-12006, US-12010 |
| BT-12009 | Anclar la versión del motor de dibujo y confinarlo a la capa 3 | indagación | EP-T03 | Antes de `g` | Alta | Sin fijar | [`ADR-12004`](../05-Arquitectura-Tecnica/Adrs/ADR-12004-Motor-De-Dibujo-Empaquetado-Y-Aislado.md); `05` §11 `PA-01`; `PRODUCT-INTAKE` §17.2.P.1 · GeometriaFactory-Visor | BT-12001 | La versión queda anclada y registrada según la regla de anclaje del producto; si es posterior a la del visualizador previo, **se documenta el cambio de interfaz que exija**; el motor **nunca se expone al anfitrión**. **Caja temporal: antes de comprometer la etapa `g`** | US-12004 |
| BT-12010 | Derivar la disposición de cada pieza de su índice | feature | EP-T03 | Antes de `g` | Alta | Sin fijar | [`ADR-12005`](../05-Arquitectura-Tecnica/Adrs/ADR-12005-Disposicion-Determinista-Derivada-Del-Indice.md); `05` §3.3, última fila de lo que no se porta | BT-12008 | Dos procesados del mismo texto producen la **misma posición** de cada pieza; **el ordenamiento aleatorio del visualizador previo se reemplaza** y no queda ningún rastro suyo; la comparación es de posición y no de orientación | US-12008 |
| BT-12011 | Construir el gobierno de los dos movimientos automáticos en el bucle de dibujo | feature | EP-T03 | `g` | Alta | Sin fijar | `PRODUCT-INTAKE` §4 (`F-25`) y §17.2.P.3 · GeometriaFactory-Visor (sexta función); `05` §4 y §6 | BT-12004, BT-12008, BT-12010 | Los dos movimientos se prenden y se apagan **por separado** sobre una instancia viva, sin reconstruirla y sin perder la selección; se detienen mientras la persona arrastra y mientras la superficie no está visible, **sin cambiar el estado gobernado**; el estado de los movimientos **sobrevive a la carga de otro texto** | US-12002, US-12012, US-12013 |
| BT-12012 | Liberar recursos y cortar el bucle al destruir la instancia | feature | EP-T03 | Antes de `g` | Alta | Sin fijar | `05` §4, última viñeta; `05` §9, tercer riesgo; [`ADR-12001`](../05-Arquitectura-Tecnica/Adrs/ADR-12001-Tres-Capas-Con-Fachada-Plana.md) | BT-12005, BT-12008 | La destrucción libera los recursos gráficos y **corta el bucle**; **un bucle que sobreviviera a la destrucción es la forma de degradación que hay que descartar**, y se mide con los dos movimientos prendidos | US-12011 |
| BT-12013 | Medir la puerta `PT-03` sobre el bundle generado | devops | EP-T04 | Antes de `g` | Alta | Sin fijar | `PRODUCT-INTAKE` §15 y §17.2.P.8 · GeometriaFactory-Visor; `05` §8, fila de dependencias externas; [`ADR-12004`](../05-Arquitectura-Tecnica/Adrs/ADR-12004-Motor-De-Dibujo-Empaquetado-Y-Aislado.md) | BT-12001, BT-12008, BT-12009 | El motor de dibujo queda **dentro** del bundle; la página funciona **sin acceso a redes de distribución externas**; exactamente **0** dependencias traídas de una red externa en tiempo de ejecución. **Una puerta que no pasa detiene la planificación de la etapa `g`** y no se arrastra como deuda | **Infraestructura compartida**: la puerta condiciona la etapa entera |
| BT-12014 | Medir la puerta `PT-02` sobre una página del anfitrión | devops | EP-T04 | Antes de `g` | Alta | Sin fijar | `PRODUCT-INTAKE` §15 y §17.2.P.8 · GeometriaFactory-Visor; `05` §5, fila de puertas bloqueantes | BT-12004, BT-12005, BT-12008, BT-12012 | El bundle carga en una página del anfitrión; la creación de instancia arma la escena; la carga del texto dibuja las **tres** figuras de `E-1` **incluido el ortoedro**; **diez** recorridos de ida y vuelta no degradan; el árbol y la escena **se sincronizan por índice**. Los recorridos se miden **con los dos movimientos prendidos** | US-12001, US-12004, US-12009, US-12011 |
| BT-12015 | Construir la página integradora sin backend, que es el sample `S-1` | feature | EP-T04 | `g` | Alta | Sin fijar | `PRODUCT-INTAKE` §16.1 y §18; [`ADR-12006`](../05-Arquitectura-Tecnica/Adrs/ADR-12006-Bundle-Generado-Y-Versionado-Del-Punto-De-Extension.md); [`Extensibilidad.md`](../05-Arquitectura-Tecnica/Extensibilidad.md) | BT-12004 a BT-12012 | Un archivo carga el bundle y un texto **pegado a mano** y dibuja, con **0** servicios del backend disponibles; recorre las **seis** funciones; es el material con el que se verifican las **seis** propiedades transversales juntas | US-12014 |
| BT-12016 | Inspeccionar la superficie del bundle generado | devops | EP-T04 | Antes de `g` | Alta | Sin fijar | `05` §8, filas de cero red y de superficie pública del bundle; `05` §9, primer riesgo | BT-12001, BT-12004 | Exactamente **6** funciones expuestas, bajo **1** nombre propio en el objeto global del navegador y **0** identificadores globales sueltos; **0** ocurrencias de las tres formas de petición de red, **en el código fuente y en el bundle generado**; **0** claves escritas en el almacenamiento del navegador | US-12001, US-12014 |
| BT-12017 | Fijar los nombres internos de funciones, de clases y de campos | indagación | EP-T03 | `g` | Media | Sin fijar | `05` §11 `PA-02` | BT-12004, BT-12008 | Los nombres internos quedan decididos y registrados. **Los nombres de las seis funciones de la fachada no entran en esta tarea**: los fija `PRODUCT-INTAKE` §17.2.P.3 · GeometriaFactory-Visor y no están abiertos. **Caja temporal: la etapa `g`** | **Infraestructura compartida**: ninguna historia la consume por separado |
| BT-12018 | Resolver el umbral numérico de fluidez, o dejarlo declaradamente cualitativo | indagación | EP-T04 | `g` | Media | Sin fijar | `05` §8, cierre; `05` §11 `PA-03` | BT-12011, BT-12014 | O bien el Product Owner fija un umbral, o bien 08 fija su guion de medición cualitativo junto con `PT-02`. **Ninguna de las dos salidas es inventar un número acá**: `05` §8 se niega explícitamente a hacerlo porque se propagaría a 08 como si fuera del producto. **Caja temporal: antes de cerrar la etapa `g`** | **Infraestructura compartida**: condiciona el guion de medición de 08 |

**Siete tareas se justifican como infraestructura compartida** —BT-12001, BT-12002, BT-12003, BT-12013, BT-12017, BT-12018 y, por su naturaleza de puerta, también BT-12016, que además declara dos historias consumidoras— y las once restantes declaran al menos una historia que las consume.

## 4. Trazabilidad BT ↔ US ↔ CU

### 4.1 `GeometriaFactory-Web`

Las veintitrés filas están, una por tarea técnica, sin agrupar. Los casos de uso son los de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3 y las superficies las de `05` §3.4.

| BT | US que la consumen | CU upstream | Superficies que toca | Fuente de arquitectura |
| --- | --- | --- | --- | --- |
| BT-10001 | Infraestructura compartida (habilita a las 30) | CU-10001 a CU-10010 | Las once | `05` §5 |
| BT-10002 | Infraestructura compartida, con efecto sobre las once superficies | CU-10001 a CU-10010 | Las once | `05` §11 `PA-01` |
| BT-10003 | Infraestructura compartida | — (esqueleto ambulante) | Ninguna de las once: es la página de salud de la etapa `a` | `PRODUCT-INTAKE` §15 |
| BT-10004 | Infraestructura compartida | — (puerta `PT-01`) | Ninguna | `05` §8, `PT-01.a` a `PT-01.d` |
| BT-10005 | Infraestructura compartida | CU-10001 a CU-10009 | Ninguna: ninguna superficie la dibuja | ADR-10007 |
| BT-10006 | Infraestructura compartida | — (puerta de publicación) | Ninguna | `05` §5 |
| BT-10007 | US-10005, US-10008, US-10029 | CU-10002, CU-10003, CU-10004 | Los dos shells y las once | ADR-10004, `RT-09`, `RT-12` |
| BT-10008 | Infraestructura compartida (habilita a las 30) | CU-10001 a CU-10010 | Las once | `05` §3.4 |
| BT-10009 | US-10013, US-10015, US-10019, US-10022 | CU-10005, CU-10006, CU-10007, CU-10008 | `Panel-De-Trabajos-Del-Alumno`, `Envio-De-Trabajo`, `Vista-De-Trabajo`, `Listado-De-La-Comision` | `05` §3.1, representaciones |
| BT-10010 | Infraestructura compartida | — (línea de base visual) | Las once, y `Vista-De-Trabajo` en la proporción de la escena | `05` §11 `PA-05` |
| BT-10011 | US-10001, US-10003, US-10009, US-10011, US-10015, US-10022, US-10024 | CU-10001 a CU-10009 | Todas menos `Estado-Degradado-Y-Reconexion` | ADR-10001, `RT-01` |
| BT-10012 | Infraestructura compartida | CU-10001 a CU-10009 | Ninguna: es configuración de la salida | `05` §11 `PA-03`, `Api ADR-10002` |
| BT-10013 | US-10002, US-10004, US-10014, US-10023, US-10026, US-10027 | Los diez | Las once, y de manera decisiva `Estado-Degradado-Y-Reconexion` | ADR-10005, `RT-03`, `RT-07` |
| BT-10014 | US-10003, US-10005, US-10006, US-10028, US-10029 | CU-10002, CU-10003, CU-10004 | `Ingreso`, `Credencial-Propia`, y los dos shells | ADR-10003, `RT-02` |
| BT-10015 | Infraestructura compartida | — (puerta de `RA-01`) | Las once | `05` §8, ADR-10001 |
| BT-10016 | US-10012, US-10018, US-10020, US-10021 | CU-10005, CU-10007 | `Envio-De-Trabajo`, `Vista-De-Trabajo` | ADR-10006, `RT-04`, `RT-10` |
| BT-10017 | US-10018, US-10021 | CU-10007 | `Vista-De-Trabajo` | ADR-10006, `RT-13` |
| BT-10018 | US-10018, US-10021 | CU-10005, CU-10007 | `Envio-De-Trabajo`, `Vista-De-Trabajo` | ADR-10006, `RT-05` |
| BT-10019 | Infraestructura compartida | CU-10001 a CU-10010 | Las once | `05` §8, línea de base |
| BT-10020 | Infraestructura compartida | CU-10001 a CU-10010 | Las once | `05` §8, guion acumulativo |
| BT-10021 | Infraestructura compartida | — (guion de medición) | Las once | `05` §11 `PA-04` |
| BT-10022 | Infraestructura compartida | CU-10008 | `Listado-De-La-Comision` | `05` §11 `PA-06` |
| BT-10023 | Infraestructura compartida | — (artefacto generado) | Ninguna | `05` §11 `PA-07` |

**Cobertura inversa: los diez casos de uso tienen al menos una tarea técnica que los realiza.** CU-10001 en BT-10008, BT-10011 y BT-10013; CU-10002 en BT-10007, BT-10011, BT-10013 y BT-10014; CU-10003 en BT-10007, BT-10013 y BT-10014; CU-10004 en BT-10007, BT-10011, BT-10013 y BT-10014; CU-10005 en BT-10009, BT-10011, BT-10013, BT-10016 y BT-10018; CU-10006 en BT-10009, BT-10011 y BT-10013; CU-10007 en BT-10009, BT-10013, BT-10016, BT-10017 y BT-10018; CU-10008 en BT-10009, BT-10011, BT-10013 y BT-10022; CU-10009 en BT-10011 y BT-10013; CU-10010 en BT-10013, BT-10019 y BT-10020. **CU-10010 no consume el cliente tipado**, y es correcto: su superficie existe precisamente para cuando el cliente no obtiene respuesta.

**Cobertura de las once superficies.** Las once quedan alcanzadas por BT-10008, BT-10019 y BT-10020; **dos de ellas** —`Envio-De-Trabajo` y `Vista-De-Trabajo`— son además las únicas que pasan por el anfitrión del visor, en BT-10016, BT-10017 y BT-10018. Que sólo dos superficies de once toquen el bundle es lo que hace barato sostener el aislamiento del visor (`05` §3.4).

**Cobertura de los ocho componentes de `05` §3.1.** Armazón y encaminamiento en BT-10007; Superficies en BT-10008; Representaciones reutilizadas en BT-10009; Servicios de aplicación de front en BT-10011 y BT-10013, que son sus dos salidas; Sesión y estado del circuito en BT-10014; Cliente tipado en BT-10011 y BT-10012; Traductor de condiciones en BT-10013; Anfitrión del visor en BT-10016, BT-10017 y BT-10018. **Los ocho tienen tarea técnica.**

### 4.2 `GeometriaFactory-Visor`

Las dieciocho filas están, una por tarea técnica, sin agrupar. Los casos de uso son los de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3.

| BT | US que la consumen | CU upstream | Fuente de arquitectura |
| --- | --- | --- | --- |
| BT-12001 | Infraestructura compartida (habilita a las 14) | CU-12001 a CU-12007 | `05` §5 |
| BT-12002 | Infraestructura compartida | — (ciclo de trabajo) | `05` §5 |
| BT-12003 | Infraestructura compartida | — (artefacto generado) | `05` §11 `PA-05` |
| BT-12004 | US-12001, US-12002, US-12003, US-12007, US-12009, US-12010, US-12012 | CU-12001, CU-12002, CU-12003, CU-12004, CU-12007 | ADR-12002, `05` §3.1 |
| BT-12005 | US-12001, US-12009, US-12010, US-12011 | CU-12001, CU-12003, CU-12004, CU-12005 | `05` §3.1, registro de instancias |
| BT-12006 | US-12003, US-12005, US-12006, US-12009, US-12010, US-12011 | CU-12001 a CU-12005, CU-12007 | Definicion-Contrato-De-Fachada §6 |
| BT-12007 | US-12004, US-12005, US-12006, US-12007 | CU-12002 | `05` §3.1, lector del texto |
| BT-12008 | US-12001, US-12004, US-12006, US-12010 | CU-12001, CU-12002, CU-12004 | `05` §3.1, servicio de dibujo |
| BT-12009 | US-12004 | CU-12002 | ADR-12004, `05` §11 `PA-01` |
| BT-12010 | US-12008 | CU-12002 | ADR-12005 |
| BT-12011 | US-12002, US-12012, US-12013 | CU-12001, CU-12007 | `PRODUCT-INTAKE` §17.2.P.3 · GeometriaFactory-Visor, `05` §4 |
| BT-12012 | US-12011 | CU-12005 | ADR-12001, `05` §4 |
| BT-12013 | Infraestructura compartida | — (puerta `PT-03`) | `05` §8, ADR-12004 |
| BT-12014 | US-12001, US-12004, US-12009, US-12011 | CU-12001, CU-12002, CU-12003, CU-12005 | `05` §5, puertas bloqueantes |
| BT-12015 | US-12014 | CU-12006 | ADR-12006, `PRODUCT-INTAKE` §16.1 y §18 |
| BT-12016 | US-12001, US-12014 | CU-12001, CU-12006 | `05` §8, ADR-12002, ADR-12003 |
| BT-12017 | Infraestructura compartida | CU-12001 a CU-12007 | `05` §11 `PA-02` |
| BT-12018 | Infraestructura compartida | — (guion de medición) | `05` §11 `PA-03` |

**Cobertura inversa: los siete casos de uso tienen al menos una tarea técnica que los realiza.** CU-12001 en BT-12004, BT-12005, BT-12006, BT-12008, BT-12011, BT-12014 y BT-12016; CU-12002 en BT-12004, BT-12006, BT-12007, BT-12008, BT-12009, BT-12010 y BT-12014; CU-12003 en BT-12004, BT-12005, BT-12006 y BT-12014; CU-12004 en BT-12004, BT-12005, BT-12006 y BT-12008; CU-12005 en BT-12005, BT-12006, BT-12012 y BT-12014; CU-12006 en BT-12015 y BT-12016; CU-12007 en BT-12004, BT-12006 y BT-12011. **La enumeración es exhaustiva**: incluye las filas de alcance general —las que declaran un rango de casos de uso— junto con las específicas, y se reconstruyó desde la matriz fila por fila en lugar de escribirse a mano.

**Cobertura de las siete garantías de `05` §10.2.** `G-1` en BT-12013 y BT-12016; `G-2` en BT-12016; `G-3` en BT-12004 y BT-12011; `G-4` en BT-12005 y BT-12008; `G-5` en BT-12006 y BT-12007; `G-6` en BT-12010; `G-7` en BT-12004 y BT-12012. **Las siete tienen tarea técnica.** Perder cualquiera de ellas es un cambio mayor aunque las seis firmas no se toquen (`05` §10.2), y por eso ninguna queda sin trabajo que la sostenga.

## 5. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 2.0 | 2026-08-16 | **Consolidación de la fusión.** Pasa a ser el documento de la **unidad de entrega**, absorbiendo el de `GeometriaFactory-Visor`, con su texto transpuesto sin reescritura. Entra §0. Sube **major**. |
