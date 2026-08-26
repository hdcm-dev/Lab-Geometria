# US-04031 — Conservar la cuenta, su estado de habilitación y todos sus trabajos después del reseteo

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-04031-Conservar-La-Cuenta-Su-Estado-Y-Todos-Sus-Trabajos-Tras-El-Reseteo.md
**Versión:** 2.0
**Estado:** Aprobada
**Fecha:** 2026-08-25
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-04003 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Prioridad MoSCoW:** Must
**Estimación:** **No aplica** — el producto no estima; ver §5.b

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que el reseteo deje intactos la identidad, el papel, el estado de habilitación y todos los trabajos de la cuenta**, para **que recuperar una contraseña olvidada no le cueste al alumno lo que ya entregó**.

## 2. Contexto

`RN-04012` declara que el reseteo conserva la cuenta y sus trabajos y que **no dispara `RN-04007`**. El contrato de uso es [`CU-00024`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00024-Resetear-La-Contrasena-De-Un-Alumno.md). `05` §10.2 declara el tramo de esta capa: la postcondición que deja todo intacto y la **ausencia deliberada** de todo retiro.

## 3. Criterios de aceptación

- Given un alumno con tres trabajos en tres estados distintos y con sus comentarios, When se resetea su contraseña, Then conserva los tres trabajos con sus estados y sus comentarios.
- Given esa misma cuenta, When se compara su estado de habilitación, su papel y su identidad antes y después, Then **no cambió ninguno de los tres**.
- Given el caso de uso de reseteo, When se inspecciona qué operaciones de retiro invoca, Then son exactamente **0**: el reseteo no pasa por ninguna ruta de retiro.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00001, NB-00002 |
| CU cubiertos | CU-04011 |
| RN e invariantes que ejerce | RN-04007 por contraste, RN-04012, RN-04015 |
| Componente de `05` §3.1 | Orquestación del gobierno de cuentas |
| Puertos que consume | Repositorio de cuentas, repositorio de trabajos |
| Comprobación de `02` §4 que la alcanza | Facultad, y cambio de contraseña pendiente antes que ella |
| BT derivadas | BT-04007, BT-04013 |
| Tests previstos en 08 | Prueba con un alumno con trabajos en tres estados, comprobando la conservación completa |

## 5. Prioridad

`Must` por `RN-04012` y porque el criterio de transición `d` → `e` lo exige verificado **sobre un alumno con trabajos en tres estados distintos y con sus comentarios**.

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

**El contraste con US-04006 es el punto de la historia.** La baja arrastra todos los trabajos; el reseteo no toca ninguno. Que las dos operaciones vivan en el mismo panel del administrador es exactamente el motivo por el que la distinción tiene que ser verificable y no sólo declarada.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador. |
| 2.0 | 2026-08-25 | **Migración normativa 10.0 → 13.3, fase M4, corte de la categoría 06** (`Audit/Plan-Migracion-10.0-a-13.3.md` **1.2** §4.1). **§5 se parte en 5 · prioridad y 5.b · estimación**, que es lo que `Rules-Backlog-Tecnico.md` **5.0** §4.4 exige desde el salto: lo que separa las dos mitades **no es un evento sino un dueño**. **La estimación se declara «no aplica» y no se difiere**, cerrada **por lectura y no por decisión**: `PRODUCT-INTAKE` §2 declara `equipo_n = 1`, `Mini-Plan.md` §1.2 declara que **no hay capacidad numérica y es deliberado**, y el hecho que lo cierra es que **ocho etapas se cerraron sin una sola estimación**. La forma es la de [`../../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md`](../../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md), `Propuesto`. **Lo anterior remitía a un ítem vencido** —`PA-01`, diferido al punto de control de la etapa `c`, que cerró el 2026-08-14—, que con la forma nueva habría entrado acá como **P1**. Estado previo archivado en [`_legacy/2026-08-25/US-04031-Conservar-La-Cuenta-Su-Estado-Y-Todos-Sus-Trabajos-Tras-El-Reseteo-v1.0.md`](_legacy/2026-08-25/US-04031-Conservar-La-Cuenta-Su-Estado-Y-Todos-Sus-Trabajos-Tras-El-Reseteo-v1.0.md). Sube **major**: el salto de la regla que lo gobierna es major. |
