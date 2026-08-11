# Casos de prueba referenciales — GeometriaFactory-Domain

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** Casos-Prueba-Referenciales.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** los **trece** casos de uso de [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/) y las **dieciséis** reglas de [`../02-Especificacion-Funcional/Reglas-De-Negocio/`](../02-Especificacion-Funcional/Reglas-De-Negocio/); los **nueve** invariantes de [`../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md`](../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md) §4; las **veintisiete** historias de [`../06-Backlog-Tecnico/historias-usuario/`](../06-Backlog-Tecnico/historias-usuario/); las **42** condiciones de [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md) §6.1; los **seis** NFR de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §8; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.19** §20 y §21
**Trazabilidad downstream:** [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md), [`Criterios-Validacion.md`](Criterios-Validacion.md), [`Plan-Pruebas.md`](Plan-Pruebas.md)

---

## Tabla de contenido

- [1. Cómo se lee este catálogo](#1-cómo-se-lee-este-catálogo)
- [2. Catálogo de casos de prueba](#2-catálogo-de-casos-de-prueba)
  - [2.1 Ciclo de vida de la cuenta](#21-ciclo-de-vida-de-la-cuenta)
  - [2.2 Credencial y admisibilidad](#22-credencial-y-admisibilidad)
  - [2.3 Trabajo, interpretación y envío](#23-trabajo-interpretación-y-envío)
  - [2.4 Acceso, alcance y desenlace](#24-acceso-alcance-y-desenlace)
  - [2.5 Pruebas de inspección estructural](#25-pruebas-de-inspección-estructural)
- [3. Recuento y verificación](#3-recuento-y-verificación)
- [4. Control de cambios](#4-control-de-cambios)

---

## 1. Cómo se lee este catálogo

Cada `TC-XX` declara ocho campos, según `Rules-Calidad-Y-Pruebas.md` §4.6: identificador y nombre, tipo, upstream cubierto, setup, pasos en Given-When-Then, salida esperada, salida observada y estado.

**Todas las filas de «Salida observada» dicen «Sin ejecutar» y todos los estados dicen `Pendiente`.** No hay sistema construido: el proyecto de código arranca en la etapa `a` y este catálogo se emite antes. Declarar cualquier otra cosa sería una afirmación sobre el estado del sistema sin evidencia.

**Vocabulario de este catálogo**, definido acá la primera vez que aparece y no redefinido después:

- **Nivel**: la posición de una prueba en la pirámide de [`Estrategia-Testing.md`](Estrategia-Testing.md) §1 — unitario o integración interna.
- **Fixture**: un constructor de entidad compartido, de los cuatro que declara [`Estrategia-Testing.md`](Estrategia-Testing.md) §5.
- **Prueba de inspección**: la que comprueba una propiedad estructural del proyecto de código y no una regla de negocio.
- **Resultado de interpretación**: el conjunto de piezas y observaciones que el consumidor le aporta al dominio ya producido. El dominio **no lo produce**: lo adopta.

**Un caso de prueba no es una historia.** Varias historias se ejercitan en el mismo `TC-XX` cuando comparten contrato de uso, setup y forma de verificación; la correspondencia completa está en [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2.

## 2. Catálogo de casos de prueba

### 2.1 Ciclo de vida de la cuenta

#### TC-01 — Alta-De-Alumno-Con-Cuenta-Pendiente

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-01`; `RN-02`; `INV-01`, `INV-08`; `US-01` |
| Setup | Ninguno. La operación constituye la entidad; la unicidad del correo llega declarada como comprobada por el consumidor |
| Pasos | Given los datos obligatorios del alta y la unicidad del correo declarada como comprobada, When se constituye el alumno, Then la cuenta nace `Pendiente`, sin credencial derivada y con papel `Alumno` |
| Salida esperada | Entidad constituida; estado de cuenta `Pendiente`; credencial derivada sin valor |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-02 — Rechazos-Del-Alta-De-Alumno

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-01`; `RN-01`, `RN-02`; `INV-01`, `INV-05`, `INV-08`; `US-02`, `US-03` |
| Setup | Ninguno |
| Pasos | Given un alta a la que le falta el correo, el nombre o el apellido, When se la invoca, Then se rechaza con `DATO_OBLIGATORIO_AUSENTE`. Given un alta sin la declaración de unicidad comprobada, Then `UNICIDAD_DE_CORREO_NO_VERIFICADA`. Given un alta que aporta credencial derivada, Then `CREDENCIAL_NO_ADMITIDA_EN_EL_ALTA`. Given un alta que pide un estado distinto de `Pendiente`, Then `ESTADO_INICIAL_NO_NEGOCIABLE`. Given un alta que pide papel `Administrador`, Then `PAPEL_DE_ADMINISTRADOR_FUERA_DE_ESTE_CAMINO` |
| Salida esperada | Cinco rechazos, uno por condición, sin entidad constituida en ninguno de los cinco |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-03 — Habilitar-Bloquear-Y-Rehabilitar-Con-Provisoria

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-02`, `CU-03`; `RN-16`, `RN-14`, `RN-06`; `INV-09`; `US-04`, `US-06` |
| Setup | Fixture de cuenta de alumno en los tres estados |
| Pasos | Given una cuenta `Pendiente` y la credencial provisoria ya derivada, When se la habilita, Then queda `Habilitado`, con la credencial fijada y **con la marca de cambio de contraseña pendiente puesta**. Given la habilitación invocada sin aportar la provisoria derivada, Then se rechaza con `HABILITACION_SIN_CREDENCIAL_PROVISORIA`. Given una cuenta `Habilitado`, When se la bloquea y se la rehabilita, Then las dos transiciones proceden. Given un par estado-operación no declarado, Then `TRANSICION_DE_CUENTA_NO_ADMITIDA` |
| Salida esperada | Tres transiciones aplicadas y dos rechazos con su condición; la marca puesta en cada habilitación |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04 — Baja-Con-Arrastre-Y-Confirmacion-Escrita

| Campo | Valor |
| --- | --- |
| Tipo | Integración interna |
| Cubre | `CU-02`; `RN-07`, `RN-04`; `INV-03`; `US-05` |
| Setup | Fixture de cuenta de alumno `Habilitado` más fixture de trabajo en los cuatro estados, los cuatro pertenecientes a esa cuenta |
| Pasos | Given una cuenta con trabajos en los cuatro estados y la confirmación escrita que coincide, When se da de baja, Then la cuenta y **los cuatro trabajos** se materializan como una sola unidad. Given la baja solicitada declarando que los trabajos se conservan, Then `BAJA_SIN_ARRASTRE_DE_TRABAJOS` |
| Salida esperada | Baja con arrastre a los cuatro estados, incluidos `Finalizado` y `Rechazado`; un rechazo con su condición |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-05 — Ninguna-Operacion-Alcanza-A-La-Cuenta-De-Administrador

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-02`, `CU-13`; `RN-01`, `RN-15`; `INV-05`, `INV-08`; `US-04`, `US-05` |
| Setup | Fixture de cuenta de administrador |
| Pasos | Given la cuenta con papel `Administrador`, When se intenta habilitarla, bloquearla, rehabilitarla, darla de baja y resetear su contraseña, Then las **cinco** invocaciones se rechazan con `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` y la cuenta queda `Habilitado` |
| Salida esperada | Cinco rechazos con el mismo código, y la cuenta intacta. Es la prueba de regresión de la familia de defectos que se abrió dos veces (`05` §9, segundo riesgo) |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06 — Configurar-El-Administrador-Y-Rechazar-El-Segundo

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-12`; `RN-01`, `RN-02`; `INV-05`, `INV-08`, `INV-01`; `US-24`, `US-25` |
| Setup | Ninguno para el camino feliz; la ausencia de administrador previo llega declarada por el consumidor |
| Pasos | Given la ausencia de administrador declarada, los datos obligatorios y la credencial ya derivada, When se configura, Then la cuenta nace `Habilitado`, con papel `Administrador` y con credencial. Given una segunda configuración, o una invocación sin la declaración de ausencia, Then `ADMINISTRADOR_YA_CONFIGURADO`. Given la configuración sin credencial derivada o con valor vacío, Then `CONFIGURACION_SIN_CREDENCIAL`. Given un estado pedido distinto de `Habilitado`, Then `ESTADO_INICIAL_NO_NEGOCIABLE`. Given la unicidad no declarada, Then `UNICIDAD_DE_CORREO_NO_VERIFICADA`. Given faltando un dato obligatorio, Then `DATO_OBLIGATORIO_AUSENTE` |
| Salida esperada | Una configuración aplicada y cinco rechazos, uno por condición de `CU-12` |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-07 — Reseteo-Que-Conserva-La-Cuenta-Y-Sus-Trabajos

| Campo | Valor |
| --- | --- |
| Tipo | Integración interna |
| Cubre | `CU-13`; `RN-12`, `RN-14`, `RN-15`; `INV-09`; `US-26` |
| Setup | Fixture de cuenta de alumno en los tres estados, cada una con trabajos en los cuatro estados |
| Pasos | Given una cuenta `Pendiente`, una `Habilitado` y una `Bloqueado`, cada una con sus trabajos, When se resetea su contraseña con la provisoria ya derivada, Then en las tres la credencial se reemplaza, **la marca de cambio de contraseña pendiente queda puesta**, el estado de cuenta **no cambia** y **ningún trabajo se pierde ni cambia de estado**. Given una solicitud armada como si fuera una baja, Then `RESETEO_CON_ARRASTRE_DE_TRABAJOS`. Given una invocación con el valor derivado vacío, Then `VALOR_DERIVADO_VACIO` |
| Salida esperada | Tres reseteos aplicados sobre los tres estados de cuenta, con el recuento de trabajos y sus estados idénticos antes y después; dos rechazos |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.2 Credencial y admisibilidad

#### TC-08 — Reemplazar-La-Credencial-Exigiendo-La-Vigente

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-03`; `RN-06`; `INV-06`; `US-07` |
| Setup | Fixture de cuenta de alumno `Habilitado` con credencial fijada |
| Pasos | Given la credencial vigente declarada verificada y un valor nuevo ya derivado, When se reemplaza, Then el valor se reemplaza y no se conserva historial. Given el reemplazo sin la declaración de verificación, Then `CREDENCIAL_VIGENTE_NO_VERIFICADA`. Given un valor derivado vacío, Then `VALOR_DERIVADO_VACIO`. Given una cuenta `Pendiente` o `Bloqueado`, Then `CUENTA_NO_HABILITADA_PARA_CREDENCIAL`. Given una fijación por primera vez sobre una credencial ya fijada, Then `CREDENCIAL_YA_FIJADA` |
| Salida esperada | Un reemplazo aplicado y cuatro rechazos, uno por condición de `CU-03` |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-09 — El-Cambio-Efectivo-Levanta-La-Marca

| Campo | Valor |
| --- | --- |
| Tipo | Integración interna |
| Cubre | `CU-03`, `CU-04`; `RN-13`, `RN-12`, `RN-16`; `INV-09`; `US-27` |
| Setup | Dos cuentas `Habilitado` con la marca puesta: una que la recibió al ser habilitada y otra al ser reseteada |
| Pasos | Given cualquiera de las dos cuentas con la marca puesta, When se reemplaza la credencial declarando verificada la vigente, Then el reemplazo procede **y la marca se levanta**. Given la misma cuenta antes del cambio, When se evalúa su admisibilidad, Then no es admisible con motivo `CAMBIO_DE_CONTRASENA_PENDIENTE`; después del cambio, Then es admisible |
| Salida esperada | La marca se levanta **únicamente** por el cambio efectuado por la propia cuenta, y el resultado es el mismo para los dos orígenes de la marca |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-10 — Admisibilidad-Como-Puerta-Unica

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04`; `RN-06`, `RN-13`, `RN-16`; `INV-06`, `INV-09`; `US-08` |
| Setup | Fixture de cuenta de alumno en los tres estados, con y sin la marca |
| Pasos | Given una cuenta `Pendiente`, When se evalúa la admisibilidad, Then devuelve no admisible con motivo `CUENTA_PENDIENTE`. Given `Bloqueado`, Then `CUENTA_BLOQUEADA`. Given `Habilitado` con la marca puesta, Then `CAMBIO_DE_CONTRASENA_PENDIENTE`. Given `Habilitado` sin la marca, Then admisible |
| Salida esperada | Tres motivos de resultado y un admisible. **La operación siempre devuelve resultado y nunca lanza**: los tres códigos son motivo de resultado y no rechazo (`03` §2.3) |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.3 Trabajo, interpretación y envío

#### TC-11 — Constituir-Un-Trabajo-Con-Dueno-Y-Texto-Original

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-05`; `RN-08`, `RN-03`; `INV-02`; `US-09` |
| Setup | Fixture de cuenta de alumno `Habilitado` |
| Pasos | Given un dueño, un nombre, una fecha declarada por el alumno y el texto original, When se constituye el trabajo, Then nace en `Borrador` con el texto **idéntico carácter por carácter** al recibido. Given la constitución sin dueño, Then `TRABAJO_SIN_DUENO`. Given sin nombre o sin fecha, Then `DATO_OBLIGATORIO_AUSENTE`. Given un texto declarado como corrección del que pegó el alumno, Then `TEXTO_ORIGINAL_ALTERADO` |
| Salida esperada | Un trabajo constituido con el texto íntegro, y tres rechazos. La comparación del texto es byte a byte |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-12 — Reedicion-Acotada-Al-Borrador

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-05`, `CU-09`; `RN-04`, `RN-10`; `INV-03`, `INV-07`; `US-10`, `US-19` |
| Setup | Fixture de trabajo en los cuatro estados, del mismo dueño |
| Pasos | Given un trabajo en `Borrador`, When se lo reedita, Then el texto nuevo lo reemplaza y **la interpretación anterior se descarta**. Given un trabajo en `Pendiente`, `Finalizado` o `Rechazado`, When se lo reedita, Then `REEDICION_FUERA_DE_BORRADOR` |
| Salida esperada | Una reedición aplicada y tres rechazos, uno por cada estado que no es `Borrador` |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-13 — Adoptar-El-Conjunto-De-Piezas-Con-Identidad-Posicional

| Campo | Valor |
| --- | --- |
| Tipo | Integración interna |
| Cubre | `CU-06`; `RN-09`; `US-11`, `US-12` |
| Setup | Fixture de trabajo en `Borrador` y el **resultado de interpretación** derivado del escenario `E-7` del intake §20: seis piezas, tres volumétricas —`Cilindro`, `Cubo`, `Ortoedro`— y tres planas —`Rectangulo`, `Cuadrado`, `Circulo`— |
| Pasos | Given ese conjunto, When se lo adopta, Then las seis piezas quedan con la posición que su figura ocupa en el conjunto raíz, **sin recalcularla**, y la familia plana o volumétrica se **deriva del tipo** y no se guarda. Given una posición repetida, negativa o fuera de rango, Then `POSICION_DE_PIEZA_INVALIDA`. Given la familia aportada como dato, Then `FAMILIA_DECLARADA_CONTRADICE_AL_TIPO`. Given un trabajo `Finalizado` o `Rechazado`, Then `RECONSTRUCCION_SOBRE_TRABAJO_TERMINAL` |
| Salida esperada | Seis piezas adoptadas con su posición y su familia derivada; tres rechazos |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-14 — La-Posicion-De-Una-Figura-No-Adoptada-Queda-Reservada

| Campo | Valor |
| --- | --- |
| Tipo | Integración interna |
| Cubre | `CU-06`, `CU-07`; `RN-09`; `US-11`, `US-14` |
| Setup | Resultado de interpretación derivado del escenario `E-5` del intake §20: dos figuras, la del índice 0 válida y la del índice 1 con tipo fuera del conjunto conocido |
| Pasos | Given ese conjunto, When se lo adopta, Then la pieza del índice 0 **se adopta** y la del índice 1 no, con `TIPO_DE_PIEZA_DESCONOCIDO`; **la posición 1 queda reservada** y la 0 conserva la suya, sin renumerar. When se registra la observación de error sobre la posición 1, Then se adopta, porque una posición reservada **sí pertenece al rango** |
| Salida esperada | Una pieza adoptada, una posición reservada, y la observación de error aceptada sobre esa posición. Es el caso insignia de `RN-09` |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-15 — Advertencia-Con-El-Par-De-Valores

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-07`; `US-13` |
| Setup | Resultados de interpretación derivados de los escenarios `E-3` y `E-4` del intake §20 |
| Pasos | Given la advertencia de área de `E-3` con declarado 36.00 y derivado 54.00, When se la registra, Then se adopta con **los dos valores**. Given la misma advertencia emitida con un solo número, Then `ADVERTENCIA_SIN_LOS_DOS_VALORES`. Given el resultado de `E-4`, que trae **cero observaciones**, When se lo adopta, Then el trabajo queda sin ninguna |
| Salida esperada | Una advertencia con su par, un rechazo, y un conjunto vacío adoptado sin error. `E-3` y `E-4` son el mismo cubo de lado 3 emitido por los dos ejemplos de la cátedra, y el contraste es lo que prueba que la verificación mide la geometría y no la forma del texto |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-16 — Error-De-Validacion-Con-Posicion-Y-Campo

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-07`; `RN-09`; `US-14` |
| Setup | Resultado de interpretación derivado del escenario `E-5` del intake §20 |
| Pasos | Given una observación de especie error de validación atribuible a una figura, con **índice de figura 1** y **campo `Tipo`**, When se la registra, Then se adopta. Given la misma sin posición ni campo, Then `ERROR_SIN_UBICACION`. Given una observación que designa una posición fuera del rango del conjunto raíz interpretado, Then `OBSERVACION_SOBRE_PIEZA_INEXISTENTE`. Given una especie que no es `Advertencia` ni error de validación, Then `ESPECIE_DE_OBSERVACION_DESCONOCIDA` |
| Salida esperada | Una observación adoptada con índice 1 y campo `Tipo`, y tres rechazos. El índice **1 y no 0** es lo que prueba que la ubicación se calcula |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-17 — Envio-Que-Verifica-Y-Pasa-A-Pendiente

| Campo | Valor |
| --- | --- |
| Tipo | Integración interna |
| Cubre | `CU-08`; `RN-05`; `INV-04`; `US-15` |
| Setup | Trabajos en `Borrador` con los resultados de interpretación derivados de los escenarios `E-1`, `E-2`, `E-4` y `E-6` del intake §20 |
| Pasos | Given el trabajo de `E-1`, con **3 piezas y 2 advertencias** y sin errores, When se lo envía, Then pasa a `Pendiente`. Given el de `E-2`, con 1 pieza y 1 advertencia de volumen, Then pasa a `Pendiente` **con la advertencia asociada**. Given el de `E-4`, con cero observaciones, Then pasa a `Pendiente`. Given el de `E-6`, que se interpreta y produce a lo sumo una advertencia, Then pasa a `Pendiente`. Given un trabajo ya en `Pendiente`, When se lo reenvía, Then `ENVIO_FUERA_DE_BORRADOR`. Given un envío antes de incorporar el resultado de la interpretación, Then `ENVIO_SIN_INTERPRETACION`. Given un desenlace pedido por esta vía, Then `DESENLACE_NO_ADMITIDO_EN_ESTE_CONTRATO` |
| Salida esperada | Cuatro envíos que pasan a `Pendiente` —las advertencias **no** lo impiden— y tres rechazos |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-18 — Envio-Que-No-Verifica-Y-Queda-En-Borrador

| Campo | Valor |
| --- | --- |
| Tipo | Integración interna |
| Cubre | `CU-08`; `RN-05`, `RN-08`; `INV-04`; `US-16` |
| Setup | Trabajos en `Borrador` con los resultados de interpretación derivados de los escenarios `E-5` y `E-8` del intake §20 |
| Pasos | Given el trabajo de `E-5`, cuyo resultado trae una observación de severidad `Error`, When se lo envía, Then **queda en `Borrador`** con su texto conservado y no pasa a `Pendiente`. Given el trabajo de `E-8`, cuya dimensión no legible **el intake resuelve como error y no como advertencia** [DECISIÓN 2026-08-09, §20.E-8 punto 5], Then **queda en `Borrador`** con el mensaje localizado por índice de figura y campo |
| Salida esperada | Dos envíos que no transicionan, con el texto original intacto en los dos. `E-8` es el modo de falla que el intake declara **más probable** de todos los escenarios |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-19 — Ninguna-Transicion-Sale-De-Un-Estado-Terminal

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-08`, `CU-10`, `CU-06`; `RN-10`; `INV-07`; `US-17` |
| Setup | Fixture de trabajo en `Finalizado` y en `Rechazado` |
| Pasos | Given un trabajo en cualquiera de los dos estados terminales, When se pide enviar, reeditar, reconstruir el conjunto de piezas o aplicar un desenlace nuevo, Then todas las invocaciones se rechazan y **ni el estado ni el contenido cambian** |
| Salida esperada | Rechazo en los dos estados y en las cuatro operaciones, con `TRANSICION_DESDE_ESTADO_TERMINAL`, `REEDICION_FUERA_DE_BORRADOR` y `RECONSTRUCCION_SOBRE_TRABAJO_TERMINAL` según la operación |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.4 Acceso, alcance y desenlace

#### TC-20 — Trabajo-Ajeno-Indistinguible-De-Inexistente

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-09`; `RN-03`, `RN-04`; `INV-02`, `INV-03`; `US-18`, `US-19` |
| Setup | Dos cuentas de alumno y un trabajo de cada una, en los cuatro estados |
| Pasos | Given un trabajo de otro alumno cuyo identificador el solicitante conoce, y un identificador inexistente, When se resuelve el acceso, Then los **dos** devuelven `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`, con resultado idéntico. Given un trabajo propio fuera de `Borrador`, When se consulta reeditar o eliminar, Then `OPERACION_FUERA_DE_BORRADOR`; When se consulta ver, Then procede en los cuatro estados. Given una operación fuera del conjunto declarado, Then `OPERACION_DESCONOCIDA` |
| Salida esperada | Dos resultados idénticos para el ajeno y el inexistente —comparados campo por campo—, la acotación al borrador de reeditar y eliminar, y ver admitido en los cuatro estados |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-21 — Alcance-Del-Administrador-Sin-Los-Borradores

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-11`; `RN-11`, `RN-04`, `RN-01`; `INV-05`; `US-22`, `US-23` |
| Setup | Fixture de trabajo en los cuatro estados |
| Pasos | Given un trabajo en `Borrador`, When el administrador consulta su alcance, Then `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR`. Given los otros **tres** estados, Then entran en su alcance y **admiten eliminación**, incluidos los dos terminales. Given un papel que no es `Administrador`, Then `ALCANCE_SIN_PAPEL_DE_ADMINISTRADOR`. Given una operación fuera del conjunto declarado, Then `OPERACION_DESCONOCIDA` |
| Salida esperada | Un borrador excluido, tres estados con eliminación admitida, y dos motivos de resultado |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-22 — Desenlace-Exclusivo-Y-Terminal

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-10`; `RN-10`, `RN-11`, `RN-01`; `INV-07`, `INV-05`; `US-20`, `US-21` |
| Setup | Fixture de trabajo en los cuatro estados y fixture de cuenta de administrador |
| Pasos | Given un trabajo en `Pendiente` y el papel `Administrador`, When se lo aprueba, Then pasa a `Finalizado`; When se lo rechaza, Then pasa a `Rechazado`; el comentario es **opcional** en los dos. Given un trabajo en otro estado, Then `DESENLACE_FUERA_DE_PENDIENTE`. Given un papel que no es `Administrador`, aun sobre un trabajo propio, Then `DESENLACE_SIN_PAPEL_DE_ADMINISTRADOR`. Given un desenlace que no es aprobar ni rechazar, Then `DESENLACE_DESCONOCIDO`. Given un trabajo terminal, Then `TRANSICION_DESDE_ESTADO_TERMINAL` |
| Salida esperada | Dos desenlaces aplicados, con y sin comentario, y cuatro rechazos |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.5 Pruebas de inspección estructural

#### TC-23 — Catalogo-De-Condiciones-En-Las-Dos-Direcciones

| Campo | Valor |
| --- | --- |
| Tipo | Prueba de inspección, nivel unitario |
| Cubre | NFR «Cobertura del catálogo de condiciones» de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §8; `BT-08` |
| Setup | El conjunto de códigos que la biblioteca puede emitir, y el catálogo de [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md) §6.2 |
| Pasos | Given los dos conjuntos, When se los compara **en las dos direcciones**, Then no hay ningún código emitido que falte en el catálogo, ni ninguna de las **42** condiciones del catálogo sin al menos una prueba que la alcance |
| Salida esperada | **42 de 42** alcanzadas y **0** emitidas fuera del catálogo. Los **cinco** identificadores retirados —tres por renombre y dos por imposibilidad de su causa (`03` §6.1)— **no se reciclan** y su aparición es una falla |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-24 — Cero-Dependencias-Salientes

| Campo | Valor |
| --- | --- |
| Tipo | Prueba de inspección, nivel unitario |
| Cubre | NFR «Dependencias salientes» de `05` §8; `BT-04`; `QG-04` de [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3 |
| Setup | El archivo de proyecto de la biblioteca |
| Pasos | Given el archivo de proyecto, When se lo inspecciona, Then declara **0** referencias a otros proyectos de código del producto y **0** a bibliotecas de persistencia, transporte o serialización |
| Salida esperada | Dos recuentos en 0. Es la propiedad que justifica el estilo entero y el intake la declara como condición de la capa (§17.1.P.1) |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-25 — El-Dominio-No-Lee-El-Reloj-Ni-El-Conjunto

| Campo | Valor |
| --- | --- |
| Tipo | Prueba de inspección, nivel unitario |
| Cubre | [`ADR-06`](../05-Arquitectura-Tecnica/Adrs/ADR-06-El-Dominio-No-Lee-El-Reloj-Ni-El-Conjunto.md); `BT-09`; `05` §7, filas de configuración y de zona horaria |
| Setup | El código de las operaciones públicas |
| Pasos | Given todas las operaciones, When se las inspecciona, Then ninguna obtiene el momento por su cuenta ni consulta conjuntos de entidades. When se corre la batería completa sin fijar el reloj del entorno, Then el resultado es idéntico en dos ejecuciones consecutivas |
| Salida esperada | Cero ocurrencias de lectura de reloj y de consulta de conjunto; dos ejecuciones con resultado idéntico |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-26 — Los-Nueve-Invariantes-Ejercidos-Sin-Dobles

| Campo | Valor |
| --- | --- |
| Tipo | Prueba de inspección sobre la matriz, nivel unitario |
| Cubre | NFR «Ejercicio de los invariantes» de `05` §8; `BT-14`; `INV-01` a `INV-09` |
| Setup | La matriz de §5 de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) |
| Pasos | Given los **nueve** invariantes, When se recorre la matriz, Then cada uno tiene al menos una prueba que **verifica su violación rechazada**, y ninguna de esas pruebas usa dobles |
| Salida esperada | **9 de 9** con prueba de violación rechazada y **0** dobles. Es la mitigación declarada del riesgo de que un invariante se ejerza en un componente y no en otro |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-27 — Ninguna-Condicion-Prevista-Viaja-Como-Excepcion

| Campo | Valor |
| --- | --- |
| Tipo | Prueba de inspección, nivel unitario |
| Cubre | [`ADR-02`](../05-Arquitectura-Tecnica/Adrs/ADR-02-Superficie-Publica-De-Guardas-Y-Resultados-Tipados.md); `BT-07`; `QG-08` |
| Setup | Las invocaciones que producen cada una de las **42** condiciones |
| Pasos | Given cada condición del catálogo, When se la provoca, Then el resultado llega como **valor de retorno tipado** con su código, y **ninguna** invocación lanza. When se invoca con un argumento nulo donde el contrato exige valor, Then sí se lanza: es defecto de programación del consumidor y no una regla de negocio |
| Salida esperada | 42 rechazos por valor de retorno y 0 excepciones de negocio; la distinción con el defecto de programación verificada aparte |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

## 3. Recuento y verificación

| Magnitud | Valor |
| --- | --- |
| Casos de prueba declarados | **27**, `TC-01` a `TC-27`, serie contigua |
| Casos de uso cubiertos | **13 de 13** |
| Reglas de negocio cubiertas | **16 de 16** |
| Invariantes cubiertos | **9 de 9** |
| Historias de usuario cubiertas | **27 de 27** |
| Condiciones del catálogo alcanzadas | **42 de 42**, agregadas en `TC-23` y desplegadas en los casos funcionales |
| NFR con caso de prueba asociado | **3 de 6**: dependencias salientes (`TC-24`), cobertura del catálogo (`TC-23`) y ejercicio de los invariantes (`TC-26`). Los otros tres —tiempo de la batería, cobertura de líneas y ramas, y advertencias de construcción— **se miden en el pipeline y no por un caso de prueba**, y su tratamiento está en [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §3 |
| Escenarios del intake §20 usados como fixture | **8 de 8**, en `TC-13`, `TC-14`, `TC-15`, `TC-17` y `TC-18` |
| Casos de prueba sin upstream declarado | **0**. Cada `TC-XX` declara al menos un `CU-XX`, `RN-XX`, `INV-XX` o NFR |

**Verificación de la cobertura de los ocho escenarios, uno por uno:** `E-1` y `E-2` en `TC-17`; `E-3` y `E-4` en `TC-15` y `E-3` además en `TC-17` por vía de `E-1`; `E-5` en `TC-14`, `TC-16` y `TC-18`; `E-6` en `TC-17`; `E-7` en `TC-13`; `E-8` en `TC-18`. Ninguno queda sin caso de prueba y ninguno se sustituye por datos sintéticos.

## 4. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara **veintisiete** casos de prueba referenciales, `TC-01` a `TC-27`, cada uno con tipo, upstream por identificador, setup, pasos en Given-When-Then, salida esperada, salida observada y estado. Los veintidós primeros cubren los **trece** casos de uso, las **dieciséis** reglas y las **veintisiete** historias; los cinco últimos son pruebas de inspección estructural sobre el catálogo de **42** condiciones, las dependencias salientes, la ausencia de lectura de reloj, el ejercicio de los **nueve** invariantes y la forma de la superficie pública. Los **ocho** escenarios del intake §20 se usan como fixture con su resultado de interpretación y no con su texto, porque el dominio adopta la interpretación y no la produce. Todas las salidas observadas dicen «Sin ejecutar» y todos los estados `Pendiente`: no hay sistema construido y afirmar otra cosa sería una afirmación sin evidencia. |
