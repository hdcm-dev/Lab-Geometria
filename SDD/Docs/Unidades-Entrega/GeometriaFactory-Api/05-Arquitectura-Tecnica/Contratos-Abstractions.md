# Contratos y abstracciones — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** Contratos-Abstractions.md
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

**Cinco de las once secciones son comunes; las otras seis existen en una sola capa, y son
complementarias en lugar de redundantes:** `GeometriaFactory-Application` aporta las **operaciones**
—la cara de arriba—, los **puertos** —la cara de abajo— y las cuatro comprobaciones que corren contra
cada operación; `GeometriaFactory-Infrastructure` aporta **qué esquema de datos cruza cada frontera**;
y el host aporta los **elementos de datos** de su superficie.

**Puestas juntas se lee el contrato entero de un extremo al otro**, que es lo que ninguna de las tres
permitía por separado. `GeometriaFactory-Domain` no aparece acá **y es correcto**: no declara
contratos hacia afuera, y su ausencia se declara en lugar de omitirse.

---

## 1. Alcance del contrato

### 1.1 `GeometriaFactory-Api`

Este documento declara **qué expone `GeometriaFactory-Domain` a sus dos consumidores** —`GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`, que lo referencian por proyecto de código (`PRODUCT-INTAKE` §14)— y con qué compromisos.

Los casos de uso que se materializan a través de este contrato son los **trece** de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3, sin excepción: cada uno describe un contrato de uso de esta superficie y no un flujo de pantallas.

**Este contrato no cruza ninguna frontera de proceso.** Los datos que salen del proceso son los del ensamblado de tipos de transferencia, que es otro proyecto de código y tiene su propio contrato. La duplicación aparente entre las entidades de acá y esos tipos es deliberada (`PRODUCT-INTAKE` §17.1.P.12 · GeometriaFactory-Domain).

### 1.2 `GeometriaFactory-Application`

Este documento declara **qué expone `GeometriaFactory-Application` y a quién**, y con qué compromisos. La superficie es **de dos caras**, y ésa es la particularidad que hay que entender antes que nada:

- **Hacia arriba**, expone sus **once** casos de uso a `GeometriaFactory-Api`.
- **Hacia abajo**, expone los **cuatro** puertos que `GeometriaFactory-Infrastructure` implementa. La dependencia se invierte: esta capa declara lo que necesita y otra lo provee (`PRODUCT-INTAKE` §14 y §17.1.P.1 · GeometriaFactory-Application).

Los casos de uso que se materializan a través de este contrato son los once de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §5, sin excepción: cada uno describe un contrato de uso de esta superficie y no un flujo de pantallas.

**Este contrato no cruza ninguna frontera de proceso.** Los datos que salen del proceso son los tipos de transferencia de `GeometriaFactory-Contracts`, que es otro proyecto de código y tiene su propio contrato.

### 1.3 `GeometriaFactory-Infrastructure`

Este documento declara **qué expone `GeometriaFactory-Infrastructure` y a quién**, y con qué compromisos. La superficie tiene una particularidad que hay que entender antes que nada: **es de una sola cara y de tres clases distintas**.

- **Cuatro adaptadores** que implementan contratos **que declara otro proyecto de código**: los puertos de `GeometriaFactory-Application`. Esta capa **no los define**: los cumple.
- **Dos mecanismos** que no son puertos de nadie y que esta capa **sí define**: credenciales y acceso firmado.
- **Una responsabilidad de arranque** que no es puerto ni mecanismo: dejar el almacén en condiciones antes de la primera petición.

**El único consumidor es la composición de raíz de `GeometriaFactory-Api`.** Nadie más referencia este proyecto de código: así lo declara el intake §14 y así lo refleja el grafo de dependencias del `PRODUCT-MANIFEST` §3.

Los casos de uso que se materializan a través de este contrato son los **diez** de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §5, sin excepción: cada uno describe un contrato de uso de esta superficie y no un flujo de pantallas.

**Este contrato no cruza ninguna frontera de proceso.** Los datos que salen del proceso son los tipos de transferencia de `GeometriaFactory-Contracts`, que es otro proyecto de código y tiene su propio contrato.

## 2. Formato

### 2.1 `GeometriaFactory-Api`

**Contrato de superficie de biblioteca, declarado en prosa estructurada.** No hay descripción formal de servicio, ni esquema de mensajes, ni definición de procedimiento remoto: no hay protocolo que describir.

**Los nombres de tipos, de operaciones y de espacios de nombres no se fijan acá.** El intake los declara abiertos y los ata al punto de control de la etapa `a` (`PRODUCT-INTAKE` §17.1.P.11 · GeometriaFactory-Domain); este documento nombra los elementos en lenguaje de dominio, igual que hace [`../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md`](../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md).

### 2.2 `GeometriaFactory-Application`

**Contrato de superficie de biblioteca, declarado en prosa estructurada.** No hay descripción formal de servicio, ni esquema de mensajes, ni definición de procedimiento remoto: no hay protocolo que describir (`PRODUCT-INTAKE` §17.1.P.3 · GeometriaFactory-Application declara «no aplica» hacia afuera del proceso).

