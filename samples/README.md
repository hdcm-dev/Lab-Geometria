# `/samples` — muestras ejecutables de Fábrica de Geometría

**Producto:** Fábrica de Geometría
**Repositorio:** `Lab-Geometria`
**Estado de todas las carpetas:** **Esqueleto — sin código**
**Fecha:** 2026-08-11

Esta carpeta es la materialización en código de la categoría **10-Examples** de los siete proyectos de código del producto. La categoría documenta el sample; el código vive acá, y **se gobierna desde §16.1 del `PRODUCT-INTAKE`** (`Rules-Examples.md` §2.1).

---

## 1. Qué hay hoy, y qué no

Hay **diecinueve** carpetas, una por cada markdown explicativo emitido, cada una con **su README local y su comando previsto**. Es exactamente lo que `Rules-Examples.md` §0.2 asigna a la **pasada de diseño**: «las carpetas de `/samples` quedan esqueletadas, con su README local y su comando previsto».

**No hay código, y ninguna carpeta afirma lo contrario.** Los diecinueve comandos previstos **todavía no resuelven**: los proyectos ejecutables, los guiones y los archivos de escenario los produce la **pasada de ejecución**, durante la codificación. Coherentemente, los diecinueve contratos de verificación declaran `evidencia` en `No verificado — sin código`, sin fecha y sin salida, y las diecinueve filas `VER-XX` de las matrices de sensado nacen en `Sin verificar`.

## 2. Un segmento por proyecto de código, y por qué

`Rules-Examples.md` §2.3 fija la estructura de `/samples` por tipo D8 —`/samples/01-basico-consola/`, `/samples/01-datos-seed/` y demás— **suponiendo un proyecto de código por repositorio**. Este producto tiene **siete** proyectos de código en un solo repositorio (`PRODUCT-INTAKE` §13 y §16), de modo que las carpetas base colisionarían entre ellos.

Se agrega, por eso, **un nivel de espacio de nombres por proyecto de código**: `/samples/<proyecto>/<XX-slug>/`. Es **carpeta extra y no renombre de las base**, que es lo único que §2.3 admite ajustar, y está declarado en la sección «Estructura de `/samples` y su desvío declarado» de los siete `README.md` de la categoría 10. Los slugs son todos de la lista cerrada de §3.1 y ninguno está atado al dominio.

## 3. Las diecinueve carpetas

