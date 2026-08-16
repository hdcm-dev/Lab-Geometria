# Auditoría de la Fase E · categoría 08 Calidad y Pruebas de los siete proyectos de código · ronda 1

| Campo | Valor |
| --- | --- |
| Producto | Fábrica de Geometría |
| Rama auditada | `sdd/fase-e-calidad` |
| Objeto de la ronda | Dictaminar la Fase E emitida en dos olas, commits `93018ed` (los tres proyectos de código de nivel topológico 0) y `1d3bbeb` (los cuatro restantes, más el intake 1.20) |
| Alcance auditado | Los **58** documentos nuevos de `Proyectos/*/08-Calidad-Y-Pruebas/` —contados sobre `git diff --name-only 7404030 HEAD`, que da 60 rutas, de las cuales una es la modificación del intake y otra la copia archivada de su versión 1.19—. Los **208** casos de prueba, los **219** criterios de validación y las **siete** matrices de cobertura. Más las fuentes contra las que se contrastan |
| Fuentes de contraste | `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.20**, en especial §15, §17.x.P.6 y P.8, §20, §21 y §22; las categorías **02**, **03**, **05** y **06** de cada proyecto de código; `Matriz-Sensado-Deriva.md` **1.2** de `GeometriaFactory-Web`, emitida en la Fase B2; y `IA.SDD/SDD/Devs/Rules/Rules-Calidad-Y-Pruebas.md` (repositorio de origen, **sólo lectura**) |
| Criterio de la ronda | **El instrumento, no la conclusión.** Ninguna cobertura se acepta por estar declarada: el mapeo inverso `TC → matriz` se reconstruyó con herramienta sobre los catálogos y las matrices, los recuentos se contaron de nuevo, y ninguna cita entrecomillada se dio por buena sin abrir el documento citado. El antecedente pesa dos veces: el rechazo de la Fase C fue por dos citas de un texto del intake que ya no existía, y esta fase **modifica el intake en el mismo commit en que lo cita** |
| Fuera de alcance | `_legacy/`; las tres fuentes originales del intake, que viven en otro repositorio bajo `PROMPTs/`; las categorías 04 y 09 a 11, no emitidas; `Matriz-Sensado-Deriva.md` de Web como artefacto, ya auditada en la Fase B2 —acá sólo se audita **qué hizo la Fase E con ella** |
| Auditor | Auditor independiente, sin participación en la emisión |
| Fecha | 2026-08-11 |

---

## Tabla de contenido

- [1. Resumen ejecutivo](#1-resumen-ejecutivo)
- [2. Cobertura, reconstruida con herramienta](#2-cobertura-reconstruida-con-herramienta)
- [3. Los apartamientos deliberados](#3-los-apartamientos-deliberados)
- [4. Las puertas técnicas](#4-las-puertas-tecnicas)
- [5. Los ocho escenarios como fixtures](#5-los-ocho-escenarios-como-fixtures)
- [6. La integración con la matriz de sensado de Web](#6-la-integracion-con-la-matriz-de-sensado-de-web)
- [7. Veracidad de las afirmaciones sobre otras fuentes](#7-veracidad-de-las-afirmaciones-sobre-otras-fuentes)
- [8. Recuentos, contados de nuevo](#8-recuentos-contados-de-nuevo)
- [9. Forma](#9-forma)
- [10. Hallazgos](#10-hallazgos)
- [11. Lo que no pude verificar](#11-lo-que-no-pude-verificar)
- [12. Dictamen](#12-dictamen)
- [13. ¿Alcanza esta estrategia para confiar en el producto?](#13-alcanza-esta-estrategia-para-confiar-en-el-producto)
- [14. Control de cambios](#14-control-de-cambios)

---

## 1. Resumen ejecutivo

**La cobertura está completa y la verifiqué reconstruyendo el mapeo inverso, no leyendo los párrafos que la declaran.** Los **71** casos de uso de los siete proyectos de código —Domain 13, Contracts 8, Visor 7, Application 11, Web 10, Infrastructure 10, Api 12, contados sobre los archivos de `02-Especificacion-Funcional/Casos-De-Uso/`— tienen las **71** filas en las siete tablas `CU ↔ tests`, una por caso de uso, ninguna agrupada. Las **dieciséis** reglas, los **nueve** invariantes, los **ocho** escenarios, las **nueve** necesidades de negocio, los **quince** códigos de contrato vivos sobre dieciocho emitidos, los **quince** puntos de acceso, las **seis** funciones de la fachada y los **diez** casos de la batería del validador aparecen todos, y los conté de nuevo uno por uno. **Ningún caso de prueba referencia un identificador que no exista**: los 208 `TC-XX` citados en las siete matrices están todos definidos en su catálogo, sin una sola excepción.

**En la dirección inversa hay un hueco chico y real.** Cinco `TC-XX` están definidos en su catálogo y **no tienen fila en ninguna tabla de su matriz de cobertura**: `TC-25` y `TC-27` de Domain, `TC-31` de Application, `TC-36` de Api y `TC-20` de Visor. Son pruebas de inspección estructural que trazan a una ADR o a un riesgo, no a un CU, una RN o un NFR —de modo que **no son verificaciones inventadas**—, pero dos matrices afirman explícitamente que ningún `TC-XX` deja de referenciar uno de esos cuatro conjuntos, y esa afirmación es falsa en su propio documento. `TC-20` de Visor es el caso más incómodo, porque es la prueba de `PT-02`.

**Los siete apartamientos de la pirámide están fundados y ninguno baja el piso.** Los siete repartos suman 100 y son reasignaciones: Application 100/0/0, Domain 90/10/0, Infrastructure 85/15/0, Contracts 0/60/40, Api 40/60/0, Visor 45/20/25/10, Web 0/0/100. Cinco de los siete motivos están **literalmente en el intake** y los verifiqué abriéndolo: §17.2.P.6 dice «pirámide del proyecto de código: 100 % unitarias», §17.4.P.6 dice «no tiene pruebas propias: son tipos sin comportamiento», §17.5.P.6 dice «60 % integración, 40 % unitarias», §17.6.P.6 dice que Web no tiene proyecto de pruebas propio, y §17.3.P.8 sostiene el nivel de integración interna de Infrastructure. Domain reasigna hacia arriba y lo declara. **El único cuyo reparto no está en el intake es el del Visor**, que lo funda en su propia categoría `02` §6 —y **lo dice**, sin atribuírselo al intake, que es la conducta correcta.

**Donde sí hay un piso que baja es en la cobertura de Api, y es el único lugar donde el apartamiento no está declarado.** La guía fija para `rest-api` «80 % aplicación, 70 % infraestructura»; Api fija 75/70 global. El número viene del intake y está rotulado, pero el documento **compara su pirámide con la guía y no compara su piso de cobertura con la guía**, mientras Domain, Application e Infrastructure sí hacen esa comparación explícita —«el piso sube, no hace falta la ADR que §2.2 exige para bajar cobertura»—. El único proyecto que efectivamente baja es el único que no lo menciona.

**Los quality gates condicionados están bien acotados en cinco proyectos y mal en dos.** El criterio se cumple donde debe: la tolerancia de **0.01 con operador estricto** es `QG-07` de Infrastructure y dice, con esas palabras, «**Bloquea la fusión, y no es condicionado**», citando §22; las puertas técnicas del Visor y de Web son bloqueantes y ninguna está condicionada; los umbrales de las puertas no están condicionados en ningún lado. Pero **Contracts `QG-05` y Web `QG-04` quedaron condicionados**, y los dos son la asunción `A-4` del intake §22, cuya columna «si el Product Owner la cambia» dice textualmente «**Cambia la forma del gate, no su carácter bloqueante**». La fuente declara expresamente que lo que está en duda es la forma y no el carácter; condicionarlos suspende justamente lo que la fuente puso a salvo. Web lo mitiga en prosa, Contracts no.

**Y hay un defecto de la clase que ya hizo caer a la Fase C, esta vez autoinfligido dentro del mismo commit.** `GeometriaFactory-Api` y `GeometriaFactory-Infrastructure` levantaron —bien— que el intake describía la batería del validador como de nueve casos cuando tiene diez, y **el intake 1.20 corrigió los cinco lugares en el commit `1d3bbeb`, que es el mismo en que se emitieron esos dos proyectos**. Pero los documentos quedaron escritos contra el texto viejo: **nueve pasajes** afirman en presente que el intake «escribe nueve pruebas del validador» en §17.3.P.8 y §17.5.P.8, y **dos de ellos son huecos abiertos con la remediación pendiente del Product Owner**. Al día de hoy el intake dice diez en los tres lugares. Son afirmaciones falsas sobre una fuente viva y **dos puntos abiertos falsos**, que es la única forma de punto abierto que esta auditoría considera hallazgo.

---

## 2. Cobertura, reconstruida con herramienta

### 2.1 Casos de uso

Conté los casos de uso sobre los archivos de `02-Especificacion-Funcional/Casos-De-Uso/` de cada proyecto de código, y las filas de la tabla `CU ↔ tests` de cada matriz con `grep -o '^| CU-[0-9]\+' | sort -u | wc -l`:

| Proyecto de código | CU en `02` | Filas en la matriz | ¿Cierra? |
| --- | --- | --- | --- |
| `GeometriaFactory-Domain` | 13 | 13 | Sí |
| `GeometriaFactory-Contracts` | 8 | 8 | Sí |
| `GeometriaFactory-Visor` | 7 | 7 | Sí |
| `GeometriaFactory-Application` | 11 | 11 | Sí |
| `GeometriaFactory-Web` | 10 | 10 | Sí |
| `GeometriaFactory-Infrastructure` | 10 | 10 | Sí |
| `GeometriaFactory-Api` | 12 | 12 | Sí |
| **Total** | **71** | **71** | **Sí** |

Ninguna fila agrupa dos casos de uso, ninguna celda de la columna de test está vacía, y ningún `CU-XX` de una matriz falta en su categoría 02. La numeración local y la previsión de nivel producto no se confunden en ningún documento: donde conviven, la previsión lleva prefijo o queda declarada aparte.

**Observación, fuera del alcance de esta fase.** El informe `D-06-07-Backlog-Siete-Proyectos-r1.md` afirma «los **67** casos de uso» con el mismo desglose 13, 8, 7, 11, 10, 10, 12, que suma **71**. La Fase E usa los recuentos por proyecto y todos son correctos; el 67 es un error de suma del informe de la Fase D y no contamina nada de lo auditado acá.

### 2.2 Mapeo inverso `TC → matriz`

Para cada proyecto de código extraje el conjunto de `TC-XX` **definidos** —los encabezados `#### TC-` del catálogo— y el conjunto de `TC-XX` **citados** en la matriz de cobertura, y los comparé en las dos direcciones.

