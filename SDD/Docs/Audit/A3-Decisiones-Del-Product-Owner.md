# A3 — Los 57 pendientes restantes, agrupados por decisión

**Producto:** Fábrica de Geometría
**Documento:** A3-Decisiones-Del-Product-Owner.md
**Versión:** 1.7
**Fecha:** 2026-08-20
**Instrumento:** paso **A3** de `Plan-Cierre-De-Pendientes.md` §2.2
**Estado:** **Detención.** Presenta decisiones; **no toma ninguna**

---

## 0. El resultado, primero

**57 filas no son 57 decisiones, ni siquiera 37.** Agrupadas por lo que hay que decidir y verificadas
contra el árbol:

| | Filas | |
|---|---|---|
| **Cerrables leyendo** — la decisión ya existe en el árbol | **~20** | **Trabajo propio.** Van a una segunda pasada de `A2` |
| **Decisiones tuyas** | **~34** | **Ocho decisiones**, no treinta y cuatro |
| **A leer en otra categoría** | ~3 | Se resuelven abriendo la `05` |

**Lo que te queda por decidir son ocho cosas.**

---

## 1. Cerrables leyendo — segunda pasada de `A2`

**`A1` había clasificado 11 filas como «ya decidido en el código» y `A2` no llegó a ellas**: cerró los
cuatro grupos de las 34 sin clasificar. Éstas son las que quedaron, más las que este agrupamiento
destapó.

| Familia | Filas | Dónde ya está la decisión, verificado |
|---|---|---|
| Herramienta concreta de cada stage y de cada paso del flujo | **~6** | `scripts/*.sh` y `deploy-front-ftp.yml`: `dotnet build`, `dotnet test`, `npm ci`, `webpack`, `playwright` |
| Versión del motor de dibujo tridimensional | **3** | **`visor/package.json`: `"three": "0.169.0"`**, anclado |
| Versión exacta de la biblioteca de componentes de interfaz | **2** | **No hay biblioteca, y es una decisión declarada**: `GeometriaFactory.Web.csproj` lleva el apartamiento — *«la etapa `b` decide NO INTRODUCIR MudBlazor»* |
| Cuál función de derivación de clave se ancla, y con qué parámetro | **3** | **`Infrastructure/Security/PasswordDerivation.cs`**: elige **PBKDF2** y escribe el criterio |
| Punto de quiebre principal y proporción de la escena | **2** | **`wwwroot/css/app.css`**, con el `@media (max-width: 768px)` |
| Criterio de comparación de dos correos | **2** | **`EmailIdentity.Normalize`**, en uso desde `ResolveSignInUseCase` |
| Rutas y verbos definitivos | **2** | **18 puntos de acceso mapeados** en `GeometriaFactory.Api` |

**Ninguna de estas siete te toca.** Se cierran citando el archivo, como las 21 de `A2`.

---

## 2. Las ocho decisiones que sí son tuyas

### D1 · Confirmar los valores `[ASUNCIÓN]` · **CONFIRMADA el 2026-08-26**

> **Presentada en detalle el 2026-08-26** en [`D1-Confirmacion-De-Asunciones.md`](D1-Confirmacion-De-Asunciones.md):
> los valores agrupados por lo que gobiernan, con lo que se destraba al confirmarlos y **el caudal
> separado del resto**, porque su fundamento se cayó al cerrarse `D5`.

**Es la más grande de lejos y la más barata.** Catorce filas repartidas en cinco documentos piden lo
mismo: que confirmes la tabla de asunciones numéricas del intake §22 — **latencia, caudal, arranque,
los 500 ms del caso de uso, los 200 ms de la interpretación, el tiempo de la batería, la fluidez de la
interacción**.

**Confirmados, cuatro `QG` pasan a bloqueantes sin ningún otro cambio.** Lo dice el propio enunciado de
`PD-03` de la 09.

### D2 · La unidad de estimación · **RETIRADA el 2026-08-25**

~~Qué unidad usa el backlog. Cinco filas la piden en dos proyectos de código.~~

**Retirada, y cerrada por lectura**: el producto **no estima**. Ver §4, `D11`. **No se borra el enunciado**: un documento que hace desaparecer la pregunta deja de mostrar contra qué se decidió.

### D3 · La vigencia exacta del acceso firmado · **REFORMULADA el 2026-08-26**

~~El intake la declara **«corta»** sin fijar número, y `ADR-00003` la toma de configuración. **No la
encontré fijada en el árbol.**~~

**Sí estaba fijada, y §5 de este documento ya anticipaba que si aparecía dejaba de ser tuya.** El valor
en efecto es **480 minutos —ocho horas—**, por omisión de `SigningOptions.cs:25`, y **nada lo
sobreescribe**: ni `appsettings`, ni la composición. Las tres filas se reformularon **por lectura** y
**dejaron de estar vencidas**.

