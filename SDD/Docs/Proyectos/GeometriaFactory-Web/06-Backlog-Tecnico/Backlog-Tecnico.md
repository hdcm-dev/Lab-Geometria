# Backlog técnico — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** Backlog-Tecnico.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Scrum Master (AG-06)
**Tipo de proyecto de código (D8):** `web-monolith`
**Trazabilidad upstream:** [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §2.2 (lo que hereda del nivel 0), §3.1 (los **ocho** componentes en tres capas), §3.4 (las **once** superficies), §5 (etapas del pipeline y puertas), §6 (vista de datos), §7 (cross-cutting), §8 (los **catorce** NFR), §9 (los **siete** riesgos), §10.2 (las **trece** restricciones transversales) y §11 (los **siete** puntos abiertos); las **siete** ADR de [`../05-Arquitectura-Tecnica/Adrs/`](../05-Arquitectura-Tecnica/Adrs/); [`../03-UX-UI-DX/Linea-Base-Visual.md`](../03-UX-UI-DX/Linea-Base-Visual.md), [`../03-UX-UI-DX/Experiencia-De-Uso.md`](../03-UX-UI-DX/Experiencia-De-Uso.md), [`../03-UX-UI-DX/Contrato-Datos-Maqueta.md`](../03-UX-UI-DX/Contrato-Datos-Maqueta.md) y las **tres** representaciones reutilizadas; [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md); [`../../GeometriaFactory-Visor/02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../../GeometriaFactory-Visor/02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.18** §14, §15, §16.1, §17.6 y §17.7 P.3
**Trazabilidad downstream:** [`Product-Backlog.md`](Product-Backlog.md), `07-Plan-Sprint`, `08-Calidad-Y-Pruebas` y `09-Devops` de GeometriaFactory-Web

---

## Tabla de contenido

- [1. Cómo se lee este backlog](#1-cómo-se-lee-este-backlog)
- [2. Épicas técnicas y sus tareas](#2-épicas-técnicas-y-sus-tareas)
  - [2.1 EP-T01 · Fundaciones, publicación y viabilidad](#21-ep-t01--fundaciones-publicación-y-viabilidad)
  - [2.2 EP-T02 · Armazón, superficies y línea de base](#22-ep-t02--armazón-superficies-y-línea-de-base)
  - [2.3 EP-T03 · Salida única, sesión y traducción](#23-ep-t03--salida-única-sesión-y-traducción)
  - [2.4 EP-T04 · Anfitrión del visor](#24-ep-t04--anfitrión-del-visor)
  - [2.5 EP-T05 · Verificación, deriva y puntos abiertos](#25-ep-t05--verificación-deriva-y-puntos-abiertos)
- [3. Detalle de las tareas técnicas](#3-detalle-de-las-tareas-técnicas)
- [4. Trazabilidad BT ↔ US ↔ CU](#4-trazabilidad-bt--us--cu)
- [5. Control de cambios](#5-control-de-cambios)

---

## 1. Cómo se lee este backlog

Las **veintitrés** tareas técnicas viven **inline**, porque el proyecto de código está por debajo del umbral de treinta.

**Ninguna tarea inventa alcance.** Cada una nace de un componente de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §3.1, de una ADR, de un NFR de su §8, de un riesgo de su §9, de un punto abierto de su §11 o de un artefacto ya emitido de la categoría 03. **Siete** convierten en trabajo un punto abierto: BT-02, BT-04, BT-10, BT-12, BT-21, BT-22 y BT-23.

**Tres particularidades de este backlog técnico, que conviene decir antes de leerlo.**

1. **Tres tareas son puertas de conteo con umbral cero y no funcionalidades.** BT-15 —cero peticiones del navegador y una sola salida—, y las inspecciones que acompañan a BT-13 y BT-16. Existen porque **este es el único proyecto de código del producto que puede violar las tres reglas de arquitectura** (`05` §1), y una prohibición que no se cuenta no se audita.
2. **Una tarea adopta una decisión que se toma en otro proyecto de código.** BT-12 adopta el formato de intercambio que fija la categoría 05 de `GeometriaFactory-Api`; `05` §11 `PA-03` declara que **no se puede decidir de un solo lado** y que esta pieza es el consumidor. La tarea **adopta y no decide**.
3. **La verificación de este proyecto de código no es cobertura de líneas: es un guion acumulativo y una matriz de deriva.** `PRODUCT-INTAKE` §17.6.P.6 declara que no hay proyecto de pruebas propio, y `05` §8 fija como NFR **74 de 74** estados, **11 de 11** superficies, **73 de 73** componentes y **24 de 24** rutas de la línea de base, verificados por las **61** filas de la matriz de sensado de deriva. BT-19 y BT-20 son esas dos tareas.

**Estimación: sin fijar**, por el fundamento de [`Product-Backlog.md`](Product-Backlog.md) §4.1.

## 2. Épicas técnicas y sus tareas

### 2.1 EP-T01 · Fundaciones, publicación y viabilidad

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que el front exista, se publique en el hosting con su dirección pública respondiendo, consuma el punto de salud del servicio de datos, y que las **cuatro** partes de `PT-01` queden medidas antes que cualquier otra cosa |
| Alcance | Proyecto, biblioteca de componentes anclada, página de salud, dirección del servicio de datos desde configuración, flujo de publicación y sus puertas |
| Fuente upstream | `PRODUCT-INTAKE` §15 (etapa `a`, puertas `PT-01` y `PT-04`), §17.6.P.7 a P.10; `05` §5, §8 filas de `PT-01`, §11 `PA-01` y `PA-02`; [`ADR-01`](../05-Arquitectura-Tecnica/Adrs/ADR-01-Render-En-El-Servidor-Con-Circuito-Interactivo.md), [`ADR-07`](../05-Arquitectura-Tecnica/Adrs/ADR-07-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md) |
| Etapa | `a` |
| BT contenidas | BT-01, BT-02, BT-03, BT-04, BT-05, BT-06 |

### 2.2 EP-T02 · Armazón, superficies y línea de base

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que los **dos** shells, el mapa de rutas, los **cuatro** guardianes, las **once** superficies y las **tres** representaciones existan sobre la línea de base visual aprobada, con pantallas de marcador de posición |
| Alcance | Armazón y encaminamiento, superficies, representaciones reutilizadas y los dos valores rotulados como asunción de la categoría 03 |
| Fuente upstream | `05` §3.1 (componentes de capa 1) y §3.4; [`ADR-04`](../05-Arquitectura-Tecnica/Adrs/ADR-04-Tres-Capas-De-Presentacion.md); [`../03-UX-UI-DX/Linea-Base-Visual.md`](../03-UX-UI-DX/Linea-Base-Visual.md) y [`../03-UX-UI-DX/Experiencia-De-Uso.md`](../03-UX-UI-DX/Experiencia-De-Uso.md); `05` §11 `PA-05` |
| Etapa | `b`, salvo el cuarto guardián, que se completa en la `d` porque hasta entonces no existe la marca |
| BT contenidas | BT-07, BT-08, BT-09, BT-10 |

### 2.3 EP-T03 · Salida única, sesión y traducción

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que exista **una sola** salida hacia el servicio de datos, que la credencial de sesión **nunca** llegue al navegador y que los **quince** códigos vivos del contrato se traduzcan a mensaje de superficie en un único lugar |
| Alcance | Cliente tipado, formato de intercambio adoptado, sesión y estado del circuito, traductor de condiciones y la puerta de cero peticiones del navegador |
| Fuente upstream | `05` §3.1 (componentes de capa 2 y 3), §7 filas de salida, autenticación y manejo de errores, §8 filas de peticiones del navegador, de salidas y de apariciones de la credencial; [`ADR-01`](../05-Arquitectura-Tecnica/Adrs/ADR-01-Render-En-El-Servidor-Con-Circuito-Interactivo.md), [`ADR-03`](../05-Arquitectura-Tecnica/Adrs/ADR-03-Credencial-De-Sesion-En-El-Estado-Del-Circuito.md), [`ADR-05`](../05-Arquitectura-Tecnica/Adrs/ADR-05-Estado-Degradado-Como-Superficie.md); `05` §11 `PA-03` |
| Etapa | `c`, que es la primera con una llamada real al servicio de datos |
| BT contenidas | BT-11, BT-12, BT-13, BT-14, BT-15 |

### 2.4 EP-T04 · Anfitrión del visor

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que el componente anfitrión —**que es la capa 1 del contrato de fachada del visor y vive en este proyecto de código**— opere el ciclo de vida de la instancia por las **seis** funciones, gobierne los dos movimientos y libere sus recursos |
| Alcance | Anfitrión, consulta de la preferencia de movimiento reducido, liberación y la medición de `PT-02` desde el lado del anfitrión |
| Fuente upstream | `05` §2.2 (las dos decisiones heredadas del visor), §3.1 componente «Anfitrión del visor», §8 filas de tráfico de circuito, de instancias no liberadas y de invocaciones al interior; [`ADR-06`](../05-Arquitectura-Tecnica/Adrs/ADR-06-Aislamiento-Del-Visor-Tras-Su-Fachada.md); `PRODUCT-INTAKE` §17.6.P.11 punto 5 y §17.7 P.3 |
| Etapa | `f` la previsualización previa al envío, `g` la vista de trabajo entera |
| BT contenidas | BT-16, BT-17, BT-18 |

### 2.5 EP-T05 · Verificación, deriva y puntos abiertos

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que la deriva contra la línea de base aprobada se sense en cada corte, que el guion de demostración acumulativo sea puerta, y que los tres puntos abiertos que no son de esta categoría queden elevados con su plazo |
| Alcance | Matriz de sensado de deriva, guion acumulativo, umbral de tiempo de respuesta, volumen de la comisión y versionado del bundle generado |
| Fuente upstream | `05` §8 filas de estados de la línea de base y de pasos del guion; [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md); `05` §11 `PA-04`, `PA-06` y `PA-07` |
| Etapa | Acumulativa de `b` a `h`; `PA-06` después de la `a`, `PA-08` antes de comprometer la `e` |
| BT contenidas | BT-19, BT-20, BT-21, BT-22, BT-23 |

## 3. Detalle de las tareas técnicas

| BT | Título | Tipo | Épica | Etapa | Prioridad | Estimación | Fuente upstream | Dependencias | Criterios de aceptación | US que la consumen |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| BT-01 | Crear el proyecto del front con su flujo de publicación | feature | EP-T01 | `a` | Alta | Sin fijar | `PRODUCT-INTAKE` §16 y §17.6.P.8; `05` §5 | Ninguna | El proyecto compila dentro del artefacto de agrupación; el flujo de publicación corre de punta a punta: obtención del código, preparación de las **dos** cadenas de herramientas, empaquetado del bundle con copia al directorio de recursos estáticos, publicación, inyección de la dirección del servicio de datos desde secretos y subida | **Infraestructura compartida**: la sostiene `05` §5. Habilita a las 30 |
| BT-02 | Anclar la versión de la biblioteca de componentes de interfaz | indagación | EP-T01 | `a` | Alta | Sin fijar | `05` §11 `PA-01`; `PRODUCT-INTAKE` §17.6.P.1, rotulado **[A VERIFICAR]** | BT-01 | La versión queda anclada según la regla de anclaje de versiones del producto y registrada en el momento del andamiaje; **la interfaz no usa estilos improvisados fuera del sistema visual adoptado**, que es criterio de aceptación de la etapa `b`. **Caja temporal: la etapa `a`** | **Infraestructura compartida**: condiciona las once superficies |
| BT-03 | Construir la página de salud que consume el punto de salud del servicio de datos | feature | EP-T01 | `a` | Alta | Sin fijar | `PRODUCT-INTAKE` §15 (etapa `a`) y §17.5.P.3; `Roadmap-Producto.md` §5.2, transición `a` → `b` | BT-01, BT-05 | La página de salud **muestra datos reales del servidor propio**; la llamada la hace el **servidor de esta pieza** y no el navegador; es la primera verificación de que el camino existe antes de recorrerlo con peso | **Infraestructura compartida**: es el esqueleto ambulante de esta pieza |
| BT-04 | Medir `PT-01` en sus cuatro partes | devops | EP-T01 | `a` | Alta | Sin fijar | `PRODUCT-INTAKE` §15 y §17.6.P.10; `05` §8, cuatro primeras filas; `05` §11 `PA-02` | BT-01, BT-03 | `PT-01.a`: la dirección pública responde **200**. `PT-01.b`: el transporte queda medido y documentado, **con el repliegue de mayor latencia aceptado y no motivo de rediseño**. `PT-01.c`: **20 minutos** de navegación continua sin reciclado y reconexión funcional al cortar y restablecer la red. `PT-01.d`: una llamada de salud devuelve datos reales. **Una puerta que no pasa detiene la planificación de las etapas que dependen de ella.** **Caja temporal: la etapa `a`, antes que cualquier otra cosa** | **Infraestructura compartida**: `PT-01` condiciona el modelo entero de esta pieza |
| BT-05 | Tomar la dirección del servicio de datos de configuración, con secretos inyectados al publicar | feature | EP-T01 | `a` | Alta | Sin fijar | [`ADR-07`](../05-Arquitectura-Tecnica/Adrs/ADR-07-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md); `05` §7, fila de configuración y secretos; `PRODUCT-INTAKE` §17.6.P.5 | BT-01 | La dirección viene de configuración y **nunca embebida en el código**; se inyecta al publicar desde secretos del repositorio; **la dirección real del servidor propio no se versiona**; ninguna superficie la dibuja, ni siquiera deshabilitada | **Infraestructura compartida**: sin ella no hay llamada posible |
| BT-06 | Puerta de publicación que termina comprobando que la dirección pública responde | devops | EP-T01 | `a` | Alta | Sin fijar | `05` §5, filas de puertas bloqueantes; `05` §9, sexto riesgo; `PRODUCT-INTAKE` §17.6.P.8 | BT-01 | Construcción **sin advertencias**; el bundle se genera **en el mismo flujo** y nunca se toma de un artefacto viejo; **el flujo no termina en la subida, termina comprobando que la dirección pública responde**. Se despliega **fuera del horario de uso**, porque la subida **no es transaccional** | **Infraestructura compartida**: una subida que deja la aplicación caída y se reporta como exitosa es peor que una falla visible |
| BT-07 | Construir los dos shells, el mapa de rutas y los cuatro guardianes | feature | EP-T02 | `b` | Alta | Sin fijar | `05` §3.1, componente «Armazón y encaminamiento»; [`ADR-04`](../05-Arquitectura-Tecnica/Adrs/ADR-04-Tres-Capas-De-Presentacion.md); `05` §10.2 `RT-09` y `RT-12` | BT-01, BT-02 | Los **dos** shells —acceso y trabajo— y el mapa de rutas existen; los **cuatro** guardianes son aprovisionamiento resuelto, sesión, papel y cambio de contraseña pendiente; **ninguna ruta del panel es alcanzable sin sesión y un alumno con sesión no alcanza ninguna ruta de administrador**. El cuarto guardián **se completa en la etapa `d`**, porque hasta entonces no existe la marca. **Esto acota lo que se ofrece y no hace cumplir nada** | US-05, US-08, US-29 |
| BT-08 | Construir las once superficies con marcador de posición, sobre la línea de base visual | feature | EP-T02 | `b` | Alta | Sin fijar | `05` §3.1, componente «Superficies», y §3.4; [`../03-UX-UI-DX/Linea-Base-Visual.md`](../03-UX-UI-DX/Linea-Base-Visual.md) | BT-07 | Las **once** superficies son alcanzables por su ruta, con pantallas de marcador de posición; cada una lleva su nombre canónico de 03; **ninguna superficie invoca al cliente tipado**: entre una superficie y la salida hay siempre un servicio de aplicación de front | **Infraestructura compartida**: es el mapa de navegación recorrible de la etapa `b`. Habilita a las 30 |
| BT-09 | Construir las tres representaciones reutilizadas | feature | EP-T02 | `b` | Media | Sin fijar | `05` §3.1, componente «Representaciones reutilizadas»; [`../03-UX-UI-DX/Representacion-Fila-De-Trabajo.md`](../03-UX-UI-DX/Representacion-Fila-De-Trabajo.md), [`../03-UX-UI-DX/Representacion-Lista-De-Observaciones.md`](../03-UX-UI-DX/Representacion-Lista-De-Observaciones.md), [`../03-UX-UI-DX/Representacion-Sello-De-Version.md`](../03-UX-UI-DX/Representacion-Sello-De-Version.md) | BT-08 | Las **tres** piezas compartidas existen: fila de trabajo con su insignia, lista de observaciones con el par declarado y derivado, y sello de versión; **todo estado se comunica por al menos dos canales y nunca sólo por color** | US-13, US-15, US-19, US-22 |
| BT-10 | Confirmar el punto de quiebre principal y la proporción de la escena | indagación | EP-T02 | `g` | Media | Sin fijar | `05` §11 `PA-05`; los dos valores rotulados **[ASUNCIÓN]** por la categoría 03 | BT-08, BT-16 | Los dos valores quedan **confirmados como valores del producto o corregidos**, sobre la línea de base visual ya aprobada. **La maqueta se aprobó, de modo que quedaron ejercidos**; lo que sigue abierto es si se confirman. **Caja temporal: antes de cerrar la etapa `g`** | **Infraestructura compartida**: la decisión es del Product Owner sobre la línea de base |
| BT-11 | Construir el cliente tipado como única salida hacia el servicio de datos | feature | EP-T03 | `c` | Alta | Sin fijar | `05` §3.1, componente «Cliente tipado»; [`ADR-01`](../05-Arquitectura-Tecnica/Adrs/ADR-01-Render-En-El-Servidor-Con-Circuito-Interactivo.md); `05` §10.2 `RT-01` | BT-01, BT-05 | La solicitud **se arma en el servidor** de esta pieza y lleva la credencial adjunta; existe **exactamente 1** salida hacia el servicio de datos y **0** bibliotecas de guion agregadas que consulten servicios por su cuenta; el cliente devuelve el tipo del contrato o su tipo de error, **sin agregar ni recortar campos** | US-01, US-03, US-09, US-11, US-15, US-22, US-24 |
| BT-12 | Adoptar el formato de intercambio que fija la categoría 05 de `GeometriaFactory-Api` | indagación | EP-T03 | `a` | Alta | Sin fijar | `05` §11 `PA-03`; [`Api ADR-02`](../../GeometriaFactory-Api/05-Arquitectura-Tecnica/Adrs/ADR-02-Formato-De-Intercambio-Y-Su-Configuracion.md), que declara que la configuración **obliga a los dos extremos** | BT-11 | El cliente de esta pieza usa **la misma** configuración que el productor: nombres de campo literales, valores de conjunto cerrado por su **nombre** y no por su posición, campos nulos emitidos, números sin cultura y lectura estricta. **Exactamente 1** configuración de intercambio declarada en el producto. **Esta tarea adopta y no decide**: la decisión pertenece al productor, y esta pieza declaró que la adopta. La coincidencia **se verifica ejerciendo el servicio real** y no comparando dos archivos. **Caja temporal: la etapa `a`** | **Infraestructura compartida**: un desajuste rompe en producción y **no lo detecta la compilación** |
| BT-13 | Construir el traductor de las quince condiciones vivas a mensaje de superficie | feature | EP-T03 | `c` | Alta | Sin fijar | `05` §3.1, componente «Traductor de condiciones»; [`ADR-05`](../05-Arquitectura-Tecnica/Adrs/ADR-05-Estado-Degradado-Como-Superficie.md); `05` §8, fila de mensajes que exponen; `05` §10.2 `RT-03` y `RT-07` | BT-11 | Cada uno de los **quince** códigos vivos del conjunto cerrado tiene su mensaje con **qué pasó, por qué y qué hacer**; **0** mensajes llevan dirección de servicio, ruta de datos o traza, verificado sobre los quince **y** sobre el camino de ausencia de respuesta; **nunca una excepción sin manejar y nunca una pantalla rota**; el traductor **no habla con el servicio de datos**, recibe el tipo de error ya traído | US-02, US-04, US-14, US-23, US-26, US-27 |
| BT-14 | Custodiar la credencial de sesión en el estado del circuito | feature | EP-T03 | `c` | Alta | Sin fijar | `05` §3.1, componente «Sesión y estado del circuito»; [`ADR-03`](../05-Arquitectura-Tecnica/Adrs/ADR-03-Credencial-De-Sesion-En-El-Estado-Del-Circuito.md); `05` §8, fila de apariciones de la credencial; `05` §10.2 `RT-02` | BT-11 | La credencial vive **en el estado del circuito, del lado del servidor**; el navegador conserva **sólo** una marca de sesión que **no la transporta y no es legible por guion**; **exactamente 0** apariciones de la credencial en el navegador, verificable con las herramientas de desarrollo en la etapa `c` | US-03, US-05, US-06, US-28, US-29 |
| BT-15 | Puerta de cero peticiones del navegador y una sola salida | devops | EP-T03 | `c` | Alta | Sin fijar | `05` §8, filas de peticiones del navegador y de salidas; `05` §9, primer riesgo; `PRODUCT-INTAKE` §14 (`RA-01`) | BT-11, BT-16 | **Exactamente 0** peticiones del navegador hacia el servicio de datos, contadas en la pestaña de red durante un recorrido completo **incluida la interacción con la escena y con los dos movimientos automáticos prendidos**, que es el peor caso declarado; **exactamente 1** salida y **0** bibliotecas de guion que consulten por su cuenta. **Se mide en cada etapa y no sólo en la que la introdujo** | **Infraestructura compartida**: `RA-01` es la regla que sostiene la topología entera |
| BT-16 | Construir el anfitrión del visor con las seis funciones y el ciclo de vida de la instancia | feature | EP-T04 | `f` | Alta | Sin fijar | `05` §2.2 (el anfitrión **es la capa 1 del contrato de fachada y vive acá**), §3.1 componente «Anfitrión del visor»; [`ADR-06`](../05-Arquitectura-Tecnica/Adrs/ADR-06-Aislamiento-Del-Visor-Tras-Su-Fachada.md); [`Definicion-Contrato-De-Fachada.md`](../../GeometriaFactory-Visor/02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md) | BT-08 | El bundle se invoca **exclusivamente** por sus **6** funciones; **0** invocaciones al interior y **0** accesos al elemento de dibujo por fuera del anfitrión; **0** tráfico de circuito durante la interacción con la escena, y el texto del trabajo viaja del servidor al navegador **una sola vez por trabajo**; el anfitrión **decide cuándo ajustar**: la fachada no observa tamaños por su cuenta | US-12, US-18, US-20, US-21 |
| BT-17 | Leer la preferencia de movimiento reducido y traducirla a dos valores de verdad | feature | EP-T04 | `g` | Alta | Sin fijar | `05` §2.2 y §10.2 `RT-13`; `PRODUCT-INTAKE` §4 (`F-25`) y §17.7 P.3; [`Visor ADR-03`](../../GeometriaFactory-Visor/05-Arquitectura-Tecnica/Adrs/ADR-03-Visualizador-Puro-Sin-Red-Ni-Identidad.md) | BT-16 | **Es esta pieza la que consulta el entorno del navegador** y manda **dos valores de verdad** por la fachada, uno por la órbita de la cámara y otro por el giro de las piezas; **el bundle no consulta nada**; los dos movimientos se gobiernan **por separado** y prenderlos o apagarlos **no reconstruye la instancia** ni pierde la selección vigente. **La ignorancia del bundle es una obligación de esta pieza, no una comodidad** | US-18, US-21 |
| BT-18 | Verificar la liberación de la instancia con diez recorridos de ida y vuelta | devops | EP-T04 | `g` | Alta | Sin fijar | `05` §8, fila de instancias no liberadas; `05` §9, quinto riesgo; `05` §10.2 `RT-05`; `PRODUCT-INTAKE` §17.6.P.11 punto 5 | BT-16, BT-17 | La liberación se invoca **al descartar el componente que aloja la instancia** y **no es opcional**; exactamente **0** instancias no liberadas tras **10** recorridos de ida y vuelta entre trabajos, **sin degradación**, medidos **con los dos movimientos prendidos**, que es su peor caso; es la parte de `PT-02` que se mide desde el lado del anfitrión | US-18, US-21 |
| BT-19 | Ejecutar las 61 filas de la matriz de sensado de deriva | docs | EP-T05 | `b` | Alta | Sin fijar | `05` §8, fila de estados de la línea de base; [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md) | BT-08, BT-09 | **74 de 74** estados, **11 de 11** superficies, **73 de 73** componentes y **24 de 24** rutas de la línea de base visual aprobada quedan demostrados; las **61** filas de la matriz se verifican **al cierre de cada corte** y no una sola vez | **Infraestructura compartida**: es la verificación de que la interfaz construida sigue siendo la maqueta aprobada |
| BT-20 | Fijar el guion de demostración acumulativo como puerta del punto de control | devops | EP-T05 | `b` | Alta | Sin fijar | `05` §8, fila de pasos del guion, rotulada **[ASUNCIÓN]** en cuanto a expresarla como puerta; `PRODUCT-INTAKE` §17.6.P.6; `Roadmap-Producto.md` §5.1 | BT-08 | **100 %** de los pasos del guion de la etapa **y de todas las anteriores** se ejecutan y pasan antes del punto de control, en el navegador del equipo anfitrión; la regla acumulativa es de la fuente y **el hecho de expresarla como puerta es la parte rotulada como asunción**; este proyecto de código **no tiene proyecto de pruebas propio** y ésta es su verificación | **Infraestructura compartida**: reemplaza a la cobertura de líneas, que esta pieza no tiene |
| BT-21 | Elevar el umbral numérico de tiempo de respuesta | indagación | EP-T05 | `c` | Media | Sin fijar | `05` §8, cierre, y §11 `PA-04` | BT-04, BT-13 | O bien el Product Owner fija un umbral, o bien 08 fija su guion de medición después de `PT-01`. **Ninguna de las dos salidas es inventar un número acá**: `05` §8 se niega explícitamente, porque las **tolerancias percibidas de 400 ms** de la categoría 03 dicen a partir de cuándo se muestra un indicador y **no cuánto puede tardar el servidor**. **Caja temporal: después de la etapa `a`** | **Infraestructura compartida**: condiciona el guion de medición de 08 |
| BT-22 | Elevar el volumen de la comisión y la ausencia de paginación | indagación | EP-T05 | `e` | Media | Sin fijar | `05` §11 `PA-06`, rotulado **[A VERIFICAR]** | BT-08 | Queda declarado si el volumen es de decenas o de cientos. El diseño de los **dos** listados supone decenas y por eso **no incorpora paginación**; si resultara mucho mayor, la superficie afectada es `Listado-De-La-Comision` y el cambio es acotado. **Caja temporal: antes de comprometer la etapa `e`** | **Infraestructura compartida**: la decisión es del Product Owner |
| BT-23 | Acompañar la decisión de versionar o ignorar el bundle generado | indagación | EP-T05 | `g` | Media | Sin fijar | `05` §11 `PA-07`; [`Visor Arquitectura-Proyecto-Codigo.md`](../../GeometriaFactory-Visor/05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §11 | BT-01 | Queda decidido y registrado. **La decisión no es de este proyecto de código**: la Fase C de `GeometriaFactory-Visor` la derivó a 09, y alcanza a esta pieza porque el bundle vive en su directorio de recursos estáticos y **nunca se edita a mano**. **Caja temporal: al emitirse 09** | **Infraestructura compartida**: la titularidad es de `09-Devops` |

**Quince tareas se justifican como infraestructura compartida** —BT-01, BT-02, BT-03, BT-04, BT-05, BT-06, BT-08, BT-10, BT-12, BT-15, BT-19, BT-20, BT-21, BT-22 y BT-23— y las **ocho** restantes —BT-07, BT-09, BT-11, BT-13, BT-14, BT-16, BT-17 y BT-18— declaran al menos una historia consumidora. **Quince más ocho son veintitrés**, y ninguna queda sin una cosa ni la otra.

La proporción es alta y tiene una causa declarable: en esta pieza **el armazón, la salida única, la publicación y las puertas de conteo no pertenecen a ninguna historia en particular** porque las sostienen todas. Es la contracara de que las once superficies compartan dos shells, un cliente tipado y un traductor.

## 4. Trazabilidad BT ↔ US ↔ CU

Las veintitrés filas están, una por tarea técnica, sin agrupar. Los casos de uso son los de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3 y las superficies las de `05` §3.4.

| BT | US que la consumen | CU upstream | Superficies que toca | Fuente de arquitectura |
| --- | --- | --- | --- | --- |
| BT-01 | Infraestructura compartida (habilita a las 30) | CU-01 a CU-10 | Las once | `05` §5 |
| BT-02 | Infraestructura compartida, con efecto sobre las once superficies | CU-01 a CU-10 | Las once | `05` §11 `PA-01` |
| BT-03 | Infraestructura compartida | — (esqueleto ambulante) | Ninguna de las once: es la página de salud de la etapa `a` | `PRODUCT-INTAKE` §15 |
| BT-04 | Infraestructura compartida | — (puerta `PT-01`) | Ninguna | `05` §8, `PT-01.a` a `PT-01.d` |
| BT-05 | Infraestructura compartida | CU-01 a CU-09 | Ninguna: ninguna superficie la dibuja | ADR-07 |
| BT-06 | Infraestructura compartida | — (puerta de publicación) | Ninguna | `05` §5 |
| BT-07 | US-05, US-08, US-29 | CU-02, CU-03, CU-04 | Los dos shells y las once | ADR-04, `RT-09`, `RT-12` |
| BT-08 | Infraestructura compartida (habilita a las 30) | CU-01 a CU-10 | Las once | `05` §3.4 |
| BT-09 | US-13, US-15, US-19, US-22 | CU-05, CU-06, CU-07, CU-08 | `Panel-De-Trabajos-Del-Alumno`, `Envio-De-Trabajo`, `Vista-De-Trabajo`, `Listado-De-La-Comision` | `05` §3.1, representaciones |
| BT-10 | Infraestructura compartida | — (línea de base visual) | Las once, y `Vista-De-Trabajo` en la proporción de la escena | `05` §11 `PA-05` |
| BT-11 | US-01, US-03, US-09, US-11, US-15, US-22, US-24 | CU-01 a CU-09 | Todas menos `Estado-Degradado-Y-Reconexion` | ADR-01, `RT-01` |
| BT-12 | Infraestructura compartida | CU-01 a CU-09 | Ninguna: es configuración de la salida | `05` §11 `PA-03`, `Api ADR-02` |
| BT-13 | US-02, US-04, US-14, US-23, US-26, US-27 | Los diez | Las once, y de manera decisiva `Estado-Degradado-Y-Reconexion` | ADR-05, `RT-03`, `RT-07` |
| BT-14 | US-03, US-05, US-06, US-28, US-29 | CU-02, CU-03, CU-04 | `Ingreso`, `Credencial-Propia`, y los dos shells | ADR-03, `RT-02` |
| BT-15 | Infraestructura compartida | — (puerta de `RA-01`) | Las once | `05` §8, ADR-01 |
| BT-16 | US-12, US-18, US-20, US-21 | CU-05, CU-07 | `Envio-De-Trabajo`, `Vista-De-Trabajo` | ADR-06, `RT-04`, `RT-10` |
| BT-17 | US-18, US-21 | CU-07 | `Vista-De-Trabajo` | ADR-06, `RT-13` |
| BT-18 | US-18, US-21 | CU-05, CU-07 | `Envio-De-Trabajo`, `Vista-De-Trabajo` | ADR-06, `RT-05` |
| BT-19 | Infraestructura compartida | CU-01 a CU-10 | Las once | `05` §8, línea de base |
| BT-20 | Infraestructura compartida | CU-01 a CU-10 | Las once | `05` §8, guion acumulativo |
| BT-21 | Infraestructura compartida | — (guion de medición) | Las once | `05` §11 `PA-04` |
| BT-22 | Infraestructura compartida | CU-08 | `Listado-De-La-Comision` | `05` §11 `PA-06` |
| BT-23 | Infraestructura compartida | — (artefacto generado) | Ninguna | `05` §11 `PA-07` |

**Cobertura inversa: los diez casos de uso tienen al menos una tarea técnica que los realiza.** CU-01 en BT-08, BT-11 y BT-13; CU-02 en BT-07, BT-11, BT-13 y BT-14; CU-03 en BT-07, BT-13 y BT-14; CU-04 en BT-07, BT-11, BT-13 y BT-14; CU-05 en BT-09, BT-11, BT-13, BT-16 y BT-18; CU-06 en BT-09, BT-11 y BT-13; CU-07 en BT-09, BT-13, BT-16, BT-17 y BT-18; CU-08 en BT-09, BT-11, BT-13 y BT-22; CU-09 en BT-11 y BT-13; CU-10 en BT-13, BT-19 y BT-20. **CU-10 no consume el cliente tipado**, y es correcto: su superficie existe precisamente para cuando el cliente no obtiene respuesta.

**Cobertura de las once superficies.** Las once quedan alcanzadas por BT-08, BT-19 y BT-20; **dos de ellas** —`Envio-De-Trabajo` y `Vista-De-Trabajo`— son además las únicas que pasan por el anfitrión del visor, en BT-16, BT-17 y BT-18. Que sólo dos superficies de once toquen el bundle es lo que hace barato sostener el aislamiento del visor (`05` §3.4).

**Cobertura de los ocho componentes de `05` §3.1.** Armazón y encaminamiento en BT-07; Superficies en BT-08; Representaciones reutilizadas en BT-09; Servicios de aplicación de front en BT-11 y BT-13, que son sus dos salidas; Sesión y estado del circuito en BT-14; Cliente tipado en BT-11 y BT-12; Traductor de condiciones en BT-13; Anfitrión del visor en BT-16, BT-17 y BT-18. **Los ocho tienen tarea técnica.**

## 5. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial del backlog técnico de `GeometriaFactory-Web`. Declara **cinco** épicas técnicas alineadas con las tres capas internas, el anfitrión del visor y la verificación, y **veintitrés** tareas técnicas inline —por debajo del umbral de treinta— cada una con tipo, fuente upstream por identificador, etapa, dependencias, criterios de aceptación verificables y las historias que la consumen. Declara las tres particularidades del proyecto de código: que **tres tareas son puertas de conteo con umbral cero** porque éste es el único proyecto de código que puede violar las tres reglas de arquitectura, que **BT-12 adopta una decisión que se toma en otro proyecto de código** y no la decide, y que **la verificación de esta pieza no es cobertura de líneas sino un guion acumulativo y una matriz de deriva**. Convierte en trabajo los siete puntos abiertos de `05` §11 que lo admiten. Emite la matriz BT ↔ US ↔ CU con sus veintitrés filas, la cobertura inversa sobre los diez casos de uso, la cobertura de las **once** superficies y la de los **ocho** componentes. |
