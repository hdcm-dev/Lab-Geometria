# 02 · Especificación funcional — GeometriaFactory-Infrastructure

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** README.md
**Versión:** 1.4
**Estado:** Aprobado
**Fecha:** 2026-08-12
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
| [`Especificacion-Funcional.md`](Especificacion-Funcional.md) | Índice maestro: catálogo, los cuatro puertos y los dos mecanismos, la frontera entre mecanismo y decisión, la matriz NB → CU → RN → US, el criterio de recorte, las omisiones y los quince puntos abiertos. **Es el punto de entrada** | Propuesto |
| [`Definicion-Contrato-Del-Validador-De-Figuras.md`](../../Definicion-Contrato-Del-Validador-De-Figuras.md) | Documento de concepto central: las cuatro trampas del formato, las siete garantías, los tipos que reconstruye, los ocho escenarios y la cobertura de los nueve casos de la batería obligatoria del producto más el décimo que agrega §21 | Propuesto |
| [`Modelo-Datos/Modelo-Conceptual.md`](../../Modelo-Datos/Modelo-Conceptual.md) | Las cinco entidades, sus atributos, las cuatro relaciones, los cuatro conjuntos cerrados y las nueve decisiones de almacenamiento | Propuesto |
| `Modelo-Datos/reglas-conceptuales-de-modelo/` | Siete reglas conceptuales de modelo, una por archivo | Propuesto |
| [`Glosario-Funcional.md`](Glosario-Funcional.md) | Vocabulario que esta categoría acuña y los cuatro términos con más de un referente | Propuesto |
| `Casos-De-Uso/` | Diez casos de uso, uno por archivo | Propuesto |
| `README.md` | Este archivo: índice navegable, orden de lectura y omisiones | Propuesto |

No hay carpeta `_legacy/`: es la emisión inicial de la categoría para este proyecto de código.

## 2. Los diez casos de uso

| CU | Nombre | En una línea |
| --- | --- | --- |
| CU-06001 | [`CU-06001` · Interpretar el texto original y reconstruir las piezas](../../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06001-Interpretar-El-Texto-Original-Y-Reconstruir-Las-Piezas.md) | La lectura tolerante del dato real del alumno, con la posición y el campo de cada defecto |
| CU-06002 | [`CU-06002` · Verificar los valores declarados contra los derivados](../../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06002-Verificar-Los-Valores-Declarados-Contra-Los-Derivados.md) | Señalar sin corregir ni rechazar, con tolerancia y operador estricto |
| CU-06003 | [`CU-06003` · Guardar y recuperar los trabajos](../../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06003-Guardar-Y-Recuperar-Los-Trabajos.md) | El texto original conservado literal y la consulta que llega ya acotada |
| CU-06004 | [`CU-06004` · Ejecutar el borrado físico y el arrastre de la baja](../../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06004-Ejecutar-El-Borrado-Fisico-Y-El-Arrastre-De-La-Baja.md) | La única operación destructiva: todo o nada, sin marca de borrado |
| CU-06005 | [`CU-06005` · Guardar y recuperar las cuentas de la comisión](../../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06005-Guardar-Y-Recuperar-Las-Cuentas-De-La-Comision.md) | Las dos unicidades del almacén y la marca que viaja sin ser un estado |
| CU-06006 | [`CU-06006` · Derivar la contraseña y verificar una credencial](../../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06006-Derivar-La-Contrasena-Y-Verificar-Una-Credencial.md) | El único punto donde la contraseña en claro se convierte en el valor guardado, y el único que la compara |
| CU-06007 | [`CU-06007` · Producir la contraseña provisoria del reseteo](../../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06007-Producir-La-Contrasena-Provisoria-Del-Reseteo.md) | La delegación explícita de RN-06014: no adivinable y sin repetirse |
| CU-06008 | [`CU-06008` · Emitir el acceso firmado](../../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06008-Emitir-El-Acceso-Firmado.md) | Cuatro reclamos, firma simétrica y una clave que no entra al repositorio de código |
| CU-06009 | [`CU-06009` · Proveer el sello del reloj del sistema](../../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06009-Proveer-El-Sello-Del-Reloj-Del-Sistema.md) | El contrato más corto, y el que explica por qué la capa vecina se prueba sin nada |
| CU-06010 | [`CU-06010` · Preparar el almacén al arrancar](../../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06010-Preparar-El-Almacen-Al-Arrancar.md) | Transformar el esquema solo, y detener el arranque antes que confiar en un almacén equivocado |

