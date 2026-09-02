// ============================================================================
// verificar-escena-al-cambiar-de-tamano.mjs — LA ESCENA ACOMPAÑA EL RECUADRO.
//
// POR QUE EXISTE. `resize` estaba exportado por el visor y NADIE LO LLAMABA. Al
// cambiar el tamaño de la ventana —o al girar el teléfono, que es el gesto más
// común sobre un dibujo— la escena quedaba con la medida vieja: recortada y en un
// rincón. El peritaje del 2026-09-02 lo midió en un 26 % del recuadro.
//
// QUE COMPRUEBA: que el lienzo de la escena vuelva a ocupar su recuadro después de
// cambiar el tamaño de la ventana. Compara ANCHOS REALES, no marcado.
// ============================================================================
import { chromium } from 'playwright';
const [base, correo, clave, wid] = process.argv.slice(2);
const spki = process.env.GF_SPKI || '';
const nav = await chromium.launch({ args: spki ? ['--ignore-certificate-errors-spki-list=' + spki.trim()] : [] });
const p = await (await nav.newContext({ ignoreHTTPSErrors: true, viewport: { width: 1280, height: 900 } })).newPage();
await p.goto(`${base}/ingreso`, { waitUntil: 'load' });
await p.fill('#signin-email', correo); await p.fill('#signin-password', clave);
await Promise.all([p.waitForNavigation({ waitUntil: 'load' }), p.click('button[type="submit"]')]);
await p.goto(`${base}/trabajos/${wid}`, { waitUntil: 'load' });
await p.waitForTimeout(9000);

const medir = async () => await p.evaluate(() => {
  const c = document.querySelector('.gf-scene canvas');
  if (!c) return null;
  const caja = c.parentElement.getBoundingClientRect();
  return { lienzo: Math.round(c.getBoundingClientRect().width), recuadro: Math.round(caja.width) };
});

const antes = await medir();
if (!antes) { console.log('   NO SE PUEDE VERIFICAR · no hay lienzo'); await nav.close(); process.exit(2); }
console.log(`   escritorio 1280 px → lienzo ${antes.lienzo} px de un recuadro de ${antes.recuadro} px`);

// EL CAMBIO ES AL ANCHO DE UN TELEFONO, y no a un valor intermedio. Con 700 px el desvío sin
// observador daba 4.5 % y una tolerancia razonable lo dejaba pasar: el caso era demasiado
// suave para separar «acompañó» de «no acompañó». El gesto que hay que cubrir es el alumno que
// GIRA EL TELEFONO, y ahí la diferencia es enorme o no hay defecto.
await p.setViewportSize({ width: 420, height: 900 });
await p.waitForTimeout(2500);
const despues = await medir();
console.log(`   telefono    420 px → lienzo ${despues.lienzo} px de un recuadro de ${despues.recuadro} px`);

// EL CRITERIO MIRA LAS DOS DIRECCIONES, y esa corrección salió de probar la prueba fallando.
//
// La primera versión exigía «que ocupe más del 90 % del recuadro», y con el observador quitado
// DIO CONFORME IGUAL: al achicar la ventana el lienzo se quedaba con la medida vieja y ocupaba
// el 104 % —DESBORDABA—, que es el mismo defecto para el otro lado. Un criterio que sólo
// atrapa «quedó chico» deja pasar «quedó grande», y las dos cosas son lo mismo: el lienzo no
// acompañó.
//
// Lo que se exige es que COINCIDA con su recuadro. DOS por ciento de tolerancia, y el número
// también salió de medir: con observador el desvío es de 0.3 % —bordes y redondeos— y sin él,
// al ancho de un teléfono, es de decenas de por ciento. Cinco por ciento, que fue el primer
// intento, DEJABA PASAR EL DEFECTO con un cambio de tamaño suave.
const desvio = Math.abs(despues.lienzo - despues.recuadro) / despues.recuadro;
console.log(`   desvío del lienzo respecto de su recuadro: ${(desvio * 100).toFixed(1)} %`);
const ok = desvio < 0.02;
console.log(ok ? '   CONFORME · la escena acompañó el tamaño'
               : '   NO CONFORME · el lienzo quedó con la medida vieja');
await nav.close();
process.exit(ok ? 0 : 1);
