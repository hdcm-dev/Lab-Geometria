// ============================================================================
// El componente anfitrión mínimo: invoca las funciones de la fachada y nada más.
//
// QUÉ NO HACE, Y ES LA MITAD DE LO QUE EL SAMPLE MUESTRA: no interpreta el texto
// del alumno, no valida el trabajo, no recalcula valores y no pide nada por red.
// Recibe las piezas ya reconstruidas —`ADR-08006`— y se las entrega al visor.
//
// GUION CLÁSICO Y NO MÓDULO, por lo mismo que las piezas entran por etiqueta: una
// página abierta directamente desde el disco no puede cargar módulos, y §4 pide
// abrirla así. Se probó con módulo y no arranca.
//
// SE EXPONE EN `window` PARA QUE EL CONDUCTOR LO MANEJE. No es superficie del
// producto: es la forma de que el recorrido de `tests/` ejerza las mismas
// llamadas que una persona haría a mano, y no una copia suya.
// ============================================================================
(function () {
  'use strict';

  var fachada = window.GeometriaFactoryViewer;

  window.anfitrion = {
    identificador: '',
    piezas: window.PIEZAS_E1,

    inicializar: function () {
      this.identificador = fachada.initialize(document.getElementById('escena'));
      return { identificador: this.identificador, vivas: fachada.liveInstanceCount() };
    },

    cargarPiezas: function () {
      return fachada.loadPieces(this.identificador, this.piezas);
    },

    destruir: function () {
      fachada.destroy(this.identificador);
      return { vivas: fachada.liveInstanceCount() };
    },

    // DEVUELVE Y NO LANZA, que es lo que la fachada promete: un anfitrión que
    // pasa un identificador viejo tiene que poder seguir. El motivo viaja por
    // la consola, y el conductor lo escucha.
    usarLiberado: function () {
      return fachada.loadPieces(this.identificador, this.piezas);
    },

    /** Los tipos de las piezas que se le entregaron, para el recuento de `[3]`. */
    tiposEntregados: function () {
      return this.piezas.map(function (p) { return p.type; });
    },
  };

  window.anfitrionListo = true;
})();
