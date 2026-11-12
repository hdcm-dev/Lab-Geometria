namespace GeometriaFactory.Application.Concepts;

/// <summary>Forma de la respuesta (catálogo F1–F8 del diseño conversacional).</summary>
public enum ConceptForm
{
    /// <summary>F1: una cifra con origen en un caso de uso y su nota.</summary>
    Figure = 1,

    /// <summary>F2: un total con el desglose ofrecido.</summary>
    Total = 2,

    /// <summary>F4: pasos o ficha desde la ayuda.</summary>
    FromHelp = 4,

    /// <summary>F5: abstención, «No está en la ayuda de conceptos».</summary>
    Abstention = 5,

    /// <summary>F6: negativa útil (qué no, qué sí, a dónde ir).</summary>
    UsefulRefusal = 6,

    /// <summary>F7: afuera de la frontera.</summary>
    Outside = 7,

    /// <summary>F8: la redacción con IA está apagada; va la ficha.</summary>
    AiOff = 8,

    /// <summary>El trabajo de la pantalla no existe o no es de quien pregunta.</summary>
    NotFound = 9,

    /// <summary>Compuesta: una respuesta por parte, cada una con su forma (juicio R1).</summary>
    Compound = 10,
}

/// <summary>Anillo de la frontera de competencia.</summary>
public enum ConceptRing
{
    /// <summary>Operar el producto.</summary>
    Operate = 1,

    /// <summary>Conceptos del dominio.</summary>
    Concepts = 2,

    /// <summary>Lo de al lado (depurar, serializar). Diferido (D-4): responde con texto fijo.</summary>
    Adjacent = 3,

    /// <summary>Afuera, inyección y todo pedido de resolver.</summary>
    Outside = 4,
}
