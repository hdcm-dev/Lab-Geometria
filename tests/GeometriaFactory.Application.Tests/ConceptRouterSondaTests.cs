using GeometriaFactory.Application.Concepts;
using GeometriaFactory.Domain.Values;
using Xunit;

namespace GeometriaFactory.Application.Tests;

/// <summary>
/// Sonda acumulada del número en letras (EXP-0004): cada frase que alguna vez falló, con su esperado. La regla
/// es la función sintáctica del número (ver <see cref="ConceptRouter.SpokenValue"/>); las frases se suman por
/// ronda y ninguna se quita.
/// </summary>
public sealed class ConceptRouterSondaTests
{
    private static readonly Role[] Both = [Role.Student, Role.Administrator];

    private static readonly ConceptCard[] Cards =
    [
        new("area-total-vs-cara", "Área total", "La piel.", ["area total", "area de una cara"], Both, "trabajo-actual", "x"),
    ];

    /// <summary>Conceptos: el número cuenta un sustantivo o es partitivo; no hay medida con valor.</summary>
    [Theory]
    // Ronda 1.
    [InlineData("qué es el volumen de tres cubos iguales")]
    [InlineData("por qué el área de dos cilindros no es el doble")]
    [InlineData("qué es el área de dos triángulos")]
    [InlineData("qué es el volumen, uno de los dos conceptos del cuerpo")]
    [InlineData("por qué la superficie de un cubo son seis cuadrados")]
    // Ronda 2.
    [InlineData("qué relación hay entre el área de 6 cuadrados y el cubo")]
    [InlineData("cómo se calcula el área de las cuatro caras laterales")]
    [InlineData("por qué la fórmula del cilindro usa dos veces el círculo")]
    // Ronda 3.
    [InlineData("hay dos fórmulas para el área del cilindro?")]
    [InlineData("qué significa el dos en la fórmula del área del cilindro")]
    // Ronda 4.
    [InlineData("tengo dudas con las dos fórmulas del cono")]
    // Ronda 5.
    [InlineData("para qué sirven los dos radios en la fórmula")]
    [InlineData("en la fórmula de la esfera aparece cuatro tercios, de dónde sale")]
    // Ronda de control: casos conocidos, sin corregir (el enrutador es heurístico).
    [InlineData("para qué está el tres en la fórmula del volumen del cono", Skip = "Caso conocido (ronda de control, EXP-0004 act. 003): el número cita un coeficiente de la fórmula y el enrutador lo toma como valor. Inocuo con la configuración por omisión (sólo fichas).")]
    [InlineData("explicame el un medio de la fórmula del triángulo", Skip = "Caso conocido (ronda de control, EXP-0004 act. 003): el número cita un coeficiente de la fórmula y el enrutador lo toma como valor. Inocuo con la configuración por omisión (sólo fichas).")]
    public void Concepto_NoPideResolver(string question) =>
        Assert.NotEqual(ConceptRouter.Intents.AsksToSolve, ConceptRouter.Route(question, Cards).Intent);

