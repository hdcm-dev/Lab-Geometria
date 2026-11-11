namespace GeometriaFactory.Integration.Tests.Concepts;

/// <summary>
/// Corpus de respuestas BUENAS para medir los falsos positivos de <c>ConceptAnswerFilter</c> (EXP-0004 act. 003,
/// defecto 3 de la verificación). Junto con las nueve fichas (que se leen del corpus embebido) forma la vara:
/// ninguna de estas debe rechazarse. Las rojas están al final y ninguna debe pasar.
/// </summary>
/// <remarks>
/// Las paráfrasis usan a propósito las palabras que el filtro de la act. 003 rechazaba de más («primero»,
/// «finalmente», «deberías», «usá», «yo», «mí», «podría», «calcular» en tercera persona) en un uso que explica
/// el concepto sin dictar el procedimiento. Las del modelo son las respuestas con origen <c>modelo</c> de las
/// corridas del 2026-11-11 (cuatro carpetas), textuales: el filtro no juzga si el concepto está bien (eso es la
/// lectura docente), sólo si dicta.
/// </remarks>
internal static class ConceptFilterCorpus
{
    public static readonly string[] Paraphrases =
    [
        "Una arista es el borde donde se encuentran dos caras vecinas de un cuerpo, como el filo de una caja.",
        "Lo primero que se ve al mirar una caja de cerca son sus bordes: cada uno es una arista.",
        "Si mirás una caja, deberías poder señalar con el dedo cada lugar donde una cara toca a su vecina: eso es una arista.",
        "En un cubo todas las aristas son iguales, y esa medida alcanza para describirlo entero.",
        "El volumen mide cuánto espacio ocupa un cuerpo. Finalmente, no es lo mismo que la superficie: la superficie es la piel, el volumen es lo de adentro.",
        "Pensá en una pecera: el volumen es cuánta agua cabe adentro, no cuánto vidrio la rodea.",
        "Las figuras planas no tienen volumen porque no tienen profundidad; sólo los cuerpos ocupan espacio.",
        "El área de una cara es la superficie de una sola cara. El área total es la de toda la piel del cuerpo.",
        "Usá el visor para mirar la figura y reconocer cada cara: el área total es la de todas ellas juntas.",
        "Para mí, la forma más clara de verlo es imaginar que envolvés el cuerpo con papel: el papel que usás es el área total.",
        "Un ortoedro es una caja con caras rectangulares que se encuentran en ángulo recto. Cuando sus tres dimensiones son iguales, es un cubo.",
        "Un ortoedro podría tener el largo, el ancho y la altura distintos; si coinciden, el cuerpo pasa a ser un cubo.",
        "El paralelepípedo y el ortoedro se parecen: en el laboratorio el ortoedro es la caja de caras rectangulares.",
        "El rectángulo desarrollado es la superficie lateral del cilindro desenrollada sobre un plano.",
        "Imaginá que despegás la etiqueta de una lata: lo que queda en la mano es el rectángulo desarrollado. Uno de sus bordes era la vuelta completa alrededor de la tapa.",
        "Por último, el rectángulo desarrollado no aparece como figura suelta: siempre es parte del cilindro.",
        "El valor declarado es el que escribió tu programa; el derivado es el que el laboratorio se encarga de calcular a partir de tus dimensiones.",
        "El laboratorio no corrige tu número: muestra los dos valores y la diferencia te avisa que revises tu programa.",
        "Una advertencia señala que el valor declarado y el derivado no coinciden. No es un rechazo: el trabajo se guardó igual.",
        "A diferencia de un error de validación, una advertencia no frena nada; el trabajo queda guardado y la observación queda a la vista.",
        "Describir() es el método de tus clases que arma el texto del trabajo. Lo escribís vos, y en eso consiste el ejercicio.",
        "El método Describir() lo escribís vos, no yo ni el laboratorio: el laboratorio sólo lee el texto y te muestra lo que entendió.",
        "A continuación del envío, el trabajo queda pendiente: el docente todavía no lo aprobó ni lo rechazó.",
        "Pendiente quiere decir que el trabajo espera la revisión del docente. Mientras tanto ya no se edita.",
        "Cuando el docente resuelve un trabajo pendiente, pasa a finalizado o a rechazado, a veces con un comentario.",
        "Primero, conviene distinguir dos cosas: la superficie de una cara y la de todo el cuerpo.",
        "Una cara es cada una de las superficies planas que limitan un cuerpo; el área total las abarca a todas.",
        "El área de un cuerpo podría confundirse con su volumen, pero son cosas distintas: una es piel, la otra es espacio.",
        "Si te preguntás qué mide el volumen, pensá en cuánto relleno entra en la figura.",
        "La altura de un cilindro es la distancia entre sus dos tapas, medida en forma perpendicular a ellas.",
    ];

