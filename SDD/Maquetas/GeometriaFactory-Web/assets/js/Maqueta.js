/* ============================================================================
   Maqueta.js — Render, navegación y conmutación de estados
   Fase B2 · GeometriaFactory-Web

   Este archivo NO contiene datos de ejemplo: los lee de Datos-Maqueta.js.
   Contiene el armazón (los dos shells), el sello de versión, la barra de
   validación con su recarga automática, el render de las colecciones, la
   representación del área de dibujo y la sincronización con el árbol.

   La barra de validación y la recarga automática son instrumentos de la
   maqueta y no forman parte del producto (Maqueta-Rules.md §4.3).
   ============================================================================ */

(function (global) {
  'use strict';

  var D = global.DatosMaqueta;

  /* ==========================================================================
     Sello de la maqueta (Maqueta-Rules.md §4.6)
     ========================================================================== */

  var SELLO_MAQUETA = {
    proyectoDeCodigo: 'GeometriaFactory-Web',
    modelo: 'Catálogo base — Design-Rules-Web-Generico + Primer-Arranque + Identidad-De-Version',
    fechaIteracion: '2026-08-09',
    /* Ronda de corrección posterior a la aprobación: cierra los hallazgos de
       `SDD/Docs/Audit/B2-Maqueta-GeometriaFactory-Web-r1.md`. No es una
       iteración de validación con el Product Owner. */
    revision: 'r1-correccion-de-auditoria'
  };

  /* ==========================================================================
     Iconografía: SVG inline con currentColor (base §6.1). Grilla de 24,
     trazo 1.75. Prohibido el raster y los packs por CDN.
     ========================================================================== */

  var ICONOS = {
    laboratorio: '<path d="M9 3v6.5L4.2 18a2 2 0 0 0 1.7 3h12.2a2 2 0 0 0 1.7-3L15 9.5V3"/><path d="M8 3h8"/><path d="M7.5 15h9"/>',
    trabajos:    '<path d="M4 5.5A1.5 1.5 0 0 1 5.5 4H10l2 2.5h6.5A1.5 1.5 0 0 1 20 8v10a1.5 1.5 0 0 1-1.5 1.5h-13A1.5 1.5 0 0 1 4 18z"/>',
    nuevo:       '<path d="M12 5v14"/><path d="M5 12h14"/>',
    llave:       '<circle cx="8" cy="12" r="3.5"/><path d="M11.5 12H20"/><path d="M17 12v3"/><path d="M20 12v2.5"/>',
    cuentas:     '<circle cx="9" cy="8.5" r="3.5"/><path d="M3 20a6 6 0 0 1 12 0"/><path d="M16 5.5a3.5 3.5 0 0 1 0 6.8"/><path d="M17.5 20a6 6 0 0 0-2.2-4.6"/>',
    comision:    '<path d="M4 6h16"/><path d="M4 12h16"/><path d="M4 18h16"/>',
    salir:       '<path d="M14 4H6.5A1.5 1.5 0 0 0 5 5.5v13A1.5 1.5 0 0 0 6.5 20H14"/><path d="M17 8l4 4-4 4"/><path d="M21 12H10"/>',
    buscar:      '<circle cx="10.5" cy="10.5" r="6"/><path d="M15 15l4.5 4.5"/>',
    abrir:       '<path d="M14 4h6v6"/><path d="M20 4l-8.5 8.5"/><path d="M18 14v5.5A1.5 1.5 0 0 1 16.5 21h-11A1.5 1.5 0 0 1 4 19.5v-11A1.5 1.5 0 0 1 5.5 7H11"/>',
    editar:      '<path d="M4 20h4L20 8l-4-4L4 16z"/>',
    eliminar:    '<path d="M4.5 7h15"/><path d="M9.5 7V4.5h5V7"/><path d="M6.5 7l1 13h9l1-13"/>',
    volver:      '<path d="M11 5l-7 7 7 7"/><path d="M4 12h16"/>',
    aprobar:     '<path d="M4.5 12.5l5 5 10-11"/>',
    rechazar:    '<path d="M6 6l12 12"/><path d="M18 6L6 18"/>',
    alerta:      '<path d="M12 3.5L1.8 20.5h20.4z"/><path d="M12 10v4.5"/><path d="M12 17.6v.1"/>',
    reintentar:  '<path d="M20 6v5h-5"/><path d="M19.5 11A8 8 0 1 0 18 16.5"/>',
    copiar:      '<rect x="8.5" y="8.5" width="11" height="11" rx="1.6"/><path d="M15.5 5.5H6A1.5 1.5 0 0 0 4.5 7v9.5"/>',
    vacio:       '<path d="M4 8.5A2.5 2.5 0 0 1 6.5 6h4l2 3h5A2.5 2.5 0 0 1 20 11.5v5A2.5 2.5 0 0 1 17.5 19h-11A2.5 2.5 0 0 1 4 16.5z"/><path d="M8 13.5h8"/>',
    cubo:        '<path d="M12 3l8 4.5v9L12 21l-8-4.5v-9z"/><path d="M4 7.5l8 4.5 8-4.5"/><path d="M12 12v9"/>',
    marca:       '<path d="M12 2.5L21 8v8l-9 5.5L3 16V8z"/><path d="M12 8.5L16.5 11v4L12 17.5 7.5 15v-4z"/>'
  };

  function icono(nombre, clase) {
    var d = ICONOS[nombre];
    if (!d) { return ''; }
    return '<svg class="mq-ico ' + (clase || 'mq-ico--16') + '" viewBox="0 0 24 24" fill="none" ' +
           'stroke="currentColor" stroke-width="1.75" stroke-linecap="round" ' +
           'stroke-linejoin="round" aria-hidden="true" focusable="false">' + d + '</svg>';
  }

  /* ==========================================================================
     Utilidades
     ========================================================================== */

  function esc(s) {
    return String(s == null ? '' : s)
      .replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;')
      .replace(/"/g, '&quot;');
  }
  function $(sel, raiz) { return (raiz || document).querySelector(sel); }
  function $$(sel, raiz) { return Array.prototype.slice.call((raiz || document).querySelectorAll(sel)); }
  function crear(html) { var d = document.createElement('div'); d.innerHTML = html.trim(); return d.firstChild; }

  /* Los recuentos se anuncian con su unidad —«3 piezas», «1 advertencia»— y
     no como números sueltos. */
  function plural(n, singular, pluralForma) {
    return n + ' ' + (n === 1 ? singular : pluralForma);
  }

  function insignia(estado) {
    var m = D.insigniasTrabajo[estado] || D.insigniasCuenta[estado] || { clase: 'mq-insignia--neutro' };
    return '<span class="mq-insignia ' + m.clase + '">' + esc(estado) + '</span>';
  }

  /* ==========================================================================
     Navegación entre superficies (Experiencia-De-Uso.md §3.2 a §3.7)

     La maqueta lleva el contexto entre superficies por la dirección, con el
     mismo mecanismo que ya usa para fijar estados:
       ?t=<id>        qué trabajo se abre
       ?papel=        con qué papel se lo mira: alumno o administrador
       ?desde=        de dónde vino la persona, para que «volver» vuelva ahí
     Ninguna ruta se inventa: son las que declara Experiencia-De-Uso §3.1.
     ========================================================================== */

  function parametro(nombre) {
    return new URLSearchParams(location.search).get(nombre);
  }

  function papelVigente() {
    var p = parametro('papel');
    if (p === 'alumno' || p === 'administrador') { return p; }
    return document.body.dataset.papel || 'alumno';
  }

  /* La ruta inicial de cada papel, de Experiencia-De-Uso §3.2. */
  function listadoDelPapel(papel) {
    return papel === 'administrador'
      ? 'Listado-De-La-Comision.html'
      : 'Panel-De-Trabajos-Del-Alumno.html';
  }

  /* «Volver al listado» lleva al listado del papel de quien mira: el propio si
     es el alumno, el de la comisión si es el administrador. Si la persona vino
     del envío, vuelve al listado propio, que es donde el trabajo ya figura. */
  function destinoDeRegreso(papel) {
    var desde = parametro('desde');
    if (desde === 'comision')     { return 'Listado-De-La-Comision.html'; }
    if (desde === 'mis-trabajos') { return 'Panel-De-Trabajos-Del-Alumno.html'; }
    if (desde === 'envio')        { return 'Panel-De-Trabajos-Del-Alumno.html'; }
    return listadoDelPapel(papel);
  }

  function rotuloDeRegreso(papel) {
    var desde = parametro('desde');
    if (desde === 'comision' || (!desde && papel === 'administrador')) {
      return 'Volver a la entrega de la comisión';
    }
    return 'Volver a mis trabajos';
  }

  /* Dirección de la vista de un trabajo, con su contexto de navegación. */
  function hrefTrabajo(id, papel, desde) {
    return 'Vista-De-Trabajo.html?t=' + encodeURIComponent(id) +
           '&papel=' + encodeURIComponent(papel) +
           (desde ? '&desde=' + encodeURIComponent(desde) : '');
  }

  /* El trabajo que la dirección pide, con un valor de partida si no viene. */
  function trabajoVigente(porDefecto) {
    return D.trabajo(parametro('t')) || D.trabajo(porDefecto);
  }

  /* ==========================================================================
     Los dos shells (Experiencia-De-Uso.md §3.2)
     ========================================================================== */

  var DESTINOS = {
    alumno: [
      { id: 'mis-trabajos', rotulo: 'Mis trabajos',  href: 'Panel-De-Trabajos-Del-Alumno.html', ico: 'trabajos' },
      { id: 'trabajo-nuevo', rotulo: 'Trabajo nuevo', href: 'Envio-De-Trabajo.html',            ico: 'nuevo' },
      { id: 'mi-contrasena', rotulo: 'Mi contraseña', href: 'Credencial-Propia.html',           ico: 'llave' }
    ],
    administrador: [
      { id: 'entrega-comision', rotulo: 'Entrega de la comisión', href: 'Listado-De-La-Comision.html', ico: 'comision' },
      { id: 'cuentas',          rotulo: 'Cuentas',                href: 'Panel-De-Cuentas.html',       ico: 'cuentas' },
      { id: 'mi-contrasena',    rotulo: 'Mi contraseña',          href: 'Credencial-Propia.html',      ico: 'llave' }
    ]
  };

  function personaDe(papel) {
    if (papel === 'administrador') { return D.cuenta('C-ADMIN'); }
    return D.cuenta('C-1');
  }

  function pintarBarraLateral(nav, papel, destinoActivo) {
    var persona = personaDe(papel);
    var nombre = (persona.nombre + ' ' + persona.apellido).trim();
    var items = DESTINOS[papel] || DESTINOS.alumno;

    nav.innerHTML =
      '<span class="mq-identidad">' + icono('marca', 'mq-ico--24') + esc(D.textos.producto) + '</span>' +
      '<ul class="mq-nav mq-lista-plana--sin-hueco">' +
        items.map(function (i) {
          var activo = i.id === destinoActivo ? ' aria-current="page"' : '';
          // «Mi contraseña» desde el shell de trabajo es el curso de cambio, y
          // conserva el papel para que la barra lateral siga siendo la correcta.
          var href = i.id === 'mi-contrasena'
            ? i.href + '?papel=' + papel + '&estado=curso-cambio'
            : i.href;
          return '<li><a href="' + href + '"' + activo + '>' + icono(i.ico, 'mq-ico--20') + esc(i.rotulo) + '</a></li>';
        }).join('') +
      '</ul>' +
      '<div class="mq-sidebar-pie">' +
        '<span class="mq-sidebar-persona">' + esc(nombre) + '</span>' +
        '<span class="mq-sidebar-papel">' + (papel === 'administrador' ? 'Administrador' : 'Alumno') + '</span>' +
        '<a class="mq-btn mq-btn--secundario" href="Ingreso.html?estado=sesion-cerrada">' +
          icono('salir') + 'Cerrar sesión</a>' +
        selloDeVersionHTML('barra lateral') +
      '</div>';
  }

  /* ==========================================================================
     Sello de versión y detalle de diagnóstico
     (Representacion-Sello-De-Version.md §2 a §5)
     ========================================================================== */

  var varianteVersion = 'publicada';

  function contratoVersionVigente() {
    return D.identidadDeVersion.variantes[varianteVersion] || D.identidadDeVersion.variantes.publicada;
  }

  function selloDeVersionHTML(ubicacion) {
    var v = contratoVersionVigente();
    var id = 'mq-diag-' + ubicacion.replace(/\s+/g, '-');
    return '<div class="mq-sello" data-mq-sello>' +
      '<button type="button" class="mq-sello-boton" aria-expanded="false" aria-controls="' + id + '">' +
        esc(v.versionLegible) +
      '</button>' +
      (v.esPreliminar ? '<span class="mq-insignia mq-insignia--atencion">preliminar</span>' : '') +
      '<div class="mq-diagnostico" id="' + id + '" hidden>' +
        '<dl class="mq-clave-valor">' +
          '<dt>Versión</dt><dd>' + esc(v.versionLegible) + '</dd>' +
          '<dt>Construcción</dt><dd>' + esc(v.identificadorDeConstruccion) + '</dd>' +
          '<dt>Origen</dt><dd>' + esc(v.origen) + '</dd>' +
        '</dl>' +
        '<button type="button" class="mq-btn mq-btn--secundario mq-mt-2" data-mq-copiar>' +
          icono('copiar') + 'Copiar para reportar</button>' +
        '<p class="mq-sr-only" role="status" data-mq-copiado></p>' +
      '</div>' +
    '</div>';
  }

  function activarSellos(raiz) {
    $$('[data-mq-sello]', raiz).forEach(function (sello) {
      var boton = $('.mq-sello-boton', sello);
      var panel = $('.mq-diagnostico', sello);
      boton.addEventListener('click', function () {
        var abierto = boton.getAttribute('aria-expanded') === 'true';
        boton.setAttribute('aria-expanded', String(!abierto));
        panel.hidden = abierto;
      });
      var copiar = $('[data-mq-copiar]', sello);
      var acuse = $('[data-mq-copiado]', sello);
      if (copiar) {
        copiar.addEventListener('click', function () {
          var v = contratoVersionVigente();
          // La cadena que se muestra y la que se copia son la misma.
          var bloque = v.versionLegible + '\nConstrucción: ' + v.identificadorDeConstruccion +
                       '\nOrigen: ' + v.origen;
          if (navigator.clipboard && navigator.clipboard.writeText) {
            navigator.clipboard.writeText(bloque);
          }
          acuse.textContent = 'Diagnóstico copiado y listo para pegar.';
        });
      }
    });
  }

  function repintarSellos() {
    $$('[data-mq-sello]').forEach(function (sello) {
      var contenedor = sello.parentNode;
      var nuevo = crear(selloDeVersionHTML('r' + Math.random().toString(36).slice(2, 7)));
      contenedor.replaceChild(nuevo, sello);
      activarSellos(contenedor);
    });
  }

  /* ==========================================================================
     Conmutación de estados
     Un bloque con data-mq-estado="a b" se muestra sólo cuando el estado en
     curso es `a` o `b`. Un bloque sin el atributo se muestra siempre.
     Para agregar un estado a mano: agregá el bloque con su data-mq-estado y,
     si querés un rótulo lindo en la barra, agregá su entrada acá abajo.
     ========================================================================== */

  var ROTULOS_DE_ESTADO = {
    'con-datos': 'Con datos',
    'vacio': 'Vacío (colección con cero elementos)',
    'cargando': 'Cargando',
    'enviando': 'Enviando',
    'error-entrada': 'Error de entrada · requisito no cumplido',
    'error-operacion': 'Error de operación',
    'exito': 'Éxito',
    'indisponible': 'Indisponible (el servicio de datos no responde)',
    'indisponible-conservado': 'Indisponible, con lo escrito conservado',
    'reconectando': 'Reconectando (se cortó el circuito)',
    'resolviendo-destino': 'Resolviendo destino',
    'ya-aprovisionado': 'Ya aprovisionado (redirección neutra)',
    'confirmacion-no-coincidente': 'Confirmación no coincidente',
    'cuenta-no-habilitada': 'Cuenta no habilitada',
    'contrasena-sin-establecer': 'Contraseña sin establecer',
    'sesion-cerrada': 'Sesión cerrada',
    'sesion-no-restablecible': 'Sesión vencida o no restablecible',
    'confirmacion-aprovisionamiento': 'Confirmación de aprovisionamiento',
    'confirmacion-registro': 'Confirmación de registro',
    'confirmacion-contrasena': 'Confirmación de contraseña establecida',
    'credencial-rechazada': 'Credencial rechazada',
    'curso-establecimiento': 'Curso de establecimiento',
    'curso-cambio': 'Curso de cambio',
    'actual-rechazada': 'Contraseña actual rechazada',
    'filtrado-sin-resultados': 'Filtrado sin resultados',
    'confirmando-eliminacion': 'Confirmando eliminación',
    'correo-ya-registrado': 'Error de operación · correo ya registrado',
    'previsualizado': 'Previsualizado',
    'verifico': 'Verificó (pasa a Pendiente)',
    'verifico-con-advertencias': 'Verificó con advertencias',
    'no-verifico': 'No verificó (queda en Borrador)',
    'sin-observaciones': 'Sin observaciones',
    'sin-comentario': 'Sin comentario',
    'con-comentario': 'Con comentario',
    'borrador-con-errores': 'Trabajo en borrador con errores',
    'trabajo-ajeno': 'Trabajo ajeno o inexistente',
    'resoluble': 'Con datos · resoluble (administrador, trabajo Pendiente)',
    'no-resoluble-papel': 'Con datos · no resoluble por papel (mira el alumno)',
    'no-resoluble-estado': 'Con datos · no resoluble por estado (ya resuelto)',
    'confirmando-desenlace': 'Confirmando desenlace',
    'confirmando-retiro': 'Confirmando retiro',
    'aplicando': 'Aplicando el desenlace',
    'orientacion-posterior': 'Orientación posterior al aprovisionamiento',
    'confirmacion-escrita-pendiente': 'Confirmación escrita pendiente',
    'aplicando-situacion': 'Aplicando un cambio de situación',
    'cuenta-inexistente': 'Error de operación · cuenta inexistente',
    'administrador-ya-configurado': 'Error de operación · administrador ya configurado',
    'grupo-colapsado': 'Grupo colapsado',
    'cero-borradores': 'Cero borradores (permanente)',
    'resumen-abierto': 'Resumen abierto',
    'alumno-filtro-inexistente': 'Alumno del filtro inexistente',
    'recuperado': 'Recuperado tras el reintento',
    'fallo-no-clasificado': 'Fallo no clasificado',
    'transporte-replegado': 'Transporte replegado (ningún aviso)',
    'version-preliminar': 'Sello: versión preliminar',
    'origen-indeterminado': 'Sello: origen indeterminado',
    'eliminando': 'Eliminando',
    'exito-establecimiento': 'Éxito de establecimiento',
    'exito-cambio': 'Éxito de cambio (la sesión se conserva)',
    'cuenta-bloqueada': 'Cuenta bloqueada entre la derivación y el envío',
    'desenlace-no-administrador': 'Error · desenlace no ejercido por el administrador',
    'exito-desenlace': 'Éxito del desenlace',
    'exito-retiro': 'Éxito del retiro',
    'baja-no-coincide': 'Confirmación de baja no coincidente',
    'ejecutando-baja': 'Ejecutando la baja',
    'confirmando-baja': 'Confirmando la baja (confirmación escrita)',

    /* Las siete condiciones del contrato de fachada del visor, con el nombre
       del estado de la superficie y el código que lo produce. Los códigos son
       los de Definicion-Contrato-De-Fachada.md §6 y no se renombran. */
    'escena-no-disponible': 'Escena no disponible · CAPACIDAD_GRAFICA_AUSENTE',
    'elemento-sin-tamano': 'Elemento de dibujo sin tamaño, en creación · ELEMENTO_DE_DIBUJO_INVALIDO (C-1)',
    'elemento-sin-tamano-ajuste': 'Elemento de dibujo sin tamaño, en ajuste · ELEMENTO_DE_DIBUJO_INVALIDO (C-2)',
    'instancia-desconocida': 'Instancia desconocida · INSTANCIA_DESCONOCIDA',
    'texto-no-legible': 'Texto no legible · TEXTO_NO_LEGIBLE',
    'piezas-no-dibujadas': 'Piezas no dibujadas · TIPO_NO_DIBUJABLE',
    'dimension-no-legible': 'Pieza sin dimensión legible · DIMENSION_NO_LEGIBLE',
    'indice-sin-representacion': 'Índice sin representación en la escena · INDICE_FUERA_DE_RANGO'
  };

  function rotuloDeEstado(id) {
    if (ROTULOS_DE_ESTADO[id]) { return ROTULOS_DE_ESTADO[id]; }
    var s = id.replace(/-/g, ' ');
    return s.charAt(0).toUpperCase() + s.slice(1);
  }

  var estadoActual = null;
  var estadosDeLaSuperficie = [];

  function relevarEstados() {
    var vistos = [];
    $$('[data-mq-estado]').forEach(function (el) {
      el.getAttribute('data-mq-estado').split(/\s+/).forEach(function (id) {
        if (id && vistos.indexOf(id) === -1) { vistos.push(id); }
      });
    });
    return vistos;
  }

  function aplicarEstado(id) {
    estadoActual = id;

    /* Gancho opcional de la superficie, para lo que no se resuelve mostrando y
       ocultando bloques: vaciar los campos de un formulario, o volver a armar
       las cuatro partes con otro trabajo. Corre ANTES de la pasada de
       visibilidad, porque puede crear bloques nuevos que también hay que
       mostrar u ocultar. */
    if (typeof global.alCambiarEstado === 'function') { global.alCambiarEstado(id); }

    $$('[data-mq-estado]').forEach(function (el) {
      var lista = el.getAttribute('data-mq-estado').split(/\s+/);
      el.hidden = lista.indexOf(id) === -1;
    });

    // Cartel de reconexión y superficie inerte debajo (Estado degradado §2).
    document.body.classList.toggle('mq-inerte', id === 'reconectando');
    var cartel = $('[data-mq-cartel-reconexion]');
    if (cartel) { cartel.hidden = id !== 'reconectando'; }

    // El sello conmuta con los dos estados que lo tienen por objeto.
    var nuevaVariante = id === 'version-preliminar' ? 'preliminar'
                      : id === 'origen-indeterminado' ? 'indeterminado'
                      : 'publicada';
    if (nuevaVariante !== varianteVersion) { varianteVersion = nuevaVariante; repintarSellos(); }

    // Anuncio del cambio de estado como región activa.
    var avisador = $('[data-mq-anuncio-estado]');
    if (avisador) { avisador.textContent = 'Estado de la maqueta: ' + rotuloDeEstado(id) + '.'; }

    var selector = $('[data-mq-selector-estado]');
    if (selector && selector.value !== id) { selector.value = id; }

    try { sessionStorage.setItem('mq-estado:' + document.body.dataset.superficie, id); } catch (e) {}
  }

  /* ==========================================================================
     Barra de validación (Maqueta-Rules.md §4.3)
     Instrumento de la maqueta. No forma parte del producto.
     ========================================================================== */

  function pintarBarraDeValidacion() {
    var host = $('[data-mq-barra-validacion]');
    if (!host) { return; }

    var opciones = estadosDeLaSuperficie.map(function (id) {
      return '<option value="' + esc(id) + '">' + esc(rotuloDeEstado(id)) + '</option>';
    }).join('');

    host.innerHTML =
      '<aside class="mq-barra-validacion" aria-label="Barra de validación de maqueta">' +
        '<p class="mq-barra-validacion__rotulo">' + icono('alerta') +
          'Barra de validación de maqueta — no forma parte del producto</p>' +
        '<div class="mq-barra-validacion__fila">' +
          '<label for="mq-selector-estado">Estado de <strong>' +
            esc(document.body.dataset.superficie) + '</strong></label>' +
          '<select id="mq-selector-estado" data-mq-selector-estado>' + opciones + '</select>' +
          '<span class="mq-conmutador">' +
            '<input type="checkbox" id="mq-recarga" data-mq-recarga>' +
            '<label for="mq-recarga">Recarga automática</label>' +
          '</span>' +
          '<span class="mq-nota" data-mq-recarga-nota></span>' +
          '<a class="mq-nota" href="index.html">Volver a la portada de la maqueta</a>' +
        '</div>' +
        '<p class="mq-sr-only" role="status" data-mq-anuncio-estado></p>' +
      '</aside>';

    $('[data-mq-selector-estado]').addEventListener('change', function (ev) {
      aplicarEstado(ev.target.value);
    });

    prepararRecargaAutomatica();
  }

  /* --- Recarga automática ---------------------------------------------------
     Apagada por omisión, con su estado persistido. Detecta por comparación de
     un identificador de versión del recurso (ETag o Last-Modified), no por
     descarga completa. Intervalo de 3 s. Se suspende con la pestaña oculta.
     Sobre file:// no puede funcionar: degrada en silencio, con la razón a la
     vista y el interruptor deshabilitado.
     -------------------------------------------------------------------------- */

  function prepararRecargaAutomatica() {
    var casilla = $('[data-mq-recarga]');
    var nota = $('[data-mq-recarga-nota]');
    var CLAVE = 'mq-recarga-automatica';

    if (location.protocol === 'file:') {
      casilla.disabled = true;
      casilla.checked = false;
      nota.textContent = 'No disponible: la maqueta está abierta como archivo (file://). Servila por HTTP para usarla.';
      return;
    }

    var recursos = ['', 'assets/css/Estilos-Maqueta.css', 'assets/js/Maqueta.js', 'assets/js/Datos-Maqueta.js'];
    var huellas = {};
    var temporizador = null;

    function huellaDe(res) {
      return fetch(res || location.href, { method: 'HEAD', cache: 'no-store' })
        .then(function (r) { return r.headers.get('etag') || r.headers.get('last-modified') || ''; })
        .catch(function () { return null; });
    }

    function sondear() {
      if (document.hidden) { return; }
      recursos.forEach(function (res) {
        huellaDe(res).then(function (h) {
          if (h === null) { return; }               // degradación silenciosa
          if (huellas[res] === undefined) { huellas[res] = h; return; }
          if (huellas[res] !== h) { location.reload(); }
        });
      });
    }

    function encender() {
      recursos.forEach(function (res) {
        huellaDe(res).then(function (h) { if (h !== null) { huellas[res] = h; } });
      });
      temporizador = setInterval(sondear, 3000);
      nota.textContent = 'Encendida: consulta cada 3 s y refresca cuando un archivo cambia.';
    }
    function apagar() {
      if (temporizador) { clearInterval(temporizador); temporizador = null; }
      nota.textContent = '';
    }

    var guardado = null;
    try { guardado = localStorage.getItem(CLAVE); } catch (e) {}
    casilla.checked = guardado === '1';
    if (casilla.checked) { encender(); }

    casilla.addEventListener('change', function () {
      try { localStorage.setItem(CLAVE, casilla.checked ? '1' : '0'); } catch (e) {}
      if (casilla.checked) { encender(); } else { apagar(); }
    });

    document.addEventListener('visibilitychange', function () {
      if (document.hidden || !casilla.checked) { return; }
      sondear();
    });
  }

  /* ==========================================================================
     Área de dibujo: el visor real montado en la superficie

     Desde la iteración 3 el área de dibujo NO es una representación: monta el
     visualizador que la cátedra ya usa, portado y corregido en
     `assets/js/Visor-Tridimensional.js`. Las cinco funciones de la fachada
     operan sobre esa escena de verdad.

     La representación SVG que había antes se conserva como RESPALDO, y sólo
     para el caso en que el navegador no ofrezca capacidad gráfica
     tridimensional. No es el camino normal y se rotula como lo que es, para
     que nadie confunda el respaldo con el producto.
     ========================================================================== */

  function Visor() { return global.GeometriaFactoryVisor || null; }

  /* Interpreta el texto del alumno con el lector tolerante del visor: claves
     alternativas, comas finales y comentarios. Si el visor no está cargado,
     se cae al árbol declarado en `Datos-Maqueta.js`, que es el mismo. */
  function nodosDeTexto(texto, respaldo) {
    var V = Visor();
    if (V && typeof texto === 'string' && texto) {
      var r = V.interpretar(texto);
      if (r.ok) { return { nodos: r.nodos, condicion: null }; }
      return { nodos: [], condicion: 'TEXTO_NO_LEGIBLE' };
    }
    return { nodos: respaldo || [], condicion: null };
  }

  /* El motor vive colgado del propio elemento de dibujo: es lo que permite que
     la fachada y el render compartan una sola instancia por área. */
  var alElegirEnLaEscena = null;

  function motorDe(contenedor) {
    return (contenedor && contenedor.__mqMotor && contenedor.__mqMotor.viva())
      ? contenedor.__mqMotor : null;
  }

  /* --------------------------------------------------------------------------
     Los dos movimientos de la escena: preferencias de quien mira

     Dos casillas de verificación independientes, una por movimiento. Se tildan
     por separado y se pueden tildar las dos.

     - **Órbita de la cámara.** Es el giro del visor original: incrementa el
       ángulo horizontal y reposiciona la cámara cuando no hay arrastre y hay
       piezas; las piezas quedan quietas.
     - **Giro de las figuras.** Cada pieza rota sobre su eje vertical, en su
       lugar. ATENCIÓN: esto NO existe en el visor original. Es comportamiento
       nuevo, decidido por el Product Owner al mirar la maqueta, y está anotado
       como hallazgo H-6 en README.md §6 para el paso 6 de retroalimentación.

     Son del producto y no del instrumento, así que van junto al área de dibujo
     y no en el panel del contrato. Prendidos por omisión los dos; apagados de
     arranque si el sistema declara preferencia de movimiento reducido, y en ese
     caso el propio control lo dice. **Esa decisión es del anfitrión, no del
     bundle**: el visor arranca los dos movimientos apagados ante opciones
     ausentes (§4.1 del contrato) y esta página le manda siempre los dos valores
     de verdad ya resueltos.

     Las preferencias se persisten con el prefijo `mq-`, que es la convención de
     claves del instrumento de la maqueta. Importa: el panel mide «cero
     persistencia» contando las claves que NO empiezan con `mq-`, así que una
     clave sin prefijo haría fallar esa medición.

     Ninguno de los dos toca la POSICIÓN de las piezas, que sale del índice: uno
     mueve la cámara y el otro la orientación. Lo que la especificación exige
     determinista es dónde cae cada pieza, no cómo está rotada en un instante,
     y por eso la firma de disposición no incluye la orientación.
     -------------------------------------------------------------------------- */

  var MOVIMIENTOS = [
    { campo: 'orbita', clave: 'mq-orbita-de-camara',
      rotulo: 'Órbita de la cámara',
      ayuda: 'La cámara gira sola alrededor del conjunto; las piezas quedan quietas. ' +
             'Se detiene mientras arrastrás.' },
    { campo: 'figuras', clave: 'mq-giro-de-figuras',
      rotulo: 'Giro de las figuras',
      ayuda: 'Cada pieza rota sobre su eje vertical, en su lugar. ' +
             'Se detiene mientras arrastrás.' }
  ];

  /* LA PREFERENCIA DEL SISTEMA LA LEE EL ANFITRIÓN, NUNCA EL VISOR. Es la
     frontera que fija `Definicion-Contrato-De-Fachada.md` §3.3: la fachada no
     dibuja el control, no conserva la elección y no consulta configuración
     propia —hacerlo violaría G-3—. Esta página es el componente anfitrión: acá
     se consulta `prefers-reduced-motion`, acá se persiste la elección con
     claves `mq-`, y al bundle le llegan **dos valores de verdad**, uno por
     movimiento, por `crear(elemento, opciones)` y después por `orbitar()` y
     `girarFiguras()`. */
  function movimientoReducido() {
    try { return !!global.matchMedia('(prefers-reduced-motion: reduce)').matches; }
    catch (e) { return false; }
  }

  function preferenciaDeMovimiento(mov) {
    var guardado = null;
    try { guardado = localStorage.getItem(mov.clave); } catch (e) {}
    if (guardado === '1') { return true; }
    if (guardado === '0') { return false; }
    // Sin elección previa: prendido, salvo que el sistema pida lo contrario.
    return !movimientoReducido();
  }

  var secuenciaDeMovimiento = 0;

  /* Las dos casillas, dentro del área de dibujo. Se arman una sola vez por área
     y sobreviven a las recargas de escena. */
  function controlesDeMovimiento(contenedor) {
    var caja = $('[data-mq-movimiento]', contenedor);
    if (caja) { return caja; }

    caja = crear(
      '<div class="mq-escena-movimiento" data-mq-movimiento role="group" ' +
           'aria-label="Movimiento automático de la escena">' +
        MOVIMIENTOS.map(function (m) {
          var id = 'mq-mov-' + (++secuenciaDeMovimiento);
          /* La ayuda va en un elemento propio asociado por `aria-describedby`,
             no sólo en `title`: el texto de `title` no llega ni al teclado ni a
             buena parte de los lectores de pantalla. */
          return '<span class="mq-escena-movimiento__opcion">' +
            '<input type="checkbox" id="' + id + '" data-mq-mov="' + m.campo + '" ' +
                   'aria-describedby="' + id + '-ayuda">' +
            '<label for="' + id + '">' + esc(m.rotulo) + '</label>' +
            '<span class="mq-sr-only" id="' + id + '-ayuda">' + esc(m.ayuda) + '</span>' +
          '</span>';
        }).join('') +
        (movimientoReducido()
          ? '<span class="mq-escena-movimiento__nota">arrancan apagados: tu sistema pide ' +
            'movimiento reducido</span>'
          : '') +
        '<span class="mq-sr-only" role="status" data-mq-mov-acuse></span>' +
      '</div>');
    contenedor.appendChild(caja);

    var acuse = $('[data-mq-mov-acuse]', caja);

    MOVIMIENTOS.forEach(function (m) {
      var casilla = $('[data-mq-mov="' + m.campo + '"]', caja);
      casilla.checked = preferenciaDeMovimiento(m);
      casilla.addEventListener('change', function () {
        try { localStorage.setItem(m.clave, casilla.checked ? '1' : '0'); } catch (e) {}
        aplicarMovimientos(contenedor);
        acuse.textContent = m.rotulo + (casilla.checked ? ': activado. ' + m.ayuda
                                                        : ': desactivado. Ese movimiento se detiene.');
      });
    });

    return caja;
  }

  /* Traslada lo que dicen las dos casillas al motor vigente del área. */
  function aplicarMovimientos(contenedor) {
    var m = motorDe(contenedor);
    if (!m) { return; }
    var caja = $('[data-mq-movimiento]', contenedor);
    function tildada(campo, porDefecto) {
      var c = caja ? $('[data-mq-mov="' + campo + '"]', caja) : null;
      return c ? c.checked : porDefecto;
    }
    m.orbitar(tildada('orbita', preferenciaDeMovimiento(MOVIMIENTOS[0])));
    m.girarFiguras(tildada('figuras', preferenciaDeMovimiento(MOVIMIENTOS[1])));
  }

  /* Las casillas sólo tienen sentido con escena real: la representación de
     respaldo no se mueve. */
  function mostrarControlesDeMovimiento(contenedor, visible) {
    var caja = visible ? controlesDeMovimiento(contenedor) : $('[data-mq-movimiento]', contenedor);
    if (caja) { caja.hidden = !visible; }
    if (visible) { aplicarMovimientos(contenedor); }
  }

  function crearMotor(contenedor) {
    var V = Visor();
    if (!V) { return null; }
    if (motorDe(contenedor)) { liberarMotor(contenedor); }
    /* La instancia nueva nace con las preferencias vigentes: quien apagó un
       movimiento no lo tiene que volver a apagar al abrir otro trabajo. */
    var m = V.crear(contenedor, {
      orbitaCamara:  preferenciaDeMovimiento(MOVIMIENTOS[0]),
      giroDeFiguras: preferenciaDeMovimiento(MOVIMIENTOS[1])
    });
    contenedor.__mqMotor = m;
    if (m) {
      m.alElegirPieza(function (indice) {
        if (alElegirEnLaEscena) { alElegirEnLaEscena(indice, 'escena'); }
      });
    }
    return m;
  }

  function liberarMotor(contenedor) {
    var m = contenedor && contenedor.__mqMotor;
    if (m) { m.liberar(); contenedor.__mqMotor = null; }
    return !!m;
  }

  /* Cromo del área de dibujo: rótulo y leyenda. Se crean una sola vez, porque
     el lienzo del visor es hijo del mismo contenedor y reescribir el HTML
     entero lo arrancaría de la página. */
  function cromoDeEscena(contenedor) {
    var rotulo = $('[data-mq-escena-rotulo]', contenedor);
    if (!rotulo) {
      rotulo = crear('<span class="mq-escena-rotulo" data-mq-escena-rotulo></span>');
      contenedor.appendChild(rotulo);
    }
    var leyenda = $('[data-mq-escena-leyenda]', contenedor);
    if (!leyenda) {
      leyenda = crear('<p class="mq-escena-leyenda" data-mq-escena-leyenda></p>');
      contenedor.appendChild(leyenda);
    }
    return { rotulo: rotulo, leyenda: leyenda };
  }

  function quitarRespaldoSvg(contenedor) {
    var svg = $('svg', contenedor);
    if (svg) { svg.parentNode.removeChild(svg); }
  }

  /* Cada forma del RESPALDO se dibuja con su base apoyada en la línea de suelo. */
  var FORMAS = {
    cubo: function (x, y0, s) {
      var h = s * 0.42, y = y0 - 2 * h;
      return '<path class="mq-pieza-cuerpo" d="M' + x + ' ' + (y - s) + ' l' + s + ' ' + h +
             ' l0 ' + s + ' l-' + s + ' ' + h + ' l-' + s + ' -' + h + ' l0 -' + s + ' z"/>' +
             '<path class="mq-pieza-cuerpo mq-pieza-cuerpo--contorno" d="M' + (x - s) + ' ' + (y - s + h) +
             ' l' + s + ' ' + h + ' l' + s + ' -' + h + ' M' + x + ' ' + (y - s + 2 * h) + ' l0 ' + s + '"/>';
    },
    ortoedro: function (x, y0, s) {
      var h = s * 0.42, alto = s * 1.7, y = y0 - 2 * h;
      return '<path class="mq-pieza-cuerpo" d="M' + x + ' ' + (y - alto) + ' l' + s + ' ' + h +
             ' l0 ' + alto + ' l-' + s + ' ' + h + ' l-' + s + ' -' + h + ' l0 -' + alto + ' z"/>' +
             '<path class="mq-pieza-cuerpo mq-pieza-cuerpo--contorno" d="M' + (x - s) + ' ' + (y - alto + h) +
             ' l' + s + ' ' + h + ' l' + s + ' -' + h + ' M' + x + ' ' + (y - alto + 2 * h) + ' l0 ' + alto + '"/>';
    },
    cilindro: function (x, y0, s) {
      var ry = s * 0.34, alto = s * 1.5, y = y0 - ry;
      return '<path class="mq-pieza-cuerpo" d="M' + (x - s) + ' ' + (y - alto) +
             ' a' + s + ' ' + ry + ' 0 0 1 ' + (2 * s) + ' 0 l0 ' + alto +
             ' a' + s + ' ' + ry + ' 0 0 1 -' + (2 * s) + ' 0 z"/>' +
             '<ellipse class="mq-pieza-cuerpo" cx="' + x + '" cy="' + (y - alto) + '" rx="' + s + '" ry="' + ry + '"/>';
    },
    rectangulo: function (x, y, s) {
      return '<rect class="mq-pieza-cuerpo" x="' + (x - s) + '" y="' + (y - s * 1.2) + '" width="' + (2 * s) + '" height="' + (s * 1.2) + '" rx="2"/>';
    },
    cuadrado: function (x, y, s) {
      return '<rect class="mq-pieza-cuerpo" x="' + (x - s) + '" y="' + (y - 2 * s) + '" width="' + (2 * s) + '" height="' + (2 * s) + '" rx="2"/>';
    },
    circulo: function (x, y, s) {
      return '<circle class="mq-pieza-cuerpo" cx="' + x + '" cy="' + (y - s) + '" r="' + s + '"/>';
    }
  };

  /* Monta lo que haya que dibujar en el área. Devuelve la DISPOSICIÓN —índice,
     tipo y coordenada— que es lo que la maqueta compara para comprobar el
     determinismo. Con escena real la coordenada es la del mundo; con el
     respaldo, la del lienzo plano. En los dos casos se deriva del índice. */
  function dibujarEscena(contenedor, arbol, opciones) {
    opciones = opciones || {};
    var nodos = arbol || [];
    var dibujables = nodos.filter(function (n) { return n.forma; });
    var cromo = cromoDeEscena(contenedor);
    var V = Visor();

    /* Sin medidas no hay malla que construir: eso sólo pasa cuando el llamador
       pasa el árbol declarado en lugar del texto, y entonces se usa el
       respaldo plano en vez de inventar dimensiones. */
    var conMedidas = dibujables.every(function (n) { return !!n.dimensiones; });

    /* ---- camino normal: el visor real ---------------------------------- */
    if (V && V.hayCapacidadGrafica() && conMedidas) {
      var motor = motorDe(contenedor);
      if (!motor && dibujables.length && opciones.crearInstancia !== false) {
        motor = crearMotor(contenedor);
      }
      if (motor) {
        quitarRespaldoSvg(contenedor);
        /* Si la carga ya la hizo `cargarJson` —que es quien la tiene que
           hacer— no se vuelve a construir la escena: sólo se actualiza el
           cromo y se devuelve la disposición que la fachada informó. */
        var disp = opciones.disposicion || motor.cargar(nodos).disposicion;
        cromo.rotulo.textContent = 'Área de dibujo · escena tridimensional';
        cromo.leyenda.textContent = dibujables.length ? ''
          : (opciones.leyendaVacia || 'Todavía no hay nada que dibujar');
        cromo.leyenda.hidden = dibujables.length > 0;
        mostrarControlesDeMovimiento(contenedor, true);
        return disp;
      }
    }

    /* ---- respaldo: representación plana, sin motor gráfico -------------- */
    return respaldoSvg(contenedor, cromo, nodos, dibujables, opciones);
  }

  function respaldoSvg(contenedor, cromo, arbol, dibujables, opciones) {
    var ancho = 400, alto = 300, suelo = 235;
    var hayMotor = !!Visor() && Visor().hayCapacidadGrafica();

    quitarRespaldoSvg(contenedor);
    mostrarControlesDeMovimiento(contenedor, false);
    cromo.leyenda.textContent = '';
    cromo.leyenda.hidden = true;

    if (!dibujables.length) {
      cromo.rotulo.textContent = 'Área de dibujo';
      contenedor.appendChild(crear(
        '<svg viewBox="0 0 ' + ancho + ' ' + alto + '" role="img" ' +
        'aria-label="El área de dibujo está vacía: no hay ninguna pieza dibujada.">' +
        '<line x1="40" y1="' + suelo + '" x2="' + (ancho - 40) + '" y2="' + suelo +
        '" stroke="currentColor" stroke-width="1" opacity=".22"/>' +
        '<text x="' + (ancho / 2) + '" y="150" text-anchor="middle" class="mq-pieza-etiqueta">' +
        esc(opciones.leyendaVacia || 'Todavía no hay nada que dibujar') +
        '</text></svg>'));
      return [];
    }

    cromo.rotulo.textContent = hayMotor
      ? 'Área de dibujo · representación de respaldo'
      : 'Área de dibujo · representación de respaldo (este navegador no ofrece capacidad gráfica)';

    var ranuras = Math.max.apply(null, dibujables.map(function (n) { return n.indice; })) + 1;
    var paso = (ancho - 80) / ranuras;
    var s = Math.min(46, paso * 0.32);

    var disposicion = [];
    var piezas = dibujables.map(function (n, i) {
      /* La ubicación se deriva del ÍNDICE en el conjunto raíz, no del orden de
         llegada: es lo que hace determinista la disposición (G-6). */
      var x = Math.round(40 + paso * (n.indice + 0.5));
      disposicion.push({ indice: n.indice, tipo: n.tipo, x: x });
      var forma = FORMAS[n.forma] || FORMAS.cubo;
      /* Con muchas ranuras la etiqueta se parte en dos líneas: en una sola se
         solaparía con la de la pieza vecina. */
      var etiqueta = paso < 95
        ? '<text class="mq-pieza-etiqueta" x="' + x + '" y="' + (suelo + 18) + '" text-anchor="middle">[' +
          n.indice + ']<tspan x="' + x + '" dy="13">' + esc(n.tipo) + '</tspan></text>'
        : '<text class="mq-pieza-etiqueta" x="' + x + '" y="' + (suelo + 20) + '" text-anchor="middle">[' +
          n.indice + '] ' + esc(n.tipo) + '</text>';
      return '<g class="mq-pieza" role="button" tabindex="0" aria-pressed="false" ' +
             'data-mq-indice="' + n.indice + '" ' +
             'aria-label="Pieza ' + n.indice + ', ' + esc(n.tipo) + '. Seleccionar en la escena.">' +
             forma(x, suelo, s) + etiqueta + '</g>';
    }).join('');

    contenedor.appendChild(crear(
      '<svg viewBox="0 0 ' + ancho + ' ' + alto + '" role="group" ' +
      'aria-label="Área de dibujo con ' + plural(dibujables.length, 'pieza dibujada', 'piezas dibujadas') + '.">' +
      '<line x1="40" y1="' + suelo + '" x2="' + (ancho - 40) + '" y2="' + suelo +
      '" stroke="currentColor" stroke-width="1" opacity=".22"/>' + piezas + '</svg>'));

    return disposicion;
  }

  /* ==========================================================================
     Contrato de fachada de GeometriaFactory-Visor

     El visor no tiene pantallas propias: su superficie pública son cinco
     funciones planas que un componente anfitrión invoca. Por decisión del
     Product Owner, su validación no tiene maqueta propia y se integra acá,
     dentro de las superficies que lo usan.

     Desde la iteración 3 esto NO es un doble: las cinco funciones operan sobre
     la escena real de `Visor-Tridimensional.js`. La fachada conserva lo que
     sí le corresponde —el ciclo de vida, la bitácora de invocaciones y las
     siete condiciones tal como el contrato las declara— y delega en el motor
     el dibujo, la selección, el ajuste y la liberación de recursos.

     El panel que exhibe estas cinco funciones sigue siendo un instrumento de
     validación y no forma parte del producto: en el producto, el componente
     anfitrión las invoca sin exhibirlas.
     ========================================================================== */

  var Fachada = (function () {
    var secuencia = 0;
    var instancias = {};        // id → { host, resultado, seleccion, viva }
    var liberadas = 0;
    var bitacora = [];
    var oyentes = [];

    function anotar(llamada, resultado, condicion) {
      bitacora.push({ n: bitacora.length + 1, llamada: llamada, resultado: resultado, condicion: condicion || null });
      if (bitacora.length > 40) { bitacora.shift(); }
      oyentes.forEach(function (f) { f(); });
    }

    /* La disposición se deriva del índice en el conjunto raíz: dos cargas del
       mismo texto producen la misma disposición, comparable pieza por pieza.
       Ésta es la firma con la que la maqueta lo comprueba de verdad. */
    function firmaDeDisposicion(dibujadas) {
      return (dibujadas || []).map(function (p) {
        return p.indice + ':' + p.tipo + '@' + p.x + (p.z === undefined ? '' : ',' + p.z);
      }).join(' | ');
    }

    return {
      /* --- estado observable ---------------------------------------------- */
      bitacora: function () { return bitacora; },
      instancia: function (id) { return instancias[id] || null; },
      vivas: function () {
        return Object.keys(instancias).filter(function (k) { return instancias[k].viva; });
      },
      liberadas: function () { return liberadas; },
      alCambiar: function (f) { oyentes.push(f); },
      firmaDeDisposicion: firmaDeDisposicion,

      /* --- CU-01 · inicializar(elemento, opciones) -------------------------
         Crea la instancia del visor real sobre el elemento de dibujo: escena,
         iluminación, cámara orbital y contexto gráfico. Ninguna pieza dibujada
         hasta que se invoque cargarJson. */
      inicializar: function (host, opciones) {
        opciones = opciones || {};
        var V = Visor();
        if (opciones.capacidadGrafica === false || (V && !V.hayCapacidadGrafica() && opciones.exigirMotor)) {
          anotar('inicializar(elemento, opciones)', 'sin identificador', 'CAPACIDAD_GRAFICA_AUSENTE');
          return { condicion: 'CAPACIDAD_GRAFICA_AUSENTE' };
        }
        if (opciones.elementoValido === false) {
          anotar('inicializar(elemento, opciones)', 'sin identificador', 'ELEMENTO_DE_DIBUJO_INVALIDO');
          return { condicion: 'ELEMENTO_DE_DIBUJO_INVALIDO', curso: 'C-1, en creación' };
        }
        var id = 'vis-' + (++secuencia);
        var motor = host ? crearMotor(host) : null;
        instancias[id] = { host: host, motor: motor, resultado: null, seleccion: null, viva: true };
        anotar('inicializar(elemento, opciones)', 'id ' + id + ' · instancia viva, sin ninguna pieza dibujada' +
               (motor ? '' : ' (representación de respaldo: el navegador no ofrece capacidad gráfica)'));
        return { id: id };
      },

      /* --- CU-02 · cargarJson(id, texto) ----------------------------------
         El texto se interpreta con el lector tolerante del visor: claves
         alternativas, comas finales y comentarios. Ninguna pieza que no se
         dibuje desaparece: queda enumerada con su índice y su código. */
      cargarJson: function (id, ejercicio) {
        var inst = instancias[id];
        if (!inst || !inst.viva) {
          anotar('cargarJson(' + id + ', texto)', 'sin efecto', 'INSTANCIA_DESCONOCIDA');
          return { condicion: 'INSTANCIA_DESCONOCIDA' };
        }
        // Cada carga reemplaza por completo lo dibujado y libera lo anterior.
        inst.seleccion = null;

        var lectura = nodosDeTexto(ejercicio.texto, ejercicio.arbol);
        var condicion = ejercicio.condicionGeneral || lectura.condicion;

        if (condicion === 'TEXTO_NO_LEGIBLE') {
          inst.resultado = { dibujadas: [], noDibujadas: [], estructura: [],
                             disposicion: [], condicion: 'TEXTO_NO_LEGIBLE', raiz: 0 };
          if (inst.motor) { inst.motor.cargar([]); }
          anotar('cargarJson(' + id + ', texto)', 'instancia viva y vacía', 'TEXTO_NO_LEGIBLE');
          return inst.resultado;
        }

        var nodos = lectura.nodos;
        var noDibujadas = [];
        var dibujadas = [];
        nodos.forEach(function (n) {
          if (n.forma) { dibujadas.push({ indice: n.indice, tipo: n.tipo }); return; }
          // G-5, sin fallo silencioso: si no se dibuja, queda enumerada.
          noDibujadas.push({
            indice: n.indice,
            motivo: n.motivo || 'tipo no dibujable',
            codigo: n.codigo || 'TIPO_NO_DIBUJABLE'
          });
        });

        /* La escena se construye acá: cargarJson ES la carga. La disposición
           que devuelve es la que la maqueta compara para el determinismo, y
           sale de la derivación por índice del visor, nunca del azar. */
        var V = Visor();
        var dibujables = nodos.filter(function (n) { return !!n.forma; });
        var disposicion = inst.motor
          ? inst.motor.cargar(nodos).disposicion
          : (V ? V.disponerPorIndice(dibujables, nodos.length)
               : dibujables.map(function (n) { return { indice: n.indice, tipo: n.tipo, x: n.indice, z: 0 }; }));

        inst.resultado = {
          dibujadas: dibujadas, noDibujadas: noDibujadas,
          estructura: nodos, disposicion: disposicion, condicion: null,
          raiz: nodos.length
        };
        anotar('cargarJson(' + id + ', texto)',
               dibujadas.length + ' dibujadas · ' + noDibujadas.length + ' no dibujadas enumeradas');
        return inst.resultado;
      },

      /* --- CU-03 · seleccionarPieza(id, indice) --------------------------- */
      seleccionarPieza: function (id, indice) {
        var inst = instancias[id];
        if (!inst || !inst.viva) {
          anotar('seleccionarPieza(' + id + ', ' + indice + ')', 'sin efecto', 'INSTANCIA_DESCONOCIDA');
          return { condicion: 'INSTANCIA_DESCONOCIDA' };
        }
        var hay = inst.resultado && inst.resultado.dibujadas.some(function (p) {
          return String(p.indice) === String(indice);
        });
        if (!hay) {
          // Cubre el índice ausente y el de una pieza enumerada como no dibujada.
          anotar('seleccionarPieza(' + id + ', ' + indice + ')',
                 'la selección vigente se conserva', 'INDICE_FUERA_DE_RANGO');
          return { condicion: 'INDICE_FUERA_DE_RANGO', seleccion: inst.seleccion };
        }
        inst.seleccion = indice;   // resaltado exclusivo sobre la malla real
        if (inst.motor) { inst.motor.seleccionar(indice); }
        anotar('seleccionarPieza(' + id + ', ' + indice + ')', 'resaltada la pieza ' + indice + ', y sólo ésa');
        return { seleccion: indice };
      },

      /* --- CU-04 · redimensionar(id) -------------------------------------- */
      redimensionar: function (id, opciones) {
        opciones = opciones || {};
        var inst = instancias[id];
        if (!inst || !inst.viva) {
          anotar('redimensionar(' + id + ')', 'sin efecto', 'INSTANCIA_DESCONOCIDA');
          return { condicion: 'INSTANCIA_DESCONOCIDA' };
        }
        var ajustada = opciones.elementoValido === false
          ? false
          : (inst.motor ? inst.motor.ajustar() : true);
        if (!ajustada) {
          anotar('redimensionar(' + id + ')',
                 'la instancia sigue viva, con su escena y su selección intactas',
                 'ELEMENTO_DE_DIBUJO_INVALIDO');
          return { condicion: 'ELEMENTO_DE_DIBUJO_INVALIDO', curso: 'C-2, en ajuste' };
        }
        anotar('redimensionar(' + id + ')', 'escena ajustada al tamaño vigente · selección y disposición conservadas');
        return { ajustada: true };
      },

      /* --- CU-05 · destruir(id) --------------------------------------------
         Libera de verdad: geometrías, materiales y contexto gráfico. Lo que se
         anota en la bitácora es lo que el visor informa haber liberado. */
      destruir: function (id) {
        var inst = instancias[id];
        if (!inst || !inst.viva) {
          anotar('destruir(' + id + ')', 'sin efecto', 'INSTANCIA_DESCONOCIDA');
          return { condicion: 'INSTANCIA_DESCONOCIDA' };
        }
        inst.viva = false; inst.resultado = null; inst.seleccion = null;
        if (inst.host) { liberarMotor(inst.host); }
        inst.motor = null;
        liberadas++;
        var V = Visor();
        var r = V ? V.recursos() : null;
        anotar('destruir(' + id + ')', 'geometrías, materiales y contexto liberados · ' +
               'el identificador deja de ser válido' +
               (r ? ' · quedan ' + plural(r.geometrias, 'geometría', 'geometrías') + ', ' +
                    plural(r.materiales, 'material', 'materiales') + ' y ' +
                    plural(r.contextos, 'contexto gráfico', 'contextos gráficos') + ' vivos' : ''));
        return { liberada: true };
      },

      reiniciarBitacora: function () { bitacora.length = 0; oyentes.forEach(function (f) { f(); }); }
    };
  })();

  /* --- Medición real de las propiedades transversales ---------------------- */

  /* Cero red: se cuentan las peticiones que el navegador registró desde que la
     escena se dibujó. El umbral del contrato es exactamente 0. */
  var marcaDeRed = 0;
  function marcarRed() {
    try { marcaDeRed = performance.getEntriesByType('resource').length; } catch (e) { marcaDeRed = 0; }
  }
  function peticionesDesdeLaMarca() {
    try { return Math.max(0, performance.getEntriesByType('resource').length - marcaDeRed); }
    catch (e) { return 0; }
  }

  /* Cero persistencia: ninguna clave de la fachada en el almacenamiento. Las
     claves de la maqueta (`mq-…`) son del instrumento y no del visor.

     LIMITACIÓN CONOCIDA, QUE NO SE HEREDA AL SISTEMA CONSTRUIDO: la exclusión
     es por PREFIJO y no por espacio de nombres, así que una clave de un tercero
     que empezara con `mq-` quedaría fuera del recuento. En esta maqueta no hay
     terceros y es inocuo; donde `SD-47` mida lo mismo sobre el sistema
     construido, la exclusión tiene que ser por espacio de nombres declarado. */
  function clavesDeLaFachada() {
    var n = 0;
    [localStorage, sessionStorage].forEach(function (almacen) {
      try {
        for (var i = 0; i < almacen.length; i++) {
          var k = almacen.key(i);
          if (k && k.indexOf('mq-') !== 0) { n++; }
        }
      } catch (e) {}
    });
    return n;
  }

  /* ==========================================================================
     Árbol de la estructura y sincronización por índice

     QUÉ ÁRBOL QUEDÓ: uno solo, que fusiona los dos. Del árbol del visor
     original se absorbe lo que la documentación llama «el mejor recurso
     didáctico del visor»: el recorrido COLAPSABLE de la estructura entera,
     clave por clave y valor por valor, hasta la hoja. Del árbol de la maqueta
     se conserva lo que el original no tiene y el producto exige: el índice de
     la pieza como identidad compartida con el resultado de dibujo, la marca
     de la pieza que no se dibujó con su código de condición, los roles de
     árbol para el lector de pantalla y el recorrido por teclado.

     La rama de cada pieza arranca colapsada. El original abría todo, y con
     seis piezas de cuatro niveles el árbol dejaba de servir para lo que sirve.
     ========================================================================== */

  var secuenciaDeRama = 0;

  /* Recorrido colapsable del valor crudo, portado del árbol del visor. */
  function ramaDeValor(clave, valor, profundidad) {
    if (valor !== null && typeof valor === 'object') {
      var esArreglo = Array.isArray(valor);
      var hijos = (esArreglo
        ? valor.map(function (v, i) { return ramaDeValor('[' + i + ']', v, profundidad + 1); })
        : Object.keys(valor).map(function (k) { return ramaDeValor(k, valor[k], profundidad + 1); })
      ).join('');
      var idRama = 'mq-rama-' + (++secuenciaDeRama);
      /* UN SOLO PORTADOR DE ROL Y DE ESTADO: el `<li role="treeitem">`. Antes el
         `aria-expanded` y el `aria-selected` estaban a la vez en el `<li>` y en
         un `<button>` interno, con lo que el lector de pantalla anunciaba dos
         elementos por nodo. El interior pasa a ser un `<span>` de presentación y
         el foco lo lleva el `<li>`, con tabindex móvil. */
      return '<li role="treeitem" aria-expanded="false" tabindex="-1" ' +
                 'data-mq-rama="' + idRama + '">' +
        '<span class="mq-nodo mq-nodo--rama">' +
          '<span class="mq-nodo-flecha" data-mq-flecha aria-hidden="true"></span>' +
          '<span class="mq-json-clave">' + esc(clave) + '</span>' +
          '<span class="mq-json-cuenta">' + (esArreglo ? '[' + valor.length + ']' : '{…}') + '</span>' +
        '</span>' +
        '<ul role="group" id="' + idRama + '" class="mq-arbol-hijos" hidden>' + hijos + '</ul>' +
      '</li>';
    }
    var clase = typeof valor === 'string' ? 'mq-json-cadena'
              : typeof valor === 'number' ? 'mq-json-numero' : 'mq-json-otro';
    var texto = typeof valor === 'string' ? '"' + valor + '"' : String(valor);
    return '<li role="treeitem" tabindex="-1"><span class="mq-nodo mq-nodo--hoja">' +
             '<span class="mq-json-clave">' + esc(clave) + ':</span> ' +
             '<span class="' + clase + '">' + esc(texto) + '</span>' +
           '</span></li>';
  }

  function pintarArbol(contenedor, arbol, noDibujadas) {
    if (!contenedor) { return; }
    /* Ausencia de fallo silencioso: la pieza que no se dibujó no desaparece
       del árbol. Figura con su índice y con el código de condición que lo
       explica, además de estar en la lista de al lado de la escena. */
    function condicionDe(indice) {
      return (noDibujadas || []).filter(function (p) { return String(p.indice) === String(indice); })[0] || null;
    }

    arbol = arbol || [];

    contenedor.innerHTML =
      '<ul class="mq-arbol" role="tree" aria-label="Árbol de la estructura del trabajo">' +
        '<li role="none"><span class="mq-caption mq-sangria-2">conjunto (' + arbol.length + ')</span>' +
        '<ul role="group">' +
        arbol.map(function (n) {
          /* Los hijos salen del valor crudo de la pieza cuando el visor lo
             leyó del texto; si no, de las etiquetas ya declaradas. */
          var hijos = n.datos
            ? Object.keys(n.datos).map(function (k) { return ramaDeValor(k, n.datos[k], 1); }).join('')
            : (n.hijos || []).map(function (h) {
                return '<li role="treeitem" tabindex="-1"><span class="mq-nodo mq-nodo--hoja">' +
                       '<span class="mq-json-clave">' + esc(h.etiqueta) + '</span></span></li>';
              }).join('');
          var cond = condicionDe(n.indice);
          var marca = cond
            ? ' <span class="mq-insignia mq-insignia--neutro" title="Condición de la fachada del visor">' +
              'no dibujada · ' + esc(cond.codigo) + '</span>'
            : '';
          var idRama = 'mq-rama-' + (++secuenciaDeRama);
          return '<li role="treeitem" aria-selected="false" tabindex="-1" ' +
                     'data-mq-nodo="' + n.indice + '"' +
                     (hijos ? ' aria-expanded="false" data-mq-rama="' + idRama + '"' : '') + '>' +
                   '<span class="mq-nodo">' +
                     (hijos ? '<span class="mq-nodo-flecha" data-mq-flecha aria-hidden="true"></span>' : '') +
                     '<span class="mq-nodo-indice">[' + n.indice + ']</span> ' + esc(n.tipo) + marca +
                   '</span>' +
                   (hijos ? '<ul role="group" id="' + idRama + '" class="mq-arbol-hijos" hidden>' + hijos + '</ul>' : '') +
                 '</li>';
        }).join('') +
        '</ul></li></ul>';

    /* Recorrido por teclado del árbol, con TABINDEX MÓVIL: un solo nodo tabable
       por árbol, y las flechas mueven el foco entre los nodos visibles. En el
       nodo de pieza el despliegue va en la flecha, para que el resto del nodo
       siga siendo la selección por índice. */
    var raizArbol = $('[role="tree"]', contenedor);
    if (!raizArbol) { return; }

    function itemsDelArbol() { return $$('[role="treeitem"]', raizArbol); }
    function itemsVisibles() {
      return itemsDelArbol().filter(function (o) { return !o.closest('.mq-arbol-hijos[hidden]'); });
    }
    function enfocar(li) {
      if (!li) { return; }
      itemsDelArbol().forEach(function (o) { o.setAttribute('tabindex', o === li ? '0' : '-1'); });
      li.focus();
    }
    function alternar(li) {
      var idLista = li.getAttribute('data-mq-rama');
      var lista = idLista ? document.getElementById(idLista) : null;
      if (!lista) { return; }
      var abierto = li.getAttribute('aria-expanded') === 'true';
      li.setAttribute('aria-expanded', String(!abierto));
      lista.hidden = abierto;
    }

    var todos = itemsDelArbol();
    if (todos.length) { todos[0].setAttribute('tabindex', '0'); }

    raizArbol.addEventListener('click', function (ev) {
      var li = ev.target.closest ? ev.target.closest('[role="treeitem"]') : null;
      if (!li || !raizArbol.contains(li)) { return; }
      enfocar(li);
      if (!li.hasAttribute('data-mq-nodo') || ev.target.closest('[data-mq-flecha]')) { alternar(li); }
    });

    raizArbol.addEventListener('keydown', function (ev) {
      var li = ev.target.closest ? ev.target.closest('[role="treeitem"]') : null;
      if (!li || !raizArbol.contains(li)) { return; }
      var visibles = itemsVisibles(), i = visibles.indexOf(li);
      if (ev.key === 'ArrowDown') { ev.preventDefault(); enfocar(visibles[(i + 1) % visibles.length]); }
      else if (ev.key === 'ArrowUp') { ev.preventDefault(); enfocar(visibles[(i - 1 + visibles.length) % visibles.length]); }
      else if (ev.key === 'Home') { ev.preventDefault(); enfocar(visibles[0]); }
      else if (ev.key === 'End') { ev.preventDefault(); enfocar(visibles[visibles.length - 1]); }
      else if (ev.key === 'ArrowRight' && li.getAttribute('aria-expanded') === 'false') { ev.preventDefault(); alternar(li); }
      else if (ev.key === 'ArrowLeft' && li.getAttribute('aria-expanded') === 'true') { ev.preventDefault(); alternar(li); }
    });
  }

  function sincronizar(raiz) {
    var nodos = $$('[data-mq-nodo]', raiz);
    var piezas = $$('.mq-pieza', raiz);
    var acuse = $('[data-mq-seleccion]', raiz);
    var escena = $('[data-mq-escena]', raiz) || $('[data-mq-escena]');
    if (!nodos.length) { return; }

    function seleccionar(indice, origen) {
      nodos.forEach(function (b) {
        var activo = b.getAttribute('data-mq-nodo') === String(indice);
        b.setAttribute('aria-selected', String(activo));
      });
      /* Con escena real el resaltado va sobre la malla; el respaldo plano
         sigue funcionando cuando no hay capacidad gráfica. */
      var motor = motorDe(escena);
      var hayRepresentacion = motor ? motor.seleccionar(indice) : false;
      piezas.forEach(function (g) {
        var activo = g.getAttribute('data-mq-indice') === String(indice);
        g.setAttribute('aria-pressed', String(activo));
        if (activo) { hayRepresentacion = true; }
      });
      if (acuse) {
        var nodo = nodos.filter(function (b) { return b.getAttribute('data-mq-nodo') === String(indice); })[0];
        var nombre = nodo ? nodo.textContent.trim() : ('pieza ' + indice);
        acuse.textContent = hayRepresentacion
          ? 'Seleccionada la pieza ' + nombre + '. La escena resalta esa pieza y sólo esa.'
          : 'Seleccionada la pieza ' + nombre + '. Esa pieza no tiene representación en la escena; ' +
            'la selección vigente se conserva y el nodo sigue siendo navegable.';
      }
    }

    /* Pinchar una pieza EN LA ESCENA REAL vuelve por acá: es la mitad que
       faltaba de la sincronización por índice, ahora en los dos sentidos. */
    alElegirEnLaEscena = function (indice) { seleccionar(indice, 'escena'); };

    /* El recorrido con flechas lo gobierna `pintarArbol`, que es el dueño del
       tabindex móvil. Acá queda sólo lo que es SELECCIÓN: pinchar el nodo, o
       activarlo con Entrar o con la barra espaciadora. */
    nodos.forEach(function (b) {
      b.addEventListener('click', function () { seleccionar(b.getAttribute('data-mq-nodo'), 'arbol'); });
      b.addEventListener('keydown', function (ev) {
        if (ev.key !== 'Enter' && ev.key !== ' ') { return; }
        ev.preventDefault();
        seleccionar(b.getAttribute('data-mq-nodo'), 'arbol');
      });
    });

    piezas.forEach(function (g) {
      function activar() { seleccionar(g.getAttribute('data-mq-indice'), 'escena'); }
      g.addEventListener('click', activar);
      g.addEventListener('keydown', function (ev) {
        if (ev.key === 'Enter' || ev.key === ' ') { ev.preventDefault(); activar(); }
      });
    });
  }

  /* ==========================================================================
     Render de colecciones
     ========================================================================== */

  function accionesDeTrabajo(t, papel, desde) {
    // Lo que el estado no admite no se dibuja, ni siquiera inhabilitado.
    var a = ['<a class="mq-btn mq-btn--secundario" href="' + hrefTrabajo(t.id, papel, desde) + '">' +
             icono('abrir') + 'Abrir<span class="mq-sr-only"> «' + esc(t.nombre) + '»</span></a>'];
    if (papel === 'alumno' && t.estado === 'Borrador') {
      a.push('<a class="mq-btn mq-btn--secundario" href="Envio-De-Trabajo.html?t=' + t.id + '">' +
             icono('editar') + 'Editar<span class="mq-sr-only"> «' + esc(t.nombre) + '»</span></a>');
      a.push('<button type="button" class="mq-btn mq-btn--destructivo" data-mq-eliminar="' + t.id + '">' +
             icono('eliminar') + 'Eliminar<span class="mq-sr-only"> «' + esc(t.nombre) + '»</span></button>');
    }
    return a.join('');
  }

  function filaDeTrabajo(t, papel, desde) {
    return '<tr>' +
      '<th scope="row" class="mq-body-strong mq-th-plano mq-th-plano--cuerpo">' + esc(t.nombre) + '</th>' +
      '<td class="mq-num">' + esc(t.fechaTrabajo) + '</td>' +
      '<td>' + insignia(t.estado) + '</td>' +
      '<td class="mq-num">' + t.piezas + '</td>' +
      '<td class="mq-num">' + t.advertencias + '</td>' +
      '<td class="mq-td-acciones"><span class="mq-acciones-fila">' + accionesDeTrabajo(t, papel, desde) + '</span></td>' +
    '</tr>';
  }

  function tarjetaDeTrabajo(t, papel, desde) {
    return '<article class="mq-tarjeta-fila">' +
      '<div class="mq-tarjeta-fila-cabecera">' +
        '<h3 class="mq-body-strong">' + esc(t.nombre) + '</h3>' + insignia(t.estado) +
      '</div>' +
      '<p class="mq-caption mq-num mq-margen-arriba-2">' + esc(t.fechaTrabajo) + ' · ' +
        plural(t.piezas, 'pieza', 'piezas') + ' · ' +
        plural(t.advertencias, 'advertencia', 'advertencias') + '</p>' +
      '<div class="mq-acciones-fila">' + accionesDeTrabajo(t, papel, desde) + '</div>' +
    '</article>';
  }

  function renderListadoDeTrabajos(host, trabajos, papel, desde) {
    var tabla = $('[data-mq-filas]', host);
    var tarjetas = $('[data-mq-tarjetas]', host);
    if (tabla)    { tabla.innerHTML = trabajos.map(function (t) { return filaDeTrabajo(t, papel, desde); }).join(''); }
    if (tarjetas) { tarjetas.innerHTML = trabajos.map(function (t) { return tarjetaDeTrabajo(t, papel, desde); }).join(''); }
  }

  function renderObservaciones(host, observaciones) {
    var cuenta = $('[data-mq-observaciones-recuento]', host);
    var lista = $('[data-mq-observaciones]', host);
    if (cuenta) { cuenta.textContent = observaciones.length ? ' (' + observaciones.length + ')' : ''; }
    if (!lista) { return; }
    if (!observaciones.length) {
      lista.innerHTML = '<li class="mq-caption">Sin observaciones.</li>';
      return;
    }
    lista.innerHTML = observaciones.map(function (o) {
      var esError = o.severidad.indexOf('Error') === 0;
      var chip = '<span class="mq-insignia ' + (esError ? 'mq-insignia--peligro' : 'mq-insignia--atencion') + '">' +
                 esc(o.etiqueta) + '</span>';
      var valores = (o.declarado != null)
        ? '<dl class="mq-valores"><dt>declarado</dt><dd>' + esc(o.declarado) + '</dd>' +
          '<dt>derivado</dt><dd>' + esc(o.derivado) + '</dd></dl>'
        : '';
      var texto = o.texto ? '<p class="mq-caption mq-margen-arriba-1">' + esc(o.texto) + '</p>' : '';
      return '<li class="mq-observacion">' + chip +
        '<div class="mq-observacion-cuerpo">' +
          '<p class="mq-observacion-ubicacion mq-sin-margen">figura ' + o.figura + ' · campo ' + esc(o.campo) + '</p>' +
          texto + valores +
        '</div></li>';
    }).join('');
  }

  function renderPiezasNoDibujadas(host, piezas) {
    var bloque = $('[data-mq-no-dibujadas]', host);
    if (!bloque) { return; }
    if (!piezas || !piezas.length) { bloque.hidden = true; return; }
    bloque.hidden = false;
    $('[data-mq-no-dibujadas-lista]', bloque).innerHTML = piezas.map(function (p) {
      return '<li>pieza ' + p.indice + ' — ' + esc(p.motivo) + '</li>';
    }).join('');
  }

  /* ==========================================================================
     Las cuatro partes de la vista de trabajo
     (Wireframes-Vista-De-Trabajo.md §2 y §3)

     Vive acá, en un solo lugar, porque hay DOS superficies que la presentan:
     la vista de trabajo y la resolución del trabajo, que es un bloque alojado
     dentro de ella. Duplicar el armado en los dos HTML obligaría a hacer cada
     corrección dos veces, que es el anti-patrón que la regla nombra.

     Disposición decidida aguas arriba y probada en el aula, que este render
     materializa y NO rediseña: datos y texto original a la izquierda; área de
     dibujo arriba y árbol de la estructura abajo, a la derecha.

     `op.conEstados` agrega los bloques alternativos de la escena con su
     `data-mq-estado`. La vista de trabajo los declara; la resolución no, y por
     eso los pide apagados.
     ========================================================================== */

  function pintarCuatroPartes(host, t, op) {
    op = op || {};
    var duenio = D.cuenta(t.cuentaId);
    var nombreDuenio = (duenio.nombre + ' ' + duenio.apellido).trim();
    var conEstados = op.conEstados !== false;

    function est(lista) { return conEstados ? ' data-mq-estado="' + lista + '"' : ''; }

    var estadosBase = 'con-datos sin-observaciones sin-comentario con-comentario ' +
                      'piezas-no-dibujadas borrador-con-errores texto-no-legible ' +
                      'indice-sin-representacion reconectando dimension-no-legible ' +
                      'instancia-desconocida elemento-sin-tamano-ajuste';

    host.innerHTML =
      '<div class="mq-dos-columnas">' +

        /* ---------- Columna izquierda: datos → comentario → observaciones →
           texto original. En ese orden y por ese motivo: el comentario es lo
           primero que el alumno busca cuando su trabajo ya tiene desenlace, y
           el texto original va último y colapsado porque es material de
           consulta y no de lectura. ---------- */
        '<div class="mq-columna mq-columna--datos">' +

          '<section class="mq-tarjeta" aria-labelledby="vt-datos-h">' +
            '<h2 id="vt-datos-h" class="mq-meta">Datos del trabajo</h2>' +
            '<dl class="mq-clave-valor mq-mt-3">' +
              '<dt>Nombre</dt><dd>' + esc(t.nombre) + '</dd>' +
              '<dt>Fecha del trabajo</dt><dd>' + esc(t.fechaTrabajo) + '</dd>' +
              '<dt>Estado</dt><dd>' + insignia(t.estado) + '</dd>' +
              '<dt>Alumno</dt><dd>' + esc(nombreDuenio) + '</dd>' +
              '<dt>Descripción</dt><dd>' + esc(t.descripcion) + '</dd>' +
              (t.fechaDesenlace
                ? '<dt>Fecha del desenlace</dt><dd>' + esc(t.fechaDesenlace) + '</dd>' : '') +
            '</dl>' +
          '</section>' +

          /* Bloque de comentario: contenedor y encabezado propios, sin
             severidad, sin índice, sin campo señalado y sin tono de alerta.
             No es una observación y no es una calificación. No se dibuja si
             no viene poblado. */
          (t.comentario
            ? '<section class="mq-comentario" aria-labelledby="vt-com-h">' +
                '<h2 id="vt-com-h" class="mq-meta">Comentario del docente</h2>' +
                '<p class="mq-margen-arriba-2">' + esc(t.comentario) + '</p>' +
              '</section>'
            : '') +

          '<section class="mq-tarjeta" aria-labelledby="vt-obs-h">' +
            '<h2 id="vt-obs-h" class="mq-meta">Observaciones' +
              '<span data-mq-observaciones-recuento></span></h2>' +
            '<ul class="mq-observaciones mq-mt-3" data-mq-observaciones></ul>' +
          '</section>' +

          '<section class="mq-tarjeta" aria-labelledby="vt-texto-h">' +
            '<h2 id="vt-texto-h" class="mq-sr-only">Texto original del trabajo</h2>' +
            '<details class="mq-disclosure"><summary>Texto original</summary>' +
              '<pre class="mq-texto-original">' + esc(t.textoOriginal) + '</pre>' +
            '</details>' +
          '</section>' +
        '</div>' +

        /* ---------- Columna derecha: área de dibujo arriba, árbol abajo ---------- */
        '<div class="mq-columna mq-columna--escena">' +
          '<section aria-labelledby="vt-escena-h">' +
            '<h2 id="vt-escena-h" class="mq-meta">Escena</h2>' +
            '<div class="mq-escena mq-mt-2" data-mq-escena' + est(estadosBase) + '></div>' +

            (conEstados
              ? '<div class="mq-tarjeta mq-mt-2" data-mq-estado="escena-no-disponible">' +
                  '<h3 class="mq-body-strong">La escena no está disponible en este navegador</h3>' +
                  '<p class="mq-caption mq-margen-arriba-2">Tu navegador no ofrece la capacidad ' +
                  'gráfica que el dibujo necesita. Los datos, el texto y el árbol de la estructura ' +
                  'siguen disponibles acá abajo.</p></div>' +
                '<div class="mq-tarjeta mq-mt-2" data-mq-estado="elemento-sin-tamano">' +
                  '<h3 class="mq-body-strong">La escena no está disponible</h3>' +
                  '<p class="mq-caption mq-margen-arriba-2">El espacio disponible para el dibujo ' +
                  'quedó sin tamaño. Las otras tres partes se conservan, y la escena se vuelve a armar ' +
                  'cuando el espacio se recupera.</p></div>' +
                '<p class="mq-banda mq-banda--atencion mq-mt-2" role="status" ' +
                  'data-mq-estado="texto-no-legible">De este texto no se obtuvo ninguna pieza, así que la escena ' +
                  'quedó vacía. El árbol y las observaciones se muestran igual.</p>'
              : '') +

            /* Piezas no dibujadas: al lado de la escena, nunca encima y nunca
               en la lista de observaciones. Ninguna pieza desaparece sin
               quedar enumerada en texto. */
            '<div data-mq-no-dibujadas class="mq-mt-2">' +
              '<h3 class="mq-meta">No se dibujaron</h3>' +
              '<ul class="mq-caption mq-lista-anidada" data-mq-no-dibujadas-lista></ul>' +
            '</div>' +
          '</section>' +

          '<section aria-labelledby="vt-arbol-h">' +
            '<h2 id="vt-arbol-h" class="mq-meta">Árbol de la estructura</h2>' +
            '<div class="mq-tarjeta mq-caja-desplazable mq-mt-2" data-mq-arbol></div>' +
            '<p class="mq-sr-only" role="status" data-mq-seleccion></p>' +
            '<p class="mq-caption mq-mt-2">Elegí un nodo de pieza y la escena resalta ' +
            'esa pieza y sólo esa. Elegí una pieza de la escena y el nodo queda marcado, por el ' +
            'mismo índice.</p>' +
          '</section>' +

          /* Panel del contrato de fachada del visor. Va acá, junto a la escena
             y al árbol, porque es la superficie que invoca las cinco funciones
             y es donde el concepto se puede juzgar. */
          (op.conFachada === false ? '' : '<div data-mq-panel-fachada></div>') +
        '</div>' +
      '</div>';

    var escena = $('[data-mq-escena]', host);
    var arbolHost = $('[data-mq-arbol]', host);

    /* El componente anfitrión inicializa y carga al armar la superficie: el
       recorrido de la bitácora es el real, no uno inventado.
       Al descartar el componente anterior se libera su instancia: no es
       opcional, y es lo que hace que recorrer trabajos no acumule contextos
       gráficos. */
    Fachada.vivas().forEach(function (viejo) { Fachada.destruir(viejo); });
    Fachada.reiniciarBitacora();
    var r = Fachada.inicializar(escena, {});
    estadoDelPanel.id = r.id;
    estadoDelPanel.ejercicioId = 'E-1';
    estadoDelPanel.disposicionPrevia = null;
    estadoDelPanel.veredictoDeterminismo = null;
    estadoDelPanel.veredictoRecorridos = null;

    /* El texto del alumno es la entrada: el visor lo interpreta con su lector
       tolerante y de ahí salen la escena, el árbol y las piezas no dibujadas.
       `t.arbol` queda de respaldo para cuando el visor no está cargado. */
    var res = Fachada.cargarJson(r.id, op.escenaVacia
      ? { texto: '', arbol: [], condicionGeneral: 'TEXTO_NO_LEGIBLE' }
      : { texto: t.textoOriginal, arbol: t.arbol });

    dibujarEscena(escena, res.estructura || [], {
      disposicion: res.disposicion,
      leyendaVacia: res.condicion === 'TEXTO_NO_LEGIBLE'
        ? 'De este texto no se obtuvo ninguna pieza: la instancia queda viva y vacía'
        : null
    });
    /* En el estado «el texto no verifica» la escena queda vacía pero el árbol y
       las observaciones se muestran igual, que es lo que la superficie declara
       y lo que el Product Owner aprobó. Por eso el árbol se pinta con la
       estructura declarada cuando la carga no devolvió ninguna. */
    pintarArbol(arbolHost,
      (res.estructura && res.estructura.length) ? res.estructura : (t.arbol || []),
      res.noDibujadas || []);
    renderObservaciones(host, t.observaciones);
    renderPiezasNoDibujadas(host, res.noDibujadas || []);
    sincronizar(host);
    marcarRed();

    var panel = $('[data-mq-panel-fachada]', host);
    if (panel) {
      pintarPanelDeFachada(panel, { escena: escena, arbol: arbolHost });
    }
  }

  /* ==========================================================================
     Panel del contrato de fachada del visor

     Instrumento de validación de `GeometriaFactory-Visor`, que no tiene
     pantallas propias. Se rotula como instrumento, igual que la barra de
     validación, y no forma parte del producto: en el producto, el componente
     anfitrión invoca las seis funciones sin exhibirlas. Este panel demuestra
     CINCO: `establecerMovimiento` se decidió después de que el Product Owner
     aprobó la maqueta y no fue validada visualmente. Está declarado en
     `Linea-Base-Visual.md` §6.1 y en la sonda `SD-43`.
     ========================================================================== */

  var estadoDelPanel = {
    id: null,                 // identificador de la instancia viva, si la hay
    ejercicioId: 'E-1',
    disposicionPrevia: null,  // firma de la carga anterior, para el determinismo
    veredictoDeterminismo: null,
    veredictoRecorridos: null
  };

  function pintarPanelDeFachada(host, op) {
    op = op || {};
    var ejercicios = D.ejerciciosDeFachada;

    host.innerHTML =
      '<section class="mq-panel-fachada" aria-labelledby="fa-h">' +
        '<p class="mq-panel-fachada__rotulo">' + icono('alerta') +
          'Contrato de fachada del visor — instrumento de validación, no forma parte del producto</p>' +
        '<h2 id="fa-h" class="mq-title">GeometriaFactory-Visor · cinco de las seis funciones</h2>' +
        '<p class="mq-caption mq-margen-arriba-2">El visor no tiene pantallas propias: su ' +
        'superficie pública son seis funciones que este componente anfitrión invoca. Acá se ven cinco: ' +
        '<strong>establecerMovimiento se decidió después de la aprobación de la maqueta y no fue ' +
        'validada visualmente</strong>; el movimiento se gobierna con las dos casillas del área de ' +
        'dibujo. Acá se ve qué ' +
        'cambia en pantalla cuando cada una ocurre, y qué garantiza el contrato.</p>' +

        /* --- ciclo de vida --------------------------------------------- */
        '<div class="mq-fa-linea">' +
          '<span class="mq-meta">Ciclo de vida</span>' +
          '<span data-mq-fa-ciclo></span>' +
          '<span class="mq-caption" data-mq-fa-id></span>' +
        '</div>' +

        /* --- cinco de las seis funciones, como acciones ----------------- */
        '<div class="mq-fa-acciones" role="group" aria-label="Cinco de las seis funciones de la fachada">' +
          '<button type="button" class="mq-btn mq-btn--secundario" data-mq-fa="inicializar">inicializar</button>' +
          '<label class="mq-sr-only" for="fa-texto">Texto a cargar</label>' +
          '<select class="mq-select mq-ancho-corto" id="fa-texto" data-mq-fa-texto>' +
            ejercicios.map(function (e) {
              return '<option value="' + esc(e.id) + '">' + esc(e.rotulo) + '</option>';
            }).join('') +
          '</select>' +
          '<button type="button" class="mq-btn mq-btn--secundario" data-mq-fa="cargarJson">cargarJson</button>' +
          '<button type="button" class="mq-btn mq-btn--secundario" data-mq-fa="seleccionarPieza">seleccionarPieza</button>' +
          '<button type="button" class="mq-btn mq-btn--secundario" data-mq-fa="redimensionar">redimensionar</button>' +
          '<button type="button" class="mq-btn mq-btn--destructivo" data-mq-fa="destruir">destruir</button>' +
        '</div>' +

        /* --- resultado de dibujo, que es lo que cierra G-5 --------------- */
        '<div class="mq-fa-recuentos" data-mq-fa-resultado></div>' +

        /* --- las seis propiedades transversales ------------------------- */
        '<h3 class="mq-meta mq-mt-5">Las seis propiedades transversales</h3>' +
        '<ul class="mq-fa-propiedades" data-mq-fa-propiedades></ul>' +

        /* --- bitácora de invocaciones ----------------------------------- */
        '<h3 class="mq-meta mq-mt-5">Bitácora de invocaciones</h3>' +
        '<ol class="mq-fa-bitacora" data-mq-fa-bitacora aria-live="polite"></ol>' +

        /* --- las siete condiciones -------------------------------------- */
        '<details class="mq-mt-5"><summary class="mq-meta mq-clicable">' +
          'Las siete condiciones del contrato, y con qué estado se demuestra cada una</summary>' +
          '<div class="mq-tabla-envoltorio mq-mt-2"><table class="mq-tabla">' +
            '<thead><tr><th scope="col">Código</th><th scope="col">Curso</th>' +
            '<th scope="col">Cuándo</th><th scope="col">Efecto sobre la instancia</th></tr></thead>' +
            '<tbody>' + D.condicionesDeLaFachada.map(function (c) {
              return '<tr><th scope="row" class="mq-th-plano mq-th-plano--breve">' +
                '<code>' + esc(c.codigo) + '</code></th>' +
                '<td class="mq-caption">' + esc(c.curso) + '</td>' +
                '<td class="mq-caption">' + esc(c.cuando) + '</td>' +
                '<td class="mq-caption">' + esc(c.efecto) + '</td></tr>';
            }).join('') + '</tbody></table></div>' +
          '<p class="mq-caption mq-mt-2">Las ocho filas son siete códigos: ' +
          '<code>ELEMENTO_DE_DIBUJO_INVALIDO</code> tiene dos cursos y no dos códigos. Cada una se ' +
          'alterna desde la barra de validación del pie.</p>' +
        '</details>' +

        '<details class="mq-mt-2"><summary class="mq-meta mq-clicable">' +
          'Qué garantiza cada función</summary>' +
          '<dl class="mq-clave-valor mq-mt-2">' +
            D.funcionesDeLaFachada.map(function (f) {
              return '<dt><code>' + esc(f.firma) + '</code></dt><dd class="mq-caption">' +
                     esc(f.efecto) + ' <em>(' + esc(f.cu) + ')</em></dd>';
            }).join('') +
          '</dl>' +
        '</details>' +
      '</section>';

    cablearPanelDeFachada(host, op);
    refrescarPanelDeFachada(host);
  }

  function cablearPanelDeFachada(host, op) {
    var selector = $('[data-mq-fa-texto]', host);
    selector.value = estadoDelPanel.ejercicioId;
    selector.addEventListener('change', function () { estadoDelPanel.ejercicioId = selector.value; });

    $$('[data-mq-fa]', host).forEach(function (b) {
      b.addEventListener('click', function () {
        var fn = b.getAttribute('data-mq-fa');
        var id = estadoDelPanel.id;

        if (fn === 'inicializar') {
          var r = Fachada.inicializar(op.escena, {});
          if (r.id) { estadoDelPanel.id = r.id; estadoDelPanel.disposicionPrevia = null; }
          dibujarEscena(op.escena, [], { leyendaVacia: 'Instancia viva, sin ninguna pieza cargada' });
          pintarArbol(op.arbol, [], []);
        }

        if (fn === 'cargarJson') {
          var ej = D.ejercicio(estadoDelPanel.ejercicioId);
          var res = Fachada.cargarJson(id, ej);
          if (!res.condicion || res.condicion === 'TEXTO_NO_LEGIBLE') {
            var disp = dibujarEscena(op.escena, res.estructura || [],
              { disposicion: res.disposicion,
                leyendaVacia: res.condicion === 'TEXTO_NO_LEGIBLE'
                  ? 'De este texto no se obtuvo ninguna pieza: la instancia queda viva y vacía'
                  : null });
            pintarArbol(op.arbol, res.estructura || [], res.noDibujadas || []);
            sincronizar(document);
            // Determinismo: se compara la disposición contra la carga anterior.
            var firma = Fachada.firmaDeDisposicion(disp);
            if (estadoDelPanel.disposicionPrevia !== null) {
              estadoDelPanel.veredictoDeterminismo =
                (estadoDelPanel.disposicionPrevia === firma)
                  ? 'idéntica pieza por pieza a la carga anterior'
                  : 'DISTINTA de la carga anterior';
            }
            estadoDelPanel.disposicionPrevia = firma;
          }
        }

        if (fn === 'seleccionarPieza') {
          var inst = Fachada.instancia(id);
          var dib = inst && inst.resultado ? inst.resultado.dibujadas : [];
          var proximo = dib.length ? dib[0].indice : 0;
          if (inst && inst.seleccion !== null && dib.length) {
            var pos = dib.map(function (p) { return String(p.indice); }).indexOf(String(inst.seleccion));
            proximo = dib[(pos + 1) % dib.length].indice;
          }
          var sel = Fachada.seleccionarPieza(id, proximo);
          if (!sel.condicion) { resaltarPorIndice(document, sel.seleccion); }
        }

        if (fn === 'redimensionar') { Fachada.redimensionar(id, {}); }

        if (fn === 'destruir') {
          var d = Fachada.destruir(id);
          if (!d.condicion) {
            estadoDelPanel.id = null;
            estadoDelPanel.disposicionPrevia = null;
            dibujarEscena(op.escena, [], { leyendaVacia: 'Instancia liberada: no queda escena ni identificador válido' });
            pintarArbol(op.arbol, [], []);
          }
        }

        refrescarPanelDeFachada(host);
      });
    });
  }

  /* Atributos que identifican a los controles que este panel reemplaza por
     `innerHTML`. Se usan para devolver el foco al mismo control después de
     refrescar: sin esto, pulsar una de las dos comprobaciones a mano destruía
     el elemento enfocado y el foco caía al `<body>`. */
  var ATRIBUTOS_DE_FOCO = ['data-mq-fa-determinismo', 'data-mq-fa-recorridos', 'data-mq-fa'];

  function marcaDeFoco(host) {
    var activo = document.activeElement;
    if (!activo || !host.contains(activo)) { return null; }
    for (var i = 0; i < ATRIBUTOS_DE_FOCO.length; i++) {
      var at = ATRIBUTOS_DE_FOCO[i];
      if (activo.hasAttribute(at)) {
        var v = activo.getAttribute(at);
        return v ? '[' + at + '="' + v + '"]' : '[' + at + ']';
      }
    }
    return null;
  }

  function devolverFoco(host, marca) {
    if (!marca) { return; }
    var destino = $(marca, host);
    if (destino && destino.focus) { destino.focus(); }
  }

  function refrescarPanelDeFachada(host) {
    var marcaFoco = marcaDeFoco(host);
    var id = estadoDelPanel.id;
    var inst = id ? Fachada.instancia(id) : null;
    var viva = !!(inst && inst.viva);
    var res = inst ? inst.resultado : null;

    var ciclo = !id ? 'Inexistente' : (viva ? 'Viva' : 'Liberada');
    var claseCiclo = ciclo === 'Viva' ? 'mq-insignia--exito'
                   : ciclo === 'Liberada' ? 'mq-insignia--neutro' : 'mq-insignia--atencion';
    $('[data-mq-fa-ciclo]', host).innerHTML =
      '<span class="mq-insignia ' + claseCiclo + '">' + ciclo + '</span>';
    var V0 = Visor();
    var motorReal = !!(V0 && V0.hayCapacidadGrafica());
    $('[data-mq-fa-id]', host).textContent =
      (id ? 'identificador de instancia: ' + id + (viva ? '' : ' (ya no es válido)')
          : 'todavía no hay identificador de instancia') +
      ' · ' + (motorReal
        ? 'escena tridimensional real'
        : 'representación de respaldo: este navegador no ofrece capacidad gráfica');

    /* Resultado de dibujo: dibujadas + no dibujadas = conjunto raíz. Es la
       cuenta que cierra la ausencia de fallo silencioso. */
    var cont = $('[data-mq-fa-resultado]', host);
    if (!res) {
      cont.innerHTML = '<p class="mq-caption mq-sin-margen">Sin resultado de dibujo: ' +
        'todavía no se cargó ningún texto en esta instancia.</p>';
    } else {
      var d = res.dibujadas.length, nd = res.noDibujadas.length, raiz = res.raiz || (d + nd);
      var cierra = (d + nd) === raiz;
      cont.innerHTML =
        '<span class="mq-insignia mq-insignia--info">conjunto raíz ' + raiz + '</span>' +
        '<span class="mq-insignia mq-insignia--exito">dibujadas ' + d + '</span>' +
        '<span class="mq-insignia ' + (nd ? 'mq-insignia--atencion' : 'mq-insignia--neutro') + '">' +
          'no dibujadas enumeradas ' + nd + '</span>' +
        '<span class="mq-insignia ' + (cierra ? 'mq-insignia--exito' : 'mq-insignia--peligro') + '">' +
          'sin registro ' + (raiz - d - nd) + '</span>' +
        (res.condicion ? '<span class="mq-insignia mq-insignia--atencion">' + esc(res.condicion) + '</span>' : '');
    }

    /* Las seis propiedades, con lo que la maqueta puede medir de verdad. */
    var props = D.propiedadesTransversales;
    var recursos = Visor() ? Visor().recursos() : null;
    var medidas = {
      'Cero red': {
        valor: peticionesDesdeLaMarca() + ' peticiones desde que se dibujó la escena',
        ok: peticionesDesdeLaMarca() === 0
      },
      'Cero persistencia': {
        valor: clavesDeLaFachada() + ' claves ajenas a la maqueta en el almacenamiento',
        ok: clavesDeLaFachada() === 0
      },
      'Se ejercita sin backend': {
        valor: 'esta maqueta no tiene backend: el recorrido completo corre igual',
        ok: true
      },
      'Disposición determinista': {
        valor: estadoDelPanel.veredictoDeterminismo ||
               'cargá el mismo texto dos veces para comparar las dos disposiciones',
        ok: estadoDelPanel.veredictoDeterminismo === null ||
            estadoDelPanel.veredictoDeterminismo.indexOf('idéntica') === 0
      },
      /* Se mide contra lo que el visor informa que sigue vivo: geometrías,
         materiales y contextos gráficos. No se lee el interior de la escena:
         el visor lleva su propia cuenta y la expone. */
      'Liberación de recursos': {
        valor: estadoDelPanel.veredictoRecorridos ||
               (plural(Fachada.vivas().length, 'instancia viva', 'instancias vivas') + ' · ' +
                plural(Fachada.liberadas(), 'liberada', 'liberadas') +
                (recursos ? ' · ' + plural(recursos.geometrias, 'geometría', 'geometrías') + ', ' +
                            plural(recursos.materiales, 'material', 'materiales') + ' y ' +
                            plural(recursos.contextos, 'contexto gráfico', 'contextos gráficos') + ' vivos' : '')),
        ok: Fachada.vivas().length <= 1 && (!recursos || recursos.contextos <= 1)
      },
      'Ausencia de fallo silencioso': {
        valor: res ? ((res.dibujadas.length + res.noDibujadas.length) + ' de ' +
                      (res.raiz || 0) + ' piezas del conjunto raíz con registro')
                   : 'cargá un texto para medirlo',
        ok: !res || (res.dibujadas.length + res.noDibujadas.length) === (res.raiz || 0)
      }
    };

    $('[data-mq-fa-propiedades]', host).innerHTML = props.map(function (pr) {
      var m = medidas[pr.nombre] || { valor: '—', ok: true };
      return '<li>' +
        '<span class="mq-insignia ' + (m.ok ? 'mq-insignia--exito' : 'mq-insignia--peligro') + '">' +
          (m.ok ? 'cumple' : 'no cumple') + '</span> ' +
        '<strong>' + esc(pr.nombre) + '</strong> — <span class="mq-caption">' + esc(m.valor) + '</span>' +
        '<br><span class="mq-caption mq-atenuado">Umbral: ' + esc(pr.umbral) + '</span>' +
      '</li>';
    }).join('') +
      '<li class="mq-mt-2">' +
        '<button type="button" class="mq-btn mq-btn--secundario" data-mq-fa-determinismo>' +
          'Volver a cargar el mismo texto y comparar</button> ' +
        '<button type="button" class="mq-btn mq-btn--secundario" data-mq-fa-recorridos>' +
          'Hacer 10 recorridos de ida y vuelta</button>' +
      '</li>';

    $('[data-mq-fa-bitacora]', host).innerHTML = Fachada.bitacora().length
      ? Fachada.bitacora().slice().reverse().map(function (l) {
          return '<li><code>' + esc(l.llamada) + '</code> → ' + esc(l.resultado) +
            (l.condicion ? ' <span class="mq-insignia mq-insignia--atencion">' + esc(l.condicion) + '</span>' : '') +
          '</li>';
        }).join('')
      : '<li class="mq-caption">Todavía no se invocó ninguna función.</li>';

    // Las dos comprobaciones que el humano corre a mano.
    var botonDet = $('[data-mq-fa-determinismo]', host);
    var botonRec = $('[data-mq-fa-recorridos]', host);
    var escena = $('[data-mq-escena]');
    var arbol = $('[data-mq-arbol]');

    botonDet.addEventListener('click', function () {
      if (!estadoDelPanel.id) {
        var nueva = Fachada.inicializar(escena, {});
        estadoDelPanel.id = nueva.id;
        estadoDelPanel.disposicionPrevia = null;
      }
      var boton = $$('[data-mq-fa]', host).filter(function (b) {
        return b.getAttribute('data-mq-fa') === 'cargarJson';
      })[0];
      boton.click(); boton.click();     // dos cargas del mismo texto
    });

    botonRec.addEventListener('click', function () {
      /* Se libera la instancia en curso antes de empezar: si no, los diez
         recorridos dejarían una instancia viva sin dueño. */
      if (estadoDelPanel.id) { Fachada.destruir(estadoDelPanel.id); estadoDelPanel.id = null; }
      var ej = D.ejercicio(estadoDelPanel.ejercicioId);
      var previa = null, iguales = true;
      for (var i = 0; i < 10; i++) {
        var r = Fachada.inicializar(escena, {});
        var res2 = Fachada.cargarJson(r.id, ej);
        dibujarEscena(escena, res2.estructura || [], { disposicion: res2.disposicion });
        var firma = Fachada.firmaDeDisposicion(res2.disposicion);
        if (previa !== null && previa !== firma) { iguales = false; }
        previa = firma;
        Fachada.seleccionarPieza(r.id, (res2.dibujadas[0] || {}).indice);
        Fachada.redimensionar(r.id, {});
        Fachada.destruir(r.id);
      }
      estadoDelPanel.id = null;
      var V = Visor();
      var quedan = V ? V.recursos() : null;
      estadoDelPanel.veredictoRecorridos =
        '10 recorridos completos · ' +
        plural(Fachada.vivas().length, 'instancia viva', 'instancias vivas') + ' al final · ' +
        plural(Fachada.liberadas(), 'liberada', 'liberadas') + ' en total · disposición ' +
        (iguales ? 'idéntica en los 10' : 'DISTINTA entre recorridos') +
        (quedan ? ' · quedan ' + plural(quedan.geometrias, 'geometría', 'geometrías') + ', ' +
                  plural(quedan.materiales, 'material', 'materiales') + ' y ' +
                  plural(quedan.contextos, 'contexto gráfico', 'contextos gráficos') + ' vivos' : '');
      dibujarEscena(escena, [], { leyendaVacia: 'Instancia liberada tras los 10 recorridos' });
      pintarArbol(arbol, [], []);
      refrescarPanelDeFachada(host);
    });

    /* El foco vuelve al control que disparó el refresco. */
    devolverFoco(host, marcaFoco);
  }

  /* Las tres condiciones que no cambian la superficie y sólo se observan en la
     bitácora de la fachada, más la que sí cambia lo dibujado. Se disparan desde
     la barra de validación, como el resto de los estados. */
  function demostrarCondicionDeFachada(que) {
    var host = $('[data-mq-panel-fachada]');
    if (!host) { return; }
    var escena = $('[data-mq-escena]');
    var arbol = $('[data-mq-arbol]');
    var id = estadoDelPanel.id;

    if (que === 'dimension-no-legible') {
      /* Una pieza de tipo dibujable que no expone su dimensión: no se dibuja y
         queda enumerada. Las otras cinco sí se dibujan. */
      var ej = D.ejercicio('DIM');
      estadoDelPanel.ejercicioId = 'DIM';
      var res = Fachada.cargarJson(id, ej);
      if (!res.condicion) {
        dibujarEscena(escena, res.estructura, { disposicion: res.disposicion });
        pintarArbol(arbol, res.estructura, res.noDibujadas);
        renderPiezasNoDibujadas(document, res.noDibujadas);
        sincronizar(document);
      }
    }

    if (que === 'instancia-desconocida') {
      /* Un identificador que no corresponde a ninguna instancia viva: ninguna
         instancia cambia y no se rompe la que sí existe. */
      Fachada.seleccionarPieza('vis-liberada', 0);
    }

    if (que === 'elemento-sin-tamano-ajuste') {
      /* Segundo curso: la instancia sigue viva, con su escena y su selección
         intactas, y la persona no ve ningún aviso. */
      Fachada.redimensionar(id, { elementoValido: false });
    }

    refrescarPanelDeFachada(host);
  }

  /* Resalta por índice sobre la escena real —o sobre el respaldo plano si no
     hay capacidad gráfica— y marca el nodo del árbol por el mismo índice. */
  function resaltarPorIndice(raiz, indice) {
    var motor = motorDe($('[data-mq-escena]'));
    if (motor) { motor.seleccionar(indice); }
    $$('.mq-pieza', raiz).forEach(function (g) {
      g.setAttribute('aria-pressed', String(g.getAttribute('data-mq-indice') === String(indice)));
    });
    $$('[data-mq-nodo]', raiz).forEach(function (b) {
      var activo = b.getAttribute('data-mq-nodo') === String(indice);
      /* UN SOLO PORTADOR DE ROL Y DE ESTADO: `data-mq-nodo` vive en el
         `<li role="treeitem">`, de modo que escribir también en su
         `parentNode` marcaría el `<ul role="group">`, que no es
         seleccionable. Corregido por el hallazgo `F26-22` de
         `SDD/Docs/Audit/F26-Propagacion-r1.md`; el camino gemelo
         `seleccionar()` ya estaba bien. */
      b.setAttribute('aria-selected', String(activo));
    });
  }

  /* ==========================================================================
     Bloque de decisión (Wireframes-Resolucion-Del-Trabajo.md §2 y §3)

     No tiene ruta propia: se aloja dentro de la vista de trabajo, debajo de la
     cabecera del trabajo y ANTES de las cuatro partes. La decisión se toma
     después de mirar, pero el control tiene que estar donde se lo encuentre
     sin buscarlo.

     Sólo aparece para el administrador y sólo mientras el trabajo está en
     estado `Pendiente`. En cualquier otro caso el bloque no se dibuja.
     ========================================================================== */

  function pintarBloqueDeDecision(host, t, op) {
    op = op || {};
    var conEstados = op.conEstados !== false;
    var duenio = D.cuenta(t.cuentaId);

    function est(lista) { return conEstados ? ' data-mq-estado="' + lista + '"' : ''; }

    var esAdministrador = op.papel === 'administrador';
    var resoluble = esAdministrador && t.estado === 'Pendiente';
    var resuelto  = esAdministrador && (t.estado === 'Finalizado' || t.estado === 'Rechazado');

    // Para el alumno dueño el bloque no se dibuja, ni siquiera inhabilitado.
    if (!conEstados && !resoluble && !resuelto) { host.innerHTML = ''; return; }

    var bloqueResoluble =
      '<section class="mq-tarjeta" aria-labelledby="rt-res-h"' +
        est('resoluble confirmando-desenlace aplicando confirmando-retiro reconectando') + '>' +
        '<h2 id="rt-res-h" class="mq-title">Resolver esta entrega</h2>' +
        '<div class="mq-campo mq-mt-4 mq-ancho-ancho">' +
          /* La palabra «opcional» va en la ETIQUETA y no sólo en el texto de
             apoyo: un lector de pantalla que anuncie sólo la etiqueta tiene
             que enterarse igual. */
          '<label for="rt-comentario">Comentario para el alumno (opcional)</label>' +
          '<textarea class="mq-textarea mq-textarea--media" id="rt-comentario" ' +
            'aria-describedby="rt-destino"></textarea>' +
          '<span class="mq-requisito" id="rt-destino">' + esc(D.textos.notaDestinoComentario) + '</span>' +
        '</div>' +
        '<div class="mq-acciones-pie mq-justificar-inicio">' +
          '<button type="button" class="mq-btn mq-btn--primario" data-mq-decision="Aprobar"' +
            est('resoluble confirmando-desenlace confirmando-retiro reconectando') + '>Aprobar</button>' +
          /* Rechazar es una decisión legítima, no una destrucción: no es un
             control destructivo. Es la misma solicitud con el otro valor. */
          '<button type="button" class="mq-btn mq-btn--secundario" data-mq-decision="Rechazar"' +
            est('resoluble confirmando-desenlace confirmando-retiro reconectando') + '>Rechazar</button>' +
          /* El control en curso sólo existe donde su estado se demuestra. */
          (conEstados
            ? '<button type="button" class="mq-btn mq-btn--primario" disabled' +
              est('aplicando') + '><span class="mq-spinner"></span> Aplicando…</button>'
            : '') +
          /* La acción de retirar queda visualmente separada de las dos
             decisiones, con su color de peligro y su propio diálogo. */
          '<span class="mq-flexible"></span>' +
          '<button type="button" class="mq-btn mq-btn--destructivo" data-mq-retirar ' +
            'aria-label="Retirar el trabajo «' + esc(t.nombre) + '»">Retirar</button>' +
        '</div>' +
      '</section>';

    var bloqueResuelto =
      '<section class="mq-tarjeta" aria-labelledby="rt-resuelto-h"' +
        est('no-resoluble-estado exito-desenlace') + '>' +
        '<h2 id="rt-resuelto-h" class="mq-title">Resuelto el ' +
          '<span class="mq-num">' + esc(t.fechaDesenlace || '—') + '</span> · ' +
          insignia(t.estado === 'Pendiente' ? 'Finalizado' : t.estado) + '</h2>' +
        '<p class="mq-caption mq-mt-2">Esta entrega ya tiene desenlace y no se ' +
        'puede cambiar.</p>' +
        '<div class="mq-acciones-pie mq-justificar-inicio">' +
          '<button type="button" class="mq-btn mq-btn--destructivo" data-mq-retirar ' +
            'aria-label="Retirar el trabajo «' + esc(t.nombre) + '»">Retirar</button>' +
        '</div>' +
      '</section>';

    var notaAlumno = conEstados
      ? '<p class="mq-banda mq-banda--info" data-mq-estado="no-resoluble-papel">' +
        '<span><strong>El bloque no se dibuja.</strong> Quien mira es el alumno dueño, y para él la ' +
        'superficie es exactamente la vista de trabajo, sin ningún control de decisión. Este ' +
        'recuadro es una nota de la maqueta y no forma parte del producto.</span></p>'
      : '';

    host.innerHTML =
      (conEstados || resoluble ? bloqueResoluble : '') +
      (conEstados || resuelto  ? bloqueResuelto  : '') +
      notaAlumno;

    inyectarDialogosDeDecision(t, duenio);
    cablearDecision(host, t, duenio);
    activarDialogos(document.body);
  }

  function inyectarDialogosDeDecision(t, duenio) {
    if ($('#rt-dlg-desenlace')) {
      // Ya existen: sólo se refresca a qué trabajo se refieren.
      $$('[data-mq-dlg-trabajo]').forEach(function (e) { e.textContent = t.nombre; });
      $$('[data-mq-dlg-duenio]').forEach(function (e) { e.textContent = duenio.nombre; });
      return;
    }
    var html =
      /* Declara la terminalidad ANTES de aplicarla, y la declaración se asocia
         por descripción accesible a la acción que confirma. */
      '<dialog class="mq-dialogo" id="rt-dlg-desenlace" aria-labelledby="rt-dlg-h">' +
        '<h2 id="rt-dlg-h"><span data-mq-dlg-decision></span> «<span data-mq-dlg-trabajo>' + esc(t.nombre) + '</span>»</h2>' +
        '<p id="rt-dlg-terminal" data-mq-dlg-terminalidad></p>' +
        '<p><strong>Comentario:</strong> «<span data-mq-dlg-comentario></span>»</p>' +
        '<div class="mq-acciones-pie">' +
          '<button type="button" class="mq-btn mq-btn--secundario" data-mq-cierra-dialogo>Cancelar</button>' +
          '<button type="button" class="mq-btn mq-btn--primario" aria-describedby="rt-dlg-terminal" ' +
            'data-mq-dlg-confirmar></button>' +
        '</div></dialog>' +
      '<dialog class="mq-dialogo" id="rt-dlg-retiro" aria-labelledby="rt-dlg-retiro-h">' +
        '<h2 id="rt-dlg-retiro-h">Retirar «<span data-mq-dlg-trabajo>' + esc(t.nombre) + '</span>»</h2>' +
        '<p class="mq-banda mq-banda--atencion" id="rt-dlg-arrastre">El trabajo deja de existir y ' +
        'desaparece también del listado de <span data-mq-dlg-duenio>' + esc(duenio.nombre) + '</span>. No se puede deshacer.</p>' +
        '<div class="mq-acciones-pie">' +
          '<button type="button" class="mq-btn mq-btn--secundario" data-mq-cierra-dialogo>Cancelar</button>' +
          '<button type="button" class="mq-btn mq-btn--destructivo" aria-describedby="rt-dlg-arrastre">' +
            'Retirar</button>' +
        '</div></dialog>';
    var cont = document.createElement('div');
    cont.innerHTML = html;
    while (cont.firstChild) { document.body.appendChild(cont.firstChild); }
  }

  function cablearDecision(host, t, duenio) {
    var comentario = $('#rt-comentario', host);

    $$('[data-mq-decision]', host).forEach(function (b) {
      b.setAttribute('data-mq-abre-dialogo', 'rt-dlg-desenlace');
      b.addEventListener('click', function () {
        var d = b.getAttribute('data-mq-decision');
        var destino = d === 'Aprobar' ? 'Finalizado' : 'Rechazado';
        $('[data-mq-dlg-decision]').textContent = d;
        $('[data-mq-dlg-terminalidad]').textContent =
          'El trabajo pasa a ' + destino + '. Es definitivo: no se puede volver atrás. Si ' +
          duenio.nombre + ' quiere corregir, tiene que cargar un trabajo nuevo.';
        $('[data-mq-dlg-comentario]').textContent = comentario ? comentario.value : '';
        $('[data-mq-dlg-confirmar]').textContent = d;
      });
    });

    $$('[data-mq-retirar]', host).forEach(function (b) {
      b.setAttribute('data-mq-abre-dialogo', 'rt-dlg-retiro');
    });

    /* Confirmar el desenlace o el retiro devuelve al listado de la comisión,
       que ya refleja el estado nuevo (Experiencia-De-Uso §3.7). */
    var confirmar = $('[data-mq-dlg-confirmar]');
    if (confirmar && !confirmar.dataset.cableado) {
      confirmar.dataset.cableado = '1';
      confirmar.addEventListener('click', function () {
        $('#rt-dlg-desenlace').close();
        location.href = 'Listado-De-La-Comision.html?estado=con-datos';
      });
    }
    var retirar = $('#rt-dlg-retiro .mq-btn--destructivo');
    if (retirar && !retirar.dataset.cableado) {
      retirar.dataset.cableado = '1';
      retirar.addEventListener('click', function () {
        $('#rt-dlg-retiro').close();
        location.href = 'Listado-De-La-Comision.html?estado=con-datos';
      });
    }
  }

  /* ==========================================================================
     Diálogos
     ========================================================================== */

  function activarDialogos(raiz) {
    $$('[data-mq-abre-dialogo]', raiz).forEach(function (boton) {
      if (boton.dataset.mqDlgCableado) { return; }
      boton.dataset.mqDlgCableado = '1';
      boton.addEventListener('click', function () {
        var dlg = document.getElementById(boton.getAttribute('data-mq-abre-dialogo'));
        if (!dlg) { return; }
        dlg.returnValue = '';
        if (dlg.showModal) { dlg.showModal(); } else { dlg.setAttribute('open', ''); }
        var primero = $('input, button, [href]', dlg);
        if (primero) { primero.focus(); }
        dlg.addEventListener('close', function alVolver() {
          dlg.removeEventListener('close', alVolver);
          boton.focus();                  // el foco vuelve al control que lo abrió
        });
      });
    });
    $$('[data-mq-cierra-dialogo]', raiz).forEach(function (b) {
      if (b.dataset.mqDlgCableado) { return; }
      b.dataset.mqDlgCableado = '1';
      b.addEventListener('click', function () {
        var dlg = b.closest('dialog');
        if (dlg) { dlg.close ? dlg.close() : dlg.removeAttribute('open'); }
      });
    });
  }

  /* ==========================================================================
     Arranque
     ========================================================================== */

  function pintarSelloDeMaqueta() {
    var pie = document.createElement('footer');
    pie.className = 'mq-sello-maqueta';
    pie.innerHTML = 'Maqueta de validación visual · <strong>' + esc(SELLO_MAQUETA.proyectoDeCodigo) +
      '</strong> · modelo: ' + esc(SELLO_MAQUETA.modelo) +
      ' · iteración del ' + esc(SELLO_MAQUETA.fechaIteracion) +
      ' · ' + esc(SELLO_MAQUETA.revision);
    document.body.appendChild(pie);
  }

  function estadoInicial() {
    var pedido = new URLSearchParams(location.search).get('estado');
    if (pedido && estadosDeLaSuperficie.indexOf(pedido) !== -1) { return pedido; }
    var guardado = null;
    try { guardado = sessionStorage.getItem('mq-estado:' + document.body.dataset.superficie); } catch (e) {}
    if (guardado && estadosDeLaSuperficie.indexOf(guardado) !== -1) { return guardado; }
    var preferido = document.body.dataset.estadoInicial;
    if (preferido && estadosDeLaSuperficie.indexOf(preferido) !== -1) { return preferido; }
    return estadosDeLaSuperficie[0];
  }

  function arrancar() {
    var b = document.body;

    // 1. Armazón
    var nav = $('[data-mq-sidebar]');
    if (nav) { pintarBarraLateral(nav, papelVigente(), b.dataset.destino || ''); }

    var selloAcceso = $('[data-mq-sello-acceso]');
    if (selloAcceso) { selloAcceso.innerHTML = selloDeVersionHTML('lienzo'); }

    // 2. Cartel de reconexión, transversal a las once superficies
    if (!$('[data-mq-cartel-reconexion]')) {
      var cartel = crear('<div class="mq-cartel-reconexion" data-mq-cartel-reconexion role="status" hidden>' +
        icono('reintentar', 'mq-ico--20') + '<span>' + esc(D.textos.reconexion) + '</span>' +
        '<button type="button" class="mq-btn mq-btn--secundario mq-ml-auto">Reintentar la conexión</button></div>');
      b.insertBefore(cartel, b.firstChild);
    }

    // 2.b Enlace de salto: lleva al primer <main> visible, porque algunas
    //     superficies tienen dos shells y sólo uno está a la vista.
    var salto = $('.mq-skip');
    if (salto) {
      salto.addEventListener('click', function (ev) {
        var visible = $$('main').filter(function (m) { return m.offsetParent !== null; })[0];
        if (!visible) { return; }
        ev.preventDefault();
        visible.setAttribute('tabindex', '-1');
        visible.focus();
      });
    }

    // 3. Render específico de la superficie
    if (typeof global.renderSuperficie === 'function') { global.renderSuperficie(Maqueta, D); }

    // 4. Estados y barra de validación
    estadosDeLaSuperficie = relevarEstados();
    pintarBarraDeValidacion();
    activarSellos(document);
    activarDialogos(document);
    sincronizar(document);
    if (estadosDeLaSuperficie.length) { aplicarEstado(estadoInicial()); }

    pintarSelloDeMaqueta();
  }

  /* ========================================================================== */

  var Maqueta = {
    icono: icono,
    esc: esc,
    $: $, $$: $$,
    insignia: insignia,
    plural: plural,
    dibujarEscena: dibujarEscena,
    nodosDeTexto: nodosDeTexto,
    liberarMotor: liberarMotor,
    pintarArbol: pintarArbol,
    sincronizar: sincronizar,
    renderListadoDeTrabajos: renderListadoDeTrabajos,
    renderObservaciones: renderObservaciones,
    renderPiezasNoDibujadas: renderPiezasNoDibujadas,
    pintarCuatroPartes: pintarCuatroPartes,
    pintarPanelDeFachada: pintarPanelDeFachada,
    fachada: Fachada,
    resaltarPorIndice: resaltarPorIndice,
    demostrarCondicionDeFachada: demostrarCondicionDeFachada,
    pintarBloqueDeDecision: pintarBloqueDeDecision,
    parametro: parametro,
    papelVigente: papelVigente,
    trabajoVigente: trabajoVigente,
    listadoDelPapel: listadoDelPapel,
    destinoDeRegreso: destinoDeRegreso,
    rotuloDeRegreso: rotuloDeRegreso,
    hrefTrabajo: hrefTrabajo,
    aplicarEstado: function (id) { aplicarEstado(id); },
    sello: SELLO_MAQUETA
  };
  global.Maqueta = Maqueta;

  if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', arrancar);
  } else {
    arrancar();
  }

})(window);
