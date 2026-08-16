# Pipeline CI/CD — GeometriaFactory-Domain

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** Pipeline-CI-CD.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero DevOps Senior + Release Engineer (AG-09)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../../../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) 1.0 §3 (los **ocho** quality gates); [`../08-Calidad-Y-Pruebas/Criterios-Validacion.md`](../../../08-Calidad-Y-Pruebas/Criterios-Validacion.md) 1.0; [`../08-Calidad-Y-Pruebas/Definition-Of-Done.md`](../../../08-Calidad-Y-Pruebas/Definition-Of-Done.md) 1.0 §1.3; [`../08-Calidad-Y-Pruebas/Plan-Pruebas.md`](../../../08-Calidad-Y-Pruebas/Plan-Pruebas.md) 1.0 §3; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §5 y §8; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.20** §10, §13, §15, §16, §17.1.P.6 · GeometriaFactory-Domain a §17.1.P.10 · GeometriaFactory-Domain y §22
**Trazabilidad downstream:** [`Estrategia-Versionado.md`](Estrategia-Versionado.md), [`Entornos-Deploy.md`](Entornos-Deploy.md), [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md); `10-Examples` y `11-Documentacion` cuando se emitan; `Producto/Pipeline-Producto.md`, que orquesta el orden de construcción y **no duplica** este pipeline

---

## Tabla de contenido

