# A3 — Los 57 pendientes restantes, agrupados por decisión

**Producto:** Fábrica de Geometría
**Documento:** A3-Decisiones-Del-Product-Owner.md
**Versión:** 1.0
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

### D1 · Confirmar los valores `[ASUNCIÓN]` · **~14 filas, una sola decisión**

**Es la más grande de lejos y la más barata.** Catorce filas repartidas en cinco documentos piden lo
mismo: que confirmes la tabla de asunciones numéricas del intake §22 — **latencia, caudal, arranque,
los 500 ms del caso de uso, los 200 ms de la interpretación, el tiempo de la batería, la fluidez de la
interacción**.

**Confirmados, cuatro `QG` pasan a bloqueantes sin ningún otro cambio.** Lo dice el propio enunciado de
`PD-03` de la 09.

### D2 · La unidad de estimación · **5 filas, una decisión**

Qué unidad usa el backlog. Cinco filas la piden en dos proyectos de código.

### D3 · La vigencia exacta del acceso firmado · **3 filas**

El intake la declara **«corta»** sin fijar número, y `ADR-00003` la toma de configuración. **No la
encontré fijada en el árbol.**

### D4 · El límite de tamaño del cuerpo de una petición · **2 filas**

**Verificado: no está fijado en el código.** No hay `MaxRequestBodySize` ni equivalente.

### D5 · El volumen de la comisión · **2 filas** · `[A VERIFICAR]`

Cuántos alumnos. Es un dato tuyo, y alimenta las asunciones de caudal de `D1`.

### D6 · La versión de plataforma que soporta el hosting · **3 filas** · `[A VERIFICAR]`

**No es una decisión: es una medición**, y **la fase `i` la hace sola**. Publicar el front contra el
hosting real la contesta.

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

## 4. Lo que este documento no sabe

- **Los recuentos son aproximados.** El agrupamiento es por enunciado y varias familias se solapan;
  los números llevan `~` donde el solapamiento es real.
- **`D3` puede estar fijada y no la encontré.** Busqué en la composición de la API y no en la
  configuración. **Si aparece, pasa a la lista de §1** y deja de ser tuya.
- **Ninguna se evaluó por vigencia**, igual que en `A1` y en el plan.

## 5. Control de cambios

| Versión | Fecha | Cambios | Autor |
|---|---|---|---|
| 1.0 | 2026-08-20 | Emisión inicial, paso **A3**. Agrupa las **57** filas vencidas restantes por **lo que hay que decidir** en lugar de por fila. **~20 resultan cerrables leyendo** —el motor 3D anclado en `three 0.169.0`, la biblioteca de componentes **decidida por apartamiento** en el `.csproj`, `PBKDF2` en `PasswordDerivation.cs`, el punto de quiebre en `app.css`, `EmailIdentity.Normalize`, los 18 puntos de acceso y las herramientas de cada stage en los guiones— y van a una **segunda pasada de `A2`**. Las restantes se reducen a **ocho decisiones**, con **`D1` absorbiendo catorce filas en una sola**: confirmar la tabla de asunciones numéricas del intake §22, que además habilita cuatro `QG`. Propone el orden **D5 → D1 → D3/D4**, con `D6` resuelta por la propia fase `i` —es medición y no decisión— y declara que **retirar un punto es tan válido como decidirlo**, porque ninguna fila se evaluó por vigencia. | Orquestador SDD |
