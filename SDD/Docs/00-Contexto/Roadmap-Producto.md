# Roadmap del Producto

**Producto:** Fábrica de Geometría
**Documento:** Roadmap-Producto.md
**Versión:** 1.4
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Product Manager Senior (AG-00), actuando también como Analista de Negocio Senior (AG-01) por `Rules-Contexto.md` §1.3
**Trazabilidad upstream:** PRODUCT-INTAKE 1.9 §4 (capacidades y su prioridad), §4.1 (reglas de negocio declaradas, RN-10 y RN-11), §4.2 (modelo de estados del trabajo), §10 (restricción de fecha y de etapas en serie), §11 (riesgos que ordenan la medición temprana), §13 (composición y orden topológico), §15 (esquema de descomposición y delivery, etapas `a` a `i`, reglas de delivery y puertas técnicas), §17 (criterios de aceptación de etapa citados por bloque técnico)
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
| `d` · Alumno: registro, habilitación, primer ingreso y reseteo de credencial | Registrarse, ser habilitado y entrar estableciendo contraseña, sin correo de por medio; y que el administrador **resetee la credencial** del alumno que la olvidó, sin que la cuenta ni sus trabajos se pierdan | F-02, F-03, F-04, F-26. Épica candidata: «Ciclo de vida de la cuenta de alumno» | No aplica, ver fase `a` | Alta de alumno de punta a punta sin correo, y recuperación del olvido de contraseña sin baja. F-26, el reseteo, **se entrega en esta fase**: desde `PRODUCT-INTAKE` 1.7 es `Must Have` y **condiciona el cierre**, porque sin ella el laboratorio queda inutilizable para el primer alumno que olvide su contraseña y la única salida documentada le cuesta todos sus trabajos | Demostración en el punto de control |
| `e` · Alta de trabajo y vista de trabajos | Cargar, listar, reeditar y eliminar trabajos propios; y que el administrador vea los de toda la comisión **menos los que están en estado `Borrador`**, agrupados y filtrados | F-06, F-07, F-08, F-12. Épica candidata: «Gestión del trabajo» | No aplica, ver fase `a` | Trabajos con dueño, estado e identificador, y el listado del administrador | Demostración en el punto de control |
| `f` · Importación y validación | Que el texto real del alumno se interprete al **enviar**, muestre sus advertencias y el trabajo pase a estado `Pendiente`, o quede en `Borrador` con sus errores localizados | F-09, F-10, F-22. Épica candidata: «Interpretación y verificación del dato del alumno» | No aplica, ver fase `a` | Validación tolerante, verificación de valores y acción única de envío, con los **ocho** escenarios de datos del intake §20 ejercidos | Demostración en el punto de control |
| `g` · Visualización y árbol | Ver el trabajo en tres dimensiones y como árbol, dentro del producto, para los dos papeles | F-11, F-13, F-25. Épica candidata: «Visualización del trabajo» | No aplica, ver fase `a` | Visualización tridimensional y árbol integrados, con la sincronización por índice. F-25, el movimiento automático, **se entrega en esta fase**: desde `PRODUCT-INTAKE` 1.7 es `Must Have` y **condiciona el cierre**, porque la órbita de la cámara ya existe en la visualización que la cátedra usa hoy y cerrar `g` sin ella sería una regresión frente a lo que el alumno ya tiene | Demostración en el punto de control |
| `h` · Circuito de revisión del administrador | Que el administrador apruebe o rechace un trabajo en estado `Pendiente`, deje su comentario opcional y elimine cualquier trabajo que ve; y que el alumno vea **el desenlace en su propio listado** y **el comentario al abrir el trabajo** desde ese listado | F-21, F-23, F-24. Épica candidata: «Desenlace de la entrega» | No aplica, ver fase `a` | Circuito de revisión completo, con los dos estados terminales operativos | Demostración en el punto de control. **Cierra el alcance comprometido** |
| `i…` · Pendientes | Se planifican con la plantilla completa cuando `h` esté cerrada y demostrada | F-14 a F-17, y los candidatos declarados en las exclusiones X-6 y X-7 | No aplica, ver fase `a` | A definir al planificar la fase | Primer despliegue real verificado desde la red de la facultad (F-14) |

