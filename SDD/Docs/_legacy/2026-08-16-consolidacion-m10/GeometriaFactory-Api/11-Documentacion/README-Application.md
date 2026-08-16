---
doc_id: DOC-APPLICATION-README-01
doc_type: plan-documental
title: Plan documental — GeometriaFactory-Application
status: Vigente
rol_intervencion: [integrador, mantenedor]
owner: Technical Writer / Documentation Lead (AG-11)
version: "1.1"
last_review: 2026-08-11
momento: 1
traces:
  - PRODUCT-MANIFEST-1.3
  - PRODUCT-INTAKE-1.26
  - Vista-Producto-1.2
---

# Plan documental — GeometriaFactory-Application

**Producto:** Fábrica de Geometría
**Proyecto de código:** `GeometriaFactory-Application`
**Documento:** README.md de la categoría 11
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Technical Writer / Documentation Lead (AG-11)
**Tipo de proyecto de código (D8):** `library` · nivel topológico 1
**Momento:** 1 — plan documental, sin contenido redactado
**Trazabilidad upstream:** [`../../../Producto/11-Documentacion/README.md`](../../../../../Producto/11-Documentacion/README.md) 1.1; [`../05-Arquitectura-Tecnica/`](../05-Arquitectura-Tecnica/) con sus **6** ADR; [`../02-Especificacion-Funcional/`](../02-Especificacion-Funcional/) con sus **11** casos de uso
**Tiempo estimado de lectura:** 4 min

> **Los tres campos de estado de este documento responden a dos enums distintos, y eso no es una contradicción sino una dualidad que hasta ahora no estaba declarada.**
>
> | Campo | Enum al que responde | Qué declara | Valor de este README |
> | --- | --- | --- | --- |
> | `status:` del frontmatter | `Rules-Documentacion.md` §4.1: `Planificado`, `Borrador`, `Vigente`, `Potencialmente desactualizado`, `Superado` | **Ciclo de vida del contenido**: si el artefacto está redactado y al día | `Vigente` |
> | Columna `Estado` de la tabla de §2 | El mismo enum, acotado por `Rules-Documentacion.md` §3 punto 3 a `Planificado`, `Vigente` y `Potencialmente desactualizado` | Lo mismo, para cada artefacto del plan | `Vigente` |
> | `**Estado:**` de esta cabecera | `Root-Rules.md` §6: `Borrador`, `Propuesto`, `Aprobado`, `Vigente`, `Superado`, `Archivado` | **Situación de aprobación** del documento dentro del framework | `Propuesto` |
>
> Los dos primeros son el **mismo** enum y por eso tienen que decir lo mismo: hoy dicen `Vigente`, porque este README **sí está redactado** y su `last_review` es la fecha de su última revisión —un artefacto `Planificado` no lleva ninguna—. El tercero es otro eje y dice **`Propuesto`** con fundamento propio: [`../../../Handoff-Checkout.md`](../../../../../Handoff-Checkout.md) §2 declara que la promoción documental del 2026-08-11 dejó **expresamente fuera** a los ocho README de la categoría 11 —el de `Producto/` y uno por proyecto de código— porque «su contenido está pendiente», la categoría va por el modelo de documentación viva y hoy sólo tiene el Momento 1, y «promoverlos sería sellar un plan como si fuera la documentación». Su promoción es trabajo de la Fase J.


## Resumen ejecutivo

Los casos de uso y los cuatro puertos: la frontera hacia afuera del dominio. Define el contrato que la capa de adaptadores implementa, de modo que la dependencia se invierte. Su cuerpo documental sirve al integrador que la consume desde la composición de raíz y al mantenedor que agrega un caso de uso.

**Nada de lo que se enumera acá está redactado.** Este documento es el índice: qué artefactos va a tener este proyecto de código, a qué rol de intervención sirve cada uno y en qué estado está. El estado de todos, salvo el de este propio README, es `Planificado`.

---

## 1. Matriz de ruteo

Actor por intención, hacia el documento que responde. **Todas las celdas apuntan a documentos en estado `Planificado`.**