| Proyecto de código | `TC` definidos | Citados en la matriz y no definidos | Definidos y sin fila en la matriz |
| --- | --- | --- | --- |
| `GeometriaFactory-Domain` | 27 | — | `TC-25`, `TC-27` |
| `GeometriaFactory-Contracts` | 22 | — | — |
| `GeometriaFactory-Visor` | 21 | — | `TC-20` |
| `GeometriaFactory-Application` | 31 | — | `TC-31` |
| `GeometriaFactory-Web` | 35 | — | — |
| `GeometriaFactory-Infrastructure` | 35 | — | — |
| `GeometriaFactory-Api` | 37 | — | `TC-36` |
| **Total** | **208** | **0** | **5** |

**La columna del medio es cero, y es la que más importa**: ningún caso de prueba inventa una verificación colgada de un identificador inexistente. La columna de la derecha es el hallazgo `H-04`.

### 2.3 Reglas, invariantes y necesidades

Las **dieciséis** reglas `RN-01` a `RN-16` de la categoría 02 de Domain tienen las dieciséis filas en su tabla §4, ninguna agrupada, y el reparto de la columna de invariante es **diez con invariante y seis sin él** —conté: con, `RN-01` a `RN-06`, `RN-10`, `RN-12`, `RN-13`, `RN-16`; sin, `RN-07`, `RN-08`, `RN-09`, `RN-11`, `RN-14`, `RN-15`—, que es exactamente lo que la matriz declara. Los **nueve** invariantes tienen las nueve filas de la cuarta tabla, cada uno con al menos una prueba de violación rechazada y la columna «usa dobles» en `No` en las nueve. Las **nueve** necesidades `NB-00001` a `NB-00009` existen en `01-Necesidades-Negocio/` y aparecen citadas en la cadena.

Un punto de honestidad que conviene destacar: la matriz de Domain declara que `RN-12`, `RN-13` y `RN-16` comparten `INV-09` «con la lectura que la categoría 02 adoptó», y agrega que **«no afirma que la prosa del intake la respalde»**. Eso es exactamente lo que hay que hacer con una lectura heredada de una fuente ambigua.

---

## 3. Los apartamientos deliberados

### 3.1 Las siete pirámides

| Proyecto de código | Tipo D8 | Reparto de la guía §2.2 | Reparto adoptado | Suma | ¿Motivo en el intake? |
| --- | --- | --- | --- | --- | --- |
| `-Domain` | `library` | 80 / 15 / 5 | 90 unit / 10 integ / 0 | 100 | Parcial: el 90 se apoya en §17.1.P.6 «pruebas unitarias puras y sin dobles»; la reasignación la funda la categoría y **el piso sube** |
| `-Application` | `library` | 80 / 15 / 5 | 100 / 0 / 0 | 100 | **Sí, literal**: §17.2.P.6 «pirámide del proyecto de código: 100 % unitarias; la integración vive en `GeometriaFactory.Integration.Tests`, que pertenece a la Api» |
| `-Infrastructure` | `library` | 80 / 15 / 5 | 85 / 15 / 0 | 100 | **Sí**: §17.3.P.8 declara la etapa de verificación de migraciones y el criterio de la etapa `c`. **El piso sube** |
| `-Contracts` | `library` | 80 / 15 / 5 | 0 / 60 / 40 inspección | 100 | **Sí, literal**: §17.4.P.6 «no tiene pruebas propias: son tipos sin comportamiento» |
| `-Api` | `rest-api` | 70 / 20 / 10 | 60 integ / 40 unit / 0 | 100 | **Sí, literal**: §17.5.P.6 «60 % integración, 40 % unitarias» con la palabra «invertida … a propósito» |
| `-Web` | `web-monolith` | 70 / 20 / 10 | 0 / 0 / 100 observado | 100 | **Sí**: §17.6.P.6 «no tiene proyecto de pruebas propio … su verificación es el guion de demostración de cada etapa» |
| `-Visor` | `library` | 80 / 15 / 5 | 45 / 20 / 25 / 10 | 100 | **No el reparto.** El intake §17.7.P.6 fija el gate de inspección en lugar de cobertura, y §17.7.P.8 exige `PT-02` en una página real; **el reparto numérico lo funda la categoría en su propio `02` §6**, y así lo declara |

