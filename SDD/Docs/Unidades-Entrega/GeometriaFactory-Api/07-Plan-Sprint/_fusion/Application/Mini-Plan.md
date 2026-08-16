# Mini-Plan — GeometriaFactory-Application

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** Mini-Plan.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Scrum Master + Maintainer Lead (AG-07)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../06-Backlog-Tecnico/Product-Backlog.md`](../../../06-Backlog-Tecnico/_fusion/Application/Product-Backlog.md) **1.1** (seis épicas, treinta y dos historias), [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../../../06-Backlog-Tecnico/_fusion/Application/Backlog-Tecnico.md) **1.1** (veintiuna tareas técnicas) y [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../../../06-Backlog-Tecnico/_fusion/Application/Definition-Of-Ready.md) 1.0; [`../../../00-Contexto/Roadmap-Producto.md`](../../../../../00-Contexto/Roadmap-Producto.md) 1.5 §2.1, §4 y §5; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.18** §2 (`equipo_n = 1`), §10 y §15; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/_fusion/Application/Arquitectura-Proyecto-Codigo.md) 1.0 §5, §8, §9 y §11; [`../../../Producto/Vista-Producto.md`](../../../../../Producto/Vista-Producto.md) §3
**Trazabilidad downstream:** `08-Calidad-Y-Pruebas`, `09-Devops` y `11-Documentacion` de GeometriaFactory-Application

---

## Tabla de contenido

- [1. Información general](#1-información-general)
  - [1.1 Por qué esta categoría emite un mini-plan y no planes de iteración](#11-por-qué-esta-categoría-emite-un-mini-plan-y-no-planes-de-iteración)
  - [1.2 Capacidad disponible](#12-capacidad-disponible)
- [2. Objetivo de cada tramo](#2-objetivo-de-cada-tramo)
- [3. Ítems comprometidos por tramo](#3-ítems-comprometidos-por-tramo)
- [4. Alcance técnico y orden de construcción](#4-alcance-técnico-y-orden-de-construcción)
- [5. Definition of Done aplicada](#5-definition-of-done-aplicada)
- [6. Riesgos y mitigaciones](#6-riesgos-y-mitigaciones)
- [7. Criterios de hecho de cada tramo](#7-criterios-de-hecho-de-cada-tramo)
- [8. Trazabilidad](#8-trazabilidad)
- [9. Bitácora de avance](#9-bitácora-de-avance)
- [10. Control de cambios](#10-control-de-cambios)

---

## 1. Información general

| Campo | Valor |
| --- | --- |
| Unidad de planificación | La **etapa** del producto, no el sprint (`Roadmap-Producto.md` §1.2) |
| Etapas comprometidas del producto | **Ocho**, `a` a `h` (`PRODUCT-INTAKE` §15) |
| Etapas que toca este proyecto de código | **Seis**: `a`, `c`, `d`, `e`, `f` y `h` |
| Duración de cada etapa | **Sin fecha.** El intake declara sin plazo calendario y el avance se mide por etapas cerradas (`Roadmap-Producto.md` §1.1) |
| Tamaño del equipo | `equipo_n = 1` (`PRODUCT-INTAKE` §2) |
| Unidad de estimación | **Sin fijar**, por [`../06-Backlog-Tecnico/Product-Backlog.md`](../../../06-Backlog-Tecnico/_fusion/Application/Product-Backlog.md) §4.1 |
| Nivel topológico | **1**, con una sola dependencia saliente: `GeometriaFactory-Domain` (`Vista-Producto.md` §3) |
| Etapas del pipeline | `restore` → `build` → `test`, con las puertas bloqueantes de `05` §8 (`05` §5) |
| Paralelismo entre etapas | **Ninguno.** Las etapas son estrictamente secuenciales y sin OK explícito no se avanza (`Roadmap-Producto.md` §4) |

### 1.1 Por qué esta categoría emite un mini-plan y no planes de iteración

El intake declara **`equipo_n = 1`** en su §2, y de ese dato el framework deriva que la categoría 07 emita **únicamente** `Mini-Plan.md`. `Roadmap-Producto.md` lo declara en su §2.1, en su §3 y en su §6. En consecuencia **no se emiten** `Plan-Iteracion-Sprint-XX.md`, `Template-Sprint-Review.md`, `Template-Sprint-Retrospectiva.md` ni `Velocidad-Equipo.md`, y su ausencia es decisión declarada y no omisión.

**Y hay un segundo motivo, que conviene decir aparte**: aunque el equipo creciera, este producto **no planifica en sprints**. Su ciclo es etapa, informe de cierre, punto de control bloqueante y fusión, y su métrica de avance es la etapa cerrada y demostrada.

### 1.2 Capacidad disponible

**No se declara capacidad numérica, y es deliberado.** Ninguna fuente da base: el intake declara sin plazo calendario, no hay iteraciones cerradas y el equipo es de una persona que además es el Product Owner.

Y hay un motivo propio de este proyecto de código: **el único NFR de tiempo que lo alcanza ya viene rotulado como asunción sin confirmar** —los 500 ms del caso de uso más pesado, `05` §8 y §11 `PA-05`—. Declarar acá una capacidad en puntos agregaría un segundo número sin respaldo al lado del primero, en lugar de reemplazarlo.

Lo que **sí** limita la capacidad y está declarado es el **cuello de diseño**: el punto de control de cada etapa, que el intake §10 nombra como el que fija el ritmo.

## 2. Objetivo de cada tramo

Una frase por etapa, orientada a lo que queda disponible al cerrarla. El objetivo de la etapa a nivel producto vive en `Roadmap-Producto.md` §2.1 y no se reescribe acá.

| Etapa | Objetivo de este proyecto de código al cerrar la etapa |
| --- | --- |
| `a` | La biblioteca de casos de uso existe, compila con una sola dependencia saliente, y los nombres de sus tipos y **el del cuarto puerto** quedaron fijados en el punto de control. |
| `c` | El consumidor puede configurar la única cuenta de administrador, consultar la admisibilidad de una cuenta con su motivo y pedir el reemplazo de una credencial exigiendo la vigente, con la guarda de autorización ya en pie. |
| `d` | El consumidor puede recorrer el ciclo de vida completo de una cuenta de alumno, incluidos la provisoria que produce la habilitación, el reseteo que conserva todos sus trabajos y la comprobación que impide operar hasta cambiarla. |
| `e` | El consumidor puede constituir, reeditar y retirar un trabajo con dueño, y resolver las dos consultas con su predicado de alcance ya aplicado y sin componentes en el listado. |
| `f` | El consumidor puede enviar un trabajo, interpretarlo por el puerto y dejar que el dominio resuelva entre `Borrador` y estado `Pendiente`, sin tocar la base de datos en ninguna prueba. |
| `h` | El consumidor puede resolver el desenlace de un trabajo en estado `Pendiente`, eliminarlo con el alcance del administrador y devolverle al alumno su desenlace y su comentario. |

**Las etapas `b` y `g` no producen trabajo en este proyecto de código**, y por eso no tienen fila. No es un hueco: es lo que [`../06-Backlog-Tecnico/Product-Backlog.md`](../../../06-Backlog-Tecnico/_fusion/Application/Product-Backlog.md) §2 declara con su motivo.

## 3. Ítems comprometidos por tramo

Los identificadores son los del backlog de 06 y **ninguno se inventa acá**. La columna de estimación queda sin valor por §1.2 y la de asignación es la única persona del equipo.

| Etapa | ID | Tipo | Descripción corta | Prioridad | Estimación | Asignado | Estado |
| --- | --- | --- | --- | --- | --- | --- | --- |
| `a` | BT-04001 | Tarea técnica | Crear el proyecto de código y su proyecto de pruebas, con una sola dependencia saliente | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-04002 | Tarea técnica | Fijar los nombres de tipos, de espacios de nombres y el del cuarto puerto | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-04003 | Tarea técnica | Elegir y anclar la herramienta que calcula la versión | Media | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-04004 | Tarea técnica | Puerta bloqueante de dependencias salientes | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-04005 | Tarea técnica | Puerta de construcción con cero advertencias | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-04006 | Tarea técnica | Puerta propia de cero pruebas que tocan la base de datos real | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-04007 | Tarea técnica | Declarar los cuatro puertos como frontera | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-04010 | Tarea técnica | Guarda de autorización con las cuatro comprobaciones en orden fijo | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-04012 | Tarea técnica | Orquestación del alta de cuentas | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-04014 | Tarea técnica | Orquestación del ingreso y la credencial | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-04003 | Historia | Configurar la cuenta de administrador con su ventana de alta | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-04028 | Historia | Rechazar la configuración de un segundo administrador | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-04007 | Historia | Devolver el motivo de una cuenta que no admite ingreso | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-04009 | Historia | Reemplazar la credencial derivada exigiendo la vigente | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | BT-04011 | Tarea técnica | Matriz de ejercicio de las cuatro comprobaciones | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | BT-04013 | Tarea técnica | Orquestación del gobierno de cuentas | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | BT-04018 | Tarea técnica | Confirmar los dos valores rotulados como asunción | Media | Sin fijar | Equipo (1) | Pendiente |
| `d` | BT-04020 | Tarea técnica | Elevar los sellos de alta, modificación y desenlace | Media | Sin fijar | Equipo (1) | Pendiente |
| `d` | BT-04021 | Tarea técnica | Acompañar la decisión del criterio de comparación de correos | Media | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-04001 | Historia | Constituir una cuenta de alumno `Pendiente` y sin credencial | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-04002 | Historia | Rechazar el alta con un correo ya registrado | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-04004 | Historia | Habilitar, bloquear y rehabilitar con verificación de facultad | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-04005 | Historia | Dar de baja exigiendo el correo escrito como confirmación | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-04006 | Historia | Arrastrar en la baja todos los trabajos de la cuenta | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-04008 | Historia | Fijar la credencial derivada provisoria dentro de la habilitación | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-04029 | Historia | Resetear la contraseña de un alumno con verificación de facultad | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-04030 | Historia | Impedir que una cuenta marcada ejerza cualquier otra capacidad | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-04031 | Historia | Conservar la cuenta, su estado y todos sus trabajos tras el reseteo | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-04032 | Historia | Levantar la marca con el cambio hecho por la propia cuenta | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | BT-04009 | Tarea técnica | Fijar el alcance de la unidad de trabajo | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | BT-04015 | Tarea técnica | Orquestación del trabajo | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | BT-04016 | Tarea técnica | Orquestación de la consulta, con la proyección sin componentes | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-04010 | Historia | Cargar un trabajo con dueño, identificador propio y sello del reloj | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-04011 | Historia | Conservar el texto original íntegro al cargar y al reeditar | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-04012 | Historia | Reeditar sólo un trabajo propio en `Borrador` | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-04017 | Historia | Listar los trabajos propios con los cuatro estados distinguibles | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-04019 | Historia | Detalle con piezas y componentes, y listado sin componentes | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-04020 | Historia | Listar los trabajos de la comisión excluyendo los borradores | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-04021 | Historia | Filtrar el listado de la comisión por alumno | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-04022 | Historia | Abrir el detalle de un trabajo de la comisión | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-04026 | Historia | Eliminar un trabajo propio sólo en `Borrador` | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | BT-04019 | Tarea técnica | Medir el tiempo del caso de uso más pesado sobre `E-1`, sin base | Media | Sin fijar | Equipo (1) | Pendiente |
| `f` | BT-04008 | Tarea técnica | Resultado tipado y catálogo de las 36 condiciones en las dos direcciones | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-04013 | Historia | Enviar un trabajo con advertencias y que pase a estado `Pendiente` | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-04014 | Historia | Enviar un trabajo con errores y que quede en `Borrador` | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-04015 | Historia | Interpretar el texto por el puerto, sin tocar la base de datos | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-04016 | Historia | Terminar de forma controlada cuando la interpretación no está disponible | Media | Sin fijar | Equipo (1) | Pendiente |
| `h` | BT-04017 | Tarea técnica | Orquestación del desenlace | Alta | Sin fijar | Equipo (1) | Pendiente |
| `h` | US-04018 | Historia | Ver el desenlace y el comentario del trabajo propio | Alta | Sin fijar | Equipo (1) | Pendiente |
| `h` | US-04023 | Historia | Aprobar un trabajo en estado `Pendiente`, con comentario opcional | Alta | Sin fijar | Equipo (1) | Pendiente |
| `h` | US-04024 | Historia | Rechazar un trabajo en estado `Pendiente`, con comentario opcional | Alta | Sin fijar | Equipo (1) | Pendiente |
| `h` | US-04025 | Historia | Rechazar toda transición sin facultad o desde un estado terminal | Alta | Sin fijar | Equipo (1) | Pendiente |
| `h` | US-04027 | Historia | Eliminar por el administrador en los tres estados que ve | Alta | Sin fijar | Equipo (1) | Pendiente |

**Total comprometido: 32 historias y 21 tareas técnicas, repartidas en seis etapas.** La prioridad de la columna es de **ejecución dentro de la etapa** y no reemplaza a la MoSCoW del backlog, que vive en 06.

**US-04016 figura con prioridad de ejecución `Media`**, y su MoSCoW en 06 es `Should`. Es la única historia de este backlog donde las dos coinciden en señalar lo mismo: si la etapa `f` aprieta, es la primera candidata a diferirse.

## 4. Alcance técnico y orden de construcción

Esta sección **no redefine arquitectura**: referencia la de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/_fusion/Application/Arquitectura-Proyecto-Codigo.md).

**Orden dentro de cada etapa**, derivado de las dependencias de [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../../../06-Backlog-Tecnico/_fusion/Application/Backlog-Tecnico.md) §3:

1. `a`: BT-04001 primero; BT-04002 y BT-04003 sobre él; BT-04004, BT-04005 y BT-04006 al cerrar, porque son puertas y no se pueden medir sobre un proyecto que todavía no compila. **BT-04002 termina en el punto de control**, y ahí queda fijado el nombre del cuarto puerto.
2. `c`: BT-04007 antes que todo lo demás —los orquestadores consumen puertos—; después BT-04010, después BT-04012 y BT-04014; las cuatro historias de la etapa sobre esas tareas.
3. `d`: BT-04013 sobre BT-04010 y BT-04007; las diez historias después; **BT-04011 se abre con US-04030 y se cierra con la última historia de la etapa**, porque la cuarta comprobación no tiene sobre qué decidir hasta que exista la marca; BT-04018, BT-04020 y BT-04021 antes del punto de control.
4. `e`: BT-04009 sobre BT-04007; después BT-04015 y BT-04016; las nueve historias después.
5. `f`: las cuatro historias sobre BT-04015; **BT-04008 al cerrar**, porque el catálogo de condiciones no se puede recorrer en las dos direcciones hasta que el conjunto esté entero producido; BT-04019 al final, porque mide sobre algo terminado.
6. `h`: BT-04017 sobre BT-04010 y BT-04007; las cinco historias después.

**Regla de dependencias interna que ninguna tarea puede cruzar** (`05` §3.2): **ningún orquestador depende de otro orquestador**, la guarda **no lee conjuntos y no escribe**, y la flecha hacia `GeometriaFactory-Infrastructure` es de implementación y va al revés que la de dependencia —este proyecto de código **no la nombra ni la referencia**—.

**Consecuencia del nivel topológico 1, y es lo que más condiciona el orden dentro de cada etapa**: el trabajo de `GeometriaFactory-Domain` va **antes** que el de este proyecto de código, y el de este proyecto de código va **antes** que el de `GeometriaFactory-Infrastructure` y el de `GeometriaFactory-Api`. Lo que **no** cambia es el orden de las etapas: siguen siendo secuenciales.

## 5. Definition of Done aplicada

**La DoD canónica del proyecto de código vive en `08-Calidad-Y-Pruebas` y todavía no está emitida.** Este plan la referencia por destino y **no la redefine**; hasta que exista, lo que gobierna el cierre son los criterios de transición de [`../../../00-Contexto/Roadmap-Producto.md`](../../../../../00-Contexto/Roadmap-Producto.md) §5.

Criterios específicos que este plan agrega, y que no reemplazan a los anteriores:

1. **La actualización de la categoría 11 forma parte del cierre.** La categoría 11 de este proyecto de código **todavía no está emitida**, de modo que hasta su emisión la condición se cumple de forma vacía y **se registra así en el informe de cierre**, en lugar de darse por cumplida en silencio.
2. **Las tres puertas de construcción se miden en cada etapa y no sólo en la que las introdujo**: una sola dependencia saliente (BT-04004), cero advertencias (BT-04005) y **cero pruebas que toquen la base de datos real** (BT-04006).
3. **La matriz de ejercicio de las cuatro comprobaciones se revisa al cerrar cada etapa** que introduzca o toque un camino de lectura o de escritura.
4. **Ningún guion de prueba que involucre el texto de figuras usa datos inventados**: se usan los escenarios `E-1` a `E-8` del intake §20, por la regla de delivery 5 de su §15.
5. **Los dos valores rotulados [ASUNCIÓN] se usan como vigentes y la puerta de cobertura no se declara bloqueante en 09** hasta que BT-04018 cierre. Ninguna etapa se cierra declarando cumplido un número que el Product Owner no confirmó.

## 6. Riesgos y mitigaciones

| Riesgo | Probabilidad | Impacto | Mitigación |
| --- | --- | --- | --- |
| Que aparezca un camino que ejerza una capacidad **sin** resolver antes la marca de cambio de contraseña pendiente | Media, **y es una dependencia de disciplina heredada**: [`Domain ADR-02005`](../../../05-Arquitectura-Tecnica/Adrs/ADR-02005-Guarda-Unica-De-Admisibilidad.md) §6 declaró que el dominio no puede impedirla | **Muy alto**: `INV-09` deja de valer y una clave que el administrador conoce queda sirviendo para operar como el alumno | BT-04010 con el orden fijo en un único componente, BT-04011 con la prueba específica de que la cuarta corta primero, y el criterio 5 de la DoR, que no admite excepción (`05` §9, segundo riesgo) |
| Que un caso de uso consulte la base por su cuenta y deje de ser probable con dobles | Media: es la presión natural cuando una pantalla pide un dato que la proyección no trae | Alto: se pierde la propiedad que justifica el estilo entero y la autorización por pertenencia deja de poder verificarse sin base | BT-04004 y **BT-04006**, la puerta propia de cero pruebas que tocan la base real, medidas en cada etapa (`05` §9, primer riesgo) |
| Que la negativa por pertenencia y la negativa por facultad se confundan, y un trabajo ajeno responda «no autorizado» | Media: es un error de lectura fácil, y la categoría 03 lo llama «el error más caro que un consumidor puede cometer contra esta capa» | Alto: permite averiguar por tanteo qué identificadores existen, que es lo que `RN-04003` viene a cerrar | BT-04011, con la prueba que pide un trabajo ajeno y compara el motivo emitido; y la tabla de traducciones prohibidas de 03 (`05` §9, tercer riesgo) |
| Que el nombre del cuarto puerto se fije sin punto de control y haya que renombrarlo en los cuatro componentes que lo consumen | **Alta**: hoy no tiene nombre declarado en ninguna fuente (`05` §9, sexto riesgo) | Bajo: costo de retrabajo, no de corrección | BT-04002, con caja temporal en la etapa `a` y validación en su punto de control, y el nombramiento en lenguaje de dominio mientras tanto |
| Que un caso de uso reparta su efecto entre dos unidades de trabajo y la baja deje trabajos huérfanos | Baja | Alto: `RN-04007` deja de valer y el arrastre se vuelve parcial | BT-04009, con la baja como caso testigo y el NFR de **0** casos de uso que repartan su efecto (`05` §9, cuarto riesgo) |
| Que la etapa `f` avance con los valores rotulados como asunción sin confirmar y la puerta de cobertura se declare bloqueante sobre un número que nadie aprobó | Media | Medio | BT-04018, con la condición explícita de que la puerta **no se declara bloqueante en 09** hasta que el Product Owner confirme (`05` §11 `PA-05`; `PRODUCT-INTAKE` §22) |
| Que el punto de control de una etapa se demore y el trabajo de los niveles 2 y 3 se adelante sobre una superficie todavía no aprobada | Media | Alto: rompe la regla de etapas en serie del intake §10 | La regla de delivery 4 del intake §15 —una rama y una solicitud de incorporación por etapa, y no se abre la siguiente antes de fusionar—, que se aplica también a este proyecto de código aunque su trabajo esté terminado antes |

## 7. Criterios de hecho de cada tramo

Una etapa de este proyecto de código está hecha cuando:

- [ ] Todas sus historias y tareas comprometidas en §3 están en estado terminado.
- [ ] Los criterios comunes a toda transición de [`../../../00-Contexto/Roadmap-Producto.md`](../../../../../00-Contexto/Roadmap-Producto.md) §5.1 se cumplen, incluida la no regresión: los guiones de todas las etapas anteriores vuelven a pasar **sin correcciones**.
- [ ] Los criterios propios de la transición correspondiente de su §5.2 que alcanzan a este proyecto de código se cumplen.
- [ ] La etapa incorporó pruebas automatizadas de las reglas de negocio que introdujo, **todas sin base de datos**.
- [ ] Las tres puertas del pipeline pasan: una sola dependencia saliente, cero advertencias y cero pruebas que toquen la base real.
- [ ] El informe de cierre de la etapa está escrito y es autocontenido, con su índice.
- [ ] Los documentos de la categoría 11 afectados están revisados, o se registra que la categoría todavía no está emitida.
- [ ] El Product Owner dio **OK explícito** en el punto de control, y la rama está incorporada antes de abrir la siguiente.

## 8. Trazabilidad

| Etapa | NB que avanzan | CU que avanzan | ADR que gobiernan las decisiones |
| --- | --- | --- | --- |
| `a` | Ninguna: es un hito interno sin capacidad funcional asociada | Ninguno | ADR-04001, ADR-04003 |
| `c` | NB-00001, NB-00002 | CU-04003, CU-04010 | ADR-04001, ADR-04002, ADR-04004, ADR-04006 |
| `d` | NB-00001, NB-00002 | CU-04001, CU-04002, CU-04003, CU-04011 | ADR-04002, ADR-04004, ADR-04005 |
| `e` | NB-00003, NB-00006 (parcial), NB-00007 | CU-04004, CU-04006, CU-04007, CU-04009 | ADR-04001, ADR-04004, ADR-04005 |
| `f` | NB-00004, NB-00005, NB-00003 | CU-04005 | ADR-04001, ADR-04002, ADR-04006 |
| `h` | NB-00009 | CU-04006, CU-04008, CU-04009 | ADR-04004, ADR-04005, ADR-04006 |

**Las seis etapas declaran al menos una necesidad de negocio en avance, salvo la `a`**, y esa excepción es del propio roadmap: `a` es un hito interno sin capacidad funcional asociada (§2.1).

**`NB-00008` no aparece en ninguna fila, y es declaración y no olvido.** `02` §7.2 declara que este proyecto de código **no la toca**: su dolor es de acceso y de despliegue, y esta capa no atiende peticiones, no abre conexiones y no conoce la frontera de proceso. Se cubre en 02 de `GeometriaFactory-Web` y de `GeometriaFactory-Api` y en `09-Devops`.

**Puertas técnicas del producto y este proyecto de código.** `PT-01` y `PT-04` se miden en la etapa `a` y `PT-02` y `PT-03` antes de comprometer la `g`; **ninguna de las cuatro se mide sobre este proyecto de código** —son del front, del servicio de datos y del bundle del visor—, y por eso no figuran como ítem de §3. Lo que sí lo alcanza es la consecuencia: una puerta que no pasa **detiene la planificación de las etapas que dependen de ella**.

## 9. Bitácora de avance

**Sin entradas al 2026-08-10.** Ninguna etapa está abierta: el producto está en la fase de especificación y la etapa `a` todavía no arrancó.

| Fecha | Etapa | Qué se cerró | Qué quedó abierto | Punto de control |
| --- | --- | --- | --- | --- |
| — | — | — | — | — |

La bitácora se completa **al cerrar cada etapa**, junto con el informe de cierre que el intake §15 exige, y no semana a semana: la cadencia de este producto es la etapa y no el calendario. Para la etapa `a`, lo que se registra es el **resultado del punto de control sobre los nombres**, incluido el del cuarto puerto.

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial del mini-plan de `GeometriaFactory-Application`. Declara por qué la categoría emite un único artefacto —`equipo_n = 1`, con la constancia de los cuatro que **no** se emiten— y por qué no se declara capacidad numérica, con el fundamento propio de que el único NFR de tiempo que alcanza a esta capa ya viene rotulado como asunción sin confirmar. Fija el objetivo de cada uno de los **seis** tramos que este proyecto de código toca de las **ocho** etapas comprometidas, compromete las **32** historias y las **21** tareas técnicas del backlog de 06 sin inventar ningún identificador, declara el orden de construcción dentro de cada etapa con la regla de dependencias interna que ninguna tarea puede cruzar, referencia la Definition of Done por destino con la constancia de que 08 todavía no está emitida, y declara **siete** riesgos con mitigación, los criterios de hecho de cada tramo y la trazabilidad de necesidades, casos de uso y ADR por etapa, con la constancia de que **`NB-00008` no la toca este proyecto de código**. |
| 1.1 | 2026-08-11 | **Actualiza la trazabilidad upstream** a las versiones del `Product-Backlog.md` y del `Backlog-Tecnico.md` de la sección 06, que subieron a **1.1** el 2026-08-11. El `Product-Backlog.md` subió al absorber la promoción de `F-13` a `Must Have` (`PRODUCT-INTAKE` **1.19** §4) y al declarar la regularidad de la distribución MoSCoW (hallazgo `D-06-03`). El `Backlog-Tecnico.md` subió a 1.1 al cerrar el hallazgo `D-06-02` del informe [`../../../Audit/D-06-07-Backlog-Siete-Proyectos-r1.md`](../../../../../Audit/D-06-07-Backlog-Siete-Proyectos-r1.md) 1.0, que completó una enumeración de cobertura inversa sin tocar ninguna tarea técnica. **Ninguna historia ni tarea técnica de este proyecto de código cambia de prioridad, de etapa ni de tramo**, y ningún compromiso, riesgo ni orden de construcción de este plan se toca: la fila existe para que la versión citada sea la vigente y no una que ya no está. Sube minor. |
