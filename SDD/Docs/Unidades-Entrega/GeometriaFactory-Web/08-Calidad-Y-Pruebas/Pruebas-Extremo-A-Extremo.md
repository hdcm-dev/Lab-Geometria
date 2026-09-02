# Pruebas de extremo a extremo — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** Pruebas-Extremo-A-Extremo.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-09-02
**`tipo_unidad_entrega` (D8):** `web-monolith`
**Proyecto de código que la ejecuta:** `tests/GeometriaFactory.E2ETests`

---

## 0. Cómo leer este documento

**Este documento describe una batería que YA EXISTE Y CORRE**, no una que se propone. Cada
afirmación se puede comprobar contra un archivo del repositorio, y la sección lo nombra.

**No reemplaza a [`Estrategia-Testing.md`](Estrategia-Testing.md)**, que fija la pirámide y sus
apartamientos. Lo que agrega es lo que aquella declaraba **como una persona ejecutando pasos**
—«el guion de demostración de cada etapa, ejecutado en el navegador del equipo anfitrión»— y hoy
además **está automatizado**: §3.1 de aquel documento nombra las herramientas por su función y
dice, con todas las letras, «no es un marco: es una persona ejecutando pasos». Esa frase sigue
siendo cierta para el guion de demostración. Esta batería es **otra cosa que ahora también
existe**, y por eso se declara en un documento propio en lugar de reescribir aquél.

---

## 1. Qué cubre, y por qué hacía falta

**Cubre lo que ve una persona**: abre un navegador de verdad contra la pieza pública y hace lo que
haría alguien del aula. No compila contra ningún proyecto del producto —su archivo de proyecto no
referencia ninguno, y es deliberado—: una prueba de extremo a extremo que compila contra el
producto puede afirmar cosas que sólo son ciertas del lado del servidor.

**El hueco que tapa está medido, no supuesto.** Las tres baterías de la solución —dominio,
aplicación e integración— llegan al punto de acceso del servicio de datos y **no a la pantalla**.
Cuatro reportes del Product Owner del tipo «el botón de aprobar no funciona» convivieron con esas
baterías en verde. Y el `P0` `MI-02` —los tres listados sin dibujar **ninguna fila** por debajo de
768 px— sobrevivió catorce días a una construcción entera en verde.

---

## 2. Los dos modos de corrida

**El interruptor es una sola variable de entorno: `URL_BASE`.**

| | **Banco local** (sin `URL_BASE`) | **Laboratorio desplegado** (con `URL_BASE`) |
| --- | --- | --- |
| Qué se prueba | El producto publicado por la propia corrida | El sitio realmente publicado |
| Quién lo monta | `Infraestructura/BancoLocal.cs` | Nadie: ya está |
| Almacén | Uno de esta corrida, en un directorio temporal que se borra | El del docente, con alumnos de verdad |
| Administrador | Lo crea la corrida por `POST /cuentas/administrador` | El del laboratorio, por secreto |
| Secretos que exige | **Ninguno** | `API_BASE_URL`, `E2E_ADMIN_EMAIL`, `E2E_ADMIN_PASSWORD` |
| Datos ajenos que toca | **Ninguno** | Siembra y limpia lo suyo |
| Cuándo corre | En cada cambio, y a mano | A mano, y después de una publicación exitosa |
| Qué puede afirmar | Que el producto anda | Que **el sitio publicado** anda |

**Ninguno de los dos reemplaza al otro, y conviene tenerlo escrito.** El banco local no puede ver
un defecto del anfitrión —ni de la marca `Secure` de la cookie, ver §6—; el desplegado no se puede
correr sin la credencial del docente y por lo tanto **no se corre antes de empujar un cambio**.

### 2.1 Lo que el banco local levanta

Publica el servicio de datos y la pieza pública, los arranca sobre **puertos que le pide al
sistema** —nunca fijos: los contenedores del Product Owner no se rozan—, espera a que los dos
atiendan, y siembra el administrador. Al terminar los baja y borra la publicación y el almacén.

**Se corre la publicación y no el proyecto**, y ésa es la lección del 2026-09-01 escrita en
`tools/verificar-resolucion-del-trabajo.sh`: `dotnet run` sirve los archivos del marco desde el
manifiesto de recursos estáticos del proyecto, y una publicación los sirve como archivos de
verdad, que es lo que hace el anfitrión. **Un banco que no corre lo mismo que el anfitrión inventa
defectos propios y tapa los ajenos.**

---

## 3. Las ocho clases, y qué cuida cada una

**Treinta y dos casos**, medidos en la corrida del 2026-09-02 sobre el banco local.

