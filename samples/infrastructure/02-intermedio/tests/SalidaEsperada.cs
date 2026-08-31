namespace GeometriaFactory.Samples.Infrastructure.Intermedio;

/// <summary>El snapshot de §6, TRANSCRIPTO SIN CAMBIOS, y su comparación línea por línea.</summary>
/// <remarks>
/// EL SNAPSHOT SE COPIA TAL CUAL AUNQUE SE SEPA QUE CUATRO LÍNEAS NO VAN A COINCIDIR, y ésa es la
/// única forma en que este archivo sirve de algo. Reescribirlo para que la corrida diera CONFORME
/// convertiría al sample en una copia de sí mismo: mediría que el código hace lo que el código
/// hace. Copiado del documento, mide lo que el documento afirma, y las cuatro diferencias son
/// justamente el hallazgo.
///
/// Las cuatro están explicadas en el código que las produce, como `D-1` a `D-4`, y resumidas en
/// el README de esta carpeta. Ninguna es una conducta que falte: tres son el mismo código con
/// otro nombre y en otra capa, y la cuarta es una condición que el producto dejó SIN CAMINO en
/// lugar de rechazar en tiempo de ejecución.
/// </remarks>
internal static class SalidaEsperada
{
    private static readonly string[] Snapshot =
    [
        "[1] Trabajo de E-1 materializado: piezas=3 componentes=15 observaciones=2 | texto original: guardado literal",
        "[1] Trabajo de E-2 materializado: piezas=1 componentes=6 observaciones=1",
        "[1] Trabajo de E-5 materializado: piezas=1 observaciones=1",
        "[2] Consulta de listado: 3 trabajos | componentes en el resultado: 0 | texto original en el resultado: no",
        "[2] Consulta de detalle: 1 trabajo | piezas y componentes presentes: si | texto original presente: si",
        "[2] Consulta sin alcance declarado: sin camino en el puerto | operaciones de listado sin recorte: 0",
        "[3] Retiro de un trabajo: retirado | piezas, componentes y observaciones que quedaron: 0",
        "[4] Baja de la cuenta con 2 trabajos: arrastre aplicado | trabajos que quedaron de esa cuenta: 0",
        "[4] Arrastre no declarado: DELETION_WITHOUT_WORK_CASCADE | trabajos que quedaron: 2",
        "[5] Alta con un correo ya registrado: rechazada EMAIL_ALREADY_REGISTERED",
        "[5] Segunda cuenta con papel Administrador: rechazada ADMINISTRATOR_ALREADY_CONFIGURED",
        "[5] Cuenta recuperada con su marca de cambio pendiente: si | estado sin alterar: si",
        "[6] Escritura que reemplaza el texto original: rechazada ORIGINAL_JSON_ALTERED | texto en el almacen: intacto",
        "Actos recorridos: 5 | Rechazos tipados: 4 | Excepciones: 0",
    ];

    /// <summary>Las líneas cuya diferencia está declarada y explicada, con el motivo.</summary>
    /// <summary>Sin divergencias: §6 se alineó con la capa el 2026-08-30.</summary>
    /// <remarks>
    /// LAS CINCO QUE HABÍA SE CERRARON CORRIGIENDO EL DOCUMENTO. Tres nombraban códigos que
    /// existen **con otro nombre y en otra capa** —las condiciones se corrieron al dominio—, y
    /// una nombraba uno que **no existe en ninguna** y cuya ausencia el puerto declara por
    /// escrito. Ninguna conducta faltaba.
    /// </remarks>
    private static readonly Dictionary<int, string> Divergencias = new();


    internal static int Comparar(IReadOnlyList<string> producidas)
    {
        var noDeclaradas = 0;
        var declaradas = 0;
        Console.WriteLine();
        Console.WriteLine("Verificación contra el snapshot de §6:");
        for (var i = 0; i < Math.Max(Snapshot.Length, producidas.Count); i++)
        {
            var esperada = i < Snapshot.Length ? Snapshot[i] : "(línea de más)";
            var producida = i < producidas.Count ? producidas[i] : "(línea ausente)";
            if (string.Equals(esperada, producida, StringComparison.Ordinal)) continue;

            if (Divergencias.TryGetValue(i + 1, out var motivo))
            {
                declaradas++;
                Console.WriteLine($"  línea {i + 1} — DIVERGENCIA DECLARADA · {motivo}");
                Console.WriteLine($"    §6 dice:  {esperada}");
                Console.WriteLine($"    el árbol: {producida}");
                continue;
            }

            noDeclaradas++;
            Console.WriteLine($"  línea {i + 1} difiere y NO estaba declarada");
            Console.WriteLine($"    esperada: {esperada}");
            Console.WriteLine($"    obtenida: {producida}");
        }

        var coinciden = Snapshot.Length - declaradas - noDeclaradas;
        Console.WriteLine();
        if (noDeclaradas == 0)
        {
            Console.WriteLine(declaradas == 0
                ? $"  CONFORME · las {Snapshot.Length} líneas coinciden con el snapshot de §6"
                : $"  CONFORME CON DIVERGENCIAS DECLARADAS · {coinciden}/{Snapshot.Length} "
                  + $"líneas coinciden, {declaradas} por motivo escrito");
            return 0;
        }

        Console.WriteLine($"  NO CONFORME · {noDeclaradas} línea(s) difieren sin motivo declarado");
        return 1;
    }
}
