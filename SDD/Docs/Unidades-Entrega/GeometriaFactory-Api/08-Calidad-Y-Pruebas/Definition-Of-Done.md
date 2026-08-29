# Definition of Done — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** Definition-Of-Done.md
**Versión:** 2.1
**Estado:** Propuesto
**Fecha:** 2026-08-16
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**`tipo_unidad_entrega` (D8):** `rest-api` · **Unidad de entrega principal del producto**
**Proyectos de código que la componen:** `GeometriaFactory-Api`, `GeometriaFactory-Domain`, `GeometriaFactory-Application`, `GeometriaFactory-Infrastructure` y `GeometriaFactory-Contracts`
**Trazabilidad upstream:** [`Estrategia-Calidad.md`](Estrategia-Calidad.md); [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **2.1** §17.1.P.6 y §22
**Trazabilidad downstream:** `09-Devops` y `11-Documentacion`
**Consolida a:** los documentos homónimos de `GeometriaFactory-Domain`, `GeometriaFactory-Application` e `GeometriaFactory-Infrastructure`, por `Audit/Migracion-M10-Consolidacion-Fusion.md` 1.1 §4

---

## 0. Cómo leer este documento

**La unidad de entrega tiene un solo documento de esta clase, y sus cuatro proyectos de código tenían
el suyo.** Cada sección lleva **una subsección por proyecto**, con su texto **transpuesto sin
reescritura**: lo que cambia es el orden y no el contenido.

**Las tres secciones son comunes a las cuatro capas, y la definición de terminado de la unidad de
entrega es la conjunción de las cuatro.** Un incremento está terminado cuando lo está en las cuatro,
no cuando lo está en la que lo tocó.

---

## 1. DoD por capa

### 1.1 `GeometriaFactory-Api`

**Por qué la tercera capa se llama «etapa» y no «sprint».** Este producto no tiene sprints: la unidad de planificación es la **etapa**.

**Y por qué la cuarta se llama «entrega del artefacto» y no «release».** Este proyecto de código **sí produce un artefacto entregable** —la imagen que corre en el servidor propio— pero `redistribuible` es false y **no se publica en ningún registro**: se construye en destino. Además **el despliegue no es del agente**: el intake §17.1.P.8 · GeometriaFactory-Api lo declara manual y del Product Owner. Lo que esta DoD declara terminado es **el artefacto entregado**, no el despliegue realizado.

Cada criterio responde a «¿cómo se valida?» con una operación concreta.

### 1.1 Historia de usuario

- [ ] Todos los criterios Given/When/Then de la historia están cubiertos por al menos un `TC-XX` de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md). **Se valida** leyendo la columna de tests de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2.
- [ ] Esos `TC-XX` están escritos y **pasan**. **Se valida** con la salida de `scripts/test.sh`.
- [ ] **Si la historia agrega o modifica un punto de acceso, quedó declarado si está dentro de la guardia o si es una de las cuatro exenciones con su motivo, y `TC-00007` se reejecutó.** **Se valida** con la tabla de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §5. **Una historia que agregue un punto y no reejecute `TC-00007` no está terminada**: es el primer riesgo de `05` §9, y es un defecto de **omisión** que no se ve leyendo el punto nuevo.
- [ ] **Si la historia agrega una respuesta de fallo, `TC-00024`, `TC-00025`, `TC-00026` y `TC-00027` se reejecutaron.** **Se valida** con esos cuatro casos. Ninguna familia empobrecida puede haberse enriquecido.
- [ ] **Ninguna condición que la historia presenta está fuera del conjunto cerrado de diecisiete códigos**, y ninguna se acuñó, renombró ni tradujo a texto acá. **Se valida** con `TC-00027`.
- [ ] **Si la historia introduce una propiedad de ausencia** —cero exposiciones, cero truncamientos, cero eliminaciones fuera de alcance—, se midió **con umbral cero y en la condición declarada**. **Se valida** con el `TC-XX` correspondiente.
- [ ] **Si la historia introduce una acotación, existe un caso que la verifica forzando la petición.** **Se valida** con `TC-00020` para la eliminación, que es la que la fuente exige así.
- [ ] La construcción termina en 0 y sin advertencias. **Se valida** con la salida de `scripts/build.sh`.
- [ ] La cobertura del componente que la historia toca no bajó respecto de la medición anterior. **Se valida** comparando el informe de cobertura por componente.

### 1.2 Tarea técnica

- [ ] Los criterios de aceptación que la tarea declara en [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md) se cumplen uno por uno. **Se valida** por inspección, por prueba automatizada o por medición de la puerta que la tarea nombra.
- [ ] Si la propiedad que la tarea sostiene es una **ausencia**, el criterio se midió **con umbral cero y en la condición declarada**, y no se dio por cumplido por no haberse observado lo contrario. **Se valida** con el `TC-XX` de inspección correspondiente.
- [ ] Si la tarea es de tipo indagación, la decisión que produjo está **registrada** en el documento que corresponde. **Se valida** leyendo ese documento.
- [ ] Si la tarea es una **puerta técnica**, se midió y su resultado quedó registrado, **y si no pasó, la salida declarada se ejecutó en lugar de arrastrarse como deuda**. **Se valida** con el informe de la medición.
- [ ] Si la tarea toca la composición de raíz, **la resolución de los cuatro puertos se verifica en el arranque y falta alguno falla en construcción**. **Se valida** con `TC-00028`.
- [ ] La construcción y las dos baterías —unitaria y de integración— pasan enteras. **Se valida** con `scripts/build.sh` y `scripts/test.sh`.

