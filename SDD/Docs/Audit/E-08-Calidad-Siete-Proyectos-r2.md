# Auditoría de la Fase E · categoría 08 Calidad y Pruebas de los siete proyectos de código · ronda 2

| Campo | Valor |
| --- | --- |
| Producto | Fábrica de Geometría |
| Rama auditada | `sdd/fase-e-calidad` |
| Objeto de la ronda | Dictaminar si se levanta el **RECHAZO** de `E-08-Calidad-Siete-Proyectos-r1.md` 1.0, contra la corrección del commit `8d5be75` |
| Alcance auditado | Los **46** archivos que `8d5be75` toca —**45** documentos de las siete carpetas `08-Calidad-Y-Pruebas/` más el intake—, los **nueve** hallazgos de r1 uno por uno, los **77** quality gates de los siete proyectos de código, los **208** casos de prueba y los **219** criterios en las dos direcciones, y la tabla de control de cambios del intake |
| Fuentes de contraste | `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.20**, en especial §15, §17.x.P.6, P.8 y P.10, §20, §21 y §22; las categorías **02**, **05** y **06** de cada proyecto de código; `Matriz-Sensado-Deriva.md` **1.2** de Web y **1.0** del Visor; `Rules-Calidad-Y-Pruebas.md` §2.1 y §2.2 |
| Criterio de la ronda | **El instrumento, no la conclusión.** Ninguna afirmación de la corrección se aceptó por estar escrita: los tres recuentos que la corrección declaró subcontados se recontaron sobre el texto de `1d3bbeb` con `git show`; la regla de los gates se aplicó a los **77**, no a una muestra; los mapeos inversos `TC → matriz` y `CU → matriz` se reconstruyeron de nuevo con herramienta; y forma —celdas, enlaces, anclas, versiones— se verificó con programa sobre los 46 archivos |
| Fuera de alcance | `_legacy/`; las tres fuentes originales bajo `PROMPTs/` de otro repositorio; las categorías 04 y 09 a 11, no emitidas; `Matriz-Sensado-Deriva.md` de Web como artefacto |
| Auditor | Auditor independiente, sin participación en la emisión ni en la corrección |
| Fecha | 2026-08-11 |

---

## Tabla de contenido

- [1. Resumen ejecutivo](#1-resumen-ejecutivo)
- [2. Los nueve hallazgos de r1, uno por uno](#2-los-nueve-hallazgos-de-r1-uno-por-uno)
- [3. Los tres recuentos que la corrección declaró subcontados](#3-los-tres-recuentos-que-la-correccion-declaro-subcontados)
- [4. `H-02`: la regla del carácter de los gates, verificada en los 77](#4-h-02-la-regla-del-caracter-de-los-gates-verificada-en-los-77)
- [5. Las dos cosas que la corrección decidió no hacer](#5-las-dos-cosas-que-la-correccion-decidio-no-hacer)
- [6. Regresiones](#6-regresiones)
- [7. La tabla de control de cambios del intake](#7-la-tabla-de-control-de-cambios-del-intake)
- [8. Hallazgos nuevos](#8-hallazgos-nuevos)
- [9. Lo que no pude verificar](#9-lo-que-no-pude-verificar)
- [10. Dictamen](#10-dictamen)
- [11. ¿Alcanza esta estrategia para confiar en el producto?](#11-alcanza-esta-estrategia-para-confiar-en-el-producto)
- [12. Control de cambios](#12-control-de-cambios)

---

## 1. Resumen ejecutivo

**Los nueve hallazgos están cerrados y los verifiqué en el texto, no en la declaración de que se corrigieron.** Los dos P1 —`H-01`, las citas al texto muerto del intake; `H-02`, los dos gates condicionados contra el carácter que la fuente puso a salvo— están resueltos de raíz y propagados: `H-02` no se arregló cambiando una palabra en dos celdas sino recorriendo, en Contracts y en Web, el gate, el criterio de validación, la DoD, el plan, la matriz, el caso de prueba y el README, con la forma —y sólo la forma— declarada sujeta a confirmación.

**Los tres recuentos que la corrección dijo subcontados están subcontados de verdad, y lo comprobé sobre el texto anterior.** `H-01`: r1 omitió de su tabla **el documento entero** donde el defecto era más visible —`GeometriaFactory-Infrastructure/Estrategia-Calidad.md`, cuya §3.2 se titulaba «*La batería del validador tiene diez casos y el intake escribe nueve en dos lugares*», con entrada en la tabla de contenido, dos viñetas y un párrafo de cierre que derivaba la remediación al Product Owner— y también `Estrategia-Testing.md` de ese proyecto. Por documento son **once**, no nueve ni diez. `H-05`: el «nueve» de §21 estaba en **tres** documentos —Visor, Domain y Contracts—, no en uno; lo leí en `1d3bbeb` en los tres. `H-03`: no era una frase suelta sino un **cruce sistemático**: la tabla §3.2 de `Estrategia-Calidad.md` de Web tenía las definiciones de `PT-02` y `PT-03` **intercambiadas entre sí**, y `TC-20` declaraba cubrir `PT-03` cuando lo que verifica es la sincronización por índice que el intake §17.7.P.8 pone en `PT-02`. **Los tres son casos de subconteo de r1, no de inflación de la corrección.**

**La regla del carácter de los gates la verifiqué en los 77, no en una muestra.** Los siete proyectos de código suman 8 + 9 + 9 + 11 + 11 + 14 + 15 = **77** gates. **Quince** llevan un valor rotulado `[ASUNCIÓN]`; los otros **62** no llevan ninguno y **los 62 bloquean**. De los quince, tres son bloqueantes —Contracts `QG-05` y Web `QG-04`, que son la forma de `A-4`, e Infrastructure `QG-07`, que cita §22 para decir que la tolerancia de 0.01 no es asunción— y doce quedan condicionados. **Once de esos doce son asunciones sobre el umbral mismo** y se apoyan en `A-3` o `A-5` de §22, cuyas columnas dicen «cambia el gate del pipeline» y «cambia lo que la categoría 08 verifique». **No condicionó de más ni de menos.**

**Las dos decisiones de no hacer están bien tomadas.** No subir el piso de `GeometriaFactory-Api` es correcto: subirlo de 75 a 80 habría sido contradecir al intake por cuenta de la categoría 08, y una ADR no es un artefacto que la 08 pueda emitir. Lo que r1 pidió —declarar el apartamiento— está hecho con precisión, y el punto abierto que queda es **verdadero** y con dueño nombrado. No reescribir las filas 1.0 de control de cambios también es correcto, y es la misma disciplina que r1 recomendaba: una fila fechada dice qué declaró esa versión, y reescribirla borraría la evidencia de que el defecto existió.

**Y hay una regresión chica, una sola.** `GeometriaFactory-Infrastructure/Plan-Pruebas.md` sigue citando `Estrategia-Testing.md` **1.0** en su trazabilidad upstream cuando ese documento pasó a **1.1** en el mismo commit —la corrección actualizó exactamente esa cita en Api, Domain, Contracts, Visor y Web, y se salteó Infrastructure—. Es P3 y no cambia nada de lo verificado. Los recuentos siguen cerrando: **208** casos, **219** criterios, **71** casos de uso, **10** de la batería, **61** sondas de Web, **cero** identificadores fantasma, **cero** enlaces rotos y **cero** filas de tabla desparejas.

---

## 2. Los nueve hallazgos de r1, uno por uno

| # | Sev. r1 | Estado verificado | Cómo lo comprobé |
| --- | --- | --- | --- |
| `H-01` | P1 | **Cerrado**, y el defecto era **mayor** que el declarado en r1 | `grep -rn "nueve"` sobre las siete carpetas en `HEAD`: **ninguna** ocurrencia afirma ya en presente que el intake diga nueve. Las once quedaron en pasado con versión —«**hasta 1.19** el intake escribía…»— o en fila de control de cambios. Los **dos huecos falsos** están tachados con `~~…~~ **CERRADO**` y su desenlace, conservando la fila para no dejar hueco de numeración (`Api/Matriz` §8, `Infrastructure/Matriz` §8). Abrí el intake vivo: líneas 728 (§17.2.P.11), 783 (§17.3.P.6), 790 (§17.3.P.8), 923 (§17.5.P.8) dicen **diez** |
| `H-02` | P1 | **Cerrado**, con propagación completa | Extraje la columna de carácter de los **77** gates con `awk`. Contracts `QG-05`: «**Bloquea la fusión, y no es condicionado.** Lo sujeto a confirmación es **la forma**». Web `QG-04`: «**Bloquea el punto de control, y no es condicionado**». Seguí la propagación documento por documento: Contracts `CV-08`, DoD (tres puntos), matriz §3 y §8, `TC-21` y README; Web `CV-13`, DoD §84, `Plan-Pruebas` §3, matriz §3 y §8, y README §5. No quedó ni un lugar declarándolos condicionados |
| `H-03` | P2 | **Cerrado**, y era **cuatro veces** el defecto que r1 describió | Ver §3.3. El §8.2 de Web dice ahora «la **otra puerta** de la correspondencia, **`PT-03`** … que es como el intake §17.7.P.8 define `PT-03`»; la tabla §3.2 de `Estrategia-Calidad.md` tiene hoy las dos definiciones **en su lugar**, comparadas carácter por carácter contra §17.7.P.8 del intake; `TC-20` cubre `PT-02` con la razón explícita; y la fila de recuento del catálogo dice «**`PT-03` no tiene caso propio acá**» |
| `H-04` | P2 | **Cerrado** | Reconstruí el mapeo inverso con `comm` en las dos direcciones sobre los siete pares catálogo/matriz: **0 citados no definidos y 0 definidos sin fila**, contra los 5 de r1. Los cuatro proyectos afectados agregaron una **§2.1** que enumera la prueba con qué verifica, a qué traza según su campo «Cubre» y su estado, y las dos frases falsas de Domain y Application se reemplazaron por el recuento verdadero —**25 de 27** y **30 de 31**—. `TC-20` del Visor, el caso más sensible, tiene además la remisión a `CV-20` a `CV-23` de su §4, que abrí y existen |
| `H-05` | P2 | **Cerrado**, y estaba en **tres** documentos | Ver §3.2. Los tres §6 dicen hoy «**diez**», los tres con la aposición «los nueve de la fuente técnica más el décimo que esa misma sección agregó el 2026-08-09» |
| `H-06` | P2 | **Cerrado en lo que r1 pidió**; el apartamiento queda declarado, no resuelto | `Api/Estrategia-Testing.md` §2 tiene ahora tres párrafos nuevos: **«Este piso baja respecto del de la guía, y hay que decirlo»** con la cita literal de §2.2 y el «75 < 80»; **«Con qué autoridad baja»**, que dice que **«la autoridad de la fuente no reemplaza a la ADR que §2.2 exige»**; y **«Qué compensa la caída, que no es un argumento para no declararla»**. La fila de la tabla de pisos también lo dice. Y hay un hueco nuevo en la matriz §8 con dueño: la categoría 05. Dictamen en §5.1 |
| `H-07` | P3 | **Cerrado**, en dos tiempos | La mitad de la sección —`§17.3.P.4` → **`§17.2.P.11`**— se corrigió en `7a10ab9`; lo verifiqué con `git show 7a10ab9 --word-diff`, que muestra exactamente esa sustitución y ninguna otra. La mitad del orden se corrigió en `8d5be75`. Ver §7 |
| `H-08` | P3 | **Cerrado** | Contracts §3.1 ya no menciona `A-4` para `QG-06`: dice «Su rótulo es **[ASUNCIÓN derivada del intake §17.4.P.10]**», que es lo que la fuente dice —abrí §17.4.P.10 y el rótulo es «[ASUNCIÓN derivada de RT §7.2]»—. Api dice ahora que §2.2 fija el mutation score «para el tipo **`library`** … y la fila **`rest-api`** … **no pide mutation score**. Esta categoría lo adopta igual, tomándolo prestado» |
| `H-09` | P3 | **Cerrado** | Las tres citas: Web `QG-03` restituye «una subida **por FTP** que deja la aplicación caída»; Visor §2 restituye «rotar y acercar **con el mouse**»; Visor §3 saca de las comillas la descripción de las tres formas de petición en lugar de fingir literalidad. Comparé las tres contra §17.6.P.8, §17.7.P.10 y §17.7.P.6 del intake |

**Nueve de nueve cerrados.** Ninguna corrección cambia un caso de prueba, un umbral ni un recuento; la única mudanza de sustancia es el carácter de los dos gates de `H-02`, que es lo que r1 exigía.

---

## 3. Los tres recuentos que la corrección declaró subcontados

Los reconté yo sobre el texto anterior a la corrección, con `git show 1d3bbeb:<archivo>`. **Los tres son subconteos de r1.**

### 3.1 `H-01`: eran más de nueve

r1 listó nueve pasajes en nueve documentos: cinco de Api y cuatro de Infrastructure. **Faltaban dos documentos de Infrastructure**, y uno de los dos es donde el defecto era más grande:

- **`GeometriaFactory-Infrastructure/08-Calidad-Y-Pruebas/Estrategia-Calidad.md`**, ausente por completo de la tabla de r1. En `1d3bbeb` tenía: la entrada de tabla de contenido (línea 22), **el título de sección** «### 3.2 La batería del validador tiene diez casos **y el intake escribe nueve en dos lugares**» (línea 87), la viñeta «El intake **§17.3.P.8** escribe «las **nueve** pruebas del validador pasan», y **§17.5.P.8** repite…» (línea 92) y el párrafo de cierre «El texto de los dos gates del intake **es** anterior … **corregirlo es del Product Owner sobre su propio documento**» (línea 95), que es un punto abierto falso más, además de los dos que r1 sí contó.
- **`GeometriaFactory-Infrastructure/08-Calidad-Y-Pruebas/Estrategia-Testing.md`** línea 173: «Sobre el recuento de nueve que dos gates del intake **todavía escriben**».

Además, r1 contó un solo pasaje en dos documentos que tenían dos: `Api/Criterios-Validacion.md` (fila `CV-31` y nota de §6) y `Api/Estrategia-Calidad.md` (líneas 89 y 91), y no listó la fila `CV-02` de `Infrastructure/Criterios-Validacion.md`.

**Veredicto.** Por documento son **once**; por pasaje distinguible, entre diez y catorce según dónde se corte una sección que es un bloque. La cifra exacta depende de la granularidad, pero **la dirección de la afirmación de la corrección es verdadera y verificada: r1 subcontó**, y omitió el documento donde el defecto tenía título propio en la tabla de contenido. La corrección no infló: corrigió los once y sólo los once.

**Un error menor de r1, de paso.** Su tabla de `H-01` ubica el hueco de `Infrastructure/Matriz-Cobertura-Pruebas.md` en «§7»; los encabezados de ese documento, en `1d3bbeb` y en `HEAD`, ponen «Huecos identificados» en **§8** —§7 es «Cobertura por capa»—. No afecta el hallazgo.

### 3.2 `H-05`: estaba en tres documentos

r1 lo registró sólo en el Visor. Leí los tres §6 en `1d3bbeb`:

| Documento | Texto en `1d3bbeb` | Texto en `HEAD` |
| --- | --- | --- |
| `Visor/Estrategia-Testing.md` línea 104 | «§21 los cruza contra la batería obligatoria de **nueve** casos de prueba» | **diez** |
| `Domain/Estrategia-Testing.md` línea 112 | «§21 los cruza contra la batería obligatoria de **nueve** casos de prueba» | **diez**, con la aposición del décimo |
| `Contracts/Estrategia-Testing.md` línea 102 | «§21 los cruza contra la batería obligatoria de **nueve** casos de prueba» | **diez**, con la aposición del décimo |

La frase es literalmente la misma en los tres: es una transcripción propagada entre proyectos de código de la misma ola. **Confirmado: eran tres y r1 vio uno.**

### 3.3 `H-03`: era un cruce sistemático, no una frase

r1 lo describió como un error en «el párrafo que certifica la correspondencia». En `1d3bbeb`, la tabla §3.2 de `Web/Estrategia-Calidad.md` —la tabla de las puertas técnicas, que r1 dio por buena en su §4— tenía las dos definiciones **cambiadas de lugar**:

- `PT-02` decía «Que el visor funcione embebido **y que el motor de dibujo quede dentro del bundle**…»
- `PT-03` decía «Que **el bundle cargue en una página del anfitrión**, que la escena y el árbol **se sincronicen por índice** y que los recorridos no degraden»

El intake §17.7.P.8, que abrí, dice lo contrario de las dos: **`PT-03`** es «Three.js dentro del bundle, la página funciona sin acceso a CDN» y **`PT-02`** es «el bundle carga en una página Blazor Interactive Server … navegar y volver 10 veces no degrada, y el árbol y la escena se sincronizan por índice». Y en `Casos-Prueba-Referenciales.md`, **`TC-20` —«Sincronizacion-Del-Arbol-Y-La-Escena-Por-Indice-De-Pieza»— declaraba cubrir `PT-03`**, cuando la sincronización por índice es de `PT-02`; la fila de recuento del final decía además «`PT-03` se ejerce en `TC-20` y `TC-21`».

Son **cuatro lugares** —dos filas de la tabla §3.2, §8.2 y el campo «Cubre» de `TC-20`— más la fila de recuento, que arrastra el mismo error. **Confirmado: r1 vio uno de cuatro, y el que vio no era el más grave: una prueba declaraba verificar la puerta equivocada.** En `HEAD` los cinco lugares están corregidos y comparados contra §17.7.P.8.

---

## 4. `H-02`: la regla del carácter de los gates, verificada en los 77

**Conté los gates.** `grep -c '^| QG-'` sobre las siete `Estrategia-Calidad.md`: Domain **8**, Contracts **9**, Visor **9**, Application **11**, Web **11**, Infrastructure **14**, Api **15**. Suma **77**, que es el número que la corrección declara. La numeración es contigua de `QG-01` en los siete.

**Extraje la columna de carácter de los 77 y la crucé con el rótulo `[ASUNCIÓN]` de la condición.** Quince gates llevan rótulo; los **62 restantes no llevan ninguno y los 62 bloquean** —la fusión, la publicación, el flujo, el punto de control o el cierre de la etapa, según su columna—, sin un solo condicionado colgado de nada. Los quince:

| Gate | Qué asume | Rótulo en el intake | Carácter en `HEAD` | ¿Cierra la regla? |
| --- | --- | --- | --- | --- |
| Domain `QG-03` | 90 % líneas / 85 % ramas | §17.1.P.6, `A-3` | Condicionado | **Sí**: umbral |
| Domain `QG-07` | Batería en menos de 10 s | §17.1.P.10, `A-5` | Condicionado | **Sí**: umbral |
| Application `QG-03` | 85 / 80 | §17.2.P.6, `A-3` | Condicionado | **Sí**: umbral |
| Application `QG-10` | 500 ms | §17.2.P.10, `A-5` | Condicionado | **Sí**: umbral |
| Infrastructure `QG-05` | 85 / 80 | §17.3.P.6, `A-3` | Condicionado | **Sí**: umbral |
| Infrastructure `QG-06` | 95 % en el validador | §17.3.P.6, `A-3` | Condicionado | **Sí**: umbral |
| Infrastructure `QG-14` | 200 ms | §17.3.P.10, `A-5` | Condicionado | **Sí**: umbral |
| Api `QG-03` | 75 / 70 | §17.5.P.6, `A-3` | Condicionado | **Sí**: umbral |
| Api `QG-04` | Pirámide 60 / 40 | §17.5.P.6, rotulado `[ASUNCIÓN]` en la fuente | Condicionado | **Sí**: es un valor numérico de la fuente, no la forma de la puerta |
| Api `QG-13` | 30 s de arranque en frío | §17.5.P.10, `A-5` | Condicionado | **Sí**: umbral |
| Api `QG-14` | p99 500 ms y 20 pet./min | §17.5.P.10, `A-5` | Condicionado | **Sí**: umbral |
| **Contracts `QG-05`** | 100 % de DTOs ejercitados | §17.4.P.6, **`A-4`** | **Bloqueante** | **Sí**: `A-4` dice «cambia **la forma** del gate, no su carácter bloqueante» |
| **Web `QG-04`** | 100 % de pasos del guion | §17.6.P.6, **`A-4`** | **Bloqueante** | **Sí**: mismo fundamento, y §17.6.P.6 lo llama «gate **bloqueante** y numérico» |
| Infrastructure `QG-07` | — (0.01) | §22, «lo que **NO** es asunción» | Bloqueante | **Sí**: no es asunción |
| Contracts `QG-06` | Qué campos quedan fuera de la proyección de listado | §17.4.P.10, «[ASUNCIÓN **derivada** de RT §7.2]» | Condicionado | **Sí, con matiz**: ver abajo |

**El único caso que exige argumento es Contracts `QG-06`**, y lo examiné a fondo porque es el que la regla enunciada —«sólo queda condicionado lo que el intake rotula como asunción sobre el umbral mismo»— no cubre con esas palabras: `QG-06` no tiene umbral, tiene contenido. Pero tampoco es un caso de `A-4`: **§22 no lo enumera** —lo confirmé leyendo la fila `A-4`, que sólo lista «100 % de DTOs ejercitados, 100 % de pasos de guion, cero llamadas de red»—, de modo que no hay ninguna declaración de la fuente que ponga a salvo su carácter bloqueante, que es exactamente lo que hacía inadmisibles a los otros dos. Y lo que la asunción pone en duda es **si el gate tiene objeto**: si el Product Owner decide que la proyección de listado sí puede llevar el texto original, el gate no cambia de forma, **desaparece**. Condicionarlo es la lectura correcta, y `Estrategia-Calidad.md` §3.1 lo dice con esas mismas palabras: «lo que la asunción pone en duda es **qué se verifica** … no la forma de expresar la puerta». **Lo doy por bien condicionado.**

**No condicionó de más ni de menos.** No hay ningún gate bloqueante cuyo `[ASUNCIÓN]` sea sobre el umbral, ni ningún condicionado cuya asunción sea sobre la forma. La regla cierra en los 77.

---

## 5. Las dos cosas que la corrección decidió no hacer

### 5.1 No subir el piso de cobertura de `GeometriaFactory-Api`

**Dictamen: la decisión es correcta, y el punto abierto que deja es verdadero.**

Las tres razones, en orden de peso:

1. **Subirlo habría sido contradecir a la fuente por cuenta propia.** El 75/70 no lo eligió la categoría 08: lo fija el intake §17.5.P.6 con rótulo `[ASUNCIÓN]` y §22 lo registra como `A-3`. Una categoría 08 que sube un número de la fuente para que su documento cierre contra la guía está haciendo, en dirección contraria, exactamente lo que r1 elogió que Infrastructure y Api **no** hicieran con la batería: no bajaron la batería a nueve para que coincidiera con la redacción del gate. La simetría es exacta.
2. **La ADR no es un artefacto que la 08 pueda emitir.** `Rules-Calidad-Y-Pruebas.md` §2.2 no pide que se declare la caída: pide **un ADR que la justifique**, y las ADR viven en `05-Arquitectura-Tecnica/Adrs/`. Cerrar el apartamiento acá habría exigido que la categoría 08 se autorizara a sí misma un apartamiento de arquitectura. Decir «cerrarla es de la categoría 05» no es escurrir el bulto: es respetar de quién es la decisión.
3. **Lo que r1 pidió está hecho, y con más de lo pedido.** r1 exigía «declarar en Api la comparación de piso». `Estrategia-Testing.md` §2 declara hoy el «75 < 80», cita la frase entera de §2.2 —«no bajarla sin un **ADR** que lo justifique»—, dice con qué autoridad se baja, dice que **«la autoridad de la fuente no reemplaza a la ADR que §2.2 exige»**, y dice qué la compensa aclarando que la compensación «no es un argumento para no declararla». Y la matriz §8 lleva un hueco nuevo con consecuencia y dueño.

**El punto abierto es verdadero, no falso**, que es la distinción que esta auditoría hace: la ADR efectivamente no existe, y el criterio negativo de la ronda excluye del hallazgo al punto abierto correctamente declarado. **Anoto una obligación, no un hallazgo:** la categoría 05 de `GeometriaFactory-Api` ya está emitida en **1.1**, de modo que cerrar este hueco requiere **reabrirla**, y eso no ocurre solo. Si la 05 no se reabre, el apartamiento sobrevive declarado y sin resolver hasta el despliegue.

### 5.2 No reescribir las filas 1.0 de control de cambios

**Dictamen: el criterio es correcto, y es el que esta misma auditoría viene recomendando.**

Verifiqué que se cumplió: `git show 8d5be75 -U0 | grep -c '^-| 1\.0 |'` devuelve **1**, y esa única línea es la fila 1.0 del intake **movida de lugar**, no reescrita —su texto es idéntico carácter por carácter—. Los 38 documentos que subieron a 1.1 conservan su fila 1.0 intacta y agregaron una fila 1.1 que nombra el hallazgo que corrige, qué cambió y qué **no** cambió.

Es lo correcto por dos motivos. Primero, porque una fila de control de cambios **fechada** no es una descripción del documento: es el registro de qué declaró esa versión ese día. Reescribirla para que diga lo que el documento dice hoy borra la evidencia de que el defecto existió y hace imposible auditar la trayectoria —que es, precisamente, lo que permitió a esta ronda reconstruir los tres subconteos con `git show`—. Segundo, porque el corpus ya usa esa disciplina en el intake, cuyas filas 1.10, 1.18 y 1.20 describen en pasado errores que ya no están en el texto. Reescribir las 1.0 habría roto esa convención en la dirección equivocada.

**Un solo cuidado, que la corrección tuvo:** una fila 1.0 conservada no debe quedar como la **única** descripción vigente de algo que cambió. Lo verifiqué en los seis documentos donde más riesgo había —los de `H-02`— y en los seis la fila 1.1 dice explícitamente qué enunciado de la 1.0 quedó superado.

---

## 6. Regresiones

Busqué las cinco clases que las correcciones producen. **Cuatro dan cero; la quinta da un caso.**

| Clase | Resultado | Cómo lo comprobé |
| --- | --- | --- |
| Recuentos que dejaron de cerrar | **0** | Recontados con herramienta sobre `HEAD`: **208** casos de prueba (27+22+21+31+35+35+37), **219** criterios (22+25+34+28+35+35+40), **71** casos de uso contra 71 filas de las siete tablas `CU ↔ tests`, **10** filas en §6.1 de Infrastructure, **61** sondas en la matriz de Web y **8** filas en §8.2, **12** sondas en la del Visor. Los recuentos nuevos también cierran: 27−2=**25**, 31−1=**30**, 21−1=**20**, 37−1=**36** |
| Identificadores fantasma | **0** | Mapeo inverso `TC → matriz` con `comm` en las dos direcciones sobre los siete pares: **0 citados no definidos, 0 definidos sin fila**. Los identificadores nuevos de las §2.1 —`BT-07`, `BT-09`, `BT-14`, `BT-16`, `BT-18`, `QG-08`, `QG-11`, `RA-01`, `CV-20` a `CV-23`— existen todos en su documento de origen, que abrí |
| Citas nuevas sin verificar | **0** | Abrí una por una las citas que la corrección **agregó**: §22 `A-4` («Cambia la forma del gate, no su carácter bloqueante»), §17.4.P.6 («el gate equivalente y bloqueante»), §17.6.P.6 («Gate bloqueante y numérico en lugar de cobertura de líneas»), §17.7.P.8 (las dos puertas), §17.4.P.10 («[ASUNCIÓN derivada de RT §7.2]»), §2.2 de la guía («80 % aplicación…» y «no bajarla sin un ADR»), y las cuatro del intake que dicen **diez**. **Las diez son literales** |
| Filas de tabla discordantes | **0** | Programa sobre los 46 archivos: para cada tabla, número de celdas de cada fila contra el de su encabezado, descontando `\|` escapados. **Cero desparejas** |
| Enlaces rotos | **0** | Programa sobre los 46: resolví todos los enlaces relativos contra el sistema de archivos. **Cero rotos.** Las anclas internas nuevas —`#21-…` de las cuatro §2.1 y la de §3.2 renombrada de Infrastructure— resuelven contra su encabezado |
| Versiones y tablas de artefactos | **1 caso**, ver `N-01` | Los 38 documentos que subieron llevan `Versión: 1.1` y **exactamente una** fila 1.1. Las siete tablas de artefactos de los README declaran, para cada documento, la versión que el documento realmente tiene: **cero desajustes** |

