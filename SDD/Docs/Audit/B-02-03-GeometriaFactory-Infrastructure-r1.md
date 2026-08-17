# Informe de auditoría — Fase B · GeometriaFactory-Infrastructure · categorías 02 y 03 · ronda 1

**Producto:** Fábrica de Geometría
**Fase auditada:** B (02-Especificacion-Funcional y 03-UX-UI-DX)
**Unidad de entrega:** GeometriaFactory-Api
**Alcance de la ronda:** los **27 documentos** vivos de `SDD/Docs/Proyectos/GeometriaFactory-Infrastructure/02-Especificacion-Funcional/` y `.../03-UX-UI-DX/`, todos en versión 1.0 y con fecha 2026-08-10. Contrastado contra `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.12**, `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` **1.2**, las nueve necesidades de `01-Necesidades-Negocio/Necesidades-De-Negocio/`, y los proyectos de código vecinos aguas arriba `GeometriaFactory-Domain`, `GeometriaFactory-Application` y `GeometriaFactory-Contracts`
**Auditor:** Arquitecto de Soluciones + QA Senior, invocado desde cero, sin participación en la generación
**Fecha:** 2026-08-10
**Ronda:** 1

**Categoría 04:** omitida por gating (`usa_llm` == false). Su ausencia no es hallazgo y no se evalúa.

---

## Tabla de contenido

- [1. Resumen ejecutivo](#1-resumen-ejecutivo)
- [2. Verificación de las afirmaciones sobre otras fuentes](#2-verificación-de-las-afirmaciones-sobre-otras-fuentes)
- [3. La decisión del intake 1.12 sobre E-8](#3-la-decisión-del-intake-112-sobre-e-8)
- [4. Recuentos y conjuntos cerrados, recontados de forma independiente](#4-recuentos-y-conjuntos-cerrados-recontados-de-forma-independiente)
- [5. Identificadores citados](#5-identificadores-citados)
- [6. Cobertura de lo que las otras capas delegan acá](#6-cobertura-de-lo-que-las-otras-capas-delegan-acá)
- [7. Las tres reglas de arquitectura](#7-las-tres-reglas-de-arquitectura)
- [8. Forma](#8-forma)
- [9. Hallazgos](#9-hallazgos)
- [10. Lo que se verificó y quedó bien](#10-lo-que-se-verificó-y-quedó-bien)
- [11. Veredicto y condiciones para promover](#11-veredicto-y-condiciones-para-promover)

---

## 1. Resumen ejecutivo

**La calidad mecánica de esta emisión es alta y la de su contenido también.** Los recuentos que el proyecto declara —diez casos de uso, diecisiete condiciones distintas, siete reglas conceptuales de modelo, veinticinco historias, quince reglas de negocio referenciadas con trece tramos acá, diecisiete términos acuñados— **cierran todos**, recontados de forma independiente sobre los archivos y sin apoyarse en las cifras declaradas. Las citas cruzadas que más peso cargan —la delegación de RN-14 por `GeometriaFactory-Application`, por `GeometriaFactory-Contracts` y por `GeometriaFactory-Domain`— **son exactas palabra por palabra**. Las 27 tablas están bien formadas: cero filas con distinto número de celdas que su encabezado. Los datos de prueba de los ocho escenarios se transcriben del intake sin inventar ninguno, y los nueve casos de la batería obligatoria mapean a criterios de aceptación que existen y dicen lo que la matriz dice que dicen.

**Y con todo eso, la emisión está desactualizada en el punto exacto que más importa.** El intake pasó a **1.12** y resolvió el desenlace del escenario **E-8** —dimensión no legible, del tipo `"3,50"`— como **error**, con el trabajo en `Borrador` y mensaje localizado por índice y campo. Los 27 documentos se emitieron citando el intake **1.11**, y **nueve pasajes de siete documentos declaran ese desenlace como punto abierto y afirman que la fuente no lo prescribe**. La afirmación era cierta contra 1.11 y **hoy es falsa**: el intake que vive en `SDD/Intake/` dice lo contrario, en §20.E-8 punto 5 y en la fila de E-8 de §21. Es el modo de falla que el propio proyecto declara **más probable** del producto, es del validador, y el validador es de esta capa. La ironía documentada es que la fila 1.12 del control de cambios del intake dice que **fue esta misma emisión la que levantó el punto**: la decisión se tomó por pedido de este proyecto de código y no volvió a él.

Se emiten **seis hallazgos**: **un P0**, **dos P1**, **uno P2** y **dos P3**.

**Veredicto: RECHAZADO.**

---

## 2. Verificación de las afirmaciones sobre otras fuentes

Es el defecto por el que se rechazó la tanda anterior, así que se muestrearon las citas cruzadas de más peso y se verificaron **abriendo la fuente**. Resultado: **todas exactas salvo las que dependen del estado de E-8**.

| Afirmación auditada | Dónde | Fuente abierta | Resultado |
| --- | --- | --- | --- |
| «`GeometriaFactory-Application` §6 declara que RN-14 es la única de las quince **sin tramo en su capa**» | `Especificacion-Funcional.md` §6, fila RN-14; `CU-07` §1 | `GeometriaFactory-Application/02/Especificacion-Funcional.md:120` y `:124`: «**Catorce de las quince tienen tramo acá** —la excepción es RN-14…»; `:140`: «**No se ejerce acá**… La ejerce `GeometriaFactory-Infrastructure`» | **Exacta** |
| «`GeometriaFactory-Contracts` `CU-08` §10 la exige sin declarar mecanismo» | `Especificacion-Funcional.md` §6; `CU-07` §1 y cabecera | `Contracts/…/CU-08-Contrato-De-Reseteo-Y-Cambio-Obligatorio-De-Contrasena.md` §10: «El contrato **no declara mecanismo**: cómo se produce un valor con esas dos propiedades es de `05-Arquitectura-Tecnica` y de `GeometriaFactory-Infrastructure`» | **Exacta** |
| «`RN-14` §3 nombra a este proyecto de código como el lugar de la generación» | `Especificacion-Funcional.md` §6, fila RN-14 | `Domain/…/RN-14-Provisoria-Producida-Por-El-Sistema.md` §3: «se ejerce donde el valor nace… y **la generación es de `GeometriaFactory-Infrastructure`**» | **Exacta** |
| «El `PRODUCT-MANIFEST` §5 declara el flag de persistencia true acá y también en `GeometriaFactory-Api`, pero aquél **delega en éste**» | `Especificacion-Funcional.md` §1; `Modelo-Conceptual.md` §1 | Manifiesto 1.2 §5: `tiene_persistencia` true en `Infrastructure` y en `Api`; fundamento: «`Api` (toma de configuración la ruta del archivo y aplica migraciones al arrancar). Los otros cinco declaran "No aplica"» | **Exacta** |
| «Es el único `library` del producto con persistencia declarada true» | `Especificacion-Funcional.md` §9 | Manifiesto 1.2 §5: los cinco `library` son Domain, Contracts, Visor, Application e Infrastructure; sólo el último tiene true | **Exacta** |
| «El intake declara la persistencia "la responsabilidad central del proyecto de código" (§17.3.P.4)» | `Especificacion-Funcional.md` §9 | Intake §17.3.P.4 | **Exacta** |
| «El intake declara con probabilidad alta y con impacto alto que el validador se escribe sin leer el análisis» (RN-B3) | `Especificacion-Funcional.md` §1; `Definicion-Contrato…` §1 | Intake §11, RN-B3 | **Exacta** |
| Las cuatro trampas T1 a T4 y su origen en §17.3.P.11 punto 1 | `Definicion-Contrato…` §2; `CU-01` §10 | Intake §17.3.P.11 punto 1 | **Exacta** |
| E-1 rinde «3 piezas y 2 advertencias»; el área del cilindro **no** advierte por diferencia de exactamente 0.01; el área del ortoedro coincide en 686.00 | `Definicion-Contrato…` §6; `CU-01` CA-07; `CU-02` CA-01 a CA-03 | Intake §20.E-1 «Qué verificar» puntos 1 a 5, y §17.3.P.10 con el operador estricto fijado en 1.4 | **Exacta** |
| E-5 rinde cantidad 2, una pieza y una observación con **posición 1** y campo `Tipo` | `CU-01` CA-04 | Intake §20.E-5 «Qué verificar» puntos 1 a 3, con el mismo argumento del índice 1 y no 0 | **Exacta** |
| La matriz de nueve casos obligatorios de §21 y el escenario que ejercita a cada uno | `Definicion-Contrato…` §7 | Intake §21, primera tabla | **Exacta** en las nueve filas |
| `RectanguloDesarrollado` sin escenario propio, y el motivo | `Definicion-Contrato…` §5; `CU-01` §10 | Intake §21, «Tipos de figura sin escenario propio» | **Exacta** |
| «**§20** … su **punto 4** dice que no prescribe el desenlace del envío» | Cinco pasajes que lo afirman en positivo (ver §3) | Intake **1.12** §20.E-8 punto 4: «…decidir si el trabajo pasa a `Pendiente` es del validador, no del bundle (RA-02)». **La oración que se cita no está.** Está en `SDD/Intake/_legacy/2026-08-08/…-v1.11.md:1464` | **Falsa contra la fuente vigente** → **H-01** |
| «La décima fila de §21 **no pertenece a la batería obligatoria** … su lugar de verificación es la **etapa de visualización**» | `Definicion-Contrato…` §7, precisión 1 | Intake 1.12 §21: «Dimensión no legible \| E-8 \| … En el validador, **error**: el trabajo queda en `Borrador` \| **Etapas `f` y `g`**». La etapa `f` es la del validador | **Falsa contra la fuente vigente** → **H-03** |

**Cómo se verificó:** apertura directa de cada sección citada en `SDD/Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md` (1.12), `SDD/Intake/PRODUCT-MANIFEST-Fabrica-De-Geometria.md` (1.2) y los archivos nombrados de los tres proyectos de código vecinos, con contraste literal de la cadena citada. Para la cita de E-8 se localizó además **la versión de la fuente en la que sí era cierta**, en `SDD/Intake/_legacy/2026-08-08/`.

---

## 3. La decisión del intake 1.12 sobre E-8

El intake 1.12, fila del control de cambios: *«**Desenlace del envío para `E-8`**, resuelto por el Product Owner… Se resuelve como **error**: el trabajo queda en `Borrador` con el mensaje localizado por índice y campo»*. El texto vivo lo lleva en dos lugares:

- **§20.E-8, «Qué verificar», punto 5**: *«**El desenlace del envío es error, no advertencia** [DECISIÓN 2026-08-09]. El trabajo **queda en `Borrador`** y no pasa a `Pendiente`, con el mensaje localizado por índice de figura y campo que exige RN-09»*, con el fundamento —una dimensión ilegible no es un valor mal calculado sino uno que no se pudo leer— y la observación de que es **el modo de falla más probable de todos los escenarios**.
- **§21**, fila «Dimensión no legible»: *«En el validador, **error**: el trabajo queda en `Borrador`»*, con lugar de verificación **etapas `f` y `g`**.

**Dónde no llegó.** Barrido de `E-8` sobre los 27 documentos:

| Documento | Pasaje | Qué dice hoy |
| --- | --- | --- |
| `02/Especificacion-Funcional.md` §11, primera fila | Punto abierto propio | «**Ninguna fuente declara el resultado esperado para esta capa**… su punto 4 dice que no prescribe el desenlace del envío» |
| `02/Definicion-Contrato-Del-Validador-De-Figuras.md` §6, fila E-8 | Resultado esperado en este contrato | «**Nada declarado para este contrato**… **No hay resultado declarado**» |
| `02/Definicion-Contrato-Del-Validador-De-Figuras.md` §7, precisión 1 | Cobertura | «no pertenece a la batería obligatoria… su lugar de verificación es la etapa de visualización» |
| `02/Definicion-Contrato-Del-Validador-De-Figuras.md` §9, primera fila | Punto abierto del contrato | «**Esta categoría no elige** y lo eleva» |
| `02/Casos-De-Uso/CU-01…md` §10 | Nota | «Qué devuelve **este** contrato ante ese texto **no está declarado por ninguna fuente**» |
| `02/README.md` línea 96 | Nota de puntos abiertos | «entre ellos **qué devuelve el validador ante el texto de `E-8`**» |
| `03/Guia-Onboarding-Developer.md` línea 194 | Dónde buscar | «**Ninguna fuente lo declara**… Es un punto abierto elevado al Product Owner» |
| `03/README.md` línea 100 | Puntos abiertos que roza | «**qué devuelve el validador ante el texto de `E-8`**» |
| `03/DX-Error-Messages.md` §5 punto 3 | Localización | «Qué hace el validador con él es **un punto abierto** declarado en la categoría 02» |

**Nueve pasajes en siete documentos.** Los cuatro primeros y el de `CU-01` afirman además, en positivo, que la fuente **no** prescribe el desenlace, lo cual hoy es una afirmación falsa sobre el intake. Los cuatro restantes se limitan a remitir al punto abierto y caen por arrastre.

**Consecuencia funcional, que es lo que lo vuelve P0 y no P2.** El validador de figuras es de esta capa y este proyecto de código es su documento de concepto central. Con el corpus como está, `08-Calidad-Y-Pruebas` no tiene resultado esperado para el modo de falla más probable del producto, `05-Arquitectura-Tecnica` no sabe si `"3,50"` produce una observación de especie **error de validación** o una **advertencia**, y las dos opciones tienen efectos **opuestos** sobre el estado del trabajo —`Borrador` contra `Pendiente`— por RN-05. Además, la decisión ya tomada tiene consecuencias concretas sobre artefactos que hoy dicen otra cosa: la fila E-8 de `Definicion-Contrato…` §6 debería declarar el resultado, y `CU-01` debería tener un criterio de aceptación con el texto de E-8, la observación de especie error de validación con **posición 1** y **campo `Largo`**, y la primera pieza reconstruida igual —exactamente la forma de CA-04, que ya existe para E-5—.

---

## 4. Recuentos y conjuntos cerrados, recontados de forma independiente

Extraídos de los archivos, sin usar las cifras declaradas.

### 4.1 Casos de uso

Diez archivos en `Casos-De-Uso/`, `CU-01` a `CU-10`, serie contigua sin huecos. El catálogo de `Especificacion-Funcional.md` §5 tiene **diez filas** y coincide una a una con los archivos y con sus títulos. **Cierra.**

### 4.2 Condiciones de error

Filas de la §6 de cada caso de uso, contadas sobre el archivo:

| CU | Filas en su §6 | Códigos |
| --- | --- | --- |
| CU-01 | 2 | `TEXTO_ORIGINAL_AUSENTE`, `INTERPRETACION_NO_DISPONIBLE` |
| CU-02 | 1 | `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO` |
| CU-03 | 4 | `CONSULTA_SIN_ALCANCE_DECLARADO`, `ESCRITURA_QUE_REESCRIBE_EL_TEXTO_ORIGINAL`, `ESCRITURA_CONCURRENTE_RECHAZADA`, `ALMACEN_NO_DISPONIBLE` |
| CU-04 | 2 | `RETIRO_PARCIAL_NO_ADMITIDO`, `ALMACEN_NO_DISPONIBLE` |
| CU-05 | 3 | `CORREO_YA_REGISTRADO`, `UNICIDAD_DE_ADMINISTRADOR_VIOLADA`, `ALMACEN_NO_DISPONIBLE` |
| CU-06 | 2 | `CONTRASENA_EN_CLARO_AUSENTE`, `CREDENCIAL_DERIVADA_ILEGIBLE` |
| CU-07 | 1 | `FUENTE_DE_ALEATORIEDAD_NO_DISPONIBLE` |
| CU-08 | 2 | `CLAVE_DE_FIRMA_AUSENTE`, `RECLAMOS_INCOMPLETOS` |
| CU-09 | 0 | ninguna, con ausencia declarada en su §6 |
| CU-10 | 2 | `MIGRACION_NO_APLICABLE`, `RUTA_DEL_ALMACEN_NO_DISPONIBLE` |
| **Total** | **19** | **17 distintos** |

Única condición repetida: `ALMACEN_NO_DISPONIBLE`, con multiplicidad 3 y por lo tanto **2 reapariciones**. Cuadre 17 + 2 = 19. **Las cifras de `DX-Error-Messages.md` §7.1 y §7.2 son exactas en todas sus filas y en sus dos totales**, incluida la columna por caso de uso.

Contraste contra el catálogo de §3: **17 filas de tabla** distribuidas 2·1·4·1·2·2·1·2·2 sobre nueve subsecciones, **diferencia simétrica vacía** contra las §6, **cero condiciones inventadas** y cero condiciones de caso de uso sin entrada. La subsección faltante es la de `CU-09`, con su ausencia declarada en §2.5.

Taxonomía de §2.1, recontada sobre la tabla de §7.3: entrada inválida 6, recurso ausente 1, conflicto de estado 4, conflicto de facultad 0, conflicto de alcance 0, error transitorio 5, error interno 1. **Suman 17 y coinciden con los siete valores declarados.** Forma de terminación de §2.3: negativa sin escritura 11, degradada 4, arranque detenido 2. **Suman 17 y coinciden.**

### 4.3 Reglas de negocio

`Especificacion-Funcional.md` §6 declara «Trece de las quince tienen tramo acá y dos no lo tienen». Recontado: la tabla tiene **quince filas**, RN-01 a RN-15 sin huecos; las que dicen «**Sin tramo acá**» son **RN-06** y **RN-10**, dos. 15 − 2 = 13. **Cierra.** Las tres con tramo principal acá —RN-08, RN-09, RN-14— están marcadas como tales en sus tres filas y se repiten con el mismo trío en `DX-Error-Messages.md` §7.4. **Cierra.**

### 4.4 Reglas conceptuales de modelo

Siete archivos `RC-01` a `RC-07` en `reglas-conceptuales-de-modelo/`, serie contigua. La tabla de `Modelo-Conceptual.md` §5 tiene **siete filas** y cada una enlaza al archivo que existe. **Cierra.**

### 4.5 Historias previstas

`Especificacion-Funcional.md` §7.3: **veinticinco filas**, US-01 a US-25 sin huecos, cada una con su CU de origen. Contrastadas contra las celdas «Historias de usuario a generar en 06» de las §9 de los diez casos de uso: la unión de esas celdas es exactamente US-01 a US-25, sin sobrantes ni faltantes. **Cierra.**

### 4.6 Modelo de datos y glosario

Cinco entidades en `Modelo-Conceptual.md` §3 (Cuenta, Trabajo, Pieza, Componente, Observación), **cuatro** relaciones en el diagrama, **cuatro** conjuntos cerrados con 2·3·4·2 valores, **nueve** decisiones de almacenamiento en §2. Todos coinciden con lo declarado. `Glosario-Funcional.md` §2 tiene **diecisiete** filas de término acuñado, que es la cifra que su control de cambios declara. **Cierra.**

### 4.7 El único recuento que no cierra

**Los «diez puntos abiertos».** Ver **H-04**.

---

## 5. Identificadores citados

Barrido de `RN-XX`, `INV-XX`, `NB-XX`, `E-X`, `CU-XX`, `RC-XX`, `T-X` y `RA-XX` sobre los 27 documentos, contra el lugar donde cada uno debe existir.

| Serie | Rango citado en la Fase B | Existe donde se dice | Resultado |
| --- | --- | --- | --- |
| `RN-XX` | RN-01 a RN-15, sin ninguno fuera de rango | Intake §4.1 declara quince; `GeometriaFactory-Domain/…/Reglas-De-Negocio/` tiene los quince archivos, y las quince rutas enlazadas en §6 resuelven | **Sin fantasmas** |
| `INV-XX` | Sólo INV-09, en `CU-07` §10 y en la cabecera de `CU-07` | Intake §17.1.P.2 declara nueve invariantes; INV-09 existe y es la marca de cambio de contraseña pendiente | **Correcto** |
| `NB-XX` | NB-00001 a NB-00009 | Nueve archivos en `01-Necesidades-Negocio/Necesidades-De-Negocio/`; las nueve rutas de la matriz §7.1 resuelven | **Sin fantasmas** |
| `E-X` | E-1 a E-8 | Intake §20 declara ocho escenarios | **Sin fantasmas**, con la salvedad de contenido de H-01 |
| `CU-XX` | CU-01 a CU-10 locales, más citas cruzadas a `Application` CU-01, CU-03, CU-05, CU-11 y a `Contracts` CU-08 | Los diez archivos locales existen; `Application/…/CU-11-Resetear-La-Contrasena-De-Un-Alumno.md` y `Contracts/…/CU-08-…md` existen y dicen lo citado | **Sin fantasmas**, con la salvedad de H-05 |
| `RC-XX` | RC-01 a RC-07 | Siete archivos propios | **Sin fantasmas** |
| `T-X` | T1 a T4 | Intake §17.3.P.11 punto 1 | **Sin fantasmas** |
| `RA-XX` | RA-03, citada cinco veces. RA-01 y RA-02 **sólo** aparecen dentro de la cadena «§14 (RA-01 a RA-03)» de una trazabilidad upstream | Intake §14 declara las tres | **Ninguna inexistente**, pero ver H-06 |

**Cómo se verificó:** extracción de identificadores por expresión regular sobre los 27 archivos, y comprobación de existencia archivo por archivo en el intake, en `01-Necesidades-Negocio/` y en los proyectos de código vecinos. **Ningún identificador citado en esta Fase B falta en su origen.**

---

## 6. Cobertura de lo que las otras capas delegan acá

### 6.1 RN-14 — la producción de la contraseña provisoria

**Cubierta, y con holgura.** Es lo mejor del corpus. `CU-07` es un caso de uso entero dedicado a la delegación, con siete criterios de aceptación que **prueban las dos propiedades como propiedades y no como prosa**: CA-01 (tres provisorias distintas, la misma que `Contracts` `CU-08` CA-10 verifica del otro lado), CA-02 (mil sin repetición), CA-03 (no derivable del correo, nombre, apellido, identificador ni fecha), CA-04 (el momento no interviene), CA-05 (fuente caída → cero valores y **ningún** valor compuesto por otro medio), CA-06 (el valor en claro no aparece en registro ni almacén) y CA-07 (no se conserva; el camino es resetear de nuevo).

Tres decisiones de diseño la refuerzan y merecen quedar registradas como bien hechas:

1. **La invocación no recibe ningún dato de la cuenta** (`CU-07` §3), lo cual convierte la no derivabilidad en una garantía **estructural** y no en una promesa de implementación.
2. **La condición `FUENTE_DE_ALEATORIEDAD_NO_DISPONIBLE`** no compone el valor por otro medio, con el argumento correcto en `DX-Error-Messages.md` §2.4: «un reseteo que no se completa es recuperable; una provisoria adivinable no se nota hasta que alguien la usa».
3. **§17 de `CU-07`** declara incompatibles y de versión mayor los cinco cambios que vaciarían la regla, y señala cuál la vacía **en silencio**.

La única salvedad es de forma y no de cobertura: `CU-07` §1 y `Especificacion-Funcional.md` §4 hablan de «**las tres capas de arriba**» y enumeran `Application`, `Contracts` y `Domain`. Verificado: son efectivamente tres las que delegan **el mecanismo** por escrito. `GeometriaFactory-Web` nombra RN-14 en su `Glosario-UX.md` pero para declarar que el panel no tiene dónde escribir la provisoria, que no es una delegación de mecanismo. **No es hallazgo.**

### 6.2 RN-08 — la conservación íntegra del texto original

**Cubierta.** Tramo principal declarado acá con dos brazos y los dos verificados:

- **No se altera al leer.** `CU-01` CA-09: texto devuelto y recibido **idénticos carácter por carácter**, sin reordenar ni normalizar. `CU-02` CA-08: la discrepancia se señala y el valor del alumno no se corrige.
- **No se reescribe al guardar.** `RC-01` con archivo propio, la condición `ESCRITURA_QUE_REESCRIBE_EL_TEXTO_ORIGINAL` en `CU-03` §6, y `CU-03` CA-01 (el texto de E-2 vuelve con sus dos comas finales) y CA-02 (segunda materialización con texto distinto → rechazo y texto conservado sin cambio).

Y una prohibición transversal que la protege por el otro lado: `DX-Error-Messages.md` §1.4 prohíbe incluir el texto del alumno, entero o en parte, dentro de un mensaje.

### 6.3 RN-07 — el borrado físico con arrastre

**Cubierta.** `CU-04` con su §7 en tres postcondiciones —éxito de trabajo, éxito de cuenta, fallo sin retiro parcial— y cinco criterios: CA-01 (trabajo con piezas, componentes y observaciones, no queda ninguna fila), CA-02 (cuenta con tres trabajos en tres estados distintos, con comentario, no queda nada — literalmente el criterio de verificación que el intake §4.1 declara para RN-07), CA-03 (arrastre no declarado → `RETIRO_PARCIAL_NO_ADMITIDO` y todo intacto), CA-04 (cuenta sin trabajos) y CA-05 (baja interrumpida → todo entero). `RC-05` lo materializa en el modelo, y `Modelo-Conceptual.md` §6 declara que **no existe** marca de borrado lógico. La confirmación escrita del correo se declara de la capa de aplicación, correctamente: el intake atribuye a RN-07 las dos mitades y esta capa dice explícitamente cuál es la suya.

---

## 7. Las tres reglas de arquitectura

| Regla | Cómo la trata esta Fase B | Evaluación |
| --- | --- | --- |
| **RA-01** — ningún JavaScript del navegador llama a la API | No se nombra en el cuerpo de ningún documento. **Nada del corpus la roza**: esta capa no tiene superficie de navegador, no atiende peticiones y su único consumidor declarado es la composición de raíz de `GeometriaFactory-Api` | **No se viola.** Falta la declaración de no aplicabilidad → H-06 |
| **RA-02** — el visor es visualizador puro, sin red, sin configuración y sin identidad | Tampoco se nombra, pero **su contenido se respeta y se refuerza**: `Definicion-Contrato…` §8 declara la frontera con el visor sin duplicar validación, y le atribuye «en el navegador, **sin red y sin identidad**». Del lado propio, `CU-01` CA-11 exige **0 peticiones de red** originadas por el contrato del validador, y G-6 lo eleva a garantía. La frontera está bien trazada: el visor decide qué dibuja, este contrato decide si el trabajo verifica | **No se viola.** Falta nombrarla → H-06 |
| **RA-03** — todo pasa por el front y ningún mensaje expone direcciones de servicios internos | **Es la que esta capa sí ejerce, y la ejerce bien.** `Especificacion-Funcional.md` §4 precisión 4 la declara transversal con su contracara —todo error que se muestre queda registrado del lado del servidor—; `DX-Error-Messages.md` §1.4 la convierte en tabla de cuatro prohibiciones con su reemplazo textual, extendiéndola explícitamente a **la ruta del almacén** («es una dirección de servicio interno **a los efectos de RA-03**», extensión declarada y no encubierta); §4 la baja a reglas de tono con ejemplo negativo concreto; `CU-08` §9 la lleva a la trazabilidad y su §17 declara **incompatible** devolver la dirección de un servicio interno en un mensaje. Verificado por barrido: **ninguna de las 17 entradas del catálogo contiene ruta, clave ni dirección** | **Cumple** |

---

## 8. Forma

| Comprobación | Resultado |
| --- | --- |
| Documentos vivos | **27**, los declarados |
| Versión y fecha en cabecera | **27 de 27**, todos 1.0 y 2026-08-10 |
| Sección de control de cambios | **27 de 27**, con una fila 1.0 «Emisión inicial» |
| Filas de tabla con distinto número de celdas que su encabezado | **0**, sobre las 27 archivos y todas sus tablas, contando escapes de barra |
| Enlaces relativos a documentos vecinos y a `Reglas-De-Negocio/` | Resueltos: los quince `RN-XX`, los nueve `NB-XX`, los siete `RC-XX`, `Application` `CU-01/03/05/11` y `Contracts` `CU-08` existen en las rutas escritas |
| Numeración de secciones de los casos de uso | Uniforme: §1 a §11 más §17 de compatibilidad de la superficie pública, la variante `library` |
| Numeración de `DX-Error-Messages.md` | §6 control de cambios antes de §7 cobertura, igual que en `GeometriaFactory-Application` tras su ronda 2 |
| Versión del intake citada en la cabecera | **1.11 en 25 de 27** documentos; los dos restantes (`02/README.md`, `03/Glosario-UX.md`) no citan versión de intake. **Ninguno cita 1.12** → H-02 |
| Versión del manifiesto citada | **1.2**, correcta y vigente |

**Observación, no hallazgo:** el propio intake 1.12 tiene en §20.E-8 **dos puntos numerados «5»** en su lista «Qué verificar». Es un defecto de forma de la fuente y no del proyecto auditado; se deja anotado para quien corrija el intake.

---

## 9. Hallazgos

### H-01 · **P0** · La decisión del intake 1.12 sobre el desenlace de E-8 no llegó, y el corpus afirma lo contrario de lo que la fuente dice

**Dónde está.** Nueve pasajes en siete documentos, tabulados en §3 de este informe. Los cinco que afirman en positivo son: `02/Especificacion-Funcional.md` §11 fila 1; `02/Definicion-Contrato-Del-Validador-De-Figuras.md` §6 fila E-8, §7 precisión 1 y §9 fila 1; `02/Casos-De-Uso/CU-01-…md` §10, quinta viñeta.

**Qué dice.** «**Ninguna fuente declara el resultado esperado para esta capa**: §20 lo declara borde de la pieza que dibuja y **su punto 4 dice que no prescribe el desenlace del envío**»; «**No hay resultado declarado**… Es un punto abierto»; «**Esta categoría no elige** y lo eleva».

**Qué debería decir.** Que el desenlace **está resuelto**: ante el texto de E-8 el validador emite una observación de especie **error de validación**, con **posición 1** y **campo `Largo`**, y por RN-05 el trabajo **queda en `Borrador`** y no pasa a `Pendiente`. Concretamente: la fila E-8 de `Definicion-Contrato…` §6 debe llevar resultado esperado; su §7 precisión 1 debe rehacerse (H-03); su §9 y la §11 del índice maestro deben **retirar** el punto abierto; `CU-01` debe incorporar un criterio de aceptación con el texto de E-8, con la misma forma que CA-04 ya tiene para E-5, y su §10 debe declarar que el payload **sí** es de este contrato en cuanto al desenlace, distinguiéndolo de la condición `DIMENSION_NO_LEGIBLE`, que sigue siendo de la fachada; `03/Guia-Onboarding-Developer.md`, los dos README y `DX-Error-Messages.md` §5 punto 3 deben dejar de remitir a un punto abierto que ya no existe.

**Cómo lo verifiqué.** Abrí `SDD/Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`, cabecera y control de cambios: versión **1.12**, con fila que declara la decisión. Leí §20.E-8 completo: el punto 4 vigente dice «decidir si el trabajo pasa a `Pendiente` es del validador, no del bundle (RA-02)» y **no** contiene la oración citada; el punto 5 declara el desenlace como error, con el trabajo en `Borrador`. Leí §21: la fila de E-8 declara «En el validador, **error**». Busqué la oración citada en todo el árbol: aparece **únicamente** en `SDD/Intake/_legacy/2026-08-08/PRODUCT-INTAKE-…-v1.7.md`, `-v1.8.md`, `-v1.10.md` y `-v1.11.md`, línea 1462-1464 según la versión, es decir en las **versiones superadas**. Barrido de `E-8` sobre los 27 documentos para localizar los nueve pasajes.

**Agravante.** La fila 1.12 del intake declara que fue **la emisión de la Fase B de este mismo proyecto de código** la que levantó el punto y provocó la decisión. La decisión se tomó a pedido de este corpus y no volvió a él.

---

### H-02 · **P1** · Los 27 documentos se emiten contra el `PRODUCT-INTAKE` 1.11, que ya estaba archivado

**Dónde está.** La línea «**Trazabilidad upstream:**» de 25 de los 27 documentos, y la fila 1.0 del control de cambios de `02/Especificacion-Funcional.md`: «Emisión inicial de la categoría para este proyecto de código, contra el `PRODUCT-INTAKE` **1.11** y el `PRODUCT-MANIFEST` **1.2**».

**Qué dice.** `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.11**.

