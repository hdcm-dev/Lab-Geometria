# Auditoría de la Fase G · categoría 10 Examples de los siete proyectos de código · ronda 2

| Campo | Valor |
| --- | --- |
| Producto | Fábrica de Geometría |
| Rama auditada | `sdd/fase-g-examples` |
| Objeto de la ronda | Dictaminar si se levanta el **RECHAZO** de `G-10-Examples-Siete-Proyectos-r1.md` 1.0 (1 P0, 4 P1, 2 P2, 3 P3), a la vista de la corrección entregada en `2ba229b` |
| Alcance auditado | El árbol `/samples` completo —20 archivos nuevos— contra `Rules-Examples.md` §0.2; los **19** comandos de los contratos contra los **19** métodos de verificación de las matrices, comparados **con programa carácter por carácter**; los 24 documentos que la corrección tocó, por `git diff`; el `PRODUCT-MANIFEST` **1.3** y el `PRODUCT-INTAKE` **1.26**; las 61 filas previas de Web y las 20 previas del Visor, contrastadas contra `5b2f63e`; los diez recuentos, recontados; y forma —tablas, enlaces— verificada con programa sobre los 44 archivos nuevos o modificados |
| Fuentes de contraste | `PRODUCT-INTAKE` **1.26** §16.1, §18, §20; `PRODUCT-MANIFEST` **1.3** §5; `Rules-Examples.md` **4.1** §0.2, §2.1, §2.3, §6 y `Deriva-Rules.md` §2.3 y §6 de `IA.SDD`, **leídas como norma y no modificadas**; el estado del corpus en `03da12f` (pre-corrección) y en `5b2f63e` (pre-Fase G) |
| Método | Herramienta primero. `git ls-files`, `find`, `git diff`, y dos programas propios: uno que extrae y compara los 19 comandos de las tres fuentes, otro que resuelve todos los enlaces relativos y cuenta celdas por fila de tabla |
| Dictamen | **APROBADO**, con **un hallazgo nuevo P3** y **dos observaciones** |

---

## 1. Resumen ejecutivo

El P0 está **resuelto de verdad y no de palabra**. Las diecinueve carpetas existen, están versionadas, tienen el contenido exacto que `Rules-Examples.md` §0.2 asigna a la pasada de diseño, y la afirmación más fuerte de la corrección —que los diecinueve comandos de los contratos y los diecinueve métodos de verificación de las matrices coinciden carácter por carácter— es **verdadera**: la comprobé con programa sobre las tres poblaciones (contratos, matrices y README locales de `/samples`) y el conjunto de diferencias es vacío. Además, ninguna carpeta promete una corrida: las diecinueve declaran, con esas palabras, que el comando previsto **todavía no resuelve**.

Los ocho hallazgos restantes están cerrados sobre el instrumento. Los cuatro puntos abiertos falsos sobre §16.1 se reemplazaron por el desenlace real y citado desde la fuente; la afirmación de las «cinco funciones de §18» se cortó **donde nace** —el `PRODUCT-MANIFEST` §5, hoy 1.3, con marca explícita de corrección— y no queda viva en ningún documento del corpus; §18 y §16.1 quedaron alineados por la vía correcta, que era declarar el alcance de la tabla `S-X` en vez de inflarla; la fila de Web en §16.1 tiene dueño y fecha.

Las **tres correcciones que la corrección hizo sobre el propio informe de r1** son verdaderas en lo sustancial: r1 contó **cinco** documentos con la cita falsa donde había **cuatro** más la fuente, y r1 erró la lista de ubicaciones del P2-2 **en las dos direcciones**, nombrando `Contracts` —que no la tenía— y salteándose `Api` e `Infrastructure` —que sí—. Las tres las verifiqué en el árbol de `03da12f`, no en la declaración.

Las 61 filas de Web y las 20 del Visor están **byte a byte idénticas**; sólo se agregaron las nuevas. Los diez recuentos dan. Cero enlaces rotos sobre 44 archivos.

Lo que queda es **un P3 nuevo**: el residuo de la Definition of Done que r1 registró en tres `06-Backlog-Tecnico/README.md` vive en realidad en los **siete**, y la corrección arregló exactamente los tres que r1 nombró. Es la misma clase de error de lista que la corrección supo detectar en el P2-2 y no aplicó acá. No es motivo de rechazo: no es regresión de la Fase G, no toca ningún dato de la categoría 10 y la corrección de los cuatro restantes es un renglón por archivo.

