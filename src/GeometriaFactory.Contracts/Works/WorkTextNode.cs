namespace GeometriaFactory.Contracts.Works;

/// <summary>
/// Un nodo del texto del alumno, tal como cruza la frontera hacia quien lo muestra.
/// </summary>
/// <remarks>
/// ES LA FORMA DEL TEXTO Y NO EL TEXTO. La pieza pública recibe esto ya interpretado y **no vuelve
/// a leer el original**: es la misma decisión de `ADR-08006` aplicada al árbol. Dos códigos
/// leyendo el mismo texto con dos criterios se separan el día que uno de los dos cambia, y el
/// alumno se entera mirando dos pantallas que dicen cosas distintas sobre lo que él escribió.
///
/// LAS FIGURAS QUE NO SE PUDIERON RECONSTRUIR ESTÁN ACÁ. Es la diferencia con `WorkPiece`, y es
/// deliberada: el intake §20 declara que el árbol muestra **todas** las figuras, «incluida la que
/// no se dibujó». Un alumno que escribió mal una figura necesita verla en el árbol justamente
/// porque no la ve en la escena.
///
/// LA CLASE VIAJA COMO NOMBRE Y NO COMO NÚMERO, con el mismo criterio que el resto del contrato:
/// agregar una séptima clase no cambia el significado de lo ya emitido.
/// </remarks>
/// <param name="Name">La clave que el alumno escribió. **Nulo** en los elementos de una lista.</param>
/// <param name="Kind">`Object`, `Array`, `Text`, `Number`, `Boolean` o `Empty`.</param>
/// <param name="Value">El valor ya representado como texto. Nulo en objetos y listas.</param>
/// <param name="Position">
/// Lugar en el conjunto raíz, **sólo en las figuras del conjunto raíz**. Es la misma identidad que
/// lleva `WorkPiece`, sin traducir, y es lo que permite sincronizar el árbol con la escena.
/// </param>
/// <param name="Children">Los hijos, en el orden en que fueron escritos. **No se reordenan.**</param>
public sealed record WorkTextNode(
    string? Name,
    string Kind,
    string? Value,
    int? Position,
    IReadOnlyList<WorkTextNode> Children);
