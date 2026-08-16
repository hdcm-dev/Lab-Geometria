// Empaqueta el visor como BIBLIOTECA EN `window`, con un nombre propio y sin globales sueltas,
// y con el motor gráfico DENTRO del bundle: sin red de distribución externa (intake §17.7.P.1
// y §17.7.P.3; es lo que mide `PT-03`). En la etapa `a` no hay motor gráfico todavía: el bundle
// es «vacío pero real» (intake §15).

const path = require('path');

module.exports = {
  entry: './src/main.ts',
  output: {
    path: path.resolve(__dirname, 'dist'),
    filename: 'geometriafactory-visor.js',
    library: {
      name: 'GeometriaFactoryViewer',
      type: 'window'
    },
    clean: true
  },
  resolve: {
    extensions: ['.ts', '.js']
  },
  module: {
    rules: [
      {
        test: /\.ts$/,
        use: 'ts-loader',
        exclude: /node_modules/
      }
    ]
  },
  // Sin `externals`: nada se resuelve desde una red de distribución externa.
  externals: {},

  // EL PRESUPUESTO DE TAMAÑO SE DECLARA, EN LUGAR DE APAGAR EL AVISO.
  //
  // El valor por omisión de webpack son 244 KiB, y con el motor gráfico adentro el paquete pesa
  // más. El aviso no es un defecto: es una recomendación que **supone que se puede partir el
  // paquete o traer parte por red**, y este producto declara lo contrario en `PT-03` —el motor
  // entra empaquetado, sin red de distribución— porque el front tiene que funcionar sin acceso a
  // ninguna. Apagar el aviso dejaría de avisar el día que el paquete crezca de verdad.
  //
  // EL UMBRAL ES UNA MEDICIÓN Y NO UNA ASPIRACIÓN: sale del tamaño medido con el motor adentro
  // —483 KiB el 2026-08-16— con un margen chico. Un crecimiento que lo pase vuelve a avisar, que
  // es exactamente lo que se quiere conservar. **[decisión de la etapa `g`, declarada.]**
  performance: {
    hints: 'warning',
    maxAssetSize: 560 * 1024,
    maxEntrypointSize: 560 * 1024
  }
};
