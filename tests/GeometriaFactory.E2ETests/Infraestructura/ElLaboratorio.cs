using System.Net.Http.Json;
using System.Text.Json;
using NUnit.Framework;

namespace GeometriaFactory.E2ETests;

/// <summary>
/// El laboratorio contra el que se prueba: dónde vive, con qué credencial se entra, y cómo se
/// siembra y se limpia lo que cada recorrido necesita.
/// </summary>
/// <remarks>
/// ═══════════════════════════════════════════════════════════════════════════════════════════
/// SE PRUEBA CONTRA UN DESPLIEGUE, NO CONTRA UN PROCESO QUE ESTA SUITE LEVANTA. El laboratorio de
/// referencia publica y arranca la aplicación cuando no hay `URL_BASE`; acá no, y la razón es que
/// este producto son DOS piezas —la pública y el servicio de datos— más un almacén, y orquestar
/// eso ya lo hace `tools/verificar-resolucion-del-trabajo.sh`. Duplicarlo daría dos lugares donde
/// el montaje puede decir cosas distintas, que es el defecto que esta casa persigue.
///
/// Para correrlas contra algo local se levanta ese banco y se apunta `URL_BASE` ahí.
///
/// ═══════════════════ POR QUE HACE FALTA UNA CREDENCIAL DE VERDAD ═══════════════════
///
/// EL ALMACEN TIENE UNICIDAD SOBRE EL PAPEL: no puede haber dos administradores. La suite **no
/// puede crear el suyo** —lo comprobó el peritaje del 2026-09-02, con un `UNIQUE constraint
/// failed: Account.Role`—, así que entra con la del docente, que llega por secreto y nunca se
/// escribe en el código ni en el registro de la corrida.
///
/// ═══════════════════ Y POR ESO LA REGLA DE ORO DE ESTA SUITE ═══════════════════
///
/// **NINGUNA PRUEBA TOCA UN DATO QUE NO SEMBRO.** Se corre contra el laboratorio donde hay
/// trabajos de alumnos de verdad: aprobar uno ajeno para «ver si el botón anda» sería tomar una
/// decisión pedagógica que no le corresponde a una prueba. Cada recorrido siembra su propio alumno
/// —con correo único— y sus propios trabajos, y los borra al terminar.
/// ═══════════════════════════════════════════════════════════════════════════════════════════
/// </remarks>
public static class ElLaboratorio
{
    private static readonly JsonSerializerOptions Json = new(JsonSerializerDefaults.Web);

    /// <summary>La dirección pública, la que abre el navegador.</summary>
    public static string UrlBase { get; private set; } = string.Empty;

    /// <summary>La dirección del servicio de datos, la que se usa para sembrar y limpiar.</summary>
    /// <remarks>
    /// LA SIEMBRA VA POR EL SERVICIO DE DATOS Y NO POR LA PANTALLA, y es deliberado: si la siembra
    /// pasara por la interfaz, un defecto en el alta de cuentas dejaría sin correr los recorridos
    /// que no tienen nada que ver con el alta. **Lo que se prueba se hace por pantalla; lo que se
    /// prepara, no.**
    /// </remarks>
    public static string UrlDelServicioDeDatos { get; private set; } = string.Empty;

    private static string _correoAdministrador = string.Empty;
    private static string _claveAdministrador = string.Empty;

    /// <summary>Lee la configuración de la corrida, o falla diciendo exactamente qué falta.</summary>
    public static void Leer()
    {
        UrlBase = Exigir("URL_BASE", "la dirección pública del laboratorio, por ejemplo https://aplicada.somee.com").TrimEnd('/');
        UrlDelServicioDeDatos = Exigir("API_BASE_URL", "la dirección del servicio de datos, la misma que la publicación le inyecta al front").TrimEnd('/');
        _correoAdministrador = Exigir("E2E_ADMIN_EMAIL", "el correo del administrador del laboratorio");
        _claveAdministrador = Exigir("E2E_ADMIN_PASSWORD", "la contraseña de ese administrador");

        TestContext.Progress.WriteLine($"Laboratorio: {UrlBase}");
        TestContext.Progress.WriteLine($"Servicio de datos: {UrlDelServicioDeDatos}");
    }

