# Auditoría B · 02-03 · GeometriaFactory-Api · ronda 2

| Campo | Valor |
| --- | --- |
| Fase | B — Especificación por proyecto de código |
| Producto | Fábrica de Geometría |
| Proyecto de código | GeometriaFactory-Api (`GeometriaFactory.Api`, `tipo_proyecto_codigo` = `rest-api`, **proyecto de código principal**) |
| Alcance auditado | **Verificación de cierre** de los diecisiete hallazgos de `B-02-03-GeometriaFactory-Api-r1.md` 1.0 sobre los 21 artefactos vivos de las categorías **02-Especificacion-Funcional** y **03-UX-UI-DX**, más **barrido de regresión** sobre los 33 archivos que tocó el commit de corrección `bef453d` y sobre los consumidores aguas abajo de los recuentos corregidos |
| Corrección auditada | Commit `bef453d` «cierre de los hallazgos de los dos informes tardíos», 2026-08-11, sobre la rama `sdd/cierre-de-huecos`. **33 archivos**, 160 inserciones / 125 supresiones |
| Fuentes contrastadas | `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.26** (§4.1, §17.5.P.10) y `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` **1.3** (§5), verificadas como versiones vigentes leyendo su cabecera; `GeometriaFactory-Contracts/CU-06` §10; `GeometriaFactory-Domain/02/Reglas-De-Negocio/`; `GeometriaFactory-Infrastructure/02/Especificacion-Funcional.md` §6; y las categorías **05**, **06**, **08** y **10** de este proyecto de código como consumidoras |
| Método | Recuento **sobre el instrumento** —filas contadas, `grep` sobre el corpus vivo, `git diff` contra el estado anterior—, no sobre la declaración del control de cambios. Ninguna afirmación de este informe se apoya en lo que una fila de control de cambios dice haber hecho |
| Auditor | Arquitecto de Soluciones + QA Senior, invocado desde cero, sin participación en la emisión de la ronda 1 ni en la corrección |
| Fecha | 2026-08-11 |
| Ronda | 2 |

---

## Tabla de contenido

- [1. Resumen ejecutivo](#1-resumen-ejecutivo)
- [2. Los diecisiete hallazgos de la ronda 1, uno por uno](#2-los-diecisiete-hallazgos-de-la-ronda-1-uno-por-uno)
- [3. El P0, contado a mano](#3-el-p0-contado-a-mano)
- [4. Las tres correcciones que la corrección le hizo a la ronda 1](#4-las-tres-correcciones-que-la-corrección-le-hizo-a-la-ronda-1)
- [5. Dictamen sobre la causa raíz](#5-dictamen-sobre-la-causa-raíz)
- [6. Hallazgos nuevos](#6-hallazgos-nuevos)
- [7. Barrido de regresión](#7-barrido-de-regresión)
- [8. Lo verificado y no reportado](#8-lo-verificado-y-no-reportado)
- [9. Lo no verificado](#9-lo-no-verificado)
- [10. Dictamen](#10-dictamen)
- [11. Opinión sobre la promoción a `Aprobado`](#11-opinión-sobre-la-promoción-a-aprobado)
- [12. Control de cambios](#12-control-de-cambios)

---

## 1. Resumen ejecutivo

**Los diecisiete hallazgos están cerrados sobre el instrumento, y el P0 está cerrado en los seis lugares sin que sobreviva un séptimo.** Conté a mano las siete tablas de §3 de `DX-Error-Messages.md` —3 + 2 + 2 + 1 + 2 + 5 + 1 = **16**, catorce con código del contrato y dos sin él— y verifiqué con `grep` sobre todo `SDD/Docs` que la afirmación «18 entradas» no queda viva en ninguna parte: la única ocurrencia restante es la fila **1.0** del control de cambios del propio catálogo, que describe el estado de una versión anterior, que es su función.

**La condición de método está implementada.** Cada recuento corregido lleva, en su fila de control de cambios, el alcance de la búsqueda de propagación con el número de lugares alcanzados. Recorrí seis de esos alcances declarados y **los conté**: «seis lugares vivos» del catálogo son seis; «cuatro citas vivas» del reparto de reglas son cuatro; «tres lugares vivos» del reparto de puntos abiertos son tres; «dos celdas mutiladas» son dos; «diecinueve archivos con la cita vieja» son diecinueve; «cuatro lugares vivos» de «catorce de las quince rutas» son cuatro. **Los seis alcances son verdaderos en su recuento de lugares.** La corrección además usó el método para encontrar lo que la ronda 1 no había mirado: doce cabeceras de casos de uso que el informe no listaba, y el mismo defecto de reparto vivo en `GeometriaFactory-Infrastructure`, que este informe no audita.

**Las tres correcciones que la corrección le hizo a la ronda 1 son las tres verdaderas**, verificadas sobre el instrumento y no sobre su enunciado. La ronda 1 subcontó las cabeceras envejecidas por más del doble, apoyó un recuento en una cita que no existe donde la ubica, y su propia aritmética no cierra.

**Lo que queda son dos hallazgos nuevos, los dos P2, y los dos son de la misma familia que este producto ya conoce: un número copiado sin recontar.** Uno está en cinco filas de control de cambios —las que declaran el alcance de propagación, es decir, el instrumento mismo de la causa raíz— y dice «cinco documentos» donde son **cuatro**, en un caso enumerando los cuatro en la misma oración. El otro es peor de origen: **tres documentos de nivel producto quedaron escribiendo «quince hallazgos» sobre este informe de ronda 1**, con el desglose «un P0, cinco P1, seis P2 y cinco P3» en la misma frase, que suma **diecisiete** — y la corrección lo escribió sabiéndolo, porque su propio mensaje de commit declara que esa aritmética no cierra.

**Ninguno de los dos toca un número del producto**: no hay un punto de acceso, un código del contrato, una regla, una métrica ni un criterio verificable que hoy diga algo falso. Por eso el rechazo se levanta. Pero los dos son afirmaciones falsas vivas en documentos que están a punto de sellarse como `Aprobado`, y de eso se ocupa §11.

---

## 2. Los diecisiete hallazgos de la ronda 1, uno por uno

| # | Sev. | Qué pedía | Estado | Cómo lo comprobé |
| --- | --- | --- | --- | --- |
| `B-API-01` | P0 | El catálogo, recontado en los **seis** lugares vivos que lo citan: 16 entradas = 14 + 2 | **CERRADO** | Ver §3. Conté las siete tablas de §3 de `DX-Error-Messages.md` (3+2+2+1+2+5+1 = 16; catorce filas con `CONTRATO_*` y dos «— (sin código)»). Abrí los seis lugares: `03/README.md` §2 dice hoy «**16 entradas** —14 códigos con destino más 2 sin código—»; `Glosario-UX.md` §2 dice «**16** situaciones… **14** códigos»; `Glosario-UX.md` §3.1 dice «las 16 situaciones»; `DX-Developer-Experience.md` §5 dice «las **16** entradas»; su §6 dice objetivo «**16 de 16**»; `Guia-Onboarding-Developer.md` §3.5 dice «las **16** entradas». `grep -rn "18 entradas\|18 de 18\|18 situaciones\|de las 18"` sobre todo `SDD/Docs` menos `Audit/`: **una sola ocurrencia**, la fila 1.0 del control de cambios del propio catálogo |
| `B-API-02` | P1 | `CU-12` §9: «13 de 15, los dos que no: `A-08` y `A-16`» | **CERRADO** | §9 dice hoy, literal: «**13 de 15**. Los **dos** que no —`A-08` y `A-16`—, en §10». Contrastado con su §8 `CA-08` («13 de los 15… Los **2** que no ejercita, A-08 y A-16»), con su §10 («Los dos puntos de acceso que la colección no ejercita, y una precisión sobre un tercero») y con `Definicion-Superficie-HTTP.md` §3, cuyas filas conté: **quince**, `A-01` a `A-03` y `A-05` a `A-16`, sin `A-04`. Los identificadores `A-08` y `A-16` existen y no son fantasma |
| `B-API-03` | P1 | `CU-09` §1 punto 1: «uno de los **quince**» | **CERRADO** | §1 punto 1 dice hoy «se convierten en uno de los quince». `grep "diecisiete"` sobre las categorías 02 y 03 devuelve **sólo filas de control de cambios**, que registran el paso de diecisiete a quince y son correctas |
| `B-API-04` | P1 | Índice maestro §5 y §8: catorce con destino; recorrer los quince | **CERRADO** | `Especificacion-Funcional.md` §5, fila `CU-09`: «los **catorce** códigos con destino y **el que no lo tiene**». §8, viñeta «Particiones»: «se prueba **recorriendo los quince**». Contrastado con `Definicion-Superficie-HTTP.md` §6 y con `Contracts/CU-06` §10 |
| `B-API-05` | P1 | El objetivo del tramo de 30 minutos, sin la categoría intermedia | **CERRADO** | `DX-Developer-Experience.md` §2 dice hoy «**cuatro sin acceso firmado y once bajo la guardia. Cuatro más once son quince**, y ninguno queda con su forma de identificación abierta». Es literalmente el cierre de `Definicion-Superficie-HTTP.md` §3. Verifiqué además la cita nueva que el control de cambios agrega: `05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md` **§3** —línea 194, dentro de «3. Vista lógica»— dice «Quince puntos: cuatro sin acceso firmado y once bajo la guardia. Cuatro más once son quince». **La cita existe y es exacta** |
| `B-API-06` | P1 | «trece con tramo acá, **tres** sin él» en los dos documentos de 03 | **CERRADO** | `03/README.md` §6 y `DX-Developer-Experience.md` §8 dicen hoy «**tres** sin él». La fuente, `Especificacion-Funcional.md` §6, dice «**Trece de las dieciséis tienen tramo acá y tres no lo tienen**». `grep` del reparto sobre el proyecto de código: **cuatro citas vivas** —§6 fuente, `08/Matriz-Cobertura-Pruebas.md`, `03/README.md`, `DX-Developer-Experience.md`—, las cuatro coincidentes hoy |
| `B-API-07` | P2 | Quitar los dos restos mutilados que dejaban viva una referencia a `A-04` | **CERRADO** | `grep -rn "~a \*\*A-04\|~ino"` sobre las categorías 02 y 03: **cero ocurrencias en el cuerpo**; las dos únicas apariciones de esas cadenas son las filas de control de cambios que citan el fragmento retirado entre comillas, que es su función. Recorrí las 26 menciones vivas de `A-04` en las dos categorías: **todas declaran su retiro**; ninguna lo describe en presente como camino vigente |
| `B-API-08` | P2 | `CU-01` §6: «cinco vivas» y «**dos** motivos comparten el `403`» | **CERRADO** | §6 dice hoy «**Ninguna de las cinco vivas** emite acceso» y «**Por qué dos motivos distintos comparten el `403`**… esperar la habilitación **o** cambiar la provisoria. Eran tres hasta que **RN-16** suprimió el establecimiento de la contraseña como camino propio». Conté las filas de §6: cinco vivas más una tachada; con `403` hay dos. El camino enumerado que `RN-16` suprimió **ya no se enumera** |
| `B-API-09` | P2 | `02/README.md` §4: **siete** ausencias | **CERRADO** | §4 dice hoy «declara las **siete** ausencias de la superficie y qué las repone». Coincide con §1 del mismo README y con `Definicion-Superficie-HTTP.md` §7, cuyas siete filas conté |
| `B-API-10` | P2 | `Guia-Onboarding-Developer.md` §1: «y **catorce** no lo están» | **CERRADO** | §1 lectura obligatoria 1 dice hoy «y catorce no lo están». `Definicion-Superficie-HTTP.md` §3 rotula sólo `A-01` como **[declarada por la fuente]** y §2 habla de «los **catorce** puntos restantes». Ver §4 de este informe: la corroboración que r1 daba para este hallazgo era falsa, y el hallazgo igual lo era |
| `B-API-11` | P2 | `Guia-Onboarding-Developer.md` §5: los **dos** huecos | **CERRADO** | §5 última viñeta dice hoy «los **dos** huecos elevados al Product Owner». `Especificacion-Funcional.md` §11 dice «Los **dos** primeros son huecos de la superficie». `grep "tres huecos"` sobre las dos categorías: cero fuera de controles de cambios |
| `B-API-12` | P2 | La vigencia del acceso firmado, rotulada heredada, y el reparto a **6 + 5** | **CERRADO** | Conté las filas de `Especificacion-Funcional.md` §11: **doce filas, una tachada, once vivas**; seis llevan «**Propio.**» y la de la vigencia lleva hoy «**Heredado.**». El párrafo de apertura dice «**Seis son propios de esta categoría y cinco vienen declarados de aguas arriba**». Los otros dos lugares que citan el reparto —`02/README.md` y `03/README.md` §6— dicen hoy «seis propios / cinco heredados». `Definicion-Superficie-HTTP.md` §9 no cambia y no debía: sus «cinco primeros propios y el sexto heredado» son los puntos abiertos **de ese documento**, otro conjunto |
| `B-API-13` | P3 | Cabeceras que citan versiones de fuente envejecidas | **CERRADO, y más ampliamente de lo que el hallazgo pedía** | Extraje con `git show 302c127:` la cabecera de los 21 artefactos **antes** de la corrección: **diecinueve** citaban `PRODUCT-INTAKE` 1.13 o 1.14; los dos que no la citaban son `02/README.md` y `Glosario-UX.md`. Hoy los diecinueve citan **1.26**, y los cuatro que citaban manifiesto citan **1.3**, verificados como vigentes en la cabecera del intake (`Versión 1.26`) y del manifiesto (`1.3`) |
| `B-API-14` | P3 | `Definicion-Superficie-HTTP.md` §10 en orden ascendente | **CERRADO** | Las filas van hoy **1.0, 1.1, 1.2, 1.3, 1.4** |
| `B-API-15` | P3 | Dos auto-citas de sección erradas en controles de cambios | **CERRADO** | La fila 1.1 de `Guia-Onboarding-Developer.md` dice hoy «**§3.5** actualiza el conjunto cerrado» y «**§6.3** actualiza el procedimiento de alta», que son las secciones reales. La fila 1.1 de `CU-09` ya no dice «las siete condiciones de §6»: dice «**Las dos traducciones, la tabla de destinos y las tres reglas de asignación de §6** no cambian de forma», y §6 declara `R-1` a `R-3`. La fila 1.2 de cada archivo declara la enmienda de la fila anterior, que es la forma correcta de hacerlo |
| `B-API-16` | P3 | El motivo de la omisión de `DX-Operability.md`, con el flag | **CERRADO** | `03/README.md` §4 suma hoy la condición de la guía —recomendado para «`rest-api` con SLO estricto»— y declara `tiene_observabilidad_critica` == true con lo que ese flag registra. Verifiqué la cita nueva palabra por palabra contra `PRODUCT-MANIFEST` 1.3 §5: dice «`tiene_observabilidad_critica` true en `Api`: §17.5 P.10 declara latencia p99 con métrica numérica (500 ms). **No hay SLO de disponibilidad en ningún proyecto de código**». **La cita existe y es literal**, y la omisión sigue siendo correcta |
| `B-API-17` | P3 | El `503` del punto de salud, clasificado sin ambigüedad | **CERRADO** | `DX-Error-Messages.md` §6.2, tercera comprobación, dice hoy que es «un `503` **cubierto por la entrada de no clasificado con su código**, `CONTRATO_ERROR_NO_CLASIFICADO`, y **no** una tercera respuesta sin código: las respuestas sin código son las **dos** de §2.2». Contrastado con §2.2, con §3.7 —que enumera `403`, `409`, `500` o `503` para ese código— y con `Definicion-Superficie-HTTP.md` §6. Ningún recuento se movió, y no debía |

**Diecisiete de diecisiete cerrados.** Ninguno quedó cerrado por declaración: los diecisiete se verificaron abriendo la sección y contando.

---

## 3. El P0, contado a mano

**Las siete tablas de §3 de `DX-Error-Messages.md`, fila por fila:**

| Sección | Filas | Con código del contrato | Sin código |
| --- | --- | --- | --- |
| §3.1 Entrada inválida | 3 | 2 | 1 |
| §3.2 Credencial no admitida | 2 | 1 | 1 |
| §3.3 Situación de la cuenta | 2 | 2 | 0 |
| §3.4 Facultad | 1 | 1 | 0 |
| §3.5 Recurso no visible | 2 | 2 | 0 |
| §3.6 Conflicto de estado | 5 | 5 | 0 |
| §3.7 No clasificado | 1 | 1 | 0 |
| **Total** | **16** | **14** | **2** |

**Dieciséis entradas = catorce + dos.** Coincide con §6.1 y §6.2 del propio catálogo, con `Definicion-Superficie-HTTP.md` §6 —«Quince códigos: catorce con destino en esta superficie y uno sin él»— y con el dueño del conjunto, `Contracts/CU-06` §10.

**Los seis lugares vivos, verificados uno por uno**, dicen hoy los tres números correctos. **No hay un séptimo**: el barrido `grep -rn "18 entradas\|18 de 18\|las \*\*18\*\*\|dieciocho entradas\|18 situaciones\|de las 18\|las 18 "` sobre todo `SDD/Docs` excluido `Audit/` devuelve **una sola línea**, la fila 1.0 del control de cambios del catálogo. El barrido complementario de «16 códigos del contrato con destino» y «dieciséis códigos con destino» devuelve la misma única línea.

**Aguas abajo, el número correcto ya estaba y sigue estando.** `08-Calidad-Y-Pruebas/README.md` publica «Entradas del catálogo de condiciones | **16** — los 14 códigos con destino más las 2 respuestas sin código | `03` §6.1», y `05-Arquitectura-Tecnica/Contratos-REST.md` §5 publica quince filas. La cadena 05 a 11 nunca heredó el número malo.

---

## 4. Las tres correcciones que la corrección le hizo a la ronda 1

Las tres se verificaron sobre el instrumento, no sobre lo que la corrección afirma. **Las tres son verdaderas: en las tres se equivocó la ronda 1.**

**(a) `B-API-13` estaba subcontado por más del doble. VERDADERA.** Extraje la cabecera de trazabilidad de los 21 artefactos en el commit anterior a la corrección (`302c127`) y conté cuántas citaban una versión de fuente vencida: **diecinueve**. La ronda 1 listaba nueve, y **los doce casos de uso citaban `PRODUCT-INTAKE` 1.13 en su cabecera**, con el informe listando sólo a `CU-12`. La ronda 1 además incluyó en su tabla a `Glosario-UX.md`, cuya cabecera **no cita ni el intake ni el manifiesto** —su trazabilidad upstream termina en `Vocabulario-Rules.md`—, de modo que la lista de nueve no sólo era corta: contenía un falso positivo. La corrección tocó las diecinueve y no tocó `Glosario-UX.md` en su cabecera, que es lo correcto.

**(b) `B-API-10` apoya su recuento en una cita que no existe donde la ubica. VERDADERA.** La ronda 1 enumera cinco lugares donde el corpus dice bien «catorce de las quince rutas», y el quinto es «el control de cambios **1.1 de esta misma guía**». Abrí esa fila en el estado anterior a la corrección: dice «§4 actualiza el conjunto cerrado de códigos de diecisiete a **quince**» y «§7 actualiza el procedimiento… los puntos a revisar pasan de dieciséis a **quince**». **No menciona rutas ni el reparto catorce/quince.** Los lugares vivos son **cuatro** —`02/README.md` §5, `03/README.md` §1 y §6, `DX-Developer-Experience.md` §1.2 punto 2— más `Definicion-Superficie-HTTP.md` §2, que habla de «los catorce puntos restantes»; los conté con `grep`. El hallazgo era correcto en su sustancia y falso en una de sus cinco corroboraciones.

**(c) La aritmética de la ronda 1 no cierra. VERDADERA, y en tres lugares del informe.** Su §1 dice «**quince hallazgos: un P0, cinco P1, seis P2 y cinco P3**» — 1 + 5 + 6 + 5 = **17**. Su §7 abre con «Quince hallazgos» y a continuación enumera **`B-API-01` a `B-API-17`**. Su §10 pide «corregir los quince hallazgos» y su control de cambios repite «Levanta **quince hallazgos: un P0, cinco P1, seis P2 y cinco P3**». **La corrección resolvió los diecisiete**, que es lo que corresponde: los diecisiete existen, están enunciados con dónde, qué y cómo, y los diecisiete están cerrados.

**Y hay un cuarto defecto de la ronda 1 que la corrección no señaló** y que este informe registra para que no se propague: **la ronda 1 usa un esquema de identificadores paralelo que su propia §7 no define**. Sus §3 y §4 remiten a `P0-01`, `P1-01`, `P1-02`, `P2-03`, `P2-06` y `P3-01`, y en §7 los hallazgos se llaman `B-API-01` a `B-API-17`. Son **identificadores fantasma**: nada en el informe los liga. Su §3 además dice «Falso en **cinco** lugares (`P0-01`)» donde su §7 dice seis. No es hallazgo contra los 21 artefactos —no es documento del corpus de producto— pero sí es lo que explica el hallazgo `N-02` de §6.

---

## 5. Dictamen sobre la causa raíz

La ronda 1 identificó la causa raíz correctamente: **controles de cambios que declaraban correcciones que no se completaron**, con tres ejemplos verificables —`CU-09` 1.1 que dice haber actualizado §1 dejándolo a medias, `CU-12` 1.1 que tocó §8 y no §9, `03/README.md` 1.2 que corrigió un número de una oración sin recontar el de al lado—. Y fijó una condición de método: **que cada recuento corregido lleve escrito en su fila de control de cambios el alcance de la búsqueda de propagación.**

**La condición está implementada, y en todas las filas que corrigen un recuento, no en algunas.** Las once filas de control de cambios que la corrección agregó en las categorías 02 y 03 llevan la fórmula «Búsqueda de propagación hecha con `grep` sobre todo el corpus vivo, según la condición de método del informe» seguida del alcance con su número.

**Y el alcance declarado es verdadero en su recuento de lugares.** Conté los seis alcances que enuncian un número:

| Alcance declarado | Dónde | Conté |
| --- | --- | --- |
| «el recuento del catálogo se citaba mal en **seis lugares** vivos» | `DX-Error-Messages.md` 1.4 y otras cuatro filas | **Seis.** `03/README.md` §2, `Glosario-UX.md` §2 y §3.1, `DX-Developer-Experience.md` §5 y §6, `Guia-Onboarding-Developer.md` §3.5 |
| «**cuatro** citas vivas del reparto en el proyecto de código, y las otras dos ya decían tres» | `03/README.md` 1.3 | **Cuatro.** `Especificacion-Funcional.md` §6 (fuente), `08/Matriz-Cobertura-Pruebas.md`, `03/README.md` §6, `DX-Developer-Experience.md` §8. Las dos que ya decían tres son las dos primeras |
| «el reparto de puntos abiertos se corrige en los **tres** lugares vivos que lo citan» | `03/README.md` 1.3 | **Tres.** `Especificacion-Funcional.md` §11, `02/README.md`, `03/README.md` §6 |
| «las celdas mutiladas eran **dos**» | `CU-01` 1.2 y `CU-05` 1.2 | **Dos**, y ninguna sobrevive |
| «el `grep` devuelve **diecinueve** archivos con la cita vieja, los doce casos de uso entre ellos» | `CU-02` 1.2 y las once filas hermanas | **Diecinueve**, contados sobre el commit anterior |
| «aparece bien en **cuatro** lugares vivos de tres documentos… ésta era la única desviada» | `Guia-Onboarding-Developer.md` 1.2 | **Cuatro lugares en tres documentos**, exacto |

**El método además produjo lo que un método sirve para producir: encontró lo que la ronda 1 no había mirado.** Tres cosas, las tres verificadas:

1. **Las doce cabeceras de casos de uso** que el informe no listaba (§4a).
2. **El mismo defecto de reparto vivo en `GeometriaFactory-Infrastructure`**, que este informe no audita. `Infrastructure/03/DX-Developer-Experience.md` §8 y su `03/README.md` §6 decían «**trece** con tramo acá», y la fuente de ese proyecto de código, `Infrastructure/02/Especificacion-Funcional.md` §6, dice «**Catorce** de las dieciséis tienen tramo acá y dos no lo tienen». Los dos pasan hoy a «catorce». **Verifiqué la fuente y la corrección es correcta**: buscar el patrón en vez de la ocurrencia es exactamente lo que la condición de método pedía.
3. **Tres documentos de nivel producto** que afirmaban que el informe de Fase B de la `Api` no existe, hoy falso.

**Dictamen sobre la causa raíz: LEVANTADA.** El vector que la ronda 1 describió —corregir en el origen sin preguntar quién cita— está atacado con un instrumento que deja rastro auditable y que en esta tanda funcionó. La reserva está en §6: **el instrumento heredó, sin recontarlo, un número de la ronda 1 que es falso**, y lo hizo cinco veces. Es el mismo defecto, cometido dentro del remedio, sobre una cifra que no es del producto.

---

## 6. Hallazgos nuevos

### `N-01` (P2) · Cinco filas de alcance de propagación dicen «cinco documentos» donde son cuatro

**Dónde está.** En las cinco filas de control de cambios que declaran el alcance de la propagación del recuento del catálogo:

| Archivo | Fila | Qué dice |
| --- | --- | --- |
| `03-UX-UI-DX/DX-Error-Messages.md` | 1.4 | «se citaban mal en **seis lugares vivos de cinco documentos** de `03-UX-UI-DX` —`README.md` §2, `Glosario-UX.md` §2 y §3.1, `DX-Developer-Experience.md` §5 y §6, `Guia-Onboarding-Developer.md` §3.5—» |
| `03-UX-UI-DX/Glosario-UX.md` | 1.2 | «se citaba mal en **seis lugares de cinco documentos**, todos de esta categoría» |
| `03-UX-UI-DX/DX-Developer-Experience.md` | 1.2 | «se citaba mal en **seis lugares vivos de cinco documentos** de esta categoría —dos de ellos en este archivo—» |
| `03-UX-UI-DX/README.md` | 1.3 | «se citaba mal en **seis lugares vivos de cinco documentos**, todos de esta categoría» |
| `03-UX-UI-DX/Guia-Onboarding-Developer.md` | 1.2 | «se citaba mal en **seis lugares de cinco documentos**» |

**Qué debería decir.** **Cuatro** documentos.

**Cómo lo verifiqué.** Conté los archivos distintos de la enumeración que la propia fila de `DX-Error-Messages.md` 1.4 escribe en la misma oración: `README.md`, `Glosario-UX.md`, `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` — **cuatro**. Los seis lugares son seis y ése es el número que importa; el que no cierra es el de documentos. La fila de `DX-Error-Messages.md` **se contradice dentro de la misma frase**: dice cinco y enumera cuatro.

**De dónde viene.** De la ronda 1, que en su §7 escribe «Seis lugares vivos en **cinco documentos**» y en su §1 «los **cinco documentos** que lo citan», enumerando cuatro. **La corrección lo copió sin recontarlo.**

**Por qué P2 y no P3.** No falsifica ningún número del producto y las seis correcciones sustantivas están bien hechas. Pero está **en el instrumento mismo de la causa raíz**: la fila que declara el alcance de la búsqueda de propagación es lo único que una revisión posterior puede usar para saber si la búsqueda fue completa, y esta ronda tenía por encargo verificar que ese alcance fuera verdadero. Un alcance con un número falso es exactamente lo que la ronda 1 llamó «peor que la ausencia de corrección: apaga la próxima revisión». Que el número falso sea el de documentos y no el de lugares es lo que lo mantiene en P2.

---

### `N-02` (P2) · Tres documentos de nivel producto quedaron afirmando «quince hallazgos» con un desglose que suma diecisiete

**Dónde está.** Los tres son archivos que la corrección escribió en este mismo commit:

- `SDD/Docs/Handoff-Checkout.md` §6.1, salvedad `B-1`: «Su dictamen es **RECHAZADO**, con **quince hallazgos —un P0, cinco P1, seis P2 y cinco P3—**…». **El desglose que suma diecisiete está en la misma oración que el «quince».**
- `SDD/Docs/README.md` §8: «emitido el 2026-08-11 con **quince hallazgos** de recuento y de cita».
- `SDD/Docs/Producto/Vista-Producto.md` §1.1: «con dictamen **RECHAZADO** por **quince hallazgos** de recuento y de cita».

**Qué debería decir.** **Diecisiete**: un P0, cinco P1, seis P2 y cinco P3. Es el número de hallazgos que la ronda 1 enunció (`B-API-01` a `B-API-17`) y el que la corrección efectivamente resolvió.

**Cómo lo verifiqué.** Sumé el desglose: 1 + 5 + 6 + 5 = 17. Conté los identificadores enunciados en §7 del informe de ronda 1: diecisiete, sin saltos. Conté las filas de control de cambios que cierran cada uno: los diecisiete están referenciados.

**Por qué es un hallazgo nuevo y no una herencia.** Porque **la corrección lo escribió sabiendo que era falso**. Su propio mensaje de commit dice: «la aritmética del informe de la API no cierra: dice quince hallazgos y enumera diecisiete. **Resolvió los diecisiete**». Es decir: identificó el defecto, corrigió las diecisiete cosas, y a continuación **copió el número equivocado a tres documentos de nivel producto** —los tres de mayor alcance de lectura del corpus, incluido el documento de check-out del handoff— sin aplicarles la misma corrección. En `Handoff-Checkout.md` la frase es **autorrefutable a la vista**.

**Por qué P2.** No toca ninguna decisión, ningún recuento del producto ni ningún criterio verificable. Toca la trazabilidad del proceso de auditoría, y la toca en los tres documentos que un equipo entrante lee primero. Se corrige con tres reescrituras de una palabra.

**Nota de propagación para quien lo cierre.** Los tres documentos declaran además que la Fase B de `GeometriaFactory-Api` «**falta la ronda 2 que lo levante**» (`README.md` §8, `Handoff-Checkout.md` §6.1 `B-1`, `Vista-Producto.md` §1.1) y `Handoff-Checkout.md` §5 cuenta «**32** informes» con `ls SDD/Docs/Audit/` —que verifiqué: eran 32—. **Este informe es el trigésimo tercero y levanta el rechazo**, de modo que las tres declaraciones y el recuento pasan a ser falsos con su emisión. **No es hallazgo contra la corrección** —eran verdaderos al escribirse— pero es trabajo obligado de la misma tanda que cierre `N-02`, y conviene hacerlo en un solo pase.

---

## 7. Barrido de regresión

La corrección tocó 33 archivos. Barrí los 33 buscando lo que una corrección de recuentos rompe típicamente.

| Qué busqué | Cómo | Resultado |
| --- | --- | --- |
| **Enlaces relativos rotos** | Resolví contra el sistema de archivos los 100 % de los destinos de enlace de los 33 archivos tocados, incluidos los enlaces nuevos a `../../../Audit/B-02-03-GeometriaFactory-Api-r1.md` que agregan las filas de control de cambios | **Cero rotos** |
| **Filas de tabla discordantes** | Recorrido programático de todas las tablas de los 33 archivos, comparando el número de celdas de cada fila con el de su encabezado | **Cero discordantes.** Los positivos que arrojó el recorrido son todos tuberías escapadas (`\|`) dentro de código en línea —los `grep` de `Handoff-Checkout.md` §5 y la columna de evidencia de `Matriz-Sensado-Deriva.md`—, verificados uno por uno. Las cuatro filas con una celda de más que la ronda 1 dejó señaladas están corregidas |
| **Identificadores fantasma** | Enumeré los `A-XX` de `Definicion-Superficie-HTTP.md` y los contrasté con los que citan los documentos corregidos | **Ninguno.** `A-08` y `A-16`, que entran en la corrección de `CU-12` §9, existen y están vivos. Los quince puntos son `A-01` a `A-03` y `A-05` a `A-16`, contados sobre las quince filas de §3 |
| **Citas nuevas sin verificar** | Las tres citas que la corrección introduce y que no estaban en el corpus: `Arquitectura-Proyecto-Codigo.md` §3, `PRODUCT-MANIFEST` 1.3 §5 y `Infrastructure/02/Especificacion-Funcional.md` §6 | **Las tres existen y son literales.** Ver §2, filas `B-API-05` y `B-API-16`, y §5 punto 2 |
| **Recuentos que dejaron de cerrar** | Barrido de los números que la corrección movió, sobre todo el corpus vivo: «18 entradas», «16 códigos con destino», «diecisiete», «dieciséis puntos», «13 de 16», «tres huecos», «siete propios», «cuatro heredados», «dos sin él» | **Cero afirmaciones vivas equivocadas.** Todas las ocurrencias restantes son celdas de control de cambios que describen el estado de una versión anterior, que es su función |
| **Coherencia con los consumidores aguas abajo** | `05-Arquitectura-Tecnica/Contratos-REST.md` §5 y `Arquitectura-Proyecto-Codigo.md` §3; `06-Backlog-Tecnico/README.md` y `Product-Backlog.md`; `08-Calidad-Y-Pruebas/README.md` y `Matriz-Cobertura-Pruebas.md`; `10-Examples/README.md` | **Coherentes.** `08/README.md` publica «16 — los 14 códigos con destino más las 2 respuestas sin código», `08/Matriz-Cobertura-Pruebas.md` publica «Trece de las dieciséis con tramo acá y tres sin él», y `10-Examples/README.md` §2 dejó de decir que el residuo de `CU-12` §9 sigue abierto y registra que `CU-12` 1.3 lo corrigió. **La corrección propagó hacia adelante y no sólo hacia los pares** |
| **Correcciones fuera de las dos categorías auditadas** | Los cambios en `Infrastructure`, `Visor` y `Web` | **Correctos en lo que verifiqué.** El de `Infrastructure` está verificado contra su fuente (§5). Los de `Web` cierran hallazgos de `B2-Maqueta-GeometriaFactory-Web-r2.md`, que **no es alcance de este informe** y quedan declarados no verificados en §9 |

**Regresiones encontradas: cero de sustancia.** Los dos hallazgos de §6 no son regresiones en sentido estricto: `N-01` es un número heredado de la ronda 1 y copiado, y `N-02` es un número heredado de la ronda 1, copiado **después de haberlo declarado falso**.

---

## 8. Lo verificado y no reportado

Se declara para que una ronda posterior no lo vuelva a levantar.

1. **Las polisemias siguen intactas y sus contextos siguen colisionando.** La corrección tocó la primera fila de la polisemia «error» de `Glosario-UX.md` §3.1 sólo en su número (18 → 16). Ningún referente, ninguna forma obligatoria y ninguna evidencia de colisión cambió. **Las cinco no-polisemias declaradas siguen siendo no-polisemias con contextos disjuntos, y reportarlas sería un defecto de este informe.**
2. **El «dieciocho» de `Glosario-UX.md` no es el «dieciocho» del catálogo.** El glosario acuña dieciocho términos y el catálogo tenía dieciocho entradas; son conjuntos sin relación, la corrección lo dice explícitamente en su fila 1.2 y verifiqué que el recuento de términos de §2 **no se tocó** y sigue siendo correcto. **Es polisemia de número con contextos disjuntos y no es hallazgo.**
3. **Los once puntos abiertos siguen siendo once, siguen siendo verdaderos y siguen teniendo titular.** Lo que cambió es el reparto, no el conjunto. Un punto abierto correctamente declarado no es hallazgo.
4. **Las omisiones y apartamientos siguen declarados con motivo y con el flag que los habilita.** La de `DX-Operability.md` está hoy **mejor motivada** que antes y sigue siendo correcta: el manifiesto no declara SLO de disponibilidad en ningún proyecto de código.
5. **Ninguna corrección subió versión mayor y ninguna tocó una decisión.** Verificado sobre las cabeceras: todas las subidas son minor, y todas las filas declaran explícitamente qué **no** cambia.
6. **La enmienda de dos filas históricas de control de cambios** —la 1.1 de `Guia-Onboarding-Developer.md` y la 1.1 de `CU-09`— es la forma correcta de cerrar `B-API-15`: la fila enmendada corrige su auto-cita y la fila nueva declara la enmienda. **No es reescritura de la historia y no se levanta.**

---

## 9. Lo no verificado

- **La conformidad de las dos categorías con `Rules-Especificacion-Funcional.md` 4.0 y `Rules-UX-UI-DX.md` criterio por criterio.** La ronda 1 la verificó y la declaró cumplida en estructura, completitud y forma; esta ronda es de **cierre**, y no la reabrió. Lo que sí verifiqué es que ninguna corrección de esta tanda tocó estructura, secciones obligatorias ni artefactos emitidos.
- **Los cambios en `GeometriaFactory-Web` y `GeometriaFactory-Visor`** que el commit `bef453d` incluye. Cierran hallazgos de `B2-Maqueta-GeometriaFactory-Web-r2.md`, que es otro informe con otro alcance. Verifiqué únicamente que no rompen tablas ni enlaces.
- **Que los guiones del quick-start existan y corran.** Sigue fuera de alcance de una auditoría documental, igual que en la ronda 1.
- **Los diez casos de la batería del validador.** La discrepancia interna del intake que la ronda 1 declaró sigue sin tocar ninguno de los 21 artefactos, y sigue perteneciendo a quien audite la fuente.
- **Que la corrección de `Infrastructure` no deje otro residuo en las categorías 05 a 11 de ese proyecto de código.** Verifiqué la corrección contra su fuente; no barrí los consumidores de `Infrastructure`.

---

## 10. Dictamen

# APROBADO

**Se levanta el rechazo de `B-02-03-GeometriaFactory-Api-r1.md` 1.0.**

**Fundamento.**

1. **El P0 está cerrado en los seis lugares y no hay un séptimo.** Conté las siete tablas del catálogo a mano —dieciséis entradas, catorce con código y dos sin él— y barrí el corpus vivo entero: la afirmación falsa no sobrevive en ningún lugar, ni en las categorías 02 y 03, ni en los consumidores de 05 a 11. La métrica de `DX-Developer-Experience.md` §6, que era el peor de los seis por ser un objetivo declarado con su forma de medición, hoy **da verdadero al medirse exactamente como manda su propio texto**.
2. **Los dieciséis hallazgos restantes están cerrados, uno por uno, sobre el instrumento.** Incluidos los tres que el encargo señalaba: `CU-12` §9 dice hoy «13 de 15, los dos que no: `A-08` y `A-16`»; los dos restos mutilados que dejaban viva una referencia a `A-04` desaparecieron y ninguna mención viva de `A-04` lo describe ya como camino vigente; y el «tres motivos comparten el `403`» pasó a dos, con el camino que `RN-16` suprimió fuera de la enumeración.
3. **La causa raíz está atacada con un instrumento verdadero.** Los seis alcances de propagación que declaran un número se contaron y los seis dan el número que dicen en su recuento de lugares. El método además encontró tres cosas que la ronda 1 no había mirado, incluido el mismo defecto vivo en otro proyecto de código.
4. **Las tres correcciones que la corrección le hizo a la ronda 1 son verdaderas**, verificadas contra el estado del repositorio anterior al commit y contra la aritmética del propio informe. La ronda 1 se equivocó en las tres, y la corrección hizo lo correcto: resolver diecisiete.
5. **Ninguna corrección tocó una decisión, un contrato, una regla, un punto de acceso ni un código.** Todas son reescrituras de recuento y de frase, todas con subida minor, todas con su fila declarando qué no cambia.

**Los dos hallazgos nuevos son P2 y ninguno justifica sostener el rechazo**: no falsifican ningún número del producto, no afectan ningún criterio verificable y se cierran con tres reescrituras de una palabra y una de dos. Pero **los dos son afirmaciones falsas vivas**, y de eso se ocupa la sección que sigue.

---

## 11. Opinión sobre la promoción a `Aprobado`

Se me pide opinión sobre si las categorías **02-Especificacion-Funcional** y **03-UX-UI-DX** de `GeometriaFactory-Api` están en condiciones de promoverse de `Propuesto` a `Aprobado`.

**Sí, los 21 artefactos están en condiciones.** Y lo digo con la reserva explícita de que **`Aprobado` no es un adorno**: es el sello que dice que lo que el documento afirma sobre una fuente viva es verdadero hoy. Sobre esos 21 archivos hice el barrido completo de los números que esta cadena vio envejecer y **no queda ninguno equivocado**: ni el catálogo, ni el conjunto cerrado, ni los puntos de acceso, ni las rutas derivadas, ni las ausencias, ni los huecos elevados, ni el reparto de reglas, ni el reparto de puntos abiertos, ni las cabeceras de trazabilidad. Los enlaces resuelven, las tablas cierran, los identificadores existen. Este proyecto de código era el que llegaba sin auditoría de Fase B y llega hoy con dos rondas.

**Y hay dos cosas que, en mi opinión, deben cerrarse antes de estampar el sello. Ninguna está dentro de los 21 archivos.**

**Primera, y es la que más me preocupa: `N-02`.** Tres documentos de nivel producto —`Handoff-Checkout.md`, `SDD/Docs/README.md` y `Producto/Vista-Producto.md`— dicen hoy que este proyecto de código fue rechazado por «quince hallazgos», con el desglose que suma diecisiete al lado. Son los tres documentos que un equipo entrante lee antes que nada, y **la frase se refuta sola a la vista**. Que el número venga de la ronda 1 no lo mejora: la corrección lo declaró falso en su mensaje de commit y lo escribió igual. Promover el corpus con eso adentro es sellar como aprobada una frase que el propio corrector sabía falsa, y es precisamente el gesto que este producto viene pagando caro. **Corregirlo cuesta tres palabras.**

**Segunda: `N-01`.** Cinco filas de control de cambios declaran un alcance de propagación de «cinco documentos» que son cuatro, y una de ellas los enumera en la misma oración. El alcance de propagación es el instrumento que esta ronda tenía por encargo verificar, y es lo único con lo que una ronda futura puede saber si una búsqueda fue completa. **Un instrumento de verificación con un número falso adentro no puede promoverse a `Aprobado`**: es el mismo mecanismo que el propio corpus describió como «una verificación que blinda el número equivocado».

**Y una tercera cosa, que no es hallazgo pero es trabajo obligado de la misma tanda.** Con la emisión de este informe, las tres declaraciones de que «falta la ronda 2 que lo levante» y el recuento de «32 informes» de `Handoff-Checkout.md` §5 pasan a ser falsos. Conviene cerrarlas en el mismo pase que `N-02`, porque son los mismos tres archivos.

**Recomendación concreta.** Promover las categorías **02** y **03** de `GeometriaFactory-Api` a `Aprobado` **después** de un pase corto sobre cinco filas de control de cambios y tres documentos de nivel producto. Es media hora de trabajo y ninguna decisión. **No recomiendo promover el corpus completo antes de ese pase**, no por la magnitud de lo que queda mal, que es mínima, sino por lo que significa: el sello `Aprobado` sobre una frase autorrefutable enseña que el sello no verifica nada, y este corpus se pasó siete fases construyendo lo contrario.

**Lo que sí digo sin reserva:** no hay nada en estas dos categorías que obligue a rehacer una decisión, reabrir un contrato, retocar un punto de acceso o revisar una regla. **Lo que queda es de recuento y de cita, y esta vez está afuera de los documentos que se promueven.**

---

## 12. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Ronda 2 de la auditoría de Fase B de las categorías 02 y 03 de `GeometriaFactory-Api`. Verifica el cierre de los **diecisiete** hallazgos de la ronda 1 —no quince: la aritmética del informe anterior no cerraba y este informe lo dictamina— sobre el commit de corrección `bef453d`, abriendo cada sección y contando, sin apoyarse en ninguna declaración de control de cambios. Cuenta a mano las siete tablas de §3 de `DX-Error-Messages.md` (3+2+2+1+2+5+1 = **16** = 14 + 2), verifica los **seis** lugares del P0 y barre el corpus vivo para descartar un séptimo. Verifica las **tres** correcciones que la corrección le hizo a la ronda 1 y las declara **las tres verdaderas**: diecinueve cabeceras envejecidas y no nueve, una cita inexistente en `B-API-10`, y la aritmética que no cierra. Dictamina la **causa raíz LEVANTADA**, contando los **seis** alcances de propagación declarados y verificándolos verdaderos en su recuento de lugares. Registra un cuarto defecto de la ronda 1 no señalado por la corrección: los identificadores fantasma `P0-01` a `P3-01`. Barrido de regresión sobre los **33** archivos tocados: cero enlaces rotos, cero filas discordantes, cero identificadores fantasma, tres citas nuevas verificadas literales, cero recuentos vivos equivocados. Levanta **dos hallazgos nuevos, los dos P2**: el alcance de propagación que dice «cinco documentos» donde son cuatro, y los tres documentos de nivel producto que quedaron afirmando «quince hallazgos» con el desglose que suma diecisiete al lado. **Dictamen: APROBADO**, se levanta el rechazo, con recomendación de cerrar los dos P2 —cinco filas y tres archivos, todos fuera de los 21 artefactos promovidos— antes de sellar el corpus como `Aprobado`. |