**Qué debería decir.** **1.12**, previa revisión de qué más cambió entre las dos versiones. En este caso el delta es acotado —1.12 sólo toca E-8— y por eso el hallazgo es P1 y no P0 por sí mismo: su daño de contenido es exactamente H-01. Pero la citación de una versión archivada es la señal formal que hace que el defecto de contenido pase inadvertido en la siguiente lectura, y `Especificacion-Funcional.md` §6 declara «las quince reglas» y `§9` «las quince del producto» apoyándose en un documento que ya no es el vigente.

**Cómo lo verifiqué.** Extracción de la cadena «`PRODUCT-INTAKE-…` **1.1X**» de las cabeceras de los 27 archivos: 25 dicen 1.11, 2 no citan versión de intake, **ninguno dice 1.12**. Contrastado contra la cabecera y el control de cambios del intake vivo, que declara 1.12 y archiva 1.11. Verificado que el manifiesto sí está bien citado en 1.2.

---

### H-03 · **P1** · La precisión sobre la décima fila de §21 del intake es falsa contra la fuente vigente

**Dónde está.** `02/Definicion-Contrato-Del-Validador-De-Figuras.md` §7, precisión 1.

**Qué dice.** «**La matriz de §21 tiene diez filas y no nueve.** La décima —"dimensión no legible en el visor", E-8— **no pertenece a la batería obligatoria** de este proyecto de código: entró el 2026-08-09 con el contrato de la pieza que dibuja y **su lugar de verificación es la etapa de visualización**».