| Pregunta del lector | Documento |
| --- | --- |
| ¿Cuáles son los puertos y quién los implementa? | `Referencia-Api`, y `Recorrido-Codigo` para la ubicación del adaptador |
| ¿Dónde empieza y dónde termina una unidad de trabajo? | `Conceptos-Fundamentales`, que cita la decisión ya emitida |
| ¿Cómo agrego un caso de uso de punta a punta? | `Guia-Contribucion` |

**El cuarto puerto no tiene nombre todavía**, y eso alcanza a dos documentos de esta categoría. El puerto existe y su alcance está decidido; lo que falta es el identificador. Hasta que se fije, ninguna ruta ni ninguna firma que lo mencione puede escribirse como verificable.

## 2. Artefactos planificados

| Artefacto | Cuerpo | Rol de intervención | Estado | Última revisión |
| --- | --- | --- | --- | --- |
| `README.md` | Índice | Todos | **Vigente** (es este documento) | 2026-08-11 |
| `Conceptos-Fundamentales.md` | Integrador | Integrador | Planificado | — |
| `Guia-Onboarding-Developer.md` | Integrador | Integrador | Planificado | — |
| `guia-integracion-biblioteca-de-la-solucion.md` | Integrador | Integrador | Planificado | — |
| `Referencia-Api.md` | Integrador | Integrador | Planificado | — |
| `Troubleshooting.md` | Integrador | Integrador | Planificado | — |
| `Glosario-Tecnico.md` | Integrador | Todos | Planificado | — |
| `Recorrido-Codigo.md` | Mantenedor | Mantenedor | Planificado | — |
| `Guia-Contribucion.md` | Mantenedor | Mantenedor | Planificado | — |

**9 artefactos en total**, incluido este README.

### 2.1 Las guías de integración y su sistema objetivo

El nombre de cada guía se parametriza con el stack o sistema receptor, nunca con un nombre comercial. El sistema objetivo de cada una sale de una arista real del grafo de dependencias del manifiesto.

| Guía | Sistema objetivo, y de qué arista sale |
| --- | --- |
| `guia-integracion-biblioteca-de-la-solucion.md` | Sus dos consumidores —`GeometriaFactory-Infrastructure` y `GeometriaFactory-Api`— comparten stack: un solo sistema objetivo |

**El segmento del sistema objetivo queda sujeto a confirmación en el Momento 2.** Se deriva acá de la arista que lo justifica; si al construir aparece un receptor distinto, el nombre se ajusta y el cambio se registra.

## 3. Gating aplicado

Cada omisión cita la regla o el flag que la produce. **Ninguna se omite por conveniencia.**

| Cuerpo o artefacto | Estado | Fundamento |
| --- | --- | --- |
| Cuerpo integrador | **Obligatorio** | El gating por tipo D8 lo declara obligatorio para `library` |
| Cuerpo mantenedor | **Obligatorio** | Lo es para los ocho tipos D8, sin excepción: todo proyecto de código va a ser retomado por alguien |
| Cuerpo operador | **No aplica** | El gating lo declara así para `library`: no se despliega como servicio |
| `Guia-Extension.md` | Omitida | `tiene_extensibilidad` es false en el manifiesto §5 |
| `Referencia-Cli.md` | Omitida | No expone interfaz de línea de comandos oficial |

## 4. Orden de lectura sugerido

| Rol | Orden |
| --- | --- |
| Integrador | `Conceptos-Fundamentales` → `Guia-Onboarding-Developer` → `Referencia-Api` → `Troubleshooting` |
| Mantenedor | `Recorrido-Codigo` → `Guia-Contribucion` |

Antes de cualquiera de los tres conviene leer `Vision-General-Sistema.md` de nivel producto: este proyecto de código no se entiende solo, y lo que le impone el resto no está escrito acá.

## 5. Cómo se mantiene

Los disparadores de actualización son los del producto: cierre de etapa, cierre de incremento demostrable, y cambio que altere un contrato público, un procedimiento de despliegue o una ruta de código citada, que se atiende de inmediato. Un documento sin revisar desde hace más de dos cortes se marca en la tabla de §2 con su fecha visible.

