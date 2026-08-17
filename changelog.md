# Registro de cambios — Fábrica de Geometría

**Quién lo actualiza:** el **equipo de desarrollo**, en la rama de la etapa (`PRODUCT-INTAKE` §2).
**Quién verifica que se actualizó:** el **Product Owner**, en la revisión del pull request de la
etapa, que es el punto de control bloqueante (`PRODUCT-INTAKE` §15).
**Cuándo:** **en la rama de la etapa, no después de la fusión** (intake §16 y §17.5.P.7).

**Cuando este documento y el historial del repositorio no coinciden, gana el historial** y la
diferencia se repara acá, nunca al revés. El detalle está en
[`SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/09-Devops/Estrategia-Versionado.md`](SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/09-Devops/Estrategia-Versionado.md) §11.

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

---

## Etapa `f` — Importación y validación

**Rama:** `codigo/etapa-f-validador`
Escrita **en la rama de la etapa**, como manda el intake §16, y completada a medida que la etapa
avanzó. **Los cinco criterios de la transición `e` → `f` estaban cerrados desde la etapa anterior; lo
que esta etapa cierra son los ocho de la `f` → `g`, menos `PT-02` y `PT-03`, que se miden antes de
comprometer la `g` y no para cerrar ésta.**

### Agregado

- **El validador de figuras**, que es la mitad de riesgo del producto: el intake declara en su §11
  que el defecto que más veces se repite es **escribir el validador sin leer el análisis** (`RN-B3`),
  con probabilidad alta e impacto alto, y que la consecuencia es que «la aplicación no sirve para el
  dato que existe». Su mitigación declarada es la batería obligatoria, y por eso la batería se
  escribió **con los ocho escenarios del intake §20 como fixtures y antes que ninguna otra cosa**.
- **Las tres entidades que las cinco etapas anteriores dejaron declaradas y vacías**: `Piece`,
  `Component` y `Observation`, con los atributos de `Definicion-Modelo-De-Dominio.md` §2.3, §2.4 y
  §2.5. Con eso **las cinco entidades del dominio quedan modeladas**.
- **Dos conjuntos cerrados nuevos**, `FigureType` y `ComponentRole`, que son los atributos «Tipo» y
  «Papel» del modelo. El tipo tiene **siete** valores y no seis: `RectanguloDesarrollado` es un
  discriminante del texto y **no es un tipo de pieza**, porque aparece sólo como componente.
- **El puerto de validación de figuras, con su único miembro.** Un solo miembro para las dos mitades
  del contrato —interpretar y verificar—: el consumidor pide una interpretación y recibe las
  observaciones de las dos especies juntas, porque lo que el dominio necesita para resolver el
  estado es el conjunto completo.
- **`LocalFigureValidator`**, el adaptador, con las cuatro trampas del formato **declaradas de
  entrada y no descubiertas después**: `T1`, las claves `Tapas` y `Bases` como sinónimas del
  ortoedro —la línea que desbloquea el dibujo de todos los ortoedros que el visualizador previo
  pierde—; `T2`, la lectura con tolerancia a comas finales, porque el texto del alumno **no es JSON
  estrictamente válido** y eso es un hecho del producto; `T3`, la cara del cubo aceptada como
  `Cuadrado` y como `Rectangulo`; y `T4`, los valores calculados erróneos **señalados, nunca
  corregidos ni rechazados**.
- **La batería obligatoria: quince pruebas sobre los diez casos** de `RT` §11 más el décimo de §21,
  con los ocho textos transcriptos **carácter por carácter** del intake —comas finales, sangría
  irregular y `"3,50"` entre comillas incluidos—. **Ningún dato de prueba se inventó**, que es la
  regla de delivery 5 de §15.
- **El operador estricto de la tolerancia, anclado en una prueba y no sólo en la prosa**: una
  diferencia de 0.010 no advierte y una de 0.011 sí. Es lo que hace que `E-1` devuelva **dos**
  advertencias y no tres, porque el área del cilindro difiere en exactamente 0.01.
- **La orquestación del envío**, que es lo que convierte al validador en producto: el envío
  interpreta el texto, **adopta** el resultado en el trabajo y **deja que el dominio resuelva el
  estado** con RN-05. Vale para el alta y para la reedición, porque **enviar es la única acción de
  guardado** del alumno.
