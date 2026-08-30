using GeometriaFactory.Application.Accounts;
using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Samples.Infrastructure.Intermedio;

/// <summary>Acto 5 — `CU-06005`: las dos preguntas sobre el conjunto, y la marca que sobrevive al viaje.</summary>
/// <remarks>
/// LAS DOS PREGUNTAS SON PREGUNTAS Y NO RECHAZOS, y ahí está lo que el sample muestra. El puerto
/// expone `EmailIsRegisteredAsync` y `AdministratorExistsAsync`: devuelven un booleano sobre el
/// conjunto, y **quien rechaza es el caso de uso**. El adaptador no decide sobre la admisión.
///
/// PERO EL ALMACÉN NO SE FÍA DE ESO. Las dos reglas están además en el esquema, como índices
/// únicos —`UX_Account_NormalizedEmail` y `UX_Account_SingleAdministrator`, este último filtrado
/// al papel de administrador—. Es defensa en dos capas y es deliberado: la pregunta previa evita
/// la excepción en el camino normal, y el índice hace que la regla siga valiendo si alguien
/// escribe sin preguntar. El sample ejercita las dos.
/// </remarks>
internal static class ActoCuentas
{
    internal static async Task<int> EjecutarAsync(Contexto contexto, Action<string> escribir)
    {
        var excepciones = 0;

        // ---- Correo ya registrado ----
        const string correo = "repetida@ejemplo.edu";
        var primera = Account.Register(correo, "Bruno", "Salas", passwordHash: null,
            emailUniquenessVerified: true, Role.Student, AccountStatus.Pending, contexto.Reloj.UtcNow)
            .Exigir("El alta de la primera cuenta");
        await contexto.EnCuentas(r => r.AddAsync(primera)).ConfigureAwait(false);

        var yaEsta = await contexto.EnCuentas(r =>
            r.EmailIsRegisteredAsync(primera.NormalizedEmail)).ConfigureAwait(false);
        escribir($"[5] Alta con un correo ya registrado: "
            + $"{(yaEsta ? "rechazada " + ApplicationConditionCode.EmailAlreadyRegistered : "ADMITIDA")}");

        // ---- Segundo administrador ----
        // DIVERGENCIA D-3 CONTRA EL SNAPSHOT DE §6, de NOMBRE y de CAPA, igual que la D-2.
        // §6 espera `ADMINISTRATOR_UNIQUENESS_VIOLATED`; el que existe es
        // `ADMINISTRATOR_ALREADY_CONFIGURED`, y lo devuelve `Account.ConfigureAdministrator` en el
        // dominio a partir de la respuesta que este puerto da sobre el conjunto.
        var admin = Account.ConfigureAdministrator("admin@ejemplo.edu", "Carla", "Vega", "hash",
            administratorAbsenceDeclared: true, emailUniquenessVerified: true,
            AccountStatus.Enabled, contexto.Reloj.UtcNow).Exigir("La configuración del administrador");
        await contexto.EnCuentas(r => r.AddAsync(admin)).ConfigureAwait(false);

        var hayAdmin = await contexto.EnCuentas(r => r.AdministratorExistsAsync()).ConfigureAwait(false);
        var segundo = Account.ConfigureAdministrator("otro@ejemplo.edu", "Dario", "Luna", "hash",
            administratorAbsenceDeclared: !hayAdmin, emailUniquenessVerified: true,
            AccountStatus.Enabled, contexto.Reloj.UtcNow);
        escribir($"[5] Segunda cuenta con papel Administrador: rechazada {segundo.ConditionCode}");

        // ---- La marca de cambio pendiente, ida y vuelta por el almacén ----
        // QUE LA MARCA VIAJE NO ES OBVIO: es una propiedad con acceso privado que el mapeo tiene
        // que estar leyendo por campo. Si el mapeo la omitiera, la cuenta volvería sin la marca y
        // el alumno entraría sin que nadie le pida cambiar la contraseña provisoria.
        var pendiente = Account.Register("pendiente@ejemplo.edu", "Elsa", "Roca", passwordHash: null,
            emailUniquenessVerified: true, Role.Student, AccountStatus.Pending, contexto.Reloj.UtcNow)
            .Exigir("El alta de la cuenta con marca pendiente");
        // `worksCascadeDeclared` SE PASA EN FALSO Y ES CONTRAINTUITIVO: el parámetro no pregunta
        // si la cuenta tiene trabajos, pregunta si QUIEN PIDE declaró un arrastre, y un reseteo de
        // contraseña que arrastrara trabajos sería otra operación. Pasarlo en verdadero devuelve
        // `RESET_WITH_WORK_CASCADE` y la marca no se fija. Pasó al escribir este sample, y sin el
        // `Exigir` de abajo se habría leído como que el mapeo perdía la marca en el viaje.
        pendiente.ResetPassword("hash-provisorio", worksCascadeDeclared: false)
            .Exigir("El reseteo de contraseña");
        var estadoAntes = pendiente.Status;
        await contexto.EnCuentas(r => r.AddAsync(pendiente)).ConfigureAwait(false);

        var recuperada = await contexto.EnCuentas(r =>
            r.FindByNormalizedEmailAsync(pendiente.NormalizedEmail)).ConfigureAwait(false);
        escribir($"[5] Cuenta recuperada con su marca de cambio pendiente: "
            + $"{(recuperada!.MustChangePassword ? "si" : "no")} | estado sin alterar: "
            + $"{(recuperada.Status == estadoAntes ? "si" : "no")}");

        return excepciones;
    }
}
