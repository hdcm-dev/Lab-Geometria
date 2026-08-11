# Estrategia de versionado — GeometriaFactory-Domain

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** Estrategia-Versionado.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Ingeniero DevOps Senior + Release Engineer (AG-09)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../05-Arquitectura-Tecnica/Adrs/ADR-03-Versionado-Y-Estabilidad-De-La-Superficie.md`](../05-Arquitectura-Tecnica/Adrs/ADR-03-Versionado-Y-Estabilidad-De-La-Superficie.md) 1.0; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §5; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.20** §10, §13, §15, §17.1.P.7 y §17.1.P.11
**Trazabilidad downstream:** [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md), [`Entornos-Deploy.md`](Entornos-Deploy.md); `11-Documentacion` cuando se emita

---

## Tabla de contenido

- [1. Versionado semántico](#1-versionado-semántico)
- [2. Convenciones de mensaje de confirmación](#2-convenciones-de-mensaje-de-confirmación)
- [3. Herramienta de cálculo de la versión](#3-herramienta-de-cálculo-de-la-versión)
- [4. Modelo de ramas](#4-modelo-de-ramas)
- [5. Canales](#5-canales)
- [6. Política de obsolescencia y de cambios incompatibles](#6-política-de-obsolescencia-y-de-cambios-incompatibles)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Versionado semántico

**Se adopta el versionado semántico en su versión 2.0.0**, con el formato `MAJOR.MINOR.PATCH[-PRERELEASE][+BUILDMETADATA]`. El intake §17.1.P.7 lo declara «sin excepciones», junto con las convenciones de mensaje de confirmación.

**Qué gobierna la versión acá, que es la pregunta que hay que contestar en un proyecto de código que no se publica.** [`ADR-03`](../05-Arquitectura-Tecnica/Adrs/ADR-03-Versionado-Y-Estabilidad-De-La-Superficie.md) §2 la contesta: gobierna la **compatibilidad de compilación de los dos consumidores del dominio**, `GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`. Esta categoría no reabre esa decisión y no agrega criterios: transcribe el criterio de §7 de esa ADR porque es el que el pipeline tiene que hacer cumplir.

| Clase | Qué la produce, según `ADR-03` §7 |
| --- | --- |
| **Mayor** | Quitar o renombrar un tipo, una operación o un atributo de la superficie pública; cambiar qué recibe una operación; **quitar un valor de un conjunto cerrado** —los cuatro estados del trabajo, los tres estados de cuenta, los dos papeles, las dos especies de observación—; y **perder cualquiera de los nueve invariantes**, aunque ninguna firma cambie |
| **Menor** | Agregar un tipo, una operación o un atributo opcional; **agregar un valor a un conjunto cerrado**, que obliga al consumidor a contemplarlo pero no rompe su compilación; agregar una condición de error al catálogo |
| **Parche** | Corregir el comportamiento de una guarda para que cumpla el invariante que ya declaraba, sin cambiar la superficie |

**La fila que conviene no perder de vista es la última de «mayor»**: perder un invariante es cambio mayor aunque ninguna firma se toque. No lo detecta ninguna herramienta de resolución de dependencias; lo detecta `QG-06`, que exige los **nueve** invariantes ejercidos con prueba de violación rechazada y sin dobles.

**Desde cuándo hay superficie que versionar.** `ADR-03` §2 declara que la superficie pública empieza a ser estable en el **punto de control de la etapa `a`**, cuando se fijan los nombres de tipos y de espacios de nombres que el intake §17.1.P.11 deja abiertos. Todo lo anterior es prehistoria de versionado y no genera cambio mayor.

## 2. Convenciones de mensaje de confirmación

Se adoptan las **Conventional Commits 1.0.0**, declaradas por el intake §17.1.P.7 sin excepciones. El efecto sobre el número de versión es el de la tabla, y es lo que hace que la versión se calcule y no se escriba a mano:

| Prefijo del mensaje | Efecto sobre la versión |
| --- | --- |
| `feat` | Sube **MINOR** |
| `fix` | Sube **PATCH** |
| `feat!`, o `BREAKING CHANGE` en el pie del mensaje | Sube **MAJOR** |
| `refactor`, `perf`, `test`, `chore`, `docs`, `style`, `build`, `ci` | No sube nada |

**El prefijo no reemplaza al criterio de §1.** Un cambio marcado `feat` que en realidad quita un valor de un conjunto cerrado es un cambio mayor mal etiquetado, y lo levanta la revisión del pull request de la etapa. La convención de mensajes ordena el cálculo; **quien decide la clase es el criterio de `ADR-03` §7**.

## 3. Herramienta de cálculo de la versión

**Se declara por su función y no por su producto, y es deliberado.** El intake §17.1.P.7 dice que la versión la calcula «la herramienta que se ancle en la etapa `a`» y que se registra en ese momento; `ADR-03` §6 acepta explícitamente depender de una herramienta todavía no elegida y **no la nombra**. Esta categoría hace lo mismo: nombrar una acá sería inventar una decisión que el intake ata a un punto de control futuro.

| Aspecto | Decisión |
| --- | --- |
| Función | Calcular la versión a partir de las etiquetas del repositorio y de los mensajes de confirmación desde la última etiqueta |
| Prefijo de etiqueta | El que se fije al anclarla, registrado en el mismo punto de control |
| Dónde se ancla | Etapa `a`, por la regla de anclaje de versiones del intake, encabezado de la Parte C: toda versión se fija explícitamente y su cambio mayor se documenta, nunca como efecto colateral de una actualización |
| Qué se registra | La elección y su versión, en el punto de control de la etapa `a`. Queda abierto como `PD-01` de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §10 |

**Lo que no se hace es versionar a mano.** El anti-patrón está declarado en `Rules-Devops.md` §4.8 y el intake ya lo previene al exigir el cálculo por herramienta.

## 4. Modelo de ramas

El modelo lo declara el producto y este proyecto de código lo hereda entero. No se elige acá ninguna variante:

- **Una rama por etapa**, a partir de la rama principal, con **etiqueta al fusionar** (intake §17.1.P.7).
- **Un pull request por etapa, y el pull request es el punto de control** (intake §15).
- **Etapas en serie**: no se abre la rama de una etapa antes de que la anterior esté fusionada (intake §10 y §15).
- **Sin OK explícito del Product Owner no se avanza** (intake §10, restricción «etapas en serie»).

**Consecuencia sobre las reglas de protección de la rama principal**, que es lo que esta categoría sí aporta: la fusión exige los gates bloqueantes de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.1 en verde y la constancia del OK del punto de control. **No se exige un revisor humano independiente**, y no por relajación: `equipo_n` es 1 y [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §4 ya declara que lo que reemplaza al revisor independiente es el punto de control bloqueante de cada etapa.

**Las etapas que este proyecto de código toca son seis** —`a`, `c`, `d`, `e`, `f` y `h`—, según [`../08-Calidad-Y-Pruebas/Plan-Pruebas.md`](../08-Calidad-Y-Pruebas/Plan-Pruebas.md) §1. Las etapas `b` y `g` no producen rama de trabajo acá, y su ausencia está declarada allá.

## 5. Canales

**No hay canales, y el motivo no es una omisión de esta categoría.** El intake §17.1.P.7 declara que esta biblioteca **no se publica en ningún feed** y que se compila dentro de `GeometriaFactory.sln`; el intake §13 lo generaliza al producto entero. Sin feed no hay canal `preview` ni canal `stable` a los que promover: serían dos nombres sin destino.

`Rules-Devops.md` §2.2 fija para el tipo `library` un modelo de canales `preview` / `stable` sobre feed único y admite quitar ambientes «con un ADR que lo justifique». **Ese ADR existe y es anterior a esta categoría**: [`ADR-03`](../05-Arquitectura-Tecnica/Adrs/ADR-03-Versionado-Y-Estabilidad-De-La-Superficie.md), que evaluó la publicación en un repositorio de paquetes interno como alternativa y la descartó porque el intake la descarta explícitamente y porque agregaría infraestructura a un producto que las fuentes declaran básico. El apartamiento queda desarrollado en [`Entornos-Deploy.md`](Entornos-Deploy.md) §1.

**Sufijos de versión de anticipo.** El formato admite `-alpha`, `-beta` y `-rc`, pero **este proyecto de código no los usa**, porque no hay canal donde publicar un anticipo ni integrador que lo consuma. La versión que la herramienta calcula entre etiquetas es de trabajo y no se entrega a nadie.

## 6. Política de obsolescencia y de cambios incompatibles

**No hay política de plazos de obsolescencia, y declararlo es la respuesta correcta.** Una política de obsolescencia existe para dar tiempo de migración a integradores que no controlás. Acá los **dos** consumidores son proyectos de código del mismo producto, se compilan en el mismo artefacto de agrupación y en la misma ejecución del pipeline: un cambio incompatible **rompe su compilación en el acto**, que es el aviso más temprano y más barato que puede existir. Prometer «dos versiones menores antes de remover» sería una promesa hecha a nadie.

Lo que sí hay, y es obligatorio:

| Obligación | Cómo se verifica | Fundamento |
| --- | --- | --- |
| Todo cambio mayor recibe su **fila en el registro de cambios del producto**, `changelog.md` | Revisión del pull request de la etapa. Objetivo: **0** cambios mayores sin fila | `ADR-03` §7 y §8 |
| Un cambio mayor exige que los **nueve** invariantes se verifiquen por prueba antes de fusionar | `QG-06`, con `TC-26` | `ADR-03` §8, cuarta métrica |
| Un elemento que se va a quitar se marca como obsoleto en la superficie antes de removerse, dentro de la misma etapa o de la siguiente | Revisión del pull request | Decisión de esta categoría: es lo único que la ausencia de plazos deja sin cubrir, y no cuesta nada en un producto de dos consumidores compilados juntos |
| Toda etapa cerrada lleva su etiqueta | Inspección de etiquetas contra la lista de etapas cerradas. Objetivo: **100 %** | `ADR-03` §8, segunda métrica |

**La reversión se apoya en la etiqueta y no en el retiro de una versión publicada**: ver [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §7.

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Adopta el versionado semántico 2.0.0 y las Conventional Commits 1.0.0 que el intake §17.1.P.7 declara sin excepciones, y transcribe el criterio de cambio mayor, menor y parche de `ADR-03` §7 **sin agregarle ni quitarle nada**, incluida la fila que declara que perder un invariante es cambio mayor sin cambio de firma. Declara la herramienta de cálculo **por su función**, porque el intake la ancla en la etapa `a` y la ADR acepta explícitamente no nombrarla. Declara el modelo de ramas del producto —una rama y un pull request por etapa, etiqueta al fusionar, etapas en serie— y la ausencia de canales con el ADR que la sostiene y con el apartamiento declarado frente a `Rules-Devops.md` §2.2. Declara que no hay política de plazos de obsolescencia, con el fundamento de que los dos consumidores compilan juntos, y las **cuatro** obligaciones que sí rigen. |