### 1.3 Etapa

- [ ] Todas las historias de la épica de la etapa cumplen §1.1, y todas sus tareas técnicas cumplen §1.2. **Se valida** recorriendo el índice de la épica en [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §3.
- [ ] Los **doce** criterios de salida de [`Plan-Pruebas.md`](Plan-Pruebas.md) §3 se cumplen. **Se valida** con esa lista.
- [ ] **`TC-00007` cierra con 4 y 11 sobre los quince puntos, en las dos direcciones.** **Se valida** con la tabla de la matriz §5.
- [ ] **`TC-00025` da 3 de 3 comparaciones idénticas.** **Se valida** con ese caso.
- [ ] **A partir de la etapa `f`: la batería del validador que corre desde acá pasa entera, 10 de 10.** **Se valida** con `CV-31`. **Nueve casos no cumplen**, y el motivo está en [`Criterios-Validacion.md`](Criterios-Validacion.md) §6.
- [ ] Los criterios bloqueantes de [`Criterios-Validacion.md`](Criterios-Validacion.md) —`CV-16` a `CV-27`, `CV-35` a `CV-40`— se cumplen. **Se valida** con el informe del pipeline y con los casos nombrados.
- [ ] Los criterios condicionados —`CV-11` a `CV-15`, `CV-33`— **se midieron y se registraron**, aunque no bloqueen. **Se valida** con la presencia de la medición en el informe de cierre. Registrar «sin medir» cuando la medición era posible **no cumple**.
- [ ] Las dos baterías completas —y no sólo lo que la etapa tocó— corren y pasan. **Se valida** con `CV-28`.
- [ ] Ningún caso de verificación que pasaba dejó de pasar sin justificación escrita. **Se valida** con `CV-29`.
- [ ] [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) está actualizada, **incluida su tabla de quince puntos de acceso**. **Se valida** comparando la matriz contra el índice de la épica.
- [ ] Todo defecto cerrado en la etapa generó al menos un `TC-XX`. **Se valida** con `CV-32`.
- [ ] El punto de control de la etapa tiene el **OK explícito del Product Owner**, con constancia escrita. **Se valida** con el informe de cierre (intake §15, reglas de delivery 2 y 3).

### 1.4 Entrega del artefacto

Se aplica cada vez que el artefacto del servidor propio se construye para entregarse.

- [ ] **`PT-04` pasa**: la imagen se construye con su archivo de construcción **multietapa**, arranca desde el contenedor de desarrollo, **aplica las transformaciones sobre un almacén vacío y responde salud**. **Se valida** con la medición registrada de la puerta.
- [ ] La imagen final lleva **sólo el entorno de ejecución**, sin kit de desarrollo ni depurador, y **no tiene linaje con la imagen del contenedor de desarrollo**. **Se valida** por inspección del archivo de construcción.
- [ ] **Ningún secreto entra al repositorio ni a la imagen.** La clave de firma y la ubicación del almacén llegan por variable de entorno o archivo montado. **Se valida** por inspección del repositorio, del archivo de construcción y del de composición.
- [ ] El almacén apunta a un **volumen persistente** y no a una ruta dentro de la imagen. **Se valida** por inspección del archivo de composición.
- [ ] La etiqueta de la etapa existe y permite **volver a cualquier demostración ya aprobada**. **Se valida** con el registro de la etiqueta.
- [ ] **El artefacto queda entregado, no desplegado.** El archivo de construcción y el de composición se entregan; **el despliegue lo ejecuta el Product Owner**. **Se valida** con la constancia de la entrega en el informe de cierre.
- [ ] La reversión está disponible: **volver a la etiqueta anterior y reconstruir**. **Se valida** con la etiqueta previa existente.

### 1.2 `GeometriaFactory-Domain`

**Por qué la tercera capa se llama «etapa» y no «sprint».** Este producto no tiene sprints: la unidad de planificación es la **etapa**, y así lo declaran el roadmap §1.2 y [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §4.1. Llamarla sprint habría creado una unidad que ninguna fuente tiene. La cuarta capa se llama «entrega del proyecto de código» y no «release» porque **este proyecto de código no se publica**: `redistribuible` es false y no viaja a ningún repositorio de paquetes (`05` §5).

Cada criterio responde a «¿cómo se valida?» con una operación concreta.

### 1.1 Historia de usuario

- [ ] Todos los criterios Given/When/Then de la historia están cubiertos por al menos un `TC-XX` de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md). **Se valida** leyendo la columna de test de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2 para el `CU-XX` de la historia.
- [ ] Esos `TC-XX` están escritos y **en verde**. **Se valida** con la salida de `scripts/test.sh`.
- [ ] Toda regla e invariante que la historia declara ejercer tiene su fila en la matriz §4 y §5 con este `TC-XX` entre sus tests. **Se valida** leyendo esas dos tablas.
- [ ] Toda condición de rechazo que la historia produce está en el catálogo de [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md) y alcanzada por prueba. **Se valida** con `TC-02023`.
- [ ] La historia no introdujo ninguna dependencia saliente. **Se valida** con `TC-02024`.
- [ ] La construcción termina en 0 y sin advertencias. **Se valida** con la salida de `scripts/build.sh`.
- [ ] La cobertura del componente que la historia toca no bajó respecto de la medición anterior. **Se valida** comparando el informe de cobertura por componente.

### 1.2 Tarea técnica

- [ ] Los criterios de aceptación que la tarea declara en [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md) se cumplen uno por uno. **Se valida** por inspección, por prueba automatizada o por medición de la puerta que la tarea nombra, según lo exige la DoR §2 criterio 3.
- [ ] Si la tarea es de tipo indagación, la decisión que produjo está **registrada** en el documento que corresponde, y no sólo tomada. **Se valida** leyendo ese documento.
- [ ] Si la tarea cierra un punto abierto de `05` §11 o de [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §6, ese punto queda declarado cerrado con su desenlace. **Se valida** leyendo la tabla de puntos abiertos.
- [ ] Si la tarea es una puerta —`BT-02004`, `BT-02005`, `BT-02008`, `BT-02014`— la puerta se midió al menos una vez y su resultado quedó registrado. **Se valida** con la salida del pipeline.
- [ ] La construcción y la batería pasan enteras. **Se valida** con `scripts/build.sh` y `scripts/test.sh`.

### 1.3 Etapa

- [ ] Todas las historias de la épica de la etapa cumplen §1.1, y todas sus tareas técnicas cumplen §1.2. **Se valida** recorriendo el índice de la épica en [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §3.
- [ ] Los **nueve** criterios de salida de [`Plan-Pruebas.md`](Plan-Pruebas.md) §3 se cumplen. **Se valida** con esa lista.
- [ ] Los criterios bloqueantes de [`Criterios-Validacion.md`](Criterios-Validacion.md) —`CV-10` a `CV-13`, `CV-20` a `CV-22`— se cumplen. **Se valida** con el informe del pipeline y con `TC-02023`, `TC-02024`, `TC-02026` y `TC-02027`.
- [ ] Los criterios condicionados —`CV-08`, `CV-09`, `CV-18`— **se midieron y se registraron**, aunque no bloqueen. **Se valida** con la presencia de la medición en el informe de cierre. Registrar «sin medir» cuando la medición era posible **no cumple**.
- [ ] La batería completa —y no sólo lo que la etapa tocó— corre y pasa. **Se valida** con `CV-14`.
- [ ] Ningún `TC-XX` que estaba en verde pasó a rojo sin justificación escrita. **Se valida** con `CV-15`.
- [ ] [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) está actualizada: ninguna fila dice `Pendiente` para un elemento que la etapa cerró. **Se valida** comparando la matriz contra el índice de la épica.
- [ ] Todo defecto cerrado en la etapa generó al menos un `TC-XX`. **Se valida** con `CV-16`.
- [ ] El punto de control de la etapa tiene el **OK explícito del Product Owner**, con constancia escrita. **Se valida** con el informe de cierre de la etapa (intake §15).

### 1.4 Entrega del proyecto de código

Se aplica cuando las **seis** etapas que este proyecto de código toca —`a`, `c`, `d`, `e`, `f` y `h`— están cerradas.

- [ ] Los **veintidós** criterios de [`Criterios-Validacion.md`](Criterios-Validacion.md) están evaluados uno por uno, con su resultado registrado. **Se valida** con ese documento.
- [ ] **13 de 13** casos de uso, **16 de 16** reglas, **9 de 9** invariantes y **27 de 27** historias con caso de prueba en verde. **Se valida** con los recuentos de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md).
- [ ] **42 de 42** condiciones alcanzadas y **0** fuera del catálogo. **Se valida** con `TC-02023`.
- [ ] Los **ocho** escenarios del intake §20 siguen siendo el material de los casos de prueba que los usan, sin sustitución por datos sintéticos. **Se valida** con `CV-06` y con el recuento de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) §3.
- [ ] Los dos valores rotulados **[ASUNCIÓN]** están confirmados por el Product Owner, o su continuidad como asunción está declarada. **Se valida** leyendo el intake §22 y el estado de `BT-02015`.
- [ ] No queda ningún punto abierto de `05` §11 sin desenlace declarado. **Se valida** leyendo esa tabla.
- [ ] La versión de la biblioteca está calculada según la estrategia de versionado del intake §17.1.P.7 · GeometriaFactory-Domain y la etiqueta de la etapa existe. **Se valida** con el registro de la etiqueta.

### 1.3 `GeometriaFactory-Application`

**Por qué la tercera capa se llama «etapa» y no «sprint».** Este producto no tiene sprints: la unidad de planificación es la **etapa**, y así lo declaran el roadmap y [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §2. Llamarla sprint habría creado una unidad que ninguna fuente tiene. La cuarta capa se llama «entrega del proyecto de código» y no «release» porque **este proyecto de código no se publica**: `redistribuible` es false y no viaja a ningún repositorio de paquetes (`05` §5).

Cada criterio responde a «¿cómo se valida?» con una operación concreta.

### 1.1 Historia de usuario

- [ ] Todos los criterios Given/When/Then de la historia están cubiertos por al menos un `TC-XX` de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md). **Se valida** leyendo la columna de tests de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2 para el `CU-XX` de la historia.
- [ ] Esos `TC-XX` están escritos y **en verde**. **Se valida** con la salida de `scripts/test.sh`.
- [ ] **La comprobación de autorización que la historia declaró en su Definition of Ready criterio 5 tiene prueba de su negativa.** **Se valida** leyendo la matriz §5. Una historia que dijo «ninguna me alcanza» y resultó tocar una operación que lee o escribe **no está terminada**.
- [ ] Toda regla e invariante que la historia declara ejercer tiene su fila en la matriz §4 y §6 con este `TC-XX` entre sus tests. **Se valida** leyendo esas dos tablas.
- [ ] Toda condición de rechazo que la historia produce está en el catálogo de las **36** y alcanzada por prueba. **Se valida** con `TC-04028`.
- [ ] La historia **no introdujo ninguna prueba que abra el almacén real** ni ninguna dependencia saliente nueva. **Se valida** con `TC-04026` y `TC-04027`.
- [ ] Los `TC-XX` de la historia usan **dobles de puerto y no dobles de componente interno**. **Se valida** por inspección del código de prueba, contra [`Estrategia-Testing.md`](Estrategia-Testing.md) §5.
- [ ] La construcción termina en 0 y sin advertencias. **Se valida** con la salida de `scripts/build.sh`.
- [ ] La cobertura del componente que la historia toca no bajó respecto de la medición anterior. **Se valida** comparando el informe de cobertura por componente.

### 1.2 Tarea técnica

- [ ] Los criterios de aceptación que la tarea declara en [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md) se cumplen uno por uno. **Se valida** por inspección, por prueba automatizada o por medición de la puerta que la tarea nombra, según lo exige la Definition of Ready §2 criterio 3.
- [ ] Si la propiedad que la tarea sostiene es una **ausencia** —cero dependencias de más, cero pruebas que tocan la base, cero componentes cargados—, el criterio se midió **con umbral cero y en la condición declarada**, y no se dio por cumplido por no haberse observado lo contrario. **Se valida** con el `TC-XX` de inspección correspondiente.
- [ ] Si la tarea es de tipo indagación, la decisión que produjo está **registrada** en el documento que corresponde, y no sólo tomada. **Se valida** leyendo ese documento.
- [ ] Si la tarea **acompaña** un punto abierto cuya titularidad es de otro proyecto de código —`BT-04020`, `BT-04021`—, declaró de quién es la decisión y no la tomó por su cuenta. **Se valida** leyendo la fila de la tarea.
- [ ] Si la tarea es una puerta —`BT-04004`, `BT-04005`, `BT-04006`, `BT-04018`, `BT-04019`— la puerta se midió al menos una vez y su resultado quedó registrado. **Se valida** con la salida del pipeline.
- [ ] La construcción y la batería pasan enteras. **Se valida** con `scripts/build.sh` y `scripts/test.sh`.

### 1.3 Etapa

- [ ] Todas las historias de la épica de la etapa cumplen §1.1, y todas sus tareas técnicas cumplen §1.2. **Se valida** recorriendo el índice de la épica en [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §3.
- [ ] Los **once** criterios de salida de [`Plan-Pruebas.md`](Plan-Pruebas.md) §3 se cumplen. **Se valida** con esa lista.
- [ ] Los criterios bloqueantes de [`Criterios-Validacion.md`](Criterios-Validacion.md) —`CV-11` a `CV-17`, `CV-25` a `CV-28`— se cumplen. **Se valida** con el informe del pipeline y con `TC-04011`, `TC-04026`, `TC-04027`, `TC-04028`, `TC-04029`, `TC-04030` y `TC-04031`.
- [ ] Los criterios condicionados —`CV-09`, `CV-10`, `CV-23`— **se midieron y se registraron**, aunque no bloqueen. **Se valida** con la presencia de la medición en el informe de cierre. Registrar «sin medir» cuando la medición era posible **no cumple**.
- [ ] La batería completa —y no sólo lo que la etapa tocó— corre y pasa. **Se valida** con `CV-18`.
- [ ] Ningún `TC-XX` que estaba en verde pasó a rojo sin justificación escrita. **Se valida** con `CV-19`.
- [ ] [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) está actualizada: ninguna fila dice `Pendiente` para un elemento que la etapa cerró. **Se valida** comparando la matriz contra el índice de la épica.
- [ ] Todo defecto cerrado en la etapa generó al menos un `TC-XX`. **Se valida** con `CV-20`.
- [ ] El punto de control de la etapa tiene el **OK explícito del Product Owner**, con constancia escrita. **Se valida** con el informe de cierre de la etapa (intake §15, regla de delivery 2 y 3).

### 1.4 Entrega del proyecto de código

Se aplica cuando las **seis** etapas que este proyecto de código toca —`a`, `c`, `d`, `e`, `f` y `h`— están cerradas.

- [ ] Los **veintiocho** criterios de [`Criterios-Validacion.md`](Criterios-Validacion.md) están evaluados uno por uno, con su resultado registrado. **Se valida** con ese documento.
- [ ] **11 de 11** casos de uso, **16 de 16** reglas, **4 de 4** comprobaciones, **9 de 9** invariantes y **32 de 32** historias con caso de prueba en verde. **Se valida** con los recuentos de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md).
- [ ] **36 de 36** condiciones alcanzadas y **0** fuera del catálogo. **Se valida** con `TC-04028`.
- [ ] Los **ocho** escenarios del intake §20 siguen siendo el material de los casos de prueba que los usan, sin sustitución por datos inventados. **Se valida** con `CV-07` y con el recuento de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) §3.
- [ ] Los dos valores rotulados **[ASUNCIÓN]** están confirmados por el Product Owner, o su continuidad como asunción está declarada. **Se valida** leyendo el intake §22 y el estado de `BT-04018`.
- [ ] No queda ningún punto abierto de `05` §11 sin desenlace declarado, **incluido el nombre del cuarto puerto**. **Se valida** leyendo esa tabla.
- [ ] La versión de la biblioteca está calculada según la estrategia de versionado del intake §17.1.P.7 · GeometriaFactory-Application y la etiqueta de la etapa existe. **Se valida** con el registro de la etiqueta.

