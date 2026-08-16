# Auditoría de la Fase C · categoría 05 Arquitectura Técnica de los siete proyectos de código · ronda 1

| Campo | Valor |
| --- | --- |
| Producto | Fábrica de Geometría |
| Rama auditada | `sdd/fase-c-arquitectura` |
| Alcance auditado | Los **76** archivos de `Proyectos/*/05-Arquitectura-Tecnica/` de los **siete** proyectos de código, emitidos en tres olas por los commits `ee73c99`, `e176845` y `0a71935`; más `GeometriaFactory-Api/03-UX-UI-DX/DX-Error-Messages.md` **1.2**, por el defecto ya localizado |
| Fuentes de contraste | `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.18**, `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` **1.2**, las categorías 02 y 03 de cada proyecto de código, y `IA.SDD/SDD/Devs/Rules/Rules-Arquitectura-Tecnica.md` **3.1** (repositorio de origen, sólo lectura) |
| Criterio de la ronda | **El instrumento, no la declaración.** Ningún recuento se acepta por estar escrito: se cuenta sobre el archivo. Ninguna cita cruzada se acepta por estar entrecomillada: se abre la fuente y se busca la cadena. Las tablas se verifican por celda con script, no por lectura |
| Fuera de alcance | `_legacy/`; las tres fuentes del intake, que viven en otro repositorio bajo `PROMPTs/`; las categorías 04 y 06 a 11, no emitidas |
| Auditor | Auditor independiente, sin participación en la generación de ninguna de las tres olas |
| Fecha | 2026-08-10 |
| Volumen contado | **76** archivos vivos en la categoría; **45** ADR individuales; **7** documentos maestros; **7** índices de decisiones; **7** README de sección; **4** contratos de superficie, **1** modelo lógico, **2** flujos de ejecución, **1** extensibilidad |

---

## Tabla de contenido

- [1. Resumen ejecutivo](#1-resumen-ejecutivo)
- [2. Coherencia entre las siete arquitecturas](#2-coherencia-entre-las-siete-arquitecturas)
- [3. Afirmaciones sobre otras fuentes, verificadas abriendo la fuente](#3-afirmaciones-sobre-otras-fuentes-verificadas-abriendo-la-fuente)
- [4. Lo que no se lee como texto corrido](#4-lo-que-no-se-lee-como-texto-corrido)
- [5. Recuentos y conjuntos cerrados, contados por el auditor](#5-recuentos-y-conjuntos-cerrados-contados-por-el-auditor)
- [6. Las tres reglas de arquitectura](#6-las-tres-reglas-de-arquitectura)
- [7. Forma y conformidad con la guía](#7-forma-y-conformidad-con-la-guia)
- [8. Hallazgos](#8-hallazgos)
- [9. Lo que no reporto, y lo que no pude verificar](#9-lo-que-no-reporto-y-lo-que-no-pude-verificar)
- [10. Dictamen](#10-dictamen)
- [11. Estado general de la arquitectura del producto](#11-estado-general-de-la-arquitectura-del-producto)

---

## 1. Resumen ejecutivo

**Esta es la emisión mecánicamente más limpia que este corpus produjo.** Los barridos que en rondas anteriores levantaban defectos por decenas dan cero: **cero filas con distinto número de celdas que su encabezado** en las 76 tablas del árbol de la categoría, **cero enlaces relativos rotos** sobre las referencias que apuntan a archivo, **cero ADR con la numeración o el orden de sus diez secciones alterado** sobre 45, **cero divergencias entre el índice `Decisiones-Arquitectura.md` y los archivos reales de `Adrs/`** en los siete proyectos de código, y **cero recuentos de NFR, riesgos y puntos abiertos que no cierren** contra las filas de sus propias tablas.

Los tres reasignados entre capas están **los tres bien resueltos**, y lo verifiqué en las dos puntas: el **criterio de comparación de correos** lo cierra `Infrastructure ADR-03` con su índice `IX-01`, exactamente donde `Application` §11 lo había derivado; el **formato de intercambio** lo cierra `Api ADR-02` y `Contratos-REST.md` §2.2 para los dos extremos, después de que `Web` declarara por escrito que no lo decide de un solo lado; y el **nombre del cuarto puerto** está declarado **no cerrado con fundamento**, atado al punto de control de la etapa `a` por `Application ADR-02`, y las dos capas de arriba —`Infrastructure` y `Api`— lo citan y **no lo reabren**. Ninguna de las tres es hallazgo.

Levanto **seis hallazgos**: **ninguno P0**, **tres P1**, **dos P2** y **uno P3**. Los tres P1 son de la misma familia que viene rechazando este producto —**decir de una fuente algo que la fuente no dice**— y dos de ellos son su variante peor: **puntos abiertos falsos**, trabajo asignado al Product Owner sobre algo que el Product Owner ya hizo, en el **mismo commit** que emite el documento. El tercero es el defecto ya localizado de `DX-Error-Messages.md`, que confirmo y cuyos números verdaderos doy contados fila por fila.

El patrón de fondo no cambió de naturaleza, cambió de lugar: **la emisión ya no congela recuentos propios, congela lo que cree recordar de la fuente**. Los siete documentos maestros cuentan sus propios conjuntos bien; los dos que fallan lo hacen citando el intake de memoria, en la celda de una tabla de puntos abiertos, que es exactamente el género de texto donde este corpus siempre falló.

---

## 2. Coherencia entre las siete arquitecturas

### 2.1 El orden topológico y la regla de citar sin rehacer

Las tres olas y su contenido, verificados con `git diff --stat` por commit:

| Ola | Commit | Proyectos de código | Nivel | Intake citado como insumo |
| --- | --- | --- | --- | --- |
| 1 | `ee73c99` | `Domain`, `Contracts`, `Visor` | 0 | **1.15** |
| 2 | `e176845` | `Application`, `Web` | 1 | **1.16** |
| 3 | `0a71935` | `Infrastructure`, `Api` | 2 y 3 | **1.17** |

La cadena de versiones del intake es la correcta: cada ola cita como insumo la versión vigente al empezar, y cada ola devuelve una versión nueva al intake con lo que levantó. **No hay ninguna ola que cite una versión posterior a la que existía cuando se emitió**, que sería el defecto grave, ni ninguna que cite una anterior a la de su propia ola.

La regla de **citar y no rehacer** se cumple, y se cumple explícitamente y no por omisión. Cada documento maestro de nivel superior abre con una tabla de decisiones heredadas: `Web` declara «las **cuatro** decisiones de los dos proyectos de código de nivel 0 que hereda sin reabrir», `Infrastructure` «las **cinco** decisiones de los niveles 0 y 1 que hereda sin reabrir», `Api` «las **siete** decisiones de los cuatro proyectos de código que ensambla y no reabre». Verifiqué las siete de `Api` contra las ADR citadas: las siete existen, tienen el identificador que se les atribuye y dicen lo que la fila dice que dicen.

**No encontré ninguna ADR que contradiga a otra de una capa inferior.** El caso donde una contradicción era más probable —la serialización— se resuelve al revés de como suele fallar: `Contracts ADR-01` §6 punto 4 acepta por escrito el trade-off de no imponer formato, y `Api ADR-02` lo cierra citando textualmente esa aceptación en su §1, sin tocar la decisión de `Contracts`. El otro candidato —quién abre la unidad de trabajo— tampoco se pisa: `Application ADR-05` fija el **alcance** («un caso de uso, una unidad de trabajo») y `Infrastructure ADR-02` fija el **mecanismo** («un archivo escritor único, una unidad de trabajo por operación»), y ninguno de los dos reasigna lo del otro.

### 2.2 Las dependencias declaradas

Los enlaces cruzados entre proyectos de código resuelven: el barrido de las referencias relativas de los 76 archivos —incluidas las **veintitantas** que cruzan de un proyecto de código a otro con `../../`— da **cero rotas**. Y resuelven a lo que dicen: verifiqué las que fundan decisiones, por ejemplo `Application ADR-02` → `Domain ADR-06-El-Dominio-No-Lee-El-Reloj-Ni-El-Conjunto.md`, `Api ADR-02` → `Contracts ADR-01`, `Infrastructure` §4 → `Application ADR-02`. Las tres apuntan al archivo que nombran y ese archivo dice lo que la cita le atribuye.

### 2.3 Los tres reasignados entre capas

**El criterio de comparación de correos, con su índice. Cerrado.** `Application` §11 `PA-03` lo derivó nombrando destinatario —«la categoría 05 de `GeometriaFactory-Infrastructure`, junto con el índice que la sostenga»— y `Infrastructure ADR-03` lo toma con ese mismo nombre y lo cierra: dos correos son el mismo ignorando mayúsculas y minúsculas y nada más, se conserva la forma escrita, se indexa la normalizada, y el índice único es `IX-01` de `Modelo-Datos-Logico.md` §3. Verifiqué que `IX-01` existe en el modelo lógico emitido. La ADR agrega una convención que cierra la puerta por la que este defecto vuelve: «ningún otro componente normaliza correos», con métrica «componentes que normalizan correos: exactamente **1**». **Sin hallazgo.**

**El formato de intercambio, que debe servir a los dos extremos. Cerrado, y para los dos.** La cadena es de tres tramos y los tres están escritos: `Contracts` §11 `PA-03` lo reasigna a las categorías 05 de `Api` y de `Web`; `Web` §11 `PA-03` declara que **no lo fija unilateralmente** —«no se puede decidir de un solo lado: los dos extremos tienen que coincidir o el contrato deja de ser el mismo»— y lo devuelve al productor declarando que lo adopta; `Api ADR-02` lo fija y declara «esta decisión obliga a los dos extremos». El fundamento de la devolución es correcto y es, además, el criterio que la guía §3.4 pide para un contrato inter-proyecto. **Sin hallazgo por el fondo**; hay un hallazgo P2 por el recuento de sus reglas, en `C-05-03`.

**El nombre del cuarto puerto. No cerrado, con fundamento, y sin reabrirse.** `Application ADR-02` resuelve la mitad decidible —el puerto existe, y su ausencia de nombre en el intake es omisión de nombre y no de alcance— y deja el identificador atado al punto de control de la etapa `a`, con la convención transitoria de nombrarlo en lenguaje de dominio. Las dos capas de arriba lo respetan citando la ADR por su nombre: `Infrastructure` §4 dice «fijarlo desde acá sería nombrar un tipo que este proyecto de código no declara y contradecir una decisión ya emitida», y `Api ADR-06` §7 dice «no se fija acá». Es un punto abierto **verdadero**: ninguna fuente lo resuelve, lo comprobé buscando el nombre del puerto en el intake y en las categorías 02 de `Application` y `Domain`. **Sin hallazgo.**

---

## 3. Afirmaciones sobre otras fuentes, verificadas abriendo la fuente

Muestreé **once** citas cruzadas de peso, todas las que fundan una decisión o una omisión de artefacto, y abrí la fuente en cada caso. **Nueve verifican exactamente. Dos son falsas**, y son los hallazgos `C-05-01` y `C-05-02`.

| Documento que cita | Qué afirma | Fuente | Resultado |
| --- | --- | --- | --- |
| `Api ADR-08` §1 | El intake declara «no hay versionado de rutas porque no hay clientes de terceros» (§17.5.P.3) | Intake §17.5.P.3 | **Verifica.** «Sin clientes de terceros, no hay versionado de rutas» |
| `Api ADR-07` §1 | §17.5.P.4 declara como responsabilidad propia aplicar las transformaciones al arrancar y tomar de configuración la ruta del almacén, que apunta a un volumen persistente | Intake §17.5.P.4 | **Verifica** palabra por palabra, con la sustitución léxica que la regla exige |
| `Api ADR-07` §1 | El intake declara arranque en frío por debajo de 30 segundos | Intake §17.5.P.10 | **Verifica.** «Arranque en frío: aplica migraciones y responde salud en menos de 30 segundos [ASUNCIÓN]» |
| `Api` §8 | «No hay NFR de disponibilidad… el intake declara "sin SLO"» | Intake §17.5.P.10 | **Verifica.** «Disponibilidad: sin SLO» |
| `Application ADR-05` §1 | §17.2.P.4 declara la persistencia «no aplica directamente» pero le asigna el alcance de la unidad de trabajo | Intake §17.2.P.4 | **Verifica.** «**No aplica directamente.** Declara el puerto de repositorio y el alcance de la unidad de trabajo» |
| `Application ADR-01` §6 | El intake declara como trade-off aceptado escribir a mano el mapeo entre entidades y tipos de transferencia (§17.2.P.12) | Intake §17.2.P.12 | **Verifica.** «Se acepta escribir a mano el mapeo entre entidades y DTOs» |
| `Contracts ADR-04` §6 | El intake declara las cero dependencias como quality gate bloqueante (§17.4.P.8) | Intake §17.4.P.8 | **Verifica.** «Quality gate bloqueante: compila sin advertencias y sin referencias hacia `GeometriaFactory.Domain`» |
| `Contracts` §5 | No hay etapa de `test`: el intake declara que no tiene pruebas propias (§17.4.P.6) | Intake §17.4.P.6 | **Verifica.** «No tiene pruebas propias… Stages: restore → build» |
| `Domain` §7 | El intake declara «sin observabilidad propia» en §17.1.P.10 | Intake §17.1.P.10 | **Verifica.** «Sin observabilidad propia: no registra ni instrumenta» |
| `Infrastructure` §10.5 y §11 `PA-08` | §17.3.P.4 dice «los siete escenarios de §20» y §17.3.P.6 declara la batería «con los escenarios E-1 a E-7» | Intake §17.3.P.4 y §17.3.P.6 | **FALSA.** Ver `C-05-01` |
| `Api` §11 `PA-06` | §16.1 describe la colección «con los escenarios E-1 a E-7 como cuerpo», y «ninguna se actualizó» | Intake §16.1 | **FALSA en su mitad de §16.1.** Ver `C-05-02` |

La tasa es alta y la calidad de las nueve que verifican es notable: no son paráfrasis, son transcripciones con la sustitución léxica que el framework impone sobre los nombres de producto comercial. **La falla no está en la disciplina de citar, está en las dos celdas donde la emisión citó de memoria en vez de abrir el archivo** — y en las dos, el archivo estaba en el mismo commit.

---

## 4. Lo que no se lee como texto corrido

Barrí con script las tres familias donde este corpus acumuló sus peores defectos.

**Tablas mal formadas: cero.** Recorrí las 76 archivos detectando cada bloque de tabla por su fila separadora y comparando el número de celdas de cada fila de datos contra el de su encabezado. **Cero filas discrepantes.** Es el primer barrido de este tipo en el corpus que da cero limpio.

**Enlaces relativos que no resuelven: cero.** Extraje cada destino de enlace markdown que no es ancla interna ni URL externa, lo normalicé contra el directorio del archivo que lo contiene y verifiqué existencia en disco. **Cero rotos** sobre el total, incluidas las travesías `../../` entre proyectos de código.

**Tablas de correspondencia incompletas: una encontrada, en la categoría 03.** Verifiqué las tablas de correspondencia de reglas e invariantes de los siete documentos maestros contando los identificadores distintos que aparecen como primera celda de fila:

| Proyecto de código | Filas `RN-XX` distintas | Filas `INV-XX` distintas |
| --- | --- | --- |
| `Domain` | **16**, `RN-01` a `RN-16` | **9**, `INV-01` a `INV-09` |
| `Application` | **16** | **9** |
| `Infrastructure` | **16** | **9** |
| `Contracts` | **16** | — (sin tabla; el proyecto de código no modela invariantes) |
| `Api` | **16** | **9** |
| `Web` | **16** | — (sin tabla, con motivo declarado) |
| `Visor` | — | — |

`Visor` no tiene tabla de reglas, y **lo declara en la celda en vez de dejar el hueco**: «RN aplicables: **Ninguna.** Un visualizador puro no tiene reglas de dominio: las decide el backend. Lo que tiene son condiciones de contrato, que no son reglas de negocio». Es la forma correcta de una ausencia. **Ninguna de las seis tablas de reglas tiene la fila de una regla faltante**, que era el defecto que tres auditorías anteriores no vieron: `RN-16` está en las seis.

La tabla incompleta que sí existe está en `GeometriaFactory-Api/03-UX-UI-DX/DX-Error-Messages.md` §2.1 y §6.2, y es el hallazgo `C-05-04`.

**Rangos de identificadores congelados: cero en texto normativo, dos en celdas de puntos abiertos.** Barrí `E-1 a E-7`, «siete escenarios», `RN-01 a RN-15`, «quince reglas», «ocho invariantes», «diecisiete códigos», «dieciocho códigos» y «dieciséis puntos de acceso» sobre los 76 archivos. **Ninguno aparece como afirmación propia de la categoría.** Las tres apariciones de `E-1 a E-7` y las dos de «siete escenarios» son **citas de la fuente**, no recuentos propios — y ahí está el problema: la fuente ya no dice eso. Es `C-05-01` y `C-05-02`.

Lo que sí quiero dejar registrado como mérito: `Infrastructure` §10.5 contiene el párrafo «Dos recuentos del intake que esta categoría no puede propagar, y por qué», donde la emisión **cuenta los encabezados de §20 en lugar de copiar el rango** y declara ocho. Esa es la conducta correcta, y es literalmente la que el intake 1.18 acredita en su control de cambios como origen de su propia corrección. El defecto es que el párrafo se quedó a mitad de camino: contó bien y describió mal la fuente que ya se había corregido.

---

## 5. Recuentos y conjuntos cerrados, contados por el auditor

Conté cada conjunto sobre el instrumento, sin leer la cifra declarada.

| Conjunto | Esperado | Contado | Cómo lo conté |
| --- | --- | --- | --- |
| Reglas de negocio | 16, `RN-01`…`RN-16` | **16** | Archivos de `Domain/02-Especificacion-Funcional/Reglas-De-Negocio/`, excluido `_legacy/` |
| Invariantes | 9 | **9**, `INV-01` a `INV-09` | Identificadores distintos en el intake, contrastados con §17.1.P.2 |
| Escenarios | 8, `E-1`…`E-8` | **8** | Encabezados `### §20.E-N` del intake §20 |
| Necesidades de negocio | 9 | **9**, `NB-00001` a `NB-00009` | Rangos declarados en 05, contrastados con `01-Necesidades-Negocio/` |
| Códigos de contrato vivos | 15 sobre 18 emitidos | **15** | Filas `CONTRATO_*` de la tabla de traducción de `Api/Contratos-REST.md` §5 |
| Puntos de acceso | 15 | **15** | Filas `A-XX` de `Api/Contratos-REST.md` §3: `A-01` a `A-16` **sin `A-04`**, retirado por `RN-16` |
| Códigos de respuesta | 10 | **10** | `400`, `401`, `403`, `404`, `409`, `500`, `503` de fallo, más `200`, `201`, `204` de éxito |
| Funciones de fachada | 6 | **6** | Filas numeradas 1 a 6 de `Visor/Contratos-Abstractions.md` §3, con los seis nombres que §17.7.P.3 declara |

