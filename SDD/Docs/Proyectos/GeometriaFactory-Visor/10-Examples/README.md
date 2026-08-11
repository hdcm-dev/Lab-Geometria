# 10 · Ejemplos — GeometriaFactory-Visor

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Visor
**Documento:** README.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Developer Advocate / Sample Engineer Senior (AG-10)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../02-Especificacion-Funcional/`](../02-Especificacion-Funcional/), los **siete** casos de uso y el contrato de la fachada; [`../05-Arquitectura-Tecnica/Extensibilidad.md`](../05-Arquitectura-Tecnica/Extensibilidad.md); [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §3, las **catorce** historias; [`../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md`](../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md) 1.0, los **veintiún** casos de prueba; `PRODUCT-INTAKE` **1.25** §15, §16.1, §17.7, §18 y §20
**Trazabilidad downstream:** [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), que suma las tres sondas `VER-XX` como `SD-13` a `SD-15`; [`../08-Calidad-Y-Pruebas/Guia-Testing-Extensibilidad.md`](../08-Calidad-Y-Pruebas/Guia-Testing-Extensibilidad.md), que usa el sample 03 como batería de aceptación de un reemplazo; `11-Documentacion` cuando se emita

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

Tres markdown explicativos con sus **diez** secciones obligatorias de `Rules-Examples.md` §4.2, y este índice. Cada uno apunta a una carpeta ejecutable de [`/samples/visor/`](../../../../../samples/visor/), que esta pasada deja **esqueletada**: con su README local y su comando previsto, y sin corrida hecha. **Las tres carpetas existen** —[`01-basico/`](../../../../../samples/visor/01-basico/), [`02-intermedio/`](../../../../../samples/visor/02-intermedio/) y [`03-avanzado/`](../../../../../samples/visor/03-avanzado/)—, cada una con su `README.md` local y el comando previsto de su contrato, y ninguna con código.

Es la **pasada de diseño** de `Rules-Examples.md` §0.2: los tres contratos de verificación están completos salvo `evidencia`, que dice `No verificado — sin código` en los tres.

**Los tres samples son las tres partes del sample `S-1` del `PRODUCT-INTAKE` §18**, la página integradora sin backend: un archivo que carga el archivo de guion, un área donde se pega un texto y una superficie de dibujo. No son tres samples que compiten con S-1: son su progresión didáctica, y entre los tres ejercen las **seis** funciones que §17.7.P.3 declara desde la versión 1.6 del intake. El `PRODUCT-INTAKE` §18 dice que S-1 «ejerce el contrato entero sin ninguna pieza del backend, que es exactamente la propiedad que hace reemplazable al motor 3D»; el ejemplo 03 es el que cierra esa promesa.

**Este es el único proyecto de código del producto cuyo `tiene_extensibilidad` es true** (`PRODUCT-MANIFEST` §5): el punto de extensión del producto es el contrato de esta fachada, y estos samples son su demostración ejecutable.

## 2. Tabla maestra de samples

| Sample | Nivel | Tiempo de setup | CU ilustrados | Ubicación |
| --- | --- | --- | --- | --- |
| [`ejemplo-01-basico.md`](ejemplo-01-basico.md) | Básico | < 5 min | CU-01, CU-02, CU-05 | `/samples/visor/01-basico/` |
| [`ejemplo-02-intermedio.md`](ejemplo-02-intermedio.md) | Intermedio | 10-15 min | CU-02, CU-03, CU-04 | `/samples/visor/02-intermedio/` |
| [`ejemplo-03-avanzado.md`](ejemplo-03-avanzado.md) | Avanzado | 20-30 min | CU-06, CU-07 | `/samples/visor/03-avanzado/` |

**Tres samples, el piso que `Rules-Examples.md` §2.2 fija para `library`.** El tiempo de setup crece porque el 02 necesita un conductor de navegador y el 03 agrega la preferencia de movimiento reducido declarada, la ausencia de acceso a redes externas y la inspección del archivo de guion generado.

**Cobertura de los siete casos de uso: 7 de 7.** Uno por uno: `CU-01`, `CU-02` y `CU-05` en el 01; `CU-02`, `CU-03` y `CU-04` en el 02; `CU-06` y `CU-07` en el 03. `CU-02` aparece en dos samples y la redundancia está justificada: el 01 verifica que las **tres** piezas de `E-1` se dibujen, y el 02 verifica la lectura de las variantes de clave del emisor y la enumeración de las piezas no dibujadas, que son dos aspectos distintos del mismo caso de uso.

**Cobertura de las seis funciones: 6 de 6.** El 01 ejerce tres —`inicializar`, `cargarJson`, `destruir`—, el 02 suma `seleccionarPieza` y `redimensionar`, y el 03 suma `establecerMovimiento` y recorre las seis juntas.

**Cobertura de las siete garantías: 7 de 7.** `G-1` en el 01 y, en su peor caso, en el 03; `G-2` y `G-3` en el 03; `G-4` en el 01; `G-5` en el 02; `G-6` en el 01 y el 03; `G-7` en el 01 y el 02.

**Cobertura de los ocho escenarios reales: 7 de 8.** Uno por uno: `E-1` en el 01 y el 03; `E-2`, `E-5`, `E-6`, `E-7` y `E-8` en el 02; `E-7` además en el 03. **El que falta es `E-3`, y su ausencia está justificada**: `E-3` y `E-4` son el mismo cubo de lado 3 emitido por los dos ejemplos de la cátedra, y lo que ese par ejercita es la **verificación del área declarada**, que este proyecto de código no hace —la fachada lee una dimensión y no valida un trabajo—. Los dos están cubiertos por `TC-06` de [`../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md`](../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md), y el sample 02 los ofrece como **variación sugerida**, con esa aclaración escrita. **Ningún escenario se sustituye por datos sintéticos.**

## 3. Contratos de verificación

| Sonda | Sample | Verifica | Comando | Estado | Última corrida |
| --- | --- | --- | --- | --- | --- |
| `VER-01` | [`ejemplo-01-basico.md`](ejemplo-01-basico.md) | CU-01, CU-02, CU-05; US-01, US-04, US-07, US-08, US-11 | `bash scripts/build-visor.sh && npm --prefix samples/visor/01-basico run verify` | No verificado — sin código | — |
| `VER-02` | [`ejemplo-02-intermedio.md`](ejemplo-02-intermedio.md) | CU-02, CU-03, CU-04; US-05, US-06, US-07, US-09, US-10 | `bash scripts/build-visor.sh && npm --prefix samples/visor/02-intermedio run verify` | No verificado — sin código | — |
| `VER-03` | [`ejemplo-03-avanzado.md`](ejemplo-03-avanzado.md) | CU-06, CU-07; US-02, US-12, US-13, US-14 | `bash scripts/build-visor.sh && npm --prefix samples/visor/03-avanzado run verify` | No verificado — sin código | — |

**Tres sondas, con una redundancia declarada y justificada**: `CU-02` lo ejercitan `VER-01` y `VER-02`, y `US-07` también, por el motivo de §2. Las demás intersecciones son vacías.

**Las tres entran a [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md) como `SD-13`, `SD-14` y `SD-15`, en estado `Sin verificar`**, que es lo que `Deriva-Rules.md` §2.4 declara para un contrato en `No verificado — sin código`. Esa matriz ya tenía **doce** filas ancladas en el contrato de la fachada y declaraba que «ninguna fila `VER-XX`, porque `10-Examples` no está emitida»; esta emisión cierra ese hueco.

**Qué gates de la categoría 08 quedan ejercidos por los samples.** `QG-02` —`PT-03`— y `QG-03` —`PT-02`— por `VER-03`; `QG-04` —cero red con los dos movimientos prendidos— por `VER-03`; `QG-05` —cero persistencia— por `VER-03`; `QG-06` —seis funciones, un nombre global, cero globales sueltas— por `VER-03`; `QG-07` —piezas no dibujadas enumeradas— por `VER-02`; `QG-08` —siete códigos, ninguno acuñado aguas abajo— por `VER-03`. **`QG-01` y `QG-09` no los ejercen los samples**: el primero es del comando de construcción y el segundo, que el archivo de guion nunca se edite a mano, se rechaza en revisión.

## 4. Convenciones de los samples

- **Sin backend, y es la propiedad que define al proyecto de código.** Ninguno necesita servicio de datos, credencial ni acceso a redes de distribución externas. Es lo que el `PRODUCT-INTAKE` §17.7.P.6 exige no perder.
- **Ejecutables en entorno limpio en cinco pasos o menos**, dentro del entorno de desarrollo contenido del repositorio.
- **El comando de construcción es el corto.** Los tres usan `scripts/build-visor.sh`, que produce **sólo** el archivo de guion; `scripts/build.sh` lo encadena con la compilación del resto del producto y no hace falta acá (`PRODUCT-INTAKE` §17.7.P.8).
- **Toda aserción de ausencia lleva su condición de medición**, y la condición es vinculante. Un umbral cero sin condición de medición es un criterio mal escrito, y así lo declara [`../08-Calidad-Y-Pruebas/Estrategia-Testing.md`](../08-Calidad-Y-Pruebas/Estrategia-Testing.md) §4.
- **Los datos son reales**, transcriptos del `PRODUCT-INTAKE` §20 sin modificación, en archivos `.txt` y no `.json`, porque el texto de `E-2` no es JSON estrictamente válido y una herramienta que lo reformateara rompería lo que ese escenario ejercita.
- **El archivo de guion nunca se edita a mano.** Los tres samples lo consumen generado por el comando de construcción, que es la regla del `PRODUCT-INTAKE` §17.7.P.7.
- **Los samples no acuñan vocabulario ni códigos.** Los **siete** códigos de condición tienen fuente única en `Definicion-Contrato-De-Fachada.md` §6, y ninguno puede nacer acá.

## 5. Estructura de `/samples` y su desvío declarado

| Tipo D8 | Estructura que `Rules-Examples.md` §2.3 fija | Estructura de este proyecto de código |
| --- | --- | --- |
| `library` | `/samples/01-basico-consola/`, `/samples/02-intermedio-con-extensiones/`, `/samples/03-avanzado-integracion-real/` | `/samples/visor/01-basico/`, `/samples/visor/02-intermedio/`, `/samples/visor/03-avanzado/` |

Tres desvíos, los tres declarados acá y ninguno de nomenclatura por dominio:

1. **Un nivel de espacio de nombres por proyecto de código.** `Rules-Examples.md` §2.3 supone un proyecto de código por repositorio; este producto tiene **siete** en uno solo (`PRODUCT-INTAKE` §13 y §16). Se agrega el segmento `visor/`, que es carpeta extra y no renombre de las base.
2. **El slug del primero no es `-consola`.** Este proyecto de código no tiene consola: su artefacto es un archivo de guion que corre en un navegador. Se usa `basico`, admitido por `Rules-Examples.md` §3.1.
3. **El slug del tercero no es `-integracion-real`.** La integración real de este archivo de guion es con el componente anfitrión que vive en `GeometriaFactory-Web`, y un sample que la usara dejaría de ser «sin backend», que es la propiedad que S-1 existe para demostrar.

**Sin tensión con `PRODUCT-INTAKE` §16.1.** A diferencia de los otros dos proyectos de código de nivel topológico 0, acá el intake **exige** el sample: §16.1 declara que `/samples` de `GeometriaFactory-Visor` lleva una «página integradora sin backend», y agrega que «es una propiedad exigida explícitamente por RT §8.3 y por el criterio de aceptación de la etapa `g`, no un agregado de conveniencia». Estos tres documentos materializan esa exigencia.

**El residuo del intake §18 que esta sección declaraba ya no existe, y la afirmación se retira.** La versión 1.0 de este README decía que §18, al describir el punto de extensión, arrastraba una enumeración de **cinco** funciones de la fachada, y lo tomaba del `PRODUCT-MANIFEST` §5 en lugar de abrir §18. **§18 abierto en la fuente dice otra cosa**, y desde la versión 1.11 del intake:

> El punto de extensión del producto es el contrato de la fachada del visor (`inicializar`, `cargarJson`, `seleccionarPieza`, `redimensionar`, `destruir` y `establecerMovimiento`, las **seis** que §17.7 P.3 declara desde 1.6).

Las **seis**, por nombre y con el rótulo del recuento. §18, §17.7.P.3 y [`../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md) §4 **coinciden**, y contra las tres se escriben estos samples. **Lo que sí quedó desactualizado es el `PRODUCT-MANIFEST` §5**, que afirmaba que «la enumeración de §18 del intake sigue nombrando cinco»; esa línea se corrigió el 2026-08-11 al resolver el hallazgo **P1-2** del informe de auditoría de esta fase. **No hay nada que elevar al Product Owner sobre §18 por parte de este proyecto de código.**

