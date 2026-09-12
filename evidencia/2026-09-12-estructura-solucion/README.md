# El bundle lo genera el `.csproj`, y todo entra al árbol de solución

Cierre de la Feature 20 del framework (`IA.SDD.Documentacion/PROMPTs/Features/20-Agregar-BundleJS-Estructura-Solucion/OUPUTs/Especificacion-Estructura-Solucion.md` 3.1, §7.1), tras dos ciclos de mesa evaluadora.

## Lo que se corrigió, medido

- **Un clon limpio construía el front sin visor y sin que nada fallara.** Ahora `dotnet publish` en una invocación genera el bundle, lo copia y lo lista en el manifiesto de recursos estáticos (§1 de `verificacion.md`); la segunda construcción no vuelve a correr `npm ci` (§2); sin Node y sin bandera falla en vez de callar (§3); con `-p:SkipVisorBuild=true` construye sin Node (§4 y §5).
- **Cuatro lugares generaban el mismo bundle.** Queda uno: el target `BuildVisor` de `GeometriaFactory.Web.csproj` (`Web ADR-10008`).
- **Sin sello de caché.** El `<script>` del visor lleva `?v=<sha256 del bundle>` generado en la construcción, servido antes del guardián de aprovisionamiento (§10).
- **Todo bajo el árbol de solución** (decisión del Product Owner, 2026-09-11): el visor como nodo sin construcción, los nueve `Sample.*.csproj` construidos con la solución, `E2ETests` visible y corrido por su ruta. `QG-03` medido antes y después: **igual** (§7). Veinte proyectos en `GeometriaFactory.sln` (§5).
- **El conjunto de samples del visor no ejercía la única vía del visor hacia el anfitrión.** `visor/02-intermedio` suma el acto `[15]`, selección desde la escena por `onPieceSelected` (§9).
- **`samples/README.md` decía «Esqueleto — sin código»** desde el 2026-08-11 y enlazaba a una ruta inexistente. Estado real por carpeta y enlaces corregidos.

## Lo que NO cambió

`visor/` sigue en la raíz (apartamiento vigente); la arista `Visor → Web` sigue siendo un activo de construcción; `build-visor.sh` sigue siendo el ciclo corto del visor; ninguna puerta cambia de umbral.
