// ============================================================================
// verificar-acuse-de-la-escena.mjs — LA PANTALLA NO AFIRMA LO QUE NO DIBUJO.
//
// POR QUE EXISTE. El peritaje del 2026-09-02 reprodujo que, en una máquina sin
// capacidad 3D, el recuadro de la escena quedaba liso y la página decía igual
// «Se dibujaron las 3 figuras del trabajo». Texto idéntico con y sin 3D.
//
// La afirmación la escribe el SERVIDOR, que no puede saber si el navegador
// dibujó algo. Ahora el servidor sirve un hecho que siempre es cierto —cuántas
// figuras TIENE el trabajo— y el guion lo mueve según lo que ocurrió.
//
// SE CORRE EN LOS DOS ESTADOS, y esa es la gracia: un acuse que sólo se probó
// con 3D disponible no prueba nada, porque el defecto vivía en el otro lado.
// ============================================================================
import { chromium } from 'playwright';
const [base, correo, clave, wid] = process.argv.slice(2);

const mirar = async (con3d) => {
  const spki = process.env.GF_SPKI || '';
  const args = spki ? ['--ignore-certificate-errors-spki-list=' + spki.trim()] : [];
  if (!con3d) { args.push('--disable-3d-apis', '--disable-webgl'); }
  const nav = await chromium.launch({ args });
  const p = await (await nav.newContext({ ignoreHTTPSErrors: true })).newPage();
  await p.goto(`${base}/ingreso`, { waitUntil: 'load' });
  await p.fill('#signin-email', correo); await p.fill('#signin-password', clave);
  await Promise.all([p.waitForNavigation({ waitUntil: 'load' }), p.click('button[type="submit"]')]);
  await p.goto(`${base}/trabajos/${wid}`, { waitUntil: 'load' });
  await p.waitForTimeout(9000);
  const canvas = await p.locator('.gf-scene canvas').count();
  const acuse = await p.locator('[data-gf-escena-acuse]').count()
    ? (await p.locator('[data-gf-escena-acuse]').innerText()).replace(/\s+/g, ' ').trim()
    : '(sin acuse)';
  const clases = await p.locator('[data-gf-escena-acuse]').getAttribute('class').catch(() => '');
  await nav.close();
  return { canvas, acuse, clases };
};

const con = await mirar(true);
const sin = await mirar(false);
console.log(`   CON 3D  canvas=${con.canvas}  clases="${con.clases}"`);
console.log(`           acuse: ${con.acuse}`);
console.log(`   SIN 3D  canvas=${sin.canvas}  clases="${sin.clases}"`);
console.log(`           acuse: ${sin.acuse}`);
const ok = con.canvas > 0 && /Se dibujaron/.test(con.acuse)
        && sin.canvas === 0 && /no pudo dibujar/.test(sin.acuse) && /warning/.test(sin.clases || '');
console.log(ok ? '   CONFORME · el acuse dice la verdad en los dos estados'
               : '   NO CONFORME · el acuse no distingue los dos estados');
process.exit(ok ? 0 : 1);
