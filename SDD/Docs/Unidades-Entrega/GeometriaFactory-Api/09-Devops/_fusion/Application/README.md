# 09 · DevOps — GeometriaFactory-Application

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
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
- [4. Los once quality gates, y en qué stage corre cada uno](#4-los-once-quality-gates-y-en-qué-stage-corre-cada-uno)
- [5. Recuentos que esta sección sostiene](#5-recuentos-que-esta-sección-sostiene)
- [6. Control de cambios](#6-control-de-cambios)

---

## 1. Artefactos de esta sección

| Documento | Versión | Estado | Propósito |
| --- | --- | --- | --- |
| [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) | 1.0 | Propuesto | Los **tres** stages, los **once** gates materializados con su carácter, la puerta propia del intake y dónde se la hace cumplir, triggers, matriz, caché, promoción, reversión y notificaciones |
| [`Estrategia-Versionado.md`](Estrategia-Versionado.md) | 1.0 | Propuesto | Versionado semántico y convenciones de mensaje, la tabla de clases de cambio de `ADR-04003` con su asimetría aditiva-mayor, modelo de ramas y ausencia de canales |
| [`Entornos-Deploy.md`](Entornos-Deploy.md) | 1.0 | Propuesto | Por qué no hay ambientes ni canales, dónde viaja el ensamblado, y la ausencia de configuración y de secretos propios |
| [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) | 1.0 | Propuesto | Inventario, firma, nivel de integridad de la construcción, análisis de dependencias, análisis estático y dinámico, política ante vulnerabilidades, y la autorización como preocupación de cadena de suministro |

## 2. Orden de lectura

1. [`Estrategia-Versionado.md`](Estrategia-Versionado.md) — qué clase de cambio es cada cosa, y por qué agregar una operación a un puerto es mayor.
2. [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) — qué corre, qué bloquea y qué se registra.
3. [`Entornos-Deploy.md`](Entornos-Deploy.md) — dónde viaja el ensamblado, y por qué no hay más ambiente que el contenedor de desarrollo.
4. [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) — dónde está el riesgo real de este proyecto de código.

**El acuerdo de equipo que `Rules-Devops.md` §3.5 sugiere leer primero no existe como documento propio de este producto**: sus reglas —una rama y un pull request por etapa, etapas en serie, punto de control bloqueante— las declara el intake §15 y §10, y las tres se citan desde [`Estrategia-Versionado.md`](Estrategia-Versionado.md) §4.

## 3. Artefactos omitidos y su motivo

| Artefacto | Estado | Motivo |
| --- | --- | --- |
| `Guia-Publicacion-<tipo-artefacto>.md` | **Omitido** | `Rules-Devops.md` §2.1 lo declara obligatorio para «todos los tipos D8 **con artefacto publicable**» y lo omite para «tipos cuyo artefacto no se publica externamente». El intake §17.1.P.7 · GeometriaFactory-Application declara la estrategia idéntica a §17.1.P.7 · GeometriaFactory-Domain, **sin publicación en feed**, y §13 lo generaliza al producto. **La tensión con el criterio de aceptación de `Rules-Devops.md` §6 —que pide «al menos una» guía— se declara en lugar de resolverse escribiendo una guía vacía**: no hay cuenta, ni token, ni comando de publicación, ni verificación posterior, ni retiro. A diferencia de `GeometriaFactory-Visor`, este ensamblado **tampoco se entrega**: se referencia dentro de la misma construcción |
| `Pipeline-Producto.md` | **No es de esta sección** | Es artefacto de nivel producto (`Rules-Devops.md` §2.1 y §4.9). Se emite una sola vez bajo `Producto/`, al cierre del bucle de proyectos de código, y **no lo emite la Fase F de un proyecto de código** |

## 4. Los once quality gates, y en qué stage corre cada uno

Resumen de lectura rápida. **El texto vinculante sobre el carácter de cada gate es el de [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../../../08-Calidad-Y-Pruebas/_fusion/Application/Estrategia-Calidad.md) §3**; el de dónde corre, el de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.1. Esta categoría **no cambió el carácter de ninguno**.

| Gate | Stage donde corre | Carácter |
| --- | --- | --- |
| QG-01 | `build` | Bloqueante |
| QG-02 | `test` | Bloqueante |
| QG-03 | `test`, informe de cobertura por componente | **Condicionado** |
| QG-04 | `test`, con `TC-04026`, más la revisión del pull request | Bloqueante |
| QG-05 | `build`, inspección del archivo de proyecto (`TC-04027`) | Bloqueante |
| QG-06 | `test`, inspección `TC-04028` en las dos direcciones | Bloqueante |
| QG-07 | Cierre de la etapa, con `TC-04011` y la matriz de comprobaciones | Bloqueante al cierre de etapa |
| QG-08 | `test`, inspección de los once orquestadores y `TC-04029` | Bloqueante |
| QG-09 | `test`, con `TC-04030` | Bloqueante |
| QG-10 | `test`, medición sobre la batería unitaria | **Condicionado** |
| QG-11 | Revisión del pull request, con `TC-04031` | Se rechaza en revisión |

**Los dos condicionados se miden y se registran igual.** Dependen de valores rotulados **[ASUNCIÓN]** en el intake §22 —`A-3` para la cobertura, `A-5` para los 500 ms—, y `Estrategia-Calidad.md` §3.1 declara que la puerta no se declara bloqueante en esta categoría hasta que el Product Owner los confirme. **Confirmados, los dos pasan a bloqueantes sin ningún otro cambio**: el umbral no se toca. La tarea que los eleva es `BT-04018`, al cerrar la etapa `d`.

**`QG-05` es el gate que sostiene a `QG-04`, y por eso corre antes.** Es la única decisión de ubicación que esta categoría tomó por su cuenta, y su fundamento está en [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.2: con **0** bibliotecas de persistencia declaradas, una prueba de esta capa no tiene con qué abrir un almacén.

## 5. Recuentos que esta sección sostiene

| Magnitud | Valor | Fuente |
| --- | --- | --- |
| Stages del pipeline | **3**: `restore`, `build`, `test` | `05` §5; intake §17.1.P.8 · GeometriaFactory-Application |
| Quality gates materializados | **11**, con **2** condicionados | `08` `Estrategia-Calidad.md` §3 |
| Ambientes de despliegue propios | **0**; el ensamblado viaja embebido en **1** proceso | `05` §5; intake §13 |
| Canales de publicación | **0** | Intake §17.1.P.7 · GeometriaFactory-Application |
| Dependencias externas | **0**; **1** referencia a otro proyecto de código del producto | Intake §17.1.P.1 · GeometriaFactory-Application; `QG-05` |
| Casos de uso | **11** | `02` §5, citado por `08` README §5 |
| Puertos | **4** | `02` §3, citado por `08` README §5 |
| Comprobaciones de autorización | **4** | `02` §4, citado por `08` README §5 |
| Condiciones distintas catalogadas | **36** | `03` §7.1, citado por `08` README §5 |
| Componentes | **8** | `05` §3.1, citado por `08` README §5 |
| Criterios de salida del plan de pruebas | **11** | `08` `Plan-Pruebas.md` §3 |
| Etapas que este proyecto de código toca | **6**: `a`, `c`, `d`, `e`, `f` y `h` | `06` `Product-Backlog.md` §2, citado por `08` README §5 |
| Puntos abiertos de esta categoría | **3**: `PD-01` a `PD-03` | [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §10 |

## 6. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial del índice de la categoría 09 de `GeometriaFactory-Application`. Lista los **cuatro** artefactos emitidos, el orden de lectura, la omisión de la guía de publicación **con la tensión frente al criterio de aceptación de `Rules-Devops.md` §6 declarada** y con la precisión de que este ensamblado tampoco se entrega, el resumen de los **once** quality gates con el stage donde corre cada uno y la constancia de que ninguno cambió de carácter, y la tabla de recuentos con la fuente de cada uno. Deja registrada la única decisión de ubicación propia de esta categoría: `QG-05` corre en `build` porque es lo que sostiene a `QG-04`. |
