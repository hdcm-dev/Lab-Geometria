# Pipeline CI/CD — GeometriaFactory-Contracts

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** Pipeline-CI-CD.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Ingeniero DevOps Senior + Release Engineer (AG-09)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) 1.1 §3 (los **nueve** quality gates); [`../08-Calidad-Y-Pruebas/Criterios-Validacion.md`](../08-Calidad-Y-Pruebas/Criterios-Validacion.md) 1.1; [`../08-Calidad-Y-Pruebas/Definition-Of-Done.md`](../08-Calidad-Y-Pruebas/Definition-Of-Done.md) 1.1 §1.3; [`../08-Calidad-Y-Pruebas/Plan-Pruebas.md`](../08-Calidad-Y-Pruebas/Plan-Pruebas.md) 1.0 §3; [`../08-Calidad-Y-Pruebas/Estrategia-Testing.md`](../08-Calidad-Y-Pruebas/Estrategia-Testing.md) 1.1 §3 y §7; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.1 §5 y §8; [`../05-Arquitectura-Tecnica/Adrs/ADR-03-Versionado-Por-Compilacion-Compartida.md`](../05-Arquitectura-Tecnica/Adrs/ADR-03-Versionado-Por-Compilacion-Compartida.md) 1.0; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.22** §10, §13, §15, §16, §17.4.P.3 a §17.4.P.10, §17.6.P.7, §17.6.P.8 y §22
**Trazabilidad downstream:** [`Estrategia-Versionado.md`](Estrategia-Versionado.md), [`Entornos-Deploy.md`](Entornos-Deploy.md), [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md); `Producto/Pipeline-Producto.md`, que orquesta el orden de construcción y **no duplica** este pipeline

---

## Tabla de contenido

