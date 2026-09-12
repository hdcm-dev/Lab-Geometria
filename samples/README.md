# `/samples` — muestras ejecutables de Fábrica de Geometría

**Producto:** Fábrica de Geometría
**Repositorio:** `Lab-Geometria`
**Estado:** **dieciséis implementadas y corriendo; tres en esqueleto** (ver §1 y §3)
**Fecha:** 2026-09-12

Esta carpeta es la materialización en código de la categoría **10-Examples** de los siete proyectos de código del producto. La categoría documenta el sample; el código vive acá, y **se gobierna desde §16.1 del `PRODUCT-INTAKE`** (`Rules-Examples.md` §2.1).

---

## 1. Qué hay hoy, y qué no

Hay **diecinueve** carpetas, una por cada contrato de verificación declarado. **Dieciséis tienen código y
corren**: se implementaron el 2026-08-30 (`SDD/Docs/Audit/Reporte-Hallazgos-De-Los-Samples-2026-08-30.md`
§2: cinco cierran exactos contra su §6, once con divergencias declaradas) y **doce se volvieron a correr el
2026-09-12** al entrar a la solución (§3). Las **tres de `contracts/`** siguen en esqueleto —README local y
comando previsto— y no tienen documento gobernante propio: la categoría 10 se emite por **unidad de
entrega** (`GeometriaFactory-Api` y `GeometriaFactory-Web`), y `Contracts` es proyecto de código de las
dos.

**Los nueve `Sample.*.csproj` entran a `GeometriaFactory.sln` desde el 2026-09-12** y se construyen con
ella: es la forma de cumplir `Rules-Examples.md` §3.4 («su CI debe garantizar que siempre compila contra la
versión actual del producto»), y la decisión del Product Owner de que todo proyecto quede bajo el árbol de
solución. El temor de que sus ensamblados movieran `QG-03` se midió antes y después con `scripts/coverage.sh`:
**la cobertura no cambia** (`evidencia/2026-09-12-estructura-solucion/`).

## 2. Un segmento por proyecto de código, y por qué

`Rules-Examples.md` §2.3 fija la estructura de `/samples` por tipo D8 —`/samples/01-basico-consola/`, `/samples/01-datos-seed/` y demás— **suponiendo un proyecto de código por repositorio**. Este producto tiene **siete** proyectos de código en un solo repositorio (`PRODUCT-INTAKE` §13 y §16), de modo que las carpetas base colisionarían entre ellos.

Se agrega, por eso, **un nivel de espacio de nombres por proyecto de código**: `/samples/<proyecto>/<XX-slug>/`. Es **carpeta extra y no renombre de las base**, que es lo único que §2.3 admite ajustar, y está declarado en la sección «Estructura de `/samples` y su desvío declarado» de los siete `README.md` de la categoría 10. Los slugs son todos de la lista cerrada de §3.1 y ninguno está atado al dominio.

## 3. Las diecinueve carpetas

