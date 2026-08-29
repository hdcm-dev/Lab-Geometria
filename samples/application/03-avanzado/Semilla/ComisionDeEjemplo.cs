using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Values;
using GeometriaFactory.Samples.Application.Avanzado.Dobles;

namespace GeometriaFactory.Samples.Application.Avanzado.Semilla;

/// <summary>
/// La comisión con la que corre el recorrido: **un administrador, tres alumnos y cuatro trabajos
/// en los cuatro estados**.
/// </summary>
/// <remarks>
/// LOS CUATRO ESTADOS ESTÁN A PROPÓSITO, y no para llenar: el acto `[2]` mide **cuántos ve el
/// administrador** —tres de cuatro, porque `RN-04011` deja el borrador afuera— y el `[3]` necesita
/// uno en `Pendiente` para resolver y uno ya `Aprobado` para que el desenlace sobre terminal se
/// pueda provocar.
///
/// **Nada se fabrica a mano**: las cuentas se constituyen por el dominio y los trabajos también,
/// de modo que la semilla no puede producir un estado que las reglas no admitan.
/// </remarks>
internal sealed class ComisionDeEjemplo
{
    internal Account Administrador { get; private set; } = null!;

    internal Account Alumna { get; private set; } = null!;

    /// <summary>La que el acto `[1]` da de baja, con sus dos trabajos que se arrastran.</summary>
    internal Account AlumnaBloqueada { get; private set; } = null!;

    /// <summary>
    /// **Una segunda cuenta bloqueada, y hace falta que sean dos.** El acto `[1]` da de baja a la
    /// primera y el `[4]` resetea sobre una bloqueada: con una sola, el reseteo cae sobre una
    /// cuenta que ya no existe. La primera versión de este sample tenía una sola y el acto `[4]`
    /// devolvía `ACCOUNT_NOT_FOUND`.
    /// </summary>
    internal Account AlumnaBloqueadaParaReseteo { get; private set; } = null!;

    internal Account AlumnaPendiente { get; private set; } = null!;

    internal Work EnBorrador { get; private set; } = null!;

    internal Work EnPendiente { get; private set; } = null!;

    internal Work Aprobado { get; private set; } = null!;

    internal Work Rechazado { get; private set; } = null!;

    internal static async Task<ComisionDeEjemplo> SembrarAsync(
        RepositorioDeCuentasEnMemoria cuentas, RepositorioDeTrabajosEnMemoria trabajos)
    {
        var m = RelojFijo.Momento;
        var c = new ComisionDeEjemplo();

        c.Administrador = Account.ConfigureAdministrator("docente@frre.utn.edu.ar", "Docente",
            "Titular", "hash-inicial", true, true, AccountStatus.Enabled, m).Value!;
        c.Alumna = Habilitada("alumna@frre.utn.edu.ar", "Alumna", "Ejemplo", m);
        c.AlumnaBloqueada = Habilitada("bloqueada@frre.utn.edu.ar", "Bloqueada", "Ejemplo", m);
        c.AlumnaBloqueada.Block();
        c.AlumnaBloqueadaParaReseteo = Habilitada("reseteo@frre.utn.edu.ar", "Reseteo", "Ejemplo", m);
        c.AlumnaBloqueadaParaReseteo.Block();
        c.AlumnaPendiente = Account.Register("pendiente@frre.utn.edu.ar", "Pendiente", "Ejemplo",
            null, true, Role.Student, AccountStatus.Pending, m).Value!;

        foreach (var cuenta in new[] { c.Administrador, c.Alumna, c.AlumnaBloqueada,
                                       c.AlumnaBloqueadaParaReseteo, c.AlumnaPendiente })
        {
            cuentas.Agregar(cuenta);
        }

        c.EnBorrador = Trabajo(c.Alumna.Id, "En borrador", WorkStatus.Draft, m);
        c.EnPendiente = Trabajo(c.Alumna.Id, "En pendiente", WorkStatus.Submitted, m);
        c.Aprobado = Trabajo(c.Alumna.Id, "Aprobado", WorkStatus.Approved, m);
        c.Rechazado = Trabajo(c.Alumna.Id, "Rechazado", WorkStatus.Rejected, m);

        foreach (var t in new[] { c.EnBorrador, c.EnPendiente, c.Aprobado, c.Rechazado })
        {
            await trabajos.AddAsync(t);
        }

        // Los dos trabajos de la alumna bloqueada, que el acto [4] cuenta para verificar que el
        // reseteo NO los arrastra.
        foreach (var nombre in new[] { "Suyo uno", "Suyo dos" })
        {
            await trabajos.AddAsync(Trabajo(c.AlumnaBloqueada.Id, nombre, WorkStatus.Submitted, m));
            await trabajos.AddAsync(Trabajo(c.AlumnaBloqueadaParaReseteo.Id, nombre, WorkStatus.Submitted, m));
        }

        return c;
    }

    private static Account Habilitada(string correo, string nombre, string apellido, DateTimeOffset m)
    {
        var a = Account.Register(correo, nombre, apellido, null, true, Role.Student,
            AccountStatus.Pending, m).Value!;
        a.Enable("hash-de-la-provisoria");
        a.ReplaceCredential("hash-elegido", currentCredentialVerified: true);
        return a;
    }

    private static Work Trabajo(Guid dueño, string nombre, WorkStatus estado, DateTimeOffset m)
    {
        var w = Work.Create(dueño, nombre, "2026-08-29", null, "[]", true, m).Value!;
        if (estado == WorkStatus.Draft) return w;

        w.Submit(parseResultDeclared: true, validationErrorsDeclared: false, updatedAt: m);
        if (estado == WorkStatus.Submitted) return w;

        w.ApplyOutcome(Role.Administrator,
            estado == WorkStatus.Approved ? WorkOutcome.Approve : WorkOutcome.Reject, null, m);
        return w;
    }
}
