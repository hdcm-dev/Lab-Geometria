// ============================================================================
// barrido-cobertura-de-contrato.cs — Encuentra las condiciones que el dominio
// emite y que la capa API no nombra en ningún lado.
//
// POR QUÉ EXISTE. Hasta el 2026-08-31 este producto había encontrado SEIS ramas
// defensivas inalcanzables, **cada una por su lado y ninguna buscada**: aparecían
// tropezando con ellas al arreglar otra cosa. La mesa del 2026-08-31 lo levantó
// como `M-06` —no hay medición de esta clase— y este barrido es la medición.
//
// LA PRIMERA CORRIDA ENCONTRÓ QUINCE donde el reporte hablaba de dos.
//
// QUÉ MIDE, EXACTAMENTE. Una condición está **cubierta** si algún archivo de
// `GeometriaFactory.Api` la nombra: el conmutador de `ContractTranslation` o un
// punto de acceso que la traduzca por su cuenta. La primera versión de este
// barrido miraba **sólo el conmutador** y dio falsos positivos, porque tres
// condiciones se traducen en el punto. Se corrigió mirando la capa entera.
//
// QUÉ NO MIDE, Y ES DELIBERADO: **no pregunta si la condición es alcanzable**.
// Esa pregunta se contesta hoy leyendo los invocadores y deja de valer mañana,
// cuando alguien agregue uno. La pregunta que este producto eligió es **de quién
// sería el defecto si se alcanzara**, y la contesta una persona, no un guion.
//
// LA RED PERMANENTE NO ES ESTE ARCHIVO: es `ContractCoverageTests`, que exige que
// el catálogo esté particionado en cuatro listas declaradas. Este barrido sirve
// para la primera clasificación y para revisarla; la prueba es la que impide que
// una condición nueva caiga al genérico por omisión.
//
//   dotnet run tools/barrido-cobertura-de-contrato.cs
// ============================================================================
using System.Text.RegularExpressions;

var raiz = Directory.GetCurrentDirectory();
string Leer(string patron, string sub) => string.Join('\n',
    Directory.GetFiles(Path.Combine(raiz, sub), patron, SearchOption.AllDirectories)
        .Where(f => !f.Contains("/obj/") && !f.Contains("/bin/"))
        .Select(File.ReadAllText));

var todo = Leer("*.cs", "src");
var api = Leer("*.cs", Path.Combine("src", "GeometriaFactory.Api"));

// El catálogo del dominio: nombre de la constante y su valor.
var catalogo = Regex.Matches(
        File.ReadAllText(Path.Combine(raiz, "src/GeometriaFactory.Domain/Values/ConditionCode.cs")),
        @"public const string (\w+)\s*=\s*""([A-Z_]+)""")
    .Select(m => (Nombre: m.Groups[1].Value, Valor: m.Groups[2].Value))
    .ToArray();

var nombradas = Regex.Matches(api, @"(?:Application|Infrastructure)?ConditionCode\.(\w+)")
    .Select(m => m.Groups[1].Value).ToHashSet(StringComparer.Ordinal);

var huerfanas = new List<string>();
var sinUso = new List<string>();

foreach (var (nombre, valor) in catalogo.OrderBy(c => c.Valor, StringComparer.Ordinal))
{
    // «EMITIDA» ES CUALQUIER USO FUERA DE SU DECLARACIÓN, y no sólo `Rejected(...)`.
    // La primera versión buscaba `Rejected(` y dio tres falsos positivos: `ACCOUNT_BLOCKED`,
    // `ACCOUNT_PENDING` y `PASSWORD_CHANGE_PENDING` se emiten por `Admission.NotAdmissible(...)`,
    // que es otra puerta. Buscar la forma de la llamada mide **cómo se emite** y no **si se emite**.
    var usos = Regex.Matches(todo, @"(?<![\w.])(?:\w+ConditionCode|ConditionCode)\." + nombre + @"\b").Count;
    var emitida = usos > 0;
    if (!emitida) { sinUso.Add(valor); continue; }
    if (!nombradas.Contains(nombre)) huerfanas.Add(valor);
}

Console.WriteLine($"Condiciones en el catálogo del dominio: {catalogo.Length}");
Console.WriteLine($"Nombradas por la capa API:              {catalogo.Count(c => nombradas.Contains(c.Nombre))}");
Console.WriteLine();
Console.WriteLine($"DECLARADAS Y QUE NINGUNA OPERACIÓN EMITE: {sinUso.Count}");
foreach (var v in sinUso) Console.WriteLine($"  {v}");
Console.WriteLine();
Console.WriteLine($"EMITIDAS Y QUE LA CAPA API NO NOMBRA: {huerfanas.Count}");
foreach (var v in huerfanas) Console.WriteLine($"  {v}");
Console.WriteLine();
Console.WriteLine("Cada una de las de abajo necesita una decisión: si se alcanzara,");
Console.WriteLine("¿el defecto sería del PRODUCTO —y `500` es correcto— o del PEDIDO?");
return huerfanas.Count == 0 && sinUso.Count == 0 ? 0 : 1;
