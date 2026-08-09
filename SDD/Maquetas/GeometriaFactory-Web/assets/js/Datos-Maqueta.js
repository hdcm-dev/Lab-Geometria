/* ============================================================================
   Datos-Maqueta.js — Fuente ÚNICA de los datos de ejemplo de la maqueta
   Fase B2 · GeometriaFactory-Web

   Ningún HTML de esta maqueta hardcodea datos: todo se renderiza desde acá.
   Si los datos quedaran dispersos en el HTML, la maqueta dejaría de servir
   para validar el modelo de datos, que es uno de sus dos propósitos
   (Maqueta-Rules.md §4.2).

   PROCEDENCIA DE CADA DATO
   -----------------------
   Todo valor de este archivo sale de la documentación ya generada. Cada
   colección declara su origen en su comentario, y `CONTRATO_DE_CAMPOS`
   traza campo por campo contra el modelo conceptual de 02 y el intake.

   Los cuatro valores que la documentación NO declara y que el Product Owner
   autorizó componer para que la maqueta sea recorrible están marcados con
   `origen: 'compuesto-para-la-maqueta'`. Son exactamente cuatro y están
   enumerados en README.md §5.
   ============================================================================ */

(function (global) {
  'use strict';

  /* ==========================================================================
     1. Identidad de la instancia — contrato de identidad de versión
     Origen: Representacion-Sello-De-Version.md §2 (valores de sus esquemas)
     y Design-Rules-Identidad-De-Version.md §2 (los cuatro campos).
     ========================================================================== */

  var IDENTIDAD_DE_VERSION = {
    versionLegible: 'Versión 1.4.2',
    identificadorDeConstruccion: 'a3f81c6',
    esPreliminar: false,
    origenIndeterminado: false,
    // Variantes que la barra de validación conmuta, del mismo documento §3.
    variantes: {
      publicada:     { versionLegible: 'Versión 1.4.2',       identificadorDeConstruccion: 'a3f81c6', esPreliminar: false, origenIndeterminado: false, origen: 'publicado' },
      preliminar:    { versionLegible: 'Versión 1.5.0-rc.2',  identificadorDeConstruccion: 'a3f81c6', esPreliminar: true,  origenIndeterminado: false, origen: 'preliminar' },
      indeterminado: { versionLegible: 'Versión no identificada', identificadorDeConstruccion: '—',   esPreliminar: false, origenIndeterminado: true,  origen: 'indeterminado' }
    }
  };

  /* ==========================================================================
     2. Textos de trabajo — escenarios de datos del intake §20
     Se transcriben carácter por carácter: RN-08 exige que el texto del alumno
     se conserve íntegro y nunca se reescriba, y esta maqueta no lo contradice.
     ========================================================================== */

  // §20.E-1 · JSON semilla del visor: tres piezas y dos advertencias.
  var TEXTO_E1 = [
    '[ ',
    '  {',
    '  "Tipo": "Cilindro", ',
    '  "Tapas": ',
    '  [',
    '    {  ',
    '  "Tipo":"Circulo", ',
    '  "Radio": 3.00, ',
    '  "Area": 28.27',
    '}, ',
    '    {  ',
    '  "Tipo":"Circulo", ',
    '  "Radio": 3.00, ',
    '  "Area": 28.27',
    '}',
    '  ],',
    '  "Lado": ',
    '{ ',
    '  "Tipo": "RectanguloDesarrollado", ',
    '  "Largo": 3.00, ',
    '  "Ancho": 18.85, ',
    '  "Area": 56.55',
    '},',
    '  "Area": 113.10,',
    '  "Volumen": 84.82',
    '},',
    '  {  ',
    '  "Tipo": "Cubo", ',
    '  "Caras": ',
    '  [',
    '    { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 }, ',
    '    { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 }, ',
    '    { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 }, ',
    '    { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 }, ',
    '    { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 }, ',
    '    { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 }',
    '  ],  ',
    '  "Area": 36.00,',
    '  "Volumen": 27.00',
    '},',
    '  {  ',
    '  "Tipo": "Ortoedro", ',
    '  "Bases": ',
    '  [',
    '    { "Tipo": "Rectangulo", "Largo": 7.00, "Ancho": 7.00, "Area": 49.00 }, ',
    '    { "Tipo": "Rectangulo", "Largo": 7.00, "Ancho": 7.00, "Area": 49.00 }',
    '  ],',
    '  "Laterales": ',
    '    [',
    '      { "Tipo": "Rectangulo", "Largo": 21.00, "Ancho": 7.00, "Area": 147.00 }, ',
    '      { "Tipo": "Rectangulo", "Largo": 21.00, "Ancho": 7.00, "Area": 147.00 }, ',
    '      { "Tipo": "Rectangulo", "Largo": 21.00, "Ancho": 7.00, "Area": 147.00 }, ',
    '      { "Tipo": "Rectangulo", "Largo": 21.00, "Ancho": 7.00, "Area": 147.00 }',
    '    ],',
    '  "Area": 686.00,',
    '  "Volumen": 343.00',
    '}',
    ']'
  ].join('\n');

  // §20.E-3 · Cubo(3) de Ejemplo1: caras Cuadrado y área declarada 36.00.
  var TEXTO_E3 = [
    '{',
    '  "Tipo": "Cubo", ',
    '  "Caras": ',
    '  [',
    '    { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 }, ',
    '    { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 }, ',
    '    { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 }, ',
    '    { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 }, ',
    '    { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 }, ',
    '    { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 }',
    '  ],  ',
    '  "Area": 36.00,',
    '  "Volumen": 27.00',
    '}'
  ].join('\n');

  // §20.E-4 · Cubo(3) de Ejemplo2: caras Rectangulo y área declarada 54.00.
  // Cero observaciones: es el escenario que demuestra «Sin observaciones».
  var TEXTO_E4 = [
    '{  ',
    '  "Tipo": "Cubo", ',
    '  "Caras": ',
    '  [',
    '    {"Tipo": "Rectangulo", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00}, ',
    '    {"Tipo": "Rectangulo", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00},',
    '    {"Tipo": "Rectangulo", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00}, ',
    '    {"Tipo": "Rectangulo", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00}, ',
    '    {"Tipo": "Rectangulo", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00}, ',
    '    {"Tipo": "Rectangulo", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00}',
    '  ],  ',
    '  "Area": 54.00,',
    '  "Volumen": 27.00',
    '}'
  ].join('\n');

  // §20.E-5 · Tipo desconocido: error de validación con índice de figura y campo.
  var TEXTO_E5 = [
    '[',
    '  {',
    '    "Tipo": "Cubo",',
    '    "Caras": [',
    '      { "Tipo": "Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 },',
    '      { "Tipo": "Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 },',
    '      { "Tipo": "Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 },',
    '      { "Tipo": "Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 },',
    '      { "Tipo": "Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 },',
    '      { "Tipo": "Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 }',
    '    ],',
    '    "Area": 54.00,',
    '    "Volumen": 27.00',
    '  },',
    '  { "Tipo": "Piramide", "Largo": 5.00, "Ancho": 5.00, "Area": 25.00 }',
    ']'
  ].join('\n');

  // §20.E-6 · Dimensión en 0.00: la figura no se descarta.
  var TEXTO_E6 = [
    '[',
    '  { "Tipo": "Rectangulo", "Largo": 0.00, "Ancho": 5.00, "Area": 0.00 }',
    ']'
  ].join('\n');

  // §20.E-2 · Ortoedro(7,7,21) tal como lo emite el programa del alumno:
  // clave "Tapas" y dos comas finales. Texto de partida del envío de ejemplo.
  var TEXTO_E2 = [
    '[',
    '{',
    '  "Tipo": "Ortoedro",',
    '  "Tapas":',
    '  [',
    '    { "Tipo": "Rectangulo", "Largo": 7.00, "Ancho": 7.00, "Area": 49.00 },',
    '    { "Tipo": "Rectangulo", "Largo": 7.00, "Ancho": 7.00, "Area": 49.00 }',
    '  ],',
    '  "Laterales":',
    '    [',
    '      { "Tipo": "Rectangulo", "Largo": 21.00, "Ancho": 7.00, "Area": 147.00 },',
    '      { "Tipo": "Rectangulo", "Largo": 21.00, "Ancho": 7.00, "Area": 147.00 },',
    '      { "Tipo": "Rectangulo", "Largo": 21.00, "Ancho": 7.00, "Area": 147.00 },',
    '      { "Tipo": "Rectangulo", "Largo": 21.00, "Ancho": 7.00, "Area": 147.00 },',
    '    ],',
    '  "Area": 686.00,',
    '  "Volumen": 343.00',
    '},',
    ']'
  ].join('\n');

  // §20.E-7 · Cobertura de los seis tipos dibujables. Es el material de dibujo
  // que Especificacion-Funcional.md §6 del visor declara junto con E-1, y el
  // texto del sample S-1, que ejerce la fachada entera sin backend.
  var TEXTO_E7 = [
    '[',
    '  {',
    '    "Tipo": "Cilindro",',
    '    "Tapas": [',
    '      { "Tipo": "Circulo", "Radio": 3.00, "Area": 28.27 },',
    '      { "Tipo": "Circulo", "Radio": 3.00, "Area": 28.27 }',
    '    ],',
    '    "Lado": { "Tipo": "RectanguloDesarrollado", "Largo": 5.00, "Ancho": 18.85, "Area": 94.25 },',
    '    "Area": 150.80,',
    '    "Volumen": 141.37',
    '  },',
    '  {',
    '    "Tipo": "Cubo",',
    '    "Caras": [',
    '      { "Tipo": "Cuadrado", "Largo": 4.00, "Ancho": 4.00, "Area": 16.00 },',
    '      { "Tipo": "Cuadrado", "Largo": 4.00, "Ancho": 4.00, "Area": 16.00 },',
    '      { "Tipo": "Cuadrado", "Largo": 4.00, "Ancho": 4.00, "Area": 16.00 },',
    '      { "Tipo": "Cuadrado", "Largo": 4.00, "Ancho": 4.00, "Area": 16.00 },',
    '      { "Tipo": "Cuadrado", "Largo": 4.00, "Ancho": 4.00, "Area": 16.00 },',
    '      { "Tipo": "Cuadrado", "Largo": 4.00, "Ancho": 4.00, "Area": 16.00 }',
    '    ],',
    '    "Area": 96.00,',
    '    "Volumen": 64.00',
    '  },',
    '  {',
    '    "Tipo": "Ortoedro",',
    '    "Bases": [',
    '      { "Tipo": "Rectangulo", "Largo": 6.00, "Ancho": 4.00, "Area": 24.00 },',
    '      { "Tipo": "Rectangulo", "Largo": 6.00, "Ancho": 4.00, "Area": 24.00 }',
    '    ],',
    '    "Laterales": [',
    '      { "Tipo": "Rectangulo", "Largo": 6.00, "Ancho": 8.00, "Area": 48.00 },',
    '      { "Tipo": "Rectangulo", "Largo": 6.00, "Ancho": 8.00, "Area": 48.00 },',
    '      { "Tipo": "Rectangulo", "Largo": 4.00, "Ancho": 8.00, "Area": 32.00 },',
    '      { "Tipo": "Rectangulo", "Largo": 4.00, "Ancho": 8.00, "Area": 32.00 }',
    '    ],',
    '    "Area": 208.00,',
    '    "Volumen": 192.00',
    '  },',
    '  { "Tipo": "Rectangulo", "Largo": 6.00, "Ancho": 3.00, "Area": 18.00 },',
    '  { "Tipo": "Cuadrado",   "Largo": 4.00, "Ancho": 4.00, "Area": 16.00 },',
    '  { "Tipo": "Circulo",    "Radio": 2.50, "Area": 19.63 }',
    ']'
  ].join('\n');

  // Variante de E-7 con el círculo sin su `Radio`, para demostrar la condición
  // DIMENSION_NO_LEGIBLE. Es el ÚNICO de los siete códigos de la fachada que
  // no tiene escenario propio en el intake §20 ni fila en su §21: se compone
  // acá quitando una clave de una pieza documentada, y queda declarado como
  // hallazgo para el paso de retroalimentación.
  var TEXTO_DIMENSION_NO_LEGIBLE = TEXTO_E7.replace(
    '{ "Tipo": "Circulo",    "Radio": 2.50, "Area": 19.63 }',
    '{ "Tipo": "Circulo",    "Area": 19.63 }'
  );

  var ARBOL_E7 = [
    { indice: 0, tipo: 'Cilindro',   forma: 'cilindro',   hijos: [ { etiqueta: 'Tapas (2)' }, { etiqueta: 'Lado' } ] },
    { indice: 1, tipo: 'Cubo',       forma: 'cubo',       hijos: [ { etiqueta: 'Caras (6)' } ] },
    { indice: 2, tipo: 'Ortoedro',   forma: 'ortoedro',   hijos: [ { etiqueta: 'Bases (2)' }, { etiqueta: 'Laterales (4)' } ] },
    { indice: 3, tipo: 'Rectangulo', forma: 'rectangulo', hijos: [] },
    { indice: 4, tipo: 'Cuadrado',   forma: 'cuadrado',   hijos: [] },
    { indice: 5, tipo: 'Circulo',    forma: 'circulo',    hijos: [] }
  ];

  // El mismo conjunto raíz, con el círculo sin dimensión legible: no se dibuja
  // y queda enumerado. Las otras cinco piezas se dibujan.
  var ARBOL_E7_SIN_DIMENSION = ARBOL_E7.map(function (n) {
    return n.indice === 5 ? { indice: 5, tipo: 'Circulo', forma: null, hijos: [] } : n;
  });

  /* ==========================================================================
     2.b Contrato de fachada de GeometriaFactory-Visor
     El visor no tiene pantallas propias: su superficie pública son cinco
     funciones que un componente anfitrión invoca. Su validación se integra en
     esta maqueta, dentro de las superficies que lo usan, en lugar de tener
     maqueta propia.

     Origen: Definicion-Contrato-De-Fachada.md §4 (las cinco funciones), §6
     (los siete códigos de condición, con sus dos cursos) y
     Especificacion-Funcional.md §6 del visor (las seis propiedades
     transversales con su umbral). Ningún código se inventa ni se renombra.
     ========================================================================== */

  var FUNCIONES_DE_LA_FACHADA = [
    { nombre: 'inicializar',      firma: 'inicializar(elemento, opciones)',
      efecto: 'Queda una instancia viva con escena, iluminación y cámara orbital. No dibuja ninguna pieza hasta que se invoque cargarJson.', cu: 'CU-01' },
    { nombre: 'cargarJson',       firma: 'cargarJson(id, texto)',
      efecto: 'Reemplaza por completo lo dibujado y libera lo anterior. Devuelve el resultado de dibujo: piezas dibujadas, piezas no dibujadas con su código, y la estructura del texto.', cu: 'CU-02' },
    { nombre: 'seleccionarPieza', firma: 'seleccionarPieza(id, indice)',
      efecto: 'Resaltado exclusivo: una sola pieza resaltada por instancia, por el mismo índice con el que figura en el resultado de dibujo.', cu: 'CU-03' },
    { nombre: 'redimensionar',    firma: 'redimensionar(id)',
      efecto: 'Recalcula la relación de aspecto contra el tamaño vigente del elemento de dibujo, sin deformar las piezas. Conserva la selección y la disposición.', cu: 'CU-04' },
    { nombre: 'destruir',         firma: 'destruir(id)',
      efecto: 'Libera geometrías, materiales y contexto gráfico. El identificador deja de ser válido y no se reutiliza.', cu: 'CU-05' }
  ];

  var CONDICIONES_DE_LA_FACHADA = [
    { codigo: 'CAPACIDAD_GRAFICA_AUSENTE', curso: 'Único',
      cuando: 'El navegador no provee la capacidad gráfica tridimensional.',
      efecto: 'No se crea instancia. inicializar no devuelve identificador.',
      estado: 'escena-no-disponible' },
    { codigo: 'ELEMENTO_DE_DIBUJO_INVALIDO', curso: 'C-1, en creación',
      cuando: 'El elemento recibido por inicializar no sirve como superficie de dibujo, o tiene tamaño nulo.',
      efecto: 'No se crea instancia y no se devuelve identificador.',
      estado: 'elemento-sin-tamano' },
    { codigo: 'ELEMENTO_DE_DIBUJO_INVALIDO', curso: 'C-2, en ajuste',
      cuando: 'El elemento de una instancia viva dejó de servir como superficie al invocar redimensionar.',
      efecto: 'La instancia sigue viva, con su escena y su selección intactas. Una invocación posterior ajusta cuando el elemento vuelva a tener tamaño.',
      estado: 'elemento-sin-tamano-ajuste' },
    { codigo: 'INSTANCIA_DESCONOCIDA', curso: 'Único, en cuatro funciones',
      cuando: 'El identificador recibido no corresponde a ninguna instancia viva, o corresponde a una ya liberada.',
      efecto: 'Ninguno: ninguna instancia cambia.',
      estado: 'instancia-desconocida' },
    { codigo: 'TEXTO_NO_LEGIBLE', curso: 'Único',
      cuando: 'El texto recibido por cargarJson no permite obtener un conjunto de piezas.',
      efecto: 'La instancia queda viva y vacía: se libera lo dibujado antes y no se dibuja nada nuevo.',
      estado: 'texto-no-legible' },
    { codigo: 'TIPO_NO_DIBUJABLE', curso: 'Único, por pieza',
      cuando: 'Una pieza del conjunto raíz declara un tipo que no está entre los seis dibujables.',
      efecto: 'Esa pieza no se dibuja; las demás sí. Queda enumerada con su índice.',
      estado: 'piezas-no-dibujadas' },
    { codigo: 'DIMENSION_NO_LEGIBLE', curso: 'Único, por pieza',
      cuando: 'Una pieza de un tipo dibujable no expone la dimensión necesaria para construir su malla.',
      efecto: 'Esa pieza no se dibuja; las demás sí. Queda enumerada con su índice.',
      estado: 'dimension-no-legible' },
    { codigo: 'INDICE_FUERA_DE_RANGO', curso: 'Único',
      cuando: 'El índice recibido por seleccionarPieza no corresponde a ninguna pieza dibujada del resultado de dibujo vigente. Cubre también el índice de una pieza enumerada como no dibujada.',
      efecto: 'Ninguno: la selección vigente se conserva.',
      estado: 'indice-sin-representacion' }
  ];

  var PROPIEDADES_TRANSVERSALES = [
    { nombre: 'Cero red',            umbral: 'Exactamente 0 peticiones originadas por el archivo de guion.' },
    { nombre: 'Cero persistencia',   umbral: '0 claves escritas en el almacenamiento del navegador, y ningún estado conservado entre páginas.' },
    { nombre: 'Se ejercita sin backend', umbral: 'Recorrido completo de las cinco funciones con 0 servicios del backend disponibles.' },
    { nombre: 'Disposición determinista', umbral: 'Dos procesados del mismo texto producen la misma disposición, comparable pieza por pieza.' },
    { nombre: 'Liberación de recursos',   umbral: '10 recorridos de ida y vuelta entre trabajos sin degradación.' },
    { nombre: 'Ausencia de fallo silencioso', umbral: '100 % de las piezas no dibujadas enumeradas con su índice y su código, y 0 piezas que dejen de aparecer sin quedar registradas.' }
  ];

  /* ==========================================================================
     3. Cuentas — situación de cuenta en sus tres valores
     Origen de las tres cuentas de alumno y de sus correos y fechas:
     Wireframes-Panel-De-Cuentas.md §2. Nombre del administrador: la barra
     lateral del administrador de Experiencia-De-Uso.md §3.2 y de los
     wireframes del shell de trabajo, que lo rotulan «Docente».

     Modelo de dominio (Definicion-Modelo-De-Dominio.md §65 y §243-250):
     el estado de cuenta es `Pendiente`, `Habilitado` o `Bloqueado`; la
     superficie lo rotula «situación» y concuerda en femenino con «cuenta»
     (Wireframes-Panel-De-Cuentas.md §3 y §7).
     ========================================================================== */

  var CUENTAS = [
    {
      id: 'C-ADMIN', papel: 'Administrador',
      nombre: 'Docente', apellido: '', iniciales: 'DO',
      correo: 'docente@ej.test',
      estadoCuenta: 'Habilitado', etiquetaSituacion: 'Habilitada',
      fechaRegistro: '01/08/2026',
      operable: false,   // ninguna de las cuatro operaciones procede sobre el administrador
      origen: 'Experiencia-De-Uso.md §3.2 (nombre); correo compuesto-para-la-maqueta'
    },
    {
      id: 'C-1', papel: 'Alumno',
      nombre: 'Ana', apellido: 'Diaz', iniciales: 'AD',
      correo: 'ana@ej.test',
      estadoCuenta: 'Habilitado', etiquetaSituacion: 'Habilitada',
      fechaRegistro: '05/08/2026',
      operable: true,
      origen: 'Wireframes-Panel-De-Cuentas.md §2'
    },
    {
      id: 'C-2', papel: 'Alumno',
      nombre: 'Beto', apellido: 'Lopez', iniciales: 'BL',
      correo: 'beto@ej.test',
      estadoCuenta: 'Habilitado', etiquetaSituacion: 'Habilitada',
      fechaRegistro: '03/08/2026',
      operable: true,
      origen: 'Wireframes-Panel-De-Cuentas.md §2'
    },
    {
      id: 'C-3', papel: 'Alumno',
      nombre: 'Cora', apellido: 'Mera', iniciales: 'CM',
      correo: 'cora@ej.test',
      estadoCuenta: 'Bloqueado', etiquetaSituacion: 'Bloqueada',
      fechaRegistro: '02/08/2026',
      operable: true,
      origen: 'Wireframes-Panel-De-Cuentas.md §2'
    },
    {
      id: 'C-4', papel: 'Alumno',
      nombre: 'Dario', apellido: 'Nuñez', iniciales: 'DN',
      correo: 'dario@ej.test',
      estadoCuenta: 'Pendiente', etiquetaSituacion: 'Pendiente',
      fechaRegistro: '08/08/2026',
      operable: true,
      origen: 'compuesto-para-la-maqueta'
    }
  ];

  /* Credencial de la cuenta de administrador de prueba.
     Es un instrumento de la maqueta, no un dato del producto: existe para que
     el Product Owner pueda recorrer el ingreso. Se exhibe a la vista en
     `Ingreso.html` y NO se traslada ni a la especificación ni al código. */
  var CREDENCIAL_DE_MAQUETA = {
    correo: 'docente@ej.test',
    contrasena: 'laboratorio',
    origen: 'compuesto-para-la-maqueta'
  };

  /* ==========================================================================
     4. Observaciones — lo que el producto emite al interpretar el texto
     Origen: intake §20.E-1 «qué verificar» puntos 3 y 4, §20.E-5 punto 2,
     y Representacion-Lista-De-Observaciones.md §2 y §4.

     Los valores declarado y derivado se muestran EXACTAMENTE como el texto
     los trae y como el sistema los recalcula, sin reformatear
     (Representacion-Lista-De-Observaciones.md §4, excepción declarada de la
     convención de coma decimal en Experiencia-De-Uso.md §6).
     ========================================================================== */

  var OBSERVACIONES_E1 = [
    { severidad: 'Advertencia', etiqueta: 'advertencia', figura: 1, campo: 'Area',
      declarado: '36.00', derivado: '54.00' },
    { severidad: 'Advertencia', etiqueta: 'advertencia', figura: 2, campo: 'Volumen',
      declarado: '343.00', derivado: '1029.00' }
  ];

  var OBSERVACIONES_E3 = [
    { severidad: 'Advertencia', etiqueta: 'advertencia', figura: 0, campo: 'Area',
      declarado: '36.00', derivado: '54.00' }
  ];

  var OBSERVACIONES_E5 = [
    { severidad: 'Error de validación', etiqueta: 'error', figura: 1, campo: 'Tipo',
      texto: 'El tipo declarado no se pudo interpretar.' }
  ];

  /* ==========================================================================
     5. Árboles de la estructura — lo que devuelve la fachada del visualizador
     Origen: intake §20.E-1 «qué verificar» puntos 1 y 2, y la composición de
     cada texto. El índice del nodo de pieza es el MISMO con el que la pieza
     figura en el resultado de dibujo (Wireframes-Vista-De-Trabajo.md §4).

     Desde la iteración 3 estos árboles son el RESPALDO: el árbol y la escena
     se derivan del texto con el lector tolerante del visor real, y estas
     colecciones se usan sólo si el visor no está cargado. Se conservan porque
     son el contrato declarado, y porque son con lo que se contrasta que el
     lector real devuelva lo mismo. Ver README.md §6.
     ========================================================================== */

  var ARBOL_E1 = [
    { indice: 0, tipo: 'Cilindro', forma: 'cilindro', hijos: [
        { etiqueta: 'Tapas (2)' }, { etiqueta: 'Lado' } ] },
    { indice: 1, tipo: 'Cubo', forma: 'cubo', hijos: [
        { etiqueta: 'Caras (6)' } ] },
    { indice: 2, tipo: 'Ortoedro', forma: 'ortoedro', hijos: [
        { etiqueta: 'Bases (2)' }, { etiqueta: 'Laterales (4)' } ] }
  ];

  var ARBOL_E3 = [
    { indice: 0, tipo: 'Cubo', forma: 'cubo', hijos: [ { etiqueta: 'Caras (6)' } ] }
  ];

  var ARBOL_E4 = [
    { indice: 0, tipo: 'Cubo', forma: 'cubo', hijos: [ { etiqueta: 'Caras (6)' } ] }
  ];

  var ARBOL_E5 = [
    { indice: 0, tipo: 'Cubo', forma: 'cubo', hijos: [ { etiqueta: 'Caras (6)' } ] },
    { indice: 1, tipo: 'Piramide', forma: null, hijos: [] }
  ];

  var ARBOL_E6 = [
    { indice: 0, tipo: 'Rectangulo', forma: 'rectangulo', hijos: [] }
  ];

  // El ortoedro con clave «Tapas»: SÍ se dibuja, porque el port tolera las dos
  // claves. Con el visor original quedaba fuera de la escena sin aviso.
  var ARBOL_E2 = [
    { indice: 0, tipo: 'Ortoedro', forma: 'ortoedro', hijos: [
        { etiqueta: 'Tapas (2)' }, { etiqueta: 'Laterales (4)' } ] }
  ];

  /* Los textos que el panel de fachada puede cargar. Todos salen del intake. */
  var EJERCICIOS_DE_FACHADA = [
    { id: 'E-1', rotulo: 'E-1 · tres piezas del texto semilla',
      texto: TEXTO_E1, arbol: ARBOL_E1, origen: 'intake §20.E-1' },
    { id: 'E-7', rotulo: 'E-7 · los seis tipos dibujables',
      texto: TEXTO_E7, arbol: ARBOL_E7, origen: 'intake §20.E-7' },
    /* El ortoedro tal como lo emite el programa del alumno: clave «Tapas» en
       lugar de «Bases» y dos comas finales. Es el texto con el que se
       comprueba la tolerancia de claves que el port agrega: con el visor
       original este ortoedro no se dibujaba. */
    { id: 'E-2', rotulo: 'E-2 · ortoedro con clave «Tapas» y comas finales',
      texto: TEXTO_E2, arbol: ARBOL_E2, origen: 'intake §20.E-2' },
    { id: 'E-5', rotulo: 'E-5 · con una pieza de tipo no dibujable',
      texto: TEXTO_E5, arbol: ARBOL_E5, origen: 'intake §20.E-5',
      noDibujadas: [ { indice: 1, motivo: 'tipo no dibujable', codigo: 'TIPO_NO_DIBUJABLE' } ] },
    { id: 'DIM', rotulo: 'E-7 sin la dimensión del círculo',
      texto: TEXTO_DIMENSION_NO_LEGIBLE, arbol: ARBOL_E7_SIN_DIMENSION,
      origen: 'compuesto-para-la-maqueta sobre intake §20.E-7',
      noDibujadas: [ { indice: 5, motivo: 'dimensión no legible', codigo: 'DIMENSION_NO_LEGIBLE' } ] },
    { id: 'VACIO', rotulo: 'Texto del que no se obtienen piezas',
      texto: 'no es un conjunto de piezas', arbol: [], origen: 'Definicion-Contrato-De-Fachada.md §6',
      condicionGeneral: 'TEXTO_NO_LEGIBLE' }
  ];

  /* ==========================================================================
     6. Trabajos — los cuatro estados del conjunto cerrado
     Nombres, fechas y estados: Wireframes-Panel-De-Trabajos-Del-Alumno.md §2
     y Wireframes-Listado-De-La-Comision.md §2.
     Textos, piezas y observaciones: intake §20.
     Comentario del rechazo: Wireframes-Resolucion-Del-Trabajo.md §2.
     ========================================================================== */

  var TRABAJOS = [
    {
      id: 'T-1',
      nombre: 'Cubo y ortoedro',
      fechaTrabajo: '12/08/2026',
      descripcion: 'Entrega de la Actividad 1 con las tres figuras del enunciado.',
      cuentaId: 'C-1',
      estado: 'Pendiente',
      textoOriginal: TEXTO_E1,
      arbol: ARBOL_E1,
      piezas: 3,
      advertencias: 2,
      observaciones: OBSERVACIONES_E1,
      piezasNoDibujadas: [],
      comentario: null,
      fechaDesenlace: null,
      origen: 'Wireframes-Panel-De-Trabajos-Del-Alumno.md §2 + intake §20.E-1'
    },
    {
      id: 'T-2',
      nombre: 'Prueba 2',
      fechaTrabajo: '10/08/2026',
      descripcion: 'Probando una figura que agregué por mi cuenta.',
      cuentaId: 'C-1',
      estado: 'Borrador',
      textoOriginal: TEXTO_E5,
      arbol: ARBOL_E5,
      piezas: 1,
      advertencias: 0,
      observaciones: OBSERVACIONES_E5,
      piezasNoDibujadas: [ { indice: 1, motivo: 'tipo no dibujable', codigo: 'TIPO_NO_DIBUJABLE' } ],
      comentario: null,
      fechaDesenlace: null,
      origen: 'Wireframes-Panel-De-Trabajos-Del-Alumno.md §2 + intake §20.E-5'
    },
    {
      id: 'T-3',
      nombre: 'Entrega 1',
      fechaTrabajo: '03/08/2026',
      descripcion: 'Primera entrega, con el cubo del segundo ejemplo de la cátedra.',
      cuentaId: 'C-1',
      estado: 'Finalizado',
      textoOriginal: TEXTO_E4,
      arbol: ARBOL_E4,
      piezas: 1,
      advertencias: 0,
      observaciones: [],           // demuestra el estado «Sin observaciones»
      piezasNoDibujadas: [],
      comentario: null,            // demuestra el estado «Sin comentario»
      fechaDesenlace: '13/08/2026',
      origen: 'Wireframes-Panel-De-Trabajos-Del-Alumno.md §2 + intake §20.E-4'
    },
    {
      id: 'T-4',
      nombre: 'Primer intento',
      fechaTrabajo: '01/08/2026',
      descripcion: 'Mi primer envío con el cubo.',
      cuentaId: 'C-1',
      estado: 'Rechazado',
      textoOriginal: TEXTO_E3,
      arbol: ARBOL_E3,
      piezas: 1,
      advertencias: 1,
      observaciones: OBSERVACIONES_E3,
      piezasNoDibujadas: [],
      // Rechazado CON comentario. Texto: Wireframes-Resolucion-Del-Trabajo.md §2.
      comentario: 'Revisá el área del cubo.',
      fechaDesenlace: '13/08/2026',
      origen: 'Wireframes-Listado-De-La-Comision.md §2 + intake §20.E-3'
    },
    {
      id: 'T-5',
      nombre: 'Segundo intento',
      fechaTrabajo: '05/08/2026',
      descripcion: 'Reintento con una sola figura.',
      cuentaId: 'C-2',
      estado: 'Rechazado',
      textoOriginal: TEXTO_E6,
      arbol: ARBOL_E6,
      piezas: 1,
      advertencias: 0,
      observaciones: [],
      piezasNoDibujadas: [],
      // Rechazado SIN comentario: el Product Owner admite el rechazo sin
      // explicación escrita, y el estado expresa el desenlace por sí solo.
      comentario: null,
      fechaDesenlace: '13/08/2026',
      origen: 'intake §20.E-6; nombre compuesto-para-la-maqueta'
    }
  ];

  /* Texto de partida del formulario de envío, en su curso de reedición.
     Origen: intake §20.E-2, el trabajo mínimo que un alumno entrega. */
  var BORRADOR_DE_ENVIO = {
    nombre: 'Ortoedro de la Actividad 1',
    fechaTrabajo: '14/08/2026',
    descripcion: 'Pegado tal como salió del programa.',
    textoOriginal: TEXTO_E2,
    arbol: [ { indice: 0, tipo: 'Ortoedro', forma: 'ortoedro',
               hijos: [ { etiqueta: 'Tapas (2)' }, { etiqueta: 'Laterales (4)' } ] } ],
    piezas: 1,
    observaciones: [
      { severidad: 'Advertencia', etiqueta: 'advertencia', figura: 0, campo: 'Volumen',
        declarado: '343.00', derivado: '1029.00' }
    ],
    origen: 'intake §20.E-2'
  };

  /* ==========================================================================
     7. Microcopy — textos de superficie
     Salen de los wireframes, que los transcriben literalmente en sus §2 y §3.
     Ninguno incluye la dirección de un servicio interno, un nombre de archivo
     de datos, una traza ni un código de error (RA-03, Experiencia §8.3).
     ========================================================================== */

  var TEXTOS = {
    producto: 'Fábrica de Geometría',

    // Estado degradado y reconexión (Wireframes-Estado-Degradado §2)
    indisponibleTitulo: 'Ahora no podemos traer tus trabajos',
    indisponibleCuerpo: 'El laboratorio está en pie, pero no está pudiendo llegar a tus datos en este momento. Probá de nuevo en un rato.',
    indisponibleAccion: 'Reintentar',
    reconexion: 'Se cortó la conexión con el laboratorio. Reintentando…',

    // Nota de la contraseña olvidada (Wireframes-Ingreso §2)
    notaContrasenaOlvidada: 'Si olvidaste tu contraseña, pedile al docente que te dé de alta otra vez. Tené en cuenta que eso borra también tus trabajos.',

    // Requisito declarado de la credencial (Wireframes-Credencial-Propia §3)
    requisitoContrasena: 'Vas a usarla para entrar; no hay forma de recuperarla.',

    // Notas del envío (Wireframes-Envio-De-Trabajo §2 y §3)
    notaPrevisualizacion: 'El dibujo es sólo para que veas lo que modelaste. No decide si tu trabajo verifica.',
    notaAccionUnica: 'Enviar es la única forma de guardar. Si el texto no verifica, queda en borrador y lo reenviás.',
    notaAdvertenciasNoBloquean: 'Las advertencias no impiden la entrega. Mirálas: te dicen que el valor que calculó tu programa no coincide con la geometría.',

    // Notas de la resolución (Wireframes-Resolucion-Del-Trabajo §2)
    notaDestinoComentario: 'Lo va a ver al abrir el trabajo. Podés dejarlo vacío.',

    // Nota de ausencia del listado de la comisión (Wireframes-Listado §2)
    notaAusencia: 'Sólo figuran los alumnos con trabajos entregados. Si un alumno no aparece, todavía no entregó nada.',

    // Aviso de arrastre de la baja (Wireframes-Panel-De-Cuentas §2)
    avisoArrastre: 'Esta baja elimina la cuenta y también TODOS sus trabajos. No se puede deshacer y no hay forma de recuperarlos.'
  };

  /* ==========================================================================
     8. Insignias — mapa de estado a par texto + tint
     Origen: Representacion-Fila-De-Trabajo.md §2 (los cuatro valores del
     trabajo) y Wireframes-Panel-De-Cuentas.md §3 (los tres de la cuenta).
     El texto está siempre presente; el color es refuerzo.
     ========================================================================== */

  var INSIGNIAS_TRABAJO = {
    'Borrador':   { clase: 'mq-insignia--neutro',   nota: 'el texto todavía no verificó, o recién se creó' },
    'Pendiente':  { clase: 'mq-insignia--atencion', nota: 'entregado, a la espera de revisión' },
    'Finalizado': { clase: 'mq-insignia--exito',    nota: 'aprobado. Terminal' },
    'Rechazado':  { clase: 'mq-insignia--peligro',  nota: 'rechazado. Terminal' }
  };

  var INSIGNIAS_CUENTA = {
    'Pendiente':  { clase: 'mq-insignia--atencion' },
    'Habilitada': { clase: 'mq-insignia--exito' },
    'Bloqueada':  { clase: 'mq-insignia--peligro' }
  };

  /* ==========================================================================
     9. Contrato de campos
     Traza cada campo que la maqueta exhibe contra su entidad de origen. Es el
     insumo directo de `Contrato-Datos-Maqueta.md`, que se emite al aprobar.
     ========================================================================== */

  var CONTRATO_DE_CAMPOS = [
    // Cuenta
    { entidad: 'Cuenta', campo: 'correo',          tipo: 'texto',              ejemplo: 'ana@ej.test',        origen: 'Wireframes-Panel-De-Cuentas.md §2' },
    { entidad: 'Cuenta', campo: 'nombre',          tipo: 'texto',              ejemplo: 'Ana',                origen: 'Wireframes-Panel-De-Cuentas.md §2' },
    { entidad: 'Cuenta', campo: 'apellido',        tipo: 'texto',              ejemplo: 'Diaz',               origen: 'Wireframes-Panel-De-Cuentas.md §2' },
    { entidad: 'Cuenta', campo: 'papel',           tipo: 'conjunto cerrado (2)', ejemplo: 'Alumno',           origen: 'Definicion-Modelo-De-Dominio.md §5.1' },
    { entidad: 'Cuenta', campo: 'estadoCuenta',    tipo: 'conjunto cerrado (3)', ejemplo: 'Habilitado',       origen: 'Definicion-Modelo-De-Dominio.md §65' },
    { entidad: 'Cuenta', campo: 'fechaRegistro',   tipo: 'fecha del sistema',  ejemplo: '05/08/2026',         origen: 'Wireframes-Panel-De-Cuentas.md §2' },

    // Trabajo
    { entidad: 'Trabajo', campo: 'nombre',         tipo: 'texto declarado',    ejemplo: 'Cubo y ortoedro',    origen: 'Wireframes-Panel-De-Trabajos-Del-Alumno.md §2' },
    { entidad: 'Trabajo', campo: 'fechaTrabajo',   tipo: 'fecha declarada por el alumno', ejemplo: '12/08/2026', origen: 'Wireframes-Panel-De-Trabajos-Del-Alumno.md §2' },
    { entidad: 'Trabajo', campo: 'descripcion',    tipo: 'texto declarado',    ejemplo: 'Entrega de la Act. 1…', origen: 'Wireframes-Vista-De-Trabajo.md §2' },
    { entidad: 'Trabajo', campo: 'estado',         tipo: 'conjunto cerrado (4)', ejemplo: 'Pendiente',        origen: 'Definicion-Modelo-De-Dominio.md §85' },
    { entidad: 'Trabajo', campo: 'textoOriginal',  tipo: 'cadena íntegra',     ejemplo: 'intake §20.E-1',     origen: 'RN-08, intake §20' },
    { entidad: 'Trabajo', campo: 'piezas',         tipo: 'entero',             ejemplo: '3',                  origen: 'Representacion-Fila-De-Trabajo.md §4' },
    { entidad: 'Trabajo', campo: 'advertencias',   tipo: 'entero',             ejemplo: '2',                  origen: 'Representacion-Fila-De-Trabajo.md §4' },
    { entidad: 'Trabajo', campo: 'comentario',     tipo: 'texto libre opcional (0..1)', ejemplo: 'Revisá el área del cubo.', origen: 'Wireframes-Resolucion-Del-Trabajo.md §2' },
    { entidad: 'Trabajo', campo: 'fechaDesenlace', tipo: 'fecha del sistema',  ejemplo: '13/08/2026',         origen: 'Wireframes-Resolucion-Del-Trabajo.md §2' },

    // Observación
    { entidad: 'Observación', campo: 'severidad',  tipo: 'conjunto cerrado (2)', ejemplo: 'Advertencia',      origen: 'Definicion-Modelo-De-Dominio.md §131' },
    { entidad: 'Observación', campo: 'figura',     tipo: 'índice entero',      ejemplo: '1',                  origen: 'RN-09; intake §20.E-1' },
    { entidad: 'Observación', campo: 'campo',      tipo: 'texto',              ejemplo: 'Area',               origen: 'RN-09; intake §20.E-1' },
    { entidad: 'Observación', campo: 'declarado',  tipo: 'texto sin reformatear', ejemplo: '36.00',           origen: 'intake §20.E-1 punto 3' },
    { entidad: 'Observación', campo: 'derivado',   tipo: 'texto sin reformatear', ejemplo: '54.00',           origen: 'intake §20.E-1 punto 3' },

    // Condición del dibujo — no es una observación
    { entidad: 'Condición del dibujo', campo: 'indice', tipo: 'índice entero', ejemplo: '1',                  origen: 'Wireframes-Vista-De-Trabajo.md §3' },
    { entidad: 'Condición del dibujo', campo: 'motivo', tipo: 'texto',         ejemplo: 'tipo no dibujable',  origen: 'Wireframes-Vista-De-Trabajo.md §2' },

    // Identidad de versión
    { entidad: 'Identidad de versión', campo: 'versionLegible',              tipo: 'cadena ya formada', ejemplo: 'Versión 1.4.2', origen: 'Representacion-Sello-De-Version.md §2' },
    { entidad: 'Identidad de versión', campo: 'identificadorDeConstruccion', tipo: 'referencia opaca', ejemplo: 'a3f81c6',        origen: 'Representacion-Sello-De-Version.md §2' },
    { entidad: 'Identidad de versión', campo: 'esPreliminar',                tipo: 'booleano',         ejemplo: 'false',          origen: 'Design-Rules-Identidad-De-Version.md §2' },
    { entidad: 'Identidad de versión', campo: 'origenIndeterminado',         tipo: 'booleano',         ejemplo: 'false',          origen: 'Design-Rules-Identidad-De-Version.md §2' }
  ];

  /* ==========================================================================
     10. Notas de coherencia que la maqueta NO puede contradecir
     ========================================================================== */

  var INVARIANTES_DE_LA_MAQUETA = [
    'El texto semilla (§20.E-1) produce exactamente 3 piezas y 2 advertencias.',
    'El cilindro de §20.E-1 no produce ninguna observación: su área declara ' +
      '113.10 y sus componentes suman 113.09, con diferencia de exactamente ' +
      '0.01. El operador de tolerancia es estricto —advierte si la diferencia ' +
      'es MAYOR que 0.01— así que ese caso no advierte. Si la maqueta mostrara ' +
      'tres advertencias, contradiría el caso de prueba canónico del producto.',
    'Los valores declarado y derivado se muestran sin reformatear.',
    'El comentario del administrador no es una observación ni una calificación.',
    '`Pendiente` nombra dos estados distintos: el de la cuenta y el del trabajo. ' +
      'Todo texto visible lo califica.',
    'Ningún mensaje visible incluye la dirección de un servicio interno.'
  ];

  /* ========================================================================== */

  global.DatosMaqueta = {
    identidadDeVersion: IDENTIDAD_DE_VERSION,
    cuentas: CUENTAS,
    credencialDeMaqueta: CREDENCIAL_DE_MAQUETA,
    trabajos: TRABAJOS,
    borradorDeEnvio: BORRADOR_DE_ENVIO,
    textos: TEXTOS,
    insigniasTrabajo: INSIGNIAS_TRABAJO,
    insigniasCuenta: INSIGNIAS_CUENTA,
    contratoDeCampos: CONTRATO_DE_CAMPOS,

    // Contrato de fachada de GeometriaFactory-Visor, validado dentro de esta
    // maqueta en lugar de tener maqueta propia.
    funcionesDeLaFachada: FUNCIONES_DE_LA_FACHADA,
    condicionesDeLaFachada: CONDICIONES_DE_LA_FACHADA,
    propiedadesTransversales: PROPIEDADES_TRANSVERSALES,
    ejerciciosDeFachada: EJERCICIOS_DE_FACHADA,
    ejercicio: function (id) {
      return EJERCICIOS_DE_FACHADA.filter(function (e) { return e.id === id; })[0] || null;
    },
    invariantes: INVARIANTES_DE_LA_MAQUETA,

    // Ayudas de lectura
    cuenta: function (id) {
      return CUENTAS.filter(function (c) { return c.id === id; })[0] || null;
    },
    trabajo: function (id) {
      return TRABAJOS.filter(function (t) { return t.id === id; })[0] || null;
    },
    trabajosDe: function (cuentaId) {
      return TRABAJOS.filter(function (t) { return t.cuentaId === cuentaId; });
    },
    trabajosEntregados: function () {
      return TRABAJOS.filter(function (t) { return t.estado !== 'Borrador'; });
    }
  };

})(window);