    private static string Exigir(string nombre, string queEs)
    {
        var valor = Environment.GetEnvironmentVariable(nombre);

        if (!string.IsNullOrWhiteSpace(valor))
        {
            return valor;
        }

        // SE FALLA DICIENDO QUE FALTA Y QUE ES, y no con una referencia nula tres capas más abajo.
        // El laboratorio de referencia declara el sintoma de ese descuido: «la URL base llega vacía
        // y Playwright se queja de la cookie», que no le dice a nadie qué configurar.
        throw new InvalidOperationException(
            $"Falta la variable de entorno «{nombre}»: {queEs}. " +
            "Estas pruebas corren contra un despliegue y no levantan nada por su cuenta.");
    }

    /// <summary>El correo del administrador. Sólo lo usa el paso de ingreso.</summary>
    public static string CorreoDelAdministrador => _correoAdministrador;

    /// <summary>La contraseña del administrador.</summary>
    public static string ClaveDelAdministrador => _claveAdministrador;

    /// <summary>
    /// Comprueba que el laboratorio esté atendiendo, ANTES de correr una sola prueba.
    /// </summary>
    /// <remarks>
    /// ═══════════════════════════════════════════════════════════════════════════════════════
    /// POR QUE EXISTE, Y ES LA LECCION MAS CARA DE TODA ESTA JORNADA. El 2026-09-02, la primera
    /// corrida en serie de esta suite dio TRECE ROJOS. Ninguno era del producto:
    ///
    ///     Timeout 30000ms exceeded — navigating to "https://aplicada.somee.com/estado"
    ///
    /// El anfitrión no estaba atendiendo. Medido en el momento, tres intentos seguidos:
    ///
    ///     intento 1: HTTP 000  45.0 s
    ///     intento 2: HTTP 000   9.9 s
    ///     intento 3: HTTP 200   4.7 s
    ///
    /// El servicio de datos, en paralelo, respondía en 0.0006 s. **El anfitrión de somee es
    /// intermitente**: se cae por decenas de segundos y vuelve.
    ///
    /// UNA SUITE QUE NO DISTINGUE ESO ES PEOR QUE NO TENERLA. Trece pruebas rojas con nombres de
    /// producto —«el administrador entra», «aprobar aplica el desenlace»— leídas por alguien
    /// apurado dicen «el producto está roto», y esta jornada ya perdió medio día persiguiendo
    /// defectos que eran del banco y no del producto. Cuatro veces.
    ///
    /// ASI QUE SE FALLA UNA VEZ, ANTES DE EMPEZAR, Y CON EL MOTIVO EN LA CARA. La corrida sigue
    /// siendo roja —no se disimula—, pero nadie la confunde con un defecto.
    ///
    /// NO SE USA `Assert.Ignore`, y se descartó a propósito: una corrida en gris se lee como «no
    /// hizo falta probar» y este laboratorio no puede permitirse eso. Que el anfitrión no atienda
    /// ES un problema, sólo que no es del producto.
    /// ═══════════════════════════════════════════════════════════════════════════════════════
    /// </remarks>
    public static async Task ExigirQueElLaboratorioRespondaAsync()
    {
        const int Intentos = 6;
        using var cliente = new HttpClient { Timeout = TimeSpan.FromSeconds(20) };
        var motivos = new List<string>();

        for (var intento = 1; intento <= Intentos; intento++)
        {
            try
            {
                var respuesta = await cliente.GetAsync($"{UrlBase}/estado");

                if (respuesta.IsSuccessStatusCode)
                {
                    TestContext.Progress.WriteLine($"El laboratorio atiende (intento {intento}).");
                    return;
                }

                motivos.Add($"intento {intento}: HTTP {(int)respuesta.StatusCode}");
            }
            catch (Exception falla)
            {
                motivos.Add($"intento {intento}: {falla.GetType().Name}");
            }

            // SE REINTENTA PORQUE LA INTERMITENCIA ES DE SEGUNDOS, no de minutos: la medición
            // muestra que vuelve. Seis intentos con diez segundos entre medio dan un minuto de
            // margen, que es más de lo que duró la caída medida.
            if (intento < Intentos)
            {
                await Task.Delay(TimeSpan.FromSeconds(10));
            }
        }

        throw new InvalidOperationException(
            $"EL LABORATORIO NO RESPONDIO en {Intentos} intentos contra {UrlBase}/estado " +
            $"({string.Join(" · ", motivos)}). " +
            "NO SE PUEDE CONCLUIR NADA SOBRE EL PRODUCTO: no se corrió ninguna prueba. " +
            "Este anfitrión es intermitente y se cae por decenas de segundos; " +
            "volver a lanzar la corrida es lo que corresponde.");
    }

