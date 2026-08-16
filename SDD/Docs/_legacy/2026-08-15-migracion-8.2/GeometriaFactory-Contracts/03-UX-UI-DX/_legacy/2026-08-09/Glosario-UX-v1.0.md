> **Artefacto archivado — estado `Superado`**
>
> Esta es una **copia archivada** del documento `Glosario-UX.md` en su versión **1.0**, tomada el 2026-08-09 por el orquestador SDD antes de que la versión vigente la superara (`Master-Prompt.md` §5 y §5.1).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-09
> - **Versión vigente:** [`Glosario-UX.md`](../../Glosario-UX.md)
>
> El cuerpo que sigue **no se modifica**: un registro que se corrige después deja de ser un registro. Este archivo no se renombra, no se reenlaza y no vuelve a tocarse.

---

# Glosario de la sección 03 — GeometriaFactory-Contracts

**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** Glosario-UX.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-08
**Autor:** DX Lead (AG-03)
**Variante:** DX
**Trazabilidad upstream:** `02-Especificacion-Funcional/Glosario-Funcional.md` §2 (diecinueve términos acuñados, incluidas las entradas «papel» y «estado degradado»), §3.1, §3.2 y §4; `02-Especificacion-Funcional/Especificacion-Funcional.md` §4.2 (correspondencia con la previsión de 01, con el prefijo `P·`) y §6; `00-Contexto/Vision-Producto.md` §9 (glosario raíz de la cadena: §9.1, §9.2 y §9.3); `Vocabulario-Rules.md` §2, §4 y §9; `Rules-UX-UI-DX.md` §3.3; `PRODUCT-INTAKE` §12.1, §15, §16, §17.4 P.3, P.5 y P.8
**Trazabilidad downstream:** `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas`, `10-Examples` y `11-Documentacion` de este proyecto de código

---

## Tabla de contenido