### 1.4 `GeometriaFactory-Infrastructure`

**Por qué la tercera capa se llama «etapa» y no «sprint».** Este producto no tiene sprints: la unidad de planificación es la **etapa**. La cuarta se llama «entrega del proyecto de código» y no «release» porque **este proyecto de código no se publica**: `redistribuible` es false y viaja embebido en el proceso de `GeometriaFactory-Api`.

Cada criterio responde a «¿cómo se valida?» con una operación concreta.

### 1.1 Historia de usuario

- [ ] Todos los criterios Given/When/Then de la historia están cubiertos por al menos un `TC-XX` de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md). **Se valida** leyendo la columna de tests de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2.
- [ ] Esos `TC-XX` están escritos y **en verde**. **Se valida** con la salida de `scripts/test.sh`.
- [ ] Toda regla de negocio, invariante y **regla conceptual de modelo** que la historia declara ejercer tiene su fila en la matriz §4 y §5 con este `TC-XX` entre sus tests. **Se valida** leyendo esas dos tablas.
- [ ] **Si la historia usa un texto de figuras, ese texto sale del intake §20 y no está escrito a mano.** **Se valida** por inspección del fixture, contra `CV-34`. Una historia con un texto propio **no está terminada**: es el modo en que el riesgo de negocio que la fuente pone primero se materializa sin que nadie lo note.
- [ ] Toda condición de rechazo que la historia produce está en el catálogo de las **17** y alcanzada por prueba. **Se valida** con `TC-06034`.
- [ ] **Si la historia introduce una propiedad de ausencia** —cero peticiones de red, cero retiros parciales, cero provisorias repetidas, cero secretos en mensajes—, se midió **con umbral cero y en la condición declarada**, y no se dio por cumplida por no haberse observado lo contrario. **Se valida** con el `TC-XX` correspondiente.
- [ ] **Si la historia toca el almacén, su prueba crea y descarta su propio almacén efímero.** **Se valida** por inspección, contra `CV-33`.
- [ ] La construcción termina en 0 y sin advertencias. **Se valida** con la salida de `scripts/build.sh`.
- [ ] La cobertura del componente que la historia toca no bajó respecto de la medición anterior, **y si el componente es uno de los dos motores, se reporta también en el informe acotado**. **Se valida** comparando los dos informes de cobertura.

