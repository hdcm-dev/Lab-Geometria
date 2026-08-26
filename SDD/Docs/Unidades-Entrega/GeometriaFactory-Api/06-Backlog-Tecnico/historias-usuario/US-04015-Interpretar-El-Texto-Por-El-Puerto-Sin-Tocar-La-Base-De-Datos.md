# US-04015 — Interpretar el texto por el puerto de validación, sin tocar la base de datos

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-04015-Interpretar-El-Texto-Por-El-Puerto-Sin-Tocar-La-Base-De-Datos.md
**Versión:** 2.1
**Estado:** Aprobada
**Fecha:** 2026-08-25
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-04005 Interpretación y verificación del dato del alumno
**Etapa del producto:** `f`
**Prioridad MoSCoW:** Must
**Estimación:** **No aplica** — el producto no estima; ver §5.b

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que la interpretación del texto ocurra detrás del puerto de validación y que el caso de uso entero se pueda ejercer con un doble**, para **poder probar el envío sin base de datos y sin frontera de proceso**.

## 2. Contexto

`PRODUCT-INTAKE` §17.1.P.11 · GeometriaFactory-Application punto 1 declara que **el validador de figuras es un puerto y no una dependencia concreta**, y §17.1.P.8 · GeometriaFactory-Application fija la puerta propia: ninguna prueba de esta capa toca la base de datos real. El contrato de uso es [`CU-00026`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00026-Enviar-Un-Trabajo-Y-Ver-Sus-Observaciones.md).

## 3. Criterios de aceptación

- Given un doble del puerto de validación que devuelve piezas y observaciones, When se ejerce el caso de uso de envío entero, Then se resuelve **sin abrir la base de datos**.
- Given ese mismo ejercicio, When se cuenta cuántas dependencias concretas de interpretación aparecen en el archivo de proyecto, Then son exactamente **0**: la interpretación llega por el puerto.
- Given el texto de **3** piezas del escenario `E-1` del intake §20, When se mide el caso de uso más pesado sin acceso a base, Then el tiempo se compara contra el valor vigente de `05` §8, **rotulado como asunción** hasta que el Product Owner lo confirme.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00004, NB-00003 |
| CU cubiertos | CU-04005 |
| RN e invariantes que ejerce | RN-04005, RN-04009 |
| Componente de `05` §3.1 | Declaración de puertos, Orquestación del trabajo |
| Puertos que consume | Validación de figuras |
| Comprobación de `02` §4 que la alcanza | Pertenencia, y cambio de contraseña pendiente antes que ella |
| BT derivadas | BT-04006, BT-04007, BT-04015, BT-04019 |
| Tests previstos en 08 | Batería unitaria con doble del puerto y medición del tiempo sobre `E-1` |

## 5. Prioridad

`Must` porque es la propiedad que justifica el estilo entero de la capa: `05` §9 declara como riesgo de impacto **alto** que un caso de uso consulte por su cuenta y deje de ser probable con dobles.

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

**El valor de los 500 ms se usa como vigente y viene rotulado [ASUNCIÓN]** desde el intake (`05` §8 y §11 `PA-05`). BT-04018 lo eleva al Product Owner; hasta entonces la puerta **no se declara bloqueante en 09**, y esta historia **no inventa otro número**.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador. |
| 2.0 | 2026-08-25 | **Migración normativa 10.0 → 13.3, fase M4, corte de la categoría 06** (`Audit/Plan-Migracion-10.0-a-13.3.md` **1.2** §4.1). **§5 se parte en 5 · prioridad y 5.b · estimación**, que es lo que `Rules-Backlog-Tecnico.md` **5.0** §4.4 exige desde el salto: lo que separa las dos mitades **no es un evento sino un dueño**. **La estimación se declara «no aplica» y no se difiere**, cerrada **por lectura y no por decisión**: `PRODUCT-INTAKE` §2 declara `equipo_n = 1`, `Mini-Plan.md` §1.2 declara que **no hay capacidad numérica y es deliberado**, y el hecho que lo cierra es que **ocho etapas se cerraron sin una sola estimación**. La forma es la de [`../../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md`](../../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md), `Propuesto`. **Lo anterior remitía a un ítem vencido** —`PA-01`, diferido al punto de control de la etapa `c`, que cerró el 2026-08-14—, que con la forma nueva habría entrado acá como **P1**. Estado previo archivado en [`_legacy/2026-08-25/US-04015-Interpretar-El-Texto-Por-El-Puerto-Sin-Tocar-La-Base-De-Datos-v1.0.md`](_legacy/2026-08-25/US-04015-Interpretar-El-Texto-Por-El-Puerto-Sin-Tocar-La-Base-De-Datos-v1.0.md). Sube **major**: el salto de la regla que lo gobierna es major. |
| 2.1 | 2026-08-25 | **Ronda 2 del corte de la 06**, sobre el audit independiente que lo aprobó **con hallazgos**. **Se corrige una afirmación histórica que era falsa en 116 de las 144** (**P2**): §5.b decía que el texto anterior remitía «al `PA-01` de §6», y **sólo 28 lo nombraban**; las otras 116 remitían a §4.1 a secas. Se declara además la **unificación de las tres redacciones previas en una**, con la constancia de que la sustancia de la más larga quedó preservada y de que cada historia conserva la suya en su `_legacy/`. **Y una constancia sobre los recuentos del mensaje de entrega de la ronda 1**, que el audit refutó (**P3**) y que no se pueden editar allí: los estados previos archivados son **148** y no 152 —144 historias, 2 `Product-Backlog` y 2 `Mini-Plan`—, y los enlaces reescritos **1099** y no 694. |
