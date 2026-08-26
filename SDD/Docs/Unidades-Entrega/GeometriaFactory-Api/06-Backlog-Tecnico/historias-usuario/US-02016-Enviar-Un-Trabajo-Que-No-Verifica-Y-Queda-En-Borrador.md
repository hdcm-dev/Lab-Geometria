# US-02016 — Enviar un trabajo que no verifica y queda en `Borrador` con sus errores

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-02016-Enviar-Un-Trabajo-Que-No-Verifica-Y-Queda-En-Borrador.md
**Versión:** 2.0
**Estado:** Aprobada
**Fecha:** 2026-08-25
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-02005 Interpretación y verificación del dato del alumno
**Etapa del producto:** `f`
**Prioridad MoSCoW:** Must
**Estimación:** **No aplica** — el producto no estima; ver §5.b

## 1. Historia

Como **código consumidor de la biblioteca de dominio (`GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`)**, quiero **que el envío de un trabajo cuyo texto no verifica lo deje en `Borrador` con sus errores localizados**, para **que el alumno no pierda lo cargado y sepa exactamente qué corregir**.

## 2. Contexto

Es la otra mitad de la capacidad `F-22` del intake §4. `RN-02005` declara que un trabajo no pasa a estado `Pendiente` con errores de interpretación, e `INV-04` lo expresa como condición permanente.

## 3. Criterios de aceptación

- Given un trabajo en `Borrador` cuyo resultado de interpretación trae al menos un error de validación, When se envía, Then queda en `Borrador` y con sus errores adoptados y localizados.
- Given ese mismo trabajo, When se consulta después del envío fallido, Then conserva su texto original íntegro, por `RN-02008`.
- Given ese trabajo en `Borrador`, When el alumno lo reedita y lo vuelve a enviar con un texto que verifica, Then pasa a estado `Pendiente`, que es US-02015.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00003, NB-00004, NB-00005 |
| CU cubiertos | CU-02008 |
| RN e invariantes que ejerce | RN-02005, RN-02008; INV-04 |
| BT derivadas | BT-02012, BT-02013 |
| Etapa del producto | `f`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba unitaria sobre el escenario `E-5` del intake §20 como entrada con error, y verificación de que el estado no cambió. |

## 5. Prioridad

`Must` por derivar de `F-22`, `Must Have` en `PRODUCT-INTAKE` §4.

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

- [x] Declara al menos un caso de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Cita por identificador toda regla e invariante que ejerce, sin volver a enunciarla
- [x] Las condiciones de rechazo que produce existen en el catálogo de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Sus tareas técnicas están identificadas y ninguna está bloqueada

## 7. Notas y supuestos

Que el trabajo quede en `Borrador` **no** es un fallo del envío: es su resultado declarado. La distinción importa porque el consumidor no debe traducirlo como error de la operación.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.3 previó con este mismo identificador y este mismo contenido. |
| 2.0 | 2026-08-25 | **Migración normativa 10.0 → 13.3, fase M4, corte de la categoría 06** (`Audit/Plan-Migracion-10.0-a-13.3.md` **1.2** §4.1). **§5 se parte en 5 · prioridad y 5.b · estimación**, que es lo que `Rules-Backlog-Tecnico.md` **5.0** §4.4 exige desde el salto: lo que separa las dos mitades **no es un evento sino un dueño**. **La estimación se declara «no aplica» y no se difiere**, cerrada **por lectura y no por decisión**: `PRODUCT-INTAKE` §2 declara `equipo_n = 1`, `Mini-Plan.md` §1.2 declara que **no hay capacidad numérica y es deliberado**, y el hecho que lo cierra es que **ocho etapas se cerraron sin una sola estimación**. La forma es la de [`../../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md`](../../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md), `Propuesto`. **Lo anterior remitía a un ítem vencido** —`PA-01`, diferido al punto de control de la etapa `c`, que cerró el 2026-08-14—, que con la forma nueva habría entrado acá como **P1**. Estado previo archivado en [`_legacy/2026-08-25/US-02016-Enviar-Un-Trabajo-Que-No-Verifica-Y-Queda-En-Borrador-v1.0.md`](_legacy/2026-08-25/US-02016-Enviar-Un-Trabajo-Que-No-Verifica-Y-Queda-En-Borrador-v1.0.md). Sube **major**: el salto de la regla que lo gobierna es major. |