    // ---- LA SIEMBRA ---------------------------------------------------------------------------

    private static HttpClient Cliente() => new() { BaseAddress = new Uri(UrlDelServicioDeDatos + "/") };

    /// <summary>Canjea la credencial del administrador por un testigo del servicio de datos.</summary>
    public static async Task<string> TestigoDelAdministradorAsync()
    {
        using var cliente = Cliente();
        var respuesta = await cliente.PostAsJsonAsync("auth/token",
            new { email = _correoAdministrador, password = _claveAdministrador });

        respuesta.EnsureSuccessStatusCode();
        var cuerpo = await respuesta.Content.ReadFromJsonAsync<JsonElement>(Json);
        return cuerpo.GetProperty("accessToken").GetString()!;
    }

    /// <summary>
    /// Da de alta un alumno propio de esta corrida y lo deja listo para entrar.
    /// </summary>
    /// <remarks>
    /// EL CORREO LLEVA UN SUFIJO UNICO Y UNA MARCA RECONOCIBLE. Lo único porque dos corridas pueden
    /// solaparse —la matriz de navegadores corre tres a la vez—; la marca porque si algo falla a
    /// mitad de camino y una cuenta queda sin borrar, el docente tiene que poder mirar su panel y
    /// saber, sin preguntarle a nadie, que esa cuenta la dejó una prueba.
    /// </remarks>
    public static async Task<AlumnoSembrado> SembrarAlumnoAsync(string recorrido)
    {
        var correo = $"e2e-{recorrido}-{Guid.NewGuid():n}@prueba-automatica.invalid".ToLowerInvariant();

        // EL ALTA VA SIN CREDENCIAL, Y ESO NO ES UN DESCUIDO: registrarse es un acto de la persona
        // que se registra, no del administrador. El servicio de datos lo hace cumplir y responde
        // 401 si llega con el testigo del docente —lo comprobó la primera corrida de esta suite,
        // el 2026-09-02, con cinco casos rojos en la siembra—.
        //
        // LA SUITE ATRAPO SU PROPIO ERROR, que es una forma barata de aprender la regla: la
        // infraestructura de pruebas también es un cliente del contrato y también se equivoca.
        using var sinCredencial = Cliente();
        var alta = await sinCredencial.PostAsJsonAsync("cuentas",
            new { email = correo, firstName = "Prueba", lastName = "Automatica" });
        alta.EnsureSuccessStatusCode();
        var cuenta = await alta.Content.ReadFromJsonAsync<JsonElement>(Json);
        var id = cuenta.GetProperty("accountId").GetGuid();

        // ═══ DESDE ACA, SI ALGO FALLA, LA CUENTA YA EXISTE Y HAY QUE BORRARLA ═══
        //
        // Y NO ES UNA PRECAUCION TEORICA. El 2026-09-02, dos corridas seguidas fallaron en el paso
        // de cambio de contraseña —un 401 que era error de esta misma plomería— y dejaron CUATRO
        // CUENTAS SEMBRADAS en el panel del docente. No falló la limpieza: falló la siembra a mitad
        // de camino, y como la excepción cortaba antes de devolver el alumno, EL DESMONTAJE NO
        // TENIA QUE BORRAR.
        //
        // El que ensucia limpia, incluso cuando se cae en el intento.
        try
        {
            // LA HABILITACION SI ES DEL ADMINISTRADOR, y es la que devuelve la provisoria.
            var testigo = await TestigoDelAdministradorAsync();
            using var cliente = Cliente();
            cliente.DefaultRequestHeaders.Authorization = new("Bearer", testigo);

            var habilitacion = await cliente.PostAsJsonAsync($"cuentas/{id}/situacion",
                new { accountId = id, intendedStatus = "Enabled" });
            habilitacion.EnsureSuccessStatusCode();
            var provisoria = (await habilitacion.Content.ReadFromJsonAsync<JsonElement>(Json))
                .GetProperty("provisionalPassword").GetString()!;

        // Y EL CAMBIO DE CONTRASEÑA TAMBIEN VA SIN CREDENCIAL, por la misma razón que el alta:
        // elegir la propia clave es un acto DE LA PERSONA, no del docente —que además nunca llega a
        // conocerla, y el producto lo declara así—. Con el testigo del administrador responde 401.
        //
        // SON DOS VECES EL MISMO ERROR EN EL MISMO METODO, y la lección no es «acordarse»: es que
        // el servicio de datos distingue quién puede hacer qué con más rigor del que uno supone
        // cuando escribe la plomería de una prueba.
            var propia = $"E2e-{Guid.NewGuid():n}"[..20] + "-2026";
            var cambio = await sinCredencial.PostAsJsonAsync("cuenta/contrasena",
                new { email = correo, currentPassword = provisoria, newPassword = propia });
            cambio.EnsureSuccessStatusCode();

            return new AlumnoSembrado(id, correo, propia);
        }
        catch
        {
            Avisar($"La siembra de {correo} se cayó a mitad de camino. Se borra la cuenta ya creada.");
            await LimpiarAsync(new AlumnoSembrado(id, correo, string.Empty));
            throw;
        }
    }

