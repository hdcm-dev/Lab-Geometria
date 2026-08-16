namespace GeometriaFactory.Domain.Values;

/// <summary>
/// Qué es un componente respecto de su pieza. Conjunto cerrado del **vocabulario del emisor**.
/// </summary>
/// <remarks>
/// LOS CINCO VALORES SALEN DE LAS CLAVES QUE EL TEXTO DEL ALUMNO USA, y no de una taxonomía que el
/// producto haya elegido: `Tapas` en el cilindro, `Caras` en el cubo, `Bases` y `Laterales` en el
/// ortoedro, y `Lado` en el cilindro (`Definicion-Modelo-De-Dominio.md` §2.4).
///
/// <see cref="Lateral"/> Y <see cref="Side"/> NO SON EL MISMO PAPEL, y la distinción se conserva
/// porque el emisor la hace: `Laterales` son las cuatro caras verticales del ortoedro y `Lado` es
/// el único rectángulo desarrollado del cilindro.
///
/// `Base` Y `Lateral` SON HOMÓNIMOS DECLARADOS: el castellano y el inglés coinciden
/// (`Norma-De-Nomenclatura.md` §6.21.2).
/// </remarks>
public enum ComponentRole
{
    /// <summary>Etiqueta: «Tapa». Clave `Tapas` del cilindro.</summary>
    Cap = 1,

    /// <summary>Etiqueta: «Cara». Clave `Caras` del cubo.</summary>
    Face = 2,

    /// <summary>Etiqueta: «Base». Clave `Bases` —o `Tapas`, que T1 acepta— del ortoedro.</summary>
    Base = 3,

    /// <summary>Etiqueta: «Lateral». Clave `Laterales` del ortoedro.</summary>
    Lateral = 4,

    /// <summary>Etiqueta: «Lado». Clave `Lado` del cilindro, y el único que no es un conjunto.</summary>
    Side = 5
}