---

## 7. La tabla de control de cambios del intake

**Está completa, ordenada, y no se perdió ninguna fila.**

- **Recuento.** En `1d3bbeb` y en `7a10ab9` la tabla tenía **23** filas; en `HEAD` tiene **23**. El `git diff` del intake en `8d5be75` es de **una línea borrada y una agregada**, y son la misma fila —la 1.0— con texto idéntico: no hubo oportunidad de perder nada.
- **Orden.** Antes: `1.0, 1.20, 1.19, 1.18 … 1.1`. Ahora: `1.20, 1.19, 1.18, 1.17, 1.16, 1.15, 1.14, 1.13, 1.12, 1.11, 1.10, 1.9, 1.8, 1.7, 1.6, 1.5, 1.4, 1.3, 1.3, 1.2, 1.2, 1.1, 1.0`. **Descendente estricta, con la 1.0 al final donde corresponde.**
- **Los dos pares repetidos son legítimos y los verifiqué leyéndolos.** Las dos filas **1.2** son dos correcciones distintas del mismo día: una resuelve `HI-2` sobre §12 y §12.1 —el término «unidad de entrega»— y la otra resuelve `HI-1` sobre §6, flujo 4 —las tres piezas y dos advertencias de `E-1`—. Las dos **1.3** son igualmente distintas. Ninguna es duplicado de la otra.
- **`H-07`, primera mitad.** La fila 1.20 dice hoy «el fundamento del puerto en **§17.2.P.11**». Verifiqué con `git show 7a10ab9 --word-diff` que esa sustitución —y ninguna otra— se aplicó en el commit de la auditoría de r1, no en `8d5be75`. El resultado en `HEAD` es correcto: la línea 728 del intake, bajo §17.2.P.11, es la que habla del puerto del validador y de los **diez** casos, y §17.3.P.4 es «Persistencia» y no fue tocada.