---

## 2. El P0, verificado con herramienta

### 2.1 Las carpetas existen, son diecinueve, y están versionadas

`git ls-files | grep ^samples` devuelve **20** rutas: `samples/README.md` más un `README.md` en cada una de diecinueve carpetas. `find samples -mindepth 2 -maxdepth 2 -type d | wc -l` devuelve **19**. El reparto es `domain` 3, `contracts` 3, `application` 3, `infrastructure` 3, `api` 3, `visor` 3 y `web` 1 — 3 × 6 + 1 = 19, que es la correspondencia uno a uno con los diecinueve markdown explicativos y las diecinueve sondas.

La estructura es `/samples/<proyecto>/<XX-slug>/`, que **no** es la que `Rules-Examples.md` §2.3 fija. El desvío está declarado en `samples/README.md` §2 y en los siete README de la categoría 10, con su motivo —siete proyectos de código en un repositorio harían colisionar las carpetas base— y con el argumento de que es **carpeta extra y no renombre**, que es lo único que §2.3 admite ajustar. Los slugs son de la lista cerrada de §3.1. **Desvío correctamente declarado, no hallazgo.**

### 2.2 El contenido es el que §0.2 exige

`Rules-Examples.md` §0.2 asigna a la pasada de diseño, además de los markdown, «las carpetas de `/samples` quedan **esqueletadas, con su README local y su comando previsto**». Verifiqué con programa que los diecinueve README locales traen, sin excepción: el bloque `**Comando previsto:**` con el comando en un bloque `bash`; el rótulo `**Estado de esta carpeta:** **Esqueleto — sin código.**`; el enlace al markdown que la gobierna; el identificador del contrato `VER-XX`; el de la sonda `SD-XX`; y las cuatro secciones —objetivo, prerequisitos, cómo correrlo, y «qué hay hoy acá y qué falta»—. Ningún README declara una corrida, una salida ni una fecha.

Y traen, textualmente, lo que r1 exigía como alternativa honesta: «**El comando previsto todavía no resuelve, y esta carpeta no promete lo contrario.**»

### 2.3 Los diecinueve comandos coinciden carácter por carácter — **verificado, y es verdad**

Extraje con programa tres poblaciones y las comparé como cadenas exactas:

1. Los **19** valores del campo `comando:` de los bloques de contrato de verificación, uno por cada `SDD/Docs/Proyectos/*/10-Examples/ejemplo-*.md`.
2. Los **19** comandos de la columna «Método de verificación» de las filas `VER-XX` de las siete `Matriz-Sensado-Deriva.md`, con la clave de correspondencia tomada del enlace `../10-Examples/<archivo>` de la propia fila.
3. Los **19** comandos de los bloques `**Comando previsto:**` de los README locales de `/samples`.

Resultado: los tres conjuntos de claves son idénticos —diferencia simétrica vacía— y el conjunto de pares con texto distinto es **vacío**. No hay ni una diferencia de espacio, de mayúscula ni de barra. La afirmación de la corrección es exacta.

### 2.4 Las rutas resuelven — con una precisión que hay que decir bien

Las **diecinueve rutas de carpeta** de los comandos resuelven: `samples/domain/01-basico`, `samples/web/01-datos-seed` y sus diecisiete hermanas existen y están versionadas. Eso es lo que la corrección afirmó y es cierto.

Los **comandos completos** todavía no resuelven, porque falta lo que la pasada de ejecución produce: los `run.sh` de Api y Web, el `package.json` con el script `verify` del Visor, los proyectos `.csproj`, y los `scripts/*.sh` que las precondiciones invocan. Esto **no es un hallazgo**, por dos razones que verifiqué:

- `Deriva-Rules.md` §6 exige que «ninguna **evidencia citada** apunta a una ruta, identificador o comando que no resuelve», y §57 lo califica como P0 cuando «una afirmación **con evidencia** que no resuelve». Los diecinueve contratos declaran `evidencia: No verificado — sin código`, sin fecha y sin salida: **no citan evidencia**, de modo que la cláusula no se activa. El P0 de r1 no era éste; era que los README **afirmaban haber creado carpetas que no existían**.
- Los veinte documentos nuevos y los siete README de la categoría declaran expresamente que el comando no resuelve todavía y por qué. Un punto abierto correctamente declarado no es hallazgo.

