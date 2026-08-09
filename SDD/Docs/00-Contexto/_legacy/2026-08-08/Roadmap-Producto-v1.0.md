> **Artefacto archivado — estado `Superado`**
>
> Esta es una **copia archivada** del documento `Roadmap-Producto.md` en su versión **1.0**, tomada el 2026-08-08 por el orquestador SDD antes de que la versión vigente la superara (`Master-Prompt.md` §5 y §5.1).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-08
> - **Versión vigente:** [`Roadmap-Producto.md`](../../Roadmap-Producto.md)
>
> El cuerpo que sigue **no se modifica**: un registro que se corrige después deja de ser un registro. Este archivo no se renombra, no se reenlaza y no vuelve a tocarse.

---

# Roadmap del Producto

**Producto:** Fábrica de Geometría
**Documento:** Roadmap-Producto.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-08
**Autor:** Product Manager Senior (AG-00), actuando también como Analista de Negocio Senior (AG-01) por `Rules-Contexto.md` §1.3
**Trazabilidad upstream:** PRODUCT-INTAKE §4 (capacidades y su prioridad), §10 (restricción de fecha y de etapas en serie), §11 (riesgos que ordenan la medición temprana), §13 (composición y orden topológico), §15 (esquema de descomposición y delivery, etapas `a` a `h`, reglas de delivery y puertas técnicas), §17 (criterios de aceptación de etapa citados por bloque técnico)
**Trazabilidad downstream:** 06-Backlog-Tecnico, 07-Plan-Sprint, 05-Arquitectura-Tecnica, 08-Calidad-Y-Pruebas, 09-Devops

---

## Tabla de contenido

