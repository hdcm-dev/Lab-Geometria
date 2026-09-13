# ADR-00010 — Versión en la ruta, sólo el MAJOR, para la superficie pública

**Unidad de entrega:** GeometriaFactory-Api
**Documento:** ADR-00010-Version-En-La-Ruta-Solo-Major-Para-La-Superficie-Publica.md
**Versión:** 1.0
**Estado:** Aceptado
**Fecha:** 2026-09-12
**Autor:** Arquitecto de Software Senior + API Designer (AG-05), sobre decisión del Product Owner del 2026-09-12 (`E-04`)
**Categoría:** Comunicación
**Supera parcialmente a:** [`ADR-00008`](ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) — la regla 1 queda derogada; las reglas 2 a 5 subsisten (§2.3)

---

## 1. Contexto

[`ADR-00008`](ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) decidió **no versionar las rutas** sobre una premisa que el intake declaraba sin ambigüedad hasta su versión 4.2: **no había clientes de terceros**. El único consumidor era `GeometriaFactory-Web`, servidor a servidor, compilado contra el mismo ensamblado de tipos de transferencia, de modo que un cambio incompatible rompía la compilación antes de romper el tiempo de ejecución. La ausencia de versionado tenía sustituto —el despliegue conjunto— y no era un pendiente.

**La premisa dejó de ser cierta el 2026-09-12, y por dos decisiones distintas del Product Owner.** La primera es `E-02`: «la API debe ser expuesta públicamente; la idea es ofrecerla para otros clientes» (`PRODUCT-INTAKE` **4.3** §17.1.P.3, fila «Quién la consume»; `Audit/Mesa-2026-09-12-ciclo-2.md` §8). La segunda es la que dice **quiénes son esos clientes**, y la registra [`ADR-00009`](ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md): no son terceros —el Product Owner descartó modelarlos: «no hay tercero»— sino **aplicaciones propias del producto además del front**: otro front, una aplicación MAUI, un script del docente, cada una actuando en nombre de una persona con uno de los dos papeles. El intake lo asienta en §17.1.P.3 · GeometriaFactory-Contracts: la afirmación «no hay clientes de terceros», «cierta hasta la versión 4.2, **deja de serlo**».

**Lo que cambia no es que aparezca un extraño: es que aparece un consumidor que no compila contra el ensamblado.** La red de `ADR-00008` era la compilación compartida, y esa red sólo cubre a las dos piezas que la comparten. Una aplicación MAUI o un script del docente alcanzan la superficie **por su ruta y por su forma en el cable**, y ninguna compilación les avisa cuando eso cambia. Para ellos hace falta lo que `ADR-00008` descartó por no tener destinatario: **una versión visible en la ruta, y un plazo de convivencia cuando cambia.**

**La convención no la elige esta ADR: la eligió el Product Owner sobre el estándar de la industria.** `E-04` fue un mandato —«la mesa debería buscar una forma de nomenclar el versionado, buscar cómo se hace en el estándar en la industria»— y la mesa de ciclo 2 relevó las cuatro prácticas con sus fuentes (`Audit/Mesa-2026-09-12-ciclo-2.md` §3.2) y propuso una (§3.3), que el Product Owner aprobó con los defaults `D-02` y `D-03` confirmados (§8, `E-04`). El intake la asienta como decisión en la fila «Versionado del contrato» de §17.1.P.3 · GeometriaFactory-Api, y deja escrito **quién reescribe la superficie de esa decisión**: `ADR-00008` —esta ADR— y `Estrategia-Versionado.md` §1.1 (`BT-00033`, `BT-00035`).

Motivación upstream: `PRODUCT-INTAKE` **4.4** §17.1.P.3 · GeometriaFactory-Api (filas «Quién la consume» y «Versionado del contrato») y §17.1.P.3 · GeometriaFactory-Contracts; `Audit/Mesa-2026-09-12-ciclo-2.md` §2.7, §3.2, §3.3, §4 ítem 1 y §8 (`E-04`, `D-02`, `D-03`); [`ADR-00009`](ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md); `BT-00027`.

## 2. Decisión

**La superficie pública de esta unidad lleva la versión en la ruta, sólo el `MAJOR`, con la forma `/v{MAJOR}/`, y ese `MAJOR` es el mismo del producto.** Tres bloques: qué se adopta, qué se conserva de `ADR-00008` y qué queda derogado.

### 2.1 Lo que se adopta

