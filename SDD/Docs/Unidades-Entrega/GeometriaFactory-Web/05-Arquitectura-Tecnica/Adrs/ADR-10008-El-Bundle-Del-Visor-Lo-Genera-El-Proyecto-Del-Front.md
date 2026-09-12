# ADR-10008 — El bundle del visor lo genera el proyecto del front, y es el único que lo genera

**Unidad de entrega:** GeometriaFactory-Web
**Documento:** ADR-10008-El-Bundle-Del-Visor-Lo-Genera-El-Proyecto-Del-Front.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-09-12
**Autor:** Arquitecto de Software Senior (AG-05), sobre los hallazgos de la mesa evaluadora de la Feature 20 del framework (2026-09-11)
**Categoría:** Despliegue

---

## 1. Contexto

La arista `Visor → Web` es una dependencia de compilación real —sin el bundle el front no dibuja— que no es una referencia de proyecto: un paquete Node y un proyecto .NET no se referencian (`PRODUCT-MANIFEST` §3). Hasta hoy la materializaba `scripts/build-visor.sh`, invocado **a mano desde cuatro lugares**: `scripts/build.sh`, el flujo `e2e.yml`, `scripts/pruebas-e2e.sh` y la etapa 1 de `deploy/Dockerfile.web`. Tres hechos medidos el 2026-09-11 motivan esta decisión:

1. **Un clon limpio construía un front sin visor y sin que nada fallara**: `dotnet build GeometriaFactory.sln` terminaba en 0 con `wwwroot/js/` vacío (`deploy/Dockerfile.web`, comentario de su etapa 1).
2. **Cuatro generadores del mismo artefacto**, cuando el propio repositorio había escrito el criterio contrario en `deploy-front-ftp.yml`: «un solo lugar desde donde se genera el mismo artefacto».
3. **Un archivo generado en `wwwroot/` durante la construcción no entra al manifiesto de recursos estáticos en la primera construcción** si se lo declara como contenido estático, porque los ítems se evalúan antes de que corra ningún target. Reproducido con el SDK 10.0.400: el endpoint faltaba en la primera construcción y aparecía en la segunda.

Motivación upstream: `PRODUCT-INTAKE` §17.2.P.8 · GeometriaFactory-Web y · GeometriaFactory-Visor; `Vista-Producto.md` §3.1 (la clase «activo de construcción»); [`ADR-12006`](ADR-12006-Bundle-Generado-Y-Versionado-Del-Punto-De-Extension.md) (el artefacto nunca se edita a mano); [`ADR-10007`](ADR-10007-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md) (el bundle se genera en el mismo flujo, nunca de un artefacto viejo).

## 2. Decisión

**`GeometriaFactory.Web.csproj` es el único generador del bundle**, con dos targets:

- `BuildVisor`, antes de compilar, ejecuta `scripts/build-visor.sh` **sólo cuando cambió una fuente del visor** (`Inputs` sobre `visor/src/**/*.ts`, el archivo de bloqueo y la configuración del empaquetador; `Outputs` sobre `wwwroot/js/geometriafactory-visor.js`), y se apaga con `-p:SkipVisorBuild=true`.
- `DeclareVisorAsset`, después, **declara el archivo como contenido dentro del target**, para que entre al manifiesto de recursos estáticos en la misma invocación; excluye el ítem si el glob por omisión ya lo trajo.

**Donde no hay Node se pasa la bandera y se recibe el bundle hecho**: la etapa de compilación de `deploy/Dockerfile.web` conserva la etapa `node:22` y publica con `SkipVisorBuild=true`. Sin la bandera y sin Node el target **falla** (`npm: command not found`), y es lo que debe hacer: nunca callar.

**Las invocaciones directas del guion se retiran**: `build.sh`, `e2e.yml`, `pruebas-e2e.sh` y el aviso del banco local de las pruebas de extremo a extremo pasan a decir quién genera el bundle. `build-visor.sh` queda como lo que el target ejecuta y como ciclo corto de trabajo sobre el visor.

**La clase de la arista no cambia**: `Visor → Web` sigue siendo un activo de construcción; lo que cambia es que ahora la expresa un `.csproj` con un target, y ya no un guion que alguien tiene que acordarse de correr.

## 3. Estado

