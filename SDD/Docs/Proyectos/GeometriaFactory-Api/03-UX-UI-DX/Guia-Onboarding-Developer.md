# Guía de onboarding — la primera hora contra la superficie HTTP

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** Guia-Onboarding-Developer.md
**Versión:** 1.2
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** DX Lead (AG-03)
**Variante:** DX
**Trazabilidad upstream:** [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §1, §2, §3 y §5; [`DX-Error-Messages.md`](DX-Error-Messages.md) completo; `02-Especificacion-Funcional/Definicion-Superficie-HTTP.md` completo; `02-Especificacion-Funcional/Especificacion-Funcional.md` §3, §4, §6 y §11; §4, §6 y §8 de CU-01 a CU-12; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.26** §10 (host sin herramientas), §15 (no se inventan textos de prueba), §16, §18 y §20
**Trazabilidad downstream:** `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas`, `10-Examples` y `11-Documentacion` de GeometriaFactory-Api

---

## Tabla de contenido

- [1. Audiencia y prerrequisitos](#1-audiencia-y-prerrequisitos)
- [2. Instalación o acceso](#2-instalación-o-acceso)
- [3. Primer ejemplo ejecutable](#3-primer-ejemplo-ejecutable)
  - [3.1 El resultado que se busca](#31-el-resultado-que-se-busca)
  - [3.2 Los pasos](#32-los-pasos)
  - [3.3 El primer ejemplo con sentido: el envío que no verifica](#33-el-primer-ejemplo-con-sentido-el-envío-que-no-verifica)
  - [3.4 El segundo ejemplo: dos respuestas que tienen que ser iguales](#34-el-segundo-ejemplo-dos-respuestas-que-tienen-que-ser-iguales)
  - [3.5 Leer una respuesta de fallo](#35-leer-una-respuesta-de-fallo)
- [4. Diagnóstico de problemas frecuentes en la primera hora](#4-diagnóstico-de-problemas-frecuentes-en-la-primera-hora)
- [5. Próximos pasos](#5-próximos-pasos)
- [6. Las tres cosas que no fallan](#6-las-tres-cosas-que-no-fallan)
  - [6.1 Por qué son la parte más importante de esta guía](#61-por-qué-son-la-parte-más-importante-de-esta-guía)
  - [6.2 Las tres, una por una](#62-las-tres-una-por-una)
  - [6.3 Dónde va un punto de acceso nuevo](#63-dónde-va-un-punto-de-acceso-nuevo)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Audiencia y prerrequisitos

Esta guía es para quien va a tocar la superficie HTTP del producto: agregar o cambiar un punto de acceso, traducir un motivo nuevo, o escribir el cliente que la consume. Se supone que ya escribió servicios HTTP; **no** se supone que conozca este producto.

**Lectura obligatoria antes del paso 1**, y son dos:

1. [`../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md`](../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md) **§2 y §3, en ese orden**. §2 dice qué de la superficie está declarado por una fuente y qué es propuesta; §3 es la tabla de los quince puntos. **Leer §3 sin §2 hace creer que las quince rutas están decididas, y catorce no lo están.**
2. [`DX-Developer-Experience.md`](DX-Developer-Experience.md) **§1.4**, dos párrafos: las dos reglas del producto que se rompen desde acá **sin que nada falle**.

Lo que **no** hace falta leer para la primera hora: los doce casos de uso completos. Se consultan por tarea, cuando toque una.

## 2. Instalación o acceso

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

## 3. Primer ejemplo ejecutable

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

**Las dos respuestas tienen que ser indistinguibles**: mismo código de respuesta, mismo código del contrato, mismo cuerpo. Si difieren en algo —una fecha, una longitud, un texto— **RN-03 está rota**, y ninguna capa de adentro se enteró: la capa de aplicación devolvió el motivo correcto y esta capa lo tradujo mal.

Repetir el ejercicio pidiendo **eliminar** cada uno de los dos, que es el camino que el intake declara **bloqueante** y que exige verificarse **forzando la petición contra la superficie**, no ocultando un control en una pantalla.

### 3.5 Leer una respuesta de fallo

Toda respuesta de fallo de esta superficie tiene **dos identificadores** y hay que leerlos juntos:

- **El código de respuesta** dice de qué clase es el fallo, y es lo que decide qué hace el consumidor: corregir y reintentar, derivar, mostrar, o pasar a estado degradado.
- **El código del contrato** dice cuál exactamente, dentro de un conjunto cerrado de **quince**.

Dos excepciones declaradas, y conviene conocerlas para no buscar un código que no está: el `401` de la guardia y el `400` de una petición que no se puede leer **no llevan código del contrato**, porque ocurren antes de que haya un contrato con el que hablar.

El catálogo entero, con las **16** entradas y qué hace el consumidor con cada una, está en [`DX-Error-Messages.md`](DX-Error-Messages.md) §3. **Se consulta por código, no se lee de corrido.**

## 4. Diagnóstico de problemas frecuentes en la primera hora

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
| Se agregó un punto de acceso y una cuenta reseteada puede usarlo | **El punto quedó fuera de la guardia** | Ver §6.2. Es una de las tres cosas que no fallan |
| Dos respuestas que deberían ser iguales difieren | La traducción distinguió el recurso ajeno del inexistente | Ver §6.2. Es otra de las tres |
| El texto guardado no es idéntico al enviado | Algo lo normalizó en el borde: codificación, espacios, saltos de línea o un recorte por tamaño | Ver §6.2. Es la tercera |
| La colección se ejecuta dos veces y falla al principio | El administrador ya está configurado y el correo ya está registrado | Reiniciar el almacén con el guion del paso 3. **Es un resultado legítimo y la colección lo declara** |

## 5. Próximos pasos

- **Para implementar un punto de acceso**: el caso de uso que lo describe, en [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/), y la fila del punto en la tabla de la superficie.
- **Para traducir un motivo nuevo**: [`../02-Especificacion-Funcional/Casos-De-Uso/CU-09-Traducir-El-Motivo-Del-Contrato-A-Respuesta-De-Protocolo.md`](../02-Especificacion-Funcional/Casos-De-Uso/CU-09-Traducir-El-Motivo-Del-Contrato-A-Respuesta-De-Protocolo.md), **entero**, y en particular su §10.
- **Para entender por qué la superficie es como es**: [`../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md`](../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md) §5 y §7.
- **Para el vocabulario**: [`Glosario-UX.md`](Glosario-UX.md) y [`../02-Especificacion-Funcional/Glosario-Funcional.md`](../02-Especificacion-Funcional/Glosario-Funcional.md) §3, sobre todo por las polisemias de «acceso» y «código».
- **Para saber qué está sin decidir**: `../02-Especificacion-Funcional/Especificacion-Funcional.md` §11, y en particular los **dos huecos elevados al Product Owner**.

## 6. Las tres cosas que no fallan

### 6.1 Por qué son la parte más importante de esta guía

Todo lo de arriba se aprende equivocándose: si se manda mal una petición, el servicio responde mal y se corrige. **Lo de esta sección no.** Son tres defectos que **dejan el sistema funcionando**: ninguna prueba los encuentra si no está escrita a propósito, y ninguna capa de adentro los puede reparar, porque las de adentro habrían hecho su parte bien.

Las tres tienen métrica propia en [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §6, con objetivo **cero y sin tolerancia**.

### 6.2 Las tres, una por una

| Qué | El atajo o el descuido | Qué se rompe, y por qué no se nota |
| --- | --- | --- |
| **Un punto de acceso fuera de la guardia** | Agregar un punto y no ponerlo bajo la comprobación de acceso, papel y marca. Es un descuido, no un atajo: nadie lo decide | Rompe **RN-13**. Una cuenta con la provisoria sin cambiar —cuya contraseña **el administrador conoce**— puede operar por ese punto. El punto funciona perfecto, responde bien y no falla nunca. **Se detecta contando**: los puntos que exigen acceso y los que están guardados tienen que ser el mismo número |
| **Distinguir el recurso ajeno del inexistente** | Responder «no autorizado» donde corresponde «no encontrado», que es lo que la mayoría de los servicios hacen y parece más correcto | Rompe **RN-03**. Permite averiguar por tanteo qué identificadores existen, y en un laboratorio de aula eso es saber qué entregó cada compañero. **Se detecta comparando** dos respuestas que tienen que ser idénticas |
| **Normalizar el texto del alumno en el borde** | Reserializarlo como si fuera JSON, normalizar su codificación, recortarlo por un límite de tamaño | Rompe **RN-08**. El texto del alumno **no es JSON estrictamente válido**: trae comas finales y claves que un lector estricto rechaza. Normalizarlo hace que el alumno abra su trabajo y vea un texto que no escribió, y que el escenario que documenta la tolerancia deje de ser reproducible desde el almacén. **Se detecta comparando** el texto guardado con el enviado, carácter por carácter |

La regla que resume las tres, y que conviene poder recitar: **lo que no falla hay que ir a buscarlo, contando, comparando o inspeccionando.**

Y una cuarta de la misma familia, que no es una regla de negocio sino de arquitectura: **exponer en un mensaje la ruta del almacén, la dirección de un servicio interno o una traza de implementación**. Tampoco falla; simplemente le entrega a quien mire la respuesta algo que no debería tener. Está en [`DX-Error-Messages.md`](DX-Error-Messages.md) §1.4.

### 6.3 Dónde va un punto de acceso nuevo

El procedimiento, en cinco preguntas, y **en este orden**:

1. **¿Existe ya?** Mirar los quince. La mitad de los puntos que alguien quiere agregar son una variante de uno que ya está, y una variante en la ruta suele ser información sobre el solicitante en un lugar donde no hace falta —**es el motivo por el que la eliminación es un solo punto y no dos**—.
2. **¿Qué tipo transporta?** Si no hay un tipo del ensamblado de contratos que le sirva, **el punto no se agrega todavía**: los tipos son de aquel proyecto de código y esta capa no los declara.
3. **¿Qué papel exige, y va bajo la guardia?** Desde `PRODUCT-INTAKE` 1.13 **no queda ninguna situación en la que un punto que escribe no pueda exigir acceso**: la que había —el establecimiento de la contraseña del primer ingreso— se suprimió con **RN-16**, y los cuatro puntos que no exigen acceso firmado no fijan contraseñas. Salvo que se trate de uno de esos cuatro, **va bajo la guardia**. Agregarlo a la tabla de puntos y a la cuenta de la métrica es parte de agregarlo.
4. **¿Qué puede fallar, y con qué código del conjunto cerrado?** Si ningún código corresponde, **no se inventa uno**: se usa el genérico y **se declara el hueco**, como están declarados los dos que hoy existen.
5. **¿Qué agrega a la colección de peticiones?** Un punto que no se puede ejercitar desde la colección es un punto que nadie va a volver a probar.

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Recorrido de la primera hora contra la superficie: prerrequisitos con **lectura obligatoria de dos documentos**, el quick-start de tres pasos dentro del entorno de desarrollo contenido, el **primer ejemplo con sentido** —el envío del escenario `E-5`, que responde con éxito y deja el trabajo en `Borrador`— y el segundo —dos respuestas que tienen que ser indistinguibles—, cómo leer una respuesta de fallo con sus dos identificadores y sus dos excepciones, **trece diagnósticos frecuentes** de la primera hora, los próximos pasos por tarea, y **las tres cosas que no fallan** con su detección por recuento, comparación e inspección, más el procedimiento de cinco preguntas para agregar un punto de acceso nuevo. |
| 1.1 | 2026-08-10 | Actualización por `PRODUCT-INTAKE` **1.13** §4.1 (**RN-16**) y la precisión de **F-04**. §3.5 actualiza el conjunto cerrado de códigos de diecisiete a **quince**. §6.3 actualiza el procedimiento de alta de un punto de acceso: los puntos a revisar pasan de dieciséis a **quince**, y el paso 3 deja de contemplar la excepción del establecimiento de la contraseña, **que dejó de existir** —los cuatro puntos que no exigen acceso firmado no fijan contraseñas—. **Ningún paso del recorrido de la primera hora y ningún diagnóstico cambia.** Sube minor. |
| 1.2 | 2026-08-11 | **Cierra los hallazgos `B-API-01` (P0), `B-API-10` (P2), `B-API-11` (P2), `B-API-15` (P3) y `B-API-13` (P3)** del informe [`B-02-03-GeometriaFactory-Api-r1.md`](../../../Audit/B-02-03-GeometriaFactory-Api-r1.md) 1.0. **§1**, lectura obligatoria 1: «las quince rutas están decididas, y **quince** no lo están» pasa a «**catorce** no lo están». La única ruta que declara una fuente es la del canje: `../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md` §3 rotula `A-01` **[declarada por la fuente]** y las otras catorce **[derivado]**, y su §2 habla de «los **catorce** puntos restantes». Era la única frase del corpus que decía quince, y estaba en lo primero que se lee del proyecto de código. **§3.5**, última línea: el catálogo pasa de **18** a **16** entradas, recontado sobre las siete tablas de §3 de [`DX-Error-Messages.md`](DX-Error-Messages.md) (3+2+2+1+2+5+1) y coincidente con su §6.1, **16 = 14 + 2**. **§5**, última viñeta: los huecos elevados al Product Owner pasan de **tres** a **dos**; el tercero —cómo se identifica la cuenta al establecer la contraseña del primer ingreso— quedó **cerrado** por `RN-16`, y `../02-Especificacion-Funcional/Especificacion-Funcional.md` §11 ya dice «los **dos** primeros son huecos de la superficie». **La fila 1.1 se corrige en sus dos auto-citas de sección**: el conjunto cerrado se menciona en **§3.5** y no en §4, que es la tabla de diagnósticos, y el procedimiento de alta de un punto de acceso es **§6.3** y no §7, que es este mismo control de cambios. **Cabecera**: pasa a citar `PRODUCT-INTAKE` **1.26**, vigente hoy. **Búsqueda de propagación hecha con `grep` sobre todo el corpus vivo**, según la condición de método del informe: «catorce de las quince rutas» aparece bien en **cuatro** lugares vivos de tres documentos —`../02-Especificacion-Funcional/README.md` §5, [`README.md`](README.md) §1 y §6, y [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §1.2—, más `../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md` §2, que habla de «los catorce puntos restantes»; ésta era la única desviada; el recuento del catálogo se citaba mal en **seis lugares de cuatro documentos** y los seis se corrigen en esta tanda; «tres huecos elevados» no sobrevive en ningún otro lugar vivo. **Ningún paso del recorrido de la primera hora y ningún diagnóstico cambia.** Sube minor. **Enmienda de esta misma fila, 2026-08-11**, absorbida en la versión en curso sin subir —la política de versionado del framework absorbe dentro de la versión vigente las correcciones derivadas del audit de la propia fase de emisión mientras el documento está en `Propuesto`—: el alcance de propagación declaraba «cinco documentos» donde son **cuatro**, contados sobre la enumeración misma —`README.md`, `Glosario-UX.md`, `DX-Developer-Experience.md` y `Guia-Onboarding-Developer.md`—; el número venía heredado sin recontar de la ronda 1. **Los seis lugares siguen siendo seis y ningún recuento del producto se mueve.** Cierra el hallazgo `N-01` (P2) de [`B-02-03-GeometriaFactory-Api-r2.md`](../../../Audit/B-02-03-GeometriaFactory-Api-r2.md) 1.0. |
