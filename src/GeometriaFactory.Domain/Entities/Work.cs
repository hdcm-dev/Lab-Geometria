using GeometriaFactory.Domain.Guards;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Domain.Entities;

/// <summary>
/// Trabajo entregado por un alumno. Una fila por entrega, con dueño, identidad propia y estado.
/// </summary>
/// <remarks>
/// ETAPA `e` (`Domain BT-06` para los atributos y `Domain BT-12` para la máquina de estados): la
/// entidad se modela con los atributos de `Definicion-Modelo-De-Dominio.md` §2.2 y con las cinco
/// operaciones que las capacidades de esta etapa ejercen —constituir (CU-05), reeditar (CU-05
/// FA-01), enviar (CU-08), resolver el acceso del alumno (CU-09) y resolver el alcance del
/// administrador (CU-11)—, más el desenlace (CU-10) que `BT-12` incluye en su criterio.
///
/// EL TEXTO NO SE INTERPRETA ACÁ, Y NO SE INTERPRETA EN NINGÚN LADO TODAVÍA. `Piece`,
/// `Component` y `Observation` siguen sin atributos: son de la etapa `f`, que es la que construye
/// el validador de figuras. La consecuencia observable, y es la que el roadmap pide para la
/// transición `e` → `f`: **en esta etapa TODO trabajo queda en `Draft`**, porque
/// <see cref="Submit"/> exige el resultado de una interpretación que todavía no existe y lo
/// rechaza con `SUBMISSION_WITHOUT_PARSE_RESULT`. `Borrador` significa exactamente eso —«el texto
/// todavía no verificó, o el trabajo recién se creó»— y el alumno reedita cuantas veces quiera.
///
/// EL DOMINIO NO LEE NINGÚN RELOJ (`Domain ADR-06`): los dos sellos —creación y última
/// modificación— **los aporta el consumidor** en cada operación que escribe, igual que la fecha
/// de alta de <see cref="Account"/> (intake §17.3.P.4).
///
/// Y LOS TRES TIEMPOS NO SE CONFUNDEN: <see cref="DeclaredDate"/> **la escribe el alumno** y por
/// eso viaja y se guarda **como texto sin convertir de zona** (`Modelo-Datos-Logico.md` §2.2,
/// `RC-06`); <see cref="CreatedAt"/> y <see cref="UpdatedAt"/> son sellos del sistema en tiempo
/// universal coordinado.
///
/// LAS DOS RESOLUCIONES DE ACCESO NO SON LA MISMA PREGUNTA Y NO COMPARTEN MOTIVOS.
/// <see cref="ResolveStudentAccess"/> oculta la existencia del trabajo ajeno (RN-03, INV-02) y
/// <see cref="ResolveAdministratorScope"/> **no oculta nada**: expresa que el borrador está fuera
/// de su flujo de trabajo (RN-11). Son `Domain CU-09` y `Domain CU-11`, y son simétricas a
/// propósito.
/// </remarks>
public sealed class Work
{
    private readonly List<Piece> _pieces = [];
    private readonly List<Observation> _observations = [];

    /// <summary>
    /// Constructor de materialización. Lo usa el motor de persistencia y nadie más: el único
    /// camino de alta del producto es <see cref="Create"/>, y no hay un segundo.
    /// </summary>
    private Work()
    {
        Name = string.Empty;
        DeclaredDate = string.Empty;
        OriginalJson = string.Empty;
    }

    /// <summary>
    /// El único estado que queda FUERA del alcance del administrador (RN-11).
    /// </summary>
    /// <remarks>
    /// ES EL PREDICADO DE ALCANCE, EXPUESTO COMO DATO Y NO COMO MÉTODO, para que la consulta de
    /// listado del adaptador lo use **tal cual** en lugar de repetir la comparación. Un predicado
    /// escrito como método no lo puede traducir el motor de datos y terminaría copiado en la
    /// consulta, que es exactamente el segundo lugar donde RN-11 puede decir otra cosa
    /// (`Domain CU-11` §10). **[decisión de la etapa `e`, declarada]**
    /// </remarks>
    public static WorkStatus StatusOutsideAdministratorScope => WorkStatus.Draft;

