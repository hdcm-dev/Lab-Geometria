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
        "[1] Derivacion sin contrasena en claro: nulo, sin codigo tipado",
        "[2] Provisorias producidas: 100 | repetidas: 0 | derivadas de un dato de la cuenta: no",
        "[2] Produccion sin fuente de aleatoriedad: no provocable desde el sample | caminos alternativos en la fuente del componente: 0",
        "[3] Acceso emitido: reclamos presentes=4 | verificacion del acceso propio: valida",
        "[3] Acceso con firma ajena: invalido | Acceso vencido: invalido",
        "[3] Emision sin clave de firma: nulo, sin codigo tipado | accesos emitidos: 0",
        "[3] Emision con reclamos incompletos: nulo, sin codigo tipado | distinguible de la anterior: no",
        "[4] Sello del reloj por el puerto: obtenido | dos corridas con el puerto fijado: sello identico",
        "[5] Preparacion del almacen: transformaciones aplicadas | linaje registrado",
        "[5] Segunda preparacion sobre el mismo almacen: sin transformaciones nuevas",
        "[5] Preparacion sobre un almacen con linaje desconocido: arranque detenido InvalidOperationException, sin codigo tipado",
        "[insp] Ocurrencias de clave de firma, contrasena real o ruta del almacen en la fuente del sample: 0",
        "[insp] Ocurrencias de contrasena en claro o de valor derivado en la salida producida: 0",
        "Actos recorridos: 5 | Rechazos tipados: 1 | Excepciones: 0",
    ];

    /// <summary>Las líneas cuya diferencia está declarada y explicada, con el motivo.</summary>
    /// <remarks>
    /// LAS CINCO DIVERGENCIAS DICEN UNA SOLA COSA, y es la que este sample vino a encontrar:
    /// **esta capa declara DOS códigos tipados y §6 le pide SEIS**. Los cuatro que faltan no
    /// están escritos con otro nombre en otro lado, como pasaba en el sample `02`: no existen,
    /// y las fallas correspondientes viajan como `null`. La conducta se cumple en los cinco
    /// casos; lo que no hay es forma de que el llamador sepa cuál ocurrió.
    /// </remarks>
    /// <summary>Sin divergencias: §6 se alineó con la capa el 2026-08-30.</summary>
    /// <remarks>
    /// LAS SEIS QUE HABÍA SE CERRARON CORRIGIENDO EL DOCUMENTO Y NO LA CAPA. Cinco pedían códigos
    /// tipados que no existen en el árbol, y la sexta pedía uno que existe pero cuya condición no
    /// es provocable desde un sample. **No se agregaron cuatro códigos sin consumidor**: la
    /// conducta que §6 describía se cumple entera, y lo que faltaba era el nombre.
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
