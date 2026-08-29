namespace GeometriaFactory.Samples.Domain.Intermedio;

/// <summary>El snapshot de §6, transcripto sin cambios, y su comparación línea por línea.</summary>
/// <remarks>
/// Transcripto y no leído del markdown, por el mismo motivo que en el `01-basico`: leerlo del
/// documento haría que el sample pasara siempre, porque cualquier cambio en la salida se
/// propagaría solo al criterio que debería atraparlo.
/// </remarks>
internal static class SalidaEsperada
{
    private static readonly string[] Snapshot =
    [
        "[E-1] Trabajo constituido: texto-identico=si estado=Borrador",
        "[E-1] Piezas adoptadas: 3 | Observaciones adoptadas: 2 | Errores de validacion: 0",
        "[E-1] Envio: estado=Pendiente (las advertencias no impiden el envio)",
        "[E-3] Observacion adoptada: especie=Advertencia campo=Area declarado=36.00 derivado=54.00",
        "[E-4] Observaciones adoptadas: 0 (mismo cubo de lado 3, area declarada coincidente)",
        "[E-6] Piezas adoptadas: 1 | Envio: estado=Pendiente (el cero es un valor, no una ausencia)",
        "[E-5] Pieza del indice 0 adoptada | Pieza del indice 1 rechazada: TIPO_DE_PIEZA_DESCONOCIDO",
        "[E-5] Posicion 1 reservada: observacion de error aceptada sobre esa posicion",
        "[E-5] Observacion de error: indice-figura=1 campo=Tipo",
        "[E-5] Envio: estado=Borrador (RN-02005: un error de validacion retiene el trabajo)",
        "[E-8] Observacion de error localizada: indice-figura=1 campo=Largo",
        "[E-8] Envio: estado=Borrador | texto-original-intacto=si",
        "Trabajos recorridos: 6 | Envios a Pendiente: 4 | Envios retenidos en Borrador: 2 | Excepciones: 0",
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

        if (diferencias == 0)
        {
            Console.WriteLine($"  CONFORME · las {Snapshot.Length} líneas coinciden");
            return 0;
        }

        Console.WriteLine($"  NO CONFORME · {diferencias} línea(s) difieren del snapshot");
        return 1;
    }
}
