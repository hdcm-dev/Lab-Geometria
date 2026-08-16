> **Artefacto archivado — estado `Superado`**
>
> Esta es una **copia archivada** del documento `DX-Error-Messages.md` en su versión **1.0**, tomada el 2026-08-09 por el orquestador SDD antes de que la versión vigente la superara (`Master-Prompt.md` §5 y §5.1).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-09
> - **Versión vigente:** [`DX-Error-Messages.md`](../../DX-Error-Messages.md)
>
> El cuerpo que sigue **no se modifica**: un registro que se corrige después deja de ser un registro. Este archivo no se renombra, no se reenlaza y no vuelve a tocarse.

---

# Catálogo de errores — GeometriaFactory-Contracts

**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** DX-Error-Messages.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-08
**Autor:** DX Lead (AG-03)
**Variante:** DX
**Trazabilidad upstream:** `02-Especificacion-Funcional/Casos-De-Uso/CU-06-Contrato-De-Respuesta-De-Error.md` íntegro (§4, §5, §6, §7, §8, §17); `CU-01` §5, §6, §10 y §17; `CU-02` §6 y §17; `CU-03` §6 y §17; `CU-04` §6, §6.1 y §17; `CU-05` §6, §6.1 y §17; `02-Especificacion-Funcional/Especificacion-Funcional.md` §2 y §6 (`RT-01` a `RT-07`); `00-Contexto/Vision-Producto.md` §9.1 (Fallo silencioso, Advertencia, Error de validación); `01-Necesidades-Negocio/Necesidades-De-Negocio/NB-04-Interpretacion-Fiel-Del-Dato-Del-Alumno.md` §1 y §5; `NB-08-Alcance-Del-Laboratorio-Desde-El-Aula.md` §5; `PRODUCT-INTAKE` §17.4 P.3, P.5, P.7, P.8 y P.10, §14 (RA-03), §20.E-5
**Trazabilidad downstream:** `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas`, `09` (verificación de los gates de construcción) y `11-Documentacion` de este proyecto de código

---

## Tabla de contenido

