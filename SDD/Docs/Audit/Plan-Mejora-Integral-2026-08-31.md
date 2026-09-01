# Plan de mejora integral — mesa del 2026-08-31

**Producto:** Fábrica de Geometría
**Documento:** Plan-Mejora-Integral-2026-08-31.md
**Versión:** 1.0
**Fecha:** 2026-08-31
**Instrumento:** entrega P4 de la mesa registrada en [`Mesa-2026-08-31-B.md`](Mesa-2026-08-31-B.md)
**Estado:** **Propuesto.** Ninguna unidad se ejecutó; **espera aprobación del Product Owner**

> **AVISO SOBRE LOS NÚMEROS DE LÍNEA DE `ClassSubmissionList.razor`.** Este plan se escribió antes de
> `U-02`, que **agregó ~48 líneas** a ese archivo. Las citas del tipo `:323`, `:331`, `:340` y `:275-281`
> corresponden al árbol **anterior** a esa unidad; hoy son `:368`, `:376`, `:385` y `:320-326`. **Los
> hechos que citan no cambiaron** —las dos llamadas al servicio siguen ahí, y la primera sigue siendo sin
> criterio—: lo que se movió es dónde están. Verificado el 2026-08-31 al ejecutar `U-08`.
>
> **Los identificadores de hallazgo se renumeraron de `F-NN` a `MI-NN` al emitir.** La mesa los produjo
> con el prefijo `F`, que **ya nombra 294 apariciones en `Audit/`** —los informes de Fase F—, y
> `Mesa-Rules.md` §2.2 punto 1 prohíbe reusar una familia presente en la carpeta. `MI` se verificó libre
> contra las **sesenta** familias existentes.

---

# PLAN INTEGRAL DE CIERRE — Mesa del 2026-08-31

*Presidente de mesa, §6.6 P4. Todo lo que sigue se verificó contra el árbol en `/home/fernando/workspaces/workspace-dev/PROG2/Geometria/Lab-Geometria` con fecha 2026-08-31. Donde corrijo un número del panel o del contrato de entrada, lo digo.*

---

## 1 · DIAGNÓSTICO INTEGRAL

**Este producto verifica sus condiciones y no verifica sus efectos, y ésa es la única enfermedad de los doce hallazgos.** En todos los casos alguien escribió una condición correcta y nadie contrastó nunca qué produce esa condición. El `@media (max-width: 768px)` está escrito —y `PA-05` lo cerró «por lectura» el 2026-08-20 comprobando exactamente eso— pero lo que la regla enciende es `.gf-stacked-cards`, una clase que **cero** componentes emiten (verificado: `app.css:449` la define, `:782` la enciende, `grep -rn gf-stacked-cards src/GeometriaFactory.Web/Components/` no devuelve nada), y por debajo de 768 px las tres listas del producto no dibujan ninguna fila. `D1` confirmó los umbrales el 2026-08-26 y **36 filas de tabla en seis documentos vivos** siguen diciendo «Condicionado» (conté: `Criterios-Validacion.md` 16, `Estrategia-Calidad.md` 11, `09-Devops/README.md` 4, `08-Calidad-Y-Pruebas/README.md` 4 —este último no lo nombró nadie—, `Guia-Publicacion-Image-Docker.md` 1). El intake declara diario WAL y `UseSqlite(connectionString)` a secas deja el motor en `delete` (`grep -rniE 'journal_mode|pragma' src/ --include=*.cs` → sólo `#pragma warning` de migraciones). `DegradedStateOverlay` tiene el texto aprobado y un `[Parameter] Visible` que los dos shells no pasan. `StoreIsPrepared` se escribe una vez en el arranque y un `healthcheck` la sondea cada 30 segundos creyendo que pregunta algo. **En los seis casos la condición existe, es correcta, y su realización nunca se midió.**

**La razón por la que no se midió tampoco es cultural: es estructural, y se puede nombrar con precisión. Todas las verificaciones que este producto tiene corren en una sola dirección.** `verify-visual-system.sh` C-3 compara clases *usadas* contra *definidas* y por construcción no puede ver una clase definida y sin emisor —que es el P0—. `tools/informe-cobertura.cs` corta por ensamblado y no por componente, de modo que `QG-06` (95 % en los dos motores) no tiene instrumento posible ni siquiera cuando la puerta corre. `grep -rn 'TC-' tests/` devuelve **0** sobre **52** archivos de prueba, contra **36** identificadores `TC-` que `Pipeline-CI-CD.md` nombra como instrumento de un gate: no hay forma de contestar «¿qué prueba cubre `QG-00005`?» ni la inversa. Y para las decisiones no hay ninguna verificación: `D3` reformuló la vigencia el 2026-08-26 y llegó a tres documentos de siete; `D4` dejó seis anclados a la etapa `a`, cerrada el 2026-08-13; `D8` dejó catorce filas con la palabra «todavía». El corpus sabe probar que algo está escrito. **No tiene un solo instrumento capaz de probar que lo escrito se realizó.** `QG-00005` ya nombra este modo de falla para un caso puntual; lo que la mesa midió es que es la forma general de fallar de este producto.

**Lo que no está roto conviene decirlo, porque decide el plan: la disciplina hacia abajo es alta y el diseño acertó donde se lo pudo medir.** Cada superficie que viró a render estático dejó sus apartamientos numerados en la cabecera del componente; el peso por fila del JSON se mantiene en 257–262 B de 30 a 1002 trabajos, que es exactamente la propiedad estructural que `A-5` declara, con p99 de 12,3 ms contra 500 ms —margen de 40x—; la guardia de arranque contra la mala configuración es coherente de punta a punta; el sellado de la imagen con su revisión real está cerrado. **El producto contrasta hacia abajo y nunca hacia arriba.** Por eso el plan no es «arreglar doce cosas»: es **subir cada contraste a la capa que decide y dejar en el árbol la compuerta que mide la dirección inversa**, empezando por lo único que no se deshace —las entregas de la comisión no tienen respaldo y `MigrateAsync` corre en cada arranque sobre el almacén real— y por lo único que le quita función a una persona hoy —el docente en un teléfono ve un recuento que afirma trabajos y cero filas—.

---

## 2 · EL PLAN

Quince unidades, **una por PR**. **No hay estimación de esfuerzo: `D2` retiró la unidad de estimación y el producto no estima.** El costo va en la escala de la mesa (bajo / medio / alto), que mide amplitud de diff y riesgo de regresión, no tiempo.

### Por qué este orden, dicho antes de la lista