**Observación, no hallazgo.** Ni el reordenamiento ni la corrección de la sección subieron la versión del intake ni agregaron fila de control de cambios: sigue en **1.20**. Es defendible —las dos operaciones son sobre la tabla de control de cambios misma, y ninguna toca una decisión ni un número del producto—, pero deja al intake con dos ediciones posteriores a la fila que las describiría. Lo registro para que quede visible, no como defecto.

---

## 8. Hallazgos nuevos

### P0 y P1

**Ninguno.**

### P2

**Ninguno.**

### P3

---

**`N-01` — `Infrastructure/Plan-Pruebas.md` cita `Estrategia-Testing.md` en la versión que dejó de tener, en el mismo commit que la cambió. Es la única regresión.**

**Dónde está.** `Proyectos/GeometriaFactory-Infrastructure/08-Calidad-Y-Pruebas/Plan-Pruebas.md`, cabecera de trazabilidad upstream.

**Qué dice.** `«**Trazabilidad upstream:** [Estrategia-Testing.md](Estrategia-Testing.md) **1.0**; [Casos-Prueba-Referenciales.md](Casos-Prueba-Referenciales.md) 1.0; …».`

**Qué debería decir.** `Estrategia-Testing.md` **1.1**. Ese documento subió a 1.1 en `8d5be75` por `H-01`, y `Plan-Pruebas.md` de Infrastructure también se editó y subió a 1.1 en el mismo commit. La cita a `Casos-Prueba-Referenciales.md` 1.0 sí es correcta: ese documento no se tocó.

