// ============================================================================
// surface-interaction.js — EL ÚNICO GUION PROPIO DE LA PIEZA PÚBLICA.
//
// QUÉ ES Y QUÉ NO ES. Es interacción de superficie, y nada más: cuatro cosas que
// bajo render estático no se pueden hacer sin un guion, y que el Product Owner
// autorizó una por una sobre `Panel-De-Cuentas` y `Registro-De-Cuenta`:
//
//   1. Copiar al portapapeles en un solo gesto.
//   2. Dibujar un estado en curso mientras la operación viaja.
//   3. Mantener una acción inhabilitada hasta que lo tecleado coincide.
//   4. Cerrar un diálogo con la tecla de escape, y confinarle el foco.
//
// LO QUE ESTE ARCHIVO NO HACE, Y NO ES UNA PROMESA SINO UNA PUERTA. No sale a la
// red —ni al servicio de datos ni a ninguna otra parte, que es `RA-01`—, no toca
// el almacenamiento persistente del navegador, y no ve la marca de sesión: la
// marca es `HttpOnly` y `Web ADR-03` §2 lo exige así, de modo que ni siquiera
// tiene con qué mirarla. `scripts/verify-stage-c.sh` C-4 lo cuadra contra este
// archivo, y su lista de atributos autorizados es la de §6.17.3 de la norma de
// nomenclatura.
//
// NO LLEVA NI UN TEXTO DE PRODUCTO ADENTRO. Todo lo que se dibuja lo escribe el
// servidor en el marcado, en los atributos que este guion lee. Un guion con
// textos propios sería una segunda fuente de la palabra que la pantalla dice, y
// los estados en curso ya están escritos en los wireframes.
//
// MEJORA PROGRESIVA, Y SE PRUEBA. Nada de lo que hay acá es necesario para que
// las cuatro superficies funcionen: sin este archivo, las pantallas se comportan
// exactamente como antes de que existiera —el botón de copiado no aparece, no
// hay indicador, la acción destructiva está habilitada y la comparación la hace
// el servidor, y los diálogos se cierran por su propio control de salida—. Es lo
// que `AccountLifecycleWebSurfaceTests` verifica, porque ejercita las superficies
// sobre HTTP de verdad y sin ningún motor de guiones.
// ============================================================================

'use strict';

