# US-00026 — Conectar cada puerto con su adaptador y tomar de configuración lo que el despliegue provee

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-00026-Conectar-Cada-Puerto-Con-Su-Adaptador-Y-Tomar-La-Configuracion.md
**Versión:** 2.0
**Estado:** Aprobada
**Fecha:** 2026-08-25
**Autor:** Scrum Master + API Product Owner (AG-06)
**Épica:** EP-00001 Esqueleto ambulante y verificación de viabilidad
**Etapa del producto:** `a`
**Punto de acceso:** Ninguno: la composición de raíz no atiende peticiones
**Prioridad MoSCoW:** Must
**Estimación:** **No aplica** — el producto no estima; ver §5.b

## 1. Historia

Como **producto**, quiero **que los cuatro puertos queden conectados con sus cuatro adaptadores en un solo lugar y que toda la configuración del despliegue entre por ahí**, para **que la frontera sea contable y que un puerto sin adaptador falle en construcción y no en la primera petición**.

## 2. Contexto

`02` §3 declara la **composición de la aplicación** como una de las cinco responsabilidades. `05` §2.1 descartó repartirla en módulos por área con un fundamento propio: **el defecto característico de esta capa es de omisión**, y un puerto sin adaptador **se detecta comparando contra una lista, no leyendo un módulo**. El contrato de uso es [`CU-00010`](../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-00010-Componer-La-Aplicacion-Y-Conectar-Los-Puertos-Con-Sus-Adaptadores.md).

## 3. Criterios de aceptación

- Given la composición de raíz, When arranca el servicio, Then los **4 de 4** puertos resuelven a su adaptador, y hay **0** puertos sin adaptador o con más de uno.
- Given un puerto sin adaptador, When se intenta arrancar, Then **falla en construcción** y **no hay petición que responder**.
- Given la configuración del despliegue —ubicación del almacén, clave de firma, vigencia del acceso y límite de cuerpo—, When se la busca en el código, Then **entra sólo por acá** y **ningún componente la lee por su cuenta**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | **Ninguna.** `02` §7.2 declara que `CU-00010` **no traza a ninguna necesidad**: conectar un puerto con su adaptador es **construcción, no capacidad**, y nadie la percibe |
| CU cubiertos | CU-00010 |
| RN que ejerce | — |
| Componente de `05` §3.1 | Composición de raíz, **transversal** |
| ¿Decide qué se dice? | **No** |
| Familia empobrecida | **No** |
| BT derivadas | BT-00002, BT-00008, BT-00010 |
| Tests previstos en 08 | Prueba de arranque que resuelve las cuatro dependencias y falla en construcción si falta alguna |

## 5. Prioridad

`Must` porque **todo lo demás es probable con dobles gracias a esta historia**, que es lo que las tres capas de adentro dan por sentado; y porque `05` §9 declara con probabilidad media el riesgo de que un puerto quede sin adaptador y **el fallo aparezca en la primera petición, en producción y sin nadie mirando**.

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

**El nombre del cuarto puerto se fija en el punto de control de la etapa `a`, y no acá.** Lo declara `GeometriaFactory-Application` y su ADR correspondiente lo ató a ese punto de control; esta capa **conecta exactamente cuatro** puertos con cuatro adaptadores y no nombra ninguno por su cuenta.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
| 2.0 | 2026-08-25 | **Migración normativa 10.0 → 13.3, fase M4, corte de la categoría 06** (`Audit/Plan-Migracion-10.0-a-13.3.md` **1.2** §4.1). **§5 se parte en 5 · prioridad y 5.b · estimación**, que es lo que `Rules-Backlog-Tecnico.md` **5.0** §4.4 exige desde el salto: lo que separa las dos mitades **no es un evento sino un dueño**. **La estimación se declara «no aplica» y no se difiere**, cerrada **por lectura y no por decisión**: `PRODUCT-INTAKE` §2 declara `equipo_n = 1`, `Mini-Plan.md` §1.2 declara que **no hay capacidad numérica y es deliberado**, y el hecho que lo cierra es que **ocho etapas se cerraron sin una sola estimación**. La forma es la de [`../../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md`](../../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md), `Propuesto`. **Lo anterior remitía a un ítem vencido** —`PA-01`, diferido al punto de control de la etapa `c`, que cerró el 2026-08-14—, que con la forma nueva habría entrado acá como **P1**. Estado previo archivado en [`_legacy/2026-08-25/US-00026-Conectar-Cada-Puerto-Con-Su-Adaptador-Y-Tomar-La-Configuracion-v1.1.md`](_legacy/2026-08-25/US-00026-Conectar-Cada-Puerto-Con-Su-Adaptador-Y-Tomar-La-Configuracion-v1.1.md). Sube **major**: el salto de la regla que lo gobierna es major. |