**La regla dura de `Recorrido-Codigo`: toda ruta citada existe.** Una ruta que no resuelve es el error que más caro le sale al mantenedor, porque lo manda a buscar algo que no está.

## 6. Puntos abiertos heredados que este cuerpo va a tener que absorber

Este plan **no resuelve ninguno**. Los registra para que el Momento 2 los encuentre declarados.

| Punto abierto | Documento que lo va a tocar | Titular |
| --- | --- | --- |
| El identificador del cuarto puerto, el de repositorio de cuentas | `Referencia-Api`, `Recorrido-Codigo` | Product Owner y equipo, en la etapa `a` |
| El **sello de desenlace**, que ninguna fuente declara como atributo del trabajo. **Enunciado acotado**: los otros dos sellos ya no son discrepancia —[`../../GeometriaFactory-Domain/02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md`](../../../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md) §2.2 declara la fecha de creación y la de última modificación del trabajo como atributos aportados por el consumidor—. Ver [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §11 `PA-04` | `Conceptos-Fundamentales` | Product Owner, sin fecha comprometida |
| La herramienta que calcula la versión desde los mensajes de confirmación | `Guia-Contribucion` | El equipo, en la etapa `a` |

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial en la Fase H, como parte del **Momento 1** del modelo de documentación viva. Declara los **9** artefactos planificados de este proyecto de código con su cuerpo, su rol de intervención y su estado; la matriz de ruteo; el gating aplicado con el fundamento de cada omisión; el orden de lectura por rol; la cadencia de actualización; y los **3** puntos abiertos heredados que este cuerpo va a tener que absorber, **ninguno de los cuales se resuelve acá**. **Sin contenido redactado**, que es lo que el Momento 1 prescribe. **Autor:** Technical Writer / Documentation Lead (AG-11) |
| 1.1 | 2026-08-11 | **Unifica los tres campos de estado de este documento y declara la dualidad que los separa.** Cierra el hallazgo `P2-2` de [`../../../Audit/H-Final-Consolidado-r1.md`](../../../../../Audit/H-Final-Consolidado-r1.md) §4, que registraba que los ocho README de la categoría 11 declaran `status: Planificado` en el encabezado estructurado, `Estado: Propuesto` en la cabecera de prosa y `Vigente` en su propia fila de la tabla de artefactos. **La revisión encuentra que no son tres estados de un mismo eje sino dos ejes**: el `status` del frontmatter y la columna `Estado` de la tabla responden al enum de `Rules-Documentacion.md` §4.1 y §3 punto 3 —ciclo de vida del **contenido**—, y el `**Estado:**` de la cabecera responde al de `Root-Rules.md` §6 —situación de **aprobación**—. **Lo que se corrige es una sola celda**: `status` pasa de `Planificado` a **`Vigente`**, porque está en el mismo enum que la fila de la tabla y la fila es la que dice la verdad —este README está redactado y declara `last_review`, cosa que un artefacto `Planificado` no lleva—; [`../../../Handoff-Checkout.md`](../../../../../Handoff-Checkout.md) §2 usa esa misma lectura al contar «72 menos los **8** README que están `Vigente`». **Lo que no se toca es `Estado: Propuesto`**, correcto y con motivo declarado: la promoción documental del 2026-08-11 dejó a los ocho fuera a propósito y su promoción es trabajo de la Fase J. **Lo que se agrega es la declaración de la dualidad**, para que la próxima lectura no la vuelva a tomar por una contradicción. **Y se corrige en §6 un punto abierto que envejeció**: de los tres sellos, la fecha de creación y la de última modificación del trabajo **sí** están declaradas como atributos por el modelo del dominio desde `Definicion-Modelo-De-Dominio.md` 1.6, y lo que sigue abierto es el **sello de desenlace**, en línea con `05` §11 `PA-04`. Ningún artefacto planificado, ningún recuento, ningún gating y ningún orden de lectura cambia. Sube minor. |
