namespace GeometriaFactory.Contracts.Works;

/// <summary>
/// La ÚNICA solicitud de guardado que el contrato le declara al alumno (`Contracts CU-03` §4).
/// </summary>
/// <remarks>
/// CUATRO CAMPOS Y UN IDENTIFICADOR. Los cuatro son los que la persona carga —nombre, fecha,
/// descripción y el texto—; el identificador viaja para que la reedición pueda referirse a un
/// trabajo ya constituido, y en el alta llega sin valor.
///
/// EL TEXTO ES **UNA SOLA CADENA** Y EL TIPO NO LE IMPONE FORMA (`CU-03` CA-01). No hay campo de
/// pieza, de componente, de valor derivado, de observación ni de estado pretendido: **el estado
/// lo decide la interpretación y no el consumidor**. Que el tipo no los declare es lo que lo hace
/// imposible.
///
/// Y LA CADENA VIAJA EXACTA. El texto que el alumno pega no es notación de objetos estrictamente
/// válida —trae comas finales y claves que un lector ingenuo rechaza— y **no se normaliza en el
/// borde**: no se recodifica, no se recortan espacios, no se normalizan saltos de línea y no se
/// reserializa (RN-08; `Contratos-REST.md` §2). Las tres formas de romperlo dejan el sistema
/// funcionando, que es lo que las vuelve caras.
///
/// EL IDENTIFICADOR DE LA RUTA GOBIERNA SOBRE EL DEL CUERPO, con el mismo criterio con el que la
/// etapa `d` lo resolvió para las tres solicitudes de cuenta: el contrato lo declara porque es el
/// mismo tipo para los dos extremos, y el punto de acceso usa el de la ruta, para que no haya un
/// lugar donde los dos puedan no coincidir.
/// </remarks>
/// <param name="WorkId">Identidad del trabajo en la reedición. Sin valor en el alta.</param>
/// <param name="Name">Título que el alumno le da a su trabajo.</param>
/// <param name="DeclaredDate">
/// Fecha que el alumno declara. **Viaja como texto y no se convierte de zona**: la escribe la
/// persona y no es un sello del sistema (`Modelo-Datos-Logico.md` §2.2, `RC-06`).
/// </param>
/// <param name="Description">Texto libre con el que explica qué modeló. Admite vacío.</param>
/// <param name="OriginalJson">El texto que el alumno pegó, tal como lo emitió su programa.</param>
public sealed record WorkSubmissionRequest(
    Guid? WorkId,
    string Name,
    string DeclaredDate,
    string? Description,
    string OriginalJson);
