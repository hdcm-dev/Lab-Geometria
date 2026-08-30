// Las piezas de `E-1` YA RECONSTRUIDAS, que es lo que el visor recibe (`ADR-08006`).
//
// POR QUÉ UNA ETIQUETA DE GUION Y NO UN ARCHIVO DE DATOS QUE LA PÁGINA LEA. §4 pide
// abrir `index.html` DIRECTAMENTE en un navegador, y una página abierta así no puede
// leer archivos vecinos: el navegador se lo prohíbe. Se probó, y el sample se colgaba
// esperando datos que nunca llegaban. Una etiqueta de guion sí carga, y es lo único
// que funciona igual abierto a mano y conducido por el recorrido de `tests/`.
//
// LOS VALORES NO SE INVENTARON: salen de correr el intérprete real del producto sobre
// `datos/E1.txt`, que es lo que publica el sample `infrastructure/01-basico`. El porqué
// de que haya dos archivos está en `datos/POR-QUE-DOS-ARCHIVOS.md`.
window.PIEZAS_E1 = [
  {
    "position": 0,
    "type": "Cylinder",
    "declaredArea": 113.1,
    "derivedArea": 113.09,
    "declaredVolume": 84.82,
    "derivedVolume": 84.82300164692441,
    "components": [
      {
        "position": 0,
        "role": "Cap",
        "type": "Circle",
        "declaredRadius": 3.0,
        "declaredArea": 28.27
      },
      {
        "position": 1,
        "role": "Cap",
        "type": "Circle",
        "declaredRadius": 3.0,
        "declaredArea": 28.27
      },
      {
        "position": 2,
        "role": "Side",
        "type": "DevelopedRectangle",
        "declaredLength": 3.0,
        "declaredWidth": 18.85,
        "declaredArea": 56.55
      }
    ]
  },
  {
    "position": 1,
    "type": "Cube",
    "declaredArea": 36.0,
    "derivedArea": 54.0,
    "declaredVolume": 27.0,
    "derivedVolume": 27.0,
    "components": [
      {
        "position": 0,
        "role": "Face",
        "type": "Square",
        "declaredLength": 3.0,
        "declaredWidth": 3.0,
        "declaredArea": 9.0
      },
      {
        "position": 1,
        "role": "Face",
        "type": "Square",
        "declaredLength": 3.0,
        "declaredWidth": 3.0,
        "declaredArea": 9.0
      },
      {
        "position": 2,
        "role": "Face",
        "type": "Square",
        "declaredLength": 3.0,
        "declaredWidth": 3.0,
        "declaredArea": 9.0
      },
      {
        "position": 3,
        "role": "Face",
        "type": "Square",
        "declaredLength": 3.0,
        "declaredWidth": 3.0,
        "declaredArea": 9.0
      },
      {
        "position": 4,
        "role": "Face",
        "type": "Square",
        "declaredLength": 3.0,
        "declaredWidth": 3.0,
        "declaredArea": 9.0
      },
      {
        "position": 5,
        "role": "Face",
        "type": "Square",
        "declaredLength": 3.0,
        "declaredWidth": 3.0,
        "declaredArea": 9.0
      }
    ]
  },
  {
    "position": 2,
    "type": "Orthohedron",
    "declaredArea": 686.0,
    "derivedArea": 686.0,
    "declaredVolume": 343.0,
    "derivedVolume": 1029.0,
    "components": [
      {
        "position": 0,
        "role": "Base",
        "type": "Rectangle",
        "declaredLength": 7.0,
        "declaredWidth": 7.0,
        "declaredArea": 49.0
      },
      {
        "position": 1,
        "role": "Base",
        "type": "Rectangle",
        "declaredLength": 7.0,
        "declaredWidth": 7.0,
        "declaredArea": 49.0
      },
      {
        "position": 2,
        "role": "Lateral",
        "type": "Rectangle",
        "declaredLength": 21.0,
        "declaredWidth": 7.0,
        "declaredArea": 147.0
      },
      {
        "position": 3,
        "role": "Lateral",
        "type": "Rectangle",
        "declaredLength": 21.0,
        "declaredWidth": 7.0,
        "declaredArea": 147.0
      },
      {
        "position": 4,
        "role": "Lateral",
        "type": "Rectangle",
        "declaredLength": 21.0,
        "declaredWidth": 7.0,
        "declaredArea": 147.0
      },
      {
        "position": 5,
        "role": "Lateral",
        "type": "Rectangle",
        "declaredLength": 21.0,
        "declaredWidth": 7.0,
        "declaredArea": 147.0
      }
    ]
  }
];
