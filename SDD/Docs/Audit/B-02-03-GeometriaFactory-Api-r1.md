# Auditoría B · 02-03 · GeometriaFactory-Api · ronda 1

| Campo | Valor |
| --- | --- |
| Fase | B — Especificación por proyecto de código |
| Producto | Fábrica de Geometría |
| Proyecto de código | GeometriaFactory-Api (`GeometriaFactory.Api`, `tipo_proyecto_codigo` = `rest-api`, **proyecto de código principal**) |
| Alcance auditado | Categoría **02-Especificacion-Funcional** (16 artefactos vivos: `Especificacion-Funcional.md`, `Definicion-Superficie-HTTP.md`, `Glosario-Funcional.md`, `README.md` y `CU-01` a `CU-12`) y categoría **03-UX-UI-DX** (5 artefactos vivos: `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md`, `DX-Error-Messages.md`, `Glosario-UX.md`, `README.md`). Total: **21 artefactos vivos**, en su estado actual sobre la rama `sdd/cierre-de-huecos` |
| Guías normativas | `Rules-Especificacion-Funcional.md` **4.0** y `Rules-UX-UI-DX.md`, del repositorio de origen `IA.SDD` (sólo lectura). Se auditó **contra ellas**, no contra la práctica de los proyectos de código hermanos |
| Fuentes contrastadas | `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.26** (§4.1, §14, §16.1, §17.5 íntegro, §18, §20, §21), `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` **1.3** (§1, §5), y las categorías 02 y 03 de `GeometriaFactory-Contracts` y `GeometriaFactory-Application`. `GeometriaFactory-Domain` e `-Infrastructure` se consultaron sólo para verificar recuentos y citas |
| Fuera de alcance | Categoría 04 (omitida por gating, `usa_llm` == false): su ausencia **no es hallazgo**. Las categorías 05 a 11 de este proyecto de código no se auditan acá; se abrieron únicamente para verificar que un enlace resuelva y que un recuento citado exista |
| Auditor | Arquitecto de Soluciones + QA Senior, invocado desde cero, sin participación en la generación ni en la corrección de ninguno de los 21 artefactos |
| Fecha | 2026-08-11 |
| Ronda | 1 |
| Informes anteriores de este proyecto de código y estas categorías | **Ninguno.** Es la primera vez que estas dos categorías reciben el control de su propia fase |

---

## 0. Declaración de emisión tardía y qué implica para el alcance

**Esta auditoría se emite tarde, después de las fases C, D, E, F, F26, G y H.** Los otros seis proyectos de código del producto recibieron su informe `B-02-03-*` antes de que su cadena aguas abajo se emitiera; éste, que es el **proyecto de código principal**, no lo recibió nunca. Su contenido fue revisado transversalmente en `Coherencia-Corpus-r1.md` y `-r2.md` —de ahí salieron los cierres `C-02`, `C-04` y `C-05` que hoy figuran en los controles de cambios— y en `C-05-Arquitectura-Siete-Proyectos-r1.md`, pero ninguno de esos informes tenía por objeto la conformidad de estas dos categorías con su guía. **La ausencia de este informe es, ella misma, un hueco de proceso**, y el hueco tuvo consecuencia observable: los hallazgos de §7 que están abiertos hoy son en su mayoría del tipo que una ronda 1 en tiempo habría levantado antes de que 05 a 11 se construyeran encima.

Cuatro consecuencias que acotan lo que este informe puede y no puede decir:

1. **No se audita la emisión, se audita el estado actual.** Los 21 artefactos llegan con correcciones de fases posteriores ya absorbidas. Lo que aquellas fases cerraron bien **no se reabre**, y lo que cerraron mal se levanta acá con la referencia al cierre que lo dejó incompleto. Un lector que busque «lo que estaba mal en la emisión 1.0» no lo va a encontrar acá: se perdió la ventana para observarlo.
2. **Toda corrección que salga de acá es una corrección con consumidores ya emitidos.** `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas`, `09-Devops`, `10-Examples` y `11-Documentacion` de este proyecto de código ya existen y citan estos documentos. Una corrección de recuento acá **no se agota en el archivo corregido**: obliga a la misma verificación de propagación que la Fase F26 hizo para el intake. Este informe declara, hallazgo por hallazgo, si el consumidor aguas abajo ya tiene el número correcto o el equivocado.
3. **No se juzgan decisiones ratificadas más tarde.** El recorte en doce casos de uso, la emisión del documento de concepto central, la omisión del modelo de datos y las omisiones de la variante UX/UI atravesaron C, E y H sin ser objetadas. Se verifica que estén **declaradas con motivo**, que es lo que la guía exige, y no se discute la decisión.
4. **Los puntos abiertos se verifican contra la fuente de hoy, no contra la de la emisión.** Un punto abierto que el intake resolvió entre 1.13 y 1.26 sería hallazgo. Se recorrieron los once y **ninguno lo es** (§6).

---

## Tabla de contenido

- [1. Resumen ejecutivo](#1-resumen-ejecutivo)
- [2. Conformidad con la guía de cada categoría](#2-conformidad-con-la-guía-de-cada-categoría)
  - [2.1 Categoría 02 contra `Rules-Especificacion-Funcional.md` §6](#21-categoría-02-contra-rules-especificacion-funcional-6)
  - [2.2 Categoría 03 contra `Rules-UX-UI-DX.md` §6](#22-categoría-03-contra-rules-ux-ui-dx-6)
  - [2.3 Apartamientos declarados](#23-apartamientos-declarados)
- [3. Recuentos verificados por el auditor](#3-recuentos-verificados-por-el-auditor)
- [4. Verificación de citas contra la fuente original](#4-verificación-de-citas-contra-la-fuente-original)
- [5. Las tres reglas de arquitectura](#5-las-tres-reglas-de-arquitectura)
- [6. Los once puntos abiertos, uno por uno](#6-los-once-puntos-abiertos-uno-por-uno)
- [7. Hallazgos](#7-hallazgos)
  - [7.1 P0](#71-p0)
  - [7.2 P1](#72-p1)
  - [7.3 P2](#73-p2)
  - [7.4 P3](#74-p3)
- [8. Lo que esta auditoría verificó y no reporta](#8-lo-que-esta-auditoría-verificó-y-no-reporta)
- [9. Lo no verificado](#9-lo-no-verificado)
- [10. Dictamen](#10-dictamen)
- [11. Control de cambios](#11-control-de-cambios)

---

## 1. Resumen ejecutivo

**La estructura está bien y los números no.** Las dos categorías cumplen la guía en todo lo que es forma y completitud: los doce casos de uso llevan las once secciones obligatorias del §4.2 sin excepción, ninguno baja de siete criterios de aceptación con valores concretos —la guía pide tres—, los cuatro artefactos DX obligatorios o recomendados para `rest-api` están emitidos con sus secciones, los dos glosarios tienen tabla no vacía y declaran sus polisemias con evidencia de colisión, **ninguna fila de tabla tiene un número de celdas distinto del de su encabezado en los 21 archivos**, y **los 112 enlaces relativos a archivo resuelven, sin una sola excepción**. Los apartamientos —el modelo de datos, las reglas de negocio, la sección opcional §17, los seis artefactos de la variante UX/UI y el portal— están **todos declarados con motivo y con el flag del manifiesto que los habilita**, que verifiqué uno por uno contra `PRODUCT-MANIFEST` 1.3 §5.

Lo que falla es lo otro, y es exactamente el defecto de fondo del producto: **afirmaciones falsas sobre fuentes vivas, sostenidas por recuentos que se corrigieron en el documento origen y no en los que lo citan.** El retiro de `A-04` y el achicamiento del conjunto cerrado de códigos de diecisiete a quince —`PRODUCT-INTAKE` 1.13, absorbido en las emisiones 1.1 de nueve de estos documentos— dejó un rastro de números viejos que las auditorías de coherencia levantaron **sólo donde miraron**. `DX-Error-Messages.md` fue corregido dos veces (1.2 por `C-04`, 1.3 por `C-05-04`) y hoy es internamente impecable; **los cinco documentos que lo citan siguen describiendo el catálogo anterior**. Uno de esos cinco lo hace en forma de **métrica con objetivo declarado** —«18 de 18, sin inventadas»—, es decir, un criterio que hoy da falso al medirse contra el §6 del propio catálogo que la métrica manda recontar.

Se levantan **quince hallazgos: un P0, cinco P1, seis P2 y cinco P3** (P3-01 agrupa nueve cabeceras). **Ninguno toca una decisión de contrato**: no hay un punto de acceso mal definido, un código mal traducido ni una regla mal atribuida. Todos se corrigen recontando y reescribiendo frases, y en cuatro casos borrando restos de una edición anterior que quedó a medio hacer. Pero dos de ellos —`P1-01` y la mitad del `P0-01`— **ya habían sido reportados y no se corrigieron**, y uno de los P1 falsifica un objetivo de onboarding que se declara verificable.

---

## 2. Conformidad con la guía de cada categoría

### 2.1 Categoría 02 contra `Rules-Especificacion-Funcional.md` §6

| Criterio de la guía | Resultado | Evidencia |
| --- | --- | --- |
| Existe `Especificacion-Funcional.md` con índice maestro y matriz NB→CU→RN→US | **Cumple** | §5 catálogo, §7.1 matriz de nueve filas con las cuatro columnas |
| La cantidad de CU cumple el mínimo del tipo D8 | **Cumple.** `rest-api` exige «1 por recurso público + 5 transversales»; hay doce, con cinco transversales identificables (CU-02, CU-09, CU-10, CU-11, CU-12) | §2.2 de la guía contra §5 del índice maestro |
| Cada CU contiene las once secciones obligatorias del §4.2 | **Cumple, los doce.** Recuento mecánico: 12 encabezados `##` por archivo (tabla de contenido más las once secciones), en los doce | `grep -c '^## '` = 12 en `CU-01` a `CU-12` |
| Cada CU declara trazabilidad y **al menos tres** criterios Given/When/Then con valores concretos | **Cumple con holgura.** Mínimo observado: **7** (`CU-08`, `CU-10`); máximo: **10** (`CU-07`) | Recuento de las filas `CA-XX` dentro de §8 de cada archivo |
| Cada RN con sus siete secciones y CU afectados | **No aplica.** Artefacto omitido con motivo declarado; ver §2.3 | §9 del índice maestro |
| Modelo de datos si el tipo lo exige | **Apartamiento declarado.** Ver §2.3 | §9 del índice maestro, fila 2 |
| Existe `Glosario-Funcional.md` con las cinco secciones de §4.2.4 y tabla no vacía | **Cumple.** Trece términos acuñados, §3 con tres polisemias y evidencia, §3.4 con los casos que no lo son, §4 con los referenciados | `Glosario-Funcional.md` §1 a §5 |
| Ninguna polisemia con contextos disjuntos reportada ni corregida calificando todo | **Cumple, y es explícito.** §3.4 declara «salud» y la convivencia `A-XX`/`CU-XX` como **no polisemia** y pide que no se levanten | `Glosario-Funcional.md` §3.4 |
| Ningún archivo con sufijo de versión en el nombre; versión en la cabecera | **Cumple.** Los 16 archivos declaran `Versión` y `Estado` en cabecera; no hay `_legacy/` y se declara por qué | §9 del índice maestro, fila 4 |
| Un solo archivo por nombre lógico | **Cumple** | `ls` de la carpeta y de `Casos-De-Uso/` |
| Sin stacks concretos ni protocolos del dominio fuente | **Cumple parcialmente por decisión declarada.** El documento de concepto central se llama `Definicion-Superficie-HTTP.md` y nombra verbos y números de protocolo. **No lo cuento como hallazgo**: el concepto central de un `rest-api` es su superficie, la guía §2.1 habilita `Definicion-<Concepto-Central>.md` para eso, y el intake §17.5.P.3 declara el protocolo como dato de la fuente, no como elección de esta categoría | `Rules-Especificacion-Funcional.md` §2.1 contra `Definicion-Superficie-HTTP.md` y `PRODUCT-INTAKE` 1.26 §17.5.P.3 |
| Tabla de contenido en todo documento con más de tres secciones de primer nivel | **Cumple, en los 16** | Verificado archivo por archivo |

