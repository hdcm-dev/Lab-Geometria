# Informe de auditoría — B-02-03 · GeometriaFactory-Domain · ronda 3

**Producto:** Fábrica de Geometría
**Fase auditada:** B — categorías `02-Especificacion-Funcional` y `03-UX-UI-DX`
**Proyecto de código:** GeometriaFactory-Domain (`library`)
**Alcance:** los treinta y dos documentos vivos de 02 y 03, los snapshots de `_legacy/2026-08-09/` y `_legacy/2026-08-09-b/`, y la nota de archivado omitido de `03-UX-UI-DX/_legacy/2026-08-09/`
**Motivo de la ronda:** verificación de la resolución del **P0** que la auditoría de `GeometriaFactory-Application` reportó en `B-02-03-GeometriaFactory-Application-r1.md` (hallazgo H-01) y que el orquestador atribuyó a este proyecto de código: la cuenta del administrador nacía `Pendiente` y dejaba la instancia inutilizable en el primer arranque. **No es una re-auditoría de las rondas 1 y 2**, cuyos hallazgos están cerrados.
**Auditor:** Arquitecto de Soluciones + QA Senior — auditor independiente, sin participación en la generación ni en las correcciones auditadas
**Fecha:** 2026-08-09
**Insumos de referencia:** `PRODUCT-INTAKE-Fabrica-De-Geometria.md` 1.3 · `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` 1.1 · `SDD/Docs/00-Contexto/` · `SDD/Docs/01-Necesidades-Negocio/` · `Rules-Especificacion-Funcional.md` §6 · `Rules-UX-UI-DX.md` §6 · `Vocabulario-Rules.md` §9 y §10 · `Master-Prompt.md` §5, §8 y §10

---

## Tabla de contenido