### 1.2 Tarea técnica

- [ ] Los criterios de aceptación que la tarea declara en [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md) se cumplen uno por uno. **Se valida** por inspección, por prueba automatizada o por medición de la puerta que la tarea nombra.
- [ ] Si la tarea es de tipo indagación, la decisión que produjo está **registrada** en el documento que corresponde, y no sólo tomada. **Se valida** leyendo ese documento.
- [ ] Si la tarea cierra un punto abierto de `05` §11, ese punto queda declarado cerrado con su desenlace. **Se valida** leyendo esa tabla.
- [ ] Si la tarea es una puerta del pipeline —construcción, batería, cobertura, **verificación de transformaciones**— la puerta se midió al menos una vez y su resultado quedó registrado. **Se valida** con la salida del pipeline.
- [ ] La construcción y la batería pasan enteras, **incluida la etapa de verificación de transformaciones**. **Se valida** con `scripts/build.sh` y `scripts/test.sh`.

### 1.3 Etapa

- [ ] Todas las historias de la épica de la etapa cumplen §1.1, y todas sus tareas técnicas cumplen §1.2. **Se valida** recorriendo el índice de la épica en [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §3.
- [ ] Los **once** criterios de salida de [`Plan-Pruebas.md`](Plan-Pruebas.md) §3 se cumplen. **Se valida** con esa lista.
- [ ] **A partir de la etapa `f`: la batería del validador pasa entera, 10 de 10.** **Se valida** con la tabla de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §6, recorrida fila por fila. **Nueve casos no cumplen**, y el motivo está en [`Criterios-Validacion.md`](Criterios-Validacion.md) §6.
- [ ] Los criterios bloqueantes de [`Criterios-Validacion.md`](Criterios-Validacion.md) —`CV-13` a `CV-23`, `CV-31` a `CV-35`— se cumplen. **Se valida** con el informe del pipeline y con los casos de prueba nombrados.
- [ ] Los criterios condicionados —`CV-10`, `CV-11`, `CV-12`, `CV-29`— **se midieron y se registraron**, aunque no bloqueen. **Se valida** con la presencia de la medición en el informe de cierre. Registrar «sin medir» cuando la medición era posible **no cumple**.
- [ ] La batería completa —y no sólo lo que la etapa tocó— corre y pasa. **Se valida** con `CV-24`.
- [ ] Ningún `TC-XX` que estaba en verde pasó a rojo sin justificación escrita. **Se valida** con `CV-25`.
- [ ] [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) está actualizada: ninguna fila dice `Pendiente` para un elemento que la etapa cerró. **Se valida** comparando la matriz contra el índice de la épica.
- [ ] Todo defecto cerrado en la etapa generó al menos un `TC-XX`. **Se valida** con `CV-27`.
- [ ] El punto de control de la etapa tiene el **OK explícito del Product Owner**, con constancia escrita. **Se valida** con el informe de cierre (intake §15, reglas de delivery 2 y 3).

### 1.4 Entrega del proyecto de código

Se aplica cuando las **cinco** etapas que este proyecto de código toca —`a`, `c`, `d`, `e` y `f`— están cerradas.

- [ ] Los **treinta y cinco** criterios de [`Criterios-Validacion.md`](Criterios-Validacion.md) están evaluados uno por uno, con su resultado registrado. **Se valida** con ese documento.
- [ ] **10 de 10** casos de uso, **16 de 16** reglas de negocio con su tramo verificado, **7 de 7** reglas conceptuales de modelo y **25 de 25** historias con caso de prueba en verde. **Se valida** con los recuentos de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md).
- [ ] **10 de 10** casos de la batería del validador, con los **ocho** escenarios como entrada. **Se valida** con la matriz §6.
- [ ] **17 de 17** condiciones alcanzadas y **0** fuera del catálogo. **Se valida** con `TC-06034`.
- [ ] Los **ocho** escenarios del intake §20 siguen siendo el material de los casos que los usan, **como texto literal**. **Se valida** con `CV-07` y con el recuento de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) §3.
- [ ] Los **tres** valores rotulados **[ASUNCIÓN]** están confirmados por el Product Owner, o su continuidad como asunción está declarada. **Se valida** leyendo el intake §22 y `PA-11` de `05` §11.
- [ ] No queda ningún punto abierto de `05` §11 sin desenlace declarado. **Se valida** leyendo esa tabla, que ya tiene una fila **resuelta** con su fecha.
- [ ] La versión de la biblioteca está calculada según la estrategia de versionado del intake §17.1.P.7 · GeometriaFactory-Infrastructure, la etiqueta de la etapa existe y **ninguna transformación de esquema ya fusionada fue editada**. **Se valida** con el registro de la etiqueta y con el historial de las transformaciones.