- **`Work.AdoptInterpretation`** (`Domain BT-13`), la operación que la etapa `e` declaró pendiente.
  **Reemplaza y no acumula**, y rechaza cuatro cosas que serían defectos del validador y no del
  alumno: una posición fuera del rango del conjunto raíz, una especie desconocida, un error sin
  ubicación y una advertencia sin los dos valores. **Los cuatro códigos ya estaban en el glosario:
  cero filas nuevas**, que es la cuarta etapa seguida en que eso ocurre.
- **Las tres tablas restantes del modelo de datos** —`Pieza`, `Componente` y `Observacion`— con su
  transformación de esquema, sus arrastres y el índice único de trabajo y posición. **Con ellas las
  cinco tablas del modelo existen**, y la transformación de la etapa `c` **no se editó**.
- **El cuarto puerto conectado.** Con `IFigureValidator` ⟶ `LocalFigureValidator`, el cuadre de
  `QG-10` queda **completo por primera vez desde la etapa `a`**: los cuatro puertos declarados
  tienen adaptador.
- **La colección de observaciones en el contrato**, que la etapa `e` declaró ausente anunciando que
  entraba acá. Entra como el cambio **compatible** que aquella emisión anticipó.
- **La pantalla que se las muestra al alumno**, con la forma que la maqueta aprobada ya tenía
  diseñada —`gf-findings`, `gf-finding-location` y la grilla `gf-values`— esperando a la etapa que
  la poblara: **no se inventó ni una clase**. Cada observación se lee con su figura, su posición y
  su campo, y la advertencia muestra **los dos valores uno al lado del otro**.
- `Norma-De-Nomenclatura.md` **1.18** §6.21: los **44** identificadores que esta etapa necesitó —5
  tipos, 12 valores de conjunto cerrado y 27 miembros—, agregados **antes** de escribirlos, por el
  corolario 4 de §6.1.

### Decidido en esta etapa, y elevado al punto de control

- **De dónde sale la altura del ortoedro, que es lo único que las fuentes no enuncian como regla.**
  Los dos escenarios que la fijan **la ponen en claves distintas**: en `E-1` y `E-2` el lateral es
  `Largo 21 · Ancho 7` sobre bases de `7 · 7` y el volumen derivado declarado es `7·7·21`, con la
  altura en `Largo`; en `E-7` el lateral es `Largo 6 · Ancho 8` sobre bases de `6 · 4` y el intake
  declara «altura = `Laterales[0].Ancho` = 8». La regla que satisface a los dos, y la única que se
  sostiene geométricamente, es que **la altura es la dimensión del lateral que no es un lado de la
  base**. Tomar siempre `Largo` rompe `E-7`; tomar siempre `Ancho` rompe `E-1`, que es el caso de
  prueba canónico del producto.
- **Un conjunto de componentes incompleto no se suma.** El ortoedro de `E-8` trae `Bases` y no trae
  `Laterales`: sumar lo que hay daría 48.00 contra 208.00 declarados y emitiría **una advertencia
  que ninguna fuente pide**, sobre una diferencia que no es del alumno sino de lo que su texto no
  incluyó. Sin el conjunto completo, el área no se deriva y no se compara.
- **El texto del mensaje no es un atributo de la observación.** El modelo declara cuatro atributos y
  ninguno es una frase: la observación lleva la especie, la posición, el campo y los dos valores, y
  quien la redacta para la persona es la pieza pública. Guardar una frase acá la ataría al idioma y
  a la redacción del día en que se escribió. Hay una prueba que lo fija.
- **Los escenarios `E-3` y `E-4` traen una figura suelta y no un array**, y se aceptan así: el
  conjunto raíz de un texto de ese tipo tiene una figura. Rechazarlos dejaría **dos casos de la
  batería obligatoria sin poder ejecutarse**.
- **Las dos observaciones que no son de ninguna figura** —conjunto raíz vacío y texto ilegible—
  llevan el campo `Texto`. `CU-06001` FA-03 y FA-04 declaran que van sin posición y no dicen con qué
  campo; RN-02009 exige campo, y ponerle el nombre de una clave afirmaría que el defecto está en un
  campo que nadie leyó.
- **La batería vive en el proyecto de pruebas de integración y no en uno propio.** `CU-06001` §3
  exige poder ejercerla sin motor de persistencia, y así se ejerce: no levanta ningún host, no abre
  ninguna base y no toca la red. Agregar un cuarto proyecto de pruebas es una decisión de estructura
  que esta etapa no toma por su cuenta.