- [1. Alcance y qué no es este pipeline](#1-alcance-y-qué-no-es-este-pipeline)
- [2. Stages](#2-stages)
  - [2.1 Tabla de stages y gates](#21-tabla-de-stages-y-gates)
  - [2.2 Dónde corre el gate que este proyecto de código no puede correr solo](#22-dónde-corre-el-gate-que-este-proyecto-de-código-no-puede-correr-solo)
  - [2.3 Los stages del catálogo que no existen acá](#23-los-stages-del-catálogo-que-no-existen-acá)
- [3. Triggers](#3-triggers)
- [4. Matriz de sistema operativo y plataforma](#4-matriz-de-sistema-operativo-y-plataforma)
- [5. Caché y artefactos](#5-caché-y-artefactos)
- [6. Promoción, y la regla de despliegue conjunto](#6-promoción-y-la-regla-de-despliegue-conjunto)
- [7. Reversión](#7-reversión)
- [8. Notificaciones](#8-notificaciones)
- [9. Qué aporta a la canalización de las dos unidades desplegables](#9-qué-aporta-a-la-canalización-de-las-dos-unidades-desplegables)
- [10. Puntos abiertos](#10-puntos-abiertos)
- [11. Control de cambios](#11-control-de-cambios)

---

## 1. Alcance y qué no es este pipeline

`GeometriaFactory-Contracts` es una **biblioteca de tipos de transferencia, no un servicio desplegable**. `05` §5 lo declara sin unidad de despliegue propia: **se carga en los dos procesos**, el del hosting público y el del servidor propio. `redistribuible` es false y el intake §13 declara que ningún proyecto de código del producto se publica como paquete redistribuible.

Hay además una particularidad que ordena todo este documento y conviene decirla primero: **este proyecto de código no tiene etapa de pruebas propias**. El intake §17.4.P.6 declara que no tiene pruebas propias porque son tipos sin comportamiento, y que se ejercitan íntegramente desde la batería de integración que golpea el servicio real; `05` §5 lo traduce a pipeline: **`restore` → `build`, sin `test`**, y lo declara correcto en lugar de omitirlo.

La consecuencia es que **la mitad de sus gates no se ejecutan corriendo algo, sino leyendo la superficie pública**. [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §4 lo dice con precisión: cinco de los nueve gates se comprueban leyendo la superficie y no corriendo nada, porque no hay nada que correr, y están escritos como recuentos para que no dependan de que alguien se acuerde. Este pipeline los materializa con esa forma.

## 2. Stages

### 2.1 Tabla de stages y gates

| Stage | Qué ejecuta | Gate que verifica | Umbral | Carácter |
| --- | --- | --- | --- | --- |
| `restore` | Restauración de dependencias de la plataforma para el artefacto de agrupación | Ninguno propio | — | Bloqueante por construcción |
| `build` | `scripts/build.sh` | `QG-01`: el ensamblado compila **sin advertencias** —y el gate es «sin advertencias», no «sin errores» | 0 advertencias | **Bloqueante** |
| `build` | Inspección del archivo de proyecto | `QG-02`: **0** referencias hacia `GeometriaFactory-Domain` | 0 | **Bloqueante**, y se rechaza en revisión |
| Inspección de superficie | `TC-15`, y `TC-01`, `TC-04` y `TC-19` sobre las familias que la etapa toca | `QG-03`: **0** campos capaces de transportar el hash de la contraseña, la clave de firma, una dirección de servicio interno, una ruta de archivo de datos o una traza | 0 | **Se rechaza aunque compile** |
| Inspección de superficie | `TC-16` | `QG-04`: el conjunto cerrado tiene exactamente **15** códigos vivos y se producen **0** fuera de él | 15 y 0 | **Se rechaza aunque compile** |
| Batería de integración | `TC-21`, sobre la matriz de tipo contra prueba | `QG-05`: **100 %** de los tipos de transferencia ejercitados por al menos una prueba de integración | 100 % | **Bloqueante.** Ver §2.2 |
| Inspección de superficie | `TC-09` | `QG-06`: la proyección de listado no lleva texto original, ni componentes de pieza, ni comentario | 0, 0 y 0 **[ASUNCIÓN derivada del intake §17.4.P.10]** | **Condicionado**: se verifica y se registra; no bloquea |
| Inspección de superficie | `TC-01` y `TC-02` | `QG-07`: la respuesta de sesión declara exactamente **4** campos y **0** que transporten una condición que impida operar | 4 y 0 | **Se rechaza aunque compile** |
| Revisión del pull request | Lectura del cambio contra la restricción transversal de despliegue conjunto | `QG-08`: ante un cambio incompatible, **las dos unidades desplegables se despliegan juntas** | 100 % | **Bloquea la publicación de la etapa.** Ver §6 |
| Inspección de superficie | `TC-18` y `TC-22` | `QG-09`: **ningún** tipo permite salir de un estado terminal y ninguno habilita a que el navegador invoque el servicio de datos | 0 y 0 | **Se rechaza aunque compile** |

**Por qué `QG-05` bloquea y `QG-06` no, que es la distinción que la Fase E fijó y esta categoría respeta sin reabrir.** Los dos llevan rótulo **[ASUNCIÓN]**, pero no sobre lo mismo. El de `QG-05` viene del intake §17.4.P.6, que lo llama «el gate equivalente y bloqueante», y la fila `A-4` del intake §22 declara que un cambio del Product Owner «cambia la forma del gate, no su carácter bloqueante»: lo que está en duda es **cómo se expresa** la condición, no si detiene la fusión. El de `QG-06` viene de §17.4.P.10 y pone en duda **qué se verifica** —qué campos quedan fuera de la proyección de listado—, de modo que es el umbral mismo el que espera confirmación. **Esta categoría lo materializa exactamente así: `QG-05` bloquea desde la primera etapa que lo alcanza, `QG-06` se mide y se registra.**

**Las inspecciones de superficie no tienen stage de nombre propio, y es deliberado.** Son pruebas ejecutables —`03` §3 ya publica la comprobación reproducible de dos de ellas, para `DXC-01` y `DXC-09`— pero no pertenecen a un stage de pruebas de este proyecto de código, que no lo tiene. Corren **en cada pull request que agrega o cambia un campo**, que es la cadencia propia que `Estrategia-Calidad.md` §5 declara para este proyecto de código con el fundamento de que su defecto característico entra de a un campo y compila.

### 2.2 Dónde corre el gate que este proyecto de código no puede correr solo

`QG-05` exige el **100 %** de los tipos ejercitados por al menos una prueba de integración contra el servicio real. Esa batería **no es de este proyecto de código**: [`../08-Calidad-Y-Pruebas/Estrategia-Testing.md`](../08-Calidad-Y-Pruebas/Estrategia-Testing.md) §7 declara que la verificación efectiva corre en la batería de integración que golpea el servicio real, que pertenece a `GeometriaFactory-Api`, y que sus condiciones de ejecución **no se declaran acá**.

| Aspecto | Decisión de esta categoría | Fundamento |
| --- | --- | --- |
| Cómo se ejecuta | Con `scripts/test.sh`, el mismo guion del producto | `Estrategia-Testing.md` §3, primera fila |
| Cuándo corre para este proyecto de código | En el pull request de toda etapa que agregue o cambie un tipo de transferencia, **aunque este proyecto de código no tenga stage de `test` propio** | Sin eso, `QG-05` no tendría dónde medirse y un gate bloqueante quedaría sin ejecución |
| Mientras `GeometriaFactory-Api` no exista | La prueba de integración **se declara diferida por escrito**, con la etapa en que se ejecuta, y la **inspección de superficie correspondiente sí se ejecuta y no se difiere** | [`../08-Calidad-Y-Pruebas/Criterios-Validacion.md`](../08-Calidad-Y-Pruebas/Criterios-Validacion.md) §6, segunda fila; Definition of Done §2 |
| Quién aprueba el diferimiento | El Product Owner, en el punto de control, con constancia escrita | Los mismos dos documentos |

**Diferir no es relajar.** El diferimiento tiene tres condiciones que el pipeline hace cumplir: es por escrito, nombra la etapa en que se ejecuta, y **la inspección de superficie equivalente corre igual**. Un gate diferido sin las tres es un gate incumplido.

### 2.3 Los stages del catálogo que no existen acá

| Stage del catálogo | Estado acá | Motivo |
| --- | --- | --- |
| Lint | **Incorporado en `build`** | El criterio es «compila sin advertencias» (`QG-01`), y ninguna fuente declara un linter separado |
| Test | **No existe como stage propio, y está declarado** | Intake §17.4.P.6 y `05` §5: no tiene pruebas propias; su verificación efectiva vive en la batería de integración. Ver §2.2 |
| SCA | **Se reduce a una comprobación de ausencia** | El intake §17.4.P.1 declara este proyecto de código **sin dependencias**, y `CV-22` exige **0** bibliotecas de serialización declaradas. Ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §4 |
| SBOM | **No se genera acá** | No hay artefacto publicado del que emitir inventario; el que importa es el de las dos unidades que embeben este ensamblado |
| Firma | **No se firma acá** | No hay canal ni integrador externo: `redistribuible` es false |
| Publish | **No existe** | Intake §17.4.P.7: no se publica en ningún feed |

## 3. Triggers

| Evento | Qué corre | Qué bloquea |
| --- | --- | --- |
| Confirmación empujada a la rama de una etapa | `restore` → `build` | Nada por sí solo |
| Apertura o actualización del pull request de la etapa | `restore` → `build`, **las cinco inspecciones de superficie** y la revisión de `QG-08` | La fusión |
| Pull request que **agrega o cambia un campo, un tipo o un valor de conjunto cerrado** | Todo lo anterior, más la batería de integración de §2.2 o su diferimiento por escrito | La fusión |
| Fusión a la rama principal | `restore` → `build` sobre el estado fusionado | El cierre de la etapa |
| Etiqueta de cierre de etapa | Lo mismo, sobre el estado etiquetado | La declaración de etapa cerrada |

**No hay trigger por calendario**: el intake §10 declara «sin plazo; el avance se mide por etapas cerradas».

## 4. Matriz de sistema operativo y plataforma

| Trigger | Sistema operativo | Plataforma objetivo |
| --- | --- | --- |
| Todos los de §3 | El del contenedor de desarrollo, que es el mismo del servidor del backend | `net10.0`, sin sufijo de plataforma |

Justificación, del intake §17.4.P.9: `net10.0`, Linux, y **se carga en los dos procesos**, el del hosting y el del servidor propio. Una matriz cruzada no compraría cobertura: los dos procesos que lo cargan corren sobre el mismo sistema operativo, y no hay integrador externo con otro.

**Una precisión que este proyecto de código sí tiene y los otros dos de nivel topológico 0 no.** El intake §17.6.P.9 deja **[A VERIFICAR]** qué versión de la plataforma soporta el hosting del front, y declara que si no pasa la puerta correspondiente la salida es **bajar la versión objetivo del front, no la del backend**, porque son dos artefactos independientes. Como este ensamblado se carga en los dos procesos, **una bajada de versión del front lo alcanza**: su plataforma objetivo tiene que seguir siendo cargable por los dos. Queda registrado como `PD-02` en §10.

## 5. Caché y artefactos

| Aspecto | Decisión | Fundamento |
| --- | --- | --- |
| Caché de dependencias | Caché del restaurador de paquetes, con llave derivada de los archivos de proyecto | Decisión de esta categoría |
| Invalidación | Al cambiar cualquier archivo de proyecto. **Sin expiración por tiempo** | Un plazo en días no lo da ninguna fuente |
| Artefacto del stage `build` | El ensamblado compilado, consumido en la misma ejecución por `GeometriaFactory-Api` y por `GeometriaFactory-Web` | Intake §13, columna de dependencias |
| Artefacto de las inspecciones | El **recuento** de cada una: campos de filtración, códigos vivos, campos de la respuesta de sesión, carga útil del listado | `Estrategia-Calidad.md` §4: los gates de este proyecto de código están escritos como recuentos |
| Retención | Mientras dure el punto de control de la etapa; los recuentos se adjuntan al informe de cierre | Intake §15, regla de delivery 3 |

**No hay informe de cobertura de líneas, y su ausencia está declarada aguas arriba**: `CV-24` declara que la cobertura de líneas **no aplica como criterio** en este proyecto de código y que el gate equivalente es `CV-08`, el del 100 % de tipos ejercitados.

## 6. Promoción, y la regla de despliegue conjunto

| Transición | Trigger | Prerrequisitos | Aprobador |
| --- | --- | --- | --- |
| Rama de etapa → rama principal | Fusión del pull request | Los gates bloqueantes y los de rechazo en revisión de §2.1, y los **nueve** criterios de salida de [`../08-Calidad-Y-Pruebas/Plan-Pruebas.md`](../08-Calidad-Y-Pruebas/Plan-Pruebas.md) §3 | El Product Owner, con **OK explícito** en el punto de control (intake §15) |
| Etapa fusionada → etapa cerrada | Etiqueta al fusionar | La Definition of Done §1.3 entera | El mismo |
| Cambio incompatible del contrato → producto desplegado | Sólo con **las dos unidades desplegables desplegadas juntas** | `QG-08`, con `CV-18` y `CV-19` | El mismo |

**La regla de despliegue conjunto es la única obligación de este proyecto de código que alcanza a un acto de despliegue**, y no la inventa esta categoría: el intake §17.4.P.3 declara que Api y Web se despliegan juntos ante un cambio de contrato, y [`ADR-03`](../05-Arquitectura-Tecnica/Adrs/ADR-03-Versionado-Por-Compilacion-Compartida.md) §2 la declara explícitamente como «una entrada para la categoría 09». La entrada se recibe acá y se hace operativa así:

1. Todo pull request que cambie el contrato declara, en su propio texto, **si el cambio es incompatible**, según el criterio de `ADR-03` §7. Esa declaración es la que `CV-18` verifica.
2. Si lo es, el cierre de la etapa **no se declara** hasta que las dos unidades estén desplegadas desde el mismo estado del repositorio. Es lo que `QG-08` bloquea: la publicación de la etapa, no la fusión.
3. **Tres de las siete clases de cambio del criterio de `ADR-03` §7 no las detecta la compilación**, y las tres son mayores: quitar un valor de un conjunto cerrado, agregar un código al conjunto cerrado de error, y agregar un campo capaz de transportar una dirección de servicio, una ruta de datos o un secreto. El paso 1, entonces, **no puede delegarse en el compilador**. Es revisión, y por eso es gate.

**El hallazgo que esta categoría elevó, y cómo quedó.** Cuando se emitió la versión 1.0 de este documento, el intake §17.6.P.7 restringía el flujo de trabajo que publica el front a cambios bajo `src/GeometriaFactory.Web/` y `visor/`. Un cambio de este ensamblado vive en `src/GeometriaFactory.Contracts/` y, por esa restricción, **no disparaba la publicación del front**, aunque `QG-08` exija que las dos unidades salgan juntas: el despliegue conjunto quedaba apoyado en que alguien se acordara de dispararlo a mano, que es exactamente lo que un gate escrito como recuento viene a evitar. Se elevó como `PD-01` a la categoría 09 de `GeometriaFactory-Web`, dueña de ese flujo, y **el Product Owner lo resolvió en el intake 1.22**: §17.6.P.7 enumera hoy `src/GeometriaFactory.Web/`, `visor/` y **`src/GeometriaFactory.Contracts/`**, de modo que un cambio de este ensamblado dispara la publicación del front por fusión. `PD-01` de §10 queda **cerrado**.

**Lo que ese cierre no cambia, y conviene no sobrevender.** El filtro dispara una construcción; **no coordina dos despliegues**, y uno de los dos —el del servidor propio— es manual por decisión del intake §17.5.P.8. El paso 2 de arriba sigue siendo el mecanismo, y el despliegue conjunto sigue siendo **un acto humano coordinado**. Lo que el intake 1.22 agrega en la misma decisión es el **orden**: cuando las dos unidades salen juntas, **primero el backend**, porque una API nueva normalmente acepta lo que mandaba el front anterior. El orden **no vuelve automático** el despliegue conjunto: minimiza el intervalo, no lo elimina.

## 7. Reversión

| Situación | Procedimiento | Fundamento |
| --- | --- | --- |
| Una etapa fusionada rompe algo que estaba en verde | Volver a la **etiqueta de la etapa anterior** | Intake §17.1.P.7 y §15, modelo de ramas del producto |
| Un cambio incompatible llegó a las dos unidades y hay que volver atrás | **Se revierten las dos juntas**, por la misma regla que las obliga a desplegarse juntas. La reversión desacoplada reproduce `RI-02` de [`../../../Producto/Vista-Producto.md`](../../../Producto/Vista-Producto.md) §7: la lectura estricta rompe ante el extremo desactualizado | `ADR-03` §2 y §6; `Vista-Producto.md` §7 |
| El procedimiento operativo de cada unidad | **No se escribe acá**: pertenece a la categoría 09 de `GeometriaFactory-Api` y de `GeometriaFactory-Web` | `Rules-Devops.md` §0, frontera de responsabilidades |

**No hay retiro de versión publicada ni ventana de gracia**, porque no hay artefacto publicado que retirar.

## 8. Notificaciones

| Canal | Qué comunica |
| --- | --- |
| La salida del pipeline sobre el pull request de la etapa | El resultado de `build` y el **recuento** de cada inspección de superficie |
| El texto del propio pull request | La declaración de si el cambio es incompatible, que `CV-18` verifica |
| El informe de cierre de la etapa | La medición de `QG-06` con su distancia al umbral, los diferimientos de integración con la etapa en que se ejecutan, y la constancia del despliegue conjunto cuando hubo cambio incompatible |
| El registro de cambios del producto | Toda fila de cambio mayor de este contrato (`ADR-03` §7) |

**No se declara ningún canal de mensajería ni ningún tablero**: ninguna fuente lo declara y `equipo_n` es 1.

## 9. Qué aporta a la canalización de las dos unidades desplegables

| Aporte | Efecto sobre la canalización | Fundamento |
| --- | --- | --- |
| Es **nivel topológico 0** | Se construye antes que `GeometriaFactory-Api` y que `GeometriaFactory-Web`, que son los dos que lo referencian | Intake §13 |
| Es el **único** proyecto de código que las dos unidades desplegables comparten en compilación | Un cambio suyo obliga a reconstruir las dos, y si es incompatible, a desplegarlas juntas | Intake §13 y §17.4.P.3 |
| Su gate `QG-05` se mide en la batería de integración | La canalización de `GeometriaFactory-Api` es la que la ejecuta; este proyecto de código **depende** de que exista | §2.2 |
| Su gate `QG-08` bloquea la **publicación** de la etapa, no la fusión | Es el único gate de nivel 0 que alcanza al acto de despliegue | `Estrategia-Calidad.md` §3 |

## 10. Puntos abiertos

| Id | Punto abierto | Quién lo cierra | Cuándo |
| --- | --- | --- | --- |
| PD-01 | ~~La **restricción de rutas del flujo de trabajo del front** (`src/GeometriaFactory.Web/` y `visor/`, intake §17.6.P.7) deja fuera a `src/GeometriaFactory.Contracts/`, de modo que un cambio de contrato no dispara la publicación del front por fusión, aunque `QG-08` exija el despliegue conjunto~~ · **CERRADO el 2026-08-11**: la categoría 09 de `GeometriaFactory-Web` lo elevó al Product Owner y el intake **1.22** §17.6.P.7 agregó la tercera ruta | La categoría 09 de `GeometriaFactory-Web`, que es la dueña de ese flujo de trabajo, con elevación al Product Owner si cambia lo que el intake declara | Cerrado con el intake 1.22 |
| PD-02 | Si la versión de la plataforma del front baja por la puerta técnica `PT-01.a`, este ensamblado tiene que seguir siendo **cargable por los dos procesos** | El equipo, al medir `PT-01` en la etapa `a` | Etapa `a` |
| PD-03 | La **confirmación del valor rotulado [ASUNCIÓN]** que hoy deja condicionado a `QG-06`. Confirmado, pasa a bloqueante sin ningún otro cambio | El Product Owner, sobre el intake §17.4.P.10, por `BT-18` | Sin fecha comprometida |
| PD-04 | La **herramienta concreta** de cada stage y su anclaje de versión | El equipo, en el punto de control de la etapa `a` | Etapa `a` |

## 11. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara los **dos** stages que el intake §17.4.P.8 y `05` §5 fijan —`restore` y `build`, **sin `test` propio**— y los **nueve** quality gates de `08` §3 materializados uno por uno, con su carácter intacto: **`QG-05` bloqueante** porque la asunción es sobre la forma del gate, y **`QG-06` condicionado** porque la asunción es sobre lo que se verifica. Declara dónde corre el gate que este proyecto de código no puede correr solo y las **tres** condiciones de un diferimiento válido. Declara los stages del catálogo ausentes con su motivo, los triggers por evento —incluido el propio de este proyecto de código, el cambio de superficie—, la matriz de una sola combinación con la precisión de que una bajada de versión del front lo alcanza, la promoción con la **regla de despliegue conjunto** hecha operativa en tres pasos, y la reversión de las dos unidades juntas. **Eleva como `PD-01` que la restricción de rutas del flujo de trabajo del front deja fuera a este ensamblado**, de modo que el despliegue conjunto queda hoy apoyado en el disparo manual. |
| 1.1 | 2026-08-11 | **Propagación de las dos decisiones de despliegue del Product Owner** del intake **1.22** §17.6.P.7 y fila 1.22 de su control de cambios. **(a)** El filtro de rutas del flujo que publica el front incluye hoy `src/GeometriaFactory.Contracts/`: se reescribe el cierre de §6, que ya no eleva un hallazgo sino que registra su desenlace, y **`PD-01` de §10 queda cerrado** conservando su fila con la fecha. **(b)** Se declara el orden de salida —**primero el backend**— en §6, con la constancia de que el filtro no coordina despliegues y de que el orden **no vuelve automático** el despliegue conjunto: el intervalo se minimiza, no se elimina. Sube la trazabilidad upstream del intake de **1.20** a **1.22**. |
