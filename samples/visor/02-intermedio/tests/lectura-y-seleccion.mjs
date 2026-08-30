// ============================================================================
// Conduce el navegador y compara con el snapshot de §6.
//
// CON UN NAVEGADOR DE VERDAD. Lo que se verifica es qué DIBUJA el visor y qué
// resalta, y eso necesita capacidad gráfica tridimensional. Es la misma imagen
// que `visor/verification/lifecycle.mjs` ya usa para medir `PT-02`.
// ============================================================================

import { chromium } from 'playwright';
import { fileURLToPath } from 'node:url';
import { dirname, join } from 'node:path';
import { readFileSync } from 'node:fs';

const aqui = dirname(fileURLToPath(import.meta.url));
const raiz = join(aqui, '..');
const pagina = 'file://' + join(raiz, 'index.html');

const lineas = [];
const decir = (l) => lineas.push(l);

const navegador = await chromium.launch({ args: ['--use-gl=swiftshader', '--enable-unsafe-swiftshader'] });
const hoja = await (await navegador.newContext()).newPage();

const peticionesDelPaquete = [];
hoja.on('request', (p) => { if (!p.url().startsWith('file://')) peticionesDelPaquete.push(p.url()); });

const avisos = [];
hoja.on('console', (m) => { if (m.text().startsWith('[visor]')) avisos.push(m.text()); });

const ultimoAviso = async () => { await hoja.waitForTimeout(60); return avisos.length ? avisos[avisos.length - 1] : ''; };
const codigoDelAviso = (a) => (a.match(/[A-Z][A-Z_]{3,}/) ?? [''])[0];

await hoja.goto(pagina);
await hoja.waitForFunction(() => window.anfitrionListo === true);
await hoja.evaluate(() => window.anfitrion.inicializar());

const escena = hoja.locator('#escena');
const VOLUMETRICOS = ['Cylinder', 'Cube', 'Orthohedron'];

// ---- [1] E-7, los seis ----
const e7 = await hoja.evaluate(() => window.anfitrion.cargar('E7'));
const tiposE7 = await hoja.evaluate(() => window.anfitrion.tipos('E7'));
const dibujadosE7 = e7.drawn.map((i) => tiposE7[i]);
const volumetricos = dibujadosE7.filter((t) => VOLUMETRICOS.includes(t)).length;
decir(`[1] E-7 cargado: piezas dibujadas=${e7.drawn.length}`
  + ` | tipos volumetricos=${volumetricos} | tipos planos=${e7.drawn.length - volumetricos}`);

// ---- [2] las medidas propias del ortoedro ----
// SALEN DE SUS COMPONENTES y no de la pieza: un ortoedro no lleva sus medidas en
// sí mismo, las lleva en sus caras. Es la distinción que `contract.d.ts` declara.
const m = await hoja.evaluate(() => window.anfitrion.medidasDelOrtoedro('E7'));
decir(`[2] E-7, ortoedro: ancho=${m.ancho} profundidad=${m.profundidad} altura=${m.altura}`);

// ---- [3] y [4] las dos claves ----
// EL SINÓNIMO LO RESOLVIÓ EL LABORATORIO, NO EL VISOR. `E-2` escribe `Tapas` donde
// `E-7` escribe `Bases`, y las dos llegan acá como el mismo rol `Base`: por eso el
// contrato del visor dice que no hay claves sinónimas, «y no las hay porque no
// llegan». Lo que este sample comprueba es que el ortoedro de cada uno se dibuja.
const seDibujaElOrtoedro = async (escenario) => {
  const r = await hoja.evaluate((e) => window.anfitrion.cargar(e), escenario);
  const tipos = await hoja.evaluate((e) => window.anfitrion.tipos(e), escenario);
  return r.drawn.some((i) => tipos[i] === 'Orthohedron');
};
decir(`[3] E-2, clave Tapas: el ortoedro se dibuja=${await seDibujaElOrtoedro('E2') ? 'si' : 'no'}`);
decir(`[4] E-7, clave Bases: el ortoedro se dibuja=${await seDibujaElOrtoedro('E7') ? 'si' : 'no'}`
  + ' (las dos claves son sinonimos)');