## 2. Excepciones admitidas

### 2.1 `GeometriaFactory-Api`

| Caso | Qué se flexibiliza | Quién lo aprueba | Qué queda registrado |
| --- | --- | --- | --- |
| Criterio **condicionado** no alcanzado | Deja de bloquear el cierre, porque su umbral es un valor rotulado [ASUNCIÓN] sin confirmar | Nadie: es el tratamiento declarado en [`Criterios-Validacion.md`](Criterios-Validacion.md) §6 | La medición y su distancia al umbral |
| Mutation score **no exigible todavía** | `CV-00034` se reporta «sin medir» hasta que la herramienta corra. **La composición de raíz queda exenta** con su fundamento | — | El hueco de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §8 |
| Deuda técnica que una etapa no alcanza a cerrar | El criterio se difiere **una sola vez**, y sólo si no es de los bloqueantes de §1.3 | El Product Owner, en el punto de control | Una `BT-XX` nueva, con la etapa en que se cierra |
| **Puerta técnica que no pasa** | **No se admite excepción.** El intake §15 declara que detiene la planificación de las etapas que dependen de ella | El Product Owner decide la salida | La medición y la salida ejecutada |
| **Punto de acceso agregado sin declarar su ubicación respecto de la guardia** | **No se admite.** Es el primer riesgo de `05` §9 y **nada falla cuando ocurre** | — | — |
| **Familia empobrecida enriquecida** | **No se admite.** La respuesta más informativa es la tentadora, y **ninguna capa de adentro puede repararla** | — | — |
| **Cuerpo truncado en lugar de rechazado** | **No se admite.** Rompe `RN-00008` en silencio | — | — |
| **Eliminación fuera de alcance dada por verificada sin forzar la petición** | **No se admite.** Es el único criterio del producto que la fuente exige ejercer así | — | — |
| **Batería del validador cerrada con nueve casos** | **No se admite.** Tiene **diez**, y el décimo cubre `E-8`. El intake **1.20** lo dice así en §17.1.P.8 · GeometriaFactory-Api | — | — |

