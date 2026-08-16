# Entornos y canales — GeometriaFactory-Contracts

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** Entornos-Deploy.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero DevOps Senior + Release Engineer (AG-09)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.1 §5 y §8; [`../05-Arquitectura-Tecnica/Adrs/ADR-08003-Versionado-Por-Compilacion-Compartida.md`](../05-Arquitectura-Tecnica/Adrs/ADR-08003-Versionado-Por-Compilacion-Compartida.md) 1.0; [`../08-Calidad-Y-Pruebas/Estrategia-Testing.md`](../08-Calidad-Y-Pruebas/Estrategia-Testing.md) 1.1 §7; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.22** §10, §13, §14, §16, §17.4.P.3, §17.4.P.4, §17.4.P.5, §17.4.P.9 y §17.6.P.7
**Trazabilidad downstream:** [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md), [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md); `Producto/Pipeline-Producto.md`

---

## Tabla de contenido

- [1. Ambientes y canales de este proyecto de código](#1-ambientes-y-canales-de-este-proyecto-de-código)
  - [1.1 Apartamiento declarado del modelo de la categoría](#11-apartamiento-declarado-del-modelo-de-la-categoría)
- [2. Dónde viaja este ensamblado](#2-dónde-viaja-este-ensamblado)
- [3. Provisión](#3-provisión)
- [4. Configuración](#4-configuración)
- [5. Secretos](#5-secretos)
- [6. Promoción](#6-promoción)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Ambientes y canales de este proyecto de código

**Ninguno propio de despliegue y ninguno de publicación**, y las afirmaciones que lo sostienen son de la fuente:

| Afirmación | Dónde está declarada |
| --- | --- |
| No tiene unidad de despliegue propia: **se carga en los dos procesos**, el del hosting público y el del servidor propio | `05` §5, primera fila; intake §17.4.P.9 |
| No se publica en ningún repositorio de paquetes: `redistribuible` es false | `05` §5, última fila; intake §13 y §17.4.P.7 |
| Un cambio incompatible obliga al **despliegue conjunto** de las dos unidades desplegables | `05` §5, fila de orden de despliegue; intake §17.4.P.3 |

| Ambiente o canal | Destino | Aprobador | Ventana o acuerdo de nivel de servicio |
| --- | --- | --- | --- |
| Contenedor de desarrollo | La máquina de quien construye, y el ejecutor del pipeline | Nadie: no hay promoción hacia él | No aplica: no ejecuta nada |

### 1.1 Apartamiento declarado del modelo de la categoría

`Rules-Devops.md` §2.2 fija para el tipo `library` el modelo de canales `preview` / `stable` sobre feed único, y admite quitar ambientes con un ADR que lo justifique. **El ADR existe y es [`ADR-08003`](../05-Arquitectura-Tecnica/Adrs/ADR-08003-Versionado-Por-Compilacion-Compartida.md)**, que en su §2 decide que la compatibilidad la gobierna la compilación compartida y no un esquema de versiones con convivencia, y en su §4 descarta las tres alternativas que habrían justificado tener canales: versionado de rutas del servicio, negociación de versión en tiempo de ejecución y compatibilidad sólo aditiva.

**Este proyecto de código es el caso donde la distinción entre publicar y desplegar se ve mejor de todo el producto.** Nada se publica —no hay feed—, pero el ensamblado **sí llega a los dos ambientes de ejecución del producto**, embebido en las dos unidades que se despliegan. Confundir las dos cosas y declarar acá un `DEV`, un `QA` y un `PROD` habría duplicado los ambientes de `GeometriaFactory-Api` y de `GeometriaFactory-Web` con otro nombre y otro dueño, que es exactamente el anti-patrón que `Rules-Devops.md` §4.8 nombra.

## 2. Dónde viaja este ensamblado

Es la tabla que reemplaza a la de ambientes, y dice lo que un lector de esta categoría necesita saber:

| Destino | Cómo llega | Quién es dueño de ese despliegue |
| --- | --- | --- |
| El proceso del **servidor propio** | Embebido en la imagen del backend, construida desde `deploy/Dockerfile` multietapa (intake §16) | Categoría 09 de `GeometriaFactory-Api` |
| El proceso del **hosting público** | Embebido en la publicación del front, que se sube por FTP con el flujo de trabajo que el intake §17.6.P.7 declara | Categoría 09 de `GeometriaFactory-Web` |

**Los dos destinos se alcanzan desde el mismo estado del repositorio**, y esa es la propiedad que la regla de despliegue conjunto necesita: no hay versión intermedia que resolver ni artefacto publicado que pueda quedar desfasado entre los dos. Lo que puede desfasarse es **el momento de cada despliegue**, y de ahí `QG-08`. El `PD-01` de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §10 —que un cambio de este ensamblado no disparara la publicación del front— quedó **cerrado por el intake 1.22**, que agregó `src/GeometriaFactory.Contracts/` al filtro de rutas de §17.6.P.7. **El desfase de momentos no lo cierra ese cambio**: el front sale al fusionar y el backend a mano, de modo que la coordinación sigue siendo humana. Lo que el mismo intake 1.22 sí fija es el **orden**, **primero el backend**, con el intervalo minimizado y no eliminado.

## 3. Provisión

**No hay infraestructura declarativa atribuible a este proyecto de código.** No tiene servidor, red, almacenamiento ni servicio administrado propios; `05` §5 declara **ninguna** dependencia de infraestructura.

La infraestructura del producto existe y está enumerada en el árbol del intake §16 —`deploy/Dockerfile`, `deploy/compose.yaml` y el flujo de trabajo de publicación del front—, pero **pertenece a los dos proyectos de código que se despliegan y no se describe acá**. Lo único de entorno que este proyecto de código usa es la definición del contenedor de desarrollo, común al producto.

## 4. Configuración

| Aspecto | Decisión | Fundamento |
| --- | --- | --- |
| Configuración de ejecución | **Ninguna.** Son tipos de transferencia planos, sin comportamiento | Intake §17.4.P.2; `05` §1 |
| Persistencia | **No aplica** | Intake §17.4.P.4 |
| Variables de entorno del pipeline | **Ninguna** | [`../08-Calidad-Y-Pruebas/Estrategia-Testing.md`](../08-Calidad-Y-Pruebas/Estrategia-Testing.md) §7 |
| Formato de intercambio | **No se decide acá.** Es una preocupación transversal del producto, con **una sola configuración declarada**, y se decide en `GeometriaFactory-Api` | [`../../../Producto/Vista-Producto.md`](../../../Producto/Vista-Producto.md) §6, fila de formato de intercambio |

**La última fila es la que conviene no perder de vista.** El modo de falla `RI-01` de [`../../../Producto/Vista-Producto.md`](../../../Producto/Vista-Producto.md) §7 es que **los dos extremos se configuren distinto sin romper ninguna compilación**, y es el único modo de falla del contrato que la compilación compartida no atrapa. La mitigación declarada es una sola configuración en todo el producto y la verificación **ejerciendo el servicio real** desde la batería de integración —no comparando dos archivos—, que es la misma batería en la que se mide `QG-05`.

## 5. Secretos

**Ninguno propio, y hay una afirmación más fuerte que corresponde declarar acá**: este ensamblado es **donde se decide qué se expone** (intake §17.4.P.5), y su regla es que ningún tipo incluye el hash de la contraseña, la clave de firma ni ninguna dirección de servicio interno.

| Obligación | Cómo se verifica | Carácter |
| --- | --- | --- |
| **0** campos capaces de transportar el hash de la contraseña, la clave de firma, una dirección de servicio interno, una ruta de archivo de datos o una traza | `QG-03`, con `TC-08015`, `TC-08001`, `TC-08004` y `TC-08019` | **Se rechaza aunque compile** |
| Credenciales usadas en los cuerpos de prueba: **ficticias**, y viajan en claro por diseño del canje, siempre servidor a servidor | [`../08-Calidad-Y-Pruebas/Estrategia-Testing.md`](../08-Calidad-Y-Pruebas/Estrategia-Testing.md) §7, restricción `RT-11` | Declarado |
| Secretos del pipeline de este proyecto de código | **Ninguno**: sus stages leen el repositorio y escriben recuentos | Decisión de esta categoría, derivada de §2.1 de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) |

**Un campo nuevo que la revisión rechaza por la regla de exposición no admite excepción**, y no lo decide esta categoría: [`../08-Calidad-Y-Pruebas/Criterios-Validacion.md`](../08-Calidad-Y-Pruebas/Criterios-Validacion.md) §6 lo declara como la única de sus cuatro situaciones sin salida admitida, con el fundamento de que agregar un campo de diagnóstico es la forma habitual en que ese defecto entra y entra sin que nadie lo note porque compila.

**No se declara ninguna frecuencia de rotación**: no hay secreto propio que rotar. Los del producto —dirección base del servicio de datos y credenciales del canal de publicación del front— viven como secretos del repositorio y su gobierno pertenece a la categoría 09 de `GeometriaFactory-Web` (intake §17.6.P.5).

## 6. Promoción

La de estado del trabajo, igual que en el resto del producto, con **una transición propia que los otros dos proyectos de código de nivel topológico 0 no tienen**:

| Transición | Trigger | Aprobador | Registro |
| --- | --- | --- | --- |
| Rama de etapa → rama principal | Fusión del pull request | Product Owner, con OK explícito | Informe de cierre (intake §15) |
| Etapa fusionada → etapa cerrada | Etiqueta al fusionar | El mismo | La etiqueta |
| **Cambio incompatible → producto desplegado** | Sólo con las **dos** unidades desplegadas desde el mismo estado del repositorio, **primero el backend** (intake §17.6.P.7 desde 1.22) | El mismo, con constancia | La constancia del despliegue conjunto en el informe de cierre |

**La tercera fila es la que hace de este proyecto de código un caso especial dentro del nivel topológico 0**: es el único cuyo gate alcanza a un acto de despliegue, aunque no despliegue nada por sí mismo.

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara que este proyecto de código **no tiene ambientes ni canales propios** y registra el apartamiento del modelo `preview` / `stable` apoyado en `ADR-08003`, que ya había descartado las tres alternativas de versionado con convivencia. Reemplaza la tabla de ambientes por la de **dónde viaja el ensamblado** —los dos procesos desplegables, con el dueño de cada despliegue nombrado— y deja claro que los dos destinos se alcanzan desde el mismo estado del repositorio. Declara la ausencia de infraestructura declarativa, de configuración y de secretos propios, con la advertencia de que el modo de falla `RI-01` del producto es el único que la compilación compartida no atrapa, y que este ensamblado es **donde se decide qué se expone**. Declara la transición de promoción propia: la que exige el despliegue conjunto. |
| 1.1 | 2026-08-11 | **Propagación de las dos decisiones de despliegue del Product Owner** del intake **1.22** §17.6.P.7. **(a)** Registra en §2 que `PD-01` quedó cerrado porque el filtro de rutas del flujo que publica el front incluye hoy `src/GeometriaFactory.Contracts/`, con la constancia de que **eso no cierra el desfase de momentos**. **(b)** Declara el orden de salida —**primero el backend**— en §2 y en la tercera fila de la tabla de promoción, con el intervalo minimizado y no eliminado. Sube la trazabilidad upstream del intake de **1.20** a **1.22** y le agrega §17.6.P.7. |
