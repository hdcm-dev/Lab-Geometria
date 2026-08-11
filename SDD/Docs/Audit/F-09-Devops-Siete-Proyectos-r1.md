# Auditoría de la Fase F · categoría 09 DevOps de los siete proyectos de código · ronda 1

| Campo | Valor |
| --- | --- |
| Producto | Fábrica de Geometría |
| Rama auditada | `sdd/fase-f-devops` |
| Objeto de la ronda | Dictaminar la emisión 1.0 de la categoría **09-Devops** de los siete proyectos de código, entregada en dos olas: `1f5df1a` (Domain, Contracts, Visor, más el intake a 1.21) y `87fbb3b` (Application, Infrastructure, Api, Web) |
| Alcance auditado | Los **38** documentos nuevos de las siete carpetas `09-Devops/`; los **77** quality gates de los siete proyectos de código, uno por uno, contra la categoría 08 y contra §22; las **121** citas entrecomilladas de los 38 documentos; los siete modelos de ambientes; las dos guías de publicación de servicio y la del bundle; y forma —cabeceras, celdas, enlaces, anclas— verificada con programa sobre los 38 archivos |
| Fuentes de contraste | `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.21**, en especial §13, §14, §15, §16, §17.x.P.6, P.8, P.9, P.10 y P.11, y §22; las categorías **05**, **06** y **08** de cada proyecto de código; `Rules-Devops.md` **3.1** de `IA.SDD`, leída como norma y no modificada |
| Criterio de la ronda | **Ninguna cita se dio por buena sin abrir la fuente.** Las 121 ocurrencias entre comillas angulares se extrajeron con herramienta y se contrastaron contra el documento al que cada una se atribuye. El carácter de los 77 gates se verificó abriendo §22, las §17.x.P.6 y las §17.x.P.8, no leyendo la declaración de la emisión. Los recuentos se recontaron |
| Fuera de alcance | `_legacy/`; las fuentes originales bajo `PROMPTs/` de otro repositorio; `Producto/Pipeline-Producto.md`, no emitido y correctamente declarado fuera de sección; las categorías 10 y 11, no emitidas |
| Auditor | Auditor independiente, sin participación en la emisión |
| Fecha | 2026-08-11 |

---

## Tabla de contenido

- [1. Resumen ejecutivo](#1-resumen-ejecutivo)
- [2. Fidelidad de los gates, verificada en los 77](#2-fidelidad-de-los-gates-verificada-en-los-77)
- [3. Despliegue inventado: los cuatro que no despliegan](#3-despliegue-inventado-los-cuatro-que-no-despliegan)
- [4. Los dos servicios reales](#4-los-dos-servicios-reales)
- [5. Lo declarado irreducible o pendiente](#5-lo-declarado-irreducible-o-pendiente)
- [6. Afirmaciones sobre otras fuentes](#6-afirmaciones-sobre-otras-fuentes)
- [7. Recuentos](#7-recuentos)
- [8. Forma](#8-forma)
- [9. Hallazgos](#9-hallazgos)
- [10. Lo que no pude verificar](#10-lo-que-no-pude-verificar)
- [11. Dictamen](#11-dictamen)
- [12. ¿Alcanza este DevOps para poner el producto en manos de los alumnos?](#12-alcanza-este-devops-para-poner-el-producto-en-manos-de-los-alumnos)
- [13. Control de cambios](#13-control-de-cambios)

---

## 1. Resumen ejecutivo

**Los 77 gates están, ninguno sobra y el carácter de los 77 coincide con el que la Fase E fijó.** Los conté por proyecto de código sobre la tabla §3 de cada `Estrategia-Calidad.md` —8 en Domain, 9 en Contracts, 11 en Application, 14 en Infrastructure, 9 en el Visor, 11 en Web, 15 en Api— y los recuperé de los 38 documentos con herramienta: el conjunto de identificadores es exactamente el mismo, sin uno de más ni uno de menos, y cada gate aparece en el `Pipeline-CI-CD.md` de su proyecto de código con umbral y verificación. **Doce quedan condicionados y son exactamente los doce que deben quedarlo**: los dos de Domain, los dos de Application, los tres de Infrastructure, los cuatro de Api y el `QG-06` de Contracts. **Los tres casos donde condicionar habría sido el error —Contracts `QG-05`, Web `QG-04`, Infrastructure `QG-07`— bloquean, y cada uno declara por qué.** No condicionó de más ni de menos.

**La regla se aplicó abriendo §22, no repitiéndola.** Los doce condicionados se apoyan en `A-3` o `A-5`, cuyas celdas leí una por una: las cuatro coberturas de `A-3` y los seis valores de `A-5` están todos, y no hay ninguno condicionado que no salga de esas dos filas —salvo la pirámide 60/40 de Api, que sí está rotulada `[ASUNCIÓN]` en §17.5.P.6 pero **no** figura en la enumeración de `A-3`, que es el único desliz de atribución que encontré en esta materia—. Los tres bloqueantes se apoyan en `A-4` —«Cambia la forma del gate, no su carácter bloqueante», que leí literal en §22— y en la frase de §22 «Lo que NO es asunción y conviene no confundir: la tolerancia de 0.01…», que Infrastructure cita con su caso testigo: 113.10 contra 113.09, diferencia de exactamente 0.01, y el operador estricto que hace que `E-1` dé dos advertencias y no tres.

**No hay despliegue inventado en ninguno de los cuatro que no despliegan.** Domain, Contracts, Application e Infrastructure declaran «ninguno de despliegue y ninguno de publicación», con la tabla de ambientes reducida a una fila que **no es un ambiente desplegado** —el contenedor de desarrollo—, el apartamiento del modelo `preview`/`stable` registrado en un §1.1 propio, y el motivo sostenido en `05` §5 y en el intake §13, que verifiqué. Ninguno de los cuatro tiene guía de publicación, y los cuatro `README.md` declaran esa omisión citando la celda de `Rules-Devops.md` §2.1 que la autoriza. Tres de los cuatro anclan el apartamiento en `ADR-03`; **Infrastructure lo ancla en el intake y no en un ADR**, que es una desviación menor de `Rules-Devops.md` §2.2.

**No hay un solo secreto, credencial ni dirección concreta en los 38 documentos.** Lo verifiqué con búsqueda sobre las siete carpetas: cero direcciones de red, cero nombres de proveedor de hosting, cero direcciones IP, cero puertos, cero cadenas de credencial. Los dos secretos del front y los tres valores de configuración del backend se nombran por su función y declaran dónde vive el valor, con la frase que cierra los pre-requisitos de la guía de FTP: «Ningún pre-requisito de esta guía se cumple escribiendo un valor acá». `RA-03` se sostiene desde los dos lados, con tres propiedades que se apoyan entre sí, y **el punto de salud no diagnostica**: `ADR-07` §2 regla 4 lo dice y la categoría 09 lo recoge sin ablandarlo.

**Las tres cosas que la emisión declaró irreducibles o pendientes están bien dictaminadas**, y ninguna es un punto abierto falso. El despliegue conjunto queda como acto humano coordinado porque un extremo se publica al fusionar y el otro lo ejecuta una persona: la emisión lo escribe sin suavizarlo y no inventa una automatización que la fuente no autoriza. La construcción en destino tiene procedimiento de prueba escrito y **la emisión declara expresamente que no declara que funcione**. La frecuencia de respaldo no se inventa: queda como `PD-04` con el «a definir por el docente» del intake §17.3.P.4, que leí en la fuente.

**De las 121 citas entrecomilladas, 118 resuelven contra la fuente que se les atribuye.** Las tres restantes son alteraciones de puntuación o paráfrasis breve dentro de comillas angulares, todas semánticamente fieles y con la fuente viva: ninguna cita a texto muerto, que es el defecto que rechazó dos fases de este producto. **La primera ola cita 1.20 y la segunda 1.21, como corresponde**, y verifiqué que el único cambio de 1.21 —§17.6.P.3, de cinco a seis funciones de la fachada— cae en un proyecto de código de la segunda ola, de modo que ningún documento de la primera quedó apoyado en el texto corregido.

**Los recuentos cierran los siete**: nueve invariantes, ocho escenarios, diez casos de la batería del validador, quince códigos vivos sobre dieciocho emitidos con tres retirados, quince puntos de acceso, seis funciones de la fachada. Ninguna cifra contradictoria en los 38 documentos. **Forma: cero problemas** —38 cabeceras completas, 38 en 1.0 y Propuesto, 38 con control de cambios, cero filas de tabla desparejas, cero enlaces relativos rotos, cero anclas muertas—.

**Cinco hallazgos, ninguno P0 ni P1**: uno P2 de forma de cita y cuatro P3 de atribución y de forma del apartamiento. **Dictamen: APROBADO.**

---

## 2. Fidelidad de los gates, verificada en los 77

### 2.1 El recuento, hecho por mí

Extraje los identificadores `QG-xx` de las siete `Estrategia-Calidad.md` §3 y de las siete carpetas `09-Devops/`, y comparé los conjuntos:

| Proyecto de código | Gates en 08 | Gates materializados en 09 | Diferencia |
| --- | --- | --- | --- |
| `GeometriaFactory-Domain` | 8 | `QG-01` a `QG-08` | Ninguna |
| `GeometriaFactory-Contracts` | 9 | `QG-01` a `QG-09` | Ninguna |
| `GeometriaFactory-Application` | 11 | `QG-01` a `QG-11` | Ninguna |
| `GeometriaFactory-Infrastructure` | 14 | `QG-01` a `QG-14` | Ninguna |
| `GeometriaFactory-Visor` | 9 | `QG-01` a `QG-09` | Ninguna |
| `GeometriaFactory-Web` | 11 | `QG-01` a `QG-11` | Ninguna |
| `GeometriaFactory-Api` | 15 | `QG-01` a `QG-15` | Ninguna |
| **Total** | **77** | **77** | **Ninguna** |

Los 77 aparecen en el `Pipeline-CI-CD.md` de su proyecto de código y en el `README.md` de la sección; ninguno aparece sólo en el índice. **No hay gate inventado**: no existe en los 38 documentos ningún identificador `QG-` por encima del último de cada proyecto de código, ni ninguna fila de la tabla de stages que declare un umbral bloqueante sin un `QG` detrás, salvo dos casos que la emisión rotula explícitamente como **no gates de 08** y que son puertas técnicas del producto —`PT-04` en Api §2.1 y las dos puertas del Visor, que 08 ya declaraba vinculantes—.

### 2.2 El carácter, verificado abriendo §22 y las P.6 y P.10

| Proyecto de código | Condicionados en 08 | Condicionados en 09 | Fila de §22 que lo sostiene |
| --- | --- | --- | --- |
| Domain | `QG-03`, `QG-07` | Los mismos dos | `A-3` (90/85) y `A-5` (10 s de la batería de dominio) |
| Contracts | `QG-06` | El mismo | §17.4.P.10, «[ASUNCIÓN derivada de RT §7.2]» sobre el contenido de la proyección |
| Application | `QG-03`, `QG-10` | Los mismos dos | `A-3` (85/80) y `A-5` (500 ms de validación en Application) |
| Infrastructure | `QG-05`, `QG-06`, `QG-14` | Los mismos tres | `A-3` (85/80 y 95 en el validador) y `A-5` (200 ms en el validador) |
| Visor | Ninguno | Ninguno | — |
| Web | Ninguno | Ninguno | — |
| Api | `QG-03`, `QG-04`, `QG-13`, `QG-14` | Los mismos cuatro | `A-3` (75/70), `A-5` (p99 500 ms, 20 por minuto, 30 s) y §17.5.P.6 para la pirámide |

**Doce condicionados, y los doce están en las filas `A-3` y `A-5` de §22, que abrí.** `A-3` enumera «90/85 en Domain, 85/80 en Application, 85/80 con 95 en el validador de Infrastructure, 75/70 en Api»: cuatro coberturas, cinco umbrales, todos condicionados. `A-5` enumera «500 ms de validación en Application, 200 ms en el validador, p99 de 500 ms y 20 peticiones por minuto en la Api, 30 s de arranque en frío, 10 s de la batería de dominio»: todos condicionados. **No sobra ninguno y no falta ninguno.**

**Los tres que no se condicionaron, y por qué está bien:**

1. **Contracts `QG-05`** —100 % de los tipos de transferencia ejercitados—. El intake §17.4.P.6 rotula `[ASUNCIÓN]` la frase «el gate equivalente y bloqueante es que el 100 % de los DTOs esté ejercitado por al menos una prueba de integración», que leí literal. §22 `A-4` cubre ese caso y su columna de consecuencia dice, textual, «Cambia la forma del gate, no su carácter bloqueante». La asunción es sobre la forma. **Bloquea, y el documento lo argumenta en su §2.2.**
2. **Web `QG-04`** —100 % de los pasos del guion de demostración—. §17.6.P.6 rotula «[ASUNCIÓN en cuanto a expresarlo como gate; la regla acumulativa es de RF §9.4]». La regla acumulativa no es asunción de nadie: la declara el intake §15. **Bloquea desde la primera etapa que lo alcanza.**
3. **Infrastructure `QG-07`** —tolerancia 0.01 con operador estricto—. §22 lo enumera expresamente entre «Lo que NO es asunción y conviene no confundir», con el fundamento del redondeo a dos decimales. El documento va más allá y escribe la consecuencia: un gate condicionado sobre `QG-07` dejaría al caso canónico del producto fallando sin detener nada. **Bloquea.**

El Visor merece una nota: **ninguno de sus nueve gates es condicionado**, y la marca `[ASUNCIÓN]` que lo alcanza —§17.7.P.6, «en cuanto a expresarlo como gate automatizable; la regla es de RA-02»— es sobre la forma. Verifiqué el texto en la fuente. `QG-04` bloquea, sin gradación.

**Nada se relajó.** Comparé columna a columna el carácter de los 77 entre 08 y 09: donde 08 dice «Bloquea la fusión», 09 dice «Bloqueante»; donde 08 dice «Bloquea el cierre de la etapa», 09 dice «Bloqueante al cierre de etapa»; donde 08 dice «Se rechaza en revisión aunque compile», 09 lo repite entero. **Los seis gates que 08 sitúa fuera de la fusión —cierre de etapa, publicación, punto de control, flujo— conservan su punto de aplicación en 09**, que era el modo de relajación más fácil de dejar pasar.

---

## 3. Despliegue inventado: los cuatro que no despliegan

Los cuatro proyectos de código sin unidad de despliegue propia —Domain, Contracts, Application, Infrastructure— resuelven el punto igual y bien:

| Verificación | Domain | Contracts | Application | Infrastructure |
| --- | --- | --- | --- | --- |
| ¿Declara ambientes de despliegue? | No | No | No | No |
| ¿Declara canales de publicación? | No | No | No | No |
| ¿La única fila de la tabla es un ambiente desplegado? | No: contenedor de desarrollo | No | No | No |
| ¿Tiene `Guia-Publicacion-<tipo>.md`? | No | No | No | No |
| ¿Declara la omisión de la guía con su motivo? | Sí, `README.md` §3 | Sí | Sí | Sí |
| ¿Declara el apartamiento del modelo de `Rules-Devops.md` §2.2? | Sí, §1.1 | Sí, §1.1 | Sí, §1.1 | Sí, §1.1 |
| ¿Ancla el apartamiento en un ADR, como pide §2.2? | Sí, `ADR-03` §4 | Sí, `ADR-03` §2 y §4 | Sí, `ADR-03` §2 | **No**: lo ancla en el intake §17.3.P.7 y §13 |

Los cuatro citan las mismas tres afirmaciones de fuente, que verifiqué: `05` §5 primera fila —sin unidad de despliegue propia—, `05` §5 última fila —`redistribuible` es false— y el intake §13, que declara que los dos artefactos entregables del producto son una imagen de contenedor y una publicación subida por FTP, y que ningún proyecto de código se publica como paquete redistribuible.

Y hay una frase que conviene registrar porque es el razonamiento correcto y no el trámite: Domain §1.1 declara que inventar un canal `preview` y uno `stable` sin feed detrás sería «la versión inversa» del anti-patrón de `Rules-Devops.md` §4.8 —inventar publicación donde sólo hay compilación—. Application e Infrastructure declaran lo simétrico: un `DEV`/`QA`/`PROD` propio duplicaría los ambientes de Api con otro nombre y otro dueño. **Ninguno de los cuatro inventó nada.**

La única desviación es la de la última fila: **Infrastructure no ancla su apartamiento en un ADR** aunque su §1.1 reconoce que `Rules-Devops.md` §2.2 lo pide. Es `H-04`.

---

## 4. Los dos servicios reales

### 4.1 Secretos, credenciales y direcciones concretas

Busqué sobre los 38 documentos, con expresión regular, nombres de proveedor de hosting, direcciones `http`/`https`, direcciones IP en cuatro octetos, puertos numéricos, rutas `ftp://` y las formas habituales de asignación de credencial. **Cero coincidencias en los 38.** El intake nombra al proveedor de hosting en §17.6.P.9; **ningún documento de esta categoría lo repite**, y lo llaman «el hosting público» o «el servicio gratuito con servidor de información, transporte seguro y dominio público».