**Los ocho cierran.** Y cierran también los recuentos internos que cada documento maestro declara sobre sí mismo, que conté restando encabezado y separador de cada tabla:

| Proyecto de código | NFR declarados / contados | Riesgos declarados / contados | Puntos abiertos declarados / contados | ADR en índice / archivos en `Adrs/` |
| --- | --- | --- | --- | --- |
| `Api` | 17 / **17** | 9 / **9** | 10 / **10** | 8 / **8** |
| `Application` | 9 / **9** | 6 / **6** | 6 / **6** | 6 / **6** |
| `Contracts` | 7 / **7** | 6 / **6** | 4 / **4** | 5 / **5** |
| `Domain` | 6 / **6** | 5 / **5** | 4 / **4** | 6 / **6** |
| `Infrastructure` | 14 / **14** | 8 / **8** | 11 / **11** | 7 / **7** |
| `Visor` | 8 / **8** | 6 / **6** | 5 / **5** | 6 / **6** |
| `Web` | 14 / **14** | 7 / **7** | 7 / **7** | 7 / **7** |

**Veintiocho recuentos declarados, veintiocho que cierran.** Es el resultado más limpio que este corpus dio en esta dimensión.

Un recuento que a primera vista parecía un rango recortado y **no lo es**: `Application` §10 declara «NB-00001 a NB-00007 y NB-00009, **ocho** de las **nueve**», y justifica en la misma celda que `NB-00008` no la toca ese proyecto de código porque su dolor es de acceso y de despliegue. Es una cobertura parcial declarada con motivo, no un rango congelado.