1. **Primero lo irreversible.** De los doce hallazgos, exactamente uno tiene un modo de falla que no se deshace: sin respaldo, un `down -v` o una transformación equivocada se lleva las entregas y no hay nada que restituir. Todo lo demás se revierte con `git revert`.
2. **Segundo lo que le quita función a una persona hoy**, aunque sea documental el arreglo de sus derivados.
3. **Tercero, y antes de cualquier barrida de texto: la compuerta.** La causa común es que lo escrito no tiene quién lo contraste. Una barrida documental sin compuerta se deshace por lectura —es literalmente lo que pasó con `PA-05`, cerrado once días antes de que la mesa encontrara el P0 adentro de la regla que ese cierre había leído—. Por eso `U-04` va antes que las nueve unidades documentales que la siguen.
4. **Dentro de cada frente, la capa que decide antes que la que deriva**: `ADR-10001` antes de los wireframes; `ADR-00009` antes de las catorce filas de mutación; `08-Calidad-Y-Pruebas` antes de `09-Devops`.
5. **Las líneas de base se fijan después de los cambios de marcado**, nunca antes: `U-09` mide después de `U-02`, o la puerta nace en rojo el mismo día.
6. **Lo caro y de rendimiento decreciente al final** (`U-15`), donde se lo puede parar sin dejar nada a medias.

---

### FRENTE A — Los datos (irreversible)

**U-01 · El respaldo existe, y el diario es el que la fuente declara**
- **Resuelve:** hoy el único mecanismo de vuelta atrás que el corpus declara no existe en ningún árbol (`grep -rlEi "backup|respaldo|restaurar|sqlite3|vacuum into" scripts/ deploy/ .github/` → `scripts/reset-db.sh`, que es el que **vacía** el almacén, y una mención en `deploy/compose.yaml`), y la única especificación escrita para construirlo describe un modo de diario que el producto no usa.
- **Cierra:** MI-01 (con OP-05 fusionado). **Cambia el contenido de `PD-04`** de «no hay respaldo» a «falta la política».
- **Contiene:** `StorePreparation.PrepareAsync` fija y **comprueba** `PRAGMA journal_mode=WAL` (lee lo que devuelve, no asume); `scripts/respaldo-almacen.sh` (`VACUUM INTO`, sin detener el servicio, se niega si el destino es el directorio del almacén, `integrity_check` y borra la copia si no verifica); `scripts/restaurar-almacen.sh` (exige servicio detenido comprobando el `-wal`, no borra el almacén anterior, lo aparta con sello); `sqlite3` entra a `deploy/Dockerfile` junto a `curl` con su motivo escrito; `Entornos-Deploy.md` §11.1, `Guia-Publicacion-Image-Docker.md` §4 y `Pipeline-CI-CD.md` `PD-04` dejan de describir un mecanismo inexistente y **declaran la ventana de indisponibilidad de la restitución**, que hoy no está escrita en ninguna parte (es la de `ADR-00007` §6 punto 3: no hay decisión nueva que tomar).
- **Costo:** medio. **Riesgo:** el de restauración es el artefacto más peligroso que esta mesa produce, hermano de `reset-db.sh`, que ya destruyó datos el 2026-08-15: por eso pide confirmación por estado del archivo y aparta en vez de borrar. **`journal_mode=WAL` es persistente y no se revierte con `git revert`**: queda en el encabezado del archivo, igual que el esquema sobrevive a volver a la etiqueta.
- **Habilitada por:** nada. **Bloquea a:** U-03.

**U-03 · `/salud` deja de ser un sello del arranque**
- **Resuelve:** `TwoPhaseStartup.cs:34` escribe `StoreIsPrepared` una sola vez y nada la reevalúa; `HealthEndpoint` la publica; `deploy/compose.yaml:57-64` sondea esa ruta cada 30 s. El servicio informa 200 «Ready» para siempre con el almacén borrado, de sólo lectura o corrupto. **`ADR-00007` §2 puntos 4 y 5 ya prohíben esto: la decisión estaba bien tomada y no se realizó.**
- **Cierra:** MI-09 (5P/0N/0I; el jurado corrigió el ancla a **E1**, no E2).
- **Contiene:** `StoreHealth` en Infrastructure (recuento de `__EFMigrationsHistory` —abrir la conexión no alcanza: SQLite crea el archivo al abrirlo— más `BEGIN IMMEDIATE; ROLLBACK;`); `/salud` responde 503 sin decir por qué (`RA-03`); `ADR-00007` §7 suma la convención, la decisión **no cambia**; `compose.yaml` **no se toca**.
- **Costo:** medio. **Riesgo:** verifiqué que `restart: unless-stopped` **no** reacciona a un healthcheck en rojo —Docker reinicia por salida del proceso—, de modo que no se crea un ciclo que recrearía el esquema vacío; queda declarado que esa propiedad hay que revisar si alguna vez entra un supervisor que sí reaccione. **No detecta disco lleno**, y eso se dice en vez de suponerse.
- **Habilitada por:** U-01. **Por qué después:** poner el aviso en rojo antes de que exista la copia deja al operador viendo el problema y sin salida.

### FRENTE B — La pieza pública

**U-02 · Las tres listas dibujan filas por debajo de 768 px, y el grupo recupera su encabezado**
- **Resuelve:** el P0 más caro de la mesa, y la mitad de accesibilidad que toca el mismo bloque de código.
- **Cierra:** MI-02 + MI-11 (fusionados: los dos tocan `ClassSubmissionList.razor:158-207`; en secuencia romperían el bloque).
- **Contiene:** tarjetas apiladas en los tres listados; `<h2>` dentro del `<summary>` del grupo; ancla de retorno (`id="abrir-{WorkId}"` + `BackHref` con fragmento) para que el foco vuelva a la fila; párrafo de recuento con `role="status"` y `action` con fragmento para que el resultado del filtro se anuncie bajo render estático; **control `C-5` nuevo en `verify-visual-system.sh`: toda clase que la versión angosta enciende la emite algún componente**.
- **Costo:** medio. **Riesgo:** puramente aditivo, cero efecto por encima de 768 px, `git revert` de tres archivos. El riesgo está entero del lado de no corregir. **Límite declarado:** por debajo de 768 px el fragmento de retorno no tiene destino visible; queda como deuda menor con su lugar de registro (U-04).
- **Bloquea a:** U-09 (la línea de base del peso se fija **después** de este cambio) y U-10.

