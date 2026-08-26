# US-06023 — Proveer el sello por un puerto, para que las pruebas lo puedan fijar

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-06023-Proveer-El-Sello-Por-Un-Puerto-Para-Que-Las-Pruebas-Lo-Puedan-Fijar.md
**Versión:** 2.0
**Estado:** Aprobada
**Fecha:** 2026-08-25
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-06002 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Prioridad MoSCoW:** **Should**
**Estimación:** **No aplica** — el producto no estima; ver §5.b

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **obtener el momento actual por un puerto y no leyendo el reloj del entorno**, para **que las pruebas de las capas de adentro sean reproducibles sin fijar el reloj de la máquina**.

## 2. Contexto

`PRODUCT-INTAKE` §17.1.P.11 · GeometriaFactory-Application punto 3 declara que **el reloj es un puerto para que las fechas de alta y modificación sean verificables en prueba**, y `Domain ADR-06006` declara que el dominio no lee el reloj. El contrato de uso es [`CU-06009`](../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06009-Proveer-El-Sello-Del-Reloj-Del-Sistema.md), que `02` llama **el contrato más corto de la capa y el que explica por qué la capa se puede probar entera con dobles**.

## 3. Criterios de aceptación

- Given una invocación al puerto de reloj, When se la resuelve, Then devuelve el momento actual.
- Given un doble del puerto en una batería de pruebas, When se repite una operación que sella, Then el sello es **reproducible** sin fijar el reloj del entorno.
- Given el adaptador, When se inspeccionan sus dependencias, Then **no depende del contexto de persistencia** y no tiene ninguna del producto.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | **Ninguna.** `02` §7.2 declara que `CU-06009` **no traza a ninguna necesidad de negocio**, y que inventarle una haría creer que hay una necesidad detrás de una decisión de testabilidad |
| CU cubiertos | CU-06009 |
| RN que ejerce | — |
| Componente de `05` §3.1 | Adaptador de reloj del sistema |
| Reglas conceptuales de modelo | `RC-06006`, tres sellos de tiempo distintos |
| ¿Toma alguna decisión de negocio? | **No** |
| ¿Toca el almacén? | **No** |
| BT derivadas | BT-06008, BT-06012 |
| Tests previstos en 08 | Su valor se mide en las pruebas de las capas de adentro que lo reemplazan por un doble |

## 5. Prioridad

**`Should`, y es la única de las veinticinco.** Su origen **no es una capacidad** del intake §4 sino una **decisión de testabilidad**, y su caso de uso es el único de los diez que **no traza a ninguna necesidad de negocio** (`02` §7.2). El producto funciona sin ella —las capas de adentro leerían el reloj del entorno— y lo que se pierde es que sus pruebas sean reproducibles. **Diferible, y con un costo que se paga en cada batería.** El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.2.

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

**La zona horaria y la precisión no las decide esta historia sino BT-06008**, que cierra un punto abierto de la categoría 02: los sellos se producen y se guardan en tiempo universal coordinado, **sin truncar la precisión**, y la conversión a la zona de quien lee es de la superficie que lo muestra.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
| 2.0 | 2026-08-25 | **Migración normativa 10.0 → 13.3, fase M4, corte de la categoría 06** (`Audit/Plan-Migracion-10.0-a-13.3.md` **1.2** §4.1). **§5 se parte en 5 · prioridad y 5.b · estimación**, que es lo que `Rules-Backlog-Tecnico.md` **5.0** §4.4 exige desde el salto: lo que separa las dos mitades **no es un evento sino un dueño**. **La estimación se declara «no aplica» y no se difiere**, cerrada **por lectura y no por decisión**: `PRODUCT-INTAKE` §2 declara `equipo_n = 1`, `Mini-Plan.md` §1.2 declara que **no hay capacidad numérica y es deliberado**, y el hecho que lo cierra es que **ocho etapas se cerraron sin una sola estimación**. La forma es la de [`../../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md`](../../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md), `Propuesto`. **Lo anterior remitía a un ítem vencido** —`PA-01`, diferido al punto de control de la etapa `c`, que cerró el 2026-08-14—, que con la forma nueva habría entrado acá como **P1**. Estado previo archivado en [`_legacy/2026-08-25/US-06023-Proveer-El-Sello-Por-Un-Puerto-Para-Que-Las-Pruebas-Lo-Puedan-Fijar-v1.0.md`](_legacy/2026-08-25/US-06023-Proveer-El-Sello-Por-Un-Puerto-Para-Que-Las-Pruebas-Lo-Puedan-Fijar-v1.0.md). Sube **major**: el salto de la regla que lo gobierna es major. |