Los valores se nombran por función y declaran dónde vive el valor:

| Valor | Cómo se nombra | Dónde se declara que vive | Documento |
| --- | --- | --- | --- |
| Credenciales del canal de publicación del front | Por su función, con alcance mínimo declarado | Secreto del repositorio | `Web/Guia-Publicacion-Front-Ftp.md` §1 |
| Dirección base del servicio de datos | Por su función | Secreto del repositorio, inyectado al publicar; «la dirección real del servidor propio no se versiona» | `Web/Guia-Publicacion-Front-Ftp.md` §1, `Web/Entornos-Deploy.md` §5 |
| Clave de firma del acceso | Por su función | Variable de entorno o archivo montado; «la recibe y no la busca» | `Api/Entornos-Deploy.md` §5, `Infrastructure/Entornos-Deploy.md` §5 |
| Ruta del almacén | Por su función | Configuración del ambiente; ningún punto de acceso la devuelve | `Api/Entornos-Deploy.md` §5 |
| Credenciales del repositorio, si fuera privado | Por su función | En el destino, fuera del repositorio | `Api/Entornos-Deploy.md` §6 |

### 4.2 `RA-03`

`Api/Entornos-Deploy.md` §4.1 sostiene la regla con tres propiedades que se apoyan entre sí y ninguna alcanza sola: el navegador nunca llama al servicio, la dirección no viaja en ningún contenido servido al navegador, y la dirección no se filtra por un mensaje de error. Cada una tiene dónde se verifica: `QG-05` de Web, `ADR-07` §7 y §8 de Web, y los dos `QG-08` —el de Api sobre los quince puntos y el registro del servidor, el de Web sobre los quince códigos vivos y el camino de ausencia de respuesta—.