    /// <summary>Identidad propia del trabajo. Presente desde la creación y no se reutiliza.</summary>
    public Guid Id { get; private set; }

    /// <summary>Alumno al que pertenece. Obligatorio y NO TRANSFERIBLE (INV-02).</summary>
    public Guid OwnerId { get; private set; }

    /// <summary>Título que el alumno le da a su trabajo.</summary>
    public string Name { get; private set; }

    /// <summary>
    /// Fecha que el alumno DECLARA para el trabajo. Es dato del alumno y no del reloj del
    /// sistema: se conserva **como la escribió** y no se convierte de zona (`RC-06`).
    /// </summary>
    public string DeclaredDate { get; private set; }

    /// <summary>Texto libre con el que el alumno explica qué modeló. Admite vacío.</summary>
    public string? Description { get; private set; }

    /// <summary>
    /// El texto que el alumno pegó, tal como lo emitió su programa. **Se conserva íntegro y el
    /// producto nunca lo reescribe** (RN-08).
    /// </summary>
    public string OriginalJson { get; private set; }

    /// <summary>Estado del trabajo. Conjunto cerrado de cuatro valores, con dos terminales.</summary>
    public WorkStatus Status { get; private set; }

    /// <summary>
    /// Comentario que el administrador deja al aprobar o al rechazar. A lo sumo uno, porque los
    /// dos desenlaces son terminales. **No es una observación y no es una calificación.**
    /// </summary>
    public string? AdministratorComment { get; private set; }

    /// <summary>
    /// Cuántas figuras trae el texto interpretado, incluidas las que no se pudieron reconstruir.
    /// </summary>
    /// <remarks>
    /// NULA MIENTRAS EL TEXTO NO SE INTERPRETÓ, que en la etapa `e` es **siempre**
    /// (`Modelo-Datos-Logico.md` §2.2). La escribe la adopción de la interpretación de la etapa
    /// `f` (`Domain BT-13`). Se declara ya porque es el rango de posiciones válidas contra el que
    /// se valida una observación (RN-09) y la columna es parte de la tabla desde su creación.
    /// </remarks>
    public int? RootFigureCount { get; private set; }

    /// <summary>Las piezas que la interpretación reconstruyó, en su posición del conjunto raíz.</summary>
    /// <remarks>Vacío mientras el texto no se interpretó, y **con huecos** cuando alguna falló.</remarks>
    public IReadOnlyList<Piece> Pieces => _pieces;

    /// <summary>Las observaciones que la interpretación emitió, de las dos especies.</summary>
    public IReadOnlyList<Observation> Observations => _observations;

    /// <summary>Momento en que el trabajo quedó constituido. Obligatorio y NO SE REESCRIBE.</summary>
    public DateTimeOffset CreatedAt { get; private set; }

    /// <summary>Momento del último cambio. Nace igual al de creación y lo aporta el consumidor.</summary>
    public DateTimeOffset UpdatedAt { get; private set; }

    /// <summary>
    /// CU-05 — Constituye un trabajo con dueño, en estado `Draft`.
    /// </summary>
    /// <param name="ownerId">Alumno dueño. Sin dueño no hay trabajo (INV-02).</param>
    /// <param name="name">Título del trabajo. Obligatorio.</param>
    /// <param name="declaredDate">Fecha que declara el alumno. Obligatoria.</param>
    /// <param name="description">Descripción. Admite vacío y admite ausencia.</param>
    /// <param name="originalJson">
    /// El texto del alumno, TAL COMO LLEGÓ. No se recorta, no se normaliza y no se reordena.
    /// Se comprueba por presencia y no por forma: un texto en blanco es un texto del alumno.
    /// </param>
    /// <param name="originalJsonPreservedDeclared">
    /// El consumidor declara que el texto que aporta es **el del alumno** y no una versión que el
    /// producto haya corregido (RN-08). **Tiene que llegar en verdadero**: el parámetro existe
    /// para poder rechazar la corrección, no para poder hacerla.
    /// </param>
    /// <param name="createdAt">Momento de alta, aportado por el consumidor.</param>
    public static DomainResult<Work> Create(
        Guid ownerId,
        string? name,
        string? declaredDate,
        string? description,
        string? originalJson,
        bool originalJsonPreservedDeclared,
        DateTimeOffset createdAt)
    {
        // El orden es el del flujo principal de CU-05 §4, paso por paso.
        if (ownerId == Guid.Empty)
        {
            return DomainResult<Work>.Rejected(ConditionCode.WorkWithoutOwner);
        }

        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(declaredDate))
        {
            return DomainResult<Work>.Rejected(ConditionCode.RequiredFieldMissing);
        }