**Ninguna de las siete baja el total ni descarta puntos: los siete repartos suman 100 y en todos los casos los puntos se reasignan.** Verifiqué el requisito de la guía —«los porcentajes son piso, no techo … no bajarla sin un ADR que lo justifique», `Rules-Calidad-Y-Pruebas.md` §2.2, abierto y leído— y confirmé que la frase habla de **cobertura**, no del reparto de la pirámide; las tres categorías que la citan la citan bien.

El caso del Visor merece una nota y no un hallazgo: su unitario baja de 80 a 45 y su extremo a extremo sube de 5 a 25, y el documento **no le atribuye ese reparto al intake**. Lo funda en que «cuatro de las seis propiedades transversales» de su `02` §6 exigen un bucle de dibujo corriendo, y en que sin página real no se miden ni los diez recorridos ni la liberación del contexto gráfico —lo cual es consistente con lo que §17.7.P.8 exige de `PT-02`—. Declarar el origen real de una decisión en vez de prestarle autoridad al intake es la conducta que esta auditoría premia.

### 3.2 Los gates condicionados

El criterio es el del intake §22: quedan condicionados los valores rotulados `[ASUNCIÓN]`, y **no** la tolerancia de 0.01, los 20 minutos y el semáforo de `PT-01`, los umbrales de las cinco puertas técnicas ni la regla acumulativa del guion, que §17.6.P.6 atribuye a RF §9.4.

**Lo que está bien, y lo verifiqué gate por gate sobre las siete tablas §3:**

- **La tolerancia de 0.01 con operador estricto** es `QG-07` de Infrastructure, y su columna de carácter dice: «**Bloquea la fusión, y no es condicionado.** El intake §22 declara expresamente que la tolerancia no es asunción». Abrí §22 y la enumera, en efecto, entre «lo que NO es asunción». Correcto.
- **Ningún umbral de puerta técnica está condicionado** en ningún proyecto de código. `QG-02` y `QG-03` del Visor —`PT-03` y `PT-02`— son «Bloqueante, y detiene la planificación de la etapa `g`». Los tres gates de puerta de Web son bloqueantes.
- Los condicionados de Domain (`QG-03`, `QG-07`), Application (`QG-03`, `QG-10`), Infrastructure (`QG-05`, `QG-06`, `QG-14`) y Api (`QG-03`, `QG-04`, `QG-13`, `QG-14`) corresponden **todos** a valores que el intake rotula `[ASUNCIÓN]` en §17.x.P.6 o §17.x.P.10, y las asunciones `A-3` y `A-5` de §22 dicen respectivamente «cambia el gate del pipeline» y «cambia lo que la categoría 08 verifique como NFR-tests». Condicionarlos está fundado.

**Lo que está mal:** Contracts `QG-05` y Web `QG-04` son la asunción `A-4`, cuya columna dice «**Cambia la forma del gate, no su carácter bloqueante**», y además §17.4.P.6 llama al de Contracts «el gate **equivalente y bloqueante**». Es el hallazgo `H-02`.

### 3.3 Mutation testing

El enunciado que recibí decía «se adopta en un solo proyecto». **No es así, y lo conté:** se adopta con umbral 60 % en **cuatro** —Domain `CV-19`, Application `CV-24`, Infrastructure `CV-30`, Api `CV-34`—, siempre como «no exigible todavía» porque la herramienta no está elegida, con el hueco declarado en la matriz de los cuatro; y se declara **no aplicable con motivo** en **dos**: Contracts `CV-25` («no hay lógica que mutar») y Visor `CV-33` («no hay forma de matar los mutantes del código de dibujo sin recurrir a la comparación de imágenes»). Web no lo menciona, y es correcto: la guía §2.2 sólo lo pide en la fila `library`.

Los cuatro que lo adoptan **separan bien la procedencia**: los cuatro dicen, con estas palabras o equivalentes, que «ninguna fuente del producto lo declara» y que el 60 % es piso de la regla de la categoría. Eso es exactamente lo que hay que hacer. La única imprecisión es que Api es `rest-api` y la fila `rest-api` de §2.2 no pide mutation score —hallazgo `H-08`, menor y en dirección de más rigor, no de menos.

---

## 4. Las puertas técnicas

Extraje todas las ocurrencias de `PT-0` en los 58 documentos: aparecen **`PT-01` (y sus cuatro partes `a` a `d`), `PT-02`, `PT-03`, `PT-04` y `PT-05`, y ninguna más**. No hay ninguna puerta inventada, ni ninguna renombrada, ni ningún identificador fuera del rango que §15 y §17.x.P.8 del intake declaran.

El reparto por proyecto de código es coherente con dónde el intake las ubica: Visor `PT-02` y `PT-03`; Web `PT-01`, `PT-02` y `PT-03`; Api `PT-04` y `PT-05`; Infrastructure `PT-04`; Application `PT-01` y `PT-05` como contexto; Domain y Contracts ninguna, que es correcto porque el intake no les asigna ninguna.

**Vinculantes y no condicionadas, comprobado en la columna de carácter:**

- Visor `QG-02` (`PT-03`) y `QG-03` (`PT-02`): «Bloqueante, y detiene la planificación de la etapa `g`». Es la consecuencia que el intake §15 declara —«una puerta que no pasa detiene la planificación de las etapas que dependen de ella y no se arrastra como deuda»—, y no la de un gate ordinario.
- Web: las tres puertas que lo alcanzan viven en §3.2 de su `Estrategia-Calidad.md`, aparte de los gates, con la misma consecuencia. Ninguna aparece en la tabla de condicionados.
- Api: `PT-04` y `PT-05` en §3.3, también aparte y con la misma consecuencia.

Hay un error de contenido sobre las puertas, no de identificador, y está en Web: `H-03`.

---

## 5. Los ocho escenarios como fixtures

**Los siete proyectos de código declaran los ocho escenarios como datos reales y ninguno los sustituye por datos sintéticos.** Los siete llevan un párrafo con la misma fórmula —«los datos de prueba de este producto son reales y no se sustituyen por datos sintéticos»— seguido de un apartado de **regeneración y versionado** que dice que los ocho no se regeneran y que «un fixture que cambie un valor de un escenario es un defecto, no una actualización». Domain lo convierte además en riesgo explícito de su plan (`RQ-06`: «que un escenario del intake §20 se sustituya por un dato sintético *porque es más cómodo de escribir*»), e Infrastructure lo pone entre lo que **no** es excepción admitida: «escribir a mano un texto de figuras porque el del intake es largo».

**Y cada capa declara cómo entra el escenario en ella, que era la parte delicada porque no es igual en todas.** Lo verifiqué en el §6 de las siete `Estrategia-Testing.md`:

