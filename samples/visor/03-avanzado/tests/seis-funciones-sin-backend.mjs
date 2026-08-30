// `CU-12006` — las seis funciones y las propiedades transversales que no dependen
// del movimiento.
import { abrir, decir, reiniciarAcumulado } from './comun.mjs';

reiniciarAcumulado();
const { navegador, hoja, red } = await abrir();

// ---- [1] las seis, de verdad invocadas ----
// SE CUENTAN LAS QUE ESTE RECORRIDO INVOCA, no las que la fachada declara: decir
// «6 de 6» leyendo el objeto global mediría el objeto y no el recorrido.
const invocadas = [];
await hoja.evaluate(() => window.anfitrion.inicializar()); invocadas.push('crear');
await hoja.evaluate(() => window.anfitrion.cargar('E1')); invocadas.push('cargar');
await hoja.evaluate(() => window.anfitrion.seleccionar(0)); invocadas.push('seleccionar');
await hoja.evaluate(() => window.anfitrion.ajustar()); invocadas.push('ajustar');
await hoja.evaluate(() => window.anfitrion.gobernar({ pieceSpin: true })); invocadas.push('gobernar');
await hoja.evaluate(() => window.anfitrion.destruir()); invocadas.push('destruir');
decir(`[1] Recorrido de las seis funciones con E-1: ${invocadas.join(', ')}=${invocadas.length} de 6`);

// ---- [2] el backend ----
// NO SE AFIRMA QUE NO HAY BACKEND: se cuenta. Cualquier petición que no sea el
// propio archivo local ya sería un servicio consultado.
decir(`[2] Servicios del backend disponibles durante el recorrido: ${red.length}`);

await navegador.close();