---

## 6. Las tres reglas de arquitectura

Transcribí las tres del intake §14 y las busqué en los siete proyectos de código. Aparecen **144 veces** en la categoría.

| Regla | Enunciado del intake §14 | Estado en la Fase C |
| --- | --- | --- |
| `RA-01` | «Ningún JavaScript del navegador invoca la API» | **Sostenida, y con el mecanismo escrito.** `Api/Contratos-REST.md` §1 declara las tres ausencias que se derivan —sin intercambio de origen cruzado, sin canal bidireccional, sin punto de acceso pensado para un navegador— y `Api` §9 la registra como riesgo con la mitigación de la ausencia declarada. `Web ADR-01` la sostiene desde el otro lado con el render en el servidor |
| `RA-02` | «El bundle del visor es un visualizador puro: sin configuración, sin red, sin conocimiento del sistema» | **Sostenida, con puerta bloqueante verificable.** `Visor ADR-03` fija «cero ocurrencias de las tres formas de petición de red en el código fuente **y en el bundle generado**», y declara explícitamente que la sexta función de la fachada **no la afloja**: el anfitrión pasa dos valores de verdad y el bundle no consulta la preferencia del sistema. Lo contrasté con `PRODUCT-MANIFEST` **1.2** §5, que declara lo mismo |
| `RA-03` | «Todo lo que el navegador deba obtener del backend pasa por el front; los mensajes de error nunca incluyen direcciones de servicios internos» | **Sostenida en las dos mitades.** `Api/Contratos-REST.md` §5 la nombra «acá es donde se puede violar hacia afuera» y la exige desde `Api ADR-04` §2 punto 6; `Web ADR-06` aísla el visor tras su fachada; `Visor` §10.3 declara que la cumple «por ignorancia, no por disciplina», y deja escrito el motivo para que no deje de ser cierto |