**Cómo lo verifiqué.** Programa sobre las siete carpetas: para cada `[`X.md`](ruta) N.N` de una cabecera de trazabilidad upstream, abrí el documento apuntado y comparé su `**Versión:**` con la citada. De los siete `Plan-Pruebas.md`, **cinco citan 1.1** —Api, Domain, Contracts, Visor, Web—, uno cita 1.0 correctamente —Application, cuya `Estrategia-Testing.md` no se tocó— y **sólo Infrastructure quedó desactualizado**. La corrección hizo exactamente esta actualización en siete documentos de otros proyectos de código y se salteó éste.

**Por qué P3 y no más.** Es una etiqueta de versión, no una afirmación de contenido: el §6 de `Estrategia-Testing.md` 1.1 dice lo mismo que decía en 1.0 salvo el «nueve→diez», que `Plan-Pruebas.md` ya incorporó por su cuenta en `RQ-10`. No hay contradicción de sustancia entre los dos documentos.

---

**`N-02` — Los diez documentos de la categoría 08 de Infrastructure y Api citan su propia categoría 05 en `1.0`, y está en `1.1` desde antes de la Fase E.**

**Dónde está.** Las cabeceras de trazabilidad upstream de `Estrategia-Calidad.md`, `Estrategia-Testing.md`, `Criterios-Validacion.md`, `Matriz-Cobertura-Pruebas.md` y `Plan-Pruebas.md` de `GeometriaFactory-Infrastructure` y de `GeometriaFactory-Api`, que citan `../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md` **1.0**.