## 3. Las siete reglas conceptuales de modelo

| RC | Enunciado en una línea |
| --- | --- |
| [RC-06001](../../Modelo-Datos/reglas-conceptuales-de-modelo/RC-06001-Texto-Original-Escrito-Una-Sola-Vez.md) | El texto original se escribe una sola vez y no se reescribe |
| [RC-06002](../../Modelo-Datos/reglas-conceptuales-de-modelo/RC-06002-Identidad-Posicional-De-La-Pieza.md) | La identidad de la pieza es su posición, y las posiciones no se compactan |
| [RC-06003](../../Modelo-Datos/reglas-conceptuales-de-modelo/RC-06003-Valor-Declarado-Y-Derivado-Por-Separado.md) | El valor declarado y el derivado se guardan por separado |
| [RC-06004](../../Modelo-Datos/reglas-conceptuales-de-modelo/RC-06004-La-Familia-No-Se-Persiste.md) | La familia plana o volumétrica no se persiste |
| [RC-06005](../../Modelo-Datos/reglas-conceptuales-de-modelo/RC-06005-Retiro-Fisico-Con-Arrastre.md) | El retiro es físico y la baja arrastra todo, en una sola unidad de trabajo |
| [RC-06006](../../Modelo-Datos/reglas-conceptuales-de-modelo/RC-06006-Tres-Sellos-De-Tiempo-Distintos.md) | Los tres tiempos del trabajo son distintos y no se confunden |
| [RC-06007](../../Modelo-Datos/reglas-conceptuales-de-modelo/RC-06007-La-Marca-No-Es-Un-Estado-De-Cuenta.md) | La marca no es un estado de cuenta, y el comentario no es una observación |

## 4. Orden de lectura sugerido

1. [`Especificacion-Funcional.md`](Especificacion-Funcional.md) §1, §3 y §4: qué es esta capa, qué implementa y **qué no decide**. Sin §4, los diez casos de uso se leen como si acá se tomaran decisiones de negocio, que es exactamente lo que no pasa.
2. [`Definicion-Contrato-Del-Validador-De-Figuras.md`](../../Definicion-Contrato-Del-Validador-De-Figuras.md) **entero, y antes de escribir una línea de lectura de texto**. El intake declara que el defecto que más veces se repite en este producto es escribir el validador sin leer el análisis; este documento es esa lectura, condensada.
3. Los casos de uso del dato del alumno, en el orden en que ocurren: **CU-06001** y **CU-06002**, que se leen juntos porque son las dos mitades del mismo puerto y sus observaciones tienen efectos opuestos sobre el estado del trabajo.
4. [`Modelo-Datos/Modelo-Conceptual.md`](../../Modelo-Datos/Modelo-Conceptual.md) y sus siete `RC`, antes de los casos de uso del almacén: **CU-06003**, **CU-06004**, **CU-06005** y **CU-06010**.
5. Los casos de uso de seguridad: **CU-06006**, **CU-06007** y **CU-06008**. **CU-06006 y CU-06007 se leen juntos**: la provisoria nace en uno y se deriva en el otro, exactamente igual que la contraseña que el alumno elige.
6. **CU-06009** en cualquier momento: son dos páginas y explican por qué la capa vecina se prueba sin base de datos.
7. [`Glosario-Funcional.md`](Glosario-Funcional.md), en particular §3.1 y §3.3, que resuelven las dos polisemias que más caro salen acá: «validador» y «derivado».

