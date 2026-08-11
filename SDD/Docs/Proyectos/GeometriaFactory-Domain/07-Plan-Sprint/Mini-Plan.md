# Mini-Plan — GeometriaFactory-Domain

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** Mini-Plan.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Scrum Master + Maintainer Lead (AG-07)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) **1.1** (seis épicas, veintisiete historias), [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md) **1.1** (dieciséis tareas técnicas) y [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/Definition-Of-Ready.md) 1.0; [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) 1.5 §2.1, §4 y §5; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.18** §2 (`equipo_n = 1`), §10 y §15 (etapas, reglas de delivery y puertas técnicas); [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §8, §9 y §11; [`../../../Producto/Vista-Producto.md`](../../../Producto/Vista-Producto.md) 1.1 §3
**Trazabilidad downstream:** `08-Calidad-Y-Pruebas`, `09-Devops` y `11-Documentacion` de GeometriaFactory-Domain

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
| Etapas que toca este proyecto de código | **Seis**: `a`, `c`, `d`, `e`, `f`, `h` |
| Duración de cada etapa | **Sin fecha.** El intake declara sin plazo calendario y el avance se mide por etapas cerradas (`Roadmap-Producto.md` §1.1) |
| Tamaño del equipo | `equipo_n = 1` (`PRODUCT-INTAKE` §2) |
| Unidad de estimación | **Sin fijar**, por [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §4.1 |
| Nivel topológico | **0**, sin dependencias salientes (`Vista-Producto.md` §3) |
| Paralelismo entre etapas | **Ninguno.** Las etapas son estrictamente secuenciales y sin OK explícito no se avanza (`Roadmap-Producto.md` §4) |

### 1.1 Por qué esta categoría emite un mini-plan y no planes de iteración

El intake declara **`equipo_n = 1`** en su §2, y de ese dato el framework deriva que la categoría 07 emita **únicamente** `Mini-Plan.md`. `Roadmap-Producto.md` lo declara tres veces —en su §2.1, en su §3 y en su §6— y el propio intake lo repite en su tabla de trazabilidad downstream. En consecuencia **no se emiten** `Plan-Iteracion-Sprint-XX.md`, `Template-Sprint-Review.md`, `Template-Sprint-Retrospectiva.md` ni `Velocidad-Equipo.md`, y su ausencia es decisión declarada y no omisión.

**Y hay un segundo motivo, que conviene decir aparte**: aunque el equipo creciera, este producto **no planifica en sprints**. Su ciclo es etapa, informe de cierre, punto de control bloqueante y fusión, y su métrica de avance es la etapa cerrada y demostrada. Un archivo de velocidad sobre un producto sin iteraciones cerradas y sin plazo calendario contendría números que ninguna fuente sostiene.

### 1.2 Capacidad disponible

**No se declara capacidad numérica, y es deliberado.** La regla de la categoría pide capacidad en puntos o en horas con un factor de foco; ninguna fuente de este producto da base para ninguno de los dos. El intake declara sin plazo calendario, no hay historial de iteraciones y el equipo es de una persona que además es el Product Owner y el docente de la cátedra.

Lo que **sí** limita la capacidad y está declarado es el **cuello de diseño**: el punto de control de cada etapa, que el intake §10 nombra como el que fija el ritmo. Ese es el gobierno real del avance, y ponerle un número de puntos al lado lo volvería menos legible, no más.

## 2. Objetivo de cada tramo

Una frase por etapa, orientada a lo que queda disponible al cerrarla. Las frases son de este proyecto de código: el objetivo de la etapa a nivel producto vive en `Roadmap-Producto.md` §2.1 y no se reescribe acá.

| Etapa | Objetivo de este proyecto de código al cerrar la etapa |
| --- | --- |
| `a` | La biblioteca de dominio existe, compila con cero dependencias salientes y sus nombres y su cálculo de versión quedaron fijados en el punto de control. |
| `c` | El consumidor puede constituir la única cuenta de administrador, evaluar la admisibilidad de una cuenta y reemplazar su credencial exigiendo la vigente. |
| `d` | El consumidor puede recorrer el ciclo de vida completo de una cuenta de alumno, incluido el reseteo que conserva sus trabajos y la marca que le impide operar hasta cambiar la provisoria. |
| `e` | El consumidor puede constituir y reeditar un trabajo con dueño, y resolver qué ve el alumno y qué ve el administrador. |
| `f` | El consumidor puede adoptar el conjunto de piezas y las observaciones de un trabajo, y resolver si el envío lo lleva a estado `Pendiente` o lo deja en `Borrador`. |
| `h` | El consumidor puede resolver el desenlace de un trabajo en estado `Pendiente` y la eliminación por el administrador, con los dos estados terminales operativos. |

**Las etapas `b` y `g` no producen trabajo en este proyecto de código**, y por eso no tienen fila. No es un hueco: es lo que [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §2 declara con su motivo.

## 3. Ítems comprometidos por tramo

Los identificadores son los del backlog de 06 y **ninguno se inventa acá**. La columna de estimación queda vacía por §1.2 y la de asignación es la única persona del equipo.

| Etapa | ID | Tipo | Descripción corta | Prioridad | Estimación | Asignado | Estado |
| --- | --- | --- | --- | --- | --- | --- | --- |
| `a` | BT-01 | Tarea técnica | Crear el proyecto de código y su proyecto de pruebas, sin dependencias salientes | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-02 | Tarea técnica | Fijar los nombres de tipos y de espacios de nombres, y validarlos en el punto de control | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-03 | Tarea técnica | Elegir y anclar la herramienta que calcula la versión | Media | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-04 | Tarea técnica | Puerta bloqueante de cero dependencias salientes | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-05 | Tarea técnica | Puerta de construcción con cero advertencias | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-06 | Tarea técnica | Núcleo de entidades con las cinco entidades del modelo | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-07 | Tarea técnica | Superficie pública de guardas con resultado tipado | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-10 | Tarea técnica | Guardas de cuenta | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-11 | Tarea técnica | Evaluador de admisibilidad como puerta única | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-24 | Historia | Configurar la cuenta de administrador en el primer arranque | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-25 | Historia | Rechazar la configuración de un segundo administrador | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-08 | Historia | Evaluar la admisibilidad de la cuenta y devolver su motivo | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-07 | Historia | Reemplazar la credencial derivada exigiendo la vigente | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | BT-09 | Tarea técnica | Momento y unicidad por parámetro | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | BT-14 | Tarea técnica | Matriz de ejercicio de los nueve invariantes | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | BT-15 | Tarea técnica | Confirmar los dos valores rotulados como asunción | Media | Sin fijar | Equipo (1) | Pendiente |
| `d` | BT-16 | Tarea técnica | Decidir el criterio de comparación de dos correos | Media | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-01 | Historia | Constituir un alumno con cuenta `Pendiente` y sin credencial | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-02 | Historia | Rechazar el alta con datos obligatorios ausentes | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-03 | Historia | Exigir la unicidad del correo verificada en el alta | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-04 | Historia | Habilitar, bloquear y rehabilitar una cuenta | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-05 | Historia | Dar de baja una cuenta arrastrando sus trabajos | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-06 | Historia | Fijar la credencial derivada provisoria en el acto de habilitación | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-26 | Historia | Resetear la contraseña conservando cuenta y trabajos | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-27 | Historia | Exigir el cambio de la provisoria antes de toda otra capacidad | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | BT-12 | Tarea técnica | Máquina de estados del trabajo | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-09 | Historia | Constituir un trabajo con dueño, identidad y texto original | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-10 | Historia | Reeditar un trabajo en `Borrador` descartando la interpretación anterior | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-18 | Historia | Resolver la pertenencia de un trabajo a su dueño | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-19 | Historia | Acotar al estado `Borrador` lo que el alumno reedita y elimina | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-22 | Historia | Excluir los trabajos en `Borrador` del alcance del administrador | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | BT-08 | Tarea técnica | Cerrar el catálogo de las 42 condiciones en las dos direcciones | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | BT-13 | Tarea técnica | Adopción de la interpretación | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-11 | Historia | Reconstruir el conjunto de piezas con identidad posicional | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-12 | Historia | Derivar la familia plana o volumétrica desde el tipo | Media | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-13 | Historia | Registrar advertencias con el valor declarado y el derivado | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-14 | Historia | Registrar errores de validación con posición de pieza y campo | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-15 | Historia | Enviar un trabajo que verifica y pasa a estado `Pendiente` | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-16 | Historia | Enviar un trabajo que no verifica y queda en `Borrador` | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-17 | Historia | Rechazar toda transición desde un estado terminal | Alta | Sin fijar | Equipo (1) | Pendiente |
| `h` | US-20 | Historia | Aprobar un trabajo en estado `Pendiente`, con comentario opcional | Alta | Sin fijar | Equipo (1) | Pendiente |
| `h` | US-21 | Historia | Rechazar un trabajo en estado `Pendiente`, con comentario opcional | Alta | Sin fijar | Equipo (1) | Pendiente |
| `h` | US-23 | Historia | Eliminar por el administrador en los tres estados que ve | Alta | Sin fijar | Equipo (1) | Pendiente |

**Total comprometido: 27 historias y 16 tareas técnicas, repartidas en seis etapas.** La prioridad de la columna es de ejecución dentro de la etapa y no reemplaza a la MoSCoW del backlog, que vive en 06.

## 4. Alcance técnico y orden de construcción

Esta sección **no redefine arquitectura**: referencia la de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md).

**Orden dentro de cada etapa**, derivado de las dependencias de [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md) §3:

1. `a`: BT-01 primero; BT-02 y BT-03 en paralelo sobre él; BT-04 y BT-05 al cerrar, porque son puertas y no se pueden medir sobre un proyecto que todavía no compila.
2. `c`: BT-06 y BT-07 antes que BT-10 y BT-11; las cuatro historias de la etapa después de las cuatro tareas.
3. `d`: BT-09 sobre BT-06; las ocho historias después; BT-14 se abre con la primera historia y se cierra con la última; BT-15 y BT-16 antes del punto de control.
4. `e`: BT-12 sobre BT-06 y BT-07; las cinco historias después.
5. `f`: BT-13 sobre BT-12; BT-08 al cerrar, porque necesita el catálogo ejercido.
6. `h`: las tres historias sobre BT-12 ya construida; BT-14 se revisa por última vez.

**Consecuencia del nivel topológico 0, y es lo que más condiciona el orden del producto**: dentro de cada etapa, el trabajo de este proyecto de código va **antes** que el de `GeometriaFactory-Application`, `GeometriaFactory-Infrastructure` y `GeometriaFactory-Api`, que compilan contra él. Una guarda que acá no exista es una guarda que allá no se puede invocar. Lo que **no** habilita el nivel 0 es adelantar etapas: siguen siendo secuenciales.

## 5. Definition of Done aplicada

**La DoD canónica del proyecto de código vive en `08-Calidad-Y-Pruebas` y todavía no está emitida.** Este plan la referencia por destino y **no la redefine**; hasta que exista, lo que gobierna el cierre son los criterios de transición de [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §5, que son de nivel producto.

Criterios específicos que este plan agrega, y que no reemplazan a los anteriores:

1. **La actualización de la categoría 11 forma parte del cierre.** Ninguna etapa se declara cerrada con documentos del cuerpo documental de entrega afectados por sus ítems y sin revisar. **La categoría 11 de este proyecto de código todavía no está emitida**, de modo que hasta su emisión la condición se cumple de forma vacía y se registra así en el informe de cierre, en lugar de darse por cumplida en silencio.
2. **Las puertas de construcción se miden en cada etapa y no sólo en la que las introdujo**: cero dependencias salientes (BT-04) y cero advertencias (BT-05).
3. **La matriz de ejercicio de los nueve invariantes se revisa al cerrar cada etapa** que introduzca o toque una guarda.
4. **Ningún guion de prueba que involucre el texto de figuras usa datos inventados**: se usan los escenarios `E-1` a `E-8` del intake §20, por la regla de delivery 5 de su §15.

## 6. Riesgos y mitigaciones

| Riesgo | Probabilidad | Impacto | Mitigación |
| --- | --- | --- | --- |
| Que una dependencia se cuele en el nivel 0 —una anotación de mapeo, un atributo de serialización— y el dominio deje de ser probable sin infraestructura | Media | Alto | BT-04, puerta bloqueante de cero dependencias salientes, medida en **cada** etapa y no sólo en la `a` (`05` §9, primer riesgo) |
| Que un invariante se ejerza en un componente y no en otro, y quede una puerta por la que se lo saltea | Media, **y con precedente registrado** en el audit de la categoría 02 de este proyecto de código | Alto | BT-11, puerta única de admisibilidad, y BT-14, matriz de ejercicio de los nueve invariantes (`05` §9, segundo riesgo) |
| Que los nombres de tipos y de espacios de nombres se fijen sin punto de control y después haya que renombrarlos | Media | Bajo: costo de retrabajo, no de corrección | BT-02, con caja temporal en la etapa `a` y validación en su punto de control (`05` §9, quinto riesgo) |
| Que las etapas `c` a `h` avancen con los dos valores rotulados como asunción sin confirmar, y que la puerta de cobertura se declare bloqueante sobre un número que nadie aprobó | Media | Medio | BT-15, con la condición explícita de que la puerta **no se declara bloqueante en 09** hasta que el Product Owner confirme (`05` §11 PA-02, `PRODUCT-INTAKE` §22) |
| Que el punto de control de una etapa se demore y el trabajo de los proyectos de código de nivel 1 a 3 se adelante sobre una superficie todavía no aprobada | Media | Alto: rompe la regla de etapas en serie del intake §10 | La regla de delivery 4 del intake §15 —una rama y una solicitud de incorporación por etapa, y no se abre la siguiente antes de fusionar— se aplica también a este proyecto de código, aunque su trabajo esté terminado antes |

## 7. Criterios de hecho de cada tramo

Una etapa de este proyecto de código está hecha cuando:

- [ ] Todas sus historias y tareas comprometidas en §3 están en estado terminado.
- [ ] Los criterios comunes a toda transición de [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §5.1 se cumplen, incluida la no regresión: los guiones de todas las etapas anteriores vuelven a pasar **sin correcciones**.
- [ ] Los criterios propios de la transición correspondiente de su §5.2 que alcanzan a este proyecto de código se cumplen.
- [ ] La etapa incorporó pruebas automatizadas de las reglas de negocio que introdujo.
- [ ] El informe de cierre de la etapa está escrito y es autocontenido, con su índice.
- [ ] Los documentos de la categoría 11 afectados están revisados, o se registra que la categoría todavía no está emitida.
- [ ] El Product Owner dio **OK explícito** en el punto de control, y la rama está incorporada antes de abrir la siguiente.

## 8. Trazabilidad

| Etapa | NB que avanzan | CU que avanzan | ADR que gobiernan las decisiones |
| --- | --- | --- | --- |
| `a` | Ninguna: es un hito interno sin capacidad funcional asociada | Ninguno | ADR-01, ADR-03 |
| `c` | NB-01, NB-02 | CU-03, CU-04, CU-12 | ADR-01, ADR-02, ADR-04, ADR-05 |
| `d` | NB-01, NB-02 | CU-01, CU-02, CU-03, CU-04, CU-13 | ADR-01, ADR-04, ADR-05, ADR-06 |
| `e` | NB-03, NB-07 | CU-05, CU-09, CU-11 | ADR-02, ADR-06 |
| `f` | NB-04, NB-05, NB-03 | CU-06, CU-07, CU-08 | ADR-01, ADR-02 |
| `h` | NB-09 | CU-10, CU-11 | ADR-02 |

**Las seis etapas declaran al menos una necesidad de negocio en avance, salvo la `a`**, y esa excepción es del propio roadmap: `a` es un hito interno sin capacidad funcional asociada (§2.1). Lo que la `a` sí produce y es verificable son las mediciones de las puertas técnicas del producto.

**Puertas técnicas del producto y este proyecto de código.** `PT-01` y `PT-04` se miden en la etapa `a` y `PT-02` y `PT-03` antes de comprometer la `g`; **ninguna de las cuatro se mide sobre este proyecto de código** —son del front, del servicio de datos y del bundle del visor—, y por eso no figuran como ítem de §3. Lo que sí lo alcanza es la consecuencia: una puerta que no pasa **detiene la planificación de las etapas que dependen de ella**, incluidas las de este proyecto de código.

## 9. Bitácora de avance

**Sin entradas al 2026-08-10.** Ninguna etapa está abierta: el producto está en la fase de especificación y la etapa `a` todavía no arrancó.

| Fecha | Etapa | Qué se cerró | Qué quedó abierto | Punto de control |
| --- | --- | --- | --- | --- |
| — | — | — | — | — |

La bitácora se completa **al cerrar cada etapa**, junto con el informe de cierre que el intake §15 exige, y no semana a semana: la cadencia de este producto es la etapa y no el calendario.

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial del mini-plan de `GeometriaFactory-Domain`. Declara por qué la categoría emite un único artefacto —`equipo_n = 1`, con la constancia de los cuatro artefactos que **no** se emiten— y por qué no se declara capacidad numérica ni velocidad. Fija el objetivo de cada uno de los **seis** tramos que este proyecto de código toca de las **ocho** etapas comprometidas, compromete las **27** historias y las **16** tareas técnicas del backlog de 06 sin inventar ningún identificador, declara el orden de construcción dentro de cada etapa y la consecuencia del nivel topológico 0, referencia la Definition of Done por destino con la constancia de que 08 todavía no está emitida, y declara **cinco** riesgos con mitigación, los criterios de hecho de cada tramo y la trazabilidad de necesidades, casos de uso y ADR por etapa. |
| 1.1 | 2026-08-11 | **Actualiza la trazabilidad upstream** a las versiones del `Product-Backlog.md` y del `Backlog-Tecnico.md` de la sección 06, que subieron a **1.1** el 2026-08-11. El `Product-Backlog.md` subió al absorber la promoción de `F-13` a `Must Have` (`PRODUCT-INTAKE` **1.19** §4) y al declarar la regularidad de la distribución MoSCoW (hallazgo `D-06-03`). El `Backlog-Tecnico.md` subió a 1.1 al cerrar el hallazgo `D-06-02` del informe [`../../../Audit/D-06-07-Backlog-Siete-Proyectos-r1.md`](../../../Audit/D-06-07-Backlog-Siete-Proyectos-r1.md) 1.0, que completó una enumeración de cobertura inversa sin tocar ninguna tarea técnica. **Ninguna historia ni tarea técnica de este proyecto de código cambia de prioridad, de etapa ni de tramo**, y ningún compromiso, riesgo ni orden de construcción de este plan se toca: la fila existe para que la versión citada sea la vigente y no una que ya no está. Sube minor. |
