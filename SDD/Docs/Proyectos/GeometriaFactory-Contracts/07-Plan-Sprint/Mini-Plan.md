# Mini-Plan — GeometriaFactory-Contracts

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** Mini-Plan.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Scrum Master + Maintainer Lead (AG-07)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) **1.1**, [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md) 1.0 y [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/Definition-Of-Ready.md) 1.0; [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) 1.5 §2.1, §4 y §5; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.18** §2, §10, §15 y §17.4; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §5, §8, §9 y §11; [`../../../Producto/Vista-Producto.md`](../../../Producto/Vista-Producto.md) 1.1 §3, §4 y §7
**Trazabilidad downstream:** `08-Calidad-Y-Pruebas`, `09-Devops` y `11-Documentacion` de GeometriaFactory-Contracts

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
| Etapas que toca este proyecto de código | **Siete**: `a`, `c`, `d`, `e`, `f`, `g` y `h`. La única que no toca es la `b` |
| Duración de cada etapa | **Sin fecha.** El avance se mide por etapas cerradas (`Roadmap-Producto.md` §1.1) |
| Tamaño del equipo | `equipo_n = 1` (`PRODUCT-INTAKE` §2) |
| Unidad de estimación | **Sin fijar**, por [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §4.1 |
| Nivel topológico | **0**, sin dependencias salientes (`Vista-Producto.md` §3) |
| Etapas del pipeline | `restore` → `build`. **Sin etapa de pruebas propias** (`05` §5) |
| Paralelismo entre etapas | **Ninguno** (`Roadmap-Producto.md` §4) |

### 1.1 Por qué esta categoría emite un mini-plan y no planes de iteración

El intake declara **`equipo_n = 1`** en su §2, y de ese dato el framework deriva que la categoría 07 emita **únicamente** `Mini-Plan.md`; `Roadmap-Producto.md` lo declara en su §2.1, en su §3 y en su §6. En consecuencia **no se emiten** `Plan-Iteracion-Sprint-XX.md`, `Template-Sprint-Review.md`, `Template-Sprint-Retrospectiva.md` ni `Velocidad-Equipo.md`, y su ausencia es decisión declarada.

El segundo motivo es del producto y no del tamaño del equipo: **este producto no planifica en sprints**. Su ciclo es etapa, informe de cierre, punto de control bloqueante y fusión.

### 1.2 Capacidad disponible

**No se declara capacidad numérica, y es deliberado.** Ninguna fuente da base para puntos ni para horas: el intake declara sin plazo calendario, no hay iteraciones cerradas y el equipo es de una persona.

Hay además un motivo propio de este proyecto de código: **su trabajo no es proporcional a su volumen de código**. Declarar un tipo con cinco campos y decidir cuáles de esos cinco pueden cruzar la frontera cuestan cosas distintas, y la segunda es la que importa. Un número de puntos al lado de cada historia ocultaría exactamente esa asimetría.

## 2. Objetivo de cada tramo

| Etapa | Objetivo de este proyecto de código al cerrar la etapa |
| --- | --- |
| `a` | El ensamblado existe, compila sin dependencias y sin ninguna referencia hacia el dominio, y sus dos puertas de construcción están medidas. |
| `c` | Los dos extremos pueden intercambiar una sesión y cualquier fallo, con un único tipo de error y su conjunto cerrado de quince códigos. |
| `d` | Los dos extremos pueden intercambiar todo el ciclo de vida de una cuenta de alumno, incluido el reseteo, cuya solicitud no tiene campo de contraseña. |
| `e` | Los dos extremos pueden intercambiar el envío, la eliminación y el listado de trabajos, con la proyección de listado ya acotada. |
| `f` | Los dos extremos pueden intercambiar el trabajo interpretado, con sus piezas, sus componentes y sus observaciones. |
| `g` | El detalle lleva el texto original íntegro, que es lo que el árbol despliega y lo que el bundle del visor dibuja. |
| `h` | Los dos extremos pueden intercambiar el desenlace de la revisión y el comentario del administrador como bloque propio. |

**La etapa `b` no produce trabajo en este proyecto de código**: construye la cáscara del front con pantallas de marcador de posición y todavía no hay ningún dato que cruce la frontera.

## 3. Ítems comprometidos por tramo

Los identificadores son los del backlog de 06 y **ninguno se inventa acá**.

| Etapa | ID | Tipo | Descripción corta | Prioridad | Estimación | Asignado | Estado |
| --- | --- | --- | --- | --- | --- | --- | --- |
| `a` | BT-01 | Tarea técnica | Crear el ensamblado de tipos, sin dependencias | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-02 | Tarea técnica | Puerta de cero referencias hacia `GeometriaFactory-Domain` | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-03 | Tarea técnica | Puerta de construcción con cero advertencias | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-04 | Tarea técnica | Fijar los nombres de la familia de sesión y de la de error | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-05 | Tarea técnica | Fijar la zona horaria y la precisión del campo de momento | Media | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-06 | Tarea técnica | Tipo de error único con sus cuatro campos | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-07 | Tarea técnica | Conjunto cerrado de quince códigos vivos, con la regla de no reciclado | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-08 | Tarea técnica | Prueba de inspección de superficie pública para los campos prohibidos | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-09 | Tarea técnica | Familia de sesión con su respuesta de cuatro campos | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-16 | Tarea técnica | Matriz tipo contra prueba de integración | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-17 | Tarea técnica | Adoptar el formato de intercambio que fijan los dos extremos | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-01 | Historia | Canje de credenciales y respuesta de sesión de cuatro campos | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-14 | Historia | Error neutro con el conjunto cerrado de quince códigos | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-16 | Historia | Cerrar el conjunto con el código no clasificado | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | BT-10 | Tarea técnica | Familia de cuentas | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | BT-11 | Tarea técnica | Familia de reseteo | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | BT-18 | Tarea técnica | Confirmar los dos valores rotulados como asunción | Media | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-02 | Historia | Registro de una cuenta de alumno | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-03 | Historia | Listado de cuentas del panel del administrador | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-04 | Historia | Cambio de situación de la cuenta | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-05 | Historia | Baja con su confirmación escrita | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-21 | Historia | Reseteo sin campo de contraseña y con la provisoria producida | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-22 | Historia | Reutilizar la solicitud de cambio para el cambio obligatorio | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | BT-12 | Tarea técnica | Familia de trabajo | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | BT-13 | Tarea técnica | Familia de listado con su carga útil acotada | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-06 | Historia | Envío del trabajo con el texto original como cadena | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-07 | Historia | Solicitud única de eliminación del trabajo | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-08 | Historia | Proyección de listado sin la carga del detalle | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-09 | Historia | Alcance del listado según el papel, con los datos para agrupar y filtrar | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-19 | Historia | Conjunto cerrado de cuatro estados del trabajo | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | BT-14 | Tarea técnica | Familia de detalle, con el comentario como bloque propio | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-11 | Historia | Detalle del trabajo interpretado con sus piezas y componentes | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-13 | Historia | Observación con su severidad y su par de valores | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-15 | Historia | Detalle de ubicación con índice de figura y campo | Alta | Sin fijar | Equipo (1) | Pendiente |
| `g` | US-12 | Historia | Texto original en el detalle, para el árbol y para la escena | Alta | Sin fijar | Equipo (1) | Pendiente |
| `h` | BT-15 | Tarea técnica | Familia de desenlace | Alta | Sin fijar | Equipo (1) | Pendiente |
| `h` | US-17 | Historia | Desenlace con su conjunto cerrado de dos valores | Alta | Sin fijar | Equipo (1) | Pendiente |
| `h` | US-18 | Historia | Comentario del administrador como bloque propio | Alta | Sin fijar | Equipo (1) | Pendiente |
| `h` | US-20 | Historia | Desenlace al alumno: estado en el listado y comentario en el detalle | Alta | Sin fijar | Equipo (1) | Pendiente |

**Total comprometido: 21 historias y 18 tareas técnicas**, repartidas en siete etapas. **US-10 no está comprometida**: es `Could`, cae en la fase `i…` y el roadmap §2.1 declara que esa fase se planifica con la plantilla completa cuando `h` esté cerrada y demostrada.

## 4. Alcance técnico y orden de construcción

Esta sección **no redefine arquitectura**: referencia la de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md).

