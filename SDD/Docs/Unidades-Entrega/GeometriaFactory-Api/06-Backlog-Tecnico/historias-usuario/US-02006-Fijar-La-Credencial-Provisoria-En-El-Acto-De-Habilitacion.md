# US-02006 — Fijar la credencial derivada provisoria en el acto de habilitación

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-02006-Fijar-La-Credencial-Provisoria-En-El-Acto-De-Habilitacion.md
**Versión:** 2.1
**Estado:** Aprobada
**Fecha:** 2026-08-25
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-02003 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Prioridad MoSCoW:** Must
**Estimación:** **No aplica** — el producto no estima; ver §5.b

## 1. Historia

Como **código consumidor de la biblioteca de dominio (`GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`)**, quiero **que la habilitación fije la credencial derivada provisoria y deje puesta la marca de cambio pendiente**, para **que no exista ninguna escritura anónima de contraseña y que el primer ingreso del alumno use el mismo mecanismo que el reseteo**.

## 2. Contexto

`RN-02016`, que el intake 1.13 incorporó, declara que habilitar produce una contraseña provisoria con el mismo tratamiento que la del reseteo y deja la cuenta con cambio de contraseña pendiente. Es lo que hizo que la fijación de la credencial dejara de ser un acto del alumno anónimo ([`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §9).

## 3. Criterios de aceptación

- Given una cuenta de alumno en estado `Pendiente` y una credencial ya derivada aportada por el consumidor, When el administrador la habilita, Then la cuenta queda `Habilitado`, con esa credencial y con la marca de cambio de contraseña pendiente puesta.
- Given una habilitación a la que **no** se le aporta credencial derivada, When se procesa, Then se rechaza con la condición que 03 declara para ese caso: no existe cuenta `Habilitado` sin credencial.
- Given una cuenta habilitada por este camino, When se evalúa su admisibilidad, Then no es admisible por cambio de contraseña pendiente, que es `INV-09` y se ejerce en US-02027.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00002, NB-00001 |
| CU cubiertos | CU-02003, CU-02002 |
| RN e invariantes que ejerce | RN-02016, RN-02014, RN-02006; INV-09 |
| BT derivadas | BT-02010, BT-02011 |
| Etapa del producto | `d`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba unitaria de la habilitación con y sin credencial aportada, y de la marca puesta como efecto. |

## 5. Prioridad

`Must` por derivar de `F-04`, `Must Have` en `PRODUCT-INTAKE` §4, y porque `RN-02016` es lo que cierra la escritura anónima de contraseña.

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

**Lo que decía antes, y por qué era falso.** Decía «**Estimación: sin fijar**», remitiendo a
[`../Product-Backlog.md`](../Product-Backlog.md) **§4.1** —y, en **28** de las 144 historias, nombrando
además el `PA-01` de su §6—. Ese punto abierto estaba **diferido al punto de control de la etapa `c`,
que cerró el 2026-08-14 sin registrarlo**: estaba **vencido**, y con la forma nueva habría entrado a
este documento como hallazgo **P1** por la tabla de escalamiento de `Root-Rules.md` §12.2.

**Las tres redacciones anteriores se unificaron en una, y se declara.** El §5 previo tenía **tres**
formas —116 remitían a §4.1 a secas, 27 desarrollaban el fundamento y nombraban `PA-01`, y 1 combinaba
las dos—. La sustancia de la más larga *«ninguna fuente da base para puntos de historia ni para tallas,
y el intake declara sin plazo calendario: el avance se mide por etapas cerradas»* **está preservada**
en esta subsección, atribuida a su fuente. El estado previo de cada historia, con su redacción propia,
queda en su `_legacy/2026-08-25/`.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Cita por identificador toda regla e invariante que ejerce, sin volver a enunciarla
- [x] Las condiciones de rechazo que produce existen en el catálogo de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Sus tareas técnicas están identificadas y ninguna está bloqueada

## 7. Notas y supuestos

**La contraseña llega ya derivada** y el dominio no ve valores en claro, no deriva y no compara credenciales por su cuenta (`05` §7, `PRODUCT-INTAKE` §17.1.P.5 · GeometriaFactory-Domain). Que la provisoria no sea adivinable y no se repita es `RN-02014`, y **su mecanismo vive en `GeometriaFactory-Infrastructure`**, no acá.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.3 previó con este mismo identificador y este mismo contenido. |
| 2.0 | 2026-08-25 | **Migración normativa 10.0 → 13.3, fase M4, corte de la categoría 06** (`Audit/Plan-Migracion-10.0-a-13.3.md` **1.2** §4.1). **§5 se parte en 5 · prioridad y 5.b · estimación**, que es lo que `Rules-Backlog-Tecnico.md` **5.0** §4.4 exige desde el salto: lo que separa las dos mitades **no es un evento sino un dueño**. **La estimación se declara «no aplica» y no se difiere**, cerrada **por lectura y no por decisión**: `PRODUCT-INTAKE` §2 declara `equipo_n = 1`, `Mini-Plan.md` §1.2 declara que **no hay capacidad numérica y es deliberado**, y el hecho que lo cierra es que **ocho etapas se cerraron sin una sola estimación**. La forma es la de [`../../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md`](../../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md), `Propuesto`. **Lo anterior remitía a un ítem vencido** —`PA-01`, diferido al punto de control de la etapa `c`, que cerró el 2026-08-14—, que con la forma nueva habría entrado acá como **P1**. Estado previo archivado en [`_legacy/2026-08-25/US-02006-Fijar-La-Credencial-Provisoria-En-El-Acto-De-Habilitacion-v1.0.md`](_legacy/2026-08-25/US-02006-Fijar-La-Credencial-Provisoria-En-El-Acto-De-Habilitacion-v1.0.md). Sube **major**: el salto de la regla que lo gobierna es major. |
| 2.1 | 2026-08-25 | **Ronda 2 del corte de la 06**, sobre el audit independiente que lo aprobó **con hallazgos**. **Se corrige una afirmación histórica que era falsa en 116 de las 144** (**P2**): §5.b decía que el texto anterior remitía «al `PA-01` de §6», y **sólo 28 lo nombraban**; las otras 116 remitían a §4.1 a secas. Se declara además la **unificación de las tres redacciones previas en una**, con la constancia de que la sustancia de la más larga quedó preservada y de que cada historia conserva la suya en su `_legacy/`. **Y una constancia sobre los recuentos del mensaje de entrega de la ronda 1**, que el audit refutó (**P3**) y que no se pueden editar allí: los estados previos archivados son **148** y no 152 —144 historias, 2 `Product-Backlog` y 2 `Mini-Plan`—, y los enlaces reescritos **1099** y no 694. |