**Los nombres de tipos, de operaciones y de espacios de nombres no se fijan acá.** El intake los declara abiertos y los ata al punto de control de la etapa `a`; este documento nombra los elementos en lenguaje de dominio, igual que hacen las categorías 02 y 03 de este proyecto de código. Los **tres** identificadores de puerto que el intake sí declara se transcriben en §4 y son la única cita de identificadores de código de esta cadena.

### 2.3 `GeometriaFactory-Infrastructure`

**Contrato de superficie de biblioteca, declarado en prosa estructurada.** No hay descripción formal de servicio, ni esquema de mensajes, ni definición de procedimiento remoto: el intake declara «no aplica» en comunicación e integración para este proyecto de código, porque **no expone puntos de acceso** (§17.1.P.3 · GeometriaFactory-Infrastructure).

**Los nombres de tipos, de operaciones y de espacios de nombres no se fijan acá.** El intake los ata al punto de control de la etapa `a`; este documento nombra los elementos en lenguaje de dominio, igual que hacen las categorías 02 y 03 de este proyecto de código. Los **tres** identificadores de puerto que el intake sí declara —`IWorkRepository`, `IFigureValidator` e `ISystemClock`— se citan en §3 y son la única cita de identificadores de código de esta cadena; el cuarto **no tiene identificador declarado** y esta categoría no lo inventa ([`ADR-06003`](Adrs/ADR-06003-Comparacion-De-Correos-Y-El-Indice-Que-La-Sostiene.md) §6).

## 3. Operaciones

### 3.1 `GeometriaFactory-Api`

Trece operaciones, una por caso de uso. La columna «Exige resuelto» declara qué tiene que haber resuelto el consumidor **antes** de invocar, que es la contrapartida de [`ADR-02005`](Adrs/ADR-02005-Guarda-Unica-De-Admisibilidad.md) y de [`ADR-02006`](Adrs/ADR-02006-El-Dominio-No-Lee-El-Reloj-Ni-El-Conjunto.md).

| Op | Caso de uso | Qué hace | Exige resuelto por el consumidor | Componente |
| --- | --- | --- | --- | --- |
| OP-01 | CU-02001 | Constituye un alumno con cuenta `Pendiente`, sin credencial derivada y con papel `Alumno` | Unicidad del correo; momento del alta | Núcleo de entidades, Guardas de cuenta |
| OP-02 | CU-02002 | Habilita, bloquea, rehabilita o da de baja una cuenta **de alumno** | Que quien opera es el administrador; el texto de confirmación en la baja; momento | Guardas de cuenta |
| OP-03 | CU-02003 | Fija o reemplaza la credencial derivada de una cuenta | Que la credencial vigente fue verificada; la credencial nueva **ya derivada** | Guardas de cuenta |
| OP-04 | CU-02004 | Responde si la cuenta admite acceso, y con qué motivos si no | Nada: es la puerta de entrada | Evaluador de admisibilidad |
| OP-05 | CU-02005 | Constituye o reedita un trabajo con dueño, identidad propia y texto original íntegro | Pertenencia del trabajo; momento de creación y de última modificación | Núcleo de entidades, Máquina de estados |
| OP-06 | CU-02006 | Adopta el conjunto de piezas y componentes reconstruido, con identidad posicional | La interpretación del texto, hecha afuera; la cantidad de figuras del conjunto raíz | Adopción de la interpretación |
| OP-07 | CU-02007 | Adopta las observaciones del trabajo, comprobando que están bien formadas | La emisión de las observaciones, hecha afuera | Adopción de la interpretación |
| OP-08 | CU-02008 | Resuelve el estado del trabajo en el envío: `Pendiente` si el texto verifica, `Borrador` si no | El resultado de la interpretación; momento | Máquina de estados |
| OP-09 | CU-02009 | Resuelve si un alumno accede a un trabajo y qué puede hacer con él | Nada más que las entidades | Máquina de estados |
| OP-10 | CU-02010 | Aplica el desenlace —aprobar o rechazar— sobre un trabajo en estado `Pendiente`, con comentario opcional | Que quien opera es el administrador; momento | Máquina de estados |
| OP-11 | CU-02011 | Resuelve qué trabajos entran en el alcance del administrador y cuáles puede eliminar | Nada más que las entidades | Máquina de estados |
| OP-12 | CU-02012 | Constituye la única cuenta de administrador, `Habilitado` y con credencial, mientras no exista ninguna | Que no existe ninguna cuenta con papel `Administrador`; la credencial **ya derivada**; momento | Guardas de cuenta |
| OP-13 | CU-02013 | Resetea la contraseña de una cuenta de alumno: fija la provisoria **ya derivada** y pone la marca | Que quien opera es el administrador; la provisoria ya producida y derivada; momento | Guardas de cuenta |

**Trece operaciones sobre trece casos de uso.** OP-11 y OP-09 devuelven predicados y no aplican efecto: el dominio no ejecuta consultas, declara el criterio con el que la consulta se acota.

### 3.2 `GeometriaFactory-Infrastructure`

Las **siete** filas de superficie están, agrupadas por clase y sin agrupar dentro de cada clase. La columna «Exige resuelto» declara qué tiene que haber resuelto el consumidor **antes** de invocar.