**Orden dentro de cada etapa**, derivado de las dependencias de [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md) §3:

1. `a`: BT-01 primero; BT-02 y BT-03 al cerrar, porque son puertas y necesitan algo que compile.
2. `c`: **BT-06 antes que todo lo demás**, porque las siete familias restantes dependen de la de error y ella de ninguna (`05` §3.1); después BT-07, BT-08 y BT-09; BT-04, BT-05 y BT-17 se cierran antes del punto de control; BT-16 se abre acá y se mantiene hasta la etapa `h`.
3. `d`: BT-10 antes que BT-11, porque el reseteo depende de cuentas —la única arista adicional del grafo—; las seis historias después; BT-18 antes del punto de control.
4. `e`: BT-12 antes que BT-13; las cinco historias después.
5. `f`: BT-14 sobre BT-12; las tres historias después.
6. `g`: sólo US-12, sobre la familia de detalle ya construida.
7. `h`: BT-15 sobre BT-06 y BT-12; las tres historias después.

**Consecuencia del nivel topológico 0**: dentro de cada etapa, este proyecto de código va **antes** que `GeometriaFactory-Api` y `GeometriaFactory-Web`, que compilan los dos contra el mismo ensamblado. Un tipo que acá no exista es un tipo que ninguno de los dos puede usar.

