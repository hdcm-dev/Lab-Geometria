# Reporte: despliegue del front a un hosting compartido gratuito

| Campo | Valor |
|---|---|
| Versión | 1.0 |
| Fecha | 2026-08-13 |
| Estado | **Aprobado** |
| Autor | Ingeniero DevOps Senior + Deploy Engineer (AG-09) |
| Origen | Pedido del Product Owner: dejar registro de la experiencia del primer despliegue real, para que **otro producto que despliegue en un hosting compartido no la redescubra** |
| Producto | Fábrica de Geometría |
| Objeto reportado | La publicación de `GeometriaFactory-Web` al hosting público somee.com, ocurrida y **en línea**, y las mediciones que habilitó |
| Naturaleza | **Experiencia de despliegue, medida.** No es una auditoría de documentos: es el registro de un acto que ocurrió, con su evidencia y con lo que salió distinto de lo previsto |
| Regla de secretos | **Ningún valor de credencial, usuario, dirección de canal ni ruta de FTP aparece en este reporte.** Todo se nombra por su función. La dirección pública `https://www.aplicada.somee.com` sí se nombra: es pública |

---

## Tabla de contenido

- [1. Qué se hizo, y cómo](#1-qué-se-hizo-y-cómo)
- [2. Qué se midió, y con qué resultado](#2-qué-se-midió-y-con-qué-resultado)
- [3. Qué salió distinto de lo previsto, y por qué](#3-qué-salió-distinto-de-lo-previsto-y-por-qué)
- [4. Lo que costó descubrir](#4-lo-que-costó-descubrir)
- [5. Qué debería estar en el framework SDD](#5-qué-debería-estar-en-el-framework-sdd)
- [6. Qué NO prueba este reporte](#6-qué-no-prueba-este-reporte)
- [7. Dónde quedó documentado cada cosa](#7-dónde-quedó-documentado-cada-cosa)
- [8. Control de cambios](#8-control-de-cambios)

---

## 1. Qué se hizo, y cómo

**Se publicó la pieza pública del producto a un hosting compartido gratuito, por FTP, y quedó en línea.** El destino es somee.com y la dirección pública es `https://www.aplicada.somee.com`.

**El mecanismo, contrastado contra la base de conocimiento del proveedor y contra la práctica.** Las dos fuentes públicas son el [artículo 203](https://somee.com/DOKA/Help/Article/203/Deploy_ASP.Net_Core_application) y el [artículo 219](https://somee.com/doka/Help/Article/219/How_do_I_deploy_my_application), y lo que dicen se verificó ejerciéndolo:

| # | Hecho del mecanismo | Grado de generalidad |
|---|---|---|
| 1 | **Se publica por FTP a la carpeta raíz del sitio, sin subcarpeta.** El contenido de la salida de publicación va directo a la raíz que expone el canal | **Propio de este proveedor**, pero la familia —«el canal expone una raíz y el artefacto va ahí, no colgado de un subdirectorio»— es común al hosting compartido |
| 2 | **El archivo de configuración del servidor de información es requisito duro.** Sin él, el hosting **no sabe arrancar el sitio y devuelve 500** | **Generalizable.** En hosting compartido, quien arranca el proceso es el servidor del proveedor, y necesita que el artefacto le diga cómo. Un artefacto que arranca solo en la máquina de desarrollo **no trae esa información consigo** |
| 3 | **El archivo que genera el comando de publicación sirve tal cual**: declara el manejador, apunta al ensamblado de la pieza pública y usa modelo **in-process** | **Generalizable.** La cadena de herramientas ya sabe emitirlo; el error típico no es escribirlo mal, es **no subirlo** |
| 4 | **La versión de plataforma se elige en el panel de la cuenta**, y el archivo de configuración tiene que coincidir con esa elección | **Generalizable.** En hosting compartido hay **configuración fuera del repositorio** que ninguna infraestructura declarativa del producto controla, y que puede contradecir al artefacto |
| 5 | **La subida espeja con borrado de lo anterior**, y por eso hubo **respaldo completo previo de los 169 archivos** de la aplicación que ocupaba la carpeta | **Generalizable.** El espejado con borrado **no es reversible desde el hosting**, y lo borrado puede no ser del producto que se despliega |

**El procedimiento efectivo, en el orden en que se corrió:** inventariar lo que ocupaba la carpeta raíz del sitio → bajar el respaldo completo y comprobar que el recuento coincide → espejar la salida de publicación sobre la raíz con borrado → verificar interrogando la dirección pública. **La subida terminada sin error no es la verificación**: el criterio del producto exige que el despliegue termine comprobando que la dirección pública responde, y no en la subida.

## 2. Qué se midió, y con qué resultado

Todo lo de esta tabla se midió **contra el hosting real**, el 2026-08-13, sobre la dirección pública y sin necesitar ningún secreto.

| Medición | Resultado |
|---|---|
| **`PT-01.a` · la dirección pública responde** | **PASA.** `https://www.aplicada.somee.com/estado` → **200** |
| **Versión de plataforma soportada**, incógnita `[A VERIFICAR]` del intake §17.6.P.9 | **RESUELTA: el hosting soporta `net10.0`.** **No hizo falta bajar la versión objetivo del front**, que era la salida declarada para el caso contrario |
| Raíz del sitio | **404.** En la etapa `a` hay **una sola** ruta servida, la página de estado. No es un defecto de la publicación |
| **`RA-03` en producción** | **Se sostiene.** **Cero** apariciones de la dirección y del puerto internos en el HTML público |
| Estado degradado | **Se ve correctamente.** El servicio de datos corre en el servidor propio y el front público no lo alcanza; la página lo dice en lugar de mostrar un dato inventado |
| **`PT-01.b` sobre el hosting real** | **El hosting NO ofrece WebSockets.** La negociación del circuito devuelve **dos** transportes: `ServerSentEvents` y `LongPolling`. En desarrollo ofrecía los **tres** |

**Los comandos, para que cualquiera vuelva a correrlos:**

```bash
curl -sS -o /dev/null -w "%{http_code}\n" https://www.aplicada.somee.com/estado
curl -sS -o /dev/null -w "%{http_code}\n" https://www.aplicada.somee.com/
curl -sS -X POST "https://www.aplicada.somee.com/_blazor/negotiate?negotiateVersion=1" -H "Content-Length: 0"
```

## 3. Qué salió distinto de lo previsto, y por qué

### 3.1 El hallazgo principal: no hay WebSockets en producción, y la medición de desarrollo no lo anticipaba

**Lo previsto.** La medición en el entorno de desarrollo contenido, con navegador real, había dado el mejor de los tres estados posibles: el servidor ofrecía los **tres** transportes y el navegador elegía **WebSockets**. El semáforo se declaró **verde**.

**Lo que ocurrió.** El hosting **no ofrece WebSockets**. No es que el navegador prefiera otra cosa ni que una red intermedia lo bloquee: **el servidor del proveedor no lo pone en la oferta de la negociación**. La consecuencia es dura y hay que decirla sin rodeos: **la sesión interactiva del producto no va a usar WebSockets en producción**.

**Por qué salió distinto.** Porque **el entorno de desarrollo mide la capacidad del proceso propio, y en producción la capacidad la fija el proveedor**. El transporte de la sesión interactiva no es una propiedad del código: es una propiedad del servidor de información que está delante, que en desarrollo es el mismo proceso del producto y en hosting compartido es infraestructura ajena. **Ninguna medición local podía anticiparlo.**

**Por qué no es una crisis, y esto también hay que decirlo.** El repliegue **ya estaba medido y funcionando** antes de publicar: con el túnel del WebSocket bloqueado a propósito, el circuito repliega a long polling, la sesión se sostiene, los veinte minutos se sostienen y la reconexión tras un corte de red vuelve **al mismo circuito**. La especificación tenía ese escenario declarado por adelantado como **aceptable y no motivo de rediseño**. Es decir: **el producto había ejercido la contingencia antes de necesitarla, y la contingencia resultó ser el caso normal.** El semáforo se revisó de verde a **amarillo estable**, no a rojo.

**Lo que sí se invalidó, y es el costo real del hallazgo.** La latencia percibida medida en desarrollo —mediana **6 ms** por WebSockets contra **8 ms** por long polling, medida del clic al repintado— **es de bucle local y no es extrapolable a la red real**. Ahí el trayecto de red es despreciable; en producción no lo es, y long polling **paga una petición completa por mensaje**, que es exactamente donde el costo aparece cuando hay red de por medio. **Los 8 ms no son la latencia de producción y no se pueden citar como tal.** La latencia percibida sobre el hosting real **sigue sin medir**.

### 3.2 Lo previsto que se confirmó, y sirve de contraste

Dos cosas se habían anticipado antes de publicar, midiendo la publicación levantada en local, y **el hosting real las confirmó**:

| Anticipado | Confirmado sobre el hosting |
|---|---|
| En la etapa `a` la pieza pública sirve **una sola ruta** y la raíz no está servida: una comprobación apuntada a la raíz desnuda **daría rojo con una publicación correcta** | **Confirmado**: raíz **404**, ruta servida **200**. La comprobación se apuntó a la ruta que la etapa sirve, y dio verde **sin ablandar el criterio** |
| El paso de comprobación posterior a la subida es el que separa «subida terminada» de «producto en pie» | **Confirmado por el modo de falla del punto 2 de §1**: un **500** con la subida terminada sin error es un estado perfectamente alcanzable |

**La lección de contraste es la que más se puede reutilizar: lo que se anticipó midiendo se confirmó; lo que se dio por bueno extrapolando, no.** El transporte se extrapoló y falló; la ruta servida se midió y se confirmó.

### 3.3 Un detalle del destino que nadie había previsto

**El hosting inyecta contenido propio en la página servida**: un enlace de atribución al proveedor y un guion de publicidad cargado desde un host suyo. Comprobado sobre el HTML público el 2026-08-13.

No compromete `RA-03` —no hay ni una aparición de la dirección ni del puerto internos—, pero tiene dos consecuencias que conviene saber de antemano: **el HTML público no es byte a byte el que la publicación generó**, de modo que cualquier verificación que compare el servido contra el generado va a fallar por un motivo ajeno al producto; y **la página pública carga un recurso de un tercero** que el producto no controla.

## 4. Lo que costó descubrir

Registrado por lo que costó, no por lo que ocupa escrito.

| Qué | Por qué costó |
|---|---|
| **Que el archivo de configuración del servidor es requisito duro, y que su ausencia da 500** | Porque **el síntoma no señala la causa**. Un 500 con la subida terminada sin error se parece a un problema de la aplicación, y no lo es: es que el hosting no sabe arrancarla. Sin saberlo de antemano, se busca en el lugar equivocado |
| **Que la versión de plataforma se elige en un panel fuera del repositorio** | Porque es **configuración invisible desde el árbol de fuentes**. El producto no tiene infraestructura declarativa que la cubra, y un desajuste entre el panel y el artefacto **produce el mismo 500** que la causa anterior. Dos causas distintas, un solo síntoma |
| **Que el hosting no ofrece WebSockets** | Porque **sólo se descubre publicando**. Ninguna medición local lo anticipa, la documentación pública del proveedor no lo declaraba en los artículos consultados, y el síntoma es silencioso: **la aplicación funciona igual**, sólo que por otro transporte. Si el repliegue no estuviera ejercido, el hallazgo habría llegado como una degradación inexplicable en vez de como un dato |
| **Que la subida espeja con borrado y hay que respaldar antes** | Porque **el costo de descubrirlo tarde es irreversible**. La carpeta raíz del sitio estaba ocupada por otra aplicación —169 archivos— y el hosting no ofrece de dónde recuperarla |

## 5. Qué debería estar en el framework SDD

Cinco piezas, todas derivadas de lo de arriba y ninguna inventada. Están escritas para **cualquier producto que despliegue en un hosting compartido**, no para éste.

| # | Qué incorporar | Por qué, con el hecho que lo sostiene |
|---|---|---|
| **F-1** | **Una lista de comprobación de «capacidades del hosting» que se mide publicando, no declarando**, con al menos: versión de plataforma soportada, transportes que el servidor de información ofrece, límites de proceso, y si el proveedor inyecta contenido en lo servido. **Todas se miden contra la dirección pública, sin secretos.** | §3.1: el transporte se dio por bueno desde una medición local y el hosting lo desmintió. §3.3: la inyección de contenido no la había previsto nadie |
| **F-2** | **La regla de que una medición hecha en el entorno de desarrollo se rotula con su alcance y no vale como medición de producción cuando lo medido es una capacidad del servidor ajeno.** Distinguir dos clases: propiedades **del código**, que se trasladan, y propiedades **del entorno de ejecución**, que no | §3.1: transporte y latencia son propiedades del entorno, no del código. La documentación del producto ya rotulaba el alcance, y ese rótulo es lo que evitó que la corrección fuera una contradicción |
| **F-3** | **La obligación de que todo canal de despliegue por espejado declare su procedimiento de respaldo previo, con inventario y verificación de recuento**, antes de la primera subida | §1 punto 5 y §4: el espejado borra, no es reversible desde el hosting, y lo borrado puede no ser del producto |
| **F-4** | **Un catálogo de modos de falla del hosting compartido con su síntoma**, empezando por el más caro: **500 en la dirección pública con la subida terminada sin error**, y sus dos causas conocidas —falta el archivo de configuración del servidor; la versión elegida en el panel no coincide con la del artefacto—. Escrito **por síntoma**, que es como lo encuentra quien despliega | §4: dos causas distintas producen un solo síntoma, y el síntoma no señala ninguna de las dos |
| **F-5** | **La práctica de ejercer la contingencia declarada antes de necesitarla.** Cuando la especificación declara una alternativa aceptable ante un escenario adverso, **medirla funcionando** en la etapa que la declara, no cuando el escenario ocurra | §3.1: el repliegue estaba ejercido y funcionando antes de publicar, y por eso el hallazgo llegó como un dato y no como una crisis. **Es lo que convirtió el peor hallazgo del despliegue en un cambio de semáforo** |

**Y una observación de método que las cinco comparten.** Ninguna de las cinco es una decisión de arquitectura: son **obligaciones de medición y de registro**. El producto ya tenía las decisiones correctas —el repliegue declarado aceptable, la comprobación posterior a la subida, la ruta servida anticipada—; lo que faltaba era **saber qué preguntarle al hosting antes de confiar en lo medido en casa**.

## 6. Qué NO prueba este reporte

**Es un despliegue, en un proveedor, con un producto.** No es una muestra.

| Lo que no prueba | Por qué |
|---|---|
| Que otros hosting compartidos se comporten igual | Los puntos 1 y 5 de §1 son del proveedor. Lo generalizable está marcado como tal en esa misma tabla, y lo que no, también |
| Que la sesión interactiva se sostenga en producción | **`PT-01.c` sigue sin medir sobre el hosting**: los veinte minutos y el corte de red no se corrieron ahí, y es donde vive el riesgo de que el proceso del hosting gratuito recicle la sesión. **Con un motivo más desde este reporte**: lo medido en desarrollo fue sobre un circuito por WebSockets, y el de producción no lo es |
| Cuál es la latencia percibida en producción | **No está medida** (§3.1). La de desarrollo es de bucle local y este reporte la declara **no extrapolable** |
| Que la salida del front público hacia el servidor propio funcione | Al momento de medir, el servicio de datos corría en local y el front público **no lo alcanzaba**. Lo que sí quedó comprobado es que el **estado degradado se ve correctamente**, que es el comportamiento correcto ante esa situación |

## 7. Dónde quedó documentado cada cosa

| Contenido | Documento | Sección |
|---|---|---|
| El mecanismo del hosting, el modo de falla del 500 y la dependencia del panel | [`../Proyectos/GeometriaFactory-Web/09-Devops/Guia-Publicacion-Front-Ftp.md`](../Proyectos/GeometriaFactory-Web/09-Devops/Guia-Publicacion-Front-Ftp.md) **1.2** | §2.1 |
| El procedimiento de respaldo previo y espejado | La misma | §2.2 |
| Las mediciones sobre el hosting real y la corrección de `PT-01.b` | [`../00-Contexto/Compatibilidad-Plataformas.md`](../00-Contexto/Compatibilidad-Plataformas.md) **1.4** | §2.6 |
| El cierre de `PT-01.a` y el recuento de criterios de la etapa | [`../Producto/Plan-Etapa-A.md`](../Producto/Plan-Etapa-A.md) **1.7** | §5.2 |
| La experiencia reutilizable por otro producto | Este reporte | §3, §4 y §5 |

## 8. Control de cambios

| Versión | Fecha | Descripción | Autor |
|---|---|---|---|
| 1.0 | 2026-08-13 | Emisión inicial. Registra el **primer despliegue real del producto**, ocurrido y en línea: publicación de la pieza pública por FTP a un hosting compartido gratuito, con el mecanismo contrastado contra los artículos 203 y 219 de la base de conocimiento del proveedor y contra la práctica, y con el procedimiento de respaldo previo de **169 archivos** ante un canal que **espeja con borrado**. Registra las **seis** mediciones sobre el hosting real: **`PT-01.a` pasa** con **200** en la ruta servida y **404** en la raíz, la incógnita de versión de plataforma queda **resuelta** —soporta `net10.0`, sin bajar la versión objetivo del front—, **`RA-03` se sostiene en producción** y el **estado degradado se ve correctamente**. Deja como **hallazgo principal** que **el hosting no ofrece WebSockets** —sólo `ServerSentEvents` y `LongPolling`, contra los tres de desarrollo—, de modo que **la sesión interactiva no usa WebSockets en producción**; explica por qué ninguna medición local podía anticiparlo, por qué no obliga a rediseño —el repliegue estaba ejercido y funcionando, y la especificación lo declaraba aceptable— y **qué se invalidó**: la latencia percibida de desarrollo, de bucle local, **no es extrapolable**. Agrega el detalle no previsto de que el proveedor **inyecta contenido propio en la página servida**. Enumera **cuatro** cosas que costaron descubrir con el motivo de cada una, y propone **cinco** incorporaciones al framework SDD, escritas para cualquier producto sobre hosting compartido. Declara en §6 **qué no prueba**, empezando por `PT-01.c`, que **sigue sin medir sobre el hosting**. | Ingeniero DevOps Senior + Deploy Engineer (AG-09) |
