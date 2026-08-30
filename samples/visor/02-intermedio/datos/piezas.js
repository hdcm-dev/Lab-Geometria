// Las piezas de los cinco escenarios YA RECONSTRUIDAS, que es lo que el visor recibe
// (`ADR-08006`). Producidas corriendo el intérprete REAL del producto sobre los `.txt`
// de esta misma carpeta: ningún valor se escribió a mano.
//
// POR QUÉ UNA ETIQUETA DE GUION Y NO UN ARCHIVO QUE LA PÁGINA LEA: §4 pide abrir
// `index.html` directamente en un navegador, y una página abierta así no puede leer
// archivos vecinos. Es la misma decisión —y el mismo motivo medido— que en el sample
// `visor/01-basico`.
//
// LOS `.txt` SIGUEN ACÁ y son el dato de origen. El visor no los lee: desde `ADR-08006`
// la reconstrucción es del laboratorio, y este sample no la rehace.
window.PIEZAS = {
  "E2": [
    {
      "position": 0,
      "type": "Orthohedron",
      "declaredArea": 686,
      "derivedArea": 686,
      "declaredVolume": 343,
      "derivedVolume": 1029,
      "declaredLength": null,
      "declaredWidth": null,
      "declaredRadius": null,
      "components": [
        {
          "position": 0,
          "role": "Base",
          "type": "Rectangle",
          "declaredLength": 7,
          "declaredWidth": 7,
          "declaredRadius": null,
          "declaredArea": 49
        },
        {
          "position": 1,
          "role": "Base",
          "type": "Rectangle",
          "declaredLength": 7,
          "declaredWidth": 7,
          "declaredRadius": null,
          "declaredArea": 49
        },
        {
          "position": 2,
          "role": "Lateral",
          "type": "Rectangle",
          "declaredLength": 21,
          "declaredWidth": 7,
          "declaredRadius": null,
          "declaredArea": 147
        },
        {
          "position": 3,
          "role": "Lateral",
          "type": "Rectangle",
          "declaredLength": 21,
          "declaredWidth": 7,
          "declaredRadius": null,
          "declaredArea": 147
        },
        {
          "position": 4,
          "role": "Lateral",
          "type": "Rectangle",
          "declaredLength": 21,
          "declaredWidth": 7,
          "declaredRadius": null,
          "declaredArea": 147
        },
        {
          "position": 5,
          "role": "Lateral",
          "type": "Rectangle",
          "declaredLength": 21,
          "declaredWidth": 7,
          "declaredRadius": null,
          "declaredArea": 147
        }
      ]
    }
  ],
  "E5": [
    {
      "position": 0,
      "type": "Cube",
      "declaredArea": 54,
      "derivedArea": 54,
      "declaredVolume": 27,
      "derivedVolume": 27,
      "declaredLength": null,
      "declaredWidth": null,
      "declaredRadius": null,
      "components": [
        {
          "position": 0,
          "role": "Face",
          "type": "Square",
          "declaredLength": 3,
          "declaredWidth": 3,
          "declaredRadius": null,
          "declaredArea": 9
        },
        {
          "position": 1,
          "role": "Face",
          "type": "Square",
          "declaredLength": 3,
          "declaredWidth": 3,
          "declaredRadius": null,
          "declaredArea": 9
        },
        {
          "position": 2,
          "role": "Face",
          "type": "Square",
          "declaredLength": 3,
          "declaredWidth": 3,
          "declaredRadius": null,
          "declaredArea": 9
        },
        {
          "position": 3,
          "role": "Face",
          "type": "Square",
          "declaredLength": 3,
          "declaredWidth": 3,
          "declaredRadius": null,
          "declaredArea": 9
        },
        {
          "position": 4,
          "role": "Face",
          "type": "Square",
          "declaredLength": 3,
          "declaredWidth": 3,
          "declaredRadius": null,
          "declaredArea": 9
        },
        {
          "position": 5,
          "role": "Face",
          "type": "Square",
          "declaredLength": 3,
          "declaredWidth": 3,
          "declaredRadius": null,
          "declaredArea": 9
        }
      ]
    }
  ],
  "E6": [
    {
      "position": 0,
      "type": "Rectangle",
      "declaredArea": 0,
      "derivedArea": 0,
      "declaredVolume": null,
      "derivedVolume": null,
      "declaredLength": 0,
      "declaredWidth": 5,
      "declaredRadius": null,
      "components": []
    }
  ],
  "E7": [
    {
      "position": 0,
      "type": "Cylinder",
      "declaredArea": 150.8,
      "derivedArea": 150.79,
      "declaredVolume": 141.37,
      "derivedVolume": 141.3716694115407,
      "declaredLength": null,
      "declaredWidth": null,
      "declaredRadius": null,
      "components": [
        {
          "position": 0,
          "role": "Cap",
          "type": "Circle",
          "declaredLength": null,
          "declaredWidth": null,
          "declaredRadius": 3,
          "declaredArea": 28.27
        },
        {
          "position": 1,
          "role": "Cap",
          "type": "Circle",
          "declaredLength": null,
          "declaredWidth": null,
          "declaredRadius": 3,
          "declaredArea": 28.27
        },
        {
          "position": 2,
          "role": "Side",
          "type": "DevelopedRectangle",
          "declaredLength": 5,
          "declaredWidth": 18.85,
          "declaredRadius": null,
          "declaredArea": 94.25
        }
      ]
    },
    {
      "position": 1,
      "type": "Cube",
      "declaredArea": 96,
      "derivedArea": 96,
      "declaredVolume": 64,
      "derivedVolume": 64,
      "declaredLength": null,
      "declaredWidth": null,
      "declaredRadius": null,
      "components": [
        {
          "position": 0,
          "role": "Face",
          "type": "Square",
          "declaredLength": 4,
          "declaredWidth": 4,
          "declaredRadius": null,
          "declaredArea": 16
        },
        {
          "position": 1,
          "role": "Face",
          "type": "Square",
          "declaredLength": 4,
          "declaredWidth": 4,
          "declaredRadius": null,
          "declaredArea": 16
        },
        {
          "position": 2,
          "role": "Face",
          "type": "Square",
          "declaredLength": 4,
          "declaredWidth": 4,
          "declaredRadius": null,
          "declaredArea": 16
        },
        {
          "position": 3,
          "role": "Face",
          "type": "Square",
          "declaredLength": 4,
          "declaredWidth": 4,
          "declaredRadius": null,
          "declaredArea": 16
        },
        {
          "position": 4,
          "role": "Face",
          "type": "Square",
          "declaredLength": 4,
          "declaredWidth": 4,
          "declaredRadius": null,
          "declaredArea": 16
        },
        {
          "position": 5,
          "role": "Face",
          "type": "Square",
          "declaredLength": 4,
          "declaredWidth": 4,
          "declaredRadius": null,
          "declaredArea": 16
        }
      ]
    },
    {
      "position": 2,
      "type": "Orthohedron",
      "declaredArea": 208,
      "derivedArea": 208,
      "declaredVolume": 192,
      "derivedVolume": 192,
      "declaredLength": null,
      "declaredWidth": null,
      "declaredRadius": null,
      "components": [
        {
          "position": 0,
          "role": "Base",
          "type": "Rectangle",
          "declaredLength": 6,
          "declaredWidth": 4,
          "declaredRadius": null,
          "declaredArea": 24
        },
        {
          "position": 1,
          "role": "Base",
          "type": "Rectangle",
          "declaredLength": 6,
          "declaredWidth": 4,
          "declaredRadius": null,
          "declaredArea": 24
        },
        {
          "position": 2,
          "role": "Lateral",
          "type": "Rectangle",
          "declaredLength": 6,
          "declaredWidth": 8,
          "declaredRadius": null,
          "declaredArea": 48
        },
        {
          "position": 3,
          "role": "Lateral",
          "type": "Rectangle",
          "declaredLength": 6,
          "declaredWidth": 8,
          "declaredRadius": null,
          "declaredArea": 48
        },
        {
          "position": 4,
          "role": "Lateral",
          "type": "Rectangle",
          "declaredLength": 4,
          "declaredWidth": 8,
          "declaredRadius": null,
          "declaredArea": 32
        },
        {
          "position": 5,
          "role": "Lateral",
          "type": "Rectangle",
          "declaredLength": 4,
          "declaredWidth": 8,
          "declaredRadius": null,
          "declaredArea": 32
        }
      ]
    },
    {
      "position": 3,
      "type": "Rectangle",
      "declaredArea": 18,
      "derivedArea": 18,
      "declaredVolume": null,
      "derivedVolume": null,
      "declaredLength": 6,
      "declaredWidth": 3,
      "declaredRadius": null,
      "components": []
    },
    {
      "position": 4,
      "type": "Square",
      "declaredArea": 16,
      "derivedArea": 16,
      "declaredVolume": null,
      "derivedVolume": null,
      "declaredLength": 4,
      "declaredWidth": 4,
      "declaredRadius": null,
      "components": []
    },
    {
      "position": 5,
      "type": "Circle",
      "declaredArea": 19.63,
      "derivedArea": 19.634954084936208,
      "declaredVolume": null,
      "derivedVolume": null,
      "declaredLength": null,
      "declaredWidth": null,
      "declaredRadius": 2.5,
      "components": []
    }
  ],
  "E8": [
    {
      "position": 0,
      "type": "Orthohedron",
      "declaredArea": 208,
      "derivedArea": null,
      "declaredVolume": 192,
      "derivedVolume": 192,
      "declaredLength": null,
      "declaredWidth": null,
      "declaredRadius": null,
      "components": [
        {
          "position": 0,
          "role": "Base",
          "type": "Rectangle",
          "declaredLength": 6,
          "declaredWidth": 4,
          "declaredRadius": null,
          "declaredArea": 24
        },
        {
          "position": 1,
          "role": "Base",
          "type": "Rectangle",
          "declaredLength": 6,
          "declaredWidth": 4,
          "declaredRadius": null,
          "declaredArea": 24
        }
      ]
    }
  ]
};
