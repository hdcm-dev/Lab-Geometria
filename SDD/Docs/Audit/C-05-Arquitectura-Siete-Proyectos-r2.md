# Auditoría de la Fase C · categoría 05 Arquitectura Técnica de los siete proyectos de código · ronda 2

| Campo | Valor |
| --- | --- |
| Producto | Fábrica de Geometría |
| Rama auditada | `sdd/fase-c-arquitectura` |
| Objeto de la ronda | Dictaminar si se levanta el **RECHAZO** de [`C-05-Arquitectura-Siete-Proyectos-r1.md`](C-05-Arquitectura-Siete-Proyectos-r1.md) **1.0** —seis hallazgos, tres P1, dos P2, un P3, ninguno P0— corregido por el commit `802731e` |
| Alcance auditado | Los **76** archivos de `Proyectos/*/05-Arquitectura-Tecnica/` más `Producto/Vista-Producto.md` **1.0**, nuevo = **77** en el árbol de la categoría; más `GeometriaFactory-Api/03-UX-UI-DX/DX-Error-Messages.md` **1.3**, insumo directo de la 05 de `Api` = **78** verificados. Además, los tres archivos de las categorías 02 que la corrección tocó por herencia de la cita falsa |
| Fuentes de contraste | `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.18**, `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` **1.2**, y `IA.SDD/SDD/Devs/Rules/Rules-Arquitectura-Tecnica.md` (repositorio de origen, **sólo lectura**) |
| Criterio de la ronda | **El instrumento, no la conclusión.** Ningún cierre se acepta por estar declarado en un control de cambios: se abre el archivo corregido, se abre la fuente que cita, y se recuenta sobre las filas. Los recuentos se cuentan de nuevo, no se leen |
| Fuera de alcance | `_legacy/`; las tres fuentes originales del intake, que viven en otro repositorio bajo `PROMPTs/`; las categorías 04 y 06 a 11, no emitidas |
| Auditor | Auditor independiente, sin participación en la emisión ni en la corrección |
| Fecha | 2026-08-10 |

---

## Tabla de contenido

- [1. Resumen ejecutivo](#1-resumen-ejecutivo)
- [2. Los seis hallazgos de la ronda 1, uno por uno](#2-los-seis-hallazgos-de-la-ronda-1-uno-por-uno)
- [3. Lo que la corrección reportó y la ronda 1 no vio](#3-lo-que-la-correccion-reporto-y-la-ronda-1-no-vio)
- [4. Auditoría de `Producto/Vista-Producto.md` 1.0](#4-auditoria-de-productovista-producto-md-10)
- [5. Regresiones](#5-regresiones)
- [6. Los ocho conjuntos cerrados, contados de nuevo](#6-los-ocho-conjuntos-cerrados-contados-de-nuevo)
- [7. Hallazgos nuevos](#7-hallazgos-nuevos)
- [8. Lo que no reporto, y lo que no pude verificar](#8-lo-que-no-pude-verificar)
- [9. Dictamen](#9-dictamen)
- [10. Estado general de la arquitectura del producto](#10-estado-general-de-la-arquitectura-del-producto)
- [11. Control de cambios](#11-control-de-cambios)

---

## 1. Resumen ejecutivo

**Los seis hallazgos de la ronda 1 están cerrados, y los seis los verifiqué sobre el instrumento y no sobre la declaración.** Recontó bien quien recontó: el reparto 3-2-2-1-2-5-1 de `DX-Error-Messages.md` lo conté yo fila por fila sobre las siete tablas de §3 y da **16**, con **14** entradas que citan código, conjunto cerrado de **15** y **14** con destino, y los cuatro bloques congelados —§2.1, §3.6, §6.1 y §6.2— quedaron los cuatro alineados con ese recuento. Las dos citas falsas del intake ya no existen en el corpus vivo: barrí `E-1 a E-7`, `siete escenarios` y `E-1 a E-6` sobre todo `SDD/Docs` y da **cero ocurrencias** fuera de los informes de auditoría. El desacuerdo seis contra ocho está resuelto del modo correcto —nombrando los conjuntos— y el cuadre **6 + 1 + 1 = 8** está publicado, con las mismas palabras, en las **tres** fuentes que discrepaban.

**Los dos pendientes que nacían cerrados se conservaron como filas resueltas, y el reparto que declaran cierra contando las filas.** `Infrastructure` §11 declara «once filas: diez abiertas y una resuelta» y tiene **11** filas `PA-`; `Api` §11 declara «diez filas: nueve abiertas y una resuelta» y tiene **10**. La decisión de no retirarlos es la correcta y está fundada: `PA-08` está citado desde §10.5 y desde el README de la sección —lo comprobé—, y retirarlo habría dejado un hueco de numeración sin declarar.

**La ronda 1 subcontó, y la corrección tiene razón en los tres puntos que reporta.** Los contratos de superficie son **seis** y no cuatro: los conté con `find`, hay cinco `Contratos-Abstractions.md` —una por biblioteca, `Visor` incluido— más `Contratos-REST.md`. La cita falsa de §16.1 **nace en la categoría 02 de `Api`** y la 05 la heredó: el texto de la fila es el mismo en `Especificacion-Funcional.md` y en `CU-12`, fuera de los 76 archivos que r1 alcanzó. Y había un cuarto residuo, en la trazabilidad de cabecera de `Domain/02/Definicion-Modelo-De-Dominio.md`. Los cuatro están corregidos.

**`Vista-Producto.md` se emitió en 1.0 y no se difirió, y es un documento verdadero.** Tiene las ocho secciones que la guía manda, y lo que afirma resiste el contraste: el grafo coincide bloque por bloque con `PRODUCT-MANIFEST` §3, los seis contratos existen y cada uno corresponde a una arista real, el «once de los doce» casos de uso que cruzan la frontera lo dice `Contratos-REST.md` §1 con esas palabras, y la frase que atribuye a la guía —«si los contratos son pocos»— está literal en la guía. No inventa nada y no reabre nada.

**Levanto dos hallazgos nuevos, los dos P3, ninguno bloqueante.** Uno es un recuento que no cierra dentro de la vista de producto —declara siete aristas de compilación y enumera ocho—, y nace aguas arriba, en un desacuerdo del propio manifiesto entre su §2 y su §3. El otro son tres menciones a stacks concretos en la categoría 05, contra un criterio de aceptación explícito de la guía, una de ellas nueva en la vista de producto y dos preexistentes que la ronda 1 tampoco vio.

**Dictamen: APROBADO.**

---

## 2. Los seis hallazgos de la ronda 1, uno por uno

| Hallazgo r1 | Sev. | Estado verificado | Cómo lo comprobé |
| --- | --- | --- | --- |
| `C-05-01` — dos citas falsas del intake en `Infrastructure` §10.5 y `PA-08`, que sostenían un punto abierto ya cerrado | P1 | **CERRADO** | Abrí el intake vivo: §17.3.P.4 dice «ver los **ocho** escenarios de §20» y §17.3.P.6 «con los escenarios **E-1 a E-8** de la Parte D como entrada». Abrí `Infrastructure` §10.5 **1.1**: transcribe esos dos textos, acredita el control de cambios 1.18 con los seis lugares corregidos y declara el recuento cerrado en las dos puntas. `PA-08` pasó a fila **RESUELTO** con desenlace y fecha (`PRODUCT-INTAKE` 1.18, 2026-08-09). Barrido de `E-1 a E-7` y `siete escenarios` sobre todo `SDD/Docs`: **cero** fuera de `Audit/` |
| `C-05-02` — cita entrecomillada de §16.1 inexistente en la fuente, en `Api` `PA-06` | P1 | **CERRADO, y refundado** | `PRODUCT-INTAKE` §16.1 vivo dice «Colección de peticiones HTTP reproducible con los escenarios **E-1 a E-8** como cuerpo»; §18 **S-2** dice «con los cuerpos de **E-2 y E-5**». `Api` `PA-06` **1.1** transcribe los dos textos, declara que **los dos están al día** y refunda el punto abierto sobre la **divergencia de alcance** —ocho contra dos—, que efectivamente subsiste en la fuente. Lo que se pide al Product Owner pasó de «actualizar» a «declarar cuál alcance rige». El punto abierto es **verdadero**: la fuente no declara cuál manda |
| `C-05-03` — seis reglas en `ADR-02` contra ocho en el contrato y en el README | P2 | **CERRADO en las tres fuentes** | Conté los ítems numerados de `ADR-02` §2: **6**. Conté las filas de datos de `Contratos-REST.md` §2.2: **8** (Notación, Nombres de campo, Conjuntos cerrados, Campos nulos, Números decimales, Lectura de la petición, Tamaño del cuerpo, Texto original). Las tres fuentes publican ahora el mismo cuadre con la misma partición: `ADR-02` §2 «**6 + 1 + 1 = 8**», `Contratos-REST.md` §2.2 «Ocho filas, y no son ocho reglas de formato: son seis, más dos que no lo son… **6 + 1 + 1 = 8**», README §7 «ocho filas, de las cuales las **seis reglas de formato**… El cuadre **6 + 1 + 1 = 8** está en `ADR-02` §2 y en `Contratos-REST.md` §2.2». `Contratos-REST.md` §9 pasó a decir «las ocho **filas** de §2.2». El predicado «ninguna depende de que dos configuraciones coincidan» queda predicado de las **seis** en las tres |
| `C-05-04` — cuatro bloques de recuento congelados en `DX-Error-Messages.md` | P1 | **CERRADO** | **Recuento propio**, contando filas de datos de las siete tablas de §3.1 a §3.7 con script: 3, 2, 2, 1, 2, 5, 1 = **16**. Contra el documento **1.3**: §2.1 declara ese mismo reparto y «**Dieciséis entradas**… 3 + 2 + 2 + 1 + 2 + 5 + 1 = 16», con «Estas **dos** llevan motivo» y «**Cinco** de las dieciséis»; §3.6 dice «**Cinco entradas**, la categoría más poblada»; §6.1 da **15 / 14 / 1 / 2 / 16 = 14 + 2** con el cuadre «**14 + 1 = 15**» y «**14 + 2 = 16**»; §6.2 repone la tabla con 2 y 5, totaliza **16** y sus comprobaciones dan 15/14/1 y 16/14/2. **Los cuatro bloques cierran, y cierran contra mi recuento y no contra el suyo.** Los «diez códigos de respuesta» de §6.2 los verifiqué aparte contando las filas de `Contratos-REST.md` §4: **10** |
| `C-05-05` — la 05 correcta y el insumo 03 diciendo dieciocho | P2 | **CERRADO** | Se cierra por arrastre de `C-05-04`, y además con lo que r1 pedía: §6.1 de `DX-Error-Messages.md` **cita explícitamente** la tabla de traducción de `../05-Arquitectura-Tecnica/Contratos-REST.md` §5 como su cuadre. Conté esa tabla: **15** filas `CONTRATO_*`. `Api` `PA-10` pasó a fila **RESUELTO** con desenlace y fecha |
| `C-05-06` — no hay `Producto/Vista-Producto.md` y nadie la declara pendiente | P3 | **CERRADO por emisión** | `SDD/Docs/Producto/Vista-Producto.md` **1.0** existe, y es el único archivo de esa carpeta —no quedó `Producto/Adrs/` huérfano, cuya ausencia el documento declara explícitamente en §5. Auditada aparte en §4 de este informe contra `Rules-Arquitectura-Tecnica.md` §4.8 y sus criterios de aceptación de nivel producto |

### 2.1 Sobre los dos pendientes que nacían cerrados

La corrección **no los retiró**: los pasó a filas resueltas con desenlace y fecha, y declaró el reparto. Verifiqué las dos cosas que ese modo de cierre obliga a verificar.

| Documento | Filas `PA-` contadas | Reparto declarado | ¿Cierra? | Identificador citado desde otra sección |
| --- | --- | --- | --- | --- |
| `Infrastructure/05` §11 | **11** | «Once filas: **diez abiertas y una resuelta**» | **Sí** | **Sí.** `PA-08` aparece en §10.5 del maestro y en la fila del README de la sección, que además pasó a decir «once puntos abiertos —diez abiertos y `PA-08` resuelto—» |
| `Api/05` §11 | **10** | «Diez filas: **nueve abiertas** —`PA-01` a `PA-09`— **y una resuelta, `PA-10`**» | **Sí** | `PA-10` no se cita desde otra sección; el motivo declarado —el hueco de numeración— basta y es el mismo criterio |

**Ningún identificador fantasma.** Para los siete proyectos de código, el mayor `PA-XX` referenciado en cualquier archivo de la sección coincide exactamente con el número de filas de la tabla: `Api` 10/10, `Application` 6/6, `Contracts` 4/4, `Domain` 4/4, `Infrastructure` 11/11, `Visor` 5/5, `Web` 7/7. **Cuarenta y siete filas, cuarenta y cinco abiertas y dos resueltas.**

---

## 3. Lo que la corrección reportó y la ronda 1 no vio

Los tres puntos son **ciertos**. La ronda 1 subcontó en los tres.

**(a) La cita falsa no nace en la categoría 05 sino en la 02 de `Api`, y se heredó.** Verificado con `git show 802731e`: la fila «El alcance de la colección de peticiones» de `Api/02-Especificacion-Funcional/Especificacion-Funcional.md` contenía, antes de la corrección, exactamente las mismas dos afirmaciones falsas que r1 levantó en la 05 —«§16.1 la describe con "los escenarios **E-1 a E-7** como cuerpo"» y «ninguna de las dos se actualizó»—, y `CU-12` la propagaba. **La 05 no inventó la cita: la heredó de su propia 02.** El diagnóstico de la corrección es más preciso que el de r1: lo que envejece no es el recuento propio sino la cita cruzada, y viaja aguas abajo sin que nadie reabra el eslabón donde se escribió. Los tres archivos están corregidos y refundados sobre la divergencia de alcance.

**(b) Había un residuo más, en `Domain/02/Definicion-Modelo-De-Dominio.md`.** Su trazabilidad de cabecera citaba «§20 (escenarios **E-1 a E-7**)». Corregido a «§20 (los **ocho** escenarios `E-1` a `E-8`)» en la versión 1.9, que además declara que cierra un residuo del hallazgo `N-4` de `F26-Propagacion-r2.md` que aquel barrido no había alcanzado.

**(c) La ronda 1 subcontó los contratos de superficie: son seis, no cuatro.** Contados sobre disco, excluido `_legacy/`:

| # | Archivo | Proyecto de código productor |
| --- | --- | --- |
| 1 | `Contratos-Abstractions.md` | `GeometriaFactory-Domain` |
| 2 | `Contratos-Abstractions.md` | `GeometriaFactory-Application` |
| 3 | `Contratos-Abstractions.md` | `GeometriaFactory-Contracts` |
| 4 | `Contratos-Abstractions.md` | `GeometriaFactory-Infrastructure` |
| 5 | `Contratos-Abstractions.md` | `GeometriaFactory-Visor` |
| 6 | `Contratos-REST.md` | `GeometriaFactory-Api` |

**Cinco bibliotecas con `Contratos-Abstractions.md` más el `Contratos-REST.md` del `rest-api` = 6.** La cifra «4 contratos de superficie» del volumen contado de r1 es errónea; la conclusión que r1 sacaba de ella —que la guía se cumple— no cambia, y de hecho se refuerza. **`Vista-Producto.md` §4 publica el número correcto.**

**Barrido final del defecto de fondo.** `grep -rn "E-1 a E-7\|siete escenarios\|E-1 a E-6"` sobre todo `SDD/Docs`, excluido `_legacy/` y excluido `Audit/`: **cero ocurrencias**. El barrido complementario de los otros conjuntos que este producto vio envejecer —«cinco funciones» como enunciado del contrato de fachada, «ocho invariantes», «quince reglas», «RN-01 a RN-15», «diecisiete códigos», «dieciocho entradas», «dieciséis puntos de acceso»— da **cero afirmaciones vivas**: todas las apariciones que quedan son celdas de control de cambios que describen el estado de una versión anterior, que es su función, o el enunciado correcto de que `INSTANCIA_DESCONOCIDA` se presenta en **cinco** de las seis funciones, que es una verdad distinta y no un recuento congelado.

---

## 4. Auditoría de `Producto/Vista-Producto.md` 1.0

### 4.1 Forma, contra `Rules-Arquitectura-Tecnica.md` §4.8 y §6

| Criterio | Resultado |
| --- | --- |
| §4.8 — las **ocho** secciones obligatorias, en orden | **Cumple.** §1 Objetivo y alcance, §2 Mapa de proyectos de código, §3 Grafo de dependencias, §4 Contratos inter-proyecto, §5 Decisiones de nivel producto, §6 Cross-cutting compartido, §7 Riesgos de integración, §8 Trazabilidad. Más §9 Control de cambios como extensión legítima, igual que en los siete maestros |
| §4.8 — «referencia, no reescribe» | **Cumple, y lo declara en su §1.** Verifiqué que **no toma ninguna decisión**: cada celda de las siete tablas apunta al documento que decide. Ninguna de las 45 ADR se reabre |
| §4.1 — cabecera con `Versión`, `Estado`, `Fecha`, `Autor`, tabla de contenido | **Cumple.** 1.0 / Propuesto / 2026-08-10 / AG-05, con `Nivel: Producto` en lugar de `Proyecto de código`. `Categoría` se omite, como la guía admite para los artefactos que no son ADR |
| §6 — mapa refleja el manifiesto sin divergencias | **Cumple.** Contrasté las siete filas contra `PRODUCT-MANIFEST` §2 celda por celda: mismos `Nombre-Proyecto-Codigo`, mismas `Identidad-Codigo` —incluida la minúscula con guiones de `geometriafactory-visor`—, mismos tipos D8 (5 `library`, 1 `web-monolith`, 1 `rest-api`), `redistribuible` false en los siete |
| §6 — grafo acíclico y coincidente con el manifiesto | **Cumple en el bloque.** El bloque `text` de §3 es **idéntico línea por línea** al de `PRODUCT-MANIFEST` §3, y el orden topológico de cuatro niveles también. Ver el hallazgo `N-1`, que es sobre el **recuento** de aristas y no sobre el grafo |
| §6 — cada contrato corresponde a una arista y referencia al productor | **Cumple.** Las seis filas de §4 apuntan al `Contratos-*` del **productor** y las seis rutas resuelven |
| §6 — decisiones de nivel producto en `Producto/Adrs/` | **Cumple por ausencia declarada.** `Producto/` contiene únicamente `Vista-Producto.md`; §5 declara que no hay `Producto/Adrs/` «en lugar de dejar el hueco», y resuelve las tres candidatas que la guía nombra —estilo de composición, versionado inter-proyecto, comunicación entre proyectos— señalando dónde se decide cada una |
| §4.8 — `Contratos-Inter-Proyecto.md` | **Omitido con motivo declarado**, y el motivo cita la guía correctamente. La frase que entrecomilla —«si los contratos son pocos»— **está literal** en la guía, línea 81 de su §2.2 |

### 4.2 Veracidad de lo que afirma, contra los documentos que cita

No basta con que la vista sea plausible: cada afirmación de peso la contrasté contra la fuente.

| Afirmación de la vista | Fuente | Resultado |
| --- | --- | --- |
| Las cinco líneas del grafo de dependencias de compilación | `PRODUCT-MANIFEST` §3 | **Verifica.** Bloque idéntico |
| `Web → Api` es de tiempo de ejecución y por eso no introduce ciclo | `PRODUCT-MANIFEST` §3, nota | **Verifica**, con el mismo fundamento y sin nombrar el stack que la fuente sí nombra |
| «Ningún proyecto de código es `redistribuible`, de modo que el prefijo de paquetes redistribuibles queda sin uso» | `PRODUCT-MANIFEST` §1.2 | **Verifica.** La fila del prefijo de paquetes redistribuibles está rotulada `Aplicada` con la observación «Sin uso: ningún proyecto de código es `redistribuible`» |
| Seis contratos de superficie: cinco `Contratos-Abstractions.md` más `Contratos-REST.md` | Disco | **Verifica.** Contados: 6 |
| §4 fila 5: quince puntos de acceso, diez códigos de respuesta, traducción de quince códigos | `Contratos-REST.md` §3, §4, §5 | **Verifica.** 15 filas `A-XX`, 10 filas de código de respuesta, 15 filas `CONTRATO_*` |
| §4 fila 6: las **seis** funciones de la fachada del bundle, con `tiene_extensibilidad` == true | `Visor/Contratos-Abstractions.md` §3 y `PRODUCT-MANIFEST` §5 | **Verifica.** Seis filas numeradas; el flag es true sólo en `Visor` |
| §6: `tiene_observabilidad_critica` sólo en `Api`, y por eso no hay correlación distribuida | `PRODUCT-MANIFEST` §5 | **Verifica.** Es el único true de esa columna en las siete filas |
| §7 `RI-03`: decisiones heredadas «`Web` cuatro, `Infrastructure` cinco, `Api` siete» | Los tres maestros, §2.2 | **Verifica.** Son los tres números que los tres documentos declaran, ya contrastados en r1 §2.1 |
| §8: «once de los doce» casos de uso cruzan la frontera, «según declara ese contrato en su §1» | `Contratos-REST.md` §1 | **Verifica palabra por palabra.** «Los casos de uso que se materializan a través de este contrato son **once de los doce**… El doceavo —la colección de peticiones reproducible— **ejercita este contrato en lugar de exponerlo**» |
| §5: las 45 ADR son todas internas a un proyecto de código | Disco | **Verifica.** 45 archivos bajo los siete `Adrs/`, reparto 8-6-5-6-7-6-7, y ninguna carpeta `Producto/Adrs/` |
| §4: el formato de intercambio con «las seis reglas de formato más la notación y la prohibición de normalizar el texto original» | `Api ADR-02` §2 y `Contratos-REST.md` §2.2 | **Verifica**, y usa la partición corregida en `C-05-03` en lugar de importar un número suelto |

**Ninguna afirmación falsa.** La vista pasa la prueba que las dos rondas anteriores usaron como criterio: no dice de ninguna fuente algo que la fuente no diga.

---

## 5. Regresiones

Las correcciones son donde nacen los defectos. Barrí las cinco familias sobre los **77** archivos del árbol de la categoría —los 76 de r1 más `Vista-Producto.md`— y sobre `DX-Error-Messages.md`.

| Familia | Resultado | Cómo lo barrí |
| --- | --- | --- |
| **Recuentos que dejaron de cerrar** | **Uno**, y es el hallazgo `N-1` | Recontadas las filas de todas las tablas cuyo total el texto declara: las 7 tablas de `DX` §3, la de §6.2, las 8 filas de `Contratos-REST.md` §2.2, las 15 de §3, las 10 de §4, las 15 de §5, las 11 y 10 filas `PA-` de los dos maestros tocados, las 6 filas de contratos, las 6 de riesgos y las 7 de cross-cutting de la vista |
| **Identificadores fantasma** | **Cero** | Para los siete proyectos de código, el mayor `PA-XX` referenciado en cualquier archivo de la sección coincide con el número de filas de su tabla. Ninguna referencia a un `PA` retirado, ninguna a un `ADR` inexistente |
| **Citas cruzadas nuevas sin verificar** | **Cero falsas** | Abrí las once citas que la corrección introduce o reescribe: las dos de §17.3 y la de §16.1 y §18 S-2 del intake, el control de cambios 1.18 con sus seis lugares, la frase de la guía sobre contratos pocos, y las siete de la tabla de veracidad de §4.2 de este informe. **Las once verifican** |
| **Filas de tabla discordantes** | **Cero sobre 77 archivos** | Script: detección de tabla por su fila separadora, comparación del número de celdas de cada fila de datos contra el de su encabezado |
| **Enlaces relativos rotos** | **Cero sobre 77 archivos** | Script: cada destino markdown que no es ancla ni URL, normalizado contra el directorio contenedor y verificado en disco, incluidas las travesías `../../` entre proyectos de código y las nueve nuevas de `Vista-Producto.md` hacia `../Proyectos/` |

La corrección declaró **cero** en las dos últimas familias sobre 78 archivos. **Confirmo cero**, sobre los 77 del árbol de la categoría; el archivo 78 es `DX-Error-Messages.md`, que barrí aparte y también da cero. La diferencia de una unidad es de encuadre, no de resultado.

---

## 6. Los ocho conjuntos cerrados, contados de nuevo

Los conté yo, sobre el instrumento, sin leer la cifra declarada.

| Conjunto | Esperado | Contado | Cómo lo conté |
| --- | --- | --- | --- |
| Reglas de negocio | 16 | **16** | Archivos `RN-*` de `Domain/02-Especificacion-Funcional/Reglas-De-Negocio/`, excluido `_legacy/` |
| Invariantes | 9 | **9**, `INV-01` a `INV-09` | Identificadores distintos en el intake |
| Escenarios | 8 | **8** | `grep -c "^### §20.E-"` sobre el intake |
| Necesidades de negocio | 9 | **9**, `NB-00001` a `NB-00009` | Identificadores distintos en `01-Necesidades-Negocio/`, excluido `_legacy/` |
| Códigos de contrato vivos | 15 sobre 18 emitidos | **15** | Filas `CONTRATO_*` de `Contratos-REST.md` §5. Los 18 emitidos y los 3 retirados los declara §5 del mismo documento |
| Puntos de acceso | 15 | **15** | Filas de la tabla de `Contratos-REST.md` §3. Los identificadores llegan a `A-16`; **`A-04` no está en la tabla** y su retiro por `RN-16` se declara en el párrafo siguiente, con el criterio de que no se recicla |
| Códigos de respuesta | 10 | **10** | Filas de la tabla de `Contratos-REST.md` §4, y contraste con la enumeración de `DX-Error-Messages.md` §6.2: `400`, `401`, `403`, `404`, `409`, `500`, `503`, más `200`, `201`, `204` |
| Funciones de fachada | 6 | **6** | Filas numeradas 1 a 6 de `Visor/Contratos-Abstractions.md` §3 |

**Los ocho cierran.**

---

## 7. Hallazgos nuevos

### N-1 · P3 · La vista de producto declara siete aristas de compilación y enumera ocho

**Dónde.** `SDD/Docs/Producto/Vista-Producto.md` **1.0**, §3 (línea 69), §4 fila 2 y §8 (filas 2 y 6, y la línea de cierre 159).

**Qué dice.** §3 cierra su bloque de grafo con «**Siete aristas de compilación, y todas resuelven**», y §8 cierra con «**Cobertura del grafo: 6 contratos sobre 7 aristas de compilación** más 1 de tiempo de ejecución».

**Qué encontré al contar.** Las aristas de compilación **distintas** que la propia vista enumera en sus §4 y §8 son **ocho**: `Domain → Application`, `Domain → Infrastructure`, `Application → Infrastructure`, **`Application → Api`**, `Infrastructure → Api`, `Contracts → Api`, `Contracts → Web` y `Visor → Web`. La octava —`Application → Api`— aparece nombrada dos veces, en la fila 2 de §4 («`Application` → `Infrastructure`, `Api`») y en la fila 2 de §8, donde además se la atribuye a la columna «Arista del manifiesto **§3**». **El bloque `text` de `PRODUCT-MANIFEST` §3 no la dibuja**: sólo dibuja siete, y trata la relación de `Application` con `Api` como transitiva a través de `Infrastructure`.

**Y sin embargo la arista existe.** `PRODUCT-MANIFEST` §2 declara las dependencias de `GeometriaFactory-Api` como «`GeometriaFactory-Application`, `GeometriaFactory-Infrastructure`, `GeometriaFactory-Contracts`» —tres—, y `Api/05` §2 lo confirma por su lado: «Este proyecto de código depende por compilación de tres». De modo que las aristas **declaradas directas** son ocho y el DAG de §3 dibuja siete, y el §4 del manifiesto sella «las **siete** aristas resuelven».

**Dónde nace, y por qué es P3 y no más.** **El desacuerdo nace aguas arriba**, entre el §2, el §3 y el §4 del manifiesto, que es un artefacto de nivel intake y está fuera del alcance de la Fase C; la vista de producto lo hereda al copiar el bloque, que es exactamente la conducta que la guía pide. Nada de arquitectura cambia: el grafo **es acíclico** en las dos lecturas, el orden topológico es el mismo, y la cobertura de contratos no se altera porque `Application → Api` la materializa el mismo `Contratos-Abstractions.md` que ya está en la tabla. Lo que hay es **un total que no cuadra con la enumeración que lo acompaña**, en el documento que acaba de emitirse como fuente única del grafo del producto, y una atribución de una arista al §3 del manifiesto cuando quien la declara es su §2.

**Qué corresponde.** No reabrir nada: declarar el reparto, del mismo modo en que `C-05-03` se cerró nombrando conjuntos. Basta con que §3 diga «siete aristas dibujadas en el DAG del manifiesto §3, y **ocho** dependencias directas declaradas en su §2, porque `Application → Api` es directa además de transitiva», y que §8 cite §2 y no §3 en la fila que la nombra. Y conviene elevarlo al Product Owner sobre el manifiesto, que es donde el desacuerdo vive.

**Cómo lo verifiqué.** Extraje las aristas del bloque `text` de la vista y del manifiesto y las comparé línea por línea: idénticas, siete. Enumeré las aristas distintas nombradas en las columnas «Arista» de §4 y §8 de la vista: ocho. Leí la columna `Dependencias` de las siete filas de `PRODUCT-MANIFEST` §2: la fila de `Api` lista tres, y el §4 del mismo manifiesto declara «las siete aristas resuelven». Contrasté con `Api/05` §2, que dice tres.

### N-2 · P3 · Tres menciones a stacks concretos en la categoría 05, contra un criterio de aceptación de la guía

**Dónde.**

1. `SDD/Docs/Producto/Vista-Producto.md` §2, línea 55: «`GeometriaFactory-Visor` es el único proyecto de código fuera del **ecosistema .NET**». **Es nueva**, emitida en este commit.
2. `GeometriaFactory-Infrastructure/05-Arquitectura-Tecnica/Adrs/ADR-04-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md` §1: «declara «**PBKDF2 o Argon2**» y deja la elección a la regla de anclaje». **Preexistente**, y la ronda 1 tampoco la vio.
3. El mismo par en `GeometriaFactory-Infrastructure/05` §11, fila `PA-03`: «El intake declara «PBKDF2 o Argon2» y **no elige**». **Preexistente.**

**Qué dice la guía.** `Rules-Arquitectura-Tecnica.md`, criterio de aceptación de §6: «**No hay menciones a stacks concretos, productos comerciales ni protocolos específicos del dominio fuente**», y lo repite como restricción del subagente en §8. Es la regla de sustitución léxica que el resto del corpus cumple con notable disciplina.

**Qué lo atenúa, y por qué igual lo levanto.** Las dos de `Infrastructure` son **citas entrecomilladas de la fuente** dentro de una decisión que precisamente **no elige** entre las dos funciones, de modo que sustituirlas costaría legibilidad y la ADR resuelve lo que sí le corresponde —qué se guarda junto al valor derivado—. La de la vista es descriptiva y sostiene un apartamiento de nombre real. Aun así son las **únicas tres** de los 77 archivos: el barrido de `SQLite`, `EF Core`, `Blazor`, `MudBlazor`, `webpack`, `npm`, `Node.js`, `TypeScript` y `JWT` sobre la categoría da **cero**, y la propia vista de producto elude los tres nombres de herramienta que el manifiesto sí usa en el párrafo equivalente. La disciplina existe y estas tres son su excepción, no su ausencia.

**Gravedad.** P3, y no bloqueante. Se resuelve con tres sustituciones léxicas —«el ecosistema de la plataforma principal», «las dos funciones de derivación de clave que la fuente admite»— sin tocar ninguna decisión.

**Cómo lo verifiqué.** Barrido insensible a mayúsculas de trece nombres de stack sobre los 77 archivos del árbol de la categoría, y lectura del criterio de aceptación de la guía en el repositorio de origen.

---

## 8. Lo que no reporto, y lo que no pude verificar

**Lo que no reporto, deliberadamente.**

- **Los cuarenta y cinco puntos abiertos que siguen abiertos.** Muestreé los de mayor peso —el nombre del cuarto puerto, la vigencia del acceso firmado, la función de derivación de clave, los dos huecos del conjunto cerrado, el alcance de la colección de peticiones refundado— y **ninguno es falso**: fui a la fuente y ninguna los resuelve. Un punto abierto correctamente declarado no es hallazgo, y esta fase declara más de cuarenta legítimamente. **No queda ningún punto abierto falso en el corpus**: los dos que r1 levantó son los dos que pasaron a resueltos.
- **Ninguna polisemia.** «Contrato» en tres contextos, «puerto» en dos, «regla» en dos —regla de negocio y regla de formato—, «estado» en varios. En todos los casos el contexto es disjunto y el referente queda fijado por la sección; en el caso de «regla», que era el que `C-05-03` puso en tensión, la corrección lo resolvió **acuñando el nombre del conjunto** en las tres fuentes, que es más de lo que el criterio exige. Reportarlo sería un defecto de este informe.
- **La asimetría entre los dos README.** El de `Infrastructure` incorporó el reparto abiertas/resueltas a su fila del maestro y el de `Api` no. Los dos números son correctos; no hay recuento que no cierre.
- **La cabecera de `DX-Error-Messages.md` 1.3, que sigue citando el intake 1.14.** La corrección fue de recuentos internos y no de fuente; las cuatro versiones del control de cambios declaran contra qué se corrigió cada una.

**Lo que no pude verificar, y declaro no verificado.**

- **Si el desacuerdo del manifiesto entre siete y ocho aristas debe resolverse hacia siete o hacia ocho.** Es una decisión sobre un artefacto de nivel intake, fuera del alcance de esta categoría. `N-1` señala el síntoma, no dictamina el número.
- **La corrección semántica de las decisiones técnicas.** Esta ronda audita coherencia, veracidad de citas y forma, igual que la anterior; no dictamina sobre la calidad de la ingeniería ni sobre si los 75 objetivos numéricos de NFR son alcanzables.
- **Las tres fuentes originales del intake**, que viven en otro repositorio bajo `PROMPTs/`. Donde un documento cita `RT §x` o `RF §y` a través del intake, verifiqué **contra el intake**.
- **Las categorías 04 y 06 a 11**, no emitidas, y por lo tanto el efecto aguas abajo de la vista de producto sobre ellas.

---

## 9. Dictamen

## **APROBADO**

**Fundamento.** Los seis hallazgos de la ronda 1 están cerrados, y los seis los verifiqué **sobre el instrumento**: no acepté ningún control de cambios como prueba de su propio cierre. Los tres P1 —los dos de citas falsas y el de los recuentos congelados— eran el motivo del rechazo, y los tres desaparecieron de la forma correcta. El recuento de `DX-Error-Messages.md` lo conté yo, fila por fila sobre las siete tablas de §3, y **da 16 con el reparto 3-2-2-1-2-5-1**, con 14 entradas con código, conjunto cerrado de 15 y 14 con destino; los cuatro bloques que estaban congelados dicen hoy exactamente eso, y §6.2 —la verificación que antes blindaba un número falso— cierra contra el recuento verdadero y cita como cuadre la tabla de traducción de `Contratos-REST.md` §5, que es lo que `C-05-05` pedía. Las dos citas falsas del intake no existen en ningún archivo vivo del corpus: el barrido da cero.

**Lo que eleva esta corrección por encima del mínimo.** No arregló la ocurrencia, arregló el eslabón. La ronda 1 localizó la cita falsa en la categoría 05 y la corrección demostró que **nace en la 02 de `Api` y la 05 la hereda**, la persiguió hasta `CU-12` y encontró de paso un cuarto residuo en `Domain/02` que un barrido anterior no había alcanzado. También corrigió el recuento de la propia ronda 1: los contratos de superficie son seis y no cuatro, y lo verifiqué. Un corpus que audita su auditoría con más precisión que la auditoría es un corpus que dejó de necesitar que le señalen las cosas dos veces.

**Y no arregló de más.** Los dos pendientes que nacían cerrados no se retiraron: pasaron a filas resueltas con desenlace y fecha, y el reparto que declaran cierra contando las filas. `PA-06` de `Api` no desapareció: se **refundó** sobre la divergencia de alcance que efectivamente subsiste entre §16.1 y §18 S-2 del intake, y sigue siendo un punto abierto verdadero. `C-05-03` no se cerró igualando números sino **nombrando los conjuntos**, con el cuadre 6+1+1=8 publicado en las tres fuentes con las mismas palabras. Y `C-05-06` se cerró **emitiendo** la vista de producto en lugar de declarar su diferimiento, que era la salida barata: el bucle de proyectos de código está cerrado, el insumo existía, y lo que se emitió es verdadero contra cada documento que cita.

**Los dos hallazgos nuevos no bloquean.** `N-1` es un total que no cuadra con su propia enumeración, en un documento nuevo, y el desacuerdo nace aguas arriba en el manifiesto; nada de arquitectura depende de si son siete u ocho, porque el grafo es acíclico en las dos lecturas y la cobertura de contratos no cambia. `N-2` son tres sustituciones léxicas pendientes contra un criterio de la guía, dos de ellas preexistentes. Los dos se resuelven sin reabrir ninguna decisión, y corresponde tomarlos en la próxima intervención sobre los archivos afectados, no en una tanda propia.

**Ninguna decisión de arquitectura debe reabrirse.** Las 45 ADR siguen siendo sustantivamente correctas y coherentes entre sí, y ninguna de las dos rondas encontró una sola contradicción entre capas.

---

## 10. Estado general de la arquitectura del producto

**La arquitectura de este producto está completa y, desde este commit, tiene por primera vez un lugar donde se la ve entera.** Eso es lo que cambió: hasta ayer el grafo, el mapa, los contratos que cruzan fronteras y los riesgos de integración estaban repartidos en las tablas de decisiones heredadas de siete documentos maestros, cada uno mirando la frontera desde un solo lado. `Vista-Producto.md` los junta sin reescribirlos —indexa y remite, que es exactamente lo que la guía pide— y al hacerlo produce dos cosas que el corpus no tenía: la **lista de las seis fronteras del producto** con su contrato y su productor, y la **lista de los seis riesgos que sólo existen entre proyectos de código**, que es un objeto distinto de la suma de los riesgos internos. `RI-01` es el mejor ejemplo de por qué hacía falta: «los dos extremos se configuran distinto sin romper ninguna compilación» es el único modo de falla del contrato que el versionado por compilación compartida **no** atrapa, y no era el riesgo de ningún proyecto de código en particular; era el riesgo de la arista.

**La disciplina de la frontera, que r1 destacó como la propiedad frágil de este corpus, sobrevivió a la corrección intacta.** Ninguno de los once documentos tocados decidió por otro. `Api ADR-02` no se llevó las dos filas que no eran suyas: las nombró como lo que son y publicó la resta. `Infrastructure` §10.5 no borró el párrafo incómodo: lo reescribió declarando que **el recuento está cerrado en las dos puntas**, y dejó acreditado que la corrección del intake ocurrió porque esta categoría la levantó. La vista de producto no aprovechó su posición de nivel superior para arbitrar nada: declara que **no hay decisiones de nivel producto** y muestra dónde se decide cada una de las tres candidatas, en el productor. Un documento situado por encima de siete que resiste la tentación de decidir es una señal más fuerte que cualquier recuento que cierre.

**El defecto de fondo cambió de naturaleza, y ahora sí.** En la ronda 1 el diagnóstico era que la categoría había aprendido a contar sus conjuntos pero no a releer la fuente el día que emite. La corrección instaló esa conducta y fue más lejos: encontró que la cita falsa no nacía donde se la había levantado, y subió por la cadena hasta el eslabón donde se había escrito por primera vez. Los dos hallazgos que quedan son de otra clase y de otra magnitud —un total que no cuadra con su enumeración, tres nombres de herramienta que había que sustituir—, y uno de los dos ni siquiera nace en esta fase: nace en un desacuerdo del manifiesto entre su §2 y su §3 que arrastra desde la derivación y que ninguna auditoría había mirado, porque hasta ahora nadie había tenido que enumerar las aristas de a una. Es el efecto secundario más útil de emitir una vista de producto: **un documento que junta lo que estaba repartido hace visible el desacuerdo que la dispersión escondía.** El corpus está en condiciones de seguir a las categorías 06 a 11 sin arrastrar deuda de coherencia.

---

## 11. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Auditoría de ronda 2 de la Fase C —categoría 05 Arquitectura Técnica— sobre la corrección del commit `802731e`, contra los seis hallazgos de [`C-05-Arquitectura-Siete-Proyectos-r1.md`](C-05-Arquitectura-Siete-Proyectos-r1.md) 1.0. Verifica los seis sobre el instrumento: recuenta `DX-Error-Messages.md` fila por fila —16 entradas, reparto 3-2-2-1-2-5-1, 14 con código, conjunto cerrado de 15 con 14 con destino—, contrasta las citas del intake 1.18 abriendo la fuente, comprueba el cuadre 6+1+1=8 en las tres fuentes que discrepaban, y audita `Producto/Vista-Producto.md` 1.0 contra las ocho secciones de §4.8 de la guía y contra la veracidad de once afirmaciones sobre los documentos que cita. Confirma los tres subcontajes que la corrección reporta de la ronda 1, incluido que los contratos de superficie son **seis** y no cuatro. Barre cinco familias de regresión sobre 77 archivos: cero enlaces rotos, cero filas de tabla discordantes, cero identificadores fantasma, cero citas cruzadas nuevas falsas. Recuenta los ocho conjuntos cerrados y los ocho cierran. Levanta **dos hallazgos nuevos, los dos P3 y ninguno bloqueante**: el recuento de aristas de compilación de la vista de producto, que declara siete y enumera ocho por un desacuerdo que nace en el manifiesto, y tres menciones a stacks concretos contra un criterio de aceptación de la guía. **Dictamen: APROBADO.** Ninguna decisión de arquitectura debe reabrirse. |