### 2.2 `GeometriaFactory-Domain`

| Caso | Qué se flexibiliza | Quién lo aprueba | Qué queda registrado |
| --- | --- | --- | --- |
| Criterio **condicionado** no alcanzado | Deja de bloquear el cierre de la etapa, porque su umbral es un valor rotulado [ASUNCIÓN] sin confirmar | Nadie: es el tratamiento declarado en [`Criterios-Validacion.md`](Criterios-Validacion.md) §6, no una excepción concedida | La medición y su distancia al umbral, en el informe de cierre |
| Mutation score **no exigible todavía** | El criterio `CV-02019` se reporta «sin medir» hasta que la herramienta esté elegida y corra | — | El hueco de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §7 |
| Deuda técnica que una etapa no alcanza a cerrar | El criterio se difiere **una sola vez**, y sólo si no es de los bloqueantes de §1.3 | El Product Owner, en el punto de control | Una `BT-XX` nueva en el backlog técnico, con la etapa en que se cierra |
| Caso de prueba deshabilitado | **No se admite sin motivo escrito en su fila** del catálogo. Un caso deshabilitado sin motivo incumple `CV-02021` | — | — |
| Historia que la etapa sólo ejerce parcialmente | **No se admite.** Es la misma regla que la DoR §3 declara para la entrada: una historia que no cabe entera en su etapa está mal cortada y se redivide | — | — |

