using Microsoft.Playwright;
using NUnit.Framework;

namespace GeometriaFactory.E2ETests;

/// <summary>
/// La versión angosta del producto: lo que el sistema visual promete por debajo de 768 px, mirado
/// en una ventana de 768 px para abajo.
/// </summary>
/// <remarks>
/// ═══════════════════════════════════════════════════════════════════════════════════════════
/// POR QUE EXISTE ESTA CLASE, Y NO ES UNA PRECAUCION TEORICA. El 2026-08-31 este producto tuvo
/// un `P0` —`MI-02`— que decía exactamente esto: la hoja de estilos apagaba la tabla por debajo
/// de 768 px y encendía una clase de tarjetas QUE NINGUN COMPONENTE EMITIA. Resultado: en un
/// teléfono, los tres listados del producto no dibujaban NINGUNA FILA. Ni un error, ni un aviso:
/// una pantalla vacía.
///
/// **LAS 372 PRUEBAS DE LA SOLUCION NO PODIAN VERLO**, y no por descuido: ninguna abre un
/// navegador, y las que sí lo abrían corrían en una ventana de escritorio. Un defecto que sólo
/// existe por debajo de un ancho no lo encuentra nadie que no mire ese ancho.
///
/// Y CUANDO SE CERRO, SE CERRO SIN MIRAR. El registro de la jornada lo dice con todas las
/// letras: «el `P0` de las 768 px se cerró y nunca se mostró un teléfono con filas; cuando se
/// sacaron capturas aparecieron tres recortes que los conteos daban por buenos».
///
/// ═══════════════════ QUE SE AFIRMA, Y DE DONDE SALE CADA AFIRMACION ═══════════════════
///
/// **NO SE INVENTA NINGUNA REGLA.** Cada caso verifica una regla escrita en
/// `wwwroot/css/app.css`, y el comentario dice cuál:
///
///   · `@media (max-width: 768px)` → `.gf-table-wrapper { display: none }` y
///     `.gf-stacked-cards { display: flex }`  ..............  el `P0` `MI-02`
///   · `.gf-two-columns { grid-template-columns: minmax(0,1fr) }` con
///     `.gf-column--scene { order: 1 }` .....................  una sola columna, escena primero
///   · `R-06` · `.gf-footer-actions { flex-direction: column }` ..  la primaria primero
///
/// **LAS CAPTURAS QUEDAN AUNQUE EL CASO PASE**, y es deliberado: en esta casa la evidencia de lo
/// visual se mira. Un conteo en verde ya dio por bueno un teléfono con tres recortes.
///
/// ═══════════════════ POR QUE UNA VENTANA Y NO UN DISPOSITIVO ═══════════════════
///
/// Se abre un contexto con un ANCHO en píxeles y no un descriptor `Pixel 7`. Lo que el sistema
/// visual declara es un ancho; emular un teléfono entero traería además factor de escala, agente
/// de usuario y eventos táctiles —tres variables más para explicar un rojo que no las necesita—.
/// ═══════════════════════════════════════════════════════════════════════════════════════════
/// </remarks>
public sealed class DisenoResponsivoTests : PruebaE2E
{
    /// <summary>
    /// El punto de quiebre del sistema visual, en píxeles.
    /// </summary>
    /// <remarks>
    /// NO ES UN NUMERO ELEGIDO POR ESTA CLASE: es el que `wwwroot/css/app.css` escribe en sus dos
    /// bloques `@media (max-width: 768px)`. Se repite acá porque una prueba que verifica el
    /// comportamiento angosto tiene que decir contra qué medida lo verifica; si el sistema visual
    /// mueve su quiebre, esta constante se mueve con él.
    /// </remarks>
    private const int PuntoDeQuiebre = 768;

    // UNA MEDIDA DE CADA LADO DEL QUIEBRE, Y NINGUNA PEGADA A EL. 390 px es el ancho de un
    // teléfono corriente y 1280 el de una ventana de escritorio: probar en 767 y 769 mediría la
    // regla del `@media` y no el producto, que es lo que le interesa a quien lo usa.
    private const int AnchoAngosto = 390;
    private const int AltoAngosto = 844;
    private const int AnchoAncho = 1280;
    private const int AltoAncho = 800;