        // EL TEXTO SE COMPRUEBA POR PRESENCIA Y NO POR FORMA. Que falte es un dato obligatorio
        // ausente —la columna no admite nulo—; que no verifique NO ES UN FALLO de esta operación
        // y es exactamente lo que `Draft` significa. **[derivación de la etapa `e`, declarada:
        // `Domain CU-05` §6 no lo enumera, y `Modelo-Datos-Logico.md` §2.2 declara la columna no
        // nulable, de modo que un nulo no se puede materializar.]**
        if (originalJson is null)
        {
            return DomainResult<Work>.Rejected(ConditionCode.RequiredFieldMissing);
        }

        if (!originalJsonPreservedDeclared)
        {
            return DomainResult<Work>.Rejected(ConditionCode.OriginalJsonAltered);
        }

        var work = new Work
        {
            Id = Guid.NewGuid(),
            OwnerId = ownerId,
            Name = name.Trim(),
            DeclaredDate = declaredDate.Trim(),
            Description = description,
            // NI `Trim` NI NADA: el texto se adopta carácter por carácter (RN-08).
            OriginalJson = originalJson,
            Status = WorkStatus.Draft,
            AdministratorComment = null,
            // Sin interpretar: 0 piezas, 0 observaciones y ninguna cantidad de figuras.
            RootFigureCount = null,
            CreatedAt = createdAt,
            UpdatedAt = createdAt,
        };

