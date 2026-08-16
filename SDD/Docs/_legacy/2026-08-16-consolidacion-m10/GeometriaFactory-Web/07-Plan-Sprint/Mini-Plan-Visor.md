# Mini-Plan — GeometriaFactory-Visor

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Visor
**Documento:** Mini-Plan.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Scrum Master + Maintainer Lead (AG-07)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../06-Backlog-Tecnico/Product-Backlog.md`](../../../06-Backlog-Tecnico/_fusion/Visor/Product-Backlog.md) **1.1**, [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../../../06-Backlog-Tecnico/_fusion/Visor/Backlog-Tecnico.md) **1.1** y [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../../../06-Backlog-Tecnico/_fusion/Visor/Definition-Of-Ready.md) 1.0; [`../../../00-Contexto/Roadmap-Producto.md`](../../../../../00-Contexto/Roadmap-Producto.md) 1.5 §2.1, §2.2, §4 y §5; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.19** §2, §10, §15, §16.1, §17.7 y §18; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/_fusion/Visor/Arquitectura-Proyecto-Codigo.md) 1.0 §5, §8, §9 y §11; [`../../../Producto/Vista-Producto.md`](../../../../../Producto/Vista-Producto.md) 1.1 §3, §4 y §7
**Trazabilidad downstream:** `08-Calidad-Y-Pruebas`, `09-Devops`, `10-Examples` y `11-Documentacion` de GeometriaFactory-Visor

---

## Tabla de contenido

- [1. Información general](#1-información-general)
  - [1.1 Por qué esta categoría emite un mini-plan y no planes de iteración](#11-por-qué-esta-categoría-emite-un-mini-plan-y-no-planes-de-iteración)
  - [1.2 Capacidad disponible](#12-capacidad-disponible)
  - [1.3 Los tres tramos de este proyecto de código](#13-los-tres-tramos-de-este-proyecto-de-código)
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
| Etapas que toca este proyecto de código | **Dos**: `a` y `g`, más el **momento de medición** de `PT-02` y `PT-03`, que precede a la `g` |
| Duración de cada etapa | **Sin fecha.** El avance se mide por etapas cerradas (`Roadmap-Producto.md` §1.1) |
| Tamaño del equipo | `equipo_n = 1` (`PRODUCT-INTAKE` §2) |
| Unidad de estimación | **Sin fijar**, por [`../06-Backlog-Tecnico/Product-Backlog.md`](../../../06-Backlog-Tecnico/_fusion/Visor/Product-Backlog.md) §4.1 |
| Nivel topológico | **0**, sin dependencias salientes (`Vista-Producto.md` §3) |
| Etapas del pipeline | Instalación reproducible de dependencias → empaquetado → copia al directorio de recursos estáticos del anfitrión (`05` §5) |
| Paralelismo entre etapas | **Ninguno** entre etapas del producto (`Roadmap-Producto.md` §4) |

### 1.1 Por qué esta categoría emite un mini-plan y no planes de iteración

El intake declara **`equipo_n = 1`** en su §2, y de ese dato el framework deriva que la categoría 07 emita **únicamente** `Mini-Plan.md`; `Roadmap-Producto.md` lo declara en su §2.1, en su §3 y en su §6. **No se emiten** `Plan-Iteracion-Sprint-XX.md`, `Template-Sprint-Review.md`, `Template-Sprint-Retrospectiva.md` ni `Velocidad-Equipo.md`.

El segundo motivo es del producto: **no planifica en sprints**. Su ciclo es etapa, informe de cierre, punto de control bloqueante y fusión.

### 1.2 Capacidad disponible

**No se declara capacidad numérica, y es deliberado.** Ninguna fuente da base: el intake declara sin plazo calendario, no hay iteraciones cerradas y el equipo es de una persona.

Y hay un motivo que en este proyecto de código pesa más que en los otros dos: **la categoría 05 ya se negó a inventar el único umbral numérico que le faltaba**, el de fluidez de la interacción, con el fundamento de que un valor inventado se propagaría a 08 como si fuera del producto (`05` §8, cierre). Declarar acá una capacidad en puntos sería hacer exactamente lo que ese documento evitó, un escalón más abajo.

### 1.3 Los tres tramos de este proyecto de código

Este proyecto de código no reparte su trabajo en seis o siete etapas como los otros dos de nivel 0: lo concentra en **dos etapas y un momento**.

| Tramo | Qué es | Fuente |
| --- | --- | --- |
| Etapa `a` | Etapa del producto. El bundle es **vacío pero real** | `PRODUCT-INTAKE` §15 |
| **Antes de comprometer la etapa `g`** | **Un momento declarado del roadmap, no una etapa.** Es donde se miden `PT-02` y `PT-03`, y una puerta que no pasa **detiene la planificación de la etapa que depende de ella** | `Roadmap-Producto.md` §2.2 y §5.2 |
| Etapa `g` | Etapa del producto. La visualización y el árbol se integran para los dos papeles | `PRODUCT-INTAKE` §15 |

**Este plan no crea una etapa nueva ni renombra ninguna.** El tramo del medio es un momento que el roadmap ya declara, y reflejarlo es lo que impide leer que todo el visor se construye dentro de la etapa `g`, que sería falso y llevaría a comprometer esa etapa sin haber medido lo que la condiciona.

## 2. Objetivo de cada tramo

| Tramo | Objetivo de este proyecto de código al cerrarlo |
| --- | --- |
| Etapa `a` | El proyecto del bundle existe, su construcción es reproducible desde el entorno de desarrollo y produce un archivo vacío pero real, copiado al directorio de recursos estáticos del anfitrión. |
| Antes de comprometer `g` | El bundle carga en una página del anfitrión, crea la escena, dibuja las tres figuras del escenario semilla, sincroniza por índice, libera sus recursos y funciona sin acceso a redes externas: las dos puertas están medidas. |
| Etapa `g` | La persona ve el trabajo en tres dimensiones y como árbol dentro del producto, con los dos movimientos automáticos gobernados por separado, y el punto de extensión tiene su demostración sin backend. |

**Ninguna otra etapa produce trabajo en este proyecto de código.** Las etapas `b` a `f` no dibujan nada, y en la `h` la fachada dibuja el mismo trabajo para el alumno y para el administrador **sin saber cuál de los dos lo mira**, que es lo que `RA-02` exige.

## 3. Ítems comprometidos por tramo

Los identificadores son los del backlog de 06 y **ninguno se inventa acá**.

| Tramo | ID | Tipo | Descripción corta | Prioridad | Estimación | Asignado | Estado |
| --- | --- | --- | --- | --- | --- | --- | --- |
| `a` | BT-12001 | Tarea técnica | Crear el proyecto del bundle con su cadena de construcción reproducible | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-12002 | Tarea técnica | Guion de construcción propio del bundle, para el ciclo corto | Media | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-12003 | Tarea técnica | Decidir si el bundle generado se versiona o se ignora | Media | Sin fijar | Equipo (1) | Pendiente |
| Antes de `g` | BT-12004 | Tarea técnica | Fachada plana con las seis funciones | Alta | Sin fijar | Equipo (1) | Pendiente |
| Antes de `g` | BT-12005 | Tarea técnica | Registro de instancias con su invalidación | Alta | Sin fijar | Equipo (1) | Pendiente |
| Antes de `g` | BT-12006 | Tarea técnica | Los siete códigos de condición desde su fuente única | Alta | Sin fijar | Equipo (1) | Pendiente |
| Antes de `g` | BT-12007 | Tarea técnica | Lector del texto con las variantes de clave del emisor | Alta | Sin fijar | Equipo (1) | Pendiente |
| Antes de `g` | BT-12008 | Tarea técnica | Servicio de dibujo | Alta | Sin fijar | Equipo (1) | Pendiente |
| Antes de `g` | BT-12009 | Tarea técnica | Anclar la versión del motor de dibujo y confinarlo a la capa 3 | Alta | Sin fijar | Equipo (1) | Pendiente |
| Antes de `g` | BT-12010 | Tarea técnica | Disposición derivada del índice | Alta | Sin fijar | Equipo (1) | Pendiente |
| Antes de `g` | BT-12012 | Tarea técnica | Liberar recursos y cortar el bucle al destruir | Alta | Sin fijar | Equipo (1) | Pendiente |
| Antes de `g` | BT-12013 | Tarea técnica | Medir la puerta `PT-03` sobre el bundle generado | Alta | Sin fijar | Equipo (1) | Pendiente |
| Antes de `g` | BT-12014 | Tarea técnica | Medir la puerta `PT-02` sobre una página del anfitrión | Alta | Sin fijar | Equipo (1) | Pendiente |
| Antes de `g` | BT-12016 | Tarea técnica | Inspeccionar la superficie del bundle generado | Alta | Sin fijar | Equipo (1) | Pendiente |
| Antes de `g` | US-12001 | Historia | Crear una instancia del visor sobre un elemento de dibujo | Alta | Sin fijar | Equipo (1) | Pendiente |
| Antes de `g` | US-12004 | Historia | Dibujar las piezas del texto del trabajo | Alta | Sin fijar | Equipo (1) | Pendiente |
| Antes de `g` | US-12009 | Historia | Resaltar en exclusiva la pieza del índice indicado | Alta | Sin fijar | Equipo (1) | Pendiente |
| Antes de `g` | US-12011 | Historia | Liberar los recursos de la instancia y cortar su bucle de dibujo | Alta | Sin fijar | Equipo (1) | Pendiente |
| `g` | BT-12011 | Tarea técnica | Gobierno de los dos movimientos automáticos en el bucle | Alta | Sin fijar | Equipo (1) | Pendiente |
| `g` | BT-12015 | Tarea técnica | Página integradora sin backend, sample `S-1` | Alta | Sin fijar | Equipo (1) | Pendiente |
| `g` | BT-12017 | Tarea técnica | Fijar los nombres internos de funciones, clases y campos | Media | Sin fijar | Equipo (1) | Pendiente |
| `g` | BT-12018 | Tarea técnica | Resolver el umbral de fluidez, o dejarlo declaradamente cualitativo | Media | Sin fijar | Equipo (1) | Pendiente |
| `g` | US-12002 | Historia | Fijar el estado inicial de los dos movimientos al crear la instancia | Alta | Sin fijar | Equipo (1) | Pendiente |
| `g` | US-12003 | Historia | Informar la ausencia de capacidad gráfica en lugar de fallar en silencio | Alta | Sin fijar | Equipo (1) | Pendiente |
| `g` | US-12005 | Historia | Leer las dimensiones con las variantes de clave del emisor | Alta | Sin fijar | Equipo (1) | Pendiente |
| `g` | US-12006 | Historia | Enumerar toda pieza no dibujada con su índice y su condición | Alta | Sin fijar | Equipo (1) | Pendiente |
| `g` | US-12007 | Historia | Devolver la estructura del texto para que el anfitrión arme el árbol | Alta | Sin fijar | Equipo (1) | Pendiente |
| `g` | US-12008 | Historia | Derivar la disposición de cada pieza de su índice | Media | Sin fijar | Equipo (1) | Pendiente |
| `g` | US-12010 | Historia | Ajustar la escena al tamaño del elemento de dibujo | Alta | Sin fijar | Equipo (1) | Pendiente |
| `g` | US-12012 | Historia | Gobernar en vivo los dos movimientos automáticos | Alta | Sin fijar | Equipo (1) | Pendiente |
| `g` | US-12013 | Historia | Detener el movimiento al arrastrar y al no estar visible | Alta | Sin fijar | Equipo (1) | Pendiente |
| `g` | US-12014 | Historia | Ejercitar las seis funciones desde una página integradora sin backend | Alta | Sin fijar | Equipo (1) | Pendiente |

**Total comprometido: 14 historias y 18 tareas técnicas**, repartidas en dos etapas y un momento. **Las catorce historias están dentro del tramo comprometido de ocho etapas**: este proyecto de código no tiene ninguna de la fase `i…`.

**US-12008 y US-12009 figuran con prioridad de ejecución `Media` y `Alta` respectivamente**, y su MoSCoW en 06 es **`Must` en las dos** desde el 2026-08-10. La diferencia entre las dos columnas subsiste y tiene el mismo motivo de siempre: la prioridad de ejecución ordena **dentro** de la etapa y no dice qué se difiere, de modo que dos historias igual de comprometidas pueden tener orden distinto. Lo que desapareció es la contradicción: US-12009 está dentro de lo que `PT-02` mide y por eso su ejecución no era diferible **aunque su MoSCoW lo admitiera**, y esa tensión —que 06 elevó como `PA-06` y que este plan se negó a resolver subiéndole la prioridad— la **cerró el Product Owner** promoviendo `F-13` a `Must Have` en `PRODUCT-INTAKE` **1.19**.

## 4. Alcance técnico y orden de construcción

Esta sección **no redefine arquitectura**: referencia la de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/_fusion/Visor/Arquitectura-Proyecto-Codigo.md).

**Orden**, derivado de las dependencias de [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../../../06-Backlog-Tecnico/_fusion/Visor/Backlog-Tecnico.md) §3:

1. `a`: BT-12001 primero; BT-12002 y BT-12003 sobre él.
2. Antes de `g`: **BT-12009 temprano**, porque anclar la versión del motor condiciona toda la capa 3 y `05` §9 le asigna probabilidad **alta** al cambio de interfaz que puede exigir. Después BT-12004 y BT-12005, después BT-12007, después BT-12008 sobre los tres, después BT-12010 y BT-12012. Las cuatro historias del tramo, sobre esas tareas. **Al final BT-12013, BT-12014 y BT-12016**, que son las mediciones y sólo tienen sentido sobre algo terminado.
3. `g`: BT-12011 sobre BT-12004, BT-12008 y BT-12010; las diez historias del tramo; BT-12015 al final, porque el sample recorre las seis funciones; BT-12017 y BT-12018 antes del punto de control.

**Regla de dependencias entre capas, que ninguna tarea puede cruzar**: la capa 1 no conoce el interior, la capa 2 no contiene lógica de dibujo y la capa 3 no conoce al anfitrión (`05` §3.1).

**Consecuencia del nivel topológico 0**, que acá es más fuerte que en los otros dos proyectos de código del mismo nivel: **el bundle se ejercita sin backend**, de modo que todo el tramo del medio se puede construir y medir sin que exista ninguna otra pieza del producto más allá de una página que lo cargue. Es lo que hace realizable la exigencia del roadmap de medir `PT-02` y `PT-03` antes de comprometer la etapa `g`.

## 5. Definition of Done aplicada

**La DoD canónica vive en `08-Calidad-Y-Pruebas` y todavía no está emitida.** Este plan la referencia por destino y **no la redefine**; hasta que exista, lo que gobierna el cierre son los criterios de transición de [`../../../00-Contexto/Roadmap-Producto.md`](../../../../../00-Contexto/Roadmap-Producto.md) §5 y las dos puertas técnicas.

Criterios específicos que este plan agrega:

1. **La actualización de la categoría 11 forma parte del cierre.** La categoría 11 de este proyecto de código todavía no está emitida; hasta su emisión la condición se cumple de forma vacía y **se registra así en el informe de cierre**.
2. **Las mediciones de ausencia se hacen con sus condiciones declaradas**, que `../02-Especificacion-Funcional/Especificacion-Funcional.md` §6 fija como lugar único: cero red **con los dos movimientos prendidos y sostenidos**, y los diez recorridos **también con los movimientos prendidos**. Una medición sin esas condiciones no cuenta como hecha.
3. **La verificación de cero red se hace sobre el bundle generado y no sólo sobre el código fuente.**
4. **La etapa `g` no se compromete sin `PT-02` y `PT-03` medidas.** Una puerta que no pasa detiene la planificación y **no se arrastra como deuda**.
5. **El material de dibujo son los escenarios `E-1` y `E-7` del intake §20**, y para `DIMENSION_NO_LEGIBLE` el `E-8`. **No se inventan textos de prueba.**
6. **El sample `S-1` está funcionando al cerrar la etapa `g`**: es el punto de extensión declarado del producto y su demostración, no un agregado de conveniencia.

## 6. Riesgos y mitigaciones

| Riesgo | Probabilidad | Impacto | Mitigación |
| --- | --- | --- | --- |
| Que aparezca una petición de red en el bundle, por comodidad o **por una dependencia que la haga por dentro** | Baja para la primera causa, **media para la segunda** | **Muy alto**: reabre contenido mixto, restricción de origen cruzado y exposición de la dirección del servidor propio, y rompe `RA-01` a través de `RA-02` | BT-12016, inspección con cero ocurrencias de las tres formas de petición **en el código fuente y en el bundle generado**, más el conteo en la pestaña de red con los movimientos prendidos (`05` §9, primer riesgo) |
| Que la versión del motor de dibujo que se ancle exija una interfaz distinta de la del visualizador previo | **Alta**: el intake ya lo anticipa, porque el visualizador previo reimplementa la cámara orbital a mano por una carencia de su versión | Medio: retrabajo acotado a la capa 3 | BT-12009 **temprano** en el tramo del medio, y el confinamiento del motor a la capa 3 que [`ADR-12004`](../../../05-Arquitectura-Tecnica/Adrs/ADR-12004-Motor-De-Dibujo-Empaquetado-Y-Aislado.md) declara (`05` §9, cuarto riesgo) |
| Que un bucle de dibujo sobreviva a la destrucción y se acumule al recorrer trabajos | Media | Alto: degradación progresiva, que es lo que `PT-02` mide | BT-12012 y BT-12014, con los diez recorridos medidos **con los movimientos prendidos**, que es su peor caso (`05` §9, tercer riesgo) |
| Que el anfitrión termine dependiendo de nombres internos del motor de dibujo y el motor deje de ser reemplazable | Media: es la presión natural cuando una pantalla necesita algo que la fachada no expone | Alto: se pierde el punto de extensión declarado del producto | [`ADR-12001`](../../../05-Arquitectura-Tecnica/Adrs/ADR-12001-Tres-Capas-Con-Fachada-Plana.md) y [`Extensibilidad.md`](../../../05-Arquitectura-Tecnica/Extensibilidad.md) §5, que declara qué se hace cuando falta algo en la fachada (`05` §9, segundo riesgo) |
| Que se acuñe un código de condición fuera de la categoría 02 | Media: el catálogo de 03 ya creció **sin** que creciera el conjunto de códigos, y esa distinción es fácil de perder | Medio: el conjunto deja de ser cerrado y 03 y 08 se desincronizan | BT-12006 y el criterio 6 de la DoR, que no admite excepción: los códigos son siete, su fuente única es el contrato de fachada y un curso nuevo es fila de curso y no código (`05` §9, sexto riesgo) |
| Que la etapa `g` se comprometa antes de medir `PT-02` y `PT-03` | Media, porque el trabajo de este proyecto de código se lee fácilmente como si viviera **dentro** de la etapa `g` | Alto: es exactamente lo que la regla de puertas del intake prohíbe | Los **tres tramos** de §1.3 y la épica EP-12002 del backlog, que existen para que ese momento sea visible en la planificación y no una nota al pie |

## 7. Criterios de hecho de cada tramo

Un tramo de este proyecto de código está hecho cuando:

- [ ] Todas sus historias y tareas comprometidas en §3 están en estado terminado.
- [ ] Los criterios comunes a toda transición de [`../../../00-Contexto/Roadmap-Producto.md`](../../../../../00-Contexto/Roadmap-Producto.md) §5.1 se cumplen, incluida la no regresión sin correcciones.
- [ ] Las **seis** propiedades transversales se midieron **con sus condiciones declaradas**.
- [ ] Para el tramo del medio: `PT-02` y `PT-03` están medidas y pasan, **antes** de que la etapa `g` se comprometa.
- [ ] Para la etapa `g`: los criterios propios de la transición `g` → `h` de §5.2 que alcanzan a este proyecto de código se cumplen, incluido el gobierno independiente de los dos movimientos automáticos.
- [ ] El informe de cierre de la etapa está escrito y es autocontenido, con su índice.
- [ ] Los documentos de la categoría 11 afectados están revisados, o se registra que la categoría todavía no está emitida.
- [ ] El Product Owner dio **OK explícito** en el punto de control, y la rama está incorporada antes de abrir la siguiente.

## 8. Trazabilidad

| Tramo | NB que avanzan | CU que avanzan | ADR que gobiernan las decisiones |
| --- | --- | --- | --- |
| Etapa `a` | Ninguna: es un hito interno sin capacidad funcional asociada | Ninguno | ADR-12006 |
| Antes de comprometer `g` | NB-00006, y NB-00004 en su parte de piezas dibujadas | CU-12001, CU-12002, CU-12003, CU-12005 | ADR-12001, ADR-12002, ADR-12003, ADR-12004, ADR-12005 |
| Etapa `g` | NB-00006, NB-00004 (parcial), y NB-00008 por contribución negativa | CU-12001, CU-12002, CU-12004, CU-12006, CU-12007 | ADR-12001, ADR-12002, ADR-12003, ADR-12006 |

**Este proyecto de código sostiene una sola necesidad de negocio entera, `NB-00006`**, y toca otras dos parcialmente: `NB-00004` sólo en la parte de que las piezas se dibujen, y `NB-00008` por contribución **negativa** —no hacer red—, que se verifica en `CU-12006` pero que no implementa ninguna capacidad de esa necesidad (`02` §5.3). Las **seis** restantes las implementan otros proyectos de código y no quedan sin cubrir por esta declaración.

**Puertas técnicas del producto y este proyecto de código.** `PT-02` y `PT-03` **son de este proyecto de código** y se miden antes de comprometer la etapa `g`; son las dos únicas de las cinco que lo alcanzan. `PT-01` y `PT-04` son del front y del servicio de datos, y `PT-05` del despliegue real de la fase `i`.

## 9. Bitácora de avance

**Sin entradas al 2026-08-10.** Ningún tramo está abierto.

| Fecha | Tramo | Qué se cerró | Qué quedó abierto | Punto de control |
| --- | --- | --- | --- | --- |
| — | — | — | — | — |

La bitácora se completa **al cerrar cada tramo**. Para el tramo del medio, lo que se registra es el **resultado de las dos puertas**, que es lo que habilita a comprometer la etapa `g`.

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial del mini-plan de `GeometriaFactory-Visor`. Declara por qué la categoría emite un único artefacto —`equipo_n = 1`— y por qué no se declara capacidad numérica, con el fundamento propio de que la categoría 05 ya se negó a inventar el único umbral que le faltaba. Declara los **tres tramos** de este proyecto de código —etapa `a`, el **momento** de medición de `PT-02` y `PT-03`, y etapa `g`— con la constancia explícita de que el del medio **no es una etapa nueva** sino un momento que el roadmap ya declara. Compromete las **14** historias y las **18** tareas técnicas del backlog de 06 sin inventar ningún identificador, declara el orden de construcción con el anclaje del motor de dibujo temprano, y **seis** riesgos con mitigación, incluido el de comprometer la etapa `g` antes de medir sus puertas. Registra la tensión de `PA-06` sin resolverla reprioritizando. |
| 1.1 | 2026-08-11 | **Absorbe la promoción de `F-13` a `Must Have`** (`PRODUCT-INTAKE` **1.19** §4), que cierra la tensión de `PA-06`. La cabecera pasa a citar el intake **1.19** y las versiones **1.1** de los dos artefactos de 06 que este plan consume. **§3**: la nota de US-12008 y US-12009 declara que su MoSCoW es hoy `Must` en las dos, conserva el motivo por el que la prioridad de ejecución sigue siendo distinta —ordena dentro de la etapa, no dice qué se difiere— y registra el desenlace de la tensión. **Ningún tramo, ningún orden de construcción, ningún riesgo y ningún compromiso cambia**: las catorce historias ya estaban comprometidas en este plan, que es precisamente lo que hacía visible la contradicción. Sube minor. |
