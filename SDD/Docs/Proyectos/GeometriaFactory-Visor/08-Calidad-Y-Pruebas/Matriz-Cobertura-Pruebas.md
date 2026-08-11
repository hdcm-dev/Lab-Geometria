# Matriz de cobertura de pruebas — GeometriaFactory-Visor

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Visor
**Documento:** Matriz-Cobertura-Pruebas.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) 1.0; [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) 1.2 §3, §5.2 y §6; [`../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md) 1.1 §3.2 y §6; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §3.1, §8 y §10.2
**Trazabilidad downstream:** [`Criterios-Validacion.md`](Criterios-Validacion.md), [`Definition-Of-Done.md`](Definition-Of-Done.md), [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md); `09-Devops`

---

## Tabla de contenido

- [1. Propósito y alcance](#1-propósito-y-alcance)
- [2. Trazabilidad CU ↔ tests](#2-trazabilidad-cu--tests)
  - [2.1 El caso de prueba de la puerta técnica `PT-02`](#21-el-caso-de-prueba-de-la-puerta-técnica-pt-02)
- [3. Trazabilidad NFR ↔ tests](#3-trazabilidad-nfr--tests)
- [4. Trazabilidad RN ↔ tests](#4-trazabilidad-rn--tests)
- [5. Trazabilidad garantía ↔ tests](#5-trazabilidad-garantía--tests)
- [6. Trazabilidad código de condición ↔ tests](#6-trazabilidad-código-de-condición--tests)
- [7. Cobertura por capa](#7-cobertura-por-capa)
- [8. Huecos identificados](#8-huecos-identificados)
- [9. Control de cambios](#9-control-de-cambios)

---

## 1. Propósito y alcance

Relaciona los **siete** casos de uso, los **ocho** NFR, las **dieciséis** reglas de negocio del producto, las **siete** garantías del contrato de fachada y los **siete** códigos de condición con los **veintiún** casos de prueba de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md), y declara la cobertura por componente.

**Ninguna columna de estado afirma que algo esté verde.** El bundle no está construido.

Esta matriz **agrega dos tablas** a las tres que `Rules-Calidad-Y-Pruebas.md` §4.5 exige: la de garantías y la de códigos de condición. Las dos tienen fundamento en la arquitectura: `05` §10.2 declara que **las siete garantías son parte del contrato y no detalles de implementación**, de modo que perder una es cambio mayor aunque las seis firmas no se toquen; y `05` §9 declara como riesgo que un código se acuñe aguas abajo y que 03 y 08 se desincronicen.

## 2. Trazabilidad CU ↔ tests

Siete filas, una por caso de uso de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3. Ninguna se agrupa.

| CU | Criterio Given-When-Then principal | Test | Tipo | Estado |
| --- | --- | --- | --- | --- |
| CU-01 Inicializar una instancia del visor | Given un elemento de dibujo con tamaño, When se crea la instancia, Then se devuelve un identificador y la escena queda viva; sin capacidad gráfica o con el elemento sin tamaño, Then se informa la condición y **no se crea instancia** | `TC-01`, `TC-02`, `TC-03` | Integración y extremo a extremo | `Pendiente` |
| CU-02 Cargar el texto del trabajo y dibujar | Given el texto de un trabajo, When se lo carga, Then se dibujan sus piezas y **toda pieza no dibujada queda enumerada** con su índice y su código; el resultado trae además la estructura del texto | `TC-05`, `TC-06`, `TC-07`, `TC-08`, `TC-09`, `TC-10` | Unit, integración y extremo a extremo | `Pendiente` |
| CU-03 Seleccionar una pieza por su índice | Given una escena dibujada, When se selecciona un índice, Then esa pieza queda resaltada **en exclusiva**; con un índice que no corresponde a ninguna pieza dibujada, Then se informa la condición y la selección vigente se conserva | `TC-11` | Extremo a extremo | `Pendiente` |
| CU-04 Redimensionar la escena | Given un cambio de tamaño del elemento, When se invoca el ajuste, Then se recalcula la relación de aspecto; con el elemento sin tamaño, Then se informa la condición y **la instancia sigue viva** | `TC-12` | Extremo a extremo | `Pendiente` |
| CU-05 Destruir la instancia y liberar recursos | Given una instancia viva, When se la destruye, Then libera sus recursos y **corta su bucle**; el identificador queda invalidado | `TC-04` | Extremo a extremo | `Pendiente` |
| CU-06 Ejercitar la fachada sin backend | Given la página integradora y un texto pegado a mano, When se recorren las **seis** funciones con **0 servicios del backend disponibles**, Then el recorrido cierra entero y las **seis** propiedades transversales se verifican juntas | `TC-15`, `TC-16`, `TC-17` | Extremo a extremo | `Pendiente` |
| CU-07 Gobernar el movimiento automático | Given una instancia viva, When se prende o se apaga un movimiento, Then el cambio surte efecto **sin reconstruir la instancia** y sin perder la selección; el no nombrado conserva su estado | `TC-13`, `TC-14`, `TC-03` | Extremo a extremo | `Pendiente` |

**Siete de siete casos de uso con al menos un caso de prueba.** Ninguno queda huérfano.

**Veinte de los veintiún `TC-XX` tienen fila en alguna de las cinco tablas de trazabilidad de esta matriz.** El restante es `TC-20`, cuya trazabilidad primaria es una **puerta técnica** —y no un `CU-XX`, un NFR, una `RN-XX`, una garantía ni un código de condición—, y por eso no aparece en ninguna de ellas. Está en §2.1, y **no queda sin instrumento de trazabilidad**. Es el caso más sensible de la matriz, porque **es la prueba de `PT-02`** y una puerta que no pasa detiene la planificación de la etapa `g`.

### 2.1 El caso de prueba de la puerta técnica `PT-02`

| Caso de prueba | Qué verifica | A qué traza, según su campo «Cubre» | Estado |
| --- | --- | --- | --- |
| `TC-20` La puerta `PT-02`: el bundle en una página del anfitrión | Los **cinco** tramos que la puerta exige, medidos juntos: el bundle carga, la creación de instancia arma la escena, el texto de `E-1` dibuja las **tres** figuras con el ortoedro, **diez** recorridos de ida y vuelta con los dos movimientos prendidos **no degradan**, y el árbol y la escena **se sincronizan por índice** | Puerta técnica **`PT-02`** del intake §15 y §17.7.P.8; `US-01`, `US-04`, `US-09`, `US-11`; `QG-03`; `BT-14` | `Pendiente` |

**Dónde están sus criterios de validación.** Los tramos se cuentan en [`Criterios-Validacion.md`](Criterios-Validacion.md) §4, que es la sección de puertas técnicas que este proyecto de código tiene y los otros dos de nivel topológico 0 no: **cuatro** criterios, `CV-20` a `CV-23`, que reparten los cinco tramos —`CV-20` toma juntos la carga del bundle y la creación de la escena—. **`TC-20` no está fuera de la trazabilidad; está en el instrumento que le corresponde**, y esta subsección lo enlaza desde la matriz para que el recorrido inverso `TC → matriz` cierre.

## 3. Trazabilidad NFR ↔ tests

Ocho filas, una por cada NFR de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §8. Los seis primeros son las **seis propiedades transversales** de `02` §6, con las condiciones de medición que esa sección declara como lugar único.

| NFR | Objetivo numérico | Condición de medición | Test | Estado |
| --- | --- | --- | --- | --- |
| Cero red | Exactamente **0** peticiones originadas por el archivo de guion | **Con los dos movimientos prendidos y sostenidos**, y durante los gestos de rotar y acercar | `TC-16`, `TC-18` | `Pendiente` |
| Cero persistencia | **0** claves escritas en el almacenamiento del navegador, y ningún estado conservado entre páginas | Cualquier estado de los movimientos; se comprueba además que recargar no repone la preferencia | `TC-17`, `TC-03` | `Pendiente` |
| Se ejercita sin backend | Recorrido completo de las **seis** funciones con **0 servicios del backend disponibles** | Sin condición adicional | `TC-15` | `Pendiente` |
| Disposición determinista | Dos procesados del mismo texto producen la **misma disposición**, comparable pieza por pieza | **Se compara posición, no orientación**; vale con cualquier estado de los movimientos | `TC-09` | `Pendiente` |
| Liberación de recursos | **10 recorridos** de ida y vuelta sin degradación | **Con los dos movimientos prendidos** durante los recorridos | `TC-04` | `Pendiente` |
| Ausencia de fallo silencioso | **100 %** de las piezas no dibujadas enumeradas con índice y código, y **0** sin registro | Sin condición adicional | `TC-07` | `Pendiente` |
| Dependencias traídas de una red de distribución externa en tiempo de ejecución | Exactamente **0** | Página abierta sin acceso a redes externas; puerta `PT-03` | `TC-19` | `Pendiente` |
| Superficie pública del bundle | Exactamente **6** funciones, bajo **1** nombre propio en el objeto global y **0** identificadores globales sueltos | Inspección del **bundle generado**, no sólo de la fuente | `TC-18` | `Pendiente` |

**Ocho de ocho NFR con caso de prueba.**

**Las condiciones de medición son vinculantes y no se redefinen acá.** `02` §6 es su lugar único; esta tabla las transcribe. Una medición hecha sin su condición no cuenta como medición: mediría el caso fácil.

**No hay NFR de latencia con umbral numérico**, y esta categoría **no lo inventa**. `05` §8 declara que la fuente no fija un valor de fluidez y lo deja como punto abierto `PA-03`; ver §8.

## 4. Trazabilidad RN ↔ tests

**Este proyecto de código no tiene reglas de dominio**, y no es una omisión: es un visualizador puro y las reglas del trabajo del alumno las decide el backend (`02` §5.2; intake §14 `RA-02`, §17.7.P.5 y P.11 punto 4). Lo que tiene son **condiciones de contrato**, que están en §6 de esta matriz.

La tabla se emite igual, con las **dieciséis** reglas del producto, para declarar de forma verificable **que ninguna se verifica acá y dónde se verifica cada una**. Dieciséis filas, ninguna agrupada.

| RN | ¿La verifica este proyecto de código? | Dónde se verifica |
| --- | --- | --- |
| RN-01 Administrador único y papeles fijos | No. La fachada no sabe quién mira ni qué papel cumple | `GeometriaFactory-Domain` |
| RN-02 El correo del alumno es único | No | `GeometriaFactory-Domain` y `GeometriaFactory-Infrastructure` |
| RN-03 Trabajo ajeno indistinguible de inexistente | No. La fachada dibuja el mismo trabajo sin saber de quién es | `GeometriaFactory-Domain` |
| RN-04 Eliminación acotada al borrador | No | `GeometriaFactory-Domain` |
| RN-05 Sin errores de validación no hay estado `Pendiente` | No, **y es la frontera que más se confunde**: el visor informa por qué no dibujó una pieza, y **decidir si el trabajo pasa a `Pendiente` es del validador** (intake §20.E-8 punto 4) | `GeometriaFactory-Domain` y `GeometriaFactory-Infrastructure` |
| RN-06 Cuenta `Pendiente` o `Bloqueado` sin acceso | No. La fachada no participa de ninguna decisión de autorización | `GeometriaFactory-Domain` |
| RN-07 Baja con arrastre y confirmación escrita | No | `GeometriaFactory-Domain` |
| RN-08 Texto original conservado íntegro | No. La fachada **no conserva ni reescribe** el texto: lo recibe, lo lee y no lo persiste | `GeometriaFactory-Domain` |
| RN-09 Observación de error con posición y campo | No. La fachada **no emite observaciones**, ni advertencias ni errores de validación. Lo que sí emite es la **enumeración de piezas no dibujadas con su índice y su código**, que es otra cosa y se verifica en `TC-07` | `GeometriaFactory-Domain` y `GeometriaFactory-Contracts` |
| RN-10 Desenlace exclusivo y terminal | No | `GeometriaFactory-Domain` |
| RN-11 El administrador no ve los borradores | No | `GeometriaFactory-Domain` |
| RN-12 El reseteo conserva la cuenta y sus trabajos | No | `GeometriaFactory-Domain` |
| RN-13 Cambio forzado antes de toda otra capacidad | No | `GeometriaFactory-Domain` |
| RN-14 La provisoria la produce el sistema | No | `GeometriaFactory-Infrastructure` |
| RN-15 Resetear no exige cuenta habilitada | No | `GeometriaFactory-Domain` |
| RN-16 Habilitar produce la provisoria | No | `GeometriaFactory-Domain` |

**Cero de dieciséis, y es el resultado correcto.** Un caso de prueba de este proyecto de código que verificara una regla de negocio sería un defecto de titularidad: le atribuiría a un visualizador puro una decisión que `RA-02` le prohíbe tomar.

## 5. Trazabilidad garantía ↔ tests

Siete filas, `G-1` a `G-7`, las de [`../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md) §3.2. Ninguna se agrupa.

