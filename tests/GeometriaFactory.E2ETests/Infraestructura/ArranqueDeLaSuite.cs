using NUnit.Framework;

namespace GeometriaFactory.E2ETests;

/// <summary>
/// Lee la configuración de la corrida antes de la primera prueba.
/// </summary>
/// <remarks>
/// EL NAMESPACE ES EL DE LAS PRUEBAS Y NO UNO ANIDADO, aunque el archivo viva en
/// `Infraestructura/`. Un `[SetUpFixture]` cubre su propio namespace y los que cuelgan de él,
/// nunca el de arriba: declarado en `...E2ETests.Infraestructura` NO CORRERIA para las pruebas de
/// `...E2ETests`, y el síntoma es desconcertante —la dirección base llega vacía y el navegador se
/// queja de otra cosa—. Es el detalle que la guía marca como «el que cuesta una tarde».
/// </remarks>
[SetUpFixture]
public sealed class ArranqueDeLaSuite
{
    [OneTimeSetUp]
    public async Task PrepararAsync()
    {
        ElLaboratorio.Leer();
        await ElLaboratorio.ExigirQueElLaboratorioRespondaAsync();
    }
}
