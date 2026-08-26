# US-04002 — Rechazar el alta con un correo ya registrado

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-04002-Rechazar-El-Alta-Con-Un-Correo-Ya-Registrado.md
**Versión:** 2.0
**Estado:** Aprobada
**Fecha:** 2026-08-25
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-04003 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Prioridad MoSCoW:** Must
**Estimación:** **No aplica** — el producto no estima; ver §5.b

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **recibir un motivo tipado cuando el correo del alta ya está registrado**, para **poder decirle a la persona que ese correo está ocupado sin revelar nada de la cuenta que lo ocupa**.

## 2. Contexto

`RN-04002` fija que el correo del alumno es único, y `02` §6 declara que **la verificación sobre el conjunto de cuentas es de esta capa, en los dos caminos de alta**. El contrato de uso es [`CU-00021`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00021-Dar-De-Alta-Una-Cuenta-De-Alumno.md). Sin esta historia, la colisión llegaría al consumidor como una excepción del almacén, que es exactamente lo que [`ADR-04006`](../../05-Arquitectura-Tecnica/Adrs/ADR-04006-Resultado-Tipado-Y-Catalogo-Cerrado-De-Condiciones.md) descarta.

## 3. Criterios de aceptación

- Given un correo que el puerto de repositorio de cuentas responde como ya registrado, When se solicita el alta, Then **no se constituye ninguna cuenta** y se devuelve el motivo de correo ocupado, tomado del catálogo cerrado.
- Given ese rechazo, When se inspecciona el motivo emitido, Then **no declara la situación ni el papel** de la cuenta que ocupa el correo.
- Given el mismo rechazo, When se cuenta el efecto sobre el estado, Then **no hay ninguno**: la operación termina de forma controlada y nada queda a medio escribir.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00001, NB-00002 |
| CU cubiertos | CU-04001 |
| RN e invariantes que ejerce | RN-04002; INV-01 |
| Componente de `05` §3.1 | Orquestación del alta de cuentas |
| Puertos que consume | Repositorio de cuentas |
| Comprobación de `02` §4 que la alcanza | Ninguna: el solicitante es anónimo, igual que en US-04001 |
| BT derivadas | BT-04008, BT-04012, BT-04021 |
| Tests previstos en 08 | Prueba unitaria con doble que responde correo ocupado, y prueba de inspección del motivo emitido contra el catálogo |

## 5. Prioridad

`Must` porque `RN-04002` es una de las dieciséis reglas del producto y porque sin esta negativa el alta duplicaría cuentas, con lo que el ingreso dejaría de ser determinista (`05` §9, último riesgo de `GeometriaFactory-Infrastructure` sobre la misma regla).

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
- [x] Las condiciones de rechazo que produce existen en el catálogo de las 36
- [x] Se puede verificar con dobles de los cuatro puertos, sin base de datos

## 7. Notas y supuestos

El **criterio de comparación de dos correos** —tal cual o normalizados— condiciona cuándo se produce esta negativa y **no se decide acá**: `05` §11 `PA-03` lo derivó a la categoría 05 de `GeometriaFactory-Infrastructure`. BT-04021 lo acompaña con su plazo.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia prevista en `02` §7.3 con este identificador. |
| 2.0 | 2026-08-25 | **Migración normativa 10.0 → 13.3, fase M4, corte de la categoría 06** (`Audit/Plan-Migracion-10.0-a-13.3.md` **1.2** §4.1). **§5 se parte en 5 · prioridad y 5.b · estimación**, que es lo que `Rules-Backlog-Tecnico.md` **5.0** §4.4 exige desde el salto: lo que separa las dos mitades **no es un evento sino un dueño**. **La estimación se declara «no aplica» y no se difiere**, cerrada **por lectura y no por decisión**: `PRODUCT-INTAKE` §2 declara `equipo_n = 1`, `Mini-Plan.md` §1.2 declara que **no hay capacidad numérica y es deliberado**, y el hecho que lo cierra es que **ocho etapas se cerraron sin una sola estimación**. La forma es la de [`../../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md`](../../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md), `Propuesto`. **Lo anterior remitía a un ítem vencido** —`PA-01`, diferido al punto de control de la etapa `c`, que cerró el 2026-08-14—, que con la forma nueva habría entrado acá como **P1**. Estado previo archivado en [`_legacy/2026-08-25/US-04002-Rechazar-El-Alta-Con-Un-Correo-Ya-Registrado-v1.0.md`](_legacy/2026-08-25/US-04002-Rechazar-El-Alta-Con-Un-Correo-Ya-Registrado-v1.0.md). Sube **major**: el salto de la regla que lo gobierna es major. |