### Los cuatro adaptadores de puerto

| Op | Frontera | Qué ofrece | Exige resuelto por el consumidor | CU | ADR |
| --- | --- | --- | --- | --- | --- |
| OP-01 | Puerto de repositorio de trabajos (`IWorkRepository`) | Recuperar un trabajo; resolver una consulta **ya acotada** por dueño o por alcance, en sus **dos** formas —proyección de listado sin texto original, sin componentes y sin comentario, y detalle completo—; materializar el resultado; ejecutar el retiro | El recorte, declarado en el pedido. **Sin recorte no hay consulta**; y la pertenencia y la facultad, ya comprobadas | CU-06003, CU-06004 | ADR-06001, ADR-06002 |
| OP-02 | Puerto de repositorio de cuentas (**sin identificador declarado**) | Recuperar una cuenta por su correo; responder si un correo ya está registrado y si ya existe una cuenta con papel `Administrador`; materializar el resultado **incluida la marca de cambio de contraseña pendiente**; ejecutar el retiro con arrastre | La credencial **ya derivada**, cuando la haya; la facultad, ya comprobada; el correo de confirmación, ya comparado | CU-06005, CU-06004 | ADR-06001, ADR-06003 |
| OP-03 | Puerto de validación de figuras (`IFigureValidator`) | Interpretar el texto original y devolver **tres cosas**: la cantidad de figuras del conjunto raíz, las piezas reconstruidas con su posición y las observaciones con su especie, su posición y su campo | Nada más que el texto. **No recibe identidad, ni estado, ni configuración** | CU-06001, CU-06002 | ADR-06006 |
| OP-04 | Puerto de reloj del sistema (`ISystemClock`) | Devolver el momento actual, en tiempo universal coordinado | Nada | CU-06009 | ADR-06002 |

### Los dos mecanismos

| Op | Mecanismo | Qué ofrece | Exige resuelto por el consumidor | CU | ADR |
| --- | --- | --- | --- | --- | --- |
| OP-05 | Credenciales | Derivar una contraseña; verificar una credencial contra un valor derivado; y **producir la contraseña provisoria** de la habilitación y del reseteo | Para las dos primeras, la contraseña en claro. **Para la tercera, nada: la producción no recibe ningún parámetro** | CU-06006, CU-06007 | ADR-06004, ADR-06005 |
| OP-06 | Acceso firmado | Emitir un acceso con sus **cuatro** reclamos —identificador, correo, papel y expiración— y verificar uno recibido | Los cuatro reclamos, **completos**; la admisibilidad de la cuenta, **ya resuelta**: una cuenta que no admite acceso no llega acá | CU-06008 | ADR-06004 |

### La responsabilidad de arranque

| Op | Responsabilidad | Qué ofrece | Exige resuelto por el consumidor | CU | ADR |
| --- | --- | --- | --- | --- | --- |
| OP-07 | Preparación del almacén | Crear el almacén si no existe, aplicar el linaje de transformaciones si está desactualizado, y **detener el arranque** antes que operar sobre un almacén en el que no se puede confiar | La ubicación del almacén, provista por configuración. **Esta capa la recibe y no la busca** | CU-06010 | ADR-06007 |

**Siete filas de superficie sobre diez casos de uso**, y la diferencia no es un hueco: `CU-06001` y `CU-06002` comparten la frontera del puerto de validación —son los dos motores de un mismo pipeline—, y `CU-06003` con `CU-06004` y `CU-06005` con `CU-06004` comparten las dos fronteras de repositorio, porque el retiro es una operación más de cada una aunque sea el caso de uso que se verifica por ausencia.

## 4. Elementos de datos

### 4.1 `GeometriaFactory-Api`

### 4.1 Entidades

Las cinco de [`../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md`](../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md) §2, con la semántica y las restricciones conceptuales que ese documento fija y que este contrato no redefine.

| Entidad | Qué expone | Qué no expone |
| --- | --- | --- |
| Alumno | Identificador, correo, nombre, apellido, papel, estado de cuenta, marca de cambio de contraseña pendiente, fecha de alta | El contenido de la credencial derivada, que es opaca: se comprueba su presencia, nunca su valor |
| Trabajo | Identificador, dueño, nombre, fecha declarada por el alumno, fecha de creación, fecha de última modificación, descripción, texto original, estado, conjunto de piezas, cantidad de figuras del conjunto raíz, observaciones, comentario del administrador | Ninguna operación de escritura libre sobre el estado ni sobre el texto original |
| Pieza | Posición, tipo, área declarada, área derivada, volumen declarado, volumen derivado, componentes | La familia plana o volumétrica, que **se deriva del tipo** y no se guarda |
| Componente | Posición, papel, tipo, dimensiones declaradas, área declarada | Ninguna corrección ni unificación de discriminantes |
| Observación | Especie, posición de pieza, campo, valor declarado, valor derivado | Ninguna relación con el comentario del administrador: no comparten campos |

### 4.2 Conjuntos cerrados

Agregar un valor a cualquiera de estos conjuntos es cambio **menor**; quitarlo es cambio **mayor** ([`ADR-02003`](Adrs/ADR-02003-Versionado-Y-Estabilidad-De-La-Superficie.md) §7).

