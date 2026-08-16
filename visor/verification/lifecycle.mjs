// Medición de `PT-02` sobre el paquete construido, con un navegador de verdad.
//
// QUÉ MIDE, Y POR QUÉ ASÍ. La puerta dice: «sin degradación tras 10 navegaciones de ida y vuelta
// entre trabajos: `destruir` libera geometrías, materiales y el contexto WebGL». Cada escena toma
// un contexto gráfico, y el navegador permite pocos vivos —del orden de ocho a dieciséis—: si al
// salir no se libera, el navegador **descarta el más viejo sin avisar** y la escena se apaga sin
// error. Diez está elegido por encima de ese límite, que es lo que hace aparecer el defecto.
//
// LO QUE SE EJERCE ES EL MECANISMO Y NO LA RUTA: crear y liberar diez veces es lo que una
// navegación de ida y vuelta hace por dentro. Levantar el producto entero mediría lo mismo con más
// piezas en el medio.

import { chromium } from 'playwright';
import { fileURLToPath } from 'node:url';
import { dirname, join } from 'node:path';

const aqui = dirname(fileURLToPath(import.meta.url));
const pagina = 'file://' + join(aqui, 'lifecycle.html');

// Las tres piezas del escenario `E-1` del intake, ya reconstruidas: es lo que el visor recibe
// desde `ADR-08006`. Los valores son los del escenario y no se inventó ninguno.
const PIEZAS = [
  { position: 0, type: 'Cylinder', components: [
      { position: 0, role: 'Cap', type: 'Circle', declaredRadius: 3.0, declaredArea: 28.27 },
      { position: 1, role: 'Cap', type: 'Circle', declaredRadius: 3.0, declaredArea: 28.27 },
      { position: 2, role: 'Side', type: 'DevelopedRectangle', declaredLength: 3.0, declaredWidth: 18.85 }] },
  { position: 1, type: 'Cube', components: Array.from({ length: 6 }, (unused, i) => (
      { position: i, role: 'Face', type: 'Square', declaredLength: 3.0, declaredWidth: 3.0, declaredArea: 9.0 })) },
  { position: 2, type: 'Orthohedron', components: [
      { position: 0, role: 'Base', type: 'Rectangle', declaredLength: 7.0, declaredWidth: 7.0, declaredArea: 49.0 },
      { position: 1, role: 'Base', type: 'Rectangle', declaredLength: 7.0, declaredWidth: 7.0, declaredArea: 49.0 },
      { position: 2, role: 'Lateral', type: 'Rectangle', declaredLength: 21.0, declaredWidth: 7.0, declaredArea: 147.0 }] },
];

const VUELTAS = 10;

const navegador = await chromium.launch({ args: ['--use-gl=swiftshader', '--enable-unsafe-swiftshader'] });
const pestania = await navegador.newPage();

// TODA ADVERTENCIA DEL NAVEGADOR SE MIRA. La que delata el defecto —«Too many active WebGL
// contexts»— llega por acá y por ningún otro lado: no lanza excepción y no rompe nada visible.
const avisos = [];
pestania.on('console', (m) => { if (m.type() === 'warning' || m.type() === 'error') avisos.push(m.text()); });

await pestania.goto(pagina);
await pestania.waitForFunction(() => typeof window.GeometriaFactoryViewer !== 'undefined');

const medicion = await pestania.evaluate(async ({ piezas, vueltas }) => {
  const visor = window.GeometriaFactoryViewer;
  const elemento = document.getElementById('escena');
  const rondas = [];

  for (let vuelta = 1; vuelta <= vueltas; vuelta++) {
    const id = visor.initialize(elemento);
    const resultado = visor.loadPieces(id, piezas);

    // Un cuadro de dibujo, para que la escena llegue a renderizar de verdad.
    await new Promise((listo) => requestAnimationFrame(() => requestAnimationFrame(listo)));

    const lienzos = document.querySelectorAll('canvas').length;

    visor.destroy(id);

    rondas.push({
      vuelta,
      dibujadas: resultado.drawn.length,
      noDibujadas: resultado.undrawn.length,
      lienzosDurante: lienzos,
      lienzosDespues: document.querySelectorAll('canvas').length,
      instanciasVivas: visor.liveInstanceCount(),
    });
  }

  return rondas;
}, { piezas: PIEZAS, vueltas: VUELTAS });

await navegador.close();

// ---- El veredicto, control por control -------------------------------------------------------
const linea = (ok, texto) => console.log(`   ${ok ? 'OK  ' : 'FALLA'} · ${texto}`);
let fallas = 0;
const exigir = (condicion, texto) => { linea(condicion, texto); if (!condicion) fallas++; };

console.log(`\n== PT-02 · ${VUELTAS} ciclos de crear y liberar, con navegador real ==\n`);

const primera = medicion[0];
const ultima = medicion[medicion.length - 1];

exigir(primera.dibujadas === 3, `la primera vuelta dibuja las 3 piezas (dibujó ${primera.dibujadas})`);
exigir(ultima.dibujadas === 3, `la vuelta ${VUELTAS} dibuja las 3 piezas IGUAL que la primera (dibujó ${ultima.dibujadas})`);
exigir(medicion.every((r) => r.dibujadas === 3), 'las 10 vueltas dibujan las 3, sin una sola degradada');
exigir(medicion.every((r) => r.noDibujadas === 0), 'ninguna pieza queda sin dibujar en ninguna vuelta');
exigir(medicion.every((r) => r.lienzosDurante === 1), 'nunca hay más de UN lienzo vivo a la vez');
exigir(medicion.every((r) => r.lienzosDespues === 0), 'liberar deja CERO lienzos en la página');
exigir(medicion.every((r) => r.instanciasVivas === 0), 'liberar deja CERO instancias vivas');

const desbordes = avisos.filter((a) => /Too many active WebGL contexts|WebGL context was lost/i.test(a));
exigir(desbordes.length === 0, `el navegador no avisó desborde de contextos gráficos (${desbordes.length} avisos)`);

console.log(`\n== RESULTADO ==`);
if (fallas === 0) {
  console.log(`CONFORME · los ${8} controles pasan sobre ${VUELTAS} ciclos`);
} else {
  console.log(`NO CONFORME · ${fallas} control(es) fallan`);
  avisos.slice(0, 5).forEach((a) => console.log(`   aviso: ${a}`));
}
process.exit(fallas === 0 ? 0 : 1);