### 2.2 Categoría 03 contra `Rules-UX-UI-DX.md` §6

| Criterio de la guía | Resultado | Evidencia |
| --- | --- | --- |
| Variante declarada en cabecera y coherente con el tipo D8 | **Cumple.** Los cinco artefactos declaran `**Variante:** DX`; `tiene_ui_final` == false en `PRODUCT-MANIFEST` 1.3 §5 | Cabeceras + manifiesto |
| `DX-Developer-Experience.md` con las **nueve** secciones de §4.2.3, con Diátaxis y tramos 5/30/60 | **Cumple en estructura.** Las nueve están: rol, tramos, quick-start, Diátaxis, errores, métricas, feedback, trazabilidad, control de cambios. **El objetivo del tramo de 30 minutos es falso hoy** → `P1-04` | `DX-Developer-Experience.md` §1 a §9 |
| `Guia-Onboarding-Developer.md` con las seis secciones de §4.2.4 | **Cumple.** Audiencia, instalación, primer ejemplo, diagnóstico, próximos pasos, control de cambios; más §6, que es adicional y declarada | `Guia-Onboarding-Developer.md` §1 a §7 |
| `DX-Error-Messages.md` con las seis secciones de §4.2.5 | **Cumple.** Principios, taxonomía, catálogo, tono y voz, localización, control de cambios | `DX-Error-Messages.md` §1 a §7 |
| `DX-Portal-Developers.md` | **Apartamiento declarado y correcto.** La guía §2.1 lo hace obligatorio para «rest-api **con portal visible**»; `tiene_portal_developers` == false en el manifiesto | `03/README.md` §4 + `MANIFEST` §5 |
| `DX-Operability.md` | **Omisión correcta.** Obligatorio para `worker-service`; recomendado para «rest-api con SLO estricto», y el manifiesto declara «sin SLO de disponibilidad». Motivo incompleto → `P3-04` | `03/README.md` §4 |
| Cada `dx-` doc con quick-start verificable y reproducible | **Cumple, con una no aplicabilidad declarada y bien fundada.** `DX-Error-Messages.md` §6.3 declara el quick-start no aplicable por ser modo *reference* y remite al único del proyecto de código. **No se da por cumplido**, que es lo que la guía castiga | `DX-Error-Messages.md` §6.3, `03/README.md` §5 |
| WCAG 2.2 AA como piso de toda accesibilidad declarada | **No aplica, declarado sin darlo por cumplido** | `03/README.md` §5, fila 4 |
| Artefactos UX/UI y de Fase B2 | **No aplican.** `requiere_maqueta` == false en el manifiesto; las seis omisiones están declaradas con motivo | `03/README.md` §4 |
| Existe `Glosario-UX.md`, tabla no vacía, sin duplicar 02 con otra semántica | **Cumple.** Dieciocho términos, §4.2 declara los trece de 02 que reusa «sin excepción con la misma semántica y no redefine ninguno» | `Glosario-UX.md` §2 y §4.2 |
| Ninguna polisemia con contextos disjuntos reportada como defecto | **Cumple, y es explícito.** §3.3 declara tres casos que **no** lo son y pide que no se levanten | `Glosario-UX.md` §3.3 |
| Trazabilidad upstream y downstream en cada artefacto | **Cumple los cinco** | Cabeceras + §8 / §6.3 |
| Tabla de contenido y ausencia de sufijo de versión en nombre | **Cumple los cinco** | — |

### 2.3 Apartamientos declarados

Los cinco están declarados con motivo, que es lo que exige la guía. **Ninguno es hallazgo.**

| Apartamiento | Qué exige la guía | Motivo declarado | Verificación |
| --- | --- | --- | --- |
| `Reglas-De-Negocio/RN-XX` omitido | Obligatorias para `rest-api` | Las dieciséis reglas viven en `GeometriaFactory-Domain` con archivo propio y acá se referencian; §6 declara dónde se ejerce cada una | Conté **16** archivos `RN-01` a `RN-16` en `GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/`; los dieciséis enlaces de §6 resuelven |
| `Modelo-Datos/` omitido | Obligatorio para `rest-api` con persistencia | `tiene_persistencia` == true «porque toma de configuración la ruta y dispara las transformaciones, no porque modele el dato»; el modelo está en `Infrastructure` | `MANIFEST` §5 confirma el flag y su fundamento textual; el modelo existe en `Infrastructure/02/Modelo-Datos/` |
| Sección opcional §17 de los CU, no emitida | `Rules-Especificacion-Funcional.md` §4.3 la asigna al tipo `library` | «Este proyecto de código es `rest-api` y esta categoría no se apropia de una asignación que no le corresponde» | Correcto contra la guía |
| Seis artefactos de la variante UX/UI, no emitidos | Obligatorios para tipos con UI final y con maqueta | `tiene_ui_final` == false, `requiere_maqueta` == false | `MANIFEST` §5, fila `GeometriaFactory-Api` |
| `DX-Portal-Developers.md`, no emitido | Obligatorio para `rest-api` **con portal visible** | `tiene_portal_developers` == false | `MANIFEST` §5 |

