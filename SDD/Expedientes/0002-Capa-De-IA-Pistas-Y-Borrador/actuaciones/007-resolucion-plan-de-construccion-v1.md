# EXP-0002 · Actuación 007 — Resolución: plan de construcción de V1

| Campo | Valor |
|---|---|
| Tipo | resolucion |
| Fecha | 2026-11-02 |
| Autor | coordinador de la sesión de implementación, sobre el informe de la mesa (actuación 006) |
| Corrige | — |

**Expediente:** [EXP-0002](../README.md) · **Foliada:** 007 — no se reescribe

---

## Índice

- [1. Resolución](#1-resolución)
- [2. Plan por tramos](#2-plan-por-tramos)
- [3. Lo que queda excluido](#3-lo-que-queda-excluido)
- [4. Reintegración](#4-reintegración)

## 1. Resolución

Se construye **V1 (pistas)** con la capacidad **apagada por defecto**, por la delegación de la actuación 005
y con las decisiones de la actuación 006. Sobre el lote de la actuación 004:

| Punto | Resolución |
|---|---|
| A | Se construye como `Could`, flag apagado. El intake **no** se edita; el ingreso como F-n queda pendiente del Product Owner |
| C | El nivel 3 se muestra sólo a pedido, tras un botón explícito |
| E | No hay servidor de PROD: la dirección llega por configuración y sin valor por omisión |
| F | Plantillas provisionales redactadas por el agente, rotuladas «provisional, pendiente del docente» |
| G | La tabla de la Norma de Nomenclatura y las ADR-14006 a ADR-14010 entran antes del código |

B (revisión humana del checklist) y D (contenido de V2) siguen abiertos y no traban V1.

## 2. Plan por tramos

| Tramo | Qué | Aceptación |
|---|---|---|
| T1 | Norma §6.27 (19 filas de `ai-layer/04` más las que la construcción necesite) y ADR-14006 a ADR-14010 | Commit anterior a todo cambio en `src/**/AI/**`; cada identificador nuevo con fila |
| T2 | Puertos y caso de uso en Application | Pruebas con doble del puerto; `IFigureValidator` sigue sincrónico; build sin advertencias |
| T3 | `Infrastructure/AI`: filtro, plantillas, adaptador, cupo y opciones | Filtro con paridad 8/8 con `pistas.py`; caídas a plantilla; topes; telemetría |
| T4 | Composición, flag y compuertas de arquitectura | Arranque degradado sin dirección; `DomainReferencesNoAiSdk`; SDK sólo en `AI` y composición; `deploy/compose.yaml` sin cambios |
| T5 | Ruta `POST /v1/interpretaciones/pistas` y contrato | A-18 intacto; sin configuración responde 200 con plantilla; nada se persiste |
| T6 | Web detrás de `AI:HintsEnabled` | Con la configuración por omisión no aparece el botón; nivel 3 a pedido; texto codificado |
| T7 | Documentación por incremento | Fila de control de cambios que cita «expediente 0002» en cada categoría de Reintegra |
| T8 | Verificación, evidencia y cierre de la etapa | Batería completa, cobertura, R-PRIV, corrida rotulada con el modelo local |

## 3. Lo que queda excluido

**V2 (borrador de comentario)** queda especificada y **diferida**. Se reabre cuando se cumplan las tres
condiciones: piloto de V1 cerrado con la puntuación docente, banco de conducta de V2 construido y umbral de
REQ-IA-26 fijado por el docente. ADR-IA-06 queda propuesta y sin número.

## 4. Reintegración

| Campo | Valor |
|---|---|
| Reintegra | 05-Arquitectura-Tecnica, 06-Backlog-Tecnico, 08-Calidad-Y-Pruebas, 09-Devops, Adrs, Norma-De-Nomenclatura, 03-UX-UI-DX |

Rutas del destino por categoría (`SPEC` de A13):
`05-Arquitectura-Tecnica=SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/05-Arquitectura-Tecnica`
`06-Backlog-Tecnico=SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/06-Backlog-Tecnico`
`08-Calidad-Y-Pruebas=SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/08-Calidad-Y-Pruebas`
`09-Devops=SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/09-Devops`
`Adrs=SDD/Docs/Producto/Adrs`
`Norma-De-Nomenclatura=SDD/Docs/Producto/Norma-De-Nomenclatura.md`
`03-UX-UI-DX=SDD/Docs/Unidades-Entrega/GeometriaFactory-Web/03-UX-UI-DX`

Sigue: ejecución de los tramos T1 a T8, a cargo del implementador; cada tramo queda en una constancia.