- [1. Alcance de este glosario](#1-alcance-de-este-glosario)
- [2. Términos que esta categoría acuña](#2-términos-que-esta-categoría-acuña)
- [3. Términos con más de un referente](#3-términos-con-más-de-un-referente)
  - [3.1 Error](#31-error)
  - [3.2 Contrato y pieza: resueltos aguas arriba](#32-contrato-y-pieza-resueltos-aguas-arriba)
- [4. Términos referenciados y no redefinidos](#4-términos-referenciados-y-no-redefinidos)
- [5. Control de cambios](#5-control-de-cambios)

---

## 1. Alcance de este glosario

Declara únicamente el vocabulario que **esta categoría** acuña: los términos del recorrido de integración y de la superficie pública que aparecen en más de un artefacto de la sección 03. Todo lo demás se referencia y no se redefine.

Dos fuentes están aguas arriba y mandan sobre este documento:

- `00-Contexto/Vision-Producto.md` §9 es el **glosario raíz de la cadena**.
- `02-Especificacion-Funcional/Glosario-Funcional.md` es el glosario de la categoría anterior de este mismo proyecto de código, con diecinueve términos acuñados y dos términos con más de un referente.

Ningún término de esos dos glosarios se redefine acá. Si aparece en un artefacto de esta sección con la misma semántica, se usa tal cual y se lo enumera en §4.

La regla de inclusión aplicada es la de `Rules-UX-UI-DX.md` §3.3: entra al glosario todo término que aparece en más de un artefacto de esta categoría. Un término que vive en un solo documento se define ahí y no entra.

## 2. Términos que esta categoría acuña

| Término canónico | Definición operativa | Artefactos de 03 donde aparece | Sinónimos y alias |
| --- | --- | --- | --- |
| Rol de intervención developer | Quien interviene sobre este proyecto de código. Acá son tres figuras concretas y no un integrador externo hipotético: el mantenedor presente, el mantenedor futuro y el agente de construcción por etapas | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md`, `README.md` | Sustituye a «audiencia» en los artefactos DX. La sección vigente que lo nombra es `Rules-UX-UI-DX.md` §4.2.3 punto 1, y la decisión que hizo el reemplazo está registrada en §9 de esa regla, entrada 1.7 del control de cambios. «Audiencia» queda para el público de un producto con superficie visible. **No se confunde con «papel»**, que designa el papel de una persona dentro del producto y está declarado en `Glosario-Funcional.md` §2 |
| Mantenedor futuro | La misma persona que escribió el ensamblado, meses después y sin el contexto en la cabeza. Es el destinatario para el que se escribe el porqué de cada decisión, y no sólo el qué | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md`, `README.md` | — |
| Agente de construcción por etapas | El agente de IA que recorre la cadena documental acumulando contexto y escribe el código de la etapa en curso. No tiene memoria entre sesiones y **no infiere una prohibición que no esté escrita**: por eso las restricciones se enuncian como predicados verificables | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md`, `DX-Error-Messages.md`, `README.md` | — |
| Regla de exposición | La frase que gobierna todo este proyecto de código: la superficie pública del ensamblado **es donde se decide qué se expone**. Ningún tipo de transferencia incluye el hash de contraseña, la clave de firma ni ninguna dirección de servicio interno | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md`, `DX-Error-Messages.md`, `README.md` | Es la forma corta de `RT-01` y de la regla de arquitectura RA-03 de `PRODUCT-INTAKE` §14. No sustituye a esos identificadores en la trazabilidad |
| Contenedor de desarrollo | El entorno declarado en `.devcontainer/devcontainer.json` dentro del cual ocurre el ciclo entero de construcción y verificación. **El host de desarrollo no tiene las herramientas y no va a tenerlas**: ningún paso documentado puede asumirlas | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md`, `DX-Error-Messages.md`, `README.md` | — |
| Comando de construcción | Cada uno de los ejecutables de `scripts/` que construyen o ejercitan el producto dentro del contenedor de desarrollo. **Se lo nombra así, y no «guion»**, porque en el vocabulario del intake §15 «guion» designa el guion de demostración de una etapa, que es otra cosa | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| Tramo de onboarding | Cada uno de los tres cortes del recorrido de la primera hora —5, 30 y 60 minutos—, con su hito verificable. Un tramo sin hito verificable no es un tramo | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md`, `README.md` | — |
| Primer resultado exitoso | El hito del tramo de 5 minutos: la construcción del ensamblado terminando en 0 **y sin advertencias**. Es el mismo predicado que el quality gate del pipeline, y no se da por cumplido con una salida que sólo carece de errores | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md`, `DX-Error-Messages.md` | Es lo que mide TTFS |
| Cambio de control | Un cambio propuesto sobre la superficie pública que se usa como ejercicio de clasificación: compatible, incompatible o rechazado aunque compile. Los tres del recorrido son el hito del tramo de 1 hora | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| Error de construcción del contrato | Clase C del catálogo. El que rompe la compilación de al menos uno de los dos consumidores del contrato, o el que compila y aun así se rechaza en revisión. Su acción típica es corregir la superficie o desplegar juntas las dos piezas desplegables | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md`, `DX-Error-Messages.md`, `README.md` | Identificadores `DXC-XX`, locales a esta sección |
| Error transportado | Clase T del catálogo. La respuesta de error que el ensamblado **transporta** y no produce: código de un conjunto cerrado, texto neutro, detalles de ubicación y momento | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md`, `DX-Error-Messages.md`, `README.md` | Identificadores `DXT-XX`, locales a esta sección. El tipo que lo transporta es la respuesta de error neutra de `Glosario-Funcional.md` §2 |
| Diagnóstico accionable | Enunciado de un error que dice las tres cosas: qué pasó, por qué pasó y qué hacer al respecto. En la clase T lo hace además **sin filtrar nada** y ubicando el defecto con índice de figura y campo señalado cuando corresponde | `DX-Developer-Experience.md`, `DX-Error-Messages.md`, `README.md` | — |
| Catálogo de errores | El inventario de las dos clases con su diagnóstico accionable, que vive en `DX-Error-Messages.md`. Es el modo how-to del plan de Diátaxis para quien llega con un síntoma | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md`, `DX-Error-Messages.md`, `README.md` | — |
| Inspección de superficie pública | La verificación de qué campos declara un tipo de transferencia, hecha leyendo la superficie y no ejecutando nada. Es la forma en que `RT-01` y `RT-04` se comprueban, y por eso sus criterios se escriben con conteos —«0 campos de»— y no con enunciados de intención | `DX-Developer-Experience.md`, `DX-Error-Messages.md` | Se apoya en «superficie pública del contrato», declarada en `Glosario-Funcional.md` §2, que no se redefine acá |
| Modo Diátaxis | Cada uno de los cuatro modos de documentación —tutorial, how-to, reference, explanation— con su ubicación declarada en la cadena documental. La regla que los ordena es que **ningún modo reescribe el contenido de otro** | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md`, `README.md` | — |
| TTFS / TTFV | Las dos métricas de tiempo del recorrido: hasta el primer resultado exitoso y hasta el primer valor, que acá es clasificar correctamente un cambio de control | `DX-Developer-Experience.md`, `README.md` | Time-to-first-success y time-to-first-value; se usan las siglas con su forma extendida en la primera mención de cada documento |

## 3. Términos con más de un referente

Se declara únicamente el término cuyos sentidos **colisionan en el mismo contexto de lectura**, con el criterio de `Vocabulario-Rules.md` §9.2, donde el contexto de lectura de un subagente es la sección. Los términos cuyos sentidos se distinguen solos quedan fuera: reportarlos sería el falso positivo que §9.4 prohíbe.

### 3.1 Error

Tres referentes, y los tres se leen en la misma sección de este documento y en la misma tabla de `DX-Error-Messages.md`.

| Referente | Forma que corresponde | Dónde se lee |
| --- | --- | --- |
| El defecto que impide interpretar el texto del alumno, especie de «observación» junto con la advertencia | **«error de validación»**, siempre calificado. Declarado en `Vision-Producto.md` §9.1 y referenciado en `Glosario-Funcional.md` §4 | `DX-Error-Messages.md` §3.2 y §4; los detalles de ubicación de `DXT-09` |
| El que rompe la compilación de un consumidor del contrato, o el que se rechaza en revisión aunque compile | **«error de construcción del contrato»**, o «clase C» dentro de una sección donde ya se nombró la forma completa | `DX-Developer-Experience.md` §5.1; `DX-Error-Messages.md` §2.1 y §3.1 |
| La respuesta que el ensamblado transporta cuando un fallo cruza la frontera de servicio | **«error transportado»**, o «clase T» con la misma regla | `DX-Developer-Experience.md` §5.1; `DX-Error-Messages.md` §2.1 y §3.2 |

Evidencia de que los contextos colisionan: en `DX-Error-Messages.md` §3.2, la entrada `DXT-09` es un **error transportado** cuya causa es un **error de validación** del texto del alumno, y su vecina `DXC-04` de §3.1 es un **error de construcción**. Los tres sentidos conviven a pocas líneas de distancia, de modo que la forma desnuda «error» sin calificar no permite decidir de cuál se habla.

La forma desnuda se admite sólo dentro de una sección donde ya se nombró el referente en su forma completa. En títulos, en cabeceras de trazabilidad y en la primera mención de cada sección se escribe la forma calificada.

### 3.2 Contrato y pieza: resueltos aguas arriba

Los dos términos con más de un referente que esta sección hereda ya están resueltos y **no se vuelven a declarar acá**: se cumplen.

- **«Contrato»** tiene tres referentes, declarados en `Glosario-Funcional.md` §3.1: el **ensamblado de contratos**, el **contrato de uso** que describe cada caso de uso, y el **contrato de verificación** `VER-XX` de un sample, que aparece aguas abajo en `10-Examples`. La forma calificada es obligatoria en títulos, en cabeceras de trazabilidad y en la primera mención de cada sección. Los artefactos de esta categoría siguen esa resolución.
- **«Pieza»** tiene dos referentes, declarados en `Vision-Producto.md` §9.2 y reproducidos en `Glosario-Funcional.md` §3.2: la figura del conjunto raíz del trabajo, en **forma desnuda**, y cada artefacto desplegable, **siempre calificado** —«pieza pública», «pieza de datos», «piezas desplegables»—.

## 4. Términos referenciados y no redefinidos

Usados en esta sección con la misma semántica que en su fuente. Se referencian y no se redefinen, por la regla de no duplicación de `Rules-UX-UI-DX.md` §3.3.

| Término | Dónde está declarado | Uso en esta categoría |
| --- | --- | --- |
| Ensamblado de contratos | `Glosario-Funcional.md` §2 | Es el objeto que esta sección documenta |
| Tipo de transferencia | `Glosario-Funcional.md` §2 | Lo que se inspecciona, lo que se ejercita y lo que un cambio incompatible rompe |
| Superficie pública del contrato | `Glosario-Funcional.md` §2 | Base de «inspección de superficie pública» de §2 |
| Consumidor del contrato | `Glosario-Funcional.md` §2 | Los dos: el código de la pieza pública y el de la pieza de datos. **No se confunde con el rol de intervención developer**, que es humano o agente |
| Frontera de servicio | `Glosario-Funcional.md` §2 | El límite que sólo se atraviesa con tipos del ensamblado |
| Carga útil | `Glosario-Funcional.md` §2 | Lo que viaja en cada solicitud o respuesta |
| Cambio incompatible de contrato | `Glosario-Funcional.md` §2 | Lo que los cambios de control enseñan a clasificar |
| Despliegue conjunto | `Glosario-Funcional.md` §2 | La acción correcta ante todo error de construcción de clase incompatible |
| Respuesta de error neutra | `Glosario-Funcional.md` §2 | El tipo que transporta cada error transportado |
| Índice de figura, campo señalado | `Glosario-Funcional.md` §2 | Los dos datos que hacen ubicable un defecto del texto del alumno |
| Texto original del trabajo | `Glosario-Funcional.md` §2 | La cadena que viaja sin interpretarse; `DXC-04` protege esa forma |
| Proyección de listado, detalle del trabajo | `Glosario-Funcional.md` §2 | El par que sostiene `RT-04` y `DXC-06` |
| Situación de cuenta, estado del trabajo, credencial de sesión | `Glosario-Funcional.md` §2 | Conjuntos cerrados cuyos valores nuevos son incompatibles de hecho (`DXC-03`) |
| Papel | `Glosario-Funcional.md` §2 | El valor, dentro de un conjunto cerrado de dos, con el que una persona opera en el producto. Se usa acá en `DXC-03`, que lo enumera junto a los otros conjuntos cerrados cuyo valor nuevo es incompatible de hecho. **La forma «rol» del vocabulario del intake no se usa**, tal como esa entrada declara; y no se confunde con «rol de intervención developer» de §2, que designa a quien interviene sobre el proyecto de código y no a una persona dentro del producto |
| Estado degradado | `Glosario-Funcional.md` §2 | La situación en la que la pieza pública sigue en pie y no puede obtener datos. Es el destino del handoff de `DXT-11` y una de las dos acciones típicas de la clase T en `DX-Error-Messages.md` §2.1. Se distingue de una colección vacía **por el tipo recibido y no por el conteo**, que es exactamente lo que `DXT-N1` protege |
| Observación, advertencia, error de validación | `Vision-Producto.md` §9.1 | Superordinado y sus dos especies. Ver §3.1 por la resolución de «error» |
| Trabajo, pieza, componente | `Vision-Producto.md` §9.1 | Vocabulario del dominio usado en los textos del catálogo |
| Fallo silencioso | `Vision-Producto.md` §9.1 | Lo que `DXT-12` y la regla de ubicar el defecto vienen a eliminar |
| Laboratorio | `Vision-Producto.md` §9.1 | Nombre corriente del producto en los textos neutros propuestos |
| Etapa, punto de control, hito interno, hito demostrable | `Vision-Producto.md` §9.1 y §9.2 | La unidad del plan de entrega y su detención bloqueante, de la que cuelga el lazo de retroalimentación |
| Pieza pública, pieza de datos, piezas desplegables | `Vision-Producto.md` §9.2 | Los dos artefactos que se despliegan juntos ante un cambio incompatible |
| Unidad de entrega | `Vocabulario-Rules.md` §2 y `Vision-Producto.md` §9.2 | Término normativo que designa a las dos piezas desplegables, **no** al trabajo del alumno |
| Proyecto de código | `Vocabulario-Rules.md` §2 y `Vision-Producto.md` §9.3 | Unidad de compilación. **La palabra «proyecto» a secas no se usa**; los de la Actividad 1 se nombran `Ejemplo1` y `Ejemplo2` |

## 5. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-08 | Corrección absorbida de la ronda 1 de auditoría (`Audit/B-02-03-GeometriaFactory-Contracts-r1.md`), sin subir versión por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **H-06**: la fila «Rol de intervención developer» de §2 citaba `Rules-UX-UI-DX.md` §1.7, que no es una sección sino una entrada del control de cambios de esa regla; la cita pasa a §4.2.3 punto 1, que es la sección vigente que nombra el rol de intervención, con la entrada 1.7 de §9 como registro de la decisión. **H-04 en su forma de 03**: §4 suma «papel» y «estado degradado», que aparecen en esta sección y que AG-02 dio de alta en `Glosario-Funcional.md` §2; se **referencian y no se redefinen**, con la nota de que la forma «rol» del intake no se usa y de que «papel» no se confunde con «rol de intervención developer». §4 pasa de veintiuna a veintitrés entradas y §1 actualiza el conteo del glosario de 02 a diecinueve términos. **No se agregó ninguna entrada a §3**: los dos términos son monosémicos en esta fase, y la auditoría verificó los contextos disjuntos de «papel» con «papel en la pieza», caso que `Vocabulario-Rules.md` §9.1 prohíbe corregir calificando. **Alineación con el upstream**: la cabecera suma `Especificacion-Funcional.md` §4.2, que es donde vive la correspondencia con la previsión `P·CU-XX` de 01. | DX Lead (AG-03) |
| 1.0 | 2026-08-08 | Emisión inicial. Declara dieciséis términos que esta categoría acuña —rol de intervención, tramos del recorrido, clases de error y modos de documentación—, resuelve «error» con sus tres referentes y la evidencia de colisión, remite a la resolución ya vigente de «contrato» y de «pieza» sin volver a declararla, y enumera veintiuna entradas de términos referenciados del glosario de 02 y del glosario raíz sin redefinirlos. | DX Lead (AG-03) |
