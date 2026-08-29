namespace GeometriaFactory.Samples.Infrastructure.Basico;

/// <summary>El snapshot de §6, transcripto sin cambios, y su comparación línea por línea.</summary>
internal static class SalidaEsperada
{
    private static readonly string[] Snapshot =
    [
        "[E-1] Figuras del conjunto raiz: 3 | Piezas reconstruidas: 3 | Observaciones: 2",
        "[E-1] Cilindro: 2 tapas Circulo y 1 lado RectanguloDesarrollado | Observaciones: 0 (tolerancia estricta)",
        "[E-1] Cubo: advertencia de area declarado=36.00 derivado=54.00",
        "[E-1] Ortoedro: advertencia de volumen declarado=343.00 derivado=1029.00 | area sin observacion",
        "[E-2] Parseo con comas finales: exitoso (T2) | Clave Tapas leida como bases (T1)",
        "[E-2] Estructura: 1 pieza, 2 bases, 4 laterales | Observaciones: 1 advertencia de volumen",
        "[E-3] Caras Cuadrado interpretadas (T3) | advertencia de area declarado=36.00 derivado=54.00",
        "[E-4] Caras Rectangulo interpretadas (T3) | Observaciones: 0",
        "[E-5] Figuras del conjunto raiz: 2 | Piezas reconstruidas: 1",
        "[E-5] Observacion Error: indice-figura=1 campo=Tipo",
        "[E-6] Dimension 0.00: la figura se interpreta y no se descarta | Errores de interpretacion: 0",
        "[E-7] Piezas reconstruidas: 6 | volumetricos=3 planos=3 | Clave Bases leida como bases (T1)",
        "[E-7] Ortoedro: ancho=6.00 profundidad=4.00 altura=8.00",
    ];

    internal static int Comparar(IReadOnlyList<string> producidas)
    {
        var diferencias = 0;
        Console.WriteLine();
        Console.WriteLine("Verificación contra el snapshot de §6:");
        for (var i = 0; i < Math.Max(Snapshot.Length, producidas.Count); i++)
        {
            var esperada = i < Snapshot.Length ? Snapshot[i] : "(línea de más)";
            var producida = i < producidas.Count ? producidas[i] : "(línea ausente)";
            if (string.Equals(esperada, producida, StringComparison.Ordinal)) continue;
            diferencias++;
            Console.WriteLine($"  línea {i + 1} difiere");
            Console.WriteLine($"    esperada: {esperada}");
            Console.WriteLine($"    obtenida: {producida}");
        }
        if (diferencias == 0) { Console.WriteLine($"  CONFORME · las {Snapshot.Length} líneas coinciden"); return 0; }
        Console.WriteLine($"  NO CONFORME · {diferencias} línea(s) difieren del snapshot");
        return 1;
    }
}