**Y una consecuencia que sólo tiene este proyecto de código**: cualquier cambio incompatible que se decida en una etapa posterior **rompe la compilación de los dos extremos**, no la de uno. Eso es deliberado —es el mecanismo del versionado del producto— y su contrapartida aceptada es el **despliegue conjunto** ([`ADR-03`](../05-Arquitectura-Tecnica/Adrs/ADR-03-Versionado-Por-Compilacion-Compartida.md)).

## 5. Definition of Done aplicada

**La DoD canónica vive en `08-Calidad-Y-Pruebas` y todavía no está emitida.** Este plan la referencia por destino y **no la redefine**; hasta que exista, lo que gobierna el cierre son los criterios de transición de [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §5.

Criterios específicos que este plan agrega:

1. **La actualización de la categoría 11 forma parte del cierre.** La categoría 11 de este proyecto de código todavía no está emitida, de modo que hasta su emisión la condición se cumple de forma vacía y **se registra así en el informe de cierre**, en lugar de darse por cumplida en silencio.
2. **Las dos puertas de construcción se miden en cada etapa** y no sólo en la `a`: cero referencias hacia el dominio (BT-02) y cero advertencias (BT-03).
3. **La inspección de superficie pública (BT-08) se repite en toda etapa que agregue un campo a un tipo.** No alcanza con haberla corrido una vez.
4. **La matriz de tipos ejercitados por integración (BT-16) se actualiza al cerrar cada etapa.** Es el gate equivalente que reemplaza a la cobertura de líneas, porque este proyecto de código no tiene pruebas propias (`RT-07`).
5. **Ningún guion de prueba que involucre el texto de figuras usa datos inventados**: se usan los escenarios `E-1` a `E-8` del intake §20.

## 6. Riesgos y mitigaciones

| Riesgo | Probabilidad | Impacto | Mitigación |
| --- | --- | --- | --- |
| Que aparezca una referencia hacia `GeometriaFactory-Domain` y el acoplamiento vuelva por esa vía | Media: el intake la nombra como «la vía por la que el acoplamiento vuelve» | Alto | BT-02, puerta bloqueante medida en **cada** etapa, y rechazo en revisión (`05` §9, primer riesgo) |
| Que un campo nuevo transporte una dirección de servicio o una traza, **sin que nadie lo note porque compila** | Media | Alto: viola `RA-03` y expone la topología del producto | BT-08, repetida en toda etapa que agregue un campo, y el criterio 6 de la DoR, que no admite excepción (`05` §9, segundo riesgo) |
| Que el listado incorpore un campo del detalle «porque hace falta en una pantalla» | **Alta**: es la presión natural de la capa de presentación | Medio | BT-13 con umbral cero en las tres dimensiones, y [`ADR-05`](../05-Arquitectura-Tecnica/Adrs/ADR-05-Proyeccion-De-Listado-Separada-Del-Detalle.md), que declara que la proyección existe precisamente para **no** ser el detalle (`05` §9, tercer riesgo) |
| Que una de las dos unidades desplegables se despliegue sin la otra tras un cambio incompatible | Media | Alto: el contrato deja de ser el mismo de los dos lados | Regla operativa de despliegue conjunto, que **09 tiene que materializar** y que este plan eleva como entrada a esa categoría (`05` §9, quinto riesgo; `Vista-Producto.md` §7 `RI-02`) |
| Que los dos extremos se configuren distinto **sin romper ninguna compilación**, y un campo llegue nulo en producción | Media si no se controla | **Alto**: `Vista-Producto.md` §7 lo declara como el único modo de falla del contrato que la compilación compartida no atrapa | BT-17, adoptar la **única** configuración de intercambio declarada en todo el producto, y verificación **ejerciendo el servicio real** desde la batería de integración, no comparando dos archivos |

## 7. Criterios de hecho de cada tramo

Una etapa de este proyecto de código está hecha cuando:

- [ ] Todas sus historias y tareas comprometidas en §3 están en estado terminado.
- [ ] Los criterios comunes a toda transición de [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §5.1 se cumplen, incluida la no regresión sin correcciones.
- [ ] Los criterios propios de la transición correspondiente de su §5.2 que alcanzan a este proyecto de código se cumplen.
- [ ] Las dos puertas de construcción y la inspección de superficie pública están medidas **en esta etapa**.
- [ ] La matriz de tipos ejercitados por integración está al día.
- [ ] El informe de cierre de la etapa está escrito y es autocontenido, con su índice.
- [ ] Los documentos de la categoría 11 afectados están revisados, o se registra que la categoría todavía no está emitida.
- [ ] El Product Owner dio **OK explícito** en el punto de control, y la rama está incorporada antes de abrir la siguiente.

## 8. Trazabilidad

| Etapa | NB que avanzan | CU que avanzan | ADR que gobiernan las decisiones |
| --- | --- | --- | --- |
| `a` | Ninguna: es un hito interno sin capacidad funcional asociada | Ninguno | ADR-01, ADR-03 |
| `c` | NB-01, NB-02, NB-04, NB-08 | CU-01, CU-06 | ADR-01, ADR-02, ADR-04 |
| `d` | NB-01, NB-02 | CU-02, CU-08 | ADR-04 |
| `e` | NB-03, NB-04, NB-07, NB-09 | CU-03, CU-04 | ADR-01, ADR-05 |
| `f` | NB-04, NB-05, NB-06, NB-07 | CU-05, CU-06 | ADR-05 |
| `g` | NB-04, NB-06 | CU-05 | ADR-01, ADR-05 |
| `h` | NB-09, NB-07 | CU-05, CU-07 | ADR-05 |

**Las siete etapas declaran al menos una necesidad de negocio en avance, salvo la `a`**, y esa excepción es del propio roadmap: `a` es un hito interno sin capacidad funcional asociada.

**Puertas técnicas del producto y este proyecto de código.** Ninguna de las cinco se mide sobre este proyecto de código: `PT-01` y `PT-04` son del front y del servicio de datos, `PT-02` y `PT-03` del bundle del visor y `PT-05` del despliegue real. Lo que sí lo alcanza es la consecuencia declarada: una puerta que no pasa **detiene la planificación de las etapas que dependen de ella**.

## 9. Bitácora de avance

**Sin entradas al 2026-08-10.** Ninguna etapa está abierta.

| Fecha | Etapa | Qué se cerró | Qué quedó abierto | Punto de control |
| --- | --- | --- | --- | --- |
| — | — | — | — | — |

La bitácora se completa **al cerrar cada etapa**, junto con el informe de cierre que el intake §15 exige.

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial del mini-plan de `GeometriaFactory-Contracts`. Declara por qué la categoría emite un único artefacto —`equipo_n = 1`, con la constancia de los cuatro que no se emiten— y por qué no se declara capacidad numérica, con el motivo propio de este proyecto de código: su trabajo no es proporcional a su volumen de código. Fija el objetivo de cada uno de los **siete** tramos que toca de las **ocho** etapas comprometidas, compromete **21** historias y **18** tareas técnicas sin inventar ningún identificador, y deja explícitamente **fuera** a US-10, que es de la fase `i…`. Declara el orden de construcción con la familia de error primero, la doble consecuencia del nivel topológico 0 y del contrato que dos proyectos de código compilan a la vez, y **cinco** riesgos con mitigación, incluido el único modo de falla que la compilación compartida no atrapa. |
| 1.1 | 2026-08-11 | **Actualiza la trazabilidad upstream** a la versión del `Product-Backlog.md` de la sección 06, que subieron a **1.1** el 2026-08-11. El `Product-Backlog.md` subió al absorber la promoción de `F-13` a `Must Have` (`PRODUCT-INTAKE` **1.19** §4) y al declarar la regularidad de la distribución MoSCoW (hallazgo `D-06-03`). **Ninguna historia ni tarea técnica de este proyecto de código cambia de prioridad, de etapa ni de tramo**, y ningún compromiso, riesgo ni orden de construcción de este plan se toca: la fila existe para que la versión citada sea la vigente y no una que ya no está. Sube minor. |
