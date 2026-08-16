# Estrategia de versionado — GeometriaFactory-Application

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** Estrategia-Versionado.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero DevOps Senior + Release Engineer (AG-09)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../05-Arquitectura-Tecnica/Adrs/ADR-04003-Versionado-Y-Estabilidad-De-La-Superficie.md`](../../../05-Arquitectura-Tecnica/Adrs/ADR-04003-Versionado-Y-Estabilidad-De-La-Superficie.md) 1.0 §2, §7 y §8; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §5 y §11 (`PA-06`); [`../08-Calidad-Y-Pruebas/Definition-Of-Done.md`](../../../08-Calidad-Y-Pruebas/Definition-Of-Done.md) 1.0 §1.3; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.21** §10, §13, §15, §17.1.P.7 · GeometriaFactory-Domain, §17.1.P.3 · GeometriaFactory-Application y §17.1.P.7 · GeometriaFactory-Application
**Trazabilidad downstream:** [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md), [`Entornos-Deploy.md`](Entornos-Deploy.md)

---

## Tabla de contenido

- [1. Versionado semántico](#1-versionado-semántico)
- [2. Convenciones de mensaje de confirmación](#2-convenciones-de-mensaje-de-confirmación)
- [3. Herramienta de cálculo de la versión](#3-herramienta-de-cálculo-de-la-versión)
- [4. Modelo de ramas](#4-modelo-de-ramas)
- [5. Canales](#5-canales)
- [6. Política de cambios incompatibles](#6-política-de-cambios-incompatibles)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Versionado semántico

Se adopta el **versionado semántico 2.0.0**, con el formato `MAJOR.MINOR.PATCH[-PRERELEASE][+BUILDMETADATA]`. El intake §17.1.P.7 · GeometriaFactory-Application declara la estrategia de este proyecto de código **idéntica a la de §17.1.P.7 · GeometriaFactory-Domain**: versionado semántico, convenciones de mensaje de confirmación, **sin publicación en feed**, y una rama y una etiqueta por etapa.

**Qué gobierna la compatibilidad acá, y no lo decide esta categoría.** [`ADR-04003`](../../../05-Arquitectura-Tecnica/Adrs/ADR-04003-Versionado-Y-Estabilidad-De-La-Superficie.md) §2 lo decide: **el contrato se protege por compilación compartida y no por descripción formal ni por convivencia de versiones**, un cambio incompatible rompe la compilación del artefacto de agrupación, y la política es corregir las dos caras **en la misma etapa**.

**La superficie de este proyecto de código tiene dos caras, y de ahí sale su asimetría propia.** `ADR-04003` §2 la declara y esta categoría la transcribe sin tocarla: **agregar una operación a un puerto es cambio mayor**, porque obliga a todo implementador a proveerla, mientras que agregar un caso de uso es cambio menor. La tabla de clases se toma de `ADR-04003` §7 sin agregarle ni quitarle nada:

| Cambio sobre la superficie | Cara | Clase |
| --- | --- | --- |
| Quitar o renombrar un caso de uso, o cambiar su postcondición | Hacia arriba | **Mayor** |
| Cambiar qué exige resuelto un caso de uso antes de invocarlo | Hacia arriba | **Mayor** |
| Quitar, renombrar o cambiar la firma de una operación de un puerto | Hacia abajo | **Mayor** |
| **Agregar** una operación a un puerto existente | Hacia abajo | **Mayor**, por la asimetría de `ADR-04003` §2 |
| Agregar un puerto nuevo | Hacia abajo | **Mayor** |
| Quitar una condición del catálogo de `03`, o reciclar su identificador | Las dos | **Mayor** |
| Agregar un caso de uso | Hacia arriba | Menor |
| Agregar una condición al catálogo de `03` | Las dos | Menor |
| Corregir un orquestador para que ejerza la comprobación que ya declaraba | Ninguna | Parche |

**La fila que hay que leer dos veces es la cuarta.** Es contraintuitiva —agregar suele ser menor— y es la única de las nueve donde un cambio aditivo sube mayor. El motivo es que la cara de abajo es un contrato **que otro implementa**: `GeometriaFactory-Infrastructure` tiene que proveer la operación nueva, y hasta que la provea el artefacto de agrupación no compila.

## 2. Convenciones de mensaje de confirmación

Se adoptan las **Conventional Commits 1.0.0**, con el mismo efecto sobre la versión que en el resto del producto:

| Prefijo del mensaje | Efecto sobre la versión |
| --- | --- |
| `feat` | Sube **MINOR** |
| `fix` | Sube **PATCH** |
| `feat!`, o `BREAKING CHANGE` en el pie del mensaje | Sube **MAJOR** |
| `refactor`, `perf`, `test`, `chore`, `docs`, `style`, `build`, `ci` | No sube nada |

**Precisión propia de este proyecto de código.** La tabla de §1 tiene **una** fila donde un cambio que se escribiría naturalmente como `feat` es **mayor**: agregar una operación a un puerto. Quien la escriba tiene que marcarla con `feat!` o con el pie de cambio incompatible **aunque el verbo del cambio sea «agregar»**. No hay herramienta que lo deduzca; lo deduce el criterio de `ADR-04003` §7 y lo verifica la revisión del pull request.

## 3. Herramienta de cálculo de la versión

**Se declara por su función, y esta categoría no la elige.** `05` §11 registra el punto abierto `PA-06` —«la herramienta que calcula la versión a partir de las convenciones de mensaje de confirmación no está elegida»— y lo ata al punto de control de la etapa `a`; `ADR-04003` §7 dice lo mismo. Elegirla acá cerraría un punto abierto que la fuente dejó atado a una medición que todavía no se hizo.

| Aspecto | Decisión |
| --- | --- |
| Función | Calcular la versión desde las etiquetas del repositorio y los mensajes de confirmación desde la última etiqueta |
| Dónde vive la versión | En el archivo de proyecto, calculada; `ADR-04003` §7 lo declara |
| Prefijo de etiqueta | El que se fije al anclarla, registrado en el punto de control de la etapa `a` |
| Qué **no** calcula la herramienta | La clase de cambio de la fila aditiva-mayor de §1. Ninguna herramienta de comparación de superficie la marcaría como mayor sin conocer que la cara de abajo la implementa otro proyecto de código |

## 4. Modelo de ramas

El del producto, heredado entero y sin variantes: **una rama por etapa** a partir de la principal, con etiqueta al fusionar; **un pull request por etapa, que es el punto de control**; **etapas en serie**, sin abrir la rama de una etapa antes de fusionar la anterior; y sin OK explícito no se avanza (intake §10, §15 y §17.1.P.7 · GeometriaFactory-Domain).

**Reglas de protección de la rama principal**, que es lo que esta categoría aporta:

- La fusión exige los gates bloqueantes y los de rechazo en revisión de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.1.
- **Todo pull request que agregue o cambie un caso de uso, un puerto o una condición del catálogo ejecuta las inspecciones correspondientes** —`TC-04028` en las dos direcciones y `TC-04029` sobre el caso de uso tocado—, por la cadencia que [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §3 declara.
- No se exige revisor humano independiente: `equipo_n` es 1 y el filtro es el punto de control bloqueante, exactamente como lo declara [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../../../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §4.

**Las etapas que este proyecto de código toca son seis** —`a`, `c`, `d`, `e`, `f` y `h`—, según [`../06-Backlog-Tecnico/Product-Backlog.md`](../../../06-Backlog-Tecnico/_fusion/Application/Product-Backlog.md) §2, citado por [`../08-Calidad-Y-Pruebas/README.md`](../../../08-Calidad-Y-Pruebas/README.md) §5.

## 5. Canales

**No hay canales de publicación.** El intake §17.1.P.7 · GeometriaFactory-Application, por remisión a §17.1.P.7 · GeometriaFactory-Domain, declara que no se publica en ningún feed, y §13 lo generaliza al producto entero: **ningún proyecto de código se publica como paquete redistribuible**. `05` §5 lo repite en su última fila.

`Rules-Devops.md` §2.2 fija para el tipo `library` el modelo `preview` / `stable` sobre feed único y admite apartarse con un ADR que lo justifique: **el ADR existe y es [`ADR-04003`](../../../05-Arquitectura-Tecnica/Adrs/ADR-04003-Versionado-Y-Estabilidad-De-La-Superficie.md)**, cuyo §2 declara que no se publica en ningún repositorio de paquetes y que por eso **no hay deprecación gradual, ni versiones conviviendo, ni consumidor externo al que avisar**. El apartamiento queda desarrollado en [`Entornos-Deploy.md`](Entornos-Deploy.md) §1.

**Tampoco se usan sufijos de anticipo** —`-alpha`, `-beta`, `-rc`—: no hay canal donde publicar un anticipo ni integrador que lo consuma. Los dos consumidores compilan contra el estado del repositorio.

## 6. Política de cambios incompatibles

Esta sección reemplaza a la política de obsolescencia que `Rules-Devops.md` §4.3 pide, y el reemplazo está fundado: **una política de obsolescencia da plazo de migración a integradores que no se controlan, y acá no hay ninguno**. Lo que rige en su lugar sale de `ADR-04003` y de la Definition of Done:

| Obligación | Cómo se verifica | Fundamento |
| --- | --- | --- |
| Ante un cambio mayor, **las dos caras se corrigen en la misma etapa** | Imposible por construcción: el artefacto de agrupación no compila. Se verifica en cada pull request | `ADR-04003` §2 y §8, segunda métrica |
| **0** advertencias de construcción | `QG-01`, en el stage `build` | `ADR-04003` §8, primera métrica |
| **0** paquetes publicados en un repositorio de paquetes | Inspección del pipeline | `ADR-04003` §8, tercera métrica |
| **0** etapas cerradas sin etiqueta | Inspección de etiquetas contra el índice de informes de cierre | `ADR-04003` §8, cuarta métrica |
| Todo cambio mayor recibe su fila en el registro de cambios del producto | Revisión del pull request de la etapa, que **es** el punto de control | Intake §15, regla de delivery 3; `changelog.md` del árbol del intake §16 |
| Una condición retirada del catálogo **no recicla su identificador** | Revisión, con la fila «quitar una condición del catálogo, o reciclar su identificador» de §1 | `ADR-04003` §7 |

**Las cuatro métricas de `ADR-04003` §8 se adoptan sin agregar ninguna.** La segunda es la más fuerte del documento y conviene no perderle el sentido: su modo de verificación es «imposible por construcción», y eso es exactamente lo que compra la compilación compartida. Donde la compilación no llega —el reciclado de un identificador de condición— el filtro es la revisión, y por eso figura como fila propia.

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Adopta el versionado semántico 2.0.0 y las Conventional Commits 1.0.0 que el intake §17.1.P.7 · GeometriaFactory-Application hereda de §17.1.P.7 · GeometriaFactory-Domain, y transcribe la tabla de clases de cambio de `ADR-04003` §7 con su **asimetría propia**: agregar una operación a un puerto es **mayor**, y es la única fila aditiva de las nueve que lo es. Declara la herramienta de cálculo **por su función**, sin cerrar el punto abierto `PA-06` que la fuente ató a la etapa `a`, y precisa qué no calcularía ninguna herramienta. Declara el modelo de ramas del producto con la cadencia propia de este proyecto de código y la ausencia de canales con el ADR que la sostiene. Reemplaza la política de obsolescencia por la **política de cambios incompatibles**, con el fundamento de que no hay integrador externo a quien dar plazo, y adopta las **cuatro** métricas de `ADR-04003` §8 sin agregar ninguna. |
