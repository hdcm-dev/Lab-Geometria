# US-04016 — Terminar de forma controlada cuando la interpretación no está disponible

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-04016-Terminar-De-Forma-Controlada-Cuando-La-Interpretacion-No-Esta-Disponible.md
**Versión:** 2.1
**Estado:** Aprobada
**Fecha:** 2026-08-25
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-04005 Interpretación y verificación del dato del alumno
**Etapa del producto:** `f`
**Prioridad MoSCoW:** **Should**
**Estimación:** **No aplica** — el producto no estima; ver §5.b

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que el caso de uso de envío termine con una condición legible cuando la interpretación no está disponible, dejando el texto original intacto**, para **poder decirle a la persona que el servicio no pudo interpretar en lugar de mostrarle un fallo sin manejar**.

## 2. Contexto

`05` §4, última viñeta, declara que **la indisponibilidad de un puerto es una condición y no una excepción que escapa**, y que si la interpretación no está disponible el caso de uso de envío termina de forma controlada y el texto original queda intacto. **Ninguna capacidad del intake §4 la pide**: su origen es esta decisión de arquitectura, y eso es lo que la hace `Should`.

## 3. Criterios de aceptación

- Given un doble del puerto de validación que no puede resolver, When se pide el envío, Then el caso de uso devuelve la condición correspondiente del catálogo cerrado y **no propaga ninguna excepción**.
- Given esa terminación, When se inspecciona el trabajo, Then el **texto original queda intacto** y el estado no cambió.
- Given la misma terminación, When se compara la condición emitida contra el catálogo de las 36, Then figura en él: esta historia **no acuña ninguna condición nueva**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00004, NB-00003 |
| CU cubiertos | CU-04005 |
| RN e invariantes que ejerce | RN-04008 |
| Componente de `05` §3.1 | Orquestación del trabajo |
| Puertos que consume | Validación de figuras, repositorio de trabajos |
| Comprobación de `02` §4 que la alcanza | Pertenencia, y cambio de contraseña pendiente antes que ella |
| BT derivadas | BT-04008, BT-04015 |
| Tests previstos en 08 | Prueba con doble del puerto que no resuelve, comprobando texto intacto y condición emitida |

## 5. Prioridad

**`Should`, y es la única de las treinta y dos.** Su origen **no es una capacidad** de `PRODUCT-INTAKE` §4 sino la decisión de `05` §4 sobre la indisponibilidad de un puerto. El producto funciona sin ella —el caso de uso terminaría con una excepción que el consumidor tendría que atrapar— y lo que se pierde es que el motivo sea legible y que el texto quede garantizadamente intacto. **Diferible, y no gratis.** El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.2.

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

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el componente de `05` §3.1 y los puertos que consume
- [x] Declara qué comprobación de `02` §4 la alcanza
- [x] Las condiciones de rechazo que produce existen en el catálogo de las 36 de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Se puede verificar con dobles de los cuatro puertos, sin base de datos

## 7. Notas y supuestos

**La prioridad declarada no es la prioridad de ejecución.** Esta historia vive en la etapa `f` y se construye con las otras tres de su épica; ser `Should` significa que, si la etapa aprieta, es la primera candidata a diferirse, no que se construya después de la etapa.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador. |
| 2.0 | 2026-08-25 | **Migración normativa 10.0 → 13.3, fase M4, corte de la categoría 06** (`Audit/Plan-Migracion-10.0-a-13.3.md` **1.2** §4.1). **§5 se parte en 5 · prioridad y 5.b · estimación**, que es lo que `Rules-Backlog-Tecnico.md` **5.0** §4.4 exige desde el salto: lo que separa las dos mitades **no es un evento sino un dueño**. **La estimación se declara «no aplica» y no se difiere**, cerrada **por lectura y no por decisión**: `PRODUCT-INTAKE` §2 declara `equipo_n = 1`, `Mini-Plan.md` §1.2 declara que **no hay capacidad numérica y es deliberado**, y el hecho que lo cierra es que **ocho etapas se cerraron sin una sola estimación**. La forma es la de [`../../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md`](../../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md), `Propuesto`. **Lo anterior remitía a un ítem vencido** —`PA-01`, diferido al punto de control de la etapa `c`, que cerró el 2026-08-14—, que con la forma nueva habría entrado acá como **P1**. Estado previo archivado en [`_legacy/2026-08-25/US-04016-Terminar-De-Forma-Controlada-Cuando-La-Interpretacion-No-Esta-Disponible-v1.0.md`](_legacy/2026-08-25/US-04016-Terminar-De-Forma-Controlada-Cuando-La-Interpretacion-No-Esta-Disponible-v1.0.md). Sube **major**: el salto de la regla que lo gobierna es major. |
| 2.1 | 2026-08-25 | **Ronda 2 del corte de la 06**, sobre el audit independiente que lo aprobó **con hallazgos**. **Se corrige una afirmación histórica que era falsa en 116 de las 144** (**P2**): §5.b decía que el texto anterior remitía «al `PA-01` de §6», y **sólo 28 lo nombraban**; las otras 116 remitían a §4.1 a secas. Se declara además la **unificación de las tres redacciones previas en una**, con la constancia de que la sustancia de la más larga quedó preservada y de que cada historia conserva la suya en su `_legacy/`. **Y una constancia sobre los recuentos del mensaje de entrega de la ronda 1**, que el audit refutó (**P3**) y que no se pueden editar allí: los estados previos archivados son **148** y no 152 —144 historias, 2 `Product-Backlog` y 2 `Mini-Plan`—, y los enlaces reescritos **1099** y no 694. |
