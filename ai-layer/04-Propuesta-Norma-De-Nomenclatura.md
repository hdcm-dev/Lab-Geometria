# Capa de IA · Propuesta de filas para la Norma de Nomenclatura §6

| Campo | Valor |
|---|---|
| Documento | `ai-layer/04-Propuesta-Norma-De-Nomenclatura.md` |
| Versión | 0.1 |
| Estado | Propuesta. **No** edita `SDD/Docs/Producto/Norma-De-Nomenclatura.md`: las filas entran ahí, como tabla nueva del rango de §6.1, en el mismo cambio que apruebe la capacidad (ADR-IA-05) |
| Fecha | 2026-11-01 |

## Índice

- [1. Por qué antes del código](#1-por-qué-antes-del-código)
- [2. Filas propuestas](#2-filas-propuestas)
- [3. Rutas del contrato](#3-rutas-del-contrato)

## 1. Por qué antes del código

La norma del repositorio manda: «si un concepto del dominio no está en el glosario […] se agrega primero a la
tabla que le corresponde y recién después se escribe el identificador» (§6.1), y agregar una fila es un acto
declarado con su entrada en el control de cambios (corolario 4). Esta especificación nombra tipos que todavía
no existen; por eso los propone aquí, con la forma de fila de §6.1: castellano, inglés, clase y dónde está
declarado el concepto.

## 2. Filas propuestas

| Castellano | Inglés | Clase | Dónde está declarado el concepto |
| --- | --- | --- | --- |
| redactor de pistas | `IObservationHintWriter` | Puerto, propuesto | `ai-layer/01-Especificacion-V1-Pistas.md` §3.3 |
| pedido de pistas | `ObservationHintRequest` | Tipo | ídem §3.3 |
| resultado de pistas | `HintResult` | Tipo | ídem §3.3 |
| pistas | `Hints` | Tipo | ídem §3.3 |
| nivel 1, 2, 3 de la pista | `Level1`, `Level2`, `Level3` | Miembros | ídem §3.3 |
| origen de la pista | `HintOrigin` | Tipo, conjunto cerrado | ídem §3.3 |
| modelo / plantilla (origen) | `Model` / `Template` | Valores de conjunto cerrado | ídem §3.3 |
| sentido de la diferencia | `DiscrepancyDirection` | Tipo, conjunto cerrado | ídem §3.3 |
| mayor / menor (sentido) | `Above` / `Below` | Valores de conjunto cerrado | ídem §3.3 |
| explicar observaciones | `ExplainObservationsUseCase` | Caso de uso | ídem §5.1 |
| redactor de pistas con modelo | `ModelObservationHintWriter` | Adaptador | ídem §5.1 |
| filtro de pistas | `HintFilter` | Tipo | ídem §4.2 |
| pistas de plantilla | `TemplateHints` | Tipo | ídem §5.2 |
| redactor de borradores de comentario | `ICommentDraftWriter` | Puerto, propuesto | `ai-layer/02-Especificacion-V2-Borrador-De-Comentario.md` §3 |
| pedido de borrador | `CommentDraftRequest` | Tipo | ídem §3 |
| resultado del borrador | `DraftResult` | Tipo | ídem §3 |
| redactar borrador de comentario del trabajo | `DraftWorkCommentUseCase` | Caso de uso | ídem §3 |
| redactor de borradores con modelo | `ModelCommentDraftWriter` | Adaptador | ídem §3 |
| capa de IA (subsegmento de espacio de nombres) | `AI` | Superficie derivada | `ai-layer/03-ADR.md` ADR-IA-03 |

Los códigos de condición que hagan falta (por ejemplo, para un pedido de pistas sin texto) se toman del
catálogo vigente antes de crear uno nuevo; la etapa `c` y la `e` mostraron que suelen alcanzar (norma §6.2).

## 3. Rutas del contrato

Las rutas siguen el prefijo único `/v1/` (`src/GeometriaFactory.Api/Endpoints/ContractRoutePrefix.cs`) y el
castellano de las rutas vigentes (`/interpretaciones`, `/trabajos`):

| Ruta | Capacidad |
|---|---|
| `POST /v1/interpretaciones/pistas` | V1 |
| `POST /v1/trabajos/{id}/borrador-de-comentario` | V2 |
