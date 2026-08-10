# Catálogo de condiciones de error de los adaptadores

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** DX-Error-Messages.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** DX Lead (AG-03)
**Variante:** DX
**Trazabilidad upstream:** §6 de los **diez** casos de uso de `02-Especificacion-Funcional/Casos-De-Uso/` (CU-01 a CU-10), de donde se deriva cada entrada, con sus §3, §5, §7, §8, §9 y §10; `02-Especificacion-Funcional/Especificacion-Funcional.md` §3, §4 (**la frontera entre mecanismo y decisión** y sus cuatro precisiones), §6 y §11; `02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md` §3, §4 y §8; `02-Especificacion-Funcional/Modelo-Datos/` completo; `02-Especificacion-Funcional/Glosario-Funcional.md` §2 y §3; RN-01 a RN-15 de `Proyectos/GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/`; `Proyectos/GeometriaFactory-Application/02-Especificacion-Funcional/` §4 y §6, y su `CU-05` §6, que declara qué motivo recibe por el puerto de validación; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.12** §11 (RN-B3, RN-B5), §14 (RA-03), §17.3 íntegro
**Trazabilidad downstream:** `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas` y `11-Documentacion` de GeometriaFactory-Infrastructure

---

## Tabla de contenido

- [1. Principios de redacción de errores](#1-principios-de-redacción-de-errores)
  - [1.1 Qué pasó, por qué pasó, qué hacer](#11-qué-pasó-por-qué-pasó-qué-hacer)
  - [1.2 Siete resultados que no son condiciones de error](#12-siete-resultados-que-no-son-condiciones-de-error)
  - [1.3 Qué emite esta capa y qué compone el consumidor](#13-qué-emite-esta-capa-y-qué-compone-el-consumidor)
  - [1.4 Lo que ninguna condición de esta capa puede decir](#14-lo-que-ninguna-condición-de-esta-capa-puede-decir)
- [2. Taxonomía](#2-taxonomía)
  - [2.1 Las categorías en uso](#21-las-categorías-en-uso)
  - [2.2 Las dos categorías vacías, y por qué acá lo están](#22-las-dos-categorías-vacías-y-por-qué-acá-lo-están)
  - [2.3 Forma de terminación](#23-forma-de-terminación)
  - [2.4 Las tres condiciones que fallan hacia el lado seguro](#24-las-tres-condiciones-que-fallan-hacia-el-lado-seguro)
  - [2.5 El caso de uso sin condiciones](#25-el-caso-de-uso-sin-condiciones)
- [3. Catálogo](#3-catálogo)
  - [3.1 CU-01 Interpretar el texto original y reconstruir las piezas](#31-cu-01-interpretar-el-texto-original-y-reconstruir-las-piezas)
  - [3.2 CU-02 Verificar los valores declarados contra los derivados](#32-cu-02-verificar-los-valores-declarados-contra-los-derivados)
  - [3.3 CU-03 Guardar y recuperar los trabajos](#33-cu-03-guardar-y-recuperar-los-trabajos)
  - [3.4 CU-04 Ejecutar el borrado físico y el arrastre de la baja](#34-cu-04-ejecutar-el-borrado-físico-y-el-arrastre-de-la-baja)
  - [3.5 CU-05 Guardar y recuperar las cuentas de la comisión](#35-cu-05-guardar-y-recuperar-las-cuentas-de-la-comisión)
  - [3.6 CU-06 Derivar la contraseña y verificar una credencial](#36-cu-06-derivar-la-contraseña-y-verificar-una-credencial)
  - [3.7 CU-07 Producir la contraseña provisoria del reseteo](#37-cu-07-producir-la-contraseña-provisoria-del-reseteo)
  - [3.8 CU-08 Emitir el acceso firmado](#38-cu-08-emitir-el-acceso-firmado)
  - [3.9 CU-10 Preparar el almacén al arrancar](#39-cu-10-preparar-el-almacén-al-arrancar)
- [4. Tono y voz](#4-tono-y-voz)
- [5. Localización](#5-localización)
- [6. Control de cambios](#6-control-de-cambios)
- [7. Cobertura y trazabilidad](#7-cobertura-y-trazabilidad)
  - [7.1 Recuento](#71-recuento)
  - [7.2 Verificación mecánica de cobertura](#72-verificación-mecánica-de-cobertura)
  - [7.3 Tabla de cobertura](#73-tabla-de-cobertura)
  - [7.4 Trazabilidad del artefacto](#74-trazabilidad-del-artefacto)

---

## 1. Principios de redacción de errores

### 1.1 Qué pasó, por qué pasó, qué hacer

Las tres partes son obligatorias en cada entrada y se corresponden con las tres columnas del catálogo: **mensaje** dice qué pasó, **causa probable** dice por qué pasó, **acción sugerida** dice qué hacer al respecto.

La tercera parte tiene acá un destinatario que las capas de adentro no tienen, y es lo que hace específico a este catálogo:

> En esta capa, **la mitad de las condiciones no las provoca nadie que haya invocado mal**: las provoca el mundo. Un archivo que no está montado, una fuente de aleatoriedad que no responde, un esquema que no corresponde. El diagnóstico dice entonces **qué revisar del lado del despliegue**, no qué corregir del lado del código.

Cinco reglas de redacción que ninguna entrada incumple:

1. **Lenguaje plano y sin culpar a nadie.** El enunciado describe la comprobación que se negó o la cosa que no respondió.
2. **Nada genérico.** No hay «error de base de datos» ni «error interno». Una condición dice **qué** no se pudo hacer y **con qué** no se pudo.
3. **Ninguna condición revela lo que RA-03 prohíbe.** Ningún mensaje incluye la ruta del almacén, la clave de firma ni la dirección de un servicio interno, **y todos quedan registrados del lado del servidor**. Es la única forma de diagnosticar sin exponer.
4. **Ningún código es un código de protocolo.** Su traducción pertenece a `GeometriaFactory-Api`.
5. **Ninguna condición deja efecto parcial.** Todas las escrituras ocurren dentro de una unidad de trabajo que se cierra entera o no se cierra.

### 1.2 Siete resultados que no son condiciones de error

Es la distinción que sostiene todo lo demás, y la que más se equivoca en esta capa: **la mayoría de lo que parece un fallo acá es el funcionamiento normal del producto.** Ninguno de los siete tiene entrada en este catálogo, y confundirlos con fallos produce un producto que le grita al alumno por hacer bien su trabajo.

| Lo que ocurre | Por qué **no** es una condición de error | Dónde está declarado |
| --- | --- | --- |
| Una figura del texto no se pudo reconstruir | Es una **observación de especie error de validación**: una entidad del dominio, un resultado, y **lo que el alumno tiene que ver** | CU-01 FA-01, FA-02, FA-05 |
| El texto no se pudo leer **ni con la tolerancia** | Es un resultado igual: se devuelven 0 figuras y **una observación**, no la condición degradada. El trabajo queda en `Borrador` y el alumno corrige | CU-01 FA-04 y **CA-10** |
| La verificación no encontró ninguna discrepancia | Cero advertencias es un resultado, no un fallo. Es el criterio negativo, más difícil de acertar que el positivo | CU-02 FA-01 |
| La recuperación no encontró nada | Es «nada encontrado». Quién lo traduce, **y sin revelar la existencia de un recurso ajeno**, es el consumidor | CU-03 FA-01, CU-04 FA-01, CU-05 FA-01 |
| Una consulta con alcance devuelve el conjunto vacío | Una comisión sin entregas todavía | CU-03 FA-02 |
| La credencial no coincide | Una contraseña equivocada es el caso normal. **No se distingue hacia afuera cuál campo falló** | CU-06 FA-01 |
| Un acceso está vencido, o su firma no corresponde | Es exactamente lo que la verificación existe para detectar. La renovación del producto es **por reingreso** | CU-08 FA-01, FA-02 |

**La consecuencia más cara de confundirlos** está en el segundo: si un texto ilegible devolviera `INTERPRETACION_NO_DISPONIBLE` en lugar de una observación, el alumno vería «el servicio no está disponible» cuando lo que pasa es que su programa emitió algo que no se puede leer. Se quedaría esperando a que el sistema se recupere de un problema que no tiene.

### 1.3 Qué emite esta capa y qué compone el consumidor

Esta capa emite un **código**, no un texto. No produce mensajes para personas, no los formatea y no los traduce: no expone endpoints y sus contratos son referencias de proyecto de código dentro de la misma solución de código.

La columna «mensaje» de este catálogo es el **enunciado canónico en lenguaje plano** de cada condición: la base sobre la que las capas de afuera componen lo que una persona lee. No es una cadena que la biblioteca produzca.

**Una sola de estas condiciones tiene destinatario declarado aguas arriba**, y conviene saberlo: `INTERPRETACION_NO_DISPONIBLE` es la que `GeometriaFactory-Application` `CU-05` §6 declara recibir por el puerto de validación. Las demás llegan a la composición de raíz o a la capa de aplicación sin nombre propio allá, y su traducción se decide en `05-Arquitectura-Tecnica`.

### 1.4 Lo que ninguna condición de esta capa puede decir

Es la restricción que este catálogo comparte con ninguna otra sección del producto, porque **acá viven los tres secretos y la única ruta de archivo**.

| Nunca aparece en un mensaje | Por qué | Qué corresponde |
| --- | --- | --- |
| La **clave de firma**, ni una parte de ella | No entra al repositorio de código, no entra a la imagen y no entra a un mensaje | «No hay clave de firma provista» |
| La **contraseña en claro** ni el **valor derivado** de una credencial | Es el último punto del recorrido de la primera —de acá para adentro sólo circula la segunda—, y el único lugar donde las dos conviven | «Falta la contraseña» |
| La **contraseña provisoria** producida | Se devuelve una vez, al consumidor, y **no se registra en ninguna traza** | Nada: el valor viaja en el resultado, nunca en un mensaje |
| La **ruta del archivo del almacén** | Es una dirección de servicio interno a los efectos de RA-03 | «La ubicación configurada del almacén no admite escritura» |
| El **texto original del alumno**, entero o en parte, dentro de un mensaje | El texto es el trabajo de una persona y el registro del servidor no es su lugar | La posición y el campo, que es lo que la regla exige |

**Y la contracara, que es igual de obligatoria:** todo error que se muestre queda **registrado del lado del servidor**. Sin ese registro, la prohibición de exponer se convierte en imposibilidad de diagnosticar.

## 2. Taxonomía

### 2.1 Las categorías en uso

| Categoría | Qué agrupa | Cuántas condiciones |
| --- | --- | --- |
| **Entrada inválida** | Lo que llegó no es utilizable: falta, está vacío, o pide algo que el contrato no admite | 6 |
| **Recurso ausente** | Lo que la operación necesita no fue provisto | 1 |
| **Conflicto de estado** | La operación es legítima, pero el estado del almacén o del conjunto no la admite | 4 |
| **Conflicto de facultad** | — | **0** |
| **Conflicto de alcance** | — | **0** |
| **Error transitorio** | Algo de lo que esta capa depende no pudo completar lo que se le pidió, por una causa que no depende de lo que se pidió | 5 |
| **Error interno** | El dato guardado no permite hacer lo que el contrato promete | 1 |

**El error transitorio es la categoría más poblada de este catálogo, y es la señal más clara de dónde está esta capa.** Las capas de adentro se prueban enteras con dobles y no dependen de nada; acá se depende de un archivo, de una fuente de aleatoriedad y de un secreto que alguien tiene que haber provisto. **Cinco de diecisiete condiciones existen porque el mundo puede no responder.**

### 2.2 Las dos categorías vacías, y por qué acá lo están

| Categoría | Por qué está vacía |
| --- | --- |
| **Conflicto de facultad** | **Esta capa no autoriza.** No comprueba el papel de quien pide y no recibe la identidad del solicitante para comprobarla. La verificación de facultad es de `GeometriaFactory-Application`, y llega resuelta |
| **Conflicto de alcance** | Por la misma razón. El recorte por dueño o por estado **llega en el pedido**: esta capa lo resuelve, no lo decide. Lo único que hace por su cuenta es **negarse a resolver una consulta que llega sin recorte**, y eso está clasificado como entrada inválida, porque lo que falta es un dato del pedido y no una facultad de quien lo hace |

**Es el espejo exacto del proyecto de código hermano.** En `GeometriaFactory-Application` estas dos categorías son las que existen y las que justifican su flag de autenticación; acá están vacías y el flag vale true por otra cosa: porque acá viven **los mecanismos**. Quien busque en este catálogo una negativa de autorización está buscando en la capa equivocada.

### 2.3 Forma de terminación

Dimensión ortogonal a la categoría, y hay que leerla junto con ella porque cambia lo que hay que hacer:

| Forma | Qué significa | Cuántas | Qué tiene que hacer quien la recibe |
| --- | --- | --- | --- |
| **Negativa sin escritura** | El contrato se niega. No abre la unidad de trabajo, o la cierra sin efecto; el almacén queda exactamente como estaba | 11 | Corregir la invocación |
| **Terminación degradada** | La operación no se completó por una causa que no depende del pedido, y el contrato **lo declara en vez de fingir un resultado**. **Esta capa no reintenta** | 4 | Informar el estado degradado. Reintentar, si corresponde, lo decide el consumidor |
| **Arranque detenido** | La preparación del almacén no se pudo completar y **el servicio no atiende ninguna petición**. Es la forma propia de esta capa y no existe en ninguna otra | 2 | Revisar el despliegue: el volumen, la ruta, el linaje de transformaciones. **No es un problema de código** |

**La forma «motivo de resultado» no se usa en este catálogo, y se declara para que su ausencia no se lea como olvido.** Es la forma de las consultas que siempre devuelven un resultado con su razón, y acá las consultas devuelven **el dato o nada encontrado**: la razón por la que no hay nada la pone el consumidor, que es el que sabe quién preguntó.

### 2.4 Las tres condiciones que fallan hacia el lado seguro

Son las tres que un implementador apurado convertiría en un valor por defecto, y las tres tienen en común que **el atajo no falla: funciona mal en silencio.** Es la clase de defecto que este catálogo existe para prevenir.

| Condición | El atajo tentador | Por qué el atajo es peor que la condición |
| --- | --- | --- |
| `FUENTE_DE_ALEATORIEDAD_NO_DISPONIBLE` | Componer la contraseña provisoria con un contador, con la fecha o con el correo del alumno | Produce una provisoria **adivinable**, que es exactamente lo que RN-14 prohíbe, y el reseteo parece haber funcionado. **Un reseteo que no se completa es recuperable; una provisoria adivinable no se nota hasta que alguien la usa** |
| `CLAVE_DE_FIRMA_AUSENTE` | Generar una clave al vuelo, o emitir sin firmar | El sistema arranca, emite accesos y nadie lo nota hasta que alguien falsifica uno. Una clave generada al vuelo además invalida todos los accesos en cada reinicio, con lo cual el síntoma visible es otro |
| `RUTA_DEL_ALMACEN_NO_DISPONIBLE` | Caer hacia una ruta alternativa dentro de la imagen | El servicio arranca, acepta trabajos de la comisión entera **y los pierde en el siguiente reemplazo de versión**. Nadie se entera hasta que alguien busca su trabajo y no está |

La regla que resume las tres, y que conviene poder recitar: **cuando el mecanismo no puede cumplir su promesa, se detiene y lo dice. No la cumple a medias.**

`MIGRACION_NO_APLICABLE` es de la misma familia y merece su propia línea porque su atajo es el más destructivo de todos: **descartar el almacén y crearlo de nuevo** deja el servicio impecable y sin los trabajos de nadie.

### 2.5 El caso de uso sin condiciones

**`CU-09` no tiene ninguna entrada en este catálogo, y su ausencia está declarada.** Devolver el momento actual no recibe entrada que pueda ser inválida, no toca el almacén, no consume secretos y no depende de nada que pueda no responder: la única forma de que falle es que falle el proceso entero, y eso no es una condición de ningún contrato.

Se deja escrito por dos motivos. Uno, para que una revisión posterior no lo levante como cobertura faltante: **son nueve subsecciones de catálogo para diez casos de uso, y el hueco es intencional**. Y dos, porque su ausencia dice algo: es el contrato más trivial de la capa, y **que sea trivial es la prueba de que la inversión está bien hecha**. Si algún día tuviera una condición, sería señal de que se le agregó lógica que pertenece a otro lado.

## 3. Catálogo

**Diecisiete condiciones, derivadas una por una de la §6 de los diez casos de uso.** Ninguna se inventó y ninguna quedó afuera; el recuento y la verificación mecánica están en §7.

**Una sola condición se declara en más de un caso de uso** —`ALMACEN_NO_DISPONIBLE`, en CU-03, CU-04 y CU-05, siempre con la misma causa—, lleva **una sola entrada** en la subsección donde aparece primero, y sus dos apariciones restantes se anotan ahí. **No hay ninguna fila excedente: 17 filas de tabla para 17 condiciones.**

### 3.1 CU-01 Interpretar el texto original y reconstruir las piezas

Es el contrato de mayor riesgo del producto. **Ninguna de sus dos condiciones nace de que el alumno haya escrito mal el texto**: eso produce observaciones, que son resultados (§1.2).

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `TEXTO_ORIGINAL_AUSENTE` | Entrada inválida | Se pidió interpretar sin texto | La invocación llegó con texto nulo o vacío | Aportar el texto original. **No se confunde con el conjunto raíz vacío**, que sí es un texto, sí se interpreta y sí produce una observación (CU-01 FA-03): acá el defecto es de la invocación y allá es del dato del alumno |
| `INTERPRETACION_NO_DISPONIBLE` | Error transitorio | La interpretación no se pudo completar por una causa que no depende del texto | El adaptador no pudo resolver | Informar el estado degradado y **no presentar el trabajo como interpretado**. **No se inventan observaciones, no se devuelve un conjunto vacío como si fuera un resultado y no se informan figuras que no se contaron.** Es el único código de este catálogo con destinatario declarado aguas arriba: `GeometriaFactory-Application` `CU-05` §6 lo espera por este puerto. **Esta capa no reintenta** |

**La confusión que este par previene.** Si un texto ilegible devolviera la segunda condición en lugar de una observación, el producto le diría al alumno que el servicio no está disponible cuando lo que pasa es que su programa emitió algo que no se puede leer. El criterio `CU-01` CA-10 existe exactamente para eso y exige el resultado, no el código.

### 3.2 CU-02 Verificar los valores declarados contra los derivados

Forma de terminación: negativa sin escritura. **Este contrato no emite errores de validación**: los emite CU-01.

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO` | Conflicto de estado | Se pidió verificar los valores sin haber reconstruido las piezas | La orquestación del adaptador salteó la interpretación | Reconstruir primero, por CU-01. **No se devuelve «0 advertencias»**: sería indistinguible de un trabajo verificado sin discrepancias, y convertiría un defecto de orquestación en un resultado creíble. **Es una decisión derivada de la categoría 02**, declarada como punto abierto en `../02-Especificacion-Funcional/Especificacion-Funcional.md` §11 y en `CU-02` §6 y §10: ninguna fuente enuncia esta condición |

### 3.3 CU-03 Guardar y recuperar los trabajos

Forma de terminación: negativa sin escritura en las dos primeras, degradada en las dos últimas. Ninguna deja escritura parcial.

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `CONSULTA_SIN_ALCANCE_DECLARADO` | Entrada inválida | La consulta de listado llegó sin dueño y sin predicado de alcance | El consumidor no trasladó el recorte al pedido | Trasladar el recorte **antes** de pedir. Un listado sin recorte sería el listado de todos los trabajos de la comisión, que es lo que RN-03 y RN-11 vienen a impedir. **Esta capa no lo comprueba por autorización sino por integridad del pedido**: no sabe quién preguntó |
| `ESCRITURA_QUE_REESCRIBE_EL_TEXTO_ORIGINAL` | Entrada inválida | El texto original no admite ser reemplazado | Una materialización aportó, para un trabajo existente, un texto distinto del conservado | Conservar el texto tal como el alumno lo pegó (RN-08, `RC-01`). **Es la condición que hace exigible la regla en el único lugar donde el texto puede perderse.** La reedición cambia los datos del trabajo y el texto que el alumno **vuelve a pegar**, nunca el ya guardado |
| `ESCRITURA_CONCURRENTE_RECHAZADA` | Error transitorio | Otra operación tenía el almacén tomado para escribir | El motor **no admite escrituras concurrentes** y el backend opera como escritor único | Informar y **no reintentar acá**: si corresponde reintentar, lo decide el consumidor. La concurrencia real es baja porque el alcance es de aula, y el escritor único es una restricción **aceptada por escrito** a cambio de un despliegue sin servicio de base de datos aparte |
| `ALMACEN_NO_DISPONIBLE` | Error transitorio | El almacén no está alcanzable | La ubicación configurada no responde, o el volumen persistente no está montado | Revisar el despliegue, no el código. **No hay réplica ni caché**: los datos no están disponibles hasta que el servidor vuelva, y la pieza pública lo declara como estado degradado. **El mensaje no incluye la ruta** (§1.4). Esta condición vuelve a declararse en CU-04 y en CU-05, con la misma causa |

### 3.4 CU-04 Ejecutar el borrado físico y el arrastre de la baja

Forma de terminación: negativa sin escritura. **Ninguna deja retiro parcial**, y es la propiedad entera de este contrato.

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `RETIRO_PARCIAL_NO_ADMITIDO` | Entrada inválida | La baja de una cuenta retira todos sus trabajos o no ocurre | Se pidió la baja sin declarar el arrastre, o declarándolo sobre un subconjunto | Declarar el arrastre completo. Un arrastre parcial dejaría **trabajos sin dueño**, que es la forma más silenciosa de romper el modelo: nada falla y el listado del administrador sigue mostrándolos. El criterio con el que RN-07 se verifica es que **no quede ningún trabajo del alumno dado de baja** |

**El almacén no disponible en el retiro.** `ALMACEN_NO_DISPONIBLE` tiene su entrada en §3.3 y vuelve a declararse acá con la misma causa. Su precisión propia: **no retira nada**, de modo que una baja interrumpida deja la cuenta y sus trabajos enteros (CU-04 CA-05).

### 3.5 CU-05 Guardar y recuperar las cuentas de la comisión

Forma de terminación: negativa sin escritura en las dos primeras.

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `CORREO_YA_REGISTRADO` | Conflicto de estado | El correo aportado ya pertenece a una cuenta | La materialización colisionó con una cuenta existente | **No informar el estado ni el papel de la cuenta que ocupa el correo.** Es la **segunda línea** de la unicidad: la consulta previa del consumidor no es una garantía por sí sola, y `GeometriaFactory-Application` `CU-01` **FA-02** ya declara ese camino: «el puerto de repositorio rechaza la materialización por una colisión de correo que la consulta no vio», con el mismo motivo. **El código se llama igual allá y acá, y no es casualidad**: es la misma regla verificada dos veces |
| `UNICIDAD_DE_ADMINISTRADOR_VIOLADA` | Conflicto de estado | La instancia admite una sola cuenta con papel `Administrador` | La materialización habría dejado dos | Usar el camino de configuración del administrador, que la capa de aplicación gobierna con su ventana de alta. **Acá se impide el resultado, no se explica el camino**: esta capa no conoce la ventana |

**El almacén no disponible en las cuentas.** `ALMACEN_NO_DISPONIBLE`, con entrada en §3.3, vuelve a declararse acá con la misma causa y sin precisión propia.

### 3.6 CU-06 Derivar la contraseña y verificar una credencial

Forma de terminación: negativa sin escritura. **Ninguna incluye en su respuesta la contraseña ni el valor derivado** (§1.4).

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `CONTRASENA_EN_CLARO_AUSENTE` | Entrada inválida | Se pidió derivar o verificar sin contraseña | La invocación llegó con valor nulo o vacío | Aportar la contraseña. **No se deriva la cadena vacía**: produciría un valor derivado válido para una credencial que nadie eligió, y la capa de aplicación ya rechaza el valor derivado vacío del otro lado de la frontera |
| `CREDENCIAL_DERIVADA_ILEGIBLE` | Error interno | El valor derivado guardado no permite verificar | No lleva los parámetros con los que se produjo, o su forma no corresponde a la función anclada | **Corregir el dato guardado o el camino de migración de parámetros, no la invocación.** Y **no responder «no coincide»**: lo haría indistinguible de una contraseña equivocada, y la cuenta quedaría inaccesible sin que nadie supiera por qué. Es un defecto del almacén o de una migración, no de quien intenta entrar |

### 3.7 CU-07 Producir la contraseña provisoria del reseteo

Forma de terminación: degradada. **Es el contrato con una sola condición, y es la más importante de este catálogo.**

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `FUENTE_DE_ALEATORIEDAD_NO_DISPONIBLE` | Error transitorio | No se pudo producir una contraseña provisoria | La fuente de material impredecible del sistema no respondió | **Informar que el reseteo no se completó, y no completarlo.** Bajo ninguna circunstancia se compone el valor por otro medio: con un contador, con la fecha o con un dato de la cuenta, la provisoria queda **adivinable**, que es exactamente lo que RN-14 prohíbe, y el reseteo parece haber funcionado. **Un reseteo que no se completa es recuperable; una provisoria adivinable no se nota hasta que alguien la usa.** El camino declarado es volver a intentar el reseteo, que produce un valor nuevo |

**Por qué esta condición existe y no es paranoia.** El fundamento de la regla que sostiene es de uso: si la provisoria la escribiera el docente, terminaría siendo la misma clave para toda la comisión. Una provisoria producida por un contador reproduce ese defecto **sin que nadie lo haya decidido**.

### 3.8 CU-08 Emitir el acceso firmado

Forma de terminación: negativa sin escritura. **Ninguna incluye la clave de firma ni la dirección de un servicio interno** (§1.4).

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `CLAVE_DE_FIRMA_AUSENTE` | Recurso ausente | No hay clave de firma provista | El arranque no recibió el valor por variable de entorno ni por archivo montado | Proveerla en el despliegue. **No se genera una clave de reemplazo al vuelo y no se emite sin firmar**: un acceso sin firma verificable es peor que ningún acceso, porque el sistema seguiría funcionando y nadie lo notaría hasta que alguien lo falsifique. **El mensaje no dice de dónde se esperaba leerla** |
| `RECLAMOS_INCOMPLETOS` | Entrada inválida | El acceso exige identificador, correo, papel y expiración | Se pidió emitir sin alguno de los cuatro | Aportar los cuatro. **Ninguno se completa con un valor por defecto**: un acceso sin papel dejaría a las capas de adentro decidiendo sobre un dato que nadie declaró, y uno sin expiración no vencería nunca |

### 3.9 CU-10 Preparar el almacén al arrancar

Forma de terminación: **arranque detenido**, en las dos. Es la única subsección del catálogo donde aparece esa forma.

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `MIGRACION_NO_APLICABLE` | Conflicto de estado | El esquema encontrado no corresponde al linaje de transformaciones conocido | El almacén está por delante del código, o su esquema divergió | Revisar el despliegue: restaurar el respaldo, o revisar la transformación. **No se aplica un esquema por aproximación y no se descarta el almacén**: el segundo atajo deja el servicio impecable y sin los trabajos de nadie. Causa frecuente: **una transformación ya fusionada que se editó** |
| `RUTA_DEL_ALMACEN_NO_DISPONIBLE` | Error transitorio | La ubicación configurada del almacén no admite escritura | El volumen persistente no está montado | Revisar el montaje del volumen. **No se cae hacia una ruta alternativa dentro de la imagen**: el servicio arrancaría, aceptaría trabajos de la comisión entera y los perdería en el siguiente reemplazo de versión. **El mensaje no incluye la ruta** (§1.4) |

## 4. Tono y voz

Coherente con la guía de estilo del producto: español rioplatense neutro técnico, sin marketing y sin emojis.

| Regla | Sí | No |
| --- | --- | --- |
| Describir lo que no se pudo hacer, no juzgar a quien invocó | «El almacén no está alcanzable» | «Te olvidaste de montar el volumen» |
| Nombrar la entidad y el estado con el vocabulario del dominio | «El correo aportado ya pertenece a una cuenta» | «Violación de restricción única» |
| Decir la acción en imperativo, **y del lado que corresponde** | «Revisar el montaje del volumen» | «Reintentar la operación» |
| No prometer lo que esta capa no hace | «Informar el estado degradado» | «Reintentando automáticamente» |
| No exponer secretos ni rutas | «No hay clave de firma provista» | «No se encontró la clave en `/run/secrets/...`» |
| No confundir el dato del alumno con un fallo | «Se pidió interpretar sin texto» | «El JSON del alumno es inválido» |
| Calificar siempre `Pendiente` | «cuenta `Pendiente`», «trabajo en estado `Pendiente`» | «pendiente» a secas |

Dos excepciones declaradas a la regla de calificación de `Pendiente`, que no son defectos: **los nombres de los códigos son identificadores literales del contrato** y no se califican ni se traducen, y las enumeraciones del conjunto cerrado de estados, donde el atributo enunciado ya fija el referente.

## 5. Localización

**Esta capa no localiza nada.** Política, en tres reglas:

1. **Los códigos son identificadores estables**, en mayúsculas y sin acentos, y **no se traducen nunca**. Son parte de la superficie pública: renombrar uno rompe la compilación de quien lo consume, que es la señal más temprana posible. La §17 de cada caso de uso declara qué cambio es compatible.
2. **El texto que una persona lee no se compone acá.** La traducción a mensaje y a respuesta de protocolo pertenece a `GeometriaFactory-Api` y a la superficie que lo muestra, y está sujeta a la prohibición de §1.4, que no es una recomendación de estilo sino RA-03, regla de nivel producto.
3. **Un solo idioma en el producto v1**: español rioplatense. **Con una excepción de hecho que conviene declarar**: el texto del alumno puede traer separadores decimales de su cultura —una coma en lugar de un punto—, y eso **no es un problema de localización de esta capa** sino un rasgo del dato de entrada. **Qué hace el validador con él está declarado** desde el `PRODUCT-INTAKE` **1.12**, §20.E-8 punto 5: es **error de validación**, con el índice de figura y el campo, y el trabajo **queda en `Borrador`**. Es el escenario `E-8`, y la categoría 02 lo lleva en `Definicion-Contrato-Del-Validador-De-Figuras.md` §6 y en `CU-01` CA-12.

## 6. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Cataloga las **17** condiciones distintas derivadas de la §6 de los diez casos de uso, sobre **19** filas declaradas y **17** filas de tabla, sin ninguna excedente. Declara los siete resultados que **no** son condiciones de error, con la confusión más cara de esta capa; la prohibición de §1.4 sobre los tres secretos y la ruta del almacén, con su contracara de registro del lado del servidor; la taxonomía con **dos categorías vacías** —conflicto de facultad y de alcance— y su motivo, que es el espejo del proyecto de código hermano; la forma de terminación **arranque detenido**, propia de esta capa, y la ausencia declarada de «motivo de resultado»; las tres condiciones que fallan hacia el lado seguro, con el atajo tentador de cada una y por qué el atajo es peor; y la ausencia declarada de `CU-09` del catálogo. |
| 1.1 | 2026-08-10 | Ronda 2 de auditoría: correcciones de `SDD/Docs/Audit/B-02-03-GeometriaFactory-Infrastructure-r1.md` contra el `PRODUCT-INTAKE` **1.12**. **H-01**: el punto 3 de §5 decía que qué hace el validador con el separador decimal de la cultura del alumno era un punto abierto de la categoría 02; pasa a declarar el resultado que el intake 1.12 fija en §20.E-8 punto 5 —error de validación con índice de figura y campo, y el trabajo en `Borrador`— y a remitir a `Definicion-Contrato-Del-Validador-De-Figuras.md` §6 y a `CU-01` CA-12. **H-04**: la entrada `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO` de §3.2 nombra dónde queda registrado el punto abierto que declara. **H-02**: la trazabilidad upstream cita el `PRODUCT-INTAKE` **1.12**. Las 17 condiciones, sus recuentos de §7.1 a §7.4 y la taxonomía no cambian. |

## 7. Cobertura y trazabilidad

### 7.1 Recuento

| Magnitud | Valor |
| --- | --- |
| Casos de uso de los que deriva el catálogo | **10** (CU-01 a CU-10) |
| Casos de uso **con** condiciones declaradas | **9**. `CU-09` no tiene ninguna, y su ausencia está declarada en §2.5 |
| Filas de condición declaradas en la §6 de los diez casos de uso | **19** |
| Condiciones declaradas en más de un caso de uso | **1** (`ALMACEN_NO_DISPONIBLE`, en CU-03, CU-04 y CU-05, siempre con la misma causa) |
| Reapariciones, sobre esa una | **2** |
| **Condiciones distintas catalogadas** | **17** |
| Filas de tabla en §3 | **17. Ninguna excedente**: no hay ningún código con causas opuestas según el camino |
| Condiciones inventadas por esta categoría | **0** |
| Condiciones de los casos de uso sin entrada en el catálogo | **0** |
| Resultados declarados que **no** son condiciones, reunidos en §1.2 | **7**, ninguno de ellos condición de este catálogo |

Cuadre: **17 + 2 = 19**.

### 7.2 Verificación mecánica de cobertura

La verificación se hizo en las dos direcciones, caso de uso por caso de uso, y su resultado se deja escrito para que una revisión posterior la pueda repetir sin rehacerla:

| CU | Filas en su §6 | Entradas nuevas en §3 | Condiciones ya catalogadas que reaparecen | Suma |
| --- | --- | --- | --- | --- |
| CU-01 | 2 | 2 | 0 | 2 |
| CU-02 | 1 | 1 | 0 | 1 |
| CU-03 | 4 | 4 | 0 | 4 |
| CU-04 | 2 | 1 | 1 (`ALMACEN_NO_DISPONIBLE`) | 2 |
| CU-05 | 3 | 2 | 1 (`ALMACEN_NO_DISPONIBLE`) | 3 |
| CU-06 | 2 | 2 | 0 | 2 |
| CU-07 | 1 | 1 | 0 | 1 |
| CU-08 | 2 | 2 | 0 | 2 |
| **CU-09** | **0** | **0** | **0** | **0** |
| CU-10 | 2 | 2 | 0 | 2 |
| **Total** | **19** | **17** | **2** | **19** |

Las dos comprobaciones que cierran la verificación:

- **De caso de uso a catálogo.** Ninguna de las 19 filas quedó sin entrada: 17 dieron entrada nueva y 2 son reapariciones de una condición ya catalogada, anotadas con su caso de uso adicional.
- **De catálogo a caso de uso.** Ninguna de las 17 entradas de §3 existe sin una fila que la respalde en la §6 del caso de uso que la titula. **No hay ninguna condición inventada**, y en particular **no se agregó ninguna a partir de los flujos alternativos**: se recorrieron los flujos alternativos de los diez casos de uso y **ninguno cita un código**, porque todos terminan en un resultado. Los siete resultados que eso produce están reunidos en §1.2 y **no entran en ningún recuento**.

### 7.3 Tabla de cobertura

| Código | CU que lo declara | Regla de negocio | Categoría | Forma de terminación |
| --- | --- | --- | --- | --- |
| `TEXTO_ORIGINAL_AUSENTE` | CU-01 | — | Entrada inválida | Negativa sin escritura |
| `INTERPRETACION_NO_DISPONIBLE` | CU-01 | RN-08 (como garantía: el texto queda intacto) | Error transitorio | Terminación degradada |
| `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO` | CU-02 | — | Conflicto de estado | Negativa sin escritura |
| `CONSULTA_SIN_ALCANCE_DECLARADO` | CU-03 | RN-03, RN-11 | Entrada inválida | Negativa sin escritura |
| `ESCRITURA_QUE_REESCRIBE_EL_TEXTO_ORIGINAL` | CU-03 | RN-08 | Entrada inválida | Negativa sin escritura |
| `ESCRITURA_CONCURRENTE_RECHAZADA` | CU-03 | — | Error transitorio | Terminación degradada |
| `ALMACEN_NO_DISPONIBLE` | CU-03, CU-04, CU-05 | — | Error transitorio | Terminación degradada |
| `RETIRO_PARCIAL_NO_ADMITIDO` | CU-04 | RN-07, RN-04 | Entrada inválida | Negativa sin escritura |
| `CORREO_YA_REGISTRADO` | CU-05 | RN-02 | Conflicto de estado | Negativa sin escritura |
| `UNICIDAD_DE_ADMINISTRADOR_VIOLADA` | CU-05 | RN-01 | Conflicto de estado | Negativa sin escritura |
| `CONTRASENA_EN_CLARO_AUSENTE` | CU-06 | — | Entrada inválida | Negativa sin escritura |
| `CREDENCIAL_DERIVADA_ILEGIBLE` | CU-06 | — | Error interno | Negativa sin escritura |
| `FUENTE_DE_ALEATORIEDAD_NO_DISPONIBLE` | CU-07 | **RN-14** | Error transitorio | Terminación degradada |
| `CLAVE_DE_FIRMA_AUSENTE` | CU-08 | — | Recurso ausente | Negativa sin escritura |
| `RECLAMOS_INCOMPLETOS` | CU-08 | — | Entrada inválida | Negativa sin escritura |
| `MIGRACION_NO_APLICABLE` | CU-10 | — | Conflicto de estado | Arranque detenido |
| `RUTA_DEL_ALMACEN_NO_DISPONIBLE` | CU-10 | — | Error transitorio | Arranque detenido |

Tres notas sobre las columnas, para que nadie las complete con atribuciones inventadas:

| Caso | Situación |
| --- | --- |
| Columnas de regla con guion | **Diez de las diecisiete no tienen regla de negocio detrás, y es correcto.** Esta capa provee mecanismos, y un mecanismo tiene precondiciones que ninguna regla de negocio enuncia: que llegue el texto, que llegue la contraseña, que esté la clave. Inventarles una regla sería el defecto contrario al que este catálogo evita |
| `RN-09` sin condición que la haga cumplir por rechazo | No la tiene, y no es un hueco: su tramo principal está en esta capa pero se ejerce **produciendo** el mensaje ubicado, no rechazando nada. Su verificación vive en `CU-01` CA-04 |
| `RN-14` con una sola condición | `FUENTE_DE_ALEATORIEDAD_NO_DISPONIBLE` es lo único que esta capa **rechaza** por esa regla. Lo que la regla exige de verdad —las dos propiedades del valor— no se rechaza: **se produce**, y se verifica en `CU-07` CA-01 a CA-04 |

### 7.4 Trazabilidad del artefacto

**Quick-start: no aplicable en este documento, y el motivo es explícito.** Este artefacto es del modo **reference** y se consulta por código, no se recorre de principio a fin: no hay una secuencia de pasos que produzca un primer resultado. El quick-start del proyecto de código es único y vive en [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §3, con su compromiso de verificación por punto de control, y su recorrido guiado en [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md). Duplicarlo acá crearía una segunda fuente de verdad sobre pasos ejecutables. **No se da por cumplido: se declara no aplicable.**

| Dimensión | Referencia |
| --- | --- |
| Rol de intervención | Implementador de adaptadores, mantenedor de la capa y **operador del despliegue**, que acá sí existe ([`DX-Developer-Experience.md`](DX-Developer-Experience.md) §1.1) |
| Superficie pública que se documenta | Las 17 condiciones de error de los diez contratos, y la frontera entre mecanismo y decisión de `Especificacion-Funcional.md` §4 |
| CU origen | CU-01 a CU-10, §6 de cada uno. **`CU-09` no declara ninguna** |
| Reglas de negocio relevantes | RN-01 a RN-15 de `GeometriaFactory-Domain`, con la correspondencia de §7.3. **Tres tienen su tramo principal en esta capa**: RN-08, RN-09 y RN-14 |
| Necesidades de negocio | NB-01 a NB-09, las nueve. La correspondencia está en `Especificacion-Funcional.md` §7.1 |
| Wireframes asociados | N/A. `tiene_ui_final` == false |
| US a generar en 06 | US del catálogo mantenido junto al código; US de las tres condiciones que fallan hacia el lado seguro, **con el atajo prohibido como criterio de aceptación**; US de la prohibición de §1.4, con inspección del registro del servidor |
| Tests previstos en 08 | Una prueba por condición. Las de CU-01, CU-02, CU-06, CU-07 y CU-08, **unitarias y sin almacén**; las de CU-03, CU-04, CU-05 y CU-10, de integración contra el almacén real. Y una inspección de que ningún mensaje contiene la clave de firma, una contraseña, una provisoria, la ruta del almacén ni el texto del alumno |
| Catálogo de diseño aplicado | N/A para variante DX |
| Configuración dirigida por esquema, primer arranque, acceso de operador único, identidad de versión | N/A. **La preparación del almacén de CU-10 no es la extensión de primer arranque**: acá es un contrato de uso, y la superficie de aprovisionamiento, si la hubiera, viviría en la categoría 03 de la pieza pública |
| Validación visual de maqueta y línea de base | N/A. `requiere_maqueta` == false |