1. **Todo punto de acceso del contrato REST se publica bajo el prefijo `/v{MAJOR}/`.** Los puntos son los que [`../Contratos-REST.md`](../Contratos-REST.md) §3 enumera; la ruta pública de cada uno es su ruta actual con el prefijo antepuesto, y **nada más cambia** en el punto: ni verbo, ni cuerpo, ni papel exigido, ni lado de la guardia. El primer prefijo publicado es `/v1/`, y lo implementa `BT-00032` con la batería de integración en verde contra las rutas versionadas. **Esta ADR no cambia ninguna ruta**: fija la convención y su fundamento.
2. **`MAJOR` es una sola cifra y es la del producto.** No hay un contador de versión del contrato separado del SemVer 2.0.0 del producto: cuando el producto sube de `MAJOR`, la superficie estrena `/v{MAJOR+1}/`; y a la inversa, un cambio incompatible del contrato REST —los que [`../Contratos-REST.md`](../Contratos-REST.md) §6 clasifica como **Mayor**— es un cambio mayor del producto. Lo eligió la mesa por ser «extensión mínima de lo ya adoptado; operable sin herramienta extra por un equipo de un docente» (§3.3). Una consecuencia que conviene dejar dicha, porque sale de la aritmética y no de una decisión nueva: la última etiqueta del repositorio es `v0.8.0`, y exponer la superficie bajo `/v1/` supone que el producto sale de `0.x` —SemVer 2.0.0 §5: «la versión 1.0.0 define la API pública»—. La etiqueta la calcula la herramienta que `BT-00033` adopta (`D-03`, MinVer); esta ADR no la asigna.
3. **Sólo el `MAJOR` va en la ruta.** `MINOR` y `PATCH` no se ven en la superficie: un cambio menor —agregar un punto, agregar un código al conjunto cerrado— convive dentro del mismo `/v{MAJOR}/` sin que ningún cliente tenga que moverse. Es lo que hace que el versionado no «invite a versionar el recurso entero», que es el contra que la mesa registró para esta práctica (§3.2).
4. **Cuando se publica `/v{N+1}/`, `/v{N}/` se mantiene un plazo mínimo de un cuatrimestre** (`D-02`, default aceptado), con la cabecera `Deprecation` de `draft-ietf-httpapi-deprecation-header`. **El plazo y el mecanismo de aviso los declara `Estrategia-Versionado.md`, no esta ADR** (`BT-00035`, dependiente de `BT-00032`); acá queda fijado sólo que existe un plazo y que es el que el intake asienta. Convivir dos `MAJOR` a la vez es la única convivencia admitida: dentro de un mismo `/v{MAJOR}/` sigue viviendo **una** forma de cada punto.
5. **La versión se decide en la ruta y en ningún otro lugar.** No hay cabecera de versión, no hay media type con versión, no hay fecha de versión por cliente: §4 dice por qué se descartó cada una. Una petición sin prefijo de versión a un punto del contrato **no es una petición al contrato**; qué responde el servicio en ese caso lo fija `BT-00032` con la batería.

### 2.2 Lo que se conserva de `ADR-00008`

`ADR-00008` §2 tenía cinco reglas y una ausencia declarada. **Cuatro reglas y la ausencia siguen vigentes**, y esta ADR las adopta en lugar de reescribirlas:

- **Regla 2 — despliegue conjunto ante cambio de contrato.** Sigue rigiendo **entre las piezas propias que compilan contra el ensamblado de contratos**: `GeometriaFactory-Api` y `GeometriaFactory-Web`. La compilación compartida no dejó de ser la red de esas dos; lo que esta ADR agrega es la red **para las que no la comparten**. Una aplicación propia que no compile contra el ensamblado no obliga a desplegar nada en conjunto: la protege el prefijo y el plazo del punto 4.
- **Regla 3 — las tres clases de cambio que la compilación no detecta**, cada una con su mecanismo: la configuración de intercambio ([`ADR-00002`](ADR-00002-Formato-De-Intercambio-Y-Su-Configuracion.md)), el esquema del almacén (arranque en dos fases, [`ADR-00007`](ADR-00007-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md)) y las rutas, que la batería de integración ejerce contra el servicio real. **Las rutas ahora llevan prefijo, y la batería las ejerce con él** (`BT-00032`); la clase no cambia, cambia lo que la batería tiene que comprobar.
- **Regla 4 — la etiqueta por fusión.** Subsiste que toda fusión que cierra algo recibe una etiqueta SemVer y que la reversión es volver a la etiqueta anterior y reconstruir. **Lo que cambia es el evento**: deja de ser «cada etapa cerrada y fusionada» y pasa a ser **«toda fusión a `main` que cambie código de producción»**, calculada por Conventional Commits (`PRODUCT-INTAKE` **4.4** §17.1.P.3, fila «Versionado del contrato»; mesa §3.3). **El evento y su herramienta los fija `BT-00033` sobre `Estrategia-Versionado.md`**, y esta ADR remite a ellos en lugar de decidirlos.
- **Regla 5 — la colección de peticiones reproducible es parte del contrato hacia afuera**, se reproduce en cinco pasos o menos y no inventa datos de prueba. Cuando la superficie cambia de prefijo, la colección cambia con ella (`BT-00032`), y el sample de onboarding de `BT-00034` la ejerce contra `/v1/`.
- **La ausencia declarada — la pasarela de reenvío del front no se implementa.** `X-9` se reevaluó en el intake **4.4** y **sigue sin darse**: la exposición es hacia aplicaciones propias, no una habilitación del navegador del propio front. Su condición de reingreso es la que el intake declara.

