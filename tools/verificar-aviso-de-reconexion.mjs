// ============================================================================
// verificar-aviso-de-reconexion.mjs — LOS CINCO TRAMOS DEL AVISO, EJERCIDOS.
//
// POR QUÉ EXISTE. El 2026-08-31 se conectó el aviso de reconexión al mecanismo
// del marco y se dejaron `failed` y `rejected` SIN MOSTRARSE, con el argumento
// —correcto— de que la maqueta sólo aprobó el texto de reintento. La conclusión
// fue desastrosa: **el marco ya cubría ese caso con su aviso por omisión, y al
// declarar el elemento se lo quitó**. Al agotarse los reintentos no aparecía
// nada: pantalla viva, controles muertos, cero aviso. Estuvo EN PRODUCCIÓN, y
// es el «vencimiento silencioso» que `Wireframes-Estado-Degradado-Y-Reconexion.md`
// §5 prohíbe por escrito. Lo levantó la mesa del 2026-09-01 como `R-1`.
//
// NINGUNA DE LAS 510 PRUEBAS PODÍA VERLO: no hay proyecto de pruebas de la capa
// pública, y el estado lo aplica el marco en el navegador. Por eso esto es un
// guion de navegador y no una prueba de xUnit.
//
// QUÉ COMPRUEBA. Que en cada uno de los cinco estados que Blazor aplica, la
// persona ve LO QUE CORRESPONDE: nada cuando no hay corte, el texto de reintento
// mientras reintenta, y el de reconexión agotada CON SU SALIDA cuando se rinde.
//
// Uso:  node verificar-aviso-de-reconexion.mjs <base>
// Salida: 0 si los cinco tramos son correctos, 1 si alguno no.
// ============================================================================
import { chromium } from 'playwright';

const [base] = process.argv.slice(2);
const ESPERADO = [
  { clase: '',                                  visible: false, reintentando: false, agotado: false },
  { clase: 'components-reconnect-show',         visible: true,  reintentando: true,  agotado: false },
  { clase: 'components-reconnect-failed',       visible: true,  reintentando: false, agotado: true  },
  { clase: 'components-reconnect-rejected',     visible: true,  reintentando: false, agotado: true  },
  { clase: 'components-reconnect-hide',         visible: false, reintentando: false, agotado: false },
];

const nav = await chromium.launch();
const p = await (await nav.newContext({ viewport: { width: 1280, height: 900 } })).newPage();
await p.goto(`${base}/estado`, { waitUntil: 'load' });

let fallas = 0;
for (const e of ESPERADO) {
  const r = await p.evaluate((c) => {
    const el = document.getElementById('components-reconnect-modal');
    if (!el) return null;
    el.className = 'gf-reconnect-notice' + (c ? ' ' + c : '');
    const vis = (s) => { const x = el.querySelector(s); return !!x && x.getBoundingClientRect().height > 0; };
    return { visible: el.getBoundingClientRect().height > 0,
             reintentando: vis('.gf-reconnect-reintentando'),
             agotado: vis('.gf-reconnect-agotado'),
             texto: el.innerText.replace(/\s+/g, ' ').trim() };
  }, e.clase);

  if (!r) { console.log('NO CONFORME · no existe #components-reconnect-modal en el documento'); fallas++; break; }
  const ok = r.visible === e.visible && r.reintentando === e.reintentando && r.agotado === e.agotado;
  if (!ok) fallas++;
  console.log(`  ${ok ? 'ok  ' : 'FALLA'} ${(e.clase || '(sin clase)').padEnd(34)} ${ok ? r.texto.slice(0, 70) : JSON.stringify(r)}`);
}

console.log(fallas === 0
  ? 'CONFORME · los cinco tramos del aviso de reconexión muestran lo que corresponde'
  : `NO CONFORME · ${fallas} tramo(s) del aviso no muestran lo que corresponde`);
await nav.close();
process.exit(fallas === 0 ? 0 : 1);