---

## 3. Recuentos verificados por el auditor

Contados a mano sobre los archivos, no leídos de una declaración.

| Magnitud | Valor verdadero | Cómo lo conté | Estado en el corpus auditado |
| --- | --- | --- | --- |
| Casos de uso de esta categoría | **12** | `ls Casos-De-Uso/`, `CU-01` a `CU-12` sin huecos | **Coherente** en los 21 archivos |
| Puntos de acceso | **15** | Filas de `Definicion-Superficie-HTTP.md` §3: `A-01`,`A-02`,`A-03`,`A-05`…`A-16`. `A-04` retirado y **no reciclado** | **Coherente salvo** `CU-12` §9 (`P1-01`) y `DX-Developer-Experience.md` §2 (`P1-04`) |
| Puntos sin acceso firmado / bajo la guardia | **4 / 11** | A-01, A-02, A-03, A-16 sin acceso; los once restantes bajo `CU-02`. 4 + 11 = 15 | **Coherente salvo** `DX-Developer-Experience.md` §2, que aún parte 4 + 1 + 11 = 16 |
| Rutas que **no** declara ninguna fuente | **14 de 15** | Sólo `A-01` lleva `[declarada por la fuente]`; `A-16` declara el punto y no la ruta | **Coherente salvo** `Guia-Onboarding-Developer.md` §1 (`P2-04`) |
| Códigos de respuesta | **10** | Filas de `Definicion-Superficie-HTTP.md` §4: 200, 201, 204, 400, 401, 403, 404, 409, 500, 503 | **Coherente** en los 21 archivos |
| Códigos de contrato **vivos** | **15** | Filas de `Definicion-Superficie-HTTP.md` §6, contra `Contracts/CU-06` §10: «El conjunto cerrado tiene quince códigos» | **Coherente salvo** `CU-09` §1 y `Especificacion-Funcional.md` §5 y §8 (`P1-02`, `P1-03`) |
| Códigos de contrato **emitidos** en la historia | **18** | 15 vivos + 3 retirados y no reciclados: `CONTRATO_TEXTO_NO_INTERPRETABLE` (Contracts 1.1), `CONTRATO_CONTRASENA_NO_ESTABLECIDA` y `CONTRATO_RESETEO_NO_APLICABLE_A_CUENTA_SIN_CONTRASENA` (Contracts 1.6) | Ningún artefacto auditado afirma un total histórico; no hay contradicción |
| Códigos con destino / sin destino | **14 / 1** | Recuento fila por fila de §6; el sin destino es `CONTRATO_SERVICIO_NO_DISPONIBLE` | **Coherente en `DX-Error-Messages.md`; falso en los cinco documentos que lo citan** (`P0-01`) |
| Entradas del catálogo de fallos | **16** = 14 + 2 | Recuento sobre §3.1 a §3.7: 3+2+2+1+2+5+1 | **Falso en cinco lugares** (`P0-01`) |
| Reglas de negocio del producto | **16** | Archivos de `GeometriaFactory-Domain`; `PRODUCT-INTAKE` 1.26 §4.1 titula «RN-01 a RN-16» | **Coherente en el recuento; falso en el reparto** en dos lugares (`P1-05`) |
| Invariantes del producto | **9** | `PRODUCT-INTAKE` 1.26 §14: «INV-01 a INV-09 —los nueve que §17.1.P.2 declara—» | No se citan recuentos de invariantes en estas dos categorías; nada que contradecir |
| Escenarios del intake | **8** | `§20.E-1` a `§20.E-8` | **Coherente** en `CU-12`, `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` y los dos glosarios |
| Casos en la batería del validador | **10** | `PRODUCT-INTAKE` 1.26 §17.5.P.8: «incluidas las **diez** pruebas del validador» | Ningún artefacto auditado lo cita. Ver §9 |
| Casos de uso de `GeometriaFactory-Contracts` | **8** | `ls` de su `Casos-De-Uso/` | **Coherente**: «los ocho contratos de uso», en seis lugares |
| Casos de uso de `GeometriaFactory-Application` | **11** | `ls` de su `Casos-De-Uso/` | **Coherente**: §7.4 los mapea los once, uno por uno |
| Necesidades de negocio | **9** | NB-00001 a NB-00009 | **Coherente**; la matriz §7.1 tiene nueve filas y la cobertura inversa, doce |
| Historias previstas | **30** | Filas de §7.3, `US-01` a `US-30` sin huecos | **Coherente** |
| Puntos abiertos | **11** = 7 propios + 4 heredados | Filas vivas de §11 | **Coherente en el total; contradictorio en el reparto** de uno (`P2-06`) |

---

## 4. Verificación de citas contra la fuente original

Se abrieron las fuentes. **Ninguna cita entrecomillada se dio por buena.** Se prestó atención especial a las citas hechas *a través* de un documento intermedio, que es el vector del defecto de fondo del producto.

| Cita | Dónde | Fuente abierta | Resultado |
| --- | --- | --- | --- |
| «delega en `GeometriaFactory.Infrastructure`» | `Especificacion-Funcional.md` §9 y `02/README.md` §4 | `PRODUCT-INTAKE` 1.26 §17.5.P.4 | **Verdadera, literal** |
| «con los escenarios **E-1 a E-8** como cuerpo» | `CU-12` §10 y `Especificacion-Funcional.md` §11 | `PRODUCT-INTAKE` 1.26 §16.1 | **Verdadera, literal.** La corrección 1.2/1.3 que reemplazó «E-1 a E-7» está bien hecha |
| «con los cuerpos de **E-2 y E-5**» | `CU-12` §10 y `Especificacion-Funcional.md` §11 | `PRODUCT-INTAKE` 1.26 §18, S-2 | **Verdadera, literal.** La divergencia de alcance con §16.1 **subsiste hoy** |
| «E-8 es el modo de falla que el propio intake llama **el más probable** de todos» | `CU-12` §10, `Guia-Onboarding-Developer.md` §3.3 | `PRODUCT-INTAKE` 1.26 §20.E-8, punto 5 | **Verdadera**: «Es además el modo de falla **más probable** de todos los escenarios» |
| E-8: error de validación, el trabajo **queda en `Borrador`** | `Especificacion-Funcional.md` §11, `CU-06`, `CU-12` §6, `DX-Error-Messages.md` §5 | `PRODUCT-INTAKE` 1.26 §20.E-8, punto 5 | **Verdadera** |
| «un puerto publicado hacia el enrutador es el único punto de entrada al servidor propio» | `Especificacion-Funcional.md` §1 y `Definicion-Superficie-HTTP.md` §1, citando §17.5.**P.9** | `PRODUCT-INTAKE` 1.26 §17.5.P.9 | **Verdadera, y la sección citada es la correcta** |
| «`401` genérico sin revelar cuál campo falló» / «`403` con motivo ante cuenta `Pendiente` o `Bloqueada`» | `Definicion-Superficie-HTTP.md` §2 y §4 | `PRODUCT-INTAKE` 1.26 §17.5.P.5 | **Verdaderas las dos** |
| «`POST /auth/token`», única ruta declarada | `Definicion-Superficie-HTTP.md` §2 y §3 | `PRODUCT-INTAKE` 1.26 §17.5.P.3 y P.5 | **Verdadera** |
| Vigencia «corta», «sin token de refresco», sin número | `Definicion-Superficie-HTTP.md` §7 y §9 | `PRODUCT-INTAKE` 1.26 §17.5.P.5 | **Verdadera** |
| «la pasarela de reenvío **no se implementa** y queda especificada» (X-9) | `Especificacion-Funcional.md` §8, `Definicion-Superficie-HTTP.md` §7 | `PRODUCT-INTAKE` 1.26 §17.5.P.11 punto 4 | **Verdadera** |
| Despliegue «manual y a cargo del docente» | `02/README.md` §5, `03/README.md` §6 | `PRODUCT-INTAKE` 1.26 §17.5.P.8 | **Verdadera** |
| «El conjunto cerrado tiene quince códigos» | `Definicion-Superficie-HTTP.md` §6 | `Contracts/CU-06` §10 | **Verdadera, literal** |
| «los **once** casos de uso» de la capa de aplicación | `Especificacion-Funcional.md` §7.4 | `ls` de `Application/02/Casos-De-Uso/` | **Verdadera**, y los once están mapeados |
| «`GeometriaFactory-Infrastructure` §7.2 declara ser **una de las dos** secciones que cubren las nueve necesidades» | `Especificacion-Funcional.md` §7.2 y §11 | `Infrastructure/02/Especificacion-Funcional.md` §7.2, línea 171 | **Verdadera, literal.** El residuo que esta categoría anota para que otro lo absorba **existe y sigue abierto** |
| «`GeometriaFactory-Application` declara explícitamente que no toca NB-00008» | `Especificacion-Funcional.md` §7.2 | `Application/02/Especificacion-Funcional.md`, matriz §7.1 y su tabla de necesidades sin CU | **Verdadera** |
| «`Especificacion-Funcional.md` §6 dice: trece con tramo acá, **dos** sin él» | `03/README.md` §6 y `DX-Developer-Experience.md` §8 | `Especificacion-Funcional.md` §6 | **FALSA.** §6 dice «Trece de las dieciséis tienen tramo acá y **tres** no lo tienen» → `P1-05` |
| «el catálogo de las **18 entradas** — **16** códigos con destino más **2** sin código» | `03/README.md` §2, `Glosario-UX.md` §2 y §3.1, `DX-Developer-Experience.md` §5 y §6, `Guia-Onboarding-Developer.md` §3.5 | `DX-Error-Messages.md` §2.1, §3, §6.1, §6.2 | **FALSA en los seis lugares.** El catálogo dice dieciséis entradas, catorce con destino → `P0-01` |
| «§7 declara las **seis** ausencias de la superficie» | `02/README.md` §4 | `Definicion-Superficie-HTTP.md` §7 | **FALSA.** Son siete, y el mismo README §1 dice siete → `P2-03` |
| «uno de los **diecisiete**» códigos del contrato | `CU-09` §1 | `Contracts/CU-06` §10 y el propio §1 dos párrafos antes | **FALSA** → `P1-02` |

