namespace GeometriaFactory.Samples.Application.Avanzado;

/// <summary>El snapshot de §6, transcripto sin cambios, y su comparación línea por línea.</summary>
internal static class SalidaEsperada
{
    private static readonly string[] Snapshot =
    [
        "[1] Habilitar cuenta pendiente: habilitada | provisoria producida por el sistema: si",
        "[1] Bloquear cuenta habilitada: bloqueada | Rehabilitar: habilitada + provisoria nueva",
        "[1] Transicion no admitida sobre una cuenta habilitada: rechazada ACCOUNT_TRANSITION_NOT_ALLOWED",
        "[1] Baja con el correo escrito distinto: rechazada DELETION_CONFIRMATION_MISMATCH",
        "[1] Baja con el correo escrito coincidente: dada de baja | trabajos arrastrados: 2",
        "[1] Baja de la cuenta de administrador: rechazada OPERATION_NOT_APPLICABLE_TO_ADMINISTRATOR_ACCOUNT",
        "[2] Listado de la comision: 3 trabajos | borradores visibles: 0 (RN-04011)",
        "[2] Detalle de un trabajo en Borrador pedido por el administrador: WORK_OUTSIDE_ADMINISTRATOR_SCOPE",
        "[2] Listado de la comision pedido por un alumno: rechazado SCOPE_REQUIRES_ADMINISTRATOR_ROLE",
        "[3] Aprobar desde Pendiente con comentario: Aprobado",
        "[3] Rechazar desde Pendiente sin comentario: Rechazado (el comentario es opcional)",
        "[3] Desenlace sobre un trabajo ya Aprobado: rechazado TRANSITION_FROM_TERMINAL_STATUS",
        "[3] Desenlace pedido por un alumno: rechazado OUTCOME_REQUIRES_ADMINISTRATOR_ROLE",
        "[4] Reseteo de un alumno bloqueado: aplicado | situacion conservada=Bloqueada trabajos conservados=2",
        "[4] Reseteo sobre la cuenta de administrador: rechazado RESET_LIMITED_TO_STUDENT_ACCOUNTS",
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