**Qué debería decir.** Que la décima fila **sí tiene tramo en este proyecto de código**. La fila vigente de §21 dice: «Dimensión no legible \| **E-8** \| En el visor, la pieza no se dibuja y se enumera con índice y código. **En el validador, error: el trabajo queda en `Borrador`** \| **Etapas `f` y `g`**». La etapa `f` es la del validador —lo confirman las otras nueve filas de esa misma matriz, que ubican en `f` todos los casos de interpretación—. La afirmación de que su lugar de verificación es sólo la visualización queda contradicha por la propia celda que la matriz lleva hoy.

Se separa de H-01 porque no es la misma oración ni el mismo argumento: H-01 es la cita de una oración retirada, y H-03 es una **caracterización de la matriz** que la matriz vigente desmiente. Cerrar H-01 sin tocar §7 dejaría el corpus internamente contradictorio: la batería obligatoria diría nueve casos mientras el resultado del décimo ya está declarado para este contrato.

**Cómo lo verifiqué.** Lectura de §21 del intake 1.12, tabla completa, con las diez filas y su columna «Dónde se ejercita»; contraste literal contra la precisión 1 de §7.

---

### H-04 · **P2** · El conjunto de «diez puntos abiertos» está declarado cerrado y no cierra: hay al menos seis más, y uno de ellos afirma falsamente estar en la lista

