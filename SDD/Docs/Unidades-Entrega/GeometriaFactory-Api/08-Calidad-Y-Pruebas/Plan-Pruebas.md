# Plan de pruebas — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** Plan-Pruebas.md
**Versión:** 2.0
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

**Las seis secciones son comunes a las cuatro capas.** El plan de la unidad de entrega es la unión de
los cuatro, y su orden de ejecución lo fija el grafo de compilación: primero el dominio, después la
aplicación y la infraestructura, y al final la batería de integración que ejerce la superficie.

---

## 1. Alcance del plan

### 1.1 `GeometriaFactory-Api`

**Qué cubre.** Los **treinta y siete** casos de verificación de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md), repartidos entre las **seis** etapas del producto que este proyecto de código toca —`a`, `c`, `d`, `e`, `f` y `h`—, que son las que [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §2 declara como sus épicas.

**Y cubre algo más, que conviene declarar aparte porque no es sólo de este proyecto de código: la batería de integración del producto.** El intake §17.1.P.6 · GeometriaFactory-Api declara que `GeometriaFactory.Integration.Tests` golpea la superficie real por su protocolo contra el almacén real, y §17.1.P.6 · GeometriaFactory-Infrastructure le asigna **la persistencia real** de `GeometriaFactory-Infrastructure`. Esa batería vive acá y este plan la planifica.

**Qué no cubre, y dónde se cubre.** Las reglas del dominio y sus invariantes, en `GeometriaFactory-Domain`; la orquestación y las cuatro comprobaciones de autorización sobre el dato, en `GeometriaFactory-Application`; la interpretación del texto y los mecanismos de seguridad, en `GeometriaFactory-Infrastructure`; el recorrido de la persona, en `GeometriaFactory-Web`; el dibujo, en `GeometriaFactory-Visor`.

**Y una cosa que este plan explícitamente no planifica: el despliegue.** El intake §17.1.P.8 · GeometriaFactory-Api lo declara **manual, por el docente**, y que el agente **entrega el archivo de construcción y el de composición y no ejecuta el despliegue**. Lo que sí se verifica es que el artefacto se construya, arranque y responda.

**La unidad de planificación es la etapa y no el sprint.** El intake declara «sin plazo calendario; el avance se mide por etapas cerradas». Por eso §5 se titula «Plan por etapa» y **ninguna de sus filas lleva una fecha ni una duración**.

**Las etapas `b` y `g` no aparecen en el plan**, y es declaración y no olvido: `../06-Backlog-Tecnico/Product-Backlog.md` §2 lo fundamenta. La `b` no agrega ningún punto de acceso, y **todo lo que la `g` necesita de esta superficie ya está expuesto en la `e`**.

### 1.2 `GeometriaFactory-Domain`

**Qué cubre.** Los **veintisiete** casos de prueba de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md), repartidos entre las **seis** etapas del producto que este proyecto de código toca —`a`, `c`, `d`, `e`, `f` y `h`—, que son las que [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/_fusion/Domain/Product-Backlog.md) §2 declara como sus épicas.

**Qué no cubre, y dónde se cubre.** La interpretación del texto del alumno y la tolerancia de formato, en `GeometriaFactory-Infrastructure`; el transporte de los datos por la frontera de servicio, en `GeometriaFactory-Contracts`; el dibujo, en `GeometriaFactory-Visor`; los recorridos de punta a punta del producto, en `GeometriaFactory-Api` y `GeometriaFactory-Web`.

