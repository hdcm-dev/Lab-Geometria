# Matriz de cobertura de pruebas — GeometriaFactory-Contracts

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** Matriz-Cobertura-Pruebas.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) 1.0; [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) 1.6 §3, §5 y §6; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.1 §3.1, §8, §10.2 y §10.3
**Trazabilidad downstream:** [`Criterios-Validacion.md`](Criterios-Validacion.md), [`Definition-Of-Done.md`](Definition-Of-Done.md); `09-Devops`

---

## Tabla de contenido

- [1. Propósito y alcance](#1-propósito-y-alcance)
- [2. Trazabilidad CU ↔ tests](#2-trazabilidad-cu--tests)
- [3. Trazabilidad NFR ↔ tests](#3-trazabilidad-nfr--tests)
- [4. Trazabilidad RN ↔ tests](#4-trazabilidad-rn--tests)
- [5. Trazabilidad restricción transversal ↔ tests](#5-trazabilidad-restricción-transversal--tests)
- [6. Cobertura por familia de tipos](#6-cobertura-por-familia-de-tipos)
- [7. Huecos identificados](#7-huecos-identificados)
- [8. Control de cambios](#8-control-de-cambios)

---

## 1. Propósito y alcance

Relaciona los **ocho** contratos de uso, los **siete** NFR, las **dieciséis** reglas de negocio del producto y las **once** restricciones transversales con los **veintidós** casos de prueba de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md), y declara la cobertura por familia de tipos.

**Ninguna columna de estado afirma que algo esté verde.** El ensamblado no está construido.

Esta matriz **agrega una cuarta tabla** a las tres que `Rules-Calidad-Y-Pruebas.md` §4.5 exige: la de restricción transversal contra prueba. El motivo es que en este proyecto de código las once `RT-XX` son la forma en que se expresa lo que decide —qué cruza la frontera y qué no— y `05` §10.2 ya las traza contra la decisión de arquitectura que las materializa; sin esta tabla, esa cadena se cortaría antes de llegar a la verificación.

## 2. Trazabilidad CU ↔ tests

Ocho filas, una por contrato de uso de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3. Ninguna se agrupa.

| CU | Criterio Given-When-Then principal | Test | Tipo | Estado |
| --- | --- | --- | --- | --- |
| CU-01 Contrato de canje de credenciales y sesión | Given un canje válido, When se lo ejerce, Then la respuesta de sesión llega con **cuatro** campos y ninguno más; con la cuenta no habilitada o con la marca puesta, Then llega **respuesta de error** y no sesión | `TC-01`, `TC-02` | Integración e inspección | `Pendiente` |
| CU-02 Contrato de administración de cuentas | Given el registro, el listado, el cambio de situación y la baja con confirmación escrita, When se los ejerce, Then cada uno transporta lo que declara; habilitar **devuelve la provisoria producida** | `TC-03`, `TC-04`, `TC-05`, `TC-06` | Integración e inspección | `Pendiente` |
| CU-03 Contrato de carga y edición del trabajo | Given el envío con el texto original, When se lo envía y se pide el detalle, Then el texto vuelve íntegro como **una sola cadena**; la solicitud de eliminación es **única** para los dos papeles | `TC-07`, `TC-08` | Integración e inspección | `Pendiente` |
| CU-04 Contrato de listado de trabajos | Given la proyección de listado, When se la inspecciona, Then no lleva texto original, ni componentes, ni comentario; el alcance cambia según el papel y excluye los `Borrador` para el administrador | `TC-09`, `TC-10`, `TC-11` | Inspección e integración | `Pendiente` |
| CU-05 Contrato de detalle del trabajo interpretado | Given el detalle, When se lo pide, Then trae piezas con sus componentes, el texto original, las observaciones con severidad y **par de valores**, y el comentario **como bloque propio** | `TC-12`, `TC-13`, `TC-14` | Integración e inspección | `Pendiente` |
| CU-06 Contrato de respuesta de error | Given el tipo de error, When se inspecciona su superficie, Then declara **cuatro** campos y **0** capaces de filtrar; el conjunto cerrado tiene **quince** códigos vivos y `CONTRATO_ERROR_NO_CLASIFICADO` lo cierra | `TC-15`, `TC-16`, `TC-17`, `TC-02` | Inspección e integración | `Pendiente` |
| CU-07 Contrato de desenlace de la revisión | Given el desenlace, When se lo inspecciona, Then es un conjunto cerrado de **dos** valores con comentario opcional, y **ningún** tipo permite salir de un estado terminal | `TC-18`, `TC-14` | Inspección e integración | `Pendiente` |
| CU-08 Contrato de reseteo y cambio obligatorio | Given la solicitud de reseteo, When se la inspecciona, Then lleva **un solo campo** y **0** de contraseña; el resultado declara la situación conservada, el cambio pendiente y la provisoria producida | `TC-19`, `TC-02` | Inspección e integración | `Pendiente` |

**Ocho de ocho contratos de uso con al menos un caso de prueba.** Ninguno queda huérfano.

## 3. Trazabilidad NFR ↔ tests

Siete filas, una por cada NFR de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §8.

| NFR | Objetivo numérico | Test | Herramienta de medición | Estado |
| --- | --- | --- | --- | --- |
| Tipos ejercitados por prueba de integración | **100 %** de los tipos, con al menos una prueba cada uno **[ASUNCIÓN del intake §17.4.P.6]** | `TC-21` | Matriz de §6, sobre la batería que golpea el servicio real | `Pendiente` |
| Carga útil del listado | **0** ocurrencias del texto original, **0** de componentes de pieza y **0** del comentario **[ASUNCIÓN derivada del intake §17.4.P.10]** | `TC-09` | Inspección de la superficie de la familia de listado | `Pendiente` |
| Referencias hacia `GeometriaFactory-Domain` | Exactamente **0** | `TC-20` | Inspección del archivo de proyecto, más la comprobación reproducible de `03` §3 | `Pendiente` |
| Campos capaces de transportar una dirección de servicio, una ruta de datos o un secreto | Exactamente **0** en los tipos de las **ocho** familias | `TC-15`, `TC-01`, `TC-04` | Prueba de inspección de superficie pública, que es `CA-01` de `CU-06` | `Pendiente` |
| Códigos de error del conjunto cerrado | Exactamente **15** vivos y **0** producidos fuera del conjunto | `TC-16` | Prueba de inspección del conjunto cerrado, que es `CA-09` de `CU-06` | `Pendiente` |
| Campos de la respuesta de sesión | Exactamente **4**, y **0** que transporten una condición que impida operar | `TC-01`, `TC-02` | Inspección de la superficie pública, restricción `RT-10` | `Pendiente` |
| Advertencias de construcción | Exactamente **0** | **Ninguno**: gate `QG-01`, etapa `build` del pipeline | Etapa `build` | `Pendiente` |

**Los dos valores rotulados [ASUNCIÓN] se citan con su rótulo y no se convierten en compromiso.** Su confirmación está pendiente del Product Owner en el intake §22, asunción `A-4`, y su conversión en trabajo es `BT-18`. Hasta entonces sus gates son **condicionados** ([`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3.1).

**Seis de los siete NFR tienen caso de prueba.** El séptimo es una medición del pipeline y no un comportamiento del ensamblado.

## 4. Trazabilidad RN ↔ tests

**Este proyecto de código no redacta ninguna regla de negocio**: es el caso que `Rules-Especificacion-Funcional.md` §2.1 nombra como proyecto de código sin estado ni invariantes, y así lo declara [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §5. Las **dieciséis** reglas viven en `GeometriaFactory-Domain`.

Lo que esta tabla declara es **qué transporta este ensamblado de cada regla, y qué caso de prueba lo verifica**. La columna del medio es la de `05` §10.3, resumida; esta matriz no la redefine. Dieciséis filas, ninguna agrupada.

| RN | Qué transporta este proyecto de código de ella | Test | Estado |
| --- | --- | --- | --- |
| RN-01 Administrador único | El rechazo de configurar un segundo administrador, con código propio | `TC-05` | `Pendiente` |
| RN-02 Correo único | El rechazo del registro con correo ya usado, con código propio | `TC-03` | `Pendiente` |
| RN-03 Trabajo ajeno indistinguible de inexistente | Un solo código y un solo texto para los dos casos | `TC-08`, `TC-10` | `Pendiente` |
| RN-04 Eliminación acotada | La solicitud **única** de eliminación para los dos papeles, y el rechazo por estado en el camino del alumno | `TC-08` | `Pendiente` |
| RN-05 Sin errores de validación no hay estado `Pendiente` | El estado resultante del envío y las observaciones con su especie | `TC-07`, `TC-17` | `Pendiente` |
| RN-06 Cuenta `Pendiente` o `Bloqueado` sin acceso | El motivo de la situación, **como respuesta de error y no como campo de sesión** | `TC-02` | `Pendiente` |
| RN-07 Baja con arrastre y confirmación escrita | La confirmación escrita como campo de la solicitud, y su rechazo si no coincide | `TC-06` | `Pendiente` |
| RN-08 Texto original íntegro | El texto como cadena no interpretada, **en las dos direcciones** | `TC-07`, `TC-12` | `Pendiente` |
| RN-09 Observación con posición y campo | El índice de figura y el campo señalado en la observación del detalle | `TC-17`, `TC-13` | `Pendiente` |
| RN-10 Desenlace exclusivo y terminal | El desenlace como conjunto cerrado de dos valores, el estado terminal, y dos códigos de rechazo propios | `TC-18` | `Pendiente` |
| RN-11 El administrador no ve los borradores | El alcance del listado según el papel, y la causa ampliada del código de no encontrado | `TC-10`, `TC-08` | `Pendiente` |
| RN-12 El reseteo conserva la cuenta y sus trabajos | Un resultado que **no declara ningún campo por el que los trabajos se pierdan** | `TC-19` | `Pendiente` |
| RN-13 Cambio forzado antes de toda otra capacidad | Un **solo** código para todas las operaciones bloqueadas | `TC-02`, `TC-16` | `Pendiente` |
| RN-14 La provisoria la produce el sistema | Una solicitud de reseteo **sin campo de contraseña**, y un resultado que lleva la provisoria producida | `TC-19` | `Pendiente` |
| RN-15 Resetear no exige cuenta habilitada | La **ausencia** de un código por cuenta no habilitada: esa causa no existe y no recibe código | `TC-16`, `TC-19` | `Pendiente` |
| RN-16 Habilitar produce la provisoria | El mismo código para los **dos orígenes** de la marca, y la ausencia de todo tipo de establecimiento anónimo de contraseña | `TC-02`, `TC-05` | `Pendiente` |

**Dieciséis de dieciséis reglas con al menos un caso de prueba que verifica lo que este proyecto de código transporta de ellas.** Ninguna se verifica acá **como regla**: eso ocurre en `GeometriaFactory-Domain`, y confundir las dos cosas sería atribuirle a un ensamblado de tipos planos una capacidad que no tiene.

## 5. Trazabilidad restricción transversal ↔ tests

Once filas, `RT-01` a `RT-11`, las de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §6. Ninguna se agrupa.

| RT | Qué exige, en una línea | Test | Estado |
| --- | --- | --- | --- |
| RT-01 | Ningún tipo lleva hash de contraseña, clave de firma ni dirección de servicio interno | `TC-15`, `TC-01`, `TC-04`, `TC-19` | `Pendiente` |
| RT-02 | La respuesta de error lleva texto neutro y, cuando corresponde, índice de figura y campo | `TC-15`, `TC-17` | `Pendiente` |
| RT-03 | El texto original viaja como cadena, sin interpretarse | `TC-07`, `TC-12` | `Pendiente` |
| RT-04 | La proyección de listado no lleva texto original, ni componentes, ni comentario | `TC-09` | `Pendiente` |
| RT-05 | El ensamblado no declara ninguna referencia hacia `GeometriaFactory-Domain` | `TC-20` | `Pendiente` |
| RT-06 | Un cambio incompatible obliga al despliegue conjunto de las dos unidades | **Ninguno**: es una regla operativa, no una propiedad del ensamblado. Se verifica en la revisión del pull request de la etapa y en `09-Devops` (gate `QG-08`) | `Pendiente` |
| RT-07 | Sin pruebas propias: el gate equivalente es el 100 % de tipos ejercitados por integración | `TC-21` | `Pendiente` |
| RT-08 | Cuatro estados del trabajo, dos terminales, y ningún tipo que permita salir de ellos | `TC-18`, `TC-07` | `Pendiente` |
| RT-09 | El comentario viaja como bloque propio y nunca como observación | `TC-14` | `Pendiente` |
| RT-10 | Ninguna condición que impida operar viaja como campo de la respuesta de sesión | `TC-01`, `TC-02` | `Pendiente` |
| RT-11 | Ningún tipo habilita a que el navegador invoque el servicio de datos | `TC-22` | `Pendiente` |

**Diez de once restricciones con caso de prueba.** `RT-06` es la única sin uno, y **su ausencia está declarada y no es un hueco**: una regla de despliegue conjunto no se comprueba leyendo la superficie ni golpeando el servicio, sino mirando qué se publicó. Su verificación pertenece a `09-Devops`.

## 6. Cobertura por familia de tipos

La partición es por las **ocho** familias de `05` §3.1, que son sus componentes. **No hay cobertura por líneas**, y el motivo está en [`Estrategia-Testing.md`](Estrategia-Testing.md) §2: el intake declara «cobertura mínima: no aplica como gate propio».

| Familia de tipos | Tipos ejercitados por integración | Recuento de superficie propio | Umbral | Estado |
| --- | --- | --- | --- | --- |
| Familia de sesión | Sin medir | Campos de la respuesta de sesión | 100 % ejercitados; exactamente **4** campos | `Pendiente` |
| Familia de cuentas | Sin medir | Campos capaces de filtrar | 100 %; **0** | `Pendiente` |
| Familia de trabajo | Sin medir | Tipo del campo de texto original | 100 %; **una** cadena | `Pendiente` |
| Familia de listado | Sin medir | Ocurrencias prohibidas en la proyección | 100 %; **0**, **0** y **0** | `Pendiente` |
| Familia de detalle | Sin medir | Campos compartidos entre comentario y observaciones | 100 %; **0** | `Pendiente` |
| Familia de desenlace | Sin medir | Valores del conjunto cerrado del desenlace | 100 %; **2** | `Pendiente` |
| Familia de reseteo | Sin medir | Campos de la solicitud | 100 %; **1** | `Pendiente` |
| Familia de error | Sin medir | Campos del tipo, y códigos vivos del conjunto cerrado | 100 %; **4** campos y **15** códigos | `Pendiente` |
| **Ensamblado completo** | Sin medir | Referencias hacia el dominio, y advertencias de construcción | **100 %** [ASUNCIÓN]; **0** y **0** | `Pendiente` |

**«Sin medir» y no «0 %».** No hay ensamblado construido: un cero sería una afirmación falsa sobre el estado del sistema.

**No hay columna de mutation score**, y su ausencia se declara: no hay lógica que mutar, por lo que [`Estrategia-Testing.md`](Estrategia-Testing.md) §2 desarrolla.

## 7. Huecos identificados

| Hueco | Consecuencia | Plan de remediación |
| --- | --- | --- |
| **La batería de integración no vive en este proyecto de código** sino en `GeometriaFactory-Api`, de nivel topológico 3 | Ningún tipo se puede ejercitar de verdad hasta que ese proyecto de código exista. Entre tanto, lo único verificable son las inspecciones de superficie | Es una dependencia declarada por el intake §17.4.P.6 y no una omisión. Las inspecciones de superficie —`TC-01`, `TC-09`, `TC-14` a `TC-16`, `TC-18` a `TC-22`— **sí corren desde la etapa `c`** y cubren los cinco gates de superficie |
| **Los dos valores rotulados [ASUNCIÓN]** —tipos ejercitados y carga útil del listado— siguen sin confirmar | `QG-05` y `QG-06` son condicionados y no bloquean la fusión | `BT-18`, antes de fijar la puerta en `09-Devops` |
| **`RT-06` no tiene caso de prueba** | El despliegue conjunto ante un cambio incompatible depende de la disciplina del pull request de la etapa | Gate `QG-08` y la materialización en `09-Devops`. Su detección tardía está catalogada como `DXC-08` |
| **La zona horaria y la precisión del campo de momento no están decididas** (`05` §11 `PA-02`, `BT-05`) | Ningún caso de prueba puede afirmar nada sobre el formato de ese campo | `BT-05`, antes de cerrar la etapa `c` |
| **Ninguna fila `VER-XX` y ninguna matriz de sensado de deriva** | Este proyecto de código no ejecutó la Fase B2 —`requiere_maqueta` es false— y no tiene categoría 10 emitida | `Rules-Calidad-Y-Pruebas.md` §2.1 omite la matriz para ese caso. Ver [`README.md`](README.md) §3 |

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara las tres tablas obligatorias —**ocho** filas de contrato de uso, **siete** de NFR y **dieciséis** de regla de negocio, ninguna agrupada— y una cuarta de **once** restricciones transversales, con la constancia de que la tabla de reglas declara **qué transporta** este ensamblado de cada una y no que las verifique como reglas. Declara la cobertura por las **ocho** familias de tipos con «Sin medir» en lugar de cero, la ausencia de cobertura por líneas y de mutation score con su fundamento, los dos valores rotulados **[ASUNCIÓN]** con su rótulo, y **cinco** huecos con su plan, incluida la dependencia de la batería de integración respecto de un proyecto de código de nivel 3. |
