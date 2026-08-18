// Medición de los criterios de la transición `g` → `h` que sólo un navegador puede resolver.
//
// QUÉ MIDE Y POR QUÉ ACÁ. De los siete criterios de `Roadmap-Producto.md` §5.2, cuatro se predican
// de lo que pasa DENTRO de la escena —qué se dibuja, dónde queda, qué pide la red y qué hace el
// movimiento automático— y ninguno se puede afirmar desde una prueba de integración: la prueba ve
// el marcado que se sirve, no la escena que el navegador construye. Los otros tres se miden donde
// corresponde y este banco no los toca: el 2 en `verify-viewer-lifecycle.sh`, el 6 en la batería de
// integración, y el 2 también allí en su forma de marcado.
//
// CÓMO MIDE, Y ES LA DECISIÓN QUE DEFINE ESTE ARCHIVO. Sólo por la FACHADA PÚBLICA y por lo que el
// navegador deja observar: **los píxeles del lienzo** y **los avisos del contrato**. No se le agrega
// al paquete ninguna función de medición. Las seis funciones las fijó el Product Owner, y un banco
// que necesita una séptima para poder medir **estaría midiendo otro producto**.
//
// LOS PÍXELES SON EL INSTRUMENTO, y no es un rodeo: la disposición de las piezas y el movimiento
// automático son propiedades de lo que se ve. Comparar dos capturas responde «¿quedó igual?» sin
// preguntarle al objeto que se está midiendo.
//
// SE MIDE SOBRE EL PAQUETE CONSTRUIDO y no sobre las fuentes, por el mismo motivo que `PT-02`: es lo
// que se sirve, y es donde la puerta puede fallar sin que nadie lo note.
//
// LOS DATOS SON LOS DEL ESCENARIO SEMILLA `E-1` del intake §20, ya reconstruidos como los recibe el
// visor por `ADR-08006`. Ningún valor se inventó.

import { chromium } from 'playwright';
import { fileURLToPath } from 'node:url';
import { dirname, join } from 'node:path';

const aqui = dirname(fileURLToPath(import.meta.url));
const pagina = 'file://' + join(aqui, 'stage-g.html');

const PIEZAS = [
  { position: 0, type: 'Cylinder', components: [
      { position: 0, role: 'Cap', type: 'Circle', declaredRadius: 3.0, declaredArea: 28.27 },
      { position: 1, role: 'Cap', type: 'Circle', declaredRadius: 3.0, declaredArea: 28.27 },
      { position: 2, role: 'Side', type: 'DevelopedRectangle', declaredLength: 3.0, declaredWidth: 18.85 }] },
  { position: 1, type: 'Cube', components: Array.from({ length: 6 }, (unused, i) => (
      { position: i, role: 'Face', type: 'Square', declaredLength: 3.0, declaredWidth: 3.0, declaredArea: 9.0 })) },
  // El ortoedro del escenario lleva `Bases` y `Laterales` —no `Side`—, y sus laterales miden 21 × 7:
  // es `Ortoedro(7,7,21)`, tal como el intake §20.E-1 lo transcribe y como `Scenarios.cs` lo ejerce.
  { position: 2, type: 'Orthohedron', components: [
      { position: 0, role: 'Base', type: 'Rectangle', declaredLength: 7.0, declaredWidth: 7.0, declaredArea: 49.0 },
      { position: 1, role: 'Base', type: 'Rectangle', declaredLength: 7.0, declaredWidth: 7.0, declaredArea: 49.0 },
      { position: 2, role: 'Lateral', type: 'Rectangle', declaredLength: 21.0, declaredWidth: 7.0, declaredArea: 147.0 },
      { position: 3, role: 'Lateral', type: 'Rectangle', declaredLength: 21.0, declaredWidth: 7.0, declaredArea: 147.0 },
      { position: 4, role: 'Lateral', type: 'Rectangle', declaredLength: 21.0, declaredWidth: 7.0, declaredArea: 147.0 },
      { position: 5, role: 'Lateral', type: 'Rectangle', declaredLength: 21.0, declaredWidth: 7.0, declaredArea: 147.0 }] },
];

const resultados = [];
const anotar = (criterio, pasa, detalle) => {
  resultados.push({ criterio, pasa });
  console.log(`${pasa ? 'PASA  ' : 'FALLA '} ${criterio}\n        ${detalle}`);
};