### 2.3 Lo que queda derogado

- **Regla 1 de `ADR-00008` — «una sola versión de la superficie vive a la vez; no hay prefijo de versión en las rutas, no hay convivencia de dos formas de un punto y no hay deprecación gradual: no hay a quién dársela».** Derogada **para la superficie pública**, que es toda la superficie del contrato REST: hay prefijo, puede haber dos `MAJOR` conviviendo durante el plazo de deprecación, y **hay a quién darle plazo** —las aplicaciones propias que no compilan contra el ensamblado—. De la regla sobrevive una parte, y §2.1 punto 4 la conserva: **dentro de un `/v{MAJOR}/` vive una sola forma de cada punto**.
- **Las convenciones impuestas de `ADR-00008` §7 que aplicaban la regla 1**: «ninguna ruta lleva prefijo ni sufijo de versión, y ningún punto de acceso convive con una forma anterior de sí mismo». Derogadas en su primera mitad; la segunda se relee como en el punto anterior.
- **Las dos primeras métricas de `ADR-00008` §8** —«rutas con prefijo o sufijo de versión: exactamente 0» y «formas conviviendo de un mismo punto de acceso: exactamente 0»—. Las reemplazan las de §8 de esta ADR. Las otras cuatro métricas de `ADR-00008` §8 subsisten con la lectura de §2.2.
- **La premisa de `ADR-00008` §1, «no hay clientes de terceros», como fundamento de cualquier otra decisión.** Donde el corpus la cite como cierta, se lee con esta ADR: no hay terceros porque el producto no los modela ([`ADR-00009`](ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md)), pero **sí hay clientes que no compilan contra el ensamblado**, y esa es la propiedad que decide el versionado. `BT-00027` recorre el conjunto medido de esos lugares.

**Lo que no cambia:** la guardia de [`ADR-00003`](ADR-00003-Credencial-Firmada-Papel-Por-Punto-Y-Guardia-Transversal.md) y sus cuatro puntos fuera; el formato de intercambio de [`ADR-00002`](ADR-00002-Formato-De-Intercambio-Y-Su-Configuracion.md); la política de nivel Producto de [`ADR-08003`](../../../../Producto/Adrs/ADR-08003-Versionado-Por-Compilacion-Compartida.md) para el tramo `Web`↔`Api` —su premisa «no hay clientes de terceros» queda alcanzada por esta ADR y la reescritura de esa ADR es consecuencia obligada de nivel Producto, declarada en `BT-00027` §2 por el criterio 6 de la DoR—.

## 3. Estado

**Aceptado** desde 2026-09-12, por decisión del Product Owner del mismo día: `E-04` respondida en `Audit/Mesa-2026-09-12-ciclo-2.md` §8 —«lo que la mesa propuso (§3-§4 de este informe) y el Product Owner aprobó, con los tres defaults de §6 confirmados»— y asentada en `PRODUCT-INTAKE` **4.3** §17.1.P.3, fila «Versionado del contrato». No pasa por `Propuesto`: la convención es decisión de producto tomada sobre una propuesta de la mesa con fuentes, y esta ADR la registra con su fundamento técnico y con lo que deroga y conserva de `ADR-00008`. Sigue la forma de [`ADR-00009`](ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md) §3.

## 4. Alternativas consideradas

