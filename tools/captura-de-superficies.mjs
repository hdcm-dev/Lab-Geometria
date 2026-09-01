// ============================================================================
// captura-de-superficies.mjs — LA EVIDENCIA QUE SE MIRA, no la que se lee.
//
// POR QUÉ EXISTE. El 2026-09-01 el Product Owner dijo: «hasta ahora no vi
// resultados funcionando». Tenía razón. Se había corregido un `P0` que decía
// «por debajo de 768 px el docente ve CERO FILAS» y **nunca se le mostró una
// pantalla angosta con filas**: sólo pruebas en verde y compuertas conformes,
// que no es lo mismo.
//
// QUÉ HACE. Levanta las superficies del producto en dos anchos —360 px, un
// teléfono, y 1280 px— y guarda una imagen de cada una. Nada más. No mide, no
// verifica, no opina: **deja algo que se puede mirar**.
//
// Uso:  node captura-de-superficies.mjs <base> <correo> <clave> <destino>
// ============================================================================
import { chromium } from 'playwright';
import { mkdirSync } from 'node:fs';

const [base, correo, clave, destino] = process.argv.slice(2);
mkdirSync(destino, { recursive: true });

const ANCHOS = [
  { nombre: 'telefono', width: 360, height: 780 },
  { nombre: 'escritorio', width: 1280, height: 900 },
];

// El orden importa: las tres de listado primero, que son las del `P0`.
const SUPERFICIES = [
  { archivo: 'listado-de-la-comision', ruta: '/entrega-comision' },
  { archivo: 'panel-de-cuentas', ruta: '/cuentas' },
  { archivo: 'mis-trabajos', ruta: '/mis-trabajos' },
];

const navegador = await chromium.launch();
const salida = [];

for (const ancho of ANCHOS) {
  const contexto = await navegador.newContext({ viewport: { width: ancho.width, height: ancho.height } });
  const pagina = await contexto.newPage();

  await pagina.goto(`${base}/ingreso`, { waitUntil: 'load' });
  await pagina.fill('#signin-email', correo);
  await pagina.fill('#signin-password', clave);
  await Promise.all([
    pagina.waitForURL((u) => !u.pathname.endsWith('/ingreso'), { timeout: 30000 }),
    pagina.click('button[type=submit]'),
  ]).catch(() => {});

  for (const sup of SUPERFICIES) {
    await pagina.goto(`${base}${sup.ruta}`, { waitUntil: 'load' });
    await pagina.waitForTimeout(400);

    // Lo que interesa contar es lo que la persona PUEDE ABRIR: en la versión
    // angosta las filas de tabla están ocultas y lo que se ve son las tarjetas.
    const visibles = await pagina.evaluate(() => {
      const cuenta = (sel) =>
        [...document.querySelectorAll(sel)].filter((e) => e.getBoundingClientRect().height > 0).length;
      return { filasTabla: cuenta('table.gf-table tbody tr'), tarjetas: cuenta('article.gf-row-card') };
    });

    const archivo = `${destino}/${sup.archivo}-${ancho.nombre}.png`;
    await pagina.screenshot({ path: archivo, fullPage: true });
    salida.push({ superficie: sup.ruta, ancho: ancho.nombre, px: ancho.width, ...visibles, archivo });
  }
  await contexto.close();
}

console.log(JSON.stringify(salida, null, 1));
await navegador.close();
