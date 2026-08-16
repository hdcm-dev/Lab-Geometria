# 02 · Especificación funcional — GeometriaFactory-Infrastructure

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** README.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`Especificacion-Funcional.md`](Especificacion-Funcional.md) (índice maestro de esta categoría); `01-Necesidades-Negocio/Necesidades-Negocio.md`; `00-Contexto/Vision-Producto.md`; `Proyectos/GeometriaFactory-Application/02-Especificacion-Funcional/` y `Proyectos/GeometriaFactory-Domain/02-Especificacion-Funcional/`
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas` y `09-Devops` de GeometriaFactory-Infrastructure

---

## Tabla de contenido

- [1. Qué hay en esta carpeta](#1-qué-hay-en-esta-carpeta)
- [2. Los diez casos de uso](#2-los-diez-casos-de-uso)
- [3. Las siete reglas conceptuales de modelo](#3-las-siete-reglas-conceptuales-de-modelo)
- [4. Orden de lectura sugerido](#4-orden-de-lectura-sugerido)
- [5. Artefactos omitidos y el que se emite contra la guía del tipo](#5-artefactos-omitidos-y-el-que-se-emite-contra-la-guía-del-tipo)
- [6. Notas de uso de esta sección](#6-notas-de-uso-de-esta-sección)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Qué hay en esta carpeta

| Documento | Propósito | Estado |
| --- | --- | --- |
| [`Especificacion-Funcional.md`](Especificacion-Funcional.md) | Índice maestro: catálogo, los cuatro puertos y los dos mecanismos, la frontera entre mecanismo y decisión, la matriz NB → CU → RN → US, el criterio de recorte, las omisiones y los diez puntos abiertos. **Es el punto de entrada** | Propuesto |
| [`Definicion-Contrato-Del-Validador-De-Figuras.md`](Definicion-Contrato-Del-Validador-De-Figuras.md) | Documento de concepto central: las cuatro trampas del formato, las siete garantías, los tipos que reconstruye, los ocho escenarios y la cobertura de la batería obligatoria de nueve casos | Propuesto |
| [`Modelo-Datos/Modelo-Conceptual.md`](Modelo-Datos/Modelo-Conceptual.md) | Las cinco entidades, sus atributos, las cuatro relaciones, los cuatro conjuntos cerrados y las nueve decisiones de almacenamiento | Propuesto |
| `Modelo-Datos/reglas-conceptuales-de-modelo/` | Siete reglas conceptuales de modelo, una por archivo | Propuesto |
| [`Glosario-Funcional.md`](Glosario-Funcional.md) | Vocabulario que esta categoría acuña y los cuatro términos con más de un referente | Propuesto |
| `Casos-De-Uso/` | Diez casos de uso, uno por archivo | Propuesto |
| `README.md` | Este archivo: índice navegable, orden de lectura y omisiones | Propuesto |

No hay carpeta `_legacy/`: es la emisión inicial de la categoría para este proyecto de código.

## 2. Los diez casos de uso

| CU | Nombre | En una línea |
| --- | --- | --- |
| CU-01 | [Interpretar el texto original y reconstruir las piezas](../../Casos-De-Uso/CU-06001-Interpretar-El-Texto-Original-Y-Reconstruir-Las-Piezas.md) | La lectura tolerante del dato real del alumno, con la posición y el campo de cada defecto |
| CU-02 | [Verificar los valores declarados contra los derivados](../../Casos-De-Uso/CU-06002-Verificar-Los-Valores-Declarados-Contra-Los-Derivados.md) | Señalar sin corregir ni rechazar, con tolerancia y operador estricto |
| CU-03 | [Guardar y recuperar los trabajos](../../Casos-De-Uso/CU-06003-Guardar-Y-Recuperar-Los-Trabajos.md) | El texto original conservado literal y la consulta que llega ya acotada |
| CU-04 | [Ejecutar el borrado físico y el arrastre de la baja](../../Casos-De-Uso/CU-06004-Ejecutar-El-Borrado-Fisico-Y-El-Arrastre-De-La-Baja.md) | La única operación destructiva: todo o nada, sin marca de borrado |
| CU-05 | [Guardar y recuperar las cuentas de la comisión](../../Casos-De-Uso/CU-06005-Guardar-Y-Recuperar-Las-Cuentas-De-La-Comision.md) | Las dos unicidades del almacén y la marca que viaja sin ser un estado |
| CU-06 | [Derivar la contraseña y verificar una credencial](../../Casos-De-Uso/CU-06006-Derivar-La-Contrasena-Y-Verificar-Una-Credencial.md) | El único punto donde la contraseña en claro se convierte en el valor guardado, y el único que la compara |
| CU-07 | [Producir la contraseña provisoria del reseteo](../../Casos-De-Uso/CU-06007-Producir-La-Contrasena-Provisoria-Del-Reseteo.md) | La delegación explícita de RN-14: no adivinable y sin repetirse |
| CU-08 | [Emitir el acceso firmado](../../Casos-De-Uso/CU-06008-Emitir-El-Acceso-Firmado.md) | Cuatro reclamos, firma simétrica y una clave que no entra al repositorio de código |
| CU-09 | [Proveer el sello del reloj del sistema](../../Casos-De-Uso/CU-06009-Proveer-El-Sello-Del-Reloj-Del-Sistema.md) | El contrato más corto, y el que explica por qué la capa vecina se prueba sin nada |
| CU-10 | [Preparar el almacén al arrancar](../../Casos-De-Uso/CU-06010-Preparar-El-Almacen-Al-Arrancar.md) | Transformar el esquema solo, y detener el arranque antes que confiar en un almacén equivocado |

## 3. Las siete reglas conceptuales de modelo

| RC | Enunciado en una línea |
| --- | --- |
| [RC-01](../../Modelo-Datos/reglas-conceptuales-de-modelo/RC-06001-Texto-Original-Escrito-Una-Sola-Vez.md) | El texto original se escribe una sola vez y no se reescribe |
| [RC-02](../../Modelo-Datos/reglas-conceptuales-de-modelo/RC-06002-Identidad-Posicional-De-La-Pieza.md) | La identidad de la pieza es su posición, y las posiciones no se compactan |
| [RC-03](../../Modelo-Datos/reglas-conceptuales-de-modelo/RC-06003-Valor-Declarado-Y-Derivado-Por-Separado.md) | El valor declarado y el derivado se guardan por separado |
| [RC-04](../../Modelo-Datos/reglas-conceptuales-de-modelo/RC-06004-La-Familia-No-Se-Persiste.md) | La familia plana o volumétrica no se persiste |
| [RC-05](../../Modelo-Datos/reglas-conceptuales-de-modelo/RC-06005-Retiro-Fisico-Con-Arrastre.md) | El retiro es físico y la baja arrastra todo, en una sola unidad de trabajo |
| [RC-06](../../Modelo-Datos/reglas-conceptuales-de-modelo/RC-06006-Tres-Sellos-De-Tiempo-Distintos.md) | Los tres tiempos del trabajo son distintos y no se confunden |
| [RC-07](../../Modelo-Datos/reglas-conceptuales-de-modelo/RC-06007-La-Marca-No-Es-Un-Estado-De-Cuenta.md) | La marca no es un estado de cuenta, y el comentario no es una observación |

## 4. Orden de lectura sugerido

1. [`Especificacion-Funcional.md`](Especificacion-Funcional.md) §1, §3 y §4: qué es esta capa, qué implementa y **qué no decide**. Sin §4, los diez casos de uso se leen como si acá se tomaran decisiones de negocio, que es exactamente lo que no pasa.
2. [`Definicion-Contrato-Del-Validador-De-Figuras.md`](Definicion-Contrato-Del-Validador-De-Figuras.md) **entero, y antes de escribir una línea de lectura de texto**. El intake declara que el defecto que más veces se repite en este producto es escribir el validador sin leer el análisis; este documento es esa lectura, condensada.
3. Los casos de uso del dato del alumno, en el orden en que ocurren: **CU-01** y **CU-02**, que se leen juntos porque son las dos mitades del mismo puerto y sus observaciones tienen efectos opuestos sobre el estado del trabajo.
4. [`Modelo-Datos/Modelo-Conceptual.md`](Modelo-Datos/Modelo-Conceptual.md) y sus siete `RC`, antes de los casos de uso del almacén: **CU-03**, **CU-04**, **CU-05** y **CU-10**.
5. Los casos de uso de seguridad: **CU-06**, **CU-07** y **CU-08**. **CU-06 y CU-07 se leen juntos**: la provisoria nace en uno y se deriva en el otro, exactamente igual que la contraseña que el alumno elige.
6. **CU-09** en cualquier momento: son dos páginas y explican por qué la capa vecina se prueba sin base de datos.
7. [`Glosario-Funcional.md`](Glosario-Funcional.md), en particular §3.1 y §3.3, que resuelven las dos polisemias que más caro salen acá: «validador» y «derivado».

Para el lector que llega desde la capa de aplicación: la tabla de §3 del índice maestro dice qué puerto implementa cada caso de uso. **La correspondencia nunca se lee por número.**

## 5. Artefactos omitidos y el que se emite contra la guía del tipo

| Artefacto | Situación |
| --- | --- |
| `Reglas-De-Negocio/RN-XX-<Nombre>.md` | **Omitido.** Las **quince** reglas del producto viven en `GeometriaFactory-Domain`, las quince con archivo propio allá, y acá se **referencian**. §6 del índice maestro declara, regla por regla, dónde se ejerce cada una en esta capa —**trece con tramo, dos sin él y tres con su tramo principal acá**— |
| `Modelo-Datos/Modelo-Conceptual.md` y sus `RC-XX` | **Emitidos, y es la diferencia con los cinco proyectos de código anteriores.** Los dos hermanos `library` los omiten con dos motivos, y acá sólo se cumple uno: **este es el único `library` del producto con persistencia declarada** —el flag vale true acá y en `GeometriaFactory-Api`, que delega en éste—, y el intake la llama «la responsabilidad central del proyecto de código». Omitirlos dejaría al producto sin ningún documento que describa el dato guardado. Se emiten como **apartamiento declarado**, con su fundamento en §9 del índice maestro |
| `Definicion-<Concepto-Central>.md` | **Emitido**, y su concepto central es el **validador de figuras**. No es una elección de gusto: es la pieza que el intake declara de mayor riesgo del producto, la única con una batería de pruebas obligatoria y la única cuya cobertura mínima es la más alta del producto |

## 6. Notas de uso de esta sección

- **Los identificadores `CU-XX` son locales a este proyecto de código.** No coinciden con los de `GeometriaFactory-Domain` ni con los de `GeometriaFactory-Application`. La correspondencia se lee por §3 y por la matriz de §7.1 del índice maestro, **nunca por número**.
- **Los `RC-XX` no son reglas de negocio.** Declaran cómo el dato sobrevive, no qué decidió el negocio, y por eso conviven con las `RN-XX` sin competir con ellas.
- **Los códigos que devuelven los casos de uso no son códigos de protocolo.** Su traducción hacia afuera del proceso pertenece a `GeometriaFactory-Api`, y ninguna de ellas puede incluir la ruta del almacén, la clave de firma ni la dirección de un servicio interno.
- **Cada caso de uso lleva una sección §17 «Compatibilidad de la superficie pública»**, que es la sección opcional que `Rules-Especificacion-Funcional.md` §4.3 asigna al tipo `library`, con ese número. No es una sección obligatoria desplazada.
- **Los escenarios se citan por el identificador del intake** —`E-1` a `E-8`— y las trampas del formato por el suyo —`T1` a `T4`—, sin renumerar. **Ningún dato de prueba se inventó**: es la regla de delivery del producto que prohíbe inventar textos de prueba.
- Esta categoría **no toma decisiones de arquitectura**: los nombres de tipos, la elección de la función de derivación, el esquema físico y los ADR pertenecen a `05-Arquitectura-Tecnica`, y la estrategia de pruebas a `08-Calidad-Y-Pruebas`. Lo que acá se declara como «tests previstos» es una previsión, no un plan.
- **Diez puntos abiertos**, ninguno bloqueante: cinco propios de esta categoría —entre ellos qué devuelve el validador ante el texto de `E-8` y cómo se sostiene que la provisoria no se repite— y cinco que vienen declarados de aguas arriba y **no se reabren**. Están en §11 del índice maestro.

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Índice navegable de los diez casos de uso, de las siete reglas conceptuales de modelo y de los tres documentos transversales de la sección; orden de lectura de siete pasos, con la indicación de leer el documento de concepto central **antes** de escribir una línea de lectura de texto; la omisión de las reglas de negocio, el apartamiento declarado por el que se emite el modelo de datos y el fundamento del concepto central elegido; y las notas de uso de la sección. |
