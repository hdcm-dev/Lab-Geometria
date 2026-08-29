// ============================================================================
// informe-cobertura.cs — El informe de `TC-00037`, que es el instrumento con el
// que se miden `QG-03` y `QG-04`. Lo invoca `scripts/coverage.sh`.
//
// POR QUÉ ES UN ARCHIVO SUELTO DE C# Y NO UN GUION EN OTRO LENGUAJE. Leer XML y
// sumar contadores es trabajo de un programa, no de una tubería de texto; y
// hacerlo en Python o con una herramienta de informes habría metido una
// dependencia nueva en un repositorio cuyo intake declara que **ninguna versión
// de paquete se resuelve sola**. Con `dotnet run informe-cobertura.cs` el
// instrumento no agrega NADA al anclaje: corre con el mismo kit que compila el
// producto.
//
// POR QUÉ HAY QUE SUMAR Y NO SE PUEDEN LEER LAS TASAS. Cobertura escribe
// `line-rate` por paquete, pero la corrida deja **un archivo por proyecto de
// prueba** y el mismo ensamblado aparece en varios. Promediar tasas de
// muestras de distinto tamaño da un número que no es la cobertura de nada: hay
// que sumar líneas cubiertas y líneas válidas, y recién ahí dividir.
// ============================================================================
using System.Globalization;
using System.Xml.Linq;

// Umbrales de QG-03, transcriptos del intake §22 (asunción `A-3`) y confirmados
// por el Product Owner el 2026-08-26, en
// `SDD/Docs/Audit/D1-Confirmacion-De-Asunciones.md` §3.2.
// `Contracts` y `Web` NO llevan umbral de líneas: sus gates son de otra forma
// —DTOs ejercitados y pasos de guion— y este informe no los mide.
var umbral = new Dictionary<string, (int Lineas, int Ramas)>
{
    ["GeometriaFactory.Domain"]         = (90, 85),
    ["GeometriaFactory.Application"]    = (85, 80),
    ["GeometriaFactory.Infrastructure"] = (85, 80),
    ["GeometriaFactory.Api"]            = (75, 70),
};
const int PiramideIntegracion = 60; // QG-04

var raiz = Path.Combine(AppContext.BaseDirectory);
var resultados = args.Length > 0 ? args[0] : "TestResults";
if (!Directory.Exists(resultados))
{
    Console.WriteLine($"NO SE PUEDE MEDIR · no existe {resultados}");
    return 2;
}

// EL MISMO INFORME APARECE DOS VECES Y HAY QUE DESCARTAR LA COPIA, o todos los
// contadores salen al doble. Con `--logger trx`, VSTest deja el adjunto en su
// carpeta de la corrida Y lo copia bajo `<informe>/In/<host>/`. Se deduplica por
// el contenido, que es lo único que distingue una copia de un informe distinto:
// los nombres de archivo son todos iguales y las carpetas, todas legítimas.
var vistos = new HashSet<string>(StringComparer.Ordinal);
var cobertura = Directory.GetFiles(resultados, "coverage.cobertura.xml", SearchOption.AllDirectories)
    .Where(f => vistos.Add(Convert.ToHexString(
        System.Security.Cryptography.SHA256.HashData(File.ReadAllBytes(f)))))
    .ToArray();
var informes = Directory.GetFiles(resultados, "*.trx", SearchOption.AllDirectories);
if (cobertura.Length == 0)
{
    Console.WriteLine("NO SE PUEDE MEDIR · el recolector no dejó ningún informe de cobertura");
    return 2;
}

var acum = new Dictionary<string, long[]>(); // [lineas cubiertas, válidas, ramas cubiertas, válidas]
foreach (var archivo in cobertura)
{
    foreach (var paquete in XDocument.Load(archivo).Descendants("package"))
    {
        var nombre = (string?)paquete.Attribute("name") ?? "(sin nombre)";
        if (!acum.TryGetValue(nombre, out var a)) acum[nombre] = a = new long[4];
        foreach (var linea in paquete.Descendants("line"))
        {
            a[1]++;
            if (int.Parse((string?)linea.Attribute("hits") ?? "0", CultureInfo.InvariantCulture) > 0) a[0]++;
            var cc = (string?)linea.Attribute("condition-coverage");
            if (cc is null || !cc.Contains('(')) continue;
            var partes = cc[(cc.IndexOf('(') + 1)..].TrimEnd(')').Split('/');
            a[2] += long.Parse(partes[0], CultureInfo.InvariantCulture);
            a[3] += long.Parse(partes[1], CultureInfo.InvariantCulture);
        }
    }
}