    /// <summary>
    /// Habilita, por correo, una cuenta que se registró POR PANTALLA, y devuelve su provisoria.
    /// </summary>
    /// <remarks>
    /// EXISTE PARA EL RECORRIDO DEL ALUMNO, donde el registro es parte de lo que se prueba y por
    /// lo tanto no se puede sembrar por atajo. Lo que sí es preparación —y va por acá— es la
    /// habilitación, que es un acto del administrador y tiene su propia clase de pruebas.
    ///
    /// LA IDENTIDAD SE BUSCA EN EL LISTADO Y NO SE ADIVINA: el registro por pantalla no le
    /// devuelve el identificador a nadie, y componerlo del lado de la prueba sería inventarse un
    /// contrato que el producto no ofrece.
    /// </remarks>
    public static async Task<(Guid Cuenta, string Provisoria)> HabilitarPorCorreoAsync(string correo)
    {
        var testigo = await TestigoDelAdministradorAsync();
        using var cliente = Cliente();
        cliente.DefaultRequestHeaders.Authorization = new("Bearer", testigo);

        var listado = await cliente.GetAsync("cuentas");
        listado.EnsureSuccessStatusCode();
        var cuentas = await listado.Content.ReadFromJsonAsync<JsonElement>(Json);

        var id = Guid.Empty;

        foreach (var cuenta in cuentas.EnumerateArray())
        {
            if (string.Equals(cuenta.GetProperty("email").GetString(), correo, StringComparison.OrdinalIgnoreCase))
            {
                id = cuenta.GetProperty("accountId").GetGuid();
                break;
            }
        }

        if (id == Guid.Empty)
        {
            throw new InvalidOperationException(
                $"La cuenta «{correo}» no aparece en el listado del administrador después de " +
                "registrarse por pantalla. El registro no prosperó, o el listado no la muestra.");
        }

        var habilitacion = await cliente.PostAsJsonAsync($"cuentas/{id}/situacion",
            new { accountId = id, intendedStatus = "Enabled" });
        habilitacion.EnsureSuccessStatusCode();

        var provisoria = (await habilitacion.Content.ReadFromJsonAsync<JsonElement>(Json))
            .GetProperty("provisionalPassword").GetString()!;

        return (id, provisoria);
    }

