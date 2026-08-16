# Casos de prueba referenciales — GeometriaFactory-Application

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** Casos-Prueba-Referenciales.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** los **once** casos de uso de [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/) y las **cuatro** comprobaciones transversales de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../02-Especificacion-Funcional/_fusion/Application/Especificacion-Funcional.md) §4; las **dieciséis** reglas y los **nueve** invariantes de `GeometriaFactory-Domain`, referenciados por identificador; las **treinta y dos** historias de [`../06-Backlog-Tecnico/historias-usuario/`](../06-Backlog-Tecnico/historias-usuario/); las **36** condiciones de [`../03-UX-UI-DX/DX-Error-Messages.md`](../../../03-UX-UI-DX/_fusion/Application/DX-Error-Messages.md) §3 y §7.1; los **nueve** NFR de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/_fusion/Application/Arquitectura-Proyecto-Codigo.md) §8; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.19** §20 y §21
**Trazabilidad downstream:** [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md), [`Criterios-Validacion.md`](Criterios-Validacion.md), [`Plan-Pruebas.md`](Plan-Pruebas.md)

---

## Tabla de contenido

- [1. Cómo se lee este catálogo](#1-cómo-se-lee-este-catálogo)
- [2. Catálogo de casos de prueba](#2-catálogo-de-casos-de-prueba)
  - [2.1 Alta y gobierno de cuentas](#21-alta-y-gobierno-de-cuentas)
  - [2.2 Ingreso, credencial y la marca](#22-ingreso-credencial-y-la-marca)
  - [2.3 Trabajo, envío e interpretación](#23-trabajo-envío-e-interpretación)
  - [2.4 Consulta, alcance y desenlace](#24-consulta-alcance-y-desenlace)
  - [2.5 Pruebas de inspección estructural](#25-pruebas-de-inspección-estructural)
- [3. Recuento y verificación](#3-recuento-y-verificación)
- [4. Control de cambios](#4-control-de-cambios)

---

## 1. Cómo se lee este catálogo

Cada `TC-XX` declara ocho campos, según `Rules-Calidad-Y-Pruebas.md` §4.6: identificador y nombre, tipo, upstream cubierto, setup, pasos en Given-When-Then, salida esperada, salida observada y estado.

**Todas las filas de «Salida observada» dicen «Sin ejecutar» y todos los estados dicen `Pendiente`.** No hay sistema construido: el proyecto de código arranca en la etapa `a` y este catálogo se emite antes. Declarar cualquier otra cosa sería una afirmación sobre el estado del sistema sin evidencia.

**Vocabulario de este catálogo**, definido acá la primera vez que aparece y no redefinido después:

- **Nivel**: la posición de una prueba en la pirámide de [`Estrategia-Testing.md`](../../Estrategia-Testing.md) §1. Acá hay un solo nivel: unitario.
- **Doble de puerto**: la sustitución de uno de los **cuatro** puertos de `02` §3, que es la única frontera que una prueba de este proyecto de código sustituye ([`Estrategia-Testing.md`](../../Estrategia-Testing.md) §5).
- **Fixture**: un constructor compartido, de los cuatro que declara [`Estrategia-Testing.md`](../../Estrategia-Testing.md) §5.
- **Prueba de inspección**: la que comprueba una propiedad estructural del proyecto de código y no un caso de uso.
- **Resultado de interpretación**: el conjunto de piezas, observaciones y cantidad de figuras del conjunto raíz que el puerto de validación devuelve. Esta capa **no lo produce**: lo recibe y lo entrega al dominio.
- **La marca**: la marca de cambio de contraseña pendiente, que es lo que `INV-09` gobierna.

**Un caso de prueba no es una historia.** Varias historias se ejercitan en el mismo `TC-XX` cuando comparten contrato de uso, setup y forma de verificación; la correspondencia completa está en [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2.

## 2. Catálogo de casos de prueba

### 2.1 Alta y gobierno de cuentas

#### TC-04001 — Alta-De-Alumno-Con-La-Unicidad-Resuelta-Por-El-Puerto

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04001`; `RN-04002`, `RN-04006`; `INV-01`; `US-04001` |
| Setup | Doble del repositorio de cuentas que responde que el correo **no** está registrado; doble del reloj con un momento fijo elegido por la prueba |
| Pasos | Given los datos obligatorios del alta y un correo que el repositorio declara libre, When se invoca el auto-registro, Then la cuenta se constituye **`Pendiente`, sin credencial y con papel `Alumno`**, y la materialización se pide una sola vez |
| Salida esperada | Cuenta constituida en estado `Pendiente`, sin credencial derivada; el doble de repositorio recibe exactamente una materialización; el momento usado es el que la prueba fijó |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04002 — Rechazos-Del-Auto-Registro

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04001`; `RN-04001`, `RN-04002`; `INV-01`, `INV-05`; `US-04002` |
| Setup | Doble del repositorio de cuentas en dos configuraciones: correo libre y correo ocupado |
| Pasos | Given un correo que el repositorio declara ocupado, When se invoca el alta, Then `CORREO_YA_REGISTRADO`. Given el mismo correo declarado libre pero rechazado por la materialización, Then **el mismo motivo por el segundo camino**. Given un alta a la que le falta un dato obligatorio, Then `DATO_OBLIGATORIO_AUSENTE`. Given un alta que aporta credencial, Then `CREDENCIAL_NO_ADMITIDA_EN_EL_ALTA`. Given un alta que pide un estado distinto de `Pendiente`, Then `ESTADO_INICIAL_NO_NEGOCIABLE`. Given un alta que pide papel `Administrador`, Then `PAPEL_DE_ADMINISTRADOR_FUERA_DE_ESTE_CAMINO` |
| Salida esperada | Seis rechazos —`CORREO_YA_REGISTRADO` por sus **dos** caminos y cuatro motivos más—, **sin cuenta constituida** en ninguno y sin unidad de trabajo abierta |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04003 — Configurar-El-Administrador-Y-Rechazar-El-Segundo

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04010`; `RN-04001`, `RN-04002`, `RN-04006`; `INV-05`, `INV-08`, `INV-01`; `US-04003`, `US-04028` |
| Setup | Doble del repositorio de cuentas que responde si ya existe una cuenta con papel `Administrador`, en sus dos formas; doble del reloj |
| Pasos | Given que el repositorio declara que no existe administrador, los datos obligatorios y la credencial **ya derivada**, When se configura, Then la cuenta nace **`Habilitado`, con papel `Administrador` y con credencial**. Given que ya existe uno, Then `ADMINISTRADOR_YA_CONFIGURADO`. Given la configuración sin credencial derivada, Then `CONFIGURACION_SIN_CREDENCIAL`. Given un estado pedido distinto de `Habilitado`, Then `ESTADO_INICIAL_NO_NEGOCIABLE` **por la causa opuesta a la de `TC-04002`**. Given un correo ya registrado, Then `CORREO_YA_REGISTRADO`. Given un dato obligatorio ausente, Then `DATO_OBLIGATORIO_AUSENTE` |
| Salida esperada | Una configuración aplicada y cinco rechazos. La fila de `ESTADO_INICIAL_NO_NEGOCIABLE` se verifica **en sus dos causas opuestas**, que es lo que [`../03-UX-UI-DX/DX-Error-Messages.md`](../../../03-UX-UI-DX/_fusion/Application/DX-Error-Messages.md) §1.4 declara |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04004 — Habilitar-Bloquear-Y-Rehabilitar-Produciendo-La-Provisoria

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04002`, `CU-04003`; `RN-04016`, `RN-04014`, `RN-04006`, `RN-04001`; `INV-09`, `INV-06`; `US-04004`, `US-04008` |
| Setup | Fixture de cuenta de alumno en los tres estados; fixture de solicitante administrador sin marca; dobles del repositorio de cuentas y del reloj |
| Pasos | Given una cuenta `Pendiente` y el administrador como solicitante, When se la habilita, Then queda `Habilitado`, con la credencial provisoria **ya derivada** fijada y **con la marca puesta**. Given la habilitación invocada sin aportar el valor derivado, Then `HABILITACION_SIN_CREDENCIAL_PROVISORIA`. Given el valor derivado vacío, Then `VALOR_DERIVADO_VACIO`. Given una cuenta `Habilitado`, When se la bloquea y se la rehabilita, Then las dos transiciones proceden y la rehabilitación **vuelve a poner la marca**. Given un par estado-operación no declarado, Then `TRANSICION_DE_CUENTA_NO_ADMITIDA`. Given un solicitante alumno, Then `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` |
| Salida esperada | Tres transiciones aplicadas con la marca puesta en las dos que producen provisoria, y cuatro rechazos con su motivo. **`RN-04014` no se ejerce acá**: lo que se verifica es que el valor llega ya producido y ya derivado y que la operación lo rechaza vacío |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04005 — Baja-Con-Confirmacion-Escrita-Y-Arrastre-En-Una-Sola-Unidad-De-Trabajo

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04002`; `RN-04007`, `RN-04004`, `RN-04001`; `INV-03`, `INV-08`; `US-04005`, `US-04006` |
| Setup | Fixture de cuenta de alumno `Habilitado` con trabajos en los **cuatro** estados; fixture de solicitante administrador; dobles de los repositorios de cuentas y de trabajos |
| Pasos | Given la cuenta con sus cuatro trabajos y el correo escrito que **coincide**, When se da de baja, Then la cuenta y **los cuatro trabajos** se materializan en **una sola** unidad de trabajo. Given un correo escrito que no coincide, Then `CONFIRMACION_DE_BAJA_NO_COINCIDE` y **nada cambia**. Given la baja pedida sobre la cuenta de administrador, Then `CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA`. Given un solicitante alumno, Then `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` |
| Salida esperada | Baja con arrastre a los cuatro estados, incluidos `Finalizado` y `Rechazado`, con **una** apertura de unidad de trabajo y no dos; tres rechazos sin efecto parcial. Es el caso testigo del NFR de unidades de trabajo por caso de uso |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04006 — Reseteo-Que-Conserva-Cuenta-Estado-Y-Todos-Sus-Trabajos

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04011`; `RN-04012`, `RN-04015`, `RN-04014`, `RN-04013`; `INV-09`; `US-04029`, `US-04031` |
| Setup | Fixture de cuenta de alumno en los **tres** estados, cada una con trabajos en los cuatro; fixture de solicitante administrador; dobles de los repositorios y del reloj |
| Pasos | Given una cuenta `Pendiente`, una `Habilitado` y una `Bloqueado`, cada una con sus trabajos, When el administrador resetea su contraseña con la provisoria **ya producida y ya derivada**, Then en las tres la credencial se reemplaza, **la marca queda puesta**, el estado de cuenta **no cambia** y **ningún trabajo se pierde ni cambia de estado ni pierde su comentario**. Given el valor derivado vacío, Then `VALOR_DERIVADO_VACIO`. Given una cuenta inexistente, Then `CUENTA_INEXISTENTE` |
| Salida esperada | Tres reseteos aplicados sobre los tres estados de cuenta, con el recuento de trabajos y sus estados idénticos antes y después; **cero** retiros pedidos al repositorio de trabajos; dos rechazos. La ausencia de comprobación de estado es lo que verifica `RN-04015` |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04007 — El-Reseteo-Esta-Acotado-A-Cuentas-De-Alumno

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04011`; `RN-04001`, `RN-04015`; `INV-08`, `INV-05`; `US-04029` |
| Setup | Fixture de cuenta de administrador; fixture de solicitante administrador y de solicitante alumno |
| Pasos | Given la cuenta con papel `Administrador` como objeto del reseteo, When se lo pide, Then `RESETEO_ACOTADO_A_CUENTAS_DE_ALUMNO` y la cuenta queda intacta. Given un solicitante alumno sobre una cuenta ajena, Then `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` **antes** de tocar el repositorio |
| Salida esperada | Dos rechazos con su motivo, cero escrituras, y la constancia de que la negativa por facultad se resuelve sin consultar el repositorio de cuentas |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.2 Ingreso, credencial y la marca

#### TC-04008 — Admisibilidad-Con-Su-Motivo-En-Los-Tres-Estados

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04003`; `RN-04006`, `RN-04013`; `INV-06`, `INV-09`; `US-04007` |
| Setup | Fixture de cuenta de alumno en los tres estados, con y sin la marca; doble del repositorio de cuentas |
| Pasos | Given una cuenta `Pendiente`, When se consulta la admisibilidad, Then **no admisible** con `CUENTA_PENDIENTE`. Given una `Bloqueado`, Then `CUENTA_BLOQUEADA`. Given una `Habilitado` sin marca, Then **admisible**. Given una `Habilitado` **con la marca puesta**, Then **no admisible** con `CAMBIO_DE_CONTRASENA_PENDIENTE`. Given un correo que no corresponde a ninguna cuenta, Then `CUENTA_INEXISTENTE` |
| Salida esperada | Cinco resultados distintos, con los **motivos sin colapsar**: `CUENTA_PENDIENTE` y `CUENTA_BLOQUEADA` no se confunden entre sí ni con la marca. Ninguna invocación lanza |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04009 — Reemplazo-De-Credencial-Exigiendo-La-Vigente

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04003`; `RN-04013`, `RN-04006`; `INV-09`, `INV-06`; `US-04009` |
| Setup | Fixture de cuenta de alumno `Habilitado` con credencial fijada; doble del repositorio de cuentas |
| Pasos | Given la credencial vigente declarada verificada y un valor nuevo **ya derivado**, When la propia cuenta la reemplaza, Then el valor se reemplaza. Given la vigente **no** verificada, Then `CREDENCIAL_VIGENTE_NO_VERIFICADA`. Given un valor derivado vacío, Then `VALOR_DERIVADO_VACIO`. Given una cuenta que no está habilitada, Then `CUENTA_NO_HABILITADA_PARA_CREDENCIAL`. Given una fijación sobre una cuenta que ya tiene credencial, Then `CREDENCIAL_YA_FIJADA` |
| Salida esperada | Un reemplazo aplicado y cuatro rechazos, cada uno con su motivo y sin escritura parcial |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04010 — La-Marca-Se-Levanta-Solo-Con-El-Cambio-Hecho-Por-La-Propia-Cuenta

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04003`, `CU-04002`, `CU-04011`; `RN-04013`, `RN-04016`, `RN-04012`; `INV-09`; `US-04032`, `US-04030` |
| Setup | Fixture de cuenta de alumno `Habilitado` **con la marca puesta**; dobles del repositorio de cuentas y del reloj |
| Pasos | Given la cuenta con la marca, When la **propia cuenta** reemplaza su credencial presentando la vigente verificada, Then el valor se reemplaza **y la marca se levanta**. Given la misma cuenta, When el administrador la resetea, Then la marca **queda puesta** y no se levanta. Given la misma cuenta, When el administrador la habilita o la rehabilita, Then la marca **queda puesta**. Given cualquier otra operación de la capa sobre esa cuenta, Then la marca **no cambia** |
| Salida esperada | **Un solo camino levanta la marca** y los otros tres la ponen o la dejan como estaba. Es la verificación directa de `INV-09` y de la precisión 5 de `02` §4 |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04011 — La-Cuarta-Comprobacion-Corta-Antes-Que-Las-Otras-Tres

| Campo | Valor |
| --- | --- |
| Tipo | Unit, **prueba de orden** |
| Cubre | Los **once** casos de uso, de forma transversal; las **cuatro** comprobaciones de `02` §4; `RN-04013`, `RN-04003`, `RN-04001`, `RN-04011`; `INV-09`, `INV-02`, `INV-03`; `US-04030` |
| Setup | Fixture de solicitante en sus **cuatro** formas; fixture de trabajo propio y ajeno en los cuatro estados; dobles de los cuatro puertos, **sin base de datos** |
| Pasos | Given un solicitante **con la marca puesta** que además pide un trabajo ajeno, When invoca cualquier caso de uso salvo el reemplazo de `CU-04003` FA-05, Then el motivo emitido es `CAMBIO_DE_CONTRASENA_PENDIENTE` y **no** `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`. Given un solicitante con la marca que además carece de facultad, Then el motivo es el de la marca y **no** `FACULTAD_DE_ADMINISTRADOR_REQUERIDA`. Given un solicitante con la marca sobre un borrador ajeno al alcance del administrador, Then el motivo es el de la marca y **no** `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR`. Given el reemplazo de `CU-04003` FA-05, Then la marca **no corta** y la operación procede |
| Salida esperada | La cuarta comprobación gana sobre las otras tres en los tres cruces, y cede **sólo** en la única excepción declarada. `05` §8 exige exactamente **1** prueba de este orden, y ésta es esa prueba |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04012 — Pertenencia-Y-Facultad-No-Se-Confunden

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04004`, `CU-04005`, `CU-04006`, `CU-04009`, `CU-04007`, `CU-04008`, `CU-04002`, `CU-04011`; `RN-04003`, `RN-04001`; `INV-02`; `US-04012`, `US-04025` |
| Setup | Fixture de trabajo propio y ajeno; fixture de solicitante alumno y administrador, ninguno con marca; dobles de los cuatro puertos |
| Pasos | Given un trabajo **ajeno** y un identificador **inexistente**, When el alumno los pide, Then los dos resultados son **idénticos**: `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`, sin ninguna diferencia observable. Given una operación reservada pedida por un alumno, Then `FACULTAD_DE_ADMINISTRADOR_REQUERIDA`, que sí es explícito. Given un solicitante no declarado, Then `SOLICITANTE_NO_DECLARADO`. Given un papel que no es ninguno de los dos, Then `PAPEL_NO_RECONOCIDO` |
| Salida esperada | Indistinguibilidad verificada en los dos sentidos y las dos negativas separadas. Es la mitigación del tercer riesgo de `05` §9 |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.3 Trabajo, envío e interpretación

#### TC-04013 — Constituir-El-Trabajo-Con-Dueno-Y-Sello-Del-Reloj

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04004`; `RN-04008`, `RN-04003`; `INV-02`; `US-04010`, `US-04011` |
| Setup | Doble del reloj con **dos momentos consecutivos distintos** elegidos por la prueba; doble del repositorio de trabajos |
| Pasos | Given dueño, nombre, fecha declarada por el alumno y texto original, When se constituye el trabajo, Then nace en `Borrador`, con dueño, con identificador propio y con el sello tomado **del puerto de reloj** y no del reloj del entorno. Given un trabajo sin dueño, Then `TRABAJO_SIN_DUENO`. Given un dato obligatorio ausente, Then `DATO_OBLIGATORIO_AUSENTE`. Given el mismo texto de `E-2` cargado y recuperado, Then el texto es **idéntico carácter por carácter** |
| Salida esperada | Trabajo constituido con los tres sellos distinguibles —la fecha del alumno y los del sistema—, texto íntegro y dos rechazos. Dos ejecuciones consecutivas con momentos distintos producen sellos distintos y **el mismo resultado por lo demás** |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04014 — Reeditar-Solo-Un-Trabajo-Propio-En-Borrador

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04004`; `RN-04003`, `RN-04004`, `RN-04008`; `INV-02`, `INV-03`; `US-04012`, `US-04011` |
| Setup | Fixture de trabajo en los cuatro estados, propio y ajeno; dobles de los puertos |
| Pasos | Given un trabajo propio en `Borrador`, When se lo reedita con un texto nuevo, Then el texto se reemplaza **entero** y la interpretación anterior se descarta. Given un trabajo propio fuera de `Borrador`, Then `OPERACION_FUERA_DE_BORRADOR`. Given un trabajo ajeno, Then `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`. Given una reedición que pretende alterar el texto conservado sin reemplazarlo, Then `TEXTO_ORIGINAL_ALTERADO` |
| Salida esperada | Una reedición aplicada en los tres estados donde no procede y tres rechazos; el texto nunca queda mezclado entre la versión vieja y la nueva |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04015 — Envio-Con-Advertencias-Que-Pasa-A-Pendiente

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04005`; `RN-04005`, `RN-04008`, `RN-04009`; `INV-04`; `US-04013` |
| Setup | Doble del puerto de validación cargado con los resultados de interpretación de los escenarios **`E-1`, `E-2` y `E-3`** del intake §20; fixture de trabajo propio en `Borrador` |
| Pasos | Given el resultado de `E-1` —**3 piezas y 2 advertencias**, ninguna de severidad `Error`—, When se envía el trabajo, Then el dominio lo lleva a `Pendiente` y las dos advertencias quedan incorporadas. Given el de `E-2` —1 pieza, 1 advertencia de volumen—, Then pasa a `Pendiente` **con la advertencia asociada**. Given el de `E-3` —advertencia de área con el par declarado **36.00** y derivado **54.00**—, Then pasa a `Pendiente` y el par de valores llega entero, sin texto genérico y **sin corregir el valor del alumno** |
| Salida esperada | Tres envíos que pasan a `Pendiente` con sus observaciones incorporadas; el texto original intacto en los tres. **Los datos son los del intake §20 y no se sustituyen por datos sintéticos** |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04016 — Envio-Con-Error-Que-Queda-En-Borrador

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04005`; `RN-04005`, `RN-04009`, `RN-04008`; `INV-04`; `US-04014` |
| Setup | Doble del puerto de validación cargado con los resultados de **`E-5`** y **`E-8`**; fixture de trabajo propio en `Borrador` |
| Pasos | Given el resultado de `E-5` —una observación de severidad **`Error`** con **índice de figura 1** y **campo `Tipo`**, y la primera pieza interpretada igual—, When se envía, Then el trabajo **queda en `Borrador`**, con su texto conservado y con la observación ubicada. Given el resultado de `E-8` —dimensión no legible—, Then **también es error y no advertencia**, y el trabajo **queda en `Borrador`** [DECISIÓN 2026-08-09 del intake §20.E-8 punto 5]. Given una observación sin índice de figura o sin campo, Then `OBSERVACION_MAL_FORMADA` |
| Salida esperada | Dos envíos que **no** transicionan, con las observaciones localizadas por índice y campo, más un rechazo por observación mal formada. La distinción entre `E-3` —advertencia— y `E-8` —error— es lo que este caso separa |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04017 — Envio-Sin-Observaciones-Y-Con-Dimension-En-Cero

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04005`; `RN-04005`; `INV-04`; `US-04013` |
| Setup | Doble del puerto de validación cargado con los resultados de **`E-4`** y **`E-6`** |
| Pasos | Given el resultado de `E-4` —**cero observaciones en total**—, When se envía, Then el trabajo pasa a `Pendiente` **sin ninguna observación que incorporar**. Given el de `E-6` —una figura que **se interpreta** y produce a lo sumo una advertencia—, Then el trabajo pasa a `Pendiente` y la figura **no se descarta** |
| Salida esperada | Dos envíos que pasan a `Pendiente`; el de `E-4` con la colección de observaciones vacía y no ausente. Es el **criterio negativo**, que el intake §20.E-4 declara más difícil de acertar que el positivo |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04018 — Interpretacion-Por-El-Puerto-Sin-Tocar-El-Almacen

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04005`; `RN-04009`; `US-04015` |
| Setup | Dobles de los cuatro puertos, con el de validación instrumentado para contar invocaciones |
| Pasos | Given un envío, When se lo ejecuta, Then el puerto de validación se invoca **exactamente una vez** con el texto original tal cual, y el resultado —piezas, observaciones y **la cantidad de figuras del conjunto raíz**— se entrega al dominio sin recomponerlo. Given la cantidad de figuras que el puerto declara y una observación cuyo índice cae fuera de ese rango, Then `CONJUNTO_DE_PIEZAS_MAL_FORMADO` |
| Salida esperada | Una invocación al puerto, cero al almacén real, y la cantidad de figuras usada como rango de validación de la posición. Es lo que `02` §3 declara que sólo `CU-04005` hace viajar |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04019 — Terminacion-Controlada-Cuando-La-Interpretacion-No-Esta-Disponible

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04005`; `RN-04008`; `US-04016` |
| Setup | Doble del puerto de validación configurado como **no disponible**; fixture de trabajo propio en `Borrador` con su texto |
| Pasos | Given el puerto de validación no disponible, When se envía el trabajo, Then se devuelve `INTERPRETACION_NO_DISPONIBLE` **como valor y no como excepción**, el trabajo **queda en `Borrador`**, el texto original queda **intacto** y no se abre ninguna unidad de trabajo de escritura. Given un envío sobre un trabajo que no está en `Borrador`, Then `ENVIO_FUERA_DE_BORRADOR` |
| Salida esperada | Un rechazo controlado sin efecto parcial y un rechazo por estado. Ninguna excepción escapa de la capa |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.4 Consulta, alcance y desenlace

#### TC-04020 — Listado-Propio-Con-Los-Cuatro-Estados-Y-Sin-Componentes

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04006`; `RN-04003`; `INV-02`; `US-04017`, `US-04019` |
| Setup | Fixture de trabajo en los cuatro estados, propios y de otro alumno; doble del repositorio de trabajos que registra el predicado recibido |
| Pasos | Given trabajos propios en los cuatro estados y trabajos de otro alumno, When el alumno pide su listado, Then recibe **sólo los propios**, con los cuatro estados distinguibles, y el predicado de dueño **llegó en la consulta** y no se aplicó después. Given el mismo listado, Then **ningún componente de pieza viene materializado** |
| Salida esperada | Listado acotado por consulta, cuatro estados distinguibles y cero componentes cargados |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04021 — Listado-De-La-Comision-Sin-Borradores-Y-Con-Filtro-Por-Alumno

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04007`; `RN-04011`, `RN-04001`; `US-04020`, `US-04021` |
| Setup | Fixture de trabajo en los cuatro estados de **dos** alumnos distintos; fixture de solicitante administrador y alumno |
| Pasos | Given trabajos de dos alumnos en los cuatro estados, When el administrador pide el listado de la comisión, Then **ningún trabajo en `Borrador`** aparece y cada fila trae su dueño. Given el filtro por un alumno, Then el recorte **se compone con** el predicado de alcance y no lo reemplaza. Given un solicitante alumno, Then `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` **sin consultar el repositorio de trabajos** |
| Salida esperada | Listado sin borradores, filtro compuesto con el alcance, negativa por facultad resuelta antes de consultar |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04022 — Detalle-Equivalente-Para-Los-Dos-Papeles-Con-Desenlace-Y-Comentario

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04006`, `CU-04007`; `RN-04003`, `RN-04011`; `INV-02`; `US-04018`, `US-04019`, `US-04022` |
| Setup | Doble del puerto de validación con el resultado de **`E-7`** —6 piezas, los seis tipos—; fixture de trabajo `Finalizado` con comentario y `Rechazado` con comentario |
| Pasos | Given un trabajo propio con desenlace y comentario, When el alumno abre su detalle, Then recibe **piezas con su índice y sus componentes**, las observaciones, el desenlace y el comentario. Given el mismo trabajo, When el administrador lo abre, Then recibe **los mismos elementos**. Given un borrador ajeno pedido por el administrador, Then `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR`. Given un trabajo ajeno pedido por un alumno, Then `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE` |
| Salida esperada | Dos detalles equivalentes con las **seis** piezas de `E-7` y sus componentes, y dos negativas **distintas** según el papel, que es lo que separa el alcance del administrador de la pertenencia del alumno |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04023 — Aprobar-Y-Rechazar-Desde-Pendiente-Con-Comentario-Opcional

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04008`; `RN-04010`, `RN-04011`; `INV-07`; `US-04023`, `US-04024` |
| Setup | Fixture de trabajo en estado `Pendiente`; fixture de solicitante administrador; doble del reloj |
| Pasos | Given un trabajo en `Pendiente` y el administrador, When se lo aprueba **sin comentario**, Then alcanza `Finalizado`. Given otro, When se lo rechaza **con comentario**, Then alcanza `Rechazado` y el comentario queda con su fecha y su autor. Given un desenlace que no es ninguno de los dos, Then `DESENLACE_DESCONOCIDO` |
| Salida esperada | Dos estados terminales alcanzados, el comentario opcional en las dos formas, y un rechazo. El sello de desenlace sale del puerto de reloj |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04024 — Rechazo-De-Toda-Transicion-Sin-Facultad-O-Desde-Estado-Terminal

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04008`; `RN-04010`, `RN-04001`, `RN-04011`; `INV-07`; `US-04025` |
| Setup | Fixture de trabajo en los cuatro estados; fixture de solicitante alumno y administrador |
| Pasos | Given un trabajo en `Pendiente` y un solicitante alumno, When pide el desenlace, Then `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` **antes** de comprobar el estado. Given un trabajo en `Borrador` y el administrador, Then `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR`. Given un trabajo `Finalizado` o `Rechazado`, Then `TRANSICION_DESDE_ESTADO_TERMINAL` y **el contenido no cambia**. Given un trabajo que no está en `Pendiente` pero sí en el alcance, Then `DESENLACE_FUERA_DE_PENDIENTE`. Given un identificador que no existe, Then `TRABAJO_INEXISTENTE` |
| Salida esperada | Cinco rechazos, con **el orden verificado**: facultad antes que alcance y alcance antes que estado, que es lo que impide que un rechazo por facultad se lea como rechazo por terminalidad |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04025 — Eliminacion-Con-Sus-Dos-Alcances-Opuestos

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04009`; `RN-04004`, `RN-04011`, `RN-04003`; `INV-03`, `INV-02`; `US-04026`, `US-04027` |
| Setup | Fixture de trabajo en los cuatro estados, propio y ajeno; fixture de solicitante alumno y administrador |
| Pasos | Given un trabajo propio en `Borrador` y el alumno, When lo elimina, Then se retira. Given un trabajo propio fuera de `Borrador` y el alumno, Then `OPERACION_FUERA_DE_BORRADOR`. Given un trabajo ajeno y el alumno, Then `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`. Given los **tres** estados que el administrador ve, When los elimina, Then los tres se retiran. Given un `Borrador` y el administrador, Then `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR` |
| Salida esperada | Cuatro retiros —uno del alumno y tres del administrador— y tres rechazos. Los dos alcances son **opuestos** y ninguno se filtra en el otro |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.5 Pruebas de inspección estructural

#### TC-04026 — Cero-Pruebas-Que-Tocan-La-Base-De-Datos-Real

| Campo | Valor |
| --- | --- |
| Tipo | Unit, **prueba de inspección** |
| Cubre | NFR de pruebas que tocan la base real (`05` §8); `QG-04`; `BT-04006` |
| Setup | Ninguno. Se inspecciona el propio proyecto de pruebas |
| Pasos | Given el proyecto de pruebas de esta capa, When se lo inspecciona, Then **0** pruebas abren un almacén real, **0** referencian una biblioteca de acceso a datos y **0** leen una cadena de conexión |
| Salida esperada | Tres recuentos en cero. Es la puerta propia y bloqueante que el intake §17.1.P.8 · GeometriaFactory-Application declara: una prueba que la incumpla **está mal ubicada** y pertenece a la batería de integración de `GeometriaFactory-Api` |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04027 — Una-Sola-Dependencia-Saliente

| Campo | Valor |
| --- | --- |
| Tipo | Unit, **prueba de inspección** |
| Cubre | NFR de dependencias salientes (`05` §8); `QG-05`; `BT-04001`, `BT-04004` |
| Setup | Ninguno. Se lee el archivo de proyecto |
| Pasos | Given el archivo de proyecto, When se lo inspecciona, Then declara exactamente **1** referencia a otro proyecto de código del producto —`GeometriaFactory-Domain`— y **0** a bibliotecas de persistencia, transporte, serialización o marco web |
| Salida esperada | Un recuento en 1 y cuatro en 0. Es la propiedad que justifica el estilo entero y la que hace posible `TC-04026` |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04028 — Catalogo-De-36-Condiciones-En-Las-Dos-Direcciones

| Campo | Valor |
| --- | --- |
| Tipo | Unit, **prueba de inspección** |
| Cubre | NFR de cobertura del catálogo (`05` §8); `QG-06`; las **36** condiciones de `03` §7.1 |
| Setup | El conjunto de códigos que la batería observó emitidos, y el catálogo de `03` §3 |
| Pasos | Given los dos conjuntos, When se los compara, Then **las 36 condiciones del catálogo están alcanzadas por al menos una prueba** y **ninguna condición emitida queda fuera del catálogo** |
| Salida esperada | 36 de 36 alcanzadas y 0 fuera. La comparación es **en las dos direcciones**: una sola dirección deja pasar el código inventado aguas abajo, que es lo que la Definition of Ready §1 criterio 6 prohíbe |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04029 — A-Lo-Sumo-Una-Unidad-De-Trabajo-Por-Caso-De-Uso

| Campo | Valor |
| --- | --- |
| Tipo | Unit, **prueba de inspección** |
| Cubre | NFR de unidades de trabajo por caso de uso (`05` §8); `QG-08`; `RN-04007`; `US-04006` |
| Setup | Dobles de los repositorios instrumentados para contar aperturas de unidad de trabajo |
| Pasos | Given cada uno de los once casos de uso, When se lo ejerce, Then abre **a lo sumo una** unidad de trabajo. Given la baja de una cuenta con trabajos en los cuatro estados —caso testigo—, Then la cuenta y los cuatro trabajos se materializan en **la misma** |
| Salida esperada | Once recuentos en 0 o 1, y el caso testigo con **una** apertura y cinco materializaciones dentro de ella |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04030 — Cero-Componentes-Cargados-En-Las-Consultas-De-Listado

| Campo | Valor |
| --- | --- |
| Tipo | Unit, **prueba de inspección** |
| Cubre | NFR de componentes de pieza en las consultas de listado (`05` §8); `QG-09`; `US-04019` |
| Setup | Doble del repositorio de trabajos que devuelve la proyección declarada |
| Pasos | Given el listado del alumno y el de la comisión, When se los resuelve, Then la colección de componentes **no viene materializada** en ninguna de las dos. Given el detalle, Then **sí** viene |
| Salida esperada | Dos listados sin componentes y un detalle con ellos. Es la decisión de modelado con efecto directo en el tiempo de respuesta del listado del administrador (intake §17.1.P.10 · GeometriaFactory-Application) |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04031 — Ninguna-Condicion-Prevista-Viaja-Como-Excepcion

| Campo | Valor |
| --- | --- |
| Tipo | Unit, **prueba de inspección** |
| Cubre | `QG-11`; [`ADR-04006`](../../../05-Arquitectura-Tecnica/Adrs/ADR-04006-Resultado-Tipado-Y-Catalogo-Cerrado-De-Condiciones.md); el quinto riesgo de `05` §9 |
| Setup | Los mismos dobles con los que se ejercen las 36 condiciones |
| Pasos | Given cada una de las **36** condiciones del catálogo, When se la provoca, Then el caso de uso **devuelve un valor** con su código y **no lanza**. Given la indisponibilidad de un puerto, Then tampoco lanza: devuelve `INTERPRETACION_NO_DISPONIBLE` |
| Salida esperada | 36 rechazos como valor y **0** excepciones de negocio. Las excepciones quedan reservadas a defectos de programación del consumidor, que es lo que `ADR-04006` decidió |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

## 3. Recuento y verificación

| Magnitud | Valor | Cómo se verifica |
| --- | --- | --- |
| Casos de prueba de este catálogo | **31**, `TC-04001` a `TC-04031` | Contar los encabezados de §2 |
| Casos de uso con al menos un caso de prueba | **11 de 11** | [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2 |
| Reglas de negocio con al menos un caso de prueba | **16 de 16** | Matriz §4 |
| Invariantes con al menos un caso de prueba | **9 de 9** | Matriz §6 |
| Comprobaciones de autorización con prueba de su negativa | **4 de 4**, más **1** prueba de orden (`TC-04011`) | Matriz §5 |
| Historias con caso de prueba | **32 de 32** | Matriz §2, columna de historias |
| NFR con caso de prueba propio | **6 de 9**; los otros tres son mediciones del pipeline | Matriz §3 |
| Escenarios del intake §20 usados como fixture | **8 de 8** | `TC-04015` (`E-1`, `E-2`, `E-3`), `TC-04016` (`E-5`, `E-8`), `TC-04017` (`E-4`, `E-6`), `TC-04022` (`E-7`) |
| Casos de prueba de inspección estructural | **6**, `TC-04026` a `TC-04031` | §2.5 |
| Casos de prueba deshabilitados | **0** | Ninguna fila lo declara |

**Los ocho escenarios están, uno por uno, y ninguno se sustituye.** `E-1`, `E-2` y `E-3` en `TC-04015`; `E-4` y `E-6` en `TC-04017`; `E-5` y `E-8` en `TC-04016`; `E-7` en `TC-04022`. La forma en que entran a esta capa es la que [`Estrategia-Testing.md`](../../Estrategia-Testing.md) §6 declara: **el resultado de interpretación que el doble del puerto devuelve**, no el texto.

## 4. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara **treinta y un** casos de prueba, `TC-04001` a `TC-04031`, repartidos en cinco grupos —alta y gobierno de cuentas, ingreso y credencial con la marca, trabajo y envío, consulta y desenlace, e inspección estructural—, cada uno con sus ocho campos y con su upstream explícito. Incluye la **prueba de orden** que `05` §8 exige con umbral 1 (`TC-04011`) y las **seis** pruebas de inspección que materializan los NFR estructurales. Todos los estados dicen `Pendiente` y todas las salidas observadas dicen «Sin ejecutar», porque no hay sistema construido. Los **ocho** escenarios del intake §20 entran como resultado de interpretación en cuatro casos de prueba, sin sustituirse por datos sintéticos. |
