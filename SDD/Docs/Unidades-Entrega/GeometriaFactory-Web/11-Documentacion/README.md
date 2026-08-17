---
doc_id: DOC-WEB-README-01
doc_type: plan-documental
title: Plan documental — GeometriaFactory-Web
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

# Plan documental — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** README.md de la categoría 11
**Versión:** 2.0
**Estado:** Propuesto
**Fecha:** 2026-08-16
**Autor:** Technical Writer / Documentation Lead (AG-11)
**Tipo de proyecto de código (D8):** `web-monolith` · nivel topológico 1
**Momento:** 1 — plan documental, sin contenido redactado
**Trazabilidad upstream:** [`../../../Producto/11-Documentacion/README.md`](../../../Producto/11-Documentacion/README.md) 1.1; [`../05-Arquitectura-Tecnica/`](../05-Arquitectura-Tecnica/) con sus **7** ADR; [`../02-Especificacion-Funcional/`](../02-Especificacion-Funcional/) con sus **10** casos de uso
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

El front del producto y el único punto de contacto del navegador. Es hoja del grafo: no expone contrato a nadie, y por eso su cuerpo documental **no tiene cuerpo integrador**. Lo que sí tiene, y es obligatorio, es el cuerpo mantenedor y el cuerpo operador: es una de las dos unidades desplegables del producto.

**Nada de lo que se enumera acá está redactado.** Este documento es el índice: qué artefactos va a tener este proyecto de código, a qué rol de intervención sirve cada uno y en qué estado está. El estado de todos, salvo el de este propio README, es `Planificado`.

---


## 0. Esta categoría es de la unidad de entrega

**Los documentos de esta categoría se consolidaron el 2026-08-16**, absorbiendo los de `GeometriaFactory-Visor`. Cada uno lleva una subsección por proyecto de código, con su texto transpuesto sin reescritura.

**Los dos índices decían casi lo mismo sobre árboles distintos**, que es el caso donde la palabra «duplicado» sí describe lo que había.

**La carpeta `_fusion/` se retira**: la fusión terminó acá. Lo absorbido está en
[`../../../_legacy/2026-08-16-consolidacion-m10/GeometriaFactory-Web/11-Documentacion/`](../../../_legacy/2026-08-16-consolidacion-m10/GeometriaFactory-Web/11-Documentacion/).

## 1. Matriz de ruteo

Actor por intención, hacia el documento que responde. **Todas las celdas apuntan a documentos en estado `Planificado`.**

| Pregunta del lector | Documento |
| --- | --- |
| ¿Dónde vive la página que atiende esta ruta de navegación? | `Recorrido-Codigo` |
| ¿Cómo agrego una superficie nueva sin romper el aislamiento del visor? | `Guia-Contribucion` |
| La publicación quedó rota, ¿qué miro y cómo vuelvo atrás? | `Runbook-Operacion`, y la guía de publicación ya emitida en la categoría 09 |

**Es el proyecto de código donde la regla de que ningún guion del navegador invoca el servicio de datos se puede romper sin que nada falle.** Su `Recorrido-Codigo` y su `Guia-Contribucion` tienen que dejar escrito por dónde pasa esa frontera, porque una llamada agregada desde el navegador compila igual y se ve igual.

## 2. Artefactos planificados

| Artefacto | Cuerpo | Rol de intervención | Estado | Última revisión |
| --- | --- | --- | --- | --- |
| `README.md` | Índice | Todos | **Vigente** (es este documento) | 2026-08-11 |
| `Troubleshooting.md` | Integrador, **resumido** | Mantenedor, operador | Planificado | — |
| `Recorrido-Codigo.md` | Mantenedor | Mantenedor | Planificado | — |
| `Guia-Contribucion.md` | Mantenedor | Mantenedor | Planificado | — |
| `Guia-Contenedor.md` | Operador | Operador | Planificado | — |
| `Runbook-Operacion.md` | Operador | Operador | Planificado | — |

**6 artefactos en total**, incluido este README.

## 3. Gating aplicado

Cada omisión cita la regla o el flag que la produce. **Ninguna se omite por conveniencia.**