---

## 5. Las tres reglas de arquitectura

Enunciados verificados **contra el original**, `PRODUCT-INTAKE` 1.26 §14, tabla de reglas de nivel producto. Es la frontera del producto, así que las tres se auditaron en detalle.

| Regla | Enunciado de la fuente | Cómo la trata este proyecto de código | Resultado |
| --- | --- | --- | --- |
| **RA-01** | «Ningún JavaScript del navegador invoca la API» | `Especificacion-Funcional.md` §4 precisión 4 la deriva a «esta superficie no tiene ningún cliente legítimo que no sea `GeometriaFactory-Web`», y de ahí saca **tres ausencias declaradas**: sin CORS, sin WebSockets y sin ningún punto pensado para un navegador. `Definicion-Superficie-HTTP.md` §7 las convierte en tres filas con «qué las repone si se rompe». `03/README.md` §1 la usa para justificar cero wireframes | **Correcta y bien derivada.** La derivación se apoya además en §17.5.P.3, que declara literalmente «Únicamente `GeometriaFactory.Web`, servidor a servidor. El navegador nunca la alcanza (RA-01)». Recorrí las quince filas de §3: **ninguna declara un punto pensado para el navegador**, y ninguna tabla de códigos incluye respuestas de preflight |
| **RA-02** | «El bundle del visor es un visualizador puro: sin configuración, sin red, sin conocimiento del sistema» | `Especificacion-Funcional.md` §4 precisión 5 y `DX-Developer-Experience.md` §1.3 precisión 2 la declaran **sin tramo acá**, con el fundamento de que esta capa no compone el bundle, no lo sirve y no lo configura, y con una contribución negativa: al no existir punto pensado para el navegador, no hay nada que el bundle pudiera llamar | **Correcta.** «No tener tramo no es incumplirla» está dicho en los dos lugares. Es el tratamiento que la guía pide para una regla no aplicable: declararla, no omitirla |
| **RA-03** | «Todo lo que el navegador deba obtener del backend pasa por el front; los mensajes de error nunca incluyen direcciones de servicios internos» | Tratada como **la regla que sólo acá se puede violar hacia afuera**: `Definicion-Superficie-HTTP.md` §8 y `DX-Error-Messages.md` §1.4 son dos tablas de «lo que ninguna respuesta puede decir» —dirección de servicio interno, ruta del almacén, clave de firma, contraseña, provisoria, texto del alumno, trazas de implementación—, cada una con su reemplazo. Su contracara, el registro del lado del servidor, está en las dos, citando §17.5.P.10 | **Correcta, y es la parte mejor construida de las dos categorías.** Verifiqué que **ninguna** de las quince filas de la tabla de traducción de §6, ninguna de las dieciséis entradas del catálogo y ninguno de los trece diagnósticos de la guía de onboarding devuelva una ruta, una dirección o un secreto: el diagnóstico del `503` dice explícitamente «el mensaje no dice la ruta, y es a propósito». Además tiene **métrica propia con tolerancia cero** en `DX-Developer-Experience.md` §6 |

**Ningún hallazgo de arquitectura.** Las tres reglas están enunciadas con las palabras de la fuente, atribuidas al nivel producto y con su tramo —o su ausencia de tramo— declarado.

---

## 6. Los once puntos abiertos, uno por uno

Criterio: un punto abierto es **falso** si la fuente ya lo resolvió, o si no tiene titular. Se abrió el intake 1.26 para cada uno.

| # | Punto abierto | Titular declarado | Verificación contra la fuente de hoy | Veredicto |
| --- | --- | --- | --- | --- |
| 1 | Código del contrato para «el papel no alcanza» fuera del desenlace | Product Owner y `GeometriaFactory-Contracts` | Recorrí los quince códigos vivos: el único de facultad es `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR`, acotado por enunciado al desenlace | **Verdadero** |
| 2 | Código del contrato para envío o reedición forzados fuera de `Borrador` | Product Owner y `GeometriaFactory-Contracts` | `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` es el análogo y está acotado a la eliminación y al camino del alumno | **Verdadero** |
| 3 | Rutas y verbos definitivos | `05-Arquitectura-Tecnica` | El intake declara sólo `POST /auth/token` y la existencia de un punto de salud sin ruta. Abrí `05-Arquitectura-Tecnica/Contratos-REST.md` §3: publica los quince puntos **sin columna de ruta**, de modo que tampoco los fija | **Verdadero.** Ver la nota de abajo |
| 4 | Código de respuesta de una terminación degradada del almacén | `05` y Product Owner | El conjunto cerrado no tiene código propio; `CU-09` §6 adopta uno y lo rotula derivación | **Verdadero** |
| 5 | Alcance de la colección de peticiones | Product Owner | §16.1 dice «E-1 a E-8»; §18 S-2 dice «los cuerpos de E-2 y E-5». **Los dos textos están vivos en 1.26 y siguen divergiendo** | **Verdadero** |
| 6 | Vigencia exacta del acceso firmado | `05` y Product Owner | §17.5.P.5: «Corta. Renovación por reingreso; sin token de refresco». Sin número | **Verdadero.** Su clasificación como propio/heredado sí es hallazgo → `P2-06` |
| 7 | Límite de tamaño del cuerpo de una petición | Product Owner y `05` | Ninguna sección del intake lo declara | **Verdadero** |
| 8 | Nombres de tipos y espacios de nombres | `05` | Declarado abierto aguas arriba, sin resolver | **Verdadero, heredado** |
| 9 | Versiones exactas de los paquetes | `05`, primera etapa | §17.5.P.11: «Queda abierto: la versión exacta de los paquetes, que se ancla en la etapa `a`» | **Verdadero, heredado** |
| 10 | Construcción de la imagen en destino desde el repositorio | `09-Devops` | §17.5.P.11 punto 5 lo rotula **[A VERIFICAR]** y exige probarlo una vez | **Verdadero, heredado** |
| 11 | Valores numéricos de los requerimientos no funcionales | Product Owner y `08` | §17.5.P.6 y P.10 llevan **[ASUNCIÓN]** en cobertura, latencia, caudal y arranque en frío | **Verdadero, heredado** |

