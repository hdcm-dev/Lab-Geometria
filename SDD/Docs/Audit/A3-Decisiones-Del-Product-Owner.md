# A3 — Los 57 pendientes restantes, agrupados por decisión

**Producto:** Fábrica de Geometría
**Documento:** A3-Decisiones-Del-Product-Owner.md
**Versión:** 1.9
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

> **Índice de estado — el único lugar donde se lee cuántas quedan.** Al **2026-08-31**: **UNA abierta,
> cinco cerradas, dos retiradas.**
>
> | | Decisión | Estado |
> |---|---|---|
> | `D1` | Confirmar los valores `[ASUNCIÓN]` | **CONFIRMADA** el 2026-08-26, con el caudal excluido |
> | `D2` | La unidad de estimación | **RETIRADA** el 2026-08-25 (ver `D11`) |
> | `D3` | La vigencia del acceso firmado | **REFORMULADA por lectura** el 2026-08-26 |
> | `D4` | El límite de tamaño del cuerpo | **DECIDIDA** el 2026-08-20: se adopta el valor por omisión del servidor |
> | `D5` | El volumen de la comisión | **CERRADA POR INCOGNOSCIBLE** el 2026-08-20. No se fija número |
> | `D6` | La versión de plataforma del hosting | **RETIRADA** el 2026-08-31: ya estaba contestada |
> | `D7` | La herramienta que calcula la versión | **ABIERTA — la única** |
> | `D8` | Si el *mutation score* entra al flujo | **DECIDIDA con un NO** el 2026-08-20. No entra |
>
> **Este índice existe porque el documento no registraba desenlaces, y en la emisión 1.8 eso me hizo
> concluir exactamente al revés.** Hasta la 1.7, §4 cerraba afirmando que «`D6` y `D7` continúan abiertas
> — **dos**» mientras `D4`, `D5` y `D8` **no tenían desenlace escrito en §2**. La 1.8 leyó esa ausencia
> como que estaban **abiertas** y declaró «eran cinco, no dos». **Es falso, y al revés:** las tres
> estaban **decididas desde el 2026-08-20** —commit `b4a4804`, que las cerró en **cinco documentos** y
> **no tocó éste**—. **El recuento de §4 era correcto y lo que faltaba eran las entradas.**
>
> **Y el error no fue no mirar el árbol: fue mirar el árbol equivocado.** Antes de escribir la 1.8
> verifiqué contra el código —no hay `MaxRequestBodySize`, no hay herramienta de cálculo de versión, no
> hay herramienta de mutación— y tomé esas ausencias por «sin decidir». Pero **`D4` se decidió adoptando
> el valor por omisión y `D8` se decidió con un NO**: son decisiones **cuyo cumplimiento consiste en que
> no haya nada**. En el árbol, *decidido que no* y *sin decidir* **se ven idénticos**. Para esa clase de
> decisión el árbol no puede ser la fuente: sólo el registro puede, y el registro era justamente lo que
> no se había actualizado.

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

### D4 · El límite de tamaño del cuerpo de una petición · **2 filas** · **DECIDIDA el 2026-08-20**

**Verificado: no está fijado en el código.** No hay `MaxRequestBodySize` ni equivalente.

**Y eso no significa que estuviera sin decidir: significa que la decisión FUE ÉSA.** El Product Owner
resolvió el **2026-08-20** —commit `b4a4804`— **adoptar el valor por omisión del servidor HTTP y no fijar
uno propio**. Queda una **obligación derivada**, declarada en las filas de destino: por la regla que
`scripts/verify-explicit-configuration.sh` hace cumplir —*una configuración que se sobreentiende acierta
hasta el día en que alguien cambia el otro lado*—, **el valor por omisión se declara explícitamente
cuando se toque la composición**. No hay trabajo de código pendiente hoy.

### D5 · El volumen de la comisión · **2 filas** · **CERRADA POR INCOGNOSCIBLE el 2026-08-20**

Cuántos alumnos. **El Product Owner declaró que el dato no se sabe ni se puede saber de antemano, y por
lo tanto NO SE FIJA NÚMERO** —commit `b4a4804`—. **Es mejor desenlace que inventar uno**, y es un cierre
legítimo: la pregunta no queda pendiente, queda contestada con «no es cognoscible».

