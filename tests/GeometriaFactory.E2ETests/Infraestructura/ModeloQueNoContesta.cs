using System.Net;
using System.Net.Sockets;

namespace GeometriaFactory.E2ETests;

/// <summary>
/// Un servidor de modelos de mentira que acepta la conexión y no contesta nunca (G1 de la mesa UX
/// del 2026-11-03). Sólo existe en el banco local, sobre el bucle local.
/// </summary>
/// <remarks>
/// NO RESPONDE NI CIERRA: retiene cada conexión hasta que el banco baja. Es la forma más dura de
/// «el modelo tarda»: el servicio de datos tiene que cortar por su propio tope y devolver la pista
/// general del laboratorio (REQ-IA-07). La precarga del servicio también queda colgada acá, y está
/// bien: corre en segundo plano y no bloquea el arranque (REQ-IA-17).
/// </remarks>
public static class ModeloQueNoContesta
{
    private static TcpListener? _escucha;
    private static readonly List<TcpClient> Retenidas = [];

    /// <summary>Levanta el servidor y devuelve su dirección.</summary>
    public static string Levantar()
    {
        _escucha = new TcpListener(IPAddress.Loopback, 0);
        _escucha.Start();
        var puerto = ((IPEndPoint)_escucha.LocalEndpoint).Port;
        _ = AceptarAsync(_escucha);
        return $"http://127.0.0.1:{puerto}";
    }

    public static void Bajar()
    {
        _escucha?.Stop();
        _escucha = null;

        lock (Retenidas)
        {
            foreach (var conexion in Retenidas)
            {
                conexion.Dispose();
            }

            Retenidas.Clear();
        }
    }

    private static async Task AceptarAsync(TcpListener escucha)
    {
        try
        {
            while (true)
            {
                var conexion = await escucha.AcceptTcpClientAsync();
                lock (Retenidas)
                {
                    Retenidas.Add(conexion);
                }
            }
        }
        catch (Exception falla) when (falla is ObjectDisposedException or SocketException or InvalidOperationException)
        {
            // El banco bajó.
        }
    }
}