| Conjunto | Valores | Cantidad |
| --- | --- | --- |
| Papel de la cuenta | `Alumno`, `Administrador` | 2 |
| Estado de cuenta | `Pendiente`, `Habilitado`, `Bloqueado` | 3 |
| Marca de cambio de contraseña pendiente | Puesta, levantada | 2 |
| Estado del trabajo | `Borrador`, `Pendiente`, `Finalizado`, `Rechazado`, con los dos últimos terminales | 4 |
| Especie de observación | Advertencia, error de validación | 2 |
| Desenlace de la revisión | Aprobar, rechazar | 2 |
| Tipo de pieza | `Cilindro`, `Cubo`, `Ortoedro`, `Rectangulo`, `Cuadrado`, `Circulo` | 6 |
| Tipo de componente | `Circulo`, `Cuadrado`, `Rectangulo`, `RectanguloDesarrollado` | 4 |
| Papel del componente | Tapa, cara, base, lateral, lado | 5 |

### 4.3 Resultado de operación

Toda operación que pueda rechazar devuelve un resultado con dos salidas posibles —efecto aplicado, o condición que lo impidió— según [`ADR-02002`](Adrs/ADR-02002-Superficie-Publica-De-Guardas-Y-Resultados-Tipados.md). Tres precisiones de forma:

1. **La admisibilidad devuelve varios motivos**, no uno: una cuenta puede ser no admisible por más de una causa a la vez.
2. **La adopción de la interpretación devuelve una colección de condiciones**, porque un conjunto de piezas puede estar mal formado en más de un lugar.
3. **Las demás operaciones devuelven una sola condición.**

## 5. Manejo de errores

### 5.1 `GeometriaFactory-Api`

- **El conjunto de condiciones es cerrado y su fuente única es la categoría 03**: las **42** condiciones de [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md). Este contrato no acuña ninguna y no las transcribe: las referencia.
- **Códigos reservados.** El catálogo registra **cinco identificadores retirados** —tres por renombre y dos por imposibilidad de su causa—, y ninguno se recicla. Un identificador retirado no vuelve a nombrar otra condición.
- **Sin excepciones para reglas de negocio.** Las excepciones quedan reservadas a defectos de programación del consumidor.
- **Sin texto de presentación.** El dominio devuelve códigos, no mensajes para una persona: la composición del mensaje es de la capa que expone, y la traducción a respuesta de protocolo, de `GeometriaFactory-Api`.
- **Sin dirección de servicio en ninguna condición** (RA-03). Es trivial acá porque el dominio no conoce ninguna, y se declara para que no deje de serlo.

### 5.2 `GeometriaFactory-Application`

- **El conjunto de condiciones es cerrado y su fuente única es la categoría 03**: las **36** de [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md). Este contrato no acuña ninguna y no las transcribe: las referencia ([`ADR-04006`](Adrs/ADR-04006-Resultado-Tipado-Y-Catalogo-Cerrado-De-Condiciones.md)).
- **Resultado tipado, no excepción.** Toda condición prevista viaja como valor de retorno con su código estable. Las excepciones quedan reservadas a defectos de programación del consumidor.
- **Las tres negativas de autorización no se confunden.** La de pertenencia oculta la existencia del recurso y el consumidor la traduce a «no encontrado» y **nunca** a «no autorizado»; la de facultad sí admite ser explícita; la de alcance del administrador es propia y distinta de las dos.
- **Una sola negativa de facultad**, aunque el dominio declare dos códigos para la misma: esta capa corta con su propia verificación antes de invocarlo.
- **Sin texto de presentación.** Esta capa devuelve códigos y, cuando corresponde, índice de figura y campo. La composición del mensaje es de quien expone y la traducción a respuesta de protocolo, de `GeometriaFactory-Api`.
- **Sin dirección de servicio, ruta de datos ni traza en ninguna condición** (`RA-03`). Es trivial acá porque esta capa no conoce ninguna de las tres, y se declara para que no deje de serlo.
- **Las observaciones del trabajo no son condiciones de error de esta capa**: son datos del trabajo, con su especie y su ubicación. Y **el comentario del administrador no es una observación**: no comparten ni un campo.

### 5.3 `GeometriaFactory-Infrastructure`

- **El conjunto de condiciones es cerrado y su fuente única es la categoría 03**: las **17** de [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md). Este contrato no acuña ninguna y no las transcribe: las referencia.
- **Código, no texto y no excepción.** Esta capa emite un código estable de una enumeración cerrada. No produce mensajes para personas, no los formatea y no los traduce.
- **Ningún código es un código de protocolo.** Su traducción pertenece a `GeometriaFactory-Api`, y una sola condición tiene destinatario declarado aguas arriba: `INTERPRETACION_NO_DISPONIBLE`, que `GeometriaFactory-Application` `CU-06005` §6 espera por el puerto de validación.
- **Dos categorías de conflicto están vacías, y no es un hueco**: facultad y alcance. **Esta capa no autoriza** y no recibe la identidad del solicitante para comprobar nada. Quien busque acá una negativa de autorización está buscando en la capa equivocada.
- **Cuatro condiciones son de terminación degradada y dos detienen el arranque.** Esta capa **no reintenta**: reintentar, si corresponde, lo decide el consumidor.
- **Ninguna condición deja efecto parcial.** Todas las escrituras ocurren dentro de una unidad de trabajo que se cierra entera o no se cierra.
- **Ninguna condición lleva un secreto, la ruta del almacén ni el texto del alumno**, y **todas quedan registradas del lado del servidor**. Es `RA-03` ejercida por disciplina y no por ignorancia, porque esta capa **sí conoce** las tres cosas que no puede decir.
- **Siete cosas que parecen fallos y son resultados**, y ninguna tiene entrada en el catálogo: una figura que no se pudo reconstruir, un texto que no se pudo leer ni con la tolerancia, una verificación sin discrepancias, una recuperación que no encontró nada, una consulta con alcance que devuelve el conjunto vacío, una credencial que no coincide y un acceso vencido o con firma que no corresponde.

