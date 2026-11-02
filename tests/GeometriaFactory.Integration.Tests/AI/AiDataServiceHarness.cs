using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GeometriaFactory.Integration.Tests.AI;

/// <summary>
/// El servicio de datos REAL con dos ajustes que sólo las pruebas de la capa de IA necesitan: un
/// entorno elegido y servicios sustituidos (un doble de <c>IChatClient</c>, un registro que captura).
/// </summary>
internal sealed class AiDataServiceHarness : DataServiceHarness
{
    private readonly Action<IServiceCollection>? _services;
    private readonly string? _environment;

    public AiDataServiceHarness(
        string storePath,
        Action<IServiceCollection>? services = null,
        string? environment = null,
        IReadOnlyDictionary<string, string?>? settings = null)
        : base(storePath, settings)
    {
        _services = services;
        _environment = environment;
    }

    public CapturingLoggerProvider Logs { get; } = new();

    protected override void ConfigureWebHost(Microsoft.AspNetCore.Hosting.IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        if (_environment is not null)
        {
            builder.UseEnvironment(_environment);
        }

        builder.ConfigureTestServices(services =>
        {
            services.AddSingleton<ILoggerProvider>(Logs);
            _services?.Invoke(services);
        });
    }
}

/// <summary>Proveedor de registros que guarda cada línea formateada de cualquier categoría.</summary>
internal sealed class CapturingLoggerProvider : ILoggerProvider
{
    private readonly List<string> _lines = [];

    public IReadOnlyList<string> Lines
    {
        get
        {
            lock (_lines)
            {
                return [.. _lines];
            }
        }
    }

    public ILogger CreateLogger(string categoryName) => new Line(_lines);

    public void Dispose()
    {
    }

    private sealed class Line(List<string> lines) : ILogger
    {
        public IDisposable? BeginScope<TState>(TState state)
            where TState : notnull => null;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            lock (lines)
            {
                lines.Add(formatter(state, exception));
            }
        }
    }
}