| Carpeta | Proyecto de código | Sonda | Comando previsto | Documento que la gobierna |
| --- | --- | --- | --- | --- |
| [`domain/01-basico/`](domain/01-basico/) | GeometriaFactory-Domain | `VER-01` · `SD-01` | `dotnet run --project samples/domain/01-basico` | [`ejemplo-01-basico.md`](../SDD/Docs/Proyectos/GeometriaFactory-Domain/10-Examples/ejemplo-01-basico.md) |
| [`domain/02-intermedio/`](domain/02-intermedio/) | GeometriaFactory-Domain | `VER-02` · `SD-02` | `dotnet run --project samples/domain/02-intermedio` | [`ejemplo-02-intermedio.md`](../SDD/Docs/Proyectos/GeometriaFactory-Domain/10-Examples/ejemplo-02-intermedio.md) |
| [`domain/03-avanzado/`](domain/03-avanzado/) | GeometriaFactory-Domain | `VER-03` · `SD-03` | `dotnet run --project samples/domain/03-avanzado` | [`ejemplo-03-avanzado.md`](../SDD/Docs/Proyectos/GeometriaFactory-Domain/10-Examples/ejemplo-03-avanzado.md) |
| [`contracts/01-basico/`](contracts/01-basico/) | GeometriaFactory-Contracts | `VER-01` · `SD-01` | `dotnet run --project samples/contracts/01-basico` | [`ejemplo-01-basico.md`](../SDD/Docs/Proyectos/GeometriaFactory-Contracts/10-Examples/ejemplo-01-basico.md) |
| [`contracts/02-intermedio/`](contracts/02-intermedio/) | GeometriaFactory-Contracts | `VER-02` · `SD-02` | `dotnet run --project samples/contracts/02-intermedio` | [`ejemplo-02-intermedio.md`](../SDD/Docs/Proyectos/GeometriaFactory-Contracts/10-Examples/ejemplo-02-intermedio.md) |
| [`contracts/03-avanzado/`](contracts/03-avanzado/) | GeometriaFactory-Contracts | `VER-03` · `SD-03` | `dotnet run --project samples/contracts/03-avanzado` | [`ejemplo-03-avanzado.md`](../SDD/Docs/Proyectos/GeometriaFactory-Contracts/10-Examples/ejemplo-03-avanzado.md) |
| [`application/01-basico/`](application/01-basico/) | GeometriaFactory-Application | `VER-01` · `SD-01` | `dotnet run --project samples/application/01-basico` | [`ejemplo-01-basico.md`](../SDD/Docs/Proyectos/GeometriaFactory-Application/10-Examples/ejemplo-01-basico.md) |
| [`application/02-intermedio/`](application/02-intermedio/) | GeometriaFactory-Application | `VER-02` · `SD-02` | `dotnet run --project samples/application/02-intermedio` | [`ejemplo-02-intermedio.md`](../SDD/Docs/Proyectos/GeometriaFactory-Application/10-Examples/ejemplo-02-intermedio.md) |
| [`application/03-avanzado/`](application/03-avanzado/) | GeometriaFactory-Application | `VER-03` · `SD-03` | `dotnet run --project samples/application/03-avanzado` | [`ejemplo-03-avanzado.md`](../SDD/Docs/Proyectos/GeometriaFactory-Application/10-Examples/ejemplo-03-avanzado.md) |
| [`infrastructure/01-basico/`](infrastructure/01-basico/) | GeometriaFactory-Infrastructure | `VER-01` · `SD-01` | `dotnet run --project samples/infrastructure/01-basico` | [`ejemplo-01-basico.md`](../SDD/Docs/Proyectos/GeometriaFactory-Infrastructure/10-Examples/ejemplo-01-basico.md) |
| [`infrastructure/02-intermedio/`](infrastructure/02-intermedio/) | GeometriaFactory-Infrastructure | `VER-02` · `SD-02` | `dotnet run --project samples/infrastructure/02-intermedio` | [`ejemplo-02-intermedio.md`](../SDD/Docs/Proyectos/GeometriaFactory-Infrastructure/10-Examples/ejemplo-02-intermedio.md) |
| [`infrastructure/03-avanzado/`](infrastructure/03-avanzado/) | GeometriaFactory-Infrastructure | `VER-03` · `SD-03` | `dotnet run --project samples/infrastructure/03-avanzado` | [`ejemplo-03-avanzado.md`](../SDD/Docs/Proyectos/GeometriaFactory-Infrastructure/10-Examples/ejemplo-03-avanzado.md) |
| [`api/01-basico/`](api/01-basico/) | GeometriaFactory-Api | `VER-01` · `SD-01` | `bash samples/api/01-basico/run.sh` | [`ejemplo-01-basico.md`](../SDD/Docs/Proyectos/GeometriaFactory-Api/10-Examples/ejemplo-01-basico.md) |
| [`api/02-intermedio/`](api/02-intermedio/) | GeometriaFactory-Api | `VER-02` · `SD-02` | `bash samples/api/02-intermedio/run.sh` | [`ejemplo-02-intermedio.md`](../SDD/Docs/Proyectos/GeometriaFactory-Api/10-Examples/ejemplo-02-intermedio.md) |
| [`api/03-avanzado/`](api/03-avanzado/) | GeometriaFactory-Api | `VER-03` · `SD-03` | `bash samples/api/03-avanzado/run.sh` | [`ejemplo-03-avanzado.md`](../SDD/Docs/Proyectos/GeometriaFactory-Api/10-Examples/ejemplo-03-avanzado.md) |
| [`visor/01-basico/`](visor/01-basico/) | GeometriaFactory-Visor | `VER-01` · `SD-13` | `bash scripts/build-visor.sh && npm --prefix samples/visor/01-basico run verify` | [`ejemplo-01-basico.md`](../SDD/Docs/Proyectos/GeometriaFactory-Visor/10-Examples/ejemplo-01-basico.md) |
| [`visor/02-intermedio/`](visor/02-intermedio/) | GeometriaFactory-Visor | `VER-02` · `SD-14` | `bash scripts/build-visor.sh && npm --prefix samples/visor/02-intermedio run verify` | [`ejemplo-02-intermedio.md`](../SDD/Docs/Proyectos/GeometriaFactory-Visor/10-Examples/ejemplo-02-intermedio.md) |
| [`visor/03-avanzado/`](visor/03-avanzado/) | GeometriaFactory-Visor | `VER-03` · `SD-15` | `bash scripts/build-visor.sh && npm --prefix samples/visor/03-avanzado run verify` | [`ejemplo-03-avanzado.md`](../SDD/Docs/Proyectos/GeometriaFactory-Visor/10-Examples/ejemplo-03-avanzado.md) |
| [`web/01-datos-seed/`](web/01-datos-seed/) | GeometriaFactory-Web | `VER-01` · `SD-62` | `bash samples/web/01-datos-seed/run.sh` | [`ejemplo-01-datos-seed.md`](../SDD/Docs/Proyectos/GeometriaFactory-Web/10-Examples/ejemplo-01-datos-seed.md) |

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
| 1.0 | 2026-08-11 | Emisión inicial de la carpeta `/samples`, en la **pasada de diseño** de `Rules-Examples.md` §0.2. Se crean las **diecinueve** carpetas esqueletadas, cada una con su README local y su comando previsto, y ninguna con código. Resuelve el **P0-1** del informe `SDD/Docs/Audit/G-10-Examples-Siete-Proyectos-r1.md` 1.0, que había verificado que las carpetas no existían mientras los siete `README.md` de la categoría 10 afirmaban haberlas dejado esqueletadas. Declara el desvío de estructura respecto de `Rules-Examples.md` §2.3 —un segmento por proyecto de código, porque el producto tiene siete en un repositorio—, la correspondencia uno a uno con los diecinueve contratos y sus diecinueve sondas, la ubicación de las tres muestras nombradas del `PRODUCT-INTAKE` **1.25** §18, y la convención `.txt` de los archivos de escenario. |