| Clase | Casos | Qué deja de detectarse si se la borra |
| --- | --- | --- |
| `EstadoDelLaboratorioTests` | 5 | Las claves de protección efímeras, que rompían sesiones y componentes **en silencio** |
| `IngresoTests` | 3 | Un ingreso roto, que vuelve inútil cualquier otra prueba |
| `NavegacionTests` | 10 | El armazón: destinos por papel, el desvío sin sesión, el 404 con su código, y el cierre de sesión |
| `RecorridoDelAlumnoTests` | 1 | El camino entero de registro a entrega, que ninguna otra clase atraviesa |
| `ResolucionDelTrabajoTests` | 3 | Que «aprobar» **aplique el desenlace** y no sólo navegue |
| `VentanaMuertaTests` | 2 | Los controles dibujados y muertos mientras el circuito engancha |
| `FiguraQueNoSePudoLeerTests` | 2 | Que la pantalla afirme haber dibujado lo que no dibujó |
| `DisenoResponsivoTests` | 6 | La versión angosta: el `P0` `MI-02` y la regla `R-06`. Ver §5 |

---

## 4. Cómo se ejecuta

### 4.1 En la máquina de quien desarrolla

```bash
scripts/pruebas-e2e.sh                 # banco local, chromium
scripts/pruebas-e2e.sh firefox         # otro navegador
URL_BASE=... API_BASE_URL=... E2E_ADMIN_EMAIL=... E2E_ADMIN_PASSWORD=... \
  scripts/pruebas-e2e.sh               # contra el laboratorio desplegado
```

**Todo corre dentro de un contenedor**, construido desde [`deploy/e2e/Dockerfile`](../../../../../deploy/e2e/Dockerfile):
el anfitrión de esta casa no tiene el kit de desarrollo —decisión de la etapa `a`— ni las
bibliotecas de sistema que piden los navegadores. La imagen es la oficial de Playwright **de la
misma versión que el paquete**, más el kit de desarrollo; si la versión del paquete cambia, la
etiqueta de la imagen cambia con ella.

**El bundle del visor se genera antes**, con `scripts/build-visor.sh`: es artefacto, no se versiona,
y sin él la escena 3D no carga.

### 4.2 En la máquina de integración

[`.github/workflows/e2e.yml`](../../../../../.github/workflows/e2e.yml), sobre el runner propio
`[self-hosted, i7infra-dev]`, con dos trabajos:

| Trabajo | Cuándo | Navegadores | Secretos |
| --- | --- | --- | --- |
| `banco-local` | En cada empujón a `main`, en cada pull request y a mano | `chromium` | Ninguno |
| `pruebas` | A mano, y después de una publicación exitosa | Matriz: `chromium`, `firefox`, `webkit` | Los cuatro de §2 |

**La matriz no se corta con el primer rojo.** Que falle en un navegador y ande en los otros **es**
el dato: dice que el defecto es del navegador y no del producto.

> **Y hay un dato medido que conviene tener a la vista antes de leer esa matriz como una promesa.**
> **Sobre el banco local, sólo `chromium` está verificado.** Ver `H-E2E-01` en §6.

**El trabajo desplegado que no corre llega como `skipped`, y un salto no es un rojo.** El resumen
lo distingue a propósito: una compuerta que se pone en rojo sola se termina apagando, y este
repositorio ya lo pagó con `C-3` catorce días en rojo sin que nadie lo leyera.

### 4.3 La evidencia que deja

| Qué | Dónde |
| --- | --- |
| Informe de la corrida | `resultados-e2e/<navegador>.trx` |
| Traza de cada caso **fallido** | `tests/GeometriaFactory.E2ETests/resultados-e2e/trazas/` |
| Captura de cada caso fallido **y de la versión angosta aunque pase** | `…/resultados-e2e/capturas/` |

**Las capturas de la versión angosta quedan aunque el caso pase**, y es deliberado: en esta casa la
evidencia de lo visual se mira. Un conteo en verde ya dio por bueno un teléfono con tres recortes.

---

## 5. La versión angosta

**No se inventa ninguna regla.** Cada caso de `DisenoResponsivoTests` verifica una regla escrita en
`src/GeometriaFactory.Web/wwwroot/css/app.css`, y el comentario del caso dice cuál:

| Regla, tal como está escrita en la hoja | Qué se afirma | Origen |
| --- | --- | --- |
| `@media (max-width: 768px)` · `.gf-table-wrapper { display: none }` y `.gf-stacked-cards { display: flex }` | En 390 px el listado son tarjetas, **y se dibuja al menos una** | `P0` `MI-02` |
| Idem, en 1280 px | El listado es una tabla y no hay tarjetas | contrapeso del anterior |
| `.gf-two-columns { grid-template-columns: minmax(0,1fr) }` con `.gf-column--scene { order: 1 }` | Una sola columna, con la escena primero | `Linea-Base-Visual` |
| `R-06` · `.gf-footer-actions { flex-direction: column }` | La acción primaria por encima de la destructiva | `R-06` |
| Idem, en 1280 px | Las acciones siguen en una sola fila, la primaria primero | contrapeso del anterior |
| — | Ninguna pantalla del panel **se desplaza de costado** en 390 px | síntoma clásico de un ancho fijo |