var fallos = new List<string>();
Console.WriteLine();
Console.WriteLine("QG-03 · cobertura por proyecto de código");
Console.WriteLine($"  {"proyecto",-34}{"líneas",19}{"ramas",19}   veredicto");
foreach (var nombre in acum.Keys.OrderBy(x => x, StringComparer.Ordinal))
{
    var a = acum[nombre];
    var li = a[1] == 0 ? 0d : 100d * a[0] / a[1];
    var br = a[3] == 0 ? 0d : 100d * a[2] / a[3];
    string veredicto;
    if (umbral.TryGetValue(nombre, out var u))
    {
        var pasa = li >= u.Lineas && br >= u.Ramas;
        if (!pasa) fallos.Add($"QG-03 · {nombre}");
        veredicto = pasa ? "PASA" : $"NO PASA · pide {u.Lineas}/{u.Ramas}";
    }
    else veredicto = "sin umbral de líneas en la fuente";
    Console.WriteLine($"  {nombre,-34}{a[0],6}/{a[1],-6}{li,6:F1}%{a[2],6}/{a[3],-6}{br,6:F1}%   {veredicto}");
}

// QG-04 — el reparto se cuenta del informe de la corrida, no de los atributos
// del código fuente: `[Theory]` con datos en línea es un método y varios casos.
XNamespace ns = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010";
var porEnsamblado = new Dictionary<string, int>(StringComparer.Ordinal);
foreach (var archivo in informes)
{
    var doc = XDocument.Load(archivo);
    var definiciones = doc.Descendants(ns + "UnitTest")
        .ToDictionary(u => (string?)u.Attribute("id") ?? "", u => (string?)u.Attribute("storage") ?? "");
    foreach (var prueba in doc.Descendants(ns + "UnitTestResult"))
    {
        if (!definiciones.TryGetValue((string?)prueba.Attribute("testId") ?? "", out var ruta)) continue;
        var nombre = Path.GetFileNameWithoutExtension(ruta);
        if (nombre.Length == 0) continue;
        porEnsamblado[nombre] = porEnsamblado.GetValueOrDefault(nombre) + 1;
    }
}

Console.WriteLine();
Console.WriteLine($"QG-04 · pirámide invertida — pide {PiramideIntegracion} % de integración");
foreach (var kv in porEnsamblado.OrderBy(x => x.Key, StringComparer.Ordinal))
    Console.WriteLine($"  {kv.Key,-34}{kv.Value,6} casos");

// SIN IGNORAR MAYÚSCULAS ACÁ EL REPARTO DA CERO: el informe de la corrida
// escribe la ruta del ensamblado tal como la resolvió, y sale en minúsculas.
var integracion = porEnsamblado.Where(x => x.Key.Contains("Integration", StringComparison.OrdinalIgnoreCase)).Sum(x => x.Value);
var unitarias = porEnsamblado.Where(x => !x.Key.Contains("Integration", StringComparison.OrdinalIgnoreCase)).Sum(x => x.Value);
var total = integracion + unitarias;
if (total == 0)
{
    Console.WriteLine("  NO SE PUEDE MEDIR · el informe de la corrida no trae casos");
    return 2;
}
var pct = 100d * integracion / total;
var piramideOk = pct >= PiramideIntegracion;
if (!piramideOk) fallos.Add("QG-04 · pirámide");
Console.WriteLine();
Console.WriteLine($"  integración {integracion} · unitarias {unitarias} · total {total}"
                  + $"  →  {pct:F1} % de integración   {(piramideOk ? "PASA" : "NO PASA")}");

Console.WriteLine();
if (fallos.Count > 0)
{
    Console.WriteLine("NO CONFORME · " + string.Join(", ", fallos));
    return 1;
}
Console.WriteLine("CONFORME · QG-03 y QG-04 pasan");
return 0;