| Carpeta | Proyecto de código | Sonda | Comando previsto | Documento que la gobierna | Estado (última corrida) |
| --- | --- | --- | --- | --- | --- |
| [`domain/01-basico/`](domain/01-basico/) | GeometriaFactory-Domain | `VER-01` · `SD-01` | `dotnet run --project samples/domain/01-basico` | [`ejemplo-01-basico-dominio.md`](../SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/10-Examples/ejemplo-01-basico-dominio.md) | CONFORME 10/10 · 2026-09-12 |
| [`domain/02-intermedio/`](domain/02-intermedio/) | GeometriaFactory-Domain | `VER-02` · `SD-02` | `dotnet run --project samples/domain/02-intermedio` | [`ejemplo-02-intermedio-dominio.md`](../SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/10-Examples/ejemplo-02-intermedio-dominio.md) | CONFORME 13/13 · 2026-09-12 |
| [`domain/03-avanzado/`](domain/03-avanzado/) | GeometriaFactory-Domain | `VER-03` · `SD-03` | `dotnet run --project samples/domain/03-avanzado` | [`ejemplo-03-avanzado-dominio.md`](../SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/10-Examples/ejemplo-03-avanzado-dominio.md) | CONFORME 13/13 · 2026-09-12 |
| [`contracts/01-basico/`](contracts/01-basico/) | GeometriaFactory-Contracts | `VER-01` · `SD-01` | `dotnet run --project samples/contracts/01-basico` | sin documento propio (ver §1) | Esqueleto — sin código |
| [`contracts/02-intermedio/`](contracts/02-intermedio/) | GeometriaFactory-Contracts | `VER-02` · `SD-02` | `dotnet run --project samples/contracts/02-intermedio` | sin documento propio (ver §1) | Esqueleto — sin código |
| [`contracts/03-avanzado/`](contracts/03-avanzado/) | GeometriaFactory-Contracts | `VER-03` · `SD-03` | `dotnet run --project samples/contracts/03-avanzado` | sin documento propio (ver §1) | Esqueleto — sin código |
| [`application/01-basico/`](application/01-basico/) | GeometriaFactory-Application | `VER-01` · `SD-01` | `dotnet run --project samples/application/01-basico` | [`ejemplo-01-basico-aplicacion.md`](../SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/10-Examples/ejemplo-01-basico-aplicacion.md) | CONFORME 12/12 · 2026-09-12 |
| [`application/02-intermedio/`](application/02-intermedio/) | GeometriaFactory-Application | `VER-02` · `SD-02` | `dotnet run --project samples/application/02-intermedio` | [`ejemplo-02-intermedio-aplicacion.md`](../SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/10-Examples/ejemplo-02-intermedio-aplicacion.md) | CONFORME 14/14 · 2026-09-12 |
| [`application/03-avanzado/`](application/03-avanzado/) | GeometriaFactory-Application | `VER-03` · `SD-03` | `dotnet run --project samples/application/03-avanzado` | [`ejemplo-03-avanzado-aplicacion.md`](../SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/10-Examples/ejemplo-03-avanzado-aplicacion.md) | CONFORME 15/15 · 2026-09-12 |
| [`infrastructure/01-basico/`](infrastructure/01-basico/) | GeometriaFactory-Infrastructure | `VER-01` · `SD-01` | `dotnet run --project samples/infrastructure/01-basico` | [`ejemplo-01-basico-infraestructura.md`](../SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/10-Examples/ejemplo-01-basico-infraestructura.md) | CONFORME 13/13 · 2026-09-12 |
| [`infrastructure/02-intermedio/`](infrastructure/02-intermedio/) | GeometriaFactory-Infrastructure | `VER-02` · `SD-02` | `dotnet run --project samples/infrastructure/02-intermedio` | [`ejemplo-02-intermedio-infraestructura.md`](../SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/10-Examples/ejemplo-02-intermedio-infraestructura.md) | CONFORME 14/14 · 2026-09-12 (almacén propio) |
| [`infrastructure/03-avanzado/`](infrastructure/03-avanzado/) | GeometriaFactory-Infrastructure | `VER-03` · `SD-03` | `dotnet run --project samples/infrastructure/03-avanzado` | [`ejemplo-03-avanzado-infraestructura.md`](../SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/10-Examples/ejemplo-03-avanzado-infraestructura.md) | CONFORME 18/18 · 2026-09-12 (almacén propio) |
| [`api/01-basico/`](api/01-basico/) | GeometriaFactory-Api | `VER-01` · `SD-01` | `bash samples/api/01-basico/run.sh` | [`ejemplo-01-basico-api.md`](../SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/10-Examples/ejemplo-01-basico-api.md) | 11/13 con divergencias declaradas · 2026-08-30 (reporte §2); exige `reset-db` + `run-api` + clave por entorno |
| [`api/02-intermedio/`](api/02-intermedio/) | GeometriaFactory-Api | `VER-02` · `SD-02` | `bash samples/api/02-intermedio/run.sh` | [`ejemplo-02-intermedio-api.md`](../SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/10-Examples/ejemplo-02-intermedio-api.md) | 22/23 · 2026-08-30 (íd.) |
| [`api/03-avanzado/`](api/03-avanzado/) | GeometriaFactory-Api | `VER-03` · `SD-03` | `bash samples/api/03-avanzado/run.sh` | [`ejemplo-03-avanzado-api.md`](../SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/10-Examples/ejemplo-03-avanzado-api.md) | 9/10 · 2026-08-31 (reporte 6.0) |
| [`visor/01-basico/`](visor/01-basico/) | GeometriaFactory-Visor | `VER-01` · `SD-13` | `bash scripts/build-visor.sh && npm --prefix samples/visor/01-basico run verify` | [`ejemplo-01-basico.md`](../SDD/Docs/Unidades-Entrega/GeometriaFactory-Web/10-Examples/ejemplo-01-basico.md) | CONFORME 9/9 · 2026-09-12 |
| [`visor/02-intermedio/`](visor/02-intermedio/) | GeometriaFactory-Visor | `VER-02` · `SD-14` | `bash scripts/build-visor.sh && npm --prefix samples/visor/02-intermedio run verify` | [`ejemplo-02-intermedio.md`](../SDD/Docs/Unidades-Entrega/GeometriaFactory-Web/10-Examples/ejemplo-02-intermedio.md) | CONFORME 16/16 · 2026-09-12 (documento 2.1, acto `[15]`) |
| [`visor/03-avanzado/`](visor/03-avanzado/) | GeometriaFactory-Visor | `VER-03` · `SD-15` | `bash scripts/build-visor.sh && npm --prefix samples/visor/03-avanzado run verify` | [`ejemplo-03-avanzado.md`](../SDD/Docs/Unidades-Entrega/GeometriaFactory-Web/10-Examples/ejemplo-03-avanzado.md) | CONFORME 17/17 · 2026-09-12 |
| [`web/01-datos-seed/`](web/01-datos-seed/) | GeometriaFactory-Web | `VER-01` · `SD-62` | `bash samples/web/01-datos-seed/run.sh` | [`ejemplo-01-datos-seed.md`](../SDD/Docs/Unidades-Entrega/GeometriaFactory-Web/10-Examples/ejemplo-01-datos-seed.md) |
 | 13/13 sin divergencias · 2026-08-30 (reporte §2); exige `run-api` y `reset-db` |
