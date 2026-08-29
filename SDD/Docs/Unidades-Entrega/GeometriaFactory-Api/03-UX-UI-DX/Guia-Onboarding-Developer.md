# Guía de onboarding — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** Guia-Onboarding-Developer.md
**Versión:** 2.1
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

**Cinco de las nueve secciones son comunes; las otras cuatro son el atajo propio de cada capa**, y son
exactamente lo que un desarrollador nuevo necesita y ninguna guía sola le daba:

| Sección | Sólo en |
| --- | --- |
| Las tres cosas que no fallan | `GeometriaFactory-Api` |
| Dónde va una regla nueva | `GeometriaFactory-Domain` |
| La inversión de dependencias, en la práctica | `GeometriaFactory-Application` |
| Los tres atajos que no fallan | `GeometriaFactory-Infrastructure` |

---

## 1. Audiencia y prerrequisitos

### 1.1 `GeometriaFactory-Api`

Esta guía es para quien va a tocar la superficie HTTP del producto: agregar o cambiar un punto de acceso, traducir un motivo nuevo, o escribir el cliente que la consume. Se supone que ya escribió servicios HTTP; **no** se supone que conozca este producto.

**Lectura obligatoria antes del paso 1**, y son dos:

1. [`../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md`](../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md) **§2 y §3, en ese orden**. §2 dice qué de la superficie está declarado por una fuente y qué es propuesta; §3 es la tabla de los quince puntos. **Leer §3 sin §2 hace creer que las quince rutas están decididas, y catorce no lo están.**
2. [`DX-Developer-Experience.md`](DX-Developer-Experience.md) **§1.4**, dos párrafos: las dos reglas del producto que se rompen desde acá **sin que nada falle**.

Lo que **no** hace falta leer para la primera hora: los doce casos de uso completos. Se consultan por tarea, cuando toque una.

### 1.2 `GeometriaFactory-Domain`

Esta guía está escrita para dos lectores concretos, y no para un integrador hipotético:

- **El mantenedor que vuelve sobre este proyecto de código sin el contexto de la etapa en que lo escribió.** Es el caso más frecuente en un equipo de una persona.
- **El agente de IA que construye por etapas** y que necesita, en cada arranque, reconstruir por qué una guarda existe antes de tocarla.

Los dos escriben además `GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`, que son los únicos consumidores de esta superficie pública.

Prerrequisitos:

| Prerrequisito | Detalle |
| --- | --- |
| El repositorio abierto en el entorno de desarrollo contenido del propio repositorio | Todo el ciclo ocurre adentro. El host no tiene las herramientas y no va a tenerlas (`Alcance-Producto.md` §4.4) |
| Nada más | Sin base de datos, sin red, sin servicio levantado y sin credencial de acceso. Este proyecto de código no tiene dependencias (`PRODUCT-INTAKE` §17.1.P.1 · GeometriaFactory-Domain) y su persistencia está declarada como «no aplica» (§17.1.P.4 · GeometriaFactory-Domain) |

Conocimiento previo que **no** se supone: el estilo de modelo de dominio con invariantes explícitas. Es lo que la §7 de esta guía enseña.

Vocabulario mínimo para no perderse en la primera media hora. Los términos están definidos en `00-Contexto/Vision-Producto.md` §9, que es el glosario raíz, y en `02-Especificacion-Funcional/Glosario-Funcional.md`; acá sólo se enumeran para que el lector sepa qué buscar: alumno, trabajo, pieza, componente, observación, advertencia, error de validación, estado de cuenta, **camino de alta**, estado del trabajo, credencial derivada, texto original, desenlace y comentario.

Una convención que conviene conocer desde el primer minuto porque se cruza en cada documento: **`Pendiente` va siempre calificado**, «cuenta `Pendiente`» o «trabajo en estado `Pendiente`», porque nombra dos estados distintos (`Vision-Producto.md` §9.2).

### 1.3 `GeometriaFactory-Application`

Esta guía está escrita para tres lectores concretos, y no para un integrador hipotético:

- **El mantenedor que vuelve sobre este proyecto de código sin el contexto de la etapa en que lo escribió.** Es el caso más frecuente en un equipo de una persona.
- **El agente de IA que construye por etapas** y que necesita, en cada arranque, reconstruir por qué una negativa existe antes de tocarla.
- **Quien escribe una de las dos capas vecinas**: `GeometriaFactory-Api`, que invoca los casos de uso, o `GeometriaFactory-Infrastructure`, que implementa los puertos. Son los únicos consumidores de esta superficie pública, y no hay integradores externos.

Prerrequisitos:

| Prerrequisito | Detalle |
| --- | --- |
| El repositorio abierto en el entorno de desarrollo contenido del propio repositorio | Todo el ciclo ocurre adentro. El host no tiene las herramientas y no va a tenerlas (`Alcance-Producto.md` §4.4) |
| Nada más | Sin base de datos, sin red, sin servicio levantado y sin credencial de acceso. La dependencia core única de esta capa es `GeometriaFactory-Domain` (`PRODUCT-INTAKE` §17.1.P.1 · GeometriaFactory-Application) y su persistencia está declarada como «no aplica directamente» (§17.1.P.4 · GeometriaFactory-Application) |
| Haber leído dos secciones de la especificación funcional | `Especificacion-Funcional.md` §3, la tabla de los cuatro puertos, y §4, las **cuatro** comprobaciones. Sin ellas los once casos de uso se leen mal, porque los dos rasgos que los recorren están enunciados una sola vez ahí |

Conocimiento previo que **no** se supone: el estilo de casos de uso con inversión de dependencias. Es lo que la §7 de esta guía enseña, y es lo que hay que entender antes de tocar nada, porque quien no lo entienda va a intentar consultar datos desde acá.

Vocabulario mínimo para no perderse en la primera media hora. Los términos están definidos en `00-Contexto/Vision-Producto.md` §9, que es el glosario raíz, en [`../02-Especificacion-Funcional/Glosario-Funcional.md`](../02-Especificacion-Funcional/Glosario-Funcional.md) y en [`Glosario-UX.md`](Glosario-UX.md); acá sólo se enumeran para que el lector sepa qué buscar: puerto, doble, motivo, unidad de trabajo, alcance de consulta, verificación de pertenencia, verificación de facultad, camino de alta, metadato de orquestación, cantidad de figuras del conjunto raíz, trabajo, pieza, observación, advertencia, error de validación, texto original, desenlace y comentario.

Tres convenciones que conviene conocer desde el primer minuto porque se cruzan en cada documento:

- **`Pendiente` va siempre calificado**, «cuenta `Pendiente`» o «trabajo en estado `Pendiente`», porque nombra dos estados distintos (`Vision-Producto.md` §9.2).
- **«Repositorio» a secas no se escribe acá.** Es la polisemia propia de esta capa: se dice «puerto de repositorio de trabajos» o «puerto de repositorio de cuentas», y «repositorio de código» para el árbol del producto (`Glosario-Funcional.md` §3.1).
- **«Trabajo» es lo que carga el alumno; el tramo transaccional es siempre «unidad de trabajo»**, en su forma compuesta completa, incluso cuando el contexto parecería bastar (`Glosario-Funcional.md` §3.5).

### 1.4 `GeometriaFactory-Infrastructure`

Esta guía está escrita para tres lectores concretos, y no para un integrador hipotético:

- **Quien escribe la implementación de un puerto** que `GeometriaFactory-Application` declaró, o uno de los dos mecanismos de seguridad.
- **El mantenedor que vuelve sobre este proyecto de código sin el contexto de la etapa en que lo escribió.** Es el caso más frecuente en un equipo de una persona.
- **El agente de IA que construye por etapas**, que necesita reconstruir en cada arranque por qué una condición existe antes de tocarla.

Y un cuarto que aparece después, en el despliegue: **el docente que arranca el contenedor a mano** y se encuentra con un arranque detenido. Para él está la §4.

Prerrequisitos:

| Prerrequisito | Detalle |
| --- | --- |
| El repositorio de código abierto en el entorno de desarrollo contenido | Todo el ciclo ocurre adentro. El host no tiene las herramientas y no va a tenerlas |
| Nada más para empezar | La batería del validador corre **sin almacén, sin red y sin secreto**: recibe texto y devuelve observaciones |
| **Haber leído un documento entero** | [`../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md`](../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md). No es una recomendación: el intake declara que **el defecto que más veces se repite en este producto es escribir el validador sin leer el análisis**, y ese documento es el análisis condensado |
| Haber mirado el modelo de datos | [`../02-Especificacion-Funcional/Modelo-Datos/Modelo-Conceptual.md`](../02-Especificacion-Funcional/Modelo-Datos/Modelo-Conceptual.md), si lo que se va a tocar guarda algo |

Conocimiento previo que **no** se supone: cómo es el dato real que este producto tiene que leer. Es lo que la §3.3 y la §3.4 de esta guía muestran con textos verdaderos, y es lo que hay que entender antes de tocar nada.

Vocabulario mínimo para no perderse en la primera media hora. Los términos están definidos en el glosario raíz, en [`../02-Especificacion-Funcional/Glosario-Funcional.md`](../02-Especificacion-Funcional/Glosario-Funcional.md) y en [`Glosario-UX.md`](Glosario-UX.md); acá sólo se enumeran para que el lector sepa qué buscar: adaptador, almacén, trampa del formato, lectura tolerante, existencia contra veracidad, operador estricto, posición reservada, cantidad de figuras del conjunto raíz, contraseña provisoria, valor derivado de la credencial, acceso firmado, terminación degradada, arranque detenido, transformación de esquema, regla conceptual de modelo, observación, advertencia, error de validación y texto original.

Tres convenciones que conviene conocer desde el primer minuto:

- **`Pendiente` va siempre calificado**, «cuenta `Pendiente`» o «trabajo en estado `Pendiente`», porque nombra dos estados distintos.
- **«Derivado» a secas designa la geometría.** El valor con el que se guarda una contraseña se nombra siempre con su complemento: «valor derivado de la credencial» o «credencial derivada». Es la polisemia que más caro sale acá, porque las dos cosas son números que el sistema calcula.
- **«Validador» designa la implementación**; el contrato se nombra «puerto de validación de figuras», completo.

## 2. Instalación o acceso

### 2.1 `GeometriaFactory-Api`

Todo ocurre **dentro del entorno de desarrollo contenido que el propio repositorio define**. El host no tiene las herramientas y no va a tenerlas: es una restricción declarada del producto, no una preferencia.

```bash
# Dentro del entorno de desarrollo contenido, que el propio repositorio
# define en `.devcontainer/`.

# 1. Script de construcción de la solución de código.
#    Criterio de éxito: termina en 0 y sin advertencias.
./scripts/build.sh

# 2. Script de pruebas del repositorio.
#    Criterio de éxito: pasa entero.
./scripts/test.sh
```

Si el paso 1 termina con advertencias, **no se sigue**: la puerta de calidad del producto es cero advertencias, y arrastrarlas hace que la siguiente sea invisible.

### 2.2 `GeometriaFactory-Domain`

No hay instalación: este proyecto de código no se publica en ningún feed y se compila dentro de `GeometriaFactory.sln` (`PRODUCT-INTAKE` §17.1.P.7 · GeometriaFactory-Domain). El acceso es abrir el repositorio.

```bash
# Dentro del entorno de desarrollo contenido, que el propio repositorio
# define en `.devcontainer/`.

# 1. Script de construcción de la solución de código.
#    Criterio de éxito: termina en 0 y sin advertencias.
./scripts/build.sh

# 2. Script de pruebas del repositorio.
#    Criterio de éxito: pasa entero.
./scripts/test.sh
```

