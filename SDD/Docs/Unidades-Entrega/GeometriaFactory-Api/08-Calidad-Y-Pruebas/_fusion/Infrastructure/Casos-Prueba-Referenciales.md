# Casos de prueba referenciales — GeometriaFactory-Infrastructure

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** Casos-Prueba-Referenciales.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** los **diez** casos de uso de [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/); las **siete** reglas conceptuales de modelo de [`../02-Especificacion-Funcional/Modelo-Datos/reglas-conceptuales-de-modelo/`](../02-Especificacion-Funcional/Modelo-Datos/reglas-conceptuales-de-modelo/); los **diez** casos de la batería de [`../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md`](../../../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md) §7 y de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/_fusion/Infrastructure/Arquitectura-Proyecto-Codigo.md) §10.5; las **veinticinco** historias de [`../06-Backlog-Tecnico/historias-usuario/`](../06-Backlog-Tecnico/historias-usuario/); las **17** condiciones de [`../03-UX-UI-DX/DX-Error-Messages.md`](../../../03-UX-UI-DX/_fusion/Infrastructure/DX-Error-Messages.md) §3; los **catorce** NFR de `05` §8; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.19** §20 y §21
**Trazabilidad downstream:** [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md), [`Criterios-Validacion.md`](Criterios-Validacion.md), [`Plan-Pruebas.md`](Plan-Pruebas.md)

---

## Tabla de contenido