El orden respeta el orden topológico de la composición del producto: la fase `a` construye los siete proyectos de código en su esqueleto y de ahí en adelante cada fase agrega comportamiento sobre esa estructura ya validada (PRODUCT-INTAKE §15).

### 2.2 Puertas técnicas y dónde se miden

Una puerta que no pasa **detiene la planificación de las fases que dependen de ella**; no se arrastra como deuda (PRODUCT-INTAKE §15).

| Puerta | Dónde se mide | Qué condiciona |
|---|---|---|
| PT-01, en sus cuatro partes | Fase `a`, antes que cualquier otra cosa | El modelo entero de la pieza pública. Sólo el peor resultado en el transporte o una falla de estabilidad obligan a cambiarlo; un repliegue de transporte no es motivo de rediseño |
| PT-04 | Fase `a` | Que la imagen del servicio de datos se construya y arranque desde el entorno de desarrollo |
| PT-02 y PT-03 | Antes de comprometer la fase `g` | Que la visualización funcione embebida y que su motor gráfico quede dentro del paquete, sin depender de una red externa |
| PT-05 | Fase `i`, con el despliegue real | Valida la premisa completa de la partición del producto. El intake recomienda no relegarla. **La letra corrió de `h` a `i`** el 2026-08-08, al insertarse el circuito de revisión como fase `h`: la puerta sigue atada al despliegue real, no a la letra |

## 3. Matriz fase, épica, sprint y release

Las épicas formales las define la categoría 06; acá figuran como candidatas derivadas del agrupamiento de capacidades que el intake ya declara por etapa (PRODUCT-INTAKE §15). La columna de sprint no aplica en todo el roadmap por la misma razón declarada en §2.1: el intake no planifica en sprints y la categoría 07 emite `Mini-Plan.md`.

| Fase | Épica candidata (a formalizar en 06) | Capacidades | Sprint | Release |
|---|---|---|---|---|
| `a` | Esqueleto ambulante y verificación de viabilidad | — | No aplica | Sin release: hito interno |
| `b` | Navegación y sistema visual | — | No aplica | Sin release: hito interno |
| `c` | Identidad del administrador y sesión | F-01, F-05 | No aplica | Demostración `c` |
| `d` | Ciclo de vida de la cuenta de alumno | F-02, F-03, F-04, F-26 | No aplica | Demostración `d` |
| `e` | Gestión del trabajo | F-06, F-07, F-08, F-12 | No aplica | Demostración `e` |
| `f` | Interpretación y verificación del dato del alumno | F-09, F-10, F-22 | No aplica | Demostración `f` |
| `g` | Visualización del trabajo | F-11, F-13, F-25 | No aplica | Demostración `g` |
| `h` | Desenlace de la entrega | F-21, F-23, F-24 | No aplica | Demostración `h`, cierre del alcance comprometido |
| `i…` | A definir al planificar la fase | F-14, F-15, F-16, F-17 | No aplica | Despliegue real verificado |

**Por qué F-26 se ubica en la fase `d`.** El intake incorpora F-26 en 1.7 §4 como `Must Have`, y **no le asigna etapa**: §15 sigue declarando `d` con F-02, F-03 y F-04. La ubicación es entonces una decisión de planificación de este roadmap —la misma situación de F-25— y se toma por el criterio de corte de §1.2, qué puede hacer la persona al terminar la fase. Tres razones:

1. **Toda la superficie que F-26 necesita la levanta `d`, y ninguna fase anterior la tiene.** El reseteo se acciona desde el panel de cuentas del administrador, que lo construye F-03 en esta misma fase, y su consecuencia observable —que el alumno entre con la provisoria y quede obligado a cambiarla antes de llegar a ninguna otra parte— ocurre sobre el circuito de primer ingreso que construye F-04, también acá. Ubicarla después obligaría a volver sobre dos superficies ya cerradas; ubicarla antes es imposible, porque no existirían ni el panel ni el ingreso del alumno.
2. **Es la fase donde el agujero se abre.** Al cerrar `d` el producto ya tiene alumnos con cuenta y con contraseña propia, de modo que desde ese punto en adelante el olvido es posible. Entregar el reseteo más tarde significaría dejar declarado, durante todas las fases intermedias, que la única salida es dar de baja y volver a dar de alta, que es exactamente el procedimiento que esta capacidad vino a reemplazar. En `e` esa salida ya destruye trabajos cargados.
3. **No depende de nada que `d` no tenga.** El reseteo no toca trabajos, no toca la interpretación del texto ni la visualización, y por **RN-15** no es siquiera una transición de la máquina de estados de la cuenta: opera sobre la credencial. No hay ninguna capacidad de `e` a `h` de la que dependa.