| Cuerpo o artefacto | Estado | Fundamento |
| --- | --- | --- |
| Cuerpo integrador | **Omitido salvo troubleshooting resumido** | El gating lo declara opcional para `web-monolith` y sólo si expone API externa. Este proyecto de código **no la expone**: es hoja del grafo y no publica contrato a nadie |
| Cuerpo mantenedor | **Obligatorio** | Lo es para los ocho tipos D8, sin excepción: todo proyecto de código va a ser retomado por alguien |
| Cuerpo operador | **Obligatorio** | Es una de las **dos** unidades desplegables del producto |
| `Guia-Extension.md` | Omitida | `tiene_extensibilidad` es false en el manifiesto §5 |
| `Referencia-Cli.md` | Omitida | No expone interfaz de línea de comandos oficial |
| `Guia-Contenedor.md` | Se emite con su nombre canónico | **Esta unidad no se containeriza**: se publica por transferencia de archivos al hosting. El documento conserva el nombre de la guía y documenta el contrato de ejecución de la unidad publicada —variables, puntos de entrada, dependencias de arranque y comprobación de salud—; el apartamiento se declara acá para que el Momento 2 no lo lea como contradicción |

## 4. Orden de lectura sugerido

| Rol | Orden |
| --- | --- |
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
| La versión de plataforma que soporta el hosting, marcada para verificar | `Guia-Contenedor`, `Runbook-Operacion` | Se resuelve midiendo, en la etapa `a` |
| El umbral numérico de tiempo de respuesta: ninguna fuente lo declara | `Runbook-Operacion`, en su tabla de métricas | Product Owner |
| El volumen de la comisión, marcado para verificar: los dos listados no incorporan paginación | `Recorrido-Codigo` | Product Owner |

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial en la Fase H, como parte del **Momento 1** del modelo de documentación viva. Declara los **6** artefactos planificados de este proyecto de código con su cuerpo, su rol de intervención y su estado; la matriz de ruteo; el gating aplicado con el fundamento de cada omisión; el orden de lectura por rol; la cadencia de actualización; y los **3** puntos abiertos heredados que este cuerpo va a tener que absorber, **ninguno de los cuales se resuelve acá**. **Sin contenido redactado**, que es lo que el Momento 1 prescribe. **Autor:** Technical Writer / Documentation Lead (AG-11) |
| 1.1 | 2026-08-11 | **Unifica los tres campos de estado de este documento y declara la dualidad que los separa.** Cierra el hallazgo `P2-2` de [`../../../Audit/H-Final-Consolidado-r1.md`](../../../Audit/H-Final-Consolidado-r1.md) §4, que registraba que los ocho README de la categoría 11 declaran `status: Planificado` en el encabezado estructurado, `Estado: Propuesto` en la cabecera de prosa y `Vigente` en su propia fila de la tabla de artefactos. **La revisión encuentra que no son tres estados de un mismo eje sino dos ejes**: el `status` del frontmatter y la columna `Estado` de la tabla responden al enum de `Rules-Documentacion.md` §4.1 y §3 punto 3 —ciclo de vida del **contenido**—, y el `**Estado:**` de la cabecera responde al de `Root-Rules.md` §6 —situación de **aprobación**—. **Lo que se corrige es una sola celda**: `status` pasa de `Planificado` a **`Vigente`**, porque está en el mismo enum que la fila de la tabla y la fila es la que dice la verdad —este README está redactado y declara `last_review`, cosa que un artefacto `Planificado` no lleva—; [`../../../Handoff-Checkout.md`](../../../Handoff-Checkout.md) §2 usa esa misma lectura al contar «72 menos los **8** README que están `Vigente`». **Lo que no se toca es `Estado: Propuesto`**, correcto y con motivo declarado: la promoción documental del 2026-08-11 dejó a los ocho fuera a propósito y su promoción es trabajo de la Fase J. **Lo que se agrega es la declaración de la dualidad**, para que la próxima lectura no la vuelva a tomar por una contradicción. Ningún artefacto planificado, ningún recuento, ningún gating y ningún orden de lectura cambia. Sube minor. |
| 2.0 | 2026-08-16 | **Consolidación de la fusión.** Pasa a indexar la categoría de la **unidad de entrega**. Entra §0. La carpeta `_fusion/` **se retira**. Sube major. |