Verificable en un vistazo: si el paso 1 termina en 0 y sin advertencias y el paso 2 pasa entero, el prerrequisito está cumplido y se pasa a §3. Si alguno falla, la §4 tiene los tres arranques que fallan de verdad.

### 2.3 `GeometriaFactory-Application`

No hay instalación: este proyecto de código no se publica en ningún feed y se compila dentro de `GeometriaFactory.sln` (`PRODUCT-INTAKE` §17.1.P.7 · GeometriaFactory-Application). El acceso es abrir el repositorio.

```bash
# Dentro del entorno de desarrollo contenido, que el propio repositorio
# define en `.devcontainer/`.

# 1. Script de construcción de la solución de código.
#    Criterio de éxito: termina en 0 y sin advertencias.
./scripts/build.sh

# 2. Script de pruebas del repositorio.
#    Criterio de éxito: pasa entero.
./scripts/test.sh
```

Verificable en un vistazo: si el paso 1 termina en 0 y sin advertencias y el paso 2 pasa entero, el prerrequisito está cumplido y se pasa a §3. Si alguno falla, la §4 tiene los arranques que fallan de verdad.

### 2.4 `GeometriaFactory-Infrastructure`

No hay instalación: este proyecto de código no se publica en ningún feed y se compila dentro de la solución de código del producto. El acceso es abrir el repositorio de código.

```bash
# Dentro del entorno de desarrollo contenido, que el propio repositorio
# define en `.devcontainer/`.

# 1. Script de construcción de la solución de código.
#    Criterio de éxito: termina en 0 y sin advertencias.
./scripts/build.sh

# 2. Script de pruebas del repositorio.
#    Criterio de éxito: pasa entero, incluidas las diez pruebas del validador.
./scripts/test.sh
```

Verificable en un vistazo: si el paso 1 termina en 0 y sin advertencias y el paso 2 pasa entero, el prerrequisito está cumplido. Si alguno falla, la §4 tiene los arranques que fallan de verdad.

## 3. Primer ejemplo ejecutable

### 3.1 `GeometriaFactory-Api`

### 3.1 El resultado que se busca

**La colección de peticiones corriendo entera contra el servicio real.** Es el primer resultado exitoso de este proyecto de código, y el que mejor lo explica: no hay pantalla, no hay circuito y no hay visor. Sólo el servicio y ocho textos de alumno.

El resultado esperado, que conviene tener a mano para comparar: **8 envíos, 8 respuestas de éxito, 6 trabajos en estado `Pendiente` y 2 en `Borrador`**.

### 3.2 Los pasos

```bash
# 3. Guion de reinicio del almacén: deja el estado de primer arranque.
#    Criterio de éxito: el almacén queda vacío y con su esquema al día.
./scripts/reset-db.sh

# 4. Guion de ejecución del servicio.
#    Criterio de éxito: arranca, aplica las transformaciones y el punto de salud responde.
./scripts/run-api.sh

# 5. Ejecutar la colección de peticiones contra el servicio.
```

Los nombres de los guiones y sus rutas **salen del intake y no se eligen acá**.

### 3.3 El primer ejemplo con sentido: el envío que no verifica

Tomar el texto del escenario **E-5** del intake §20 —un cubo válido y, en la segunda posición, una figura con un tipo que no existe— y enviarlo por el punto de alta de trabajo.

**Lo que hay que mirar, en este orden:**

1. **El código de respuesta es de éxito.** No es un `400`, no es un `422` y no es un `409`. **El trabajo se guardó.**
2. **El estado del trabajo que vuelve es `Borrador`.** Ahí está el «no verificó», y está en el cuerpo, no en el número.
3. **La observación trae índice de figura 1 y campo `Tipo`.** Índice **1**, no 0: el primer elemento es válido a propósito, y ese detalle existe para comprobar que el índice se calcula y no se informa siempre el primero.

**Por qué éste es el primer ejemplo y no el camino feliz.** Porque es el que ordena todo lo demás: quien entiende por qué esta respuesta es exitosa entiende dónde está la frontera de esta capa. **El código de respuesta habla de la petición; el estado del trabajo habla del texto del alumno.** Confundirlos es el defecto que [`DX-Error-Messages.md`](DX-Error-Messages.md) §1.2 llama la consecuencia más cara de esta capa.

Repetir con el texto de **E-8**, donde una dimensión llega como `"3,50"` —la coma decimal que produce la configuración regional de la máquina del alumno—. **Misma forma de respuesta**: éxito, estado `Borrador`, observación localizada. Y es el modo de falla que el propio intake llama **el más probable de todos**, porque no lo produce un error de programación sino la máquina.

### 3.4 El segundo ejemplo: dos respuestas que tienen que ser iguales

Con dos cuentas de alumno y un trabajo del primero, pedir desde la segunda:

1. El detalle del trabajo del primero, cuyo identificador se conoce.
2. El detalle de un identificador que no existe.

**Las dos respuestas tienen que ser indistinguibles**: mismo código de respuesta, mismo código del contrato, mismo cuerpo. Si difieren en algo —una fecha, una longitud, un texto— **RN-00003 está rota**, y ninguna capa de adentro se enteró: la capa de aplicación devolvió el motivo correcto y esta capa lo tradujo mal.

Repetir el ejercicio pidiendo **eliminar** cada uno de los dos, que es el camino que el intake declara **bloqueante** y que exige verificarse **forzando la petición contra la superficie**, no ocultando un control en una pantalla.

### 3.5 Leer una respuesta de fallo

Toda respuesta de fallo de esta superficie tiene **dos identificadores** y hay que leerlos juntos:

- **El código de respuesta** dice de qué clase es el fallo, y es lo que decide qué hace el consumidor: corregir y reintentar, derivar, mostrar, o pasar a estado degradado.
- **El código del contrato** dice cuál exactamente, dentro de un conjunto cerrado de **diecisiete**.

Dos excepciones declaradas, y conviene conocerlas para no buscar un código que no está: el `401` de la guardia y el `400` de una petición que no se puede leer **no llevan código del contrato**, porque ocurren antes de que haya un contrato con el que hablar.

El catálogo entero, con las **18** entradas y qué hace el consumidor con cada una, está en [`DX-Error-Messages.md`](DX-Error-Messages.md) §3. **Se consulta por código, no se lee de corrido.**

### 3.2 `GeometriaFactory-Domain`

### 3.1 El resultado que se busca

El primer resultado exitoso de este proyecto de código es **la batería de dominio en verde en menos de 10 segundos**. Vale la pena detenerse en por qué es tan barato: las pruebas son unitarias puras y **sin dobles**, porque no hay nada que sustituir. No hay base de datos que preparar, ni servicio que levantar, ni reloj que congelar —la fecha llega como dato, porque el reloj es un puerto de la capa de aplicación—. Eso es consecuencia directa de la regla de dependencias, y es lo que justifica que este sea el proyecto de código con la cobertura mínima más alta del producto (`PRODUCT-INTAKE` §17.1.P.6 · GeometriaFactory-Domain, §17.1.P.10 · GeometriaFactory-Domain).

### 3.2 Los pasos

```bash
# 3. Comando de prueba del ecosistema, acotado al proyecto de prueba de este
#    proyecto de código. Criterio de éxito: verde, y completa en menos de 10 segundos.
dotnet test tests/GeometriaFactory.Domain.Tests
```

El segundo resultado, que es el que tiene sentido de dominio, es **ver una guarda negándose**. El caso más corto es el criterio de aceptación CA-02 de CU-02001, el **auto-registro del alumno**, transcripto acá tal como el caso de uso lo declara:

| Given | When | Then |
| --- | --- | --- |
| Los datos de registro con apellido vacío y correo `ana@example.com` | La capa de aplicación solicita constituir el alumno | El dominio rechaza con el código `REQUIRED_FIELD_MISSING` y **no devuelve ninguna entidad** |

Y el contraste, que es CA-01 del mismo caso de uso:

| Given | When | Then |
| --- | --- | --- |
| Correo `ana@example.com`, nombre `Ana`, apellido `Rossi`, fecha de alta 2026-08-09, con la unicidad del correo declarada como verificada | La capa de aplicación solicita constituir el alumno | El dominio devuelve un alumno con papel `Alumno`, cuenta `Pendiente`, credencial derivada sin valor y 0 trabajos |

Los dos criterios se materializan en `tests/GeometriaFactory.Domain.Tests` y se ejecutan con el paso 3. **Los nombres de tipos y de espacios de nombres son un punto abierto declarado**, que se resuelve en `05-Arquitectura-Tecnica` y se valida en el punto de control de la etapa `a` (`PRODUCT-INTAKE` §17.1.P.11 · GeometriaFactory-Domain; `Especificacion-Funcional.md` §9). Por eso acá los conceptos se nombran en lenguaje de dominio y no se propone ninguna firma: la que valga la va a fijar 05, y esta guía la va a citar entonces.

### 3.3 Leer la guarda que se negó

Con el rechazo a la vista, el recorrido de lectura es siempre el mismo, y es lo que hay que automatizar en la cabeza:

1. **El código** dice qué guarda se negó: `REQUIRED_FIELD_MISSING`.
2. **El catálogo** ([`DX-Error-Messages.md`](DX-Error-Messages.md)) dice qué pasó, por qué pasó y qué hacer del lado del consumidor.
3. **El caso de uso** que lo declara (§6 de CU-02001) dice cuál es la respuesta del dominio: rechaza la constitución, no se produce ninguna instancia, no hay efecto parcial.
4. **La regla o el invariante** (§9 del mismo caso de uso) dice por qué la guarda existe.

Cuatro saltos, todos con enlace. Si en algún rechazo la cadena se corta, es un defecto de esta sección.

Y la observación que le da sentido a todo el ejercicio: **el dominio no arregló nada**. No completó el apellido, no consultó a nadie y no reintentó. Devolvió la causa y se quedó quieto. Quien tiene que decidir qué hacer es el consumidor.

### 3.4 Las tres máquinas de estado, en diez minutos

Son las tres figuras que más se consultan durante la primera hora. Están completas, con sus transiciones inadmisibles, en `Definicion-Modelo-De-Dominio.md` §5.1, §5.2 y §5.3. Lo que hay que retener:

**Cuenta.** Tres estados: `Pendiente`, `Habilitado`, `Bloqueado`. **El estado con el que nace depende del camino de alta, y hay dos**:

| Camino de alta | Caso de uso | Nace | Credencial |
| --- | --- | --- | --- |
| Auto-registro del alumno | CU-02001 | cuenta `Pendiente` | Sin valor: se fija **en el acto de habilitación** (CU-02002, que invoca la fijación de CU-02003) con la contraseña provisoria que el sistema produce (RN-02016) |
| Configuración del administrador en el primer arranque | CU-02012 | cuenta **`Habilitado`** | Se aporta ya derivada en el mismo acto |

**La cuenta del administrador nace habilitada porque es la que habilita a las demás**: ninguna cuenta anterior podría habilitarla a ella, y si naciera `Pendiente` por INV-06 tampoco obtendría acceso, de modo que la instancia quedaría inutilizable en el primer arranque. Cada camino **rechaza el del otro**, y por eso `INITIAL_STATUS_NOT_NEGOTIABLE` tiene causas opuestas en cada uno (`DX-Error-Messages.md` §1.4).

Ningún estado vuelve a `Pendiente`. El administrador habilita, bloquea y rehabilita, siempre con acto explícito: no hay habilitación automática. La baja no es un estado: es la desaparición de la cuenta y de sus trabajos (RN-02007). Bloquear una cuenta `Pendiente` sin haber pasado por `Habilitado` no está declarado, y el dominio no lo infiere (CU-02002 FA-03).

