# Glosario funcional — GeometriaFactory-Contracts

**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** Glosario-Funcional.md
**Versión:** 1.4
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `00-Contexto/Vision-Producto.md` §9 (glosario raíz de la cadena: §9.1 términos del dominio del cliente, §9.2 términos que esa categoría precisa, §9.3 resolución del choque de vocabulario); `PRODUCT-INTAKE` 1.3 §4.1, §4.2, §12 y §12.1, §17.4 P.2, P.3, P.5, P.10 y P.11; `Vocabulario-Rules.md` §2, §4 y §9
**Trazabilidad downstream:** `03-UX-UI-DX` (por `Rules-UX-UI-DX.md` §3.3), `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas`, `10-Examples`

---

## Tabla de contenido

- [1. Alcance de este glosario](#1-alcance-de-este-glosario)
- [2. Términos que esta categoría acuña](#2-términos-que-esta-categoría-acuña)
- [3. Términos con más de un referente](#3-términos-con-más-de-un-referente)
  - [3.1 Contrato](#31-contrato)
  - [3.2 Pieza](#32-pieza)
  - [3.3 `Pendiente`](#33-pendiente)
- [4. Términos referenciados y no redefinidos](#4-términos-referenciados-y-no-redefinidos)
- [5. Control de cambios](#5-control-de-cambios)

---

## 1. Alcance de este glosario

Este glosario declara únicamente el vocabulario que la especificación funcional de `GeometriaFactory-Contracts` **acuña**, y referencia lo que ya está declarado en `Vision-Producto.md` §9, que es el glosario raíz de la cadena. Ningún término de §4 se redefine acá.

La regla de inclusión aplicada es la de `Rules-Especificacion-Funcional.md` §3.3: entra al glosario todo término que aparece en más de un artefacto de esta categoría. Un término que vive en un solo caso de uso se define ahí y no entra.

## 2. Términos que esta categoría acuña

| Término canónico | Definición operativa | Artefactos de 02 donde aparece | Sinónimos y alias |
| --- | --- | --- | --- |
| Ensamblado de contratos | El artefacto de compilación que produce este proyecto de código y contra el que se compilan las dos piezas desplegables. Es lo único que cruza la frontera entre ellas | `Especificacion-Funcional.md`, CU-01 a CU-08, `README.md` | «el contrato», en el primer referente de §3.1 |
| Tipo de transferencia | Cada tipo declarado por el ensamblado de contratos. Es plano y sin comportamiento: transporta datos y no decide nada | `Especificacion-Funcional.md`, CU-01 a CU-08 | «DTO» en el vocabulario del intake §17.4 |
| Superficie pública del contrato | El conjunto de tipos de transferencia y de campos que el ensamblado expone a quien lo referencia. Es lo que se inspecciona para verificar qué se expone y qué no | `Especificacion-Funcional.md`, CU-01, CU-02, CU-04, CU-05, CU-06 | — |
| Carga útil | El contenido que viaja en una solicitud o en una respuesta, expresado con tipos de transferencia | `Especificacion-Funcional.md`, CU-04, CU-05, CU-06 | «payload» en el vocabulario del intake; no se usa esa forma en la documentación generada |
| Frontera de servicio | El límite entre la pieza pública y la pieza de datos, que sólo se atraviesa con tipos de transferencia del ensamblado de contratos | CU-01, CU-03, CU-05 | — |
| Consumidor del contrato | El código que compila contra el ensamblado de contratos. Son dos: el de la pieza pública y el de la pieza de datos. **Es el actor de todos los casos de uso de esta categoría**: no hay actor humano | `Especificacion-Funcional.md`, CU-01 a CU-08, `README.md` | — |
| Texto original del trabajo | La cadena exacta que la persona cargó, transportada sin interpretar y sin modificar ni un carácter. Es el campo alrededor del cual gira CU-03 | `Especificacion-Funcional.md`, CU-03, CU-04, CU-05 | «texto crudo» en el intake §17.4 P.11; la forma canónica acá es «texto original del trabajo» |
| Proyección de listado | El tipo de transferencia que representa un trabajo dentro de una colección, deliberadamente pobre: sin texto original y sin componentes de las piezas | `Especificacion-Funcional.md`, CU-04, CU-05 | «elemento de listado» |
| Detalle del trabajo | El tipo de transferencia que representa un trabajo completo: datos, texto original, piezas, componentes y observaciones. Es el único del ensamblado que transporta el texto original entero | `Especificacion-Funcional.md`, CU-03, CU-04, CU-05 | — |
| Índice de figura | La posición de una pieza dentro del conjunto raíz del texto original, usada para ubicar una observación. Es la identidad de la pieza, porque el dato no trae identificador propio | `Especificacion-Funcional.md`, CU-03, CU-05, CU-06 | «índice de pieza» en el intake §4 (F-13) |
| Campo señalado | El nombre del campo del texto original al que una observación apunta | `Especificacion-Funcional.md`, CU-03, CU-05, CU-06 | — |
| Respuesta de error neutra | La forma única con la que un fallo cruza la frontera de servicio: código de un conjunto cerrado, texto que no revela infraestructura, y ubicación del defecto cuando la hay | `Especificacion-Funcional.md`, CU-01 a CU-08 | — |
| Cambio incompatible de contrato | Modificación de la superficie pública que rompe la compilación de al menos uno de los dos consumidores, o que los deja sin cubrir todos los casos aunque compile | `Especificacion-Funcional.md`, CU-01 a CU-08, `README.md` | «breaking» en el intake §17.4 P.7; no se usa esa forma |
| Contraseña provisoria | Contraseña que **el sistema produce** cuando el administrador resetea una cuenta de alumno, y que el administrador le comunica **por fuera del producto**. Viaja en claro dentro del **resultado** del reseteo —no dentro de la solicitud, que no lleva contraseña—; lo que ninguna respuesta transporta es su forma almacenada | `Especificacion-Funcional.md`, CU-08, CU-02 | — |
| Cambio de contraseña pendiente | Condición de una cuenta que fue reseteada y todavía no reemplazó su contraseña provisoria. **No viaja como campo de la respuesta de sesión**: viaja como respuesta de error con código propio (`RT-10`) | `Especificacion-Funcional.md`, CU-08, CU-01 | «Marca de cambio de contraseña pendiente» en `GeometriaFactory-Domain`, donde es un atributo. Acá es una condición transportada, no un atributo |
| Despliegue conjunto | Regla operativa según la cual, ante un cambio incompatible de contrato, la pieza pública y la pieza de datos se publican juntas | `Especificacion-Funcional.md`, CU-01 a CU-08 | — |
| Papel | El valor, dentro de un conjunto cerrado de dos, que declara con qué papel del producto opera una persona: alumno o administrador. El contrato lo **transporta** en la respuesta de sesión y lo usa la pieza pública para decidir qué panel arma; **no lo hace cumplir**, que es responsabilidad de la pieza de datos. Los dos papeles son fijos: el producto no admite papeles configurables ni permisos finos | `Especificacion-Funcional.md` §4.1, CU-01, CU-02, CU-04, CU-05 | «rol» en el vocabulario del intake §17.5 P.5; no se usa esa forma en la documentación generada. No se confunde con «papel en la pieza», que designa la función de un componente dentro de una figura y aparece siempre calificado |
| Estado degradado | La situación en la que la pieza pública sigue en pie y no puede obtener datos porque la pieza de datos no responde. El contrato la sostiene con una respuesta de error de código propio y texto neutro, para que la persona reciba un aviso explícito y **nunca una pantalla rota ni un fallo silencioso**. Se distingue por tipo, no por conteo, de una colección vacía | `Especificacion-Funcional.md` §4.1, CU-01, CU-02, CU-03, CU-04, CU-05, CU-06 | — |
| Desenlace | La decisión del administrador que resuelve un trabajo en estado `Pendiente`, dentro de un conjunto cerrado de dos valores: aprobar y rechazar. El contrato la transporta como campo de la solicitud de CU-07 y devuelve el estado terminal alcanzado | `Especificacion-Funcional.md` §4.1 y §4.2, CU-04, CU-05, CU-06, CU-07 | «aprobar / rechazar» en el glosario raíz, que declara las dos decisiones; «desenlace» es el nombre del conjunto que las agrupa y lo acuña esta categoría |
| Estado terminal | Cada uno de los dos estados del trabajo de los que no sale ninguna transición: `Finalizado` y `Rechazado`. El contrato no declara ningún tipo que permita salir de ellos, y esa ausencia es deliberada | `Especificacion-Funcional.md` §4.2, CU-03, CU-04, CU-05, CU-06, CU-07 | — |
| Señal declarada que no es error | Un código que un caso de uso nombra en su subsección §6.1 y que **no** produce respuesta de error ni forma parte del conjunto cerrado de códigos de CU-06. Son tres: la colección vacía de CU-04 y el texto que no verifica en CU-03 y en CU-05 | CU-03, CU-04, CU-05, CU-06, `Especificacion-Funcional.md` §6 | — |
| Situación de cuenta | El valor, dentro de un conjunto cerrado, que declara si una cuenta está pendiente, habilitada o bloqueada. El contrato lo transporta; no lo hace cumplir | `Especificacion-Funcional.md`, CU-01, CU-02 | «estado de la cuenta»; se prefiere «situación» para no colisionar con el estado del trabajo |
| Estado del trabajo | Referenciado, no redefinido: ver §4. El conjunto cerrado que el contrato transporta tiene **cuatro** valores —`Borrador`, `Pendiente`, `Finalizado` y `Rechazado`—, de los cuales los dos últimos son terminales | `Especificacion-Funcional.md`, CU-03, CU-04, CU-05, CU-06, CU-07 | La entrada canónica está en `Vision-Producto.md` §9.1; acá sólo se registra que es un conjunto cerrado que cruza la frontera de servicio |
| Credencial de sesión | El dato que la pieza de datos devuelve tras un canje exitoso y que la pieza pública guarda en su propio estado de servidor. El contrato lo transporta como valor opaco | `Especificacion-Funcional.md`, CU-01, CU-03, CU-04, CU-05 | — |

## 3. Términos con más de un referente

Se declaran los tres términos cuyos sentidos **colisionan en el mismo contexto de lectura**, según el criterio de `Vocabulario-Rules.md` §9.2: el contexto de lectura de un subagente es la sección, no el documento. No se reporta ningún otro caso: los términos cuyos sentidos se distinguen solos quedan fuera, por la prohibición de §9.4 y por el anti-patrón de `Rules-Especificacion-Funcional.md` §4.5 sobre calificar ocurrencias de contextos disjuntos.

### 3.1 Contrato

Tres referentes, y los tres se leen en secciones de esta misma cadena documental:

| Referente | Forma que corresponde | Dónde se lee |
| --- | --- | --- |
| El ensamblado de tipos de transferencia que produce este proyecto de código | **«ensamblado de contratos»** cuando se nombra el artefacto | `Especificacion-Funcional.md`, los ocho casos de uso, el `README.md` de la sección |
| El acuerdo de uso que cada caso de uso describe: qué se envía, qué se recibe y qué no se expone | **«contrato de uso»**, o «el contrato de CU-XX» con el identificador del caso de uso | Los ocho casos de uso, §1 y §17 de cada uno |
| El contrato de verificación `VER-XX` de un sample | **«contrato de verificación»**, siempre calificado | `10-Examples`, aguas abajo de esta categoría |

La forma desnuda «el contrato», sin calificar, sólo se admite dentro de una sección donde ya se nombró el referente en su forma completa. En títulos, en cabeceras de trazabilidad y en la primera mención de cada sección se escribe la forma calificada.

Evidencia de que los contextos colisionan: los tres referentes conviven en la cadena `02 → 05 → 08 → 10` del mismo proyecto de código, y `08-Calidad-Y-Pruebas` es upstream de `10-Examples`, de modo que un subagente que reciba «el contrato de este proyecto de código» como sección suelta no puede decidir si le hablan del ensamblado o de la verificación del sample.

### 3.2 Pieza

Dos referentes, declarados en el glosario raíz y reproducidos acá porque los dos aparecen en esta categoría:

| Referente | Forma que corresponde | Dónde se lee |
| --- | --- | --- |
| Cada figura del conjunto raíz del trabajo (`Vision-Producto.md` §9.1) | **Forma desnuda: «pieza»** | CU-03, CU-04, CU-05 |
| Cada artefacto del producto que se despliega por separado (`Vision-Producto.md` §9.2) | **Siempre calificado: «pieza pública», «pieza de datos», «piezas desplegables»** | CU-01 a CU-08, `Especificacion-Funcional.md`, `README.md` |

Evidencia de que los contextos colisionan: en CU-04 y en CU-05 los dos referentes aparecen en la misma sección —la carga útil que la **pieza pública** consume trae la colección de **piezas** del trabajo—, de modo que la forma desnuda sin la calificación del segundo referente sería ambigua para un lector que entra por esa sección. La resolución adoptada es la que ya fijó `Vision-Producto.md` §9.2 y no se altera acá: se referencia y se cumple.

### 3.3 `Pendiente`

Dos referentes, y la resolución **ya está decidida aguas arriba**: `PRODUCT-INTAKE` 1.3 §4.2 la declara vinculante para toda la documentación generada y `Vision-Producto.md` §9.2 la recoge. Acá se referencia y se cumple; no se vuelve a decidir.

| Referente | Forma que corresponde | Dónde se lee |
| --- | --- | --- |
| El estado de una **cuenta** registrada y todavía no habilitada por el administrador | **«cuenta `Pendiente`»** | CU-01, CU-02 |
| El estado de un **trabajo** enviado, con el texto interpretado sin errores, a la espera de revisión | **«trabajo en estado `Pendiente`»** | CU-03, CU-04, CU-05, CU-06, CU-07 |

**La forma desnuda no se usa.** Dos excepciones que no son incumplimiento y que se declaran para que ninguna ronda de auditoría las levante: las **enumeraciones del conjunto cerrado** —«`Borrador`, `Pendiente`, `Finalizado` y `Rechazado`»—, donde el referente lo fija el conjunto que se está enumerando, y los **identificadores literales de código**, como `CONTRATO_CUENTA_NO_HABILITADA`, que son cadenas y no prosa. Calificarlas sería el falso positivo que `Vocabulario-Rules.md` §9.1 tipifica como defecto.

Evidencia de que los contextos colisionan: este proyecto de código es el único del producto donde los dos referentes cruzan **el mismo contrato**. CU-01 transporta la situación de una cuenta y CU-04 el estado de un trabajo, y `Especificacion-Funcional.md` §4.1 los nombra a los dos en la misma tabla.

## 4. Términos referenciados y no redefinidos

Declarados en `Vision-Producto.md` §9 y usados en esta categoría con la misma semántica. Se referencian y no se redefinen, por la regla de no duplicación de `Rules-Especificacion-Funcional.md` §3.3.

| Término | Dónde está declarado | Uso en esta categoría |
| --- | --- | --- |
| Trabajo | `Vision-Producto.md` §9.1 | Es lo que el alumno entrega en el laboratorio. No es «unidad de entrega»: ese término normativo designa a las dos piezas desplegables |
| Pieza (referente del dominio) | `Vision-Producto.md` §9.1 | Cada figura del conjunto raíz del trabajo; ver §3.2 |
| Pieza pública, pieza de datos | `Vision-Producto.md` §9.2 | Los dos consumidores del ensamblado de contratos; ver §3.2 |
| Componente | `Vision-Producto.md` §9.1 | Figura plana que forma parte de una pieza; viaja dentro del detalle de CU-05 |
| Observación | `Vision-Producto.md` §9.1 | Superordinado de advertencia y de error de validación; es el tipo que CU-05 transporta con su severidad |
| Advertencia | `Vision-Producto.md` §9.1 | Discrepancia entre valor declarado y derivado; **no impide que el trabajo pase a estado `Pendiente`** |
| Error de validación | `Vision-Producto.md` §9.1 | Defecto que impide interpretar el texto; **impide que el trabajo pase a estado `Pendiente`**, que queda en `Borrador` con sus errores localizados |
| Estado del trabajo | `Vision-Producto.md` §9.1 | Conjunto cerrado de cuatro valores, con `Finalizado` y `Rechazado` terminales; el contrato lo transporta en el listado de CU-04 y en el detalle de CU-05 |
| Enviar | `Vision-Producto.md` §9.1 | La única acción de guardado del alumno: el contrato declara una sola solicitud de guardado en CU-03, y el estado lo decide la interpretación |
| Aprobar / Rechazar | `Vision-Producto.md` §9.1 | Las dos decisiones del administrador sobre un trabajo en estado `Pendiente`; el contrato las transporta como los dos valores del desenlace de CU-07 |
| Comentario | `Vision-Producto.md` §9.1 | Texto libre y opcional del administrador. **No es calificación y no es observación**; viaja en el detalle de CU-05 como bloque propio, nunca dentro de la colección de observaciones |
| `Pendiente`, forma calificada obligatoria | `Vision-Producto.md` §9.2 | Nombra dos estados distintos, el de una cuenta y el de un trabajo, y los dos cruzan este mismo contrato. Se escribe siempre «cuenta `Pendiente`» o «trabajo en estado `Pendiente`»; la forma desnuda no se usa. Ver §3.3 |
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
| 1.1 | 2026-08-09 | Actualización por contenido nuevo aguas arriba: `PRODUCT-INTAKE` 1.3 §4.1, §4.2 y §12, y `Vision-Producto.md` §9 en su versión 1.1. Cambios: §2 da de alta tres términos que esta categoría acuña —**desenlace**, **estado terminal** y **señal declarada que no es error**—, y la entrada «estado del trabajo» pasa a referenciar el glosario raíz en lugar de definirlo, ahora con cuatro valores; §3 suma la subsección **§3.3 `Pendiente`**, que referencia la resolución ya decidida en `PRODUCT-INTAKE` §4.2 y `Vision-Producto.md` §9.2, con la evidencia de que los dos referentes cruzan este mismo contrato y con las dos excepciones que no se califican; §4 suma cinco términos referenciados del glosario raíz —estado del trabajo, enviar, aprobar / rechazar, comentario y la forma calificada obligatoria de `Pendiente`—. La tabla de §2 pasa de diecinueve a **veintidós** términos acuñados y la de §4 de diecinueve a **veinticuatro** referenciados. **Precisión de la misma intervención**: las glosas de «advertencia» y «error de validación» de §4 seguían diciendo «impide finalizar», acción que el modelo vigente no tiene, y se alinean con el enunciado actualizado del glosario raíz, que corta en el paso a estado `Pendiente`.  **Corrección de la ronda 3 de auditoría, hallazgo H-03**, absorbida sin subir versión: ocho celdas de ámbito de §2, §3.1 y §3.2 declaraban «CU-01 a CU-06» para términos que también aparecen en `CU-07`, y pasan a «CU-01 a CU-07» y a «los siete casos de uso». Ningún término cambia de definición ni de criterio de inclusión. **Autor:** Analista Funcional + API Designer (AG-02) |
| 1.2 | 2026-08-09 | Actualización por contenido nuevo aguas arriba: `PRODUCT-INTAKE` **1.7** incorpora la capacidad **F-26** y la categoría 02 emite **CU-08**. Cambios: §2 da de alta dos términos —**contraseña provisoria** y **cambio de contraseña pendiente**—, el segundo con la advertencia de que en este proyecto de código es una **condición transportada** y no el atributo que `GeometriaFactory-Domain` declara con el mismo nombre; la tabla de §2 pasa de veintidós a **veinticuatro** términos acuñados. Las celdas de ámbito que declaraban «CU-01 a CU-07» pasan a «CU-01 a CU-08» y las que decían «los siete casos de uso», a ocho. Ningún término cambia de definición. **Autor:** Analista Funcional + API Designer (AG-02) |
| 1.3 | 2026-08-09 | Actualización por la decisión del Product Owner que `CU-08` 1.2 absorbe: **la contraseña provisoria la produce el sistema y no la escribe el administrador**, porque una provisoria escrita por el docente termina siendo la misma clave para toda la comisión. La entrada **contraseña provisoria** de §2 corrige su definición —quién la produce y en qué sentido viaja, en el resultado y no en la solicitud— sin cambiar su ámbito ni su alias. **Ningún término se da de alta ni de baja**: la tabla de §2 sigue con veinticuatro términos acuñados. **Autor:** Analista Funcional + API Designer (AG-02) |
| 1.4 | 2026-08-09 | **Cierra la parte del hallazgo `F26-20` y la parte del hallazgo `F26-27`** que alcanzan a este archivo, del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0, contra `PRODUCT-INTAKE` **1.10**. **`F26-20`**: §3.1 remitía a «los **seis** casos de uso» al declarar dónde se lee el referente «ensamblado de contratos», y el catálogo tiene **ocho** desde que entraron `CU-07` y `CU-08`; la fila inmediatamente siguiente ya decía «los ocho casos de uso». **`F26-27`**: **Cierra la parte del hallazgo `F26-27`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0 que alcanza a este archivo: el control de cambios tenía **filas con más celdas que columnas** —la celda de autor sobrante, sobre una tabla de tres columnas—, y el texto de esas filas se conserva íntegro: el autor pasa a leerse dentro de la celda de cambios, en lugar de en una cuarta columna que la tabla no declara. **Ningún término cambia de definición, de forma obligatoria ni de criterio de inclusión**, y el recuento de términos acuñados no cambia. Sube minor: corrige un recuento de remisión y repara la tabla de este control de cambios. |
