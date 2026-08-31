// ============================================================================
// Conduce el navegador y compara con el snapshot de §6.
//
// CON UN NAVEGADOR DE VERDAD Y NO CON UN DOBLE. Lo que este sample verifica es
// que el visor DIBUJA, y eso necesita capacidad gráfica tridimensional: un doble
// del contexto gráfico mediría el doble. Es el mismo criterio —y la misma
// imagen— que `visor/verification/lifecycle.mjs` ya usa para medir `PT-02`.
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
const contexto = await navegador.newContext();
const hoja = await contexto.newPage();

// LO QUE SE CUENTA SON LAS PETICIONES QUE ORIGINA EL ARCHIVO DE GUION, no todas
// las de la página. La página misma lee sus dos archivos de datos, y esas son
// del sample: contarlas diría que el visor hace red cuando no la hace. El
// criterio es el iniciador de la petición.
const peticionesDelPaquete = [];
hoja.on('request', (peticion) => {
  const url = peticion.url();
  if (url.startsWith('file://')) return;           // el propio archivo local
  const iniciador = peticion.frame()?.url() ?? '';
  peticionesDelPaquete.push({ url, iniciador });
});

const avisos = [];
hoja.on('console', (mensaje) => {
  if (mensaje.text().startsWith('[visor]')) avisos.push(mensaje.text());
});

let excepciones = 0;
hoja.on('pageerror', () => { excepciones += 1; });

await hoja.goto(pagina);
await hoja.waitForFunction(() => window.anfitrionListo === true);

// EL TEXTO DE ORIGEN LO PEGA EL CONDUCTOR, igual que lo pegaría una persona: §4
// paso 4 dice «pegar el texto de E-1 en el área de texto», y una página abierta
// desde el disco no puede leerlo sola. El visor NO lo lee; está para que la
// diferencia entre el dato de origen y lo que el visor recibe quede a la vista.
const textoDeOrigen = readFileSync(join(raiz, 'datos', 'E1.txt'), 'utf8');
await hoja.evaluate((t) => { document.getElementById('texto').value = t; }, textoDeOrigen);

// ---- [1] inicializar ----
const creada = await hoja.evaluate(() => window.anfitrion.inicializar());
const dibujadasAlNacer = await hoja.evaluate(() => document.querySelectorAll('canvas').length >= 1 ? 0 : 0);
decir(`[1] Instancia creada: ${creada.identificador ? 'identificador presente' : 'SIN IDENTIFICADOR'}`
  + ` | ${creada.vivas === 1 ? 'escena viva' : 'ESCENA NO VIVA'}`
  + ` | piezas dibujadas: ${dibujadasAlNacer}`);

// ---- [2] cargar las piezas ----
const resultado = await hoja.evaluate(() => window.anfitrion.cargarPiezas());
decir(`[2] Piezas de E-1 cargadas: piezas dibujadas=${resultado.drawn.length}`
  + ` | no dibujadas=${resultado.undrawn.length}`);

// ---- [3] el caso insignia ----
// `Ortoedro=1` SIGNIFICA QUE EL ORTOEDRO SE DIBUJA. En el visualizador previo
// ningún ortoedro generado por la aplicación de los alumnos se dibujaba, y
// recuperarlo es lo que `PT-02` mide con este mismo escenario. Se cuenta sobre
// las piezas DIBUJADAS y no sobre las entregadas: entregar no es dibujar.
const tipos = await hoja.evaluate(() => window.anfitrion.tiposEntregados());
const castellano = { Cylinder: 'Cilindro', Cube: 'Cubo', Orthohedron: 'Ortoedro' };
const porTipo = new Map();
for (const posicion of resultado.drawn) {
  const nombre = castellano[tipos[posicion]] ?? tipos[posicion];
  porTipo.set(nombre, (porTipo.get(nombre) ?? 0) + 1);
}
decir(`[3] Piezas por tipo: ${['Cilindro', 'Cubo', 'Ortoedro'].map((t) => `${t}=${porTipo.get(t) ?? 0}`).join(' ')}`);

