# Mini-Plan — GeometriaFactory-Infrastructure

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** Mini-Plan.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Maintainer Lead (AG-07)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) 1.0 (cinco épicas, veinticinco historias), [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md) 1.0 (veintiséis tareas técnicas) y [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/Definition-Of-Ready.md) 1.0; [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) 1.5 §2.1, §2.2, §4 y §5; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.18** §2, §10, §15, §17.3, §20 y §22; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.1 §5, §8, §9, §10.5 y §11
**Trazabilidad downstream:** `08-Calidad-Y-Pruebas`, `09-Devops` y `11-Documentacion` de GeometriaFactory-Infrastructure

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
| Etapas que toca este proyecto de código | **Cinco**: `a`, `c`, `d`, `e` y `f` |
| Duración de cada etapa | **Sin fecha.** El avance se mide por etapas cerradas (`Roadmap-Producto.md` §1.1) |
| Tamaño del equipo | `equipo_n = 1` (`PRODUCT-INTAKE` §2) |
| Unidad de estimación | **Sin fijar**, por [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §4.1 |
| Nivel topológico | **2**, con dos dependencias de compilación y **un solo consumidor**: la composición de raíz de `GeometriaFactory-Api` |
| Etapas del pipeline | `restore` → `build` → `test` → **verificación de transformaciones de esquema**, que es **propia de este proyecto de código** (`05` §5) |
| Puertas técnicas del producto que lo alcanzan | **`PT-04`**, en su parte de que la imagen **aplique las actualizaciones de esquema sobre base vacía**, medida en la etapa `a` |
| Paralelismo entre etapas | **Ninguno** (`Roadmap-Producto.md` §4) |

### 1.1 Por qué esta categoría emite un mini-plan y no planes de iteración

El intake declara **`equipo_n = 1`** en su §2, y de ese dato el framework deriva que la categoría 07 emita **únicamente** `Mini-Plan.md`; `Roadmap-Producto.md` lo declara en su §2.1, en su §3 y en su §6. **No se emiten** `Plan-Iteracion-Sprint-XX.md`, `Template-Sprint-Review.md`, `Template-Sprint-Retrospectiva.md` ni `Velocidad-Equipo.md`, y su ausencia es decisión declarada y no omisión.

**Y hay un segundo motivo**: este producto **no planifica en sprints**. Su ciclo es etapa, informe de cierre, punto de control bloqueante y fusión.

### 1.2 Capacidad disponible

**No se declara capacidad numérica, y es deliberado.** Ninguna fuente da base: sin plazo calendario, sin iteraciones cerradas y con una sola persona.

Y hay un motivo propio de este proyecto de código: de los **catorce** requerimientos no funcionales de `05` §8, **tres vienen rotulados como asunción** desde el intake y siguen pendientes de confirmación —los 200 ms de la interpretación y las **tres** coberturas, incluida la de **95 %** del validador, que es el número más alto del producto—. Declarar acá una capacidad en puntos agregaría un cuarto número sin respaldo, y este plan **usa los tres primeros como vigentes precisamente porque no los inventó**.

Lo que **sí** limita la capacidad y está declarado es el **cuello de diseño**: el punto de control de cada etapa (`PRODUCT-INTAKE` §10).

## 2. Objetivo de cada tramo

| Etapa | Objetivo de este proyecto de código al cerrar la etapa |
| --- | --- |
| `a` | El proyecto de código existe, **la función de derivación de clave está anclada con sus parámetros versionados**, y el almacén se crea y se transforma solo al arrancar, deteniendo el arranque antes que operar sobre un almacén en el que no se puede confiar. |
| `c` | El almacén sostiene por sí mismo las dos unicidades y responde las dos preguntas sobre el conjunto; la contraseña se deriva y se verifica sin quedar nunca en claro; y el acceso firmado se emite con la clave que se recibe y no se busca. |
| `d` | La contraseña provisoria la produce el sistema, no es adivinable y no se repite; la marca viaja con la cuenta sin ser un estado; y la baja arrastra todos los trabajos, todo o nada. |
| `e` | El trabajo se materializa con su texto literal y todo lo que cuelga de él, la consulta se resuelve sólo con su recorte declarado y el listado no arrastra componentes ni texto. |
| `f` | El texto real del alumno se interpreta con sus cuatro trampas, los valores se verifican con tolerancia **0.01** y operador **estricto**, y **la batería de diez casos pasa con los ocho escenarios del intake como entrada**. |

**Las etapas `b`, `g` y `h` no producen trabajo en este proyecto de código**, y por eso no tienen fila. El motivo, incluido el de la `h` —cuyo aporte ya está construido en la `e`—, está en [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §2.

## 3. Ítems comprometidos por tramo

Los identificadores son los del backlog de 06 y **ninguno se inventa acá**.

| Etapa | ID | Tipo | Descripción corta | Prioridad | Estimación | Asignado | Estado |
| --- | --- | --- | --- | --- | --- | --- | --- |
| `a` | BT-01 | Tarea técnica | Crear el proyecto de código y su proyecto de pruebas | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-02 | Tarea técnica | Fijar nombres y el criterio de nombrado del adaptador de cuentas | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-03 | Tarea técnica | Anclar la función de derivación de clave y sus parámetros versionados | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-05 | Tarea técnica | Contexto de persistencia y mapeo de las cinco entidades | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-08 | Tarea técnica | Fijar la zona horaria y la precisión de los sellos | Media | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-06 | Tarea técnica | Preparación del almacén con linaje inmutable y arranque detenido | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-04 | Tarea técnica | Puerta de construcción con cero advertencias | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-07 | Tarea técnica | Puerta de transformaciones sobre un almacén inexistente | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | US-24 | Historia | Aplicar las transformaciones de esquema al arrancar | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | US-25 | Historia | Detener el arranque en lugar de operar sobre un almacén dudoso | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-09 | Tarea técnica | Adaptador de repositorio de cuentas con el índice único | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-12 | Tarea técnica | Adaptador de reloj del sistema | Media | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-13 | Tarea técnica | Mecanismo de derivación y verificación de credenciales | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-15 | Tarea técnica | Mecanismo de acceso firmado con la clave que recibe y no busca | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-14 | Historia | Sostener en el almacén la unicidad del correo y la del administrador | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-15 | Historia | Responder las dos preguntas sobre el conjunto | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-17 | Historia | Derivar una contraseña sin guardarla ni registrarla en claro | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-18 | Historia | Verificar una credencial y distinguir el derivado ilegible | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-21 | Historia | Emitir el acceso firmado con sus cuatro reclamos | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-22 | Historia | Rechazar la emisión sin clave de firma | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-23 | Historia | Proveer el sello por un puerto | Media | Sin fijar | Equipo (1) | Pendiente |
| `d` | BT-14 | Tarea técnica | Producción de la contraseña provisoria, no adivinable y sin repetirse | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | BT-21 | Tarea técnica | Cerrar el catálogo de las 17 condiciones en las dos direcciones | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | BT-22 | Tarea técnica | Inspección de que ningún mensaje ni traza lleva secreto, ruta ni texto | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | BT-23 | Tarea técnica | Confirmar los valores rotulados como asunción y las tres coberturas | Media | Sin fijar | Equipo (1) | Pendiente |
| `d` | BT-25 | Tarea técnica | Elevar la forma de sostener que la provisoria no se repite | Media | Sin fijar | Equipo (1) | Pendiente |
| `d` | BT-26 | Tarea técnica | Elevar la frecuencia del respaldo y la fecha de última modificación | Baja | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-13 | Historia | Arrastrar todos los trabajos de una cuenta dada de baja | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-16 | Historia | Conservar y transportar la marca sin alterar el estado | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-19 | Historia | Producir una provisoria no adivinable y sin repetirse | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-20 | Historia | Terminar sin producir valor cuando la aleatoriedad no responde | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | BT-10 | Tarea técnica | Adaptador de repositorio de trabajos con la proyección separada | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | BT-11 | Tarea técnica | Retiro físico con todo o nada y arrastre de la baja | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-08 | Historia | Conservar el texto original literal y rechazar toda escritura que lo reemplace | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-09 | Historia | Materializar el trabajo con sus piezas, componentes y observaciones | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-10 | Historia | Resolver la consulta con el recorte ya trasladado al pedido | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-11 | Historia | Excluir componentes y texto original del resultado de un listado | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-12 | Historia | Retirar físicamente un trabajo con todo lo que cuelga de él | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | BT-16 | Tarea técnica | Motor de interpretación con las cuatro trampas del formato | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | BT-19 | Tarea técnica | Fijar la tabla de derivación por tipo | Media | Sin fijar | Equipo (1) | Pendiente |
| `f` | BT-17 | Tarea técnica | Motor de verificación con tolerancia 0.01 y operador estricto | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | BT-20 | Tarea técnica | Puerta de cero peticiones de red de los dos motores | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | BT-24 | Tarea técnica | Elevar hasta dónde llega el conjunto de tipos reconstruibles | Media | Sin fijar | Equipo (1) | Pendiente |
| `f` | BT-18 | Tarea técnica | Batería de diez casos con los ocho escenarios como entrada | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-01 | Historia | Leer el texto real con tolerancia a comas finales y claves sinónimas | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-02 | Historia | Devolver la cantidad de figuras del conjunto raíz | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-03 | Historia | Reconstruir las piezas con su posición y sus componentes | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-04 | Historia | Emitir el error de validación con posición de figura y campo | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-05 | Historia | Derivar el valor desde las dimensiones y los componentes | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-06 | Historia | Comparar con tolerancia absoluta y operador estricto | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-07 | Historia | Emitir la advertencia con el valor declarado y el derivado | Alta | Sin fijar | Equipo (1) | Pendiente |

**Total comprometido: 25 historias y 26 tareas técnicas**, repartidas en cinco etapas. La prioridad de la columna es de **ejecución dentro de la etapa** y no reemplaza a la MoSCoW del backlog.

**US-23 figura con prioridad de ejecución `Media`**, y su MoSCoW en 06 es `Should`: es la única historia de este backlog donde las dos coinciden en señalar lo mismo.

## 4. Alcance técnico y orden de construcción

Esta sección **no redefine arquitectura**: referencia la de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md).

**Orden dentro de cada etapa**, derivado de las dependencias de [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md) §3:

1. `a`: BT-01 primero; **BT-03 temprano**, porque el anclaje de la derivación de clave condiciona los dos mecanismos y es una decisión que el intake asigna a este proyecto de código sin elegir por él; BT-02 en paralelo; después BT-05, BT-08 y BT-06; las dos historias sobre ellos; **BT-04 y BT-07 al cerrar**, porque son puertas y no se miden sobre algo que todavía no compila ni arranca.
2. `c`: BT-09 sobre BT-05; BT-12, BT-13 y BT-15 en paralelo, porque **no dependen del contexto de persistencia**; las siete historias después.
3. `d`: BT-14 sobre BT-13; las cuatro historias; BT-21 y BT-22 al cerrar, porque el catálogo y la inspección de secretos necesitan el conjunto ya producido; BT-23, BT-25 y BT-26 antes del punto de control.
4. `e`: BT-10 y BT-11 sobre BT-05 y BT-09; las cinco historias después.
5. `f`: BT-16 primero; BT-19 y BT-17 sobre él; las siete historias; **BT-18 y BT-20 al cerrar**, porque son la batería y la inspección, y sólo tienen sentido sobre algo terminado.

**Reglas de dependencia interna que ninguna tarea puede cruzar** (`05` §3.2): **ningún adaptador depende de otro adaptador** —el único par acoplado son los dos motores, en una sola dirección: la verificación exige las piezas ya reconstruidas—; **los dos motores, el reloj y el mecanismo de credenciales no dependen del contexto de persistencia**; y **la composición de raíz no es de acá**: este proyecto de código declara sus adaptadores y `GeometriaFactory-Api` los conecta.

**Consecuencia del nivel topológico 2**: dentro de cada etapa, el trabajo de `GeometriaFactory-Domain` y de `GeometriaFactory-Application` va **antes** que el de este proyecto de código —un puerto que allá no exista es un adaptador que acá no se puede escribir— y el de `GeometriaFactory-Api` va **después**. Lo que **no** cambia es el orden de las etapas.

**Y una consecuencia que abarata todo el tramo `f`**: los dos motores **no tocan el almacén y no hacen red**, de modo que la épica del validador entera se puede construir y correr sin base y sin ninguna otra pieza del producto en pie.

## 5. Definition of Done aplicada

**La DoD canónica vive en `08-Calidad-Y-Pruebas` y todavía no está emitida.** Este plan la referencia por destino y **no la redefine**; hasta que exista, lo que gobierna el cierre son los criterios de transición de [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §5.

Criterios específicos que este plan agrega:

1. **La actualización de la categoría 11 forma parte del cierre.** La categoría 11 de este proyecto de código todavía no está emitida; hasta su emisión la condición se cumple de forma vacía y **se registra así en el informe de cierre**.
2. **Las dos puertas propias del pipeline se miden en cada etapa**: construcción sin advertencias y **transformaciones aplicadas solas sobre un almacén inexistente**, que es la cuarta etapa y es propia de acá.
3. **Ningún guion de prueba que involucre el texto de figuras usa datos inventados**: el material son los escenarios `E-1` a `E-8` del intake §20, por la regla de delivery 5 de su §15.
4. **Los tres valores rotulados [ASUNCIÓN] se usan como vigentes y las puertas de cobertura no se declaran bloqueantes en 09** hasta que BT-23 cierre.
5. **La etapa `f` no se cierra sin la batería de diez casos pasando entera.** Es la mitigación declarada del único riesgo de negocio del producto, y su cobertura es la más alta del producto.
6. **Ningún mensaje ni ninguna traza lleva un secreto, la ruta del almacén o el texto del alumno**, verificado **en las dos direcciones** al cerrar cada etapa que agregue condiciones.

## 6. Riesgos y mitigaciones

| Riesgo | Probabilidad | Impacto | Mitigación |
| --- | --- | --- | --- |
| Que el validador se escriba sin leer el análisis y no sirva para el dato que existe | **Alta si no se controla**, así lo declara la fuente | **Muy alto**: es el **único riesgo de negocio del producto cuya mitigación declarada es una batería de pruebas**, y su materialización deja el producto inútil para el dato real | Las **cuatro** trampas escritas **antes de leer texto** (BT-16), la batería de **10** casos con los ocho escenarios (BT-18), la cobertura más alta del producto y la tabla de derivación por tipo (BT-19) |
| Que la provisoria se componga por un contador, la fecha o el correo cuando la fuente de aleatoriedad no responde | Media | **Muy alto**: produce una provisoria adivinable **y el reseteo parece haber funcionado**. Un reseteo que no se completa es recuperable; una provisoria adivinable **no se nota hasta que alguien la usa** | BT-14, con el atajo **escrito como prohibido**, la condición propia y el NFR de **0** provisorias repetidas; y US-20, cuyo entregable es la terminación |
| Que ante la ausencia de clave de firma se genere una al vuelo o se emita sin firmar | Media | **Muy alto**: el sistema arranca, emite accesos y **nadie lo nota hasta que alguien falsifica uno** | BT-15, con la clave que **se recibe y no se busca**, y US-22, con **0** accesos emitidos sin clave |
| Que la preparación del almacén descarte el almacén y lo cree de nuevo ante un esquema que no corresponde | Baja, **pero es el atajo más destructivo del producto** | **Muy alto**: deja el servicio impecable y **sin los trabajos de nadie** | BT-06 y US-25, con arranque detenido y la regla de que **una transformación ya fusionada no se edita** |
| Que la ubicación del almacén caiga hacia una ruta dentro de la imagen cuando el volumen no está montado | Media, **porque es el comportamiento por defecto de casi cualquier biblioteca de acceso a archivos** | Alto: el servicio arranca, acepta trabajos de la comisión entera y **los pierde en el siguiente reemplazo de versión** | BT-06 y la regla de que **la configuración se recibe y no se busca** |
| Que un texto ilegible devuelva la condición de servicio no disponible en lugar de una observación | **Alta**: la categoría 03 declara que ésa es la garantía que más veces se rompe al implementar | Alto: el alumno esperaría a que se recupere **de un problema que no tiene** | US-04 con su tercer criterio, BT-21 con la separación entre **resultado** y **fallo** ejercida, y la segunda regla de refinamiento del backlog |
| Que una consulta de listado arrastre los componentes de cada pieza o el texto original | **Media-alta**: es el comportamiento por defecto de cualquier carga completa de entidad | Medio: rompe el requerimiento de tiempo del listado del administrador | BT-10 y US-11, con **0** componentes cargados verificados sobre la proyección devuelta |
| Que la unicidad del correo se sostenga sólo con la consulta previa del consumidor | Media, **porque la consulta previa no es una garantía por sí sola** | Alto: dos cuentas con el mismo correo hacen que el ingreso deje de ser determinista e `INV-01` deja de valer | BT-09 y US-14, con el índice único como **segunda línea** y su condición declarada como camino |

## 7. Criterios de hecho de cada tramo

Una etapa de este proyecto de código está hecha cuando:

- [ ] Todas sus historias y tareas comprometidas en §3 están en estado terminado.
- [ ] Los criterios comunes a toda transición de [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §5.1 se cumplen, incluida la no regresión sin correcciones.
- [ ] Los criterios propios de la transición correspondiente de su §5.2 que alcanzan a este proyecto de código se cumplen.
- [ ] Las **dos** puertas propias del pipeline pasan: cero advertencias y transformaciones aplicadas solas sobre un almacén inexistente.
- [ ] Ninguna condición nueva quedó fuera del catálogo, y ningún mensaje ni traza lleva secreto, ruta del almacén ni texto del alumno.
- [ ] Para la etapa `a`: la parte de `PT-04` que alcanza a este proyecto de código está medida.
- [ ] Para la etapa `f`: **la batería de diez casos pasa entera**, con los ocho escenarios del intake como entrada y **sin datos inventados**.
- [ ] El informe de cierre de la etapa está escrito y es autocontenido, con su índice.
- [ ] Los documentos de la categoría 11 afectados están revisados, o se registra que la categoría todavía no está emitida.
- [ ] El Product Owner dio **OK explícito** en el punto de control, y la rama está incorporada antes de abrir la siguiente.

## 8. Trazabilidad

| Etapa | NB que avanzan | CU que avanzan | ADR que gobiernan las decisiones |
| --- | --- | --- | --- |
| `a` | NB-03, NB-08 (parcial) | CU-10 | ADR-01, ADR-02, ADR-04, ADR-07 |
| `c` | NB-01, NB-02 | CU-05, CU-06, CU-08, CU-09 | ADR-01, ADR-02, ADR-03, ADR-04 |
| `d` | NB-01, NB-02 | CU-04, CU-05, CU-07 | ADR-02, ADR-05 |
| `e` | NB-03, NB-07 (parcial), NB-09 | CU-03, CU-04 | ADR-01, ADR-02 |
| `f` | NB-04, NB-05, NB-06 (parcial) | CU-01, CU-02 | ADR-06 |

**Las cinco etapas declaran al menos una necesidad de negocio en avance, incluida la `a`**, y en eso este proyecto de código se distingue de los demás: su etapa `a` no es un hito interno vacío, porque la preparación del almacén ya aporta a `NB-03` y a `NB-08` en su parte de que el producto quede en un estado que la pieza pública pueda declarar.

**Puertas técnicas del producto y este proyecto de código.** **`PT-04` lo alcanza** en su parte de que las actualizaciones de esquema se apliquen sobre base vacía, y se mide en la etapa `a`. `PT-01` es del front, `PT-02` y `PT-03` del bundle del visor y del anfitrión, y `PT-05` del despliegue real de la fase `i`. Lo que alcanza a este proyecto de código de las otras cuatro es la consecuencia: **una puerta que no pasa detiene la planificación de las etapas que dependen de ella**.

## 9. Bitácora de avance

**Sin entradas al 2026-08-10.** Ninguna etapa está abierta: el producto está en fase de especificación.

| Fecha | Etapa | Qué se cerró | Qué quedó abierto | Punto de control |
| --- | --- | --- | --- | --- |
| — | — | — | — | — |

La bitácora se completa **al cerrar cada etapa**, junto con el informe de cierre. Para la etapa `a` lo que se registra es **qué función de derivación de clave se ancló y con qué parámetros**; para la `f`, **el resultado de los diez casos de la batería**.

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial del mini-plan de `GeometriaFactory-Infrastructure`. Declara por qué la categoría emite un único artefacto —`equipo_n = 1`, con los cuatro omitidos— y por qué no se declara capacidad numérica, con el fundamento propio de que **tres de los catorce requerimientos no funcionales ya vienen rotulados como asunción sin confirmar**. Fija el objetivo de cada uno de los **cinco** tramos que este proyecto de código toca, compromete las **25** historias y las **26** tareas técnicas del backlog de 06 sin inventar ningún identificador, declara el orden de construcción con **el anclaje de la derivación de clave temprano** y con las tres reglas de dependencia interna que ninguna tarea puede cruzar, referencia la Definition of Done por destino con la constancia de que 08 todavía no está emitida, y declara **ocho** riesgos con mitigación, cuatro de ellos de impacto **muy alto**, incluidos los dos atajos que dejan el sistema aparentemente funcionando. |
