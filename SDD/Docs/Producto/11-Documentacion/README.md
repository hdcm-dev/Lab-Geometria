---
doc_id: DOC-PRODUCTO-README-01
doc_type: plan-documental
title: Plan documental del producto — Fábrica de Geometría
status: Planificado
rol_intervencion: [integrador, mantenedor, operador]
owner: Technical Writer / Documentation Lead (AG-11)
version: "1.0"
last_review: 2026-08-11
momento: 1
traces:
  - PRODUCT-MANIFEST-1.3
  - PRODUCT-INTAKE-1.26
  - Vista-Producto-1.2
  - Pipeline-Producto-1.0
---

# Plan documental del producto — Fábrica de Geometría

**Producto:** Fábrica de Geometría
**Documento:** README.md de la categoría 11 de nivel producto
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Technical Writer / Documentation Lead (AG-11)
**Rol de intervención:** Integrador, mantenedor y operador
**Momento:** 1 — plan documental
**Tiempo estimado de lectura:** 8 min

## Resumen ejecutivo

Este documento es el índice del cuerpo documental de entrega de Fábrica de Geometría: qué documentos van a existir cuando el producto esté construido, a qué rol de intervención sirve cada uno y en qué estado está hoy. **Ninguno tiene contenido redactado todavía**, y eso es exactamente lo que el Momento 1 del modelo de documentación viva prescribe: el plan se emite con el manifiesto confirmado y antes del código, para que se vea desde el principio qué documentación va a existir al final.

Sirve a los tres roles de intervención, pero hoy sirve sobre todo a uno: al Product Owner que quiere saber qué va a recibir. Un lector que venga a usar, mantener u operar el sistema todavía no encuentra acá lo que busca, y esa ausencia está declarada en la columna de estado de cada fila en lugar de disimularse.

---

## Tabla de contenido