**El punto de salud no diagnostica.** Abrí `ADR-07` de Api: su regla 4 dice que el punto de salud «no dice dónde está el almacén, ni con qué esquema, ni qué ruta se configuró», y cierra con «Es `RA-03` en el punto más tentador de todos: el que existe para diagnosticar». `Api/Entornos-Deploy.md` §4 lo cita literal —lo comprobé carácter por carácter— y agrega la frase que lo hace estructural: «Un servicio que no conoce su propia dirección externa no puede filtrarla». La tabla de valores de configuración cierra con la fila «Dirección externa del propio servicio · **No es configuración de este proyecto de código**».

### 4.3 La brecha de la cadena de suministro

Los siete `Supply-Chain-Seguridad.md` fijan el primer nivel del marco de integridad de la construcción y **los siete lo declaran con su brecha abierta, ninguno como alcanzado**. La fila de procedencia dice, en los siete, «**No cumplido.** Hoy no se emite ninguna». Los dos que despliegan agregan lo suyo: Web y Api declaran «No se firma, y la brecha se declara en lugar de darse por cubierta», y Api escribe una brecha propia que ninguna otra unidad tiene —**la imagen que la canalización verifica no es la imagen que corre**, porque la del servidor propio se construye ahí—. Es exactamente la clase de afirmación que una emisión complaciente habría omitido.

