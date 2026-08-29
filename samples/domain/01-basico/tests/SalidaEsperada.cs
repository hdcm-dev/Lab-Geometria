namespace GeometriaFactory.Samples.Domain.Basico;

/// <summary>
/// El snapshot de §6 del documento que gobierna esta carpeta, transcripto SIN cambios, y la
/// comparación línea por línea que lo hace verificable.
/// </summary>
/// <remarks>
/// LOS CÓDIGOS DE CONDICIÓN VAN EN INGLÉS, y no es una elección de este archivo: es la decisión
/// <c>F-03</c> del Product Owner del 2026-08-12, registrada en `Producto/Norma-De-Nomenclatura.md`
/// §5.3 —«los 101 van a inglés: los 80 internos y los 21 de contrato»—, **reconfirmada el
/// 2026-08-29** cuando este mismo sample destapó que el documento que lo gobierna todavía los
/// nombraba en castellano. El código nunca estuvo mal; el renombre documental de `F-03` quedó
/// suspendido el 2026-08-13 y ésta es la parte que le tocaba a este documento.
///
/// POR QUÉ EL SNAPSHOT VIVE ACÁ Y NO SE LEE DEL MARKDOWN. Leerlo del documento haría que el
/// sample pasara siempre: cualquier cambio en la salida se propagaría solo al criterio que
/// debería atraparlo. Transcripto acá, **los dos tienen que moverse a mano** y la diferencia
/// se ve en el diff, que es exactamente lo que se quiere de un snapshot.
/// </remarks>
internal static class SalidaEsperada
{
    private static readonly string[] Snapshot =
    [
        "[1] Administrador configurado: papel=Administrador estado=Habilitado credencial=fijada",
        "[1b] Segundo administrador rechazado: ADMINISTRATOR_ALREADY_CONFIGURED",
        "[2] Alumno constituido: papel=Alumno estado=Pendiente credencial=sin-valor",
        "[2b] Alta sin correo rechazada: REQUIRED_FIELD_MISSING",
        "[3] Admisibilidad de la cuenta Pendiente: no-admisible motivos=ACCOUNT_PENDING",
        "[4] Cuenta habilitada: estado=Habilitado credencial=fijada cambio-pendiente=puesta",
        "[5] Admisibilidad tras habilitar: no-admisible motivos=PASSWORD_CHANGE_PENDING",
        "[6] Credencial reemplazada por la propia cuenta: cambio-pendiente=levantada",
        "[7] Admisibilidad final: admisible motivos=0",
        "Operaciones invocadas: 9 | Rechazos tipados: 2 | Excepciones: 0",
    ];

    internal static int Comparar(IReadOnlyList<string> producidas)
    {
        var diferencias = 0;
        Console.WriteLine();
        Console.WriteLine("Verificación contra el snapshot de §6:");

        for (var i = 0; i < Math.Max(Snapshot.Length, producidas.Count); i++)
        {
            var esperada = i < Snapshot.Length ? Snapshot[i] : "(el sample produjo una línea de más)";
            var producida = i < producidas.Count ? producidas[i] : "(el sample no produjo esta línea)";
            if (string.Equals(esperada, producida, StringComparison.Ordinal))
            {
                continue;
            }

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
