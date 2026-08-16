# 09 · DevOps — GeometriaFactory-Contracts

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** README.md
**Versión:** 1.2
**Estado:** Aprobado
**Fecha:** 2026-08-12
**Autor:** Ingeniero DevOps Senior + Release Engineer (AG-09)
**Tipo de proyecto de código (D8):** `library`

---

## Tabla de contenido

- [1. Artefactos de esta sección](#1-artefactos-de-esta-sección)
- [2. Orden de lectura](#2-orden-de-lectura)
- [3. Artefactos omitidos y su motivo](#3-artefactos-omitidos-y-su-motivo)
- [4. Los nueve quality gates, y dónde corre cada uno](#4-los-nueve-quality-gates-y-dónde-corre-cada-uno)
- [5. Recuentos que esta sección sostiene](#5-recuentos-que-esta-sección-sostiene)
- [6. Control de cambios](#6-control-de-cambios)

---

## 1. Artefactos de esta sección

| Documento | Versión | Estado | Propósito |
| --- | --- | --- | --- |
| [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) | 1.1 | Propuesto | Los **dos** stages, los **nueve** gates materializados, dónde corre el que este proyecto de código no puede correr solo, y la regla de despliegue conjunto con su orden |
| [`Estrategia-Versionado.md`](Estrategia-Versionado.md) | 1.0 | Propuesto | Versionado semántico, criterio de clase de cambio de `ADR-08003` con la columna de qué detecta la compilación, y política de cambios incompatibles |
| [`Entornos-Deploy.md`](Entornos-Deploy.md) | 1.1 | Propuesto | Por qué no hay ambientes ni canales propios, dónde viaja el ensamblado, y la superficie de exposición como regla de secretos |
| [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) | 1.0 | Propuesto | Inventario, firma, nivel de integridad, análisis, política ante vulnerabilidades, y la superficie de exposición como preocupación de cadena de suministro |

## 2. Orden de lectura

1. [`Estrategia-Versionado.md`](Estrategia-Versionado.md) — qué clase de cambio es cada cosa, y cuáles no las detecta la compilación.
2. [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) — qué corre, qué bloquea, y qué se difiere por escrito mientras la batería de integración no exista.
3. [`Entornos-Deploy.md`](Entornos-Deploy.md) — dónde viaja el ensamblado y quién es dueño de cada despliegue.
4. [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) — dónde está el riesgo real de este proyecto de código.

## 3. Artefactos omitidos y su motivo

| Artefacto | Estado | Motivo |
| --- | --- | --- |
| `Guia-Publicacion-<tipo-artefacto>.md` | **Omitido** | `Rules-Devops.md` §2.1 lo declara obligatorio para «todos los tipos D8 **con artefacto publicable**» y lo omite para «tipos cuyo artefacto no se publica externamente». El intake §17.4.P.7 declara que este ensamblado **no se publica en ningún feed**, y §13 lo generaliza al producto. **La tensión con el criterio de aceptación de `Rules-Devops.md` §6 se declara en lugar de resolverse con un documento vacío**: no hay cuenta, ni token, ni comando de publicación, ni verificación posterior, ni retiro. Lo más parecido a una entrega que este proyecto de código tiene es el **despliegue conjunto**, y su procedimiento vive donde corresponde: en [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §6, y en las categorías 09 de los dos proyectos de código que se despliegan |
| `Pipeline-Producto.md` | **No es de esta sección** | Artefacto de nivel producto (`Rules-Devops.md` §4.9), emitido una sola vez bajo `Producto/` al cierre del bucle de proyectos de código |

## 4. Los nueve quality gates, y dónde corre cada uno

**El texto vinculante sobre el carácter de cada gate es el de [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §3.** Esta categoría **no cambió el carácter de ninguno**.

| Gate | Dónde corre | Carácter |
| --- | --- | --- |
| QG-01 | `build` | Bloqueante |
| QG-02 | `build`, inspección del archivo de proyecto | Bloqueante |
| QG-03 | Inspección de superficie, en cada pull request que toca un campo | Se rechaza aunque compile |
| QG-04 | Inspección de superficie del conjunto cerrado | Se rechaza aunque compile |
| QG-05 | **Batería de integración**, ejecutada con el guion de pruebas del producto | **Bloqueante** |
| QG-06 | Inspección de superficie de la familia de listado | **Condicionado** |
| QG-07 | Inspección de superficie de la respuesta de sesión | Se rechaza aunque compile |
| QG-08 | Revisión del pull request de la etapa | Bloquea la **publicación** de la etapa |
| QG-09 | Inspección de superficie | Se rechaza aunque compile |

**El único gate condicionado es `QG-06`**, cuyo rótulo **[ASUNCIÓN derivada]** viene del intake §17.4.P.10 y pone en duda **qué se verifica**. **`QG-05` bloquea**: su rótulo viene de §17.4.P.6, que lo llama «el gate equivalente y bloqueante», y la fila `A-4` del intake §22 declara que un cambio del Product Owner «cambia la forma del gate, no su carácter bloqueante». Es la distinción que la Fase E fijó tras corregirse a sí misma, y esta categoría la materializa sin reabrirla.

**`QG-05` es además el único gate del nivel topológico 0 que depende de un proyecto de código que todavía no existe.** Mientras `GeometriaFactory-Api` no esté, la prueba de integración **se difiere por escrito**, con la etapa en que se ejecuta, y la inspección de superficie equivalente **corre igual**: las tres condiciones están en [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.2.

## 5. Recuentos que esta sección sostiene

| Magnitud | Valor | Fuente |
| --- | --- | --- |
| Stages del pipeline | **2**: `restore`, `build` | Intake §17.4.P.8; `05` §5 |
| Quality gates materializados | **9**, con **1** condicionado | `08` `Estrategia-Calidad.md` §3 |
| Ambientes de despliegue propios | **0**; el ensamblado viaja embebido en **2** procesos | `05` §5; intake §17.4.P.9 |
| Canales de publicación | **0** | Intake §17.4.P.7 |
| Dependencias externas | **0** | Intake §17.4.P.1 |
| Códigos vivos del conjunto cerrado | **17**, sobre **20** emitidos, con **3** retirados y **0** reciclados | `08` `Criterios-Validacion.md` `CV-05` y `CV-20` |
| Contratos de uso | **8** | `02` §3, citado por `08` README §5 |
| Clases de cambio que la compilación **no** detecta | **3**, las tres mayores | `ADR-08003` §7 |
| Etapas comprometidas que este proyecto de código toca | **7**: `a`, `c`, `d`, `e`, `f`, `g` y `h` | `08` `Definition-Of-Done.md` §1.4 |
| Puntos abiertos de esta categoría | **4** declarados, `PD-01` a `PD-04`, de los cuales **3 vigentes**: `PD-01` quedó cerrado por el intake 1.22 y conserva su fila | [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §10 |

## 6. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial del índice de la categoría 09 de `GeometriaFactory-Contracts`. Lista los **cuatro** artefactos emitidos, el orden de lectura, la omisión de la guía de publicación con la tensión frente al criterio de aceptación de `Rules-Devops.md` §6 declarada, el resumen de los **nueve** quality gates con dónde corre cada uno y la constancia de que ninguno cambió de carácter —`QG-05` bloqueante, `QG-06` único condicionado—, y la tabla de recuentos con la fuente de cada uno. Deja registrado que `QG-05` es el único gate del nivel topológico 0 que depende de un proyecto de código que todavía no existe, y las tres condiciones de su diferimiento. |
| 1.1 | 2026-08-11 | **Propagación de las dos decisiones de despliegue del Product Owner** del intake **1.22** §17.6.P.7. El `PD-01` que esta categoría había elevado —el filtro de rutas del flujo del front dejaba fuera a este ensamblado— quedó **cerrado**: §17.6.P.7 enumera hoy las **tres** rutas. La fila de recuentos pasa a declarar **4** puntos abiertos con **3 vigentes**, y se actualizan a 1.1 las versiones de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) y [`Entornos-Deploy.md`](Entornos-Deploy.md) en §1. |
| 1.2 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