---

## 5. Lo declarado irreducible o pendiente

**El despliegue conjunto como acto humano coordinado: correcto.** `Web/README.md` §5 lo escribe sin suavizar: «con el front publicándose automáticamente al fusionar y el backend desplegándose a mano, el despliegue conjunto es siempre un acto humano coordinado». La cadena es coherente: Contracts eleva el hallazgo como `PD-01` —el filtro de rutas del flujo del front, que el intake §17.6.P.7 restringe a dos directorios, deja fuera al ensamblado de contratos—, Web lo resuelve con tres decisiones y **eleva al Product Owner la que cambia lo que la fuente enumera**, y sostiene la regla en `QG-08` de Contracts y no en el filtro, «porque el filtro dispara una construcción y no coordina dos despliegues, y uno de los dos es manual por decisión». La frontera de partida está bien puesta: el intake §17.5.P.8 declara el despliegue manual y del docente, y `Estrategia-Calidad.md` §3.3 de Api lo recoge —ningún criterio de la 08 se cumple ejecutando un despliegue—. **No se inventó automatización.**

**El punto de construcción en destino: correcto, y es el dictamen más difícil de los tres.** El intake §17.5.P.11 punto 5 marca el mecanismo `[A VERIFICAR]` y exige probarlo una vez antes de depender de él; lo leí en la fuente. `Api/Pipeline-CI-CD.md` §10 cierra con la distinción exacta: «esta categoría escribe **cómo se prueba** el mecanismo de construcción en destino, pero **no declara que funcione**». `PA-08` de `05` §11 queda con procedimiento y sin cerrar. La guía de publicación lo repite en su §2.1 y lo lista entre los pre-requisitos con la marca `[A VERIFICAR]` intacta. **La emisión escribió el procedimiento y no se atribuyó el resultado.**