**La unidad de planificación es la etapa y no el sprint.** El intake declara «sin plazo calendario; el avance se mide por etapas cerradas», y el producto no tiene sprints ([`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/_fusion/Domain/Product-Backlog.md) §4.1). Por eso §5 se titula «Plan por etapa» y **ninguna de sus filas lleva una fecha ni una duración**: sería un plazo que ninguna fuente da.

**Las etapas `b` y `g` no aparecen en el plan**, y es declaración y no olvido: no producen épica en este proyecto de código, porque no tocan entidades, invariantes ni transiciones.

### 1.3 `GeometriaFactory-Application`

**Qué cubre.** Los **treinta y un** casos de prueba de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md), repartidos entre las **seis** etapas del producto que este proyecto de código toca —`a`, `c`, `d`, `e`, `f` y `h`—, que son las que [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/_fusion/Application/Product-Backlog.md) §2 declara como sus épicas.

**Qué no cubre, y dónde se cubre.** Las entidades, los invariantes y las máquinas de estado, en `GeometriaFactory-Domain`; la interpretación efectiva del texto, la derivación de la contraseña, la producción de la provisoria y el guardado, en `GeometriaFactory-Infrastructure`; el transporte de los datos por la frontera de proceso, en `GeometriaFactory-Contracts`; **la batería de integración contra el almacén real y la API real**, en `GeometriaFactory-Api` (intake §17.1.P.6 · GeometriaFactory-Application); las superficies y el dibujo, en `GeometriaFactory-Web` y `GeometriaFactory-Visor`.

**La unidad de planificación es la etapa y no el sprint.** El intake declara «sin plazo calendario; el avance se mide por etapas cerradas», y el producto no tiene sprints. Por eso §5 se titula «Plan por etapa» y **ninguna de sus filas lleva una fecha ni una duración**: sería un plazo que ninguna fuente da.

**Las etapas `b` y `g` no aparecen en el plan**, y es declaración y no olvido: [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/_fusion/Application/Product-Backlog.md) §2 declara que no producen épica en este proyecto de código, porque ninguna de las dos orquesta un caso de uso ni ejerce una comprobación de autorización.

### 1.4 `GeometriaFactory-Infrastructure`

**Qué cubre.** Los **treinta y cinco** casos de prueba de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md), repartidos entre las **cinco** etapas del producto que este proyecto de código toca —`a`, `c`, `d`, `e` y `f`—, que son las que [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/_fusion/Infrastructure/Product-Backlog.md) §2 declara como sus épicas.

**Qué no cubre, y dónde se cubre.** Las entidades, los invariantes y las máquinas de estado, en `GeometriaFactory-Domain`; la orquestación, la autorización y el alcance transaccional declarado, en `GeometriaFactory-Application`; **la persistencia real ejercida por la superficie del producto**, en `GeometriaFactory-Api`, que es donde el intake §17.1.P.6 · GeometriaFactory-Infrastructure ubica la batería de integración; las superficies y el dibujo, en `GeometriaFactory-Web` y `GeometriaFactory-Visor`.

**La unidad de planificación es la etapa y no el sprint.** El intake declara «sin plazo calendario; el avance se mide por etapas cerradas». Por eso §5 se titula «Plan por etapa» y **ninguna de sus filas lleva una fecha ni una duración**.

**Las etapas `b`, `g` y `h` no aparecen en el plan**, y es declaración y no olvido. `../06-Backlog-Tecnico/Product-Backlog.md` §2 lo fundamenta: la `b` y la `g` no tocan el almacén, los motores ni los mecanismos, y lo que esta capa aporta a la `h` —guardar el estado terminal y el comentario del administrador— **ya está construido en la etapa `e`**, porque el comentario es **campo y no entidad**.

## 2. Criterios de entrada

### 2.1 `GeometriaFactory-Api`

Lo que tiene que estar listo para que este plan se ejecute en una etapa:

- [ ] La rama de la etapa está abierta y la sesión de refinamiento se hizo.
- [ ] Las historias de la etapa cumplen los criterios de [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/Definition-Of-Ready.md) §1, incluida la declaración del punto de acceso que ejercen y la de que **ninguna acuña, renombra ni traduce a texto un código del contrato**.
- [ ] **Las capas de adentro que la etapa consume ya emitieron lo que esta superficie expone.** Es el proyecto de código de nivel 3: un caso de uso que no exista en `GeometriaFactory-Application` no se puede exponer acá.
- [ ] **`PT-04` está medida en la etapa `a`** y su resultado registrado.
- [ ] **Todo punto de acceso nuevo de la etapa está declarado como dentro de la guardia, o como una de las cuatro exenciones con su motivo.** Sin esa declaración `TC-00007` no puede correr.
- [ ] Los **ocho** textos literales de los escenarios del intake §20 están cargados, **sin ninguna modificación**.
- [ ] El contenedor de desarrollo levanta, `scripts/test.sh` corre de punta a punta y la batería de integración levanta el proceso contra un almacén efímero.

### 2.2 `GeometriaFactory-Domain`

Lo que tiene que estar listo para que este plan se ejecute en una etapa:

- [ ] La rama de la etapa está abierta y la sesión de refinamiento se hizo ([`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/_fusion/Domain/Product-Backlog.md) §5).
- [ ] Las historias de la etapa cumplen los **seis** criterios de [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/_fusion/Domain/Definition-Of-Ready.md) §1, incluido el de tener criterios de aceptación en Given/When/Then.
- [ ] Toda condición de rechazo que las historias de la etapa producen **ya existe** en el catálogo de [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md), o su alta está comprometida bajo la excepción de la DoR §3.
- [ ] `BT-02001` está cerrada: el proyecto de código y su proyecto de pruebas existen y la batería corre, aunque sea vacía.
- [ ] `BT-02002` está cerrada: los nombres de tipos y de espacios de nombres están fijados y validados en el punto de control de la etapa `a`. Sin esto ningún caso de prueba se puede escribir sin retrabajo.
- [ ] El contenedor de desarrollo levanta y `scripts/test.sh` corre de punta a punta.

### 2.3 `GeometriaFactory-Application`

Lo que tiene que estar listo para que este plan se ejecute en una etapa:

