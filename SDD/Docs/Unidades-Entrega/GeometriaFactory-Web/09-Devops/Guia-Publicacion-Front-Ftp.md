# Guía de publicación — Front por FTP

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** Guia-Publicacion-Front-Ftp.md
**Versión:** 1.3
**Estado:** Aprobado
**Fecha:** 2026-08-13
**Autor:** Ingeniero DevOps Senior + Deploy Engineer (AG-09)
**Tipo de proyecto de código (D8):** `web-monolith`
**Tipo de artefacto:** `Front-Ftp`
**Trazabilidad upstream:** [`../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`](../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md) 1.0 §5; [`../05-Arquitectura-Tecnica/Adrs/ADR-10007-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md`](../05-Arquitectura-Tecnica/Adrs/ADR-10007-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md) 1.0 §2, §7 y §8; [`../08-Calidad-Y-Pruebas/Definition-Of-Done.md`](../08-Calidad-Y-Pruebas/Definition-Of-Done.md) 1.1 §1.4; [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) 1.1 §3; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.21** §13, §16, §17.2.P.5 · GeometriaFactory-Web, §17.2.P.7 · GeometriaFactory-Web, §17.2.P.8 · GeometriaFactory-Web, §17.2.P.9 · GeometriaFactory-Web, §17.2.P.10 · GeometriaFactory-Web y §17.2.P.12 · GeometriaFactory-Web
**Trazabilidad downstream:** [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md), [`Entornos-Deploy.md`](Entornos-Deploy.md); `11-Documentacion` cuando se emita

---

## Tabla de contenido