**Qué debería decir.** **1.1**. Los dos documentos de la categoría 05 pasaron a 1.1 el 2026-08-10 en el commit `802731e` —la corrección de la Fase C—, **anterior** a las dos olas de la Fase E. Y no fue un cambio cosmético: la 1.1 de Infrastructure reescribió justamente **§10.5**, que es la sección que `Estrategia-Calidad.md` §3.2 de la 08 cita entre comillas para sostener que la batería tiene diez casos.

**Cómo lo verifiqué.** El mismo programa de `N-01`. Después abrí las filas 1.1 de los dos documentos de la 05 y su §10.5 vivo: la sustancia que la 08 cita —«la batería tiene 10 casos», `E-7` como cobertura adicional— **sigue siendo verdadera en 1.1**, de modo que ninguna cita de la 08 quedó falsa. Lo que quedó viejo es la etiqueta.

**Por qué es de esta ronda y por qué P3.** No es una regresión de la corrección: viene de la emisión de la Fase E y r1 no lo detectó. Es P3 porque ninguna cita perdió veracidad. Pero es exactamente el defecto de familia que r1 señaló en su §13: la cabecera de trazabilidad es el instrumento que el corpus tiene para fechar sus afirmaciones sobre otras fuentes, y un instrumento que no se actualiza no informa.

