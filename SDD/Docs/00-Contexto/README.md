# 00 · Contexto del producto

**Producto:** Fábrica de Geometría
**Documento:** README.md
**Versión:** 1.4
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Product Manager Senior (AG-00), actuando también como Analista de Negocio Senior (AG-01) por `Rules-Contexto.md` §1.3
**Trazabilidad upstream:** PRODUCT-INTAKE 1.9 §2 (stakeholders y cantidad de personas del equipo), §4.2 (colisión de vocabulario de `Pendiente`), §13 (composición del producto), PRODUCT-MANIFEST §5 (flag `equipo_n`)
**Trazabilidad downstream:** 01-Necesidades-Negocio, 02-Especificacion-Funcional, 03-UX-UI-DX, 05-Arquitectura-Tecnica, 07-Plan-Sprint, 09-Devops, 10-Examples

---

## Tabla de contenido

- [1. Qué hay en esta carpeta](#1-qué-hay-en-esta-carpeta)
- [2. Orden de lectura sugerido](#2-orden-de-lectura-sugerido)
- [3. Documento omitido y su motivo](#3-documento-omitido-y-su-motivo)
- [4. Stakeholders del producto](#4-stakeholders-del-producto)
- [5. Notas de uso de esta sección](#5-notas-de-uso-de-esta-sección)
- [6. Control de cambios](#6-control-de-cambios)

---

## 1. Qué hay en esta carpeta

Esta categoría es de **nivel producto**: se genera una sola vez, para Fábrica de Geometría entero, y no por proyecto de código. Es el inicio de la cadena de trazabilidad: no tiene documentos upstream más allá del `PRODUCT-INTAKE`.

| Documento | Propósito | Estado |
|---|---|---|
| `Vision-Producto.md` | Por qué existe el producto, a quién sirve, qué promete, sus objetivos con métrica y plazo, sus restricciones, sus riesgos y el **glosario raíz del dominio** | Propuesto, versión 1.3 |
| `Alcance-Producto.md` | Qué entra y qué no entra, con la justificación de cada exclusión, los supuestos de los que depende el alcance y los criterios de aceptación del producto | Propuesto, versión 1.4 |
| `Roadmap-Producto.md` | Las ocho fases comprometidas, `a` a `h`, más el tramo pendiente `i…`, con sus dependencias y los criterios verificables de transición. Sin fechas, por decisión declarada aguas arriba | Propuesto, versión 1.4 |
| `Compatibilidad-Plataformas.md` | Las tres matrices de plataforma del producto, sus divergencias, la regla de precedencia y el estado de verificación de cada una | Propuesto, versión 1.1 |
| `README.md` | Este archivo: índice de la sección, orden de lectura, omisiones declaradas y stakeholders | Propuesto, versión 1.4 |

## 2. Orden de lectura sugerido

1. **`Vision-Producto.md`** — primero siempre. Su §9 es el glosario raíz de toda la cadena de documentación: las categorías 02 y 03 referencian sus términos en lugar de redefinirlos, y su §9.3 declara la resolución vinculante del choque de vocabulario del término «proyecto».
2. **`Alcance-Producto.md`** — qué se comprometió y qué se excluyó. Es la lectura obligatoria antes de derivar casos de uso, para no generar ninguno sobre una capacidad excluida.
3. **`Roadmap-Producto.md`** — en qué orden se construye y con qué criterios se cierra cada fase.
4. **`Compatibilidad-Plataformas.md`** — se puede leer suelto: es el insumo directo de la categoría 09 y no depende de los anteriores para entenderse.

## 3. Documento omitido y su motivo

**`Acuerdo-Equipo.md` no se genera.**

Motivo: `equipo_n = 1`. El intake declara «1 docente + agente IA», y el agente de IA no es una persona del equipo, de modo que el equipo de desarrollo es de **una sola persona, sin cliente externo** (PRODUCT-INTAKE §2, PRODUCT-MANIFEST §5).

Fundamento normativo:

- `Rules-Contexto.md` §2.1 declara `Acuerdo-Equipo.md` obligatorio para equipos de más de dos personas, recomendado para equipos de dos que coordinan con externos, y **omitido para proyectos de código de un solo desarrollador sin equipo**.
- `Rules-Contexto.md` §1.3 declara que en proyectos de código de un desarrollador sin cliente externo, AG-00 actúa también como AG-01 y el acuerdo de equipo queda omitido. Por eso los cinco documentos de esta carpeta llevan la doble especialidad en el campo de autor.

Lo que ese documento habría cubierto y que **sí está declarado en otro lado**, para que la omisión no deje un hueco:

| Contenido típico de `Acuerdo-Equipo.md` | Dónde vive en este producto |
|---|---|
| Ceremonias y cadencia | No aplica: no hay ceremonias con un equipo de una persona. La detención periódica es el punto de control de cada etapa (`Roadmap-Producto.md` §5) |
| Estrategia de ramas | Declarada en el intake: una rama y una solicitud de incorporación por etapa, en serie, con etiqueta al incorporar. Baja a la categoría 09 |
| Revisión de cambios | La solicitud de incorporación de la etapa **es** el punto de control, y la revisa el Product Owner (`Roadmap-Producto.md` §5.1) |
| Definición de terminado | Los criterios de transición de `Roadmap-Producto.md` §5, que bajan a la categoría 08 |
| Herramientas | Declaradas por proyecto de código en el intake; bajan a las categorías 05 y 09 |

## 4. Stakeholders del producto

Con rol concreto, sin genéricos. La tabla completa, con su nivel de involucramiento, está en `Vision-Producto.md` §2.

| Rol concreto | Categoría | Qué decide o qué recibe |
|---|---|---|
| Docente de Programación 2 (TUP), responsable de la cátedra y de la Actividad 1, en su papel de **Product Owner** | Propietario | Aprueba el intake, arbitra prioridades y exclusiones, y da el OK explícito en cada punto de control |
| Cátedra de Programación 2, como **dueño del problema** | Propietario | Padece la falta de entrega y de revisión; decide el rumbo del laboratorio |
| El mismo docente, en su papel de **equipo de desarrollo** (una persona, asistida por un agente de IA) | Implementador | Construye y mantiene; valida y fusiona lo que el agente desarrolla por etapas |
| **Alumno de la comisión** | Beneficiario | Carga sus trabajos, los previsualiza en tres dimensiones y los entrega |
| El mismo docente, en su papel de **administrador del laboratorio**, con la cuenta única de administrador | Beneficiario y operador | Habilita, bloquea, rehabilita, **resetea la contraseña** y da de baja cuentas; revisa, filtra y agrupa los trabajos de toda la comisión |

Product Owner, responsable técnico, único desarrollador humano y administrador **son la misma persona**. Los papeles no se fusionan porque las responsabilidades son distintas y el punto de control de cada etapa es explícitamente el momento en que el docente cambia de papel (PRODUCT-INTAKE §2).

## 5. Notas de uso de esta sección

- **Vocabulario.** La palabra «proyecto» a secas no se usa en ninguna documentación de este producto. «Proyecto de código» designa exclusivamente una unidad de compilación; las dos unidades de la Actividad 1 que emiten el dato se nombran `Ejemplo1` y `Ejemplo2`. La resolución completa está en `Vision-Producto.md` §9.3.
- **Autoridad.** Nada de esta carpeta origina una prioridad, una exclusión, una fecha, un target de métrica ni un criterio de transición: todo deriva del `PRODUCT-INTAKE` y traza a su sección de origen.
- **Valores pendientes de confirmación.** Los targets de las cuatro métricas de negocio están rotulados como asunción en el intake y esperan confirmación del Product Owner (`Alcance-Producto.md` §6.1). Se usan como valores vigentes hasta que la confirmación llegue.
- **Punto abierto resuelto.** El alcance del objetivo OBJ-01 estuvo escalado al Product Owner mientras su target contaba siete etapas y el alcance comprometido eran ocho. **El Product Owner lo resolvió** el 2026-08-09 en `PRODUCT-INTAKE` §8: se cuentan las ocho comprometidas, y `Vision-Producto.md` §5 y §6 ya lo declaran así. **No queda residuo en la fuente**: el intake **1.10** corrigió también la fila A-2 de §22, que hoy transcribe «8 de 8 etapas» con la constancia de qué decía antes. Verificado sobre el texto vivo del intake, no sobre lo que otro documento dice de él.
- **Habilita, bloquea, da de baja... y resetea.** Desde el 2026-08-09 el panel del administrador tiene una quinta operación, el **reseteo de contraseña** (capacidad F-26), que resuelve el olvido **sin dar de baja la cuenta**. Ninguna documentación de esta carpeta puede volver a describir la baja y el alta como la salida del olvido de contraseña: ése es el procedimiento que F-26 reemplazó, y su exclusión X-2 está retirada (`Alcance-Producto.md` §5).
- **`Pendiente` va siempre calificado.** El término nombra un estado de cuenta y un estado de trabajo. En toda la documentación se escribe «cuenta `Pendiente`» o «trabajo en estado `Pendiente`»; la forma desnuda no se usa (`Vision-Producto.md` §9.2).
- **Nombres de archivo.** Ningún archivo vivo de esta carpeta lleva sufijo de versión: cada uno declara su versión en el campo `Versión` de su cabecera.

## 6. Control de cambios

| Versión | Fecha | Cambios | Autor |
|---|---|---|---|
| 1.0 | 2026-08-08 | Emisión inicial del índice de la sección. Enumera los cuatro documentos generados con su propósito y estado, fija el orden de lectura, declara la omisión de `Acuerdo-Equipo.md` por `equipo_n = 1` con su fundamento normativo y con el destino de cada contenido que ese documento habría cubierto, y lista los cinco stakeholders con rol concreto y categoría de la tríada. | Product Manager Senior (AG-00) |
| 1.1 | 2026-08-08 | Absorbe el circuito de revisión del administrador incorporado por el Product Owner en `PRODUCT-INTAKE` 1.3. Sube minor y archiva el estado anterior porque el documento ya es citado como insumo por otras categorías (`Master-Prompt.md` §5). **§1**: los cuatro documentos pasan a versión 1.1 y el roadmap se describe como ocho fases comprometidas más el tramo pendiente. **§5**: las métricas de negocio pasan de tres a cuatro, entra la nota del punto abierto de OBJ-01 escalado al Product Owner y la nota de la forma calificada obligatoria de `Pendiente`. Los stakeholders, la omisión de `Acuerdo-Equipo.md` y el orden de lectura no cambian. | Product Manager Senior (AG-00) |
| 1.2 | 2026-08-09 | Absorbe la incorporación de la capacidad **F-25**, movimiento automático de la escena, que el Product Owner declara en `PRODUCT-INTAKE` 1.5 §4 a partir de la **validación visual de la Fase B2**, aprobada tras cuatro iteraciones. **Sube minor y archiva el estado anterior** porque el documento ya es citado como insumo por cinco proyectos de código (`Master-Prompt.md` §5). **§1**: `Alcance-Producto.md` y `Roadmap-Producto.md` pasan a versión 1.2 y este archivo también; `Vision-Producto.md` y `Compatibilidad-Plataformas.md` **siguen en 1.1 y no se tocaron**, porque F-25 no cambia la visión, sus objetivos, su glosario ni ninguna matriz de plataforma. Los stakeholders, el orden de lectura, la omisión de `Acuerdo-Equipo.md` y las cinco notas de uso no cambian: no hay métrica, target ni recuento de esta sección afectado, y el punto abierto de las siete u ocho etapas de OBJ-01 sigue escalado al Product Owner. | Product Manager Senior (AG-00) |
| 1.3 | 2026-08-09 | **Cierra la parte de los hallazgos `F26-02`, `F26-03` y `F26-06` que alcanza a este índice**, del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0, contra `PRODUCT-INTAKE` **1.9**. **Sube minor y archiva el estado anterior** por `Master-Prompt.md` §5. **§1**: los cuatro documentos actualizan su versión —`Vision-Producto.md` a **1.2**, `Alcance-Producto.md` a **1.3**, `Roadmap-Producto.md` a **1.4** y este archivo a **1.3**—; `Compatibilidad-Plataformas.md` **sigue en 1.1 y no se tocó**, porque ni F-26 ni la promoción de F-25 cambian ninguna matriz de plataforma. **§4**: la fila del administrador del laboratorio suma el **reseteo de contraseña** a las operaciones que ejerce, que es la quinta del panel desde el 2026-08-09. **§5 (`F26-06`)**: la nota del punto abierto escalado pasa a declararlo **resuelto**, con la decisión del Product Owner del intake §8 y con la constancia del residuo que quedó en §22 A-2; y entra una nota nueva que fija que la baja y el alta dejaron de ser la salida del olvido de contraseña, para que ninguna emisión posterior de esta carpeta la reponga. Los stakeholders restantes, el orden de lectura y la omisión de `Acuerdo-Equipo.md` no cambian. | Product Manager Senior (AG-00) |
| 1.4 | 2026-08-10 | **Cierra la parte del hallazgo `N-1`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r2.md` 1.0 que alcanza a este índice, contra el texto vivo del `PRODUCT-INTAKE` **1.10**. La nota de punto abierto resuelto terminaba diciendo que «lo único que subsiste es un residuo de la fuente: la fila A-2 del intake §22 todavía transcribe “7 de 7”, y corregirla es del Product Owner sobre su propio documento». **No subsiste**: el intake 1.10 corrigió esa fila en el mismo commit en que este índice pasó a 1.3, y §22 A-2 transcribe hoy «8 de 8 etapas». La nota pasa a declarar el punto cerrado en la fuente y a decir contra qué se verificó. **§1** actualiza además las versiones de los tres documentos de la carpeta que la misma corrección movió: `Vision-Producto.md` a **1.3**, `Alcance-Producto.md` a **1.4** y este índice a **1.4**. **Ningún documento del índice, orden de lectura ni omisión cambia.** Sube minor: corrige una afirmación sobre otra fuente y sincroniza el recuento de versiones. | Product Manager Senior (AG-00) |
