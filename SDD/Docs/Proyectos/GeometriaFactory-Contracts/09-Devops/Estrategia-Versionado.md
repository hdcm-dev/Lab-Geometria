# Estrategia de versionado — GeometriaFactory-Contracts

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** Estrategia-Versionado.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero DevOps Senior + Release Engineer (AG-09)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../05-Arquitectura-Tecnica/Adrs/ADR-03-Versionado-Por-Compilacion-Compartida.md`](../05-Arquitectura-Tecnica/Adrs/ADR-03-Versionado-Por-Compilacion-Compartida.md) 1.0; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.1 §5; [`../08-Calidad-Y-Pruebas/Criterios-Validacion.md`](../08-Calidad-Y-Pruebas/Criterios-Validacion.md) 1.1 §4; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.20** §10, §13, §15, §17.1.P.7, §17.4.P.3 y §17.4.P.7
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

Se adopta el **versionado semántico 2.0.0**, con el formato `MAJOR.MINOR.PATCH[-PRERELEASE][+BUILDMETADATA]`. El intake §17.4.P.7 declara que la estrategia de este proyecto de código es idéntica a la de `GeometriaFactory-Domain` —§17.1.P.7, versionado semántico y convenciones de mensaje **sin excepciones**— y agrega una precisión propia: **un cambio incompatible en un tipo de transferencia es breaking y sube major del producto en el registro de cambios, aunque no se publique en ningún feed**.

**Qué gobierna la versión acá.** [`ADR-03`](../05-Arquitectura-Tecnica/Adrs/ADR-03-Versionado-Por-Compilacion-Compartida.md) §2 lo decide y esta categoría no lo reabre: **la compatibilidad del contrato la gobierna la compilación compartida**, no un esquema de versiones de ruta ni una negociación en tiempo de ejecución. No hay versionado de rutas del servicio ni convivencia de dos versiones del contrato, porque no hay clientes de terceros a quienes dar plazo.

El criterio de clase de cambio se transcribe de `ADR-03` §7 **sin agregarle ni quitarle nada**, y con la columna que es su rasgo distintivo:

| Clase | Qué la produce | ¿Lo detecta la compilación? |
| --- | --- | --- |
| **Mayor** | Quitar o renombrar un tipo o un campo; cambiar el tipo de un campo | Sí |
| **Mayor** | Quitar un valor de un conjunto cerrado —los cuatro estados del trabajo, los dos valores del desenlace, los quince códigos de error— | **No** |
| **Mayor** | Agregar un código al conjunto cerrado de error | **No** |
| **Mayor** | Agregar un campo capaz de transportar una dirección de servicio, una ruta de datos o un secreto | **No** |
| **Menor** | Agregar un tipo o un campo opcional que no viole la regla de exposición | — |
| **Menor** | Agregar un valor a un conjunto cerrado que no sea el de códigos de error | — |
| **Parche** | Corregir el texto neutro de un código sin cambiar su causa | — |

**Tres de las siete clases no las detecta la compilación, y las tres son mayores.** Es el dato que ordena todo el resto de este documento: de las **cuatro** clases mayores, la señal barata —la rotura de compilación— sólo existe para **una**. En las otras tres la única barrera es la revisión, y por eso está escrita como gate en [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.1.

## 2. Convenciones de mensaje de confirmación

Se adoptan las **Conventional Commits 1.0.0**, con el mismo efecto sobre la versión que en el resto del producto:

| Prefijo del mensaje | Efecto sobre la versión |
| --- | --- |
| `feat` | Sube **MINOR** |
| `fix` | Sube **PATCH** |
| `feat!`, o `BREAKING CHANGE` en el pie del mensaje | Sube **MAJOR** |
| `refactor`, `perf`, `test`, `chore`, `docs`, `style`, `build`, `ci` | No sube nada |

**Regla propia de este proyecto de código, y es la más importante de esta sección.** Como tres clases de cambio mayor **compilan igual**, el marcador de cambio incompatible en el mensaje de confirmación **no puede depender de que el compilador avise**: se escribe porque el criterio de §1 dice que corresponde. Un cambio que quita un valor de un conjunto cerrado y llega etiquetado `feat` es un cambio mayor mal marcado, y lo levanta la revisión del pull request, que `CV-18` verifica exigiendo que **todo cambio de un conjunto cerrado esté declarado como incompatible en el `§17` del contrato de uso afectado, aunque compile**.

## 3. Herramienta de cálculo de la versión

**Se declara por su función**, por el mismo motivo que en los otros proyectos de código del producto: el intake §17.1.P.7 —al que §17.4.P.7 remite— dice que la versión la calcula la herramienta que se ancle en la etapa `a`, y ninguna fuente la nombra.

| Aspecto | Decisión |
| --- | --- |
| Función | Calcular la versión desde las etiquetas del repositorio y los mensajes de confirmación desde la última etiqueta |
| Prefijo de etiqueta | El que se fije al anclarla, registrado en el punto de control de la etapa `a` |
| Qué no calcula la herramienta | **La clase de cambio de los tres casos que compilan igual.** Esos los decide el criterio de §1 y los verifica `CV-18`. Ninguna herramienta de comparación de superficie los detectaría sin conocer la semántica de los conjuntos cerrados |

## 4. Modelo de ramas

El del producto, heredado entero y sin variantes: **una rama por etapa** a partir de la principal, con etiqueta al fusionar; **un pull request por etapa, que es el punto de control**; **etapas en serie**, sin abrir la rama de una etapa antes de fusionar la anterior; y sin OK explícito no se avanza (intake §10, §15 y §17.1.P.7).

**Reglas de protección de la rama principal**, que es lo que esta categoría aporta:

- La fusión exige los gates bloqueantes y los de rechazo en revisión de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.1.
- **Todo pull request que agregue o cambie un campo, un tipo o un valor de conjunto cerrado ejecuta las cinco inspecciones de superficie**, por la cadencia por cambio de superficie que [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §5 declara, con el fundamento de que el defecto característico de este proyecto de código entra de a un campo y compila.
- No se exige revisor humano independiente: `equipo_n` es 1 y el filtro es el punto de control bloqueante.

**Las etapas comprometidas que este proyecto de código toca son siete** —`a`, `c`, `d`, `e`, `f`, `g` y `h`—, según [`../08-Calidad-Y-Pruebas/Definition-Of-Done.md`](../08-Calidad-Y-Pruebas/Definition-Of-Done.md) §1.4.

## 5. Canales

**No hay canales de publicación.** El intake §17.4.P.7 declara que no se publica en ningún feed, y §13 lo generaliza al producto. `Rules-Devops.md` §2.2 fija para el tipo `library` el modelo `preview` / `stable` sobre feed único y admite apartarse con un ADR que lo justifique: **el ADR existe y es [`ADR-03`](../05-Arquitectura-Tecnica/Adrs/ADR-03-Versionado-Por-Compilacion-Compartida.md)**, cuyo §4 evalúa y descarta tres alternativas de versionado con convivencia —versionado de rutas, negociación en tiempo de ejecución y compatibilidad sólo aditiva— y adopta la compilación compartida con despliegue conjunto. El apartamiento queda desarrollado en [`Entornos-Deploy.md`](Entornos-Deploy.md) §1.

**Tampoco se usan sufijos de anticipo** —`-alpha`, `-beta`, `-rc`—: no hay canal donde publicar un anticipo del contrato ni integrador que lo consuma. Los dos consumidores compilan contra el estado del repositorio.

## 6. Política de cambios incompatibles

Esta sección reemplaza a la política de obsolescencia que `Rules-Devops.md` §4.3 pide, y el reemplazo está fundado: **una política de obsolescencia da plazo de migración a integradores que no se controlan, y acá no hay ninguno**. `ADR-03` §4 lo dice al descartar el versionado de rutas: no hay a quién darle plazo, los dos consumidores son del mismo producto.

Lo que rige en su lugar:

| Obligación | Cómo se verifica | Fundamento |
| --- | --- | --- |
| Todo cambio de un conjunto cerrado —papel, situación de cuenta, estado del trabajo, severidad, desenlace o código de error— **está declarado como incompatible** en el `§17` del contrato de uso afectado, aunque compile | `CV-18`, 100 % de los cambios declarados | [`../08-Calidad-Y-Pruebas/Criterios-Validacion.md`](../08-Calidad-Y-Pruebas/Criterios-Validacion.md) §4 |
| Ante un cambio incompatible, **las dos unidades desplegables se despliegan juntas** | `CV-19` y `QG-08`, que bloquea la publicación de la etapa | El mismo, y `ADR-03` §2 |
| **Ningún identificador de código retirado se reasigna** a otra condición | `CV-20`, **0** reciclados sobre los **3** retirados | El mismo |
| Todo cambio mayor recibe su fila en el registro de cambios del producto | Revisión del pull request. Objetivo: **0** cambios mayores sin fila | `ADR-03` §8 |
| Reponer un identificador de código retirado **se rechaza aunque compile** | Revisión, contra `CA-09` de `CU-06` | `ADR-03` §7, cierre |

**El conjunto cerrado tiene 15 códigos vivos sobre 18 identificadores emitidos, con 3 retirados** ([`../08-Calidad-Y-Pruebas/Criterios-Validacion.md`](../08-Calidad-Y-Pruebas/Criterios-Validacion.md) `CV-05`). El versionado de este proyecto de código **es, en buena medida, el gobierno de ese conjunto**: dos de las tres clases de cambio mayor que la compilación no detecta son cambios sobre conjuntos cerrados.

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Adopta el versionado semántico 2.0.0 y las Conventional Commits 1.0.0 que el intake §17.4.P.7 hereda de §17.1.P.7, y transcribe el criterio de clase de cambio de `ADR-03` §7 **con su columna de qué detecta la compilación**, dejando a la vista que **tres de las siete clases no la detectan y las tres son mayores**. Declara la herramienta de cálculo por su función y precisa qué **no** calcula ninguna herramienta. Declara el modelo de ramas del producto con la cadencia propia de este proyecto de código —inspección de superficie en todo pull request que cambie un campo— y la ausencia de canales con el ADR que la sostiene. Reemplaza la política de obsolescencia por la **política de cambios incompatibles**, con el fundamento de que no hay integrador externo a quien dar plazo, y con las **cinco** obligaciones que sí rigen. |
