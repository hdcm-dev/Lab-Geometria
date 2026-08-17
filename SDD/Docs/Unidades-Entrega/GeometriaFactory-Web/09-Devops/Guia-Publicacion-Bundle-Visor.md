# Guía de publicación — Bundle del visor

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** Guia-Publicacion-Bundle-Visor.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero DevOps Senior + Release Engineer (AG-09)
**Tipo de proyecto de código (D8):** `library`
**Tipo de artefacto:** `Bundle-Visor`
**Trazabilidad upstream:** [`../05-Arquitectura-Tecnica/Adrs/ADR-12006-Bundle-Generado-Y-Versionado-Del-Punto-De-Extension.md`](../05-Arquitectura-Tecnica/Adrs/ADR-12006-Bundle-Generado-Y-Versionado-Del-Punto-De-Extension.md) 1.0 §7 y §8; [`../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`](../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md) 1.0 §5; [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) 1.1 §3; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.20** §13, §16, §16.1, §17.2.P.7 · GeometriaFactory-Web, §17.2.P.8 · GeometriaFactory-Web, §17.2.P.7 · GeometriaFactory-Visor, §17.2.P.8 · GeometriaFactory-Visor y §18
**Trazabilidad downstream:** [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md), [`Entornos-Deploy.md`](Entornos-Deploy.md); `10-Examples` (sample S-1) y `11-Documentacion` cuando se emitan

---

## Tabla de contenido

