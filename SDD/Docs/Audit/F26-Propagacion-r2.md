# Auditoría de la propagación de F-26 y del cierre de la Fase B2 · ronda 2

| Campo | Valor |
| --- | --- |
| Producto | Fábrica de Geometría |
| Rama auditada | `sdd/fase-b2-cierre-y-reseteo-de-clave` |
| Alcance auditado | Los tres commits de corrección sobre el estado `96913db`: `e3468dd` (intake 1.9), `68228db` (nivel producto e intake 1.10) y `95c79c4` (los cinco proyectos de código). Se verifica el estado vivo del árbol, no lo que los commits dicen haber hecho |
| Motivo de la ronda | Dictaminar si se levanta el rechazo de `F26-Propagacion-r1.md` 1.0, que devolvió la tanda con **30 hallazgos** (4 P0, 13 P1, 9 P2, 4 P3) |
| Criterio de la ronda | **El instrumento, no la conclusión.** Ningún hallazgo se da por cerrado porque un documento declare haberlo cerrado: se comprueba en el texto vivo y con `git diff`. El defecto más caro de la tanda anterior fue exactamente ése —`F26-04`, una conclusión escrita sin ejecutar— y esta ronda lo trata como el primer riesgo a descartar |
| Fuera de alcance | La categoría 04 de los proyectos con `usa_llm` == false; su ausencia no es hallazgo |
| Auditor | Auditor independiente (Arquitecto de Soluciones + QA Senior), invocado desde cero, sin participación en la generación ni en la corrección de la tanda |
| Fecha | 2026-08-10 |
| Informes anteriores | `F26-Propagacion-r1.md` (leído, **no modificado**) |

---

## Tabla de contenido

