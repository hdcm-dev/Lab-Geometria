# DX — Superficie pública de los adaptadores y de los mecanismos

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** DX-Developer-Experience.md
**Versión:** 1.4
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** DX Lead (AG-03)
**Variante:** DX
**Trazabilidad upstream:** `02-Especificacion-Funcional/Especificacion-Funcional.md` §1, §3, §4, §6, §8 y §11; §6 de los diez casos de uso CU-06001 a CU-06010 de `02-Especificacion-Funcional/Casos-De-Uso/`, y sus §3, §5, §9 y §10; `02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md` completo; `02-Especificacion-Funcional/Modelo-Datos/` completo; `02-Especificacion-Funcional/Glosario-Funcional.md`; `00-Contexto/Vision-Producto.md` §9 (glosario raíz de la cadena); `00-Contexto/Alcance-Producto.md`; `01-Necesidades-Negocio/Necesidades-Negocio.md` (NB-00001 a NB-00009); RN-06001 a RN-06016 de `Proyectos/GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/`; `Proyectos/GeometriaFactory-Application/02-Especificacion-Funcional/Especificacion-Funcional.md` §3, §4, §6 y §8; `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` **1.2** §5; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.12** §11, §14 y §17.3 íntegro
**Trazabilidad downstream:** `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas`, `09-Devops` y `11-Documentacion` de GeometriaFactory-Infrastructure

---

## Tabla de contenido