- [0. Qué significa «publicación» acá, y qué no](#0-qué-significa-publicación-acá-y-qué-no)
- [1. Pre-requisitos](#1-pre-requisitos)
- [2. Comando y stage de entrega](#2-comando-y-stage-de-entrega)
- [3. Verificación posterior a la entrega](#3-verificación-posterior-a-la-entrega)
- [4. Reversión](#4-reversión)
- [5. Métricas](#5-métricas)
- [6. Control de cambios](#6-control-de-cambios)

---

## 0. Qué significa «publicación» acá, y qué no

**Este documento no describe una publicación externa, porque no la hay.** El intake §17.2.P.7 · GeometriaFactory-Visor declara que el bundle **no se publica** en ningún repositorio de paquetes, `redistribuible` es false (intake §13) y [`ADR-12006`](../05-Arquitectura-Tecnica/Adrs/ADR-12006-Bundle-Generado-Y-Versionado-Del-Punto-De-Extension.md) §4 descartó publicarlo con el fundamento de que sería un portal para una comunidad que no existe.

Lo que sí hay es **una entrega**, y tiene procedimiento, verificación y reversión propios: el artefacto se genera y **se copia al directorio de recursos estáticos de `GeometriaFactory-Web`**, desde donde viaja dentro del despliegue de esa unidad (`05` §5). Ese acto es lo que esta guía documenta, con la estructura que `Rules-Devops.md` §4.5 exige para una guía de publicación.

**Por qué existe este documento y los otros dos proyectos de código de nivel topológico 0 no lo tienen.** `Rules-Devops.md` §2.1 omite la guía para los tipos cuyo artefacto no se publica externamente, y `GeometriaFactory-Domain` y `GeometriaFactory-Contracts` la omitieron por eso: su artefacto no se entrega, se **referencia** dentro de la misma construcción. Acá el artefacto **sí es un archivo que cambia de lugar, de proyecto de código y de cadena de herramientas**, y hay un modo de falla propio de ese traslado —un archivo viejo servido como si fuera el nuevo— que sólo se puede prevenir escribiéndolo.

**`<tipo-artefacto>` = `Bundle-Visor`.** `Rules-Devops.md` §3.1 declara que la lista de tipos de artefacto **no es cerrada** y admite incorporar tipos nuevos respetando el formato del nombre y la convención de prefijo de familia **según familia**. Este artefacto **no pertenece a ninguna de las seis familias declaradas** —no es paquete de gestor, ni imagen, ni carta, ni binario de publicación, ni instalador, ni envío a tienda— porque no se distribuye por ningún gestor: se copia. Se declara con nombre propio y sin prefijo de familia, y esta declaración es la constancia de por qué.

## 1. Pre-requisitos

**Ninguna cuenta, ningún testigo de acceso y ningún alcance de permisos**, porque no hay destino externo que autenticar. Lo que hace falta es de entorno:

| Pre-requisito | Detalle | Fundamento |
| --- | --- | --- |
| Contenedor de desarrollo levantado | Es donde corre el gestor de paquetes del ecosistema del navegador. El equipo anfitrión no tiene las cadenas de herramientas instaladas | Intake §17.2.P.1 · GeometriaFactory-Visor y §10 |
| Dependencias instaladas de forma **reproducible**, desde el archivo de bloqueo | La regla de anclaje de versiones del intake prohíbe que una versión cambie como efecto colateral | Intake, encabezado de la Parte C |
| Versión del motor de dibujo tridimensional **anclada y registrada** | Punto abierto `PA-01` de `05` §11, cerrado por `BT-12009` antes del momento de medición | Intake §17.2.P.1 · GeometriaFactory-Visor |
| Que exista el directorio de recursos estáticos del anfitrión, destino de la copia | `src/GeometriaFactory.Web/wwwroot/js/`, declarado en el árbol del repositorio | Intake §13 y §16 |

**Ningún secreto**: ver [`Entornos-Deploy.md`](Entornos-Deploy.md) §5.

## 2. Comando y stage de entrega

Los comandos son los guiones que el intake §17.2.P.8 · GeometriaFactory-Visor declara, y **no se inventa ninguno**:

| Camino | Comando | Cuándo se usa | Qué produce |
| --- | --- | --- | --- |
| Ciclo corto de trabajo sobre el visor | `scripts/build-visor.sh` | Mientras se trabaja sobre `visor/`, y como primer paso del sample **S-1** | Sólo el bundle, sin encadenar la construcción del resto del producto |
| Construcción del producto | `scripts/build.sh` | En la canalización, y antes de publicar | El bundle encadenado con la compilación del resto de la solución |
| Publicación del front | El flujo de trabajo de publicación, que **genera el bundle en su propio interior** antes de publicar y subir | Al fusionar a la rama principal con cambios bajo `src/GeometriaFactory.Web/` o `visor/`, y también por disparo manual | El bundle recién generado, dentro de la publicación del front |

**El tercer camino es el que efectivamente entrega el artefacto a un usuario**, y no lo ejecuta este proyecto de código: es del flujo de trabajo que el intake §17.2.P.7 · GeometriaFactory-Web y §17.2.P.8 · GeometriaFactory-Web declaran. Sus pasos, en el orden que la fuente declara: obtener el código, preparar las dos cadenas de herramientas, instalar dependencias en `visor/`, **generar el bundle y copiarlo a los recursos estáticos**, publicar el front, inyectar la configuración desde secretos, subir por FTP y **verificar que la dirección pública responde**.

**Variables de entorno requeridas por esta entrega: ninguna.** Las que ese flujo de trabajo necesita —dirección base del servicio de datos y credenciales de la subida— son del front y no de este artefacto.

**Stage de la canalización:** `copiar`, el último de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.1, y corre **después** de que las tres inspecciones sobre el bundle generado estén en verde. El orden importa: un bundle que no pasó `QG-04`, `QG-05` o `QG-06` **no llega al anfitrión**.

## 3. Verificación posterior a la entrega

**Cuatro verificaciones, en orden de costo creciente.** Las tres primeras las ejecuta la canalización; la cuarta cierra la publicación del front.

| # | Verificación | Cómo se comprueba | Umbral |
| --- | --- | --- | --- |
| 1 | **El artefacto corresponde al fuente que lo generó**: nunca se editó a mano | Revisión del pull request de la etapa, que **es** el punto de control | **0** ediciones manuales (`QG-09`, `CV-30`) |
| 2 | **Es reproducible**: dos construcciones desde el mismo estado producen el mismo artefacto | Comparación de dos construcciones | Idénticos (`ADR-12006` §8) |
| 3 | **El contrato entero se puede ejercer sin backend**: el sample **S-1** carga el bundle, se pega un texto y dibuja | Ejecución de S-1, en **cinco pasos o menos** | **6 de 6** funciones, con **0** servicios del backend disponibles (`ADR-12006` §8) |
| 4 | **El front publicado responde** con el bundle nuevo servido | La verificación de que la dirección pública responde, que el intake §17.2.P.8 · GeometriaFactory-Web declara como cierre obligatorio de ese flujo | La dirección pública responde |

**La tercera es la más valiosa de las cuatro, y conviene decir por qué.** `ADR-12006` §5, punto 3, declara que el sample S-1 funciona **como prueba de contrato**: ejerce las seis funciones sin ninguna pieza del backend y por eso detecta un cambio incompatible **sin necesidad de levantar el producto**. Como ninguna de las tres clases de cambio mayor la detecta una compilación ([`Estrategia-Versionado.md`](Estrategia-Versionado.md) §1), S-1 es la barrera más temprana que existe contra un cambio mayor no declarado.

**La cuarta no la ejecuta este proyecto de código**, y se declara igual porque es donde la entrega termina: el intake §17.2.P.8 · GeometriaFactory-Web declara que ese flujo **no termina en la subida, termina comprobando que la dirección pública responde**, y que una subida que deja la aplicación caída y se reporta como exitosa es peor que una falla visible.

## 4. Reversión

**No hay delist ni retiro de versión publicada**, porque no hay repositorio de paquetes del cual retirarla. La reversión es de estado del repositorio y de publicación del front:

| Situación | Procedimiento | Fundamento |
| --- | --- | --- |
| El bundle entregado al anfitrión está roto, y todavía no se publicó | Volver a la **etiqueta de la etapa anterior** y **regenerar** el bundle desde ese estado. No se restaura un archivo: se reconstruye | [`Entornos-Deploy.md`](Entornos-Deploy.md) §2; `ADR-12006` §5, punto 2 |
| El bundle roto ya está publicado en el hosting | **Volver a publicar desde la etiqueta anterior**, que es el procedimiento de reversión que el intake §17.2.P.8 · GeometriaFactory-Web declara para el front. La regeneración del bundle es parte de esa publicación | Intake §17.2.P.8 · GeometriaFactory-Web |
| Un cambio mayor del punto de extensión rompió al anfitrión en ejecución | Lo anterior, más la fila de cambio mayor en el registro de cambios del producto que faltó. **Ninguna compilación lo iba a detectar**: la mitigación previa es la revisión y S-1 | `ADR-12006` §2 y §6 |

**Ventana y comunicación.** El intake §17.2.P.8 · GeometriaFactory-Web declara dos condiciones que alcanzan a toda entrega que llegue al hosting y que esta guía **no reescribe ni suaviza**: la subida **no es transaccional** —riesgo asumido— y **se despliega fuera del horario de uso**. La comunicación del producto es el punto de control de la etapa y su informe de cierre; no hay lista de integradores a quien avisar.

## 5. Métricas

Las cinco de `ADR-12006` §8, que esta categoría adopta **sin agregar ninguna**, con la columna de dónde se observa cada una dentro de la canalización:

| Métrica | Objetivo | Dónde se observa |
| --- | --- | --- |
| Ediciones manuales del artefacto generado | Exactamente **0** | Revisión del pull request (`QG-09`) |
| Reproducibilidad | Dos construcciones desde el mismo estado producen el mismo artefacto | Comparación de dos ejecuciones del stage `empaquetar` |
| Cambios mayores sin registro | Exactamente **0** | Revisión del pull request de la etapa, que **es** el punto de control |
| Garantías vigentes tras un cambio mayor | **7 de 7** verificadas antes de fusionar | Batería de la categoría 08 sobre las siete garantías |
| Recorrido del contrato por el sample S-1 | **6 de 6** funciones, con **0** servicios del backend disponibles | Ejecución de S-1, en cinco pasos o menos |

**No se declara ninguna métrica de descargas, de adopción ni de tiempo hasta detección de regresión.** Las tres presuponen un artefacto distribuido a integradores y un canal que las mida, y acá no hay ninguno de los dos. Inventarlas sería declarar un observatorio sin observador.

## 6. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara de entrada que **no hay publicación externa** y que lo que documenta es la **entrega interna** del bundle al anfitrión, con la estructura de `Rules-Devops.md` §4.5, y por qué los otros dos proyectos de código de nivel topológico 0 omiten esta guía y éste no. Declara `Bundle-Visor` como tipo de artefacto nuevo, **sin prefijo de familia**, con la constancia de que no pertenece a ninguna de las seis familias declaradas porque no se distribuye por ningún gestor. Declara los pre-requisitos —todos de entorno, **ninguna credencial**—, los **tres** caminos de construcción con los guiones que el intake declara, las **cuatro** verificaciones posteriores en orden de costo, la reversión **por regeneración y no por restauración**, y las **cinco** métricas de `ADR-12006` §8 sin agregar ninguna, con la constancia de por qué no se declaran métricas de descargas ni de adopción. |