### 2.3 `GeometriaFactory-Application`

| Caso | Qué se flexibiliza | Quién lo aprueba | Qué queda registrado |
| --- | --- | --- | --- |
| Criterio **condicionado** no alcanzado | Deja de bloquear el cierre de la etapa, porque su umbral es un valor rotulado [ASUNCIÓN] sin confirmar | Nadie: es el tratamiento declarado en [`Criterios-Validacion.md`](Criterios-Validacion.md) §6, no una excepción concedida | La medición y su distancia al umbral, en el informe de cierre |
| Mutation score **no exigible todavía** | El criterio `CV-04024` se reporta «sin medir» hasta que la herramienta esté elegida y corra | — | El hueco de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §8 |
| Deuda técnica que una etapa no alcanza a cerrar | El criterio se difiere **una sola vez**, y sólo si no es de los bloqueantes de §1.3 | El Product Owner, en el punto de control | Una `BT-XX` nueva en el backlog técnico, con la etapa en que se cierra |
| Caso de prueba deshabilitado | **No se admite sin motivo escrito en su fila** del catálogo. Un caso deshabilitado sin motivo incumple `CV-04026` | — | — |
| Prueba que abre el almacén real | **No se admite en ninguna forma.** El intake §17.1.P.8 · GeometriaFactory-Application declara la puerta propia y bloqueante, y la salida correcta es **reubicar la prueba** en la batería de integración de `GeometriaFactory-Api` porque ahí es donde pertenece, no para esquivar la puerta | — | — |
| Historia que la etapa sólo ejerce parcialmente | **No se admite.** Es la misma regla que la Definition of Ready declara para la entrada: una historia que no cabe entera en su etapa está mal cortada y se redivide | — | — |

### 2.4 `GeometriaFactory-Infrastructure`

| Caso | Qué se flexibiliza | Quién lo aprueba | Qué queda registrado |
| --- | --- | --- | --- |
| Criterio **condicionado** no alcanzado | Deja de bloquear el cierre, porque su umbral es un valor rotulado [ASUNCIÓN] sin confirmar | Nadie: es el tratamiento declarado en [`Criterios-Validacion.md`](Criterios-Validacion.md) §6 | La medición y su distancia al umbral, en el informe de cierre |
| Mutation score **no exigible todavía** | El criterio `CV-06030` se reporta «sin medir» hasta que la herramienta esté elegida y corra | — | El hueco de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §8 |
| **Adaptador de reloj sin mutation score** | Queda exento con su fundamento declarado: un umbral de mutación sobre una operación de una línea no aporta información | — | La fila correspondiente de [`Estrategia-Testing.md`](Estrategia-Testing.md) §2 |
| Deuda técnica que una etapa no alcanza a cerrar | El criterio se difiere **una sola vez**, y sólo si no es de los bloqueantes de §1.3 | El Product Owner, en el punto de control | Una `BT-XX` nueva, con la etapa en que se cierra |
| Caso de prueba deshabilitado | **No se admite sin motivo escrito en su fila** del catálogo | — | — |
| **Batería cerrada con nueve casos** | **No se admite.** La batería tiene **diez** y el décimo cubre `E-8`. El intake **1.20** lo dice así en §17.1.P.6 · GeometriaFactory-Infrastructure, §17.1.P.8 · GeometriaFactory-Infrastructure y §17.1.P.8 · GeometriaFactory-Api; la redacción de nueve fue de versiones anteriores al décimo caso y ya está corregida | — | — |
| **Texto de figuras escrito a mano** | **No se admite en ninguna forma.** Los ocho escenarios existen precisamente porque nadie los escribió pensando en el validador | — | — |
| **NFR de umbral cero dado por cumplido sin medición** | **No se admite.** No haber observado lo contrario no es una medición | — | — |

## 3. Vigencia

### 3.1 `GeometriaFactory-Api`

**Este documento es la fuente canónica de la Definition of Done de `GeometriaFactory-Api`.**

- [`../07-Plan-Sprint/Mini-Plan.md`](../07-Plan-Sprint/Mini-Plan.md) y cualquier plan de etapa **referencian** esta DoD y no la redefinen. Una lista de criterios de cierre escrita en un plan es un hallazgo, y el que rige es éste.
- [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/Definition-Of-Ready.md) §5 declara que la DoD vive en esta categoría y que hasta su emisión regían los criterios de transición del roadmap §5, que son de nivel producto. **Con esta emisión ese interinato termina**: los criterios del roadmap siguen valiendo a nivel producto y esta DoD los complementa a nivel de proyecto de código.
- **Esta DoD no declara terminado ningún despliegue.** El despliegue es manual y del Product Owner, y ningún criterio de este documento se cumple ejecutándolo.
- **El conjunto cerrado de códigos no se amplía desde este documento.** Es del ensamblado de contratos; esta DoD exige que se respete, no lo define.
- Todo cambio en los criterios de §1 se registra en §4 y se comunica en el punto de control de la etapa siguiente.
- La DoD **no habla de cuándo empezar**: eso es la Definition of Ready, y las dos no se solapan.

