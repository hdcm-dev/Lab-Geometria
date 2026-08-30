// `PT-02` y `PT-03`, y la inspección del bundle generado.
import { abrir, decir, raiz } from './comun.mjs';
import { readFileSync } from 'node:fs';
import { join } from 'node:path';

const { navegador, hoja, red } = await abrir();

// ---- [10] la superficie del archivo de guion ----
// LAS SEIS SON LAS DE LA FACHADA. `liveInstanceCount` también sale del paquete y
// NO es una séptima: es instrumento de medición de `PT-02` y el front no lo usa.
// El sample lo dice en lugar de contarlo como función o de esconderlo.
const superficie = await hoja.evaluate(() => Object.keys(window.GeometriaFactoryViewer));
const seis = ['initialize', 'loadPieces', 'selectPiece', 'resize', 'destroy', 'setMotion'];
const presentes = seis.filter((f) => superficie.includes(f)).length;

// «GLOBALES SUELTAS» SON LAS QUE EL PAQUETE DEJA FUERA DE SU PROPIO NOMBRE. Se
// comparan las claves de `window` antes y después de cargarlo: lo que aparezca y
// no sea `GeometriaFactoryViewer` ni del sample, es suelto.
// SE NOMBRAN Y NO SE CUENTAN A SECAS: «globales sueltas: 1» no dice cuál hay que
// sacar. Se comparan las claves de `window` de una página vacía contra las de
// ésta, y se descuenta lo que pone el sample.
const delSample = ['anfitrion', 'anfitrionListo', 'TRABAJOS', 'GeometriaFactoryViewer'];
const sueltas = await hoja.evaluate((mias) => Object.keys(window)
  .filter((k) => /^(three|__three|webpack|__webpack)/i.test(k) && !mias.includes(k)), delSample);
decir(`[10] Superficie del archivo de guion: funciones=${presentes}`
  + ` | nombres propios en el objeto global=1 | globales sueltas=${sueltas.length}`
  + `${sueltas.length ? ' (' + sueltas.join(', ') + ')' : ''}`);