**Dónde está.** `02/Especificacion-Funcional.md` §11 («**Diez**, y ninguno bloqueante. **Cinco son propios** de esta categoría y **cinco** vienen declarados de aguas arriba»), replicado en `02/README.md` línea 96, en `03/README.md` línea 100 y en la fila 1.0 de control de cambios de `Especificacion-Funcional.md`.

**Qué dice.** Diez. La enumeración de los cinco propios es: E-8, tipos reconstruibles, cómo se sostiene «no se repite», longitud y alfabeto de la provisoria, vigencia del acceso firmado.

**Qué debería decir.** El recuento real de puntos abiertos que esta categoría declara es **al menos dieciséis**. Los seis que la §11 no recoge:

| Punto abierto declarado | Dónde se declara | ¿Está en §11? |
| --- | --- | --- |
| De dónde sale el valor derivado del área de una pieza volumétrica | `Definicion-Contrato…` §9 fila 3, y `CU-02` §10 | **No** |
| El límite de tamaño del texto que se acepta | `Definicion-Contrato…` §9 fila 4 | **No** |
| Zona horaria y precisión de los sellos | `Modelo-Conceptual.md` §7 fila 2, `CU-09` §10 y `RC-06` | **No** |
| Frecuencia del respaldo | `Modelo-Conceptual.md` §7 fila 3 | **No** |
| Fecha de última modificación de la cuenta | `Modelo-Conceptual.md` §7 fila 4 | **No** |
| La condición derivada `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO` | `CU-02` §10 última viñeta, y `DX-Error-Messages.md` §3.2 | **No**, pese a que `CU-02` afirma que sí |

