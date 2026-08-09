# NB-01 — Control de admisión y de bajas del laboratorio

| Campo | Valor |
| --- | --- |
| Producto | Fábrica de Geometría |
| Documento | NB-01-Control-De-Admision-Al-Laboratorio.md |
| Versión | 1.0 |
| Estado | Propuesto |
| Fecha | 2026-08-08 |
| Autor | Analista de Negocio Senior (AG-01) |
| Trazabilidad upstream | PRODUCT-INTAKE §1 (idea y problema), §3 (propuesta de valor), §4 (capacidades F-01 y F-03), §7 (caso límite CL-6), §9 (exclusión X-3), §11 (riesgo RN-B6); `Vision-Producto.md` §1, §2 y §9; `Alcance-Producto.md` §4.1 y §5 |
| Trazabilidad downstream | CU-01, CU-02, CU-03 (previstas en 02-Especificacion-Funcional); 06-Backlog-Tecnico, 07-Plan-Sprint, 08-Calidad-Y-Pruebas |

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

La restricción que ordena esta necesidad es que **no hay canal de correo** en el producto, por decisión declarada del Product Owner (PRODUCT-INTAKE §9, exclusión X-1). Eso descarta la forma habitual de autorizar altas y obliga a que la admisión sea un acto explícito del docente sobre una lista visible. La misma decisión hace que la única forma de resolver una cuenta perdida sea darla de baja y volver a darla de alta, y que esa baja arrastre los trabajos de esa cuenta (PRODUCT-INTAKE §7, CL-6, y §11, RN-B6): es una operación destructiva que el producto tiene que hacer difícil de ejecutar por accidente.

La necesidad también fija su propio techo. El producto es deliberadamente básico: un solo administrador, dos papeles fijos y ningún esquema de permisos configurables (PRODUCT-INTAKE §9, exclusión X-3). Resolver la admisión no es construir una administración de identidades; es darle al docente el control mínimo y suficiente sobre la lista de su comisión.

## 2. Ejemplo de uso desde la perspectiva del negocio

Arranca la cursada. El docente abre el laboratorio por primera vez y configura su propia cuenta de administrador; a partir de ese momento nadie más puede configurar otra. Durante la primera semana, veinticuatro alumnos se registran y quedan a la espera. El docente entra a su panel, los ve listados, reconoce a los de su comisión y los habilita; a dos que no reconoce los deja sin habilitar. Más adelante, un alumno pierde su contraseña y no hay forma de recuperarla, así que el docente da de baja la cuenta —el producto le exige escribir el correo de esa cuenta para confirmar, y le advierte que se van también los trabajos cargados— y el alumno vuelve a registrarse desde cero.

## 3. Impacto

- Si se resuelve: la comisión queda delimitada y el docente sabe en todo momento quiénes están habilitados, bloqueados o fuera del laboratorio.
- Si se resuelve: toda la cadena posterior tiene sobre qué apoyarse, porque un trabajo sólo puede tener dueño si antes existe una cuenta habilitada.
- Si se resuelve: la ausencia de canal de correo deja de ser un impedimento y pasa a ser un rasgo del circuito, resuelto con un acto explícito del docente.
- Si queda sin resolver: no hay forma de distinguir a un alumno de la comisión de cualquier otra persona, y ninguna de las capacidades comprometidas posteriores tiene fundamento.
- Si queda sin resolver: la única salida ante una cuenta perdida quedaría fuera del producto, y el docente volvería a depender de mirar la pantalla del alumno.
- Riesgo residual aceptado: la baja elimina también los trabajos de la cuenta, y esa pérdida es irreversible (PRODUCT-INTAKE §11, RN-B6).

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
| Cobertura de las operaciones sobre una cuenta | Operaciones disponibles en el panel del administrador, sobre las 4 declaradas: habilitar, bloquear, rehabilitar y dar de baja | 4 de 4 | Punto de control de la etapa `d` |
| Protección de la operación destructiva | Bajas que se ejecutan sin que el administrador escriba el correo de la cuenta como confirmación | 0 | Punto de control de la etapa `d` |
| Advertencia previa a la baja | Confirmaciones de baja que declaran explícitamente que se eliminan también los trabajos de esa cuenta, sobre el total de confirmaciones de baja | 100 % | Punto de control de la etapa `d` |

Origen de cada criterio: el primero deriva de PRODUCT-INTAKE §4 (F-01) y de la transición `c` a `d` de `Roadmap-Producto.md` §5.2; el segundo, de PRODUCT-INTAKE §6 (flujo 1) y §4 (F-03); el tercero, de PRODUCT-INTAKE §4 (F-03); el cuarto y el quinto, de PRODUCT-INTAKE §7 (CL-6) y §11 (RN-B6). Ninguno de estos cinco valores depende de la asunción A-2 del intake: los cinco son binarios o de recuento y están declarados en las fuentes.

## 6. Stakeholders involucrados

| Rol | Nivel | Qué pide o aporta |
| --- | --- | --- |
| Docente de Programación 2 (TUP), responsable de la cátedra y de la Actividad 1, en su papel de Product Owner | Propietario | Decidió que no hay canal de correo y que el producto tiene un único administrador; da el OK explícito en el punto de control de las etapas `c` y `d` |
| Cátedra de Programación 2, como dueño del problema | Propietario | Padece hoy la ausencia de una lista de participantes del laboratorio; fija el rumbo del laboratorio |
| El mismo docente, en su papel de equipo de desarrollo (una persona, asistida por un agente de IA) | Implementador | Construye el circuito de admisión y lo demuestra en el punto de control de cada etapa |
| El mismo docente, en su papel de administrador del laboratorio, con la cuenta única de administrador | Beneficiario y operador | Habilita, bloquea, rehabilita y da de baja cuentas; es quien ejecuta la admisión en cada cursada |
| Alumno de la comisión | Beneficiario | Recibe un aviso explícito de su situación —pendiente, habilitado o bloqueado— en lugar de un rechazo sin explicación |

## 7. Trazabilidad a CU

| NB | CU prevista | Estado |
| --- | --- | --- |
| NB-01 | CU-01 configurar la cuenta de administrador en el primer arranque | a generar |
| NB-01 | CU-02 habilitar, bloquear y rehabilitar una cuenta de alumno | a generar |
| NB-01 | CU-03 dar de baja una cuenta de alumno con confirmación escrita | a generar |

## 8. Dependencias con otras NB

- Depende de: sin dependencias. Es la raíz de la cadena de necesidades del producto.
- Es prerequisito de: NB-02 (la habilitación del alumno la ejerce un administrador que tiene que existir antes) y NB-07 (el administrador es quien revisa la comisión).

## 9. Prioridad MoSCoW

**Must Have.** Se deriva de PRODUCT-INTAKE §4, donde las dos capacidades que esta NB agrupa —F-01 y F-03— están declaradas Must Have; las dos pertenecen a la misma prioridad, de modo que no hay agregación de prioridades distintas.

## 10. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Articula la necesidad de admisión y de bajas del laboratorio a partir de las capacidades F-01 y F-03 del intake, con cinco criterios de éxito trazados a su sección de origen y tres casos de uso previstos. |