**P0-1: CERRADO.**

---

## 3. Estado de los nueve hallazgos de r1

| # | Hallazgo de r1 | Estado | Cómo lo comprobé |
| --- | --- | --- | --- |
| **P0-1** | Las carpetas de `/samples` no existen y los siete README afirman haberlas dejado esqueletadas | **CERRADO** | `git ls-files ^samples` → 20 rutas; `find -mindepth 2 -maxdepth 2 -type d` → **19**. Programa que verifica en los 19 README locales el bloque `**Comando previsto:**`, el rótulo `Esqueleto — sin código`, el `VER-XX`, el `SD-XX` y las cuatro secciones: **0 faltantes**. Programa que compara los 19 comandos de contratos, matrices y README locales: **diferencia simétrica de claves vacía y cero diferencias de texto**. Los siete README de la categoría 10 pasaron a decir «**Las tres carpetas existen**» con enlace resoluble a cada una |
| **P1-1** | Cuatro README declaran abierta la consolidación de §16.1 que su propio commit cerró | **CERRADO** | `git diff 03da12f 2ba229b` sobre los cuatro README: la frase «lo que queda abierto: la consolidación de §16.1…» se reemplaza por «**El punto que quedaba abierto está cerrado, y se conserva con su desenlace**», con la fila vigente de §16.1 **citada en bloque desde la fuente** y la declaración de que la fuente vinculante es §16.1 y no el README. Abrí §16.1 del intake 1.26 (línea 595): cinco filas que cubren los **siete** proyectos, las tres nuevas rotuladas `[AMPLIADO 2026-08-11]` |
| **P1-2** | Cinco documentos citan como vivo un residuo de §18 que §18 no tiene | **CERRADO, y r1 contó mal** | `git grep` de «residuo de §18», «nombrando cinco» y «residuo de la fuente anterior» sobre `03da12f`: **cinco aciertos, de los cuales cuatro son documentos que copian** (Domain, Contracts, Application, Visor) **y el quinto es el `PRODUCT-MANIFEST` §5, que es la fuente**. La corrección tiene razón: eran **cuatro**. El mismo `git grep` sobre `HEAD` fuera de `/Audit/` y de `_legacy/`: **cero aciertos vivos**; el único acierto es la línea corregida del manifiesto, que ahora dice lo contrario y lleva la marca `[CORREGIDO 2026-08-11]` |
| **P1-3** | §18 desalineado con §16.1 | **CERRADO por la vía correcta** | Abrí §18 vigente (línea 1114). Sigue con **tres** filas `S-X` —lo correcto, porque son demostraciones nombradas y no el conjunto de carpetas— y agrega el párrafo «**Las tres muestras `S-1`, `S-2` y `S-3` no son el conjunto de las carpetas de `/samples`** [PRECISADO 2026-08-11]», que es exactamente el remedio que r1 pedía como alternativa a inflar la tabla. La frase «no hay sample de flujo de usuario final» sigue siendo verdadera: el sample de Web es un seed que corre por comando, no un flujo de pantalla |
| **P1-4** | §16.1 dice que Web «no produce sample propio» y Web produjo uno | **CERRADO** | Abrí la fila de `GeometriaFactory-Web` en §16.1: «**`/samples/web/`, con un solo sample** [AMPLIADO 2026-08-11]», con la distinción entre el guion que ejecuta una persona y la muestra que se corre sola, y el motivo de que lleve uno y no tres. §5 del README de Web ya no dice «nada que elevar»: dice que esa conclusión **estaba mal fundada**, que se elevó y se cerró, y que la fuente vinculante es §16.1. La contradicción tiene dueño y fecha |
| **P2-1** | Cita truncada de `Rules-Examples.md` §6 que suprime dónde debe vivir la justificación | **CERRADO, y bien resuelto** | Ver §6 de este informe. La cita se restituye completa —«…o la ausencia está justificada **en `Decisiones-Proyecto.md`**»—, `find -name "Decisiones-Proyecto.md"` sigue devolviendo vacío, y el README lo declara y eleva a orquestación |
| **P2-2** | Cita rotulada «literalmente» con elisión no marcada de `Deriva-Rules.md` §2.3 | **CERRADO, y r1 erró la lista en las dos direcciones** | `git grep "la matriz se emite igual"` sobre `03da12f`: la elisión estaba en las matrices de **Api, Application, Domain e Infrastructure** y en los `08/README.md` de **Application, Domain e Infrastructure**. r1 nombró `Contracts` —que **no** la tenía— y se salteó `Api` e `Infrastructure` —que **sí**—. La corrección tiene razón. Y corrigió el conjunto **real**: `git diff` muestra la cita restituida completa en las cuatro matrices y la elisión marcada en los tres `08/README.md`, y **no** tocó `Contracts`, que no tenía nada que corregir. Comparé el texto restituido carácter por carácter contra `Deriva-Rules.md` §2.3: coincide |
| **P3-1** | Convención de extensión rota en `Domain/ejemplo-02-intermedio.md` | **CERRADO** | `git diff` de ese archivo: las líneas 45–46 pasan de `E1.json … E8.json` a `E1.txt … E8.txt`, y se agrega el párrafo que **funda** la convención `.txt` —`E-2` no es JSON estrictamente válido y una herramienta lo reformatearía—, incluyendo la precisión de que `E-2` no está entre los seis de este sample. `grep -rn "\.json"` sobre `Domain/10-Examples/` ya no devuelve escenarios. El fundamento se replicó además en `samples/README.md` §5 |
| **P3-2** | La prosa de `SD-62` llama «sin gradación» a `SD-10`, que no está en esa lista | **CERRADO** | `git diff` de la matriz de Web: el párrafo pasa a decir que `SD-36` **sí** está entre las filas sin gradación de §5 y que `SD-10` **no** está, pero que declara «Mayor» para ese supuesto y por lo tanto tampoco admite grado. La lista de §5 **no se tocó** —lo verifiqué en el diff de las 61 filas— y el adjetivo sobrante desapareció |
| **P3-3** | Residuo de la DoD en los `06-Backlog-Tecnico/README.md` de tres proyectos | **CERRADO PARCIALMENTE → hallazgo nuevo `N-1`** | Ver §5. Corregido en los tres que r1 nombró; **vive todavía en los otros cuatro** |