Las cuatro prácticas son las que la mesa relevó con fuente (`Audit/Mesa-2026-09-12-ciclo-2.md` §3.2); la quinta es conservar `ADR-00008`.

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Versión en la ruta, sólo el `MAJOR`, compartido con el producto (**adoptada**) | Visible en la URL, en los registros y en el navegador; cacheable; convivir dos versiones es trivial; es la convención dominante en REST público y la que Microsoft REST API Guidelines §12 recomienda para servicios públicos (`https://github.com/microsoft/api-guidelines/blob/vNext/azure/Guidelines.md#12-versioning`); no exige herramienta extra ni un contador aparte | Invita a versionar el recurso entero (mesa §3.2); se mitiga llevando sólo el `MAJOR` a la ruta, con lo que los cambios menores no mueven a nadie. Duplica, durante el plazo de deprecación, la superficie que la guardia protege —el mismo contra que `ADR-00008` §4 dio para descartarla—, y por eso el plazo es acotado y el prefijo no toca la guardia |
| Cabecera de versión (`Accept-Version`) | Ruta limpia | **Descartada.** Invisible en los registros y en el navegador (mesa §3.2), y suele combinarse con la ruta, con lo que no la reemplaza. Es además la clase de cosa que `ADR-00008` §4 ya señaló: una elección de versión que pasa a ser configuración del cliente y **no rompe ninguna compilación** |
| Media type con versión (`application/vnd.x.v1+json`) | «Más REST» | **Descartada.** Compleja de implementar y de documentar (mesa §3.2); GitHub la usa como mecanismo secundario, no principal. Choca con [`ADR-00002`](ADR-00002-Formato-De-Intercambio-Y-Su-Configuracion.md), que fija un solo formato de intercambio para los dos extremos: meter la versión en el tipo de contenido convierte esa regla en una por versión |
| Fecha de versión por cliente, estilo `Stripe-Version: 2024-06-20` (`https://stripe.com/blog/api-versioning`) | Cambios incrementales por cliente | **Descartada.** Exige una capa de traducción interna por fecha (mesa §3.2), que es infraestructura para un producto que las fuentes declaran básico y que opera un docente. Resuelve un problema —muchos clientes externos en fechas distintas— que [`ADR-00009`](ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md) dice que este producto no tiene |
| Conservar `ADR-00008` sin cambios | Nada que construir | **Descartada por el Product Owner** (`E-02`, `E-04`). La premisa cayó: hay aplicaciones propias que no compilan contra el ensamblado, y para ellas el despliegue conjunto no es una red |

## 5. Consecuencias positivas

1. **La superficie tiene una red para el consumidor que no compila contra el ensamblado**, que es exactamente el que `ADR-00008` no contemplaba. La compilación compartida sigue cubriendo a las dos piezas que la comparten.
2. **Un solo contador de versión en todo el producto.** No hay que explicar a nadie la diferencia entre «versión del producto» y «versión del contrato»: es la misma cifra.
3. **La convención tiene fuente y precedente**: es la práctica dominante y la que recomienda la guía de Microsoft para servicios públicos, con lo que un cliente propio nuevo no tiene que aprender nada particular de este producto.
4. **Las cinco tareas que siguen tienen su fundamento escrito**: `BT-00032` (rutas bajo `/v1/`), `BT-00033` (evento de etiqueta y MinVer), `BT-00034` (sample contra `/v1/`), `BT-00035` (deprecación) y `BT-00031` (documentación publicada) remiten a esta ADR y no a una premisa derogada.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta que durante un plazo convivan dos `MAJOR` de la superficie**, con la guardia protegiendo las dos. Es el contra que `ADR-00008` dio para descartar el prefijo, y se acepta acotado: un cuatrimestre, y una sola forma de cada punto dentro de cada `MAJOR`.
2. **Se acepta que un cambio mayor del contrato REST sea un cambio mayor del producto**, aunque el front —que compila contra el ensamblado— no lo necesite. Es el precio de compartir la cifra.
3. **Se acepta que exponer bajo `/v1/` saque al producto de `0.x`.** La aritmética de SemVer no deja alternativa una vez decidido que `MAJOR` se comparte; la etiqueta concreta la calcula `BT-00033`.
4. **Se acepta que la pieza propia que no compila contra el ensamblado se entere de un cambio menor por la documentación y no por el compilador.** La descripción OpenAPI generada ([`ADR-08008`](../../../../Producto/Adrs/ADR-08008-La-Superficie-HTTP-Se-Describe-Y-El-Explorador-No-Se-Publica-Solo.md)) y el sample de `BT-00034` son lo que la avisa.

## 7. Implementación