| Proyecto de código | Cómo entra el escenario | Comprobado |
| --- | --- | --- |
| `-Infrastructure` | **Texto literal, carácter por carácter**, con las comas finales y las claves tal como están | Tabla de ocho filas en §6, con el estado declarado de cada uno —medido, derivado, reconstruido— y la fuente por punto de §20 |
| `-Visor` | **Texto**, porque `cargarJson` lo procesa: «los ocho escenarios entran acá como texto y no como resultado» | Tabla de §6 con los ocho, `E-3` y `E-4` en una fila común y declarada como tal |
| `-Web` | **Texto que la persona pega en el formulario de envío**, «que es exactamente la forma en que el alumno los produce» | Tabla de §6 |
| `-Api` | **Cuerpo de una petición**, «que es la forma en que llegan de verdad desde el front». Declara además qué **no** hace con ellos: no los interpreta, no los normaliza, no los trunca | §6, más la restricción de «0 datos de prueba inventados» de la colección de peticiones |
| `-Domain` | **Resultado de interpretación**, no texto: «el dominio no interpreta el texto del alumno … le llega ya reconstruida» | §6, con la tabla de conjuntos de piezas y observaciones |
| `-Application` | **Resultado de interpretación** por el puerto de validación de figuras, más la indisponibilidad del puerto | §6 |
| `-Contracts` | **Cuerpos de petición y respuesta**, derivados del sample S-2 y no copiados; el ensamblado transporta el texto como cadena sin interpretarlo | §6, con `E-5` identificado como el cuerpo que verifica índice y campo |

**La batería del validador cierra exactamente contra §21.** La tabla §6.1 de Infrastructure tiene diez filas, una por caso, y cada fila cita el mismo escenario que §21 le asigna: `E-2`, `E-2`, `E-3`, `E-4`, `E-3`, `E-2`+`E-1`, `E-6`, `E-5`, `E-1`, `E-8`. Las comparé fila por fila contra §21 del intake y **coinciden las diez**. `E-7` no respalda ninguno de los diez, se usa igual como cobertura adicional, y el documento declara que esa afirmación no es suya sino de `05` §10.5 —lo cual también verifiqué que dice.

---

## 6. La integración con la matriz de sensado de Web

**La matriz preexistente no está pisada ni duplicada, y lo verifiqué con `git`.** `GeometriaFactory-Web/08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md` sigue en **1.2**, fechada 2026-08-09, con autor AG-03M, y su último commit es `a2d5b22`, anterior a la Fase E: **no aparece en `git diff --name-only 7404030 HEAD`**. La Fase E no le tocó una fila. Sus **61** sondas siguen siendo 61, contadas de nuevo con `grep`.

**Lo que la Fase E hizo con ella está declarado y es lo que corresponde.** `Estrategia-Testing.md` §8 lista acción por acción qué hizo y qué no, y §8.1 resuelve el método de verificación por familia. **Las diez familias suman 61**: 11 + 11 + 5 + 9 + 6 + 6 + 5 + 3 + 2 + 3. Los sub-rangos son contiguos y sin solapamiento, de `SD-01` a `SD-61`.

**La verificación de la verificación.** El Visor emitió su propia `Matriz-Sensado-Deriva.md` con doce sondas y una tabla de **ocho correspondencias** contra la de Web para evitar doble sensado, y la Fase E de Web declaró en §8.2 que las ocho son verdaderas. **Abrí las ocho filas de Web citadas y las comparé una por una con lo que el Visor les atribuye:**

| Correspondencia del Visor | Fila de Web | Lo que Web dice de verdad | Veredicto |
| --- | --- | --- | --- |
| `SD-01` → `SD-43` | `SD-43` | «La escena se opera **exclusivamente por las seis funciones** de la fachada … y ningún guion del navegador llama al servicio de datos» | Verdadera |
| `SD-02` → `SD-43` | `SD-43` | La misma fila incluye el recuento de peticiones durante la interacción | Verdadera |
| `SD-03` → `SD-47` | `SD-47` | «La **preferencia de cada movimiento es del componente anfitrión**: la fachada no la conserva … y no escribe ninguna clave» | Verdadera |
| `SD-06` → `SD-39`, `SD-40` | `SD-39`, `SD-40` | `SD-39`: la pieza con `0.00` **se dibuja**. `SD-40`: el recuento de piezas sin registro es **0** | Verdadera |
| `SD-07` → `SD-41`, `SD-45` | `SD-41`, `SD-45` | `SD-41`: dos cargas del mismo texto, comparación pieza por pieza. `SD-45`: disposiciones en las **cuatro** combinaciones | Verdadera |
| `SD-09` → `SD-18` | `SD-18` | «Los **ocho** estados que materializan las **siete condiciones del contrato de fachada** … usan los códigos del contrato sin renombrarlos» | Verdadera |
| `SD-11` → `SD-44`, `SD-46`, `SD-48` | `SD-44`, `SD-46`, `SD-48` | Dos controles independientes; reposición de la orientación de partida; arranque destildado con preferencia de movimiento reducido | Verdadera |
| `SD-12` → `SD-42` | `SD-42` | «Diez recorridos de ida y vuelta **no degradan**» | Verdadera, y Web declara por su cuenta que es **parcial en alcance** |

**Las ocho correspondencias son verdaderas y la declaración de Web es cierta.** Más aún: la octava fila de §8.2 no se conforma con decir «verdadera», sino que señala por su cuenta que `SD-42` cubre sólo una mitad de lo que la sonda del Visor abarca. Eso es auditarse a sí mismo, y es la conducta correcta. **El problema es que identifica mal la mitad que falta**: `H-03`.

---

## 7. Veracidad de las afirmaciones sobre otras fuentes

Abrí todas las citas entrecomilladas atribuidas al intake. El resultado es bueno con dos excepciones y una imprecisión de estilo.

**Verificadas literales contra el intake 1.20:** «pruebas unitarias puras y sin dobles» (§17.1.P.6); «si una lo hace, está mal ubicada y pertenece a integración» (§17.2.P.8); «la persistencia real contra SQLite se prueba desde `GeometriaFactory.Integration.Tests`» (§17.3.P.6); «el validador de figuras no hace red» (§17.3.P.3); «no tiene pruebas propias: son tipos sin comportamiento» y «cobertura mínima: no aplica como gate propio» (§17.4.P.6); «porque lo que este proyecto de código aporta es cableado, y el cableado se verifica ejerciéndolo» (§17.5.P.6); «si en alguna etapa se agregan pruebas automatizadas de componentes, su cobertura mínima se fija en ese momento y se registra» (§17.6.P.6); «lo que NO es asunción» (§22, tres proyectos de código la citan y las tres veces dice lo que dicen que dice).

**Falsas contra el texto vivo:** las nueve ocurrencias de «el intake escribe *nueve* pruebas del validador» (`H-01`) y la del Visor sobre §21 (`H-05`).

**Imprecisión de estilo, no de contenido:** tres citas entrecomilladas omiten palabras **dentro de las comillas** sin elipsis, siempre para sacar un nombre de tecnología que el corpus evita: «una subida … que deja la aplicación caída» pierde «por FTP» (§17.6.P.8); «cero ocurrencias de **las tres formas de petición**» reemplaza los tres nombres literales (§17.7.P.6); «interacción fluida al rotar y acercar» pierde «con el mouse» (§17.7.P.10). El sentido se conserva en las tres. Es `H-09`.

---

## 8. Recuentos, contados de nuevo

