---
doc_id: DOC-API-README-01
doc_type: plan-documental
title: Plan documental — GeometriaFactory-Api
status: Vigente
rol_intervencion: [integrador, mantenedor, operador]
owner: Technical Writer / Documentation Lead (AG-11)
version: "1.1"
last_review: 2026-08-11
momento: 1
traces:
  - PRODUCT-MANIFEST-1.3
  - PRODUCT-INTAKE-1.26
  - Vista-Producto-1.2
---

# Plan documental — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** README.md de la categoría 11
**Versión:** 2.0
**Estado:** Propuesto
**Fecha:** 2026-08-16
**Autor:** Technical Writer / Documentation Lead (AG-11)
**Tipo de proyecto de código (D8):** `rest-api` · nivel topológico 3
**Momento:** 1 — plan documental, sin contenido redactado
**Trazabilidad upstream:** [`../../../Producto/11-Documentacion/README.md`](../../../Producto/11-Documentacion/README.md) 1.1; [`../05-Arquitectura-Tecnica/`](../05-Arquitectura-Tecnica/) con sus **8** ADR; [`../02-Especificacion-Funcional/`](../02-Especificacion-Funcional/) con sus **12** casos de uso
**Tiempo estimado de lectura:** 4 min

> **Los tres campos de estado de este documento responden a dos enums distintos, y eso no es una contradicción sino una dualidad que hasta ahora no estaba declarada.**
>
> | Campo | Enum al que responde | Qué declara | Valor de este README |
> | --- | --- | --- | --- |
> | `status:` del frontmatter | `Rules-Documentacion.md` §4.1: `Planificado`, `Borrador`, `Vigente`, `Potencialmente desactualizado`, `Superado` | **Ciclo de vida del contenido**: si el artefacto está redactado y al día | `Vigente` |
> | Columna `Estado` de la tabla de §2 | El mismo enum, acotado por `Rules-Documentacion.md` §3 punto 3 a `Planificado`, `Vigente` y `Potencialmente desactualizado` | Lo mismo, para cada artefacto del plan | `Vigente` |
> | `**Estado:**` de esta cabecera | `Root-Rules.md` §6: `Borrador`, `Propuesto`, `Aprobado`, `Vigente`, `Superado`, `Archivado` | **Situación de aprobación** del documento dentro del framework | `Propuesto` |
>
> Los dos primeros son el **mismo** enum y por eso tienen que decir lo mismo: hoy dicen `Vigente`, porque este README **sí está redactado** y su `last_review` es la fecha de su última revisión —un artefacto `Planificado` no lleva ninguna—. El tercero es otro eje y dice **`Propuesto`** con fundamento propio: [`../../../Handoff-Checkout.md`](../../../Handoff-Checkout.md) §2 declara que la promoción documental del 2026-08-11 dejó **expresamente fuera** a los ocho README de la categoría 11 —el de `Producto/` y uno por proyecto de código— porque «su contenido está pendiente», la categoría va por el modelo de documentación viva y hoy sólo tiene el Momento 1, y «promoverlos sería sellar un plan como si fuera la documentación». Su promoción es trabajo de la Fase J.


## Resumen ejecutivo

El host del servidor propio y el proyecto de código principal del producto. Expone los **15** puntos de acceso, traduce los códigos del contrato a respuestas del protocolo y prepara el almacén al arrancar. Es el único proyecto de código con los **tres** cuerpos completos: integrador, mantenedor y operador.

**Nada de lo que se enumera acá está redactado.** Este documento es el índice: qué artefactos va a tener este proyecto de código, a qué rol de intervención sirve cada uno y en qué estado está. El estado de todos, salvo el de este propio README, es `Planificado`.

---


## 0. Esta categoría es de la unidad de entrega

**El índice de documentación se consolidó el 2026-08-16.** Es la categoría con **más solapamiento del
inventario de la fusión —33 %—**, y el motivo es que sus documentos son los más formulaicos: cuatro
índices que dicen casi lo mismo sobre árboles distintos. Es el único caso donde la palabra
«duplicado» describía bien lo que había.

