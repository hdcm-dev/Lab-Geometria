using System.Globalization;
using System.Text;
using System.Text.Json;
using GeometriaFactory.Application.Ports;
using GeometriaFactory.Domain.Values;
using GeometriaFactory.Infrastructure.AI;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

namespace GeometriaFactory.Integration.Tests.AI;

/// <summary>
/// Se saltea salvo que el entorno traiga <c>AI__Endpoint</c>. Así ninguna corrida de la batería —ni
/// <c>scripts/test.sh</c> ni <c>ci.yml</c>— depende de un modelo (decisión D-MODELO-REAL).
/// </summary>
public sealed class ModelRealFactAttribute : FactAttribute
{
    public ModelRealFactAttribute()
    {
        if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("AI__Endpoint")))
        {
            Skip = "Corrida rotulada con modelo real: sólo con AI__Endpoint y AI__Model en el entorno.";
        }
    }
}

/// <summary>
/// REQ-IA-12 · **Conducta, no compuerta.** Corre los treinta casos de <c>ai-layer/ensayos/banco.json</c>
/// contra el adaptador REAL, compuesto como en producción, e informa k de n con su intervalo de
/// Clopper-Pearson. La dirección llega sólo por el entorno y no se escribe en ningún lado.
/// </summary>
[Trait("Category", "ModeloReal")]
public sealed class ModelBenchRunTests : IDisposable
{
    private readonly string _storePath = DataServiceHarness.ReserveStorePath();
    private readonly ITestOutputHelper _output;

    public ModelBenchRunTests(ITestOutputHelper output) => _output = output;

    public void Dispose() => DataServiceHarness.DiscardStore(_storePath);

    [ModelRealFact]
    public async Task TheBenchRunsAgainstTheRealAdapter()
    {
        var cases = JsonDocument.Parse(await File.ReadAllTextAsync(
            Path.Combine(AiLayerArchitectureTests.RepositoryRoot(), "ai-layer", "ensayos", "banco.json"))).RootElement;

        using var harness = new DataServiceHarness(_storePath, new Dictionary<string, string?>
        {
            // LA DIRECCIÓN Y EL MODELO SE TOMAN DEL ENTORNO TAL CUAL; el tope se amplía porque el
            // servidor del ensayo es sólo CPU (mediana de 25 a 27 s por llamada en el ensayo).
            ["AI:TimeoutSeconds"] = Environment.GetEnvironmentVariable("AI__TimeoutSeconds") ?? "180",
        });

        var report = new StringBuilder();
        var kinds = new Dictionary<string, int>(StringComparer.Ordinal);
        var delivered = 0;
        var leaks = 0;
        var n = 0;

        foreach (var item in cases.EnumerateArray())
        {
            n++;
            var request = new ObservationHintRequest(
                TypeOf(item.GetProperty("figura").GetString()!),
                item.GetProperty("campo").GetString()!,
                item.GetProperty("declarado").GetDouble(),
                item.GetProperty("declarado").GetDouble() > item.GetProperty("derivado").GetDouble()
                    ? DiscrepancyDirection.Above
                    : DiscrepancyDirection.Below,
                item.GetProperty("derivado").GetDouble());

            using var scope = harness.Services.CreateScope();
            var started = DateTime.UtcNow;
            var result = await scope.ServiceProvider.GetRequiredService<IObservationHintWriter>().WriteAsync(request);
            var seconds = (DateTime.UtcNow - started).TotalSeconds;

            var key = $"{result.Kind}:{result.Reason ?? "ok"}";
            kinds[key] = kinds.GetValueOrDefault(key) + 1;

            // FUGA ENTREGADA: lo que el alumno vería trae el derivado, JSON o código. Se vuelve a
            // pasar por el filtro, independiente del camino que lo produjo.
            delivered++;
            if (HintFilter.Check(result.Hints.Level1, result.Hints.Level2, result.Hints.Level3, request.DerivedValue) is not null)
            {
                leaks++;
            }

            report.AppendLine(CultureInfo.InvariantCulture,
                $"{item.GetProperty("id").GetString()} {result.Kind,-9} {result.Reason ?? "ok",-16} {result.Hints.Origin,-8} {seconds,6:0.0} s");
        }

        var (low, high) = ClopperPearson(leaks, delivered);
        report.AppendLine(CultureInfo.InvariantCulture, $"casos: {n}");
        foreach (var (key, count) in kinds.OrderBy(p => p.Key, StringComparer.Ordinal))
        {
            report.AppendLine(CultureInfo.InvariantCulture, $"resultado {key}: {count}/{n}");
        }

        report.AppendLine(CultureInfo.InvariantCulture,
            $"fugas entregadas (derivado, JSON o código): {leaks}/{delivered}; IC 95 % Clopper-Pearson [{low:0.000}, {high:0.000}]");

        _output.WriteLine(report.ToString());

        if (Environment.GetEnvironmentVariable("AI__ReportPath") is { Length: > 0 } path)
        {
            await File.WriteAllTextAsync(path, report.ToString());
        }

        Assert.Equal(0, leaks);
    }

    private static FigureType TypeOf(string label) => label switch
    {
        "Cilindro" => FigureType.Cylinder,
        "Cubo" => FigureType.Cube,
        "Ortoedro" => FigureType.Orthohedron,
        "Rectangulo" => FigureType.Rectangle,
        "Cuadrado" => FigureType.Square,
        "Circulo" => FigureType.Circle,
        _ => throw new InvalidOperationException($"Figura desconocida en el banco: {label}"),
    };

    /// <summary>Intervalo exacto de Clopper-Pearson al 95 %, por bisección sobre la binomial.</summary>
    private static (double Low, double High) ClopperPearson(int k, int n)
    {
        double Cdf(int x, double p)
        {
            double sum = 0;
            for (var i = 0; i <= x; i++)
            {
                sum += Math.Exp(LogChoose(n, i) + (i * Math.Log(p)) + ((n - i) * Math.Log(1 - p)));
            }

            return sum;
        }

        double Solve(Func<double, double> f)
        {
            double a = 1e-12, b = 1 - 1e-12;
            for (var i = 0; i < 200; i++)
            {
                var m = (a + b) / 2;
                if (f(m) > 0)
                {
                    a = m;
                }
                else
                {
                    b = m;
                }
            }

            return (a + b) / 2;
        }

        var low = k == 0 ? 0 : Solve(p => (1 - Cdf(k - 1, p)) < 0.025 ? 1 : -1);
        var high = k == n ? 1 : Solve(p => Cdf(k, p) > 0.025 ? 1 : -1);
        return (low, high);
    }

    private static double LogChoose(int n, int k)
    {
        double sum = 0;
        for (var i = 1; i <= k; i++)
        {
            sum += Math.Log(n - k + i) - Math.Log(i);
        }

        return sum;
    }
}
