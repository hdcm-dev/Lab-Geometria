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
        "[2] Consulta sin alcance declarado: rechazada QUERY_WITHOUT_DECLARED_SCOPE",
        "[3] Retiro de un trabajo: retirado | piezas, componentes y observaciones que quedaron: 0",
        "[4] Baja de la cuenta con 2 trabajos: arrastre aplicado | trabajos que quedaron de esa cuenta: 0",
        "[4] Arrastre interrumpido a la mitad: PARTIAL_DELETION_NOT_ALLOWED | trabajos que quedaron: 2",
        "[5] Alta con un correo ya registrado: rechazada EMAIL_ALREADY_REGISTERED",
        "[5] Segunda cuenta con papel Administrador: rechazada ADMINISTRATOR_UNIQUENESS_VIOLATED",
        "[5] Cuenta recuperada con su marca de cambio pendiente: si | estado sin alterar: si",
        "[6] Escritura que reemplaza el texto original: rechazada WRITE_REWRITES_ORIGINAL_JSON",
        "Actos recorridos: 5 | Rechazos tipados: 5 | Excepciones: 0",
    ];

    /// <summary>Las líneas cuya diferencia está declarada y explicada, con el motivo.</summary>
    private static readonly Dictionary<int, string> Divergencias = new()
    {
        [6] = "D-1 · `QUERY_WITHOUT_DECLARED_SCOPE` no existe: el puerto no declara ninguna operación que la produzca",
        [9] = "D-2 · el código es `DELETION_WITHOUT_WORK_CASCADE` y vive en el dominio",
        [11] = "D-3 · el código es `ADMINISTRATOR_ALREADY_CONFIGURED` y vive en el dominio",
        [13] = "D-4 · el código es `ORIGINAL_JSON_ALTERED` y vive en el dominio",
        [14] = "consecuencia de D-1: los rechazos tipados alcanzables son cuatro y no cinco",
    };

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
            Console.WriteLine($"  CONFORME CON DIVERGENCIAS DECLARADAS · {coinciden}/{Snapshot.Length} "
                + $"líneas coinciden, {declaradas} difieren por motivo escrito");
            return 0;
        }

        Console.WriteLine($"  NO CONFORME · {noDeclaradas} línea(s) difieren sin motivo declarado");
        return 1;
    }
}
