# US-06024 — Aplicar las transformaciones de esquema al arrancar, sobre base inexistente

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-06024-Aplicar-Las-Transformaciones-De-Esquema-Al-Arrancar.md
**Versión:** 2.0
**Estado:** Aprobada
**Fecha:** 2026-08-25
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-06001 Esqueleto ambulante y verificación de viabilidad
**Etapa del producto:** `a`
**Prioridad MoSCoW:** Must
**Estimación:** **No aplica** — el producto no estima; ver §5.b

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que el almacén se cree y se transforme solo al arrancar el servicio**, para **que el laboratorio se pueda levantar desde cero sin ningún paso manual de despliegue**.

## 2. Contexto

`PRODUCT-INTAKE` §17.1.P.11 · GeometriaFactory-Infrastructure punto 3 declara las transformaciones **aplicadas al arrancar y no por un paso manual**, y §17.1.P.8 · GeometriaFactory-Infrastructure las declara **criterio de aceptación de la etapa `c`**. Y `PT-04`, que se mide en la etapa `a`, exige que la imagen del servicio de datos **arranque, aplique sus actualizaciones de esquema sobre base vacía y responda salud**. El contrato de uso es [`CU-06010`](../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06010-Preparar-El-Almacen-Al-Arrancar.md).

## 3. Criterios de aceptación

- Given un almacén inexistente, When arranca el servicio, Then las transformaciones se aplican **solas** y el almacén queda en condiciones: **1 de 1** intento exitoso, sin paso manual.
- Given un almacén desactualizado con linaje compatible, When arranca, Then se le aplican las transformaciones que le faltan.
- Given una transformación ya fusionada, When se la mira, Then **no se edita**: cada una se versiona con el código de su etapa.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00003, NB-00008 (parcial) |
| CU cubiertos | CU-06010 |
| RN que ejerce | — |
| Componente de `05` §3.1 | Mecanismo de acceso firmado y preparación del almacén, Contexto de persistencia y mapeo |
| Reglas conceptuales de modelo | Materializa el esquema que las siete gobiernan |
| ¿Toma alguna decisión de negocio? | **No** |
| ¿Toca el almacén? | **Sí**, y es la operación que lo crea |
| BT derivadas | BT-06005, BT-06006, BT-06007 |
| Tests previstos en 08 | Etapa de verificación de transformaciones del pipeline, sobre un almacén recién creado |

## 5. Prioridad

`Must` porque es parte de lo que **`PT-04` mide en la etapa `a`**, y una puerta que no pasa **detiene la planificación de las etapas que dependen de ella**; y porque el criterio de transición `c` → `d` exige que las actualizaciones de esquema **se apliquen solas sobre una base inexistente**.

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

**El guion de restablecimiento que el intake declara no es un camino de producción**: reproduce el estado de primer arranque, o sea **un almacén vacío**. `05` §5 lo dice explícitamente para que no se lo use como reversión.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
| 2.0 | 2026-08-25 | **Migración normativa 10.0 → 13.3, fase M4, corte de la categoría 06** (`Audit/Plan-Migracion-10.0-a-13.3.md` **1.2** §4.1). **§5 se parte en 5 · prioridad y 5.b · estimación**, que es lo que `Rules-Backlog-Tecnico.md` **5.0** §4.4 exige desde el salto: lo que separa las dos mitades **no es un evento sino un dueño**. **La estimación se declara «no aplica» y no se difiere**, cerrada **por lectura y no por decisión**: `PRODUCT-INTAKE` §2 declara `equipo_n = 1`, `Mini-Plan.md` §1.2 declara que **no hay capacidad numérica y es deliberado**, y el hecho que lo cierra es que **ocho etapas se cerraron sin una sola estimación**. La forma es la de [`../../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md`](../../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md), `Propuesto`. **Lo anterior remitía a un ítem vencido** —`PA-01`, diferido al punto de control de la etapa `c`, que cerró el 2026-08-14—, que con la forma nueva habría entrado acá como **P1**. Estado previo archivado en [`_legacy/2026-08-25/US-06024-Aplicar-Las-Transformaciones-De-Esquema-Al-Arrancar-v1.0.md`](_legacy/2026-08-25/US-06024-Aplicar-Las-Transformaciones-De-Esquema-Al-Arrancar-v1.0.md). Sube **major**: el salto de la regla que lo gobierna es major. |