- [0. Qué significa «publicación» acá, y qué no](#0-qué-significa-publicación-acá-y-qué-no)
- [1. Pre-requisitos](#1-pre-requisitos)
- [2. Comando y stage de publicación](#2-comando-y-stage-de-publicación)
  - [2.1 El mecanismo real del hosting, comprobado en el despliegue del 2026-08-13](#21-el-mecanismo-real-del-hosting-comprobado-en-el-despliegue-del-2026-08-13)
  - [2.2 Procedimiento de respaldo previo y espejado](#22-procedimiento-de-respaldo-previo-y-espejado)
- [3. Verificación posterior a la publicación](#3-verificación-posterior-a-la-publicación)
- [4. Reversión](#4-reversión)
- [5. Métricas](#5-métricas)
- [6. Control de cambios](#6-control-de-cambios)

---

## 0. Qué significa «publicación» acá, y qué no

**No es una publicación en un repositorio de paquetes.** El intake §13 declara que **ningún proyecto de código del producto se publica como paquete redistribuible** y que los dos artefactos entregables son **una imagen de contenedor y una publicación subida por FTP**. Éste es el segundo, y `05` §5 lo declara «la publicación de la aplicación en el hosting público, con dominio y transporte seguro».

Lo que sí hay es **un despliegue**, y tiene pre-requisitos, procedimiento, verificación y reversión propios. Esta guía los documenta con la estructura que `Rules-Devops.md` §4.5 exige.

**`<tipo-artefacto>` = `Front-Ftp`.** `Rules-Devops.md` §2.2 fija `image-docker o artefacto desplegable equivalente` para el tipo `web-monolith`, y §3.1 declara que la lista de tipos **no es cerrada**, admitiendo incorporar tipos nuevos respetando el formato del nombre y la convención de prefijo **según familia**. Este artefacto **no es una imagen de contenedor** —la imagen del producto es la del backend— y **no pertenece a ninguna de las seis familias declaradas**, porque no se distribuye por ningún gestor: se sube por FTP a un destino único. Se declara con nombre propio y sin prefijo de familia, y esta declaración es la constancia de por qué. Es el mismo tratamiento que `GeometriaFactory-Visor` dio a su `Bundle-Visor`.

**Un artefacto, y sólo uno.** `Rules-Devops.md` §2.2 admite un `openapi` versionado como artefacto secundario para servicios; acá no aplica, y tampoco aplicaría en el backend: el intake §17.1.P.3 · GeometriaFactory-Api declara que **no hay versionado de rutas porque no hay clientes de terceros**, y el contrato compartido es un ensamblado, no una descripción publicada.

## 1. Pre-requisitos

| Pre-requisito | Detalle | Fundamento |
| --- | --- | --- |
| **Cuenta en el hosting público** | El servicio gratuito con servidor de información, transporte seguro y dominio público. Se contrata y se configura **por fuera del repositorio**: no hay infraestructura declarativa | Intake §17.2.P.9 · GeometriaFactory-Web; [`Entornos-Deploy.md`](Entornos-Deploy.md) §3 |
| **Credenciales del canal de publicación**, nombradas por su función | Viven como **secreto del repositorio**. Su alcance mínimo es escribir en el directorio de la aplicación del hosting, y nada más. **El valor no aparece en ningún documento de esta cadena** | Intake §17.2.P.5 · GeometriaFactory-Web y §16 |
| **Dirección base del servicio de datos**, nombrada por su función | Vive como **secreto del repositorio** y se inyecta al publicar. **La dirección real del servidor propio no se versiona** | Intake §17.2.P.5 · GeometriaFactory-Web; [`ADR-10007`](../05-Arquitectura-Tecnica/Adrs/ADR-10007-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md) §2 |
| **Contenedor de desarrollo levantado**, para la publicación manual | Es donde corren las dos cadenas de herramientas: el equipo anfitrión no las tiene instaladas | Intake §10 y encabezado de la Parte C |
| **Versión de plataforma del hosting comprobada** — **RESUELTA el 2026-08-13** | Estaba **[A VERIFICAR]** en la fuente y se resolvió **midiendo `PT-01.a`**, no decidiendo: **el hosting soporta `net10.0`** y **no hizo falta bajar la versión objetivo del front**. La salida declarada para el caso contrario —bajar la del front, nunca la del backend— **no se ejerció**. Queda el pre-requisito operativo de §2.1 fila 4: **la versión se elige en el panel de la cuenta** y el `web.config` tiene que coincidir | Intake §17.2.P.9 · GeometriaFactory-Web y §17.2.P.10 · GeometriaFactory-Web; `PA-02` de `05` §11; §2.1 |
| **Versión de la biblioteca de componentes de interfaz anclada** | También **[A VERIFICAR]** en la fuente; se ancla al crear el andamiaje | Intake §17.2.P.1 · GeometriaFactory-Web; `PA-01` de `05` §11, `BT-10002` |

**Ningún pre-requisito de esta guía se cumple escribiendo un valor acá.** Los dos secretos se nombran por su función y se declara dónde vive el valor; las dos marcas **[A VERIFICAR]** se resuelven midiendo. **Desde el 2026-08-13 una de las dos está resuelta** —la versión de plataforma del hosting—, y se resolvió del único modo admitido: publicando y midiendo.

**El inventario completo de secretos que el flujo consume, leído sobre el flujo y no supuesto.** El intake §17.2.P.5 · GeometriaFactory-Web declara **dos** por su función —la dirección del servicio de datos y las credenciales del canal—, y ésas son las **dos** que la tabla de arriba exige. El flujo escrito en `.github/workflows/deploy-front-ftp.yml` consume además **el destino dentro del hosting**, que acompaña a las credenciales del canal, y **la dirección pública que el paso 8 interroga**, que es la que `QG-10003` mide. Se dejan nombrados por su función y **sin ningún valor**, porque quien vaya a publicar necesita saber que **el flujo se detiene si falta cualquiera de ellos**:

| Secreto, nombrado por su función | Paso que lo usa | Qué pasa si falta |
| --- | --- | --- |
| Dirección base del servicio de datos | 6 | El paso **se detiene antes de escribir nada**: comprueba que el valor no esté vacío. Comprobado corriendo el 2026-08-13 |
| Credenciales del canal de publicación, y **el destino dentro del hosting** | 7 | La subida no ocurre |
| **Dirección pública** | 8 | La comprobación final no tiene qué interrogar, y el flujo no cierra su gate |

**Y una precisión sobre el valor de la última, que no es cosmética.** El paso 8 exige respuesta correcta de **la dirección que se le da**. En la etapa `a` la pieza pública sirve **una sola ruta —la página de estado— y la raíz no está servida**: se comprobó corriendo el 2026-08-13, levantando la publicación resultante en local, que la raíz responde **404** y la página de estado responde **200**. Si el valor de este secreto es la raíz desnuda, **el paso 8 dará rojo con una publicación correcta** hasta que la etapa `b` ponga las rutas navegables. **No se resuelve ablandando el paso** —eso reabriría exactamente el modo de falla que el intake §17.2.P.8 · GeometriaFactory-Web vino a cerrar—: se resuelve fijando el valor del secreto en una ruta que la etapa sirva.

## 2. Comando y stage de publicación

**El acto de publicar es el flujo de trabajo del repositorio**, `.github/workflows/deploy-front-ftp.yml`, que el árbol del intake §16 declara. Sus **ocho** pasos y el gate de cada uno están en [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.1 y **no se repiten acá**; lo que esta guía agrega es cómo se lo invoca y qué necesita.

| Camino | Cómo se invoca | Cuándo se usa |
| --- | --- | --- |
| **Automático** | Fusión a la rama principal con cambios bajo las rutas del filtro | Es el camino normal al cerrar una etapa |
| **Manual** | Disparo manual del mismo flujo | Cuando el cambio no está bajo las rutas del filtro —hoy, un cambio del ensamblado de contratos— y cuando hay que republicar sin que haya cambiado nada, por ejemplo tras rotar el secreto de la dirección del servicio de datos |

**Los dos caminos corren el mismo flujo entero**, incluidos los pasos 4 y 8. No hay un camino corto que suba sin regenerar el bundle o sin comprobar: `QG-02` y `QG-03` lo impiden.

**Variables requeridas por la publicación: todas secretas y todas nombradas por su función** —la dirección base del servicio de datos y las credenciales del canal, que son las **dos** que el intake §17.2.P.5 · GeometriaFactory-Web declara, más el destino dentro del hosting y la dirección pública que el paso 8 interroga, que el flujo escrito consume y §1 inventaría—. Ninguna otra, y **ninguna con valor en esta cadena de documentos**. El bundle del visor **no requiere ninguna**: no lee configuración propia (`RA-02`).

**Construcción local para depurar el flujo.** Los guiones del repositorio que el intake §16 lista permiten reproducir los pasos 1 a 5 en la máquina de quien construye, dentro del contenedor de desarrollo: `scripts/build-visor.sh` para el bundle y `scripts/build.sh` para la construcción encadenada. **Los pasos 6, 7 y 8 no se reproducen en local**, porque involucran el secreto y el destino real; intentar reproducirlos exigiría el secreto en la máquina, que es lo que el intake §17.2.P.5 · GeometriaFactory-Web evita.

### 2.1 El mecanismo real del hosting, comprobado en el despliegue del 2026-08-13

**El despliegue ocurrió y está en línea**: la pieza pública se subió por FTP al hosting y responde en `https://www.aplicada.somee.com/estado`. Esta subsección documenta **cómo funciona el destino**, que hasta 1.1 esta guía trataba como una caja negra. Lo de acá está contrastado contra la base de conocimiento pública del proveedor —[artículo 203](https://somee.com/DOKA/Help/Article/203/Deploy_ASP.Net_Core_application) y [artículo 219](https://somee.com/doka/Help/Article/219/How_do_I_deploy_my_application)— **y contra la práctica del despliegue real**. La dirección pública se nombra porque **es pública**; ningún otro valor del canal aparece acá.

| # | Hecho del mecanismo | Consecuencia operativa |
| --- | --- | --- |
| 1 | **Se publica por FTP a la carpeta raíz del sitio, sin subcarpeta.** El contenido de la salida de publicación va directo a la raíz que el canal expone | El destino dentro del hosting —secreto nombrado en §1— es **la raíz del sitio**, no un subdirectorio. Colgar la aplicación de una subcarpeta no la sirve |
| 2 | **`web.config` es requisito duro.** Sin él, el hosting **no sabe arrancar el sitio y devuelve 500** | No es un archivo opcional ni un residuo de la publicación: **es el que declara cómo se arranca**. Si se sube una selección de archivos y se lo deja afuera, el síntoma es un **500** sobre una publicación por lo demás correcta |
| 3 | **El `web.config` que genera `dotnet publish` sirve tal cual.** No hay que escribirlo a mano ni retocarlo | Declara el manejador `aspNetCore` sobre `AspNetCoreModuleV2`, apunta al `.dll` de la pieza pública y usa modelo **in-process**. Verificable abriendo el archivo dentro del directorio publicado |
| 4 | **La versión de plataforma se elige en el panel de la cuenta**, y el `web.config` tiene que coincidir con lo que ahí quedó elegido | Es configuración **fuera del repositorio**, como la cuenta misma (§1). Un desajuste entre la versión elegida en el panel y la que la publicación espera es otra vía al mismo **500** |

**El modo de falla que hay que reconocer, escrito una sola vez y sin rodeos: un 500 en la dirección pública, con la subida terminada sin error, apunta al arranque y no al contenido.** Las dos causas comprobadas son las filas 2 y 4 de la tabla: falta el `web.config`, o la versión elegida en el panel no es la que la publicación espera. **La verificación 1 de §3 lo detecta** —el paso 8 exige 200— y por eso el flujo no termina en la subida.

**La incógnita de versión de plataforma quedó resuelta y no por decisión, sino midiendo.** El intake §17.2.P.9 · GeometriaFactory-Web la dejaba marcada **[A VERIFICAR]** y §17.2.P.10 · GeometriaFactory-Web declaraba la salida si no pasaba: bajar la versión objetivo del front, nunca la del backend. **No hizo falta**: el hosting soporta `net10.0`, que es la versión objetivo declarada, y el front publicado responde **200** en la ruta que la etapa sirve. El pre-requisito de §1 que decía «está [A VERIFICAR] en la fuente» queda **cumplido**, y la evidencia vive en [`../../../00-Contexto/Compatibilidad-Plataformas.md`](../../../00-Contexto/Compatibilidad-Plataformas.md) §2.6.

**Lo que la etapa `a` sirve, confirmado sobre el hosting real y no ya sólo en local.** La raíz responde **404** y la página de estado responde **200**: en la etapa `a` hay **una sola** ruta servida. El aviso que 1.1 dejó escrito sobre el valor del secreto de la dirección pública **se confirmó contra el destino real**, no contra una instancia levantada en local.

### 2.2 Procedimiento de respaldo previo y espejado

**La subida es un espejado con borrado de lo anterior**, y por eso el respaldo previo no es una recomendación: es un paso del procedimiento. La carpeta raíz del sitio estaba ocupada por otra aplicación, y **antes de espejar se bajó un respaldo completo de sus 169 archivos**.

| Paso | Qué se hace | Por qué |
| --- | --- | --- |
| 1 | **Inventariar** lo que ocupa la carpeta raíz del sitio antes de tocar nada | Sin inventario no hay forma de saber si el respaldo quedó completo. El del 2026-08-13 dio **169 archivos** |
| 2 | **Bajar el respaldo completo** por el mismo canal, y comprobar que el recuento del respaldo coincide con el del inventario | El espejado con borrado **no es reversible desde el hosting**: lo borrado no está en ningún otro lado |
| 3 | **Espejar la salida de publicación sobre la raíz**, con borrado de lo que ya no corresponde | Deja el destino igual a lo publicado y sin restos de la aplicación anterior, que en un modelo in-process pueden interferir con el arranque |
| 4 | **Verificar** según §3, empezando por la dirección pública | La subida terminada sin error **no es la verificación**; el intake §17.2.P.8 · GeometriaFactory-Web lo declara |

**Esto no reemplaza la reversión de §4 y no la contradice.** La reversión del producto sigue siendo **volver a publicar desde la etiqueta anterior**, porque lo que se revierte es el producto. El respaldo del paso 2 cubre otra cosa: **lo que había en la carpeta y no es de este producto**, que ninguna etiqueta puede regenerar.

**Una observación del destino que conviene conocer antes de leer el HTML público.** El hosting **inyecta contenido propio en la página servida**: un enlace de atribución al proveedor y un guion de publicidad desde un host suyo. Comprobado el 2026-08-13 sobre el HTML de la página de estado. **No compromete `RA-03`** —no hay ni una aparición de la dirección ni del puerto internos—, pero significa que **el HTML público no es byte a byte el que la publicación generó**, y cualquier comprobación que dé eso por sentado va a fallar por un motivo que no es del producto.

## 3. Verificación posterior a la publicación

**Cuatro verificaciones, en orden de costo creciente.** La primera la ejecuta el propio flujo; las tres siguientes son de la etapa.

| # | Verificación | Cómo se comprueba | Umbral |
| --- | --- | --- | --- |
| 1 | **La dirección pública responde** | Paso 8 del flujo, obligatorio, sobre **el valor del secreto de la dirección pública**: comprueba una ruta, no el sitio entero (§1). **Ejercida sobre el hosting real el 2026-08-13: 200** | La dirección pública responde (`QG-03`) |
| 2 | **El front publicado alcanza el servicio de datos** | Una llamada de salud que devuelve **datos reales** del servidor propio, que es lo que `PT-01.d` mide | Datos reales del servidor propio |
| 3 | **El bundle servido es el que se generó en este flujo** | Inspección de la definición del flujo: el paso de generación precede al de publicación y no hay artefacto cacheado | **0** publicaciones con un bundle no generado en el mismo flujo (`QG-02`) |
| 4 | **El guion de demostración de la etapa y los de todas las anteriores pasan** | Ejecución en el navegador del equipo anfitrión (`TC-10035`) | **100 %** (`QG-04`) |

**La primera es la que la fuente exige y la que define este canal.** El intake §17.2.P.8 · GeometriaFactory-Web declara que el flujo **no termina en la subida, termina comprobando que la dirección pública responde**, y lo funda: «una subida por FTP que deja la aplicación caída y se reporta como exitosa es peor que una falla visible».

**La segunda tiene un falso negativo declarado y conviene conocerlo.** Si el servidor propio está caído —`R-08`, riesgo aceptado— o si su dirección cambió, la verificación 2 falla **sin que la publicación tenga nada malo**. El síntoma correcto en ese caso es el **estado degradado** del front, que es una superficie declarada del producto, y el procedimiento está en [`Entornos-Deploy.md`](Entornos-Deploy.md) §5. **No se revierte la publicación por eso.**

**Y un falso positivo que [`ADR-10007`](../05-Arquitectura-Tecnica/Adrs/ADR-10007-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md) §6 acepta por escrito**: una intermitencia del hosting puede marcar en rojo un despliegue correcto. Es preferible al inverso, que es el modo de falla que la verificación 1 viene a cerrar.

## 4. Reversión

**No hay delist ni retiro de versión publicada**, porque no hay repositorio de paquetes del que retirarla. La reversión es **otra publicación**:

| Situación | Procedimiento | Fundamento |
| --- | --- | --- |
| La publicación dejó la aplicación caída | **Volver a publicar desde la etiqueta anterior.** El flujo corre entero, de modo que el bundle también se regenera | Intake §17.2.P.8 · GeometriaFactory-Web; Definition of Done §1.4 |
| La publicación quedó a medias | El mismo procedimiento. **La subida no es transaccional** (`R-03`) y no hay estado intermedio que reparar parcialmente: se vuelve a subir el conjunto | Intake §17.2.P.8 · GeometriaFactory-Web y §17.2.P.12 · GeometriaFactory-Web |
| Un cambio incompatible del contrato llegó a las dos unidades | **Se revierten las dos juntas**, desde el mismo estado del repositorio | Intake §17.2.P.3 · GeometriaFactory-Contracts; [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §3.2 |

**Ventana y comunicación.** El intake §17.2.P.8 · GeometriaFactory-Web declara dos condiciones que esta guía **no reescribe ni suaviza**: la subida **no es transaccional** —riesgo asumido— y **se despliega fuera del horario de uso**. La Definition of Done §1.4 lo exige con la hora registrada del flujo. **No hay lista de integradores a quien avisar**: la comunicación del producto es el punto de control de la etapa y su informe de cierre.

**No hay despliegue con solapamiento y no se lo simula.** El canal es una subida sobre el mismo destino: durante la subida el producto puede estar a medias, y eso es lo que la ventana fuera de horario administra. Declarar un despliegue azul-verde acá sería declarar una infraestructura que no existe.

## 5. Métricas

Las **seis** de [`ADR-10007`](../05-Arquitectura-Tecnica/Adrs/ADR-10007-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md) §8, que esta categoría adopta **sin agregar ninguna**, con la columna de dónde se observa cada una dentro de la canalización:

| Métrica | Objetivo | Dónde se observa |
| --- | --- | --- |
| Apariciones de la dirección del servidor propio en el repositorio | Exactamente **0** | Inspección del árbol de fuentes y del historial |
| Flujos de publicación que terminan sin comprobar la dirección pública | Exactamente **0** | Inspección de la definición del flujo |
| `PT-01.a` · la dirección pública responde tras publicar | **200** | Paso 8 del flujo. **Medido el 2026-08-13 sobre el hosting real: `https://www.aplicada.somee.com/estado` responde 200.** La raíz responde 404, que es lo esperado en la etapa `a` (§2.1) |
| `PT-01.d` · salida hacia el servicio de datos | Una llamada de salud devuelve **datos reales** del servidor propio | Recorrido en la etapa `a` |
| Publicaciones que usan un bundle no generado en el mismo flujo | Exactamente **0** | Inspección de la definición del flujo |
| Advertencias de construcción | Exactamente **0** | Paso 5 del flujo, bloqueante |

**No se declara ninguna métrica de descargas, de adopción, de tasa de despliegues por semana ni de tiempo medio hasta detección de regresión.** Las cuatro presuponen un artefacto distribuido a integradores o una cadencia calendaria, y acá no hay ninguno de los dos: el intake §10 declara «sin plazo; el avance se mide por etapas cerradas». Inventarlas sería declarar un observatorio sin observador.

## 6. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.3 | 2026-08-29 | **Tramo `R-4` · renumerado de `QG` y `CV` al mapa de bloques del destino**, decidido por el Product Owner el 2026-08-29 al **retirar el `ADR-14005`** en lugar de aceptarlo. **1 línea(s)** pasan de `QG-NN` a `QG-<bloque>NNN`, con el bloque **deducido de la línea o de la sección y nunca inventado** — `00` Api, `02` Domain, `04` Application, `06` Infrastructure, `08` Contracts, `10` Web, `12` Visor. Con esto las dos familias **dejan de necesitar apartamiento**: cumplen [`../../../Producto/Norma-De-Nomenclatura.md`](../../../Producto/Norma-De-Nomenclatura.md) y `Root-Rules.md` §9.1 y §9.2. Las referencias cuyo bloque no estaba en el texto **conservan la forma vieja a propósito** y quedan inventariadas en [`../../../Audit/Inventario-Renumerado-R-4-2026-08-29.md`](../../../Audit/Inventario-Renumerado-R-4-2026-08-29.md). Se respeta §4.1: no se tocan las filas de control de cambios ni lo que está entre «…». |
| 1.2 | 2026-08-13 | **Documenta el mecanismo real del hosting, comprobado en un despliegue que ya ocurrió y está en línea**, y con eso deja la guía repetible sin adivinar. Agrega **§2.1** con los cuatro hechos del mecanismo —publicación por FTP **a la carpeta raíz del sitio, sin subcarpeta**; **`web.config` como requisito duro**, cuyo modo de falla es **500** en la dirección pública con la subida terminada sin error; **el `web.config` que genera `dotnet publish` sirve tal cual**, con manejador `aspNetCore` sobre `AspNetCoreModuleV2`, apuntando al `.dll` de la pieza pública y en modelo **in-process**; y **la versión de plataforma elegida en el panel de la cuenta**, que el `web.config` tiene que igualar—, contrastados contra los artículos 203 y 219 de la base de conocimiento del proveedor y contra la práctica. Agrega **§2.2** con el **procedimiento de respaldo previo y espejado** en cuatro pasos, con el hecho de que la subida **borra lo anterior y no es reversible desde el hosting**, y el respaldo completo de **169 archivos** de la aplicación que ocupaba la carpeta. Deja escrito que el hosting **inyecta contenido propio en el HTML servido** —atribución y guion de publicidad— sin comprometer `RA-03`. **§1** marca la incógnita de versión de plataforma como **RESUELTA: el hosting soporta `net10.0`** y **no hizo falta bajar la versión objetivo del front**. **§3** y **§5** registran `PT-01.a` medida sobre el hosting real: **200** en la ruta que la etapa sirve, **404** en la raíz. **No cambia el tipo de artefacto, ni los dos caminos de invocación, ni la reversión, ni las seis métricas.** Sube minor. |
| 1.1 | 2026-08-13 | **Precisa los pre-requisitos operativos de la publicación con lo que el flujo escrito consume**, sin cambiar ninguna decisión. §1 agrega el **inventario completo de secretos nombrados por su función**: a los **dos** que el intake §17.2.P.5 · GeometriaFactory-Web declara se suman el **destino dentro del hosting**, que acompaña a las credenciales del canal, y **la dirección pública que el paso 8 interroga**, con qué pasa si falta cada uno y **ningún valor**. Agrega la precisión de que el paso 8 comprueba **la dirección que se le da**, y el hecho comprobado corriendo el 2026-08-13 de que en la etapa `a` la pieza pública **sirve la página de estado y no la raíz** —404 en la raíz, 200 en la página de estado, sobre la publicación levantada en local—, de modo que un valor apuntado a la raíz desnuda **daría rojo con una publicación correcta**; se declara que **no se resuelve ablandando el paso**, porque eso reabriría el modo de falla que el intake §17.2.P.8 · GeometriaFactory-Web cerró. §2 y la primera verificación de §3 quedan alineadas con ese inventario. **No cambia el tipo de artefacto, ni el procedimiento, ni la reversión, ni las seis métricas.** Sube minor. |
| 1.0 | 2026-08-11 | Emisión inicial. Declara de entrada que **no hay publicación en un repositorio de paquetes** y que lo que documenta es el **despliegue** de la unidad al hosting público, con la estructura de `Rules-Devops.md` §4.5. Declara `Front-Ftp` como tipo de artefacto nuevo, **sin prefijo de familia**, con la constancia de por qué no es una imagen de contenedor ni pertenece a ninguna de las seis familias, y de por qué tampoco corresponde una guía de contrato publicado. Declara los pre-requisitos con los **dos** secretos nombrados por su función y las **dos** marcas [A VERIFICAR] que se resuelven midiendo, los **dos** caminos de invocación del mismo flujo entero, las **cuatro** verificaciones posteriores con su falso negativo y su falso positivo declarados, la reversión **por republicación desde la etiqueta anterior** con la constancia de que no hay despliegue con solapamiento y no se lo simula, y las **seis** métricas de `ADR-10007` §8 sin agregar ninguna. |