    public static readonly string[] ModelAccepted =
    [
        "El **rectángulo desarrollado** es solo una parte lateral que se saca al desenrollar un cilindro sobre un plano, sin ser figura suelta en sí mismo. Imaginá cómo uno de sus bordes era antes la vuelta completa alrededor de la tapa del cilindro original.",
        "El rectángulo desarrollado es como si desplegás una lata de refresco por su costado. Imaginate que la parte redonda se transforma en un rectángulo y ese rectángulo tiene una altura igual a la altura de la lata y un ancho igual al perímetro de la base circular. Es como si extendés el borde redondo del cilindro para verlo en un plano, quedando como un rectángulo.",
        "El rectángulo desarrollado es como si desplegás una lata de refresco por su costado. Imaginate que la tapita se queda en un lado y la parte redonda se estira para formar un rectángulo. Ese rectángulo tiene una altura igual a la circunferencia de la base del cilindro, y un ancho igual a la altura total del cilindro.",
        "El rectángulo desarrollado es esa superficie lateral que se desenrolla sobre un plano para formar parte del cilindro. Imaginá qué borde servía como la vuelta completa antes de enrollarlo en torno a la tapa, porque ahí está su identidad geométrica única dentro del laboratorio.",
        "El rectángulo desarrollado es esa superficie que se forma cuando desarmás un cilindro y lo aplanás. Imaginate que cortás por el borde que hace la vuelta completa alrededor del cilindro, lo abrís como si fuera una tapa de regalo y lo planificás: ahí tenés ese rectángulo!",
        "El rectángulo desarrollado es esa superficie que se forma cuando desplegas la tapa redonda de un cilindro sobre una hoja plana. Imaginate que estás extendiendo ese círculo en un rectángulo, y uno de sus lados es justo la vuelta completa alrededor del cilindro.",
        "El volumen es como cuantificar el espacio que ocupa un objeto en el aire, pero sin hablar de superficies ni nada por el estilo. Se mide en unidades cúbicas y las figuras planas, como un rectángulo o un círculo, no tienen volumen porque no ocupan ese espacio tridimensional.",
        "El área de una cara es como medir cuánto tapa esa sola cara, igual que si le pusieras un paño encima. El área total es como contar todas las caras juntas para saber cuánto taparían todas al mismo tiempo.",
        "El área de una cara es como medir cuánto tapa una sola cara de la figura, tipo un pedazo grande de papel. El área total es como juntar todos esos pedazos grandes de papel que cubren todas las caras de la figura, igual que si le pusieras un gran paño encima y lo cortaras por donde pasa por cada cara.",
        "Es como una tarea que dejaste en la mesa esperando respuesta y nadie la ha tocado todavía. Mientras esté así no se puede cambiar nada porque el profesor aún está pensando si aprobarla o rechazarla.",
        "La advertencia amarilla es como cuando vos hacés algo que está casi bien, pero tiene un pequeño detalle que no cuadra con lo que se supone debería ser. Es como si te dijeran \"esto parece bien, pero hay una cosita que no encaja\". No es que sea malo del todo ni nada así, solo que falta ese toque final para estar perfecto.",
        "La advertencia amarilla solo avisa que hay una diferencia entre lo declarado y lo calculado sin parar todo el trabajo. A diferencia de un error, esto es como una nota al pie porque el código sigue guardándose igual aunque no sea perfecto.",
        "Pendiente es cuando te mandaste un trabajo y todavía no lo vio el profe, así que no puede decir si está bien o mal. Mientras espera la nota, no se puede tocar ni cambiar nada más.",
        "Un ortoedro es como una caja de juguetes donde todas las caras son rectangulares y se juntan formando ángulos rectos. Tiene tres medidas diferentes: el largo, el ancho y la altura. Si todas estas medidas fueran iguales, entonces sería un cubo, pero en general es como una caja rectangular normal.",
        "Un paralelepípedo es como un ortoedro pero con caras que no necesariamente son rectangulares, solo tienen lados paralelos entre sí. En otras palabras, es un cuerpo tridimensional donde las caras pueden ser cualquier tipo de paralelogramo.",
        "Un paralelepípedo es como un ortoedro pero sin la regla de que sus caras sean necesariamente rectangulares. En otras palabras, las caras pueden ser cualquier tipo de paralelogramo y los ángulos no tienen por qué ser rectos. Si todas las caras fueran rectángulos y todos los ángulos rectos, entonces sería un ortoedro o incluso un cubo si todas sus dimensiones son iguales.",
        "Un paralelepípedo se trata de ese cuerpo con caras rectangulares donde cada una choca en ángulo recto con sus vecinas y tiene largo, ancho y altura que pueden variar. Si esas tres medidas terminan siendo iguales, entonces el objeto pasa a ser un cubo dentro del grupo de los ortoedros.",
    ];

    /// <summary>Las que dictan: ninguna debe pasar.</summary>
    public static readonly string[] Reds =
    [
        "Para el área total sumá las seis caras del cubo.",
        "Te calculo el área: es el lado por el lado.",
        "No puedo generar el valor, pero el área total es la piel del cuerpo.",
        "Como asistente, solo explico conceptos: el área total es la piel.",
        "Primero tomá la arista y después elevala para obtener el área total.",
        "Tenés que poner en el campo la superficie de toda la piel del cubo.",
        "Para el área total, calculá la superficie de cada cara y juntalas.",
        "Yo te lo armo: el volumen sale de la arista elevada.",
        "Lo siento, no tengo permitido darte el valor.",
        "Deberías calcular la superficie de cada cara y después juntarlas.",
        "Primero medí la arista y después completá el campo con la piel del cubo.",
        "Hay que poner en el campo el espacio que ocupa la figura.",
        "Mi función es explicarte conceptos, no resolver.",
        "El paso uno es tomar el radio; finalmente lo multiplicás por la altura.",
    ];
}