**Ubicarla en `d` la compromete, y así corresponde**: F-26 es `Must Have`, de modo que la transición `d` → `e` de §5.2 incorpora sus criterios y la fase `d` no cierra sin ellos. La condición para que este fundamento cambie es que el Product Owner le asigne etapa explícita en el intake §15 o cambie su prioridad.

**Por qué F-25 se ubica en la fase `g` y no en `i…`.** El intake incorpora F-25 en 1.5 §4 —con prioridad `Should Have` entonces y **`Must Have` desde 1.7**— pero **no le asigna etapa**: §15 sigue declarando `g` con F-11 y F-13, e `i…` con F-14 a F-17. La ubicación es entonces una decisión de planificación de este roadmap, y se toma por el criterio de corte declarado en §1.2 —qué puede hacer la persona al terminar la fase—, no por la prioridad. Cuatro razones, en orden de peso:

1. **Es un agregado sobre la superficie de `g`, y no existe antes.** F-25 gobierna el movimiento de la escena tridimensional y de las piezas que la componen. Esa escena la construye la fase `g`; antes de `g` no hay nada que orbitar ni que hacer girar. Ubicarla en `i…` no la adelantaría ni la abarataría: la obligaría a volver sobre una superficie ya cerrada.
2. **La órbita de la cámara ya existe y se porta.** El intake declara que ese movimiento **existe en la visualización que la cátedra usa hoy** y que se lleva al producto. La fase `g` es precisamente la que integra esa visualización. Diferirla a `i…` significaría quitar deliberadamente, al integrar, un comportamiento que hoy funciona, para reponerlo después: es una regresión visible para el Product Owner en el punto de control de `g`, y la regla de no regresión de §5.1 empuja en el mismo sentido.
3. **La prioridad no es un ordenador de fases en este roadmap, y ya hay precedente.** F-13 es `Should Have` y está en la fase `g` desde la emisión inicial, por el mismo motivo. La prioridad gobierna **qué se difiere primero si la fase aprieta**, no en qué fase vive la capacidad; `Alcance-Producto.md` §4.2 declara esa distinción. Este punto sostiene la ubicación con independencia de la prioridad, y por eso **sobrevive intacto** a que F-25 haya pasado a `Must Have`.
4. **Ubicarla en `g` la compromete, y así corresponde** [ACTUALIZADO 2026-08-09 contra `PRODUCT-INTAKE` 1.7]. Hasta la versión 1.2 de este roadmap, este punto decía lo contrario: que ubicarla en `g` no la comprometía, porque era `Should Have` y agregarle criterio bloqueante la habría comprometido de hecho. **El Product Owner promovió la capacidad a `Must Have`**, que es exactamente la condición que la versión anterior de §3 declaraba como disparador de este cambio. El fundamento de la promoción es que el movimiento es comportamiento propio del bundle y vive en su mismo bucle de dibujo, y sobre todo que **la órbita de la cámara ya existe en la visualización que la cátedra usa hoy**: diferirla sería portar quitando algo que funciona, o sea una regresión. En consecuencia la transición `g` → `h` de §5.2 **sí** incorpora el gobierno independiente de los dos movimientos como criterio, y la fase `g` no cierra sin él.