    private ElLaboratorio.AlumnoSembrado? _alumno;
    private Guid _trabajo;

    /// <summary>
    /// Siembra un alumno y un trabajo enviado: sin datos no hay filas, y sin filas esta clase no
    /// puede distinguir «se dibujan tarjetas» de «no se dibuja nada», que es justamente el defecto
    /// que vino a cuidar.
    /// </summary>
    [OneTimeSetUp]
    public async Task SembrarAsync()
    {
        // LAS DOS MEDIDAS TIENEN QUE CAER UNA DE CADA LADO DEL QUIEBRE, y si alguien mueve una sin
        // mirar la otra, esto lo dice antes de que la clase entera empiece a afirmar cualquier cosa.
        Assert.That(AnchoAngosto, Is.LessThan(PuntoDeQuiebre));
        Assert.That(AnchoAncho, Is.GreaterThan(PuntoDeQuiebre));

        _alumno = await ElLaboratorio.SembrarAlumnoAsync("responsivo");
        _trabajo = await ElLaboratorio.SembrarTrabajoEnviadoAsync(_alumno, "E2E diseño responsivo");
    }

    [OneTimeTearDown]
    public Task LimpiarAsync() => ElLaboratorio.LimpiarAsync(_alumno);

    [Test]
    public async Task EnPantallaAnchaElListadoEsUnaTablaYNoHayTarjetas()
    {
        var pagina = await AbrirVentanaAsync(AnchoAncho, AltoAncho);
        await IngresarComoAdministradorAsync(pagina);
        await pagina.GotoAsync("/cuentas", new() { WaitUntil = WaitUntilState.Load });

        await Expect(pagina.Locator(".gf-table-wrapper").First).ToBeVisibleAsync();
        await Expect(pagina.Locator(".gf-stacked-cards").First).Not.ToBeVisibleAsync();
    }

    [Test]
    public async Task EnPantallaAngostaElListadoSonTarjetasYNoQuedaVacio()
    {
        var pagina = await AbrirVentanaAsync(AnchoAngosto, AltoAngosto);
        await IngresarComoAdministradorAsync(pagina);
        await pagina.GotoAsync("/cuentas", new() { WaitUntil = WaitUntilState.Load });

        await Expect(pagina.Locator(".gf-table-wrapper").First).Not.ToBeVisibleAsync();
        await Expect(pagina.Locator(".gf-stacked-cards").First).ToBeVisibleAsync();

        // ═══ ESTE ES EL ASERTO DEL `P0`, Y NO ES EL DE ARRIBA ═══
        //
        // Que el contenedor de tarjetas esté visible NO PRUEBA QUE HAYA UNA SOLA FILA: un
        // contenedor vacío también se muestra. El defecto de agosto era exactamente ése —la
        // clase encendida sin emisor—, así que lo que se cuenta son las tarjetas.
        var tarjetas = pagina.Locator(".gf-stacked-cards .gf-row-card");
        Assert.That(await tarjetas.CountAsync(), Is.GreaterThan(0),
            "Por debajo de 768 px el listado no dibujó ninguna fila. Es el defecto `MI-02`.");

        await Expect(tarjetas.First).ToBeVisibleAsync();
        await CapturarAsync(pagina, "cuentas-angosto");
    }

    [Test]
    public async Task EnPantallaAngostaNingunaPantallaDelPanelSeDesplazaDeCostado()
    {
        var pagina = await AbrirVentanaAsync(AnchoAngosto, AltoAngosto);
        await IngresarComoAdministradorAsync(pagina);

        foreach (var ruta in new[] { "/cuentas", "/entrega-comision", $"/trabajos/{_trabajo}" })
        {
            await pagina.GotoAsync(ruta, new() { WaitUntil = WaitUntilState.Load });

            // EL DESPLAZAMIENTO HORIZONTAL ES EL SINTOMA CLASICO de un ancho fijo que sobrevivió
            // al punto de quiebre, y en un teléfono se paga caro: la mitad derecha de la pantalla
            // queda afuera y nada avisa. Se mide el documento, que es lo que se arrastra.
            var ancho = await pagina.EvaluateAsync<int>("() => document.documentElement.scrollWidth");

            // UN PIXEL DE TOLERANCIA, y sale de cómo redondea el navegador los anchos
            // fraccionarios, no de aflojar el criterio.
            Assert.That(ancho, Is.LessThanOrEqualTo(AnchoAngosto + 1),
                $"«{ruta}» se desplaza de costado en {AnchoAngosto} px: el documento mide {ancho} px.");
        }
    }