**U-05 · `ADR-10001` v1.1: el reparto de modos de render entra a la capa que decide**
- **Resuelve:** la ADR de estilo declara «toda interacción viaja por un circuito» y **nueve de quince superficies hacen lo contrario desde la etapa `c`**; la alternativa que el código adoptó no figura entre las cuatro que §4 evaluó; el control de cambios tiene una sola fila y §3 dice «Propuesto» con cabecera «Aprobado». **Corrijo el contrato de entrada: son SEIS superficies interactivas, no ocho** —verificado con `grep -rl '^@rendermode' Components/Pages/`: `InitialDestination`, `InitialProvisioning`, `NotFoundPage`, `OwnCredentialChange`, `OwnCredentialSetup`, `Status`—. `App.razor` no está en `Pages/` y `AccountRegistration.razor` declara lo contrario en su cabecera.
- **Cierra:** MI-06. **Absorbe:** ARQ-01, cuyo enunciado («no hay criterio declarado») es falso y refutable de un grep.
- **Contiene:** la tabla de reparto superficie por superficie como parte de la decisión; la quinta alternativa; la contingencia re-dimensionada —la salida preferente alcanza a seis superficies, no a quince—; la convención «una superficie nueva es estática salvo que declare por qué no puede serlo».
- **Costo:** medio (un documento). **Riesgo:** el jurado marcó INSUFICIENTE en reversibilidad porque el hallazgo no fijaba dirección. **La dirección la fijo acá y es vinculante: se actualiza la ADR al código. El código no se toca.** Alinear el código a la ADR volvería interactivas nueve superficies sobre un hosting sin WebSocket con `PT-01.b` en amarillo estable, y eso toca topología cerrada.
- **Bloquea a:** U-06, U-07, U-09, U-10 (los cinco citan su §7).

**U-06 · Las promesas inaplicables bajan a su alcance real**
- **Resuelve:** el cartel de reconexión está declarado para once superficies y es estructuralmente imposible en nueve —sin circuito no hay corte de circuito—; el esqueleto por fila está prometido en tres wireframes y `grep -rn gf-skeleton src/` devuelve **cero** usos fuera de `app.css`; `Wireframes-Vista-De-Trabajo.md:145` celebra una propiedad sobre una superficie que no abre circuito.
- **Cierra:** MI-07 (mitad de diseño), MI-10 (mitad de wireframe), MI-12 (mitad de producto).
- **Contiene:** `Linea-Base-Visual.md` `EST-10011` acotado con **la nota de aplicación obligatoria** (cruzar `ADR-10001` §7 con el mapa superficie→componente: `Credencial-Propia` tiene tres componentes y `OwnCredentialForcedChange` es estático → se declara *parcialmente aplicable*); las filas «Cargando» y «Reconectando» de los tres wireframes; `ADR-10005` §2 precisa que **sus dos tramos no alcanzan a las mismas superficies**; `Wireframes-Panel-De-Cuentas.md:220` recibe la deuda de `A-06`.
- **Costo:** medio. **Riesgo:** cinco documentos, cero ejecutable. **Ningún recuento de la línea de base baja**: el estado sigue existiendo y sigue validado; lo que cambia es en cuántas superficies se sensa.
- **Derivado que hay que abrir y no está en ningún hallazgo:** `Wireframes-Panel-De-Trabajos-Del-Alumno.md`, tercera superficie de listado, puede repetir las dos promesas.

**U-07 · El cartel de reconexión se enciende donde sí aplica**
- **Resuelve:** en las seis superficies con circuito, un corte deja la pantalla presente y los controles inertes sin ningún aviso, con el texto aprobado construido y nunca mostrado.
- **Cierra:** MI-07 (tramo de código). **Corrige** el conteo de UX-05, que nombraba ocho superficies e incluía el registro de cuenta, que es estático.
- **Contiene:** `DegradedStateOverlay` pasa a llevar `id="components-reconnect-modal"` —lo enciende el marco, no el componente— y se elimina el `[Parameter] Visible`; tres reglas en `app.css` sin un solo literal de color; los dos shells **no se tocan** y ahora instanciarlo a secas es correcto. Los tramos `failed`/`rejected` quedan **sin texto a propósito**: escribirlos sería inventar castellano de producto.
- **Costo:** bajo. **Riesgo:** bajo. Condición explícita: **encender el aviso no se resuelve convirtiendo superficies estáticas en interactivas.**

**U-08 · La corrección del informe de pintado §4**
- **Resuelve:** «El filtro no escala con el total, porque el recorte lo hace el servicio» es falso: `ClassSubmissionList.razor:323` pide la colección **completa sin criterio** para poblar el selector y `:340` pide la filtrada. Los propios números lo muestran: 101 · 129 · 97 · **305** ms, con el salto justo donde la primera trae 1002 filas y el selector dibuja 334 opciones. **Es la única evidencia medida que hoy sostiene no paginar.**
- **Cierra:** MI-03, con el enunciado reformulado que pidió el jurado: **«la propiedad que el informe atribuye no existe en el código»**, no «el filtro no escala» —esa mitad se apoyaría en una corrida única por corte—.
- **Contiene:** §4 reescrito; fila 1.1 del control de cambios (la 1.0 **no** se reescribe); el dato que sí queda medido: **filtrar cuesta dos peticiones a `A-13`**, y `PT-05` tiene que contar peticiones HTTP y no acciones del docente o el caudal sale a la mitad; comentario en `:339`.
- **Costo:** bajo. **Riesgo:** la corrección del código **no** es borrar la primera llamada —`Students` se deriva de `_unfiltered` y podarla vacía el selector—: esa mitad va a deuda con reemplazo requerido.

**U-09 · La puerta determinista de peso de documento, `/cuentas` medida, y los bytes que cruzan el cable**
- **Resuelve:** el producto midió su peor superficie —96 ms/40 KB a 30 trabajos, **1579 ms/1,2 MB a 1002**— y no hay una sola puerta que pueda ponerse en rojo por eso: `QG-14` es del servicio y tiene margen de 40x, `PT-05` mide caudal, y `Web/08` declaró ausentes sus dos puertas. Y el Panel de Cuentas, que es la misma superficie de riesgo, con fila **más pesada** (un `<form>` con `@formname` y `AntiforgeryToken` más dos anclas con SVG por cuenta) y **filtro que no puede recortar en el servicio** (`ListAccountsAsync(string?, CancellationToken)`: `A-06` no admite criterio), nunca se midió: `medicion-pintado-del-listado.mjs:62` sólo navega a `/entrega-comision`.
- **Cierra:** MI-10 (accionable) + MI-12 (accionable). **Incorpora N-1.**
- **Contiene:** NFR nuevo en `Web/05` §8 —**donde nace un NFR**, no en `08`—: peso del documento por corte de volumen, ≤ 110 % de una línea de base registrada, cuatro cortes × dos superficies; `CV-23` y `QG-12` en los tres documentos de `Web/08` («Once gates» → «Doce gates»); el instrumento mide dos superficies y **emite `transferSize` además de `content().length`**, con las dos columnas nombradas distinto: «marcado emitido» y «bytes transferidos».
- **Costo:** medio. **Riesgo:** la línea de base se registra **después** de U-02, o la puerta nace en rojo. `PA-04` **se queda en NO APLICA**: lo que entra no es un umbral de latencia —comprometería un hosting cuya latencia la fuente declara incógnita— sino bytes deterministas medibles sin hosting.
- **Habilitada por:** U-02, U-05.

