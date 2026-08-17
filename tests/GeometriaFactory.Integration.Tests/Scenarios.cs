namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// Los ocho escenarios del `PRODUCT-INTAKE` §20, transcriptos **carácter por carácter**.
/// </summary>
/// <remarks>
/// NO SE NORMALIZÓ NADA, y es el punto: la sangría irregular de `E-1`, las **dos comas finales** de
/// `E-2`, el objeto suelto sin array de `E-3` y `E-4`, y las comillas de `"3,50"` en `E-8` son el
/// dato que existe. Un fixture «prolijo» probaría un texto que ningún alumno entrega.
///
/// `E-1` ESTÁ EDITADO A MANO EN LA FUENTE, y el intake lo declara: usa `"Bases"` donde el programa
/// emite `"Tapas"` y no trae comas finales, de modo que ejercita el camino feliz de la lectura de
/// claves y **no** las tolerancias T1 y T2. Para eso están `E-2` y `E-3`.
/// </remarks>
internal static class Scenarios
{
    /// <summary>§20.E-1 · JSON semilla del visor: tres piezas y dos advertencias. Estado: medido.</summary>
    public const string E1 = """
        [
          {
          "Tipo": "Cilindro",
          "Tapas":
          [
            {
          "Tipo":"Circulo",
          "Radio": 3.00,
          "Area": 28.27
        },
            {
          "Tipo":"Circulo",
          "Radio": 3.00,
          "Area": 28.27
        }
          ],
          "Lado":
        {
          "Tipo": "RectanguloDesarrollado",
          "Largo": 3.00,
          "Ancho": 18.85,
          "Area": 56.55
        },
          "Area": 113.10,
          "Volumen": 84.82
        },
          {
          "Tipo": "Cubo",
          "Caras":
          [
            { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 },
            { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 },
            { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 },
            { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 },
            { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 },
            { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 }
          ],
          "Area": 36.00,
          "Volumen": 27.00
        },
          {
          "Tipo": "Ortoedro",
          "Bases":
          [
            { "Tipo": "Rectangulo", "Largo": 7.00, "Ancho": 7.00, "Area": 49.00 },
            { "Tipo": "Rectangulo", "Largo": 7.00, "Ancho": 7.00, "Area": 49.00 }
          ],
          "Laterales":
            [
              { "Tipo": "Rectangulo", "Largo": 21.00, "Ancho": 7.00, "Area": 147.00 },
              { "Tipo": "Rectangulo", "Largo": 21.00, "Ancho": 7.00, "Area": 147.00 },
              { "Tipo": "Rectangulo", "Largo": 21.00, "Ancho": 7.00, "Area": 147.00 },
              { "Tipo": "Rectangulo", "Largo": 21.00, "Ancho": 7.00, "Area": 147.00 }
            ],
          "Area": 686.00,
          "Volumen": 343.00
        }
        ]
        """;

    /// <summary>
    /// §20.E-2 · `Ortoedro(7,7,21)` tal como lo emite el programa. Estado: derivado.
    /// **Trae `Tapas` (T1) y dos comas finales (T2): no es JSON estrictamente válido.**
    /// </summary>
    public const string E2 = """
        [
        {
          "Tipo": "Ortoedro",
          "Tapas":
          [
            { "Tipo": "Rectangulo", "Largo": 7.00, "Ancho": 7.00, "Area": 49.00 },
            { "Tipo": "Rectangulo", "Largo": 7.00, "Ancho": 7.00, "Area": 49.00 }
          ],
          "Laterales":
            [
              { "Tipo": "Rectangulo", "Largo": 21.00, "Ancho": 7.00, "Area": 147.00 },
              { "Tipo": "Rectangulo", "Largo": 21.00, "Ancho": 7.00, "Area": 147.00 },
              { "Tipo": "Rectangulo", "Largo": 21.00, "Ancho": 7.00, "Area": 147.00 },
              { "Tipo": "Rectangulo", "Largo": 21.00, "Ancho": 7.00, "Area": 147.00 },
            ],
          "Area": 686.00,
          "Volumen": 343.00
        },
        ]
        """;

    /// <summary>§20.E-3 · `Cubo(3)` de Ejemplo1: caras `Cuadrado` y área declarada 36.00. Estado: medido.</summary>
    public const string E3 = """
        {
          "Tipo": "Cubo",
          "Caras":
          [
            { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 },
            { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 },
            { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 },
            { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 },
            { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 },
            { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 }
          ],
          "Area": 36.00,
          "Volumen": 27.00
        }
        """;

    /// <summary>§20.E-4 · `Cubo(3)` de Ejemplo2: caras `Rectangulo` y área declarada 54.00. Estado: derivado.</summary>
    public const string E4 = """
        {
          "Tipo": "Cubo",
          "Caras":
          [
            {"Tipo": "Rectangulo", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00},
            {"Tipo": "Rectangulo", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00},
            {"Tipo": "Rectangulo", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00},
            {"Tipo": "Rectangulo", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00},
            {"Tipo": "Rectangulo", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00},
            {"Tipo": "Rectangulo", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00}
          ],
          "Area": 54.00,
          "Volumen": 27.00
        }
        """;