**Diecinueve carpetas y diecinueve contratos**, en correspondencia uno a uno: 3 de Domain, 3 de Contracts, 3 de Application, 3 de Infrastructure, 3 de Api, 3 del Visor y 1 de Web. 3 × 6 + 1 = 19.

## 4. Las tres muestras nombradas del `PRODUCT-INTAKE` §18 viven acá adentro

§18 nombra tres demostraciones por su papel, y **no son el conjunto de las carpetas** (`PRODUCT-INTAKE` 1.25 §18):

| Muestra | Qué es | Dónde vive en esta carpeta |
| --- | --- | --- |
| `S-1` | Página integradora sin backend, que prueba el punto de extensión | Las **tres** carpetas de [`visor/`](visor/), que son sus tres partes |
| `S-2` | Colección de peticiones HTTP de la API | [`api/02-intermedio/`](api/02-intermedio/) |
| `S-3` | Juego de datos de los ocho escenarios, en archivos sueltos | Los archivos de escenario de [`infrastructure/01-basico/`](infrastructure/01-basico/) |

## 5. Convención de los archivos de escenario

Los textos de los ocho escenarios del `PRODUCT-INTAKE` §20 se transcriben **sin modificación** y se guardan con extensión **`.txt` y no `.json`** en las siete carpetas que los usan. El motivo es uno solo y es verificable: el texto de `E-2` **no es JSON estrictamente válido** —trae dos comas finales— y nombrarlo `.json` invitaría a que una herramienta lo reformateara al abrirlo, rompiendo la comparación carácter por carácter que `RN-08` exige y que varias sondas verifican.

## 6. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 2.0 | 2026-09-12 | **Estado real por carpeta y enlaces corregidos.** La cabecera decía «Esqueleto — sin código» desde el 2026-08-11 mientras dieciséis carpetas corrían desde el 2026-08-30; §1 y la columna nueva de §3 lo dicen con la fecha de la última corrida y su fuente. Los diecinueve enlaces apuntaban a `SDD/Docs/Proyectos/…`, ruta que no existe desde la migración 8.0: pasan a `SDD/Docs/Unidades-Entrega/<unidad>/10-Examples/`, y `contracts/*` declara que no tiene documento propio. Registra la entrada de los nueve `Sample.*.csproj` a `GeometriaFactory.sln` con `QG-03` medido antes y después. Lo pidió la mesa evaluadora de la Feature 20 del framework. Sube **major**: cambia el estado declarado de dieciséis carpetas. |
| 1.0 | 2026-08-11 | Emisión inicial de la carpeta `/samples`, en la **pasada de diseño** de `Rules-Examples.md` §0.2. Se crean las **diecinueve** carpetas esqueletadas, cada una con su README local y su comando previsto, y ninguna con código. Resuelve el **P0-1** del informe `SDD/Docs/Audit/G-10-Examples-Siete-Proyectos-r1.md` 1.0, que había verificado que las carpetas no existían mientras los siete `README.md` de la categoría 10 afirmaban haberlas dejado esqueletadas. Declara el desvío de estructura respecto de `Rules-Examples.md` §2.3 —un segmento por proyecto de código, porque el producto tiene siete en un repositorio—, la correspondencia uno a uno con los diecinueve contratos y sus diecinueve sondas, la ubicación de las tres muestras nombradas del `PRODUCT-INTAKE` **1.25** §18, y la convención `.txt` de los archivos de escenario. |
