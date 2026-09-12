# BT-00030 — Declarar CORS, condicional al cliente que aparezca (`D-01`)

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** BT-00030-Declarar-Cors-Condicional-Al-Cliente-Que-Aparezca-D-01.md
**Versión:** 1.1
**Estado:** Ready
**Fecha:** 2026-09-12
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-T06 · Exposición pública y versionado del contrato
**Etapa del producto:** `k`
**Tipo:** indagación
**Prioridad:** Media
**Estimación:** **No aplica** — el producto no estima; ver §6

## 1. Descripción

Declarar CORS, condicional al cliente que aparezca (`D-01`).

## 2. Justificación

`Mesa-2026-09-12-ciclo-2.md` §4 ítem 5; `05` §9.1 (`GeometriaFactory-Api`), riesgo **«Que se agregue un punto de acceso pensado para el navegador, o se configure el intercambio de origen cruzado»**, mitigado hoy por «las tres ausencias declaradas de la superficie de 02» y por que «el único cliente legítimo esté declarado en el manifiesto y en el grafo»; condicionado además al ítem diferido **D-01** (`PRODUCT-INTAKE` §17.1.P.3, `Mesa-2026-09-12.md` §8), que decide quién es el cliente

## 3. Criterios de aceptación

- Mientras `D-01` siga abierto, el documento de arquitectura declara **explícitamente por qué CORS no aplica hoy** (no hay cliente de navegador de otro origen conocido)
- si `D-01` cierra con un cliente JavaScript de otro origen, se agrega una política por origen explícito (OWASP CORS Cheat Sheet) antes de habilitar cualquier origen. **Caja temporal: hasta que `D-01` cierre**

## 4. Dependencias

- BT-00028

## 5. Tipo

`indagación`. si `D-01` cierra con un cliente JavaScript de otro origen, se agrega una política por origen explícito (OWASP CORS Cheat Sheet) antes de habilitar cualquier origen. **Caja temporal: hasta que `D-01` cierre**.

## 6. Estimación

**No aplica.** El producto no estima: planifica por etapas con punto de control bloqueante. `PA-01` de [`../Product-Backlog.md`](../Product-Backlog.md) §6 quedó **cerrado por lectura** el 2026-08-25, y el fundamento está en su §4.1 (ver también [`../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md`](../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) §5.b).

## 7. Trazabilidad a US

| Aspecto | Contenido |
| --- | --- |
| US que la consumen | **Infraestructura compartida**: depende del ítem diferido `D-01` y no lo duplica |
| CU upstream | — (sin CU: condicionado a `D-01`) |
| Puntos de acceso que toca | Ninguno |
| Fuente de arquitectura | `05` §9.1 (`GeometriaFactory-Api`), riesgo de CORS/navegador; condicionado al ítem diferido `D-01` |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | **Extracción a archivo individual** de la fila BT-00030 de [`../Backlog-Tecnico.md`](../Backlog-Tecnico.md) v3.0 §3.1, por cruce del umbral de treinta `BT` que fija `Rules-Backlog-Tecnico.md` §3.3 para el bloque `00xxx` (`GeometriaFactory-Api`, 35 tareas con la apertura de la fase `k`). **Contenido transpuesto sin reescritura** desde la fila del catálogo (§3.1), la épica EP-T06 de §2.1 y la fila correspondiente de la matriz de trazabilidad (§4.1). Autocorrección de la misma corrida (`Master-Prompt.md` §8.1), `ORIGEN DEL HECHO: de la corrida`, base `9167e68`. Evidencia en [`../../../../Audit/Apertura-Fase-k-2026-09-12.md`](../../../../Audit/Apertura-Fase-k-2026-09-12.md). |
| 1.1 | 2026-09-12 | **Evaluación de DoR** (`Definition-Of-Ready.md` §2.1) y **corrección de fuente**. §2 y §7 citaban sólo el ítem diferido `D-01`, que no es punto abierto de `05` §11 y no es fuente admitida por el criterio 1 en la letra. Existe una fuente admitida que sostiene la misma tarea: `05` §9.1 ya trae el riesgo «que se agregue un punto de acceso pensado para el navegador, o se configure el intercambio de origen cruzado», con probabilidad baja e impacto muy alto, mitigado hoy por las tres ausencias declaradas de la superficie de 02. Se agrega esa cita en §2 y en §7, conservando `D-01` como condición adicional (quién es el cliente, no si CORS aplica hoy). **Ningún criterio de aceptación cambia**: la tarea sigue siendo de tipo `indagación` con caja temporal hasta que `D-01` cierre, que es la excepción de DoR §3.1 para este tipo de tarea. Pasa a **Ready**. Evidencia en [`../../../../Audit/DoR-Tramo-k-2026-09-12.md`](../../../../Audit/DoR-Tramo-k-2026-09-12.md). |
