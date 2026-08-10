# Guía de onboarding — GeometriaFactory-Contracts

**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** Guia-Onboarding-Developer.md
**Versión:** 1.5
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** DX Lead (AG-03)
**Variante:** DX
**Trazabilidad upstream:** `02-Especificacion-Funcional/Especificacion-Funcional.md` §1, §2, §6 (`RT-01` a `RT-11`); `CU-01` §10 y §17, `CU-02` §17, `CU-03` §1, CA-01, CA-02, §6.1 y §17, `CU-04` CA-01, §6.1, §10 y §17, `CU-05` CA-05, CA-06, CA-07, CA-08, §6.1 y §17, `CU-06` §6, CA-01, §10 y §17, `CU-07` §6, CA-03, CA-05, §10 y §17; `00-Contexto/Vision-Producto.md` §9.1 (Estado del trabajo, Enviar, Aprobar / Rechazar, Comentario) y §9.2; `00-Contexto/Alcance-Producto.md` §2.2 y §8; `01-Necesidades-Negocio/Necesidades-De-Negocio/NB-04-Interpretacion-Fiel-Del-Dato-Del-Alumno.md` §1; `NB-09-Desenlace-Explicito-De-La-Entrega.md` §1; `PRODUCT-INTAKE` **1.14** §4.1, §4.2, §17.4 P.3, P.5, P.6, P.8, P.10 y P.11, §15, §16, §16.1
**Trazabilidad downstream:** `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas` y `11-Documentacion` de este proyecto de código

---

## Tabla de contenido

