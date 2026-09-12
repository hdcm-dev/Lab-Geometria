# BT-00035 — Política de deprecación en `Estrategia-Versionado.md`

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** BT-00035-Politica-De-Deprecacion-En-Estrategia-Versionado.md
**Versión:** 1.1
**Estado:** Ready
**Fecha:** 2026-09-12
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-T06 · Exposición pública y versionado del contrato
**Etapa del producto:** `k`
**Tipo:** docs
**Prioridad:** Media
**Estimación:** **No aplica** — el producto no estima; ver §6

## 1. Descripción

Política de deprecación en `Estrategia-Versionado.md`.

## 2. Justificación

`Mesa-2026-09-12-ciclo-2.md` §4 ítem 10; [`ADR-00008`](../../05-Arquitectura-Tecnica/Adrs/ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) §1 punto 1 vigente («no hay deprecación gradual: no hay a quién dársela»), premisa que BT-00027 reescribe y que esta tarea, dependiente de BT-00032, ejecuta sobre `Estrategia-Versionado.md`; `PRODUCT-INTAKE` **4.3**

## 3. Criterios de aceptación

- [`Estrategia-Versionado.md`](../../09-Devops/Estrategia-Versionado.md) suma una entrada con el plazo de convivencia de **un cuatrimestre** (`D-02`) entre versiones de ruta y el mecanismo de aviso de deprecación de la versión anterior

## 4. Dependencias

- BT-00032

## 5. Tipo

`docs`.

## 6. Estimación

**No aplica.** El producto no estima: planifica por etapas con punto de control bloqueante. `PA-01` de [`../Product-Backlog.md`](../Product-Backlog.md) §6 quedó **cerrado por lectura** el 2026-08-25, y el fundamento está en su §4.1 (ver también [`../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md`](../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) §5.b).

## 7. Trazabilidad a US

| Aspecto | Contenido |
| --- | --- |
| US que la consumen | **Infraestructura compartida**: cierra el ítem 10 del plan de la fila `k` |
| CU upstream | — (sin CU: política de deprecación) |
| Puntos de acceso que toca | Ninguno |
| Fuente de arquitectura | [`ADR-00008`](../../05-Arquitectura-Tecnica/Adrs/ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) §1 punto 1 (a reescribir por BT-00027) |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | **Extracción a archivo individual** de la fila BT-00035 de [`../Backlog-Tecnico.md`](../Backlog-Tecnico.md) v3.0 §3.1, por cruce del umbral de treinta `BT` que fija `Rules-Backlog-Tecnico.md` §3.3 para el bloque `00xxx` (`GeometriaFactory-Api`, 35 tareas con la apertura de la fase `k`). **Contenido transpuesto sin reescritura** desde la fila del catálogo (§3.1), la épica EP-T06 de §2.1 y la fila correspondiente de la matriz de trazabilidad (§4.1). Autocorrección de la misma corrida (`Master-Prompt.md` §8.1), `ORIGEN DEL HECHO: de la corrida`, base `9167e68`. Evidencia en [`../../../../Audit/Apertura-Fase-k-2026-09-12.md`](../../../../Audit/Apertura-Fase-k-2026-09-12.md). |
| 1.1 | 2026-09-12 | **Evaluación de DoR** (`Definition-Of-Ready.md` §2.1) y **corrección de fuente**. §2 y §7 citaban el registro de mesa y «`PRODUCT-INTAKE` 4.3» sin sección, ninguno admitido. `ADR-00008` §1 punto 1, vigente, declara hoy «no hay deprecación gradual: no hay a quién dársela» — es la premisa exacta que BT-00027 reescribe y que esta política ejecuta sobre `Estrategia-Versionado.md`. Se agrega esa cita. **Ningún criterio de aceptación cambia**. Pasa a **Ready**. Evidencia en [`../../../../Audit/DoR-Tramo-k-2026-09-12.md`](../../../../Audit/DoR-Tramo-k-2026-09-12.md). |
