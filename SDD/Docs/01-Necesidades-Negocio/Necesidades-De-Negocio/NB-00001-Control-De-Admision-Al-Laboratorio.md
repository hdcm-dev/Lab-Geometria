# NB-00001 — Control de admisión y de bajas del laboratorio

| Campo | Valor |
| --- | --- |
| Producto | Fábrica de Geometría |
| Documento | NB-00001-Control-De-Admision-Al-Laboratorio.md |
| Versión | 1.2 |
| Estado | Aprobado |
| Fecha | 2026-08-10 |
| Autor | Analista de Negocio Senior (AG-01) |
| Trazabilidad upstream | PRODUCT-INTAKE 1.9 §1 (idea y problema), §3 (propuesta de valor), §4 (capacidades F-01 y F-03; **F-26**, que vive en el mismo panel y pertenece a NB-00002), §4.1 (**RN-15**, que declara que el reseteo no es una transición de la máquina de estados), §7 (casos límite CL-6 y **CL-7** reescrito), §9 (exclusión X-3 vigente y **X-2 retirada**), §11 (riesgo **RN-B6**, tachado el 2026-08-09 por el intake 1.10); `Vision-Producto.md` §1, §2, §8 (riesgo RG-06) y §9; `Alcance-Producto.md` §4.1 y §5 |
| Trazabilidad downstream | `CU-00003`, `CU-00004`, `CU-00005`, `CU-02001`, `CU-02002`, `CU-02004`, `CU-02012`, `CU-02013`, `CU-04001`, `CU-04002`, `CU-04010`, `CU-04011`, `CU-06004`, `CU-06005`, `CU-06007` en `GeometriaFactory-Api`; `CU-10001`, `CU-10002`, `CU-10004` en `GeometriaFactory-Web` (emitidos en 02-Especificacion-Funcional); 06-Backlog-Tecnico, 07-Plan-Sprint, 08-Calidad-Y-Pruebas |

---

## Tabla de contenido

