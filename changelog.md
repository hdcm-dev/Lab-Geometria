# Registro de cambios — Fábrica de Geometría

Se actualiza **en la rama de la etapa, no después de la fusión** (intake §16 y §17.5.P.7).

> ## Las etapas `c`, `d` y `e` se repusieron el 2026-08-16, después de la fusión
>
> **La regla de la línea de arriba se incumplió tres veces seguidas.** Las etapas `c`, `d` y `e` se
> construyeron y se fusionaron a `main` entre el 2026-08-14 y el 2026-08-15 **sin que ninguna
> actualizara este documento**, que es el único que declara el avance de la construcción. Este
> registro quedó afirmando la etapa `b` mientras el código llegaba a la `e`.
>
> **Lo encontró el orquestador de reanudación** al contrastar este documento contra el historial del
> repositorio, y lo declaró como la divergencia `D-01` de
> [`SDD/Docs/Audit/Estado-Del-Destino-2026-08-16.md`](SDD/Docs/Audit/Estado-Del-Destino-2026-08-16.md) §2.
> Sin ese contraste, una sesión limpia habría concluido que faltaba arrancar la `c` y habría
> reconstruido tres etapas ya hechas.
>
> **Qué se hizo, y qué no.** Las tres entradas de abajo se escribieron **el 2026-08-16, desde los
> commits y desde el código**, no desde la memoria de las sesiones que las construyeron. Se marcan
> como repuestas en lugar de presentarse como si se hubieran escrito a tiempo: un registro que
> disimula cuándo se escribió no sirve para lo que este documento existe. **No se reescribió ni un
> commit**, y ningún contenido de las tres entradas se infirió: cada afirmación sale de un mensaje de
> commit o de un archivo del árbol.
>
> **La regla no cambia y sigue siendo la de la primera línea.** Lo que cambia es que ahora hay un
> antecedente de qué pasa cuando no se cumple, y un contraste —el historial del repositorio— que lo
> detecta la próxima vez.

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

---

## Etapa `c` — Administrador: alta inicial y sesión

**Ramas:** `codigo/etapa-b-limpiar-andamiaje`, `codigo/etapa-c-dominio`, `codigo/cambio-forzado-alcanzable`, `codigo/sesion-por-marca-de-navegador`
**Repuesta el 2026-08-16 desde los commits y el código.**

### Agregado

- **La primera rebanada vertical del producto**, con los cuatro criterios de la transición
  `c` → `d` verificados **corriendo los dos servicios**: el administrador se configura una sola vez
  y el segundo intento choca contra el índice único; entrar, cambiar la contraseña exigiendo la
  actual y salir funcionan, y el cambio **sobrevive al reinicio del servicio**; el esquema se aplica
  solo sobre una base que no existía, con su tabla, su índice y su registro de transformaciones; y
  la credencial de sesión **no es observable desde el navegador**.
- **La sesión por marca de navegador**, la mitad de `Web ADR-03` §2 que la etapa `c` no había
  construido: identificador opaco (`HttpOnly`, `Secure`, `SameSite=Strict`) en el navegador y el
  testigo firmado en un almacén del lado del servidor. Sin ella la sesión no sobrevivía a una
  recarga ni a una pestaña nueva.
- **El guardián 2 de `ADR-03`**: sin sesión, las siete rutas del panel desvían a `/ingreso`.
- **`A-05` con dos formas de autenticar** (intake 1.34): con acceso firmado, la cuenta es la del
  acceso; sin acceso, **la contraseña vigente autentica** y la cuenta tiene que tener la marca de
  cambio pendiente. Es lo que vuelve alcanzable la pantalla del cambio forzado.
- **La pieza pública se niega a arrancar sin la dirección del servicio de datos**, y la composición
  del servicio de datos exige la ruta del almacén.
- Las pantallas dejan de hablar en jerga del proyecto: se limpia el andamiaje de la etapa `b`.

### Decidido en esta etapa, y elevado al punto de control

- **La interactividad pasa a ser global, y es una corrección estructural obligada.** Con
  interactividad por página cada navegación abre un circuito nuevo y el estado de sesión se pierde
  en cada salto: la decisión de arquitectura de llevar la credencial en el circuito **era
  irrealizable**, y la única salida habría sido acercarle la credencial al navegador.
- **La página de ingreso deja de ser interactiva y hace un POST de verdad**, que es lo único que
  puede escribir una cabecera. El cierre de sesión pasa a POST por lo mismo. El resto de las
  páginas siguen siendo interactivas de servidor.
- **Se reconcilian tres fuentes que se contradecían** sobre si `A-05` viaja siempre con acceso
  firmado (`Api CU-01` §6, `CU-03` §3, `Definicion-Superficie-HTTP` §3).