**La carpeta `_fusion/` se retira**: la fusión terminó acá. Lo absorbido está en
[`../../../_legacy/2026-08-16-consolidacion-m10/GeometriaFactory-Api/11-Documentacion/`](../../../_legacy/2026-08-16-consolidacion-m10/GeometriaFactory-Api/11-Documentacion/).

## 1. Matriz de ruteo

Actor por intención, hacia el documento que responde. **Todas las celdas apuntan a documentos en estado `Planificado`.**

| Pregunta del lector | Documento |
| --- | --- |
| ¿Cuáles son los puntos de acceso y cuáles no piden credencial? | `Referencia-Api`, con los **15** y los **4** fuera de la guardia |
| Recibí un código de respuesta, ¿qué lo produjo? | `Troubleshooting`, que cita la tabla de traducción ya emitida |
| ¿Qué necesita el servicio para arrancar en un contenedor? | `Guia-Contenedor` |

**Es el único lugar del producto donde se puede violar hacia afuera la regla de que ningún mensaje expone la dirección de un servicio interno.** Su `Referencia-Api` y su `Troubleshooting` tienen que documentar los errores sin reproducir el defecto que describen, y esa es la restricción más dura de todo el cuerpo documental.

## 2. Artefactos planificados

| Artefacto | Cuerpo | Rol de intervención | Estado | Última revisión |
| --- | --- | --- | --- | --- |
| `README.md` | Índice | Todos | **Vigente** (es este documento) | 2026-08-11 |
| `Conceptos-Fundamentales.md` | Integrador | Integrador | Planificado | — |
| `Guia-Onboarding-Developer.md` | Integrador | Integrador | Planificado | — |
| `guia-integracion-front-web.md` | Integrador | Integrador | Planificado | — |
| `Referencia-Api.md` | Integrador | Integrador | Planificado | — |
| `Troubleshooting.md` | Integrador | Integrador | Planificado | — |
| `Glosario-Tecnico.md` | Integrador | Todos | Planificado | — |
| `Recorrido-Codigo.md` | Mantenedor | Mantenedor | Planificado | — |
| `Guia-Contribucion.md` | Mantenedor | Mantenedor | Planificado | — |
| `Guia-Contenedor.md` | Operador | Operador | Planificado | — |
| `Runbook-Operacion.md` | Operador | Operador | Planificado | — |

**11 artefactos en total**, incluido este README.

### 2.1 Las guías de integración y su sistema objetivo

El nombre de cada guía se parametriza con el stack o sistema receptor, nunca con un nombre comercial. El sistema objetivo de cada una sale de una arista real del grafo de dependencias del manifiesto.

| Guía | Sistema objetivo, y de qué arista sale |
| --- | --- |
| `guia-integracion-front-web.md` | La arista de tiempo de ejecución `Web → Api`: el único consumidor de la superficie, servidor a servidor |

**El segmento del sistema objetivo queda sujeto a confirmación en el Momento 2.** Se deriva acá de la arista que lo justifica; si al construir aparece un receptor distinto, el nombre se ajusta y el cambio se registra.

## 3. Gating aplicado

Cada omisión cita la regla o el flag que la produce. **Ninguna se omite por conveniencia.**

| Cuerpo o artefacto | Estado | Fundamento |
| --- | --- | --- |
| Cuerpo integrador | **Obligatorio** | El gating por tipo D8 lo declara obligatorio para `rest-api` |
| Cuerpo mantenedor | **Obligatorio** | Lo es para los ocho tipos D8, sin excepción: todo proyecto de código va a ser retomado por alguien |
| Cuerpo operador | **Obligatorio** | Es una de las **dos** unidades desplegables del producto |
| `Guia-Extension.md` | Omitida | `tiene_extensibilidad` es false en el manifiesto §5 |
| `Referencia-Cli.md` | Omitida | No expone interfaz de línea de comandos oficial |

## 4. Orden de lectura sugerido

| Rol | Orden |
| --- | --- |
| Integrador | `Conceptos-Fundamentales` → `Guia-Onboarding-Developer` → `Referencia-Api` → `Troubleshooting` |
| Mantenedor | `Recorrido-Codigo` → `Guia-Contribucion` |
| Operador | `Guia-Contenedor` → `Runbook-Operacion`, con la guía de despliegue de nivel producto antes de la primera salida |

