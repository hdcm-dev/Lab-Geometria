---
doc_id: DOC-API-README-01
doc_type: plan-documental
title: Plan documental — GeometriaFactory-Api
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
---

# Plan documental — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Proyecto de código:** `GeometriaFactory-Api`
**Documento:** README.md de la categoría 11
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Technical Writer / Documentation Lead (AG-11)
**Tipo de proyecto de código (D8):** `rest-api` · nivel topológico 3
**Momento:** 1 — plan documental, sin contenido redactado
**Trazabilidad upstream:** [`../../../Producto/11-Documentacion/README.md`](../../../Producto/11-Documentacion/README.md) 1.0; [`../05-Arquitectura-Tecnica/`](../05-Arquitectura-Tecnica/) con sus **8** ADR; [`../02-Especificacion-Funcional/`](../02-Especificacion-Funcional/) con sus **12** casos de uso
**Tiempo estimado de lectura:** 4 min

## Resumen ejecutivo

El host del servidor propio y el proyecto de código principal del producto. Expone los **15** puntos de acceso, traduce los códigos del contrato a respuestas del protocolo y prepara el almacén al arrancar. Es el único proyecto de código con los **tres** cuerpos completos: integrador, mantenedor y operador.

**Nada de lo que se enumera acá está redactado.** Este documento es el índice: qué artefactos va a tener este proyecto de código, a qué rol de intervención sirve cada uno y en qué estado está. El estado de todos, salvo el de este propio README, es `Planificado`.

---

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