| Garantía | Enunciado, en una línea | Componente que la sostiene (`05` §10.2) | Test | Estado |
| --- | --- | --- | --- | --- |
| G-1 · Cero red | Ninguna función ni ningún movimiento origina una petición | Todos, por ausencia; se verifica sobre el bundle entero | `TC-16`, `TC-18`, `TC-01`, `TC-15` | `Pendiente` |
| G-2 · Cero persistencia | Ninguna función escribe en el almacenamiento del navegador | Todos, por ausencia | `TC-17`, `TC-03` | `Pendiente` |
| G-3 · Sin configuración propia | Todo lo que la instancia necesita llega por parámetro | Fachada plana | `TC-03`, `TC-01` | `Pendiente` |
| G-4 · Aislamiento entre instancias | Dos instancias vivas no comparten escena, ni selección, ni disposición | Registro de instancias, servicio de dibujo | `TC-01`, `TC-04`, `TC-11` | `Pendiente` |
| G-5 · Sin fallo silencioso | Toda pieza no dibujada queda enumerada con su índice | Lector del texto, servicio de dibujo | `TC-07` | `Pendiente` |
| G-6 · Determinismo | La misma entrada produce la misma **posición** de cada pieza, no la misma orientación | Servicio de dibujo | `TC-09` | `Pendiente` |
| G-7 · Terminación controlada | O la operación surte efecto completo, o la instancia queda como estaba | Fachada plana | `TC-02`, `TC-10`, `TC-11`, `TC-12`, `TC-13`, `TC-04` | `Pendiente` |