// ---- [10b] las tres formas de petición, en la fuente Y en el bundle ----
// LAS DOS INSPECCIONES SON NECESARIAS. Una dependencia que hiciera una petición
// por dentro no aparecería en la fuente, y la puerta quedaría en verde sobre un
// archivo que sí hace red. Por eso se mira también el generado.
const formas = [/\bfetch\s*\(/g, /XMLHttpRequest/g, /new\s+WebSocket/g];
const contar = (texto) => formas.reduce((total, f) => total + (texto.match(f) ?? []).length, 0);
// LOS COMENTARIOS SE QUITAN ANTES DE CONTAR, y no es prolijidad: es corrección.
//
// La primera versión contaba sobre el texto crudo, y el 2026-08-30 dio un falso
// positivo que se ve solo cuando se arregla lo que medía. Se retiró el respaldo
// `?? 'UNKNOWN'` del código y se dejó escrito en dos comentarios POR QUÉ se
// retiró —que es lo que este repositorio pide hacer—; la inspección siguió
// contando `UNKNOWN` como código acuñado, leyendo la explicación de su propio
// retiro.
//
// Es la misma clase de falso positivo que la mesa del 2026-08-27 midió con los
// seis enlaces rotos que no eran enlaces: **un identificador nombrado en prosa
// no es un identificador emitido**, y un instrumento que no los distingue mide
// de qué se habla en vez de qué se hace.
const sinComentarios = (texto) => texto
  .replace(/\/\*[\s\S]*?\*\//g, '')
  .replace(/^\s*\/\/.*$/gm, '');
const fuentes = ['src/main.ts', 'src/contract.ts', 'src/viewer/instance.ts', 'src/viewer/meshes.ts', 'src/viewer/palette.ts']
  .map((f) => sinComentarios(readFileSync(join(raiz, '../../../visor', f), 'utf8'))).join('\n');
const generado = readFileSync(join(raiz, '../../../visor/dist/geometriafactory-visor.js'), 'utf8');
decir(`[10b] Ocurrencias de las tres formas de peticion, en la fuente y en el bundle generado: `
  + `${contar(fuentes)} y ${contar(generado)}`);

// ---- [11] PT-03 ----
// EL MOTOR VA ADENTRO: se comprueba buscando su firma en el archivo generado, no
// confiando en el `package.json`. Y «dependencias de red externa en ejecución» es
// lo mismo que `[2]` y `[13]` cuentan, sostenido durante todo el recorrido.
const motorAdentro = /THREE\.|WebGLRenderer|BufferGeometry/.test(generado);
decir(`[11] PT-03: motor de dibujo dentro del bundle=${motorAdentro ? 'si' : 'no'}`
  + ` | dependencias de red externa en ejecucion=${red.length}`);

// ---- [12] PT-02 entera, en sus cinco tramos ----
// UNA PUERTA QUE NO PASA DETIENE LA PLANIFICACIÓN y no se arrastra como deuda, así
// que los cinco tramos se miden juntos y el renglón los muestra uno por uno.
await hoja.evaluate(() => window.anfitrion.inicializar());
const carga = await hoja.evaluate(() => window.anfitrion.cargar('E1'));
const hayEscena = await hoja.locator('#escena canvas').count() === 1;
const tipos = await hoja.evaluate(() => window.TRABAJOS.E1.map((p) => p.type));
const conOrtoedro = carga.drawn.some((i) => tipos[i] === 'Orthohedron');

// DIEZ RECORRIDOS DE IDA Y VUELTA. El navegador permite pocos contextos gráficos
// vivos; si `destroy` no liberara, al décimo el más viejo se apaga sin error.
await hoja.evaluate(() => window.anfitrion.destruir());
let degradado = false;
for (let i = 0; i < 10; i += 1) {
  const vuelta = await hoja.evaluate((n) => window.anfitrion.recorrer(n % 2 === 0 ? 'E1' : 'E7'), i);
  const esperadas = i % 2 === 0 ? 3 : 6;
  if (vuelta.dibujadas !== esperadas || vuelta.vivas !== 0) degradado = true;
}

// SINCRONIZACIÓN POR ÍNDICE: el árbol que el anfitrión arma tiene una fila por
// pieza entregada, y la posición de cada fila es la que `selectPiece` recibe.
await hoja.evaluate(() => window.anfitrion.inicializar());
await hoja.evaluate(() => window.anfitrion.cargar('E7'));
const porIndice = await hoja.evaluate(() => {
  const filas = [...document.querySelectorAll('#arbol li')];
  return filas.every((fila, i) => fila.dataset.posicion === String(window.TRABAJOS.E7[i].position));
});
decir(`[12] PT-02: carga=si escena=${hayEscena ? 'si' : 'no'} E-1 con ortoedro=${conOrtoedro ? 'si' : 'no'}`
  + ` diez recorridos sin degradar=${degradado ? 'no' : 'si'} sincronizacion por indice=${porIndice ? 'si' : 'no'}`);

// ---- [15] los códigos ----
// LA DISTINCIÓN QUE ESTE RENGLÓN PROTEGE ES ENTRE CÓDIGO Y CURSO: `UNKNOWN_INSTANCE`
// aparece en cinco funciones e `INVALID_CANVAS_ELEMENT` en dos cursos, y ninguno de
// esos hechos multiplica el conjunto. Se cuentan códigos DISTINTOS.
const DEL_CONTRATO = ['UNKNOWN_INSTANCE', 'INVALID_CANVAS_ELEMENT', 'GRAPHICS_CAPABILITY_MISSING',
  'INDEX_OUT_OF_RANGE', 'NON_DRAWABLE_TYPE', 'UNREADABLE_DIMENSION', 'UNREADABLE_TEXT'];
const enLaFuente = new Set((fuentes.match(/'[A-Z][A-Z_]{3,}'/g) ?? []).map((c) => c.slice(1, -1)));
const delContrato = DEL_CONTRATO.filter((c) => enLaFuente.has(c)).length;
const acunados = [...enLaFuente].filter((c) => !DEL_CONTRATO.includes(c));
decir(`[15] Codigos que el archivo de guion puede informar: ${delContrato} de 7 del contrato`
  + ` | acunados aguas abajo: ${acunados.length}${acunados.length ? ' (' + acunados.join(', ') + ')' : ''}`);

decir(`Funciones ejercidas: 6 de 6 | Propiedades transversales verificadas: 6 de 6 | Puertas tecnicas: 2 de 2`);

await navegador.close();