| Conjunto | Esperado | Contado por mí | Cómo |
| --- | --- | --- | --- |
| Documentos nuevos de la Fase E | — | **58** | `git diff --name-only 7404030 HEAD`, restando el intake y su copia archivada |
| Casos de prueba | 208 | **208** | Suma de `grep -c '^#### TC-'` sobre los siete catálogos: 27 + 22 + 21 + 31 + 35 + 35 + 37 |
| Criterios de validación | 219 | **219** | Suma de `CV-XX` únicos: 22 + 25 + 34 + 28 + 35 + 35 + 40 |
| Reglas de negocio | 16 | **16** | `RN-01` a `RN-16` en `02` de Domain; 16 filas en la matriz |
| Invariantes | 9 | **9** | `INV-01` a `INV-09`; 9 filas en la cuarta tabla de la matriz de Domain |
| Escenarios | 8 | **8** | `E-1` a `E-8` en §20; los siete proyectos de código dicen «ocho» |
| Necesidades de negocio | 9 | **9** | `NB-00001` a `NB-00009` en `01-Necesidades-Negocio/` |
| Códigos de contrato | 15 vivos / 18 emitidos | **15 / 18** | `CV-05` y `CV-12` de Contracts; coincide con `03` |
| Puntos de acceso | 15 | **15** | `CV-02` de Api y su tabla §5 |
| Funciones de fachada | 6 | **6** | `CV-02` del Visor, `QG-06`, `SD-01` y `SD-43` de Web |
| Batería del validador | 10 | **10** | Diez filas en §6.1 de Infrastructure, contra las diez de §21 |
| Casos de uso | 71 | **71** | Archivos de `Casos-De-Uso/` y filas de las siete matrices |

**Todos los recuentos cierran.** No encontré un solo conjunto cerrado con un número viejo dentro de los documentos de la Fase E, que era el defecto que las versiones 1.16 a 1.20 del intake fueron corrigiendo una por una.

---

## 9. Forma

Comprobado con herramienta sobre los **59** documentos de las siete carpetas `08-Calidad-Y-Pruebas/` —los 58 de esta fase más la matriz de sensado preexistente de Web:

- **Versiones y estado.** Los 58 llevan `Versión: 1.0` y `Estado: Propuesto` en la cabecera. El único documento de la categoría con otra versión es la matriz de sensado de Web, que es **1.2** y de la Fase B2, y su README lo declara.
- **Control de cambios.** Los 58 tienen exactamente **una** sección de control de cambios, con su fila de emisión inicial fechada 2026-08-11.
- **Filas con tantas celdas como columnas.** Recorrí todas las tablas de los 58 documentos comparando el número de celdas de cada fila con el de su encabezado, descontando los `|` escapados. **Cero filas desparejas.**
- **Enlaces relativos.** Resolví todos los enlaces relativos de los 58 documentos contra el sistema de archivos. **Cero enlaces rotos.**

Un defecto de forma está en el intake y no en la Fase E, pero lo introdujo el mismo commit: `H-06` y `H-07`.

---

## 10. Hallazgos

### P0

Ninguno. No encontré un caso de uso, una regla, un invariante, una condición de error ni un criterio de aceptación sin caso de prueba, ni un caso de prueba que invente una verificación que ninguna fuente pida.

### P1

---

**`H-01` — Nueve pasajes afirman en presente que el intake dice «nueve pruebas del validador», y el intake 1.20 dice «diez» desde el mismo commit. Dos de ellos son puntos abiertos falsos.**

**Dónde está.**

| Documento | Línea |
| --- | --- |
| `Proyectos/GeometriaFactory-Api/08-Calidad-Y-Pruebas/Estrategia-Calidad.md` | 89, §3.2 |
| `Proyectos/GeometriaFactory-Api/08-Calidad-Y-Pruebas/Criterios-Validacion.md` | 115 |
| `Proyectos/GeometriaFactory-Api/08-Calidad-Y-Pruebas/Matriz-Cobertura-Pruebas.md` | 186, §8 **(hueco abierto)** |
| `Proyectos/GeometriaFactory-Api/08-Calidad-Y-Pruebas/Plan-Pruebas.md` | 87, riesgo `RQ-11` |
| `Proyectos/GeometriaFactory-Api/08-Calidad-Y-Pruebas/Definition-Of-Done.md` | 97 |
| `Proyectos/GeometriaFactory-Infrastructure/08-Calidad-Y-Pruebas/Criterios-Validacion.md` | 109 |
| `Proyectos/GeometriaFactory-Infrastructure/08-Calidad-Y-Pruebas/Matriz-Cobertura-Pruebas.md` | 172, §7 **(hueco abierto)** |
| `Proyectos/GeometriaFactory-Infrastructure/08-Calidad-Y-Pruebas/Plan-Pruebas.md` | 81, riesgo `RQ-10` |
| `Proyectos/GeometriaFactory-Infrastructure/08-Calidad-Y-Pruebas/Definition-Of-Done.md` | 90 |

**Qué dice.** «El intake §17.5.P.8 declara que el guion de pruebas de este proyecto de código pasa **«incluidas las nueve pruebas del validador»**» (Api, `Estrategia-Calidad.md` §3.2). «El intake escribe «las **nueve** pruebas del validador» en §17.3.P.8 y en §17.5.P.8» (Infrastructure, `Criterios-Validacion.md`). Y, como hueco con plan de remediación: «**El intake escribe «nueve pruebas del validador» en dos gates** —§17.3.P.8 y §17.5.P.8— **y la batería tiene diez** … Plan de remediación: **El Product Owner sobre su propio documento**».

**Qué debería decir.** El intake **1.20**, subido en el commit `1d3bbeb` —**el mismo commit que emitió estos dos proyectos de código**—, dice hoy: §17.3.P.8, «las **diez** pruebas del validador pasan»; §17.5.P.8, «`scripts/test.sh` pasa entero, incluidas las **diez** pruebas del validador»; §17.3.P.6, «la **batería obligatoria de diez casos**». Los nueve pasajes deberían estar en pasado y citando la versión —«**hasta 1.19** el intake escribía nueve»—, y los dos huecos de las matrices deberían estar **cerrados**, no abiertos con remediación pendiente del Product Owner: la fuente ya lo hizo. Un punto abierto que la fuente ya resolvió es la única clase de punto abierto que esta auditoría cuenta como hallazgo.

**Cómo lo verifiqué.** `git diff 7404030 HEAD -- SDD/Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md` muestra las cinco sustituciones de «nueve» por «diez» dentro de `1d3bbeb`; después leí §17.3.P.6, §17.3.P.8, §17.5.P.8 y §21 en el texto vivo y los cuatro dicen diez. Después grepeé «nueve» sobre los 58 documentos de la fase y ordené las ocurrencias por documento.

**Nota que juega a favor de la emisión y no la exime.** El fondo del asunto está **bien resuelto**: los dos proyectos de código aplicaron **diez** y no bajaron la batería a nueve para que coincidiera con la redacción del gate, y las §6.1, `QG-03` y `CV-02` correspondientes están escritas con diez. Lo que quedó mal es la descripción del estado de la fuente. Es exactamente el defecto por el que se rechazó la Fase C.

---

**`H-02` — Dos gates de la asunción `A-4` quedaron condicionados, y §22 declara expresamente que esa asunción no pone en duda su carácter bloqueante.**

**Dónde está.** `Proyectos/GeometriaFactory-Contracts/08-Calidad-Y-Pruebas/Estrategia-Calidad.md` línea 61 (`QG-05`) y §3.1 línea 69. `Proyectos/GeometriaFactory-Web/08-Calidad-Y-Pruebas/Estrategia-Calidad.md` línea 63 (`QG-04`) y §3.1 línea 76.

**Qué dice.** Contracts `QG-05`: «**100 %** de los tipos de transferencia ejercitados por al menos una prueba de integración **[ASUNCIÓN del intake §17.4.P.6]** … **Condicionado**», y §3.1 agrega que «la puerta **no se declara bloqueante** en `09-Devops` hasta que el Product Owner los confirme». Web `QG-04`: «**100 %** de los pasos del guion de demostración de la etapa **y de todas las anteriores** … **Condicionado**».

