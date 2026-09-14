using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Infrastructure.Persistence;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Xunit;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// R-10 — UNA TRANSFORMACIÓN DE ESQUEMA SE APLICA SOBRE UN ALMACÉN QUE YA TIENE DATOS, y no sólo
/// sobre uno vacío (mesa `SDD/Docs/Audit/Mesa-2026-09-14.md`).
/// </summary>
/// <remarks>
/// POR QUÉ HACÍA FALTA. Las transformaciones se aplican solas al arrancar, y hasta esta prueba
/// ninguna se había ejercitado sobre filas existentes: el almacén del laboratorio desplegado
/// existe desde el 2026-09-06 y la primera transformación posterior es ésta. Si fallara sobre
/// datos, el servicio arrancaría con la salud en «no listo», en plena clase.
///
/// CÓMO LO MIDE. Lleva el almacén hasta la transformación ANTERIOR, escribe una cuenta con SQL
/// directo —el estado que dejó en disco una versión anterior del producto—, aplica el resto y
/// comprueba que la fila sigue intacta, que la columna nueva quedó nula y que el modelo actual
/// la lee.
/// </remarks>
public sealed class CredentialChangedAtMigrationTests : IDisposable
{
    private const string PreviousMigration = "20260816230145_PieceOwnDimensions";

    private readonly string _storePath = DataServiceHarness.ReserveStorePath();

    public void Dispose() => DataServiceHarness.DiscardStore(_storePath);

    private GeometriaFactoryDbContext ContextOverStore() =>
        new(new DbContextOptionsBuilder<GeometriaFactoryDbContext>()
            .UseSqlite($"Data Source={_storePath}")
            .Options);

    [Fact]
    public async Task TheColumnIsAddedOverAnExistingAccountWithoutTouchingIt()
    {
        var id = Guid.Parse("6f1d3c2a-0b7e-4f1a-9c3d-2e5b8a7c4d10");

        await using (var before = ContextOverStore())
        {
            await before.GetService<IMigrator>().MigrateAsync(PreviousMigration);
            Assert.Contains(
                "20260816230145_PieceOwnDimensions",
                await before.Database.GetAppliedMigrationsAsync());

            await before.Database.ExecuteSqlRawAsync(
                """
                INSERT INTO "Account" ("Id", "Email", "NormalizedEmail", "FirstName", "LastName",
                                       "Role", "Status", "PasswordHash", "MustChangePassword", "CreatedAt")
                VALUES ({0}, 'alumna@frre.utn.edu.ar', 'alumna@frre.utn.edu.ar', 'Ana', 'Diaz',
                        'Student', 'Enabled', 'derivado-anterior', 0, '2026-09-06 10:00:00+00:00')
                """,
                id.ToString().ToUpperInvariant());
        }

        await using (var after = ContextOverStore())
        {
            Assert.NotEmpty(await after.Database.GetPendingMigrationsAsync());

            await after.Database.MigrateAsync();

            Assert.Empty(await after.Database.GetPendingMigrationsAsync());

            var account = await after.Set<Account>().AsNoTracking().SingleAsync();
            Assert.Equal(id, account.Id);
            Assert.Equal("alumna@frre.utn.edu.ar", account.Email);
            Assert.Equal("derivado-anterior", account.PasswordHash);
            Assert.False(account.MustChangePassword);
            Assert.Null(account.CredentialChangedAt);
        }

        // Y LA COLUMNA EXISTE EN DISCO, nula, leída sin pasar por el modelo.
        await using var connection = new SqliteConnection($"Data Source={_storePath};Pooling=False");
        await connection.OpenAsync();
        await using var command = connection.CreateCommand();
        command.CommandText = """SELECT COUNT(*) FROM "Account" WHERE "CredentialChangedAt" IS NULL""";
        Assert.Equal(1L, (long)(await command.ExecuteScalarAsync())!);
    }
}