## 6. Cómo agregar un sample nuevo

1. Elegir el número correlativo siguiente y un slug de `Rules-Examples.md` §3.1, por nivel o por capacidad, **nunca por dominio**.
2. Copiar la cabecera de §4.1 y las **diez** secciones de §4.2 de esas reglas.
3. Declarar el contrato de verificación en la §9, con un `VER-XX` no usado en este proyecto de código, criterio de aceptación evaluable y **condición de medición** en toda aserción de ausencia.
4. Agregar la fila a las tablas de §2 y §3 de este README.
5. Dar de alta la sonda en [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), en `Sin verificar`, según `Deriva-Rules.md` §4, y declarar en §4 de esa matriz si el elemento ya lo mira alguna fila de la matriz de `GeometriaFactory-Web`.

## 7. Vínculo con 05 y con 11

Los tres samples invocan **sólo** la fachada y no nombran ninguna primitiva del motor de dibujo: es la regla de dependencias de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §3.1, y es lo que hace que la variación de reemplazo del ejemplo 03 sea posible.

[`../05-Arquitectura-Tecnica/Extensibilidad.md`](../05-Arquitectura-Tecnica/Extensibilidad.md) declaraba que el sample S-1 lo desarrollaría «la categoría **10-Examples**, que todavía no está emitida para este proyecto de código». Esta emisión lo desarrolla; la actualización de esa referencia pertenece a la categoría 05 y no se hace desde acá.