**Lo que sigue siendo tuyo, y ahora es una pregunta más chica y más precisa:** `ADR-00003` §5 fija el
criterio de que el acceso **«caduque dentro de la sesión de trabajo de una clase»**. **¿Ocho horas lo
cumplen?** No se puede contestar leyendo, porque **ninguna fuente del producto declara cuánto dura una
clase**.

**Y una obligación derivada que no depende de tu respuesta**, con el mismo criterio con que cerraste
`D4` el 2026-08-20: **el valor se declara explícitamente** en la configuración, en lugar de quedar como
omisión del código.

### D4 · El límite de tamaño del cuerpo de una petición · **2 filas**

**Verificado: no está fijado en el código.** No hay `MaxRequestBodySize` ni equivalente.

### D5 · El volumen de la comisión · **2 filas** · `[A VERIFICAR]`

Cuántos alumnos. Es un dato tuyo, y alimenta las asunciones de caudal de `D1`.

### D6 · La versión de plataforma que soporta el hosting · **3 filas** · `[A VERIFICAR]` · **RETIRADA el 2026-08-31: ya estaba contestada cuando se presentó**

~~**No es una decisión: es una medición**, y **la fase `i` la hace sola**. Publicar el front contra el
hosting real la contesta.~~

**El diagnóstico era correcto y el pronóstico llegó tarde.** Es una medición, sí — y **ya se había
hecho**: el **2026-08-13**, publicando el front contra el hosting real, exactamente como esta entrada
anticipaba. `PT-01.a` **pasa** con **200** en la ruta pública, el hosting soporta **`net10.0`** y está
confirmado desde el panel de la cuenta. Este documento se emitió el **2026-08-20**, siete días después,
y presentó al Product Owner como pendiente una pregunta que el producto ya había respondido.

**No es un descuido de redacción: es el mismo hueco que este documento arrastra en otras tres entradas.**
`D6` no se contrastó contra lo que el producto hizo, sino contra lo que faltaba por hacer. Ver
[`Inventario-Marcas-A-Verificar-2026-08-31.md`](Inventario-Marcas-A-Verificar-2026-08-31.md) §2.1.

### D7 · La herramienta que calcula la versión — `PA-06` · **5 filas**

**Verificado que sigue abierta**: no hay MinVer, GitVersion, Nerdbank ni semantic-release en el árbol.
Es la única de la familia de anclajes que no se cerró sola, y es coherente con que el prefijo `v` se
fijara sin elegirla.

### D8 · Si el mutation score entra al pipeline · **1 fila**

**Verificado: no hay herramienta.** `CV-19` se reporta «sin medir» con su hueco declarado.

---

## 3. Cómo conviene tomarlas, y en qué orden

| Orden | Cuáles | Por qué |
|---|---|---|
| **1º** | **D5**, el volumen de la comisión | **Alimenta a `D1`**: sin saber cuántos alumnos, confirmar el caudal es adivinar |
| **2º** | **D1**, los `[ASUNCIÓN]` | Cierra **14 filas de un saque** y habilita cuatro `QG` |
| **3º** | **D3** y **D4** | Son valores de configuración, y los dos alcanzan al despliegue de `i` |
| **junto con `i`** | **D6** | La fase `i` la contesta midiendo |
| **cuando quieras** | **D2**, **D7**, **D8** | No bloquean nada. `D7` conviene antes de la primera etiqueta calculada |

**Y una salida que vale para todas: retirar también cierra.** Ninguna de las 57 se evaluó por
vigencia. Si `D2` ya no importa porque el backlog no se estima, **retirar el punto es tan válido como
decidirlo**, y deja el árbol diciendo la verdad.

---

## 4. Decisiones tomadas fuera de las ocho, y por qué se registran acá

**Este documento se emitió para presentar decisiones, no para tomarlas.** Pero la migración 10.0 → 13.3
obligó a dos que **no estaban entre las ocho** —el salto las volvió obligatorias—, y el audit
independiente del corte 09 levantó como **P2** que vivieran **sólo en el documento que las usa**: una
decisión del Product Owner que no se registra donde se la busca **no es auditable en la ronda
siguiente**.

