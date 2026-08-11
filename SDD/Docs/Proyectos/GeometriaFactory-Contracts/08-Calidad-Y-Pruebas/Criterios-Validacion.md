# Criterios de validación — GeometriaFactory-Contracts

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** Criterios-Validacion.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) 1.0; [`Estrategia-Calidad.md`](Estrategia-Calidad.md) 1.0 §3; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.1 §8; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.19** §15, §17.4.P.3, §17.4.P.6, §17.4.P.8 y §22
**Trazabilidad downstream:** [`Definition-Of-Done.md`](Definition-Of-Done.md); `09-Devops`

---

## Tabla de contenido

- [1. Propósito](#1-propósito)
- [2. Criterios funcionales](#2-criterios-funcionales)
- [3. Criterios no funcionales](#3-criterios-no-funcionales)
- [4. Criterios de regresión y de compatibilidad](#4-criterios-de-regresión-y-de-compatibilidad)
- [5. Criterios de calidad de código](#5-criterios-de-calidad-de-código)
- [6. Excepciones documentadas](#6-excepciones-documentadas)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Propósito

Define qué significa que `GeometriaFactory-Contracts` está **validado**. Como el ensamblado no se publica en ningún repositorio de paquetes y no es unidad de despliegue —viaja dentro de los **dos** procesos desplegables del producto—, «validado» quiere decir **que la frontera declarada es la que el sistema construido respeta**.

El momento en que se aplican estos criterios es el **punto de control de cada etapa**. No hay fecha de liberación que preparar.

## 2. Criterios funcionales

| Id | Criterio | Cómo se comprueba | Umbral |
| --- | --- | --- | --- |
| CV-01 | Los **ocho** contratos de uso tienen al menos un caso de prueba en verde, y cada criterio de aceptación declarado en su `§8` está cubierto | [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2 | **8 de 8** |
| CV-02 | Las **once** restricciones transversales tienen caso de prueba, salvo `RT-06`, cuya verificación pertenece a `09-Devops` y está declarada como tal | Matriz §5 | **10 de 11** con caso de prueba, y la undécima con su verificación declarada |
| CV-03 | Las **veintidós** historias tienen su caso de prueba; `US-10` queda declarada fuera del tramo comprometido | Matriz §2 cruzada con [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §3 | **22 de 22**, **21** comprometidas |
| CV-04 | Las **dieciséis** reglas de negocio tienen un caso de prueba que verifica **qué transporta este ensamblado de ellas** | Matriz §4 | **16 de 16** |
| CV-05 | El conjunto cerrado tiene **15** códigos vivos sobre **18** identificadores emitidos, y ninguno de los **3** retirados se recicla | `TC-16` | 15, 18 y **0** reciclados |
| CV-06 | `CONTRATO_ERROR_NO_CLASIFICADO` cierra el conjunto: no hay camino por el que un fallo llegue sin representación | `TC-16` | **0** fallos sin representación |
| CV-07 | Los **ocho** escenarios del intake §20 están alcanzados en la parte que a este proyecto de código le toca, **sin sustituirlos por datos sintéticos** | Verificación uno por uno de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) §3 | **8 de 8** |

## 3. Criterios no funcionales

Uno por cada NFR de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §8.

| Id | Criterio | Umbral | Cómo se mide | Carácter |
| --- | --- | --- | --- | --- |
| CV-08 | Todos los tipos de transferencia están ejercitados por al menos una prueba de integración contra el servicio real | **100 %** **[ASUNCIÓN del intake §17.4.P.6, asunción `A-4` de §22]** | `TC-21`, sobre la matriz §6 | **Condicionado** |
| CV-09 | La proyección de listado no lleva texto original, ni componentes de pieza, ni comentario | **0**, **0** y **0** **[ASUNCIÓN derivada del intake §17.4.P.10]** | `TC-09` | **Condicionado** |
| CV-10 | El ensamblado no declara ninguna referencia hacia `GeometriaFactory-Domain` | **0** | `TC-20` | **Bloqueante** |
| CV-11 | Ningún tipo de las **ocho** familias tiene un campo capaz de transportar el hash de la contraseña, la clave de firma, una dirección de servicio interno, una ruta de archivo de datos o una traza | **0** | `TC-15`, `TC-01`, `TC-04`, `TC-19` | **Bloqueante** |
| CV-12 | El conjunto cerrado tiene exactamente **15** códigos vivos y se producen **0** fuera de él | 15 y 0 | `TC-16` | **Bloqueante** |
| CV-13 | La respuesta de sesión declara exactamente **4** campos y **0** que transporten una condición que impida operar | 4 y 0 | `TC-01`, `TC-02` | **Bloqueante** |
| CV-14 | El ensamblado compila **sin advertencias** | 0 advertencias | Etapa `build`; intake §17.4.P.8 | **Bloqueante** |

**No hay criterio de latencia ni de throughput, y es correcto que no lo haya**: el ensamblado no ejecuta nada (`05` §8, cierre). El único atributo de rendimiento que puede empeorar es el **tamaño de la carga útil**, y por eso `CV-09` existe.

**No se declara ningún tiempo de ejecución de suite.** Ninguna fuente lo da para la batería de integración, y esta categoría no lo inventa.

## 4. Criterios de regresión y de compatibilidad

| Id | Criterio | Umbral |
| --- | --- | --- |
| CV-15 | Todas las inspecciones de superficie se reejecutan al cerrar cada etapa, y no sólo sobre las familias que la etapa tocó | 100 % de las inspecciones escritas hasta ese momento |
| CV-16 | **Ningún caso de prueba que estaba en verde pasa a rojo** sin justificación escrita en el informe de cierre | 0 regresiones sin justificar |
| CV-17 | Todo defecto cerrado generó al menos un `TC-XX` nuevo o extendió uno existente | 1 por defecto cerrado, como mínimo |
| CV-18 | **Todo cambio de un conjunto cerrado** —papel, situación de cuenta, estado del trabajo, severidad, desenlace o código de error— **está declarado como incompatible** en el `§17` del contrato de uso afectado, aunque compile | 100 % de los cambios de conjunto cerrado declarados |
| CV-19 | Ante un cambio incompatible, **las dos unidades desplegables se despliegan juntas** | 100 %. Su incumplimiento se manifiesta como `DXC-08`: fallos de forma en la carga útil y pruebas de integración que fallan en bloque |
| CV-20 | Ningún identificador de código retirado se reasigna a otra condición | 0 reciclados sobre los **3** retirados |

**La compatibilidad es un criterio de validación de este proyecto de código y no un tema aparte**, porque es la propiedad que el intake §17.4.P.3 declara como su mecanismo de protección: un cambio incompatible **rompe la compilación antes que el tiempo de ejecución**. Recibir esa rotura es la señal esperada; ignorarla es lo que produce `DXC-08`.

## 5. Criterios de calidad de código

| Id | Criterio | Umbral | Carácter |
| --- | --- | --- | --- |
| CV-21 | Los tipos son **serializables sin comportamiento**: sin lógica en los descriptores de acceso, sin campos calculados y sin ciclos entre tipos | 0 en las tres formas | **Bloqueante**. Es lo que `BT-17` exige y lo que este proyecto de código conserva tras el cierre de `PA-03` |
| CV-22 | El ensamblado **no declara ninguna biblioteca de serialización** | 0 | **Bloqueante**: declararla rompería las cero dependencias |
| CV-23 | El grafo de dependencias entre las **ocho** familias es acíclico, y la única arista adicional —reseteo hacia cuentas— conserva su motivo declarado | 0 ciclos, 1 arista adicional con motivo | **Bloqueante** |
| CV-24 | **Cobertura de líneas: no aplica como criterio.** El intake §17.4.P.6 lo declara así y fija el gate equivalente de `CV-08` | — | **No aplicable, declarado** |
| CV-25 | **Mutation score: no aplica.** No hay lógica que mutar | — | **No aplicable, declarado** |

**`CV-24` y `CV-25` se declaran en lugar de omitirse.** Un lector que no encuentre cobertura de líneas en un proyecto de código de tipo `library` tiene que poder leer por qué, y no deducirlo de un silencio.

## 6. Excepciones documentadas

| Situación | Salida admitida | Quién la aprueba |
| --- | --- | --- |
| Criterio **condicionado** —`CV-08`, `CV-09`— no alcanzado | Se registra la medición y su distancia al umbral en el informe de cierre, y **no bloquea**: el umbral es un valor rotulado [ASUNCIÓN] sin confirmar (`BT-18`) | Nadie: es el tratamiento declarado |
| Prueba de integración **no ejecutable todavía** porque `GeometriaFactory-Api` no existe | Se declara diferida **por escrito**, con la etapa en que se ejecuta, y la inspección de superficie correspondiente **sí se ejecuta** | El Product Owner, en el punto de control, con constancia escrita |
| Criterio **bloqueante** no cumplido | Se abre una tarea técnica con la remediación y **la etapa no cierra** hasta que se cumpla o hasta que el Product Owner acepte la excepción por escrito | El Product Owner |
| Campo nuevo que la revisión rechaza por `CV-11` | **No se admite excepción.** `05` §9 declara que agregar un campo de diagnóstico es la forma habitual en que ese defecto entra, y que entra sin que nadie lo note porque compila | — |

**Lo que no es una excepción admitida:** reponer un código retirado del conjunto cerrado —contradice `CA-09` de `CU-06` y describe situaciones que `RN-16` no admite—, agregar un campo del detalle a la proyección de listado «porque hace falta en una pantalla», o declarar cumplido un criterio de superficie cuya inspección no se hizo.

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara **veinticinco** criterios de validación, `CV-01` a `CV-25`, repartidos en funcionales, no funcionales, de regresión y compatibilidad, y de calidad de código, cada uno con su umbral y su forma de comprobación. Distingue tres caracteres —bloqueante, condicionado y no aplicable declarado— y ata los condicionados a los dos valores rotulados **[ASUNCIÓN]** del intake §22, asunción `A-4`. Declara la compatibilidad como criterio de validación propio, con el fundamento de que es el mecanismo de protección que el intake §17.4.P.3 le asigna a este proyecto de código; declara explícitamente que la cobertura de líneas y el mutation score no aplican, en lugar de omitirlos; y declara **cuatro** salidas ante un criterio no cumplido, una de ellas sin excepción posible. |