El último caso es el más serio de los seis, porque **no es una omisión sino una afirmación falsa sobre un documento hermano**: `CU-02` §6, cierre, dice «*se declara como tal en §10 y en `Especificacion-Funcional.md` §11*», y §11 no lo contiene. Es del mismo tipo de defecto que H-01, en pequeño y hacia adentro.

**Precisión sobre el criterio.** Ninguno de los seis es hallazgo **por estar abierto**: los seis están correctamente declarados abiertos donde viven, con su motivo y su destinatario, y eso es legítimo. El hallazgo es que el índice maestro **declara un conjunto cerrado de diez** y ese conjunto no es el de la categoría; quien lea §11 para saber qué queda por decidir antes de la categoría 05 se va con seis decisiones menos de las que hay.

**Cómo lo verifiqué.** Barrido de «punto abierto», «puntos abiertos», «queda abierto» y «declarado abierto» sobre los 27 documentos; extracción de todas las tablas de puntos abiertos (`Especificacion-Funcional.md` §11, `Definicion-Contrato…` §9, `Modelo-Conceptual.md` §7) y de las viñetas de las §10 de los diez casos de uso; contraste uno a uno contra las diez filas de §11.

---

### H-05 · **P3** · `CU-04` atribuye a `CU-07` la conservación de la cuenta y sus trabajos, que `CU-07` no hace