const navegador = await chromium.launch({ args: ['--use-gl=swiftshader', '--enable-unsafe-swiftshader'] });
const hoja = await (await navegador.newContext()).newPage();

// TODA PETICIÓN QUE SALGA DE LA PÁGINA SE REGISTRA, desde antes de cargar nada.
const peticiones = [];
hoja.on('request', (p) => peticiones.push(p.url()));

await hoja.goto(pagina);
await hoja.waitForFunction(() => window.GeometriaFactoryViewer !== undefined);
await hoja.evaluate((piezas) => { window.__piezas = piezas; }, PIEZAS);

const escena = hoja.locator('#escena');
const capturar = () => escena.screenshot();
const quieto = () => hoja.waitForTimeout(350);

// ---- Criterio 1 · las tres figuras del escenario semilla se dibujan, ortoedro incluido ----------

const dibujo = await hoja.evaluate(() => {
  const v = window.GeometriaFactoryViewer;
  const id = v.initialize(document.getElementById('escena'), {});
  const salida = v.loadPieces(id, window.__piezas);
  window.__id = id;
  return { dibujadas: [...salida.drawn], sinDibujar: salida.undrawn.map((u) => u.position) };
});

anotar('1 · las tres figuras del escenario semilla se dibujan, ortoedro incluido',
  dibujo.dibujadas.length === 3 && dibujo.sinDibujar.length === 0 && dibujo.dibujadas.includes(2),
  `dibujadas ${JSON.stringify(dibujo.dibujadas)} · sin dibujar ${JSON.stringify(dibujo.sinDibujar)} · el ortoedro es la posición 2`);

await quieto();
const primeraCorrida = await capturar();

// ---- Criterio 3 · procesar el mismo trabajo dos veces produce la misma disposición ---------------
//
// LAS PIEZAS SE PASAN DESORDENADAS LA SEGUNDA VEZ, y es lo que le da valor a la comparación: si la
// fila se armara con el orden de llegada en lugar de con la posición, las dos capturas diferirían.
//
// SE PREDICA DE LA POSICIÓN Y NO DE LA ORIENTACIÓN EN UN INSTANTE (intake §17.7 P.10): por eso se
// mide con el movimiento automático apagado, que es como arranca.

await hoja.evaluate(() => {
  const v = window.GeometriaFactoryViewer;
  v.destroy(window.__id);
  const id = v.initialize(document.getElementById('escena'), {});
  const p = window.__piezas;
  v.loadPieces(id, [p[2], p[0], p[1]]);
  window.__id = id;
});

await quieto();
const segundaCorrida = await capturar();

anotar('3 · la misma disposición dos veces, con las piezas pasadas desordenadas',
  primeraCorrida.equals(segundaCorrida),
  `dos capturas del lienzo, ${primeraCorrida.length} y ${segundaCorrida.length} bytes: ${primeraCorrida.equals(segundaCorrida) ? 'idénticas' : 'DIFIEREN'}`);

// ---- Criterio 5 · el árbol y la escena se sincronizan por índice, en las dos direcciones ---------
//
// DEL ÁRBOL A LA ESCENA: se pide resaltar una pieza y el lienzo tiene que cambiar. El visor «avisa y
// resalta» (contrato §5.5), de modo que el resalte es observable sin preguntarle nada.
//
// DE LA ESCENA AL ÁRBOL: se hace clic sobre el lienzo y el aviso `onPieceSelected` —que viaja en las
// opciones desde `ADR-08007`— tiene que llegar con un índice de pieza.

await hoja.evaluate(() => {
  const v = window.GeometriaFactoryViewer;
  v.destroy(window.__id);
  window.__avisos = [];
  const id = v.initialize(document.getElementById('escena'), {
    onPieceSelected: (p) => window.__avisos.push(p),
  });
  v.loadPieces(id, window.__piezas);
  window.__id = id;
});

await quieto();
const sinResaltar = await capturar();
await hoja.evaluate(() => window.GeometriaFactoryViewer.selectPiece(window.__id, 2));
await quieto();
const conResaltado = await capturar();
const arbolAEscena = !sinResaltar.equals(conResaltado);

