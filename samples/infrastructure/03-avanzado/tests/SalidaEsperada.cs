namespace GeometriaFactory.Samples.Infrastructure.Avanzado;

/// <summary>El snapshot de §6, TRANSCRIPTO SIN CAMBIOS, y su comparación línea por línea.</summary>
internal static class SalidaEsperada
{
    private static readonly string[] Snapshot =
    [
        "[1] Derivacion de contrasena: valor derivado producido | contrasena en claro guardada: no",
        "[1] Verificacion con la credencial correcta: verdadera",
        "[1] Verificacion con la credencial incorrecta: falsa",
        "[1] Verificacion contra un derivado ilegible: UNREADABLE_PASSWORD_HASH (distinto de falsa)",
        "[1] Derivacion sin contrasena en claro: rechazada PLAINTEXT_PASSWORD_MISSING",
        "[2] Provisorias producidas: 100 | repetidas: 0 | derivadas de un dato de la cuenta: no",
        "[2] Produccion sin fuente de aleatoriedad: RANDOMNESS_SOURCE_UNAVAILABLE | valores producidos: 0",
        "[3] Acceso emitido: reclamos presentes=4 | verificacion del acceso propio: valida",
        "[3] Acceso con firma ajena: invalido | Acceso vencido: invalido",
        "[3] Emision sin clave de firma: rechazada SIGNING_KEY_MISSING | accesos emitidos: 0",
        "[3] Emision con reclamos incompletos: rechazada INCOMPLETE_CLAIMS",
        "[4] Sello del reloj por el puerto: obtenido | dos corridas con el puerto fijado: sello identico",
        "[5] Preparacion del almacen: transformaciones aplicadas | linaje registrado",
        "[5] Segunda preparacion sobre el mismo almacen: sin transformaciones nuevas",
        "[5] Preparacion sobre un almacen con linaje desconocido: arranque detenido MIGRATION_NOT_APPLICABLE",
        "[insp] Ocurrencias de clave de firma, contrasena real o ruta del almacen en la fuente del sample: 0",
        "[insp] Ocurrencias de contrasena en claro o de valor derivado en la salida producida: 0",
        "Actos recorridos: 5 | Rechazos tipados: 6 | Excepciones: 0",
    ];

    /// <summary>Las líneas cuya diferencia está declarada y explicada, con el motivo.</summary>
    /// <remarks>
    /// LAS CINCO DIVERGENCIAS DICEN UNA SOLA COSA, y es la que este sample vino a encontrar:
    /// **esta capa declara DOS códigos tipados y §6 le pide SEIS**. Los cuatro que faltan no
    /// están escritos con otro nombre en otro lado, como pasaba en el sample `02`: no existen,
    /// y las fallas correspondientes viajan como `null`. La conducta se cumple en los cinco
    /// casos; lo que no hay es forma de que el llamador sepa cuál ocurrió.
    /// </remarks>
    private static readonly Dictionary<int, string> Divergencias = new()
    {
        [5] = "D-1 · `PLAINTEXT_PASSWORD_MISSING` no existe: `Derive` devuelve nulo y no dice por qué",
        [7] = "D-2 · el código existe pero la condición NO ES PROVOCABLE desde el sample; se mide en su lugar que no haya segundo camino",
        [10] = "D-3 · `SIGNING_KEY_MISSING` no existe: `Issue` devuelve nulo",
        [11] = "D-4 · `INCOMPLETE_CLAIMS` no existe, y es EL MISMO nulo que D-3: las dos fallas son indistinguibles",
        [15] = "D-5 · `MIGRATION_NOT_APPLICABLE` no existe: el arranque se detiene, pero con la excepción del proveedor",
        [18] = "consecuencia de D-1 a D-5: de los seis rechazos que §6 cuenta, el sample sólo puede exhibir UNO — `UNREADABLE_PASSWORD_HASH`. El otro código que la capa declara no es provocable, y los cuatro restantes no existen",
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