- [1. Alcance y qué no es este pipeline](#1-alcance-y-qué-no-es-este-pipeline)
- [2. Stages](#2-stages)
  - [2.1 Tabla de stages y gates](#21-tabla-de-stages-y-gates)
  - [2.2 Los cuatro stages que no existen acá, y por qué](#22-los-cuatro-stages-que-no-existen-acá-y-por-qué)
- [3. Triggers](#3-triggers)
- [4. Matriz de sistema operativo y plataforma](#4-matriz-de-sistema-operativo-y-plataforma)
- [5. Caché y artefactos](#5-caché-y-artefactos)
- [6. Promoción](#6-promoción)
- [7. Reversión](#7-reversión)
- [8. Notificaciones](#8-notificaciones)
- [9. Qué aporta a la canalización de las dos unidades desplegables](#9-qué-aporta-a-la-canalización-de-las-dos-unidades-desplegables)
- [10. Puntos abiertos](#10-puntos-abiertos)
- [11. Control de cambios](#11-control-de-cambios)

---

## 1. Alcance y qué no es este pipeline

`GeometriaFactory-Domain` es una **biblioteca, no un servicio desplegable**. `05` §5 lo declara sin unidad de despliegue propia: su artefacto se compila dentro del artefacto de agrupación del producto y viaja embebido en las dos unidades desplegables por la vía de sus consumidores. `redistribuible` es false y el intake §13 declara que **ningún proyecto de código del producto se publica como paquete redistribuible**, porque ninguna fuente declara publicación en un feed.

De ahí el alcance de este documento, y conviene decirlo antes que nada para que no se lo lea como un pipeline recortado: **su DevOps es compilación, prueba y verificación estructural**. No hay empaquetado propio, no hay publicación, no hay ambientes y no hay despliegue. Inventarle cualquiera de esas cuatro cosas sería inventar un acto que este proyecto de código no ejecuta.

Lo que sí hay, y es lo que este pipeline ejecuta y bloquea, son **los ocho quality gates** que [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../../../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §3 declara. **Esta categoría no los redefine, no los relaja y no agrega ninguno**: los materializa como stages, con el carácter que la Fase E les fijó.

## 2. Stages

Los stages son los **tres** que declaran el intake §17.1.P.8 · GeometriaFactory-Domain —«restore → build → test»— y `05` §5. Cada uno declara su comando, su verificación y su criterio de éxito. Los comandos son los **guiones del repositorio** que el intake §16 lista, y no un comando de plataforma escrito acá: el intake, en el encabezado de su Parte C, declara que todo el ciclo ocurre dentro del contenedor de desarrollo porque el equipo anfitrión no tiene el kit de desarrollo instalado, y §10 lo declara como restricción del cliente. **Un pipeline que invocara la plataforma por fuera de esos guiones no sería reproducible en la máquina de quien construye.**

### 2.1 Tabla de stages y gates

| Stage | Qué ejecuta | Gate que verifica | Umbral | Carácter |
| --- | --- | --- | --- | --- |
| `restore` | Restauración de dependencias de la plataforma para el artefacto de agrupación | Ninguno propio. Su falla detiene la construcción por sí misma | — | Bloqueante por construcción |
| `build` | `scripts/build.sh` | `QG-01`: termina en **0 y sin advertencias** | 0 advertencias | **Bloqueante** |
| `build` | El mismo guion, con el análisis estático de la plataforma activo | `QG-04`: **0** referencias a otros proyectos de código del producto y **0** a bibliotecas de persistencia, transporte o serialización | 0 y 0 | **Bloqueante** |
| `test` | `scripts/test.sh` | `QG-02`: la batería pasa entera, **0** pruebas rojas y **0** deshabilitadas sin motivo escrito | 0 y 0 | **Bloqueante** |
| `test` | Prueba de inspección `TC-02023` | `QG-05`: **42 de 42** condiciones del catálogo alcanzadas y **0** emitidas fuera de él, comparado en las dos direcciones | 42 y 0 | **Bloqueante** |
| `test` | Prueba de inspección `TC-02026` sobre la matriz de invariantes | `QG-06`: **9 de 9** invariantes con prueba de violación rechazada, **sin dobles** | 9 y 0 | **Bloqueante al cierre de etapa** |
| `test` | Recolector de cobertura, con informe **por componente** | `QG-03`: **90 %** de líneas y **85 %** de ramas | 90 / 85 **[ASUNCIÓN del intake §17.1.P.6 · GeometriaFactory-Domain, asunción `A-3` de §22]** | **Condicionado**: se mide y se registra; no bloquea la fusión |
| `test` | Duración total reportada por el ejecutor | `QG-07`: la batería completa termina en menos de **10 segundos** | 10 s **[ASUNCIÓN del intake §17.1.P.10 · GeometriaFactory-Domain, asunción `A-5` de §22]** | **Condicionado** |
| Revisión del pull request | Lectura de la superficie pública contra [`../05-Arquitectura-Tecnica/Adrs/ADR-02002-Superficie-Publica-De-Guardas-Y-Resultados-Tipados.md`](../../../05-Arquitectura-Tecnica/Adrs/ADR-02002-Superficie-Publica-De-Guardas-Y-Resultados-Tipados.md) | `QG-08`: ninguna condición prevista viaja como excepción de control de flujo | 0 excepciones de negocio | **Se rechaza en revisión aunque compile** |

**Por qué el gate de análisis estático no tiene stage propio de `lint`.** Ninguna fuente del producto declara un linter separado, y el criterio que un stage de `lint` verificaría ya está expresado como **cero advertencias de construcción**: `CV-20` de [`../08-Calidad-Y-Pruebas/Criterios-Validacion.md`](../../../08-Calidad-Y-Pruebas/Criterios-Validacion.md) declara que el análisis estático no introduce advertencias nuevas y lo hace **bloqueante por `CV-13`**, que es el gate de `build`. Abrir un stage aparte duplicaría la misma medición en dos lugares. La elección concreta de las reglas de análisis y su anclaje de versión son de la etapa `a`, por la regla de anclaje de versiones del encabezado de la Parte C del intake.

**Los dos gates condicionados se miden igual.** Condicionado no es opcional: `Estrategia-Calidad.md` §3.1 declara que la medición se hace y el resultado se registra, y lo que queda en suspenso es la consecuencia automática. El pipeline **emite el número y no lo silencia**; su incumplimiento entra como hallazgo del punto de control de la etapa, no como rechazo de la fusión. Los dos dependen de valores rotulados **[ASUNCIÓN]** en el intake §22, y `BT-02015` es la tarea que los eleva al Product Owner.

### 2.2 Los cuatro stages que no existen acá, y por qué

`Rules-Devops.md` §4.2 enumera siete stages obligatorios. Tres de ellos existen arriba; los cuatro restantes **se declaran ausentes con su motivo**, en lugar de omitirse en silencio:

| Stage del catálogo | Estado acá | Motivo |
| --- | --- | --- |
| SCA | **Se reduce a una comprobación de ausencia**, y es `QG-04` | No hay superficie que analizar: el intake §17.1.P.1 · GeometriaFactory-Domain declara este proyecto de código **sin dependencias core**, y `05` §8 fija en **0** las referencias salientes. Un análisis de composición sobre un inventario vacío no tiene sujeto; lo que sí tiene sujeto es verificar que ese cero se sostiene, y eso ya bloquea |
| SBOM | **No se genera acá** | No hay artefacto publicado del que emitir inventario. El inventario que importa es el de las dos unidades desplegables, que son las que salen del repositorio. Ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §1 |
| Firma | **No se firma acá** | Sólo se firma lo que un integrador recibe por un canal, y no hay canal ni integrador externo: `redistribuible` es false (intake §13). Ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §2 |
| Publish | **No existe** | El intake §17.1.P.7 · GeometriaFactory-Domain declara que la biblioteca **no se publica en ningún feed** y que se compila dentro de `GeometriaFactory.sln` |

## 3. Triggers

Explícitos por evento, y derivados del modelo de trabajo que el intake §15 declara: una rama y un pull request por etapa, con el pull request como punto de control, y **etapas en serie** —no se abre la rama de una etapa antes de fusionar la anterior— (intake §10 y §15).

| Evento | Qué corre | Qué bloquea |
| --- | --- | --- |
| Confirmación empujada a la rama de una etapa | `restore` → `build` → `test` completos | Nada por sí solo: es la señal temprana para quien construye |
| Apertura o actualización del pull request de la etapa | Los tres stages, más la revisión de `QG-08` | **La fusión**, por los gates bloqueantes de §2.1 |
| Fusión a la rama principal | Los tres stages sobre el estado fusionado | El cierre de la etapa si algo se rompió al fusionar |
| Etiqueta de cierre de etapa | Los tres stages sobre el estado etiquetado | La declaración de etapa cerrada |

**No hay trigger por calendario.** El intake §10 declara «sin plazo; el avance se mide por etapas cerradas», y una ejecución programada semanal sería una cadencia que ninguna fuente da.

## 4. Matriz de sistema operativo y plataforma

**Una sola combinación, y es una decisión declarada y no una carencia.**

| Trigger | Sistema operativo | Plataforma objetivo |
| --- | --- | --- |
| Todos los de §3 | El del contenedor de desarrollo, que es el mismo del servidor del backend | `net10.0`, sin sufijo de plataforma |

Justificación, tomada del intake §17.1.P.9 · GeometriaFactory-Domain: la biblioteca apunta a `net10.0` sin sufijo y se ejecuta en Linux, que es el sistema operativo del contenedor de desarrollo y el del servidor del backend; **toda combinación no listada se considera no soportada**, y en particular **no** apunta a `net10.0-windows`, que es de la Actividad 1 —el emisor del dato— y no forma parte de este producto.

**Contra una matriz cruzada con un segundo sistema operativo**: no cubriría a ningún integrador real. Los dos consumidores de esta biblioteca son `GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`, del mismo producto, y las dos unidades desplegables corren sobre el mismo sistema operativo. El costo de minutos no compraría cobertura de nada.

## 5. Caché y artefactos

| Aspecto | Decisión | Fundamento |
| --- | --- | --- |
| Caché de dependencias | Caché del restaurador de paquetes de la plataforma, con llave derivada de los archivos de proyecto del artefacto de agrupación | Decisión de esta categoría. Es el único insumo externo del stage `restore` |
| Invalidación | Al cambiar cualquier archivo de proyecto. **No se declara ninguna expiración por tiempo** | Una expiración en días sería un plazo que ninguna fuente da |
| Artefacto del stage `build` | El ensamblado compilado, consumido en la misma ejecución por `test` y por los proyectos de código dependientes | `05` §5: el artefacto viaja embebido en sus consumidores |
| Artefacto del stage `test` | El **informe de cobertura por componente** y la salida de la batería, con su duración total | `Estrategia-Testing.md` §2 exige el informe por componente y prohíbe el número global único |
| Retención | Mientras dure el punto de control de la etapa: los dos artefactos se adjuntan al **informe de cierre** que el intake §15 declara obligatorio | El informe de cierre es autocontenido por regla del intake §15 |

**El informe de cobertura no se emite como número global.** `Estrategia-Testing.md` §2 declara que un 90 % global con el evaluador de admisibilidad en 70 % es un incumplimiento aunque el promedio cierre, de modo que el artefacto del stage `test` es la tabla por componente y no un único porcentaje.

## 6. Promoción

**No hay canales entre los que promover, y declararlo es más honesto que inventar dos.** El modelo de `Rules-Devops.md` §2.2 para el tipo `library` es un par de canales sobre un feed único; acá **no hay feed**, y el propio catálogo de reglas declara anti-patrón confundir publicación con despliegue. Ver [`Entornos-Deploy.md`](Entornos-Deploy.md) §1, donde el apartamiento queda declarado con el ADR que ya lo sostiene.

La única promoción que este proyecto de código ejecuta es la **de estado del trabajo**, y es la del producto entero:

| Transición | Trigger | Prerrequisitos | Aprobador |
| --- | --- | --- | --- |
| Rama de etapa → rama principal | Fusión del pull request de la etapa | Los gates bloqueantes de §2.1 en verde, y los **nueve** criterios de salida de [`../08-Calidad-Y-Pruebas/Plan-Pruebas.md`](../../../08-Calidad-Y-Pruebas/Plan-Pruebas.md) §3 cumplidos | El Product Owner, con **OK explícito** en el punto de control (intake §15) |
| Etapa fusionada → etapa cerrada | Etiqueta al fusionar | La Definition of Done §1.3 entera, incluida la constancia de la medición de los criterios condicionados | El mismo, con constancia escrita en el informe de cierre |

**El aprobador humano no es un agregado de esta categoría.** El intake §15 declara el punto de control bloqueante y `equipo_n` es 1: la misma persona construye y aprueba, y `Estrategia-Calidad.md` §4 ya declara que esta situación no se disimula con un RACI de tres columnas.

## 7. Reversión

| Situación | Procedimiento | Fundamento |
| --- | --- | --- |
| Una etapa fusionada rompe algo que estaba en verde | Volver a la **etiqueta de la etapa anterior**, que permite reconstruir cualquier demostración ya aprobada | Intake §17.1.P.8 · GeometriaFactory-Domain, y [`../05-Arquitectura-Tecnica/Adrs/ADR-02003-Versionado-Y-Estabilidad-De-La-Superficie.md`](../../../05-Arquitectura-Tecnica/Adrs/ADR-02003-Versionado-Y-Estabilidad-De-La-Superficie.md) §5 |
| Un cambio de esta biblioteca rompe la compilación de un consumidor | La rotura aparece en la construcción del consumidor, antes de cualquier despliegue. Se revierte la confirmación o se corrige en la misma rama de etapa | `ADR-02003` §7: qué constituye cambio mayor, menor y parche |
| Un cambio mayor llegó sin fila en el registro de cambios del producto | Se agrega la fila en `changelog.md` antes de cerrar la etapa | `ADR-02003` §8, métrica de cambios mayores sin registro, objetivo **0** |

**No hay delist, no hay retiro de versión y no hay ventana de gracia**, porque no hay artefacto publicado que retirar. El procedimiento de reversión de esta biblioteca **es de código y de etiqueta**, y termina ahí.

## 8. Notificaciones

| Canal | Qué comunica | Fundamento |
| --- | --- | --- |
| La salida del pipeline sobre el pull request de la etapa | El resultado de los tres stages, gate por gate, con el número de los dos condicionados | El pull request **es** el punto de control (intake §15) |
| El informe de cierre de la etapa | La medición de `QG-03` y `QG-07` con su distancia al umbral, y la constancia de los gates bloqueantes | Definition of Done §1.3 y `Criterios-Validacion.md` §6 |
| El registro de cambios del producto | Toda fila de cambio mayor de esta biblioteca | `ADR-02003` §7 |

**No se declara ningún canal de mensajería ni ningún tablero.** Ninguna fuente del producto declara uno, `equipo_n` es 1 y el destinatario de la notificación es la misma persona que ejecuta. Un escalamiento por severidad hacia un equipo que no existe sería ceremonia sin lector.

## 9. Qué aporta a la canalización de las dos unidades desplegables

Este proyecto de código no se despliega, pero **condiciona** a las dos canalizaciones que sí despliegan. Lo que aporta, y que la canalización de nivel producto tiene que respetar:

| Aporte | Efecto sobre la canalización de la unidad desplegable | Fundamento |
| --- | --- | --- |
| Es **nivel topológico 0** y no tiene dependencias | Se construye **antes** que `GeometriaFactory-Application`, `GeometriaFactory-Infrastructure` y `GeometriaFactory-Api`. Ningún orden de construcción puede ubicarlo después de sus dependientes | Intake §13, orden topológico |
| Sus gates corren **antes** de que la imagen del backend se construya | Un fallo de `QG-01`, `QG-02`, `QG-04`, `QG-05` o `QG-06` detiene la cadena en el punto más barato posible, sin llegar al empaquetado | `05` §5 y la tabla de §2.1 |
| Viaja **embebido** en la imagen del backend | No hay paso de publicación intermedio ni versión que resolver: la imagen se construye desde el mismo estado del repositorio | Intake §13: los dos artefactos entregables del producto son una imagen y una publicación por FTP |
| Un cambio mayor suyo **no** obliga por sí solo a redesplegar el front | El front no lo referencia: sus dependencias son `GeometriaFactory-Contracts` y el bundle del visor | Intake §13, columna de dependencias |

## 10. Puntos abiertos

| Id | Punto abierto | Quién lo cierra | Cuándo |
| --- | --- | --- | --- |
| PD-01 | La **herramienta concreta** de cada stage —ejecutor de pruebas, recolector de cobertura y reglas de análisis estático— y su anclaje de versión | El equipo, en el punto de control de la etapa `a` | Etapa `a`, por la regla de anclaje de versiones del intake, encabezado de la Parte C |
| PD-02 | La **confirmación de los dos valores rotulados [ASUNCIÓN]** que hoy dejan condicionados a `QG-03` y `QG-07`. Confirmados, los dos pasan a bloqueantes sin ningún otro cambio de este documento | El Product Owner, sobre el intake §22 | `BT-02015`, al cerrar la etapa `d` (`Estrategia-Calidad.md` §5) |
| PD-03 | Si el mutation score entra alguna vez al pipeline. Hoy `CV-19` se reporta «sin medir» y su hueco está declarado | La categoría 08, si la herramienta se elige | Sin fecha comprometida |

**Ninguno de los tres se cierra inventando un valor.** El tratamiento de `PD-02` es el que la Fase E ya declaró y esta categoría lo adopta sin cambiarlo.

## 11. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara los **tres** stages que el intake §17.1.P.8 · GeometriaFactory-Domain fija —`restore`, `build` y `test`—, con los **ocho** quality gates de `08` §3 materializados uno por uno, respetando su carácter: **seis bloqueantes** y **dos condicionados** por depender de valores rotulados [ASUNCIÓN] en el intake §22. Declara los **cuatro** stages del catálogo de reglas que no existen acá, cada uno con su motivo, en lugar de omitirlos. Declara los triggers por evento, la matriz de una sola combinación con su justificación, la política de caché y de artefactos, la ausencia de canales de promoción, la reversión por etiqueta de etapa y la ausencia de canales de notificación, con la constancia de que ninguna fuente los declara. Suma una sección con lo que este proyecto de código aporta a las canalizaciones de las dos unidades desplegables, y **tres** puntos abiertos. |
