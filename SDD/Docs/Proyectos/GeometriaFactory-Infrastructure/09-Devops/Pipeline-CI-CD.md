# Pipeline CI/CD — GeometriaFactory-Infrastructure

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** Pipeline-CI-CD.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero DevOps Senior + Release Engineer (AG-09)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) 1.1 §3, §3.1 y §3.2 (los **catorce** quality gates); [`../08-Calidad-Y-Pruebas/Plan-Pruebas.md`](../08-Calidad-Y-Pruebas/Plan-Pruebas.md) 1.1 §3 (los **once** criterios de salida); [`../08-Calidad-Y-Pruebas/Definition-Of-Done.md`](../08-Calidad-Y-Pruebas/Definition-Of-Done.md) 1.1 §1.3; [`../08-Calidad-Y-Pruebas/Estrategia-Testing.md`](../08-Calidad-Y-Pruebas/Estrategia-Testing.md) 1.1; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §5, §8, §10.5 y §11; [`../05-Arquitectura-Tecnica/Adrs/ADR-06007-Transformaciones-Al-Arrancar-Con-Linaje-Inmutable.md`](../05-Arquitectura-Tecnica/Adrs/ADR-06007-Transformaciones-Al-Arrancar-Con-Linaje-Inmutable.md); [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md) (`BT-06004`, `BT-06007`, `BT-06018`, `BT-06020`, `BT-06022`, `BT-06023`); [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.22** §10, §13, §15, §16, §17.3.P.1 a §17.3.P.12, §21 y §22
**Trazabilidad downstream:** [`Estrategia-Versionado.md`](Estrategia-Versionado.md), [`Entornos-Deploy.md`](Entornos-Deploy.md), [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md); `10-Examples` y `11-Documentacion` cuando se emitan; `Producto/Pipeline-Producto.md`

---

## Tabla de contenido

- [1. Alcance y qué no es este pipeline](#1-alcance-y-qué-no-es-este-pipeline)
- [2. Stages](#2-stages)
  - [2.1 Tabla de stages y gates](#21-tabla-de-stages-y-gates)
  - [2.2 El cuarto stage, que es propio de este proyecto de código](#22-el-cuarto-stage-que-es-propio-de-este-proyecto-de-código)
  - [2.3 Tres condicionados y uno que no lo es](#23-tres-condicionados-y-uno-que-no-lo-es)
  - [2.4 Los stages del catálogo que no existen acá](#24-los-stages-del-catálogo-que-no-existen-acá)
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

`GeometriaFactory-Infrastructure` es una **biblioteca de adaptadores, no un servicio desplegable**. [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §5 lo declara sin unidad de despliegue propia: se compila dentro del artefacto de agrupación y **viaja embebido en la unidad desplegable del servidor propio, por la vía de `GeometriaFactory-Api`**. `redistribuible` es false y el intake §13 declara que ningún proyecto de código del producto se publica como paquete redistribuible.

Pero **es la biblioteca del producto con más superficie de canalización**, y conviene decir por qué antes de la tabla de stages. Tres rasgos, los tres de la fuente:

1. **Tiene un stage que ninguna otra biblioteca tiene.** El intake §17.3.P.8 declara los stages «restore → build → test → **verificación de migraciones**», y `05` §5 lo repite: la cuarta etapa **es propia de este proyecto de código**.
2. **Es el único de los cinco proyectos de código que no se despliegan con dependencias externas reales**, y con dos de ellas sensibles: la biblioteca de derivación de clave y la de emisión de acceso firmado (intake §17.3.P.1).
3. **Acá viven las dos piezas sensibles del producto** —la derivación de la contraseña y la emisión del acceso firmado— y **la clave de firma se provee desde afuera y nunca entra al repositorio ni a la imagen** (intake §17.3.P.5).

Lo que este pipeline ejecuta y bloquea son **los catorce quality gates** de [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §3. **Esta categoría no los redefine, no los relaja y no agrega ninguno**: los materializa como stages, con el carácter que la Fase E les fijó.

**Lo que no hay**: empaquetado propio, publicación, ambientes y despliegue. `05` §5 declara la publicación como «no se publica» y la reversión como un guion de restablecimiento que **no es un camino de producción**.

## 2. Stages

Los stages son los **cuatro** que el intake §17.3.P.8 y `05` §5 fijan. Los comandos son los **guiones del repositorio** que el intake §16 lista, y todo corre dentro del contenedor de desarrollo, porque el equipo anfitrión no tiene el kit de desarrollo instalado (intake §10 y encabezado de la Parte C).

### 2.1 Tabla de stages y gates

| Stage | Qué ejecuta | Gate que verifica | Umbral | Carácter |
| --- | --- | --- | --- | --- |
| `restore` | Restauración de dependencias de la plataforma para el artefacto de agrupación | Ninguno propio. Su falla detiene la construcción por sí misma | — | Bloqueante por construcción |
| `build` | `scripts/build.sh` | `QG-01`: termina en **0 y sin advertencias** | 0 advertencias | **Bloqueante** |
| `build` | Inspección de dependencias de los **dos motores** (`TC-06014`) | `QG-08`: los dos motores originan exactamente **0** peticiones de red | 0 | **Bloqueante** |
| `test` | `scripts/test.sh` | `QG-02`: la batería pasa entera, **0** rojas y **0** deshabilitadas sin motivo escrito | 0 y 0 | **Bloqueante** |
| `test` | `TC-06001` a `TC-06010`, contra la tabla de `05` §10.5 | `QG-03`: **la batería del validador pasa entera, 10 de 10**, con los **ocho** escenarios como entrada | 10 y 8 | **Bloqueante** |
| `test` | `TC-06009` | `QG-07`: tolerancia **0.01** absoluta con operador **estricto**: `E-1` da **exactamente 2** advertencias y no 3 | 2 | **Bloqueante, y no es condicionado.** Ver §2.3 |
| `test` | `TC-06027` | `QG-09`: **0** provisorias iguales en dos producciones consecutivas sobre la misma cuenta y entre cuentas distintas, y ninguna derivable del nombre, del correo ni de la fecha | 0 | **Bloqueante** |
| `test` | `TC-06019` | `QG-10`: **0** componentes de pieza cargados y **0** apariciones del texto original en una proyección de listado | 0 y 0 | **Bloqueante** |
| `test` | `TC-06016` y `TC-06021` | `QG-11`: **0** escrituras aceptadas que reemplacen el texto original conservado, y **0** retiros parciales tras una baja interrumpida | 0 y 0 | **Bloqueante** |
| `test` | `TC-06030` | `QG-12`: **0** emisiones de acceso sin clave de firma, y **0** claves generadas al vuelo | 0 y 0 | **Bloqueante** |
| `test` | `TC-06034` y `TC-06035`, comparación en las dos direcciones | `QG-13`: **100 %** de las **17** condiciones del catálogo alcanzadas, **0** emitidas fuera de él, y **0** mensajes o trazas con un secreto, la ruta del almacén o el texto del alumno | 17, 0 y 0 | **Bloqueante** |
| `test` | Recolector de cobertura, con informe **por componente** | `QG-05`: **85 %** de líneas y **80 %** de ramas | 85 / 80 **[ASUNCIÓN del intake §17.3.P.6, asunción `A-3` de §22]** | **Condicionado** |
| `test` | Informe de cobertura **acotado a los dos motores** | `QG-06`: **95 %** de líneas en el validador de figuras | 95 **[ASUNCIÓN del mismo origen]** | **Condicionado** |
| `test` | `TC-06015` | `QG-14`: la interpretación del texto de **3** piezas de `E-1` termina en menos de **200 ms**, medida **sin almacén** | 200 ms **[ASUNCIÓN del intake §17.3.P.10, asunción `A-5` de §22]** | **Condicionado** |
| **`verificar-transformaciones`** | El stage propio de este proyecto de código, y `TC-06032` | `QG-04`: **las transformaciones de esquema se aplican solas sobre un almacén inexistente**, sin paso manual | 0 pasos manuales | **Bloqueante.** Criterio de aceptación de la etapa `c`. Ver §2.2 |

**Catorce gates, y ninguno movido de lugar.** Los que el intake §17.3.P.8 declara son **`QG-01`, `QG-03`, `QG-04` y `QG-05`** —construcción en cero sin advertencias, las **diez** pruebas del validador, las transformaciones aplicadas solas sobre un almacén inexistente y la cobertura de los mínimos de P.6—, que **no son los cuatro primeros de la lista**: los demás salen de una fila de `05` §8, que declara los **catorce** NFR de este proyecto de código. Se enumeran por identificador y no por posición, porque el orden de `Estrategia-Calidad.md` §3 no es el de la fuente.

### 2.2 El cuarto stage, que es propio de este proyecto de código

`QG-04` no se verifica leyendo nada: **se verifica arrancando contra un almacén que no existe y comprobando que aparece completo, sin que nadie ejecute un paso aparte**. Por eso tiene stage propio y no cabe dentro de `test`.

| Aspecto | Decisión | Fundamento |
| --- | --- | --- |
| Qué corre | La aplicación de las transformaciones de esquema **sobre un almacén inexistente**, y la comprobación de que quedó completo | Intake §17.3.P.8, criterio de aceptación de la etapa `c` |
| Cuándo corre | Después de `test`, y **antes** de que la canalización de `GeometriaFactory-Api` construya la imagen | `05` §5, orden de las etapas del pipeline; intake §17.5.P.8, fila de imagen |
| Sobre qué corre | Sobre un almacén **desechable** creado por la propia ejecución. **No sobre el almacén de nadie** | Decisión de esta categoría |
| Qué **no** es este stage | **No es el guion de restablecimiento.** `05` §5 declara que ese guion reproduce el estado de primer arranque y que **no es un camino de producción**: reproduce un almacén vacío | `05` §5, fila de reversión |
| Qué relación tiene con `PT-04` | Es su mitad barata. `PT-04` exige que **la imagen** arranque, aplique las transformaciones sobre un almacén vacío y responda salud; este stage verifica la parte de las transformaciones **sin construir la imagen**, y por eso un fallo se ve antes | Intake §17.5.P.8; [`../08-Calidad-Y-Pruebas/README.md`](../08-Calidad-Y-Pruebas/README.md) §4, que asigna `PT-04` a la etapa `a` de este proyecto de código |

**La última fila es la razón de ser del stage.** `PT-04` se mide sobre la imagen del backend, que es cara de construir; este stage mide la parte que más se rompe —una transformación nueva que no cierra sobre un almacén vacío— **en el punto más barato de la cadena**. Un fallo acá evita construir una imagen que no iba a arrancar.

**Y una consecuencia sobre el linaje que esta categoría no decide sino que hereda.** [`ADR-06007`](../05-Arquitectura-Tecnica/Adrs/ADR-06007-Transformaciones-Al-Arrancar-Con-Linaje-Inmutable.md) fija que el linaje de transformaciones es inmutable, y el intake §17.3.P.7 lo dice operativamente: **cada transformación se versiona con el código de su etapa y no se edita una ya fusionada**. Este stage lo hace visible: una transformación editada después de fusionada produce un linaje distinto del que ya se aplicó en cualquier almacén existente.

### 2.3 Tres condicionados y uno que no lo es

**`QG-05`, `QG-06` y `QG-14` son condicionados**, y no lo decide esta categoría: `Estrategia-Calidad.md` §3.1 lo declara, con `A-3` para las dos coberturas y `A-5` para los 200 ms. **Condicionado no es opcional**: la medición se hace, el número se registra en el informe de cierre, y lo que queda en suspenso es la consecuencia automática. `BT-06023` —«Confirmar los valores rotulados como asunción y fijar las tres puertas de cobertura», etapa `d`— es la tarea que los eleva.

**`QG-07` no es condicionado, y confundirlo sería el error característico de esta tabla.** También lleva un número —**0.01**— pero el intake §22 lo enumera expresamente entre **«lo que NO es asunción»**, con su fundamento: sale de que el emisor redondea a dos decimales. Y hay más: el intake §17.3.P.10 declara que el operador es **estricto** —se emite advertencia cuando la diferencia absoluta es **mayor** que 0.01, no mayor o igual— y da el motivo verificable, que es el caso testigo del producto: en el escenario `E-1` el área del cilindro declara 113.10 y la suma de sus componentes da 113.09, con una diferencia de **exactamente 0.01**; con el operador estricto ese caso **no** produce advertencia y el escenario da las **dos** que §20.E-1 declara, y con «mayor o igual» daría **tres**.

**La consecuencia para el pipeline es concreta y vale escribirla**: un gate condicionado sobre `QG-07` haría que el caso de prueba canónico del producto pudiera fallar sin detener nada. El pipeline **lo bloquea**.

### 2.4 Los stages del catálogo que no existen acá

| Stage del catálogo | Estado acá | Motivo |
| --- | --- | --- |
| Lint | **Incorporado en `build`** | El criterio es «en 0 y sin advertencias» (`QG-01`), y ninguna fuente declara un linter separado. La elección concreta de las reglas de análisis y su anclaje de versión son de la etapa `a` |
| SCA | **Existe, y acá sí tiene sujeto** | Es el único de los cinco proyectos de código que no se despliegan **con dependencias externas reales**, y dos son sensibles. Ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §4 |
| SBOM | **No se genera acá** | No hay artefacto publicado del que emitir inventario. El que importa es el de la unidad desplegable del servidor propio, que es la que sale del repositorio, y **este proyecto de código aporta a ese inventario la mayor parte de sus dependencias externas**. Ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §1 |
| Firma | **No se firma acá** | No hay canal ni integrador externo: `redistribuible` es false. **No confundir con la emisión de accesos firmados**, que es una capacidad del producto y no una firma de artefacto: ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §2 |
| Publish | **No existe** | Intake §17.3.P.7, idéntico a §17.1.P.7: sin publicación en feed. `05` §5 lo repite en su última fila |

## 3. Triggers

| Evento | Qué corre | Qué bloquea |
| --- | --- | --- |
| Confirmación empujada a la rama de una etapa | Los cuatro stages completos | Nada por sí solo |
| Apertura o actualización del pull request de la etapa | Los cuatro stages | **La fusión**, por los gates bloqueantes de §2.1 |
| **Pull request que agrega o cambia una transformación de esquema** | Todo lo anterior, con el stage `verificar-transformaciones` **sobre un almacén inexistente y sobre el linaje completo** | La fusión. Es la cadencia propia de este proyecto de código: una transformación que no cierra sobre un almacén vacío no se detecta ejecutando la batería |
| Pull request que **toca el validador de figuras** | Todo lo anterior, con `QG-03` sobre los **diez** casos y `QG-07` sobre el caso testigo | La fusión |
| Fusión a la rama principal | Los cuatro stages sobre el estado fusionado | El cierre de la etapa |
| Etiqueta de cierre de etapa | Los cuatro stages sobre el estado etiquetado | La declaración de etapa cerrada |

**No hay trigger por calendario.** El intake §10 declara «sin plazo; el avance se mide por etapas cerradas».

**Y no hay trigger de respaldo, aunque el respaldo exista como preocupación declarada.** El intake §17.3.P.4 declara la copia del archivo del almacén con el diario activo y su **frecuencia «a definir por el docente»**; `PA-07` de `05` §11 lo registra como punto abierto y lo dirige a esta categoría. **Esta categoría no inventa una frecuencia**: ver [`Entornos-Deploy.md`](Entornos-Deploy.md) §4.

## 4. Matriz de sistema operativo y plataforma

**Una sola combinación, y es una decisión declarada y no una carencia.**

| Trigger | Sistema operativo | Plataforma objetivo |
| --- | --- | --- |
| Todos los de §3 | El del contenedor de desarrollo, que es el mismo del servidor del backend | `net10.0`, sin sufijo de plataforma |

Justificación, tomada del intake §17.3.P.9: `net10.0`, Linux —contenedor de desarrollo y servidor propio—, con el motor de almacenamiento **en su versión embebida por el proveedor de acceso a datos, anclada en la etapa `a`**.

**Contra una matriz cruzada con un segundo sistema operativo**: el único consumidor es `GeometriaFactory-Api`, y la única unidad desplegable donde termina embebido corre sobre el mismo sistema operativo. **Y hay un motivo más fuerte que el costo de minutos**: el motor de almacenamiento es un archivo único con un modo de diario declarado y **escritor único** (intake §17.3.P.4), de modo que una matriz con otro sistema de archivos probaría un comportamiento que el producto nunca va a tener en ejecución.

**Este proyecto de código no llega al front**, de modo que la puerta `PT-01.a` y una eventual bajada de la versión objetivo del front **no lo alcanzan** (intake §13, columna de dependencias).

## 5. Caché y artefactos

| Aspecto | Decisión | Fundamento |
| --- | --- | --- |
| Caché de dependencias | Caché del restaurador de paquetes, con llave derivada de los archivos de proyecto | Decisión de esta categoría |
| Invalidación | Al cambiar cualquier archivo de proyecto. **Sin expiración por tiempo** | Un plazo en días no lo da ninguna fuente |
| **Prohibición explícita de caché** | **El almacén del stage `verificar-transformaciones` no se cachea entre ejecuciones.** Se crea inexistente en cada una | `QG-04` mide la aplicación **sobre un almacén inexistente**; un almacén cacheado dejaría de ser inexistente y el gate mediría otra cosa |
| Artefacto del stage `build` | El ensamblado compilado, consumido en la misma ejecución por `test` y por `GeometriaFactory-Api` | Intake §13; `05` §5 |
| Artefacto del stage `test` | El **informe de cobertura por componente**, el **informe acotado a los dos motores**, la salida de la batería y el número de `QG-14` | `Estrategia-Calidad.md` §3, columnas de verificación de `QG-05`, `QG-06` y `QG-14` |
| Artefacto del stage `verificar-transformaciones` | El registro del linaje aplicado y la constancia de que **no hubo paso manual** | `QG-04` |
| Artefactos de inspección | Los **recuentos** de `QG-08` a `QG-13`, cada uno con la condición en que se midió | `Estrategia-Calidad.md` §3 |
| Retención | Mientras dure el punto de control de la etapa; se adjuntan al **informe de cierre** | Intake §15, regla de delivery 3 |

**Los dos informes de cobertura son dos y no uno**, y no es redundancia: `QG-05` mide el conjunto del proyecto de código y `QG-06` mide **sólo los dos motores**, con un piso más alto. Un informe único no permitiría verificar el segundo, que es donde el intake §17.3.P.6 puso el número más alto del producto porque es el criterio que más veces se rompe.

## 6. Promoción

**No hay canales entre los que promover.** El modelo de `Rules-Devops.md` §2.2 para el tipo `library` es un par de canales sobre un feed único; acá **no hay feed**. Ver [`Entornos-Deploy.md`](Entornos-Deploy.md) §1.

| Transición | Trigger | Prerrequisitos | Aprobador |
| --- | --- | --- | --- |
| Rama de etapa → rama principal | Fusión del pull request de la etapa | Los gates bloqueantes de §2.1 en verde, y los **once** criterios de salida de [`../08-Calidad-Y-Pruebas/Plan-Pruebas.md`](../08-Calidad-Y-Pruebas/Plan-Pruebas.md) §3 | El Product Owner, con **OK explícito** en el punto de control (intake §15) |
| Etapa fusionada → etapa cerrada | Etiqueta al fusionar | La Definition of Done §1.3 entera, incluida la constancia de la medición de los **tres** criterios condicionados | El mismo, con constancia escrita en el informe de cierre |

**El aprobador humano no es un agregado de esta categoría**: el intake §15 declara el punto de control bloqueante y `equipo_n` es 1.

## 7. Reversión

| Situación | Procedimiento | Fundamento |
| --- | --- | --- |
| Una etapa fusionada rompe algo que estaba en verde | Volver a la **etiqueta de la etapa anterior** | `05` §5; intake §17.3.P.7 |
| Una transformación de esquema quedó mal | **No se edita la transformación ya fusionada.** Se agrega una nueva que corrija, versionada con el código de su etapa | Intake §17.3.P.7; [`ADR-06007`](../05-Arquitectura-Tecnica/Adrs/ADR-06007-Transformaciones-Al-Arrancar-Con-Linaje-Inmutable.md) |
| Hace falta reproducir el estado de primer arranque en desarrollo | El guion de restablecimiento que el intake §17.3.P.8 declara. **`05` §5 advierte que no es un camino de producción**: reproduce un almacén **vacío** | `05` §5, fila de reversión |
| Un cambio de esta biblioteca rompe la compilación de `GeometriaFactory-Api` | La rotura aparece en la construcción del consumidor, **antes de cualquier despliegue** | Intake §13, columna de dependencias |

**La tercera fila es la que más fácil se lee mal, y por eso la fuente ya la marcó.** El guion de restablecimiento **no restaura datos**: los borra y deja el almacén como en el primer arranque. Usarlo en el servidor propio para «arreglar» un problema haría desaparecer el trabajo de la comisión.

**No hay delist, no hay retiro de versión y no hay ventana de gracia**, porque no hay artefacto publicado que retirar.

## 8. Notificaciones

| Canal | Qué comunica | Fundamento |
| --- | --- | --- |
| La salida del pipeline sobre el pull request de la etapa | El resultado de los cuatro stages, gate por gate, con los **recuentos** de las inspecciones y el número de los tres condicionados | El pull request **es** el punto de control (intake §15) |
| El registro del stage `verificar-transformaciones` | El linaje aplicado y la constancia de que no hubo paso manual | `QG-04` |
| El informe de cierre de la etapa | La medición de `QG-05`, `QG-06` y `QG-14` con su distancia al umbral, y la constancia de los gates bloqueantes | Definition of Done §1.3 |
| El registro de cambios del producto | Toda fila de cambio mayor de esta biblioteca | [`Estrategia-Versionado.md`](Estrategia-Versionado.md) §6 |

**No se declara ningún canal de mensajería ni ningún tablero**: ninguna fuente lo declara y `equipo_n` es 1.

**Y una regla de notificación propia de este proyecto de código**: `QG-13` mide **0** mensajes o trazas con un secreto, la ruta del almacén o el texto del alumno. **Eso alcanza también a la salida del pipeline**: un registro de ejecución que imprimiera la ruta del almacén de prueba o un fragmento del texto de un escenario estaría produciendo, en la canalización, exactamente lo que el gate prohíbe en el producto.

## 9. Qué aporta a la canalización de la unidad desplegable del servidor propio

| Aporte | Efecto sobre la canalización de la unidad desplegable | Fundamento |
| --- | --- | --- |
| Es **nivel topológico 2** | Se construye después de `GeometriaFactory-Domain` y de `GeometriaFactory-Application`, y **antes** de `GeometriaFactory-Api` | Intake §13, orden topológico |
| Su stage `verificar-transformaciones` corre **antes** de que la imagen se construya | Es la mitad barata de `PT-04`: un linaje que no cierra sobre un almacén vacío se detecta **sin construir la imagen** | §2.2 |
| Aporta **la mayor parte de las dependencias externas** del artefacto del servidor propio | El inventario de esa unidad depende de lo que se ancle acá, incluidas las **dos** bibliotecas sensibles | Intake §17.3.P.1; [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §1 |
| **Recibe la clave de firma, no la busca** | La canalización de la unidad desplegable tiene que proveerla por variable de entorno o archivo montado; **si no llega, el arranque falla con su condición declarada** en lugar de generar una al vuelo | `05` §5, fila de secretos; `QG-12` |
| El almacén va a un **volumen persistente y nunca dentro de la imagen** | Es una restricción sobre cómo se arma esa imagen, y no sobre este ensamblado | Intake §17.3.P.4; `05` §5 |
| **No llega al front** | Un cambio suyo no obliga a republicar el front | Intake §13 |

## 10. Puntos abiertos

| Id | Punto abierto | Quién lo cierra | Cuándo |
| --- | --- | --- | --- |
| PD-01 | La **herramienta concreta** de cada stage —ejecutor de pruebas, recolector de cobertura, reglas de análisis estático y la herramienta de transformaciones como herramienta local del repositorio— y su anclaje de versión | El equipo, en el punto de control de la etapa `a` | Etapa `a`, por la regla de anclaje de versiones del intake |
| PD-02 | La **confirmación de los tres valores rotulados [ASUNCIÓN]** que hoy dejan condicionados a `QG-05`, `QG-06` y `QG-14`. Confirmados, los tres pasan a bloqueantes sin ningún otro cambio de este documento | El Product Owner, sobre el intake §22, por `BT-06023` | Al cerrar la etapa `d` |
| PD-03 | **Cuál de las dos funciones de derivación de clave se ancla, y con qué parámetros.** El intake §17.3.P.1 declara «PBKDF2 o Argon2» y **no elige**; `PA-03` de `05` §11 deja la forma y el criterio fijados por `ADR-06004` y la elección concreta en la regla de anclaje. **Esta categoría no la elige**, y declara su efecto de canalización: es una dependencia externa del artefacto del servidor propio y entra en su inventario | El equipo, en la etapa `a` | Etapa `a` |
| PD-04 | La **frecuencia del respaldo del almacén**, que el intake §17.3.P.4 declara «a definir por el docente» y `PA-07` de `05` §11 dirige a esta categoría. **No se inventa un número**: ver [`Entornos-Deploy.md`](Entornos-Deploy.md) §4 | El Product Owner | Sin fecha comprometida |
| PD-05 | **El ADR que `Rules-Devops.md` §2.2 pide para apartarse del modelo de canales `preview` / `stable`, y que este proyecto de código no tiene.** Las otras tres bibliotecas lo anclan en su `ADR-06003`; las siete ADR de acá no cubren publicación ni canales, de modo que el apartamiento de [`Entornos-Deploy.md`](Entornos-Deploy.md) §1.1 se apoya hoy en el intake §17.3.P.7 y §13 y no en el instrumento que la regla nombra. **El apartamiento no está en duda; falta el instrumento** | La categoría 05 de este proyecto de código, emitiendo la ADR correspondiente | Antes de la próxima emisión de la categoría 05 |

## 11. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara los **cuatro** stages que el intake §17.3.P.8 y `05` §5 fijan —`restore`, `build`, `test` y **`verificar-transformaciones`**, este último propio de este proyecto de código— y los **catorce** quality gates de `08` §3 materializados uno por uno, respetando su carácter: **once bloqueantes** y **tres condicionados** por depender de valores rotulados [ASUNCIÓN] en el intake §22. Declara por qué el cuarto stage no cabe dentro de `test`, y que es **la mitad barata de `PT-04`**. Declara que **`QG-07` no es condicionado** aunque lleve número, con el caso testigo de `E-1` —113.10 contra 113.09, diferencia de exactamente 0.01— y la consecuencia de que condicionarlo dejaría al caso canónico del producto fallando sin detener nada. Declara la **prohibición de cachear el almacén** del cuarto stage, los **dos** informes de cobertura y por qué son dos, la reversión con la advertencia de que el guion de restablecimiento **no es camino de producción**, y una regla de notificación propia: `QG-13` alcanza también a la salida del pipeline. **Cuatro** puntos abiertos, incluida la frecuencia de respaldo, que **no se inventa**. |
| 1.1 | 2026-08-11 | **Corrección de un hallazgo de la auditoría `F-09-Devops-Siete-Proyectos-r1.md` y registro de otro.** **`H-03` (P3):** §2.2 atribuía a «los cuatro primeros» gates de la lista de `Estrategia-Calidad.md` §3 la procedencia del intake §17.3.P.8; contados contra la fuente, los que §17.3.P.8 declara son **`QG-01`, `QG-03`, `QG-04` y `QG-05`**, y ahora se enumeran por identificador. **`H-04` (P3):** se abre **`PD-05`** en §10 por la ausencia de la ADR que `Rules-Devops.md` §2.2 exige para el apartamiento del modelo de canales, con la categoría 05 como dueña; los puntos abiertos de esta categoría pasan de **cuatro** a **cinco**. Trazabilidad upstream del intake a **1.22**, cuyas dos decisiones de §17.6.P.7 no alcanzan a este proyecto de código, que no es unidad desplegable. |
