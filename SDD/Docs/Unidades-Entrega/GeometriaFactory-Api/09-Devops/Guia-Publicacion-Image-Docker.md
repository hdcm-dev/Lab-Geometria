# Guía de publicación — Imagen de contenedor

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** Guia-Publicacion-Image-Docker.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero DevOps Senior + Platform Engineer (AG-09)
**Tipo de proyecto de código (D8):** `rest-api`
**Tipo de artefacto:** `image-docker`
**Trazabilidad upstream:** [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §5; [`../05-Arquitectura-Tecnica/Adrs/ADR-00007-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md`](../05-Arquitectura-Tecnica/Adrs/ADR-00007-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md) 1.0; [`../05-Arquitectura-Tecnica/Adrs/ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md`](../05-Arquitectura-Tecnica/Adrs/ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) 1.0; [`../08-Calidad-Y-Pruebas/Definition-Of-Done.md`](../08-Calidad-Y-Pruebas/Definition-Of-Done.md) 1.1 §1.4; [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) 1.1 §3.3; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.22** §10, §13, §15, §16, §17.1.P.4 · GeometriaFactory-Api, §17.1.P.5 · GeometriaFactory-Api, §17.1.P.7 · GeometriaFactory-Api, §17.1.P.8 · GeometriaFactory-Api, §17.1.P.9 · GeometriaFactory-Api, §17.1.P.11 · GeometriaFactory-Api y §17.1.P.12 · GeometriaFactory-Api
**Trazabilidad downstream:** [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md), [`Entornos-Deploy.md`](Entornos-Deploy.md); `11-Documentacion` cuando se emita, que **cita esta política y no define una paralela** (`Rules-Devops.md` §0)

---

## Tabla de contenido

- [0. Qué significa «publicación» acá, y qué no](#0-qué-significa-publicación-acá-y-qué-no)
- [1. Pre-requisitos](#1-pre-requisitos)
- [2. Comando y stage de publicación](#2-comando-y-stage-de-publicación)
  - [2.1 La prueba única del mecanismo, que la fuente exige](#21-la-prueba-única-del-mecanismo-que-la-fuente-exige)
- [3. Verificación posterior al despliegue](#3-verificación-posterior-al-despliegue)
- [4. Reversión](#4-reversión)
- [5. Métricas](#5-métricas)
- [6. Control de cambios](#6-control-de-cambios)

---

## 0. Qué significa «publicación» acá, y qué no

**No hay publicación en ningún registro de imágenes.** El intake §17.1.P.7 · GeometriaFactory-Api declara el canal de entrega: **imagen construida en destino desde el repositorio con el archivo de composición, sin publicar en un registro**. `05` §5 lo repite y agrega la marca de la fuente: el mecanismo lleva **[A VERIFICAR]** y **debe probarse una vez antes de depender de él**.

Lo que sí hay es **un despliegue**, y esta guía documenta su procedimiento con la estructura que `Rules-Devops.md` §4.5 exige. Con una advertencia que ordena todo el documento:

**El despliegue lo ejecuta el Product Owner, a mano.** El intake §13 lo declara **[DECISIÓN]** en su fila «Despliegue manual del backend»: «El agente IA entrega el `Dockerfile` y el `compose.yaml`; no ejecuta el despliegue». La fila `despliegue` de §17.1.P.8 · GeometriaFactory-Api lo repite: «Manual, por el docente [DECISIÓN, RT §13]». **Esta guía está escrita para quien lo ejecuta**, no para una canalización que lo ejecute.

**`<tipo-artefacto>` = `image-docker`**, que es uno de los valores admitidos por `Rules-Devops.md` §3.1 y el que §2.2 fija para el tipo `rest-api`. **No se declara un tipo nuevo**, a diferencia de lo que hicieron `GeometriaFactory-Visor` y `GeometriaFactory-Web`: el artefacto **sí es** una imagen de contenedor; lo que no es convencional es su canal, que no la publica.

**Y el segundo artefacto que `Rules-Devops.md` §2.2 admite para este tipo no existe acá.** La tabla nombra un contrato de servicio versionado como artefacto secundario; el intake §17.1.P.3 · GeometriaFactory-Api declara que **el versionado del contrato es el del ensamblado de contratos** y que **no hay versionado de rutas porque no hay clientes de terceros**. Una guía de publicación de contrato describiría una entrega que nadie recibe.

## 1. Pre-requisitos

**Ninguna cuenta de registro, ningún testigo de publicación y ningún alcance de permisos de repositorio de imágenes**, porque no hay destino de publicación que autenticar. Los pre-requisitos son de entorno y de acceso al servidor propio:

| Pre-requisito | Detalle | Fundamento |
| --- | --- | --- |
| **Servidor propio con motor de contenedores** | Ya existe: el intake §10 lo cuenta entre las tres piezas de infraestructura de costo cero | Intake §10 |
| **Salida a la red desde el destino** en el momento de desplegar | El mecanismo **construye ahí**: necesita traer el código y las dependencias. Sin red **no se puede desplegar ni revertir** | **Consecuencia declarada por esta categoría** en [`Entornos-Deploy.md`](Entornos-Deploy.md) §3 |
| **Que el motor de contenedores del destino resuelva la referencia al repositorio**, con credenciales si es privado | **[A VERIFICAR]** en la fuente. Ver §2.1 | Intake §17.1.P.11 · GeometriaFactory-Api punto 5 |
| **Volumen persistente** para el almacén, **fuera de la imagen** | Si el almacén quedara dentro de la imagen, cada reemplazo de versión borraría el trabajo de la comisión | Intake §17.1.P.4 · GeometriaFactory-Api; Definition of Done §1.4 |
| **Clave de firma del acceso**, nombrada por su función | Provista por variable de entorno o archivo montado, **fuera del repositorio y fuera de la imagen**. El archivo de composición la referencia; **nunca la contiene** | Intake §17.1.P.5 · GeometriaFactory-Api |
| **Un puerto publicado hacia el enrutador** | Es **el único punto de entrada al servidor propio** | `05` §5 |
| **La etiqueta de la etapa cerrada** | Es lo que se despliega, y lo que permite volver a cualquier demostración ya aprobada | Intake §17.1.P.7 · GeometriaFactory-Api; Definition of Done §1.4 |

**Ningún pre-requisito de esta guía se cumple escribiendo un valor acá.** La clave de firma se nombra por su función y se declara dónde vive; la dirección del servidor propio **no aparece**, y no aparece tampoco en el archivo de composición versionado: ver [`Entornos-Deploy.md`](Entornos-Deploy.md) §4.

## 2. Comando y stage de publicación

**Las dos piezas que el agente entrega son las que el árbol del intake §16 declara**: `deploy/Dockerfile`, multietapa, y `deploy/compose.yaml`, «despliegue desde Git en destino». El acto de desplegar es **levantar la composición en el servidor propio desde la etiqueta de la etapa cerrada**, y el motor de contenedores construye ahí.

| Momento | Quién | Qué ocurre |
| --- | --- | --- |
| En la canalización, stage `imagen` | Automático | La imagen **se construye y se arranca desde el contenedor de desarrollo** para medir `PT-04`: aplica las transformaciones sobre un almacén vacío y responde salud. **Esa imagen no se guarda ni se despliega**: existe para verificar |
| En el servidor propio | **El Product Owner, a mano** | Se levanta la composición desde la etiqueta; el motor construye la imagen en destino y arranca el servicio con el volumen y la clave de firma que el ambiente provee |

**Variables requeridas por el despliegue: dos, nombradas por su función** —la **ruta del almacén**, apuntando al volumen persistente, y la **clave de firma del acceso**—. Ninguna otra requerida, y ninguna con su valor escrito en el repositorio.

**La imagen se sella a sí misma con su revisión, y eso ya no es una variable que alguien tenga que acompañar.** El Dockerfile deriva el commit del `.git` del contexto cuando el contexto lo trae —los tres caminos reales de construcción lo traen: el árbol de trabajo, el checkout de la canalización y el clon que Docker hace de la URL del repositorio con `BUILDKIT_CONTEXT_KEEP_GIT_DIR=1`—. `SOURCE_REVISION_ID` **queda como respaldo**, para construir desde un tarball sin `.git` o para forzar una revisión puntual. El motivo del cambio es que la falla anterior **no tenía síntoma**: al actualizar el código sin actualizar la variable, el servicio informaba por `/salud` una revisión que no era la suya y seguía andando perfecto.

**Y una opcional, agregada en la etapa `g`: la publicación del explorador de la superficie.** Sin ella el servicio desplegado **no sirve ni el documento OpenAPI ni el explorador**, que es el comportamiento anterior a [`ADR-08008`](../../../Producto/Adrs/ADR-08008-La-Superficie-HTTP-Se-Describe-Y-El-Explorador-No-Se-Publica-Solo.md). Se dice explícitamente porque un explorador **enumera todos los puntos ante cualquiera que abra la dirección**, y eso es una decisión de quien despliega y no un efecto de haber agregado un paquete. Es la única variable de las tres cuya ausencia **no impide arrancar**.

**Reemplazo de versión: detener y arrancar, con ventana de indisponibilidad.** El intake §17.1.P.8 · GeometriaFactory-Api lo declara y `05` §5 lo repite: **sin proxy inverso no hay despliegue con solapamiento**. Y hay un motivo más que se suma al de la fuente: el almacén es un archivo con **escritor único**, de modo que dos versiones vivas a la vez escribirían sobre el mismo archivo.

**Qué ocurre en cada arranque, y por qué importa para quien despliega.** [`ADR-00007`](../05-Arquitectura-Tecnica/Adrs/ADR-00007-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md) §2 declara el arranque en **dos fases**, y que **el servicio no escucha hasta que las dos terminaron**: primero se construye el grafo de dependencias —si falta algo, falla en construcción y no hay servicio— y después se dispara la preparación del almacén —si no se pudo completar, el arranque se detiene y **ninguna petición se atiende**—. **No hay modo de sólo lectura ni arranque parcial.** Para quien despliega, la consecuencia es simple: **si el servicio responde, está entero**; y si no arranca, no hay un estado intermedio que interpretar.

### 2.1 La prueba única del mecanismo, que la fuente exige

El intake §17.1.P.11 · GeometriaFactory-Api punto 5 marca el mecanismo **[A VERIFICAR]** y exige **probarlo una vez antes de depender de él**. `PA-08` de `05` §11 lo dirige a esta categoría **para medirlo, no para decidirlo**. Esta guía escribe qué es esa prueba; **no declara que el mecanismo funcione**.

| # | Qué se comprueba | Cuándo se da por pasada |
| --- | --- | --- |
| 1 | El motor de contenedores del destino **resuelve la referencia al repositorio** y trae el código de la etiqueta indicada | El código llega completo al destino |
| 2 | Si el repositorio es privado, **las credenciales del destino alcanzan** | Lo mismo, sin intervención manual adicional |
| 3 | La construcción **termina en destino**, con el archivo multietapa y **sin kit de desarrollo instalado en el anfitrión** | La imagen queda construida ahí |
| 4 | El servicio **arranca, aplica las transformaciones sobre el almacén y responde salud** | El punto de salud responde |
| 5 | El **volumen persistente sobrevive** a un reemplazo de versión | Tras detener y volver a levantar, el dato anterior sigue estando |

**Si la prueba no pasa, la salida no la decide esta categoría.** El intake declara el mecanismo como decisión con marca de verificación pendiente; un fallo es material para el Product Owner, y las alternativas —traer una imagen ya construida, o construir en la máquina de desarrollo y transferirla— **cambian el canal de entrega declarado** y por lo tanto **no se adoptan acá**. Queda como `PD-01` en [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §10.

**La quinta comprobación no está en la fuente y es un agregado de esta categoría**, declarado como tal: el intake exige que el almacén viva en un volumen persistente y **nunca dentro de la imagen**, y la única forma de saber que eso quedó bien configurado es **reemplazar la versión y comprobar que el dato sigue**. Es la comprobación más barata contra el modo de falla más caro del producto.

## 3. Verificación posterior al despliegue

**Cinco verificaciones, en orden de costo creciente.** Las dos primeras las hace la canalización antes de que el artefacto se entregue; las tres siguientes las hace quien despliega.

| # | Verificación | Cómo se comprueba | Umbral |
| --- | --- | --- | --- |
| 1 | **La imagen se construye y arranca** | Stage `imagen` de la canalización, desde el contenedor de desarrollo | `PT-04`: construye, arranca, aplica las transformaciones sobre un almacén vacío y **responde salud** |
| 2 | **El arranque en frío no se estira** | `TC-00033` | Menos de **30 segundos** [ASUNCIÓN del intake §17.1.P.10 · GeometriaFactory-Api]. **Condicionado**: se mide y se registra |
| 3 | **El servicio desplegado responde salud** | El punto de salud, que **no exige acceso firmado** y **tiene que poder responder cuando nadie puede autenticarse** | Responde |
| 4 | **El front lo alcanza** | Una llamada de salud desde el front que devuelva **datos reales** del servidor propio | Es `PT-01.d`; su salida declarada si no pasa es **publicar el servicio en un puerto convencional** |
| 5 | **La premisa de la topología se sostiene desde la facultad** | La puerta `PT-05`, en el despliegue real, medida **desde la red donde el producto se usa** | La declara el intake §15; la fuente **recomienda no relegarla** |

**La tercera merece una advertencia de lectura, y no es de esta categoría sino de [`ADR-00007`](../05-Arquitectura-Tecnica/Adrs/ADR-00007-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md) §2, regla 4.** El punto de salud **responde por el estado del servicio, no por el de sus dependencias en detalle**: dice si puede atender o no, y **no dice dónde está el almacén, ni con qué esquema, ni qué ruta se configuró**. Quien despliegue no va a encontrar ahí un diagnóstico, y es deliberado: es `RA-03` en el punto más tentador de todos.

**La quinta es la única verificación del producto que esta cadena no puede preparar ni ensayar**, porque exige estar en la red de la facultad. El intake §15 registra que **el 2026-08-08 su letra corrió de `h` a `i`** al insertarse una etapa, **sin que la puerta se despegue del despliegue real**.

## 4. Reversión

**No hay delist ni retiro de imagen publicada**, porque no hay registro del cual retirarla. La reversión es **volver a la etiqueta anterior y reconstruir en destino**:

| Situación | Procedimiento | Fundamento |
| --- | --- | --- |
| El servicio desplegado está roto | Levantar la composición **desde la etiqueta de la etapa anterior**. El motor reconstruye en destino | Intake §17.1.P.8 · GeometriaFactory-Api; [`ADR-00008`](../05-Arquitectura-Tecnica/Adrs/ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) §6, trade-off 3 |
| Hace falta revertir **y no hay red en el destino** | **No se puede.** La reversión necesita traer el código y reconstruir. Es la consecuencia del canal declarado, registrada en [`Entornos-Deploy.md`](Entornos-Deploy.md) §3 | **Declarado por esta categoría** |
| Un cambio incompatible del contrato llegó a las dos unidades | **Se revierten las dos juntas**, desde el mismo estado del repositorio | Intake §17.1.P.3 · GeometriaFactory-Contracts; [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §7 |
| Una transformación de esquema quedó mal | **Volver a la etiqueta no deshace el esquema del almacén**: el esquema sobrevive al reemplazo. Se corrige con otra transformación, y lo único que restituye datos es el **respaldo** | [`../../GeometriaFactory-Infrastructure/09-Devops/Estrategia-Versionado.md`](Estrategia-Versionado.md) §4 |

**Ventana y comunicación.** Cada reversión, como cada despliegue, es **detener y arrancar** y por lo tanto tiene ventana de indisponibilidad. **No hay lista de integradores a quien avisar**: la comunicación del producto es el punto de control de la etapa y su informe de cierre, y hacia la persona que usa el producto el canal es el **estado degradado** del front, que **nunca incluye la dirección del servicio interno**.

**Y la advertencia que esta guía repite porque es la más cara de olvidar**: la cuarta fila. Revertir el código **no revierte los datos**. Un despliegue que aplicó una transformación equivocada deja el almacén transformado, y ninguna etiqueta lo deshace.

## 5. Métricas

Las **seis** de [`ADR-00008`](../05-Arquitectura-Tecnica/Adrs/ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) §8, que esta categoría adopta **sin agregar ninguna**, con la columna de dónde se observa cada una:

| Métrica | Objetivo | Dónde se observa |
| --- | --- | --- |
| Rutas con prefijo o sufijo de versión | Exactamente **0** | Inspección de los **quince** puntos |
| Formas conviviendo de un mismo punto de acceso | Exactamente **0** | Inspección de la superficie |
| Etapas cerradas sin etiqueta | Exactamente **0** | Inspección del historial |
| Pasos de la colección de peticiones reproducible | **5 o menos** | Ejecución en la demostración de etapa (`QG-15`) |
| Datos de prueba inventados en la colección | Exactamente **0** | Comparación contra los escenarios del anexo del intake (`QG-15`) |
| Cambios del ensamblado de contratos desplegados sin la pieza pública | Exactamente **0** | Revisión de cada etapa que toque el ensamblado |

**No se declara ninguna métrica de descargas, de tirones de imagen, de frecuencia de despliegue ni de tiempo medio de recuperación.** Las cuatro presuponen un registro que cuente, o una cadencia calendaria, y acá no hay ninguno de los dos: no se publica en un registro y el intake §10 declara «sin plazo; el avance se mide por etapas cerradas». Inventarlas sería declarar un observatorio sin observador.

## 6. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara de entrada que **no hay publicación en ningún registro de imágenes** y que lo que documenta es el **despliegue construyendo en destino desde el repositorio**, con la advertencia de que **lo ejecuta el Product Owner a mano** y de que esta guía está escrita para quien lo ejecuta. Usa `image-docker`, valor admitido por `Rules-Devops.md` §3.1, **sin declarar un tipo nuevo**, y declara por qué el artefacto secundario que §2.2 admite para el tipo **no tiene sujeto acá**. Declara los pre-requisitos —**ninguna cuenta de registro**, la clave de firma nombrada por su función, y la salida a la red del destino como consecuencia declarada del canal—, las **dos** piezas que el agente entrega y qué ocurre en cada arranque en **dos fases**. Escribe la **prueba única del mecanismo** que la fuente exige, en **cinco** comprobaciones, con la quinta declarada como agregado propio, **sin declarar que el mecanismo funcione**. Declara **cinco** verificaciones posteriores, la reversión por reconstrucción desde la etiqueta —incluida la situación en la que **no se puede revertir sin red**— y la advertencia de que **revertir el código no revierte los datos**, y las **seis** métricas de `ADR-00008` §8 sin agregar ninguna. |
| 1.1 | 2026-08-11 | **Corrección del `H-01` de la auditoría `F-09-Devops-Siete-Proyectos-r1.md`, en su variante de este documento.** La cita de §0 es **literal**, pero la fuente que se le atribuía era la equivocada: el texto entrecomillado es la fila «Despliegue manual del backend» del intake **§13**, no §17.1.P.8 · GeometriaFactory-Api, cuya fila `despliegue` dice «Manual, por el docente [DECISIÓN, RT §13]». Se corrige la atribución y se agrega la cita literal de §17.1.P.8 · GeometriaFactory-Api al lado. Sube la trazabilidad upstream del intake de **1.21** a **1.22**. |
