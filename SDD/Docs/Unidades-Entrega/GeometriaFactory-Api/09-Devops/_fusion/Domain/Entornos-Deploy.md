# Entornos y canales — GeometriaFactory-Domain

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** Entornos-Deploy.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero DevOps Senior + Release Engineer (AG-09)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/_fusion/Domain/Arquitectura-Proyecto-Codigo.md) 1.0 §5, §7 y §8; [`../05-Arquitectura-Tecnica/Adrs/ADR-02003-Versionado-Y-Estabilidad-De-La-Superficie.md`](../../../05-Arquitectura-Tecnica/Adrs/ADR-02003-Versionado-Y-Estabilidad-De-La-Superficie.md) 1.0 §4; [`../08-Calidad-Y-Pruebas/Estrategia-Testing.md`](../../../08-Calidad-Y-Pruebas/_fusion/Domain/Estrategia-Testing.md) 1.1 §7; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.20** §10, §13, §14, §16, §17.1.P.4 · GeometriaFactory-Domain, §17.1.P.5 · GeometriaFactory-Domain y §17.1.P.9 · GeometriaFactory-Domain
**Trazabilidad downstream:** [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md), [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md); `Producto/Pipeline-Producto.md`

---

## Tabla de contenido

- [1. Ambientes y canales de este proyecto de código](#1-ambientes-y-canales-de-este-proyecto-de-código)
  - [1.1 Apartamiento declarado del modelo de la categoría](#11-apartamiento-declarado-del-modelo-de-la-categoría)
- [2. El único ambiente que existe: el contenedor de desarrollo](#2-el-único-ambiente-que-existe-el-contenedor-de-desarrollo)
- [3. Provisión](#3-provisión)
- [4. Configuración](#4-configuración)
- [5. Secretos](#5-secretos)
- [6. Promoción](#6-promoción)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Ambientes y canales de este proyecto de código

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

**El ADR existe y es anterior a esta categoría.** [`ADR-02003`](../../../05-Arquitectura-Tecnica/Adrs/ADR-02003-Versionado-Y-Estabilidad-De-La-Superficie.md) §4 evaluó como alternativa la publicación en un repositorio de paquetes interno y la descartó con dos motivos: el intake la descarta explícitamente, y agregaría infraestructura a un producto que las fuentes declaran básico. El apartamiento, entonces, **no lo decide 09**: 09 lo registra y lo hace operativo.

**Y hay una razón de fondo para no simular los dos canales.** `Rules-Devops.md` §4.8 declara anti-patrón confundir publicación con despliegue. Declarar acá un canal `preview` y un canal `stable` sin feed detrás sería la versión inversa del mismo error: **inventar publicación donde sólo hay compilación**. Un canal es un destino del que alguien retira un artefacto; acá nadie retira nada, porque el consumidor lo obtiene por referencia de proyecto dentro de la misma construcción.

## 2. El único ambiente que existe: el contenedor de desarrollo

| Aspecto | Decisión | Fundamento |
| --- | --- | --- |
| Dónde ocurre todo el ciclo | Dentro del contenedor de desarrollo | Intake, encabezado de la Parte C, y §10: el host de desarrollo **no tiene ni va a tener** instalado el kit de desarrollo, y ningún guion puede asumirlo en el host |
| Plataforma objetivo | `net10.0` sin sufijo de plataforma, sobre el sistema operativo del contenedor, que es el mismo del servidor del backend | Intake §17.1.P.9 · GeometriaFactory-Domain |
| Dependencias de infraestructura | **Ninguna.** No requiere base de datos, ni almacén de secretos, ni servicio externo | `05` §5, tercera fila |
| Base de datos para pruebas | **Ninguna.** `tiene_persistencia` es false | [`../08-Calidad-Y-Pruebas/Estrategia-Testing.md`](../../../08-Calidad-Y-Pruebas/_fusion/Domain/Estrategia-Testing.md) §7 |
| Definición del contenedor | `.devcontainer/devcontainer.json`, en la raíz del repositorio | Intake §16 |

**El contenedor de desarrollo no es un ambiente de despliegue disfrazado.** No sirve tráfico, no tiene URL y nadie promociona nada hacia él: es donde se construye y se prueba. Llamarlo `DEV` habría abierto la puerta a que alguien pidiera un `QA` detrás.

## 3. Provisión

**No hay infraestructura declarativa que escribir, y su ausencia es consecuencia de §1 y no una deuda.** No hay ambiente que provisionar: no hay servidor, no hay red, no hay almacenamiento y no hay servicio administrado atribuibles a este proyecto de código.

Lo único que se aproxima a una declaración de entorno es el **archivo de definición del contenedor de desarrollo**, y ya está declarado en el árbol del intake §16. Su contenido concreto —la característica de plataforma que instala y su anclaje de versión— es de la etapa `a`.

**La infraestructura del producto sí existe**, pero pertenece a los dos proyectos de código que se despliegan: `deploy/Dockerfile` multietapa y `deploy/compose.yaml` para el backend, y el flujo de trabajo de publicación por FTP para el front (intake §16). **Este documento no los describe**: son de la categoría 09 de esos proyectos de código, y describirlos acá crearía la segunda fuente de verdad que el corpus ya tiene documentada como su defecto más repetido.

## 4. Configuración

| Aspecto | Decisión | Fundamento |
| --- | --- | --- |
| Configuración de ejecución | **Ninguna.** El proyecto de código no lee configuración | `05` §7, citado por [`../08-Calidad-Y-Pruebas/Estrategia-Testing.md`](../../../08-Calidad-Y-Pruebas/_fusion/Domain/Estrategia-Testing.md) §7 |
| Variables de entorno | **Ninguna**, ni en construcción ni en prueba | `Estrategia-Testing.md` §7, fila de variables de entorno |
| Reloj | **No se fija ni se simula**: el momento entra por parámetro | [`../05-Arquitectura-Tecnica/Adrs/ADR-02006-El-Dominio-No-Lee-El-Reloj-Ni-El-Conjunto.md`](../../../05-Arquitectura-Tecnica/Adrs/ADR-02006-El-Dominio-No-Lee-El-Reloj-Ni-El-Conjunto.md) |

**El principio de configuración externa se cumple de la forma más fuerte posible: no habiendo configuración.** Un mapa de variables por ambiente sería una tabla vacía con encabezados.

## 5. Secretos

**Este proyecto de código no maneja ningún secreto**, y la afirmación se puede verificar en dos lugares distintos de la cadena:

| Afirmación | Dónde está declarada |
| --- | --- |
| No maneja secretos: la contraseña llega **ya derivada** y se guarda como valor de credencial derivada, nulo hasta el primer ingreso | Intake §17.1.P.5 · GeometriaFactory-Domain |
| El proyecto de código no deriva ni compara credenciales | [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../../../08-Calidad-Y-Pruebas/_fusion/Domain/Estrategia-Calidad.md) §2, fila de seguridad |
| Ninguno en el ambiente de pruebas | [`../08-Calidad-Y-Pruebas/Estrategia-Testing.md`](../../../08-Calidad-Y-Pruebas/_fusion/Domain/Estrategia-Testing.md) §7 |

**Consecuencias operativas, que sí son de esta categoría:**

- El pipeline de este proyecto de código **no requiere ninguna credencial**: sus tres stages leen el repositorio y escriben informes. Un stage suyo que pidiera un secreto sería una señal de que algo se salió de su alcance.
- La prohibición de confirmar secretos en el repositorio rige igual, y es del producto: el intake §17.2.P.5 · GeometriaFactory-Web declara que las credenciales del canal de publicación del front viven como secretos del repositorio y que **la dirección real del servidor propio no se versiona**. Este proyecto de código no aporta ninguno de los dos, pero comparte el repositorio.
- **No se declara ninguna frecuencia de rotación**, porque no hay secreto propio que rotar. Los del producto pertenecen a la categoría 09 de `GeometriaFactory-Web` y de `GeometriaFactory-Api`.

## 6. Promoción

**No hay promoción entre ambientes ni entre canales, porque no hay ni ambientes ni canales.** Lo que existe es la promoción de estado del trabajo, declarada en [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §6: rama de etapa → rama principal por fusión del pull request, y etapa fusionada → etapa cerrada por etiqueta, las dos con **OK explícito del Product Owner** en el punto de control (intake §15).

**Registro de auditoría de esa promoción**, que es lo que la reemplaza acá:

| Qué queda registrado | Dónde | Fundamento |
| --- | --- | --- |
| El OK explícito del Product Owner, con constancia escrita | Informe de cierre de la etapa, en el directorio de avances que el intake §15 declara | Intake §15, regla de delivery 3 |
| La medición de los dos gates condicionados con su distancia al umbral | El mismo informe | [`../08-Calidad-Y-Pruebas/Criterios-Validacion.md`](../../../08-Calidad-Y-Pruebas/_fusion/Domain/Criterios-Validacion.md) §6 |
| La etiqueta de la etapa | El repositorio | Intake §17.1.P.7 · GeometriaFactory-Domain |

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara que este proyecto de código **no tiene ambientes de despliegue ni canales de publicación**, con las tres afirmaciones de la fuente que lo sostienen, y registra el apartamiento del modelo `preview` / `stable` que `Rules-Devops.md` §2.2 fija para el tipo `library`, apoyado en `ADR-02003` §4, que ya había descartado la publicación en un repositorio de paquetes. Declara el contenedor de desarrollo como único ambiente y por qué **no** se lo llama `DEV`. Declara la ausencia de infraestructura declarativa, de configuración y de secretos propios, cada una con la sección de la fuente que la respalda, y la ausencia de frecuencia de rotación por no haber secreto que rotar. Declara que la infraestructura de despliegue del producto pertenece a los dos proyectos de código que se despliegan y **no se describe acá**. |