**Qué debería decir.** Bloqueante. El intake §22 fila `A-4`, columna «Si el Product Owner la cambia», dice textualmente: «**Cambia la forma del gate, no su carácter bloqueante**». Y §17.4.P.6 dice del de Contracts: «el gate **equivalente y bloqueante** es que el 100 % de los DTOs esté ejercitado por al menos una prueba de integración». Lo que `A-4` deja abierto es **cómo se expresa** la puerta, no si detiene. Los dos gates deberían quedar **bloqueantes con la forma sujeta a confirmación**, que es la lectura fiel de la fuente.

**Cómo lo verifiqué.** Extraje la columna de carácter de los **77** `QG-XX` de los siete proyectos de código —15, 11, 9, 8, 14, 9 y 11— con `awk` y ordené los condicionados; después crucé cada condicionado con la asunción de §22 que lo respalda. Los de `A-3` y `A-5` están bien fundados —sus columnas dicen «cambia el gate del pipeline» y «cambia lo que la categoría 08 verifique»—; los dos de `A-4` no.

**Atenuante para Web, no para Contracts.** El §3.1 de Web declara por su cuenta que «**la regla acumulativa es de la fuente y no está en duda**» y que condicionar «en particular no habilita a ejecutar el guion de la etapa sin los de las anteriores». Es una salvaguarda real y bien escrita, pero está en la prosa y no en la columna que `09-Devops` va a leer. Contracts no tiene ni siquiera eso.

### P2

---

**`H-03` — Web atribuye a `PT-02` el contenido de `PT-03` en el párrafo que certifica la correspondencia con el Visor.**

**Dónde está.** `Proyectos/GeometriaFactory-Web/08-Calidad-Y-Pruebas/Estrategia-Testing.md` línea 198, §8.2, última fila.

**Qué dice.** «**Lo que esa fila no cubre es la otra mitad de `PT-02`** —que el motor de dibujo quede **dentro** del bundle, sin acceso a redes externas—».

**Qué debería decir.** «la otra mitad de lo que la sonda `SD-12` del Visor abarca, que es **`PT-03`**». El intake §17.7.P.8 define **`PT-03`** como «Three.js dentro del bundle, la página funciona sin acceso a CDN», y **`PT-02`** como «el bundle carga en una página Blazor Interactive Server, `inicializar` crea la escena, `cargarJson` dibuja las tres figuras de E-1 incluido el ortoedro, navegar y volver 10 veces no degrada, y el árbol y la escena se sincronizan por índice». Lo que Web llama «la otra mitad de `PT-02`» es literalmente la definición de `PT-03`.

**Cómo lo verifiqué.** Abrí §17.7.P.8 del intake y leí las dos definiciones; después leí la fila `SD-12` de la matriz del Visor, que dice correctamente «Las dos puertas técnicas `PT-02` y `PT-03`», y la fila de §8.2 de Web que la comenta.

**Por qué P2 y no P3.** El párrafo no es decorativo: es el que dictamina que la correspondencia es verdadera y que no hay doble sensado. Un error de identificación de puerta dentro de ese párrafo puede llevar a que alguien busque el sensado de `PT-03` donde no está.

---

**`H-04` — Cinco casos de prueba no tienen fila en ninguna tabla de su matriz de cobertura, y dos matrices afirman lo contrario.**

**Dónde está.** `TC-25` y `TC-27` de `Proyectos/GeometriaFactory-Domain/08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md`; `TC-31` de Application; `TC-36` de Api; `TC-20` de Visor. Las afirmaciones falsas están en `GeometriaFactory-Domain/08-Calidad-Y-Pruebas/Matriz-Cobertura-Pruebas.md` línea 57 y `GeometriaFactory-Application/08-Calidad-Y-Pruebas/Matriz-Cobertura-Pruebas.md` línea 56.

**Qué dice.** Domain: «Ninguno queda huérfano y **ningún `TC-XX` deja de referenciar un `CU-XX`, un `RN-XX`, un `INV-XX` o un NFR**». Application: la misma frase con «una comprobación» agregada al conjunto.

**Qué debería decir.** `TC-25` de Domain declara en su campo «Cubre» a `ADR-06`, `BT-09` y `05` §7; `TC-27`, a `ADR-02`, `BT-07` y `QG-08`. Ninguno de los dos referencia un `CU`, una `RN`, un `INV` ni un NFR, y ninguno tiene fila en las cuatro tablas de la matriz. Lo mismo `TC-31` de Application (`QG-11`, `ADR-06`, quinto riesgo de `05` §9) y `TC-36` de Api (`RA-01`, sexto riesgo de `05` §9). El caso más sensible es **`TC-20` del Visor, que es la prueba de `PT-02`** y trazá a `US-01`, `US-04`, `US-09`, `US-11`, `QG-03` y `BT-14`, y sin embargo no aparece en ninguna de las tablas de su matriz. Las dos frases deberían decir que hay un pequeño conjunto de pruebas de inspección estructural cuya trazabilidad es hacia ADR, riesgo o gate, y la matriz debería llevar una tabla más —o una columna— que las recoja, como Domain hizo con los invariantes y Api con los puntos de acceso.

**Cómo lo verifiqué.** Para cada proyecto de código extraje el conjunto de `TC-XX` definidos con `grep -o '^#### TC-[0-9]\+'` y el conjunto citado en la matriz con `grep -o 'TC-[0-9]\+'`, y los comparé con `comm` en las dos direcciones. Después abrí los cinco `TC-XX` sobrantes y leí su campo «Cubre».

**Por qué no es P1.** Ninguna verificación se pierde: los cinco están escritos, con setup, pasos y salida esperada, y los cinco están amarrados a un gate o a una ADR. Lo que falla es el instrumento de trazabilidad y la frase que declara su completitud.

---

**`H-05` — El Visor afirma que §21 del intake cruza «la batería obligatoria de nueve casos», y §21 dice diez.**

**Dónde está.** `Proyectos/GeometriaFactory-Visor/08-Calidad-Y-Pruebas/Estrategia-Testing.md` línea 104, §6.

**Qué dice.** «§21 los cruza contra la batería obligatoria de **nueve** casos de prueba y declara, en su tabla de cobertura de invariantes, que el contrato de fachada tiene sus siete condiciones con escenario en `E-1` a `E-8`».

**Qué debería decir.** Diez. La tabla de §21 tiene diez filas desde que el intake 1.7 incorporó `E-8` el 2026-08-09; lo único que decía nueve era el **encabezado** de esa sección, y el intake 1.20 lo corrigió. La segunda mitad de la frase —las siete condiciones con escenario en `E-1` a `E-8`— sí es verdadera y la verifiqué en §21.

**Cómo lo verifiqué.** Conté las filas de la tabla de §21 del intake: son diez. Después leí el encabezado de §21 en 1.20, que hoy dice «los **nueve** casos de RT §11 más el **décimo** … **diez** en total».

**Atenuante.** El Visor se emitió en la ola 1 (`93018ed`), antes de que la ola 2 levantara el defecto. Copió el encabezado de §21 tal como estaba. Pero la Fase E entera se dictamina contra el estado del repositorio en `HEAD`, y en `HEAD` la afirmación es falsa: la ola 2 corrigió la fuente y no propagó hacia atrás.

---