**Y tiene una consecuencia que está escrita y no hay que perder de vista**, porque es lo único que
sobrevive de esta decisión: el **caudal de 20 peticiones por minuto** de `Web/05` §8 **se derivaba de
«una comisión operando durante una clase»**. Sin tamaño conocido, **ese fundamento se cae**. El número
queda marcado **provisorio** en la propia fila de la tabla de NFR, se conserva como referencia, y **su
valor definitivo sale de lo que `PT-05` mida sobre el uso real**, en la fase `i`.

**Por eso el caudal quedó fuera de la confirmación `D1` del 2026-08-26**, y no por falta de decisión:
`D5` estaba decidida seis días antes, y lo que `D1` no podía hacer era confirmar un número **cuyo
fundamento esta decisión había retirado**.

**Lo que el diseño de los dos listados sigue suponiendo —decenas y no cientos— no queda validado por
esto**, y tampoco refutado: queda **sin dato**, que es exactamente lo que el Product Owner declaró. Ver
[`Medicion-Volumen-De-Comision-2026-08-31.md`](Medicion-Volumen-De-Comision-2026-08-31.md), que mide qué
volumen sostiene el servicio de datos ya que el número de la comisión no se puede saber.

### D6 · La versión de plataforma que soporta el hosting · **3 filas** · `[A VERIFICAR]` · **RETIRADA el 2026-08-31: ya estaba contestada cuando se presentó**

~~**No es una decisión: es una medición**, y **la fase `i` la hace sola**. Publicar el front contra el
hosting real la contesta.~~

**El diagnóstico era correcto y el pronóstico llegó tarde.** Es una medición, sí — y **ya se había
hecho**: el **2026-08-13**, publicando el front contra el hosting real, exactamente como esta entrada
anticipaba. `PT-01.a` **pasa** con **200** en la ruta pública, el hosting soporta **`net10.0`** y está
confirmado desde el panel de la cuenta. Este documento se emitió el **2026-08-20**, siete días después,
y presentó al Product Owner como pendiente una pregunta que el producto ya había respondido.

**No es un descuido de redacción, y tiene un precedente exacto en este mismo documento.** `D6` no se
contrastó contra lo que el producto **hizo**, sino contra lo que faltaba por hacer — que es literalmente
lo que le pasó a **`D2`**, retirada el 2026-08-25 al advertirse que ocho etapas se habían cerrado sin una
sola estimación. **Son dos de ocho**, y las dos se cerraron por el mismo acto: mirar el árbol en lugar
del calendario. Ver [`Inventario-Marcas-A-Verificar-2026-08-31.md`](Inventario-Marcas-A-Verificar-2026-08-31.md) §2.1.

*[CORREGIDO en la emisión 1.8: la 1.7 decía «el mismo hueco que este documento arrastra en otras tres
entradas» y **no las había contado**. Son dos, `D2` y `D6`.]*

### D7 · La herramienta que calcula la versión — `PA-06` · **5 filas** · **ABIERTA, y es la ÚNICA**

**Verificado que sigue abierta**: no hay MinVer, GitVersion, Nerdbank ni semantic-release en el árbol.
Es la única de la familia de anclajes que no se cerró sola, y es coherente con que el prefijo `v` se
fijara sin elegirla.

### D8 · Si el *mutation score* entra al pipeline · **1 fila** · **DECIDIDA con un NO el 2026-08-20**

**Verificado: no hay herramienta.** `CV-19` de `Api/08` §5.2 se reporta «sin medir» con su hueco
declarado, contra el piso del **60 %** que `Rules-Calidad-Y-Pruebas.md` §2.2 fija para el tipo `library`.

**Y otra vez, la ausencia de herramienta ES el cumplimiento de la decisión, no su falta.** El Product
Owner cerró **con un NO** el **2026-08-20** —commit `b4a4804`—: **el *mutation score* no entra al
pipeline**. `CV-19` queda declarado sin medir, con su motivo, y **deja de ser un pendiente**.

---

## 3. Cómo conviene tomarlas, y en qué orden