**La frecuencia de respaldo: correcto.** El intake §17.3.P.4 declara la copia del archivo con el diario activo y la frecuencia «a definir por el docente» —cita verbatim, verificada—. `Infrastructure/Entornos-Deploy.md` §4 declara literalmente «Esta categoría no inventa una frecuencia» y aporta lo que sí le corresponde: las condiciones que el respaldo tiene que cumplir para servir de algo, con la observación de que el respaldo es el único mecanismo del producto para volver atrás sobre datos, porque volver a una etiqueta revierte el código y no el almacén. Queda como `PD-04`, con dueño. **Ninguno de los tres es un punto abierto falso**: abrí las tres fuentes y las tres siguen abiertas.

Revisé además los otros dieciséis puntos abiertos declarados en los siete `Pipeline-CI-CD.md`. Dos que podían ser falsos no lo son: `PD-04` de Api —la vigencia del acceso firmado— cita que el intake la declara «corta» sin fijar número, y §17.5.P.5 dice, en su fila Vigencia, «Corta. Renovación por reingreso; sin token de refresco en este alcance»; `PD-03` de Infrastructure —cuál de las dos funciones de derivación de clave se ancla— cita «PBKDF2 o Argon2», que el intake enumera en §17.3.P.1 sin elegir. **Los dos son verdaderos.**

---

## 6. Afirmaciones sobre otras fuentes

### 6.1 Las 121 citas

Extraje con herramienta las **121** ocurrencias entre comillas angulares de los 38 documentos y contrasté cada una contra el documento al que se la atribuye —intake, `05`, ADR, `06`, `08`, `Rules-Devops.md` o el propio documento cuando es autocita de una fila de tabla—. **118 resuelven exactas**, admitiendo como equivalentes la caída de marcas de negrita dentro de la cita y la diferencia de mayúscula inicial cuando la cita se inserta en medio de una oración, que son convención del corpus y no alteración de sentido.

Las **tres** que no resuelven exactas están en la sección 9 como `H-01` y `H-02`. Ninguna cita texto que no exista: las tres tienen fuente viva y contenido fiel. **Cero citas a texto muerto**, que es el defecto por el que se rechazaron dos fases de este producto.

### 6.2 El cambio de versión del intake entre las dos olas

| Ola | Commit | Proyectos de código | Versión de intake citada |
| --- | --- | --- | --- |
| Primera | `1f5df1a` | Domain, Contracts, Visor | **1.20** en los 16 documentos |
| Segunda | `87fbb3b` | Application, Infrastructure, Api, Web | **1.21** en los 22 documentos |

Es lo que corresponde. El commit `1f5df1a` bumpea el intake a 1.21 y archiva 1.20; la fila 1.21 de la tabla de control de cambios dice, textual, «Sube minor y archiva: 1.20 ya fue citada como insumo», que es la disciplina que el corpus ya aplicó en 1.19 y 1.20.

**Y verifiqué que el cambio de 1.21 no contaminara a la primera ola.** 1.21 corrige dos cosas: §17.6.P.3, que enumeraba cinco funciones de la fachada cuando §17.7.P.3 declara seis desde 1.6, y la fecha de cabecera. §17.6 es de `GeometriaFactory-Web`, que está en la **segunda** ola y cita 1.21. Ningún documento de la primera ola se apoya en §17.6.P.3. **La cita de 1.20 en Domain, Contracts y Visor es correcta y no encubre texto corregido.** Anoto, además, que la fila 1.21 atribuye el levantamiento a «la Fase F de nivel 0, que no la propagó»: la propagación era de la segunda ola y la segunda ola aplica seis, como se ve en el `QG-09` de Web y en la guía del bundle.

---

## 7. Recuentos

Conté sobre las fuentes y busqué contradicciones en los 38 documentos:

| Conjunto | Valor esperado | Verificación | Contradicciones en los 38 |
| --- | --- | --- | --- |
| Reglas de negocio | Dieciséis | Ningún documento de 09 las enumera; ninguno declara otra cifra | Ninguna |
| Invariantes de Domain | Nueve | `Domain/Pipeline-CI-CD.md` §2.1 «9 de 9»; `Domain/Estrategia-Versionado.md` §6 «los **nueve** invariantes» | Ninguna |
| Escenarios del intake | Ocho | `Infrastructure/README.md` y `Api/README.md` «8 de 8»; `Infrastructure/Pipeline-CI-CD.md` §2.1 «los **ocho** escenarios como entrada» | Ninguna |
| Casos de la batería del validador | Diez | `Infrastructure/Pipeline-CI-CD.md` §2.1 «10 de 10», `TC-01` a `TC-10`; `Api/Pipeline-CI-CD.md` remite a la misma batería | Ninguna. **Ningún documento dice nueve** |
| Códigos de contrato | Quince vivos sobre dieciocho emitidos | `Contracts/Estrategia-Versionado.md` §6: «El conjunto cerrado tiene 15 códigos vivos sobre 18 identificadores emitidos, con 3 retirados»; `Contracts/Pipeline-CI-CD.md` `QG-04` «exactamente 15»; `Api` `QG-06` «14 de 15» con uno sin destino declarado; `Web` `QG-08` «los quince códigos vivos» | Ninguna |
| Puntos de acceso | Quince | Seis apariciones en cuatro documentos de Api, todas «quince» | Ninguna |
| Funciones de la fachada | Seis | `Visor/Pipeline-CI-CD.md` `QG-06` «exactamente 6»; `Visor/Guia-Publicacion-Bundle-Visor.md` «6 de 6»; `Web/Pipeline-CI-CD.md` `QG-09` «las 6 funciones» | Ninguna. **Ningún documento dice cinco** |

