# 10 · Ejemplos — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** README.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Developer Advocate / Sample Engineer Senior (AG-10)
**Tipo de proyecto de código (D8):** `web-monolith`
**Trazabilidad upstream:** [`../02-Especificacion-Funcional/`](../02-Especificacion-Funcional/), los **diez** casos de uso; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §3.1, los **ocho** componentes en sus **tres** capas; [`../06-Backlog-Tecnico/historias-usuario/`](../06-Backlog-Tecnico/historias-usuario/), las **treinta** historias; [`../08-Calidad-Y-Pruebas/Estrategia-Testing.md`](../08-Calidad-Y-Pruebas/Estrategia-Testing.md) 1.1 §3 y §6; [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md) 1.2, sus **61** filas; `PRODUCT-INTAKE` 1.23 §16.1, §18 y §20
**Trazabilidad downstream:** [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), que suma la sonda `VER-01` como fila `SD-62`; `11-Documentacion` cuando se emita

---

## Tabla de contenido

- [1. Qué hay en esta carpeta](#1-qué-hay-en-esta-carpeta)
- [2. Tabla maestra de samples](#2-tabla-maestra-de-samples)
- [3. Contratos de verificación](#3-contratos-de-verificación)
- [4. Por qué hay un sample y no dos](#4-por-qué-hay-un-sample-y-no-dos)
- [5. Qué relación tiene esto con lo que §16.1 dice de este proyecto de código](#5-qué-relación-tiene-esto-con-lo-que-161-dice-de-este-proyecto-de-código)
- [6. Convenciones del sample](#6-convenciones-del-sample)
- [7. Estructura de `/samples` y su desvío declarado](#7-estructura-de-samples-y-su-desvío-declarado)
- [8. Cómo agregar un sample nuevo](#8-cómo-agregar-un-sample-nuevo)
- [9. Vínculo con 05 y con 11](#9-vínculo-con-05-y-con-11)
- [10. Control de cambios](#10-control-de-cambios)

---

## 1. Qué hay en esta carpeta

Un markdown explicativo con sus **diez** secciones obligatorias de `Rules-Examples.md` §4.2, y este índice. El markdown apunta a una carpeta ejecutable de `/samples/web/` del repositorio, que esta pasada deja **esqueletada**: con su README local y su comando previsto, y sin corrida hecha.

Esta emisión es la **pasada de diseño** de `Rules-Examples.md` §0.2. El contrato de verificación está completo salvo el campo `evidencia`, que dice `No verificado — sin código`. **Ninguna carpeta de `/samples` promete una corrida que no se hizo.**

## 2. Tabla maestra de samples

| Sample | Nivel | Tiempo de setup | CU ilustrados | Ubicación |
| --- | --- | --- | --- | --- |
| [`ejemplo-01-datos-seed.md`](ejemplo-01-datos-seed.md) | Básico | 10-15 min | CU-05, CU-06, CU-08 | `/samples/web/01-datos-seed/` |

**Un sample, y §4 declara por qué no son dos.** La progresión es **por capacidad** y no por nivel: `datos-seed` es el slug que `Rules-Examples.md` §3.1 admite y el que su §2.3 fija para `web-monolith`. El nivel implícito se declara igual en la §2 del markdown, como esa misma sección exige cuando la progresión es por capacidad.

**Cobertura de los diez casos de uso: 3 de 10, y los 7 restantes tienen su verificación declarada en otro lado.** El sample ilustra `CU-05`, `CU-06` y `CU-08`, que son los tres cuyo material es el dato del alumno. `CU-01`, `CU-02`, `CU-03`, `CU-04`, `CU-07`, `CU-09` y `CU-10` **no quedan sin verificación**: los cubre el guion de demostración de cada etapa —que el `PRODUCT-INTAKE` §16.1 declara como la forma de verificación de este proyecto de código— y las **61** filas de [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md). `Rules-Examples.md` §6 admite exactamente eso: «todo CU declarado crítico en 02 tiene al menos una sonda `VER-XX` que lo ejercita, **o la ausencia está justificada**». Ésta es la justificación, y es la misma que el intake ya había escrito.

**Cobertura de los ocho escenarios reales: 8 de 8.** El sample los transporta enteros, y es **el único proyecto de código del producto que los usa en su forma original y completa**, carácter por carácter, porque acá el escenario es lo que la persona pega en el formulario de envío ([`../08-Calidad-Y-Pruebas/Estrategia-Testing.md`](../08-Calidad-Y-Pruebas/Estrategia-Testing.md) §6). **Ningún escenario se sustituye por datos sintéticos.**

## 3. Contratos de verificación

Vista de conjunto de la arista B, en el formato de `Rules-Examples.md` §4.4.

| Sonda | Sample | Verifica | Comando | Estado | Última corrida |
| --- | --- | --- | --- | --- | --- |
| `VER-01` | [`ejemplo-01-datos-seed.md`](ejemplo-01-datos-seed.md) | CU-05, CU-06, CU-08; US-11, US-15, US-17, US-22, US-23 | `bash samples/web/01-datos-seed/run.sh` | No verificado — sin código | — |

**Una sonda, sin redundancia con ninguna otra.** Entra a [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md) como fila **`SD-62`**, en estado `Sin verificar`, que es lo que `Deriva-Rules.md` §2.4 declara para un contrato en `No verificado — sin código`. Esa matriz ya tenía **61** filas y declaraba «ninguna fila `VER-XX`, porque este proyecto de código no tiene categoría 10 todavía»: **esta emisión cierra ese hueco**, y las 61 filas anteriores no cambian.

**Qué aporta esta sonda que las 61 filas anteriores no aportaban.** Las 61 se resuelven, en su mayoría, por **inspección** —visual, de accesibilidad, de código o de tráfico—, porque este proyecto de código no tiene proyecto de pruebas propio y su verificación es un guion que **una persona ejecuta** ([`../08-Calidad-Y-Pruebas/Estrategia-Testing.md`](../08-Calidad-Y-Pruebas/Estrategia-Testing.md) §1 y §3). `SD-62` es la **primera fila de esta matriz que trae su propio comando y su propia aserción**, y eso es exactamente la asimetría que `Deriva-Rules.md` §4 declara en su cuarto momento: «una `SUP-XX` exige que alguien mire y compare, mientras que una `VER-XX` se corre sola y devuelve un veredicto».

## 4. Por qué hay un sample y no dos

`Rules-Examples.md` §2.2 fija para `web-monolith` un piso de **dos** samples, «datos seed + tema custom **(si hay punto de extensión visual)**», y su §2.3 repite la condición en la estructura de carpetas: «`/samples/02-tema-custom/` (este último **sólo si** hay punto de extensión visual)».

**Este proyecto de código no tiene punto de extensión visual**, y por eso el segundo sample no se emite:

| Comprobación | Resultado |
| --- | --- |
| `tiene_extensibilidad` en el `PRODUCT-MANIFEST` §5 | **false**. El único true del producto es `GeometriaFactory-Visor` |
| Cuál es el punto de extensión del producto | El **contrato de la fachada del visor**, con sus **seis** funciones (`PRODUCT-MANIFEST` §5, `PRODUCT-INTAKE` §17.7.P.3). Vive en otro proyecto de código y ya tiene sus samples: [`../../GeometriaFactory-Visor/10-Examples/`](../../GeometriaFactory-Visor/10-Examples/) |
| Qué pasa con los valores visuales de esta pieza | Salen de un **catálogo cerrado de tokens**, y la fila `SD-54` de la matriz de sensado declara deriva mayor cuando aparece un literal visual fuera de él. Un catálogo cerrado con umbral cero **es lo contrario de un punto de extensión** |

**Un sample de tema custom acá afirmaría una capacidad que el producto no tiene**, y sería el anti-patrón de `Rules-Examples.md` §4.5 en su forma más cara: un ejemplo que ilustra bien algo que no existe.

## 5. Qué relación tiene esto con lo que §16.1 dice de este proyecto de código

El `PRODUCT-INTAKE` §16.1 declara para `GeometriaFactory-Web`: «No produce sample propio: el guion de demostración de cada etapa, ejecutado en el navegador del host, cumple ese papel (RF §9.3)». **Esa frase sigue siendo cierta de la arista A y este README no la contradice.**

`Rules-Examples.md` §0.1 distingue dos aristas, y hay que verlas por separado:

| Arista | Destinatario | ¿Lo cubre el guion de demostración? |
| --- | --- | --- |
| **A — Referencia de integración** | El integrador que incorpora el proyecto de código en una aplicación propia | **Sí, y §16.1 tiene razón.** Este proyecto de código no tiene integradores: es la pieza pública que la persona usa, y lo que hay que mostrar es el recorrido, que el guion muestra mejor que ningún sample. **Este README no emite ningún sample de arista A** |
| **B — Arnés de autovalidación** | El equipo que construye y los agentes que codifican contra la especificación | **No.** El guion lo ejecuta una persona en un navegador y su resultado no es un veredicto de máquina. El único sample que se emite acá es de arista B, y lo que aporta es el **estado de partida reproducible** que el guion necesita antes de empezar |

**La diferencia con `Domain`, `Contracts`, `Application` e `Infrastructure` conviene decirla, porque el argumento no es el mismo.** En aquéllos, el segundo término del argumento es que sin categoría 10 quedaban **sin ninguna sonda de deriva**. Acá **no**: este proyecto de código ejecutó la Fase B2, tiene línea de base visual aprobada y su matriz de sensado nació con **61** filas en la Fase B2. Lo que acá falta no es el instrumento sino **una sonda que se corra sola**, y eso es lo que esta emisión agrega, sin desplazar al guion de demostración de su papel.

**Lo que queda abierto:** nada que elevar sobre §16.1 por parte de este proyecto de código. Su fila no está anotada como pendiente de revisión —las que sí lo están son las de `Application` e `Infrastructure`, y sus categorías 10 las resuelven en sus propios README—, y este sample **no la contradice**: agrega un dato seed, que es exactamente el contenido que `Rules-Examples.md` §2.3 le asigna al tipo `web-monolith`.

## 6. Convenciones del sample

- **El sample no dibuja pantallas y no reemplaza al guion de demostración.** Deja el estado desde el que el guion arranca, y lo verifica **sin pasar por la pantalla**, que es el instrumento que [`../08-Calidad-Y-Pruebas/Estrategia-Testing.md`](../08-Calidad-Y-Pruebas/Estrategia-Testing.md) §3 llama «verificación forzando la solicitud».
- **No viola `RA-01`.** Esa regla prohíbe que **el JavaScript del navegador** llame al servicio de datos. El sample corre dentro del entorno de desarrollo contenido, servidor a servidor, igual que la colección de peticiones de `GeometriaFactory-Api`: no hay navegador involucrado.
- **No expone ninguna dirección de servicio interno**, que es `RA-03`: la dirección del servicio de datos la toma de configuración, como el resto de la pieza ([`ADR-07`](../05-Arquitectura-Tecnica/Adrs/ADR-07-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md)).
- **Ejecutable en entorno limpio en cinco pasos o menos**, dentro del entorno de desarrollo contenido del repositorio.
- **Nivel declarado** en la §2 del markdown, aunque la progresión sea por capacidad.
- **Criterio de aceptación evaluable por una máquina**: exit code más líneas exactas de salida. No está redactado como prosa.
- **Los datos son reales**, transcriptos del `PRODUCT-INTAKE` §20 sin modificación, en archivos `.txt` y no `.json`, porque el texto de `E-2` no es JSON estrictamente válido y una herramienta que lo reformateara rompería lo que ese escenario ejercita. Los datos de identidad son valores evidentemente ficticios y se declaran como tales; los valores compuestos para la maqueta que `Contrato-Datos-Maqueta.md` §5 declara **no propagados** —la credencial de prueba y la cuarta cuenta de ejemplo— **no aparecen**, porque la fila `SD-60` de la matriz de sensado declara deriva mayor si lo hicieran.
- **El sample no acuña vocabulario.** Todo término está declarado en [`../02-Especificacion-Funcional/Glosario-Funcional.md`](../02-Especificacion-Funcional/Glosario-Funcional.md) y en [`../03-UX-UI-DX/Glosario-UX.md`](../03-UX-UI-DX/Glosario-UX.md).

## 7. Estructura de `/samples` y su desvío declarado

| Tipo D8 | Estructura que `Rules-Examples.md` §2.3 fija | Estructura de este proyecto de código |
| --- | --- | --- |
| `web-monolith` | `/samples/01-datos-seed/`, `/samples/02-tema-custom/` (este último sólo si hay punto de extensión visual) | `/samples/web/01-datos-seed/` |

Dos desvíos, los dos declarados acá y ninguno de nomenclatura por dominio:

1. **Un nivel de espacio de nombres por proyecto de código.** `Rules-Examples.md` §2.3 supone un proyecto de código por repositorio. Este producto tiene **siete** en un solo repositorio (`PRODUCT-INTAKE` §13 y §16), de modo que `/samples/01-datos-seed/` colisionaría entre proyectos de código. Se agrega el segmento `web/`, que es carpeta extra y no renombre de las base. Es el mismo criterio que ya aplicaron los otros seis proyectos de código al emitir su categoría 10.
2. **La segunda carpeta no existe**, y la propia regla lo admite con su condición: no hay punto de extensión visual. El fundamento entero está en §4.

## 8. Cómo agregar un sample nuevo

1. Elegir el número correlativo siguiente y un slug de `Rules-Examples.md` §3.1, por nivel o por capacidad, **nunca por dominio**.
2. Copiar la cabecera de §4.1 y las **diez** secciones de §4.2 de esas reglas, y declarar el nivel implícito en la §2 si el slug es de capacidad.
3. Declarar el contrato de verificación en la §9, con un `VER-XX` no usado en este proyecto de código, y criterio de aceptación evaluable **sin pasar por la pantalla**.
4. Agregar la fila a las tablas de §2 y §3 de este README.
5. Dar de alta la sonda en [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), a continuación de `SD-62`, en `Sin verificar`, según `Deriva-Rules.md` §4, y **declarar si el elemento ya lo mira alguna de las 61 filas anteriores**, para que nada se sense dos veces con umbrales distintos.

## 9. Vínculo con 05 y con 11

El sample no invoca ningún componente interno de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §3.1: habla con el servicio de datos por la misma superficie que el **cliente tipado** de capa 3 usa, y por eso lo que deja armado es exactamente lo que la pieza pública va a leer después.

**`11-Documentacion` todavía no está emitida** para este proyecto de código. Cuando lo esté, referencia este sample y lo contextualiza **sin duplicar su código**, que es la división que `Rules-Examples.md` §0 fija: 10 demuestra con código ejecutable y verificable, 11 explica, referencia y enlaza.

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial de la categoría, en la **pasada de diseño** de `Rules-Examples.md` §0.2. Declara **un** sample, `datos-seed`, con progresión por capacidad y nivel implícito declarado, y **una** sonda `VER-01` en `No verificado — sin código`, que entra a la matriz de sensado ya emitida como fila **`SD-62`** sin tocar sus **61** filas anteriores. Declara **por qué hay un sample y no dos** —la regla condiciona el de tema custom a un punto de extensión visual que este proyecto de código no tiene, con `tiene_extensibilidad` false—, **qué relación tiene con `PRODUCT-INTAKE` §16.1** —que sigue siendo cierto de la arista A, y esta emisión es sólo de arista B—, y en qué se diferencia el argumento del que valió para los proyectos de código sin maqueta. Verifica **3 de 10** casos de uso con la ausencia de los otros siete justificada según `Rules-Examples.md` §6, y **8 de 8** escenarios del intake §20 usados en su forma original y completa. |
