# SDD — Documentación de especificación de Fábrica de Geometría

Esta carpeta contiene los artefactos del **Framework SDD** para este repositorio destino. El framework vive en un repositorio aparte (`IA/IA.SDD`) y no se copia acá: las reglas, plantillas y master-prompts se leen desde allá y sólo los artefactos del producto se escriben acá.

## Estructura

| Ruta | Qué contiene | Quién escribe |
|---|---|---|
| `Intake/` | `PRODUCT-INTAKE-Fabrica-De-Geometria.md`, el documento de entrada único del producto, y —cuando el orquestador lo derive y el humano lo confirme— `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` | El intake, el Product Owner (asistido por agente). El manifiesto, el orquestador |
| `Docs/` | Documentación generada: categorías 00 a 11, la vista de producto, los informes de auditoría y el README raíz de la salida | El orquestador SDD |
| `Maquetas/` | Maquetas de validación visual, sólo si algún proyecto de código ejecuta la Fase B2 | El orquestador y el humano, a mano |

`Docs/` está **vacía**: la documentación de especificación todavía no se generó. Es el estado que el orquestador espera para arrancar sin ejecutar reconciliación normativa.

## Estado actual

| Paso | Estado |
|---|---|
| Documento de intake | **Emitido**, versión 1.1, en estado `Borrador`. Ver §22 del intake: cuatro asunciones esperan confirmación del Product Owner |
| Manifiesto derivado | Pendiente. Lo deriva el orquestador de §13 del intake y lo presenta para confirmación |
| Generación de `Docs/` | Pendiente |

## Cómo se continúa

Con el intake confirmado, se invoca el prompt de entrada del framework:

```text
Leer y Ejecutar /IA/IA.SDD/PROMPTS/PROMPT-Agente-Bootstrap-SDD.md en el repositorio: /PROG2/Geometria/Lab-Geometria
```

El orquestador valida la completitud del intake, deriva el `PRODUCT-MANIFEST` de su §13, lo presenta para confirmación y recién entonces genera la documentación por fases, con auditoría entre fases y confirmación humana en cada corte.

## Fuentes del intake

Las tres fuentes que este intake integra viven en el repositorio de documentación `Lab-Geometria.Documentacion`: los Requerimientos Funcionales y los Requerimientos Técnicos de `PROMPTs/03-Ejecutar-Prompt-Integrador-Documento-Intake/INPUTs/`, y el Análisis Final Integrado de `Analisis/`.
