# EXP-0002 · Actuación 005 — Providencia: encargo de construcción de V1

| Campo | Valor |
|---|---|
| Tipo | providencia |
| Fecha | 2026-11-02 |
| Autor | coordinador de la sesión de implementación (agente), por delegación del Product Owner |
| Corrige | — |

**Expediente:** [EXP-0002](../README.md) · **Foliada:** 005 — no se reescribe

---

## Índice

- [1. Origen del hecho](#1-origen-del-hecho)
- [2. Qué se provee](#2-qué-se-provee)
- [3. Relación con el lote de la actuación 004](#3-relación-con-el-lote-de-la-actuación-004)

## 1. Origen del hecho

El Product Owner pidió, en la sesión del 2026-11-02, armar una mesa de especialistas y **implementar** la
especificación de `ai-layer/` con la norma del framework IA.SDD. El pedido se asienta como testimonio; el
pasaje que habla de otro repositorio se redacta por S2 (no es parte de este caso y no se nombra en un
repositorio público).

```testimonio
y porque no armas una mesa de especialistas , en base a la mesa que ya contas para seguir con el proyecto en `/Geometría (#236): quedó el expediente 0002 y la carpeta ai-layer/, que contiene solo la especificación: requisitos, ADR, arquitectura y la matriz de riesgos. La aplicación no tiene ninguna llamada a un modelo.`

tenes acceso al sdd (`/IA/SDD/IA.SDD/README.md`) que la creeo y podes hacer las modificaciones para implementar esa especificacion el proyecto

 [redactado S2: pedido paralelo sobre otro repositorio]

revisa primero, que no haya otros agentes trabajando esos repos, si los hay coordinen
```

| Campo | Valor |
|---|---|
| Canal | sesión de trabajo con el agente, mensaje del Product Owner (no respondía a una pregunta) |
| Fecha-hora | 2026-11-02, hora de Argentina (UTC-3) |
| Huella | `df6ad999d4707fae0de466dc32fc542dc2a82ed93f27ba53baf21f75310f2d77` |
| Huella del original | `02acab0ca0f0d0f15b262fc49c0dd07d3dc19704a40462ce8cfec59e538af064` |

**Clasificación (§3.3 de la regla de expedientes):** es un **pedido**, fuente de intención. No es una
aprobación del checklist ni una respuesta punto por punto al lote de la actuación 004. Funda la
**delegación**: «si falta algo, hacelo; si hay que decidir, mesa y aplicá».

## 2. Qué se provee

1. Se abre la etapa de **construcción** del expediente, sobre `main` `c5afa0e` (fusión del PR #236), en la
   rama `ai-layer/implementacion-v1`, en un árbol de trabajo propio y sin tocar el clon compartido.
2. Se convoca una **mesa de construcción** de cinco lentes —arquitectura, seguridad, pruebas, dominio
   didáctico y norma— que vota el plan. Su informe es la actuación 006.
3. Alcance: **V1 completa** (pistas). **V2** sólo si la especificación la da por lista y la mesa la vota.
4. Restricción dura del encargo: fusionar a `main` despliega el front y la imagen de producción, de modo que
   la capacidad queda **apagada por defecto**; sin dirección del servidor de modelos el producto se comporta
   como hoy.
5. No se abre pull request ni se fusiona desde esta etapa: lo hace el coordinador.

## 3. Relación con el lote de la actuación 004

El lote de la actuación 004 sigue abierto y **no se da por respondido**. Los puntos A, F y G que traban la
construcción los resuelve la mesa por delegación (actuación 007), sin editar el intake: el ingreso como
F-n sigue siendo del Product Owner.

Sigue: informe de la mesa de construcción (actuación 006), a cargo de la mesa convocada.
