# Auditoría de la propagación de F-26 y del cierre de la Fase B2 · ronda 1

| Campo | Valor |
| --- | --- |
| Producto | Fábrica de Geometría |
| Rama auditada | `sdd/fase-b2-cierre-y-reseteo-de-clave` |
| Alcance auditado | La tanda completa de cuatro commits sobre el estado `7177e26`: `43245da`, `a2d5b22`, `894c0da` y `20706ef`. `SDD/Intake/` (intake 1.8, manifiesto 1.1); `SDD/Docs/00-Contexto/` y `SDD/Docs/01-Necesidades-Negocio/` íntegras; las categorías 02 y 03 de `GeometriaFactory-Domain`, `GeometriaFactory-Contracts`, `GeometriaFactory-Application`, `GeometriaFactory-Web` y `GeometriaFactory-Visor`; `08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`; `SDD/Maquetas/GeometriaFactory-Web/` íntegra; y el cierre de los dieciocho hallazgos de `B2-Maqueta-GeometriaFactory-Web-r1.md` |
| Motivo de la ronda | Verificar (1) el cierre del rechazo de la Fase B2, (2) la propagación de la capacidad **F-26** desde el intake 1.8 a los cinco proyectos de código y al nivel producto, y (3) la **coherencia entre proyectos**, que es el riesgo declarado de la tanda: cuatro subagentes editaron los mismos temas leyendo estados distintos del repositorio |
| Fuera de alcance | La categoría 04 de los proyectos con `usa_llm` == false; su ausencia no es hallazgo |
| Auditor | Auditor independiente (Arquitecto de Soluciones + QA Senior), invocado desde cero, sin participación en la generación de la tanda |
| Fecha | 2026-08-09 |
| Informes anteriores | `B2-Maqueta-GeometriaFactory-Web-r1.md` y los doce informes `A-00-01-*` / `B-02-03-*` (leídos, **no modificados**) |

---

## Tabla de contenido

