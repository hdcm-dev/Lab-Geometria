# Guía de onboarding — la primera hora contra el dato real del alumno

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** Guia-Onboarding-Developer.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** DX Lead (AG-03)
**Variante:** DX
**Trazabilidad upstream:** `02-Especificacion-Funcional/Especificacion-Funcional.md` §1, §3, §4, §6, §8 y §11; `02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md` §2, §3, §4, §5, §6, §7, §8 y §9; `02-Especificacion-Funcional/Modelo-Datos/` completo; CU-01 §4, §5, §6, §8 y §10; CU-02 §4, §5, §6, §8 y §10; CU-03 §4, §6, §8 y §10; CU-04 §6, §8 y §10; CU-05 §5, §6 y §10; CU-06 §5, §6 y §10; **CU-07 §1, §4, §6, §8 y §10**; CU-08 §6 y §10; CU-09 §6 y §10; CU-10 §5, §6, §8 y §10; `02-Especificacion-Funcional/Glosario-Funcional.md` §2 y §3; RN-01 a RN-15 de `Proyectos/GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/`; `00-Contexto/Vision-Producto.md` §9; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.12** §11, §16, §17.3 y §20
**Trazabilidad downstream:** `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas`, `09-Devops` y `11-Documentacion` de GeometriaFactory-Infrastructure

---

## Tabla de contenido

