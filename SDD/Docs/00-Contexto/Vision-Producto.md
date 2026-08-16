# Visión de Producto

**Producto:** Fábrica de Geometría
**Documento:** Vision-Producto.md
**Versión:** 1.6
**Estado:** Aprobado
**Fecha:** 2026-08-14
**Autor:** Product Manager Senior (AG-00), actuando también como Analista de Negocio Senior (AG-01) por `Rules-Contexto.md` §1.3
**Trazabilidad upstream:** PRODUCT-INTAKE 1.9 §1 (idea y problema), §2 (audiencia y stakeholders), §3 (propuesta de valor y diferenciación), §4 (alcance funcional pretendido), §4.1 (reglas de negocio declaradas), §4.2 (modelo de estados del trabajo y colisión de vocabulario), §8 (métricas de éxito desde el negocio), §9 (exclusiones), §10 (restricciones del cliente), §11 (riesgos detectados desde el negocio), §12 y §12.1 (glosario del dominio del cliente y choque de vocabulario), §15 (etapas), §22 (supuestos declarados)
**Trazabilidad downstream:** 01-Necesidades-Negocio, 02-Especificacion-Funcional, 03-UX-UI-DX, 05-Arquitectura-Tecnica, 07-Plan-Sprint, 10-Examples

---

## Tabla de contenido