| Id | Qué se decidió | Quién y cuándo | Dónde se aplica |
|---|---|---|---|
| **D9** | **El formato del inventario de componentes: CycloneDX, salida JSON.** Lo pide `Rules-Devops.md` §4.6 punto 1, que admite CycloneDX o SPDX y **no impone ninguno**. Se decide hoy porque la regla declara que **el formato no depende del runtime** y que sólo el generador puede diferirse: diferir los cuatro campos era el arrastre invertido que la 11.0 vino a corregir | El **Product Owner**, el **2026-08-24**, sobre el corte 09 de la migración | `Supply-Chain-Seguridad.md` §2.b de **las dos** unidades de entrega |
| **D10** | **La forma de los identificadores de los puntos abiertos que la categoría 09 acuña**: `PD-NN`, sin familia nueva, con ámbito **el documento** y la serie propia empezando **donde no pisa ningún token que el documento cite**. Lo exige `Migracion-Rules.md` §4.3.1 pasada 1.b para toda familia que el destino acuñe | El **Product Owner**, el **2026-08-24**; la regla de numeración se corrigió el mismo día, en la ronda 3, al aparecer una colisión real en `GeometriaFactory-Web` | `Supply-Chain-Seguridad.md` §2.b de **las dos** unidades |

| **D11** | **`D2` queda RETIRADA: el producto no estima.** No es una decisión de valor sino el reconocimiento de un hecho que nadie había contrastado — `PRODUCT-INTAKE` §2 declara `equipo_n = 1`, `Mini-Plan.md` §1.2 declara que **no hay capacidad numérica y es deliberado**, y **ocho etapas se planificaron, se construyeron, se demostraron y se cerraron sin una sola estimación**. §3 de este documento ya admitía el retiro como cierre válido; lo que faltaba era el acto | El **Product Owner**, el **2026-08-25**, sobre la recomendación del orquestador en el corte de la 06 | `PA-01` cerrado **por lectura** en las **seis** tablas de los dos `Product-Backlog.md`; las **144** `US-*.md` con su §5.b «no aplica»; las **130** celdas de los dos `Backlog-Tecnico.md`; los dos `Mini-Plan.md` |

**Lo que este registro no es.** No convierte a `D9` y `D10` en dos de las ocho: **las ocho siguen
siendo las de §2**, y de ellas **`D6` y `D7` continúan abiertas** —dos: `D1` se **confirmó** el 2026-08-26 con el caudal excluido, `D2` quedó **retirada** el 2026-08-25 (ver `D11`) y `D3` se **reformuló por lectura** el 2026-08-26, con lo que queda de ella declarado en §2—. Estas dos entran
por una vía distinta —un salto normativo que volvió obligatorio lo que antes no lo era— y se numeran a
continuación para que no haya dos series.

---

## 5. Lo que este documento no sabe

- **Los recuentos son aproximados.** El agrupamiento es por enunciado y varias familias se solapan;
  los números llevan `~` donde el solapamiento es real.
- **`D3` puede estar fijada y no la encontré.** Busqué en la composición de la API y no en la
  configuración. **Si aparece, pasa a la lista de §1** y deja de ser tuya.
- **Ninguna se evaluó por vigencia**, igual que en `A1` y en el plan.

## 6. Control de cambios

