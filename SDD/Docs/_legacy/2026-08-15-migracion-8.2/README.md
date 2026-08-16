# Contenido retirado en la migración normativa de SDD 6.0 a 8.2

**Fecha:** 2026-08-15
**Estado:** Archivado. Conservado íntegro, no descartado

---

## Qué hay acá

El árbol documental de `GeometriaFactory-Contracts`, que en el modelo anterior era un proyecto de
código con las once categorías y en el modelo 8.x **no es una unidad de entrega**: es un proyecto de
código compartido entre `GeometriaFactory-Api` y `GeometriaFactory-Web`, y los proyectos de código no
tienen árbol documental propio.

Nada se descartó. `Migracion-Rules.md` §4.3.2 lo exige: el contenido sin destino se declara en el
informe y no se borra en silencio.

## Qué se llevó al árbol vivo, y a dónde

| Origen | Destino | Motivo |
| --- | --- | --- |
| `02-Especificacion-Funcional/Casos-De-Uso/` (8 documentos) | `Producto/Contratos-Inter-Unidad/` | No son casos de uso de una unidad de entrega: son los contratos de integración con los que las dos cruzan la frontera |
| `05-Arquitectura-Tecnica/Contratos-Abstractions.md` | `Producto/Contratos-Inter-Unidad/` | Es la superficie pública del contrato compartido |
| `05-Arquitectura-Tecnica/Adrs/` (5 documentos) | `Producto/Adrs/` | Una decisión sobre un proyecto compartido alcanza a todas las unidades que lo componen, de modo que es de nivel producto |

## Qué quedó archivado, y por qué

| Categoría | Documentos | Motivo |
| --- | --- | --- |
| `03-UX-UI-DX` | 5 | Un proyecto de código que ningún integrador consume no tiene experiencia de developer propia. Incluye `Guia-Onboarding-Developer.md` y `DX-Error-Messages.md` |
| `06-Backlog-Tecnico` | 26 | El backlog es por unidad de entrega. Estas historias y tareas describen trabajo **ya realizado** sobre el proyecto compartido: se cierran en bloque en lugar de repartirse, porque un backlog es la lista de lo que falta |
| `07-Plan-Sprint` | 2 | Los artefactos del equipo son de nivel producto y los planes son por unidad de entrega |
| `08-Calidad-Y-Pruebas` | 9 | La estrategia de verificación es de la unidad de entrega que consume el contrato |
| `09-Devops` | 5 | Un pipeline publica, y este proyecto no se publica. Su `Entornos-Deploy.md` ya abría con un apartamiento declarando que «no tiene ambientes ni canales propios» |
| `10-Examples` | 4 | `redistribuible: false`: no hay integrador externo. Bajo `Rules-Examples.md` 6.0 la categoría ya no le sería obligatoria |
| `11-Documentacion` | 1 | El cuerpo documental de entrega es de la unidad que se entrega |

## Cómo recuperar algo de acá

Los documentos están íntegros y en el control de versiones. Si una de las dos unidades de entrega
necesita contenido de este árbol —un caso de prueba del contrato, una decisión de su pipeline—, se
lo incorpora a su categoría con la procedencia declarada, y esta carpeta lo registra.
