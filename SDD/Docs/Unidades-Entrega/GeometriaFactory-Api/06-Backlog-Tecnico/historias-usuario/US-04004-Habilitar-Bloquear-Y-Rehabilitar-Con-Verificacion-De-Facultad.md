# US-04004 — Habilitar, bloquear y rehabilitar una cuenta con verificación de facultad

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-04004-Habilitar-Bloquear-Y-Rehabilitar-Con-Verificacion-De-Facultad.md
**Versión:** 2.0
**Estado:** Aprobada
**Fecha:** 2026-08-25
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-04003 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Prioridad MoSCoW:** Must
**Estimación:** **No aplica** — el producto no estima; ver §5.b

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **orquestar las transiciones de admisión de una cuenta ajena verificando antes que quien las pide tenga el papel `Administrador`**, para **que sólo el docente decida quién entra al laboratorio y quién queda afuera**.

## 2. Contexto

`NB-00001` pide control de admisión y `F-03` del intake §4 lo declara `Must Have`. El contrato de uso es [`CU-00023`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00023-Gobernar-Las-Cuentas-De-La-Comision.md). La verificación de **facultad** es una de las cuatro comprobaciones transversales de `02` §4, y admite ser explícita porque **no hay recurso ajeno cuya existencia proteger**.

## 3. Criterios de aceptación

- Given un solicitante con papel `Administrador` y una cuenta de alumno en un estado que admite la transición, When se pide habilitar, bloquear o rehabilitar, Then la transición se aplica y el estado resultante es el que el dominio resuelve.
- Given un solicitante sin el papel `Administrador`, When pide cualquiera de las tres, Then se devuelve el motivo de **facultad requerida** y **el estado de la cuenta no cambia**.
- Given una transición que el dominio no admite desde el estado vigente, When se la pide con la facultad correcta, Then se devuelve el motivo del dominio **sin colapsarlo** con el de facultad.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00001 |
| CU cubiertos | CU-04002 |
| RN e invariantes que ejerce | RN-04001, RN-04006, RN-04016; INV-06, INV-08 |
| Componente de `05` §3.1 | Orquestación del gobierno de cuentas, Guarda de autorización |
| Puertos que consume | Repositorio de cuentas |
| Comprobación de `02` §4 que la alcanza | **Facultad**, y **cambio de contraseña pendiente**, que corta antes que ella |
| BT derivadas | BT-04010, BT-04013 |
| Tests previstos en 08 | Prueba de la negativa de facultad sin base de datos, y prueba de que la cuarta comprobación corta antes |

## 5. Prioridad

`Must` por derivar de `F-03`, `Must Have`, y porque el criterio de transición `d` → `e` exige que el administrador habilite, bloquee y rehabilite.

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

**Habilitar y rehabilitar no terminan acá**: por `RN-04016` producen además la contraseña provisoria y dejan la marca puesta, y eso es US-04008. Esta historia cubre la transición de admisión; la credencial provisoria es la otra mitad del mismo acto y se declara aparte para que cada una tenga su criterio.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia prevista en `02` §7.3 con este identificador. |
| 2.0 | 2026-08-25 | **Migración normativa 10.0 → 13.3, fase M4, corte de la categoría 06** (`Audit/Plan-Migracion-10.0-a-13.3.md` **1.2** §4.1). **§5 se parte en 5 · prioridad y 5.b · estimación**, que es lo que `Rules-Backlog-Tecnico.md` **5.0** §4.4 exige desde el salto: lo que separa las dos mitades **no es un evento sino un dueño**. **La estimación se declara «no aplica» y no se difiere**, cerrada **por lectura y no por decisión**: `PRODUCT-INTAKE` §2 declara `equipo_n = 1`, `Mini-Plan.md` §1.2 declara que **no hay capacidad numérica y es deliberado**, y el hecho que lo cierra es que **ocho etapas se cerraron sin una sola estimación**. La forma es la de [`../../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md`](../../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md), `Propuesto`. **Lo anterior remitía a un ítem vencido** —`PA-01`, diferido al punto de control de la etapa `c`, que cerró el 2026-08-14—, que con la forma nueva habría entrado acá como **P1**. Estado previo archivado en [`_legacy/2026-08-25/US-04004-Habilitar-Bloquear-Y-Rehabilitar-Con-Verificacion-De-Facultad-v1.0.md`](_legacy/2026-08-25/US-04004-Habilitar-Bloquear-Y-Rehabilitar-Con-Verificacion-De-Facultad-v1.0.md). Sube **major**: el salto de la regla que lo gobierna es major. |
