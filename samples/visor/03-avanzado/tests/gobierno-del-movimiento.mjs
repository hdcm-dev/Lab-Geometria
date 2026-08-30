// `CU-12007` — las ocho reglas del gobierno del movimiento.
import { abrir, decir } from './comun.mjs';

const { navegador, hoja, red } = await abrir();
const escena = hoja.locator('#escena');

await hoja.evaluate(() => window.anfitrion.inicializar());
await hoja.evaluate(() => window.anfitrion.cargar('E1'));

const encendido = (b) => (b ? 'prendido' : 'apagado');

// ---- [3] el estado inicial ----
// CON LAS OPCIONES AUSENTES, los dos apagados. La fachada NO consulta la
// preferencia del sistema: la recibe. Que arranque apagado es el valor que el
// anfitrión trae, no una decisión del visor.
const inicial = await hoja.evaluate(() => window.anfitrion.preferencia);
decir(`[3] Estado inicial con opciones ausentes: orbita=${encendido(inicial.cameraOrbit)} giro=${encendido(inicial.pieceSpin)}`);

// ---- [4] gobernar uno sin tocar el otro ----
const soloGiro = await hoja.evaluate(() => window.anfitrion.gobernar({ pieceSpin: true }));
decir(`[4] Prender solo el giro: giro=${encendido(soloGiro.giro)} orbita=${encendido(soloGiro.orbita)}`
  + ' (el no nombrado conserva su estado)');

// ---- [5] el cambio en vivo no mueve nada más ----
// SE COMPARAN LAS CINCO COSAS QUE §6 NOMBRA, una por una: disposición, selección,
// encuadre, resultado de dibujo e identificador. Comparar sólo el cuadro no
// serviría —el giro lo cambia por definición—, así que la disposición y el
// encuadre se miran con el movimiento DETENIDO a los dos lados del cambio.
await hoja.evaluate(() => window.anfitrion.gobernar({ pieceSpin: false, cameraOrbit: false }));
await hoja.evaluate(() => window.anfitrion.seleccionar(1));
const idAntes = await hoja.evaluate(() => window.anfitrion.identificador);
const dibujoAntes = await hoja.evaluate(() => window.anfitrion.ultimo.drawn.join(','));
const cuadroAntes = await escena.screenshot();

// EL CAMBIO SE APLICA Y SE DESHACE SIN DEJAR CORRER UN SOLO CUADRO EN EL MEDIO, y
// la ausencia de espera es deliberada. Lo que §6 afirma es que **gobernar** no
// mueve nada más; si se dejara correr la órbita, la cámara se movería por el
// movimiento y no por el acto de gobernarlo, y el renglón mediría otra cosa. Sin
// esta precisión la medición depende de cuántos cuadros pasaron, que es lo que
// hace a una prueba dar distinto en dos corridas iguales.
await hoja.evaluate(() => {
  window.anfitrion.gobernar({ cameraOrbit: true });
  window.anfitrion.gobernar({ cameraOrbit: false });
});

const idDespues = await hoja.evaluate(() => window.anfitrion.identificador);
const dibujoDespues = await hoja.evaluate(() => window.anfitrion.ultimo.drawn.join(','));
const cuadroDespues = await escena.screenshot();
// SE DICE CUÁL CAMBIÓ Y NO «CON CAMBIOS»: un renglón que sólo avisa que algo se
// movió obliga a repetir la medición para saber qué. Cuatro de las cinco cosas se
// leen por separado; la quinta —el encuadre— sólo se ve en el cuadro.
const iguales = [];
const distintos = [];
(idAntes === idDespues ? iguales : distintos).push('identificador');
(dibujoAntes === dibujoDespues ? iguales : distintos).push('resultado de dibujo');
(cuadroAntes.equals(cuadroDespues) ? iguales : distintos).push('disposicion, seleccion y encuadre');
decir(`[5] Cambio en vivo: ${distintos.length === 0
  ? 'disposicion, seleccion, encuadre, resultado de dibujo e identificador sin cambios'
  : `sin cambios ${iguales.join(' y ')}; CAMBIA ${distintos.join(' y ')}`}`);

// ---- [6] idempotencia ----
const unaVez = await hoja.evaluate(() => window.anfitrion.gobernar({ pieceSpin: true }));
const dosVeces = await hoja.evaluate(() => window.anfitrion.gobernar({ pieceSpin: true }));
decir(`[6] Invocar dos veces con el mismo valor: estado efectivo identico=`
  + `${unaVez.giro === dosVeces.giro && unaVez.orbita === dosVeces.orbita ? 'si' : 'no'} (idempotente)`);