| Orden | Cuáles | Por qué |
|---|---|---|
| ~~**1º**~~ | ~~**D5**, el volumen de la comisión~~ **TOMADA el 2026-08-20** | ~~**Alimenta a `D1`**: sin saber cuántos alumnos, confirmar el caudal es adivinar~~ **El orden SÍ se respetó**: `D5` se tomó el 2026-08-20 y `D1` el 2026-08-26, seis días después. Y se tomó del único modo posible —**incognoscible, sin número**—, que es la razón por la que `D1` **no podía** confirmar el caudal: no le faltaba la decisión, le faltaba el **fundamento**, que `D5` había retirado |
| **2º** | **D1**, los `[ASUNCIÓN]` | Cierra **14 filas de un saque** y habilita cuatro `QG` |
| ~~**3º**~~ | ~~**D3** y **D4**~~ **TOMADAS las dos** | ~~Son valores de configuración, y los dos alcanzan al despliegue de `i`~~ `D3` reformulada por lectura el 2026-08-26; `D4` decidida el 2026-08-20 adoptando el valor por omisión |
| ~~**junto con `i`**~~ | ~~**D6**~~ | ~~La fase `i` la contesta midiendo~~ **Ya la contestó la fase `a`, el 2026-08-13. `D6` queda RETIRADA el 2026-08-31** |
| **cuando quieras** | ~~**D2**~~, **D7**, ~~**D8**~~ | No bloquean nada. **`D7` es la única que queda de las ocho**, y conviene antes de la primera etiqueta calculada. *(`D2` quedó **retirada** el 2026-08-25; `D8` se decidió **con un NO** el 2026-08-20.)* |

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
siendo las de §2**. ~~De ellas **`D6` y `D7` continúan abiertas** —dos: `D1` se **confirmó** el 2026-08-26 con el caudal excluido, `D2` quedó **retirada** el 2026-08-25 (ver `D11`) y `D3` se **reformuló por lectura** el 2026-08-26, con lo que queda de ella declarado en §2—.~~

**Esa frase era CORRECTA, y la emisión 1.8 la «corrigió» hacia el error.** Daba cuenta de cinco de las
ocho y omitía a `D4`, `D5` y `D8`, que **no tenían desenlace escrito en §2**. La 1.8 leyó esa ausencia
como que estaban abiertas y declaró «eran cinco, no dos». **Las tres estaban decididas desde el
2026-08-20**, por el commit `b4a4804`, que las cerró en **cinco documentos de las dos unidades de
entrega** y **no tocó éste**. El recuento estaba bien; **lo que faltaba eran las entradas**.

**Restituido el 2026-08-31 en la emisión 1.9.** Hoy, con `D6` retirada, **queda UNA sola abierta: `D7`.**

**Lo que este ida y vuelta deja, y es lo más caro del día.** El error de la 1.8 no fue dejar de mirar el
árbol: **fue mirar el árbol y sacar la conclusión opuesta.** Se verificó que no hay `MaxRequestBodySize`,
que no hay herramienta de cálculo de versión y que no hay herramienta de mutación, y se tomaron esas tres
ausencias por «sin decidir». Pero **`D4` se decidió adoptando el valor por omisión y `D8` se decidió con
un NO**: son decisiones **cuyo cumplimiento consiste, exactamente, en que no haya nada**. En el árbol,
**«decidido que no» y «sin decidir» son indistinguibles**, y ninguna verificación sobre el código puede
separarlos. Para esa clase de decisión **el registro es la única fuente** — y el registro era justamente
lo que `b4a4804` no había actualizado.

