using GeometriaFactory.Samples.Infrastructure.Avanzado;

// ============================================================================
// Sample `infrastructure/03-avanzado` — LO QUE ESTA CAPA SABE Y NO PUEDE CONTAR.
//
// Los cuatro componentes que conocen un secreto —la derivación de credenciales,
// la fábrica de provisorias, el emisor de accesos y la preparación del almacén—
// más el reloj, que es el que hace reproducible a todo lo demás.
//
// LAS DOS INSPECCIONES DE UMBRAL CERO NO SON ADORNO. Esta es la capa que conoce
// el valor derivado, la clave de firma y la ruta del almacén; que no aparezcan
// ni en su fuente ni en su traza es la única forma de que la regla de exposición
// del contrato del producto siga siendo cierta.
// ============================================================================

var directorio = Almacen.ResolverDirectorio();
if (directorio is null)
{
    Console.Error.WriteLine(
        $"El sample no arranca: falta la variable de entorno `{Almacen.LlaveDeConfiguracion}`.");
    return 2;
}

var lineas = new List<string>();
void Escribir(string l) { lineas.Add(l); Console.WriteLine(l); }

var excepciones = 0;

// LA CONTRASEÑA REAL SE PRODUCE CON LA FÁBRICA DEL PRODUCTO Y NO SE ESCRIBE ACÁ. Es la condición
// que hace que la inspección de la fuente pueda dar cero: el sample no conoce el valor hasta que
// corre, así que no hay dónde escribirlo.
var clara = new GeometriaFactory.Infrastructure.Security.ProvisionalPasswordFactory().Produce()!;
var derivado = string.Empty;

try
{
    // ---- Acto 1 · CU-06006 ----
    (_, derivado) = ActoDerivarYVerificar.Ejecutar(clara, Escribir);

    // ---- Acto 2 · CU-06007 ----
    ActoProducirProvisoria.Ejecutar("alumna@ejemplo.edu", "Quiroga", Escribir);

    // ---- Acto 3 · CU-06008 ----
    await ActoEmitirAcceso.EjecutarAsync(Escribir);

    // ---- Acto 4 · CU-06009 ----
    ActoReloj.Ejecutar(Escribir);

    // ---- Acto 5 · CU-06010 ----
    await ActoPrepararAlmacen.EjecutarAsync(directorio, Escribir);
}
catch (Exception error)
{
    excepciones++;
    Console.Error.WriteLine($"Excepción no esperada: {error.GetType().Name}: {error.Message}");
}

// ---- Las dos inspecciones, con umbral cero ----
// CORREN AL FINAL PORQUE LA SEGUNDA CUENTA SOBRE LA SALIDA PRODUCIDA, que hasta acá no existía.
try
{
    var enLaFuente = Inspecciones.SecretosEnLaFuente(
        claveDeFirma: Environment.GetEnvironmentVariable("AccessToken__SigningKey") ?? "",
        contrasenaReal: clara,
        rutaDelAlmacen: directorio);
    Escribir($"[insp] Ocurrencias de clave de firma, contrasena real o ruta del almacen "
        + $"en la fuente del sample: {enLaFuente}");

    var enLaTraza = Inspecciones.ClaroEnLaTraza(lineas, clara, derivado);
    Escribir($"[insp] Ocurrencias de contrasena en claro o de valor derivado "
        + $"en la salida producida: {enLaTraza}");
}
catch (InvalidOperationException sinMedicion)
{
    // UNA INSPECCIÓN QUE NO ENCONTRÓ QUÉ MEDIR NO INFORMA CERO. Informa que no midió.
    excepciones++;
    Console.Error.WriteLine($"Inspección sin medición: {sinMedicion.Message}");
}

var rechazos = lineas.Count(l =>
    System.Text.RegularExpressions.Regex.IsMatch(l, @"\b[A-Z]{3,}(_[A-Z]+)+\b"));
Escribir($"Actos recorridos: 5 | Rechazos tipados: {rechazos} | Excepciones: {excepciones}");

return SalidaEsperada.Comparar(lineas) + (excepciones > 0 ? 1 : 0);