**`H-06` — Api baja el piso de cobertura de la guía y es el único proyecto de código que no declara la comparación.**

**Dónde está.** `Proyectos/GeometriaFactory-Api/08-Calidad-Y-Pruebas/Estrategia-Testing.md` §1 y §2, líneas 35 a 65.

**Qué dice.** §1 compara la pirámide con la guía —«`Rules-Calidad-Y-Pruebas.md` §2.2 fija para el tipo `rest-api` la distribución 70/20/10 … este proyecto de código se aparta»— y §2 fija el piso global en «**75 % de líneas y 70 % de ramas** [ASUNCIÓN del intake §22, asunción `A-3`]», sin comparar ese número con la guía.

**Qué debería decir.** La guía §2.2, fila `rest-api`, exige «**80 % aplicación**, 70 % infraestructura, 100 % de endpoints cubiertos por contract test», y agrega: «Los porcentajes son piso, no techo … pero no bajarla sin un **ADR** que lo justifique». 75 < 80. El documento debería decir que el piso **baja** respecto de la guía, con qué autoridad —el intake— y si eso exige o no la ADR de §2.2. Domain, Application e Infrastructure hacen precisamente esa comparación y concluyen «el piso **sube** … no hace falta la ADR que §2.2 exige para bajar cobertura». El único proyecto de código que efectivamente baja es el único que no la hace.

**Cómo lo verifiqué.** Abrí `Rules-Calidad-Y-Pruebas.md` §2.2 en el repositorio de origen y leí la fila `rest-api` y la frase del piso; después leí §1 y §2 de la `Estrategia-Testing.md` de Api completas buscando la comparación, que no está.

**Atenuante real.** La caída del piso global está compensada componente por componente: la guardia de admisión y el traductor van a 95/90, la superficie de acceso y la de trabajos a 80/75, el arranque a 85/80, y los cuatro conjuntos contables —15 puntos, 15 códigos, 4 puertos, 4 puntos fuera de la guardia— no admiten promedio. La sustancia está bien; lo que falta es decir que se bajó un piso de la guía.

### P3

---

**`H-07` — La fila 1.20 del control de cambios del intake ubica mal uno de los cinco lugares que corrigió, y la tabla quedó desordenada.**

**Dónde está.** `SDD/Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`, sección «Control de cambios», fila 1.20.

**Qué dice.** «**cinco lugares** siguieron diciendo nueve: los dos quality gates de §17.3.P.8 y §17.5.P.8, la forma de verificación de §17.3.P.6, **el fundamento del puerto en §17.3.P.4** y el encabezado de la propia §21».

**Qué debería decir.** «§17.**2**.P.**11**». El `git diff` del commit `1d3bbeb` muestra que el quinto cambio se aplicó bajo el encabezado `### §17.2.P.11 Decisiones técnicas pre-tomadas (pre-ADR)`, en la frase «el validador de figuras es un puerto, no una dependencia concreta … es lo que permite probar los **diez** casos de la batería». §17.3.P.4 es «Persistencia» de Infrastructure y no se tocó. Además, la fila 1.20 quedó insertada **después** de la fila 1.0, de modo que la tabla lee 1.0, 1.20, 1.19, 1.18…; debería ir primera.

**Cómo lo verifiqué.** `git diff 7404030 HEAD` sobre el intake: los cinco *hunks* llevan sus encabezados de contexto, y el del puerto dice §17.2.P.11.

---

**`H-08` — Contracts atribuye `QG-06` a la asunción `A-4` de §22, que no lo enumera; y Api atribuye a la guía un piso de mutación que la guía sólo fija para `library`.**

**Dónde está.** `Proyectos/GeometriaFactory-Contracts/08-Calidad-Y-Pruebas/Estrategia-Calidad.md` §3.1 línea 69; `Proyectos/GeometriaFactory-Api/08-Calidad-Y-Pruebas/Estrategia-Testing.md` línea 65 y `Criterios-Validacion.md` `CV-34`.

**Qué dice.** Contracts: «`QG-05` y `QG-06` son **los dos gates** cuyo umbral es un valor rotulado **[ASUNCIÓN]** en el intake §22, **asunción `A-4`**». Api: el mutation score de 60 % «es el piso que `Rules-Calidad-Y-Pruebas.md` §2.2 fija».

**Qué debería decir.** `A-4` enumera «100 % de DTOs ejercitados, 100 % de pasos de guion, cero llamadas de red» y **no** menciona la proyección de listado; el respaldo real de `QG-06` es §17.4.P.10, «[ASUNCIÓN **derivada de RT §7.2**]», y la propia celda del gate lo cita bien —es sólo §3.1 la que lo mete en `A-4`—. Api debería decir «el piso que §2.2 fija **para el tipo `library`**», porque la fila `rest-api` de esa tabla no pide mutation score; adoptarlo igual es más rigor, no menos, pero la atribución es incorrecta.

**Cómo lo verifiqué.** Leí la fila `A-4` de §22 y §17.4.P.10 del intake, y las ocho filas de §2.2 de la guía.

---

**`H-09` — Tres citas entrecomilladas del intake omiten palabras dentro de las comillas sin elipsis.**

**Dónde está.** `GeometriaFactory-Web/08-Calidad-Y-Pruebas/Estrategia-Calidad.md` `QG-03`; `GeometriaFactory-Visor/08-Calidad-Y-Pruebas/Estrategia-Testing.md` §2 y `Estrategia-Calidad.md`.

**Qué dice.** «una subida que deja la aplicación caída y se reporta como exitosa es peor que una falla visible»; «cero ocurrencias de **las tres formas de petición** en el código fuente y en el bundle generado»; «interacción fluida al rotar y acercar, sin tráfico de circuito durante el gesto».

**Qué debería decir.** El intake dice «una subida **por FTP** que deja…» (§17.6.P.8), «cero ocurrencias de `fetch`, `XMLHttpRequest` y `WebSocket` en el código fuente **de `visor/`** y en el bundle generado» (§17.7.P.6) e «interacción fluida al rotar y acercar **con el mouse**, sin tráfico…» (§17.7.P.10). El sentido se conserva en las tres y la sustitución responde a la convención del corpus de no nombrar tecnologías; pero una comilla angular promete literalidad. Corresponde parafrasear sin comillas o marcar la elipsis.

**Cómo lo verifiqué.** Extraje todas las citas entrecomilladas atribuidas al intake con `grep -o` y abrí las secciones citadas una por una.

---

## 11. Lo que no pude verificar

- **Las tres fuentes originales —RF, RT y AN— viven en otro repositorio bajo `PROMPTs/`, y no las abrí.** Toda afirmación de la Fase E que se apoye en «RT §11», «RF §9.4», «AN §9.3» o «RT §12» la verifiqué **sólo hasta el intake**, que es el eslabón que sí tengo. Si el intake transcribió mal alguna de ellas, esta auditoría no lo detecta. Queda **no verificado**.
- **Los umbrales de rendimiento** —500 ms, 200 ms, p99 de 500 ms, 20 peticiones por minuto, 30 s de arranque, 10 s de la batería— son asunciones declaradas del intake y no tengo forma de juzgar si son adecuados al uso previsto. Verifiqué que estén rotulados y condicionados correctamente, no que sean razonables. **No verificado.**
- **Si las 61 filas de la matriz de sensado de Web siguen describiendo la línea de base visual vigente** es materia de la auditoría de la Fase B2, ya hecha. Acá sólo verifiqué que la Fase E no las tocó y que las ocho correspondencias del Visor son ciertas.
- **La adecuación del reparto 45/20/25/10 del Visor** en tanto reparto de esfuerzo: verifiqué que suma 100, que no descarta niveles y que su motivo está declarado con su origen real, no que 45 sea el número correcto. Ninguna fuente da un número contra el cual contrastarlo. **No verificado.**