Para el lector que llega desde la capa de aplicación: la tabla de §3 del índice maestro dice qué puerto implementa cada caso de uso. **La correspondencia nunca se lee por número.**

## 5. Artefactos omitidos y el que se emite contra la guía del tipo

| Artefacto | Situación |
| --- | --- |
| `Reglas-De-Negocio/RN-XX-<Nombre>.md` | **Omitido.** Las **dieciséis** reglas del producto viven en `GeometriaFactory-Domain`, las dieciséis con archivo propio allá, y acá se **referencian**. §6 del índice maestro declara, regla por regla, dónde se ejerce cada una en esta capa —**catorce con tramo, dos sin él y tres con su tramo principal acá**— |
| `Modelo-Datos/Modelo-Conceptual.md` y sus `RC-XX` | **Emitidos, y es la diferencia con los cinco proyectos de código anteriores.** Los dos hermanos `library` los omiten con dos motivos, y acá sólo se cumple uno: **este es el único `library` del producto con persistencia declarada** —el flag vale true acá y en `GeometriaFactory-Api`, que delega en éste—, y el intake la llama «la responsabilidad central del proyecto de código». Omitirlos dejaría al producto sin ningún documento que describa el dato guardado. Se emiten como **apartamiento declarado**, con su fundamento en §9 del índice maestro |
| `Definicion-<Concepto-Central>.md` | **Emitido**, y su concepto central es el **validador de figuras**. No es una elección de gusto: es la pieza que el intake declara de mayor riesgo del producto, la única con una batería de pruebas obligatoria y la única cuya cobertura mínima es la más alta del producto |

## 6. Notas de uso de esta sección