- **Apartamiento a confirmar:** la pantalla del cambio forzado lleva cuatro campos y el wireframe
  dibuja tres, declarado en el componente y en `Wireframes-Credencial-Propia.md` 1.5. Ese curso ya
  venía sin validación visual.

### No hecho, y declarado

- **No se manejó un navegador de verdad inspeccionando el almacenamiento en vivo.** Lo que sí está
  cubierto por construcción es que **no existe código capaz de escribir ahí**.
- **El paseo sin sesión por las pantallas sobrevive como puerta de servicio que sólo rige en
  desarrollo**, con prueba de que no abre nada fuera de ahí.
- El reciclado del proceso del hosting —marca viva, almacén vacío— se atiende borrando la marca y
  desviando con el motivo declarado, que es el costo que `ADR-03` §6.1 ya aceptaba por escrito.

---

## Etapa `d` — Alumno: registro, habilitación, primer ingreso y reseteo de credencial

**Ramas:** `codigo/etapa-d-ciclo-de-vida-de-cuenta`, `codigo/etapa-d-panel`
**Repuesta el 2026-08-16 desde los commits y el código.**

### Agregado

- **Nueve de los diez criterios de la transición `d` → `e`**: registro sin elegir contraseña,
  habilitación, bloqueo, rehabilitación, baja con confirmación y reseteo de la credencial.
- **La contraseña provisoria la produce el sistema y nadie la escribe.** Doce caracteres, sin los
  que se confunden al dictarla, **sin ningún parámetro de entrada**: no se deriva del correo, del
  nombre ni de la fecha. Mil generaciones sin repetir. En el almacén va derivada; el claro se
  muestra **una vez y no se guarda**.
- **El panel de cuentas, sin ni un campo de contraseña**, verificado **contando sobre el marcado
  servido**: cero.
- **`RN-13` con dientes**: la marca de cambio pendiente corta el acceso **incluso con un acceso
  firmado obtenido antes del reseteo**. La guardia es un intermediario y no un filtro por punto,
  porque el defecto a impedir es olvidarse de un punto.
- **`RN-15`**: resetear **no es una transición de la máquina de estados**. Procede sobre una cuenta
  `Bloqueado` y sobre una `Pendiente` sin cambiarles la situación, leída del almacén antes y después.
- **`INV-08` en la superficie**: ninguna de las cuatro operaciones procede sobre la cuenta de
  administrador, con prueba que **fuerza la petición**. Su fila no se dibuja: no es un control
  deshabilitado, es una acción que no existe.
- **La confirmación de la baja la exige también la superficie HTTP**, no sólo la pantalla: sin eso
  una petición forzada borraría una cuenta y todos sus trabajos sin confirmación.
- **Las cuatro asperezas del panel**, autorizadas por el Product Owner: copiar la provisoria en un
  gesto, el aviso mientras la operación viaja, el botón de baja que espera a que el correo coincida,
  y `Escape` que cierra los diálogos, que además confinan el foco.
- **`verify-explicit-configuration.sh`**, la cuarta puerta: la configuración deja de depender del
  valor por omisión. `run-api.sh`, `run-web.sh` y `migrate.sh` construían `Release` por un lado y
  ejecutaban `Debug` por el otro.
- **El servicio de datos se niega a arrancar sin la clave de firma**, y también con una clave más
  corta que el mínimo del algoritmo. El mensaje nombra la llave de configuración y **nunca el
  valor**, ni entero ni en fragmentos, con prueba que lo fija.
- **El almacén de desarrollo sale del árbol del repositorio** y `reset-db.sh` pasa a **preguntar,
  diciendo qué archivo borra y cuánto hay adentro**. Sin nadie que pueda contestar, no borra.
- **El guardián 1 de `ADR-03`**, que nunca se había construido y arrastraba desde la etapa `c`, con
  el punto de acceso `A-17` —anónimo, de sólo lectura, un solo dato— que lo vuelve realizable.

### Decidido en esta etapa, y elevado al punto de control

- **Apartamiento de fondo, elevado:** las dos superficies del ciclo de vida pasan a **render
  estático con envío HTTP**, como el ingreso. Es lo que permite verificarlas sobre HTTP real, y lo
  que cuesta los estados en curso y el copiado en un gesto.
- **`A-17` queda a ratificación del Product Owner.** No va dentro del punto de salud: ésa la consume
  el chequeo del contenedor, y mezclarle un hecho del producto acopla dos cosas que cambian por
  motivos distintos.
- **El caché del guardián 1 es asimétrico**, y por eso no tiene vencimiento que elegir bien: el «sí»
  se recuerda para siempre porque el estado es monótono; el «no» no se recuerda ni un segundo. Con
  el servicio caído **no se desvía nada**: no saber no es lo mismo que saber que no.
