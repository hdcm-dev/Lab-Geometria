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
  externals: {}
};