- [1. Cómo se lee este catálogo](#1-cómo-se-lee-este-catálogo)
- [2. Catálogo de casos de prueba](#2-catálogo-de-casos-de-prueba)
  - [2.1 La batería del validador: los diez casos](#21-la-batería-del-validador-los-diez-casos)
  - [2.2 Cobertura adicional del validador](#22-cobertura-adicional-del-validador)
  - [2.3 Almacén: trabajos y cuentas](#23-almacén-trabajos-y-cuentas)
  - [2.4 Mecanismos de seguridad](#24-mecanismos-de-seguridad)
  - [2.5 Arranque y preparación del almacén](#25-arranque-y-preparación-del-almacén)
  - [2.6 Pruebas de inspección estructural](#26-pruebas-de-inspección-estructural)
- [3. Recuento y verificación](#3-recuento-y-verificación)
- [4. Control de cambios](#4-control-de-cambios)

---

## 1. Cómo se lee este catálogo

Cada `TC-XX` declara ocho campos, según `Rules-Calidad-Y-Pruebas.md` §4.6: identificador y nombre, tipo, upstream cubierto, setup, pasos en Given-When-Then, salida esperada, salida observada y estado.

**Todas las filas de «Salida observada» dicen «Sin ejecutar» y todos los estados dicen `Pendiente`.** No hay sistema construido: el proyecto de código arranca en la etapa `a` y este catálogo se emite antes.

**Vocabulario de este catálogo**, definido acá la primera vez que aparece y no redefinido después:

- **Nivel**: la posición de una prueba en la pirámide de [`Estrategia-Testing.md`](Estrategia-Testing.md) §1 — unitario o integración interna.
- **Integración interna**: la prueba que necesita un **almacén efímero**, creado y descartado por ella misma. No es la batería de integración del producto, que es de `GeometriaFactory-Api`.
- **Fixture**: uno de los cuatro constructores compartidos de [`Estrategia-Testing.md`](Estrategia-Testing.md) §5, incluidos los **ocho textos literales** de los escenarios del intake §20.
- **Prueba de inspección**: la que comprueba una propiedad estructural del proyecto de código y no un contrato.
- **Los diez casos de la batería**: los que [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/_fusion/Infrastructure/Arquitectura-Proyecto-Codigo.md) §10.5 enumera, con su origen en el intake §21.

**Los diez primeros casos de este catálogo son, uno a uno, los diez de la batería.** No se agruparon ni se reordenaron: la correspondencia con la tabla de `05` §10.5 es de identidad, y así se puede recorrer sin traducir.

## 2. Catálogo de casos de prueba

### 2.1 La batería del validador: los diez casos

#### TC-06001 — Ortoedro-Con-Clave-Sinonima

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | Caso **1** de la batería (`T1`); `CU-06001`; `RN-06009`; `US-06001`; paso `P-3` del flujo |
| Setup | Fixture con el **texto literal** del escenario `E-2` del intake §20 |
| Pasos | Given el texto de `E-2`, cuyo ortoedro declara sus bases con **la clave que el programa del alumno emite**, When se lo interpreta, Then las bases **se leen** y la pieza se reconstruye con **2 bases y 4 laterales**. Given el mismo texto con la clave equivalente, Then el resultado es **idéntico**: las dos se aceptan como sinónimas |
| Salida esperada | Una pieza reconstruida con sus seis componentes, por cualquiera de las dos claves. **Con un validador ingenuo, acá es donde falla** (§20.E-2, punto 3) |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06002 — Texto-Con-Comas-Finales

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | Caso **2** de la batería (`T2`); `CU-06001`; `US-06001`; paso `P-2` |
| Setup | Fixture con el texto literal de `E-2`, **con sus dos comas finales** |
| Pasos | Given el texto tal como el programa lo emite, **con comas finales**, When se lo lee, Then **el parseo tiene éxito**. Given un texto con comentarios, Then también, por la misma tolerancia. Given un texto que no parsea **ni con tolerancia**, Then se emite una observación de validación y **no** `INTERPRETACION_NO_DISPONIBLE` |
| Salida esperada | Dos lecturas exitosas y un rechazo con el código correcto. La distinción entre **texto ilegible** y **motor no disponible** es la que `TC-06013` desarrolla |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06003 — Cubo-Con-Caras-Cuadrado

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | Caso **3** de la batería (`T3`); `CU-06001`; `US-06001`; paso `P-4` |
| Setup | Fixture con el texto literal de `E-3` |
| Pasos | Given el cubo de `E-3`, cuyas caras declaran el tipo que emite el primer ejemplo de la cátedra, When se lo interpreta, Then las caras **se interpretan** y el campo que se usa para dibujar es el largo |
| Salida esperada | Seis caras interpretadas, con la lectura del largo. Es la mitad de `T3` que viene del primer ejemplo; la otra mitad es `TC-06004` |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06004 — Cubo-Con-Caras-Rectangulo

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | Caso **4** de la batería (`T3`); `CU-06001`; `US-06001`; paso `P-4` |
| Setup | Fixture con el texto literal de `E-4` |
| Pasos | Given el mismo cubo de lado 3, emitido por el **otro** ejemplo de la cátedra, cuyas caras declaran el otro tipo, When se lo interpreta, Then las caras **se interpretan igual que las de `TC-06003`**: las dos traen el largo, que es lo que se usa |
| Salida esperada | Resultado equivalente al de `TC-06003` en cuanto a la reconstrucción. **El contraste entre los dos ejemplos es lo que hace visible el defecto** (§20.E-4, contexto) |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06005 — Area-Del-Cubo-Declarada-Contra-Derivada

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | Caso **5** de la batería; `CU-06002`; `RN-06009`; `US-06005`, `US-06007`; paso `P-6` |
| Setup | Fixture con el texto literal de `E-3`, ya interpretado por `TC-06003` |
| Pasos | Given el cubo de `E-3` con su área declarada **36.00**, When se deriva el área desde sus componentes, Then el valor derivado es **54.00** y se emite **una advertencia** que expresa **los dos valores**, no un texto genérico. Then el volumen declarado **27.00** coincide con el derivado y **no produce observación**. Then **el valor del alumno no se corrige** y el trabajo **no se rechaza** |
| Salida esperada | Una advertencia con su par de valores y una comparación sin observación. Es «el caso incómodo por excelencia»: el dato erróneo es un dato **correctamente emitido** por el programa del alumno |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06006 — Volumen-Del-Ortoedro-Declarado-Contra-Derivado

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | Caso **6** de la batería; `CU-06002`; `RN-06009`; `US-06005`, `US-06007`; paso `P-6` |
| Setup | Fixtures con los textos literales de `E-2` y de `E-1` |
| Pasos | Given el ortoedro de `E-2` con volumen declarado **343.00**, When se deriva, Then el derivado es **1029.00** y se emite **una advertencia**, **no un error**. Then su **área** derivada coincide con la declarada y **no** produce observación. Given el mismo ortoedro dentro de `E-1`, Then el resultado es el mismo |
| Salida esperada | Una advertencia de volumen y ninguna de área, en los dos escenarios. La advertencia **permite el paso a estado `Pendiente`**: esa decisión es del dominio y no de acá |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06007 — Dimension-En-Cero-Que-No-Descarta-La-Figura

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | Caso **7** de la batería; `CU-06001`, `CU-06002`; `US-06003`; pasos `P-4` y `P-6` |
| Setup | Fixture con el texto literal de `E-6` |
| Pasos | Given la figura de `E-6` con una dimensión en **0.00**, When se la interpreta, Then **se interpreta y no se descarta**: la comprobación es de **existencia del campo, no de veracidad de su valor**. Then se produce **a lo sumo una advertencia** por el valor derivado, y **nunca un error de interpretación** |
| Salida esperada | Una figura reconstruida y a lo sumo una advertencia. **Descartarla sería aplicar un juicio que ninguna regla pidió** y dejaría al alumno sin ver su propio error (§20.E-6, «Qué ejercita») |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06008 — Tipo-Desconocido-Con-Posicion-Y-Campo

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | Caso **8** de la batería; `CU-06001`; `RN-06009`; `US-06004`; paso `P-3` |
| Setup | Fixture con el texto literal de `E-5` |
| Pasos | Given el texto de `E-5`, When se lo interpreta, Then se produce una observación de severidad **`Error`**, no de advertencia, con **índice de figura 1** y **campo `Tipo`**, y **nunca un texto genérico**. Then **la primera pieza, que es válida, se interpreta igual**: un error en un elemento **no descarta el resto del análisis**. Given un elemento sin el campo de tipo, un conjunto raíz vacío y un texto que no parsea ni con tolerancia, Then los tres producen **el mismo tratamiento de error** |
| Salida esperada | Un error localizado, la pieza válida reconstruida y tres tratamientos equivalentes. Es el tramo principal de `RN-06009` |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06009 — Texto-Semilla-Completo

| Campo | Valor |
| --- | --- |
| Tipo | Unit, **y medición de la tolerancia** |
| Cubre | Caso **9** de la batería; `CU-06001`, `CU-06002`; `US-06002`, `US-06006`; NFR de tolerancia; pasos `P-1` a `P-7` |
| Setup | Fixture con el texto literal de `E-1` |
| Pasos | Given el texto semilla de `E-1`, When se lo interpreta y se verifican sus valores, Then se reconstruyen **3 piezas** con índices 0, 1 y 2, y se emiten **exactamente 2 advertencias**. Then **el cilindro no produce ninguna observación**: su área declarada 113.10 contra la suma de componentes 113.09 da una diferencia de **exactamente 0.01**, y con el operador **estricto** eso **no** produce advertencia. Then **ninguna observación es de severidad `Error`** |
| Salida esperada | 3 piezas y **2** advertencias. **Una tercera advertencia significa que el operador de tolerancia dejó de ser estricto**, y el caso de prueba canónico del producto falla. El intake §17.1.P.10 · GeometriaFactory-Infrastructure declara este número con su fundamento y **lo excluye expresamente de las asunciones** |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06010 — Dimension-No-Legible

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | Caso **10** de la batería; `CU-06001`; `RN-06009`; `US-06004`; paso `P-4` |
| Setup | Fixture con el texto literal de `E-8` |
| Pasos | Given el texto de `E-8`, cuya dimensión viene escrita con el separador decimal de la configuración regional del alumno y por eso **deja de ser un número**, When se lo interpreta, Then se produce un **error de validación** con **índice de figura** y **campo**, y **el código es el de dimensión no legible y no el de texto inválido**: el texto es sintácticamente válido y lo que falla es la lectura de un valor. Then la otra pieza **se interpreta igual** |
| Salida esperada | Un error localizado con el código correcto. **Confundir los dos códigos es el error que este escenario detecta** (§20.E-8, punto 3), y es el modo de falla **más probable** de todos los escenarios porque lo produce la máquina del alumno y no su programación |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.2 Cobertura adicional del validador

#### TC-06011 — Los-Seis-Tipos-Reconstruibles

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-06001`; `US-06003`; **cobertura adicional declarada** de `05` §10.5 |
| Setup | Fixture con el texto literal de `E-7` |
| Pasos | Given el texto de `E-7`, When se lo interpreta, Then se reconstruyen **seis** piezas, una por cada tipo reconstruible, con las figuras planas **como piezas del conjunto raíz** y no sólo como componentes. Then el ortoedro se lee por su clave alternativa, igual que en `TC-06001` |
| Salida esperada | Seis piezas de seis tipos distintos. **`E-7` no respalda ninguno de los diez casos de la batería y se usa igual**, porque es el único texto que ejercita el mapeo completo: así lo declara `05` §10.5 |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06012 — La-Cantidad-De-Figuras-Del-Conjunto-Raiz-Y-La-Posicion-Reservada

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-06001`; `RN-06009`; `US-06002`, `US-06003`; `RC-06002` |
| Setup | Fixtures con los textos literales de `E-5` y de `E-1` |
| Pasos | Given el texto de `E-5`, donde una figura **no se reconstruye**, When se lo interpreta, Then la **cantidad de figuras del conjunto raíz** que se devuelve **incluye la no reconstruida**, y **la posición de esa figura queda reservada**: la siguiente pieza **no se renumera**. Given `E-1`, Then la cantidad coincide con las tres piezas reconstruidas. Given un conjunto en el que la cantidad y las posiciones no cierran, Then `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO` |
| Salida esperada | La cantidad que incluye lo no reconstruido, la posición reservada y un rechazo. Es lo que hace que el índice de una observación tenga un rango contra el que validarse |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06013 — Texto-Ilegible-No-Es-Motor-No-Disponible

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-06001`; `US-06001`; el segundo riesgo de `05` §9 |
| Setup | Un texto que no parsea ni con tolerancia; el texto ausente; y el motor forzado a no estar disponible |
| Pasos | Given un texto ilegible, When se lo interpreta, Then se emite **una observación de validación** y **no** `INTERPRETACION_NO_DISPONIBLE`. Given el texto ausente, Then `TEXTO_ORIGINAL_AUSENTE`. Given el motor efectivamente no disponible, Then **sí** `INTERPRETACION_NO_DISPONIBLE` |
| Salida esperada | Tres resultados distintos y **ninguna confusión entre resultado y fallo**. `05` §9 le asigna probabilidad **alta**: es la garantía que más veces se rompe al implementar, porque el alumno vería «el servicio no está disponible» y esperaría a que se recupere de un problema que no tiene |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06014 — Cero-Peticiones-De-Red-De-Los-Dos-Motores

| Campo | Valor |
| --- | --- |
| Tipo | Unit, **prueba de inspección** |
| Cubre | NFR de peticiones de red (`05` §8); `QG-08`; `CU-06001` CA-11 |
| Setup | El árbol de dependencias de los dos motores |
| Pasos | Given los dos motores, When se inspeccionan sus dependencias y se ejecuta la batería completa con el acceso a red observado, Then el recuento de peticiones originadas por ellos es exactamente **0**. Then **ninguno abre el almacén**: reciben texto y devuelven observaciones |
| Salida esperada | Dos recuentos en cero. Es lo que el intake §17.1.P.3 · GeometriaFactory-Infrastructure declara —«el validador de figuras no hace red»— y lo que hace que la interpretación se pueda medir **sin almacén** |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06015 — Tiempo-De-Interpretacion-Del-Texto-Semilla

| Campo | Valor |
| --- | --- |
| Tipo | Unit, **medición** |
| Cubre | NFR de tiempo de interpretación (`05` §8); `QG-14`; `US-06001` |
| Setup | Fixture con el texto literal de `E-1`; medición **sin almacén** |
| Pasos | Given el texto de **3** piezas de `E-1`, When se lo interpreta y se verifican sus valores, Then el tiempo total es menor a **200 ms**, medido **sin abrir el almacén**, que es la condición que el intake §17.1.P.10 · GeometriaFactory-Infrastructure declara |
| Salida esperada | Una medición registrada. El umbral viene rotulado **[ASUNCIÓN del intake §22, asunción `A-5`]** y su gate es **condicionado**: se mide y se registra, y no bloquea hasta la confirmación del Product Owner |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.3 Almacén: trabajos y cuentas

#### TC-06016 — Texto-Original-Literal-Y-Escritura-Que-Lo-Reemplaza-Rechazada

| Campo | Valor |
| --- | --- |
| Tipo | Integración interna |
| Cubre | `CU-06003`; `RN-06008`; `RC-06001`; `US-06008`; NFR de escrituras que reemplazan el texto |
| Setup | Almacén efímero preparado; fixture con el texto literal de `E-2`, **con sus comas finales** |
| Pasos | Given el texto de `E-2`, When se materializa el trabajo y se lo recupera, Then el texto guardado es **idéntico carácter por carácter** al original, con sus comas finales intactas. When se intenta materializar el **mismo trabajo** con un texto distinto, Then se rechaza con `ESCRITURA_QUE_REESCRIBE_EL_TEXTO_ORIGINAL` y **el texto guardado no cambia** |
| Salida esperada | Comparación byte a byte sin diferencias y un rechazo sin efecto. Es el **tramo principal de `RN-06008`**: ésta es la capa donde el texto se escribe, y por lo tanto donde puede perderse |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06017 — Materializacion-Del-Trabajo-En-Una-Unidad-De-Trabajo

| Campo | Valor |
| --- | --- |
| Tipo | Integración interna |
| Cubre | `CU-06003`; `US-06009` |
| Setup | Almacén efímero; fixture de trabajo con piezas, componentes y observaciones |
| Pasos | Given un trabajo con sus piezas, sus componentes y sus observaciones, When se lo materializa, Then las cuatro cosas quedan en **una sola** unidad de trabajo. When el almacén se interrumpe a mitad, Then **no queda nada escrito**. Given una escritura concurrente sobre el mismo trabajo, Then `ESCRITURA_CONCURRENTE_RECHAZADA`. Given el almacén no disponible, Then `ALMACEN_NO_DISPONIBLE` |
| Salida esperada | Una materialización completa, una interrupción sin efecto parcial y dos rechazos con su código |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06018 — Consulta-Con-El-Recorte-Ya-Trasladado-Y-Sin-Recorte-No-Resuelta

| Campo | Valor |
| --- | --- |
| Tipo | Integración interna |
| Cubre | `CU-06003`; `RN-06003`, `RN-06011`; `US-06010` |
| Setup | Almacén efímero con trabajos de dos alumnos en los cuatro estados |
| Pasos | Given una consulta con el recorte por dueño **ya declarado**, When se la resuelve, Then devuelve sólo lo que ese recorte admite. Given una consulta con el predicado de alcance del administrador declarado, Then **ningún borrador viaja**. Given una consulta **sin recorte declarado**, Then `CONSULTA_SIN_ALCANCE_DECLARADO` y **no se resuelve** |
| Salida esperada | Dos consultas acotadas y un rechazo. **Esta capa no comprueba pertenencia**: lo que hace es **no ofrecer el camino** por el que `RN-06003` y `RN-06011` se romperían |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06019 — Listado-Sin-Componentes-Y-Sin-Texto-Original

| Campo | Valor |
| --- | --- |
| Tipo | Integración interna |
| Cubre | `CU-06003`; `US-06011`; NFR de componentes en listados; `QG-10` |
| Setup | Almacén efímero con un trabajo con seis piezas y sus componentes, materializado desde `E-7` |
| Pasos | Given la **proyección de listado**, When se la resuelve, Then la colección de componentes **no viene materializada** y el **texto original no aparece** en el resultado. Given el **detalle**, Then las dos cosas **sí** vienen |
| Salida esperada | Dos recuentos en cero para el listado y presencia completa en el detalle. `05` §9 le asigna probabilidad **media-alta**: es el comportamiento por defecto de cualquier carga completa de entidad |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06020 — Retiro-Fisico-Todo-O-Nada

| Campo | Valor |
| --- | --- |
| Tipo | Integración interna |
| Cubre | `CU-06004`; `RN-06004`; `RC-06005`; `US-06012` |
| Setup | Almacén efímero con un trabajo con piezas, componentes y observaciones |
| Pasos | Given un trabajo con todo lo que cuelga de él, When se lo retira, Then **se retira físicamente**, sin marca lógica, con sus piezas, sus componentes y sus observaciones. When se consulta después, Then no existe. Given un retiro que sólo alcanzaría a parte de lo que cuelga, Then `RETIRO_PARCIAL_NO_ADMITIDO` |
| Salida esperada | Un retiro completo y un rechazo. **No hay borrado lógico**: es la única operación destructiva del producto y se ejerce entera |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06021 — Arrastre-De-La-Baja-Interrumpido-Sin-Retiro-Parcial

| Campo | Valor |
| --- | --- |
| Tipo | Integración interna, **con el almacén interrumpido a mitad de operación** |
| Cubre | `CU-06004`; `RN-06007`; `RC-06005`; `US-06013`; NFR de retiros parciales; `QG-11` |
| Setup | Almacén efímero con una cuenta y **cuatro** trabajos suyos en los cuatro estados |
| Pasos | Given la cuenta con sus cuatro trabajos, When se ejecuta el arrastre de la baja, Then **la cuenta y los cuatro trabajos** se retiran en la misma unidad. When el almacén **se interrumpe a mitad de la operación**, Then **no se retira nada**: la cuenta sigue y sus cuatro trabajos también |
| Salida esperada | Un arrastre completo y una interrupción con **0** retiros parciales. Es el mecanismo de medición que `05` §8 declara para ese NFR |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06022 — Unicidad-Del-Correo-Y-Del-Administrador-En-El-Almacen

| Campo | Valor |
| --- | --- |
| Tipo | Integración interna |
| Cubre | `CU-06005`; `RN-06001`, `RN-06002`; `INV-01`, `INV-05`; `US-06014` |
| Setup | Almacén efímero con una cuenta de alumno y una de administrador |
| Pasos | Given una cuenta con un correo ya registrado, When se materializa otra con el mismo correo, Then el almacén la rechaza con `CORREO_YA_REGISTRADO`, **aunque la consulta previa del consumidor no lo hubiera visto**. Given una segunda cuenta con papel de administrador, Then `UNICIDAD_DE_ADMINISTRADOR_VIOLADA` |
| Salida esperada | Dos rechazos del almacén. Es la **segunda línea deliberada**: la consulta previa del consumidor **no es una garantía por sí sola**, y la capa de aplicación ya declara este camino como flujo alternativo propio |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06023 — Las-Dos-Preguntas-Sobre-El-Conjunto

| Campo | Valor |
| --- | --- |
| Tipo | Integración interna |
| Cubre | `CU-06005`; `RN-06001`, `RN-06002`; `US-06015` |
| Setup | Almacén efímero, en dos estados: con administrador y sin él |
| Pasos | Given un correo, When se pregunta si está registrado, Then la respuesta es un sí o un no y **no una cuenta**. Given el almacén sin administrador, When se pregunta si existe uno, Then no; con administrador, Then sí. Then **ninguna de las dos respuestas revela el estado ni el papel de la cuenta que ocupa un correo** |
| Salida esperada | Cuatro respuestas correctas y ninguna filtración. Son las dos preguntas que **ninguna entidad sola responde**, y por eso viven en el repositorio |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06024 — La-Marca-Viaja-Y-No-Altera-El-Estado-De-La-Cuenta

| Campo | Valor |
| --- | --- |
| Tipo | Integración interna |
| Cubre | `CU-06005`; `RN-06012`, `RN-06013`, `RN-06015`, `RN-06016`; `RC-06007`; `US-06016` |
| Setup | Almacén efímero con cuentas en los **tres** estados, cada una con trabajos |
| Pasos | Given las tres cuentas, When se escribe la marca de cambio de contraseña pendiente en cada una, Then **el estado de la cuenta no cambia** en ninguna, **ningún trabajo se pierde ni cambia de estado**, y **la marca viaja** al recuperarla. Then **la marca no es un estado de cuenta**: no ocupa su lugar ni lo reemplaza (`RC-06007`) |
| Salida esperada | Tres escrituras de marca sobre tres estados distintos, con el estado y los trabajos intactos. **La comprobación de qué habilita la marca no es de acá**: acá se conserva y se transporta |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.4 Mecanismos de seguridad

#### TC-06025 — Derivacion-Sin-Guardar-Ni-Registrar-En-Claro

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-06006`; `US-06017` |
| Setup | Una contraseña en claro evidentemente ficticia, declarada como tal |
| Pasos | Given una contraseña en claro, When se la deriva, Then se devuelve el valor derivado **con sus parámetros versionados junto a él**. Then **la contraseña en claro no queda escrita en ninguna parte** —ni en el almacén, ni en el registro del servidor, ni en el mensaje de ninguna condición—. Given la contraseña en claro ausente, Then `CONTRASENA_EN_CLARO_AUSENTE` |
| Salida esperada | Un valor derivado con sus parámetros y **0** apariciones del valor en claro en los tres lugares. Es el **único punto del producto donde la contraseña en claro se convierte en el valor guardado** |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06026 — Verificacion-Que-Distingue-El-Derivado-Ilegible

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-06006`; `US-06018` |
| Setup | Un valor derivado válido, uno con parámetros que no se pueden leer, y dos contraseñas en claro |
| Pasos | Given la contraseña correcta y su valor derivado, When se verifica, Then el veredicto es afirmativo. Given una contraseña distinta, Then negativo. Given un valor derivado **ilegible** —parámetros ausentes o no interpretables—, Then `CREDENCIAL_DERIVADA_ILEGIBLE`, **que no es lo mismo que una contraseña equivocada** |
| Salida esperada | Dos veredictos y un rechazo distinguible. Confundir el derivado ilegible con la contraseña equivocada haría que un dato corrupto se leyera como intento fallido |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06027 — Provisoria-No-Adivinable-Y-Sin-Repetirse

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-06007`; **`RN-06014`, su tramo principal y único**; `RN-06016`; `US-06019`; NFR de provisorias repetidas; `QG-09` |
| Setup | La fuente de material impredecible disponible; dos cuentas distintas |
| Pasos | Given la misma cuenta, When se producen **dos** provisorias consecutivas, Then **son distintas**. Given dos cuentas distintas, Then también. Then **ninguna es derivable del nombre, del correo ni de la fecha**. Then la invocación **no lleva ningún dato del acto que la motiva**, de modo que la de la **habilitación** y la del **reseteo** son el mismo mecanismo y no se pueden distinguir |
| Salida esperada | **0** provisorias iguales y **0** derivables de un dato conocido. Es la delegación explícita que las tres capas de arriba le hacen a ésta: `RN-06014` es la única de las dieciséis reglas cuyo tramo principal **y único** vive acá |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06028 — Sin-Aleatoriedad-No-Se-Produce-Valor

| Campo | Valor |
| --- | --- |
| Tipo | Unit, con la fuente de material impredecible doblada |
| Cubre | `CU-06007`; `RN-06014`; `US-06020`; el tercer riesgo de `05` §9 |
| Setup | La fuente de material impredecible **que no responde** |
| Pasos | Given la fuente que no responde, When se pide una provisoria, Then se devuelve `FUENTE_DE_ALEATORIEDAD_NO_DISPONIBLE` y **no se produce ningún valor**. Then **no se compone una provisoria por otro medio**: ni un contador, ni la fecha, ni el correo, ni el nombre |
| Salida esperada | Un rechazo y **cero** valores producidos por un atajo. `05` §9 lo declara de impacto **muy alto** con un fundamento que conviene repetir: **un reseteo que no se completa es recuperable; una provisoria adivinable no se nota hasta que alguien la usa** |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06029 — Acceso-Firmado-Con-Sus-Cuatro-Reclamos

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-06008`; `RN-06001`; `US-06021` |
| Setup | Una clave de firma evidentemente ficticia, provista por configuración de prueba |
| Pasos | Given los **cuatro** reclamos y la clave, When se emite el acceso, Then lleva los cuatro y **la firma verifica**. When se lo verifica con una clave distinta, Then el veredicto es negativo. Given reclamos incompletos, Then `RECLAMOS_INCOMPLETOS` y **no se emite**. Then el acceso **transporta el papel sin decidir qué habilita** |
| Salida esperada | Una emisión con sus cuatro reclamos, dos veredictos y un rechazo. La decisión de qué habilita el papel **no es de acá** |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06030 — Sin-Clave-De-Firma-No-Hay-Emision

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-06008`; `US-06022`; `QG-12`; el cuarto riesgo de `05` §9 |
| Setup | La configuración de prueba **sin** clave de firma |
| Pasos | Given la ausencia de clave, When se pide emitir un acceso, Then `CLAVE_DE_FIRMA_AUSENTE` y **no se emite ninguno**. Then **no se genera una clave al vuelo** y **no se emite sin firmar** |
| Salida esperada | Un rechazo y **cero** accesos emitidos por cualquiera de los dos atajos. `05` §9 lo declara de impacto muy alto: con cualquiera de ellos **el sistema arranca, emite accesos y nadie lo nota hasta que alguien falsifica uno** |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06031 — El-Sello-Del-Reloj-Entra-Por-Puerto

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-06009`; `US-06023` |
| Setup | Ninguno |
| Pasos | Given el adaptador de reloj, When se le pide el momento, Then devuelve el momento actual del sistema. Then **es el contrato más corto de la capa**, y es lo que permite que las capas de arriba fijen el momento en sus pruebas sin tocar el reloj del entorno |
| Salida esperada | Un momento devuelto, y dos invocaciones consecutivas que **no son necesariamente iguales**. La reproducibilidad de los sellos es de quien lo consume, no de acá |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.5 Arranque y preparación del almacén

#### TC-06032 — Transformaciones-Aplicadas-Sobre-Almacen-Inexistente

| Campo | Valor |
| --- | --- |
| Tipo | Integración interna |
| Cubre | `CU-06010`; `US-06024`; NFR de aplicación de transformaciones; `QG-04` |
| Setup | Un almacén **inexistente**, en una ubicación recibida por configuración de prueba |
| Pasos | Given un almacén que no existe, When arranca la preparación, Then el almacén **se crea**, las transformaciones **se aplican solas** y **ningún paso manual hace falta**. When se vuelve a arrancar sobre el almacén ya preparado, Then **no se aplica nada dos veces** y el linaje queda registrado |
| Salida esperada | **1 de 1** aplicación exitosa sobre almacén inexistente y una segunda ejecución idempotente. Es **criterio de aceptación de la etapa `c`** y etapa propia del pipeline (intake §17.1.P.8 · GeometriaFactory-Infrastructure) |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06033 — Arranque-Detenido-En-Lugar-De-Operar-Sobre-Un-Almacen-Dudoso

| Campo | Valor |
| --- | --- |
| Tipo | Integración interna |
| Cubre | `CU-06010`; `US-06025`; el quinto y el sexto riesgo de `05` §9 |
| Setup | Tres almacenes: uno con un esquema que **no corresponde** al linaje esperado; una ubicación **no disponible**; y uno correcto |
| Pasos | Given un almacén cuyo esquema no corresponde, When arranca la preparación, Then `MIGRACION_NO_APLICABLE` y **el arranque se detiene**. Then **el almacén no se descarta y no se recrea**. Given una ubicación no disponible, Then `RUTA_DEL_ALMACEN_NO_DISPONIBLE` y el arranque **se detiene**; Then **no se cae hacia una ruta alternativa dentro de la imagen**. Given el almacén correcto, Then el arranque procede |
| Salida esperada | Dos detenciones y un arranque. La primera evita «el atajo más destructivo del producto» —dejar el servicio impecable y **sin los trabajos de nadie**—; la segunda evita que el servicio acepte trabajos de la comisión entera y **los pierda en el siguiente reemplazo de versión** |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.6 Pruebas de inspección estructural

#### TC-06034 — Catalogo-De-17-Condiciones-En-Las-Dos-Direcciones

| Campo | Valor |
| --- | --- |
| Tipo | Unit, **prueba de inspección** |
| Cubre | NFR de cobertura del catálogo (`05` §8); `QG-13`; las **17** condiciones de `03` §3 |
| Setup | El conjunto de códigos que la batería observó emitidos, y el catálogo de `03` §3 |
| Pasos | Given los dos conjuntos, When se los compara, Then **las 17 condiciones están alcanzadas por al menos una prueba** y **ninguna condición emitida queda fuera del catálogo** |
| Salida esperada | 17 de 17 alcanzadas y 0 fuera. La comparación es **en las dos direcciones** |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06035 — Ningun-Mensaje-Ni-Traza-Con-Un-Secreto-La-Ruta-Del-Almacen-O-El-Texto-Del-Alumno

| Campo | Valor |
| --- | --- |
| Tipo | Unit, **prueba de inspección** |
| Cubre | `RA-03`; NFR de mensajes y trazas (`05` §8); `QG-13` |
| Setup | Las 17 condiciones provocadas una por una, y el registro del servidor de esa ejecución |
| Pasos | Given cada una de las 17 condiciones, When se la provoca, Then su mensaje **no contiene** la clave de firma, ninguna contraseña en claro, la ruta del almacén ni el texto del alumno. Given el registro del servidor de la misma ejecución, Then **tampoco**. Then **todo error que se muestre queda registrado del lado del servidor**, que es la contracara de `RA-03` |
| Salida esperada | **0** apariciones en las dos direcciones —mensajes y registro— y **17 de 17** errores registrados del lado del servidor |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

## 3. Recuento y verificación

| Magnitud | Valor | Cómo se verifica |
| --- | --- | --- |
| Casos de prueba de este catálogo | **35**, `TC-06001` a `TC-06035` | Contar los encabezados de §2 |
| Casos de la batería del validador | **10 de 10**, `TC-06001` a `TC-06010`, en el mismo orden que `05` §10.5 | §2.1 y [`Estrategia-Testing.md`](Estrategia-Testing.md) §6.1 |
| Casos de uso con al menos un caso de prueba | **10 de 10** | [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2 |
| Reglas de negocio con tramo acá y caso de prueba | **14 de 14**; las **dos** sin tramo se declaran | Matriz §4 |
| Reglas conceptuales de modelo con caso de prueba | **7 de 7** | Matriz §5 |
| Historias con caso de prueba | **25 de 25** | Matriz §2, columna de historias |
| NFR con caso de prueba propio | **11 de 14**; los otros tres son mediciones del pipeline | Matriz §3 |
| Escenarios del intake §20 usados como texto literal | **8 de 8** | `E-1` en `TC-06006`, `TC-06009`, `TC-06012`, `TC-06015`; `E-2` en `TC-06001`, `TC-06002`, `TC-06006`, `TC-06016`; `E-3` en `TC-06003`, `TC-06005`; `E-4` en `TC-06004`; `E-5` en `TC-06008`, `TC-06012`; `E-6` en `TC-06007`; `E-7` en `TC-06011`, `TC-06019`; `E-8` en `TC-06010` |
| Casos de prueba de inspección estructural | **3** — `TC-06014`, `TC-06034`, `TC-06035` | §2.2 y §2.6 |
| Casos de integración interna | **11** — `TC-06016` a `TC-06024`, `TC-06032`, `TC-06033` | §2.3 y §2.5 |
| Casos de prueba deshabilitados | **0** | Ninguna fila lo declara |

**Los ocho escenarios están, uno por uno, y entran como texto literal.** `E-7` es el único que no respalda un caso de la batería, y se usa igual como cobertura adicional declarada por `05` §10.5.

## 4. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara **treinta y cinco** casos de prueba, `TC-06001` a `TC-06035`, cuyos **diez primeros son, uno a uno y en el mismo orden, los diez casos de la batería del validador** que `05` §10.5 enumera, para que la correspondencia sea de identidad y no haya que traducirla. Suma cinco casos de cobertura adicional del validador —incluido `E-7`, que no respalda ningún caso de la batería y se usa igual—, nueve de almacén en integración interna, siete de los mecanismos de seguridad, dos de arranque y tres de inspección estructural. Los **ocho** escenarios del intake §20 entran **como texto literal, entero y carácter por carácter**, sin sustituirse por datos sintéticos. Todos los estados dicen `Pendiente` y todas las salidas observadas dicen «Sin ejecutar». |