**U-10 · `Web/05` §11.1: `PA-05` reabierto, `PA-06` corregido, el recuento arreglado**
- **Resuelve:** `PA-05` está **Cerrado** desde el 2026-08-20 «por lectura» sobre la regla que contiene el P0; `PA-06` dice que la superficie afectada es sólo el listado; y el párrafo de cierre de la versión 3.8 dice «cuatro abiertas» contra cuatro celdas que dicen **Cerrado** y «`PA-04` sigue abierto» contra una celda que dice **NO APLICA**. **El estado del ítem que gobierna el umbral de la espera quedó indeterminado en su propio documento durante una versión entera.**
- **Cierra:** la mitad de gobierno de MI-02, MI-10 y MI-12. **Lo que sigue abierto en `PA-05` es lo que la fila siempre preguntó y el cierre nunca contestó:** si 768 px y 4:3 son valores del producto. Eso es del PO.
- **Costo:** bajo (un archivo). **Riesgo:** bajo. **Derivado obligatorio:** `PA-01` se cerró el mismo día y por el mismo método «A2b, por lectura». **El método falló una vez; hay que releer las otras filas cerradas así**, y esa lección no está registrada en ninguna parte.
- **Habilitada por:** U-02 (la construcción) y U-09 (`PA-06` cita `CV-23`).

### FRENTE C — El aparato declarativo de la API

**U-11 · `D4`: el límite de cuerpo se declara, y su ausencia falla**
- **Resuelve:** `D4` cerró el 2026-08-20 adoptando el valor por omisión del servidor, con la obligación derivada de declararlo al tocar la composición. **El disparador ocurrió** (`630dec4`, 2026-08-30) y `grep -rn 'MaxRequestBodySize' src/` devuelve **0**. Seis documentos vivos siguen anclando el número a la etapa `a`, cerrada el 2026-08-13.
- **Cierra:** MI-04.
- **Contiene:** `ADR-00002` v1.1, `Api/05/README.md`, `BT-00009`, `Mini-Plan.md`, `Definicion-Superficie-HTTP.md`, `Especificacion-Funcional.md`; `Composition/RequestBodyLimit.cs` con `30_000_000` —**el valor por omisión exacto de Kestrel**— y control `C-3` en `verify-explicit-configuration.sh`.
- **Costo:** medio. **Riesgo:** **es el único ítem documental que toca comportamiento en ejecución.** Escribir un número distinto al default empieza a rechazar envíos con 413: se declara el default vigente y no se elige uno nuevo. Si alguna prueba de integración cambia de resultado, el que estaba mal era el supuesto.

**U-04 · La compuerta de la dirección inversa y el registro durable de diferidos** *(va acá en el orden real: cuarta, antes de toda barrida — ver la lista ordenada al final)*
- **Resuelve:** la causa común. Sin esto, las nueve unidades documentales que siguen son reversibles por lectura y el patrón vuelve en la próxima decisión.
- **Contiene N-2 y N-4.** Detalle en la sección 5.

**U-12 · `ADR-00009`, la vigencia del acceso, y el mutation score**
- **Resuelve:** dos verdades simultáneas sobre un umbral de seguridad —`SigningOptions.cs:25` dice `= 480` y nada lo sobreescribe, mientras `CU-06008:110`, `ADR-00003:31`, `ADR-06004:31` y `:61` y `Api/05/README.md:102` dicen que no hay número y que se ancla en una etapa cerrada—; y catorce filas que exigen un mutation score que `D8` decidió no medir nunca, con la palabra «todavía» (`grep -rc 'exigible todavía'`: `Criterios-Validacion.md` **8**, `Definition-Of-Done.md` **4** — más de las que el panel contó).
- **Cierra:** MI-08. **Corrige** el recuento de `A3-Decisiones-Del-Product-Owner.md:305` («las TRES filas del árbol»: eran siete).
- **Aporte del presidente, verificado y no reportado por nadie — MI-13:** `ADR-06004` tiene un **segundo** anclaje vivo a la etapa `a` que ningún hallazgo abrió: la elección de la función de derivación. §2 línea 17, §4 línea 45, §6 punto 2 y §7 línea 70 dicen que se ancla en la etapa `a` y «sigue como punto abierto». **El código ya eligió**: `PasswordDerivation.cs:69` es `Rfc2898DeriveBytes.Pbkdf2`, con el criterio de la propia ADR escrito en su cabecera (`:18-22`). Es el patrón exacto de MI-08, en el mismo archivo, y entra en esta unidad porque el diff ya está abierto ahí.
- **Contiene además:** `ADR-00009 · El mutation score no se mide y se declara`, que es **dependencia dura** de U-13 y U-14: sin ella la tanda cambia una deuda con vencimiento falso por un apartamiento invisible de `Rules-Calidad-Y-Pruebas.md` §2.2, que es el mismo defecto un piso más abajo. Nace **Propuesto**: la aceptación es del PO, como `ADR-14004`.
- **Costo:** medio. **Riesgo:** el más bajo de la lista; nada se ejecuta.

**U-13 · El vocabulario de tres caracteres y las tablas de `08`**
- **Resuelve:** la **causa** de las 36 filas «Condicionado»: `Estrategia-Calidad.md` conserva **viva** la regla que las ordena —«la puerta **no se declara bloqueante** en `09-Devops` hasta que el Product Owner los confirme»— cuya condición se cumplió el 2026-08-26. Mientras esa frase esté escrita, cualquier corrección aguas abajo se «repara» de vuelta por lectura. Y `Matriz-Cobertura-Pruebas.md` §7 justifica cuatro tablas enteras en «Sin medir» con «**No hay código construido**», que es falso desde hace días (`scripts/coverage.sh` del 2026-08-27, medición real del 2026-08-29 en `coverlet.runsettings`, 504 pruebas en verde).
- **Cierra:** MI-05 en su capa de origen. Fusiona las filas de MI-05 y MI-08 de `Criterios-Validacion.md`, que son **adyacentes en las mismas cuatro tablas**.
- **Contiene:** el vocabulario **Bloqueante / Confirmado y sin instrumento / Condicionado**, que es la pieza que faltaba —«confirmado y sin instrumento» no es «condicionado» con otro nombre: en el primero **nadie está esperando al PO y el ítem que falta es del equipo**—; las 16 filas de `Criterios-Validacion.md`; las 11 de `Estrategia-Calidad.md` y sus cuatro párrafos de regla; §7 y §8 de `Matriz-Cobertura-Pruebas.md`, con la fila «Proyecto de código completo» llenada **con una corrida real y su fecha**, no transcripta.
- **Costo:** medio. **Riesgo:** amplio en filas, nulo en ejecución. **Condición dura: la barrida no puede encender como bloqueante ninguna puerta que hoy carezca de instrumento** — un gate bloqueante sin instrumento pasa por omisión, que es peor que no tenerlo.
- **Habilitada por:** U-12 (cita `ADR-00009`).