- **La puerta de dominio de la etapa `e` se relevó por tercera vez, y es la última.** Exigía que las
  tres entidades del texto del alumno siguieran sin atributos, y **se puso en rojo al escribir el
  primero**: hizo exactamente lo que su comentario prometía. En su lugar quedan dos puertas nuevas
  con contenido —que las tres entidades sólo se escriban por sus propias fábricas, y que la
  observación no lleve mensaje redactado—.

### Encontrado al construir, y corregido

- **Un defecto que sólo aparecía en el segundo envío de un trabajo.** El repositorio traía el
  trabajo **sin sus colecciones**, de modo que la interpretación anterior no se borraba: el segundo
  envío intentaba insertar una pieza en una posición que la del primero seguía ocupando, y el
  índice único la rechazaba. La primera entrega de cada trabajo funcionaba perfecto. Lo encontró la
  prueba de superficie de la reedición, que es la única que envía dos veces el mismo trabajo.
- **Dos fixtures que decían ser escenarios del intake y no lo eran.** Las dos pruebas de trabajos de
  la etapa `e` llamaban `ScenarioE2` a un objeto con una clave `Figuras` que **ninguna fuente
  transcribe**. Pasó desapercibido porque hasta la etapa `e` el texto no se interpretaba y a la
  fixture sólo se le pedía llegar entera al almacén. Se reemplazan por escenarios reales: es la
  regla de delivery 5 de §15, y la etapa que interpreta es la primera que puede notar la diferencia.

### No hecho, y declarado
- **`PT-02` y `PT-03` no se midieron**: la transición `f` → `g` las exige **antes de comprometer la
  etapa `g`**, no para cerrar ésta.
- **La visualización no se toca**: el dibujo es de la etapa `g`. La pantalla de envío **sigue sin
  ofrecer «previsualizar»**, porque un control que no dibuja nada es una promesa incumplida.
- **Tres apartamientos del modelo de datos, declarados y elevados**, los tres con el mismo
  fundamento —el propio §1 del modelo de datos declara que **manda el modelo del dominio** cuando
  los dos difieren—: la pieza **no lleva columna de dimensiones**, porque el modelo del dominio
  enumera siete atributos y ninguno lo es; el componente lleva **tres columnas** en lugar de una de
  texto, porque las claves del emisor son un conjunto cerrado y chico; y la posición de la
  observación es **nulable**, porque hay dos observaciones que no son de ninguna figura y con la
  columna obligatoria **no se podrían guardar**, que son justamente las únicas que el alumno tiene
  para entender qué pasó cuando su texto no se pudo leer.
- **Cuántos tipos reconstruibles hay sigue siendo punto abierto aguas arriba.** Los seis de
  `Definicion-Contrato-Del-Validador-De-Figuras.md` §5 son los que los escenarios ejercitan; el
  análisis menciona siete clases en `Ejemplo1` y diez en `Ejemplo2`, y **ninguna fuente las
  enumera**. Un tipo fuera de los seis produce error de validación, que es correcto y puede no ser
  lo deseado. Lo resuelve el Product Owner con la enumeración de las clases de la Actividad 1.

## Etapa `g` — Visualización 3D

*En curso. Esta sección se completa al cerrar la etapa; lo que sigue son los cambios ya hechos.*

### La superficie HTTP pasa a describirse a sí misma
- **Documento OpenAPI generado** en `/openapi/v1.json` y **explorador navegable** en
  `/documentacion`, por decisión del Product Owner. Revierte la renuncia declarada en el intake
  §17.5 —«se renuncia a un contrato descrito en OpenAPI»— y su fundamento está en
  `ADR-08008`: lo que la renuncia evitaba era **un segundo contrato escrito a mano que se
  desincroniza**, y un documento **generado de los puntos que ya existen** no lo es. No se genera
  ningún cliente y el contrato entre las dos piezas **sigue siendo el ensamblado**
  `GeometriaFactory.Contracts`.
- **El explorador no se publica solo.** En desarrollo está siempre; fuera de desarrollo hace falta
  decir `Documentacion__Publicada=true`. El servicio de datos se expone a Internet y un explorador
  **enumera todos los puntos ante cualquiera que abra la dirección**: puede estar bien —es una API
  de laboratorio— pero es una decisión de quien despliega y no un efecto de agregar un paquete.
- **Ningún punto cambia.** `A-01` a `A-18` son los mismos, con las mismas formas y los mismos
  verbos. Lo que se agrega **los describe sin tocarlos**.
