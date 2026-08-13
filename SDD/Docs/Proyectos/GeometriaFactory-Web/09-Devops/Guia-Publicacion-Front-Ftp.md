# Guía de publicación — Front por FTP

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** Guia-Publicacion-Front-Ftp.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-13
**Autor:** Ingeniero DevOps Senior + Deploy Engineer (AG-09)
**Tipo de proyecto de código (D8):** `web-monolith`
**Tipo de artefacto:** `Front-Ftp`
**Trazabilidad upstream:** [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §5; [`../05-Arquitectura-Tecnica/Adrs/ADR-07-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md`](../05-Arquitectura-Tecnica/Adrs/ADR-07-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md) 1.0 §2, §7 y §8; [`../08-Calidad-Y-Pruebas/Definition-Of-Done.md`](../08-Calidad-Y-Pruebas/Definition-Of-Done.md) 1.1 §1.4; [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) 1.1 §3; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.21** §13, §16, §17.6.P.5, §17.6.P.7, §17.6.P.8, §17.6.P.9, §17.6.P.10 y §17.6.P.12
**Trazabilidad downstream:** [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md), [`Entornos-Deploy.md`](Entornos-Deploy.md); `11-Documentacion` cuando se emita

---

## Tabla de contenido

- [0. Qué significa «publicación» acá, y qué no](#0-qué-significa-publicación-acá-y-qué-no)
- [1. Pre-requisitos](#1-pre-requisitos)
- [2. Comando y stage de publicación](#2-comando-y-stage-de-publicación)
- [3. Verificación posterior a la publicación](#3-verificación-posterior-a-la-publicación)
- [4. Reversión](#4-reversión)
- [5. Métricas](#5-métricas)
- [6. Control de cambios](#6-control-de-cambios)

---

## 0. Qué significa «publicación» acá, y qué no

**No es una publicación en un repositorio de paquetes.** El intake §13 declara que **ningún proyecto de código del producto se publica como paquete redistribuible** y que los dos artefactos entregables son **una imagen de contenedor y una publicación subida por FTP**. Éste es el segundo, y `05` §5 lo declara «la publicación de la aplicación en el hosting público, con dominio y transporte seguro».

Lo que sí hay es **un despliegue**, y tiene pre-requisitos, procedimiento, verificación y reversión propios. Esta guía los documenta con la estructura que `Rules-Devops.md` §4.5 exige.

**`<tipo-artefacto>` = `Front-Ftp`.** `Rules-Devops.md` §2.2 fija `image-docker o artefacto desplegable equivalente` para el tipo `web-monolith`, y §3.1 declara que la lista de tipos **no es cerrada**, admitiendo incorporar tipos nuevos respetando el formato del nombre y la convención de prefijo **según familia**. Este artefacto **no es una imagen de contenedor** —la imagen del producto es la del backend— y **no pertenece a ninguna de las seis familias declaradas**, porque no se distribuye por ningún gestor: se sube por FTP a un destino único. Se declara con nombre propio y sin prefijo de familia, y esta declaración es la constancia de por qué. Es el mismo tratamiento que `GeometriaFactory-Visor` dio a su `Bundle-Visor`.

**Un artefacto, y sólo uno.** `Rules-Devops.md` §2.2 admite un `openapi` versionado como artefacto secundario para servicios; acá no aplica, y tampoco aplicaría en el backend: el intake §17.5.P.3 declara que **no hay versionado de rutas porque no hay clientes de terceros**, y el contrato compartido es un ensamblado, no una descripción publicada.

## 1. Pre-requisitos

| Pre-requisito | Detalle | Fundamento |
| --- | --- | --- |
| **Cuenta en el hosting público** | El servicio gratuito con servidor de información, transporte seguro y dominio público. Se contrata y se configura **por fuera del repositorio**: no hay infraestructura declarativa | Intake §17.6.P.9; [`Entornos-Deploy.md`](Entornos-Deploy.md) §3 |
| **Credenciales del canal de publicación**, nombradas por su función | Viven como **secreto del repositorio**. Su alcance mínimo es escribir en el directorio de la aplicación del hosting, y nada más. **El valor no aparece en ningún documento de esta cadena** | Intake §17.6.P.5 y §16 |
| **Dirección base del servicio de datos**, nombrada por su función | Vive como **secreto del repositorio** y se inyecta al publicar. **La dirección real del servidor propio no se versiona** | Intake §17.6.P.5; [`ADR-07`](../05-Arquitectura-Tecnica/Adrs/ADR-07-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md) §2 |
| **Contenedor de desarrollo levantado**, para la publicación manual | Es donde corren las dos cadenas de herramientas: el equipo anfitrión no las tiene instaladas | Intake §10 y encabezado de la Parte C |
| **Versión de plataforma del hosting comprobada** | Está **[A VERIFICAR]** en la fuente y se resuelve midiendo `PT-01.a`, no decidiendo. Si no pasa, la salida es **bajar la versión objetivo del front, no la del backend** | Intake §17.6.P.9; `PA-02` de `05` §11 |
| **Versión de la biblioteca de componentes de interfaz anclada** | También **[A VERIFICAR]** en la fuente; se ancla al crear el andamiaje | Intake §17.6.P.1; `PA-01` de `05` §11, `BT-02` |

**Ningún pre-requisito de esta guía se cumple escribiendo un valor acá.** Los dos secretos se nombran por su función y se declara dónde vive el valor; las dos marcas **[A VERIFICAR]** se resuelven midiendo.

**El inventario completo de secretos que el flujo consume, leído sobre el flujo y no supuesto.** El intake §17.6.P.5 declara **dos** por su función —la dirección del servicio de datos y las credenciales del canal—, y ésas son las **dos** que la tabla de arriba exige. El flujo escrito en `.github/workflows/deploy-front-ftp.yml` consume además **el destino dentro del hosting**, que acompaña a las credenciales del canal, y **la dirección pública que el paso 8 interroga**, que es la que `QG-03` mide. Se dejan nombrados por su función y **sin ningún valor**, porque quien vaya a publicar necesita saber que **el flujo se detiene si falta cualquiera de ellos**:

| Secreto, nombrado por su función | Paso que lo usa | Qué pasa si falta |
| --- | --- | --- |
| Dirección base del servicio de datos | 6 | El paso **se detiene antes de escribir nada**: comprueba que el valor no esté vacío. Comprobado corriendo el 2026-08-13 |
| Credenciales del canal de publicación, y **el destino dentro del hosting** | 7 | La subida no ocurre |
| **Dirección pública** | 8 | La comprobación final no tiene qué interrogar, y el flujo no cierra su gate |

**Y una precisión sobre el valor de la última, que no es cosmética.** El paso 8 exige respuesta correcta de **la dirección que se le da**. En la etapa `a` la pieza pública sirve **una sola ruta —la página de estado— y la raíz no está servida**: se comprobó corriendo el 2026-08-13, levantando la publicación resultante en local, que la raíz responde **404** y la página de estado responde **200**. Si el valor de este secreto es la raíz desnuda, **el paso 8 dará rojo con una publicación correcta** hasta que la etapa `b` ponga las rutas navegables. **No se resuelve ablandando el paso** —eso reabriría exactamente el modo de falla que el intake §17.6.P.8 vino a cerrar—: se resuelve fijando el valor del secreto en una ruta que la etapa sirva.

## 2. Comando y stage de publicación

**El acto de publicar es el flujo de trabajo del repositorio**, `.github/workflows/deploy-front-ftp.yml`, que el árbol del intake §16 declara. Sus **ocho** pasos y el gate de cada uno están en [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.1 y **no se repiten acá**; lo que esta guía agrega es cómo se lo invoca y qué necesita.

| Camino | Cómo se invoca | Cuándo se usa |
| --- | --- | --- |
| **Automático** | Fusión a la rama principal con cambios bajo las rutas del filtro | Es el camino normal al cerrar una etapa |
| **Manual** | Disparo manual del mismo flujo | Cuando el cambio no está bajo las rutas del filtro —hoy, un cambio del ensamblado de contratos— y cuando hay que republicar sin que haya cambiado nada, por ejemplo tras rotar el secreto de la dirección del servicio de datos |

**Los dos caminos corren el mismo flujo entero**, incluidos los pasos 4 y 8. No hay un camino corto que suba sin regenerar el bundle o sin comprobar: `QG-02` y `QG-03` lo impiden.

**Variables requeridas por la publicación: todas secretas y todas nombradas por su función** —la dirección base del servicio de datos y las credenciales del canal, que son las **dos** que el intake §17.6.P.5 declara, más el destino dentro del hosting y la dirección pública que el paso 8 interroga, que el flujo escrito consume y §1 inventaría—. Ninguna otra, y **ninguna con valor en esta cadena de documentos**. El bundle del visor **no requiere ninguna**: no lee configuración propia (`RA-02`).

**Construcción local para depurar el flujo.** Los guiones del repositorio que el intake §16 lista permiten reproducir los pasos 1 a 5 en la máquina de quien construye, dentro del contenedor de desarrollo: `scripts/build-visor.sh` para el bundle y `scripts/build.sh` para la construcción encadenada. **Los pasos 6, 7 y 8 no se reproducen en local**, porque involucran el secreto y el destino real; intentar reproducirlos exigiría el secreto en la máquina, que es lo que el intake §17.6.P.5 evita.

## 3. Verificación posterior a la publicación

**Cuatro verificaciones, en orden de costo creciente.** La primera la ejecuta el propio flujo; las tres siguientes son de la etapa.

| # | Verificación | Cómo se comprueba | Umbral |
| --- | --- | --- | --- |
| 1 | **La dirección pública responde** | Paso 8 del flujo, obligatorio, sobre **el valor del secreto de la dirección pública**: comprueba una ruta, no el sitio entero (§1) | La dirección pública responde (`QG-03`) |
| 2 | **El front publicado alcanza el servicio de datos** | Una llamada de salud que devuelve **datos reales** del servidor propio, que es lo que `PT-01.d` mide | Datos reales del servidor propio |
| 3 | **El bundle servido es el que se generó en este flujo** | Inspección de la definición del flujo: el paso de generación precede al de publicación y no hay artefacto cacheado | **0** publicaciones con un bundle no generado en el mismo flujo (`QG-02`) |
| 4 | **El guion de demostración de la etapa y los de todas las anteriores pasan** | Ejecución en el navegador del equipo anfitrión (`TC-35`) | **100 %** (`QG-04`) |

**La primera es la que la fuente exige y la que define este canal.** El intake §17.6.P.8 declara que el flujo **no termina en la subida, termina comprobando que la dirección pública responde**, y lo funda: «una subida por FTP que deja la aplicación caída y se reporta como exitosa es peor que una falla visible».

**La segunda tiene un falso negativo declarado y conviene conocerlo.** Si el servidor propio está caído —`R-08`, riesgo aceptado— o si su dirección cambió, la verificación 2 falla **sin que la publicación tenga nada malo**. El síntoma correcto en ese caso es el **estado degradado** del front, que es una superficie declarada del producto, y el procedimiento está en [`Entornos-Deploy.md`](Entornos-Deploy.md) §5. **No se revierte la publicación por eso.**

**Y un falso positivo que [`ADR-07`](../05-Arquitectura-Tecnica/Adrs/ADR-07-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md) §6 acepta por escrito**: una intermitencia del hosting puede marcar en rojo un despliegue correcto. Es preferible al inverso, que es el modo de falla que la verificación 1 viene a cerrar.

## 4. Reversión

**No hay delist ni retiro de versión publicada**, porque no hay repositorio de paquetes del que retirarla. La reversión es **otra publicación**:

| Situación | Procedimiento | Fundamento |
| --- | --- | --- |
| La publicación dejó la aplicación caída | **Volver a publicar desde la etiqueta anterior.** El flujo corre entero, de modo que el bundle también se regenera | Intake §17.6.P.8; Definition of Done §1.4 |
| La publicación quedó a medias | El mismo procedimiento. **La subida no es transaccional** (`R-03`) y no hay estado intermedio que reparar parcialmente: se vuelve a subir el conjunto | Intake §17.6.P.8 y §17.6.P.12 |
| Un cambio incompatible del contrato llegó a las dos unidades | **Se revierten las dos juntas**, desde el mismo estado del repositorio | Intake §17.4.P.3; [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §3.2 |

**Ventana y comunicación.** El intake §17.6.P.8 declara dos condiciones que esta guía **no reescribe ni suaviza**: la subida **no es transaccional** —riesgo asumido— y **se despliega fuera del horario de uso**. La Definition of Done §1.4 lo exige con la hora registrada del flujo. **No hay lista de integradores a quien avisar**: la comunicación del producto es el punto de control de la etapa y su informe de cierre.

**No hay despliegue con solapamiento y no se lo simula.** El canal es una subida sobre el mismo destino: durante la subida el producto puede estar a medias, y eso es lo que la ventana fuera de horario administra. Declarar un despliegue azul-verde acá sería declarar una infraestructura que no existe.

## 5. Métricas

Las **seis** de [`ADR-07`](../05-Arquitectura-Tecnica/Adrs/ADR-07-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md) §8, que esta categoría adopta **sin agregar ninguna**, con la columna de dónde se observa cada una dentro de la canalización:

| Métrica | Objetivo | Dónde se observa |
| --- | --- | --- |
| Apariciones de la dirección del servidor propio en el repositorio | Exactamente **0** | Inspección del árbol de fuentes y del historial |
| Flujos de publicación que terminan sin comprobar la dirección pública | Exactamente **0** | Inspección de la definición del flujo |
| `PT-01.a` · la dirección pública responde tras publicar | **200** | Paso 8 del flujo |
| `PT-01.d` · salida hacia el servicio de datos | Una llamada de salud devuelve **datos reales** del servidor propio | Recorrido en la etapa `a` |
| Publicaciones que usan un bundle no generado en el mismo flujo | Exactamente **0** | Inspección de la definición del flujo |
| Advertencias de construcción | Exactamente **0** | Paso 5 del flujo, bloqueante |

**No se declara ninguna métrica de descargas, de adopción, de tasa de despliegues por semana ni de tiempo medio hasta detección de regresión.** Las cuatro presuponen un artefacto distribuido a integradores o una cadencia calendaria, y acá no hay ninguno de los dos: el intake §10 declara «sin plazo; el avance se mide por etapas cerradas». Inventarlas sería declarar un observatorio sin observador.

## 6. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-13 | **Precisa los pre-requisitos operativos de la publicación con lo que el flujo escrito consume**, sin cambiar ninguna decisión. §1 agrega el **inventario completo de secretos nombrados por su función**: a los **dos** que el intake §17.6.P.5 declara se suman el **destino dentro del hosting**, que acompaña a las credenciales del canal, y **la dirección pública que el paso 8 interroga**, con qué pasa si falta cada uno y **ningún valor**. Agrega la precisión de que el paso 8 comprueba **la dirección que se le da**, y el hecho comprobado corriendo el 2026-08-13 de que en la etapa `a` la pieza pública **sirve la página de estado y no la raíz** —404 en la raíz, 200 en la página de estado, sobre la publicación levantada en local—, de modo que un valor apuntado a la raíz desnuda **daría rojo con una publicación correcta**; se declara que **no se resuelve ablandando el paso**, porque eso reabriría el modo de falla que el intake §17.6.P.8 cerró. §2 y la primera verificación de §3 quedan alineadas con ese inventario. **No cambia el tipo de artefacto, ni el procedimiento, ni la reversión, ni las seis métricas.** Sube minor. |
| 1.0 | 2026-08-11 | Emisión inicial. Declara de entrada que **no hay publicación en un repositorio de paquetes** y que lo que documenta es el **despliegue** de la unidad al hosting público, con la estructura de `Rules-Devops.md` §4.5. Declara `Front-Ftp` como tipo de artefacto nuevo, **sin prefijo de familia**, con la constancia de por qué no es una imagen de contenedor ni pertenece a ninguna de las seis familias, y de por qué tampoco corresponde una guía de contrato publicado. Declara los pre-requisitos con los **dos** secretos nombrados por su función y las **dos** marcas [A VERIFICAR] que se resuelven midiendo, los **dos** caminos de invocación del mismo flujo entero, las **cuatro** verificaciones posteriores con su falso negativo y su falso positivo declarados, la reversión **por republicación desde la etiqueta anterior** con la constancia de que no hay despliegue con solapamiento y no se lo simula, y las **seis** métricas de `ADR-07` §8 sin agregar ninguna. |
