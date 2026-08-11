# Matriz de sensado de deriva — GeometriaFactory-Contracts

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** Matriz-Sensado-Deriva.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Developer Advocate / Sample Engineer Senior (AG-10)
**Variante:** Sensado de deriva por contratos de verificación
**Trazabilidad upstream:** [`../10-Examples/README.md`](../10-Examples/README.md) 1.1 §3 y los tres contratos de verificación de [`../10-Examples/ejemplo-01-basico.md`](../10-Examples/ejemplo-01-basico.md), [`../10-Examples/ejemplo-02-intermedio.md`](../10-Examples/ejemplo-02-intermedio.md) y [`../10-Examples/ejemplo-03-avanzado.md`](../10-Examples/ejemplo-03-avanzado.md), los tres 1.0; `Deriva-Rules.md` §2.3, §2.4, §3 y §4
**Trazabilidad downstream:** [`README.md`](README.md) §3 y [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §7, que declaraban su ausencia y ahora la citan; `09-Devops`

---

## Tabla de contenido

- [1. Por qué existe esta matriz y por qué recién ahora](#1-por-qué-existe-esta-matriz-y-por-qué-recién-ahora)
- [2. Contra qué se sensa](#2-contra-qué-se-sensa)
- [3. La matriz](#3-la-matriz)
- [4. Umbrales de deriva aplicados](#4-umbrales-de-deriva-aplicados)
- [5. Qué no sensa esta matriz](#5-qué-no-sensa-esta-matriz)
- [6. Control de cambios](#6-control-de-cambios)

---

## 1. Por qué existe esta matriz y por qué recién ahora

`GeometriaFactory-Contracts` tiene `requiere_maqueta` en **false** (`PRODUCT-MANIFEST` §5): no ejecutó la Fase B2 y no tiene línea de base visual ni contrato de datos de maqueta. Hasta la emisión de `10-Examples` no había ninguna fuente de sondas, y por eso [`README.md`](README.md) §3 y [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §7 declararon la omisión **condicionada y temporal**, con la frase de cierre «cuando se emita la categoría 10 la matriz se abre con sus filas `VER-XX` y esta fila se retira».

`Deriva-Rules.md` §2.3 lo prevé literalmente para este caso, y §6 lo exige: «ningún proyecto de código con categoría 10 queda sin `Matriz-Sensado-Deriva.md`, aunque no haya ejecutado Fase B2».

**Quién la abre.** La abre **AG-10** al cerrar la fase que genera la categoría 10, que es el segundo momento de sensado de `Deriva-Rules.md` §4, porque la Fase E de este proyecto de código ya cerró y en ese momento las sondas no existían. La incorporación a la estrategia de testing sigue siendo de AG-08.

**Ninguna fila afirma nada sobre el estado del sistema construido.** Las tres nacen en `Sin verificar` y sin fecha, porque el ensamblado no está construido.

## 2. Contra qué se sensa

Contra los **contratos de verificación** de los tres samples de `10-Examples`, y contra nada más. No hay identificadores `SUP-XX`, `CMP-XX`, `EST-XX`, `NAV-XX` ni `DM-XX` que citar: este proyecto de código no tiene superficie visual ni maqueta.

**Hay un motivo adicional para que esta matriz importe acá, y conviene decirlo.** La verificación de este ensamblado **no vive en `tests/` de este proyecto de código**: son tipos sin comportamiento y se ejercitan desde la batería de integración de `GeometriaFactory-Api` (`PRODUCT-INTAKE` §17.4.P.6), que es de nivel topológico 3. Mientras ese proyecto de código no exista, las tres sondas son el **único** instrumento ejecutable que sensa esta superficie.

## 3. La matriz

| ID | Elemento de línea de base | Afirmación a verificar | Método de verificación | Evidencia esperada | Umbral de deriva | Estado | Última verificación |
| --- | --- | --- | --- | --- | --- | --- | --- |
| `SD-01` | `VER-01` de [`../10-Examples/ejemplo-01-basico.md`](../10-Examples/ejemplo-01-basico.md) §9 | La respuesta de sesión declara exactamente **4** campos; **0** transportan el hash de la contraseña o la clave de firma; **0** transportan una condición que impida operar; el listado de cuentas tiene **0** campos con alguna forma de la contraseña almacenada; y hay **0** tipos de establecimiento anónimo de contraseña en la superficie | El comando del contrato: `dotnet run --project samples/contracts/01-basico` | Campo `evidencia` de `VER-01`, con su fecha | **Menor**: cambia el texto de una línea de salida sin cambiar su semántica. **Mayor**: el `criterio_aceptacion` falla, cambia el comando sin actualizar el contrato, aparecen precondiciones no declaradas, o `CU-01` o `CU-02` dejan de estar cubiertos | `Sin verificar` | — |
| `SD-02` | `VER-02` de [`../10-Examples/ejemplo-02-intermedio.md`](../10-Examples/ejemplo-02-intermedio.md) §9 | El texto original de `E-2` vuelve **idéntico carácter por carácter** del ida y vuelta; la proyección de listado tiene **0** ocurrencias de texto original, de componentes de pieza y de comentario, y sí trae el estado; los detalles de `E-1`, `E-7` y `E-6` traen **3**, **6** y **1** piezas; y la observación de `E-3` lleva el par 36.00 y 54.00 en **campos separados**, contra las **0** observaciones de `E-4` | El comando del contrato: `dotnet run --project samples/contracts/02-intermedio` | Campo `evidencia` de `VER-02`, con su fecha | **Menor**: cambia el texto de una línea sin cambiar su semántica. **Mayor**: el `criterio_aceptacion` falla, un escenario se sustituye por datos sintéticos o se reformatea, o `CU-03`, `CU-04` o `CU-05` dejan de estar cubiertos | `Sin verificar` | — |
| `SD-03` | `VER-03` de [`../10-Examples/ejemplo-03-avanzado.md`](../10-Examples/ejemplo-03-avanzado.md) §9 | El tipo de error declara **4** campos y **0** capaces de transportar dirección, ruta o secreto; el conjunto cerrado tiene **15** códigos vivos sobre **18** emitidos, con **3** retirados y **0** reciclados; los detalles de ubicación de `E-5` y `E-8` traen **índice 1**; hay **0** campos que permitan salir de un estado terminal; **0** referencias hacia `GeometriaFactory-Domain`; y **0** tipos que habiliten al navegador a armar la solicitud | El comando del contrato: `dotnet run --project samples/contracts/03-avanzado` | Campo `evidencia` de `VER-03`, con su fecha | **Menor**: cambia el texto de una línea sin cambiar su semántica. **Mayor**: el `criterio_aceptacion` falla, aparece un código fuera del conjunto de quince, se recicla un identificador retirado, el índice reportado pasa a ser 0, o `CU-06`, `CU-07` o `CU-08` dejan de estar cubiertos | `Sin verificar` | — |

**Tres filas, una por contrato de verificación, sin contratos huérfanos ni filas sin contrato que las respalde** (`Deriva-Rules.md` §6). La correspondencia es uno a uno: `SD-01`↔`VER-01`, `SD-02`↔`VER-02`, `SD-03`↔`VER-03`.

**El método de verificación de las tres es el comando declarado en su contrato, sin desvío**, que es lo que `Deriva-Rules.md` §2.4 declara para una sonda que trae su propio comando y su propia aserción.

## 4. Umbrales de deriva aplicados

De la fila «Contratos y comportamiento (`VER-XX`)» de `Deriva-Rules.md` §3, sin agregarle dimensiones.

| Dimensión | Deriva menor, se registra y no bloquea | Deriva mayor, bloquea y exige decisión | Filas |
| --- | --- | --- | --- |
| Contratos y comportamiento (`VER-XX`) | Cambia el texto de un mensaje de salida sin cambiar su semántica, o cambia el formato de un registro | El `criterio_aceptacion` falla; cambia el comando de ejecución sin actualizar el contrato; aparecen precondiciones no declaradas; o el contrato de uso que la sonda ejercita deja de estar cubierto | `SD-01`, `SD-02`, `SD-03` |

**Una precisión propia de este proyecto de código.** Un cambio incompatible en un tipo de transferencia **rompe la compilación** antes que ninguna sonda, porque los dos extremos se compilan contra el mismo ensamblado (`PRODUCT-INTAKE` §17.4.P.3). Estas tres sondas no detectan ese cambio: detectan el que **compila igual** y sin embargo cambia lo que la frontera expone, que es el que se escapa.

**Toda deriva mayor se resuelve por una de dos vías y nunca por omisión** (`Deriva-Rules.md` §3): se corrige el ensamblado, o se cambia la especificación con aprobación humana explícita, en cuyo caso la categoría 02 la modifica, el sample se rehace y esta matriz se actualiza. **Un código del conjunto cerrado no se cambia desde acá.**

## 5. Qué no sensa esta matriz

| Elemento | Quién lo verifica |
| --- | --- |
| El **100 %** de los tipos ejercitados contra el servicio real, gate `QG-05`, **bloqueante** | La batería de integración de `GeometriaFactory-Api`, por `TC-21`. **Ningún sample la sustituye** |
| `RT-06`, el despliegue conjunto ante un cambio incompatible | El gate `QG-08` y la disciplina del pull request de la etapa; su detección tardía está catalogada como `DXC-08` |
| El resumen por alumno y por estado, `US-10` | `TC-11`, declarado **fuera del tramo comprometido** |
| Las **once** restricciones transversales, una por una | Los **veintidós** casos de prueba de `TC-01` a `TC-22` |

**Las tres sondas no reemplazan a la batería de integración**: la anteceden. Cuando `GeometriaFactory-Api` exista, las tres siguen valiendo, porque miden la superficie del ensamblado y no el comportamiento del servicio.

## 6. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **Actualización de trazabilidad al resolver el informe `G-10-Examples-Siete-Proyectos-r1.md` 1.0.** El `README.md` de [`../10-Examples/`](../10-Examples/), del que esta matriz toma sus contratos, pasó a **1.1** al corregir sus puntos abiertos falsos sobre el `PRODUCT-INTAKE` §16.1 y §18; la trazabilidad upstream lo cita ahora en esa versión. Las carpetas de `/samples` que el **P0-1** reclamaba **ya existen**, esqueletadas con su README local y su comando previsto, de modo que el «método de verificación» de cada fila apunta a una ruta que resuelve. **Ninguna fila, contrato, umbral ni estado cambia**, y las sondas siguen en `Sin verificar` sin fecha. Contrastado contra el texto vivo del `PRODUCT-INTAKE` **1.25**, en particular §16.1 y §18, y no contra lo que otro documento dice de ellas. Sube minor. |
| 1.0 | 2026-08-11 | Emisión inicial, abierta por AG-10 al cerrar la fase que genera la categoría 10, que es el segundo momento de sensado de `Deriva-Rules.md` §4. Cierra el hueco que [`README.md`](README.md) §3 y [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §7 declararon como condicionado y temporal. Declara **tres** filas, `SD-01` a `SD-03`, una por contrato de verificación, con el comando del contrato como método, el campo `evidencia` del sample como evidencia esperada, el umbral de la fila «Contratos y comportamiento» de `Deriva-Rules.md` §3 y estado `Sin verificar` sin fecha. Declara la precisión de que estas sondas detectan el cambio que **compila igual** y cambia la frontera, no el incompatible que rompe la compilación, y qué queda fuera de su alcance, empezando por el gate bloqueante `QG-05`. |