- [1. Resumen ejecutivo](#1-resumen-ejecutivo)
- [2. Los treinta hallazgos de la ronda 1, uno por uno](#2-los-treinta-hallazgos-de-la-ronda-1-uno-por-uno)
  - [2.1 Los cuatro P0, verificados en el instrumento](#21-los-cuatro-p0-verificados-en-el-instrumento)
  - [2.2 Tabla de estado de los treinta](#22-tabla-de-estado-de-los-treinta)
- [3. Lo que las correcciones podrían haber roto](#3-lo-que-las-correcciones-podrían-haber-roto)
- [4. Las cuatro preguntas de coherencia](#4-las-cuatro-preguntas-de-coherencia)
- [5. Hallazgos nuevos](#5-hallazgos-nuevos)
- [6. Dictamen](#6-dictamen)
- [7. Lo que este informe no reporta, y lo que no pudo verificar](#7-lo-que-este-informe-no-reporta-y-lo-que-no-pudo-verificar)

---

## 1. Resumen ejecutivo

**Se levanta el rechazo.** Los cuatro P0 y los trece P1 de la ronda 1 están cerrados, y `F26-04` —el que motivó la advertencia más dura del informe anterior— está cerrado **en el instrumento**: la transición `g` → `h` de `Roadmap-Producto.md` §5.2 tiene hoy **siete** criterios y el séptimo es el gobierno independiente de los dos movimientos de F-25, contado sobre la celda viva y confirmado con `git diff`. No es una afirmación repetida: la sección cambió.

La corrección resolvió el problema de fondo que r1 señalaba. El intake subió a **1.9** para registrar las dos decisiones que la tanda había propagado sin escribir —entran **RN-14** y **RN-15**— y a **1.10** para cerrar los dos residuos internos que esa emisión dejó, el riesgo `RN-B6` y la asunción `A-2`. Con eso la dirección de autoridad del framework quedó restablecida: los once documentos que declaraban ratificadas por el Product Owner dos decisiones ausentes de la fuente ahora tienen fuente, y la fuente ya no dice lo contrario.

Quedan **ocho hallazgos nuevos**, ninguno P0 ni P1: uno P2 de la clase «cita falsa a otra fuente» —tres documentos de nivel producto afirman que el intake §22 «todavía transcribe 7 de 7» cuando el **mismo commit** que lo escribió corrigió esa fila—, un P2 que es la continuación de un hallazgo de r1 que nadie tocó (`F26-21`, el manifiesto), y seis P3 de recuento y de vocabulario. Ninguno bloquea, y r1 ya había declarado en su §7 punto 6 que los P2 y P3 no bloquean.

Conviene decir lo que no encontré, porque es lo que más se buscó: **ningún identificador fantasma nuevo**, **ninguna contradicción con RA-01, RA-02 ni RA-03**, y **las cuatro preguntas de coherencia tienen hoy la misma respuesta en los cinco proyectos, sin un solo residuo**. Los dos residuos de Web y el del glosario UX que r1 había dejado sobrevivir están corregidos, y los recuentos derivados de las quince reglas cierran en los cinco proyectos.

---

## 2. Los treinta hallazgos de la ronda 1, uno por uno

### 2.1 Los cuatro P0, verificados en el instrumento

**`F26-04` — cerrado, y esta vez ejecutado.** Es el que había que mirar primero. `git diff 96913db..HEAD -- SDD/Docs/00-Contexto/Roadmap-Producto.md` muestra que **§5.2 sí aparece en el diff** esta vez, y que la celda `g` → `h` sumó un criterio. Los conté sobre el texto vivo: figuras del escenario semilla, diez recorridos sin degradar, misma disposición, cero peticiones de red, sincronización árbol-escena, vista única del administrador, y —nuevo— «**Los dos movimientos automáticos de F-25 se gobiernan por separado**… su estado inicial lo fija la pieza pública pasando dos valores de verdad, porque es ella —y no la visualización— la que consulta la preferencia de movimiento reducido». **Seis pasaron a siete.** Con eso, la afirmación de `Roadmap-Producto.md` §3 punto 4 y la de `NB-00006` §5 —que r1 declaró falsas— pasan a ser verdaderas, y `AB2-04` queda cerrado por su instrumento. La fila 1.4 del control de cambios lo declara con la misma precisión y sin adornarlo: «la fila tenía seis criterios y ninguno lo mencionaba».

**`F26-01` — cerrado.** El intake está en **1.10** y §4.1 incorpora **RN-14** («la contraseña provisoria la produce el sistema, no la escribe el administrador… no es adivinable y no se repite») y **RN-15** («resetear no exige que la cuenta esté habilitada… es una operación sobre la credencial y no una transición de la máquina de estados»). Las tres secciones que r1 citaba como contradicción frontal están reescritas: §4 F-26 (:177) dice hoy «**el sistema produce una contraseña provisoria**… el panel **no lleva campo de contraseña**», §4.1 RN-12 (:213) dice «**El sistema produce** una contraseña provisoria», y §7 CL-7 (:286) «el sistema produce una contraseña provisoria que él le comunica». La fila 1.9 del control de cambios registra las dos decisiones y asume el defecto con nombre: «el defecto es del orquestador, que propagó hacia adentro sin cerrar hacia afuera».

**`F26-02` — cerrado.** Verificado por conteo propio y no por la nota. `Alcance-Producto.md` §4.1 declara **dieciocho** capacidades `Must Have` y su tabla tiene dieciocho filas; extraje los identificadores de la tabla del intake §4 con prioridad `Must Have` y de la tabla de §4.1 y **coinciden fila por fila**: F-01…F-12, F-21…F-24, F-25 y F-26. `Alcance-Producto.md` §5 tacha **X-2** como se tachó X-5, con su motivo escrito, y su preámbulo declara «diez exclusiones… de las cuales **ocho siguen vigentes**», que es el recuento correcto. `Vision-Producto.md` RG-06 pasó a «**Baja desde el 2026-08-09**» y perdió la mitigación por baja. `NB-00001` y `NB-00002` ya no enseñan el procedimiento destructivo: `grep -rn "dando de baja y volviendo a dar de alta\|cuesta la cuenta y sus trabajos"` sobre las dos categorías devuelve **cero ocurrencias vivas** —las únicas coincidencias están dentro de filas de control de cambios que describen la corrección, y ésas no son hallazgo—. F-26 entra al catálogo **por fusión en `NB-00002`**, con `CU-29` y `CU-30` previstos y con el fundamento escrito de por qué no cae en `NB-00001`, apoyado en **RN-15**: resetear no es un acto de admisión. Y `Roadmap-Producto.md` §2.1, §3 y §5.2 la ubican en la fase `d` con tres razones propias y con la constancia explícita de que **el intake §15 no le asigna etapa**, de modo que la ubicación es decisión de planificación declarada como tal.

**`F26-03` — cerrado.** F-25 salió de `Alcance-Producto.md` §4.2 y está en §4.1 con el resto del alcance comprometido; el preámbulo de §4.2 declara adónde se fue en lugar de dejar el hueco, y `F-13` queda como la única capacidad de prioridad menor en etapa comprometida. `Necesidades-Negocio.md` ya no dice que F-25 «no está comprometida»: §5.1 la declara `Must Have` y su nota de cierre dice de las dos —F-25 y F-26— que «**viven en etapa comprometida y la comprometen**», con remisión a los criterios de transición que ahora las recogen. La contradicción viva entre las dos categorías desapareció.

### 2.2 Tabla de estado de los treinta

| Id | Sev. r1 | Estado verificado | Cómo lo comprobé |
| --- | --- | --- | --- |
| `F26-01` | P0 | **Cerrado** | Lectura de intake §4 (:177), §4.1 RN-12 (:213), RN-14 (:211), RN-15 (:212) y §7 CL-7 (:286); cabecera en `1.10`; filas 1.9 y 1.10 del control de cambios |
| `F26-02` | P0 | **Cerrado** | Conteo fila por fila de `Must Have` en intake §4 contra `Alcance-Producto.md` §4.1 (dieciocho, mismos identificadores); §5 con X-2 tachada y «ocho vigentes»; `grep` de las cuatro formulaciones destructivas sobre `00-Contexto` y `01-Necesidades-Negocio` → cero vivas; `NB-00002` §9 y §5.3 |
| `F26-03` | P0 | **Cerrado** | Lectura de §4.1 y §4.2 de `Alcance-Producto.md` y de §5.1 y §6 de `Necesidades-Negocio.md` |
| `F26-04` | P0 | **Cerrado en el instrumento** | Conteo de los criterios de la celda `g` → `h` de §5.2 (siete) y `git diff 96913db..HEAD` sobre `Roadmap-Producto.md`, donde §5.2 **sí** aparece |
| `F26-05` | P1 | **Cerrado** | `grep -oE "^\| 1\.[0-9]"` sobre §7 de `Roadmap-Producto.md` (1.0…1.4) y sobre el control de cambios de `NB-00006` (1.0…1.4): la fila **1.3 que faltaba está repuesta** en los dos, y declarada como repuesta |
| `F26-06` | P1 | **Cerrado** | Intake §22 A-2 (:1548) dice hoy «**8 de 8 etapas** [corregido el 2026-08-09…]»; `Vision-Producto.md` §5 y §6, `Alcance-Producto.md` §3 y §6.1, `00-Contexto/README.md`, `Necesidades-Negocio.md` §6 y `01-Necesidades-Negocio/README.md` declaran el punto **resuelto**. Ver `N-1`: la forma en que tres de ellos lo declaran introduce un defecto nuevo |
| `F26-07` | P1 | **Cerrado** | Intake §11 (:342): la fila `RN-B6` está **tachada** con «Riesgo cerrado el 2026-08-09» y el motivo escrito. Ver `N-5` sobre las citas vivas que quedaron |
| `F26-08` | P1 | **Cerrado** | Intake §4.1 RN-12 (:213), columna de verificación: dice hoy «**Se autentica con la provisoria, cambia la contraseña y los ve**». El verbo que no pasaba desapareció |
| `F26-09` | P1 | **Cerrado, y bien** | `Especificacion-Funcional.md` (:99) y `02-…/README.md` (:82) de Domain ya no afirman que §17.1.P.2 lo declara: declaran que la fuente de la lectura es «**la columna “regla de negocio que sostiene” de INV-09… y no su prosa**», y dejan escrita la ambigüedad del intake. Es la salida correcta |
| `F26-10` | P1 | **Cerrado** | `Contracts/DX-Error-Messages.md` §3.2 (:118) dice «**Diecisiete entradas de código**… más una fila de retiro», concordante con §2.2 (:73) y §3.3 |
| `F26-11` | P1 | **Cerrado** | `Visor/Especificacion-Funcional.md` §4 habla de «cada una de las **seis** funciones»; `Definicion-Contrato-De-Fachada.md` §5.5 pasó a «Punto abierto resuelto, **y consolidado aguas arriba**»; ninguna ocurrencia viva de F-25 como `Should Have` en el Visor |
| `F26-12` | P1 | **Cerrado** | `Web/Bitacora-Validacion-Maqueta.md` (:99) dice hoy «`PRODUCT-INTAKE` 1.7 lo consolidó, y **la propagación al nivel producto ya está hecha**»; su §4 suma la fila que registra la propagación de 1.7 y 1.9 a las dos categorías de nivel producto; fila 1.2 del control de cambios |
| `F26-13` | P1 | **Cerrado** | `grep -rn "con sesión iniciada\|sesión otorgada"` sobre `Docs/Proyectos` excluyendo `_legacy`: las ocurrencias vivas restantes son precondiciones legítimas de casos de aceptación con sesión ordinaria (`CU-02` CA-05, `CU-03` CA-02/CA-03, `CU-04` CA-07, `CU-09` CA-05). **Los dos residuos de `Linea-Base-Visual.md` §6.1 y `Wireframes-Ingreso.md` §8 ya no están** |
| `F26-14` | P1 | **Cerrado** | `Application/DX-Developer-Experience.md` §1.4 se titula «Las **cuatro** negativas» y su tabla las tiene; `Guia-Onboarding-Developer.md` §3.5 idem; `grep -c CAMBIO_DE_CONTRASENA_PENDIENTE` sobre los dos archivos → **2 y 2**, contra los 0 de r1 |
| `F26-15` | P1 | **Cerrado** | `Application/CU-11`: `CA-08` ya no se cita, y `CA-03` (:104) dice hoy «por **CU-03 FA-05**» |
| `F26-16` | P1 | **Cerrado** | `Contracts/CU-01` §10 ya no declara el punto abierto; fila 1.3 del control de cambios lo registra contra el intake 1.10 |
| `F26-17` | P1 | **Cerrado** | `Domain/Especificacion-Funcional.md` (:215) declara el punto **resuelto**, y `Definicion-Modelo-De-Dominio.md` §2.2 (:89, :90) **sí declara hoy** «Fecha de creación» y «Fecha de última modificación», con la aclaración de que las aporta el consumidor |
| `F26-18` | P2 | **Cerrado** | `Application/DX-Error-Messages.md` §2.5 (:208, :209) lista hoy `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` y `RESETEO_CON_ARRASTRE_DE_TRABAJOS` con su motivo de inalcanzabilidad |
| `F26-19` | P2 | **Cerrado** | `Web/Glosario-UX.md` (:44): «la credencial que **el sistema produce**… **El panel no tiene dónde escribirla** (RN-14)» |
| `F26-20` | P2 | **Parcial** | Verifiqué las diecisiete filas por su cadena literal. Cierran dieciséis: rangos `RN-01 a RN-15` en los cuatro proyectos, «los once casos de uso» en Application, `NB-00009` presente en la cobertura del Visor, «diez exclusiones / ocho vigentes» en el alcance, «ocho escenarios» en `Roadmap` §2.1. **Sobrevive la fila de los escenarios de datos en dos archivos que r1 no había nombrado** → `N-4` |
| `F26-21` | P2 | **Abierto** | `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` sigue en **1.1** y no fue tocado por ninguno de los tres commits. §5 sigue enumerando la fachada con **cinco** funciones (:149) y sigue afirmando que Web y Visor ejecutan la Fase B2 «**cada uno con su maqueta propia**» (:152), contra `Visor/03-UX-UI-DX/README.md` §4 |
| `F26-22` | P2 | **Cerrado** | `Maqueta.js`: `grep -n "aria-selected"` da hoy `b.setAttribute(...)` en las dos funciones (:1259 y :1974). El `b.parentNode.setAttribute` desapareció |
| `F26-23` | P2 | **Cerrado** | `Web/Contrato-Datos-Maqueta.md` §5 (:108) dice «**Punto abierto resuelto por el Product Owner**» |
| `F26-24` | P2 | **Cerrado** | El intake tiene hoy fuente única: **RN-15** (:212) cierra con «Sigue sin admitirse sobre la cuenta de administrador (**INV-08**)». `INV-08` aparece ahora citado en Contracts (`CU-08`, `DX-Error-Messages.md`) y en Application (`CU-11`, `DX-Error-Messages.md`), donde r1 contó cero |
| `F26-25` | P2 | **No verificado** | Ver §7. No pude comprobar con costo razonable si las filas de control de cambios repuestas describen exactamente los pasajes que `a2d5b22` había cambiado sin registro |
| `F26-26` | P2 | **Parcial** | Las cabeceras se movieron —doce documentos citan hoy `1.10` y el nivel producto cita `1.9`—, pero `Contracts/CU-01` y `Contracts/CU-06` conservan `1.7` en su trazabilidad de cabecera mientras su propia fila nueva declara haberse escrito «contra `PRODUCT-INTAKE` **1.10**» → `N-6` |
| `F26-27` | P3 | **Cerrado** | Barrido de tablas partidas por línea en blanco sobre los siete archivos nombrados: **cero** coincidencias. La fila de F-26 de `Linea-Base-Visual.md` §6.1 renderiza dentro de su tabla |
| `F26-28` | P3 | **Cerrado** | `grep -oE "^\| 1\.[0-9]+"` sobre los cuatro archivos: `Domain/Especificacion-Funcional.md` 1.0→1.7 ascendente, `Domain/02-…/README.md` 1.0→1.6, `Contracts/DX-Developer-Experience.md` y `Contracts/DX-Error-Messages.md` ascendentes. Se corrigió además `Contracts/Glosario-UX.md`, que tenía el mismo defecto sin estar reportado |
| `F26-29` | P3 | **Cerrado** | `grep -n "rgba(" Estilos-Maqueta.css` → tres ocurrencias, las tres en **:76, :77, :78**, dentro del bloque de tokens y como valor de una custom property |
| `F26-30` | P3 | **Cerrado** | `grep` de las dos formulaciones sobre `Docs/Proyectos`: ninguna ocurrencia viva; la única coincidencia está en la fila 1.3 de `Wireframes-Panel-De-Cuentas.md` que declara la corrección |

**Recuento: 26 cerrados, 2 parciales (`F26-20` y `F26-26`), 1 abierto (`F26-21`) y 1 no verificado (`F26-25`).** Ninguno de los que quedan es P0 ni P1. `F26-06` se cuenta entre los cerrados: el punto abierto está efectivamente resuelto en los seis documentos y en la fuente; lo que `N-1` levanta no es su reapertura sino la coletilla falsa con que tres de ellos lo declaran.

---

## 3. Lo que las correcciones podrían haber roto

**Las quince reglas y los recuentos derivados.** Conté los archivos de `Domain/02-Especificacion-Funcional/Reglas-De-Negocio/`: son **quince**, `RN-01` a `RN-15`, serie contigua y sin huecos. Los rangos derivados cierran en los cinco proyectos: `RN-01 a RN-15` en las cinco fichas «Reglas de negocio relevantes» de Domain, Contracts, Application y Web; «Las **quince** reglas del producto viven en `GeometriaFactory-Domain`» en Application §7 y en Web §5 y §8; «La serie es **contigua de RN-01 a RN-15**» en Domain. El defecto que r1 registraba como «contigua de RN-01 a RN-11» desapareció.

**Los archivos `RN-14` y `RN-15` de Domain.** Existen, y son de la calidad que la categoría exige. `RN-14` transcribe el enunciado del intake sin ampliarlo, declara con precisión inusual que **no se ejerce en este proyecto de código** —«el dominio no produce la provisoria y no la conoce: llega ya derivada (`PRODUCT-INTAKE` §17.1.P.5)»— y remite a dónde sí se ejerce; declara además que el mecanismo es de 05 y no de esta categoría. `RN-15` enuncia la ausencia de precondición y cierra sobre la cuenta de administrador por INV-08. **No contradicen a RN-12 ni a RN-13**: RN-12 conserva la cuenta y los trabajos, RN-13 acota lo que la cuenta puede hacer hasta cambiar la provisoria, RN-14 dice quién produce el valor y RN-15 dice sobre qué situaciones procede la operación. Son cuatro predicados sobre objetos distintos y ninguno se pisa. La tabla de correspondencia con invariantes les asigna guion a las dos, con el motivo declarado.

**Nivel producto.** Verificado en §2.1: dieciocho `Must Have` contados fila por fila contra el intake, X-2 tachada, F-26 incorporada por fusión en `NB-00002` con su fundamento de partición, y criterios nuevos en **dos** transiciones de §5.2 —cinco en `d` → `e`, contados uno por uno, y uno en `g` → `h`—.

**Las dieciséis filas de control de cambios mal formadas.** Ésta la verifiqué con el instrumento más directo: `git diff --word-diff=plain -U0` sobre `SDD/Docs` y `SDD/Intake`, filtrando las líneas que empiezan con `| 1.`. El resultado es **exactamente dieciséis** filas modificadas, y en las dieciséis el cambio es **el mismo y sólo ése**: un separador de celda `|` reemplazado por la etiqueta literal `**Autor:**`. Es decir, la corrección plegó la cuarta celda —la de autor— dentro de la celda de cambios, con rótulo explícito, para que la fila tuviera las tres columnas que su tabla declara. **El texto de las dieciséis filas no fue alterado en ninguna otra posición**: comparadas palabra por palabra, no hay una sola inserción o supresión fuera de ese punto. Lo que decían, siguen diciéndolo.

---

## 4. Las cuatro preguntas de coherencia

Las cuatro tienen hoy **la misma respuesta en los cinco proyectos**, y a diferencia de la ronda 1 **no queda ningún residuo**.

| Pregunta | Respuesta única | Verificación |
| --- | --- | --- |
| ¿La cuenta reseteada obtiene sesión de trabajo? | **No.** Se autentica y se la deriva | La formulación «no obtiene sesión de trabajo» aparece en los cinco proyectos —`Domain/RN-13`, `Contracts/CU-01` y `CU-08`, `Application/CU-03` y `Especificacion-Funcional.md` §7, `Web/CU-02`, `CU-03`, `CU-04` y `Experiencia-De-Uso.md`—. Los dos residuos de Web que r1 dejó abiertos están corregidos |
| ¿El reseteo exige estado habilitado? | **No.** Procede sobre `Pendiente`, `Habilitado` y `Bloqueado` | Fuente propia desde el intake 1.9: **RN-15**. Citada como regla aplicable en `Application/CU-11` §9, `Application/Especificacion-Funcional.md` §6, `Domain/CU-13`, `Contracts/CU-08` y `Web/CU-04`. `Application/CU-11` (:125) lo dice sin ambigüedad: «El reseteo **NO exige** la cuenta en estado `Habilitado`» |
| ¿Quién produce la provisoria? | **El sistema** | **RN-14** en el intake; `Web/Glosario-UX.md` (:44), que era el último disidente, dice hoy «la credencial que **el sistema produce**… el panel no tiene dónde escribirla (RN-14)». Coincide con `Web/Glosario-Funcional.md`, con `Contracts/CU-08` y con `Domain/CU-13` |
| ¿Se admite resetear la cuenta de administrador? | **No** | Y ahora **con fuente única**: RN-15 cierra con «Sigue sin admitirse sobre la cuenta de administrador (INV-08)». Los cuatro proyectos que la modelan lo anclan ahí; `INV-08` dejó de estar ausente en Contracts y en Application |

**Contradicciones con RA-01, RA-02 y RA-03: ninguna.** Revisé los noventa y cuatro lugares donde los tres se citan en los cinco proyectos. El criterio nuevo que entró a `Roadmap-Producto.md` §5.2 es el punto donde más fácil habría sido romper RA-02, y está redactado del lado correcto: el estado inicial de los movimientos «lo fija la pieza pública pasando dos valores de verdad, **porque es ella —y no la visualización— la que consulta la preferencia de movimiento reducido**». Eso confirma RA-02 en lugar de aflojarlo, y coincide con lo que el intake §17.7 declara desde 1.6 y con `CU-07` del Visor.

---

## 5. Hallazgos nuevos

**Ocho, ninguno P0 ni P1.**

#### N-1 · P2 · Tres documentos de nivel producto afirman que el intake conserva un residuo que el mismo commit corrigió

- **Dónde:** `SDD/Docs/00-Contexto/Alcance-Producto.md` §6.1, fila A-2 (:172); `SDD/Docs/00-Contexto/README.md` (:84); `SDD/Docs/01-Necesidades-Negocio/Necesidades-Negocio.md` §6 (:211).
- **Qué dice:** los tres cierran el punto de las siete u ocho etapas declarando —correctamente— que el Product Owner lo resolvió en el intake §8, y los tres agregan la misma coletilla: «**su propia fila A-2 de §22 todavía transcribe “7 de 7”**, que es un residuo de la fuente y no un target vivo», «Lo único que subsiste es un residuo de la fuente: la fila A-2 del intake §22 todavía transcribe «7 de 7», **y corregirla es del Product Owner sobre su propio documento**».
- **Por qué es falso:** el intake §22 A-2 (:1548) dice hoy «Los targets de las cuatro métricas de negocio (**8 de 8 etapas** [corregido el 2026-08-09: decía «7 de 7», residuo de antes de que §8 pasara a contar todas las etapas comprometidas]…)». **El residuo no existe.** Y no lo corrigió una intervención posterior: `git show 68228db` toca en el mismo commit `Alcance-Producto.md` y el intake, y es **ese mismo commit** el que sube el intake a 1.10 y arregla A-2. La fila 1.10 del control de cambios del intake lo declara textualmente.
- **Cómo lo verifiqué:** lectura de intake :1548 y :1595, `git show 68228db --stat`, y lectura de los tres pasajes vivos.
- **Por qué P2 y no P1:** el número que los tres documentos usan es el correcto —«8 de 8»— y nadie deriva un plan equivocado de acá; lo que falla es una afirmación sobre el estado de otra fuente, y encima una que delega en el Product Owner una tarea ya hecha. Es la misma familia que `F26-12` y `F26-04`, y por eso conviene nombrarla: **la disciplina de declarar lo que quedó pendiente aguas arriba sólo funciona si se verifica aguas arriba antes de escribirla**, incluso —sobre todo— cuando la corrección aguas arriba viaja en el mismo commit.

#### N-2 · P2 · `F26-21` sigue abierto: el manifiesto no fue tocado

`SDD/Intake/PRODUCT-MANIFEST-Fabrica-De-Geometria.md` sigue en **1.1** y no aparece en ninguno de los tres commits de corrección. Conserva los dos defectos que r1 reportó: §5 enumera el punto de extensión con **cinco** funciones —`inicializar`, `cargarJson`, `seleccionarPieza`, `redimensionar`, `destruir`— contra las seis que el intake §17.7 P.3 declara desde 1.6, y afirma que Web y Visor «ejecutan la Fase B2, **cada uno con su maqueta propia**», contra `Visor/03-UX-UI-DX/README.md` §4, que declara lo contrario por decisión del Product Owner. No es una regresión: es un hallazgo de r1 al que la tanda no llegó. Lo repito acá porque es el único P2 de r1 que quedó **entero**.

#### N-3 · P3 · La prosa del intake §17.1.P.2 dice «seis» y enumera siete

Intake (:646): «Las **seis** reglas que no tienen invariante asociado —RN-07…, RN-08…, RN-09…, **RN-12**…, **RN-14**… y **RN-15**— no lo tienen porque describen comportamientos… **RN-11 tampoco**: es una regla de alcance de consulta». Seis más RN-11 son siete, y quince menos siete son ocho reglas con invariante, no nueve. Es el mismo desfase que la versión 1.8 ya tenía —decía «cuatro» y enumeraba cinco por la misma vía—, arrastrado al reescribirse en 1.9. **No contamina aguas abajo**: Domain cuenta «nueve con invariante y seis sin», que es la aritmética correcta bajo la lectura de la columna de INV-09, y declara explícitamente que esa lectura no es la de la prosa (ver `F26-09`). El defecto es interno de la fuente y de una sola oración.

#### N-4 · P3 · Cuatro residuos de «los siete escenarios de datos», en dos archivos que r1 no había nombrado

- `Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-08-Texto-Original-Conservado-Integro.md`, trazabilidad de cabecera (:10) y §6 (:56).
- `Web/03-UX-UI-DX/Contrato-Datos-Maqueta.md`, trazabilidad de cabecera (:10) y la evidencia `EV-10` de §7 (:130), que declara «§20, los **siete** escenarios de datos de los que sale todo ejemplo de §2».

El intake §20 tiene **ocho** desde 1.7, con `E-8` incorporado, y el resto del corpus ya lo dice así —incluido `Roadmap-Producto.md` §2.1, corregido en esta tanda—. Es la misma familia de `F26-20` y del mismo tamaño: una palabra por celda. Se vuelve levemente más caro en `EV-10`, porque ahí el número es parte de una **declaración de evidencia** sobre qué se verificó contra qué.

#### N-5 · P3 · Cinco citas vivas a `RN-B6` como riesgo vigente, después de que el intake lo tachara

`NB-00001-Control-De-Admision-Al-Laboratorio.md` lo cita tres veces —cabecera, §2 (:37) y §4 (:52)— como fuente de que la baja arrastra los trabajos, y `Domain/RN-07` y `Domain/RN-12` lo listan en su trazabilidad de cabecera, esta última **citando el intake 1.10**, que es justamente la versión que lo cerró. La afirmación que sostienen es verdadera y vive también en `CL-6` y en `RN-07`, de modo que **ninguna conclusión cambia**; lo que falla es la referencia, que apunta a una fila tachada. Es el precio de que el cierre de `RN-B6` (`F26-07`) haya viajado en el mismo commit que el nivel producto.

#### N-6 · P3 · Dos casos de uso de Contracts declaran haberse escrito contra el intake 1.10 y su cabecera sigue citando 1.7

`Contracts/CU-01-Contrato-De-Canje-De-Credenciales-Y-Sesion.md` y `Contracts/CU-06-Contrato-De-Respuesta-De-Error.md`: la trazabilidad de cabecera dice «`PRODUCT-INTAKE` 1.7», y la fila de control de cambios que la tanda les agregó dice «contra `PRODUCT-INTAKE` **1.10**». Es contradicción interna de un solo campo, y es el remanente de `F26-26`.

#### N-7 · P3 · Las reglas nuevas entraron a §4.1 fuera de orden y bajo un rótulo que no las cubre

En el intake §4.1, la segunda tabla está encabezada por «**Dos reglas nuevas que el circuito de revisión introduce** y que no existían en RF §7, declaradas por el Product Owner el **2026-08-08**», y contiene hoy seis filas: RN-10, RN-11, **RN-14**, **RN-15**, RN-12 y RN-13. Cuatro de las seis no vienen del circuito de revisión ni son del 2026-08-08, y **RN-14 y RN-15 quedaron intercaladas entre RN-11 y RN-12**, de modo que quien busque RN-12 después de RN-11 encuentra otra cosa. El rótulo ya estaba mal desde 1.7, cuando entraron RN-12 y RN-13; lo nuevo es el desorden numérico. Ninguna regla cambia de contenido.

#### N-8 · P3 · Tres filas de control de cambios nuevas con énfasis Markdown desbalanceado

`Web/02-Especificacion-Funcional/Especificacion-Funcional.md` fila 1.4, `Application/02-Especificacion-Funcional/Especificacion-Funcional.md` fila 1.4 y `Application/CU-11` fila 1.3 abren `**Absorbe el **PRODUCT-INTAKE** 1.10**…`, con asteriscos anidados que el renderizador resuelve al revés de la intención. No afecta al contenido.

---

## 6. Dictamen

**APROBADO. Se levanta el rechazo de `F26-Propagacion-r1.md`.**

El fundamento, en el orden en que importa:

1. **Las cuatro condiciones bloqueantes de r1 §7 están cumplidas, y las cuatro en el instrumento.** `F26-01` se resolvió por la salida que correspondía —el intake sube y registra— y no por la contraria; `F26-02` se cerró con el volumen que requería, incluida la etapa que el intake no asigna y que el roadmap ubica declarando que la ubica; `F26-03` alineó las dos categorías de nivel producto; y `F26-04` —el que r1 tipificó como «simula verificación»— **ejecutó lo que se había afirmado**: seis criterios pasaron a siete, verificado por conteo y por `git diff`. Ese punto era el que definía la ronda, y está limpio.

2. **Los trece P1 están cerrados**, incluidos los tres de la clase «afirmaciones sobre lo que otra fuente declara» (`F26-09`, `F26-11`, `F26-16`) que r1 recomendaba absorber juntos. `F26-09` merece mención aparte: en vez de tapar la ambigüedad del intake, Domain la declaró, nombró qué parte de la fuente sostiene su lectura y por qué. Es el patrón que este framework debería premiar.

3. **La coherencia entre proyectos, que era lo más difícil, quedó completa.** Las cuatro preguntas tienen una sola respuesta en los cinco proyectos y **cero residuos**, contra los tres que r1 encontró. Las dos derivaciones que se sostenían sin respaldo —quién produce la provisoria y qué estado exige el reseteo— tienen hoy regla propia, y la tercera —la cuenta de administrador— tiene fuente única.

4. **Lo que queda no bloquea, y el propio r1 lo había dicho.** Un P2 abierto entero (`F26-21`, el manifiesto), un P2 nuevo (`N-1`), dos parciales de recuento y seis P3. Ninguno cambia lo que alguien construiría leyendo esta documentación.

**Lo que conviene absorber antes de la próxima emisión**, en orden de valor y sin condicionar la promoción:

1. **`N-1`**, porque es un defecto de método más que de contenido, y porque es la tercera vez que la cadena escribe una afirmación sobre el estado de otra fuente sin releerla. Corregir las tres coletillas es una línea por documento.
2. **`F26-21` / `N-2`**: el manifiesto sube a 1.2, con las seis funciones y con la maqueta del Visor declarada como el propio Visor la declara.
3. **`N-3` y `N-7`** en la misma emisión del intake, que son de una oración y de un reordenamiento de dos filas.
4. **`N-4`, `N-5`, `N-6` y `N-8`**, que son de una celda cada uno.

---

## 7. Lo que este informe no reporta, y lo que no pudo verificar

**No reporto como hallazgo**, y conviene dejarlo escrito para que no reaparezca en una ronda siguiente:

- **Que F-26 y la sexta función de la fachada no tengan identificador de línea de base.** `Web/Linea-Base-Visual.md` §6.1 declara con nombre y fecha que no se validaron visualmente, se niega a asignarles identificador porque «un identificador afirma que alguien lo miró y lo aprobó», y pide una **iteración 5**. Es un punto abierto correctamente declarado como abierto, que es lo contrario de un defecto.
- **Que el intake §15 siga sin asignarle etapa a F-26.** `Roadmap-Producto.md` §3 lo declara explícitamente y funda la ubicación en `d` como decisión de planificación propia, igual que hizo con F-25; `Necesidades-Negocio.md` §5.1 lo recoge con la misma constancia. Está declarado, no escondido.
- **Que el nivel producto cite el intake `1.9` mientras el intake está en `1.10`.** El contenido que 1.10 agrega —el cierre de `RN-B6` y la corrección de `A-2`— ya está reflejado en esos documentos; el desfase es de la etiqueta y no del contenido. Lo único que sí reporto de ahí es `N-1`, que no es un desfase sino una afirmación falsa.
- **Las polisemias con contextos disjuntos.** El caso más visible es «cambio de contraseña pendiente», que en `GeometriaFactory-Domain` nombra un atributo de la entidad y en `GeometriaFactory-Contracts` una condición transportada; el glosario de Contracts lo declara y lo separa. No es hallazgo.
- **Las ocurrencias dentro de filas históricas de control de cambios** que describen una corrección usando la formulación corregida. Son registro, no contenido vigente.

**No pude verificar:**

- **`F26-25`.** Comprobar que las filas de control de cambios repuestas describen exactamente los pasajes que `a2d5b22` había modificado sin registro exige reconstruir ese diff pasaje por pasaje contra el texto de cada fila, y el presupuesto de esta ronda no lo permitió. Lo declaro **no verificado**, no cerrado.
- **La categoría 08 y `Matriz-Sensado-Deriva.md`**, que r1 auditó y esta ronda no volvió a recorrer: ninguno de los tres commits de corrección la toca (`git diff --stat 96913db..HEAD`), de modo que no hay motivo para suponer regresión, pero tampoco la comprobé.
- **Si el Product Owner efectivamente tomó las dos decisiones que ahora son RN-14 y RN-15.** Este informe verifica que la fuente de registro las contiene y que ya no dice lo contrario; qué se decidió fuera del repositorio sigue fuera de su alcance, como en la ronda 1.

---

## 8. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Ronda 2 sobre los commits `e3468dd`, `68228db` y `95c79c4`. Verifica los treinta hallazgos de `F26-Propagacion-r1.md` uno por uno contra el árbol vivo y contra `git diff` —25 cerrados, 3 parciales, 1 abierto, 1 no verificado—, recuenta las quince reglas y sus derivados en los cinco proyectos, comprueba que las dieciséis filas de control de cambios mal formadas no cambiaron de texto, y levanta ocho hallazgos nuevos, ninguno P0 ni P1. **Dictamen: APROBADO.** | Auditor independiente (Arquitecto de Soluciones + QA Senior) |
