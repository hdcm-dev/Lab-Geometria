# US-04025 — Rechazar toda transición pedida por quien no tiene la facultad o desde un estado terminal

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-04025-Rechazar-Toda-Transicion-Sin-Facultad-O-Desde-Un-Estado-Terminal.md
**Versión:** 2.0
**Estado:** Aprobada
**Fecha:** 2026-08-25
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-04006 Desenlace de la entrega
**Etapa del producto:** `h`
**Prioridad MoSCoW:** Must
**Estimación:** **No aplica** — el producto no estima; ver §5.b

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que toda transición de desenlace pedida sin facultad o desde un estado terminal se rechace con motivos distinguibles**, para **que forzar la petición contra el servicio no logre nada y para saber cuál de las dos cosas falló**.

## 2. Contexto

`RN-04010` declara el desenlace exclusivo del administrador y los dos estados de cierre terminales. El contrato de uso es [`CU-00029`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00029-Dar-Desenlace-A-La-Revision.md). El criterio de transición `h` → `i…` exige que **un alumno que fuerce la transición contra el servicio de datos sea rechazado**.

## 3. Criterios de aceptación

- Given un solicitante sin el papel `Administrador`, When pide aprobar o rechazar, Then se devuelve el motivo de **facultad requerida** y **el estado no cambia**.
- Given un trabajo en `Finalizado` o en `Rechazado` y un solicitante con la facultad correcta, When pide un desenlace, Then se devuelve el motivo del dominio por **estado terminal**, distinguible del anterior.
- Given los dos rechazos, When se comparan los motivos emitidos, Then **no se colapsan en uno**: la facultad se verifica antes de pedir la transición, precisamente para que se puedan distinguir.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00009 |
| CU cubiertos | CU-04008 |
| RN e invariantes que ejerce | RN-04001, RN-04010; INV-07 |
| Componente de `05` §3.1 | Guarda de autorización, Orquestación del desenlace |
| Puertos que consume | Repositorio de trabajos |
| Comprobación de `02` §4 que la alcanza | **Facultad**, **alcance del administrador** y cambio de contraseña pendiente |
| BT derivadas | BT-04008, BT-04010, BT-04011, BT-04017 |
| Tests previstos en 08 | Matriz comprobación contra prueba de `05` §8, con las dos negativas verificadas sin base de datos |

## 5. Prioridad

`Must` porque es el criterio de la transición `h` → `i…` que verifica la exclusividad de la facultad, y porque `05` §8 exige **4 de 4** comprobaciones con al menos una prueba que verifique su negativa.

## 5.b Estimación — **no aplica**, y por qué

**Esta subsección realiza el ítem 5.b de `Rules-Backlog-Tecnico.md` §4.4**, que desde la regla **5.0**
separa la **estimación** —del equipo, sale del refinamiento— de la **prioridad** —del Product Owner—.
Lo que las separa no es un evento sino un dueño: que el refinamiento no haya ocurrido no impide
priorizar, y que la prioridad esté abierta no impide estimar.

**No se estima, y no está diferida.** Este producto **no planifica por estimación**: planifica por
**etapas con punto de control bloqueante**, y eso no es una carencia sino su modelo declarado.

| Aspecto | Valor |
|---|---|
| **Unidad de estimación** | **Ninguna.** El producto no estima |
| **Por qué no tiene objeto** | `PRODUCT-INTAKE` §2 declara **`equipo_n = 1`**, y de ese dato el framework deriva que la categoría 07 emita **únicamente** `Mini-Plan.md`. [`../../07-Plan-Sprint/Mini-Plan.md`](../../07-Plan-Sprint/Mini-Plan.md) §1.2 lo declara sin rodeos: *«**No se declara capacidad numérica, y es deliberado.** Ninguna fuente da base: sin plazo calendario, sin iteraciones cerradas y con una sola persona»* |
| **Qué ocupa su lugar** | El **punto de control de cada etapa**, que `PRODUCT-INTAKE` §10 y §15 declaran bloqueante, y que `Mini-Plan.md` §1.2 nombra **el cuello de diseño** del producto |
| **Qué lo reabriría** | Que el producto pase a planificar por iteraciones, o que `equipo_n` deje de ser 1 |

**Y el hecho que lo cierra, que es lo que lo vuelve una lectura y no una decisión.** **Ocho etapas
—`a` a `h`— se planificaron, se construyeron, se demostraron y se cerraron sin una sola estimación**,
con su registro en [`../../../../../../changelog.md`](../../../../../../changelog.md). Un ítem que pregunta
por un instrumento que el producto **no usó en ocho etapas** no está esperando una decisión: **está
sin objeto**, con la figura que [`../../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md`](../../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md)
declara.

**Lo que decía antes, y por qué era falso.** Decía «Estimación: sin fijar», remitiendo al `PA-01` de
[`../Product-Backlog.md`](../Product-Backlog.md) §6 — un punto abierto **diferido al punto de control
de la etapa `c`, que cerró el 2026-08-14 sin registrarlo**. Estaba **vencido**, y con la forma nueva
habría entrado a este documento como hallazgo **P1** por la tabla de escalamiento de
`Root-Rules.md` §12.2.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el componente de `05` §3.1 y los puertos que consume
- [x] Declara qué comprobación de `02` §4 la alcanza
- [x] Las condiciones de rechazo que produce existen en el catálogo de las 36 de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Se puede verificar con dobles de los cuatro puertos, sin base de datos

## 7. Notas y supuestos

**Esta capa emite una sola negativa de facultad y el dominio declara dos motivos para la misma situación** (`02` §4): esta capa corta con su propia verificación **antes** de invocar al dominio, de modo que ninguno de los dos motivos del dominio llega a producirse. Quien lea las dos capas no debe leer tres negativas donde hay una.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador. |
| 2.0 | 2026-08-25 | **Migración normativa 10.0 → 13.3, fase M4, corte de la categoría 06** (`Audit/Plan-Migracion-10.0-a-13.3.md` **1.2** §4.1). **§5 se parte en 5 · prioridad y 5.b · estimación**, que es lo que `Rules-Backlog-Tecnico.md` **5.0** §4.4 exige desde el salto: lo que separa las dos mitades **no es un evento sino un dueño**. **La estimación se declara «no aplica» y no se difiere**, cerrada **por lectura y no por decisión**: `PRODUCT-INTAKE` §2 declara `equipo_n = 1`, `Mini-Plan.md` §1.2 declara que **no hay capacidad numérica y es deliberado**, y el hecho que lo cierra es que **ocho etapas se cerraron sin una sola estimación**. La forma es la de [`../../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md`](../../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md), `Propuesto`. **Lo anterior remitía a un ítem vencido** —`PA-01`, diferido al punto de control de la etapa `c`, que cerró el 2026-08-14—, que con la forma nueva habría entrado acá como **P1**. Estado previo archivado en [`_legacy/2026-08-25/US-04025-Rechazar-Toda-Transicion-Sin-Facultad-O-Desde-Un-Estado-Terminal-v1.0.md`](_legacy/2026-08-25/US-04025-Rechazar-Toda-Transicion-Sin-Facultad-O-Desde-Un-Estado-Terminal-v1.0.md). Sube **major**: el salto de la regla que lo gobierna es major. |
