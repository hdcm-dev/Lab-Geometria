namespace GeometriaFactory.Samples.Application.Basico;

/// <summary>El snapshot de §6, transcripto sin cambios, y su comparación línea por línea.</summary>
internal static class SalidaEsperada
{
    private static readonly string[] Snapshot =
    [
        "[1] Alta de alumno: constituida situacion=Pendiente credencial=ausente",
        "[1] Alta repetida con el mismo correo: rechazada EMAIL_ALREADY_REGISTERED",
        "[2] Alta de administrador: constituida situacion=Habilitada papel=Administrador",
        "[2] Segundo administrador: rechazado ADMINISTRATOR_ALREADY_CONFIGURED",
        "[3] Admisibilidad de la cuenta pendiente: no admisible motivo=ACCOUNT_PENDING",
        "[3] Admisibilidad de la cuenta habilitada con marca: no admisible motivo=PASSWORD_CHANGE_PENDING",
        "[3] Admisibilidad de la cuenta habilitada sin marca: admisible",
        "[4] Cuenta marcada pide listar sus trabajos: PROCEDIÓ — la capa de aplicación no comprueba la marca",
        "[4] Cuenta marcada reemplaza su credencial: aceptado (unica excepcion de ADR-04004)",
        "[4] Marca levantada por la propia cuenta: la misma peticion de listado ahora procede",
        "[4] Reemplazo sin presentar la vigente: rechazado CURRENT_CREDENTIAL_NOT_VERIFIED",
        "Actos recorridos: 4 | Rechazos tipados: 5 | Excepciones: 0",
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