- [1. Audiencia y prerrequisitos](#1-audiencia-y-prerrequisitos)
- [2. Instalación o acceso](#2-instalación-o-acceso)
- [3. Primer ejemplo ejecutable](#3-primer-ejemplo-ejecutable)
  - [3.1 El resultado que se busca](#31-el-resultado-que-se-busca)
  - [3.2 Los pasos](#32-los-pasos)
  - [3.3 El primer ejemplo con sentido: el texto que rompe a un lector estricto](#33-el-primer-ejemplo-con-sentido-el-texto-que-rompe-a-un-lector-estricto)
  - [3.4 El segundo ejemplo: los dos cubos que parecen iguales](#34-el-segundo-ejemplo-los-dos-cubos-que-parecen-iguales)
  - [3.5 Leer una condición de error](#35-leer-una-condición-de-error)
- [4. Diagnóstico de problemas frecuentes en la primera hora](#4-diagnóstico-de-problemas-frecuentes-en-la-primera-hora)
- [5. Próximos pasos](#5-próximos-pasos)
- [6. Control de cambios](#6-control-de-cambios)
- [7. Los tres atajos que no fallan](#7-los-tres-atajos-que-no-fallan)
  - [7.1 Por qué son la parte más importante de esta guía](#71-por-qué-son-la-parte-más-importante-de-esta-guía)
  - [7.2 Los tres, uno por uno](#72-los-tres-uno-por-uno)
  - [7.3 Dónde va algo nuevo](#73-dónde-va-algo-nuevo)

---

## 1. Audiencia y prerrequisitos

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

El criterio que ancla todo esto es `CU-01` CA-01, y el de la advertencia es `CU-02` CA-06.

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

Y una tercera lección, que es la que decide el escenario semilla del producto: **el operador de la comparación es estricto**. En el escenario **E-1**, el área del cilindro declara 113.10 y la suma de sus componentes da 113.09: diferencia de **exactamente 0.01**. Con el operador estricto ese caso **no** advierte y el escenario da las **dos** advertencias documentadas; con «mayor o igual» daría **tres** y el caso de prueba canónico del producto fallaría. El criterio `CU-02` CA-09 lo ancla con dos diferencias, 0.010 y 0.011.

### 3.5 Leer una condición de error

Con el código a la vista, el recorrido de lectura es siempre el mismo:

1. **El código** dice qué no se pudo hacer: `RUTA_DEL_ALMACEN_NO_DISPONIBLE`.
2. **El catálogo** ([`DX-Error-Messages.md`](DX-Error-Messages.md)) dice qué pasó, por qué pasó y qué hacer —**y de qué lado hacerlo, que acá muchas veces es el del despliegue y no el del código**.
3. **El caso de uso** que lo declara (§6 de CU-10) dice cuál es la respuesta: el arranque se detiene y no se atiende ninguna petición.
4. **La regla conceptual de modelo o la de negocio** (§9 del mismo caso de uso) dice por qué la condición existe.

Cuatro saltos, todos con enlace. Si en alguna condición la cadena se corta, es un defecto de esta sección.

**Y la pregunta que hay que hacerse antes que ninguna otra: ¿esto es una condición de error o es un resultado?** La mayoría de lo que parece un fallo en esta capa es el funcionamiento normal del producto: un error de validación, un texto ilegible, cero advertencias, nada encontrado, un conjunto vacío, una credencial que no coincide y un acceso vencido **son resultados y no están en el catálogo**. Los siete están reunidos en [`DX-Error-Messages.md`](DX-Error-Messages.md) §1.2.

## 4. Diagnóstico de problemas frecuentes en la primera hora

| Síntoma | Causa probable | Qué hacer |
| --- | --- | --- |
| Un comando del quick-start no existe en el host | Se está ejecutando fuera del entorno de desarrollo contenido. El host no tiene las herramientas y no va a tenerlas | Abrir el repositorio de código en el entorno contenido que él mismo define y repetir desde el paso 0 |
| `./scripts/build.sh` termina en 0 pero con advertencias | La puerta de calidad exige 0 **y sin advertencias** para fusionar | Tratar la advertencia como bloqueante: no es un aviso, es un criterio de la etapa |
| El texto del alumno se rechaza entero y la prueba de E-2 falla | Se está leyendo con un lector estricto. **El texto no es JSON estrictamente válido** | Leer con tolerancia a comas finales y omisión de comentarios (`T2`). Ver §3.3 |
| El ortoedro no se reconstruye | Se está buscando la clave `"Bases"` y el programa emite `"Tapas"` | Aceptar **las dos como sinónimas** (`T1`). Es la línea que desbloquea el dibujo de todos los ortoedros |
| El cubo de `Ejemplo2` no se reconstruye | Se está aceptando sólo `"Cuadrado"` como tipo de cara | Aceptar también `"Rectangulo"` (`T3`). Son la misma cara emitida por dos programas |
| Una figura con `"Largo": 0.00` se descarta | Se está evaluando la **verosimilitud** del valor en lugar de la **existencia** del campo | Comparar por existencia. El escenario **E-6** es exactamente ese caso y exige que la figura se interprete |
| El escenario semilla devuelve **3** advertencias en lugar de 2 | El operador de comparación no es estricto | Advertir cuando la diferencia es **mayor** que la tolerancia, no mayor o igual. El área del cilindro de E-1 difiere en exactamente 0.01 |
| Se quiere «arreglar» el valor declarado que está mal | El producto **no corrige el dato del alumno**: lo señala. Es su mayor valor didáctico y una regla de negocio | Emitir la advertencia con **los dos valores** y dejar el texto intacto (RN-08) |
| Un texto ilegible devuelve `INTERPRETACION_NO_DISPONIBLE` | **Es la confusión más cara del producto.** Un texto que el alumno escribió mal es un **resultado**, no una avería | Devolver 0 figuras, 0 piezas y **una observación**. El criterio `CU-01` CA-10 existe para eso |
| Se busca dónde el validador decide el estado del trabajo | No lo decide. Entrega el conjunto de observaciones y **el dominio resuelve** | `Especificacion-Funcional.md` §4. Un validador que decidiera el estado tendría dentro una regla que no le pertenece |
| Se busca dónde comprobar que el solicitante es el dueño del trabajo | **Acá no se comprueba.** El recorte de la consulta **llega en el pedido**, ya decidido | `Especificacion-Funcional.md` §4. Lo único propio es negarse a resolver una consulta **sin** recorte |
| La consulta de listado devuelve los componentes de las piezas | El listado **no los incluye**, y es una decisión de modelado con efecto en el tiempo de respuesta | `CU-03` CA-04. El detalle sí los trae; el listado, nunca |
| Se piensa en compactar las posiciones de las piezas para no dejar huecos | **Compactar no falla: produce mensajes que apuntan a la figura equivocada** | `RC-02`. La posición de una figura no reconstruida queda **reservada** |
| Se intenta derivar la cantidad de figuras del conjunto raíz contando piezas | **No es derivable**: el conjunto admite huecos. La produce el validador al interpretar | `CU-01` §4 paso 3. Sin ese dato, la observación ubicada deja de ser comprobable |
| Se busca dónde se guarda la contraseña en claro | **En ningún lado.** Se guarda su valor derivado, y el valor en claro no sale de `CU-06` ni de `CU-07` | `CU-06` §7 y CA-07 |
| Dos derivaciones de la misma contraseña dan valores distintos y parece un defecto | **No lo es**: es la propiedad esperada de una derivación con material aleatorio por credencial. Las dos verifican | `CU-06` FA-03 y CA-04 |
| Se quiere guardar la contraseña provisoria «por las dudas» | **No se guarda, no se registra y no se vuelve a producir.** Si se pierde, se resetea de nuevo | `CU-07` §7 y CA-07. Guardarla vaciaría la regla que existe para que la clave no quede circulando |
| El arranque se detiene con `MIGRACION_NO_APLICABLE` | El esquema del almacén no corresponde al linaje de transformaciones. Causa frecuente: **se editó una transformación ya fusionada** | Revisar el despliegue: restaurar el respaldo o revisar la transformación. **No descartar el almacén**: es el atajo que deja el servicio impecable y sin los trabajos de nadie |
| El arranque se detiene con `RUTA_DEL_ALMACEN_NO_DISPONIBLE` | El volumen persistente no está montado | Revisar el montaje. **No caer hacia una ruta dentro de la imagen**: el servicio arrancaría y perdería todo en el siguiente reemplazo de versión |
| Se busca el identificador del puerto de repositorio de cuentas y no aparece en el intake | Es correcto: el intake nombra tres puertos y no éste. Está declarado como **punto abierto** aguas arriba y esta sección **no lo reabre** | `Especificacion-Funcional.md` §11 |
| Se busca si se ancla PBKDF2 o Argon2 | El intake declara «PBKDF2 o Argon2» y **no elige**. Es un punto abierto | `CU-06` §10. El contrato declara la propiedad —nunca en claro, nunca resumen simple— y no el mecanismo |
| Se busca qué hace el validador con el texto del escenario **E-8** | **Está declarado, y no es un punto abierto.** El `PRODUCT-INTAKE` **1.12** lo resolvió en §20.E-8 punto 5 y en la fila «Dimensión no legible» de §21: el desenlace del envío es **error**, el trabajo **queda en `Borrador`** y no pasa a `Pendiente`, con el mensaje localizado por índice de figura y campo. La condición `DIMENSION_NO_LEGIBLE`, en cambio, sigue siendo de la fachada del visor y no de esta capa | `Definicion-Contrato-Del-Validador-De-Figuras.md` §6 y §7, y `CU-01` CA-12 |
| Se quiere agregar un texto de prueba nuevo | **No se inventan textos de prueba.** Es una regla de delivery del producto, y por eso los escenarios del intake son parte de su contrato | Usar los escenarios `E-1` a `E-7`. Si hace falta uno nuevo, lo decide el Product Owner sobre el intake |

## 5. Próximos pasos

| Modo | Ir a | Cuándo |
| --- | --- | --- |
| Tutorial | Esta guía | Es la que se acaba de recorrer. No hay otra |
| How-to | [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/), los diez contratos | Cuando hay que implementar un adaptador concreto y saber qué garantías sostener |
| Reference | [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3 y §4; [`../02-Especificacion-Funcional/Modelo-Datos/Modelo-Conceptual.md`](../02-Especificacion-Funcional/Modelo-Datos/Modelo-Conceptual.md) para el dato guardado; [`DX-Error-Messages.md`](DX-Error-Messages.md) para las condiciones; los dos glosarios | Cuando hace falta un dato puntual |
| Explanation | **[`../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md`](../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md)**, entero; [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §1.2, §1.3 y §1.4; la §7 de esta guía | Cuando la pregunta es «por qué está así» |

Punto de entrada recomendado de la sección de especificación funcional: su [`README.md`](../02-Especificacion-Funcional/README.md) propone un orden de lectura de siete pasos que este onboarding no duplica.

## 6. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Recorrido de la primera hora para quien implementa un adaptador, para el mantenedor y para el agente de IA, más el docente que despliega a mano. Declara los prerrequisitos, con la lectura obligatoria del documento de concepto central y su motivo; el primer resultado exitoso como la batería del validador en verde **sin almacén**; el primer ejemplo con sentido sobre el **texto real del escenario E-2**, con las tres cosas que hay que ver; el contraste **E-3 contra E-4** con la lección del criterio negativo y la del operador estricto; el recorrido de cuatro saltos para leer una condición y la pregunta previa de si es condición o resultado; **veintitrés diagnósticos** de la primera hora; el enlace a los cuatro modos de Diátaxis; y la §7 propia sobre los tres atajos que no fallan, con el procedimiento de dónde va algo nuevo. Las seis secciones obligatorias conservan su numeración 1 a 6; el contenido propio va al final como §7 y no desplaza a ninguna. |
| 1.1 | 2026-08-10 | Ronda 2 de auditoría: correcciones de `SDD/Docs/Audit/B-02-03-GeometriaFactory-Infrastructure-r1.md` contra el `PRODUCT-INTAKE` **1.12**. **H-01**: la fila de «dónde buscar» sobre el escenario `E-8` decía que ninguna fuente declara qué hace el validador con su texto; el intake 1.12 lo declara en §20.E-8 punto 5 y en la fila «Dimensión no legible» de §21, de modo que la fila pasa a dar el resultado —error, trabajo en `Borrador`, mensaje localizado por índice de figura y campo—, a distinguirlo de la condición `DIMENSION_NO_LEGIBLE`, que sigue siendo de la fachada del visor, y a remitir a `Definicion-Contrato-Del-Validador-De-Figuras.md` §6 y §7 y a `CU-01` CA-12 en lugar de a la tabla de puntos abiertos. **H-02**: la trazabilidad upstream cita el `PRODUCT-INTAKE` **1.12**. |

## 7. Los tres atajos que no fallan

### 7.1 Por qué son la parte más importante de esta guía

Este es el tramo que más rinde a largo plazo, y la pregunta que responde es: **qué defectos de esta capa no los va a encontrar ninguna prueba que no esté escrita a propósito.**

El patrón es siempre el mismo, y conviene poder reconocerlo antes de ver los casos: **la capa depende de cosas del mundo que pueden no responder**, y ante cada una hay una salida que deja el sistema funcionando. Esa salida nunca lanza un error, nunca rompe una prueba y nunca aparece en un registro. Simplemente el producto deja de cumplir una promesa, y nadie se entera hasta que alguien la necesita.

**La regla que resume las tres, y que hay que poder recitar: cuando el mecanismo no puede cumplir su promesa, se detiene y lo dice. No la cumple a medias.**

### 7.2 Los tres, uno por uno

| El atajo | Qué parece | Qué produce de verdad | Qué corresponde |
| --- | --- | --- | --- |
| **Componer la contraseña provisoria con un contador, con la fecha o con el correo** cuando la fuente de aleatoriedad no responde | Que el reseteo funcionó | Una provisoria **adivinable**. Y reproduce exactamente el defecto que la regla vino a cerrar: si la escribiera el docente, terminaría siendo la misma clave para toda la comisión — con un contador, termina siendo predecible **sin que nadie lo haya decidido** | Devolver `FUENTE_DE_ALEATORIEDAD_NO_DISPONIBLE` y **no completar el reseteo**. Un reseteo que no se completa es recuperable: se vuelve a intentar |
| **Generar una clave de firma al vuelo**, o emitir sin firmar, cuando no hay clave provista | Que el servicio arrancó bien | Accesos que nadie puede verificar, o que cambian de validez en cada reinicio. **Nadie lo nota hasta que alguien falsifica uno** | Devolver `CLAVE_DE_FIRMA_AUSENTE`. La clave se provee en el despliegue, por variable de entorno o archivo montado, y **no entra al repositorio de código ni a la imagen** |
| **Caer hacia una ruta dentro de la imagen** cuando el volumen no está montado | Que el almacén se creó y todo anda | El servicio acepta los trabajos de la comisión entera **y los pierde en el siguiente reemplazo de versión**. Nadie se entera hasta que alguien busca su trabajo y no está | Devolver `RUTA_DEL_ALMACEN_NO_DISPONIBLE` y **detener el arranque**. Es preferible un servicio caído y explicado a uno en pie sobre un almacén equivocado |

**Y un cuarto de la misma familia, el más destructivo de todos:** ante un esquema que no corresponde al linaje de transformaciones, **descartar el almacén y crearlo de nuevo**. Deja el servicio impecable y sin los trabajos de nadie. Corresponde `MIGRACION_NO_APLICABLE`, arranque detenido, y que una persona decida si restaura el respaldo o revisa la transformación.

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