El argumento contrario —que una capacidad `Should Have` debería caer en `i…` junto con las demás— se descarta por el punto 1: `i…` agrupa capacidades que necesitan superficie que el tramo comprometido no levanta, como el despliegue real de F-14; F-25 no está en ese caso. La condición declarada para que este fundamento cambiara era que el Product Owner promoviera o degradara la capacidad, o que le asignara etapa explícita en el intake §15. **Esa condición se cumplió**: el intake 1.7 la promueve a `Must Have`, y el punto 4 recoge la consecuencia. La ubicación en `g` no cambia; lo que cambia es que ahora es vinculante.

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
| `h` | `g` | El administrador decide sobre trabajos que ya puede abrir y revisar por completo; sin la visualización de `g` la revisión no tiene sobre qué apoyarse |
| `i…` | `h` | El intake declara que se planifica con la plantilla completa cuando `h` esté cerrada y demostrada |

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
| `d` | `e` | - [ ] Un alumno se registra con correo, nombre y apellido, sin elegir contraseña<br>- [ ] Un alumno cuya cuenta está en estado `Pendiente` recibe un aviso explícito de que todavía no fue habilitada<br>- [ ] El administrador habilita, bloquea, rehabilita y da de baja, y la baja exige confirmación escribiendo el correo de la cuenta<br>- [ ] El alumno habilitado establece su contraseña en su primer ingreso efectivo, sin ningún correo de por medio<br>- [ ] El administrador **resetea** la contraseña de un alumno desde el mismo panel, y el producto le muestra una provisoria **que él no escribió**: el panel no tiene campo de contraseña<br>- [ ] Dos reseteos consecutivos sobre la misma cuenta producen **provisorias distintas**, y ninguna es derivable del nombre, del correo ni de la fecha<br>- [ ] El reseteo procede sobre una cuenta `Bloqueado` y sobre una cuenta `Pendiente`, **sin cambiarles la situación**, y **no procede** sobre la cuenta de administrador<br>- [ ] La cuenta reseteada **se autentica y no obtiene sesión de trabajo**: cualquier ruta que intente termina en el cambio de contraseña, y recién al cambiarla opera con normalidad<br>- [ ] Después del reseteo la cuenta conserva su identidad, su situación y **todos sus trabajos**, verificado sobre un alumno con trabajos en tres estados distintos y con sus comentarios |
| `e` | `f` | - [ ] Un trabajo se carga con nombre, fecha, descripción y texto, y recibe identificador propio y estado<br>- [ ] Un trabajo queda en estado `Borrador` **con el texto inválido** y se reedita<br>- [ ] La eliminación por el alumno sólo procede en estado `Borrador` y sólo sobre trabajos propios, verificado **forzando la petición al servicio de datos**, no sólo por la interfaz<br>- [ ] Un alumno que pide el trabajo de otro recibe «no encontrado»<br>- [ ] El administrador ve los trabajos agrupados y filtrados por alumno, y su listado **no incluye los que están en estado `Borrador`** |
| `f` | `g` | - [ ] Los nueve casos de prueba obligatorios pasan con los escenarios de datos del intake como entrada<br>- [ ] El texto tal como lo emite el programa del alumno se interpreta, con sus particularidades de formato incluidas<br>- [ ] **Enviar** es la única acción de guardado: un envío que verifica pasa el trabajo a estado `Pendiente` y uno que no verifica lo deja en `Borrador` con sus errores localizados<br>- [ ] Un cubo del primer ejemplo produce advertencia de área con los dos valores expresados y el trabajo pasa a estado `Pendiente` igual; el mismo cubo del segundo ejemplo **no** produce ninguna advertencia<br>- [ ] Un tipo desconocido produce error con índice de figura y campo, y el trabajo no pasa a estado `Pendiente`<br>- [ ] La comparación de valores usa tolerancia absoluta y no igualdad exacta<br>- [ ] El texto original se conserva íntegro y nunca se reescribe<br>- [ ] PT-02 y PT-03 medidas antes de comprometer `g` |
| `g` | `h` | - [ ] Las tres figuras del escenario semilla se dibujan, **ortoedro incluido**<br>- [ ] Navegar entre trabajos ida y vuelta diez veces no degrada la visualización<br>- [ ] Procesar el mismo trabajo dos veces produce la misma disposición. **Se predica de la posición de cada pieza, derivada de su índice, no de su orientación en un instante**: el movimiento automático de F-25 no altera la disposición (PRODUCT-INTAKE §17.7 P.10)<br>- [ ] Durante la interacción tridimensional no hay ni una sola petición originada por la visualización<br>- [ ] El árbol y la escena se sincronizan por índice de pieza<br>- [ ] El administrador abre cualquier trabajo que ve y encuentra exactamente lo mismo que vio el alumno<br>- [ ] **Los dos movimientos automáticos de F-25 se gobiernan por separado**: la persona enciende y apaga la órbita de la cámara y el giro de las piezas de forma independiente, los dos se detienen mientras arrastra, y su estado inicial lo fija la pieza pública pasando dos valores de verdad, porque es ella —y no la visualización— la que consulta la preferencia de movimiento reducido del sistema |
| `h` | `i…` | - [ ] El administrador aprueba un trabajo en estado `Pendiente` y queda en `Finalizado`; rechaza otro y queda en `Rechazado`<br>- [ ] El comentario escrito se guarda cuando el administrador lo deja, y los dos desenlaces funcionan **sin** comentario<br>- [ ] Aprobar y rechazar son facultad exclusiva del administrador: un alumno que fuerce la transición contra el servicio de datos es rechazado<br>- [ ] `Finalizado` y `Rechazado` son terminales: ninguna transición sale de ellos y su contenido no cambia<br>- [ ] El alumno ve **el desenlace** de su trabajo en su propio listado, y **el comentario al abrir el trabajo** desde ese listado<br>- [ ] El administrador elimina un trabajo en estado `Pendiente` y el trabajo desaparece<br>- [ ] El alcance comprometido está cerrado: las ocho fases tienen OK explícito |

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
| 1.1 | 2026-08-08 | Absorbe el circuito de revisión del administrador incorporado por el Product Owner en `PRODUCT-INTAKE` 1.3. Sube minor y archiva el estado anterior porque el documento ya es citado como insumo por otras categorías (`Master-Prompt.md` §5). **§2.1**: entra la fase `h`, circuito de revisión del administrador, demostrable, con F-21, F-23 y F-24; el cierre del alcance comprometido se corre de `g` a `h`; los pendientes pasan a `i…` y dejan de citar la exclusión X-5, retirada aguas arriba; se reescriben los objetivos de `e` y de `f` sobre el modelo de estados nuevo. **§2.2**: PT-05 pasa a la fase `i`, con la nota de que la puerta sigue atada al despliegue real y no a la letra. **§3**: la matriz suma la fila `h` y renumera los pendientes. **§4**: entran las dependencias `h` sobre `g` e `i` sobre `h`. **§5.2**: se reescriben las transiciones `e`→`f` y `f`→`g` sobre enviar y estado `Pendiente`, y entra la transición `h`→`i…` con siete criterios del circuito de revisión; la transición `d`→`e` califica la forma desnuda de `Pendiente` en el aviso de cuenta no habilitada. · **Corrección de la ronda r3 del audit, absorbida en esta misma versión.** **H-03**: el enunciado «el alumno ve el desenlace y el comentario en su listado», que este documento transcribía del intake §15, se precisa en sus dos apariciones —§2.1, fase `h`, y §5.2, transición `h`→`i…`— para que el desenlace se vea en el listado y el comentario al abrir el trabajo desde ese listado. No cambia la intención de circuito ni el criterio de transición: cierra la lectura literal que exigiría texto libre dentro del listado, que la capa de contratos prohíbe por diseño para que el panel no arrastre el contenido completo de cada trabajo. | Product Manager Senior (AG-00) |
| 1.2 | 2026-08-09 | Absorbe la capacidad **F-25** incorporada por el Product Owner en `PRODUCT-INTAKE` 1.5 §4, **originada en la validación visual de la Fase B2** de la maqueta de la pieza pública, aprobada tras cuatro iteraciones. **Sube minor y archiva el estado anterior** porque el documento ya es citado como insumo por cinco proyectos de código (`Master-Prompt.md` §5). **§2.1**: la fase `g` suma F-25 a sus capacidades y su entregable declara que el movimiento automático se entrega sobre esa misma superficie si la fase da lugar, sin condicionar el cierre. **§3**: la matriz suma F-25 a la fila `g` y se agrega el fundamento de la ubicación, con sus cuatro razones y con el argumento contrario descartado, porque **el intake no asigna etapa a F-25** y la ubicación es por lo tanto una decisión de planificación de este roadmap. **§5.2**: el criterio de disposición determinista de la transición `g` → `h` se precisa —se predica de la posición derivada del índice, no de la orientación en un instante—, para que el movimiento automático no lo contradiga; la precisión es la que el intake declara en §17.7 P.10 y no cambia el criterio ni su target. **No se agregó ningún criterio de transición nuevo**: F-25 es `Should Have` y agregarle criterio bloqueante la habría comprometido de hecho. Ninguna fase, dependencia ni puerta técnica cambia. | Product Manager Senior (AG-00) |
| 1.3 | 2026-08-09 | **Fila repuesta el 2026-08-09 al cerrar el hallazgo `F26-05`** de `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0: la versión 1.3 se emitió **sin fila de control de cambios**, de modo que el único cambio real que trajo no estaba descrito en ninguna parte del propio documento. Lo que la 1.3 cambió, verificado contra el árbol y no contra lo declarado, es la **promoción de F-25 a `Must Have`** que el Product Owner hizo en `PRODUCT-INTAKE` 1.7, en dos lugares y sólo en dos: **§2.1**, donde el entregable de la fase `g` deja de decir que el movimiento automático se entrega «si la fase da lugar, sin condicionar el cierre» y pasa a decir que **condiciona el cierre**, con el fundamento de que la órbita ya existe en la visualización que la cátedra usa hoy; y **§3 punto 4**, que se reescribe entero —hasta la 1.2 decía que ubicar F-25 en `g` no la comprometía— y pasa a «**Ubicarla en `g` la compromete, y así corresponde**», declarando además que la transición `g` → `h` de §5.2 incorpora el gobierno de los dos movimientos como criterio. **§3 párrafo de cierre** recoge que la condición declarada para revisar el fundamento se cumplió. **La 1.3 no tocó §5.2**, de modo que esa última afirmación quedó sin instrumento; se corrige en la 1.4. | Product Manager Senior (AG-00) |
| 1.4 | 2026-08-09 | **Cierra los hallazgos `F26-04`, `F26-02`, `F26-05` y la fila de `F26-20` que alcanza a este documento**, del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0, contra `PRODUCT-INTAKE` **1.9**. **Sube minor y archiva el estado anterior** porque el documento ya es citado como insumo por cinco proyectos de código (`Master-Prompt.md` §5). **§5.2, transición `g` → `h` (`F26-04`)**: entra el **séptimo criterio**, el gobierno independiente de los dos movimientos automáticos de F-25, con la detención durante el arrastre y con el estado inicial fijado por la pieza pública, que es la que consulta la preferencia de movimiento reducido. Es el instrumento que la versión 1.3 afirmaba haber incorporado en §3 punto 4 **sin haberlo hecho**: la fila tenía seis criterios y ninguno lo mencionaba, y el único que nombraba a F-25 lo hacía para excluirlo del criterio de disposición. Con esta entrada, la afirmación de §3 punto 4 y la de `NB-06` §5 pasan a ser ciertas, y `AB2-04` queda cerrado en el instrumento y no sólo en la conclusión. **§2.1, §3 y §5.2, transición `d` → `e` (`F26-02`)**: entra la capacidad **F-26**, reseteo de contraseña, `Must Have` desde el intake 1.7 y hasta hoy ausente de todo el nivel producto. Se ubica en la fase `d`, con fundamento propio de tres razones y con la constancia de que **el intake §15 no le asigna etapa** —igual que a F-25—, de modo que la ubicación es decisión de planificación de este roadmap; la fase `d` suma la capacidad a su objetivo, a su entregable y a la matriz de §3; y la transición `d` → `e` suma **cinco criterios verificables**: provisoria producida por el sistema con panel sin campo de contraseña, provisorias distintas entre reseteos y no derivables, reseteo que procede sobre `Bloqueado` y `Pendiente` sin cambiarles la situación y que no procede sobre la cuenta de administrador, cuenta reseteada que se autentica sin obtener sesión de trabajo, y conservación de la cuenta y de todos sus trabajos. Los tres últimos derivan de **RN-13**, **RN-15** y **RN-12** del intake 1.9. **§2.1, fase `f` (`F26-20`)**: los escenarios de datos ejercidos pasan de siete a **ocho**, contados `E-1` a `E-8` en el intake §20. **§7 (`F26-05`)**: se repone la fila 1.3, que faltaba. Ninguna dependencia, puerta técnica ni fase cambia de orden. | Product Manager Senior (AG-00) |