**Dónde está.** `02/Casos-De-Uso/CU-04-Ejecutar-El-Borrado-Fisico-Y-El-Arrastre-De-La-Baja.md` §10, primera viñeta.

**Qué dice.** «**El reseteo de contraseña no pasa por acá y es deliberado.** `CU-07` conserva la cuenta y **todos** sus trabajos: el reseteo no dispara RN-07».

**Qué debería decir.** `CU-07` de esta carpeta es «Producir la contraseña provisoria del reseteo» y **no conserva nada**: no persiste, no toca la cuenta y no ve sus trabajos —lo declara su propia §7—. Lo que conserva la cuenta y sus trabajos es **RN-12**, cuyo tramo en esta capa `Especificacion-Funcional.md` §6 asigna a **CU-05** —que escribe la marca sin tocar el estado ni los trabajos— y a **CU-04 por contraste**. La viñeta debería decir «**RN-12** conserva la cuenta y todos sus trabajos» o «el reseteo, que se resuelve por CU-05 y CU-07, conserva…». El riesgo es real y no estilístico porque la §10 nota 1 del índice maestro advierte que los `CU-XX` de esta carpeta son **locales**, de modo que el lector resuelve la cita hacia el `CU-07` de acá, que es el equivocado.

**Cómo lo verifiqué.** Lectura de `CU-07` §4, §6 y §7 —no escribe, no persiste, no recibe dato de la cuenta— y de la fila RN-12 de `Especificacion-Funcional.md` §6, que asigna el tramo a CU-05 y CU-04.

