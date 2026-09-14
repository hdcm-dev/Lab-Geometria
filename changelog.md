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

**Rama:** `codigo/etapa-g-puerta`

*Cerrada. Los **siete** criterios de la transición `g` → `h` se verifican con `scripts/verify-stage-g.sh`.*

### La puerta de la etapa, que no existía

- **`scripts/verify-stage-g.sh` es nuevo, y con él la etapa `g` deja de cerrarse por lectura.** Las
  etapas `b` y `c` tenían su guion; `d`, `e`, `f` y `g` no tenían ninguno, y el orquestador de
  reanudación lo dejó declarado como estado observado. **Una puerta sin guion se verifica cuando
  alguien se acuerda**, que es la forma en que este producto ya vio degradarse otras cosas.
- **Cada criterio se mide donde se predica, y no donde es cómodo.** Cuatro —las tres figuras
  dibujadas, la disposición determinista, la ausencia de peticiones y la sincronización por índice—
  ocurren **dentro de la escena**, y ninguna prueba de integración puede afirmarlos: la prueba ve el
  marcado que se sirve, no la escena que el navegador construye. Van a
  `visor/verification/stage-g.mjs`, **con navegador de verdad**.
- **`G-7` se mide en los dos lados, y es correcto que así sea**: que los dos movimientos se gobiernen
  por separado y se detengan al arrastrar es de la escena; que la pieza pública les pase **dos
  valores de verdad** —y no uno— es del marcado.
- **`G-2` no se reescribió**: es `PT-02`, ya tenía su medición, y el guion la invoca.
- **Se mide por la fachada pública y por los píxeles del lienzo, sin agregarle al paquete una sola
  función de medición.** Las seis funciones las fijó el Product Owner, y un banco que necesitara una
  séptima para poder medir **estaría midiendo otro producto**. La disposición se comprueba comparando
  dos capturas —con las piezas pasadas **desordenadas** la segunda vez, que es lo que le da valor a la
  comparación— y el movimiento, viendo si el lienzo cambia entre dos capturas separadas en el tiempo.

### El criterio que no tenía prueba

- **`TheAdministratorOpensTheWorkAndFindsExactlyWhatTheStudentSaw`.** El criterio `G-6` —«el
  administrador abre cualquier trabajo que ve y encuentra **exactamente lo mismo** que vio el
  alumno»— era el único de los siete sin ninguna verificación.
- **Compara los portadores de dato y exige igualdad, no presencia.** Comprobar que la vista del
  administrador «tiene escena y tiene árbol» dejaría pasar el defecto que el criterio existe para
  atrapar: que tenga **otra** escena u **otro** árbol. Se comparan las piezas que bajan al visor
  —carácter por carácter— y los índices de los nodos, en su orden.
- **No se compara el documento entero, y se declara por qué**: el shell trae la identidad de quien
  mira y su barra lateral, distintas por papel. El criterio se predica del trabajo, no del marco.

### Un comentario que declaraba ausente lo que estaba doce líneas más arriba

- **`surface-interaction.js` decía «LA OTRA DIRECCIÓN NO ESTÁ… queda declarado y elevado»** sobre la
  sincronización escena → árbol, mientras el código de doce líneas antes ya la ataba con
  `onPieceSelected`. Era cierto hasta que `ADR-08007` lo cerró, y sobrevivió a su propia solución.
- Es la misma forma que este producto ya tiene registrada tres veces: **la decisión llega y la nota
  se queda**. La encontró la puerta, al ir a verificar el criterio que ese comentario negaba.

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

---

## Etapa `h` — Circuito de revisión del administrador

**Rama:** `codigo/etapa-h-revision`

*Cerrada. Los seis criterios mecánicos verificados con `scripts/verify-stage-h.sh`, y el séptimo
—«las ocho fases tienen OK explícito»— **declarado por el Product Owner el 2026-08-18**.*

> ## El alcance comprometido está cerrado
>
> Con el OK explícito de las ocho fases, **`h` queda cerrada y demostrada** y el alcance que el
> intake comprometió está completo: de `a` a `h`, con las ocho puertas verificables por guion.
>
> Es también la condición que el roadmap ponía para planificar `i…`, y esa planificación entró en
> `Roadmap-Producto.md` **1.8** el mismo día.

### El desenlace, de punta a punta

- **`F-21`, `F-23` y `F-24` cumplidas**: el administrador aprueba o rechaza un trabajo en
  `Pendiente`, deja su comentario opcional y retira cualquier trabajo que ve; el alumno ve el
  desenlace en su listado y el comentario al abrir el trabajo.
- **El dominio ya estaba.** `Work.ApplyOutcome` y `AdministratorComment` existían desde antes, con
  sus **cuatro guardas** —no terminal, sólo desde `Pendiente`, sólo administrador, desenlace del
  conjunto cerrado—. Lo que faltaba era todo lo de arriba: contrato, caso de uso, punto de acceso y
  superficie. **Ninguna guarda se reescribió en otra capa**: repetirlas habría creado un segundo
  lugar donde pueden decir otra cosa.
- **`A-15` es un solo punto para los dos desenlaces**, `POST /trabajos/{id}/desenlace`. Dos puntos
  —`/aprobar` y `/rechazar`— habrían puesto en la ruta lo que el contrato ya declara en el cuerpo, y
  habrían obligado a mantener dos caminos para las mismas cuatro guardas.
- **`WorkOutcomeName` viaja por nombre y nunca por posición**, con el mismo criterio que
  `WorkObservationKind`: un entero dejaría que agregar un valor al conjunto corriera el significado
  de los que ya viajaron.
- **La solicitud no lleva estado pretendido.** Se pide un desenlace y el dominio decide a qué estado
  lleva: un campo de estado permitiría pedir `Finalizado` sin aprobar.

### Dos motivos nuevos en la tabla única, y ningún código inventado

- `OutcomeRequiresAdministratorRole` → **`403`**, con el código `OPERATION_ADMIN_ONLY` que ya
  existía. **Facultad y no pertenencia**: el alumno sabe que su trabajo existe, es suyo y lo está
  mirando; lo que no alcanza es el papel.
- `UnknownOutcome` → **`400`**: un desenlace fuera del conjunto es un campo mal formado, no un
  estado que impida la operación.
- Los dos motivos de estado —fuera de `Pendiente` y desde un terminal— salen por
  **`WorkStateForbidsOutcome`**, con **`409`** y declarando el estado actual. **El texto no los
  distingue y es deliberado**: el dominio los separa porque son guardas distintas, pero para quien
  pide la respuesta útil es la misma y es en qué estado quedó.

### La superficie, con el bloque que la etapa `b` había dejado mudo

- **`WorkResolution.razor` pasa de marcador de posición a comportamiento.** Sus tres controles
  llaman al servicio; los dos desenlaces son **la misma solicitud con otro valor**.
- **Los tres controles se inhabilitan mientras hay una solicitud en vuelo**, y no es cosmético: los
  dos desenlaces son terminales, y el segundo clic dejaría a la persona leyendo una negativa por un
  trabajo que ella misma acaba de resolver.
- **El comentario vacío viaja como ausencia y no como cadena vacía**, para que el alumno no vea un
  bloque de comentario en blanco.
- **El aviso de falla muestra el texto del servicio tal cual**: reescribirlo en la pantalla abriría
  un segundo lugar donde el mismo motivo dice otra cosa.

### La puerta

- **`scripts/verify-stage-h.sh` corre los seis criterios mecánicos** y **declara que el séptimo no
  lo es**, en lugar de simular que lo marca. Un guion que dijera «OK» sobre «las ocho fases tienen
  OK explícito» estaría afirmando lo que sólo el punto de control puede afirmar.
- **`H-3` se verifica forzando la petición**, con el acceso firmado legítimo del alumno y sobre su
  **propio** trabajo, para que la negativa no se pueda explicar por pertenencia.
- **`H-4` ejerce los dos terminales contra los dos desenlaces**: cuatro intentos, porque un terminal
  que rechazara aprobar y aceptara rechazar seguiría siendo un terminal roto.
- **Lo que no se ve en una respuesta se lee del almacén**: el estado, el comentario y la
  desaparición del trabajo eliminado.
- **6 pruebas nuevas**, y la batería queda en **311 en verde**.

---

## Las puertas que faltaban — `d`, `e` y `f`

**Rama:** `codigo/puertas-d-e-f`

*No es una etapa: es el cierre del hueco de verificación que el orquestador de reanudación dejó
declarado el 2026-08-17. Con esto **las ocho etapas construidas tienen su puerta**.*

### Qué faltaba, y qué no

- **Los criterios ya estaban cubiertos por la batería.** Lo que faltaba era el guion que los reúne:
  cerrar `d`, `e` o `f` dependía de que alguien recordara **cuáles** pruebas mirar.
- **No se escribió ninguna prueba nueva, y es deliberado.** Si un criterio hubiera necesitado una
  prueba que no existe, la etapa no habría estado cerrada. Estos guiones **verifican que lo esté**,
  no la cierran.

| Puerta | Criterios | Pruebas que corre |
| --- | --- | --- |
| `verify-stage-d.sh` · `d` → `e` | **10** | 31 |
| `verify-stage-e.sh` · `e` → `f` | **5** | 28 |
| `verify-stage-f.sh` · `f` → `g` | **8** | 27, más `PT-02` |

### Cada criterio nombra sus pruebas, una por una

- **Es lo que vuelve auditable la puerta**: la lista es el mapa entre `Roadmap-Producto.md` §5.2 y
  la batería. Un filtro por clase habría pasado igual **sin decir qué criterio cubre qué**.
- **Y el recuento se compara contra lo pedido.** Una prueba que se renombra deja de existir para el
  filtro, y la corrida pasaría en verde **sin haberla corrido**: es el modo de falla que un filtro
  por nombre tiene, y `lib-puerta.sh` lo cierra comparando cuántas se pidieron contra cuántas
  corrieron.

### «Sin medir» dejó de confundirse con «falla»

- **`F-8` es `PT-02`, que corre con navegador en contenedor.** En un entorno sin `docker` el guion
  reportaba **FALLA**, que es inventar un defecto del producto. Ahora declara **SIN MEDIR** y sale
  con un código propio.
- **`verify-stage-g.sh` recibe la misma corrección** por el problema simétrico: sin `dotnet` no
  puede correr sus criterios de batería.
- **Las dos alternativas eran peores**: reportar falla acusa al producto de algo que no se midió, y
  saltear en silencio da por verificado lo que nadie miró.

### Una discrepancia declarada y no resuelta

**El criterio `F-1` del roadmap dice «los NUEVE casos de prueba obligatorios».** El intake **1.20**
§17.1.P.8 escribe «las **diez** pruebas del validador pasan», y `Criterios-Validacion.md` `CV-26`
dice «los **diez** casos de la batería». **El roadmap quedó con el recuento anterior a la aposición
del décimo caso.**

**El guion corre la batería entera y no elige un número.** Corregir el roadmap es un acto propio y
del Product Owner: es un criterio de transición de una etapa **ya cerrada y demostrada**, y cambiarlo
desde una puerta sería reescribir contra qué se cerró.

### Por qué hay una biblioteca compartida, y por qué no la usan las otras tres

`lib-puerta.sh` existe porque las tres puertas nuevas hacen exactamente lo mismo, y escribir ese
bucle tres veces habría creado tres lugares donde el formato y el manejo de fallas divergen. **Las
otras tres no se migran**: `verify-stage-c.sh` levanta y reinicia los dos servicios,
`verify-stage-g.sh` corre un navegador en contenedor y `verify-stage-h.sh` declara un criterio no
mecánico. Forzarlas a esta forma la habría hecho más grande que el problema.

---

## Los prerrequisitos de la fase `i` — repuesto el 2026-08-18, después de la fusión

