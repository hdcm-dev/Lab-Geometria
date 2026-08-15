namespace GeometriaFactory.Contracts.Service;

/// <summary>
/// Cuerpo de la respuesta del punto de acceso `A-17`: si el laboratorio ya tiene administrador.
/// </summary>
/// <remarks>
/// POR QUÉ ESTE PUNTO EXISTE, Y POR QUÉ NO EXISTÍA ANTES. `Web ADR-03` §2 declara **cuatro
/// guardianes de ruta**, y el primero dice: «mientras no exista la cuenta de administrador,
/// cualquier ruta pedida desvía al aprovisionamiento inicial; una vez que existe, esa ruta deja de
/// armar formulario para siempre y desvía de forma neutra, sin explicar por qué». Ese guardián
/// **nunca se construyó**, y la causa no fue un olvido: la pieza pública **no tenía con qué
/// preguntar**. Revisada la superficie punto por punto, `A-03` configura —es escritura—, `A-16`
/// responde por la salud del servicio y `A-06` lista cuentas pero exige ser administrador. Ninguno
/// le sirve a un visitante anónimo. El faltante era de la especificación, y este tipo es la mitad
/// que la cierra del lado del contrato.
///
/// UN SOLO DATO, Y ES DELIBERADO. La respuesta **no lleva correo, ni nombre, ni fecha, ni cantidad
/// de cuentas**. Lo que el guardián necesita saber es si la ventana de alta está abierta, y nada
/// más; cualquier otro campo sería un dato del laboratorio expuesto a un anónimo sin que nadie lo
/// necesite.
///
/// SOBRE LO QUE ESTE PUNTO REVELA, Y POR QUÉ NO AFLOJA LA NEUTRALIDAD DE `ADR-03` §6.4. La ADR
/// exige que el desvío del aprovisionamiento ya resuelto **no explique por qué**. Este punto no la
/// contradice, y conviene ver el motivo exacto: **el dato ya es observable en cuanto el guardián
/// empieza a desviar**, porque el desvío mismo lo delata —quien pide `/aprovisionamiento-inicial`
/// y recibe un desvío en vez de un formulario ya sabe que hay administrador—. Lo que `ADR-03`
/// protege es **no explicar el motivo en el texto de la pantalla**, que es lo que convertiría una
/// deducción en un anuncio. Este punto **no agrega información que el guardián no exponga igual**.
///
/// Y POR QUÉ NO SE LE METIÓ ESTE DATO AL PUNTO DE SALUD, QUE ERA EL ATAJO. Porque la salud la
/// consume el chequeo del contenedor de `deploy/compose.yaml` y la comprobación del despliegue:
/// mezclarle un hecho del producto acopla dos cosas que cambian por motivos distintos, y el día
/// que este dato cambie de forma se movería el contrato de lo que decide si el contenedor está
/// vivo. Son dos preguntas y son dos puntos.
///
/// EL ESPACIO DE NOMBRES ES EL DE `ServiceHealth` y no el de las cuentas, y no es indiferente:
/// este tipo **no describe ninguna cuenta**, describe una propiedad de la instancia.
/// `Norma-De-Nomenclatura.md` §6.10 ya declara `GeometriaFactory.Contracts.Service` para «estado
/// del servicio», y §6.18 le da su fila a este tipo y a su única propiedad.
/// </remarks>
/// <param name="AdministratorConfigured">
/// Si la instancia ya tiene su única cuenta con papel `Administrator`. Es el **único** dato que
/// este cuerpo transporta.
/// </param>
public sealed record LaboratoryProvisioning(bool AdministratorConfigured);