**Ningún punto abierto falso, y los once tienen titular.** El punto abierto cerrado —la identidad en el establecimiento de la contraseña del primer ingreso— está correctamente movido a prosa, con la fila tachada conservada, y su cierre por `RN-16` se verifica en el intake 1.26 §4.1.

**Nota sobre el punto 3, propia de la emisión tardía.** `05-Arquitectura-Tecnica/Contratos-REST.md` 1.1 ya está emitido y **tampoco fija las rutas**: publica los quince puntos con verbo y códigos y remite la ruta a la implementación. El punto abierto de 02 sigue siendo verdadero, y la frase «catorce de las quince rutas todavía no están decididas» sigue siendo cierta hoy. **No es hallazgo**; se registra porque un lector que llegue en 2026-08-11 podría suponer lo contrario al ver la categoría 05 emitida.

---

## 7. Hallazgos

Quince hallazgos. Cada uno con dónde está, qué dice, qué debería decir y cómo lo verifiqué.

### 7.1 P0

#### `B-API-01` (P0) · El catálogo de fallos quedó recontado en su propio archivo y sin recontar en los cinco que lo citan

**Dónde está.** Seis lugares vivos en cinco documentos:

| # | Archivo y sección | Qué dice hoy |
| --- | --- | --- |
| a | `03-UX-UI-DX/README.md` §2, fila de `DX-Error-Messages.md` | «Catálogo de las **18 entradas** de respuesta de fallo —16 códigos del contrato con destino más 2 respuestas sin código—» |
| b | `03-UX-UI-DX/Glosario-UX.md` §2, entrada «Entrada del catálogo» | «Cada una de las **18** situaciones… **16** códigos del contrato con destino más **2** respuestas sin código» |
| c | `03-UX-UI-DX/Glosario-UX.md` §3.1, primera fila de la polisemia «error» | «Cada una de las 18 situaciones en las que esta superficie responde con un fallo» |
| d | `03-UX-UI-DX/DX-Developer-Experience.md` §5, primera línea | «aplicado sin excepción a las **18** entradas del catálogo» |
| e | `03-UX-UI-DX/DX-Developer-Experience.md` §6, métrica «Cobertura del catálogo de respuestas» | Objetivo **«18 de 18, sin inventadas»**, con «Recuento contra `DX-Error-Messages.md` §6» como forma de medirlo |
| f | `03-UX-UI-DX/Guia-Onboarding-Developer.md` §3.5, última línea | «El catálogo entero, con las **18** entradas…» |

**Qué debería decir.** **Dieciséis** entradas: **catorce** códigos del conjunto cerrado con destino más **dos** respuestas sin código. La métrica (e) debe decir **«16 de 16»**.

**Cómo lo verifiqué.** Conté las filas de las siete tablas de `DX-Error-Messages.md` §3.1 a §3.7: 3 + 2 + 2 + 1 + 2 + 5 + 1 = **16**. Su §6.1 declara exactamente eso —«15 códigos del conjunto cerrado, 14 con destino, 1 sin destino, 2 respuestas sin código, 16 = 14 + 2»— y su §6.2 lo repite en las dos direcciones. Contrasté contra `Definicion-Superficie-HTTP.md` §6 («Quince códigos: catorce con destino en esta superficie y uno sin él», quince filas contadas una por una) y contra el dueño del conjunto, `GeometriaFactory-Contracts/CU-06` §10 («El conjunto cerrado tiene quince códigos»). Los tres coinciden en 15/14/1 y en 16 entradas. Los seis lugares de arriba contradicen a los tres.

**Por qué P0.** Tres motivos que se acumulan. **Primero**, es el defecto de fondo del producto en su forma exacta: seis afirmaciones falsas sobre una fuente viva, y cinco de las seis son citas *a través* del catálogo en vez de contra él. **Segundo**, el lugar (e) no es prosa sino un **objetivo de métrica declarado con su forma de medición**: quien la mida como manda el propio texto obtiene 16 y la declara incumplida, cuando lo incumplido es la métrica. **Tercero**, y es lo que lo vuelve estructural: `DX-Error-Messages.md` fue corregido **dos veces por auditorías previas** —1.2 por `C-04` y 1.3 por `C-05-04`, cuya fila dice que 1.1 «actualizó §2.3, §2.4 y el encabezado de §3 pero **no recontó las tablas de §2.1 ni de §6**»— y en ninguna de las dos ocasiones se preguntó **quién más citaba esos números**. La corrección se agotó en el archivo. El vector sigue vivo.

**Propagación aguas abajo.** Verifiqué `05-Arquitectura-Tecnica/Contratos-REST.md` §5, que es el consumidor directo: publica **quince** filas y el título correcto, de modo que **la cadena 05 tiene el número bueno**. La corrección es local a la categoría 03 y no obliga a retocar 05.

### 7.2 P1

#### `B-API-02` (P1) · `CU-12` §9: «13 de 16» y «los tres que no», contra su propia §8 y §10

**Dónde está.** `02-Especificacion-Funcional/Casos-De-Uso/CU-12-…-Reproducible.md` §9, fila «Puntos de acceso que ejercita».

**Qué dice.** «**13 de 16**. Los tres que no, en §10».

**Qué debería decir.** «**13 de 15**. Los **dos** que no, en §10» —A-08 y A-16—.

**Cómo lo verifiqué.** Tres contrastes dentro del mismo archivo y uno fuera. (1) Su §8, `CA-08`, dice: «Ejercita **13 de los 15** puntos —A-01 a A-03, A-05 a A-07 y A-09 a A-15—. Los **2** que no ejercita, A-08 y A-16… 13 + 2 = 15. El punto **A-04 se retiró**». Conté los identificadores enumerados: 3 + 3 + 7 = **13**. (2) Su §10 abre con «Los **dos** puntos de acceso que la colección no ejercita, y una precisión sobre un tercero», y ese «tercero» es **A-12**, que **sí se ejercita** —sólo en sus caminos rechazados—; la frase de cierre «Los tres puntos se declaran» cuenta a A-12, no a un tercero no ejercitado. El «tres» de §9 lee mal esa frase. (3) Su propio control de cambios 1.1 declara: «**§8** actualiza **CA-08**: la cobertura pasa de **14 de 16** a **13 de 15**» — es decir, la intervención tocó §8 y **no** §9. (4) Fuera: `Definicion-Superficie-HTTP.md` §3 tiene quince filas y declara `A-04` retirado y no reciclado.

**Por qué importa.** Es residuo anterior al retiro de `A-04`, ya reportado en rondas previas del corpus y **no corregido**. §9 es la sección de trazabilidad, que es la que leen las categorías 06 y 08 para derivar historias y pruebas; deja escrito que hay un punto de acceso no ejercitado que no existe.

#### `B-API-03` (P1) · `CU-09` §1: «uno de los diecisiete», dos párrafos debajo de «el conjunto cerrado de quince»

**Dónde está.** `Casos-De-Uso/CU-09-Traducir-El-Motivo-Del-Contrato-A-Respuesta-De-Protocolo.md` §1, punto 1.

**Qué dice.** «…llegan hasta acá y acá se convierten en uno de los **diecisiete**».

**Qué debería decir.** «…en uno de los **quince**».

**Cómo lo verifiqué.** El párrafo inmediatamente anterior del **mismo §1** dice «su unidad de verificación no es una ruta sino **el conjunto cerrado de quince códigos**», y §4 paso 2 dice «dentro del conjunto cerrado de **quince**». Contra la fuente: `Contracts/CU-06` §10, «El conjunto cerrado tiene quince códigos». Agrava que el control de cambios 1.1 de este mismo archivo afirma «**§1 y §4 actualizan el recuento**»: la actualización de §1 alcanzó a una de sus dos menciones. Es una corrección declarada como hecha y hecha a medias, que es peor que no declararla, porque una revisión que confíe en el control de cambios no vuelve a mirar.

#### `B-API-04` (P1) · El índice maestro conserva dos recuentos de diecisiete códigos

**Dónde está.** `02-Especificacion-Funcional/Especificacion-Funcional.md`, dos lugares:

