/* ============================================================================
   Visor-Tridimensional.js — el visualizador real, absorbido y corregido
   Fase B2 · GeometriaFactory-Web · iteración 3

   QUÉ ES ESTE ARCHIVO
   -------------------
   Es el port del visualizador que la cátedra ya usa
   (`tools_json_figure_viewer/js/visor.js`), adaptado a lo que la
   documentación del producto declara que el port corrige. Ya no hay
   representación rotulada: acá se construyen mallas de verdad con Three.js.

   QUÉ SE ABSORBIÓ
   ---------------
   - La construcción de los objetos tridimensionales y sus funciones de
     creación: cilindro, cubo, ortoedro y las tres formas planas.
   - La escena con sus luces (ambiental, direccional con sombra y puntual),
     su grilla de referencia y su cámara orbital reimplementada a mano,
     porque r128 no trae control orbital.

   QUÉ **NO** SE ABSORBIÓ, Y ES LO IMPORTANTE
   ------------------------------------------
   1. `Math.random()` en la disposición (visor.js:209,210,232,233,246,247,
      276,277 y el barajado de 815-816). La posición se deriva del ÍNDICE de
      la pieza en el conjunto raíz. Dos procesados del mismo texto producen
      exactamente la misma disposición, comparable pieza por pieza.
   2. La intolerancia de claves. El visor original exige `Bases` en el
      ortoedro y por eso ningún ortoedro emitido por el programa del alumno
      —que emite `Tapas`— se dibujaba. Acá se toleran las dos, más `Cuadrado`
      o `Rectangulo` en las caras, más comas finales y comentarios en el texto.
   3. El fallo silencioso. La pieza que no se puede dibujar no desaparece:
      se devuelve enumerada con su índice y su código de condición, y el
      llamador la muestra. `dibujadas + noDibujadas == conjunto raíz`, siempre.

   Y lo que la documentación declara que no se porta (intake §17.7 P.2):
   las cinco variantes comentadas del procesamiento del arreglo, las dos de
   posición aleatoria, la función de cilindro sin uso (`updateCylinder`, que
   lee dos deslizadores inexistentes), los dos manejadores que referencian
   elementos inexistentes (`toggleWireframe` y `centerObjects`, cableados
   sobre funciones que nunca se definieron) y las bibliotecas cargadas sin
   usar (jQuery, Popper y el JavaScript del framework de grilla).

   REGLAS QUE ESTE ARCHIVO CUMPLE
   ------------------------------
   - No hace red. Su umbral de peticiones es exactamente cero: no hay fetch,
     ni XHR, ni carga de texturas, ni fuentes, ni imágenes.
   - No lee configuración: no toca `localStorage`, `sessionStorage`, cookies
     ni parámetros de la dirección.
   - No sabe quién es el usuario: no recibe ni consulta identidad ninguna.
   - No define color ad hoc: los toma de los tokens del catálogo de diseño
     ya materializados como variables CSS en `Estilos-Maqueta.css`.
   ============================================================================ */

