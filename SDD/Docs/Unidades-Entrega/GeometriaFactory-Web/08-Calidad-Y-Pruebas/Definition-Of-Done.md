# Definition of Done — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** Definition-Of-Done.md
**Versión:** 2.1
**Estado:** Propuesto
**Fecha:** 2026-08-16
**`tipo_unidad_entrega` (D8):** `web-monolith`
**Proyectos de código que la componen:** `GeometriaFactory-Web`, `GeometriaFactory-Visor` y `GeometriaFactory-Contracts`
**Consolida a:** el documento homónimo de `GeometriaFactory-Visor`, por `Audit/Migracion-M10-Consolidacion-Fusion.md` 1.2 §4

---

## 0. Cómo leer este documento

**La unidad de entrega tiene un solo documento de esta clase**, y cada sección lleva **una subsección
por proyecto de código**, con su texto **transpuesto sin reescritura**.

**Las dos secciones de cada apartado son la del portal y la del bundle del visor.** Las dos declaran las mismas secciones: la unidad de entrega es una y el visor viaja adentro.

---

## 1. DoD por capa

### 1.1 `GeometriaFactory-Web`

**Por qué la tercera capa se llama «etapa» y no «sprint».** Este producto no tiene sprints: la unidad de planificación es la **etapa**. Llamarla sprint habría creado una unidad que ninguna fuente tiene.

**Y por qué la cuarta se llama «publicación» y no «release».** Este proyecto de código **sí se entrega**, a diferencia de las bibliotecas del producto: se publica en el hosting público por el flujo de publicación. Pero **no se versiona como paquete redistribuible** —`redistribuible` es false—, de modo que lo que se declara terminado no es una versión liberada sino **una publicación que quedó en pie**.

Cada criterio responde a «¿cómo se valida?» con una operación concreta.

### 1.1 Historia de usuario

- [ ] Todos los criterios Given/When/Then de la historia están cubiertos por al menos un `TC-XX` de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md). **Se valida** leyendo la columna de tests de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2.
- [ ] Esos `TC-XX` están ejecutados y **pasan**. **Se valida** con el registro del guion de la etapa.
- [ ] **Si la historia introduce una acotación, existe un caso que la verifica forzando la solicitud sin pasar por la pantalla.** **Se valida** con los seis casos de `CV-05`. Una historia que sólo demuestre que el control no se dibuja **no está terminada**: eso acota lo que se ofrece y no prueba nada.
- [ ] La superficie que la historia declaró en su Definition of Ready criterio 4 **tiene sus filas de la matriz de sensado verificadas**, con estado y fecha. **Se valida** leyendo [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md).
- [ ] **Ninguna condición que la historia presenta está fuera de los diecisiete códigos vivos** ni del camino de ausencia de respuesta, y ninguna expone dirección, ruta ni traza. **Se valida** con `TC-10031`.
- [ ] La historia **no introdujo ninguna petición del navegador hacia el servicio de datos** ni ninguna salida nueva. **Se valida** con `TC-10029` y `TC-10030`.
- [ ] Si la historia toca la escena, **no introdujo invocaciones al interior del bundle** ni tráfico de circuito durante la interacción. **Se valida** con `TC-10032` y `TC-10033`.
- [ ] La construcción termina **sin advertencias**. **Se valida** con la etapa de construcción del flujo de publicación.

### 1.2 Tarea técnica

- [ ] Los criterios de aceptación que la tarea declara en [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md) se cumplen uno por uno. **Se valida** por inspección, por ejecución observada o por medición de la puerta que la tarea nombra.
- [ ] Si la propiedad que la tarea sostiene es una **ausencia** —cero peticiones del navegador, cero salidas nuevas, cero invocaciones al interior del bundle, cero apariciones de la credencial—, el criterio se midió **con umbral cero y en la condición declarada**. **Se valida** con el `TC-XX` de inspección correspondiente. En particular, **el conteo de peticiones se hace con los dos movimientos automáticos prendidos**: sin esa condición no cuenta.
- [ ] Si la tarea es de tipo indagación, la decisión que produjo está **registrada** en el documento que corresponde. **Se valida** leyendo ese documento.
- [ ] Si la tarea es una **puerta técnica**, se midió y su resultado quedó registrado, **y si no pasó, la salida declarada se ejecutó en lugar de arrastrarse como deuda**. **Se valida** con el informe de la medición.
- [ ] La construcción pasa entera y el bundle **se generó en el mismo flujo**, no se tomó de un artefacto viejo. **Se valida** con el registro del flujo de publicación.