// EL CLIC BARRE LA FILA EN LUGAR DE CLAVAR EL CENTRO, y es una corrección de la medición y no una
// concesión: las piezas se disponen con el tamaño de cada una, de modo que dónde cae cada figura
// depende del trabajo. Clavar el centro mide «hay una pieza justo en el medio», que no es el
// criterio. Barrer la fila mide el criterio: **hacer clic sobre una figura avisa con su índice**.
const caja = await escena.boundingBox();
const y = caja.y + caja.height / 2;

for (const fraccion of [0.5, 0.35, 0.65, 0.2, 0.8]) {
  await hoja.mouse.click(caja.x + caja.width * fraccion, y);
  await quieto();
  if ((await hoja.evaluate(() => window.__avisos)).length > 0) {
    break;
  }
}

const avisos = await hoja.evaluate(() => window.__avisos);
const dibujadas = dibujo.dibujadas;
const escenaAArbol = avisos.length > 0 && Number.isInteger(avisos[0]) && dibujadas.includes(avisos[0]);

anotar('5 · el árbol y la escena se sincronizan por índice, en las dos direcciones',
  arbolAEscena && escenaAArbol,
  `árbol → escena: pedir el resalte de la pieza 2 ${arbolAEscena ? 'cambió el lienzo' : 'NO cambió el lienzo'} · escena → árbol: el clic avisó ${JSON.stringify(avisos)}`);

// ---- Criterio 7 · los dos movimientos, gobernados por separado y detenidos al arrastrar ----------
//
// CADA UNO SE ENCIENDE SOLO Y SE MIDE SOLO. Un movimiento encendido cambia el lienzo entre dos
// capturas separadas en el tiempo; los dos apagados lo dejan quieto. Es la única forma de
// comprobar que se gobiernan **por separado** sin mirar adentro.

const mueve = async (opciones) => {
  await hoja.evaluate((o) => window.GeometriaFactoryViewer.setMotion(window.__id, o), opciones);
  await quieto();
  const antes = await capturar();
  await hoja.waitForTimeout(600);
  const despues = await capturar();
  return !antes.equals(despues);
};

const apagados = await mueve({ cameraOrbit: false, pieceSpin: false });
const soloOrbita = await mueve({ cameraOrbit: true, pieceSpin: false });
const soloGiro = await mueve({ cameraOrbit: false, pieceSpin: true });

// Y con la órbita encendida, arrastrar tiene que detenerla mientras dura el arrastre.
await hoja.evaluate(() => window.GeometriaFactoryViewer.setMotion(window.__id, { cameraOrbit: true, pieceSpin: false }));
await hoja.mouse.move(caja.x + caja.width / 2, caja.y + caja.height / 2);
await hoja.mouse.down();
await quieto();
const arrastrandoAntes = await capturar();
await hoja.waitForTimeout(600);
const arrastrandoDespues = await capturar();
await hoja.mouse.up();
const seDetiene = arrastrandoAntes.equals(arrastrandoDespues);

anotar('7 · los dos movimientos se gobiernan por separado y se detienen al arrastrar',
  !apagados && soloOrbita && soloGiro && seDetiene,
  `apagados: ${apagados ? 'SE MUEVE' : 'quieto'} · sólo órbita: ${soloOrbita ? 'se mueve' : 'QUIETO'} · sólo giro: ${soloGiro ? 'se mueve' : 'QUIETO'} · arrastrando: ${seDetiene ? 'detenido' : 'SIGUE MOVIÉNDOSE'}`);

// ---- Criterio 4 · ninguna petición originada por la visualización -------------------------------
//
// SE MIDE ÚLTIMO A PROPÓSITO: para entonces la escena se creó y se liberó tres veces, se cargaron
// piezas, se resaltó, se hizo clic y se ejercieron los dos movimientos con su arrastre. Si algo de
// eso pidiera red, ya habría pedido. La página y el paquete se cargan por `file://`: ésas son las
// únicas peticiones admitidas.

const externas = peticiones.filter((u) => !u.startsWith('file://'));
anotar('4 · durante la interacción no hay ni una sola petición originada por la visualización',
  externas.length === 0,
  `${peticiones.length} peticiones, todas \`file://\` · externas ${externas.length}${externas.length ? ': ' + externas.join(', ') : ''}`);

await navegador.close();

const fallan = resultados.filter((r) => !r.pasa);
console.log(`\n${resultados.length - fallan.length} de ${resultados.length} criterios con navegador PASAN.`);
if (fallan.length) {
  console.log('Fallan: ' + fallan.map((f) => f.criterio).join(' · '));
  process.exit(1);
}