---

## 4. Regresiones: qué busqué y qué encontré

### 4.1 Las 61 filas de Web y las 12 del Visor

`diff` entre las filas `| \`SD-` de cada matriz en `5b2f63e` —el merge previo a toda la Fase G— y en `HEAD`:

- **Web**: 61 filas antes, 62 ahora. El `diff` es exactamente `61a62`, una única inserción: `SD-62`. **Las 61 anteriores son byte a byte idénticas.**
- **Visor**: 20 líneas `SD-` antes, 23 ahora. El `diff` es exactamente `12a13,15`: se insertan `SD-13`, `SD-14` y `SD-15` después de la duodécima. **Las 12 filas de la matriz y las 8 líneas `SD-` de las listas de §5 son byte a byte idénticas.**

Ninguna fila previa cambió, ni en la ronda 1 ni en la corrección.

### 4.2 Recuentos, identificadores, citas, tablas y enlaces

- **Enlaces.** Programa que resuelve todo enlace relativo de los 44 archivos nuevos o modificados: **0 rotos**, incluidos los `../../../../../samples/...` de los siete README de la categoría —cinco niveles arriba, que es el punto donde este tipo de corrección suele romperse— y los `../../../SDD/Docs/...` de los diecinueve README locales.
- **Tablas.** Programa que cuenta celdas por fila en los mismos archivos: **cero filas discordantes**. Las dos alertas iniciales del programa —matrices de Visor y Web— resultaron ser **falsos positivos míos**: las filas antiguas llevan pipes escapados (`\|`) dentro de un `code span`, que mi contador sumaba. Verificado a mano; son doce filas de ocho celdas, bien formadas.
- **Identificadores.** Los `VER-01` a `VER-03`, los `SD-XX`, los `CU-XX` y los `E-X` citados en los archivos nuevos existen todos. Los enlaces a `Definition-Of-Done.md` que la corrección agregó a tres `06-Backlog-Tecnico/README.md` resuelven.
- **Citas nuevas.** Las tres citas en bloque que la corrección agregó —la fila de §16.1 de Web, la de Domain/Contracts y la de Application/Infrastructure— las contrasté contra el texto vivo del intake: coinciden, y las elisiones van marcadas con `[…]`. La cita de §18 en el README del Visor y en el manifiesto 1.3 coincide con §18 línea a línea.
- **Un recuento nuevo que la corrección detectó y arregló, y que es correcto arreglar.** El intake **1.26** corrige a su propia **1.25**: §18 decía que §16.1 «asigna carpeta a **seis** de los siete proyectos». Conté las filas de §16.1: son cinco filas que cubren `Api`, `Visor`, `Web`, `Domain`+`Contracts` y `Application`+`Infrastructure` = **siete**. La 1.25 contó mal y la 1.26 tiene razón. Está bien declarado, incluida la observación de que el defecto reapareció en el párrafo escrito para declararlo.
- **Contradicciones internas que la corrección declara haber encontrado.** Verifiqué la más citable: `Visor/10-Examples/ejemplo-02-intermedio.md` atribuía a `ADR-02` —titulada `ADR-02-Superficie-De-Seis-Funciones-Planas.md`— una superficie de cinco funciones. La fila ahora dice «Las **cinco** funciones que este sample invoca —de las **seis** que ADR-02 declara— … La sexta, `establecerMovimiento`, la ejerce el ejemplo 03». Correcto.