- [1. Resumen ejecutivo](#1-resumen-ejecutivo)
- [2. Verificación de la resolución del P0](#2-verificación-de-la-resolución-del-p0)
  - [2.1 Comprobación 1 — La instancia es arrancable](#21-comprobación-1--la-instancia-es-arrancable)
  - [2.2 Comprobación 2 — El fundamento contra las fuentes](#22-comprobación-2--el-fundamento-contra-las-fuentes)
  - [2.3 Comprobación 3 — Las citas corregidas](#23-comprobación-3--las-citas-corregidas)
  - [2.4 Comprobación 4 — `ESTADO_INICIAL_NO_NEGOCIABLE` con dos causas opuestas](#24-comprobación-4--estado_inicial_no_negociable-con-dos-causas-opuestas)
  - [2.5 Comprobación 5 — Recuento independiente del catálogo](#25-comprobación-5--recuento-independiente-del-catálogo)
- [3. El invariante propuesto y el punto abierto de las fechas](#3-el-invariante-propuesto-y-el-punto-abierto-de-las-fechas)
- [4. Conformidad y estructura, donde el cambio la pudo alterar](#4-conformidad-y-estructura-donde-el-cambio-la-pudo-alterar)
- [5. Gobierno del glosario](#5-gobierno-del-glosario)
- [6. Hallazgos](#6-hallazgos)
- [7. Observación dirigida al orquestador — el archivado omitido de 03](#7-observación-dirigida-al-orquestador--el-archivado-omitido-de-03)
- [8. Veredicto y condiciones para promover](#8-veredicto-y-condiciones-para-promover)

---

## 1. Resumen ejecutivo

**El P0 está resuelto.** El camino completo del primer arranque —configurar, evaluar admisibilidad, entrar, cambiar contraseña— se recorre de punta a punta sin ningún punto de bloqueo, y el fundamento se sostiene contra el intake: ninguna fuente ata el estado inicial `Pendiente` a toda alta, y la etapa `c` exige entrar inmediatamente después de configurar. El recuento del catálogo, rehecho desde cero, da **exactamente 40 condiciones distintas sobre 46 filas de las §6 de los doce casos de uso**, con diferencia de conjuntos **vacía en las dos direcciones** contra el catálogo de 03. El invariante candidato INV-08 está correctamente separado, declarado no vigente y no contado, y el modelo es correcto sin él.

Total de hallazgos: **7** — **P0: 0 · P1: 1 · P2: 1 · P3: 5**. El P1 no es el P0 reabierto: es un agujero residual **de la misma clase** que la corrección no cerró —`CU-02` no impide bloquear la cuenta del administrador, con lo que la instancia vuelve a quedar sin salida por otra vía—. El P2 es la ruptura de cuatro referencias cruzadas producida por la renumeración de §4.2 a §4.3 en el modelo de dominio, una de ellas en la propia `CU-12`.

**Veredicto: APROBADO CON OBSERVACIONES.**

---

## 2. Verificación de la resolución del P0

Las cinco comprobaciones se hicieron por separado y en este orden. Ninguna se apoyó en la declaración de los documentos: cada una se contrastó contra la fuente o se rehizo mecánicamente.

### 2.1 Comprobación 1 — La instancia es arrancable

Se recorrió el guion de la etapa `c` del intake —«configurar el administrador en el primer arranque, **entrar**, cambiar contraseña y salir, persistido» (§15)— paso por paso sobre los contratos del dominio, buscando un punto donde la instancia quede bloqueada.

| Paso del guion | Contrato que lo resuelve | Resultado |
| --- | --- | --- |
| Configurar el administrador | `CU-12` §4, pasos 6 a 9: «El dominio fija el papel en `Administrador`» · «fija el estado de cuenta en **`Habilitado`**, no en `Pendiente`» · «adopta la credencial derivada aportada» | Procede. La ventana es única: sólo mientras no exista ninguna cuenta con ese papel |
| Entrar inmediatamente | `CU-04` §4: comprueba estado `Habilitado` y credencial con valor, y devuelve admisible. `CU-12` CA-02 lo ancla: «El dominio devuelve **admisible**, con 0 motivos» | Procede. No hay motivo `CUENTA_PENDIENTE`, que era el bloqueo del defecto |
| Cambiar la contraseña | `CU-03` FA-03, nuevo: «Entra por el reemplazo de FA-01 y no por la fijación: su credencial ya tiene valor desde CU-12, de modo que el camino de fijación por primera vez le devolvería `CREDENCIAL_YA_FIJADA`» | Procede. `CU-03` FA-01 sólo exige la cuenta `Habilitado` y la credencial vigente verificada, y las dos se cumplen |
| Salir | Fuera del dominio: la sesión la emite `GeometriaFactory-Infrastructure` (`Definicion-Modelo-De-Dominio.md` §7) | No aplica en esta capa, y está declarado |

**Ningún punto queda bloqueado.** Se verificó además que el otro camino no se contamina: `CU-01` FA-01 dejó de constituir cuentas de administrador y remite a `CU-12`, con el código nuevo `PAPEL_DE_ADMINISTRADOR_FUERA_DE_ESTE_CAMINO` y su criterio CA-06, y `CU-01` §7 declara por qué el alumno sí puede nacer `Pendiente`: «eso es correcto en este camino porque ese administrador ya existe: lo constituyó CU-12 en el primer arranque».

**Lo que la comprobación sí encontró, y es hallazgo aparte.** La instancia arranca, pero la **misma condición sin salida** se alcanza por otra transición que la corrección no revisó: `Habilitado` → `Bloqueado` está declarada sin acotar el papel del sujeto, y `CU-02` §6 rechaza únicamente la **baja** del administrador. Ver **H-01 · P1**.

### 2.2 Comprobación 2 — El fundamento contra las fuentes

Se abrió `PRODUCT-INTAKE-Fabrica-De-Geometria.md` 1.3 en lugar de aceptar lo que los documentos declaran.

| Afirmación del proyecto de código | Verificación contra la fuente | Resultado |
| --- | --- | --- |
| «Las fuentes atan el estado inicial `Pendiente` al acto de auto-registro del alumno, no a toda alta de cuenta» (`CU-01` §10, `Definicion-Modelo-De-Dominio.md` §5.1) | El intake declara **dos capacidades separadas**: «F-01 · Configurar la cuenta de administrador en el primer arranque, y sólo mientras no exista ninguna · RF-01, RF-02, RN-01» y «F-02 · Registro de alumno con correo, nombre y apellido, sin elegir contraseña · RF-03, RF-04» (§4). El flujo 1 de §6 atribuye el estado a la persona que **se registra**: «el alumno entra a la aplicación y se registra … El sistema le dice que su cuenta quedó pendiente de autorización». RN-06 se verifica «con una cuenta recién **registrada**» (§4.1) | ✔ Se sostiene |
| Ninguna fuente enuncia un estado inicial uniforme para toda cuenta | Barrido completo del intake sobre `Pendiente`, `Habilitado` y `Bloqueado`: los tres estados se declaran en §17.1.P.5 como conjunto —«el estado de la cuenta (`Pendiente` / `Habilitado` / `Bloqueado`)»— **sin** ninguna afirmación sobre con cuál nace una cuenta. No existe en el intake ninguna tabla de transiciones de cuenta | ✔ Se sostiene: la generalización era del proyecto de código, no de la fuente |
| «El guion de la etapa `c` exige entrar inmediatamente después de configurar» | §15, etapa `c`: «Administrador: alta inicial y sesión — Configurar el administrador en el primer arranque, **entrar**, cambiar contraseña y salir, persistido — F-01, F-05» | ✔ Literal. Con la cuenta `Pendiente` la etapa era **insatisfacible**, porque INV-06 le niega el acceso (§17.1.P.2: «Un alumno en estado `Pendiente` o `Bloqueado` no obtiene token») |
| «`NB-00001` §7 ya lo preveía como caso de uso propio» (`CU-12` §11) | `NB-00001` §7, primera fila: «NB-00001 · CU-01 configurar la cuenta de administrador en el primer arranque · a generar» | ✔ Se sostiene. La discrepancia de número es la que `Especificacion-Funcional.md` §8 punto 1 ya declara: los `CU-XX` son locales al proyecto de código |

**Conclusión: la raíz declarada es correcta.** La capacidad F-01 existía en el intake desde el origen y no tenía caso de uso propio en esta categoría; sobrevivía como flujo alternativo de `CU-01`, que atravesaba el paso que fija `Pendiente`. Emitir `CU-12` es la corrección de la causa, no del síntoma.

**Una salvedad de forma, no de fondo.** `Definicion-Modelo-De-Dominio.md` §5.1 y `CU-01` §10 presentan entre comillas angulares una transición literal —«la transición inicial que declaran es "se registra (RF-03)"»— que **no aparece con esa forma en ninguna fuente accesible**: el intake sólo nombra RF-03 en la columna de origen de F-02, y el archivo `Requerimientos-Funcionales.md` no existe en el árbol. La sustancia es correcta y está probada arriba; la forma de cita, no. Ver **H-06 · P3**.

### 2.3 Comprobación 3 — Las citas corregidas

El P0 de `GeometriaFactory-Application` señalaba que «las dos fuentes que FA-01 invoca como fundamento no lo sostienen: RN-01 §1 dice sólo "Existe **exactamente un** administrador, y su alta sólo es posible mientras no exista ninguno", y INV-05 lo mismo. **Ninguna de las dos dice que su cuenta nazca habilitada**». Se verificó que la atribución se retiró y que no quedó ninguna equivalente.

| Artefacto | Texto vigente | ¿Atribuye el estado inicial a RN-01 o a INV-05? |
| --- | --- | --- |
| `CU-01` §9 | «**INV-05 no se cita como fundamento de este camino**: dice que existe exactamente un administrador y que su alta sólo es posible mientras no exista ninguno, y **no dice nada sobre el estado inicial** de una cuenta» | **No.** Retirada explícitamente |
| `CU-01` §9, fila de reglas | «RN-01 **sólo** en cuanto al conjunto cerrado de dos papeles: la unicidad del administrador y su ventana de alta se ejercen en CU-12, no acá» | **No.** Acotada |
| `RN-01` §3 | «La regla fija la unicidad y la ventana de alta —"sólo mientras no exista ninguno"—; **no fija el estado con el que esa cuenta nace**, que lo declara la máquina de estados de `Definicion-Modelo-De-Dominio.md` §5.1» | **No.** Declarado en la propia regla |
| `CU-12` §10 | «**RN-01 e INV-05 no dicen nada sobre el estado inicial.** … Se citan acá por lo que sí declaran —la unicidad y la ventana de alta—, no como fundamento del estado» | **No.** El caso de uso nuevo se autolimita |
| `DX-Error-Messages.md` §6.2, tabla de orígenes | «`ESTADO_INICIAL_NO_NEGOCIABLE` … **Ninguna de las once reglas enuncia con qué estado nace una cuenta.** La cita de INV-05 como fundamento se retiró en 02» | **No.** Y su columna de invariante queda en «— (INV-08 candidato)» |
| `Definicion-Modelo-De-Dominio.md` §4.1 | INV-05 conserva el enunciado del intake, palabra por palabra, y su columna de ejercicio pasa a «CU-12, CU-02» | **No.** El enunciado no se tocó |

**Barrido de residuos.** Se recorrieron los treinta y dos documentos vivos buscando cualquier otra cita que sostenga sobre una fuente algo que la fuente no dice, en el perímetro que el cambio tocó. **No quedó ninguna.** La única imprecisión de citación detectada es la de §2.2 —la transición literal atribuida a las fuentes— y una remisión de sección equivocada, que es H-02.

### 2.4 Comprobación 4 — `ESTADO_INICIAL_NO_NEGOCIABLE` con dos causas opuestas

**Las dos causas existen y son efectivamente opuestas**, verificadas contra las §6 de origen y no contra el catálogo:

| Caso de uso | Causa declarada en su §6 | Estado que impone |
| --- | --- | --- |
| `CU-01` | «Se solicita constituir la cuenta del **auto-registro** en un estado distinto de `Pendiente`» | `Pendiente` |
| `CU-12` | «Se solicita constituirla en un estado distinto de `Habilitado`» | `Habilitado` |

**La forma elegida es comprensible y no se lee como inconsistencia.** El catálogo la declara como excepción antes de que el lector la encuentre, en `DX-Error-Messages.md` §1.4, con el enunciado que la resuelve: «No es una inconsistencia y no hay que unificarlo: el enunciado del código es "el estado inicial de este camino no se elige", y cuál es ese estado lo fija el camino». Las dos filas de §3.1 y §3.12 llevan **remisión mutua explícita** —«**Mismo identificador, causa opuesta en CU-12** … ver §3.12 y §1.4» y su simétrica—, y §3 lo anticipa en su preámbulo. El tratamiento es correcto y es el que evita que un implementador unifique los dos caminos y reintroduzca el defecto.

**Lo que la comprobación sí objeta** es la afirmación de exclusividad que acompaña a la forma. §1.4 dice: «Por eso es el único código del catálogo que lleva **fila completa en dos subsecciones de §3** … Los otros cuatro códigos declarados en más de un caso de uso conservan la misma causa en todos y siguen con **entrada única**». Verificado contra §3: los otros cuatro también llevan fila completa en cada subsección donde aparecen —`DATO_OBLIGATORIO_AUSENTE` en §3.1, §3.5 y §3.12, con mensaje propio en §3.5 («Falta el nombre o la fecha del trabajo»); `UNICIDAD_DE_CORREO_NO_VERIFICADA` en §3.1 y §3.12; `TRANSICION_DESDE_ESTADO_TERMINAL` en §3.8 y §3.10; `OPERACION_DESCONOCIDA` en §3.9 y §3.11—. Lo que los distingue no es la fila, es la **causa**. Ver **H-05 · P3**: es imprecisión del enunciado, no defecto de la solución.

### 2.5 Comprobación 5 — Recuento independiente del catálogo

Se extrajeron mecánicamente los identificadores de la §6 de los **doce** casos de uso y se compararon contra §3 y §6.2 de `DX-Error-Messages.md`, sin usar los recuentos declarados.

| Caso de uso | Filas en su §6 |
| --- | --- |
| CU-01 | 5 |
| CU-02 | 3 |
| CU-03 | 4 |
| CU-04 | 3 |
| CU-05 | 4 |
| CU-06 | 4 |
| CU-07 | 4 |
| CU-08 | 4 |
| CU-09 | 3 |
| CU-10 | 4 |
| CU-11 | 3 |
| CU-12 | 5 |
| **Total de filas declaradas** | **46** |

Condiciones declaradas en más de un caso de uso: `DATO_OBLIGATORIO_AUSENTE` en 3 (CU-01, CU-05, CU-12) → 2 filas excedentes; `UNICIDAD_DE_CORREO_NO_VERIFICADA`, `ESTADO_INICIAL_NO_NEGOCIABLE`, `TRANSICION_DESDE_ESTADO_TERMINAL` y `OPERACION_DESCONOCIDA` en 2 cada una → 4 filas excedentes. **Total de filas excedentes: 6.**

**46 − 6 = 40 condiciones distintas.** Coincide con lo declarado en §6.1.

| Verificación de conjuntos | Resultado |
| --- | --- |
| Condiciones distintas en las §6 de los doce CU | **40** |
| Entradas de la tabla de cobertura de §6.2 | **40** |
| En las §6 y **no** en el catálogo | **∅** (ninguna) |
| En el catálogo y **no** en las §6 | **∅** (ninguna) |

**Diferencia de conjuntos vacía en las dos direcciones.** Ninguna condición inventada y ninguna faltante.

Se verificaron además los recuentos por categoría de §2.1, que el cambio actualizó: **entrada inválida 18 · recurso ausente 3 · conflicto de estado 14 · conflicto de facultad 5 = 40**. Los cuatro cuadran cuando se descuentan las repeticiones (por ejemplo, «conflicto de estado» tiene 15 filas en §3 y 14 condiciones distintas, porque `TRANSICION_DESDE_ESTADO_TERMINAL` aparece dos veces). Y se verificó que los recuentos derivados en 03 se movieron todos con el catálogo: `DX-Developer-Experience.md` §7 métrica de cobertura «40 de 40», `README.md` §2 «40 condiciones … sobre doce casos de uso», `Glosario-UX.md` §2 «de 37 a 40».

---

## 3. El invariante propuesto y el punto abierto de las fechas

### 3.1 INV-08 — propuesto y no adoptado

**Está claramente separado y no se lo cuenta entre los vigentes.** La separación es estructural y no una nota al pie: `Definicion-Modelo-De-Dominio.md` §4 se parte en **§4.1 «Los siete invariantes»**, que sigue con las siete filas transcriptas del intake, y **§4.2 «Invariante candidato, propuesto y no adoptado»**, que abre con la frase que fija su estatuto: «**INV-08 no existe en el intake.** Se propone acá y **no se cuenta entre los invariantes vigentes**, que siguen siendo los siete de §4.1». Su fila lleva columna de estado con el valor «**Propuesto, no vigente.** Requiere decisión del Product Owner y su incorporación a `PRODUCT-INTAKE` §17.1.P.2».

Se verificó que **ningún artefacto lo cuenta**: `Especificacion-Funcional.md` §4 mantiene «los siete invariantes» y remite al candidato como tal; `DX-Error-Messages.md` §6.2 lo anota entre paréntesis en dos filas y declara «**no lo cuenta**: los invariantes vigentes siguen siendo los siete de §4.1»; `DX-Developer-Experience.md` §8 declara «invariantes INV-01 a INV-07»; `Guia-Onboarding-Developer.md` §7.1 conserva el título «Once reglas, siete invariantes» y su control de cambios registra que «la §7 no cambia … el invariante candidato **INV-08 no es vigente**». Está registrado como punto abierto con destinatario en `Especificacion-Funcional.md` §9, tercera fila.

**¿Es correcto el modelo sin él?** Sí, y por el motivo que el propio documento da: «la propiedad se sostiene por las reglas de los casos de uso —CU-01 y CU-12 fijan cada uno su estado inicial y rechazan el del otro— y por la máquina de estados de §5.1». Verificado punto por punto: `CU-01` rechaza el estado ajeno (`ESTADO_INICIAL_NO_NEGOCIABLE`, CA-05) y el papel ajeno (`PAPEL_DE_ADMINISTRADOR_FUERA_DE_ESTE_CAMINO`, CA-06); `CU-12` rechaza el estado ajeno (CA-05) y la segunda configuración (`ADMINISTRADOR_YA_CONFIGURADO`, CA-03); §5.1 declara las dos transiciones iniciales y su lista de inadmisibles cierra la simetría: «**ninguna cuenta de alumno nace `Habilitado`**, como ninguna cuenta de administrador nace `Pendiente`». La propiedad queda verificable operación por operación, que es exactamente lo que el documento afirma. **La afirmación se sostiene y no es hallazgo.**

**Lo único que se objeta es una remisión.** `CU-12` §9 envía al lector a «`Definicion-Modelo-De-Dominio.md` **§4.3**» para encontrar INV-08, y §4.3 es la correspondencia entre reglas e invariantes: el candidato está en §4.2. Ver **H-02 · P2**.

### 3.2 El punto abierto de las fechas del trabajo

Se verificó en `Especificacion-Funcional.md` §9. **La omisión no está declarada allí.** §9 registra tres puntos abiertos: nombres de tipos y espacios de nombres, criterio de comparación de dos correos, y la adopción de INV-08. **La ausencia de fecha de creación y de última modificación del trabajo no figura**, ni en §9, ni en §7 «Omisiones declaradas», ni en `Definicion-Modelo-De-Dominio.md` §2.2, cuya tabla de atributos del trabajo enumera identificador, dueño, nombre, fecha, descripción, texto original, estado, conjunto de piezas, cantidad de figuras, observaciones y comentario, **sin declarar que las dos marcas temporales de auditoría queden fuera y sin remitir a quién lo resuelve**.

Lo que sí se verificó, y es lo que evita que esto sea un defecto de fondo: **el modelo no las rellenó**. No hay ningún atributo inventado, ninguna semántica de auditoría improvisada y ninguna afirmación de que el dominio las provea —al contrario, §2.2 declara que la fecha del trabajo «es dato del alumno, no del reloj del sistema» y §7 ubica la lectura del reloj en `GeometriaFactory-Application`—. La omisión es correcta; lo que falta es **declararla**. Ver **H-03 · P3**. Consistente con la instrucción de esta ronda, **no se imputa como defecto del proyecto de código**: es un punto elevado al Product Owner que este proyecto de código debe registrar como abierto en lugar de dejarlo tácito.

---

## 4. Conformidad y estructura, donde el cambio la pudo alterar

Sólo se verificó el perímetro que el cambio tocó. El resto lo verificaron las rondas 1 y 2.

### 4.1 `Rules-Especificacion-Funcional.md` §6

| Criterio alcanzado por el cambio | Verificación | Resultado |
| --- | --- | --- |
| La cantidad de CU cumple el mínimo del tipo D8 | Doce archivos `CU-XX`, sobre un mínimo de cinco para `library`. El apartamiento de la guía orientativa «library con menos de diez» está declarado con su causa en `Especificacion-Funcional.md` §6, actualizado al recuento nuevo | ✔ |
| Cada CU contiene las once secciones obligatorias de §4.2 | `CU-12` las tiene todas, en orden, más §17 «Compatibilidad de la superficie pública», que es la sección opcional que §4.3 asigna a `library`. Tabla de contenido presente | ✔ |
| Cada CU declara trazabilidad NB→CU→US y al menos tres criterios Given/When/Then con valores concretos | `CU-12` §9 declara las cinco dimensiones y §8 trae **cinco** criterios con valores concretos (correo `docente@example.com`, credencial de 64 caracteres, fecha 2026-08-09) | ✔ |
| Cada RN contiene las siete secciones obligatorias y enumera CU afectados explícitos | `RN-01` 1.2 y `RN-02` 1.1, las dos tocadas, conservan las siete y actualizan §5 con `CU-12` | ✔ |
| Existe `Glosario-Funcional.md` con sus cinco secciones y tabla no vacía | 1.2, quince términos, con «camino de alta» dado de alta | ✔ |
| Todo término del dominio en más de un artefacto está en el glosario | «Camino de alta» aparece en `Definicion-Modelo-De-Dominio.md`, `CU-01`, `CU-03`, `CU-12`, `RN-01` y `RN-02`, y está declarado con esa lista | ✔ |
| Un archivo por nombre lógico; las versiones superadas en `_legacy/` con sufijo | Verificado sobre los treinta y dos vivos y los veintiocho snapshots. Ningún vivo lleva sufijo | ✔ |
| Numeración contigua de CU | `CU-01` a `CU-12`, sin huecos | ✔ |

### 4.2 `Rules-UX-UI-DX.md` §6

| Criterio alcanzado por el cambio | Verificación | Resultado |
| --- | --- | --- |
| Variante declarada en la cabecera y coherente con el tipo D8 | Los cinco documentos de 03 declaran variante DX; `tiene_ui_final` == false | ✔ |
| `DX-Developer-Experience.md` con sus nueve secciones, Diátaxis y onboarding por tramos | Conserva la estructura; el cambio tocó §1.2, §1.3, §5, §7 y §8, sin desplazar ninguna obligatoria | ✔ |
| Cada `dx-` doc presenta un quick-start verificable | `DX-Error-Messages.md` §6.3 lo declara **no aplicable con su motivo** (artefacto del modo *reference*), remitiendo al quick-start único de `DX-Developer-Experience.md` §3. Es la formulación que r1 ya validó y el cambio no la alteró | ✔ |
| Cada artefacto declara trazabilidad upstream y downstream | Las cinco cabeceras incorporan «los **doce** casos de uso CU-01 a CU-12» y, donde corresponde, «§4.2 (invariante candidato INV-08, propuesto y no vigente)» | ✔ |
| `Glosario-UX.md` no duplica términos de 02 con semántica distinta | §4 **referencia** «camino de alta» apuntando a `Glosario-Funcional.md` §2 y no lo redefine; su control de cambios lo declara: «Ningún término de §2 se agrega ni se quita: el vocabulario nuevo es del dominio y pertenece a 02» | ✔ |
| Extensiones de primer arranque, configuración, operador único e identidad de versión | `DX-Developer-Experience.md` §8 las declara N/A **una por una y con motivo propio** —«Primer arranque aplicado · N/A. El dominio no se despliega por instancia»—, que sigue siendo cierto después de `CU-12`: el predicado de aprovisionamiento vive acá como condición, y la superficie que lo recoge es de `GeometriaFactory-Web` | ✔ |

### 4.3 Forma y versionado

| Comprobación | Resultado |
| --- | --- |
| En 02, los tocados suben a 1.2, y a 1.1 los que habían nacido en 1.0 | ✔ `Definicion-Modelo-De-Dominio.md`, `Especificacion-Funcional.md`, `Glosario-Funcional.md`, `README.md`, `CU-01`, `CU-03` y `RN-01` en **1.2**; `RN-02` en **1.1** |
| `CU-12` nace en 1.0 | ✔ 1.0, estado `Propuesto`, fecha 2026-08-09, con control de cambios de emisión inicial que declara el origen en el P0 |
| Los no tocados conservan su versión | ✔ `CU-02`, `CU-04` a `CU-09`, `RN-03` a `RN-09` en 1.1; `CU-10`, `CU-11`, `RN-06`, `RN-10`, `RN-11` en 1.0 |
| En 03, los cinco suben a 1.1 | ✔ `DX-Developer-Experience.md`, `DX-Error-Messages.md`, `Glosario-UX.md`, `Guia-Onboarding-Developer.md` y `README.md` |
| Snapshots de 02 en `_legacy/2026-08-09-b/` sólo para los que cambiaron | ✔ Ocho snapshots, exactamente los ocho documentos que cambiaron, en las tres carpetas. **Ninguno falta y ninguno sobra**: la ausencia de los no tocados es correcta y no se levanta |
| Todos los snapshots del producto llevan bloque de archivado con estado `Superado` | ✔ Verificado sobre los veintiocho snapshots de este proyecto de código: los veintiocho abren con «**Artefacto archivado — estado `Superado`**», su versión preservada, la fecha de archivado y el puntero al vigente |
| D4 · ningún archivo vivo lleva sufijo de versión | ✔ |
| D9 · alcanza al estado del sistema, y acá no hay sistema construido | n/a, salvo por la nota de archivado de §7, donde sí se aplicó |

---

## 5. Gobierno del glosario

Verificado contra `Vocabulario-Rules.md` §10 y su criterio negativo, **sólo sobre el vocabulario que el cambio movió**.

| Criterio de §10 | Resultado |
| --- | --- |
| Todo término que la fase acuña y aparece en más de un artefacto está en el glosario de su categoría | **Cumple.** El único término nuevo es **«camino de alta»**, declarado en `Glosario-Funcional.md` §2 con su definición operativa y su lista de artefactos, y referenciado —no redefinido— en `Glosario-UX.md` §4 |
| Todo término con más de un referente tiene entrada o forma calificada donde colisiona | **Cumple.** «Camino de alta» tiene **un solo referente** con dos instancias enumeradas; no es polisemia. `Pendiente` conserva su entrada de §3.3 con sus dos referentes y sus excepciones |
| Ninguna forma desnuda de una familia calificada queda sin resolver | **Cumple.** Barrido de todas las ocurrencias de `Pendiente` en los ocho documentos de 02 tocados y en los cinco de 03: **cero** formas desnudas fuera de las tres excepciones declaradas. El texto nuevo de `CU-12`, de `DX-Error-Messages.md` §1.4 y §3.12 y de `Guia-Onboarding-Developer.md` §3.4 califica sin excepción —«cuenta `Pendiente`», «trabajo en estado `Pendiente`»— |
| Referenciar y no redefinir entre las tres capas | **Cumple.** `Glosario-UX.md` §4 apunta a `Glosario-Funcional.md` §2 para «camino de alta»; ninguna entrada de §2 de ninguno de los dos glosarios pisa una definición aguas arriba |
| **Criterio negativo:** ninguna polisemia de contextos disjuntos se reporta como defecto | **Este informe no reporta ninguna.** |

**Polisemias evaluadas y descartadas en esta ronda.** Se enumeran porque el criterio negativo obliga a mostrar el trabajo, no a levantarlas:

| Término | Referentes considerados | Por qué **no** es hallazgo |
| --- | --- | --- |
| **Camino de alta** | Auto-registro del alumno / configuración del administrador | Un solo referente con dos instancias, enumeradas en la propia entrada. Calificarlo sería el falso positivo típico |
| **Configuración** | El acto de `CU-12` / la «configuración dirigida por esquema» de las extensiones de `Rules-UX-UI-DX.md` | **Contextos disjuntos.** El segundo aparece únicamente en las tablas de trazabilidad del artefacto, declarado N/A, y nunca en prosa de dominio |
| **Primer arranque** | El momento en que se configura el administrador / la extensión de UX de primer arranque | **Contextos disjuntos**, y el segundo se declara no aplicable con motivo propio en `DX-Developer-Experience.md` §8. No colisionan en la lectura |
| **Estado inicial** | El de la cuenta según el camino / el estado inicial del trabajo (`Borrador`) | **No colisionan**: los dos van siempre con su sujeto nombrado en la misma oración, y las dos máquinas viven en subsecciones distintas (§5.1 y §5.2) |
| **`Pendiente`** en enumeraciones de conjunto cerrado, en filas de tabla de transición y en identificadores literales | — | Las **tres excepciones declaradas** por el proyecto de código y admitidas por `Vocabulario-Rules.md` §9.1. Exigir la calificación sería el falso positivo que el framework tipifica |

**No se reportan** las polisemias que r1 evaluó y descartó —observación, comentario, `Pendiente` en sus tres excepciones, estado, pieza desnuda, mensaje, guarda, rechazo, motivo de resultado— ni la de «rol», que r1 levantó como P3-10 y r2 cerró.

---

## 6. Hallazgos

### H-01 · P1 · Nada impide bloquear la cuenta del administrador, y la instancia vuelve a quedar sin salida

**Archivos:** `.../02-Especificacion-Funcional/Casos-De-Uso/CU-02-Gobernar-El-Ciclo-De-Vida-De-La-Cuenta.md` (§5, §6) · `.../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md` (§5.1)
**Secciones:** `CU-02` §6, tabla de excepciones · `Definicion-Modelo-De-Dominio.md` §5.1, tabla de transiciones y párrafo «Por qué esta distinción no es un detalle»

**Evidencia.** `Definicion-Modelo-De-Dominio.md` §5.1, reescrita por esta corrección, declara la transición sin acotar sobre qué cuenta se aplica:

> «| `Habilitado` | `Bloqueado` | El administrador | Acto explícito |»

Y su párrafo de cierre razona exactamente sobre la condición sin salida, pero **sólo para el nacimiento**:

> «Si la cuenta del administrador naciera `Pendiente`, la única transición que la sacaría de ahí es que un administrador la habilite, y por INV-06 ella misma no obtendría acceso: no habría ninguna cuenta capaz de habilitarla y la instancia quedaría inutilizable en el primer arranque, sin salida.»

El mismo razonamiento se aplica palabra por palabra a `Habilitado` → `Bloqueado` sobre la cuenta del administrador, y **ningún artefacto lo cierra**. `CU-02` §6 rechaza una sola operación por papel:

> «| `CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA` | Se solicita dar de baja la cuenta con papel `Administrador` | Rechaza la operación: la instancia tiene un único administrador y quedaría sin él (INV-05, RN-01) |»

`CU-02` §3 admite como precondición cualquier cuenta cuyo estado esté en el conjunto, sin distinguir papel, y §4 paso 3 sólo consulta la tabla de transiciones, que declara `Habilitado` → `Bloqueado` como admitida. `CU-12` FA-03 anticipa el problema al **constituir** —«Se solicita constituir la cuenta de administrador con el estado `Pendiente` o `Bloqueado` … Es un estado terminal de hecho, sin salida»— pero no alcanza a la transición posterior.

**Por qué es P1 y no P0.** La instancia **es arrancable**: el camino que el P0 bloqueaba está resuelto y verificado en §2.1. Esto es un agujero residual de la misma clase que la corrección no barrió: exige un acto explícito del propio administrador para alcanzarse, de modo que no es un defecto de arranque sino de recuperabilidad. Pero el resultado es idéntico al del P0 —cuenta sin acceso por INV-06, sin ninguna otra capaz de rehabilitarla— y la propia justificación de `RN-01` para prohibir la baja («quedaría sin él») lo implica sin declararlo.

**Recomendación.** Cerrar la transición por papel, no por estado, con una decisión declarada en `CU-02`: agregar una excepción del tipo `CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BLOQUEO` con su criterio de aceptación, y acotar en `Definicion-Modelo-De-Dominio.md` §5.1 la fila `Habilitado` → `Bloqueado` al **sujeto alumno**, como ya se hizo con `Pendiente` → `Habilitado` («No hay habilitación automática **de una cuenta de alumno**»). Si se adopta, el catálogo de 03 pasa de 40 a 41 condiciones y hay que actualizar §2.1, §6.1, §6.2 y las métricas derivadas. Alternativamente, si el Product Owner decide que el bloqueo del administrador es admisible, declararlo con la vía de recuperación que lo saque de ahí. Lo que no puede quedar es el silencio.

---

### H-02 · P2 · La renumeración de §4.2 a §4.3 rompió cuatro referencias cruzadas, una de ellas en `CU-12`

**Archivos:** `.../02-Especificacion-Funcional/Casos-De-Uso/CU-12-Configurar-La-Cuenta-De-Administrador.md` · `.../03-UX-UI-DX/DX-Developer-Experience.md` · `.../03-UX-UI-DX/Guia-Onboarding-Developer.md`
**Secciones:** `CU-12` §9 · `DX-Developer-Experience.md` §1.2 y §2 · `Guia-Onboarding-Developer.md` §7.1

**Evidencia.** El cambio insertó §4.2 «Invariante candidato» y desplazó la correspondencia regla ↔ invariante a §4.3, como su propio control de cambios declara: «**§4.2 nueva** propone el invariante candidato **INV-08** … la correspondencia con las reglas **pasa a §4.3**». Cuatro remisiones quedaron apuntando al número viejo o al equivocado:

| Documento | Texto vigente | A dónde apunta hoy | A dónde debería |
| --- | --- | --- | --- |
| `CU-12` §9 | «Ver además el **invariante candidato INV-08** propuesto en `Definicion-Modelo-De-Dominio.md` **§4.3**» | A la correspondencia con las once reglas | **§4.2** |
| `DX-Developer-Experience.md` §1.2 | «sobre la correspondencia que declara `Definicion-Modelo-De-Dominio.md` **§4.2**» | Al invariante candidato | **§4.3** |
| `DX-Developer-Experience.md` §2 | «coincidiendo con `Definicion-Modelo-De-Dominio.md` **§4.2**» (clasificación de RN-07, RN-08, RN-09 y RN-11 como reglas sin invariante) | Al invariante candidato | **§4.3** |
| `Guia-Onboarding-Developer.md` §7.1 | «La correspondencia es la de `Definicion-Modelo-De-Dominio.md` **§4.2** y se transcribe acá porque es el corazón de este tramo» | Al invariante candidato | **§4.3** |

Las tres últimas contradicen además a la cabecera de su propio documento, que cita correctamente «§4.2 (invariante candidato INV-08, propuesto y no vigente), §4.3».

**Por qué es P2.** No hay error de sustancia y ningún lector queda con información falsa, pero las cuatro remisiones fallan justamente en el eje sobre el que se abrió esta ronda: la de `CU-12` desvía al lector que busca INV-08 —el objeto del punto abierto elevado al Product Owner— hacia una tabla que no lo contiene. Es el mismo tipo de defecto que r2 cerró como N-04 en `Guia-Onboarding-Developer.md`, reaparecido por una renumeración nueva.

**Recomendación.** Corregir las cuatro remisiones y barrer el árbol vivo buscando cualquier otra cita a subsecciones de §4 de `Definicion-Modelo-De-Dominio.md`, como r2 documentó haber hecho con la renumeración anterior.

---

### H-03 · P3 · Las fechas de creación y de última modificación del trabajo no están declaradas como punto abierto

**Archivos:** `.../02-Especificacion-Funcional/Especificacion-Funcional.md` (§7 y §9) · `.../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md` (§2.2)

**Evidencia.** `Especificacion-Funcional.md` §9 «Puntos abiertos» declara tres, y ninguno es éste:

> «| Nombres de tipos y de espacios de nombres | … | | Criterio de comparación de dos correos | … | | **Adopción del invariante candidato INV-08** | … |»

`Definicion-Modelo-De-Dominio.md` §2.2 enumera los once atributos del trabajo sin declarar que las marcas temporales de auditoría queden fuera. El bloque técnico de la capa de casos de uso las supone, de modo que hay una asimetría entre dos proyectos de código que ninguna de las dos categorías registra.

**Por qué es P3 y por qué no se imputa como defecto de fondo.** El modelo **no las rellenó**, que es lo correcto: no inventó atributos ni semántica de auditoría, y §7 ubica explícitamente la lectura del reloj fuera del dominio. Lo que falta es la declaración. `Especificacion-Funcional.md` §7 y §9 son exactamente los lugares donde el framework pide que una omisión de este tipo aparezca con su motivo y su destinatario, en lugar de quedar tácita.

**Recomendación.** Sumar una fila a §9 con el punto abierto, su destinatario —Product Owner, con incorporación al `PRODUCT-INTAKE`— y la constancia de que el dominio no las provee porque no lee el reloj. No agregar atributos al modelo mientras la decisión no exista.

---

### H-04 · P3 · `CU-01` §6 anuncia cuatro errores sobre una tabla de cinco

**Archivo:** `.../02-Especificacion-Funcional/Casos-De-Uso/CU-01-Registrar-El-Alta-De-Un-Alumno.md`
**Sección:** §6, párrafo de cierre

**Evidencia.** La tabla de §6 trae cinco filas —`DATO_OBLIGATORIO_AUSENTE`, `UNICIDAD_DE_CORREO_NO_VERIFICADA`, `CREDENCIAL_NO_ADMITIDA_EN_EL_ALTA`, `ESTADO_INICIAL_NO_NEGOCIABLE` y `PAPEL_DE_ADMINISTRADOR_FUERA_DE_ESTE_CAMINO`—, y el párrafo que la cierra quedó con el recuento anterior:

> «**Los cuatro errores** terminan de forma controlada: el dominio no construye la entidad y devuelve la causa al consumidor»

El propio documento se contradice dos secciones más abajo, en §9: «Pruebas unitarias puras sin dobles sobre la constitución y sobre **los cinco rechazos**». `CU-12`, emitido con el mismo cambio, sí cuadra: «Los cinco rechazos terminan de forma controlada».

**Por qué es P3.** No altera el catálogo —el recuento de 40 se hace sobre las filas, no sobre el párrafo— ni induce a omitir una guarda, porque §9 y `DX-Error-Messages.md` §3.1 declaran cinco. Es residuo del alta del código nuevo.

**Recomendación.** «Los cinco errores».

---

### H-05 · P3 · La exclusividad de la fila doble no es cierta: otros cuatro códigos también la llevan

**Archivo:** `.../03-UX-UI-DX/DX-Error-Messages.md`
**Secciones:** §1.4 y preámbulo de §3

**Evidencia.** §1.4 afirma:

> «Por eso es el único código del catálogo que lleva **fila completa en dos subsecciones de §3** en lugar de una entrada única con nota, y las dos filas se leen juntas. **Los otros cuatro códigos declarados en más de un caso de uso conservan la misma causa en todos y siguen con entrada única.**»

Y §3 lo repite: «Cuatro de ellas … llevan **una sola entrada**, en el caso de uso donde se declaran primero, con la nota de sus otras apariciones». Verificado contra §3: los cuatro llevan fila completa en cada subsección donde aparecen, con mensaje, causa y acción propios. `DATO_OBLIGATORIO_AUSENTE` en §3.5 tiene incluso mensaje distinto del de §3.1 —«Falta el nombre o la fecha del trabajo» frente a «Falta un dato obligatorio del alta: correo, nombre o apellido»— y se anota a sí mismo como «Entrada única en §3.1; **ésta es su segunda declaración**», que es la formulación que revela la contradicción.

**Por qué es P3.** Lo que la afirmación quiere decir —que `ESTADO_INICIAL_NO_NEGOCIABLE` es el único cuya **causa** difiere según el caso de uso, y por eso el único con dos filas **autónomas y de lectura obligatoriamente conjunta**— es correcto, está bien fundado y resuelve el riesgo real. El defecto es del enunciado, que elige el criterio equivocado para distinguirlo: la fila, y no la causa. Un lector que audite el catálogo contando filas encuentra la contradicción de inmediato.

**Recomendación.** Reescribir el distintivo sobre la causa: «es el único código cuya causa depende del caso de uso, y por eso el único cuyas dos filas se leen juntas; los otros cuatro repiten fila en cada subsección conservando la misma causa, con la entrada canónica señalada».

---

### H-06 · P3 · Una transición se cita entre comillas angulares sin fuente localizable

**Archivos:** `.../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md` (§5.1) · `.../02-Especificacion-Funcional/Casos-De-Uso/CU-01-Registrar-El-Alta-De-Un-Alumno.md` (§10)

**Evidencia.** `Definicion-Modelo-De-Dominio.md` §5.1:

> «Las fuentes atan el estado inicial `Pendiente` al **auto-registro del alumno** —la transición inicial que declaran es «**se registra (RF-03)**»—»

Y `CU-01` §10 repite la construcción: «la transición inicial de la máquina de estados de cuenta es "se registra (RF-03)", y RF-03 es el auto-registro del alumno». La cadena accesible no contiene esa transición con esa forma: `PRODUCT-INTAKE` 1.3 nombra RF-03 **una sola vez**, en la columna de origen de la capacidad F-02 de §4, y **no tiene ninguna tabla ni diagrama de transiciones de la cuenta**; el archivo `Requerimientos-Funcionales.md` que declara la serie RF no existe en el árbol del producto.

**Por qué es P3.** La sustancia es verdadera y está probada por otras vías en §2.2 de este informe: F-01 y F-02 son capacidades separadas, el flujo 1 de §6 atribuye el estado a quien se registra y RN-06 se verifica sobre «una cuenta recién registrada». Nada de la conclusión depende de la cita. Pero la forma con comillas angulares presenta como transcripción literal de una fuente algo que el auditor no puede localizar, y en una ronda cuyo objeto es precisamente que ninguna cita sostenga lo que su fuente no dice, la asimetría se nota.

**Recomendación.** Reemplazar la cita literal por la evidencia verificable: `PRODUCT-INTAKE` §4 (F-01 y F-02, con sus orígenes RF-01/RF-02 y RF-03), §6 flujo 1 y §15 etapa `c`. Si la transición literal existe en `Requerimientos-Funcionales.md`, declarar la ruta del archivo.

---

### H-07 · P3 · Tres recuentos y un orden quedaron en el estado anterior al cambio

**Archivos:** `.../02-Especificacion-Funcional/Glosario-Funcional.md` (§3.4) · `.../02-Especificacion-Funcional/README.md` (§4) · `.../02-Especificacion-Funcional/Casos-De-Uso/CU-03-Fijar-Y-Reemplazar-La-Credencial-Derivada.md` (§5)

**Evidencia.** El cambio actualizó a «doce» los recuentos de casi todo el árbol —dieciocho ocurrencias verificadas— y dejó tres atrás, en prosa viva y no en filas de control de cambios, que sí deben conservar el número histórico:

| Ubicación | Texto vigente |
| --- | --- |
| `Glosario-Funcional.md` §3.4, párrafo de apertura | «Los dos referentes conviven en la misma tabla y hasta en la misma celda —la tabla de actores de **los once casos de uso**—» |
| `Glosario-Funcional.md` §3.4, tabla, columna «Dónde aparece acá» | «§2 «Actores» de **los once casos de uso**» |
| `README.md` §4, orden de lectura, punto 2 | «**Los once casos de uso** se leen sobre él, y en particular §5.2» |

El mismo `Glosario-Funcional.md` dice «Los doce CU» dos filas más arriba, en §2, y el mismo `README.md` titula su §2 «Los doce casos de uso».

Se suma un defecto de orden, del mismo origen: `CU-03` §5 quedó con sus flujos alternativos en la secuencia **FA-01, FA-03, FA-02**, porque FA-03 se insertó entre los dos existentes en lugar de al final.

**Por qué es P3.** Ninguno induce a error sobre el contenido —las tablas de actores existen en los doce y el orden de las filas no altera el disparador ni el punto de retorno de ninguna—, pero son inconsistencias internas visibles dentro del mismo documento.

**Recomendación.** «Doce» en los tres lugares, y reordenar la tabla de `CU-03` §5 como FA-01, FA-02, FA-03 conservando los identificadores, que ya están citados por `CU-12` FA-01.

---

## 7. Observación dirigida al orquestador — el archivado omitido de 03

**No es hallazgo del proyecto de código y no se imputa a los subagentes.** Se registra como observación dirigida al **orquestador SDD**, que es quien la nota misma señala como responsable.

**Verificación de la veracidad de la nota**, afirmación por afirmación, sobre `03-UX-UI-DX/_legacy/2026-08-09/NOTA-Archivado-Omitido.md`:

| Afirmación de la nota | Verificación independiente | Resultado |
| --- | --- | --- |
| «Esta carpeta **no contiene los snapshots** de la versión 1.0 de los cinco documentos» | `03-UX-UI-DX/_legacy/2026-08-09/` contiene **un solo archivo**: la propia nota. No hay ningún `-v1.0.md` | ✔ Veraz |
| «El estado 1.0 de esos cinco archivos se perdió. No es recuperable: la carpeta `SDD/` no está bajo seguimiento de control de versiones» | `Lab-Geometria/` no es repositorio git y no existe ninguna otra copia en el árbol. Los cinco vivos están en 1.1 y su contenido 1.0 no es reconstruible desde ningún artefacto | ✔ Veraz |
| «**Del orquestador, no del subagente.** `Master-Prompt.md` §8 asigna el snapshot al orquestador y lo declara **anterior** a la construcción del despacho» | Consistente con el resto del producto: los veintiocho snapshots de 02 y los de `00-Contexto`, `01-Necesidades-Negocio` y `GeometriaFactory-Contracts` llevan en su bloque «tomada … **por el orquestador SDD** antes de que la versión vigente la superara» | ✔ Veraz |
| «El subagente editó los cinco documentos, informó correctamente que ya lo había hecho y **no archivó nada por su cuenta**, que es lo que su despacho le indicaba» | Los cinco documentos de 03 llevan fila de control de cambios 1.1 completa, con alcance por sección y cita del informe que originó la corrección. No hay ningún archivado parcial ni ningún snapshot espurio | ✔ Veraz. El trabajo del subagente está completo y correctamente registrado |
| «Fabricar los snapshots copiando el contenido vigente y rotulándolo `1.0` … `Master-Prompt.md` §5.1 lo prohíbe» | La decisión de **no reconstruir** es la correcta bajo D9: un snapshot rotulado 1.0 con contenido 1.1 sería una afirmación sin evidencia. Un archivado falso es indetectable; el ausente se nota | ✔ Veraz y bien fundada |
| «Qué sí se conserva: la **trazabilidad narrativa** … en las filas de control de cambios de los cinco documentos vivos» | Verificado en los cinco. Las filas 1.1 declaran qué cambió por sección, con qué alcance y citando `B-02-03-GeometriaFactory-Application-r1.md`. Lo perdido es el texto literal anterior, no el registro | ✔ Veraz |
| «Los demás archivados del producto están completos: … `00-Contexto/`, `01-Necesidades-Negocio/`, `GeometriaFactory-Contracts/` y las tres carpetas de `GeometriaFactory-Domain/02-Especificacion-Funcional/`» | Verificado: cinco snapshots en `00-Contexto/_legacy/2026-08-08/`, nueve en `01-Necesidades-Negocio/`, catorce en `GeometriaFactory-Contracts/`, y veintiocho en las tres carpetas de 02 de este proyecto de código, repartidos entre `2026-08-09/` y `2026-08-09-b/`. Todos con bloque de archivado y estado `Superado` | ✔ Veraz. La pérdida está acotada a esta categoría y a esta transición |
| «El orquestador vuelve a tomar el snapshot **antes** de construir cualquier despacho» | Corrección de procedimiento declarada. No verificable en este árbol hasta la próxima intervención | Declarada, pendiente de comprobación en la ronda siguiente |

**La nota es veraz en todas sus afirmaciones comprobables.** Declara el error, lo atribuye correctamente, explica por qué no se reconstruyó con la regla que lo prohíbe y acota el alcance de la pérdida. Es el tratamiento que D9 exige: dejar visible una ausencia en lugar de taparla con contenido no verificado.

**Consecuencia para esta auditoría.** La verificación del cambio en 03 se hizo **sin poder diferenciar contra el estado 1.0**, y se apoyó en la trazabilidad narrativa de las filas 1.1 y en la comprobación mecánica del contenido vigente contra las §6 de 02, que es independiente del estado anterior. Ninguna conclusión de §2.5 depende del snapshot faltante: el recuento se rehizo desde las fuentes, no desde la versión previa del catálogo.

**Observación al orquestador.** La ausencia es correcta como está, y **no debe rellenarse**. Lo que corresponde es sostener la corrección de procedimiento declarada y verificarla en la próxima intervención sobre un entregable existente. Se recomienda además evaluar poner `SDD/` bajo control de versiones, que es lo que convierte esta clase de pérdida en irrelevante.

---

## 8. Veredicto y condiciones para promover

### Veredicto: **APROBADO CON OBSERVACIONES**

No existe ningún hallazgo P0. El defecto por el que se abrió esta ronda **está resuelto en su causa y no sólo en su síntoma**: la capacidad F-01 recuperó el caso de uso propio que las fuentes preveían, los dos caminos de alta quedaron separados con estado inicial, tratamiento de credencial, ventana de alta y códigos de rechazo distintos, cada uno rechaza el del otro, el catálogo de errores cuadra con diferencia de conjuntos vacía en las dos direcciones, y las citas que sostenían el estado inicial sobre artefactos que no lo declaran se retiraron de los cinco lugares donde estaban.

### ¿Puede el proyecto de código avanzar a la Fase C?

**Sí.** `GeometriaFactory-Domain` está en condiciones de promover a la **Fase C**. Ninguno de los siete hallazgos bloquea: el P1 es un agujero de recuperabilidad que no impide arrancar la instancia ni compromete ningún contrato ya especificado, el P2 y los cinco P3 son de precisión documental. La categoría 02 y la categoría 03 son insumo suficiente y consistente para 05 y 06.

### Condiciones

**Antes de cerrar la Fase B, o en la primera intervención de la Fase C:**

1. **H-01 (P1)** — Resolver el bloqueo de la cuenta del administrador, con decisión declarada en `CU-02` y en `Definicion-Modelo-De-Dominio.md` §5.1. Si se adopta un código nuevo, propagar el recuento del catálogo de 03 (40 → 41) por §2.1, §6.1, §6.2 y la métrica de cobertura de `DX-Developer-Experience.md` §7. Es la única condición con impacto sobre el contrato.
2. **H-02 (P2)** — Corregir las cuatro remisiones a §4.2/§4.3 y barrer el árbol vivo en busca de otras.

**Sin bloquear la promoción, y acumulables a la próxima subida de versión:**

3. **H-03** — Declarar en `Especificacion-Funcional.md` §9 el punto abierto de las fechas de creación y de última modificación del trabajo, con su destinatario. No rellenar el modelo.
4. **H-04** — «Los cinco errores» en `CU-01` §6.
5. **H-05** — Reescribir el distintivo de `ESTADO_INICIAL_NO_NEGOCIABLE` sobre la causa y no sobre la fila.
6. **H-06** — Reemplazar la cita literal de la transición por la evidencia verificable del intake.
7. **H-07** — Los tres «once» → «doce» y el orden de los flujos alternativos de `CU-03` §5.

**Dirigido al orquestador, fuera del alcance del proyecto de código:** sostener y verificar en la próxima intervención la corrección de procedimiento que la nota de `03-UX-UI-DX/_legacy/2026-08-09/` declara, y evaluar poner `SDD/` bajo control de versiones. La ausencia de los cinco snapshots 1.0 **no se reconstruye**.

### Nota sobre `GeometriaFactory-Application`

Esta auditoría verifica la corrección en `GeometriaFactory-Domain` y no levanta el RECHAZADO de `B-02-03-GeometriaFactory-Application-r1.md`. Con `CU-12` emitido, el camino compatible que el H-01 de aquel informe pedía decidir **ya existe en el dominio**: la capa de casos de uso debe reescribir su `CU-01` para invocarlo en lugar de constituir la cuenta habilitada por su cuenta, y eso se verifica en la ronda que corresponda a ese proyecto de código.
