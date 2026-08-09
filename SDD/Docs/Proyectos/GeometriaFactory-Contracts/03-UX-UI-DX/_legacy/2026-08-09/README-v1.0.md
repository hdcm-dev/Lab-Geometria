> **Artefacto archivado — estado `Superado`**
>
> Esta es una **copia archivada** del documento `README.md` en su versión **1.0**, tomada el 2026-08-09 por el orquestador SDD antes de que la versión vigente la superara (`Master-Prompt.md` §5 y §5.1).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-09
> - **Versión vigente:** [`README.md`](../../README.md)
>
> El cuerpo que sigue **no se modifica**: un registro que se corrige después deja de ser un registro. Este archivo no se renombra, no se reenlaza y no vuelve a tocarse.

---

# 03 · UX / UI / DX — GeometriaFactory-Contracts

**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** README.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-08
**Autor:** DX Lead (AG-03)
**Variante:** DX
**Trazabilidad upstream:** `02-Especificacion-Funcional/` completo —`Especificacion-Funcional.md`, los seis contratos de uso `CU-01` a `CU-06` y `Glosario-Funcional.md`—; `00-Contexto/Vision-Producto.md` §9; `00-Contexto/Alcance-Producto.md` §2.2 y §8; `01-Necesidades-Negocio/Necesidades-Negocio.md` §2; `PRODUCT-INTAKE` §17.4 y §16.1; `PRODUCT-MANIFEST` §5 (flags del proyecto de código)
**Trazabilidad downstream:** `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas`, `10-Examples` y `11-Documentacion` de este proyecto de código

---

## Tabla de contenido

