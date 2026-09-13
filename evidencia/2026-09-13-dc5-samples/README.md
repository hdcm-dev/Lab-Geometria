# Los veinte samples bajo el árbol de solución (DC-5)

Completa la decisión del Product Owner del 2026-09-11 —«todos los proyectos quedan bajo el árbol de solución de
Visual Studio, aunque sea bajo carpetas virtuales»— sobre la rama `estructura/dc5-samples-en-la-solucion`,
base `main` = `1ce1b2c`. Corridas del 2026-09-13 en `mcr.microsoft.com/dotnet/sdk:10.0`, con el padre montado
en la misma ruta absoluta.

## Lo que faltaba, medido

`find samples -mindepth 2 -maxdepth 2 -type d` da **veinte** carpetas; `dotnet sln list` sobre `main` nombraba
**nueve** de ellas (`sln-list-antes.txt`). La puerta nueva, corrida sobre `main` sin cambios, lo dice carpeta
por carpeta: **once** sin nodo, salida 1 (`verify-sobre-main.txt`). Diez existían antes de `6cc6f86`, cuyo
mensaje decía «todo entra al árbol de solución»; `api/04-cliente-http-basico` entró el 2026-09-13.

## Archivos

| Archivo | Qué muestra |
| --- | --- |
| `sln-list-antes.txt` | `dotnet sln list` sobre `main`: 20 proyectos, nueve samples |
| `sln-list-despues.txt` | `dotnet sln list` en la rama: 31 proyectos, veinte samples |
| `arbol-anidado.txt` | Las carpetas de solución y lo que cuelga de cada una, leído de `NestedProjects` |
| `verify-sobre-main.txt` | La puerta sobre `main`: once FALLA, salida 1 |
| `verify-verde.txt` | La puerta en la rama: salida 0 |
| `verify-fallando-en-copias.txt` | La puerta sobre copias con cinco defectos inyectados: salida 1 en los cinco |
| `build-y-tests.txt` | `dotnet build` Release con `-p:SkipVisorBuild=true` y `dotnet test --no-build` |
| `cobertura-antes.txt` · `cobertura-despues.txt` | `QG-03` y `QG-04` de `scripts/coverage.sh` antes y después, con su `diff` en `verificacion.md` |
| `verificacion.md` | Resumen de las corridas |

## Lo que NO cambió

`src/`; los nueve `Sample.*.csproj` construidos; los comandos con que se corre cada sample; las configuraciones de
solución (`Debug|Any CPU`, `Release|Any CPU`, iguales a `main`); los umbrales de las puertas.