    [Test]
    public async Task EnPantallaAngostaLaVistaDeTrabajoSeApilaConLaEscenaPrimero()
    {
        var pagina = await AbrirVentanaAsync(AnchoAngosto, AltoAngosto);
        await IngresarComoAdministradorAsync(pagina);
        await pagina.GotoAsync($"/trabajos/{_trabajo}", new() { WaitUntil = WaitUntilState.Load });

        // Se espera a que las dos columnas estén dibujadas antes de medirlas: una caja pedida a
        // mitad del dibujo vuelve nula, y el rojo diría «no se dibujó» donde lo que pasó es «no
        // se dibujó todavía».
        await Expect(pagina.Locator(".gf-column--scene")).ToBeVisibleAsync();
        await Expect(pagina.Locator(".gf-column--data")).ToBeVisibleAsync();

        var escena = await pagina.Locator(".gf-column--scene").BoundingBoxAsync();
        var datos = await pagina.Locator(".gf-column--data").BoundingBoxAsync();

        Assert.That(escena, Is.Not.Null, "No se dibujó la columna de la escena.");
        Assert.That(datos, Is.Not.Null, "No se dibujó la columna de los datos.");

        // UNA SOLA COLUMNA: las dos empiezan en la misma izquierda. Si siguieran lado a lado, la
        // segunda arrancaría más a la derecha y cada una mediría la mitad.
        Assert.That(escena!.X, Is.EqualTo(datos!.X).Within(1),
            "Las dos columnas no están apiladas: siguen una al lado de la otra.");

        // Y LA ESCENA VA PRIMERO, que es lo que declara `order: 1` sobre `.gf-column--scene`. En
        // el teléfono lo primero que se ve es la figura, no la tabla de datos.
        Assert.That(escena.Y, Is.LessThan(datos.Y),
            "La escena no quedó primera: `order` no está haciendo efecto.");

        await CapturarAsync(pagina, "vista-de-trabajo-angosto");
    }

    [Test]
    public async Task EnPantallaAngostaLaAccionPrimariaVaAntesQueLaDestructiva()
    {
        var pagina = await AbrirVentanaAsync(AnchoAngosto, AltoAngosto);
        await IngresarComoAdministradorAsync(pagina);
        await pagina.GotoAsync($"/trabajos/{_trabajo}", new() { WaitUntil = WaitUntilState.Load });

        // SE MIDE CUANDO LOS CONTROLES ESTAN VIVOS, Y NO ANTES. La medida sin esta espera es
        // intermitente y lo fue: el mismo caso pasó en una corrida y falló en la siguiente con
        // «no se dibujó la acción de retirar». El bloque de resolución se prerrenderiza y después
        // el circuito lo vuelve a dibujar; en esa ventana el nodo se reemplaza y NO TIENE CAJA.
        // Esperar a que el control se habilite es esperar una condición del producto —la misma
        // que espera una persona— y no un tiempo.
        var (aprobar, retirar) = await CajasDeLasAccionesAsync(pagina);

        Assert.That(aprobar, Is.Not.Null, "No se dibujó la acción de aprobar.");
        Assert.That(retirar, Is.Not.Null, "No se dibujó la acción de retirar.");

        // ═══ ES `R-06`, Y LO QUE ARREGLA NO ES COSMETICO ═══
        //
        // `column-reverse` en el pie ponía «Retirar» —la acción que borra— en el primer lugar de
        // la pantalla, a 583 px del borde superior, y hundía «Aprobar» al fondo, a 731 px. En el
        // teléfono el primer lugar es el que el pulgar alcanza sin mirar.
        Assert.That(aprobar!.Y, Is.LessThan(retirar!.Y),
            "La acción destructiva quedó por encima de la primaria. Es el defecto `R-06`.");

        await CapturarAsync(pagina, "acciones-angosto");
    }

