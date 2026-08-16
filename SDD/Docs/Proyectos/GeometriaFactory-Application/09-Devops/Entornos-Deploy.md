# Entornos y canales — GeometriaFactory-Application

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** Entornos-Deploy.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero DevOps Senior + Release Engineer (AG-09)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §5 y §8; [`../05-Arquitectura-Tecnica/Adrs/ADR-04002-Cuatro-Puertos-Y-La-Frontera-Que-Declaran.md`](../05-Arquitectura-Tecnica/Adrs/ADR-04002-Cuatro-Puertos-Y-La-Frontera-Que-Declaran.md) 1.0; [`../05-Arquitectura-Tecnica/Adrs/ADR-04003-Versionado-Y-Estabilidad-De-La-Superficie.md`](../05-Arquitectura-Tecnica/Adrs/ADR-04003-Versionado-Y-Estabilidad-De-La-Superficie.md) 1.0; [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) 1.0 §1 y §3; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.21** §10, §13, §14, §16, §17.2.P.3, §17.2.P.4, §17.2.P.5 y §17.2.P.9
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

**Ninguno propio de despliegue y ninguno de publicación.** Las afirmaciones que lo sostienen son de la fuente:

| Afirmación | Dónde está declarada |
| --- | --- |
| No tiene unidad de despliegue propia: se compila dentro del artefacto de agrupación y **viaja embebido en la unidad desplegable del servidor propio, por la vía de `GeometriaFactory-Api`** | `05` §5, primera fila |
| **Ninguna dependencia de infraestructura**: no requiere base de datos, ni almacén de secretos, ni servicio externo. Todo lo que necesita del exterior entra por los **cuatro** puertos | `05` §5, tercera fila |
| No se publica en ningún repositorio de paquetes: `redistribuible` es false | `05` §5, última fila; intake §13 y §17.2.P.7 |

| Ambiente o canal | Destino | Aprobador | Ventana o acuerdo de nivel de servicio |
| --- | --- | --- | --- |
| Contenedor de desarrollo | La máquina de quien construye, y el ejecutor del pipeline | Nadie: no hay promoción hacia él | No aplica |

**Una sola fila, y es la única honesta.** Este proyecto de código no ejecuta nada por su cuenta: es una biblioteca que se carga dentro de otro proceso. El único lugar donde su código corre solo es la batería de pruebas, y ésa corre en el contenedor de desarrollo.

### 1.1 Apartamiento declarado del modelo de la categoría

`Rules-Devops.md` §2.2 fija para el tipo `library` el modelo de canales `preview` / `stable` sobre feed único, y admite apartarse con un ADR que lo justifique. **El ADR existe y es [`ADR-04003`](../05-Arquitectura-Tecnica/Adrs/ADR-04003-Versionado-Y-Estabilidad-De-La-Superficie.md)**, cuyo §2 declara que el contrato se protege por compilación compartida, que **no se publica en ningún repositorio de paquetes** y que por eso no hay deprecación gradual ni versiones conviviendo.

**Declarar acá un `DEV`, un `QA` y un `PROD` sería duplicar los ambientes de `GeometriaFactory-Api` con otro nombre y otro dueño**, que es exactamente el anti-patrón que `Rules-Devops.md` §4.8 nombra: confundir publicación con despliegue. Los ambientes de ejecución donde este ensamblado termina son los de la unidad que lo embebe, y su dueño es la categoría 09 de `GeometriaFactory-Api`.

## 2. Dónde viaja este ensamblado

Es la tabla que reemplaza a la de ambientes, y dice lo que un lector de esta categoría necesita saber:

| Destino | Cómo llega | Quién es dueño de ese despliegue |
| --- | --- | --- |
| El proceso del **servidor propio** | Embebido en la imagen del backend, construida desde `deploy/Dockerfile` multietapa (intake §16), por la vía de `GeometriaFactory-Api` | Categoría 09 de `GeometriaFactory-Api` |
| El proceso del **hosting público** | **No llega.** El front no lo referencia: sus dependencias son `GeometriaFactory-Contracts` y `GeometriaFactory-Visor` | — |

