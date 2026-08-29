namespace GeometriaFactory.Samples.Domain.Avanzado;

/// <summary>El snapshot de §6, transcripto sin cambios, y su comparación línea por línea.</summary>
internal static class SalidaEsperada
{
    private static readonly string[] Snapshot =
    [
        "[1] Trabajo ajeno: WORK_NOT_FOUND_FOR_REQUESTER",
        "[2] Trabajo inexistente: WORK_NOT_FOUND_FOR_REQUESTER",
        "[3] Resultados [1] y [2] comparados campo por campo: identicos=si",
        "[4] Alcance del administrador: en-alcance=3 fuera-de-alcance=1 (Borrador)",
        "[5] Eliminacion por el administrador admitida en: Pendiente, Finalizado, Rechazado",
        "[6] Aprobar trabajo en Pendiente: estado=Finalizado comentario=ausente",
        "[7] Rechazar trabajo en Pendiente: estado=Rechazado comentario=presente",
        "[7b] Desenlace sobre estado terminal: TRANSITION_FROM_TERMINAL_STATUS",
        "[7c] Desenlace sin papel de administrador: OUTCOME_REQUIRES_ADMINISTRATOR_ROLE",
        "[8] Reseteo: estado-de-cuenta=sin-cambio trabajos-antes=4 trabajos-despues=4",
        "[9] Dependencias salientes declaradas: 0 | Bibliotecas de persistencia o transporte: 0",
        "[10] Dos corridas consecutivas sin fijar el reloj: resultado-identico=si",
        "[11] Condiciones provocadas: 12 | Devueltas por valor: 12 | Excepciones de negocio: 0",
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