Verifiqué también el recuento propio que Api hace de su upstream —«los **diecisiete** NFR de este proyecto de código», atribuido a `05` §8—: la tabla de `05` §8 tiene diecisiete filas de datos. Correcto.

---

## 8. Forma

Verificado con programa sobre los 38 archivos:

| Comprobación | Resultado |
| --- | --- |
| Cabecera completa —proyecto de código, documento, versión, estado, fecha— | **38 de 38** |
| Versión declarada | **38 en 1.0** |
| Estado declarado | **38 en Propuesto**, coherente entre sí y con el `README.md` de cada sección |
| Sección de control de cambios | **38 de 38**, con fila 1.0 fechada y descriptiva |
| Filas de tabla con tantas celdas como columnas | **Cero desparejas** en las 38 |
| Enlaces relativos que resuelven a un archivo existente | **Cero rotos** |
| Anclas internas de tabla de contenido | **Cero muertas** |
| Tabla de contenido en documentos de más de tres secciones | Presente en los 38 |
| Nomenclatura de la guía de publicación | Las tres usan el patrón `Guia-Publicacion-<tipo-artefacto>.md`; las dos que acuñan tipo nuevo —`Bundle-Visor` y `Front-Ftp`— declaran por escrito por qué no encajan en ninguna familia de `Rules-Devops.md` §3.1 y se apoyan en que la lista «no es cerrada» |
| `Pipeline-Producto.md` | No emitido, y los siete `README.md` lo declaran «**No es de esta sección**», con la cita de `Rules-Devops.md` §2.1 y §4.9. **Correctamente diferido, no omitido en silencio** |

---

## 9. Hallazgos

### `H-01` · P2 · Paráfrasis dentro de comillas angulares, atribuida al intake §17.5.P.8

**Dónde.** `SDD/Docs/Proyectos/GeometriaFactory-Api/09-Devops/Pipeline-CI-CD.md` §1, línea 39. Con variante en `Guia-Publicacion-Image-Docker.md` §1, línea 36.

**Qué dice.** «El intake §17.5.P.8 lo declara **[DECISIÓN]**: “el despliegue lo ejecuta el docente, a mano; el agente IA entrega el `Dockerfile` y el `compose.yaml` y no ejecuta el despliegue”».

**Qué dice la fuente.** La fila `despliegue` de §17.5.P.8 dice: «**Manual, por el docente** [DECISIÓN, RT §13]. El agente IA entrega el `Dockerfile` y el `compose.yaml` y no ejecuta el despliegue». La segunda mitad es verbatim; **la primera —«el despliegue lo ejecuta el docente, a mano»— es una reescritura**, fiel en sentido pero no literal, presentada entre comillas angulares. La variante de la guía altera además la conjunción por punto y coma.

**Qué debería decir.** O la cita literal de la celda, o la reformulación fuera de las comillas con la referencia al lado. En un corpus donde dos fases se rechazaron por citas defectuosas, la comilla angular tiene que significar transcripción.

**Cómo lo verifiqué.** Extraje las 121 citas con `grep -o «[^»]*»` sobre los 38 documentos y busqué cada una literal en el intake; ésta dio cero. Después abrí §17.5.P.8 entera con `awk` y comparé la celda contra la cita.

**Por qué P2 y no P1.** El contenido es correcto, la fuente existe y está viva, y la consecuencia sobre el pipeline —el stage `despliegue` no ejecuta nada y ningún gate de 08 se cumple desplegando— está bien derivada. Es forma de cita, no afirmación falsa.

### `H-02` · P3 · Cita del intake §17.3.P.3 con la puntuación y un término alterados

**Dónde.** `SDD/Docs/Proyectos/GeometriaFactory-Infrastructure/09-Devops/Entornos-Deploy.md` §1, línea 43.

**Qué dice.** «no expone endpoints; consume el sistema de archivos donde vive el archivo y nada más».

**Qué dice la fuente.** §17.3.P.3: «**No aplica**: no expone endpoints. Consume el sistema de archivos donde vive el archivo SQLite y nada más». Dos oraciones fundidas en una con punto y coma, y **el término del motor de almacenamiento eliminado**.

**Qué debería decir.** La transcripción literal, o la elisión marcada con corchetes. La supresión del nombre del motor es coherente con la disciplina del corpus de no nombrar stacks en prosa, pero entonces corresponde parafrasear fuera de las comillas.

**Cómo lo verifiqué.** Misma extracción de citas; búsqueda literal en el intake con cero resultados; apertura de §17.3.P.3 completa.

### `H-03` · P3 · Dos atribuciones de procedencia de gates que no cierran

**Dónde.** `GeometriaFactory-Api/09-Devops/Pipeline-CI-CD.md` §2.1, párrafo de cierre, y `GeometriaFactory-Infrastructure/09-Devops/Pipeline-CI-CD.md` §2.2, primer párrafo.

**Qué dice.** Api: «Los cinco primeros de la lista de `Estrategia-Calidad.md` §3 salen del intake §17.5.P.8». Infrastructure: «Los cuatro primeros de la lista de `Estrategia-Calidad.md` §3 los declara el intake §17.3.P.8».

