# Glosario de experiencia — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** Glosario-UX.md
**Versión:** 2.0
**Estado:** Propuesto
**Fecha:** 2026-08-16
**`tipo_unidad_entrega` (D8):** `rest-api` · **Unidad de entrega principal del producto**
**Proyectos de código que la componen:** `GeometriaFactory-Api`, `GeometriaFactory-Domain`, `GeometriaFactory-Application`, `GeometriaFactory-Infrastructure` y `GeometriaFactory-Contracts`
**Trazabilidad upstream:** [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **2.1**
**Consolida a:** los documentos homónimos de las capas que componen la unidad, por `Audit/Migracion-M10-Consolidacion-Fusion.md` 1.2 §4

---

## 0. Cómo leer este documento

**La unidad de entrega tiene un solo documento de esta clase.** Cada sección lleva **una subsección
por proyecto de código**, con su texto **transpuesto sin reescritura**.

**Las cuatro secciones son comunes.** Un término que dos capas definen distinto **no se unifica**: las dos definiciones quedan con su capa nombrada, porque una polisemia con contextos disjuntos no es un defecto (`Vocabulario-Rules.md` §10).

---

## 1. Alcance de este glosario

### 1.1 `GeometriaFactory-Api`

Acá se declaran únicamente los términos que **esta** categoría acuña para **este** proyecto de código, y que aparecen en más de uno de sus artefactos. Todo lo demás se **referencia** en §4:

- `00-Contexto/Vision-Producto.md` §9 es el **glosario raíz de la cadena**.
- [`../02-Especificacion-Funcional/Glosario-Funcional.md`](../02-Especificacion-Funcional/Glosario-Funcional.md) declara lo que la categoría 02 acuña para este proyecto de código, incluidos **punto de acceso**, **código de respuesta**, **código del contrato**, **las dos traducciones**, **la guardia de admisión**, **la ruta propuesta**, **la señal que no es un fallo** y **el hueco del conjunto cerrado**, y las tres polisemias de «acceso», «código» y «punto».

Ninguna entrada de §2 pisa a ninguna de esas fuentes. Lo único que se acuña acá es el vocabulario de **quien interviene sobre este proyecto de código** y del **recorrido de implementación, de consumo y de despliegue**.

Rigen sin excepción las resoluciones de vocabulario del producto: **`Pendiente` va siempre calificado** —salvo en las enumeraciones del conjunto cerrado y en los identificadores literales—, **«acceso» a secas designa el valor firmado**, **«código» a secas no se escribe**, «trabajo» no es «unidad de entrega», y **la palabra «proyecto» a secas no se usa**.

### 1.2 `GeometriaFactory-Domain`

Acá se declaran únicamente los términos que **esta** categoría acuña para **este** proyecto de código, y que aparecen en más de uno de sus artefactos. Los términos del dominio ya están declarados aguas arriba y se **referencian** en §4:

- `00-Contexto/Vision-Producto.md` §9 es el **glosario raíz de la cadena**.
- `02-Especificacion-Funcional/Glosario-Funcional.md` declara lo que la categoría 02 acuña para este proyecto de código.

Ninguna entrada de §2 pisa a ninguna de las dos fuentes. La regla de no duplicación es explícita: si un término ya está declarado con la misma semántica, se referencia; el vocabulario de la superficie pública y del recorrido de integración es lo único que se acuña acá.

Rigen sin excepción las tres resoluciones de vocabulario del producto: **`Pendiente` va siempre calificado**, «pieza» en su referente del dominio va desnuda y en su referente de artefacto desplegable va calificada, y **la palabra «proyecto» a secas no se usa**.

### 1.3 `GeometriaFactory-Application`

Acá se declaran únicamente los términos que **esta** categoría acuña para **este** proyecto de código, y que aparecen en más de uno de sus artefactos. Todo lo demás se **referencia** en §4:

- `00-Contexto/Vision-Producto.md` §9 es el **glosario raíz de la cadena**.
- [`../02-Especificacion-Funcional/Glosario-Funcional.md`](../02-Especificacion-Funcional/Glosario-Funcional.md) declara lo que la categoría 02 acuña para este proyecto de código, incluidos los cuatro puertos, las dos verificaciones, el motivo, el doble y la unidad de trabajo.
- El glosario funcional de `GeometriaFactory-Domain` declara el vocabulario de la capa de la que este proyecto de código depende.

Ninguna entrada de §2 pisa a ninguna de las tres fuentes. La regla de no duplicación es explícita: si un término ya está declarado con la misma semántica, se referencia; lo único que se acuña acá es el vocabulario de la **superficie pública vista por quien interviene** y del **recorrido de integración**.

Rigen sin excepción las resoluciones de vocabulario del producto: **`Pendiente` va siempre calificado** —salvo en las enumeraciones del conjunto cerrado y en los identificadores literales de los motivos—, «pieza» va desnuda en su referente del dominio y calificada en su referente de artefacto desplegable, **«repositorio» a secas no se escribe**, «trabajo» no es «unidad de entrega», y **la palabra «proyecto» a secas no se usa**.

### 1.4 `GeometriaFactory-Infrastructure`

Acá se declaran únicamente los términos que **esta** categoría acuña para **este** proyecto de código, y que aparecen en más de uno de sus artefactos. Todo lo demás se **referencia** en §4:

- `00-Contexto/Vision-Producto.md` §9 es el **glosario raíz de la cadena**.
- [`../02-Especificacion-Funcional/Glosario-Funcional.md`](../02-Especificacion-Funcional/Glosario-Funcional.md) declara lo que la categoría 02 acuña para este proyecto de código, incluidas las cuatro trampas del formato, la lectura tolerante, el operador estricto, la terminación degradada y el arranque detenido.
- Los glosarios de `GeometriaFactory-Domain` y de `GeometriaFactory-Application` declaran el vocabulario de las dos capas de las que este proyecto de código depende.

Ninguna entrada de §2 pisa a ninguna de esas fuentes. Lo único que se acuña acá es el vocabulario de **quien interviene sobre este proyecto de código** y del **recorrido de implementación y de despliegue**.

Rigen sin excepción las resoluciones de vocabulario del producto: **`Pendiente` va siempre calificado** —salvo en las enumeraciones del conjunto cerrado y en los identificadores literales de los códigos—, «pieza» va desnuda en su referente del dominio y calificada en su referente de artefacto desplegable, **«repositorio» a secas no se escribe**, **«derivado» a secas designa la geometría**, «trabajo» no es «unidad de entrega», y **la palabra «proyecto» a secas no se usa**.

## 2. Términos que esta categoría acuña

### 2.1 `GeometriaFactory-Api`

| Término canónico | Definición operativa | Artefactos de 03 donde aparece | Sinónimos y alias |
| --- | --- | --- | --- |
| Superficie pública de la pieza de datos | El conjunto de lo que este proyecto de código existe hacia afuera. **Son sus quince puntos de acceso y nada más**: no lo referencia nadie por compilación | Los tres | «La superficie» cuando el proyecto de código está nombrado |
| **Consumidor de la superficie** | El rol de intervención de quien escribe el cliente que consume estos puntos de acceso. **Existe acá y no en las capas de adentro**, y es a quien le habla el catálogo entero | `DX-Developer-Experience.md`, `DX-Error-Messages.md` | «El consumidor» cuando el rol ya está nombrado. Acá lo encarna quien escribe el cliente tipado de la pieza pública. Ver §3.2 |
| Implementador de la superficie | El rol de intervención de quien agrega o cambia un punto de acceso | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| Mantenedor de la capa | El rol de intervención de quien sostiene este proyecto de código y vuelve sobre él sin el contexto de la etapa en que lo escribió | `DX-Developer-Experience.md` | — |
| Operador del despliegue | El rol de intervención de quien arranca el contenedor del servicio **a mano**. Acá lo encarna el docente | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | «El operador» cuando el rol ya está nombrado |
| Rol de intervención | Quién interviene sobre este proyecto de código, como tipo. **No es la persona objetivo del producto**, que es el alumno o el administrador | `DX-Developer-Experience.md` | El término de la variante DX |
| **Entrada del catálogo** | Cada una de las **18** situaciones en las que esta superficie responde con un fallo: **16** códigos del contrato con destino más **2** respuestas sin código | `DX-Error-Messages.md` | **No se dice «error»** para este referente: ver §3.1 |
| **Familia empobrecida** | Cada uno de los **tres** conjuntos de respuestas que dicen **menos de lo que el servicio sabe**, a propósito: credenciales inválidas, recurso que no se ve y correo ya registrado | `DX-Error-Messages.md`, `Guia-Onboarding-Developer.md` | «Respuesta empobrecida». **No se dice «respuesta vaga»**: la vaguedad sería un defecto y esto es una decisión |
| **Qué hace el consumidor** | La cuarta columna del catálogo, y la que decide si sirve: una de **cuatro** acciones —corregir y reintentar, derivar, mostrar, o pasar a estado degradado— | `DX-Error-Messages.md` | «Acción del consumidor». Es el equivalente del diagnóstico accionable de las capas de adentro |
| **Lo que no falla** | Cada uno de los **tres** defectos que dejan el sistema funcionando y equivocado: el punto fuera de la guardia, la respuesta que distingue lo ajeno de lo inexistente y el texto normalizado en el borde. Tienen métrica propia, con objetivo **cero** | `Guia-Onboarding-Developer.md` §6, `DX-Developer-Experience.md` §1.4 | «Las tres cosas que no fallan». **No se dice «bug»** |
| Detección por recuento | La forma de encontrar el defecto que no falla cuando lo que está mal es una **ausencia**: se cuentan los puntos guardados contra los puntos que exigen acceso | `Guia-Onboarding-Developer.md`, `DX-Developer-Experience.md` | — |
| Detección por comparación | La forma de encontrarlo cuando lo que está mal es una **diferencia** entre dos respuestas que deberían ser idénticas | `Guia-Onboarding-Developer.md`, `DX-Error-Messages.md` | — |
| Tramo de onboarding | Cada uno de los tres cortes del recorrido —5 minutos, 30 minutos, 1 hora—, cada uno con un objetivo **verificable** | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| Quick-start | La secuencia mínima y reproducible que produce el primer resultado exitoso. Ocurre entera **dentro del entorno de desarrollo contenido** | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| Primer resultado exitoso | Acá, **la colección de peticiones corriendo entera contra el servicio real**, sin pantalla, sin circuito y sin visor | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| TTFS | *Time-to-first-success*: tiempo desde abrir el repositorio de código hasta el primer resultado exitoso | `DX-Developer-Experience.md` | — |
| TTFV | *Time-to-first-value*: tiempo hasta haber corrido la colección y saber por qué los ocho escenarios responden con éxito | `DX-Developer-Experience.md` | — |
| Modo de documentación | Cada uno de los cuatro modos de Diátaxis, con su ubicación declarada en un artefacto concreto de la cadena | `DX-Developer-Experience.md` | «Modo Diátaxis» |

### 2.2 `GeometriaFactory-Domain`

| Término canónico | Definición operativa | Artefactos de 03 donde aparece | Sinónimos y alias |
| --- | --- | --- | --- |
| Superficie pública del dominio | El conjunto de lo que un consumidor invoca de este proyecto de código: la construcción y la transición de entidades, con sus guardas. **No es una API de servicio**: no expone protocolo, no cruza frontera de proceso y no se publica en ningún feed | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md`, `DX-Error-Messages.md` | «Superficie pública» a secas cuando el proyecto de código está nombrado en la misma oración |
| Guarda | Verificación que una entidad del dominio hace sobre sí misma y que la lleva a **negarse a entrar en un estado prohibido**. Es la unidad de la superficie pública: lo que se documenta, lo que se prueba y lo que produce un código de condición | Los tres | — |
| Condición de error del dominio | Cada una de las 42 situaciones catalogadas en las que una guarda se niega. Se identifica por un **código**, no por un texto. No es una observación y no es un comentario: ver §3.1 | Los tres | «Código de condición», «código de rechazo» en los artefactos de 02. Se dice «condición de error» cuando se habla de la situación y «código» cuando se habla del identificador |
| Rechazo | Forma de terminación en la que el dominio se niega a la operación: no construye la entidad, o la deja exactamente como estaba, y no queda efecto parcial ni estado intermedio | `DX-Error-Messages.md`, `Guia-Onboarding-Developer.md` | «Terminación controlada» |
| Motivo de resultado | Forma de terminación en la que la operación es una consulta que **siempre devuelve un resultado**, y el código es la razón por la que ese resultado es «no admisible» o «no procede». No es una excepción de programa y no modifica nada | `DX-Error-Messages.md`, `Guia-Onboarding-Developer.md` | «Motivo» cuando el resultado ya está nombrado en la misma oración |
| Diagnóstico accionable | La tercera parte obligatoria de toda entrada del catálogo: **qué hacer del lado del consumidor**. Existe en esa forma porque el dominio no consulta, no reintenta y no corrige el dato | `DX-Error-Messages.md`, `DX-Developer-Experience.md` | «Acción sugerida», que es el nombre de la columna |
| Categoría de error | Cada uno de los cuatro grupos en que el catálogo ordena las condiciones —entrada inválida, recurso ausente, conflicto de estado, conflicto de facultad—, más las dos declaradas vacías con su motivo | `DX-Error-Messages.md`, `DX-Developer-Experience.md` | «Taxonomía» para el conjunto |
| Rol de intervención | Quién interviene sobre este proyecto de código, como tipo: mantenedor, integrador de capa u operador. **No es la persona objetivo del producto**, que es el alumno o el administrador | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | El término de la variante DX; en la variante UX/UI el término equivalente es «audiencia» |
| Mantenedor del dominio | El rol de intervención principal acá: quien sostiene este proyecto de código y vuelve sobre él sin el contexto de la etapa en que lo escribió. Acá lo encarnan una persona y un agente de IA | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| Integrador de capa | El rol de intervención que escribe `GeometriaFactory-Application` o `GeometriaFactory-Infrastructure` contra esta superficie pública. **No hay integradores externos**: los dos consumidores son proyectos de código del mismo producto | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | Precisa a «consumidor de la biblioteca», que `Glosario-Funcional.md` §2 ya declara, agregando quién lo escribe |
| Tramo de onboarding | Cada uno de los tres cortes del recorrido de aprendizaje —5 minutos, 30 minutos, 1 hora—, cada uno con un objetivo **verificable**: algo que se ejecuta o se responde, no una lectura declarada como hecha | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| Quick-start | La secuencia mínima y reproducible de pasos que produce el primer resultado exitoso. Ocurre entera **dentro del entorno de desarrollo contenido**: ningún paso asume herramientas en el host | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| Primer resultado exitoso | Acá, la batería de pruebas de dominio en verde en menos de 10 segundos. Es el hito que cierra el tramo de 5 minutos y el que mide el TTFS | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| TTFS | *Time-to-first-success*: tiempo desde abrir el repositorio hasta el primer resultado exitoso | `DX-Developer-Experience.md` | — |
| TTFV | *Time-to-first-value*: tiempo hasta haber visto una guarda negándose y saber ubicar la regla o el invariante que la sostiene | `DX-Developer-Experience.md` | — |
| Modo de documentación | Cada uno de los cuatro modos de Diátaxis —tutorial, how-to, reference, explanation— con su ubicación declarada en un artefacto concreto de la cadena | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | «Modo Diátaxis» |

### 2.3 `GeometriaFactory-Application`

| Término canónico | Definición operativa | Artefactos de 03 donde aparece | Sinónimos y alias |
| --- | --- | --- | --- |
| Superficie pública de la capa de aplicación | El conjunto de lo que se invoca y de lo que se implementa contra este proyecto de código. Tiene **dos caras que miran para lados opuestos**: los casos de uso, que un consumidor invoca, y los puertos, que esta capa declara y otra implementa | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md`, `DX-Error-Messages.md` | «Superficie pública» a secas cuando el proyecto de código está nombrado en la misma oración |
| Inversión de dependencias | El rasgo que gobierna la capa: acá se declara **qué** hace falta y otra capa provee el **cómo**. Es lo que permite ejercer un caso de uso entero con dobles, sin base de datos ni frontera de proceso | Los tres | «La dependencia se invierte», que es la forma que usa `Especificacion-Funcional.md` §1 |
| Condición de error del caso de uso | Cada una de las **36** situaciones catalogadas en las que una comprobación se niega. Se identifica por un **motivo**. No es una observación y no es un comentario: ver §3.1 | Los tres | «Condición de error» cuando el proyecto de código está nombrado. El identificador se llama **motivo**, que `Glosario-Funcional.md` §2 ya declara |
| Negativa por pertenencia | La condición que se produce cuando el trabajo pedido no es del alumno solicitante. **Oculta la existencia del recurso**: el trabajo ajeno y el identificador inexistente comparten motivo por diseño | `DX-Error-Messages.md`, `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | Nombra el resultado de la «verificación de pertenencia» que `Glosario-Funcional.md` §2 declara |
| Negativa por facultad | La condición que se produce cuando quien pide una operación reservada no tiene el papel `Administrador`. **No oculta nada**: no hay recurso ajeno cuya existencia proteger | Los tres | Nombra el resultado de la «verificación de facultad» de `Glosario-Funcional.md` §2 |
| Negativa por alcance | La condición que se produce cuando el trabajo pedido está en `Borrador` y no forma parte del flujo de trabajo del administrador. **Tampoco oculta la existencia**: expresa que está fuera de su alcance | Los tres | Nombra el resultado de la comprobación de «alcance del administrador» |
| Traducción prohibida | Cada una de las cuatro correspondencias entre motivo y respuesta que ninguna capa aguas abajo puede establecer, empezando por traducir la negativa por pertenencia a «no autorizado». Tiene métrica propia, con objetivo **cero** | `DX-Error-Messages.md`, `DX-Developer-Experience.md` | — |
| Frontera de autorización | El límite declarado entre lo que esta capa hace —decidir quién puede hacer qué sobre qué recurso— y lo que no hace: comparar contraseñas, derivarlas, emitir accesos, sostener sesiones o autenticar la petición. **Esta capa no autentica: autoriza** | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | «La frontera» cuando ya está nombrada en la misma sección |
| Diagnóstico accionable | La tercera parte obligatoria de toda entrada del catálogo: qué hacer al respecto. Acá tiene **dos destinatarios**, el consumidor y el adaptador del puerto, según de dónde nazca la negativa | `DX-Error-Messages.md`, `DX-Developer-Experience.md` | «Acción sugerida», que es el nombre de la columna |
| Categoría de error | Cada uno de los siete grupos en que el catálogo ordena las condiciones: entrada inválida, recurso ausente, conflicto de estado, conflicto de facultad, conflicto de alcance, error transitorio y error interno | `DX-Error-Messages.md`, `DX-Developer-Experience.md` | «Taxonomía» para el conjunto |
| Forma de terminación | Dimensión ortogonal a la categoría, que dice qué le queda por hacer al consumidor: **negativa sin escritura**, **motivo de resultado** o **terminación degradada** | `DX-Error-Messages.md` | — |
| Terminación degradada | La forma de terminación de una sola condición, `INTERPRETACION_NO_DISPONIBLE`: la operación no se completó por una causa que no depende del pedido, y el caso de uso lo declara en vez de fingir un resultado | `DX-Error-Messages.md`, `Guia-Onboarding-Developer.md` | «Estado degradado», que es la forma que usa CU-04005 §6 |
| Condición agregada | Condición de esta capa que **reúne varios rechazos distintos del dominio** bajo un solo motivo, porque ninguno de ellos es un resultado que el alumno deba ver. Son dos: la del conjunto de piezas y la del conjunto de observaciones. El motivo fino queda del lado del dominio | `DX-Error-Messages.md`, `Guia-Onboarding-Developer.md` | — |
| Rechazo inalcanzable por construcción | Rechazo que el dominio declara y que **esta capa no puede provocar**, porque su propio flujo resuelve antes la condición que lo dispararía. No es una condición del catálogo y su ausencia no es un olvido: está declarado | `DX-Error-Messages.md`, `Guia-Onboarding-Developer.md` | Se distingue del **rechazo equivalente**, que sí puede ocurrir pero esta capa nombra con un motivo propio, y del **agregado**, que es la fila anterior |
| Adaptador de puerto | La implementación concreta de un puerto, que vive en `GeometriaFactory-Infrastructure`. Se nombra así cuando hay que decir de qué lado se corrige un defecto | `DX-Error-Messages.md`, `Guia-Onboarding-Developer.md` | «La implementación del puerto». No se dice «el puerto» a secas cuando el sujeto es la implementación |
| Rol de intervención | Quién interviene sobre este proyecto de código, como tipo: mantenedor de la capa, integrador por casos de uso, implementador de puertos u operador. **No es la persona objetivo del producto**, que es el alumno o el administrador | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | El término de la variante DX; en la variante UX/UI el término equivalente es «audiencia» |
| Integrador por casos de uso | El rol de intervención que escribe `GeometriaFactory-Api` contra los casos de uso de esta capa. **No hay integradores externos** | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | Precisa al «consumidor de los casos de uso» que `Glosario-Funcional.md` §2 declara, agregando quién lo escribe |
| Implementador de puertos | El rol de intervención que escribe `GeometriaFactory-Infrastructure` contra los puertos que esta capa declara. Es el otro consumidor, y mira la superficie desde el lado opuesto | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| Mantenedor de la capa | El rol de intervención de quien sostiene este proyecto de código y vuelve sobre él sin el contexto de la etapa en que lo escribió. Acá lo encarnan una persona y un agente de IA | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| Tramo de onboarding | Cada uno de los tres cortes del recorrido de aprendizaje —5 minutos, 30 minutos, 1 hora—, cada uno con un objetivo **verificable**: algo que se ejecuta o se responde, no una lectura declarada como hecha | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| Quick-start | La secuencia mínima y reproducible de pasos que produce el primer resultado exitoso. Ocurre entera **dentro del entorno de desarrollo contenido**: ningún paso asume herramientas en el host | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| Primer resultado exitoso | Acá, la batería de pruebas de la capa de aplicación en verde **sin haber preparado nada externo**. Es el hito que cierra el tramo de 5 minutos y el que mide el TTFS | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| TTFS | *Time-to-first-success*: tiempo desde abrir el repositorio hasta el primer resultado exitoso | `DX-Developer-Experience.md` | — |
| TTFV | *Time-to-first-value*: tiempo hasta haber ejercitado un caso de uso entero con dobles y saber nombrar los cuatro puertos | `DX-Developer-Experience.md` | — |
| Modo de documentación | Cada uno de los cuatro modos de Diátaxis —tutorial, how-to, reference, explanation— con su ubicación declarada en un artefacto concreto de la cadena | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | «Modo Diátaxis» |

### 2.4 `GeometriaFactory-Infrastructure`

| Término canónico | Definición operativa | Artefactos de 03 donde aparece | Sinónimos y alias |
| --- | --- | --- | --- |
| Superficie pública de la capa de infraestructura | El conjunto de lo que se implementa contra este proyecto de código. **No es propia**: tiene la forma de los contratos que otra capa declaró, más dos mecanismos y una responsabilidad de arranque | Los tres | «Superficie pública» a secas cuando el proyecto de código está nombrado |
| Condición de error del adaptador | Cada una de las **17** situaciones catalogadas en las que un contrato de esta capa no puede hacer lo que se le pidió. Se identifica por un **código**. **No es un resultado**: ver §3.1 | Los tres | «Condición de error» cuando el proyecto de código está nombrado |
| Resultado que no es una condición | Cada una de las **siete** situaciones que parecen un fallo y son el funcionamiento normal del producto: el error de validación, el texto ilegible, cero advertencias, nada encontrado, el conjunto vacío, la credencial que no coincide y el acceso vencido | `DX-Error-Messages.md`, `Guia-Onboarding-Developer.md` | — |
| **Atajo prohibido** | Cada una de las tres salidas que un implementador apurado tomaría cuando algo del mundo no responde, y que **no fallan**: dejan el sistema funcionando y equivocado. Tienen métrica propia, con objetivo **cero** | `DX-Error-Messages.md`, `Guia-Onboarding-Developer.md`, `DX-Developer-Experience.md` | «Los tres atajos». **No se dice «mitigación»**: no hay nada que mitigar, hay algo que no se hace |
| Falla hacia el lado seguro | La propiedad que las condiciones de los atajos prohibidos sostienen: **cuando el mecanismo no puede cumplir su promesa, se detiene y lo dice; no la cumple a medias** | `DX-Error-Messages.md`, `Guia-Onboarding-Developer.md` | — |
| Defecto que no falla | El patrón que agrupa a los atajos prohibidos y a las tres reglas cuyo tramo principal vive acá: **se rompen produciendo algo válido**, de modo que ninguna prueba los encuentra si no está escrita a propósito | `Guia-Onboarding-Developer.md` §7, `DX-Developer-Experience.md` §1.4 | — |
| Operador del despliegue | El rol de intervención de quien arranca el contenedor de la pieza de datos **a mano**. **Existe acá y no en las capas de adentro**, y es a quien le hablan seis de las diecisiete condiciones | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | «El operador» cuando el rol ya está nombrado. Acá lo encarna el docente |
| Implementador de adaptadores | El rol de intervención de quien escribe la implementación de un puerto que la capa de aplicación declaró | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| Mantenedor de la capa | El rol de intervención de quien sostiene este proyecto de código y vuelve sobre él sin el contexto de la etapa en que lo escribió. Acá lo encarnan una persona y un agente de IA | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| Rol de intervención | Quién interviene sobre este proyecto de código, como tipo. **No es la persona objetivo del producto**, que es el alumno o el administrador | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | El término de la variante DX |
| Diagnóstico accionable | La tercera parte obligatoria de toda entrada del catálogo: qué hacer al respecto. Acá dice además **de qué lado hacerlo**, porque la mitad de las condiciones se corrigen en el despliegue y no en el código | `DX-Error-Messages.md`, `DX-Developer-Experience.md` | «Acción sugerida», que es el nombre de la columna |
| Categoría de error | Cada uno de los siete grupos en que el catálogo ordena las condiciones. **Dos están vacías acá** —conflicto de facultad y conflicto de alcance— y su vacío es informativo | `DX-Error-Messages.md` | «Taxonomía» para el conjunto |
| Categoría vacía | Grupo de la taxonomía sin ninguna condición en este proyecto de código, **declarado con su motivo** en lugar de omitido | `DX-Error-Messages.md` §2.2 | — |
| Tramo de onboarding | Cada uno de los tres cortes del recorrido —5 minutos, 30 minutos, 1 hora—, cada uno con un objetivo **verificable** | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| Quick-start | La secuencia mínima y reproducible que produce el primer resultado exitoso. Ocurre entera **dentro del entorno de desarrollo contenido** | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| Primer resultado exitoso | Acá, **la batería del validador en verde sobre los textos reales de los escenarios**, sin almacén, sin red y sin secreto | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| TTFS | *Time-to-first-success*: tiempo desde abrir el repositorio de código hasta el primer resultado exitoso | `DX-Developer-Experience.md` | — |
| TTFV | *Time-to-first-value*: tiempo hasta haber corrido la batería obligatoria y saber qué prueba cada caso | `DX-Developer-Experience.md` | — |
| Modo de documentación | Cada uno de los cuatro modos de Diátaxis, con su ubicación declarada en un artefacto concreto de la cadena | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | «Modo Diátaxis» |

## 3. Términos con más de un referente

### 3.1 `GeometriaFactory-Api`

Los dos siguientes se verificaron contra el criterio de `Vocabulario-Rules.md` §9.1 y §9.2: en los dos, los sentidos aparecen en el **mismo contexto de lectura** —la sección— y por eso se desambiguan. Ninguno se declara acá por analogía con otro.

### 3.1 Error

Es la colisión central de esta sección, igual que en las capas hermanas, **y acá tiene un referente que ninguna de ellas tiene**: la clase de respuesta.

| Referente | Forma que corresponde | Evidencia de colisión |
| --- | --- | --- |
| Cada una de las 16 situaciones en las que esta superficie responde con un fallo | **«entrada del catálogo»**, o **«respuesta de fallo»**. La forma desnuda «error» **no se usa** para este referente | `DX-Error-Messages.md` §1.2 y §3 usan varios referentes en la misma sección |
| El defecto que impide interpretar el texto del alumno, que es **una de las dos especies de observación** | **«error de validación», siempre completo.** Es entidad del dominio, es un **resultado**, viaja en una respuesta exitosa y **no está en el catálogo** | `DX-Error-Messages.md` §1.2 pone los dos referentes en la misma tabla |
| La clase de fallo que el código de respuesta expresa | **«código de respuesta»**, o su número. **No se dice «error 500»**, se dice «respuesta `500`» | `Guia-Onboarding-Developer.md` §3.5 y §4 |
| Una falla no declarada del proyecto de código | **«defecto».** No es una entrada del catálogo: su lugar es una prueba que falla | `Guia-Onboarding-Developer.md` §6 |

Regla de uso, en una línea: **«error» a secas no se escribe en esta sección**. Las formas son «entrada del catálogo», «respuesta de fallo», «error de validación», «código de respuesta» y «defecto».

Y la distinción que completa el cuadro, heredada de las capas de adentro y todavía más importante acá: **el error de validación es lo que el alumno tiene que ver, y viaja en una respuesta exitosa.** Ninguna de las otras formas lo es.

### 3.2 Consumidor

| Referente | Forma que corresponde | Evidencia de colisión |
| --- | --- | --- |
| El rol de intervención de quien escribe el cliente de esta superficie | **«consumidor de la superficie»**, o «el consumidor» cuando el rol ya está nombrado en la misma sección | `DX-Developer-Experience.md` §1.1 y `DX-Error-Messages.md` §1.3 usan los dos referentes en secciones vecinas |
| El código que invoca una biblioteca, que es como lo usan las capas de adentro | **No se nombra «consumidor» a secas** en esta sección. Cuando hace falta, se dice **«la composición de raíz»**, que es el término de aquellas capas | Los dos aparecerían en las mismas tablas de trazabilidad |

### 3.3 Tres casos que no son polisemia y no se corrigen

Se declaran para que una revisión posterior no los levante como hallazgo, que es el falso positivo que `Vocabulario-Rules.md` §9.1 tipifica.

- **Los nombres de los códigos del contrato son identificadores literales** y no se califican, no se traducen y no se acentúan. La misma excepción alcanza a las enumeraciones del conjunto cerrado de estados.
- **Los números de código de respuesta se escriben como números** y no se traducen a prosa. «Responde `404`» es la forma; «responde no encontrado» describe la obligación, no la respuesta, y sólo se usa citando la regla que la exige.
- **«Guardia»** tiene un solo referente en esta sección —la comprobación de acceso, papel y marca— y no colisiona con nada. Aparece en singular porque **es una sola**, aunque gobierne once puntos.

### 3.2 `GeometriaFactory-Domain`

Los dos términos siguientes se verificaron contra el criterio de `Vocabulario-Rules.md` §9.1 y §9.2: en los dos, los sentidos aparecen en el **mismo contexto de lectura** —la sección— y por eso se desambiguan. Ningún término se declara acá por analogía con otro.

### 3.1 Error

Es la colisión central de esta sección y la que hay que dejar imposible de confundir: los tres referentes conviven en las mismas secciones de `DX-Error-Messages.md` y de `Guia-Onboarding-Developer.md`.

| Referente | Forma que corresponde | Evidencia de colisión |
| --- | --- | --- |
| La guarda que impide una operación ilegítima del consumidor. No se guarda en ninguna parte y no sobrevive a la invocación | **«condición de error del dominio»**, o «código» cuando se nombra el identificador. La forma desnuda «error» **no se usa** para este referente | `DX-Error-Messages.md` §1.2 y §3 hablan de los tres referentes en la misma sección: catalogan condiciones, y sus enunciados nombran observaciones de especie error de validación |
| El defecto que impide interpretar el texto del alumno como figuras, que es **una de las dos especies de observación** y que impide que el trabajo pase a estado `Pendiente` | **«error de validación», siempre completo.** Es entidad del dominio, no guarda | Declarado en `Vision-Producto.md` §9.1. `ERROR_SIN_UBICACION` y `ADVERTENCIA_SIN_LOS_DOS_VALORES` son condiciones **sobre** observaciones, y los dos sentidos aparecen en la misma fila del catálogo |
| Una falla no declarada del proyecto de código | **«defecto».** No es una condición del catálogo: su lugar es una prueba que falla | `DX-Error-Messages.md` §2.2 declara vacía la categoría «error interno» por este motivo |

Regla de uso, en una línea: **«error» a secas no se escribe en esta sección**. Las tres formas son «condición de error del dominio», «error de validación» y «defecto».

Y la distinción que completa el cuadro, aunque no sea polisemia de la palabra: **ninguno de los tres es el comentario** del administrador, que lo escribe una persona, hay a lo sumo uno por trabajo y no es una calificación (`Vision-Producto.md` §9.1).

### 3.2 Mensaje

| Referente | Forma que corresponde | Evidencia de colisión |
| --- | --- | --- |
| El enunciado canónico en lenguaje plano de una condición de error, que este catálogo declara como columna y que el dominio **no produce** | **«mensaje del catálogo»** cuando el otro referente está en la misma sección; «mensaje» a secas dentro de la columna de la tabla, donde el encabezado ya fija el referente | `DX-Error-Messages.md` §1.3 y §5 hablan de los dos referentes en la misma sección: la columna del catálogo y lo que la capa que expone compone |
| El texto que una persona lee, compuesto por la capa que expone y traducido a respuesta de protocolo | **«mensaje al usuario»** o «texto que compone el consumidor» | `DX-Error-Messages.md` §5 declara que la composición y la traducción no viven acá |

### 3.3 Dos casos que no son polisemia y no se corrigen

Se declaran para que una revisión posterior no los levante como hallazgo, que es exactamente el falso positivo que `Vocabulario-Rules.md` §9.1 tipifica y que su §10 declara defecto del informe y no del documento auditado.

- **«Observación»** tiene un solo referente y dos especies: es un término **superordinado**, no ambiguo. La relación con «advertencia» y «error de validación» es de hiperonimia, ya declarada en `Vision-Producto.md` §9.1 y en `Glosario-Funcional.md` §3.4. Lo que sí colisiona es «error», y está resuelto en §3.1.
- **Los nombres de los códigos son identificadores literales del contrato** y no se califican. `CUENTA_PENDIENTE` se escribe así aunque `Pendiente` lleve forma calificada obligatoria en prosa; calificar un identificador sería el falso positivo que `Glosario-Funcional.md` §3.3 ya declaró como excepción. La misma excepción alcanza a las enumeraciones del conjunto cerrado de estados y a las filas de las tablas de transición cuyo encabezado ya fija el referente.

### 3.3 `GeometriaFactory-Application`

Los dos términos siguientes se verificaron contra el criterio de `Vocabulario-Rules.md` §9.1 y §9.2: en los dos, los sentidos aparecen en el **mismo contexto de lectura** —la sección— y por eso se desambiguan. Ningún término se declara acá por analogía con otro.

### 3.1 Error

Es la colisión central de esta sección, y la misma que el proyecto de código hermano resolvió para su propia categoría 03. Acá los referentes son cuatro, porque esta capa suma uno que el dominio no tenía: el defecto del adaptador de un puerto.

| Referente | Forma que corresponde | Evidencia de colisión |
| --- | --- | --- |
| La comprobación que impide una operación ilegítima o imposible. No se guarda en ninguna parte y no sobrevive a la invocación | **«condición de error del caso de uso»**, o **«motivo»** cuando se nombra el identificador. La forma desnuda «error» **no se usa** para este referente | `DX-Error-Messages.md` §1.2 y §3 hablan de varios referentes en la misma sección: catalogan condiciones, y sus enunciados nombran observaciones de especie error de validación |
| El defecto que impide interpretar el texto del alumno como figuras, que es **una de las dos especies de observación** y que impide que el trabajo pase a estado `Pendiente` | **«error de validación», siempre completo.** Es entidad del dominio, no comprobación | Declarado en `Vision-Producto.md` §9.1. `OBSERVACION_MAL_FORMADA` es una condición **sobre** observaciones, y los dos sentidos aparecen en la misma fila del catálogo |
| Lo que un adaptador de puerto devolvió y el contrato no admite | **«defecto del adaptador»**, y la categoría del catálogo se llama «error interno» sólo como nombre de categoría, donde el encabezado ya fija el referente | `DX-Error-Messages.md` §2.2 y §3.5 hablan de los dos referentes en la misma sección |
| Una falla no declarada del proyecto de código | **«defecto».** No es una condición del catálogo: su lugar es una prueba que falla | `DX-Error-Messages.md` §2.2 lo declara al justificar la categoría de error interno |

Regla de uso, en una línea: **«error» a secas no se escribe en esta sección**. Las formas son «condición de error del caso de uso», «error de validación», «defecto del adaptador» y «defecto».

Y la distinción que completa el cuadro, aunque no sea polisemia de la palabra: **ninguno de los cuatro es el comentario** del administrador, que lo escribe una persona, hay a lo sumo uno por trabajo y no lleva nota ni escala (`Vision-Producto.md` §9.1).

### 3.2 Negativa

| Referente | Forma que corresponde | Evidencia de colisión |
| --- | --- | --- |
| El resultado de una de las **cuatro** comprobaciones de autorización: pertenencia, facultad, alcance o cambio de contraseña pendiente | **Siempre calificada**: «negativa por pertenencia», «negativa por facultad», «negativa por alcance», «negativa por cambio de contraseña pendiente». Es el uso que domina la sección | `DX-Error-Messages.md` §2.4 y `DX-Developer-Experience.md` §1.4 usan los dos referentes en la misma sección: enumeran las cuatro calificadas y hablan además de la negativa como forma de terminación |
| Cualquier terminación en la que el caso de uso no hace lo que se le pidió, incluidas las que nada tienen que ver con autorización | **«negativa sin escritura»** cuando se habla de la forma de terminación, que es donde el término aparece sin calificar por comprobación | `DX-Error-Messages.md` §2.3 declara la forma, y §2.4 declara las tres calificadas |

La forma desnuda «negativa» sólo se admite cuando la calificación aparece en la misma oración y el referente ya quedó fijado.

### 3.3 Tres casos que no son polisemia y no se corrigen

Se declaran para que una revisión posterior no los levante como hallazgo, que es exactamente el falso positivo que `Vocabulario-Rules.md` §9.1 tipifica.

- **«Observación»** tiene un solo referente y dos especies: es un término **superordinado**, no ambiguo. La relación con «advertencia» y «error de validación» es de hiperonimia, ya declarada en `Vision-Producto.md` §9.1 y en `Glosario-Funcional.md` §3.6. Lo que sí colisiona es «error», y está resuelto en §3.1. **El comentario del administrador no es una observación.**
- **Los nombres de los motivos son identificadores literales del contrato** y no se califican ni se traducen. `CUENTA_PENDIENTE` se escribe así aunque `Pendiente` lleve forma calificada obligatoria en prosa; calificar un identificador sería el falso positivo que `Glosario-Funcional.md` §3.3 ya declaró como excepción. La misma excepción alcanza a las enumeraciones del conjunto cerrado de estados.
- **«Puerto»** designa acá una sola cosa: el contrato que esta capa declara y otra implementa. No tiene relación con ningún sentido de infraestructura de red, que no aparece en ningún artefacto de esta sección. Los contextos son disjuntos y por eso no se califica; la resolución es idéntica a la que declara `Glosario-Funcional.md` §3.6 y se conserva.

### 3.4 `GeometriaFactory-Infrastructure`

Los dos términos siguientes se verificaron contra el criterio de `Vocabulario-Rules.md` §9.1 y §9.2: en los dos, los sentidos aparecen en el **mismo contexto de lectura** —la sección— y por eso se desambiguan. Ninguno se declara acá por analogía con otro.

### 3.1 Error

Es la colisión central de esta sección, igual que en las dos capas hermanas, **y acá tiene un referente más que ninguna de las dos**: el defecto del despliegue.

| Referente | Forma que corresponde | Evidencia de colisión |
| --- | --- | --- |
| La situación en la que un contrato de esta capa no puede hacer lo que se le pidió. No se guarda en ninguna parte | **«condición de error del adaptador»**, o **«código»** cuando se nombra el identificador. La forma desnuda «error» **no se usa** para este referente | `DX-Error-Messages.md` §1.2 y §3 hablan de varios referentes en la misma sección |
| El defecto que impide interpretar el texto del alumno, que es **una de las dos especies de observación** | **«error de validación», siempre completo.** Es entidad del dominio, es un **resultado**, y **no está en el catálogo** | Declarado en el glosario raíz. `DX-Error-Messages.md` §1.2 pone los dos referentes en la misma tabla |
| Lo que falta o no responde **en el despliegue**: el volumen sin montar, la clave sin proveer, el esquema divergente | **«defecto del despliegue»**, y las categorías del catálogo se llaman «error transitorio» y «error interno» sólo como nombre de categoría, donde el encabezado ya fija el referente | `DX-Developer-Experience.md` §1.1 y `Guia-Onboarding-Developer.md` §4 hablan de los tres referentes en la misma sección |
| Una falla no declarada del proyecto de código | **«defecto».** No es una condición del catálogo: su lugar es una prueba que falla | `DX-Error-Messages.md` §2.1 |

Regla de uso, en una línea: **«error» a secas no se escribe en esta sección**. Las formas son «condición de error del adaptador», «error de validación», «defecto del despliegue» y «defecto».

Y la distinción que completa el cuadro: **el error de validación es lo que el alumno tiene que ver, y ninguna de las otras tres lo es.** Confundirlos produce un producto que le grita al alumno por hacer bien su trabajo.

### 3.2 Atajo

| Referente | Forma que corresponde | Evidencia de colisión |
| --- | --- | --- |
| Cada una de las tres salidas prohibidas de §2, que dejan el sistema funcionando y equivocado | **«atajo prohibido»**, o «el atajo» cuando la condición ya está nombrada en la misma oración | `Guia-Onboarding-Developer.md` §7 y `DX-Error-Messages.md` §2.4 usan los dos referentes en la misma sección |
| Un camino de lectura abreviado dentro de la documentación | **No se nombra «atajo».** Se dice «orden de lectura» o «punto de entrada» | Los dos aparecerían en las mismas secciones de orden de lectura, y por eso el segundo se evita |

### 3.3 Tres casos que no son polisemia y no se corrigen

Se declaran para que una revisión posterior no los levante como hallazgo, que es exactamente el falso positivo que `Vocabulario-Rules.md` §9.1 tipifica.

- **«Observación»** tiene un solo referente y dos especies: es un término **superordinado**, no ambiguo. Lo que sí colisiona es «error», y está resuelto en §3.1. **El comentario del administrador no es una observación.**
- **Los nombres de los códigos son identificadores literales del contrato** y no se califican ni se traducen. Se escriben en mayúsculas y sin acentos, y la misma excepción alcanza a las enumeraciones del conjunto cerrado de estados.
- **«Migración»** aparece en las fuentes técnicas del producto con el sentido de transformación de esquema, y esta sección usa **«transformación de esquema»** en prosa. **No es una polisemia a corregir**: es una elección de forma de la categoría 02, que admite «migración» cuando el sujeto es la herramienta, y los contextos no se cruzan.

## 4. Términos referenciados y no redefinidos

### 4.1 `GeometriaFactory-Api`

### 4.1 Del glosario raíz de 00

Trabajo; Pieza; Componente; Observación; Advertencia; Error de validación; Estado del trabajo con sus cuatro valores y la terminalidad de dos de ellos; Enviar como única acción de guardado; Aprobar / Rechazar; Comentario; Valor declarado / valor derivado; Laboratorio; Actividad 1; Punto de control; `Pendiente` con su forma calificada obligatoria; Etapa; Capacidad.

### 4.2 Del glosario funcional de 02 de este proyecto de código

Todos declarados en [`../02-Especificacion-Funcional/Glosario-Funcional.md`](../02-Especificacion-Funcional/Glosario-Funcional.md) §2 y §3. **Esta sección los usa sin excepción con la misma semántica y no redefine ninguno**: punto de acceso, superficie HTTP, código de respuesta, código del contrato, las dos traducciones, guardia de admisión, papel exigido, composición de raíz, arranque detenido, ruta propuesta, colección de peticiones, señal que no es un fallo y hueco del conjunto cerrado; más las tres polisemias de «acceso», «código» y «punto».

### 4.3 De los glosarios de los proyectos de código vecinos

| Término | Qué designa, en una línea | Dónde está declarado |
| --- | --- | --- |
| Papel, Estado de cuenta, Credencial derivada, Marca de cambio de contraseña pendiente | El vocabulario de la cuenta | `GeometriaFactory-Domain` |
| Desenlace, Terminalidad, Alcance del administrador | Las nociones que gobiernan el cierre del circuito | `GeometriaFactory-Domain` |
| Puerto, Verificación de pertenencia, Verificación de facultad, Unidad de trabajo, Motivo, Doble | El vocabulario de la orquestación. **Las dos verificaciones no se hacen acá** | `GeometriaFactory-Application` |
| Contrato de uso, Tipo de transferencia, Conjunto cerrado de códigos, Señal declarada | El vocabulario de lo que cruza la frontera | `GeometriaFactory-Contracts` |
| Adaptador, Almacén, Transformación de esquema, Terminación degradada, Arranque detenido | El vocabulario de la capa que toca el mundo | `GeometriaFactory-Infrastructure` |
| Trampa del formato, Lectura tolerante, Operador estricto, Contraseña provisoria, Clave de firma | El vocabulario del dato del alumno y de los secretos | `GeometriaFactory-Infrastructure` |
| **Atajo prohibido**, **Falla hacia el lado seguro**, **Defecto que no falla** | Los términos con los que la sección 03 de la capa que toca el mundo nombra la misma familia de defectos que acá se llama **«lo que no falla»** | `GeometriaFactory-Infrastructure` 03 |

**Sobre el último grupo, una precisión para que no se lea como una polisemia nueva.** Aquella sección y ésta nombran **la misma clase de defecto** —el que se rompe produciendo algo válido— y la nombran distinto porque **el sujeto es distinto**: allá son **atajos** que alguien toma deliberadamente cuando el mundo no responde, y acá son en dos de los tres casos **descuidos** que nadie decide, como olvidarse de poner un punto nuevo bajo la guardia. Se referencia el término de aquella sección y no se lo redefine.

Los seis términos normativos del framework —producto, unidad de entrega, módulo, solución de código, proyecto de código y proyecto— conservan el sentido de `Vocabulario-Rules.md` §2 y no se redefinen acá. En particular, **este proyecto de código sí es una unidad de entrega**, y es una de las dos del producto.

### 4.2 `GeometriaFactory-Domain`

Los siguientes términos aparecen en los artefactos de esta sección con la misma semántica con la que ya están declarados aguas arriba. Se referencian y no se redefinen; ninguna entrada de §2 los pisa.

| Término | Dónde está declarado |
| --- | --- |
| Trabajo, Pieza, Componente, Observación, Advertencia, Error de validación | `Vision-Producto.md` §9.1 |
| Estado del trabajo, con sus cuatro valores y la terminalidad de dos de ellos | `Vision-Producto.md` §9.1 |
| Enviar, Aprobar / Rechazar, Comentario | `Vision-Producto.md` §9.1 |
| Valor declarado / valor derivado | `Vision-Producto.md` §9.1 |
| Laboratorio, Actividad 1, `Describir()`, Tapa, Rectángulo desarrollado, Coma final, Fallo silencioso | `Vision-Producto.md` §9.1 |
| Punto de control, Hito interno / hito demostrable | `Vision-Producto.md` §9.1 |
| `Pendiente`, forma calificada obligatoria | `Vision-Producto.md` §9.2 |
| Pieza, en su segundo referente, siempre calificado | `Vision-Producto.md` §9.2 |
| Etapa, Puerta técnica, Capacidad | `Vision-Producto.md` §9.2 |
| Alumno, Papel, Estado de cuenta, Credencial derivada | `Glosario-Funcional.md` §2 |
| **Camino de alta**, con sus dos referentes: el auto-registro del alumno, que nace `Pendiente` y sin credencial, y la configuración del administrador, que nace `Habilitado` y con credencial | `Glosario-Funcional.md` §2 |
| Admisibilidad de la cuenta, Baja de la cuenta | `Glosario-Funcional.md` §2 |
| Texto original, Posición de pieza, Familia plana o volumétrica | `Glosario-Funcional.md` §2 |
| Especie de observación, Desenlace, Alcance del administrador | `Glosario-Funcional.md` §2 |
| Consumidor de la biblioteca, Sujeto de la regla | `Glosario-Funcional.md` §2 |
| Invariante, en el sentido de condición que no puede romperse nunca | `Definicion-Modelo-De-Dominio.md` §4 |

Los seis términos normativos del framework —producto, unidad de entrega, módulo, solución de código, proyecto de código y proyecto— conservan el sentido de `Vocabulario-Rules.md` §2 y no se redefinen acá. En particular, **«trabajo» no es «unidad de entrega»**: las unidades de entrega de este producto son las dos piezas desplegables, y el trabajo del alumno es un registro de datos que no se despliega.

### 4.3 `GeometriaFactory-Application`

Los siguientes términos aparecen en los artefactos de esta sección con la misma semántica con la que ya están declarados aguas arriba. Se referencian y no se redefinen; ninguna entrada de §2 los pisa.

### 4.1 Del glosario raíz de 00

| Término | Dónde está declarado |
| --- | --- |
| Trabajo, Pieza, Componente, Observación, Advertencia, Error de validación | `Vision-Producto.md` §9.1 |
| Estado del trabajo, con sus cuatro valores y la terminalidad de dos de ellos | `Vision-Producto.md` §9.1 |
| Enviar, como única acción de guardado; Aprobar / Rechazar; Comentario | `Vision-Producto.md` §9.1 |
| Valor declarado / valor derivado | `Vision-Producto.md` §9.1 |
| Laboratorio, Actividad 1, Punto de control | `Vision-Producto.md` §9.1 |
| `Pendiente`, forma calificada obligatoria | `Vision-Producto.md` §9.2 |
| Pieza, en su segundo referente, siempre calificado | `Vision-Producto.md` §9.2 |
| Etapa, Capacidad | `Vision-Producto.md` §9.2 |

### 4.2 Del glosario funcional de 02 de este proyecto de código

Todos declarados en [`../02-Especificacion-Funcional/Glosario-Funcional.md`](../02-Especificacion-Funcional/Glosario-Funcional.md) §2 y §3. **Esta sección los usa sin excepción con la misma semántica y no redefine ninguno**, ni siquiera cuando le agrega un nombre para el resultado de una comprobación, como en las tres negativas de §2.

| Término | Qué designa, en una línea |
| --- | --- |
| Puerto | Contrato que esta capa declara y que otra capa implementa |
| Puerto de repositorio de trabajos | Por donde el caso de uso recupera, consulta acotado, materializa y retira |
| Puerto de repositorio de cuentas | Por donde recupera una cuenta, pregunta por un correo y materializa. **Su identificador es punto abierto** |
| Puerto de validación de figuras | Por donde entrega el texto original y recibe piezas y observaciones |
| Puerto de reloj del sistema | Por donde obtiene el sello, para que los sellos de alta, de modificación y de desenlace sean verificables en prueba |
| Consumidor de los casos de uso | El proyecto de código que invoca la superficie pública: `GeometriaFactory-Api` |
| Verificación de pertenencia | La comprobación de que el trabajo pedido es del alumno solicitante |
| Verificación de facultad | La comprobación de que quien pide una operación reservada tiene el papel `Administrador` |
| Alcance de consulta | El recorte que el caso de uso traslada al puerto antes de pedir |
| Unidad de trabajo | El tramo dentro del cual las escrituras de un caso de uso ocurren enteras o no ocurren |
| Motivo | El valor de la enumeración cerrada con la que un caso de uso explica por qué una operación no procede |
| Doble | Implementación de prueba de un puerto, que hace ejercitable un caso de uso entero sin base de datos |
| Camino de alta | Cada una de las dos vías por las que nace una cuenta, con reglas opuestas: el auto-registro del alumno y la configuración del administrador |
| Metadato de orquestación | Dato que esta capa aporta al materializar y que el modelo del dominio no declara como atributo: los sellos de alta, de modificación y de desenlace |
| Cantidad de figuras del conjunto raíz | Cuántas figuras trae el texto interpretado, incluidas las que no se pudieron reconstruir; es el rango de posiciones válidas del trabajo. `Glosario-Funcional.md` §4.2 lo referencia del modelo del dominio, y esta sección hace lo mismo |
| Repositorio, con sus dos referentes y la forma calificada obligatoria | `Glosario-Funcional.md` §3.1 |
| Trabajo y la forma compuesta «unidad de trabajo» | `Glosario-Funcional.md` §3.5 |
| Rol, con «papel» para el atributo de la cuenta | `Glosario-Funcional.md` §3.4 |

### 4.3 Del glosario funcional de GeometriaFactory-Domain

| Término | Qué designa, en una línea |
| --- | --- |
| Alumno, Papel, Estado de cuenta, Credencial derivada | El vocabulario de la cuenta |
| Admisibilidad de la cuenta, Baja de la cuenta | Las dos nociones que CU-04003 y CU-04002 orquestan |
| Texto original, Posición de pieza, Familia plana o volumétrica | El vocabulario del trabajo interpretado |
| Especie de observación, Desenlace, Alcance del administrador | Las tres nociones que gobiernan el cierre del circuito |
| Sujeto de la regla | La persona sobre la que recae una regla, que no es actor de ningún caso de uso |

Los seis términos normativos del framework —producto, unidad de entrega, módulo, solución de código, proyecto de código y proyecto— conservan el sentido de `Vocabulario-Rules.md` §2 y no se redefinen acá. En particular, **«trabajo» no es «unidad de entrega»**: las unidades de entrega de este producto son las dos piezas desplegables, y el trabajo del alumno es un registro de datos que no se despliega.

### 4.4 `GeometriaFactory-Infrastructure`

Los siguientes aparecen en los artefactos de esta sección con la misma semántica con la que ya están declarados aguas arriba. Se referencian y no se redefinen; ninguna entrada de §2 los pisa.

### 4.1 Del glosario raíz de 00

Trabajo; Pieza en sus dos referentes, el segundo siempre calificado; Componente; Observación; Advertencia; Error de validación; Estado del trabajo con sus cuatro valores y la terminalidad de dos de ellos; Enviar como única acción de guardado; Aprobar / Rechazar; Comentario; Valor declarado / valor derivado; Laboratorio; Actividad 1; Punto de control; `Pendiente` con su forma calificada obligatoria; Etapa; Capacidad.

### 4.2 Del glosario funcional de 02 de este proyecto de código

Todos declarados en [`../02-Especificacion-Funcional/Glosario-Funcional.md`](../02-Especificacion-Funcional/Glosario-Funcional.md) §2 y §3. **Esta sección los usa sin excepción con la misma semántica y no redefine ninguno.**

| Término | Qué designa, en una línea |
| --- | --- |
| Adaptador | La implementación concreta de un puerto, que vive en este proyecto de código |
| Almacén | El archivo único donde el producto guarda lo que sobrevive al apagado del proceso |
| Trampa del formato | Cada uno de los cuatro rasgos del texto real del alumno que rompen a un lector ingenuo |
| Lectura tolerante | Admitir comas finales, omitir comentarios y aceptar las claves sinónimas |
| Existencia contra veracidad | Comprobar que el campo esté, no que su valor tenga sentido geométrico |
| Operador estricto | Advertir cuando la diferencia es **mayor** que la tolerancia, y no cuando es mayor o igual |
| Posición reservada | La posición de una figura que no se pudo reconstruir, que no se compacta |
| Cantidad de figuras del conjunto raíz | Cuántas trae el texto interpretado; **no es derivable de las piezas adoptadas** |
| Contraseña provisoria | El valor que este proyecto de código **produce** cuando el administrador resetea |
| Valor derivado de la credencial | Lo que el producto guarda en lugar de la contraseña. **No es el «valor derivado» de la geometría** |
| Acceso firmado, Clave de firma | Lo que se emite para operar contra la pieza de datos, y el secreto con el que se firma |
| Terminación degradada | La forma de terminar de una operación que no se pudo completar por causa del mundo |
| Arranque detenido | La forma propia de la preparación del almacén: el servicio no atiende ninguna petición |
| Transformación de esquema | Cada paso versionado que lleva el almacén de una forma a la siguiente |
| Regla conceptual de modelo | Cada una de las siete condiciones que el dato guardado tiene que cumplir. **No es una regla de negocio** |
| Segunda línea | El papel de las restricciones de unicidad del almacén frente a la consulta previa del consumidor |
| Validador, con sus dos referentes | `Glosario-Funcional.md` §3.1 |
| Repositorio, con sus tres referentes | `Glosario-Funcional.md` §3.2 |
| Derivado, con sus dos referentes | `Glosario-Funcional.md` §3.3 |

### 4.3 De los glosarios de GeometriaFactory-Domain y GeometriaFactory-Application

| Término | Qué designa, en una línea |
| --- | --- |
| Alumno, Papel, Estado de cuenta, Credencial derivada | El vocabulario de la cuenta |
| Texto original, Posición de pieza, Especie de observación | El vocabulario del trabajo interpretado |
| Familia plana o volumétrica | Clasificación que se deriva del tipo y **no se guarda** |
| Desenlace, Alcance del administrador | Las dos nociones que gobiernan el cierre del circuito |
| Sujeto de la regla | La persona sobre la que recae una regla, que no es actor de ningún caso de uso |
| Puerto, y los cuatro puertos por su nombre | Los contratos que la capa de aplicación declara y **ésta implementa** |
| Verificación de pertenencia, verificación de facultad | Las dos comprobaciones que **no se hacen acá** |
| Alcance de consulta, Unidad de trabajo, Motivo, Doble | El vocabulario de la orquestación. **El doble es lo que acá se reemplaza**, no lo que se escribe |
| Marca de cambio de contraseña pendiente | El atributo que el reseteo deja sobre la cuenta y que sólo el cambio efectivo levanta |
| Metadato de orquestación | Los sellos de alta, de modificación y de desenlace, que aquella capa aporta al materializar |

Los seis términos normativos del framework —producto, unidad de entrega, módulo, solución de código, proyecto de código y proyecto— conservan el sentido de `Vocabulario-Rules.md` §2 y no se redefinen acá. En particular, **«trabajo» no es «unidad de entrega»**: las unidades de entrega de este producto son las dos piezas desplegables, y el trabajo del alumno es un registro de datos que no se despliega.

## 5. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 2.0 | 2026-08-16 | **Consolidación de la fusión** (`Audit/Migracion-M10-Consolidacion-Fusion.md` 1.2 §4). Pasa de ser el documento de un proyecto de código a ser el de la **unidad de entrega**, con una subsección por proyecto y su texto transpuesto **sin reescritura**. Entra **§0**. Los absorbidos quedan archivados. Sube **major**. |
