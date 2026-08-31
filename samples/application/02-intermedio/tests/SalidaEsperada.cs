namespace GeometriaFactory.Samples.Application.Intermedio;

/// <summary>El snapshot de §6, transcripto sin cambios, y su comparación línea por línea.</summary>
internal static class SalidaEsperada
{
    private static readonly string[] Snapshot =
    [
        "[E-1] Cargado: texto-identico=si estado=Borrador | Envio: 3 piezas, 2 advertencias, 0 errores -> Pendiente",
        "[E-2] Envio: 1 pieza, 1 advertencia de volumen, 0 errores -> Pendiente",
        "[E-3] Envio: advertencia de area declarado=36.00 derivado=54.00 -> Pendiente",
        "[E-4] Envio: 0 observaciones -> Pendiente (mismo cubo de lado 3, area declarada coincidente)",
        "[E-5] Envio: observacion Error indice-figura=1 campo=Tipo -> Borrador (RN-04005)",
        "[E-6] Envio: la figura se interpreta y no se descarta -> Pendiente",
        "[E-7] Detalle: 6 piezas con componentes | Listado: 6 piezas sin componentes",
        "[E-8] Envio: observacion Error indice-figura=1 campo=Largo -> Borrador (RN-04005)",
        "[Consulta] Listado propio: 8 trabajos | Pendiente=6 Borrador=2 Aprobado=0 Rechazado=0",
        "[Retiro] Trabajo en Borrador por su dueno: retirado",
        "[Retiro] Trabajo en Pendiente por su dueno: rechazado OPERATION_OUTSIDE_DRAFT",
        "[Retiro] Trabajo ajeno: rechazado WORK_NOT_FOUND_FOR_REQUESTER",
        "[Reedicion] Trabajo fuera de Borrador: rechazado OPERATION_OUTSIDE_DRAFT | texto-original-intacto=si",
        "Escenarios recorridos: 8 | Envios a Pendiente: 6 | Retenidos en Borrador: 2 | Excepciones: 0",
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