**Las cuatro operaciones alcanzan sólo a las cuentas con papel `Alumno`**, y no es una decisión del modelo: es el enunciado literal de la capacidad F-03, «habilitar, bloquear, rehabilitar y dar de baja física cuentas **de alumno**». Sobre la cuenta del administrador no procede ninguna de las cuatro, y el dominio las rechaza con `OPERATION_NOT_APPLICABLE_TO_ADMINISTRATOR_ACCOUNT`. El motivo es la contracara del anterior: **la cuenta que habilita a las demás no puede quedar en un estado del que nadie pueda sacarla**. Y el efecto no se agota en el acceso —que ya sería grave por INV-06—: **sin administrador nadie aprueba ni rechaza**, así que todo trabajo enviado queda en estado `Pendiente` para siempre y el circuito de revisión entero se detiene (RN-02010).

Las dos guardas, la del alta y la del ciclo de vida, son la misma familia vista en dos momentos, y es la propiedad que **INV-08** enuncia de una sola vez. **Está adoptado desde `PRODUCT-INTAKE` §17.1.P.2 · GeometriaFactory-Domain** y se cuenta entre los nueve invariantes vigentes de §7.1; la guía lo citaba como candidato y esa cita quedó desactualizada.

**Trabajo.** Cuatro estados: `Borrador`, `Pendiente`, `Finalizado`, `Rechazado`. Dos propiedades gobiernan lo demás:

1. **`Borrador` significa exactamente «el texto no verificó»**, o que el trabajo recién se creó. Guardar y enviar se unificaron en una sola acción, enviar: no se puede conservar en borrador un trabajo cuyo texto sí verifica.
2. **`Finalizado` y `Rechazado` son terminales.** No sale ninguna transición de ellos, y corregir un rechazo significa cargar un trabajo nuevo (INV-07, RN-02010). Lo único que un trabajo terminal admite es que el administrador lo elimine.

Un trabajo sin piezas y sin observaciones es un estado normal: es el trabajo recién creado, antes de que su texto se haya interpretado.

Tres distinciones que conviene fijar antes de seguir, porque confundirlas es el error más caro de esta sección:

| No son lo mismo | Diferencia | Dónde está declarado |
| --- | --- | --- |
| Observación y condición de error del dominio | La **observación** es una entidad del dominio, con varias filas por trabajo, que el producto emite al interpretar el texto del alumno; sus dos especies son la advertencia y el error de validación. La **condición de error** es una guarda que impide una operación ilegítima del consumidor y no se guarda en ninguna parte | `Definicion-Modelo-De-Dominio.md` §2.5; [`Glosario-UX.md`](Glosario-UX.md) §3.1 |
| Observación y comentario | La observación la emite el producto y hay tantas como defectos; el **comentario** lo escribe una persona, el administrador, y hay a lo sumo uno por trabajo. No es una calificación | `Vision-Producto.md` §9.1; `Definicion-Modelo-De-Dominio.md` §2.5 |
| Advertencia y error de validación | Las dos son especies de observación. Sólo el **error de validación** impide que el trabajo pase a estado `Pendiente`; la advertencia no lo impide, y es deliberado | RN-02005; `Vision-Producto.md` §9.1 |

### 3.3 `GeometriaFactory-Application`

### 3.1 El resultado que se busca

El primer resultado exitoso de este proyecto de código es **la batería de la capa de aplicación en verde, sin haber preparado nada externo**. Vale la pena detenerse en por qué eso es posible: las pruebas son unitarias con **dobles** —repositorio simulado, validador doble, reloj fijado—, y no hay base de datos que preparar, ni servicio que levantar, ni credencial de acceso que configurar.

Eso no es una comodidad: es la propiedad que justifica el diseño entero. Se renunció a consultar la base con proyecciones ad-hoc desde el caso de uso, y lo que se compró con esa renuncia fue poder ejercer cada contrato de punta a punta con dobles (`PRODUCT-INTAKE` §17.1.P.12 · GeometriaFactory-Application). La puerta de calidad propia y bloqueante lo dice sin rodeos: **ninguna prueba de esta capa toca la base de datos real; si una lo hace, está mal ubicada y pertenece a integración** (§17.1.P.8 · GeometriaFactory-Application).

### 3.2 Los pasos

```bash
# 3. Comando de prueba del ecosistema, acotado al proyecto de prueba de este
#    proyecto de código. Criterio de éxito: verde, y sin haber preparado
#    ninguna base de datos, ningún servicio y ninguna credencial de acceso.
dotnet test tests/GeometriaFactory.Application.Tests
```

Los pasos se nombran por su papel —entorno de desarrollo contenido, script de construcción, script de pruebas, comando de prueba del ecosistema— y conservan su forma literal porque se tienen que poder ejecutar. Las rutas y el nombre del proyecto de prueba salen de `PRODUCT-INTAKE` §16 y §17.1.P.6 · GeometriaFactory-Application, y no se eligen acá.

### 3.3 El primer ejemplo con sentido de aplicación

El segundo resultado, que es el que explica la capa, es **ver un caso de uso entero resolviendo con dobles**. El más ilustrativo es el más pesado: CU-04005, el envío. Se transcribe su criterio de aceptación CA-01 tal como el caso de uso lo declara:

| Given | When | Then |
| --- | --- | --- |
| Un trabajo en `Borrador` del alumno A con el texto semilla de 3 piezas —cilindro, cubo y ortoedro—, y un validador doble que devuelve 3 piezas y 2 advertencias: área declarada 36.00 contra derivada 54.00 en el cubo, y volumen declarado 343.00 contra derivado 1029.00 en el ortoedro | El alumno A envía el trabajo | El caso de uso devuelve el trabajo en estado `Pendiente` con 3 piezas y 2 advertencias, y ninguna de las dos lo bloquea |

Tres cosas ocurrieron ahí, y las tres son la capa entera en miniatura:

1. **El texto no se interpretó acá.** El caso de uso se lo entregó al puerto de validación de figuras y recibió la cantidad de figuras del conjunto raíz, las piezas reconstruidas y las observaciones. Por eso un doble alcanza para ejercerlo: la lógica de tolerancia del formato vive en la implementación, en `GeometriaFactory-Infrastructure`.
2. **El estado no lo decidió el caso de uso.** Le entregó al dominio el conjunto de observaciones y el dominio resolvió: sin errores de validación pasa a estado `Pendiente`, con errores queda en `Borrador` (RN-04005). Las advertencias señalan y no bloquean, y eso es deliberado.
3. **Nada tocó una base de datos.** Ni siquiera el sello: el sello de modificación salió del puerto de reloj, fijado por la prueba.

Y el contraste, que es CA-05 del mismo caso de uso y el ejemplo canónico de la comprobación de pertenencia:

| Given | When | Then |
| --- | --- | --- |
| Un trabajo en `Borrador` del alumno A | El alumno B lo envía | El caso de uso devuelve el motivo `WORK_NOT_FOUND_FOR_REQUESTER` y **el validador doble registra 0 invocaciones** |

Ese cero es lo que hay que mirar: la comprobación ocurrió **antes** de invocar al validador y antes de escribir nada. La pertenencia se verifica sobre el dato recuperado, no sobre lo que declara la petición (CU-04004 §10).

**Los nombres de tipos y de espacios de nombres son un punto abierto declarado**, que se resuelve en `05-Arquitectura-Tecnica` y se valida en el punto de control de la etapa `a` (`Especificacion-Funcional.md` §11). Por eso acá los conceptos se nombran en lenguaje de dominio y no se propone ninguna firma: la que valga la va a fijar 05, y esta guía la va a citar entonces.

### 3.4 Leer una negativa

Con el motivo a la vista, el recorrido de lectura es siempre el mismo, y es lo que hay que automatizar en la cabeza:

1. **El motivo** dice qué comprobación se negó: `WORK_NOT_FOUND_FOR_REQUESTER`.
2. **El catálogo** ([`DX-Error-Messages.md`](DX-Error-Messages.md)) dice qué pasó, por qué pasó y qué hacer —y de qué lado hacerlo, que acá no siempre es el del consumidor.
3. **El caso de uso** que lo declara (§6 de CU-04004, CU-04005, CU-04006 o CU-04009) dice cuál es la respuesta: no procede, sin escritura y sin invocar al validador.
4. **La regla de negocio** (§9 del mismo caso de uso) dice por qué la comprobación existe: RN-04003, que vive en `GeometriaFactory-Domain` y que esta capa **ejerce** sin volver a enunciarla.

Cuatro saltos, todos con enlace. Si en alguna negativa la cadena se corta, es un defecto de esta sección.

Y la observación que le da sentido al ejercicio: **el caso de uso no arregló nada.** No consultó de más, no reintentó y no corrigió el pedido. Devolvió el motivo y dejó el repositorio como estaba. Quien tiene que decidir qué hacer —y sobre todo **cómo traducirlo hacia afuera**— es el consumidor.

### 3.5 Las cuatro negativas, en diez minutos

