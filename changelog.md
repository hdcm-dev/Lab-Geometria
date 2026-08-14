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

---

## Etapa `b` — Navegación y sistema visual

**Rama:** `codigo/etapa-b-navegacion`

### Agregado

- **Las once superficies de `Linea-Base-Visual.md` §2 como pantallas de marcador de posición**,
  con su ruta, su shell, su título y su subtítulo tomados de la maqueta aprobada. Nueve tienen
  ruta; `SUP-08` se aloja dentro de `SUP-07` y `SUP-11` se superpone a los dos shells, las dos
  **sin ruta**, tal como la línea de base manda —darle ruta propia a `SUP-08` es deriva mayor—.
  `SUP-04` lleva tres rutas, una por cada uno de sus tres cursos, porque cambian de shell.
- **La cáscara de navegación con los dos shells** de `Experiencia-De-Uso.md` §3.2: `AccessShell`,
  sin navegación, y `WorkShell`, con la barra lateral de **los tres destinos del papel y ninguno
  del otro**, la identidad de la persona, el cierre de sesión y el sello de versión al pie.
- **El sistema visual portado desde la maqueta aprobada** a `wwwroot/css/app.css`: los cincuenta
  tokens, **idénticos nombre por nombre y valor por valor**, y las 165 clases traducidas al
  inglés. **Ningún literal de color fuera del bloque `:root`.**
- **La raíz `/`**, que hasta ahora daba 404: es el punto donde corre el guardián de
  aprovisionamiento de `NAV-01` y `NAV-03`, con sus dos destinos a un clic mientras el guardián
  no exista.
- `scripts/verify-visual-system.sh` y `scripts/verify-navigation.sh`, las dos puertas de los dos
  criterios de transición de la etapa, con cuatro controles de pasa/falla cada una.
- `Norma-De-Nomenclatura.md` **1.6** §6.12: los **216** identificadores que esta etapa necesitó
  —24 tipos de componente, 13 miembros, 9 iconos y 170 nombres de clase CSS—, agregados **antes**
  de escribirlos, por el corolario 4 de §6.1 y fuera de los 155.

### Decidido en esta etapa, y elevado al punto de control

- **`V-5` sigue abierto, y la etapa `b` decide NO introducir MudBlazor todavía.** El fundamento
  está en `GeometriaFactory.Web.csproj`, con su medición: la maqueta aprobada carga Bootstrap por
  CDN y **no usa una sola de sus clases**, de modo que el sistema visual adoptado no es el de
  ninguna biblioteca de componentes, y traer una segunda cascada es lo que el criterio de
  transición prohíbe. La decisión es de esta etapa, no de la fuente.
- **Las direcciones de las rutas son propuesta de esta etapa.** Ninguna fuente las declara. Las
  seis del shell de trabajo usan los identificadores de destino que la maqueta ya tiene en
  `DESTINOS` (`EV-04`); las de acceso derivan del nombre canónico de la superficie.
- **La pantalla de la dirección que no existe es propuesta declarada y no lleva `SUP-XX`.**

### No hecho, y declarado

- **Ninguna superficie tiene comportamiento.** La etapa `b` no implementa lógica de negocio, no
  llama al servicio de datos y no tiene formularios que hagan nada.
- **`/estado` se conserva** y queda fuera de la línea de base visual: su hoja se movió a
  `wwwroot/css/scaffold.css` y sólo la carga esa página.
- El tercer curso de `SUP-04` —cambio forzado, capacidad `F-26`— **se construye desde el
  wireframe vigente y se rotula en pantalla como no validado visualmente**, porque
  `Linea-Base-Visual.md` §6.1 declara que nadie lo miró y no le asigna `NAV-XX` ni `CMP-XX`.