---

## 12. Dictamen

# RECHAZADO

**Fundamento.** No hay ningún P0: la cobertura —que es lo propio de esta fase— está completa, la reconstruí con herramienta en las dos direcciones y cierra. Los 71 casos de uso, las 16 reglas, los 9 invariantes, los 8 escenarios, los 15 códigos, los 15 puntos, las 6 funciones y los 10 casos de la batería tienen todos su caso de prueba, y **ningún caso de prueba inventa una verificación que ninguna fuente pida**. Los siete apartamientos de la pirámide están fundados, cinco de ellos con texto literal del intake que abrí y comparé. Las puertas técnicas son exactamente las cinco del intake, sin una sola inventada, y las del visor y las de Web son vinculantes y no condicionadas. Los ocho escenarios son datos reales en las siete capas y cada capa declara cómo entran en ella. La matriz de sensado de Web no está pisada ni duplicada, y las ocho correspondencias que el Visor declaró contra ella son verdaderas: las verifiqué fila por fila abriendo las filas de Web.

**Y sin embargo se rechaza, por dos motivos que no admiten pasar.**

El primero es `H-01`. La Fase C de este mismo producto se rechazó por **dos** citas de un texto del intake que ya no existía. Acá hay **nueve** pasajes que describen el estado presente de una fuente viva de manera falsa, y **dos de ellos son puntos abiertos con remediación asignada al Product Owner sobre algo que el Product Owner ya resolvió** —en el mismo commit, tres párrafos más abajo—. Que el fondo esté bien resuelto no lo salva: un lector de `09-Devops` que lea el hueco de la matriz de Infrastructure va a creer que el intake sigue diciendo nueve, y ni siquiera va a ir a mirar. Es el defecto característico de este producto y la corrección es mecánica.

El segundo es `H-02`. Dos gates quedaron condicionados contra el texto expreso de la fuente que los gobierna: §22 dice que un cambio del Product Owner sobre `A-4` «cambia la forma del gate, **no su carácter bloqueante**», y §17.4.P.6 llama al de Contracts «equivalente y **bloqueante**». Condicionar es exactamente suspender el carácter que la fuente puso a salvo. Esto no es una imprecisión de redacción: es la diferencia entre que el 100 % de los pasos del guion acumulativo detenga una etapa o no la detenga, en el único proyecto de código del producto que **no tiene ninguna otra red de seguridad automatizada**.

**Qué hace falta para levantar el rechazo.** Poner los nueve pasajes de `H-01` en pasado con su número de versión y **cerrar los dos huecos**; devolver a bloqueante los dos gates de `H-02`, con la forma —y sólo la forma— sujeta a confirmación; corregir la puerta en `H-03`; recoger los cinco `TC-XX` de `H-04` en la matriz y ajustar las dos frases; actualizar el «nueve» del Visor en `H-05`; y declarar en Api la comparación de piso de `H-06`. Los tres P3 pueden ir en la misma pasada. **Ninguna de las seis correcciones cambia una decisión de prueba, un umbral ni un caso**: las seis son de redacción y de trazabilidad, y el cuerpo de la fase queda intacto.

---

## 13. ¿Alcanza esta estrategia para confiar en el producto?

Sí, con una reserva nombrada. Lo que más me convence no son los 208 casos ni los 219 criterios, sino **qué eligieron verificar**. Este producto tiene un riesgo dominante y la estrategia lo enfrenta de frente: que el validador se escriba sin leer el dato que los alumnos producen de verdad. Contra eso, los ocho escenarios entran como **datos reales en las siete capas**, cada capa declara la forma exacta en que entran —texto literal, resultado de interpretación, cuerpo de petición, texto pegado en un formulario—, ningún proyecto de código se permite un dato sintético de geometría, y dos de ellos convierten esa prohibición en riesgo de plan con nombre y en excepción no admitida. La batería de diez casos cierra contra §21 fila por fila, y los dos proyectos que descubrieron que la fuente decía nueve **aplicaron diez** en lugar de bajar la batería para que la redacción cerrara. Esa es la decisión que separa una estrategia de prueba de un trámite.

Lo segundo que convence es que la estrategia sabe dónde no puede confiar en sí misma. Los proyectos de código sin cobertura de líneas —Contracts, Web, Visor— no se conforman con declarar que su gate es de otra forma: lo reemplazan por **conjuntos cerrados con umbral exacto que se cuentan y no se opinan**: 15 puntos de acceso, 15 códigos, 6 funciones, 4 puertos, 0 peticiones de red, 0 claves escritas, 42 y 36 y 17 condiciones alcanzadas en las dos direcciones. Web, que es el único sin batería automatizada y por decisión de la fuente, dice sin adornos que un guion observado es más caro y menos reproducible, y lo compensa con 61 sondas enumerables, 35 casos y seis inspecciones de umbral cero «que no dependen de que alguien mire bien». Api invierte su pirámide y dedica un párrafo a decir **qué se paga** por invertirla. Nadie declaró una cobertura verde: las 208 salidas observadas dicen «Sin ejecutar» y los 208 estados dicen `Pendiente`, porque no hay código, y afirmar otra cosa habría sido una afirmación sin evidencia.

La reserva es la de siempre en este producto, y no es de la Fase E sino de su ecología: **la estrategia es más confiable que la descripción que hace de sus propias fuentes**. Por cuarta vez consecutiva el defecto encontrado no está en lo que se decidió sino en cómo se contó lo que otro documento dice, y esta vez ocurrió dentro del mismo commit que corrigió esa fuente. Mientras el corpus siga creciendo por transcripción de recuentos y de citas, cada fase va a heredar el mismo hallazgo. La medida que lo cortaría no es más prosa: es que las afirmaciones sobre otras fuentes lleven **siempre la versión de la fuente citada**, como ya hacen las cabeceras de trazabilidad upstream de estos mismos documentos y no hacen sus cuerpos. Con esa disciplina, y con las seis correcciones de §12 aplicadas, esta estrategia de pruebas alcanza de sobra para confiar en el producto que se va a construir.

---

## 14. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Ronda 1 de la auditoría de la Fase E, categoría `08-Calidad-Y-Pruebas` de los siete proyectos de código, contra el intake **1.20** y `Rules-Calidad-Y-Pruebas.md`. Verifica la cobertura reconstruyendo el mapeo inverso `TC → matriz` con herramienta en las dos direcciones sobre los **208** casos de prueba, recuenta los doce conjuntos cerrados, dictamina los siete apartamientos de la pirámide y los gates condicionados contra §22, comprueba que las puertas técnicas son las cinco del intake y ninguna más, verifica las ocho correspondencias del Visor abriendo las filas de la matriz de sensado de Web, y comprueba forma —celdas, enlaces, versiones y control de cambios— con herramienta sobre los 58 documentos. **Nueve hallazgos: cero P0, dos P1, cuatro P2 y tres P3.** Dictamen **RECHAZADO**, por afirmaciones falsas sobre el texto vivo del intake en nueve pasajes con dos puntos abiertos falsos, y por dos gates condicionados contra el texto expreso de la asunción que los gobierna. |