- §5, catálogo, fila `CU-09`: «Las dos traducciones, los **dieciséis** códigos con destino y **el que no lo tiene**» —dieciséis más uno es diecisiete—.
- §8, viñeta «Particiones»: «su unidad de verificación es el conjunto cerrado de códigos del contrato… **se prueba recorriendo los diecisiete**, no ejerciendo una ruta».

**Qué debería decir.** «los **catorce** códigos con destino y el que no lo tiene»; «se prueba recorriendo los **quince**».

**Cómo lo verifiqué.** Contra `Definicion-Superficie-HTTP.md` §6, que es el documento que este mismo índice maestro declara dueño de la tabla («catorce con destino en esta superficie y uno sin él»), y contra `Contracts/CU-06` §10. El propio control de cambios 1.1 de este archivo declara: «**§9 y §11**: los recuentos del conjunto cerrado del ensamblado pasan de diecisiete a **quince**» — §5 y §8 no estaban en la lista y quedaron sin tocar. **Este archivo es el punto de entrada declarado de la categoría**, y su §5 es lo primero que lee quien busca qué hace `CU-09`.

#### `B-API-05` (P1) · El objetivo verificable del tramo de 30 minutos describe una superficie de dieciséis puntos

**Dónde está.** `03-UX-UI-DX/DX-Developer-Experience.md` §2, tabla de onboarding, fila «30 minutos», columna «Cómo se verifica».

**Qué dice.** «Reproduce, sin abrirla, la partición de la tabla de puntos de acceso: **cuatro sin acceso, uno con identidad abierta y once bajo la guardia**» — cuatro más uno más once es **dieciséis**.

**Qué debería decir.** «cuatro sin acceso firmado y once bajo la guardia. Cuatro más once son quince», sin la categoría intermedia.

**Cómo lo verifiqué.** `Definicion-Superficie-HTTP.md` §3, cierre de la tabla: «**Quince puntos de acceso**… cuatro no exigen acceso firmado —A-01, A-02, A-03 y A-16— y **los once restantes** exigen acceso firmado y quedan bajo la guardia de `CU-02`. Cuatro más once son quince. **Ningún punto queda con su forma de identificación abierta**, y ésa es la diferencia con la emisión 1.0». El punto que tenía identidad abierta era `A-04`, retirado. Recorrí las quince filas: los cuatro sin papel exigido son los declarados, y no hay ninguno con la columna de papel indeterminada.

**Por qué P1 y no P2.** La guía §6 exige que los tramos de onboarding tengan **objetivo verificable**. Éste lo tiene y **hoy da falso**: quien lo reproduzca correctamente contra la tabla vigente falla el tramo. No es una frase envejecida en prosa: es un criterio de aceptación de la sección.

#### `B-API-06` (P1) · Dos documentos de 03 citan a §6 del índice maestro con un reparto que §6 no dice

**Dónde está.**

- `03-UX-UI-DX/README.md` §6, nota «Las reglas de negocio no viven acá»: «`Especificacion-Funcional.md` §6 dice, regla por regla, dónde se ejerce cada una: **trece con tramo acá, dos sin él**, y dos que esta capa puede romper hacia afuera sola».
- `03-UX-UI-DX/DX-Developer-Experience.md` §8, fila «Reglas de negocio relevantes»: «con el lugar donde se ejerce cada una declarado en `Especificacion-Funcional.md` §6: **trece con tramo acá, dos sin él**…».

**Qué dice la fuente.** `Especificacion-Funcional.md` §6, segundo párrafo: «**Trece de las dieciséis tienen tramo acá y tres no lo tienen**, y el recuento cierra en dieciséis».

**Qué debería decir.** «trece con tramo acá, **tres** sin él».

**Cómo lo verifiqué.** Abrí §6 y conté sus dieciséis filas: las marcadas **Sin tramo acá** son `RN-05`, `RN-14` y `RN-16` — tres. Trece más tres es dieciséis, que es el número de archivos de `GeometriaFactory-Domain/02/Reglas-De-Negocio/`, contados. Trece más dos es quince, el reparto anterior a `RN-16`.

**Por qué importa, y por qué es la forma más peligrosa del defecto.** Las dos frases **atribuyen a §6 palabras que §6 no tiene**, y las dos lo hacen en el mismo movimiento en que corrigieron el otro número de la frase: `03/README.md` 1.2 dice explícitamente que «la nota decía quince» reglas y la llevó a dieciséis, **sin recontar el reparto que la misma oración enunciaba**. Se abrió la fuente para un número y no para el de al lado.

### 7.3 P2

#### `B-API-07` (P2) · Restos de texto mutilado que dejan viva una referencia a `A-04` en dos tablas de excepciones

**Dónde está.**

- `Casos-De-Uso/CU-01-Canjear-Credenciales-Por-Un-Acceso-Firmado.md` §6, fila `~~CONTRATO_CONTRASENA_NO_ESTABLECIDA~~`, final de celda: «…para que una cita vieja no quede sin respuesta **~a A-04**».
- `Casos-De-Uso/CU-05-Exponer-El-Reseteo-De-La-Contrasena-De-Un-Alumno.md` §6, fila `~~CONTRATO_RESETEO_NO_APLICABLE_A_CUENTA_SIN_CONTRASENA~~`, final de celda: «**El identificador no se recicla** **~ino que ya existe es que la persona la establezca en su primer ingreso, por A-04**».

**Qué debería decir.** Las dos celdas deben terminar donde termina su enunciado, sin el fragmento colgado. La segunda es un trozo de la redacción anterior a 1.1 —«…lo que ya existe es que la persona la establezca en su primer ingreso, por A-04»— que quedó tras un reemplazo con marca de tachado mal cerrada (`~` en lugar de `~~`).

**Cómo lo verifiqué.** Leí las dos celdas enteras; el fragmento no es gramatical y arranca a mitad de palabra («~ino»). Contra la regla que el propio corpus fija: `Definicion-Superficie-HTTP.md` §3 declara que «el identificador `A-04` queda retirado y **no se recicla**, para que una cita vieja no resuelva en silencio a otro punto», y `Especificacion-Funcional.md` §10 punto 3 dice que los `A-XX` son los puntos que §3 enumera —quince, sin A-04—. Las dos celdas **describen a A-04 en presente como camino vigente**, que es justamente lo que el retiro quería evitar.

#### `B-API-08` (P2) · `CU-01` §6: «tres motivos comparten el `403`» y una enumeración que incluye un camino suprimido

**Dónde está.** `Casos-De-Uso/CU-01-…-Acceso-Firmado.md` §6, los dos párrafos posteriores a la tabla.

**Qué dice.** «**Ninguna de las seis** emite acceso…» y «**Por qué tres motivos distintos comparten el `403`** y uno solo tiene el `401`. Los tres del `403` describen la situación de una cuenta que existe y la persona necesita saber cuál es, porque de eso depende qué tiene que hacer después: esperar la habilitación, **establecer su contraseña** o cambiar la provisoria».

**Qué debería decir.** «Ninguna de las **cinco**…» y «Por qué **dos** motivos distintos comparten el `403`… esperar la habilitación **o** cambiar la provisoria».

**Cómo lo verifiqué.** Conté las filas vivas de la tabla de §6: `CONTRATO_CAMPO_REQUERIDO_AUSENTE` (400), `CONTRATO_CREDENCIAL_INVALIDA` (401), `CONTRATO_CUENTA_NO_HABILITADA` (403), `CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO` (403), `CONTRATO_ERROR_NO_CLASIFICADO` (503) — **cinco vivas**, más una tachada. Con `403` hay **dos**. El tercer camino enumerado, «establecer su contraseña», es la operación que `RN-16` suprimió: `Definicion-Superficie-HTTP.md` §7, séptima ausencia, declara que ya no existe ningún punto que fije una contraseña sin credencial, y el propio control de cambios 1.1 de `CU-01` dice que «el primer ingreso deja de tener camino y código propios».

#### `B-API-09` (P2) · `02/README.md` §4 dice «seis ausencias» y su propia §1 dice siete

**Dónde está.** `02-Especificacion-Funcional/README.md` §4, última fila, motivo de la sección opcional §17: «…y `Definicion-Superficie-HTTP.md` §7, que declara las **seis** ausencias de la superficie y qué las repone».

**Qué debería decir.** **Siete** ausencias.