## 6. Versionado del contrato

### 6.1 `GeometriaFactory-Api`

Aplica el criterio de [`ADR-02003`](Adrs/ADR-02003-Versionado-Y-Estabilidad-De-La-Superficie.md) §7, con estas concreciones sobre los elementos de este contrato:

| Cambio sobre este contrato | Clase |
| --- | --- |
| Quitar o renombrar una operación, o cambiar qué exige resuelto | Mayor |
| Quitar un valor de cualquiera de los nueve conjuntos cerrados de §4.2 | Mayor |
| Quitar un atributo de una entidad, o cambiar su semántica | Mayor |
| Perder un invariante, aunque ninguna firma cambie | Mayor |
| Agregar una operación, un atributo opcional o un valor a un conjunto cerrado | Menor |
| Agregar una condición al catálogo de 03 | Menor |
| Corregir una guarda para que cumpla el invariante que ya declaraba | Parche |

**Compatibilidad hacia atrás.** Los dos consumidores se compilan dentro del mismo artefacto de agrupación, de modo que un cambio incompatible **rompe la compilación antes de romper el tiempo de ejecución**. No hay deprecación gradual ni convivencia de dos versiones: la política es corregir a los dos consumidores en la misma etapa.

### 6.2 `GeometriaFactory-Application`

Aplica el criterio de [`ADR-04003`](Adrs/ADR-04003-Versionado-Y-Estabilidad-De-La-Superficie.md) §7, con estas concreciones sobre los elementos de este contrato:

| Cambio sobre este contrato | Cara | Clase |
| --- | --- | --- |
| Quitar o renombrar una operación, o cambiar su postcondición | Arriba | Mayor |
| Cambiar qué exige resuelto una operación antes de invocarla | Arriba | Mayor |
| Cambiar la columna de una comprobación en la tabla de §5 | Arriba | Mayor |
| Quitar, renombrar o cambiar la firma de una operación de un puerto | Abajo | Mayor |
| **Agregar** una operación a un puerto existente, o agregar un puerto | Abajo | **Mayor** |
| Quitar una condición del catálogo de 03, o reciclar su identificador | Las dos | Mayor |
| Agregar una operación a la cara de arriba | Arriba | Menor |
| Agregar una condición al catálogo de 03 | Las dos | Menor |
| Corregir un orquestador para que ejerza la comprobación que ya declaraba | Ninguna | Parche |

**Compatibilidad hacia atrás.** Los consumidores de las dos caras se compilan dentro del mismo artefacto de agrupación, de modo que un cambio incompatible **rompe la compilación antes de romper el tiempo de ejecución**. No hay deprecación gradual ni convivencia de dos versiones: la política es corregir a los dos lados en la misma etapa.

### 6.3 `GeometriaFactory-Infrastructure`

Aplica el criterio general del producto —versionado semántico, sin publicación en ningún repositorio de paquetes, con una rama y una etiqueta por etapa— con estas concreciones sobre los elementos de este contrato.

| Cambio sobre este contrato | Clase |
| --- | --- |
| Quitar o renombrar una operación de un adaptador, o cambiar su postcondición | Mayor |
| Cambiar qué exige resuelto una operación antes de invocarla | Mayor |
| Cambiar lo que cruza una frontera en la tabla de §4 | Mayor |
| Quitar una condición del catálogo de 17, o reciclar su identificador | Mayor |
| Cambiar la forma de terminación de una condición existente | Mayor |
| **Editar una transformación de esquema ya fusionada** | **Prohibido**, no versionado. Entra una transformación nueva ([`ADR-06007`](Adrs/ADR-06007-Transformaciones-Al-Arrancar-Con-Linaje-Inmutable.md)) |
| Agregar una operación a un adaptador existente, o agregar una condición al catálogo | Menor |
| Agregar una transformación de esquema al linaje | Menor |
| Cambiar los parámetros de la función de derivación de clave | Menor, **porque los parámetros viajan con el valor derivado** ([`ADR-06004`](Adrs/ADR-06004-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md)): las credenciales existentes siguen verificándose |
| Corregir un adaptador para que cumpla lo que ya declaraba | Parche |

**Compatibilidad hacia atrás.** El único consumidor se compila dentro del mismo artefacto de agrupación, de modo que un cambio incompatible **rompe la compilación antes de romper el tiempo de ejecución**. No hay deprecación gradual ni convivencia de dos versiones: la política es corregir los dos lados en la misma etapa.