    /// <summary>El único trabajo de una cuenta. Falla si hay más de uno, o ninguno.</summary>
    /// <remarks>
    /// FALLA EN VEZ DE TOMAR EL PRIMERO. Si un recorrido que carga un solo trabajo encuentra dos,
    /// algo pasó que la prueba no entiende, y elegir uno al azar convertiría ese desconcierto en
    /// un verde o en un rojo arbitrario.
    /// </remarks>
    public static async Task<Guid> UnicoTrabajoDeAsync(Guid cuenta)
    {
        var testigo = await TestigoDelAdministradorAsync();
        using var cliente = Cliente();
        cliente.DefaultRequestHeaders.Authorization = new("Bearer", testigo);

        var listado = await cliente.GetAsync("trabajos");
        listado.EnsureSuccessStatusCode();
        var trabajos = await listado.Content.ReadFromJsonAsync<JsonElement>(Json);

        var suyos = new List<Guid>();

        foreach (var trabajo in trabajos.EnumerateArray())
        {
            if (trabajo.TryGetProperty("ownerId", out var duenio) && duenio.GetGuid() == cuenta)
            {
                suyos.Add(trabajo.GetProperty("workId").GetGuid());
            }
        }

        return suyos.Count == 1
            ? suyos[0]
            : throw new InvalidOperationException(
                $"Se esperaba UN trabajo de la cuenta {cuenta} y hay {suyos.Count}.");
    }

    /// <summary>Carga un trabajo enviado a nombre del alumno sembrado.</summary>
    public static Task<Guid> SembrarTrabajoEnviadoAsync(AlumnoSembrado alumno, string nombre) =>
        SembrarTrabajoEnviadoAsync(alumno, nombre, TextoDeUnTrabajo);

    /// <summary>Idem, con el texto que el caso necesita en vez del texto canónico.</summary>
    /// <remarks>
    /// EL TEXTO ES PARTE DEL CASO cuando lo que se prueba es qué dice la pantalla sobre lo que se
    /// pudo interpretar. `FiguraQueNoSePudoLeerTests` necesita uno cuyas claves el producto NO
    /// reconozca, y ése no puede salir de la constante compartida.
    /// </remarks>
    public static async Task<Guid> SembrarTrabajoEnviadoAsync(
        AlumnoSembrado alumno, string nombre, string textoDelTrabajo)
    {
        using var cliente = Cliente();
        var canje = await cliente.PostAsJsonAsync("auth/token",
            new { email = alumno.Correo, password = alumno.Clave });
        canje.EnsureSuccessStatusCode();
        var testigo = (await canje.Content.ReadFromJsonAsync<JsonElement>(Json)).GetProperty("accessToken").GetString()!;

        cliente.DefaultRequestHeaders.Authorization = new("Bearer", testigo);
        var envio = await cliente.PostAsJsonAsync("trabajos", new
        {
            name = nombre,
            declaredDate = "2026-08-30",
            description = (string?)null,
            originalJson = textoDelTrabajo,
        });

        envio.EnsureSuccessStatusCode();
        return (await envio.Content.ReadFromJsonAsync<JsonElement>(Json)).GetProperty("workId").GetGuid();
    }

    /// <summary>Cómo quedó un trabajo, según el SERVICIO DE DATOS y no según la pantalla.</summary>
    /// <remarks>
    /// ES LA DIFERENCIA ENTRE «LA PANTALLA NAVEGO» Y «EL DESENLACE SE APLICO». Una prueba que
    /// termina mirando la pantalla que ella misma provocó puede dar verde con el dato intacto: pasó
    /// el 2026-09-01, con el botón de aprobar dibujado y el trabajo en `Pendiente`.
    /// </remarks>
    public static async Task<string> EstadoDelTrabajoAsync(Guid trabajo)
    {
        var testigo = await TestigoDelAdministradorAsync();
        using var cliente = Cliente();
        cliente.DefaultRequestHeaders.Authorization = new("Bearer", testigo);

        var respuesta = await cliente.GetAsync($"trabajos/{trabajo}");
        respuesta.EnsureSuccessStatusCode();
        return (await respuesta.Content.ReadFromJsonAsync<JsonElement>(Json)).GetProperty("status").GetString()!;
    }

