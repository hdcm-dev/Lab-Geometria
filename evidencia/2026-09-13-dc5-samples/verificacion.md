# Corridas del 2026-09-13 (`mcr.microsoft.com/dotnet/sdk:10.0`, padre montado en la misma ruta absoluta)

## 1. `dotnet sln list`: 20 → 31

Antes (`main` = `1ce1b2c`), nueve samples de veinte: `sln-list-antes.txt`. Después, los veinte: `sln-list-despues.txt`
(`proyectos: 31`). Árbol anidado leído de `NestedProjects`: `arbol-anidado.txt`.

`GlobalSection(SolutionConfigurationPlatforms)` comparada con `diff` contra `git show main:GeometriaFactory.sln`: **igual**
(`Debug|Any CPU`, `Release|Any CPU`). El `.sln` se editó a mano con GUID nuevos, sin `dotnet sln add`.

## 2. La puerta `scripts/verify-solution-tree.sh`

| Corrida | Salida | Archivo |
| --- | --- | --- |
| Sobre `main` sin cambios | **1** · once carpetas sin archivo de proyecto | `verify-sobre-main.txt` |
| En la rama | **0** · 20 carpetas revisadas | `verify-verde.txt` |
| Copia sin el nodo de `api/04-cliente-http-basico` | **1** · A-2 | `verify-fallando-en-copias.txt` |
| Copia con `samples/api/05-otro` sin nodo | **1** · A-2 | íd. |
| Copia con `Sample.Web.DatosSeed` sin anidar | **1** · A-3 | íd. |
| Copia con `Debug\|x64` inyectada | **1** · A-6 (dos FALLA) | íd. |
| Copia con `Exec` enganchado a `Build` en `Sample.Visor.Basico` | **1** · A-5 | íd. |

`verify-explicit-configuration.sh` da las mismas dos fallas en la rama que sobre `main` (`coverage.sh:49`, `e2e.yml:106`):
**preexistentes**, ninguna de este cambio.

## 3. `QG-01` y `QG-02`

`dotnet build GeometriaFactory.sln -c Release -p:SkipVisorBuild=true`: **0 Warning(s), 0 Error(s)**, los once nodos en la
salida. `dotnet test … --no-build`: **94 + 56 + 396 = 546/546**, como `main`. Detalle en `build-y-tests.txt`.

Los nodos no producen ensamblado: **0** `.dll` bajo `bin/` u `obj/` de `samples/{visor,api,contracts,web}`.
`api/03-avanzado/almacenes/almacen.cs`, aplicación de un solo archivo con el nodo en la carpeta de arriba, corre igual con
`dotnet run <archivo>` (`--componer-roto` salida 0; lectura del linaje salida 0).

**La primera construcción falló** (`MSB4025` en los tres nodos del visor): el comentario decía `npm --prefix`, y un comentario
XML no admite `--`. Se corrigió antes de todo lo de arriba.

## 4. `QG-03` y `QG-04`: la cobertura no cambia, y hay que correrla con el mismo estado de `App_Data/claves/`

Con árbol fresco, sin el anillo de claves del front:

```text
proyecto                           main (líneas · ramas)  rama (líneas · ramas) 
GeometriaFactory.Api               1316/1348 · 213/241    1316/1348 · 213/241   
GeometriaFactory.Application       493/516 · 124/148      493/516 · 124/148     
GeometriaFactory.Contracts         142/142 · 0/0          142/142 · 0/0         
GeometriaFactory.Domain            332/346 · 139/160      332/346 · 139/160     
GeometriaFactory.Infrastructure    1658/1717 · 210/258    1658/1717 · 210/258   
GeometriaFactory.Web               1504/1828 · 622/980    1504/1828 · 624/980   
```

**Líneas: iguales en los seis proyectos.** Ramas: `Web` 622 → 624; la segunda corrida de `main` fresco también dio 624, es
variación entre corridas y no del cambio. `QG-04`: 396 · 150 · 546 en las dos.

**Lo que casi se registra como hallazgo.** La primera medición después del cambio dio `GeometriaFactory.Web` **1491/1828** contra
**1504/1828** de `main`, reproducible dos veces. El `diff` por línea de los informes Cobertura puso las trece líneas en
`Services/StartupObservations.cs`: el cuerpo de `Anotar` y del `Log` del proveedor, que sólo corren si el marco emite un
aviso en el arranque. El aviso es el de la **creación** del anillo de claves, que `Program.cs` persiste en
`src/GeometriaFactory.Web/App_Data/claves/` (ignorado). La primera corrida en un árbol fresco crea las claves y cubre las
trece; las siguientes las encuentran y no. Medido en los dos sentidos:

| Árbol | Anillo de claves | `Web` líneas |
| --- | --- | --- |
| `main` | ausente | 1504 |
| `main` | presente | **1491** |
| rama | presente | 1491 |
| rama | ausente | **1504** |

Corridas con claves en `cobertura-con-claves-persistidas.txt`. **Es probablemente también por qué la evidencia del 2026-09-12
dio `Web` 1491 antes y después**: medía un árbol ya usado. No se corrige acá (`src/` y `coverage.sh` quedan fuera del
alcance): queda anotado como condición de medición de `QG-03`.