**Cómo lo verifiqué.** `Definicion-Superficie-HTTP.md` §7 abre con «cuatro de estas **siete** se reintroducen fácil por comodidad. La séptima no se reintroduce por comodidad sino por inercia de diseño» y su tabla tiene siete filas, contadas: CORS, WebSockets, pasarela de reenvío, versionado de rutas, sesión del lado del servidor, acceso de refresco, y el punto que fija contraseña sin credencial. La séptima entró con 1.1 por `RN-16`, según el control de cambios de ese archivo. Y el **mismo README**, §1, describe ese documento como «las **siete** ausencias declaradas»: el archivo se contradice a sí mismo a catorce líneas de distancia.

#### `B-API-10` (P2) · `Guia-Onboarding-Developer.md` §1: «quince rutas… y quince no lo están»

**Dónde está.** §1, lectura obligatoria 1.

**Qué dice.** «§3 es la tabla de los quince puntos. **Leer §3 sin §2 hace creer que las quince rutas están decididas, y quince no lo están.**»

**Qué debería decir.** «…y **catorce** no lo están».

**Cómo lo verifiqué.** `Definicion-Superficie-HTTP.md` §3 rotula `A-01` con **[declarada por la fuente]** y las otras catorce con **[derivado]**; su §2 declara «las rutas y los verbos de los **catorce** puntos restantes» como derivación. El resto del corpus dice «catorce de las quince rutas» en cinco lugares —`02/README.md` §5, `03/README.md` §1 y §6, `DX-Developer-Experience.md` §1.2 punto 2 y el control de cambios 1.1 de esta misma guía—. Esta frase es la única que dice quince, y es de la sección de lectura obligatoria, es decir, lo primero que alguien lee del proyecto de código.

#### `B-API-11` (P2) · `Guia-Onboarding-Developer.md` §5: «los tres huecos elevados al Product Owner»

**Dónde está.** §5, «Próximos pasos», última viñeta.

**Qué dice.** «Para saber qué está sin decidir: `../02-Especificacion-Funcional/Especificacion-Funcional.md` §11, y en particular los **tres huecos elevados al Product Owner**».

**Qué debería decir.** Los **dos** huecos.

**Cómo lo verifiqué.** `Especificacion-Funcional.md` §11, párrafo de apertura: «Los **dos** primeros son huecos de la superficie que esta categoría encontró y no resolvió». El tercero —la identidad en el establecimiento de la contraseña— está **cerrado**, y §11 lo dice en prosa y con la fila tachada; el cierre se verifica en `PRODUCT-INTAKE` 1.26 §4.1, `RN-16`. Los demás documentos de 03 ya dicen «dos»: `03/README.md` §6 y `DX-Error-Messages.md` §2.4. Esta guía subió a 1.1 por el mismo cambio y no tocó la viñeta.

#### `B-API-12` (P2) · Un punto abierto clasificado como propio en un documento y como heredado en otros dos

**Dónde está.** «Vigencia exacta del acceso firmado».

**Qué dice cada uno.**

- `Especificacion-Funcional.md` §11, fila 7: rotulada «**Propio.**», y su propio texto dice «Es el mismo punto que `GeometriaFactory-Infrastructure` §11 declara abierto, y esta categoría **no lo reabre ni lo resuelve**: **lo hereda** como condición de su guardia».
- `Definicion-Superficie-HTTP.md` §9: «Los **cinco** primeros son propios de este documento; **el sexto se hereda**» — y el sexto es la vigencia.
- `03-UX-UI-DX/README.md` §6: «**cuatro** heredados de aguas arriba, **entre ellos la vigencia exacta del acceso firmado**».

**Qué debería decir.** Una sola cosa. Si la vigencia es heredada —que es lo que dicen dos de los tres documentos y el propio texto de la celda—, el reparto de §11 no es «siete propios y cuatro heredados» sino **seis y cinco**, y el rótulo de la fila debe decir «Heredado».

**Cómo lo verifiqué.** Conté las filas vivas de §11: once, de las cuales siete llevan el rótulo «**Propio.**» y cuatro no. El total de once es correcto y no cambia; lo que está mal es el reparto, y la contradicción es visible dentro de la propia celda, que se rotula propia y se describe heredada.

### 7.4 P3

#### `B-API-13` (P3) · Cabeceras que citan versiones de fuente anteriores a las que el propio cuerpo verifica

Ningún contenido citado cambió entre las versiones involucradas, de modo que **no hay afirmación falsa**; lo que se pierde es la trazabilidad de contra qué se validó cada documento.

| Archivo | Cabecera dice | El cuerpo o el control de cambios verifica contra |
| --- | --- | --- |
| `Especificacion-Funcional.md` | `PRODUCT-INTAKE` **1.13**, `PRODUCT-MANIFEST` **1.2** | §11 y control 1.3: intake **1.18** |
| `Definicion-Superficie-HTTP.md` | intake **1.14** | Control 1.3: absorbe intake **1.15** |
| `Glosario-Funcional.md` | intake **1.13** | — |
| `CU-12` | intake **1.13** | §10: verificado contra **1.18** |
| `DX-Error-Messages.md` | intake **1.14** | — |
| `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md`, `Glosario-UX.md`, `03/README.md` | intake **1.13**, manifiesto **1.2** | — |

Vigentes hoy: intake **1.26**, manifiesto **1.3**. Verifiqué que lo citado de cada uno —§17.5 completo, §14, §16.1, §18, §20, §21, §4.1, §5 del manifiesto— **no cambió de contenido** en lo que estos documentos afirman; por eso es P3 y no P1.

#### `B-API-14` (P3) · `Definicion-Superficie-HTTP.md` §10: filas del control de cambios fuera de orden

Las filas van **1.1, 1.0, 1.2, 1.3**. La guía exige tabla de control de cambios con versión, fecha y descripción; no fija el orden, pero las tres tablas equivalentes de la categoría 02 y las cinco de la 03 van en orden ascendente. Verificado leyendo las cuatro filas.

#### `B-API-15` (P3) · Auto-citas de sección erradas en dos controles de cambios

- `Guia-Onboarding-Developer.md` 1.1: «**§4** actualiza el conjunto cerrado de códigos de diecisiete a quince» — el conjunto cerrado se menciona en **§3.5**, no en §4, que es la tabla de diagnósticos. Y «**§7** actualiza el procedimiento de alta de un punto de acceso» — el procedimiento es **§6.3**; §7 es el propio control de cambios.
- `CU-09` 1.1: «las **siete** condiciones de §6 no cambian de forma» — §6 declara **tres** reglas de asignación, R-1 a R-3, y ninguna condición propia. Verificado contando las filas de §6, y contra `02/README.md` §2, que describe correctamente «las dos traducciones, **las tres reglas de asignación** y los dos huecos declarados».

#### `B-API-16` (P3) · Motivo incompleto en la omisión de `DX-Operability.md`

`03/README.md` §4 motiva la omisión sólo por el tipo D8 («es obligatorio para `worker-service` y este proyecto de código es `rest-api`»). La guía §2.1 lo hace además **recomendado** para «rest-api con SLO estricto», y este proyecto de código tiene `tiene_observabilidad_critica` == **true** en `PRODUCT-MANIFEST` 1.3 §5. **La omisión sigue siendo correcta** —el manifiesto declara «Disponibilidad: **sin SLO**» y el intake §17.5.P.10 lo confirma—, pero el motivo no menciona el flag que un revisor va a mirar primero. Verificado abriendo el manifiesto §5 y el intake §17.5.P.10.

#### `B-API-17` (P3) · Una respuesta sin código de contrato que no está entre «las dos»

`DX-Error-Messages.md` §6.2, tercera comprobación, describe la única respuesta de fallo de `CU-11` como «un `503` **sin código del contrato**… cubierta por la entrada de no clasificado». Su §2.2 declara un conjunto cerrado de **dos** respuestas sin código —el `401` de la guardia y el `400` ilegible—, y la entrada de no clasificado **sí lleva** código (`CONTRATO_ERROR_NO_CLASIFICADO`). O el `503` del punto de salud lleva ese código, y entonces no es «sin código», o no lo lleva, y entonces las respuestas sin código son tres. Verificado contra §2.2, §3.7 y `CU-11` §6. No afecta ningún recuento del catálogo, que cierra igual por cualquiera de las dos lecturas.

---

## 8. Lo que esta auditoría verificó y no reporta

Se declara para que una ronda posterior no lo vuelva a levantar.