`D9` y `D10` entran
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
| 1.9 | 2026-08-31 | **Revierte el error central de la emisión 1.8: `D4`, `D5` y `D8` NO estaban abiertas. Estaban decididas desde el 2026-08-20.** El commit `b4a4804` las cerró ese día sobre **cinco documentos de las dos unidades de entrega** y **no tocó éste**. `D4`: se adopta el **valor por omisión** del servidor HTTP, con la obligación derivada de declararlo explícitamente cuando se toque la composición. `D5`: **cerrada por INCOGNOSCIBLE** —el dato no se sabe ni se puede saber, y no se fija número—, con la consecuencia de que el **caudal** pierde su fundamento y queda provisorio hasta `PT-05`. `D8`: **cerrada con un NO**, el *mutation score* no entra al pipeline. **El recuento de §4 que la 1.8 declaró equivocado era el correcto**: las abiertas eran dos, `D6` y `D7`. Lo que faltaba no era el total sino **las entradas de §2**. Hoy, con `D6` retirada, **queda UNA: `D7`**. **Y lo que este ida y vuelta deja es la lección más cara del día, porque contradice la que veníamos sacando.** El error de la 1.8 no fue dejar de mirar el árbol: **fue mirarlo y concluir al revés.** Se verificó que no hay `MaxRequestBodySize`, ni herramienta de cálculo de versión, ni herramienta de mutación, y se tomaron esas ausencias por «sin decidir». Pero `D4` y `D8` son decisiones **cuyo cumplimiento consiste en que no haya nada**: en el árbol, **«decidido que no» y «sin decidir» son indistinguibles**, y ninguna verificación sobre el código puede separarlos. **Para esa clase de decisión el registro es la única fuente** — y el registro era exactamente lo que `b4a4804` no había actualizado. Se corrigen en consecuencia el índice de estado de §2, las tres entradas, el orden recomendado de §3 y el cierre de §4. | Orquestador SDD |
| 1.8 | 2026-08-31 | **El recuento de decisiones abiertas estaba mal, y el documento que existe para que no se pierda una decisión perdía tres.** §4 cerraba afirmando que «`D6` y `D7` continúan abiertas — **dos**» y daba cuenta de cinco de las ocho: **`D4`, `D5` y `D8` no tenían desenlace en §2 ni entraban en el total**. **Eran cinco abiertas, no dos.** Entra un **índice de estado** al frente de §2 —la misma medicina que `M-03` de la mesa del 2026-08-31 le recetó al reporte de hallazgos: un índice que se arma **contando** encuentra lo que uno armado **recordando** no—, y `D4`, `D5` y `D8` reciben desenlace explícito. **Hoy quedan cuatro abiertas**: `D4`, `D5`, `D7` y `D8`. **`D5` se declara la más consecuente**, con tres razones medidas: `D1` se tomó sin ella y por eso el caudal quedó excluido y es **el único valor abierto del §22**; condiciona la ausencia de paginación en dos superficies; y **es la única marca `[A VERIFICAR]` del producto que se decide y no se mide**, motivo por el cual faltaba en el §22 hasta hoy. Se registra además que **el orden recomendado por §3 no se respetó**: `D5` era la primera y se tomó `D1`. **Ninguna de las tres omitidas estaba mal argumentada: se perdieron por no estar contadas.** Se corrige por último una afirmación de la emisión **1.7**, en `D6`: decía «el mismo hueco que este documento arrastra en **otras tres** entradas» **sin haberlas contado**; son **dos**, `D2` y `D6`. | Orquestador SDD |
| 1.7 | 2026-08-31 | **Cierre de las dos incógnitas `[A VERIFICAR]` que ya no tenían pregunta**, sobre el inventario [`Inventario-Marcas-A-Verificar-2026-08-31.md`](Inventario-Marcas-A-Verificar-2026-08-31.md), que clasificó las **71** apariciones vivas del corpus en **cinco** incógnitas. **(a) La versión de plataforma del hosting quedó RESUELTA el 2026-08-13, midiendo**: `PT-01.a` pasa con **200** y el hosting soporta `net10.0`, confirmado desde el panel; no hizo falta bajar la versión objetivo del front. **(b) La versión de la biblioteca de componentes queda SIN OBJETO**: la biblioteca nunca se introdujo y su ausencia es una decisión declarada en el `.csproj` — `PA-01` de `Web/05` §11 **ya lo había cerrado por lectura el 2026-08-20** y el desenlace no bajó. **Ninguna de las dos se decide acá: las dos se leen.** **`D6` queda RETIRADA: ya estaba contestada cuando este documento la presentó.** Se emitió el 2026-08-20 diciendo que «la fase `i` la hace sola», y la fase `a` la había hecho el **2026-08-13**, publicando el front contra el hosting real — exactamente el acto que la entrada anticipaba. **El diagnóstico era correcto y el pronóstico llegó tarde**, porque la entrada se contrastó contra lo que faltaba por hacer y no contra lo que el producto ya había hecho. **Ningún umbral, ningún contrato y ninguna decisión cambian.** | Orquestador SDD |
| 1.6 | 2026-08-26 | **`D1` confirmada.** El Product Owner confirmó los valores `[ASUNCIÓN]` el 2026-08-26, **con el caudal excluido** —su fundamento se cayó con `D5` y su valor sale de `PT-05`—. **12 filas vencidas cerradas** y los gates condicionados pasaron a **bloqueantes**. **Quedan abiertas `D6` y `D7`** —dos de las ocho—, y `D6` la contesta midiendo la fase `i`. | Orquestador SDD |
| 1.5 | 2026-08-26 | **`D1` se presenta en detalle** en [`D1-Confirmacion-De-Asunciones.md`](D1-Confirmacion-De-Asunciones.md) 1.0: las **doce filas vencidas** repartidas en cinco documentos quedan en una sola tabla, agrupadas por lo que cada valor gobierna, con lo que se destraba al confirmarlas —**cuatro `QG` pasan a bloqueantes**— y con **el caudal separado del resto**, porque su fundamento se cayó al cerrarse `D5` y su valor sale de `PT-05`. El recuento de la cabecera pasa de «~14 filas» a **12**, contadas sobre el árbol. | Orquestador SDD |
| 1.4 | 2026-08-26 | **`D3` se reformula por lectura, y §4 §5 lo había autorizado.** «Si aparece, pasa a la lista de §1 y deja de ser tuya» — **apareció**: la vigencia está fijada en **480 minutos** por omisión de `SigningOptions.cs:25`, sin nada que la sobreescriba. Las tres filas del árbol dejaron de afirmar que no hay número y **dejaron de estar vencidas**. **Lo que queda es una pregunta más chica**: si ocho horas cumplen el criterio de `ADR-00003` §5, que **no se puede contestar leyendo** porque ninguna fuente declara cuánto dura una clase. Más la obligación derivada de declarar el valor explícitamente, por el criterio de `D4`. **Siguen abiertas `D1`, `D6` y `D7`** — tres, no cuatro. | Orquestador SDD |
| 1.3 | 2026-08-25 | **Corrige el recuento de §4**, que el audit de **M6** levantó como **P2**: §2 declaraba `D2` **retirada** y §4 la seguía contando entre las abiertas, en el mismo documento y a dieciocho líneas de distancia. Son **cuatro** las que continúan abiertas —`D1`, `D3`, `D6` y `D7`—, no cinco. | Orquestador SDD |
| 1.2 | 2026-08-25 | **`D2` queda retirada y entra como `D11` en §4.** Lo levantó el audit del corte de la 06 como **P2**: el cierre se había ejecutado en **150 documentos** y este artefacto —el que declara **de quién es cada decisión**— seguía pidiéndosela al Product Owner en su §2. **La decisión no era de valor sino de hecho**: ocho etapas cerradas sin una sola estimación. §2 conserva el enunciado tachado en lugar de borrarlo, porque un documento que hace desaparecer la pregunta deja de mostrar contra qué se decidió. **Quedan cuatro de las ocho abiertas**: `D1`, `D3`, `D6` y `D7`. | Orquestador SDD |
| 1.1 | 2026-08-24 | **Entra §4, las decisiones tomadas fuera de las ocho**, y las dos secciones siguientes corren a §5 y §6. La migración 10.0 → 13.3 obligó a dos que el salto volvió obligatorias —**`D9`**, el formato del inventario (**CycloneDX / JSON**), y **`D10`**, la forma de los identificadores que la 09 acuña— y el **audit independiente del corte 09 las levantó como P2** porque vivían **sólo en el documento que las usa**: una decisión del Product Owner que no se registra donde se la busca no es auditable en la ronda siguiente. Se numeran a continuación de las ocho **para que no haya dos series**, y §4 declara que **las ocho de §2 siguen siendo las ocho** y que cinco continúan abiertas. | Orquestador SDD |
| 1.0 | 2026-08-20 | Emisión inicial, paso **A3**. Agrupa las **57** filas vencidas restantes por **lo que hay que decidir** en lugar de por fila. **~20 resultan cerrables leyendo** —el motor 3D anclado en `three 0.169.0`, la biblioteca de componentes **decidida por apartamiento** en el `.csproj`, `PBKDF2` en `PasswordDerivation.cs`, el punto de quiebre en `app.css`, `EmailIdentity.Normalize`, los 18 puntos de acceso y las herramientas de cada stage en los guiones— y van a una **segunda pasada de `A2`**. Las restantes se reducen a **ocho decisiones**, con **`D1` absorbiendo catorce filas en una sola**: confirmar la tabla de asunciones numéricas del intake §22, que además habilita cuatro `QG`. Propone el orden **D5 → D1 → D3/D4**, con `D6` resuelta por la propia fase `i` —es medición y no decisión— y declara que **retirar un punto es tan válido como decidirlo**, porque ninguna fila se evaluó por vigencia. | Orquestador SDD |