**U-14 · El reflejo en `09-Devops` y la regla replicada en `06`/`07`**
- **Resuelve:** que `09` mienta en las dos direcciones —cinco filas «Condicionado» en `README.md` y `Guia-Publicacion` contra seis filas «Bloqueante desde el 2026-08-26» en `Pipeline-CI-CD.md` que **no tienen instrumento**— y que la regla vencida siga replicada. **Corrijo el conteo del panel:** `grep -rn 'no se declara bloqueante'` sobre la unidad Api da **12 apariciones vivas en cuatro documentos**, no siete: `Estrategia-Calidad.md` 4, `Backlog-Tecnico.md` 3, **`Mini-Plan.md` 4** y **`US-04015` 1**, estos dos últimos sin nombrar por nadie.
- **Cierra:** MI-05 en su capa reflejo.
- **Contiene:** las once celdas de `Pipeline-CI-CD.md` (cinco pasan a «bloqueante **y con instrumento**», seis a «confirmada su asunción y **sin instrumento**»); el párrafo que dice la cuenta que el documento debe —**de quince gates la canalización automatiza dos**, `.github/workflows/` tiene un solo flujo y es de FTP del front—; `BT-00025`, `BT-02015`, `BT-04018` **cerrados con su desenlace, no retirados** (tienen objeto y el trabajo se hizo el 2026-08-26); las cuatro de `Mini-Plan.md` y la de `US-04015`.
- **Rider aprobado por el presidente:** el residuo P3 de OP-02 —`Entornos-Deploy.md` §2.1 y `Guia-Publicacion-Image-Docker.md` §2 siguen describiendo `deploy/compose.yaml` como el despliegue en destino sin la salvedad que hoy vive sólo en el comentario del archivo y en `changelog.md:729`— entra acá porque **este PR ya abre los dos archivos**. Dos oraciones, no un hallazgo.
- **Costo:** bajo. **Habilitada por:** U-13 (`09` refleja a `08`, nunca al revés).

**U-15 · La trazabilidad `TC` en la batería**
- **Resuelve:** `grep -rn 'TC-' tests/` = **0** sobre **52** archivos, contra **36** `TC-` nombrados como instrumento de un gate en `Pipeline-CI-CD.md` y **130** en el catálogo. Hoy no se puede contestar qué prueba cubre qué puerta en ninguna de las dos direcciones.
- **Cierra:** MI-05, tramo de código. Su criterio (`CV-41`) lo mandata U-13: **hoy las 52 clases no incumplen nada porque nadie lo pidió nunca.**
- **Contiene:** `[Trait("TC", "TC-xxxxx")]` en las clases que realizan los **36** `TC` de gate —no los 130—, y `TrazabilidadDeCasosTests` con dos afirmaciones opuestas. **Ajuste del presidente al parche, para que la batería no nazca en rojo y sin abrir una excepción que tape:** el caso de la primera dirección no afirma «cero gates sin prueba», afirma que **el conjunto de `TC` de gate sin prueba es exactamente `{TC-00033, TC-00034, TC-06015}`** —los tres que el catálogo tiene y nadie escribió—, y se pone en rojo en las dos direcciones: cuando aparece un gate huérfano nuevo y cuando alguien escribe uno de los tres y olvida sacarlo de la lista.
- **Costo:** alto. **Riesgo:** ancho, revertible entero con `git checkout -- tests/`, cero efecto sobre el producto. **Se aplica entero o no se aplica.** Es la unidad donde el plan se puede parar sin dejar nada a medias.

---

### ORDEN DE EJECUCIÓN, definitivo

| # | Unidad | Frente | Costo | La bloquea |
|---|---|---|---|---|
| 1 | **U-01** El respaldo existe y el diario es WAL | Datos | medio | — |
| 2 | **U-02** Las tres listas a 768 px + encabezado de grupo | Pública | medio | — |
| 3 | **U-03** `/salud` vuelve a evaluar el almacén | Datos | medio | U-01 |
| 4 | **U-04** Compuerta de propagación + registro de diferidos | Transversal | medio | — |
| 5 | **U-05** `ADR-10001` v1.1, el reparto de modos de render | Pública | medio | U-04 (conveniencia) |
| 6 | **U-06** Las promesas inaplicables bajan a su alcance | Pública | medio | U-05 |
| 7 | **U-07** El cartel de reconexión se enciende | Pública | bajo | U-06 |
| 8 | **U-08** Corrección del informe de pintado §4 | Pública | bajo | — |
| 9 | **U-09** Puerta de peso + `/cuentas` + bytes en el cable | Pública | medio | U-02, U-05 |
| 10 | **U-10** `Web/05` §11.1 | Pública | bajo | U-02, U-09 |
| 11 | **U-11** `D4`, el límite de cuerpo | Api | medio | — |
| 12 | **U-12** `ADR-00009` + `D3` + `D8` + `ADR-06004` | Api | medio | U-04 |
| 13 | **U-13** Vocabulario y tablas de `08` | Api | medio | U-12 |
| 14 | **U-14** Reflejo en `09` + regla en `06`/`07` | Api | bajo | U-13 |
| 15 | **U-15** Trazabilidad `TC` | Api | alto | U-13 |

---

## 3 · IMPLICANCIAS DE EJECUTARLO

**Qué se rompe, dicho con nombre y archivo:**

