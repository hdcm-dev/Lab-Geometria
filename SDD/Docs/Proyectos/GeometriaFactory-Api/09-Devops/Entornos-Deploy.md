# Entornos y despliegue — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** Entornos-Deploy.md
**Versión:** 1.2
**Estado:** Aprobado
**Fecha:** 2026-08-12
**Autor:** Ingeniero DevOps Senior + Platform Engineer (AG-09)
**Tipo de proyecto de código (D8):** `rest-api` · **Proyecto de código principal del producto**
**Trazabilidad upstream:** [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §5, §8 y §11; [`../05-Arquitectura-Tecnica/Adrs/ADR-07-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md`](../05-Arquitectura-Tecnica/Adrs/ADR-07-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md) 1.0; [`../05-Arquitectura-Tecnica/Adrs/ADR-06-Composicion-De-Raiz-Ciclos-De-Vida-Y-Configuracion.md`](../05-Arquitectura-Tecnica/Adrs/ADR-06-Composicion-De-Raiz-Ciclos-De-Vida-Y-Configuracion.md) 1.0; [`../05-Arquitectura-Tecnica/Adrs/ADR-08-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md`](../05-Arquitectura-Tecnica/Adrs/ADR-08-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) 1.0; [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) 1.1 §3 y §3.3; [`../../GeometriaFactory-Web/09-Devops/Entornos-Deploy.md`](../../GeometriaFactory-Web/09-Devops/Entornos-Deploy.md) 1.1 §5; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.22** §10, §11, §13, §14, §16, §17.5.P.1, §17.5.P.3, §17.5.P.4, §17.5.P.5, §17.5.P.7 a §17.5.P.12 y §17.6.P.7
**Trazabilidad downstream:** [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md), [`Guia-Publicacion-Image-Docker.md`](Guia-Publicacion-Image-Docker.md), [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md); `Producto/Pipeline-Producto.md`

---

## Tabla de contenido

- [1. Ambientes de este proyecto de código](#1-ambientes-de-este-proyecto-de-código)
  - [1.1 Apartamiento declarado del modelo de la categoría](#11-apartamiento-declarado-del-modelo-de-la-categoría)
- [2. Provisión](#2-provisión)
- [3. Cómo llega el código al destino](#3-cómo-llega-el-código-al-destino)
- [4. La dirección dinámica, que es la restricción que ordena todo](#4-la-dirección-dinámica-que-es-la-restricción-que-ordena-todo)
  - [4.1 Cómo se resuelve la dirección](#41-cómo-se-resuelve-la-dirección)
  - [4.2 Qué pasa cuando la dirección cambia](#42-qué-pasa-cuando-la-dirección-cambia)
  - [4.3 Cómo el front la alcanza sin violar `RA-03`](#43-cómo-el-front-la-alcanza-sin-violar-ra-03)
- [5. Configuración](#5-configuración)
- [6. Secretos](#6-secretos)
- [7. Promoción](#7-promoción)
- [8. Control de cambios](#8-control-de-cambios)

---

## 1. Ambientes de este proyecto de código

**Dos, y son los que la fuente declara.** El intake §17.5.P.8 los nombra sin rodeos: «desarrollo (devcontainer) y producción (servidor propio)».

| Ambiente | Destino | Quién lo aprueba y lo opera | Ventana o acuerdo de nivel de servicio |
| --- | --- | --- | --- |
| **Desarrollo** | El contenedor de desarrollo, en la máquina de quien construye. Ahí se ejecuta el servicio, se corre la batería de integración y se construye y arranca la imagen para medir `PT-04` | Nadie: no hay promoción hacia él | No aplica. En desarrollo **escucha sin certificado**, para evitar la fricción del certificado de confianza dentro del contenedor (intake §17.5.P.1) |
| **Producción** | El servidor domiciliario, con la imagen construida en destino y el almacén en un **volumen persistente** | El **Product Owner**, que además es quien ejecuta el despliegue a mano | **Sin acuerdo de disponibilidad.** El intake §17.5.P.10 declara «disponibilidad: sin SLO», y **cada reemplazo de versión tiene ventana de indisponibilidad** |

**La segunda fila no declara un SLO y no es un olvido.** El intake §17.5.P.10 lo declara expresamente: el servidor es domiciliario, su caída es el riesgo `R-08` y **se responde con estado degradado en el front, no con redundancia**. El intake §11 lo registra desde el negocio como `RN-B4`, con la mitigación declarada —estado degradado explícito— y con la frase que cierra la discusión: **no hay alta disponibilidad ni la va a haber, porque es un laboratorio de aula**.

### 1.1 Apartamiento declarado del modelo de la categoría

`Rules-Devops.md` §2.2 fija para el tipo `rest-api` el modelo `DEV` / `QA` / `STAGING` / `PROD` **más canario**, y admite agregar ambientes pero **no quitar ninguno sin un ADR que lo justifique**. Acá hay **dos** y no cuatro, y **no hay canario**. El apartamiento se declara con sus fundamentos, todos verificables:

| Qué falta respecto del modelo | Fundamento del apartamiento | Dónde se verifica |
| --- | --- | --- |
| `QA` y `STAGING` | **Un solo servidor y presupuesto declarado cero.** El intake §10 enumera las tres piezas de infraestructura de costo cero, y el servidor domiciliario es una. Montar dos ambientes más exigiría dos servidores más o dos instancias en el mismo, con dos almacenes que nadie mantiene | Intake §10 |
| Despliegue **canario** | **No hay proxy inverso, y sin él no hay despliegue con solapamiento.** El intake §17.5.P.8 lo declara: el reemplazo de versión es **detener y arrancar**, con ventana de indisponibilidad. Un canario requiere dos versiones vivas a la vez y un repartidor de tráfico que el producto no tiene | Intake §17.5.P.8 y §17.5.P.12 |
| Despliegue **azul-verde** | Lo mismo, y con un agravante: el almacén es **un archivo único con escritor único**. Dos versiones vivas escribirían sobre el mismo archivo | Intake §17.3.P.4, por la vía de `GeometriaFactory-Infrastructure`; `05` §5 |

**El ADR que sostiene el apartamiento es [`ADR-08`](../05-Arquitectura-Tecnica/Adrs/ADR-08-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md)**, que en su §6 acepta por escrito los cuatro trade-offs que implica, entre ellos el despliegue conjunto con su ventana de indisponibilidad y que **la reversión sea reconstruir desde una etiqueta, sin imagen publicada a la que volver**.

**Lo que el apartamiento cuesta, declarado en lugar de disimulado.** Sin `STAGING`, **la primera vez que una versión corre contra el almacén real es en producción**; y sin canario, **un despliegue malo afecta al 100 % de las peticiones desde el primer segundo**. Lo que el producto pone en su lugar son tres cosas, y ninguna es un ambiente: **el arranque en dos fases**, que hace que un servicio que no puede confiar en su almacén **no escuche** ([`ADR-07`](../05-Arquitectura-Tecnica/Adrs/ADR-07-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md)); la puerta `PT-04`, que ejercita el arranque completo **antes** de que exista la oportunidad de desplegar; y el **estado degradado** del front cuando el servicio no responde.

## 2. Provisión

**No hay infraestructura declarativa en el sentido del catálogo, y lo que hay está versionado.**

| Aspecto | Decisión | Fundamento |
| --- | --- | --- |
| Herramienta declarativa de infraestructura | **Ninguna.** No hay nube que provisionar: el servidor domiciliario ya existe y el intake §10 lo cuenta entre las tres piezas de costo cero | Intake §10 |
| Qué sí vive en el repositorio | **`deploy/Dockerfile`**, multietapa, y **`deploy/compose.yaml`**, «despliegue desde Git en destino». Son las dos piezas que el agente entrega | Intake §16, árbol del repositorio |
| Volumen del almacén | **Persistente, declarado en el archivo de composición, y nunca dentro de la imagen** | Intake §17.5.P.4 y §17.3.P.4; Definition of Done §1.4 |
| Punto de entrada | **Un puerto publicado hacia el enrutador, y es el único punto de entrada al servidor propio.** Todo lo que este proyecto de código no exponga, **no existe para nadie de afuera** | `05` §5, fila de punto de entrada |
| Comprobación de salud del contenedor | Declarada en el archivo de composición, contra el punto de salud del servicio | Intake §17.5.P.3, fila de salud |

**La cuarta fila es una decisión de seguridad y no de red.** Un único puerto publicado significa que la superficie expuesta del servidor domiciliario **es exactamente la que `05` §3.4 enumera**, y por eso el `QG-05` de la canalización —**4** puntos fuera de la guardia sobre **15**, ni uno más— es también un gate de exposición: cada punto nuevo es superficie nueva alcanzable desde afuera.

## 3. Cómo llega el código al destino

El canal de entrega que el intake §17.5.P.7 declara es **la imagen construida en destino desde el repositorio con el archivo de composición, sin publicar en ningún registro**. Es una decisión de la fuente y esta categoría no la reabre; lo que hace es escribir qué implica.

| Qué exige el mecanismo | Estado | Fundamento |
| --- | --- | --- |
| Que el **motor de contenedores del destino resuelva la referencia al repositorio**, y tenga credenciales si el repositorio es privado | **[A VERIFICAR]** en la fuente. El intake §17.5.P.11 punto 5 exige **probarlo una vez antes de depender del mecanismo** | Intake §17.5.P.11; `PA-08` de `05` §11, que lo dirige a esta categoría **para medirlo** |
| Que el destino tenga **acceso a la red** para traer el código y las dependencias en el momento de construir | Consecuencia directa del mecanismo, declarada acá | **Decisión de esta categoría**, declarada como tal |
| Que la construcción **no requiera el kit de desarrollo instalado en el anfitrión** | Cumplido por el archivo de construcción **multietapa**: la etapa de construcción vive dentro de la imagen intermedia | Intake §17.5.P.9; `05` §5 |
| Que la imagen final **no tenga linaje con la del contenedor de desarrollo** | Cumplido por diseño del archivo de construcción, y verificable por inspección | Intake §17.5.P.9; Definition of Done §1.4 |

**La segunda fila es lo que esta categoría agrega, y va declarada como decisión propia porque ninguna fuente la enuncia.** Construir en destino significa que **el servidor domiciliario necesita salida a la red en el momento del despliegue**, y que un corte de internet no sólo deja el servicio inalcanzable sino que **impide desplegar**. Es distinto de un modelo con registro, donde la imagen ya construida podría estar en la máquina. Se declara para que quien despliegue lo sepa, no para cambiar la decisión.

**Y una consecuencia sobre la reversión que se sigue de lo mismo**: como no hay imagen publicada, **la reversión es volver a la etiqueta anterior y reconstruir**, y también necesita red. Está en [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §7 y [`ADR-08`](../05-Arquitectura-Tecnica/Adrs/ADR-08-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) §6 lo acepta por escrito.

**El procedimiento paso a paso, con su verificación, vive en [`Guia-Publicacion-Image-Docker.md`](Guia-Publicacion-Image-Docker.md)**, que es donde `Rules-Devops.md` §4.5 lo pide. Acá queda la política; ahí, el procedimiento.

## 4. La dirección dinámica, que es la restricción que ordena todo

El intake §10 la declara entre las restricciones del cliente: **el servidor domiciliario no tiene dirección fija**, se admite apuntar a la dirección directa por decisión del Product Owner —«la IP dinámica realmente no cambia tanto»— y el servicio de nombres dinámico queda como **recomendación**. Y §14 declara la restricción hermana que la hace crítica: **la red de la facultad bloquea el acceso a direcciones dinámicas**, que es lo que ordena la topología entera y la razón por la que el front vive en un hosting público.

**Las dos juntas producen la propiedad que esta sección tiene que sostener**: la dirección del servidor propio **la usa un solo cliente, el front, servidor a servidor** — y **nunca el navegador** (`RA-01`).

### 4.1 Cómo se resuelve la dirección

| Aspecto | Decisión | Fundamento |
| --- | --- | --- |
| Quién conoce la dirección | **Únicamente el proceso del front**, y dentro de él, únicamente su cliente tipado del servicio de datos | Intake §17.6.P.3; `ADR-07` de `GeometriaFactory-Web` §7 |
| Dónde vive el valor | Como **secreto del repositorio** del front, inyectado al publicar. **La dirección real no se versiona** | Intake §17.6.P.5; [`../../GeometriaFactory-Web/09-Devops/Entornos-Deploy.md`](../../GeometriaFactory-Web/09-Devops/Entornos-Deploy.md) §5 |
| Qué sabe de su propia dirección este proyecto de código | **Nada, y es deliberado.** El servicio escucha en un puerto; **no conoce ni publica la dirección por la que se lo alcanza desde afuera**, y ningún punto de acceso la devuelve | `ADR-07` §2, regla 4: el punto de salud **no dice dónde está el almacén, ni con qué esquema, ni qué ruta se configuró** |
| Forma admitida del valor | **Dirección directa** —decisión registrada del Product Owner— **o nombre de un servicio de nombres dinámico**, que la fuente declara como recomendación | Intake §10 |
| Cuál se ancla | **Esta categoría no lo decide.** La fuente registra la decisión del Product Owner de admitir la dirección directa, y adoptar la recomendación es suyo | Intake §10 |

**La tercera fila es la que sostiene `RA-03` desde este lado**, y conviene ver por qué es contraintuitiva. El punto de salud es exactamente el lugar donde uno pondría información de diagnóstico —dónde está el almacén, qué esquema tiene, a qué dirección responde— y [`ADR-07`](../05-Arquitectura-Tecnica/Adrs/ADR-07-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md) §2 lo prohíbe con esa palabra: es «`RA-03` en el punto más tentador de todos: el que existe para diagnosticar». **Un servicio que no conoce su propia dirección externa no puede filtrarla.**

### 4.2 Qué pasa cuando la dirección cambia

**Nada de este proyecto de código se toca.** El procedimiento entero es del front, y esta categoría lo registra porque es donde el lector lo va a buscar:

| Paso | Qué se hace | Quién |
| --- | --- | --- |
| 1 | **No se recompila ni se redespliega el servicio.** Su configuración no contiene su dirección externa | — |
| 2 | Se actualiza el **secreto** de la dirección base en el repositorio del front | El Product Owner |
| 3 | Se vuelve a publicar el front, con el flujo entero y su comprobación final | El flujo de publicación del front |
| 4 | Se rehace la comprobación de `PT-01.d`: una llamada de salud que devuelva **datos reales** del servidor propio | El Product Owner |

**Cómo se entera alguien de que cambió.** Nada lo detecta automáticamente: **ninguna fuente declara un vigilante, un servicio de monitoreo ni una alerta**, y esta categoría no inventa uno. Lo que está declarado es el síntoma: el front entra en **estado degradado**, que es una superficie del producto, y **ese mensaje nunca incluye la dirección del servicio interno** (`QG-08`, `RA-03`). El síntoma es visible y el diagnóstico no se filtra.

**Y el detalle que la fuente pide no perder de vista.** La restricción del intake §10 es que **la red de la facultad bloquea el acceso a direcciones dinámicas**: por eso el alumno **nunca** alcanza esta dirección, ni antes ni después de que cambie. El único que la usa es el front, desde el hosting público, donde ese bloqueo no rige. **Un cambio de dirección afecta a un solo cliente**, y por eso el procedimiento de cuatro pasos alcanza.

**Si el Product Owner adopta el servicio de nombres dinámico**, los pasos 2 y 3 dejan de ejecutarse ante cada cambio: el valor pasa a ser un nombre estable. **Esta categoría no lo adopta por su cuenta**, y tampoco declara que la recomendación sea la mejor opción sin medirla: `PT-01.d` es la puerta que mide la salida del front hacia el servicio de datos, y su salida declarada ante un fallo es **publicar el servicio en un puerto convencional**, no cambiar el modo de resolución.

### 4.3 Cómo el front la alcanza sin violar `RA-03`

Las tres propiedades que hacen que la dirección nunca llegue al navegador, cada una con dónde está verificada:

| Propiedad | Mecanismo | Dónde se verifica |
| --- | --- | --- |
| **El navegador nunca llama a este servicio** | El único consumidor es el front, **servidor a servidor** | `RA-01`; intake §17.5.P.3, fila de quién la consume. Gate `QG-05` de `GeometriaFactory-Web`: **0** peticiones del navegador hacia el servicio de datos |
| **La dirección no viaja en ningún contenido servido al navegador** | Vive en la configuración del proceso del front y sólo la conoce su cliente tipado; **ninguna superficie la muestra** | `ADR-07` de `GeometriaFactory-Web` §7 y §8, primera métrica: **0** apariciones en el repositorio |
| **La dirección no se filtra por un mensaje de error** | Los mensajes se traducen en un solo lugar y **ninguno incluye direcciones de servicios internos** | `QG-08` de este proyecto de código —**0** sobre los **quince** puntos y sobre el registro del servidor— y `QG-08` de `GeometriaFactory-Web` —**0** sobre los **diecisiete** códigos vivos y el camino de ausencia de respuesta— |

**Las tres se sostienen entre sí y ninguna alcanza sola.** La primera evita el tráfico; la segunda evita que la dirección esté en el navegador aunque no haya tráfico; la tercera evita que se filtre en el único camino que atraviesa las dos anteriores, que es un mensaje de error. `RI-05` de [`../../../Producto/Vista-Producto.md`](../../../Producto/Vista-Producto.md) §7 nombra exactamente ese modo de falla, y lo ubica **en el último tramo antes de salir del servidor propio**, que es este proyecto de código.

## 5. Configuración

Configuración de doce factores: **fuera del código, en variables de entorno o archivos montados**.

| Valor de configuración | De dónde sale | Quién lo conoce |
| --- | --- | --- |
| **Ruta del almacén** | Configuración del ambiente, apuntando en producción a un **volumen persistente** | Este proyecto de código, que la toma y se la pasa a `GeometriaFactory-Infrastructure`. **Ningún punto de acceso la devuelve** (`ADR-07` §2, regla 4) |
| **Clave de firma del acceso** | Variable de entorno o archivo montado. Ver §6 | El adaptador de emisión de accesos, que **la recibe y no la busca** |
| **Vigencia del acceso firmado** | Configuración. El intake la declara «corta» y **no fija número** | `PA-04` de `05` §11, registrado como `PD-04` en [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §10 |
| **Límite de tamaño del cuerpo de una petición** | Configuración, **uno solo para todo el producto**, que **rechaza y nunca trunca**. El número se calibra en la etapa `a` | `PA-05` de `05` §11; `QG-09` mide **0** truncamientos silenciosos |
| **Configuración de intercambio** | **Una sola declarada en todo el producto**, decidida acá porque este es el productor | [`../../../Producto/Vista-Producto.md`](../../../Producto/Vista-Producto.md) §6; `QG-10` mide **1** |
| Dirección externa del propio servicio | **No es configuración de este proyecto de código.** Ver §4.1 | — |

**La última fila de la tabla de valores es la que más se busca y no está.** Un servicio suele conocer su dirección pública para armar enlaces; **éste no arma ninguno**, porque `RA-03` declara que todo lo que el navegador deba obtener del backend **pasa por el front**: descargas, imágenes y redirecciones se sirven desde el dominio del front, que a su vez las pide por su cliente tipado.

**La quinta fila es la que `RI-01` del producto vigila.** [`../../../Producto/Vista-Producto.md`](../../../Producto/Vista-Producto.md) §7 declara que **los dos extremos configurados distinto sin romper ninguna compilación** es el único modo de falla del contrato que la compilación compartida no atrapa, y su mitigación es una sola configuración en todo el producto **verificada ejerciendo el servicio real** —no comparando dos archivos—, que es lo que hace la batería de integración que vive acá.

## 6. Secretos

| Secreto, nombrado por su función | Dónde vive | Cómo llega | Rotación |
| --- | --- | --- | --- |
| **Clave de firma del acceso** | **Fuera del repositorio y fuera de la imagen**: variable de entorno o archivo montado. Se genera o se provee en el primer arranque | La provee el ambiente del servidor propio, declarada en el archivo de composición **por referencia y nunca por valor** | **No se declara ninguna frecuencia**: ninguna fuente la da. Lo que sí se declara es el efecto de rotarla: **los accesos vigentes dejan de verificar**, y como la vigencia es corta y **no hay acceso de refresco**, la consecuencia es reingreso |
| Credenciales del repositorio, si fuera privado | En el destino, para que el motor de contenedores pueda traer el código | Fuera del repositorio. Es parte de lo que el mecanismo **[A VERIFICAR]** de §3 exige comprobar | Sin frecuencia declarada |

**Ningún secreto entra al repositorio, ni al archivo del flujo de trabajo, ni a la imagen.** El intake §17.5.P.5 lo declara así, con la precisión de que en la integración continua viajan **como secreto del repositorio, nunca en el archivo del flujo de trabajo**, y la Definition of Done §1.4 lo verifica por inspección del repositorio, del archivo de construcción y del de composición.

**Y el gate que lo sostiene en ejecución**: `QG-12` de `GeometriaFactory-Infrastructure` mide **0** emisiones de acceso sin clave de firma y **0** claves generadas al vuelo. Un servicio que generara una clave al arrancar cuando no la encuentra **arrancaría bien y emitiría accesos que dejan de valer en el siguiente reinicio**, sin ningún error en el momento de la falla.

**No se nombra ningún valor y no se declara ningún gestor de secretos concreto.** El intake §17.5.P.5 declara la forma —variable de entorno o archivo montado— y ninguna fuente nombra un producto. Elegir uno acá sería declarar una pieza de infraestructura que el intake §10 no financia.

## 7. Promoción

| Transición | Trigger | Aprobador | Registro |
| --- | --- | --- | --- |
| Rama de etapa → rama principal | Fusión del pull request | Product Owner, con OK explícito | Informe de cierre (intake §15) |
| Etapa fusionada → etapa cerrada | Etiqueta al fusionar | El mismo | La etiqueta, que permite **volver a cualquier demostración ya aprobada** |
| **Etapa cerrada → artefacto entregado** | El stage `imagen` en verde, con `PT-04` medida | El mismo | La constancia de la entrega del archivo de construcción y del de composición, en el informe de cierre |
| **Artefacto entregado → servicio desplegado** | Un **acto manual del Product Owner** sobre el servidor propio | El mismo, que es quien lo ejecuta | El registro del despliegue, con la ventana de indisponibilidad |
| **Cambio incompatible del contrato → producto desplegado** | Las **dos** unidades desplegadas desde el mismo estado del repositorio, **esta primero** | El mismo | La constancia del despliegue conjunto, en el informe de cierre |

**Las dos transiciones del medio son dos y no una**, y la Definition of Done §1.4 lo declara: **el artefacto queda entregado, no desplegado**. Es la frontera de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §1.

**Sobre la última fila, y sobre el orden.** La obligación es del intake §17.4.P.3 y su tratamiento completo está en [`../../GeometriaFactory-Web/09-Devops/Pipeline-CI-CD.md`](../../GeometriaFactory-Web/09-Devops/Pipeline-CI-CD.md) §3.2. Lo que esta categoría agrega es que **el despliegue de esta unidad tiene ventana de indisponibilidad y el del front no**, de modo que el orden entre los dos deja siempre un intervalo de desajuste. **El intake §17.6.P.7 elige el orden desde 1.22: primero el backend**, o sea esta unidad, porque una API nueva normalmente acepta lo que mandaba el front anterior. Lo que esta categoría declaraba sigue vigente: el intervalo **se minimiza y se registra**, y **el orden no lo elimina**, porque el front sale al fusionar y esta unidad se despliega a mano. `PD-05` de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §10 queda **cerrado**.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara los **dos** ambientes que el intake §17.5.P.8 nombra y registra el **apartamiento del modelo de cuatro más canario** que `Rules-Devops.md` §2.2 fija para el tipo `rest-api`, con tres fundamentos verificables —un solo servidor con presupuesto cero, ausencia de proxy inverso y **escritor único** sobre un archivo—, el ADR que lo sostiene y **lo que el apartamiento cuesta**, con las tres cosas que el producto pone en su lugar. Declara qué vive en el repositorio como infraestructura y que **un único puerto publicado hace del gate de puntos fuera de la guardia también un gate de exposición**. Declara **cómo llega el código al destino** —construcción en destino desde el repositorio— con su marca **[A VERIFICAR]** intacta y con una consecuencia propia declarada: **el destino necesita salida a la red para desplegar y para revertir**. Dedica su §4 a **la dirección dinámica**: quién la conoce, dónde vive, que **este servicio no conoce su propia dirección externa** y por qué eso sostiene `RA-03` en el punto más tentador; el procedimiento de **cuatro pasos** cuando cambia, con la constancia de que **nada lo detecta automáticamente**; y las **tres** propiedades que hacen que el front la alcance sin que llegue al navegador. Declara la configuración de doce factores con **la dirección externa propia ausente y explicado por qué**, y los secretos nombrados por su función, sin valor, sin gestor concreto y sin frecuencia de rotación inventada. |
| 1.1 | 2026-08-11 | **Propagación de la decisión del Product Owner** registrada en el intake **1.22** §17.6.P.7: cuando front y backend salen juntos, **primero el backend**. Se reescribe el fundamento de §7, que se apoyaba en que **ninguna fuente elegía el orden**, y se agrega la precedencia a la última fila de la tabla de promoción, manteniendo declarado que el orden **no elimina** el intervalo —el front sale al fusionar y esta unidad a mano— sino que lo minimiza. `PD-05` queda cerrado en [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §10. Sube la trazabilidad upstream del intake de **1.21** a **1.22** y le agrega §17.6.P.7. |
| 1.2 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
