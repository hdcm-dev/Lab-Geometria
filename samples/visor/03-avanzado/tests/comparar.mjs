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
const divergencias = {
  '[5]': 'D-1 (misma causa que [7]) · el encuadre NO vuelve. Prender la orbita mueve la camara y apagarla la deja donde quedo: el bucle deja de moverla, no la devuelve. El identificador y el resultado de dibujo si quedan intactos, que son las dos cosas del renglon que se pueden leer por separado',
  '[10]': 'D-3 · el bundle deja UNA global suelta, `__THREE__`, y no la pone el producto: la registra el motor grafico al cargarse, para avisar si hay dos copias suyas en la pagina. El nombre propio del paquete sigue siendo uno solo y las seis funciones estan; lo que §6 pide en cero es esto, y esto viene adentro del motor',
  '[15]': 'D-2 · el bundle declara SEIS de los siete codigos del contrato. El que falta es UNREADABLE_TEXT, y no es un olvido: era el codigo del texto del alumno, que la fachada ya no recibe desde ADR-08006. Y hay UNO acunado aguas abajo —`UNKNOWN`, el respaldo de `reason ?? UNKNOWN`—, que §6 exige que sean cero; hoy no es alcanzable, porque `meshFor` siempre pone motivo cuando no hay malla, pero es un codigo que el contrato no declara',
  '[7]': 'D-1 · DEFECTO. §6 dice que al apagar el giro las piezas vuelven a su orientacion de partida. No vuelven: el bucle deja de incrementar `mesh.rotation.y` y las piezas se quedan donde estaban. Apagar no es deshacer, y aca la diferencia se ve: el cuadro posterior al apagado no es el anterior al encendido',
  15: 'D-2 · el bundle declara SEIS de los siete codigos del contrato. El que falta es UNREADABLE_TEXT, y no es un olvido: era el codigo del texto del alumno, que la fachada ya no recibe desde ADR-08006. Y hay UNO acunado aguas abajo —`UNKNOWN`, el respaldo de `reason ?? UNKNOWN`—, que §6 exige que sean cero; no es alcanzable hoy, porque `meshFor` siempre pone motivo cuando no hay malla, pero es un codigo que el contrato no declara',
};

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
  console.log(`  CONFORME CON DIVERGENCIAS DECLARADAS · ${coinciden}/${esperadas.length} líneas coinciden, ${declaradas} difieren por motivo escrito`);
  process.exit(0);
}
console.log(`  NO CONFORME · ${noDeclaradas} línea(s) difieren sin motivo declarado`);
process.exit(1);
