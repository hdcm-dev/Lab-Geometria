// ============================================================================
// Sincroniza el árbol con la escena POR ÍNDICE.
//
// EL ÁRBOL ES DEL ANFITRIÓN. El visor devuelve `drawn` y `undrawn` —con motivo— y
// nada más; la presentación es de acá. Es la frontera que más fácil se cruza sin
// darse cuenta, y el sample la hace explícita.
//
// POR ÍNDICE Y NO POR IDENTIDAD: la posición de la pieza es la clave que las dos
// partes comparten, y es la misma que `seleccionarPieza` recibe.
// ============================================================================
(function () {
  'use strict';

  var fachada = window.GeometriaFactoryViewer;

  window.anfitrion = {
    identificador: '',
    escenario: '',
    ultimo: { drawn: [], undrawn: [] },

    inicializar: function () {
      this.identificador = fachada.initialize(document.getElementById('escena'));
      var selector = document.getElementById('escenario');
      Object.keys(window.PIEZAS).forEach(function (nombre) {
        var opcion = document.createElement('option');
        opcion.value = nombre;
        opcion.textContent = nombre;
        selector.appendChild(opcion);
      });
      return this.identificador;
    },

    cargar: function (escenario) {
      this.escenario = escenario;
      this.ultimo = fachada.loadPieces(this.identificador, window.PIEZAS[escenario]);
      this.pintarArbol();
      return this.ultimo;
    },

    // NINGUNA PIEZA DESAPARECE DEL ÁRBOL. Las no dibujadas aparecen con su motivo, que
    // es la garantía por la que este visor existe: el visualizador previo las perdía en
    // silencio y la persona veía menos figuras de las que había pegado.
    pintarArbol: function () {
      var arbol = document.getElementById('arbol');
      var piezas = window.PIEZAS[this.escenario];
      var motivos = {};
      this.ultimo.undrawn.forEach(function (u) { motivos[u.position] = u.reason; });

      arbol.innerHTML = '';
      piezas.forEach(function (pieza) {
        var fila = document.createElement('li');
        fila.dataset.posicion = String(pieza.position);
        var motivo = motivos[pieza.position];
        fila.textContent = '[' + pieza.position + '] ' + pieza.type + (motivo ? ' — ' + motivo : '');
        if (motivo) fila.className = 'no-dibujada';
        arbol.appendChild(fila);
      });
    },

    seleccionar: function (indice) {
      fachada.selectPiece(this.identificador, indice);
      // El árbol se marca acá: el visor resalta en la escena y avisa, pero no decide
      // qué se marca afuera.
      var filas = document.querySelectorAll('#arbol li');
      for (var i = 0; i < filas.length; i += 1) {
        filas[i].classList.toggle('resaltada', filas[i].dataset.posicion === String(indice));
      }
    },

    redimensionar: function () { fachada.resize(this.identificador); },

    tamanoDeLaSuperficie: function () {
      var lienzo = document.querySelector('#escena canvas');
      return lienzo === null ? null : { ancho: lienzo.width, alto: lienzo.height };
    },

    /** Las medidas propias del ortoedro, leídas de sus componentes. */
    medidasDelOrtoedro: function (escenario) {
      var pieza = window.PIEZAS[escenario].find(function (p) { return p.type === 'Orthohedron'; });
      if (pieza === undefined) return null;
      var base = pieza.components.find(function (c) { return c.role === 'Base'; });
      var lateral = pieza.components.find(function (c) { return c.role === 'Lateral'; });
      if (base === undefined || lateral === undefined) return null;
      return { ancho: base.declaredLength, profundidad: base.declaredWidth, altura: lateral.declaredWidth };
    },

    tipos: function (escenario) {
      return window.PIEZAS[escenario].map(function (p) { return p.type; });
    },
  };

  window.anfitrionListo = true;
})();
