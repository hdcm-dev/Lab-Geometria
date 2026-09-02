// ============================================================================
// verificar-primer-arranque.mjs — LO PRIMERO QUE HACE UN LABORATORIO, POR PANTALLA.
//
// POR QUÉ EXISTE. `/aprovisionamiento-inicial` es la puerta de entrada de un
// laboratorio nuevo y **nunca se había probado por pantalla**: las pruebas de la
// solución llegan al punto de acceso del servicio de datos, que es otra cosa.
//
// Y TIENE UN AGRAVANTE QUE OTRAS SUPERFICIES NO TIENEN. Su formulario se declara
// `<form @onsubmit>` SIN `method="post"`, de modo que **fuera del circuito no hay
// envío que valga**: el navegador recarga y se lleva lo tipeado. Con la ventana
// muerta medida en este anfitrión —la página carga a los 0.9 s y el control
// responde a los 6.4 s— alguien que complete rápido y apriete no crea nada, y
// nada se lo dice.
//
// QUÉ COMPRUEBA:
//   1. La pantalla se dibuja en un laboratorio vacío.
//   2. Mientras el circuito no engancha, el formulario está INHABILITADO y LO DICE.
//   3. Cuando engancha, se habilita SOLO.
//   4. Se crea el administrador y la pantalla avanza.
//
// Necesita un laboratorio SIN administrador: con uno configurado, la superficie
// desvía a `/ingreso` y el paso 1 falla, que es lo correcto.
// ============================================================================
import { chromium } from 'playwright';
const base = process.argv[2];
const nav = await chromium.launch();
const p = await (await nav.newContext()).newPage();
const pasos = [];
const paso = (n,t,ok,d='') => { pasos.push(ok); console.log(`${ok?'PASA ':'FALLA'} ${n}. ${t}${d?' · '+d:''}`); };

await p.goto(`${base}/aprovisionamiento-inicial`, { waitUntil:'load' });
paso(1,'La pantalla de aprovisionamiento se dibuja en un laboratorio vacío',
     await p.locator('#provisioning-email').count() > 0);

const deshab = await p.locator('button[type="submit"]').isDisabled();
const avisa  = await p.locator('[data-gf-preparando]').count() > 0;
paso(2,'Mientras el circuito no engancha, el formulario está inhabilitado Y lo dice',
     deshab && avisa, `inhabilitado=${deshab} avisa=${avisa}`);

await p.locator('button[type="submit"]:not([disabled])').waitFor({ timeout: 40000 });
paso(3,'Cuando el circuito engancha, el formulario se habilita solo', true);

const clave = 'Pr-' + Math.abs(Date.now() % 100000) + '-2026';
await p.fill('#provisioning-email','docente@ejemplo.test');
await p.fill('#provisioning-first-name','Docente');
await p.fill('#provisioning-last-name','Prueba');
await p.fill('#provisioning-password', clave);
await p.fill('#provisioning-password-repeat', clave);
await Promise.all([p.waitForNavigation({waitUntil:'load', timeout:30000}).catch(()=>{}), p.click('button[type="submit"]')]);
await p.waitForTimeout(2500);
paso(4,'Se crea el administrador y la pantalla avanza', !p.url().includes('aprovisionamiento-inicial'),
     `quedó en ${new URL(p.url()).pathname}`);

console.log(`   (clave usada: ${clave})`);
await nav.close();
process.exit(pasos.every(Boolean) ? 0 : 1);
