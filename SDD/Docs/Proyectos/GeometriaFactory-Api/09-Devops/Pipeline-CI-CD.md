# Pipeline CI/CD — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** Pipeline-CI-CD.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero DevOps Senior + Platform Engineer (AG-09)
**Tipo de proyecto de código (D8):** `rest-api` · **Proyecto de código principal del producto**
**Trazabilidad upstream:** [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) 1.1 §3, §3.1, §3.2 y §3.3 (los **quince** quality gates, las **dos** puertas técnicas y la frontera del despliegue); [`../08-Calidad-Y-Pruebas/Plan-Pruebas.md`](../08-Calidad-Y-Pruebas/Plan-Pruebas.md) 1.1 §3 (los **doce** criterios de salida); [`../08-Calidad-Y-Pruebas/Definition-Of-Done.md`](../08-Calidad-Y-Pruebas/Definition-Of-Done.md) 1.1 §1.3 y §1.4; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §5, §8, §9 y §11; [`../05-Arquitectura-Tecnica/Adrs/ADR-07-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md`](../05-Arquitectura-Tecnica/Adrs/ADR-07-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md) 1.0; [`../05-Arquitectura-Tecnica/Adrs/ADR-08-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md`](../05-Arquitectura-Tecnica/Adrs/ADR-08-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) 1.0; [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md) (`BT-04`, `BT-06`, `BT-12`, `BT-22`, `BT-25`); [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.22** §10, §13, §14, §15, §16, §17.4.P.3, §17.5.P.1 a §17.5.P.12, §17.6.P.7 y §22
**Trazabilidad downstream:** [`Estrategia-Versionado.md`](Estrategia-Versionado.md), [`Entornos-Deploy.md`](Entornos-Deploy.md), [`Guia-Publicacion-Image-Docker.md`](Guia-Publicacion-Image-Docker.md), [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md); `10-Examples` y `11-Documentacion` cuando se emitan; `Producto/Pipeline-Producto.md`

---

## Tabla de contenido

- [1. Alcance, y la frontera que esta categoría no cruza](#1-alcance-y-la-frontera-que-esta-categoría-no-cruza)
- [2. Stages](#2-stages)
  - [2.1 Tabla de stages y gates](#21-tabla-de-stages-y-gates)
  - [2.2 Cuatro condicionados, y una decisión que no lo es](#22-cuatro-condicionados-y-una-decisión-que-no-lo-es)
  - [2.3 Los stages del catálogo, uno por uno](#23-los-stages-del-catálogo-uno-por-uno)
- [3. Triggers](#3-triggers)
- [4. Matriz de sistema operativo y plataforma](#4-matriz-de-sistema-operativo-y-plataforma)
- [5. Caché y artefactos](#5-caché-y-artefactos)
- [6. Promoción, y el despliegue conjunto](#6-promoción-y-el-despliegue-conjunto)
- [7. Reversión](#7-reversión)
- [8. Notificaciones](#8-notificaciones)
- [9. Las dos puertas técnicas dentro de la canalización](#9-las-dos-puertas-técnicas-dentro-de-la-canalización)
- [10. Puntos abiertos](#10-puntos-abiertos)
- [11. Control de cambios](#11-control-de-cambios)

---

## 1. Alcance, y la frontera que esta categoría no cruza

`GeometriaFactory-Api` es el **proyecto de código principal del producto** y **la unidad desplegable del servidor propio**: `05` §5 declara su unidad de despliegue como **una imagen de contenedor** que lleva embebidos los tres proyectos de código que referencia.

Y tiene una frontera que conviene declarar antes que la tabla de stages, porque ordena todo el documento: **el despliegue es manual y del Product Owner**. El intake §17.5.P.8 lo declara en la fila `despliegue` de su tabla de stages: «Manual, por el docente [DECISIÓN, RT §13]. El agente IA entrega el `Dockerfile` y el `compose.yaml` y no ejecuta el despliegue». [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §3.3 lo recoge: **ningún criterio de esa categoría se cumple ejecutando un despliegue**.

**Esta categoría hereda esa frontera sin moverla**, y la traduce a una regla de canalización:

| Qué automatiza esta canalización | Qué **no** automatiza |
| --- | --- |
| Construir, probar, medir cobertura y **construir la imagen y arrancarla** para comprobar que sirve | **Poner esa imagen en el servidor propio.** Eso es un acto del Product Owner, sobre su propia máquina |

**La consecuencia es que este pipeline termina en un artefacto verificado y no en un servicio corriendo.** Todo lo que dice sobre el despliegue —cómo llega el código al destino, cómo se resuelve la dirección, qué pasa cuando cambia— es **procedimiento documentado para quien lo ejecuta a mano**, y vive en [`Guia-Publicacion-Image-Docker.md`](Guia-Publicacion-Image-Docker.md) y en [`Entornos-Deploy.md`](Entornos-Deploy.md), no en un stage.

Lo que este pipeline ejecuta y bloquea son **los quince quality gates** de `Estrategia-Calidad.md` §3. **Esta categoría no los redefine, no los relaja y no agrega ninguno.**

## 2. Stages

Los stages son los **cinco** que el intake §17.5.P.8 declara en su tabla —`build`, `test`, cobertura, imagen y despliegue— y que `05` §5 repite en su fila de etapas del pipeline. **El quinto no lo ejecuta esta canalización**, por §1, y se declara igual porque la fuente lo enumera y porque su omisión silenciosa haría creer que el ciclo termina en la imagen.

### 2.1 Tabla de stages y gates

| Stage | Qué ejecuta | Gate que verifica | Umbral | Carácter |
| --- | --- | --- | --- | --- |
| `build` | `scripts/build.sh` | `QG-01`: termina en **0 y sin advertencias** | 0 advertencias | **Bloqueante** |
| `build` | `TC-28` y `TC-29`, sobre la composición de raíz | `QG-10`: **4 de 4** puertos conectados a su adaptador, **0** sin adaptador o con más de uno, y **1** sola configuración de intercambio declarada en el producto | 4, 0 y 1 | **Bloqueante, con fallo en construcción** cuando falta un puerto |
| `test` | `scripts/test.sh` | `QG-02`: la batería pasa entera, **incluida la del validador** | Batería entera | **Bloqueante** |
| `test` | `TC-07`, inspección en las dos direcciones | `QG-05`: exactamente **4** puntos de acceso fuera de la guardia de admisión, **ni uno más**, sobre los **quince** | 4 sobre 15 | **Bloqueante, sin gradación** |
| `test` | `TC-24` y `TC-27`, comparación en las dos direcciones | `QG-06`: **14 de 15** códigos del contrato con traducción declarada, **1** declarado sin destino con su motivo, **0** inventados y **0** renombrados | 14, 1, 0 y 0 | **Bloqueante** |
| `test` | `TC-25` | `QG-07`: **3 de 3** familias empobrecidas con respuestas **indistinguibles en cuerpo y en código** | 3 de 3 | **Bloqueante, sin gradación** |
| `test` | `TC-26` | `QG-08`: **0** respuestas que expongan dirección de servicio, ruta de datos, secreto o traza, sobre los **quince** puntos **y** sobre el registro del servidor | 0 | **Bloqueante.** Es `RA-03` |
| `test` | `TC-19` | `QG-09`: **0** caracteres de diferencia entre el texto enviado y el guardado, y **0** truncamientos silenciosos | 0 y 0 | **Bloqueante, sin gradación.** Rechazar, nunca truncar |
| `test` | `TC-31` | `QG-11`: **0** peticiones atendidas con la preparación del almacén incompleta | 0 | **Bloqueante** |
| `test` | `TC-20`, **forzando la petición** contra la superficie | `QG-12`: **0** eliminaciones fuera de alcance aceptadas | 0 | **Bloqueante.** Es el único criterio de verificación del producto que la fuente exige ejercer **forzando la petición** |
| `cobertura` | Recolector de cobertura, con informe **por componente** | `QG-03`: **75 %** de líneas y **70 %** de ramas | 75 / 70 **[ASUNCIÓN del intake §17.5.P.6, asunción `A-3` de §22]** | **Condicionado** |
| `cobertura` | Recuento de pruebas por clase en el informe (`TC-37`) | `QG-04`: pirámide de **60 %** integración y **40 %** unitarias | 60 / 40 **[ASUNCIÓN del mismo origen]** | **Condicionado**; la **inversión** no es asunción. Ver §2.2 |
| `imagen` | Construcción con `deploy/Dockerfile` **multietapa** y arranque desde el contenedor de desarrollo; `TC-33` | `QG-13`: el arranque en frío aplica las transformaciones y responde salud en menos de **30 segundos** | 30 s **[ASUNCIÓN del intake §17.5.P.10, asunción `A-5` de §22]** | **Condicionado** |
| `imagen` | La medición de la puerta técnica `PT-04` | **No es un gate de `08`**: es puerta técnica del producto. Ver §9 | La imagen se construye, arranca, aplica las transformaciones sobre un almacén vacío y **responde salud** | **Detiene la planificación** de lo que dependa de ella |
| `test` (batería de integración) | `TC-34` | `QG-14`: percentil 99 del listado por debajo de **500 ms** medido en el servidor, y caudal sostenido de **20 peticiones por minuto** | 500 ms y 20 por minuto **[ASUNCIÓN del intake §17.5.P.10]** | **Condicionado** |
| Cierre de la etapa que la incorpora | `TC-35` | `QG-15`: la colección de peticiones reproducible tiene **5 pasos o menos** y **0** datos de prueba inventados | 5 y 0 | **Bloqueante al cierre de esa etapa** |
| **`despliegue`** | **Nada, en esta canalización.** El acto es manual y del Product Owner | Ninguno de `08` se cumple ejecutándolo | — | **Fuera del alcance de la canalización.** Ver §1 y [`Guia-Publicacion-Image-Docker.md`](Guia-Publicacion-Image-Docker.md) |

**Quince gates, y ninguno movido de lugar.** Los que salen del intake §17.5.P.8 son **`QG-01`, `QG-02`, `QG-03` y `QG-13`**, uno por cada stage de su tabla que lleva un gate de `08` detrás —la quinta fila, `despliegue`, no lleva ninguno, y la de `imagen` lleva además la puerta técnica `PT-04`, que no es gate de `08`—. **`QG-04` sale de §17.5.P.6** y **`QG-05` de `05` §8 y de `RN-13`**; los demás salen de una fila de `05` §8, que declara los **diecisiete** NFR de este proyecto de código. Se enumeran por identificador y no por posición en la lista, porque el orden de `Estrategia-Calidad.md` §3 no es el de la fuente.

**`QG-05` merece una línea aparte, porque es el gate más caro de olvidar.** Su umbral no es «pocos puntos fuera de la guardia» sino **exactamente cuatro, ni uno más**, verificado **en las dos direcciones** sobre los quince. `05` §9 declara por qué: un punto nuevo fuera de la guardia hace que una regla del producto deje de valer **y nada falla**. Un pipeline que lo midiera en una sola dirección —comprobando que los cuatro conocidos siguen fuera— no detectaría el quinto.

### 2.2 Cuatro condicionados, y una decisión que no lo es

**`QG-03`, `QG-04`, `QG-13` y `QG-14` son condicionados**, y no lo decide esta categoría: `Estrategia-Calidad.md` §3.1 lo declara, con **`A-3` para la cobertura** —cuya celda de §22 enumera «90/85 en Domain, 85/80 en Application, 85/80 con 95 en el validador de Infrastructure, 75/70 en Api», y **no menciona la pirámide**—, **§17.5.P.6 para el reparto de la pirámide**, que es donde vive su rótulo, y `A-5` para el percentil, el caudal y el arranque en frío. **Condicionado no es opcional**: la medición se hace, el número se registra, y lo que queda en suspenso es la consecuencia automática. `BT-25` —«Confirmar los cinco valores rotulados como asunción», etapa `d`— es la tarea que los eleva.

**Lo rotulado en `QG-04` es el reparto numérico, no la inversión de la pirámide.** `Estrategia-Calidad.md` §3.1 lo precisa: el intake §17.5.P.6 declara la inversión **a propósito**, «porque lo que este proyecto de código aporta es cableado, y el cableado se verifica ejerciéndolo». **Esa decisión no es asunción y no queda en suspenso**, y tiene una consecuencia sobre esta canalización que conviene decir: **la mayor parte del costo de ejecución de este pipeline está en la batería de integración**, no en las unitarias, y eso es deliberado.

**Y una precisión sobre el reparto de esta ola.** La regla que la Fase E fijó y que esta cadena aplica en todo el producto es que una asunción **sobre el umbral mismo** condiciona, y una asunción **sobre la forma del gate** no. Acá los cuatro rótulos son sobre umbrales —porcentajes, milisegundos, peticiones por minuto—, de modo que los cuatro condicionan. Es el caso contrario al de `GeometriaFactory-Web`, donde la única marca era sobre la forma y el gate bloquea.

### 2.3 Los stages del catálogo, uno por uno

`Rules-Devops.md` §4.2 enumera siete stages obligatorios. **Éste es el proyecto de código del producto donde más de ellos tienen sujeto**, y conviene recorrerlos:

| Stage del catálogo | Estado acá | Motivo |
| --- | --- | --- |
| Lint | **Incorporado en `build`** | El criterio es «en 0 y sin advertencias» (`QG-01`); ninguna fuente declara un linter separado |
| Build | **Existe** | `scripts/build.sh` |
| Test | **Existe, y acá vive la batería de integración del producto** | El intake §17.5.P.6 declara que golpea la superficie real por su protocolo contra el almacén real. `Estrategia-Calidad.md` §1 agrega la consecuencia: **lo que acá se rompe no lo cubre ninguna otra batería del producto** |
| SCA | **Existe, y tiene sujeto**: la imagen final lleva un entorno de ejecución y las dependencias que los tres proyectos de código embebidos traen | Ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §4 |
| SBOM | **Existe como decisión de esta categoría** | Acá sí hay artefacto que sale del repositorio. Ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §1 |
| Firma | **No se firma, y la brecha se declara** | La imagen **no se publica en ningún registro**: se construye en destino desde el repositorio (intake §17.5.P.7). No hay artefacto en tránsito que firmar ni verificador que lo compruebe. Ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §2 |
| Publish | **No existe como publicación en un registro.** Lo que existe es la **construcción en destino**, y tiene documento propio | Intake §17.5.P.7; [`Guia-Publicacion-Image-Docker.md`](Guia-Publicacion-Image-Docker.md) |

## 3. Triggers

| Evento | Qué corre | Qué bloquea |
| --- | --- | --- |
| Confirmación empujada a la rama de una etapa | `build` → `test` → `cobertura` | Nada por sí solo |
| Apertura o actualización del pull request de la etapa | Lo anterior, **con la batería de integración completa** | **La fusión**, por los gates bloqueantes de §2.1 |
| **Pull request que agrega o cambia un punto de acceso** | Todo lo anterior, con `TC-07` **reejecutado en las dos direcciones** sobre los quince | La fusión. Es la cadencia propia de este proyecto de código, y `Estrategia-Calidad.md` §5 la declara **el control que más veces hay que ejercer** |
| Pull request que **cambia el ensamblado de contratos** | Todo lo anterior. Es donde se mide el `QG-05` de `GeometriaFactory-Contracts`, que exige **100 %** de los tipos de transferencia ejercitados por al menos una prueba de integración | La fusión, también para aquel proyecto de código |
| Fusión a la rama principal | Todo lo anterior, más el stage `imagen` | El cierre de la etapa |
| Etiqueta de cierre de etapa | Todo, sobre el estado etiquetado | La declaración de etapa cerrada, y **es lo que habilita el despliegue manual** |

**No hay trigger por calendario.** El intake §10 declara «sin plazo; el avance se mide por etapas cerradas».

**La cuarta fila es una obligación que este proyecto de código recibe de otro.** [`../../GeometriaFactory-Contracts/09-Devops/Pipeline-CI-CD.md`](../../GeometriaFactory-Contracts/09-Devops/Pipeline-CI-CD.md) §2.2 declara que su `QG-05` **no se puede correr desde aquel proyecto de código**, porque la batería que lo mide vive acá, y que mientras esta canalización no exista la prueba **se difiere por escrito** con las tres condiciones que ahí se declaran. **Desde que este proyecto de código existe, el diferimiento deja de ser admisible**: la batería está y el gate se mide.

## 4. Matriz de sistema operativo y plataforma

**Una sola combinación, y la fuente la declara como exclusiva.**

| Momento | Sistema operativo y plataforma | Fundamento |
| --- | --- | --- |
| Construcción | El del contenedor de desarrollo | Intake §17.5.P.9 y encabezado de la Parte C |
| Imagen de producción | El mismo, **con sólo el entorno de ejecución**: sin kit de desarrollo ni depurador, y **sin linaje con la imagen del contenedor de desarrollo** | Intake §17.5.P.9; `05` §5, fila de contenido de la imagen |
| Servidor propio | El mismo | Intake §17.5.P.9 |

Justificación, del intake §17.5.P.9: `net10.0`, **Linux exclusivamente**, porque contenedor de desarrollo, imagen de producción y servidor propio son los tres Linux. Una matriz cruzada no compraría cobertura de nadie: **el único consumidor de esta superficie es `GeometriaFactory-Web`, servidor a servidor**, y el navegador nunca la alcanza (`RA-01`).

**La fila del medio es una restricción de construcción y no un detalle de empaquetado.** «Sin linaje con la imagen del contenedor de desarrollo» significa que la imagen de producción **no se deriva** de la de desarrollo: se construye desde una base de ejecución propia. Un archivo de construcción que reusara la imagen de desarrollo llevaría el kit de desarrollo al servidor propio, y **el intake lo prohíbe explícitamente**.

**Y la asimetría con el front que la fuente declara**: si la puerta `PT-01.a` obliga a bajar la versión objetivo, se baja **la del front y no la del backend**, porque son dos artefactos independientes (intake §17.6.P.9). Esta canalización **no se toca por esa puerta**; la única precaución heredada es la de `GeometriaFactory-Contracts`, cuyo ensamblado tiene que seguir siendo cargable por los dos procesos.

## 5. Caché y artefactos

| Aspecto | Decisión | Fundamento |
| --- | --- | --- |
| Caché de dependencias | Caché del restaurador de paquetes, con llave derivada de los archivos de proyecto del artefacto de agrupación | Decisión de esta categoría |
| Invalidación | Al cambiar cualquier archivo de proyecto. **Sin expiración por tiempo** | Un plazo en días no lo da ninguna fuente |
| **Prohibición explícita de caché** | **El almacén de la batería de integración y el del stage `imagen` no se cachean.** Se crean vacíos en cada ejecución | `QG-11` mide **0** peticiones atendidas con la preparación del almacén incompleta, y `PT-04` exige aplicar las transformaciones **sobre un almacén vacío**. Un almacén cacheado dejaría de ser vacío |
| Artefacto del stage `build` | El ensamblado compilado, con los tres proyectos de código referenciados | Intake §13 |
| Artefacto del stage `test` | La salida de la batería, **con el recuento de pruebas por clase** para `QG-04`, y los recuentos de las inspecciones | `Estrategia-Calidad.md` §3 |
| Artefacto del stage `cobertura` | El informe **por componente** | El mismo, columna de verificación de `QG-03` |
| Artefacto del stage `imagen` | **La imagen**, más el registro de su arranque: transformaciones aplicadas, salud respondida y **el tiempo de arranque en frío** | `QG-13`; `PT-04` |
| Inventario de componentes | Emitido en el stage `imagen`, sobre lo que la imagen efectivamente lleva | Decisión de esta categoría. Ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §1 |
| **Retención de la imagen** | **Ninguna.** No se guarda: se construye para verificarla y **el despliegue la reconstruye en destino desde el repositorio** | Intake §17.5.P.7; ver §7 y [`Guia-Publicacion-Image-Docker.md`](Guia-Publicacion-Image-Docker.md) §2 |
| Retención del resto | Mientras dure el punto de control; se adjuntan al informe de cierre | Intake §15, regla de delivery 3 |

**La anteúltima fila es la más contraintuitiva de la tabla y por eso va escrita.** En una canalización convencional la imagen construida **es** el artefacto que se despliega. Acá no: el canal de entrega que el intake §17.5.P.7 declara es **construir en destino desde el repositorio, sin publicar en un registro**, de modo que la imagen que construye esta canalización **existe para ser verificada y después se descarta**. Guardarla sugeriría un camino de despliegue que no es el declarado.

## 6. Promoción, y el despliegue conjunto

| Transición | Trigger | Prerrequisitos | Aprobador |
| --- | --- | --- | --- |
| Rama de etapa → rama principal | Fusión del pull request de la etapa | Los gates bloqueantes de §2.1 en verde, y los **doce** criterios de salida de [`../08-Calidad-Y-Pruebas/Plan-Pruebas.md`](../08-Calidad-Y-Pruebas/Plan-Pruebas.md) §3 | El Product Owner, con **OK explícito** en el punto de control (intake §15) |
| Etapa fusionada → etapa cerrada | Etiqueta al fusionar | La Definition of Done §1.3 entera, y la constancia de la medición de los **cuatro** criterios condicionados | El mismo |
| **Etapa cerrada → artefacto entregado** | La etiqueta, más el stage `imagen` en verde | La Definition of Done §1.4 en sus **siete** puntos, incluido **`PT-04`** | El mismo, con la constancia de la entrega en el informe de cierre |
| **Artefacto entregado → servicio desplegado** | **Un acto manual del Product Owner**, fuera de esta canalización | Los pasos de [`Guia-Publicacion-Image-Docker.md`](Guia-Publicacion-Image-Docker.md) | El mismo, que es quien lo ejecuta |
| **Cambio incompatible del contrato → producto desplegado** | Sólo con **las dos unidades desplegables desplegadas desde el mismo estado del repositorio** | El `QG-08` de `GeometriaFactory-Contracts`, que bloquea la **publicación de la etapa** | El mismo, con constancia escrita |

**La tercera y la cuarta fila son dos y no una, y separarlas es lo que la frontera de §1 exige.** La Definition of Done §1.4 lo dice con precisión: **el artefacto queda entregado, no desplegado**. La canalización llega hasta la tercera.

**Sobre la quinta fila.** La obligación es del intake §17.4.P.3 y [`ADR-08`](../05-Arquitectura-Tecnica/Adrs/ADR-08-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) §2 la convierte en la regla operativa que **reemplaza al versionado de rutas**. Su tratamiento completo —incluidas las tres decisiones derivadas y el hallazgo de que el desfase de momentos es irreducible mientras un extremo se despliegue a mano— está en [`../../GeometriaFactory-Web/09-Devops/Pipeline-CI-CD.md`](../../GeometriaFactory-Web/09-Devops/Pipeline-CI-CD.md) §3.2, **y esta categoría lo adopta sin duplicarlo**. Lo que agrega desde este lado es la precisión sobre **el orden**, que el intake §17.6.P.7 fijó en 1.22: **primero el backend**. Ver §7.

## 7. Reversión

| Situación | Procedimiento | Fundamento |
| --- | --- | --- |
| Una etapa fusionada rompe algo que estaba en verde | Volver a la **etiqueta de la etapa anterior** | `05` §5, fila de reversión; intake §17.5.P.7 |
| El servicio desplegado está roto | **Volver a la etiqueta anterior y reconstruir en destino.** No hay imagen publicada a la que volver: el canal construye desde el repositorio | Intake §17.5.P.8; [`ADR-08`](../05-Arquitectura-Tecnica/Adrs/ADR-08-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) §6, trade-off 3 |
| Reemplazo de versión, en general | **Detener y arrancar, con ventana de indisponibilidad.** Sin proxy inverso no hay despliegue con solapamiento | Intake §17.5.P.8 y §17.5.P.12; `05` §5 |
| Un cambio incompatible del contrato llegó a las dos unidades | **Se revierten las dos juntas.** La reversión desacoplada reproduce `RI-02` de [`../../../Producto/Vista-Producto.md`](../../../Producto/Vista-Producto.md) §7 | [`../../GeometriaFactory-Contracts/09-Devops/Pipeline-CI-CD.md`](../../GeometriaFactory-Contracts/09-Devops/Pipeline-CI-CD.md) §7 |
| Una transformación de esquema quedó mal | **Volver a la etiqueta no deshace el esquema del almacén.** Se corrige con otra transformación | [`../../GeometriaFactory-Infrastructure/09-Devops/Estrategia-Versionado.md`](../../GeometriaFactory-Infrastructure/09-Devops/Estrategia-Versionado.md) §4 |

**La quinta fila es la asimetría más importante de la reversión de este producto, y no es de esta capa sino de la que embebe.** El código vuelve atrás; **el almacén no**. Cualquier procedimiento de reversión que suponga que volver a una etiqueta restituye el estado anterior **es falso para los datos**, y el único mecanismo declarado para eso es el respaldo, cuya frecuencia el intake dejó a definir por el docente.

**Y el orden del despliegue conjunto, que el Product Owner decidió en el intake 1.22.** Cuando hay que desplegar las dos unidades por un cambio incompatible, la de este proyecto de código **tiene ventana de indisponibilidad** y la del front no. Desplegar primero el front lo deja hablando con un servicio que todavía no cambió; desplegar primero el servicio deja al front viejo hablando con el servicio nuevo. **Las dos ventanas existen**, y el intake §17.6.P.7 elige la segunda: **primero el backend**, porque una API nueva normalmente acepta lo que mandaba el front anterior, mientras que un front nuevo contra una API vieja le pide algo que todavía no existe y el alumno ve el error. Lo que esta categoría agrega desde este lado sigue vigente: **el intervalo entre las dos se minimiza y se registra**, y la etapa no se cierra hasta que las dos salieron desde el mismo estado del repositorio. **El orden no vuelve automático el despliegue conjunto** —el front sale al fusionar y esta unidad se despliega a mano—, de modo que la coordinación sigue siendo un acto humano y el intervalo se minimiza en lugar de eliminarse; el propio intake 1.22 lo declara así. `PD-05` de §10 queda **cerrado**.

## 8. Notificaciones

| Canal | Qué comunica | Fundamento |
| --- | --- | --- |
| La salida del pipeline sobre el pull request de la etapa | El resultado de los stages, gate por gate, con los **recuentos** de las inspecciones y el número de los cuatro condicionados | El pull request **es** el punto de control (intake §15) |
| El registro del stage `imagen` | Las transformaciones aplicadas, la salud respondida y el **tiempo de arranque en frío** | `QG-13`; `PT-04` |
| El informe de cierre de la etapa | Lo anterior, más la constancia de la **entrega del artefacto** —el archivo de construcción y el de composición—, que es donde termina la responsabilidad del agente | Definition of Done §1.4 |
| El registro de cambios del producto | Toda fila de cambio mayor | [`Estrategia-Versionado.md`](Estrategia-Versionado.md) §6 |

**No se declara ningún canal de mensajería ni ningún tablero**: ninguna fuente lo declara y `equipo_n` es 1.

**Y una regla de notificación que este proyecto de código impone sobre su propia canalización.** `QG-08` mide **0** respuestas que expongan dirección de servicio, ruta de datos, secreto o traza, **sobre los quince puntos y sobre el registro del servidor**. Eso alcanza a la salida del pipeline: **un registro de ejecución que imprimiera la dirección del servidor propio, la ruta del almacén o la clave de firma estaría produciendo, en la canalización, exactamente lo que el gate prohíbe en el producto**. Es `RA-03` aplicada al lugar más fácil de olvidar.

## 9. Las dos puertas técnicas dentro de la canalización

`PT-04` y `PT-05` **no son criterios de esta categoría ni de la 08**: las declara el intake §15, y `Estrategia-Calidad.md` §3.3 las transcribe. Lo que le corresponde a 09 es declarar cómo se ejecutan dentro de la canalización:

| Puerta | Cuándo corre | Sobre qué | Qué pasa si no pasa |
| --- | --- | --- | --- |
| **`PT-04`** | Etapa `a`, con `BT-04` | Sobre **la imagen**, construida con el archivo de construcción multietapa y arrancada **desde el contenedor de desarrollo**: aplica las transformaciones sobre un almacén vacío y **responde salud** | **Detiene la planificación** de lo que dependa de ella. Sin ella, el artefacto del servidor propio no se puede construir ni arrancar |
| **`PT-05`** | Etapa `i`, **fuera del tramo comprometido** | Sobre el **despliegue real**: valida la premisa completa de la topología. Es la única puerta del producto que **no se puede medir sin desplegar** | El despliegue real. La fuente **recomienda no relegarla**, y el intake §15 registra que el 2026-08-08 su letra corrió de `h` a `i` al insertarse una etapa, **sin que la puerta se despegue del despliegue real** |

**`PT-04` se mide desde el contenedor de desarrollo y no en el servidor propio**, y eso es lo que la vuelve barata: verifica que el artefacto sirve **antes** de que exista la oportunidad de desplegarlo mal. Su mitad más frágil —que las transformaciones cierren sobre un almacén vacío— ya se verificó antes, en el stage `verificar-transformaciones` de `GeometriaFactory-Infrastructure`, que es la mitad barata de esta puerta.

**`PT-05` es la única puerta del producto que esta canalización no puede ejecutar ni preparar**, porque depende de un acto manual del Product Owner sobre su propia red. Lo que esta categoría hace es **documentar el procedimiento** para que esa medición sea reproducible: [`Guia-Publicacion-Image-Docker.md`](Guia-Publicacion-Image-Docker.md) §3.

## 10. Puntos abiertos

| Id | Punto abierto | Quién lo cierra | Cuándo |
| --- | --- | --- | --- |
| PD-01 | La **construcción de la imagen en destino desde el repositorio**, que el intake §17.5.P.11 punto 5 rotula **[A VERIFICAR]** y exige probar **una vez antes de depender del mecanismo**: requiere que el motor de contenedores del destino resuelva la referencia al repositorio y tenga credenciales si es privado. Es `PA-08` de `05` §11, que lo dirige a esta categoría **para medirlo, no para decidirlo** | El equipo, midiendo, con el procedimiento de [`Guia-Publicacion-Image-Docker.md`](Guia-Publicacion-Image-Docker.md) §2 | Antes de depender del mecanismo, y en todo caso antes del despliegue real |
| PD-02 | La **herramienta concreta** de cada stage —ejecutor de pruebas, recolector de cobertura, generador del inventario— y su anclaje de versión | El equipo, en el punto de control de la etapa `a` | Etapa `a` |
| PD-03 | La **confirmación de los valores rotulados [ASUNCIÓN]** que hoy dejan condicionados a `QG-03`, `QG-04`, `QG-13` y `QG-14`. Confirmados, los cuatro pasan a bloqueantes sin ningún otro cambio de este documento | El Product Owner, sobre el intake §22, por `BT-25` | Al cerrar la etapa `d` |
| PD-04 | La **vigencia exacta del acceso firmado**, que el intake declara «corta» sin fijar número y `ADR-03` toma de configuración. Alcanza a esta categoría porque **es un valor de configuración del ambiente**, no del código | El equipo en la etapa `a`, y el Product Owner si quisiera fijarlo. Es `PA-04` de `05` §11 | Etapa `a` |
| PD-05 | ~~El **orden entre los dos despliegues** ante un cambio incompatible del contrato. Ninguna fuente lo elige, y las dos alternativas dejan una ventana de desajuste. Esta categoría declara que **el intervalo se minimiza y se registra**, y no inventa un orden~~ · **CERRADO el 2026-08-11**: el intake **1.22** §17.6.P.7 elige **primero el backend**, sin volver automático el despliegue conjunto. Ver §7 | El Product Owner, que es quien ejecuta los dos actos | Cerrado con el intake 1.22 |

**`PA-08` de `05` §11 queda con procedimiento pero no cerrado**, y la distinción importa: esta categoría escribe **cómo se prueba** el mecanismo de construcción en destino, pero **no declara que funcione**. La fuente lo rotula **[A VERIFICAR]** y sólo la medición lo cierra.

## 11. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara la **frontera** que esta categoría no cruza —el despliegue es manual y del Product Owner— y su consecuencia: **la canalización termina en un artefacto verificado y no en un servicio corriendo**. Declara los **cinco** stages que el intake §17.5.P.8 enumera, con el quinto declarado **fuera del alcance de la canalización** en lugar de omitido, y los **quince** quality gates de `08` §3 materializados uno por uno: **once bloqueantes** y **cuatro condicionados**, los cuatro por rótulos sobre **umbrales**, con la precisión de que **la inversión de la pirámide no es asunción** y con la regla de reparto de esta ola declarada. Declara los siete stages del catálogo uno por uno, la matriz de una sola combinación con la restricción de **sin linaje con la imagen de desarrollo**, la **prohibición de cachear los almacenes**, y que **la imagen no se retiene** porque el despliegue la reconstruye en destino. Separa **artefacto entregado** de **servicio desplegado** como dos transiciones de promoción distintas. Declara la asimetría de la reversión —el código vuelve, el almacén no— y aporta, como `PD-05`, que **ninguna fuente elige el orden de los dos despliegues** y que el intervalo se minimiza y se registra. Declara las **dos** puertas técnicas dentro de la canalización y deja `PA-08` **con procedimiento y sin cerrar**. |
| 1.1 | 2026-08-11 | **Propagación del intake 1.22 y corrección de tres hallazgos de la auditoría `F-09-Devops-Siete-Proyectos-r1.md`.** **Propagación:** el intake **1.22** §17.6.P.7 decide que **cuando front y backend salen juntos sale primero el backend**, con lo que **`PD-05` queda cerrado** —conserva su fila con su desenlace y su fecha— y §6 y §7 pasan de «ninguna fuente elige el orden» a registrar el orden elegido, manteniendo declarado que el orden **no vuelve automático** el despliegue conjunto y que el intervalo se minimiza en vez de eliminarse. **`H-01` (P2):** la frase de §1 atribuida al intake §17.5.P.8 era una paráfrasis dentro de comillas angulares; se reemplaza por la **transcripción literal** de la fila `despliegue` de su tabla de stages. **`H-03` (P3):** §2.1 atribuía a §17.5.P.8 «los cinco primeros» gates de la lista; se enumera por identificador —`QG-01`, `QG-02`, `QG-03` y `QG-13` de §17.5.P.8, `QG-04` de §17.5.P.6 y `QG-05` de `05` §8 y `RN-13`—. **`H-05` (P3):** §2.2 adjudicaba a `A-3` de §22 la forma de la pirámide; `A-3` sólo enumera coberturas, y la pirámide se atribuye ahora a §17.5.P.6. Sube la trazabilidad upstream del intake de **1.21** a **1.22** y le agrega §17.6.P.7. |