**Ninguna ADR de las 45 contradice ninguna de las tres.** Lo verifiqué por barrido inverso: busqué las decisiones que podrían habilitar una violación —configuración de origen cruzado, dirección de servicio en el navegador, red desde el bundle— y las tres aparecen únicamente como alternativas **descartadas** en tablas de §4, con `RA-01` o `RA-02` como motivo del descarte.

---

## 7. Forma y conformidad con la guía

Contrastado contra `Rules-Arquitectura-Tecnica.md` **3.1**.

| Criterio de la guía | Resultado |
| --- | --- |
| §4.3 — diez secciones obligatorias del ADR, en orden | **45 de 45.** Verificado por script: los encabezados `## 1.` a `## 10.` existen, están numerados sin salto y su título corresponde al de la sección que la guía manda |
| §4.1 — cabecera con `Versión`, `Estado`, `Fecha`, `Autor`, `Categoría` | **45 de 45** con `Categoría` declarada; **76 de 76** con `Versión: 1.0` y `Estado: Propuesto` |
| §4.2 — diez secciones del documento maestro y las cuatro vistas mínimas | **7 de 7.** Los siete tienen §1 a §10 en orden, más §11 Puntos abiertos y §12 Control de cambios como extensiones legítimas |
| §4.1 — tabla de contenido en documentos de más de tres secciones de primer nivel | **7 de 7** en los maestros. Los ADR quedan exceptuados por brevedad |
| §3.3 — cada ADR en archivo individual bajo `Adrs/` | **Cumple.** Cero ADR consolidada dentro de otro documento |
| §3.1 — slug en Título-Con-Guiones, sin sufijo de versión en el nombre | **Cumple** en los 45 |
| §6 — control de cambios con versión, fecha y descripción | **76 de 76** |
| §2.2 — mínimo de ADR por tipo D8 | **Cumple con holgura.** `library` exige 3: `Domain` 6, `Application` 6, `Contracts` 5, `Visor` 6. `rest-api` exige 5: `Api` **8**. `web-monolith` exige 5: `Web` **7**. `Infrastructure` **7** |
| §2.2 — modelo lógico donde el tipo D8 lo exige | **Omitido en `Api` y en `Web`, las dos veces declarado.** `Web` lo omite **contra el valor por defecto de la regla** y lo registra en `ADR-02`, diciéndolo con esas palabras; `Api` lo omite por delegación y apunta al modelo lógico emitido de `Infrastructure`. Las dos omisiones son declaradas y no incumplimientos silenciosos |
| §2.2 — contratos externos donde el tipo D8 los exige | **Cumple.** `Contratos-REST.md` para `Api`; `Contratos-Abstractions.md` para las cuatro bibliotecas |
| §2.1 y §4.8 — `Producto/Vista-Producto.md` para producto de más de un proyecto de código | **No emitida.** Ver `C-05-06` |

---

## 8. Hallazgos

### C-05-01 · P1 · Dos afirmaciones falsas sobre el intake, que sostienen un punto abierto ya cerrado