- **`journal_mode=WAL` no se revierte con `git`.** Queda grabado en el encabezado del archivo del almacén, exactamente como el esquema sobrevive a volver a la etiqueta (`Guia-Publicacion-Image-Docker.md` §4). Devolver un almacén ya convertido exige `sqlite3 <almacén> 'PRAGMA journal_mode=DELETE;'` con el servicio detenido y borrar los `-wal`/`-shm`.
- **`reset-db.sh` pasa de defensivo a necesario**: ya borra `-shm` y `-wal`, y ahora esos archivos existen de verdad. Hay que revalidar que ningún otro guion borre sólo el `.db`.
- **Los cuatro cortes de la medición B dejan de valer** el día que se aplica U-02: las tarjetas apiladas suman una segunda copia por fila. La línea de base de `CV-23` se registra **después**, y `Medicion-Pintado-Del-Listado-2026-08-31.md` pasa a versión 1.2 con dos superficies.
- **La batería suma casos** (WAL, salud, trazabilidad) y `QG-02` acota la batería en 10 s. **Hoy nada la compara contra ese número** —es una de las seis puertas «confirmada y sin instrumento»—, de modo que el techo se puede cruzar sin que nada avise. Es la primera consecuencia práctica de MI-05 sobre este mismo plan.
- **`verify-visual-system.sh` pasa a cinco controles** y `verify-explicit-configuration.sh` a tres. Revertir U-02 sin revertir su control deja `C-5` en rojo; ídem U-11 con `C-3`.

**Qué hay que revalidar, en orden de probabilidad de sorpresa:**
`PT-04` / `QG-00013` (arranque en frío en 30 s: el PRAGMA suma una operación y `/salud` dos consultas por pedido) · `TC-00026` (el 503 nuevo no nombra el almacén) · `TC-00031` y `QG-11` (cero peticiones con la preparación incompleta) · `TC-06032` (transformaciones sobre almacén inexistente) · `verify-navigation.sh` y los guiones de etapa que leen `<h1>`/`<h2>` del cuerpo · `TC-10035` (guion de demostración: suma un paso a 360 px) · los recuentos de `Linea-Base-Visual.md` §1, que **no cambian** · `Matriz-Sensado-Deriva.md` `SD-10014`, que pasa de «Sin verificar» a **NO APLICA con motivo** en nueve superficies · el costo por sondeo de `/salud` contra el escritor único de `ADR-06002`, antes de bajar el intervalo de 30 s.

**Qué le destraba al Product Owner:**
- **`PD-04` pasa de incontestable a contestable.** Hoy la pregunta aparcada es «cada cuánto», y la respuesta correcta era «no hay mecanismo». Después de U-01 la pregunta que queda es sólo suya y son tres números: frecuencia, directorio, retención.
- **`D1` pasa a estar realmente aplicado** en las 36 filas y en la regla que las gobierna, en vez de aplicado en dos documentos.
- **`PA-04` queda explicado sin reabrirse**, con la nota que impide que la próxima lectura tenga que redescubrir por qué.
- **`PA-05` vuelve a tener su pregunta original visible** (768 px y 4:3) en lugar de estar cerrado sobre otra cosa.

**Qué le exige, y son siete decisiones concretas:**
1. **Frecuencia, destino y retención del respaldo** (`PD-04`). Ya era suya; ahora tiene sobre qué decidir.
2. **Aceptar `ADR-00009`**, el apartamiento del mutation score frente a `Rules-Calidad-Y-Pruebas.md` §2.2. Nace Propuesto y catorce filas la citan.
3. **Si 480 minutos cumplen el criterio «que caduque dentro de la sesión de trabajo de una clase».** Es la única pregunta que queda de `D3`, y no se puede contestar leyendo: **ninguna fuente del corpus declara cuánto dura una clase.**
4. **`I-8` en la puerta de la fase `i`** (ver N-3): toca `Roadmap-Producto.md` §5.2, que es capa `00-Contexto`.
5. **768 px y la proporción 4:3** como valores del producto, sobre la línea de base visual.
6. **Si `A-06` alguna vez lleva criterio de filtro**: cambia el contrato de un punto de acceso publicado y obliga a los dos extremos.
7. **Declarar explícitamente `AccessToken:LifetimeInMinutes` en el ambiente**, que es la obligación derivada de `D3` y hoy vive de un valor por omisión. Es de la fase `i`.

---

## 4 · LO QUE NO ENTRA AL PLAN, Y POR QUÉ

**Deuda declarada** — cada una con disparador y dueño, y todas con lugar de registro desde U-04 (hoy no lo tienen, y ése es el motivo de que el patrón se repita):

| Ítem | Por qué no entra | Disparador de reapertura | Dueño |
|---|---|---|---|
| **Criterio de filtro en `A-06`** | Cambia el contrato HTTP y obliga a ADR, `Contratos-REST.md`, punto de acceso, integración e historia, para un filtro que hoy funciona. Y `D5` cerró el volumen **por incognoscible**: dimensionar un cambio de contrato contra un volumen que nadie puede saber es lo que esa decisión prohíbe | `CV-23` en rojo sobre `/cuentas`, o `PT-05` midiendo el caudal de `A-06` | Product Owner |
| **Corte por componente del informe de cobertura** (`QG-06`, 95 % en los dos motores, y cinco `CV`) | El mapa componente→archivos **no existe en el corpus**, y escribirlo dentro de `tools/informe-cobertura.cs` sería decidir la arquitectura desde el instrumento: el número quedaría contrastable en una sola dirección, que es la enfermedad | Que `05` §3.1 declare qué archivos realiza cada componente. **Ese es el prerequisito, y es del equipo, no del PO** | Equipo, capa `05` |
| **El esqueleto por fila / `StreamRendering`** | Exige cambiar el modo de render de la superficie principal o adoptar render en flujo; reescribiría el filtro por dirección y choca con la topología. Cuesta más que los 1579 ms que evita | Un requisito de tiempo de respuesta declarado por alguna fuente | Product Owner |
| **Paginación del listado** | No se propone. U-08 sólo **retira el argumento medido** que sostenía no paginarla; no la pide ni la descarta | `D5` reabierto, o `PT-05` | Product Owner |
| **La segunda petición sin criterio del filtro** | No se poda: `Students` se deriva de `_unfiltered` y quitarla vacía el selector. Entra con su reemplazo o no entra | Un cambio de `A-13` que devuelva el padrón de alumnos aparte | Equipo |
| **Texto de los tramos `failed`/`rejected` del cartel** | Escribirlo sería inventar castellano de producto: `Datos-Maqueta.js` `TEXTOS.reconexion` tiene una sola frase | El PO aprueba dos frases | Product Owner |
| **Compresión de respuesta** | **No se decide sin el número.** El «8x a 12x» del panel no está medido, el montaje de la medición fueron dos contenedores en la misma máquina sin tramo de red, y con `hostingModel=inprocess` la compresión dinámica depende de una configuración de somee que el corpus no declara. N-1 mide primero | El `transferSize` de N-1 sobre el despliegue real | Equipo, después de N-1 |
| **Regla `.gf-inert`** | Queda sin encender después de U-07. Retirarla es otra decisión y otro diff | El próximo barrido de `C-3` | Equipo |