**La segunda fila es la que distingue a este proyecto de código de `GeometriaFactory-Contracts`.** Aquél se carga en los dos procesos y por eso una decisión de plataforma del front lo alcanza; **éste llega a uno solo**. La consecuencia operativa es directa y está en [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §9: un cambio suyo **no obliga a republicar el front**.

## 3. Provisión

**No hay infraestructura declarativa atribuible a este proyecto de código.** `05` §5 declara **ninguna** dependencia de infraestructura: no tiene servidor, red, almacenamiento ni servicio administrado propios, y todo lo que necesita del exterior entra por los cuatro puertos.

Esa frase es más fuerte de lo que parece, y es lo que hace verificable a la definición de calidad de [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §1: **si un caso de uso necesitara algo del entorno que no entra por un puerto, dejaría de ser ejercible con dobles**, y con eso caería la propiedad que justifica el estilo entero del proyecto de código.

La infraestructura del producto existe y está enumerada en el árbol del intake §16 —`deploy/Dockerfile`, `deploy/compose.yaml` y el flujo de trabajo de publicación del front—, pero **pertenece a los dos proyectos de código que se despliegan y no se describe acá**. Lo único de entorno que este proyecto de código usa es la definición del contenedor de desarrollo, común al producto.

## 4. Configuración

| Aspecto | Decisión | Fundamento |
| --- | --- | --- |
| Configuración propia de ejecución | **Ninguna.** No lee variables de entorno ni archivos de configuración: lo que necesita se lo inyecta la composición de raíz de `GeometriaFactory-Api` | `05` §5; intake §17.2.P.2 |
| Persistencia | **No aplica directamente.** Declara el puerto de repositorio y el alcance de la unidad de trabajo —**un caso de uso, una transacción**—, y la implementación es de `GeometriaFactory-Infrastructure` | Intake §17.2.P.4 |
| Reloj | **Es un puerto**, para que las fechas de alta y modificación sean verificables en prueba. No se toma del sistema | Intake §17.2.P.11, punto 3 |
| Variables de entorno del pipeline | **Ninguna** | Decisión de esta categoría, derivada de la tabla de §2.1 de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md): sus tres stages leen el repositorio y escriben recuentos e informes |

**La fila del reloj no es un detalle de estilo.** Un caso de uso que tomara la hora del sistema sería irreproducible en la canalización, y `QG-02` —batería entera en verde— empezaría a fallar por motivos que no son del código. Que el reloj entre por un puerto es lo que hace que la batería sea determinista en cualquier ejecutor.

## 5. Secretos

**Ninguno, y la afirmación es de la fuente y no de esta categoría.** El intake §17.2.P.5 declara que esta capa **no maneja secretos**: la verificación de pertenencia vive acá, pero la comparación de contraseñas y la emisión de accesos no.

| Momento | Secretos | Fundamento |
| --- | --- | --- |
| Construcción | **Ninguno.** El restaurador toma dependencias de la plataforma; no hay publicación que autenticar | Intake §17.2.P.7, por remisión a §17.1.P.7 |
| Prueba | **Ninguno.** La batería corre con dobles de los cuatro puertos, sin base de datos y sin frontera de proceso | `Estrategia-Calidad.md` §1 |
| Ejecución | **Ninguno propio.** La contraseña llega **ya derivada** y la provisoria **ya producida**: esta capa las recibe, no las fabrica | Intake §17.2.P.5 |

**Lo que sí es responsabilidad de esta capa, y conviene no confundirlo con un secreto**, es la **verificación de pertenencia**: el intake §17.2.P.5 la declara distinta de la autorización por rol y no reemplazable por ella —«el rol no alcanza; un alumno autenticado no debe poder leer el trabajo de otro cambiando el identificador en la petición»—, materializa `INV-02` e `INV-03`, y su respuesta ante un recurso ajeno es **«no encontrado», no «no autorizado»** (`RN-04003`). Desde esta categoría, la consecuencia práctica es que **un stage de este proyecto de código que pidiera una credencial sería la señal de que algo se salió de su alcance**.

**No se declara ninguna frecuencia de rotación**: no hay secreto propio que rotar. Los del producto —la clave de firma del servidor propio, la dirección base del servicio de datos y las credenciales del canal de publicación del front— viven fuera del repositorio y su gobierno pertenece a las categorías 09 de `GeometriaFactory-Api` y de `GeometriaFactory-Web`.

## 6. Promoción

La de estado del trabajo, igual que en el resto del producto:

| Transición | Trigger | Aprobador | Registro |
| --- | --- | --- | --- |
| Rama de etapa → rama principal | Fusión del pull request | Product Owner, con OK explícito | Informe de cierre (intake §15) |
| Etapa fusionada → etapa cerrada | Etiqueta al fusionar | El mismo | La etiqueta, y la constancia de `QG-07` sobre las **cuatro** comprobaciones |

**Ninguna transición de este proyecto de código alcanza a un acto de despliegue**, y es lo que lo distingue de `GeometriaFactory-Contracts`, cuyo `QG-08` sí lo hace. Acá la promoción termina en la etiqueta: lo que se despliega es la unidad que lo embebe, y su promoción la gobierna la categoría 09 de `GeometriaFactory-Api`.

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara que este proyecto de código **no tiene ambientes ni canales propios** y registra el apartamiento del modelo `preview` / `stable` apoyado en `ADR-04003` §2. Reemplaza la tabla de ambientes por la de **dónde viaja el ensamblado**, con la fila que lo distingue de `GeometriaFactory-Contracts`: **llega a un solo proceso**, y por eso un cambio suyo no obliga a republicar el front. Declara la ausencia de infraestructura declarativa con el fundamento de que **ninguna dependencia de infraestructura** es lo que hace verificable la definición de calidad de este proyecto de código, la ausencia de configuración propia con el reloj como puerto y su efecto sobre el determinismo de la batería, y la ausencia de secretos con la precisión de que la contraseña llega ya derivada. Declara que **ninguna transición de promoción de este proyecto de código alcanza a un acto de despliegue**. |