    [Test]
    public async Task EnPantallaAnchaLasAccionesSiguenEnUnaSolaFila()
    {
        var pagina = await AbrirVentanaAsync(AnchoAncho, AltoAncho);
        await IngresarComoAdministradorAsync(pagina);
        await pagina.GotoAsync($"/trabajos/{_trabajo}", new() { WaitUntil = WaitUntilState.Load });

        // SE MIDE CUANDO LOS CONTROLES ESTAN VIVOS, Y NO ANTES. La medida sin esta espera es
        // intermitente y lo fue: el mismo caso pasó en una corrida y falló en la siguiente con
        // «no se dibujó la acción de retirar». El bloque de resolución se prerrenderiza y después
        // el circuito lo vuelve a dibujar; en esa ventana el nodo se reemplaza y NO TIENE CAJA.
        // Esperar a que el control se habilite es esperar una condición del producto —la misma
        // que espera una persona— y no un tiempo.
        var (aprobar, retirar) = await CajasDeLasAccionesAsync(pagina);

        Assert.That(aprobar, Is.Not.Null, "No se dibujó la acción de aprobar.");
        Assert.That(retirar, Is.Not.Null, "No se dibujó la acción de retirar.");

        // ES EL CONTRAPESO DEL CASO ANTERIOR, y sin él la suite se dejaría engañar por la
        // corrección más burda posible: apilar los botones SIEMPRE haría pasar el caso angosto y
        // rompería el ancho sin que nadie se entere.
        Assert.That(aprobar!.Y, Is.EqualTo(retirar!.Y).Within(2),
            "En pantalla ancha las acciones dejaron de estar en la misma fila.");
        Assert.That(aprobar.X, Is.LessThan(retirar.X),
            "En pantalla ancha la acción primaria dejó de ir primera.");
    }

    /// <summary>Las cajas de la acción primaria y la destructiva, medidas cuando las dos existen.</summary>
    /// <remarks>
    /// ═══ POR QUE SE REINTENTA, Y POR QUE NO SE ESPERA A QUE EL CONTROL SE HABILITE ═══
    ///
    /// **La medida sin reintento es intermitente**, y lo fue: el mismo caso pasó en una corrida y
    /// falló en la siguiente con «no se dibujó la acción de retirar». El bloque de resolución se
    /// prerrenderiza y después el circuito lo vuelve a dibujar; en esa ventana el nodo se
    /// reemplaza y **no tiene caja**. Se vuelve a pedir hasta que las dos la tengan.
    ///
    /// **Y NO SE ESPERA A `:not([disabled])`**, que fue el primer intento y es el que corresponde
    /// cuando se va a APRETAR un control. Acá no se aprieta nada: se mide dónde quedó dibujado.
    /// Atar la medición al enganche del circuito le agrega una dependencia que no necesita —y en
    /// una máquina cargada la hace fallar por algo que no tiene nada que ver con la disposición:
    /// medido el 2026-09-02 en firefox, con la máquina en carga 15, esa espera agotaba sus 30 s—.
    /// </remarks>
    private async Task<(LocatorBoundingBoxResult? Aprobar, LocatorBoundingBoxResult? Retirar)>
        CajasDeLasAccionesAsync(IPage pagina)
    {
        await Expect(pagina.Locator("[data-gf-outcome='Approve']")).ToBeVisibleAsync();
        await Expect(pagina.Locator("[data-gf-withdraw]")).ToBeVisibleAsync();

        LocatorBoundingBoxResult? aprobar = null;
        LocatorBoundingBoxResult? retirar = null;

        for (var intento = 0; intento < 20; intento++)
        {
            aprobar = await pagina.Locator("[data-gf-outcome='Approve']").BoundingBoxAsync();
            retirar = await pagina.Locator("[data-gf-withdraw]").BoundingBoxAsync();

            if (aprobar is not null && retirar is not null)
            {
                return (aprobar, retirar);
            }

            await Task.Delay(250);
        }

        return (aprobar, retirar);
    }
}