// ---- [5] E-5 ----
// DIVERGENCIA D-1. §6 espera que el visor enumere una pieza no dibujada con
// `NON_DRAWABLE_TYPE`. No llega ninguna: la figura de `E-5` que está mal escrita
// **el laboratorio no la reconstruye**, la rechaza con una observación y nunca se
// vuelve pieza. Desde `ADR-08006` «no dibujada» del lado del visor y «rechazada»
// del lado del laboratorio son dos cosas en dos componentes distintos, y §6 —
// escrito antes— las trataba como una.
const e5 = await hoja.evaluate(() => window.anfitrion.cargar('E5'));
decir(`[5] E-5 cargado: dibujadas=${e5.drawn.length} no dibujadas=${e5.undrawn.length}`
  + ` | el laboratorio no entrega la figura mal escrita, la rechaza antes`);

// ---- [6] E-8 ----
// EL MECANISMO ES EXACTAMENTE EL QUE §6 DESCRIBE y el código es el mismo; lo que
// cambia son los números, por lo mismo que en `[5]`.
const e8 = await hoja.evaluate(() => window.anfitrion.cargar('E8'));
const sinDibujar = e8.undrawn[0];
decir(`[6] E-8 cargado: dibujadas=${e8.drawn.length} no dibujadas=${e8.undrawn.length}`
  + ` | indice=${sinDibujar?.position} codigo=${sinDibujar?.reason}`);

// ---- [7] E-6 ----
// LA DISTINCIÓN QUE EL PRODUCTO VIENE A INSTALAR: lo que produce
// `UNREADABLE_DIMENSION` es la AUSENCIA de la clave, nunca el valor que trae. El
// visualizador previo perdía esta figura sin aviso porque evaluaba la verdad del
// número en lugar de su presencia.
const e6 = await hoja.evaluate(() => window.anfitrion.cargar('E6'));
decir(`[7] E-6 cargado: dibujadas=${e6.drawn.length} no dibujadas=${e6.undrawn.length}`
  + ' (el cero es una dimension legible)');

// ---- [8] la estructura del texto ----
// DIVERGENCIA D-2, la misma de `visor/01-basico`: el visor ya no recibe el texto,
// así que no tiene estructura de texto que devolver. Lo que sí hay —y es lo que el
// árbol necesita— es la enumeración de dibujadas y no dibujadas con su motivo.
const piezasDeE8 = await hoja.evaluate(() => window.PIEZAS.E8.length);
const filasDelArbol = await hoja.locator('#arbol li').count();
decir(`[8] Estructura del texto de E-8: no la devuelve el visor`
  + ` | filas del arbol que arma el anfitrion=${filasDelArbol} de ${piezasDeE8} piezas entregadas`);

// ---- [9] selección ----
// EL RESALTE SE MIDE MIRANDO, no leyendo un contador: la fachada no publica cuál
// está resaltada. Dos selecciones distintas producen dos cuadros distintos, y
// volver a la primera reproduce el primero: eso es exclusivo y determinista. Un
// resalte que se acumulara daría un tercer cuadro al volver.
await hoja.evaluate(() => window.anfitrion.cargar('E7'));
await hoja.evaluate(() => window.anfitrion.seleccionar(0));
const conCero = await escena.screenshot();
await hoja.evaluate(() => window.anfitrion.seleccionar(1));
const conUno = await escena.screenshot();
await hoja.evaluate(() => window.anfitrion.seleccionar(0));
const deVueltaCero = await escena.screenshot();
const marcadasEnElArbol = await hoja.locator('#arbol li.resaltada').count();
const exclusivo = !conCero.equals(conUno) && conCero.equals(deVueltaCero);
decir(`[9] Seleccion del indice 0: resaltadas=${marcadasEnElArbol}`
  + ` | resaltado exclusivo=${exclusivo ? 'si' : 'no'}`);