- **Los guiones de desarrollo vuelven a `Debug` por decisión del Product Owner**, declarado y no por
  omisión; los de verificación se quedan en `Release` porque miden lo que se despliega. **La
  asimetría queda escrita para que nadie la «corrija» por simetría.**
- **La provisoria se muestra una vez y no se repite.** Guardarla para volver a mostrarla la
  convertiría en un valor almacenado, que es lo que `Api CU-05` §9 y `RT-02` prohíben. El remedio de
  perderla es resetear otra vez.
- Se corrige una afirmación falsa que sobrevivía en `Ingreso`: decía que recuperar la contraseña
  olvidada costaba todos los trabajos, y `RN-12` lo volvió falso el 2026-08-09 sin que el producto
  lo absorbiera.
- Cuatro decisiones más quedan elevadas al Product Owner, ninguna bloqueante.

### No hecho, y declarado

- **El criterio 10 no se cumplió y no se declaró cumplido.** Que la cuenta reseteada conserve todos
  sus trabajos **no era verificable hasta la etapa `e`**, porque los trabajos no existían. Se midió
  sólo la mitad que entonces tenía sustancia.

---

## Etapa `e` — Alta de trabajo y vista de trabajos

**Ramas:** `codigo/etapa-e-trabajos`, `codigo/etapa-e-pantallas`
**Repuesta el 2026-08-16 desde los commits y el código.**

### Agregado

- **Los cinco criterios de la transición `e` → `f`.** El trabajo se carga con nombre, fecha,
  descripción y texto, y recibe identificador y estado; queda en `Borrador` con el texto inválido y
  se reedita; el alumno elimina **sólo sus borradores**; el que pide el trabajo de otro recibe «no
  encontrado»; y el listado del administrador **excluye los borradores**.
- **Las cuatro superficies de trabajos pasan de maqueta a funcionar**: alta, listado propio con
  identificador y estado, reedición, eliminación, y el listado de la comisión agrupado por alumno.
- **Los dos criterios de seguridad se prueban forzando la petición**, sin pantalla de por medio.
  Pedir el trabajo de otro devuelve **el mismo código y el mismo cuerpo** que pedir uno inexistente,
  comparados entre sí: un «prohibido» habría confirmado que ese trabajo existe. Borrar fuera de
  `Borrador` se rechaza **con el trabajo sobreviviendo en el almacén**, comprobado en tres estados.
- **El listado del administrador se comprueba con dos borradores ajenos existiendo en el almacén y
  ausentes de la respuesta.** Que no aparecieran por no haberlos creado no habría probado nada.
- **Cierra la deuda de la etapa `d`**: el criterio 10 —que la cuenta reseteada conserve todos sus
  trabajos— pasa de pendiente a cumplido, verificado **campo por campo, leído del almacén**, sobre
  una alumna con trabajos en cuatro estados.
- **Tres de los cuatro puertos quedan conectados.** El cuarto es el validador de figuras, de la
  etapa `f`.
- `verify-navigation.sh` reescrito **sin aflojarlo**: medía que el bloque de resolución se dibujara
  con el papel leído de la dirección, que era lo que hacía la etapa `b` por no tener dato.

### Decidido en esta etapa, y elevado al punto de control

- **La pantalla dice la verdad sobre una etapa incompleta, y es la decisión de fondo de la etapa.**
  El texto de la maqueta —«El texto no verificó. Corregí tu programa»— es cierto en el producto
  terminado y **falso hoy**: acusaría al programa del alumno de algo que el laboratorio ni miró. Se
  reemplaza por uno que dice qué pasó y qué no —el trabajo quedó guardado entero, el laboratorio
  todavía no lo interpreta, y de eso no se deduce nada sobre lo que escribió—, con pruebas que fijan
  las tres cosas.
- **No se simula entrega en ninguna dirección.** Por el mismo motivo **no se dibujan controles
  muertos** —previsualizar, resumen—, ni un «sin observaciones» que afirmaría que se interpretó y no
  salió ninguna, ni columnas de piezas y advertencias cuyo valor sería **un cero inventado** mientras
  dos documentos se contradicen sobre qué número es.
- **Cinco contradicciones entre documentos quedan elevadas y sin resolver por cuenta propia.**

### No hecho, y declarado

- **El texto todavía no se interpreta**: es de la etapa `f`. **Consecuencia observable y declarada:
  en esta etapa todo trabajo queda en `Borrador`**, porque entregar exige un resultado de
  interpretación que aún no existe.
- No hizo falta ningún guion nuevo ni ningún atributo nuevo: alcanzó con los nueve ya autorizados.