- [1. Descripción de la necesidad](#1-descripción-de-la-necesidad)
- [2. Ejemplo de uso desde la perspectiva del negocio](#2-ejemplo-de-uso-desde-la-perspectiva-del-negocio)
- [3. Impacto](#3-impacto)
- [4. Problema específico que resuelve](#4-problema-específico-que-resuelve)
- [5. Criterios de éxito](#5-criterios-de-éxito)
- [6. Stakeholders involucrados](#6-stakeholders-involucrados)
- [7. Trazabilidad a CU](#7-trazabilidad-a-cu)
- [8. Dependencias con otras NB](#8-dependencias-con-otras-nb)
- [9. Prioridad MoSCoW](#9-prioridad-moscow)
- [10. Control de cambios](#10-control-de-cambios)

---

## 1. Descripción de la necesidad

La cátedra necesita decidir quién entra al laboratorio y quién deja de estar, y hoy no tiene ningún mecanismo para hacerlo. El circuito actual de la Actividad 1 no tiene identidad: el alumno modela, copia un texto y lo pega en una página suelta, de modo que no existe la noción de cuenta, ni de comisión, ni de permanencia (PRODUCT-INTAKE §1). Sin una puerta de entrada que alguien controle, cualquier cosa que se construya encima no distingue a un alumno de la comisión de un desconocido, y el docente no puede responder la pregunta más elemental de la administración de un curso: quiénes están adentro.

La restricción que ordena esta necesidad es que **no hay canal de correo** en el producto, por decisión declarada del Product Owner (PRODUCT-INTAKE §9, exclusión X-1). Eso descarta la forma habitual de autorizar altas y obliga a que la admisión sea un acto explícito del docente sobre una lista visible.

La baja de una cuenta **elimina también todos sus trabajos** (PRODUCT-INTAKE §7, CL-6; el §11 lo sostenía en **RN-B6**, riesgo que el intake 1.10 tachó el 2026-08-09 al quedar sin objeto su mitigación, de modo que la afirmación vive hoy en CL-6 y en RN-07): es una operación destructiva que el producto tiene que hacer difícil de ejecutar por accidente. **Hasta el 2026-08-09 era además la única salida declarada para un alumno que olvidaba su contraseña**, y esta necesidad lo decía así. Ya no lo es: el Product Owner incorporó la capacidad **F-26**, con la que el administrador **resetea la credencial** desde este mismo panel sin tocar la cuenta ni sus trabajos, y la exclusión X-2 quedó retirada. La baja vuelve entonces a ser lo que su enunciado dice —sacar a alguien del laboratorio—, y deja de ser el remedio de un olvido. **F-26 no pertenece a esta necesidad sino a NB-00002**, aunque comparta el panel: por RN-15 el reseteo opera sobre la credencial y **no es una transición de la máquina de estados de la cuenta**, de modo que no admite ni excluye a nadie; el fundamento completo está en NB-00002 §9.

La necesidad también fija su propio techo. El producto es deliberadamente básico: un solo administrador, dos papeles fijos y ningún esquema de permisos configurables (PRODUCT-INTAKE §9, exclusión X-3). Resolver la admisión no es construir una administración de identidades; es darle al docente el control mínimo y suficiente sobre la lista de su comisión.

## 2. Ejemplo de uso desde la perspectiva del negocio

Arranca la cursada. El docente abre el laboratorio por primera vez y configura su propia cuenta de administrador; a partir de ese momento nadie más puede configurar otra. Durante la primera semana, veinticuatro alumnos se registran y quedan a la espera. El docente entra a su panel, los ve listados, reconoce a los de su comisión y los habilita; a dos que no reconoce los deja sin habilitar. Más adelante, un alumno deja de cursar y el docente da de baja su cuenta: el producto le exige escribir el correo de esa cuenta para confirmar, y le advierte que se van también los trabajos cargados. Otro alumno, en cambio, olvida su contraseña, y ése **no** pasa por la baja: el docente le resetea la credencial desde el mismo panel y el alumno vuelve a entrar con todos sus trabajos (F-26, NB-00002).

## 3. Impacto

- Si se resuelve: la comisión queda delimitada y el docente sabe en todo momento quiénes están habilitados, bloqueados o fuera del laboratorio.
- Si se resuelve: toda la cadena posterior tiene sobre qué apoyarse, porque un trabajo sólo puede tener dueño si antes existe una cuenta habilitada.
- Si se resuelve: la ausencia de canal de correo deja de ser un impedimento y pasa a ser un rasgo del circuito, resuelto con un acto explícito del docente.
- Si queda sin resolver: no hay forma de distinguir a un alumno de la comisión de cualquier otra persona, y ninguna de las capacidades comprometidas posteriores tiene fundamento.
- Si queda sin resolver: no habría panel donde ejercer ninguna de las operaciones sobre una cuenta, ni la admisión ni el reseteo de credencial que NB-00002 necesita.
- Riesgo residual aceptado: la baja elimina también los trabajos de la cuenta, y esa pérdida es irreversible (PRODUCT-INTAKE §7, CL-6; el riesgo **RN-B6** que la registraba quedó tachado el 2026-08-09). **Desde el 2026-08-09 ese riesgo dejó de alcanzar al olvido de contraseña**, que se resuelve reseteando (F-26).

## 4. Problema específico que resuelve

- El docente no tiene hoy ninguna lista de quiénes están usando el material de la Actividad 1.
- No existe un acto de autorización: cualquiera que llegue a la herramienta actual la usa igual.
- No hay canal de correo para autorizar altas, y sin él la autorización tiene que ser visible y manual.
- Una cuenta que no debe seguir activa no se puede bloquear ni retirar.
- Una operación destructiva —la baja, que elimina también los trabajos— puede ejecutarse por accidente si no exige una confirmación deliberada.
- Un segundo administrador configurado por error volvería ambiguo quién manda sobre la lista de la comisión.

## 5. Criterios de éxito

| Criterio | Métrica | Target | Plazo |
| --- | --- | --- | --- |
| Unicidad de la cuenta de administrador | Cuentas de administrador que el producto admite configurar cuando ya existe una | 0 | Punto de control de la etapa `c` |
| Admisión explícita | Cuentas de alumno que acceden al laboratorio sin haber sido habilitadas por el administrador | 0 | Punto de control de la etapa `d` |
| Cobertura de las operaciones de admisión | Operaciones **de admisión** disponibles en el panel del administrador, sobre las 4 que esta necesidad declara: habilitar, bloquear, rehabilitar y dar de baja. El panel ofrece además una quinta operación, el **reseteo de contraseña**, que no es de admisión y se mide en NB-00002 | 4 de 4 | Punto de control de la etapa `d` |
| Protección de la operación destructiva | Bajas que se ejecutan sin que el administrador escriba el correo de la cuenta como confirmación | 0 | Punto de control de la etapa `d` |
| Advertencia previa a la baja | Confirmaciones de baja que declaran explícitamente que se eliminan también los trabajos de esa cuenta, sobre el total de confirmaciones de baja | 100 % | Punto de control de la etapa `d` |

Origen de cada criterio: el primero deriva de PRODUCT-INTAKE §4 (F-01) y de la transición `c` a `d` de `Roadmap-Producto.md` §5.2; el segundo, de PRODUCT-INTAKE §6 (flujo 1) y §4 (F-03); el tercero, de PRODUCT-INTAKE §4 (F-03); el cuarto y el quinto, de PRODUCT-INTAKE §7 (CL-6) —el §11 los apoyaba además en **RN-B6**, tachado el 2026-08-09—. Ninguno de estos cinco valores depende de la asunción A-2 del intake: los cinco son binarios o de recuento y están declarados en las fuentes.

## 6. Stakeholders involucrados

| Rol | Nivel | Qué pide o aporta |
| --- | --- | --- |
| Docente de Programación 2 (TUP), responsable de la cátedra y de la Actividad 1, en su papel de Product Owner | Propietario | Decidió que no hay canal de correo y que el producto tiene un único administrador; da el OK explícito en el punto de control de las etapas `c` y `d` |
| Cátedra de Programación 2, como dueño del problema | Propietario | Padece hoy la ausencia de una lista de participantes del laboratorio; fija el rumbo del laboratorio |
| El mismo docente, en su papel de equipo de desarrollo (una persona, asistida por un agente de IA) | Implementador | Construye el circuito de admisión y lo demuestra en el punto de control de cada etapa |
| El mismo docente, en su papel de administrador del laboratorio, con la cuenta única de administrador | Beneficiario y operador | Habilita, bloquea, rehabilita y da de baja cuentas; es quien ejecuta la admisión en cada cursada. Desde el mismo panel ejerce también el reseteo de contraseña, que pertenece a NB-00002 |
| Alumno de la comisión | Beneficiario | Recibe un aviso explícito de su situación —pendiente, habilitado o bloqueado— en lugar de un rechazo sin explicación |

## 7. Trazabilidad a CU

| NB | Casos de uso emitidos | Estado |
| --- | --- | --- |
| NB-00001 | `CU-00003`, `CU-00004`, `CU-00005`, `CU-02001`, `CU-02002`, `CU-02004`, `CU-02012`, `CU-02013`, `CU-04001`, `CU-04002`, `CU-04010`, `CU-04011`, `CU-06004`, `CU-06005`, `CU-06007` en `GeometriaFactory-Api`; `CU-10001`, `CU-10002`, `CU-10004` en `GeometriaFactory-Web` configurar la cuenta de administrador en el primer arranque | Emitidos |
| NB-00001 | `CU-00003`, `CU-00004`, `CU-00005`, `CU-02001`, `CU-02002`, `CU-02004`, `CU-02012`, `CU-02013`, `CU-04001`, `CU-04002`, `CU-04010`, `CU-04011`, `CU-06004`, `CU-06005`, `CU-06007` en `GeometriaFactory-Api`; `CU-10001`, `CU-10002`, `CU-10004` en `GeometriaFactory-Web` habilitar, bloquear y rehabilitar una cuenta de alumno | Emitidos |
| NB-00001 | `CU-00003`, `CU-00004`, `CU-00005`, `CU-02001`, `CU-02002`, `CU-02004`, `CU-02012`, `CU-02013`, `CU-04001`, `CU-04002`, `CU-04010`, `CU-04011`, `CU-06004`, `CU-06005`, `CU-06007` en `GeometriaFactory-Api`; `CU-10001`, `CU-10002`, `CU-10004` en `GeometriaFactory-Web` dar de baja una cuenta de alumno con confirmación escrita | Emitidos |

## 8. Dependencias con otras NB

- Depende de: sin dependencias. Es la raíz de la cadena de necesidades del producto.
- Es prerequisito de: NB-00002 (la habilitación del alumno la ejerce un administrador que tiene que existir antes) y NB-00007 (el administrador es quien revisa la comisión).

## 9. Prioridad MoSCoW

**Must Have.** Se deriva de PRODUCT-INTAKE §4, donde las dos capacidades que esta NB agrupa —F-01 y F-03— están declaradas Must Have; las dos pertenecen a la misma prioridad, de modo que no hay agregación de prioridades distintas.

## 10. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Articula la necesidad de admisión y de bajas del laboratorio a partir de las capacidades F-01 y F-03 del intake, con cinco criterios de éxito trazados a su sección de origen y tres casos de uso previstos. |
| 1.1 | 2026-08-09 | **Cierra la parte del hallazgo `F26-02` que alcanza a este archivo**, del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0, contra `PRODUCT-INTAKE` **1.9**. La auditoría encontró que **§2 enseñaba que la única forma de resolver una cuenta perdida es darla de baja y volver a darla de alta**, que es exactamente el procedimiento que la capacidad **F-26** vino a reemplazar y que el intake ya no declara. **Sube minor y archiva el estado anterior** porque el documento ya es citado como insumo por otras categorías (`Master-Prompt.md` §5). **§1**: el párrafo de la restricción se parte en dos; la baja deja de presentarse como la salida del olvido de contraseña, se declara la incorporación de F-26 y se declara **por qué F-26 no pertenece a esta necesidad sino a NB-00002** pese a compartir el panel —por RN-15 el reseteo no es una transición de la máquina de estados y no admite ni excluye a nadie—. **§2**: el ejemplo deja de resolver el olvido con una baja y distingue los dos casos, el alumno que deja de cursar y el que olvidó la clave. **§3**: se corrige el impacto de no resolver la necesidad, que citaba la salida destructiva, y el riesgo residual declara que ya no alcanza al olvido de contraseña. **§5**: el tercer criterio pasa a medir las **operaciones de admisión** y declara que el panel tiene una quinta operación, el reseteo, que se mide en NB-00002; el target sigue siendo 4 de 4 y ningún otro criterio cambia. **§6**: la fila del administrador registra que ejerce el reseteo desde el mismo panel. Ninguna prioridad, dependencia ni caso de uso previsto de esta NB cambia. |
| 1.2 | 2026-08-10 | **Cierra la parte del hallazgo `N-5`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r2.md` 1.0 que alcanza a esta necesidad. Citaba el riesgo **`RN-B6`** del intake §11 como vigente en **cuatro** lugares —trazabilidad de cabecera, §2, §4 y §5—, contados sobre el texto vivo; el informe había nombrado tres. El intake **1.10** tachó `RN-B6` el 2026-08-09, porque F-26 conserva la cuenta y sus trabajos y dejó sin objeto su mitigación «advertirlo antes de dar de baja». **Ninguna conclusión de esta NB cambia**: lo que las cuatro citas sostenían —que la baja arrastra los trabajos y que la pérdida es irreversible— vive en el caso límite **CL-6** de §7 del intake y en `RN-07` de `GeometriaFactory-Domain`, y a eso pasan a anclar. Las cuatro citas se conservan con la constancia de que la fila está tachada, en lugar de borrarse, para que no se lea como si el riesgo nunca hubiera existido. **Ningún criterio de éxito, prioridad, dependencia ni caso de uso previsto cambia.** Sube minor: corrige referencias a una fila retirada. |