- [ ] La rama de la etapa está abierta y la sesión de refinamiento se hizo ([`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/_fusion/Application/Product-Backlog.md) §5).
- [ ] Las historias de la etapa cumplen los **siete** criterios de [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/_fusion/Application/Definition-Of-Ready.md) §1, incluidos el 5 —cuál de las cuatro comprobaciones la alcanza— y el 6 —toda condición existe en el catálogo de las 36—.
- [ ] `BT-04001` está cerrada: el proyecto de código y su proyecto de pruebas existen, la batería corre aunque sea vacía, y el archivo de proyecto declara **1** dependencia saliente.
- [ ] `BT-04002` está cerrada: los nombres de tipos, de espacios de nombres y **el del cuarto puerto** están fijados y validados en el punto de control de la etapa `a`. Sin esto los dobles de puerto se escriben contra un nombre que va a cambiar.
- [ ] **Las guardas de `GeometriaFactory-Domain` que la etapa invoca ya existen.** [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/_fusion/Application/Product-Backlog.md) §1.1 lo declara: dentro de cada etapa, el trabajo del nivel 0 va primero.
- [ ] El contenedor de desarrollo levanta y `scripts/test.sh` corre de punta a punta.

### 2.4 `GeometriaFactory-Infrastructure`

Lo que tiene que estar listo para que este plan se ejecute en una etapa:

- [ ] La rama de la etapa está abierta y la sesión de refinamiento se hizo.
- [ ] Las historias de la etapa cumplen los criterios de [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/_fusion/Infrastructure/Definition-Of-Ready.md) §1, incluida la cita por identificador de las reglas y los invariantes que viven en `GeometriaFactory-Domain`.
- [ ] **Los puertos que esta etapa implementa ya están declarados en `GeometriaFactory-Application`.** Es un proyecto de código de nivel 2: un puerto que no exista arriba no se puede implementar acá.
- [ ] Los nombres de tipos y de espacios de nombres están fijados en el punto de control de la etapa `a` (`05` §11 `PA-02`).
- [ ] **A partir de la etapa `c`: la función de derivación de clave está anclada** (`05` §11 `PA-03`). Sin eso, los valores esperados de `TC-06025` y `TC-06026` no se pueden escribir sin retrabajo.
- [ ] Los **ocho** textos literales de los escenarios del intake §20 están cargados como fixture, **sin ninguna modificación**.
- [ ] El contenedor de desarrollo levanta y `scripts/test.sh` corre de punta a punta, con su etapa de **verificación de transformaciones**.

## 3. Criterios de salida

### 3.1 `GeometriaFactory-Api`

Lo que tiene que cumplirse para declarar el plan ejecutado con éxito en una etapa:

- [ ] Todos los `TC-XX` en alcance de la etapa están ejecutados y pasan.
- [ ] **`TC-00007` cierra con 4 y 11 sobre los quince puntos, en las dos direcciones**, y ningún punto nuevo de la etapa quedó fuera de la guardia sin exención declarada.
- [ ] **`TC-00025` da 3 de 3 comparaciones idénticas**, y ninguna familia empobrecida se enriqueció al agregar un punto.
- [ ] `TC-00024` y `TC-00027` cierran en las dos direcciones sobre los códigos que la etapa incorporó, con **0** inventados y **0** renombrados.
- [ ] **`TC-00026` da 0 exposiciones** sobre las respuestas de fallo de los puntos que la etapa toca, y el registro del servidor los tiene todos.
- [ ] **La batería del validador que corre desde acá pasa entera: 10 de 10**, a partir de la etapa `f`.
- [ ] Todos los NFR con umbral **cero** que la etapa toca se midieron **en la condición declarada**, y no se dieron por cumplidos por no haberse observado lo contrario.
- [ ] Los gates `QG-01`, `QG-02`, `QG-05`, `QG-06`, `QG-07`, `QG-08`, `QG-09`, `QG-10`, `QG-11`, `QG-12` y `QG-15` de [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3 pasan.
- [ ] Los gates condicionados —`QG-03`, `QG-04`, `QG-13`, `QG-14`— **se midieron y se registraron**.
- [ ] La matriz de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) está actualizada, **incluida su tabla de quince puntos**.
- [ ] Todo defecto cerrado durante la etapa generó al menos un `TC-XX` nuevo o extendió uno existente.
- [ ] El punto de control de la etapa tiene el OK explícito del Product Owner (intake §15, regla de delivery 2).

### 3.2 `GeometriaFactory-Domain`

Lo que tiene que cumplirse para declarar el plan ejecutado con éxito en una etapa:

- [ ] Todos los `TC-XX` en alcance de la etapa están escritos, ejecutados y en verde.
- [ ] **Ningún `TC-XX` que estaba en verde en la etapa anterior pasó a rojo** sin justificación escrita en el informe de cierre.
- [ ] La cobertura por componente alcanza los umbrales de [`Estrategia-Testing.md`](Estrategia-Testing.md) §2 en los componentes que la etapa toca. Gate condicionado mientras el valor siga rotulado [ASUNCIÓN].
- [ ] `TC-02023` cierra en las dos direcciones sobre las condiciones que la etapa incorporó.
- [ ] `TC-02026` cierra sobre los invariantes que la etapa toca: **cada uno con prueba de violación rechazada y sin dobles**.
- [ ] Los gates `QG-01`, `QG-02`, `QG-04`, `QG-05`, `QG-06` y `QG-08` de [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3 pasan.
- [ ] La matriz de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) está actualizada: ninguna fila dice `Pendiente` para un elemento que la etapa cerró.
- [ ] Todo defecto cerrado durante la etapa generó al menos un `TC-XX` nuevo o extendió uno existente.
- [ ] El punto de control de la etapa tiene el OK explícito del Product Owner (intake §15).

### 3.3 `GeometriaFactory-Application`

Lo que tiene que cumplirse para declarar el plan ejecutado con éxito en una etapa:

- [ ] Todos los `TC-XX` en alcance de la etapa están escritos, ejecutados y en verde.
- [ ] **Ningún `TC-XX` que estaba en verde en la etapa anterior pasó a rojo** sin justificación escrita en el informe de cierre.
- [ ] La cobertura por componente alcanza los umbrales de [`Estrategia-Testing.md`](Estrategia-Testing.md) §2 en los componentes que la etapa toca. Gate condicionado mientras el valor siga rotulado [ASUNCIÓN].
- [ ] `TC-04026` da **0** en sus tres recuentos: ninguna prueba de la etapa abrió el almacén real.
- [ ] `TC-04027` sigue dando **1** dependencia saliente y **0** de las prohibidas.
- [ ] `TC-04028` cierra en las dos direcciones sobre las condiciones que la etapa incorporó.
- [ ] `TC-04011` y la tabla de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §5 cierran sobre las comprobaciones que la etapa toca: **cada una con prueba de su negativa sin base de datos**.
- [ ] Los gates `QG-01`, `QG-02`, `QG-04`, `QG-05`, `QG-06`, `QG-07`, `QG-08`, `QG-09` y `QG-11` de [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3 pasan.
- [ ] La matriz de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) está actualizada: ninguna fila dice `Pendiente` para un elemento que la etapa cerró.
- [ ] Todo defecto cerrado durante la etapa generó al menos un `TC-XX` nuevo o extendió uno existente.
- [ ] El punto de control de la etapa tiene el OK explícito del Product Owner (intake §15, regla de delivery 2).

### 3.4 `GeometriaFactory-Infrastructure`

Lo que tiene que cumplirse para declarar el plan ejecutado con éxito en una etapa:

- [ ] Todos los `TC-XX` en alcance de la etapa están escritos, ejecutados y en verde.
- [ ] **Ningún `TC-XX` que estaba en verde en la etapa anterior pasó a rojo** sin justificación escrita en el informe de cierre.
- [ ] La cobertura por componente alcanza los umbrales de [`Estrategia-Testing.md`](Estrategia-Testing.md) §2 en los componentes que la etapa toca, **con el informe acotado a los dos motores reportado por separado**. Gates condicionados mientras los valores sigan rotulados [ASUNCIÓN].
- [ ] **A partir de la etapa `f`: la batería del validador pasa entera, 10 de 10**, contra la tabla de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §6.
- [ ] `TC-06009` da **exactamente 2** advertencias sobre `E-1`, y no 3.
- [ ] `TC-06034` cierra en las dos direcciones sobre las condiciones que la etapa incorporó, y `TC-06035` da **0** en sus dos recuentos —mensajes y registro del servidor—.
- [ ] Todos los NFR con umbral **cero** que la etapa toca se midieron **en la condición declarada**, y no se dieron por cumplidos por no haberse observado lo contrario.
- [ ] Los gates `QG-01`, `QG-02`, `QG-03`, `QG-04`, `QG-07`, `QG-08`, `QG-09`, `QG-10`, `QG-11`, `QG-12` y `QG-13` de [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3 pasan.
- [ ] La matriz de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) está actualizada: ninguna fila dice `Pendiente` para un elemento que la etapa cerró.
- [ ] Todo defecto cerrado durante la etapa generó al menos un `TC-XX` nuevo o extendió uno existente.
- [ ] El punto de control de la etapa tiene el OK explícito del Product Owner (intake §15, regla de delivery 2).

## 4. Riesgos de calidad

### 4.1 `GeometriaFactory-Api`

Alineados con los **nueve** riesgos arquitectónicos de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §9, más dos propios de esta categoría.

| Id | Riesgo | Impacto | Probabilidad | Mitigación en este plan |
| --- | --- | --- | --- | --- |
| RQ-01 | Que un punto de acceso nuevo quede **fuera de la guardia** del cambio de contraseña pendiente | **Muy alto**: `RN-00013` e `INV-09` dejan de valer **y nada falla** | **Alta**: es un defecto de **omisión**, y los defectos de omisión **no se ven leyendo el punto nuevo** | `TC-00007` en **cada** etapa, recorriendo los quince en las dos direcciones; y el criterio de entrada de §2 que exige declarar la ubicación de todo punto nuevo **antes** de construirlo |
| RQ-02 | Que el trabajo ajeno responda «no autorizado» en lugar de «no encontrado» | **Muy alto**: permite averiguar por tanteo qué identificadores existen, y **ninguna capa de adentro puede repararlo** | Media: es la traducción que parece más informativa y por eso es la tentadora | `TC-00025` con sus **3 de 3** comparaciones, ejecutado en cada etapa que agrega una respuesta de fallo |
| RQ-03 | Que el límite de tamaño del cuerpo **trunque** el texto de un alumno en lugar de rechazarlo | Alto: **rompe `RN-00008` en silencio** y el alumno lo descubre al ver el dibujo | Media: truncar es el comportamiento por defecto de varias capas de transporte | `TC-00019`, con comparación byte a byte y con el caso del cuerpo por encima del límite **rechazado y no truncado** |
| RQ-04 | Que los dos extremos serialicen distinto y el contrato deje de ser el mismo | Alto: el fallo aparece en tiempo de ejecución y **no lo detecta la compilación**, que es la única red del producto | Media, y es un trade-off aceptado por escrito aguas arriba | `TC-00029`, con **1** sola configuración declarada; y la batería de integración golpeando el servicio real |
| RQ-05 | Que un envío cuyo texto no verifica responda con un **código de fallo** | Medio: le diría a la persona que su petición estaba mal cuando lo que pasa es que su programa emitió algo que no se puede interpretar | Media: es la lectura intuitiva de «no verificó» | `TC-00017`, con los escenarios `E-1`, `E-5` y `E-8`: **las tres respuestas son exitosas** |
| RQ-06 | Que se agregue un punto pensado para el navegador, o se configure el intercambio de origen cruzado | **Muy alto**: rompe `RA-01`, que es regla de nivel producto | Baja, pero el costo de equivocarse es de **rediseño** | `TC-00036`, con sus tres ausencias verificadas, en cada etapa que agrega superficie |
| RQ-07 | Que la composición de raíz deje un puerto sin adaptador y el fallo aparezca en la primera petición | Medio: el servicio arranca y **falla al primer uso, en producción y sin nadie mirando** | Media | `TC-00028`, con **fallo en construcción** y no en la primera petición |
| RQ-08 | Que el listado de la comisión crezca por encima de lo que el requerimiento de tiempo sostiene | Medio | Baja en el alcance declarado | `TC-00034`, con la **condición de reingreso escrita**: cuando el percentil deje de cumplirse, entra paginación, y es cambio del ensamblado de contratos |
| RQ-09 | Que el mecanismo de construcción de la imagen en destino no funcione y el despliegue quede sin camino | Alto: es el único canal de entrega declarado | Media, **y la fuente lo rotula [A VERIFICAR]** | Probarlo **una vez antes de depender de él**. **No es criterio de esta categoría**: el despliegue es manual y del Product Owner |
| RQ-10 | **Que la batería de integración se dé por suficiente sin las inspecciones de umbral exacto** | Alto: las propiedades más peligrosas de este proyecto de código —los cuatro puntos exentos, los dieciséis códigos con destino, las tres familias— **no se ven ejerciendo el cable, se ven contándolo** | Media, porque una batería de integración verde da sensación de cobertura | Criterio de salida de §3: las **cinco** inspecciones con umbral exacto se ejecutan aparte y su resultado se registra por separado |
| RQ-11 | **Que la batería del validador se dé por completa con nueve casos**, arrastrando la redacción que el gate del intake tuvo hasta 1.19 | Alto: dejaría `E-8` sin cubrir | **Baja desde el intake 1.20**, que corrigió el gate a **diez**; queda como riesgo vivo sólo por las copias del texto viejo que puedan circular | [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3.2 declara el desenlace y fija **diez**; el criterio de salida de §3 lo exige |

### 4.2 `GeometriaFactory-Domain`

Alineados con los **cinco** riesgos arquitectónicos de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §9, más dos propios de esta categoría.

| Id | Riesgo | Impacto | Probabilidad | Mitigación en este plan |
| --- | --- | --- | --- | --- |
| RQ-01 | Que una dependencia se cuele en el nivel 0 y el dominio deje de ser probable sin infraestructura | Alto | Media | `TC-02024` corre en cada etapa, no sólo en la `a`; `QG-04` bloquea la fusión |
| RQ-02 | Que un invariante se ejerza en un componente y no en otro, y quede una puerta por la que se lo saltea | Alto | Media, **con precedente registrado**: la familia se abrió dos veces | `TC-02005` como prueba de regresión de esa familia; `TC-02026` sobre los nueve invariantes; umbral de cobertura de 100 % de ramas en el evaluador de admisibilidad |
| RQ-03 | Que el consumidor trate el resultado tipado como excepción y descarte los rechazos | Medio | Media | `TC-02027` verifica que ninguna condición prevista lance; el efecto sobre el consumidor se verifica en `GeometriaFactory-Application` |
| RQ-04 | Que alguna operación lea el reloj por comodidad y rompa la reproducibilidad | Medio | Baja | `TC-02025`, con la comparación de dos ejecuciones consecutivas sin fijar el reloj |
| RQ-05 | Que los nombres abiertos se fijen sin punto de control y haya que renombrar | Bajo, de retrabajo | Media | Criterio de entrada de §2: `BT-02002` cerrada antes de escribir casos de prueba |
| RQ-06 | **Que un escenario del intake §20 se sustituya por un dato sintético** «porque es más cómodo de escribir» | Alto | Media | [`Estrategia-Testing.md`](Estrategia-Testing.md) §6 lo prohíbe; el criterio de salida exige que los ocho escenarios sigan siendo el material de `TC-02013` a `TC-02018` |
| RQ-07 | **Que la matriz de cobertura quede desactualizada** y siga diciendo `Pendiente` con pruebas ya escritas | Medio | Alta, es el anti-patrón más común de la categoría | Criterio de salida de §3: la matriz se actualiza al cerrar cada etapa, y su desactualización bloquea el cierre |

### 4.3 `GeometriaFactory-Application`

Alineados con los **seis** riesgos arquitectónicos de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §9, más dos propios de esta categoría.

| Id | Riesgo | Impacto | Probabilidad | Mitigación en este plan |
| --- | --- | --- | --- | --- |
| RQ-01 | Que un caso de uso consulte el almacén por su cuenta y deje de ser probable con dobles | Alto | Media | `TC-04026` y `TC-04027` corren en **cada** etapa, no sólo en la `a`; `QG-04` y `QG-05` bloquean la fusión |
| RQ-02 | Que aparezca un camino que ejerza una capacidad **sin** resolver antes la marca de cambio de contraseña pendiente | **Muy alto** | Media | `TC-04011` como prueba de orden en cada etapa a partir de la `d`; umbral de **100 %** de líneas y ramas en la guarda de autorización; criterio 5 de la Definition of Ready |
| RQ-03 | Que la negativa por pertenencia y la negativa por facultad se confundan, y un trabajo ajeno responda «no autorizado» | Alto | Media | `TC-04012`, que verifica la indistinguibilidad **en los dos sentidos** contra la tabla de traducciones prohibidas de `03` §2.4 |
| RQ-04 | Que un caso de uso reparta su efecto entre dos unidades de trabajo y la baja deje trabajos huérfanos | Alto | Baja | `TC-04029`, con la baja de cuenta como caso testigo y el recuento de aperturas instrumentado |
| RQ-05 | Que el consumidor trate el resultado tipado como excepción y descarte los rechazos | Medio | Media | `TC-04031` verifica que ninguna de las 36 condiciones lance; el efecto sobre el consumidor se verifica en `GeometriaFactory-Api` |
| RQ-06 | Que el nombre del cuarto puerto se fije sin punto de control y haya que renombrar los dobles | Bajo, de retrabajo | Alta | Criterio de entrada de §2: `BT-04002` cerrada **antes** de escribir los casos de prueba que usan ese doble |
| RQ-07 | **Que un escenario del intake §20 se sustituya por un resultado de interpretación inventado** «porque es más cómodo de armar» | Alto | Media | [`Estrategia-Testing.md`](Estrategia-Testing.md) §6 lo prohíbe; el criterio de salida exige que los ocho escenarios sigan siendo el material de `TC-04015` a `TC-04017` y de `TC-04022` |
| RQ-08 | **Que la matriz de cobertura quede desactualizada** y siga diciendo `Pendiente` con pruebas ya escritas | Medio | Alta, es el anti-patrón más común de la categoría | Criterio de salida de §3: la matriz se actualiza al cerrar cada etapa, y su desactualización bloquea el cierre |

### 4.4 `GeometriaFactory-Infrastructure`

Alineados con los **ocho** riesgos arquitectónicos de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §9, más dos propios de esta categoría.

| Id | Riesgo | Impacto | Probabilidad | Mitigación en este plan |
| --- | --- | --- | --- | --- |
| RQ-01 | Que el validador se escriba **sin leer el análisis** y no sirva para el dato que existe | **Muy alto**: deja el producto inútil para el dato real | **Alta si no se controla**, y así lo declara la fuente | Los **diez** casos de la batería con los **ocho** escenarios como texto literal (`TC-06001` a `TC-06010`), la cobertura de **95 %** de los dos motores, y la prohibición explícita de escribir a mano un texto de figuras |
| RQ-02 | Que un texto **ilegible** devuelva «motor no disponible» en lugar de una observación | Alto: el alumno esperaría a que se recupere de un problema que no tiene | **Alta**: es la garantía que más veces se rompe al implementar | `TC-06013`, con sus **tres** resultados distintos verificados en la misma prueba |
| RQ-03 | Que la provisoria se componga por un medio distinto de la fuente de material impredecible cuando ésta no responde | **Muy alto**: **un reseteo que no se completa es recuperable; una provisoria adivinable no se nota hasta que alguien la usa** | Media | `TC-06028`, con la fuente doblada como no disponible, y `TC-06027` con **0** provisorias iguales y **0** derivables de un dato conocido |
| RQ-04 | Que ante la ausencia de clave de firma se genere una al vuelo o se emita sin firmar | **Muy alto**: el sistema arranca, emite accesos y **nadie lo nota hasta que alguien falsifica uno** | Media | `TC-06030`, con **0** accesos emitidos por cualquiera de los dos atajos |
| RQ-05 | Que la preparación del almacén, ante un esquema que no corresponde, **descarte el almacén y lo cree de nuevo** | **Muy alto**: deja el servicio impecable y **sin los trabajos de nadie** | Baja, pero es el atajo más destructivo del producto | `TC-06033`, con el arranque detenido y la verificación de que el almacén **no se descarta** |
| RQ-06 | Que la ubicación del almacén **caiga hacia una ruta alternativa** dentro de la imagen cuando el volumen no está montado | Alto: el servicio acepta trabajos de la comisión entera y **los pierde en el siguiente reemplazo de versión** | Media, porque es el comportamiento por defecto de casi cualquier biblioteca de acceso a archivos | `TC-06033`, segunda mitad: la ubicación no disponible **detiene el arranque** |
| RQ-07 | Que una consulta de listado arrastre los componentes de cada pieza o el texto original | Medio | **Media-alta**: es el comportamiento por defecto de cualquier carga completa de entidad | `TC-06019`, con dos recuentos en cero para el listado y presencia completa en el detalle |
| RQ-08 | Que la unicidad del correo se sostenga **sólo** con la consulta previa del consumidor | Alto | Media | `TC-06022`, que verifica el rechazo del almacén **aunque la consulta previa no lo hubiera visto** |
| RQ-09 | **Que un escenario del intake §20 se sustituya por un texto escrito a mano** «porque es más corto» | **Muy alto**: un texto escrito por quien conoce las cuatro trampas **las pasa sin ejercitarlas**, que es exactamente el modo en que `RQ-01` se materializa sin que nadie lo note | Media | [`Estrategia-Testing.md`](Estrategia-Testing.md) §6 lo prohíbe; el criterio de salida exige que los ocho escenarios sigan siendo el material de `TC-06001` a `TC-06011` y de `TC-06016` |
| RQ-10 | **Que la batería se dé por completa con nueve casos**, arrastrando la redacción que los dos gates del intake tuvieron hasta 1.19 en lugar de la tabla de §21 | Alto: dejaría `E-8` sin cubrir, que es el escenario que cerró la única condición del contrato de fachada sin dato de prueba | **Baja desde el intake 1.20**, que corrigió los dos gates a **diez**; queda como riesgo vivo sólo por las copias del texto viejo que puedan circular | [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3.2 declara el desenlace y fija **diez**; el criterio de salida de §3 exige 10 de 10 contra la tabla de la matriz §6 |

## 5. Plan por etapa

### 5.1 `GeometriaFactory-Api`

Sin fechas y sin duraciones, por lo declarado en §1.

| Etapa | Épica | Alcance de testing | Casos de verificación en alcance | Entregable de esta categoría |
| --- | --- | --- | --- | --- |
| `a` | EP-00001 Esqueleto ambulante y verificación de viabilidad | La composición de raíz, el arranque en dos fases y el punto de salud; y la ausencia de canal de sesión interactiva | `TC-00028`, `TC-00029`, `TC-00030`, `TC-00031`, `TC-00032`, `TC-00033`, `TC-00036` | `PT-04` medida; **4 de 4** puertos con fallo en construcción; **0** peticiones atendidas con la preparación incompleta; las tres ausencias de `RA-01` verificadas |
| `c` | EP-00002 Identidad del administrador y sesión | El canje, la guardia sobre los once puntos, los cuatro puntos de acceso y credencial propia, y **las dos traducciones con su tabla única** | `TC-00001`, `TC-00002`, `TC-00003`, `TC-00004`, `TC-00005`, `TC-00007`, `TC-00008`, `TC-00009`, `TC-00010`, `TC-00024`, `TC-00025`, `TC-00026`, `TC-00027` | La tabla de traducción cerrada en las dos direcciones; **3 de 3** familias indistinguibles; **4** puntos fuera de la guardia |
| `d` | EP-00003 Ciclo de vida de la cuenta de alumno | El gobierno de la comisión, el reseteo y **la guardia del cambio pendiente sobre todos los puntos salvo uno** | `TC-00006`, `TC-00011`, `TC-00012`, `TC-00013`, `TC-00014`, `TC-00015`, `TC-00016` | `INV-09` sostenido desde el borde: **diez rechazos y una excepción**; la provisoria devuelta una vez y **0** apariciones en trazas |
| `e` | EP-00004 Gestión del trabajo | Los cinco puntos sobre trabajos, con el texto sin normalizar y **la eliminación forzando la petición** | `TC-00018`, `TC-00019`, `TC-00020`, `TC-00021`, `TC-00022` | **0** eliminaciones fuera de alcance al forzar; **0** caracteres de diferencia y **0** truncamientos; la ausencia verificada del parámetro de borradores ajenos |
| `f` | EP-00005 Interpretación y verificación del dato del alumno | El envío y el reenvío, que **responden con éxito** transportando el estado que la interpretación decidió | `TC-00017`, `TC-00034`, `TC-00037` | Los escenarios `E-1`, `E-5` y `E-8` con respuesta exitosa; **la batería del validador 10 de 10** desde acá; percentil y caudal medidos |
| `h` | EP-00006 Desenlace de la entrega | El punto de desenlace con su terminalidad, y **la colección de peticiones reproducible** | `TC-00023`, `TC-00035`, y reejecución de `TC-00007`, `TC-00025` y `TC-00026` | Matriz completa: 12 de 12 casos de uso, **15 de 15** puntos, 16 de 16 reglas y 9 de 9 invariantes; la colección en **5 pasos o menos** con **0** datos inventados |

**La suma cubre los treinta y siete casos de verificación.** `TC-00007`, `TC-00025` y `TC-00026` se reejecutan en la etapa `h` porque son los tres cuyo resultado **cambia cada vez que se agrega un punto o una respuesta de fallo**, y la `h` agrega las dos cosas.

### 5.2 `GeometriaFactory-Domain`

Sin fechas y sin duraciones, por lo declarado en §1.

| Etapa | Épica | Alcance de testing | Casos de prueba en alcance | Entregable de esta categoría |
| --- | --- | --- | --- | --- |
| `a` | EP-02001 Esqueleto ambulante | Ninguna capacidad funcional. Se ponen en pie las pruebas de inspección estructural y la batería vacía | `TC-02024` | Batería que corre; `QG-01`, `QG-02` y `QG-04` medidos por primera vez |
| `c` | EP-02002 Identidad del administrador y sesión | Configuración del administrador, admisibilidad y reemplazo de credencial | `TC-02006`, `TC-02008`, `TC-02010`, `TC-02027` | Matriz con `CU-02003`, `CU-02004` y `CU-02012` cerrados |
| `d` | EP-02003 Ciclo de vida de la cuenta de alumno | Alta, ciclo de vida, provisoria, reseteo y marca | `TC-02001`, `TC-02002`, `TC-02003`, `TC-02004`, `TC-02005`, `TC-02007`, `TC-02009`, `TC-02025`, `TC-02026` | `INV-09` ejercido en la puerta única; `BT-02015` y `BT-02016` cerradas o elevadas |
| `e` | EP-02004 Gestión del trabajo | Constitución del trabajo, acceso del alumno y alcance del administrador | `TC-02011`, `TC-02012`, `TC-02020`, `TC-02021` | Matriz con `CU-02005`, `CU-02009` y `CU-02011` cerrados |
| `f` | EP-02005 Interpretación y verificación | Adopción del conjunto de piezas y de las observaciones, y envío | `TC-02013`, `TC-02014`, `TC-02015`, `TC-02016`, `TC-02017`, `TC-02018`, `TC-02019`, `TC-02023` | Los **ocho** escenarios del intake ejercitados; catálogo de **42** condiciones cerrado en las dos direcciones |
| `h` | EP-02006 Desenlace de la entrega | Aprobar y rechazar desde `Pendiente`, con terminalidad, y eliminación por el administrador | `TC-02022`, y reejecución de `TC-02019` y `TC-02021` | Matriz completa: 13 de 13 casos de uso, 16 de 16 reglas y 9 de 9 invariantes |

**La suma cubre los veintisiete casos de prueba.** `TC-02019` y `TC-02021` aparecen dos veces porque la etapa `h` los reejecuta con el desenlace ya construido, que es cuando la terminalidad se puede verificar de verdad.

### 5.3 `GeometriaFactory-Application`

Sin fechas y sin duraciones, por lo declarado en §1.

| Etapa | Épica | Alcance de testing | Casos de prueba en alcance | Entregable de esta categoría |
| --- | --- | --- | --- | --- |
| `a` | EP-04001 Esqueleto ambulante y verificación de viabilidad | Ninguna capacidad funcional. Se ponen en pie las pruebas de inspección estructural y la batería vacía | `TC-04026`, `TC-04027` | Batería que corre; `QG-01`, `QG-02`, `QG-04` y `QG-05` medidos por primera vez; `BT-04002` cerrada con el nombre del cuarto puerto |
| `c` | EP-04002 Identidad del administrador y sesión | Configuración del administrador, admisibilidad con su motivo y reemplazo de credencial | `TC-04003`, `TC-04008`, `TC-04009`, `TC-04031` | Matriz con `CU-04003` y `CU-04010` cerrados; primera medición de `QG-11` |
| `d` | EP-04003 Ciclo de vida de la cuenta de alumno | Auto-registro, las cuatro operaciones de admisión, la provisoria, el reseteo y **la comprobación transversal de la marca** | `TC-04001`, `TC-04002`, `TC-04004`, `TC-04005`, `TC-04006`, `TC-04007`, `TC-04010`, `TC-04011`, `TC-04029` | `INV-09` ejercido con la prueba de orden; la baja como caso testigo de la unidad de trabajo; `BT-04018` cerrada o elevada |
| `e` | EP-04004 Gestión del trabajo | Constitución y reedición del trabajo, las dos consultas con su predicado y la eliminación en sus dos alcances | `TC-04012`, `TC-04013`, `TC-04014`, `TC-04020`, `TC-04021`, `TC-04022`, `TC-04025`, `TC-04030` | Matriz con `CU-04004`, `CU-04006`, `CU-04007` y `CU-04009` cerrados; `QG-09` medido |
| `f` | EP-04005 Interpretación y verificación del dato del alumno | El envío por el puerto, con los **ocho** escenarios del intake como resultado de interpretación, y la terminación controlada | `TC-04015`, `TC-04016`, `TC-04017`, `TC-04018`, `TC-04019`, `TC-04028` | Los ocho escenarios ejercitados; catálogo de **36** condiciones cerrado en las dos direcciones; `BT-04019` mide los 500 ms sobre `E-1` |
| `h` | EP-04006 Desenlace de la entrega | Aprobar y rechazar desde `Pendiente`, con terminalidad, y la lectura del desenlace por el alumno | `TC-04023`, `TC-04024`, y reejecución de `TC-04022` y `TC-04025` | Matriz completa: 11 de 11 casos de uso, 16 de 16 reglas, 4 de 4 comprobaciones y 9 de 9 invariantes |

**La suma cubre los treinta y un casos de prueba.** `TC-04022` y `TC-04025` aparecen dos veces porque la etapa `h` los reejecuta con el desenlace ya construido, que es cuando el comentario y la terminalidad se pueden verificar de verdad.

### 5.4 `GeometriaFactory-Infrastructure`

Sin fechas y sin duraciones, por lo declarado en §1.

| Etapa | Épica | Alcance de testing | Casos de prueba en alcance | Entregable de esta categoría |
| --- | --- | --- | --- | --- |
| `a` | EP-06001 Esqueleto ambulante y verificación de viabilidad | El almacén se crea y se transforma al arrancar, y el arranque se detiene antes que operar sobre uno dudoso. **`PT-04` se mide acá** | `TC-06032`, `TC-06033` | La etapa de **verificación de transformaciones** del pipeline en pie; `QG-01`, `QG-02` y `QG-04` medidos por primera vez; `PT-04` medida |
| `c` | EP-06002 Identidad del administrador y sesión | Unicidad en el almacén, las dos preguntas sobre el conjunto, derivación y verificación de credenciales, y emisión del acceso firmado | `TC-06022`, `TC-06023`, `TC-06025`, `TC-06026`, `TC-06029`, `TC-06030`, `TC-06031`, `TC-06035` | **0** emisiones sin clave de firma y **0** apariciones de un secreto en mensajes o registro; los tres valores [ASUNCIÓN] confirmados o elevados |
| `d` | EP-06003 Ciclo de vida de la cuenta de alumno | La provisoria que el sistema produce, la marca que viaja sin ser un estado de cuenta, y el arrastre de la baja | `TC-06021`, `TC-06024`, `TC-06027`, `TC-06028` | `RN-06014` ejercida en su tramo principal y único; **0** provisorias repetidas; **0** retiros parciales con el almacén interrumpido |
| `e` | EP-06004 Gestión del trabajo | Materialización con el texto literal, consulta con el recorte ya trasladado y retiro físico | `TC-06016`, `TC-06017`, `TC-06018`, `TC-06019`, `TC-06020` | El texto original comparado carácter por carácter; **0** componentes y **0** texto original en la proyección de listado |
| `f` | EP-06005 Interpretación y verificación del dato del alumno | **El validador entero**: lectura tolerante con las cuatro trampas, derivación por tipo, tolerancia estricta y la batería de **10** casos sobre los **ocho** escenarios | `TC-06001` a `TC-06015`, y `TC-06034` | **10 de 10** casos de la batería; `E-1` con **exactamente 2** advertencias; **0** peticiones de red de los dos motores; catálogo de **17** condiciones cerrado en las dos direcciones; la medición de los 200 ms |

**La suma cubre los treinta y cinco casos de prueba.** La etapa `f` concentra dieciséis porque es donde vive el validador, que es el corazón de este proyecto de código y el riesgo de negocio que la fuente pone primero.

## 6. Recursos

### 6.1 `GeometriaFactory-Api`

| Recurso | Detalle |
| --- | --- |
| Personas | **Una**, `equipo_n = 1` (intake §2), que ejerce a la vez la construcción, la prueba y la aprobación. **El despliegue lo ejecuta el Product Owner**, no el agente |
| Ambiente | El contenedor de desarrollo, con el proceso levantado por el anfitrión en memoria de la batería de integración |
| Almacén | **Real y efímero**, el mismo motor que en producción, creado y descartado por la batería. **Nunca el almacén de desarrollo ni el de producción**; y **sin paralelismo entre pruebas que compartan archivo**, porque el motor es de escritor único |
| Datos | Los **ocho** textos literales de los escenarios del intake §20, como cuerpo de petición; y los cuatro fixtures de [`Estrategia-Testing.md`](Estrategia-Testing.md) §5, incluidos los **cinco** accesos firmados en sus formas |
| Secretos de prueba | Una clave de firma **evidentemente ficticia**, provista por configuración de prueba. **Ningún secreto real entra al repositorio, ni en el pipeline** |
| Herramientas | Las de [`Estrategia-Testing.md`](Estrategia-Testing.md) §3, nombradas por función: anfitrión en memoria, cliente de carga acotada, cliente de peticiones para forzar y el archivo de colección versionado |
| Artefactos de despliegue | El archivo de construcción **multietapa** y el de composición, que `PT-04` ejercita. **El agente los entrega y no ejecuta el despliegue** |

### 6.2 `GeometriaFactory-Domain`

| Recurso | Detalle |
| --- | --- |
| Personas | **Una**, `equipo_n = 1` (intake §2), que ejerce a la vez la construcción, la prueba y la aprobación |
| Ambiente | El contenedor de desarrollo, único ambiente de este proyecto de código. No hay ambiente desplegado que preparar |
| Datos | Los **ocho** escenarios del intake §20, en la forma que [`Estrategia-Testing.md`](Estrategia-Testing.md) §6 declara; y los cuatro fixtures de entidad de su §5 |
| Herramientas | Las de [`Estrategia-Testing.md`](Estrategia-Testing.md) §3, nombradas por función. Su elección concreta es de la etapa `a` |
| Guiones | `scripts/build.sh` y `scripts/test.sh`, que son los que el intake §17.1.P.8 · GeometriaFactory-Domain declara como puertas |

### 6.3 `GeometriaFactory-Application`

| Recurso | Detalle |
| --- | --- |
| Personas | **Una**, `equipo_n = 1` (intake §2), que ejerce a la vez la construcción, la prueba y la aprobación |
| Ambiente | El contenedor de desarrollo, único ambiente de este proyecto de código. **No hay base de datos que preparar**, y el umbral de pruebas que la tocan es 0 |
| Datos | Los **ocho** escenarios del intake §20, en la forma que [`Estrategia-Testing.md`](Estrategia-Testing.md) §6 declara; los **cuatro** dobles de puerto y los **cuatro** fixtures compartidos de su §5 |
| Herramientas | Las de [`Estrategia-Testing.md`](Estrategia-Testing.md) §3, nombradas por función. Su elección concreta es de la etapa `a` |
| Guiones | `scripts/build.sh` y `scripts/test.sh`, que son los que el intake §17.1.P.8 · GeometriaFactory-Application declara como puertas |

### 6.4 `GeometriaFactory-Infrastructure`

| Recurso | Detalle |
| --- | --- |
| Personas | **Una**, `equipo_n = 1` (intake §2), que ejerce a la vez la construcción, la prueba y la aprobación |
| Ambiente | El contenedor de desarrollo, único ambiente de este proyecto de código |
| Almacén | **Efímero, creado y descartado por cada prueba de integración interna**, con su ubicación recibida por configuración de prueba. **Nunca el almacén de desarrollo ni el de producción** |
| Datos | Los **ocho** textos literales de los escenarios del intake §20 y los cuatro fixtures de [`Estrategia-Testing.md`](Estrategia-Testing.md) §5. **Ningún texto de figuras se escribe a mano** |
| Secretos de prueba | Una clave de firma **evidentemente ficticia**, provista por configuración de prueba, y la posibilidad de **no proveerla**, que es lo que `TC-06030` necesita |
| Herramientas | Las de [`Estrategia-Testing.md`](Estrategia-Testing.md) §3, nombradas por función. Su elección concreta es de la etapa `a`, con la función de derivación de clave como punto abierto propio |
| Guiones | `scripts/build.sh`, `scripts/test.sh` y el guion de reposición del almacén al estado de primer arranque, que el intake §17.1.P.8 · GeometriaFactory-Infrastructure declara como mecanismo de reversión |

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 2.0 | 2026-08-16 | **Consolidación de la fusión** (`Audit/Migracion-M10-Consolidacion-Fusion.md` 1.1 §4). Pasa de ser el documento del proyecto de código `GeometriaFactory-Api` a ser el de la **unidad de entrega**, absorbiendo los homónimos de `GeometriaFactory-Domain`, `-Application` e `-Infrastructure`. Cada sección lleva **una subsección por proyecto de código**, con su texto transpuesto **sin reescritura**. Entra **§0** con lo que sólo se ve con los cuatro juntos. Los tres documentos absorbidos quedan archivados en `_legacy/2026-08-16-consolidacion-m10/`. Sube **major**. |
