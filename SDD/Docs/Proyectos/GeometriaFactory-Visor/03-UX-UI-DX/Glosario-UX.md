# Glosario UX — GeometriaFactory-Visor (variante DX)

**Proyecto de código:** GeometriaFactory-Visor
**Documento:** Glosario-UX.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-09
**Autor:** DX Lead (AG-03)
**Variante:** DX

**Trazabilidad upstream:** `../02-Especificacion-Funcional/Glosario-Funcional.md` (los veinticuatro términos que acuña la categoría 02 de este proyecto de código, referenciados en §4 y no redefinidos acá); `../../../00-Contexto/Vision-Producto.md` §9, glosario raíz de la cadena; `../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md` §4.6 (la sexta función), §5.5 (gobierno del movimiento automático de la escena), §6 y §7; `../../../00-Contexto/Compatibilidad-Plataformas.md` §2.3; `Rules-UX-UI-DX.md` §3.3; `Vocabulario-Rules.md` §2, §4 y §9; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §12.1, §17.7 P.7 y P.8, §18
**Trazabilidad downstream:** 05-Arquitectura-Tecnica, 06-Backlog-Tecnico, 08-Calidad-Y-Pruebas, 10-Examples, 11-Documentacion; y la Fase B2, que hereda este vocabulario al emitir sus tres artefactos en esta misma carpeta

---

## Tabla de contenido

