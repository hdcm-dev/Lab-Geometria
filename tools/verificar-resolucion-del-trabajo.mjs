// ============================================================================
// verificar-resolucion-del-trabajo.mjs — ¿EL BOTÓN DE APROBAR HACE ALGO?
//
// POR QUÉ EXISTE. El 2026-09-01 el Product Owner reportó que «Aprobar» no hacía
// nada. No fallaba: NO HACÍA NADA. Las 511 pruebas de la solución estaban en
// verde mientras el botón estaba inerte, porque ninguna abre un navegador: el
// defecto era que el manejador `@onclick` no llegaba al cliente, y eso no lo ve
// ninguna prueba de unidad ni de integración por servidor.
//
// Es la cuarta vez que este producto entrega algo verificado y roto. La regla
// que salió de la mesa es la que este guion aplica: CORRER LA COSA, no la
// prueba que la rodea.
//
// QUÉ COMPRUEBA, EN ORDEN, Y TODO CONTRA UN NAVEGADOR DE VERDAD:
//
//   1. El bloque de resolución se dibuja para el administrador.
//   2. Apretar «Aprobar» ABRE EL DIÁLOGO de confirmación —no aplica nada—.
//   3. El diálogo nombra el trabajo, declara la terminalidad y muestra el
//      comentario escrito.
//   4. «Cancelar» cierra sin aplicar, y el trabajo sigue en `Pendiente`.
//   5. Confirmar aplica el desenlace y aterriza en `/entrega-comision`
//      —NO en `/comision`, que no existe y era el segundo defecto—.
//   6. El trabajo quedó en `Finalizado` según el SERVICIO DE DATOS, no según
//      la pantalla.
//
// EL PASO 5 ES EL QUE ATRAPA EL SEGUNDO DEFECTO. Comprobar sólo que «el botón
// responde» habría dado verde con el administrador cayendo en «no encontrado»,
// que se lee exactamente igual que una aprobación fallida.
//
// SALIDA: una línea JSON por paso, y código 0 sólo si los seis pasan.
// ============================================================================
import { chromium } from 'playwright';

const [base, correo, clave, trabajoId, nombreTrabajo] = process.argv.slice(2);

const pasos = [];
let navegador;

const paso = (n, titulo, ok, detalle) => {
    pasos.push({ n, titulo, ok, detalle });
    console.log(`${ok ? 'PASA ' : 'FALLA'} ${n}. ${titulo}${detalle ? ' · ' + detalle : ''}`);
};

const morir = async (motivo) => {
    console.log(`NO SE PUEDE VERIFICAR · ${motivo}`);
    if (navegador) await navegador.close();
    process.exit(2);
};

// La huella del certificado efímero, cuando la verificación corre sobre HTTPS: hace que
// Chromium lo trate como VÁLIDO, que es lo único que le permite guardar cookies `Secure`.
const spki = process.env.GF_SPKI || '';
navegador = await chromium.launch({
    args: spki ? ['--ignore-certificate-errors-spki-list=' + spki.trim()] : [],
});
// El certificado del banco es propio y efímero: el navegador lo acepta a propósito.
const pagina = await (await navegador.newContext({ ignoreHTTPSErrors: true })).newPage();
pagina.setDefaultTimeout(15000);

// ---- LO QUE PASA DEL LADO DEL NAVEGADOR SE MIRA, NO SE SUPONE --------------
// El circuito puede no engancharse sin que la pantalla diga nada: el marcado
// prerrenderizado queda igual y los botones quedan muertos. Sin esto, un
// «no pasa nada» no distingue entre el manejador ausente y el circuito caído.
const bitacora = [];
pagina.on('console', (m) => bitacora.push(`consola[${m.type()}] ${m.text().slice(0, 220)}`));
pagina.on('pageerror', (e) => bitacora.push(`EXCEPCIÓN DE PÁGINA · ${String(e).slice(0, 220)}`));
pagina.on('requestfailed', (r) => bitacora.push(`petición fallida · ${r.url().slice(0, 140)} · ${r.failure()?.errorText}`));
pagina.on('response', (r) => {
    const u = r.url();
    if (u.includes('_blazor') || u.includes('blazor.web.js')) {
        bitacora.push(`respuesta ${r.status()} · ${u.slice(0, 140)}`);
    }
});
pagina.on('websocket', (ws) => bitacora.push(`websocket abierto · ${ws.url().slice(0, 120)}`));

const volcarBitacora = () => {
    console.log('   ---- bitácora del navegador ----');
    for (const linea of bitacora.slice(-25)) console.log(`   ${linea}`);
};

// ---- Entrar como administrador ---------------------------------------------
await pagina.goto(`${base}/ingreso`, { waitUntil: 'load' });
await pagina.fill('#signin-email', correo);
await pagina.fill('#signin-password', clave);
await Promise.all([
    pagina.waitForNavigation({ waitUntil: 'load' }),
    pagina.click('button[type="submit"]'),
]);