**Fuera de alcance por contrato, y no se discute:** el framework SDD (se le reportan hallazgos: a los tres del reporte 16 pendiente este ciclo suma **la verificación en una sola dirección como modo de falla nombrable** y el marco base de la mesa) · las carpetas `PROMPTs/` · la topología, el repliegue a long polling y la ausencia de MudBlazor · lo que exige destino real.

**Hallazgos que la conciliación tumbó y no vuelven:** Formal R-05 (`PA-04`: la condición de reapertura es una **disyunción** y su primera mitad es un acto documental verificable; el hallazgo presentaba como falla la decisión que citaba como fundamento) · Rendimiento R-01 y R-04 como P1 (se apoyaban en «1,2 MB de descarga real» sobre un montaje de dos contenedores en la misma máquina, y R-04 demostraba que la cifra no son bytes; quedan como la corrección de palabra que U-08 y U-09 aplican) · OP-02 como P1 (`changelog.md:729` lo explica y lo declara elevado; queda el residuo P3, que entra de rider en U-14) · ARQ-01 (su enunciado central es falso y se refuta de un grep; su barrido de quince páginas vive en U-05).

---

## 5 · COSAS NUEVAS

Cuatro, ninguna de negocio. Las cuatro salen de mirar el producto entero, no de un hallazgo.

### N-1 · Medir los bytes que cruzan el cable, y recién entonces decidir la compresión
**Fundamento medido:** `tools/medicion-pintado-del-listado.mjs:78` es literalmente `const bytes = (await pagina.content()).length;` — unidades UTF-16 del DOM ya reserializado por el navegador. El informe publica esa cifra bajo el encabezado «Documento» (40 KB / 125 KB / 358 KB / **1,2 MB**) y la compara contra los 257 KB del JSON, que **sí** son bytes de respuesta HTTP. Y `grep -rniE 'UseResponseCompression|Brotli|Gzip' src/ deploy/` devuelve **0**: el canal de `Program.cs:80-131` no comprime y `.publish-web/web.config` no declara ni `<httpCompression>` ni `<urlCompression>`.
**Qué es:** el instrumento emite dos columnas con nombres distintos —«marcado emitido» (`content().length`) y «bytes transferidos» (`transferSize` de la entrada de navegación)—, de modo que quien reejecute en la fase `i` pueda ver si la compresión cambió algo. Hoy obtendría el mismo número aunque se activara. **Con el número sobre la mesa, `UseResponseCompression` es una línea de canal y una decisión de una sola frase; sin el número es una conjetura.**
**Costo:** bajo. Entra en U-09. **Lo que compra:** la única palanca barata que el producto tiene sobre su propio riesgo declarado, decidida con una medición en vez de con un «entre 8x y 12x» que nadie midió.

### N-2 · `verify-propagacion.sh`: la compuerta de la dirección inversa para las decisiones
**Fundamento medido, y son cuatro mediciones sobre el mismo defecto:** `D3` llegó a 3 de 7 documentos · `D4` dejó 6 anclados a una etapa cerrada el 2026-08-13 · `D8` dejó 12 apariciones de «exigible todavía» (8 en `Criterios-Validacion.md`, 4 en `Definition-Of-Done.md`) · `D1` dejó **36** filas «Condicionado» en seis documentos y **12** apariciones vivas de la regla que las ordena, en cuatro. Y hay una quinta señal que nadie contó: **49 documentos vivos de la unidad Api nombran «la etapa `a`»**, cerrada el 2026-08-13, y la mesa auditó diez de esas menciones. Nadie clasificó las otras treinta y nueve.
**Qué es:** un guion que lee un registro declarativo —`SDD/Docs/Audit/Decisiones-Y-Frases-Retiradas.md`, una tabla de `decisión → frase que dejó de ser cierta → documentos que la pueden llevar`— y **falla si alguna frase retirada sigue viva fuera de un control de cambios**. Es la generalización de `C-5` y de `TrazabilidadDeCasosTests`: **la única clase de verificación que este producto no tiene es la que pregunta «¿quedó algo diciendo lo contrario?»**. La primera corrida lo alimenta con lo que este plan retira: «no se declara bloqueante», «exigible todavía», «no está declarado por ninguna fuente», «se ancla en la etapa `a`», «No hay código construido».
**Costo:** medio. Es U-04. **Lo que compra:** que las nueve unidades documentales de este plan no sean reversibles por lectura, que es exactamente cómo se perdieron once días con `PA-05`.

### N-3 · `I-8` en la puerta de la fase `i`: el respaldo probado antes del primer despliegue real
**Fundamento medido:** `verify-stage-i.sh` mide siete criterios (`I-1` a `I-7`) y **ninguno es el respaldo**; `PD-04` está aparcado en ese mismo punto de control y no lo contesta; `deploy/compose.yaml` monta el almacén en un **volumen con nombre** (`store:/datos`), no en un bind mount que se pueda respaldar copiando; y el riesgo crece con cada trabajo entregado desde la etapa `e`.
**Qué es:** un octavo criterio —«existe una copia tomada con `respaldo-almacen.sh` **y se la restituyó una vez en seco** sobre un almacén de prueba, con el recuento de trabajos coincidiendo antes y después»— más su tramo en el guion. Un respaldo que nadie restituyó nunca no es un respaldo: es un archivo.
**Costo:** bajo en diff, **pero toca `Roadmap-Producto.md` §5.2, que es capa `00-Contexto`: es una escalada al Product Owner, no un parche de esta mesa.** Se propone; no se aplica sin su sí.

### N-4 · El registro durable de ítems diferidos
**Fundamento medido:** este plan produce **ocho** ítems diferidos con dueño y disparador (tabla de la sección 4). Hoy cada uno quedaría escrito en el documento que a alguien se le ocurra, y la regla (a) de la conciliación de esta misma mesa dice que **un desenlace escrito fuera del circuito de lectura de quien actúa no cuenta como declarado** — es literalmente por qué OP-02 bajó a P3 en vez de a cero. El corpus no tiene registro durable de diferidos: `SDD` §12.3 sigue pendiente y la rama que lo esbozaba se borró.
**Qué es:** una tabla con cinco columnas —id, qué se difirió, por qué, **disparador de reapertura** (medible, no «que moleste»), dueño— que las unidades del plan citan en vez de inventarse cada una su lugar. Y una regla: **un ítem sin disparador medible no se puede diferir, se escala.**
**Costo:** bajo. Entra en U-04 junto con N-2, porque son la misma cosa vista de los dos lados: uno vigila lo que se retiró, el otro vigila lo que se pospuso.

