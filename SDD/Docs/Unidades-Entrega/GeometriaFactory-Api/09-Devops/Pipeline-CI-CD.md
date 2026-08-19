# Canalización CI/CD — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** Pipeline-CI-CD.md
**Versión:** 3.0
**Estado:** Propuesto
**Fecha:** 2026-08-16
**`tipo_unidad_entrega` (D8):** `rest-api` · **Unidad de entrega principal del producto**
**Proyectos de código que la componen:** `GeometriaFactory-Api`, `GeometriaFactory-Domain`, `GeometriaFactory-Application`, `GeometriaFactory-Infrastructure` y `GeometriaFactory-Contracts`
**Trazabilidad upstream:** [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **2.1**
**Consolida a:** los documentos homónimos de las capas que componen la unidad, por `Audit/Migracion-M10-Consolidacion-Fusion.md` 1.2 §4

---

## 0. Cómo leer este documento

**La unidad de entrega tiene un solo documento de esta clase.** Cada sección lleva **una subsección
por proyecto de código**, con su texto **transpuesto sin reescritura**.

**Siete de las catorce secciones son comunes.** La canalización de la unidad es una sola y las cuatro
capas aportan sus stages; lo que sólo declara el host es **la frontera del despliegue** y **las dos
puertas técnicas** dentro de la canalización, y lo que sólo declara el dominio es **qué aporta a la
canalización de las dos unidades desplegables** — que es la única sección del corpus que mira a la
otra entrega desde adentro de ésta.

---

## 1. Alcance, y la frontera que esta categoría no cruza

### 1.1 `GeometriaFactory-Api`

`GeometriaFactory-Api` es el **proyecto de código principal del producto** y **la unidad desplegable del servidor propio**: `05` §5 declara su unidad de despliegue como **una imagen de contenedor** que lleva embebidos los tres proyectos de código que referencia.

Y tiene una frontera que conviene declarar antes que la tabla de stages, porque ordena todo el documento: **el despliegue es manual y del Product Owner**. El intake §17.1.P.8 · GeometriaFactory-Api lo declara en la fila `despliegue` de su tabla de stages: «Manual, por el docente [DECISIÓN, RT §13]. El agente IA entrega el `Dockerfile` y el `compose.yaml` y no ejecuta el despliegue». [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §3.3 lo recoge: **ningún criterio de esa categoría se cumple ejecutando un despliegue**.

**Esta categoría hereda esa frontera sin moverla**, y la traduce a una regla de canalización:

| Qué automatiza esta canalización | Qué **no** automatiza |
| --- | --- |
| Construir, probar, medir cobertura y **construir la imagen y arrancarla** para comprobar que sirve | **Poner esa imagen en el servidor propio.** Eso es un acto del Product Owner, sobre su propia máquina |

**La consecuencia es que este pipeline termina en un artefacto verificado y no en un servicio corriendo.** Todo lo que dice sobre el despliegue —cómo llega el código al destino, cómo se resuelve la dirección, qué pasa cuando cambia— es **procedimiento documentado para quien lo ejecuta a mano**, y vive en [`Guia-Publicacion-Image-Docker.md`](Guia-Publicacion-Image-Docker.md) y en [`Entornos-Deploy.md`](Entornos-Deploy.md), no en un stage.

Lo que este pipeline ejecuta y bloquea son **los quince quality gates** de `Estrategia-Calidad.md` §3. **Esta categoría no los redefine, no los relaja y no agrega ninguno.**

## 2. Stages

### 2.1 `GeometriaFactory-Api`

Los stages son los **cinco** que el intake §17.1.P.8 · GeometriaFactory-Api declara en su tabla —`build`, `test`, cobertura, imagen y despliegue— y que `05` §5 repite en su fila de etapas del pipeline. **El quinto no lo ejecuta esta canalización**, por §1, y se declara igual porque la fuente lo enumera y porque su omisión silenciosa haría creer que el ciclo termina en la imagen.

### 2.1 Tabla de stages y gates

| Stage | Qué ejecuta | Gate que verifica | Umbral | Carácter |
| --- | --- | --- | --- | --- |
| `build` | `scripts/build.sh` | `QG-01`: termina en **0 y sin advertencias** | 0 advertencias | **Bloqueante** |
| `build` | `TC-00028` y `TC-00029`, sobre la composición de raíz | `QG-10`: **4 de 4** puertos conectados a su adaptador, **0** sin adaptador o con más de uno, y **1** sola configuración de intercambio declarada en el producto | 4, 0 y 1 | **Bloqueante, con fallo en construcción** cuando falta un puerto |
| `test` | `scripts/test.sh` | `QG-02`: la batería pasa entera, **incluida la del validador** | Batería entera | **Bloqueante** |
| `test` | `TC-00007`, inspección en las dos direcciones | `QG-05`: exactamente **4** puntos de acceso fuera de la guardia de admisión, **ni uno más**, sobre los **quince** | 4 sobre 15 | **Bloqueante, sin gradación** |
| `test` | `TC-00024` y `TC-00027`, comparación en las dos direcciones | `QG-06`: **16 de 17** códigos del contrato con traducción declarada, **1** declarado sin destino con su motivo, **0** inventados y **0** renombrados | 14, 1, 0 y 0 | **Bloqueante** |
| `test` | `TC-00025` | `QG-07`: **3 de 3** familias empobrecidas con respuestas **indistinguibles en cuerpo y en código** | 3 de 3 | **Bloqueante, sin gradación** |
| `test` | `TC-00026` | `QG-08`: **0** respuestas que expongan dirección de servicio, ruta de datos, secreto o traza, sobre los **quince** puntos **y** sobre el registro del servidor | 0 | **Bloqueante.** Es `RA-03` |
| `test` | `TC-00019` | `QG-09`: **0** caracteres de diferencia entre el texto enviado y el guardado, y **0** truncamientos silenciosos | 0 y 0 | **Bloqueante, sin gradación.** Rechazar, nunca truncar |
| `test` | `TC-00031` | `QG-11`: **0** peticiones atendidas con la preparación del almacén incompleta | 0 | **Bloqueante** |
| `test` | `TC-00020`, **forzando la petición** contra la superficie | `QG-12`: **0** eliminaciones fuera de alcance aceptadas | 0 | **Bloqueante.** Es el único criterio de verificación del producto que la fuente exige ejercer **forzando la petición** |
| `cobertura` | Recolector de cobertura, con informe **por componente** | `QG-03`: **75 %** de líneas y **70 %** de ramas | 75 / 70 **[ASUNCIÓN del intake §17.1.P.6 · GeometriaFactory-Api, asunción `A-3` de §22]** | **Condicionado** |
| `cobertura` | Recuento de pruebas por clase en el informe (`TC-00037`) | `QG-04`: pirámide de **60 %** integración y **40 %** unitarias | 60 / 40 **[ASUNCIÓN del mismo origen]** | **Condicionado**; la **inversión** no es asunción. Ver §2.2 |
| `imagen` | Construcción con `deploy/Dockerfile` **multietapa** y arranque desde el contenedor de desarrollo; `TC-00033` | `QG-13`: el arranque en frío aplica las transformaciones y responde salud en menos de **30 segundos** | 30 s **[ASUNCIÓN del intake §17.1.P.10 · GeometriaFactory-Api, asunción `A-5` de §22]** | **Condicionado** |
| `imagen` | La medición de la puerta técnica `PT-04` | **No es un gate de `08`**: es puerta técnica del producto. Ver §9 | La imagen se construye, arranca, aplica las transformaciones sobre un almacén vacío y **responde salud** | **Detiene la planificación** de lo que dependa de ella |
| `test` (batería de integración) | `TC-00034` | `QG-14`: percentil 99 del listado por debajo de **500 ms** medido en el servidor, y caudal sostenido de **20 peticiones por minuto** | 500 ms y 20 por minuto **[ASUNCIÓN del intake §17.1.P.10 · GeometriaFactory-Api]** | **Condicionado** |
| Cierre de la etapa que la incorpora | `TC-00035` | `QG-15`: la colección de peticiones reproducible tiene **5 pasos o menos** y **0** datos de prueba inventados | 5 y 0 | **Bloqueante al cierre de esa etapa** |
| **`despliegue`** | **Nada, en esta canalización.** El acto es manual y del Product Owner | Ninguno de `08` se cumple ejecutándolo | — | **Fuera del alcance de la canalización.** Ver §1 y [`Guia-Publicacion-Image-Docker.md`](Guia-Publicacion-Image-Docker.md) |

**Quince gates, y ninguno movido de lugar.** Los que salen del intake §17.1.P.8 · GeometriaFactory-Api son **`QG-01`, `QG-02`, `QG-03` y `QG-13`**, uno por cada stage de su tabla que lleva un gate de `08` detrás —la quinta fila, `despliegue`, no lleva ninguno, y la de `imagen` lleva además la puerta técnica `PT-04`, que no es gate de `08`—. **`QG-04` sale de §17.1.P.6 · GeometriaFactory-Api** y **`QG-05` de `05` §8 y de `RN-00013`**; los demás salen de una fila de `05` §8, que declara los **diecisiete** NFR de este proyecto de código. Se enumeran por identificador y no por posición en la lista, porque el orden de `Estrategia-Calidad.md` §3 no es el de la fuente.

**`QG-05` merece una línea aparte, porque es el gate más caro de olvidar.** Su umbral no es «pocos puntos fuera de la guardia» sino **exactamente cuatro, ni uno más**, verificado **en las dos direcciones** sobre los quince. `05` §9 declara por qué: un punto nuevo fuera de la guardia hace que una regla del producto deje de valer **y nada falla**. Un pipeline que lo midiera en una sola dirección —comprobando que los cuatro conocidos siguen fuera— no detectaría el quinto.

### 2.2 Cuatro condicionados, y una decisión que no lo es

**`QG-03`, `QG-04`, `QG-13` y `QG-14` son condicionados**, y no lo decide esta categoría: `Estrategia-Calidad.md` §3.1 lo declara, con **`A-3` para la cobertura** —cuya celda de §22 enumera «90/85 en Domain, 85/80 en Application, 85/80 con 95 en el validador de Infrastructure, 75/70 en Api», y **no menciona la pirámide**—, **§17.1.P.6 · GeometriaFactory-Api para el reparto de la pirámide**, que es donde vive su rótulo, y `A-5` para el percentil, el caudal y el arranque en frío. **Condicionado no es opcional**: la medición se hace, el número se registra, y lo que queda en suspenso es la consecuencia automática. `BT-00025` —«Confirmar los cinco valores rotulados como asunción», etapa `d`— es la tarea que los eleva.

**Lo rotulado en `QG-04` es el reparto numérico, no la inversión de la pirámide.** `Estrategia-Calidad.md` §3.1 lo precisa: el intake §17.1.P.6 · GeometriaFactory-Api declara la inversión **a propósito**, «porque lo que este proyecto de código aporta es cableado, y el cableado se verifica ejerciéndolo». **Esa decisión no es asunción y no queda en suspenso**, y tiene una consecuencia sobre esta canalización que conviene decir: **la mayor parte del costo de ejecución de este pipeline está en la batería de integración**, no en las unitarias, y eso es deliberado.

**Y una precisión sobre el reparto de esta ola.** La regla que la Fase E fijó y que esta cadena aplica en todo el producto es que una asunción **sobre el umbral mismo** condiciona, y una asunción **sobre la forma del gate** no. Acá los cuatro rótulos son sobre umbrales —porcentajes, milisegundos, peticiones por minuto—, de modo que los cuatro condicionan. Es el caso contrario al de `GeometriaFactory-Web`, donde la única marca era sobre la forma y el gate bloquea.

### 2.3 Los stages del catálogo, uno por uno

`Rules-Devops.md` §4.2 enumera siete stages obligatorios. **Éste es el proyecto de código del producto donde más de ellos tienen sujeto**, y conviene recorrerlos:

| Stage del catálogo | Estado acá | Motivo |
| --- | --- | --- |
| Lint | **Incorporado en `build`** | El criterio es «en 0 y sin advertencias» (`QG-01`); ninguna fuente declara un linter separado |
| Build | **Existe** | `scripts/build.sh` |
| Test | **Existe, y acá vive la batería de integración del producto** | El intake §17.1.P.6 · GeometriaFactory-Api declara que golpea la superficie real por su protocolo contra el almacén real. `Estrategia-Calidad.md` §1 agrega la consecuencia: **lo que acá se rompe no lo cubre ninguna otra batería del producto** |
| SCA | **Existe, y tiene sujeto**: la imagen final lleva un entorno de ejecución y las dependencias que los tres proyectos de código embebidos traen | Ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §4 |
| SBOM | **Existe como decisión de esta categoría** | Acá sí hay artefacto que sale del repositorio. Ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §1 |
| Firma | **No se firma, y la brecha se declara** | La imagen **no se publica en ningún registro**: se construye en destino desde el repositorio (intake §17.1.P.7 · GeometriaFactory-Api). No hay artefacto en tránsito que firmar ni verificador que lo compruebe. Ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §2 |
| Publish | **No existe como publicación en un registro.** Lo que existe es la **construcción en destino**, y tiene documento propio | Intake §17.1.P.7 · GeometriaFactory-Api; [`Guia-Publicacion-Image-Docker.md`](Guia-Publicacion-Image-Docker.md) |

### 2.2 `GeometriaFactory-Domain`

Los stages son los **tres** que declaran el intake §17.1.P.8 · GeometriaFactory-Domain —«restore → build → test»— y `05` §5. Cada uno declara su comando, su verificación y su criterio de éxito. Los comandos son los **guiones del repositorio** que el intake §16 lista, y no un comando de plataforma escrito acá: el intake, en el encabezado de su Parte C, declara que todo el ciclo ocurre dentro del contenedor de desarrollo porque el equipo anfitrión no tiene el kit de desarrollo instalado, y §10 lo declara como restricción del cliente. **Un pipeline que invocara la plataforma por fuera de esos guiones no sería reproducible en la máquina de quien construye.**

### 2.1 Tabla de stages y gates

| Stage | Qué ejecuta | Gate que verifica | Umbral | Carácter |
| --- | --- | --- | --- | --- |
| `restore` | Restauración de dependencias de la plataforma para el artefacto de agrupación | Ninguno propio. Su falla detiene la construcción por sí misma | — | Bloqueante por construcción |
| `build` | `scripts/build.sh` | `QG-01`: termina en **0 y sin advertencias** | 0 advertencias | **Bloqueante** |
| `build` | El mismo guion, con el análisis estático de la plataforma activo | `QG-04`: **0** referencias a otros proyectos de código del producto y **0** a bibliotecas de persistencia, transporte o serialización | 0 y 0 | **Bloqueante** |
| `test` | `scripts/test.sh` | `QG-02`: la batería pasa entera, **0** pruebas rojas y **0** deshabilitadas sin motivo escrito | 0 y 0 | **Bloqueante** |
| `test` | Prueba de inspección `TC-02023` | `QG-05`: **42 de 42** condiciones del catálogo alcanzadas y **0** emitidas fuera de él, comparado en las dos direcciones | 42 y 0 | **Bloqueante** |
| `test` | Prueba de inspección `TC-02026` sobre la matriz de invariantes | `QG-06`: **9 de 9** invariantes con prueba de violación rechazada, **sin dobles** | 9 y 0 | **Bloqueante al cierre de etapa** |
| `test` | Recolector de cobertura, con informe **por componente** | `QG-03`: **90 %** de líneas y **85 %** de ramas | 90 / 85 **[ASUNCIÓN del intake §17.1.P.6 · GeometriaFactory-Domain, asunción `A-3` de §22]** | **Condicionado**: se mide y se registra; no bloquea la fusión |
| `test` | Duración total reportada por el ejecutor | `QG-07`: la batería completa termina en menos de **10 segundos** | 10 s **[ASUNCIÓN del intake §17.1.P.10 · GeometriaFactory-Domain, asunción `A-5` de §22]** | **Condicionado** |
| Revisión del pull request | Lectura de la superficie pública contra [`../05-Arquitectura-Tecnica/Adrs/ADR-02002-Superficie-Publica-De-Guardas-Y-Resultados-Tipados.md`](../05-Arquitectura-Tecnica/Adrs/ADR-02002-Superficie-Publica-De-Guardas-Y-Resultados-Tipados.md) | `QG-08`: ninguna condición prevista viaja como excepción de control de flujo | 0 excepciones de negocio | **Se rechaza en revisión aunque compile** |

**Por qué el gate de análisis estático no tiene stage propio de `lint`.** Ninguna fuente del producto declara un linter separado, y el criterio que un stage de `lint` verificaría ya está expresado como **cero advertencias de construcción**: `CV-20` de [`../08-Calidad-Y-Pruebas/Criterios-Validacion.md`](../08-Calidad-Y-Pruebas/Criterios-Validacion.md) declara que el análisis estático no introduce advertencias nuevas y lo hace **bloqueante por `CV-13`**, que es el gate de `build`. Abrir un stage aparte duplicaría la misma medición en dos lugares. La elección concreta de las reglas de análisis y su anclaje de versión son de la etapa `a`, por la regla de anclaje de versiones del encabezado de la Parte C del intake.

**Los dos gates condicionados se miden igual.** Condicionado no es opcional: `Estrategia-Calidad.md` §3.1 declara que la medición se hace y el resultado se registra, y lo que queda en suspenso es la consecuencia automática. El pipeline **emite el número y no lo silencia**; su incumplimiento entra como hallazgo del punto de control de la etapa, no como rechazo de la fusión. Los dos dependen de valores rotulados **[ASUNCIÓN]** en el intake §22, y `BT-02015` es la tarea que los eleva al Product Owner.

### 2.2 Los cuatro stages que no existen acá, y por qué

`Rules-Devops.md` §4.2 enumera siete stages obligatorios. Tres de ellos existen arriba; los cuatro restantes **se declaran ausentes con su motivo**, en lugar de omitirse en silencio:

| Stage del catálogo | Estado acá | Motivo |
| --- | --- | --- |
| SCA | **Se reduce a una comprobación de ausencia**, y es `QG-04` | No hay superficie que analizar: el intake §17.1.P.1 · GeometriaFactory-Domain declara este proyecto de código **sin dependencias core**, y `05` §8 fija en **0** las referencias salientes. Un análisis de composición sobre un inventario vacío no tiene sujeto; lo que sí tiene sujeto es verificar que ese cero se sostiene, y eso ya bloquea |
| SBOM | **No se genera acá** | No hay artefacto publicado del que emitir inventario. El inventario que importa es el de las dos unidades desplegables, que son las que salen del repositorio. Ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §1 |
| Firma | **No se firma acá** | Sólo se firma lo que un integrador recibe por un canal, y no hay canal ni integrador externo: `redistribuible` es false (intake §13). Ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §2 |
| Publish | **No existe** | El intake §17.1.P.7 · GeometriaFactory-Domain declara que la biblioteca **no se publica en ningún feed** y que se compila dentro de `GeometriaFactory.sln` |

### 2.3 `GeometriaFactory-Application`

Los stages son los **tres** que declara `05` §5 —`restore` → `build` → `test`—, tomados del intake §17.1.P.8 · GeometriaFactory-Application, que declara su pipeline idéntico al de §17.1.P.8 · GeometriaFactory-Domain y le agrega una puerta propia. Los comandos son los **guiones del repositorio** que el intake §16 lista, y no un comando de plataforma escrito acá: el intake, en el encabezado de su Parte C, declara que todo el ciclo ocurre dentro del contenedor de desarrollo porque el equipo anfitrión no tiene el kit de desarrollo instalado, y §10 lo declara como restricción del cliente.

### 2.1 Tabla de stages y gates

| Stage | Qué ejecuta | Gate que verifica | Umbral | Carácter |
| --- | --- | --- | --- | --- |
| `restore` | Restauración de dependencias de la plataforma para el artefacto de agrupación | Ninguno propio. Su falla detiene la construcción por sí misma | — | Bloqueante por construcción |
| `build` | `scripts/build.sh` | `QG-01`: el guion de construcción termina en **0 y sin advertencias** | 0 advertencias | **Bloqueante** |
| `build` | Inspección del archivo de proyecto (`TC-04027`) | `QG-05`: exactamente **1** referencia a otro proyecto de código del producto —`GeometriaFactory-Domain`— y **0** a bibliotecas de persistencia, transporte, serialización o marco web | 1 y 0 | **Bloqueante** |
| `test` | `scripts/test.sh` | `QG-02`: la batería pasa entera, **0** pruebas rojas y **0** deshabilitadas sin motivo escrito | 0 y 0 | **Bloqueante** |
| `test` | Prueba de inspección `TC-04026`, más la revisión del pull request | `QG-04`: **ninguna** prueba de esta capa toca la base de datos real | 0 | **Bloqueante.** Es la puerta propia del intake §17.1.P.8 · GeometriaFactory-Application. Ver §2.2 |
| `test` | Prueba de inspección `TC-04028`, en las dos direcciones | `QG-06`: **100 %** de las **36** condiciones del catálogo alcanzadas por prueba y **0** emitidas fuera de él | 36 y 0 | **Bloqueante** |
| `test` | Inspección de los **once** orquestadores y `TC-04029`, con la baja de cuenta como caso testigo | `QG-08`: **a lo sumo 1** unidad de trabajo por caso de uso y **0** casos de uso que repartan su efecto entre dos | 1 y 0 | **Bloqueante** |
| `test` | `TC-04030`, sobre la proyección que devuelve la consulta | `QG-09`: **0** componentes de pieza cargados en el listado del alumno y en el de la comisión | 0 | **Bloqueante** |
| `test` | Recolector de cobertura, con informe **por componente** | `QG-03`: **85 %** de líneas y **80 %** de ramas | 85 / 80 **[ASUNCIÓN del intake §17.1.P.6 · GeometriaFactory-Application, asunción `A-3` de §22]** | **Condicionado**: se mide y se registra; no bloquea la fusión |
| `test` | Medición sobre la batería unitaria con doble del puerto de validación | `QG-10`: el caso de uso más pesado resuelve en menos de **500 ms** para el texto semilla de **3** piezas de `E-1`, **sin acceso a base** | 500 ms **[ASUNCIÓN del intake §17.1.P.10 · GeometriaFactory-Application, asunción `A-5` de §22]** | **Condicionado** |
| Cierre de la etapa | `TC-04011` y la matriz de [`../08-Calidad-Y-Pruebas/Matriz-Cobertura-Pruebas.md`](../08-Calidad-Y-Pruebas/Matriz-Cobertura-Pruebas.md) §5 | `QG-07`: **4 de 4** comprobaciones de autorización con al menos una prueba de su negativa **sin base de datos**, y **1** sola prueba que verifique que la cuarta corta antes que las otras tres | 4, 4 y 1 | **Bloqueante al cierre de la etapa** |
| Revisión del pull request | `TC-04031` y lectura de la superficie pública contra [`../05-Arquitectura-Tecnica/Adrs/ADR-04006-Resultado-Tipado-Y-Catalogo-Cerrado-De-Condiciones.md`](../05-Arquitectura-Tecnica/Adrs/ADR-04006-Resultado-Tipado-Y-Catalogo-Cerrado-De-Condiciones.md) | `QG-11`: ninguna condición prevista viaja como excepción de control de flujo | 0 excepciones de negocio | **Se rechaza en revisión aunque compile** |

**Los dos gates condicionados se miden igual.** Condicionado no es opcional: `Estrategia-Calidad.md` §3.1 declara que la medición se hace y el resultado se registra, y lo que queda en suspenso es la consecuencia automática. El pipeline **emite el número y no lo silencia**; su incumplimiento entra como hallazgo del punto de control de la etapa, no como rechazo de la fusión. Los dos dependen de valores rotulados **[ASUNCIÓN]** en el intake §22, y `BT-04018` —«Confirmar los dos valores rotulados como asunción y fijar la puerta de cobertura», etapa `d`— es la tarea que los eleva al Product Owner.

**Ningún otro gate de este proyecto de código es condicionado**, aunque `QG-10` y `QG-03` no sean los únicos con número: los **cuatro** de `QG-07`, los **36** de `QG-06` y el **1** de `QG-05` salen de `05` §8 y de `03` §7.1, no de una marca **[ASUNCIÓN]**, y bloquean.

### 2.2 La puerta propia que el intake declara, y por qué es de construcción y no de prueba

`QG-04` es el único gate de este proyecto de código que la fuente enuncia como puerta propia: el intake §17.1.P.8 · GeometriaFactory-Application, después de declarar el pipeline idéntico al de `GeometriaFactory-Domain`, agrega **«ninguna prueba de esta capa toca la base de datos real; si una lo hace, está mal ubicada y pertenece a integración»**.

Lo que esta categoría decide, y es decisión de esta categoría, es **dónde se lo hace cumplir**:

| Aspecto | Decisión | Fundamento |
| --- | --- | --- |
| Dónde se verifica | En el stage `test`, con `TC-04026`, **y en la revisión del pull request** | `Estrategia-Calidad.md` §3, columna de verificación de `QG-04` |
| Qué lo sostiene antes de que una prueba corra | **`QG-05`**, que es de `build`: con **0** referencias a bibliotecas de persistencia declaradas en el archivo de proyecto, una prueba de esta capa **no tiene con qué** abrir un almacén. `Estrategia-Calidad.md` §3 lo declara: `QG-05` «es la propiedad que sostiene `QG-04`» | El mismo |
| Consecuencia sobre el orden de los stages | La comprobación barata corre **antes**: si `QG-05` falla en `build`, la cadena se detiene sin llegar a `test` | Decisión de esta categoría |

**El orden importa y no es cosmético.** Una prueba que abre el almacén real se detecta corriendo la batería y mirando qué archivos aparecieron; una dependencia de persistencia declarada se detecta leyendo un archivo de proyecto. La segunda es la que hace imposible a la primera, y es la que corre en el stage más barato.

### 2.3 Los stages del catálogo que no existen acá

`Rules-Devops.md` §4.2 enumera siete stages obligatorios. Tres existen arriba; los cuatro restantes **se declaran ausentes con su motivo**, en lugar de omitirse en silencio:

| Stage del catálogo | Estado acá | Motivo |
| --- | --- | --- |
| Lint | **Incorporado en `build`** | Ninguna fuente del producto declara un linter separado, y el criterio que un stage de `lint` verificaría ya está expresado como **cero advertencias de construcción** (`QG-01`). Abrir un stage aparte duplicaría la misma medición en dos lugares. La elección concreta de las reglas de análisis y su anclaje de versión son de la etapa `a` |
| SCA | **Se reduce a una comprobación de ausencia**, y es `QG-05` | El intake §17.1.P.1 · GeometriaFactory-Application declara **una sola dependencia core**, `GeometriaFactory.Domain`, que es del propio producto y no tiene dependencias externas. No hay inventario de terceros que analizar; lo que sí tiene sujeto es verificar que ese **1 y 0** se sostenga, y eso ya bloquea. Ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §4 |
| SBOM | **No se genera acá** | No hay artefacto publicado del que emitir inventario. El inventario que importa es el de la unidad desplegable del servidor propio, que es la que sale del repositorio. Ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §1 |
| Firma | **No se firma acá** | Sólo se firma lo que un integrador recibe por un canal, y no hay canal ni integrador externo: `redistribuible` es false (intake §13). Ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §2 |
| Publish | **No existe** | El intake §17.1.P.7 · GeometriaFactory-Application declara la estrategia idéntica a la de §17.1.P.7 · GeometriaFactory-Domain: **sin publicación en feed**. `05` §5 lo repite en su última fila |

### 2.4 `GeometriaFactory-Infrastructure`

Los stages son los **cuatro** que el intake §17.1.P.8 · GeometriaFactory-Infrastructure y `05` §5 fijan. Los comandos son los **guiones del repositorio** que el intake §16 lista, y todo corre dentro del contenedor de desarrollo, porque el equipo anfitrión no tiene el kit de desarrollo instalado (intake §10 y encabezado de la Parte C).

### 2.1 Tabla de stages y gates

| Stage | Qué ejecuta | Gate que verifica | Umbral | Carácter |
| --- | --- | --- | --- | --- |
| `restore` | Restauración de dependencias de la plataforma para el artefacto de agrupación | Ninguno propio. Su falla detiene la construcción por sí misma | — | Bloqueante por construcción |
| `build` | `scripts/build.sh` | `QG-01`: termina en **0 y sin advertencias** | 0 advertencias | **Bloqueante** |
| `build` | Inspección de dependencias de los **dos motores** (`TC-06014`) | `QG-08`: los dos motores originan exactamente **0** peticiones de red | 0 | **Bloqueante** |
| `test` | `scripts/test.sh` | `QG-02`: la batería pasa entera, **0** rojas y **0** deshabilitadas sin motivo escrito | 0 y 0 | **Bloqueante** |
| `test` | `TC-06001` a `TC-06010`, contra la tabla de `05` §10.5 | `QG-03`: **la batería del validador pasa entera, 10 de 10**, con los **ocho** escenarios como entrada | 10 y 8 | **Bloqueante** |
| `test` | `TC-06009` | `QG-07`: tolerancia **0.01** absoluta con operador **estricto**: `E-1` da **exactamente 2** advertencias y no 3 | 2 | **Bloqueante, y no es condicionado.** Ver §2.3 |
| `test` | `TC-06027` | `QG-09`: **0** provisorias iguales en dos producciones consecutivas sobre la misma cuenta y entre cuentas distintas, y ninguna derivable del nombre, del correo ni de la fecha | 0 | **Bloqueante** |
| `test` | `TC-06019` | `QG-10`: **0** componentes de pieza cargados y **0** apariciones del texto original en una proyección de listado | 0 y 0 | **Bloqueante** |
| `test` | `TC-06016` y `TC-06021` | `QG-11`: **0** escrituras aceptadas que reemplacen el texto original conservado, y **0** retiros parciales tras una baja interrumpida | 0 y 0 | **Bloqueante** |
| `test` | `TC-06030` | `QG-12`: **0** emisiones de acceso sin clave de firma, y **0** claves generadas al vuelo | 0 y 0 | **Bloqueante** |
| `test` | `TC-06034` y `TC-06035`, comparación en las dos direcciones | `QG-13`: **100 %** de las **17** condiciones del catálogo alcanzadas, **0** emitidas fuera de él, y **0** mensajes o trazas con un secreto, la ruta del almacén o el texto del alumno | 17, 0 y 0 | **Bloqueante** |
| `test` | Recolector de cobertura, con informe **por componente** | `QG-05`: **85 %** de líneas y **80 %** de ramas | 85 / 80 **[ASUNCIÓN del intake §17.1.P.6 · GeometriaFactory-Infrastructure, asunción `A-3` de §22]** | **Condicionado** |
| `test` | Informe de cobertura **acotado a los dos motores** | `QG-06`: **95 %** de líneas en el validador de figuras | 95 **[ASUNCIÓN del mismo origen]** | **Condicionado** |
| `test` | `TC-06015` | `QG-14`: la interpretación del texto de **3** piezas de `E-1` termina en menos de **200 ms**, medida **sin almacén** | 200 ms **[ASUNCIÓN del intake §17.1.P.10 · GeometriaFactory-Infrastructure, asunción `A-5` de §22]** | **Condicionado** |
| **`verificar-transformaciones`** | El stage propio de este proyecto de código, y `TC-06032` | `QG-04`: **las transformaciones de esquema se aplican solas sobre un almacén inexistente**, sin paso manual | 0 pasos manuales | **Bloqueante.** Criterio de aceptación de la etapa `c`. Ver §2.2 |

**Catorce gates, y ninguno movido de lugar.** Los que el intake §17.1.P.8 · GeometriaFactory-Infrastructure declara son **`QG-01`, `QG-03`, `QG-04` y `QG-05`** —construcción en cero sin advertencias, las **diez** pruebas del validador, las transformaciones aplicadas solas sobre un almacén inexistente y la cobertura de los mínimos de P.6—, que **no son los cuatro primeros de la lista**: los demás salen de una fila de `05` §8, que declara los **catorce** NFR de este proyecto de código. Se enumeran por identificador y no por posición, porque el orden de `Estrategia-Calidad.md` §3 no es el de la fuente.

### 2.2 El cuarto stage, que es propio de este proyecto de código

`QG-04` no se verifica leyendo nada: **se verifica arrancando contra un almacén que no existe y comprobando que aparece completo, sin que nadie ejecute un paso aparte**. Por eso tiene stage propio y no cabe dentro de `test`.

| Aspecto | Decisión | Fundamento |
| --- | --- | --- |
| Qué corre | La aplicación de las transformaciones de esquema **sobre un almacén inexistente**, y la comprobación de que quedó completo | Intake §17.1.P.8 · GeometriaFactory-Infrastructure, criterio de aceptación de la etapa `c` |
| Cuándo corre | Después de `test`, y **antes** de que la canalización de `GeometriaFactory-Api` construya la imagen | `05` §5, orden de las etapas del pipeline; intake §17.1.P.8 · GeometriaFactory-Api, fila de imagen |
| Sobre qué corre | Sobre un almacén **desechable** creado por la propia ejecución. **No sobre el almacén de nadie** | Decisión de esta categoría |
| Qué **no** es este stage | **No es el guion de restablecimiento.** `05` §5 declara que ese guion reproduce el estado de primer arranque y que **no es un camino de producción**: reproduce un almacén vacío | `05` §5, fila de reversión |
| Qué relación tiene con `PT-04` | Es su mitad barata. `PT-04` exige que **la imagen** arranque, aplique las transformaciones sobre un almacén vacío y responda salud; este stage verifica la parte de las transformaciones **sin construir la imagen**, y por eso un fallo se ve antes | Intake §17.1.P.8 · GeometriaFactory-Api; [`../08-Calidad-Y-Pruebas/README.md`](../08-Calidad-Y-Pruebas/README.md) §4, que asigna `PT-04` a la etapa `a` de este proyecto de código |

**La última fila es la razón de ser del stage.** `PT-04` se mide sobre la imagen del backend, que es cara de construir; este stage mide la parte que más se rompe —una transformación nueva que no cierra sobre un almacén vacío— **en el punto más barato de la cadena**. Un fallo acá evita construir una imagen que no iba a arrancar.

**Y una consecuencia sobre el linaje que esta categoría no decide sino que hereda.** [`ADR-06007`](../05-Arquitectura-Tecnica/Adrs/ADR-06007-Transformaciones-Al-Arrancar-Con-Linaje-Inmutable.md) fija que el linaje de transformaciones es inmutable, y el intake §17.1.P.7 · GeometriaFactory-Infrastructure lo dice operativamente: **cada transformación se versiona con el código de su etapa y no se edita una ya fusionada**. Este stage lo hace visible: una transformación editada después de fusionada produce un linaje distinto del que ya se aplicó en cualquier almacén existente.

### 2.3 Tres condicionados y uno que no lo es

**`QG-05`, `QG-06` y `QG-14` son condicionados**, y no lo decide esta categoría: `Estrategia-Calidad.md` §3.1 lo declara, con `A-3` para las dos coberturas y `A-5` para los 200 ms. **Condicionado no es opcional**: la medición se hace, el número se registra en el informe de cierre, y lo que queda en suspenso es la consecuencia automática. `BT-06023` —«Confirmar los valores rotulados como asunción y fijar las tres puertas de cobertura», etapa `d`— es la tarea que los eleva.

**`QG-07` no es condicionado, y confundirlo sería el error característico de esta tabla.** También lleva un número —**0.01**— pero el intake §22 lo enumera expresamente entre **«lo que NO es asunción»**, con su fundamento: sale de que el emisor redondea a dos decimales. Y hay más: el intake §17.1.P.10 · GeometriaFactory-Infrastructure declara que el operador es **estricto** —se emite advertencia cuando la diferencia absoluta es **mayor** que 0.01, no mayor o igual— y da el motivo verificable, que es el caso testigo del producto: en el escenario `E-1` el área del cilindro declara 113.10 y la suma de sus componentes da 113.09, con una diferencia de **exactamente 0.01**; con el operador estricto ese caso **no** produce advertencia y el escenario da las **dos** que §20.E-1 declara, y con «mayor o igual» daría **tres**.

**La consecuencia para el pipeline es concreta y vale escribirla**: un gate condicionado sobre `QG-07` haría que el caso de prueba canónico del producto pudiera fallar sin detener nada. El pipeline **lo bloquea**.

### 2.4 Los stages del catálogo que no existen acá

| Stage del catálogo | Estado acá | Motivo |
| --- | --- | --- |
| Lint | **Incorporado en `build`** | El criterio es «en 0 y sin advertencias» (`QG-01`), y ninguna fuente declara un linter separado. La elección concreta de las reglas de análisis y su anclaje de versión son de la etapa `a` |
| SCA | **Existe, y acá sí tiene sujeto** | Es el único de los cinco proyectos de código que no se despliegan **con dependencias externas reales**, y dos son sensibles. Ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §4 |
| SBOM | **No se genera acá** | No hay artefacto publicado del que emitir inventario. El que importa es el de la unidad desplegable del servidor propio, que es la que sale del repositorio, y **este proyecto de código aporta a ese inventario la mayor parte de sus dependencias externas**. Ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §1 |
| Firma | **No se firma acá** | No hay canal ni integrador externo: `redistribuible` es false. **No confundir con la emisión de accesos firmados**, que es una capacidad del producto y no una firma de artefacto: ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §2 |
| Publish | **No existe** | Intake §17.1.P.7 · GeometriaFactory-Infrastructure, idéntico a §17.1.P.7 · GeometriaFactory-Domain: sin publicación en feed. `05` §5 lo repite en su última fila |

## 3. Triggers

### 3.1 `GeometriaFactory-Api`

| Evento | Qué corre | Qué bloquea |
| --- | --- | --- |
| Confirmación empujada a la rama de una etapa | `build` → `test` → `cobertura` | Nada por sí solo |
| Apertura o actualización del pull request de la etapa | Lo anterior, **con la batería de integración completa** | **La fusión**, por los gates bloqueantes de §2.1 |
| **Pull request que agrega o cambia un punto de acceso** | Todo lo anterior, con `TC-00007` **reejecutado en las dos direcciones** sobre los quince | La fusión. Es la cadencia propia de este proyecto de código, y `Estrategia-Calidad.md` §5 la declara **el control que más veces hay que ejercer** |
| Pull request que **cambia el ensamblado de contratos** | Todo lo anterior. Es donde se mide el `QG-05` de `GeometriaFactory-Contracts`, que exige **100 %** de los tipos de transferencia ejercitados por al menos una prueba de integración | La fusión, también para aquel proyecto de código |
| Fusión a la rama principal | Todo lo anterior, más el stage `imagen` | El cierre de la etapa |
| Etiqueta de cierre de etapa | Todo, sobre el estado etiquetado | La declaración de etapa cerrada, y **es lo que habilita el despliegue manual** |

**No hay trigger por calendario.** El intake §10 declara «sin plazo; el avance se mide por etapas cerradas».

**La cuarta fila es una obligación que este proyecto de código recibe de otro.** [`../../GeometriaFactory-Contracts/09-Devops/Pipeline-CI-CD.md`](../../../_legacy/2026-08-15-migracion-8.2/GeometriaFactory-Contracts/09-Devops/Pipeline-CI-CD.md) §2.2 declara que su `QG-05` **no se puede correr desde aquel proyecto de código**, porque la batería que lo mide vive acá, y que mientras esta canalización no exista la prueba **se difiere por escrito** con las tres condiciones que ahí se declaran. **Desde que este proyecto de código existe, el diferimiento deja de ser admisible**: la batería está y el gate se mide.

### 3.2 `GeometriaFactory-Domain`

Explícitos por evento, y derivados del modelo de trabajo que el intake §15 declara: una rama y un pull request por etapa, con el pull request como punto de control, y **etapas en serie** —no se abre la rama de una etapa antes de fusionar la anterior— (intake §10 y §15).

| Evento | Qué corre | Qué bloquea |
| --- | --- | --- |
| Confirmación empujada a la rama de una etapa | `restore` → `build` → `test` completos | Nada por sí solo: es la señal temprana para quien construye |
| Apertura o actualización del pull request de la etapa | Los tres stages, más la revisión de `QG-08` | **La fusión**, por los gates bloqueantes de §2.1 |
| Fusión a la rama principal | Los tres stages sobre el estado fusionado | El cierre de la etapa si algo se rompió al fusionar |
| Etiqueta de cierre de etapa | Los tres stages sobre el estado etiquetado | La declaración de etapa cerrada |

**No hay trigger por calendario.** El intake §10 declara «sin plazo; el avance se mide por etapas cerradas», y una ejecución programada semanal sería una cadencia que ninguna fuente da.

### 3.3 `GeometriaFactory-Application`

Explícitos por evento, y derivados del modelo de trabajo que el intake §15 declara: una rama y un pull request por etapa, con el pull request como punto de control, y **etapas en serie** —no se abre la rama de una etapa antes de fusionar la anterior— (intake §10 y §15).

| Evento | Qué corre | Qué bloquea |
| --- | --- | --- |
| Confirmación empujada a la rama de una etapa | `restore` → `build` → `test` completos | Nada por sí solo: es la señal temprana para quien construye |
| Apertura o actualización del pull request de la etapa | Los tres stages, más la revisión de `QG-04` y de `QG-11` | **La fusión**, por los gates bloqueantes de §2.1 |
| Pull request que **agrega o cambia un caso de uso, un puerto o una condición del catálogo** | Todo lo anterior, más `TC-04028` en las dos direcciones y `TC-04029` sobre el caso de uso tocado | La fusión. Es la cadencia propia de este proyecto de código: su defecto característico es una condición emitida fuera del catálogo, y entra de a una |
| Fusión a la rama principal | Los tres stages sobre el estado fusionado | El cierre de la etapa si algo se rompió al fusionar |
| Cierre de la etapa | Lo anterior, más `TC-04011` y la matriz de comprobaciones (`QG-07`) | La declaración de etapa cerrada |
| Etiqueta de cierre de etapa | Los tres stages sobre el estado etiquetado | La declaración de etapa cerrada |

**No hay trigger por calendario.** El intake §10 declara «sin plazo; el avance se mide por etapas cerradas», y una ejecución programada semanal sería una cadencia que ninguna fuente da.

### 3.4 `GeometriaFactory-Infrastructure`

| Evento | Qué corre | Qué bloquea |
| --- | --- | --- |
| Confirmación empujada a la rama de una etapa | Los cuatro stages completos | Nada por sí solo |
| Apertura o actualización del pull request de la etapa | Los cuatro stages | **La fusión**, por los gates bloqueantes de §2.1 |
| **Pull request que agrega o cambia una transformación de esquema** | Todo lo anterior, con el stage `verificar-transformaciones` **sobre un almacén inexistente y sobre el linaje completo** | La fusión. Es la cadencia propia de este proyecto de código: una transformación que no cierra sobre un almacén vacío no se detecta ejecutando la batería |
| Pull request que **toca el validador de figuras** | Todo lo anterior, con `QG-03` sobre los **diez** casos y `QG-07` sobre el caso testigo | La fusión |
| Fusión a la rama principal | Los cuatro stages sobre el estado fusionado | El cierre de la etapa |
| Etiqueta de cierre de etapa | Los cuatro stages sobre el estado etiquetado | La declaración de etapa cerrada |

**No hay trigger por calendario.** El intake §10 declara «sin plazo; el avance se mide por etapas cerradas».

**Y no hay trigger de respaldo, aunque el respaldo exista como preocupación declarada.** El intake §17.1.P.4 · GeometriaFactory-Infrastructure declara la copia del archivo del almacén con el diario activo y su **frecuencia «a definir por el docente»**; `PA-07` de `05` §11 lo registra como punto abierto y lo dirige a esta categoría. **Esta categoría no inventa una frecuencia**: ver [`Entornos-Deploy.md`](Entornos-Deploy.md) §4.

## 4. Matriz de sistema operativo y plataforma

### 4.1 `GeometriaFactory-Api`

**Una sola combinación, y la fuente la declara como exclusiva.**

| Momento | Sistema operativo y plataforma | Fundamento |
| --- | --- | --- |
| Construcción | El del contenedor de desarrollo | Intake §17.1.P.9 · GeometriaFactory-Api y encabezado de la Parte C |
| Imagen de producción | El mismo, **con sólo el entorno de ejecución**: sin kit de desarrollo ni depurador, y **sin linaje con la imagen del contenedor de desarrollo** | Intake §17.1.P.9 · GeometriaFactory-Api; `05` §5, fila de contenido de la imagen |
| Servidor propio | El mismo | Intake §17.1.P.9 · GeometriaFactory-Api |

Justificación, del intake §17.1.P.9 · GeometriaFactory-Api: `net10.0`, **Linux exclusivamente**, porque contenedor de desarrollo, imagen de producción y servidor propio son los tres Linux. Una matriz cruzada no compraría cobertura de nadie: **el único consumidor de esta superficie es `GeometriaFactory-Web`, servidor a servidor**, y el navegador nunca la alcanza (`RA-01`).

**La fila del medio es una restricción de construcción y no un detalle de empaquetado.** «Sin linaje con la imagen del contenedor de desarrollo» significa que la imagen de producción **no se deriva** de la de desarrollo: se construye desde una base de ejecución propia. Un archivo de construcción que reusara la imagen de desarrollo llevaría el kit de desarrollo al servidor propio, y **el intake lo prohíbe explícitamente**.

**Y la asimetría con el front que la fuente declara**: si la puerta `PT-01.a` obliga a bajar la versión objetivo, se baja **la del front y no la del backend**, porque son dos artefactos independientes (intake §17.2.P.9 · GeometriaFactory-Web). Esta canalización **no se toca por esa puerta**; la única precaución heredada es la de `GeometriaFactory-Contracts`, cuyo ensamblado tiene que seguir siendo cargable por los dos procesos.

### 4.2 `GeometriaFactory-Domain`

**Una sola combinación, y es una decisión declarada y no una carencia.**

| Trigger | Sistema operativo | Plataforma objetivo |
| --- | --- | --- |
| Todos los de §3 | El del contenedor de desarrollo, que es el mismo del servidor del backend | `net10.0`, sin sufijo de plataforma |

Justificación, tomada del intake §17.1.P.9 · GeometriaFactory-Domain: la biblioteca apunta a `net10.0` sin sufijo y se ejecuta en Linux, que es el sistema operativo del contenedor de desarrollo y el del servidor del backend; **toda combinación no listada se considera no soportada**, y en particular **no** apunta a `net10.0-windows`, que es de la Actividad 1 —el emisor del dato— y no forma parte de este producto.

**Contra una matriz cruzada con un segundo sistema operativo**: no cubriría a ningún integrador real. Los dos consumidores de esta biblioteca son `GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`, del mismo producto, y las dos unidades desplegables corren sobre el mismo sistema operativo. El costo de minutos no compraría cobertura de nada.

### 4.3 `GeometriaFactory-Application`

**Una sola combinación, y es una decisión declarada y no una carencia.**

| Trigger | Sistema operativo | Plataforma objetivo |
| --- | --- | --- |
| Todos los de §3 | El del contenedor de desarrollo, que es el mismo del servidor del backend | `net10.0`, sin sufijo de plataforma |

Justificación, tomada del intake §17.1.P.9 · GeometriaFactory-Application: `net10.0`, Linux, **sin dependencias de plataforma**. `05` §5 lo repite y agrega que el ciclo de construcción ocurre dentro del contenedor de desarrollo.

**Contra una matriz cruzada con un segundo sistema operativo**: no cubriría a ningún integrador real. Los consumidores de esta biblioteca son `GeometriaFactory-Api` y `GeometriaFactory-Infrastructure`, del mismo producto, y la única unidad desplegable donde termina embebida corre sobre el mismo sistema operativo. El costo de minutos no compraría cobertura de nada.

**Una precisión que este proyecto de código no comparte con `GeometriaFactory-Contracts`.** Aquel se carga en los dos procesos y por eso una bajada de versión del front lo alcanza; **éste no llega al front**: el intake §13 declara que las dependencias de `GeometriaFactory-Web` son `GeometriaFactory-Contracts` y `GeometriaFactory-Visor`. La puerta `PT-01.a` no lo condiciona.

### 4.4 `GeometriaFactory-Infrastructure`

**Una sola combinación, y es una decisión declarada y no una carencia.**

| Trigger | Sistema operativo | Plataforma objetivo |
| --- | --- | --- |
| Todos los de §3 | El del contenedor de desarrollo, que es el mismo del servidor del backend | `net10.0`, sin sufijo de plataforma |

Justificación, tomada del intake §17.1.P.9 · GeometriaFactory-Infrastructure: `net10.0`, Linux —contenedor de desarrollo y servidor propio—, con el motor de almacenamiento **en su versión embebida por el proveedor de acceso a datos, anclada en la etapa `a`**.

**Contra una matriz cruzada con un segundo sistema operativo**: el único consumidor es `GeometriaFactory-Api`, y la única unidad desplegable donde termina embebido corre sobre el mismo sistema operativo. **Y hay un motivo más fuerte que el costo de minutos**: el motor de almacenamiento es un archivo único con un modo de diario declarado y **escritor único** (intake §17.1.P.4 · GeometriaFactory-Infrastructure), de modo que una matriz con otro sistema de archivos probaría un comportamiento que el producto nunca va a tener en ejecución.

**Este proyecto de código no llega al front**, de modo que la puerta `PT-01.a` y una eventual bajada de la versión objetivo del front **no lo alcanzan** (intake §13, columna de dependencias).

## 5. Caché y artefactos

### 5.1 `GeometriaFactory-Api`

| Aspecto | Decisión | Fundamento |
| --- | --- | --- |
| Caché de dependencias | Caché del restaurador de paquetes, con llave derivada de los archivos de proyecto del artefacto de agrupación | Decisión de esta categoría |
| Invalidación | Al cambiar cualquier archivo de proyecto. **Sin expiración por tiempo** | Un plazo en días no lo da ninguna fuente |
| **Prohibición explícita de caché** | **El almacén de la batería de integración y el del stage `imagen` no se cachean.** Se crean vacíos en cada ejecución | `QG-11` mide **0** peticiones atendidas con la preparación del almacén incompleta, y `PT-04` exige aplicar las transformaciones **sobre un almacén vacío**. Un almacén cacheado dejaría de ser vacío |
| Artefacto del stage `build` | El ensamblado compilado, con los tres proyectos de código referenciados | Intake §13 |
| Artefacto del stage `test` | La salida de la batería, **con el recuento de pruebas por clase** para `QG-04`, y los recuentos de las inspecciones | `Estrategia-Calidad.md` §3 |
| Artefacto del stage `cobertura` | El informe **por componente** | El mismo, columna de verificación de `QG-03` |
| Artefacto del stage `imagen` | **La imagen**, más el registro de su arranque: transformaciones aplicadas, salud respondida y **el tiempo de arranque en frío** | `QG-13`; `PT-04` |
| Inventario de componentes | Emitido en el stage `imagen`, sobre lo que la imagen efectivamente lleva | Decisión de esta categoría. Ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §1 |
| **Retención de la imagen** | **Ninguna.** No se guarda: se construye para verificarla y **el despliegue la reconstruye en destino desde el repositorio** | Intake §17.1.P.7 · GeometriaFactory-Api; ver §7 y [`Guia-Publicacion-Image-Docker.md`](Guia-Publicacion-Image-Docker.md) §2 |
| Retención del resto | Mientras dure el punto de control; se adjuntan al informe de cierre | Intake §15, regla de delivery 3 |

**La anteúltima fila es la más contraintuitiva de la tabla y por eso va escrita.** En una canalización convencional la imagen construida **es** el artefacto que se despliega. Acá no: el canal de entrega que el intake §17.1.P.7 · GeometriaFactory-Api declara es **construir en destino desde el repositorio, sin publicar en un registro**, de modo que la imagen que construye esta canalización **existe para ser verificada y después se descarta**. Guardarla sugeriría un camino de despliegue que no es el declarado.

### 5.2 `GeometriaFactory-Domain`

| Aspecto | Decisión | Fundamento |
| --- | --- | --- |
| Caché de dependencias | Caché del restaurador de paquetes de la plataforma, con llave derivada de los archivos de proyecto del artefacto de agrupación | Decisión de esta categoría. Es el único insumo externo del stage `restore` |
| Invalidación | Al cambiar cualquier archivo de proyecto. **No se declara ninguna expiración por tiempo** | Una expiración en días sería un plazo que ninguna fuente da |
| Artefacto del stage `build` | El ensamblado compilado, consumido en la misma ejecución por `test` y por los proyectos de código dependientes | `05` §5: el artefacto viaja embebido en sus consumidores |
| Artefacto del stage `test` | El **informe de cobertura por componente** y la salida de la batería, con su duración total | `Estrategia-Testing.md` §2 exige el informe por componente y prohíbe el número global único |
| Retención | Mientras dure el punto de control de la etapa: los dos artefactos se adjuntan al **informe de cierre** que el intake §15 declara obligatorio | El informe de cierre es autocontenido por regla del intake §15 |

**El informe de cobertura no se emite como número global.** `Estrategia-Testing.md` §2 declara que un 90 % global con el evaluador de admisibilidad en 70 % es un incumplimiento aunque el promedio cierre, de modo que el artefacto del stage `test` es la tabla por componente y no un único porcentaje.

### 5.3 `GeometriaFactory-Application`

| Aspecto | Decisión | Fundamento |
| --- | --- | --- |
| Caché de dependencias | Caché del restaurador de paquetes de la plataforma, con llave derivada de los archivos de proyecto del artefacto de agrupación | Decisión de esta categoría. Es el único insumo externo del stage `restore` |
| Invalidación | Al cambiar cualquier archivo de proyecto. **No se declara ninguna expiración por tiempo** | Una expiración en días sería un plazo que ninguna fuente da |
| Artefacto del stage `build` | El ensamblado compilado, consumido en la misma ejecución por `test`, por `GeometriaFactory-Infrastructure` y por `GeometriaFactory-Api` | Intake §13, columna de dependencias; `05` §5 |
| Artefacto del stage `test` | El **informe de cobertura por componente**, la salida de la batería y **el número de `QG-10`** | `Estrategia-Calidad.md` §3, que exige el informe por componente |
| Artefactos de inspección | Los **recuentos** de `QG-04`, `QG-05`, `QG-06`, `QG-08` y `QG-09` | Los cinco están escritos como recuentos en `Estrategia-Calidad.md` §3 |
| Retención | Mientras dure el punto de control de la etapa: se adjuntan al **informe de cierre** que el intake §15 declara obligatorio | El informe de cierre es autocontenido por regla del intake §15 |

**El informe de cobertura no se emite como número global**, y no es preferencia de esta categoría: `QG-03` se verifica sobre el informe **por componente**, y este proyecto de código tiene **8** componentes (`05` §3.1). Un promedio único podría cerrar el 85 % dejando un componente muy por debajo.

### 5.4 `GeometriaFactory-Infrastructure`

| Aspecto | Decisión | Fundamento |
| --- | --- | --- |
| Caché de dependencias | Caché del restaurador de paquetes, con llave derivada de los archivos de proyecto | Decisión de esta categoría |
| Invalidación | Al cambiar cualquier archivo de proyecto. **Sin expiración por tiempo** | Un plazo en días no lo da ninguna fuente |
| **Prohibición explícita de caché** | **El almacén del stage `verificar-transformaciones` no se cachea entre ejecuciones.** Se crea inexistente en cada una | `QG-04` mide la aplicación **sobre un almacén inexistente**; un almacén cacheado dejaría de ser inexistente y el gate mediría otra cosa |
| Artefacto del stage `build` | El ensamblado compilado, consumido en la misma ejecución por `test` y por `GeometriaFactory-Api` | Intake §13; `05` §5 |
| Artefacto del stage `test` | El **informe de cobertura por componente**, el **informe acotado a los dos motores**, la salida de la batería y el número de `QG-14` | `Estrategia-Calidad.md` §3, columnas de verificación de `QG-05`, `QG-06` y `QG-14` |
| Artefacto del stage `verificar-transformaciones` | El registro del linaje aplicado y la constancia de que **no hubo paso manual** | `QG-04` |
| Artefactos de inspección | Los **recuentos** de `QG-08` a `QG-13`, cada uno con la condición en que se midió | `Estrategia-Calidad.md` §3 |
| Retención | Mientras dure el punto de control de la etapa; se adjuntan al **informe de cierre** | Intake §15, regla de delivery 3 |

**Los dos informes de cobertura son dos y no uno**, y no es redundancia: `QG-05` mide el conjunto del proyecto de código y `QG-06` mide **sólo los dos motores**, con un piso más alto. Un informe único no permitiría verificar el segundo, que es donde el intake §17.1.P.6 · GeometriaFactory-Infrastructure puso el número más alto del producto porque es el criterio que más veces se rompe.

## 6. Promoción, y el despliegue conjunto

### 6.1 `GeometriaFactory-Api`

| Transición | Trigger | Prerrequisitos | Aprobador |
| --- | --- | --- | --- |
| Rama de etapa → rama principal | Fusión del pull request de la etapa | Los gates bloqueantes de §2.1 en verde, y los **doce** criterios de salida de [`../08-Calidad-Y-Pruebas/Plan-Pruebas.md`](../08-Calidad-Y-Pruebas/Plan-Pruebas.md) §3 | El Product Owner, con **OK explícito** en el punto de control (intake §15) |
| Etapa fusionada → etapa cerrada | Etiqueta al fusionar | La Definition of Done §1.3 entera, y la constancia de la medición de los **cuatro** criterios condicionados | El mismo |
| **Etapa cerrada → artefacto entregado** | La etiqueta, más el stage `imagen` en verde | La Definition of Done §1.4 en sus **siete** puntos, incluido **`PT-04`** | El mismo, con la constancia de la entrega en el informe de cierre |
| **Artefacto entregado → servicio desplegado** | **Un acto manual del Product Owner**, fuera de esta canalización | Los pasos de [`Guia-Publicacion-Image-Docker.md`](Guia-Publicacion-Image-Docker.md) | El mismo, que es quien lo ejecuta |
| **Cambio incompatible del contrato → producto desplegado** | Sólo con **las dos unidades desplegables desplegadas desde el mismo estado del repositorio** | El `QG-08` de `GeometriaFactory-Contracts`, que bloquea la **publicación de la etapa** | El mismo, con constancia escrita |

**La tercera y la cuarta fila son dos y no una, y separarlas es lo que la frontera de §1 exige.** La Definition of Done §1.4 lo dice con precisión: **el artefacto queda entregado, no desplegado**. La canalización llega hasta la tercera.

**Sobre la quinta fila.** La obligación es del intake §17.1.P.3 · GeometriaFactory-Contracts y [`ADR-00008`](../05-Arquitectura-Tecnica/Adrs/ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) §2 la convierte en la regla operativa que **reemplaza al versionado de rutas**. Su tratamiento completo —incluidas las tres decisiones derivadas y el hallazgo de que el desfase de momentos es irreducible mientras un extremo se despliegue a mano— está en [`../../GeometriaFactory-Web/09-Devops/Pipeline-CI-CD.md`](../../GeometriaFactory-Web/09-Devops/Pipeline-CI-CD.md) §3.2, **y esta categoría lo adopta sin duplicarlo**. Lo que agrega desde este lado es la precisión sobre **el orden**, que el intake §17.2.P.7 · GeometriaFactory-Web fijó en 1.22: **primero el backend**. Ver §7.

## 7. Reversión

### 7.1 `GeometriaFactory-Api`

| Situación | Procedimiento | Fundamento |
| --- | --- | --- |
| Una etapa fusionada rompe algo que estaba en verde | Volver a la **etiqueta de la etapa anterior** | `05` §5, fila de reversión; intake §17.1.P.7 · GeometriaFactory-Api |
| El servicio desplegado está roto | **Volver a la etiqueta anterior y reconstruir en destino.** No hay imagen publicada a la que volver: el canal construye desde el repositorio | Intake §17.1.P.8 · GeometriaFactory-Api; [`ADR-00008`](../05-Arquitectura-Tecnica/Adrs/ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) §6, trade-off 3 |
| Reemplazo de versión, en general | **Detener y arrancar, con ventana de indisponibilidad.** Sin proxy inverso no hay despliegue con solapamiento | Intake §17.1.P.8 · GeometriaFactory-Api y §17.1.P.12 · GeometriaFactory-Api; `05` §5 |
| Un cambio incompatible del contrato llegó a las dos unidades | **Se revierten las dos juntas.** La reversión desacoplada reproduce `RI-02` de [`../../../Producto/Vista-Producto.md`](../../../Producto/Vista-Producto.md) §7 | [`../../GeometriaFactory-Contracts/09-Devops/Pipeline-CI-CD.md`](../../../_legacy/2026-08-15-migracion-8.2/GeometriaFactory-Contracts/09-Devops/Pipeline-CI-CD.md) §7 |
| Una transformación de esquema quedó mal | **Volver a la etiqueta no deshace el esquema del almacén.** Se corrige con otra transformación | [`../../GeometriaFactory-Infrastructure/09-Devops/Estrategia-Versionado.md`](Estrategia-Versionado.md) §4 |

**La quinta fila es la asimetría más importante de la reversión de este producto, y no es de esta capa sino de la que embebe.** El código vuelve atrás; **el almacén no**. Cualquier procedimiento de reversión que suponga que volver a una etiqueta restituye el estado anterior **es falso para los datos**, y el único mecanismo declarado para eso es el respaldo, cuya frecuencia el intake dejó a definir por el docente.

**Y el orden del despliegue conjunto, que el Product Owner decidió en el intake 1.22.** Cuando hay que desplegar las dos unidades por un cambio incompatible, la de este proyecto de código **tiene ventana de indisponibilidad** y la del front no. Desplegar primero el front lo deja hablando con un servicio que todavía no cambió; desplegar primero el servicio deja al front viejo hablando con el servicio nuevo. **Las dos ventanas existen**, y el intake §17.2.P.7 · GeometriaFactory-Web elige la segunda: **primero el backend**, porque una API nueva normalmente acepta lo que mandaba el front anterior, mientras que un front nuevo contra una API vieja le pide algo que todavía no existe y el alumno ve el error. Lo que esta categoría agrega desde este lado sigue vigente: **el intervalo entre las dos se minimiza y se registra**, y la etapa no se cierra hasta que las dos salieron desde el mismo estado del repositorio. **El orden no vuelve automático el despliegue conjunto** —el front sale al fusionar y esta unidad se despliega a mano—, de modo que la coordinación sigue siendo un acto humano y el intervalo se minimiza en lugar de eliminarse; el propio intake 1.22 lo declara así. `PD-05` de §10 queda **cerrado**.

### 7.2 `GeometriaFactory-Domain`

| Situación | Procedimiento | Fundamento |
| --- | --- | --- |
| Una etapa fusionada rompe algo que estaba en verde | Volver a la **etiqueta de la etapa anterior**, que permite reconstruir cualquier demostración ya aprobada | Intake §17.1.P.8 · GeometriaFactory-Domain, y [`../05-Arquitectura-Tecnica/Adrs/ADR-02003-Versionado-Y-Estabilidad-De-La-Superficie.md`](../05-Arquitectura-Tecnica/Adrs/ADR-02003-Versionado-Y-Estabilidad-De-La-Superficie.md) §5 |
| Un cambio de esta biblioteca rompe la compilación de un consumidor | La rotura aparece en la construcción del consumidor, antes de cualquier despliegue. Se revierte la confirmación o se corrige en la misma rama de etapa | `ADR-02003` §7: qué constituye cambio mayor, menor y parche |
| Un cambio mayor llegó sin fila en el registro de cambios del producto | Se agrega la fila en `changelog.md` antes de cerrar la etapa | `ADR-02003` §8, métrica de cambios mayores sin registro, objetivo **0** |

**No hay delist, no hay retiro de versión y no hay ventana de gracia**, porque no hay artefacto publicado que retirar. El procedimiento de reversión de esta biblioteca **es de código y de etiqueta**, y termina ahí.

### 7.3 `GeometriaFactory-Application`

| Situación | Procedimiento | Fundamento |
| --- | --- | --- |
| Una etapa fusionada rompe algo que estaba en verde | Volver a la **etiqueta de la etapa anterior**, que permite reconstruir cualquier demostración ya aprobada | `05` §5, fila de reversión |
| Un cambio de esta biblioteca rompe la compilación de un consumidor | La rotura aparece en la construcción de `GeometriaFactory-Api` o de `GeometriaFactory-Infrastructure`, **antes de cualquier despliegue**. Se revierte la confirmación o se corrige en la misma rama de etapa | Intake §17.1.P.3 · GeometriaFactory-Application: son referencias de proyecto dentro del mismo artefacto de agrupación, y un cambio incompatible **rompe la compilación, que es la señal más temprana posible** |
| Un cambio mayor llegó sin fila en el registro de cambios del producto | Se agrega la fila en `changelog.md` antes de cerrar la etapa | [`Estrategia-Versionado.md`](Estrategia-Versionado.md) §6 |

**No hay delist, no hay retiro de versión y no hay ventana de gracia**, porque no hay artefacto publicado que retirar. El procedimiento de reversión de esta biblioteca **es de código y de etiqueta**, y termina ahí.

### 7.4 `GeometriaFactory-Infrastructure`

| Situación | Procedimiento | Fundamento |
| --- | --- | --- |
| Una etapa fusionada rompe algo que estaba en verde | Volver a la **etiqueta de la etapa anterior** | `05` §5; intake §17.1.P.7 · GeometriaFactory-Infrastructure |
| Una transformación de esquema quedó mal | **No se edita la transformación ya fusionada.** Se agrega una nueva que corrija, versionada con el código de su etapa | Intake §17.1.P.7 · GeometriaFactory-Infrastructure; [`ADR-06007`](../05-Arquitectura-Tecnica/Adrs/ADR-06007-Transformaciones-Al-Arrancar-Con-Linaje-Inmutable.md) |
| Hace falta reproducir el estado de primer arranque en desarrollo | El guion de restablecimiento que el intake §17.1.P.8 · GeometriaFactory-Infrastructure declara. **`05` §5 advierte que no es un camino de producción**: reproduce un almacén **vacío** | `05` §5, fila de reversión |
| Un cambio de esta biblioteca rompe la compilación de `GeometriaFactory-Api` | La rotura aparece en la construcción del consumidor, **antes de cualquier despliegue** | Intake §13, columna de dependencias |

**La tercera fila es la que más fácil se lee mal, y por eso la fuente ya la marcó.** El guion de restablecimiento **no restaura datos**: los borra y deja el almacén como en el primer arranque. Usarlo en el servidor propio para «arreglar» un problema haría desaparecer el trabajo de la comisión.

**No hay delist, no hay retiro de versión y no hay ventana de gracia**, porque no hay artefacto publicado que retirar.

## 8. Notificaciones

### 8.1 `GeometriaFactory-Api`

| Canal | Qué comunica | Fundamento |
| --- | --- | --- |
| La salida del pipeline sobre el pull request de la etapa | El resultado de los stages, gate por gate, con los **recuentos** de las inspecciones y el número de los cuatro condicionados | El pull request **es** el punto de control (intake §15) |
| El registro del stage `imagen` | Las transformaciones aplicadas, la salud respondida y el **tiempo de arranque en frío** | `QG-13`; `PT-04` |
| El informe de cierre de la etapa | Lo anterior, más la constancia de la **entrega del artefacto** —el archivo de construcción y el de composición—, que es donde termina la responsabilidad del agente | Definition of Done §1.4 |
| El registro de cambios del producto | Toda fila de cambio mayor | [`Estrategia-Versionado.md`](Estrategia-Versionado.md) §6 |

**No se declara ningún canal de mensajería ni ningún tablero**: ninguna fuente lo declara y `equipo_n` es 1.

**Y una regla de notificación que este proyecto de código impone sobre su propia canalización.** `QG-08` mide **0** respuestas que expongan dirección de servicio, ruta de datos, secreto o traza, **sobre los quince puntos y sobre el registro del servidor**. Eso alcanza a la salida del pipeline: **un registro de ejecución que imprimiera la dirección del servidor propio, la ruta del almacén o la clave de firma estaría produciendo, en la canalización, exactamente lo que el gate prohíbe en el producto**. Es `RA-03` aplicada al lugar más fácil de olvidar.

### 8.2 `GeometriaFactory-Domain`

| Canal | Qué comunica | Fundamento |
| --- | --- | --- |
| La salida del pipeline sobre el pull request de la etapa | El resultado de los tres stages, gate por gate, con el número de los dos condicionados | El pull request **es** el punto de control (intake §15) |
| El informe de cierre de la etapa | La medición de `QG-03` y `QG-07` con su distancia al umbral, y la constancia de los gates bloqueantes | Definition of Done §1.3 y `Criterios-Validacion.md` §6 |
| El registro de cambios del producto | Toda fila de cambio mayor de esta biblioteca | `ADR-02003` §7 |

**No se declara ningún canal de mensajería ni ningún tablero.** Ninguna fuente del producto declara uno, `equipo_n` es 1 y el destinatario de la notificación es la misma persona que ejecuta. Un escalamiento por severidad hacia un equipo que no existe sería ceremonia sin lector.

### 8.3 `GeometriaFactory-Application`

| Canal | Qué comunica | Fundamento |
| --- | --- | --- |
| La salida del pipeline sobre el pull request de la etapa | El resultado de los tres stages, gate por gate, con **los recuentos** de las cinco inspecciones y el número de los dos condicionados | El pull request **es** el punto de control (intake §15) |
| El informe de cierre de la etapa | La medición de `QG-03` y `QG-10` con su distancia al umbral, la constancia de los gates bloqueantes y el cierre de `QG-07` sobre las cuatro comprobaciones | Definition of Done §1.3 |
| El registro de cambios del producto | Toda fila de cambio mayor de esta biblioteca | [`Estrategia-Versionado.md`](Estrategia-Versionado.md) §6 |

**No se declara ningún canal de mensajería ni ningún tablero.** Ninguna fuente del producto declara uno, `equipo_n` es 1 y el destinatario de la notificación es la misma persona que ejecuta. Un escalamiento por severidad hacia un equipo que no existe sería ceremonia sin lector.

### 8.4 `GeometriaFactory-Infrastructure`

| Canal | Qué comunica | Fundamento |
| --- | --- | --- |
| La salida del pipeline sobre el pull request de la etapa | El resultado de los cuatro stages, gate por gate, con los **recuentos** de las inspecciones y el número de los tres condicionados | El pull request **es** el punto de control (intake §15) |
| El registro del stage `verificar-transformaciones` | El linaje aplicado y la constancia de que no hubo paso manual | `QG-04` |
| El informe de cierre de la etapa | La medición de `QG-05`, `QG-06` y `QG-14` con su distancia al umbral, y la constancia de los gates bloqueantes | Definition of Done §1.3 |
| El registro de cambios del producto | Toda fila de cambio mayor de esta biblioteca | [`Estrategia-Versionado.md`](Estrategia-Versionado.md) §6 |

**No se declara ningún canal de mensajería ni ningún tablero**: ninguna fuente lo declara y `equipo_n` es 1.

**Y una regla de notificación propia de este proyecto de código**: `QG-13` mide **0** mensajes o trazas con un secreto, la ruta del almacén o el texto del alumno. **Eso alcanza también a la salida del pipeline**: un registro de ejecución que imprimiera la ruta del almacén de prueba o un fragmento del texto de un escenario estaría produciendo, en la canalización, exactamente lo que el gate prohíbe en el producto.

## 9. Las dos puertas técnicas dentro de la canalización

### 9.1 `GeometriaFactory-Api`

`PT-04` y `PT-05` **no son criterios de esta categoría ni de la 08**: las declara el intake §15, y `Estrategia-Calidad.md` §3.3 las transcribe. Lo que le corresponde a 09 es declarar cómo se ejecutan dentro de la canalización:

| Puerta | Cuándo corre | Sobre qué | Qué pasa si no pasa |
| --- | --- | --- | --- |
| **`PT-04`** | Etapa `a`, con `BT-00004` | Sobre **la imagen**, construida con el archivo de construcción multietapa y arrancada **desde el contenedor de desarrollo**: aplica las transformaciones sobre un almacén vacío y **responde salud** | **Detiene la planificación** de lo que dependa de ella. Sin ella, el artefacto del servidor propio no se puede construir ni arrancar |
| **`PT-05`** | Etapa `i`, **fuera del tramo comprometido** | Sobre el **despliegue real**: valida la premisa completa de la topología. Es la única puerta del producto que **no se puede medir sin desplegar** | El despliegue real. La fuente **recomienda no relegarla**, y el intake §15 registra que el 2026-08-08 su letra corrió de `h` a `i` al insertarse una etapa, **sin que la puerta se despegue del despliegue real** |

**`PT-04` se mide desde el contenedor de desarrollo y no en el servidor propio**, y eso es lo que la vuelve barata: verifica que el artefacto sirve **antes** de que exista la oportunidad de desplegarlo mal. Su mitad más frágil —que las transformaciones cierren sobre un almacén vacío— ya se verificó antes, en el stage `verificar-transformaciones` de `GeometriaFactory-Infrastructure`, que es la mitad barata de esta puerta.

**`PT-05` es la única puerta del producto que esta canalización no puede ejecutar ni preparar**, porque depende de un acto manual del Product Owner sobre su propia red. Lo que esta categoría hace es **documentar el procedimiento** para que esa medición sea reproducible: [`Guia-Publicacion-Image-Docker.md`](Guia-Publicacion-Image-Docker.md) §3.

## 10. Puntos abiertos

### 10.1 `GeometriaFactory-Api`

| Id | Punto abierto | Quién lo cierra | Dónde se cierra (artefacto y sección) | Estado |
| --- | --- | --- | --- | --- |
| PD-01 | La **construcción de la imagen en destino desde el repositorio**, que el intake §17.1.P.11 · GeometriaFactory-Api punto 5 rotula **[A VERIFICAR]** y exige probar **una vez antes de depender del mecanismo**: requiere que el motor de contenedores del destino resuelva la referencia al repositorio y tenga credenciales si es privado. Es `PA-08` de `05` §11, que lo dirige a esta categoría **para medirlo, no para decidirlo** | El equipo, midiendo, con el procedimiento de [`Guia-Publicacion-Image-Docker.md`](Guia-Publicacion-Image-Docker.md) §2 | [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §2.1, fase `i` · Despliegue real | **Vigente.** La fase `i` está planificada y **no ocurrió** |
| PD-02 | La **herramienta concreta** de cada stage —ejecutor de pruebas, recolector de cobertura, generador del inventario— y su anclaje de versión | El equipo, en el punto de control de la etapa `a` | [`../../../../../changelog.md`](../../../../../changelog.md), etapa `a` § «Decidido en esta etapa, y elevado al punto de control» | **VENCIDO.** La etapa `a` cerró el **2026-08-13** y el punto sigue abierto |
| PD-03 | La **confirmación de los valores rotulados [ASUNCIÓN]** que hoy dejan condicionados a `QG-03`, `QG-04`, `QG-13` y `QG-14`. Confirmados, los cuatro pasan a bloqueantes sin ningún otro cambio de este documento | El Product Owner, sobre el intake §22, por `BT-00025` | [`../../../../../changelog.md`](../../../../../changelog.md), etapa `d` § «Decidido en esta etapa, y elevado al punto de control» | **VENCIDO.** La etapa `d` cerró el **2026-08-15** y el punto sigue abierto |
| PD-04 | La **vigencia exacta del acceso firmado**, que el intake declara «corta» sin fijar número y `ADR-00003` toma de configuración. Alcanza a esta categoría porque **es un valor de configuración del ambiente**, no del código | El equipo en la etapa `a`, y el Product Owner si quisiera fijarlo. Es `PA-04` de `05` §11 | [`../../../../../changelog.md`](../../../../../changelog.md), etapa `a` § «Decidido en esta etapa, y elevado al punto de control» | **VENCIDO.** La etapa `a` cerró el **2026-08-13** y el punto sigue abierto |
| PD-05 | ~~El **orden entre los dos despliegues** ante un cambio incompatible del contrato. Ninguna fuente lo elige, y las dos alternativas dejan una ventana de desajuste. Esta categoría declara que **el intervalo se minimiza y se registra**, y no inventa un orden~~ · **CERRADO el 2026-08-11**: el intake **1.22** §17.2.P.7 · GeometriaFactory-Web elige **primero el backend**, sin volver automático el despliegue conjunto. Ver §7 | El Product Owner, que es quien ejecuta los dos actos | Cerrado con el intake 1.22 | **Cerrado** |

**`PA-08` de `05` §11 queda con procedimiento pero no cerrado**, y la distinción importa: esta categoría escribe **cómo se prueba** el mecanismo de construcción en destino, pero **no declara que funcione**. La fuente lo rotula **[A VERIFICAR]** y sólo la medición lo cierra.

### 10.2 `GeometriaFactory-Domain`

| Id | Punto abierto | Quién lo cierra | Dónde se cierra (artefacto y sección) | Estado |
| --- | --- | --- | --- | --- |
| PD-01 | La **herramienta concreta** de cada stage —ejecutor de pruebas, recolector de cobertura y reglas de análisis estático— y su anclaje de versión | El equipo, en el punto de control de la etapa `a` | [`../../../../../changelog.md`](../../../../../changelog.md), etapa `a` § «Decidido en esta etapa, y elevado al punto de control» | **VENCIDO.** La etapa `a` cerró el **2026-08-13** y el punto sigue abierto |
| PD-02 | La **confirmación de los dos valores rotulados [ASUNCIÓN]** que hoy dejan condicionados a `QG-03` y `QG-07`. Confirmados, los dos pasan a bloqueantes sin ningún otro cambio de este documento | El Product Owner, sobre el intake §22 | [`../../../../../changelog.md`](../../../../../changelog.md), etapa `d` § «Decidido en esta etapa, y elevado al punto de control» | **VENCIDO.** La etapa `d` cerró el **2026-08-15** y el punto sigue abierto |
| PD-03 | Si el mutation score entra alguna vez al pipeline. Hoy `CV-19` se reporta «sin medir» y su hueco está declarado | La categoría 08, si la herramienta se elige | **Falta declarar el evento** | **No conforme con §12.2**: sin evento de cierre, nada lo puede vencer. **A declarar por el Product Owner** |

**Ninguno de los tres se cierra inventando un valor.** El tratamiento de `PD-02` es el que la Fase E ya declaró y esta categoría lo adopta sin cambiarlo.

### 10.3 `GeometriaFactory-Application`

| Id | Punto abierto | Quién lo cierra | Dónde se cierra (artefacto y sección) | Estado |
| --- | --- | --- | --- | --- |
| PD-01 | La **herramienta concreta** de cada stage —ejecutor de pruebas, recolector de cobertura y reglas de análisis estático— y su anclaje de versión | El equipo, en el punto de control de la etapa `a` | [`../../../../../changelog.md`](../../../../../changelog.md), etapa `a` § «Decidido en esta etapa, y elevado al punto de control» | **VENCIDO.** La etapa `a` cerró el **2026-08-13** y el punto sigue abierto |
| PD-02 | La **confirmación de los dos valores rotulados [ASUNCIÓN]** que hoy dejan condicionados a `QG-03` y `QG-10`. Confirmados, los dos pasan a bloqueantes sin ningún otro cambio de este documento | El Product Owner, sobre el intake §22, por `BT-04018` | [`../../../../../changelog.md`](../../../../../changelog.md), etapa `d` § «Decidido en esta etapa, y elevado al punto de control» | **VENCIDO.** La etapa `d` cerró el **2026-08-15** y el punto sigue abierto |
| PD-03 | La **herramienta que calcula la versión** a partir de las convenciones de mensaje de confirmación. Es `PA-06` de `05` §11 y esta categoría **la declara por su función y no la elige** | El equipo, en la etapa `a` | [`../../../../../changelog.md`](../../../../../changelog.md), etapa `a` § «Decidido en esta etapa, y elevado al punto de control» | **VENCIDO.** La etapa `a` cerró el **2026-08-13** y el punto sigue abierto |

**Ninguno de los tres se cierra inventando un valor.** El tratamiento de `PD-02` es el que la Fase E ya declaró y esta categoría lo adopta sin cambiarlo.

### 10.4 `GeometriaFactory-Infrastructure`

| Id | Punto abierto | Quién lo cierra | Dónde se cierra (artefacto y sección) | Estado |
| --- | --- | --- | --- | --- |
| PD-01 | La **herramienta concreta** de cada stage —ejecutor de pruebas, recolector de cobertura, reglas de análisis estático y la herramienta de transformaciones como herramienta local del repositorio— y su anclaje de versión | El equipo, en el punto de control de la etapa `a` | [`../../../../../changelog.md`](../../../../../changelog.md), etapa `a` § «Decidido en esta etapa, y elevado al punto de control» | **VENCIDO.** La etapa `a` cerró el **2026-08-13** y el punto sigue abierto |
| PD-02 | La **confirmación de los tres valores rotulados [ASUNCIÓN]** que hoy dejan condicionados a `QG-05`, `QG-06` y `QG-14`. Confirmados, los tres pasan a bloqueantes sin ningún otro cambio de este documento | El Product Owner, sobre el intake §22, por `BT-06023` | [`../../../../../changelog.md`](../../../../../changelog.md), etapa `d` § «Decidido en esta etapa, y elevado al punto de control» | **VENCIDO.** La etapa `d` cerró el **2026-08-15** y el punto sigue abierto |
| PD-03 | **Cuál de las dos funciones de derivación de clave se ancla, y con qué parámetros.** El intake §17.1.P.1 · GeometriaFactory-Infrastructure declara «PBKDF2 o Argon2» y **no elige**; `PA-03` de `05` §11 deja la forma y el criterio fijados por `ADR-06004` y la elección concreta en la regla de anclaje. **Esta categoría no la elige**, y declara su efecto de canalización: es una dependencia externa del artefacto del servidor propio y entra en su inventario | El equipo, en la etapa `a` | [`../../../../../changelog.md`](../../../../../changelog.md), etapa `a` § «Decidido en esta etapa, y elevado al punto de control» | **VENCIDO.** La etapa `a` cerró el **2026-08-13** y el punto sigue abierto |
| PD-04 | La **frecuencia del respaldo del almacén**, que el intake §17.1.P.4 · GeometriaFactory-Infrastructure declara «a definir por el docente» y `PA-07` de `05` §11 dirige a esta categoría. **No se inventa un número**: ver [`Entornos-Deploy.md`](Entornos-Deploy.md) §4 | El Product Owner | **Falta declarar el evento** | **No conforme con §12.2**: sin evento de cierre, nada lo puede vencer. **A declarar por el Product Owner** |
| PD-05 | **El ADR que `Rules-Devops.md` §2.2 pide para apartarse del modelo de canales `preview` / `stable`, y que este proyecto de código no tiene.** Las otras tres bibliotecas lo anclan en su `ADR-06003`; las siete ADR de acá no cubren publicación ni canales, de modo que el apartamiento de [`Entornos-Deploy.md`](Entornos-Deploy.md) §1.1 se apoya hoy en el intake §17.1.P.7 · GeometriaFactory-Infrastructure y §13 y no en el instrumento que la regla nombra. **El apartamiento no está en duda; falta el instrumento** | La categoría 05 de este proyecto de código, emitiendo la ADR correspondiente | `05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`, su próxima emisión | **Vigente.** No hubo emisión nueva de la 05 |

## 11. Alcance y qué no es este pipeline

### 11.1 `GeometriaFactory-Domain`

`GeometriaFactory-Domain` es una **biblioteca, no un servicio desplegable**. `05` §5 lo declara sin unidad de despliegue propia: su artefacto se compila dentro del artefacto de agrupación del producto y viaja embebido en las dos unidades desplegables por la vía de sus consumidores. `redistribuible` es false y el intake §13 declara que **ningún proyecto de código del producto se publica como paquete redistribuible**, porque ninguna fuente declara publicación en un feed.

De ahí el alcance de este documento, y conviene decirlo antes que nada para que no se lo lea como un pipeline recortado: **su DevOps es compilación, prueba y verificación estructural**. No hay empaquetado propio, no hay publicación, no hay ambientes y no hay despliegue. Inventarle cualquiera de esas cuatro cosas sería inventar un acto que este proyecto de código no ejecuta.

Lo que sí hay, y es lo que este pipeline ejecuta y bloquea, son **los ocho quality gates** que [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §3 declara. **Esta categoría no los redefine, no los relaja y no agrega ninguno**: los materializa como stages, con el carácter que la Fase E les fijó.

### 11.2 `GeometriaFactory-Application`

`GeometriaFactory-Application` es una **biblioteca de casos de uso, no un servicio desplegable**. [`../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`](../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md) §5 lo declara sin unidad de despliegue propia: su artefacto se compila dentro del artefacto de agrupación del producto y **viaja embebido en la unidad desplegable del servidor propio, por la vía de `GeometriaFactory-Api`**. `redistribuible` es false y el intake §13 declara que **ningún proyecto de código del producto se publica como paquete redistribuible**.

De ahí el alcance: **compilación, prueba con dobles y verificación estructural**. No hay empaquetado propio, no hay publicación, no hay ambientes y no hay despliegue. Inventarle cualquiera de esas cuatro cosas sería inventar un acto que este proyecto de código no ejecuta.

Hay además un rasgo que ordena todo el documento y conviene decirlo antes que nada: **la calidad de este proyecto de código se mide entera sin ambiente**. [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §1 lo declara: cada uno de los **once** casos de uso se puede ejercer entero con dobles de los **cuatro** puertos, sin base de datos y sin frontera de proceso, y por eso **no hay ambiente donde descubrir un defecto suyo**: se descubre en una prueba que falla o en una revisión que rechaza. Un pipeline que le levantara un ambiente para probarlo estaría contradiciendo la propiedad que justifica el estilo entero del proyecto de código.

Lo que este pipeline ejecuta y bloquea son **los once quality gates** que `Estrategia-Calidad.md` §3 declara. **Esta categoría no los redefine, no los relaja y no agrega ninguno**: los materializa como stages, con el carácter que la Fase E les fijó.

### 11.3 `GeometriaFactory-Infrastructure`

`GeometriaFactory-Infrastructure` es una **biblioteca de adaptadores, no un servicio desplegable**. [`../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`](../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md) §5 lo declara sin unidad de despliegue propia: se compila dentro del artefacto de agrupación y **viaja embebido en la unidad desplegable del servidor propio, por la vía de `GeometriaFactory-Api`**. `redistribuible` es false y el intake §13 declara que ningún proyecto de código del producto se publica como paquete redistribuible.

Pero **es la biblioteca del producto con más superficie de canalización**, y conviene decir por qué antes de la tabla de stages. Tres rasgos, los tres de la fuente:

1. **Tiene un stage que ninguna otra biblioteca tiene.** El intake §17.1.P.8 · GeometriaFactory-Infrastructure declara los stages «restore → build → test → **verificación de migraciones**», y `05` §5 lo repite: la cuarta etapa **es propia de este proyecto de código**.
2. **Es el único de los cinco proyectos de código que no se despliegan con dependencias externas reales**, y con dos de ellas sensibles: la biblioteca de derivación de clave y la de emisión de acceso firmado (intake §17.1.P.1 · GeometriaFactory-Infrastructure).
3. **Acá viven las dos piezas sensibles del producto** —la derivación de la contraseña y la emisión del acceso firmado— y **la clave de firma se provee desde afuera y nunca entra al repositorio ni a la imagen** (intake §17.1.P.5 · GeometriaFactory-Infrastructure).

Lo que este pipeline ejecuta y bloquea son **los catorce quality gates** de [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §3. **Esta categoría no los redefine, no los relaja y no agrega ninguno**: los materializa como stages, con el carácter que la Fase E les fijó.

**Lo que no hay**: empaquetado propio, publicación, ambientes y despliegue. `05` §5 declara la publicación como «no se publica» y la reversión como un guion de restablecimiento que **no es un camino de producción**.

## 12. Promoción

### 12.1 `GeometriaFactory-Domain`

**No hay canales entre los que promover, y declararlo es más honesto que inventar dos.** El modelo de `Rules-Devops.md` §2.2 para el tipo `library` es un par de canales sobre un feed único; acá **no hay feed**, y el propio catálogo de reglas declara anti-patrón confundir publicación con despliegue. Ver [`Entornos-Deploy.md`](Entornos-Deploy.md) §1, donde el apartamiento queda declarado con el ADR que ya lo sostiene.

La única promoción que este proyecto de código ejecuta es la **de estado del trabajo**, y es la del producto entero:

| Transición | Trigger | Prerrequisitos | Aprobador |
| --- | --- | --- | --- |
| Rama de etapa → rama principal | Fusión del pull request de la etapa | Los gates bloqueantes de §2.1 en verde, y los **nueve** criterios de salida de [`../08-Calidad-Y-Pruebas/Plan-Pruebas.md`](../08-Calidad-Y-Pruebas/Plan-Pruebas.md) §3 cumplidos | El Product Owner, con **OK explícito** en el punto de control (intake §15) |
| Etapa fusionada → etapa cerrada | Etiqueta al fusionar | La Definition of Done §1.3 entera, incluida la constancia de la medición de los criterios condicionados | El mismo, con constancia escrita en el informe de cierre |

**El aprobador humano no es un agregado de esta categoría.** El intake §15 declara el punto de control bloqueante y `equipo_n` es 1: la misma persona construye y aprueba, y `Estrategia-Calidad.md` §4 ya declara que esta situación no se disimula con un RACI de tres columnas.

### 12.2 `GeometriaFactory-Application`

**No hay canales entre los que promover, y declararlo es más honesto que inventar dos.** El modelo de `Rules-Devops.md` §2.2 para el tipo `library` es un par de canales sobre un feed único; acá **no hay feed**, y el propio catálogo de reglas declara anti-patrón confundir publicación con despliegue. Ver [`Entornos-Deploy.md`](Entornos-Deploy.md) §1.

La única promoción que este proyecto de código ejecuta es la **de estado del trabajo**:

| Transición | Trigger | Prerrequisitos | Aprobador |
| --- | --- | --- | --- |
| Rama de etapa → rama principal | Fusión del pull request de la etapa | Los gates bloqueantes de §2.1 en verde, y los **once** criterios de salida de [`../08-Calidad-Y-Pruebas/Plan-Pruebas.md`](../08-Calidad-Y-Pruebas/Plan-Pruebas.md) §3 cumplidos | El Product Owner, con **OK explícito** en el punto de control (intake §15) |
| Etapa fusionada → etapa cerrada | Etiqueta al fusionar | La Definition of Done §1.3 entera, incluida la constancia de la medición de los criterios condicionados | El mismo, con constancia escrita en el informe de cierre |

**El aprobador humano no es un agregado de esta categoría.** El intake §15 declara el punto de control bloqueante y `equipo_n` es 1: la misma persona construye y aprueba, y `Estrategia-Calidad.md` §4 ya declara que esta situación no se disimula con un RACI de tres columnas.

### 12.3 `GeometriaFactory-Infrastructure`

**No hay canales entre los que promover.** El modelo de `Rules-Devops.md` §2.2 para el tipo `library` es un par de canales sobre un feed único; acá **no hay feed**. Ver [`Entornos-Deploy.md`](Entornos-Deploy.md) §1.

| Transición | Trigger | Prerrequisitos | Aprobador |
| --- | --- | --- | --- |
| Rama de etapa → rama principal | Fusión del pull request de la etapa | Los gates bloqueantes de §2.1 en verde, y los **once** criterios de salida de [`../08-Calidad-Y-Pruebas/Plan-Pruebas.md`](../08-Calidad-Y-Pruebas/Plan-Pruebas.md) §3 | El Product Owner, con **OK explícito** en el punto de control (intake §15) |
| Etapa fusionada → etapa cerrada | Etiqueta al fusionar | La Definition of Done §1.3 entera, incluida la constancia de la medición de los **tres** criterios condicionados | El mismo, con constancia escrita en el informe de cierre |

**El aprobador humano no es un agregado de esta categoría**: el intake §15 declara el punto de control bloqueante y `equipo_n` es 1.

## 13. Qué aporta a la canalización de las dos unidades desplegables

### 13.1 `GeometriaFactory-Domain`

Este proyecto de código no se despliega, pero **condiciona** a las dos canalizaciones que sí despliegan. Lo que aporta, y que la canalización de nivel producto tiene que respetar:

| Aporte | Efecto sobre la canalización de la unidad desplegable | Fundamento |
| --- | --- | --- |
| Es **nivel topológico 0** y no tiene dependencias | Se construye **antes** que `GeometriaFactory-Application`, `GeometriaFactory-Infrastructure` y `GeometriaFactory-Api`. Ningún orden de construcción puede ubicarlo después de sus dependientes | Intake §13, orden topológico |
| Sus gates corren **antes** de que la imagen del backend se construya | Un fallo de `QG-01`, `QG-02`, `QG-04`, `QG-05` o `QG-06` detiene la cadena en el punto más barato posible, sin llegar al empaquetado | `05` §5 y la tabla de §2.1 |
| Viaja **embebido** en la imagen del backend | No hay paso de publicación intermedio ni versión que resolver: la imagen se construye desde el mismo estado del repositorio | Intake §13: los dos artefactos entregables del producto son una imagen y una publicación por FTP |
| Un cambio mayor suyo **no** obliga por sí solo a redesplegar el front | El front no lo referencia: sus dependencias son `GeometriaFactory-Contracts` y el bundle del visor | Intake §13, columna de dependencias |

## 14. Qué aporta a la canalización de la unidad desplegable del servidor propio

### 14.1 `GeometriaFactory-Application`

Este proyecto de código no se despliega, pero **condiciona** a la única canalización donde termina embebido:

| Aporte | Efecto sobre la canalización de la unidad desplegable | Fundamento |
| --- | --- | --- |
| Es **nivel topológico 1** y depende sólo de `GeometriaFactory-Domain` | Se construye **después** de `GeometriaFactory-Domain` y **antes** de `GeometriaFactory-Infrastructure` y de `GeometriaFactory-Api` | Intake §13, orden topológico |
| Sus gates corren **antes** de que la imagen del backend se construya | Un fallo de `QG-01`, `QG-02`, `QG-04`, `QG-05`, `QG-06`, `QG-08` o `QG-09` detiene la cadena en el punto más barato posible, sin llegar al empaquetado | `05` §5 y la tabla de §2.1 |
| Viaja **embebido** en la imagen del backend | No hay paso de publicación intermedio ni versión que resolver: la imagen se construye desde el mismo estado del repositorio | `05` §5; intake §13 |
| Define los **cuatro puertos** que `GeometriaFactory-Infrastructure` implementa | Un cambio de puerto rompe la construcción del adaptador en la misma ejecución, sin llegar a la imagen | Intake §14, fila de `GeometriaFactory-Application` |
| **No llega al front** | Un cambio suyo **no obliga a republicar el front**: el front no lo referencia | Intake §13, columna de dependencias |

### 14.2 `GeometriaFactory-Infrastructure`

| Aporte | Efecto sobre la canalización de la unidad desplegable | Fundamento |
| --- | --- | --- |
| Es **nivel topológico 2** | Se construye después de `GeometriaFactory-Domain` y de `GeometriaFactory-Application`, y **antes** de `GeometriaFactory-Api` | Intake §13, orden topológico |
| Su stage `verificar-transformaciones` corre **antes** de que la imagen se construya | Es la mitad barata de `PT-04`: un linaje que no cierra sobre un almacén vacío se detecta **sin construir la imagen** | §2.2 |
| Aporta **la mayor parte de las dependencias externas** del artefacto del servidor propio | El inventario de esa unidad depende de lo que se ancle acá, incluidas las **dos** bibliotecas sensibles | Intake §17.1.P.1 · GeometriaFactory-Infrastructure; [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §1 |
| **Recibe la clave de firma, no la busca** | La canalización de la unidad desplegable tiene que proveerla por variable de entorno o archivo montado; **si no llega, el arranque falla con su condición declarada** en lugar de generar una al vuelo | `05` §5, fila de secretos; `QG-12` |
| El almacén va a un **volumen persistente y nunca dentro de la imagen** | Es una restricción sobre cómo se arma esa imagen, y no sobre este ensamblado | Intake §17.1.P.4 · GeometriaFactory-Infrastructure; `05` §5 |
| **No llega al front** | Un cambio suyo no obliga a republicar el front | Intake §13 |

## 15. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 2.0 | 2026-08-16 | **Consolidación de la fusión** (`Audit/Migracion-M10-Consolidacion-Fusion.md` 1.2 §4). Pasa de ser el documento de un proyecto de código a ser el de la **unidad de entrega**, con una subsección por proyecto y su texto transpuesto **sin reescritura**. Entra **§0**. Los absorbidos quedan archivados. Sube **major**. |
| 3.0 | 2026-08-19 | **Migración normativa 9.12 → 10.0, fase M4.** Las **16** filas de puntos abiertos pasan a la forma de `Root-Rules.md` **§12.2**: la columna «Cuándo» —que nombraba **momentos**— se reemplaza por **«Dónde se cierra (artefacto y sección)»** y entra la columna **«Estado»**. Un momento no deja rastro que alguien pueda abrir, y un cierre que nadie comprueba no ocurre. **Al nombrar el artefacto, 11 quedaron VENCIDAS**: su evento apunta a un punto de control de etapa ya cerrada o a la categoría 09 ya emitida. **2** quedan **sin evento declarado** —decían «sin fecha comprometida»— y §12.2 exige uno: **se marcan como no conformes y quedan para el Product Owner**, porque inventarles un evento sería exactamente lo que esta migración vino a impedir. **Ningún punto abierto se cierra acá y ninguno se inventa**: la migración los vuelve contables. Sube **major**: cambia la estructura de la tabla. | Orquestador de migración normativa SDD |