- [1. Problema de negocio](#1-problema-de-negocio)
- [2. Audiencia y stakeholders](#2-audiencia-y-stakeholders)
  - [2.1 Tabla de stakeholders](#21-tabla-de-stakeholders)
  - [2.2 Concentración de roles en una persona](#22-concentración-de-roles-en-una-persona)
- [3. Propuesta de valor](#3-propuesta-de-valor)
  - [3.1 Lo que hay hoy y por qué no alcanza](#31-lo-que-hay-hoy-y-por-qué-no-alcanza)
  - [3.2 Diferenciadores declarados](#32-diferenciadores-declarados)
- [4. Visión a 3 años](#4-visión-a-3-años)
  - [4.1 Lo comprometido](#41-lo-comprometido)
  - [4.2 Lo declarado como candidato](#42-lo-declarado-como-candidato)
  - [4.3 Lo declarado como no previsto](#43-lo-declarado-como-no-previsto)
  - [4.4 Límite del horizonte](#44-límite-del-horizonte)
- [5. Objetivos SMART](#5-objetivos-smart)
- [6. Métricas de éxito](#6-métricas-de-éxito)
- [7. Restricciones](#7-restricciones)
- [8. Riesgos](#8-riesgos)
- [9. Glosario del dominio](#9-glosario-del-dominio)
  - [9.1 Términos del dominio del cliente](#91-términos-del-dominio-del-cliente)
  - [9.2 Términos que esta categoría precisa](#92-términos-que-esta-categoría-precisa)
  - [9.3 Resolución del choque de vocabulario](#93-resolución-del-choque-de-vocabulario)
- [10. Trazabilidad](#10-trazabilidad)
- [11. Control de cambios](#11-control-de-cambios)

---

## 1. Problema de negocio

En la Actividad 1 de la cátedra, el alumno construye una aplicación que modela figuras geométricas planas y volumétricas y las describe en un texto con formato JSON. Para ver ese resultado en tres dimensiones existe hoy una página suelta, separada de todo lo demás, donde el alumno pega el texto a mano (PRODUCT-INTAKE §1).

La cadena completa es hoy: modelar, copiar el texto, pegarlo en una página aparte y mirarlo. Esa cadena **no tiene identidad, no tiene persistencia y no tiene entrega**. El trabajo del alumno vive en un portapapeles: no queda guardado, no tiene dueño, no tiene estado, y el docente no puede revisarlo salvo mirando la pantalla del alumno. Si el alumno cierra la página, no queda nada (PRODUCT-INTAKE §1).

A eso se suma un problema de fondo verificado numéricamente sobre los ejemplos de la propia cátedra: hay valores calculados que el programa del alumno emite mal en dos casos concretos y reproducibles —el área del cubo y el volumen del ortoedro— y **nada en la cadena actual se lo señala**. La página de visualización tampoco ayuda: exige una clave que el programa del alumno no emite, de modo que ningún ortoedro generado por la aplicación se dibuja, y falla en silencio, sin mensaje (PRODUCT-INTAKE §1).

Consecuencia si el producto no se construye: la Actividad 1 sigue terminando en un texto que se pega en una página suelta. El docente sigue sin poder revisar entregas y el error de fórmula del alumno sigue sin hacerse visible sobre su propio trabajo, que es exactamente donde tendría valor didáctico (PRODUCT-INTAKE §1).

## 2. Audiencia y stakeholders

La audiencia del producto son dos personas concretas del aula: **el alumno de la comisión**, que carga y entrega su trabajo, y **el docente**, que administra las cuentas y revisa las entregas. No hay integradores externos, no hay áreas de auditoría, legal ni soporte, y no hay actores indirectos: el alcance es un laboratorio de aula (PRODUCT-INTAKE §2).

### 2.1 Tabla de stakeholders

| Rol | Nombre o cargo | Categoría | Nivel de involucramiento | Responsabilidad principal |
|---|---|---|---|---|
| Product Owner | El docente de Programación 2 (TUP), responsable de la cátedra y de la Actividad 1 | Propietario | Permanente y decisorio | Aprueba el intake, arbitra prioridades y exclusiones, y valida cada punto de control de etapa (PRODUCT-INTAKE §2) |
| Dueño del problema | La cátedra de Programación 2 | Propietario | Permanente, consultivo | Padece la falta de entrega y de revisión; decide el rumbo del laboratorio (PRODUCT-INTAKE §2) |
| Equipo de desarrollo | 1 persona: el mismo docente, asistido por un agente de IA | Implementador | Permanente y ejecutor | Construye y mantiene el producto. El agente desarrolla por etapas; la persona valida y fusiona (PRODUCT-INTAKE §2) |
| Alumno de la comisión | Alumno cursante de Programación 2 | Beneficiario | Por cursada, alto en las entregas | Carga sus trabajos con el texto que produjo su Actividad 1, los previsualiza en 3D y los entrega (PRODUCT-INTAKE §2) |
| Administrador del laboratorio | El mismo docente, con la cuenta única de administrador | Beneficiario y operador | Permanente durante la cursada | Autoriza, bloquea y da de baja cuentas; revisa, filtra y agrupa los trabajos de todos los alumnos (PRODUCT-INTAKE §2) |

Las tres categorías de la tríada están representadas: propietario (Product Owner y cátedra), implementador (equipo de desarrollo de una persona) y beneficiario (alumno y administrador).

### 2.2 Concentración de roles en una persona

Product Owner, responsable técnico, único desarrollador humano y administrador del sistema **son la misma persona**. Las filas no se fusionan porque las responsabilidades sí son distintas, y el punto de control de cada etapa es explícitamente el momento en que el docente cambia de papel: valida como cliente lo que construyó como equipo (PRODUCT-INTAKE §2).

El arbitraje entre esos papeles está declarado aguas arriba y no se decide en esta categoría: lo ejerce el Product Owner en el punto de control de cada etapa.

La cantidad de personas del equipo de desarrollo es **1**. El agente de IA no es una persona del equipo (PRODUCT-INTAKE §2). De ese valor se sigue la omisión de `Acuerdo-Equipo.md`, declarada en el `README.md` de esta sección.

## 3. Propuesta de valor

### 3.1 Lo que hay hoy y por qué no alcanza

El circuito «modelar, copiar, pegar, mirar» funciona para ver una figura, pero **no produce una entrega**: no hay usuario, no hay trabajo, no hay estado ni historial (PRODUCT-INTAKE §3).

Promesa central del producto: **el trabajo del alumno queda guardado, tiene dueño, tiene estado y se entrega**, sin pedirle al alumno que cambie una coma del texto que ya produce su programa (PRODUCT-INTAKE §3).

### 3.2 Diferenciadores declarados

| Id | Diferenciador | Por qué importa al negocio |
|---|---|---|
| D-1 | El texto del alumno se acepta **tal como lo emite su programa**, con sus particularidades de formato incluidas | El producto se adapta al dato del alumno, nunca al revés. El alumno no pierde tiempo arreglando su salida para que la herramienta la acepte (PRODUCT-INTAKE §3) |
| D-2 | El producto **señala** las discrepancias entre el valor que declara el alumno y el que se deriva de las dimensiones, sin corregirlas ni rechazarlas | Es el mayor valor didáctico del producto: el alumno ve, sobre su propio trabajo, que su cubo declara 36.00 donde la geometría dice 54.00 (PRODUCT-INTAKE §3) |
| D-3 | **Los ortoedros se dibujan**, cosa que hoy no ocurre con ninguno generado por la aplicación del alumno | Elimina un fallo silencioso que hoy deja al alumno sin explicación de por qué su figura no aparece (PRODUCT-INTAKE §3) |
| D-4 | Previsualización 3D y árbol de la estructura **dentro** del producto, sobre el trabajo cargado, para el alumno y para el administrador | Corrige el corte entre modelar y ver, que hoy obliga a salir a una página aparte (PRODUCT-INTAKE §3) |
| D-5 | El administrador ve, filtra y agrupa los trabajos por alumno, sin pedirle a nadie que le mande nada | Convierte la revisión de la comisión en una sola sesión de trabajo del docente (PRODUCT-INTAKE §3). Con el modelo de estados vigente, el listado del administrador no incluye los trabajos en estado `Borrador`: no forman parte de su flujo de trabajo (PRODUCT-INTAKE §4.1, RN-02011) |
| D-6 | La entrega **tiene desenlace**: el administrador aprueba o rechaza cada trabajo recibido, y puede dejar un comentario escrito | Cierra el circuito. Sin desenlace explícito, la entrega quedaba sólo depositada y el alumno no sabía si su trabajo había sido aceptado (PRODUCT-INTAKE §5 historia 7.1, §6 flujo 2.1) |

## 4. Visión a 3 años

El intake declara explícitamente que **no hay fecha objetivo** y que el avance se mide por etapas cerradas (PRODUCT-INTAKE §10). En consecuencia, el horizonte de esta sección se expresa por grado de compromiso declarado, no por calendario.

### 4.1 Lo comprometido

Que la Actividad 1 de la cátedra deje de terminar en un portapapeles y termine en una entrega **con desenlace**: el alumno se registra, es habilitado por el docente, carga su trabajo, lo previsualiza, ve las advertencias sobre sus propios valores calculados y lo envía; el docente revisa la comisión desde un solo lugar y **aprueba o rechaza cada trabajo recibido**, con un comentario escrito si lo considera necesario (PRODUCT-INTAKE §4, capacidades Must Have F-01 a F-12, **F-13**, F-21 a F-24, **F-25 y F-26**).

Tres de esas diecinueve capacidades entraron al alcance comprometido después de la emisión inicial y conviene nombrarlas. El 2026-08-09: **F-26**, que le da al docente una forma de resolver el olvido de contraseña **sin dar de baja la cuenta**, y **F-25**, el movimiento automático de la escena, que se promovió a comprometida porque la órbita de la cámara ya existe en la visualización que la cátedra usa hoy y entregar el producto sin ella sería una regresión. El 2026-08-10: **F-13**, la sincronización entre el árbol y la escena por índice de pieza y la disposición determinista entre procesados, promovida porque la puerta técnica que precede a la etapa de visualización **ya medía las dos propiedades**, de modo que la capacidad era diferible en el papel e indiferible en los hechos (PRODUCT-INTAKE **1.19**).

El circuito de revisión del administrador es incorporación del 2026-08-08 y forma parte del alcance comprometido, no del horizonte: sin él, la entrega quedaba depositada y nadie cerraba el ciclo.

### 4.2 Lo declarado como candidato

El intake declara un conjunto de capacidades como candidatas para después del cierre del alcance comprometido, sin comprometerlas: el despliegue real verificado desde la red de la facultad; un panel de resumen con la cantidad de trabajos por alumno y por estado; la exportación del trabajo; y el modo despiece (PRODUCT-INTAKE §4, capacidades **F-14 a F-17**; §9, exclusiones X-6 y X-7 declaradas como candidatas). Se planifican cuando la etapa `h` esté cerrada y demostrada (PRODUCT-INTAKE §15, etapa `i…`).

**La sincronización entre el árbol y la escena y la disposición estable de las figuras estuvieron en esta lista hasta el 2026-08-10 y ya no están**: son F-13, que el Product Owner promovió a `Must Have` en `PRODUCT-INTAKE` **1.19** y que pasó a §4.1 con el resto del alcance comprometido. Es el segundo caso de esta clase después de F-25, y las cuatro que quedan tienen todas la misma condición y no aquélla: **necesitan superficie que el tramo comprometido no levanta**, mientras que F-13 vivía sobre la superficie de una etapa comprometida y su puerta técnica ya la medía.

### 4.3 Lo declarado como no previsto

El correo y todo lo que depende de él, los múltiples administradores con permisos finos, la edición del texto del alumno desde el producto y el segundo factor de autenticación están declarados fuera del producto sin fecha de reingreso (PRODUCT-INTAKE §9, exclusiones X-1 a X-4 y X-8). Sigue fuera, además, la **calificación con nota o escala**: lo que entró al alcance comprometido es el comentario escrito, que es texto libre y no lleva nota (PRODUCT-INTAKE §4, F-21, y §9, exclusión X-5 retirada). El detalle y la justificación de cada una viven en `Alcance-Producto.md` §5.

### 4.4 Límite del horizonte

Más allá de lo enumerado, el intake no declara compromiso alguno. Extender la visión a un horizonte de calendario de tres años sería originar una decisión de producto en esta categoría, y eso le corresponde al Product Owner. La sección se cierra acá deliberadamente.

## 5. Objetivos SMART

Los cuatro objetivos derivan de las métricas de negocio del intake. Sus valores objetivo están rotulados como asunción pendiente de confirmación del Product Owner (PRODUCT-INTAKE §8 y §22, asunción A-2): están completos y son numéricos, y se usan como tales, pero un cambio del Product Owner los reemplaza sin discusión.

| Objetivo | Métrica | Target numérico | Plazo | Responsable |
|---|---|---|---|---|
| OBJ-01 · Cerrar el producto comprometido etapa por etapa | Etapas cerradas con OK explícito en su punto de control, sobre las **8** comprometidas (`a` a `h`) | **8 de 8** | Sin plazo de calendario: el avance se mide por etapas cerradas (PRODUCT-INTAKE §10) | Product Owner, en cada punto de control |
| OBJ-02 · Que el alumno entregue | Alumnos habilitados que llegan a tener al menos un trabajo en estado `Pendiente` o posterior, sobre el total de alumnos habilitados | ≥ 80 % | Al cierre de la cursada en que se use por primera vez | Administrador del laboratorio |
| OBJ-03 · Que ninguna entrega quede sin revisar | Trabajos en estado `Pendiente` que reciben desenlace —`Finalizado` o `Rechazado`— sobre el total de trabajos que llegaron a estado `Pendiente` | 100 % | Al cierre de la cursada en que se use por primera vez | Administrador del laboratorio |
| OBJ-04 · Entregar el valor didáctico efectivamente | Advertencias de valor declarado contra valor derivado que el producto muestra sobre trabajos reales de alumnos | ≥ 1 advertencia visible por alumno que cargue un cubo del primer ejemplo de la cátedra o un ortoedro | Primera entrega de la cursada | Administrador del laboratorio |

**Por qué OBJ-02 y OBJ-03 son dos objetivos y no uno.** Hasta la incorporación del circuito de revisión, una sola métrica medía «trabajos que llegan a `Finalizado`». Con el modelo de estados vigente, `Finalizado` dejó de significar «el alumno cerró su entrega» y pasó a significar «el administrador lo aprobó», de modo que esa métrica, sin que nadie la tocara, había pasado de medir el comportamiento del alumno a medir el trabajo del docente. La partición devuelve cada objetivo a quien depende de él: OBJ-02 mide lo que hace el alumno y corta en estado `Pendiente`, que es el primer estado que expresa una entrega efectiva; OBJ-03 mide lo que hace el administrador y su target es 100 % porque un trabajo entregado y nunca revisado es exactamente el problema que el producto viene a resolver (PRODUCT-INTAKE §8).

Los cuatro objetivos son compatibles entre sí: OBJ-01 mide construcción, OBJ-02 mide adopción del alumno, OBJ-03 mide cierre del circuito por el administrador y OBJ-04 mide efecto didáctico. Ninguno se cumple a costa de otro.

**El punto abierto sobre el alcance de OBJ-01 quedó resuelto.** Hasta la versión 1.1 este apartado declaraba escalada al Product Owner la ambigüedad de si el objetivo contaba siete etapas (`a` a `g`) u ocho (`a` a `h`), y no la resolvía. **El Product Owner la resolvió**, y su decisión está en PRODUCT-INTAKE §8: «**8 de 8** [DECISIÓN 2026-08-09: se cuentan todas las comprometidas, no siete. Con siete, el producto podía declararse completo con una etapa comprometida sin construir]». El target de OBJ-01 y el de la métrica de avance de §6 pasan a contar ocho, con lo que vuelven a coincidir con las ocho fases que `Roadmap-Producto.md` deriva del intake §15. **La fuente ya no se contradice a sí misma**: hasta la versión 1.2 este apartado dejaba constancia de que la fila A-2 de PRODUCT-INTAKE §22 todavía transcribía «7 de 7 etapas» contra el §8 que decía ocho. El intake **1.10** la corrigió, y §22 A-2 dice hoy «8 de 8 etapas» con la constancia de qué decía antes y por qué. Verificado sobre el texto vivo del intake.

## 6. Métricas de éxito

| Criterio | Métrica | Target | Plazo | Fuente del dato |
|---|---|---|---|---|
| Avance del producto | Etapas cerradas con OK explícito del Product Owner en su punto de control, sobre las **8** comprometidas | **8 de 8**, resuelto por el Product Owner el 2026-08-09 (§5) | Sin plazo de calendario (PRODUCT-INTAKE §10) | Informe de cierre de etapa y su índice, más el registro del punto de control de cada etapa (PRODUCT-INTAKE §15, reglas de delivery 2 y 3) |
| Entrega del alumno | Alumnos habilitados con al menos un trabajo en estado `Pendiente` o posterior, sobre el total de alumnos habilitados | ≥ 80 % | Al cierre de la primera cursada de uso | Listado de trabajos del administrador, agrupado por alumno, y estado de las cuentas (capacidades F-08, F-12 y F-03) |
| Aprobación del administrador | Trabajos en estado `Pendiente` que reciben desenlace, sobre el total de trabajos que llegaron a estado `Pendiente` | 100 % | Al cierre de la primera cursada de uso | Estados de los trabajos en el listado del administrador, que distingue `Pendiente`, `Finalizado` y `Rechazado` (capacidades F-12 y F-23) |
| Valor didáctico entregado | Advertencias de valor declarado contra derivado mostradas sobre trabajos reales | ≥ 1 por alumno que cargue un cubo del primer ejemplo o un ortoedro | Primera entrega de la cursada | Advertencias que el producto registra y muestra sobre cada trabajo (capacidad F-10) |

Las cuatro fuentes de dato son internas al producto o al proceso de construcción, y por lo tanto obtenibles sin instrumentación adicional. La verificación efectiva de estas métricas es responsabilidad de la categoría 08.

Las métricas técnicas —latencia, cobertura, umbrales de las puertas técnicas— no se mezclan acá: viven en el intake por proyecto de código y bajan a las categorías 05, 08 y 09 (PRODUCT-INTAKE §8, nota de cierre).

## 7. Restricciones

Todas derivan de PRODUCT-INTAKE §10 y se expresan acá en lenguaje de negocio. Su tratamiento operativo pertenece a las categorías 05 y 09.

| Id | Restricción | Efecto sobre el producto |
|---|---|---|
| R-01 | **Sin fecha objetivo**, justificado: el avance se mide por etapas cerradas. El ritmo lo fija el punto de control de cada etapa, que es un cuello por diseño | El roadmap se ordena por etapa cerrada y demostrada, nunca por calendario |
| R-02 | **Sin presupuesto monetario asignado.** Los tres recursos de infraestructura son de costo cero declarado; no hay compra de licencias ni de servicios en el alcance | Ninguna decisión de producto puede apoyarse en un servicio pago |
| R-03 | **La red de la facultad bloquea el acceso a direcciones dinámicas.** Es la restricción que ordena toda la forma del producto | El producto se entrega en **dos piezas desplegables**: la **pieza pública**, que es la que la persona usa, vive donde la facultad no la bloquea, y la **pieza de datos** vive donde los datos persisten |
| R-04 | **El lugar público donde vive la pieza pública no conserva los datos** | Los datos no pueden vivir ahí. Es la contracara de R-03 y, junto con ella, hace inviable entregar el producto en una sola pieza desplegable |
| R-05 | **El servidor propio no tiene dirección fija.** Se admite apuntar a la dirección directa | Un cambio de dirección obliga a volver a publicar la pieza pública. Es un costo operativo aceptado |
| R-06 | **El formato del texto de entrada no es negociable.** Lo produce el alumno con su Actividad 1 y su formato es el que está | El producto se adapta al dato del alumno; nunca se le pide al alumno que cambie su salida |
| R-07 | **El despliegue lo ejecuta el docente, a mano** | No hay entrega automática al servidor propio. Las etapas se cierran con demostración, no con publicación automática |
| R-08 | **Etapas en serie.** No se empieza una etapa antes de cerrar la anterior, y sin OK explícito no se avanza | El roadmap no admite paralelismo entre fases |
| R-09 | **No aplica ninguna normativa de cumplimiento.** Es un laboratorio de aula, con cuentas creadas para la materia | No hay requisitos regulatorios que condicionen el alcance |

## 8. Riesgos

**Siete riesgos: los seis primeros derivan de PRODUCT-INTAKE §11** con su probabilidad, impacto y mitigación declaradas. **`RG-07` no viene del intake y es el primero que no viene de ahí**: lo levantó la verificación del panel de la cuenta del hosting del 2026-08-13, registrada en [`Compatibilidad-Plataformas.md`](Compatibilidad-Plataformas.md) §2.6.2, y se anota acá porque **éste es el registro de riesgos del producto** y el riesgo es de continuidad del servicio, no de plataforma. La columna de responsable no está declarada como tal en el intake y **no se decide acá**: se deriva mecánicamente de PRODUCT-INTAKE §2, donde el equipo es de una sola persona que ejerce los tres papeles; lo que se declara es cuál de esos papeles responde por cada riesgo.

| Id | Riesgo | Probabilidad | Impacto | Mitigación | Responsable |
|---|---|---|---|---|---|
| RG-01 | Los alumnos no pueden alcanzar el producto desde la red de la facultad, que es el escenario de uso previsto. Es el riesgo que motiva la forma entera del producto | Media | Alto: sin acceso, el laboratorio no existe | Verificación de campo desde la facultad (puerta técnica PT-05). No conviene relegarla al final: cuanto antes se mida, más barato es reaccionar | Equipo de desarrollo |
| RG-02 | El lugar público gratuito no sostiene la pieza pública: no arranca, no establece la sesión interactiva, o recicla el proceso cada pocos minutos | Media | Alto: obliga a cambiar el modelo de la pieza pública, con costo de rediseño | Medición temprana en la primera etapa, en sus cuatro partes por separado, porque fallan por separado. Salidas alternativas ya documentadas | Equipo de desarrollo |
| RG-03 | La validación se construye sin leer el análisis del dato real y termina rechazando el texto que los alumnos efectivamente producen | Alta si no se controla | Alto: el producto no sirve para el dato que existe | Batería obligatoria de nueve casos de prueba con datos verificados, y los **ocho** escenarios de datos del intake —`E-1` a `E-8` de §20— como material fijo de prueba | Equipo de desarrollo |
| RG-04 | El servidor propio se cae, por corte de luz o de conexión, durante una clase | Media | Medio: la clase pierde el laboratorio ese día | Estado degradado explícito y visible. No hay alta disponibilidad ni la va a haber: es un laboratorio de aula | Administrador del laboratorio |
| RG-05 | Las credenciales de los alumnos viajan sin cifrar en el tramo interno entre la pieza pública y la pieza de datos | Alta: es el diseño actual | Alto en confidencialidad | **Aceptado por escrito** por el Product Owner: el alcance es de aula y las cuentas se crean para la materia. Hay una salida documentada y deliberadamente no adoptada | Product Owner |
| RG-06 | El alumno pierde su trabajo por olvidar la contraseña, sin canal de recuperación | **Baja desde el 2026-08-09** | **Bajo**: el olvido ya no cuesta ni la cuenta ni los trabajos | **El riesgo dejó de tener la forma con la que se declaró.** Hasta el 2026-08-09 su mitigación era una advertencia: el docente recreaba la cuenta y la baja arrastraba los trabajos, y eso estaba declarado como consecuencia aceptada de X-1 y X-2. La capacidad **F-26** lo resuelve en el producto: el administrador resetea la credencial desde su panel, el alumno cambia la provisoria en su próximo ingreso y **la cuenta y todos sus trabajos se conservan**; X-2 quedó retirada. Lo que subsiste, y es lo que mantiene vivo el riesgo con impacto bajo, es que **no hay recuperación autónoma**: sin correo, el alumno depende de que el docente esté disponible para resetearle la clave. **Dar de baja para recuperar una cuenta dejó de ser el procedimiento del producto** | Administrador del laboratorio |
| RG-07 | **El proceso del hosting recicla la aplicación cada tanto, y con ella se pierde todo lo que viva en memoria.** Observado por el Product Owner en uso real de la cuenta durante más de un año. **No contradice la medición de `PT-01.c`**, que sostuvo veinte minutos con un solo circuito: son escalas distintas —no recicla en veinte minutos, y sí recicla eventualmente—. **[A CONFIRMAR, no verificado]**: el panel declara además que el plan gratuito da de baja el sitio sin al menos cinco visitas mensuales, y que habría un límite de usuarios simultáneos; ninguna se comprobó y el Product Owner no las conocía | Alta | **Bajo para el laboratorio, por una decisión previa.** El almacén de trabajos vive en el **servicio de datos, que corre en un servidor propio y no en el hosting** — decisión que el Product Owner tomó por este motivo. Lo que el reciclado se lleva es la sesión interactiva del front, que se rearma sola | Ninguna hace falta hoy. **La condición que la haría falta**: que alguna parte del producto pase a guardar estado en la memoria del front. Mientras el estado viva en el servicio de datos, el reciclado es transparente para el alumno | Administrador del laboratorio |

## 9. Glosario del dominio

Este es el **glosario raíz de la cadena de trazabilidad**. Las categorías 02 y 03 referencian sus términos en lugar de redefinirlos.

### 9.1 Términos del dominio del cliente

| Término | Definición | Sinónimos y notas |
|---|---|---|
| Trabajo | Unidad que carga el alumno: nombre, fecha, descripción y el texto con el conjunto de piezas. Tiene identificador propio y estado | Es lo que el alumno entrega en el laboratorio. En esta documentación la palabra «trabajo» a secas designa siempre esta unidad; el esfuerzo de construcción se nombra «tarea» o «etapa», nunca «trabajo» |
| Pieza | Cada figura del conjunto raíz del trabajo. Su identidad es su posición en ese conjunto, porque el dato no trae identificador propio | «Figura» en el vocabulario del análisis del ecosistema previo. Es el referente del dominio; el término tiene un segundo referente declarado en §9.2, y ese segundo se escribe siempre calificado |
| Laboratorio | Nombre corriente con el que la cátedra nombra a este producto en uso: «entrar al laboratorio», «administrador del laboratorio». Designa al mismo producto cuyo `Nombre-Producto` es Fábrica de Geometría | No confundir con el calificador «de aula», que no nombra al producto sino que acota su alcance: «es un laboratorio de aula» quiere decir que el alcance es el de un aula |
| Observación | Término **superordinado** de lo que el producto emite al interpretar el texto del alumno. Agrupa dos especies con efectos distintos sobre la finalización del trabajo: la **advertencia**, que no impide finalizar, y el **error de validación**, que sí lo impide | Es el vocabulario del upstream, donde el dato lleva severidad. Cuando el enunciado se refiere a una discrepancia entre valor declarado y derivado, corresponde el término específico «advertencia», no el superordinado |
| Componente | Figura plana que forma parte de una pieza: tapa, cara, base, lateral o lado | — |
| Actividad 1 | Trabajo práctico de la cátedra en el que el alumno modela figuras y las describe como texto. Es el **emisor** del dato que consume este producto, y no forma parte del producto | — |
| `Describir()` | Método que cada figura de la Actividad 1 implementa y que devuelve su representación como texto. Su salida no cumple estrictamente el formato JSON | Es vocabulario de la cátedra, no del producto |
| Advertencia | Discrepancia entre un valor declarado en el texto del alumno y el derivado de las dimensiones. **No impide que el trabajo pase a estado `Pendiente`** | Es una de las dos especies de «observación». Se opone a «error de validación» |
| Error de validación | Defecto que impide interpretar el texto como figuras. **Impide que el trabajo pase a estado `Pendiente`**: el trabajo queda en `Borrador` con sus errores localizados, y el alumno corrige y vuelve a enviar | Es la otra especie de «observación». Se opone a «advertencia» |
| Estado del trabajo | Conjunto cerrado de cuatro valores por los que pasa un trabajo: `Borrador`, `Pendiente`, `Finalizado` y `Rechazado`. `Borrador` significa que el texto todavía no verifica o que el trabajo recién se creó, y es el único estado que el alumno edita y elimina; `Pendiente` significa enviado con el texto interpretado sin errores, a la espera de revisión; `Finalizado` significa aprobado por el administrador; `Rechazado` significa rechazado por el administrador | `Finalizado` y `Rechazado` son **terminales**: ningún trabajo sale de ellos. Corregir un rechazo significa cargar un trabajo nuevo. El administrador no ve los trabajos en `Borrador` |
| Enviar | La **única** acción de guardado del alumno. Interpreta el texto y decide el estado: `Pendiente` si verifica, `Borrador` si no. **No existe una acción separada de «guardar sin enviar»** | Por eso `Borrador` significa exactamente «el texto no verificó»: un texto que verifica no puede quedarse en borrador |
| Aprobar / Rechazar | Las dos decisiones del administrador sobre un trabajo en estado `Pendiente`, y su **facultad exclusiva**. Aprobar equivale a cerrar el trabajo y lo pasa a `Finalizado`; rechazar lo pasa a `Rechazado` | El alumno no ejerce ninguna de las dos, ni siquiera sobre sus propios trabajos |
| Comentario | Texto libre y **opcional** que el administrador deja al aprobar o al rechazar un trabajo | **No es una calificación**: no lleva nota ni escala. **No es una observación**: las observaciones las emite el producto al interpretar el texto, y el comentario lo escribe una persona. Tampoco se confunde con los comentarios que el producto tolera **dentro** del texto del alumno, que son sintaxis del dato de entrada y no tienen relación con este término |
| Valor declarado / valor derivado | El que trae el texto del alumno / el que el producto recalcula desde las dimensiones de la figura | El par completo es lo que hace visible el error de fórmula |
| Tapa | Cada uno de los dos círculos que cierran un cilindro. En el ortoedro, la misma palabra se usa —erróneamente— para las bases, y ese error es el que hoy impide que los ortoedros se dibujen | Polisemia del dominio del emisor, no del producto |
| Rectángulo desarrollado | Superficie lateral del cilindro desenrollada. El nombre no lo sugiere, y es una trampa clásica para quien consume el dato | — |
| Coma final | Coma antes del cierre de un conjunto. La emite el programa del alumno y el formato estricto la rechaza. Se tolera por diseño | — |
| Fallo silencioso | Error que no produce mensaje: en la página actual, la figura simplemente no aparece. Es lo que este producto viene a eliminar | — |
| Hito interno | Etapa que valida el equipo y que no se muestra al cliente | Se opone a «hito demostrable» |
| Hito demostrable | Etapa que se ejecuta y se recorre delante del cliente | Se opone a «hito interno» |
| Punto de control | Detención obligatoria al cerrar una etapa, a la espera del OK explícito del Product Owner. No se avanza sin él | — |

### 9.2 Términos que esta categoría precisa

Se declaran acá porque aparecen en más de uno de los artefactos de esta categoría.

| Término | Definición | Sinónimos y notas |
|---|---|---|
| Etapa | Cada uno de los tramos `a` a `h` en que el intake descompone la construcción del producto, cada uno con su punto de control | Es el término del intake |
| Fase del roadmap | En `Roadmap-Producto.md`, cada fase **es** una etapa del intake, con el mismo identificador de letra. No es una agrupación distinta ni una unidad nueva | Se usa «fase» sólo donde la estructura del documento lo exige; el referente es siempre la etapa |
| Puerta técnica | Verificación de viabilidad, medida en un momento declarado, que condiciona la planificación. Una puerta que no pasa **detiene** la planificación de lo que depende de ella; no se arrastra como deuda | Identificadas PT-01 a PT-05 en el intake |
| Capacidad | Cada ítem del alcance funcional pretendido del intake, con identificador `F-XX` y prioridad declarada | No es sinónimo de caso de uso: los casos de uso los deriva la categoría 02 |
| `Pendiente`, forma calificada obligatoria | El término nombra **dos** estados distintos: el de una **cuenta** —registrada y todavía no habilitada por el administrador— y el de un **trabajo** —enviado, con el texto interpretado sin errores, a la espera de revisión—. Los dos sentidos conviven en las mismas secciones, así que en toda la documentación generada se escribe **siempre calificado**: «cuenta `Pendiente`» o «trabajo en estado `Pendiente`». **La forma desnuda no se usa** | Regla declarada aguas arriba en PRODUCT-INTAKE §4.2 y vinculante para toda la documentación generada. Es una colisión real y no un falso positivo: el mismo párrafo puede hablar de una cuenta que espera habilitación y de un trabajo que espera revisión, y la forma desnuda no las separa |
| Pieza, en su segundo referente | Cada uno de los dos artefactos del producto que se despliegan por separado y que la persona o los datos consumen: la **pieza pública**, que es lo único que toca el navegador, y la **pieza de datos**, que es lo único que toca los datos. **Este referente se escribe siempre calificado**: «pieza pública», «pieza de datos», o «piezas desplegables» en su forma colectiva. La forma desnuda «pieza» queda reservada al referente del dominio de §9.1 | Corresponde exactamente a lo que `Vocabulario-Rules.md` §2 llama **unidad de entrega**, delimitada por poder desplegarse de forma independiente. Se lo nombra así, y no con el término normativo, porque los documentos de visión y de alcance se redactan en lenguaje de negocio; la equivalencia queda declarada acá para que las categorías aguas abajo la resuelvan sin ambigüedad. Los sinónimos informales «mitad» y «parte» **no se usan** para este referente |

### 9.3 Resolución del choque de vocabulario

El intake declara un choque entre el dominio y el vocabulario normativo del framework, y su resolución es **vinculante para toda la documentación generada** (PRODUCT-INTAKE §12.1):

- **«Proyecto de código»** designa exclusivamente una unidad de compilación de este producto.
- **La palabra «proyecto» a secas no se usa.**
- Las dos unidades de la Actividad 1 que emiten el dato se nombran siempre por su nombre propio, `Ejemplo1` y `Ejemplo2`, y nunca con la forma desnuda del término normativo. No forman parte de este producto: son el emisor del dato.

Los otros cinco términos normativos —producto, unidad de entrega, módulo, solución de código y proyecto de código— no chocan con el dominio del cliente: ninguno de ellos designa nada en el vocabulario de la cátedra.

Precisión sobre **«unidad de entrega»**, que es donde el choque podría parecer que existe y no existe. El término normativo se delimita por poder desplegarse de forma independiente (`Vocabulario-Rules.md` §2). Con esa frontera, las unidades de entrega de este producto son las dos piezas desplegables declaradas en §9.2, y el Trabajo del alumno no es ninguna: es un registro de datos, no se despliega. Por eso el enunciado del Trabajo en §9.1 **no usa el término normativo**, y por eso esta fila no declara choque: el dominio del cliente no le da a «unidad de entrega» ningún sentido propio.

## 10. Trazabilidad

| Contenido de este documento | Origen upstream | Destino downstream |
|---|---|---|
| §1 Problema de negocio | PRODUCT-INTAKE §1 | 01-Necesidades-Negocio |
| §2 Audiencia y stakeholders | PRODUCT-INTAKE §2 | 01-Necesidades-Negocio, 03-UX-UI-DX |
| §3 Propuesta de valor | PRODUCT-INTAKE §3, §5 (historia 7.1), §6 (flujo 2.1) | 01-Necesidades-Negocio, 02-Especificacion-Funcional |
| §4 Visión a 3 años | PRODUCT-INTAKE §4, §9, §15 | 01-Necesidades-Negocio, 07-Plan-Sprint |
| §5 Objetivos SMART | PRODUCT-INTAKE §8, §22 (A-2) | 01-Necesidades-Negocio, 08-Calidad-Y-Pruebas |
| §6 Métricas de éxito | PRODUCT-INTAKE §8, §15 | 08-Calidad-Y-Pruebas |
| §7 Restricciones | PRODUCT-INTAKE §10 | 05-Arquitectura-Tecnica, 09-Devops |
| §8 Riesgos | PRODUCT-INTAKE §11, §2; y para `RG-07`, la verificación del panel del hosting registrada en `Compatibilidad-Plataformas.md` §2.6.2 | 05-Arquitectura-Tecnica, 07-Plan-Sprint, 09-Devops |
| §9 Glosario del dominio | PRODUCT-INTAKE §12, §12.1, §4.2 (modelo de estados y colisión de `Pendiente`) | 02-Especificacion-Funcional, 03-UX-UI-DX, 10-Examples |

Documentos hermanos de esta categoría: `Alcance-Producto.md`, `Roadmap-Producto.md` y `Compatibilidad-Plataformas.md`.

## 11. Control de cambios

| Versión | Fecha | Cambios | Autor |
|---|---|---|---|
| 1.0 | 2026-08-08 | Emisión inicial. Formaliza el problema, la audiencia, la propuesta de valor, el horizonte por grado de compromiso, tres objetivos SMART derivados de las métricas de negocio del intake, sus fuentes de dato, nueve restricciones, seis riesgos con responsable derivado del equipo de una persona, y el glosario raíz de la cadena con la resolución del choque de vocabulario del término «proyecto». | Product Manager Senior (AG-00) |
| 1.0 | 2026-08-08 | Correcciones absorbidas del audit A-00-01-r1, sin subir versión por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **H-01**: §9.2 recibe la entrada de «pieza» en su segundo referente, con la forma calificada obligatoria y la correspondencia declarada con el término normativo «unidad de entrega»; §9.1 remite a ella desde la entrada del referente del dominio; se califican las cuatro ocurrencias desnudas de §7 R-03, §7 R-04, §8 RG-02 y §8 RG-05, y el tercer referente de §7 R-02 pasa a «los tres recursos de infraestructura». **H-02**: la entrada «Trabajo» de §9.1 deja de usar el término normativo «unidad de entrega» y §9.3 declara por qué esa fila no tiene choque con la frontera de `Vocabulario-Rules.md` §2. **H-03**: §9.1 da de alta «Observación» como superordinado de «advertencia» y «error de validación». **H-06**: §9.1 da de alta «laboratorio» como nombre corriente del producto, distinto del calificador «de aula». | Product Manager Senior (AG-00) |
| 1.1 | 2026-08-08 | Absorbe el circuito de revisión del administrador incorporado por el Product Owner en `PRODUCT-INTAKE` 1.3. Sube minor y archiva el estado anterior porque el documento ya es citado como insumo por otras categorías (`Master-Prompt.md` §5). **§3.2**: D-5 precisa que el listado del administrador no incluye los trabajos en `Borrador` (RN-02011) y entra D-6, el desenlace explícito de la entrega. **§4.1**: el alcance comprometido incorpora F-21 a F-24. **§4.2**: los candidatos se planifican en la etapa `i…` y ya no citan la exclusión X-5, retirada aguas arriba. **§4.3**: lo que sigue excluido es la calificación con nota o escala, no el comentario. **§5 y §6**: las métricas de negocio pasan de tres a cuatro, con la partición de la métrica de cierre en OBJ-02 entrega del alumno (≥ 80 %) y OBJ-03 aprobación del administrador (100 %), su motivo declarado, y el punto abierto del alcance de OBJ-01 escalado como ambigüedad en lugar de resuelto acá. **§9.1**: se reformulan «advertencia» y «error de validación» sobre el estado `Pendiente` y entran «estado del trabajo» con sus cuatro valores, «enviar», «aprobar / rechazar» y «comentario». **§9.2**: entra la forma calificada obligatoria de `Pendiente`, por la colisión que declara el intake §4.2. | Product Manager Senior (AG-00) |
| 1.2 | 2026-08-09 | **Cierra la parte de los hallazgos `F26-02` y `F26-06` que alcanza a este documento**, del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0, contra `PRODUCT-INTAKE` **1.9**. **Sube minor y archiva el estado anterior** porque el documento es el glosario raíz de la cadena y lo citan las categorías 02 y 03 de los cinco proyectos de código (`Master-Prompt.md` §5). **§8, riesgo RG-06 (`F26-02`)**: la mitigación declarada era **dar de baja la cuenta y volver a darla de alta**, que desde el 2026-08-09 es el procedimiento que el producto reemplazó; se reescribe sobre la capacidad **F-26** —reseteo con provisoria producida por el sistema, con la cuenta y todos los trabajos conservados—, la probabilidad baja, y se declara qué parte del riesgo subsiste: no hay recuperación autónoma, de modo que el alumno depende de que el docente esté disponible. Ninguna otra fila de §8 cambia. **§4.1 (`F26-02`)**: el alcance comprometido pasa a citar **F-25 y F-26** además de F-01 a F-12 y F-21 a F-24, con una línea sobre por qué las dos entraron el 2026-08-09. **§5 y §6 (`F26-06`)**: el punto abierto de las siete u ocho etapas de OBJ-01 **estaba resuelto y este documento seguía declarándolo escalado**; el target pasa a **8 de 8** sobre las ocho etapas comprometidas, por la decisión del Product Owner registrada en el intake §8, y se deja constancia de que la fila A-2 del intake §22 todavía transcribe «7 de 7», que es un residuo de la fuente y no un target vivo. **§8, riesgo RG-03 (`F26-20`)**: los escenarios de datos del intake pasan de siete a **ocho**, contados `E-1` a `E-8` en su §20. Ningún término del glosario §9, ninguna restricción §7 y ningún otro objetivo cambia. | Product Manager Senior (AG-00) |
| 1.3 | 2026-08-10 | **Cierra la parte del hallazgo `N-1`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r2.md` 1.0 que alcanza a este documento, contra el texto vivo del `PRODUCT-INTAKE` **1.10**. **§5** cerraba el punto abierto de OBJ-01 con una constancia falsa: que «la fila A-2 de PRODUCT-INTAKE §22 todavía transcribe “7 de 7 etapas”, de modo que la fuente se contradice a sí misma». El intake **1.10** corrigió esa fila en el mismo commit que emitió la versión 1.2 de este documento, y §22 A-2 dice hoy «8 de 8 etapas». La constancia pasa a registrar que la contradicción **existió y quedó cerrada**, en lugar de declararla vigente. **Este documento no fue nombrado por `N-1`, que citó tres pasajes; éste es el cuarto**, encontrado al verificar el hallazgo contando las ocurrencias vivas en lugar de confiar en la lista. **Ningún objetivo, target, riesgo, restricción ni término del glosario cambia de valor**: el target de OBJ-01 y el de la métrica de avance de §6 ya contaban ocho. Sube minor: corrige una afirmación sobre otra fuente sin alterar contenido derivado. | Product Manager Senior (AG-00) |
| 1.4 | 2026-08-11 | **Absorbe la promoción de F-13 a `Must Have`**, decidida por el Product Owner y registrada en `PRODUCT-INTAKE` **1.19** §4 y en su control de cambios. **§4.1**: la enumeración de capacidades comprometidas suma **F-13** y el recuento pasa de dieciocho a **diecinueve**; el párrafo que nombraba las dos capacidades incorporadas después de la emisión inicial pasa a nombrar **tres**, con el fundamento propio de F-13 —la puerta técnica que precede a la etapa de visualización ya medía la sincronización por índice y la disposición determinista, de modo que la capacidad era diferible en el papel e indiferible en los hechos—. **§4.2**: sale la sincronización entre el árbol y la escena y la disposición estable de las figuras, y el rango de capacidades candidatas pasa de «F-13 a F-17» a **«F-14 a F-17»**; se agrega el párrafo que declara adónde se fue y por qué las cuatro que quedan no están en su caso: necesitan superficie que el tramo comprometido no levanta. Ninguna métrica, objetivo, exclusión ni entrada de glosario cambia. Sube minor. | Product Manager Senior (AG-00) |
| 1.6 | 2026-08-14 | **`RG-07` reescrito contra la experiencia del Product Owner.** La 1.5 lo registraba como «baja del sitio por no alcanzar cinco visitas mensuales», dato leído del panel y **no verificado**; el Product Owner, que usa la cuenta hace más de un año, **no lo conocía**. Pasa a **[A CONFIRMAR]** junto con el límite de usuarios simultáneos. El riesgo real, **observado en uso**, es otro: **el proceso del hosting recicla la aplicación y se lleva lo que viva en memoria**. No contradice la medición de `PT-01.c` —veinte minutos con un solo circuito—: son escalas distintas. Su impacto es **bajo por una decisión previa**: el almacén vive en el servicio de datos, en servidor propio, y el Product Owner lo puso ahí por este motivo. Se declara la condición que lo volvería alto. | Product Owner (observación) · Orquestador SDD (registro) |
| 1.5 | 2026-08-14 | **Da de alta el riesgo `RG-07`, que ninguna fuente preveía y que no viene del intake.** La verificación del panel de la cuenta del hosting del 2026-08-13 —registrada en [`Compatibilidad-Plataformas.md`](Compatibilidad-Plataformas.md) **1.6** §2.6.2— dejó a la vista una condición del plan gratuito que el corpus no tenía: **el proveedor exige al menos cinco visitas por mes o da de baja el sitio**. Choca con la forma de uso del producto, que es **por ráfagas** —concentrada en los días de entrega—, de modo que un mes sin actividad basta para perder el sitio sin que nadie haya hecho nada mal. Se registra con probabilidad **Media**, impacto **Alto** y mitigación de dos partes: la comprobación periódica contra la dirección pública que ya existe para `PT-01.a`, **con la salvedad declarada de que no está comprobado que el proveedor cuente una petición automática como visita**, y el rehacer de la publicación como red de contención. El párrafo introductorio de §8 pasa de declarar **seis** riesgos a **siete** y explica por qué el séptimo no deriva de PRODUCT-INTAKE §11, que es de donde derivan los otros seis; §10 anota esa segunda fuente y suma 09-Devops como destino. **Ningún otro riesgo cambia de probabilidad, impacto, mitigación ni responsable**, y ninguna otra sección del documento se toca. Sube minor. | Ingeniero DevOps Senior + Deploy Engineer (AG-09) |