    /// <summary>
    /// §20.E-5 · Tipo desconocido: error con índice de figura y campo. Estado: reconstruido.
    /// **El primer elemento es válido a propósito**: obliga a que el índice reportado sea 1 y no 0.
    /// </summary>
    public const string E5 = """
        [
          {
            "Tipo": "Cubo",
            "Caras": [
              { "Tipo": "Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 },
              { "Tipo": "Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 },
              { "Tipo": "Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 },
              { "Tipo": "Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 },
              { "Tipo": "Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 },
              { "Tipo": "Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 }
            ],
            "Area": 54.00,
            "Volumen": 27.00
          },
          { "Tipo": "Piramide", "Largo": 5.00, "Ancho": 5.00, "Area": 25.00 }
        ]
        """;

    /// <summary>§20.E-6 · Dimensión en 0.00: la figura no se descarta. Estado: reconstruido.</summary>
    public const string E6 = """
        [
          { "Tipo": "Rectangulo", "Largo": 0.00, "Ancho": 5.00, "Area": 0.00 }
        ]
        """;

    /// <summary>§20.E-7 · Cobertura de los seis tipos dibujables. Estado: derivado.</summary>
    public const string E7 = """
        [
          {
            "Tipo": "Cilindro",
            "Tapas": [
              { "Tipo": "Circulo", "Radio": 3.00, "Area": 28.27 },
              { "Tipo": "Circulo", "Radio": 3.00, "Area": 28.27 }
            ],
            "Lado": { "Tipo": "RectanguloDesarrollado", "Largo": 5.00, "Ancho": 18.85, "Area": 94.25 },
            "Area": 150.80,
            "Volumen": 141.37
          },
          {
            "Tipo": "Cubo",
            "Caras": [
              { "Tipo": "Cuadrado", "Largo": 4.00, "Ancho": 4.00, "Area": 16.00 },
              { "Tipo": "Cuadrado", "Largo": 4.00, "Ancho": 4.00, "Area": 16.00 },
              { "Tipo": "Cuadrado", "Largo": 4.00, "Ancho": 4.00, "Area": 16.00 },
              { "Tipo": "Cuadrado", "Largo": 4.00, "Ancho": 4.00, "Area": 16.00 },
              { "Tipo": "Cuadrado", "Largo": 4.00, "Ancho": 4.00, "Area": 16.00 },
              { "Tipo": "Cuadrado", "Largo": 4.00, "Ancho": 4.00, "Area": 16.00 }
            ],
            "Area": 96.00,
            "Volumen": 64.00
          },
          {
            "Tipo": "Ortoedro",
            "Bases": [
              { "Tipo": "Rectangulo", "Largo": 6.00, "Ancho": 4.00, "Area": 24.00 },
              { "Tipo": "Rectangulo", "Largo": 6.00, "Ancho": 4.00, "Area": 24.00 }
            ],
            "Laterales": [
              { "Tipo": "Rectangulo", "Largo": 6.00, "Ancho": 8.00, "Area": 48.00 },
              { "Tipo": "Rectangulo", "Largo": 6.00, "Ancho": 8.00, "Area": 48.00 },
              { "Tipo": "Rectangulo", "Largo": 4.00, "Ancho": 8.00, "Area": 32.00 },
              { "Tipo": "Rectangulo", "Largo": 4.00, "Ancho": 8.00, "Area": 32.00 }
            ],
            "Area": 208.00,
            "Volumen": 192.00
          },
          { "Tipo": "Rectangulo", "Largo": 6.00, "Ancho": 3.00, "Area": 18.00 },
          { "Tipo": "Cuadrado",   "Largo": 4.00, "Ancho": 4.00, "Area": 16.00 },
          { "Tipo": "Circulo",    "Radio": 2.50, "Area": 19.63 }
        ]
        """;

    /// <summary>
    /// §20.E-8 · Dimensión no legible. Estado: reconstruido.
    /// **`"3,50"` es una cadena y no un número**: `double.ToString()` bajo cultura `es-AR` escribe
    /// la coma decimal. Es el modo de falla más probable de los ocho escenarios, porque lo produce
    /// la configuración regional de la máquina y no un error del alumno.
    /// </summary>
    public const string E8 = """
        [
          {
            "Tipo": "Ortoedro",
            "Bases": [
              { "Tipo": "Rectangulo", "Largo": 6.00, "Ancho": 4.00, "Area": 24.00 },
              { "Tipo": "Rectangulo", "Largo": 6.00, "Ancho": 4.00, "Area": 24.00 }
            ],
            "Altura": 8.00,
            "Area": 208.00,
            "Volumen": 192.00
          },
          {
            "Tipo": "Cubo",
            "Caras": [
              { "Tipo": "Cuadrado", "Largo": "3,50", "Ancho": "3,50", "Area": 12.25 }
            ],
            "Area": 73.50,
            "Volumen": 42.875
          }
        ]
        """;
}
