# Registro de cambios — Fábrica de Geometría

Se actualiza **en la rama de la etapa, no después de la fusión** (intake §16 y §17.5.P.7).

## Etapa `a` — Esqueleto ambulante y verificación de viabilidad

**Rama:** `codigo/etapa-a-andamiaje`

### Agregado

- `GeometriaFactory.sln` con los **seis** proyectos de código .NET bajo `src/` y los **tres**
  proyectos de prueba bajo `tests/`, en el orden topológico de `Pipeline-Producto.md` §2.
- `GeometriaFactory.Domain`: las cinco entidades **sin atributos ni invariantes** y los cuatro
  conjuntos cerrados con sus diez valores. Cero dependencias salientes.
- `GeometriaFactory.Contracts`: `ServiceHealth`, el cuerpo de la respuesta del punto de salud.
  Cero referencias hacia `Domain`.
- `GeometriaFactory.Application`: los **cuatro** puertos declarados. Una sola dependencia saliente.
- `GeometriaFactory.Infrastructure`: `GeometriaFactoryDbContext` con modelo vacío y
  `StorePreparation`.
- `GeometriaFactory.Api`: host delgado, `CompositionRoot`, `TwoPhaseStartup` y `HealthEndpoint`,
  que realiza `A-16` fuera de la guardia.
- `GeometriaFactory.Web`: armazón Blazor Interactive Server, `DataServiceClient` —la única
  salida— y la página de estado `Status.razor`, que consume el punto de salud.
- `visor/`: paquete `geometriafactory-visor` con la fachada de las **seis** funciones y sin
  lógica de dibujo. Capa 3 vacía.
- Los **siete** guiones de `scripts/`, `.devcontainer/`, `.vscode/`, `deploy/` y el flujo de
  publicación del front con su filtro de **tres** rutas.
- Apartamientos declarados en `SDD/Docs/Producto/Plan-Etapa-A.md` §2.3: `Directory.Build.props`
  y `.editorconfig` (`AP-01`), ampliación del `.gitignore` (`AP-02`), la página de estado fuera
  de la línea de base visual (`AP-03`) y `visor/dist/` generado y no versionado (`AP-04`).

### Decidido en esta etapa, y elevado al punto de control

- La lectura de **ocho aristas** de `X-1`: `GeometriaFactory.Api` declara sus tres referencias.
- El riesgo `R-02` —`Infrastructure BT-05` de etapa `a` mapeando entidades de etapa `c`— queda
  resuelto **a favor de la etapa `c`** por decisión del Product Owner: la etapa `a` no modela
  las entidades, no crea los cinco mapeos y no genera ninguna transformación de esquema.
- El riesgo `R-03` —el cuerpo de la respuesta de salud sin tipo declarado— se cubre con
  `ServiceHealth` y sus tres miembros, **propuesta sin base declarada**.
- El riesgo `R-04` —la ruta del punto de salud— se toma de `Definicion-Superficie-HTTP.md` §3,
  que la da como `/salud` y la marca derivada.

### No hecho, y declarado

- No se ancla la biblioteca de componentes de interfaz: su versión exacta es la decisión `V-5`.
- No se genera `visor/package-lock.json`: exige resolver dependencias contra el registro de
  paquetes, y el entorno donde se escribió esta etapa no tiene red ni Node.
