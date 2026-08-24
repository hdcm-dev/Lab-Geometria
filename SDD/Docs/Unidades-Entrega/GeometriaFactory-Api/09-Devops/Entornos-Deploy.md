# Entornos y despliegue — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** Entornos-Deploy.md
**Versión:** 2.1
**Estado:** Propuesto
**Fecha:** 2026-08-24
**`tipo_unidad_entrega` (D8):** `rest-api` · **Unidad de entrega principal del producto**
**Proyectos de código que la componen:** `GeometriaFactory-Api`, `GeometriaFactory-Domain`, `GeometriaFactory-Application`, `GeometriaFactory-Infrastructure` y `GeometriaFactory-Contracts`
**Trazabilidad upstream:** [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **2.1**
**Consolida a:** los documentos homónimos de las capas que componen la unidad, por `Audit/Migracion-M10-Consolidacion-Fusion.md` 1.2 §4

---

## 0. Cómo leer este documento

**La unidad de entrega tiene un solo documento de esta clase.** Cada sección lleva **una subsección
por proyecto de código**, con su texto **transpuesto sin reescritura**.

**Sólo dos de las doce secciones son comunes a las cuatro capas, y es el documento más asimétrico de
la categoría.** El motivo es real: **tres de las cuatro capas no se despliegan**, de modo que su
«entorno» es el contenedor de desarrollo y poco más. Lo que sí es propio y no estaba junto:

| Sección | Sólo en |
| --- | --- |
| Ambientes · cómo llega el código al destino · **la dirección dinámica, que es la restricción que ordena todo** | `GeometriaFactory-Api` |
| El único ambiente que existe: el contenedor de desarrollo | `GeometriaFactory-Domain` |
| Configuración y respaldo · **secretos: la clave de firma que se recibe y no se busca** | `GeometriaFactory-Infrastructure` |

**La clave de firma y la dirección dinámica son la misma preocupación vista desde dos capas**, y hasta
esta consolidación vivían en documentos que no se citaban.

---

## 1. Ambientes de este proyecto de código

### 1.1 `GeometriaFactory-Api`

**Dos, y son los que la fuente declara.** El intake §17.1.P.8 · GeometriaFactory-Api los nombra sin rodeos: «desarrollo (devcontainer) y producción (servidor propio)».

| Ambiente | Destino | Quién lo aprueba y lo opera | Ventana o acuerdo de nivel de servicio |
| --- | --- | --- | --- |
| **Desarrollo** | El contenedor de desarrollo, en la máquina de quien construye. Ahí se ejecuta el servicio, se corre la batería de integración y se construye y arranca la imagen para medir `PT-04` | Nadie: no hay promoción hacia él | No aplica. En desarrollo **escucha sin certificado**, para evitar la fricción del certificado de confianza dentro del contenedor (intake §17.1.P.1 · GeometriaFactory-Api) |
| **Producción** | El servidor domiciliario, con la imagen construida en destino y el almacén en un **volumen persistente** | El **Product Owner**, que además es quien ejecuta el despliegue a mano | **Sin acuerdo de disponibilidad.** El intake §17.1.P.10 · GeometriaFactory-Api declara «disponibilidad: sin SLO», y **cada reemplazo de versión tiene ventana de indisponibilidad** |

**La segunda fila no declara un SLO y no es un olvido.** El intake §17.1.P.10 · GeometriaFactory-Api lo declara expresamente: el servidor es domiciliario, su caída es el riesgo `R-08` y **se responde con estado degradado en el front, no con redundancia**. El intake §11 lo registra desde el negocio como `RN-B4`, con la mitigación declarada —estado degradado explícito— y con la frase que cierra la discusión: **no hay alta disponibilidad ni la va a haber, porque es un laboratorio de aula**.

### 1.1 Apartamiento declarado del modelo de la categoría

`Rules-Devops.md` §2.2 fija para el tipo `rest-api` el modelo `DEV` / `QA` / `STAGING` / `PROD` **más canario**, y admite agregar ambientes pero **no quitar ninguno sin un ADR que lo justifique**. Acá hay **dos** y no cuatro, y **no hay canario**. El apartamiento se declara con sus fundamentos, todos verificables:

| Qué falta respecto del modelo | Fundamento del apartamiento | Dónde se verifica |
| --- | --- | --- |
| `QA` y `STAGING` | **Un solo servidor y presupuesto declarado cero.** El intake §10 enumera las tres piezas de infraestructura de costo cero, y el servidor domiciliario es una. Montar dos ambientes más exigiría dos servidores más o dos instancias en el mismo, con dos almacenes que nadie mantiene | Intake §10 |
| Despliegue **canario** | **No hay proxy inverso, y sin él no hay despliegue con solapamiento.** El intake §17.1.P.8 · GeometriaFactory-Api lo declara: el reemplazo de versión es **detener y arrancar**, con ventana de indisponibilidad. Un canario requiere dos versiones vivas a la vez y un repartidor de tráfico que el producto no tiene | Intake §17.1.P.8 · GeometriaFactory-Api y §17.1.P.12 · GeometriaFactory-Api |
| Despliegue **azul-verde** | Lo mismo, y con un agravante: el almacén es **un archivo único con escritor único**. Dos versiones vivas escribirían sobre el mismo archivo | Intake §17.1.P.4 · GeometriaFactory-Infrastructure, por la vía de `GeometriaFactory-Infrastructure`; `05` §5 |

**El ADR que sostiene el apartamiento es [`ADR-00008`](../05-Arquitectura-Tecnica/Adrs/ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md)**, que en su §6 acepta por escrito los cuatro trade-offs que implica, entre ellos el despliegue conjunto con su ventana de indisponibilidad y que **la reversión sea reconstruir desde una etiqueta, sin imagen publicada a la que volver**.

**Lo que el apartamiento cuesta, declarado en lugar de disimulado.** Sin `STAGING`, **la primera vez que una versión corre contra el almacén real es en producción**; y sin canario, **un despliegue malo afecta al 100 % de las peticiones desde el primer segundo**. Lo que el producto pone en su lugar son tres cosas, y ninguna es un ambiente: **el arranque en dos fases**, que hace que un servicio que no puede confiar en su almacén **no escuche** ([`ADR-00007`](../05-Arquitectura-Tecnica/Adrs/ADR-00007-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md)); la puerta `PT-04`, que ejercita el arranque completo **antes** de que exista la oportunidad de desplegar; y el **estado degradado** del front cuando el servicio no responde.

## 2. Provisión

### 2.b La aprobación de `plan` antes de `apply` — **ítem propio**

**Esta subsección realiza el ítem 2.b de `Rules-Devops.md` §4.4**, que desde la regla **6.0** lo pide
**separado de la herramienta**: es una **política de proceso** y no una consecuencia de qué
herramienta se elija, de modo que esperar a elegirla para declararla es diferir por arrastre.

| Aspecto | Decisión |
| --- | --- |
| **Aprobación de `plan` antes de `apply`** | **No aplica**, y no está diferida |
| **Por qué** | El ítem gobierna el ciclo de una herramienta declarativa de infraestructura, y **acá no hay ninguna**: §2.1 lo declara con su fundamento —no hay nube que provisionar, el servidor domiciliario ya existe (intake §10)—. Sin herramienta no hay `plan` ni `apply` que aprobar |
| **Qué ocupa su lugar** | El punto de control que sí existe: **una rama y un pull request por etapa**, con la revisión del Product Owner como punto bloqueante (intake §15). Todo cambio de `deploy/Dockerfile` y `deploy/compose.yaml` pasa por ahí, porque están versionados |
| **Qué lo reabriría** | Que el producto adopte una herramienta declarativa —Terraform, Pulumi, Bicep o equivalente—, decisión que hoy ninguna fuente pide |

**«No aplica» y «diferido» no son lo mismo, y la diferencia importa.** Un ítem diferido es uno que hay
que contestar más adelante y lleva los cuatro campos de `Root-Rules.md` §12.2. Éste **no se va a
contestar nunca mientras no haya herramienta**, y declararlo pendiente dejaría en la tabla de puntos
abiertos una fila que nadie puede cerrar.

### 2.1 `GeometriaFactory-Api`

**No hay infraestructura declarativa en el sentido del catálogo, y lo que hay está versionado.**

| Aspecto | Decisión | Fundamento |
| --- | --- | --- |
| Herramienta declarativa de infraestructura | **Ninguna.** No hay nube que provisionar: el servidor domiciliario ya existe y el intake §10 lo cuenta entre las tres piezas de costo cero | Intake §10 |
| Qué sí vive en el repositorio | **`deploy/Dockerfile`**, multietapa, y **`deploy/compose.yaml`**, «despliegue desde Git en destino». Son las dos piezas que el agente entrega | Intake §16, árbol del repositorio |
| Volumen del almacén | **Persistente, declarado en el archivo de composición, y nunca dentro de la imagen** | Intake §17.1.P.4 · GeometriaFactory-Api y §17.1.P.4 · GeometriaFactory-Infrastructure; Definition of Done §1.4 |
| Punto de entrada | **Un puerto publicado hacia el enrutador, y es el único punto de entrada al servidor propio.** Todo lo que este proyecto de código no exponga, **no existe para nadie de afuera** | `05` §5, fila de punto de entrada |
| Comprobación de salud del contenedor | Declarada en el archivo de composición, contra el punto de salud del servicio | Intake §17.1.P.3 · GeometriaFactory-Api, fila de salud |

**La cuarta fila es una decisión de seguridad y no de red.** Un único puerto publicado significa que la superficie expuesta del servidor domiciliario **es exactamente la que `05` §3.4 enumera**, y por eso el `QG-05` de la canalización —**4** puntos fuera de la guardia sobre **15**, ni uno más— es también un gate de exposición: cada punto nuevo es superficie nueva alcanzable desde afuera.

### 2.2 `GeometriaFactory-Domain`

**No hay infraestructura declarativa que escribir, y su ausencia es consecuencia de §1 y no una deuda.** No hay ambiente que provisionar: no hay servidor, no hay red, no hay almacenamiento y no hay servicio administrado atribuibles a este proyecto de código.

Lo único que se aproxima a una declaración de entorno es el **archivo de definición del contenedor de desarrollo**, y ya está declarado en el árbol del intake §16. Su contenido concreto —la característica de plataforma que instala y su anclaje de versión— es de la etapa `a`.

**La infraestructura del producto sí existe**, pero pertenece a los dos proyectos de código que se despliegan: `deploy/Dockerfile` multietapa y `deploy/compose.yaml` para el backend, y el flujo de trabajo de publicación por FTP para el front (intake §16). **Este documento no los describe**: son de la categoría 09 de esos proyectos de código, y describirlos acá crearía la segunda fuente de verdad que el corpus ya tiene documentada como su defecto más repetido.

### 2.3 `GeometriaFactory-Application`

**No hay infraestructura declarativa atribuible a este proyecto de código.** `05` §5 declara **ninguna** dependencia de infraestructura: no tiene servidor, red, almacenamiento ni servicio administrado propios, y todo lo que necesita del exterior entra por los cuatro puertos.

Esa frase es más fuerte de lo que parece, y es lo que hace verificable a la definición de calidad de [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §1: **si un caso de uso necesitara algo del entorno que no entra por un puerto, dejaría de ser ejercible con dobles**, y con eso caería la propiedad que justifica el estilo entero del proyecto de código.

La infraestructura del producto existe y está enumerada en el árbol del intake §16 —`deploy/Dockerfile`, `deploy/compose.yaml` y el flujo de trabajo de publicación del front—, pero **pertenece a los dos proyectos de código que se despliegan y no se describe acá**. Lo único de entorno que este proyecto de código usa es la definición del contenedor de desarrollo, común al producto.

### 2.4 `GeometriaFactory-Infrastructure`

**No hay infraestructura declarativa atribuible a este proyecto de código**: no provisiona servidor, red ni almacenamiento.

Lo que sí hace, y es lo que lo distingue, es **exigir tres cosas del ambiente que lo hospeda**, todas de `05` §5 y del intake §17.1.P.4 · GeometriaFactory-Infrastructure:

| Exigencia sobre el ambiente ajeno | Detalle | Quién la satisface |
| --- | --- | --- |
| Un **volumen persistente** donde viva el archivo del almacén, **nunca una ruta dentro de la imagen** | Si el archivo quedara dentro de la imagen, cada reemplazo de versión borraría el trabajo de la comisión | Categoría 09 de `GeometriaFactory-Api`, en el archivo de composición |
| La **ubicación del almacén tomada de configuración**, provista por `GeometriaFactory-Api` | Este proyecto de código **no la busca**: la recibe | La misma |
| La **clave de firma provista desde afuera**, por variable de entorno o archivo montado | Ver §5 | La misma |

**Las tres son restricciones y no provisiones.** Este proyecto de código no crea el volumen ni escribe el archivo de composición; lo que hace es **fallar de manera declarada si alguna de las tres no está**, que es preferible a arreglárselas solo. `QG-12` lo mide en la tercera: **0** emisiones de acceso sin clave de firma y **0** claves generadas al vuelo.

## 3. Cómo llega el código al destino

### 3.1 `GeometriaFactory-Api`

El canal de entrega que el intake §17.1.P.7 · GeometriaFactory-Api declara es **la imagen construida en destino desde el repositorio con el archivo de composición, sin publicar en ningún registro**. Es una decisión de la fuente y esta categoría no la reabre; lo que hace es escribir qué implica.

| Qué exige el mecanismo | Estado | Fundamento |
| --- | --- | --- |
| Que el **motor de contenedores del destino resuelva la referencia al repositorio**, y tenga credenciales si el repositorio es privado | **[A VERIFICAR]** en la fuente. El intake §17.1.P.11 · GeometriaFactory-Api punto 5 exige **probarlo una vez antes de depender del mecanismo** | Intake §17.1.P.11 · GeometriaFactory-Api; `PA-08` de `05` §11, que lo dirige a esta categoría **para medirlo** |
| Que el destino tenga **acceso a la red** para traer el código y las dependencias en el momento de construir | Consecuencia directa del mecanismo, declarada acá | **Decisión de esta categoría**, declarada como tal |
| Que la construcción **no requiera el kit de desarrollo instalado en el anfitrión** | Cumplido por el archivo de construcción **multietapa**: la etapa de construcción vive dentro de la imagen intermedia | Intake §17.1.P.9 · GeometriaFactory-Api; `05` §5 |
| Que la imagen final **no tenga linaje con la del contenedor de desarrollo** | Cumplido por diseño del archivo de construcción, y verificable por inspección | Intake §17.1.P.9 · GeometriaFactory-Api; Definition of Done §1.4 |

**La segunda fila es lo que esta categoría agrega, y va declarada como decisión propia porque ninguna fuente la enuncia.** Construir en destino significa que **el servidor domiciliario necesita salida a la red en el momento del despliegue**, y que un corte de internet no sólo deja el servicio inalcanzable sino que **impide desplegar**. Es distinto de un modelo con registro, donde la imagen ya construida podría estar en la máquina. Se declara para que quien despliegue lo sepa, no para cambiar la decisión.

**Y una consecuencia sobre la reversión que se sigue de lo mismo**: como no hay imagen publicada, **la reversión es volver a la etiqueta anterior y reconstruir**, y también necesita red. Está en [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §7 y [`ADR-00008`](../05-Arquitectura-Tecnica/Adrs/ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) §6 lo acepta por escrito.

**El procedimiento paso a paso, con su verificación, vive en [`Guia-Publicacion-Image-Docker.md`](Guia-Publicacion-Image-Docker.md)**, que es donde `Rules-Devops.md` §4.5 lo pide. Acá queda la política; ahí, el procedimiento.

## 4. La dirección dinámica, que es la restricción que ordena todo

### 4.1 `GeometriaFactory-Api`

El intake §10 la declara entre las restricciones del cliente: **el servidor domiciliario no tiene dirección fija**, se admite apuntar a la dirección directa por decisión del Product Owner —«la IP dinámica realmente no cambia tanto»— y el servicio de nombres dinámico queda como **recomendación**. Y §14 declara la restricción hermana que la hace crítica: **la red de la facultad bloquea el acceso a direcciones dinámicas**, que es lo que ordena la topología entera y la razón por la que el front vive en un hosting público.

**Las dos juntas producen la propiedad que esta sección tiene que sostener**: la dirección del servidor propio **la usa un solo cliente, el front, servidor a servidor** — y **nunca el navegador** (`RA-01`).

### 4.1 Cómo se resuelve la dirección

| Aspecto | Decisión | Fundamento |
| --- | --- | --- |
| Quién conoce la dirección | **Únicamente el proceso del front**, y dentro de él, únicamente su cliente tipado del servicio de datos | Intake §17.2.P.3 · GeometriaFactory-Web; `ADR-00007` de `GeometriaFactory-Web` §7 |
| Dónde vive el valor | Como **secreto del repositorio** del front, inyectado al publicar. **La dirección real no se versiona** | Intake §17.2.P.5 · GeometriaFactory-Web; [`../../GeometriaFactory-Web/09-Devops/Entornos-Deploy.md`](../../GeometriaFactory-Web/09-Devops/Entornos-Deploy.md) §5 |
| Qué sabe de su propia dirección este proyecto de código | **Nada, y es deliberado.** El servicio escucha en un puerto; **no conoce ni publica la dirección por la que se lo alcanza desde afuera**, y ningún punto de acceso la devuelve | `ADR-00007` §2, regla 4: el punto de salud **no dice dónde está el almacén, ni con qué esquema, ni qué ruta se configuró** |
| Forma admitida del valor | **Dirección directa** —decisión registrada del Product Owner— **o nombre de un servicio de nombres dinámico**, que la fuente declara como recomendación | Intake §10 |
| Cuál se ancla | **Esta categoría no lo decide.** La fuente registra la decisión del Product Owner de admitir la dirección directa, y adoptar la recomendación es suyo | Intake §10 |

**La tercera fila es la que sostiene `RA-03` desde este lado**, y conviene ver por qué es contraintuitiva. El punto de salud es exactamente el lugar donde uno pondría información de diagnóstico —dónde está el almacén, qué esquema tiene, a qué dirección responde— y [`ADR-00007`](../05-Arquitectura-Tecnica/Adrs/ADR-00007-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md) §2 lo prohíbe con esa palabra: es «`RA-03` en el punto más tentador de todos: el que existe para diagnosticar». **Un servicio que no conoce su propia dirección externa no puede filtrarla.**

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
| **El navegador nunca llama a este servicio** | El único consumidor es el front, **servidor a servidor** | `RA-01`; intake §17.1.P.3 · GeometriaFactory-Api, fila de quién la consume. Gate `QG-05` de `GeometriaFactory-Web`: **0** peticiones del navegador hacia el servicio de datos |
| **La dirección no viaja en ningún contenido servido al navegador** | Vive en la configuración del proceso del front y sólo la conoce su cliente tipado; **ninguna superficie la muestra** | `ADR-00007` de `GeometriaFactory-Web` §7 y §8, primera métrica: **0** apariciones en el repositorio |
| **La dirección no se filtra por un mensaje de error** | Los mensajes se traducen en un solo lugar y **ninguno incluye direcciones de servicios internos** | `QG-08` de este proyecto de código —**0** sobre los **quince** puntos y sobre el registro del servidor— y `QG-08` de `GeometriaFactory-Web` —**0** sobre los **diecisiete** códigos vivos y el camino de ausencia de respuesta— |

**Las tres se sostienen entre sí y ninguna alcanza sola.** La primera evita el tráfico; la segunda evita que la dirección esté en el navegador aunque no haya tráfico; la tercera evita que se filtre en el único camino que atraviesa las dos anteriores, que es un mensaje de error. `RI-05` de [`../../../Producto/Vista-Producto.md`](../../../Producto/Vista-Producto.md) §7 nombra exactamente ese modo de falla, y lo ubica **en el último tramo antes de salir del servidor propio**, que es este proyecto de código.

## 5. Configuración

### 5.1 `GeometriaFactory-Api`

Configuración de doce factores: **fuera del código, en variables de entorno o archivos montados**.

| Valor de configuración | De dónde sale | Quién lo conoce |
| --- | --- | --- |
| **Ruta del almacén** | Configuración del ambiente, apuntando en producción a un **volumen persistente** | Este proyecto de código, que la toma y se la pasa a `GeometriaFactory-Infrastructure`. **Ningún punto de acceso la devuelve** (`ADR-00007` §2, regla 4) |
| **Clave de firma del acceso** | Variable de entorno o archivo montado. Ver §6 | El adaptador de emisión de accesos, que **la recibe y no la busca** |
| **Vigencia del acceso firmado** | Configuración. El intake la declara «corta» y **no fija número** | `PA-04` de `05` §11, registrado como `PD-04` en [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §10 |
| **Límite de tamaño del cuerpo de una petición** | Configuración, **uno solo para todo el producto**, que **rechaza y nunca trunca**. El número se calibra en la etapa `a` | `PA-05` de `05` §11; `QG-09` mide **0** truncamientos silenciosos |
| **Configuración de intercambio** | **Una sola declarada en todo el producto**, decidida acá porque este es el productor | [`../../../Producto/Vista-Producto.md`](../../../Producto/Vista-Producto.md) §6; `QG-10` mide **1** |
| Dirección externa del propio servicio | **No es configuración de este proyecto de código.** Ver §4.1 | — |

**La última fila de la tabla de valores es la que más se busca y no está.** Un servicio suele conocer su dirección pública para armar enlaces; **éste no arma ninguno**, porque `RA-03` declara que todo lo que el navegador deba obtener del backend **pasa por el front**: descargas, imágenes y redirecciones se sirven desde el dominio del front, que a su vez las pide por su cliente tipado.

**La quinta fila es la que `RI-01` del producto vigila.** [`../../../Producto/Vista-Producto.md`](../../../Producto/Vista-Producto.md) §7 declara que **los dos extremos configurados distinto sin romper ninguna compilación** es el único modo de falla del contrato que la compilación compartida no atrapa, y su mitigación es una sola configuración en todo el producto **verificada ejerciendo el servicio real** —no comparando dos archivos—, que es lo que hace la batería de integración que vive acá.

### 5.2 `GeometriaFactory-Domain`

| Aspecto | Decisión | Fundamento |
| --- | --- | --- |
| Configuración de ejecución | **Ninguna.** El proyecto de código no lee configuración | `05` §7, citado por [`../08-Calidad-Y-Pruebas/Estrategia-Testing.md`](../08-Calidad-Y-Pruebas/Estrategia-Testing.md) §7 |
| Variables de entorno | **Ninguna**, ni en construcción ni en prueba | `Estrategia-Testing.md` §7, fila de variables de entorno |
| Reloj | **No se fija ni se simula**: el momento entra por parámetro | [`../05-Arquitectura-Tecnica/Adrs/ADR-02006-El-Dominio-No-Lee-El-Reloj-Ni-El-Conjunto.md`](../05-Arquitectura-Tecnica/Adrs/ADR-02006-El-Dominio-No-Lee-El-Reloj-Ni-El-Conjunto.md) |

**El principio de configuración externa se cumple de la forma más fuerte posible: no habiendo configuración.** Un mapa de variables por ambiente sería una tabla vacía con encabezados.

### 5.3 `GeometriaFactory-Application`

| Aspecto | Decisión | Fundamento |
| --- | --- | --- |
| Configuración propia de ejecución | **Ninguna.** No lee variables de entorno ni archivos de configuración: lo que necesita se lo inyecta la composición de raíz de `GeometriaFactory-Api` | `05` §5; intake §17.1.P.2 · GeometriaFactory-Application |
| Persistencia | **No aplica directamente.** Declara el puerto de repositorio y el alcance de la unidad de trabajo —**un caso de uso, una transacción**—, y la implementación es de `GeometriaFactory-Infrastructure` | Intake §17.1.P.4 · GeometriaFactory-Application |
| Reloj | **Es un puerto**, para que las fechas de alta y modificación sean verificables en prueba. No se toma del sistema | Intake §17.1.P.11 · GeometriaFactory-Application, punto 3 |
| Variables de entorno del pipeline | **Ninguna** | Decisión de esta categoría, derivada de la tabla de §2.1 de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md): sus tres stages leen el repositorio y escriben recuentos e informes |

**La fila del reloj no es un detalle de estilo.** Un caso de uso que tomara la hora del sistema sería irreproducible en la canalización, y `QG-02` —batería entera en verde— empezaría a fallar por motivos que no son del código. Que el reloj entre por un puerto es lo que hace que la batería sea determinista en cualquier ejecutor.

## 6. Secretos

### 6.1 `GeometriaFactory-Api`

| Secreto, nombrado por su función | Dónde vive | Cómo llega | Rotación |
| --- | --- | --- | --- |
| **Clave de firma del acceso** | **Fuera del repositorio y fuera de la imagen**: variable de entorno o archivo montado. Se genera o se provee en el primer arranque | La provee el ambiente del servidor propio, declarada en el archivo de composición **por referencia y nunca por valor** | **No se declara ninguna frecuencia**: ninguna fuente la da. Lo que sí se declara es el efecto de rotarla: **los accesos vigentes dejan de verificar**, y como la vigencia es corta y **no hay acceso de refresco**, la consecuencia es reingreso |
| Credenciales del repositorio, si fuera privado | En el destino, para que el motor de contenedores pueda traer el código | Fuera del repositorio. Es parte de lo que el mecanismo **[A VERIFICAR]** de §3 exige comprobar | Sin frecuencia declarada |

**Ningún secreto entra al repositorio, ni al archivo del flujo de trabajo, ni a la imagen.** El intake §17.1.P.5 · GeometriaFactory-Api lo declara así, con la precisión de que en la integración continua viajan **como secreto del repositorio, nunca en el archivo del flujo de trabajo**, y la Definition of Done §1.4 lo verifica por inspección del repositorio, del archivo de construcción y del de composición.

**Y el gate que lo sostiene en ejecución**: `QG-12` de `GeometriaFactory-Infrastructure` mide **0** emisiones de acceso sin clave de firma y **0** claves generadas al vuelo. Un servicio que generara una clave al arrancar cuando no la encuentra **arrancaría bien y emitiría accesos que dejan de valer en el siguiente reinicio**, sin ningún error en el momento de la falla.

**No se nombra ningún valor y no se declara ningún gestor de secretos concreto.** El intake §17.1.P.5 · GeometriaFactory-Api declara la forma —variable de entorno o archivo montado— y ninguna fuente nombra un producto. Elegir uno acá sería declarar una pieza de infraestructura que el intake §10 no financia.

### 6.2 `GeometriaFactory-Domain`

**Este proyecto de código no maneja ningún secreto**, y la afirmación se puede verificar en dos lugares distintos de la cadena:

| Afirmación | Dónde está declarada |
| --- | --- |
| No maneja secretos: la contraseña llega **ya derivada** y se guarda como valor de credencial derivada, nulo hasta el primer ingreso | Intake §17.1.P.5 · GeometriaFactory-Domain |
| El proyecto de código no deriva ni compara credenciales | [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §2, fila de seguridad |
| Ninguno en el ambiente de pruebas | [`../08-Calidad-Y-Pruebas/Estrategia-Testing.md`](../08-Calidad-Y-Pruebas/Estrategia-Testing.md) §7 |

**Consecuencias operativas, que sí son de esta categoría:**

- El pipeline de este proyecto de código **no requiere ninguna credencial**: sus tres stages leen el repositorio y escriben informes. Un stage suyo que pidiera un secreto sería una señal de que algo se salió de su alcance.
- La prohibición de confirmar secretos en el repositorio rige igual, y es del producto: el intake §17.2.P.5 · GeometriaFactory-Web declara que las credenciales del canal de publicación del front viven como secretos del repositorio y que **la dirección real del servidor propio no se versiona**. Este proyecto de código no aporta ninguno de los dos, pero comparte el repositorio.
- **No se declara ninguna frecuencia de rotación**, porque no hay secreto propio que rotar. Los del producto pertenecen a la categoría 09 de `GeometriaFactory-Web` y de `GeometriaFactory-Api`.

### 6.3 `GeometriaFactory-Application`

**Ninguno, y la afirmación es de la fuente y no de esta categoría.** El intake §17.1.P.5 · GeometriaFactory-Application declara que esta capa **no maneja secretos**: la verificación de pertenencia vive acá, pero la comparación de contraseñas y la emisión de accesos no.

| Momento | Secretos | Fundamento |
| --- | --- | --- |
| Construcción | **Ninguno.** El restaurador toma dependencias de la plataforma; no hay publicación que autenticar | Intake §17.1.P.7 · GeometriaFactory-Application, por remisión a §17.1.P.7 · GeometriaFactory-Domain |
| Prueba | **Ninguno.** La batería corre con dobles de los cuatro puertos, sin base de datos y sin frontera de proceso | `Estrategia-Calidad.md` §1 |
| Ejecución | **Ninguno propio.** La contraseña llega **ya derivada** y la provisoria **ya producida**: esta capa las recibe, no las fabrica | Intake §17.1.P.5 · GeometriaFactory-Application |

**Lo que sí es responsabilidad de esta capa, y conviene no confundirlo con un secreto**, es la **verificación de pertenencia**: el intake §17.1.P.5 · GeometriaFactory-Application la declara distinta de la autorización por rol y no reemplazable por ella —«el rol no alcanza; un alumno autenticado no debe poder leer el trabajo de otro cambiando el identificador en la petición»—, materializa `INV-02` e `INV-03`, y su respuesta ante un recurso ajeno es **«no encontrado», no «no autorizado»** (`RN-04003`). Desde esta categoría, la consecuencia práctica es que **un stage de este proyecto de código que pidiera una credencial sería la señal de que algo se salió de su alcance**.

**No se declara ninguna frecuencia de rotación**: no hay secreto propio que rotar. Los del producto —la clave de firma del servidor propio, la dirección base del servicio de datos y las credenciales del canal de publicación del front— viven fuera del repositorio y su gobierno pertenece a las categorías 09 de `GeometriaFactory-Api` y de `GeometriaFactory-Web`.

## 7. Promoción

### 7.1 `GeometriaFactory-Api`

| Transición | Trigger | Aprobador | Registro |
| --- | --- | --- | --- |
| Rama de etapa → rama principal | Fusión del pull request | Product Owner, con OK explícito | Informe de cierre (intake §15) |
| Etapa fusionada → etapa cerrada | Etiqueta al fusionar | El mismo | La etiqueta, que permite **volver a cualquier demostración ya aprobada** |
| **Etapa cerrada → artefacto entregado** | El stage `imagen` en verde, con `PT-04` medida | El mismo | La constancia de la entrega del archivo de construcción y del de composición, en el informe de cierre |
| **Artefacto entregado → servicio desplegado** | Un **acto manual del Product Owner** sobre el servidor propio | El mismo, que es quien lo ejecuta | El registro del despliegue, con la ventana de indisponibilidad |
| **Cambio incompatible del contrato → producto desplegado** | Las **dos** unidades desplegadas desde el mismo estado del repositorio, **esta primero** | El mismo | La constancia del despliegue conjunto, en el informe de cierre |

**Las dos transiciones del medio son dos y no una**, y la Definition of Done §1.4 lo declara: **el artefacto queda entregado, no desplegado**. Es la frontera de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §1.

**Sobre la última fila, y sobre el orden.** La obligación es del intake §17.1.P.3 · GeometriaFactory-Contracts y su tratamiento completo está en [`../../GeometriaFactory-Web/09-Devops/Pipeline-CI-CD.md`](../../GeometriaFactory-Web/09-Devops/Pipeline-CI-CD.md) §3.2. Lo que esta categoría agrega es que **el despliegue de esta unidad tiene ventana de indisponibilidad y el del front no**, de modo que el orden entre los dos deja siempre un intervalo de desajuste. **El intake §17.2.P.7 · GeometriaFactory-Web elige el orden desde 1.22: primero el backend**, o sea esta unidad, porque una API nueva normalmente acepta lo que mandaba el front anterior. Lo que esta categoría declaraba sigue vigente: el intervalo **se minimiza y se registra**, y **el orden no lo elimina**, porque el front sale al fusionar y esta unidad se despliega a mano. `PD-05` de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §10 queda **cerrado**.

### 7.2 `GeometriaFactory-Domain`

**No hay promoción entre ambientes ni entre canales, porque no hay ni ambientes ni canales.** Lo que existe es la promoción de estado del trabajo, declarada en [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §6: rama de etapa → rama principal por fusión del pull request, y etapa fusionada → etapa cerrada por etiqueta, las dos con **OK explícito del Product Owner** en el punto de control (intake §15).

**Registro de auditoría de esa promoción**, que es lo que la reemplaza acá:

| Qué queda registrado | Dónde | Fundamento |
| --- | --- | --- |
| El OK explícito del Product Owner, con constancia escrita | Informe de cierre de la etapa, en el directorio de avances que el intake §15 declara | Intake §15, regla de delivery 3 |
| La medición de los dos gates condicionados con su distancia al umbral | El mismo informe | [`../08-Calidad-Y-Pruebas/Criterios-Validacion.md`](../08-Calidad-Y-Pruebas/Criterios-Validacion.md) §6 |
| La etiqueta de la etapa | El repositorio | Intake §17.1.P.7 · GeometriaFactory-Domain |

### 7.3 `GeometriaFactory-Application`

La de estado del trabajo, igual que en el resto del producto:

| Transición | Trigger | Aprobador | Registro |
| --- | --- | --- | --- |
| Rama de etapa → rama principal | Fusión del pull request | Product Owner, con OK explícito | Informe de cierre (intake §15) |
| Etapa fusionada → etapa cerrada | Etiqueta al fusionar | El mismo | La etiqueta, y la constancia de `QG-07` sobre las **cuatro** comprobaciones |

**Ninguna transición de este proyecto de código alcanza a un acto de despliegue**, y es lo que lo distingue de `GeometriaFactory-Contracts`, cuyo `QG-08` sí lo hace. Acá la promoción termina en la etiqueta: lo que se despliega es la unidad que lo embebe, y su promoción la gobierna la categoría 09 de `GeometriaFactory-Api`.

### 7.4 `GeometriaFactory-Infrastructure`

| Transición | Trigger | Aprobador | Registro |
| --- | --- | --- | --- |
| Rama de etapa → rama principal | Fusión del pull request | Product Owner, con OK explícito | Informe de cierre (intake §15) |
| Etapa fusionada → etapa cerrada | Etiqueta al fusionar | El mismo | La etiqueta, más el registro del linaje de transformaciones aplicado |

**Ninguna transición de este proyecto de código alcanza a un acto de despliegue.** Lo que se despliega es la unidad que lo embebe, y su promoción la gobierna la categoría 09 de `GeometriaFactory-Api`.

**Pero una de sus obligaciones sí sobrevive a la promoción**, y es la única del producto que lo hace: **el linaje de transformaciones**. Una etapa cerrada deja aplicado en todo almacén existente un linaje que ninguna etiqueta posterior deshace. Por eso el registro de la segunda fila no es ceremonia: es el único rastro de qué esquema quedó en el almacén de la comisión.

## 8. Ambientes y canales de este proyecto de código

### 8.1 `GeometriaFactory-Domain`

**Ninguno de despliegue y ninguno de publicación.** Las tres afirmaciones que lo sostienen son de la fuente y no de esta categoría:

| Afirmación | Dónde está declarada |
| --- | --- |
| No tiene unidad de despliegue propia: su artefacto se compila dentro del artefacto de agrupación del producto y viaja embebido en las dos unidades desplegables por la vía de sus consumidores | `05` §5, primera fila |
| No se publica en ningún repositorio de paquetes: `redistribuible` es false | `05` §5, última fila; intake §13 |
| Los **dos** artefactos entregables del producto son una imagen de contenedor y una publicación subida por FTP, y **ningún proyecto de código se publica como paquete redistribuible** | Intake §13 |

De modo que la tabla de ambientes de este proyecto de código tiene una sola fila, y no es un ambiente desplegado:

| Ambiente o canal | Destino | Aprobador | Ventana o acuerdo de nivel de servicio |
| --- | --- | --- | --- |
| Contenedor de desarrollo | La máquina de quien construye, y el ejecutor del pipeline | Nadie: no hay promoción hacia él | No aplica: no atiende peticiones |

### 1.1 Apartamiento declarado del modelo de la categoría

`Rules-Devops.md` §2.2 fija para el tipo `library` un modelo de canales `preview` / `stable` sobre feed único, y declara que los modelos son piso: no se quita ninguno sin un ADR que lo justifique.

**El ADR existe y es anterior a esta categoría.** [`ADR-02003`](../05-Arquitectura-Tecnica/Adrs/ADR-02003-Versionado-Y-Estabilidad-De-La-Superficie.md) §4 evaluó como alternativa la publicación en un repositorio de paquetes interno y la descartó con dos motivos: el intake la descarta explícitamente, y agregaría infraestructura a un producto que las fuentes declaran básico. El apartamiento, entonces, **no lo decide 09**: 09 lo registra y lo hace operativo.

**Y hay una razón de fondo para no simular los dos canales.** `Rules-Devops.md` §4.8 declara anti-patrón confundir publicación con despliegue. Declarar acá un canal `preview` y un canal `stable` sin feed detrás sería la versión inversa del mismo error: **inventar publicación donde sólo hay compilación**. Un canal es un destino del que alguien retira un artefacto; acá nadie retira nada, porque el consumidor lo obtiene por referencia de proyecto dentro de la misma construcción.

### 8.2 `GeometriaFactory-Application`

**Ninguno propio de despliegue y ninguno de publicación.** Las afirmaciones que lo sostienen son de la fuente:

| Afirmación | Dónde está declarada |
| --- | --- |
| No tiene unidad de despliegue propia: se compila dentro del artefacto de agrupación y **viaja embebido en la unidad desplegable del servidor propio, por la vía de `GeometriaFactory-Api`** | `05` §5, primera fila |
| **Ninguna dependencia de infraestructura**: no requiere base de datos, ni almacén de secretos, ni servicio externo. Todo lo que necesita del exterior entra por los **cuatro** puertos | `05` §5, tercera fila |
| No se publica en ningún repositorio de paquetes: `redistribuible` es false | `05` §5, última fila; intake §13 y §17.1.P.7 · GeometriaFactory-Application |

| Ambiente o canal | Destino | Aprobador | Ventana o acuerdo de nivel de servicio |
| --- | --- | --- | --- |
| Contenedor de desarrollo | La máquina de quien construye, y el ejecutor del pipeline | Nadie: no hay promoción hacia él | No aplica |

**Una sola fila, y es la única honesta.** Este proyecto de código no ejecuta nada por su cuenta: es una biblioteca que se carga dentro de otro proceso. El único lugar donde su código corre solo es la batería de pruebas, y ésa corre en el contenedor de desarrollo.

### 1.1 Apartamiento declarado del modelo de la categoría

`Rules-Devops.md` §2.2 fija para el tipo `library` el modelo de canales `preview` / `stable` sobre feed único, y admite apartarse con un ADR que lo justifique. **El ADR existe y es [`ADR-04003`](../05-Arquitectura-Tecnica/Adrs/ADR-04003-Versionado-Y-Estabilidad-De-La-Superficie.md)**, cuyo §2 declara que el contrato se protege por compilación compartida, que **no se publica en ningún repositorio de paquetes** y que por eso no hay deprecación gradual ni versiones conviviendo.

**Declarar acá un `DEV`, un `QA` y un `PROD` sería duplicar los ambientes de `GeometriaFactory-Api` con otro nombre y otro dueño**, que es exactamente el anti-patrón que `Rules-Devops.md` §4.8 nombra: confundir publicación con despliegue. Los ambientes de ejecución donde este ensamblado termina son los de la unidad que lo embebe, y su dueño es la categoría 09 de `GeometriaFactory-Api`.

### 8.3 `GeometriaFactory-Infrastructure`

**Ninguno propio de despliegue y ninguno de publicación.** Las afirmaciones que lo sostienen son de la fuente:

| Afirmación | Dónde está declarada |
| --- | --- |
| No tiene unidad de despliegue propia: **viaja embebido en la unidad desplegable del servidor propio, por la vía de `GeometriaFactory-Api`** | `05` §5, primera fila |
| No se publica en ningún repositorio de paquetes: `redistribuible` es false | `05` §5, última fila; intake §13 |
| **Tres dependencias de infraestructura, y son las únicas**: el sistema de archivos donde vive el almacén, la fuente de material impredecible del sistema y la clave de firma provista desde afuera. **Ninguna es un servicio de red** | `05` §5, tercera fila |

| Ambiente o canal | Destino | Aprobador | Ventana o acuerdo de nivel de servicio |
| --- | --- | --- | --- |
| Contenedor de desarrollo | La máquina de quien construye, y el ejecutor del pipeline. Ahí corre la batería y el stage de verificación de transformaciones, **sobre almacenes desechables** | Nadie: no hay promoción hacia él | No aplica |

**La tercera fila de la primera tabla es la que hace corta a la segunda.** Este proyecto de código **no habla por red con nada**: el intake §17.1.P.3 · GeometriaFactory-Infrastructure lo declara —«No aplica: no expone endpoints. Consume el sistema de archivos donde vive el archivo […] y nada más», con la elisión del nombre del motor de almacenamiento marcada, por la convención del corpus de no nombrar stacks en prosa; y **el validador de figuras no hace red**—. Un ambiente se distingue de otro por qué servicios alcanza; acá no hay ninguno que alcanzar.

### 1.1 Apartamiento declarado del modelo de la categoría

`Rules-Devops.md` §2.2 fija para el tipo `library` el modelo de canales `preview` / `stable` sobre feed único, y admite apartarse con un ADR que lo justifique. **Acá no hay feed**: el intake §17.1.P.7 · GeometriaFactory-Infrastructure declara la estrategia idéntica a §17.1.P.7 · GeometriaFactory-Domain, sin publicación, y §13 lo generaliza al producto entero.

**Y falta el instrumento que la regla nombra, así que se declara en lugar de darse por cubierto.** Las otras tres bibliotecas del producto anclan este mismo apartamiento en su `ADR-06003`; **este proyecto de código no tiene ninguna ADR sobre publicación ni sobre canales** —sus siete, `ADR-06001` a `ADR-06007`, tratan adaptadores, almacén, comparación de correos, derivación de clave, contraseña provisoria, lectura tolerante y transformaciones—, de modo que la cita al intake **sustituye** al ADR que `Rules-Devops.md` §2.2 pide y no lo reemplaza formalmente. El apartamiento es sustantivamente correcto —no hay feed, y no lo hay por decisión del producto—; lo que falta es el instrumento. **Queda registrado como `PD-05`** en [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §10, con la categoría 05 de este proyecto de código como dueña.

**Declarar acá un `DEV`, un `QA` y un `PROD` sería duplicar los ambientes de `GeometriaFactory-Api` con otro nombre y otro dueño**, que es el anti-patrón que `Rules-Devops.md` §4.8 nombra. El ambiente de ejecución donde este ensamblado termina es el del servidor propio, y su dueño es la categoría 09 de `GeometriaFactory-Api`.

**Y una precisión que este proyecto de código sí tiene y ninguna otra biblioteca del producto**: aunque no tenga ambientes propios, **es el que impone más restricciones sobre el ambiente ajeno**. El almacén va a un **volumen persistente y nunca dentro de la imagen**, el modo de diario está declarado, la concurrencia de escritura es de **escritor único** y las transformaciones se aplican **al arrancar** (intake §17.1.P.4 · GeometriaFactory-Infrastructure). Todo eso condiciona cómo se arma la unidad desplegable, y está recogido en [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §9.

## 9. El único ambiente que existe: el contenedor de desarrollo

### 9.1 `GeometriaFactory-Domain`

| Aspecto | Decisión | Fundamento |
| --- | --- | --- |
| Dónde ocurre todo el ciclo | Dentro del contenedor de desarrollo | Intake, encabezado de la Parte C, y §10: el host de desarrollo **no tiene ni va a tener** instalado el kit de desarrollo, y ningún guion puede asumirlo en el host |
| Plataforma objetivo | `net10.0` sin sufijo de plataforma, sobre el sistema operativo del contenedor, que es el mismo del servidor del backend | Intake §17.1.P.9 · GeometriaFactory-Domain |
| Dependencias de infraestructura | **Ninguna.** No requiere base de datos, ni almacén de secretos, ni servicio externo | `05` §5, tercera fila |
| Base de datos para pruebas | **Ninguna.** `tiene_persistencia` es false | [`../08-Calidad-Y-Pruebas/Estrategia-Testing.md`](../08-Calidad-Y-Pruebas/Estrategia-Testing.md) §7 |
| Definición del contenedor | `.devcontainer/devcontainer.json`, en la raíz del repositorio | Intake §16 |

**El contenedor de desarrollo no es un ambiente de despliegue disfrazado.** No sirve tráfico, no tiene URL y nadie promociona nada hacia él: es donde se construye y se prueba. Llamarlo `DEV` habría abierto la puerta a que alguien pidiera un `QA` detrás.

## 10. Dónde viaja este ensamblado

### 10.1 `GeometriaFactory-Application`

Es la tabla que reemplaza a la de ambientes, y dice lo que un lector de esta categoría necesita saber:

| Destino | Cómo llega | Quién es dueño de ese despliegue |
| --- | --- | --- |
| El proceso del **servidor propio** | Embebido en la imagen del backend, construida desde `deploy/Dockerfile` multietapa (intake §16), por la vía de `GeometriaFactory-Api` | Categoría 09 de `GeometriaFactory-Api` |
| El proceso del **hosting público** | **No llega.** El front no lo referencia: sus dependencias son `GeometriaFactory-Contracts` y `GeometriaFactory-Visor` | — |

**La segunda fila es la que distingue a este proyecto de código de `GeometriaFactory-Contracts`.** Aquél se carga en los dos procesos y por eso una decisión de plataforma del front lo alcanza; **éste llega a uno solo**. La consecuencia operativa es directa y está en [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §9: un cambio suyo **no obliga a republicar el front**.

### 10.2 `GeometriaFactory-Infrastructure`

| Destino | Cómo llega | Quién es dueño de ese despliegue |
| --- | --- | --- |
| El proceso del **servidor propio** | Embebido en la imagen del backend, construida desde `deploy/Dockerfile` multietapa (intake §16), por la vía de `GeometriaFactory-Api` | Categoría 09 de `GeometriaFactory-Api` |
| El proceso del **hosting público** | **No llega.** El front no lo referencia, y no podría: `05` §5 declara que **nadie más que la composición de raíz de `GeometriaFactory-Api` lo referencia** | — |

**La segunda fila tiene una consecuencia de seguridad que conviene decir explícita.** Este ensamblado contiene la derivación de contraseñas y la emisión de accesos firmados. Que **no llegue al proceso del hosting** significa que esas dos piezas **nunca se despliegan en la máquina de terceros**: viven sólo en el servidor propio, que es donde vive el dato. Es una propiedad de la topología del intake §14 y no una decisión de esta categoría, pero es la que hace que un compromiso del hosting no exponga la capacidad de emitir accesos.

## 11. Configuración, y el respaldo que no se fija acá

### 11.1 `GeometriaFactory-Infrastructure`

| Aspecto | Decisión | Fundamento |
| --- | --- | --- |
| Ubicación del almacén | **Configurable, y la configuración la provee `GeometriaFactory-Api`.** En producción, un volumen persistente | Intake §17.1.P.4 · GeometriaFactory-Infrastructure; `05` §5 |
| Modo de diario | **Declarado por la fuente**, no elegido acá | Intake §17.1.P.4 · GeometriaFactory-Infrastructure |
| Concurrencia de escritura | **Escritor único.** No es una configuración: es una propiedad del motor que el producto acepta como trade-off | Intake §17.1.P.4 · GeometriaFactory-Infrastructure y §17.1.P.12 · GeometriaFactory-Infrastructure; [`ADR-06002`](../05-Arquitectura-Tecnica/Adrs/ADR-06002-Un-Archivo-Escritor-Unico-Y-Una-Unidad-De-Trabajo-Por-Operacion.md) |
| Versionado del esquema | **Transformaciones aplicadas automáticamente al arrancar**, sobre almacén inexistente o desactualizado | Intake §17.1.P.4 · GeometriaFactory-Infrastructure y §17.1.P.11 · GeometriaFactory-Infrastructure punto 3 |
| Multi-inquilino | **No.** Una instancia, un curso, un administrador | Intake §17.1.P.4 · GeometriaFactory-Infrastructure; `INV-05` |
| Variables de entorno del pipeline | **Ninguna.** Los cuatro stages leen el repositorio, crean almacenes desechables y escriben informes y recuentos | Decisión de esta categoría, derivada de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.1 |

**El respaldo del almacén, que es lo único de operación que la fuente dejó abierto y dirigió acá.** El intake §17.1.P.4 · GeometriaFactory-Infrastructure lo declara como **copia del archivo con el diario activo, consistente**, y su **frecuencia «a definir por el docente»**; `PA-07` de `05` §11 lo registra como punto abierto y lo dirige a esta categoría junto con el Product Owner.

**Esta categoría no inventa una frecuencia.** Lo que sí aporta, porque le corresponde, es qué condiciones tiene que cumplir el respaldo para servir de algo:

| Condición | Fundamento |
| --- | --- |
| Se copia **el archivo con el diario activo**, y la copia es consistente. No se copia el archivo a mano mientras el proceso escribe | Intake §17.1.P.4 · GeometriaFactory-Infrastructure |
| **El respaldo es el único mecanismo del producto para volver atrás sobre datos.** Volver a una etiqueta revierte el código y no el almacén, y el guion de restablecimiento **deja el almacén vacío** | `05` §5; [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §7 |
| La copia **vive fuera del volumen que respalda**, o no protege del modo de falla más probable de un servidor domiciliario | **Decisión de esta categoría**, declarada como tal |
| **No se declara ninguna frecuencia, ninguna retención y ningún destino concreto** | Ninguna fuente los da, y el intake §10 declara «sin plazo». Un número puesto acá se propagaría como si fuera del producto |

**El punto abierto queda registrado como `PD-04`** en [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §10, con el Product Owner como quien lo cierra.

## 12. Secretos: la clave de firma que se recibe y no se busca

### 12.1 `GeometriaFactory-Infrastructure`

**Acá viven las dos piezas sensibles del producto** —la derivación de la contraseña y la emisión del acceso firmado (intake §17.1.P.5 · GeometriaFactory-Infrastructure)— y sin embargo **este proyecto de código no custodia ningún secreto**.

| Secreto, nombrado por su función | Dónde vive | Cómo llega | Qué pasa si no llega |
| --- | --- | --- | --- |
| **Clave de firma del acceso** | **Fuera del repositorio y fuera de la imagen**: variable de entorno o archivo montado. Se genera o se provee en el primer arranque | La provee `GeometriaFactory-Api` desde la configuración del ambiente | **Falla con la condición declarada.** `05` §5: este proyecto de código **la recibe y no la busca**; `QG-12` mide **0** claves generadas al vuelo |

**La cuarta columna es la decisión de diseño que esta categoría subraya.** Un adaptador que, ante la falta de la clave, generara una al vuelo **arrancaría bien y emitiría accesos que nadie más puede verificar**: el producto funcionaría hasta el primer reinicio y después dejaría de reconocer sus propios accesos, sin ningún error visible en el momento de la falla. Por eso la ausencia es una condición declarada y no un valor por defecto.

| Momento | Secretos | Fundamento |
| --- | --- | --- |
| Construcción | **Ninguno.** El restaurador toma dependencias de repositorios públicos; no hay publicación que autenticar | Intake §17.1.P.7 · GeometriaFactory-Infrastructure |
| Prueba | **Ninguno real.** Las contraseñas de los casos son ficticias, y los almacenes son desechables | [`../08-Calidad-Y-Pruebas/Estrategia-Testing.md`](../08-Calidad-Y-Pruebas/Estrategia-Testing.md) |
| Ejecución | **Uno, recibido y no custodiado**: la clave de firma | Intake §17.1.P.5 · GeometriaFactory-Infrastructure; `05` §5 |

**Ningún secreto entra al repositorio, ni en la integración continua.** El intake §17.1.P.5 · GeometriaFactory-Infrastructure lo declara sin excepción. **No se declara ninguna frecuencia de rotación**: ninguna fuente la da, y el gobierno del valor pertenece a la categoría 09 de `GeometriaFactory-Api`, que es la que lo provee al ambiente.

**Y una regla de higiene que alcanza al pipeline y no sólo al producto**: `QG-13` mide **0** mensajes o trazas con un secreto, la ruta del almacén o el texto del alumno. Eso incluye la salida de los cuatro stages: un registro de ejecución que imprimiera la ruta del almacén desechable o un fragmento de un escenario estaría produciendo, en la canalización, lo que el gate prohíbe en el producto.

## 13. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 2.1 | 2026-08-24 | **Migración normativa 10.0 → 13.3, fase M4** (`Audit/Plan-Migracion-10.0-a-13.3.md` 1.0 §4.2). Entra **§2.b, la aprobación de `plan` antes de `apply` como ítem propio**, que `Rules-Devops.md` **6.0** §4.4 separa de la herramienta por ser **política de proceso** y no consecuencia de ella. Se declara **no aplica** —§2.1 ya declaraba que no hay herramienta declarativa de infraestructura, con su fundamento en el intake §10— y se nombra **qué ocupa su lugar**: el pull request por etapa como punto de control bloqueante. **«No aplica» no es «diferido»**, y por eso no lleva los cuatro campos de §12.2: un ítem que nadie va a poder cerrar mientras no haya herramienta no es un pendiente, es una inaplicabilidad con su condición de reapertura escrita. Sube **minor**. |
| 2.0 | 2026-08-16 | **Consolidación de la fusión** (`Audit/Migracion-M10-Consolidacion-Fusion.md` 1.2 §4). Pasa de ser el documento de un proyecto de código a ser el de la **unidad de entrega**, con una subsección por proyecto y su texto transpuesto **sin reescritura**. Entra **§0**. Los absorbidos quedan archivados. Sube **major**. |