        return DomainResult<Work>.Applied(work);
    }

    /// <summary>
    /// CU-05 FA-01 — Reedita un trabajo en `Draft`: reemplaza los cuatro datos y descarta la
    /// interpretación anterior.
    /// </summary>
    /// <remarks>
    /// CONSERVA IDENTIFICADOR, DUEÑO, ESTADO Y MOMENTO DE CREACIÓN. Lo que descarta es lo que una
    /// interpretación anterior hubiera dejado, que en la etapa `e` es la cantidad de figuras del
    /// conjunto raíz; las piezas y las observaciones entran con la etapa `f` y se descartan acá
    /// mismo cuando existan.
    ///
    /// EL TEXTO NUEVO ES OTRO TEXTO DEL ALUMNO Y NUNCA UNA VERSIÓN CORREGIDA (RN-08 §3): lo que
    /// la reedición reemplaza es el texto entero que la persona vuelve a pegar.
    /// </remarks>
    public DomainResult Edit(
        string? name,
        string? declaredDate,
        string? description,
        string? originalJson,
        bool originalJsonPreservedDeclared,
        DateTimeOffset updatedAt)
    {
        if (Status != WorkStatus.Draft)
        {
            return DomainResult.Rejected(ConditionCode.EditOutsideDraft);
        }

        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(declaredDate))
        {
            return DomainResult.Rejected(ConditionCode.RequiredFieldMissing);
        }

        if (originalJson is null)
        {
            return DomainResult.Rejected(ConditionCode.RequiredFieldMissing);
        }

        if (!originalJsonPreservedDeclared)
        {
            return DomainResult.Rejected(ConditionCode.OriginalJsonAltered);
        }

        Name = name.Trim();
        DeclaredDate = declaredDate.Trim();
        Description = description;
        OriginalJson = originalJson;

        // LA INTERPRETACIÓN ANTERIOR SE DESCARTA ENTERA, y desde la etapa `f` eso ya no es sólo la
        // cantidad de figuras: el texto nuevo es OTRO texto, y las piezas y observaciones que
        // sobrevivieran describirían uno que ya no está guardado.
        RootFigureCount = null;
        _pieces.Clear();
        _observations.Clear();
        UpdatedAt = updatedAt;

        return DomainResult.Applied();
    }

    /// <summary>
    /// `Domain BT-13` — Adopta el resultado de la interpretación, **reemplazando el anterior**.
    /// </summary>
    /// <remarks>
    /// ETAPA `f`. Es la operación que la etapa `e` declaró pendiente en <see cref="RootFigureCount"/>
    /// y en <see cref="Edit"/>: hasta hoy la interpretación no existía y no había nada que adoptar.
    ///
    /// REEMPLAZA Y NO ACUMULA. Un trabajo reenviado se interpreta de nuevo, y las piezas y
    /// observaciones de la vez anterior **no sobreviven**: dejarlas convertiría el conjunto en la
    /// unión de dos lecturas de dos textos distintos, y ninguna posición significaría nada.
    ///
    /// NO RESUELVE EL ESTADO, y por eso es una operación aparte de <see cref="Submit"/>: adoptar es
    /// incorporar lo que el validador leyó, y decidir es aplicar RN-05 sobre eso. Juntarlas haría
    /// imposible adoptar sin decidir, que es lo que la reedición necesita.
    ///
    /// LA POSICIÓN DE CADA OBSERVACIÓN TIENE QUE CAER EN EL RANGO DEL CONJUNTO RAÍZ (RN-09), y es
    /// lo único que esta operación rechaza: una observación que señala una figura que el texto no
    /// tiene es un defecto del validador, no del alumno, y adoptarla le mostraría a la persona una
    /// ubicación que no puede encontrar.
    /// </remarks>
    /// <param name="rootFigureCount">Cuántas figuras trae el texto, **incluidas las fallidas**.</param>
    /// <param name="pieces">Las piezas reconstruidas, con su posición.</param>
    /// <param name="observations">Las observaciones emitidas, de las dos especies.</param>
    /// <param name="updatedAt">Momento del cambio, aportado por el consumidor.</param>
    public DomainResult AdoptInterpretation(
        int rootFigureCount,
        IEnumerable<Piece> pieces,
        IEnumerable<Observation> observations,
        DateTimeOffset updatedAt)
    {
        ArgumentNullException.ThrowIfNull(pieces);
        ArgumentNullException.ThrowIfNull(observations);

        if (IsTerminal)
        {
            return DomainResult.Rejected(ConditionCode.TransitionFromTerminalStatus);
        }

        if (rootFigureCount < 0)
        {
            return DomainResult.Rejected(ConditionCode.RequiredFieldMissing);
        }

        var adoptedPieces = pieces.ToList();
        var adoptedObservations = observations.ToList();

        // RN-09: la posición designada tiene que existir en el conjunto raíz. Vale para las piezas
        // y para las observaciones, y por el mismo motivo.
        if (adoptedPieces.Any(p => p.Position < 0 || p.Position >= rootFigureCount)
            || adoptedObservations.Any(o => o.PiecePosition is { } position
                && (position < 0 || position >= rootFigureCount)))
        {
            return DomainResult.Rejected(ConditionCode.ObservationOnMissingPiece);
        }

        // La especie, ANTES de contar errores: RN-05 se predica de ella, y contar sobre un conjunto
        // con una especie desconocida daría un número que no significa nada.
        if (adoptedObservations.Any(o => !Enum.IsDefined(o.Kind)))
        {
            return DomainResult.Rejected(ConditionCode.UnknownObservationKind);
        }

        // RN-09: ningún mensaje genérico. Un error sin campo, o sin posición cuando hay figuras a
        // las que atribuirlo, es exactamente lo que la regla existe para impedir.
        if (adoptedObservations.Any(o => o.Kind == ObservationKind.ValidationError
            && (string.IsNullOrWhiteSpace(o.Field)
                || (o.PiecePosition is null && rootFigureCount > 0))))
        {
            return DomainResult.Rejected(ConditionCode.ErrorWithoutLocation);
        }

        // Sin los dos valores el alumno no ve EN QUÉ discrepa su programa, que es lo único que la
        // advertencia le aporta.
        if (adoptedObservations.Any(o => o.Kind == ObservationKind.Warning
            && (o.DeclaredValue is null || o.DerivedValue is null)))
        {
            return DomainResult.Rejected(ConditionCode.WarningMissingBothValues);
        }

        _pieces.Clear();
        _pieces.AddRange(adoptedPieces);
        _observations.Clear();
        _observations.AddRange(adoptedObservations);

        RootFigureCount = rootFigureCount;
        UpdatedAt = updatedAt;

        return DomainResult.Applied();
    }

    /// <summary>
    /// CU-08 — Resuelve el estado que sigue al envío: `Submitted` si el texto verificó, `Draft`
    /// si no.
    /// </summary>
    /// <remarks>
    /// UN ENVÍO CUYO TEXTO NO VERIFICA **NO ES UN RECHAZO DE LA OPERACIÓN**: el resultado se
    /// aplica, el trabajo queda en `Draft` con sus observaciones y el alumno corrige y vuelve a
    /// enviar (FA-01). Las advertencias **no impiden** el paso (RN-05, FA-02).
    ///
    /// EN LA ETAPA `e` ESTA OPERACIÓN SIEMPRE RECHAZA desde el camino del producto, porque nadie
    /// puede declarar un resultado de interpretación que no existe: el validador de figuras es de
    /// la etapa `f` y su puerto sigue sin conectar. La operación se escribe igual porque
    /// `Domain BT-12` es de esta etapa y porque sin ella no habría forma de construir un trabajo
    /// en `Submitted` para verificar los criterios de eliminación y de alcance.
    /// </remarks>
    /// <param name="parseResultDeclared">
    /// El consumidor declara que el texto original **fue interpretado** y que su resultado ya se
    /// incorporó al trabajo. Sin ese resultado no hay nada que decidir.
    /// </param>
    /// <param name="validationErrorsDeclared">
    /// Si la interpretación dejó al menos una observación de especie error de validación. Es lo
    /// único que impide el paso a `Submitted` (RN-05).
    /// </param>
    /// <param name="updatedAt">Momento del cambio, aportado por el consumidor.</param>
    public DomainResult Submit(
        bool parseResultDeclared,
        bool validationErrorsDeclared,
        DateTimeOffset updatedAt)
    {
        if (IsTerminal)
        {
            return DomainResult.Rejected(ConditionCode.TransitionFromTerminalStatus);
        }

        if (Status != WorkStatus.Draft)
        {
            // `Submitted`: el trabajo ya salió de las manos del alumno y ninguna fuente declara
            // una reentrada al envío desde ahí (FA-03).
            return DomainResult.Rejected(ConditionCode.SubmissionOutsideDraft);
        }

        if (!parseResultDeclared)
        {
            return DomainResult.Rejected(ConditionCode.SubmissionWithoutParseResult);
        }

        // FA-01: el resultado se aplica igual, y el estado que queda es `Draft`.
        Status = validationErrorsDeclared ? WorkStatus.Draft : WorkStatus.Submitted;
        UpdatedAt = updatedAt;

        return DomainResult.Applied();
    }

    /// <summary>
    /// CU-10 — Aplica el desenlace que decide el administrador y deja el trabajo terminal.
    /// </summary>
    /// <remarks>
    /// **NO SE EXPONE EN LA SUPERFICIE HTTP EN LA ETAPA `e`**: el punto `A-15` es de la etapa `h`.
    /// La transición se escribe acá porque `Domain BT-12` la nombra en su criterio de aceptación
    /// y porque es lo que permite construir trabajos `Approved` y `Rejected` con su comentario,
    /// sin los cuales el criterio 10 de la etapa `d` —«la cuenta reseteada conserva todos sus
    /// trabajos, sobre un alumno con trabajos en tres estados distintos y con sus comentarios»—
    /// seguiría sin poder verificarse.
    ///
    /// EL COMENTARIO ES OPCIONAL EN LOS DOS DESENLACES (FA-01), y su ausencia es una consecuencia
    /// aceptada por escrito aguas arriba: el estado le informa al alumno que no fue aceptado.
    /// </remarks>
    /// <param name="requesterRole">Papel declarado de quien pide el desenlace. Facultad exclusiva del administrador.</param>
    /// <param name="outcome">Aprobar o rechazar. Conjunto cerrado de dos valores.</param>
    /// <param name="comment">Comentario escrito, opcional.</param>
    /// <param name="updatedAt">Momento del cambio, aportado por el consumidor.</param>
    public DomainResult ApplyOutcome(
        Role requesterRole,
        WorkOutcome outcome,
        string? comment,
        DateTimeOffset updatedAt)
    {
        if (IsTerminal)
        {
            return DomainResult.Rejected(ConditionCode.TransitionFromTerminalStatus);
        }

        if (Status != WorkStatus.Submitted)
        {
            // Un trabajo en `Draft` no se aprueba ni se rechaza: el administrador ni lo ve (RN-11).
            return DomainResult.Rejected(ConditionCode.OutcomeOutsideSubmitted);
        }

        if (requesterRole != Role.Administrator)
        {
            return DomainResult.Rejected(ConditionCode.OutcomeRequiresAdministratorRole);
        }

        if (!Enum.IsDefined(outcome))
        {
            return DomainResult.Rejected(ConditionCode.UnknownOutcome);
        }

        Status = outcome == WorkOutcome.Approve ? WorkStatus.Approved : WorkStatus.Rejected;
        AdministratorComment = string.IsNullOrWhiteSpace(comment) ? null : comment;
        UpdatedAt = updatedAt;

        return DomainResult.Applied();
    }

    /// <summary>
    /// CU-09 — Responde si un alumno puede ejercer una operación sobre este trabajo.
    /// </summary>
    /// <remarks>
    /// NO TIENE EFECTO: la consulta no modifica nada, ni cuando procede ni cuando no.
    ///
    /// EL DUEÑO VE SUS CUATRO ESTADOS (FA-02). Lo que la acotación al borrador restringe es
    /// **operar** sobre el trabajo, no verlo: el alumno ve el desenlace y el comentario de su
    /// propio trabajo.
    ///
    /// EL MOTIVO DE LA NO PERTENENCIA ES INDISTINGUIBLE DE LA INEXISTENCIA, y quien traduce a
    /// «no encontrado» es `GeometriaFactory.Api`. Acá se declara la equivalencia para que ninguna
    /// capa la invente.
    /// </remarks>
    public DomainResult ResolveStudentAccess(Guid requesterId, WorkOperation operation)
    {
        // §6: la operación desconocida se rechaza SIN EVALUAR LA PERTENENCIA.
        if (!Enum.IsDefined(operation))
        {
            return DomainResult.Rejected(ConditionCode.UnknownOperation);
        }

        if (requesterId == Guid.Empty || requesterId != OwnerId)
        {
            return DomainResult.Rejected(ConditionCode.WorkNotFoundForRequester);
        }

        if (operation is (WorkOperation.Edit or WorkOperation.Delete) && Status != WorkStatus.Draft)
        {
            return DomainResult.Rejected(ConditionCode.OperationOutsideDraft);
        }

        return DomainResult.Applied();
    }

    /// <summary>
    /// CU-11 — Responde si este trabajo entra en el alcance del administrador para una operación.
    /// </summary>
    /// <remarks>
    /// LAS DOS CARAS DE LA MISMA PREGUNTA: el administrador **no ve los borradores** (RN-11) y
    /// **elimina cualquiera de los que sí ve, en cualquier estado** (RN-04). La terminalidad
    /// impide que el trabajo cambie de estado o de contenido (INV-07), **no que él lo retire**.
    ///
    /// LA REEDICIÓN NO ESTÁ EN SU CONJUNTO DE OPERACIONES (§3: ver, eliminar), y pedirla por esta
    /// vía devuelve `UNKNOWN_OPERATION`: el administrador no edita el trabajo de nadie.
    /// </remarks>
    public DomainResult ResolveAdministratorScope(Role requesterRole, WorkOperation operation)
    {
        // §4 paso 2: el papel, antes que el estado. CA-04 lo exige explícitamente.
        if (requesterRole != Role.Administrator)
        {
            return DomainResult.Rejected(ConditionCode.ScopeRequiresAdministratorRole);
        }

        if (operation is not (WorkOperation.View or WorkOperation.Delete))
        {
            return DomainResult.Rejected(ConditionCode.UnknownOperation);
        }

        if (Status == StatusOutsideAdministratorScope)
        {
            return DomainResult.Rejected(ConditionCode.WorkOutsideAdministratorScope);
        }

        return DomainResult.Applied();
    }

    /// <summary>Si el trabajo está en uno de los dos estados de cierre (INV-07, RN-10).</summary>
    private bool IsTerminal => Status is WorkStatus.Approved or WorkStatus.Rejected;
}