- [1. Propósito](#1-propósito)
  - [1.1 Por qué este roadmap no tiene fechas](#11-por-qué-este-roadmap-no-tiene-fechas)
  - [1.2 Unidad de planificación](#12-unidad-de-planificación)
- [2. Fases del producto](#2-fases-del-producto)
  - [2.1 Tabla de hitos](#21-tabla-de-hitos)
  - [2.2 Puertas técnicas y dónde se miden](#22-puertas-técnicas-y-dónde-se-miden)
- [3. Matriz fase, épica, sprint y release](#3-matriz-fase-épica-sprint-y-release)
- [4. Dependencias entre fases](#4-dependencias-entre-fases)
- [5. Criterios de transición entre fases](#5-criterios-de-transición-entre-fases)
  - [5.1 Criterios comunes a toda transición](#51-criterios-comunes-a-toda-transición)
  - [5.2 Criterios propios de cada transición](#52-criterios-propios-de-cada-transición)
- [6. Trazabilidad downstream](#6-trazabilidad-downstream)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Propósito

Ordenar la construcción de **Fábrica de Geometría** en fases con criterios de salida verificables, de modo que en cualquier momento se pueda responder si una fase terminó o no. Las fases, su orden y sus criterios derivan de PRODUCT-INTAKE §15 y no se originan acá.

### 1.1 Por qué este roadmap no tiene fechas

El intake declara **sin fecha objetivo**, y lo justifica: «sin plazo; el avance se mide por etapas cerradas». El ritmo lo fija el punto de control de cada etapa, que es un cuello por diseño (PRODUCT-INTAKE §10).

En consecuencia, los hitos de este roadmap se expresan por **etapa cerrada y demostrada**, no por fecha de calendario, y la columna de release target dice qué se demuestra, no cuándo. Poner fechas acá sería originar una decisión que el Product Owner declaró explícitamente que no toma.

### 1.2 Unidad de planificación

La unidad de planificación es la **etapa** del intake. Cada fase de este roadmap **es** una etapa, con el mismo identificador de letra; no hay una agrupación intermedia.

La estrategia declarada es corte vertical con esqueleto ambulante previo: cada etapa entrega una funcionalidad acotada operativa de punta a punta, atravesando todas las capas. **No se planifica por capa técnica**: el criterio de corte es qué puede hacer la persona al terminar la etapa que antes no podía (PRODUCT-INTAKE §15).

## 2. Fases del producto

Las dos primeras fases son **hitos internos**: se validan pero no se muestran al cliente. **De la fase `c` en adelante, todas son hitos demostrables sin excepción**: si una etapa planificada no produce algo que el cliente pueda recorrer, está mal cortada y se redivide (PRODUCT-INTAKE §15).

### 2.1 Tabla de hitos

| Fase | Objetivo | Épicas | Sprints estimados | Entregable | Release target |
|---|---|---|---|---|---|
| `a` · Andamiaje | Que el producto compile y que las dos piezas desplegables se ejecuten desde sus guiones, dentro del entorno de desarrollo, atravesando ya la jerarquía completa con carga funcional mínima | Sin capacidad funcional asociada. Épica candidata: «Esqueleto ambulante y verificación de viabilidad», a formalizar en 06 | No aplica: el intake no planifica en sprints. La unidad es la etapa, y la categoría 07 emite `Mini-Plan.md` por `equipo_n = 1` | Esqueleto ejecutable de las dos piezas desplegables y las mediciones de PT-01 y PT-04 | Hito interno: no se demuestra al cliente |
| `b` · Cáscara de la pieza pública | Que todas las rutas sean navegables con pantallas de marcador de posición, según el maquetado | Sin capacidad funcional asociada. Épica candidata: «Navegación y sistema visual», a formalizar en 06 | No aplica, ver fase `a` | Mapa de navegación recorrible con pantallas de marcador de posición | Hito interno: no se demuestra al cliente |
| `c` · Administrador: alta inicial y sesión | Configurar el administrador en el primer arranque, entrar, cambiar contraseña y salir, con todo persistido | F-01, F-05. Épica candidata: «Identidad del administrador y sesión» | No aplica, ver fase `a` | Circuito de administración inicial completo y persistido | Demostración en el punto de control |
| `d` · Alumno: registro, habilitación y primer ingreso | Registrarse, ser habilitado y entrar estableciendo contraseña, sin correo de por medio | F-02, F-03, F-04. Épica candidata: «Ciclo de vida de la cuenta de alumno» | No aplica, ver fase `a` | Alta de alumno de punta a punta sin correo | Demostración en el punto de control |
| `e` · Alta de trabajo y vista de trabajos | Cargar, listar, reeditar y eliminar trabajos propios; y que el administrador vea todos, agrupados y filtrados | F-06, F-07, F-08, F-12. Épica candidata: «Gestión del trabajo» | No aplica, ver fase `a` | Trabajos con dueño, estado e identificador, y el listado del administrador | Demostración en el punto de control |
| `f` · Importación y validación | Que el texto real del alumno se valide, muestre sus advertencias y el trabajo se finalice | F-09, F-10. Épica candidata: «Interpretación y verificación del dato del alumno» | No aplica, ver fase `a` | Validación tolerante y verificación de valores, con los siete escenarios de datos ejercidos | Demostración en el punto de control |
| `g` · Visualización y árbol | Ver el trabajo en tres dimensiones y como árbol, dentro del producto, para los dos papeles | F-11, F-13. Épica candidata: «Visualización del trabajo» | No aplica, ver fase `a` | Visualización tridimensional y árbol integrados, con la sincronización por índice | Demostración en el punto de control. **Cierra el alcance comprometido** |
| `h…` · Pendientes | Se planifican con la plantilla completa cuando `g` esté cerrada y demostrada | F-14 a F-17, y los candidatos declarados en las exclusiones X-5, X-6 y X-7 | No aplica, ver fase `a` | A definir al planificar la fase | Primer despliegue real verificado desde la red de la facultad (F-14) |

El orden respeta el orden topológico de la composición del producto: la fase `a` construye los siete proyectos de código en su esqueleto y de ahí en adelante cada fase agrega comportamiento sobre esa estructura ya validada (PRODUCT-INTAKE §15).

### 2.2 Puertas técnicas y dónde se miden

Una puerta que no pasa **detiene la planificación de las fases que dependen de ella**; no se arrastra como deuda (PRODUCT-INTAKE §15).

| Puerta | Dónde se mide | Qué condiciona |
|---|---|---|
| PT-01, en sus cuatro partes | Fase `a`, antes que cualquier otra cosa | El modelo entero de la pieza pública. Sólo el peor resultado en el transporte o una falla de estabilidad obligan a cambiarlo; un repliegue de transporte no es motivo de rediseño |
| PT-04 | Fase `a` | Que la imagen del servicio de datos se construya y arranque desde el entorno de desarrollo |
| PT-02 y PT-03 | Antes de comprometer la fase `g` | Que la visualización funcione embebida y que su motor gráfico quede dentro del paquete, sin depender de una red externa |
| PT-05 | Fase `h`, con el despliegue real | Valida la premisa completa de la partición del producto. El intake recomienda no relegarla |

## 3. Matriz fase, épica, sprint y release

Las épicas formales las define la categoría 06; acá figuran como candidatas derivadas del agrupamiento de capacidades que el intake ya declara por etapa (PRODUCT-INTAKE §15). La columna de sprint no aplica en todo el roadmap por la misma razón declarada en §2.1: el intake no planifica en sprints y la categoría 07 emite `Mini-Plan.md`.

| Fase | Épica candidata (a formalizar en 06) | Capacidades | Sprint | Release |
|---|---|---|---|---|
| `a` | Esqueleto ambulante y verificación de viabilidad | — | No aplica | Sin release: hito interno |
| `b` | Navegación y sistema visual | — | No aplica | Sin release: hito interno |
| `c` | Identidad del administrador y sesión | F-01, F-05 | No aplica | Demostración `c` |
| `d` | Ciclo de vida de la cuenta de alumno | F-02, F-03, F-04 | No aplica | Demostración `d` |
| `e` | Gestión del trabajo | F-06, F-07, F-08, F-12 | No aplica | Demostración `e` |
| `f` | Interpretación y verificación del dato del alumno | F-09, F-10 | No aplica | Demostración `f` |
| `g` | Visualización del trabajo | F-11, F-13 | No aplica | Demostración `g`, cierre del alcance comprometido |
| `h…` | A definir al planificar la fase | F-14, F-15, F-16, F-17 | No aplica | Despliegue real verificado |

## 4. Dependencias entre fases

Las fases son **estrictamente secuenciales**: no se empieza una antes de cerrar la anterior, y sin OK explícito no se avanza (PRODUCT-INTAKE §10 y §15). No hay paralelismo posible en este roadmap, y no es una limitación de recursos sino una regla de delivery declarada.

| Fase | Depende de | Naturaleza de la dependencia |
|---|---|---|
| `a` | — | Punto de partida. Además concentra la medición de viabilidad, porque su resultado puede cambiar el modelo de la pieza pública |
| `b` | `a` | Necesita la estructura ejecutable y el resultado de PT-01: si el modelo de la pieza pública cambia, la cáscara se construye sobre otro modelo |
| `c` | `b` | Necesita las rutas navegables sobre las que colgar el circuito de sesión |
| `d` | `c` | La habilitación del alumno la ejerce el administrador, que existe recién al cerrar `c` |
| `e` | `d` | Un trabajo tiene dueño; sin cuentas de alumno operativas no hay dueño |
| `f` | `e` | La validación se aplica sobre un trabajo cargado; sin `e` no hay dónde aplicarla |
| `g` | `f` | La visualización dibuja el resultado de la interpretación; el intake exige además PT-02 y PT-03 antes de comprometerla |
| `h…` | `g` | El intake declara que se planifica con la plantilla completa cuando `g` esté cerrada y demostrada |

Regla transversal de no regresión: al cerrar cada fase deben seguir pasando, **sin correcciones**, los guiones de todas las anteriores (PRODUCT-INTAKE §15).

## 5. Criterios de transición entre fases

### 5.1 Criterios comunes a toda transición

Derivan de las reglas de delivery del intake y se aplican en **todas** las transiciones, además de los criterios propios de cada una (PRODUCT-INTAKE §15).

- [ ] Los guiones de demostración de todas las fases anteriores vuelven a pasar sin correcciones.
- [ ] La fase incorporó pruebas automatizadas de las reglas de negocio que introdujo.
- [ ] El informe de cierre de la fase está escrito, es autocontenido y está indizado.
- [ ] La rama de la fase tiene su solicitud de incorporación abierta: esa solicitud **es** el punto de control.
- [ ] El Product Owner dio OK explícito en el punto de control.
- [ ] La rama de la fase está incorporada antes de abrir la siguiente.
- [ ] Todo guion que involucre el texto de figuras usa datos verificados del intake; **no se inventan datos de prueba**.

### 5.2 Criterios propios de cada transición

| Fase origen | Fase destino | Criterios verificables |
|---|---|---|
| — | `a` | - [ ] El intake y el manifiesto están aprobados por el Product Owner |
| `a` | `b` | - [ ] El producto compila entero y las dos piezas desplegables arrancan desde sus guiones dentro del entorno de desarrollo<br>- [ ] La página de estado de la pieza pública consume el punto de salud del servicio de datos y muestra datos reales<br>- [ ] PT-01.a: la dirección pública responde correctamente<br>- [ ] PT-01.b: el transporte de la sesión interactiva está medido y su resultado documentado, incluido el repliegue si ocurre<br>- [ ] PT-01.c: veinte minutos de navegación continua sin que el proceso recicle la sesión, y reconexión funcional al cortar y restablecer la red<br>- [ ] PT-01.d: una llamada de salud devuelve datos reales del servidor propio<br>- [ ] PT-04: la imagen del servicio de datos se construye, arranca, aplica sus actualizaciones de esquema sobre base vacía y responde salud<br>- [ ] Está verificado que la sesión interactiva no llega al servicio de datos |
| `b` | `c` | - [ ] Todas las rutas del mapa de navegación son alcanzables, con pantallas de marcador de posición<br>- [ ] La interfaz usa el sistema visual adoptado, sin estilos improvisados fuera de él |
| `c` | `d` | - [ ] El administrador se configura en el primer arranque y **sólo** mientras no exista ninguno<br>- [ ] Entrar, cambiar contraseña exigiendo la actual y salir funcionan, y el cambio persiste entre reinicios<br>- [ ] Las actualizaciones de esquema se aplican solas sobre una base inexistente<br>- [ ] La credencial de sesión no es observable desde el navegador |
| `d` | `e` | - [ ] Un alumno se registra con correo, nombre y apellido, sin elegir contraseña<br>- [ ] Un alumno no habilitado recibe un aviso explícito de que su cuenta está pendiente<br>- [ ] El administrador habilita, bloquea, rehabilita y da de baja, y la baja exige confirmación escribiendo el correo de la cuenta<br>- [ ] El alumno habilitado establece su contraseña en su primer ingreso efectivo, sin ningún correo de por medio |
| `e` | `f` | - [ ] Un trabajo se carga con nombre, fecha, descripción y texto, y recibe identificador propio y estado<br>- [ ] Un trabajo se guarda como borrador **con el texto inválido** y se reedita<br>- [ ] La eliminación sólo procede en estado `Borrador` y sólo sobre trabajos propios, verificado **forzando la petición al servicio de datos**, no sólo por la interfaz<br>- [ ] Un alumno que pide el trabajo de otro recibe «no encontrado»<br>- [ ] El administrador ve todos los trabajos, agrupados y filtrados por alumno |
| `f` | `g` | - [ ] Los nueve casos de prueba obligatorios pasan con los escenarios de datos del intake como entrada<br>- [ ] El texto tal como lo emite el programa del alumno se interpreta, con sus particularidades de formato incluidas<br>- [ ] Un cubo del primer ejemplo produce advertencia de área con los dos valores expresados; el mismo cubo del segundo ejemplo **no** produce ninguna<br>- [ ] Un tipo desconocido produce error con índice de figura y campo, y el trabajo no se puede finalizar pero sí guardar como borrador<br>- [ ] La comparación de valores usa tolerancia absoluta y no igualdad exacta<br>- [ ] El texto original se conserva íntegro y nunca se reescribe<br>- [ ] PT-02 y PT-03 medidas antes de comprometer `g` |
| `g` | `h…` | - [ ] Las tres figuras del escenario semilla se dibujan, **ortoedro incluido**<br>- [ ] Navegar entre trabajos ida y vuelta diez veces no degrada la visualización<br>- [ ] Procesar el mismo trabajo dos veces produce la misma disposición<br>- [ ] Durante la interacción tridimensional no hay ni una sola petición originada por la visualización<br>- [ ] El árbol y la escena se sincronizan por índice de pieza<br>- [ ] El administrador abre cualquier trabajo y ve exactamente lo mismo que vio el alumno<br>- [ ] El alcance comprometido está cerrado: las siete fases tienen OK explícito |

## 6. Trazabilidad downstream

| Contenido | Destino | Qué consume |
|---|---|---|
| §2 Fases y sus objetivos | 06-Backlog-Tecnico | Formaliza las épicas candidatas y las descompone en ítems de backlog |
| §3 Matriz | 07-Plan-Sprint | `Mini-Plan.md` toma las fases como unidad de planificación, con sus puntos de control |
| §4 Dependencias | 07-Plan-Sprint | Fija que no hay paralelismo y que el orden es estricto |
| §5 Criterios de transición | 08-Calidad-Y-Pruebas | Cada criterio verificable alimenta la estrategia de verificación y los guiones de demostración |
| §2.2 Puertas técnicas | 05-Arquitectura-Tecnica, 09-Devops | Las puertas condicionan decisiones de arquitectura y la definición del proceso de construcción y despliegue |

Vocabulario: «fase», «etapa», «puerta técnica», «hito interno», «hito demostrable» y «punto de control» están definidos en `Vision-Producto.md` §9 y no se redefinen acá.

## 7. Control de cambios

| Versión | Fecha | Cambios | Autor |
|---|---|---|---|
| 1.0 | 2026-08-08 | Emisión inicial. Ocho fases derivadas de las etapas `a` a `h` del intake, con hitos expresados por etapa cerrada y demostrada en lugar de fecha, por la restricción de fecha declarada y justificada aguas arriba. Incluye la ubicación de las cinco puertas técnicas, la matriz de fase a épica candidata, las dependencias estrictamente secuenciales y ocho transiciones con criterios verificables, más siete criterios comunes a toda transición derivados de las reglas de delivery. | Product Manager Senior (AG-00) |
| 1.0 | 2026-08-08 | Corrección absorbida del audit A-00-01-r1, sin subir versión por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **H-01**: se califican las tres ocurrencias desnudas de «pieza» en su referente de artefacto desplegable —§2.1, fase `a`, en la columna de objetivo y en la de entregable, y §5.2, transición `a` a `b`—, sobre la familia que declara `Vision-Producto.md` §9.2. | Product Manager Senior (AG-00) |
