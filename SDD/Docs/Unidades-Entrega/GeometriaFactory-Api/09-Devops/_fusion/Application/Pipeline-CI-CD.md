# Pipeline CI/CD — GeometriaFactory-Application

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** Pipeline-CI-CD.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero DevOps Senior + Release Engineer (AG-09)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../../../08-Calidad-Y-Pruebas/_fusion/Application/Estrategia-Calidad.md) 1.0 §3 y §3.1 (los **once** quality gates); [`../08-Calidad-Y-Pruebas/Plan-Pruebas.md`](../../../08-Calidad-Y-Pruebas/_fusion/Application/Plan-Pruebas.md) 1.0 §3 (los **once** criterios de salida); [`../08-Calidad-Y-Pruebas/Definition-Of-Done.md`](../../../08-Calidad-Y-Pruebas/_fusion/Application/Definition-Of-Done.md) 1.0 §1.3; [`../08-Calidad-Y-Pruebas/Estrategia-Testing.md`](../../../08-Calidad-Y-Pruebas/_fusion/Application/Estrategia-Testing.md) 1.0; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/_fusion/Application/Arquitectura-Proyecto-Codigo.md) 1.0 §5, §8 y §11; [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../../../06-Backlog-Tecnico/_fusion/Application/Backlog-Tecnico.md) (`BT-04004`, `BT-04005`, `BT-04006`, `BT-04018`, `BT-04019`); [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.21** §10, §13, §15, §16, §17.1.P.1 · GeometriaFactory-Application a §17.1.P.12 · GeometriaFactory-Application y §22
**Trazabilidad downstream:** [`Estrategia-Versionado.md`](Estrategia-Versionado.md), [`Entornos-Deploy.md`](Entornos-Deploy.md), [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md); `10-Examples` y `11-Documentacion` cuando se emitan; `Producto/Pipeline-Producto.md`, que orquesta el orden de construcción y **no duplica** este pipeline

---

## Tabla de contenido

- [1. Alcance y qué no es este pipeline](#1-alcance-y-qué-no-es-este-pipeline)
- [2. Stages](#2-stages)
  - [2.1 Tabla de stages y gates](#21-tabla-de-stages-y-gates)
  - [2.2 La puerta propia que el intake declara, y por qué es de construcción y no de prueba](#22-la-puerta-propia-que-el-intake-declara-y-por-qué-es-de-construcción-y-no-de-prueba)
  - [2.3 Los stages del catálogo que no existen acá](#23-los-stages-del-catálogo-que-no-existen-acá)
- [3. Triggers](#3-triggers)
- [4. Matriz de sistema operativo y plataforma](#4-matriz-de-sistema-operativo-y-plataforma)
- [5. Caché y artefactos](#5-caché-y-artefactos)
- [6. Promoción](#6-promoción)
- [7. Reversión](#7-reversión)
- [8. Notificaciones](#8-notificaciones)
- [9. Qué aporta a la canalización de la unidad desplegable del servidor propio](#9-qué-aporta-a-la-canalización-de-la-unidad-desplegable-del-servidor-propio)
- [10. Puntos abiertos](#10-puntos-abiertos)
- [11. Control de cambios](#11-control-de-cambios)

---

## 1. Alcance y qué no es este pipeline

`GeometriaFactory-Application` es una **biblioteca de casos de uso, no un servicio desplegable**. [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/_fusion/Application/Arquitectura-Proyecto-Codigo.md) §5 lo declara sin unidad de despliegue propia: su artefacto se compila dentro del artefacto de agrupación del producto y **viaja embebido en la unidad desplegable del servidor propio, por la vía de `GeometriaFactory-Api`**. `redistribuible` es false y el intake §13 declara que **ningún proyecto de código del producto se publica como paquete redistribuible**.

De ahí el alcance: **compilación, prueba con dobles y verificación estructural**. No hay empaquetado propio, no hay publicación, no hay ambientes y no hay despliegue. Inventarle cualquiera de esas cuatro cosas sería inventar un acto que este proyecto de código no ejecuta.

Hay además un rasgo que ordena todo el documento y conviene decirlo antes que nada: **la calidad de este proyecto de código se mide entera sin ambiente**. [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../../../08-Calidad-Y-Pruebas/_fusion/Application/Estrategia-Calidad.md) §1 lo declara: cada uno de los **once** casos de uso se puede ejercer entero con dobles de los **cuatro** puertos, sin base de datos y sin frontera de proceso, y por eso **no hay ambiente donde descubrir un defecto suyo**: se descubre en una prueba que falla o en una revisión que rechaza. Un pipeline que le levantara un ambiente para probarlo estaría contradiciendo la propiedad que justifica el estilo entero del proyecto de código.

Lo que este pipeline ejecuta y bloquea son **los once quality gates** que `Estrategia-Calidad.md` §3 declara. **Esta categoría no los redefine, no los relaja y no agrega ninguno**: los materializa como stages, con el carácter que la Fase E les fijó.

## 2. Stages

Los stages son los **tres** que declara `05` §5 —`restore` → `build` → `test`—, tomados del intake §17.1.P.8 · GeometriaFactory-Application, que declara su pipeline idéntico al de §17.1.P.8 · GeometriaFactory-Domain y le agrega una puerta propia. Los comandos son los **guiones del repositorio** que el intake §16 lista, y no un comando de plataforma escrito acá: el intake, en el encabezado de su Parte C, declara que todo el ciclo ocurre dentro del contenedor de desarrollo porque el equipo anfitrión no tiene el kit de desarrollo instalado, y §10 lo declara como restricción del cliente.

### 2.1 Tabla de stages y gates

| Stage | Qué ejecuta | Gate que verifica | Umbral | Carácter |
| --- | --- | --- | --- | --- |
| `restore` | Restauración de dependencias de la plataforma para el artefacto de agrupación | Ninguno propio. Su falla detiene la construcción por sí misma | — | Bloqueante por construcción |
| `build` | `scripts/build.sh` | `QG-01`: el guion de construcción termina en **0 y sin advertencias** | 0 advertencias | **Bloqueante** |
| `build` | Inspección del archivo de proyecto (`TC-04027`) | `QG-05`: exactamente **1** referencia a otro proyecto de código del producto —`GeometriaFactory-Domain`— y **0** a bibliotecas de persistencia, transporte, serialización o marco web | 1 y 0 | **Bloqueante** |
| `test` | `scripts/test.sh` | `QG-02`: la batería pasa entera, **0** pruebas rojas y **0** deshabilitadas sin motivo escrito | 0 y 0 | **Bloqueante** |
| `test` | Prueba de inspección `TC-04026`, más la revisión del pull request | `QG-04`: **ninguna** prueba de esta capa toca la base de datos real | 0 | **Bloqueante.** Es la puerta propia del intake §17.1.P.8 · GeometriaFactory-Application. Ver §2.2 |
| `test` | Prueba de inspección `TC-04028`, en las dos direcciones | `QG-06`: **100 %** de las **36** condiciones del catálogo alcanzadas por prueba y **0** emitidas fuera de él | 36 y 0 | **Bloqueante** |
| `test` | Inspección de los **once** orquestadores y `TC-04029`, con la baja de cuenta como caso testigo | `QG-08`: **a lo sumo 1** unidad de trabajo por caso de uso y **0** casos de uso que repartan su efecto entre dos | 1 y 0 | **Bloqueante** |
| `test` | `TC-04030`, sobre la proyección que devuelve la consulta | `QG-09`: **0** componentes de pieza cargados en el listado del alumno y en el de la comisión | 0 | **Bloqueante** |
| `test` | Recolector de cobertura, con informe **por componente** | `QG-03`: **85 %** de líneas y **80 %** de ramas | 85 / 80 **[ASUNCIÓN del intake §17.1.P.6 · GeometriaFactory-Application, asunción `A-3` de §22]** | **Condicionado**: se mide y se registra; no bloquea la fusión |
| `test` | Medición sobre la batería unitaria con doble del puerto de validación | `QG-10`: el caso de uso más pesado resuelve en menos de **500 ms** para el texto semilla de **3** piezas de `E-1`, **sin acceso a base** | 500 ms **[ASUNCIÓN del intake §17.1.P.10 · GeometriaFactory-Application, asunción `A-5` de §22]** | **Condicionado** |
| Cierre de la etapa | `TC-04011` y la matriz de [`../08-Calidad-Y-Pruebas/Matriz-Cobertura-Pruebas.md`](../../../08-Calidad-Y-Pruebas/_fusion/Application/Matriz-Cobertura-Pruebas.md) §5 | `QG-07`: **4 de 4** comprobaciones de autorización con al menos una prueba de su negativa **sin base de datos**, y **1** sola prueba que verifique que la cuarta corta antes que las otras tres | 4, 4 y 1 | **Bloqueante al cierre de la etapa** |
| Revisión del pull request | `TC-04031` y lectura de la superficie pública contra [`../05-Arquitectura-Tecnica/Adrs/ADR-04006-Resultado-Tipado-Y-Catalogo-Cerrado-De-Condiciones.md`](../../../05-Arquitectura-Tecnica/Adrs/ADR-04006-Resultado-Tipado-Y-Catalogo-Cerrado-De-Condiciones.md) | `QG-11`: ninguna condición prevista viaja como excepción de control de flujo | 0 excepciones de negocio | **Se rechaza en revisión aunque compile** |

**Los dos gates condicionados se miden igual.** Condicionado no es opcional: `Estrategia-Calidad.md` §3.1 declara que la medición se hace y el resultado se registra, y lo que queda en suspenso es la consecuencia automática. El pipeline **emite el número y no lo silencia**; su incumplimiento entra como hallazgo del punto de control de la etapa, no como rechazo de la fusión. Los dos dependen de valores rotulados **[ASUNCIÓN]** en el intake §22, y `BT-04018` —«Confirmar los dos valores rotulados como asunción y fijar la puerta de cobertura», etapa `d`— es la tarea que los eleva al Product Owner.

**Ningún otro gate de este proyecto de código es condicionado**, aunque `QG-10` y `QG-03` no sean los únicos con número: los **cuatro** de `QG-07`, los **36** de `QG-06` y el **1** de `QG-05` salen de `05` §8 y de `03` §7.1, no de una marca **[ASUNCIÓN]**, y bloquean.

### 2.2 La puerta propia que el intake declara, y por qué es de construcción y no de prueba

`QG-04` es el único gate de este proyecto de código que la fuente enuncia como puerta propia: el intake §17.1.P.8 · GeometriaFactory-Application, después de declarar el pipeline idéntico al de `GeometriaFactory-Domain`, agrega **«ninguna prueba de esta capa toca la base de datos real; si una lo hace, está mal ubicada y pertenece a integración»**.

Lo que esta categoría decide, y es decisión de esta categoría, es **dónde se lo hace cumplir**:

| Aspecto | Decisión | Fundamento |
| --- | --- | --- |
| Dónde se verifica | En el stage `test`, con `TC-04026`, **y en la revisión del pull request** | `Estrategia-Calidad.md` §3, columna de verificación de `QG-04` |
| Qué lo sostiene antes de que una prueba corra | **`QG-05`**, que es de `build`: con **0** referencias a bibliotecas de persistencia declaradas en el archivo de proyecto, una prueba de esta capa **no tiene con qué** abrir un almacén. `Estrategia-Calidad.md` §3 lo declara: `QG-05` «es la propiedad que sostiene `QG-04`» | El mismo |
| Consecuencia sobre el orden de los stages | La comprobación barata corre **antes**: si `QG-05` falla en `build`, la cadena se detiene sin llegar a `test` | Decisión de esta categoría |

**El orden importa y no es cosmético.** Una prueba que abre el almacén real se detecta corriendo la batería y mirando qué archivos aparecieron; una dependencia de persistencia declarada se detecta leyendo un archivo de proyecto. La segunda es la que hace imposible a la primera, y es la que corre en el stage más barato.

### 2.3 Los stages del catálogo que no existen acá

`Rules-Devops.md` §4.2 enumera siete stages obligatorios. Tres existen arriba; los cuatro restantes **se declaran ausentes con su motivo**, en lugar de omitirse en silencio:

| Stage del catálogo | Estado acá | Motivo |
| --- | --- | --- |
| Lint | **Incorporado en `build`** | Ninguna fuente del producto declara un linter separado, y el criterio que un stage de `lint` verificaría ya está expresado como **cero advertencias de construcción** (`QG-01`). Abrir un stage aparte duplicaría la misma medición en dos lugares. La elección concreta de las reglas de análisis y su anclaje de versión son de la etapa `a` |
| SCA | **Se reduce a una comprobación de ausencia**, y es `QG-05` | El intake §17.1.P.1 · GeometriaFactory-Application declara **una sola dependencia core**, `GeometriaFactory.Domain`, que es del propio producto y no tiene dependencias externas. No hay inventario de terceros que analizar; lo que sí tiene sujeto es verificar que ese **1 y 0** se sostenga, y eso ya bloquea. Ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §4 |
| SBOM | **No se genera acá** | No hay artefacto publicado del que emitir inventario. El inventario que importa es el de la unidad desplegable del servidor propio, que es la que sale del repositorio. Ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §1 |
| Firma | **No se firma acá** | Sólo se firma lo que un integrador recibe por un canal, y no hay canal ni integrador externo: `redistribuible` es false (intake §13). Ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §2 |
| Publish | **No existe** | El intake §17.1.P.7 · GeometriaFactory-Application declara la estrategia idéntica a la de §17.1.P.7 · GeometriaFactory-Domain: **sin publicación en feed**. `05` §5 lo repite en su última fila |

## 3. Triggers

Explícitos por evento, y derivados del modelo de trabajo que el intake §15 declara: una rama y un pull request por etapa, con el pull request como punto de control, y **etapas en serie** —no se abre la rama de una etapa antes de fusionar la anterior— (intake §10 y §15).

| Evento | Qué corre | Qué bloquea |
| --- | --- | --- |
| Confirmación empujada a la rama de una etapa | `restore` → `build` → `test` completos | Nada por sí solo: es la señal temprana para quien construye |
| Apertura o actualización del pull request de la etapa | Los tres stages, más la revisión de `QG-04` y de `QG-11` | **La fusión**, por los gates bloqueantes de §2.1 |
| Pull request que **agrega o cambia un caso de uso, un puerto o una condición del catálogo** | Todo lo anterior, más `TC-04028` en las dos direcciones y `TC-04029` sobre el caso de uso tocado | La fusión. Es la cadencia propia de este proyecto de código: su defecto característico es una condición emitida fuera del catálogo, y entra de a una |
| Fusión a la rama principal | Los tres stages sobre el estado fusionado | El cierre de la etapa si algo se rompió al fusionar |
| Cierre de la etapa | Lo anterior, más `TC-04011` y la matriz de comprobaciones (`QG-07`) | La declaración de etapa cerrada |
| Etiqueta de cierre de etapa | Los tres stages sobre el estado etiquetado | La declaración de etapa cerrada |

**No hay trigger por calendario.** El intake §10 declara «sin plazo; el avance se mide por etapas cerradas», y una ejecución programada semanal sería una cadencia que ninguna fuente da.

## 4. Matriz de sistema operativo y plataforma

**Una sola combinación, y es una decisión declarada y no una carencia.**

| Trigger | Sistema operativo | Plataforma objetivo |
| --- | --- | --- |
| Todos los de §3 | El del contenedor de desarrollo, que es el mismo del servidor del backend | `net10.0`, sin sufijo de plataforma |

Justificación, tomada del intake §17.1.P.9 · GeometriaFactory-Application: `net10.0`, Linux, **sin dependencias de plataforma**. `05` §5 lo repite y agrega que el ciclo de construcción ocurre dentro del contenedor de desarrollo.

**Contra una matriz cruzada con un segundo sistema operativo**: no cubriría a ningún integrador real. Los consumidores de esta biblioteca son `GeometriaFactory-Api` y `GeometriaFactory-Infrastructure`, del mismo producto, y la única unidad desplegable donde termina embebida corre sobre el mismo sistema operativo. El costo de minutos no compraría cobertura de nada.

**Una precisión que este proyecto de código no comparte con `GeometriaFactory-Contracts`.** Aquel se carga en los dos procesos y por eso una bajada de versión del front lo alcanza; **éste no llega al front**: el intake §13 declara que las dependencias de `GeometriaFactory-Web` son `GeometriaFactory-Contracts` y `GeometriaFactory-Visor`. La puerta `PT-01.a` no lo condiciona.

## 5. Caché y artefactos

| Aspecto | Decisión | Fundamento |
| --- | --- | --- |
| Caché de dependencias | Caché del restaurador de paquetes de la plataforma, con llave derivada de los archivos de proyecto del artefacto de agrupación | Decisión de esta categoría. Es el único insumo externo del stage `restore` |
| Invalidación | Al cambiar cualquier archivo de proyecto. **No se declara ninguna expiración por tiempo** | Una expiración en días sería un plazo que ninguna fuente da |
| Artefacto del stage `build` | El ensamblado compilado, consumido en la misma ejecución por `test`, por `GeometriaFactory-Infrastructure` y por `GeometriaFactory-Api` | Intake §13, columna de dependencias; `05` §5 |
| Artefacto del stage `test` | El **informe de cobertura por componente**, la salida de la batería y **el número de `QG-10`** | `Estrategia-Calidad.md` §3, que exige el informe por componente |
| Artefactos de inspección | Los **recuentos** de `QG-04`, `QG-05`, `QG-06`, `QG-08` y `QG-09` | Los cinco están escritos como recuentos en `Estrategia-Calidad.md` §3 |
| Retención | Mientras dure el punto de control de la etapa: se adjuntan al **informe de cierre** que el intake §15 declara obligatorio | El informe de cierre es autocontenido por regla del intake §15 |

**El informe de cobertura no se emite como número global**, y no es preferencia de esta categoría: `QG-03` se verifica sobre el informe **por componente**, y este proyecto de código tiene **8** componentes (`05` §3.1). Un promedio único podría cerrar el 85 % dejando un componente muy por debajo.

## 6. Promoción

**No hay canales entre los que promover, y declararlo es más honesto que inventar dos.** El modelo de `Rules-Devops.md` §2.2 para el tipo `library` es un par de canales sobre un feed único; acá **no hay feed**, y el propio catálogo de reglas declara anti-patrón confundir publicación con despliegue. Ver [`Entornos-Deploy.md`](Entornos-Deploy.md) §1.

La única promoción que este proyecto de código ejecuta es la **de estado del trabajo**:

| Transición | Trigger | Prerrequisitos | Aprobador |
| --- | --- | --- | --- |
| Rama de etapa → rama principal | Fusión del pull request de la etapa | Los gates bloqueantes de §2.1 en verde, y los **once** criterios de salida de [`../08-Calidad-Y-Pruebas/Plan-Pruebas.md`](../../../08-Calidad-Y-Pruebas/_fusion/Application/Plan-Pruebas.md) §3 cumplidos | El Product Owner, con **OK explícito** en el punto de control (intake §15) |
| Etapa fusionada → etapa cerrada | Etiqueta al fusionar | La Definition of Done §1.3 entera, incluida la constancia de la medición de los criterios condicionados | El mismo, con constancia escrita en el informe de cierre |

**El aprobador humano no es un agregado de esta categoría.** El intake §15 declara el punto de control bloqueante y `equipo_n` es 1: la misma persona construye y aprueba, y `Estrategia-Calidad.md` §4 ya declara que esta situación no se disimula con un RACI de tres columnas.

## 7. Reversión

| Situación | Procedimiento | Fundamento |
| --- | --- | --- |
| Una etapa fusionada rompe algo que estaba en verde | Volver a la **etiqueta de la etapa anterior**, que permite reconstruir cualquier demostración ya aprobada | `05` §5, fila de reversión |
| Un cambio de esta biblioteca rompe la compilación de un consumidor | La rotura aparece en la construcción de `GeometriaFactory-Api` o de `GeometriaFactory-Infrastructure`, **antes de cualquier despliegue**. Se revierte la confirmación o se corrige en la misma rama de etapa | Intake §17.1.P.3 · GeometriaFactory-Application: son referencias de proyecto dentro del mismo artefacto de agrupación, y un cambio incompatible **rompe la compilación, que es la señal más temprana posible** |
| Un cambio mayor llegó sin fila en el registro de cambios del producto | Se agrega la fila en `changelog.md` antes de cerrar la etapa | [`Estrategia-Versionado.md`](Estrategia-Versionado.md) §6 |

**No hay delist, no hay retiro de versión y no hay ventana de gracia**, porque no hay artefacto publicado que retirar. El procedimiento de reversión de esta biblioteca **es de código y de etiqueta**, y termina ahí.

## 8. Notificaciones

| Canal | Qué comunica | Fundamento |
| --- | --- | --- |
| La salida del pipeline sobre el pull request de la etapa | El resultado de los tres stages, gate por gate, con **los recuentos** de las cinco inspecciones y el número de los dos condicionados | El pull request **es** el punto de control (intake §15) |
| El informe de cierre de la etapa | La medición de `QG-03` y `QG-10` con su distancia al umbral, la constancia de los gates bloqueantes y el cierre de `QG-07` sobre las cuatro comprobaciones | Definition of Done §1.3 |
| El registro de cambios del producto | Toda fila de cambio mayor de esta biblioteca | [`Estrategia-Versionado.md`](Estrategia-Versionado.md) §6 |

**No se declara ningún canal de mensajería ni ningún tablero.** Ninguna fuente del producto declara uno, `equipo_n` es 1 y el destinatario de la notificación es la misma persona que ejecuta. Un escalamiento por severidad hacia un equipo que no existe sería ceremonia sin lector.

## 9. Qué aporta a la canalización de la unidad desplegable del servidor propio

Este proyecto de código no se despliega, pero **condiciona** a la única canalización donde termina embebido:

| Aporte | Efecto sobre la canalización de la unidad desplegable | Fundamento |
| --- | --- | --- |
| Es **nivel topológico 1** y depende sólo de `GeometriaFactory-Domain` | Se construye **después** de `GeometriaFactory-Domain` y **antes** de `GeometriaFactory-Infrastructure` y de `GeometriaFactory-Api` | Intake §13, orden topológico |
| Sus gates corren **antes** de que la imagen del backend se construya | Un fallo de `QG-01`, `QG-02`, `QG-04`, `QG-05`, `QG-06`, `QG-08` o `QG-09` detiene la cadena en el punto más barato posible, sin llegar al empaquetado | `05` §5 y la tabla de §2.1 |
| Viaja **embebido** en la imagen del backend | No hay paso de publicación intermedio ni versión que resolver: la imagen se construye desde el mismo estado del repositorio | `05` §5; intake §13 |
| Define los **cuatro puertos** que `GeometriaFactory-Infrastructure` implementa | Un cambio de puerto rompe la construcción del adaptador en la misma ejecución, sin llegar a la imagen | Intake §14, fila de `GeometriaFactory-Application` |
| **No llega al front** | Un cambio suyo **no obliga a republicar el front**: el front no lo referencia | Intake §13, columna de dependencias |

## 10. Puntos abiertos

| Id | Punto abierto | Quién lo cierra | Cuándo |
| --- | --- | --- | --- |
| PD-01 | La **herramienta concreta** de cada stage —ejecutor de pruebas, recolector de cobertura y reglas de análisis estático— y su anclaje de versión | El equipo, en el punto de control de la etapa `a` | Etapa `a`, por la regla de anclaje de versiones del intake, encabezado de la Parte C |
| PD-02 | La **confirmación de los dos valores rotulados [ASUNCIÓN]** que hoy dejan condicionados a `QG-03` y `QG-10`. Confirmados, los dos pasan a bloqueantes sin ningún otro cambio de este documento | El Product Owner, sobre el intake §22, por `BT-04018` | Al cerrar la etapa `d` |
| PD-03 | La **herramienta que calcula la versión** a partir de las convenciones de mensaje de confirmación. Es `PA-06` de `05` §11 y esta categoría **la declara por su función y no la elige** | El equipo, en la etapa `a` | Etapa `a` |

**Ninguno de los tres se cierra inventando un valor.** El tratamiento de `PD-02` es el que la Fase E ya declaró y esta categoría lo adopta sin cambiarlo.

## 11. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara los **tres** stages que `05` §5 fija —`restore`, `build` y `test`— y los **once** quality gates de `08` §3 materializados uno por uno, respetando su carácter: **nueve bloqueantes o de rechazo en revisión** y **dos condicionados** por depender de valores rotulados [ASUNCIÓN] en el intake §22. Declara por qué la puerta propia del intake §17.1.P.8 · GeometriaFactory-Application se hace cumplir en dos stages y por qué la comprobación barata —**0** dependencias de persistencia declaradas— corre antes que la cara. Declara los stages del catálogo ausentes con su motivo, los triggers por evento —incluido el propio de este proyecto de código, el cambio de condición del catálogo—, la matriz de una sola combinación con la precisión de que una bajada de versión del front **no** lo alcanza, la política de caché y de artefactos con el informe de cobertura **por componente**, la ausencia de canales de promoción, la reversión por etiqueta y la ausencia de canales de notificación. Suma una sección con lo que aporta a la canalización de la unidad desplegable del servidor propio, y **tres** puntos abiertos. |
