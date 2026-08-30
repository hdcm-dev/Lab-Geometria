// ============================================================================
// Conserva la preferencia de cada movimiento. LA FACHADA NO.
//
// ES LA LÍNEA DIVISORIA DE `G-2` Y `G-3`: la fachada no consulta la preferencia de
// movimiento reducido del sistema y no conserva la elección — la recibe y la
// ejerce—. Por eso este anfitrión puede prender los dos movimientos aunque el
// entorno declare esa preferencia, y por eso la medición de cero red puede
// hacerse con el bucle de dibujo CORRIENDO. Sin esta propiedad, `[13]` quedaría
// en verde sin haber ejercitado nunca el bucle.
//
// Y ES TAMBIÉN POR QUÉ EL ANFITRIÓN GUARDA LOS DOS VALORES: `setMotion` recibe el
// par completo, así que «gobernar uno sin tocar el otro» sólo puede significar que
// quien recuerda el otro es de este lado.
// ============================================================================
(function () {
  'use strict';

  var fachada = window.GeometriaFactoryViewer;

  window.anfitrion = {
    identificador: '',
    trabajo: '',
    ultimo: { drawn: [], undrawn: [] },

    // LA PREFERENCIA VIVE ACÁ. El estado inicial es el que la pieza pública
    // decidiría consultando la preferencia del sistema; el sample lo fija en
    // apagado porque es lo que `[3]` mide con las opciones ausentes.
    preferencia: { cameraOrbit: false, pieceSpin: false },

    inicializar: function () {
      this.identificador = fachada.initialize(document.getElementById('escena'));
      var yo = this;
      document.getElementById('orbita').addEventListener('change', function (e) {
        yo.gobernar({ cameraOrbit: e.target.checked });
      });
      document.getElementById('giro').addEventListener('change', function (e) {
        yo.gobernar({ pieceSpin: e.target.checked });
      });
      return this.identificador;
    },

    cargar: function (trabajo) {
      this.trabajo = trabajo;
      this.ultimo = fachada.loadPieces(this.identificador, window.TRABAJOS[trabajo]);
      this.pintarArbol();
      return this.ultimo;
    },

    pintarArbol: function () {
      var arbol = document.getElementById('arbol');
      var motivos = {};
      this.ultimo.undrawn.forEach(function (u) { motivos[u.position] = u.reason; });
      arbol.innerHTML = '';
      window.TRABAJOS[this.trabajo].forEach(function (pieza) {
        var fila = document.createElement('li');
        fila.dataset.posicion = String(pieza.position);
        fila.textContent = '[' + pieza.position + '] ' + pieza.type
          + (motivos[pieza.position] ? ' — ' + motivos[pieza.position] : '');
        arbol.appendChild(fila);
      });
    },

    seleccionar: function (i) { fachada.selectPiece(this.identificador, i); },
    ajustar: function () { fachada.resize(this.identificador); },

    // GOBERNAR UNO NO TOCA AL OTRO, y quien lo garantiza es este renglón: se parte
    // de la preferencia guardada y se cambia sólo lo nombrado.
    gobernar: function (cambio) {
      if (cambio.cameraOrbit !== undefined) this.preferencia.cameraOrbit = cambio.cameraOrbit;
      if (cambio.pieceSpin !== undefined) this.preferencia.pieceSpin = cambio.pieceSpin;
      fachada.setMotion(this.identificador, {
        cameraOrbit: this.preferencia.cameraOrbit,
        pieceSpin: this.preferencia.pieceSpin,
      });
      document.getElementById('orbita').checked = this.preferencia.cameraOrbit;
      document.getElementById('giro').checked = this.preferencia.pieceSpin;
      return { orbita: this.preferencia.cameraOrbit, giro: this.preferencia.pieceSpin };
    },

    destruir: function () {
      fachada.destroy(this.identificador);
      this.identificador = '';
      return fachada.liveInstanceCount();
    },

    /** Ir a otro trabajo y volver: crear, cargar y liberar, que es lo que `PT-02` mide. */
    recorrer: function (trabajo) {
      this.identificador = fachada.initialize(document.getElementById('escena'));
      var r = this.cargar(trabajo);
      fachada.destroy(this.identificador);
      this.identificador = '';
      return { dibujadas: r.drawn.length, vivas: fachada.liveInstanceCount() };
    },
  };

  window.anfitrionListo = true;
})();