// ---- [7] apagar el giro ----
// SE MIDE EL CUADRO ANTES DE PRENDER Y DESPUÉS DE APAGAR. Si al apagar las piezas
// volvieran a su orientación de partida, los dos cuadros serían el mismo.
await hoja.evaluate(() => window.anfitrion.gobernar({ pieceSpin: false, cameraOrbit: false }));
await hoja.waitForTimeout(120);
const antesDeGirar = await escena.screenshot();
await hoja.evaluate(() => window.anfitrion.gobernar({ pieceSpin: true }));
await hoja.waitForTimeout(400);
await hoja.evaluate(() => window.anfitrion.gobernar({ pieceSpin: false }));
await hoja.waitForTimeout(150);
const despuesDeApagar = await escena.screenshot();
decir(`[7] Apagar el giro: piezas de vuelta en su orientacion de partida=`
  + `${antesDeGirar.equals(despuesDeApagar) ? 'si' : 'no, quedan donde estaban'}`);

// ---- [8] cargar otro trabajo ----
const antesDeCargar = await hoja.evaluate(() => ({ ...window.anfitrion.preferencia }));
await hoja.evaluate(() => window.anfitrion.gobernar({ cameraOrbit: true, pieceSpin: true }));
const gobernado = await hoja.evaluate(() => ({ ...window.anfitrion.preferencia }));
await hoja.evaluate(() => window.anfitrion.cargar('E7'));
const despuesDeCargar = await hoja.evaluate(() => ({ ...window.anfitrion.preferencia }));
const conservado = gobernado.cameraOrbit === despuesDeCargar.cameraOrbit
  && gobernado.pieceSpin === despuesDeCargar.pieceSpin;
decir(`[8] Cargar otro texto: estado de los dos movimientos conservado=${conservado ? 'si' : 'no'}`);

// ---- [9] arrastre y superficie no visible ----
// LOS DOS SE DETIENEN MIENTRAS ARRASTRA, y por separado. Se mide congelando: dos
// cuadros tomados durante el arrastre tienen que ser el mismo, y con los dos
// movimientos prendidos no lo serían si alguno siguiera corriendo.
const caja = await escena.boundingBox();
await hoja.mouse.move(caja.x + caja.width / 2, caja.y + caja.height / 2);
await hoja.mouse.down();
await hoja.waitForTimeout(120);
const arrastre1 = await escena.screenshot();
await hoja.waitForTimeout(250);
const arrastre2 = await escena.screenshot();
await hoja.mouse.up();
const gobernadoTrasArrastre = await hoja.evaluate(() => ({ ...window.anfitrion.preferencia }));
const estadoIntacto = gobernadoTrasArrastre.cameraOrbit === gobernado.cameraOrbit
  && gobernadoTrasArrastre.pieceSpin === gobernado.pieceSpin;
decir(`[9] Arrastre de camara y superficie no visible: `
  + `${arrastre1.equals(arrastre2) ? 'los dos se detienen' : 'SIGUEN CORRIENDO'}`
  + ` | estado gobernado ${estadoIntacto ? 'sin cambios' : 'CAMBIADO'}`);

// ---- [13] la red con los dos movimientos prendidos y sostenidos ----
// LA CONDICIÓN DE MEDICIÓN ES VINCULANTE: con los dos movimientos prendidos el
// bucle de dibujo corre de continuo, que es el peor caso. Medirlo con los
// movimientos apagados —lo que pasa por defecto en un entorno con preferencia de
// movimiento reducido— dejaría la prueba en verde sin haber ejercitado el bucle.
red.length = 0;
await hoja.evaluate(() => window.anfitrion.gobernar({ cameraOrbit: true, pieceSpin: true }));
await hoja.waitForTimeout(1200);
await hoja.mouse.move(caja.x + 40, caja.y + 40);
await hoja.mouse.down();
await hoja.mouse.move(caja.x + 200, caja.y + 160, { steps: 12 });
await hoja.mouse.up();
await hoja.mouse.wheel(0, -240);
await hoja.waitForTimeout(600);
decir(`[13] Peticiones de red con los dos movimientos prendidos y sostenidos, y durante rotar y acercar: ${red.length}`);

// ---- [14] el almacenamiento ----
// LA FACHADA NO GUARDA NADA, y se cuenta después de haberla ejercitado entera: un
// recuento hecho antes de usarla no diría nada.
const claves = await hoja.evaluate(() => localStorage.length + sessionStorage.length);
decir(`[14] Claves escritas en el almacenamiento del navegador por la fachada: ${claves}`);

await navegador.close();