**Siete de siete garantías con caso de prueba.** Perder cualquiera es **cambio mayor** aunque las seis firmas no se toquen, y por eso esta tabla existe: sin ella, la verificación de un cambio se limitaría a comprobar que las firmas siguen ahí.

## 6. Trazabilidad código de condición ↔ tests

Ocho filas: **siete códigos**, uno de ellos con **dos cursos**, que es la forma en que §6 del contrato de fachada los declara. **Un curso no es un código.**

| Código | Curso | Test | Entrada de `03` | Estado |
| --- | --- | --- | --- | --- |
| `CAPACIDAD_GRAFICA_AUSENTE` | Único | `TC-02` | `E-VIS-01` | `Pendiente` |
| `ELEMENTO_DE_DIBUJO_INVALIDO` | **C-1, en creación** | `TC-02` | `E-VIS-02` | `Pendiente` |
| `ELEMENTO_DE_DIBUJO_INVALIDO` | **C-2, en ajuste** | `TC-12` | `E-VIS-07` | `Pendiente` |
| `INSTANCIA_DESCONOCIDA` | Único, **en cinco funciones** | `TC-04`, `TC-11`, `TC-13` | `E-VIS-03` a `E-VIS-06` y `E-VIS-13` | `Pendiente` |
| `TEXTO_NO_LEGIBLE` | Único | `TC-10` | `E-VIS-08` | `Pendiente` |
| `TIPO_NO_DIBUJABLE` | Único, por pieza | `TC-07` | `E-VIS-09` | `Pendiente` |
| `DIMENSION_NO_LEGIBLE` | Único, por pieza | `TC-07` | `E-VIS-10` | `Pendiente` |
| `INDICE_FUERA_DE_RANGO` | Único, con **dos casos** | `TC-11` | `E-VIS-11`, `E-VIS-12` | `Pendiente` |