**Aprobado** desde 2026-09-12, con la corrida que lo verifica (`evidencia/2026-09-12-estructura-solucion/`).

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| **Seguir con el guion invocado a mano** (estado anterior) | Cero cambios | Un clon limpio construye sin visor sin fallar; cuatro generadores |
| **Un nodo de proyecto `Microsoft.Build.NoTargets` dueño del build, referenciado por el front** | Dueño único natural; el consumidor sólo declara el archivo; reproducido y funciona | Convierte el `.sln` en dueño de `npm` en todo `build`/`test`; con la copia física en `wwwroot/js/` presente falla con `MSB4018` (clave duplicada en la compresión); cambia la clase de la arista y exige reescribir el manifiesto §3. **Queda como alternativa condicionada** a que dos consumidores del mismo repositorio construyan el mismo bundle, caso que no existe |
| **Copiar el bundle al repositorio** (versionarlo) | Sin Node en ningún ambiente | Contradice `ADR-12006` y `Visor/09` §2: el artefacto no se versiona |
| **Target en el consumidor con `Content` declarado dentro del target** (elegida) | Un solo generador; incremental; el consumidor expresa la arista; el manifiesto de recursos estáticos lo lista desde la primera construcción | Node pasa a ser prerrequisito de `build`/`test` del front salvo bandera; `npm ci` en cada disparo |

## 5. Consecuencias positivas

- `dotnet build GeometriaFactory.sln` desde un clon limpio produce el front **con** visor, en una invocación, y `dotnet publish` lo lista con huella de contenido en el manifiesto de recursos estáticos.
- Un solo lugar desde donde se genera el artefacto, que es el criterio que el repositorio ya había escrito.
- El segundo `build` no vuelve a correr `npm ci`: el target se salta cuando las fuentes no cambiaron.

## 6. Consecuencias negativas y trade-offs

- **Node es prerrequisito de todo `dotnet build`/`test` que alcance al front** (`Integration.Tests` lo referencia), salvo `SkipVisorBuild=true`. El devcontainer, los tres flujos y la imagen `e2e` ya lo traen; la etapa `sdk:10.0` del `Dockerfile.web` usa la bandera.
- `npm ci` borra y reinstala `node_modules` en cada disparo del target (sólo cuando cambia una fuente).
- Las advertencias que emita el `Exec` **no** se convierten en error por `TreatWarningsAsErrors`; sólo `-warnaserror` lo haría. `QG-01` sigue midiendo la construcción .NET; el empaquetado del visor sale hoy sin advertencias.

## 7. Implementación

- `src/GeometriaFactory.Web/GeometriaFactory.Web.csproj`: targets `BuildVisor` y `DeclareVisorAsset`, con su comentario.
- `deploy/Dockerfile.web`: `-p:SkipVisorBuild=true` en `publish`; la etapa `node:22` se conserva.
- `scripts/build.sh`, `scripts/pruebas-e2e.sh`, `.github/workflows/{ci,e2e,deploy-front-ftp}.yml`, `tests/GeometriaFactory.E2ETests/Infraestructura/BancoLocal.cs`: se retira la invocación directa o se reescribe el aviso.
- Criterio de verificación: clon limpio, `dotnet publish src/GeometriaFactory.Web -c Release -o <dir>` en una sola invocación deja `<dir>/wwwroot/js/geometriafactory-visor.js` y su `.staticwebassets.endpoints.json` lo lista; segunda construcción sin `npm ci`; sin `npm` en el PATH y sin bandera, falla; con bandera, 0 advertencias y 0 errores.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Lugares que invocan `build-visor.sh` fuera del `.csproj` | Exactamente **0** | `grep -rn build-visor.sh scripts .github tests` |
| Front publicado desde clon limpio sin bundle | Exactamente **0** casos | El criterio de §7 en cada publicación |
| Construcciones consecutivas sin cambios que reinstalen `node_modules` | **0** | `dotnet build -v:n` muestra `Skipping target "BuildVisor"` |

## 9. Referencias

- [`ADR-12006`](ADR-12006-Bundle-Generado-Y-Versionado-Del-Punto-De-Extension.md) — el artefacto es generado y nunca se edita a mano.
- [`ADR-10007`](ADR-10007-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md) — el bundle se genera en el mismo flujo que publica.
- `Vista-Producto.md` §3.1 — las dos clases de arista.
- `IA.SDD.Documentacion/PROMPTs/Features/20-Agregar-BundleJS-Estructura-Solucion/OUPUTs/Especificacion-Estructura-Solucion.md` 3.1, P-3 y §9 — la mesa que lo decidió, con sus reproducciones.

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-09-12 | Emisión inicial, con la corrida que la verifica. Cierra la observación de `Vista-Producto.md` 1.9 de que ningún `.csproj` podía expresar la arista `Visor → Web`. |
