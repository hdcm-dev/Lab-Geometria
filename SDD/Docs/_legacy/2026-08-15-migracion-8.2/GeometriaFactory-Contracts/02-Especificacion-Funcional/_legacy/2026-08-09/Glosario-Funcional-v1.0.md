> **Artefacto archivado — estado `Superado`**
>
> Esta es una **copia archivada** del documento `Glosario-Funcional.md` en su versión **1.0**, tomada el 2026-08-09 por el orquestador SDD antes de que la versión vigente la superara (`Master-Prompt.md` §5 y §5.1).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-09
> - **Versión vigente:** [`Glosario-Funcional.md`](../../Glosario-Funcional.md)
>
> El cuerpo que sigue **no se modifica**: un registro que se corrige después deja de ser un registro. Este archivo no se renombra, no se reenlaza y no vuelve a tocarse.

---

# Glosario funcional — GeometriaFactory-Contracts

**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** Glosario-Funcional.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-08
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `00-Contexto/Vision-Producto.md` §9 (glosario raíz de la cadena: §9.1 términos del dominio del cliente, §9.2 términos que esa categoría precisa, §9.3 resolución del choque de vocabulario); `PRODUCT-INTAKE` §12 y §12.1, §17.4 P.2, P.3, P.5, P.10 y P.11; `Vocabulario-Rules.md` §2, §4 y §9
**Trazabilidad downstream:** `03-UX-UI-DX` (por `Rules-UX-UI-DX.md` §3.3), `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas`, `10-Examples`

---

## Tabla de contenido