**Dónde.** `SDD/Docs/Proyectos/GeometriaFactory-Infrastructure/05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`, §10.5 (línea 354) y §11 fila `PA-08` (línea 367).

**Qué dice.** §10.5: «`PRODUCT-INTAKE` §17.3.P.4 dice "los siete escenarios de §20" y §17.3.P.6 declara la entrada de la batería como "los escenarios E-1 a E-7": los dos son anteriores a que `E-8` entrara al intake en su versión 1.7». Y `PA-08` eleva eso al Product Owner: «**Dos recuentos de escenarios congelados en la fuente** […] **corregir la fuente es del Product Owner sobre su propio documento**», sin fecha comprometida.

**Qué debería decir.** El intake **1.18**, que es la versión vigente, ya no dice ninguna de las dos cosas. §17.3.P.4 dice «ver los **ocho** escenarios de §20» y §17.3.P.6 dice «con los escenarios **E-1 a E-8** de la Parte D como entrada». El control de cambios del intake 1.18 lo declara y acredita el origen: «Los rangos de escenarios congelados en E-7. **Seis lugares** de la fuente —§16.1, §17.3.P.4, §17.3.P.6, §18 S-3, la nota de §20 y la lista de verificación de §23— seguían diciendo… **Lo levantó la Fase C de `GeometriaFactory-Infrastructure`**». Es decir: la corrección existe **porque este documento la pidió**, y el documento salió sin enterarse. El párrafo debería declarar el recuento cerrado y `PA-08` debería desaparecer de la tabla de puntos abiertos, o quedar como fila resuelta.

**Gravedad.** Es P1 y no P2 por tres razones acumuladas. Primera: es una **afirmación falsa sobre la fuente**, la familia de defecto por la que se rechazaron tandas anteriores de este producto. Segunda: es un **punto abierto falso** —el criterio que esta ronda tenía instrucción de tratar como hallazgo—, y no uno cualquiera: **asigna trabajo al Product Owner sobre algo que el Product Owner ya hizo**. Tercera: el intake corregido y el documento que lo contradice **viajan en el mismo commit `0a71935`**, de modo que el defecto nació muerto y ninguna lectura posterior del árbol puede reproducir la afirmación.

**Cómo lo verifiqué.** `grep -n "E-7" SDD/Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md` devuelve **cuatro** líneas y **ninguna** es un rango: son el ancla de la tabla de contenido de §20.E-7, el encabezado de ese escenario, la mención de E-1 y E-7 en la verificación de dibujo de §17.7, y dos filas de §21. Luego abrí §17.3.P.4 y §17.3.P.6 y leí sus enunciados vivos. Después confirmé con `git log --oneline -1 -S"1.18 | 2026-08-09"` que la fila 1.18 del control de cambios del intake entra en `0a71935`, el mismo commit que crea el archivo que la contradice.

### C-05-02 · P1 · Un punto abierto de `Api` cuyo fundamento es falso en su mitad

**Dónde.** `SDD/Docs/Proyectos/GeometriaFactory-Api/05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`, §11 fila `PA-06` (línea 369).

**Qué dice.** «El intake la declara en dos lugares que **no dicen lo mismo**: §16.1 la describe con "los escenarios **E-1 a E-7** como cuerpo" y §18 nombra "los cuerpos de E-2 y E-5". **Las dos son anteriores a que `E-8` entrara al intake y ninguna se actualizó**.»

**Qué debería decir.** §16.1 del intake **1.18** dice «Colección de peticiones HTTP reproducible con los escenarios **E-1 a E-8** como cuerpo». La afirmación entrecomillada no existe en la fuente, y la afirmación de que «ninguna se actualizó» es falsa: §16.1 es uno de los seis lugares que 1.18 corrigió. §18 S-2 **sí** dice «con los cuerpos de E-2 y E-5», eso verifica.

**Qué sobrevive.** La divergencia de alcance entre §16.1 y §18 S-2 **es real** —uno declara los ocho escenarios como cuerpo y el otro nombra dos— y el punto abierto tiene fundamento. Lo que está mal es la **evidencia** con que se lo funda: la fila atribuye la divergencia a un envejecimiento que ya no existe, en lugar de a la diferencia de alcance que sí existe. Con la cita corregida, `PA-06` sigue siendo legítimo y su enunciado cambia por completo.

**Gravedad.** P1 por ser una cita entrecomillada de una fuente que no la contiene, en la celda que funda un punto abierto elevado al Product Owner. Es media falla y no falla entera, pero la mitad falsa es la que va entre comillas.

**Cómo lo verifiqué.** Abrí la fila de `GeometriaFactory-Api` de la tabla de §16.1 del intake y la fila `S-2` de la tabla de §18. La primera contradice la cita; la segunda la confirma.

### C-05-03 · P2 · El formato de intercambio se cuenta seis reglas en la ADR y ocho en el contrato que lo materializa

**Dónde.** `GeometriaFactory-Api/05-Arquitectura-Tecnica/Adrs/ADR-02-Formato-De-Intercambio-Y-Su-Configuracion.md` §2, §4 y §10, contra `GeometriaFactory-Api/05-Arquitectura-Tecnica/Contratos-REST.md` §2.2 y §9, y contra el README de la sección línea 73.

**Qué dice.** La ADR: «**Seis reglas**, y las seis son verificables», seguidas de una lista numerada 1 a 6, más un párrafo aparte introducido como «**una regla que no es de formato** pero vive en la misma frontera: el texto original del alumno no se normaliza en el borde». Su §4 refuerza: «ninguna de las **seis** reglas depende de que los dos extremos se configuren igual por separado». Su §10: «con **seis** reglas». En cambio `Contratos-REST.md` §2.2 publica una tabla de **ocho** filas cuya primera columna se titula `Regla`, y su §9 declara «sus **ocho** reglas»; el README de la sección dice «**ocho** reglas elegidas para que ninguna dependa de que dos configuraciones coincidan».

**Qué debería decir.** Las ocho filas del contrato son las seis numeradas de la ADR **más** la notación —que la ADR declara en su oración de encabezado, no en la lista— **más** el texto original, que la ADR declara explícitamente como no siendo regla de formato. Los dos documentos cuentan bien conjuntos distintos, pero **los nombran igual**, y el README importa el número del contrato mientras cita el fundamento de la ADR («ninguna dependa de que dos configuraciones coincidan», que en la ADR se predica de las seis). Un lector que verifique el cierre del punto abierto reasignado no puede reconciliar los dos números sin abrir los dos archivos y hacer la resta. Corresponde unificar: o la ADR numera ocho, o el contrato distingue las seis de formato de las dos que no lo son.