- **Los activos del explorador se sirven desde el propio servicio**, no desde una red de terceros:
  el servicio no adquiere ninguna dependencia de tiempo de ejecución hacia afuera.
- **`ApiDocumentationSurfaceTests`, cuatro pruebas, y la que importa es la del cierre**: que en
  desarrollo se vea lo nota cualquiera la primera vez que lo abre; que **deje de verse al
  desplegar** no lo nota nadie hasta que ya está publicado.

### La imagen se sella con su propia revisión
- **El Dockerfile deriva el commit del `.git` del contexto** en lugar de recibirlo por argumento.
  `SOURCE_REVISION_ID` queda como respaldo, para el tarball sin `.git` o para forzar una revisión.
- **El motivo es que la falla anterior no tenía síntoma.** Con la revisión como argumento a mano,
  actualizar el código sin actualizar la variable dejaba al servicio informando por `/salud` una
  revisión que no era la suya, **andando perfecto**. Un error que no se ve es peor que uno que
  rompe.
- **La copia es tolerante a la ausencia** —`COPY .gi[t] ./.git/`—: un patrón que no encuentra nada
  no rompe la construcción, así que el caso sin `.git` sigue funcionando con el argumento.
- **Consecuencia para el despliegue**: la composición del host puede apuntar a una **rama** y
  `docker compose up -d --build` actualiza de verdad, con el sello correcto, sin dos variables que
  mantener parejas.

### El árbol del texto — la otra mitad de la etapa `g`
- **`F-11` cumplida**: la estructura del texto se ve como árbol colapsable, en la previsualización
  del alumno y en la vista del trabajo, la suya y la del administrador.
- **El árbol se arma del TEXTO y no de las piezas**, y es la decisión que lo define. El intake §20
  lo exige: «muestra las dos piezas, **incluida la que no se dibujó**. Se lee lo que el alumno
  escribió, no lo que la escena logró representar». Una figura que falla **no produce pieza**, así
  que un árbol derivado de `Pieces` la haría desaparecer justo cuando el alumno la busca.
- **Lo arma el validador**, que es el único componente que lee el texto. Armarlo en la pieza
  pública la volvería un segundo intérprete: dos códigos leyendo lo mismo con criterios que se
  separan el día que uno cambia.
- **Un mismo componente para las dos pantallas** (`JsonTree`), como los wireframes exigen, para que
  una corrección no haya que hacerla dos veces.
- **El número se muestra como se escribió**: `2.50` no se muestra `2.5`. Y **un número entre
  comillas se ve como texto**, que es muchas veces la explicación de por qué esa figura no se
  reconstruyó.
- **En el trabajo guardado el árbol se deriva al leer** y las piezas no, y la asimetría está
  declarada: las piezas son el resultado de la evaluación y reinterpretarlas dejaría que la vista
  muestre algo distinto de lo que el producto decidió; el árbol no evalúa nada, y guardarlo crearía
  una segunda copia del texto capaz de decir otra cosa.
- **`F-13` intacta**: los nodos de las figuras del conjunto raíz llevan la misma posición, sin
  traducir, y siguen sincronizando con la escena en las dos direcciones.
- **9 pruebas nuevas**, y la que justifica que el árbol exista es la de la figura que falla: todo lo
  demás lo daría igual un árbol derivado de las piezas.

### Un defecto que sólo aparecía llegando desde el menú
- **La escena 3D no se dibujaba nunca al entrar desde un enlace**, y sí al escribir la dirección o
  recargar. La navegación mejorada de Blazor parcha el DOM y **no vuelve a ejecutar los `script` de
  la página nueva**; el bundle del visor se sirve sólo en las superficies que dibujan —lo exige el
  inventario cerrado de guiones—, así que llegando desde el listado o desde el menú no estaba
  cargado.
- **Por qué no lo vio nadie**: las pruebas piden la página directamente y `PT-02` se midió igual.
  Fallaba exactamente por el camino que usa la persona. Lo encontró la verificación del árbol, en
  un navegador de verdad.
- **Arreglo**: los enlaces a las dos superficies que dibujan piden carga real de documento. En el
  menú lateral el dato es del destino —`Draws`—, no del marcado, para que agregar una superficie
  que dibuje obligue a decidirlo.
- **Y queda una red**: una prueba de la batería comprueba que esos enlaces la lleven. Que el
  documento después dibuje lo sigue midiendo `PT-02`, con navegador.