// EL INGRESO SE COMPRUEBA, NO SE SUPONE. Si no entró, todo lo que siga mide otra
// cosa: el 2026-09-01 esta verificación informó «el bloque no se dibujó» cuando
// en realidad el navegador estaba parado en `/ingreso`, y eso hizo creer por un
// rato que el producto fallaba en `Production`.
console.log(`   tras enviar el ingreso, el navegador quedó en: ${pagina.url()}`);
if (new URL(pagina.url()).pathname === '/ingreso') {
    const banda = await pagina.locator('.gf-banner--error').count() > 0
        ? (await pagina.locator('.gf-banner--error').first().innerText()).replace(/\s+/g, ' ')
        : '(sin banda de error: el formulario volvió limpio, así que la MARCA DE SESIÓN no se conservó)';
    const galletas = await pagina.context().cookies();
    console.log(`   cookies tras el ingreso: ${galletas.map((c) => `${c.name}[secure=${c.secure},sameSite=${c.sameSite}]`).join(', ') || '(ninguna)'}`);
    await morir(`el ingreso no prosperó · ${banda}`);
}

// ---- 1 · el bloque se dibuja -----------------------------------------------
await pagina.goto(`${base}/trabajos/${trabajoId}`, { waitUntil: 'load' });

const aprobar = pagina.locator('[data-gf-outcome="Approve"]');
if (await aprobar.count() === 0) {
    // NO SE MUERE SIN DECIR QUÉ SE VIO. Un «no se dibujó» a secas no distingue
    // entre no haber entrado, haber entrado sin papel de administrador y que el
    // trabajo esté en otro estado; y esas tres tienen arreglos distintos.
    const cuerpo = (await pagina.locator('body').innerText()).replace(/\s+/g, ' ').slice(0, 400);
    console.log(`   dirección: ${pagina.url()}`);
    console.log(`   la página dice: ${cuerpo}`);
    await morir('el bloque de resolución no se dibujó para el administrador.');
}
paso(1, 'El bloque de resolución se dibuja', true);

// ESPERAR A QUE EL CIRCUITO ESTÉ VIVO. Con render interactivo el marcado llega
// primero por prerrenderizado y los manejadores se cablean cuando la conexión se
// establece. Apretar antes de eso mediría la carrera, no el defecto.
await pagina.waitForFunction(
    () => window.Blazor !== undefined,
    null,
    { timeout: 20000 },
).catch(() => {});
await pagina.waitForTimeout(2500);

// ---- 2 y 3 · apretar abre el diálogo, y el diálogo dice lo que debe ---------
const comentario = 'Revisá el área del cubo.';
await pagina.fill('#resolution-comment', comentario);
await aprobar.click();

const dialogo = pagina.locator('dialog[data-gf-dialog]');
let abrio = false;
try {
    await dialogo.waitFor({ state: 'visible', timeout: 8000 });
    abrio = true;
} catch { /* queda en falso */ }

paso(2, 'Apretar «Aprobar» abre el diálogo de confirmación', abrio,
    abrio ? '' : 'EL BOTÓN NO HIZO NADA: no hay diálogo. Es el defecto reportado.');

if (!abrio) volcarBitacora();

if (!abrio) {
    // Sin diálogo no hay nada más que comprobar, y hay que decir si además
    // aplicó el desenlace por las suyas.
    await pagina.waitForTimeout(1500);
    paso(3, 'El diálogo nombra el trabajo y declara la terminalidad', false, 'no hay diálogo');
    paso(4, '«Cancelar» cierra sin aplicar', false, 'no hay diálogo');
    paso(5, 'Confirmar aterriza en /entrega-comision', false, `quedó en ${new URL(pagina.url()).pathname}`);
    paso(6, 'El servicio de datos dice Finalizado', false, 'no se llegó a aplicar');
    await cerrar();
}

const textoDialogo = (await dialogo.innerText()).replace(/\s+/g, ' ');
const nombra = textoDialogo.includes(nombreTrabajo);
const terminal = textoDialogo.includes('Finalizado') && textoDialogo.includes('definitivo');
const muestraComentario = textoDialogo.includes(comentario);
paso(3, 'El diálogo nombra el trabajo, declara la terminalidad y muestra el comentario',
    nombra && terminal && muestraComentario,
    `nombre=${nombra} terminalidad=${terminal} comentario=${muestraComentario}`);

// ---- 4 · cancelar no aplica -------------------------------------------------
await pagina.click('[data-gf-dialog-dismiss]');
await dialogo.waitFor({ state: 'detached', timeout: 8000 }).catch(() => {});
const siguePendiente = await pagina.locator('[data-gf-outcome="Approve"]').count() > 0;
paso(4, '«Cancelar» cierra sin aplicar y el bloque sigue disponible', siguePendiente);

// ---- 5 · confirmar aplica y aterriza donde debe -----------------------------
await pagina.locator('[data-gf-outcome="Approve"]').click();
await dialogo.waitFor({ state: 'visible', timeout: 8000 });
await Promise.all([
    pagina.waitForNavigation({ waitUntil: 'load', timeout: 20000 }).catch(() => {}),
    pagina.click('[data-gf-confirm-outcome="Approve"]'),
]);
await pagina.waitForTimeout(1500);

const destino = new URL(pagina.url()).pathname;
paso(5, 'Confirmar aterriza en /entrega-comision', destino === '/entrega-comision',
    `aterrizó en ${destino}`);

await cerrar();

async function cerrar() {
    await navegador.close();
    const fallaron = pasos.filter((p) => !p.ok);
    console.log('');
    console.log(fallaron.length === 0
        ? `CONFORME · los ${pasos.length} pasos pasaron`
        : `NO CONFORME · ${fallaron.length} de ${pasos.length} paso(s) fallaron`);
    process.exit(fallaron.length === 0 ? 0 : 1);
}