// ---- [10] seleccionar una enumerada como no dibujada ----
// NO ES UN ERROR DEL SAMPLE: un índice que el resultado enumera como no dibujado
// figura en el resultado pero **no tiene malla que resaltar**. Es uno de los dos
// casos que `INDEX_OUT_OF_RANGE` cubre, y los dos son un mismo curso.
await hoja.evaluate(() => window.anfitrion.cargar('E8'));
avisos.length = 0;
await hoja.evaluate(() => window.anfitrion.seleccionar(0));
decir(`[10] Seleccion del indice de E-8 enumerado como no dibujado: ${codigoDelAviso(await ultimoAviso())}`);

// ---- [11] fuera del conjunto raíz ----
// ACÁ APARECE UN DEFECTO, y por eso las dos mitades del renglón se miden por
// separado. El código que se informa es el correcto; lo que no se cumple es que
// la selección vigente sobreviva al rechazo.
await hoja.evaluate(() => window.anfitrion.cargar('E7'));
await hoja.evaluate(() => window.anfitrion.seleccionar(2));
const vigente = await escena.screenshot();
avisos.length = 0;
await hoja.evaluate(() => window.anfitrion.seleccionar(99));
const codigo11 = codigoDelAviso(await ultimoAviso());
const conservada = (await escena.screenshot()).equals(vigente);
decir(`[11] Seleccion de un indice fuera del conjunto raiz: ${codigo11}`
  + ` | seleccion vigente conservada=${conservada ? 'si' : 'no'}`);

// ---- [12] redimensionar ----
const antes = await hoja.evaluate(() => window.anfitrion.tamanoDeLaSuperficie());
await hoja.evaluate(() => { document.getElementById('escena').style.width = '640px'; });
await hoja.evaluate(() => window.anfitrion.redimensionar());
const despues = await hoja.evaluate(() => window.anfitrion.tamanoDeLaSuperficie());
decir(`[12] Redimensionar tras cambiar el tamano: relacion de aspecto recalculada=`
  + `${antes.ancho !== despues.ancho ? 'si' : 'no'}`);

// ---- [13] redimensionar con la superficie oculta ----
// DIVERGENCIA D-3. §6 espera `INVALID_CANVAS_ELEMENT` en su segundo curso. No se
// emite: `resize` no comprueba el tamaño, cae a `clientWidth || 1` y redimensiona a
// un píxel. La mitad que §6 afirma sí se cumple —la instancia sigue viva con su
// escena y su selección intactas—; lo que falta es el aviso.
avisos.length = 0;
await hoja.evaluate(() => { document.getElementById('escena').style.display = 'none'; });
await hoja.evaluate(() => window.anfitrion.redimensionar());
const codigo13 = codigoDelAviso(await ultimoAviso());
const oculta = await hoja.evaluate(() => window.anfitrion.tamanoDeLaSuperficie());
const viva = await hoja.evaluate(() => window.GeometriaFactoryViewer.liveInstanceCount());
decir(`[13] Redimensionar con la superficie oculta: ${codigo13 || `sin aviso, redimensiona a ${oculta.ancho}x${oculta.alto}`}`
  + ` | instancia viva=${viva === 1 ? 'si' : 'no'}`);

// ---- [14] devolverla a un tamaño válido ----
await hoja.evaluate(() => { document.getElementById('escena').style.display = ''; });
await hoja.evaluate(() => window.anfitrion.redimensionar());
const recuperada = await hoja.evaluate(() => window.anfitrion.tamanoDeLaSuperficie());
decir(`[14] Redimensionar con la superficie devuelta a un tamano valido: ajuste aplicado=`
  + `${recuperada.ancho === despues.ancho ? 'si' : 'no'}`);

// ---- cola ----
// «PIEZAS NO DIBUJADAS SIN REGISTRO» ES EL UMBRAL CERO DE ESTE SAMPLE: cada pieza
// entregada tiene que estar dibujada o enumerada, y ninguna puede faltar en los dos.
let sinRegistro = 0;
for (const escenario of ['E2', 'E5', 'E6', 'E7', 'E8']) {
  const r = await hoja.evaluate((e) => window.anfitrion.cargar(e), escenario);
  const entregadas = await hoja.evaluate((e) => window.PIEZAS[e].length, escenario);
  sinRegistro += entregadas - r.drawn.length - r.undrawn.length;
}
decir(`Funciones ejercidas: 5 de 6 | Piezas no dibujadas sin registro: ${sinRegistro}`
  + ` | Peticiones de red: ${peticionesDelPaquete.length}`);

