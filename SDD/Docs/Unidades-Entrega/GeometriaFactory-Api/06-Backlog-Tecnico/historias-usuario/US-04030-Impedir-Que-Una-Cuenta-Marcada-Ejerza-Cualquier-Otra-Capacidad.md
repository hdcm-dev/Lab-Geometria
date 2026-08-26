# US-04030 — Impedir que una cuenta con cambio de contraseña pendiente ejerza cualquier otra capacidad

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-04030-Impedir-Que-Una-Cuenta-Marcada-Ejerza-Cualquier-Otra-Capacidad.md
**Versión:** 2.0
**Estado:** Aprobada
**Fecha:** 2026-08-25
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-04003 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Prioridad MoSCoW:** Must
**Estimación:** **No aplica** — el producto no estima; ver §5.b

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que una cuenta con la marca de cambio de contraseña pendiente no pueda ejercer ninguna capacidad salvo cambiar su propia contraseña**, para **que una clave que el administrador conoce no quede sirviendo indefinidamente para operar como el alumno**.

## 2. Contexto

`RN-04013` declara que mientras la provisoria no se cambie la cuenta **se autentica y no obtiene sesión de trabajo**, e `INV-09` lo sostiene. El contrato de uso es [`CU-00024`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00024-Resetear-La-Contrasena-De-Un-Alumno.md) junto con la **cuarta comprobación transversal** de `02` §4. `Domain ADR-04005` §6 punto 1 declaró que el dominio **no puede impedir** que exista un camino que saltee la admisibilidad: esa dependencia de disciplina cae acá.

## 3. Criterios de aceptación

- Given una cuenta con la marca puesta, When pide **cualquiera** de los once casos de uso salvo el reemplazo de su propia credencial, Then se devuelve el motivo de cambio de contraseña pendiente y **no se lee ni se escribe nada**.
- Given esa misma cuenta y un pedido que además fallaría por pertenencia o por facultad, When se lo procesa, Then el motivo emitido es el de **cambio de contraseña pendiente**: la cuarta comprobación **corta antes** que las otras tres.
- Given una cuenta sin la marca, When pide cualquiera de los once, Then la cuarta comprobación **no cambia nada** y el pedido sigue su curso normal.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00002 |
| CU cubiertos | CU-04011, y la comprobación transversal de `02` §4 |
| RN e invariantes que ejerce | RN-04013, RN-04016; INV-09 |
| Componente de `05` §3.1 | Guarda de autorización |
| Puertos que consume | Repositorio de cuentas |
| Comprobación de `02` §4 que la alcanza | **La cuarta, y es su historia**: es la que la declara y la que la verifica |
| BT derivadas | BT-04008, BT-04010, BT-04011 |
| Tests previstos en 08 | **1** prueba que verifica que la cuarta corta antes que las otras tres, sin base de datos (`05` §8) |

## 5. Prioridad

`Must` porque `05` §9 declara como riesgo de impacto **muy alto** que aparezca un camino que ejerza una capacidad sin resolver antes la marca, y porque `05` §10.3 llama a esta comprobación **el aporte más consecuente de esta capa** a `INV-09`.

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
- [x] Las condiciones de rechazo que produce existen en el catálogo de las 36 de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Se puede verificar con dobles de los cuatro puertos, sin base de datos

## 7. Notas y supuestos

**Es una comprobación de esta capa y no una decisión de ruteo del front** (`02` §4, precisión 5): ocultar rutas acota lo que se ofrece y no hace cumplir nada. `GeometriaFactory-Web` declara lo mismo desde su lado —«la pieza pública no puede ser la última defensa de ninguna regla, porque el navegador no es confiable»— y `GeometriaFactory-Api` garantiza que **ningún punto de acceso quede fuera de la guardia**.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador. |
| 2.0 | 2026-08-25 | **Migración normativa 10.0 → 13.3, fase M4, corte de la categoría 06** (`Audit/Plan-Migracion-10.0-a-13.3.md` **1.2** §4.1). **§5 se parte en 5 · prioridad y 5.b · estimación**, que es lo que `Rules-Backlog-Tecnico.md` **5.0** §4.4 exige desde el salto: lo que separa las dos mitades **no es un evento sino un dueño**. **La estimación se declara «no aplica» y no se difiere**, cerrada **por lectura y no por decisión**: `PRODUCT-INTAKE` §2 declara `equipo_n = 1`, `Mini-Plan.md` §1.2 declara que **no hay capacidad numérica y es deliberado**, y el hecho que lo cierra es que **ocho etapas se cerraron sin una sola estimación**. La forma es la de [`../../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md`](../../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md), `Propuesto`. **Lo anterior remitía a un ítem vencido** —`PA-01`, diferido al punto de control de la etapa `c`, que cerró el 2026-08-14—, que con la forma nueva habría entrado acá como **P1**. Estado previo archivado en [`_legacy/2026-08-25/US-04030-Impedir-Que-Una-Cuenta-Marcada-Ejerza-Cualquier-Otra-Capacidad-v1.0.md`](_legacy/2026-08-25/US-04030-Impedir-Que-Una-Cuenta-Marcada-Ejerza-Cualquier-Otra-Capacidad-v1.0.md). Sube **major**: el salto de la regla que lo gobierna es major. |