**Rama:** `codigo/prerrequisitos-fase-i` (PR #61, fusionado el 2026-08-18)

> **Esta entrada se escribió después de la fusión, y se marca como repuesta.** La regla de la
> cabecera —*«en la rama de la etapa, no después de la fusión»*— se incumplió una vez más: la unidad
> `codigo/prerrequisitos-fase-i` cambió el flujo de publicación y la composición de verificación
> **sin escribir acá**, y este registro quedó afirmando que la última unidad era «Las puertas que
> faltaban».
>
> **Lo encontró la tercera reanudación** del destino, al contrastar este documento contra el
> historial del repositorio, y lo declaró como la divergencia `D-01` de
> [`SDD/Docs/Audit/Estado-Del-Destino-2026-08-18.md`](SDD/Docs/Audit/Estado-Del-Destino-2026-08-18.md) §2.
> Es la **reincidencia** de la `D-01` del 2026-08-16, en su forma menor: una unidad de diferencia en
> lugar de tres etapas. Que sea menor es el resultado de contrastar seguido, no de que la regla se
> haya cumplido.
>
> **Nada de lo que sigue se infirió**: cada afirmación sale del mensaje de la confirmación `4cc596b`
> o de un archivo del árbol. No se reescribió ningún commit.

*No es una etapa: son los tres cambios acotados que la fase `i` necesitaba antes de publicar de
verdad, más un ADR.*

### El flujo de FTP corre las puertas bloqueantes antes de subir

`Api/09 Pipeline-CI-CD.md` §2.1 declara **QG-01** —construir en 0 y sin advertencias— y **QG-02** —la
batería entera— como bloqueantes, y el flujo de publicación **no corría ninguna de las dos**.
`dotnet publish` compila, así que un error de compilación frenaba la publicación; **una advertencia y
la batería entera en rojo pasaban igual**, y la comprobación final tampoco lo veía: la página carga y
responde 200 con el producto roto por dentro.

- Los dos pasos invocan [`scripts/build.sh`](scripts/build.sh) y [`scripts/test.sh`](scripts/test.sh),
  que son los que esa tabla nombra y los mismos que corren en la máquina de quien construye. Un
  `dotnet test` escrito a mano en el YAML habría sido un segundo lugar donde la configuración puede
  decir otra cosa.
- **Se retira el paso suelto que empaquetaba el visor**: `build.sh` ya lo invoca. Dejarlos a los dos
  habría corrido `npm ci` y webpack dos veces por publicación.

### `deploy/compose.yaml` dice qué es

Su primera línea decía «Despliegue en destino». **No lo es**: el despliegue en destino vive en
`Container.Lab-Geometria`, que conoce la red macvlan, la IP en la LAN, el directorio de la base y los
secretos. Este archivo es la composición de **verificación** de `PT-04`, y nada más.

**No era una duda teórica.** Leyendo este archivo se informó al Product Owner que la composición del
host estaba incompleta porque no declaraba la clave de firma. Era el archivo equivocado, y el nombre,
la carpeta y la primera línea decían las tres lo mismo.

La cabecera declara ahora el reparto —si para cambiarlo hay que conocer el fuente es del fuente; si
hay que conocer el host es del proyecto de contenedor—, por qué este archivo **no** declara la clave,
y que el intake §16 todavía lo describe como el despliegue en destino.

**No se renombra el archivo**, aunque el nombre sea lo que más confunde: `deploy/compose.yaml` está
declarado en el árbol de §16 del intake, que es documento humano con escritura controlada y bump
major, y de ahí baja a `Pipeline-Producto` §4, `Entornos-Deploy` §3 y `Plan-Etapa-A`. **Queda
elevado.**

### ADR-14003, emitido para aprobar

Declara como apartamiento que la dirección del backend viaje como **IP pública dinámica** y se
actualice a mano, con su disparador —IP estática o DDNS—, su estado `vigente` y su contador en **0**.

Registra que no hace falta republicar entero: `ApiBaseUrl` se lee una sola vez al arrancar, así que
alcanza con subir `appsettings.json` y reiniciar. Y anticipa la consecuencia sobre **`PT-05`**: la
medición registra la dirección usada **y su fecha**, porque una puerta en verde sobre una dirección
que puede cambiar no es una garantía permanente.

**Aprobado en la unidad siguiente.** El Product Owner lo aceptó el 2026-08-18 y el ADR pasó a **1.1,
`Aceptado`**, sin modificar su contenido (`docs/adr-14003-aceptado`, PR #62). Con la aprobación, el
apartamiento **cuenta como decisión y no como omisión** (`Root-Rules.md` §11).

### Verificación

**311 pruebas en verde. 4700 de 4700 enlaces resuelven, 0 rotos.**

---

## La puerta de la etapa `i` — escrita antes del despliegue, no después

**Rama:** `codigo/puerta-etapa-i`

*Esta entrada **se escribe en la rama**, que es lo que la cabecera pide y lo que la unidad anterior
no hizo.*

*No es la fase `i`: es su puerta. La fase `i` la cierra un despliegue real, y este guion es con qué
se lo mide cuando exista.*

### Por qué la puerta va antes que el despliegue

Las ocho puertas anteriores se escribieron **con** su etapa o **después**. Ésta va antes por un
motivo que las otras no tenían: **la fase `i` no ocurre en la máquina de quien construye**. Se
publica en un hosting, se levanta en un servidor propio y se mide con una persona en la red de la
facultad — y las tres cosas pasan **una sola vez cada una**. Llegar a ese momento sin saber qué hay
que mirar es cómo se cierra una etapa por lectura, que es exactamente lo que
`codigo/puertas-d-e-f` vino a corregir para las tres anteriores.

### Qué mide, y qué se niega a medir

`scripts/verify-stage-i.sh` cubre los siete criterios de la transición `i` → `j…` de
`Roadmap-Producto.md` **1.8** §5.2, en tres clases:

- **Mecánicos contra el despliegue vivo** — `I-1` la dirección pública responde y el flujo no tiene
  ningún paso que espere a una persona; `I-2` el punto de salud informa una revisión sellada **y es
  la de `main`**; `I-3` el navegador no llega al servicio de datos; `I-6` el almacén quedó
  preparado.
- **Delegado** — `I-7` comprueba que los ocho guiones de puerta existan y sean ejecutables, y
  **declara que correrlos es un acto aparte**. Repetir sus criterios acá habría creado un segundo
  lugar donde el criterio puede decir otra cosa.
- **No mecánicos** — `I-4` y `I-5` necesitan personas. El guion **los declara y no los marca**,
  igual que `verify-stage-h.sh` con su `H-7`.

**`I-2` es el criterio que nació de un defecto real**, y por eso no alcanza con que el servicio
responda: hasta el 2026-08-16 la revisión entraba por un argumento escrito a mano, y `/salud`
informaba una revisión que no era la suya **sin ningún síntoma**. El guion compara lo que el
servicio dice que corre contra lo que `main` tiene.

### Sin las dos direcciones no mide, y lo dice

`PUBLIC_URL` y `API_URL` llegan por entorno —ninguna dirección real vive en el árbol, igual que en
`deploy-front-ftp.yml`— y si falta alguna el guion **sale con un código propio** en lugar de
saltear en silencio. Es la misma corrección que las puertas de `d`, `e` y `f` incorporaron para
`dotnet`: dar por verificado lo que nadie miró es peor que no haber corrido.

### Dos defectos de la propia puerta, encontrados corriéndola

**No se escribió y se dio por buena: se corrió, y las dos primeras corridas la corrigieron.**

- **`curl --write-out` ya imprime `000` cuando no llega**, y además sale distinto de cero. El
  `|| echo 000` que llevaba encima imprimía **`000000`**, que no es ningún código HTTP y se lee
  como un defecto del guion. La resolución quedó en un solo lugar.
- **Un host que no contesta daba `OK`.** La comprobación de que `/appsettings.json` no se sirve al
  navegador leía cualquier respuesta que no fuera 200 como conforme — **incluida la de un servidor
  inalcanzable**. Es el defecto simétrico del que la etapa `f` corrigió en sus guiones: allá «sin
  medir» se reportaba como falla, acá se reportaba como **conforme**, que es peor, porque afirma
  sobre el producto algo que nadie miró.

### El formulario de `PT-05`, emitido vacío a propósito

[`SDD/Docs/Audit/Medicion-PT-05.md`](SDD/Docs/Audit/Medicion-PT-05.md) entra **antes** de la
medición, en estado **`SIN MEDIR`** y **sin un solo dato inventado**. Lleva los campos que
`ADR-14003` **1.1** exige —la dirección usada **y su fecha**, porque una puerta en verde sobre una
dirección que puede cambiar no es una garantía permanente— y declara que el resultado se registra
**sea cual sea**: si el acceso no funciona, el número se anota igual y la topología se revisa.

**Y el formulario vacío no pasa la puerta.** El guion comprueba que exista **y que su estado ya no
diga `SIN MEDIR`**: que el archivo esté prueba que la pregunta se hizo, no que esté contestada.

### Lo que esta unidad NO hace

**No despliega nada.** Publicar el front, levantar el servicio de datos en el servidor propio y
medir `PT-05` necesitan los secretos del hosting, el acceso al host y una persona en la red de la
facultad. Los tres son del Product Owner, y el guion existe para que ese momento sea verificable en
lugar de declarado.

---

## Las etiquetas que nunca se crearon — repuestas el 2026-08-18

**Rama:** `codigo/etiquetas-retroactivas`

*No es una etapa: cierra la divergencia `D-03`, la más vieja de las que quedaban abiertas.*

### Qué estaba mal

`Estrategia-Versionado.md` §1 declara, desde el intake §17.1.P.7, **una etiqueta por cada etapa
cerrada y fusionada, para poder volver a cualquier demostración**, y su §2.4 punto 4 la nombra el
instrumento de reversión: *«la reversión es volver a la etiqueta anterior y reconstruir»*.

**`git tag` devolvía cero en todo el repositorio.** Ocho etapas cerradas, ninguna etiqueta. Lo
declaró la reanudación del 2026-08-17 como `D-03` y quedó abierta; la del 2026-08-18 la volvió a
encontrar, y el Product Owner decidió reponerlas antes de la fase `i`, que es donde poder volver
atrás deja de ser teórico.

### El prefijo, que era el verdadero bloqueo

**No se podían crear porque nadie había fijado con qué prefijo.** `Rules-Devops.md` §4.3 punto 3
exige que la estrategia declare «configuración base y **prefijo de tag**», y las cuatro filas de su
§3 contestaban *«el que se fije al anclarla, registrado en el punto de control de la etapa `a`»* —un
punto de control que **cerró el 2026-08-13 sin registrarlo**.

Se fija **`v`**, y no por criterio propio: el propio `Rules-Devops.md` escribe la forma literal
**«Sólo en tag `v<X.Y.Z>` sin sufijo»** en la tabla de canales de su §4.5. Queda en
`Estrategia-Versionado.md` **2.2** §3.0, con la constancia de que **fijar el prefijo no cierra la
elección de la herramienta**: `PA-06` sigue abierto, y empaquetar las dos cosas en la misma fila es
lo que dejó ocho etapas sin poder etiquetarse.

**Y una referencia que no alcanzaba lo que decía alcanzar.** §3.2 remitía el prefijo al `PD-01` de
`Pipeline-CI-CD.md` §10, que es *«la herramienta concreta de cada stage —ejecutor de pruebas,
recolector de cobertura y reglas de análisis estático»*: la herramienta de versionado no figura ahí.
El prefijo estaba diferido **hacia un punto abierto que no lo cubría**, o sea sin dueño real. Queda
declarado y elevado al framework como el reporte `14` de `IA.SDD.Documentacion`.

### Las cinco etiquetas

| Etiqueta | Etapa | Ancla | Fecha del ancla |
|---|---|---|---|
| `v0.1.0` | `a` · Esqueleto ambulante y verificación de viabilidad | `760effd` (#29 `etapa-a-pt01a-despliegue`) | 2026-08-13 |
| `v0.2.0` | `b` · Navegación y sistema visual | `cf085fe` (#33 `etapa-b-limpiar-andamiaje`) | 2026-08-14 |
| `v0.5.0` | `e` · Alta de trabajo y vista de trabajos | `718ce74` (#45 `etapa-e-pantallas`) | 2026-08-15 |
| `v0.7.0` | `g` · Visualización 3D | `aaccd35` (#57 `etapa-g-puerta`) | 2026-08-17 |
| `v0.8.0` | `h` · Circuito de revisión del administrador | `8ec80a4` (#58 `etapa-h-revision`) | 2026-08-17 |

**Son anotadas y no livianas**, y cada una lleva en su mensaje que es retroactiva, con qué fecha se
creó y sobre qué ancla. Una etiqueta puesta hoy sobre una fusión de agosto **que no dijera que es de
hoy** sería el mismo defecto que este repositorio ya cometió con el registro de cambios.

**`0.x` y una MINOR por etapa**, porque hasta la fase `i` no hubo despliegue real ni release
público, y SemVer 2.0.0 reserva `0.y.z` para el desarrollo inicial.

### Los tres huecos de numeración son deliberados

**`v0.3.0`, `v0.4.0` y `v0.6.0` no existen, y no es un error de conteo.** Las etapas `c`, `d` y `f`
**no se pueden etiquetar sin inventar el punto**, y renumerar para tapar los huecos habría roto la
correspondencia entre la etapa y su MINOR, que es lo único que hace legible esta serie:

- **`c` y `d`** tienen su fusión nominal —#34 y #40— pero **le siguen ramas que son de la etapa**:
  `#35 sesion-por-cookie` y dos `arreglo/` en `c`, y `#41`, `#42` y `#43` en `d`. Etiquetar en la
  fusión nominal dejaría afuera trabajo de la etapa; etiquetar después exige decidir dónde termina,
  y ninguna fuente lo dice.
- **`f` no tiene fusión propia.** Aterrizó dentro de `ff62b78` junto con `g` y con una migración, y
  **el propio mensaje de esa fusión declara el apartamiento**: *«el intake §15 pide una rama y un
  pull request por etapa, y esta fusión lleva tres tramos en una»*. La etapa cierra en el commit
  `c2984ed`, que lo dice, pero es **un commit y no una fusión**.

**Se eligió declararlo en lugar de resolverlo por criterio propio.** Las tres quedan sin etiqueta y
con su motivo escrito, que es una afirmación verdadera y verificable; ponerles una etiqueta elegida
por mí habría sido una afirmación cómoda sobre un punto que nadie decidió.

## El recorrido que se puede correr sin permiso de nadie — 2026-09-02

**Rama:** `e2e-banco-local-navegacion-y-responsivo`

### El problema, dicho sin vueltas

**La batería de extremo a extremo existía desde el 2026-09-02 a la mañana y no se podía correr.**
Exigía cuatro variables de entorno —dos de ellas secretas—, abría el navegador contra el sitio
publicado y **sembraba cuentas en el laboratorio del docente**. Eso tiene tres consecuencias, y las
tres se pagaron el mismo día:

1. **Nadie podía correrla antes de empujar un cambio**, porque hacía falta la credencial del
   administrador del laboratorio real.
2. **El rojo del anfitrión se confundía con el rojo del producto**: trece casos rojos con nombres de
   producto en la primera corrida, y ninguno era del producto.
3. **La forma de correrla no estaba versionada.** Se usó una imagen `gf-e2e:local` armada a mano que
   nunca entró al repositorio. Una prueba que sólo una persona puede correr no es una compuerta.

### Agregado

- **El banco local** (`tests/GeometriaFactory.E2ETests/Infraestructura/BancoLocal.cs`). Sin
  `URL_BASE`, la suite **publica y levanta el producto entero**: servicio de datos, pieza pública y
  un almacén de esta corrida, sobre **puertos que le pide al sistema** —los contenedores del Product
  Owner no se rozan— y con un administrador que ella misma siembra. Al terminar baja todo y borra lo
  que creó. **No hace falta ningún secreto y no se toca ningún dato ajeno.**
- **`deploy/e2e/Dockerfile` y `scripts/pruebas-e2e.sh`.** La imagen de la corrida deja de vivir en la
  memoria de una sesión. El guion construye la imagen la primera vez, genera el bundle del visor y
  corre la batería, en cualquiera de los dos modos y en cualquiera de los tres navegadores.
- **`NavegacionTests` — diez casos.** El armazón, que es la única parte del producto que se dibuja en
  todas las pantallas y que **ninguna otra clase miraba**: las cinco rutas del panel desvían al
  ingreso sin sesión, la barra del administrador trae sus tres destinos **y ninguno del alumno**, el
  destino en curso se marca, la dirección inventada responde **404 y no 200**, y cerrar sesión
  cierra de verdad —la marca del navegador incluida—.
- **`DisenoResponsivoTests` — seis casos.** La versión angosta, medida en una ventana de 390 px.
  Es la clase que cuida el `P0` `MI-02` —los tres listados sin dibujar **ninguna fila** por debajo de
  768 px, catorce días con toda la construcción en verde— y la regla `R-06` —la acción destructiva
  por encima de la primaria en el teléfono—. Cada caso verifica una regla **escrita en la hoja de
  estilos**, y los dos casos de pantalla ancha son el contrapeso: sin ellos, apilar los botones
  siempre haría pasar el caso angosto y rompería el ancho sin que nadie se entere.
- **Traza y captura de lo que falla**, en `resultados-e2e/`. Las capturas de la versión angosta
  quedan **aunque el caso pase**: en esta casa la evidencia de lo visual se mira, y un conteo en
  verde ya dio por bueno un teléfono con tres recortes.
- **El trabajo `banco-local` en `.github/workflows/e2e.yml`**, sobre el runner propio
  `[self-hosted, i7infra-dev]`, **en cada empujón y en cada pull request**. Las dos razones por las
  que el recorrido no corría en cada cambio —toca datos reales, depende de un sitio ajeno— son
  razones del modo desplegado y de ningún otro.

### Corregido

- **`AprobarPideConfirmacionYAplicaElDesenlace` estaba en rojo en `main` y nadie lo sabía.** El
  aserto buscaba «quedo finalizado» y el producto emite «quedó finalizado»: **nunca pudo
  coincidir**. Las dos cosas entraron en la misma fusión del 2026-09-02 —el acuse y la prueba que lo
  verifica— sin que la suite se volviera a correr. Lo encontró el banco local la primera vez que
  corrió.
- **`LaInteractividadEstaVivaEnElAnfitrion` era intermitente por construcción.** Miraba la marca de
  lectura, que se imprime con resolución de **un segundo**: contra el anfitrión remoto la ida y
  vuelta tarda más y el texto siempre cambia; contra un banco local la consulta entera entra en el
  mismo segundo y el texto queda idéntico. Ahora mira **el momento del servidor**, que lo produce el
  servicio de datos y se imprime hasta la diezmillonésima.

### Documentado

- **`SDD/.../08-Calidad-Y-Pruebas/Pruebas-Extremo-A-Extremo.md` 1.0**, artefacto nuevo de la
  categoría: los dos modos, las ocho clases, los treinta y dos casos, la evidencia y **cuatro
  apartamientos declarados** —entre ellos que el banco local corre sobre HTTP y por lo tanto **no
  puede ver un defecto que dependa de la marca `Secure` de la cookie**—.
- `08-Calidad-Y-Pruebas/README.md` 2.1 y `Estrategia-Testing.md` 2.3 lo incorporan **sin reescribir
  lo emitido**: la fila que dice que el guion de demostración es «una persona ejecutando pasos»
  sigue siendo verdadera y no se toca.
- `09-Devops/Pipeline-CI-CD.md` 3.7 declara el flujo, sus dos trabajos y por qué uno corre siempre y
  el otro no. **Queda dicho que la batería no está registrada como puerta bloqueante de rama**: eso
  lo decide el Product Owner.

### Lo que queda abierto

- **`H-E2E-01` · `firefox` no termina la batería sobre el banco local.** `chromium` recorre los 32
  casos en 18 s; `firefox` pasa los casos que no necesitan el circuito interactivo —`IngresoTests`,
  3 de 3 en 7 s— y **agota sus 30 s en cada caso que espera a que el circuito habilite un control**,
  sin terminar la corrida completa en 25 minutos. Medido tres veces, con la máquina cargada y
  descargada, con traza y sin traza. **No se sabe de quién es** —del banco, del navegador o del
  producto en ese navegador— y **no se resolvió subiendo el margen de espera**, que sería tapar la
  pregunta con un número. Queda declarado en la especificación de la batería.
- **Que este recorrido sea puerta bloqueante de rama** es decisión del Product Owner. Hoy corre y
  reporta; no bloquea la fusión.

---

## Lo que la segunda mesa de UX/UI midió y reparó — 2026-09-03

**Qué la fundó.** Una revisión agéntica del front desplegado, con el procedimiento de
`Lab-Geometria.Documentacion/PROMPTs/Fixs/02-Ajuste-UX-2.0/`, sobre los dos papeles y nueve
pantallas. El informe, con las mediciones, las capturas y la clasificación A/B/C de cada hallazgo,
está en `OUTPUTs/Revision-UX-GeometriaFactory.Web-2026-09-02.md` de esa misma carpeta.

**El sistema visual salió bien parado y no se rehízo nada:** la escala tipográfica, el espaciado, el
anillo de foco, la marca del ítem de menú activo y las fichas de la versión angosta pasaron la
revisión sin observaciones. Lo que sigue son doce reparaciones puntuales.

### Reparado en el sistema visual

- **`--color-text-tertiary` no llegaba al piso de contraste.** Medía **3,48:1** sobre blanco y
  **3,22:1** sobre el fondo terciario, contra el 4,5:1 de WCAG 1.4.3, y lo usaba texto de cuerpo:
  los recuentos del árbol del texto y las notas de la escena. Pasa a `#71716B` —4,91:1 y 4,55:1—
  **en la hoja del producto y en la de la maqueta**, porque el control `C-4` de
  `verify-visual-system.sh` exige que los dos catálogos digan lo mismo.
- **La retícula fija repartía partes iguales.** `R-05` alineó las tablas hermanas pero dejó sin
  declarar el reparto: las cuatro columnas de datos de «Cuentas» medían lo mismo —67 px a 769— y el
  correo salía en ocho renglones. Peor: con el encabezado sin partir y `overflow: visible`,
  «SITUACIÓN» pintaba 28 px sobre «FECHA DE REGISTRO». Se declaran **todas** las columnas por forma
  de tabla, y un `min-width` que hace desbordar la tabla dentro de su envoltorio en vez de astillar
  el texto.
- **La proporción del envío estaba al revés de la tarea.** El campo donde se pega el trabajo medía
  247 × 132 px con desplazamiento horizontal y la previsualización vacía 578 × 450. Se invierte la
  proporción **sólo en el envío** y el campo pasa a 537 × 320. Es la alternativa A de la propuesta
  C del informe, aprobada. **Queda abierto** que la acción primaria sigue fuera de la primera
  pantalla, ahora en la ordenada 1061.
- **«Dar de baja» era el elemento de más peso visual de «Cuentas»**, sobre una acción que se usa una
  vez por cuatrimestre. Pierde recuadro y relleno; conserva color de peligro, ícono y confirmación
  escrita. No se desapilan: esa decisión es de `R-05` y se respeta.

### Reparado en las superficies

- **Los tres formularios de filtro no mostraban espera.** Con la red lenta la pantalla se iba a
  blanco —`main` en `null`— mientras `Experiencia-De-Uso.md` §2.1 promete indicador en toda acción
  que cruza al servidor, y el mecanismo ya existía. Llevan `data-gf-pending`.
- **El ejemplo de la pantalla de envío producía una advertencia.** Declaraba el área de las seis
  caras del cubo y emitía **una**: pegado tal cual, el laboratorio contestaba «declara 54.00, la
  geometría da 9.00». Se escriben las seis caras. La regla de derivación no se tocó.
- **Jerga de implementación en cuatro lugares:** «No salió ninguna solicitud hacia el servicio de
  datos» (cuatro superficies), «Servicio de datos en línea» encabezando todas las pantallas del
  panel, «el texto **JSON**» y el `Guid` del trabajo mostrado al alumno. `Experiencia-De-Uso.md`
  §2.1 y §6 los prohíben con todas las letras.
- **El encabezado seguía pidiendo lo que ya se había hecho:** enviado el trabajo, el título decía
  «Trabajo nuevo» sobre un panel que decía que el trabajo ya estaba enviado.
- **Tres tipeos de credenciales para entrar la primera vez.** El correo viaja en la redirección del
  cambio obligado y el ingreso llega con el campo puesto.
- **Los errores de formulario no marcaban el campo:** cero elementos con `aria-invalid` en las
  cuatro formas probadas (WCAG 3.3.1). Ahora se marca el que falló, y sólo ése.
- **Los filtros se dibujaban sobre listas vacías.** No se dibujan con la colección vacía; el vacío
  de filtro conserva la barra, porque sin ella no habría cómo deshacerlo.
- **Concordancia** en «Con 1 advertencia, que no impiden la entrega», y se nombra de dónde sale el
  índice que empieza en cero: «posición 0 **en el texto**».

### Verificación

Construcción sin advertencias (`QG-01`). `verify-visual-system.sh` **conforme en los cinco
controles**. **522 pruebas pasan, 0 fallan.** Tres pruebas de integración afirmaban la copia y la
redirección anteriores y se actualizaron con el cambio.

Las mediciones de antes y después se tomaron **contra el producto levantado desde el fuente** —base
propia y vacía, datos creados en la corrida—, no contra el despliegue público, que sigue sirviendo
la versión anterior.

### Lo que queda abierto

- **La acción primaria del envío sigue fuera de la primera pantalla** (ordenada 1061 a 1440 × 900),
  porque el campo que la precede creció. Cerrarlo pide una decisión —mover «Enviar el trabajo» a la
  columna de la previsualización, o achicar el campo otra vez—, no un ajuste fino de espaciados.
- **En «Entrega de la comisión», un alumno filtrado sin entregas muestra el vacío de colección** y
  no el de filtro. Encontrado mientras se reparaba lo anterior; fuera del alcance aprobado y sin
  tocar.

## Dockerizar el front — 2026-09-06 (repuesta el 2026-09-12, después de la fusión)

**Rama:** `dockerizar-el-front`. PR #186, fusionado `89f3ab3`, ancla `6fa6844`.

> **Se repone tarde, y se dice.** La regla de este documento es actualizarse en la rama de la etapa,
> no después de la fusión (l.6). Esta entrada se escribe en la séptima reanudación del destino, sobre
> la base `5c95dab`, y **el hecho es ajeno a esta corrida**: el trabajo y su fusión son del
> 2026-09-06, seis días antes de que empezara. Es la **cuarta** vez que este documento llega tarde a
> una fusión propia — después de las etapas `c`, `d`, `e` (repuestas el 2026-08-16) y del PR #186 de
> `dockerizar-el-front` en su momento, hoy corregido.

### Cambiado

- El front deja de publicarse por transferencia al hosting externo y pasa a construirse como imagen
  Docker (`deploy/Dockerfile.web`), publicada en un contenedor del servidor propio de i7infra. La
  etapa de Node **no es opcional**: el bundle del visor lo produce `webpack` en `visor/`, y un clon
  limpio sin esa etapa construye un front sin visor, sin que nada falle.
- `Program.cs` lee `ApiBaseUrl` de `IConfiguration`: la misma imagen sirve para cualquier destino
  variando una variable de entorno, sin tocar el artefacto publicado (el apartamiento que `ADR-14003`
  documentaba para el hosting deja de ser necesario para este canal).
- Verificado contra el equipo: la imagen construye y queda sellada con la revisión del árbol; el
  bundle viaja adentro (501 676 bytes, servidos en `/js/`); con `ApiBaseUrl` apuntando al servicio de
  datos por nombre de red, `/estado` informa la versión y el reloj **del servicio de datos**.
- **Limitación registrada en el propio archivo**: la imagen hereda de `appsettings.json` un
  `ApiBaseUrl` de desarrollo, de modo que un contenedor sin configurar arranca sano y falla en
  silencio; la guarda vive en la composición, no en el `Dockerfile`.
- **No se retira el canal de FTP.** Queda vivo hasta la decisión del Product Owner del 2026-09-06 de
  conservarlo como alternativa y antecedente de despliegue (asentada en `PRODUCT-INTAKE` **4.2**,
  `Roadmap-Producto.md` **1.10**), y sin disparador automático desde esta misma reanudación
  (`.github/workflows/deploy-front-ftp.yml`).

## El bundle lo genera el `.csproj`, y todo entra al árbol de solución — 2026-09-12

**Rama:** `estructura-solucion-visor-y-samples`. Cierre de la Feature 20 del framework
(`IA.SDD.Documentacion/PROMPTs/Features/20-Agregar-BundleJS-Estructura-Solucion/`), tras dos ciclos de
mesa evaluadora. Evidencia en `evidencia/2026-09-12-estructura-solucion/`.

### Cambiado

- `GeometriaFactory.Web.csproj` **genera el bundle del visor** con el target `BuildVisor` —sólo cuando
  cambió una fuente del visor, apagable con `-p:SkipVisorBuild=true`—, lo **declara como recurso
  estático dentro del target** (sin eso no entra al manifiesto en la primera construcción, medido) y
  **lo sella por contenido** (`SealVisorAsset`: `?v=<sha256>` en `WorkSubmission` y `WorkView`).
  Un clon limpio ya no construye un front sin visor sin que nada falle. `Web ADR-10008`.
- **Un solo generador.** `build.sh`, `e2e.yml`, `pruebas-e2e.sh` y `deploy-front-ftp.yml` dejan de
  invocar `build-visor.sh`; `BancoLocal.cs` cambia su aviso. `Dockerfile.web` publica con la bandera
  y conserva su etapa `node:22`.
- **`GeometriaFactory.sln` pasa de 9 a 20 proyectos**, por decisión del Product Owner del 2026-09-11
  («todos bajo el árbol de solución, aunque sea bajo carpetas virtuales»): `visor/geometriafactory-visor.csproj`
  como nodo sin construcción (`Microsoft.Build.NoTargets`, lista fuentes por patrón, no ejecuta nada),
  los nueve `Sample.*.csproj` **construidos con la solución** (`Rules-Examples.md` §3.4; `QG-03` medido
  antes y después: igual) y `tests/GeometriaFactory.E2ETests` visible sin `.Build.0` (se corre por su
  ruta, como siempre). Los doce textos que decían «NO ENTRA EN `GeometriaFactory.sln`» se reescriben.
- `PRODUCT-INTAKE` **4.1** (§13.2, §13.3, §16) y `PRODUCT-MANIFEST` **6.0** re-derivado: el visor está
  en el agrupador; la ruta `visor/`, la identidad npm y la clase «activo de construcción» no cambian.
  `Vista-Producto` 1.10, `Plan-Etapa-A` 1.12, `Web/09 Pipeline-CI-CD` 3.8, `ADR-12006` 1.1,
  `Web/05` README y `Decisiones-Arquitectura` 2.1.
- `samples/visor/02-intermedio` suma el acto `[15]`: **selección desde la escena por `onPieceSelected`**
  (`ADR-08007`), la única vía del visor hacia el anfitrión, que ningún sample ejercía. Documento
  `ejemplo-02-intermedio.md` 2.1 con `evidencia` completada.
- `samples/README.md` 2.0: estado real por carpeta con fecha de la última corrida (dieciséis corren,
  tres en esqueleto) y diecinueve enlaces que apuntaban a `SDD/Docs/Proyectos/…`, ruta inexistente,
  pasan a `Unidades-Entrega/`.

### Verificado

`dotnet publish` limpio en una invocación con bundle y manifiesto; segunda construcción sin `npm ci`;
sin Node y sin bandera falla, con bandera 0/0; `build.sh` 0/0; `test.sh` 522/522; `coverage.sh`
`QG-03` idéntico; los doce samples sin servicio y los tres del visor conformes; el bundle sellado
servido antes del guardián de aprovisionamiento. Detalle en `evidencia/2026-09-12-estructura-solucion/verificacion.md`.

## Decisiones del Product Owner sobre las tres escaladas del ciclo 1 — 2026-09-12

**Sin código.** Entrada de intake y de roadmap, no de una rama de etapa: el Product Owner contestó,
fuera de toda corrida del orquestador, las tres escaladas `E-02`, `E-04` y `E-05` de
`SDD/Docs/Audit/Mesa-2026-09-12.md` §9, y una mesa de ciclo 2
(`SDD/Docs/Audit/Mesa-2026-09-12-ciclo-2.md`) convirtió `E-02` y `E-04` en plan.

- **`E-02`** — «la API debe ser expuesta públicamente; la idea es ofrecerla para otros clientes».
  Cambio de alcance: la API deja de estar reservada a `GeometriaFactory.Web`. Quién es el cliente
  externo concreto queda **diferido** (`D-01`): el Product Owner no lo tiene decidido todavía.
- **`E-04`** — «buscar una forma de nomenclar el versionado, según el estándar de la industria».
  SemVer 2.0.0 se mantiene para el producto; el contrato REST público se versiona en la ruta, sólo el
  `MAJOR` (`/v{MAJOR}/`), compartido con el del producto; evento de etiqueta = fusión a `main` con
  cambio de código de producto, por Conventional Commits y MinVer; plazo de convivencia entre
  versiones de un cuatrimestre. **No se reescriben `ADR-00008` ni `Estrategia-Versionado.md`** en esta
  entrada: son dos de los diez ítems del plan que abre `Roadmap-Producto.md` **1.11**, fila `k`.
- **`E-05`** — el estado durable. Front: riesgo aceptado por escrito (perder la clave de protección de
  datos cuesta un re-login a los alumnos, no hay datos en juego). Backend: se respalda el SQLite de
  `lab-geometria-api/data`; el requisito queda escrito en el intake, el mecanismo lo implementa el
  Product Owner en su despliegue.

**Asentado en:** `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **4.3** (§17.1.P.3, §17.1.P.5, `X-9`,
`RA-01`, §11 `RN-B7`/`RN-B8`), `Roadmap-Producto.md` **1.11** (fila `k`, diez ítems, estado
pendiente), `Mesa-2026-09-12.md` **1.1** (§9 respondida, §8 con los dos ítems diferidos que quedan
abiertos: `D-01` y el mecanismo de respaldo del backend).

## La API autentica personas, no aplicaciones — 2026-09-12

**Sin código.** Entrada de arquitectura, intake y backlog, no de una rama de etapa: el Product Owner
cerró el ítem diferido `D-01` (quién es el cliente externo) con una decisión, no con la aparición de un
cliente: *«El cliente o usuario del sistema: hay dos, el administrador que recepciona los trabajos y los
visa —aprobar o rechazar— y gestiona a los usuarios; y los usuarios, que en un caso particular son
alumnos. No hay diferencia entre alumno y usuario general; no importa que sea alumno»*, y *«no hay
tercero — es re simple: un administrador, y luego usuarios generales, que pueden ser cualquiera, entre
estos alumnos»*.

- **`ADR-00009`** (`SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/05-Arquitectura-Tecnica/Adrs/`,
  **Aceptado**): dos identidades y nada más —administrador y usuario, los dos valores de `Role`—;
  toda aplicación del producto obtiene el acceso por `POST /auth/token` con las credenciales de la
  persona, como el front hoy; **no hay claves de API ni `client_credentials`**. Cliente es cualquier
  aplicación propia que actúe en nombre de una persona, y su forma —otro front, MAUI, script— no cambia
  la autenticación. «Alumno» es la etiqueta del papel `Student`; el papel es «usuario», y no se
  renombra ningún uso. Descarta tres alternativas con fundamento (API key por cliente,
  `client_credentials`, un tercer papel «aplicación») y deja en §10 los comandos con que se verificó
  cada cita.
- **`BT-00028`** (autenticación por cliente para terceros) pasa a **`Descartada`**: no hay nada que
  construir. `BT-00029` (rate limiting) pasa a ser **por persona autenticada o por IP** y queda sin
  dependencias; `BT-00030` (CORS) queda condicionada sólo a que aparezca **un cliente propio que sea
  JavaScript de navegador desde otro origen**, sin dependencias; `BT-00032` retira la dependencia de
  `BT-00028`; `BT-00034` se autentica como una persona y no con una clave; `BT-00027` deja de nombrar
  `ADR-00009` como identificador libre. **Ya no hay ninguna BT del tramo `k` bloqueada**: 8 `Ready`,
  0 `Borrador`, 1 `Descartada`, y `tsort` sobre las dependencias leídas de las fichas termina en `0`.

**Asentado en:** `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **4.4** (§17.1.P.3 filas «Quién la
consume», «CORS» y «Endpoint de autenticación»; §17.1.P.5; `X-9`; `RA-01`; sin ítem diferido nuevo),
`Mesa-2026-09-12-ciclo-2.md` **1.1** (§8, `D-01` respondida), `Mesa-2026-09-12.md` **1.2** (§8, fila de
deuda cerrada), `DoR-Tramo-k-2026-09-12.md` **1.2** (§4 vacío, §6.9 con las verificaciones),
`Backlog-Tecnico.md` **3.2**, `Mini-Plan.md` **3.1**, `README.md` de 07 **2.2**,
`Decisiones-Arquitectura.md` **2.1** y `README.md` de 05 **2.1** (la ADR entra a los índices).
**No se tocan** `ADR-00008`, `Estrategia-Versionado.md` ni los dieciséis lugares con «no hay clientes
de terceros»: son `BT-00027`.

## La superficie pública se versiona en la ruta: `ADR-00010` supera parcialmente a `ADR-00008` — 2026-09-12

**Sin código.** Tarea `BT-00027` (tipo `docs`, fase `k`, rama `fase-k/bt-00027-adr-versionado-rutas`
sobre `cfb11f7`). Ninguna ruta cambia: el prefijo `/v1/` lo implementa `BT-00032`.

- **`ADR-00010`** (`SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/05-Arquitectura-Tecnica/Adrs/`,
  **Aceptado** por decisión del Product Owner del 2026-09-12, `E-04`): la superficie pública lleva la
  versión en la ruta, **sólo el `MAJOR`**, `/v{MAJOR}/`, y ese `MAJOR` es el del producto. Declara el
  cambio de premisa —«no hay clientes de terceros» dejó de ser cierto: hay **aplicaciones propias
  además del front** (`ADR-00009`) que **no compilan contra el ensamblado de contratos**—, descarta con
  fuente la cabecera, el media type y la fecha estilo Stripe (mesa ciclo 2 §3.2), y **remite** el evento
  de etiqueta a `BT-00033` y la deprecación a `BT-00035`.
- **`ADR-00008`** pasa a **`Superado parcialmente`**: la regla 1 («una sola versión de la superficie
  vive a la vez», sin prefijo ni deprecación) y sus dos primeras métricas quedan **derogadas**; las
  reglas 2 a 5 —despliegue conjunto entre `Api` y `Web`, las tres clases de cambio que la compilación
  no detecta, la etiqueta por fusión, la colección de peticiones— y la ausencia de la pasarela
  **subsisten**. Texto conservado íntegro.
- **El conjunto medido** por `git grep -n -i "no hay clientes de terceros" -- SDD ':!*/_legacy/*'
  ':!SDD/Docs/Audit/*'`: dieciséis documentos. En cada uno la premisa se reescribe con la fecha desde la
  que rige, se declara superada o remite a `ADR-00010`, con el mínimo cambio y control de cambios
  propio. `ADR-08003` (nivel Producto) recibe **aviso de lectura** y no se reescribe: su reescritura es
  consecuencia obligada de la categoría de Producto. `Estrategia-Versionado.md` sólo tacha la regla 1 como
  derogada: el evento de etiqueta es `BT-00033` y la deprecación `BT-00035`. `Contratos-REST.md` §6 deja
  de decir «no se versionan las rutas». El intake sube a **4.5** por la última transcripción viva
  (§17.2.P.3 · Contracts), por delegación expresa de su entrada 4.4.
- Índices: `Decisiones-Arquitectura.md` **2.2**, `README.md` de 05 **2.2**,
  `Arquitectura-Unidad-Entrega.md` **3.10** (§10.1: «las diez»).

**Evidencia:** `SDD/Docs/Audit/BT-00027-Cierre-2026-09-12.md` (las cinco verificaciones con comando y
salida) y la ficha `BT-00027` **2.0**, `Done`.
## La versión la calcula MinVer, y el evento de etiqueta deja de esperar al cierre de etapa — 2026-09-12

**Rama:** `fase-k/bt-00033-minver` (`BT-00033`, fase `k`). Decisión `D-03` y respuesta `E-04` del Product
Owner en `SDD/Docs/Audit/Mesa-2026-09-12-ciclo-2.md` §8.

### Cambiado

- **`Directory.Build.props` adopta MinVer 8.0.0**, anclado exacto y con `MinVerTagPrefix=v`, sin más
  configuración. La versión deja de ser el `1.0.0` que el SDK sellaba por omisión y que `/salud`
  informaba como si fuera del producto: sobre `main` hoy es `0.8.1-alpha.0.134+<sha>` —`v0.8.0` más
  134 fusiones, marcada como no entregada— y sobre un commit etiquetado es la etiqueta. **`/salud` no
  cambió**: ya leía `AssemblyInformationalVersion`.
- **Los dos `Dockerfile` completan el clon antes de publicar.** Medido, no supuesto: el despliegue
  construye desde la URL del repositorio y BuildKit deja el checkout con `--depth=1 --no-tags` aunque
  conserve el `.git`; sobre eso MinVer calcula `0.0.0-alpha.0` **sin advertir**. Si `.git/shallow`
  existe, `git fetch --unshallow --tags origin`; si no puede, avisa y no rompe la construcción.
  `ci.yml` pide `fetch-depth: 0` por el mismo motivo.
- **`Estrategia-Versionado.md` 5.0**: §3.1 a §3.4 dejan de declarar la herramienta «por su función»
  —es MinVer— y entra **§3.c, el evento de etiqueta**: toda fusión a `main` que cambie código de
  producción, número por Conventional Commits, MAJOR compartido con el contrato REST. `05` 3.11 cierra
  `PA-04` (Domain) y `PA-06` (Application), abiertos desde la etapa `a`.

### Lo que queda para el Product Owner, con `SI NO RESPONDÉS`

- **`PD-VER-01` · quién crea la etiqueta.** Un job en `ci.yml` que la cree en cada `push` a `main` con
  código de producción (subproducto del acto, pero una acción externa e irreversible desde CI), o a mano
  con el número que MinVer sugiere. **Default: a mano**, hasta que apruebe el job.
- **`PD-VER-02` · qué es «código de producción».** `src/**` y `visor/**` son indiscutidos; si entran
  `Directory.Build.props`, los `*.csproj`, la solución y `deploy/Dockerfile*`. **Default: entran.**
- **`PD-VER-03` · `89f3ab3` y `5c95dab`.** No se etiquetaron acá: etiquetar es irreversible y no se
  puede calcular —**ninguna confirmación de `main` lleva `feat` ni `fix`**, y por MinVer el siguiente
  número sería `0.8.1`, que afirmaría que en 129 fusiones sólo hubo correcciones—. **Propuesta**:
  `v0.9.0` sobre `89f3ab3` y `v0.9.1` sobre `5c95dab`, anotadas y declaradas retroactivas. **Default:
  ninguna**; `BT-00033` queda `En curso` por ese criterio.
- **Producción sigue diciendo `1.0.0+5c95dab`** hasta que se reconstruya desde `main` con esta fusión.

### Verificado

`dotnet build` Release 0 advertencias (igual que `main`); `dotnet test` 522/522; MinVer sobre `HEAD`
`0.8.1-alpha.0.135`; imagen del servicio construida sobre `cfb11f7` desde un clon superficial sin
etiquetas responde `/salud` con `0.8.1-alpha.0.133+cfb11f75…`; `git tag -l` sin cambios. Detalle en la ficha, §8 fila 1.2.

## BREAKING · El contrato REST se publica bajo `/v1/` — 2026-09-12

**Rama:** `fase-k/bt-00032-rutas-v1` (`BT-00032`, fase `k`, sobre `ce1c68f`). Implementa `ADR-00010`.
**Toda aplicación propia que consuma el contrato REST debe anteponer `/v1/` a sus rutas**: la ruta sin
prefijo —`/cuentas`, `/trabajos`, la que valió hasta hoy— responde `404`, sin redirección. Es la primera
fusión con `/v1/` y, por decisión del Product Owner de hoy, la que recibe la etiqueta `v1.0.0` (la crea
el evento de etiqueta de `BT-00033`; esta rama no etiqueta).

### Cambiado

- **`GeometriaFactory.Api`: los dieciséis puntos del contrato cuelgan de un único `MapGroup("/v1")`** en
  `Program.cs`, y la cifra vive en `Endpoints/ContractRoutePrefix.cs` (`Major = 1`, `Value = "/v1"`).
  Los contratos de punto siguen declarando su ruta relativa: agregar un punto no exige acordarse del
  prefijo. Ningún verbo, cuerpo, papel ni código de respuesta cambia.
- **Tres rutas quedan fuera del prefijo, enumeradas como exentas** (`ContractRoutePrefix.ExemptRoutes`):
  `/salud` (`A-16`) es del arranque y la salud y no del contrato —lo piden los `healthcheck` de las
  composiciones y la página de estado del front, `ADR-00007` §2 punto 3; y es donde se informa la versión
  contra la que `ADR-00010` §8 compara el prefijo—; `/openapi/v1.json` y `/documentacion` describen la
  superficie y no son parte de ella (`ADR-08008`). `ADR-00010` §7 había remitido esta decisión a
  `BT-00032`; queda escrita en `Contratos-REST.md` **1.8** §3.1.
- **`GeometriaFactory.Web`: `DataServiceClient` antepone `v1/` a todas las rutas del contrato** con la
  constante `ContractPrefix`, y a `salud` no. `ApiBaseUrl` **sigue siendo el anfitrión**: la versión la
  conoce el cliente —que compila contra el ensamblado y se despliega junto con el servicio— y no la
  configuración de quien despliega. **El despliegue no necesita cambiar `API_BASE_URL` ni el túnel**, que
  enruta por nombre de host y no por prefijo.
- **Consumidores propios actualizados en la misma intervención**: la batería de integración (todas las
  rutas literales del contrato llevan `/v1/`; las de las páginas del front no, porque no son del
  contrato), los E2E (`ElLaboratorio` y `BancoLocal` se paran sobre `<api>/v1/`), los samples
  `api/01-basico`, `api/02-intermedio` y `web/01-datos-seed` (rutas explícitas con `/v1/` en `run.sh`,
  `peticiones/` y `coleccion/`; **ningún snapshot esperado cambia**, porque ninguno imprime rutas), y
  los guiones `scripts/verify-stage-c.sh`, `tools/medicion-*.sh` y `tools/verificar-resolucion-del-trabajo.sh`.
- **`Contratos-REST.md` 1.8** (§3.1: la tabla de rutas públicas, las tres exentas, el `404` sin prefijo,
  la prueba en las dos direcciones) y **`Definicion-Superficie-HTTP.md` 1.11** (las dieciséis rutas con
  el prefijo; `A-16` sin él).

### Agregado

- **`ContractRoutePrefixTests`** (integración): transcribe a mano la tabla de `Contratos-REST.md` §3
  —diecisiete puntos, verbo y ruta pública— y la contrasta con lo que el host enruta de verdad
  (`EndpointDataSource`), **en las dos direcciones**: toda ruta publicada está en la tabla o entre las
  tres exentas; todo punto de la tabla está publicado; toda ruta publicada lleva `/v1/` salvo las tres
  exentas, que son exactamente tres. Más el `404` sin redirección de cinco rutas sin prefijo y la
  exención de `/salud` en una sola dirección (`/v1/salud` → `404`). **Probada fallando**: una ruta sin
  prefijo y una ruta ajena bajo `/v1/` agregadas a mano rompen tres de las diez pruebas, con la salida
  diciendo cuál agregar dónde.

### Verificado

`dotnet build` Release 0 advertencias; `dotnet test` **532/532** (Domain 94, Application 56, Integración
382; `main` tenía 522, los diez nuevos son `ContractRoutePrefixTests`); samples `api/01` **CONFORME
13/13**, `api/02` **CONFORME** con su única divergencia ya declarada (`D-1`), `api/03` **CONFORME** con
su única divergencia ya declarada (`D-3`) y **17 operaciones** en el documento OpenAPI, `web/01-datos-seed`
**CONFORME 13/13**, todos contra el servicio de esta rama en un puerto propio; `git tag -l` sin cambios.
Los E2E de Playwright no se corrieron localmente (corren en CI). Detalle en la ficha `BT-00032`, §8.
## CORS no aplica hoy, y queda declarado por qué — 2026-09-12

**Rama:** `fase-k/bt-00030-cors` (`BT-00030`, fase `k`, sobre `8e5e2f9`). Tarea de indagación:
**sin cambio de código**.

### Cambiado

- `SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`
  **3.12**: la fila de §9.1 del riesgo «que se agregue un punto de acceso pensado para el navegador, o
  se configure el intercambio de origen cruzado» declara en su mitigación **por qué CORS no aplica hoy**:
  ningún cliente propio es JavaScript de navegador desde otro origen (`ADR-00009`; el front es Blazor
  Server y llama servidor a servidor; una aplicación MAUI no es un navegador). Entra **§9.1.1** con la
  medición y la condición que lo reabre: si aparece ese cliente, política **por origen explícito**,
  nunca `AllowAnyOrigin` con credenciales.
- `02-Especificacion-Funcional/Definicion-Superficie-HTTP.md` **1.12**: la fila «CORS» de §7 lleva la
  condición del intake 4.4 y remite a `05` §9.1.
- `BT-00030` → **`Done`** (2.0), por la primera de las dos vías de su caja temporal: la declaración.

### Verificado

- `git grep -niE "cors" -- src/` → **0** ocurrencias.
- `curl -si -X OPTIONS https://api-geometria.aplicada.stream/salud -H "Origin: https://evil.example.com"`
  → `405`, `allow: GET`, **sin `Access-Control-Allow-Origin`**; el contenedor `lab-geometria-api` en
  `:8080` responde igual (`Server: Kestrel`).

## Producción reconstruida desde `main`: la versión es la de MinVer y OpenAPI/Scalar están publicados — 2026-09-13

**Rama:** `fase-k/cierre-bt-00031-00033` (`BT-00031` y `BT-00033`, fase `k`). Sin cambio de código: lo
que cierra las dos tareas es un despliegue del Product Owner y tres decisiones suyas.

### Desplegado

- **El Product Owner reconstruyó producción desde `main` = `ce1c68f`** (fusión #193, la adopción de
  MinVer) con `docker compose up -d --build` en `~/docker/lab-geometria` y **`Documentacion__Publicada=true`**
  en el `.env`. Verificado el 2026-09-13 02:23 UTC contra `https://api-geometria.aplicada.stream`:
  `/salud` → `{"ready":true,"version":"0.8.1-alpha.0.135+ce1c68f3a37275c57861c7f2a86035a8f4699001",…}`;
  `/openapi/v1.json` → `200`; `/documentacion` → `302` (la redirección del explorador Scalar, no un
  punto de la superficie); `docker compose ps` → `lab-geometria-api healthy · lab-geometria-web healthy`.
  Antes, `/salud` decía `1.0.0+5c95dab…` y `/openapi/v1.json` daba `404`.
- **La altura 135 es la prueba de que el `Dockerfile` hizo su trabajo**: BuildKit deja el clon
  `--depth=1 --no-tags`, y sin el `git fetch --unshallow --tags origin` la versión habría sido
  `0.0.0-alpha.0`. Salió `v0.8.0` + 135 fusiones, que es lo que `main` es.

### Decidido por el Product Owner («ok, encargate de todo y sigue», 2026-09-12)

- **`PD-VER-01` · la etiqueta es manual.** La crea quien fusiona código de producción a `main`, en el
  mismo acto, con el número que MinVer imprime. **El job de CI no se implementa.**
- **`PD-VER-02` · el conjunto amplio.** `Directory.Build.props`, los `*.csproj`, `GeometriaFactory.sln`
  y `deploy/Dockerfile*` **cuentan** como código de producción, además de `src/**` y `visor/**`.
- **`PD-VER-03` · sin etiquetas retroactivas.** Ni `89f3ab3` ni `5c95dab` se etiquetan; la propuesta
  `v0.9.0`/`v0.9.1` queda registrada como descartada. El número no se puede calcular —cero `feat`/`fix`
  en 129 fusiones— y la regla rige desde su fecha, como en su momento con `c`, `d` y `f`.
- **La salida de `0.x` la hace `BT-00032`**: la primera fusión con las rutas públicas bajo `/v1/` se
  etiqueta **`v1.0.0`**, porque por `ADR-00010` el `MAJOR` de la ruta es el `MAJOR` del producto. El
  hueco `0.9.x` es deliberado y queda declarado en `Estrategia-Versionado.md` §3.c.

### Cerrado

- **`BT-00031` → `Done`** (1.3): los cuatro criterios cumplidos en producción; el recuento de puntos
  fuera de la guardia sigue en 4.
- **`BT-00033` → `Done`** (1.3): `/salud` con la versión real en producción, el evento de etiqueta
  declarado y decidido, y el tercer criterio cerrado por su segunda vía —declarado por qué no—.
- `Estrategia-Versionado.md` **5.1** (§3.c: las tres decisiones y la regla de `v1.0.0`; la deprecación
  sigue siendo `BT-00035`), `Mini-Plan.md` **3.2** (§3.5, dos celdas de `Estado`).

### Lo que queda

- `git tag -l` sigue devolviendo las cinco del 2026-08-18: **ninguna etiqueta se creó en esta corrida**.
  La próxima es `v1.0.0`, sobre la fusión de `BT-00032`, a mano.

## Límite de tasa por persona autenticada o por dirección de origen — 2026-09-13

**Rama:** `fase-k/bt-00029-rate-limiting` (`BT-00029`, fase `k`, sobre `8e5e2f9` = `v1.0.0`). Realiza el NFR
«Caudal sostenido» de `05` §8.1 sobre el riesgo del único escritor del almacén (`ADR-06002`). **No es un cambio
mayor del contrato**: ningún punto, verbo, cuerpo ni papel cambia; entra un código de respuesta nuevo, `429`,
que toda aplicación que consuma `/v1/` tiene que estar preparada para recibir y respetar con `Retry-After`.

### Agregado

- **`GeometriaFactory.Api`: `Composition/ContractRateLimiting.cs`**, con `Microsoft.AspNetCore.RateLimiting`
  (nativo del marco, sin paquete). Dos políticas: **`contrato`**, aplicada al grupo `/v1` entero desde
  `Program.cs`, que particiona **por persona** —el reclamo `sub` del acceso firmado, `ADR-00003`— cuando la
  petición trae acceso válido y **por dirección de origen** cuando no (los cuatro puntos anónimos, y también la
  petición que la guardia va a rechazar: el limitador corre después de autenticar y antes de autorizar, así
  tantear la guardia gasta cuota); y **`canje`**, propia y más estricta, siempre por origen, declarada sobre
  `POST /v1/auth/token` en `AuthenticationEndpoints`, porque es el único punto que recibe una contraseña en
  claro y cada intento cuesta una derivación anclada. **Nunca por una clave de aplicación**, que no existe
  (`ADR-00009`).
- **Umbrales derivados del NFR y configurables** (`RateLimiting__*`, escritos en `appsettings.json`): ventana
  **deslizante de 60 s**; **60 por minuto por persona** (el triple del caudal de una comisión: una persona a
  mano no llega, un guion en bucle sí); **120 por minuto por origen** (el front es una sola dirección para la
  comisión entera y no reenvía la del navegador, y la cifra tiene que dejar entrar a la comisión al principio
  de una clase); **30 canjes por minuto por origen** (deja pasar a esa comisión y acota un diccionario a 1.800
  intentos por hora por dirección). Un umbral en cero o negativo **detiene el arranque nombrando la llave**,
  como la clave de firma. Cuando `PT-05` mida el uso real se ajustan por configuración.
- **`429` con `Retry-After` en segundos enteros y sin cuerpo**: tercera respuesta sin código del contrato
  (`Contratos-REST.md` §5.1), al lado del `401` de la guardia. `/salud` y el explorador **quedan fuera**: la
  cuota es del grupo `/v1` y no global, para que el `healthcheck` no apague un contenedor sano.
- **`UseForwardedHeaders` al principio de la tubería.** `X-Forwarded-For` se honra **sólo desde las redes que
  `ForwardedHeaders__KnownNetworks__<n>` declare**, más el bucle local del marco. Detrás del túnel de
  Cloudflare, para el zócalo toda petición viene del contenedor del túnel: **sin esa llave la partición por
  origen colapsa en una sola dirección**. La red no está en la imagen porque es topología del host:
  **declararla es obligación del despliegue** (`ForwardedHeaders__KnownNetworks__0=172.23.0.0/16` en la
  composición de `~/docker/lab-geometria`, la red `jump-host-demos-net` donde conviven el túnel y el servicio).
- **`RateLimitingTests`** (integración, **14** pruebas), **con los umbrales por omisión y no con unos bajados
  para la prueba**: `429` y `Retry-After` en [1, 60] a la 61.ª petición de una persona, la 121.ª de un origen y
  el 31.º canje; otra persona y otro origen siguen en `200`; la misma persona desde otra dirección sigue en
  `429`; la petición que la guardia rechaza gasta cuota; `/salud` responde `200` más veces que cualquier cuota;
  `X-Forwarded-For` se honra desde una red declarada y se ignora desde otra; **una ráfaga concurrente de sesenta
  escrituras de una persona llega entera al único escritor sin ningún `5xx` y el almacén tiene exactamente las
  sesenta**; los valores por omisión son los que el contrato declara; un umbral en cero y una red que no es
  CIDR detienen el arranque. **Probada fallando**: sin las dos `RequireRateLimiting` fallan 7 de 14.
  `DataServiceHarness` deja de ser `sealed` y admite configuración adicional para simular el zócalo.

### Cambiado

- **`Contratos-REST.md` 1.9**: §4 pasa de diez a once códigos; **§4.1 nuevo** con la cuota, su sujeto, las
  cifras, el fundamento, la configuración, `X-Forwarded-For` y lo que queda fuera; §5.1 pasa a tres respuestas
  sin código; el párrafo que declaraba el `429` como ausencia informativa se reescribe con constancia de por qué
  cayó su fundamento (`ADR-00005` §2: «el único cliente legítimo es la pieza pública»). **`ADR-00005` no se
  reescribe**: su decisión de no paginar sigue vigente y sólo su párrafo sobre el caudal queda superado.
- **`Definicion-Superficie-HTTP.md` 1.13**: la fila del `429` en §4, transversal a los dieciséis puntos bajo
  `/v1/` y por eso sin tocar la columna de códigos de §3. **`Arquitectura-Unidad-Entrega.md` 3.13**: §8.1, fila
  «Caudal sostenido», una celda: el NFR gana su mecanismo; la cifra no cambia y sigue provisoria.

### Verificado

`dotnet build` Release **0 advertencias**; `dotnet test` **546/546** (Domain 94, Application 56, Integración
396; `main` tenía 532, los catorce nuevos son `RateLimitingTests`); contra el servicio de esta rama en
`127.0.0.1:5081` con almacén propio: `api/01-basico` **CONFORME 13/13** y `api/02-intermedio` **CONFORME** con
su `D-1` ya declarada (**ningún `429` en los 52 pedidos de los dos samples**); `curl`: el canje responde `429`
con `Retry-After: 60` y `Content-Length: 0` al agotar los 30 de la ventana, `/v1/aprovisionamiento` al agotar
los 120 del origen, y `/salud` responde `200` **200 de 200 veces**; `git tag -l` sin cambios. Los E2E de
Playwright y `api/03-avanzado` no se corrieron localmente. Detalle en la ficha `BT-00029`, §8.
## `v1.0.0` en producción — 2026-09-13

**Rama:** `fase-k/cierre-bt-00032` (`BT-00032`, fase `k`). Sin cambio de código: lo que cierra la tarea
es una etiqueta, un despliegue y una ratificación del Product Owner.

### Etiquetado

- **`v1.0.0` existe.** Etiqueta anotada creada sobre `8e5e2f9` —la fusión #195, la primera con el
  contrato bajo `/v1/`— y empujada: `git rev-parse v1.0.0^{commit}` = `8e5e2f9`; GitHub
  `refs/tags/v1.0.0` → objeto `295e7c0`. Es la **primera etiqueta manual** bajo `Estrategia-Versionado.md`
  5.1 (`PD-VER-01`: la crea quien fusiona, con el número que MinVer imprime) y la salida de `0.x` que la
  entrada anterior dejó anunciada. `git tag -l` pasa de cinco a seis.

### Desplegado

- **Producción reconstruida desde `main` = `8e5e2f9`** (`docker compose up -d --build`,
  `~/docker/lab-geometria`); ambos contenedores `healthy`. Verificado el 2026-09-13 02:48–02:52 UTC
  contra `https://api-geometria.aplicada.stream`: **`/salud` → `{"ready":true,"version":"1.0.0+8e5e2f947c05d83b1e33cb40e35bf89bac70ef71",…}`** —el
  sello de la etiqueta, calculado por MinVer sobre `v1.0.0` sin altura—; `GET /v1/trabajos` sin acceso →
  `401`; `GET /trabajos` sin prefijo → `404`; `POST /v1/auth/token` con credencial falsa → `401`;
  `GET /openapi/v1.json` → `200`; el front `/estado` → `200` y muestra `1.0.0+8e5e2f9…`. El log del web
  registra `GET http://lab-geometria-api:8080/v1/aprovisionamiento` y `GET …/salud`: el cliente antepone
  `v1/` al contrato y no a la salud, sin que `API_BASE_URL` haya cambiado.

### Ratificado por el Product Owner («ok, encargate de todo», 2026-09-12)

- **Las tres exenciones del prefijo dejan de ser un `SI NO RESPONDÉS`.** `/salud` (`A-16`),
  `/openapi/v1.json` y `/documentacion` quedan fuera de `/v1/`, como `Contratos-REST.md` 1.8 §3.1 lo
  escribió y `ContractRoutePrefix.ExemptRoutes` lo enumera. **`/salud` no se versiona.** `ADR-00010` **1.1**
  §7 deja de remitir esa decisión a `BT-00032` y la asienta como decidida.

### Cerrado

- **`BT-00032` → `Done`** (2.0): los cuatro criterios cumplidos, con la evidencia de la rama (532/532,
  las dos direcciones probadas fallando) y la de producción sobre `8e5e2f9`. `Mini-Plan.md` **3.3**
  (§3.5, una celda de `Estado`). Con esto, `BT-00034` y `BT-00035` —que dependen sólo de `BT-00032`—
  quedan destrabadas.

## Sample de onboarding: un cliente propio contra `/v1/`, sin el código fuente — 2026-09-13

**Rama:** `fase-k/bt-00034-sample-onboarding` (`BT-00034`, fase `k`, sobre `1748503` = `v1.1.0`). Realiza la
regla 5 de `ADR-00010` §2.2 —«el sample de onboarding de `BT-00034` la ejerce contra `/v1/`»— y la convención
de `ADR-00009` §7: el sample describe **el canje de credenciales de una persona**, no una clave de cliente.
**Sólo documentación y muestras**: `src/` no se toca y no hay etiqueta nueva.

### Agregado

- **`samples/api/04-cliente-http-basico/`**, el cuarto sample de la API y **el primero que corre sin el
  repositorio**: `curl`, `bash`, `awk` y `sed`, con la carpeta copiada a cualquier directorio. Cinco pasos en un
  solo comando: descubre el contrato desde `/openapi/v1.json` y lista sus **17** operaciones (**16** bajo
  `/v1/`, `/salud` exenta); canjea las credenciales de **una persona** por `POST /v1/auth/token`; lista
  `GET /v1/trabajos`; envía `E-1` del intake §20 —copia byte a byte del de `api/02-intermedio`— y recibe `201`
  con el trabajo en `Pendiente`, dos advertencias y ningún error; y agota la cuota de la persona a propósito
  hasta el `429`, leyendo `Retry-After` (entre 1 y 60, sin cuerpo). Dirección en `API_BASE_URL` (por omisión
  la superficie publicada), credenciales en `API_EMAIL` y `API_PASSWORD`; nada de eso está escrito, y el
  acceso firmado se usa y no se imprime. `verificar.sh` compara contra `esperado/salida.txt` y corre solo.
  El slug es `cliente-http-basico`, el que `Rules-Examples.md` §2.3 reserva para el cliente HTTP de una
  `rest-api`, agregado como carpeta extra y no como renombre.
- **`10-Examples/ejemplo-04-cliente-http-basico-api.md` 1.0**, con `VER-00004` en la forma completa de
  `Rules-Examples.md` §4.6 (`recorre`/`no_recorre` por caso de uso, `discrimina` por aserción) y la
  `evidencia` de la corrida. **`SD-00004`** en `Matriz-Sensado-Deriva.md` 2.3, directamente en `Verificado`.

### Cambiado

- **`10-Examples/README.md` 2.1**: fila del cuarto en §2 y §3, párrafo en §1, desvío 4 en §6. Los recuentos de
  los tres primeros no cambian. **`samples/README.md` 2.1**: veinte carpetas, diecisiete corriendo.
- **`BT-00034` → `Done`** (2.0), con los comandos con que se levantó el servicio y se preparó la persona.

### Verificado

Contra el servicio de la rama en `127.0.0.1:5081` (`mcr.microsoft.com/dotnet/sdk:10.0`, `--network host`,
`ASPNETCORE_ENVIRONMENT=Production`, `Documentacion__Publicada=true`, almacén y clave de firma propios,
umbrales del límite de tasa por omisión), **desde una copia de la carpeta fuera del repositorio**:
**CONFORME 6/6**, salida 0; el `429` llegó en la 61.ª petición de la persona con `Retry-After: 60`.
`verificar.sh` probado fallando (`NO CONFORME`, salida 1) y `run.sh` sin credenciales (salida 2). El documento
OpenAPI de producción se leyó (`200`, 17 operaciones, las mismas); **la corrida completa contra producción no se
hizo**, porque el paso 5 gasta la cuota a propósito. `git tag -l` sin cambios.
## La deprecación del contrato queda escrita: un cuatrimestre, `Deprecation` y `Sunset` — 2026-09-13

**Rama:** `fase-k/bt-00035-deprecacion` (`BT-00035`, fase `k`, sobre `1748503` = `v1.1.0`). Tarea `docs`:
**sin cambio de código**. Ejecuta la decisión `D-02` del Product Owner (mesa del ciclo 2, §8, default
aceptado) y la remisión de `ADR-00010` §2.1 punto 4. **Hoy sólo existe `/v1/` y nada está deprecado**: esta
entrada escribe la regla que regirá el día que exista `/v2/`, y no cambia ninguna respuesta.

### Agregado

- **`SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/09-Devops/Estrategia-Versionado.md` 6.0, §6.b «La
  deprecación del contrato REST»**, ítem propio, en ocho filas: `/v{N+1}/` se abre **sólo por un cambio Mayor**
  de `Contratos-REST.md` §6, que sube `MAJOR` del producto; `/v{N}/` **convive un cuatrimestre como mínimo
  desde que `/v{N+1}/` entra a producción** —cuatro meses calendario, prorrogable al fin del cuatrimestre
  lectivo, nunca acortable—; desde esa fusión **toda respuesta de `/v{N}/` lleva `Deprecation: @<epoch>`**
  (`draft-ietf-httpapi-deprecation-header`) **y `Sunset: <fecha HTTP>`** (RFC 8594), sobre el grupo entero,
  y este registro anuncia las dos fechas en una entrada `BREAKING`; al vencer, `/v{N}/` responde **`410`**
  y no `404` —fue contrato, se anunció y se retiró a propósito; la forma sin prefijo nunca lo fue—, y el
  código **entra a los once de `Contratos-REST.md` §4 por la tarea del primer retiro, no por ésta**; dos
  prefijos como máximo; **`/salud`, `/openapi/v1.json` y `/documentacion` no se deprecan**; cómo se
  materializa en código lo decide la tarea que abra `/v{N+1}/`. §1.1 y §6.1 remiten a §6.b en lugar de decir
  que `BT-00035` «la escribe». Sube major por el precedente de la 5.0: entra una regla, no sólo estructura.

### Cambiado

- **`Contratos-REST.md` 1.10**: §3.1 remite a la política; §4 declara `Deprecation` y `Sunset` como cabeceras
  **futuras** que ninguna respuesta lleva hoy, con la medición contra producción transcripta, y que `410` no es
  el duodécimo código; §6 precisa su remisión. Sigue habiendo once códigos y diecisiete puntos.
- **`ADR-00010` 1.2**: §7 pasa de «remitido a `BT-00035`» a «decidido por `BT-00035`», con la cita a
  `Estrategia-Versionado.md` 6.0 §6.b. El cuerpo de la decisión no cambia.
- **`BT-00035` → `Done`** (2.0), con el criterio verificado en sus tres partes.

### Verificado

- `curl -s -D - -o /dev/null https://api-geometria.aplicada.stream/salud` y `…/v1/aprovisionamiento` →
  `HTTP/2 200`, **sin `Deprecation` ni `Sunset`** (`HEAD` responde `405`, `allow: GET`).
- `git tag -l` → las seis etiquetas, **sin cambios**; `git diff --stat main` **sin `src/`**.

## Los veinte samples bajo el árbol de solución, y una puerta para que no vuelva a faltar ninguno — 2026-09-13

**Rama:** `estructura/dc5-samples-en-la-solucion` (sobre `1ce1b2c`). Completa la decisión **DC-5** del Product
Owner del 2026-09-11 —«todos los proyectos quedan bajo el árbol de solución de Visual Studio, aunque sea bajo
carpetas virtuales»—, que la reestructuración del 2026-09-12 aplicó a medias. **`src/` no se toca.**
Evidencia en `evidencia/2026-09-13-dc5-samples/`.

### Corregido

- **El mensaje de `6cc6f86` —«todo entra al árbol de solución»— era inexacto, y la entrada del 2026-09-12 de
  este registro lo repitió.** Entraron el nodo del visor, los nueve `Sample.*.csproj` y `E2ETests`; **quedaron
  afuera las diez carpetas de `samples/` que no eran proyectos .NET** —`visor/01-basico`, `visor/02-intermedio`,
  `visor/03-avanzado`, `api/01-basico`, `api/02-intermedio`, `api/03-avanzado`, `contracts/01-basico`,
  `contracts/02-intermedio`, `contracts/03-avanzado` y `web/01-datos-seed`—, que la especificación que ese
  commit implementaba (`Especificacion-Estructura-Solucion.md` §6.1) incluía por nombre: «`api/`, `web/` y
  `visor/0N` como `SolutionItems` o nodos inertes». **La undécima, `api/04-cliente-http-basico`, entró el
  2026-09-13 y quedó afuera también**, porque nada miraba. Ninguna construcción ni prueba lo podía notar: una
  solución a la que le falta un nodo construye igual. La entrada del 2026-09-12 no se reescribe; se corrige acá.

### Agregado

- **Once nodos sin construcción**, uno por carpeta, anidados en las carpetas de solución nuevas
  `samples/visor`, `samples/api`, `samples/contracts` y `samples/web`: `Sample.Visor.{Basico,Intermedio,Avanzado}`,
  `Sample.Api.{Basico,Intermedio,Avanzado,ClienteHttpBasico}`, `Sample.Contracts.{Basico,Intermedio,Avanzado}` y
  `Sample.Web.DatosSeed`. El nombre sigue el precedente de `Sample.Domain.Basico`: `Sample.<Segmento>.<Nivel>`,
  con el slug en PascalCase cuando no es un nivel. La forma es la del visor (opción D de P-7):
  `Microsoft.Build.NoTargets/3.7.56`, `EnableDefaultItems=false`, un `None Include="**/*"` sin `bin/`, `obj/` ni
  `node_modules/`, **sin `Exec`, sin `Target`, sin `ProjectReference` y sin `verify` enganchado a `Build`**.
  `GeometriaFactory.sln` pasa de **20 a 31** proyectos, editado a mano con GUID nuevos: las configuraciones de
  solución siguen siendo `Debug|Any CPU` y `Release|Any CPU`, iguales a `main`.
- **`scripts/verify-solution-tree.sh`**, paso propio de `ci.yml` antes de `QG-01`, sin .NET. Falla si un
  `.csproj` de `src/`, `tests/` o `visor/` no está en la solución (A-1), si una carpeta `samples/<segmento>/<NN-…>`
  no tiene exactamente un archivo de proyecto en la solución (A-2) o no cuelga de `samples/<segmento>` (A-3), si
  la solución nombra un archivo que no existe (A-4), si un nodo NoTargets declara `Exec`, `Target` o
  `ProjectReference` (A-5), o si aparecen plataformas `x64`/`x86` (A-6). **Probada fallando** sobre `main` (once
  carpetas, salida 1) y sobre copias con cinco defectos inyectados (salida 1 en los cinco).

### Cambiado

- `samples/README.md` **2.2** (declara las veinte carpetas y la forma de los inertes), `PRODUCT-INTAKE` **4.6**
  (§16, una línea), `PRODUCT-MANIFEST` **6.1** re-derivado (§2.B dice qué más agrupa la solución y por qué no son
  filas) con `_legacy/2026-09-13/`, y `Plan-Etapa-A` **1.13**. `Directory.Build.props`: el comentario de MinVer
  pasa de «veinte» a «treinta y un» proyectos.

### Verificado

- `dotnet sln list`: **20 → 31** proyectos, los veinte samples anidados en `samples/<segmento>`; configuraciones de
  solución iguales a `main` (`diff` vacío).
- `dotnet build GeometriaFactory.sln -c Release -p:SkipVisorBuild=true`: **0 advertencias, 0 errores**;
  `dotnet test --no-build`: **546/546**, como `main`. Los once nodos no dejan ningún `.dll`. La primera construcción
  falló con `MSB4025` en los tres nodos del visor —el comentario decía `npm --prefix`, y un comentario XML no admite
  `--`— y se corrigió antes de medir.
- `scripts/verify-solution-tree.sh`: salida **1** sobre `main` (once carpetas), **0** en la rama y **1** en las cinco
  copias con un defecto inyectado.
- `scripts/coverage.sh`: **líneas iguales en los seis proyectos** con árbol fresco (`Web` 1504/1828 antes y después).
  **Condición de medición encontrada**: `QG-03` de `GeometriaFactory.Web` baja trece líneas (1504 → 1491) cuando el
  anillo de claves de `src/GeometriaFactory.Web/App_Data/claves/` ya existe de una corrida anterior, porque el aviso
  de su creación es lo único que ejercita `Services/StartupObservations.cs` en las pruebas. Medido en los dos
  sentidos sobre `main` y sobre la rama. Probablemente explica el 1491 «igual» de la evidencia del 2026-09-12. **No se
  corrige en esta rama**: `src/` y `coverage.sh` quedan fuera del alcance.

## El canje se limita por cuenta, y el origen tolera una comisión (fix) — 2026-09-13

**Rama:** `fix/canje-por-cuenta` (sobre `b58dec3`, `main` = `v1.1.1`). Corrige un defecto de producción
introducido por **`BT-00029`** (`v1.1.0`). Decisión en
[`ADR-00011`](SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/05-Arquitectura-Tecnica/Adrs/ADR-00011-El-Canje-Se-Protege-Por-Cuenta-Y-El-Origen-Tolera-Una-Comision.md).

### Corregido

- **La cuota del canje dejaba afuera a una clase.** La política `canje` particionaba por dirección de origen con
  30 intentos por minuto, y los puntos anónimos con 120. El front no reenvía la dirección del navegador —en
  producción todo ingreso llega desde el contenedor del front— y en la facultad los alumnos salen por un mismo
  NAT: la comisión entera compartía un cupo de treinta canjes por minuto, contra `RN-B1`. El E2E del banco local
  lo detectó y estaba en rojo desde la fusión #198 (5 de 32, `429` en la preparación, la limpieza y el desenlace).
  **La causa**: particionar la defensa contra la fuerza bruta por origen detrás de un intermediario propio y de
  un NAT de aula. Las entradas anteriores de este registro no se reescriben.

### Cambiado

- **Defensa por cuenta**: `Composition/CredentialAttemptThrottle.cs`, un `PartitionedRateLimiter` inyectado —10
  intentos **fallidos** por correo normalizado (`EmailIdentity.Normalize`, `ADR-06003`) cada 900 s, desde
  cualquier origen— que `A-01` y la forma sin sesión de `A-05` consultan sobre el cuerpo ya enlazado, antes de
  buscar la cuenta y de derivar. Los ingresos correctos no gastan cuota; el `429` es el mismo para una cuenta
  existente y una inexistente.
- **Topes por origen holgados**: `PermitsPerAddress` 120 → **1200**, `CredentialExchangePermitsPerAddress` 30 →
  **600**, para trescientos alumnos detrás de una dirección (corte de 334 de
  `Audit/Medicion-Volumen-De-Comision-2026-08-31.md`; `D5` cerrada por incognoscible). `PermitsPerPerson` no
  cambia. La mitigación por variable de entorno de producción queda igual a los valores por omisión y sobra.
- `appsettings.json`; `Contratos-REST.md` **1.11** §4.1; `Arquitectura-Unidad-Entrega.md` **3.14** §8.1;
  `Decisiones-Arquitectura.md` y `README.md` de `05` **2.3**; ficha `BT-00029` **1.4** (control de cambios).

### Verificado

- `dotnet build GeometriaFactory.sln -c Release -p:SkipVisorBuild=true`: **0 advertencias, 0 errores**;
  `dotnet test`: **Domain 94/94, Application 56/56, Integración 403/403 = 553/553** (`main`: 546; en
  `RateLimitingTests` se reemplaza la prueba del canje por origen y entran seis pruebas y dos casos de teoría).
- **Probadas fallando**: las pruebas nuevas contra el `src/` de `main` (umbrales de cuenta como constantes) →
  **6 de 19 en rojo**: comisión detrás de un origen, cuentas reales más allá de treinta, misma cuenta desde
  orígenes distintos, `429` indistinguible, cuota compartida con `A-05` y umbrales por omisión.
  `SuccessfulSignInsDoNotSpendTheAccountQuota` pasa también sobre `main`, por coincidencia aritmética (20
  ingresos + 10 fallos = el cupo de 30 por origen), y no cuenta como probada fallando.
- **E2E del banco local** (`gf-e2e:local`, Playwright 1.49 + SDK 10, chromium): **32/32**, ningún `429`; 42
  canjes desde un solo origen en la corrida.

## Punto de control de la fase `k`: OK del Product Owner — 2026-09-13

**Rama:** `migracion/a-13.16` (sobre `b9675d8`, `main` = `v1.1.1`). Sin cambio de código: lo que cierra la fase
es el OK explícito del Product Owner en el punto de control, que `Roadmap-Producto.md` §5.1 exige para toda
transición y que la 1.12 había dejado pendiente sin afirmarlo por él.

### Cerrado

- **Fase `k` · exposición pública de la API y versionado del contrato.** Testimonio literal del Product
  Owner, 2026-09-13: «tenés el ok de la fase K». `Roadmap-Producto.md` **1.13** (§3 y §2.1, fila `k`);
  `Mini-Plan.md` de `GeometriaFactory-Api` **3.5** (§3.5). Asentado como actuación 002 del expediente
  `SDD/Expedientes/0001-Migracion-Normativa-A-13.16/`.

### Corregido

- `Roadmap-Producto.md` §2.1, fila `k`: la celda de release seguía diciendo «Pendiente. Ninguno de los diez
  ítems está construido» después de que la 1.12 declarara el entregable realizado en §3. Las entradas
  anteriores de este registro no se reescriben.

## Migración normativa SDD 13.7 → 13.16, cerrada — 2026-09-13

**Rama:** `migracion/a-13.16` (fusionada en `1d4afc4`, PR #205: expediente, plan, mesa e informe de M6 con la
migración **parcial declarada**) y `migracion/a-13.16-aplicacion` (aprobación del Product Owner, aplicación,
audit de cierre y M5). Sin cambio de código. Expediente completo en
`SDD/Expedientes/0001-Migracion-Normativa-A-13.16/`; el instrumento es `Master-Prompt-Migracion.md` 2.10.

### Cambiado

- **`SDD/Intake/`**: `PRODUCT-INTAKE` **5.0** (plantilla 3.6: marca y pertenencia del insumo de construcción,
  perfil por ecosistema, §16.1 por sample; major por el caso (b) de `Master-Prompt.md` §13, con la aprobación
  explícita del Product Owner en la actuación 011); `PRODUCT-MANIFEST` **7.0** re-derivado (plantilla 6.1) y
  **7.1** con la **procedencia en SDD 13.16** (fase M5, con la cadena completa).
- **`SDD/Docs/`**: `Vista-Producto.md` **1.11**, `Pipeline-Producto.md` **1.9**; columna «Ciclo de origen» en
  las 118 filas de ítems diferidos de ocho documentos de 05, 06 y 09 (95 derivadas, 23 no derivables) y campo 5
  en los ocho bloques de `CU-08001` a `CU-08008`; `ADR-14001` a `ADR-14004` revisados (`Migracion-Rules.md` §4.7,
  no contemplado × 4, contadores 4/4/3/1). Veinticuatro documentos, cada uno con su estado previo en
  `_legacy/2026-09-13/`.
- **`SDD/Docs/Audit/`**: `Plan-Migracion-13.7-a-13.16.md` **1.4** (aprobado, aplicado, cerrado; §4.1 estado por
  fila), `Mesa-2026-09-13.md` **1.3** (`DD-8`, `DD-9`), `Informe-Migracion-13.7-a-13.16.md` **1.1** (audit de
  cierre, migración **completa**).

### Verificado

- La aplicación se reprodujo byte a byte sobre un worktree descartable (E-022): mismos hashes de árbol de
  `SDD/Intake` y `SDD/Docs`, los siete diffs aplican en reversa, 24 de 24 snapshots idénticos al estado previo,
  0 enlaces rotos en 984, `verify-solution-tree.sh` conforme.
- Audit de cierre de M4 por auditor independiente: **APROBADO CON OBSERVACIONES**, 0 P0, 2 P1, 3 P2, 8 P3,
  todos los P1 y P2 cerrados en la corrida (informe 1.1 §9).
- `dotnet build` de `GeometriaFactory.Web` en `sdk:10.0` sin `SkipVisorBuild` y sin Node: `MSB3073`, código 127
  (E-023), la conducta que `Pipeline-Producto.md` §4 declara.

### No hecho, y declarado

- **116 cabeceras «Trazabilidad upstream»** siguen citando el intake o el manifiesto con número de versión: el
  plan aprobado no las incluía (`DD-8` de `Mesa-2026-09-13.md` §8; ofrecidas al Product Owner en el lote de
  cierre). `visor.bundle.js` en el intake (`DD-5`), los residuos del modelo por proyecto de código (`DD-6`),
  los registros de cambios desordenados (`DD-7`) y nueve eventos de cierre que nombran un momento (`DD-9`).
- Los puntos C, D y E del lote de la actuación 010 no tuvieron respuesta: rigen sus defaults.

## Se retira `evidencia/` de la raíz, y la reparación de la octava reanudación — 2026-09-13

**Ramas:** `chore/retirar-evidencia-de-la-raiz` (PR #207, `3662bf9`, fusionada en `4fc2763`) y `reanudacion/8-2026-09-13-reparacion`. Sin cambio de código.

### Retirado

- La carpeta `evidencia/` de la raíz, con las corridas fechadas del 2026-09-01 al 2026-09-13, por decisión del Product Owner del 2026-09-13. **El contenido no se pierde**: vive en el historial y se cita como `git show 3662bf9^:evidencia/<corrida>/<archivo>`. Las entradas anteriores de este registro que nombran `evidencia/2026-09-12-estructura-solucion/` y `evidencia/2026-09-13-dc5-samples/` no se reescriben: ésta es su forma de resolverse.

### Corregido

- La entrada «Punto de control de la fase `k`» dice «`main` = `v1.1.1`» sobre `b9675d8`; `b9675d8` es **`v1.1.2`** (`git for-each-ref refs/tags/v1.1.2`), y `v1.1.1` es `b58dec3`.
- Octava reanudación, salida `A` ([`SDD/Docs/Audit/Estado-Del-Destino-2026-09-13.md`](SDD/Docs/Audit/Estado-Del-Destino-2026-09-13.md), [`SDD/Docs/Audit/Mesa-2026-09-13-ciclo-2.md`](SDD/Docs/Audit/Mesa-2026-09-13-ciclo-2.md)): `Roadmap-Producto.md` 1.15 (§4 reescrito por decisión del Product Owner sobre `E-1`), `SDD/Docs/README.md` 2.9 (fila `i` abierta, fila `k` cerrada), `Medicion-PT-05.md` 1.3, `Vision-Producto.md` 1.7, `Mini-Plan.md` de Api 3.6, ítems diferidos de Api/05 3.16, Api/06 4.7 y Api/09 3.11, `ADR-10008` 1.1, `samples/README.md`, y los README de la raíz y de `SDD/` como punteros.

### Desplegado

- Las entradas de `v1.1.1` y `v1.1.2` no llevaron esta sección. Al 2026-09-13 23:45 UTC, `GET https://api-geometria.aplicada.stream/salud` informa `1.1.2+b9675d8`: la corrección del límite de canje por cuenta (`ADR-00011`) está en producción. La versión en producción la informa `/salud`; este registro asienta la observación con su fecha.

### No hecho, y declarado

- `scripts/verify-stage-i.sh` sigue midiendo el flujo de FTP retirado y exigiendo producción = `main` (`DD-R8-1`). Es la **quinta** vez que este registro llega tarde a una fusión propia (`DD-R8-2`).

## El runner propio deja de ejecutar pull requests de forks, y el recorrido desplegado se desata del FTP — 2026-09-14

**Rama:** `ci/runner-propio-solo-ramas-propias`. Sin cambio de código de producción. Lote 0 del plan de la mesa a pedido del 2026-09-14 (`SDD/Docs/Audit/Mesa-2026-09-14.md`, ítems R-01 y R-15), aplicado por decisión del Product Owner.

### Cambiado

- `.github/workflows/e2e.yml`: el trabajo `banco-local`, que corre en el runner propio, sólo se ejecuta en un pull request si viene de una rama de este repositorio; el de un fork lo salta y el Resumen acepta el salto.
- `.github/workflows/e2e.yml`: el recorrido contra el laboratorio desplegado deja de dispararse al terminar `deploy-front-ftp`, un canal retirado; queda a mano.

### No hecho, y declarado

- El ajuste de GitHub que exige aprobación para ejecuciones de colaboradores externos, la rotación de las credenciales que usa el recorrido desplegado y la revisión del runner son del Product Owner.

## Mesa de expertos a pedido sobre el proyecto entero, para continuar la fase `i` — 2026-09-14

**Rama:** `docs/mesa-2026-09-14`. Sin cambio de código. Registro: `SDD/Docs/Audit/Mesa-2026-09-14.md`.

### Agregado

- Evaluación del proyecto entero pedida por el Product Owner: ocho comisiones, refutador y jurado de cinco. 26 ítems consolidados, 25 procedentes. Dictamen sobre qué es hoy el producto, qué falta para cerrar `i` criterio por criterio y quién puede hacerlo, en qué orden trabajar y qué riesgos van antes.
- Plan en cuatro lotes, siete deudas declaradas (`DD-R9-1..7`) y siete escaladas. El Product Owner respondió cuatro (lote 0, E-1, E-3, E-6); las otras rigen por su default.

### No hecho, y declarado

- **Tres ítems de seguridad tienen su detalle reservado** hasta el commit que corrige cada uno, porque el repositorio es público (`DD-R9-7`).
- Del lote 0 quedan los ajustes de GitHub, la rotación de credenciales, la revisión del runner y las cabeceras en el borde, a cargo del Product Owner.

## Un acceso deja de servir en cuanto la cuenta se bloquea o se elimina — 2026-09-14

**Rama:** `seguridad/lote1-admision-en-cada-peticion`. Lote 1 del plan de la mesa del 2026-09-14 (`SDD/Docs/Audit/Mesa-2026-09-14.md`, R-05, primera parte), aprobado por el Product Owner.

### Corregido

- `GeometriaFactory.Api`: la guarda que corre en cada petición con acceso firmado evalúa la **admisión completa de la cuenta** (`Account.EvaluateAdmission`) y no sólo la marca de cambio de contraseña. Un acceso obtenido antes de que el administrador bloquee la cuenta responde `403` con `AccountNotEnabled`; si la eliminó, `401` genérico. La cuenta marcada y habilitada sigue recibiendo el mismo código que antes.

### Verificado

- Prueba nueva `AnAccessObtainedBeforeTheAccountIsBlockedOrDeletedStopsWorkingOnTheNextRequest`, **vista fallar** contra la guarda anterior (`Expected: Forbidden · Actual: OK` en el paso del bloqueo) y pasar con la nueva.
- Build sin errores y suite completa en verde: Domain 94, Application 56, Integration 404.

### No hecho, y declarado

- La revocación del acceso cuando la propia cuenta cambia su contraseña (REF-02) necesita una marca temporal persistida y va en su propia unidad, junto con la prueba de migración sobre datos (R-10).

## Cambiar la contraseña propia revoca los accesos anteriores, y la migración se prueba sobre datos — 2026-09-14

**Rama:** `seguridad/lote1-revocacion-por-cambio-de-credencial`. Lote 1 del plan de la mesa del 2026-09-14 (`SDD/Docs/Audit/Mesa-2026-09-14.md`, REF-02 y R-10), aprobado por el Product Owner.

### Cambiado

- `GeometriaFactory.Domain`: `Account.CredentialChangedAt`, el instante del último cambio de credencial hecho por la propia cuenta.
- Transformación de esquema **`20260914112430_CredentialChangedAt`**: una columna nula nueva en `Account`. Es la primera que se aplica sobre un almacén con datos.
- `GeometriaFactory.Api`: la guarda de cada petición deja sin efecto un acceso emitido antes de ese instante (`401` genérico). `POST /v1/cuenta/contrasena` con sesión responde **con un acceso nuevo**, ampliación aditiva del contrato `/v1/`; sin sesión, sigue `200` sin cuerpo.
- `GeometriaFactory.Web`: la pantalla de cambio de contraseña reemplaza en la sesión el acceso por el nuevo, de modo que quien cambia su contraseña no queda afuera.
- `ADR-00003` y `Definicion-Superficie-HTTP.md` (fila A-05) declaran la revocación y por qué el acceso nuevo es reingreso y no refresco.

### Verificado

- `ChangingTheOwnPasswordRevokesEveryEarlierAccessAndHandsBackOneThatWorks`, **vista fallar** contra el código anterior en sus dos mitades: el acceso de otro dispositivo seguía respondiendo (`Expected: Unauthorized · Actual: OK`) y el cambio no devolvía acceso (cuerpo vacío).
- `CredentialChangedAtMigrationTests` (R-10), **vista fallar** sin la transformación (`Assert.NotEmpty`: no había nada pendiente) y pasar con ella: la cuenta escrita en la transformación anterior queda intacta y la columna nueva, nula.
- La prueba que fija el linaje de transformaciones sube de 4 a 5, nombrando la nueva.
- Build sin advertencias; suite completa en verde: Domain 94, Application 56, Integration 406.

### No hecho, y declarado

- La pantalla del front no tiene prueba propia del reemplazo del acceso: lo cubren el contrato y la guarda. El recorrido E2E del banco local la ejercita en CI.

## El ingreso del E2E espera una condición del producto y deja de agotarse de a ratos — 2026-09-14

**Rama:** `pruebas/e2e-ingreso-espera-condicion`. Sin cambio de código de producción.

### Corregido

- `tests/GeometriaFactory.E2ETests/Infraestructura/PruebaE2E.cs`, `IngresarAsync`: después del clic de ingreso esperaba `LoadState.Load`, y en el banco local de CI se agotaba a los 30 s de a ratos. Pasó en dos corridas de tres el 2026-09-14 (PR #210 y #212), con dos pruebas distintas y siempre en esa línea. Ahora espera lo que el ingreso produce: **salir de `/ingreso`** o **quedarse con un aviso** (`role="alert"`), lo que llegue primero, y después sólo `DOMContentLoaded`.

### Verificado

- Suite E2E completa en el banco local, **tres corridas seguidas**: 32/32 en cada una, entre 15 y 17 s. Las corridas de CI con la espera anterior tardaban entre 37 y 44 s.
- El caso que espera quedarse en `/ingreso` (`LaCredencialEquivocadaNoEntraYLaPantallaLoDice`) sigue pasando.

### No hecho, y declarado

- La intermitencia no se puede forzar en rojo a voluntad. La evidencia del defecto son los dos registros de CI con el agotamiento en `PruebaE2E.cs:187`.

## El ingreso del E2E deja de esperar un evento de carga después de salir de `/ingreso` — 2026-09-14

**Rama:** `pruebas/e2e-ingreso-sin-espera-de-carga`. Sin cambio de código de producción. Completa lo que la entrada anterior dejó a medias.

### Corregido

- `tests/GeometriaFactory.E2ETests/Infraestructura/PruebaE2E.cs`, `IngresarAsync`: la espera de `DOMContentLoaded` que quedó después de salir de `/ingreso` se agotaba a los 30 s en el banco local de CI. Pasó tres corridas seguidas del PR #214, siempre en `EnPantallaAnchaElListadoEsUnaTablaYNoHayTarjetas` y en esa línea. La navegación que sigue al ingreso la completa el propio producto sin cargar un documento nuevo, y el navegador quedaba esperando un evento que no llega. Ya no se espera ningún evento de carga: cada caso espera lo que va a mirar.

### Verificado

- Suite E2E completa en el banco local: 32/32. La intermitencia de CI no se reproduce en local; la verificación que decide es la corrida del banco local en CI de este PR y la de #214 actualizado.

### Apartamiento declarado

- Esta unidad se abrió con el PR #214 todavía sin fusionar, contra la comprobación 4 de la compuerta de arranque (`Master-Prompt.md` §12.1 T0). #214 no puede entrar mientras exista este defecto, y esperar a que entre habría sido un bloqueo circular.

## El reseteo del docente libera la cuota de intentos fallidos de la cuenta — 2026-09-14

**Rama:** `seguridad/lote1-reseteo-libera-la-cuota`. Lote 1 del plan de la mesa del 2026-09-14 (`SDD/Docs/Audit/Mesa-2026-09-14.md`, R-03; escalada E-2 con la propuesta aprobada por el Product Owner).

### Corregido

- `GeometriaFactory.Api`: la cuota por cuenta (`CredentialAttemptThrottle`) pasa de un `PartitionedRateLimiter` del marco, que no permite quitar una partición, a una **ventana deslizante propia que se puede liberar**. La ventana y el tope son los mismos: 10 fallos por correo normalizado en 900 s.
- El reseteo de la contraseña por el docente **libera la cuota de la cuenta**. Hasta hoy, una cuenta que un tercero dejó limitada recibía `429` con la provisoria recién entregada, y el único remedio era reiniciar el servicio.
- `Retry-After` por cuenta pasa a ser la espera real hasta que vence el fallo más viejo (entre 1 y 900 s).
- `ADR-00011` 1.1: la mitigación que declaraba en §6 deja de ser falsa.

### Verificado

- `TheAdministratorResetReleasesTheAccountFromItsFailedAttemptQuota`, **vista fallar** contra el código anterior (`Expected: Not TooManyRequests · Actual: TooManyRequests` después del reseteo).
- Las pruebas del límite de tasa siguen pasando sin cambios: 22/22.
- Build sin advertencias; suite completa en verde: Domain 94, Application 56, Integration 407.

## El front le dice al servicio de datos de dónde llegó el navegador — 2026-09-14

**Rama:** `seguridad/lote1-origen-real-detras-del-front`. Lote 1 del plan de la mesa del 2026-09-14 (`SDD/Docs/Audit/Mesa-2026-09-14.md`, R-02), aprobado por el Product Owner.

### Corregido

- `GeometriaFactory.Web`: `UseForwardedHeaders` primero en la tubería, con las redes de confianza de `ForwardedHeaders:KnownNetworks`, para ver la dirección real del navegador detrás del túnel. Sin ninguna declarada, la cabecera entrante se ignora.
- `GeometriaFactory.Web`: `BrowserOriginForwardingHandler` agrega `X-Forwarded-For` con esa dirección en las llamadas al servicio de datos que corren dentro de una petición del navegador: ingreso, registro y cambio obligado. Hasta hoy toda la comisión llegaba con la dirección del front y compartía una sola cuota por origen, y un solo cliente anónimo podía dejar a la clase sin entrar (`RN-B1`).
- `ADR-10009` nuevo; `Decisiones-Arquitectura.md` y `Entornos-Deploy.md` de Web lo registran.

### Verificado

- `BrowserOriginForwardingTests`: la dirección del navegador viaja al servicio de datos; la que escribe el navegador en la cabecera no viaja; fuera de una petición del navegador no se inventa ninguna. Las dos primeras **vistas fallar** contra el front anterior.
- Build sin advertencias; suite completa en verde: Domain 94, Application 56, Integration 410.

### No hecho, y declarado

- **Para que tenga efecto en producción, la composición del despliegue tiene que declarar dos redes:**
  - en el front, la red del túnel: `ForwardedHeaders__KnownNetworks__0`;
  - en el servicio de datos, la red del front (y la del túnel, si el servicio también se publica por él).

  Es configuración del host y no se versiona en este repositorio. Sin ellas, el comportamiento es el de antes.

## El documento OpenAPI dice cómo se autentica y en qué puntos — 2026-09-14

**Rama:** `api/openapi-declara-la-autenticacion`. Lote 1 del plan de la mesa del 2026-09-14 (`SDD/Docs/Audit/Mesa-2026-09-14.md`, R-08), aprobado por el Product Owner.

### Corregido

- `GeometriaFactory.Api`, `ApiDocumentation`: el documento declara el esquema de seguridad `Bearer` (HTTP, formato JWT) y lo exige en cada operación que pide acceso firmado. El requisito sale de los metadatos de cada punto (`RequireAuthorization` sin `AllowAnonymous`), así que un punto nuevo queda bien descripto sin tocar la documentación. Hasta hoy un cliente que leía el documento no podía saber qué puntos pedían acceso ni cómo presentarlo (`ADR-00009`: la superficie es pública para otros clientes).

### Verificado

- `TheDocumentDeclaresTheBearerSchemeOnlyOnThePointsThatRequireAccess`, **vista fallar** contra el documento anterior, que no declaraba ningún esquema: `GET /v1/trabajos` lo exige; `POST /v1/auth/token` y `GET /salud` no.
- Build sin advertencias; suite completa en verde: Domain 94, Application 56, Integration 411.

## El contrato de agentes dice lo que corre — 2026-09-14

**Rama:** `docs/contrato-agentes-al-dia`. Sin cambio de código. Lote 1 del plan de la mesa del 2026-09-14 (`SDD/Docs/Audit/Mesa-2026-09-14.md`, R-18).

### Corregido

- `SDD/Docs/Producto/11-Documentacion/Contrato-Agentes.md` 1.1, y `AGENTS.md` regenerado con los mismos cambios:
  - Protege `lab-geometria-web` y `lab-geometria-api`, el despliegue real. Nombraba cuatro contenedores `gf-*` que ya no corren, y dejaba sin proteger los que sí.
  - La batería de extremo a extremo tiene su propio guion y no entra en `test.sh`.
  - Fusionar un pull request es del agente con los checks en verde y sin reserva, por la decisión del Product Owner del 2026-08-31.
  - La tabla «A dónde ir» citaba diez documentos y **ocho no existían**. Cada fila apunta ahora a uno que existe, o declara que no está emitido (`DD-R9-2`).

## Las dos imágenes dejan de correr como root — 2026-09-14

**Rama:** `deploy/imagenes-sin-root`. Lote 1 del plan de la mesa del 2026-09-14 (`SDD/Docs/Audit/Mesa-2026-09-14.md`, imágenes sin `USER`), aprobado por el Product Owner.

### Cambiado

- `deploy/Dockerfile` y `deploy/Dockerfile.web` corren con el usuario sin privilegios de la imagen base (`app`, uid **1654**), y no como root.
- `/datos` (servicio de datos) y `App_Data/claves` (front) son de ese usuario desde la imagen.

### Verificado (imágenes construidas y levantadas en local, sin tocar el despliegue)

- **Servicio de datos, volumen nuevo:** el proceso corre como 1654, `/salud` responde `ready` y el almacén queda de `1654:1654`.
- **Servicio de datos, volumen con archivos de root (como el que ya existe en producción):** **el contenedor no arranca**, sale con código 78 y `attempt to write a readonly database`. Después de `chown -R 1654:1654`, `ready`.
- **Front:** corre como 1654, la portada responde 200 y escribe su clave en `App_Data/claves`.

### Paso obligatorio antes del primer despliegue de esta versión

```bash
docker run --rm -v <volumen-del-almacén>:/datos alpine chown -R 1654:1654 /datos
```

Lo mismo sobre el volumen de claves del front, si la composición lo monta desde un volumen existente. Sin esto, el servicio de datos no arranca.

## El autorregistro admite los dominios que el despliegue declara — 2026-09-14

**Rama:** `seguridad/lote1-registro-por-dominio-admitido`. Lote 1 del plan de la mesa del 2026-09-14 (`SDD/Docs/Audit/Mesa-2026-09-14.md`, `R-07`/`E-1`). Resuelto por delegación del Product Owner.

### Agregado

- `RegistrationPolicy` (Application): los dominios de correo que admite `CU-01`. Llega de `Registration__AdmittedEmailDomains__N`; **sin la llave, cualquiera**. La comparación es exacta sobre el dominio entero y no distingue mayúsculas.
- Código del contrato `EMAIL_DOMAIN_NOT_ADMITTED`, con `400`. Se verifica antes que la unicidad del correo, así que no dice si el correo ya tenía cuenta. Entra dentro de `/v1/` como cambio menor.
- `Registro-De-Cuenta` avisa que el correo no es de un dominio admitido y marca el campo.
- `Api ADR-00012`, con las alternativas descartadas. La de «alta sólo por el docente» queda abierta: dejaba sin sustento a `F-02` y exigía `/v2/`.

### Corpus

- `Definicion-Superficie-HTTP` 1.15, `CU-08002` 1.11, `Contratos-Abstractions` 1.4 (dieciocho vivos), `Norma-De-Nomenclatura` 1.30 y los dos índices de ADR de la API en 2.4.

### No hecho, y declarado

- **Ningún despliegue declara todavía un dominio**, así que en producción el registro sigue admitiendo cualquier correo. Declararlo es del Product Owner.
- El intake sigue diciendo «diecisiete vivos» hasta su próxima emisión.

## La puerta `c` autoriza los trece atributos que el marcado ya usaba — 2026-09-14

**Rama:** `puertas/lote2-atributos-autorizados`. Lote 2 del plan de la mesa del 2026-09-14 (`R-11`, escalada `E-6` aprobada).

### Cambiado

- `scripts/verify-stage-c.sh`, control `C-4`: la lista cerrada pasa de **diecisiete a treinta** atributos `data-gf-*`. Los trece nuevos los agregaron las etapas `e` a `h` sin sumarlos, y la puerta estaba en rojo en `main`.
  - **Cinco los lee el guion:** el diálogo irreversible, el que se promueve a modal y los tres del acuse de la escena.
  - **Ocho no los lee ningún guion:** son marcas de estado.
- `Norma-De-Nomenclatura.md` 1.31, §6.26: la tabla con el motivo de cada uno.

### No hecho, y declarado

- **La puerta entera no se corrió:** necesita las dos piezas levantadas. Se verificaron a mano los cuadres estáticos `3.c` y `3.d`.
- Cuatro familias de nombres siguen en castellano. Queda como deuda con evento en §6.26.

## El sello de versión muestra la identidad de la construcción — 2026-09-14

**Rama:** `interfaz/lote2-sello-de-version`. Lote 2 del plan de la mesa del 2026-09-14 (`R-14`).

### Cambiado

- `VersionSeal` (CMP-09) mostraba «Versión no identificada» siempre, también en producción. Ahora lee `VersionIdentity`, que se resuelve **una sola vez** en la composición desde `AssemblyInformationalVersion`, igual que `/salud`, y dibuja una de tres variantes (`Representacion-Sello-De-Version.md` §3):
  - **Publicada:** la versión sin adornos.
  - **Preliminar:** la versión con el distintivo textual `preliminar` (`gf-badge--warning`). Es el caso de `main`, que MinVer calcula como `-alpha.0.N`.
  - **No identificada:** sin atributo, o `0.0.0`, que es lo que MinVer calcula sin historial.
- El identificador de construcción no se muestra en la línea: §4 lo reserva para el detalle.

### No hecho, y declarado

- **El detalle de diagnóstico** —construcción, origen y «copiar para reportar»— sigue sin dibujarse. Es un despliegue interactivo y queda para otra unidad.

## Ninguna puerta queda en verde sin correr las pruebas que nombra — 2026-09-14

**Rama:** `puertas/lote2-pruebas-que-corrieron`. Lote 2 del plan de la mesa del 2026-09-14 (`R-24`).

### Corregido

- **`dotnet test` con un filtro que no coincide con nada sale con código 0** («No test matches the given testcase filter»), y así lo corrían `verify-stage-g.sh` y `verify-stage-h.sh`. Con una prueba renombrada, la puerta quedaba en verde sin haberla corrido.
- `scripts/lib-puerta.sh` suma `puerta_no_corrieron`: busca cada prueba pedida como `Passed` en el registro, con el registrador de consola en `normal`.
  - `g` y `h` la usan después de cada corrida.
  - `d`, `e` y `f` la usan en cada criterio. **Esto amplía el alcance de R-24 y se declara:** `lib-puerta.sh` ya comparaba el recuento contra lo pedido, pero una teoría con varios casos tapaba un nombre que dejó de existir.

### Verificado (en el contenedor del SDK)

- **Batería de `h` con un sexto nombre inexistente en el filtro:** `dotnet test` salió con 0 y corrieron 5.
  - Con los cinco nombres reales, no falta ninguno.
  - Con el renombrado, lo lista.
  - Con un prefijo parcial (`TheCommentIsStored`), también lo lista.
- `verify-stage-e.sh` entera: **CONFORME**, 5 criterios y 28 pruebas.

### No hecho, y declarado

- `g` y `h` no se corrieron enteras: `g` necesita `docker` y `dotnet` juntos, y este entorno los tiene separados.

## La publicación real queda descrita en el repositorio — 2026-09-14

**Rama:** `devops/lote2-publicacion-real`. Lote 2 del plan de la mesa del 2026-09-14 (`R-13`).

### Documentado

- `Guia-Publicacion-Image-Docker.md` 1.4, §2.2: la publicación **tal como corre**. Hasta hoy sólo estaba en una constancia en el servidor. Cubre:
  - la composición desde GitHub por `LAB_GEOMETRIA_REF` y los dos servicios con sus volúmenes, salud y límites;
  - las variables por nombre y el uid 1654;
  - el procedimiento con respaldo y verificación, y la reversión.
- **Divergencia declarada para el Product Owner:** la guía y `Estrategia-Versionado.md` dicen que se despliega la etiqueta de la etapa, y el servidor construye `main`.
- Sin nombres de red, rangos ni rutas del anfitrión.

## La batería E2E espera condiciones del producto, no el evento de carga — 2026-09-14

**Rama:** `pruebas/e2e-esperar-condicion-tras-el-envio`. Cierra el apartamiento de T0 declarado en #215: quedaban ocho `WaitForLoadStateAsync(LoadState.Load)` fuera del ingreso, y uno se agotó en CI (#226, paso 5 del recorrido del alumno).

### Corregido

- `RecorridoDelAlumnoTests` y `NavegacionTests` ya no esperan el evento de carga después de un envío o de un clic. Cada `Expect` que sigue espera la condición del producto. Después de enviar un trabajo se espera la **tarjeta de resultado**, que es lo que garantiza que el trabajo quedó guardado antes de navegar al listado.

### Verificado

- Banco local (`scripts/pruebas-e2e.sh chromium`), dos corridas: **32/32** las dos veces.