- **Convención impuesta:** todo punto de acceso del contrato REST se publica bajo `/v{MAJOR}/`, y `MAJOR` es el del producto. Ningún punto del contrato se publica sin prefijo. La lleva a código `BT-00032`; esta ADR no cambia rutas.
- **Convención impuesta:** dentro de un mismo `/v{MAJOR}/` vive **una** forma de cada punto. Convivir es entre `MAJOR` distintos, y sólo durante el plazo de deprecación.
- **Convención impuesta:** la versión no se negocia por cabecera, media type ni fecha. La ruta es el único lugar.
- **Convención impuesta, heredada de `ADR-00008`:** todo cambio del ensamblado de contratos entra con el despliegue de `Api` y `Web` en la misma fusión; la colección de peticiones se actualiza en la misma intervención en que cambia la superficie, sin inventar datos de prueba.
- **Remitido, no decidido acá:** el evento de etiqueta y la herramienta (`BT-00033`, `Estrategia-Versionado.md`); el plazo de convivencia y la cabecera `Deprecation` (`BT-00035`, `Estrategia-Versionado.md`); qué responde una petición sin prefijo, y si el punto de salud y la descripción generada quedan bajo el prefijo (`BT-00032`, con la batería en verde).
- **Consecuencia de nivel Producto, declarada y no ejecutada acá:** [`ADR-08003`](../../../../Producto/Adrs/ADR-08003-Versionado-Por-Compilacion-Compartida.md) sostiene la misma premisa derogada y obliga a `GeometriaFactory-Contracts`; su reescritura es de la categoría de Producto (`BT-00027` §2).

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Puntos del contrato REST publicados sin prefijo `/v{MAJOR}/` | Exactamente **0**, desde el cierre de `BT-00032` | Comparación en las dos direcciones entre las rutas publicadas y [`../Contratos-REST.md`](../Contratos-REST.md) §3 |
| Prefijos de versión distintos conviviendo | **1**, o **2** sólo durante el plazo de deprecación | Inspección de las rutas publicadas y del registro de cambios |
| Formas conviviendo de un mismo punto **dentro de un `/v{MAJOR}/`** | Exactamente **0** | Inspección de la superficie |
| Mecanismos de versión distintos de la ruta (cabecera, media type, fecha) | Exactamente **0** | `git grep -n -i -e "Accept-Version" -e "Stripe-Version" -e "vnd\." -- src` sin resultado en código de producción |
| Cifras de `MAJOR` distintas entre la ruta publicada y la etiqueta del producto | Exactamente **0** | Comparación de `/v{MAJOR}/` contra la versión que informa `/salud` (`BT-00033`) |
| Cambios del ensamblado de contratos desplegados sin la pieza pública | Exactamente **0**, sin cambio respecto de `ADR-00008` | Revisión de cada fusión que toque el ensamblado |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **4.4** §17.1.P.3 · GeometriaFactory-Api (filas «Quién la consume» y «Versionado del contrato») y §17.1.P.3 · GeometriaFactory-Contracts.
- [`../../../../Audit/Mesa-2026-09-12-ciclo-2.md`](../../../../Audit/Mesa-2026-09-12-ciclo-2.md) §2.7 (la premisa contradicha), §3.2 (las cuatro prácticas con fuente), §3.3 (la convención propuesta), §4 ítem 1 y §8 (`E-04` respondida; `D-02` y `D-03` confirmados).
- [`ADR-00008`](ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md), superada parcialmente por ésta: derogada su regla 1, vigentes sus reglas 2 a 5 y su ausencia declarada.
- [`ADR-00009`](ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md): quiénes son los clientes —aplicaciones propias que actúan por una persona— y por qué no hay terceros.
- [`ADR-08003`](../../../../Producto/Adrs/ADR-08003-Versionado-Por-Compilacion-Compartida.md), nivel Producto, misma premisa; su reescritura es consecuencia declarada.
- [`../Contratos-REST.md`](../Contratos-REST.md) §3 (los puntos que llevan el prefijo) y §6 (clases de cambio).
- [`../../09-Devops/Estrategia-Versionado.md`](../../09-Devops/Estrategia-Versionado.md) §1.1 y §6.1, donde `BT-00033` y `BT-00035` escriben el evento de etiqueta y la deprecación.
- [`BT-00027`](../../06-Backlog-Tecnico/tareas-tecnicas/BT-00027-Reescribir-Adr-00008-Adoptando-V-Major-Para-La-Superficie-Publica.md), [`BT-00032`](../../06-Backlog-Tecnico/tareas-tecnicas/BT-00032-Versionar-Las-Rutas-Publicas-Bajo-V1.md), [`BT-00033`](../../06-Backlog-Tecnico/tareas-tecnicas/BT-00033-Adoptar-Minver-Y-Etiquetar-Los-Commits-De-Produccion.md), [`BT-00035`](../../06-Backlog-Tecnico/tareas-tecnicas/BT-00035-Politica-De-Deprecacion-En-Estrategia-Versionado.md).
- SemVer 2.0.0 (`https://semver.org/spec/v2.0.0.html`) §5; Microsoft REST API Guidelines §12; Stripe, «APIs as infrastructure: future-proofing Stripe with versioning» (`https://stripe.com/blog/api-versioning`); `draft-ietf-httpapi-deprecation-header`.
- ADR relacionadas: [`ADR-00002`](ADR-00002-Formato-De-Intercambio-Y-Su-Configuracion.md), [`ADR-00003`](ADR-00003-Credencial-Firmada-Papel-Por-Punto-Y-Guardia-Transversal.md), [`ADR-00007`](ADR-00007-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md).