- **Los identificadores `CU-XX` son locales a este proyecto de código.** No coinciden con los de `GeometriaFactory-Domain` ni con los de `GeometriaFactory-Application`. La correspondencia se lee por §3 y por la matriz de §7.1 del índice maestro, **nunca por número**.
- **Los `RC-XX` no son reglas de negocio.** Declaran cómo el dato sobrevive, no qué decidió el negocio, y por eso conviven con las `RN-XX` sin competir con ellas.
- **Los códigos que devuelven los casos de uso no son códigos de protocolo.** Su traducción hacia afuera del proceso pertenece a `GeometriaFactory-Api`, y ninguna de ellas puede incluir la ruta del almacén, la clave de firma ni la dirección de un servicio interno.
- **Cada caso de uso lleva una sección §17 «Compatibilidad de la superficie pública»**, que es la sección opcional que `Rules-Especificacion-Funcional.md` §4.3 asigna al tipo `library`, con ese número. No es una sección obligatoria desplazada.
- **Los escenarios se citan por el identificador del intake** —`E-1` a `E-8`— y las trampas del formato por el suyo —`T1` a `T4`—, sin renumerar. **Ningún dato de prueba se inventó**: es la regla de delivery del producto que prohíbe inventar textos de prueba.
- Esta categoría **no toma decisiones de arquitectura**: los nombres de tipos, la elección de la función de derivación, el esquema físico y los ADR pertenecen a `05-Arquitectura-Tecnica`, y la estrategia de pruebas a `08-Calidad-Y-Pruebas`. Lo que acá se declara como «tests previstos» es una previsión, no un plan.
- **Quince filas de puntos abiertos, catorce de ellas abiertas y ninguna bloqueante**: **nueve propias** de esta categoría —entre ellas cómo se sostiene que la provisoria no se repite y de dónde sale el valor derivado del área de una pieza volumétrica— y **seis** que vienen declaradas de aguas arriba y **no se reabren**. La decimoquinta, la condición derivada `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, está **cerrada**: `PRODUCT-INTAKE` **1.29** §17.3 P.11 punto 5 la confirma el 2026-08-12. Están en §11 del índice maestro.
- **Qué devuelve el validador ante el texto de `E-8` ya no es un punto abierto.** Lo resolvió el Product Owner y el `PRODUCT-INTAKE` **1.12** lo declara en §20.E-8 punto 5 y en la fila «Dimensión no legible» de §21: es **error**, el trabajo **queda en `Borrador`** y no pasa a `Pendiente`, con el mensaje localizado por índice de figura y campo que exige RN-06009. El resultado está en `Definicion-Contrato-Del-Validador-De-Figuras.md` §6 y §7 y verificado por `CU-06001` **CA-12**.

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Índice navegable de los diez casos de uso, de las siete reglas conceptuales de modelo y de los tres documentos transversales de la sección; orden de lectura de siete pasos, con la indicación de leer el documento de concepto central **antes** de escribir una línea de lectura de texto; la omisión de las reglas de negocio, el apartamiento declarado por el que se emite el modelo de datos y el fundamento del concepto central elegido; y las notas de uso de la sección. |
| 1.1 | 2026-08-10 | Ronda 2 de auditoría: correcciones de `SDD/Docs/Audit/B-02-03-GeometriaFactory-Infrastructure-r1.md` contra el `PRODUCT-INTAKE` **1.12**. **H-01**: la nota de puntos abiertos deja de remitir a un punto abierto que ya no existe y declara el desenlace del envío de `E-8` que el intake 1.12 fija —error, trabajo en `Borrador`, mensaje localizado por índice de figura y campo—. **H-04**: el recuento de puntos abiertos pasa de **diez** a **quince**, nueve propios y seis heredados, tras incorporar el índice maestro los seis que declaraban documentos subordinados y salir el de `E-8`. **H-03**, por arrastre: el catálogo de §2 deja de describir la cobertura del documento de concepto central como «batería obligatoria de nueve casos» y nombra los nueve del producto más el décimo que agrega §21. |
| 1.2 | 2026-08-10 | Alineación con `PRODUCT-INTAKE` **1.13** §4.1 (**RN-06016**) y la precisión de **F-04**, que `CU-06007` 1.2 absorbe: la contraseña provisoria la produce el mismo contrato para la **habilitación** y para el reseteo, con lo que ese caso de uso pasa a tener **dos** consumidores en la capa de aplicación. **El nombre del archivo de `CU-06007` se conserva** por estabilidad de citación, aunque su propósito sea hoy más amplio que el reseteo. Los **diez** casos de uso no cambian de número ni de recorte y ningún artefacto se agrega ni se omite. (Analista Funcional + API Designer (AG-02)). |
| 1.3 | 2026-08-10 | **Cierra el hallazgo `C-02` (P0) del informe de auditoría `SDD/Docs/Audit/Coherencia-Corpus-r1.md` 1.0 en las declaraciones vivas de este archivo que el informe no registra, contra `PRODUCT-INTAKE` 1.14.** La fila de `Reglas-De-Negocio/` decía **quince** reglas, «las quince con archivo propio allá», y desglosaba «trece con tramo, dos sin él». Las reglas son **dieciséis**, `RN-06001` a `RN-06016`, contadas sobre los archivos de `GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/`, y el desglose pasa a **catorce con tramo y dos sin él**, recontado sobre la tabla de §6 del índice maestro, que en la misma tanda incorporó la fila de `RN-06016` que le faltaba. Las tres con tramo principal acá —RN-06008, RN-06009 y RN-06014— no cambian. **Ningún documento de la sección, ningún caso de uso y ninguna omisión declarada cambia.** Sube minor. |
| 1.4 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Absorbe la decisión (c) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.3 P.11 punto 5): se **confirma** la condición `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO` tal como esta capa la había declarado, con su fundamento —cuando ninguna pieza se pudo reconstruir corresponde una condición propia, y no una lista vacía de observaciones ni una escena en blanco—. **El enunciado no cambia**: lo que cambia es que deja de ser derivación y pasa a estar enunciada por la fuente. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