---

## 6 · LA PRIMERA UNIDAD, con detalle para arrancar mañana

### U-01 · El respaldo existe, y el diario es el que la fuente declara

**Rama:** `respaldo-del-almacen`. **Un solo PR.** Todo el ciclo dentro del contenedor de desarrollo; el host no tiene el kit.

**Orden de aplicación, y no es indistinto:**

**(1) `src/GeometriaFactory.Infrastructure/Persistence/StorePreparation.cs`** — primero, porque los dos guiones dependen de que el almacén esté en WAL. Se **inserta** dentro del `try` de `PrepareAsync`, después de `await _dbContext.Database.MigrateAsync(cancellationToken).ConfigureAwait(false);` (línea 37) y antes del `catch`, un bloque que abre la conexión, ejecuta `PRAGMA journal_mode=WAL;`, **lee lo que el motor devuelve** y lanza `InvalidOperationException` si no dice `wal`. Ninguna línea existente se modifica y no hace falta ningún `using` nuevo (`Microsoft.EntityFrameworkCore` ya está en la línea 1).
Tres cosas que el comentario del bloque tiene que decir, porque son las que el próximo lector va a necesitar: que el PRAGMA es **persistente** y por eso va una vez en la preparación y no por conexión; que **se lee el resultado y no se asume**, porque sobre un almacén en memoria o un sistema de archivos sin mapeo compartido SQLite se queda en `delete` **y no falla**; y que el `catch` existente lo envuelve a propósito, porque su criterio —causa y no síntoma— vale igual acá.

**(2) `deploy/Dockerfile`, líneas 50-54.** El bloque de comentario pasa a nombrar **dos** herramientas con su motivo cada una —`curl` para el `healthcheck`, `sqlite3` para que la copia consistente se pueda tomar **desde adentro del contenedor** y no dependa de lo que el host del destino tenga instalado— y el `RUN` pasa a `apt-get install --yes --no-install-recommends curl sqlite3`. Se mantiene el precedente de no anclar versión que `curl` ya sentó; **si se ancla, se anclan los dos juntos**, y eso es otra decisión.

**(3) `scripts/respaldo-almacen.sh`**, nuevo, 0755. Dos formas de uso: local (soursea `store-path.sh` y `gf_resolve_store`, como `run-api.sh` y `reset-db.sh`) y `--desde-contenedor <servicio>`, que es la del destino real y **se corre desde el directorio de `Container.Lab-Geometria`**, sin `--file`, porque este repositorio no tiene la composición de despliegue. Cuatro propiedades no negociables: `VACUUM INTO` y no `cp` —copiar con el proceso escribiendo produce una copia que **casi siempre funciona** y falla el día que hace falta—; **no detiene el servicio**; **se niega** si el destino es el directorio del almacén; y `PRAGMA integrity_check` sobre la copia, **borrándola y saliendo 1 si no da `ok`**. Códigos de salida con la convención de `coverage.sh`: 0 verificada, 1 no se pudo o no verificó, 2 falta con qué.

**(4) `scripts/restaurar-almacen.sh`**, nuevo, 0755. Exige el servicio detenido y **lo comprueba por el archivo** (`-s "$GF_STORE_FILE-wal"`), no por el proceso. Verifica la copia **antes** de instalarla. **No borra el almacén anterior: lo aparta con sello** — restituir es la operación que se hace con miedo, y un guion que además destruye el estado actual convierte un error de elección de archivo en una segunda pérdida, que es exactamente la lección del 2026-08-15.

**(5) La documentación de `09-Devops`**, al final y no al principio, porque hasta acá describía un mecanismo inexistente: `Entornos-Deploy.md` §11.1 (tabla de condiciones con el guion que cumple cada una, **y la ventana de indisponibilidad de la restitución declarada por primera vez**, remitida a `ADR-00007` §6 punto 3 para que no haya que decidir nada nuevo), su §13 fila 3.4, `Guia-Publicacion-Image-Docker.md` §4 cuarta fila y su párrafo de cierre —«**mientras `PD-04` siga abierto, la copia existe si alguien la corrió**»—, y el campo de estado de `PD-04` en `Pipeline-CI-CD.md` §10, que **cambia de contenido y no de dueño**.

**Criterio de verificación — un ciclo completo, y los tres rechazos:**

```
scripts/reset-db.sh --assume-yes && scripts/run-api.sh &      # sembrar una cuenta y un trabajo
. scripts/store-path.sh && gf_resolve_store
sqlite3 "$GF_STORE_FILE" 'PRAGMA journal_mode;'               # → wal      (hoy: delete)
ls "$GF_STORE_FILE"-wal                                       # existe
scripts/respaldo-almacen.sh /tmp/respaldos                    # → 0, imprime ruta y tamaño
sqlite3 "$GF_STORE_FILE" 'SELECT COUNT(*) FROM "Work";'       # → N
# detener el servicio y simular la pérdida:
rm -f "$GF_STORE_FILE" "$GF_STORE_FILE"-wal "$GF_STORE_FILE"-shm
scripts/restaurar-almacen.sh /tmp/respaldos/geometriafactory-*.db   # → 0
sqlite3 "$GF_STORE_FILE" 'SELECT COUNT(*) FROM "Work";'       # → el MISMO N
```

Y la parte que suele no probarse: `scripts/respaldo-almacen.sh "$(dirname "$GF_STORE_FILE")"` sale **1**; `scripts/restaurar-almacen.sh` con el servicio arriba sale **2**; `printf basura > /tmp/rota.db && scripts/restaurar-almacen.sh /tmp/rota.db` sale **1 sin tocar el almacén**. Más un caso de integración que afirme el modo de diario, o el criterio pasa a depender de que alguien repita la secuencia a mano.
Al cierre: `bash scripts/build.sh -c Release` sin advertencias y `bash scripts/test.sh` con las 504 en verde, más la nueva.

**Lo que NO entra en esta unidad, y hay que resistir la tentación:** la frecuencia, el directorio y la retención del respaldo. Son `PD-04` y son del Product Owner. **Un número puesto en el guion se propaga como si fuera del producto.** El mecanismo y la política son cosas distintas, y el volumen y el guion son los mismos antes y después de desplegar.

**Reversión:** se quitan el bloque insertado, los dos guiones y el `sqlite3` del Dockerfile, y se devuelven los cuatro bloques de documentación. **Advertencia obligatoria en el mensaje del PR: revertir el código no revierte el almacén.** Y no se revierte la documentación dejando aplicados los guiones —ni al revés—: el resultado sería un árbol con guiones que ningún documento nombra, que es el defecto simétrico del que se está reparando y más difícil de encontrar.