**No encontré ninguna regresión.**

---

## 5. Hallazgo nuevo

### N-1 (P3) · El residuo de la Definition of Done sigue vivo en los cuatro `06-Backlog-Tecnico/README.md` que r1 no nombró

**Dónde.** `GeometriaFactory-{Api,Infrastructure,Visor,Web}/06-Backlog-Tecnico/README.md`, sección «Definition of Ready vigente».

**Qué dice.** Los cuatro: «La Definition of Done vive en `08-Calidad-Y-Pruebas`, que **todavía no está emitida**». El de Web agrega la variante más incoherente: «…todavía no está emitida; lo que sí está emitido de esa categoría es `Matriz-Sensado-Deriva.md`», es decir, nombra un artefacto de `08` en la misma frase en que declara que `08` no existe.

**Qué debería decir.** Lo que dicen hoy los tres corregidos: que la DoD vive en `../08-Calidad-Y-Pruebas/Definition-Of-Done.md`, **emitida desde la Fase E**.

**Cómo lo comprobé.** `git grep -l "todavía no está emitida"` sobre `03da12f` acotado a `*/06-Backlog-Tecnico/README.md` devuelve los **siete** proyectos, no tres. El mismo `grep` sobre `HEAD` devuelve **cuatro** ocurrencias vivas —Api, Infrastructure, Visor, Web— más las tres menciones históricas dentro de los controles de cambios de los corregidos. Y `ls SDD/Docs/Proyectos/GeometriaFactory-{Api,Infrastructure,Visor,Web}/08-Calidad-Y-Pruebas/Definition-Of-Done.md` devuelve los cuatro archivos: **la afirmación es falsa en los cuatro**.

**Por qué es P3 y no más.** No es regresión de la Fase G —el residuo es anterior a `f8d75f3`, igual que en los tres corregidos—, no toca ningún dato, contrato, sonda ni recuento de la categoría 10, y no afecta a ninguna de las diecinueve carpetas. Es una afirmación falsa que un lector que llegue por `06` va a creer.

**Por qué se registra igual.** Porque la corrección demostró, en el P2-2, que sabe verificar una lista de ubicaciones en las dos direcciones y corregir el conjunto real y no el nombrado. Aplicó ese método al P2-2 y no al P3-3, y la lista del P3-3 estaba mal por el mismo motivo: incompleta. Es una hora de trabajo menor: un renglón por archivo.

---

## 6. El punto declarado sin resolver: `Decisiones-Proyecto.md`

**El dictamen es que está bien resuelto**, y que la decisión de restituir la cita en vez de recortarla es la correcta.