### 3.2 `GeometriaFactory-Domain`

**Este documento es la fuente canónica de la Definition of Done de `GeometriaFactory-Domain`.**

- [`../07-Plan-Sprint/Mini-Plan.md`](../07-Plan-Sprint/Mini-Plan.md) y cualquier plan de etapa **referencian** esta DoD y no la redefinen. Una lista de criterios de cierre escrita en un plan es un hallazgo, y el que rige es éste.
- [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/Definition-Of-Ready.md) §5 declara que la DoD vive en esta categoría y que hasta su emisión regían los criterios de transición del roadmap §5, que son de nivel producto. **Con esta emisión ese interinato termina**: los criterios de transición del roadmap siguen valiendo a nivel producto y esta DoD los complementa a nivel de proyecto de código, sin contradecirlos.
- Todo cambio en los criterios de §1 se registra en §4 y se comunica en el punto de control de la etapa siguiente.
- La DoD **no habla de cuándo empezar**: eso es la DoR, y las dos no se solapan.

### 3.3 `GeometriaFactory-Application`

**Este documento es la fuente canónica de la Definition of Done de `GeometriaFactory-Application`.**

- [`../07-Plan-Sprint/Mini-Plan.md`](../07-Plan-Sprint/Mini-Plan.md) y cualquier plan de etapa **referencian** esta DoD y no la redefinen. Una lista de criterios de cierre escrita en un plan es un hallazgo, y el que rige es éste.
- [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/Definition-Of-Ready.md) §5 declara que la DoD vive en esta categoría y que hasta su emisión regían los criterios de transición del roadmap §5, que son de nivel producto. **Con esta emisión ese interinato termina**: los criterios de transición del roadmap siguen valiendo a nivel producto y esta DoD los complementa a nivel de proyecto de código, sin contradecirlos.
- Todo cambio en los criterios de §1 se registra en §4 y se comunica en el punto de control de la etapa siguiente.
- La DoD **no habla de cuándo empezar**: eso es la Definition of Ready, y las dos no se solapan.

### 3.4 `GeometriaFactory-Infrastructure`

**Este documento es la fuente canónica de la Definition of Done de `GeometriaFactory-Infrastructure`.**

- [`../07-Plan-Sprint/Mini-Plan.md`](../07-Plan-Sprint/Mini-Plan.md) y cualquier plan de etapa **referencian** esta DoD y no la redefinen. Una lista de criterios de cierre escrita en un plan es un hallazgo, y el que rige es éste.
- [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/Definition-Of-Ready.md) §5 declara que la DoD vive en esta categoría y que hasta su emisión regían los criterios de transición del roadmap §5, que son de nivel producto. **Con esta emisión ese interinato termina**: los criterios del roadmap siguen valiendo a nivel producto y esta DoD los complementa a nivel de proyecto de código.
- **El recuento de la batería no se cambia desde este documento.** Esta DoD exige **diez**, siguiendo `05` §8 y §10.5; si el Product Owner corrigiera la redacción de sus gates en otro sentido, el cambio bajaría por el intake y no por acá.
- Todo cambio en los criterios de §1 se registra en §4 y se comunica en el punto de control de la etapa siguiente.
- La DoD **no habla de cuándo empezar**: eso es la Definition of Ready, y las dos no se solapan.

## 4. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 2.1 | 2026-08-29 | **Tramo `R-4` · renumerado de `QG` y `CV` al mapa de bloques del destino**, decidido por el Product Owner el 2026-08-29 al **retirar el `ADR-14005`** en lugar de aceptarlo. **6 línea(s)** pasan de `QG-NN` a `QG-<bloque>NNN`, con el bloque **deducido de la línea o de la sección y nunca inventado** — `00` Api, `02` Domain, `04` Application, `06` Infrastructure, `08` Contracts, `10` Web, `12` Visor. Con esto las dos familias **dejan de necesitar apartamiento**: cumplen [`../../../Producto/Norma-De-Nomenclatura.md`](../../../Producto/Norma-De-Nomenclatura.md) y `Root-Rules.md` §9.1 y §9.2. Las referencias cuyo bloque no estaba en el texto **conservan la forma vieja a propósito** y quedan inventariadas en [`../../../Audit/Inventario-Renumerado-R-4-2026-08-29.md`](../../../Audit/Inventario-Renumerado-R-4-2026-08-29.md). Se respeta §4.1: no se tocan las filas de control de cambios ni lo que está entre «…». |
| 2.0 | 2026-08-16 | **Consolidación de la fusión** (`Audit/Migracion-M10-Consolidacion-Fusion.md` 1.1 §4). Pasa de ser el documento del proyecto de código `GeometriaFactory-Api` a ser el de la **unidad de entrega**, absorbiendo los homónimos de `GeometriaFactory-Domain`, `-Application` e `-Infrastructure`. Cada sección lleva **una subsección por proyecto de código**, con su texto transpuesto **sin reescritura**. Entra **§0** con lo que sólo se ve con los cuatro juntos. Los tres documentos absorbidos quedan archivados en `_legacy/2026-08-16-consolidacion-m10/`. Sube **major**. |
