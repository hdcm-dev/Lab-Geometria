# 10 · Ejemplos — GeometriaFactory-Contracts

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** README.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Developer Advocate / Sample Engineer Senior (AG-10)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../02-Especificacion-Funcional/`](../02-Especificacion-Funcional/), los **ocho** contratos de uso y las **once** restricciones transversales; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §3.1, las **ocho** familias de tipos; [`../06-Backlog-Tecnico/historias-usuario/`](../06-Backlog-Tecnico/historias-usuario/), las **veintidós** historias; [`../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md`](../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md) 1.1, los **veintidós** casos de prueba; `PRODUCT-INTAKE` 1.22 §16.1, §18 y §20
**Trazabilidad downstream:** [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), que toma las tres sondas `VER-XX`; `11-Documentacion` cuando se emita

---

## Tabla de contenido

- [1. Qué hay en esta carpeta](#1-qué-hay-en-esta-carpeta)
- [2. Tabla maestra de samples](#2-tabla-maestra-de-samples)
- [3. Contratos de verificación](#3-contratos-de-verificación)
- [4. Convenciones de los samples](#4-convenciones-de-los-samples)
- [5. Estructura de `/samples` y su desvío declarado](#5-estructura-de-samples-y-su-desvío-declarado)
- [6. Cómo agregar un sample nuevo](#6-cómo-agregar-un-sample-nuevo)
- [7. Vínculo con 05 y con 11](#7-vínculo-con-05-y-con-11)
- [8. Control de cambios](#8-control-de-cambios)

---

## 1. Qué hay en esta carpeta

Tres markdown explicativos con sus **diez** secciones obligatorias de `Rules-Examples.md` §4.2, y este índice. Cada uno apunta a una carpeta ejecutable de `/samples/contracts/`, que esta pasada deja **esqueletada**: con su README local y su comando previsto, y sin corrida hecha.

Es la **pasada de diseño** de `Rules-Examples.md` §0.2: los tres contratos de verificación están completos salvo `evidencia`, que dice `No verificado — sin código` en los tres.

**Un proyecto de código sin comportamiento igual tiene qué demostrar.** Estos tres samples no ejercitan lógica: recorren la superficie pública de las **ocho** familias de tipos, componen cuerpos con los datos reales del `PRODUCT-INTAKE` §20 y **cuentan lo que no está**. La forma característica de aserción es el recuento —cero campos, cuatro campos, quince códigos—, que es la que [`../08-Calidad-Y-Pruebas/Estrategia-Testing.md`](../08-Calidad-Y-Pruebas/Estrategia-Testing.md) §4 declara propia de este proyecto de código.

**Ninguno golpea el servicio de datos, y es deliberado.** La batería de integración que ejercita los tipos contra el servicio real **no vive en este proyecto de código** sino en `GeometriaFactory-Api` (`PRODUCT-INTAKE` §17.4.P.6), que es de nivel topológico 3. Los tres samples son lo que se puede correr **antes** de que ese proyecto de código exista, y esa es precisamente su utilidad: [`../08-Calidad-Y-Pruebas/Matriz-Cobertura-Pruebas.md`](../08-Calidad-Y-Pruebas/Matriz-Cobertura-Pruebas.md) §7 declara como hueco que «ningún tipo se puede ejercitar de verdad hasta que ese proyecto de código exista».

## 2. Tabla maestra de samples

| Sample | Nivel | Tiempo de setup | CU ilustrados | Ubicación |
| --- | --- | --- | --- | --- |
| [`ejemplo-01-basico.md`](ejemplo-01-basico.md) | Básico | < 5 min | CU-01, CU-02 | `/samples/contracts/01-basico/` |
| [`ejemplo-02-intermedio.md`](ejemplo-02-intermedio.md) | Intermedio | < 5 min | CU-03, CU-04, CU-05 | `/samples/contracts/02-intermedio/` |
| [`ejemplo-03-avanzado.md`](ejemplo-03-avanzado.md) | Avanzado | 10-15 min | CU-06, CU-07, CU-08 | `/samples/contracts/03-avanzado/` |

**Tres samples, el piso que `Rules-Examples.md` §2.2 fija para `library`.**

**Cobertura de los ocho contratos de uso: 8 de 8**, sin repeticiones. Uno por uno: `CU-01` y `CU-02` en el 01; `CU-03`, `CU-04` y `CU-05` en el 02; `CU-06`, `CU-07` y `CU-08` en el 03.

**Cobertura de las ocho familias de tipos: 8 de 8.** Sesión y cuentas en el 01; trabajo, listado y detalle en el 02; error, desenlace y reseteo en el 03. La familia de error, que es la dependencia común de las otras siete, aparece como destinataria de los rechazos en los tres y se abre entera en el 03.

**Cobertura de los ocho escenarios reales: 8 de 8.** Uno por uno: `E-1`, `E-2`, `E-3`, `E-4`, `E-6` y `E-7` en el sample 02; `E-5` y `E-8` en el sample 03. Todos transcriptos del `PRODUCT-INTAKE` §20 **sin modificación**; ninguno se sustituye por datos sintéticos. El sample 01 no usa ninguno, porque ninguno de los ocho es un dato de cuenta.

**Qué queda deliberadamente fuera.** `US-10`, el resumen por alumno y por estado, no se ilustra en ningún sample: su caso de prueba `TC-11` está declarado **fuera del tramo comprometido** en [`../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md`](../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md), y un sample que la ilustrara comprometería lo que la categoría 08 declaró no comprometido.

## 3. Contratos de verificación

| Sonda | Sample | Verifica | Comando | Estado | Última corrida |
| --- | --- | --- | --- | --- | --- |
| `VER-01` | [`ejemplo-01-basico.md`](ejemplo-01-basico.md) | CU-01, CU-02; US-01 a US-05 | `dotnet run --project samples/contracts/01-basico` | No verificado — sin código | — |
| `VER-02` | [`ejemplo-02-intermedio.md`](ejemplo-02-intermedio.md) | CU-03, CU-04, CU-05; US-06, US-07, US-08, US-11, US-12, US-13, US-18, US-19 | `dotnet run --project samples/contracts/02-intermedio` | No verificado — sin código | — |
| `VER-03` | [`ejemplo-03-avanzado.md`](ejemplo-03-avanzado.md) | CU-06, CU-07, CU-08; US-14, US-15, US-16, US-17, US-21, US-22 | `dotnet run --project samples/contracts/03-avanzado` | No verificado — sin código | — |

**Tres sondas, ninguna redundante**: los conjuntos de contratos de uso que verifican son disjuntos, y las familias de tipos que recorren también. Las tres entran a [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md) con estado `Sin verificar`, que es lo que `Deriva-Rules.md` §2.4 declara para un contrato en `No verificado — sin código`.

**Qué gates de la categoría 08 quedan ejercidos desde afuera del pipeline.** `QG-04` —quince códigos vivos y ninguno fuera del conjunto— por `VER-03`; `QG-07` —cuatro campos en la respuesta de sesión y cero que impidan operar— por `VER-01`; `QG-06` —proyección de listado sin texto original, componentes ni comentario— por `VER-02`, con su carácter **condicionado** conservado; `QG-02` —cero referencias hacia el dominio— por `VER-03`; y `QG-09` —ningún tipo que permita salir de un estado terminal ni que habilite al navegador— por `VER-03`. **`QG-05` no queda ejercido**: el 100 % de los tipos ejercitados por integración exige la batería que vive en `GeometriaFactory-Api`, y ningún sample la sustituye.

## 4. Convenciones de los samples

- **Autocontenidos y sin servicio levantado.** Ninguno abre una conexión: recorren la superficie pública y componen cuerpos.
- **Ejecutables en entorno limpio en cuatro pasos**, dentro del entorno de desarrollo contenido del repositorio.
- **Nivel declarado** en la §2 de cada markdown, con progresión por nivel y no por dominio.
- **Trazabilidad obligatoria** en la §8, con al menos una fila por contrato de uso, restricción transversal, ADR o NFR.
- **Criterio de aceptación evaluable por una máquina**: exit code más líneas exactas de salida, y en dos de los tres una aserción negativa. Ninguno redactado como prosa.
- **Los datos son reales**, transcriptos del `PRODUCT-INTAKE` §20 sin modificación. Los archivos de escenario llevan extensión `.txt` y no `.json`, porque el de `E-2` no es JSON estrictamente válido y una herramienta que lo reformateara rompería la comparación carácter por carácter.
- **Los samples no acuñan vocabulario.** Todo término que usan está declarado en [`../02-Especificacion-Funcional/Glosario-Funcional.md`](../02-Especificacion-Funcional/Glosario-Funcional.md), incluida la resolución de «contrato» en sus tres referentes: el **ensamblado de contratos**, el **contrato de uso** de cada caso de uso, y el **contrato de verificación** `VER-XX` que esta categoría aporta. Los tres aparecen en esta carpeta y por eso se escriben siempre calificados.

## 5. Estructura de `/samples` y su desvío declarado

| Tipo D8 | Estructura que `Rules-Examples.md` §2.3 fija | Estructura de este proyecto de código |
| --- | --- | --- |
| `library` | `/samples/01-basico-consola/`, `/samples/02-intermedio-con-extensiones/`, `/samples/03-avanzado-integracion-real/` | `/samples/contracts/01-basico/`, `/samples/contracts/02-intermedio/`, `/samples/contracts/03-avanzado/` |

Dos desvíos declarados, ninguno de nomenclatura por dominio:

1. **Un nivel de espacio de nombres por proyecto de código.** `Rules-Examples.md` §2.3 supone un proyecto de código por repositorio; este producto tiene **siete** en uno solo (`PRODUCT-INTAKE` §13 y §16). Se agrega el segmento `contracts/`, que es carpeta extra y no renombre de las base.
2. **Los slugs son de nivel.** `basico`, `intermedio` y `avanzado`, los tres admitidos por `Rules-Examples.md` §3.1. No se usa `-con-extensiones` porque el flag `tiene_extensibilidad` de este proyecto de código es **false** (`PRODUCT-MANIFEST` §5), ni `-integracion-real` porque la integración real vive en `GeometriaFactory-Api` y no acá.

**Tensión con `PRODUCT-INTAKE` §16.1, declarada y elevada.** Esa sección dice que Domain, Application, Infrastructure y Contracts van «sin samples propios: no son consumidas por integradores externos, sólo por Api. Su verificación vive en `tests/`». Esta categoría emite igual, con tres fundamentos verificables:

- El motivo que §16.1 da alcanza a la **arista A** de `Rules-Examples.md` §0.1. La **arista B** tiene otro destinatario, declarado en esa misma sección: «al equipo que construye, y a los agentes de IA que codifican contra la especificación».
- La consecuencia práctica es más fuerte acá que en cualquier otro proyecto de código del producto: la verificación de este ensamblado **no vive en `tests/` de este proyecto de código** sino en la batería de integración de `GeometriaFactory-Api`, de nivel topológico 3. Hasta que ese proyecto de código exista, los tres samples son lo **único** ejecutable que ejercita esta superficie.
- `Deriva-Rules.md` §2.4 y §6 exigen que ningún proyecto de código con categoría 10 quede sin matriz de sensado, y los propios artefactos de `08` declararon la omisión de la suya como **condicionada y temporal** ([`../08-Calidad-Y-Pruebas/README.md`](../08-Calidad-Y-Pruebas/README.md) §3).

**Lo que queda abierto:** la consolidación de `PRODUCT-INTAKE` §16.1. Corregirlo es del Product Owner sobre su propio documento, con el mismo criterio con que el `PRODUCT-MANIFEST` §5 trata el residuo de §18 sobre el número de funciones de la fachada. **Hasta que se consolide, la fuente vinculante de la estructura de `/samples/contracts/` es esta sección.**

## 6. Cómo agregar un sample nuevo

1. Elegir el número correlativo siguiente y un slug de `Rules-Examples.md` §3.1, por nivel o por capacidad, **nunca por dominio**.
2. Copiar la cabecera de §4.1 y las **diez** secciones de §4.2 de esas reglas.
3. Declarar el contrato de verificación en la §9, con un `VER-XX` no usado en este proyecto de código y criterio de aceptación evaluable.
4. Agregar la fila a las tablas de §2 y §3 de este README.
5. Dar de alta la sonda en [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), en `Sin verificar`, según `Deriva-Rules.md` §4.

## 7. Vínculo con 05 y con 11

Los tres samples respetan la **regla de exposición de la frontera** de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §3.2 y la comprueban desde afuera: ningún cuerpo compuesto por un sample transporta el hash de la contraseña, la clave de firma, una dirección de servicio interno ni una ruta de archivo de datos.

**`11-Documentacion` todavía no está emitida** para este proyecto de código. Cuando lo esté, referencia estos samples y los contextualiza sin duplicar su código.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial de la categoría, en la **pasada de diseño** de `Rules-Examples.md` §0.2. Declara **tres** samples con su tabla maestra y la tabla de contratos de verificación con las sondas `VER-01` a `VER-03` en `No verificado — sin código`. Verifica **8 de 8** contratos de uso, **8 de 8** familias de tipos y **8 de 8** escenarios del `PRODUCT-INTAKE` §20, uno por uno. Declara qué gates de la categoría 08 quedan ejercidos desde afuera del pipeline y cuál —`QG-05`— no, por depender de la batería de integración de `GeometriaFactory-Api`. Declara los **dos** desvíos respecto de §2.3 y la tensión con `PRODUCT-INTAKE` §16.1, elevada al Product Owner con su fundamento. |