- [1. Alcance de este glosario](#1-alcance-de-este-glosario)
- [2. Términos que esta categoría acuña](#2-términos-que-esta-categoría-acuña)
- [3. Términos con más de un referente](#3-términos-con-más-de-un-referente)
  - [3.1 Contrato](#31-contrato)
  - [3.2 Pieza](#32-pieza)
- [4. Términos referenciados y no redefinidos](#4-términos-referenciados-y-no-redefinidos)
- [5. Control de cambios](#5-control-de-cambios)

---

## 1. Alcance de este glosario

Este glosario declara únicamente el vocabulario que la especificación funcional de `GeometriaFactory-Contracts` **acuña**, y referencia lo que ya está declarado en `Vision-Producto.md` §9, que es el glosario raíz de la cadena. Ningún término de §4 se redefine acá.

La regla de inclusión aplicada es la de `Rules-Especificacion-Funcional.md` §3.3: entra al glosario todo término que aparece en más de un artefacto de esta categoría. Un término que vive en un solo caso de uso se define ahí y no entra.

## 2. Términos que esta categoría acuña

| Término canónico | Definición operativa | Artefactos de 02 donde aparece | Sinónimos y alias |
| --- | --- | --- | --- |
| Ensamblado de contratos | El artefacto de compilación que produce este proyecto de código y contra el que se compilan las dos piezas desplegables. Es lo único que cruza la frontera entre ellas | `Especificacion-Funcional.md`, CU-01 a CU-06, `README.md` | «el contrato», en el primer referente de §3.1 |
| Tipo de transferencia | Cada tipo declarado por el ensamblado de contratos. Es plano y sin comportamiento: transporta datos y no decide nada | `Especificacion-Funcional.md`, CU-01 a CU-06 | «DTO» en el vocabulario del intake §17.4 |
| Superficie pública del contrato | El conjunto de tipos de transferencia y de campos que el ensamblado expone a quien lo referencia. Es lo que se inspecciona para verificar qué se expone y qué no | `Especificacion-Funcional.md`, CU-01, CU-02, CU-04, CU-05, CU-06 | — |
| Carga útil | El contenido que viaja en una solicitud o en una respuesta, expresado con tipos de transferencia | `Especificacion-Funcional.md`, CU-04, CU-05, CU-06 | «payload» en el vocabulario del intake; no se usa esa forma en la documentación generada |
| Frontera de servicio | El límite entre la pieza pública y la pieza de datos, que sólo se atraviesa con tipos de transferencia del ensamblado de contratos | CU-01, CU-03, CU-05 | — |
| Consumidor del contrato | El código que compila contra el ensamblado de contratos. Son dos: el de la pieza pública y el de la pieza de datos. **Es el actor de todos los casos de uso de esta categoría**: no hay actor humano | `Especificacion-Funcional.md`, CU-01 a CU-06, `README.md` | — |
| Texto original del trabajo | La cadena exacta que la persona cargó, transportada sin interpretar y sin modificar ni un carácter. Es el campo alrededor del cual gira CU-03 | `Especificacion-Funcional.md`, CU-03, CU-04, CU-05 | «texto crudo» en el intake §17.4 P.11; la forma canónica acá es «texto original del trabajo» |
| Proyección de listado | El tipo de transferencia que representa un trabajo dentro de una colección, deliberadamente pobre: sin texto original y sin componentes de las piezas | `Especificacion-Funcional.md`, CU-04, CU-05 | «elemento de listado» |
| Detalle del trabajo | El tipo de transferencia que representa un trabajo completo: datos, texto original, piezas, componentes y observaciones. Es el único del ensamblado que transporta el texto original entero | `Especificacion-Funcional.md`, CU-03, CU-04, CU-05 | — |
| Índice de figura | La posición de una pieza dentro del conjunto raíz del texto original, usada para ubicar una observación. Es la identidad de la pieza, porque el dato no trae identificador propio | `Especificacion-Funcional.md`, CU-03, CU-05, CU-06 | «índice de pieza» en el intake §4 (F-13) |
| Campo señalado | El nombre del campo del texto original al que una observación apunta | `Especificacion-Funcional.md`, CU-03, CU-05, CU-06 | — |
| Respuesta de error neutra | La forma única con la que un fallo cruza la frontera de servicio: código de un conjunto cerrado, texto que no revela infraestructura, y ubicación del defecto cuando la hay | `Especificacion-Funcional.md`, CU-01 a CU-06 | — |
| Cambio incompatible de contrato | Modificación de la superficie pública que rompe la compilación de al menos uno de los dos consumidores, o que los deja sin cubrir todos los casos aunque compile | `Especificacion-Funcional.md`, CU-01 a CU-06, `README.md` | «breaking» en el intake §17.4 P.7; no se usa esa forma |
| Despliegue conjunto | Regla operativa según la cual, ante un cambio incompatible de contrato, la pieza pública y la pieza de datos se publican juntas | `Especificacion-Funcional.md`, CU-01 a CU-06 | — |
| Papel | El valor, dentro de un conjunto cerrado de dos, que declara con qué papel del producto opera una persona: alumno o administrador. El contrato lo **transporta** en la respuesta de sesión y lo usa la pieza pública para decidir qué panel arma; **no lo hace cumplir**, que es responsabilidad de la pieza de datos. Los dos papeles son fijos: el producto no admite papeles configurables ni permisos finos | `Especificacion-Funcional.md` §4.1, CU-01, CU-02, CU-04, CU-05 | «rol» en el vocabulario del intake §17.5 P.5; no se usa esa forma en la documentación generada. No se confunde con «papel en la pieza», que designa la función de un componente dentro de una figura y aparece siempre calificado |
| Estado degradado | La situación en la que la pieza pública sigue en pie y no puede obtener datos porque la pieza de datos no responde. El contrato la sostiene con una respuesta de error de código propio y texto neutro, para que la persona reciba un aviso explícito y **nunca una pantalla rota ni un fallo silencioso**. Se distingue por tipo, no por conteo, de una colección vacía | `Especificacion-Funcional.md` §4.1, CU-01, CU-02, CU-03, CU-04, CU-05, CU-06 | — |
| Situación de cuenta | El valor, dentro de un conjunto cerrado, que declara si una cuenta está pendiente, habilitada o bloqueada. El contrato lo transporta; no lo hace cumplir | `Especificacion-Funcional.md`, CU-01, CU-02 | «estado de la cuenta»; se prefiere «situación» para no colisionar con el estado del trabajo |
| Estado del trabajo | El valor, dentro de un conjunto cerrado, que declara si un trabajo está en `Borrador`, `Pendiente` o `Finalizado` | `Especificacion-Funcional.md`, CU-03, CU-04, CU-05 | — |
| Credencial de sesión | El dato que la pieza de datos devuelve tras un canje exitoso y que la pieza pública guarda en su propio estado de servidor. El contrato lo transporta como valor opaco | `Especificacion-Funcional.md`, CU-01, CU-03, CU-04, CU-05 | — |

## 3. Términos con más de un referente

Se declaran los dos términos cuyos sentidos **colisionan en el mismo contexto de lectura**, según el criterio de `Vocabulario-Rules.md` §9.2: el contexto de lectura de un subagente es la sección, no el documento. No se reporta ningún otro caso: los términos cuyos sentidos se distinguen solos quedan fuera, por la prohibición de §9.4 y por el anti-patrón de `Rules-Especificacion-Funcional.md` §4.5 sobre calificar ocurrencias de contextos disjuntos.

### 3.1 Contrato

Tres referentes, y los tres se leen en secciones de esta misma cadena documental:

| Referente | Forma que corresponde | Dónde se lee |
| --- | --- | --- |
| El ensamblado de tipos de transferencia que produce este proyecto de código | **«ensamblado de contratos»** cuando se nombra el artefacto | `Especificacion-Funcional.md`, los seis casos de uso, el `README.md` de la sección |
| El acuerdo de uso que cada caso de uso describe: qué se envía, qué se recibe y qué no se expone | **«contrato de uso»**, o «el contrato de CU-XX» con el identificador del caso de uso | Los seis casos de uso, §1 y §17 de cada uno |
| El contrato de verificación `VER-XX` de un sample | **«contrato de verificación»**, siempre calificado | `10-Examples`, aguas abajo de esta categoría |

La forma desnuda «el contrato», sin calificar, sólo se admite dentro de una sección donde ya se nombró el referente en su forma completa. En títulos, en cabeceras de trazabilidad y en la primera mención de cada sección se escribe la forma calificada.

Evidencia de que los contextos colisionan: los tres referentes conviven en la cadena `02 → 05 → 08 → 10` del mismo proyecto de código, y `08-Calidad-Y-Pruebas` es upstream de `10-Examples`, de modo que un subagente que reciba «el contrato de este proyecto de código» como sección suelta no puede decidir si le hablan del ensamblado o de la verificación del sample.

### 3.2 Pieza

Dos referentes, declarados en el glosario raíz y reproducidos acá porque los dos aparecen en esta categoría:

| Referente | Forma que corresponde | Dónde se lee |
| --- | --- | --- |
| Cada figura del conjunto raíz del trabajo (`Vision-Producto.md` §9.1) | **Forma desnuda: «pieza»** | CU-03, CU-04, CU-05 |
| Cada artefacto del producto que se despliega por separado (`Vision-Producto.md` §9.2) | **Siempre calificado: «pieza pública», «pieza de datos», «piezas desplegables»** | CU-01 a CU-06, `Especificacion-Funcional.md`, `README.md` |

Evidencia de que los contextos colisionan: en CU-04 y en CU-05 los dos referentes aparecen en la misma sección —la carga útil que la **pieza pública** consume trae la colección de **piezas** del trabajo—, de modo que la forma desnuda sin la calificación del segundo referente sería ambigua para un lector que entra por esa sección. La resolución adoptada es la que ya fijó `Vision-Producto.md` §9.2 y no se altera acá: se referencia y se cumple.

## 4. Términos referenciados y no redefinidos

Declarados en `Vision-Producto.md` §9 y usados en esta categoría con la misma semántica. Se referencian y no se redefinen, por la regla de no duplicación de `Rules-Especificacion-Funcional.md` §3.3.

| Término | Dónde está declarado | Uso en esta categoría |
| --- | --- | --- |
| Trabajo | `Vision-Producto.md` §9.1 | Es lo que el alumno entrega en el laboratorio. No es «unidad de entrega»: ese término normativo designa a las dos piezas desplegables |
| Pieza (referente del dominio) | `Vision-Producto.md` §9.1 | Cada figura del conjunto raíz del trabajo; ver §3.2 |
| Pieza pública, pieza de datos | `Vision-Producto.md` §9.2 | Los dos consumidores del ensamblado de contratos; ver §3.2 |
| Componente | `Vision-Producto.md` §9.1 | Figura plana que forma parte de una pieza; viaja dentro del detalle de CU-05 |
| Observación | `Vision-Producto.md` §9.1 | Superordinado de advertencia y de error de validación; es el tipo que CU-05 transporta con su severidad |
| Advertencia | `Vision-Producto.md` §9.1 | Discrepancia entre valor declarado y derivado; **no impide finalizar** |
| Error de validación | `Vision-Producto.md` §9.1 | Defecto que impide interpretar el texto; impide finalizar y no impide guardar como borrador |
| Valor declarado / valor derivado | `Vision-Producto.md` §9.1 | El par que viaja en campos propios dentro de la observación de CU-05 |
| Laboratorio | `Vision-Producto.md` §9.1 | Nombre corriente del producto en uso |
| Actividad 1 | `Vision-Producto.md` §9.1 | Emisor del texto que este contrato transporta; no forma parte del producto |
| `Describir()` | `Vision-Producto.md` §9.1 | Método de la cátedra que produce el texto original; vocabulario del emisor |
| Coma final | `Vision-Producto.md` §9.1 | Particularidad del texto original que el contrato transporta sin tocar |
| Tapa | `Vision-Producto.md` §9.1 | Vocabulario del emisor; el contrato no lo interpreta |
| Rectángulo desarrollado | `Vision-Producto.md` §9.1 | Ídem |
| Fallo silencioso | `Vision-Producto.md` §9.1 | Lo que CU-06 elimina al cerrar el conjunto de códigos de error |
| Capacidad (`F-XX`) | `Vision-Producto.md` §9.2 | Ítem del alcance funcional del intake; no es sinónimo de caso de uso |
| Etapa, punto de control | `Vision-Producto.md` §9.1 y §9.2 | Unidad de construcción y su detención obligatoria; se citan en los plazos de los criterios de las NB |
| Proyecto de código | `Vocabulario-Rules.md` §2 y `Vision-Producto.md` §9.3 | Unidad de compilación. **La palabra «proyecto» a secas no se usa**; los de la Actividad 1 se nombran `Ejemplo1` y `Ejemplo2` |
| Unidad de entrega | `Vocabulario-Rules.md` §2 y `Vision-Producto.md` §9.2 | Término normativo que designa a las dos piezas desplegables, no al trabajo del alumno |

## 5. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Declara diecisiete términos acuñados por la especificación funcional de este proyecto de código, dos términos con más de un referente —«contrato» con tres y «pieza» con dos— con la evidencia de colisión de contextos, y diecinueve términos referenciados del glosario raíz sin redefinirlos. |
| 1.0 | 2026-08-08 | Corrección absorbida de la ronda 1 de auditoría (`Audit/B-02-03-GeometriaFactory-Contracts-r1.md`), sin subir versión por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **H-04**: §2 da de alta las dos entradas que faltaban por la regla de inclusión de `Rules-Especificacion-Funcional.md` §3.3 —**«papel»**, presente en cinco artefactos y valor de un conjunto cerrado del contrato, y **«estado degradado»**, presente en seis—, y la tabla pasa de diecisiete a diecinueve términos. Ninguno de los dos está declarado en `Vision-Producto.md` §9, de modo que se acuñan acá y no se referencian. **No se agregó ninguna entrada de §3**: los dos son monosémicos en esta fase, y la auditoría verificó que «papel» tiene contextos disjuntos con «papel en la pieza», caso que `Vocabulario-Rules.md` §9.1 prohíbe corregir calificando; la nota de alias lo deja registrado sin calificar ninguna ocurrencia. |