**Gravedad.** P2. No hay contradicción de decisión —el contenido de las ocho filas y el de las seis más dos coincide punto por punto— pero es el mismo objeto con dos recuentos en la misma ola, en el artefacto que cierra un reasignado entre capas.

**Cómo lo verifiqué.** Conté las filas de datos de la tabla de §2.2: Notación, Nombres de campo, Conjuntos cerrados, Campos nulos, Números decimales, Lectura de la petición, Tamaño del cuerpo, Texto original del alumno = **8**. Conté los ítems numerados de la ADR §2 = **6**, y verifiqué que el ítem de notación aparece en su oración de encabezado y el de texto original en un párrafo posterior con la aclaración de que no es de formato.

### C-05-04 · P1 · Recuentos congelados en tres secciones de `DX-Error-Messages.md`, contra sus propias §2.3, §2.4 y §3

**Dónde.** `SDD/Docs/Proyectos/GeometriaFactory-Api/03-UX-UI-DX/DX-Error-Messages.md` **1.2**, §2.1, §3.6, §6.1 y §6.2. Es la categoría 03, insumo directo de la 05 de `Api` y su trazabilidad downstream declarada.

**Qué dice.** Cuatro bloques congelados en los números anteriores a la emisión 1.1:

1. **§2.1, la tabla de categorías.** Declara «Situación de la cuenta: **3**» y «Conflicto de estado: **6**», y cierra con «**Dieciocho entradas**, y el reparto por código de respuesta cierra: 3 + 2 + 3 + 1 + 2 + 6 + 1 = 18». El párrafo siguiente dice «**Seis de las dieciocho** entradas». Y la celda de la categoría de situación de la cuenta dice «**Estas tres** llevan motivo», con sólo dos entradas vivas.
2. **§3.6**, el encabezado de la familia de conflicto de estado: «**Seis entradas**, la categoría más poblada» sobre una tabla de **cinco** filas.
3. **§6.1, la tabla de recuento.** «Códigos del conjunto cerrado: **17**»; «Códigos con destino: **16**»; «Entradas del catálogo: **18** = 16 + 2»; y el cuadre «**16 + 1 = 17** códigos del conjunto cerrado, y **16 + 2 = 18** entradas».
4. **§6.2, la verificación mecánica.** Repite la tabla de siete categorías con 3 y 6, totaliza **18**, y las dos comprobaciones dicen «Los **17** códigos del conjunto cerrado… **16** tienen entrada» y «Las **18** entradas se recorrieron en sentido inverso: **16** citan un código».

**Contra qué.** Contra el propio documento. §2.3 dice «Del conjunto cerrado de **quince** códigos… son **catorce** códigos con destino sobre **quince**». §3 abre con «**Dieciséis entradas.** Las **catorce** primeras son los códigos del conjunto cerrado con destino». §6.3 dice «los **quince** puntos de acceso» y «la traducción completa de los **catorce** códigos». Y el control de cambios 1.1 explica el origen exacto: `RN-16` retiró `CONTRATO_CONTRASENA_NO_ESTABLECIDA` de §3.3 y `CONTRATO_RESETEO_NO_APLICABLE_A_CUENTA_SIN_CONTRASENA` de §3.6, que es precisamente el 3→2 y el 6→5 que las tablas no reflejan. **§3 se actualizó; §2.1 y §6 no.**

**Los números verdaderos, contados fila por fila sobre §3.**

| Categoría | Sección | Filas reales | Lo que declara §2.1 y §6.2 |
| --- | --- | --- | --- |
| Entrada inválida | §3.1 | **3** (2 con código + 1 sin código) | 3 ✓ |
| Credencial no admitida | §3.2 | **2** (1 con código + 1 sin código) | 2 ✓ |
| Situación de la cuenta | §3.3 | **2** | **3** ✗ |
| Facultad | §3.4 | **1** | 1 ✓ |
| Recurso no visible | §3.5 | **2** | 2 ✓ |
| Conflicto de estado | §3.6 | **5** | **6** ✗ |
| No clasificado | §3.7 | **1** | 1 ✓ |
| **Total** | | **16** | **18** ✗ |

De donde:

- **Entradas del catálogo: 16**, no 18. El reparto verdadero es **3 + 2 + 2 + 1 + 2 + 5 + 1 = 16**.
- **Entradas que citan un código del contrato: 14**, no 16. Las otras **2** son las respuestas sin código de §2.2, y ese 2 sí está bien.
- **Códigos del conjunto cerrado: 15**, no 17.
- **Códigos con destino en esta superficie: 14**, no 16. **Sin destino, declarado: 1.** El cuadre correcto es **14 + 1 = 15** y **14 + 2 = 16**.
- La celda «Estas **tres** llevan motivo» de §2.1 debe decir **dos**; el «**Seis** de las dieciocho entradas» debe decir **cinco de las dieciséis**; el encabezado de §3.6 debe decir **cinco entradas**.
- **«Diez códigos de respuesta distintos» de §6.2 es correcto** y lo verifiqué aparte: `400`, `401`, `403`, `404`, `409`, `500`, `503` más `200`, `201`, `204`.

**Gravedad.** P1. Es una tabla a la que le falta la fila de una regla entera —el retiro de `RN-16`— en dos lugares distintos del mismo documento, y §6.2 se titula «**verificación mecánica de cobertura**» y se ofrece explícitamente «para que una revisión posterior la pueda repetir sin rehacerla»: es una verificación que certifica un número falso, que es peor que no tenerla. Además el documento ya pasó por una corrección puntual en 1.2 que arregló **una** oración de §2.3 y no recontó las tablas, que es el patrón exacto que esta ronda tenía que buscar.

**Cómo lo verifiqué.** Conté las filas de datos de las siete tablas de §3.1 a §3.7 sobre el archivo, excluyendo encabezado y separador, y contrasté cada categoría contra su fila en §2.1 y en §6.2. Después contrasté el conjunto cerrado contra la fuente hermana que el propio documento nombra, `Api/02-Especificacion-Funcional/Definicion-Superficie-HTTP.md` §6, citada en el control de cambios 1.2 como «Quince códigos: catorce con destino en esta superficie y uno sin él».

### C-05-05 · P2 · `Api` cuenta quince códigos de contrato y `Contratos-REST.md` los publica, pero el insumo 03 que cita sigue diciendo dieciocho

