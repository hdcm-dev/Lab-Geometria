# ADR-04004 — Orden fijo de las cuatro comprobaciones de autorización, cada una en su capa

**Unidad de entrega:** GeometriaFactory-Api
**Documento:** ADR-04004-Orden-Fijo-De-Las-Cuatro-Comprobaciones.md
**Versión:** 2.0
**Estado:** Aprobado
**Fecha:** 2026-08-31
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Seguridad

---

## 1. Contexto

Esta capa **autoriza y no autentica**: quién es la persona llega ya resuelto desde afuera, y lo que acá se decide es si ese pedido concreto procede. Son **cuatro** comprobaciones, declaradas en [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4: pertenencia, facultad, alcance del administrador y **cambio de contraseña pendiente**.

Hay un antecedente que pesa y que viene del nivel 0. [`Domain ADR-02005`](ADR-02005-Guarda-Unica-De-Admisibilidad.md) decidió que la admisibilidad es la puerta única de las guardas de acceso de la cuenta, y **declaró en su §6 que el dominio no puede impedir que exista un camino que ejerza una capacidad sin resolver la admisión**. Esa dependencia de disciplina cae acá: si el camino existiera, tendría que volver a comprobar la marca, y esa comprobación no sería del dominio. Es exactamente lo que esta ADR viene a cerrar.

El orden importa por dos motivos distintos, y conviene no confundirlos. El primero es de corrección: `INV-09` enuncia que una cuenta con la marca puesta no ejerce **ninguna** capacidad, ni siquiera las que su papel y su pertenencia admitirían, de modo que comprobar la marca después de la pertenencia devolvería la negativa equivocada. El segundo es de filtración: la negativa por pertenencia oculta la existencia del recurso y la negativa por facultad no tiene nada que ocultar, así que ejercerlas en el orden equivocado permitiría averiguar por tanteo qué identificadores existen.

Motivación upstream: NB-00001, NB-00002, NB-00003, NB-00007, NB-00009; RN-04001, RN-04003, RN-04006, RN-04010, RN-04011, RN-04013, RN-04015, RN-04016; INV-02, INV-03, INV-06, INV-07, INV-09; `PRODUCT-INTAKE` §17.1.P.5 · GeometriaFactory-Application y §17.1.P.2 · GeometriaFactory-Domain.

## 2. Decisión

**Las cuatro comprobaciones se ejercen en un orden fijo, sobre el dato ya recuperado y antes de cualquier escritura, y cada una vive en la capa que la posee. Ninguna se ejerce dos veces.**

| # | Comprobación | Dónde vive | Por qué ahí |
| --- | --- | --- | --- |
| 1 | **Cambio de contraseña pendiente** | `PendingPasswordChangeGuard`, intermediario de la superficie HTTP | Tiene que alcanzar **todo** punto que exija acceso firmado, y su defecto característico no es hacer mal lo que hace sino **no alcanzar a alguno**. Un filtro por punto hay que acordarse de ponerlo en cada uno, y el olvido no se nota; el intermediario alcanza a todo lo que pase por la tubería, y **lo que se declara explícitamente es la excepción**, que es una sola. Además **lee la marca del almacén y no del acceso presentado**, que es lo que hace que un acceso emitido antes de un reseteo deje de servir sin esperar a que venza |
| 2 | **Pertenencia** | `Work.ResolveStudentAccess`, en el dominio | Es una regla **sobre la entidad**: quién puede operar sobre un trabajo depende de quién es su dueño, que es un atributo del trabajo. Sacarla de ahí la volvería invisible para quien lee `Work` |
| 3 | **Facultad** | El caso de uso, en la capa de aplicación | Es una regla **sobre el pedido** y no sobre el dato: corta **antes** de pedirle nada al repositorio, que es lo que evita traer un trabajo para después negarlo |
| 4 | **Alcance del administrador** | `Work.StatusOutsideAdministratorScope` y `Work.ResolveAdministratorScope`, en el dominio | Mismo motivo que la pertenencia, y con una consecuencia propia: **el predicado sale del dominio y el adaptador lo usa tal cual**, de modo que `RN-11` no tiene un segundo lugar donde decir otra cosa |

**Lo que se garantiza no es que haya un componente: es que haya UN SOLO LUGAR POR COMPROBACIÓN.** Ningún orquestador vuelve a comprobar lo que otra capa ya comprobó, y ninguna comprobación está escrita dos veces con la posibilidad de decir cosas distintas.

**El orden se conserva íntegro y por los dos motivos de §1.** El de corrección: `INV-09` enuncia que una cuenta con la marca puesta no ejerce **ninguna** capacidad, así que comprobar la marca después de la pertenencia devolvería la negativa equivocada — y por eso la comprobación 1 es un intermediario, que corre antes de que el enrutamiento elija el punto. El de filtración: **la negativa por pertenencia y la negativa por facultad no se colapsan y no se intercambian**. La primera responde con el motivo de trabajo inexistente para el solicitante —que el consumidor traduce a «no encontrado» y **nunca** a «no autorizado»—; la segunda sí admite ser explícita, porque no hay recurso ajeno cuya existencia proteger.

### 2.1 Por qué esta versión cambia la decisión de la 1.0

**La 1.0 exigía un único componente, y lo construido son cuatro lugares. Se corrige el ADR y no el código, y el fundamento es que la distribución es mejor que lo que la 1.0 pedía.**

Mover la pertenencia y el alcance fuera del dominio las volvería invisibles para quien lee la entidad, y las dejaría a merced de que cada caso de uso se acuerde. Mover la marca fuera del intermediario reintroduce exactamente el defecto que §1 nombra: un punto nuevo que se olvida y **nada falla**.

**Lo que la 1.0 quería evitar sigue evitado**, y es lo que esta versión declara en su lugar: que dos capas comprueben lo mismo y puedan decir cosas distintas. Eso **no pasa** — se midió, comprobación por comprobación, el 2026-08-31.

## 3. Estado

**Aprobado** el 2026-08-31, en su versión 2.0. Estuvo **Propuesto desde el 2026-08-10** y nunca se aceptó: mientras tanto el código se construyó, y lo construido no cumplía la letra de la 1.0 —un único componente— sino algo que esta versión declara mejor.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Orden fijo en un único componente (**considerada en la 1.0, no adoptada**) | Cierra la dependencia de disciplina que el nivel 0 declaró que no podía cerrar; un solo lugar que auditar; el invariante se verifica con una prueba por comprobación y una por el orden | Depende de que ningún orquestador se saltee la guarda, y eso no lo garantiza el compilador |
| Orden fijo con **un lugar por comprobación, en la capa que la posee** (**adoptada en la 2.0**) | Cada regla se lee donde vive lo que gobierna: la pertenencia y el alcance en la entidad, la facultad en el pedido, la marca en la tubería. Un solo lugar por comprobación, que es la propiedad que importa | Son cuatro lugares y no uno, así que la auditoría recorre cuatro. La mitigación es la tabla de §2, que dice cuál es cada uno |
| Las cuatro comprobaciones repartidas en cada orquestador | No hay componente transversal que recordar; cada caso de uso se lee entero | **Once** lugares que mantener sincronizados. Es la misma forma en que se abrieron el P0 y el P1 del nivel 0, con la diferencia de que allá eran cuatro operaciones y acá serían once |
| Comprobaciones en la capa que expone, con esta capa confiando en lo ya verificado | La capa que expone tiene la petición entera y podría cortar antes, sin recuperar el dato | La autorización por pertenencia dejaría de ser verificable sin infraestructura, que es la propiedad que `PRODUCT-INTAKE` §17.1.P.5 · GeometriaFactory-Application y §17.1.P.6 · GeometriaFactory-Application exigen. Además el front **no puede ser la última defensa de ninguna regla**, y quien atiende la petición tampoco tiene la entidad recuperada |
| Comprobaciones sin orden fijo, resueltas por conjunto de motivos | Devolvería todas las causas de una vez, que a veces es más útil | Rompe `INV-09`: una cuenta con la marca puesta recibiría además motivos que revelan qué existe y qué no. El orden **es** parte de la corrección, no una optimización |

**La tercera alternativa merece una precisión.** No se descarta por desconfiar de la capa que expone, sino porque desplazar la comprobación allá la volvería inverificable con dobles. Nada impide que la capa que expone **también** acote lo que ofrece: eso es una decisión de presentación legítima, y `GeometriaFactory-Web` la toma. Lo que no puede es reemplazar a ésta.

## 5. Consecuencias positivas

1. `INV-09` queda con una puerta cerrada del lado que el nivel 0 declaró que no podía cerrar, y con una sola prueba que verifica que la cuarta comprobación corta antes que las otras tres.
2. Las tres negativas de autorización quedan distinguibles y auditables —**cada una en un solo lugar**—, que es lo que la categoría 03 pide en su tabla de traducciones prohibidas.
3. Agregar una capacidad nueva no obliga a escribir cuatro comprobaciones: la marca la alcanza sola por la tubería, y las tres restantes se invocan desde donde ya viven.
4. La verificación de pertenencia se prueba con dobles y sin base, que es la razón declarada de que `tiene_auth` valga true en este proyecto de código.
5. Aguas abajo, la capa que expone traduce **un solo código** para todas las operaciones bloqueadas por la marca, en lugar de uno por operación —que es lo que el contrato del producto ya decidió con su código único de cambio de contraseña requerido—.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta una dependencia de disciplina hacia adentro.** El compilador no impide que un orquestador nuevo comprueba por su cuenta algo que ya vive en otra capa, ni que se saltee la comprobación de facultad. La mitigación es de inspección: la verificación estructural de §7 y el NFR de las cuatro comprobaciones ejercitadas.
2. **Se acepta que la marca se comprueba antes de saber si el recurso existe**, de modo que una cuenta marcada que pide un trabajo ajeno recibe el motivo de la marca y no el de inexistencia. Es deliberado: revelar cualquier otra cosa a una cuenta que no debe ejercer ninguna capacidad sería exactamente lo que `INV-09` prohíbe.
3. **Se acepta que la excepción del reemplazo de credencial quede declarada y no derivada.** Es una excepción a una regla de orden, y las excepciones a reglas de orden son el lugar donde vuelven los defectos: por eso se escribe en la decisión y no en una nota.

## 7. Implementación

- **Cada comprobación tiene un solo lugar**, y son los cuatro de la tabla de §2. Ningún orquestador vuelve a ejercer una que otra capa ya ejerció.
- La comprobación de la marca **no lee el conjunto y no escribe**: resuelve la cuenta y corta. Las otras tres trabajan **sobre la entidad ya recuperada**, que es lo que las hace ejercitables con dobles.
- El **alcance del administrador** se traslada a la consulta y no se aplica después de traerla: un borrador **no se filtra en memoria, no se trae**. `IWorkRepository` toma el predicado del dominio tal cual.
- La **negativa por pertenencia** sale como trabajo inexistente para el solicitante y se traduce a `404`. Nunca a `403`.
- Verificación estructural para 08: recorrer los orquestadores y comprobar que ninguno ejerce una comprobación que ya vive en otra capa, y que ninguno invoca al dominio antes de comprobar la facultad.

### 7.1 Dos códigos de facultad que no llegan al borde, y que si llegaran responderían mal

**Medido el 2026-08-31.** El árbol declara **cuatro** códigos de negativa por facultad:

| Código | Capa | ¿Traduce? |
| --- | --- | --- |
| `ADMINISTRATOR_ROLE_REQUIRED` | Aplicación | **Sí** → `OPERATION_ADMIN_ONLY`, `403` |
| `OUTCOME_REQUIRES_ADMINISTRATOR_ROLE` | Dominio | **Sí** → `OPERATION_ADMIN_ONLY`, `403` |
| `ADMINISTRATOR_ROLE_OUTSIDE_THIS_PATH` | Dominio | **No** |
| `SCOPE_REQUIRES_ADMINISTRATOR_ROLE` | Dominio | **No** |

**Los dos sin traducción caerían al genérico, con `500`.** Una persona sin el papel recibiría «el producto falló» donde corresponde «esta operación es del docente».

**Los dos son inalcanzables hoy, y se verificó uno por uno.** `ADMINISTRATOR_ROLE_OUTSIDE_THIS_PATH` lo devuelve `Account.Register` cuando el papel pedido no es `Student`, y el único caso de uso que la invoca **fija `Role.Student` en la llamada**. `SCOPE_REQUIRES_ADMINISTRATOR_ROLE` lo devuelve `Work.ResolveAdministratorScope` cuando quien pide no es administrador, y **sus dos invocadores comprueban el papel antes** y rechazan con el código de la capa de aplicación.

**No se retiran del dominio.** Son guardas de la entidad y su valor es que la entidad no dependa de que su invocador se acuerde. **Lo que se recomienda a la categoría que gobierna la traducción** es darles su fila con destino `403`: cuestan dos líneas, y hoy la única red que los cubre es que nadie los alcance.

**Es la quinta vez que este patrón aparece en este producto** —una rama defensiva correcta, inalcanzable en la aplicación compuesta— y las cinco se descubrieron corriendo samples. Las otras cuatro: el respaldo `UNKNOWN` del visor, `NON_DRAWABLE_TYPE`, la rama de clave de firma ausente de `AccessTokenIssuer`, y `EDIT_OUTSIDE_DRAFT`. **La diferencia de estas dos es que las otras cuatro, si se alcanzaran, responderían bien.**

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Comprobaciones escritas en **más de un lugar** | Exactamente **0** | Inspección, comprobación por comprobación, contra la tabla de §2 |
| Códigos de negativa por facultad **sin traducción al contrato** | Exactamente **0** | Recorrer los códigos de facultad del árbol contra la tabla de traducción. **Hoy da 2**, los dos inalcanzables, y está declarado en §7.1 |
| Comprobaciones ejercitadas con prueba de su negativa, sin base de datos | **4 de 4** | Matriz comprobación contra prueba en 08 |
| Pruebas que verifican que la cuarta comprobación corta antes que las otras tres | Al menos **1**, con una cuenta marcada que pide una operación que su papel y su pertenencia admitirían | Prueba dedicada |
| Códigos distintos para las operaciones bloqueadas por la marca | Exactamente **1** para todas ellas | Prueba que pide tres operaciones distintas con la marca puesta y comprueba el mismo motivo |
| Orígenes de la marca que producen el mismo motivo | **2 de 2** —habilitación y reseteo— | Prueba con una cuenta recién habilitada y una reseteada |
| Negativas por pertenencia que se traducen a «no autorizado» | Exactamente **0** | Prueba que pide un trabajo ajeno y comprueba el motivo, más la tabla de traducciones prohibidas de 03 |
| Reemplazo de la propia credencial con la marca puesta | Procede en **100 %** de los casos, y levanta la marca | Prueba de la única capacidad admitida |

## 9. Referencias

- [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4, con sus cinco precisiones.
- [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md) §2.4, la tabla de traducciones prohibidas.
- [`../../../GeometriaFactory-Domain/05-Arquitectura-Tecnica/Adrs/ADR-02005-Guarda-Unica-De-Admisibilidad.md`](ADR-02005-Guarda-Unica-De-Admisibilidad.md) §6 punto 1, que es la dependencia de disciplina que esta ADR cierra.
- [`../../../GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-08006-Contrato-De-Respuesta-De-Error.md`](../../../../Producto/Contratos-Inter-Unidad/CU-08006-Contrato-De-Respuesta-De-Error.md), por el código único de las operaciones bloqueadas por la marca.
- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.16** §17.1.P.5 · GeometriaFactory-Application, §17.1.P.2 · GeometriaFactory-Domain (INV-02, INV-03, INV-09) y §4.1 (RN-04003, RN-04013, RN-04016).
- ADR relacionadas: [`ADR-04001`](ADR-04001-Casos-De-Uso-Con-Inversion-De-Dependencias.md), [`ADR-04006`](ADR-04006-Resultado-Tipado-Y-Catalogo-Cerrado-De-Condiciones.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Registra el orden fijo de las cuatro comprobaciones en un único componente como cierre de la dependencia de disciplina que la Fase C de `GeometriaFactory-Domain` declaró que el dominio no podía cerrar, evalúa cuatro alternativas con una precisión sobre la tercera, declara tres trade-offs y fija siete métricas de validación. |
| 2.0 | 2026-08-31 | **Pasa de `Propuesto` a `Aprobado`, y la decisión cambia: las cuatro comprobaciones viven cada una en su capa y no en un único componente.** La 1.0 exigía un componente único, estuvo propuesta **veintiún días sin aceptarse**, y mientras tanto el código se construyó de otra forma. **Se corrige el ADR y no el código**, porque la distribución es mejor que lo que la 1.0 pedía: mover la pertenencia y el alcance fuera del dominio las volvería invisibles para quien lee la entidad, y mover la marca fuera del intermediario reintroduce el defecto que §1 nombra —un punto nuevo que se olvida y nada falla—. **Lo que la 1.0 quería evitar sigue evitado**, y esta versión lo declara en su lugar: **un solo lugar por comprobación**, medido comprobación por comprobación. Todo lo demás de la 1.0 se conserva porque se verificó cierto: el orden, sus dos motivos, el `404` que nunca es `403`, el código único de la marca y el alcance trasladado a la consulta. §7.1 entra nuevo con **dos códigos de facultad sin traducción** que caerían al genérico con `500`: los dos son inalcanzables hoy y se recomienda darles su fila. Sube **major**: la decisión cambia. |