- [1. Audiencia y prerrequisitos](#1-audiencia-y-prerrequisitos)
  - [1.1 Para quién es esta guía](#11-para-quién-es-esta-guía)
  - [1.2 Prerrequisitos](#12-prerrequisitos)
  - [1.3 Lo que hay que leer antes de tocar nada](#13-lo-que-hay-que-leer-antes-de-tocar-nada)
- [2. Instalación o acceso](#2-instalación-o-acceso)
  - [2.1 Abrir el proyecto de código](#21-abrir-el-proyecto-de-código)
  - [2.2 Construir y verificar](#22-construir-y-verificar)
- [3. Primer ejemplo ejecutable](#3-primer-ejemplo-ejecutable)
  - [3.1 Recorrer la superficie pública](#31-recorrer-la-superficie-pública)
  - [3.2 Ejercitar los tipos contra el servicio real](#32-ejercitar-los-tipos-contra-el-servicio-real)
  - [3.3 Cuatro cambios de control](#33-cuatro-cambios-de-control)
- [4. Diagnóstico de problemas frecuentes en la primera hora](#4-diagnóstico-de-problemas-frecuentes-en-la-primera-hora)
- [5. Próximos pasos](#5-próximos-pasos)
- [6. Control de cambios](#6-control-de-cambios)

---

## 1. Audiencia y prerrequisitos

### 1.1 Para quién es esta guía

Para quien tiene que trabajar contra el ensamblado de contratos durante la próxima hora. En este producto esa persona es una de tres, declaradas en [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §1.1: el mantenedor presente, el mantenedor futuro —la misma persona sin el contexto en la cabeza— y el agente de construcción por etapas. No hay integradores externos: los dos únicos consumidores del contrato son `GeometriaFactory-Api` y `GeometriaFactory-Web`, del mismo producto.

La guía es un tutorial: un solo camino, en orden, sin alternativas. Si lo que necesitás es resolver un síntoma concreto, el camino no es éste sino [`DX-Error-Messages.md`](DX-Error-Messages.md).

### 1.2 Prerrequisitos

| Prerrequisito | Detalle |
| --- | --- |
| El repositorio clonado y abierto en el **contenedor de desarrollo** | Declarado en `.devcontainer/devcontainer.json` (`PRODUCT-INTAKE` §16). **Todo ocurre adentro**: el host de desarrollo no tiene las herramientas de construcción y no va a tenerlas |
| La etapa `a` del plan de entrega cerrada | Es el andamiaje de la solución de código: la estructura de proyectos de código y los comandos de `scripts/` (`PRODUCT-INTAKE` §15). Sin ella no hay nada que construir |
| Ningún prerrequisito de red ni de credenciales | Este proyecto de código es nivel 0 del orden topológico y no depende de nada (`PRODUCT-INTAKE` §13) |

### 1.3 Lo que hay que leer antes de tocar nada

Una sola frase, y está en `PRODUCT-INTAKE` §17.4 P.5: **este ensamblado no implementa autenticación, pero es donde se decide qué se expone**. Ningún tipo de transferencia incluye el hash de contraseña, la clave de firma ni ninguna dirección de servicio interno.

Si en algún momento de esta hora tenés dudas sobre si un campo va o no va, la respuesta por defecto es que no va. Un campo de más en esta superficie compila sin protestar, cruza la frontera de servicio y llega al otro extremo; sacarlo después no deshace lo que ya viajó.

Y una segunda frase, que en este contrato se paga casi tan cara: **el comentario del administrador no es una observación**. Los dos viajan en el mismo tipo de detalle, los dos son texto sobre el trabajo, y ahí termina el parecido. La observación la emite el producto al interpretar el texto, es una colección y lleva severidad, índice de figura, campo señalado y par de valores; el comentario lo escribe una persona, es a lo sumo uno y no lleva ninguno de esos campos. Fusionarlos compila sin protestar (`RT-09`, `CU-05` CA-08).

## 2. Instalación o acceso

No hay instalación: no hay paquete que traer. El ensamblado se construye desde el repositorio y se consume por referencia de proyecto de código desde `GeometriaFactory-Api` y `GeometriaFactory-Web`. `redistribuible` es false y no hay feed (`PRODUCT-INTAKE` §13).

### 2.1 Abrir el proyecto de código

El código vive en `src/GeometriaFactory.Contracts/`, con esa identidad exacta (`PRODUCT-INTAKE` §16). Su documentación vive en `SDD/Docs/Proyectos/GeometriaFactory-Contracts/`.

### 2.2 Construir y verificar

Desde la raíz del repositorio, **dentro del contenedor de desarrollo**:

```bash
# Paso 1 — construir. El ensamblado de contratos no depende de nadie,
# así que es lo primero que se construye.
bash scripts/build.sh
```

Resultado esperado: **termina en 0 y sin advertencias**. Ése es el hito del tramo de 5 minutos, y no es una formalidad: «compila sin advertencias» es el quality gate bloqueante del pipeline de este proyecto de código (`PRODUCT-INTAKE` §17.4 P.8).

```bash
# Paso 2 — verificar RT-05 a mano, una vez, para que la restricción deje
# de ser una frase y pase a ser algo que sabés comprobar.
grep -R "GeometriaFactory.Domain" src/GeometriaFactory.Contracts/ || echo "OK: RT-05 se cumple"
```

Resultado esperado: sin coincidencias. El ensamblado **no declara ninguna referencia hacia `GeometriaFactory-Domain`**, y ésa es la ausencia que impide que la pieza pública conozca las entidades del dominio. El intake la declara quality gate bloqueante y aclara por qué: es la vía por la que el acoplamiento vuelve (`PRODUCT-INTAKE` §17.4 P.8).

## 3. Primer ejemplo ejecutable

### 3.1 Recorrer la superficie pública

Con la construcción en verde, el recorrido de la superficie se hace leyendo, y se verifica contra cinco preguntas. Las respuestas están en los contratos de uso de `02-Especificacion-Funcional/Casos-De-Uso/`; conviene contestarlas primero de memoria y después confirmarlas.

| Pregunta | Respuesta | Dónde se confirma |
| --- | --- | --- |
| ¿Dónde viajan el texto original completo y el comentario del administrador? | Los dos, **sólo en el detalle** del trabajo interpretado. Es el único tipo del ensamblado que los transporta; el elemento de listado trae el **estado**, que es lo que expresa el desenlace | `CU-05` §10; `CU-04` CA-01 y §10 |
| ¿Por qué el elemento de listado no los trae? | Porque la proyección de listado existe precisamente para **no** ser el detalle: declara 0 campos de texto original, 0 de componente de pieza y 0 de comentario, para que el listado no arrastre texto libre de cada trabajo | `RT-04` en su enunciado ampliado; `CU-04` CA-01 y §10 |
| ¿Cuántos campos tiene la respuesta de error, y cuántos pueden transportar una dirección de servicio? | Exactamente cuatro —código, texto, detalles y momento— y **cero** de la segunda clase | `CU-06` CA-01 |
| ¿Qué recibe una persona recién habilitada, que canja con la contraseña provisoria de su primer ingreso? | Una **respuesta de error** con el código `CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO` y su motivo —**el mismo** que recibe una cuenta reseteada—. **No** una respuesta de sesión con una marca: la respuesta de sesión sigue declarando cuatro campos y ninguno agregado para este caso. Desde `PRODUCT-INTAKE` 1.13, **RN-16** hace que el primer ingreso y el cambio posterior a un reseteo sean el mismo camino, y `CONTRATO_CONTRASENA_NO_ESTABLECIDA` salió del conjunto cerrado | `CU-01` FA-02, CA-05 y §10; `CU-06` §6 y §10; `CU-08` §1 |
| ¿En qué se diferencia el comentario del administrador de una observación, si los dos viajan en el detalle? | En **cuatro planos, y no comparten ni un campo**: cardinalidad —el comentario es a lo sumo uno, las observaciones son una colección—; origen —lo escribe una persona, las emite el producto al interpretar el texto—; forma —el comentario no lleva severidad, ni índice de figura, ni campo señalado, ni par de valores, y la observación no lleva autoría humana—; y ubicación —el comentario es un bloque propio del detalle, nunca un elemento de la colección de observaciones—. Además **no es una calificación**: no lleva nota ni escala | `RT-09`; `CU-05` CA-07 y CA-08; `CU-07` CA-05 |

Si las cinco respuestas salieron, el tramo de 30 minutos está cumplido. La quinta es la que más veces se responde mal, y es la que `DXC-11` y `DXC-12` del catálogo rechazan en revisión.

### 3.2 Ejercitar los tipos contra el servicio real

Este proyecto de código **no tiene pruebas propias**: son tipos sin comportamiento. Se ejercita íntegramente desde las pruebas de integración que golpean el servicio real (`RT-07`, `PRODUCT-INTAKE` §17.4 P.6).

```bash
# Paso 3 — ejercitar los tipos de transferencia de punta a punta.
bash scripts/test.sh
```

Es el primer valor real del recorrido: hasta acá el ensamblado compilaba; a partir de acá se sabe que los tipos transportan lo que dicen transportar. Depende de que las etapas que introducen cada familia estén cerradas, así que en las etapas tempranas la cobertura es parcial por construcción y no por defecto.

### 3.3 Cuatro cambios de control

El último tramo de la hora. Clasificá cada cambio como **compatible** o **incompatible**, y decí cuál es la acción operativa. Las respuestas están abajo; conviene resolverlos antes de leerlas.

1. Agregar un campo opcional a la respuesta de sesión de `CU-01`.
2. Agregar una situación de cuenta al conjunto admitido de `CU-02` —por ejemplo, una cuarta además de pendiente, habilitada y bloqueada—.
3. Agregar el texto original al elemento de listado de `CU-04`, para ahorrarse una solicitud al abrir el detalle.
4. Hacer que el comentario del administrador viaje como un elemento más de la colección de observaciones de `CU-05`, con una severidad que lo distinga.

| Cambio | Clasificación | Por qué | Acción |
| --- | --- | --- | --- |
| 1 | **Compatible** | La pieza pública que no lee el campo nuevo sigue compilando | Se puede introducir sin coordinar despliegues, siempre que el campo no viole `RT-01` |
| 2 | **Incompatible**, aunque compile | La pieza pública deja de cubrir todos los casos: hay una situación que no contempla. Es incompatible de hecho | Despliegue conjunto de las dos piezas desplegables (`RT-06`). No se versionan rutas: no hay consumidores de terceros |
| 3 | **Se rechaza**, aunque compile y aunque nadie se rompa | Viola el requisito estructural `RT-04`, que es el motivo por el que la proyección de listado existe separada del detalle. Vale igual para el comentario del administrador, que el enunciado ampliado de `RT-04` incorpora | No se introduce. Si el problema real es el número de solicitudes, se discute en `05-Arquitectura-Tecnica`, no agregando campos acá |
| 4 | **Se rechaza**, aunque compile | Viola `RT-09`: el comentario y las observaciones no comparten ni un campo. Darle severidad al comentario lo convierte en lo que no es, y obliga a la pieza pública a filtrar la colección para no mostrar como defecto del texto algo que escribió una persona | No se introduce. El comentario es un bloque propio del detalle. Es `DXC-11` del catálogo |

Los cuatro salieron bien: el tramo de 1 hora está cumplido y ya sabés leer la señal más valiosa de este proyecto de código, que es que la incompatibilidad aparece al compilar y no en producción. Y sabés además algo que conviene no mezclar: **los tres últimos de la tabla compilan igual** —ninguno da señal automática y a los tres los detiene la revisión, no el compilador—, pero sólo **dos se rechazan**, el 3 y el 4. El 2 no se rechaza: se acepta y obliga al despliegue conjunto. Son tres salidas distintas, y distinguirlas es justamente lo que este ejercicio enseña.

## 4. Diagnóstico de problemas frecuentes en la primera hora

Cinco problemas, con lo que significan y qué hacer. El catálogo completo, con las dos clases de error separadas, está en [`DX-Error-Messages.md`](DX-Error-Messages.md).

| Síntoma en la primera hora | Qué significa | Qué hacer |
| --- | --- | --- |
| La construcción termina en 0 pero **con advertencias** | El hito de 5 minutos **no** está cumplido: el quality gate del pipeline es «sin advertencias», no «sin errores» | Resolver la advertencia antes de seguir. Ver `DXC-09` del catálogo |
| Un comando de `scripts/` no existe | La etapa `a` no está cerrada, o el repositorio no está abierto en el contenedor de desarrollo | Verificar el contenedor. **No** sustituir el comando con herramientas del host: el host no las tiene y no va a tenerlas |
| La verificación de `RT-05` devuelve coincidencias | El ensamblado adquirió una referencia hacia el proyecto de código de dominio. Es el quality gate bloqueante del proyecto de código | Quitar la referencia. Ver `DXC-01`: se rechaza en revisión, no se negocia |
| Las pruebas de integración fallan enteras y no por un caso | Lo que está roto no es el contrato: es el servicio o su base. Este proyecto de código no tiene pruebas propias que puedan fallar solas | Diagnosticar en `GeometriaFactory-Api`. Ver `DXC-08` si la sospecha es desfasaje entre las dos piezas |
| No se encuentra un fragmento de ejemplo que instancie un tipo | No existe y es deliberado: este proyecto de código no produce samples propios, porque no lo consumen integradores externos (`PRODUCT-INTAKE` §16.1) | Usar los contratos de uso de 02 como referencia de forma, y las pruebas de integración como ejemplo vivo |
| Se busca el código de error del texto que no verifica y no está en el conjunto cerrado | Salió del conjunto. Con el envío como única acción de guardado, un texto que no verifica **no hace fallar ninguna operación**: el envío procede y el trabajo queda en estado `Borrador` con sus observaciones | Tratarlo como señal y no como error. Ver `DXT-N3` del catálogo, y `DXT-N2` para el mismo código al pedir el detalle. El identificador `DXT-09` quedó retirado y no se reasigna |
| Se busca dónde ubicar el índice de figura dentro de la respuesta de error | El tipo de error **conserva la capacidad** de transportarlo, pero desde el modelo vigente ningún código del conjunto la ejerce: los defectos de interpretación viajan como observaciones del detalle de `CU-05` | No borrar la capacidad por parecer muerta ni buscar la ubicación en el error. El fundamento está en `CU-06` §10 |

## 5. Próximos pasos

Los cuatro modos de Diátaxis, con su enlace, según el plan de [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §4:

- **Tutorial** — este documento. Ya está recorrido.
- **How-to** — [`DX-Error-Messages.md`](DX-Error-Messages.md) para diagnosticar un error de cualquiera de las dos clases; la sección §17 «Compatibilidad de versión pública» de cada contrato de uso, en [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/), para decidir si un cambio concreto pasa.
- **Reference** — los ocho contratos de uso `CU-01` a `CU-08`, y [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §6 para las once restricciones transversales. Es la descripción normativa de la superficie; esta sección no la duplica.
- **Explanation** — [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §1.2 y §5.1, y [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §2 y §5, para el porqué de cada decisión. Los ADR pertenecen a `05-Arquitectura-Tecnica`.

Vocabulario: antes de escribir en cualquiera de estos documentos, [`Glosario-UX.md`](Glosario-UX.md) y [`../02-Especificacion-Funcional/Glosario-Funcional.md`](../02-Especificacion-Funcional/Glosario-Funcional.md), en particular §3.1 por los tres referentes de «contrato» y **§3.3 por `Pendiente`**, que nombra dos estados distintos —el de una cuenta y el de un trabajo— y va siempre calificado.

## 6. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Recorrido de la primera hora en tres tramos verificables, íntegramente dentro del contenedor de desarrollo: construcción sin advertencias, verificación manual de `RT-05`, tres preguntas de superficie pública, ejercicio de los tipos por prueba de integración y tres cambios de control para clasificar compatibilidad. Suma cinco problemas frecuentes con su diagnóstico y los enlaces a los cuatro modos de Diátaxis. | DX Lead (AG-03) |
| 1.0 | 2026-08-08 | Corrección absorbida de la ronda 1 de auditoría (`Audit/B-02-03-GeometriaFactory-Contracts-r1.md`), sin subir versión por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **H-01**: una ocurrencia de «solución» a secas designando el agrupador de construcción, corregida a «solución de código» en la fila de la etapa `a` de §1.2, según `Vocabulario-Rules.md` §4 R2 y sin sustitución global (§9.5). **H-09**: las referencias a la sección opcional pasan de §12 a §17 en la cabecera y en §5. **Alineación con el upstream**: la cabecera suma `CU-01` §10 y las dos §6.1 de señales declaradas que no son error de `CU-04` y `CU-05`; §3.1 suma la cuarta pregunta de superficie, por el paso del conjunto cerrado de doce a trece códigos. | DX Lead (AG-03) |
| 1.1 | 2026-08-09 | **Actualización por contenido nuevo aguas arriba**: `PRODUCT-INTAKE` 1.3 incorporó el circuito de revisión del administrador y la categoría 02 emitió `CU-07`, `RT-08` y `RT-09`. Sube minor y archiva el estado anterior por `Master-Prompt.md` §5. **§1.3** suma la segunda frase que hay que entender antes de tocar nada: el comentario del administrador no es una observación. **§3.1** pasa de cuatro preguntas a cinco, con la distinción entre comentario y observación en sus cuatro planos, y las dos primeras incorporan el comentario y el estado como lo que el listado sí transporta. **§3.3** pasa de tres cambios de control a cuatro, con el comentario fusionado con las observaciones como cuarto caso. **§4** suma dos problemas frecuentes: buscar el código de error del texto que no verifica, que salió del conjunto, y buscar el índice de figura dentro de la respuesta de error, que hoy vive en las observaciones del detalle. **§5** pasa a siete contratos de uso y nueve restricciones, y remite a `Glosario-Funcional.md` §3.3 por la forma calificada de `Pendiente`. **Corrección de la ronda 3 de auditoría (`Audit/B-02-03-GeometriaFactory-Contracts-r3.md`), absorbida en esta misma versión sin subir a 1.2 y sin snapshot nuevo, por `Master-Prompt.md` §5 y por el punto 5 de §8 del informe. H-07**: tres conteos que la pasada dejó atrás. §3.1 decía «tres preguntas» sobre una tabla de cinco; §3.3 se titulaba «Tres cambios de control» sobre una lista de cuatro, en el título y en la tabla de contenido; y el cierre de §3.3 decía «las tres rechazadas», mezclando dos clasificaciones en el peor lugar posible, que es el ejercicio que enseña a distinguirlas. El cierre pasa a decir lo que quería decir: los **tres últimos** compilan igual y a los tres los detiene la revisión, pero sólo **dos** se rechazan; el 2 se acepta y obliga al despliegue conjunto. | DX Lead (AG-03) |
| 1.2 | 2026-08-09 | **Actualización por contenido nuevo aguas arriba**: `PRODUCT-INTAKE` **1.7** incorpora la capacidad **F-26** y la categoría 02 emite **CU-08**, contrato de reseteo y de cambio obligatorio de contraseña, con dos restricciones transversales nuevas. §5 actualiza el mapa de Diátaxis a **ocho** contratos de uso y **once** restricciones transversales. **Ningún tramo del onboarding cambia**: los cuatro cambios de control siguen siendo los mismos y el hito de 1 hora no depende del alcance funcional. La distinción operativa nueva —el cambio de contraseña pendiente **no** es un quinto campo de la respuesta de sesión— vive en [`DX-Error-Messages.md`](DX-Error-Messages.md) `DXC-14` y `DXT-16`, y no se duplica acá. | DX Lead (AG-03) |
| 1.3 | 2026-08-09 | **Cierra la fila de este archivo del hallazgo `F26-20` y la parte del `F26-28`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0, contra `PRODUCT-INTAKE` **1.10**. **`F26-20`**: la cabecera de trazabilidad citaba «§6 (`RT-01` a **`RT-09`**)» y las restricciones transversales son **once**, `RT-01` a `RT-11`, desde que `CU-08` emitió `RT-10` y `RT-11`. **Cierra la parte del hallazgo `F26-28`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0 que alcanza a este archivo: las filas de este control de cambios estaban **fuera de orden cronológico** (1.1, 1.0, 1.0, 1.2) y se reordenan por versión, **sin tocar el texto de ninguna**. **Ningún paso del recorrido, ningún diagnóstico y ninguna remisión cambia.** Sube minor. | DX Lead (AG-03) |
| 1.4 | 2026-08-10 | **Actualización por `PRODUCT-INTAKE` 1.13 §4.1 (RN-16)** y la precisión de **F-04**. La pregunta de §3 sobre qué recibe una persona habilitada que todavía no estableció su contraseña **describía una situación que dejó de existir**: habilitar produce y fija la provisoria, de modo que ninguna cuenta llega a estar habilitada sin contraseña. Se reescribe sobre el primer ingreso con la provisoria, cuyo código es `CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO` —el mismo del reseteo—, con la constancia de que `CONTRATO_CONTRASENA_NO_ESTABLECIDA` salió del conjunto cerrado. **Ningún paso del recorrido y ningún otro diagnóstico cambia.** Sube minor. | DX Lead (AG-03) |
| 1.5 | 2026-08-10 | **Cierra el hallazgo `C-08` (P2) del informe de auditoría `SDD/Docs/Audit/Coherencia-Corpus-r1.md` 1.0.** La cabecera de trazabilidad declaraba derivarse del `PRODUCT-INTAKE` **1.3**, versión archivada, y pasa a declarar la **1.14**, vigente. Entre la **1.3** y la **1.14** el intake atravesó once emisiones, entre ellas las que incorporaron **F-25**, **F-26** y las reglas **RN-12** a **RN-16**: una cabecera que declaraba 1.3 declaraba derivarse de un intake que no conocía ni el reseteo ni la habilitación con contraseña provisoria. Se revisó el cuerpo antes de mover la cabecera y **no arrastra ninguna decisión de las versiones intermedias**: no queda en él ningún recuento de «quince reglas» ni de «diecisiete códigos», ninguna cita a la exclusión **X-2** como vigente y ninguna afirmación de que la marca de cambio de contraseña pendiente la ponga únicamente el reseteo. **Ningún contenido normativo de este documento cambia: la corrección es de trazabilidad.** Sube minor. | DX Lead (AG-03) |