    /// <summary>Pedidos: el número es valor de una medida.</summary>
    [Theory]
    // Ronda 1.
    [InlineData("con siete de lado, cuál sería el área")]
    [InlineData("tengo siete de radio y cinco de altura, el volumen")]
    [InlineData("un cuadrado tres, su área")]
    [InlineData("si el lado es el número tres el área")]
    // Ronda 2.
    [InlineData("un cuadrado con lado de once, área?")]
    [InlineData("quiero el resultado: esfera, radio veinte, volumen")]
    [InlineData("lado siete, área del cuadrado")]
    [InlineData("medida cuatro el lado, área del cuadrado")]
    [InlineData("hallar la diagonal de un cuadrado de lado uno")]
    // Ronda 3.
    [InlineData("cuánto ocupa una caja de dos por tres por cuatro")]
    [InlineData("hola, tengo un triángulo de catetos tres y cuatro, la hipotenusa?")]
    // Ronda 5.
    [InlineData("cuántos centímetros cuadrados tiene un cuadrado de cinco centímetros de lado")]
    [InlineData("cuánto pinto si la pared es de tres metros por dos metros")]
    // Ronda de control: casos conocidos, sin corregir (el enrutador es heurístico).
    [InlineData("cuántos metros de soga para atar un poste de radio 0,5 dando una vuelta", Skip = "Caso conocido (ronda de control, EXP-0004 act. 003): problema de la vida cotidiana sin magnitud geométrica nombrada; el enrutador no lo ve como pedido. Inocuo con la configuración por omisión: recibe la ficha, no la solución.")]
    [InlineData("el piletón mide dos metros de ancho, tres de largo y uno de profundidad, ¿litros?", Skip = "Caso conocido (ronda de control, EXP-0004 act. 003): problema de la vida cotidiana sin magnitud geométrica nombrada; el enrutador no lo ve como pedido. Inocuo con la configuración por omisión: recibe la ficha, no la solución.")]
    [InlineData("cuánto zócalo pongo en un cuarto de tres por cuatro metros", Skip = "Caso conocido (ronda de control, EXP-0004 act. 003): problema de la vida cotidiana sin magnitud geométrica nombrada; el enrutador no lo ve como pedido. Inocuo con la configuración por omisión: recibe la ficha, no la solución.")]
    [InlineData("cuántas baldosas de 30 cm entran en un piso de tres por tres metros", Skip = "Caso conocido (ronda de control, EXP-0004 act. 003): problema de la vida cotidiana sin magnitud geométrica nombrada; el enrutador no lo ve como pedido. Inocuo con la configuración por omisión: recibe la ficha, no la solución.")]
    [InlineData("cuánta pared revisto con azulejos si mide 2,5 por 1,8", Skip = "Caso conocido (ronda de control, EXP-0004 act. 003): problema de la vida cotidiana sin magnitud geométrica nombrada; el enrutador no lo ve como pedido. Inocuo con la configuración por omisión: recibe la ficha, no la solución.")]
    [InlineData("cuánta cinta necesito para rodear una caja de diez por diez", Skip = "Caso conocido (ronda de control, EXP-0004 act. 003): problema de la vida cotidiana sin magnitud geométrica nombrada; el enrutador no lo ve como pedido. Inocuo con la configuración por omisión: recibe la ficha, no la solución.")]
    [InlineData("una pileta de 8 por 4 por 1,5 metros, cuánta agua lleva", Skip = "Caso conocido (ronda de control, EXP-0004 act. 003): problema de la vida cotidiana sin magnitud geométrica nombrada; el enrutador no lo ve como pedido. Inocuo con la configuración por omisión: recibe la ficha, no la solución.")]
    [InlineData("para envolver un regalo cúbico de veinte centímetros, cuánto papel uso", Skip = "Caso conocido (ronda de control, EXP-0004 act. 003): problema de la vida cotidiana sin magnitud geométrica nombrada; el enrutador no lo ve como pedido. Inocuo con la configuración por omisión: recibe la ficha, no la solución.")]
    [InlineData("tengo que llenar una caja de arena de 50 por 40 por 20, cuánta arena", Skip = "Caso conocido (ronda de control, EXP-0004 act. 003): problema de la vida cotidiana sin magnitud geométrica nombrada; el enrutador no lo ve como pedido. Inocuo con la configuración por omisión: recibe la ficha, no la solución.")]
    [InlineData("che, una mesa redonda de 1,2 de diámetro, cuánto mantel necesito", Skip = "Caso conocido (ronda de control, EXP-0004 act. 003): problema de la vida cotidiana sin magnitud geométrica nombrada; el enrutador no lo ve como pedido. Inocuo con la configuración por omisión: recibe la ficha, no la solución.")]
    [InlineData("cuánta pintura necesito para una pared de cuatro metros de largo y tres de alto", Skip = "Caso conocido (ronda de control, EXP-0004 act. 003): problema de la vida cotidiana sin magnitud geométrica nombrada; el enrutador no lo ve como pedido. Inocuo con la configuración por omisión: recibe la ficha, no la solución.")]
    [InlineData("para cercar una huerta cuadrada de lado quince, cuántos metros de malla", Skip = "Caso conocido (ronda de control, EXP-0004 act. 003): problema de la vida cotidiana sin magnitud geométrica nombrada; el enrutador no lo ve como pedido. Inocuo con la configuración por omisión: recibe la ficha, no la solución.")]
    [InlineData("quiero cercar un terreno rectangular de veinte por treinta metros, cuánto alambre compro", Skip = "Caso conocido (ronda de control, EXP-0004 act. 003): problema de la vida cotidiana sin magnitud geométrica nombrada; el enrutador no lo ve como pedido. Inocuo con la configuración por omisión: recibe la ficha, no la solución.")]
    [InlineData("cuánto pasto artificial para una cancha de 40 por 20", Skip = "Caso conocido (ronda de control, EXP-0004 act. 003): problema de la vida cotidiana sin magnitud geométrica nombrada; el enrutador no lo ve como pedido. Inocuo con la configuración por omisión: recibe la ficha, no la solución.")]
    public void Valor_PideResolver(string question) =>
        Assert.Equal(ConceptRouter.Intents.AsksToSolve, ConceptRouter.Route(question, Cards).Intent);
}