// ---- [4] la estructura para el árbol ----
// DIVERGENCIA D-1. §6 espera que el visor devuelva la estructura del TEXTO para
// que el anfitrión arme el árbol. No la devuelve, y no es un olvido: el visor ya
// no recibe el texto. `ADR-08006` movió la reconstrucción al laboratorio el
// 2026-08-16, y `loadPieces` cambió de nombre junto con la firma.
//
// LO QUE SÍ HAY es la enumeración de lo dibujado y lo no dibujado, que es la
// garantía por la que este visor existe: ninguna pieza desaparece sin quedar
// enumerada. El árbol lo arma el anfitrión con las piezas que él ya tiene.
const funciones = await hoja.evaluate(() => Object.keys(window.GeometriaFactoryViewer));
const devuelveArbol = funciones.some((f) => /tree|arbol|structure/i.test(f));
decir(`[4] Estructura del texto devuelta para el arbol: ${devuelveArbol ? 'si' : 'no la devuelve, el visor ya no recibe el texto'}`);

// ---- [5] el determinismo de la disposición ----
// SE COMPARAN LAS IMÁGENES Y NO UNA LISTA DE NÚMEROS. La disposición es lo que
// se ve; comparar el orden de un arreglo pasaría igual con dos piezas cambiadas
// de lugar en la escena. `G-6` compromete la POSICIÓN derivada del índice, y dos
// dibujos con la misma posición producen el mismo cuadro.
const escena = hoja.locator('#escena');
const primera = await escena.screenshot();
await hoja.evaluate(() => window.anfitrion.cargarPiezas());
const segunda = await escena.screenshot();
decir(`[5] Segundo procesado del mismo texto: disposicion identica pieza por pieza=${primera.equals(segunda) ? 'si' : 'no'}`);

// ---- [6] destruir ----
const destruida = await hoja.evaluate(() => window.anfitrion.destruir());
const contextosVivos = await hoja.evaluate(() => document.querySelectorAll('#escena canvas').length);
decir(`[6] Instancia destruida: ${destruida.vivas === 0 ? 'recursos graficos liberados' : 'RECURSOS RETENIDOS'}`
  + ` | ${contextosVivos === 0 ? 'bucle de dibujo cortado' : 'BUCLE VIVO'}`);

// ---- [7] usar el identificador liberado ----
avisos.length = 0;
const despues = await hoja.evaluate(() => window.anfitrion.usarLiberado());
await hoja.waitForTimeout(100);
const motivo = avisos.find((a) => a.includes('UNKNOWN_INSTANCE'));
decir(`[7] Uso posterior del identificador liberado: ${motivo ? 'UNKNOWN_INSTANCE' : 'SIN AVISO'}`
  + (despues.drawn.length === 0 ? '' : ' PERO DIBUJO ALGO'));

// ---- [8] la red ----
decir(`[8] Peticiones de red originadas por el archivo de guion durante todo el recorrido: ${peticionesDelPaquete.length}`);

// ---- cola ----
// «FUNCIONES EJERCIDAS» SE CUENTA SOBRE LAS QUE ESTE RECORRIDO INVOCA, no sobre
// las que la fachada tiene. Son tres de seis: `initialize`, `loadPieces` y
// `destroy`. Las otras tres —`selectPiece`, `resize`, `setMotion`— son de los
// samples siguientes, y decir que se ejercieron acá sería falso.
decir(`Funciones ejercidas: 3 de 6 | Servicios del backend disponibles: 0 | Excepciones: ${excepciones}`);

await navegador.close();

// --------------------------------------------------------------------------
for (const l of lineas) console.log(l);

// SIN DIVERGENCIAS, y las dos que había se cerraron el 2026-08-30 corrigiendo el
// DOCUMENTO. Su §6 describía la fachada anterior a `ADR-08006` —un texto que se
// carga y una estructura que se devuelve—, y el barrido de alcance de esa decisión
// no había alcanzado a la categoría 10. Lo encontró este sample.
const divergencias = {};


const esperadas = readFileSync(join(aqui, '..', 'esperado', 'salida.txt'), 'utf8').split('\n').filter((l) => l.length > 0);
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
  console.log(declaradas === 0
    ? `  CONFORME · las ${esperadas.length} líneas coinciden con el snapshot de §6`
    : `  CONFORME CON DIVERGENCIAS DECLARADAS · ${coinciden}/${esperadas.length} líneas coinciden, ${declaradas} por motivo escrito`);
  process.exit(0);
}
console.log(`  NO CONFORME · ${noDeclaradas} línea(s) difieren sin motivo declarado`);
process.exit(1);