---

**`N-03` — Siete documentos se editaron sin subir de versión ni dejar constancia.**

**Dónde está.** `Application/Criterios-Validacion.md`, `Contracts/Plan-Pruebas.md`, `Domain/Criterios-Validacion.md`, `Domain/Plan-Pruebas.md`, `Visor/Criterios-Validacion.md`, `Visor/Definition-Of-Done.md`, `Visor/Plan-Pruebas.md`.

**Qué pasó.** El commit toca **45** documentos de las siete carpetas; **38** suben a 1.1 con su fila —de ahí el «treinta y ocho documentos» del mensaje de commit, que es correcto para lo que cuenta pero no para lo que se modificó—. Los siete restantes quedan en `Versión: 1.0` con su control de cambios sin fila nueva, aunque su contenido cambió.

**Qué cambió en ellos.** Sólo la versión citada de un documento hermano o del intake en la cabecera de trazabilidad upstream —`1.0`→`1.1`, `1.19`→`1.20`—; lo verifiqué con `git show --word-diff` sobre los siete, y **no hay ninguna otra diferencia**.

**Por qué P3 y no más.** El criterio es defendible: actualizar el número de versión de una cita no es un cambio del documento sino la conservación de su exactitud, y obligar a subir minor por eso llenaría los controles de cambios de ruido. Lo registro porque deja siete documentos cuya versión declarada ya no identifica sus bytes, y porque **es la convención la que falta**, no la decisión: si actualizar una cita no sube versión, conviene decirlo una vez en la regla y no dejarlo implícito en siete casos.

