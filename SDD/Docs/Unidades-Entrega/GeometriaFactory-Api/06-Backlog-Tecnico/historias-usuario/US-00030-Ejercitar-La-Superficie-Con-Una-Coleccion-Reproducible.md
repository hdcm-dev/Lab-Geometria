# US-00030 — Ejercitar la superficie con una colección reproducible en cinco pasos o menos

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-00030-Ejercitar-La-Superficie-Con-Una-Coleccion-Reproducible.md
**Versión:** 2.0
**Estado:** Aprobada
**Fecha:** 2026-08-25
**Autor:** Scrum Master + API Product Owner (AG-06)
**Épica:** EP-00006 Desenlace de la entrega
**Etapa del producto:** `h`
**Punto de acceso:** Ninguno propio: **ejercita** los quince
**Prioridad MoSCoW:** **Should**
**Estimación:** **No aplica** — el producto no estima; ver §5.b

## 1. Historia

Como **integrador de la superficie —el propio equipo o quien la revise—**, quiero **una colección de peticiones que se reproduzca en cinco pasos o menos y ejercite la superficie de punta a punta**, para **poder demostrar el servicio sin escribir peticiones a mano y sin inventar datos**.

## 2. Contexto

`PRODUCT-INTAKE` §16.1 declara, para el tipo de proyecto de código de esta pieza, una **colección de peticiones reproducible con los escenarios como cuerpo**: alta de trabajo, envío con texto que verifica y que no verifica, y **aprobación y rechazo por el administrador**, con los códigos de respuesta esperados. El contrato de uso es [`CU-00012`](../../10-Examples/CU-00012-Ejercitar-La-Superficie-Con-La-Coleccion-De-Peticiones-Reproducible.md).

## 3. Criterios de aceptación

- Given la colección, When se la reproduce desde cero, Then se completa en **5 pasos o menos**.
- Given sus cuerpos, When se los inspecciona, Then son los escenarios del intake §20 y hay **0** datos de prueba inventados.
- Given la colección, When se la recorre, Then incluye el alta, el envío que verifica, el que no verifica y **la aprobación y el rechazo por el administrador**, que es lo que la ubica en la etapa `h`.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | **Ninguna.** `02` §7.2 declara que `CU-00012` **no traza a ninguna necesidad**: **no implementa nada, demuestra**, y asignarle las necesidades de las capacidades que ejercita las contaría dos veces |
| CU cubiertos | CU-00012 |
| RN que ejerce | — |
| Componente de `05` §3.1 | **Ninguno**, y es correcto: `05` §3.3 declara que es el único de los doce casos de uso **sin componente**, porque es un artefacto del árbol de muestras y no código de producción |
| ¿Decide qué se dice? | **No** |
| Familia empobrecida | **No** |
| BT derivadas | BT-00020, BT-00021 |
| Tests previstos en 08 | Ejecución de la colección en la demostración de etapa |

## 5. Prioridad

**`Should`, y es la única de las treinta.** Su origen **no es una capacidad** de `PRODUCT-INTAKE` §4 sino la **estrategia de demostración** de §16.1 y §18, y **es la única historia de este backlog que no implementa nada**. El producto funciona sin ella; lo que se pierde es la forma de demostración que el tipo de proyecto de código tiene declarada. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.2.

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
- [x] Declara la necesidad de negocio y la etapa del roadmap, o declara que su caso de uso no traza a ninguna y por qué
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el punto de acceso que la realiza, o declara que no realiza ninguno, y el componente de `05` §3.1
- [x] Declara si su punto está bajo la guardia, y si no lo está, cuál de las cuatro ausencias declaradas es
- [x] Toda condición que transporta es uno de los diecisiete códigos vivos del contrato, con su destino declarado
- [x] Declara que no decide qué se dice
- [x] Declara si su respuesta pertenece a una de las tres familias deliberadamente empobrecidas

## 7. Notas y supuestos

**El alcance de la colección está abierto y es una divergencia entre dos textos vivos, no un recuento envejecido.** La fuente lo declara en dos lugares con alcances distintos —los **ocho** escenarios en uno y **dos** en el otro—, los dos textos están al día y **la fuente no declara cuál manda**. La categoría 02 adopta **los ocho** con el fundamento de que `E-8` es el modo de falla que el propio intake llama **el más probable**, y este backlog **hereda esa lectura y no la reabre**: es `PA-07` de [`../Product-Backlog.md`](../Product-Backlog.md) §6, elevado con BT-00021.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
| 2.0 | 2026-08-25 | **Migración normativa 10.0 → 13.3, fase M4, corte de la categoría 06** (`Audit/Plan-Migracion-10.0-a-13.3.md` **1.2** §4.1). **§5 se parte en 5 · prioridad y 5.b · estimación**, que es lo que `Rules-Backlog-Tecnico.md` **5.0** §4.4 exige desde el salto: lo que separa las dos mitades **no es un evento sino un dueño**. **La estimación se declara «no aplica» y no se difiere**, cerrada **por lectura y no por decisión**: `PRODUCT-INTAKE` §2 declara `equipo_n = 1`, `Mini-Plan.md` §1.2 declara que **no hay capacidad numérica y es deliberado**, y el hecho que lo cierra es que **ocho etapas se cerraron sin una sola estimación**. La forma es la de [`../../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md`](../../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md), `Propuesto`. **Lo anterior remitía a un ítem vencido** —`PA-01`, diferido al punto de control de la etapa `c`, que cerró el 2026-08-14—, que con la forma nueva habría entrado acá como **P1**. Estado previo archivado en [`_legacy/2026-08-25/US-00030-Ejercitar-La-Superficie-Con-Una-Coleccion-Reproducible-v1.1.md`](_legacy/2026-08-25/US-00030-Ejercitar-La-Superficie-Con-Una-Coleccion-Reproducible-v1.1.md). Sube **major**: el salto de la regla que lo gobierna es major. |