| Versión | Fecha | Cambios | Autor |
|---|---|---|---|
| 1.7 | 2026-08-31 | **Cierre de las dos incógnitas `[A VERIFICAR]` que ya no tenían pregunta**, sobre el inventario [`Inventario-Marcas-A-Verificar-2026-08-31.md`](Inventario-Marcas-A-Verificar-2026-08-31.md), que clasificó las **71** apariciones vivas del corpus en **cinco** incógnitas. **(a) La versión de plataforma del hosting quedó RESUELTA el 2026-08-13, midiendo**: `PT-01.a` pasa con **200** y el hosting soporta `net10.0`, confirmado desde el panel; no hizo falta bajar la versión objetivo del front. **(b) La versión de la biblioteca de componentes queda SIN OBJETO**: la biblioteca nunca se introdujo y su ausencia es una decisión declarada en el `.csproj` — `PA-01` de `Web/05` §11 **ya lo había cerrado por lectura el 2026-08-20** y el desenlace no bajó. **Ninguna de las dos se decide acá: las dos se leen.** **`D6` queda RETIRADA: ya estaba contestada cuando este documento la presentó.** Se emitió el 2026-08-20 diciendo que «la fase `i` la hace sola», y la fase `a` la había hecho el **2026-08-13**, publicando el front contra el hosting real — exactamente el acto que la entrada anticipaba. **El diagnóstico era correcto y el pronóstico llegó tarde**, porque la entrada se contrastó contra lo que faltaba por hacer y no contra lo que el producto ya había hecho. **Ningún umbral, ningún contrato y ninguna decisión cambian.** | Orquestador SDD |
| 1.6 | 2026-08-26 | **`D1` confirmada.** El Product Owner confirmó los valores `[ASUNCIÓN]` el 2026-08-26, **con el caudal excluido** —su fundamento se cayó con `D5` y su valor sale de `PT-05`—. **12 filas vencidas cerradas** y los gates condicionados pasaron a **bloqueantes**. **Quedan abiertas `D6` y `D7`** —dos de las ocho—, y `D6` la contesta midiendo la fase `i`. | Orquestador SDD |
| 1.5 | 2026-08-26 | **`D1` se presenta en detalle** en [`D1-Confirmacion-De-Asunciones.md`](D1-Confirmacion-De-Asunciones.md) 1.0: las **doce filas vencidas** repartidas en cinco documentos quedan en una sola tabla, agrupadas por lo que cada valor gobierna, con lo que se destraba al confirmarlas —**cuatro `QG` pasan a bloqueantes**— y con **el caudal separado del resto**, porque su fundamento se cayó al cerrarse `D5` y su valor sale de `PT-05`. El recuento de la cabecera pasa de «~14 filas» a **12**, contadas sobre el árbol. | Orquestador SDD |
| 1.4 | 2026-08-26 | **`D3` se reformula por lectura, y §4 §5 lo había autorizado.** «Si aparece, pasa a la lista de §1 y deja de ser tuya» — **apareció**: la vigencia está fijada en **480 minutos** por omisión de `SigningOptions.cs:25`, sin nada que la sobreescriba. Las tres filas del árbol dejaron de afirmar que no hay número y **dejaron de estar vencidas**. **Lo que queda es una pregunta más chica**: si ocho horas cumplen el criterio de `ADR-00003` §5, que **no se puede contestar leyendo** porque ninguna fuente declara cuánto dura una clase. Más la obligación derivada de declarar el valor explícitamente, por el criterio de `D4`. **Siguen abiertas `D1`, `D6` y `D7`** — tres, no cuatro. | Orquestador SDD |
| 1.3 | 2026-08-25 | **Corrige el recuento de §4**, que el audit de **M6** levantó como **P2**: §2 declaraba `D2` **retirada** y §4 la seguía contando entre las abiertas, en el mismo documento y a dieciocho líneas de distancia. Son **cuatro** las que continúan abiertas —`D1`, `D3`, `D6` y `D7`—, no cinco. | Orquestador SDD |
| 1.2 | 2026-08-25 | **`D2` queda retirada y entra como `D11` en §4.** Lo levantó el audit del corte de la 06 como **P2**: el cierre se había ejecutado en **150 documentos** y este artefacto —el que declara **de quién es cada decisión**— seguía pidiéndosela al Product Owner en su §2. **La decisión no era de valor sino de hecho**: ocho etapas cerradas sin una sola estimación. §2 conserva el enunciado tachado en lugar de borrarlo, porque un documento que hace desaparecer la pregunta deja de mostrar contra qué se decidió. **Quedan cuatro de las ocho abiertas**: `D1`, `D3`, `D6` y `D7`. | Orquestador SDD |
| 1.1 | 2026-08-24 | **Entra §4, las decisiones tomadas fuera de las ocho**, y las dos secciones siguientes corren a §5 y §6. La migración 10.0 → 13.3 obligó a dos que el salto volvió obligatorias —**`D9`**, el formato del inventario (**CycloneDX / JSON**), y **`D10`**, la forma de los identificadores que la 09 acuña— y el **audit independiente del corte 09 las levantó como P2** porque vivían **sólo en el documento que las usa**: una decisión del Product Owner que no se registra donde se la busca no es auditable en la ronda siguiente. Se numeran a continuación de las ocho **para que no haya dos series**, y §4 declara que **las ocho de §2 siguen siendo las ocho** y que cinco continúan abiertas. | Orquestador SDD |
| 1.0 | 2026-08-20 | Emisión inicial, paso **A3**. Agrupa las **57** filas vencidas restantes por **lo que hay que decidir** en lugar de por fila. **~20 resultan cerrables leyendo** —el motor 3D anclado en `three 0.169.0`, la biblioteca de componentes **decidida por apartamiento** en el `.csproj`, `PBKDF2` en `PasswordDerivation.cs`, el punto de quiebre en `app.css`, `EmailIdentity.Normalize`, los 18 puntos de acceso y las herramientas de cada stage en los guiones— y van a una **segunda pasada de `A2`**. Las restantes se reducen a **ocho decisiones**, con **`D1` absorbiendo catorce filas en una sola**: confirmar la tabla de asunciones numéricas del intake §22, que además habilita cuatro `QG`. Propone el orden **D5 → D1 → D3/D4**, con `D6` resuelta por la propia fase `i` —es medición y no decisión— y declara que **retirar un punto es tan válido como decidirlo**, porque ninguna fila se evaluó por vigencia. | Orquestador SDD |
