// ============================================================================
// sonda-circuito-en-el-anfitrion.mjs — ¿LA INTERACTIVIDAD ESTÁ VIVA ALLÁ?
//
// LA PREGUNTA QUE NADIE SABÍA CONTESTAR. El 2026-09-01, con el botón de aprobar
// sin funcionar en `aplicada.somee.com`, no había forma de decir si el circuito
// de tiempo real siquiera se enganchaba en el anfitrión: los avisos que se ven
// en pantalla SE DIBUJAN IGUAL DESDE EL SERVIDOR, así que verlos no prueba nada.
//
// CÓMO LO CONTESTA, Y POR QUÉ NO NECESITA CREDENCIALES. `/estado` es la única
// superficie que es interactiva Y anónima: tiene un botón `@onclick` que vuelve
// a leer la salud y reescribe la marca de lectura. Si esa marca cambia, el
// manejador corrió EN EL SERVIDOR y volvió por el circuito. Es la prueba de vida
// más barata que este producto puede dar de sí mismo, y no toca ningún dato.
//
// SE CORRE ASÍ, contra cualquier despliegue:
//     node sonda-circuito-en-el-anfitrion.mjs https://aplicada.somee.com
//
// La bitácora incluye el transporte: en somee WebSockets NO PASA y se repliega a
// sondeo largo, que funciona. Eso es del anfitrión y no un defecto del producto.
// ============================================================================
import { chromium } from 'playwright';
const base = process.argv[2];
const nav = await chromium.launch();
const pag = await (await nav.newContext()).newPage();
const log = [];
pag.on('console', m => log.push(`consola[${m.type()}] ${m.text().slice(0,200)}`));
pag.on('pageerror', e => log.push(`EXCEPCION ${String(e).slice(0,200)}`));
pag.on('response', r => { const u=r.url(); if(u.includes('_blazor')) log.push(`${r.status()} ${u.split('?')[0].slice(-40)}`); });
pag.on('websocket', ws => log.push(`websocket ${ws.url().slice(0,80)}`));
await pag.goto(`${base}/estado`, { waitUntil: 'load' });
await pag.waitForTimeout(9000);
const antes = await pag.locator('.read-at').innerText().catch(()=> '(sin marca)');
console.log(`   marca de lectura ANTES : ${antes}`);
await pag.click('button.action');
await pag.waitForTimeout(6000);
const despues = await pag.locator('.read-at').innerText().catch(()=> '(sin marca)');
console.log(`   marca de lectura DESPUES: ${despues}`);
console.log(`   ¿el boton hizo algo?: ${antes !== despues ? 'SI — EL CIRCUITO ANDA' : 'NO — EL CIRCUITO NO ENGANCHA'}`);
console.log('   ---- bitacora ----');
for (const l of log.slice(-14)) console.log(`   ${l}`);
await nav.close();