(function (global) {
  'use strict';

  /* ==========================================================================
     0. Tokens visuales
     Ningún literal de color: se leen del catálogo ya materializado en CSS.
     ========================================================================== */

  function token(nombre, respaldo) {
    try {
      var v = getComputedStyle(document.documentElement).getPropertyValue(nombre).trim();
      return v || respaldo;
    } catch (e) { return respaldo; }
  }

  function paleta() {
    return {
      fondo:      token('--color-background-secondary', '#F1F1EF'),
      grilla:     token('--color-border-secondary', '#D9D9D4'),
      seleccion:  token('--color-brand-primary', '#0F6E56'),
      porTipo: {
        Cilindro:   token('--color-accent-module-d', '#185FA5'),
        Cubo:       token('--color-accent-module-c', '#854F0B'),
        Ortoedro:   token('--color-brand-primary', '#0F6E56'),
        Rectangulo: token('--color-accent-module-b', '#534AB7'),
        Cuadrado:   token('--color-text-secondary', '#5C5C57'),
        Circulo:    token('--color-brand-primary-dark', '#04342C')
      }
    };
  }

  /* ==========================================================================
     1. Lectura tolerante del texto

     Tolerancias que el port agrega (y que el visor original no tiene):
     comentarios de línea y de bloque, comas finales, claves alternativas y
     dimensiones expresadas como cadena. El cero es una dimensión legible:
     el intake §20.E-6 exige que la figura con una dimensión en 0.00 NO se
     descarte, y el visor original la perdía por evaluar la verdad del número.
     ========================================================================== */

  /* Limpieza que respeta las cadenas: el reemplazo global del original borra
     cualquier `//` aunque esté dentro de un valor de texto. */
  function limpiar(texto) {
    var fuera = '', i = 0, n = texto.length, c, sig;
    while (i < n) {
      c = texto.charAt(i);
      if (c === '"') {                       // copiar la cadena tal cual
        fuera += c; i++;
        while (i < n) {
          c = texto.charAt(i);
          fuera += c; i++;
          if (c === '\\') { fuera += texto.charAt(i); i++; continue; }
          if (c === '"') { break; }
        }
        continue;
      }
      sig = texto.charAt(i + 1);
      if (c === '/' && sig === '/') {        // comentario de línea
        while (i < n && texto.charAt(i) !== '\n') { i++; }
        continue;
      }
      if (c === '/' && sig === '*') {        // comentario de bloque
        i += 2;
        while (i < n && !(texto.charAt(i) === '*' && texto.charAt(i + 1) === '/')) { i++; }
        i += 2;
        continue;
      }
      fuera += c; i++;
    }
    return fuera.replace(/,(\s*[}\]])/g, '$1').trim();   // comas finales
  }

  /* Número legible: acepta el número, la cadena numérica y la coma decimal.
     Devuelve `null` cuando no hay dimensión que leer. El 0 SÍ es legible. */
  function num(v) {
    if (typeof v === 'number') { return isFinite(v) ? v : null; }
    if (typeof v === 'string') {
      var x = parseFloat(v.replace(',', '.'));
      return isFinite(x) ? x : null;
    }
    return null;
  }

  /* Primera clave presente de una lista de alternativas. Acá vive la
     tolerancia `Bases` ⇄ `Tapas` que hoy impide dibujar los ortoedros. */
  function alguna(obj, claves) {
    for (var i = 0; i < claves.length; i++) {
      if (obj && Object.prototype.hasOwnProperty.call(obj, claves[i])) { return obj[claves[i]]; }
    }
    return undefined;
  }

  function primeroDe(v) { return Array.isArray(v) ? v[0] : v; }

  var TIPOS_DIBUJABLES = ['Cilindro', 'Cubo', 'Ortoedro', 'Rectangulo', 'Cuadrado', 'Circulo'];

  /* Describe una pieza del conjunto raíz: qué es, si se puede dibujar y con
     qué medidas. Nunca descarta en silencio: si no se dibuja, dice por qué. */
  function describirPieza(datos, indice) {
    var nodo = {
      indice: indice,
      tipo: (datos && datos.Tipo) ? String(datos.Tipo) : 'sin tipo declarado',
      datos: datos,
      forma: null, dimensiones: null,
      codigo: null, motivo: null,
      hijos: []
    };

    if (!datos || typeof datos !== 'object' || Array.isArray(datos)) {
      nodo.codigo = 'TIPO_NO_DIBUJABLE';
      nodo.motivo = 'la pieza no declara una figura';
      return nodo;
    }

    var tipo = nodo.tipo, tapas, lado, caras, bases, laterales, r, h, l, a;

    if (TIPOS_DIBUJABLES.indexOf(tipo) === -1) {
      nodo.codigo = 'TIPO_NO_DIBUJABLE';
      nodo.motivo = 'tipo no dibujable';
      return nodo;
    }

    switch (tipo) {
      case 'Cilindro':
        tapas = primeroDe(alguna(datos, ['Tapas', 'Bases']));
        lado  = primeroDe(alguna(datos, ['Lado', 'Lateral', 'Laterales']));
        r = tapas ? num(alguna(tapas, ['Radio'])) : null;
        h = lado ? num(alguna(lado, ['Largo', 'Alto', 'Altura'])) : null;
        if (r === null || h === null) { break; }
        nodo.forma = 'cilindro';
        nodo.dimensiones = { radio: r, alto: h };
        nodo.hijos = etiquetasDe(datos, ['Tapas', 'Bases', 'Lado', 'Lateral']);
        return nodo;

      case 'Cubo':
        // Tolerancia de claves: la colección puede venir como Caras o Tapas,
        // y cada cara puede declararse Cuadrado o Rectangulo indistintamente.
        caras = primeroDe(alguna(datos, ['Caras', 'Tapas', 'Lados']));
        l = caras ? num(alguna(caras, ['Largo', 'Lado', 'Ancho'])) : null;
        if (l === null) { break; }
        nodo.forma = 'cubo';
        nodo.dimensiones = { lado: l };
        nodo.hijos = etiquetasDe(datos, ['Caras', 'Tapas', 'Lados']);
        return nodo;

      case 'Ortoedro':
        // ÉSTA es la tolerancia que hoy falta: el programa del alumno emite
        // "Tapas" y el visor original sólo acepta "Bases".
        bases     = primeroDe(alguna(datos, ['Bases', 'Tapas']));
        laterales = primeroDe(alguna(datos, ['Laterales', 'Lados', 'Caras']));
        l = bases ? num(alguna(bases, ['Largo'])) : null;
        a = bases ? num(alguna(bases, ['Ancho'])) : null;
        h = laterales ? num(alguna(laterales, ['Ancho', 'Alto', 'Altura'])) : null;
        if (l === null || a === null || h === null) { break; }
        nodo.forma = 'ortoedro';
        nodo.dimensiones = { largo: l, ancho: a, alto: h };
        nodo.hijos = etiquetasDe(datos, ['Bases', 'Tapas', 'Laterales', 'Lados']);
        return nodo;

      case 'Rectangulo':
        l = num(alguna(datos, ['Largo']));
        a = num(alguna(datos, ['Ancho']));
        if (l === null || a === null) { break; }
        nodo.forma = 'rectangulo';
        nodo.dimensiones = { largo: l, ancho: a };
        return nodo;

      case 'Cuadrado':
        l = num(alguna(datos, ['Largo', 'Lado']));
        if (l === null) { l = num(alguna(datos, ['Ancho'])); }
        if (l === null) { break; }
        nodo.forma = 'cuadrado';
        nodo.dimensiones = { largo: l, ancho: l };
        return nodo;

      case 'Circulo':
        r = num(alguna(datos, ['Radio']));
        if (r === null) { break; }
        nodo.forma = 'circulo';
        nodo.dimensiones = { radio: r };
        return nodo;
    }

    nodo.codigo = 'DIMENSION_NO_LEGIBLE';
    nodo.motivo = 'dimensión no legible';
    return nodo;
  }

  /* Etiquetas de los hijos, con el mismo formato con el que el árbol de la
     maqueta ya los venía rotulando: «Tapas (2)», «Lado», «Caras (6)». */
  function etiquetasDe(datos, claves) {
    var fuera = [];
    claves.forEach(function (k) {
      if (!Object.prototype.hasOwnProperty.call(datos, k)) { return; }
      var v = datos[k];
      fuera.push({ etiqueta: Array.isArray(v) ? (k + ' (' + v.length + ')') : k, clave: k, valor: v });
    });
    return fuera;
  }

  /* Interpreta el texto entero. Devuelve el conjunto raíz descrito pieza por
     pieza, o la condición TEXTO_NO_LEGIBLE si no se obtiene ninguna pieza. */
  function interpretar(texto) {
    var datos;
    try {
      datos = JSON.parse(limpiar(String(texto == null ? '' : texto)));
    } catch (e) {
      return { ok: false, condicion: 'TEXTO_NO_LEGIBLE', nodos: [], crudo: null };
    }
    if (datos === null || typeof datos !== 'object') {
      return { ok: false, condicion: 'TEXTO_NO_LEGIBLE', nodos: [], crudo: null };
    }
    var raiz = Array.isArray(datos) ? datos : [datos];
    if (!raiz.length) {
      return { ok: true, condicion: null, nodos: [], crudo: datos };
    }
    return {
      ok: true, condicion: null, crudo: datos,
      nodos: raiz.map(function (d, i) { return describirPieza(d, i); })
    };
  }

  /* ==========================================================================
     2. Disposición DERIVADA DEL ÍNDICE

     El original repartía por `Math.random()` y además barajaba objetos y
     puntos de grilla. Acá la celda sale del índice de la pieza en el conjunto
     raíz: la misma entrada da siempre la misma salida, y por eso el botón
     «Volver a cargar el mismo texto y comparar» da disposición idéntica.
     La separación conserva la idea buena del original: el radio de envoltura
     máximo del conjunto más un margen.
     ========================================================================== */

  function radioDeEnvoltura(nodo) {
    var d = nodo.dimensiones;
    if (!d) { return 2.5; }
    switch (nodo.forma) {
      case 'cilindro':   return d.radio;
      case 'cubo':       return Math.sqrt(2 * d.lado * d.lado) / 2;
      case 'ortoedro':   return Math.sqrt(d.largo * d.largo + d.ancho * d.ancho) / 2;
      case 'rectangulo':
      case 'cuadrado':   return Math.sqrt(d.largo * d.largo + d.ancho * d.ancho) / 2;
      case 'circulo':    return d.radio;
      default:           return 2.5;
    }
  }

  var MARGEN = 2;

  function disponerPorIndice(dibujables, totalRaiz) {
    var maxRadio = dibujables.reduce(function (m, n) { return Math.max(m, radioDeEnvoltura(n)); }, 0);
    var separacion = (maxRadio * 2) + MARGEN;
    var lado = Math.max(1, Math.ceil(Math.sqrt(totalRaiz)));
    var centro = (lado - 1) / 2;

    return dibujables.map(function (n) {
      var col = n.indice % lado;
      var fil = Math.floor(n.indice / lado);
      return {
        indice: n.indice, tipo: n.tipo,
        x: redondear((col - centro) * separacion),
        z: redondear((fil - centro) * separacion)
      };
    });
  }

  function redondear(v) { return Math.round(v * 1000) / 1000; }

  /* ==========================================================================
     3. Instancia del visor

     Una instancia = un elemento de dibujo + una escena + una cámara orbital +
     un contexto gráfico. Todo lo que crea queda anotado para poder liberarlo
     de verdad en `liberar()`.
     ========================================================================== */

  var recursosVivos = { geometrias: 0, materiales: 0, contextos: 0 };

  /* EL VISOR NO CONSULTA NADA DEL ENTORNO fuera de la capacidad gráfica del
     elemento que le dan. En particular NO lee `prefers-reduced-motion`: hacerlo
     sería leer configuración por su cuenta y violaría la garantía G-3 del
     contrato de fachada (`Definicion-Contrato-De-Fachada.md` §3.3). Quien lee la
     preferencia del sistema es el componente anfitrión, que la traduce a los dos
     valores de verdad que le pasa a `crear` —uno por movimiento— y a
     `orbitar()` / `girarFiguras()` después. */

  function hayCapacidadGrafica() {
    if (typeof global.THREE === 'undefined') { return false; }
    try {
      var c = document.createElement('canvas');
      return !!(c.getContext('webgl') || c.getContext('experimental-webgl'));
    } catch (e) { return false; }
  }

  function crear(elemento, opciones) {
    opciones = opciones || {};
    if (!elemento || !elemento.appendChild) { return null; }
    if (!hayCapacidadGrafica()) { return null; }

    /* Si el elemento todavía no tiene medida —porque el bloque que lo aloja
       está oculto en ese instante— se crea con una medida nominal y el primer
       `ajustar()` la corrige. Quien decide que el elemento no sirve como
       superficie de dibujo es el llamador, no una medida transitoria. */
    var ancho = elemento.clientWidth || 400, alto = elemento.clientHeight || 300;

    var col = paleta();
    var lienzo = document.createElement('canvas');
    lienzo.className = 'mq-escena-lienzo';
    /* El lienzo no es accesible por sí mismo: la alternativa textual la
       compone el llamador CON LO QUE DEVUELVE LA FACHADA, nunca leyendo el
       interior de la escena. Acá sólo se declara el rol. */
    lienzo.setAttribute('role', 'img');
    lienzo.setAttribute('aria-label', 'Escena tridimensional sin ninguna pieza dibujada.');
    /* El lienzo ES alcanzable por teclado: recibe foco y se opera con las
       flechas —orbitar—, con `+`/`-` —acercar y alejar— y con Inicio —volver
       a la vista de partida—. Sin esto la manipulación de la vista quedaba
       reservada al puntero, que es la barrera que `Maqueta-Rules.md` §4.5
       prohíbe. La ELECCIÓN de pieza sigue teniendo su ruta accesible completa
       en el árbol, que es lo que declara la sonda `SD-50`. */
    lienzo.setAttribute('tabindex', '0');
    elemento.appendChild(lienzo);

    var escena = new THREE.Scene();
    escena.background = new THREE.Color(col.fondo);

    var camara = new THREE.PerspectiveCamera(60, ancho / alto, 0.1, 1000);

    var motorDeDibujo = new THREE.WebGLRenderer({ canvas: lienzo, antialias: true });
    motorDeDibujo.setPixelRatio(Math.min(global.devicePixelRatio || 1, 2));
    motorDeDibujo.setSize(ancho, alto, false);
    motorDeDibujo.shadowMap.enabled = true;
    motorDeDibujo.shadowMap.type = THREE.PCFSoftShadowMap;
    recursosVivos.contextos++;

    /* Iluminación del original: ambiental, direccional con sombra y puntual. */
    var luzAmbiente = new THREE.AmbientLight(0xffffff, 0.55);
    escena.add(luzAmbiente);
    var luzDireccional = new THREE.DirectionalLight(0xffffff, 0.75);
    luzDireccional.position.set(10, 14, 8);
    luzDireccional.castShadow = true;
    luzDireccional.shadow.mapSize.width = 1024;
    luzDireccional.shadow.mapSize.height = 1024;
    escena.add(luzDireccional);
    var luzPunto = new THREE.PointLight(0xffffff, 0.25, 120);
    luzPunto.position.set(-12, 6, -4);
    escena.add(luzPunto);

    /* Grilla de referencia, también del original. */
    var grilla = new THREE.GridHelper(60, 30, new THREE.Color(col.grilla), new THREE.Color(col.grilla));
    grilla.material.opacity = 0.45;
    grilla.material.transparent = true;
    escena.add(grilla);

    /* --- estado de la instancia ------------------------------------------ */
    var piezas = [];                 // { indice, tipo, grupo, malla, base }
    var geometrias = [], materiales = [];
    var seleccion = null;
    var viva = true;
    var cuadro = null;
    var oyentesDeSeleccion = [];

    /* Cámara orbital reimplementada a mano: r128 no trae control orbital. */
    var distancia = 25, anguloX = 0.42, anguloY = 0.6;
    var centro = { x: 0, y: 0, z: 0 };
    var arrastrando = false, ultimoX = 0, ultimoY = 0;

    /* Los dos movimientos automáticos, independientes entre sí. Son
       PREFERENCIAS de quien mira, y el llamador las gobierna con `orbitar()` y
       `girarFiguras()`. **Si no dice nada, arrancan APAGADOS**, que es lo que
       fija §4.1 del contrato para opciones ausentes o parciales: el visor no
       adivina la preferencia de nadie y no consulta el sistema.

       - Órbita de la cámara: del original. Incrementa el azimut y reposiciona
         la cámara; las piezas quedan quietas.
       - Giro de las figuras: NO está en el original. Rota cada pieza sobre su
         eje vertical, en su lugar.

       Ninguno de los dos toca la POSICIÓN de las piezas: la celda de cada una
       sale de su índice y no del tiempo, así que la disposición no se ve
       afectada por ninguna combinación de los dos. */
    var orbitaCamara = !!opciones.orbitaCamara;
    var giroDeFiguras = !!opciones.giroDeFiguras;

    function ubicarCamara() {
      camara.position.set(
        centro.x + distancia * Math.cos(anguloX) * Math.cos(anguloY),
        centro.y + distancia * Math.sin(anguloX),
        centro.z + distancia * Math.cos(anguloX) * Math.sin(anguloY)
      );
      camara.lookAt(centro.x, centro.y, centro.z);
    }

    function anotarGeometria(g) { geometrias.push(g); recursosVivos.geometrias++; return g; }
    function anotarMaterial(m) { materiales.push(m); recursosVivos.materiales++; return m; }

    function materialDe(tipo) {
      return anotarMaterial(new THREE.MeshPhongMaterial({
        color: new THREE.Color(col.porTipo[tipo] || col.seleccion),
        transparent: true, opacity: 0.9, shininess: 60,
        side: THREE.DoubleSide
      }));
    }

    /* --- funciones de creación, portadas del original -------------------- */

    function crearCilindro(d, tipo) {
      var g = anotarGeometria(new THREE.CylinderGeometry(d.radio, d.radio, d.alto, 32));
      var m = new THREE.Mesh(g, materialDe(tipo));
      m.castShadow = true; m.receiveShadow = true;
      m.position.y = d.alto / 2;
      return m;
    }

    function crearCubo(d, tipo) {
      var g = anotarGeometria(new THREE.BoxGeometry(d.lado, d.lado, d.lado));
      var m = new THREE.Mesh(g, materialDe(tipo));
      m.castShadow = true; m.receiveShadow = true;
      m.position.y = d.lado / 2;
      return m;
    }

    function crearOrtoedro(d, tipo) {
      var g = anotarGeometria(new THREE.BoxGeometry(d.largo, d.alto, d.ancho));
      var m = new THREE.Mesh(g, materialDe(tipo));
      m.castShadow = true; m.receiveShadow = true;
      m.position.y = d.alto / 2;
      return m;
    }

    /* Formas planas. El original las acostaba sobre el piso; se conserva.
       Una dimensión en 0.00 no descarta la figura (intake §20.E-6): la malla
       se construye igual y la pieza cuenta como dibujada. */
    function crearPlana(nodo) {
      var d = nodo.dimensiones, g;
      if (nodo.forma === 'circulo') {
        g = anotarGeometria(new THREE.CircleGeometry(Math.max(d.radio, 0.0001), 32));
      } else {
        g = anotarGeometria(new THREE.PlaneGeometry(
          Math.max(d.largo, 0.0001), Math.max(d.ancho, 0.0001)));
      }
      var m = new THREE.Mesh(g, materialDe(nodo.tipo));
      m.receiveShadow = true;
      m.rotation.x = -Math.PI / 2;
      m.position.y = 0.05;
      return m;
    }

    function mallaDe(nodo) {
      switch (nodo.forma) {
        case 'cilindro': return crearCilindro(nodo.dimensiones, nodo.tipo);
        case 'cubo':     return crearCubo(nodo.dimensiones, nodo.tipo);
        case 'ortoedro': return crearOrtoedro(nodo.dimensiones, nodo.tipo);
        case 'rectangulo':
        case 'cuadrado':
        case 'circulo':  return crearPlana(nodo);
        default:         return null;
      }
    }

    /* --- vaciado de la escena, con liberación real ------------------------ */
    function vaciar() {
      piezas.forEach(function (p) { escena.remove(p.grupo); });
      geometrias.forEach(function (g) { g.dispose(); recursosVivos.geometrias--; });
      materiales.forEach(function (m) { m.dispose(); recursosVivos.materiales--; });
      geometrias = []; materiales = []; piezas = []; seleccion = null;
    }

    /* --- carga: reemplaza por completo lo dibujado ------------------------ */
    function cargar(nodos) {
      vaciar();
      nodos = nodos || [];

      var dibujables = nodos.filter(function (n) { return !!n.forma; });
      var disposicion = disponerPorIndice(dibujables, nodos.length);
      var porIndice = {};
      disposicion.forEach(function (p) { porIndice[p.indice] = p; });

      dibujables.forEach(function (n) {
        var malla = mallaDe(n);
        if (!malla) { return; }
        var grupo = new THREE.Group();
        grupo.add(malla);
        var p = porIndice[n.indice];
        grupo.position.set(p.x, 0, p.z);
        escena.add(grupo);
        piezas.push({ indice: n.indice, tipo: n.tipo, grupo: grupo, malla: malla });
      });

      centrarVista();
      describirLienzo(nodos, dibujables);
      return {
        dibujadas: disposicion.map(function (p) { return { indice: p.indice, tipo: p.tipo }; }),
        disposicion: disposicion
      };
    }

    /* Alternativa textual del lienzo. Se compone con lo que la carga devuelve
       —índices y tipos—, no leyendo el interior de la escena. */
    function describirLienzo(nodos, dibujables) {
      if (!dibujables.length) {
        lienzo.setAttribute('aria-label',
          'Escena tridimensional sin ninguna pieza dibujada.');
        return;
      }
      lienzo.setAttribute('aria-label',
        'Escena tridimensional con ' + dibujables.length +
        (dibujables.length === 1 ? ' pieza dibujada' : ' piezas dibujadas') +
        ' de ' + nodos.length + ' del conjunto: ' +
        dibujables.map(function (n) { return 'pieza ' + n.indice + ', ' + n.tipo; }).join('; ') +
        '. La lista completa, con las piezas que no se dibujaron, está en el árbol de la estructura.');
    }

    function centrarVista() {
      if (!piezas.length) { centro = { x: 0, y: 0, z: 0 }; distancia = 25; ubicarCamara(); return; }
      var caja = new THREE.Box3();
      piezas.forEach(function (p) { caja.expandByObject(p.grupo); });
      var c = caja.getCenter(new THREE.Vector3());
      var t = caja.getSize(new THREE.Vector3());
      centro = { x: c.x, y: c.y, z: c.z };
      distancia = Math.max(12, Math.max(t.x, t.y, t.z) * 1.9);
      ubicarCamara();
    }

    /* --- selección: resaltado exclusivo sobre la malla real --------------- */
    function seleccionar(indice) {
      var hay = piezas.some(function (p) { return String(p.indice) === String(indice); });
      if (!hay) { return false; }
      seleccion = indice;
      piezas.forEach(function (p) {
        var activa = String(p.indice) === String(indice);
        p.malla.material.emissive.set(activa ? new THREE.Color(col.seleccion) : new THREE.Color(0x000000));
        p.malla.material.emissiveIntensity = activa ? 0.55 : 0;
        p.malla.material.opacity = activa ? 1 : 0.55;
        p.malla.material.needsUpdate = true;
      });
      return true;
    }

    /* --- ajuste al tamaño vigente ---------------------------------------- */
    function ajustar() {
      var w = elemento.clientWidth, h = elemento.clientHeight;
      if (!w || !h) { return false; }          // ELEMENTO_DE_DIBUJO_INVALIDO, curso C-2
      camara.aspect = w / h;
      camara.updateProjectionMatrix();
      motorDeDibujo.setSize(w, h, false);
      return true;                              // selección y disposición intactas
    }

    /* --- interacción: órbita, zoom y elección de pieza por puntero -------- */
    function alApretar(ev) { arrastrando = true; ultimoX = ev.clientX; ultimoY = ev.clientY; }
    function alSoltar() { arrastrando = false; }
    function alMover(ev) {
      if (!arrastrando) { return; }
      anguloY += (ev.clientX - ultimoX) * 0.01;
      anguloX -= (ev.clientY - ultimoY) * 0.01;
      anguloX = Math.max(-Math.PI / 2 + 0.1, Math.min(Math.PI / 2 - 0.1, anguloX));
      ultimoX = ev.clientX; ultimoY = ev.clientY;
      ubicarCamara();
    }
    function acercar(factor) {
      distancia = Math.max(4, Math.min(160, distancia * factor));
      ubicarCamara();
    }

    /* La rueda SÓLO se captura cuando el lienzo tiene el foco. Si no, la página
       sigue desplazándose con normalidad: una zona que se traga la rueda al
       pasar por encima es una trampa de desplazamiento. */
    function alRodar(ev) {
      if (document.activeElement !== lienzo) { return; }
      ev.preventDefault();
      acercar(ev.deltaY > 0 ? 1.1 : 0.9);
    }

    /* Órbita, acercamiento y vuelta a la vista de partida por teclado. Es el
       equivalente de teclado del arrastre y de la rueda. */
    var PASO_ANGULO = 0.08;
    function alTeclear(ev) {
      if (ev.altKey || ev.ctrlKey || ev.metaKey) { return; }
      var manejada = true;
      switch (ev.key) {
        case 'ArrowLeft':  anguloY -= PASO_ANGULO; break;
        case 'ArrowRight': anguloY += PASO_ANGULO; break;
        case 'ArrowUp':    anguloX = Math.min(Math.PI / 2 - 0.1, anguloX + PASO_ANGULO); break;
        case 'ArrowDown':  anguloX = Math.max(-Math.PI / 2 + 0.1, anguloX - PASO_ANGULO); break;
        case '+': case '=': acercar(0.9); break;
        case '-': case '_': acercar(1.1); break;
        case 'Home': anguloX = 0.42; anguloY = 0.6; distancia = 25; break;
        default: manejada = false;
      }
      if (!manejada) { return; }
      ev.preventDefault();
      ubicarCamara();
    }

    /* Elegir una pieza pinchándola: proyección de rayo contra las mallas.
       Es lo que hace que la sincronización escena ⇄ árbol funcione en los dos
       sentidos también con la escena real. */
    var rayo = new THREE.Raycaster();
    var puntero = new THREE.Vector2();
    function alPinchar(ev) {
      if (!piezas.length) { return; }
      var caja = lienzo.getBoundingClientRect();
      puntero.x = ((ev.clientX - caja.left) / caja.width) * 2 - 1;
      puntero.y = -((ev.clientY - caja.top) / caja.height) * 2 + 1;
      rayo.setFromCamera(puntero, camara);
      var tocadas = rayo.intersectObjects(piezas.map(function (p) { return p.malla; }), false);
      if (!tocadas.length) { return; }
      var elegida = piezas.filter(function (p) { return p.malla === tocadas[0].object; })[0];
      if (!elegida) { return; }
      oyentesDeSeleccion.forEach(function (f) { f(elegida.indice); });
    }

    /* Ajuste al cambio de tamaño de la ventana, portado del original. Acá es
       por instancia y se desengancha al liberar; en el original quedaba
       colgado del objeto global para siempre. */
    function alRedimensionarVentana() { ajustar(); }
    global.addEventListener('resize', alRedimensionarVentana);

    lienzo.addEventListener('mousedown', alApretar);
    lienzo.addEventListener('mousemove', alMover);
    lienzo.addEventListener('mouseup', alSoltar);
    lienzo.addEventListener('mouseleave', alSoltar);
    lienzo.addEventListener('wheel', alRodar, { passive: false });
    lienzo.addEventListener('keydown', alTeclear);
    lienzo.addEventListener('click', alPinchar);

    /* --- bucle de dibujo -------------------------------------------------- */
    function bucle() {
      if (!viva) { return; }
      cuadro = global.requestAnimationFrame(bucle);
      /* Los dos movimientos se detienen mientras la persona arrastra la cámara,
         igual que en el original, y también con la pestaña oculta. */
      var quieto = arrastrando || document.hidden || !piezas.length;
      if (orbitaCamara && !quieto) {
        anguloY += 0.0015;                    // giro lento, como el original
        ubicarCamara();
      }
      if (giroDeFiguras && !quieto) {
        /* Sobre el eje vertical y en su lugar: cambia la orientación, nunca la
           posición, que es lo que la disposición determinista fija. */
        for (var i = 0; i < piezas.length; i++) {
          piezas[i].grupo.rotation.y += 0.004;
        }
      }
      motorDeDibujo.render(escena, camara);
    }

    ubicarCamara();
    bucle();

    /* --- liberación de verdad --------------------------------------------- */
    function liberar() {
      if (!viva) { return false; }
      viva = false;
      if (cuadro) { global.cancelAnimationFrame(cuadro); cuadro = null; }

      global.removeEventListener('resize', alRedimensionarVentana);
      lienzo.removeEventListener('mousedown', alApretar);
      lienzo.removeEventListener('mousemove', alMover);
      lienzo.removeEventListener('mouseup', alSoltar);
      lienzo.removeEventListener('mouseleave', alSoltar);
      lienzo.removeEventListener('wheel', alRodar);
      lienzo.removeEventListener('keydown', alTeclear);
      lienzo.removeEventListener('click', alPinchar);

      vaciar();                                  // geometrías y materiales
      grilla.geometry.dispose();
      grilla.material.dispose();
      escena.remove(grilla);
      escena.remove(luzAmbiente); escena.remove(luzDireccional); escena.remove(luzPunto);
      if (luzDireccional.shadow && luzDireccional.shadow.map) { luzDireccional.shadow.map.dispose(); }

      motorDeDibujo.dispose();
      if (motorDeDibujo.forceContextLoss) { motorDeDibujo.forceContextLoss(); }
      recursosVivos.contextos--;

      if (lienzo.parentNode) { lienzo.parentNode.removeChild(lienzo); }
      oyentesDeSeleccion = [];
      return true;
    }

    return {
      cargar: cargar,
      seleccionar: seleccionar,
      ajustar: ajustar,
      liberar: liberar,
      /* Prender y apagar cada movimiento, por separado. Ninguno afecta la
         disposición de las piezas, que sale del índice y no del tiempo. */
      orbitar: function (v) { orbitaCamara = !!v; },
      orbita: function () { return orbitaCamara; },
      girarFiguras: function (v) {
        giroDeFiguras = !!v;
        /* Al apagarlo las piezas vuelven a su orientación de partida, para que
           dos personas que apagan el giro vean exactamente lo mismo. */
        if (!giroDeFiguras) {
          piezas.forEach(function (p) { p.grupo.rotation.y = 0; });
        }
      },
      giraFiguras: function () { return giroDeFiguras; },
      viva: function () { return viva; },
      seleccion: function () { return seleccion; },
      alElegirPieza: function (f) { oyentesDeSeleccion.push(f); },
      elemento: elemento
    };
  }

  /* ========================================================================== */

  global.GeometriaFactoryVisor = {
    interpretar: interpretar,
    describirPieza: describirPieza,
    disponerPorIndice: disponerPorIndice,
    hayCapacidadGrafica: hayCapacidadGrafica,
    crear: crear,
    tiposDibujables: TIPOS_DIBUJABLES.slice(),
    /* Lo que permite decir si `destruir` liberó de verdad, sin abrir la escena. */
    recursos: function () {
      return { geometrias: recursosVivos.geometrias,
               materiales: recursosVivos.materiales,
               contextos: recursosVivos.contextos };
    }
  };

})(window);
