# US-06009 — Materializar el trabajo con sus piezas, componentes y observaciones en una unidad de trabajo

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-06009-Materializar-El-Trabajo-Con-Sus-Piezas-Componentes-Y-Observaciones.md
**Versión:** 2.0
**Estado:** Aprobada
**Fecha:** 2026-08-25
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-06004 Gestión del trabajo
**Etapa del producto:** `e`
**Prioridad MoSCoW:** Must
**Estimación:** **No aplica** — el producto no estima; ver §5.b

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que un trabajo se guarde con sus piezas, sus componentes y sus observaciones dentro de una sola unidad de trabajo**, para **que no queden trabajos guardados a medias**.

## 2. Contexto

`NB-00003` pide persistencia del trabajo. El alcance transaccional **llega decidido** desde `GeometriaFactory-Application` —un caso de uso, una unidad de trabajo— y acá se materializa como **una por operación** (`05` §2 propiedad 3). El contrato de uso es [`CU-06003`](../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06003-Guardar-Y-Recuperar-Los-Trabajos.md).

## 3. Criterios de aceptación

- Given un trabajo con sus piezas, componentes y observaciones, When se lo materializa, Then todo se escribe **dentro de la misma unidad de trabajo**, que se cierra entera o no se cierra.
- Given una escritura que llega mientras otra tiene el almacén tomado, When se la intenta, Then termina en su condición de **escritura concurrente rechazada**, que es **terminación degradada y no espera activa**: esta capa **no reintenta**.
- Given los **tres** sellos de tiempo del trabajo, When se los guarda, Then se distinguen entre sí y **la fecha que el alumno declara no se confunde** con las dos que registra el sistema.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00003 |
| CU cubiertos | CU-06003 |
| RN que ejerce | RN-06008 |
| Componente de `05` §3.1 | Adaptador de repositorio de trabajos, Contexto de persistencia y mapeo |
| Reglas conceptuales de modelo | `RC-06001`, `RC-06003`, `RC-06006` —tres sellos de tiempo distintos— |
| ¿Toma alguna decisión de negocio? | **No** |
| ¿Toca el almacén? | **Sí** |
| BT derivadas | BT-06005, BT-06008, BT-06010 |
| Tests previstos en 08 | Pruebas de integración contra el almacén real, desde `GeometriaFactory-Api` |

## 5. Prioridad

`Must` por derivar de `F-06`, `Must Have`, y porque sin materialización no hay nada que listar ni que revisar.

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

**El escritor único es una restricción del motor y no una elección**, y el intake la acepta por escrito a cambio de un despliegue sin servicio de base de datos aparte. **Reintentar es del consumidor**, que es el que sabe si la operación es repetible; un reintento acá escondería la única señal que el producto tiene de que el almacén no está.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
| 2.0 | 2026-08-25 | **Migración normativa 10.0 → 13.3, fase M4, corte de la categoría 06** (`Audit/Plan-Migracion-10.0-a-13.3.md` **1.2** §4.1). **§5 se parte en 5 · prioridad y 5.b · estimación**, que es lo que `Rules-Backlog-Tecnico.md` **5.0** §4.4 exige desde el salto: lo que separa las dos mitades **no es un evento sino un dueño**. **La estimación se declara «no aplica» y no se difiere**, cerrada **por lectura y no por decisión**: `PRODUCT-INTAKE` §2 declara `equipo_n = 1`, `Mini-Plan.md` §1.2 declara que **no hay capacidad numérica y es deliberado**, y el hecho que lo cierra es que **ocho etapas se cerraron sin una sola estimación**. La forma es la de [`../../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md`](../../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md), `Propuesto`. **Lo anterior remitía a un ítem vencido** —`PA-01`, diferido al punto de control de la etapa `c`, que cerró el 2026-08-14—, que con la forma nueva habría entrado acá como **P1**. Estado previo archivado en [`_legacy/2026-08-25/US-06009-Materializar-El-Trabajo-Con-Sus-Piezas-Componentes-Y-Observaciones-v1.0.md`](_legacy/2026-08-25/US-06009-Materializar-El-Trabajo-Con-Sus-Piezas-Componentes-Y-Observaciones-v1.0.md). Sube **major**: el salto de la regla que lo gobierna es major. |