**Qué dice la fuente.** §17.5.P.8 declara cinco filas de stage —build, test, cobertura, imagen, despliegue—, que corresponden a `QG-01`, `QG-02`, `QG-03` y `QG-13`. **`QG-04` sale de §17.5.P.6 y `QG-05` de `05` §8 y `RN-13`, no de P.8**; y `QG-13`, que sí sale de P.8, es el decimotercero. En Infrastructure, §17.3.P.8 declara cuatro gates bloqueantes —build, las diez del validador, las transformaciones solas, la cobertura de P.6—, que son `QG-01`, `QG-03`, `QG-04` y **`QG-05`**, no los cuatro primeros de la lista.

**Qué debería decir.** Enumerar los identificadores en lugar de un rango ordinal, que es lo que el resto de los cinco documentos hace.

**Cómo lo verifiqué.** Abrí §17.5.P.8 y §17.3.P.8 completas con `awk` y las crucé fila por fila contra la tabla §3 de las dos `Estrategia-Calidad.md`.

**Sin consecuencia sobre lo auditado.** Los quince y los catorce gates están todos, con su carácter correcto; lo que falla es la frase que resume de dónde vienen.

### `H-04` · P3 · Infrastructure aparta el modelo de ambientes sin el ADR que `Rules-Devops.md` §2.2 exige

**Dónde.** `SDD/Docs/Proyectos/GeometriaFactory-Infrastructure/09-Devops/Entornos-Deploy.md` §1.1.

**Qué dice.** «`Rules-Devops.md` §2.2 fija para el tipo `library` el modelo de canales `preview` / `stable` sobre feed único, y admite apartarse con un ADR que lo justifique. **Acá no hay feed**: el intake §17.3.P.7 declara la estrategia idéntica a §17.1.P.7, sin publicación, y §13 lo generaliza al producto entero».

**Qué debería decir.** La regla pide un ADR y el documento lo reconoce en la misma oración, para después sustituirlo por una cita al intake. Domain, Contracts y Application resuelven el mismo apartamiento anclándolo en su `ADR-03`; Infrastructure tiene siete ADR y ninguna cubre publicación ni canales, de modo que la salida limpia es declarar la ausencia de ADR como punto abierto con dueño, o elevar la decisión a la categoría 05.

**Cómo lo verifiqué.** Leí §1.1 de los cuatro `Entornos-Deploy.md` de las bibliotecas y listé el directorio `05-Arquitectura-Tecnica/Adrs/` de Infrastructure: `ADR-01` a `ADR-07`, ninguna sobre publicación.

**No es despliegue inventado.** El apartamiento es sustantivamente correcto —no hay feed y no lo hay por decisión del producto—; lo que falta es el instrumento que la regla nombra.

### `H-05` · P3 · La pirámide 60/40 de Api se atribuye a la fila `A-3` de §22, que no la enumera

**Dónde.** `SDD/Docs/Proyectos/GeometriaFactory-Api/09-Devops/Pipeline-CI-CD.md` §2.2: «con `A-3` para la cobertura y la forma de la pirámide».

**Qué dice la fuente.** `A-3` de §22 dice «Coberturas mínimas: 90/85 en Domain, 85/80 en Application, 85/80 con 95 en el validador de Infrastructure, 75/70 en Api». **No menciona la pirámide.** La marca `[ASUNCIÓN]` de la pirámide está en el cuerpo de §17.5.P.6, y `05` §8 de Api la recoge —«Los cinco primeros vienen rotulados [ASUNCIÓN] desde §17.5.P.6 y §17.5.P.10»—, pero §22 no la enumera en ninguna de sus filas.

**Qué debería decir.** Atribuir la pirámide a §17.5.P.6 —como la fila de la tabla de stages sí hace, con «[ASUNCIÓN del mismo origen]»— y reservar `A-3` para la cobertura.

**Cómo lo verifiqué.** Abrí §22 completa y leí las cinco filas; abrí §17.5.P.6 y `05` §8 de Api.

**No cambia el dictamen del gate.** `QG-04` está rotulado `[ASUNCIÓN]` en la fuente y la asunción es sobre la proporción numérica, o sea sobre el umbral. **Condicionarlo es correcto**; lo que falla es la fila de §22 que se le adjudica.

---

## 10. Lo que no pude verificar

- **Que los guiones del repositorio que los pipelines invocan existan.** Los siete documentos invocan `scripts/build.sh`, `scripts/test.sh`, `scripts/build-visor.sh` y `scripts/reset-db.sh`, que el intake §16 lista en el árbol previsto. El repositorio no tiene todavía el código, de modo que la existencia de los guiones es una promesa de la etapa `a` y no un hecho comprobable hoy. **No verificado**, y correctamente declarado como tal por la emisión.
- **Que el hosting acepte lo que la guía de FTP asume.** Es `PT-01.a`, marcado `[A VERIFICAR]` en la fuente. La emisión no lo declara resuelto. **No verificado**, y correctamente abierto.
- **Que el motor de contenedores del destino resuelva la referencia al repositorio.** Es `PD-01` de Api. **No verificado**, y correctamente abierto.
- **La coherencia con las categorías 10 y 11**, no emitidas. `Rules-Devops.md` §0 declara que 11 cita la política de acá sin definir una paralela; ese contraste sólo podrá hacerse cuando la 11 exista.

---

## 11. Dictamen

**APROBADO.**

**El fundamento es la materia propia de esta fase, y se sostiene en los 77.** La categoría 09 tenía una obligación central —ejecutar exactamente los quality gates que la Fase E fijó, sin agregar, sin relajar y respetando el carácter de cada uno— y la cumplió en los siete proyectos de código. Los 77 gates están, ninguno sobra, ninguno cambió de carácter y ninguno cambió de punto de aplicación. **Los doce condicionados son los doce que las filas `A-3` y `A-5` de §22 sostienen**; los tres que habrían sido el error de condicionar de más —Contracts `QG-05`, Web `QG-04`, Infrastructure `QG-07`— bloquean, y los tres lo argumentan volviendo a la fuente en lugar de repetir la regla. El producto ya condicionó de más una vez; esta emisión no repitió el defecto, y en el caso de Infrastructure escribió además la consecuencia de haberlo repetido.

