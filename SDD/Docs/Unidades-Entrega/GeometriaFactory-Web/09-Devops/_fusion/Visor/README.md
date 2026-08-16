# 09 · DevOps — GeometriaFactory-Visor

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Visor
**Documento:** README.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero DevOps Senior + Release Engineer (AG-09)
**Tipo de proyecto de código (D8):** `library`

---

## Tabla de contenido

- [1. Artefactos de esta sección](#1-artefactos-de-esta-sección)
- [2. Orden de lectura](#2-orden-de-lectura)
- [3. Artefactos omitidos y su motivo](#3-artefactos-omitidos-y-su-motivo)
- [4. Los nueve quality gates, y dónde corre cada uno](#4-los-nueve-quality-gates-y-dónde-corre-cada-uno)
- [5. Puntos abiertos: uno cerrado, tres vivos](#5-puntos-abiertos-uno-cerrado-tres-vivos)
- [6. Recuentos que esta sección sostiene](#6-recuentos-que-esta-sección-sostiene)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Artefactos de esta sección

| Documento | Versión | Estado | Propósito |
| --- | --- | --- | --- |
| [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) | 1.1 | Propuesto | Stages, los **nueve** gates materializados, el momento de medición de las dos puertas técnicas y las **dos** plataformas |
| [`Estrategia-Versionado.md`](Estrategia-Versionado.md) | 1.0 | Propuesto | Versionado del **punto de extensión**, criterio de cambio de `ADR-12006` y política de crecimiento de la fachada |
| [`Entornos-Deploy.md`](Entornos-Deploy.md) | 1.0 | Propuesto | Ausencia de ambientes y canales, y **la resolución del punto abierto `PA-05`** |
| [`Guia-Publicacion-Bundle-Visor.md`](../../Guia-Publicacion-Bundle-Visor.md) | 1.0 | Propuesto | La **entrega interna** del bundle al anfitrión: pre-requisitos, comandos, verificación, reversión y métricas |
| [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) | 1.0 | Propuesto | Inventario del bundle, firma, nivel de integridad, análisis de composición y dinámico, y política ante vulnerabilidades |

**Cinco artefactos: es el único de los tres proyectos de código de nivel topológico 0 que emite guía de publicación**, y el motivo está en [`Guia-Publicacion-Bundle-Visor.md`](../../Guia-Publicacion-Bundle-Visor.md) §0: su artefacto **es un archivo que se traslada**, con un modo de falla propio de ese traslado, mientras que los otros dos se referencian dentro de la misma construcción.

## 2. Orden de lectura

1. [`Estrategia-Versionado.md`](Estrategia-Versionado.md) — qué gobierna la versión del punto de extensión, y por qué **ninguna** clase de cambio mayor la detecta una compilación.
2. [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) — qué corre, qué se mide sobre el bundle generado y cuándo se miden las dos puertas.
3. [`Entornos-Deploy.md`](Entornos-Deploy.md) — dónde no vive el artefacto, y por qué.
4. [`Guia-Publicacion-Bundle-Visor.md`](../../Guia-Publicacion-Bundle-Visor.md) — cómo se entrega y cómo se verifica que lo entregado es lo que se generó.
5. [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) — la única cadena de suministro con sujeto real del nivel topológico 0.

## 3. Artefactos omitidos y su motivo

| Artefacto | Estado | Motivo |
| --- | --- | --- |
| `Pipeline-Producto.md` | **No es de esta sección** | Artefacto de nivel producto (`Rules-Devops.md` §4.9), emitido una sola vez bajo `Producto/` al cierre del bucle de proyectos de código |

**Ningún otro.** Esta categoría emite los cuatro artefactos obligatorios de nivel proyecto de código, el README recomendado y la guía de publicación, que acá **sí corresponde**.

## 4. Los nueve quality gates, y dónde corre cada uno

**El texto vinculante sobre el carácter de cada gate es el de [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../../../08-Calidad-Y-Pruebas/_fusion/Visor/Estrategia-Calidad.md) §3.** Esta categoría **no cambió el carácter de ninguno**.

| Gate | Dónde corre | Carácter |
| --- | --- | --- |
| QG-01 | Stage `empaquetar` | Bloqueante |
| QG-02 (**`PT-03`**) | Momento de medición, `TC-12019` | **Bloqueante, detiene la planificación de `g`** |
| QG-03 (**`PT-02`**) | Momento de medición, `TC-12020` | **Bloqueante, detiene la planificación de `g`** |
| QG-04 | Stage `inspeccionar`, sobre el **bundle generado**, con los dos movimientos prendidos | Bloqueante, sin gradación |
| QG-05 | Stage `inspeccionar` | Bloqueante, sin gradación |
| QG-06 | Stage `inspeccionar`, sobre el bundle generado | Bloqueante |
| QG-07 | Stage `probar` | Bloqueante, sin gradación |
| QG-08 | Stage `probar`, y revisión | Se rechaza en revisión |
| QG-09 | Revisión del pull request | Se rechaza en revisión |

**Ninguno es condicionado**, y no lo decide esta categoría: [`../08-Calidad-Y-Pruebas/README.md`](../../../08-Calidad-Y-Pruebas/_fusion/Visor/README.md) §4 lo declara y da el motivo —los umbrales salen del contrato de la fachada y de las dos puertas técnicas, no de valores rotulados **[ASUNCIÓN]**—. La única marca [ASUNCIÓN] que alcanza a este proyecto de código está en el intake §17.7.P.6 y es **sobre la forma del gate y no sobre la regla**, de modo que no condiciona: `QG-04` bloquea.

**No hay gate de cobertura de líneas, ni de mutation score, ni umbral numérico de fluidez.** Las tres ausencias están declaradas aguas arriba con su motivo, y **esta categoría no inventa ninguna de las tres**.

## 5. Puntos abiertos: uno cerrado, tres vivos

| Id | Punto abierto de `05` §11 | Estado tras esta emisión |
| --- | --- | --- |
| PA-05 | Si el bundle generado se versiona en el repositorio o se ignora. `05` §11 declara que **lo cierra la categoría 09** | **Cerrado**: se ignora y lo genera la canalización. Fundamento en [`Entornos-Deploy.md`](Entornos-Deploy.md) §2, con **cuatro** apoyos verificables y **una** exigencia operativa pendiente sobre el archivo de exclusiones del repositorio |
| PA-01 | La versión del motor de dibujo tridimensional que se adopta | **Abierto.** Se cierra por `BT-12009` antes del momento de medición. Recogido como `PD-03` de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §10 |
| PA-03 | El umbral numérico de fluidez de la interacción | **Abierto, y esta categoría no lo cierra**: inventar un número acá lo propagaría como si fuera del producto |
| PA-04 | La versión mínima de navegador | **Abierto.** El requisito se declara **por capacidad** y no por versión, y así se materializa en [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §4 |

**`PA-02` —los nombres definitivos de las funciones internas— no es de esta categoría** y sigue como lo dejó `05` §11: lo cierra el equipo en la etapa que implementa la fachada.

**Puntos abiertos que esta categoría abre:** **tres**, `PD-01` a `PD-03` de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §10, más la exigencia operativa pendiente de [`Entornos-Deploy.md`](Entornos-Deploy.md) §2.2, que es una acción de la etapa `a` y no una decisión sin tomar.

## 6. Recuentos que esta sección sostiene

| Magnitud | Valor | Fuente |
| --- | --- | --- |
| Stages de la canalización | **5**: `instalar`, `empaquetar`, `inspeccionar`, `probar`, `copiar` | Intake §17.7.P.8; `05` §5 |
| Quality gates materializados | **9**, **ninguno** condicionado | `08` `Estrategia-Calidad.md` §3 |
| Puertas técnicas que se miden acá | **2**: `PT-02` y `PT-03`, en **6** tramos | Intake §15 y §17.7.P.8; `08` `Criterios-Validacion.md` §4 |
| Funciones de la fachada | **6** | Intake §17.7.P.3; `02` `Definicion-Contrato-De-Fachada.md` §4 |
| Garantías del contrato | **7** | `02` `Definicion-Contrato-De-Fachada.md` §3.2 |
| Códigos de condición | **7**, en **8** cursos | `02` `Definicion-Contrato-De-Fachada.md` §6 |
| Ambientes de despliegue propios | **0** | `05` §5 |
| Canales de publicación | **0** | Intake §17.7.P.7 |
| Artefactos entregados | **1**: el bundle, copiado al anfitrión | Intake §13; `05` §5 |
| Secretos propios | **0**, en construcción y en ejecución | [`Entornos-Deploy.md`](Entornos-Deploy.md) §5 |
| Momentos del producto que este proyecto de código toca | **3**: la etapa `a`, el momento de medición y la etapa `g` | `08` `Plan-Pruebas.md` §1 |
| Puntos abiertos de `05` §11 | **5**, de los cuales esta categoría **cierra 1** | `05` §11 |

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial del índice de la categoría 09 de `GeometriaFactory-Visor`. Lista los **cinco** artefactos emitidos —es el único de los tres proyectos de código de nivel topológico 0 con guía de publicación, y el motivo está declarado—, el orden de lectura, el resumen de los **nueve** quality gates con la constancia de que **ninguno es condicionado** y de que la única marca [ASUNCIÓN] que lo alcanza es sobre la forma del gate, y el estado de los **cinco** puntos abiertos de `05` §11 tras esta emisión: **`PA-05` cerrado**, `PA-01`, `PA-03` y `PA-04` vivos, y `PA-02` fuera del alcance de esta categoría. Cierra con la tabla de recuentos y la fuente de cada uno. |
| 1.1 | 2026-08-11 | **Propagación de la primera decisión de despliegue del Product Owner** del intake **1.22** §17.6.P.7: el filtro de rutas del flujo que publica el front incluye ahora `src/GeometriaFactory.Contracts/`. La consecuencia cae entera dentro de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md), que sube a **1.1** en §1: este proyecto de código deja de ser el único de nivel topológico 0 cuyo cambio dispara una publicación. **Las dos afirmaciones de este índice sobre lo que este proyecto de código tiene de único entre los tres de nivel topológico 0 no se tocan**: las dos son sobre la guía de publicación, y ninguna se apoyaba en el filtro de rutas. |