## 10. Evidencia de las citas y vocabulario

Toda cita se verificó con un comando sobre la base `cfb11f7` (`main`) el 2026-09-12:

```
$ ls SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/05-Arquitectura-Tecnica/Adrs/ | grep "ADR-0000"
ADR-00001-… a ADR-00009-… (nueve archivos; 00010 es el siguiente libre)

$ grep -n "^| Versionado del contrato" SDD/Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md | cut -c1-120
794:| Versionado del contrato | [DECISIÓN 2026-09-12, PO + mesa `Mesa-2026-09-12-ciclo-2.md` §3-§4, resolviendo `E-04`]

$ grep -n "deja de serlo" SDD/Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md | cut -c1-60
798:Es **el** contrato de comunicación del producto: define
1890:**Los tres fundamentos, que son la razón por la que se

$ grep -n "^### 3.3\|^### E-04" SDD/Docs/Audit/Mesa-2026-09-12-ciclo-2.md
104:### 3.3 Convención propuesta
202:### E-04 · Nomenclatura del versionado — RESPONDIDA

$ git tag -l | tail -1
v0.8.0
```

**Vocabulario.** Esta ADR no acuña términos. **«Superficie pública»** es el que usan la ficha `BT-00027`, el backlog y el intake para nombrar **el contrato REST de esta unidad expuesto públicamente** —el mismo sentido de la §17 «Compatibilidad de la superficie pública» de los casos de uso de esta unidad—; el árbol lo usa además, en las ADR de nivel Producto (`ADR-08001` a `ADR-08005`), para los **miembros públicos de un tipo del ensamblado**, y esta ADR no emplea ese segundo sentido. **«Cliente propio» / «aplicación propia»** los define [`ADR-00009`](ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md) §2 punto 2 y acá se usan con ese sentido. Medido:

```
$ git grep -c -i "superficie pública" -- SDD ':!*/_legacy/*' | wc -l
102
(102 archivos del árbol vivo lo usan; los dos sentidos están declarados arriba)

$ git grep -n -i "cliente propio\|aplicación propia" -- SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/05-Arquitectura-Tecnica ':!*/_legacy/*' | cut -d: -f1 | sort -u
SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/05-Arquitectura-Tecnica/Adrs/ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md
SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/05-Arquitectura-Tecnica/Decisiones-Arquitectura.md
```

## 11. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-09-12 | Emisión inicial, **Aceptado** por decisión del Product Owner del 2026-09-12 (`E-04`, `Audit/Mesa-2026-09-12-ciclo-2.md` §8; `PRODUCT-INTAKE` **4.3** §17.1.P.3, fila «Versionado del contrato»). Registra el cambio de premisa respecto de `ADR-00008` —dejó de ser cierto «no hay clientes de terceros»: hay aplicaciones propias además del front, `ADR-00009`, y no compilan contra el ensamblado—, adopta `/v{MAJOR}/` para la superficie pública con el `MAJOR` del producto, deroga la regla 1 de `ADR-00008` y sus dos primeras métricas, conserva sus reglas 2 a 5 y su ausencia declarada, y remite el evento de etiqueta a `BT-00033` y la deprecación a `BT-00035`. Evalúa cinco alternativas con las fuentes de la mesa §3.2, declara cuatro trade-offs, fija seis métricas y deja en §10 los comandos de verificación y el vocabulario medido. Tarea `BT-00027`. |