**Dónde.** La categoría 05 de `Api` está **correcta**: `Contratos-REST.md` §5 publica **quince** filas y las declara «catorce con destino y una sin él», y lo verifiqué contando las filas `CONTRATO_*`. El defecto es de **coherencia de cadena**: `DX-Error-Messages.md`, que la trazabilidad downstream de 03 declara como insumo de esta 05, sigue afirmando en §6 que el conjunto cerrado tiene diecisiete y el catálogo dieciocho.

**Qué debería pasar.** Con `C-05-04` corregido, esto se cierra solo. Lo registro por separado porque un lector que baje de 03 a 05 encuentra dos números y **el que manda es el de 05**, que es el correcto: conviene que la corrección de 03 cite explícitamente la tabla de traducción de `Contratos-REST.md` §5 como su cuadre.

**Gravedad.** P2, derivado.

**Cómo lo verifiqué.** `grep -c "^| \`CONTRATO_" Contratos-REST.md` = **15**; recuento de filas `A-XX` = **15**, con `A-01` a `A-16` y `A-04` ausente por el retiro que declara el control de cambios 1.1 de `DX-Error-Messages.md`.

### C-05-06 · P3 · No hay `Producto/Vista-Producto.md`, y ningún artefacto la declara pendiente

**Dónde.** `SDD/Docs/Producto/` no existe.

**Qué dice la guía.** `Rules-Arquitectura-Tecnica.md` §2.1 declara `Vista-Producto.md` **obligatoria para productos con más de un proyecto de código**, §4.8 le fija ocho secciones, §6 la incluye entre los criterios de aceptación de nivel producto, y §1.2 y §8 precisan que se produce **una sola vez, al cierre del bucle de proyectos de código**. Ese bucle acaba de cerrarse: los siete están emitidos.

**Qué debería decir.** O bien la vista se emite, o bien algún artefacto de la categoría declara que queda para una cuarta ola. Hoy no la nombra ninguno de los 76 archivos: busqué «Vista-Producto», «vista de producto» y «Producto/» en el árbol de la categoría y las únicas apariciones de «nivel producto» son las que califican a `RA-01`, `RA-02` y `RA-03`. El grafo de dependencias, el mapa de proyectos de código y los contratos inter-proyecto están hoy repartidos en las tablas de decisiones heredadas de los siete maestros, que es exactamente la duplicación que §4.8 pide evitar con un documento que referencie en lugar de reescribir.

**Gravedad.** P3, y no más, por dos motivos: el contenido no falta —está disperso pero está—, y la guía sitúa la emisión al cierre del bucle, de modo que una cuarta ola inmediata la cubriría. Lo que sí es defecto hoy es el **silencio**: la ausencia no está declarada en ningún lado, y este corpus tiene la disciplina de declarar cada omisión con su motivo en todas las demás.

**Cómo lo verifiqué.** `find SDD/Docs/Producto -type f` no devuelve nada; barrido de los tres términos sobre los 76 archivos de la categoría.

---

## 9. Lo que no reporto, y lo que no pude verificar

**Lo que no reporto, deliberadamente.**

- **Los cuarenta y siete puntos abiertos declarados.** Conté las filas `PA-XX` de los siete documentos maestros: `Api` 10, `Infrastructure` 11, `Web` 7, `Application` 6, `Visor` 5, `Contracts` 4 y `Domain` 4, **47** en total. Muestreé los de mayor peso —el nombre del cuarto puerto, la vigencia del acceso firmado, la herramienta de cálculo de versión, la zona horaria del campo de momento— y **los cuatro son abiertos verdaderos**: fui a la fuente y ninguna los resuelve. Un punto abierto correctamente declarado no es hallazgo. Los dos que sí reporto —`PA-08` de `Infrastructure` y `PA-06` de `Api`— lo son por ser **falsos**, no por estar abiertos.
- **Ninguna polisemia.** El corpus usa «contrato» en tres contextos disjuntos —contrato de fachada del visor, ensamblado de contratos, contrato de superficie HTTP—, «puerto» en dos —puerto de la arquitectura hexagonal, puerto publicado del transporte— y «estado» en varios. En todos los casos que revisé el contexto es disjunto y el referente queda fijado por la sección. Reportarlo sería un defecto de este informe.
- **La omisión del modelo lógico en `Api` y en `Web`.** Las dos están declaradas con motivo, y la de `Web` va más lejos de lo que la guía exige: declara que **no es la omisión que la regla admite** para su tipo D8 y la registra en `ADR-02`. Es conducta ejemplar, no hallazgo.
- **Las citas del intake por versiones distintas entre olas** (1.15, 1.16, 1.17). Es la cadena correcta: cada ola cita la versión vigente al empezar.

**Lo que no pude verificar, y declaro no verificado.**

- **Si hay una cuarta ola planificada** que emita `Producto/Vista-Producto.md`. No encontré documento de plan de fases en el repositorio auditado. `C-05-06` queda condicionado a eso.
- **La corrección semántica de las decisiones técnicas** —si el orden fijo de las cuatro comprobaciones es el correcto, si la derivación de clave anclada es adecuada, si el estilo de tres capas del visor es el mejor—. Esta ronda audita coherencia, veracidad de citas y forma; no dictamina sobre la calidad de la ingeniería.
- **Los objetivos numéricos de los 75 NFR.** Verifiqué que **todos** declaran valor numérico y mecanismo de medición, como pide §6 de la guía. No verifiqué que cada valor sea alcanzable ni que su mecanismo sea el correcto.
- **Las tres fuentes originales del intake**, que viven en otro repositorio bajo `PROMPTs/` y quedan fuera de alcance. Donde un documento de 05 cita `RT §x` o `RF §y` a través del intake, verifiqué la cita **contra el intake**, no contra la fuente última.

---

## 10. Dictamen

## **RECHAZADO**

**Fundamento.** El rechazo se apoya en los tres P1, y de ellos en dos que son el mismo defecto de fondo por el que este producto ya rechazó tandas anteriores: **un documento afirma que una fuente dice algo que la fuente no dice**, y en los dos casos la afirmación falsa **sostiene un punto abierto** que eleva trabajo al Product Owner.