**El segundo fundamento es lo que la emisión no hizo.** Cuatro proyectos de código sin despliegue propio no tienen canalización de despliegue, no tienen ambientes, no tienen canales y no tienen guía de publicación; los cuatro declaran la ausencia con su motivo apoyado en `05` §5 y en el intake §13. Los 38 documentos no contienen un secreto, una credencial ni una dirección concreta. La cadena de suministro declara su brecha en los siete y nunca se declara alcanzada, incluida la brecha propia de Api que ninguna otra unidad tiene. Y las tres cosas que la emisión declaró irreducibles o pendientes están bien dictaminadas: el despliegue conjunto se escribe como acto humano coordinado y no se disfraza de automatización; la construcción en destino tiene procedimiento escrito y la emisión declara expresamente que no declara que funcione; la frecuencia de respaldo no se inventa.

**Los cinco hallazgos no tocan ninguno de esos dos fundamentos.** Uno es forma de cita —una paráfrasis fiel dentro de comillas angulares—, dos más son alteraciones o imprecisiones de cita y de atribución que no alteran ningún gate ni ningún carácter, otro adjudica a la fila equivocada de §22 una asunción que existe y está bien tratada, y el último es un instrumento formal ausente en un apartamiento que es sustantivamente correcto. Ninguno es una afirmación falsa sobre una fuente, ninguno es un punto abierto falso, y ninguno cambia lo que el pipeline mide o bloquea. **Se recomienda corregirlos en la próxima versión de los cinco documentos afectados, sin que ello condicione el avance a la categoría siguiente.**

---

## 12. ¿Alcanza este DevOps para poner el producto en manos de los alumnos?

Alcanza para empezar, y su mayor virtud es que dice con precisión hasta dónde alcanza.

Lo que este DevOps garantiza es la parte que más importa en un producto de aula: **que lo que se rompe se note**. Los 77 gates cubren las propiedades que sostienen la topología entera —cero peticiones del navegador hacia el servicio de datos, cero apariciones de la credencial en el navegador, cero mensajes que expongan una dirección interna, exactamente cuatro puntos fuera de la guardia y ni uno más, cero eliminaciones fuera de alcance forzando la petición— y las cubre bloqueando, no midiendo. Un alumno que rompa cualquiera de ellas se entera antes de fusionar. Los doce condicionados son todos umbrales de rendimiento o de cobertura que el Product Owner todavía no confirmó: **ninguna propiedad de seguridad ni de topología quedó condicionada**, que es la distribución correcta del riesgo.

Lo que no garantiza está declarado y hay que leerlo. El despliegue del backend es manual, la coordinación entre los dos artefactos es humana en cada cambio incompatible del contrato, la subida por FTP no es transaccional, la construcción en destino no está probada, la imagen que la canalización verifica no es la imagen que corre, no hay firma ni procedencia, y el respaldo del almacén —único mecanismo del producto para volver atrás sobre datos, porque volver a una etiqueta revierte el código y no el almacén— no tiene frecuencia fijada. **Ese último punto es el que yo pondría primero en la lista del docente**: en un curso donde el trabajo de una comisión vive en un único archivo de un servidor domiciliario, la frecuencia de respaldo no es una decisión de infraestructura, es la diferencia entre perder una clase y perder un cuatrimestre. La emisión hizo bien en no inventar el número; el Product Owner hace mal si tarda en fijarlo.

Y hay una asimetría que conviene tener presente el primer día de uso: el front se publica solo al fusionar y el backend lo despliega una persona. Mientras eso sea así, **existe una ventana en la que el front publicado le habla a un backend viejo**, y ninguna canalización la cierra. La emisión la nombra y no la disimula, que es lo correcto en la categoría 09; cerrarla es una decisión de producto, no de pipeline.

---

## 13. Control de cambios

| Versión | Fecha | Descripción | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-11 | Auditoría de la ronda 1 de la Fase F, categoría 09-Devops de los siete proyectos de código, sobre los commits `1f5df1a` y `87fbb3b`. Verifica los **77** quality gates uno por uno contra la categoría 08 y contra §22 del intake, con el resultado de que ninguno sobra, ninguno falta, ninguno cambió de carácter y los **doce** condicionados son exactamente los que las filas `A-3` y `A-5` sostienen. Verifica que los cuatro proyectos de código sin despliegue propio no inventaron canalización, ambientes ni canales, y que la ausencia está declarada con motivo. Verifica que los **38** documentos no contienen secretos, credenciales ni direcciones concretas, que `RA-03` se sostiene desde los dos lados y que el punto de salud no diagnostica, y que la brecha de cadena de suministro se declara en los siete sin darse por alcanzada. Dictamina correctos los tres puntos que la emisión declaró irreducibles o pendientes. Contrasta las **121** citas entrecomilladas contra su fuente, con **118** exactas y ninguna a texto muerto, y confirma que la primera ola cita 1.20 y la segunda 1.21 sin contaminación entre ambas. Recuenta los siete conjuntos del producto sin contradicciones. Forma verificada con programa: cero filas desparejas, cero enlaces rotos, cero anclas muertas. **Cinco hallazgos: uno P2 y cuatro P3, ninguno P0 ni P1.** Dictamen **APROBADO**. | Auditor independiente |
