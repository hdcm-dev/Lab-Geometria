// Lo que los tres recorridos comparten: abrir el navegador, escuchar la red y la
// consola, y acumular renglones en un archivo que `comparar.mjs` lee al final.
//
// LOS TRES ESCRIBEN Y EL CUARTO COMPARA, y no cada uno el suyo: §6 es UN snapshot,
// y comparar por partes dejaría sin verificar el orden entre ellas.
import { chromium } from 'playwright';
import { fileURLToPath } from 'node:url';
import { dirname, join } from 'node:path';
import { appendFileSync, writeFileSync } from 'node:fs';

export const aqui = dirname(fileURLToPath(import.meta.url));
export const raiz = join(aqui, '..');
export const acumulado = join(raiz, 'esperado', '.producido.txt');
export const pagina = 'file://' + join(raiz, 'index.html');

export function reiniciarAcumulado() { writeFileSync(acumulado, ''); }
export function decir(linea) { appendFileSync(acumulado, linea + '\n'); console.log(linea); }

export async function abrir() {
  const navegador = await chromium.launch({
    args: ['--use-gl=swiftshader', '--enable-unsafe-swiftshader'],
  });
  const hoja = await (await navegador.newContext()).newPage();

  const red = [];
  hoja.on('request', (p) => { if (!p.url().startsWith('file://')) red.push(p.url()); });
  const avisos = [];
  hoja.on('console', (m) => { if (m.text().startsWith('[visor]')) avisos.push(m.text()); });

  await hoja.goto(pagina);
  await hoja.waitForFunction(() => window.anfitrionListo === true);
  return { navegador, hoja, red, avisos };
}