- [1. Principios de redacción de errores](#1-principios-de-redacción-de-errores)
- [2. Taxonomía](#2-taxonomía)
  - [2.1 Las dos clases](#21-las-dos-clases)
  - [2.2 Categorías dentro de la clase transportada](#22-categorías-dentro-de-la-clase-transportada)
- [3. Catálogo](#3-catálogo)
  - [3.1 Clase C — errores de construcción del contrato](#31-clase-c--errores-de-construcción-del-contrato)
  - [3.2 Clase T — errores transportados](#32-clase-t--errores-transportados)
  - [3.3 Señales declaradas que no son error](#33-señales-declaradas-que-no-son-error)
- [4. Tono y voz](#4-tono-y-voz)
- [5. Localización](#5-localización)
- [6. Control de cambios](#6-control-de-cambios)

---

## 1. Principios de redacción de errores

Los tres de siempre, aplicados a las dos clases de error de este proyecto de código: **qué pasó, por qué pasó, qué hacer al respecto**. Ninguna entrada de este catálogo se admite sin las tres partes.

Y cinco principios propios, que salen de las restricciones transversales y no de una preferencia de estilo:

1. **Lenguaje plano y sin culpa.** Ni al alumno que pegó un texto incompleto ni al mantenedor que agregó un campo de más. El texto describe el hecho y la salida.
2. **Nunca una dirección de servicio interno, una ruta de archivo de datos, un valor de secreto ni una traza de implementación.** Es `RT-01` y `RT-02`, y es la regla de arquitectura RA-03. Vale para el texto que el contrato transporta y también para el texto de diagnóstico que un mantenedor lee.
3. **Ubicar cuando se puede ubicar.** Cuando el fallo viene de interpretar el texto del alumno, el mensaje trae **índice de figura** y **campo señalado** (`CU-06` §4 paso 3). Un texto genérico ahí es exactamente el fallo silencioso que el producto viene a eliminar: la página actual no dice qué figura falló, y ése es el defecto que se corrige.
4. **No distinguir lo que el contrato decidió no distinguir.** Un recurso ajeno y un recurso inexistente producen el mismo código y el mismo texto (`CU-06` CA-05). Un canje fallido no revela si falló el correo o la contraseña (`CU-01` CA-03). Un texto «más útil» ahí es una filtración, no una mejora, y contradice un criterio de aceptación.
5. **Ningún fallo sin representación.** El conjunto de códigos es cerrado y `CONTRATO_ERROR_NO_CLASIFICADO` lo cierra (`CU-06` §7). Un camino por el que un fallo llegue sin código es un defecto del contrato, no un caso raro.

**Qué no decide este catálogo.** El texto que finalmente ve un alumno o un administrador en pantalla es decisión de `GeometriaFactory-Web` y de su propia categoría 03 (`CU-06` §10). Lo que se cataloga acá es el texto neutro que cruza la frontera de servicio y el diagnóstico que un mantenedor necesita. Tampoco se decide acá el código de estado de la respuesta del servicio: eso pertenece a `GeometriaFactory-Api` (`PRODUCT-INTAKE` §17.5 P.5).

## 2. Taxonomía

### 2.1 Las dos clases

Un ensamblado de tipos de transferencia planos no lanza excepciones en tiempo de ejecución: no tiene comportamiento. Sus errores son de dos clases distintas, con momentos, destinatarios y acciones distintas. Mezclarlas produce diagnósticos inútiles, y por eso el catálogo las separa en dos tablas.

| Clase | Cuándo se manifiesta | A quién le habla | Acción típica |
| --- | --- | --- | --- |
| **C — errores de construcción del contrato** | Al construir, o al revisar el pull request de la etapa | Al mantenedor y al agente de construcción por etapas | Corregir la superficie pública, o desplegar juntas las dos piezas desplegables |
| **T — errores transportados** | En tiempo de ejecución, del otro lado de la frontera de servicio | Al código de la pieza pública, que después decide qué mostrar | Corregir la solicitud, corregir el dato del alumno, o pasar a estado degradado |

La clase C es la propiedad de diseño más valiosa de este proyecto de código, y conviene decirlo con todas las letras: **como los dos extremos compilan contra el mismo ensamblado, un cambio incompatible rompe la compilación antes que el tiempo de ejecución** (`PRODUCT-INTAKE` §17.4 P.3). Recibir un error de la clase C es una buena noticia: significa que la señal llegó en el momento más barato posible.

La respuesta correcta a un incompatible de la clase C es **desplegar la pieza pública y la pieza de datos juntas** (`RT-06`). No es versionar rutas, no es introducir un contrato paralelo y no es agregar un campo de compatibilidad: no hay consumidores de terceros que justifiquen ninguna de las tres, y el intake descarta el versionado de rutas de forma explícita.

### 2.2 Categorías dentro de la clase transportada

| Categoría | Qué agrupa | Códigos |
| --- | --- | --- |
| Entrada inválida | La solicitud, o el dato que la solicitud transporta, no permite satisfacerla | `CONTRATO_CAMPO_REQUERIDO_AUSENTE`, `CONTRATO_CREDENCIAL_INVALIDA`, `CONTRATO_CONFIRMACION_NO_COINCIDE`, `CONTRATO_TEXTO_NO_INTERPRETABLE` |
| Recurso ausente | Lo pedido no existe, o no es visible para quien lo pide, sin distinguir los dos casos | `CONTRATO_TRABAJO_NO_ENCONTRADO`, `CONTRATO_ALUMNO_NO_ENCONTRADO` |
| Conflicto de estado | Lo pedido es incompatible con la situación actual de la cuenta o del trabajo | `CONTRATO_CUENTA_NO_HABILITADA`, `CONTRATO_CONTRASENA_NO_ESTABLECIDA`, `CONTRATO_CORREO_YA_REGISTRADO`, `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR`, `CONTRATO_ADMINISTRADOR_YA_CONFIGURADO` |
| Error transitorio | La otra pieza desplegable no respondió | `CONTRATO_SERVICIO_NO_DISPONIBLE` |
| Error interno | Un fallo que el contrato no previó, representado igual | `CONTRATO_ERROR_NO_CLASIFICADO` |

Las cinco categorías cubren los **trece** códigos del conjunto cerrado, sin huecos y sin superposición. `CONTRATO_LISTADO_VACIO` queda fuera de las cinco porque no es un código de error: las señales declaradas que no son error se catalogan aparte, en §3.3, del mismo modo que `CU-04` §6.1 y `CU-05` §6.1 las separan de su tabla de excepciones.

## 3. Catálogo

**Cómo reproducir las señales de la clase C.** Los dos comandos siguientes, ejecutados desde la raíz del repositorio **dentro del contenedor de desarrollo**, son los que producen o descartan las entradas `DXC-01` y `DXC-09`. Es el mismo bloque del quick-start de [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §3, y se ejecuta sin modificaciones:

```bash
# DXC-09 — el gate es "sin advertencias", no "sin errores".
bash scripts/build.sh

# DXC-01 — referencia prohibida hacia el proyecto de código de dominio.
# Resultado esperado: sin coincidencias.
grep -R "GeometriaFactory.Domain" src/GeometriaFactory.Contracts/ || echo "OK: RT-05 se cumple"
```

Las entradas `DXC-02` a `DXC-08` no se reproducen con un comando suelto: aparecen al construir después de un cambio concreto sobre la superficie pública, o en la revisión del pull request de la etapa. Las de la clase T se reproducen desde las pruebas de integración (`bash scripts/test.sh`), que son las que ejercitan los tipos contra el servicio real.

### 3.1 Clase C — errores de construcción del contrato

Nueve entradas. Se identifican `DXC-XX` y son locales a esta sección: no son códigos del contrato, sino situaciones de construcción y de revisión con su diagnóstico.

| Id | Categoría | Qué pasó | Por qué pasó | Qué hacer | Deriva de |
| --- | --- | --- | --- | --- | --- |
| `DXC-01` | Gate bloqueante | La construcción o la revisión rechazan el ensamblado porque declara una referencia hacia `GeometriaFactory-Domain` | Alguien reusó una entidad del dominio en lugar de declarar el tipo de transferencia equivalente. Es la vía por la que el acoplamiento vuelve y por la que la pieza pública termina conociendo campos que no le corresponden | Quitar la referencia y declarar el tipo de transferencia propio, aunque duplique forma: duplicar forma es el trade-off aceptado a cambio de desacoplar las dos piezas desplegables. **No se negocia ni se difiere** | `RT-05`; `PRODUCT-INTAKE` §17.4 P.8 y P.12 |
| `DXC-02` | Incompatible que rompe la compilación | Uno de los dos consumidores del contrato deja de compilar tras un cambio en la superficie pública | Se quitó o se renombró un campo de un tipo de transferencia, o cambió el conjunto de valores admitidos de un campo | Es la señal esperada, no una falla del proceso. Completar el cambio en los dos extremos y **desplegarlos juntos** | `RT-06`; `CU-01` §17, `CU-04` §17 |
| `DXC-03` | Incompatible que compila | Todo compila, pero la pieza pública dejó de cubrir todos los casos posibles | Se agregó un valor a un conjunto cerrado: papel, situación de cuenta, estado del trabajo, severidad de observación o código de error | Tratarlo como incompatible aunque compile: cubrir el valor nuevo en los dos extremos y desplegarlos juntos. Para los códigos de error existe la salida prevista de `CONTRATO_ERROR_NO_CLASIFICADO`, que evita agregar uno por cada fallo nuevo | `RT-06`; `CU-01` §17, `CU-02` §17, `CU-03` §17, `CU-05` §17, `CU-06` §17 |
| `DXC-04` | Incompatible de mayor impacto | El campo de texto original dejó de ser una sola cadena | Se intentó interpretar el texto del alumno dentro del contrato: piezas, componentes o valores derivados en la solicitud | Revertir. Contradice una decisión pre-tomada del intake y obliga a rehacer los dos extremos. La interpretación es de la pieza de datos y el dibujo es del bundle del visor | `RT-03`; `CU-03` CA-01 y §17; `PRODUCT-INTAKE` §17.4 P.11 |
| `DXC-05` | Se rechaza aunque compile | La revisión rechaza un campo nuevo de la superficie pública | El campo puede transportar el hash de contraseña, la clave de firma, una dirección de servicio interno, una ruta de archivo de datos o una traza de implementación | No se introduce. Es la restricción central del proyecto de código y baja a criterios verificables por inspección | `RT-01`; `CU-01` CA-02, `CU-02` CA-05, `CU-05` CA-05, `CU-06` CA-01; RA-03 de `PRODUCT-INTAKE` §14 |
| `DXC-06` | Se rechaza aunque compile | La revisión rechaza el texto original o los componentes de las piezas dentro del elemento de listado | Se quiso ahorrar una solicitud al abrir el detalle. El efecto es que el listado del administrador arrastra el texto completo de cada trabajo, que es lo que el requisito estructural evita | No se introduce. Si el problema real es el número de solicitudes, se discute en `05-Arquitectura-Tecnica` | `RT-04`; `CU-04` CA-01 y §17; `PRODUCT-INTAKE` §17.4 P.10 |
| `DXC-07` | Se rechaza aunque compile | La revisión rechaza una variante enriquecida del detalle del trabajo para el administrador | El administrador ve exactamente lo mismo que el alumno: es el mismo tipo con los mismos cinco bloques | No se introduce: contradice `CU-05` CA-06 | `CU-05` CA-06 y §17 |
| `DXC-08` | Desfasaje entre piezas | En tiempo de ejecución aparecen fallos de forma en la carga útil, o campos que llegan ausentes, y las pruebas de integración fallan en bloque | Las dos piezas desplegables están compiladas contra versiones distintas del ensamblado: se desplegó una sola tras un cambio incompatible | Desplegar las dos juntas. Es la regla operativa de `RT-06` y el motivo por el que existe. La detección temprana la da `DXC-02`; llegar hasta acá significa que la señal se ignoró | `RT-06`; `PRODUCT-INTAKE` §17.4 P.3 y P.7 |
| `DXC-09` | Gate bloqueante | La construcción termina en 0 **pero con advertencias** | El quality gate del pipeline de este proyecto de código es «compila sin advertencias», no «compila» | Resolver la advertencia antes de avanzar. El hito de 5 minutos del onboarding no está cumplido hasta que la salida esté limpia | `PRODUCT-INTAKE` §17.4 P.8 |

### 3.2 Clase T — errores transportados

Trece entradas, una por cada código del conjunto cerrado. La columna de texto propuesto es el **texto neutro** que viaja en el tipo de error; su verificación es por inspección contra el criterio de aceptación citado. La forma final del mensaje que ve la persona es de `GeometriaFactory-Web`.

La serie no se renumera cuando el conjunto cerrado crece: `DXT-13` cierra la serie aunque pertenezca a la familia de `CU-01`, para que los identificadores ya citados aguas abajo y en la auditoría sigan designando lo mismo. El orden de la tabla es de emisión, no de familia.

| Id | Código | Categoría | Texto neutro propuesto | Causa probable | Detalle de ubicación | Acción sugerida | Deriva de |
| --- | --- | --- | --- | --- | --- | --- | --- |
| `DXT-01` | `CONTRATO_CAMPO_REQUERIDO_AUSENTE` | Entrada inválida | «Falta completar un dato obligatorio de la solicitud.» | La solicitud llegó incompleta: sin correo, sin contraseña presentada, sin nombre, sin fecha o sin texto original, según el contrato de uso | Un detalle con el **campo señalado**, sin índice de figura | Recuperación: la pieza pública completa el campo y reintenta | `CU-06` §6; `CU-01` §6; `CU-02` §6; `CU-03` §6 |
| `DXT-02` | `CONTRATO_CREDENCIAL_INVALIDA` | Entrada inválida | «Los datos de ingreso no son correctos.» | El correo o la contraseña presentada no corresponden a ninguna cuenta; o el cambio de contraseña llegó sin la vigente o con una que no corresponde | Sin detalles. **El texto no nombra ni el campo de correo ni el de contraseña**: la respuesta no revela cuál de los dos falló | Terminación controlada, sin reintento automático. La persona vuelve a intentar desde la superficie de acceso | `CU-01` §6 y CA-03; `CU-02` §6; `CU-06` CA-03 |
| `DXT-03` | `CONTRATO_CUENTA_NO_HABILITADA` | Conflicto de estado | «La cuenta todavía no está habilitada para ingresar.» | La cuenta está pendiente de habilitación o bloqueada por el administrador | Sin detalles. El motivo viaja explicado, **no genérico**: la persona tiene que saber en qué situación está su cuenta | Handoff al flujo de admisión: la cuenta espera la habilitación del administrador | `CU-01` FA-01, §6 y CA-04 |
| `DXT-04` | `CONTRATO_CORREO_YA_REGISTRADO` | Conflicto de estado | «Ese correo ya tiene una cuenta en el laboratorio.» | El correo del registro ya pertenece a una cuenta | Sin detalles | Terminación controlada. No hay canal de correo en el producto: la persona resuelve con el administrador | `CU-02` §6 |
| `DXT-05` | `CONTRATO_CONFIRMACION_NO_COINCIDE` | Entrada inválida | «El correo escrito como confirmación no coincide con el de la cuenta.» | La baja exige escribir el correo de la cuenta como confirmación, y lo escrito no coincide | Un detalle con el campo de confirmación | La baja no procede. Recuperación por reintento con la confirmación correcta. **Es una barrera deliberada**: la baja arrastra los trabajos de la cuenta | `CU-02` FA-01, §6 y CA-03 |
| `DXT-06` | `CONTRATO_ADMINISTRADOR_YA_CONFIGURADO` | Conflicto de estado | «El laboratorio ya tiene su cuenta de administrador configurada.» | Se intentó configurar una cuenta de administrador cuando ya existe una | Sin detalles | Terminación controlada: el contrato no ofrece camino alternativo. La unicidad del administrador es invariante del dominio | `CU-02` FA-03 y §6 |
| `DXT-07` | `CONTRATO_TRABAJO_NO_ENCONTRADO` | Recurso ausente | «No se encontró el trabajo solicitado.» | El identificador no existe, **o** corresponde a un trabajo de otra persona | Sin detalles. Los dos casos producen código y texto idénticos: 0 campos permiten distinguirlos | Terminación controlada. **No agregar un texto que distinga los dos casos**: distinguirlos confirma la existencia del recurso ajeno | `CU-03` §6 y CA-04; `CU-05` §6; `CU-06` FA-03 y CA-05 |
| `DXT-08` | `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` | Conflicto de estado | «El trabajo no se puede eliminar en su estado actual.» | Se pidió eliminar un trabajo que no está en `Borrador` | Sin índice de figura; el texto **declara el estado actual** del trabajo | Terminación controlada. La persona reedita o finaliza según corresponda | `CU-03` §6 y CA-03 |
| `DXT-09` | `CONTRATO_TEXTO_NO_INTERPRETABLE` | Entrada inválida | «El texto del trabajo tiene defectos que impiden reconstruir las figuras.» | Al finalizar: el texto original tiene errores de validación —una figura de tipo desconocido, un elemento sin tipo, un conjunto raíz vacío, un texto que no se interpreta ni con tolerancia— | **Un detalle por defecto encontrado**, cada uno con índice de figura y campo señalado. Nunca un texto genérico | Handoff al flujo de reedición: el trabajo **permanece en `Borrador`** y se guarda igual; lo que no procede es la finalización. **El mismo código no es error al pedir el detalle**: ahí es la señal `DXT-N2` de §3.3, y el trabajo tiene que poder verse igual | `CU-03` §6 y CA-05; `CU-06` §6, FA-01 y CA-02; `CU-05` §6.1 por el tratamiento no-error; `PRODUCT-INTAKE` §20.E-5 |
| `DXT-10` | `CONTRATO_ALUMNO_NO_ENCONTRADO` | Recurso ausente | «No se encontró el alumno por el que se filtró.» | El filtro por alumno del listado referencia un identificador inexistente | Un detalle con el campo de filtro | Recuperación: reintentar sin filtro | `CU-04` §6 |
| `DXT-11` | `CONTRATO_SERVICIO_NO_DISPONIBLE` | Error transitorio | «El servicio no está disponible en este momento.» | La pieza de datos no responde o responde fuera de tiempo | **0 detalles y ninguna dirección**: ni del servicio que falló, ni de su archivo de datos. Es el único código que la propia pieza pública puede producir | Handoff al **estado degradado explícito** de la pieza pública, no una excepción sin manejar. Se distingue del listado vacío por el tipo recibido, no por el conteo | `CU-06` FA-02, §6 y CA-04; `CU-01` a `CU-05` §6 |
| `DXT-12` | `CONTRATO_ERROR_NO_CLASIFICADO` | Error interno | «No se pudo completar la operación.» | Un fallo que el contrato no previó | Sin detalles, con texto neutro y código genérico | Es la garantía de que ningún fallo llega sin representación, que es la definición de fallo silencioso que el producto viene a eliminar. Su aparición repetida es señal de que falta cubrir un caso, y se trata en el punto de control de la etapa | `CU-06` §6 y §7 |
| `DXT-13` | `CONTRATO_CONTRASENA_NO_ESTABLECIDA` | Conflicto de estado | «La cuenta está habilitada y todavía no tiene contraseña establecida.» | Una persona cuya cuenta ya fue habilitada intenta ingresar antes de establecer su contraseña, porque el registro no la elige | Sin detalles. El motivo viaja explicado, **no genérico**, y no se confunde con una cuenta pendiente o bloqueada, que es otra situación y tiene código propio | **Handoff al contrato de establecimiento de contraseña de `CU-02`, no reintento del ingreso.** El canje **no produce respuesta de sesión** y la respuesta de sesión sigue declarando cuatro campos, sin ninguno agregado para este caso: establecer la contraseña es un desenlace distinto del ingreso | `CU-01` FA-02, §6, CA-05 y **§10** (fundamento); `CU-06` §6; `PRODUCT-INTAKE` §17.5 P.5, §6 flujo 1 y §4 F-04 |

### 3.3 Señales declaradas que no son error

Dos entradas, que son exactamente las dos que 02 separa de su tabla de excepciones en `CU-04` §6.1 y `CU-05` §6.1. Se catalogan acá justamente para que no se traten como error, y **no se cuentan entre los trece códigos** de §2.2: la primera no pertenece al conjunto cerrado, y la segunda es un código que sí pertenece pero que en ese contrato de uso no produce respuesta de error.

| Id | Código | Qué es | Por qué se cataloga | Deriva de |
| --- | --- | --- | --- | --- |
| `DXT-N1` | `CONTRATO_LISTADO_VACIO` | **No es un error.** El contrato devuelve la colección de elementos de listado con cero elementos | Porque el listado vacío y el servicio no disponible son tipos distintos, y confundirlos produce el peor resultado posible: presentar como «no hay trabajos» lo que en realidad es una indisponibilidad. La pieza pública los distingue **por el tipo recibido, no por el conteo** | `CU-04` §6.1 y CA-05 |
| `DXT-N2` | `CONTRATO_TEXTO_NO_INTERPRETABLE`, al pedir el detalle | **No es una respuesta de error en este contrato de uso.** El detalle llega igual, con la colección de piezas en cero elementos y las observaciones de error de validación pobladas | Porque el trabajo existe y hay que poder verlo: devolver un error dejaría a la persona sin ver lo que cargó y sin saber qué corregir. **El mismo código sí es error al pedir la finalización**, donde es `DXT-09`. Es la única entrada del catálogo cuyo tratamiento depende del contrato de uso, y por eso las dos se leen juntas | `CU-05` §6.1; contrasta con `CU-03` §6 y CA-05 |

## 4. Tono y voz

El tono es el mismo de toda la documentación del producto: español rioplatense neutro técnico, sin marketing, sin emojis y sin exclamaciones. Y tres reglas de voz propias del catálogo:

- **Impersonal, no acusatorio.** «Los datos de ingreso no son correctos», no «escribiste mal la contraseña». El sujeto es el hecho, no la persona.
- **Sin números de la política ni parámetros internos.** El texto no enumera umbrales, ni tiempos, ni tamaños: describe la situación y la salida.
- **Vocabulario del glosario raíz, no del emisor del dato.** «Pieza» y «componente» cuando se habla del trabajo; «advertencia» cuando es una discrepancia entre valor declarado y valor derivado, y «error de validación» cuando impide interpretar. «Observación» sólo como superordinado de las dos, nunca como sinónimo de ninguna.

Los textos de la clase C tienen un destinatario distinto —un mantenedor o el agente de construcción— y por eso pueden nombrar identificadores de restricción (`RT-01`, `RT-04`) y de proyecto de código. Lo que **no** pueden hacer, igual que los de la clase T, es transcribir una dirección de servicio interno, una ruta de archivo de datos o un valor de secreto en un mensaje de diagnóstico.

## 5. Localización

**No hay traducción, y no es una omisión.** El producto tiene un único idioma, el español rioplatense, para las tres audiencias que existen: el alumno, el administrador del laboratorio y el equipo. No hay alcance internacional declarado en `Alcance-Producto.md`, y agregar una infraestructura de traducción a un ensamblado de tipos sin comportamiento sería introducir una dependencia donde el intake declara que no hay ninguna.

De ahí dos consecuencias operativas:

- **El texto neutro viaja en español dentro del tipo de error.** El contrato transporta texto ya redactado, no una clave de traducción.
- **El código es el identificador estable, no el texto.** Si en algún momento el producto necesitara traducir, la vía es que la pieza pública resuelva el texto a partir del código —que pertenece a un conjunto cerrado— y no que el contrato agregue campos. Esa decisión pertenecería a `05-Arquitectura-Tecnica`, no a esta categoría, y agregar un código al conjunto se sigue tratando como cambio incompatible.

## 6. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Cataloga las dos clases de error del ensamblado: nueve entradas `DXC-XX` de construcción del contrato, derivadas de `RT-01`, `RT-03`, `RT-04`, `RT-05` y `RT-06`, de la sección opcional de compatibilidad de versión pública de los seis contratos de uso y del intake §17.4 P.8, con el despliegue conjunto como acción y sin versionado de rutas; y doce entradas `DXT-XX` transportadas, una por código del conjunto cerrado de `CU-06`, con texto neutro propuesto, detalle de ubicación y acción sugerida. Suma una entrada de señal declarada que no es error, `CONTRATO_LISTADO_VACIO`, y declara que no hay política de traducción por alcance de un solo idioma. | DX Lead (AG-03) |
| 1.0 | 2026-08-08 | Corrección absorbida de la ronda 1 de auditoría (`Audit/B-02-03-GeometriaFactory-Contracts-r1.md`), sin subir versión por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **Alineación con el upstream, que es el cambio principal**: el conjunto cerrado pasó de doce a trece códigos porque AG-02 resolvió la contradicción de `CU-01` (H-02) sacando la contraseña no establecida de la respuesta de sesión y haciéndola viajar como respuesta de error con código propio; §3.2 suma `DXT-13` `CONTRATO_CONTRASENA_NO_ESTABLECIDA` con su diagnóstico del lado del consumidor —handoff al establecimiento de contraseña, no reintento del ingreso—, §2.2 la clasifica en conflicto de estado y actualiza el conteo a trece, y la serie **no se renumera** para no invalidar los identificadores ya citados. **H-14 en su forma de 03**: §3.3 pasa de una señal a dos y se retitula, para cubrir también `CONTRATO_TEXTO_NO_INTERPRETABLE` al pedir el detalle, que `CU-05` §6.1 declara no-error; `DXT-09` remite a `DXT-N2` y viceversa, porque es el único código cuyo tratamiento depende del contrato de uso. **H-09**: las siete referencias a la sección opcional pasan de §12 a §17, en la cabecera y en la columna «Deriva de» de `DXC-02`, `DXC-03`, `DXC-04`, `DXC-06` y `DXC-07`. **Precisión registrada por el audit §4.2**: la fila anterior de este control de cambios atribuía las nueve `DXC` a «`RT-01` a `RT-06`»; se enumeran las cinco restricciones que efectivamente las originan más el punto del intake que sostiene a `DXC-09`. | DX Lead (AG-03) |