**El aserto del `P0` no es que el contenedor esté visible: es que haya filas.** Un contenedor vacío
también se muestra, y el defecto de agosto era exactamente ése —la clase encendida sin emisor—.

**Se prueba un ancho y no un dispositivo.** Lo que el sistema visual declara es un ancho en píxeles;
emular un teléfono entero traería además factor de escala, agente de usuario y eventos táctiles,
o sea tres variables más para explicar un rojo.

---

## 6. Apartamientos y límites, declarados

| # | Qué | Por qué, y qué queda sin ver |
| --- | --- | --- |
| `AP-E2E-01` | **El banco local corre en `Development` y sobre HTTP llano** | En `Production` la cookie de sesión es `Secure` y sobre HTTP el navegador la descarta: **no habría sesión posible**. Montar HTTPS con certificado efímero es posible —`tools/verificar-resolucion-del-trabajo.sh` lo hace— pero volvería frágil la corrida de cada día. **Lo que no se puede ver desde el banco local es un defecto que dependa de la marca `Secure`**; para eso está el modo desplegado |
| `AP-E2E-02` | **La matriz de tres navegadores sólo corre en el modo desplegado** | El trabajo de cada cambio recorre `chromium`. Correr los tres en cada pull request multiplicaría por tres el tiempo del único runner propio |
| `H-E2E-01` | **HALLAZGO ABIERTO, no apartamiento: sobre el banco local, `firefox` no termina la batería y `webkit` no se midió** | Medido el 2026-09-02. `chromium` recorre los 32 casos en **18 s**. En `firefox`, los casos que **no** necesitan el circuito interactivo pasan y son rápidos —`IngresoTests`, 3 de 3 en **7 s**—, pero los que esperan a que el circuito habilite un control **agotan sus 30 s uno por uno**, y la corrida completa **no terminó en 25 minutos** en tres intentos, con la máquina en carga 3 y en carga 15. **No se sabe todavía de quién es**: puede ser del banco —que sirve por WebSocket, mientras el anfitrión real se repliega a sondeo largo—, del navegador, o del producto en ese navegador. **Se declara sin resolver en vez de callarse o de bajar el margen**: subir el tiempo de espera hasta que dé verde sería tapar la pregunta con un número |
| `AP-E2E-03` | **La batería no mide las propiedades de la escena 3D** | Lo que verifica es el recorrido. Las propiedades del visor —cero red, determinismo, piezas no dibujadas— tienen su propio sensado, declarado en [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) |
| `AP-E2E-04` | **No está registrada como puerta bloqueante de rama** | Que la protección de rama la exija es **decisión del Product Owner**, no de esta categoría |

---

## 7. Las reglas de la suite

1. **Ninguna prueba toca un dato que no sembró.** Contra el laboratorio real hay trabajos de
   alumnos de verdad: aprobar uno ajeno «para ver si el botón anda» sería tomar una decisión
   pedagógica que no le corresponde a una prueba.
2. **El que ensucia limpia, incluso cuando se cae en el intento.** La siembra que falla a mitad de
   camino borra lo que alcanzó a crear: dos corridas que no lo hacían dejaron cuatro cuentas en el
   panel del docente.
3. **Lo que se prueba se hace por pantalla; lo que se prepara, no.** La siembra va por el servicio
   de datos: si pasara por la interfaz, un defecto en el alta dejaría sin correr recorridos que no
   tienen nada que ver con el alta.
4. **La afirmación que vale la contesta quien guarda el dato.** Que la pantalla haya navegado no
   prueba que el desenlace se aplicó; eso se le pregunta al servicio de datos.
5. **Un aviso que no se ve es lo mismo que no avisar.** Los avisos de la suite van al error
   estándar, no sólo al progreso de NUnit.

---

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-02 | **Emisión inicial.** Declara la batería de extremo a extremo que ya existe y corre: sus dos modos, sus ocho clases y treinta y dos casos, cómo se ejecuta local y en integración continua, la evidencia que deja, y cuatro apartamientos con su motivo, más el **hallazgo abierto `H-E2E-01`** —`firefox` no termina la batería sobre el banco local—, declarado con su medición y **sin resolver**. Recoge la incorporación del **banco local** —que hace la batería ejecutable sin secretos y sin tocar datos ajenos— y de la clase de **diseño responsivo**, que es la que cuida el `P0` `MI-02`. |