1. **Las cinco polisemias declaradas son correctas y sus contextos colisionan.** «Acceso», «código» y «punto» en `Glosario-Funcional.md` §3; «error» y «consumidor» en `Glosario-UX.md` §3. Las cinco traen evidencia de colisión con archivo y sección, y las verifiqué abriendo cada evidencia: `CU-01` §4 efectivamente usa los tres referentes de «acceso» en la misma sección, y `DX-Error-Messages.md` §1.2 pone dos referentes de «error» en la misma tabla.
2. **Los cinco casos declarados como *no* polisemia son correctos y no se levantan.** «Salud» y la convivencia `A-XX`/`CU-XX` (`Glosario-Funcional.md` §3.4); los identificadores literales del contrato, los números de código de respuesta y «guardia» (`Glosario-UX.md` §3.3). **Reportar una polisemia con contextos disjuntos sería un defecto de este informe**, y los cinco lo son.
3. **Los once puntos abiertos son verdaderos y tienen titular** (§6). Un punto abierto correctamente declarado no es hallazgo.
4. **Las cinco omisiones y apartamientos están declarados con motivo y con el flag que los habilita** (§2.3), verificados contra `PRODUCT-MANIFEST` 1.3 §5.
5. **Forma mecánica.** Cero filas con número de celdas distinto del encabezado en los 21 archivos, verificado con un recorrido programático de todas las tablas. Cero enlaces relativos rotos, verificado resolviendo cada destino contra el sistema de archivos. Los 21 archivos declaran `Versión`, `Estado` y `Fecha`; ninguno lleva sufijo de versión en el nombre; no hay `_legacy/` en ninguna de las dos carpetas y las dos declaran por qué.
6. **La matriz de trazabilidad cierra en las dos direcciones.** Nueve necesidades con al menos un CU; doce CU mapeados, con `CU-10` y `CU-12` declarando «ninguna» **con fundamento** en vez de forzarse una traza; treinta historias sin huecos; los once casos de uso de la capa de aplicación orquestados, con cuatro casos de uso propios que declaran no orquestar ninguno.
7. **El residuo ajeno que esta categoría anota y no corrige es real.** `Infrastructure/02/Especificacion-Funcional.md` §7.2 sigue diciendo «una de las **dos** secciones». Anotarlo sin corregirlo desde otro proyecto de código es el comportamiento correcto, y no es hallazgo de este informe. **Es hallazgo pendiente de aquel documento.**

---

## 9. Lo no verificado

- **Los diez casos de la batería del validador.** `PRODUCT-INTAKE` 1.26 §17.5.P.8 dice «incluidas las **diez** pruebas del validador» y §18, muestra `S-3`, dice «la entrada de las **nueve** pruebas de RT §11». **Ninguno de los 21 artefactos auditados cita ese recuento**, de modo que no hay nada que contrastar y no se levanta hallazgo contra este proyecto de código. La discrepancia es interna del intake y su verificación pertenece a quien audite la fuente.
- **Que los guiones `build.sh`, `test.sh`, `reset-db.sh` y `run-api.sh` existan y corran.** El quick-start y la guía de onboarding los invocan por nombre y declaran que salen del intake §16 y §18. No se ejecutó ninguno: verificar el quick-start sobre un clon limpio es lo que la propia sección declara como su forma de verificación en el punto de control, y no es alcance de una auditoría documental.
- **Que las rutas propuestas de los catorce puntos derivados sean las adecuadas.** Es decisión de `05-Arquitectura-Tecnica` y punto abierto declarado; no se evalúa.
- **La conformidad de las categorías 05 a 11 de este proyecto de código.** Se abrieron dos archivos de 05 para verificar un enlace y un recuento citado; no se auditaron.

---

## 10. Dictamen

# RECHAZADO

**Fundamento.**

Las dos categorías cumplen la guía en estructura, completitud y forma: los doce casos de uso llevan sus once secciones, los artefactos DX obligatorios están, los glosarios gobiernan el vocabulario con sus polisemias declaradas y sus no-polisemias protegidas, los apartamientos están declarados con motivo y con el flag que los habilita, los once puntos abiertos son verdaderos y tienen titular, y las tres reglas de arquitectura están enunciadas con las palabras de la fuente, con `RA-03` tratada con un cuidado que es el mejor tramo de las dos categorías. **Nada de lo que se rechaza acá obliga a rehacer una decisión de contrato**: no hay un punto de acceso mal definido, un código mal traducido ni una regla mal atribuida.

Se rechaza por lo otro, que es lo que este producto ya sabe que le cuesta caro:

1. **Un P0 con seis manifestaciones vivas en cinco documentos**, todas ellas afirmaciones falsas sobre un artefacto hermano que fue corregido dos veces por auditorías previas sin que ninguna preguntara quién lo citaba. Una de las seis es una **métrica con objetivo declarado y forma de medición declarada**, que hoy da falso al medirse como manda su propio texto.
2. **Cinco P1, y los cinco son la misma clase**: números del estado anterior al retiro de `A-04` y al achicamiento del conjunto cerrado, sobrevividos dentro de documentos cuyos controles de cambios **afirman haberlos actualizado**. `CU-09` 1.1 dice que §1 se actualizó y §1 conserva «diecisiete»; `CU-12` 1.1 actualizó §8 y dejó §9 diciendo «13 de 16»; `03/README.md` 1.2 corrigió «quince reglas» a «dieciséis» sin recontar el reparto de la misma oración. Un control de cambios que declara una corrección que no se hizo es peor que la ausencia de corrección: apaga la próxima revisión.
3. **Dos de los hallazgos ya habían sido reportados y siguen abiertos.** El de `CU-12` §9 estaba señalado y no se corrigió; la mitad del P0 es la continuación directa de `C-04` y `C-05-04`, cerrados en el archivo origen y no en sus consumidores.
4. **Uno de los P1 falsifica un criterio verificable de la guía**: el objetivo del tramo de 30 minutos, que `Rules-UX-UI-DX.md` §6 exige verificable, describe una partición de dieciséis puntos que la superficie no tiene, de modo que reproducirlo bien equivale a fallarlo.

**Condiciones para promover.** Corregir los quince hallazgos, todos por reescritura de recuentos y frases, sin subir versión mayor y sin tocar ninguna decisión. Y una condición de método, que es la que este informe considera la causa raíz: **cada corrección de un recuento debe ir acompañada de la búsqueda de quién más lo cita**, y su resultado —cero consumidores, o la lista de los que se tocaron— debe quedar escrito en el control de cambios. Sin esa condición, la ronda 2 va a encontrar lo mismo en otros seis lugares.

**Nota de alcance para la ronda 2.** Por la emisión tardía, la verificación de cierre debe alcanzar además a los consumidores ya emitidos de estos números en 05 a 11. Verifiqué que `05-Arquitectura-Tecnica/Contratos-REST.md` §3, §4 y §5 tiene los recuentos correctos —quince puntos, diez códigos de respuesta, quince códigos del contrato con `A-04` retirado y no reciclado—, de modo que el hallazgo P0 no se propagó hacia adelante; los demás consumidores no se revisaron y quedan por verificar.

---

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Primera auditoría de Fase B de las categorías 02 y 03 de `GeometriaFactory-Api`, emitida **tardíamente** —después de las fases C a H— y declarándolo en §0 con sus cuatro consecuencias de alcance. Cubre los 21 artefactos vivos contra `Rules-Especificacion-Funcional.md` 4.0 y `Rules-UX-UI-DX.md` del repositorio de origen, el `PRODUCT-INTAKE` 1.26, el `PRODUCT-MANIFEST` 1.3 y las categorías 02 y 03 de `GeometriaFactory-Contracts` y `GeometriaFactory-Application`. Verifica la conformidad con las dos guías criterio por criterio, los cinco apartamientos declarados contra los flags del manifiesto, dieciocho recuentos contados a mano, diecinueve citas abiertas contra su fuente original, las tres reglas de arquitectura, y los once puntos abiertos uno por uno. Levanta **quince hallazgos: un P0, cinco P1, seis P2 y cinco P3**, todos de la familia de los recuentos congelados y de las citas hechas a través de un intermediario, y ninguno sobre una decisión de contrato. Declara siete verificaciones que no se reportan —entre ellas las cinco no-polisemias, que reportar sería un defecto— y cuatro cosas no verificadas. **Dictamen: RECHAZADO**, con condición de método sobre la propagación de correcciones de recuento. |
