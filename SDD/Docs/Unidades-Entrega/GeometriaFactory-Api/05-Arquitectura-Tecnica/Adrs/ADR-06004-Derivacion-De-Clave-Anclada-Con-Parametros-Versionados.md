# ADR-06004 — Derivación de clave anclada, con sus parámetros versionados junto al valor guardado

**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** ADR-06004-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Seguridad

---

## 1. Contexto

Acá viven las dos piezas sensibles del producto. El intake las declara juntas: **derivación de la contraseña** —«nunca en claro ni con resumen simple»— y **emisión del acceso firmado con clave simétrica**, con la clave «provista o generada en el primer arranque», viviendo **fuera del repositorio de código y fuera de la imagen** (`PRODUCT-INTAKE` §17.3.P.5).

Lo que la fuente **no** decide es cuál de las dos funciones de derivación admitidas se ancla: declara «PBKDF2 o Argon2» y deja la elección a la regla de anclaje de versiones de la etapa `a`. La categoría 02 lo derivó a esta categoría, y esta ADR tiene que resolver algo más urgente que el nombre: **qué se guarda junto al valor derivado**. Sin esa decisión, la condición `CREDENCIAL_DERIVADA_ILEGIBLE` que la categoría 03 declara no tiene forma de distinguirse de una contraseña equivocada, y una cuenta puede quedar inaccesible sin que nadie sepa por qué.

Hay un tercer hueco que esta ADR cierra por su carácter y no por su número: **la vigencia del acceso firmado**. El intake la declara «corta» y sin acceso de refresco, y no da número.

Motivación upstream: NB-00002; RN-06001, RN-06006, RN-06013, RN-06016; INV-06, INV-09; `PRODUCT-INTAKE` §17.3.P.5, §17.5.P.5, §14 (RA-03).

## 2. Decisión

**El valor derivado que se guarda lleva consigo los parámetros con los que se produjo, y la verificación los lee de ahí y no de la configuración vigente.** De esa decisión cuelgan cinco reglas:

1. **Ningún valor por defecto silencioso.** Si el valor guardado no declara sus parámetros o su forma no corresponde a la función anclada, la verificación termina en `CREDENCIAL_DERIVADA_ILEGIBLE` y **no responde «no coincide»**: responderlo lo haría indistinguible de una contraseña equivocada.
2. **La cadena vacía no se deriva.** `CONTRASENA_EN_CLARO_AUSENTE` cubre el valor nulo y el vacío: derivar la cadena vacía produciría un valor válido para una credencial que nadie eligió.
3. **La clave de firma se recibe y no se busca.** Si no llega, `CLAVE_DE_FIRMA_AUSENTE`, y **no se genera una al vuelo ni se emite sin firmar**. Un acceso sin firma verificable es peor que ningún acceso, porque el sistema seguiría funcionando hasta que alguien lo falsifique; y una clave generada al vuelo invalida todos los accesos en cada reinicio, con lo cual el síntoma visible es otro.
4. **Los cuatro reclamos del acceso son obligatorios y ninguno se completa por defecto**: identificador, correo, papel y expiración. Uno sin papel dejaría a las capas de adentro decidiendo sobre un dato que nadie declaró; uno sin expiración no vencería nunca (`RECLAMOS_INCOMPLETOS`).
5. **La vigencia del acceso se toma de configuración**, y el criterio de arquitectura que esta ADR fija es que **caduque dentro de la sesión de trabajo de una clase** y que la renovación sea **reingreso**, sin acceso de refresco. El número concreto se ancla en la etapa `a` y sigue como punto abierto.

**Cuál de las dos funciones se ancla no lo decide esta ADR, y el criterio de decisión sí:** entre las dos que la fuente admite, se ancla la que la plataforma base provea **sin agregar una dependencia nueva al proyecto de código**; si las dos lo hacen, la de mayor resistencia a hardware dedicado. El coste se calibra midiendo en el equipo objetivo y se anota con su versión, como toda versión del producto.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Parámetros versionados junto al valor derivado (**adoptada**) | Permite subir el coste sin invalidar las credenciales existentes; hace diagnosticable el valor ilegible; la migración de parámetros es posible sin que nadie pierda el acceso | El valor guardado ocupa más y su forma es parte del contrato del dato: cambiarla es una transformación de esquema |
| Parámetros en la configuración, valor derivado desnudo en el almacén | El valor guardado es más corto y el esquema más simple | **Descartada.** Cambiar el coste invalidaría todas las credenciales existentes de golpe, y no habría forma de distinguir un valor producido con parámetros viejos de uno corrupto: `CREDENCIAL_DERIVADA_ILEGIBLE` dejaría de ser diagnosticable |
| Elegir acá una de las dos funciones de derivación | Cierra el punto abierto de inmediato | **Descartada.** El intake declara las dos y ata la elección a la regla de anclaje de versiones de la etapa `a`; elegir acá adelantaría una decisión que la fuente puso en otro lado, sobre un criterio —qué provee la plataforma sin dependencia nueva— que se verifica midiendo y no razonando |
| Generar la clave de firma al vuelo cuando no llega | El servicio siempre arranca | **Descartada.** Es uno de los tres atajos que la categoría 03 declara como «fallar hacia el lado seguro»: el sistema arranca, emite accesos y nadie lo nota hasta que alguien falsifica uno |
| Emitir un acceso de refresco de vigencia larga | Evita que la persona vuelva a escribir su contraseña cuando el acceso vence | **Descartada por el intake §17.5.P.5**: vigencia corta y renovación por reingreso, sin acceso de refresco en este alcance |