### 1.3 Etapa

- [ ] Todas las historias de la épica de la etapa cumplen §1.1, y todas sus tareas técnicas cumplen §1.2. **Se valida** recorriendo el índice de la épica en [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §3.
- [ ] Los **once** criterios de salida de [`Plan-Pruebas.md`](Plan-Pruebas.md) §3 se cumplen. **Se valida** con esa lista.
- [ ] **El guion de demostración de la etapa y los de todas las anteriores pasan al 100 %, sin correcciones.** **Se valida** con `TC-10035` y `CV-23`. Ejecutar sólo el de la etapa en curso **no cumple**.
- [ ] Los criterios bloqueantes de [`Criterios-Validacion.md`](Criterios-Validacion.md) —`CV-13`, `CV-14` a `CV-18`, `CV-20` a `CV-22`, `CV-31` a `CV-35`— se cumplen. **Se valida** con el registro del flujo y con `TC-10029` a `TC-10033`.
- [ ] `CV-13` **se cumple**, y no sólo se midió: es **bloqueante** aunque su forma de puerta siga rotulada [ASUNCIÓN], porque el intake §22 `A-4` declara que lo que puede cambiar es la forma del gate y no su carácter. **Se valida** con la presencia de la medición **y de su resultado en verde** en el informe de cierre.
- [ ] **Las filas de [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) que la etapa toca están verificadas, con estado y fecha, y ninguna deriva mayor queda abierta.** **Se valida** recorriendo la matriz. Una deriva mayor se resuelve **corrigiendo lo construido o actualizando la línea de base con aprobación humana explícita**, nunca por omisión.
- [ ] Toda deriva **menor** quedó registrada aunque no bloquee. **Se valida** con `CV-27`.
- [ ] [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) está actualizada: ninguna fila dice `Pendiente` para un elemento que la etapa cerró. **Se valida** comparando la matriz contra el índice de la épica.
- [ ] Todo defecto cerrado en la etapa generó al menos un `TC-XX`. **Se valida** con `CV-28`.
- [ ] El punto de control de la etapa tiene el **OK explícito del Product Owner**, con constancia escrita. **Se valida** con el informe de cierre (intake §15, reglas de delivery 2 y 3).

### 1.4 Publicación

Se aplica cada vez que el flujo de publicación corre hacia el hosting público.

- [ ] La construcción terminó **sin advertencias** y el bundle se generó **en el mismo flujo**. **Se valida** con el registro del flujo (intake §17.2.P.8 · GeometriaFactory-Web).
- [ ] La dirección del servicio de datos se inyectó desde los secretos y **la dirección real del servidor propio no quedó versionada**. **Se valida** por inspección del repositorio y del registro del flujo.
- [ ] **El flujo no terminó en la subida: terminó comprobando que la dirección pública responde.** **Se valida** con el paso final del flujo. Es la única forma de que una subida no transaccional que deja la aplicación caída no se reporte como exitosa.
- [ ] La etiqueta de la etapa existe y permite volver a cualquier demostración ya aprobada. **Se valida** con el registro de la etiqueta.
- [ ] Si la publicación no dejó la aplicación en pie, **se volvió a publicar desde la etiqueta anterior**. **Se valida** con el registro de la reversión.
- [ ] La publicación se hizo **fuera del horario de uso**, porque la subida no es transaccional. **Se valida** con la hora registrada del flujo.

### 1.2 `GeometriaFactory-Visor`

**Por qué la tercera capa se llama «momento del producto».** El producto no tiene sprints y este proyecto de código no se organiza sólo por etapas: su momento central es el de la **medición de `PT-02` y `PT-03`**, que el roadmap §2.2 ubica antes de comprometer la etapa `g` y que `06` §2.1 declara como épica sin crear etapa nueva. Llamar «sprint» o «etapa» a esa capa habría inventado una unidad que ninguna fuente tiene.

**Por qué la cuarta se llama «entrega del proyecto de código».** El bundle **no se publica en ningún repositorio de paquetes**: `redistribuible` es false y su artefacto se copia al directorio de recursos estáticos del anfitrión.

### 1.1 Historia de usuario

- [ ] Todos los criterios Given/When/Then de la historia están cubiertos por al menos un `TC-XX`. **Se valida** leyendo la columna de test de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2 para su `CU-XX`.
- [ ] Esos `TC-XX` están escritos y **en verde**.
- [ ] **Toda garantía que la historia declara ejercer tiene su fila en la matriz §5 con este `TC-XX` entre sus tests**, y ninguna afirmación de la historia contradice a otra garantía. **Se valida** leyendo esa tabla. Es el criterio 5 de la DoR verificado del lado del cierre.
- [ ] Todo código de condición que la historia usa es **uno de los siete**, y la historia **no acuñó ninguno**. **Se valida** con `TC-12021`.
- [ ] Si la historia entrega una **ausencia**, su criterio se verificó con **umbral cero y con su condición de medición registrada**. **Se valida** leyendo el registro de la medición. Un umbral cero sin condición **no cumple**.
- [ ] Ninguna persona, papel, servicio ni credencial interviene como actor ni condiciona un flujo. **Se valida** leyendo la historia y su caso de prueba.
- [ ] El bundle se genera sin errores. **Se valida** con el guion de construcción del bundle.

### 1.2 Tarea técnica

- [ ] Los criterios de aceptación que la tarea declara en [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md) se cumplen uno por uno. **Se valida** por inspección, por prueba automatizada o por medición de la puerta que la tarea nombra.
- [ ] Si la tarea sostiene una **ausencia**, su criterio se expresó con umbral cero **y con la condición en que se mide**. **Se valida** leyendo el criterio: sin condición, la tarea ni siquiera cumplía la DoR §2.
- [ ] Si la tarea es de tipo indagación, la decisión que produjo está **registrada** y no sólo tomada. **Se valida** leyendo el documento donde quedó.
- [ ] Ninguna dependencia introducida cruza la regla de dependencias entre capas. **Se valida** con `CV-29`.
- [ ] Si la tarea mide una puerta —`BT-12013`, `BT-12014`, `BT-12016`— el resultado quedó registrado con su condición de medición. **Se valida** con el informe.

### 1.3 Momento del producto

- [ ] Todas las historias de la épica cumplen §1.1, y todas sus tareas técnicas cumplen §1.2.
- [ ] Los **diez** criterios de salida de [`Plan-Pruebas.md`](Plan-Pruebas.md) §3 se cumplen.
- [ ] Los criterios bloqueantes de [`Criterios-Validacion.md`](Criterios-Validacion.md) —`CV-29` a `CV-31`— se cumplen.
- [ ] **En el momento de medición: `PT-02` y `PT-03` pasan enteras**, en sus **seis** tramos `CV-18` a `CV-23`. **Se valida** con `TC-12019` y `TC-12020`. **Si alguna no pasa, la etapa `g` no se compromete**: no hay diferimiento, no hay deuda y no hay carácter condicionado.
- [ ] Toda medición de ausencia se hizo **con su condición** y quedó registrada junto al resultado. **Se valida** con el informe de cierre.
- [ ] La batería completa —y no sólo lo que el momento tocó— corre y pasa. **Se valida** con `CV-24`.
- [ ] Ningún `TC-XX` que estaba en verde pasó a rojo sin justificación escrita. **Se valida** con `CV-25`.
- [ ] [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) está actualizada en sus cinco tablas.
- [ ] [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) tiene el estado y la fecha de verificación de cada fila que el momento toca. **Se valida** leyendo su columna de estado.
- [ ] Todo defecto cerrado generó al menos un `TC-XX`. **Se valida** con `CV-26`.
- [ ] Si el momento propuso una función nueva en la fachada, los **seis** pasos de [`../05-Arquitectura-Tecnica/Extensibilidad.md`](../05-Arquitectura-Tecnica/Extensibilidad.md) §5 se recorrieron enteros, incluida la consolidación en el intake. **Se valida** leyendo el intake §17.2.P.3 · GeometriaFactory-Visor.
- [ ] El punto de control tiene el **OK explícito del Product Owner**.

### 1.4 Entrega del proyecto de código

Se aplica cuando la etapa `a`, el momento de medición y la etapa `g` están cerrados.

- [ ] Los **treinta y cuatro** criterios de [`Criterios-Validacion.md`](Criterios-Validacion.md) están evaluados uno por uno, con su resultado registrado.
- [ ] **7 de 7** casos de uso, **6 de 6** funciones, **7 de 7** garantías, **7 de 7** códigos en sus **8** cursos, **14 de 14** historias y **8 de 8** NFR con caso de prueba en verde.
- [ ] Las **seis** propiedades transversales verificadas **con sus condiciones de medición** y reverificadas después de incorporar el gobierno en vivo de los movimientos. **Se valida** con `CV-27`.
- [ ] Los **ocho** escenarios del intake §20 siguen siendo el material de los casos de prueba que los usan. **Se valida** con `CV-08`.
- [ ] **`PT-02` y `PT-03` pasadas**, con su registro.
- [ ] Los **ocho** compromisos de un reemplazo de la capa 3 están verificables sin backend. **Se valida** con [`Guia-Testing-Extensibilidad.md`](Guia-Testing-Extensibilidad.md) §3.
- [ ] El sample **S-1** ejerce las **seis** funciones enteras, en **cinco pasos o menos**. **Se valida** con `TC-12015`.
- [ ] Los puntos abiertos de `05` §11 tienen desenlace declarado, o su continuidad como abiertos está registrada: hoy son **cinco**, `PA-01` a `PA-05`.
- [ ] El bundle es un **artefacto generado y reproducible**, nunca editado a mano. **Se valida** con `CV-30`.

## 2. Excepciones admitidas

### 2.1 `GeometriaFactory-Web`

| Caso | Qué se flexibiliza | Quién lo aprueba | Qué queda registrado |
| --- | --- | --- | --- |
| **`CV-10013` no alcanzado** | **Nada.** No es condicionado: lo rotulado [ASUNCIÓN] es la **forma de la puerta**, y el intake §22 `A-4` deja a salvo su carácter bloqueante | El Product Owner, con constancia escrita, como en cualquier criterio bloqueante | La medición, su distancia al umbral y la remediación, en el informe de cierre |
| Cobertura de líneas **no exigible** | El criterio `CV-10030` se declara «no aplica» mientras no exista proyecto de pruebas propio | — | El fundamento del intake §17.2.P.6 · GeometriaFactory-Web |
| Deriva **menor** | Se registra y **no bloquea** el cierre | — | La fila de la matriz, con su estado y su fecha |
| Deuda técnica que una etapa no alcanza a cerrar | El criterio se difiere **una sola vez**, y sólo si no es de los bloqueantes de §1.3 | El Product Owner, en el punto de control | Una `BT-XX` nueva, con la etapa en que se cierra |
| **Puerta técnica que no pasa** | **No se admite excepción.** El intake §15 declara que detiene la planificación de las etapas que dependen de ella y **no se arrastra como deuda**. Lo que se ejecuta es la salida que la puerta declara | El Product Owner decide la salida, no la excepción | La medición y la salida ejecutada |
| **Deriva mayor sin resolver** | **No se admite.** Se corrige lo construido o se actualiza la línea de base con aprobación humana explícita | — | — |
| **Guion ejecutado sólo para la etapa en curso** | **No se admite.** Es la regla de no-regresión acumulativa del intake §15, y **no es la parte rotulada [ASUNCIÓN]** | — | — |
| **Acotación dada por verificada mirando la pantalla** | **No se admite.** Esta pieza no hace cumplir reglas: si no se forzó la solicitud, no se verificó nada | — | — |

### 2.2 `GeometriaFactory-Visor`

| Caso | Qué se flexibiliza | Quién lo aprueba | Qué queda registrado |
| --- | --- | --- | --- |
| **Umbral de fluidez inexistente** | La verificación es cualitativa declarada junto con `PT-02`. **No habilita a inventar un número** | El Product Owner, o esta categoría al fijar su guion de medición (`BT-12018`) | El guion cualitativo y su resultado, rotulado como cualitativo |
| Deuda técnica que un momento no alcanza a cerrar | Se difiere **una sola vez**, y nunca si es de los bloqueantes de §1.3 | El Product Owner | Una `BT-XX` nueva con el momento en que se cierra |
| **`PT-02` o `PT-03` que no pasan** | **Ninguna excepción.** La etapa `g` no se compromete | — | — |
| Medición de ausencia **sin su condición** | **No se admite.** No cuenta como medición | — | — |
| Historia que rompe una garantía o acuña un código | **No se admite**, y es la misma prohibición que la DoR §3 declara del lado de la entrada | — | — |

## 3. Vigencia

### 3.1 `GeometriaFactory-Web`

**Este documento es la fuente canónica de la Definition of Done de `GeometriaFactory-Web`.**

- [`../07-Plan-Sprint/Mini-Plan.md`](../07-Plan-Sprint/Mini-Plan.md) y cualquier plan de etapa **referencian** esta DoD y no la redefinen. Una lista de criterios de cierre escrita en un plan es un hallazgo, y el que rige es éste.
- [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/Definition-Of-Ready.md) §5 declara que la DoD vive en esta categoría y que hasta su emisión regían los criterios de transición del roadmap §5, que son de nivel producto. **Con esta emisión ese interinato termina**: los criterios del roadmap siguen valiendo a nivel producto y esta DoD los complementa a nivel de proyecto de código, sin contradecirlos.
- **Los umbrales de deriva de [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) no se cambian desde este documento.** Esta DoD exige que la matriz se verifique y que ninguna deriva mayor quede abierta; qué constituye deriva mayor lo declara la matriz, y cambiarlo requiere aprobación humana sobre la línea de base.
- Todo cambio en los criterios de §1 se registra en §4 y se comunica en el punto de control de la etapa siguiente.
- La DoD **no habla de cuándo empezar**: eso es la Definition of Ready, y las dos no se solapan.

### 3.2 `GeometriaFactory-Visor`

**Este documento es la fuente canónica de la Definition of Done de `GeometriaFactory-Visor`.**

- [`../07-Plan-Sprint/Mini-Plan.md`](../07-Plan-Sprint/Mini-Plan.md) y cualquier plan **referencian** esta DoD y no la redefinen.
- [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/Definition-Of-Ready.md) §5 declara que la DoD vive en esta categoría y que hasta su emisión gobernaban el cierre los criterios de transición del roadmap §5 y las dos puertas técnicas. **Con esta emisión ese interinato termina**: los criterios de transición del roadmap siguen valiendo a nivel producto, las dos puertas siguen siendo del intake, y esta DoD las incorpora sin redefinirlas.
- Esa misma sección de la DoR nombra tres condiciones de cierre que **no son suyas**: los diez recorridos sin degradación, la medición de peticiones con los movimientos prendidos y la página integradora funcionando. **Las tres viven acá**, en `CV-12014`, `CV-12010` y `CV-12012` respectivamente, y su ubicación queda así confirmada.
- Todo cambio en los criterios de §1 se registra en §4 y se comunica en el punto de control siguiente.

## 4. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 2.1 | 2026-08-29 | **Tramo `R-4` · renumerado de `QG` y `CV` al mapa de bloques del destino**, decidido por el Product Owner el 2026-08-29 al **retirar el `ADR-14005`** en lugar de aceptarlo. **3 línea(s)** pasan de `QG-NN` a `QG-<bloque>NNN`, con el bloque **deducido de la línea o de la sección y nunca inventado** — `00` Api, `02` Domain, `04` Application, `06` Infrastructure, `08` Contracts, `10` Web, `12` Visor. Con esto las dos familias **dejan de necesitar apartamiento**: cumplen [`../../../Producto/Norma-De-Nomenclatura.md`](../../../Producto/Norma-De-Nomenclatura.md) y `Root-Rules.md` §9.1 y §9.2. Las referencias cuyo bloque no estaba en el texto **conservan la forma vieja a propósito** y quedan inventariadas en [`../../../Audit/Inventario-Renumerado-R-4-2026-08-29.md`](../../../Audit/Inventario-Renumerado-R-4-2026-08-29.md). Se respeta §4.1: no se tocan las filas de control de cambios ni lo que está entre «…». |
| 2.0 | 2026-08-16 | **Consolidación de la fusión.** Pasa a ser el documento de la **unidad de entrega**, absorbiendo el de `GeometriaFactory-Visor`, con su texto transpuesto sin reescritura. Entra §0. Sube **major**. |