(function () {
    /**
     * Lo ya mejorado, para no volver a mejorarlo.
     *
     * NO SE MARCA CON UN ATRIBUTO, y es deliberado por dos motivos: la lista de
     * atributos autorizados de §6.17.3 es cerrada y éste no sería ninguno de los
     * nueve, y escribir un atributo dispararía al observador que llama a esta
     * misma función. Se lleva acá, fuera del documento, y se descarta solo
     * cuando el marco reemplaza el elemento.
     */
    const enhanced = new WeakSet();

    /**
     * Si el portapapeles está disponible.
     *
     * EXIGE CONTEXTO SEGURO, Y EN DESARROLLO LOCAL SOBRE `http://` NO LO HAY. El
     * caso se maneja y no se ignora: sin portapapeles NO SE DIBUJA NINGÚN BOTÓN
     * —un control que no hace nada es peor que no tenerlo— y en su lugar se
     * dibuja el aviso que el marcado trae.
     */
    function clipboardIsAvailable() {
        return window.isSecureContext === true
            && typeof navigator !== 'undefined'
            && navigator.clipboard !== undefined
            && typeof navigator.clipboard.writeText === 'function';
    }

    /** Los elementos que pueden tomar el foco dentro de un contenedor, en orden. */
    function focusablesOf(container) {
        const selector = 'a[href], button:not([disabled]), input:not([disabled]),'
            + ' select:not([disabled]), textarea:not([disabled]), [tabindex]:not([tabindex="-1"])';

        return Array.prototype.slice.call(container.querySelectorAll(selector))
            .filter(function (element) {
                return element.offsetParent !== null || element === document.activeElement;
            });
    }

    /**
     * Cierra el diálogo SIN EJECUTAR NINGUNA ACCIÓN.
     *
     * Hace exactamente lo que hace el control de salida que el diálogo ya tiene
     * —«Cancelar», «Listo»—, activándolo. No puede hacer otra cosa: no conoce
     * ninguna operación y no tiene a quién pedírsela.
     */
    function dismissDialog(dialog) {
        // HAY UN DIALOGO QUE LA TECLA DE ESCAPE NO PUEDE CERRAR, y la excepción tiene nombre
        // propio. En los diálogos de baja y de reseteo, «Cancelar» no pierde nada: escapar
        // equivale a arrepentirse. Pero el de la contraseña provisoria es **la única vez que esa
        // clave se ve** —el wireframe lo declara así y el propio cartel lo dice: «No se vuelve a
        // mostrar»—, y su control de salida es un enlace que NAVEGA. Ahí escapar no es
        // arrepentirse: es tirar a la basura un dato irrecuperable, sin pregunta y sin aviso.
        //
        // Medido por el peritaje del 2026-09-02:
        //
        //     después:     {"encabezados":["Contraseña provisoria de Ana Diaz"]}
        //     tras Escape: https://localhost:5296/cuentas · {"dialogos":0}
        //
        // NO ES UN APARTAMIENTO DE ACCESIBILIDAD: la guía de patrones exceptúa expresamente el
        // cierre con escape cuando cerrar pierde datos. Lo que sí sería un defecto es la
        // alternativa que se descartó —pedir una confirmación para escapar—, porque agrega un
        // paso a la persona que ya está mirando la clave que necesita.
        if (dialog.hasAttribute('data-gf-dialog-irreversible')) {
            return;
        }

        const exit = dialog.querySelector('[data-gf-dialog-dismiss]');

        if (exit !== null) {
            exit.click();
        }
    }

    /** Confina el foco dentro del diálogo mientras está abierto. */
    function trapFocus(dialog, event) {
        const focusables = focusablesOf(dialog);

        if (focusables.length === 0) {
            return;
        }

        const first = focusables[0];
        const last = focusables[focusables.length - 1];

        if (event.shiftKey && document.activeElement === first) {
            event.preventDefault();
            last.focus();
        } else if (!event.shiftKey && document.activeElement === last) {
            event.preventDefault();
            first.focus();
        }
    }

    /**
     * Dibuja el estado en curso sobre la acción primaria del formulario que se
     * está enviando, con el texto que el marcado declara.
     *
     * NO CANCELA NI RETIENE EL ENVÍO: se aplica después de que el envío salió,
     * de modo que inhabilitar el control no pueda quitarle un campo a la
     * solicitud. Previene el segundo disparo, que es lo que el estado protege.
     */
    function markPending(form) {
        const label = form.getAttribute('data-gf-pending');
        const action = form.querySelector('button[type="submit"]');

        if (label === null || label === '' || action === null || action.disabled) {
            return;
        }

        window.setTimeout(function () {
            // LA MARCA DE OCUPADO VA PRIMERO, y no es cosmética: es lo que le dice a
            // `guardConfirmationMatch` que no toque esta acción. Sin eso, el envío de
            // la baja quedaba en curso y el acotamiento la volvía a habilitar, que es
            // exactamente el doble disparo que este estado existe para prevenir.
            form.setAttribute('aria-busy', 'true');
            action.disabled = true;
            action.setAttribute('aria-disabled', 'true');
            action.textContent = '';

            const indicator = document.createElement('span');
            indicator.className = 'gf-spinner';
            indicator.setAttribute('aria-hidden', 'true');
            action.appendChild(indicator);

            const announcement = document.createElement('span');
            announcement.textContent = label;
            action.appendChild(announcement);
        }, 0);
    }

    /**
     * Mantiene la acción inhabilitada hasta que lo tecleado coincide.
     *
     * ES COMODIDAD DE SUPERFICIE Y NO LA DEFENSA, y conviene que quede escrito
     * acá también: quien compara de verdad, y quien rechaza, es el servicio de
     * datos. Una solicitud forzada que no pase por esta pantalla se rechaza
     * igual, y hay una prueba que lo fija. Por eso la comparación es permisiva
     * —recorta los blancos e ignora mayúsculas—: de más no habilita nada, y de
     * menos trabaría un envío que el servicio sí aceptaría.
     */
    function guardConfirmationMatch(action) {
        const fieldId = action.getAttribute('data-gf-match-input');
        const expected = action.getAttribute('data-gf-match-value');
        const field = fieldId === null ? null : document.getElementById(fieldId);

        if (field === null || expected === null) {
            return;
        }

        // CON EL ENVÍO EN CURSO, ESTA FUNCIÓN NO TOCA NADA. El estado en curso ya
        // inhabilitó la acción, y volver a evaluarla la rehabilitaría: sería el doble
        // disparo que ese estado previene.
        if (action.form !== null && action.form.getAttribute('aria-busy') === 'true') {
            return;
        }

        const matches = field.value.trim().toLowerCase() === expected.trim().toLowerCase();

        // SÓLO SE ESCRIBE SI CAMBIA. Escribir el mismo valor igual dispara al
        // observador, y el observador vuelve a llamar acá: sería un lazo.
        if (action.disabled === matches) {
            action.disabled = !matches;
            action.setAttribute('aria-disabled', matches ? 'false' : 'true');
        }
    }

    /**
     * Injerta la acción de copiado, o el aviso honesto si no hay portapapeles.
     *
     * El botón LO PONE ESTE GUION y no el marcado: así, sin guion, no queda un
     * control muerto en la pantalla. El texto del campo sigue siendo
     * seleccionable y anunciable carácter por carácter en los dos casos, que es
     * lo que el wireframe §7 declara irrenunciable.
     */
    function attachCopyAction(holder) {
        if (enhanced.has(holder)) {
            return;
        }

        enhanced.add(holder);

        const sourceId = holder.getAttribute('data-gf-copy-source');
        const source = sourceId === null ? null : document.getElementById(sourceId);

        if (source === null) {
            return;
        }

        if (!clipboardIsAvailable()) {
            const notice = document.createElement('p');
            notice.className = 'gf-caption';
            notice.textContent = holder.getAttribute('data-gf-copy-unavailable') || '';
            holder.appendChild(notice);
            return;
        }

        const action = document.createElement('button');
        action.type = 'button';
        action.className = 'gf-btn gf-btn--secondary';
        action.textContent = holder.getAttribute('data-gf-copy-label') || '';

        const acknowledgement = document.createElement('span');
        acknowledgement.className = 'gf-caption';
        acknowledgement.setAttribute('role', 'status');

        action.addEventListener('click', function () {
            navigator.clipboard.writeText(source.value).then(function () {
                acknowledgement.textContent = holder.getAttribute('data-gf-copy-done') || '';
            }, function () {
                acknowledgement.textContent = holder.getAttribute('data-gf-copy-unavailable') || '';
            });
        });

        holder.appendChild(action);
        holder.appendChild(acknowledgement);
    }

    /**
     * El ÚNICO punto donde este guion toca el documento.
     *
     * Se vuelve a correr cuando el marcado cambia, que es lo que hace que la
     * mejora sobreviva a una navegación mejorada del marco, que reemplaza el
     * documento sin volver a ejecutar los guiones de la página.
     */
    function applyEnhancements() {
        const holders = document.querySelectorAll('[data-gf-copy-source]');

        for (let index = 0; index < holders.length; index++) {
            attachCopyAction(holders[index]);
        }

        const guarded = document.querySelectorAll('[data-gf-match-input]');

        for (let index = 0; index < guarded.length; index++) {
            guardConfirmationMatch(guarded[index]);
        }

        const dialog = document.querySelector('[data-gf-dialog]');

        if (dialog !== null && !enhanced.has(dialog)) {
            enhanced.add(dialog);
            const focusables = focusablesOf(dialog);

            if (focusables.length > 0) {
                focusables[0].focus();
            }
        }

        // ---- Diálogos que piden CAPA SUPERIOR -----------------------------
        // POR QUÉ HACE FALTA, y está medido. Un `<dialog>` con el atributo
        // `open` NO es modal: queda en el flujo normal del documento, con el
        // apilado que le toca por su lugar en el marcado, y **todo lo que se
        // pinta después lo tapa**. En la vista de trabajo eso pasa de verdad:
        // el lienzo de three.js vive en un hermano posterior y se come los
        // clics del diálogo de confirmación. Playwright lo dejó por escrito:
        //
        //     <canvas data-engine="three.js r169"> … intercepts pointer events
        //
        // `showModal()` lo pone en la CAPA SUPERIOR del navegador, por encima
        // de todo y sin depender del apilado. Y además es lo que la hoja de
        // estilos ya suponía: `.gf-dialog::backdrop` sólo pinta si el diálogo
        // se abrió como modal, de modo que hasta hoy ESE FONDO NO SE VEÍA EN
        // NINGÚN LADO.
        //
        // ES OPCIÓN EXPLÍCITA Y NO SE APLICA A TODOS. Los diálogos del panel
        // de cuentas se sirven con `open` y funcionan porque nada se pinta
        // encima de ellos; pasarlos a modal les cambiaría el fondo y el
        // manejo nativo de la tecla de escape, que ese panel resuelve a su
        // manera. Eso es una decisión de sistema visual y no un arreglo de
        // paso: quien la quiera, la pide con `data-gf-dialog-modal`.
        const promovible = document.querySelector('[data-gf-dialog-modal]');

        if (promovible !== null && typeof promovible.showModal === 'function' && !promovible.open) {
            promovible.showModal();
        }
    }

    // ---- Los enganches, delegados en el documento -------------------------
    // Delegados a propósito: el marco reemplaza el cuerpo del documento al
    // navegar, y un enganche puesto sobre un elemento concreto se iría con él.

    document.addEventListener('input', function (event) {
        const guarded = document.querySelectorAll('[data-gf-match-input]');

        for (let index = 0; index < guarded.length; index++) {
            if (guarded[index].getAttribute('data-gf-match-input') === event.target.id) {
                guardConfirmationMatch(guarded[index]);
            }
        }
    }, true);

    document.addEventListener('submit', function (event) {
        const form = event.target;

        if (form instanceof HTMLFormElement && form.hasAttribute('data-gf-pending')) {
            markPending(form);
        }
    }, true);

    document.addEventListener('keydown', function (event) {
        const dialog = document.querySelector('[data-gf-dialog]');

        if (dialog === null) {
            return;
        }

        if (event.key === 'Escape') {
            event.preventDefault();
            dismissDialog(dialog);
        } else if (event.key === 'Tab' && dialog.contains(document.activeElement)) {
            trapFocus(dialog, event);
        }
    }, true);


    // ---- El visor: dibujar lo que la pantalla ya trajo -------------------------------------
    //
    // ESTE BLOQUE NO PIDE NADA. Las piezas bajan **dentro del marcado**, en `data-gf-viewer-pieces`,
    // porque esta pantalla ya se las pidió al servicio de datos del lado del servidor. Es lo que
    // permite que este guion siga sin una sola salida a la red: `RA-01` prohíbe que un guion del
    // navegador invoque al servicio, y el inventario de la etapa `c` lo mide con umbral cero.
    //
    // Y NO CONOCE EL FORMATO DEL ALUMNO: lee piezas ya reconstruidas y se las pasa al visor tal
    // como llegaron (`ADR-08006`).
    var pendingDraw = false;

    /**
     * Le dice al acuse de la escena qué pasó de verdad.
     *
     * POR QUE EXISTE. La frase «Se dibujaron las N figuras» la escribe el servidor, que NO PUEDE
     * SABER si el navegador dibujó algo. En una máquina sin capacidad 3D el recuadro queda liso y
     * la página afirmaba igual que había dibujado. El único aviso era un `console.warn`.
     *
     * EL ACUSE SE SIRVE CON UN HECHO QUE SIEMPRE ES CIERTO —cuántas figuras TIENE el trabajo— y
     * este guion lo mueve a «se dibujaron» o a «no se pudo» según lo que efectivamente ocurrió.
     * Sin guion, el texto servido sigue siendo verdadero: esa es la propiedad que se buscaba.
     */
    function acusarEscena(scene, dibujada) {
        const acuse = scene.parentElement === null
            ? null
            : scene.parentElement.querySelector('[data-gf-escena-acuse]');

        if (acuse === null) {
            return;
        }

        const texto = dibujada
            ? acuse.getAttribute('data-gf-escena-dibujada')
            : acuse.getAttribute('data-gf-escena-sin-dibujar');

        if (texto === null) {
            return;
        }

        acuse.textContent = texto;

        // EL FALLO SE VE, NO SE SUSURRA. Cuando no se pudo dibujar, el acuse deja de ser una
        // leyenda al pie y toma la forma de aviso que el resto del producto ya usa.
        acuse.classList.toggle('gf-banner', !dibujada);
        acuse.classList.toggle('gf-banner--warning', !dibujada);
        acuse.classList.toggle('gf-caption', dibujada);

        if (!dibujada) {
            acuse.setAttribute('role', 'status');
        }
    }

    function drawScenes() {
        var scenes = document.querySelectorAll('[data-gf-viewer-pieces]');

        for (var i = 0; i < scenes.length; i++) {
            var scene = scenes[i];

            if (scene.dataset.gfViewerDrawn === 'yes') {
                continue;
            }

            var viewer = window.GeometriaFactoryViewer;

            if (!viewer) {
                // TODAVÍA NO LLEGÓ, Y ESO ES LO NORMAL LA PRIMERA VEZ. Este guion se sirve en el
                // encabezado y el paquete del visor en el cuerpo: los dos son diferidos, y los
                // diferidos **se ejecutan en el orden del documento**, de modo que la primera
                // pasada de esta función ocurre SIEMPRE antes de que el visor exista.
                //
                // Salir sin volver a intentar es el defecto que esto evita, y no se veía en
                // ninguna prueba: el marcado quedaba perfecto, sin errores en consola, y la
                // escena simplemente no aparecía. Se reintenta cuando la página termina de
                // cargar, que es cuando el visor ya está.
                //
                // Si NUNCA llega, la escena no se dibuja y **no se simula una**: el recuadro queda
                // con su leyenda y la persona envía igual.
                if (!pendingDraw) {
                    pendingDraw = true;
                    window.addEventListener('load', function () {
                        drawScenes();

                        // SI DESPUES DE LA CARGA EL VISOR SIGUE SIN ESTAR, YA NO VA A ESTAR, y la
                        // pantalla tiene que decirlo en vez de dejar el acuse afirmando un dibujo
                        // que no ocurrió.
                        if (!window.GeometriaFactoryViewer) {
                            var huerfanas = document.querySelectorAll('[data-gf-viewer-pieces]');

                            for (var h = 0; h < huerfanas.length; h++) {
                                acusarEscena(huerfanas[h], false);
                            }
                        }
                    }, { once: true });
                }

                continue;
            }

            var pieces;

            try {
                pieces = JSON.parse(scene.dataset.gfViewerPieces);
            } catch (error) {
                continue;
            }

            // EL AVISO DE SELECCIÓN VIAJA EN LAS OPCIONES (`ADR-08007`), y es la vuelta que le
            // faltaba a `F-13`: elegir una pieza EN LA ESCENA marca su nodo en el árbol, por el
            // mismo índice y sin traducir ninguna identidad.
            var id = viewer.initialize(scene, { onPieceSelected: markNode });

            if (!id) {
                // SIN CAPACIDAD 3D. Hasta hoy se salía sin decirle nada a la pantalla, y el acuse
                // seguía afirmando que se había dibujado. Es el caso que el peritaje reprodujo.
                acusarEscena(scene, false);
                continue;
            }

            viewer.loadPieces(id, pieces);

            // LA ESCENA Y EL ÁRBOL SE SINCRONIZAN POR ÍNDICE DE PIEZA (`F-13`), sin traducir
            // ninguna identidad: el número del nodo es el mismo que el de la pieza, de los dos
            // lados. Elegir un nodo resalta esa figura y sólo esa.
            //
            // LA OTRA DIRECCIÓN TAMBIÉN ESTÁ, desde `ADR-08007`: el aviso de selección viaja en
            // las OPCIONES de `initialize` y no como séptima función de la fachada, de modo que las
            // seis que el Product Owner fijó siguen siendo seis. Se ata doce líneas más arriba.
            var nodes = document.querySelectorAll('[data-gf-piece-node]');

            for (var n = 0; n < nodes.length; n++) {
                bindNode(viewer, id, nodes[n]);
            }

            // Y LOS DOS MOVIMIENTOS, CADA UNO CON SU CASILLA: se gobiernan por separado, y quien
            // fija su estado inicial es esta superficie y no el visor.
            bindMotion(viewer, id);

            // LA INSTANCIA SE LIBERA AL DEJAR LA PÁGINA, que es la mitad de `PT-02` que se rompe
            // sin que nada falle hoy: diez navegaciones sin liberar dejan diez contextos vivos.
            scene.dataset.gfViewerDrawn = 'yes';
            acusarEscena(scene, true);
            window.addEventListener('pagehide', function () { viewer.destroy(id); }, { once: true });
        }
    }


    // ---- La sincronización de `F-13`, en sus dos direcciones ------------------------------------

    // De la escena al árbol: marca el nodo de esa pieza y lo trae a la vista.
    //
    // EL ESTADO VIVE EN EL `treeitem` Y NO EN LO QUE SE DIBUJA, que es donde la maqueta aprobada lo
    // puso: es el único portador de rol y el que recibe el foco.
    function markNode(position) {
        var nodes = document.querySelectorAll('[data-gf-piece-node]');

        for (var i = 0; i < nodes.length; i++) {
            var selected = Number(nodes[i].dataset.gfPieceNode) === position;
            nodes[i].setAttribute('aria-selected', selected ? 'true' : 'false');

            if (selected && typeof nodes[i].scrollIntoView === 'function') {
                // TRAERLO A LA VISTA ES PARTE DE LA INTERACCIÓN: un nodo marcado fuera de la
                // ventana de desplazamiento no le dice nada a nadie.
                nodes[i].scrollIntoView({ block: 'nearest' });
            }
        }
    }

    // ---- el plegado del árbol (`F-11`, «árbol colapsable») ------------------------------
    //
    // SIN GUION EL ÁRBOL SE VE ENTERO, y es deliberado: el marcado sirve `aria-expanded` y `hidden`
    // ya resueltos desde el servidor, de modo que la página es legible antes de que esto corra y
    // sigue siéndolo si nunca corre. Lo que este bloque agrega es **poder plegar**, no poder ver.
    //
    // LA FLECHA PLIEGA Y EL RESTO DEL NODO SELECCIONA. Son dos gestos distintos sobre el mismo
    // renglón y confundirlos es la molestia clásica de estos árboles: al elegir una figura para
    // verla en la escena, el nodo se plegaba y escondía lo que la persona quería mirar.
    function bindToggle(node) {
        if (node.dataset.gfToggleBound === 'yes') {
            return;
        }

        node.dataset.gfToggleBound = 'yes';

        var arrow = node.querySelector(':scope > .gf-node > .gf-node-arrow');
        var children = node.querySelector(':scope > .gf-tree-children');

        if (!arrow || !children) {
            return;
        }

        function toggle(event) {
            event.stopPropagation();
            var open = node.getAttribute('aria-expanded') === 'true';
            node.setAttribute('aria-expanded', open ? 'false' : 'true');
            children.hidden = open;
        }

        arrow.addEventListener('click', toggle);

        node.addEventListener('keydown', function (event) {
            // Las dos teclas que un árbol tiene que aceptar para plegar y desplegar, y sólo sobre
            // el nodo propio: sin esto, un árbol sólo se puede recorrer con el puntero.
            if (event.target !== node) {
                return;
            }

            if (event.key === 'ArrowRight' && node.getAttribute('aria-expanded') === 'false') {
                event.preventDefault();
                node.setAttribute('aria-expanded', 'true');
                children.hidden = false;
            } else if (event.key === 'ArrowLeft' && node.getAttribute('aria-expanded') === 'true') {
                event.preventDefault();
                node.setAttribute('aria-expanded', 'false');
                children.hidden = true;
            }
        });
    }

    // Se ata TODO nodo con hijos, tenga o no posición: el plegado no depende de que haya escena.
    function bindTree() {
        var nodes = document.querySelectorAll('.gf-tree [role="treeitem"][aria-expanded]');

        for (var i = 0; i < nodes.length; i++) {
            bindToggle(nodes[i]);
        }
    }

    // Del árbol a la escena: pide resaltar esa pieza por su índice.
    function bindNode(viewer, id, node) {
        if (node.dataset.gfPieceNodeBound === 'yes') {
            return;
        }

        node.dataset.gfPieceNodeBound = 'yes';

        function choose() {
            var position = Number(node.dataset.gfPieceNode);
            viewer.selectPiece(id, position);
            markNode(position);
        }

        node.addEventListener('click', choose);
        node.addEventListener('keydown', function (event) {
            // Con teclado, las dos activaciones que un elemento de árbol tiene que aceptar.
            if (event.key === 'Enter' || event.key === ' ') {
                event.preventDefault();
                choose();
            }
        });
    }

    // ---- Los dos movimientos automáticos (`F-25`) -----------------------------------------------

    // LA PREFERENCIA DEL SISTEMA LA LEE EL ANFITRIÓN, NUNCA EL VISOR: es la frontera que el contrato
    // de la fachada fija, y la razón por la que el visor recibe dos valores de verdad en lugar de
    // consultar nada.
    function reducedMotion() {
        try {
            return !!window.matchMedia('(prefers-reduced-motion: reduce)').matches;
        } catch (error) {
            return false;
        }
    }

    function bindMotion(viewer, id) {
        var boxes = document.querySelectorAll('[data-gf-motion]');

        if (boxes.length === 0) {
            return;
        }

        var reduced = reducedMotion();

        function push() {
            var options = { cameraOrbit: false, pieceSpin: false };

            for (var i = 0; i < boxes.length; i++) {
                options[boxes[i].dataset.gfMotion] = boxes[i].checked;
            }

            viewer.setMotion(id, options);
        }

        for (var i = 0; i < boxes.length; i++) {
            if (boxes[i].dataset.gfMotionBound === 'yes') {
                continue;
            }

            boxes[i].dataset.gfMotionBound = 'yes';

            // ESTADO INICIAL: PRENDIDO, SALVO QUE EL SISTEMA PIDA LO CONTRARIO. Es lo que la
            // maqueta aprobada decidió, y el fundamento de `F-25`: la órbita ya existe en la
            // visualización que la cátedra usa hoy, y arrancar apagado sería portar quitando algo
            // que funciona.
            boxes[i].checked = !reduced;
            boxes[i].addEventListener('change', function () {
                push();
                announceMotion();
            });
        }

        // Y CUANDO EL SISTEMA PIDE MENOS MOVIMIENTO, SE DICE POR QUÉ ARRANCAN APAGADOS: sin el
        // aviso, la persona ve dos casillas apagadas y no sabe si es una falla.
        var note = document.querySelector('[data-gf-motion-note]');

        if (note !== null) {
            note.hidden = !reduced;
        }

        push();
    }

    // El acuse de cada cambio, para quien no ve la escena.
    function announceMotion() {
        var status = document.querySelector('[data-gf-motion-status]');

        if (status === null) {
            return;
        }

        var boxes = document.querySelectorAll('[data-gf-motion]');
        var on = [];

        for (var i = 0; i < boxes.length; i++) {
            if (boxes[i].checked) {
                on.push(boxes[i].getAttribute('aria-label') || boxes[i].dataset.gfMotion);
            }
        }

        status.textContent = on.length === 0
            ? 'Movimiento automático apagado.'
            : 'Movimiento automático: ' + on.join(' y ') + '.';
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', applyEnhancements);
        document.addEventListener('DOMContentLoaded', drawScenes);
        document.addEventListener('DOMContentLoaded', bindTree);
    } else {
        applyEnhancements();
        drawScenes();
        bindTree();
    }

    // EL ÁRBOL SE ATA TAMBIÉN ACÁ porque la interactividad del servidor reemplaza fragmentos de la
    // página: un árbol que llega después de la carga tiene que quedar plegable igual. `bindToggle`
    // es idempotente, así que volver a pasar sobre lo ya atado no cuesta nada.
    new MutationObserver(function () { applyEnhancements(); drawScenes(); bindTree(); }).observe(document.documentElement, {
        childList: true,
        subtree: true,
    });
}());
