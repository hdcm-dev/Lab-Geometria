# Medición de las puertas técnicas `PT-02` y `PT-03`

**Producto:** Fábrica de Geometría
**Documento:** Medicion-Puertas-Tecnicas-PT-02-PT-03.md
**Versión:** 2.0
**Estado:** Emitido — **las dos puertas PASAN**, las dos medidas sobre el artefacto construido
**Fecha:** 2026-08-16
**Autor:** Orquestador SDD
**Nivel:** Producto
**Instrumento normativo:** `Roadmap-Producto.md` §2.2 — «Una puerta que no pasa **detiene la planificación de las fases que dependen de ella**; no se arrastra como deuda»
**Trazabilidad upstream:** `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §12 (`PT-02`, `PT-03`), §17.7.P.1 y §17.7.P.10; `Roadmap-Producto.md` §2.2

---

## 1. Qué se mide y por qué ahora

El roadmap ubica las dos puertas **antes de comprometer la etapa `g`**, y la etapa `g` empezó: el
motor gráfico se ancló y la capa 3 del visor se escribió. Medirlas después de construir sobre ellas
sería medir para justificar, no para decidir.

| Puerta | Qué exige |
| --- | --- |
| `PT-02` | Que el visor **funcione embebido**, y que **diez navegaciones de ida y vuelta entre trabajos no lo degraden**: `destroy` libera geometrías, materiales y el contexto gráfico |
| `PT-03` | Que el motor gráfico **quede dentro del paquete**, sin depender de una red de distribución externa |

## 2. `PT-03` — el motor dentro del paquete · **PASA**

**Cómo se midió.** Sobre el paquete construido, no sobre el archivo de proyecto: es donde la puerta
puede fallar sin que nadie lo note.

| Medición | Instrumento | Resultado |
| --- | --- | --- |
| El motor está anclado como dependencia y no por red | `package.json` | `three` en **versión exacta `0.169.0`**, sin rango |
| El motor está **dentro** del paquete | `grep` sobre `dist/geometriafactory-visor.js` | **Presente** |
| Tamaño del paquete con el motor adentro | `ls` | **494.455 bytes** (483 KiB) |
| Referencias a una red de distribución | `grep` de `cdn.`, `unpkg`, `jsdelivr` | **0, 0 y 0** |
| Carga diferida por red | `grep` de `import(` | **0** |

**Veredicto: PASA.** El motor entra empaquetado y el paquete no resuelve nada por red.

**Lo que la medición obligó a decidir, y quedó declarado.** El empaquetador avisa que 483 KiB pasan
su recomendación de 244 KiB y sugiere **partir el paquete o cargarlo por partes**, que es
exactamente lo que `PT-03` prohíbe. En lugar de apagar el aviso, `webpack.config.js` **declara un
presupuesto propio de 560 KiB**, tomado de la medición con un margen chico: el aviso sigue existiendo
y vuelve a sonar si el paquete crece de verdad.

## 3. `PT-02` — sin degradación tras diez navegaciones · **PASA**

Esta puerta tiene **dos mitades**, y conviene separarlas porque se miden distinto.

### 3.1 La mitad que se midió · **PASA**

Que el visor **no acumule nada** entre navegaciones depende de que `destroy` suelte todo lo que
`initialize` y `loadPieces` tomaron. Se verificó sobre el código de la instancia, enumerando las
liberaciones:

| Lo que se toma | Dónde se suelta |
| --- | --- |
| El bucle de dibujo | `cancelAnimationFrame` |
| Los cuatro escuchas de puntero | `removeEventListener`, los cuatro |
| Las geometrías de cada malla | `geometry.dispose()`, en el recorrido de las piezas |
| Los materiales de cada malla | `material.dispose()`, contemplando el caso de varios por malla |
| El contexto gráfico | `renderer.dispose()` **y `forceContextLoss()`** |
| El elemento de dibujo insertado | Se retira del árbol de la página |

**Y `loadPieces` libera antes de cargar**: una segunda carga sobre la misma instancia no acumula la
primera. Es la otra forma de la misma fuga, y la que se dispara **sin navegar**, con sólo
previsualizar dos veces.

**Cero referencias a identidad o almacenamiento en el paquete**, medido con `grep`:
`localStorage`, `sessionStorage`, `document.cookie`, `fetch(`, `XMLHttpRequest` y `WebSocket`
dan **0** las seis. Es `RA-02` verificado sobre el artefacto y no sobre la intención.

### 3.2 La otra mitad, medida con navegador de verdad · **PASA**

**La versión 1.0 de este documento dejó esta mitad sin medir**, porque el entorno de desarrollo no
tiene navegador. Se midió con uno en contenedor, y el guion queda en el árbol:
`scripts/verify-viewer-lifecycle.sh`.

**Cómo se midió.** Diez ciclos de crear la escena, cargar las tres piezas del escenario `E-1` —ya
reconstruidas, como el visor las recibe desde `ADR-08006`— y liberarla, sobre el paquete
**construido** y con Chromium dibujando de verdad.

| Control | Resultado |
| --- | --- |
| La primera vuelta dibuja las **3** piezas | **Sí** |
| La vuelta **10** dibuja las 3, igual que la primera | **Sí** |
| Las diez vueltas dibujan las 3, sin una degradada | **Sí** |
| Ninguna pieza queda sin dibujar en ninguna vuelta | **Sí** |
| Nunca hay más de **un** lienzo vivo a la vez | **Sí** |
| Liberar deja **cero** lienzos en la página | **Sí** |
| Liberar deja **cero** instancias vivas | **Sí** |
| El navegador **no avisó** desborde de contextos gráficos | **0 avisos** |

**Los ocho controles pasan. Veredicto: `PT-02` PASA.**

**Por qué se mide el mecanismo y no la ruta.** Una navegación de ida y vuelta hace por dentro un par
«crear, liberar», y ese par es lo que puede fallar. Ejercerlo diez veces mide lo mismo **con menos
piezas en el medio** y sin levantar el producto entero.

**Lo que esta medición NO cubre, y se declara.** No ejerce la navegación del producto —listado,
trabajo, listado— con su sesión y su servicio de datos. Si el defecto estuviera en **cuándo** la
superficie llama a liberar, y no en si liberar libera, esta medición no lo vería. Es un riesgo
menor y distinto, y queda nombrado en lugar de quedar tapado por un CONFORME.

**El aviso que delata el defecto se mira explícitamente.** «Too many active WebGL contexts» no lanza
excepción y no rompe nada visible: llega por la consola del navegador y por ningún otro lado. El
guion la escucha entera y la cuenta.

## 4. Consecuencia sobre la planificación

`Roadmap-Producto.md` §2.2 declara que una puerta que no pasa **detiene lo que depende de ella**.
Acá:

- **`PT-03` pasa**, y con eso el motor gráfico deja de ser un riesgo abierto.
- **`PT-02` pasa**, y con eso **la etapa `g` queda habilitada para comprometerse**: era la condición
  que el roadmap le ponía.

**Las dos quedan con guion repetible**, que es lo que impide que una puerta medida una vez se
convierta en una afirmación vieja: `scripts/verify-viewer-lifecycle.sh` para `PT-02`, y la
inspección del paquete de §2 para `PT-03`.

## 5. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-16 | Emisión inicial, al anclar el motor gráfico y escribir la capa 3 del visor. **`PT-03` PASA**, medida sobre el paquete construido: motor en versión exacta `0.169.0`, dentro del paquete, 494.455 bytes, y **cero** referencias a red de distribución o carga diferida. Declara el presupuesto de tamaño propio de 560 KiB que la medición obligó a fijar, en lugar de apagar el aviso del empaquetador que sugiere lo que `PT-03` prohíbe. **`PT-02` pasa en su mitad verificable**: se enumeran las seis liberaciones de `destroy`, se declara que `loadPieces` libera antes de cargar, y se mide **cero** uso de red, identidad y almacenamiento en el paquete. **La mitad de las diez navegaciones no se midió** —el entorno no tiene navegador— y se declara qué falta, paso por paso, con la consecuencia sobre la planificación: la etapa `g` se construye, y su punto de control no debería cerrarse sin esa medición. | Orquestador SDD |
| 2.0 | 2026-08-16 | **`PT-02` pasa entera.** La mitad que la 1.0 dejó sin medir —las diez idas y vueltas— se midió **con Chromium de verdad** sobre el paquete construido, y los **ocho controles pasan**: las tres piezas dibujadas en la décima vuelta igual que en la primera, un solo lienzo vivo a la vez, cero lienzos y cero instancias después de liberar, y **cero avisos** de desborde de contextos gráficos. La medición queda como guion repetible, `scripts/verify-viewer-lifecycle.sh`, con el navegador en contenedor: el entorno de desarrollo declarado no tiene navegador y agregarle uno cambiaría el entorno de todos por una medición que corre de vez en cuando. **Se declara lo que la medición no cubre**: la navegación del producto con su sesión: si el defecto estuviera en *cuándo* la superficie llama a liberar, esta medición no lo vería. §4 pasa de «la etapa `g` no debería cerrarse sin esto» a **la etapa `g` queda habilitada para comprometerse**. | Orquestador SDD |