**`11-Documentacion` todavía no está emitida.** Cuando lo esté, referencia estos samples y los contextualiza sin duplicar su código.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **Correcciones del informe `G-10-Examples-Siete-Proyectos-r1.md` 1.0, contrastadas contra el texto vivo del `PRODUCT-INTAKE` 1.25.** **P0-1**: las tres carpetas de [`/samples/visor/`](../../../../../samples/visor/) se crean de verdad, cada una con su README local y su comando previsto, y §1 lo declara con el enlace; el comando de las tres filas `VER-XX` de la matriz de sensado, `SD-13` a `SD-15`, queda coherente con lo que existe. **P1-2**: se retira de §5 la afirmación de que §18 arrastra un residuo de **cinco** funciones de la fachada. Era falsa y estaba tomada del `PRODUCT-MANIFEST` §5 en lugar de la fuente: §18 abierto enumera las **seis** por nombre y las rotula «las seis que §17.7 P.3 declara desde 1.6». Se cita ahora el texto de §18 y se registra que quien quedó desactualizado era el manifiesto, ya corregido. **P1-3**: se registra que la 1.25 precisó que las tres muestras `S-X` de §18 no son el conjunto de las carpetas de `/samples`, lo que confirma —y no contradice— la lectura de esta categoría de que sus tres samples son las tres partes de **S-1**. Se actualiza la trazabilidad upstream a la versión **1.25** del intake. Ningún recuento, contrato, sample ni cobertura cambia. |
| 1.0 | 2026-08-11 | Emisión inicial de la categoría, en la **pasada de diseño** de `Rules-Examples.md` §0.2. Declara **tres** samples, que son las tres partes del sample **S-1** del `PRODUCT-INTAKE` §18, con su tabla maestra y la tabla de contratos de verificación con las sondas `VER-01` a `VER-03` en `No verificado — sin código`. Verifica **7 de 7** casos de uso, **6 de 6** funciones de la fachada, **7 de 7** garantías y **7 de 8** escenarios del intake §20, con el motivo declarado del que falta. Declara qué gates de la categoría 08 quedan ejercidos por los samples y cuáles no, los **tres** desvíos respecto de §2.3, la ausencia de tensión con §16.1 —que acá **exige** el sample— y un supuesto residuo del intake §18 sobre el número de funciones, tomado del `PRODUCT-MANIFEST` §5. [Retirado en 1.1: §18 no tiene tal residuo.] |