**La excepción son los datos ya guardados, y es la que importa.** El esquema del almacén **sobrevive al despliegue** y no se recompila: un cambio del modelo lógico que la compilación no detecta se detecta al arrancar, con el linaje de transformaciones, y termina en arranque detenido si no cierra.

## 7. Trazabilidad

### 7.1 `GeometriaFactory-Api`

| Dimensión | Referencia |
| --- | --- |
| CU que lo consumen | CU-02001 a CU-02013, los trece |
| RN que cubre | RN-02001 a RN-02016, las dieciséis, con el reparto de [`Arquitectura-Unidad-Entrega.md`](Arquitectura-Unidad-Entrega.md) §10.2 |
| Invariantes que sostiene | INV-01 a INV-09, los nueve, con el reparto de [`Arquitectura-Unidad-Entrega.md`](Arquitectura-Unidad-Entrega.md) §10.3 |
| ADR que lo gobiernan | ADR-02001, ADR-02002, ADR-02003, ADR-02004, ADR-02005, ADR-02006 |
| Consumidores | `GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`, por referencia de proyecto de código |
| Tests previstos en 08 | Una prueba por operación en su camino de efecto aplicado y al menos una por condición del catálogo; prueba de inspección de la superficie pública contra §4.2 y §5 |

### 7.2 `GeometriaFactory-Application`

| Dimensión | Referencia |
| --- | --- |
| CU que lo consumen | CU-04001 a CU-04011, los **once** |
| CU de dominio que orquesta | Los **trece** de `GeometriaFactory-Domain`, con el reparto de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.4. Ninguno queda sin orquestar |
| RN que cubre | RN-04001 a RN-04016, las **dieciséis**, con el reparto de [`Arquitectura-Unidad-Entrega.md`](Arquitectura-Unidad-Entrega.md) §10.2. **Quince** tienen tramo acá; RN-04014 no |
| Invariantes | INV-01 a INV-09, los **nueve**, con el aporte de esta capa declarado en [`Arquitectura-Unidad-Entrega.md`](Arquitectura-Unidad-Entrega.md) §10.3 |
| ADR que lo gobiernan | ADR-04001, ADR-04002, ADR-04003, ADR-04004, ADR-04005, ADR-04006 |
| Consumidores | `GeometriaFactory-Api`, por la cara de arriba; `GeometriaFactory-Infrastructure`, por la de abajo. Los dos por referencia de proyecto de código |
| Tests previstos en 08 | Una prueba por operación en su camino de efecto aplicado y al menos una por condición del catálogo de 36; matriz comprobación contra prueba para las cuatro negativas; matriz puerto contra doble; prueba del arrastre de la baja como testigo de la unidad de trabajo |

### 7.3 `GeometriaFactory-Infrastructure`

| Dimensión | Referencia |
| --- | --- |
| CU que lo consumen | CU-06001 a CU-06010, los **diez** |
| Puertos que implementa | Los **cuatro** que declara [`GeometriaFactory-Application`](Contratos-Abstractions.md) §4. Ninguno queda sin adaptador y no hay adaptador sin puerto |
| RN que cubre | RN-06001 a RN-06016, las **dieciséis**, con el reparto de [`Arquitectura-Unidad-Entrega.md`](Arquitectura-Unidad-Entrega.md) §10.2. **Catorce** tienen tramo acá; RN-06006 y RN-06010 no. **Tres** lo tienen principal: RN-06008, RN-06009 y RN-06014 |
| Invariantes | INV-01 a INV-09, los **nueve**, con el aporte de esta capa declarado en [`Arquitectura-Unidad-Entrega.md`](Arquitectura-Unidad-Entrega.md) §10.3 |
| ADR que lo gobiernan | ADR-06001 a ADR-06007, las **siete** |
| Consumidores | **Uno solo**: la composición de raíz de `GeometriaFactory-Api`, por referencia de proyecto de código |
| Documentos hermanos | [`Modelo-Datos-Logico.md`](Modelo-Datos-Logico.md), para lo que cruza hacia el almacén; [`Flujo-Ejecucion.md`](Flujo-Ejecucion.md), para lo que ocurre dentro del puerto de validación |
| Tests previstos en 08 | Una prueba por operación en su camino de efecto aplicado y al menos una por condición del catálogo de 17; matriz puerto contra adaptador; batería de 10 casos del validador sin almacén; pruebas de integración contra el almacén real para los dos repositorios y para la preparación |

## 8. Operaciones: la cara de arriba

### 8.1 `GeometriaFactory-Application`

Once operaciones, una por caso de uso. La columna «Exige resuelto» declara qué tiene que haber resuelto el consumidor **antes** de invocar; la columna «Puertos» declara qué frontera consume cada una.