---

### Observaciones que no son hallazgos

- **Etiqueta de hallazgo cruzada.** Domain y Contracts rotulan `H-01` en su fila 1.1 la corrección que en realidad cierra `H-05` —el Visor sí la rotula `H-05`—. Las dos filas explican en su propio texto que es «el mismo defecto que el informe registró en `GeometriaFactory-Visor` (`H-05`) y que también estaba acá», de modo que la trazabilidad no se pierde. No lo cuento: el defecto es de r1, que registró en un solo documento algo que estaba en tres, y la corrección lo dice.
- **Convivencia de `1.19` y `1.20` en las cabeceras.** Quince documentos siguen citando el intake **1.19** en su trazabilidad upstream —los de la ola 1 que la corrección no tuvo que tocar— mientras sus hermanos citan **1.20**. **No es un hallazgo**: una cabecera declara contra qué versión se escribió el documento, y decir 1.19 cuando se escribió contra 1.19 es la disciplina correcta, no un error. Lo contrario —renumerar en masa sin releer— sí lo sería.
- **Polisemia de «nueve».** El corpus usa «nueve» para los invariantes de Domain, los NFR de Application, los gates de Contracts y del Visor, los riesgos de `05`, los estados de `SD-20` y los criterios de salida de varios planes. Los contextos son disjuntos y ninguno se confunde con la batería del validador. Por el criterio negativo de esta ronda, **no es hallazgo**.

---

## 9. Lo que no pude verificar

- **Las tres fuentes originales —RF, RT y AN— viven en otro repositorio bajo `PROMPTs/`.** Toda afirmación que se apoye en «RT §11», «RT §7.2», «RT §12» o «RF §9.4» la verifiqué **sólo hasta el intake**. En particular, que `A-4` de §22 sea una lectura fiel de lo que las fuentes dicen sobre los gates de Contracts, Web y Visor lo verifiqué contra el intake y no contra RT. **No verificado.**
- **La cifra exacta de pasajes de `H-01`.** Verifiqué que r1 subcontó y que la corrección alcanzó todos los pasajes vivos; que el número justo sea «diez» y no once o catorce depende de dónde se corte una sección que es un bloque, y no hay convención declarada que lo fije. **Lo verificado es la dirección, no el número.**
- **Si los umbrales condicionados son adecuados al uso previsto** —90/85, 85/80, 75/70, 95 en el validador, 10 s, 500 ms, 200 ms, p99 500 ms, 20 pet./min, 30 s—. Verifiqué que estén rotulados, que su condicionamiento cierre contra §22 y que ninguno se moviera en la corrección; no que sean razonables. **No verificado.**
- **Si la categoría 05 de `GeometriaFactory-Api` va a reabrirse para emitir la ADR de `H-06`.** Verifiqué que el punto abierto es verdadero, que tiene dueño nombrado y que no se disfrazó de resuelto. Su cierre efectivo es de otra fase. **No verificado.**

---

## 10. Dictamen

# APROBADO

**Se levanta el rechazo.**

**Fundamento.** Los **nueve** hallazgos están cerrados, y los verifiqué en el texto y con `git diff`, no en la declaración de que se corrigieron. Los dos P1, que eran los que sostenían el rechazo, están resueltos de raíz y no de fachada: `H-01` dejó **cero** afirmaciones en presente sobre un texto muerto y cerró los dos puntos abiertos falsos conservando su fila con el desenlace; `H-02` devolvió a bloqueantes los dos gates de `A-4` y **propagó el cambio a los siete documentos de cada proyecto de código** —gate, criterio, DoD, plan, matriz, caso de prueba y README—, con la forma, y sólo la forma, declarada sujeta a confirmación, que es la lectura fiel de la fuente.

**Lo que más pesa a favor.** La corrección **no se limitó a lo que el informe pedía: encontró más de lo que el informe había encontrado, y lo dijo.** Los tres recuentos que declaró subcontados están subcontados de verdad, y los reconté yo sobre el texto anterior: `H-01` omitía un documento entero cuya §3.2 llevaba el defecto **en el título y en la tabla de contenido**; `H-05` estaba en tres documentos y no en uno; y `H-03` no era una frase suelta sino **las definiciones de dos puertas técnicas intercambiadas entre sí en la tabla que las declara, con una prueba diciendo verificar la puerta equivocada** —lo más grave de todo el conjunto, y r1 no lo vio porque leyó la tabla de §3.2 de Web sin compararla contra §17.7.P.8—. Un corrector que sólo hubiera tachado los nueve renglones de la lista habría dejado vivo el cruce de puertas y el «nueve» de Domain y Contracts.

**Lo segundo que pesa.** La regla de los gates la verifiqué en los **77**, no en una muestra, y cierra en los 77: 62 sin rótulo y bloqueantes, 11 condicionados por umbral con respaldo en `A-3` o `A-5`, 3 bloqueantes con rótulo por razón declarada, y un solo caso de frontera —Contracts `QG-06`— que la propia estrategia argumenta y que resuelve bien, porque lo que su asunción pone en duda no es el umbral ni la forma sino **si el gate tiene objeto**. No condicionó de más ni de menos, que era el riesgo simétrico.

**Lo tercero.** Las dos decisiones de **no hacer** están bien tomadas y bien declaradas. No subir el piso de Api es la misma conducta que r1 elogió en la batería del validador, leída en el otro sentido: no se mueve un número de la fuente para que el documento propio cierre. Y el punto abierto que queda es **verdadero**, con dueño y con consecuencia escrita, que es la única forma de punto abierto que esta auditoría admite. No reescribir las filas 1.0 preserva la evidencia que hizo posible, precisamente, esta ronda.

