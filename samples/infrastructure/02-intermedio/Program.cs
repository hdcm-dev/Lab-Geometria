using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Values;
using GeometriaFactory.Samples.Infrastructure.Intermedio;

// ============================================================================
// Sample `infrastructure/02-intermedio` — EL ALMACÉN, contra SQLite de verdad.
//
// ES LA OTRA MITAD DE ESTA CAPA. El `01` corre el intérprete real y no abre
// ninguna base; éste no interpreta nada —las interpretaciones vienen congeladas
// del `01`— y sólo mira el almacén: qué se guarda, cómo se recupera, qué se
// retira y qué arrastra una baja.
//
// LOS CINCO ACTOS CORREN SOBRE UN ARCHIVO PROPIO Y NO SOBRE EL DE TRABAJO.
// Dos de ellos borran. El motivo largo está en `Almacen.cs`.
// ============================================================================

var archivo = Almacen.ResolverArchivo();
if (archivo is null)
{
    // La misma conducta que `CompositionRoot`: una pieza a la que le falta algo sin lo cual no
    // puede cumplir su función se niega a arrancar, para que el defecto aparezca acá y no como
    // una base creada en cualquier parte del árbol.
    Console.Error.WriteLine(
        $"El sample no arranca: falta la variable de entorno `{Almacen.LlaveDeConfiguracion}`.");
    Console.Error.WriteLine(
        "  . scripts/store-path.sh && gf_resolve_store && dotnet run --project samples/infrastructure/02-intermedio");
    return 2;
}

var lineas = new List<string>();
void Escribir(string l) { lineas.Add(l); Console.WriteLine(l); }

var excepciones = 0;
await using var contexto = await Contexto.AbrirAsync(archivo);

// EL ALTA Y LA HABILITACIÓN SON DOS ACTOS Y NO UNO (RN-16): `Register` nace `Pending` y SIN
// credencial —la rechaza si se la dan—, y la credencial la fija `Enable`. El sample sigue ese
// orden en lugar de construir la cuenta ya habilitada, porque es el único que el dominio admite.
var alumno = Account.Register("alumno@ejemplo.edu", "Nadia", "Ferrer", passwordHash: null,
    emailUniquenessVerified: true, Role.Student, AccountStatus.Pending, contexto.Reloj.UtcNow)
    .Exigir("El alta del alumno");
alumno.Enable("hash-provisorio");
await contexto.EnCuentas(r => r.AddAsync(alumno));

try
{
    // ---- Acto 1 · CU-06003, la escritura ----
    var deE1 = await ActoMaterializar.EjecutarAsync(contexto, alumno.Id, "E1", "E-1",
        Interpretaciones.DeE1(), mostrarComponentes: true, mostrarTextoOriginal: true, Escribir);
    await ActoMaterializar.EjecutarAsync(contexto, alumno.Id, "E2", "E-2",
        Interpretaciones.DeE2(), mostrarComponentes: true, mostrarTextoOriginal: false, Escribir);
    await ActoMaterializar.EjecutarAsync(contexto, alumno.Id, "E5", "E-5",
        Interpretaciones.DeE5(), mostrarComponentes: false, mostrarTextoOriginal: false, Escribir);

    // ---- Acto 2 · CU-06003, las dos formas de lectura ----
    await ActoConsultar.EjecutarAsync(contexto, alumno.Id, deE1, Escribir);

    // ---- Acto 3 · CU-06004, el retiro ----
    await ActoRetirar.EjecutarAsync(contexto, deE1, Escribir);

    // ---- Acto 4 · CU-06004, la baja con arrastre ----
    await ActoArrastrar.EjecutarAsync(contexto, Escribir);

    // ---- Acto 5 · CU-06005, las cuentas ----
    excepciones += await ActoCuentas.EjecutarAsync(contexto, Escribir);

    // ---- Acto 1, segunda mitad · RN-06008 ----
    await ActoTextoOriginal.EjecutarAsync(contexto, alumno.Id, Escribir);
}
catch (Exception error)
{
    // UNA EXCEPCIÓN NO SE TRAGA: el pie de la salida la cuenta, porque «Excepciones: 0» es parte
    // de lo que §6 afirma y un sample que la esconde estaría afirmando algo que no midió.
    excepciones++;
    Console.Error.WriteLine($"Excepción no esperada: {error.GetType().Name}: {error.Message}");
}

var rechazos = lineas.Count(l => System.Text.RegularExpressions.Regex.IsMatch(l, @"\b[A-Z]{3,}(_[A-Z]+)+\b"));
Escribir($"Actos recorridos: 5 | Rechazos tipados: {rechazos} | Excepciones: {excepciones}");

// El archivo del sample no sobrevive a la corrida: se creó para esto y ya cumplió.
await contexto.DisposeAsync();
Microsoft.Data.Sqlite.SqliteConnection.ClearAllPools();
if (File.Exists(archivo)) File.Delete(archivo);

return SalidaEsperada.Comparar(lineas) + (excepciones > 0 ? 1 : 0);