---

### H-06 · **P3** · Se cita §14 «(RA-01 a RA-03)» como trazabilidad upstream y sólo se ejerce RA-03, sin declarar la no aplicabilidad de las otras dos

**Dónde está.** `02/Especificacion-Funcional.md`, línea de trazabilidad upstream: «§14 (**RA-01 a RA-03**)».

**Qué dice.** Que las tres reglas de arquitectura son insumo de esta categoría. Después, en los 27 documentos, **RA-01 y RA-02 no vuelven a aparecer ni una vez**, y RA-03 aparece siete veces sustantivas en cuatro documentos, con tratamiento completo.

**Qué debería decir.** O bien citar §14 sólo por RA-03, o —mejor, y es lo que hacen otros documentos de este mismo corpus con las categorías vacías y con la ausencia de `CU-09`— declarar en §4 que **RA-01 y RA-02 no tienen tramo en esta capa**, con el motivo: esta capa no tiene superficie de navegador ni bundle, no atiende peticiones y su único consumidor es la composición de raíz de la pieza de datos. El corpus **respeta** las dos reglas de hecho —`CU-01` CA-11 exige cero peticiones de red y `Definicion-Contrato…` §8 traza la frontera con el visor sin duplicar validación—, así que esto es una omisión de declaración y no un incumplimiento. Se levanta como P3 porque este proyecto de código declara explícitamente sus ausencias en todos los demás casos y aquí no lo hace, con lo que la asimetría se lee como olvido.

**Cómo lo verifiqué.** Barrido de `RA-01`, `RA-02` y `RA-03` sobre los 27 archivos: siete ocurrencias sustantivas de RA-03 en cuatro documentos, más tres en líneas de trazabilidad; cero ocurrencias de RA-01 y RA-02 fuera de la cadena de trazabilidad citada. Lectura de §14 del intake para confirmar el enunciado de las tres.

---

## 10. Lo que se verificó y quedó bien

Se deja escrito para que la ronda 2 no lo rehaga y para que la corrección de H-01 no lo degrade.

