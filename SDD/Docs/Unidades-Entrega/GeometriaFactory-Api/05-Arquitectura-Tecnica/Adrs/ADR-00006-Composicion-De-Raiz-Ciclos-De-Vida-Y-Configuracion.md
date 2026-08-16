# ADR-00006 — Composición de raíz única: ciclos de vida y configuración en un solo lugar

**Proyecto de código:** GeometriaFactory-Api
**Documento:** ADR-00006-Composicion-De-Raiz-Ciclos-De-Vida-Y-Configuracion.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Persistencia

---

## 1. Contexto

Este proyecto de código es el único que ve a los otros tres a la vez, y por lo tanto el único que puede conectar los **cuatro** puertos que declara `GeometriaFactory-Application` con los **cuatro** adaptadores que implementa `GeometriaFactory-Infrastructure`. Las dos Fases C de esas capas dejaron esa responsabilidad acá de forma explícita: la de aplicación declara que «la composición de raíz de `GeometriaFactory-Api` los conecta», y la de infraestructura declara que **no se autorregistra** y que la configuración **la recibe y no la busca**.

Hay tres cosas del despliegue que sólo esta capa conoce y que las de adentro necesitan: la **ubicación del almacén**, la **clave de firma** y la **vigencia del acceso**. Y una cuarta que esta capa fija para el producto entero: el **límite de tamaño del cuerpo**.

Y hay una advertencia que la capa de aplicación dejó por escrito y que cae exactamente acá: su seguridad frente a invocaciones concurrentes vale **«siempre que dos hilos no compartan la misma instancia de entidad ni el mismo adaptador con estado», condición que le corresponde garantizar a la composición de raíz**.

Motivación upstream: NB-00003, NB-00008; `PRODUCT-INTAKE` §17.1.P.1 · GeometriaFactory-Api, §17.1.P.4 · GeometriaFactory-Api, §17.1.P.5 · GeometriaFactory-Api, §17.1.P.4 · GeometriaFactory-Infrastructure.

## 2. Decisión

**Una sola composición de raíz, que construye el grafo entero al arrancar y falla en construcción si algo falta.** Cinco reglas:

1. **Es el único lugar donde un puerto se conecta con un adaptador.** No hay registro automático por convención ni módulos de composición por área: el defecto característico de esta capa es de **omisión**, y una omisión se detecta comparando contra una lista, no leyendo un módulo.
2. **Los ciclos de vida se declaran acá, y la regla es una sola: nada con estado se comparte entre peticiones.** El contexto de persistencia y los dos adaptadores de repositorio viven **una instancia por operación**, que es lo que el intake declara para el alcance del contexto. Los dos motores del validador, el reloj y los dos mecanismos de seguridad **no tienen estado** y pueden compartirse; su ausencia de estado es una propiedad declarada de aquella capa y no una suposición de ésta.
3. **Toda la configuración del despliegue entra por acá y se distribuye hacia adentro**: ubicación del almacén, clave de firma, vigencia del acceso y límite de cuerpo. **Ningún otro componente lee configuración**, ni de variables de entorno ni de archivos.
4. **La ubicación del almacén apunta a un volumen persistente, nunca dentro de la imagen**, y la clave de firma llega por variable de entorno o archivo montado, **fuera del repositorio de código y fuera de la imagen**. Si falta cualquiera de las dos, el servicio **no arranca**.
5. **Si un puerto queda sin adaptador, la construcción falla y no hay servicio.** No se difiere la resolución al primer uso: un fallo en la primera petición ocurre en producción y sin nadie mirando; un fallo en construcción ocurre en el arranque y lo ve la comprobación del despliegue.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Composición única, con resolución verificada en el arranque (**adoptada**) | La frontera es contable en un solo archivo; un puerto sin adaptador falla antes de atender; la condición que la capa de aplicación dejó a cargo de la composición se garantiza en un solo lugar | El archivo de composición concentra todo el ensamblado y crece con el producto |
| Composición repartida en módulos, uno por área | Cada área declara lo suyo y el archivo no crece | **Descartada.** La frontera dejaría de ser contable de una mirada, y comprobar que los cuatro puertos están conectados exigiría recorrer varios módulos. Es exactamente la forma en que un defecto de omisión sobrevive |
| Registro automático por convención de nombres | Un adaptador nuevo queda conectado sin tocar nada | **Descartada, y es la peor para este producto.** Un puerto sin adaptador no fallaría en construcción: fallaría en la primera petición. Y `GeometriaFactory-Infrastructure` decidió por escrito **no autorregistrarse**, justamente para que la frontera se audite desde acá |
| Resolución diferida al primer uso | Arranque más rápido | **Descartada.** Convierte un fallo de despliegue en un fallo de producción, y el despliegue de este producto es manual y domiciliario: nadie lo va a estar mirando |
| Compartir una instancia del contexto de persistencia entre peticiones | Menos construcción por petición | **Descartada.** El intake declara el alcance del contexto **una por operación**, y compartirlo violaría la condición que la capa de aplicación dejó explícitamente a cargo de la composición de raíz |