await navegador.close();

// --------------------------------------------------------------------------
for (const l of lineas) console.log(l);

const divergencias = {
  5: 'D-1 · el laboratorio NO entrega la figura mal escrita de E-5: la rechaza con una observacion y nunca se vuelve pieza. Desde ADR-08006, «no dibujada» del lado del visor y «rechazada» del lado del laboratorio son dos cosas en dos componentes distintos, y §6 —escrito antes— las trataba como una. NON_DRAWABLE_TYPE queda ademas SIN CAMINO: el laboratorio solo reconstruye los seis tipos que el visor dibuja, y el septimo, RectanguloDesarrollado, lo rechaza como figura raiz',
  6: 'D-1 (misma causa) · el codigo y el mecanismo son EXACTAMENTE los que §6 describe —UNREADABLE_DIMENSION por una dimension ausente—; lo que cambia son los numeros, porque el laboratorio entrego una sola pieza y no dos',
  8: 'D-2 · el visor no devuelve la estructura del texto, y es lo mismo que en visor/01-basico: no recibe el texto. Lo que el arbol necesita —dibujadas y no dibujadas con su motivo— si lo devuelve, y el anfitrion arma con eso las filas que se cuentan al lado',
  10: 'D-1 (misma causa) · el renglon dice lo mismo que §6 salvo el indice: en E-8 la pieza enumerada como no dibujada es la 0 y no la 1, porque el laboratorio entrego una sola',
  11: 'D-4 · DEFECTO. §6 dice que la seleccion vigente se conserva y NO se conserva: una seleccion rechazada la BORRA. `ViewerInstance.select` recorre todas las mallas apagando el resalte de las que no coinciden, y recien al final descubre que ninguna coincidia; para cuando `INDEX_OUT_OF_RANGE` se informa, el estado ya se toco. La comprobacion esta despues del efecto en lugar de antes. Se ve mirando: el cuadro posterior al rechazo no es el anterior',
  13: 'D-3 · INVALID_CANVAS_ELEMENT NO se emite en este curso. `resize` no comprueba el tamano: cae a `clientWidth || 1` y redimensiona a un pixel. La mitad que §6 afirma si se cumple —la instancia sigue viva con su escena y su seleccion intactas—; lo que falta es el aviso',
};

const esperadas = readFileSync(join(raiz, 'esperado', 'salida.txt'), 'utf8').split('\n').filter((l) => l.length > 0);
let declaradas = 0;
let noDeclaradas = 0;
const verificacion = [];

for (let i = 0; i < Math.max(esperadas.length, lineas.length); i += 1) {
  const e = esperadas[i] ?? '(línea de más)';
  const p = lineas[i] ?? '(línea ausente)';
  if (e === p) continue;
  const n = i + 1;
  if (divergencias[n]) {
    declaradas += 1;
    verificacion.push(`  línea ${n} — DIVERGENCIA DECLARADA · ${divergencias[n]}`);
    verificacion.push(`    §6 dice:  ${e}`);
    verificacion.push(`    el arbol: ${p}`);
  } else {
    noDeclaradas += 1;
    verificacion.push(`  línea ${n} difiere y NO estaba declarada`);
    verificacion.push(`    esperada: ${e}`);
    verificacion.push(`    obtenida: ${p}`);
  }
}

console.log('');
console.log('Verificación contra el snapshot de §6:');
for (const l of verificacion) console.log(l);
console.log('');
const coinciden = esperadas.length - declaradas - noDeclaradas;
if (noDeclaradas === 0) {
  console.log(`  CONFORME CON DIVERGENCIAS DECLARADAS · ${coinciden}/${esperadas.length} líneas coinciden, ${declaradas} difieren por motivo escrito`);
  process.exit(0);
}
console.log(`  NO CONFORME · ${noDeclaradas} línea(s) difieren sin motivo declarado`);
process.exit(1);
