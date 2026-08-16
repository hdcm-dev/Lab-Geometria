# 09 · DevOps — GeometriaFactory-Domain

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** README.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero DevOps Senior + Release Engineer (AG-09)
**Tipo de proyecto de código (D8):** `library`

---

## Tabla de contenido

- [1. Artefactos de esta sección](#1-artefactos-de-esta-sección)
- [2. Orden de lectura](#2-orden-de-lectura)
- [3. Artefactos omitidos y su motivo](#3-artefactos-omitidos-y-su-motivo)
- [4. Los ocho quality gates, y en qué stage corre cada uno](#4-los-ocho-quality-gates-y-en-qué-stage-corre-cada-uno)
- [5. Recuentos que esta sección sostiene](#5-recuentos-que-esta-sección-sostiene)
- [6. Control de cambios](#6-control-de-cambios)

---

## 1. Artefactos de esta sección

| Documento | Versión | Estado | Propósito |
| --- | --- | --- | --- |
| [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) | 1.0 | Propuesto | Los **tres** stages, los **ocho** gates materializados con su carácter, triggers, matriz, caché, promoción, reversión y notificaciones |
| [`Estrategia-Versionado.md`](Estrategia-Versionado.md) | 1.0 | Propuesto | Versionado semántico y convenciones de mensaje, criterio de cambio mayor tomado de `ADR-02003`, modelo de ramas y ausencia de canales |
| [`Entornos-Deploy.md`](Entornos-Deploy.md) | 1.0 | Propuesto | Por qué no hay ambientes ni canales, el contenedor de desarrollo como único ambiente, y la ausencia de configuración y de secretos propios |
| [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) | 1.0 | Propuesto | Inventario, firma, nivel de integridad de la construcción, análisis de dependencias, análisis estático y dinámico, y política ante vulnerabilidades |

## 2. Orden de lectura

1. [`Estrategia-Versionado.md`](Estrategia-Versionado.md) — qué gobierna la versión en un proyecto de código que no se publica, y con qué modelo de ramas se trabaja.
2. [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) — qué corre, qué bloquea y qué se registra.
3. [`Entornos-Deploy.md`](Entornos-Deploy.md) — dónde corre, y por qué no hay más ambientes que ése.
4. [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) — qué se verifica sobre la construcción y qué le corresponde a las unidades desplegables.

**El acuerdo de equipo que `Rules-Devops.md` §3.5 sugiere leer primero no existe como documento propio de este producto**: sus reglas —una rama y un pull request por etapa, etapas en serie, punto de control bloqueante— las declara el intake §15 y §10, y las tres se citan desde [`Estrategia-Versionado.md`](Estrategia-Versionado.md) §4.

## 3. Artefactos omitidos y su motivo

| Artefacto | Estado | Motivo |
| --- | --- | --- |
| `Guia-Publicacion-<tipo-artefacto>.md` | **Omitido** | `Rules-Devops.md` §2.1 lo declara obligatorio para «todos los tipos D8 **con artefacto publicable**» y lo omite para «tipos cuyo artefacto no se publica externamente». Éste es exactamente ese caso: el intake §17.1.P.7 · GeometriaFactory-Domain declara que la biblioteca **no se publica en ningún feed** y que se compila dentro del artefacto de agrupación, y §13 lo generaliza al producto entero. **La tensión con el criterio de aceptación de `Rules-Devops.md` §6 —que pide «al menos una» guía— se declara en lugar de resolverse escribiendo una guía vacía**: no hay pre-requisito de cuenta, no hay comando de publicación, no hay verificación posterior y no hay retiro; una guía de publicación de este proyecto de código sería cinco secciones diciendo «no aplica» |
| `Pipeline-Producto.md` | **No es de esta sección** | Es artefacto de nivel producto (`Rules-Devops.md` §2.1 y §4.9). Se emite una sola vez bajo `Producto/`, al cierre del bucle de proyectos de código, y **no lo emite la Fase F de un proyecto de código** |

## 4. Los ocho quality gates, y en qué stage corre cada uno

Resumen de lectura rápida. **El texto vinculante sobre el carácter de cada gate es el de [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../../../08-Calidad-Y-Pruebas/_fusion/Domain/Estrategia-Calidad.md) §3**; el de dónde corre, el de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.1. Esta categoría **no cambió el carácter de ninguno**.

| Gate | Stage donde corre | Carácter |
| --- | --- | --- |
| QG-01 | `build` | Bloqueante |
| QG-02 | `test` | Bloqueante |
| QG-03 | `test`, informe de cobertura por componente | **Condicionado** |
| QG-04 | `build`, con inspección `TC-02024` | Bloqueante |
| QG-05 | `test`, inspección `TC-02023` | Bloqueante |
| QG-06 | `test`, inspección `TC-02026` | Bloqueante al cierre de etapa |
| QG-07 | `test`, duración total de la batería | **Condicionado** |
| QG-08 | Revisión del pull request | Se rechaza en revisión |

**Los dos condicionados se miden y se registran igual.** Dependen de valores rotulados **[ASUNCIÓN]** en el intake §22 —`A-3` para la cobertura, `A-5` para el tiempo de la batería—, y `Estrategia-Calidad.md` §3.1 declara que la puerta no se declara bloqueante en esta categoría hasta que el Product Owner los confirme. **Confirmados, los dos pasan a bloqueantes sin ningún otro cambio**: el umbral no se toca.

## 5. Recuentos que esta sección sostiene

| Magnitud | Valor | Fuente |
| --- | --- | --- |
| Stages del pipeline | **3**: `restore`, `build`, `test` | Intake §17.1.P.8 · GeometriaFactory-Domain; `05` §5 |
| Quality gates materializados | **8**, **6** bloqueantes y **2** condicionados | `08` `Estrategia-Calidad.md` §3 |
| Ambientes de despliegue | **0** | `05` §5; intake §13 |
| Canales de publicación | **0** | Intake §17.1.P.7 · GeometriaFactory-Domain |
| Artefactos publicados | **0** | Intake §13 |
| Secretos propios | **0** | Intake §17.1.P.5 · GeometriaFactory-Domain |
| Dependencias externas | **0** | Intake §17.1.P.1 · GeometriaFactory-Domain; `05` §8 |
| Etapas del producto que este proyecto de código toca | **6**: `a`, `c`, `d`, `e`, `f` y `h` | `08` `Plan-Pruebas.md` §1 |
| Puntos abiertos de esta categoría | **3**: `PD-01` a `PD-03` | [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §10 |

## 6. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial del índice de la categoría 09 de `GeometriaFactory-Domain`. Lista los **cuatro** artefactos emitidos, el orden de lectura, la omisión de la guía de publicación **con la tensión frente al criterio de aceptación de `Rules-Devops.md` §6 declarada** en lugar de resuelta con un documento vacío, el resumen de los **ocho** quality gates con el stage donde corre cada uno y la constancia de que ninguno cambió de carácter, y la tabla de recuentos con la fuente de cada uno. |