Antes de cualquiera de los tres conviene leer `Vision-General-Sistema.md` de nivel producto: este proyecto de código no se entiende solo, y lo que le impone el resto no está escrito acá.

## 5. Cómo se mantiene

Los disparadores de actualización son los del producto: cierre de etapa, cierre de incremento demostrable, y cambio que altere un contrato público, un procedimiento de despliegue o una ruta de código citada, que se atiende de inmediato. Un documento sin revisar desde hace más de dos cortes se marca en la tabla de §2 con su fecha visible.

**La regla dura de `Recorrido-Codigo`: toda ruta citada existe.** Una ruta que no resuelve es el error que más caro le sale al mantenedor, porque lo manda a buscar algo que no está.

## 6. Puntos abiertos heredados que este cuerpo va a tener que absorber

Este plan **no resuelve ninguno**. Los registra para que el Momento 2 los encuentre declarados.

| Punto abierto | Documento que lo va a tocar | Titular |
| --- | --- | --- |
| Las rutas y los verbos definitivos de los **15** puntos de acceso | `Referencia-Api` | Product Owner y equipo, en la etapa `a` |
| El alcance de la colección de peticiones reproducible, declarado en dos lugares con alcances distintos | `Guia-Onboarding-Developer` | Product Owner, sobre el intake |
| La vigencia exacta de la credencial firmada, y el valor del límite de tamaño del cuerpo de una petición | `Guia-Contenedor`, `Referencia-Api` | Product Owner |

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial en la Fase H, como parte del **Momento 1** del modelo de documentación viva. Declara los **11** artefactos planificados de este proyecto de código con su cuerpo, su rol de intervención y su estado; la matriz de ruteo; el gating aplicado con el fundamento de cada omisión; el orden de lectura por rol; la cadencia de actualización; y los **3** puntos abiertos heredados que este cuerpo va a tener que absorber, **ninguno de los cuales se resuelve acá**. **Sin contenido redactado**, que es lo que el Momento 1 prescribe. **Autor:** Technical Writer / Documentation Lead (AG-11) |
| 1.1 | 2026-08-11 | **Unifica los tres campos de estado de este documento y declara la dualidad que los separa.** Cierra el hallazgo `P2-2` de [`../../../Audit/H-Final-Consolidado-r1.md`](../../../Audit/H-Final-Consolidado-r1.md) §4, que registraba que los ocho README de la categoría 11 declaran `status: Planificado` en el encabezado estructurado, `Estado: Propuesto` en la cabecera de prosa y `Vigente` en su propia fila de la tabla de artefactos. **La revisión encuentra que no son tres estados de un mismo eje sino dos ejes**: el `status` del frontmatter y la columna `Estado` de la tabla responden al enum de `Rules-Documentacion.md` §4.1 y §3 punto 3 —ciclo de vida del **contenido**—, y el `**Estado:**` de la cabecera responde al de `Root-Rules.md` §6 —situación de **aprobación**—. **Lo que se corrige es una sola celda**: `status` pasa de `Planificado` a **`Vigente`**, porque está en el mismo enum que la fila de la tabla y la fila es la que dice la verdad —este README está redactado y declara `last_review`, cosa que un artefacto `Planificado` no lleva—; [`../../../Handoff-Checkout.md`](../../../Handoff-Checkout.md) §2 usa esa misma lectura al contar «72 menos los **8** README que están `Vigente`». **Lo que no se toca es `Estado: Propuesto`**, correcto y con motivo declarado: la promoción documental del 2026-08-11 dejó a los ocho fuera a propósito y su promoción es trabajo de la Fase J. **Lo que se agrega es la declaración de la dualidad**, para que la próxima lectura no la vuelva a tomar por una contradicción. Ningún artefacto planificado, ningún recuento, ningún gating y ningún orden de lectura cambia. Sube minor. |
| 2.0 | 2026-08-16 | **Consolidación de la fusión.** Pasa a indexar la categoría de la **unidad de entrega**. Entra §0. La carpeta `_fusion/` **se retira**. Sube major. |
