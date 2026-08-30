#:package Microsoft.Data.Sqlite@10.0.11
// ============================================================================
// almacen.cs — Lee el linaje de un almacén, y compone el de linaje desconocido.
//
// POR QUÉ UN ARCHIVO SUELTO DE C# Y NO UNA TUBERÍA DE TEXTO. En el entorno
// contenido no hay `sqlite3` ni `python3`, y agregarlos movería el anclaje de
// versión de la etapa `a` por comodidad de un sample. El repositorio ya resolvió
// esto antes de la misma forma: `tools/informe-cobertura.cs` es un archivo
// suelto por el mismo motivo.
//
// LO QUE NO HACE: no toca el almacén de trabajo. Recibe la ruta por argumento,
// y quien lo invoca es `run.sh`, que trabaja sobre un directorio temporal suyo.
// ============================================================================
using Microsoft.Data.Sqlite;

var ruta = args.Length > 0 ? args[0] : "";
if (string.IsNullOrWhiteSpace(ruta))
{
    Console.Error.WriteLine("Uso: dotnet run almacen.cs -- <ruta> [--componer-roto]");
    return 2;
}

using var conexion = new SqliteConnection($"Data Source={ruta}");
conexion.Open();

if (args.Contains("--componer-roto"))
{
    // UNA TABLA QUE SE PARECE A LA DEL PRODUCTO Y NINGÚN REGISTRO DE LINAJE.
    // El arranque cree que el almacén está vacío, trata de aplicar la primera
    // transformación, y la transformación crea `Account`, que ya existe.
    //
    // Es la forma en que el defecto aparece de verdad: un almacén que alguien
    // tocó por fuera tiene estructura verosímil y no dice de qué versión viene.
    // Un archivo corrupto se detecta solo y nadie duda de qué pasó.
    using var orden = conexion.CreateCommand();
    orden.CommandText = "CREATE TABLE IF NOT EXISTS Account (Id TEXT NOT NULL PRIMARY KEY);";
    orden.ExecuteNonQuery();
    return 0;
}

// Cuántas transformaciones registró el almacén. Si la tabla de linaje no existe,
// la respuesta es CERO y no un error: un almacén sin linaje es exactamente el
// caso que el acto 3 provoca, y ahí también hay que poder preguntar.
using var consulta = conexion.CreateCommand();
consulta.CommandText =
    "SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='__EFMigrationsHistory';";
if (Convert.ToInt32(consulta.ExecuteScalar(), System.Globalization.CultureInfo.InvariantCulture) == 0)
{
    Console.WriteLine(0);
    return 0;
}

consulta.CommandText = "SELECT COUNT(*) FROM __EFMigrationsHistory;";
Console.WriteLine(Convert.ToInt32(consulta.ExecuteScalar(), System.Globalization.CultureInfo.InvariantCulture));
return 0;