Es el tramo que más rinde de la primera media hora, y el que se evalúa en el tramo de 30 minutos de [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §2. La tabla completa está en [`DX-Error-Messages.md`](DX-Error-Messages.md) §2.4; lo que hay que retener es esto:

| Se preguntó por | Negativa | ¿Oculta que el recurso existe? | Se traduce a |
| --- | --- | --- | --- |
| Un recurso que puede ser de otra persona | `WORK_NOT_FOUND_FOR_REQUESTER` | **Sí, deliberadamente** | «No encontrado», **nunca** «no autorizado» |
| Una facultad | `ADMINISTRATOR_ROLE_REQUIRED` | No, y no tiene por qué | Un mensaje explícito |
| Un recurso fuera del alcance del papel | `WORK_OUTSIDE_ADMINISTRATOR_SCOPE` | No | Un mensaje explícito |
| Cualquier cosa, desde una cuenta con la contraseña reseteada que no cambió su provisoria | `PASSWORD_CHANGE_PENDING` | No, y **corta antes que las otras tres** | La derivación al cambio de contraseña, que es lo único que esa cuenta puede hacer (INV-09, RN-04013) |

La frase que resume todo y conviene poder recitar: **el papel no reemplaza a la pertenencia, y la pertenencia no se confiesa.** Y antes que las tres, la cuarta: **una cuenta con la provisoria sin cambiar no ejerce ninguna capacidad**, ni siquiera las que su papel y su pertenencia admitirían.

Por qué importa tanto: confirmar que un trabajo ajeno existe permite averiguar por tanteo qué identificadores existen. Por eso el trabajo ajeno y el identificador inexistente comparten motivo **por diseño**, y por eso hay dos criterios de aceptación —CA-03 de CU-04006 y CA-03 de CU-04009— cuyo único propósito es verificar que los dos motivos son el mismo.

Un cuarto caso, de la misma familia y fácil de olvidar: **la cuenta inexistente en la consulta de admisibilidad tampoco se distingue hacia afuera**, para no revelar qué correos están registrados (CU-04003 §6 y §10, CA-05).

Dos distinciones más que conviene fijar antes de seguir, porque confundirlas es el otro error caro de esta sección:

| No son lo mismo | Diferencia | Dónde está declarado |
| --- | --- | --- |
| Condición de error y observación | La **condición de error** es una comprobación que impide una operación y no se guarda en ninguna parte. La **observación** es una entidad del dominio, con dos especies, que el validador produce al interpretar el texto del alumno y que el caso de uso incorpora al trabajo. Un trabajo que vuelve en `Borrador` por un error de validación **no produjo ninguna condición de error** | [`DX-Error-Messages.md`](DX-Error-Messages.md) §1.2; [`Glosario-UX.md`](Glosario-UX.md) §3.1 |
| Observación y comentario | La observación la emite el producto y hay tantas como defectos; el **comentario** lo escribe el administrador, hay a lo sumo uno por trabajo y **no es una observación ni una calificación** | `Vision-Producto.md` §9.1; CU-04008 §10 |

### 3.4 `GeometriaFactory-Infrastructure`

### 3.1 El resultado que se busca

El primer resultado exitoso de este proyecto de código es **la batería del validador en verde sobre textos reales**. Vale la pena detenerse en por qué eso es posible sin preparar nada: el validador **recibe texto y devuelve observaciones**, no hace red y no lee configuración propia.

Eso no es una comodidad: es lo que permite que la mitigación del riesgo más alto del producto se ejerza en segundos, en cada punto de control, sin levantar nada.

### 3.2 Los pasos

```bash
# 3. Guion de reinicio del almacén: deja el estado de primer arranque.
#    Criterio de éxito: el almacén queda vacío y con su esquema al día.
./scripts/reset-db.sh
```

Los pasos se nombran por su papel y conservan su forma literal porque se tienen que poder ejecutar. Las rutas salen del intake §16 y no se eligen acá. **El tercero es propio de este proyecto de código**: es el camino de vuelta declarado del producto, y lo que permite repetir cualquier prueba de persistencia desde un estado conocido.

### 3.3 El primer ejemplo con sentido: el texto que rompe a un lector estricto

Esto es lo que el programa del alumno emite de verdad para un `Ortoedro(7, 7, 21)`. Es el escenario **E-2** del intake, transcripto de su §20:

```text
[
{
  "Tipo": "Ortoedro",
  "Tapas":
  [
    { "Tipo": "Rectangulo", "Largo": 7.00, "Ancho": 7.00, "Area": 49.00 },
    { "Tipo": "Rectangulo", "Largo": 7.00, "Ancho": 7.00, "Area": 49.00 }
  ],
  "Laterales":
    [
      { "Tipo": "Rectangulo", "Largo": 21.00, "Ancho": 7.00, "Area": 147.00 },
      { "Tipo": "Rectangulo", "Largo": 21.00, "Ancho": 7.00, "Area": 147.00 },
      { "Tipo": "Rectangulo", "Largo": 21.00, "Ancho": 7.00, "Area": 147.00 },
      { "Tipo": "Rectangulo", "Largo": 21.00, "Ancho": 7.00, "Area": 147.00 },
    ],
  "Area": 686.00,
  "Volumen": 343.00
},
]
```

**El bloque va rotulado como texto y no como JSON a propósito, y así lo rotula el intake: no es JSON estrictamente válido.** Mirá las dos comas antes de los cierres. Un resaltador estricto marca eso como error, y **ese error es justamente lo que el sistema tiene que tolerar**.

Tres cosas hay que ver acá, y son la capa entera en miniatura:

1. **Las dos comas finales** (trampa `T2`). Un lector estricto rechaza el texto entero, y con eso el trabajo del alumno no entra nunca al producto. Este contrato lee **con tolerancia a comas finales y omisión de comentarios**.
2. **La clave `"Tapas"`** (trampa `T1`) donde el visualizador previo exige `"Bases"`. Es la razón por la que **hoy ningún ortoedro generado por la aplicación se dibuja**, y por la que falla en silencio. Este contrato acepta **las dos claves como sinónimas**.
3. **El volumen declarado es 343.00 y la geometría dice `7·7·21 = 1029.00`** (trampa `T4`). Eso **no se rechaza y no se corrige**: se emite **una advertencia**, el trabajo pasa a estado `Pendiente` igual, y el alumno ve su propio error de fórmula sobre su propio trabajo. **El área, en cambio, no produce nada**: `2·49 + 4·147 = 686.00` coincide con lo declarado.

El criterio que ancla todo esto es `CU-06001` CA-01, y el de la advertencia es `CU-06002` CA-06.

### 3.4 El segundo ejemplo: los dos cubos que parecen iguales

El contraste que más enseña del producto. Son los escenarios **E-3** y **E-4**: el **mismo cubo de lado 3**, emitido por los dos ejemplos de la cátedra.

| | `Ejemplo1` (E-3) | `Ejemplo2` (E-4) |
| --- | --- | --- |
| Tipo de las caras | `"Cuadrado"` | `"Rectangulo"` |
| `Area` declarada | **36.00** | **54.00** |
| `Volumen` declarado | 27.00 | 27.00 |
| Lo que el validador debe hacer | Interpretar las caras (`T3`) y emitir **1 advertencia de área**: declarada 36.00, derivada 54.00 | Interpretar las caras igual (`T3`) y emitir **cero observaciones** |

Las dos lecciones:

1. **La forma del texto cambia y el resultado de la interpretación no.** Las caras se llaman distinto en cada ejemplo y las dos se interpretan igual: lo que se usa para reconstruir es `Largo`.
2. **El criterio negativo es más difícil de acertar que el positivo.** Un validador que advirtiera siempre pasaría E-3 y **fallaría E-4**. Por eso los dos escenarios están, y por eso los dos son casos de la batería obligatoria.

Y una tercera lección, que es la que decide el escenario semilla del producto: **el operador de la comparación es estricto**. En el escenario **E-1**, el área del cilindro declara 113.10 y la suma de sus componentes da 113.09: diferencia de **exactamente 0.01**. Con el operador estricto ese caso **no** advierte y el escenario da las **dos** advertencias documentadas; con «mayor o igual» daría **tres** y el caso de prueba canónico del producto fallaría. El criterio `CU-06002` CA-09 lo ancla con dos diferencias, 0.010 y 0.011.

### 3.5 Leer una condición de error

Con el código a la vista, el recorrido de lectura es siempre el mismo:

1. **El código** dice qué no se pudo hacer: `STORE_PATH_UNAVAILABLE`.
2. **El catálogo** ([`DX-Error-Messages.md`](DX-Error-Messages.md)) dice qué pasó, por qué pasó y qué hacer —**y de qué lado hacerlo, que acá muchas veces es el del despliegue y no el del código**.
3. **El caso de uso** que lo declara (§6 de CU-06010) dice cuál es la respuesta: el arranque se detiene y no se atiende ninguna petición.
4. **La regla conceptual de modelo o la de negocio** (§9 del mismo caso de uso) dice por qué la condición existe.

Cuatro saltos, todos con enlace. Si en alguna condición la cadena se corta, es un defecto de esta sección.

**Y la pregunta que hay que hacerse antes que ninguna otra: ¿esto es una condición de error o es un resultado?** La mayoría de lo que parece un fallo en esta capa es el funcionamiento normal del producto: un error de validación, un texto ilegible, cero advertencias, nada encontrado, un conjunto vacío, una credencial que no coincide y un acceso vencido **son resultados y no están en el catálogo**. Los siete están reunidos en [`DX-Error-Messages.md`](DX-Error-Messages.md) §1.2.

## 4. Diagnóstico de problemas frecuentes en la primera hora

### 4.1 `GeometriaFactory-Api`

| Síntoma | Causa probable | Qué hacer |
| --- | --- | --- |
| El servicio no arranca y no atiende nada | La clave de firma no está provista, o la ubicación del almacén no admite escritura | Revisar la configuración del entorno. **Es deliberado**: el servicio se detiene antes que atender sin poder verificar accesos o sin poder guardar |
| El servicio no arranca y el almacén ya existía | El esquema no corresponde al linaje de transformaciones conocido, **normalmente porque se editó una transformación ya fusionada** | Reiniciar el almacén con el guion del paso 3. **No descartar el almacén de producción**: ése es el atajo que deja el servicio impecable y sin los trabajos de nadie |
| Todas las peticiones responden `401` | No se está presentando el acceso firmado, o venció | Volver a canjear credenciales. **La vigencia es corta y no hay acceso de refresco** |
| Una petición responde `403` y el motivo dice que hay un cambio de contraseña pendiente | La cuenta fue reseteada y todavía no cambió la provisoria | Cambiar la contraseña por su punto. **Es lo único que esa cuenta puede hacer**, y es la guardia funcionando |
| El envío responde `400` nombrando un campo | Falta un campo que el contrato exige | Corregir la solicitud. **No confundir con el texto que no verifica**, que responde con éxito |
| El envío responde con éxito pero el trabajo quedó en `Borrador` | **No es un problema.** El texto no verificó y sus observaciones están en el cuerpo | Mirar las observaciones: traen índice de figura y campo |
| El listado devuelve una colección vacía | **No es un problema.** Es una comisión sin entregas | Distinguir vacío de fallo **por el tipo recibido y no por el conteo** |
| Una petición responde `503` | El almacén no está disponible, o el servicio todavía no terminó de arrancar | Revisar el despliegue. **El mensaje no dice la ruta, y es a propósito**: dice qué revisar, no dónde estaba mirando el proceso |
| Una petición responde `500` | Un defecto no previsto | Buscarlo en el registro del lado del servidor, que es donde **sí** está el detalle |
| Se agregó un punto de acceso y una cuenta con la contraseña reseteada puede usarlo | **El punto quedó fuera de la guardia** | Ver §6.2. Es una de las tres cosas que no fallan |
| Dos respuestas que deberían ser iguales difieren | La traducción distinguió el recurso ajeno del inexistente | Ver §6.2. Es otra de las tres |
| El texto guardado no es idéntico al enviado | Algo lo normalizó en el borde: codificación, espacios, saltos de línea o un recorte por tamaño | Ver §6.2. Es la tercera |
| La colección se ejecuta dos veces y falla al principio | El administrador ya está configurado y el correo ya está registrado | Reiniciar el almacén con el guion del paso 3. **Es un resultado legítimo y la colección lo declara** |

### 4.2 `GeometriaFactory-Domain`

| Síntoma | Causa probable | Qué hacer |
| --- | --- | --- |
| Un comando del quick-start no existe en el host | Se está ejecutando fuera del entorno de desarrollo contenido. El host no tiene las herramientas y no va a tenerlas | Abrir el repositorio en el entorno contenido del propio repositorio y repetir desde el paso 0 |
| `./scripts/build.sh` termina en 0 pero con advertencias | La puerta de calidad exige 0 **y sin advertencias** para fusionar (§17.1.P.8 · GeometriaFactory-Domain) | Tratar la advertencia como bloqueante: no es un aviso, es un criterio de la etapa |
| La batería de dominio tarda notablemente más de 10 segundos | Se coló una dependencia de entrada y salida en una prueba que debería ser pura y sin dobles | Buscar qué prueba toca algo externo. Este proyecto de código no tiene dependencias: una prueba que necesita preparar algo está probando otra capa |
| Se busca dónde se guarda la entidad y no aparece | La persistencia está declarada como «no aplica» (§17.1.P.4 · GeometriaFactory-Domain). El dominio no guarda nada; la materialización es posterior y externa | Buscar en `GeometriaFactory-Infrastructure`. En el fallo de una operación de dominio no queda estado intermedio, justamente porque no guarda |
| Se busca dónde se interpreta el texto del alumno | No está acá. La interpretación y el cálculo del valor derivado los hace el validador de figuras, detrás de un puerto de la capa de aplicación | `Definicion-Modelo-De-Dominio.md` §7. El dominio **recibe** el resultado de la interpretación por CU-02006 y CU-02007 |
| La configuración del administrador rechaza con `ADMINISTRATOR_ROLE_OUTSIDE_THIS_PATH`, o queda una cuenta de administrador `Pendiente` que nadie puede habilitar | Se está usando el camino del auto-registro para constituir el administrador. Son **dos caminos de alta distintos**, con estado inicial y credencial propios | Usar CU-02012, que constituye la cuenta `Habilitado` y con su credencial en el mismo acto. Es la corrección del P0: con el camino equivocado la instancia queda inutilizable en el primer arranque |
| Se busca la comparación de contraseñas o la emisión del acceso | El dominio no implementa autenticación. Sí modela las reglas que la condicionan | [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §1.3 |
| Una llamada de constitución rechaza con `EMAIL_UNIQUENESS_NOT_VERIFIED` y el correo evidentemente está libre | El consumidor no **declaró** haber verificado la unicidad. El dominio no consulta: exige la declaración | Resolver la unicidad en la capa de aplicación con el puerto de repositorio y declararla al invocar (CU-02001 §3) |
| Un envío rechaza con `SUBMISSION_WITHOUT_PARSE_RESULT` | Se envió un trabajo cuyo texto original nunca fue interpretado. El envío decide **sobre** el resultado de la interpretación | Invocar antes CU-02006 y CU-02007 con el resultado que produjo el validador, y recién después CU-02008 |
| Un envío devuelve el trabajo en `Borrador` y se lo interpreta como error | No es un error: es el resultado declarado del envío cuando hay al menos una observación de especie error de validación (CU-02008 FA-01). Las advertencias no lo impiden | No traducirlo a fallo hacia afuera: el alumno corrige y vuelve a enviar cuantas veces haga falta |
| Se intenta corregir un trabajo `Rechazado` y todo rebota | `Rechazado` es terminal por decisión aceptada por escrito. Corregir un rechazo significa cargar un trabajo nuevo | CU-02010 FA-03. El rechazado queda como registro del intento y sólo el administrador puede quitarlo |
| No se encuentra el identificador propio de una pieza | No existe: **la identidad de la pieza es su posición** en el conjunto raíz, porque el dato del alumno no trae identificador | `Definicion-Modelo-De-Dominio.md` §2.3 y §6. Por eso el orden del texto del alumno es significativo |
| Dos correos que parecen el mismo se tratan como distintos | El criterio con el que dos correos se consideran el mismo es un **punto abierto declarado y no bloqueante**: el dominio conserva el dato como lo recibe y no normaliza | `Especificacion-Funcional.md` §9. Lo resuelve `05-Arquitectura-Tecnica` junto con la capa que ejerce la verificación |
| Se busca el nombre exacto de un tipo o de un espacio de nombres | Es el otro punto abierto declarado: se fija en 05 y se valida en el punto de control de la etapa `a` | `PRODUCT-INTAKE` §17.1.P.11 · GeometriaFactory-Domain. Hasta entonces, los conceptos se nombran en lenguaje de dominio |

### 4.3 `GeometriaFactory-Application`

| Síntoma | Causa probable | Qué hacer |
| --- | --- | --- |
| Un comando del quick-start no existe en el host | Se está ejecutando fuera del entorno de desarrollo contenido. El host no tiene las herramientas y no va a tenerlas | Abrir el repositorio de código en el entorno contenido que él mismo define y repetir desde el paso 0 |
| `./scripts/build.sh` termina en 0 pero con advertencias | La puerta de calidad exige 0 **y sin advertencias** para fusionar (§17.1.P.8 · GeometriaFactory-Application) | Tratar la advertencia como bloqueante: no es un aviso, es un criterio de la etapa |
| Una prueba de esta capa necesita preparar una base de datos | La prueba está mal ubicada. La puerta de calidad propia de la capa es que **ninguna prueba de acá toca la base de datos real** (§17.1.P.8 · GeometriaFactory-Application) | Moverla a `tests/GeometriaFactory.Integration.Tests`, que pertenece a la Api, o revisar por qué el caso de uso no pasa por un puerto |
| Se busca dónde el caso de uso arma la consulta | No la arma. Le pide al puerto una consulta **ya acotada** por dueño o por alcance, y el cómo vive en `GeometriaFactory-Infrastructure` | `Especificacion-Funcional.md` §3. Si aparece la tentación de traer todo y filtrar en memoria, ver la fila siguiente |
| Se filtra en memoria el resultado de una consulta | Es el patrón que la separación entre alumnos viene a impedir: da el mismo resultado visible y deja de ser una garantía | Trasladar el recorte al pedido (CU-04006 §10, CU-04007 §10). Un borrador que llega a esta capa y se descarta acá **ya viajó** |
| Se busca dónde se guarda el trabajo | Acá no se guarda: se **entrega** al puerto de repositorio dentro de una única unidad de trabajo. La persistencia está declarada como «no aplica directamente» (§17.1.P.4 · GeometriaFactory-Application) | Buscar en `GeometriaFactory-Infrastructure`. El alcance transaccional es un caso de uso, una unidad de trabajo |
| Se busca dónde se interpreta el texto del alumno | No está acá. La interpretación y el cálculo del valor derivado los hace el validador de figuras, detrás del puerto de validación de figuras | CU-04005 §10. El caso de uso **entrega** el texto y **recibe** piezas y observaciones |
| Se busca la comparación de contraseñas o la emisión del acceso | Esta capa no autentica: **autoriza**. El valor de credencial llega ya derivado y el valor en claro nunca la atraviesa | [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §1.3, la tabla de la frontera |
| Se busca el identificador del puerto de repositorio de cuentas y no aparece en el intake | Es correcto: el intake nombra tres puertos y no éste. Está declarado como **punto abierto** y esta sección no lo reabre | `Especificacion-Funcional.md` §3 y §11. Hasta que 05 lo fije, se lo nombra en lenguaje de dominio |
| Se busca el nombre exacto de un tipo o de un espacio de nombres | Es el otro punto abierto declarado: se fija en 05 y se valida en el punto de control de la etapa `a` | `PRODUCT-INTAKE` §17.1.P.11 · GeometriaFactory-Application; `Especificacion-Funcional.md` §11 |
| Un envío devuelve el trabajo en `Borrador` y se lo interpreta como error | No es un error y **no produjo ninguna condición del catálogo**: es el resultado declarado del envío cuando hay al menos una observación de especie error de validación (CU-04005 FA-01). Las advertencias no lo impiden | No traducirlo a fallo hacia afuera: el alumno corrige por CU-04004 y vuelve a enviar cuantas veces haga falta |
| Un envío devuelve `PARSE_RESULT_UNAVAILABLE` y se lo trata como dato inválido | No lo es: el puerto de validación no pudo completar la interpretación. El trabajo queda en `Borrador` con su texto intacto y el estado es **degradado**, no interpretado | Informar que la interpretación no está disponible. **No inventar observaciones y no pasar a estado `Pendiente`.** Esta capa no reintenta |
| Un envío devuelve `MALFORMED_OBSERVATION` y se busca qué corrigió mal el alumno | No es del alumno ni del consumidor: es un **defecto del adaptador del validador**, que devolvió un conjunto que el contrato no admite | Corregir el adaptador en `GeometriaFactory-Infrastructure`. Un conjunto mal formado no es un resultado que el alumno deba ver (CU-04005 §6) |
| Una reedición rebota con `OPERATION_OUTSIDE_DRAFT` y se lo confunde con la negativa de pertenencia | Son motivos distintos a propósito: acá **la existencia del trabajo ya está admitida para su dueño**, y lo que se niega es la operación, no el acceso | CU-04004 FA-03. **Ver** un trabajo propio sí procede en los cuatro estados; lo que se acota al borrador es operarlo |
| Se busca cómo corregir un trabajo `Rechazado` y todo rebota | `Rechazado` es terminal por decisión aceptada por escrito. Corregir un rechazo significa cargar un trabajo nuevo | CU-04008 FA-03. El rechazado queda como registro del intento y sólo el administrador puede quitarlo, por CU-04009 |
| Un alta rechaza con `EMAIL_ALREADY_REGISTERED` aunque la consulta previa dijo que estaba libre | Es el camino declarado de CU-04001 FA-03: **la verificación previa no es una garantía por sí sola**, y la unicidad efectiva la sostiene también la capa que guarda | No materializar nada y devolver el motivo. Sin informar el estado ni el papel de la cuenta que ocupa el correo |
| Dos correos que parecen el mismo se tratan como distintos | El criterio con el que dos correos se consideran el mismo es un **punto abierto declarado y no bloqueante**, que viene del dominio y esta categoría no reabre | `Especificacion-Funcional.md` §11. Lo resuelve 05 junto con la capa que ejerce la verificación |
| Se busca dónde se enuncia una regla de negocio y no está | Las **dieciséis** reglas viven en `GeometriaFactory-Domain` y acá se **ejercen**, no se redactan. **Las dieciséis tienen archivo allá**, incluidas RN-04012 y RN-04013, que entraron con el `PRODUCT-INTAKE` 1.7, RN-04014 y RN-04015, que entraron con el 1.10, y **RN-04016**, que entró con el 1.13 | `Especificacion-Funcional.md` §6 dice, regla por regla, dónde se ejerce cada una en esta capa |
| Se busca en CU-04001 el alta del administrador y no está | Son **dos caminos de alta con reglas opuestas** —estado inicial, credencial y ventana de alta—, y por eso son dos contratos: el auto-registro del alumno es CU-04001 y la configuración del administrador es CU-04010 | [`DX-Error-Messages.md`](DX-Error-Messages.md) §1.4, con la tabla de los cinco rasgos opuestos |
| Un alta rechaza con `INITIAL_STATUS_NOT_NEGOTIABLE` y la causa parece contradecir a la del otro camino | No se contradicen: el enunciado es «el estado inicial de **este** camino no se elige», y cuál es ese estado lo fija el camino. `Pendiente` en el auto-registro, `Habilitado` en la configuración del administrador | Es el único motivo con fila completa en dos subsecciones del catálogo, con remisión mutua ([`DX-Error-Messages.md`](DX-Error-Messages.md) §3.1 y §3.10) |
| Se busca la cantidad de figuras del conjunto raíz y se la intenta derivar contando piezas | **No es derivable**: el conjunto de piezas adoptadas admite huecos, porque la posición de una figura no reconstruida queda reservada. La produce el validador al interpretar | §7.1 de esta guía. Sin ese dato el dominio no tiene rango contra el cual validar la posición de una observación |
| Un envío rechaza con `MALFORMED_PIECE_SET` o con `MALFORMED_OBSERVATION` y se busca el motivo fino | Son **condiciones agregadas**: cada una reúne cuatro rechazos del dominio, y esta capa emite uno solo porque ninguno de los ocho es un resultado que el alumno deba ver | El motivo fino está en la 02 de `GeometriaFactory-Domain`. La agregación está declarada en [`DX-Error-Messages.md`](DX-Error-Messages.md) §2.5 |
| Se busca en el catálogo un rechazo que el dominio declara y no aparece | Puede ser **inalcanzable por construcción**, equivalente a otro de esta capa, o estar agregado. Ninguna de las tres es un olvido, y las tres están declaradas | [`DX-Error-Messages.md`](DX-Error-Messages.md) §2.5, con las dieciséis filas y su lugar de declaración en la 02 |

### 4.4 `GeometriaFactory-Infrastructure`

| Síntoma | Causa probable | Qué hacer |
| --- | --- | --- |
| Un comando del quick-start no existe en el host | Se está ejecutando fuera del entorno de desarrollo contenido. El host no tiene las herramientas y no va a tenerlas | Abrir el repositorio de código en el entorno contenido que él mismo define y repetir desde el paso 0 |
| `./scripts/build.sh` termina en 0 pero con advertencias | La puerta de calidad exige 0 **y sin advertencias** para fusionar | Tratar la advertencia como bloqueante: no es un aviso, es un criterio de la etapa |
| El texto del alumno se rechaza entero y la prueba de E-2 falla | Se está leyendo con un lector estricto. **El texto no es JSON estrictamente válido** | Leer con tolerancia a comas finales y omisión de comentarios (`T2`). Ver §3.3 |
| El ortoedro no se reconstruye | Se está buscando la clave `"Bases"` y el programa emite `"Tapas"` | Aceptar **las dos como sinónimas** (`T1`). Es la línea que desbloquea el dibujo de todos los ortoedros |
| El cubo de `Ejemplo2` no se reconstruye | Se está aceptando sólo `"Cuadrado"` como tipo de cara | Aceptar también `"Rectangulo"` (`T3`). Son la misma cara emitida por dos programas |
| Una figura con `"Largo": 0.00` se descarta | Se está evaluando la **verosimilitud** del valor en lugar de la **existencia** del campo | Comparar por existencia. El escenario **E-6** es exactamente ese caso y exige que la figura se interprete |
| El escenario semilla devuelve **3** advertencias en lugar de 2 | El operador de comparación no es estricto | Advertir cuando la diferencia es **mayor** que la tolerancia, no mayor o igual. El área del cilindro de E-1 difiere en exactamente 0.01 |
| Se quiere «arreglar» el valor declarado que está mal | El producto **no corrige el dato del alumno**: lo señala. Es su mayor valor didáctico y una regla de negocio | Emitir la advertencia con **los dos valores** y dejar el texto intacto (RN-06008) |
| Un texto ilegible devuelve `PARSE_RESULT_UNAVAILABLE` | **Es la confusión más cara del producto.** Un texto que el alumno escribió mal es un **resultado**, no una avería | Devolver 0 figuras, 0 piezas y **una observación**. El criterio `CU-06001` CA-10 existe para eso |
| Se busca dónde el validador decide el estado del trabajo | No lo decide. Entrega el conjunto de observaciones y **el dominio resuelve** | `Especificacion-Funcional.md` §4. Un validador que decidiera el estado tendría dentro una regla que no le pertenece |
| Se busca dónde comprobar que el solicitante es el dueño del trabajo | **Acá no se comprueba.** El recorte de la consulta **llega en el pedido**, ya decidido | `Especificacion-Funcional.md` §4. Lo único propio es negarse a resolver una consulta **sin** recorte |
| La consulta de listado devuelve los componentes de las piezas | El listado **no los incluye**, y es una decisión de modelado con efecto en el tiempo de respuesta | `CU-06003` CA-04. El detalle sí los trae; el listado, nunca |
| Se piensa en compactar las posiciones de las piezas para no dejar huecos | **Compactar no falla: produce mensajes que apuntan a la figura equivocada** | `RC-06002`. La posición de una figura no reconstruida queda **reservada** |
| Se intenta derivar la cantidad de figuras del conjunto raíz contando piezas | **No es derivable**: el conjunto admite huecos. La produce el validador al interpretar | `CU-06001` §4 paso 3. Sin ese dato, la observación ubicada deja de ser comprobable |
| Se busca dónde se guarda la contraseña en claro | **En ningún lado.** Se guarda su valor derivado, y el valor en claro no sale de `CU-06006` ni de `CU-06007` | `CU-06006` §7 y CA-07 |
| Dos derivaciones de la misma contraseña dan valores distintos y parece un defecto | **No lo es**: es la propiedad esperada de una derivación con material aleatorio por credencial. Las dos verifican | `CU-06006` FA-03 y CA-04 |
| Se quiere guardar la contraseña provisoria «por las dudas» | **No se guarda, no se registra y no se vuelve a producir.** Si se pierde, se resetea de nuevo | `CU-06007` §7 y CA-07. Guardarla vaciaría la regla que existe para que la clave no quede circulando |
| El arranque se detiene con `MIGRATION_NOT_APPLICABLE` | El esquema del almacén no corresponde al linaje de transformaciones. Causa frecuente: **se editó una transformación ya fusionada** | Revisar el despliegue: restaurar el respaldo o revisar la transformación. **No descartar el almacén**: es el atajo que deja el servicio impecable y sin los trabajos de nadie |
| El arranque se detiene con `STORE_PATH_UNAVAILABLE` | El volumen persistente no está montado | Revisar el montaje. **No caer hacia una ruta dentro de la imagen**: el servicio arrancaría y perdería todo en el siguiente reemplazo de versión |
| Se busca el identificador del puerto de repositorio de cuentas y no aparece en el intake | Es correcto: el intake nombra tres puertos y no éste. Está declarado como **punto abierto** aguas arriba y esta sección **no lo reabre** | `Especificacion-Funcional.md` §11 |
| Se busca si se ancla PBKDF2 o Argon2 | El intake declara «PBKDF2 o Argon2» y **no elige**. Es un punto abierto | `CU-06006` §10. El contrato declara la propiedad —nunca en claro, nunca resumen simple— y no el mecanismo |
| Se busca qué hace el validador con el texto del escenario **E-8** | **Está declarado, y no es un punto abierto.** El `PRODUCT-INTAKE` **1.12** lo resolvió en §20.E-8 punto 5 y en la fila «Dimensión no legible» de §21: el desenlace del envío es **error**, el trabajo **queda en `Borrador`** y no pasa a `Pendiente`, con el mensaje localizado por índice de figura y campo. La condición `UNREADABLE_DIMENSION`, en cambio, sigue siendo de la fachada del visor y no de esta capa | `Definicion-Contrato-Del-Validador-De-Figuras.md` §6 y §7, y `CU-06001` CA-12 |
| Se quiere agregar un texto de prueba nuevo | **No se inventan textos de prueba.** Es una regla de delivery del producto, y por eso los escenarios del intake son parte de su contrato | Usar los escenarios `E-1` a `E-7`. Si hace falta uno nuevo, lo decide el Product Owner sobre el intake |

## 5. Próximos pasos

### 5.1 `GeometriaFactory-Api`

- **Para implementar un punto de acceso**: el caso de uso que lo describe, en [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/), y la fila del punto en la tabla de la superficie.
- **Para traducir un motivo nuevo**: [`../02-Especificacion-Funcional/Casos-De-Uso/CU-00009-Traducir-El-Motivo-Del-Contrato-A-Respuesta-De-Protocolo.md`](../05-Arquitectura-Tecnica/Operaciones-Internas/CU-00009-Traducir-El-Motivo-Del-Contrato-A-Respuesta-De-Protocolo.md), **entero**, y en particular su §10.
- **Para entender por qué la superficie es como es**: [`../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md`](../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md) §5 y §7.
- **Para el vocabulario**: [`Glosario-UX.md`](Glosario-UX.md) y [`../02-Especificacion-Funcional/Glosario-Funcional.md`](../02-Especificacion-Funcional/Glosario-Funcional.md) §3, sobre todo por las polisemias de «acceso» y «código».
- **Para saber qué está sin decidir**: `../02-Especificacion-Funcional/Especificacion-Funcional.md` §11, y en particular los **dos huecos elevados al Product Owner**.

### 5.2 `GeometriaFactory-Domain`

Los cuatro modos de documentación, con el orden en que conviene visitarlos después de esta guía. El plan completo está en [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §4.

| Modo | Ir a | Cuándo |
| --- | --- | --- |
| Tutorial | Esta guía | Es la que se acaba de recorrer. No hay otra |
| How-to | [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/), los trece contratos de uso | Cuando hay que invocar una operación concreta y saber qué resolver antes |
| Reference | [`../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md`](../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md) para entidades y transiciones; [`DX-Error-Messages.md`](DX-Error-Messages.md) para las condiciones de error; [`Glosario-UX.md`](Glosario-UX.md) y [`../02-Especificacion-Funcional/Glosario-Funcional.md`](../02-Especificacion-Funcional/Glosario-Funcional.md) para el vocabulario | Cuando hace falta un dato puntual |
| Explanation | [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §1.2 y §1.3; `Definicion-Modelo-De-Dominio.md` §4, §6 y §7; la §7 de esta guía | Cuando la pregunta es «por qué está así» |

Punto de entrada recomendado de la sección de especificación funcional: su `README.md` propone un orden de lectura de ocho pasos que este onboarding no duplica.

### 5.3 `GeometriaFactory-Application`

Los cuatro modos de documentación, con el orden en que conviene visitarlos después de esta guía. El plan completo está en [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §4.

| Modo | Ir a | Cuándo |
| --- | --- | --- |
| Tutorial | Esta guía | Es la que se acaba de recorrer. No hay otra |
| How-to | [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/), los once contratos de uso | Cuando hay que invocar una operación concreta y saber qué resolver antes |
| Reference | [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3 y §4 para los puertos y las comprobaciones; [`DX-Error-Messages.md`](DX-Error-Messages.md) para las condiciones de error; [`Glosario-UX.md`](Glosario-UX.md) y [`../02-Especificacion-Funcional/Glosario-Funcional.md`](../02-Especificacion-Funcional/Glosario-Funcional.md) para el vocabulario | Cuando hace falta un dato puntual |
| Explanation | [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §1.2, §1.3 y §1.4; `Especificacion-Funcional.md` §1, §4 y §8; la §7 de esta guía | Cuando la pregunta es «por qué está así» |

Punto de entrada recomendado de la sección de especificación funcional: su [`README.md`](../02-Especificacion-Funcional/README.md) propone un orden de lectura que este onboarding no duplica. Y para el lector que llega desde el dominio, la tabla de §7.4 del índice de 02 dice qué caso de uso de `GeometriaFactory-Domain` orquesta cada uno de los once de acá.

### 5.4 `GeometriaFactory-Infrastructure`

| Modo | Ir a | Cuándo |
| --- | --- | --- |
| Tutorial | Esta guía | Es la que se acaba de recorrer. No hay otra |
| How-to | [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/), los diez contratos | Cuando hay que implementar un adaptador concreto y saber qué garantías sostener |
| Reference | [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3 y §4; [`../02-Especificacion-Funcional/Modelo-Datos/Modelo-Conceptual.md`](../02-Especificacion-Funcional/Modelo-Datos/Modelo-Conceptual.md) para el dato guardado; [`DX-Error-Messages.md`](DX-Error-Messages.md) para las condiciones; los dos glosarios | Cuando hace falta un dato puntual |
| Explanation | **[`../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md`](../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md)**, entero; [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §1.2, §1.3 y §1.4; la §7 de esta guía | Cuando la pregunta es «por qué está así» |

Punto de entrada recomendado de la sección de especificación funcional: su [`README.md`](../02-Especificacion-Funcional/README.md) propone un orden de lectura de siete pasos que este onboarding no duplica.

## 6. Las tres cosas que no fallan

### 6.1 `GeometriaFactory-Api`

### 6.1 Por qué son la parte más importante de esta guía

Todo lo de arriba se aprende equivocándose: si se manda mal una petición, el servicio responde mal y se corrige. **Lo de esta sección no.** Son tres defectos que **dejan el sistema funcionando**: ninguna prueba los encuentra si no está escrita a propósito, y ninguna capa de adentro los puede reparar, porque las de adentro habrían hecho su parte bien.

Las tres tienen métrica propia en [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §6, con objetivo **cero y sin tolerancia**.

### 6.2 Las tres, una por una

| Qué | El atajo o el descuido | Qué se rompe, y por qué no se nota |
| --- | --- | --- |
| **Un punto de acceso fuera de la guardia** | Agregar un punto y no ponerlo bajo la comprobación de acceso, papel y marca. Es un descuido, no un atajo: nadie lo decide | Rompe **RN-00013**. Una cuenta con la provisoria sin cambiar —cuya contraseña **el administrador conoce**— puede operar por ese punto. El punto funciona perfecto, responde bien y no falla nunca. **Se detecta contando**: los puntos que exigen acceso y los que están guardados tienen que ser el mismo número |
| **Distinguir el recurso ajeno del inexistente** | Responder «no autorizado» donde corresponde «no encontrado», que es lo que la mayoría de los servicios hacen y parece más correcto | Rompe **RN-00003**. Permite averiguar por tanteo qué identificadores existen, y en un laboratorio de aula eso es saber qué entregó cada compañero. **Se detecta comparando** dos respuestas que tienen que ser idénticas |
| **Normalizar el texto del alumno en el borde** | Reserializarlo como si fuera JSON, normalizar su codificación, recortarlo por un límite de tamaño | Rompe **RN-00008**. El texto del alumno **no es JSON estrictamente válido**: trae comas finales y claves que un lector estricto rechaza. Normalizarlo hace que el alumno abra su trabajo y vea un texto que no escribió, y que el escenario que documenta la tolerancia deje de ser reproducible desde el almacén. **Se detecta comparando** el texto guardado con el enviado, carácter por carácter |

La regla que resume las tres, y que conviene poder recitar: **lo que no falla hay que ir a buscarlo, contando, comparando o inspeccionando.**

Y una cuarta de la misma familia, que no es una regla de negocio sino de arquitectura: **exponer en un mensaje la ruta del almacén, la dirección de un servicio interno o una traza de implementación**. Tampoco falla; simplemente le entrega a quien mire la respuesta algo que no debería tener. Está en [`DX-Error-Messages.md`](DX-Error-Messages.md) §1.4.

### 6.3 Dónde va un punto de acceso nuevo

El procedimiento, en cinco preguntas, y **en este orden**:

1. **¿Existe ya?** Mirar los quince. La mitad de los puntos que alguien quiere agregar son una variante de uno que ya está, y una variante en la ruta suele ser información sobre el solicitante en un lugar donde no hace falta —**es el motivo por el que la eliminación es un solo punto y no dos**—.
2. **¿Qué tipo transporta?** Si no hay un tipo del ensamblado de contratos que le sirva, **el punto no se agrega todavía**: los tipos son de aquel proyecto de código y esta capa no los declara.
3. **¿Qué papel exige, y va bajo la guardia?** Desde `PRODUCT-INTAKE` 1.13 **no queda ninguna situación en la que un punto que escribe no pueda exigir acceso**: la que había —el establecimiento de la contraseña del primer ingreso— se suprimió con **RN-00016**, y los cuatro puntos que no exigen acceso firmado no fijan contraseñas. Salvo que se trate de uno de esos cuatro, **va bajo la guardia**. Agregarlo a la tabla de puntos y a la cuenta de la métrica es parte de agregarlo.
4. **¿Qué puede fallar, y con qué código del conjunto cerrado?** Si ningún código corresponde, **no se inventa uno**: se usa el genérico y **se declara el hueco**, como están declarados los dos que hoy existen.
5. **¿Qué agrega a la colección de peticiones?** Un punto que no se puede ejercitar desde la colección es un punto que nadie va a volver a probar.

## 7. Dónde va una regla nueva

### 7.1 `GeometriaFactory-Domain`

Este es el tramo de una hora y el que más rinde a largo plazo. La pregunta que responde es: aparece una regla nueva, ¿va como guarda de una entidad de este proyecto de código, o va en otra capa?

### 7.1 Dieciséis reglas, nueve invariantes

Los invariantes **no son reglas distintas** de las **dieciséis** del negocio: son las mismas vistas desde el dominio. La regla declara qué decidió el negocio; el invariante declara qué condición sobre los datos no puede romperse nunca, sin importar la operación ni quién la ejecute, aunque la petición llegue por fuera de la interfaz.

**Diez** de las dieciséis reglas tienen invariante asociado y **seis** no; **RN-02012, RN-02013 y RN-02016 comparten INV-09**, que es el único invariante que sostiene más de una regla — y la lectura que sostiene esa fila es la **columna** «regla de negocio que sostiene» de `PRODUCT-INTAKE` §17.1.P.2 · GeometriaFactory-Domain, no su prosa, que enumera a RN-02012 entre las que no tienen ninguno (`02-Especificacion-Funcional/Especificacion-Funcional.md` §4). La correspondencia es la de `Definicion-Modelo-De-Dominio.md` §4.3 y se transcribe acá porque es el corazón de este tramo:

| Regla | Enunciado abreviado | Invariante que la expresa como condición permanente |
| --- | --- | --- |
| RN-02001 | Existe exactamente un administrador; su alta sólo es posible mientras no exista ninguno | INV-05 |
| RN-02002 | El correo del alumno es único | INV-01 |
| RN-02003 | Un alumno sólo ve y opera sus propios trabajos | INV-02 |
| RN-02004 | El alumno elimina sólo en `Borrador`; el administrador, cualquier trabajo que ve | INV-03 |
| RN-02005 | Un trabajo no pasa a estado `Pendiente` con errores de validación; las advertencias sí lo permiten | INV-04 |
| RN-02006 | Una cuenta `Pendiente` o `Bloqueado` no obtiene acceso | INV-06 |
| RN-02007 | La baja arrastra los trabajos y exige confirmación escrita | — |
| RN-02008 | El texto original del alumno se conserva íntegro | — |
| RN-02009 | Toda observación de error indica la posición de la pieza y el campo | — |
| RN-02010 | El desenlace es exclusivo del administrador y los dos estados de cierre son terminales | INV-07 |
| RN-02011 | El administrador no ve los trabajos en `Borrador` | — |
| RN-02012 | El reseteo de contraseña conserva la cuenta y todos sus trabajos | INV-09 |
| RN-02013 | Con la contraseña provisoria sin cambiar, la cuenta no llega a ninguna otra parte | INV-09 |
| RN-02014 | La contraseña provisoria la produce el sistema: no es adivinable y no se repite | — |
| RN-02015 | Resetear no exige que la cuenta esté habilitada | — |
| RN-02016 | Habilitar una cuenta produce su contraseña provisoria | INV-09 |

### 7.2 Por qué seis reglas no tienen invariante

No es un olvido y no hay que «completarlas». El motivo está declarado en `PRODUCT-INTAKE` §17.1.P.2 · GeometriaFactory-Domain y es el criterio que se reusa cada vez que aparece una regla nueva:

- **RN-02007, RN-02008 y RN-02009 describen comportamientos**, no condiciones permanentes sobre el estado. «La baja arrastra los trabajos», «el texto no se reescribe» y «el error indica dónde está» son cosas que el sistema **hace** en un momento; no son afirmaciones que tengan que ser verdaderas siempre sobre cualquier dato guardado.
- **RN-02011 es una regla de alcance de consulta.** Restringe qué devuelve un listado, y un listado no es un dato: no hay ningún estado que la regla vuelva imposible.
- **RN-02014 describe cómo se produce un valor**, y además **no se ejerce en este proyecto de código**: la contraseña provisoria llega al dominio ya derivada, de modo que acá no hay con qué comprobar que no sea adivinable ni que no se repita. Se ejerce donde el valor nace, en `GeometriaFactory-Application` CU-02011 y en `GeometriaFactory-Contracts` CU-02008.
- **RN-02016 sí tiene invariante, y es INV-09.** No enuncia un comportamiento: enuncia una condición sobre los datos —ninguna cuenta de alumno `Habilitado` sin credencial derivada, y ninguna habilitación sin la marca puesta—. Lo que agrega a INV-09 no es una mitad nueva de la condición sino un **segundo origen** de la marca, junto al reseteo.
- **RN-02015 enuncia la ausencia de una precondición.** «Resetear no exige cuenta habilitada» no vuelve imposible ningún estado: retira una comprobación. Un invariante que la expresara tendría que afirmar algo, y no hay nada que afirmar.

Dos precisiones de ubicación, que evitan que alguien busque en la capa equivocada:

- **INV-01 es del sistema y el dominio no lo puede verificar solo.** La unicidad se afirma sobre el conjunto de alumnos y una entidad no conoce a ese conjunto. Por eso el dominio la **exige declarada** —y rechaza con `EMAIL_UNIQUENESS_NOT_VERIFIED` si no lo está— mientras quien la ejerce efectivamente es `GeometriaFactory-Application` con su puerto de repositorio.
- **INV-06 se cumple aunque el acceso se materialice afuera.** El dominio modela la condición; el mecanismo que emite el acceso vive en `GeometriaFactory-Infrastructure` y en `GeometriaFactory-Api`. Es la frontera de autenticación de [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §1.3.

### 7.3 El procedimiento de decisión

Ante una regla nueva, en este orden:

1. **¿Es una afirmación que tiene que ser verdadera siempre sobre el estado de una entidad, sin importar la operación ni quién la ejecute?**
   Si la respuesta es no, no es invariante: va como comportamiento en la capa que lo ejecuta, o como alcance de consulta en la capa que consulta. Termina acá.
2. **Si es sí: ¿la condición se afirma sobre una entidad, o sobre un conjunto de entidades?**
   Sobre una entidad, va como **guarda de esa entidad** en este proyecto de código, con su código de condición y su entrada en el catálogo.
   Sobre un conjunto, el dominio **no la puede verificar solo**: se modela como exigencia declarada —el consumidor afirma haberla resuelto y el dominio rechaza si no lo declara, como INV-01— y quien la ejerce es la capa de aplicación.
3. **¿La regla condiciona un mecanismo que el dominio no implementa** —acceso, persistencia, transporte, serialización, cálculo del valor derivado, interpretación del texto—?
   Entonces acá va **la condición** y afuera va el mecanismo, como INV-06. Nunca las dos cosas en el mismo lugar, y nunca el mecanismo acá.
4. **Si la regla entra:** sube el enunciado a `02-Especificacion-Funcional/`, obtiene su identificador `RN-XX` y su `INV-XX` si corresponde, aparece en el §6 de algún caso de uso como código de condición, y **recién entonces** entra al catálogo de [`DX-Error-Messages.md`](DX-Error-Messages.md). El catálogo no inventa condiciones: las deriva.

La tabla de fronteras de `Definicion-Modelo-De-Dominio.md` §7 es la referencia rápida del paso 3: enumera las ocho responsabilidades que este proyecto de código no tiene y dónde vive cada una.

## 8. La inversión de dependencias, en la práctica

### 8.1 `GeometriaFactory-Application`

Este es el tramo de una hora y el que más rinde a largo plazo. La pregunta que responde es: **qué hace exactamente que esta capa se pueda probar entera sin nada, y qué la rompería.**

El enunciado, una vez: esta capa **declara** qué necesita y otra capa lo provee. Un caso de uso que mencionara el motor de persistencia, el mecanismo de acceso o el protocolo de transporte estaría mal ubicado (`Especificacion-Funcional.md` §1).

### 7.1 Los cuatro puertos y qué le pide esta capa a cada uno

La tabla es la de `Especificacion-Funcional.md` §3, y se transcribe acá porque es el corazón de este tramo. **Lo que hay que leer es la columna del medio**: es la única forma correcta de pensar un puerto, porque enuncia una necesidad y no una implementación.

| Puerto | Qué le pide esta capa | Casos de uso que lo consumen | Qué NO le pide |
| --- | --- | --- | --- |
| Repositorio de trabajos | Recuperar un trabajo, resolver una consulta **ya acotada** por dueño o por alcance, materializar el resultado y ejecutar el retiro | CU-04002, CU-04004, CU-04005, CU-04006, CU-04007, CU-04008, CU-04009 | Una consulta ad-hoc que el caso de uso arme; los componentes de las piezas en un listado |
| Repositorio de cuentas | Recuperar una cuenta por su correo, responder si un correo ya está registrado y **si ya existe una cuenta con papel `Administrador`**, y materializar el resultado | CU-04001, CU-04002, CU-04003, CU-04007, CU-04010 | Comparar credenciales; decidir si el correo es «el mismo» según un criterio que esta capa no fijó |
| Validación de figuras | Interpretar el texto original y devolver **la cantidad de figuras del conjunto raíz**, las piezas reconstruidas y las observaciones, con su especie y su ubicación | CU-04005 | Decidir el estado del trabajo; corregir el texto del alumno; emitir un conjunto de piezas o de observaciones mal formado |
| Reloj del sistema | Los sellos de alta, de modificación y de desenlace | CU-04001, CU-04003, CU-04004, CU-04005, CU-04008, CU-04010 | Nada más: es el puerto más chico y el más instructivo. Ver §7.2. **CU-04002 no lo consume**: el modelo del dominio no declara fecha de última modificación de la cuenta, de modo que sus cuatro operaciones no registran ningún sello |

**Dos datos que viajan por esos puertos y que conviene reconocer, porque los dos se malinterpretan:**

- **Los sellos son metadatos de orquestación de esta capa**, no atributos del dominio: el modelo declara la fecha de alta del alumno —que recibe del consumidor— y la «Fecha» que el alumno declara en su trabajo, y nada más. La discrepancia está elevada al Product Owner y declarada como punto abierto (`Especificacion-Funcional.md` §3 y §11).
- **La cantidad de figuras del conjunto raíz** entra por el puerto de validación junto con las piezas y las observaciones, y CU-04005 la hace viajar hasta el dominio. **No es derivable de las piezas adoptadas**, porque la posición de una figura que no se pudo reconstruir queda reservada y el conjunto admite huecos. Es el rango contra el que el dominio valida la posición de cada observación: sin ella, RN-04009 deja de ser comprobable. CU-04005 es el único orquestador de la reconstrucción y del registro de observaciones, de modo que es el único que puede aportarla.

**El identificador del puerto de repositorio de cuentas es un punto abierto declarado.** El intake nombra tres —`IWorkRepository`, `IFigureValidator` e `ISystemClock`— y no éste, que la orquestación de las cuentas y la verificación de unicidad del correo necesitan. No es una regla nueva ni una decisión de alcance: es un nombre, y su definición se difiere a `05-Arquitectura-Tecnica` y al punto de control de la primera etapa (`Especificacion-Funcional.md` §11). Esta guía no lo reabre y lo nombra en lenguaje de dominio.

### 7.2 Por qué el reloj es un puerto

Es la decisión que más sorprende y la que mejor explica el estilo, así que conviene entenderla antes que ninguna otra. Está declarada como decisión pre-tomada en `PRODUCT-INTAKE` §17.1.P.11 · GeometriaFactory-Application punto 3, y el motivo es explícito: **para que las fechas de alta y de modificación sean verificables en prueba** —que es lo que esta capa llama, desde la corrección de la ronda r1, los **sellos** de alta, de modificación y de desenlace—.

Sin puerto de reloj, un criterio de aceptación como CA-01 de CU-04001 —que exige devolver la cuenta «con sello de alta 2026-03-15»— no se puede escribir sin trucos. Con puerto de reloj se escribe en una línea: el doble devuelve ese sello y la prueba compara. La misma lógica vale para el validador y para los dos repositorios: **si algo no se puede fijar desde la prueba, es porque no pasó por un puerto**, y esa es la señal más barata de que un caso de uso está mal construido.

Consecuencia práctica para quien escribe un caso de uso nuevo: **el sello nunca se toma del ambiente**. Se pide al puerto, aunque parezca ceremonia para una línea de código.

### 7.3 Dónde va algo nuevo

Ante una capacidad nueva, en este orden:

1. **¿Es una condición sobre una entidad, que tiene que ser verdadera siempre?**
   Entonces no va acá: es un invariante y va como guarda de la entidad en `GeometriaFactory-Domain`. Esta capa **ejerce** las reglas sobre el pedido concreto, no las enuncia (`Especificacion-Funcional.md` §6). Termina acá.
2. **¿Exige conocer un conjunto de entidades, o el momento actual, o un dato que hay que interpretar?**
   Entonces sí es de esta capa, y va como **caso de uso apoyado en un puerto**. Es el caso de la unicidad del correo, que exige el conjunto de cuentas y por eso se verifica acá y no en el dominio (CU-04001 §10).
3. **¿Necesita un puerto que todavía no existe?**
   Antes de declararlo, verificar que la necesidad se pueda enunciar **sin nombrar el mecanismo**. Si el enunciado no se puede escribir sin decir «base de datos», «HTTP» o «archivo», lo que hace falta no es un puerto nuevo sino repensar qué se le está pidiendo. Un puerto nuevo se declara acá y se implementa en `GeometriaFactory-Infrastructure`; su identificador, como el del repositorio de cuentas, lo fija 05.
4. **¿Es un mecanismo —guardar, autenticar, derivar, transportar, serializar, interpretar—?**
   Entonces acá va **la orquestación** y afuera va el mecanismo. Nunca las dos cosas en el mismo lugar, y nunca el mecanismo acá.
5. **Si la capacidad entra:** sube a [`../02-Especificacion-Funcional/`](../02-Especificacion-Funcional/) como caso de uso con su identificador `CU-XX`, declara qué puertos consume, sus condiciones de error aparecen en su §6, y **recién entonces** entran al catálogo de [`DX-Error-Messages.md`](DX-Error-Messages.md). El catálogo no inventa condiciones: las deriva.

La prueba de humo de todo el procedimiento, que se puede aplicar sin leer nada más: **si el caso de uso nuevo no se puede ejercer entero con dobles, algo se coló que no pasó por un puerto.**

## 9. Los tres atajos que no fallan

### 9.1 `GeometriaFactory-Infrastructure`

### 7.1 Por qué son la parte más importante de esta guía

Este es el tramo que más rinde a largo plazo, y la pregunta que responde es: **qué defectos de esta capa no los va a encontrar ninguna prueba que no esté escrita a propósito.**

El patrón es siempre el mismo, y conviene poder reconocerlo antes de ver los casos: **la capa depende de cosas del mundo que pueden no responder**, y ante cada una hay una salida que deja el sistema funcionando. Esa salida nunca lanza un error, nunca rompe una prueba y nunca aparece en un registro. Simplemente el producto deja de cumplir una promesa, y nadie se entera hasta que alguien la necesita.

**La regla que resume las tres, y que hay que poder recitar: cuando el mecanismo no puede cumplir su promesa, se detiene y lo dice. No la cumple a medias.**

### 7.2 Los tres, uno por uno

| El atajo | Qué parece | Qué produce de verdad | Qué corresponde |
| --- | --- | --- | --- |
| **Componer la contraseña provisoria con un contador, con la fecha o con el correo** cuando la fuente de aleatoriedad no responde | Que el reseteo funcionó | Una provisoria **adivinable**. Y reproduce exactamente el defecto que la regla vino a cerrar: si la escribiera el docente, terminaría siendo la misma clave para toda la comisión — con un contador, termina siendo predecible **sin que nadie lo haya decidido** | Devolver `RANDOMNESS_SOURCE_UNAVAILABLE` y **no completar el reseteo**. Un reseteo que no se completa es recuperable: se vuelve a intentar |
| **Generar una clave de firma al vuelo**, o emitir sin firmar, cuando no hay clave provista | Que el servicio arrancó bien | Accesos que nadie puede verificar, o que cambian de validez en cada reinicio. **Nadie lo nota hasta que alguien falsifica uno** | Devolver `SIGNING_KEY_MISSING`. La clave se provee en el despliegue, por variable de entorno o archivo montado, y **no entra al repositorio de código ni a la imagen** |
| **Caer hacia una ruta dentro de la imagen** cuando el volumen no está montado | Que el almacén se creó y todo anda | El servicio acepta los trabajos de la comisión entera **y los pierde en el siguiente reemplazo de versión**. Nadie se entera hasta que alguien busca su trabajo y no está | Devolver `STORE_PATH_UNAVAILABLE` y **detener el arranque**. Es preferible un servicio caído y explicado a uno en pie sobre un almacén equivocado |

**Y un cuarto de la misma familia, el más destructivo de todos:** ante un esquema que no corresponde al linaje de transformaciones, **descartar el almacén y crearlo de nuevo**. Deja el servicio impecable y sin los trabajos de nadie. Corresponde `MIGRATION_NOT_APPLICABLE`, arranque detenido, y que una persona decida si restaura el respaldo o revisa la transformación.

**Las tres reglas de negocio que sólo se rompen acá siguen el mismo patrón**, y por eso conviene leerlas juntas con esto: normalizar el texto al guardarlo no falla, compactar las posiciones no falla, y componer la provisoria por otro medio no falla. **Las tres se rompen produciendo algo válido**, y por eso sus criterios de aceptación comparan, cuentan o inspeccionan en lugar de esperar que algo se caiga.

### 7.3 Dónde va algo nuevo

Ante una capacidad nueva, en este orden:

1. **¿Es una condición sobre una entidad, que tiene que ser verdadera siempre?**
   No va acá: es un invariante y va al dominio. Termina acá.
2. **¿Es una decisión sobre quién puede hacer qué, o sobre qué estado resulta?**
   Tampoco: es de la capa de aplicación o del dominio. **Acá no se decide nada.**
3. **¿Es un mecanismo —leer, guardar, derivar, firmar, transformar el esquema—?**
   Entonces sí es de acá, y va como **adaptador de un puerto ya declarado**. Si el puerto no existe, **se declara arriba y no acá**: un puerto nuevo es una decisión de la capa que lo necesita.
4. **¿Cambia lo que el producto guarda?**
   Entonces toca el modelo de datos, y hay dos preguntas antes de escribir: ¿alguna de las siete reglas conceptuales lo prohíbe?, y ¿el modelo del dominio lo declara? **Si el dominio no lo declara, la capacidad entra por el dominio y no por acá.**
5. **¿Depende de algo del mundo que puede no responder?**
   Entonces necesita una condición de error, y **la condición se elige por su forma de terminación**: negativa sin escritura si el defecto es del pedido, degradada si el mundo no respondió, arranque detenido si el servicio no puede operar con confianza. Y hay que preguntarse, explícitamente, **cuál es el atajo tentador y por qué está prohibido**: si el atajo existe, va escrito en la acción sugerida.
6. **Si la capacidad entra:** sube a [`../02-Especificacion-Funcional/`](../02-Especificacion-Funcional/) como caso de uso con su identificador, declara qué puerto implementa, sus condiciones aparecen en su §6, y **recién entonces** entran al catálogo de [`DX-Error-Messages.md`](DX-Error-Messages.md). El catálogo no inventa condiciones: las deriva.

La prueba de humo de todo el procedimiento: **si lo nuevo decide algo en lugar de hacer algo, está en la capa equivocada.**

## 10. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 2.1 | 2026-08-29 | **Tramo `R-3a` del renombre `F-03`** —«los 101 códigos de condición van a inglés», decisión del Product Owner del 2026-08-12, reconfirmada el 2026-08-29—, que **reanuda los tramos que la [`Norma-De-Nomenclatura.md`](../../../Producto/Norma-De-Nomenclatura.md) **1.5** suspendió el 2026-08-13**. **30 ocurrencias** pasan de la forma castellana a la vigente, con el mapeo de **§6.8** —101 pares— y **sin elegir ninguno acá**. Se respeta **§4.1**: no se tocan las filas de control de cambios ni lo que está entre «…». **Ninguna palabra de prosa cambia**: el control de diff verificó que las 362 líneas modificadas del tramo difieren **exactamente** en un par del glosario y en nada más. | AG-00030 |
| 2.0 | 2026-08-16 | **Consolidación de la fusión** (`Audit/Migracion-M10-Consolidacion-Fusion.md` 1.2 §4). Pasa de ser el documento de un proyecto de código a ser el de la **unidad de entrega**, con una subsección por proyecto y su texto transpuesto **sin reescritura**. Entra **§0**. Los absorbidos quedan archivados. Sube **major**. |