**Siete de siete códigos cubiertos, en ocho filas de curso.** El catálogo de `03` los desarrolla en **trece** entradas porque su unidad de catalogación es la **función** y no el código; esta matriz sigue la unidad del contrato, que es la condición. `TC-21` verifica que las dos cifras no se confundan y que **ningún código se acuñe aguas abajo**.

## 7. Cobertura por capa

La partición es por los **seis** componentes de `05` §3.1, dos de los cuales no son de este proyecto de código.

| Componente | Capa | Métrica declarada | Medición | Umbral |
| --- | --- | --- | --- | --- |
| Componente anfitrión | 1, **fuera de este proyecto de código** | — | — | No aplica: su cobertura es de la categoría 08 de `GeometriaFactory-Web` |
| Fachada plana | 2 | Funciones ejercitadas | Sin medir | **6 de 6** |
| Registro de instancias | 2 | Cursos del ciclo de vida del identificador | Sin medir | 100 %: válido, ya liberado, inexistente |
| Lector del texto | 3 | Tipos dibujables y variantes de clave | Sin medir | **6 de 6** tipos; `Tapas` y `Bases` como sinónimos; **el cero como dimensión legible** |
| Servicio de dibujo | 3 | Garantías que lo alcanzan | Sin medir | `G-5`, `G-6`, disposición determinista y liberación de recursos |
| Motor de dibujo tridimensional | 3, **empaquetado** | — | — | **No se prueba por dentro**, y es deliberado ([`ADR-04`](../05-Arquitectura-Tecnica/Adrs/ADR-04-Motor-De-Dibujo-Empaquetado-Y-Aislado.md)) |
| **Bundle generado** | — | Recuentos de superficie y de ausencia | Sin medir | **6**, **1**, **0**, **0**, **0**, **0** |