Lo que vuelve inaceptable a `C-05-01` no es la magnitud del error de recuento —es de uno— sino su topología: el intake **1.18** corrigió los seis lugares congelados **porque esta misma Fase C se lo pidió**, y el commit `0a71935` publica en el mismo acto la fuente corregida y el documento que la declara sin corregir. El punto abierto `PA-08` nace cerrado. Un corpus cuyo valor entero descansa en que sus citas cruzadas sean literales no puede publicar una cita entrecomillada de un archivo que viaja en el mismo commit diciendo otra cosa. `C-05-02` es la misma falla, media, en `Api`.

`C-05-04` no es de esta categoría pero es el insumo directo de la 05 de `Api`, estaba localizado antes de esta ronda, y sigue sin corregir después de una emisión 1.2 que tocó una oración de §2.3 y no recontó las tablas de §2.1 ni de §6. Su §6.2 se llama «verificación mecánica de cobertura» y certifica dieciocho entradas sobre un catálogo de dieciséis: es una verificación que blinda el número equivocado, exactamente el mismo mecanismo que el hallazgo `C-04` de la ronda anterior había levantado en el párrafo vecino.

**Qué hace falta para levantar el rechazo.** Cuatro correcciones acotadas, ninguna de contenido arquitectónico:

1. `Infrastructure` §10.5 y `PA-08`: reescribir contra el intake **1.18** y cerrar el punto abierto (`C-05-01`).
2. `Api` `PA-06`: corregir la cita de §16.1 y refundar el punto abierto sobre la divergencia de alcance, que sí subsiste (`C-05-02`).
3. `DX-Error-Messages.md` §2.1, §3.6, §6.1 y §6.2: llevar los cuatro bloques a **16 entradas / 14 con código / 15 en el conjunto cerrado / 14 con destino**, con el reparto 3-2-2-1-2-5-1 (`C-05-04`, y con él `C-05-05`).
4. Unificar el recuento de las reglas del formato de intercambio entre `Api ADR-02`, `Contratos-REST.md` §2.2 y el README (`C-05-03`).

`C-05-06` no bloquea: puede resolverse emitiendo la vista de producto o declarando su diferimiento.

**Ninguna decisión de arquitectura debe reabrirse.** Las 45 ADR son sustantivamente correctas y coherentes entre sí; lo que hay que corregir son cuatro celdas y cuatro tablas.

---

## 11. Estado general de la arquitectura del producto

**La arquitectura de este producto está bien puesta, y la Fase C es su mejor emisión hasta acá.** El estilo elegido resiste la prueba más dura que se le puede hacer, que es leer los siete documentos en orden topológico y buscar dónde una capa de arriba deshace lo que una de abajo decidió: **no ocurre ni una vez en 45 decisiones**. Las tres reglas de arquitectura del producto —el navegador que nunca alcanza la API, el visor que es un visualizador puro, el front como único camino al navegador— no quedaron como declaración de intenciones en el intake: cada una tiene hoy un mecanismo escrito que la sostiene, una ADR que la nombra como motivo de descarte de la alternativa que la rompería, y al menos una métrica de puerta bloqueante. `RA-02` es el mejor ejemplo: se verifica contando ocurrencias de tres formas de petición de red en el bundle **generado**, no en el código fuente, que es la única medición que no se puede engañar.

Lo que más me llama la atención, y conviene que quede escrito porque es una propiedad frágil, es la **disciplina de la frontera**. Los tres objetos que cruzaron de capa lo hicieron nombrando destinatario, y llegaron. El formato de intercambio recorrió tres proyectos de código —`Contracts` lo suelta, `Web` se niega a decidirlo de un solo lado y lo devuelve al productor, `Api` lo cierra para los dos extremos— y en cada tramo el documento que lo pasa **cita textualmente el trade-off que el anterior aceptó por escrito**. Eso es lo que hace que una arquitectura por capas sea auditable y no un conjunto de siete documentos que se ignoran. Y el cuarto puerto, que es el caso incómodo, se resolvió del modo difícil: `Application` decidió **la mitad decidible** —el puerto existe— y declaró la otra mitad atada a un punto de control, en lugar de inventar un nombre para cerrar la fila. Las dos capas de arriba lo respetaron, con la frase correcta: «fijarlo desde acá sería contradecir una decisión ya emitida».

La calidad mecánica también dio un salto que merece registrarse: cero tablas mal formadas, cero enlaces rotos, veintiocho recuentos propios que cierran sobre veintiocho, 45 ADR con sus diez secciones en orden. Este corpus llegaba a esta fase con un historial de tablas rotas y recuentos congelados, y **la Fase C dejó de congelar sus propios números**.

Y sin embargo el defecto de fondo sobrevivió, migrado. **Lo que envejece ya no son los recuentos de la categoría, es lo que la categoría cree recordar de la fuente.** Los dos P1 de citas falsas están los dos en celdas de tablas de puntos abiertos, escritas en el registro de «esto lo tiene que arreglar otro», que es el género de texto que nadie relee porque no afirma nada del producto. El intake acumuló ya tres conjuntos que envejecieron del mismo modo —las funciones de la fachada, los invariantes de `Domain`, los escenarios— y su propio control de cambios 1.18 diagnostica el mecanismo con una frase que vale para toda la cadena: **la fuente enumera sus conjuntos en más lugares de los que actualiza cuando crecen**. La Fase C aprendió a contar sus conjuntos; falta que aprenda a **releer la fuente el día que la emite**, y no el día que empezó a escribir. La corrección que este informe pide es de cuatro celdas; el hábito que hace falta instalar es de una línea: antes de entrecomillar una fuente, abrirla.

---

## 12. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Auditoría inicial de la Fase C —categoría 05 Arquitectura Técnica— sobre los siete proyectos de código, 76 archivos y 45 ADR emitidos en tres olas por `ee73c99`, `e176845` y `0a71935`, contra `PRODUCT-INTAKE` 1.18, `PRODUCT-MANIFEST` 1.2 y `Rules-Arquitectura-Tecnica.md` 3.1. Verifica coherencia inter-capa y los tres reasignados, once citas cruzadas de peso abriendo la fuente, la forma mecánica de las 76 tablas y de los 45 ADR por script, y ocho conjuntos cerrados más veintiocho recuentos declarados por recuento propio. Levanta seis hallazgos: tres P1, dos P2 y un P3. **Dictamen: RECHAZADO**, por dos afirmaciones falsas sobre el intake que sostienen puntos abiertos ya cerrados y por los recuentos congelados de `DX-Error-Messages.md`, cuyos números verdaderos se dan contados fila por fila. Ninguna decisión de arquitectura debe reabrirse. |