    /// <summary>
    /// Borra lo que esta corrida sembró.
    /// </summary>
    /// <remarks>
    /// SE TRAGA LOS ERRORES A PROPOSITO. La limpieza corre en el desmontaje, después de que el caso
    /// ya dio su veredicto: si fallara, taparía el resultado real con un error de plomería. Lo que
    /// sí hace es DECIRLO en el progreso, para que una cuenta huérfana no quede en silencio.
    /// </remarks>
    public static async Task LimpiarAsync(AlumnoSembrado? alumno)
    {
        if (alumno is null)
        {
            return;
        }

        try
        {
            var testigo = await TestigoDelAdministradorAsync();
            using var cliente = Cliente();
            cliente.DefaultRequestHeaders.Authorization = new("Bearer", testigo);

            // LA BAJA DE CUENTA SE LLEVA SUS TRABAJOS —lo declara el aviso de la propia pantalla—,
            // así que no hace falta borrarlos uno por uno. Y pide el correo escrito como
            // confirmación, que es la misma defensa que enfrenta una persona.
            var baja = new HttpRequestMessage(HttpMethod.Delete, $"cuentas/{alumno.Id}")
            {
                Content = JsonContent.Create(new { accountId = alumno.Id, confirmationEmail = alumno.Correo }),
            };

            var respuesta = await cliente.SendAsync(baja);

            if (!respuesta.IsSuccessStatusCode)
            {
                var detalle = await respuesta.Content.ReadAsStringAsync();
                Avisar($"NO SE PUDO LIMPIAR {alumno.Correo}: HTTP {(int)respuesta.StatusCode} · {detalle}");
            }
        }
        catch (Exception falla)
        {
            Avisar($"NO SE PUDO LIMPIAR {alumno.Correo}: {falla.GetType().Name} · {falla.Message}");
        }
    }

    /// <summary>Un aviso que SE VE en la salida de la corrida.</summary>
    /// <remarks>
    /// SE ESCRIBE EN EL ERROR ESTANDAR Y NO SOLO EN EL PROGRESO DE NUNIT, y la diferencia no es
    /// cosmética: el 2026-09-02 la limpieza falló DOS CORRIDAS SEGUIDAS y dejó cuatro cuentas
    /// sembradas en el panel del docente **sin que nada apareciera en la salida**, porque
    /// `TestContext.Progress` no llega al registro de la consola con la verbosidad por omisión.
    ///
    /// Un aviso que no se ve es exactamente lo mismo que no avisar.
    /// </remarks>
    private static void Avisar(string mensaje)
    {
        Console.Error.WriteLine($"[E2E] {mensaje}");
        TestContext.Progress.WriteLine(mensaje);
    }

    /// <param name="Id">Identidad de la cuenta, para poder borrarla.</param>
    /// <param name="Correo">El correo, que también es la confirmación de la baja.</param>
    /// <param name="Clave">La contraseña que la prueba le puso, ya cambiada desde la provisoria.</param>
    public sealed record AlumnoSembrado(Guid Id, string Correo, string Clave);

    /// <summary>
    /// Un trabajo mínimo y válido: un cubo.
    /// </summary>
    /// <remarks>
    /// ES CHICO A PROPOSITO. Lo que estas pruebas verifican es el recorrido, no la geometría: para
    /// la geometría están las 94 pruebas de dominio, que corren en milisegundos y no necesitan
    /// navegador. Un texto grande sólo agregaría segundos de red a cada siembra.
    /// </remarks>
    private const string TextoDeUnTrabajo = """
        [ { "Tipo": "Cubo", "Caras": [
          { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 },
          { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 },
          { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 },
          { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 },
          { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 },
          { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 } ],
          "Area": 54.00, "Volumen": 27.00 } ]
        """;
}
