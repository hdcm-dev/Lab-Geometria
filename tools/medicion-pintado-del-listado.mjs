// ============================================================================
// medicion-pintado-del-listado.mjs — el conductor de navegador de
// `tools/medicion-pintado-del-listado.sh`. No se corre solo.
//
// QUÉ MIDE, y son dos cosas distintas que conviene no promediar:
//
//   1. PRIMERA PINTURA. Desde `goto('/entrega-comision')` hasta que están en el
//      documento los grupos y las filas que se esperan. Incluye el render del
//      servidor y el transporte del documento. Es lo que ve el docente cuando
//      entra a la pantalla.
//
//   2. FILTRO POR ALUMNO. Volver a pedir la colección acotada a un alumno.
//
// Y ACÁ HAY QUE DESHACER UNA SUPOSICIÓN QUE PARECÍA OBVIA Y ES FALSA. Esta
// superficie NO USA EL CIRCUITO: `ClassSubmissionList.razor` no declara
// `@rendermode` —sólo SEIS componentes de la pieza pública lo hacen, contados en
// `ADR-10001` §2.1, y éste no está— y su filtro es un `<form method="get">`. El propio código lo dice:
// «los dos viajan por la dirección porque esta superficie es de render
// estático». De modo que el filtro es UNA NAVEGACIÓN, no un viaje por la sesión
// interactiva.
//
// LA CONSECUENCIA ES BUENA Y CONVIENE NO PERDERLA: el comportamiento de esta
// pantalla ante el volumen NO DEPENDE DEL TRANSPORTE. Que el hosting ofrezca
// WebSocket o repliegue a long polling no cambia nada acá, porque el circuito no
// participa. Lo medido en local es por lo tanto REPRESENTATIVO —salvo la latencia
// de red hasta el hosting— y no un piso.
//
// Uso:  node medicion-pintado-del-listado.mjs <base> <correo> <clave> <grupos> <filas>
// Salida: una línea JSON por corrida, a stdout.
// ============================================================================
import { chromium } from 'playwright';

const [base, correo, clave, gruposEsperados, filasEsperadas] = process.argv.slice(2);
const nGrupos = Number(gruposEsperados);
const nFilas = Number(filasEsperadas);

const navegador = await chromium.launch();
const contexto = await navegador.newContext();
const pagina = await contexto.newPage();

const morir = async (motivo) => {
  console.log(JSON.stringify({ error: motivo }));
  await navegador.close();
  process.exit(2);
};

// ---- Ingreso. No se mide: es preparación. -----------------------------------
await pagina.goto(`${base}/ingreso`, { waitUntil: 'load' });
await pagina.fill('#signin-email', correo);
await pagina.fill('#signin-password', clave);
await Promise.all([
  pagina.waitForURL((u) => !u.pathname.endsWith('/ingreso'), { timeout: 30000 }),
  pagina.click('button[type=submit]'),
]).catch(() => {});
if (pagina.url().includes('/ingreso')) await morir('el ingreso no prosperó');

// ---- 1 · Primera pintura ----------------------------------------------------
// El criterio de «terminó» es el CONTEO, y no un `load`: la página puede estar
// cargada y el listado a medio pintar. Se espera a que estén los grupos y las
// filas que la siembra dejó, que es lo único que significa «el docente ya lo ve».
const t0 = performance.now();
await pagina.goto(`${base}/entrega-comision`, { waitUntil: 'commit' });
try {
  await pagina.waitForFunction(
    ([g, f]) =>
      document.querySelectorAll('section.gf-group').length >= g &&
      document.querySelectorAll('table.gf-table tbody tr').length >= f,
    [nGrupos, nFilas],
    { timeout: 120000 },
  );
} catch {
  const g = await pagina.locator('section.gf-group').count();
  const f = await pagina.locator('table.gf-table tbody tr').count();
  await morir(`no pintó lo esperado en 120 s: ${g}/${nGrupos} grupos, ${f}/${nFilas} filas`);
}
const pintura = performance.now() - t0;

const bytes = (await pagina.content()).length;
const grupos = await pagina.locator('section.gf-group').count();
const filas = await pagina.locator('table.gf-table tbody tr').count();

// ---- 2 · Filtro por alumno --------------------------------------------------
// Se elige el segundo `option` de `#submissions-student` —el primero es «todos»—
// y se envía el formulario. Es una navegación completa, no una actualización
// parcial: se mide hasta que el documento nuevo tiene UN solo grupo.
let filtro = null;
const alumno = pagina.locator('#submissions-student option').nth(1);
const valor = await alumno.getAttribute('value').catch(() => null);
if (valor) {
  await pagina.selectOption('#submissions-student', valor);
  const t1 = performance.now();
  await pagina.click('form.gf-filters button[type=submit]');
  try {
    await pagina.waitForFunction(
      () => document.querySelectorAll('section.gf-group').length === 1,
      null,
      { timeout: 60000 },
    );
    filtro = performance.now() - t1;
  } catch {
    filtro = null;
  }
}

console.log(JSON.stringify({
  pintura_ms: Math.round(pintura),
  filtro_ms: filtro === null ? null : Math.round(filtro),
  bytes,
  grupos,
  filas,
}));

await navegador.close();