| Op | Caso de uso | Qué hace | Exige resuelto por el consumidor | Puertos | Componente |
| --- | --- | --- | --- | --- | --- |
| OP-01 | CU-04001 | Registra el alta de una cuenta de alumno por auto-registro: correo libre, cuenta `Pendiente` y **sin** credencial | La identidad de quien pide, que acá es anónima por diseño | Reloj, Repositorio de cuentas | Alta de cuentas |
| OP-02 | CU-04002 | Gobierna la cuenta de un alumno: habilitar, bloquear, rehabilitar y dar de baja. **Habilitar y rehabilitar producen además la contraseña provisoria** | Que quien opera es el administrador; el texto de confirmación en la baja; la provisoria **ya producida y ya derivada** | Repositorio de cuentas, Repositorio de trabajos | Gobierno de cuentas |
| OP-03 | CU-04003 | Resuelve el ingreso: admisibilidad de la cuenta con su motivo, fijación de la credencial derivada dentro de la habilitación y su reemplazo por la propia cuenta | La credencial **ya derivada**; para el reemplazo, que la vigente fue verificada afuera | Reloj, Repositorio de cuentas | Ingreso y credencial |
| OP-04 | CU-04004 | Carga y reedita un trabajo propio, con dueño y texto original íntegro, y sólo en `Borrador` | La identidad del solicitante | Reloj, Repositorio de trabajos | Trabajo |
| OP-05 | CU-04005 | Envía el trabajo: interpreta su texto por el puerto, incorpora piezas y observaciones y deja que el dominio resuelva el estado. **Es la única acción de guardado** | La identidad del solicitante | Reloj, Repositorio de trabajos, Validación de figuras | Trabajo |
| OP-06 | CU-04006 | Consulta los trabajos propios del alumno: listado acotado al dueño y **sin componentes**, y detalle con desenlace y comentario | La identidad del solicitante | Repositorio de trabajos | Consulta |
| OP-07 | CU-04007 | Revisa los trabajos de la comisión: listado **sin borradores**, con dueño para agrupar y filtrar, y detalle equivalente al del alumno | Que quien opera es el administrador | Repositorio de trabajos, Repositorio de cuentas | Consulta |
| OP-08 | CU-04008 | Da desenlace a un trabajo: aprobar o rechazar desde estado `Pendiente`, con comentario opcional y terminalidad | Que quien opera es el administrador | Reloj, Repositorio de trabajos | Desenlace |
| OP-09 | CU-04009 | Elimina un trabajo, con los **dos alcances opuestos**: el alumno sólo en `Borrador`, el administrador en todo lo que ve | La identidad y el papel del solicitante | Repositorio de trabajos | Trabajo |
| OP-10 | CU-04010 | Configura la cuenta de administrador: única, con papel `Administrador`, `Habilitado` y con credencial, **sólo mientras no exista ninguna** | La credencial **ya derivada** | Reloj, Repositorio de cuentas | Alta de cuentas |
| OP-11 | CU-04011 | Resetea la contraseña de un alumno: fija la provisoria, la devuelve una vez y pone la marca, **conservando la cuenta, su estado cualquiera sea y todos sus trabajos** | Que quien opera es el administrador; la provisoria **ya producida y ya derivada** | Reloj, Repositorio de cuentas | Gobierno de cuentas |

**Once operaciones sobre once casos de uso.** OP-06 y OP-07 no aplican efecto: devuelven proyecciones ya acotadas por el predicado que la propia operación entrega al puerto.

## 9. Puertos: la cara de abajo

### 9.1 `GeometriaFactory-Application`

Los cuatro son los de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3, y este contrato no los redefine: declara qué se les pide y qué operaciones los consumen.

| Puerto | Identificador declarado | Qué le pide esta capa | Operaciones que lo consumen |
| --- | --- | --- | --- |
| Repositorio de trabajos | `IWorkRepository` | Recuperar un trabajo, resolver una consulta **ya acotada** por dueño o por alcance, materializar el resultado y ejecutar el retiro. Ofrece **dos** formas de lectura: la proyección de listado —sin texto original, sin componentes y sin comentario— y el detalle completo | OP-02, OP-04, OP-05, OP-06, OP-07, OP-08, OP-09 |
| Validación de figuras | `IFigureValidator` | Interpretar el texto original y devolver **la cantidad de figuras del conjunto raíz**, las piezas reconstruidas y las observaciones, con su especie y su ubicación | OP-05 |
| Reloj del sistema | `ISystemClock` | Los sellos de alta, de modificación y de desenlace, **para que sean verificables en prueba** | OP-01, OP-03, OP-04, OP-05, OP-08, OP-10, OP-11 |
| Repositorio de cuentas | **Sin identificador declarado**, ver [`ADR-04002`](Adrs/ADR-04002-Cuatro-Puertos-Y-La-Frontera-Que-Declaran.md) §2 | Recuperar una cuenta por su correo, responder si un correo ya está registrado y si ya existe una cuenta con papel `Administrador`, y materializar el resultado, **incluida la marca de cambio de contraseña pendiente** | OP-01, OP-02, OP-03, OP-07, OP-10, OP-11 |

**Dos precisiones sobre lo que viaja por los puertos**, tomadas de la categoría 02 y no redefinidas acá:

1. **Los sellos de alta, de modificación y de desenlace son metadatos de orquestación de esta capa**, distintos de la «Fecha» que el alumno declara en su trabajo. El modelo del dominio no los declara como atributos y la discrepancia está elevada al Product Owner.
2. **La cantidad de figuras del conjunto raíz la produce el validador**, incluidas las figuras que no pudo reconstruir, y **no es derivable de las piezas adoptadas**, que admiten huecos. El dominio la exige como precondición de la reconstrucción, de modo que OP-05 —único consumidor del puerto de validación— es quien la hace viajar.

**Nada más cruza estas cuatro fronteras.** En particular, la **producción** de la contraseña provisoria no abre puerto: el valor llega a esta capa ya producido y ya derivado, del mismo lado desde el que llega la contraseña que el alumno elige.

## 10. Las cuatro comprobaciones contra cada operación

### 10.1 `GeometriaFactory-Application`

Las once filas están, sin agrupar. Son las comprobaciones de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §4, ejercidas en el orden fijo de [`ADR-04004`](Adrs/ADR-04004-Orden-Fijo-De-Las-Cuatro-Comprobaciones.md): la cuarta primero, después pertenencia, después facultad, después alcance.

| Op | Cambio de contraseña pendiente | Pertenencia | Facultad | Alcance del administrador |
| --- | --- | --- | --- | --- |
| OP-01 | Sí | — | — | — |
| OP-02 | Sí | — | **Sí** | — |
| OP-03 | Sí, **con la única excepción declarada**: el reemplazo de FA-05, que es lo que la levanta | — | — | — |
| OP-04 | Sí | **Sí** | — | — |
| OP-05 | Sí | **Sí** | — | — |
| OP-06 | Sí | **Sí** | — | — |
| OP-07 | Sí | — | **Sí** | **Sí** |
| OP-08 | Sí | — | **Sí** | **Sí** |
| OP-09 | Sí | **Sí** | — | **Sí** |
| OP-10 | Sí | — | — | — |
| OP-11 | Sí | — | **Sí** | — |

**La primera columna es «Sí» en las once, y ésa es exactamente la propiedad que `INV-09` exige**: una cuenta con la marca puesta no ejerce ninguna capacidad, ni siquiera las que su papel y su pertenencia admitirían. La única celda con excepción es la de OP-03, y la excepción está acotada al reemplazo de la propia credencial.

## 11. Esquemas de datos: qué cruza cada frontera

### 11.1 `GeometriaFactory-Infrastructure`

**Nada cruza estas fronteras que no esté en esta tabla.** En particular, la **producción** de la contraseña provisoria no abre puerto nuevo: el valor sale por el mecanismo y llega a la capa de aplicación **ya producido y ya derivado**, del mismo lado desde el que llega la contraseña que el alumno elige.

| Frontera | Entra | Sale | Lo que **nunca** cruza |
| --- | --- | --- | --- |
| Repositorio de trabajos | Identidad de un trabajo, o un pedido de consulta **con su recorte**; entidades a materializar | Trabajo completo, proyección de listado, o nada encontrado | El conjunto completo de trabajos de la comisión: no hay operación que lo devuelva |
| Repositorio de cuentas | Correo, identidad de cuenta, entidades a materializar | Cuenta con su estado, su papel y **su marca**; respuesta de las dos preguntas sobre el conjunto | La contraseña en claro, y el valor derivado hacia arriba de esta frontera **en ningún caso salvo el que la propia verificación consume** |
| Validación de figuras | **Sólo el texto original** | Cantidad de figuras del conjunto raíz, piezas y observaciones | El estado del trabajo: el motor no lo decide. Y ninguna petición de red sale de esta frontera |
| Reloj del sistema | Nada | Un momento | Nada más: es el contrato más corto de la capa, y que sea trivial es la prueba de que la inversión está bien hecha |
| Credenciales | Contraseña en claro, o valor derivado a verificar, o nada | Valor derivado, veredicto, o provisoria en claro **una sola vez** | La provisoria hacia una traza o un registro; la contraseña en claro hacia adentro del producto |
| Acceso firmado | Cuatro reclamos, o un acceso a verificar | Acceso firmado, o veredicto | La clave de firma, ni una parte de ella |
| Preparación del almacén | Ubicación del almacén | Almacén preparado, o arranque detenido | La ruta del almacén, dentro de cualquier mensaje |

**Dos precisiones tomadas de la categoría 02 y no redefinidas acá:**

1. **La cantidad de figuras del conjunto raíz la produce el validador**, incluidas las figuras que no pudo reconstruir, y **no es derivable de las piezas**, que admiten huecos. El dominio la exige como precondición de la reconstrucción.
2. **Los sellos de creación y de última modificación son metadatos que produce el consumidor por el puerto de reloj**, distintos de la `Fecha` que el alumno declara en su trabajo. Los tres tiempos no se confunden (`RC-06006`).

## 12. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 2.0 | 2026-08-16 | **Consolidación de la fusión** (`Audit/Migracion-M10-Consolidacion-Fusion.md` 1.2 §4). Pasa de ser el documento de un proyecto de código a ser el de la **unidad de entrega**, con una subsección por proyecto y su texto transpuesto **sin reescritura**. Entra **§0** con lo que sólo se ve con las capas juntas. Los documentos absorbidos quedan archivados en `_legacy/2026-08-16-consolidacion-m10/`. Sube **major**. |