- [1. Alcance de este glosario](#1-alcance-de-este-glosario)
- [2. Términos que esta categoría acuña](#2-términos-que-esta-categoría-acuña)
  - [2.1 Roles y recorrido de integración](#21-roles-y-recorrido-de-integración)
  - [2.2 Documentación y su organización](#22-documentación-y-su-organización)
  - [2.3 Diagnóstico y catálogo de condiciones](#23-diagnóstico-y-catálogo-de-condiciones)
  - [2.4 Medición](#24-medición)
- [3. Términos con más de un referente](#3-términos-con-más-de-un-referente)
  - [3.1 «Recorrido»](#31-recorrido)
  - [3.2 «Reference», modo de documentación](#32-reference-modo-de-documentación)
  - [3.3 Verificación negativa](#33-verificación-negativa)
- [4. Términos referenciados y no redefinidos](#4-términos-referenciados-y-no-redefinidos)
  - [4.1 Del glosario funcional de este proyecto de código](#41-del-glosario-funcional-de-este-proyecto-de-código)
  - [4.2 Del glosario raíz del producto](#42-del-glosario-raíz-del-producto)
  - [4.3 De otros documentos del producto](#43-de-otros-documentos-del-producto)
- [5. Control de cambios](#5-control-de-cambios)

---

## 1. Alcance de este glosario

Declara únicamente el vocabulario que **esta categoría** acuña: el de la superficie pública vista desde quien la integra, el del recorrido de integración y el del diagnóstico de condiciones. Es obligatorio para los ocho tipos D8, también en variante DX, porque los tipos DX acuñan el vocabulario de su propia superficie pública (`Rules-UX-UI-DX.md` §2.1).

Rige la **regla de no duplicación** de `Rules-UX-UI-DX.md` §3.3: todo término que ya está en `Glosario-Funcional.md` de 02 con la misma semántica se **referencia** y no se redefine, y lo mismo vale para el glosario raíz del producto. Las dos listas de referencia están en §4, y ninguna entrada de §2 las pisa.

Regla de inclusión aplicada: entra todo término que aparece en **más de un artefacto de esta categoría**. Un término que vive en un solo artefacto se define ahí y no entra acá.

## 2. Términos que esta categoría acuña

### 2.1 Roles y recorrido de integración

| Término | Definición operativa | Artefactos de 03 donde aparece | Sinónimos y alias |
| --- | --- | --- | --- |
| Rol de intervención | Quien interviene **sobre** el proyecto de código, no quien usa el producto. En este proyecto de código son dos, y los cumple la misma persona más un agente de IA | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md`, `DX-Error-Messages.md` | Reemplaza a «audiencia» en las secciones DX (`Rules-UX-UI-DX.md`, control de cambios 1.7). «Audiencia» queda para las secciones UX, que no existen en esta categoría |
| Developer integrador del bundle | Rol de intervención que embebe el archivo de guion en una superficie anfitriona e invoca las seis funciones desde ella. No modifica el interior del archivo de guion | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md`, `DX-Error-Messages.md` | «Integrador», en su forma corta, dentro de esta categoría. **No hay integrador externo**: el artefacto no se publica |
| Developer mantenedor del bundle | Rol de intervención que modifica el interior del archivo de guion —lectura de dimensiones, construcción de mallas, disposición, liberación— sin alterar el contrato | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | «Mantenedor», en su forma corta |
| Superficie pública | El conjunto de lo que un consumidor puede invocar: las **seis** funciones de la fachada, sus firmas, las siete garantías y los siete códigos de condición. Nada más. La sexta, `establecerMovimiento`, entró el 2026-08-09 y **no movió las otras dos cifras**: no acuña garantía ni código | Los cuatro artefactos de esta categoría | Se usa con la misma semántica que en `Definicion-Contrato-De-Fachada.md` §7, que la enuncia sin declararla como término |
| Recorrido de integración | Secuencia de invocaciones de las seis funciones que un rol de intervención ejecuta para verificar el contrato de punta a punta. Es la unidad de trabajo del onboarding y del quick-start | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | Ver §3.1: **la forma desnuda «recorrido» no se usa** en esta categoría |
| Tramo de onboarding | Cada uno de los tres cortes temporales del onboarding —5, 30 y 60 minutos— con un objetivo verificable propio | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | «Tramo», en su forma corta, dentro de esta categoría |
| Objetivo verificable | Enunciado de cierre de un tramo que se cumple o no se cumple por observación directa, sin juicio intermedio | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| Quick-start | Camino más corto desde el repositorio hasta ver una pieza dibujada: cinco pasos, todos dentro del entorno de desarrollo contenido | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| Ciclo corto de construcción | Camino que genera **sólo** el archivo de guion, sin compilar el resto del producto. Es el que rige para trabajar sobre el visor | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | Se opone al **ciclo completo de construcción**, que encadena las dos cosas (PRODUCT-INTAKE §17.7 P.8) |

### 2.2 Documentación y su organización

| Término | Definición operativa | Artefactos de 03 donde aparece | Sinónimos y alias |
| --- | --- | --- | --- |
| Modo de documentación | Cada uno de los cuatro modos de Diátaxis con los que se organiza la documentación de este proyecto de código: **modo tutorial**, **modo how-to**, **modo reference** y **modo explanation**. Cada modo tiene un solo dueño declarado | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md`, `README.md` | Los cuatro nombres se conservan en su forma original por ser los del marco. Ver §3.2 sobre «reference» |
| Lectura por sección | Propiedad de redacción de esta categoría: cada sección se escribe para ser legible sola, porque un agente de IA recibe secciones y no documentos (`Vocabulario-Rules.md` §9.2) | `DX-Developer-Experience.md`, `DX-Error-Messages.md` | — |

### 2.3 Diagnóstico y catálogo de condiciones

| Término | Definición operativa | Artefactos de 03 donde aparece | Sinónimos y alias |
| --- | --- | --- | --- |
| Entrada de catálogo | Desarrollo documental de un código de condición para una función concreta, identificado `E-VIS-XX`. Un mismo código puede tener varias entradas, porque el trabajo que le queda al anfitrión cambia según desde qué función se produjo | `DX-Error-Messages.md`, `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | El identificador `E-VIS-XX` es **documental**: no forma parte del retorno de ninguna función y no se muestra a nadie |
| Diagnóstico accionable | Las tres partes obligatorias de una entrada de catálogo: qué pasó, por qué pasó y qué hacer al respecto. Sin la tercera, la entrada no está terminada | `DX-Error-Messages.md`, `DX-Developer-Experience.md` | — |
| Acción del lado del anfitrión | Tercera parte del diagnóstico accionable. Es siempre trabajo del componente anfitrión, porque la fachada no puede resolver ninguna condición por su cuenta: no pide datos, no reintenta y no consulta nada | `DX-Error-Messages.md`, `Guia-Onboarding-Developer.md` | — |
| Alcance de la condición | Si una condición afecta a la **invocación completa** o a una **pieza suelta** dentro de una carga exitosa. La distinción cambia lo que el anfitrión tiene que leer del retorno | `DX-Error-Messages.md`, `Guia-Onboarding-Developer.md` | — |
| Gate de cero red | Verificación bloqueante de que el archivo de guion no origina ninguna petición: por inspección del código fuente y del artefacto generado, y contando peticiones en la pestaña de red durante la interacción. El umbral, exactamente 0, lo declara `../02-Especificacion-Funcional/Especificacion-Funcional.md` §6, lugar único de las seis propiedades transversales | `DX-Error-Messages.md`, `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | Es la verificación de la propiedad **cero red**, que declara `Glosario-Funcional.md`; el término de acá nombra el control, no la propiedad |
| Fuga de la fachada | Toda invocación a nombres internos del archivo de guion, o manipulación del elemento de dibujo, hecha por un anfitrión por fuera de las seis funciones. Tocar la escena para prender o apagar un movimiento automático, en lugar de invocar `establecerMovimiento`, es una fuga como cualquier otra. Su umbral es 0 y un valor distinto es defecto bloqueante | `DX-Developer-Experience.md`, `README.md` | — |

### 2.4 Medición

| Término | Definición operativa | Artefactos de 03 donde aparece | Sinónimos y alias |
| --- | --- | --- | --- |
| TTFS · time-to-first-success | Tiempo desde abrir el repositorio en el entorno de desarrollo contenido hasta ver dibujadas las piezas del escenario de cobertura | `DX-Developer-Experience.md`, `README.md` | Se mide con cronómetro y un solo observador: no hay telemetría posible en un proyecto de código sin red y sin persistencia |
| TTFV · time-to-first-value | Tiempo desde el primer éxito hasta modificar el interior del archivo de guion, regenerarlo y comprobar que el contrato quedó idéntico | `DX-Developer-Experience.md`, `README.md` | Mismo método de medición que TTFS |

## 3. Términos con más de un referente

Criterio aplicado: `Vocabulario-Rules.md` §9.1. Se desambigua **sólo** cuando los sentidos comparten contexto de lectura, y el contexto de lectura de un subagente es la sección.

### 3.1 «Recorrido»

Es el término polisémico verificado de esta categoría. **Evidencia de colisión, por ocurrencia y en secciones donde los dos sentidos conviven:**

| Sección | Ocurrencia con el sentido de integración | Ocurrencia con el sentido de continuidad de uso |
| --- | --- | --- |
| `DX-Error-Messages.md` §4, tabla de situaciones que no son entradas del catálogo | Fila «Petición de red observada durante el recorrido de integración» | Fila siguiente: «Diez recorridos de ida y vuelta no deben degradar la visualización» |
| `Guia-Onboarding-Developer.md` §6, tabla de trazabilidad | Filas «CU origen» y «Tests previstos»: «recorrido de integración completo sin backend», «recorrido de integración de humo» | Fila «Necesidad de negocio»: «10 de 10 recorridos de ida y vuelta» |

Los dos sentidos conviven **dentro de una misma tabla**, que es el caso en que la entrada de glosario no alcanza: quien lee una fila no tiene a la vista la otra. Por eso la forma de desambiguación elegida es la **calificada obligatoria**, que es el segundo escalón de `Vocabulario-Rules.md` §9.3, y no el primero.

| Referente | Qué designa | Dónde nace | Forma que le corresponde |
| --- | --- | --- | --- |
| De integración | Secuencia de invocaciones de las seis funciones que verifica el contrato de punta a punta | Esta categoría (§2.1) | **Siempre calificado**: «recorrido de integración» |
| De continuidad de uso | Cada ida y vuelta entre trabajos, de los diez con que se verifica que la visualización no degrada | `NB-00006` §5, tercer criterio; `CU-12005` CA-04 | **Siempre calificado**: «recorrido de ida y vuelta» |

**La forma desnuda «recorrido» no se usa como sustantivo en esta categoría.** Es el corolario de `Vocabulario-Rules.md` §9.2: cuando conviven dos formas calificadas, el término desnudo es el defecto.

**Alcance exacto de la invariante, para que sea verificable por barrido.** Gobierna el **sustantivo en uso** —«el recorrido», «los recorridos»—, que es la forma que admite los dos referentes. No gobierna:

- Las **formas verbales y el participio** —«se recorren los tres tramos», «recorrer trabajos de ida y vuelta», «las seis funciones, recorridas en el orden de su ciclo de vida»—, donde el complemento del verbo fija el referente en la misma oración.
- Las **menciones metalingüísticas**, en las que el término se nombra a sí mismo entre comillas: el título de esta entrada, las filas de glosario que la citan y las entradas de control de cambios que la registran.

Extender la invariante a esas formas sería la **sobrecorrección** que `Vocabulario-Rules.md` **§9.1** tipifica como defecto: esa sección cierra diciendo que «la corrección que ese falso positivo induce —calificar todas las ocurrencias del término— **es** un defecto», con el énfasis en «es» que trae la fuente. No corresponde citar acá §9.4, que prohíbe otra cosa: declarar una invariante sin haber verificado que los contextos colisionan. Esa verificación sí se hizo, y su evidencia está más arriba en esta misma sección.

**Estado de cumplimiento verificado.** El audit `B-02-03-GeometriaFactory-Visor-r1.md`, hallazgo **H-03**, encontró la invariante declarada y no cumplida. `Vocabulario-Rules.md` §9.5 exige que el registro de una intervención léxica declare **cuántas ocurrencias se revisaron y cuántas se cambiaron**, porque es el par de cifras que permite distinguir una intervención por ocurrencia de una sustitución global disfrazada. Las dos:

| Cifra | Valor | Cómo se obtiene |
| --- | --- | --- |
| **Revisadas** | **61** ocurrencias de la raíz «recorrid» en los cinco artefactos de la categoría: **48** en el cuerpo y **13** en las entradas de control de cambios, que son el registro y no el corpus intervenido | Barrido de la raíz sobre los cinco archivos, clasificando cada ocurrencia por referente y por forma. El par cuerpo/registro se declara porque el total crece a cada ronda: el registro menciona el término al registrarlo |
| **Cambiadas** | **20** sustantivos desnudos en uso, calificados uno por uno y sin ninguna sustitución global: **nueve** en `DX-Developer-Experience.md`, **nueve** en `Guia-Onboarding-Developer.md` y **dos** en `DX-Error-Messages.md`. Más **un** participio de `Guia-Onboarding-Developer.md` §5 —«ya está recorrido»— sustituido por «ya se recorrió», que no admite lectura de sustantivo | Enumeración previa de las ocurrencias y sustitución sólo de las que cambiaban de forma, con barrido posterior de verificación |

Las ocurrencias revisadas y no cambiadas ya estaban calificadas, o son formas verbales, participios o menciones metalingüísticas que la invariante no gobierna. `README.md` §6 criterio 14 registra el estado corregido.

### 3.2 «Reference», modo de documentación

No es una polisemia declarada: es una precisión de nombre para no crear una. «Reference» designa acá **uno de los cuatro modos de Diátaxis**, y no la referencia bibliográfica ni la trazabilidad de un documento. Por eso los cuatro modos se escriben siempre con su calificador —«modo reference», «modo how-to», «modo tutorial», «modo explanation»— y nunca sueltos.

### 3.3 Verificación negativa

Se revisaron los demás términos acuñados en §2 buscando referentes múltiples dentro del corpus del producto. **Ninguno verificado** además del de §3.1.

En particular, **no se califican** «escena», «malla», «árbol» ni «instancia»: `Glosario-Funcional.md` §3.3 ya verificó que sus contextos son disjuntos y resolvió no calificarlos. Esta categoría **adopta esa resolución y no la reabre**; volver a calificarlos sería el falso positivo que `Vocabulario-Rules.md` §9.1 y §9.4 tipifican como defecto.

Tampoco se reabre la resolución de «pieza»: rige la del glosario raíz, con la forma desnuda reservada al referente del dominio y el segundo referente siempre calificado. En los artefactos de esta categoría el segundo referente **no aparece**.

## 4. Términos referenciados y no redefinidos

### 4.1 Del glosario funcional de este proyecto de código

Puntero único: [`../02-Especificacion-Funcional/Glosario-Funcional.md`](../02-Especificacion-Funcional/Glosario-Funcional.md). Los **veinticuatro** términos que esa categoría acuña se usan acá **con su misma semántica** —eran veinte hasta que la capacidad **F-25** y su sexta función sumaron los cuatro del movimiento automático—. Los que aparecen en más de un artefacto de esta categoría:

| Término | Cómo lo usa esta categoría |
| --- | --- |
| Fachada | El objeto que toda esta documentación describe. «Contrato de fachada» cuando se nombra el conjunto de funciones más sus garantías |
| Componente anfitrión | El destinatario de toda acción sugerida del catálogo de condiciones |
| Elemento de dibujo | Lo que el anfitrión entrega a `inicializar` y **no vuelve a tocar** por su cuenta |
| Instancia del visor · Identificador de instancia | Unidad del ciclo de vida que el onboarding recorre entera |
| Resultado de dibujo | Lo que el anfitrión tiene que leer completo, incluidas las piezas no dibujadas |
| Estructura del texto · Árbol · Índice de pieza · Selección | Material con el que el anfitrión sincroniza su árbol con la escena |
| Tipo dibujable · Malla · Escena · Disposición | Vocabulario del dibujo, usado en las entradas del catálogo y en las verificaciones de los tramos |
| Código de condición | Fuente única del catálogo de `DX-Error-Messages.md`. Los siete están declarados en `Definicion-Contrato-De-Fachada.md` §6 y esta categoría **no agrega ninguno**. La sexta función tampoco: `INSTANCIA_DESCONOCIDA` pasa a presentarse en **cinco** funciones y sigue siendo un solo código |
| Movimiento automático · Órbita de la cámara · Giro de las figuras | Los dos movimientos independientes de la capacidad **F-25** y su superordinado, declarados en `Glosario-Funcional.md` §2 y desarrollados en `Definicion-Contrato-De-Fachada.md` §5.5. Esta categoría los usa para decir **qué gobierna el anfitrión invocando la fachada** y qué no toca por su cuenta; no los redefine y no los renombra |
| Estado efectivo del movimiento | Lo que `establecerMovimiento` devuelve: el estado en que quedan **los dos** movimientos después de la operación. Es lo que el anfitrión lee para sincronizar su control visible con lo que la escena está haciendo, en lugar de suponerlo |
| Cero red · Cero persistencia | Dos de las **seis propiedades transversales**, verificadas en el quick-start y en cada tramo del onboarding. Su membresía y su umbral se declaran una sola vez en `../02-Especificacion-Funcional/Especificacion-Funcional.md` §6, y esta categoría no los re-enumera |
| Página integradora | La superficie del sample S-1 sobre la que se ejecuta todo el onboarding |
| Texto del trabajo | Lo que se pega a mano en el área de texto de la página integradora |
| Capacidad gráfica tridimensional | Prerrequisito del onboarding y causa de la entrada E-VIS-01 del catálogo |

**Frontera de vocabulario del movimiento automático, para que estos cuatro términos no se usen mal.** Los cuatro nombran **lo que la escena hace**, y ninguno nombra un control ni una preferencia. El **control visible**, la **consulta de la preferencia de movimiento reducido** del sistema y la **conservación de la elección** son del componente anfitrión: si el archivo de guion consultara la preferencia violaría G-3 —leer configuración propia— y si guardara la elección violaría G-2 —persistir—. La fachada sólo **recibe el estado deseado y lo aplica**, y devuelve el estado efectivo. Una frase de esta categoría que le atribuya a la fachada un control, una preferencia o una memoria de la elección es un defecto, no un matiz.

### 4.2 Del glosario raíz del producto

Puntero único: [`../../../00-Contexto/Vision-Producto.md`](../../../00-Contexto/Vision-Producto.md) §9.

| Término | Cómo lo usa esta categoría |
| --- | --- |
| Trabajo | Lo que el alumno entrega en el laboratorio. **No es una «unidad de entrega»**: ese término normativo designa a las piezas desplegables del producto |
| Pieza, referente del dominio | Cada figura del conjunto raíz del trabajo. Forma desnuda. Ver §3.3 |
| Pieza en su segundo referente | Siempre calificado. No aparece en los artefactos de esta categoría |
| Observación, advertencia, error de validación | Se nombran **sólo** para declarar que este proyecto de código no emite ninguna de las tres, y para que no se las confunda con los códigos de condición de la fachada, que son otra cosa y no llevan esos nombres |
| Fallo silencioso | Lo que la enumeración de piezas no dibujadas elimina, y el motivo por el que ninguna condición puede quedar sin entrada de catálogo |
| Componente, figura plana de una pieza | De donde se leen las dimensiones. **No confundir con «componente anfitrión»** |
| Punto de control | Momento en que se registran las métricas DX del recorrido de integración |

Rige además el **choque de vocabulario** de `Vision-Producto.md` §9.3 y `PRODUCT-INTAKE` §12.1: «proyecto de código» designa exclusivamente una unidad de compilación, **la palabra «proyecto» a secas no se usa**, y las dos unidades de la Actividad 1 que emiten el dato se nombran `Ejemplo1` y `Ejemplo2`.

### 4.3 De otros documentos del producto

| Término | Dónde está declarado | Cómo lo usa esta categoría |
| --- | --- | --- |
| Bundle | `PRODUCT-INTAKE` §17.7 y §14 | Nombre con el que el intake designa al artefacto de este proyecto de código. En la prosa de esta categoría se usa **archivo de guion**, que es la forma que fijó 02; «bundle» se conserva únicamente dentro de los dos nombres de rol de §2.1, que son los que el encargo de la categoría acuñó |
| Entorno de desarrollo contenido | `Compatibilidad-Plataformas.md` §2.3 | Único lugar donde se ejecuta cualquier paso de esta categoría. El host de desarrollo no tiene ni va a tener las herramientas |
| Artefacto generado | `PRODUCT-INTAKE` §17.7 P.7 | El archivo de guion se genera y **nunca se edita a mano** |
| Punto de extensión | `PRODUCT-INTAKE` §18 | El contrato de la fachada. Es lo que la documentación de esta categoría existe para sostener |
| Sample S-1 | `PRODUCT-INTAKE` §16.1 y §18 | La página integradora sin backend sobre la que corre el onboarding. Su materialización es de 10-Examples |
| Escenario E-1, escenario E-7 | `PRODUCT-INTAKE` §20 | Material de dibujo del quick-start y de los tramos: E-7 cubre los seis tipos dibujables, E-1 trae las tres piezas con el ortoedro incluido |
| `RA-01`, `RA-02`, `RA-03` | `PRODUCT-INTAKE` §14 | Reglas de arquitectura de nivel producto. `RA-02` es la que esta documentación no puede contradecir ni de pasada |

## 5. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Declara diecinueve términos acuñados por esta categoría, agrupados en roles y recorrido de integración, documentación, diagnóstico y medición; resuelve el término polisémico «recorrido» con forma calificada obligatoria en sus dos referentes y prohíbe su forma desnuda; precisa el uso de «reference» como modo de documentación; declara la verificación negativa adoptando sin reabrirla la resolución de `Glosario-Funcional.md` §3.3 sobre «escena», «malla», «árbol» e «instancia»; y referencia sin redefinir los términos del glosario funcional de 02, del glosario raíz del producto y de los demás documentos del producto. |
| 1.0 | 2026-08-08 | Correcciones absorbidas del audit `B-02-03-GeometriaFactory-Visor-r1.md`, sin subir versión por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **H-03**: §3.1 conserva la invariante —la desambiguación es genuina— y declara su **alcance exacto**, que gobierna el sustantivo en uso y no las formas verbales, los participios ni las menciones metalingüísticas, y registra el estado de cumplimiento: veinte ocurrencias calificadas una por una en los tres artefactos que las tenían, sin sustitución global. **H-04**: la evidencia de colisión deja de citar `DX-Developer-Experience.md` §2 —donde el sustantivo no aparece— y pasa a una tabla de dos filas con las secciones donde los dos sentidos conviven efectivamente, `DX-Error-Messages.md` §4 y `Guia-Onboarding-Developer.md` §6, cada una con la ocurrencia de cada sentido. **H-05**: la cabecera y §4.1 decían «diecinueve términos» al referirse al glosario de 02, que acuña **veinte**; el conteo queda corregido en los dos lugares sobre el valor que `Glosario-Funcional.md` fijó al absorber el mismo hallazgo. El «diecinueve» de §2, que cuenta los términos propios de esta categoría, **es correcto y no se toca**. **H-02, de su lado**: §2.3 y §4.1 remiten a `Especificacion-Funcional.md` §6 como lugar único de la membresía y del umbral de las **seis** propiedades transversales. |
| 1.0 | 2026-08-08 | Correcciones absorbidas del audit `B-02-03-GeometriaFactory-Visor-r2.md`, sin subir versión por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **N-01**: §3.1 fundaba el acotamiento del alcance de la invariante en `Vocabulario-Rules.md` §9.4, que prohíbe declarar una invariante sin verificar la colisión; la sección que tipifica la sobrecorrección es **§9.1**, y la cita pasa a ella con su enunciado textual. Se declara además por qué §9.4 no aplica acá: la verificación de colisión sí se hizo y su evidencia está en la misma sección. **N-02**: el registro de la intervención léxica declaraba sólo las ocurrencias cambiadas y decía «todas» las revisadas; `Vocabulario-Rules.md` §9.5 exige las **dos** cifras. Pasa a declarar **61 revisadas** —48 en el cuerpo y 13 en los controles de cambios— y **20 cambiadas** más un participio, con el método de obtención de cada cifra. |
| 1.0 | 2026-08-09 | Alineación con la **sexta función de la fachada**, `establecerMovimiento(id, opciones)`, acuñada por `Definicion-Contrato-De-Fachada.md` §4.6 al cerrar la **Fase B2** de validación visual de la maqueta, y con el intake **1.6**, que la consolida. **Sin subir versión** por `Master-Prompt.md` §5, que lo admite mientras el documento está en estado `Propuesto`. **(a) Conteo del glosario funcional de 02**: la cabecera y §4.1 pasan de **veinte** a **veinticuatro** términos, que es el valor que `Glosario-Funcional.md` fijó al sumar **movimiento automático**, **órbita de la cámara**, **giro de las figuras** y **estado efectivo del movimiento**. El «diecinueve» de §2, que cuenta los términos propios de esta categoría, **no cambia**: la sexta función no acuña vocabulario de 03. **(b) Los cuatro términos nuevos se referencian y no se redefinen**: §4.1 suma dos filas que declaran cómo los usa esta categoría, con puntero a `Glosario-Funcional.md` §2 y a `Definicion-Contrato-De-Fachada.md` §5.5. **(c) Superficie pública**: las entradas «Superficie pública», «Recorrido de integración», «Developer integrador del bundle», «Fuga de la fachada» y la fila «De integración» de §3.1 pasan de **cinco** a **seis** funciones; las **siete** garantías y los **siete** códigos **no cambian**, y la entrada «Código de condición» lo deja declarado junto con que `INSTANCIA_DESCONOCIDA` pasa a presentarse en **cinco** funciones. **(d) Frontera bundle/anfitrión**: §4.1 suma la nota que declara que el control visible, la consulta de la preferencia de movimiento reducido y la conservación de la elección son del anfitrión, porque hacerlas la fachada violaría G-3 y G-2. La invariante léxica de §3.1 **no se reabre** y sus dos cifras de intervención no cambian: los términos tocados en esta ronda no son «recorrido». |
| 1.0 | 2026-08-09 | Corrección absorbida de la auditoría `B2-Maqueta-GeometriaFactory-Web-r1.md`, **sin subir versión** por `Master-Prompt.md` §5. **`AB2-16`**: la fila del 2026-08-08 correspondiente al audit `B-02-03-GeometriaFactory-Visor-r2.md` figuraba **después** de la fila del 2026-08-09, y el control de cambios pasa a estar en orden cronológico. **`AB2-10`**: la fecha de cabecera pasa de 2026-08-08 a **2026-08-09**, que es cuando el documento se tocó por última vez. |