- [1. Qué hay en esta carpeta](#1-qué-hay-en-esta-carpeta)
- [2. Variante aplicada y por qué](#2-variante-aplicada-y-por-qué)
- [3. Orden de lectura sugerido](#3-orden-de-lectura-sugerido)
- [4. Artefactos omitidos y su motivo](#4-artefactos-omitidos-y-su-motivo)
- [5. Criterios de aceptación no aplicables a la variante DX](#5-criterios-de-aceptación-no-aplicables-a-la-variante-dx)
- [6. Notas de uso de esta sección](#6-notas-de-uso-de-esta-sección)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Qué hay en esta carpeta

| Documento | Propósito | Estado |
| --- | --- | --- |
| [`DX-Developer-Experience.md`](DX-Developer-Experience.md) | Marco DX: rol de intervención, la regla de exposición, los tres tramos de onboarding, el quick-start dentro del contenedor de desarrollo, el plan de Diátaxis, las dos clases de error, las métricas y el lazo de retroalimentación. **Es el punto de entrada** | Propuesto |
| [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) | El recorrido de la primera hora, paso a paso y con hitos verificables. Es el modo tutorial | Propuesto |
| [`DX-Error-Messages.md`](DX-Error-Messages.md) | Catálogo de errores en sus dos clases, con diagnóstico accionable por entrada. Es el modo how-to para quien llega con un síntoma | Propuesto |
| [`Glosario-UX.md`](Glosario-UX.md) | Vocabulario que esta categoría acuña, la resolución de «error» con sus tres referentes y los términos que se referencian sin redefinir | Propuesto |
| `README.md` | Este archivo: índice navegable, variante aplicada, orden de lectura y declaración de las omisiones | Propuesto |

**Cero wireframes**, que es exactamente el mínimo que `Rules-UX-UI-DX.md` §2.2 fija para `library`.

## 2. Variante aplicada y por qué

**Variante DX**, leída de `Rules-UX-UI-DX.md` §1.2, fila `library`: el producto de este proyecto de código se consume por código, y el foco está en la superficie pública, en la documentación de cada tipo y en los ejemplos ejecutables. El flag `tiene_ui_final` es false, de modo que no hay una combinación con la variante UX/UI.

Lo que hace atípico a este proyecto de código, y que atraviesa los cuatro documentos: **no hay integradores externos**. Los dos únicos consumidores del contrato son `GeometriaFactory-Api` y `GeometriaFactory-Web`, del mismo producto y compilados contra el mismo ensamblado, y `redistribuible` es false. El destinatario real de esta documentación son el mantenedor futuro —la misma persona, meses después— y el agente de construcción por etapas. Eso no reduce el rigor: cambia a quién se le habla, y sobre todo cambia qué hace falta escribir, porque un agente sin memoria entre sesiones **no infiere una prohibición que no esté escrita**.

## 3. Orden de lectura sugerido

1. [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §1.2 — la regla de exposición. Son cinco líneas y es lo primero que hay que haber entendido antes de tocar el proyecto de código.
2. [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) — el recorrido completo, si lo que corresponde es empezar a trabajar hoy.
3. [`DX-Error-Messages.md`](DX-Error-Messages.md) §2.1 — la separación en dos clases de error. Conviene leerla temprano aunque no haya ningún síntoma: es lo que explica por qué la señal más valiosa de este proyecto de código aparece al compilar.
4. [`Glosario-UX.md`](Glosario-UX.md) — se puede leer suelto, y conviene abrirlo primero si el lector viene de otra categoría, por los tres referentes de «error».
5. El resto de `DX-Developer-Experience.md`, para el plan de Diátaxis, las métricas y el lazo de retroalimentación.

## 4. Artefactos omitidos y su motivo

`Rules-UX-UI-DX.md` §2.1 define trece artefactos posibles para esta categoría. Se emiten cinco y se omiten ocho, cada uno con el fundamento que la propia regla declara.

| Artefacto omitido | Regla que lo admite u obliga | Motivo de la omisión |
| --- | --- | --- |
| `Experiencia-De-Uso.md` | §2.1 lo marca obligatorio para tipos con UI final y expresamente **omitible para `library`** | `tiene_ui_final` es false. Este proyecto de código no tiene superficie visible: su actor es el código que compila contra el ensamblado, y las personas del producto aparecen en la categoría 03 de `GeometriaFactory-Web` |
| `wireframes-<superficie>.md` | §2.1 y §2.2: mínimo **0** para `library` | No hay pantallas que esquematizar. Dibujar una sería inventar una superficie que este proyecto de código no tiene |
| `representacion-<concepto>.md` | §2.1 lo condiciona a que exista una representación visual o estructural reutilizada | No hay ninguna. Las representaciones estructurales del producto —el árbol del texto, la escena del visor— pertenecen a `GeometriaFactory-Web` y a `GeometriaFactory-Visor` |
| `DX-Portal-Developers.md` | §2.1 lo marca obligatorio para `rest-api` con portal visible y recomendado para «library con portal hospedado» | `tiene_portal_developers` es false y **no hay integradores externos**. Un portal no tendría a quién servirle: la documentación vive en la propia cadena documental, con su plan de Diátaxis declarado en `DX-Developer-Experience.md` §4 |
| `DX-Operability.md` | §2.1 lo marca obligatorio para `worker-service` | El tipo D8 de este proyecto de código es `library`. No hay proceso que operar: el ensamblado se carga dentro de las dos piezas desplegables y no tiene ciclo de vida propio |
| `Linea-Base-Visual.md` | §2.1 y §1.5, condicionados a `requiere_maqueta` == true | `requiere_maqueta` es false. No hay Fase B2 de validación visual de maqueta para este proyecto de código |
| `Contrato-Datos-Maqueta.md` | Ídem | Ídem |
| `Bitacora-Validacion-Maqueta.md` | Ídem | Ídem |

## 5. Criterios de aceptación no aplicables a la variante DX

`Rules-UX-UI-DX.md` §6 mezcla criterios de las dos variantes. Los que corresponden a la variante UX/UI se declaran **no aplicables con su motivo**, en lugar de darse por cumplidos.

| Criterio de §6 | Estado | Motivo |
| --- | --- | --- |
| Existe `Experiencia-De-Uso.md` con las once secciones del §4.2 | No aplicable | Sólo obligatorio para tipos con UI final; `tiene_ui_final` es false. Ver §4 |
| Existe al menos un `wireframes-<superficie>.md` por superficie clave, con las nueve secciones del §4.2.1 | No aplicable | El mínimo de wireframes para `library` es 0 |
| Cada wireframe enumera al menos los estados vacío, cargando, con datos y error | No aplicable | No hay wireframes. La enumeración equivalente en variante DX son las dos clases de error del catálogo, que sí está cubierta |
| Toda accesibilidad declarada toma WCAG 2.2 nivel AA como piso mínimo | No aplicable | Esta sección **no declara ninguna accesibilidad**, porque no hay superficie que un lector de pantalla o un teclado recorran. El compromiso WCAG 2.2 AA del producto rige en la categoría 03 de `GeometriaFactory-Web`, que es donde hay pantallas |
| En proyectos de código con `requiere_maqueta` == true: nombre canónico de superficie y estados de la maqueta | No aplicable | `requiere_maqueta` es false |
| En proyectos de código con `requiere_maqueta` == true y maqueta aprobada: los tres artefactos de la Fase B2 | No aplicable | `requiere_maqueta` es false |

Los demás criterios de §6 aplican y se cumplen: variante declarada en cada cabecera, `DX-Developer-Experience.md` con las nueve secciones del §4.2.3 incluyendo Diátaxis y tramos verificables de 5, 30 y 60 minutos, quick-start reproducible en cada documento DX, trazabilidad upstream y downstream por artefacto, nomenclatura sin sufijo de versión en el nombre, un solo archivo por nombre lógico, glosario existente y no vacío, no duplicación con `Glosario-Funcional.md`, polisemia declarada sólo donde los contextos colisionan, y tabla de contenido en los cinco documentos.

## 6. Notas de uso de esta sección

- **Autoridad.** Esta sección no origina ninguna capacidad, prioridad ni exclusión. Todo se deriva de los seis contratos de uso de `02-Especificacion-Funcional/`, de las siete restricciones transversales `RT-01` a `RT-07` y del `PRODUCT-INTAKE` §17.4, y traza a su sección de origen.
- **Fronteras.** La arquitectura y los ADR son de `05-Arquitectura-Tecnica`; las historias de usuario, de `06-Backlog-Tecnico`; el pipeline y la automatización de los quality gates, de `09`; los samples y sus contratos de verificación, de `10-Examples`; el cuerpo documental de entrega, de `11-Documentacion`. Esta sección refiere y delega, no redacta.
- **Samples.** `PRODUCT-INTAKE` §16.1 declara que este proyecto de código **no produce samples propios**, porque no lo consumen integradores externos: su verificación vive en `tests/`. Por eso el quick-start no incluye un fragmento que instancie un tipo de transferencia, y lo declara explícitamente en lugar de dejar el hueco sin explicar.
- **Entorno.** Ningún paso de esta documentación asume herramientas en el host de desarrollo. El ciclo entero ocurre dentro del contenedor de desarrollo, y esa restricción es del producto, no una preferencia de esta categoría.
- **Vocabulario.** Los términos del dominio están en `Vision-Producto.md` §9 y los del contrato en `02-Especificacion-Funcional/Glosario-Funcional.md`; acá no se redefinen. «Contrato» tiene tres referentes y «pieza» dos, resueltos aguas arriba; «error» tiene tres referentes y se resuelve en [`Glosario-UX.md`](Glosario-UX.md) §3.1. La palabra «proyecto» a secas no se usa.
- **Nombres de archivo.** Ningún archivo vivo de esta carpeta lleva sufijo de versión: cada uno declara su versión en el campo `Versión` de su cabecera. No hay `_legacy/` porque no hay ninguna versión superada todavía.

## 7. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial del índice de la sección. Enumera los cuatro documentos emitidos más este README, declara la variante DX con su fundamento y el destinatario real de la documentación, fija el orden de lectura, declara las ocho omisiones con la regla que las admite y su motivo, declara los seis criterios de §6 que son de la variante UX/UI como no aplicables, y las notas de autoridad, fronteras, samples, entorno, vocabulario y nombres de archivo. | DX Lead (AG-03) |