- [1. Qué es este cuerpo documental y a quién sirve](#1-qué-es-este-cuerpo-documental-y-a-quién-sirve)
- [2. Matriz de ruteo](#2-matriz-de-ruteo)
- [3. Estado del cuerpo documental](#3-estado-del-cuerpo-documental)
  - [3.1 Artefactos de nivel producto](#31-artefactos-de-nivel-producto)
  - [3.2 Artefactos por proyecto de código](#32-artefactos-por-proyecto-de-código)
  - [3.3 Recuento](#33-recuento)
- [4. Gating aplicado, y las omisiones declaradas](#4-gating-aplicado-y-las-omisiones-declaradas)
- [5. Orden de lectura sugerido por rol](#5-orden-de-lectura-sugerido-por-rol)
- [6. Cómo se mantiene](#6-cómo-se-mantiene)
- [7. Lo que este plan hereda sin decidir](#7-lo-que-este-plan-hereda-sin-decidir)
- [8. Control de cambios](#8-control-de-cambios)

---

## 1. Qué es este cuerpo documental y a quién sirve

La categoría 11 es lo que queda cuando el sistema está construido y alguien de afuera tiene que usarlo, mantenerlo u operarlo. Su lector primario es un agente humano en primer contacto: alguien que no participó de ninguna fase de la especificación y no puede recuperar el contexto preguntándole al equipo que la produjo, porque en buena medida ese equipo fue una secuencia de agentes de IA.

En este producto esa figura tiene nombre y no es hipotética. El intake declara `equipo_n` igual a 1 y una audiencia de dos personas del aula; quien retome el laboratorio dentro de dos cursadas no va a tener a quién preguntarle. **Por eso el cuerpo mantenedor es el que más pesa acá**, y por eso `Recorrido-Codigo` es el documento que este plan considera más caro de omitir: sin él, retomar un proyecto de código obliga a reconstruir el mapa leyendo código.

**Este plan no redacta nada y no decide nada.** Deriva la lista de artefactos del tipo D8 y de los flags de cada proyecto de código, aplicando el gating de la guía de la categoría, y cita el documento que sostiene cada omisión.

## 2. Matriz de ruteo

Actor por intención, hacia el documento que responde. Es lo que permite encontrar el camino sin conocer la estructura de carpetas. **Todas las celdas apuntan hoy a documentos en estado `Planificado`.**

| Rol de intervención | Intención | Documento |
| --- | --- | --- |
| Cualquiera | «¿Qué es esto y por dónde entro?» | `Producto/11-Documentacion/Vision-General-Sistema.md` |
| Cualquiera | «¿Cómo lo levanto entero en una máquina limpia?» | `Producto/11-Documentacion/Guia-Inicio-Rapido.md` |
| Operador | «¿Cómo lo despliego, en qué orden y cómo vuelvo atrás?» | `Producto/11-Documentacion/Guia-Despliegue.md` |
| Operador | «¿Qué necesita este servicio para correr en un contenedor?» | `Proyectos/GeometriaFactory-Api/11-Documentacion/Guia-Contenedor.md` |
| Operador | «Algo falla en ejecución, ¿qué miro?» | `Runbook-Operacion.md` del proyecto de código afectado |
| Operador, mantenedor | «Esto ya le pasó a alguien, ¿cómo lo resolvió?» | `Producto/11-Documentacion/Bitacora-Eventualidades.md` |
| Mantenedor | «¿Dónde vive en el repositorio lo que la arquitectura llama componente?» | `Recorrido-Codigo.md` del proyecto de código |
| Mantenedor | «¿Cómo agrego una funcionalidad de punta a punta sin romper el diseño?» | `Guia-Contribucion.md` del proyecto de código |
| Mantenedor | «¿Cómo corro los tests y qué deberían devolver?» | `Guia-Contribucion.md` del proyecto de código, que cita la estrategia de la categoría 08 |
| Integrador | «¿Cuál es el modelo mental de esta pieza?» | `Conceptos-Fundamentales.md` del proyecto de código |
| Integrador | «¿Cómo llego a mi primer éxito?» | `Guia-Onboarding-Developer.md` del proyecto de código |
| Integrador | «¿Cuál es la superficie exacta y qué devuelve cada operación?» | `Referencia-Api.md` del proyecto de código |
| Integrador | «Me da un error, ¿qué significa?» | `Troubleshooting.md` del proyecto de código |
| Integrador del visor | «¿Cómo extiendo el visor desde su anfitrión?» | `Proyectos/GeometriaFactory-Visor/11-Documentacion/Guia-Extension.md` |
| Agente de IA que codifica | «¿Cómo se construye, cómo se valida y qué no puedo tocar?» | `AGENTS.md` en la raíz del repositorio, derivado de `Contrato-Agentes.md` |

## 3. Estado del cuerpo documental

Estados admitidos: `Planificado`, `Vigente`, `Potencialmente desactualizado`. Hoy **todos** están en `Planificado` y ninguno tiene fecha de última revisión, porque ninguno se redactó.

### 3.1 Artefactos de nivel producto

Se generan una sola vez para todo el producto, bajo `SDD/Docs/Producto/11-Documentacion/`.

| Artefacto | Rol de intervención | Momento en que se emite | Estado | Última revisión |
| --- | --- | --- | --- | --- |
| `README.md` | Todos | 1 | **Vigente** (es este documento) | 2026-08-11 |
| `Vision-General-Sistema.md` | Todos | 2 | Planificado | — |
| `Guia-Inicio-Rapido.md` | Mantenedor, operador | 2 | Planificado | — |
| `Guia-Despliegue.md` | Operador | 2 | Planificado | — |
| `Bitacora-Eventualidades.md` | Operador, mantenedor | 2, con triaje en cada corte | Planificado | — |
| `Contrato-Agentes.md` | Todos | 2, refrescado en cada corrida | Planificado | — |
| `AGENTS.md`, en la **raíz del repositorio** | Agentes de IA | 2, derivado del contrato | Planificado | — |

**`Vision-General-Sistema.md` no duplica la vista de producto de la categoría 05.** La vista documenta la arquitectura como decisión, con su grafo, sus contratos y sus riesgos de integración, y se dirige a quien continúa la cadena de especificación. Este documento describe el sistema como hecho consumado, para quien llega de afuera: qué hace, qué proyectos de código lo componen, cómo se comunican y dónde vive el código de cada uno. Una responde «por qué se decidió así»; la otra, «qué es esto».

**`Guia-Inicio-Rapido` tiene un objetivo duro y una restricción propia de este producto.** El objetivo es un solo comando, o la menor cantidad posible, con verificación al final. La restricción es que el producto tiene **dos** unidades desplegables y el orden de arranque sale del grafo de dependencias del manifiesto, con el entorno de desarrollo contenido como única plataforma de construcción declarada.

### 3.2 Artefactos por proyecto de código

Una fila por proyecto de código, con el detalle desplegado en el README de su propia categoría 11, bajo `Proyectos/<Nombre-Proyecto-Codigo>/11-Documentacion/`.

| Proyecto de código | Tipo D8 | Integrador | Mantenedor | Operador | Artefactos planificados |
| --- | --- | --- | --- | --- | --- |
| `GeometriaFactory-Domain` | `library` | Obligatorio | Obligatorio | No aplica | 9 |
| `GeometriaFactory-Contracts` | `library` | Obligatorio | Obligatorio | No aplica | 10 |
| `GeometriaFactory-Visor` | `library` | Obligatorio | Obligatorio, **con `Guia-Extension`** | No aplica | 11 |
| `GeometriaFactory-Application` | `library` | Obligatorio | Obligatorio | No aplica | 9 |
| `GeometriaFactory-Web` | `web-monolith` | Omitido salvo troubleshooting resumido | Obligatorio | Obligatorio | 6 |
| `GeometriaFactory-Infrastructure` | `library` | Obligatorio | Obligatorio | No aplica | 9 |
| `GeometriaFactory-Api` | `rest-api` | Obligatorio | Obligatorio | Obligatorio | 11 |

**El cuerpo mantenedor es obligatorio en los siete, sin excepción.** Todo proyecto de código va a ser retomado por alguien, y ese alguien puede no haber participado de ninguna fase de su especificación. Es el cambio de fondo del gating de la guía y acá se aplica sin ablandarlo.

**El cuerpo operador existe en dos de los siete**, que son exactamente las dos unidades desplegables: `GeometriaFactory-Api` y `GeometriaFactory-Web`. Las cinco bibliotecas no se despliegan como servicio, de modo que no llevan `Guia-Contenedor` ni `Runbook-Operacion`.

### 3.3 Recuento

| Ámbito | Artefactos planificados |
| --- | --- |
| Nivel producto | 7, incluido el `AGENTS.md` de la raíz del repositorio |
| `GeometriaFactory-Domain` | 9 |
| `GeometriaFactory-Contracts` | 10 |
| `GeometriaFactory-Visor` | 11 |
| `GeometriaFactory-Application` | 9 |
| `GeometriaFactory-Web` | 6 |
| `GeometriaFactory-Infrastructure` | 9 |
| `GeometriaFactory-Api` | 11 |
| **Total** | **72** |

El recuento incluye el `README.md` de cada categoría 11, que es obligatorio siempre. Siete de esos ocho README —los de los proyectos de código— se emiten con este mismo plan y quedan en estado `Propuesto`; el resto de los artefactos queda en `Planificado`.

## 4. Gating aplicado, y las omisiones declaradas

Cada omisión cita la regla que la produce y el flag que la habilita. **Ninguna se omite por conveniencia.**

| Artefacto | Dónde se omite | Fundamento |
| --- | --- | --- |
| Cuerpo operador completo | Las cinco bibliotecas | El gating por tipo D8 declara «no aplica» para `library`: no se despliegan como servicio |
| Cuerpo integrador completo | `GeometriaFactory-Web` | El gating declara el cuerpo integrador opcional para `web-monolith` y sólo si expone API externa. **No la expone**: es hoja del grafo y no publica contrato a nadie, según el intake §14 |
| `Referencia-Cli.md` | Los siete | Ningún proyecto de código expone una interfaz de línea de comandos oficial. Los guiones del repositorio son herramientas de construcción, no producto |
| `Guia-Extension.md` | Seis de los siete | Sólo `GeometriaFactory-Visor` declara `tiene_extensibilidad` igual a true en el manifiesto §5. El punto de extensión del producto es la fachada del visor, con sus **seis** funciones |
| `guia-integracion-<sistema-objetivo>.md` | `GeometriaFactory-Web` | Sin cuerpo integrador no hay guía de integración |

**Una precisión sobre la referencia de la superficie HTTP.** La guía de la categoría prescribe generar o curar `Referencia-Api.md` desde una descripción formal de servicio cuando la API es HTTP. `GeometriaFactory-Api` **declaró el apartamiento** de esa descripción formal en su contrato de superficie, con el fundamento tomado de la fuente. La referencia se cura entonces desde el contrato ya emitido y no desde un esquema que el producto decidió no producir; el plan lo registra acá para que el Momento 2 no lo reabra ni lo invente.

## 5. Orden de lectura sugerido por rol

| Rol | Orden | Por qué |
| --- | --- | --- |
| Integrador | `Vision-General-Sistema` → `Conceptos-Fundamentales` del proyecto de código → `Guia-Onboarding-Developer` → `Referencia-Api` → `Troubleshooting` | Primero el mapa del producto, después el modelo mental de la pieza, y recién entonces la superficie. Al revés, la referencia se lee como una lista de nombres |
| Mantenedor | `Vision-General-Sistema` → `Recorrido-Codigo` → `Guia-Contribucion` → `Guia-Extension` cuando exista | El puente entre arquitectura y árbol de archivos va antes que el procedimiento de cambio: no se puede agregar funcionalidad en un repositorio que no se sabe leer |
| Operador | `Guia-Inicio-Rapido` → `Guia-Contenedor` → `Guia-Despliegue` → `Runbook-Operacion` → `Bitacora-Eventualidades` | Levantarlo una vez en limpio antes de desplegarlo en serio, y el runbook antes de que haga falta |
| Agente de IA que codifica | `AGENTS.md` → `Recorrido-Codigo` → `Guia-Contribucion` | El contrato de contexto primero, porque es lo que se carga al iniciar sesión en el repositorio |

## 6. Cómo se mantiene

El cuerpo documental se actualiza por cortes, no de una sola vez al cierre. Los disparadores son tres, en orden de precedencia: el **cierre de etapa** —que en este producto reemplaza al cierre de sprint, porque el intake planifica por etapas `a` a `h` con punto de control—, el **cierre de incremento demostrable**, y el **cambio que altera un contrato público, un procedimiento de despliegue o una ruta de código citada**, que se atiende de inmediato sin esperar el corte.

**La actualización es parte de la condición de terminado de la etapa.** Una etapa no se declara cerrada con documentos afectados sin revisar. Un documento sin revisar desde hace más de dos cortes aparece marcado como potencialmente desactualizado en la tabla de §3, con su fecha visible, y esa marca es el disparador de la revisión del corte siguiente.

**Qué se espera de quien lee.** Que reporte el punto exacto en el que tuvo que salirse de la documentación. Ese punto es el hallazgo: cada trabada se convierte en una entrada con destino asignado —qué documento y qué sección la absorbe— y se resuelve antes de cerrar el corte.

**El ensayo de entrega es un gate y no lo corre quien redactó.** El ensayo automatizado lo ejecuta el agente en cada corte; el ensayo humano lo corre el Product Owner y es condición para cerrar el Momento 3. El agente que documentó no puede aprobarlo por sí mismo: conoce el sistema porque acaba de documentarlo, y esa contaminación anula la prueba.

**Precondición dura del Momento 2.** No arranca sobre un repositorio sin código. Hoy no hay código, no hay muestras implementadas y no hay tests que corran, de modo que **el cuerpo documental no puede pasar de `Planificado` todavía**, y decirlo es preferible a redactar documentos que describan intenciones y se lean como si describieran hechos.

## 7. Lo que este plan hereda sin decidir

Este plan **no resuelve ningún punto abierto de las fases anteriores**. Los que alcanzan al cuerpo documental se registran acá para que el Momento 2 los encuentre declarados y no los reabra por su cuenta.

| Punto abierto heredado | Qué documento de 11 lo va a tocar | Titular |
| --- | --- | --- |
| Cuántas aristas de compilación tiene el producto, siete u ocho | `Vision-General-Sistema`, en su diagrama de contenedores | Product Owner, sobre el manifiesto |
| El nombre del cuarto puerto, el de repositorio de cuentas | `Recorrido-Codigo` y `Referencia-Api` de `GeometriaFactory-Application` | Product Owner y equipo, en la etapa `a` |
| El umbral numérico de fluidez del visor | `Conceptos-Fundamentales` de `GeometriaFactory-Visor` | Product Owner |
| El alcance de la colección de peticiones reproducible | `Guia-Onboarding-Developer` de `GeometriaFactory-Api` | Product Owner |
| Los nombres definitivos de tipos y espacios de nombres, abiertos en los siete | Todo `Recorrido-Codigo` y toda `Referencia-Api` | El equipo, en el punto de control de la etapa `a` |
| Que el motor de contenedores del destino resuelva la referencia al repositorio | `Guia-Despliegue` y `Guia-Contenedor` de `GeometriaFactory-Api` | Se resuelve midiendo, antes de la etapa `i` |

**El quinto es el que más condiciona a esta categoría.** Mientras los nombres de tipos y de espacios de nombres estén abiertos, ningún `Recorrido-Codigo` puede escribir una ruta verificable, y la regla dura de ese documento es que toda ruta citada exista. Por eso el Momento 2 no puede adelantarse a la etapa `a`.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial en la Fase H. Es el **Momento 1** del modelo de documentación viva: el índice del cuerpo documental de entrega, **sin contenido redactado**. Declara la matriz de ruteo con **quince** entradas de actor por intención; los **siete** artefactos de nivel producto y los **sesenta y cinco** repartidos entre los siete proyectos de código, **setenta y dos** en total, todos en estado `Planificado` salvo los ocho README de categoría; el gating aplicado con sus **cinco** clases de omisión, cada una contra el flag o el tipo D8 que la produce; el orden de lectura para **cuatro** roles; la cadencia de actualización anclada a las etapas del intake; y los **seis** puntos abiertos heredados que este plan **no resuelve**, con el documento de 11 que va a tener que absorber cada uno. **Autor:** Technical Writer / Documentation Lead (AG-11) |
