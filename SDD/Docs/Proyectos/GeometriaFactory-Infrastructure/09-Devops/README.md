# 09 · DevOps — GeometriaFactory-Infrastructure

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
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
- [4. Los catorce quality gates, y en qué stage corre cada uno](#4-los-catorce-quality-gates-y-en-qué-stage-corre-cada-uno)
- [5. Recuentos que esta sección sostiene](#5-recuentos-que-esta-sección-sostiene)
- [6. Control de cambios](#6-control-de-cambios)

---

## 1. Artefactos de esta sección

| Documento | Versión | Estado | Propósito |
| --- | --- | --- | --- |
| [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) | 1.1 | Propuesto | Los **cuatro** stages —incluido el propio de este proyecto de código—, los **catorce** gates con su carácter, triggers, matriz, caché, promoción, reversión y notificaciones |
| [`Estrategia-Versionado.md`](Estrategia-Versionado.md) | 1.0 | Propuesto | Versionado semántico, las clases de cambio con las **dos** que compilan, y los **dos linajes** que este proyecto de código versiona además del suyo |
| [`Entornos-Deploy.md`](Entornos-Deploy.md) | 1.1 | Propuesto | Por qué no hay ambientes ni canales, las **tres** exigencias sobre el ambiente ajeno, el respaldo que la fuente dejó abierto y el secreto que se recibe y no se busca |
| [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) | 1.1 | Propuesto | Inventario, firma —con la distinción entre firma de artefacto y firma de acceso—, nivel de integridad, análisis de dependencias con sujeto real, y las **dos** bibliotecas sensibles |

## 2. Orden de lectura

1. [`Estrategia-Versionado.md`](Estrategia-Versionado.md) — qué clase de cambio es cada cosa, y qué sobrevive al despliegue.
2. [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) — qué corre, qué bloquea y qué se registra.
3. [`Entornos-Deploy.md`](Entornos-Deploy.md) — qué le exige al ambiente que lo hospeda, y qué pasa con los datos.
4. [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) — dónde está el riesgo real de este proyecto de código, que acá **sí es de dependencias**.

**El acuerdo de equipo que `Rules-Devops.md` §3.5 sugiere leer primero no existe como documento propio de este producto**: sus reglas —una rama y un pull request por etapa, etapas en serie, punto de control bloqueante— las declara el intake §15 y §10, y las tres se citan desde [`Estrategia-Versionado.md`](Estrategia-Versionado.md) §5.

## 3. Artefactos omitidos y su motivo

| Artefacto | Estado | Motivo |
| --- | --- | --- |
| `Guia-Publicacion-<tipo-artefacto>.md` | **Omitido** | `Rules-Devops.md` §2.1 lo declara obligatorio para «todos los tipos D8 **con artefacto publicable**» y lo omite para «tipos cuyo artefacto no se publica externamente». El intake §17.3.P.7 declara la estrategia idéntica a §17.1.P.7, **sin publicación en feed**, y §13 lo generaliza al producto. **La tensión con el criterio de aceptación de `Rules-Devops.md` §6 se declara en lugar de resolverse con un documento vacío**: no hay cuenta, ni token, ni comando de publicación, ni verificación posterior, ni retiro. A diferencia de `GeometriaFactory-Visor`, este ensamblado **tampoco se entrega**: se referencia dentro de la misma construcción |
| `Pipeline-Producto.md` | **No es de esta sección** | Artefacto de nivel producto (`Rules-Devops.md` §2.1 y §4.9), emitido una sola vez bajo `Producto/` al cierre del bucle de proyectos de código |

## 4. Los catorce quality gates, y en qué stage corre cada uno

Resumen de lectura rápida. **El texto vinculante sobre el carácter de cada gate es el de [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §3**; el de dónde corre, el de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.1. Esta categoría **no cambió el carácter de ninguno**.

| Gate | Stage donde corre | Carácter |
| --- | --- | --- |
| QG-01 | `build` | Bloqueante |
| QG-02 | `test` | Bloqueante |
| QG-03 | `test`, batería del validador (`TC-06001` a `TC-06010`) | Bloqueante |
| QG-04 | **`verificar-transformaciones`**, con `TC-06032` | Bloqueante |
| QG-05 | `test`, informe de cobertura por componente | **Condicionado** |
| QG-06 | `test`, informe acotado a los dos motores | **Condicionado** |
| QG-07 | `test`, con `TC-06009` | Bloqueante. **No es condicionado** |
| QG-08 | `build`, inspección de dependencias de los dos motores (`TC-06014`) | Bloqueante |
| QG-09 | `test`, con `TC-06027` | Bloqueante |
| QG-10 | `test`, con `TC-06019` | Bloqueante |
| QG-11 | `test`, con `TC-06016` y `TC-06021` | Bloqueante |
| QG-12 | `test`, con `TC-06030` | Bloqueante |
| QG-13 | `test`, con `TC-06034` y `TC-06035` en las dos direcciones | Bloqueante |
| QG-14 | `test`, con `TC-06015` | **Condicionado** |

**Los tres condicionados se miden y se registran igual.** Dependen de valores rotulados **[ASUNCIÓN]** en el intake §22 —`A-3` para las dos coberturas, `A-5` para los 200 ms—, y `Estrategia-Calidad.md` §3.1 declara que la puerta no se declara bloqueante en esta categoría hasta que el Product Owner los confirme. **Confirmados, los tres pasan a bloqueantes sin ningún otro cambio.** La tarea que los eleva es `BT-06023`, al cerrar la etapa `d`.

**`QG-07` lleva número y no es condicionado**, y la distinción es de la fuente: el intake §22 enumera la tolerancia de **0.01** entre «lo que **NO** es asunción», con su fundamento. El caso testigo y la consecuencia de confundirlo están en [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.3.

**Una puerta técnica del producto se mide en la etapa `a` de este proyecto de código**: [`../08-Calidad-Y-Pruebas/README.md`](../08-Calidad-Y-Pruebas/README.md) §4 declara que el backlog asigna `PT-04` a su épica de la etapa `a`. El stage `verificar-transformaciones` es **su mitad barata**: verifica la parte de las transformaciones **sin construir la imagen** ([`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.2).

## 5. Recuentos que esta sección sostiene

| Magnitud | Valor | Fuente |
| --- | --- | --- |
| Stages del pipeline | **4**: `restore`, `build`, `test` y `verificar-transformaciones` | Intake §17.3.P.8; `05` §5 |
| Quality gates materializados | **14**, con **3** condicionados | `08` `Estrategia-Calidad.md` §3 |
| Ambientes de despliegue propios | **0**; el ensamblado viaja embebido en **1** proceso | `05` §5; intake §13 |
| Canales de publicación | **0** | Intake §17.3.P.7 |
| Dependencias core externas | **3**, **2** de ellas sensibles | Intake §17.3.P.1 |
| Dependencias de infraestructura | **3**, **ninguna** de red | `05` §5 |
| Secretos propios custodiados | **0**; **1** recibido y no custodiado: la clave de firma | Intake §17.3.P.5; `05` §5 |
| Casos de uso | **10** | `02` §5, citado por `08` README §5 |
| Casos de la batería del validador | **10** | `05` §10.5; intake §21 |
| Escenarios del intake usados como entrada | **8 de 8** | `PRODUCT-INTAKE` §20 |
| Condiciones distintas catalogadas | **17** | `03` §7.1, citado por `08` README §5 |
| Componentes | **8** | `05` §3.1, citado por `08` README §5 |
| Criterios de salida del plan de pruebas | **11** | `08` `Plan-Pruebas.md` §3 |
| Etapas que este proyecto de código toca | **5**: `a`, `c`, `d`, `e` y `f` | `06` `Product-Backlog.md` §2, citado por `08` README §5 |
| Puntos abiertos de esta categoría | **5**: `PD-01` a `PD-05`, el último abierto por el `H-04` de la auditoría de la ronda 1 | [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §10 |

## 6. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial del índice de la categoría 09 de `GeometriaFactory-Infrastructure`. Lista los **cuatro** artefactos emitidos, el orden de lectura, la omisión de la guía de publicación **con la tensión frente al criterio de aceptación de `Rules-Devops.md` §6 declarada**, el resumen de los **catorce** quality gates con el stage donde corre cada uno —incluido el **cuarto stage propio de este proyecto de código**— y la constancia de que ninguno cambió de carácter, con la precisión de que **`QG-07` lleva número y no es condicionado**. Deja registrado que la puerta técnica `PT-04` se mide en la etapa `a` de este proyecto de código y que el cuarto stage es su mitad barata, y la tabla de recuentos con la fuente de cada uno. |
| 1.1 | 2026-08-11 | **Constancia de las correcciones de la auditoría `F-09-Devops-Siete-Proyectos-r1.md`.** Sube a 1.1 en §1 las versiones de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) —`H-03`, atribución de la procedencia de los gates a §17.3.P.8, y apertura de `PD-05` por `H-04`—, [`Entornos-Deploy.md`](Entornos-Deploy.md) —`H-02`, cita del intake §17.3.P.3, y `H-04`, apartamiento del modelo de canales sin la ADR que `Rules-Devops.md` §2.2 exige— y [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) —atribución de una cita que el informe no había listado—. Los puntos abiertos de la categoría pasan de **cuatro** a **cinco**. El intake **1.22** no cambia nada de este proyecto de código: sus dos decisiones son de §17.6.P.7 y alcanzan a las dos unidades desplegables. |