- [1. Rol de intervención developer](#1-rol-de-intervención-developer)
  - [1.1 Quién interviene acá](#11-quién-interviene-acá)
  - [1.2 Qué es la superficie pública de este proyecto de código](#12-qué-es-la-superficie-pública-de-este-proyecto-de-código)
  - [1.3 La frontera entre el mecanismo y la decisión](#13-la-frontera-entre-el-mecanismo-y-la-decisión)
  - [1.4 Las tres cosas que sólo se rompen acá](#14-las-tres-cosas-que-sólo-se-rompen-acá)
- [2. Onboarding por tramos](#2-onboarding-por-tramos)
- [3. Quick-start](#3-quick-start)
  - [3.1 Pasos](#31-pasos)
  - [3.2 Verificación del quick-start](#32-verificación-del-quick-start)
- [4. Diátaxis](#4-diátaxis)
  - [4.1 Dónde vive cada modo](#41-dónde-vive-cada-modo)
  - [4.2 Cómo se enlazan](#42-cómo-se-enlazan)
- [5. Mensajes de error y diagnóstico](#5-mensajes-de-error-y-diagnóstico)
- [6. Métricas DX](#6-métricas-dx)
- [7. Feedback loop](#7-feedback-loop)
- [8. Trazabilidad](#8-trazabilidad)
- [9. Control de cambios](#9-control-de-cambios)

---

## 1. Rol de intervención developer

### 1.1 Quién interviene acá

No hay integradores externos. `GeometriaFactory-Infrastructure` no se publica en ningún feed, se compila dentro de la solución de código del producto y **no la referencia nadie más que la composición de raíz de `GeometriaFactory-Api`**. Pero hay un tipo de interviniente que las capas de adentro no tienen, y es el que ordena buena parte de esta sección:

| Tipo de developer | Quién es acá | Qué necesita de esta documentación |
| --- | --- | --- |
| Implementador de adaptadores | La persona que sostiene el producto, o el agente de IA que construye por etapas, escribiendo la implementación de un puerto que `GeometriaFactory-Application` declaró | Qué le pide el puerto, **qué garantías tiene que sostener** y **qué no puede devolver** sin romper un caso de uso de la capa de adentro |
| Mantenedor de la capa | La misma persona, semanas después, sin el contexto de la etapa en que lo escribió. El equipo es de **una persona más un agente de IA** | Dónde va un adaptador nuevo, por qué una condición existe, y **cuál de los atajos tentadores está prohibido y por qué** |
| **Operador del despliegue** | El docente, que despliega **a mano** el contenedor de la pieza de datos | Qué significa cada terminación degradada y cada arranque detenido, y **qué revisar del lado del despliegue**: el volumen, la ruta, la clave de firma, el linaje de transformaciones |
| Integrador por casos de uso | **No aplica acá.** Nadie invoca esta capa por su superficie: la composición de raíz la conecta y los casos de uso la usan **a través de los puertos**, sin conocerla | — |

**El operador es lo que hace distinta a esta sección.** En el proyecto de código hermano ese papel se declara «no aplica»: aquella capa no atiende peticiones, no abre conexiones y no registra. Acá **seis de las diecisiete condiciones de error se diagnostican mirando el despliegue y no el código**, y por eso el operador tiene fila propia y no una nota al pie. Las seis, nombradas para que el recuento se pueda comprobar: `ALMACEN_NO_DISPONIBLE`, `RUTA_DEL_ALMACEN_NO_DISPONIBLE`, `MIGRACION_NO_APLICABLE`, `CLAVE_DE_FIRMA_AUSENTE`, `FUENTE_DE_ALEATORIEDAD_NO_DISPONIBLE` y `CREDENCIAL_DERIVADA_ILEGIBLE`.

Nivel de experiencia esperado: quien ya escribe código de acceso a datos, pero **no** necesariamente conoce el dato real que este producto tiene que leer. Esa parte no se supone conocida: se enseña en [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) §7, y su fuente es [`../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md`](../../../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md).

### 1.2 Qué es la superficie pública de este proyecto de código

Lo primero que hay que entender, y lo que decide si un cambio acá es correcto:

> **Esta capa no tiene superficie propia: tiene la forma de los contratos que otra capa declaró.** Los cuatro puertos son de `GeometriaFactory-Application`; acá se los implementa. Lo único propio son **dos mecanismos** —credenciales y acceso firmado— y **una responsabilidad de arranque**. La dependencia se invirtió arriba, y acá se paga la factura.

Cinco consecuencias operativas, que gobiernan todo lo demás:

1. **Acá vive el mecanismo y no la decisión.** Un adaptador que decidiera un estado, una autorización o una transición estaría mal ubicado. La tabla completa está en §1.3.
2. **Acá está el riesgo declarado del producto.** El intake registra, con probabilidad alta y con impacto alto, que **el validador se escribe sin leer el análisis** y rechaza el dato real de los alumnos: *«la aplicación no sirve para el dato que existe»*. Es el único riesgo de negocio del producto cuya mitigación es una batería de pruebas, y esa batería es de esta capa. **Antes de escribir una línea de lectura de texto hay que leer el documento de concepto central**, y no es una recomendación de estilo.
3. **Acá está la única persistencia del producto.** Es el único `library` de los siete con persistencia declarada —el flag vale true acá y en `GeometriaFactory-Api`, que delega en éste—, y por eso el único con un modelo de datos documentado. **El modelo del dominio manda**: acá se materializa, no se decide.
4. **Acá viven los tres secretos.** La contraseña en claro —que existe **sólo** en este proyecto de código—, el valor derivado de la credencial y la clave de firma. Ninguno entra a un mensaje, a una traza ni al repositorio de código.
5. **Acá se depende del mundo.** Un archivo que puede no estar montado, una fuente de aleatoriedad que puede no responder, un esquema que puede no corresponder. **Cinco de las diecisiete condiciones existen por eso**, y todas terminan de forma degradada o deteniendo el arranque: **ninguna finge un resultado**.

**Dos garantías que esta capa tiene que sostener y que su contrato no puede expresar solo**, porque se rompen produciendo algo válido:

- **La cantidad de figuras del conjunto raíz la produce el validador**, incluidas las que no se pudieron reconstruir, y **no es derivable de las piezas adoptadas**. Si el adaptador la calculara contando piezas, el número sería siempre creíble y siempre estaría mal cuando hubiera un hueco, y el mecanismo entero de la observación ubicada dejaría de ser comprobable.
- **La posición de una figura no reconstruida queda reservada**, y el almacén no compacta. Compactar tampoco falla: produce mensajes que apuntan a la figura equivocada.

### 1.3 La frontera entre el mecanismo y la decisión

Es la frontera que hace que el flag de autenticación valga true en este proyecto de código, y **conviene notar que vale true por un motivo distinto del de las dos capas de adentro**: allá vale porque se modela la regla o se ejerce la autorización; acá vale porque **está el mecanismo**.

**Enunciado en una línea: esta capa provee el mecanismo y no toma ninguna decisión de negocio.**

| Qué | Vive acá | Vive afuera |
| --- | --- | --- |
| Derivar una contraseña y verificar una credencial | **Sí** (CU-06006). Es el último punto del recorrido de la contraseña en claro: acá se convierte en el valor guardado y acá se compara | — |
| **Producir la contraseña provisoria** del reseteo | **Sí** (CU-06007). Es una **delegación explícita** de las tres capas de arriba | — |
| Emitir y verificar el acceso firmado | **Sí** (CU-06008) | — |
| Leer el texto real del alumno y emitir observaciones ubicadas | **Sí** (CU-06001, CU-06002) | — |
| Guardar, recuperar y retirar | **Sí** (CU-06003, CU-06004, CU-06005) | — |
| Decidir si una cuenta admite el acceso, y con qué motivo | **No.** Llega resuelto: una cuenta no admitida **no llega** a la emisión | `GeometriaFactory-Domain` y `GeometriaFactory-Application` |
| Comprobar pertenencia o facultad | **No.** El recorte de una consulta **llega en el pedido** | `GeometriaFactory-Application` |
| Decidir el estado del trabajo tras el envío | **No.** Se entrega el conjunto de observaciones y **el dominio resuelve** | `GeometriaFactory-Domain` |
| Comparar el correo escrito como confirmación de una baja | **No.** Llega resuelto | `GeometriaFactory-Application` |
| Traducir un código a respuesta de protocolo | **No** | `GeometriaFactory-Api` |
| Decidir dónde vive el archivo del almacén y cuándo arranca el servicio | **No.** La ruta llega de configuración | `GeometriaFactory-Api` y `09-Devops` |

Tres precisiones que la tabla no alcanza a decir sola:

1. **El traslado del recorte no es una comprobación de autorización.** Que una consulta llegue acotada por dueño o por alcance es una decisión ya tomada afuera. Lo único que esta capa hace por su cuenta es **negarse a resolver una consulta que llega sin recorte**, y lo hace por integridad del pedido: no sabe quién preguntó.
2. **Las restricciones de unicidad del almacén sí son una segunda línea, y eso es deliberado.** El código de correo ocupado se llama igual acá y en la capa de aplicación, y no es casualidad: `GeometriaFactory-Application` `CU-06001` **FA-02** ya declara ese camino como flujo alternativo propio, con el mismo motivo. **La verificación previa no es una garantía por sí sola.**
3. **La marca de cambio de contraseña pendiente se conserva acá y se comprueba afuera.** Esta capa la escribe, la conserva sobre cualquiera de los tres estados de cuenta y la hace viajar; **la comprobación transversal que confina a la cuenta es de la capa de aplicación**. Sin el dato, esa comprobación no tendría sobre qué decidir; con el dato, esta capa no decide nada.

### 1.4 Las tres cosas que sólo se rompen acá

De las **dieciséis** reglas de negocio del producto, **tres tienen su tramo principal en esta capa**, y es la única de la que eso se puede decir. La consecuencia práctica es directa: **si acá se hacen mal, ninguna capa de más adentro puede repararlas**.

| Regla | Qué se rompe si acá se hace mal | Dónde se verifica |
| --- | --- | --- |
| **RN-06008** — el texto original se conserva íntegro | Normalizar el texto al guardarlo no falla: el alumno vuelve a abrir su trabajo y ve un texto que no escribió, las comas finales desaparecen y el escenario que documenta la tolerancia deja de ser reproducible desde el almacén | CU-06001 CA-09, CU-06003 CA-01 y CA-02 |
| **RN-06009** — toda observación de error indica posición y campo | Compactar las posiciones no falla: produce mensajes que apuntan a la figura equivocada, y el alumno busca su defecto donde no está | CU-06001 CA-04, CU-06003 CA-08 |
| **RN-06014** — la provisoria la produce el sistema, no es adivinable y no se repite | Componerla con un contador o con la fecha no falla: el reseteo parece haber funcionado. **Un reseteo que no se completa es recuperable; una provisoria adivinable no se nota hasta que alguien la usa** | CU-06007 CA-01 a CA-05 |

**Las tres se rompen produciendo algo válido**, y ése es el patrón. Por eso las tres tienen criterios de aceptación que comparan, cuentan o inspeccionan, en lugar de esperar que algo falle.

**RN-06014 merece una nota aparte porque es una delegación con nombre.** `GeometriaFactory-Application` §6 declara que es **la única de las dieciséis sin tramo en su capa**, porque el valor le llega ya producido y ya derivado; `GeometriaFactory-Contracts` `CU-06008` §10 exige las dos propiedades del valor devuelto y declara explícitamente que **el contrato no declara mecanismo**; y la propia regla, en `GeometriaFactory-Domain`, nombra a este proyecto de código como el lugar de la generación. **Tres documentos apuntan acá. Acá no hay a quién apuntar.**

## 2. Onboarding por tramos

Cada tramo cierra con un objetivo verificable: algo que se ejecuta o se responde, no una lectura declarada como hecha.

| Tramo | Objetivo | Cómo se verifica |
| --- | --- | --- |
| 5 minutos | El ciclo de construcción y de prueba corre entero dentro del entorno de desarrollo contenido | `./scripts/build.sh` termina en 0 y sin advertencias y `./scripts/test.sh` pasa entero |
| 30 minutos | **Sabe qué tiene de raro el dato real del alumno.** Dado un texto, nombra las cuatro trampas del formato y dice qué hace un lector ingenuo con cada una | Recorre [`../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md`](../../../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md) §2 y responde, sobre el texto del escenario **E-2**, por qué un lector estricto lo rechaza entero y por qué el ortoedro no se dibuja hoy |
| 1 hora | **Corre la batería obligatoria y entiende qué prueba cada caso.** Nombra los nueve casos de prueba del producto, dice qué escenario ejercita a cada uno y por qué el criterio negativo de **E-4** es más difícil de acertar que el positivo de **E-3** | La tabla de cobertura de §7 del documento de concepto central, reproducida sin abrirlo, y la batería del validador en verde |

El recorrido completo de esa primera hora, paso por paso, está en [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md).

**El tramo de 30 minutos es el que más rinde de todo el producto**, y su objetivo no es casual: es exactamente el conocimiento cuya ausencia el intake declara como el defecto que más veces se repite.

## 3. Quick-start

Objetivo del quick-start: **el primer resultado exitoso**, que acá es **la batería del validador en verde sobre los textos reales de los escenarios**. Es el resultado que mejor explica la capa: el validador se prueba **sin almacén**, porque recibe texto y devuelve observaciones.

### 3.1 Pasos

Todo el ciclo ocurre **dentro del entorno de desarrollo contenido definido en el propio repositorio**. El host no tiene las herramientas y no va a tenerlas. Ningún paso de acá se ejecuta en el host.

```bash
# 0. Abrir el repositorio de código en el entorno de desarrollo contenido, que el
#    propio repositorio define en `.devcontainer/`. Todo lo demás corre adentro.

# 1. Script de construcción de la solución de código.
#    Criterio de éxito: termina en 0 y sin advertencias.
./scripts/build.sh

# 2. Script de pruebas del repositorio.
#    Criterio de éxito: pasa entero, incluidas las diez pruebas del validador.
./scripts/test.sh

# 3. Guion de reinicio del almacén: deja el estado de primer arranque.
#    Criterio de éxito: el almacén queda vacío y con su esquema al día.
./scripts/reset-db.sh
```

Los pasos se nombran por su papel —entorno de desarrollo contenido, script de construcción, script de pruebas, guion de reinicio del almacén— y conservan su forma literal porque el lector los tiene que poder ejecutar. Las rutas y los nombres de guion salen del intake §16: **no se eligen acá**.

**El paso 3 es propio de este proyecto de código** y no existe en el quick-start de las capas de adentro. Es el camino de vuelta declarado del producto y lo que permite repetir cualquier prueba de persistencia desde un estado conocido.

Lo que el quick-start deliberadamente **no** incluye: arrancar la pieza de datos, configurar una clave de firma o alcanzar la red. Ninguna de las tres hace falta para el primer resultado, y si un paso futuro las pidiera, **el paso está mal ubicado**.

### 3.2 Verificación del quick-start

Los pasos son ejecutables a partir de la primera etapa, que es la que crea el andamiaje de la solución de código y ancla las versiones. El compromiso de verificación es el siguiente:

- Se ejecuta a mano, sobre un clon limpio, en el punto de control de cada etapa que toque este proyecto de código.
- Si un paso deja de valer, el documento sube versión en la misma operación y declara el motivo en su control de cambios.
- Los nombres de los guiones y las rutas salen del intake §16 y §17.1.P.6 · GeometriaFactory-Infrastructure, y no se inventan acá.

## 4. Diátaxis

Los cuatro modos existen, pero **tres de ellos ya viven en artefactos de la cadena** y este documento no los duplica: los ubica y los enlaza.

### 4.1 Dónde vive cada modo

| Modo | Orientación | Dónde vive | Qué responde |
| --- | --- | --- | --- |
| Tutorial | Aprendizaje | [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) | «Nunca escribí un adaptador de este producto; llevame de la mano una hora» |
| How-to | Tarea | Los diez casos de uso de [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/) | «Tengo que implementar la lectura del texto / el retiro / la emisión del acceso: qué garantías tengo que sostener» |
| Reference | Información | [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../02-Especificacion-Funcional/Especificacion-Funcional.md) §3 y §4; [`../02-Especificacion-Funcional/Modelo-Datos/`](../../../02-Especificacion-Funcional/Modelo-Datos/Modelo-Conceptual.md) para el dato guardado; [`DX-Error-Messages.md`](DX-Error-Messages.md) para las condiciones; los dos glosarios | «Qué guarda la pieza» / «qué significa `MIGRACION_NO_APLICABLE`» |
| Explanation | Comprensión | **[`../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md`](../../../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md), que es el explanation más importante del producto**; §1.2, §1.3 y §1.4 de este documento; [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) §7 | «Por qué el texto del alumno no es JSON válido y por qué eso no se corrige» |

Este documento es, él mismo, del modo **explanation**: fija el marco y no enseña ninguna tarea.

### 4.2 Cómo se enlazan

- El tutorial termina en «próximos pasos» y enlaza explícitamente a los tres modos restantes.
- Cada entrada del catálogo de errores enlaza al caso de uso que la declara, que es su how-to.
- Cada caso de uso declara en su §9 la regla de negocio, la regla conceptual de modelo y el puerto que implementa, que son su explanation.
- Cada regla conceptual de modelo declara en su §6 la prueba que la verifica.
- Los glosarios de esta sección y de la anterior referencian el glosario raíz en lugar de redefinir términos.

Regla de mantenimiento: **un how-to que empieza a explicar por qué, se corta y enlaza**; una explanation que empieza a enumerar pasos, se corta y enlaza.

## 5. Mensajes de error y diagnóstico

Principio de redacción, aplicado sin excepción a las **17** condiciones del catálogo: **qué pasó, por qué pasó, qué hacer al respecto**. La tercera parte es la que decide si el catálogo sirve, y acá tiene un destinatario que las capas de adentro no tienen:

> En esta capa, **la mitad de las condiciones no las provoca nadie que haya invocado mal**: las provoca el mundo. El diagnóstico dice entonces **qué revisar del lado del despliegue**, no qué corregir del lado del código.

Cinco precisiones que el catálogo hace cumplir:

1. **La mayoría de lo que parece un fallo acá es el funcionamiento normal del producto.** Un error de validación, un texto ilegible, cero advertencias, nada encontrado, un conjunto vacío, una credencial que no coincide y un acceso vencido **son resultados**. [`DX-Error-Messages.md`](DX-Error-Messages.md) §1.2 los reúne, y ninguno tiene entrada en el catálogo.
2. **La confusión más cara del producto es una sola**, y conviene poder recitarla: si un texto ilegible devolviera `INTERPRETACION_NO_DISPONIBLE` en lugar de una observación, el alumno vería «el servicio no está disponible» cuando lo que pasa es que su programa emitió algo que no se puede leer, y **se quedaría esperando a que el sistema se recupere de un problema que no tiene**.
3. **Ningún mensaje incluye la clave de firma, una contraseña, una provisoria, la ruta del almacén ni el texto del alumno.** Es RA-03, regla de nivel producto, y su contracara obligatoria es que **todo error que se muestre queda registrado del lado del servidor**: sin ese registro, la prohibición de exponer se convierte en imposibilidad de diagnosticar. La tabla está en [`DX-Error-Messages.md`](DX-Error-Messages.md) §1.4.
4. **Hay tres atajos prohibidos, y los tres son tentadores porque no fallan.** Componer la provisoria por otro medio cuando la fuente de aleatoriedad no responde; generar una clave de firma al vuelo; y caer hacia una ruta alternativa cuando el volumen no está montado. Los tres dejan el sistema funcionando y equivocado. Están en [`DX-Error-Messages.md`](DX-Error-Messages.md) §2.4, con un cuarto de la misma familia: descartar el almacén ante un esquema divergente.
5. **Esta capa no reintenta.** Devuelve el estado degradado y quien decida reintentar es el consumidor.

## 6. Métricas DX

Las métricas se miden **a mano**, cronometradas sobre un clon limpio en el punto de control de la etapa. No hay telemetría y no la va a haber: el producto no tiene canal de correo y no hay developers externos a quienes encuestar.

| Métrica | Definición | Objetivo | Cómo se mide |
| --- | --- | --- | --- |
| TTFS | Tiempo desde abrir el repositorio de código hasta la batería del validador en verde | <= 10 minutos, con el entorno de desarrollo contenido ya construido | Cronometrado a mano sobre un clon limpio |
| TTFV | Tiempo hasta el primer valor: haber corrido la batería obligatoria y saber qué prueba cada caso | <= 1 hora, que es el tramo largo de §2 | El objetivo verificable del tramo de 1 hora |
| Tasa de error en onboarding | Proporción de pasos del quick-start que fallan en la verificación del punto de control | 0 de 4 pasos | Ejecución del quick-start de §3.1 en cada punto de control |
| Cobertura del catálogo de errores | Condiciones declaradas en la §6 de los diez casos de uso que tienen entrada en el catálogo | **17 de 17**, sin inventadas | Recuento contra [`DX-Error-Messages.md`](DX-Error-Messages.md) §7 |
| **Cobertura de la batería obligatoria** | Casos de prueba del producto con criterio de aceptación en esta categoría | **10 de 10**, con los escenarios del intake como fixtures. Son los nueve de la batería obligatoria del producto más el décimo que §21 agrega para la dimensión no legible | La tabla de §7 del documento de concepto central, contra los criterios de CU-06001 y CU-06002 |
| **Textos de prueba inventados** | Cantidad de fixtures de validación que no salen de los escenarios declarados | **0, sin tolerancia.** Es una regla de delivery del producto: no se inventan textos de prueba | Revisión de los fixtures en cada punto de control |
| **Secretos y rutas filtrados** | Lugares donde un mensaje, una traza o el repositorio de código contienen la clave de firma, una contraseña, una provisoria o la ruta del almacén | **0, sin tolerancia** | Inspección del registro del servidor y del repositorio en cada punto de control, contra [`DX-Error-Messages.md`](DX-Error-Messages.md) §1.4 |
| **Atajos prohibidos** | Cantidad de lugares donde una de las tres condiciones de §2.4 del catálogo se resuelve con su atajo en lugar de detenerse | **0, sin tolerancia** | Revisión de las tres rutas de código en cada punto de control |

Las tres primeras son las métricas DX canónicas. Las cinco últimas son propias de este proyecto de código: dos porque acá está el riesgo declarado del producto, y tres porque acá están los secretos y los atajos que no fallan.

## 7. Feedback loop

No hay canal de issues externo ni encuesta a developers de adopción: el equipo es de una persona más un agente de IA, y el único consumidor es la composición de raíz de la pieza de datos. El lazo existe igual y usa los mecanismos que el producto ya tiene:

| Vía | Qué recoge | Cómo se incorpora |
| --- | --- | --- |
| Punto de control de la etapa | Detención obligatoria a la espera del OK explícito del Product Owner. Es donde se corre la verificación del quick-start y se miden las métricas de §6 | Lo que falla se corrige antes de avanzar; el documento afectado sube versión en la misma operación |
| Pull request de la etapa | El pull request de la etapa **es** el punto de control. Un cambio incompatible en un adaptador rompe la compilación de la composición de raíz | Una compilación rota es retroalimentación inmediata, no un accidente de construcción |
| **La batería obligatoria del validador** | Es la mitigación declarada del riesgo de negocio más alto del producto. Cuando un caso de la batería empieza a fallar, la señal no es «una prueba rota»: es **que el producto dejó de servir para el dato que existe** | Se corrige antes de fusionar, sin excepción. La cobertura mínima del validador es la más alta del producto |
| **La verificación de transformaciones de esquema** | Que se apliquen solas sobre un almacén inexistente es puerta de calidad bloqueante. Cuando deja de pasar, suele ser porque **se editó una transformación ya fusionada** | Se corrige la transformación nueva, no la vieja, y se declara en el control de cambios |
| **El despliegue a mano** | El docente despliega el contenedor a mano, y es quien primero ve un arranque detenido. **Un mensaje que no le alcanza para saber qué revisar es un defecto de esta sección**, no del despliegue | Se corrige el diagnóstico accionable de esa condición en el catálogo |
| Informe de cierre por etapa | Documento autocontenido por etapa | Lo que costó entender baja a esta sección como corrección de documentación |
| Uso por el agente de IA | Un tramo del onboarding que el agente no puede completar con los documentos enlazados es un defecto de esta sección, no del agente | Se corrige acá y se declara en el control de cambios |

## 8. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Rol de intervención | Implementador de adaptadores, mantenedor de la capa y **operador del despliegue**, los tres internos al producto. El integrador por casos de uso **no aplica** acá (§1.1) |
| Superficie pública que se documenta | Los diez contratos de [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/): los cuatro puertos que implementa, los dos mecanismos que provee y la responsabilidad de arranque |
| CU origen | CU-06001 a CU-06010 de este proyecto de código |
| Reglas de negocio relevantes | RN-06001 a RN-06016, con el lugar donde se ejerce cada una declarado en `Especificacion-Funcional.md` §6: **catorce con tramo acá, dos sin él, y tres con su tramo principal acá** —RN-06008, RN-06009 y RN-06014— |
| Reglas conceptuales de modelo | RC-06001 a RC-06007, en [`../02-Especificacion-Funcional/Modelo-Datos/`](../../../02-Especificacion-Funcional/Modelo-Datos/Modelo-Conceptual.md) |
| Necesidades de negocio | NB-00001 a NB-00009, **las nueve**, tres de ellas parcialmente. Es una de las dos secciones del producto que las tocan todas —la otra es `GeometriaFactory-Web`—, y el motivo está en `Especificacion-Funcional.md` §7.2 |
| Wireframes asociados | N/A. `tiene_ui_final` == false; el mínimo de wireframes para `library` es cero (`Rules-UX-UI-DX.md` §2.2) |
| US a generar en 06 | US de la lectura tolerante con sus cuatro trampas; US de las tres reglas con tramo principal acá, **con el atajo prohibido como criterio de aceptación**; US de las transformaciones de esquema aplicadas al arrancar; US del quick-start verificable en el punto de control |
| Tests previstos en 08 | Las nueve pruebas de la batería obligatoria con los textos de los escenarios como fixtures, **sin almacén**; las de persistencia contra el almacén real; y las inspecciones de secretos, rutas y textos de prueba inventados |
| Catálogo de diseño aplicado | N/A para variante DX (`Rules-UX-UI-DX.md` §1.4) |
| Configuración dirigida por esquema aplicada | N/A. La configuración —ruta del almacén, clave de firma— **la toma `GeometriaFactory-Api`** y esta capa la recibe ya resuelta |
| Primer arranque aplicado | N/A como extensión. **La preparación del almacén de CU-06010 es un contrato de uso**, no una superficie de aprovisionamiento |
| Acceso de operador único aplicado | N/A. Esta capa no dibuja ninguna superficie de acceso; la frontera está en §1.3 |
| Identidad de versión aplicada | N/A. No produce artefacto desplegable identificable: no se publica en ningún feed |
| Modelo UX-UI aplicado en la Fase B2, validación visual y línea de base | N/A. `requiere_maqueta` == false |

## 9. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial de la categoría para este proyecto de código. Declara el rol de intervención con **tres tipos internos, entre ellos el operador del despliegue**, que las capas de adentro declaran no aplicable y que acá tiene fila propia porque seis de las diecisiete condiciones se diagnostican mirando el despliegue; el enunciado de que esta capa **no tiene superficie propia** sino la forma de los contratos que otra declaró, con sus cinco consecuencias y las dos garantías que se rompen produciendo algo válido; la frontera entre el mecanismo y la decisión en tabla, con sus tres precisiones; **las tres reglas cuyo tramo principal vive acá** —RN-06008, RN-06009 y RN-06014— con lo que se rompe en cada caso y dónde se verifica; el onboarding en tres tramos, con el de 30 minutos dedicado al dato real del alumno; el quick-start entero dentro del entorno de desarrollo contenido, con el guion de reinicio del almacén como cuarto paso propio; la ubicación de los cuatro modos de Diátaxis; los principios de error con sus cinco precisiones, entre ellas la confusión más cara del producto; **ocho métricas DX** medibles a mano, tres de ellas con tolerancia cero; y el lazo de retroalimentación apoyado en el punto de control, en la batería obligatoria y en el despliegue a mano del docente. |
| 1.1 | 2026-08-10 | Actualización de la cita del `PRODUCT-INTAKE` de **1.11** a **1.12** en la trazabilidad upstream: 1.11 quedó archivada al resolver el Product Owner el desenlace del envío del escenario `E-8`. Corrige el hallazgo **H-02** del informe de auditoría `SDD/Docs/Audit/B-02-03-GeometriaFactory-Infrastructure-r1.md` (ronda 1). El delta entre 1.11 y 1.12 se revisó y sólo alcanza a `E-8`, que no toca lo que este documento declara: sin cambios de contenido. |
| 1.2 | 2026-08-10 | Alineación de recuento con `PRODUCT-INTAKE` **1.13**, que incorpora la regla **RN-06016** —habilitar una cuenta produce su contraseña provisoria— y lleva las reglas de negocio del producto de quince a **dieciséis**. §3 actualiza el recuento de las reglas de negocio del producto, que esta guía cita al declarar cuáles tienen su tramo principal acá; **las tres siguen siendo las mismas** —RN-06008, RN-06009 y RN-06014—. **Ninguna decisión de este documento cambia.** Sube minor. |
| 1.3 | 2026-08-10 | **Cierra el hallazgo `C-02` (P0) del informe de auditoría `SDD/Docs/Audit/Coherencia-Corpus-r1.md` 1.0 en una declaración viva que el informe no registra, contra `PRODUCT-INTAKE` 1.14.** La nota sobre **RN-06014** citaba a `GeometriaFactory-Application` §6 diciendo que es «la única de las **quince** sin tramo en su capa». Las reglas del producto son **dieciséis**, `RN-06001` a `RN-06016`, contadas sobre los archivos de `GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/`. La cita se corrige al recuento que hoy declara la fuente citada. **Ninguna condición, ningún diagnóstico y ninguna delegación cambia**: RN-06014 sigue siendo la única sin tramo allá y con tramo principal acá. Sube minor. |
| 1.4 | 2026-08-11 | **Cierra un hallazgo abierto por la búsqueda de propagación que exige el informe [`B-02-03-GeometriaFactory-Api-r1.md`](../../../../../Audit/B-02-03-GeometriaFactory-Api-r1.md) 1.0 en su hallazgo `B-API-06` (P1) y en la condición de método de su §10.** Aquel hallazgo levanta, sobre `GeometriaFactory-Api`, la cita de un reparto de reglas de negocio que la fuente no dice; el `grep` del reparto sobre todo el corpus vivo encontró **el mismo defecto acá**, en un proyecto de código que aquel informe no audita. **§8**, fila «Reglas de negocio relevantes», decía «**trece** con tramo acá, dos sin él», y la fuente citada —[`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../02-Especificacion-Funcional/Especificacion-Funcional.md) §6— dice «**Catorce** de las dieciséis tienen tramo acá y **dos** no lo tienen». Trece más dos son quince, y las reglas son **dieciséis**: el reparto era el anterior a `RN-06016`, que entró **con** tramo acá. Contadas las filas «Sin tramo acá» de aquella tabla: **dos**, `RN-06006` y `RN-06010`. Las **tres con tramo principal acá** —`RN-06008`, `RN-06009` y `RN-06014`— no cambian y son un subconjunto de las catorce. **Ningún artefacto, ninguna omisión declarada y ningún otro recuento cambia.** Sube minor. |
