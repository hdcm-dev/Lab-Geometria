# US-10027 — Sostener la reconexión y el estado degradado como dos tramos independientes

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** US-10027-Sostener-La-Reconexion-Y-El-Estado-Degradado-Como-Dos-Tramos.md
**Versión:** 2.1
**Estado:** Aprobada
**Fecha:** 2026-08-25
**Autor:** Scrum Master (AG-06)
**Épica:** EP-10003 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Superficie de 03:** `Estado-Degradado-Y-Reconexion`
**Prioridad MoSCoW:** Must
**Estimación:** **No aplica** — el producto no estima; ver §5.b

## 1. Historia

Como **persona que usa el laboratorio**, quiero **que la aplicación distinga si se cortó mi conexión con ella o si el servicio de datos no responde**, para **saber si tengo que esperar, recargar o avisarle al docente**, y no encontrarme con una pantalla rota.

## 2. Contexto

`NB-00008` pide alcance del laboratorio desde el aula. El caso de uso es [`CU-10010`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-10010-Sostener-La-Aplicacion-En-Estado-Degradado-Y-Reconexion.md), **transversal a los otros nueve**: concentra en un solo lugar la superficie donde `RA-03` se puede violar. `05` §4 declara los dos tramos como **independientes** y advierte que confundirlos es el error de lectura más probable de la pieza.

## 3. Criterios de aceptación

- Given una sesión abierta, When el circuito se corta y se restablece, Then la superficie muestra el cartel de reconexión y la persona vuelve a operar.
- Given una sesión abierta, When el servicio de datos deja de responder, Then la superficie declara el **estado degradado**, que es un tramo distinto del anterior y **no se mezcla con él**.
- Given cualquiera de los dos, When se inspecciona el mensaje, Then **no incluye la dirección de un servicio interno, un nombre de archivo de datos ni una traza**, y **nunca es una excepción sin manejar**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00008 |
| CU cubiertos | CU-10010 |
| Restricciones transversales que la alcanzan | RT-03, RT-06, RT-07 |
| Componente de `05` §3.1 | Traductor de condiciones a presentación, Superficies |
| Quién hace cumplir lo que esta historia sólo ofrece | La disponibilidad del servicio no la sostiene esta pieza: el servidor es domiciliario y su caída es un riesgo aceptado |
| BT derivadas | BT-10013 |
| Tests previstos en 08 | Recorrido con el servicio de datos detenido, y recorrido con la red cortada y restablecida —parte de `PT-01.c`— |

## 5. Prioridad

`Must` porque el estado degradado explícito es lo único de `NB-00008` que la persona ve, y porque `PT-01.c` exige **reconexión funcional al cortar y restablecer la red**.

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
- [x] Declara la superficie de 03 que la aloja y el componente de `05` §3.1 que la sostiene
- [x] Declara qué restricciones transversales de `02` §6 la alcanzan
- [x] Toda condición que presenta es uno de los diecisiete códigos vivos del contrato, o el camino de ausencia de respuesta
- [x] Ninguna afirmación depende de que la pieza pública haga cumplir una regla
- [x] Se puede maquetar y validar sin servicio de datos

## 7. Notas y supuestos

**El reciclado del proceso del hosting es el peor escenario del producto y la fuente declara que no tiene mitigación en el código** (`R-06`). Lo que sí hay es tratamiento: el estado «sesión no restablecible» está diseñado como estado propio de esta superficie, y **el envío es la única acción de guardado**, de modo que un corte no deja un trabajo a medias.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Numera y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 había repartido por necesidad de negocio con este identificador, y que su §3.2 dejó a la categoría 06 para redactar. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
| 2.0 | 2026-08-25 | **Migración normativa 10.0 → 13.3, fase M4, corte de la categoría 06** (`Audit/Plan-Migracion-10.0-a-13.3.md` **1.2** §4.1). **§5 se parte en 5 · prioridad y 5.b · estimación**, que es lo que `Rules-Backlog-Tecnico.md` **5.0** §4.4 exige desde el salto: lo que separa las dos mitades **no es un evento sino un dueño**. **La estimación se declara «no aplica» y no se difiere**, cerrada **por lectura y no por decisión**: `PRODUCT-INTAKE` §2 declara `equipo_n = 1`, `Mini-Plan.md` §1.2 declara que **no hay capacidad numérica y es deliberado**, y el hecho que lo cierra es que **ocho etapas se cerraron sin una sola estimación**. La forma es la de [`../../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md`](../../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md), `Propuesto`. **Lo anterior remitía a un ítem vencido** —`PA-01`, diferido al punto de control de la etapa `c`, que cerró el 2026-08-14—, que con la forma nueva habría entrado acá como **P1**. Estado previo archivado en [`_legacy/2026-08-25/US-10027-Sostener-La-Reconexion-Y-El-Estado-Degradado-Como-Dos-Tramos-v1.1.md`](_legacy/2026-08-25/US-10027-Sostener-La-Reconexion-Y-El-Estado-Degradado-Como-Dos-Tramos-v1.1.md). Sube **major**: el salto de la regla que lo gobierna es major. |
| 2.1 | 2026-08-25 | **Ronda 2 del corte de la 06**, sobre el audit independiente que lo aprobó **con hallazgos**. **Se corrige una afirmación histórica que era falsa en 116 de las 144** (**P2**): §5.b decía que el texto anterior remitía «al `PA-01` de §6», y **sólo 28 lo nombraban**; las otras 116 remitían a §4.1 a secas. Se declara además la **unificación de las tres redacciones previas en una**, con la constancia de que la sustancia de la más larga quedó preservada y de que cada historia conserva la suya en su `_legacy/`. **Y una constancia sobre los recuentos del mensaje de entrega de la ronda 1**, que el audit refutó (**P3**) y que no se pueden editar allí: los estados previos archivados son **148** y no 152 —144 historias, 2 `Product-Backlog` y 2 `Mini-Plan`—, y los enlaces reescritos **1099** y no 694. |