**«Sin medir» y no «0 %».** No hay bundle construido.

**No hay columna de líneas, de ramas ni de mutation score**, y las tres ausencias están declaradas con su motivo en [`Estrategia-Testing.md`](Estrategia-Testing.md) §2: el intake pone un gate de inspección **en lugar de** la cobertura de líneas, y mutar código de dibujo produciría mutantes que sólo una comparación de imágenes podría matar, técnica que §1 de ese documento descarta con su fundamento.

## 8. Huecos identificados

| Hueco | Consecuencia | Plan de remediación |
| --- | --- | --- |
| **El umbral numérico de fluidez no existe** (`05` §11 `PA-03`, `BT-18`) | La interacción fluida se verifica de forma **cualitativa declarada** junto con `PT-02`, y no con un número | `BT-18`, antes de cerrar la etapa `g`: o el Product Owner fija un umbral, o esta categoría fija su guion de medición cualitativo. **Ninguna de las dos salidas es inventar un número**, y `05` §8 se niega explícitamente a hacerlo |
| **La versión del motor de dibujo no está anclada** (`05` §11 `PA-01`, `BT-09`) | Si la versión que se adopte exige una interfaz distinta de la del visualizador previo, la capa 3 se rehace y varios casos de prueba se reescriben | `BT-09`, antes de comprometer la etapa `g`, que es cuando se miden `PT-02` y `PT-03` |
| **La versión mínima de navegador no está fijada** (`05` §11 `PA-04`) | El requisito se declara **por capacidad** y no por versión, de modo que `TC-02` verifica la ausencia de capacidad gráfica y no una versión | El Product Owner sobre su propio documento, sin fecha comprometida. **No es bloqueante** |
| **No hay filas `VER-XX` en la matriz de sensado de deriva** | Las sondas de contrato y comportamiento que la categoría 10 aporta todavía no existen | `10-Examples` de este proyecto de código, que desarrollará el sample **S-1**. Al emitirse, [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) suma una fila `VER-XX` por contrato de verificación |
| **Las pruebas de extremo a extremo exigen un navegador con capacidad gráfica en el entorno de ejecución** | Un entorno de integración continua sin esa capacidad no puede medir `PT-02`, `TC-16` ni `TC-17` | Es una condición del ambiente, declarada en [`Estrategia-Testing.md`](Estrategia-Testing.md) §7. Su provisión concreta pertenece a `09-Devops` |

## 9. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **`H-04`.** `TC-20` estaba definido en el catálogo y **no tenía fila en ninguna de las cinco tablas** de esta matriz, siendo **la prueba de la puerta `PT-02`**. Se agrega **§2.1**, que lo enumera con sus cinco tramos, su trazabilidad y la remisión a los criterios `CV-20` a `CV-23` de [`Criterios-Validacion.md`](Criterios-Validacion.md) §4, para que el recorrido inverso `TC → matriz` cierre. **Ninguna cobertura, umbral ni caso cambia.** Corrige contra [`../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md`](../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md) 1.0 y contra el texto vivo del intake **1.20**. |
| 1.0 | 2026-08-11 | Emisión inicial. Declara las tres tablas obligatorias —**siete** filas de caso de uso, **ocho** de NFR y **dieciséis** de regla de negocio— y dos más: las **siete** garantías del contrato de fachada y los **siete** códigos de condición en sus **ocho** filas de curso. La tabla de reglas declara de forma verificable que **ninguna de las dieciséis se verifica acá y dónde se verifica cada una**, que es el resultado correcto para un visualizador puro. Transcribe las condiciones de medición de `02` §6 sin redefinirlas y las declara vinculantes. Declara la cobertura por los **seis** componentes con «Sin medir», las tres ausencias de métrica de código con su motivo, y **cinco** huecos, incluido el umbral de fluidez que esta categoría **no inventa**. |
