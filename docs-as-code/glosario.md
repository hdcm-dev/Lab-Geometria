---
doc_id: FIC-GEO-004
doc_type: anexo
title: Glosario de la ayuda de Lab-Geometria
description: Un término por concepto para la ayuda de uso, el catálogo del punto de uso y las pistas de Lab-Geometria.
status: borrador
origin: ia-generated
confidence: media
owner: mantenimiento de docs-as-code (agente, expediente 0003)
last_review: 2026-11-02
audience: [desarrollo, agente, docente]
delivery: [publico]
traces: [FIC-GEO-001]
---

# Glosario de la ayuda

Un término por concepto. Si la interfaz usa dos nombres para lo mismo, se anota en la última columna y se propone
unificar; la propuesta no toca `src/` (ola 2, con ok de la sesión de la capa de IA).

| Término | Qué es | Qué no es | Dónde aparece |
| --- | --- | --- | --- |
| Valor declarado | El número que el alumno escribe en su trabajo (por ejemplo, `Area` de un cuadrado) | No es el resultado correcto: puede estar mal, y el producto no lo corrige | `DeclaredValue` (`src/GeometriaFactory.Domain/Entities/Observation.cs:88`); los términos del contrato (`Tipo`, `Largo`, `Ancho`, `Radio`, `Altura`, `Area`) no se traducen (`WorkSubmission.razor:421-422`) |
| Valor derivado | El número que el producto calcula a partir de las dimensiones declaradas | No reemplaza al declarado: se muestran los dos | `DerivedValue` (`Observation.cs:89`) |
| Observación | Lo que el producto anota sobre un trabajo. Es de una de dos especies: advertencia o error de validación | No es un rechazo del docente | `src/GeometriaFactory.Domain/Values/ObservationKind.cs:4`, `:9-16` (conjunto cerrado de dos valores) |
| Advertencia | Observación de **discrepancia** entre el valor declarado y el derivado, con los dos valores. El producto la **señala** sin corregir ni rechazar | No es un error de validación | `ObservationKind.cs:11-12`; `Observation.cs:76`; `SDD/Docs/00-Contexto/Vision-Producto.md:88` (decisión D-2 del producto) |
| Error de validación | Observación que dice que el texto no cumple la forma o las reglas del contrato | No es una discrepancia de cuentas | `ObservationKind.cs:14-15` |
| Pista | Texto que la capa de IA genera, a pedido, sobre una advertencia | No es ayuda estática: la ayuda de uso no la duplica ni la anticipa | `WorkSubmission.razor:141-146` («Pistas para tu advertencia»; expediente 0002) |
| Ayuda de uso | Texto fijo que explica **el campo y el formato** | No explica por qué un valor está mal: eso es de la pista | Formularios de la Web; catálogo del punto de uso (PR 3) |

**Nombres dobles detectados:** ninguno confirmado en este PR. El relevamiento por pantalla va con el catálogo del punto
de uso (PR 3).
