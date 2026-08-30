// Los DOS TRABAJOS entre los que el recorrido va y vuelve diez veces, ya
// reconstruidos (`ADR-08006`). Producidos corriendo el intérprete REAL del producto
// sobre los `.txt` de esta carpeta: ningún valor se escribió a mano.
//
// SON DOS Y NO UNO PORQUE `PT-02` MIDE LA IDA Y LA VUELTA. Cada escena toma un
// contexto gráfico y el navegador permite pocos vivos —del orden de ocho a
// dieciséis—: si al salir no se libera, el navegador descarta el más viejo sin
// avisar y la escena se apaga sin error. Diez recorridos está elegido por encima
// de ese límite, que es lo que hace aparecer el defecto.
window.TRABAJOS = {
  "E1": [
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
  ]
};