Lo que verifiqué. `grep` sobre `Rules-Examples.md` §6 confirma el texto completo del criterio, con el destino `en Decisiones-Proyecto.md`. `find . -name "Decisiones-Proyecto.md"` sobre todo el corpus sigue devolviendo **vacío**: el artefacto no existe en ninguno de los siete proyectos de código. El README de Web §5 ahora cita el criterio **completo**, dice con todas las letras que «**el destino que la regla fija no existe en este producto**», declara dónde vive mientras tanto la justificación —en el README y en §16.1 del intake, que es donde el intake ya la había escrito— y cierra elevando la decisión: «resolver si este corpus debe emitir `Decisiones-Proyecto.md` o si la regla no aplica a un producto de siete proyectos de código en un repositorio es **de la orquestación y no de esta categoría**».

Por qué es la resolución correcta y no una evasiva. Tres razones. Primera: el defecto que r1 objetó era **de la cita**, no del fondo, y la cita se restituyó entera, incluido el fragmento que volvía exigente a la regla. Segunda: elevar era lo único que la categoría 10 podía hacer sin invadir competencia ajena — decidir si un producto de siete proyectos de código en un repositorio emite ese artefacto es una decisión de estructura del corpus, que ninguna guía de categoría toma. Tercera, y es la que lo separa de un punto abierto falso: el punto abierto que declara **es verdadero y verificable con un comando**, y el propio documento dice cuál es el comando —que el artefacto no existe en ninguna parte del corpus—.

Qué falta, y no es de esta fase. La orquestación debe tomar la decisión. Mientras no la tome, la ausencia de cobertura de siete casos de uso de Web queda justificada en un lugar que la regla no prevé, aunque esté declarado. Lo registro como **punto abierto correctamente declarado**, que por el criterio negativo de esta ronda **no es hallazgo**.

---

## 7. Recuentos, recontados

| Recuento | Declarado | Contado | Cómo |
| --- | --- | --- | --- |
| Reglas de negocio | 16 | **16** | `grep -o 'RN-[0-9]\+' \| sort -u` sobre el intake: `RN-01` a `RN-16`, sin huecos |
| Invariantes | 9 | **9** | Ídem con `INV-`: `INV-01` a `INV-09` |
| Escenarios reales | 8 | **8** | Ídem con `E-[1-9]`: `E-1` a `E-8` |
| Casos de la batería obligatoria | 10 | **10** | Los diez primeros de `Infrastructure/08/Casos-Prueba-Referenciales.md`, declarados como batería en el README de la categoría 10 de ese proyecto y en `PRODUCT-INTAKE` §21 |
| Códigos de contrato vivos / emitidos | 15 / 18 | **15 / 18** | `Contracts/03-UX-UI-DX/DX-Error-Messages.md` §3.2, la única tabla donde los dieciocho están juntos: quince entradas más tres filas de retiro —`DXT-09`, `DXT-13`, `DXT-18`—, ninguno reciclado |
| Puntos de acceso | 15 | **15** | `Api/02/Definicion-Superficie-HTTP.md`, citado en tres lugares de la categoría 10 de Api con el mismo número |
| Funciones de la fachada | 6 | **6** | Intake §17.7 P.3 y §18, línea 492: `inicializar`, `cargarJson`, `seleccionarPieza`, `redimensionar`, `destruir`, `establecerMovimiento`. Manifiesto 1.3 §5 y `Visor/02/Definicion-Contrato-De-Fachada.md` §4 coinciden |
| Sondas `VER-XX` | 19 | **19** | `grep -c` de filas `SD-` que citan `VER-` en las siete matrices: 3+3+3+3+3+3+1 |
| Casos de uso · Domain | 13 | **13** | `find */02-Especificacion-Funcional -name "CU-*.md" -not -path "*_legacy*"` |
| · Contracts | 8 | **8** | Ídem |
| · Visor | 7 | **7** | Ídem |
| · Application | 11 | **11** | Ídem |
| · Web | 10 | **10** | Ídem |
| · Infrastructure | 10 | **10** | Ídem |
| · Api | 12 | **12** | Ídem |
| **Total casos de uso** | 71 | **71** | Suma de los siete |

**Los quince recuentos dan.** Ninguno dejó de cerrar por efecto de la corrección.

---

## 8. Observaciones que no son hallazgos

**O-1 · «Dos hallazgos» donde el error de lista fue de uno.** El mensaje de commit dice que las ubicaciones **de dos hallazgos** estaban mal en las dos direcciones. Lo que verifiqué es que el error en las dos direcciones —un proyecto nombrado que no lo tenía, dos salteados que sí— es exacto y **cabe entero en el P2-2**, cuya lista de r1 mezclaba matrices y `08/README.md`. La lista del P3-3 también estaba mal, pero **en una sola dirección** —incompleta por cuatro—, y no se corrigió: ése es el `N-1`. La imprecisión vive en el mensaje de commit y no en el corpus, de modo que no la registro como hallazgo; la anoto porque afecta a la trazabilidad de qué se revisó.

