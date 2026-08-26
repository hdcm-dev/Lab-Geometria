# US-06017 — Derivar una contraseña sin guardarla ni registrarla en claro

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-06017-Derivar-Una-Contrasena-Sin-Guardarla-Ni-Registrarla-En-Claro.md
**Versión:** 2.0
**Estado:** Aprobada
**Fecha:** 2026-08-25
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-06002 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Prioridad MoSCoW:** Must
**Estimación:** **No aplica** — el producto no estima; ver §5.b

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que la contraseña en claro se convierta en un valor derivado y no quede guardada ni registrada en ningún lado**, para **que el producto no conserve las contraseñas de nadie**.

## 2. Contexto

`02` §1 declara que acá viven **las dos piezas sensibles** del producto, y §4 que **éste es el único punto donde la contraseña en claro se convierte en el valor guardado**. `PRODUCT-INTAKE` §17.1.P.5 · GeometriaFactory-Infrastructure lo fija: derivación **nunca en claro ni con resumen simple**. El contrato de uso es [`CU-06006`](../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06006-Derivar-La-Contrasena-Y-Verificar-Una-Credencial.md).

## 3. Criterios de aceptación

- Given una contraseña en claro, When se la deriva, Then el resultado es el valor derivado y **la contraseña en claro no se guarda**.
- Given esa derivación, When se inspeccionan los mensajes y las trazas, Then **0** contienen la contraseña en claro y **0** el valor derivado.
- Given el valor derivado, When se lo guarda, Then **los parámetros de derivación se versionan junto a él** y **no hay ningún valor por defecto silencioso**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00002 |
| CU cubiertos | CU-06006 |
| RN que ejerce | — directamente; sostiene la credencial que `RN-06013` y `RN-06016` gobiernan |
| Componente de `05` §3.1 | Mecanismo de credenciales |
| Reglas conceptuales de modelo | — |
| ¿Toma alguna decisión de negocio? | **No.** Decidir si una cuenta admite el acceso llega resuelto |
| ¿Toca el almacén? | **No.** Su prueba es unitaria |
| BT derivadas | BT-06003, BT-06013, BT-06022 |
| Tests previstos en 08 | Prueba de inspección de que ningún mensaje ni traza lleva la contraseña ni el valor derivado |

## 5. Prioridad

`Must` porque es una de las dos piezas que **el producto no puede permitirse mal hechas**, y porque la elección de la función de derivación es una decisión que el intake asigna a este proyecto de código y **no elige por él**.

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
- [x] Declara el componente de `05` §3.1 y, si toca el almacén, las reglas conceptuales de modelo que materializa
- [x] Declara que no toma ninguna decisión de negocio
- [x] Toda condición que produce existe en el catálogo de las 17 de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md), declarada como resultado o como fallo
- [x] Declara el camino en que el mecanismo se detiene en lugar de cumplir a medias, cuando puede fallar
- [x] Declara si toca el almacén y, en consecuencia, dónde vive su prueba

## 7. Notas y supuestos

**Cuál de las dos funciones candidatas se ancla es `PA-03`** de [`../Product-Backlog.md`](../Product-Backlog.md) §6, cerrado como trabajo en BT-06003, con caja temporal en la etapa `a`. La ADR correspondiente fija la **forma** —parámetros versionados, sin valor por defecto silencioso— y el **criterio de elección**; la elección concreta es del equipo.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
| 2.0 | 2026-08-25 | **Migración normativa 10.0 → 13.3, fase M4, corte de la categoría 06** (`Audit/Plan-Migracion-10.0-a-13.3.md` **1.2** §4.1). **§5 se parte en 5 · prioridad y 5.b · estimación**, que es lo que `Rules-Backlog-Tecnico.md` **5.0** §4.4 exige desde el salto: lo que separa las dos mitades **no es un evento sino un dueño**. **La estimación se declara «no aplica» y no se difiere**, cerrada **por lectura y no por decisión**: `PRODUCT-INTAKE` §2 declara `equipo_n = 1`, `Mini-Plan.md` §1.2 declara que **no hay capacidad numérica y es deliberado**, y el hecho que lo cierra es que **ocho etapas se cerraron sin una sola estimación**. La forma es la de [`../../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md`](../../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md), `Propuesto`. **Lo anterior remitía a un ítem vencido** —`PA-01`, diferido al punto de control de la etapa `c`, que cerró el 2026-08-14—, que con la forma nueva habría entrado acá como **P1**. Estado previo archivado en [`_legacy/2026-08-25/US-06017-Derivar-Una-Contrasena-Sin-Guardarla-Ni-Registrarla-En-Claro-v1.0.md`](_legacy/2026-08-25/US-06017-Derivar-Una-Contrasena-Sin-Guardarla-Ni-Registrarla-En-Claro-v1.0.md). Sube **major**: el salto de la regla que lo gobierna es major. |