| Comprobación | Resultado |
| --- | --- |
| Diez casos de uso, serie contigua, catálogo coincidente | **Cierra** |
| 19 filas de condición, 17 distintas, 1 repetida con 2 reapariciones, 17 filas de tabla sin excedente | **Cierra**, y las cifras declaradas en `DX-Error-Messages.md` §7.1 y §7.2 son exactas fila por fila |
| Taxonomía 6+1+4+0+0+5+1 y terminación 11+4+2 | **Cierran las dos en 17** |
| Quince reglas, trece con tramo, dos sin él, tres con tramo principal acá | **Cierra** |
| Siete `RC-XX` con archivo propio | **Cierra** |
| Veinticinco US, US-01 a US-25, contra las §9 de los diez CU | **Cierra**, unión exacta |
| Cinco entidades, cuatro relaciones, cuatro conjuntos cerrados, nueve decisiones de almacenamiento | **Cierran** |
| Diecisiete términos acuñados en el glosario funcional | **Cierra** |
| Nueve NB con caso de uso, tres declaradas parciales, `CU-09` sin traza declarada en vez de forzada | **Correcto**, y la declaración de `CU-09` es la forma buena de tratar una ausencia |
| Nueve casos de la batería obligatoria contra criterios de aceptación de `CU-01` y `CU-02` | **Los nueve resuelven**: cada par (CU, CA) citado en `Definicion-Contrato…` §7 existe y dice lo que la matriz dice |
| Datos de prueba | **Ninguno inventado.** Los ocho escenarios se citan por el identificador del intake y sus valores —113.10 contra 113.09, 36.00 contra 54.00, 343.00 contra 1029.00, 686.00 coincidente— coinciden carácter por carácter con §20 |
| Operador estricto de la tolerancia | Transcrito sin margen, y `CU-02` **CA-09** lo ancla en prueba con 0.010 y 0.011, que es más de lo que la fuente exigía |
| Frontera mecanismo / decisión (§4) | Bien trazada y sostenida en los diez casos de uso: ninguno decide estado, autorización ni transición |
| Cobertura de RN-14, RN-08 y RN-07 | **Completa**, ver §6 |
| RA-03 | **Cumple**, ver §7 |
| Tablas bien formadas, versiones, control de cambios | **27 de 27** |
| Apartamiento declarado por el que se emite `Modelo-Datos/` en un `library` | **Correctamente declarado**, con su fundamento en el flag de persistencia y con la salida prevista si el orquestador decidiera lo contrario. No es hallazgo |

**Sobre el criterio negativo del encargo.** No se reporta ninguna polisemia: `Glosario-Funcional.md` §3 trata los cuatro términos de más de un referente que conviven en la misma sección —«validador», «repositorio», «derivado» y `Pendiente`— y §3.5 declara tres casos que deliberadamente no corrige por tener contextos disjuntos. Se revisó y **el criterio está bien aplicado**. Tampoco se reporta ningún punto abierto por estar abierto; el único hallazgo de esa familia, H-04, es de recuento y de una remisión falsa, no de apertura.

---

## 11. Veredicto y condiciones para promover

# RECHAZADO

**Fundamento.** El corpus es de calidad alta y no tiene ningún defecto de recuento, de forma, de identificadores ni de cobertura de las tres delegaciones que se le encomendaron. Pero **falla en lo que esta capa existe para decidir**. El validador del JSON del alumno es de este proyecto de código, el modo de falla más probable del producto es el que la configuración regional de la máquina produce —`"3,50"` con coma decimal—, el Product Owner ya decidió qué hace el validador con él, y los 27 documentos declaran que nadie lo decidió. No es una desactualización menor de referencia: hay **cinco pasajes que afirman en positivo que la fuente no lo prescribe**, cuando la fuente lo prescribe, y esa afirmación viaja al lugar exacto donde `08-Calidad-Y-Pruebas` va a buscar el resultado esperado y `05-Arquitectura-Tecnica` la tabla de derivación. Un P0 de esta clase —una afirmación falsa sobre otra fuente, en el punto de decisión propio de la capa— no se promueve.

**Condiciones para promover, en orden:**

1. **Cerrar H-01.** Incorporar el desenlace de E-8 a los cinco pasajes que hoy lo declaran indeterminado, retirar el punto abierto de `Especificacion-Funcional.md` §11 y de `Definicion-Contrato…` §9, dar resultado esperado a la fila E-8 de `Definicion-Contrato…` §6, y dotar a `CU-01` de un criterio de aceptación con el texto de E-8 —observación de especie error de validación, posición 1, campo `Largo`, primera pieza reconstruida—, con la forma que CA-04 ya tiene para E-5. Los cuatro pasajes que sólo remiten al punto abierto se actualizan por arrastre.
2. **Cerrar H-03** al mismo tiempo, rehaciendo la precisión 1 de `Definicion-Contrato…` §7 contra la fila vigente de §21, y decidiendo y declarando si la batería de este proyecto de código pasa a tener **diez** casos.
3. **Cerrar H-02** subiendo la citación a `PRODUCT-INTAKE` **1.12** en los 25 documentos que citan 1.11, **después** de 1 y 2 y no antes: cambiar la cita sin cambiar el contenido dejaría el corpus afirmando cosas falsas sobre una fuente que declara citar correctamente, que es peor que lo que hay hoy.
4. **Cerrar H-04** completando la §11 con los seis puntos abiertos que hoy quedan fuera y corrigiendo la remisión falsa de `CU-02` §6. Nótese que el punto abierto de E-8 **sale** de la lista en el paso 1, de modo que el recuento final debe recalcularse y no simplemente sumarse.
5. **Cerrar H-05 y H-06**, que son de una línea cada uno.

Los seis hallazgos son cerrables sin tocar ninguna decisión de diseño, ningún recuento que hoy cierra, ninguna partición de casos de uso y ninguno de los criterios de aceptación existentes. **La corrección es acotada y la ronda 2 debería ser corta.**

---

## Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial del informe. Auditoría de ronda 1 de la Fase B de `GeometriaFactory-Infrastructure`, categorías 02 y 03, sobre 27 documentos en 1.0, contra el `PRODUCT-INTAKE` 1.12, el `PRODUCT-MANIFEST` 1.2, las nueve necesidades de negocio y los tres proyectos de código vecinos aguas arriba. Seis hallazgos: un P0 —la decisión del intake 1.12 sobre el desenlace de E-8 no llegó y cinco pasajes afirman lo contrario de la fuente—, dos P1 —citación del intake 1.11 archivado, y caracterización falsa de la décima fila de §21—, un P2 —el conjunto de diez puntos abiertos no cierra y una remisión de `CU-02` es falsa— y dos P3. Recuentos recontados de forma independiente y **todos cerrados** salvo el de puntos abiertos; citas cruzadas de RN-14 verificadas exactas contra las tres capas que delegan; cobertura de RN-14, RN-08 y RN-07 verificada completa; RA-03 cumplida y RA-01 y RA-02 no aplicables sin declaración; 27 de 27 tablas bien formadas. **Veredicto: RECHAZADO.** |