**O-2 · La corrección atribuye a la versión 1.11 del intake el arreglo del residuo de §18, y r1 lo atribuía a la 1.6.** No pude dirimirlo sin abrir los archivados intermedios, y no cambia nada: lo relevante —que §18 vigente enumera las seis— está verificado en la fuente. **No verificado.**

**O-3 · Polisemia de «cinco», descartada.** `grep` de «cinco» sobre los siete README de la categoría 10 devuelve, además de las menciones históricas al residuo corregido, «cinco componentes» de Domain, «cinco pasos o menos» de Api, Visor, Infrastructure y Web, y «cinco funciones que este sample invoca de las seis» del Visor. Contextos disjuntos. **No es hallazgo**, por el criterio negativo de esta ronda.

---

## 9. Lo que no pude verificar

- **La corrección aritmética de los criterios de aceptación que involucran recuentos de datos seed.** Sigue sin fuente contra la cual contrastar los estados `Aprobado` y `Rechazado` de `VER-01` de Web, que los produce el propio seed por decisión suya. La corrección no cambió ese criterio y r1 ya lo había declarado. **No verificado**, igual que en r1.
- **Si los `US-XX` citados en los bloques `verifica` corresponden en contenido a lo que cada sample ejercita.** Habría exigido abrir 176 historias. La corrección no tocó esos bloques. **No verificado en contenido**, igual que en r1.
- **En qué versión del intake se corrigió el residuo de §18.** Ver O-2. **No verificado.**

---

## 10. Dictamen

**APROBADO.**

El P0 no se cerró con una redacción más prudente: se cerró **produciendo lo que faltaba**. Las diecinueve carpetas existen, están versionadas, y su contenido es exactamente el que la regla asigna a la pasada de diseño. La afirmación fuerte que la corrección se atrevió a hacer —diecinueve comandos idénticos carácter por carácter entre contratos y matrices— la comprobé con programa sobre tres poblaciones y es **verdadera sin una sola excepción**, lo que en un corpus de este tamaño es un resultado poco común. Y la honestidad quedó del lado correcto: cada una de las diecinueve carpetas declara que su comando **todavía no resuelve**, en vez de dejar que el lector lo descubra.

Los cuatro P1 comparten raíz y la corrección fue a la raíz. La afirmación sobre las cinco funciones se cortó en el `PRODUCT-MANIFEST` §5, que es donde nacía, con una marca explícita para que la próxima fase no la vuelva a copiar; verifiqué con `git grep` que no queda viva en ningún documento del corpus. Los cuatro puntos abiertos falsos se reemplazaron por su desenlace, citado en bloque desde la fuente y no a través de otro artefacto — que era exactamente el hábito que produjo el defecto. §18 se alineó con §16.1 declarando el alcance de su tabla, que es la solución correcta y no la fácil. La fila de Web tiene dueño y fecha.

Y hay algo que conviene decir porque es infrecuente: **la corrección auditó al auditor y tuvo razón**. Verifiqué sus tres correcciones sobre el informe de r1 contra el árbol de `03da12f`, y las tres son ciertas. Eran cuatro documentos y no cinco. La lista de ubicaciones del P2-2 estaba mal en las dos direcciones, y la corrección arregló el conjunto **real** —cuatro matrices y tres README— y no el nombrado. Un ciclo de auditoría en el que el corregido encuentra los errores del informe y los prueba con herramienta es el ciclo funcionando.

Lo que impide la aprobación sin reserva no existe: el único hallazgo nuevo es un P3 preexistente, no es regresión de la Fase G, no toca la categoría 10 y se cierra con un renglón por archivo. Lo dejo registrado para la fase que toque esos `06`, con la misma constancia con que r1 lo dejó — y con la observación de que el método que la corrección aplicó bien al P2-2 es el que faltaba aplicar acá.

