# Glosario funcional — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** Glosario-Funcional.md
**Versión:** 1.2
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `00-Contexto/Vision-Producto.md` §9 (glosario raíz de la cadena); `Especificacion-Funcional.md` §3, §4 y §6 y `Definicion-Superficie-HTTP.md` completo, de este proyecto de código; los glosarios funcionales de `GeometriaFactory-Domain`, `GeometriaFactory-Application`, `GeometriaFactory-Contracts` y `GeometriaFactory-Infrastructure`; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.26** §12 y §12.1; `Vocabulario-Rules.md` §2 y §9, en cuanto a los seis términos normativos y al criterio de polisemia
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas`, `09-Devops` y `10-Examples` de GeometriaFactory-Api

---

## Tabla de contenido

- [1. Alcance de este glosario](#1-alcance-de-este-glosario)
- [2. Términos que esta categoría acuña](#2-términos-que-esta-categoría-acuña)
- [3. Términos con más de un referente](#3-términos-con-más-de-un-referente)
  - [3.1 Acceso](#31-acceso)
  - [3.2 Código](#32-código)
  - [3.3 Punto](#33-punto)
  - [3.4 Dos casos que no son polisemia y no se corrigen](#34-dos-casos-que-no-son-polisemia-y-no-se-corrigen)
- [4. Términos referenciados y no redefinidos](#4-términos-referenciados-y-no-redefinidos)
- [5. Control de cambios](#5-control-de-cambios)

---

## 1. Alcance de este glosario

Acá se declaran únicamente los términos que **esta** categoría acuña para **este** proyecto de código, y que aparecen en más de uno de sus artefactos. Todo lo demás se **referencia** en §4, sin redefinirlo:

- `00-Contexto/Vision-Producto.md` §9 es el **glosario raíz de la cadena**.
- Los glosarios funcionales de los cuatro proyectos de código que este ensambla o transporta declaran el vocabulario de sus capas, y esta categoría lo usa con la misma semántica.

Rigen sin excepción las resoluciones de vocabulario del producto: **`Pendiente` va siempre calificado** —«cuenta `Pendiente`» o «trabajo en estado `Pendiente`»—, salvo en las enumeraciones del conjunto cerrado y en los identificadores literales; **«trabajo» no es «unidad de entrega»**; y **la palabra «proyecto» a secas no se usa**.

Una precisión de vocabulario propia de esta categoría, que conviene fijar antes de la tabla: **acá se dice «punto de acceso» y no «endpoint»**, y **«código de respuesta» y no «status»**. No es purismo idiomático: es que los dos términos ingleses arrastran connotaciones de otras herramientas, y el producto tiene ya tres referentes distintos para la palabra «código».

## 2. Términos que esta categoría acuña

| Término canónico | Definición operativa | Artefactos donde aparece | Sinónimos y alias |
| --- | --- | --- | --- |
| **Punto de acceso** | Cada una de las **quince** entradas de la superficie, identificada por `A-XX`, con su verbo, su ruta, el papel que exige y sus códigos de respuesta. **No es un caso de uso**: un caso de uso puede describir más de uno | `Definicion-Superficie-HTTP.md`, los doce casos de uso | «El punto» cuando ya está identificado. **No se dice «endpoint»** |
| **Superficie HTTP** | El conjunto de los quince puntos de acceso más las reglas que los gobiernan. **Es todo lo que este proyecto de código existe hacia afuera** | Los tres documentos y los casos de uso | «La superficie» cuando el proyecto de código está nombrado |
| **Código de respuesta** | El número con el que una petición termina. Son **diez** en toda la superficie | `Definicion-Superficie-HTTP.md`, los casos de uso | **No se dice «status» ni «status code»** |
| **Código del contrato** | Cada uno de los **quince** identificadores del conjunto cerrado que declara el ensamblado de contratos. **Esta categoría no agrega ninguno** | `Definicion-Superficie-HTTP.md`, `CU-09`, los casos de uso de puntos | «El código del conjunto cerrado». Ver §3.2 |
| **Las dos traducciones** | El par de conversiones que toda terminación fallida atraviesa: de motivo interno a código del contrato, y de código del contrato a código de respuesta | `Definicion-Superficie-HTTP.md` §5, `CU-09` | — |
| **Guardia de admisión** | La condición que las peticiones de los once puntos que exigen acceso atraviesan antes de llegar a un caso de uso: acceso, papel y marca | `CU-02`, `Definicion-Superficie-HTTP.md` | «La guardia» cuando ya está nombrada |
| **Papel exigido** | El papel que un punto de acceso comprueba sobre el acceso firmado. **No es la autorización completa**, que se hace sobre el dato recuperado y vive en otra capa | `Definicion-Superficie-HTTP.md` §3, `CU-02` | — |
| **Composición de raíz** | El único lugar del producto donde los puertos se encuentran con sus adaptadores y donde entra la configuración del despliegue | `CU-10`, `Especificacion-Funcional.md` §3 | «La composición» |
| **Arranque detenido** | La forma de terminación del arranque cuando el almacén no queda en condiciones: **el servicio no atiende ninguna petición**. El término lo acuñó `GeometriaFactory-Infrastructure` y acá se usa con la misma semántica | `CU-11` | — |
| **Ruta propuesta** | Una ruta de la tabla de puntos de acceso que **esta categoría deriva** y que ninguna fuente declara. Va rotulada fila por fila | `Definicion-Superficie-HTTP.md` §3 | «Propuesta derivada» |
| **Colección de peticiones** | La demostración ejecutable del proyecto de código: la muestra que recorre la superficie con los ocho escenarios como cuerpo, en tres pasos | `CU-12` | «La colección». Es la muestra **S-2** del producto |
| **Señal que no es un fallo** | Un resultado que otro producto trataría como error y que acá viaja en una respuesta exitosa: el texto que no verifica y el listado vacío. Son **dos** | `Definicion-Superficie-HTTP.md` §5, `CU-06`, `CU-07` | «Señal declarada», que es como la nombra el ensamblado de contratos |
| **Hueco del conjunto cerrado** | Un camino que las capas de adentro distinguen y para el que **el contrato no declara código propio**, de modo que sólo queda el genérico. Son **dos**, y están elevados al Product Owner | `CU-09` §10, `Definicion-Superficie-HTTP.md` §9 | — |

## 3. Términos con más de un referente

Los tres siguientes se verificaron contra el criterio de `Vocabulario-Rules.md` §9.1 y §9.2: en los tres, los sentidos aparecen en el **mismo contexto de lectura** —la sección— y por eso se desambiguan. Ninguno se declara acá por analogía con otro.

### 3.1 Acceso

Es la colisión central de esta categoría, y la única que aparece en los doce casos de uso.

| Referente | Forma que corresponde | Evidencia de colisión |
| --- | --- | --- |
| El valor firmado con el que la pieza pública opera contra este servicio | **«acceso firmado»**, o «el acceso» cuando ya está nombrado en la misma oración | `CU-01` §4 y `CU-02` §4 hablan de los tres referentes en la misma sección |
| Una entrada de la superficie | **«punto de acceso»**, siempre completo, o su identificador `A-XX` | `Definicion-Superficie-HTTP.md` §3 |
| La posibilidad de que una cuenta entre al sistema, que el dominio resuelve | **«admisibilidad»**, que es el término de la capa que la resuelve. **No se dice «acceso» para este referente** | `CU-01` §4 pasos 2 y 3 |

Regla de uso, en una línea: **«acceso» a secas designa el valor firmado**, y los otros dos referentes se escriben siempre completos.

### 3.2 Código

| Referente | Forma que corresponde | Evidencia de colisión |
| --- | --- | --- |
| El número con el que una petición termina | **«código de respuesta»**, siempre completo | La tabla de `Definicion-Superficie-HTTP.md` §6 tiene los dos en columnas contiguas |
| El identificador del conjunto cerrado del ensamblado de contratos | **«código del contrato»**, o el identificador literal en mayúsculas | Ídem |
| El texto fuente del producto | **«repositorio de código»**, «solución de código», «proyecto de código»: **nunca «código» a secas** | `CU-10` §6 nombra el repositorio de código y los códigos del contrato en la misma tabla |

Regla de uso: **«código» a secas no se escribe en esta categoría.**

### 3.3 Punto

| Referente | Forma que corresponde | Evidencia de colisión |
| --- | --- | --- |
| Una entrada de la superficie | **«punto de acceso»** | `CU-11` habla del punto de salud y del punto de control en secciones vecinas |
| La detención obligatoria al cerrar una etapa, a la espera del OK explícito | **«punto de control»**, término del glosario raíz, siempre completo | Ídem |
| Un asunto sin resolver | **«punto abierto»**, siempre completo | `Especificacion-Funcional.md` §11 |

### 3.4 Dos casos que no son polisemia y no se corrigen

Se declaran para que una revisión posterior no los levante como hallazgo, que es el falso positivo que `Vocabulario-Rules.md` §9.1 tipifica.

- **«Salud»** tiene un solo referente en esta categoría: el estado del servicio que A-16 informa. No colisiona con nada.
- **Los identificadores `A-XX` y `CU-XX` conviven sin ambigüedad** porque tienen prefijos distintos y porque `Especificacion-Funcional.md` §10 declara que **no son la misma serie**: un caso de uso puede describir más de un punto de acceso, y dos casos de uso nunca comparten uno.

## 4. Términos referenciados y no redefinidos

### 4.1 Del glosario raíz de 00

Trabajo; Pieza; Componente; Observación; Advertencia; Error de validación; Estado del trabajo con sus cuatro valores y la terminalidad de dos de ellos; Enviar como única acción de guardado; Aprobar / Rechazar; Comentario; Valor declarado / valor derivado; Laboratorio; Actividad 1; **Punto de control**; `Pendiente` con su forma calificada obligatoria; Etapa; Capacidad.

### 4.2 De los glosarios funcionales de los cuatro proyectos de código que este ensambla o transporta

| Término | Qué designa, en una línea | Dónde está declarado |
| --- | --- | --- |
| Papel, Estado de cuenta, Credencial derivada | El vocabulario de la cuenta | `GeometriaFactory-Domain` |
| Marca de cambio de contraseña pendiente | El atributo que el reseteo deja y que sólo el cambio efectivo levanta | `GeometriaFactory-Domain` |
| Desenlace, Terminalidad, Alcance del administrador | Las nociones que gobiernan el cierre del circuito | `GeometriaFactory-Domain` |
| Puerto, y los cuatro puertos | Los contratos que la capa de aplicación declara | `GeometriaFactory-Application` |
| Verificación de pertenencia, verificación de facultad | Las dos comprobaciones que **no se hacen acá** | `GeometriaFactory-Application` |
| Alcance de consulta, Unidad de trabajo, Motivo, Doble | El vocabulario de la orquestación | `GeometriaFactory-Application` |
| Contrato de uso, Tipo de transferencia, Conjunto cerrado de códigos | El vocabulario del ensamblado que cruza la frontera | `GeometriaFactory-Contracts` |
| Adaptador, Almacén, Transformación de esquema | El vocabulario de la capa que toca el mundo | `GeometriaFactory-Infrastructure` |
| Terminación degradada | La forma de terminar de una operación que el mundo no dejó completar | `GeometriaFactory-Infrastructure` |
| Trampa del formato, Lectura tolerante, Operador estricto | El vocabulario del dato real del alumno | `GeometriaFactory-Infrastructure` |
| Contraseña provisoria | El valor que el reseteo produce, y que esta superficie devuelve **una sola vez** | `GeometriaFactory-Infrastructure` |
| Acceso firmado, Clave de firma | Lo que se emite para operar contra este servicio, y el secreto con el que se firma | `GeometriaFactory-Infrastructure` |

**Los seis términos normativos del framework** —producto, unidad de entrega, módulo, solución de código, proyecto de código y proyecto— conservan el sentido de `Vocabulario-Rules.md` §2 y no se redefinen acá. Dos precisiones que este proyecto de código vuelve pertinentes:

- **Este proyecto de código sí es una unidad de entrega**, y es una de las dos del producto: se despliega de forma independiente, como contenedor en el servidor propio. La otra es la pieza pública. **El trabajo del alumno no lo es**, y el intake lo declara expresamente.
- **«Proyecto» a secas no se usa**, por el choque de vocabulario que el intake declara: la palabra designa tanto una unidad de compilación como los ejemplos de la cátedra que emiten el dato.

## 5. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Declara los **trece** términos que esta categoría acuña —entre ellos **punto de acceso**, **código de respuesta**, **código del contrato**, **las dos traducciones**, **la guardia de admisión**, **la ruta propuesta**, **la señal que no es un fallo** y **el hueco del conjunto cerrado**—; los **tres** términos con más de un referente con su forma obligatoria y su evidencia de colisión —«acceso», «código» y «punto»—, con las dos reglas de uso que de ahí salen; los dos casos que deliberadamente no se corrigen; y los términos referenciados del glosario raíz y de los cuatro proyectos de código vecinos, que no se redefinen, con la precisión de que **este proyecto de código sí es una unidad de entrega** en el sentido normativo. |
| 1.1 | 2026-08-10 | Actualización por `PRODUCT-INTAKE` **1.13** §4.1 (**RN-16**) y la precisión de **F-04**. Tres entradas de §2 actualizan su recuento y ninguna cambia de referente: los **puntos de acceso** pasan de dieciséis a **quince** con el retiro de `A-04`, la **superficie HTTP** se define sobre esos quince, y los **códigos del contrato** pasan de diecisiete a **quince**. **No se acuña ningún término y no se retira ninguno.** Sube minor. |
| 1.2 | 2026-08-11 | **Cierra el hallazgo `B-API-13` (P3)** del informe [`B-02-03-GeometriaFactory-Api-r1.md`](../../../Audit/B-02-03-GeometriaFactory-Api-r1.md) 1.0. La cabecera citaba `PRODUCT-INTAKE` **1.13** y pasa a citar **1.26**, vigente hoy. Se abrieron §12 y §12.1 del intake 1.26 y **su contenido no cambió** en nada que este glosario afirme, de modo que ninguna definición, ninguna polisemia y ningún recuento se toca: lo que se repara es la trazabilidad, no una afirmación falsa. **Búsqueda de propagación hecha con `grep`**: los tres recuentos que este glosario declara —quince puntos de acceso, quince códigos del contrato y la superficie definida sobre esos quince— se contrastaron contra [`Definicion-Superficie-HTTP.md`](Definicion-Superficie-HTTP.md) §3 y §6 y **coinciden**; no hay ningún recuento envejecido en este archivo. Sube minor. |
