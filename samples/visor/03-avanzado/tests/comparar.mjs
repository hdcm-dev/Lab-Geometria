// Compara lo que los tres recorridos produjeron contra el snapshot de §6.
import { readFileSync } from 'node:fs';
import { join, dirname } from 'node:path';
import { fileURLToPath } from 'node:url';

const raiz = join(dirname(fileURLToPath(import.meta.url)), '..');
const producidas = readFileSync(join(raiz, 'esperado', '.producido.txt'), 'utf8').split('\n').filter((l) => l);
const esperadas = readFileSync(join(raiz, 'esperado', 'salida.txt'), 'utf8').split('\n').filter((l) => l);

// EL ORDEN EN QUE SE PUEDE MEDIR NO ES EL ORDEN EN QUE §6 SE LEE, y los tres
// recorridos de §5 lo hacen inevitable: `[13]` y `[14]` sólo se pueden medir con
// el movimiento gobernado —son del segundo recorrido— y §6 los lee después de las
// puertas técnicas, que son del tercero.
//
// SE REORDENA LA EMISIÓN Y NO LA MEDICIÓN: cada renglón vale lo que valía cuando
// salió, y se lo ubica por su etiqueta. Un renglón producido que no tenga etiqueta
// esperada queda al final y se ve como línea de más.
const etiqueta = (l) => (l.match(/^\[[^\]]+\]/) ?? ['(cola)'])[0];
const porEtiqueta = new Map(producidas.map((l) => [etiqueta(l), l]));
const lineas = esperadas.map((e) => porEtiqueta.get(etiqueta(e)) ?? '(línea ausente)');
for (const [clave, valor] of porEtiqueta) {
  if (!esperadas.some((e) => etiqueta(e) === clave)) lineas.push(valor);
}

// LAS DIVERGENCIAS SE ANOTAN POR ETIQUETA Y NO POR NÚMERO DE RENGLÓN. Se probó con
// número y falló en silencio: `[10b]` corre la numeración un lugar, así que la
// declaración de `[15]` apuntaba al renglón equivocado y aparecía como no declarada.
// La etiqueta es del renglón; el número es de su posición.
// SIN DIVERGENCIAS, y las tres que había se cerraron el 2026-08-30 corrigiendo el
// DOCUMENTO: `[7]` afirmaba que apagar el giro devuelve la orientación —el Product
// Owner decidió que apagar es detener—, `[10]` exigía cero globales sueltas cuando
// la que hay la pone el motor gráfico, y `[15]` contaba siete códigos cuando el
// séptimo era el del texto que la fachada ya no recibe.
const divergencias = {};


let declaradas = 0;
let noDeclaradas = 0;
const verificacion = [];

for (let i = 0; i < Math.max(esperadas.length, lineas.length); i += 1) {
  const e = esperadas[i] ?? '(línea de más)';
  const p = lineas[i] ?? '(línea ausente)';
  if (e === p) continue;
  const n = i + 1;
  const motivo = divergencias[etiqueta(e)];
  if (motivo) {
    declaradas += 1;
    verificacion.push(`  línea ${n} — DIVERGENCIA DECLARADA · ${motivo}`);
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