**Qué queda anotado para después de esta fase.** Corregir el residuo de la DoD en los `06-Backlog-Tecnico/README.md` de Api, Infrastructure, Visor y Web (`N-1`, P3). Y resolver, en la orquestación y no en una categoría, si este corpus emite `Decisiones-Proyecto.md` o si la regla de `Rules-Examples.md` §6 no aplica a un producto de siete proyectos de código en un repositorio.

---

## 11. ¿Alcanzan estos ejemplos para que alguien construya el producto a partir de la especificación?

Sí, y ahora sin la reserva que r1 tuvo que poner. Diecinueve contratos con criterio de aceptación evaluable por una máquina, declarados antes de que exista una línea de código, cubren los setenta y un casos de uso de los siete proyectos —trece, ocho, siete, once, diez, diez y doce— con la única ausencia justificada de siete casos de uso de Web, y transportan los ocho escenarios reales del intake sin sustituirlos por datos sintéticos. Doce de los diecinueve declaran aserciones negativas, y ahí está lo que hace útil a esta categoría: no verifican que el camino feliz funcione, verifican que el modo de falla más probable no pase inadvertido — que el índice reportado sea 1 y no 0, que el cilindro de `E-1` cuya diferencia es exactamente 0.01 no produzca observación porque el operador es estricto, que la negativa por pertenencia no salga como negativa por facultad, que un solo borrador visible en el listado de la comisión sea falla y no diferencia de grado, que una contraseña provisoria no se derive de un dato de la cuenta.

Lo que cambió en esta ronda es que ahora **hay dónde ponerlos**. Cada uno de los diecinueve criterios tiene una carpeta con nombre, un comando escrito, un README que dice qué va a vivir ahí y un enlace de ida y vuelta al markdown que declara el árbol de archivos y la salida exacta esperada. Un equipo —o un agente— que abra `samples/domain/01-basico/` encuentra, sin salir de la carpeta, el objetivo del sample, sus prerequisitos, el comando, el contrato que lo sensa, la sonda que lo vigila y la advertencia de que todavía no corre. Eso es un andamio, no una promesa: la pasada de ejecución tiene que escribir código dentro de una estructura que ya está decidida y verificada, en vez de decidirla mientras codifica.

Falta el código, y falta con todas las letras. Los diecinueve comandos no resuelven todavía, y los `scripts/*.sh` que las precondiciones invocan tampoco existen. Pero eso es lo que la pasada de ejecución produce, está declarado como tal en los veinte archivos nuevos, y ninguno finge lo contrario. La diferencia entre esta ronda y la anterior es exactamente ésa, y es la diferencia que esta categoría existe para instalar: en r1 los diecinueve criterios eran excelentes especificaciones de prueba apuntando a la nada; hoy son excelentes especificaciones de prueba apuntando a diecinueve carpetas que existen y que dicen la verdad sobre lo que todavía no tienen.

---

## 12. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Ronda 2 de la auditoría de la Fase G, sobre la corrección `2ba229b`. **APROBADO**: se levanta el rechazo. El **P0-1** está cerrado produciendo lo que faltaba —diecinueve carpetas versionadas, con el contenido exacto de `Rules-Examples.md` §0.2, y la afirmación de coincidencia carácter por carácter de los diecinueve comandos entre contratos, matrices y README locales **verificada con programa y verdadera sin excepción**—. Los cuatro **P1**, los dos **P2** y dos de los tres **P3** están cerrados y verificados sobre el instrumento. Se verifican como **ciertas** las tres correcciones que la corrección hizo sobre el informe de r1: eran cuatro documentos con la cita falsa y no cinco, y la lista de ubicaciones del P2-2 estaba mal en las dos direcciones. Se confirma que la cadena se cortó donde nace —`PRODUCT-MANIFEST` §5, hoy 1.3— y que ningún documento vivo del corpus copia la afirmación. **Cero regresiones**: las 61 filas de Web y las 20 del Visor byte a byte idénticas, cero enlaces rotos y cero filas de tabla discordantes sobre 44 archivos, y los quince recuentos dan. Se levanta **un hallazgo nuevo P3** (`N-1`): el residuo de la Definition of Done vive en los siete `06-Backlog-Tecnico/README.md` y sólo se corrigieron los tres que r1 había nombrado. Se dictamina **bien resuelto** el punto abierto de `Decisiones-Proyecto.md`. Se declaran **tres** puntos no verificados y **tres** observaciones. |