## 5. Consecuencias positivas

1. Una cuenta nunca queda inaccesible sin diagnóstico: el valor ilegible tiene su propia condición y su propia acción sugerida.
2. El coste de derivación se puede subir en cualquier etapa sin obligar a nadie a cambiar su contraseña.
3. Las cinco cosas que no pueden salir en un mensaje —clave de firma, contraseña en claro, valor derivado, provisoria producida y ruta del almacén— quedan cubiertas por una sola prueba de inspección, porque las cinco viven en estos dos mecanismos.
4. `RA-03` queda exigible acá **por disciplina y no por ignorancia**, que es la diferencia con las dos capas de adentro.
5. El acceso transporta el papel sin decidir qué habilita, lo que mantiene la autorización donde la capa de aplicación la puso.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta que la forma del valor derivado sea parte del contrato del dato guardado**, y que cambiarla sea una transformación de esquema con su linaje.
2. **Se acepta arrastrar un punto abierto de elección de función hasta la etapa `a`**, con el criterio escrito para que la elección no sea de gusto.
3. **Se acepta que la vigencia del acceso quede sin número hasta la etapa `a`**, y con ella la consecuencia práctica: mientras no esté fijada, la prueba de expiración se escribe contra el valor configurado y no contra una constante.
4. **Se acepta que una persona vuelva a escribir su contraseña cuando su acceso vence**, que es el precio declarado de no tener acceso de refresco.

## 7. Implementación

- El mecanismo de credenciales y el mecanismo de acceso firmado de [`../Arquitectura-Proyecto-Codigo.md`](../Arquitectura-Proyecto-Codigo.md) §3.1 son los únicos lugares donde una contraseña en claro y una clave de firma existen dentro del producto.
- **Convención impuesta:** ni la contraseña en claro, ni el valor derivado, ni la provisoria, ni la clave de firma entran en un mensaje, en un resultado de error ni en una traza. La contracara obligatoria es que **todo error que se muestre queda registrado del lado del servidor**.
- **Convención impuesta:** ningún componente fuera de estos dos recibe la contraseña en claro. De acá para adentro circula sólo el valor derivado.
- La columna del valor derivado del esquema físico está en [`../Modelo-Datos-Logico.md`](../Modelo-Datos-Logico.md) §2.1, y es **nula mientras la cuenta está `Pendiente`**: toma valor en el acto de habilitación, con la provisoria que produce [`ADR-06005`](ADR-06005-Contrasena-Provisoria-No-Adivinable-Y-Sin-Repetirse.md).
- Criterio de elección de la función de derivación, para el punto de control de la etapa `a`: la que la plataforma base provea sin dependencia nueva; si las dos, la de mayor resistencia a hardware dedicado; el coste se calibra midiendo en el equipo objetivo.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Contraseñas en claro guardadas o registradas | Exactamente **0** | Inspección del esquema y del registro del servidor |
| Accesos emitidos sin clave de firma provista | Exactamente **0** | Prueba de emisión con la clave ausente, que debe producir la condición y ningún acceso |
| Accesos emitidos con menos de **4** reclamos | Exactamente **0** | Prueba por cada reclamo faltante |
| Verificaciones que responden «no coincide» ante un valor derivado ilegible | Exactamente **0** | Prueba con un valor guardado deformado |
| Mensajes y trazas que contienen un secreto o la ruta del almacén | Exactamente **0** | Prueba de inspección sobre las 17 condiciones y sobre el registro |
| Credenciales invalidadas al subir el coste de derivación | Exactamente **0** | Prueba que verifica una credencial producida con parámetros anteriores |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.17** §14 (RA-03), §17.3.P.5, §17.3.P.10 y §17.5.P.5.
- [`../../02-Especificacion-Funcional/Casos-De-Uso/CU-06006-Derivar-La-Contrasena-Y-Verificar-Una-Credencial.md`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-06006-Derivar-La-Contrasena-Y-Verificar-Una-Credencial.md) y [`CU-06008`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-06008-Emitir-El-Acceso-Firmado.md).
- [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md) §1.4 y §2.4.
- ADR relacionadas: [`ADR-06005`](ADR-06005-Contrasena-Provisoria-No-Adivinable-Y-Sin-Repetirse.md), [`ADR-06007`](ADR-06007-Transformaciones-Al-Arrancar-Con-Linaje-Inmutable.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Fija que el valor derivado lleva consigo sus parámetros, con las cinco reglas que cuelgan de esa decisión —sin valor por defecto silencioso, sin derivar la cadena vacía, la clave de firma recibida y no buscada, los cuatro reclamos obligatorios y la vigencia con criterio y sin número—, y deja el criterio de elección de la función de derivación para el punto de control de la etapa `a`. Evalúa cinco alternativas, declara cuatro trade-offs y fija seis métricas de validación. |