## 5. Consecuencias positivas

1. Los cuatro puertos quedan conectados en un lugar contable, y la prueba correspondiente es de arranque, no de recorrido.
2. La condición que la capa de aplicación dejó a cargo de la composición —que dos hilos no compartan instancia de entidad ni adaptador con estado— queda garantizada por una regla de ciclo de vida y no por disciplina.
3. Un despliegue sin volumen montado o sin clave de firma **no arranca**, en lugar de arrancar y perder datos o emitir accesos sin firma verificable.
4. La configuración de intercambio de [`ADR-00002`](ADR-00002-Formato-De-Intercambio-Y-Su-Configuracion.md) tiene un lugar único donde declararse, que es lo que hace verificable el «exactamente una» de aquella ADR.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta un archivo de composición que concentra todo el ensamblado**, y que crece con el producto. Es el precio de que la frontera se lea de una vez.
2. **Se acepta construir el contexto de persistencia una vez por operación**, con el costo que eso tiene por petición. Es lo que el intake ya decidió.
3. **Se acepta que el arranque sea más lento** por resolver todo el grafo antes de escuchar, a cambio de que el fallo ocurra donde se lo puede ver.
4. **Se acepta depender de que los componentes sin estado de la capa de infraestructura sigan sin tenerlo.** Es una propiedad declarada de aquella capa, y si dejara de valer, el que se rompe es este ciclo de vida.

## 7. Implementación

- La composición de raíz de [`../Arquitectura-Proyecto-Codigo.md`](../Arquitectura-Proyecto-Codigo.md) §3.1 es el único componente que ve a los tres proyectos de código referenciados a la vez.
- **Convención impuesta:** ningún punto de acceso ni ninguna superficie construye una dependencia por su cuenta.
- **Convención impuesta:** ningún componente lee configuración. La recibe.
- La preparación del almacén se dispara **después** de construir el grafo y **antes** de escuchar ([`ADR-00007`](ADR-00007-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md)).
- El identificador del cuarto puerto **no se fija acá**: lo declara `GeometriaFactory-Application` y su ADR-00002 lo ató al punto de control de la etapa `a`. La composición lo nombra en lenguaje de dominio hasta entonces.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Puertos conectados a su adaptador | **4 de 4**, y **0** con más de uno o con ninguno | Prueba de arranque que resuelve las cuatro dependencias |
| Lugares donde se conecta un puerto | Exactamente **1** | Inspección en revisión |
| Componentes que leen configuración fuera de la composición | Exactamente **0** | Inspección en revisión |
| Instancias del contexto de persistencia compartidas entre peticiones | Exactamente **0** | Prueba de dos peticiones concurrentes que comprueba que no comparten unidad de trabajo |
| Arranques exitosos sin clave de firma o sin ubicación de almacén | Exactamente **0** | Prueba de arranque con cada una ausente |
| Fallos de resolución de dependencias detectados en la primera petición | Exactamente **0**: todos se detectan en construcción | Prueba de arranque con un adaptador quitado |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.17** §17.1.P.4 · GeometriaFactory-Infrastructure, §17.1.P.1 · GeometriaFactory-Api, §17.1.P.4 · GeometriaFactory-Api y §17.1.P.5 · GeometriaFactory-Api.
- [`../../02-Especificacion-Funcional/Casos-De-Uso/CU-00010-Componer-La-Aplicacion-Y-Conectar-Los-Puertos-Con-Sus-Adaptadores.md`](../Operaciones-Internas/CU-00010-Componer-La-Aplicacion-Y-Conectar-Los-Puertos-Con-Sus-Adaptadores.md).
- [`../../../GeometriaFactory-Application/05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../Arquitectura-Proyecto-Codigo.md) §4, que es donde queda declarada la condición a cargo de la composición de raíz.
- [`../../../GeometriaFactory-Infrastructure/05-Arquitectura-Tecnica/Adrs/ADR-06001-Adaptadores-Por-Puerto-Sin-Repositorio-Generico.md`](ADR-06001-Adaptadores-Por-Puerto-Sin-Repositorio-Generico.md), que decide no autorregistrarse.
- ADR relacionadas: [`ADR-00001`](ADR-00001-Host-Delgado-Con-Composicion-De-Raiz-Unica.md), [`ADR-00002`](ADR-00002-Formato-De-Intercambio-Y-Su-Configuracion.md), [`ADR-00007`](ADR-00007-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Fija la composición de raíz única como el único lugar donde un puerto se conecta con un adaptador, con la regla de ciclos de vida que garantiza la condición que la capa de aplicación dejó explícitamente a su cargo, la configuración recibida y no buscada, y el fallo en construcción en lugar de en la primera petición. Evalúa cinco alternativas, declara cuatro trade-offs y fija seis métricas de validación. |
