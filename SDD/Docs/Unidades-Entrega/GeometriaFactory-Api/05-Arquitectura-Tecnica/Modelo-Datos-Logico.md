# Modelo lógico de datos — GeometriaFactory-Infrastructure

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** Modelo-Datos-Logico.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)

---

## Tabla de contenido

- [1. Alcance, y por qué este documento existe en un `library`](#1-alcance-y-por-qué-este-documento-existe-en-un-library)
- [2. Las cinco tablas](#2-las-cinco-tablas)
  - [2.1 Cuenta](#21-cuenta)
  - [2.2 Trabajo](#22-trabajo)
  - [2.3 Pieza](#23-pieza)
  - [2.4 Componente](#24-componente)
  - [2.5 Observación](#25-observación)
- [3. Índices](#3-índices)
- [4. Restricciones](#4-restricciones)
- [5. Transformación inicial del esquema](#5-transformación-inicial-del-esquema)
- [6. Estrategia de partición por instancia](#6-estrategia-de-partición-por-instancia)
- [7. Trazabilidad](#7-trazabilidad)
- [8. Puntos abiertos](#8-puntos-abiertos)
- [9. Control de cambios](#9-control-de-cambios)

---

## 1. Alcance, y por qué este documento existe en un `library`

Describe el **esquema físico** de lo que el producto guarda: qué tablas hay, con qué tipos, con qué índices, con qué restricciones y con qué transformación inicial. Su origen conceptual es [`../02-Especificacion-Funcional/Modelo-Datos/Modelo-Conceptual.md`](../02-Especificacion-Funcional/Modelo-Datos/Modelo-Conceptual.md), entidad por entidad, y **no lo redefine**: lo materializa. Cuando los dos digan cosas distintas, manda el modelo del dominio, que es el que aquél a su vez materializa.

**Emitirlo es un apartamiento declarado de la guía del tipo, con el mismo fundamento con el que la categoría 02 emitió su modelo conceptual.** La guía de esta categoría omite el modelo lógico para «`library` puro **sin estado**», y este proyecto de código no lo es: el `PRODUCT-MANIFEST` §5 declara `tiene_persistencia` en true acá, y el intake declara la persistencia «la responsabilidad central del proyecto de código» (§17.1.P.4 · GeometriaFactory-Infrastructure). Omitirlo dejaría al producto **sin ningún documento que describa el esquema del dato guardado**: el otro proyecto de código con el flag en true es `GeometriaFactory-Api`, que delega en éste y sólo toma de configuración la ruta y dispara la preparación al arrancar.

**Los nombres de tablas y de columnas de este documento son de lenguaje de dominio**, igual que en el resto de la cadena: los identificadores definitivos se anclan en el punto de control de la etapa `a`, con los demás nombres de tipos. Lo que sí es definitivo es la **forma**: qué columnas hay, de qué tipo, con qué nulabilidad, con qué índice y con qué restricción.

## 2. Las cinco tablas

Cinco tablas y **cuatro** relaciones, las mismas del modelo conceptual. Los tipos físicos se nombran por su naturaleza —texto, entero, real, momento, valor de verdad, identidad— y no por el nombre que les da ningún motor concreto.

### 2.1 Cuenta

Entidad conceptual de origen: **Cuenta** (`Modelo-Conceptual.md` §3.2).

| Columna | Tipo físico | Nulable | Valor por defecto | Notas |
| --- | --- | --- | --- | --- |
| Identidad de la cuenta | Identidad | No | Ninguno | Clave primaria |
| Correo escrito | Texto | No | Ninguno | **Tal como la persona lo escribió.** Es lo que se muestra |
| Correo normalizado | Texto | No | Ninguno | **Derivado del anterior, y nunca editado por separado.** Es lo que decide la identidad ([`ADR-06003`](Adrs/ADR-06003-Comparacion-De-Correos-Y-El-Indice-Que-La-Sostiene.md)) |
| Nombre | Texto | No | Ninguno | Dato del alta |
| Apellido | Texto | No | Ninguno | Dato del alta |
| Papel | Texto | No | Ninguno | Conjunto cerrado de **2** valores, guardado por su **nombre** y no por su posición |
| Estado de cuenta | Texto | No | Ninguno | Conjunto cerrado de **3** valores, guardado por su nombre |
| Credencial derivada | Texto | **Sí** | Ninguno | **Nula mientras la cuenta está `Pendiente`**; toma valor en el acto de habilitación, con la provisoria que el sistema produce (`RN-06016`). Lleva consigo los parámetros con los que se produjo ([`ADR-06004`](Adrs/ADR-06004-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md)) |
| Marca de cambio de contraseña pendiente | Valor de verdad | No | Falso | **Atributo propio, que no es un estado de cuenta** (`RC-06007`) |
| Momento de alta | Momento | No | Ninguno | En tiempo universal coordinado, aportado por el puerto de reloj ([`ADR-06002`](Adrs/ADR-06002-Un-Archivo-Escritor-Unico-Y-Una-Unidad-De-Trabajo-Por-Operacion.md)) |

**No hay columna de momento de última modificación de la cuenta**, y el motivo está en `RC-06006`: el modelo del dominio no la declara y el consumidor no la registra. Sigue como punto abierto del Product Owner.

### 2.2 Trabajo

Entidad conceptual de origen: **Trabajo** (`Modelo-Conceptual.md` §3.3).

| Columna | Tipo físico | Nulable | Valor por defecto | Notas |
| --- | --- | --- | --- | --- |
| Identidad del trabajo | Identidad | No | Ninguno | Clave primaria |
| Dueño | Identidad | No | Ninguno | Clave foránea hacia Cuenta. **Un trabajo sin dueño no es un trabajo** |
| Nombre | Texto | No | Ninguno | Lo escribe el alumno |
| Descripción | Texto | Sí | Ninguno | Lo escribe el alumno |
| Fecha declarada | Texto | No | Ninguno | **La escribe el alumno y no es un sello** (`RC-06006`). Se guarda tal como la escribió y **no se convierte de zona** |
| Texto original | Texto | No | Ninguno | El texto que el alumno pegó, **conservado literal** (`RC-06001`). Se guarda como texto y **no se consulta por su contenido** |
| Cantidad de figuras del conjunto raíz | Entero | Sí | Ninguno | **Incluidas las no reconstruidas.** Es el rango de posiciones válidas (`RC-06002`). Nula mientras el texto no se interpretó |
| Estado | Texto | No | Ninguno | Conjunto cerrado de **4** valores, guardado por su nombre |
| Comentario del administrador | Texto | Sí | Ninguno | **Campo, no entidad, y sin historial** (`RC-06007`) |
| Momento del comentario | Momento | Sí | Ninguno | Nulo mientras no haya comentario |
| Autor del comentario | Identidad | Sí | Ninguno | Clave foránea hacia Cuenta |
| Momento de creación | Momento | No | Ninguno | Sello del sistema, en tiempo universal coordinado (`RC-06006`) |
| Momento de última modificación | Momento | No | Ninguno | Sello del sistema, en tiempo universal coordinado (`RC-06006`) |

**Los tres tiempos son distintos y no se confunden**: la fecha declarada la escribe el alumno, los dos momentos los produce el sistema por el puerto de reloj. Es `RC-06006`, y el intake lo declara con rótulo de decisión del Product Owner.

### 2.3 Pieza

Entidad conceptual de origen: **Pieza** (`Modelo-Conceptual.md` §3.4).

| Columna | Tipo físico | Nulable | Valor por defecto | Notas |
| --- | --- | --- | --- | --- |
| Identidad de la pieza | Identidad | No | Ninguno | Clave primaria |
| Trabajo | Identidad | No | Ninguno | Clave foránea hacia Trabajo |
| Posición en el conjunto raíz | Entero | No | Ninguno | **Es su identidad de dominio, y no se compacta** (`RC-06002`) |
| Tipo | Texto | No | Ninguno | El discriminante que el texto del alumno declara, guardado por su nombre |
| Dimensiones | Texto | No | Ninguno | Las que su tipo requiere, con su nombre y su valor. **No se normalizan por tipo**: el conjunto de dimensiones depende del tipo y el esquema no lo fija |
| Área declarada | Real | Sí | Ninguno | La que el texto trae |
| Área derivada | Real | Sí | Ninguno | La que el producto calcula (`RC-06003`). **Los dos por separado** |
| Volumen declarado | Real | Sí | Ninguno | Nulo en las piezas planas |
| Volumen derivado | Real | Sí | Ninguno | Nulo en las piezas planas |

**La familia plana o volumétrica no se guarda** (`RC-06004`): se deriva del tipo, y guardarla crearía un segundo lugar donde puede decir otra cosa.

### 2.4 Componente

Entidad conceptual de origen: **Componente** (`Modelo-Conceptual.md` §3.5).

| Columna | Tipo físico | Nulable | Valor por defecto | Notas |
| --- | --- | --- | --- | --- |
| Identidad del componente | Identidad | No | Ninguno | Clave primaria |
| Pieza | Identidad | No | Ninguno | Clave foránea hacia Pieza |
| Papel dentro de la pieza | Texto | No | Ninguno | Tapa, cara, base, lateral o lado, tal como el texto lo declara |
| Tipo | Texto | No | Ninguno | Siempre una figura plana |
| Dimensiones | Texto | No | Ninguno | Las que el texto del alumno trae |
| Área declarada | Real | Sí | Ninguno | La que el texto trae |

**Los componentes se persisten pese a su redundancia** —un cubo de lado 3 guarda seis caras idénticas para expresar un solo número— **porque son parte del ejercicio**, y se compensa **no cargándolos nunca en las consultas de listado** (`PRODUCT-INTAKE` §17.1.P.12 · GeometriaFactory-Infrastructure).

### 2.5 Observación

Entidad conceptual de origen: **Observación** (`Modelo-Conceptual.md` §3.6).

| Columna | Tipo físico | Nulable | Valor por defecto | Notas |
| --- | --- | --- | --- | --- |
| Identidad de la observación | Identidad | No | Ninguno | Clave primaria |
| Trabajo | Identidad | No | Ninguno | Clave foránea hacia Trabajo. **No cuelga de la pieza**, y por eso puede designar una posición sin pieza |
| Especie | Texto | No | Ninguno | Conjunto cerrado de **2** valores, guardado por su nombre. Sólo el error de validación impide el paso a estado `Pendiente` |
| Posición de la figura | Entero | No | Ninguno | La ubicación que el mensaje declara (`RN-06009`) |
| Campo | Texto | No | Ninguno | El campo que el mensaje declara (`RN-06009`) |
| Valor declarado | Real | Sí | Ninguno | **Sólo en la advertencia**, y junto con el siguiente (`RC-06003`) |
| Valor derivado | Real | Sí | Ninguno | **Sólo en la advertencia** |

**La observación cuelga del trabajo y no de la pieza**, y ésa es la decisión de modelado que hace posible observar una figura que **no se pudo reconstruir**: designa una posición, que puede no tener pieza.

## 3. Índices

| Índice | Tabla | Columnas | Tipo | Motivación |
| --- | --- | --- | --- | --- |
| IX-01 | Cuenta | Correo normalizado | **Único** | Es **la segunda línea de `RN-06002` e `INV-01`**: la consulta previa del consumidor no es garantía por sí sola. La colisión que la consulta no vio termina en `EMAIL_ALREADY_REGISTERED` ([`ADR-06003`](Adrs/ADR-06003-Comparacion-De-Correos-Y-El-Indice-Que-La-Sostiene.md)) |
| IX-02 | Cuenta | Papel | **Único parcial**, sólo sobre las filas con papel `Administrador` | Sostiene `INV-05` y `RN-06001` en el almacén: la materialización que dejaría dos administradores termina en `ADMINISTRATOR_UNIQUENESS_VIOLATED`. Es **parcial** porque las cuentas de alumno no son únicas por papel |
| IX-03 | Trabajo | Dueño | Compuesto con Estado | Sostiene las **dos** consultas de listado del producto: la del alumno, acotada por dueño; y la del administrador, acotada por estado —sin borradores— y agrupable por dueño. Es el índice del que depende el requerimiento de tiempo del listado |
| IX-04 | Pieza | Trabajo, Posición en el conjunto raíz | **Único compuesto** | Sostiene `RC-06002`: dos piezas del mismo trabajo no pueden ocupar la misma posición, que es la identidad de dominio de la pieza |
| IX-05 | Componente | Pieza | Simple | Sostiene la carga del detalle, y **sólo** del detalle: el listado nunca lo recorre |
| IX-06 | Observación | Trabajo | Simple | Sostiene la carga de las observaciones del detalle |

**Seis índices y ninguno más.** No hay índice sobre el texto original —no se consulta por su contenido— ni sobre los sellos: ninguna consulta del producto ordena ni filtra por ellos.

## 4. Restricciones

| Restricción | Dónde | Qué exige | Origen |
| --- | --- | --- | --- |
| RE-01 | Cuenta | Clave primaria sobre la identidad | Modelo conceptual §3.2 |
| RE-02 | Cuenta | Papel pertenece al conjunto cerrado de **2** valores | `Modelo-Conceptual.md` §4 |
| RE-03 | Cuenta | Estado de cuenta pertenece al conjunto cerrado de **3** valores | `Modelo-Conceptual.md` §4 |
| RE-04 | Cuenta | Correo normalizado único (IX-01) | `RN-06002`, `INV-01`, [`ADR-06003`](Adrs/ADR-06003-Comparacion-De-Correos-Y-El-Indice-Que-La-Sostiene.md) |
| RE-05 | Cuenta | A lo sumo **una** fila con papel `Administrador` (IX-02) | `RN-06001`, `INV-05` |
| RE-06 | Trabajo | Clave primaria sobre la identidad, y clave foránea de dueño hacia Cuenta **con arrastre del retiro** | `RN-06007`, `RC-06005` |
| RE-07 | Trabajo | Estado pertenece al conjunto cerrado de **4** valores | `Modelo-Conceptual.md` §4 |
| RE-08 | Trabajo | El texto original no admite ser reemplazado por uno distinto en una fila existente | `RN-06008`, `RC-06001`, `WRITE_REWRITES_ORIGINAL_JSON` |
| RE-09 | Pieza | Clave foránea hacia Trabajo **con arrastre del retiro**, y posición única por trabajo (IX-04) | `RC-06002`, `RC-06005` |
| RE-10 | Pieza | La posición es mayor o igual que cero y menor que la cantidad de figuras del conjunto raíz del trabajo | `RC-06002` |
| RE-11 | Componente | Clave foránea hacia Pieza **con arrastre del retiro** | `RC-06005` |
| RE-12 | Observación | Clave foránea hacia Trabajo **con arrastre del retiro**; y **ninguna** hacia Pieza | `Modelo-Conceptual.md` §3.1, `RC-06005` |
| RE-13 | Observación | Especie pertenece al conjunto cerrado de **2** valores | `Modelo-Conceptual.md` §4 |
| RE-14 | Observación | El valor declarado y el valor derivado están los dos presentes o los dos ausentes, y sólo están presentes en la advertencia | `RC-06003` |
| RE-15 | Todas | **Ninguna columna de borrado lógico y ninguna de pertenencia a instancia** | `RC-06005`, `INV-05`, `PRODUCT-INTAKE` §17.1.P.4 · GeometriaFactory-Infrastructure |

**Los cuatro arrastres del retiro —RE-06, RE-09, RE-11 y RE-12— son lo que hace que el retiro físico sea comprobable por ausencia**, que es exactamente el criterio con el que `RN-06007` se verifica: no queda ningún trabajo del alumno dado de baja. La unidad de trabajo los cubre a los cuatro de una vez ([`ADR-06002`](Adrs/ADR-06002-Un-Archivo-Escritor-Unico-Y-Una-Unidad-De-Trabajo-Por-Operacion.md)).

## 5. Transformación inicial del esquema

| Aspecto | Definición |
| --- | --- |
| Identificador | **TR-01**, primera transformación del linaje. Se versiona con el código de la etapa `a`, que es la que la introduce |
| Qué hace | Crea las **cinco** tablas de §2, los **seis** índices de §3 y las **quince** restricciones de §4 sobre un almacén inexistente |
| Cuándo se aplica | **Al arrancar**, automáticamente, antes de que el servicio atienda su primera petición ([`ADR-06007`](Adrs/ADR-06007-Transformaciones-Al-Arrancar-Con-Linaje-Inmutable.md)) |
| Herramienta | La del mapeador que el intake ancla en la etapa `a`, instalada como **herramienta local del repositorio** para que su versión quede versionada junto al código (`PRODUCT-INTAKE` §17.1.P.1 · GeometriaFactory-Infrastructure) |
| Inmutabilidad | **Una transformación ya fusionada no se edita.** Si hay que corregirla, entra una nueva. Es la causa frecuente de `MIGRATION_NOT_APPLICABLE` |
| Verificación | Puerta bloqueante del pipeline: **las transformaciones se aplican solas sobre un almacén inexistente**, criterio de aceptación de la etapa `c` (`PRODUCT-INTAKE` §17.1.P.8 · GeometriaFactory-Infrastructure) |
| Qué pasa si no se puede aplicar | **El arranque se detiene.** No se aplica un esquema por aproximación y **no se descarta el almacén** ([`ADR-06007`](Adrs/ADR-06007-Transformaciones-Al-Arrancar-Con-Linaje-Inmutable.md)) |

## 6. Estrategia de partición por instancia

**No aplica, y la ausencia es una decisión declarada y no un pendiente.** El intake lo dice en una línea: «una instancia, un curso, un administrador» (`INV-05`), y el flag `multi_tenant` es **false** en los siete proyectos de código.

La consecuencia sobre este modelo es concreta y verificable: **ninguna de las cinco tablas lleva columna de pertenencia a instancia**, y RE-15 lo declara como restricción. El modelo conceptual ya había dejado escrito que esa columna «no existe y no va a existir». Un despliegue para un segundo curso es un **segundo archivo y un segundo contenedor**, no una columna.

## 7. Trazabilidad

| Tabla | Entidad conceptual de origen | CU de la categoría 02 que la consumen | Reglas conceptuales que materializa |
| --- | --- | --- | --- |
| Cuenta | Cuenta (`Modelo-Conceptual.md` §3.2) | CU-06004, CU-06005 | `RC-06006`, `RC-06007` |
| Trabajo | Trabajo (§3.3) | CU-06003, CU-06004 | `RC-06001`, `RC-06002`, `RC-06005`, `RC-06006`, `RC-06007` |
| Pieza | Pieza (§3.4) | CU-06001, CU-06003, CU-06004 | `RC-06002`, `RC-06003`, `RC-06004`, `RC-06005` |
| Componente | Componente (§3.5) | CU-06001, CU-06003, CU-06004 | `RC-06004`, `RC-06005` |
| Observación | Observación (§3.6) | CU-06001, CU-06002, CU-06003, CU-06004 | `RC-06002`, `RC-06003`, `RC-06005` |

**Las cinco entidades conceptuales tienen tabla y ninguna tabla existe sin entidad conceptual.** Las **siete** reglas conceptuales están todas representadas: `RC-06001` en RE-08, `RC-06002` en IX-04 y RE-10, `RC-06003` en RE-14, `RC-06004` por la **ausencia** de columna de familia, `RC-06005` en los cuatro arrastres y en RE-15, `RC-06006` en las tres columnas de tiempo de Trabajo y en la ausencia de la cuarta en Cuenta, y `RC-06007` en la marca como columna propia y en el comentario como campo sin historial.

| Dimensión | Referencia |
| --- | --- |
| CU que lo consumen | CU-06001 a CU-06005 y CU-06010 de [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/) |
| RN que sostiene en el almacén | RN-06001, RN-06002, RN-06004, RN-06007, RN-06008, RN-06009, RN-06011, RN-06012, RN-06013, RN-06015, RN-06016 |
| Invariantes que sostiene | INV-01, INV-05, INV-07, INV-09 |
| ADR que lo gobiernan | ADR-06002, ADR-06003, ADR-06004, ADR-06007 |
| Tests previstos en 08 | Pruebas de integración contra el almacén real: unicidad del correo con dos capitalizaciones, unicidad del administrador, arrastre completo de la baja, rechazo de la reescritura del texto original, posición única por trabajo, y aplicación de TR-01 sobre almacén inexistente |

## 8. Puntos abiertos

| Punto | Situación | Quién lo cierra |
| --- | --- | --- |
| Los **nombres definitivos** de tablas, columnas e índices | Declarados abiertos aguas arriba y atados al punto de control de la etapa `a`. Lo que este documento fija es la forma, no el identificador | El equipo en la etapa `a` |
| El **momento de última modificación de la cuenta** | El modelo del dominio no lo declara y el consumidor no lo registra; este modelo **no lo incorpora por su cuenta**. Si el Product Owner lo quisiera, entraría por el dominio | Product Owner, y `GeometriaFactory-Domain` |
| La **frecuencia del respaldo** | El intake la declara explícitamente «a definir por el docente». No es omisión de esta categoría | Product Owner, y `09-Devops` |
| La **forma exacta del valor derivado de una credencial**, con sus parámetros | [`ADR-06004`](Adrs/ADR-06004-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md) fija que los parámetros viajan junto al valor; cuál función se ancla se decide en la etapa `a`, y con ella la forma concreta de esa columna | El equipo en la etapa `a` |

## 9. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-29 | **Tramo `R-3c` del renombre `F-03`**, reactivado por el Product Owner el 2026-08-29 y registrado en [`../../../Producto/Norma-De-Nomenclatura.md`](../../../Producto/Norma-De-Nomenclatura.md) §8. **4 línea(s)** pasan los códigos de condición de la forma castellana a la vigente, con el mapeo de **§6.8** —101 pares— y **sin elegir ninguno acá**. Se respeta **§4.1**: no se tocan las filas de control de cambios, ni lo que está entre «…», ni los informes de `Audit/`. **Ninguna palabra de prosa cambia**, verificado con el control de diff del tramo. |
| 1.0 | 2026-08-10 | Emisión inicial del modelo lógico, como **apartamiento declarado** de la guía del tipo `library` con el mismo fundamento con el que la categoría 02 emitió su modelo conceptual. Declara las cinco tablas con sus tipos físicos y su nulabilidad, los seis índices con su motivación —incluido el único sobre la forma normalizada del correo y el único parcial del papel `Administrador`—, las quince restricciones con los cuatro arrastres del retiro, la transformación inicial `TR-01` con su inmutabilidad y su verificación, la ausencia declarada de partición por instancia, la trazabilidad tabla por tabla contra las cinco entidades conceptuales y las siete reglas conceptuales, y cuatro puntos abiertos. |