**Los tres hallazgos nuevos son P3 y ninguno bloquea.** Uno es una regresión real —una etiqueta de versión que la corrección actualizó en seis proyectos de código y se salteó en el séptimo—; los otros dos son una etiqueta vieja que viene de la emisión y una convención que conviene escribir. Ninguno toca un caso de prueba, un umbral, un recuento ni una decisión, y los tres se arreglan en una pasada de minutos que no necesita otra ronda de auditoría.

**Lo que conviene arrastrar a la fase siguiente, sin condicionar esta aprobación:** cerrar `N-01`; actualizar las diez citas de `N-02`; declarar la convención de `N-03`; y **anotar en el backlog del producto que la categoría 05 de `GeometriaFactory-Api` tiene que reabrirse** para emitir la ADR que el piso de 75 % exige, porque una categoría ya emitida no se reabre sola.

---

## 11. ¿Alcanza esta estrategia para confiar en el producto?

Sí, y esta ronda agrega una razón que la anterior no podía tener todavía.

La ronda 1 dijo que lo que convence no son los 208 casos ni los 219 criterios sino **qué eligieron verificar**, y eso sigue siendo cierto y sigue intacto: los ocho escenarios entran como datos reales en las siete capas, cada capa declara la forma exacta en que entran, ningún proyecto de código se permite un dato sintético de geometría, y los proyectos sin cobertura de líneas reemplazan el umbral por conjuntos cerrados que se cuentan y no se opinan —15 puntos de acceso, 15 códigos, 6 funciones, 61 sondas, 0 peticiones de red medidas en el peor caso—. Nada de eso se movió: la corrección tocó 45 documentos y **no cambió un solo caso, umbral ni recuento**, lo cual verifiqué contando de nuevo los doce conjuntos cerrados.

Lo que esta ronda agrega es una prueba sobre el proceso, no sobre el producto, y es la que faltaba. Una estrategia de pruebas vale lo que valga su capacidad de encontrar lo que se le escapó. **Acá el corrector encontró tres cosas que el auditor no había encontrado, las contó, las corrigió, y dijo que el informe estaba corto** —incluida la peor de las tres, dos puertas técnicas cruzadas en la tabla que las define—. Eso es lo contrario de un trámite de cumplimiento: un corrector que optimiza por cerrar la lista habría tachado nueve renglones y devuelto el documento. Y en el mismo movimiento se negó dos veces a cerrar por la vía fácil: no subió un piso de cobertura para que su tabla cerrara contra la guía, y no reescribió la historia de sus propios documentos para que su presente quedara prolijo. Un equipo que resiste esas dos tentaciones es un equipo del que se puede creer el resto de lo que declara.

La reserva que r1 nombró —«la estrategia es más confiable que la descripción que hace de sus propias fuentes»— **está a medio resolver, y hay que decirlo así.** El defecto de fondo era describir en presente el estado de un documento vivo; el remedio propuesto era que toda afirmación sobre otra fuente lleve la versión de esa fuente. La corrección lo aplicó donde tocó —«**hasta 1.19** el intake escribía nueve», «el intake **1.20** dice diez»— y ese patrón es exactamente el correcto. Pero `N-01` y `N-02` muestran que el instrumento que fecha esas afirmaciones, la cabecera de trazabilidad upstream, **se actualiza a mano y por eso se olvida**: once citas apuntan a versiones que ya no existen, diez de ellas desde la emisión misma. Mientras el corpus crezca por transcripción, esa fila va a envejecer sola en cada fase. No es motivo de rechazo —ninguna de las once cita algo que hoy sea falso, y lo verifiqué— pero es la próxima cosa que va a hacer caer una ronda si nadie la mecaniza.

Con eso dicho: **esta estrategia de pruebas alcanza de sobra para confiar en el producto que se va a construir.** Lo que verifica es lo que importa —que el validador se escriba contra el dato que los alumnos producen de verdad—, lo verifica con conjuntos contables y no con juicios, sabe dónde no puede confiar en sí misma y lo declara, y no hay un solo caso de prueba en los 208 que invente una verificación que ninguna fuente pida. Ninguna cobertura está declarada verde: las 208 salidas dicen «Sin ejecutar» y los 208 estados dicen `Pendiente`, porque todavía no hay código. Eso también es una forma de honestidad, y a esta altura del corpus ya se puede decir que es la forma habitual de este equipo.

---

## 12. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Ronda 2 de la auditoría de la Fase E, categoría `08-Calidad-Y-Pruebas` de los siete proyectos de código, contra la corrección del commit `8d5be75`. Verifica los **nueve** hallazgos de r1 uno por uno en el texto y con `git diff`; **reconstruye los tres recuentos que la corrección declaró subcontados** sobre el texto de `1d3bbeb` y confirma los tres —`H-01` omitía un documento entero de Infrastructure, `H-05` estaba en tres documentos y `H-03` era un cruce de `PT-02`/`PT-03` en cuatro lugares de Web, con `TC-20` declarando la puerta equivocada—; **verifica la regla del carácter de los gates en los 77**, con la tabla de los quince rotulados `[ASUNCIÓN]` y el único caso de frontera argumentado; dictamina las dos decisiones de no hacer —el piso de cobertura de Api y las filas 1.0— como **correctas**; recuenta los doce conjuntos cerrados y reconstruye el mapeo inverso `TC → matriz` en las dos direcciones sobre los **208** casos, con **0** huérfanos contra los 5 de r1; y comprueba forma con programa sobre los **46** archivos tocados —celdas, enlaces, anclas, versiones y tablas de artefactos—, con cero defectos. Verifica además que la tabla de control de cambios del intake quedó **completa (23 filas), descendente y sin perder ninguna**, con los dos pares legítimos de 1.2 y 1.3 conservados. **Tres hallazgos nuevos, los tres P3: una regresión de etiqueta de versión, diez citas viejas a la categoría 05 y siete documentos editados sin subir versión.** Dictamen **APROBADO**: se levanta el rechazo. |