- [1. Resumen ejecutivo](#1-resumen-ejecutivo)
- [2. Verificación de lo que la tanda afirma haber hecho](#2-verificación-de-lo-que-la-tanda-afirma-haber-hecho)
- [3. Coherencia entre proyectos: las cuatro preguntas](#3-coherencia-entre-proyectos-las-cuatro-preguntas)
- [4. Recuentos y conjuntos cerrados, contados de nuevo](#4-recuentos-y-conjuntos-cerrados-contados-de-nuevo)
- [5. Cierre de los dieciocho hallazgos de la Fase B2](#5-cierre-de-los-dieciocho-hallazgos-de-la-fase-b2)
- [6. Hallazgos](#6-hallazgos)
  - [6.1 P0 — bloqueantes](#61-p0--bloqueantes)
  - [6.2 P1 — altos](#62-p1--altos)
  - [6.3 P2 — medios](#63-p2--medios)
  - [6.4 P3 — bajos](#64-p3--bajos)
- [7. Veredicto y condiciones para promover](#7-veredicto-y-condiciones-para-promover)
- [8. Lo que esta auditoría no reporta, y lo que no pudo verificar](#8-lo-que-esta-auditoría-no-reporta-y-lo-que-no-pudo-verificar)
- [9. Control de cambios](#9-control-de-cambios)

---

## 1. Resumen ejecutivo

**Lo que salió bien conviene decirlo primero, porque es la mayor parte del trabajo y es de buena calidad.** El P0 que motivaba el rechazo de la Fase B2 está **genuinamente cerrado**: expandí todos los rangos de las sesenta y una filas de la matriz de sensado y los contrasté contra los identificadores realmente definidos en la línea de base — **211 de 211 cubiertos, cero identificadores sin sonda y cero sondas que citen un identificador inexistente** —, y la sonda `SD-61` sensa de verdad los cuatro componentes que faltaban en lugar de anotarlos donde la cuenta cerrara. La maqueta quedó en **cero atributos `style=` en línea** y **cero literales de color fuera de `:root`**, el bundle del visor ya no consulta `prefers-reduced-motion` y con opciones ausentes los dos movimientos arrancan apagados. **Ninguno de los cinco proyectos de código contradice RA-01, RA-02 ni RA-03**; al contrario, `CU-08` de Contracts declara explícitamente que RA-01 no se afloja en un circuito de credenciales, que es exactamente donde había que decirlo. La reconciliación de la pregunta central —si la cuenta reseteada obtiene sesión— **quedó unificada sin residuos sustantivos en los cuatro proyectos**. Los conjuntos cerrados de Domain (43 condiciones), Application (36 condiciones, 11 casos de uso, 54 filas de §6) y Contracts (17 códigos) los conté uno por uno y **cierran contra el recuento declarado**. Los snapshots de `_legacy/2026-08-09/` que verifiqué —los catorce de Domain, los veinticinco de Contracts, los diez de Application, los veintiuno de Web, los siete del Visor y los dos de nivel producto— son **byte a byte** la versión previa real, y el número del nombre coincide con la versión previa real en todos los casos.

**Lo que falló es de otra clase, y es grave.** Los defectos no están en la calidad de cada propagación sino en su **frontera**: la tanda propagó con cuidado hacia abajo y hacia los costados, y no cerró ni hacia arriba ni hacia el nivel producto.

El defecto de fondo es que **la fuente de verdad no registra las decisiones que la tanda propagó**. El commit `20706ef` distribuye dos decisiones del Product Owner —que el reseteo no exige cuenta habilitada, y que la provisoria la produce el sistema— a los cuatro proyectos, y **no tocó el intake**, que sigue en 1.8 diciendo tres veces «**el administrador fija una contraseña provisoria**». No es un desfase de versión: es que la fuente dice lo contrario de lo que once documentos aguas abajo declaran como decisión ratificada por el Product Owner. En paralelo, **F-26 no existe en el nivel producto**: cero menciones en `00-Contexto`, en `01-Necesidades-Negocio` y en el manifiesto, mientras `Alcance-Producto.md` sigue listando la exclusión **X-2 como vigente** y `NB-01` y `NB-02` siguen enseñando que ante un olvido de contraseña hay que dar de baja y volver a dar de alta perdiendo los trabajos — que es exactamente el agujero que F-26 vino a cerrar. La promoción de F-25 corrió la misma suerte a medias: se aplicó a `Roadmap-Producto.md` y a `NB-06`, y **no** a `Alcance-Producto.md` ni a `Necesidades-Negocio.md`, que hoy afirman lo contrario dentro de la misma categoría. Y el hallazgo `AB2-04`, que la Fase B2 dejó como condición de promoción, se cerró **escribiendo la conclusión sin ejecutarla**: dos documentos afirman que la transición `g` → `h` incorporó un criterio que esa sección no tiene.

Se registran **30 hallazgos: 4 P0, 13 P1, 9 P2 y 4 P3.**

**Veredicto: RECHAZADO.** El fundamento está en §7 y no es de forma: la propagación distribuyó correctamente un contenido cuya fuente lo contradice, y dejó el nivel producto enseñando el procedimiento destructivo que la capacidad nueva reemplazó.

---

## 2. Verificación de lo que la tanda afirma haber hecho

Cada fila cita evidencia verificada, con archivo y sección. Enum de estado: `hecho` / `hecho parcialmente` / `no hecho`.

| # | Afirmación | Estado | Verificación |
| --- | --- | --- | --- |
| 1 | El intake pasa de 1.6 a **1.8** con F-26 `Must Have`, RN-12, RN-13, INV-09, retiro de X-2, reescritura de CL-7, F-25 a `Must Have`, escenario E-8 y objetivo de 7 a 8 etapas | **hecho** | Cabecera línea 17 «Versión \| 1.8»; F-26 (:177), F-25 `Must Have` (:178), RN-12 (:211), RN-13 (:212), CL-7 (:284), `~~X-2~~` retirada (:306), INV-09 (:641), §20.E-8 (:1427), «**8 de 8**» (:294). Conté los invariantes: **9 filas `INV-0X`**, coincidiendo con «Nueve invariantes» de la prosa (:644). Conté las reglas: **13 filas `RN-XX`** |
| 2 | Se cierra el rechazo de la Fase B2, con sus dieciocho hallazgos | **hecho parcialmente** | 16 cerrados, 2 cerrados parcialmente. El P0 `AB2-01` está **genuinamente** cerrado (§5). `AB2-04` y `AB2-08(b)` no. Ver §5 |
| 3 | Se propaga a los cinco proyectos de código | **hecho**, y bien | Los cinco absorbieron F-26 en su plano. Domain emitió `CU-13`, `RN-12` y `RN-13`; Contracts emitió `CU-08`; Application emitió `CU-11`; Web sumó la quinta operación del panel y el tercer curso de la credencial propia; el Visor no participa de F-26 y correctamente no la nombra |
| 4 | Se propaga **a nivel producto** a `Roadmap-Producto.md` y a `NB-06` | **hecho parcialmente, y con el objeto equivocado** | Los dos documentos cambiaron, pero **por F-25, no por F-26**. `git diff HEAD~4 HEAD` sobre los dos muestra que el único contenido tocado es la promoción de F-25 en `Roadmap` §2.1 y §3 punto 4, y en `NB-06` §1, §5 y §9. **`grep -rn "F-26"` sobre `00-Contexto`, `01-Necesidades-Negocio` y el manifiesto: cero resultados.** Ver **F26-02** |
| 5 | Decisión A: el reseteo no exige cuenta habilitada | **hecho aguas abajo, sin fuente aguas arriba** | Correcto y sin residuos en los cuatro proyectos (§3). Pero `grep -ni "no exige que la cuenta"` sobre el intake: **cero resultados**. Ver **F26-01** |
| 6 | Decisión B: la provisoria la produce el sistema y el panel no lleva campo de contraseña | **hecho aguas abajo, contra lo que la fuente dice** | La superficie está bien resuelta: `CU-08` §3 pasa a un solo campo y `CU-04` `CA-12` verifica «**0 campos** de contraseña» en el panel. Pero el intake dice tres veces lo contrario (:177, :211, :284). Ver **F26-01** |
| 7 | «Contracts emite `CONTRATO_RESETEO_NO_APLICABLE_A_CUENTA_SIN_CONTRASENA` y su conjunto cerrado pasa de 16 a 17» | **hecho** | Conteo propio: `grep -roh 'CONTRATO_[A-Z_]*'` sobre los ocho CU y los cuatro documentos de 03 da **19 identificadores distintos**, de los cuales 2 son señales declaradas fuera del conjunto (`CONTRATO_TEXTO_NO_INTERPRETABLE`, `CONTRATO_LISTADO_VACIO`). **19 − 2 = 17**, y la unión acumulada por caso de uso lo confirma. El número declarado es correcto en 10 de los 11 lugares donde se declara; el que falla es **F26-10** |
| 8 | «Application CU-11 pasa a orquestar CU-13 en lugar de CU-03» | **hecho, y correcto** | `CU-11` orquesta hoy `CU-13`; verifiqué que ese caso de uso del dominio (a) existe, (b) hace lo que Application dice —§4 pasos 6-7 reemplazan la credencial y ponen la marca en un solo acto— y (c) **no** exige `Habilitado`: `CU-13` §3 «pertenece al conjunto `Pendiente`, `Habilitado`, `Bloqueado`» y FA-02 lo declara |
| 9 | «Sale de Application la causa de rechazo por cuenta no habilitada» | **hecho, y bien declarado** | `DX-Error-Messages.md` §3.11 lo escribe para que no se reponga: «no se relajó ni se renombró: **dejó de existir para este caso de uso**. Sigue vigente en CU-03, donde la cuenta que fija o reemplaza **su propia** credencial sí tiene que estar habilitada» |

---

## 3. Coherencia entre proyectos: las cuatro preguntas

Es el riesgo declarado de la tanda y la razón de este informe. Barrido propio sobre los cinco proyectos, excluyendo `_legacy/` y `Docs/Audit/`, y separando el texto vivo de las filas históricas de control de cambios, que **no son hallazgo**.

| Pregunta | Respuesta correcta (intake 1.8) | Domain | Contracts | Application | Web | Residuos |
| --- | --- | --- | --- | --- | --- | --- |
| ¿La cuenta reseteada obtiene sesión de trabajo? | **No.** Se autentica y se la deriva (RN-13, intake :212) | ✔ `RN-13` §1, `CU-04` FA-03 | ✔ `CU-08` §4 paso 5, `RT-10`, `CU-01` FA-04 | ✔ `CU-03` FA-06 y §10, `Especificacion-Funcional.md` §4 | ✔ `CU-02` FA-07, `CU-03` FA-04, `RT-12`, `Glosario-UX` | **Dos**, los dos en Web: `Linea-Base-Visual.md` §6.1 y `Wireframes-Ingreso.md` §8 → **F26-13** |
| ¿El reseteo exige estado habilitado? | **No.** Procede sobre `Pendiente`, `Habilitado` y `Bloqueado` | ✔ `CU-13` §3 y FA-02 | ✔ `CU-08` FA-04 y CA-08 («**0 códigos** por la situación de la cuenta») | ✔ retirada la causa, con constancia | ✔ `CU-04` FA-06, FA-08, CA-11 | **Ninguno.** Barrido `reseteo.{0,60}habilitad` y su recíproco: 0 |
| ¿Quién produce la provisoria? | **El sistema** | ✔ `CU-13` §2 y §10 | ✔ `CU-08` §2, §4, CA-01 («**0 campos** de contraseña») | ✔ `CU-11` §10 | ✔ `CU-04` §10 y CA-12; `Glosario-Funcional` | **Uno**: `Glosario-UX.md` de Web → **F26-19** |
| ¿Se admite resetear la cuenta de administrador? | **No** | ✔ `CU-13` §6, `CA-04` | ✔ `CU-08` FA-03 y §6 | ✔ `CU-11` §6 | ✔ `CU-04` §6 | **Ninguno** en la respuesta; sí en su fundamento → **F26-24** |

**La reconciliación de la pregunta central funcionó.** Es lo más difícil que tenía la tanda —tres subagentes en paralelo la habían resuelto de tres maneras— y el commit `894c0da` la unificó a favor de 1.8 con corrección quirúrgica: `CU-02` de Web, que declaraba que la cuenta reseteada «obtenía sesión y quedaba confinada», hoy dice lo contrario en sus tres lugares (FA-07, §6 y CA-08), y con eso **desapareció la excepción** que hacía del cambio forzado el único uso del shell de acceso con sesión, de modo que la frontera entre los dos armazones de `Experiencia-De-Uso.md` §3.2 vuelve a valer sin salvedades. Los dos códigos provisorios que Web había acuñado se reemplazaron por los definitivos de Contracts, y verifiqué por extracción que **todo código `CONTRATO_*` citado por Web, Application, Domain y el Visor existe en Contracts**: cero fantasmas en los cuatro sentidos. Las dos únicas ocurrencias de los provisorios que quedan viven dentro de filas de control de cambios que describen su reemplazo, que es uso legítimo.

**Una precisión honesta sobre el enunciado de esta ronda.** El enunciado atribuye a **INV-08** el cierre sobre la cuenta de administrador. Leído el invariante (intake :642), dice que esa cuenta está siempre `Habilitado` y no admite baja, y **no menciona el reseteo**. La conclusión es correcta y ningún documento la contradice, pero su fuente declarada varía: Domain la ancla en INV-08 (`CU-13` §9), Contracts en `RN-01` e `INV-05`, y **INV-08 no aparece ni una vez en Contracts ni en Application**. Lo registro como **F26-24** y no como incoherencia de respuesta.

---

## 4. Recuentos y conjuntos cerrados, contados de nuevo

No se toma ningún número de la prosa. Cada celda es conteo propio por extracción.

| Conjunto | Declarado | Contado | Resultado |
| --- | --- | --- | --- |
| Invariantes del intake §17.1.P.2 | nueve | **9** (`INV-01`…`INV-09`) | ✔ |
| Reglas del intake §4.1 | trece | **13** | ✔ |
| Capacidades `Must Have` del intake §4 | — | **18** (F-01…F-12, F-21…F-24, F-25, F-26) | Contradice a `Alcance-Producto.md` → **F26-02** |
| Etapas comprometidas del intake §15 | ocho (`a`…`h`) | **8** | ✔, pero §15 **no asigna etapa a F-26 ni a F-25** → **F26-02** |
| Condiciones de Domain | 43 | **43** distintas en 51 filas de §6, con 7 repetidas y 8 filas excedentes | ✔ exacto, sin sobrantes ni faltantes |
| Casos de uso / reglas de Domain | 13 / 13 | **13 / 13** archivos | ✔ |
| Casos de uso de Application | 11 | **11** archivos, `CU-01`…`CU-11` sin huecos | ✔ |
| Condiciones de Application | 36 | **36** distintas en 37 filas (`ESTADO_INICIAL_NO_NEGOCIABLE` por causas opuestas) | ✔ |
| Filas de §6 de Application | 54 | **54** (5,5,9,5,6,2,3,5,4,5,5) | ✔ |
| Conjunto cerrado de Contracts | 17 | **17** por dos vías independientes | ✔ en 10 de 11 lugares → **F26-10** |
| Contratos de uso de Contracts | 8 | **8** archivos | ✔ salvo dos residuos → **F26-20** |
| Funciones de la fachada del Visor | 6 | **6** (§4.1 a §4.6) | ✔ en el Visor y en Web; falla en el manifiesto → **F26-21** |
| Condiciones del contrato de fachada | 7 | **7** distintas en 8 filas | ✔ |
| Operaciones del panel de cuentas | 5 | **5** (habilitar, bloquear, rehabilitar, resetear, baja) | ✔ en los nueve lugares donde se declara |
| Identificadores de la línea de base | 211 | **11 + 73 + 74 + 24 + 29 = 211**, series contiguas sin huecos | ✔, y **211 de 211 cubiertos** por las 61 sondas |
| Superficies / wireframes de Web | 11 / 11 | **11 archivos `Wireframes-*.md`**, 11 `*.html` | ✔ |
| Necesidades de negocio | 9 | **9 archivos `NB-0X`** | ✔ como recuento; F-26 no tiene ninguna → **F26-02** |

**Cuatro conjuntos declarados cerrados no cierran**, y son los hallazgos **F26-10** (Contracts, dieciséis contra diecisiete), **F26-02** (nivel producto, dieciséis capacidades `Must Have` contra dieciocho), **F26-18** (Application, `§2.5` declarado cerrado sin los rechazos de `CU-13`) y **F26-20** (residuos de recuento en Domain, Contracts, Application y Visor). El resto de los recuentos que la tanda tocó **cierran**, lo que es notable dado el volumen.

---

## 5. Cierre de los dieciocho hallazgos de la Fase B2

Verificado hallazgo por hallazgo contra el estado actual del árbol, no contra lo que la fila de control de cambios declara.

**Dieciséis cerrados**, y varios con más rigor del pedido. El P0 `AB2-01` se cerró abriendo `SD-61` en lugar de anotar los cuatro identificadores en filas existentes: expandí los rangos de las sesenta y una filas y el resultado es 211 de 211, sin identificadores citados que no existan. `AB2-02` se cerró y además **abrió la §6.1 nueva**, que declara con nombre y fecha lo que la línea de base no validó — y que es, con diferencia, la mejor pieza de la tanda: dice que `establecerMovimiento` «**no aparece en ningún archivo de la maqueta** y **nadie la miró en pantalla**», y verifiqué que las dos afirmaciones son ciertas. `AB2-07` quedó en cero `style=` y cero literales de color; `AB2-13(b)` recalculé el contraste declarado de 11.94:1 con la fórmula WCAG y **da 11.94 exacto**: el número es honesto.

**Dos no cerrados del todo:**

- **`AB2-04` — cerrado en la conclusión, no en el instrumento.** El informe r1 daba dos salidas y pedía declarar una. Se eligió la primera: F-25 sube a `Must Have`, `Roadmap-Producto.md` §3 punto 4 se reescribe y `NB-06` §5 lo recoge. Pero los dos documentos afirman que **la transición `g` → `h` de §5.2 incorporó el gobierno de los movimientos como criterio**, y esa sección no lo incorporó: `git diff HEAD~4 HEAD` sobre el roadmap muestra que §5.2 **no fue tocada**. Es **F26-04**, y es la misma familia de defecto que el P0 que motivó el rechazo: una afirmación cuya evidencia interna no resuelve. Además la promoción quedó a medio propagar → **F26-03**.
- **`AB2-08(b)` — la ARIA del árbol quedó con un segundo portador de estado.** `Maqueta.js`, en `resaltarPorIndice()`, hace `b.parentNode.setAttribute('aria-selected', …)` sobre un `<ul role="group">`, contra el comentario del propio archivo «UN SOLO PORTADOR DE ROL Y DE ESTADO». El camino gemelo `seleccionar()` sí quedó bien corregido: el arrastre quedó sólo en esta función. Es **F26-22**.

**Y el cierre introdujo un defecto de su propia clase.** `AB2-03` reprochaba a la bitácora tres afirmaciones falsas sobre el estado de los archivos. La bitácora 1.1 las corrigió y **escribió dos nuevas**: declara que `Roadmap-Producto.md` §3 punto 4 y `NB-06` §5 «siguen escritos contra el intake 1.5», cuando los dos se corrigieron **en el mismo commit** que subió la bitácora a 1.1 — y no nombra lo que sí sigue pendiente, que es `Alcance-Producto.md` y `Necesidades-Negocio.md`. Es **F26-12**.

---

## 6. Hallazgos

**30 hallazgos: 4 P0 (`F26-01` a `F26-04`), 13 P1 (`F26-05` a `F26-17`), 9 P2 (`F26-18` a `F26-26`) y 4 P3 (`F26-27` a `F26-30`).**

### 6.1 P0 — bloqueantes

#### F26-01 · P0 · Las dos decisiones finales del Product Owner no están en el intake, y para una de ellas el intake dice literalmente lo contrario

- **Dónde:** `SDD/Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (:177), §4.1 RN-12 (:211), §7 CL-7 (:284) y §23 control de cambios (:1593), contra once documentos de `GeometriaFactory-Domain`, `-Contracts`, `-Application` y `-Web`.
- **Qué dice:** el intake, en las tres secciones que describen F-26, dice «**El administrador fija una contraseña provisoria** que le comunica al alumno», y no dice nada sobre qué estado exige el reseteo. Aguas abajo, once documentos declaran lo contrario **atribuyéndolo al Product Owner**: `Contracts/CU-08` §10 «**Quién produce la provisoria lo decidió el Product Owner y el contrato lo declara: la produce el sistema, no la escribe el administrador**»; `Contracts/CU-06` §10 «El Product Owner resolvió que **resetear no exige que la cuenta esté habilitada** … y que **la contraseña provisoria la produce el sistema**»; `Domain/CU-13` §10 «**La produce el sistema** —no la escribe el administrador, **por decisión del Product Owner**» y «el Product Owner **la ratificó**»; y con el mismo tenor `Domain/RN-12` §3, `Domain/Glosario-Funcional.md`, `Application/CU-11` §10, `Web/CU-04` §10.
- **Qué debería decir:** o el intake sube a **1.9** recogiendo las dos decisiones y reescribiendo F-26, RN-12 y CL-7, o la propagación de `20706ef` no tiene sustento. No hay tercera salida: la decisión B no es un vacío que se completa, es una **contradicción frontal** con el texto vigente de tres secciones de la fuente.
- **Cómo lo verifiqué:** `grep -ni "produce el sistema\|genera la contraseña\|no la escribe\|no exige que la cuenta\|no adivinable"` sobre el intake completo → **0 resultados**. `grep -ni 'provisoria'` → 8 líneas, ninguna atribuye la producción al sistema. La fila 1.8 del control de cambios (:1593) enumera **exactamente dos** correcciones —el recuento de invariantes y la precisión de RN-13— y ninguna de éstas. `git show --name-only 20706ef -- SDD/Intake/` y `894c0da -- SDD/Intake/` → **vacío en los dos casos**: el intake fue tocado por última vez en `43245da`.
- **Por qué P0:** los cinco proyectos se rigen por la regla que ellos mismos escriben (`Domain/02-Especificacion-Funcional/README.md`): «Nada se origina acá. Toda regla, todo invariante y todo valor numérico traza a su sección del intake… **Lo que el intake no declara, no se inventa**». Once documentos afirman como ratificado por el Product Owner algo que la fuente de registro contradice. Cualquier lector que resuelva la discrepancia a favor de la fuente —que es lo que la regla manda— construye el panel con campo de contraseña, que es exactamente el defecto que la decisión B vino a evitar.

#### F26-02 · P0 · F-26 no llegó al nivel producto, que sigue enseñando el procedimiento destructivo que la capacidad reemplazó

- **Dónde:** `SDD/Docs/00-Contexto/Alcance-Producto.md` §4.1 (:78), §5 (:144, :149, :159), `Vision-Producto.md` §7 (:173), `SDD/Docs/01-Necesidades-Negocio/Necesidades-Negocio.md` (:65, :161), `NB-01-Control-De-Admision-Al-Laboratorio.md` (:35), `NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md` (:11, :35, :37, :50, :80).
- **Qué dice:**
  1. **F-26 no aparece.** `grep -rn "F-26\|RN-12\|RN-13\|INV-09"` sobre `00-Contexto`, `01-Necesidades-Negocio` y `PRODUCT-MANIFEST`: **cero resultados**. Una capacidad `Must Have` no tiene necesidad de negocio que la articule, no tiene fila en el alcance y no tiene etapa en el roadmap.
  2. **La exclusión retirada sigue vigente.** `Alcance-Producto.md` §5: «X-2 · Recuperación de contraseña olvidada … **La resuelve el administrador dando de baja y volviendo a dar de alta**», y la nota de cierre de §5 la valida: «**X-1, X-2, X-3 y X-4** se corresponden con las capacidades declaradas Won't Have v1».
  3. **Dos NB enseñan el camino destructivo como el único.** `NB-02` §1: «un alumno que olvida su contraseña sólo se recupera por intervención del administrador, **que da de baja la cuenta y la vuelve a dar de alta, perdiendo los trabajos**»; §3: «olvidar la contraseña **cuesta la cuenta y sus trabajos**»; §6: «**resuelve, dando de baja y volviendo a dar de alta**». `NB-01` §2 repite: «**la única forma de resolver una cuenta perdida sea darla de baja y volver a darla de alta**».
  4. **El recuento de capacidades comprometidas no cierra.** `Alcance-Producto.md` §4.1: «**Dieciséis** capacidades con prioridad `Must Have`», con una tabla de dieciséis filas. Conté el intake §4: **dieciocho**. Faltan F-25 y F-26. `Necesidades-Negocio.md` lo arrastra dos veces, incluida la afirmación «Las **dieciséis** capacidades `Must Have` están cubiertas: **ninguna quedó sin necesidad que la articule**», que hoy es falsa.
- **Qué debería decir:** F-26 tiene que entrar en `Alcance-Producto.md` §4.1 con su etapa, X-2 tiene que tacharse en §5 como ya se tachó X-5, `NB-01` y `NB-02` tienen que reemplazar la salida destructiva por el reseteo, `Vision-Producto.md` RG-06 tiene que perder su mitigación por baja, y F-26 necesita una necesidad de negocio —propia o por fusión, como se hizo con F-25 en `NB-06`—. El recuento de `Must Have` pasa a dieciocho.
- **Cómo lo verifiqué:** `grep -rn` sobre las dos categorías y el manifiesto; conteo fila por fila de la tabla de §4 del intake contra la de `Alcance-Producto.md` §4.1; `git diff --name-status 7177e26..HEAD` sobre el nivel producto, que muestra que **sólo** se tocaron `Roadmap-Producto.md` y `NB-06`.
- **Por qué P0:** el nivel producto es lo que la categoría 06 y la 07 leen para armar el backlog y el plan. Tal como está, una capacidad `Must Have` del alcance comprometido no tiene ni necesidad, ni etapa, ni fila, ni caso de uso previsto — y los documentos que sí la mencionarían enseñan activamente el procedimiento que la destruye. Además el intake §15 **no le asigna etapa a F-26**, de modo que el objetivo «8 de 8, todas las comprometidas» no tiene dónde entregarla.

#### F26-03 · P0 · La promoción de F-25 quedó a medias y produce contradicción viva dentro de las dos categorías de nivel producto

- **Dónde:** `Alcance-Producto.md` §4.2 (:109, :112, :120) y `Necesidades-Negocio.md` (:59, :65, :94, :155, :161), contra `Roadmap-Producto.md` §2.1 (:62) y §3 (:95, :100) y `NB-06` §1 (:39), §3 (:52), §5 (:84) y §9 (:112). También `GeometriaFactory-Visor/02-Especificacion-Funcional/Especificacion-Funcional.md` (:93) y `CU-07` (:97).
- **Qué dice:** `Roadmap-Producto.md` 1.3 dice que F-25 «desde `PRODUCT-INTAKE` 1.7 es `Must Have` y **condiciona el cierre**», y `NB-06` 1.3 que sus criterios octavo y noveno «**son además bloqueantes**». En la misma categoría, `Alcance-Producto.md` 1.2 la lista en §4.2 con «Prioridad declarada: **Should Have**», bajo el preámbulo «**ninguna está comprometida para el tramo `a` a `h`**», y remata: «Su prioridad `Should Have` no es una concesión sino la clasificación correcta: es **comodidad de lectura, no capacidad de entrega** … **esta capacidad no está comprometida**». `Necesidades-Negocio.md` 1.2 la declara `Should Have` en cuatro lugares y afirma: «**Que F-25 sea `Should Have` y viva en una etapa comprometida no la compromete**: no es criterio de transición de ninguna fase ni criterio de aceptación del producto» — que es la negación literal de lo que dice `NB-06`, la necesidad de la que ese catálogo deriva.
- **Qué debería decir:** F-25 pasa a §4.1 de `Alcance-Producto.md` con las demás `Must Have`, y las cinco afirmaciones de `Necesidades-Negocio.md` se alinean con `NB-06`.
- **Cómo lo verifiqué:** lectura completa de §4.1 y §4.2 de `Alcance-Producto.md` y de §3, §5.1 y §5.4 de `Necesidades-Negocio.md`; `grep -rn "Should Have\|no la compromete\|diferible"` sobre `SDD/` excluyendo `_legacy/` y `Docs/Audit/`; `git diff --name-status 7177e26..HEAD` para confirmar que los dos archivos no fueron tocados.
- **Por qué P0 y no P1:** no es un conteo desactualizado sino una **contradicción de compromiso** entre dos documentos de la misma categoría sobre si una capacidad bloquea el cierre de una fase. Los dos son insumo declarado de 06 y 07, y las dos lecturas producen planes distintos. Se suma que `NB-06` declara en su cabecera como upstream a `Alcance-Producto.md` §4.2, que es precisamente la sección que lo contradice.

#### F26-04 · P0 · Dos documentos afirman que la transición `g` → `h` incorporó un criterio que esa sección no tiene

- **Dónde:** `SDD/Docs/00-Contexto/Roadmap-Producto.md` §3, punto 4 (:100) y `SDD/Docs/01-Necesidades-Negocio/Necesidades-De-Negocio/NB-06-Visualizacion-Dentro-Del-Producto.md` §5 (:84), contra `Roadmap-Producto.md` §5.2 (:147).
- **Qué dice:** el roadmap: «En consecuencia **la transición `g` → `h` de §5.2 sí incorpora el gobierno independiente de los dos movimientos como criterio, y la fase `g` no cierra sin él**». `NB-06`: «promovida F-25 a `Must Have`, la transición `g` → `h` de `Roadmap-Producto.md` §5.2 **incorpora el gobierno independiente de los dos movimientos**». La fila `g` → `h` de §5.2 tiene **seis** criterios y **ninguno** menciona el gobierno de los movimientos: figuras del escenario semilla, diez recorridos sin degradar, misma disposición, cero peticiones de red, sincronización árbol-escena y vista única del administrador. El único que nombra F-25 lo hace **para excluirlo**: «el movimiento automático de F-25 **no altera** la disposición».
- **Qué debería decir:** o §5.2 suma el criterio y entonces las dos afirmaciones son ciertas, o las dos afirmaciones se corrigen. Como `AB2-04` pedía elegir entre dos salidas y **declarar** la elegida, lo que corresponde es sumar el criterio: es la salida que la tanda dice haber tomado.
- **Cómo lo verifiqué:** conteo de los seis criterios de la celda `:147`; `git diff HEAD~4 HEAD -- SDD/Docs/00-Contexto/Roadmap-Producto.md`, que muestra que el commit tocó **sólo** la cabecera, la fila `g` de §2.1 y los puntos 3 y 4 de §3 — **§5.2 no aparece en el diff**.
- **Por qué P0:** `Deriva-Rules.md` §1, citada por el informe r1 y aplicada allí para elevar `AB2-01` a P0, tipifica el caso: «una afirmación con evidencia que no resuelve es un hallazgo P0: es peor que no citar, porque **simula verificación**». Acá el defecto es más caro que en `AB2-01`, porque la afirmación es la **prueba de cierre** de una condición de promoción de la ronda anterior: quien verifique `AB2-04` leyendo el punto 4 concluye que se corrigió, y no se corrigió.

### 6.2 P1 — altos

#### F26-05 · P1 · Los dos documentos de nivel producto subieron versión sin fila de control de cambios

`Roadmap-Producto.md` declara `**Versión:** 1.3` en su cabecera y su §7 termina en la fila **1.2**. `NB-06` declara `Versión | 1.3` y su control de cambios termina en la fila **1.2**. Los dos son los únicos documentos de nivel producto que la tanda editó, y en los dos el cambio real —la promoción de F-25— **no está descrito en ninguna parte del propio documento**. Verificado con `grep -o "^| 1\.[0-9] |"` sobre las dos secciones de control de cambios y lectura del final de los dos archivos. Agrava que el archivado sí se hizo bien: `_legacy/2026-08-09/Roadmap-Producto-v1.2.md` y `…/NB-06-…-v1.2.md` son **byte a byte** el contenido previo (`diff` contra `git show HEAD~4:<ruta>` → idéntico), de modo que la disciplina se aplicó a la mitad del procedimiento.

#### F26-06 · P1 · El punto abierto de las siete u ocho etapas sigue declarado abierto en cinco documentos, y el intake se contradice a sí mismo

El intake 1.7 resolvió el objetivo de avance: §8 (:294) declara «**8 de 8** [DECISIÓN 2026-08-09: se cuentan todas las comprometidas, no siete]». Pero el propio intake, en §22 A-2 (:1546), sigue diciendo «los targets de las cuatro métricas de negocio (**7 de 7 etapas**, …)»: **contradicción interna de la fuente**. Y el nivel producto no se enteró: `Vision-Producto.md` §5 (:122, :137) mantiene OBJ-01 con «sobre las **7** planificadas (`a` a `g`) \| **7 de 7**», y **cinco documentos** siguen declarando el asunto como punto abierto escalado al Product Owner que «**no se resuelve acá**» — `Vision-Producto.md` (:131), `Alcance-Producto.md` (:69, :169), `00-Contexto/README.md` (:84), `Necesidades-Negocio.md` (:167, :209) y `01-Necesidades-Negocio/README.md` (:103) —, cuando **ya está resuelto**. Es el caso exacto del criterio (g): un punto declarado abierto que no lo está. Verificado con `grep -rn "7 de 7\|8 de 8\|siete etapas\|siete u ocho"` sobre el intake y las dos categorías.

#### F26-07 · P1 · El intake §11 sigue declarando el riesgo de pérdida de trabajos como consecuencia aceptada de X-2

`SDD/Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md` §11, fila `RN-B6` (:340): «El alumno pierde su trabajo por olvidar la contraseña, **sin canal de recuperación** … el administrador recrea la cuenta, pero **la baja física elimina también sus trabajos** (RN-07) \| Declarado como **consecuencia aceptada de X-1 y X-2**. Conviene que el docente lo advierta al alumno **antes de dar de baja**». X-2 fue retirada en la misma versión, treinta y cuatro líneas más arriba (:306), y CL-7 fue reescrito (:284) precisamente para que el remedio deje de ser la baja. El riesgo, tal como está, **ya no existe**, y su mitigación es el procedimiento prohibido. Es además la fila que `Vision-Producto.md` RG-06 (:173) copia palabra por palabra, de modo que el defecto se propagó aguas abajo desde la fuente. Verificado leyendo §9, §7 y §11 del intake y contrastando con RG-06.

#### F26-08 · P1 · El criterio verificable de RN-12 conserva la ambigüedad que la versión 1.8 dice haber eliminado

`SDD/Intake/…` §4.1, RN-12 (:211), columna de verificación: «Un alumno con tres trabajos … conserva los tres, con sus comentarios, después del reseteo. **Ingresa con la provisoria y los ve**». Por INV-09 (:641) una cuenta con la marca «**no ejerce ninguna capacidad del sistema salvo cambiar su propia contraseña**», y por RN-13 precisada (:212) «se autentica pero **no obtiene sesión de trabajo**»: con la provisoria **no puede ver sus trabajos**. La fila 1.8 del control de cambios (:1593) declara haber corregido exactamente este verbo —«**RN-13 decía que la cuenta reseteada “ingresa”**, que se leía como que obtenía sesión de trabajo»— y lo corrigió en RN-13 y no en RN-12, que está en la fila inmediatamente anterior. El dato de prueba, tal como está redactado, **no pasa**. Verificado leyendo las tres filas y la entrada de control de cambios.

#### F26-09 · P1 · Domain atribuye a §17.1.P.2 del intake lo contrario de lo que esa sección dice sobre RN-12

`GeometriaFactory-Domain/02-Especificacion-Funcional/Especificacion-Funcional.md` (:97): «**RN-12 y RN-13 comparten invariante**, INV-09, y no es un descuido de la tabla: **el intake lo declara así en §17.1.P.2**», y `02-Especificacion-Funcional/README.md` (:80) lo repite. El intake §17.1.P.2 (:644) dice literalmente lo opuesto: «Las cuatro reglas que no tienen invariante asociado —RN-07 …, RN-08 …, RN-09 … y **RN-12 conservación de la cuenta y sus trabajos al resetear**— no lo tienen … **RN-13 sí lo tiene, y es INV-09**». Y la fila 1.8 del control de cambios (:1593) declara esa lista como la **corrección deliberada** de la emisión anterior. Es una afirmación falsa sobre la fuente en el documento índice de la categoría, y está propagada a `Guia-Onboarding-Developer.md`, `DX-Developer-Experience.md` y `DX-Error-Messages.md`.

**Matiz que corresponde declarar:** el intake **es internamente ambiguo** en este punto, porque la columna «Regla de negocio que sostiene» de INV-09 (:641) sí dice «RN-12, RN-13». `Definicion-Modelo-De-Dominio.md` (:230) se apoya en esa columna y su cita es **verdadera**, porque la califica: «así lo declara §17.1.P.2 **en su columna de regla sostenida**». El defecto está en los dos lugares que afirman sin calificar que la sección lo declara, cuando su prosa lo niega. Verificado leyendo §17.1.P.2 completo y los cinco lugares de Domain.

#### F26-10 · P1 · El catálogo de códigos de Contracts declara dieciséis entradas y tiene diecisiete

`GeometriaFactory-Contracts/03-UX-UI-DX/DX-Error-Messages.md` §3.2 (:118): «**Dieciséis entradas de código**, una por cada código del conjunto cerrado, más una fila de retiro». Conté las filas: **17 entradas `DXT-nn`** (`DXT-01`…`DXT-18` sin `DXT-09`, que es la fila de retiro) más **3** filas de señal `DXT-Nx` en §3.3. Contradice a §2.2 (:72, «Las seis categorías cubren los **diecisiete** códigos», y sumé sus celdas: 3+2+7+3+1+1 = 17) y a §3.3 (:147, «los **diecisiete**») **del mismo archivo**. Es el único de los once lugares donde el conjunto cerrado se declara que quedó en dieciséis, y es justamente **el único lugar de todo el proyecto donde los diecisiete están enumerados juntos**: la tabla §6 de `CU-06` lista sólo diez de los diecisiete, de modo que no hay en la categoría 02 ningún sitio contra el cual verificar el conteo. Verificado con `grep -o "^| \`DXT-[0-9][0-9]\`" | sort -u | wc -l` → 17.

#### F26-11 · P1 · El Visor afirma que el intake declara cinco funciones de fachada, y declara F-25 `Should Have`

Cuatro lugares del Visor están escritos contra un intake anterior al 1.6:
- `02-Especificacion-Funcional/Especificacion-Funcional.md` §4: el concepto central acuña la sexta función «**mientras el intake declara cinco**». El intake §17.7 P.3 (:1063) declara seis desde 1.6: «`establecerMovimiento(id, opciones)` … **Sexta función** [DECISIÓN 2026-08-09]».
- `Definicion-Contrato-De-Fachada.md` §5.5 (:244), titulada «**Punto abierto resuelto**», sigue diciendo que «el intake §17.7 P.3 **sigue declarando cinco funciones** y corresponde que las declare seis»; §1 (:46) y la trazabilidad de cabecera (:9) repiten la premisa; `02-Especificacion-Funcional/README.md` (:63) también.
- `Especificacion-Funcional.md` §5.1 (:93) declara F-25 como «capacidad **F-25** del intake §4, **`Should Have`**», cuando el intake (:178) la declara `Must Have` desde 1.7 y con «**Subida a `Must Have` el 2026-08-09**» escrito en la propia celda. `CU-07` (:97) lo repite.

Es un punto abierto declarado vivo que se cerró aguas arriba hace dos versiones, y una prioridad que quedó una versión atrás en el proyecto de código que **entrega** la capacidad. Verificado contrastando cada cita contra la línea del intake.

#### F26-12 · P1 · La bitácora reemplazó tres afirmaciones falsas por dos nuevas

`GeometriaFactory-Web/03-UX-UI-DX/Bitacora-Validacion-Maqueta.md` (:99 y :135): «`Roadmap-Producto.md` §3 punto 4 y `NB-06` §5 **siguen escritos contra el intake 1.5** y afirman que ubicar F-25 en la etapa `g` “no la compromete” y que es `Should Have` diferible». Los dos documentos **ya fueron corregidos**, y en el **mismo commit `43245da`** que subió la bitácora a 1.1: `Roadmap-Producto.md` (:100) dice hoy «**Ubicarla en `g` la compromete, y así corresponde** [ACTUALIZADO 2026-08-09 contra `PRODUCT-INTAKE` 1.7]» y `NB-06` (:39) «desde `PRODUCT-INTAKE` 1.7 la capacidad es **`Must Have`**». Y lo que sí sigue sin propagar —`Alcance-Producto.md` y `Necesidades-Negocio.md`, hallazgo **F26-03**— la bitácora **no lo nombra**. Es `AB2-03` reproducido dentro de la versión que lo corrigió, con el agravante de que su fila 1.1 se acredita haber verificado las afirmaciones «una por una contra los archivos». Verificado con `git show --stat 43245da` y lectura de los dos archivos vivos.

#### F26-13 · P1 · Dos residuos de «con sesión» sobrevivieron a la reconciliación, en Web

- `03-UX-UI-DX/Linea-Base-Visual.md` §6.1 (:281): «el **tercer curso de `SUP-04`**, el cambio forzado sobre el shell de acceso **con sesión iniciada**». Es la formulación exacta que `Experiencia-De-Uso.md` 1.2, `Glosario-UX.md` 1.2 y `Wireframes-Credencial-Propia.md` 1.2 corrigieron; `Linea-Base-Visual.md` no fue tocado por `894c0da` ni por `20706ef`. La misma celda remite a «`Wireframes-Panel-De-Cuentas.md` **1.1** y `Wireframes-Credencial-Propia.md` **1.1**» como lo que hay que construir, y los dos están hoy en **1.2**: quien siga la remisión literal implementa el campo de contraseña que la 1.2 eliminó.
- `03-UX-UI-DX/Wireframes-Ingreso.md` §8 (:157): «el **ingreso con provisoria que deriva al cambio forzado con sesión otorgada**», contra §4 (:98) y §5 (:117) del mismo archivo, que dicen «**No se otorga sesión**», y contra su propia fila 1.2, que declara haber corregido §4 y §5 — y efectivamente no tocó §8.

Verificado con `grep -rn "con sesión iniciada\|sesión otorgada"` sobre `SDD/Docs` excluyendo `_legacy/` y `Docs/Audit/`: **los dos son los únicos residuos vivos**; todas las demás ocurrencias están dentro de filas de control de cambios que describen la corrección, y no son hallazgo.

#### F26-14 · P1 · Application enseña tres negativas donde declara cuatro comprobaciones, y omite la que corta primero

`03-UX-UI-DX/DX-Developer-Experience.md`: el título de §1.4 dice «Las **tres** negativas», el cuerpo inmediatamente debajo dice «Las **cuatro** comprobaciones transversales … producen **cuatro** negativas distintas», y la tabla tiene **tres** filas. `grep -c CAMBIO_DE_CONTRASENA_PENDIENTE` sobre ese archivo → **0**: el marco DX de la capa nunca nombra el motivo de la comprobación que él mismo declara como cuarta. Lo mismo en `Guia-Onboarding-Developer.md`, cuyo §3.5 se titula «Las **tres** negativas, en diez minutos» con tabla de tres filas, y cuyo `grep -c` también da **0** — de modo que quien recorra entero el artefacto de onboarding termina sin saber que existe la negativa que corta antes que las otras tres. `03-UX-UI-DX/README.md` lo repite en dos lugares. Agrava que la fila 1.1 de `DX-Developer-Experience.md` declare haber pasado «de tres a cuatro negativas», cuando `git show 43245da` sobre ese archivo muestra que se cambió **una sola oración** y no se agregó la fila a la tabla.

#### F26-15 · P1 · Dos identificadores mal citados en `CU-11` de Application

- `CU-11-Resetear-La-Contrasena-De-Un-Alumno.md` §10 (:128): «las dos únicas propiedades que este caso de uso le exige al valor, y **que CA-08 verifica**». La §8 de ese caso de uso tiene **`CA-01` a `CA-07`**: `CA-08` no existe. Verificado extrayendo los identificadores de la tabla (`grep -o "^| CA-0[0-9]"` → siete). Es el único identificador inexistente que apareció en toda la categoría.
- `CU-11` `CA-03` (:104): «La propia cuenta reemplaza su credencial **por CU-03 FA-03**». El flujo que levanta la marca es **FA-05** (`CU-03` :72, «Es el **cambio forzado** … además **levanta la marca**»); FA-03 es el reemplazo común, que no la toca. Toda la demás cadena cita FA-05 correctamente, incluido `Especificacion-Funcional.md` en tres lugares.

#### F26-16 · P1 · `CU-01` de Contracts declara abierto un punto que está cerrado por dos vías

`GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-01-…md` §10 (:112): «Que RN-13 diga que la cuenta reseteada “**ingresa**” está declarado como **punto abierto** en `GeometriaFactory-Domain` `Especificacion-Funcional.md` §9». Falso por las dos puntas: el intake 1.8 (:212) **ya no dice «ingresa»**, y `Domain/02-Especificacion-Funcional/Especificacion-Funcional.md` 1.5 §9 **ya no lista ese punto** y en su lugar declara «El punto abierto de cómo llega la cuenta reseteada al cambio de contraseña **quedó resuelto**». El propio `CU-08` §10 lo registra como cerrado. `CU-01` quedó sin propagar: no fue tocado por `894c0da` ni por `20706ef`.

#### F26-17 · P1 · Domain declara abierto el punto de los sellos de tiempo del trabajo, que el Product Owner resolvió en el intake 1.4

`Domain/02-Especificacion-Funcional/Especificacion-Funcional.md` §9 lo lista como pendiente de «declaración del Product Owner en `PRODUCT-INTAKE` §17.1». El intake §17.3 P.4 ya lleva: «**Ampliación del 2026-08-09: sellos de tiempo del trabajo** [**DECISIÓN del Product Owner**]. `TRABAJO` suma **fecha de creación** y **fecha de última modificación**», y la fila 1.4 de su control de cambios (:1597, punto (b)) lo registra como decisión consolidada. Consecuencia material: `Definicion-Modelo-De-Dominio.md` §2.2 **sigue sin declarar esos dos atributos**. Matiz declarado: la decisión vive en §17.3 y no en §17.1, y dice que las produce el consumidor por el puerto de reloj, de modo que es discutible cuánto de ella baja a la entidad de dominio; lo que no es discutible es que el punto está redactado como si el Product Owner no se hubiera pronunciado.

### 6.3 P2 — medios

#### F26-18 · P2 · El conjunto de rechazos del dominio inalcanzables desde Application se declara cerrado y no lo está

`Application/03-UX-UI-DX/DX-Error-Messages.md` §2.5 se define como el conjunto de rechazos del dominio que esta capa no puede producir, y §7.1 lo cuantifica en **dieciséis**, que es el recuento correcto de la tabla que lista. Pero **ninguno de los rechazos de `CU-13` figura**: `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` y `RESETEO_CON_ARRASTRE_DE_TRABAJOS` no aparecen en ningún archivo vivo de Application (`grep -r` → 0). Los dos son inalcanzables por construcción desde `CU-11`, que es exactamente el criterio de pertenencia de §2.5. El conjunto entró un caso de uso nuevo y no creció.

#### F26-19 · P2 · El glosario UX de Web dice que la provisoria la fija el administrador

`GeometriaFactory-Web/03-UX-UI-DX/Glosario-UX.md` (:44): «**Contraseña provisoria** \| La credencial que **el administrador le fija** a un alumno al resetearla», contra `02-Especificacion-Funcional/Glosario-Funcional.md` (:50): «La credencial que **el sistema produce** … **El panel no tiene dónde escribirla**». `Glosario-UX.md` no fue tocado por `20706ef`, que es el commit de la decisión B, de modo que la entrada de vocabulario de la categoría 03 quedó con la formulación anterior a la decisión, contradiciendo a la de la categoría 02 del mismo proyecto.

#### F26-20 · P2 · Diecisiete recuentos desactualizados repartidos en cuatro proyectos

Todos de la misma familia: la propagación movió el contenido y no terminó de mover los conteos. Fuera de filas históricas de control de cambios, que no son hallazgo.

| Proyecto | Dónde | Dice | Es |
| --- | --- | --- | --- |
| Domain | `DX-Developer-Experience.md` §5 | «las **treinta y siete** condiciones del catálogo» | 43, y el mismo archivo dice «43 de 43» veintidós líneas después |
| Domain | `DX-Error-Messages.md` §8 | «el total daría **41** en lugar de **40**» | números de la versión 1.2; con 43 vivas y tres retiradas serían 46 y 43 |
| Domain | `Especificacion-Funcional.md` §8 | «la serie es **contigua de RN-01 a RN-11**» | `RN-01` a `RN-13`; el `README.md` de la sección lo dice bien |
| Domain | `Guia-Onboarding-Developer.md` §3.4 y `03-UX-UI-DX/README.md` | «Las **dos** máquinas de estado» | tres desde 1.4: `Definicion-Modelo-De-Dominio.md` §5.3 es la de la marca |
| Domain | `DX-Developer-Experience.md` §3 | enumera tres motivos de admisibilidad | cuatro: falta `CAMBIO_DE_CONTRASENA_PENDIENTE` |
| Contracts | `Especificacion-Funcional.md` §8 | «los **siete** contratos de uso» | ocho |
| Contracts | `Glosario-Funcional.md` §3.1 | «los **seis** casos de uso», en la fila anterior a una que dice «Los **ocho**» | ocho |
| Contracts | `DX-Developer-Experience.md` §8 | «`RT-01` a **`RT-09`**» y «`US-01` a **`US-20`**» | `RT-01` a `RT-11`, `US-01` a `US-22`; §4.1 del mismo archivo dice «once» |
| Contracts | `CU-08` §17 | «entran **dos códigos** al conjunto cerrado» | tres, según su propia §6 |
| Application | `Especificacion-Funcional.md` ×2 y `DX-Error-Messages.md` | «los **doce** casos de uso de `GeometriaFactory-Domain`» | trece; la tabla de §7.4 sí lista los trece |
| Application | `Especificacion-Funcional.md` §9 y `03-UX-UI-DX/README.md` | «Las **once** reglas del producto» | trece |
| Application | `DX-Error-Messages.md` §3, §7.1, §7.3, §7.4 | «**Doce** condiciones compartidas», «36 + **19** = **55**», «`CU-01` a `CU-10`», «`RN-01` a `RN-11`» | once, 36 + 18 = 54, `CU-01` a `CU-11`, `RN-01` a `RN-13`; dos filas de §7.3 omiten `CU-11` |
| Application | `DX-Developer-Experience.md` §6, `Glosario-UX.md` §2 | «**treinta y cuatro** condiciones» | 36 |
| Visor | `Especificacion-Funcional.md` §3.2 y §5.2 | «estos **seis** contratos de uso», «las **cinco** filas» | siete y seis |
| Visor | `Especificacion-Funcional.md` §5.3 | cobertura sobre **ocho** NB, «las **seis** NB restantes» | nueve desde el intake 1.3; **`NB-09` no aparece en ninguna parte del Visor** |
| Producto | `Alcance-Producto.md` §5 | «**Diez** exclusiones» | ocho vigentes: X-5 está tachada y X-2 debería estarlo |
| Producto | `Roadmap-Producto.md`, `Alcance-Producto.md` ×2, `NB-04` | «los **siete** escenarios de datos» | ocho desde el intake 1.7 (E-8) |

#### F26-21 · P2 · El manifiesto declara el punto de extensión con cinco funciones y atribuye al Visor una maqueta propia

`SDD/Intake/PRODUCT-MANIFEST-Fabrica-De-Geometria.md` enumera el contrato de fachada como «(`inicializar`, `cargarJson`, `seleccionarPieza`, `redimensionar`, `destruir`)» — cinco, contra las seis del intake §17.7 P.3 desde 1.6 — y afirma que Web y Visor «ejecutan la Fase B2, **cada uno con su maqueta propia**», mientras `GeometriaFactory-Visor/03-UX-UI-DX/README.md` §4 declara lo contrario: «este proyecto de código **no tuvo maqueta propia** por decisión del Product Owner». **Lo que sí está bien y conviene registrarlo:** la composición del manifiesto sigue coincidiendo con el intake §13 —siete proyectos, mismos tipos, mismas dependencias, mismo orden topológico—, de modo que **no correspondía re-derivarlo**: ni 1.7 ni 1.8 tocaron §13.

#### F26-22 · P2 · `AB2-08(b)` quedó con un segundo portador de estado en el árbol

`SDD/Maquetas/GeometriaFactory-Web/assets/js/Maqueta.js`, en `resaltarPorIndice()`: `b.parentNode.setAttribute('aria-selected', String(activo))`. `data-mq-nodo` vive hoy sólo en el `<li role="treeitem">`, de modo que `parentNode` es el `<ul role="group">`, al que se le asigna `aria-selected`. Contradice el comentario del propio archivo, «UN SOLO PORTADOR DE ROL Y DE ESTADO», y es código vivo. El camino gemelo `seleccionar()` sí quedó correctamente corregido: el arrastre quedó sólo en esta función.

#### F26-23 · P2 · `Contrato-Datos-Maqueta.md` declara abierto el escenario `E-8`, que el intake 1.7 cerró

`GeometriaFactory-Web/03-UX-UI-DX/Contrato-Datos-Maqueta.md` §5: «**Punto abierto del `PRODUCT-INTAKE` 1.5**: … Corresponde al Product Owner decidir si se incorpora un escenario `E-8` o si la condición queda declarada sin dato de prueba». El intake tiene **§20.E-8** (:1427) y su §21 lo mapea (:1480), y la propia `Bitacora-Validacion-Maqueta.md` declara `H-5` «Resuelto por el Product Owner».

#### F26-24 · P2 · La prohibición de resetear al administrador no tiene fuente declarada y se ancla distinto en cada proyecto

Ningún punto del intake dice que el reseteo de la cuenta de administrador no se admita: INV-08 (:642) habla de que esa cuenta está siempre `Habilitado` y no admite baja, y no menciona el reseteo. Domain lo ancla en INV-08 (`CU-13` §9), Contracts en `RN-01` e `INV-05` (`DXT-17`, `CU-08` §9), y **`INV-08` no está citado ni una vez en Contracts ni en Application**. La conclusión es correcta y nadie la contradice; lo que falta es una fuente única. Se relaciona con **F26-01**: es la tercera derivación de F-26 que la cadena sostiene sin respaldo en el intake.

#### F26-25 · P2 · Cambios sustantivos sin fila de control de cambios en dos proyectos

`git show a2d5b22` toca `Web/02-Especificacion-Funcional/Especificacion-Funcional.md` en cuatro pasajes de contenido —«cuatro operaciones»→«cinco», «veintisiete US»→«treinta», «once reglas»→«trece»— **sin subir versión y sin fila nueva**; las filas 1.2 y 1.3, añadidas después, describen otros cambios y no lo mencionan. El mismo commit toca `Application/03-UX-UI-DX/DX-Error-Messages.md` en dos correcciones reales, también sin fila, cuando el resto de la categoría sí deja constancia cuando absorbe sin subir versión. Y `US-30` se agregó a la trazabilidad de `Web/CU-04` y de `Wireframes-Panel-De-Cuentas.md` sin registro.

#### F26-26 · P2 · Trazabilidades de cabecera congeladas en versiones del intake anteriores a su contenido

Sólo `Contracts/CU-08` cita el intake **1.8** en su cabecera. Citan **1.7** documentos cuyo contenido depende de la precisión de RN-13 que sólo existe en 1.8 (`Contracts/CU-06` 1.3, `Contracts/CU-01` 1.2, `Application/CU-11`, `Web/Wireframes-Panel-De-Cuentas.md`, `Web/Wireframes-Ingreso.md`, `Web/Bitacora-Validacion-Maqueta.md`, `NB-06` 1.3). Citan **1.5** o **1.3** documentos cuyo contenido nuevo viene íntegro de 1.7 (`Roadmap-Producto.md` 1.3, `Alcance-Producto.md`, `Necesidades-Negocio.md`, `Contracts/DX-Error-Messages.md` 1.3, que cita 1.3 y **no menciona** F-26, RN-12, RN-13, INV-09, `RT-10`, `RT-11` ni `CU-08`). `Vision-Producto.md` sigue en **1.3**.

### 6.4 P3 — bajos

- **F26-27 · Cuatro tablas Markdown partidas por una línea en blanco**, lo que deja fuera de la tabla la fila que la tanda agregó: `Web/Linea-Base-Visual.md` §6.1 (la fila de **F-26**, que es el contenido más consecuente de la sección), `Web/Bitacora-Validacion-Maqueta.md` §4 (la fila `H-7`), y las tablas de control de cambios de `Domain/CU-03` y `Domain/CU-12`. En `Contracts`, `CU-01`, `CU-02` y `CU-06` tienen el mismo defecto antes de su fila 1.2. Además `Web/Wireframes-Panel-De-Cuentas.md` tiene una fila de control de cambios con cuatro columnas en una tabla de tres.
- **F26-28 · Filas de control de cambios fuera de orden cronológico** en `Domain/Especificacion-Funcional.md` (1.0, 1.1, 1.3, 1.2, 1.4, 1.5), `Domain/02-…/README.md`, `Contracts/DX-Developer-Experience.md` y `Contracts/DX-Error-Messages.md`.
- **F26-29 · Tres `rgba()` fuera del bloque `:root`** en `Estilos-Maqueta.css`, contra la afirmación absoluta del propio archivo «No hay literales visuales ad hoc». Los nueve literales hexadecimales que motivaron `AB2-07` sí desaparecieron.
- **F26-30 · Dos residuos de vocabulario** que la decisión B corrigió en el resto del corpus: `Web/Experiencia-De-Uso.md` «el alumno **entra** con una provisoria» y `Web/Wireframes-Panel-De-Cuentas.md` «Le vas a **fijar** una contraseña provisoria». Ninguno afirma sesión ni campo de escritura, y el resultado del diálogo muestra una provisoria generada, de modo que no son contradicciones; son las dos formulaciones que quedaron sin alinear.

---

## 7. Veredicto y condiciones para promover

> ## RECHAZADO

**El fundamento son los cuatro P0, y los cuatro son de la misma naturaleza: la tanda propagó hacia adentro y no cerró hacia afuera.**

`F26-01` es el más grave porque invierte la dirección de autoridad del framework. Once documentos de cuatro proyectos declaran, como decisión ratificada por el Product Owner, que la provisoria la produce el sistema y que el reseteo no exige cuenta habilitada; el intake, que es la fuente de registro y que la tanda dejó en 1.8 sin tocar, dice tres veces que **la fija el administrador** y no dice nada sobre el estado. No es un desfase de versión que se resuelva citando 1.9 en las cabeceras: es que la fuente afirma lo contrario. Mientras eso siga así, la regla que los propios proyectos escriben —«lo que el intake no declara, no se inventa»— resuelve la discrepancia a favor de la fuente, y quien construya el panel siguiendo esa regla pone el campo de contraseña que la decisión B vino a eliminar.

`F26-02` es el más caro en trabajo pendiente. F-26 es `Must Have`, y en el nivel producto **no existe**: no tiene necesidad de negocio, no tiene fila en el alcance, no tiene etapa —tampoco en el intake §15— y no tiene caso de uso previsto. Peor que la ausencia es lo que sí hay: `Alcance-Producto.md` lista X-2 como exclusión vigente con su salida destructiva, y `NB-01` y `NB-02` enseñan cuatro veces que ante un olvido de contraseña se da de baja y se vuelve a dar de alta perdiendo los trabajos. Ése es literalmente el agujero que el intake 1.7 declara haber cerrado, y sigue documentado como el procedimiento del producto.

`F26-03` y `F26-04` son el cierre incompleto de `AB2-04`, que era condición de promoción de la ronda anterior. La decisión se tomó bien y se fundó bien —la órbita ya existe en la herramienta que la cátedra usa, y diferirla sería retirar algo que el alumno ya tiene—, pero se escribió su conclusión sin ejecutarla: la transición `g` → `h` no incorporó el criterio que dos documentos afirman que incorporó, y dos documentos de nivel producto siguen declarando que F-25 no está comprometida. Un lector que verifique el cierre por el punto 4 del roadmap concluye que se hizo.

**Conviene decir con la misma claridad lo que no motiva el rechazo,** porque es la mayor parte de la tanda y está bien hecho. El P0 que causó el rechazo de la Fase B2 está cerrado de verdad, verificado por expansión de rangos y no por lectura de la nota de cobertura: 211 de 211. Los conjuntos cerrados de los cuatro proyectos los conté uno por uno y **cierran**, con la única excepción de `F26-10`. Las cuatro preguntas de coherencia tienen hoy **la misma respuesta en los cuatro proyectos**, que es lo más difícil que la tanda tenía por delante y que exigió deshacer tres lecturas divergentes: quedan dos residuos en Web y uno en su glosario UX, y ninguno en Domain, Contracts ni Application. Ninguno de los cinco proyectos contradice RA-01, RA-02 ni RA-03. Los snapshots de `_legacy/2026-08-09/` son byte a byte la versión previa real en los setenta y nueve casos verificados, con el número de versión correcto en el nombre. Y la §6.1 nueva de `Linea-Base-Visual.md` es un ejemplo de la disciplina que este informe echa de menos en otras partes: declara con nombre y fecha que F-26 y la sexta función **no se validaron visualmente**, se niega a asignarles identificador de línea de base porque «un identificador afirma que alguien lo miró y lo aprobó, y nadie lo miró», y pide una iteración 5 en lugar de inflar los recuentos. Verifiqué esas afirmaciones contra la maqueta y **son ciertas**.

**Condiciones para promover, en orden de bloqueo:**

1. **Resolver `F26-01`.** Es la única estrictamente bloqueante de las cuatro, porque las otras tres se corrigen editando y ésta requiere decisión: o el intake sube a **1.9** recogiendo las dos decisiones y reescribiendo F-26, RN-12 y CL-7 —que es lo que corresponde si el Product Owner efectivamente las tomó—, o la propagación de `20706ef` se revierte. Aprovechar la misma emisión para `F26-07` (RN-B6 apoyada en X-2), `F26-08` (el criterio de RN-12) y `F26-06` (§22 A-2 con siete etapas), que son defectos internos de la fuente y se corrigen en la misma pasada.
2. **Cerrar `F26-02`.** Es la de más volumen: F-26 al alcance con su etapa, X-2 tachada, `NB-01` y `NB-02` reescritas, RG-06 de la visión, una necesidad de negocio para F-26, y el recuento de `Must Have` a dieciocho. Requiere además que el intake §15 le asigne etapa, que es decisión de planificación y no de esta corrección.
3. **Cerrar `F26-04` y `F26-03` en la misma intervención.** Sumar el criterio de gobierno de los movimientos a la transición `g` → `h` de `Roadmap-Producto.md` §5.2, con lo que las dos afirmaciones pasan a ser ciertas; y alinear `Alcance-Producto.md` §4.1/§4.2 y `Necesidades-Negocio.md` con `NB-06`. Sin esto, `AB2-04` **no puede darse por cerrado** y el rechazo de la Fase B2 sigue en pie por su propia condición 3.
4. **Reponer las dos filas de control de cambios de `F26-05`** antes de cualquier otra edición sobre esos dos archivos: son los únicos dos documentos de nivel producto que la tanda tocó, y hoy no hay dónde leer qué cambió en ellos.
5. **Los P1 restantes** —`F26-09` a `F26-17`— son ediciones acotadas y ninguna requiere decisión ajena. Conviene absorber `F26-09`, `F26-11` y `F26-16` juntos, porque los tres son la misma clase: afirmaciones sobre lo que otra fuente declara, que dejaron de ser ciertas.
6. **Los P2 y P3 no bloquean**, y conviene absorberlos en la misma pasada porque `F26-20` es de una línea por celda y `F26-27` rompe el renderizado de la fila de F-26, que es contenido que alguien va a necesitar leer.

**Sobre la Fase B2.** Con dieciséis de dieciocho hallazgos cerrados y el P0 cerrado de verdad, la fase está **materialmente** resuelta; lo que impide declararla cerrada es `AB2-04`, cuya condición 3 pedía «decidir **y declarar**», y la declaración se escribió sin que el instrumento la acompañara.

**Ronda siguiente:** `SDD/Docs/Audit/F26-Propagacion-r2.md`. Este informe no se edita.

---

## 8. Lo que esta auditoría no reporta, y lo que no pudo verificar

**No se reporta, y se deja constancia para que una ronda posterior no lo levante:**

- **Las polisemias con contextos disjuntos.** Se evaluaron y se descartan: `Pendiente` en su referente de cuenta y en el de estado del trabajo, «contrato» con sus tres referentes, «papel» como rol de persona y como papel en la pieza, «sesión» en la capa de superficie y en la de contrato, «estado» del trabajo y situación de cuenta, y «pieza» como figura y como artefacto desplegable. Las seis están declaradas y resueltas en los glosarios de sus proyectos, ninguna es ambigua en su punto de uso, y exigir que se califiquen sería un defecto de este informe por el criterio negativo de `Vocabulario-Rules.md` §9.1 y §10.
- **Las filas históricas de control de cambios** que dicen «catorce códigos», «dieciséis códigos», «los seis casos de uso», «cinco funciones», «con sesión iniciada» o «el administrador fija la provisoria»: son registro de lo que la versión anterior decía, se leyeron una por una para separarlas del texto vivo, y no se reescriben.
- **Que la maqueta no incorpore F-26 ni `establecerMovimiento`.** Está declarado con precisión en `Linea-Base-Visual.md` §6.1 y en `Matriz-Sensado-Deriva.md`, con la iteración 5 pedida explícitamente y sin inflar los recuentos de la línea de base. Es un punto abierto **realmente abierto y bien declarado**, y lo verifiqué contra la maqueta: cero menciones de reseteo o provisoria en los doce HTML y los tres JS.
- **Que el Visor no implemente F-26 ni cite `NB-09` como necesidad propia.** Es correcto que no lo haga; lo que sí es hallazgo es que su tabla de cobertura de necesidades omita `NB-09` en lugar de declararla fuera de alcance (`F26-20`).
- **La ausencia de la categoría 04** en los proyectos con `usa_llm` == false.
- **`AB2-18`**, cerrado por declaración: la exclusión por prefijo sigue en el código de la maqueta, y eso es exactamente lo que el hallazgo original admitía como inocuo, con la advertencia de no heredarlo al sistema construido ahora escrita en el archivo y exigida por `SD-47`.

**No verificado, declarado como tal en lugar de suponerlo:**

- **La política de archivado en `_legacy/` al subir versión menor.** Los commits `894c0da` y `20706ef` subieron versión en Domain, Contracts, Application y Web **sin archivar** el estado anterior, mientras `43245da` sí archivó y el nivel producto también. Los propios documentos citan la regla («Sube minor y **archiva el estado anterior** por `Master-Prompt.md` §5»), pero **`Master-Prompt.md` y los `*-Rules.md` no están en este repositorio** (`find . -name 'Master-Prompt*'` → vacío), de modo que no pude leer si la regla exime el cierre de un punto abierto o la absorción de una corrección. Queda como observación, **no como hallazgo**. Lo mismo vale para la asimetría entre proyectos —Domain y Contracts archivan versiones intermedias, Application y Web no— y para que los snapshots del Visor conserven el mismo número de versión que el archivo vivo.
- **Los recuentos «antes» que cita el informe `B2-Maqueta-…-r1.md`** (135 atributos `style=`, nueve literales de color): se verificó únicamente el estado actual, que es cero en los dos casos.
- **Si el Product Owner efectivamente tomó las dos decisiones de `F26-01`.** Este informe verifica que la fuente de registro no las contiene y que para una de ellas dice lo contrario; no puede verificar qué se decidió fuera del repositorio, y por eso la condición 1 de §7 admite las dos salidas.

---

## 9. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. Auditoría independiente de la tanda de cuatro commits que cierra la Fase B2 y propaga la capacidad F-26 del intake 1.8. Verifica las nueve afirmaciones de la tanda una por una; contrasta las cuatro preguntas de coherencia de F-26 en los cinco proyectos de código, separando el texto vivo de las filas históricas de control de cambios; recuenta por extracción propia dieciocho conjuntos declarados —invariantes y reglas del intake, condiciones de Domain y de Application, conjunto cerrado de Contracts, funciones y condiciones del contrato de fachada, operaciones del panel, y los 211 identificadores de la línea de base contra las 61 sondas—; verifica el cierre de los dieciocho hallazgos de `B2-Maqueta-GeometriaFactory-Web-r1.md` contra el estado del árbol y no contra lo declarado; comprueba las tres reglas de arquitectura del producto en los cinco proyectos; verifica el versionado y la fidelidad byte a byte de setenta y nueve snapshots de `_legacy/2026-08-09/`; y contrasta contra el intake las citas cruzadas que más peso cargan. Treinta hallazgos: cuatro P0, trece P1, nueve P2 y cuatro P3. Veredicto: RECHAZADO, por la fuente que no registra las decisiones propagadas, por la ausencia de F-26 en el nivel producto y por el cierre sólo declarativo de `AB2-04`. | Auditor independiente (Arquitecto de Soluciones + QA Senior) |
